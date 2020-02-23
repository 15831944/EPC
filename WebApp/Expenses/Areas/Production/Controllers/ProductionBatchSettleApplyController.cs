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
using Workflow.Logic.Domain;
using Base.Logic.Domain;
using Formula.Exceptions;

namespace Expenses.Areas.Production.Controllers
{
    public class ProductionBatchSettleApplyController : ExpensesFormController<S_EP_ProductionBatchSettleApply>
    {
        protected override void BeforeSaveSubTable(Dictionary<string, string> dic, string subTableName, List<Dictionary<string, string>> detailList, S_UI_Form formInfo)
        {
            if (subTableName == "Detail")
            {
                if (detailList.Count == 0)
                {
                    throw new Formula.Exceptions.BusinessValidationException("产值明细不能为空！");
                }
            }
        }

        protected override void BeforeSaveDetail(Dictionary<string, string> dic, string subTableName, Dictionary<string, string> detail, List<Dictionary<string, string>> detailList, S_UI_Form formInfo)
        {
            if (subTableName == "Detail")
            {
                decimal CurrentConfirmValueTotal = 0;
                decimal.TryParse(detail.GetValue("CurrentConfirmValueTotal"), out CurrentConfirmValueTotal);
                decimal PlanProductionValue = 0;
                decimal.TryParse(detail.GetValue("PlanProductionValue"), out PlanProductionValue);
                if (CurrentConfirmValueTotal > PlanProductionValue)
                {
                    throw new BusinessException(string.Format("名称【{0}】,编号【{1}】的项目本期末累计确认产值不能超出计划产值。",
                        detail.GetValue("CBSInfoName"), detail.GetValue("CBSInfoCode")));
                }
            }
        }

        protected override void OnFlowEnd(S_EP_ProductionBatchSettleApply data, S_WF_InsTaskExec taskExec, S_WF_InsDefRouting routing)
        {
            if (data != null)
            {
                data.Push();
            }
        }

        public JsonResult GetProductionSettleList(string CBSNodeID)
        {
            var sql = String.Format(@"select * from (select ID,ProductionValue as PlanValue,ProgressNodeName as NodeName,CurrentSettleValue as SettleValue,
SettleDate,ApplyUserName,CBSNode,'ProductionSettleApply' as FormCode,'ProductionSettleApply' as FlowCode from S_EP_ProductionSettleApply
union select S_EP_ProductionBatchSettleApply.ID,PlanProductionValue as PlanValue,CurrentConfirmNodeName as NodeName,CurrentConfirmValue as SettleValue,
ConfirmDate as SettleDate,ApplyUserName,CBSNodeID as CBSNode,'ProductionBatchSettleApply' as FormCode,'FormCode' as FlowCode
from S_EP_ProductionBatchSettleApply_Detail
left join S_EP_ProductionBatchSettleApply on S_EP_ProductionBatchSettleApplyID=S_EP_ProductionBatchSettleApply.ID where State='Normal') tableInfo where CBSNode='{0}'", CBSNodeID);
            var dt = this.SQLDB.ExecuteDataTable(sql);           
            return Json(new { SettleData = JsonHelper.ToJson(dt), AddData = "" });
        }

