using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Config.Logic;
using OEMSzsowAPI.Helper;
using Formula.Helper;
using OEMSzsowAPI.Model;
using Formula;
using Config;
using System.Data;
using OEMSzsowAPI.Common;

namespace OEMSzsowAPI.ApiLogic
{
    public class Exchange : BaseApi
    {
        SQLHelper _BusinessSQLHelper;
        public SQLHelper BusinessSQLHelper
        {
            get
            {
                if (_BusinessSQLHelper == null)
                    _BusinessSQLHelper = SQLHelper.CreateSqlHelper(ConnEnum.Project);
                return _BusinessSQLHelper;
            }
        }
        
        public Exchange(S_OEM_TaskList task)
            : base(task)
        { }

        public override void SaveLogic(string businessID)
        {
            var dt = this.BusinessSQLHelper.ExecuteDataTable(@"select m.*,wbs.Name WBSName,wbs.WBSType from S_P_MileStone  m
            left join S_W_WBS wbs on m.WBSID = wbs.ID 
            where m.ProjectInfoID='" + businessID + "' and m.MileStoneType = 'Cooperation'");
            if (dt.Rows.Count <= 0)
                throw new Exception("未找到指定ID的业务数据");
            var api = this.BaseServerUrl + this.Task.BusinessCode + "/edit";
            //设置参数
            var param = new ProjectExchangeRequestData();
            param.projectId = businessID;
            param.exchanges = new List<exchange>();
            foreach (DataRow row  in dt.Rows)
            {
                var item = new exchange();
                item.id = row["ID"].ToString();
                item.name = Convert.ToDateTime(row["PlanFinishDate"]).ToString("yyyy.MM.dd") + row["Name"].ToString();
                if (row["WBSType"] != null && row["WBSType"] != DBNull.Value)
                {
                    if (row["WBSType"].ToString() == WBSNodeType.SubProject.ToString())
                        item.subProjectName = row["WBSName"].ToString();
                }
                item.userId = row["CreateUserID"].ToString();
                item.phaseName = GlobalData.PhaseEnum.GetValue(row["PhaseValue"].ToString());
                item.specialtyName = GlobalData.MajorEnum.GetValue(row["MajorValue"].ToString());
                item.RecSpecialties = new List<string>();
                if (row["OutMajorValue"] != null && row["OutMajorValue"] != DBNull.Value)
                {
                    foreach (var _i in row["OutMajorValue"].ToString().Split(','))
                        item.RecSpecialties.Add(GlobalData.MajorEnum.GetValue(_i));
                }
                param.exchanges.Add(item);
            }

            //请求
            this.Task.RequestUrl = api;
            this.Task.RequestData = JsonHelper.ToJson<ProjectExchangeRequestData>(param);
            var response = HttpHelper.Post(api, param);
            this.Task.Response = response;
        }

        public override void GetDataLogic(string businessID)
        {
            if (string.IsNullOrEmpty(businessID))
                return;
            var api = this.BaseServerUrl + this.Task.BusinessCode + "/getdata?projectId=" + businessID;
            //请求
            this.Task.RequestUrl = api;
            var response = HttpHelper.Get(api);
            this.Task.Response = response;
        }

        public override void RemoveLogic(string businessID)
        {
            var api = this.BaseServerUrl + this.Task.BusinessCode + "/delete";
            //设置参数
            var param = new DeleteRequestData();
            param.id = businessID;
            //请求
            this.Task.RequestUrl = api;
            this.Task.RequestData = JsonHelper.ToJson<DeleteRequestData>(param);
            var response = HttpHelper.Post(api, param);
            this.Task.Response = response;
        }

        public override void AfterGetList(List<Dictionary<string, object>> dataList)
        {
            //base.AfterGetList(dataList);
            if (this.Task.BusinessType != BusinessType.GetData.ToString())
                return;
            var projectInfoID = this.Task.BusinessID;
            var mileStoneDt = this.BusinessSQLHelper.ExecuteDataTable(@"select * from S_P_MileStone where ProjectInfoID='" + projectInfoID + "'");
            var detailDt = this.BusinessSQLHelper.ExecuteDataTable(@"select * from S_P_MileStone_ProductDetail where ProjectInfoID='" + projectInfoID + "'");

            StringBuilder sb = new StringBuilder();
            StringBuilder filesb = new StringBuilder();
            var errorList = new List<Dictionary<string, string>>();
            foreach (var item in dataList)
            {
                var plan = JsonHelper.ToObject(item.GetValue("Plan"));
                var files = JsonHelper.ToList(item.GetValue("Files"));
                var mileStoneID = plan.GetValue("TaskID");
                var msRow = mileStoneDt.Select("ID='" + mileStoneID + "'").FirstOrDefault();
                if (msRow == null)
                {
                    //errorList.Add(new Dictionary<string, string>() { { "ID", mileStoneID }, { "Name", plan.GetValue("Name") }, { "Msg", "S_P_MileStone为null" } });
                    //删除
                    filesb.AppendFormat("insert into S_OEM_TaskList(OEMCode,BusinessType,BusinessCode,BusinessID,CreateTime) values( '{0}','{1}','{2}','{3}','{4}')",
                        Task.OEMCode, BusinessType.Remove.ToString(), Task.BusinessCode, plan.GetValue("ID"), Task.CreateTime.ToString());
                    filesb.AppendLine();
                    continue;
                }
                foreach (var f in files)
                {
                    var file = JsonHelper.ToObject(f.GetValue("File"));
                    var records = JsonHelper.ToList(f.GetValue("Records"));
                    var detailID = file.GetValue("ID");
                    var md5Code = file.GetValue("MD5");
                    var fileName = file.GetValue("Name");
                    var userID = file.GetValue("LockUserID");
                    var userRow = GlobalData.UserDt.Select("ID='" + userID + "'").FirstOrDefault();
                    var userName = file.GetValue("SendUserName");
                    if (userRow != null)
                        userName = userRow["Name"].ToString();
                    var date = DataHelper.FormatTime(file.GetValue("CreateDate"));
                    //生成业务记录S_P_MileStone_ProductDetail
                    var detailRow = detailDt.Select("ID='" + detailID + "'").FirstOrDefault();
                    if (detailRow == null)
                    {
                        detailRow = detailDt.NewRow();
                        detailDt.Rows.Add(detailRow);
                        detailRow["ID"] = detailID;
                        detailRow["S_P_MileStoneID"] = mileStoneID;
                        detailRow["ProjectInfoID"] = projectInfoID;
                        detailRow["CreateUser"] = userName;
                        detailRow["CreateUserID"] = userID;
                        detailRow["CreateDate"] = date;
                    }
                    detailRow["ModifyUser"] = userName;
                    detailRow["ModifyUserID"] = userID;
                    detailRow["ModifyDate"] = date;
                    detailRow["Name"] = fileName;

                    if (detailRow["MD5Code"] == null || detailRow["MD5Code"] == DBNull.Value || detailRow["MD5Code"].ToString() != md5Code)
                    {
                        detailRow["MD5Code"] = md5Code;
                        #region 生成待同步文件队列

                        if (!string.IsNullOrEmpty(md5Code) && !string.IsNullOrEmpty(fileName))
                        {
                            string fileTaskSql = @" insert into S_OEM_TaskFileList(OEMCode,BusinessCode,BusinessID,CreateTime,MD5Code,FileName) 
                                        values( '{0}','{1}','{2}','{3}','{4}','{5}')";
                            fileTaskSql = string.Format(fileTaskSql, Task.OEMCode, Task.BusinessCode, detailID, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), md5Code, fileName.Replace("'", "''"));
                            filesb.AppendLine(fileTaskSql);
                        }
                        #endregion
                    }
                    var receiveList = new List<Dictionary<string, object>>();
                    foreach (var receive in records)
                    {
                        //Status：0：未接收（已发送），1：已接收，2：已拒绝
                        //Flag：0：未确认，1：已确认
                        var dic = new Dictionary<string, object>();
                        dic.SetValue("ReceiveSpecialtyName", receive.GetValue("ReceiveSpecialtyName"));//接收专业
                        var _status = receive.GetValue("Status");
                        dic.SetValue("Status", _status == "0" ? "未接收" : _status == "1" ? "已接收" : "已拒绝");//接收状态
                        if (_status != "0")
                        {
                            dic.SetValue("ReceiveUserName", receive.GetValue("ReceiveUserName"));//接收人
                            dic.SetValue("ReceiveDate", DataHelper.FormatTime(receive.GetValue("ReceiveDate")).ToShortDateString());//接收时间
                            dic.SetValue("Comment", receive.GetValue("Comment"));//备注
                        }
                        receiveList.Add(dic);
                    }
                    detailRow["ReceiveInfo"] = JsonHelper.ToJson(receiveList);
                    string detailSql = SQLHelper.CreateUpdateSql("S_P_MileStone_ProductDetail", detailRow);
                    sb.AppendLine(detailSql);
                }
            }
            if (errorList.Count > 0)
                this.Task.ErrorMsg = JsonHelper.ToJson(errorList);
            if (sb.Length > 0)
                this.BusinessSQLHelper.ExecuteNonQuery(sb.ToString());
            if (filesb.Length > 0)
                this.BaseSQLHelper.ExecuteNonQuery(filesb.ToString());
        }

        public static void test()
        {
            var prjid = "a95f00fb-11c2-4564-96bd-dec56280fbd0";// "a93d00ee-c52a-4852-8f0b-75cc23521278";
            var api = "http://www.szsow.com/api/goodway/exchange/getdata?projectId=" + prjid;
            var response = HttpHelper.Get(api);
            var rtn = JsonHelper.ToObject<Dictionary<string, object>>(response);
            var appendData = JsonHelper.ToList(rtn.GetValue("AppendData"));
            new Exchange(new S_OEM_TaskList() { BusinessID = prjid, BusinessType = BusinessType.GetData.ToString() }).AfterGetList(appendData);
        }

    }
    public class ProjectExchangeRequestData
    {
        public string projectId  { get; set; }//项目id
        public List<exchange> exchanges { get; set; }//提资计划列表
    }
    public class exchange
    {
        public string id { get; set; }
        public string name { get; set; }
        public string userId { get; set; }
        public string phaseName { get; set; }//阶段名称
        public string subProjectName { get; set; }//子项WBSName
        public string specialtyName { get; set; }//专业名称
        public List<string> RecSpecialties { get; set; }//接收专业名称集合
    }
}
