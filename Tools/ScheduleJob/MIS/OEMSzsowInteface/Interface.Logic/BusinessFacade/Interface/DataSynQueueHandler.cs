using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Config;
using Config.Logic;
using System.Data;
using Formula.Helper;
using System.Configuration;

namespace Interface.Logic
{
    public class DataSynQueueHandler
    {
        #region 公共属性

        SQLHelper _SQLHelpeBase;
        public SQLHelper SQLHelpeBase
        {
            get
            {
                if (_SQLHelpeBase == null)
                    _SQLHelpeBase = SQLHelper.CreateSqlHelper(ConnEnum.Base);
                return _SQLHelpeBase;
            }
        }

        SQLHelper _SQLHelpeProject;
        public SQLHelper SQLHelpeProject
        {
            get
            {
                if (_SQLHelpeProject == null)
                    _SQLHelpeProject = SQLHelper.CreateSqlHelper(ConnEnum.Project);
                return _SQLHelpeProject;
            }
        }

        SQLHelper _SQLHelperInterface;
        public SQLHelper SQLHelperInterface
        {
            get
            {
                if (_SQLHelperInterface == null)
                    _SQLHelperInterface = SQLHelper.CreateSqlHelper("SzsowInteface");
                return _SQLHelperInterface;
            }
        }

        public string OEMCollactorSerial = ConfigurationManager.AppSettings["OEMCollactorSerial"];//OEM校对环节编号
        public string OEMAuditorSerial = ConfigurationManager.AppSettings["OEMAuditorSerial"];//OEM审核环节编号
        public string OEMApproverSerial = ConfigurationManager.AppSettings["OEMApproverSerial"];//OEM审定环节编号
        #endregion

        public void ExecuteQueue()
        {
            var queueDataList = this.SQLHelperInterface.ExecuteList<I_DataSynQueue>("select * from I_DataSynQueue where SynState='" + SynState.New.ToString() + "' order by CreateTime,ID");
            foreach (var queueData in queueDataList)
            {
                try
                {
                    //业务处理
                    SynDataFO fo = null;
                    switch (queueData.RelateTable)
                    {
                        case "S_A_BaseEnum": fo = new BaseEnumFO(); break;
                        case "S_A_User": fo = new UserFO(); break;
                        case "S_I_ProjectInfo": fo = new ProjectFO(); break;
                        case "S_P_MileStone": fo = new MilestoneFO(); break;
                        case "S_D_Input": fo = new DesignInputFO(); break;
                        case "S_D_InputDocument": fo = new DesignInputFileFO(); break;
                        case "S_AE_AuditAdviceCatalog": fo = new AuditAdviceCatalogFO(); break;
                        case "S_AE_AuditAdvice": fo = new AuditAdviceFO(); break;
                        case "S_W_TaskWork": fo = new TaskWorkFO(); break;
                        case "S_E_Product":
                        case "I_FlowAuditProduct":
                        case "I_FlowChangeProduct":
                        case "I_FlowExchangeProduct":
                        case "I_FlowSignProduct": fo = new GetDataFO(); break;
                        default: break;
                    }
                    if (!fo.ValidateRequeset(queueData))
                        continue;
                    if (queueData.SynType == SynType.GetData.ToString())
                        queueData.Response = HttpHelper.Get(queueData.RequestUrl);
                    else
                        //queueData.Response = HttpHelper.Post(queueData.RequestUrl, JsonHelper.ToObject<ProjectRequestData>(queueData.RequestData));
                        queueData.Response = HttpHelper.Post(queueData.RequestUrl, queueData.RequestData);
                    if (string.IsNullOrEmpty(queueData.Response))
                        throw new Exception("请求接口没有返回值");
                    else if (!queueData.Response.StartsWith("{") && !queueData.Response.StartsWith("["))
                        throw new Exception("请求接口返回值格式错误：" + queueData.Response);

                    //接口返回对象
                    var rtn = JsonHelper.ToObject(queueData.Response);
                    var errorStr = string.Empty;

                    if (rtn.GetValue("ResultType") != "0")
                    {
                        if (!string.IsNullOrEmpty(rtn.GetValue("Message")))
                            errorStr = string.Format("Message：{0}，LogMessage：{1}", rtn.GetValue("Message"), rtn.GetValue("LogMessage"));
                        else
                            errorStr = queueData.Response;
                        if (rtn.GetValue("ResultType") != "9")
                            throw new Exception("请求接口失败：" + errorStr);
                    }

                    var responseDataStr = rtn.GetValue("AppendData");

                    var interfaceSb = new StringBuilder();
                    fo.ExecuteQueueLogic(queueData, responseDataStr, interfaceSb);
                    ComplateSync(queueData, interfaceSb);
                }
                catch (Exception e)
                {
                    ErrorSync(queueData, e.Message);
                }
                //queueData = this.SQLHelperInterface.ExecuteObject<I_DataSynQueue>("select top 1 * from I_DataSynQueue where SynState='" + SynState.New.ToString() + "' order by CreateTime,ID");
            }
        }

        #region 私有方法

        /// <summary>
        /// 接口正常返回处理
        /// </summary>
        /// <param name="queueData"></param>
        private void ComplateSync(I_DataSynQueue queueData, StringBuilder interfaceSb)
        {
            queueData.SynTime = DateTime.Now;
            interfaceSb.AppendLine(@" delete from I_DataSynQueue where ID='" + queueData.ID.ToString() + "'");
            if (interfaceSb.Length > 0)
                this.SQLHelperInterface.ExecuteNonQuery(interfaceSb.ToString());
        }

        /// <summary>
        /// 接口错误处理
        /// </summary>
        /// <param name="queueData"></param>
        /// <param name="msg"></param>
        private void ErrorSync(I_DataSynQueue queueData, string msg)
        {
            queueData.SynState = SynState.Error.ToString();
            queueData.SynTime = DateTime.Now;
            queueData.ErrorMsg = msg;
            if (string.IsNullOrEmpty(queueData.Response)) queueData.Response = string.Empty;
            string sql = @" update I_DataSynQueue set SynTime = '{1}', Response='{2}',ErrorMsg='{3}' ,SynState='{4}' 
                where ID='{0}'";
            sql = string.Format(sql, queueData.ID.ToString(), queueData.SynTime.Value.ToString("yyyy-MM-dd HH:mm:ss")
                , queueData.Response.Replace("'", "''"), queueData.ErrorMsg.Replace("'", "''"), queueData.SynState.ToString());
            this.SQLHelperInterface.ExecuteNonQuery(sql);
        }

        #endregion

    }
}
