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
using Config;
using Config.Logic;
using MvcAdapter;
using Market.Logic;
using Market.Logic.Domain;

namespace Market.Areas.Basic.Controllers
{
    public class ManageContractController : MarketController<S_C_ManageContract>
    {

        public ActionResult TabList()
        {
            string projectID = this.GetQueryString("ProjectID");
            var tab = new Tab();
            string sql = @"select mc.*
from S_C_ManageContract as mc
left join S_C_ManageContract_ProjectRelation as cpr
on mc.ID=cpr.S_C_ManageContractID
where cpr.ProjectID='{0}'
and IsSigned='Signed'
order by SignDate desc";

            sql = string.Format(sql, projectID);
            var dt = this.SqlHelper.ExecuteDataTable(sql);

            Category contractItem = new Category();
            contractItem.Key = "";
            contractItem.Name = "已签合同";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var dataRow = dt.Rows[i];
                var item = new CategroyItem();
                item.Name = dataRow["Name"].ToString();
                item.Value = dataRow["ID"].ToString();
                item.SortIndex = i + 1;
                contractItem.Items.Add(item);
                if (i == 0)
                    contractItem.SetDefaultItem(item.Value);
            }

            contractItem.Multi = false;
            tab.Categories.Add(contractItem);
            tab.IsDisplay = true;
            ViewBag.Tab = tab;
            return View();
        }
        public JsonResult GetInvoiceList(string ContractID, QueryBuilder qb)
        {
            var state = InvoiceState.Normal.ToString();
            var data = this.entities.Set<S_C_Invoice>().Where(d => d.ContractInfoID == ContractID && d.State
                == state).WhereToGridData(qb);
            return Json(data);
        }

        public JsonResult GetReceiptList(string ContractID, QueryBuilder qb)
        {
            var data = this.entities.Set<S_C_Receipt>().Where(d => d.ContractInfoID == ContractID).WhereToGridData(qb);
            return Json(data);
        }

        public JsonResult GetPlanReceiptList(QueryBuilder qb, string ContractID)
        {
            string sql = "select a.*,b.FactInvoiceValue from S_C_PlanReceipt a left join  S_C_ManageContract_ReceiptObj b on a.ReceiptObjectID=b.ID where a.ContractInfoID='" + ContractID + "'";
            //var data = this.entities.Set<S_C_PlanReceipt>().Where(d => d.ContractInfoID == ContractID).Where((SearchCondition)qb).
            //    OrderBy(d => d.PlanReceiptDate).ThenBy(d => d.ID).ToList();
            var data = this.SqlHelper.ExecuteGridData(sql, qb);
            return Json(data);
        }

        public ActionResult PlanReceiptList()
        {
            var tab = new Tab();
            var planStateCategory = CategoryFactory.GetCategory(typeof(PlanReceiptState), "State");
            planStateCategory.SetDefaultItem(PlanReceiptState.UnReceipt.ToString());
            tab.IsDisplay = true;
            tab.Categories.Add(planStateCategory);
            ViewBag.Tab = tab;
            return View();
        }

        public JsonResult GetContract(string id)
        {
            return Json(entities.Set<S_C_Invoice>().Find(id).S_C_ManageContract);
        }
    }
}
