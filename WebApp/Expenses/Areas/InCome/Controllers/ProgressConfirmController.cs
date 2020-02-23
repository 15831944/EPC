using Expenses.Logic.Domain;
using Formula;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Config.Logic;
using Expenses.Logic;
using System.Data;
using Config;
using Formula.Helper;

namespace Expenses.Areas.InCome.Controllers
{
    public class ProgressConfirmController : ExpensesFormController<S_EP_ProgressConfirm>
    {
        protected override void AfterGetData(System.Data.DataTable dt, bool isNew, string upperVersionID)
        {
            if (isNew)
            {
                DataRow dr = dt.Rows[0];
                string unitID = GetQueryString("UnitID");
                if (!string.IsNullOrEmpty(unitID))
                {
                    string sql = @"select Name as Evidence ,ID as DefineID from {1}..S_EP_DefineProgress_ProgressNode_EvidenceDefine
                                  left join (select S_EP_IncomeUnit.ID as UnitID, MinScale,S_EP_IncomeUnit_ProgressNode.ProgressNodeDefineID,
                                  isnull(S_EP_IncomeUnit.TotalScale,0) as UnitTotalScale,
                                  NodeName,NodeCode,S_EP_IncomeUnit_ProgressNode.ID as MinNodeID
                                  from S_EP_IncomeUnit
                                  left join (select Min(TotalAllScale) as MinScale,IncomeUnitID  from S_EP_IncomeUnit_ProgressNode where State<>'Finish'
                                  group by IncomeUnitID) MinNodeInfo
                                  on S_EP_IncomeUnit.ID=MinNodeInfo.IncomeUnitID
                                  left join S_EP_IncomeUnit_ProgressNode 
                                  on S_EP_IncomeUnit_ProgressNode.TotalAllScale=MinNodeInfo.MinScale
                                  and S_EP_IncomeUnit_ProgressNode.IncomeUnitID = S_EP_IncomeUnit.ID) MinNodeInfo
                                  on {1}..S_EP_DefineProgress_ProgressNode_EvidenceDefine.ProgressNodeID = ProgressNodeDefineID
                                  where UnitID='{0}'";
                    var baseDB = SQLHelper.CreateSqlHelper(ConnEnum.InfrasBaseConfig).DbName;
                    var evidence = this.SQLDB.ExecuteDataTable(String.Format(sql, unitID,baseDB));
                    dr["Evidence"] = JsonHelper.ToJson(evidence);
                }
            }
        }
        protected override void BeforeSave(Dictionary<string, string> dic, Base.Logic.Domain.S_UI_Form formInfo, bool isNew)
        {
            string datetTimeStr = dic.GetValue("FactEndDate");
            DateTime dateTime = Convert.ToDateTime(datetTimeStr);
            CostFO.ValidatePeriodIsClosed(dateTime);
        }
        protected override void OnFlowEnd(S_EP_ProgressConfirm data, Workflow.Logic.Domain.S_WF_InsTaskExec taskExec, Workflow.Logic.Domain.S_WF_InsDefRouting routing)
        {
            if (data != null)
            {
                data.Push();
            }
        }

        public JsonResult ValidateSubmit(string UnitID)
        {
            string sql = String.Format("SELECT COUNT(ID) FROM S_EP_ProgressConfirm WHERE IncomeUnit='{0}' AND FlowPhase<>'{1}'", UnitID, "End");
            var obj = this.SQLDB.ExecuteScalar(sql);
            if (Convert.ToInt32(obj) > 0)
            {
                throw new Formula.Exceptions.BusinessValidationException("存在尚未完成审批的进度申报，请等待审批完成后再进行申报");
            }
            sql = String.Format("SELECT COUNT(ID) FROM S_EP_IncomeUnit_ProgressNode WHERE IncomeUnitID='{0}' AND State='{1}'", UnitID, "Create");
            obj = this.SQLDB.ExecuteScalar(sql);
            if (Convert.ToInt32(obj) == 0)
            {
                throw new Formula.Exceptions.BusinessValidationException("所以的节点都已经完成了，不能进行节点确认");
            }
            return Json("");
        }

