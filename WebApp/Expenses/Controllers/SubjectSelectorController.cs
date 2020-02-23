using Formula;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Formula.Helper;
using Config.Logic;
using Config;

namespace Expenses.Controllers
{
    public class SubjectSelectorController : ExpensesController
    {
        protected override string TableName
        {
            get
            {
                return "S_EP_DefineSubject";
            }
        }

        public override JsonResult GetTree(MvcAdapter.QueryBuilder qb)
        {
            string sql = "select * from {0}  WITH(NOLOCK) ";
            qb.PageSize = 0;

            DataTable dtTmpl = this.InstraDB.ExecuteDataTable(String.Format("select * from {0}  WITH(NOLOCK) where 1!=1", this.TableName));
            foreach (string key in Request.QueryString.Keys)
            {
                if (string.IsNullOrEmpty(key))
                    continue;
                if ("ID,FullID,FULLID,TmplCode,IsPreView,_winid,_t".Split(',').Contains(key) || key.StartsWith("$"))
                    continue;
                if (dtTmpl.Columns.Contains(key))
                    qb.Add(key, QueryMethod.In, Request[key]); ;
            }

            this.BeforeGetTree(qb);
            var dt = this.InstraDB.ExecuteDataTable(String.Format(sql, this.TableName), qb);
            this.AfterGetTree(dt, qb);
            return Json(dt);
        }

        public JsonResult GetSubjectByReimbursementType()
        {
            string expenseType = GetQueryString("ExpenseType");

            string sql = @"select S_EP_DefineSubject.*,Parent.ID as ParentSubjectID,Parent.Name as ParentSubjectName
                         from S_EP_DefineSubject 
                         left join 
                         (
                         select S_EP_DefineSubject.ID, count(Children.ID) as ChildrenCount from S_EP_DefineSubject 
                         left join S_EP_DefineSubject Children on Children.FullID like S_EP_DefineSubject.FullID + '%' and Children.FullID != S_EP_DefineSubject.FullID
                         where S_EP_DefineSubject.ExpenseType = '{0}' 
                         group by S_EP_DefineSubject.ID
                         ) tmp on tmp.ID = S_EP_DefineSubject.ID
                         left join S_EP_DefineSubject Parent
                         on Parent.ID = S_EP_DefineSubject.ParentID
                         
                         where S_EP_DefineSubject.ExpenseType = '{0}' and tmp.ChildrenCount = 0";
            var resDT = this.InstraDB.ExecuteDataTable(string.Format(sql, expenseType));
            return Json(resDT);
        }

        public JsonResult GetCBSInfoSubjectByReimbursementType()
        {
            string expenseType = GetQueryString("ExpenseType");
            string cbsInfo = GetQueryString("CBSInfoID");

            string sql = @"select defineSubject.*,
                         cbsNode.ID as CBSNode, cbsNode.Name as CBSNodeName,cbsNode.ChargerUser,cbsNode.ChargerUserName,
                         cbsNode.ChargerDept,cbsNode.ChargerDeptName
                         from 
                         (select S_EP_DefineSubject.*,Parent.ID as ParentSubjectID,Parent.Name as ParentSubjectName,Parent.CBSNodeID as ParentCBSNodeID
                         from S_EP_DefineSubject 
                         left join 
                         (
                         select S_EP_DefineSubject.ID, count(Children.ID) as ChildrenCount from S_EP_DefineSubject 
                         left join S_EP_DefineSubject Children on Children.FullID like S_EP_DefineSubject.FullID + '%' and Children.FullID != S_EP_DefineSubject.FullID
                         where S_EP_DefineSubject.ExpenseType = '{0}' 
                         group by S_EP_DefineSubject.ID
                         ) tmp on tmp.ID = S_EP_DefineSubject.ID
                         left join (select S_EP_DefineSubject.ID, S_EP_DefineSubject.Name, S_EP_CBSNode.ID as CBSNodeID from {2}.dbo.S_EP_CBSNode inner join S_EP_DefineSubject on S_EP_DefineSubject.ID = S_EP_CBSNode.SubjectID where CBSInfoID = '{1}') Parent 
                         on Parent.ID = S_EP_DefineSubject.ParentID
                         
                         where S_EP_DefineSubject.ExpenseType = '{0}' and tmp.ChildrenCount = 0) defineSubject
                         inner join (select * from {2}.dbo.S_EP_CBSNode where CBSInfoID = '{1}') cbsNode
                         on defineSubject.ID = cbsNode.SubjectID";
            var resDT = this.InstraDB.ExecuteDataTable(string.Format(sql, expenseType, cbsInfo, this.SQLDB.DbName));
            return Json(resDT);
        }

        public JsonResult GetCBSNodeSubject(string nodeIdArr)
        {
            string sql = @"select S_EP_DefineSubject.ID as SubjectID,S_EP_DefineSubject.Code as SubjectCode,S_EP_DefineSubject.Name as SubjectName,
                         S_EP_CBSNode.BudgetValue, S_EP_CBSNode.ID as CBSNodeID, S_EP_CBSNode.Name as CBSNodeName,
                         S_EP_CBSInfo.Name as CBSInfoName,S_EP_CBSInfo.ID as CBSInfoID,
                         isnull(apply.ApplyValueTotal,0) as OtherApplyValue
                         from S_EP_CBSNode 
                         inner join {1}.dbo.S_EP_DefineSubject on S_EP_DefineSubject.ID = S_EP_CBSNode.SubjectID
                         inner join S_EP_CBSInfo on S_EP_CBSInfo.ID = S_EP_CBSNode.CBSInfoID
                         left join 
                         (select sum(S_EP_ReimbursementApply_UpSubjects.ApplyValue) ApplyValueTotal,CBSNodeID from S_EP_ReimbursementApply_UpSubjects inner join S_EP_ReimbursementApply
                         on S_EP_ReimbursementApply.ID = S_EP_ReimbursementApply_UpSubjects.S_EP_ReimbursementApplyID where S_EP_ReimbursementApply.FlowPhase = 'End'
                          group by CBSNodeID) apply
                          on apply.CBSNodeID = S_EP_CBSNode.ID
                         where S_EP_CBSNode.ID in ('{0}')";
            var resDT = this.SQLDB.ExecuteDataTable(string.Format(sql, nodeIdArr.Replace(",", "','"), this.InstraDB.DbName));
            return Json(resDT);
        }
    }
}
