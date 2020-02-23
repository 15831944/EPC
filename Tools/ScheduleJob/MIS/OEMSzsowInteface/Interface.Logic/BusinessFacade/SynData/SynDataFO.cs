using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Config;
using System.Configuration;

namespace Interface.Logic
{
    public class SynDataFO
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

        SQLHelper _SQLHelpeProjectConfig;
        public SQLHelper SQLHelpeProjectConfig
        {
            get
            {
                if (_SQLHelpeProjectConfig == null)
                    _SQLHelpeProjectConfig = SQLHelper.CreateSqlHelper(ConnEnum.InfrasBaseConfig);
                return _SQLHelpeProjectConfig;
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

        string _BaseServerUrl;
        public string BaseServerUrl
        {
            get
            {
                if (_BaseServerUrl == null)
                {
                    _BaseServerUrl = ConfigurationManager.AppSettings["OEMSyncURL"] ?? "";
                    if (!_BaseServerUrl.EndsWith("/"))
                        _BaseServerUrl = _BaseServerUrl + "/";
                }
                return _BaseServerUrl;
            }
        }

        public string OEMCollactorSerial = ConfigurationManager.AppSettings["OEMCollactorSerial"];//OEM校对环节编号
        public string OEMAuditorSerial = ConfigurationManager.AppSettings["OEMAuditorSerial"];//OEM审核环节编号
        public string OEMApproverSerial = ConfigurationManager.AppSettings["OEMApproverSerial"];//OEM审定环节编号
        

        public string SaveQueueSqlTmp = @"
if exists(select 1 from I_DataSynQueue where SynType='{0}' and RelateID='{2}' and SynState ='" + SynState.New.ToString() + @"')
    update I_DataSynQueue set CreateTime = '{4}', RequestData='{5}',  RequestUrl='{6}' where SynType='{0}' and RelateID='{2}' and SynState ='" + SynState.New.ToString() + @"'
else
    insert into I_DataSynQueue(SynType,RelateTable,RelateID,RelateType,CreateTime,RequestData,RequestUrl,SynState) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','" + SynState.New.ToString() + "')";

        #endregion


        public virtual void ExecuteQueueLogic(I_DataSynQueue queueData, string responseDataStr, StringBuilder interfaceSb)
        {
            throw new NotImplementedException();
        }

        public virtual bool ValidateRequeset(I_DataSynQueue queueData)
        {
            return true;
        }
    }
}
