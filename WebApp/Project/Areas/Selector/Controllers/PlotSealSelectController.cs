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
using MvcAdapter;
using Project.Logic;
using Project.Logic.Domain;
using Config;
using Config.Logic;

namespace Project.Areas.Selector.Controllers
{
    public class PlotSealSelectController : ProjectController
    {
        public ActionResult PlotSealSelector()
        {
            var PlotSealType = EnumBaseHelper.GetEnumDef("Project.PlotSealType").EnumItem;
            var list = new List<object>();
            list.AddRange(PlotSealType.Select(a => new { text = a.Name, value = a.Code }));
            list.Add(new { text = "组合章", value = "Group" });
            ViewBag.PlotSealType = "var PlotSealType = " + JsonHelper.ToJson(list);
            return View();
        }

        public JsonResult GetSealList(QueryBuilder qb)
        {
            var sql = @"select 'Group' as Type,g.Name,g.Code,g.ID,gg.BlockKey,'主章：'+gg.Name+'，从章：'+isnull((stuff((select '、' + Name from S_EP_PlotSealGroup_GroupInfo where S_EP_PlotSealGroupID = g.ID and IsMain <> 'true'  for xml path('')), 1, 1, '')),'无') Remark
from S_EP_PlotSealGroup g
left join S_EP_PlotSealGroup_GroupInfo gg on g.ID = gg.S_EP_PlotSealGroupID and gg.IsMain = 'true'
union all
select Type,Name,Code,ID,BlockKey,Remark from S_EP_PlotSealInfo";
            var list = this.SqlHelper.ExecuteGridData(sql, qb);
            return Json(list);
        }
    }
}
