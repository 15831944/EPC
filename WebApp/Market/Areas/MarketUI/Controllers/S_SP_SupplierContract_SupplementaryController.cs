using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Market.Logic.Domain;
using Market.Logic;
using Formula;
using Config;
using Config.Logic;

namespace Market.Areas.MarketUI.Controllers
{
    public class SupplierContractSupplementaryController : MarketFormContorllor<S_SP_SupplierContract_Supplementary>
    {
        protected override void BeforeSave(Dictionary<string, string> dic, Base.Logic.Domain.S_UI_Form formInfo, bool isNew)
        {
            var contractID = dic.GetValue("MainContract");
            //补充协议关联的合同必须有签约日期
            var contract = this.GetEntityByID<S_SP_SupplierContract>(contractID);
            if (contract.SignDate == null)
                throw new Formula.Exceptions.BusinessException("未签约的分包合同不能添加补充协议");
        }
        protected override void AfterSave(Dictionary<string, string> dic, Base.Logic.Domain.S_UI_Form formInfo, bool isNew)
        {
            var supContract = this.GetEntityByID<S_SP_SupplierContract>(dic.GetValue("MainContract"));
            if (supContract == null) throw new Formula.Exceptions.BusinessException("未能找到指定的分包合同");
            var sumSettle = this.BusinessEntities.Set<S_SP_SupplierContract_Supplementary>()
                .Where(a => a.MainContract == supContract.ID).Select(a => a.ContractValue).Sum();
            supContract.ContractValue = (supContract.ThisContractValue ?? 0) + (sumSettle ?? 0m);

            var entity = new S_SP_SupplierContract_Supplementary();
            this.UpdateEntity(entity, dic);
            var date = Convert.ToDateTime(entity.SignDate);
            entity.BelongQuarter = MarketHelper.GetQuarter(date);
            entity.BelongYear = date.Year;
            entity.BelongMonth = date.Month;
            this.BusinessEntities.SaveChanges();
        }

        public override JsonResult Delete()
        {
            var Ids = this.GetQueryString("ListIDs").Split(',');
            var details = this.BusinessEntities.Set<S_SP_SupplierContract_Supplementary>().Where(a => Ids.Contains(a.ID)).ToList();
            var mainIDs = details.Select(a => a.MainContract).ToList();
            foreach (var item in details)
                this.BusinessEntities.Set<S_SP_SupplierContract_Supplementary>().Remove(item);
            BusinessEntities.SaveChanges();
            var contractList = this.BusinessEntities.Set<S_SP_SupplierContract>().Where(a => mainIDs.Contains(a.ID)).ToList();
            foreach (var contract in contractList)
            {
                var sumSettle = this.BusinessEntities.Set<S_SP_SupplierContract_Supplementary>()
                    .Where(a => a.MainContract == contract.ID).Select(a => a.ContractValue).Sum();
                contract.ContractValue = (contract.ThisContractValue ?? 0) + (sumSettle ?? 0m);
            }
            BusinessEntities.SaveChanges();
            return Json("");
        }
    }
}
