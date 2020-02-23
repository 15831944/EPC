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
    public class ProductionProgressConfirmDetailAdjustController : ExpensesFormController<S_EP_ProductionProgressConfirm_DetailAdjustInfo>
    {
        protected override void BeforeSave(Dictionary<string, string> dic, S_UI_Form formInfo, bool isNew)
        {
            //调整后的本期末确认产值不能超出计划总产值
            decimal CurrentConfirmValueTotalNew = 0;
            decimal.TryParse(dic.GetValue("CurrentConfirmValueTotalNew"), out CurrentConfirmValueTotalNew);
            decimal PlanProductionValue = 0;
            decimal.TryParse(dic.GetValue("PlanProductionValue"), out PlanProductionValue);
            if (CurrentConfirmValueTotalNew > PlanProductionValue)
            {
                throw new BusinessException("调整后的本期末累计确认产值不能超出计划总产值");
            }
        }
        public JsonResult ValidateDetailAdjust(string unitID)
        {
            string sql = @"select S_EP_ProductionProgressConfirm_Detail.*,S_EP_ProductionProgressConfirm.FlowPhase from S_EP_ProductionProgressConfirm_Detail
inner join (select max(ID) MaxID,ProductionUnit from S_EP_ProductionProgressConfirm_Detail group by ProductionUnit)
maxDetail on S_EP_ProductionProgressConfirm_Detail.ID = maxDetail.MaxID left join S_EP_ProductionProgressConfirm 
on S_EP_ProductionProgressConfirm.ID = S_EP_ProductionProgressConfirm_Detail.S_EP_ProductionProgressConfirmID
where S_EP_ProductionProgressConfirm_Detail.ProductionUnit = '{0}'";
            var detailDt = this.SQLDB.ExecuteDataTable(string.Format(sql, unitID));
            if (detailDt.Rows.Count == 0)
            {
                throw new BusinessException("未做过产值确认，无法进行进度调整");
            }
            else if(detailDt.Rows[0]["FlowPhase"].ToString() != "End")
            {
                throw new BusinessException("产值确认流程还未结束，无法进行进度调整");
            }

            string adjustSql = @"select * from S_EP_ProductionProgressConfirm_DetailAdjustinfo where ProductionUnit = '" + unitID + "' order by ID desc";
            var adjustDt = this.SQLDB.ExecuteDataTable(adjustSql);
            if (adjustDt.Rows.Count > 0)
            {
                if (adjustDt.Rows[0]["FlowPhase"].ToString() != "End")
                {
                    throw new BusinessException("产值调整流程还未结束，无法进行新的调整");
                }
            }
            return Json("");
        }
        protected override void OnFlowEnd(S_EP_ProductionProgressConfirm_DetailAdjustInfo data, S_WF_InsTaskExec taskExec, S_WF_InsDefRouting routing)
        {
            if (data != null)
            {
                data.Push();
            }
        }
    }
}
