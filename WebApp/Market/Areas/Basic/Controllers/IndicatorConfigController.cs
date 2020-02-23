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
using Config;
using Config.Logic;
using MvcAdapter;
using Market.Logic;
using Market.Logic.Domain;

namespace Market.Areas.Basic.Controllers
{
    public class IndicatorConfigController : MarketController<S_KPI_IndicatorConfig>
    {
        protected override void AfterGetMode(Dictionary<string, object> entityDic, S_KPI_IndicatorConfig entity, bool isNew)
        {
            if (isNew)
            {
                entityDic.SetValue("DataSource", ConnEnum.Market.ToString());
                entityDic.SetValue("YearField", "BelongYear");
                entityDic.SetValue("QuarterField", "BelongQuarter");
                entityDic.SetValue("MonthField", "BelongMonth");
            }
        }

        protected override void BeforeSave(S_KPI_IndicatorConfig entity, bool isNew)
        {
            if (isNew)
                entity.State = YesOrNo.Yes.ToString();
        }

        public JsonResult Publish(string ListData)
        {
            var list = JsonHelper.ToList(ListData);
            foreach (var item in list)
            {
               
                var id =  item.GetValue("ID");
                var kpiConfig = this.entities.Set<S_KPI_IndicatorConfig>().FirstOrDefault(d => d.ID == id);
                kpiConfig.Publish();
            }
            this.entities.SaveChanges();
            return Json("");
        }
    }
}
