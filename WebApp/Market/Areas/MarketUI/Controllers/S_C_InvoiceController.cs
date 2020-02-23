using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Market.Logic.Domain;
using Formula.Helper;
using Config.Logic;
using Formula.DynConditionObject;
using Formula;
using Market.Logic;

namespace Market.Areas.MarketUI.Controllers
{
    public class InvoiceController : MarketFormContorllor<S_C_Invoice>
    {
        protected override void AfterGetData(System.Data.DataTable dt, bool isNew, string upperVersionID)
        {
            if (isNew)
            {
            }
            else
            {
                var contractID = dt.Rows[0]["ContractInfoID"].ToString();
                if (contractID != "" && contractID != null)
                {
                    var contract = this.GetEntityByID<S_C_ManageContract>(contractID);
                    var invoiceID = dt.Rows[0]["ID"].ToString();
                    var contractInvoiceValue = Convert.ToDecimal(contract.S_C_Invoice.Where(d => d.ID != invoiceID).Sum(d => d.Amount));
                    var remainInvoiceValue = Convert.ToDecimal(contract.ContractRMBAmount) - contractInvoiceValue;
                    if (remainInvoiceValue < 0) remainInvoiceValue = 0;
                    SetDtValue(dt, "ContractValue", Math.Round(Convert.ToDecimal(contract.ContractRMBAmount), 2));
                    SetDtValue(dt, "ContractInvoiceValue", Math.Round(contractInvoiceValue, 2));
                    SetDtValue(dt, "RemainInvoiceValue", Math.Round(remainInvoiceValue, 2));

                    //收款项子表最新数据
                    string sql = @"select S_C_Invoice_ReceiptObject.*,NewSumRelationValue,S_C_ManageContract_ReceiptObj.Name ObjName,
S_C_ManageContract_ReceiptObj.ReceiptValue ObjReceiptValue from S_C_Invoice_ReceiptObject 
left join S_C_ManageContract_ReceiptObj on S_C_Invoice_ReceiptObject.ReceiptObjectID = S_C_ManageContract_ReceiptObj.ID 
left join (select Sum(RelationValue)as NewSumRelationValue,ReceiptObjectID 
from S_C_Invoice_ReceiptObject where S_C_InvoiceID !='{0}' group by ReceiptObjectID) SumDt on SumDt.ReceiptObjectID = S_C_ManageContract_ReceiptObj.ID 
where S_C_InvoiceID='{0}'  order by S_C_ManageContract_ReceiptObj.ID";
                    var receiptObjDt = this.MarketSQLDB.ExecuteDataTable(String.Format(sql, invoiceID));
                    var receiptObjList = FormulaHelper.DataTableToListDic(receiptObjDt);
                    foreach (var receiptObj in receiptObjList)
                    {
                        receiptObj.SetValue("SumRelationValue", receiptObj.GetValue("NewSumRelationValue"));
                        if (string.IsNullOrEmpty(receiptObj.GetValue("Name")))
                            receiptObj.SetValue("Name", receiptObj.GetValue("ObjName"));
                        if (string.IsNullOrEmpty(receiptObj.GetValue("ReceiptValue")))
                            receiptObj.SetValue("ReceiptValue", receiptObj.GetValue("ObjReceiptValue"));
                    }
                    SetDtValue(dt, "ReceiptObject", JsonHelper.ToJson(receiptObjList));

                }
            }
        }

        protected override void BeforeSaveDetail(Dictionary<string, string> dic, string subTableName, Dictionary<string, string> detail, List<Dictionary<string, string>> detailList, Base.Logic.Domain.S_UI_Form formInfo)
        {
            if (subTableName == "ReceiptObject")
            {
                detail.SetValue("ContractInfoID", dic.GetValue("ContractInfoID"));
                var receiptObject = this.GetEntityByID<S_C_ManageContract_ReceiptObj>(detail.GetValue("ReceiptObjectID"));
                if (receiptObject == null)
                    throw new Formula.Exceptions.BusinessException("未找到收款项【" + detail.GetValue("Name") + "】");
                var relationValue = String.IsNullOrEmpty(detail.GetValue("RelationValue")) ? 0m : Convert.ToDecimal(detail.GetValue("RelationValue"));
                var invoiceState = InvoiceState.Normal.ToString();
                var id = dic.GetValue("ID");
                var factInvoiceValue = Convert.ToDecimal(receiptObject.S_C_Invoice_ReceiptObject.Where(a => a.S_C_Invoice.State == invoiceState && a.S_C_InvoiceID != id).Sum(a => a.RelationValue));
                var receiptValue = Convert.ToDecimal(receiptObject.ReceiptValue);
                if (dic.GetValue("InvoiceType") == Market.Logic.InvoiceType.CreditNote.ToString())
                {
                    var sumRelationValue = String.IsNullOrEmpty(detail.GetValue("SumRelationValue")) ? 0m : Convert.ToDecimal(detail.GetValue("SumRelationValue"));

                    if (relationValue > 0)
                        throw new Formula.Exceptions.BusinessException("红冲发票中【" + detail.GetValue("Name") + "】本次开票金额要小于0");
                    if (sumRelationValue < Math.Abs(relationValue))
                        throw new Formula.Exceptions.BusinessException("红冲发票中【" + detail.GetValue("Name") + "】本次开票金额不能超过已开票金额【" + sumRelationValue + "】");
                }
                else
                {
                    var remainInvoiceValue = receiptValue - factInvoiceValue;
                    if (relationValue > remainInvoiceValue) throw new Formula.Exceptions.BusinessException("【" + detail.GetValue("Name") + "】的本次开票金额，不能高于剩余可开票金额【" + remainInvoiceValue + "】");
                }
            }
        }

