using MvcAdapter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Config;
using Config.Logic;

namespace Expenses.Controllers
{
    public class BudgetUnitSelectorController : ExpensesController
    {
        protected override string TableName
        {
            get
            {
                return "S_EP_BudgetUnit";
            }
        }

        public JsonResult GetVersionList(string UnitID)
        {
            var sql = "SELECT * FROM S_EP_BudgetVersion WHERE BudgetUnitID = '{0}'";
            var data = this.SQLDB.ExecuteDataTable(String.Format(sql, UnitID));
            return Json(data);
        }

        public JsonResult GetBudgetUnitList(QueryBuilder qb)
        {
            var sql = "select * from S_EP_BudgetUnit";
            var data = this.SQLDB.ExecuteDataTable(sql, qb);
            return Json(data);
        }
    }
}