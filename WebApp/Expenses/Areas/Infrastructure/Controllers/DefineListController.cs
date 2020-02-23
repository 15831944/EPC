using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Config;
using Config.Logic;
using Formula;
using Formula.Helper;
using MvcAdapter;
using Expenses.Logic;
using Expenses.Logic.Domain;
using System.Data;

namespace Expenses.Areas.Infrastructure.Controllers
{
    public class DefineListController : InstrasController
    {
        protected override string TableName
        {
            get
            {
                return "S_EP_DefineCBSInfo";
            }
        }

        protected override void BeforeDelete(string[] ids)
        {
            var db = SQLHelper.CreateSqlHelper(ConnEnum.Market);
            foreach (var id in ids)
            {
               var obj =  db.ExecuteScalar(String.Format("select count(ID) from S_EP_CBSInfo where CBSDefineInfoID='{0}'",id));
               if (Convert.ToInt32(obj) > 0)
               {
                   throw new Formula.Exceptions.BusinessValidationException("已经有立项项目的模式信息，不能被删除");
               }
            }
        }

        public ActionResult TreeList()
        {
            var defineID = this.GetQueryString("DefineID");
            ViewBag.DefineID = defineID;
            return View();
        }

        public ActionResult CBSDefineEdit()
        {
            ViewBag.ContractFields = "";

            return View();
        }

        public ActionResult AutoCreateConfig()
        {
            var defineID = this.GetQueryString("ID");
            var defineDic = this.GetDataDicByID(this.TableName, defineID);
            if (defineDic.GetValue("AutoCreateMethod") == Expenses.Logic.SetCBSOpportunity.Contract.ToString())
            {
                ViewBag.DataEnum = "ContractColumn";
            }
            else if (defineDic.GetValue("AutoCreateMethod") == Expenses.Logic.SetCBSOpportunity.TaskNoticeComplete.ToString())
            {
                ViewBag.DataEnum = "ProjectColumn";
            }
            else if (defineDic.GetValue("AutoCreateMethod") == Expenses.Logic.SetCBSOpportunity.Engineering.ToString())
            {
                ViewBag.DataEnum = "EngineeringColumn";
            }
            else if (defineDic.GetValue("AutoCreateMethod") ==Expenses.Logic.SetCBSOpportunity.SchemaComplete.ToString())
            {
                ViewBag.DataEnum = "EngineeringColumn";
            }
            else if (defineDic.GetValue("AutoCreateMethod") == Expenses.Logic.SetCBSOpportunity.CBSInfoBuilder.ToString())
            {
                ViewBag.DataEnum = "BuilderColumn";
            }
            else
            {
                ViewBag.DataEnum = "";
            }
            return View();
        }

        public ActionResult CBSDefineConfig()
        {
            var defineID = this.GetQueryString("DefineID");
            var defineDic = this.GetDataDicByID(this.TableName, defineID);
            if (defineDic.GetValue("AutoCreateCBSInfo") == true.ToString().ToLower())
            {
                ViewBag.AutoBuild = true;
            }
            else
            {
                ViewBag.AutoBuild = false;
            }          
            ViewBag.DefineID = defineID;
            return View();
        }

    }
}
