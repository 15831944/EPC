using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Market.Logic.Domain;
using Formula.Helper;
using Formula;
using System.Data;
using Base.Logic.BusinessFacade;
using Formula.DynConditionObject;
using Expenses.Logic;
using Config.Logic;
using Formula.Exceptions;

namespace Market.Areas.MarketUI.Controllers
{
    public class SupplierInvoiceController : MarketFormContorllor<S_SP_Invoice>
    {
        protected override void BeforeSave(Dictionary<string, string> dic, Base.Logic.Domain.S_UI_Form formInfo, bool isNew)
        {
            if (!isNew)
            {
                Expenses.Logic.BusinessFacade.DataInterfaceFo.ValidateDataSyn("S_SP_Invoice", dic.GetValue("ID"));
            }
            var invoiceValue = 0m;
            decimal.TryParse(dic.GetValue("Amount"), out invoiceValue);
            if (invoiceValue <=0)
            {
                throw new BusinessValidationException("【开票金额】必须大于0！");
            }

            var sql = string.Format("select top 1 * from S_EP_InvoiceConfirm where InvoiceID='{0}' order by ID desc", dic.GetValue("ID"));
            var dt = MarketSQLDB.ExecuteDataTable(sql);
            if (dt.Rows.Count > 0)
            {
                var invoiceConfirmInfo = FormulaHelper.DataRowToDic(dt.Rows[0]);
                var totalConfirmValue = 0m;
                decimal.TryParse(invoiceConfirmInfo.GetValue("TotalValue"), out totalConfirmValue);
                if (invoiceValue <= totalConfirmValue)
                {
                    throw new BusinessValidationException(string.Format("开票金额【{0}】必须大于开票已确认金额【{1}】", invoiceValue, totalConfirmValue.ToString("#0.00")));
                }
                double totalProgress = 0;
                double.TryParse(invoiceConfirmInfo.GetValue("TotalProgress"), out totalProgress);
                if (totalProgress >=1 && invoiceValue != totalConfirmValue)
                {
                    throw new BusinessValidationException("开票已经确认至100%，【开票金额】不能变更！");
                }
            }

            if (SysParams.Params.GetValue("SubContractConfirmMethod") == "InvoiceConfirm")//委外开票确认
            {
                if (string.IsNullOrEmpty(dic.GetValue("CostUnit")))
                {
                    throw new BusinessValidationException("请选择成本单元！");
                }
            }
            else
            {
                dic.SetValue("CostUnit", string.Empty);
                dic.SetValue("CostUnitName", string.Empty);
            }

            var invoiceDate = DateTime.Now;
            if (string.IsNullOrEmpty(dic.GetValue("InvoiceDate")) || !DateTime.TryParse(dic.GetValue("InvoiceDate"), out invoiceDate))
            {
                throw new BusinessValidationException("请选择开票日期！");
            }
            dic.SetValue("InvoiceDate", invoiceDate.ToString("yyyy-MM-dd"));
            dic.SetValue("BelongYear", invoiceDate.Year.ToString());
            dic.SetValue("BelongQuarter", ((invoiceDate.Month + 2) / 3).ToString());
            dic.SetValue("BelongMonth", invoiceDate.Month.ToString());

            var entity = this.GetEntityByID(dic["ID"]);
            if (entity == null) entity = new S_SP_Invoice();
            this.UpdateEntity(entity, dic);
            entity.Save();
        }

        protected override void AfterSave(Dictionary<string, string> dic, Base.Logic.Domain.S_UI_Form formInfo, bool isNew)
        {
            var contract = this.GetEntityByID<S_SP_SupplierContract>(dic["SupplierContract"]);
            if (contract != null)
            {
                contract.SummaryData();
                this.BusinessEntities.SaveChanges();
            }
        }

        protected override void BeforeDelete(string[] Ids)
        {
            foreach (var Id in Ids)
            {
                Expenses.Logic.BusinessFacade.DataInterfaceFo.ValidateDataSyn("S_SP_Invoice", Id);
            }
        }
        public override JsonResult Delete()
        {
            string listIDs = Request["ListIDs"];
            Specifications res = new Specifications();
            res.AndAlso("ID", listIDs.Split(','), QueryMethod.In);
            var list = this.BusinessEntities.Set<S_SP_Invoice>().Where(res.GetExpression<S_SP_Invoice>()).ToList();
            foreach (var item in list)
                item.Delete();
            this.BusinessEntities.SaveChanges();
            return Json("");
        }
    }
}
