using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Expenses.Controllers
{
    public class CBSDefineNodeSelectorController : ExpensesController
    {
        protected override string TableName
        {
            get
            {
                return "S_EP_DefineCBSNode";
            }
        }

        public override JsonResult GetTree(MvcAdapter.QueryBuilder qb)
        {
            string fullID = this.GetQueryString("FullID");
            var sql = @"SELECT * FROM S_EP_DefineCBSNode WHERE FullID LIKE '{0}%' AND FULLID!='{0}' and ID not in
                       (select childCBSNode.ID from S_EP_DefineCBSNode parentCBSNode inner join S_EP_DefineCBSNode childCBSNode
                        on childCBSNode.FullID LIKE parentCBSNode.FullID + '%' AND childCBSNode.FULLID != parentCBSNode.FullID
                        where parentCBSNode.FullID LIKE '{0}%' AND parentCBSNode.FULLID!='{0}' and parentCBSNode.BudgetUnit = 'true')";
            var data = this.InstraDB.ExecuteDataTable(String.Format(sql, fullID));
            return Json(data);
        }
    }
}
