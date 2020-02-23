using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Config.Logic;
using Formula.Helper;
using Expenses.Logic;
using Expenses.Logic.Domain;
using Formula;
using System.Data;

namespace Expenses.Areas.Cost.Controllers
{
    public class ReimbursementSubmitController : ExpensesFormController<S_EP_ReimbursementApply>
    {
        protected override void BeforeSave(Dictionary<string, string> dic, Base.Logic.Domain.S_UI_Form formInfo, bool isNew)
        {
            if (!isNew)
            {
                Expenses.Logic.BusinessFacade.DataInterfaceFo.ValidateDataSyn("S_EP_ReimbursementApply", dic.GetValue("ID"));
            }
            CostFO.ValidatePeriodIsClosed(DateTime.Now);
        }

        protected override void OnFlowEnd(S_EP_ReimbursementApply data, Workflow.Logic.Domain.S_WF_InsTaskExec taskExec, Workflow.Logic.Domain.S_WF_InsDefRouting routing)
        {
            if (data != null)
            {
                data.Push();
                data.DoRepay();
            }
        }

        protected override void BeforeDelete(string[] Ids)
        {
            foreach (var Id in Ids)
            {
                Expenses.Logic.BusinessFacade.DataInterfaceFo.ValidateDataSyn("S_EP_ReimbursementApply", Id);
            }
        }

        public JsonResult GetSubjectListWithOutProject(string ExpenseType)
        {
            var sql = String.Format(@"select * from (
select S_EP_DefineSubject.ID as value,
S_EP_DefineSubject.Name as text,
 isnull(ChildCount,0) as ChildCount, S_EP_DefineSubject.ID,S_EP_DefineSubject.Name,
ParentInfo.ID as ParentID,ParentInfo.Name as ParentName,ParentInfo.Code as ParentCode
from S_EP_DefineSubject
left join (select Count(ID) as ChildCount,ParentID from S_EP_DefineSubject
group by ParentID) ChildTableInfo on S_EP_DefineSubject.ID=ChildTableInfo.ParentID
left join S_EP_DefineSubject as ParentInfo on ParentInfo.ID=S_EP_DefineSubject.ParentID
where S_EP_DefineSubject.ExpenseType='{0}') TableInfo
where ChildCount=0", ExpenseType);
            var dt = this.InfrasDB.ExecuteDataTable(sql);
            var result = new Dictionary<string, object>();
            result.SetValue("SubjectList", dt);
            return Json(result);
        }

