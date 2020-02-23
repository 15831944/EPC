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
    public class SubInvoiceConfirmRevertController : ExpensesFormController<S_EP_InvoiceConfirmRevert>
    {
        protected override void BeforeSave(Dictionary<string, string> dic, S_UI_Form formInfo, bool isNew)
        {
            ValidateConfirmNode(dic.GetValue("InvoiceID"), dic.GetValue("InvoiceConfirmID"));
        }

        protected override void OnFlowEnd(S_EP_InvoiceConfirmRevert data, S_WF_InsTaskExec taskExec, S_WF_InsDefRouting routing)
        {
            if (data != null)
            {
                data.Push();
            }
        }

        //撤销验证
        public JsonResult ValidateRevert(string id, string nodeID)
        {
            ValidateConfirmNode(id, nodeID);
            return Json("");
        }

        private void ValidateConfirmNode(string id, string nodeID)
        {
            //判断是否是最后确认节点
            var dt = this.SQLDB.ExecuteDataTable(string.Format("select top 1 ID,CBSInfoID,ConfirmDate from S_EP_InvoiceConfirm where InvoiceID='{0}' order by ID desc ", id));
            if (dt.Rows.Count == 0)
            {
                throw new Formula.Exceptions.BusinessValidationException("无确认信息，无需撤销！");
            }
            var dicConfirm = FormulaHelper.DataRowToDic(dt.Rows[0]);
            if (nodeID != dicConfirm.GetValue("ID"))
            {
                throw new Formula.Exceptions.BusinessValidationException("只能逐步撤销节点");
            }
            CostFO.ValidatePeriodIsClosed(Convert.ToDateTime(dicConfirm.GetValue("ConfirmDate")));

            var cbsInfoDic = this.GetDataDicByID("S_EP_CBSInfo", dicConfirm.GetValue("CBSInfoID"));
            if (cbsInfoDic == null)
            {
                throw new Formula.Exceptions.BusinessValidationException("没有找到成本单元对应的核算项目，无法撤销！");
            }

        }


    }
}