        public JsonResult GetFinishProgressNode()
        {
            string unitID = GetQueryString("UnitID");
            string sql = string.Format(@"
select S_EP_IncomeUnit_Progress.Name as ProgressName,FormInfo.ID as FormID,
FormInfo.FlowPhase,S_EP_IncomeUnit_ProgressNode.ID,S_EP_IncomeUnit_ProgressNode.IncomeUnitID,
ProgressInfoID,NodeName,NodeCode,isnull(S_EP_IncomeUnit_ProgressNode.Scale,0)/100 as Scale,
isnull(S_EP_IncomeUnit_ProgressNode.TotalScale,0)/100 as TotalScale,
isnull(S_EP_IncomeUnit_ProgressNode.AllScale,0)/100 as AllScale,
isnull(S_EP_IncomeUnit_ProgressNode.TotalAllScale,0)/100 as TotalAllScale,
isnull(S_EP_IncomeUnit_ProgressNode.AllIncomeScale,0)/100 as AllIncomeScale,
ProgressNodeDefineID,IncomDefineDetailID,S_EP_IncomeUnit_ProgressNode.SortIndex,PlanEndDate,S_EP_IncomeUnit_ProgressNode.FactEndDate,
case when FormInfo.FlowPhase is not null and FormInfo.FlowPhase<>'End' then 'Processing' else State end as State ,
Status,IsIncomeNode,
S_EP_ProgressConfirmRevert.ID as RevertID
from S_EP_IncomeUnit_ProgressNode
left join S_EP_IncomeUnit_Progress on S_EP_IncomeUnit_Progress.ID=S_EP_IncomeUnit_ProgressNode.ProgressInfoID
left join (select S_EP_ProgressConfirm.* from S_EP_ProgressConfirm right join (
select Max(ID) as MaxID,CurrentProgressNode from S_EP_ProgressConfirm group by CurrentProgressNode) MaxFormInfo
on S_EP_ProgressConfirm.ID=MaxFormInfo.MaxID) FormInfo
on FormInfo.CurrentProgressNode=S_EP_IncomeUnit_ProgressNode.ID
left join S_EP_ProgressConfirmRevert on S_EP_ProgressConfirmRevert.S_EP_ProgressConfirmID = FormInfo.ID
where IncomeUnitID='{0}' order by S_EP_IncomeUnit_ProgressNode.SortIndex", unitID);
            var dt = this.SQLDB.ExecuteDataTable(sql);
            var dicList = FormulaHelper.DataTableToListDic(dt);
            return Json(dicList);
        }

        public JsonResult GetMinNodeInfo(string UnitID)
        {
            var dt = this.SQLDB.ExecuteDataTable(String.Format(@"select top 1 S_EP_IncomeUnit_ProgressNode.*,Name,Code from S_EP_IncomeUnit_ProgressNode
left join S_EP_IncomeUnit_Progress on ProgressInfoID=S_EP_IncomeUnit_Progress.ID and State<>'Finsih'
where IncomeUnitID='{0}' order by SortIndex", UnitID));
            if (dt.Rows.Count == 0)
            {
                return Json("");
            }
            else
            {
                var result = FormulaHelper.DataRowToDic(dt.Rows[0]);
                var evidence = this.InfrasDB.ExecuteDataTable(String.Format(@"select Name,ID as DefineID from S_EP_DefineProgress_ProgressNode_EvidenceDefine
where ProgressNodeID = '{0}'", result.GetValue("ProgressNodeDefineID")));
                result.SetValue("EvidenceList", evidence);
                return Json(result);
            }
        }

        public JsonResult GetEvidence(string NodeDefineID)
        {
            var evidence = this.InfrasDB.ExecuteDataTable(String.Format(@"select Name,ID as DefineID from S_EP_DefineProgress_ProgressNode_EvidenceDefine
where ProgressNodeID = '{0}'", NodeDefineID));
            return Json(evidence);
        }

        public JsonResult ValidateRevert(string NodeID)
        {
            //只能逐步撤销
            {
                string existSql = @"select count(*) from S_EP_IncomeUnit_ProgressNode where 
                                  IncomeUnitID = (select IncomeUnitID from S_EP_IncomeUnit_ProgressNode where ID = '{0}')
                                  and State = 'Finish' and SortIndex > 
                                 isnull((select SortIndex from S_EP_IncomeUnit_ProgressNode where ID = '{0}'),0)";
                var countObj = this.SQLDB.ExecuteScalar(string.Format(existSql, NodeID));
                if (countObj != null && (int)countObj > 0)
                {
                    throw new Formula.Exceptions.BusinessValidationException("只能逐步撤销节点");
                }
            }

            //如果有包含收入节点，以收入节点的收入百分比来判断是否被收入确认
            {
                string sql = String.Format("SELECT * FROM S_EP_IncomeUnit_ProgressNode WHERE ID = '{0}'", NodeID);
                var progressNodeDT = this.SQLDB.ExecuteDataTable(sql);
                if(progressNodeDT.Rows.Count == 0) throw new Formula.Exceptions.BusinessValidationException("未找到数据");
                var row = progressNodeDT.Rows[0];

                string allIncomeScaleSql = @"select min(AllIncomeScale) from S_EP_IncomeUnit_ProgressNode where IncomeUnitID = '{0}' and IsIncomeNode = 'true' and SortIndex <= {1} and SortIndex > 
                                       isnull((select top 1 isnull(S_EP_IncomeUnit_ProgressNode.SortIndex, 0) from S_EP_IncomeUnit_ProgressNode left join S_EP_ProgressConfirm 
                                       on S_EP_ProgressConfirm.CurrentProgressNode = S_EP_IncomeUnit_ProgressNode.ID
                                       where S_EP_ProgressConfirm.ID is not null and S_EP_IncomeUnit_ProgressNode.IncomeUnitID = '{0}' and SortIndex < {1}
                                       order by SortIndex desc),0)";
                var allIncomeScaleObj = this.SQLDB.ExecuteScalar(string.Format(allIncomeScaleSql, row["IncomeUnitID"].ToString(), row["SortIndex"].ToString()));
                if (allIncomeScaleObj != null)
                {
                    decimal allIncomeScale = 0;
                    decimal.TryParse(allIncomeScaleObj.ToString(), out allIncomeScale);
                    if (allIncomeScale != 0)
                    {
                        string revenueSql = @"select max(TotalScale) from S_EP_RevenueInfo_Detail
                                        inner join S_EP_RevenueInfo on S_EP_RevenueInfo.ID = S_EP_RevenueInfo_Detail.S_EP_RevenueInfoID where S_EP_RevenueInfo.State != 'Removed' and IncomeUnitID = '{0}'";
                        var revenueObj = this.SQLDB.ExecuteScalar(string.Format(revenueSql, row["IncomeUnitID"].ToString()));
                        if (revenueObj != null)
                        {
                            decimal revenue = 0;
                            decimal.TryParse(revenueObj.ToString(), out revenue);
                            if (revenue != 0 && revenue >= allIncomeScale)
                            {
                                throw new Formula.Exceptions.BusinessValidationException("申报的节点已经已经确认过收入，无法撤销");
                            }
                        }
                    }
                }
            }
            return Json("");
        }

        public JsonResult CheckPeriodIsClosed()
        {
            var dateStr = GetQueryString("date");
            DateTime date = DateTime.Now;
            if (DateTime.TryParse(dateStr, out date))
            {
                CostFO.ValidatePeriodIsClosed(date);
            }
            return Json("");
        }
    }
}
