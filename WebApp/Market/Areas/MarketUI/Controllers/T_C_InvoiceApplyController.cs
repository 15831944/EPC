using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Market.Logic.Domain;
using Market.Logic;
using Formula.Helper;
using Config.Logic;
using Config;

namespace Market.Areas.MarketUI.Controllers
{
    public class InvoiceApplyController : MarketFormContorllor<T_C_InvoiceApply>
    {
        protected override void BeforeSave(Dictionary<string, string> dic, Base.Logic.Domain.S_UI_Form formInfo, bool isNew)
        {
            if (isNew)
            {
                dic.SetValue("State", InvoiceApplyState.New.ToString());

                var contract = this.BusinessEntities.Set<S_C_ManageContract>().Find(dic["Contract"]);
                if (contract == null) throw new Formula.Exceptions.BusinessException("必须选择一个合同才能进行开票申请操作");
                var amount = Convert.ToDecimal(dic["Amount"]);
                //从数据库中得到申请中的金额总额
                SQLHelper sqlHeler = SQLHelper.CreateSqlHelper(ConnEnum.Market);
                decimal dec = contract.RemainInvoiceValue;
                string sql = string.Format(@"select sum(Amount) from T_C_InvoiceApply where FlowPhase!='End'and Contract='{0}'", dic["Contract"]);
                object sum = sqlHeler.ExecuteScalar(sql);
                dec -= sum == null || sum == DBNull.Value ? 0m : Convert.ToDecimal(sum);
                if (dec < amount)
                    throw new Formula.Exceptions.BusinessException("开票金额不能大于本合同剩余可开票金额【" + Math.Round(dec, 2) + "】");
            }
            var entity = this.GetEntityByID(dic["ID"]);
            if (entity == null) entity = new T_C_InvoiceApply();
            this.UpdateEntity(entity, dic);
            entity.Validate();
        }

        protected override void BeforeSaveDetail(Dictionary<string, string> dic, string subTableName, Dictionary<string, string> detail, List<Dictionary<string, string>> detailList, Base.Logic.Domain.S_UI_Form formInfo)
        {
            var remainValue = String.IsNullOrEmpty(detail["RemainInvoiceAmount"]) ? 0 : Convert.ToDecimal(detail["RemainInvoiceAmount"]);
            var amount = String.IsNullOrEmpty(detail["ApplyInvoiceAmount"]) ? 0 : Convert.ToDecimal(detail["ApplyInvoiceAmount"]);
            if (remainValue < amount)
                throw new Formula.Exceptions.BusinessException("【" + detail["PlanReceiptName"] + "】不能超过剩余开票金额");
        }

        protected override void BeforeSaveSubTable(Dictionary<string, string> dic, string subTableName, List<Dictionary<string, string>> detailList, Base.Logic.Domain.S_UI_Form formInfo)
        {
            var sumApplyInvoiceAmount = detailList.Select(a => String.IsNullOrEmpty(a.GetValue("ApplyInvoiceAmount")) ? 0m : Convert.ToDecimal(a.GetValue("ApplyInvoiceAmount"))).Sum();
            var Amount = String.IsNullOrEmpty(dic.GetValue("Amount")) ? 0m : Convert.ToDecimal(dic.GetValue("Amount"));
            if (Amount < sumApplyInvoiceAmount)
                throw new Formula.Exceptions.BusinessException("收款对应到发票的金额，不能高于开票金额");
        }

        protected override void OnFlowEnd(T_C_InvoiceApply entity, Workflow.Logic.Domain.S_WF_InsTaskExec taskExec, Workflow.Logic.Domain.S_WF_InsDefRouting routing)
        {
            entity.State = InvoiceApplyState.Wait.ToString();
            var contract = this.BusinessEntities.Set<S_C_ManageContract>().Find(entity.Contract);
            if (contract == null) throw new Formula.Exceptions.BusinessException("没有找到对应的合同信息，开票操作失败");
            entity.Submit();
            this.BusinessEntities.SaveChanges();
            contract.SummaryInvoiceData();
            this.BusinessEntities.SaveChanges();
        }

        public JsonResult InvoiceSubmit(string Ids)
        {
            var data = Ids.Split(',');
            foreach (var item in data)
            {
                var entity = this.GetEntityByID(item);
                if (entity == null)
                    throw new Formula.Exceptions.BusinessException("未能找到ID未【" + entity.ID + "】的开票申请记录，无法进行开票处理");
                entity.Submit();
            }
            this.BusinessEntities.SaveChanges();
            return Json("");
        }
    }
}
