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
using DocSystem.Logic;
using DocSystem.Logic.Domain;
using Config;
using Formula.DynConditionObject;
using Config.Logic;
using System.Reflection;

namespace DocSystem.Areas.Config.Controllers
{
    public class TreeConfigController : DocConfigController<S_DOC_TreeConfig>
    {

        public JsonResult TreeConfigList()
        {
            string SpaceID = this.Request["SpaceID"];
            string sql = "select *,'树配置项' as Name from S_DOC_TreeConfig where  SpaceID='" + SpaceID + "'";
            var dt = SQLHelper.CreateSqlHelper(ConnEnum.InfrasBaseConfig).ExecuteDataTable(sql);
            return Json(dt);
        }


        public override JsonResult Save()
        {
            var entity = UpdateEntity<S_DOC_TreeConfig>();
            string ListData = this.Request["ListData"];
            var json = JsonHelper.ToJson(ListData);
            entity.TreeSort = json;
            entities.SaveChanges();
            return Json(new { ID = entity.ID,entity.TreeSort });
        }

    }
}
