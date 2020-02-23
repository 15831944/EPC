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
    public class AcceptanceBillController : MarketController<S_C_AcceptanceBill>
    {
        protected override void BeforeSave(S_C_AcceptanceBill entity, bool isNew)
        {
            base.BeforeSave(entity, isNew);
            string planData = this.Request["PlanReceiptData"];
            string invoiceData = this.Request["InvoiceData"];
            string contractData = this.Request["ContractData"]; 

            string receiptIDs = entity.ReceiptIDs;

            if (String.IsNullOrEmpty(contractData))
                throw new Formula.Exceptions.BusinessException("必须有关联合同项");

            var contractList = JsonHelper.ToList(contractData);
            var contractAmount = contractList.Sum(a => { 
               decimal tmp = 0;
               decimal.TryParse(a.GetValue("ReceiptValue"), out tmp);
               return tmp;
            });

            if(contractAmount != entity.Amount)
                throw new Formula.Exceptions.BusinessException("汇票金额必须与所列合同的本次收款金额总和相等");

            var planList = JsonHelper.ToList(planData);
            var invoiceList = JsonHelper.ToList(invoiceData);
            

            //清除原有关系数据,重新赋值
            List<S_C_Receipt> oldUnDeletedReceiptList = new List<S_C_Receipt>();
            if (!string.IsNullOrEmpty(receiptIDs))
            {
                var receiptIDArr = receiptIDs.Split(',');
                foreach (var receiptID in receiptIDArr)
                {
                    var receipt = entities.Set<S_C_Receipt>().Find(receiptID);
                    //如果合同列表已经没有对应的旧数据则删除
                    if (!contractList.Any(a => a.GetValue("ID") == receipt.ContractInfoID))
                    {
                        receipt.Delete();
                    }
                    else
                    {
                        oldUnDeletedReceiptList.Add(receipt);
                        receipt.ClearPlanReceipt();
                        receipt.ClearInvoice();
                    }
                }
            }

            receiptIDs = "";
            foreach (var contract in contractList)
            {
                var contractObj = entities.Set<S_C_ManageContract>().Find(contract.GetValue("ID"));

                S_C_Receipt receipt = oldUnDeletedReceiptList.SingleOrDefault(a => a.ContractInfoID == contractObj.ID);
                if (receipt == null)
                {
                    receipt = new S_C_Receipt();                     
                    receipt.ID = FormulaHelper.CreateGuid();
                    receipt.ContractInfoID = contractObj.ID;
                    receipt.ContractName = contractObj.Name;
                    receipt.ContractCode = contractObj.SerialNumber;
                    receipt.ReceiptMasterUnitID = contractObj.BusinessDept;
                    receipt.ReceiptMasterUnit = contractObj.BusinessDeptName;
                    receipt.ProductUnit = contractObj.ProductionDept;
                    receipt.ProductUnitName = contractObj.ProductionDeptName;
                    receipt.CreateUserID = CurrentUserInfo.UserID;
                    receipt.CreateUser = CurrentUserInfo.UserName;
                    receipt.CreateDate = DateTime.Now;
                    receipt.ModifyUserID = CurrentUserInfo.UserID;
                    receipt.ModifyUser = CurrentUserInfo.UserName;
                    receipt.OrgID = CurrentUserInfo.UserOrgID;
                    receipt.OrgName = CurrentUserInfo.UserOrgName;
                    receipt.ModifyDate = DateTime.Now;
                    receipt.ReceiptType = "汇票";//TODO
                    receipt.State = "Create";
                    contractObj.S_C_Receipt.Add(receipt);
                    entities.Set<S_C_Receipt>().Add(receipt);
                }
                else
                {
                    //原到款信息已经绑定过该合同的,不允许删除再绑
                    if (receipt.ID != contract.GetValue("ReceiptID"))
                    {
                        throw new Formula.Exceptions.BusinessException("合同【" + contractObj.Name + "】原已存在该汇票中,不允许删除后再次加入");
                    }
                    receipt.ModifyUserID = CurrentUserInfo.UserID;
                    receipt.ModifyUser = CurrentUserInfo.UserName;
                    receipt.ModifyDate = DateTime.Now;
                    receipt.State = "Modify";
                }
                
                decimal amount = 0;
                decimal.TryParse(contract.GetValue("ReceiptValue"), out amount);
                receipt.Amount = amount;               
                receipt.ArrivedDate = entity.BillDate??DateTime.MaxValue;
                receipt.BelongMonth = receipt.ArrivedDate.Month;
                receipt.BelongYear = receipt.ArrivedDate.Year;
                receipt.BelongQuarter = MarketHelper.GetQuarter(receipt.ArrivedDate);
                receipt.CustomerFullID = entity.Customer;
                receipt.CustomerID = entity.Customer;
                receipt.CustomerName = entity.CustomerName;
                receipt.OperatorID = entity.Receiver;
                receipt.Operator = entity.ReceiverName;


                var sumReceiptValue = 0M;
                foreach (var plan in planList.Where(a => a.GetValue("ContractInfoID") == contractObj.ID))
                {
                    if (String.IsNullOrEmpty(plan.GetValue("RelationValue"))) throw new Formula.Exceptions.BusinessException("合同【" + contractObj.Name + "】计划收款未填写关联金额，操作失败");
                    var relateValue = Convert.ToDecimal(plan.GetValue("RelationValue"));
                    sumReceiptValue += relateValue;
                    receipt.RelateToPlanReceipt(plan.GetValue("PlanID"), relateValue);
                }
                if (sumReceiptValue > amount)
                    throw new Formula.Exceptions.BusinessException("合同【" + contractObj.Name + "】关联的计划收款总金额不能大于本次收款金额");

                var sumInvoiceValue = 0M;
                foreach (var invoice in invoiceList.Where(a => a.GetValue("ContractInfoID") == contractObj.ID))
                {
                    if (String.IsNullOrEmpty(invoice.GetValue("RelationValue"))) throw new Formula.Exceptions.BusinessException("合同【" + contractObj.Name + "】发票未填写关联金额，操作失败");
                    var relateValue = Convert.ToDecimal(invoice.GetValue("RelationValue"));
                    sumInvoiceValue += relateValue;
                    receipt.RelateToInvoice(invoice.GetValue("InvoiceID"), relateValue);
                }
                if (sumInvoiceValue > amount)
                    throw new Formula.Exceptions.BusinessException("合同【" + contractObj.Name + "】关联的发票总金额不能大于本次收款金额");

                receiptIDs += receipt.ID + ",";
                contractObj.SummaryReceiptData();
            }

            entity.ReceiptIDs = receiptIDs.Substring(0, receiptIDs.Length - 1);
        }

        protected override void BeforeDelete(List<S_C_AcceptanceBill> entityList)
        {
            base.BeforeDelete(entityList);
            foreach (var entity in entityList)
            {
                string receiptIDs = entity.ReceiptIDs;
                if (!string.IsNullOrEmpty(receiptIDs))
                {
                    var receiptIDArr = receiptIDs.Split(',');
                    foreach (var receiptID in receiptIDArr)
                    {
                        var receipt = entities.Set<S_C_Receipt>().Find(receiptID);
                        receipt.Delete();
                    }
                }
            }
        }

        public JsonResult GetContractList(string billID)
        {
            string sql = @"select S_C_ManageContract.*, S_C_ManageContract.SumReceiptValue,S_C_Receipt.Amount as ReceiptValue,
                         (S_C_ManageContract.ContractRMBAmount - S_C_ManageContract.SumReceiptValue) as SumNoReceiptValue,S_C_Receipt.ID as ReceiptID,
                         S_C_ManageContract.ContractRMBAmount as Amount
                         from S_C_AcceptanceBill 
                         inner join S_C_Receipt on S_C_AcceptanceBill.ReceiptIDS like '%'+S_C_Receipt.ID+'%' 
                         inner join S_C_ManageContract on S_C_Receipt.ContractInfoID = S_C_ManageContract.ID
                         where S_C_AcceptanceBill.ID = '{0}'";
            var dt = this.SqlHelper.ExecuteDataTable(String.Format(sql, billID));
            return Json(dt);
        }

        public JsonResult GetPlanRelationList(string billID)
        {
            var acceptanceBill = entities.Set<S_C_AcceptanceBill>().Find(billID);
            if (acceptanceBill == null)
                return Json("");
            List<Dictionary<string, object>> dicList = new List<Dictionary<string, object>>();
            if (!string.IsNullOrEmpty(acceptanceBill.ReceiptIDs))
            {
                var idArr = acceptanceBill.ReceiptIDs.Split(',');
                foreach (var receiptID in idArr)
                {
                    string sql = @"select * from dbo.S_C_ReceiptPlanRelation
                                 left join dbo.S_C_PlanReceipt on S_C_PlanReceipt.ID=S_C_ReceiptPlanRelation.PlanID
                                 left join (select Sum(RelationValue) as SumRelationValue,PlanID
                                 from S_C_ReceiptPlanRelation where ReceiptID !='{0}'
                                 group by PlanID) SumRelationInfo on  SumRelationInfo.PlanID = S_C_ReceiptPlanRelation.ID
                                 where ReceiptID='{0}' ";
                    var dt = this.SqlHelper.ExecuteDataTable(String.Format(sql, receiptID));
                    string json = JsonHelper.ToJson(dt);
                    dicList.AddRange(JsonHelper.ToList(json));
                }
            }

            return Json(dicList);
        }

        public JsonResult GetInvoiceRelationList(string billID)
        {
            var acceptanceBill = entities.Set<S_C_AcceptanceBill>().Find(billID);
            if (acceptanceBill == null)
                return Json("");
            List<Dictionary<string, object>> dicList = new List<Dictionary<string, object>>();
            if (!string.IsNullOrEmpty(acceptanceBill.ReceiptIDs))
            {
                var idArr = acceptanceBill.ReceiptIDs.Split(',');
                foreach (var receiptID in idArr)
                {
                    string sql = @"select * from S_C_InvoiceReceiptRelation
                         left join dbo.S_C_Invoice on S_C_Invoice.ID=S_C_InvoiceReceiptRelation.InvoiceID
                         left join (select Sum(RelationValue) as SumRelationValue,InvoiceID
                         from S_C_InvoiceReceiptRelation where ReceiptID !='{0}'
                         group by InvoiceID) SumRelationInfo on  SumRelationInfo.InvoiceID = S_C_Invoice.ID
                         where ReceiptID='{0}' ";
                    var dt = this.SqlHelper.ExecuteDataTable(String.Format(sql, receiptID));
                    string json = JsonHelper.ToJson(dt);
                    dicList.AddRange(JsonHelper.ToList(json));
                }
            }

            return Json(dicList);
        }

        public JsonResult GetWriteOffList(QueryBuilder qb)
        {
            string sql = @"select * from (select S_C_Receipt.*,PlanRelationValue,InvoiceRelationValue,
                         isnull(Amount,0)-isnull(PlanRelationValue,0) as RemainPlanValue,
                         isnull(Amount,0)-isnull(InvoiceRelationValue,0) as RemainInvoiceValue,
                         case when (isnull(Amount,0)-isnull(PlanRelationValue,0))=0 and 
                         (isnull(Amount,0)-isnull(InvoiceRelationValue,0))=0 then 'Yes' else 'No' end as WriteOffState
                         from S_C_Receipt left join (select Sum(RelationValue) as PlanRelationValue,ReceiptID from S_C_ReceiptPlanRelation
                         group by ReceiptID) PlanRelation on PlanRelation.ReceiptID=S_C_Receipt.ID
                         left join (select Sum(RelationValue) as InvoiceRelationValue,ReceiptID from S_C_InvoiceReceiptRelation
                         group by ReceiptID) InvoiceRelation on  InvoiceRelation.ReceiptID = S_C_Receipt.ID) TableInfo  ";
            var data = this.SqlHelper.ExecuteGridData(sql, qb);
            return Json(data);
        }

        public JsonResult GetInvoiceList(QueryBuilder qb, string ContractInfoID)
        {
            var State = InvoiceState.Invalid.ToString();
            string sql = @" select S_C_Invoice.*,isnull(SumRelationValue,0) as SumRelationValue,
                         Amount-isnull(SumRelationValue,0) as RemainRelationValue from S_C_Invoice
                         left join (select Sum(RelationValue) as SumRelationValue,InvoiceID from dbo.S_C_InvoiceReceiptRelation
                         group by InvoiceID) RelationInfo
                         on RelationInfo.InvoiceID = S_C_Invoice.ID where S_C_Invoice.ContractInfoID='{0}' and (State <> '" + State + "' or State is null )";
            string whereStr = qb.GetWhereString(false);
            if (!String.IsNullOrEmpty(whereStr)) sql += whereStr;
            var dt = this.SqlHelper.ExecuteDataTable(String.Format(sql, ContractInfoID));
            return Json(dt);
        }

        public JsonResult GetReceiptObjectList(string ContractInfoID)
        {
            string sql = @"select * from dbo.S_C_ReceiptObject where ContractInfoID ='{0}'   order by Code ";
            var dt = this.SqlHelper.ExecuteDataTable(String.Format(sql, ContractInfoID));
            return Json(dt);
        }
        
        public JsonResult GetContract(string receiptId)
        {
            var recepit = entities.Set<S_C_Receipt>().Find(receiptId);
            if (recepit != null && recepit.S_C_ManageContract != null)
                return Json(recepit.S_C_ManageContract);
            return Json("");
        }
    }
}
