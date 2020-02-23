using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Data;
using System.Data.Entity;
using System.Collections;
using System.Text;
using Formula;
using Formula.Helper;
using Formula.Exceptions;
using MvcAdapter;
using Market.Logic;
using Market.Logic.Domain;
using Config;
using Config.Logic;

namespace Market.Areas.Basic.Controllers
{
    public class BondController : MarketFormContorllor<S_B_Bond>
    {
        public JsonResult SaveBond(string data)
        {
            var dic = JsonHelper.ToObject(data);
            var bond = BusinessEntities.Set<S_B_Bond>().Find(dic.GetValue("Id"));
            if (bond == null)
                throw new BusinessException("id为【" + dic.GetValue("Id") + "】的S_B_Bond为空");

            bond.State = "Settle";
            decimal returnAmount = 0;
            decimal.TryParse(dic.GetValue("ReturnAmount"), out returnAmount);
            decimal performanceAmount = 0;
            decimal.TryParse(dic.GetValue("PerformanceAmount"), out performanceAmount);

            if (returnAmount != 0)
            {
                var tmpBond = bond.Clone<S_B_Bond>();
                tmpBond.ID = FormulaHelper.CreateGuid();
                tmpBond.SerialNumber += "-R";
                tmpBond.Amount = returnAmount;
                tmpBond.State = "Return";
                tmpBond.BondType = "Bid";
                BusinessEntities.Set<S_B_Bond>().Add(tmpBond);
            }

            if (performanceAmount != 0)
            {
                var tmpBond = bond.Clone<S_B_Bond>();
                tmpBond.ID = FormulaHelper.CreateGuid();
                tmpBond.SerialNumber += "-P";
                tmpBond.Amount = performanceAmount;
                tmpBond.State = "Return";
                tmpBond.BondType = "Performance";
                BusinessEntities.Set<S_B_Bond>().Add(tmpBond);
            }

            BusinessEntities.SaveChanges();
            return Json("1");
        }

        protected override void BeforeDelete(string[] Ids)
        {
            base.BeforeDelete(Ids);
            foreach (var id in Ids)
            {
                Expenses.Logic.BusinessFacade.DataInterfaceFo.ValidateDataSyn("S_B_Bond", id);

                var bond = BusinessEntities.Set<S_B_Bond>().Find(id);
                if(bond != null && bond.State != "NoReturn")
                {
                    throw new BusinessException("保证金已经归还或者结清,不能删除");
                }
            }
        }

        protected override void BeforeSave(Dictionary<string, string> dic, Base.Logic.Domain.S_UI_Form formInfo, bool isNew)
        {
            base.BeforeSave(dic, formInfo, isNew);
            if (!isNew)
            {
                Expenses.Logic.BusinessFacade.DataInterfaceFo.ValidateDataSyn("S_B_Bond", dic.GetValue("ID"));

                var bond = BusinessEntities.Set<S_B_Bond>().Find(dic.GetValue("ID"));
                if (bond != null && bond.State == "Settle")
                {
                    throw new BusinessException("保证金已经归还或者结清,不能编辑");
                }
            }
        }
    }
}
