using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Config;
using Config.Logic;
using Formula.Helper;
using MvcAdapter;
using Expenses.Logic;
using Expenses.Logic.Domain;
using Formula;
using System.Data;
using Formula.Exceptions;
using Base.Logic.Domain;
using Workflow.Logic.Domain;

namespace Expenses.Areas.Cost.Controllers
{
    public class SubContractConfirmRevertController : ExpensesFormController<S_EP_SupplierContractConfirmRevert>
    {
        protected override void BeforeSave(Dictionary<string, string> dic, S_UI_Form formInfo, bool isNew)
        {
            if (isNew)
            {
                ValidateNodeRevert(dic.GetValue("SubContractDetailID"), dic.GetValue("SubContractConfirmID"));
            }
        }

        protected override void OnFlowEnd(S_EP_SupplierContractConfirmRevert data, S_WF_InsTaskExec taskExec, S_WF_InsDefRouting routing)
        {
            if (data != null)
            {
                data.Push();
            }
        }

        //撤销验证
        public JsonResult ValidateBeforeRevert(string id, string nodeID)
        {
            ValidateNodeRevert(id, nodeID);
            return Json("");
        }

        private void ValidateNodeRevert(string id, string nodeID)
        {
            #region 判断节点是否是最后确认节点，是否已确认，是否已撤销
            var sql = string.Format(@"select FinalConfirm.*,RevertInfo.FlowPhase as RevertFlowPhase, RevertInfo.ID as ConfirmRevertID
from (select top 1 ID,CBSInfoID,ConfirmDate,FlowPhase as ConfirmFlowPhase from S_EP_SupplierContractConfirm where SubContractDetailID='{0}' order by ID desc) FinalConfirm
left join S_EP_SupplierContractConfirmRevert RevertInfo on FinalConfirm.ID=RevertInfo.SubContractConfirmID ", id);
            var dt = this.SQLDB.ExecuteDataTable(sql);
            if (dt.Rows.Count == 0)
            {
                throw new Formula.Exceptions.BusinessValidationException("无确认信息，无需撤销！");
            }
            var dicConfirm = FormulaHelper.DataRowToDic(dt.Rows[0]);
            if (dicConfirm.GetValue("ConfirmFlowPhase") != "End")
            {
                throw new Formula.Exceptions.BusinessValidationException("此节点在审批中，不允许撤销！");
            }
            if (nodeID != dicConfirm.GetValue("ID"))
            {
                throw new Formula.Exceptions.BusinessValidationException("只能逐步撤销节点");
            }
            if (!string.IsNullOrWhiteSpace(dicConfirm.GetValue("ConfirmRevertID")))
            {
                throw new Formula.Exceptions.BusinessValidationException("此节点已撤销，不能重复撤销，请确认！");
            }

            #endregion

            CostFO.ValidatePeriodIsClosed(Convert.ToDateTime(dicConfirm.GetValue("ConfirmDate")));

            var cbsInfoDic = this.GetDataDicByID("S_EP_CBSInfo", dicConfirm.GetValue("CBSInfoID"));
            if (cbsInfoDic == null)
            {
                throw new Formula.Exceptions.BusinessValidationException("没有找到对应的核算项目，无法撤销！");
            }

        }



    }
}
