using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Market.Logic.Domain;
using Market.Logic;
using Formula.Helper;
using Config.Logic;

namespace Market.Areas.MarketUI.Controllers
{
    public class InvoiceMakeUpApplyController : MarketFormContorllor<T_C_InvoiceMakeUpApply>
    {
        protected override void BeforeSave(Dictionary<string, string> dic, Base.Logic.Domain.S_UI_Form formInfo, bool isNew)
        {
            if (isNew)
            {
                dic.SetValue("State", InvoiceApplyState.New.ToString());
            }
            var entity = this.GetEntityByID(dic["ID"]);
            if (entity == null) entity = new T_C_InvoiceMakeUpApply();
            this.UpdateEntity(entity, dic);
            entity.Validate();
        }

  
        protected override void OnFlowEnd(T_C_InvoiceMakeUpApply entity, Workflow.Logic.Domain.S_WF_InsTaskExec taskExec, Workflow.Logic.Domain.S_WF_InsDefRouting routing)
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
                //entity.Submit();
            }
            this.BusinessEntities.SaveChanges();
            return Json("");
        }
    }
}
