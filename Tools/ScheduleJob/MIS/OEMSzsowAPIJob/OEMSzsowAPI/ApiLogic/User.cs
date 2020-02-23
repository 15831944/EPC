using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Config.Logic;
using OEMSzsowAPI.Helper;
using Formula.Helper;
using OEMSzsowAPI.Model;
using Formula;

namespace OEMSzsowAPI.ApiLogic
{
    public class User : BaseApi
    {
        public User(S_OEM_TaskList task)
            : base(task)
        { }

        public override void SaveLogic(string businessID)
        {
            var api = this.BaseServerUrl + this.Task.BusinessCode + "/edit";
            var dt = this.BaseSQLHelper.ExecuteDataTable("select * from S_A_User where id='" + businessID + "'");
            var userinfo = new Dictionary<string, object>();
            if (dt.Rows.Count > 0)
                userinfo = DataHelper.DataRowToDic(dt.Rows[0]);
            else
                throw new Exception("未找到指定ID的业务数据");
            //设置参数
            var param = new UserRequestData();
            param.id = userinfo.GetValue("ID");
            param.name = userinfo.GetValue("Name");
            param.loginid = userinfo.GetValue("Code");
            param.signature = null;
            param.enable = userinfo.GetValue("IsDelete") == "1" ? false : true;
            //签名
            var obj = this.BaseSQLHelper.ExecuteScalar("select DwgFile from S_A_UserImg where UserID='" + businessID + "'");
            if (obj != null && obj != DBNull.Value)
            {
                byte[] bytes = (byte[])obj;
                param.signature = Convert.ToBase64String(bytes);
            }
            //请求
            this.Task.RequestUrl = api;
            this.Task.RequestData = JsonHelper.ToJson<UserRequestData>(param);
            var response = HttpHelper.Post(api, param);
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

        public List<string> GetIDs()
        {
            var api = this.BaseServerUrl + "user/getdata";
            var rtn = new List<string>();
            try
            {
                StartSync();
                //请求
                this.Task.RequestUrl = api;
                var response = HttpHelper.Get(api);
                this.Task.Response = response;
                var dataStr = AnalysisResponse();
                ComplateSync();
                if (dataStr.StartsWith("["))
                {
                    var list = JsonHelper.ToObject<List<Dictionary<string, object>>>(dataStr);
                    rtn = list.Select(a => a.GetValue("ID")).ToList();
                }
            }
            catch (Exception e)
            {
                LogWriter.Error(e, string.Format("TaskID：{0}，错误：{1}", Task.ID, e.Message));
                ErrorSync(e.Message);
            }
            return rtn;
        }
    }

    public class UserRequestData
    {
        public string id { get; set; }
        public string name { get; set; }
        public string loginid { get; set; }
        public string signature { get; set; }
        public bool enable { get; set; }
    }
}
