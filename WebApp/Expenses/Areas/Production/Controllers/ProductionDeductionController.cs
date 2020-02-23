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
using Expenses.Logic;

namespace Expenses.Areas.Production.Controllers
{
    public class ProductionDeductionController : ExpensesFormController<S_EP_ProductionDeductionApply>
    {
        public JsonResult ValidateDeduction(string cbsNodeID)
        {
            var cbsDic = this.GetDataDicByID("S_EP_CBSNode", cbsNodeID);
            if (cbsDic == null) { throw new Formula.Exceptions.BusinessValidationException("没有找到指定的CBS节点，无法重新分解"); }

            var adjustRTCount = this.SQLDB.ExecuteScalar(string.Format("select count(*) from S_EP_ProductionDeductionApply where FlowPhase != 'End' and CBSNode = '{0}'", cbsNodeID));
            if (Convert.ToInt32(adjustRTCount) > 0)
            {
                throw new BusinessException("存在正在进行的补贴审批，无法发起新的产值补贴");
            }
            return Json("");
        }

        protected override void AfterGetData(System.Data.DataTable dt, bool isNew, string upperVersionID)
        {
            var cbsNodeID = dt.Rows[0]["CBSNode"] == DBNull.Value || dt.Rows[0]["CBSNode"] == null ? "" : dt.Rows[0]["CBSNode"].ToString();
            dt.Columns.Add("IsLeaf");
            dt.Columns.Add("HasReserved");
            dt.Rows[0]["IsLeaf"] = false.ToString().ToLower();
            dt.Rows[0]["HasReserved"] = false.ToString().ToLower();
            var cbsDic = this.GetDataDicByID("S_EP_CBSNode", cbsNodeID);
            if (cbsDic == null)
            {
                throw new Formula.Exceptions.BusinessValidationException("没有找到对应的CBS产值节点，无法补贴");
            }
            var cbsNode = new S_EP_CBSNode(cbsDic);
            var defineChildren = cbsNode.DefineNode.ChildrenNode.Where(c => c.ModelDic.GetValue("NodeType") != CBSNodeType.Communal.ToString()
                && c.ModelDic.GetValue("NodeType") != CBSNodeType.Reserved.ToString()).ToList();
            if (defineChildren.Count == 0)
                dt.Rows[0]["IsLeaf"] = true.ToString().ToLower();
            if (cbsNode.DefineNode.ChildrenNode.Count(c => c.ModelDic.GetValue("NodeType") == CBSNodeType.Reserved.ToString()) > 0)
            {
                dt.Rows[0]["HasReserved"] = true.ToString().ToLower();
            }
            else
            {
                dt.Rows[0]["ProductionReserveValue"] = 0;
                dt.Rows[0]["ReserveScale"] = 0;
            }
        }

        public JsonResult ValidateCBSIsLeaf(string cbsNodeID)
        {
            var result = new Dictionary<string, object>();
            result.SetValue("IsLeaf", "false");
            var cbsDic = this.GetDataDicByID("S_EP_CBSNode", cbsNodeID);
            if (cbsDic == null)
            {
                throw new Formula.Exceptions.BusinessValidationException("没有找到对应的CBS产值节点，无法补贴");
            }
            var cbsNode = new S_EP_CBSNode(cbsDic);
            var defineChildren = cbsNode.DefineNode.ChildrenNode.Where(c => c.ModelDic.GetValue("NodeType") != CBSNodeType.Communal.ToString()
                && c.ModelDic.GetValue("NodeType") != CBSNodeType.Reserved.ToString()).ToList();
            if (defineChildren.Count > 0)
                result.SetValue("IsLeaf", "true");
            return Json(result);
        }

        protected override void BeforeSave(Dictionary<string, string> dic, Base.Logic.Domain.S_UI_Form formInfo, bool isNew)
        {
            var settleProductionValue = String.IsNullOrEmpty(dic.GetValue("SettleProductionValue")) ? 0 : Convert.ToDecimal(dic.GetValue("SettleProductionValue"));
            var productionValue = String.IsNullOrEmpty(dic.GetValue("ProductionValue")) ? 0 : Convert.ToDecimal(dic.GetValue("ProductionValue"));
            if (productionValue < settleProductionValue)
            {
                throw new Formula.Exceptions.BusinessValidationException("扣减后的产值金额不能小于已确认产值金额，请重新调整");
            }
        }

        protected override void BeforeSaveDetail(Dictionary<string, string> dic, string subTableName, Dictionary<string, string> detail, List<Dictionary<string, string>> detailList, Base.Logic.Domain.S_UI_Form formInfo)
        {
            if (subTableName == "Detail")
            {
                //验证调整金额不能超过已确金额
                decimal productionValueNew = 0;
                decimal productionSelValue = 0;
                decimal.TryParse(detail.GetValue("ProductionValueNew"), out productionValueNew);
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
                if (detailList.Count > 0)
                {
                    //验证调整金额不能超过已确金额
                    decimal canSplitValue = 0;
                    decimal.TryParse(dic.GetValue("CanSplitValue"), out canSplitValue);
                    decimal totalProductionValueNew = detailList.Sum(a =>
                    {
                        decimal tmp = 0;
                        decimal.TryParse(a.GetValue("ProductionValueNew"), out tmp);
                        return tmp;
                    });

                    if (canSplitValue != totalProductionValueNew)
                    {
                        throw new BusinessException(String.Format("分解的产值金额必须分配完毕，目前尚差【{0}】金额未分配", canSplitValue - totalProductionValueNew));
                    }
                }
            }
        }

        protected override void OnFlowEnd(S_EP_ProductionDeductionApply data, Workflow.Logic.Domain.S_WF_InsTaskExec taskExec, Workflow.Logic.Domain.S_WF_InsDefRouting routing)
        {
            if (data != null)
            {
                data.Push();
            }
        }

    }
}
