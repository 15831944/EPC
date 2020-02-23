using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Config;
using OEMSzsowAPI.Common;
using OEMSzsowAPI.Model;
using Config.Logic;
using System.Configuration;
using Formula.Helper;
using Formula;

namespace OEMSzsowAPI.ApiLogic
{
    public class BaseApi : AbstractApi
    {
        SQLHelper _BaseSQLHelper;
        public SQLHelper BaseSQLHelper
        {
            get
            {
                if (_BaseSQLHelper == null)
                    _BaseSQLHelper = SQLHelper.CreateSqlHelper(ConnEnum.Base);
                return _BaseSQLHelper;
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

        S_OEM_TaskList _Task;
        public S_OEM_TaskList Task
        {
            get
            {
                return _Task;
            }
        }
        
        public BaseApi(S_OEM_TaskList task)
        {
            _Task = task;
            if (string.IsNullOrEmpty(_Task.RequestData))
                _Task.RequestData = string.Empty;
            if (string.IsNullOrEmpty(_Task.Response))
                _Task.Response = string.Empty;
            if (string.IsNullOrEmpty(_Task.ErrorMsg))
                _Task.ErrorMsg = string.Empty;
        }

        public override void SaveLogic(string businessID)
        {
            throw new NotImplementedException();
        }

        public override void RemoveLogic(string businessID)
        {
            throw new NotImplementedException();
        }

        public virtual void GetDataLogic(string businessID)
        {
        }

        public virtual void AfterGetData(Dictionary<string, object> data)
        { }
        public virtual void AfterGetList(List<Dictionary<string, object>> dataList)
        { }

        public override void Sync()
        {
            try
            {
                StartSync();
                if (Task.BusinessType == BusinessType.Save.ToString())
                    SaveLogic(Task.BusinessID);
                else if(Task.BusinessType == BusinessType.Remove.ToString())
                    RemoveLogic(Task.BusinessID);
                else if (Task.BusinessType == BusinessType.GetData.ToString())
                    GetDataLogic(Task.BusinessID);
                var dataStr = AnalysisResponse();
                if (dataStr.StartsWith("{"))
                {
                    var data = JsonHelper.ToObject<Dictionary<string, object>>(dataStr);
                    AfterGetData(data);
                }
                else if (dataStr.StartsWith("["))
                {
                    var dataList = JsonHelper.ToObject<List<Dictionary<string, object>>>(dataStr);
                    AfterGetList(dataList);
                }
                ComplateSync();
            }
            catch (Exception e)
            {
                LogWriter.Error(e, string.Format("TaskID：{0}，错误：{1}", Task.ID, e.Message));
                ErrorSync(e.Message);
            }
        }
        
        public virtual void StartSync()
        {
            this.Task.SyncState = SyncState.Start.ToString();
            this.Task.SyncTime = DateTime.Now;
        }

        public virtual string AnalysisResponse()
        {
            var error = string.Empty;
            var dataStr = string.Empty;
            if (this.Task.Response.StartsWith("{"))
            {
                var rtn = JsonHelper.ToObject(this.Task.Response);
                if (rtn.GetValue("ResultType") != "0")
                {
                    if (!string.IsNullOrEmpty(rtn.GetValue("Message")))
                        error = string.Format("Message：{0}，LogMessage：{1}", rtn.GetValue("Message"), rtn.GetValue("LogMessage"));
                    else
                        error = this.Task.Response;
                }
                dataStr = rtn.GetValue("AppendData");
            }
            else
                error = this.Task.Response;
            if (!string.IsNullOrEmpty(error))
                throw new Exception(error);
            return dataStr;
        }

        public virtual void ComplateSync()
        {
            this.Task.SyncState = SyncState.Complate.ToString();
            this.Task.SyncTime = DateTime.Now;
            updateTask();
        }

        public virtual void ErrorSync(string msg)
        {
            this.Task.SyncState = SyncState.Error.ToString();
            this.Task.SyncTime = DateTime.Now;
            this.Task.ErrorMsg = msg;
            LogWriter.Info(string.Format("接口调用异常：TaskID：{0}，SyncTime：{1}，BusinessCode：{2}，ErrorMsg：{3}",
                Task.ID, Task.SyncTime.Value.ToString("yyyy-MM-dd HH:mm:ss"), Task.BusinessCode, this.Task.ErrorMsg.Replace("'", "''")));
            updateTask();
        }

        private void updateTask()
        {
            var sql = "update S_OEM_TaskList set SyncState='{1}',SyncTime='{2}',RequestUrl='{3}',RequestData='{4}',Response='{5}',ErrorMsg='{6}' where ID='{0}'";
            sql = string.Format(sql, Task.ID, Task.SyncState, Task.SyncTime.Value.ToString("yyyy-MM-dd HH:mm:ss"), Task.RequestUrl, Task.RequestData.Replace("'", "''"), Task.Response.Replace("'", "''"), Task.ErrorMsg.Replace("'", "''"));
            this.BaseSQLHelper.ExecuteNonQuery(sql);
        }
    }
}
