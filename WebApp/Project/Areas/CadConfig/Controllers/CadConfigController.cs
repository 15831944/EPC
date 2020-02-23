using Config;
using MvcAdapter;
using Project.Logic.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Areas.CadConfig.Controllers
{
    public class CadConfigController : BaseConfigController<S_CAD_FormURL>
    {
        public ActionResult Tab()
        {
            return View();
        }

        public ActionResult URLConfig()
        {
            return View();
        }

        public override JsonResult GetList(QueryBuilder qb)
        {
            qb.PageSize = 0;
            var sql = @"select * from S_CAD_FormURL order by ID";
            var db = SQLHelper.CreateSqlHelper(ConnEnum.InfrasBaseConfig);
            var grid = db.ExecuteGridData(sql, qb);
            return Json(grid);
        }

        public override JsonResult SaveList()
        {
            var listData = Request["ListData"];
            var list = UpdateList<S_CAD_FormURL>(listData);
            foreach (var item in list)
            {
                if (string.IsNullOrEmpty(item.HttpUrl) && string.IsNullOrEmpty(item.HttpUrlInner))
                    throw new Formula.Exceptions.BusinessException((string.IsNullOrEmpty(item.ProjectModeName) ? "" : "【" + item.ProjectModeName + "】") + "【" + item.TypeName + "】未设置内网地址或外网地址");
                if (!string.IsNullOrEmpty(item.HttpUrlInner) && !item.HttpUrlInner.ToLower().StartsWith("http://") && !item.HttpUrlInner.ToLower().StartsWith("https://"))
                    throw new Formula.Exceptions.BusinessException((string.IsNullOrEmpty(item.ProjectModeName) ? "" : "【" + item.ProjectModeName + "】") + "【" + item.TypeName + "】内网地址不正确");
                if (!string.IsNullOrEmpty(item.HttpUrl) && !item.HttpUrl.ToLower().StartsWith("http://") && !item.HttpUrl.ToLower().StartsWith("https://"))
                    throw new Formula.Exceptions.BusinessException((string.IsNullOrEmpty(item.ProjectModeName) ? "" : "【" + item.ProjectModeName + "】") + "【" + item.TypeName + "】外网地址不正确");
            }
            entities.SaveChanges();
            return Json("");
        }

        public override JsonResult Delete()
        {
            var listData = Request["List"];
            var list = UpdateList<S_CAD_FormURL>(listData);
            foreach (var item in list)
            {
                entities.Set<S_CAD_FormURL>().Remove(item);
            }
            entities.SaveChanges();
            return Json("");
        }
    }
}
