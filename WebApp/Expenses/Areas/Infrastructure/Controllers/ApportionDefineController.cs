using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Transactions;
using Config;
using Config.Logic;
using Formula;
using Formula.Helper;
using MvcAdapter;
using Expenses.Logic;
using Expenses.Logic.Domain;

namespace Expenses.Areas.Infrastructure.Controllers
{
    public class ApportionDefineController : InstrasController
    {
        private readonly string[] _fieldMapExceptFields = new string[] { "ID", "S_EP_CostApportionID", "SortIndex",
            "IsReleased"};

        protected override string TableName
        {
            get
            {
                return "S_EP_DefineApportion";
            }
        }

        public ActionResult DefineConfig()
        {
            string sql = string.Format("select text=a.name,value=a.name from syscolumns a  inner join sysobjects d on a.id=d.id and d.name='{0}'", "S_EP_CostApportion_Detail");
            var marketHelper = SQLHelper.CreateSqlHelper(ConnEnum.Market);
            var dt = marketHelper.ExecuteDataTable(sql);
            var res = FormulaHelper.DataTableToListDic(dt);
            res.RemoveAll(a => _fieldMapExceptFields.Contains(a.GetValue("text")));
            ViewBag.FieldMapCodeList = JsonHelper.ToJson(res);
            return View();
        }

        public JsonResult SaveDefineConfig()
        {
            string id = GetQueryString("ID");
            string formData = GetQueryString("FormData");
            var formDic = JsonHelper.ToObject(formData);
            S_EP_DefineApportion define = new S_EP_DefineApportion(formDic);
            define.Validate();
            formDic.UpdateDB(this.SQLDB, TableName, id);
            return Json(true);
        }

        public JsonResult GetDefineConfig()
        {
            string id = GetQueryString("ID");
            var formDic = this.GetDataDicByID(TableName, id);
            return Json(formDic);
        }

        public JsonResult GetFieldMapCodeList()
        {
            string sql = string.Format("select text=a.name,value=a.name from syscolumns a  inner join sysobjects d on a.id=d.id and d.name='{0}'", TableName);
            var dt = this.SQLDB.ExecuteDataTable(sql);
            return Json(dt);
        }
    }
}