        protected override void BeforeSaveSubTable(Dictionary<string, string> dic, string subTableName, List<Dictionary<string, string>> detailList, Base.Logic.Domain.S_UI_Form formInfo)
        {
            base.BeforeSaveSubTable(dic, subTableName, detailList, formInfo);
            if (subTableName == "ReceiptObject")
            {
                var sumRelation = 0M;
                var invoiceValue = String.IsNullOrEmpty(dic.GetValue("Amount")) ? 0m : Convert.ToDecimal(dic.GetValue("Amount"));
                foreach (var item in detailList)
                {
                    var relationValue = String.IsNullOrEmpty(item.GetValue("RelationValue")) ? 0m : Convert.ToDecimal(item.GetValue("RelationValue"));
                    sumRelation += relationValue;
                }
                if (Math.Abs(sumRelation) > Math.Abs(invoiceValue))
                    throw new Formula.Exceptions.BusinessException("关联收款项的本次开票金额总计不能大于开票金额");
            }
            if (subTableName == "InvoiceDatail")
            {
                var sumRelation = 0M;
                var invoiceValue = String.IsNullOrEmpty(dic.GetValue("Amount")) ? 0m : Convert.ToDecimal(dic.GetValue("Amount"));
                foreach (var item in detailList)
                {
                    var relationValue = String.IsNullOrEmpty(item.GetValue("InvoiceAmount")) ? 0m : Convert.ToDecimal(item.GetValue("InvoiceAmount"));

                    if (item.GetValue("InvoiceState") == InvoiceState.Normal.ToString())
                        sumRelation += relationValue;
                }
                if (Math.Abs(sumRelation) > Math.Abs(invoiceValue))
                    throw new Formula.Exceptions.BusinessException("发票明细正常金额合计与开票金额不相等，请重新确认！");
            }
        }

        protected override void BeforeSave(Dictionary<string, string> dic, Base.Logic.Domain.S_UI_Form formInfo, bool isNew)
        {
            if (!isNew)
            {
                Expenses.Logic.BusinessFacade.DataInterfaceFo.ValidateDataSyn("S_C_Invoice", dic.GetValue("ID"));
            }
            var contractid = dic.GetValue("ContractInfoID");
            var id = dic.GetValue("ID");
            var contract = this.GetEntityByID<S_C_ManageContract>(contractid);
            if (contract == null) throw new Formula.Exceptions.BusinessException("未能找到ID为【" + contractid + "】的合同信息，无法进行开票操作");
            if (String.IsNullOrEmpty(dic.GetValue("PayerUnitID")))
            {
                dic.SetValue("PayerUnitID", contract.PartyA);
                dic.SetValue("PayerUnitIDName", contract.PartyAName);
            }
            dic.SetValue("PayerUnit", dic.GetValue("PayerUnitIDName"));
            dic.SetValue("InvoiceMaker", dic.GetValue("InvoiceMakerIDName"));
            dic.SetValue("ContractCode", contract.SerialNumber);
            dic.SetValue("ContractName", contract.Name);
            if (String.IsNullOrEmpty(dic.GetValue("PayerUnitSub")))
            {
                dic.SetValue("PayerUnitSub", contract.PartyA);
                dic.SetValue("PayerUnitSubName", contract.PartyAName);
            }
            var customer = this.GetEntityByID<S_F_Customer>(dic.GetValue("PayerUnitID"));
            if (customer != null)
            {
                if (customer.S_F_Customer_BankAccounts.Count(c => c.BankAccount == dic.GetValue("BankAccount")) == 0
                    && !String.IsNullOrEmpty(dic.GetValue("BankAccount")) && !String.IsNullOrEmpty(dic.GetValue("BankName")))
                {
                    var bankAccount = new S_F_Customer_BankAccounts();
                    bankAccount.BankName = dic.GetValue("BankName");
                    bankAccount.BankAccount = dic.GetValue("BankAccount");
                    bankAccount.BankAccountName = dic.GetValue("BankName");
                    customer.S_F_Customer_BankAccounts.Add(bankAccount);
                }
                if (string.IsNullOrEmpty(customer.TaxAccounts))
                    customer.TaxAccounts = dic.GetValue("TaxAccount");
                if (string.IsNullOrEmpty(customer.InvoiceName))
                    customer.InvoiceName = dic.GetValue("InvoiceName");
                if (string.IsNullOrEmpty(dic.GetValue("CustomerFullID")))
                    dic.SetValue("CustomerFullID", customer.FullID);
            }
            if (String.IsNullOrEmpty(dic.GetValue("ReceviedUnitID")))
            {
                dic.SetValue("ReceviedUnitID", contract.BusinessDept);
                dic.SetValue("ReceviedUnit", contract.BusinessDeptName);
            }
            if (String.IsNullOrEmpty(dic.GetValue("State")))
                dic.SetValue("State", InvoiceState.Normal.ToString());
            DateTime InvoiceDate = DateTime.Now;
            if (!String.IsNullOrEmpty(dic.GetValue("InvoiceDate")))
                InvoiceDate = Convert.ToDateTime(dic.GetValue("InvoiceDate"));
            dic.SetValue("BelongYear", InvoiceDate.Year.ToString());
            dic.SetValue("BelongMonth", InvoiceDate.Month.ToString());
            dic.SetValue("BelongQuarter", MarketHelper.GetQuarter(InvoiceDate.Month).ToString());
            var contractInvoiceAmmout = 0M;
            //获取所属合同除本发票外的所有发票的开票金额
            var invoceState = InvoiceState.Normal.ToString();
            if (this.BusinessEntities.Set<S_C_Invoice>().Where(d => d.ContractInfoID == contractid && d.ID != id && d.State == invoceState).Count() > 0)
                contractInvoiceAmmout = Convert.ToDecimal(this.BusinessEntities.Set<S_C_Invoice>().
                    Where(d => d.ContractInfoID == contractid && d.ID != id &&
                        d.State == invoceState).Sum(d => d.Amount));
            var sumInvoiceValue = contractInvoiceAmmout + Convert.ToDecimal(dic.GetValue("Amount"));
            //限制总开票金额不能大于合同金额
            if (sumInvoiceValue > contract.ContractRMBAmount)
                throw new Formula.Exceptions.BusinessException("累计开票金额【" + sumInvoiceValue + "】不能大于合同总金额【" + contract.ContractRMBAmount + "】");
        }

