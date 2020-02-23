using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Config;
using Config.Logic;
using Formula.Helper;
using Formula;
using HR.Logic;
using HR.Logic.Domain;
using System.Data;
using MvcAdapter;

namespace HR.Areas.ResourcePriceInfo.Controllers
{
    public class ActualUnitPriceDefineController : HRController<S_W_DefineUserActualUnitPrice>
    {
        //public ActionResult DefineConfig()
        //{
        //    return View();
        //}
        //public JsonResult SaveDefineConfig()
        //{
        //    string id = GetQueryString("ID");
        //    string formData = GetQueryString("FormData");
        //    S_W_DefineUserActualUnitPrice define = this.GetEntityByID(id);            
        //    if (define == null)
        //    {
        //        define = new S_W_DefineUserActualUnitPrice();
        //        define.ID = FormulaHelper.CreateGuid();
        //        define.CreateDate = DateTime.Now;
        //        define.CreateUser = CurrentUserInfo.UserName;
        //        define.CreateUserID = CurrentUserInfo.UserID;
        //        define.ModifyDate = DateTime.Now;
        //        define.ModifyUser = CurrentUserInfo.UserName;
        //        define.ModifyUserID = CurrentUserInfo.UserID;
        //        entities.Set<S_W_DefineUserActualUnitPrice>().Add(define);
        //    }
        //    else
        //    {
        //        define.ModifyDate = DateTime.Now;
        //        define.ModifyUser = CurrentUserInfo.UserName;
        //        define.ModifyUserID = CurrentUserInfo.UserID;
        //    }
        //    FormulaHelper.UpdateEntity(define, JsonHelper.ToObject(formData));
        //    define.Validate();
        //    entities.SaveChanges();
        //    return Json(true);
        //}

        //public JsonResult GetDefineConfig()
        //{
        //    string id = GetQueryString("ID");
        //    var res = this.GetEntityByID(id);
        //    return Json(res);
        //}
    }
}
