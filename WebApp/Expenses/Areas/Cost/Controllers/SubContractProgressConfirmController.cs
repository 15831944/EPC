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



namespace Expenses.Areas.Cost.Controllers
{
    public class SubContractProgressConfirmController : ExpensesFormController<S_EP_SubContractProgressConfirm>
    {

        public JsonResult GetList(QueryBuilder qb, string DetailID)
        {
            string sql = String.Format("SELECT * FROM S_EP_SubContractProgressConfirm WHERE TaskDetailID='{0}'", DetailID);
            var data = this.SQLDB.ExecuteDataTable(sql, qb);
            return Json(data);
        }

        protected override void BeforeSave(Dictionary<string, string> dic, Base.Logic.Domain.S_UI_Form formInfo, bool isNew)
        {
            CheckSubContractConfirmMethod();
            if (String.IsNullOrEmpty(dic.GetValue("ConfirmDate")))
            {
                throw new Formula.Exceptions.BusinessValidationException("委外进度确认必须填写完工日期");
            }
            var dateTime = String.IsNullOrEmpty(dic.GetValue("ConfirmDate")) ? DateTime.Now : Convert.ToDateTime(dic.GetValue("ConfirmDate"));
            CostFO.ValidatePeriodIsClosed(dateTime);
        }

        protected override void OnFlowEnd(S_EP_SubContractProgressConfirm data, Workflow.Logic.Domain.S_WF_InsTaskExec taskExec, Workflow.Logic.Domain.S_WF_InsDefRouting routing)
        {
            if (data != null)
            {
                CheckSubContractConfirmMethod();
                data.Push();
            }
        }

        public JsonResult ValidateStart(string DetailID)
        {
            CheckSubContractConfirmMethod();

            var detailDic = this.GetDataDicByID("S_EP_SubContractTask_CostUnitDetail", DetailID);
            if (detailDic == null)
            {
                throw new Formula.Exceptions.BusinessValidationException("没有找到指定的委外任务单元，无法进行进度确认");
            }
            var subContractTaskDic = this.GetDataDicByID("S_EP_SubContractTask", detailDic.GetValue("S_EP_SubContractTaskID"));
            if (subContractTaskDic == null)
            {
                throw new Formula.Exceptions.BusinessValidationException("没有找到指定的委外任务，无法进行进度确认");

            }
            if (Convert.ToDecimal(detailDic.GetValue("SubContractProgress")) >= 1)
            {
                throw new Formula.Exceptions.BusinessValidationException("进度已经确认至100%的单元不能再进行进度确认");
            }
            var currentID = this.SQLDB.ExecuteScalar("SELECT TOP 1 ID FROM S_EP_SubContractProgressConfirm WHERE TaskDetailID='"
                + DetailID + "' AND FLOWPHASE='Start'");
            if (currentID != null && currentID != DBNull.Value && !String.IsNullOrEmpty(currentID.ToString()))
            {
                return Json(new { ID = currentID.ToString() });
            }

            if (Convert.ToInt32(this.SQLDB.ExecuteScalar("SELECT COUNT(ID) FROM S_EP_SubContractProgressConfirm WHERE TaskDetailID='"
                 + DetailID + "' AND FLOWPHASE!='End'")) > 0)
            {
                throw new Formula.Exceptions.BusinessValidationException("【" + subContractTaskDic.GetValue("Name") + "】有在审批中的进度确认表，审批完成后才能再次进行进度确认");
            }
            return Json("");
        }

        /// <summary>
        /// 验证外委成本确认方式
        /// </summary>
        private void CheckSubContractConfirmMethod()
        {
            if (SysParams.Params.GetValue("SubContractConfirmMethod") != "ProgressConfirm")//委外进度确认
            {
                throw new Formula.Exceptions.BusinessValidationException("【按进度确认】不是系统设置的外委成本确认方式！");
            }

        }

    }
}
