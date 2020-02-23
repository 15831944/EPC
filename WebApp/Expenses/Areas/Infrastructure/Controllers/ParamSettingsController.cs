using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Formula.Helper;
using Config.Logic;

namespace Expenses.Areas.Infrastructure.Controllers
{
    public class ParamSettingsController : InstrasController
    {
        public override JsonResult GetModel(string id)
        {
            string sql = "select * from S_EP_DefineParams";
            var result = new Dictionary<string, object>();
            var dt = this.SQLDB.ExecuteDataTable(sql);
            foreach (DataRow row in dt.Rows)
            {
                result[row["Code"].ToString()] = row["Value"].ToString();
            }
            return Json(result);
        }

        public override JsonResult Save()
        {
            var dic = new Dictionary<string, object>();
            var formData = this.Request["FormData"];
            dic = JsonHelper.ToObject(formData);
            foreach (var key in dic.Keys)
            {
                if (!String.IsNullOrEmpty(dic.GetValue(key)))
                {
                    var sql = String.Format("select count(ID) from S_EP_DefineParams where Code='{0}'", key);
                    var obj = this.SQLDB.ExecuteScalar(sql);
                    if (Convert.ToInt32(obj) > 0)
                    {
                        sql = String.Format("update S_EP_DefineParams set Value='{1}' where Code='{0}'", key, dic.GetValue(key));
                    }
                    else
                    {
                        sql = String.Format("insert into S_EP_DefineParams (Code,Value) values ('{0}','{1}')", key, dic.GetValue(key));
                    }
                    this.SQLDB.ExecuteNonQuery(sql);
                }
            }
            return Json("");
        }

        public JsonResult ResetSysParam()
        {
            Expenses.Logic.SysParams.Reset();
            return Json("");
        }
    }
}
