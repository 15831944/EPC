using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Config.Logic;
using Formula.Helper;
using Expenses.Logic.Domain;
using Formula;
using Formula.Exceptions;

namespace Expenses.Areas.Production.Controllers
{
    public class ProductionDistributeController : ExpensesFormController<S_EP_ProductionDistribute>
    {
        public JsonResult RefreshParents(string NodeInfo)
        {
            var nodeDic = JsonHelper.ToObject(NodeInfo);
            string sql = String.Format(@"select S_EP_CBSInfo.Name as CBSInfoName, 
case when isnull(ChildCount,0)>0 then 0 else 1 end as isLeaf,
S_EP_CBSInfo.Code as CBSInfoCode,
S_EP_CBSNode.ID,S_EP_CBSNode.ParentID,S_EP_CBSNode.FullID,S_EP_CBSNode.CBSInfoID,
S_EP_CBSNode.Name,S_EP_CBSNode.Code,S_EP_CBSNode.FullCode,
S_EP_CBSNode.NodeType,
S_EP_CBSNode.CreateFormID,S_EP_CBSNode.ModifyFormID,
case when S_EP_CBSNode.CreateFormID is null or S_EP_CBSNode.CreateFormID='' then '' else '查看' end as ViewCreateForm,
case when S_EP_CBSNode.ModifyFormID is null or S_EP_CBSNode.CreateFormID='' then '' else '查看' end as ViewSplitForm,
S_EP_CBSNode.ContractValue,S_EP_CBSNode.ProductionValue,
isnull(S_EP_CBSNode.ProductionValue,0)-isnull(CommunalValue,0) as ThisProductionValue,
isnull(SplitProductionValue,0) as SplitProductionValueInfo,
isnull(S_EP_CBSNode.ProductionValue,0)-isnull(CommunalValue,0)-isnull(SplitProductionValue,0) as RemainSplitValue,
S_EP_CBSNode.ProductionSettleValue,
isnull(CommunalValue,0) as CommunalValue,
isnull(S_EP_CBSNode.ProductionValue,0)-isnull(S_EP_CBSNode.ProductionSettleValue,0) as RemainProductionValue,
case when isnull(S_EP_CBSNode.ProductionValue,0)-isnull(S_EP_CBSNode.ProductionSettleValue,0)>0 then 'UnFinish'
else 'Finish' end as SettleState,
S_EP_CBSNode.ChargerDept,S_EP_CBSNode.ChargerDeptName,
S_EP_CBSNode.ChargerUser,S_EP_CBSNode.ChargerUserName,S_EP_CBSNode.DefineID,S_EP_CBSNode.SortIndex,
S_EP_CBSNode.ContractInfoID,S_EP_CBSNode.EngineeringInfoID,S_EP_CBSNode.ProjectInfoID,
case when S_EP_ProductionUnit.ID is not null then 'true' else 'false' end IsProductionUnit,
isnull(ChangeLog.AdjustCount,0) PAdjustCount  --计划产值调整次数
from S_EP_CBSNode with(nolock)
left hash join S_EP_CBSInfo with(nolock) on S_EP_CBSNode.CBSInfoID=S_EP_CBSInfo.ID
left hash join (select Sum(ProductionValue) as CommunalValue,ParentID from S_EP_CBSNode where NodeType='Communal'
group by ParentID) CommuanlTable on CommuanlTable.ParentID=S_EP_CBSNode.ID
left hash join (select Sum(ProductionValue) as SplitProductionValue,ParentID from S_EP_CBSNode where NodeType<>'Communal' and NodeType<>'Reserved' 
and ParentID is not null and ParentID<>'' group by ParentID) SplitProductionValueInfo 
on SplitProductionValueInfo.ParentID=S_EP_CBSNode.ID
left hash join S_EP_ProductionUnit on S_EP_ProductionUnit.CBSNodeID = S_EP_CBSNode.ID
left hash join (select count(*) AdjustCount,UseCBSNode from S_EP_ProductionCBSChangeLog  group by UseCBSNode) ChangeLog
on ChangeLog.UseCBSNode = S_EP_CBSNode.ID
left join (select count(ID) as ChildCount,ParentID from S_EP_CBSNode where NodeType<>'Reserved' and NodeType<>'Communal'
group by ParentID) ChildrenCount on ChildrenCount.ParentID=S_EP_CBSNode.ID
where AccountNodeType like '%Production%' and NodeType<>'Communal' and NodeType<>'Reserved'
and '{0}' like FullID+'%'", nodeDic.GetValue("FullID"));
            var dt = this.SQLDB.ExecuteDataTable(sql);
            return Json(dt);
        }

        public JsonResult ValidateDistribute(string cbsNodeID)
        {
            var cbsDic = this.GetDataDicByID("S_EP_CBSNode", cbsNodeID);
            if (cbsDic == null) { throw new Formula.Exceptions.BusinessValidationException("没有找到指定的CBS节点，无法重新分解"); }
            var cbsNode = new S_EP_CBSNode(cbsDic);
            if (cbsNode.DefineNode.ChildrenNode.Count == 0)
                throw new Formula.Exceptions.BusinessValidationException(String.Format("【{0}】已经是末级节点，不能进行分解", cbsDic.GetValue("Name")));

            var adjustRTCount = this.SQLDB.ExecuteScalar(string.Format("select count(*) from S_EP_ProductionDistribute where FlowPhase != 'End' and CBSNode = '{0}'", cbsNodeID));
            if (Convert.ToInt32(adjustRTCount) > 0)
            {
                throw new BusinessException("存在正在进行的计划产值调整审批，无法发起新的计划产值调整流程");
            }
            return Json("");
        }

        protected override void BeforeSaveDetail(Dictionary<string, string> dic, string subTableName, Dictionary<string, string> detail, List<Dictionary<string, string>> detailList, Base.Logic.Domain.S_UI_Form formInfo)
        {
            if (subTableName == "Detail")
            {
                //验证调整金额不能超过已确金额
                decimal productionValueNew = 0;
                decimal productionSelValue = 0;
                decimal.TryParse(detail.GetValue("ProductionValue"), out productionValueNew);
                decimal.TryParse(detail.GetValue("ProductionSelValue"), out productionSelValue);
                if (productionValueNew < productionSelValue)
                {
                    throw new BusinessException(string.Format("调整明细【{0}】的调整后金额不能小于已确认产值", detail.GetValue("Name")));
                }
            }
        }

        protected override void BeforeSaveSubTable(Dictionary<string, string> dic, string subTableName, List<Dictionary<string, string>> detailList, Base.Logic.Domain.S_UI_Form formInfo)
        {
            if (subTableName == "Detail")
            {
                //验证调整金额不能超过已确金额
                decimal canSplitValue = 0;
                decimal.TryParse(dic.GetValue("CanSplitValue"), out canSplitValue);
                decimal totalProductionValueNew = detailList.Sum(a =>
                {
                    decimal tmp = 0;
                    decimal.TryParse(a.GetValue("ProductionValue"), out tmp);
                    return tmp;
                });

                var remainValue = canSplitValue - totalProductionValueNew;
                if (remainValue < 0) throw new Formula.Exceptions.BusinessValidationException(String.Format("分解产值总和不能超过可分配产值总金额【{0}】,目前已超出【{1}】",canSplitValue,Math.Abs(remainValue)));
                if(remainValue>0)  throw new BusinessException(String.Format("分解的产值金额必须分配完毕，目前尚差【{0}】金额未分配", remainValue));
            }
        }

        protected override void OnFlowEnd(S_EP_ProductionDistribute data, Workflow.Logic.Domain.S_WF_InsTaskExec taskExec, Workflow.Logic.Domain.S_WF_InsDefRouting routing)
        {
            if (data != null)
            {
                data.Push();
            }
        }

    }
}