        protected override void AfterSave(Dictionary<string, string> dic, Base.Logic.Domain.S_UI_Form formInfo, bool isNew)
        {
            //sql汇总合同的开票金额、收款项的开票金额
            string sql = @"update S_C_ManageContract 
set SumInvoiceValue = (select SUM(Amount) from S_C_Invoice where ContractInfoID='{0}')
where ID='{0}'
update S_C_ManageContract_ReceiptObj
set FactInvoiceValue = (select SUM(RelationValue) from S_C_Invoice_ReceiptObject where ReceiptObjectID=S_C_ManageContract_ReceiptObj.ID)
where S_C_ManageContractID='{0}' ";
            sql = string.Format(sql, dic.GetValue("ContractInfoID"));
            this.MarketSQLDB.ExecuteNonQuery(sql);
        }

        protected override void BeforeDelete(string[] Ids)
        {
            foreach (var Id in Ids)
            {
                Expenses.Logic.BusinessFacade.DataInterfaceFo.ValidateDataSyn("S_C_Invoice", Id);
            }
        }

        public override JsonResult Delete()
        {
            string listIDs = Request["ListIDs"];
            Specifications res = new Specifications();
            res.AndAlso("ID", listIDs.Split(','), QueryMethod.In);
            var list = this.BusinessEntities.Set<S_C_Invoice>().Where(res.GetExpression<S_C_Invoice>()).ToList();
            foreach (var item in list)
                item.Delete();
            this.BusinessEntities.SaveChanges();
            return Json("");
        }

        public JsonResult GetInvoice_MaxAmount()
        {
            var rtn = "0";
            if (string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["Invoice_MaxAmount"]) == false)
                rtn = System.Configuration.ConfigurationManager.AppSettings["Invoice_MaxAmount"];
            return Json(rtn);
        }

        public JsonResult Invalid(string invoiceList)
        {
            var list = JsonHelper.ToList(invoiceList);
            foreach (var item in list)
            {
                var invoice = this.GetEntityByID(item.GetValue("ID"));
                if (invoice == null)
                    throw new Formula.Exceptions.BusinessException("未能找到ID为【" + item.GetValue("ID") + "】的开票信息，无法进行作废操作");
                invoice.Invalid();
            }
            this.BusinessEntities.SaveChanges();
            return Json("");
        }

        public JsonResult UnInvalid(string invoiceList)
        {
            var list = JsonHelper.ToList(invoiceList);
            foreach (var item in list)
            {
                var invoice = this.GetEntityByID(item.GetValue("ID"));
                if (invoice == null)
                    throw new Formula.Exceptions.BusinessException("未能找到ID为【" + item.GetValue("ID") + "】的开票信息，无法进行作废操作");
                invoice.Invalid();
            }
            this.BusinessEntities.SaveChanges();
            return Json("");
        }
    }
}
