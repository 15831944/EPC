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
    public class CostUnitSelectorController : ExpensesController
    {
        protected override string TableName
        {
            get
            {
                return "S_EP_CostUnit";
            }
        }

        public override JsonResult GetList(QueryBuilder qb)
        {
            var sql = String.Format("select * from S_EP_CBSInfo where State<>'{0}'", Expenses.Logic.ModifyState.Removed.ToString());
            var result = this.SQLDB.ExecuteGridData(sql, qb);
            return Json(result);
        }

        public JsonResult GetCostUnitList(string CBSInfoID)
        {
            var sql = String.Format(@"SELECT S_EP_CostUnit.*,ChargerUser,ChargerUserName,ChargerDept,ChargerDeptName FROM S_EP_CostUnit LEFT JOIN S_EP_CBSNode ON CBSNodeID=S_EP_CBSNode.ID 
            WHERE S_EP_CostUnit.CBSInfoID='{0}' order by CBSNodeFullID", CBSInfoID);
            var dt = this.SQLDB.ExecuteDataTable(sql);
            return Json(dt);
        }

        /// <summary>
        /// 获取cbsNode子节点中指定科目id的cbsNode节点
        /// </summary>
        /// <param name="cbsNodeID"></param>
        /// <param name="subjectID"></param>
        /// <returns></returns>
        public JsonResult GetCBSNodeSubSubjectCBSNode(string cbsNodeID, string subjectID)
        {
            var sql = @"select children.* 
                      from S_EP_CBSNode
                      left join  S_EP_CBSNode children on children.FullID like S_EP_CBSNode.FullID + '%'
                      where children.SubjectID = '{0}' and S_EP_CBSNode.ID = '{1}'";
            var data = this.SQLDB.ExecuteDataTable(string.Format(sql, subjectID, cbsNodeID));
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}