        public JsonResult GetSourceData(string CostUnitList, string ExpenseType)
        {
            var list = JsonHelper.ToList(CostUnitList);
            var ids = String.Join(",", list.Select(c => c.GetValue("ID")).ToList());
            //S_EP_CBSNode.ID ParentID, 默认报销内容节点也作为其科目节点 
            //S_EP_CBSNode.Name ParentName,
            //S_EP_CBSNode.Code ParentCode,
            //S_EP_CBSNode.BudgetValue BudgetValue,
            //S_EP_CBSNode.CostValue CostValue,
            string sql = String.Format(@"select S_EP_CBSNode.ID as value,S_EP_CBSNode.Name as text,S_EP_CBSNode.Code as code,
S_EP_CostUnit.ID as UnitID,S_EP_CostUnit.Name as UnitName,S_EP_CBSNode.FullID,
case when (S_EP_CBSNode.BudgetValue is null or S_EP_CBSNode.BudgetValue = 0) then 'true'
else 'false' end UpSubjectNodeBudgetZero,
ParentCBSNode.ID ParentID,
ParentCBSNode.Name ParentName,
ParentCBSNode.Code ParentCode,
S_EP_CBSNode.BudgetValue BudgetValue,
S_EP_CBSNode.CostValue CostValue,
isnull(S_EP_CBSNode.BudgetValue,0)-isnull(S_EP_CBSNode.CostValue,0) BudgetValueRemain,
S_EP_CostUnit.CBSInfoID as CBSInfoID,
S_EP_CBSNode.Sortindex from S_EP_CostUnit inner join S_EP_CBSNode on S_EP_CBSNode.FullID  like S_EP_CostUnit.CBSNodeFullID+'%'
inner join (select ID,Code,ChildCount,Name from (
select isnull(ChildCount,0) as ChildCount, S_EP_DefineSubject.* from {2}.dbo.S_EP_DefineSubject
left join (SELECT COUNT(ID) as ChildCount,ParentID FROM {2}.dbo.S_EP_DefineSubject 
WHERE ParentID is not null and ParentID <>''
GROUP BY ParentID) ChildCountInfo on S_EP_DefineSubject.ID=ChildCountInfo.ParentID) SubjectInfo 
 where  ExpenseType='{0}' ) TableInfo on SubjectID=TableInfo.ID
left join S_EP_CBSNode ParentCBSNode on ParentCBSNode.ID = S_EP_CBSNode.ParentID
where S_EP_CBSNode.SubjectID is not null
and ChildCount=0 and S_EP_CostUnit.ID in ('{1}')", ExpenseType, ids.Replace(",", "','"), this.InfrasDB.DbName);
            var dt = this.SQLDB.ExecuteDataTable(sql);

            foreach (DataRow dr in dt.Rows)
            {
                string upParentSql = string.Format(@"select * from S_EP_CBSNode ParentInfo where '{0}' like ParentInfo.FullID + '%' order by len(ParentInfo.FullID) desc", dr["FullID"]);
                var parentDt = this.SQLDB.ExecuteDataTable(upParentSql);
                foreach (DataRow drr in parentDt.Rows)
                {
                    decimal budgetValue = 0;
                    decimal.TryParse(drr["BudgetValue"].ToString(), out budgetValue);
                    //当父级节点有做过预算的情况下，报销内容的父节点作为其科目节点
                    if(budgetValue > 0)
                    {
                        dr["UpSubjectNodeBudgetZero"] = "false";
                        dr["ParentID"] = drr["ID"];
                        dr["ParentName"] = drr["Name"];
                        dr["ParentCode"] = drr["Code"];
                        dr["BudgetValue"] = drr["BudgetValue"];
                        dr["CostValue"] = drr["CostValue"];
                        decimal costValue = 0;
                        decimal.TryParse(drr["CostValue"].ToString(), out costValue);
                        dr["BudgetValueRemain"] = budgetValue - costValue;
                        break;
                    }
                }
            }

            var unitCostSQL = String.Format(@" select S_EP_CostUnit.ID as value,S_EP_CostUnit.Name as text,S_EP_CostUnit.ID,S_EP_CBSNode.ChargerDept,
S_EP_CostUnit.CBSNodeID,S_EP_CostUnit.CBSInfoID,S_EP_CostUnit.Code,
S_EP_CBSNode.ChargerDeptName,S_EP_CBSNode.ChargerUser,S_EP_CBSNode.ChargerUserName
from S_EP_CostUnit left join S_EP_CBSNode on S_EP_CostUnit.CBSNodeID=S_EP_CBSNode.ID where S_EP_CostUnit.ID in ('{0}')", ids.Replace(",", "','"));
            var costUnitDt = this.SQLDB.ExecuteDataTable(unitCostSQL);
            var result = new Dictionary<string, object>();
            result.SetValue("SubjectList", dt);
            result.SetValue("CostUnitList", costUnitDt);
            return Json(result);
        }

        public JsonResult InputTaxTransfer(string data)
        {
            var dicList = JsonHelper.ToList(data);
            var idArr = dicList.Select(a => a.GetValue("ID"));
            string sql = "";
            foreach (var dic in dicList)
            {
                dic.SetValue("S_EP_ReimbursementApply_DetailID", dic.GetValue("ID"));
                dic.SetValue("CreateUser", CurrentUserInfo.UserName);
                dic.SetValue("CreateUserID", CurrentUserInfo.UserID);
                dic.SetValue("ModifyUser", CurrentUserInfo.UserName);
                dic.SetValue("ModifyUserID", CurrentUserInfo.UserID);
                dic.SetValue("CreateDate", DateTime.Now);
                dic.SetValue("ModifyDate", DateTime.Now);
                sql += dic.CreateInsertSql(this.SQLDB, "S_EP_InputTaxTransfer", FormulaHelper.CreateGuid());
            }
            this.SQLDB.ExecuteNonQuery(sql);
            return Json(true);
        }

        public JsonResult GetSubjectListWithProject(string costUnitID)
        {
            var sql = String.Format(@"select SubjectDefine.ID as value,SubjectDefine.Name as text,
SubjectDefine.Code SubjectCode,SubjectDefine.ExpenseType,S_EP_CBSNode.Code,
S_EP_CBSNode.ChargerUser,S_EP_CBSNode.ChargerUserName,
S_EP_CBSNode.ChargerDept,S_EP_CBSNode.ChargerDeptName,S_EP_CBSNode.ID CBSNodeID,
S_EP_CBSNode.FullCode,S_EP_CBSNode.FullID
from S_EP_CostUnit inner join S_EP_CBSNode on S_EP_CBSNode.FullID  like S_EP_CostUnit.CBSNodeFullID+'%'
inner join (select DefineSubject.* from {1}..S_EP_DefineSubject DefineSubject
left join (
select {1}..S_EP_DefineSubject.ID, count(Children.ID) as ChildrenCount from {1}..S_EP_DefineSubject 
left join {1}..S_EP_DefineSubject Children on Children.FullID like {1}..S_EP_DefineSubject.FullID + '%' and Children.FullID != {1}..S_EP_DefineSubject.FullID
group by {1}..S_EP_DefineSubject.ID
) tmp on tmp.ID = DefineSubject.ID
left join {1}..S_EP_DefineSubject Parent on Parent.ID = DefineSubject.ParentID  where tmp.ChildrenCount = 0) SubjectDefine
on SubjectDefine.ID = S_EP_CBSNode.SubjectID
where S_EP_CostUnit.ID = '{0}'", costUnitID, InfrasDB.DbName);
            var dt = this.SQLDB.ExecuteDataTable(sql);
            return Json(dt, JsonRequestBehavior.AllowGet);
        }
    }
}