        public JsonResult GetBatchProductionUnitInfo(string ListData, string ViewType)
        {
            var list = JsonHelper.ToList(ListData);
            var nodeIds = list.Select(c => c.GetValue("ID")).Distinct().ToList();
            if (ViewType == "ProductionUnit")
                nodeIds = list.Select(c => c.GetValue("CBSNodeID")).Distinct().ToList();
            string sql = String.Format(@"select S_EP_ProductionBatchSettleApply_Detail.* from S_EP_ProductionBatchSettleApply_Detail
left join S_EP_ProductionBatchSettleApply on S_EP_ProductionBatchSettleApplyID= S_EP_ProductionBatchSettleApply.ID
where S_EP_ProductionBatchSettleApply.FlowPhase<>'End' and CBSNodeID in ('{0}')", String.Join("','", nodeIds));
            var dt = this.SQLDB.ExecuteDataTable(sql);
            if (dt.Rows.Count > 0)
            {
                throw new Formula.Exceptions.BusinessValidationException(String.Format("项目【{0}】下【{1}】存在审批中的产值确认申请，请等待审批完成后再进行确认",
                    dt.Rows[0]["CBSInfoName"], dt.Rows[0]["ProductionUnitName"]));
            }

            sql = String.Format(@"select S_EP_CBSNode.ID,S_EP_CBSNode.Name,S_EP_CBSNode.Code,S_EP_CBSInfo.Name as CBSInfoName,
S_EP_ProductionUnit.ID as UnitID from S_EP_CBSNode left join S_EP_ProductionUnit on S_EP_CBSNode.ID=S_EP_ProductionUnit.CBSNodeID
left join S_EP_CBSInfo on S_EP_CBSInfo.ID=S_EP_CBSNode.CBSInfoID
where S_EP_ProductionUnit.ID is null and S_EP_CBSNode.ID in ('{0}')", String.Join("','", nodeIds));
            dt = this.SQLDB.ExecuteDataTable(sql);
            if (dt.Rows.Count > 0)
            {
                throw new Formula.Exceptions.BusinessValidationException(String.Format("项目【{0}】下【{1}】不是产值单元，请重新选择",
                  dt.Rows[0]["CBSInfoName"], dt.Rows[0]["Name"]));
            }

            sql = String.Format(@"select S_EP_CBSInfo.ID as CBSInfo,S_EP_CBSInfo.Name as CBSInfoName,S_EP_CBSInfo.Code as CBSInfoCode,
S_EP_ProductionUnit.ID as ProductionUnit,CBSNodeID,
S_EP_ProductionUnit.Name as ProductionUnitName,
S_EP_ProductionUnit.Code as ProductionCode,S_EP_ProductionUnit.ProductionValue as PlanProductionValue,
PrgNodeInfo.ID as CurrentConfirmNode,PrgNodeInfo.NodeName as CurrentConfirmNodeName,
S_EP_ProductionUnit.ProductionSettleValue as LastConfirmedValueTotal,
case when isnull(S_EP_ProductionUnit.ProductionValue,0)=0 then 0 else
isnull(S_EP_ProductionUnit.ProductionSettleValue,0)/S_EP_ProductionUnit.ProductionValue*100 end as LastConfirmedScaleTotal,
isnull(PrgNodeInfo.TotalScale,0) as CurrentConfirmScaleTotal,
isnull(PrgNodeInfo.TotalScale,0) *isnull(S_EP_ProductionUnit.ProductionValue,0)/100 as CurrentConfirmValueTotal,
isnull(PrgNodeInfo.TotalScale,0) *isnull(S_EP_ProductionUnit.ProductionValue,0)/100 - S_EP_ProductionUnit.ProductionSettleValue as CurrentConfirmValue,
getDate() as ConfirmDate
from S_EP_ProductionUnit
left join S_EP_CBSInfo on S_EP_ProductionUnit.CBSInfoID=S_EP_CBSInfo.ID
left join (select S_EP_ProductionUnit_ProgressNode.* from (
select Min(SortIndex) minSortIndex,ProductionUnitID from S_EP_ProductionUnit_ProgressNode
where State<>'Finish'
group by ProductionUnitID) MinProgressNodeInfo
inner join S_EP_ProductionUnit_ProgressNode on S_EP_ProductionUnit_ProgressNode.ProductionUnitID=MinProgressNodeInfo.ProductionUnitID
and S_EP_ProductionUnit_ProgressNode.SortIndex=minSortIndex) PrgNodeInfo
on PrgNodeInfo.ProductionUnitID= S_EP_ProductionUnit.ID
where CBSNodeID in ('{0}') ", String.Join("','", nodeIds));
            var res = this.SQLDB.ExecuteDataTable(sql);
            return Json(res);
        }

    }
}
