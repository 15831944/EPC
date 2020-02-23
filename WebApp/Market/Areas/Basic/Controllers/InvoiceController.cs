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
    public class InvoiceController : MarketController<S_C_Invoice>
    {
        public ActionResult InvoiceEdit()
        {
            //发票的最大金额
            StringBuilder sb = new StringBuilder();
            if (string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["Invoice_MaxAmount"]) == false)
                sb.AppendFormat("\n\n var Invoice_MaxAmount='{0}';", System.Configuration.ConfigurationManager.AppSettings["Invoice_MaxAmount"]);
            else
                sb.AppendFormat("\n\n var Invoice_MaxAmount='{0}';", 0);
            ViewBag.Scripts = sb.ToString();
            return View();
        }

        public ActionResult InvoiceList()
        {
            var tab = new Tab();
            var invoiceTypeCategory = CategoryFactory.GetCategory("Market.InvoiceType", "行业", "InvoiceType");
            invoiceTypeCategory.SetDefaultItem("All");
            tab.Categories.Add(invoiceTypeCategory);

            var stateCategory = CategoryFactory.GetCategory(typeof(InvoiceState), "State");
            stateCategory.Multi = false;
            stateCategory.SetDefaultItem(InvoiceState.Normal.ToString());
            tab.Categories.Add(stateCategory);

            var yearCategory = CategoryFactory.GetYearCategory("BelongYear");
            yearCategory.SetDefaultItem(DateTime.Now.Year.ToString());
            tab.Categories.Add(yearCategory);

            tab.IsDisplay = true;
            ViewBag.Tab = tab;
            return View();
        }

        public override JsonResult GetList(QueryBuilder qb)
        {
            string sql = @"select S_C_Invoice.*,RelationValue from S_C_Invoice
left join (select Sum(RelationValue) as RelationValue,InvoiceID from dbo.S_C_InvoiceReceiptRelation
group by InvoiceID) RelationInfo on S_C_Invoice.ID = RelationInfo.InvoiceID";
            var data = this.SqlHelper.ExecuteGridData(sql, qb);
            string sumSql = "select isnull(Sum(Amount),0) as SumAmount,isnull(Sum(RelationValue),0) as SumRelationValue from (" + sql + qb.GetWhereString() + ") SumDataInfo";
            var sumDt = this.SqlHelper.ExecuteDataTable(sumSql);
            if (sumDt != null && sumDt.Rows.Count > 0)
            {
                data.sumData.SetValue("Amount", Convert.ToDecimal(sumDt.Rows[0]["SumAmount"]).ToString("c"));
                data.sumData.SetValue("RelationValue", Convert.ToDecimal(sumDt.Rows[0]["SumRelationValue"]).ToString("c"));
            }
            return Json(data);
        }

        public JsonResult Invalid(string invoiceList)
        {
            var list = JsonHelper.ToList(invoiceList);
            foreach (var item in list)
            {
                var invoice = this.GetEntityByID(item.GetValue("ID"));
                if (invoice == null)
                    throw new Formula.Exceptions.BusinessException("未能找到ID为【" + item.GetValue("ID") + "】的发票信息，无法进行作废操作");
                invoice.Invalid();
            }
            entities.SaveChanges();
            return Json("");
        }

        protected override void AfterGetMode(Dictionary<string, object> entityDic, S_C_Invoice entity, bool isNew)
        {
            if (isNew)
            {
                entityDic.SetValue("InvoiceDate", DateTime.Now);
                entityDic.SetValue("InvoiceMaker", this.CurrentUserInfo.UserName);
                entityDic.SetValue("InvoiceMakerID", this.CurrentUserInfo.UserID);
            }
            else
            {
                var contract = this.GetEntityByID<S_C_ManageContract>(entityDic.GetValue("ContractInfoID"));
                entityDic.SetValue("ContractValue", contract.ContractRMBAmount);
                var invoiceID = entityDic.GetValue("ID");
                var contractInvoiceValue = Convert.ToDecimal(contract.S_C_Invoice.Where(d => d.ID != invoiceID).Sum(d => d.Amount));
                entityDic.SetValue("ContractInvoiceValue", contractInvoiceValue);
                var remainInvoiceValue = Convert.ToDecimal(contract.ContractRMBAmount) - contractInvoiceValue;
                if (remainInvoiceValue < 0) remainInvoiceValue = 0;
                entityDic.SetValue("RemainInvoiceValue", remainInvoiceValue);
            }
        }

        protected override void BeforeSave(S_C_Invoice entity, bool isNew)
        {
            entity.ClearPlanRelation();
            string relationData = this.Request["RelationData"];
            var sumRelation = 0M;
            if (!String.IsNullOrEmpty(relationData))
            {
                var planRelations = JsonHelper.ToList(relationData);
                foreach (var item in planRelations)
                {
                    var relationValue = 0M;
                    if (String.IsNullOrEmpty(item.GetValue("RelationValue")))
                        throw new Formula.Exceptions.BusinessException("未填写计划收款划分金额，无法将发票拆分关联至计划收款");
                    relationValue = Convert.ToDecimal(item.GetValue("RelationValue"));
                    sumRelation += relationValue;
                    entity.RelateToReceiptObject(item.GetValue("ReceiptObjectID"), relationValue);
                }
            }
            if (sumRelation > entity.Amount) throw new Formula.Exceptions.BusinessException("关联的收款项金额不能大于开票金额");
            string InvoiceDatailData = this.Request["InvoiceDatailData"];
            entity.S_C_Invoice_InvoiceDatail.Clear();
            if (!String.IsNullOrEmpty(InvoiceDatailData))
            {
                var InvoiceDatailDatas = JsonHelper.ToList(InvoiceDatailData);
                var i = 0;
                foreach (var item in InvoiceDatailDatas)
                {
                    var MarketEntities = FormulaHelper.GetEntities<MarketEntities>();
                    var id = item.GetValue("ID");
                    var detail = MarketEntities.Set<S_C_Invoice_InvoiceDatail>().FirstOrDefault(a => a.ID == id);
                    if (detail == null)
                    {
                        detail = new S_C_Invoice_InvoiceDatail();
                        detail.ID = FormulaHelper.CreateGuid();
                        detail.SortIndex = i++;
                    }
                    detail.Code = item.GetValue("Code");
                    detail.InvoiceAmount = decimal.Parse(item.GetValue("InvoiceAmount"));
                    detail.InvoiceState = item.GetValue("InvoiceState");
                    entity.S_C_Invoice_InvoiceDatail.Add(detail);
                }
            }
            entity.Save();
        }

        protected override void BeforeDelete(List<S_C_Invoice> entityList)
        {
            foreach (var item in entityList)
                item.Delete();
        }

        public JsonResult GetPlanRelationList(string ID)
        {
            string sql = @"select S_C_Invoice_ReceiptObject.*,S_C_ManageContract_ReceiptObj.Name,
S_C_ManageContract_ReceiptObj.ReceiptValue,SumRelationValue from S_C_Invoice_ReceiptObject 
left join S_C_ManageContract_ReceiptObj on S_C_Invoice_ReceiptObject.ReceiptObjectID = S_C_ManageContract_ReceiptObj.ID 
left join (select Sum(RelationValue)as SumRelationValue,ReceiptObjectID 
from S_C_Invoice_ReceiptObject where S_C_InvoiceID !='{0}' group by ReceiptObjectID) SumDt on SumDt.ReceiptObjectID = S_C_ManageContract_ReceiptObj.ID 
where S_C_InvoiceID='{0}'  order by S_C_ManageContract_ReceiptObj.ID";
            var db = SQLHelper.CreateSqlHelper(ConnEnum.Market);
            var dt = db.ExecuteDataTable(String.Format(sql, ID));
            return Json(dt);
        }

        public JsonResult GetReceiptList(QueryBuilder qb, string InvoiceID)
        {
            string sql = @"select * from (select Sum(RelationValue) as RelationValue,InvoiceID,ReceiptID 
from dbo.S_C_InvoiceReceiptRelation group by InvoiceID,ReceiptID) RelationInfo
left join S_C_Receipt on RelationInfo.ReceiptID =S_C_Receipt.ID where InvoiceID = '{0}' {1}";
            string orderStr = " order by " + qb.SortField + " " + qb.SortOrder;
            var dt = this.SqlHelper.ExecuteDataTable(String.Format(sql, InvoiceID, orderStr));
            return Json(dt);
        }

        public JsonResult GetInvoiceDatailList(string ID)
        {
            string sql = @"select * from S_C_Invoice_InvoiceDatail where S_C_InvoiceID = '{0}'";
            var dt = this.SqlHelper.ExecuteDataTable(String.Format(sql, ID));
            return Json(dt);
        }

    }
}
