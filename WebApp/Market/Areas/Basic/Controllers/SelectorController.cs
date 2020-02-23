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
using MvcAdapter;
using Market.Logic;
using Market.Logic.Domain;
using Config;
using Base.Logic;
namespace Market.Areas.Basic.Controllers
{
    public class SelectorController : MarketController
    {
        #region 客户联系人

        public ActionResult CustomerLinkmanMulti()
        {
            return View();
        }

        /// <summary>
        /// 获取客户联系人
        /// </summary>
        /// <param name="qb"></param>
        /// <returns></returns>
        public JsonResult GetCustomerLinkmanList(QueryBuilder qb)
        {
            string customerID = Request["CustomerID"];
            var data = entities.Set<S_F_Customer_LinkMan>().Where(c => c.S_F_CustomerID == customerID).WhereToGridData(qb);
            return Json(data);
        }

        #endregion

        #region 选择客户

        public ActionResult CustomerSingle()
        {
            return View();
        }

        public JsonResult GetSingleCustomerList(QueryBuilder qb)
        {
            GridData grid = entities.Set<S_F_Customer>().WhereToGridData(qb);
            return Json(grid);
        }



        #endregion

        #region 选择工程

        public JsonResult GetProgrammeList(QueryBuilder qb)
        {
            GridData grid = entities.Set<S_I_Engineering>().WhereToGridData(qb);
            return Json(grid);
        }

        #endregion

        #region 选择项目
        public JsonResult GetMarketProjectList(QueryBuilder qb)
        {
            string engineeringInfoID = this.GetQueryString("EngineeringInfo");
            if (!String.IsNullOrEmpty(engineeringInfoID))
                qb.Add("EngineeringInfo", QueryMethod.Equal, engineeringInfoID);
            GridData grid = entities.Set<S_I_Project>().WhereToGridData(qb);
            return Json(grid);
        }
        #endregion

        #region 选择合同

        public JsonResult GetManageContractList(QueryBuilder qb)
        {
            if (Request.QueryString["CustomerID"] != null)
                qb.Add("PartyAID", QueryMethod.Equal, Request.QueryString["CustomerID"]);//甲方
            //qb.Add("IsSigned", QueryMethod.Equal, ContractIsSigned.Signed.ToString());
            var data = entities.Set<S_C_ManageContract>().Where(qb);
            GridData gridData = new GridData(data);
            gridData.total = qb.TotolCount;

            return Json(gridData);
        }

        #endregion

        #region 获取年月
        /// <summary>
        /// 获取年月枚举
        /// </summary>
        /// <returns></returns>
        public JsonResult GetYearMonthEnum()
        {
            List<YearMonth> list = GetYearMonth();
            string key = Request["key"];
            return Json(list.Where(p => p.value.Contains(key)));
        }

        public struct YearMonth
        {
            public string value;
            public string text;
        }

        /// <summary>
        /// 年月枚举,201401
        /// </summary>
        /// <returns></returns>
        private List<YearMonth> GetYearMonth()
        {
            List<YearMonth> list = new List<YearMonth>();
            int beginYear = DateTime.Now.Year;
            for (int year = beginYear; year <= DateTime.Now.Year + 1; year++)
            {
                for (int month = 12; month >= 1; month--)
                {
                    var yearMonth = year.ToString() + month.ToString().PadLeft(2, '0');
                    list.Add(new YearMonth { value = yearMonth, text = yearMonth });
                }
            }
            for (int year = DateTime.Now.Year - 2; year < DateTime.Now.Year; year++)
            {
                for (int month = 12; month >= 1; month--)
                {
                    var yearMonth = year.ToString() + month.ToString().PadLeft(2, '0');
                    list.Add(new YearMonth { value = yearMonth, text = yearMonth });
                }
            }
            return list;
        }

        #endregion

        #region 选择合同和计划收款

        //获取合同列表
        public JsonResult GetContractList(QueryBuilder qb)
        {
            var sql = @"select
S_F_Customer.InvoiceName,BankInfo.BankAccount,BankInfo.BankName,
dbo.S_F_Customer.TaxAccounts, dbo.S_F_Customer.Address, dbo.S_F_Customer.LinkTelphone, 
isnull(S_C_ManageContract.ContractRMBAmount,0) - isnull(S_C_ManageContract.SumInvoiceValue,0) AS NoInvoiceAmount, 
S_C_ManageContract.*
from S_C_ManageContract
left join S_F_Customer on S_C_ManageContract.PartyA = S_F_Customer.ID
left join (select S_F_Customer_BankAccounts.* from (
select Min(ID) as MinID,S_F_CustomerID from S_F_Customer_BankAccounts
group by S_F_CustomerID) FirstBankAcount inner join S_F_Customer_BankAccounts on 
S_F_Customer_BankAccounts.ID=FirstBankAcount.MinID) BankInfo on
BankInfo.S_F_CustomerID=S_F_Customer.ID";

            string notIncludeIDs = GetQueryString("NotIncludeIDs");
            string customerID = GetQueryString("CustomerID");
            string partyAID = GetQueryString("PartyAID");
            var payerID = GetQueryString("PayerID");
            string where = " where 1=1 ";
            if (!string.IsNullOrEmpty(notIncludeIDs))
            {
                where += "and '" + notIncludeIDs + "' not like '%'+ ID + '%'";
            }

            if (!string.IsNullOrEmpty(customerID) && !string.IsNullOrEmpty(partyAID))
                where += " and (CustomerFullID like '%" + customerID + "%' or PartyA = '" + partyAID + "')";
            else if (!string.IsNullOrEmpty(customerID))
                where += " and CustomerFullID like '%" + customerID + "%'";
            else if (!string.IsNullOrEmpty(partyAID))
                where += " and PartyA = '" + partyAID + "'";

            if (!string.IsNullOrEmpty(payerID))
                where += " and charindex('" + payerID + "',PayerUnit,0)>0";

            sql += where;

            var db = SQLHelper.CreateSqlHelper(ConnEnum.Market);
            this.FillQueryBuilderFilter<S_C_ManageContract>(qb);
            var data = db.ExecuteGridData(sql, qb);
            return Json(data);
        }

        //获取计划收款列表
        public JsonResult GetReceiptObjectList(QueryBuilder qb, string ContractInfoID)
        {
            string sql = @"select *,(isnull(ReceiptValue,0)-isnull(FactInvoiceValue,0)-isnull(FactBadValue,0)) as RemainInvoiceValue from dbo.S_C_ManageContract_ReceiptObj
where S_C_ManageContractID ='{0}' {1} order by PlanFinishDate ";
            var whereStr = qb.GetWhereString(false);
            sql = String.Format(sql, ContractInfoID, whereStr);
            var dt = this.SqlHelper.ExecuteDataTable(sql);
            return Json(dt);
        }

        public JsonResult GetContractPlanReceiptList(QueryBuilder qb, string ContractInfoID)
        {
            var state = PlanReceiptState.UnReceipt.ToString();
            var data = this.entities.Set<S_C_PlanReceipt>().Where(d => d.ContractInfoID == ContractInfoID
                && d.State == state).Where(qb).ToList();
            return Json(data);
        }

        public JsonResult GetContractInvoiceList(QueryBuilder qb, string ContractInfoID)
        {
            string sql = @" select S_C_Invoice.*,isnull(SumRelationValue,0) as SumRelationValue,
Amount-isnull(SumRelationValue,0) as RemainRelationValue from S_C_Invoice
left join (select Sum(RelationValue) as SumRelationValue,InvoiceID from dbo.S_C_InvoiceReceiptRelation
group by InvoiceID) RelationInfo
on RelationInfo.InvoiceID = S_C_Invoice.ID where S_C_Invoice.ContractInfoID='{0}' and State='" + InvoiceState.Normal.ToString() + "'";
            string whereStr = qb.GetWhereString(false);
            if (!String.IsNullOrEmpty(whereStr)) sql += whereStr;
            var dt = this.SqlHelper.ExecuteDataTable(String.Format(sql, ContractInfoID));
            return Json(dt);
        }

        public JsonResult GetCreditNoteReceiptList(QueryBuilder qb, string ContractInfoID)
        {
            string sql = @"select S_C_ManageContract_ReceiptObj.ID, S_C_ManageContract.ContractRMBAmount,S_C_ManageContract.SumInvoiceValue, S_C_ManageContract_ReceiptObj.Name, S_C_ManageContract_ReceiptObj.PlanFinishDate, isnull(MinusRelationValue,0) as MinusRelationValue, (isnull(FactInvoiceValue,0)- isnull(ApplyCreditValue,0)) as RemainValue from dbo.S_C_ManageContract_ReceiptObj
inner join S_C_ManageContract on S_C_ManageContract_ReceiptObj.S_C_ManageContractID = S_C_ManageContract.ID
left join (select abs(sum(isnull(RelationValue,0))) as MinusRelationValue,ReceiptObjectID from S_C_Invoice_ReceiptObject inner join S_C_Invoice on S_C_Invoice.ID = S_C_Invoice_ReceiptObject.S_C_InvoiceID where S_C_Invoice.InvoiceType = 'CreditNote' group by ReceiptObjectID ) InvoiceReceiptObj on InvoiceReceiptObj.ReceiptObjectID = S_C_ManageContract_ReceiptObj.ID
left join (select abs(sum(isnull(T_C_CreditNoteApply_Detail.CreditValue,0))) as ApplyCreditValue,PlanReceiptID from T_C_CreditNoteApply inner join T_C_CreditNoteApply_Detail on T_C_CreditNoteApply.ID = T_C_CreditNoteApply_Detail.T_C_CreditNoteApplyID where T_C_CreditNoteApply.FlowPhase <> 'End' group by  PlanReceiptID) CreditNoteApplyDetail on CreditNoteApplyDetail.PlanReceiptID  = S_C_ManageContract_ReceiptObj.ID
where S_C_ManageContractID = '{0}' order by PlanFinishDate ";
            string whereStr = qb.GetWhereString(false);
            sql = string.Format(sql, ContractInfoID, whereStr);
            var dt = this.SqlHelper.ExecuteDataTable(String.Format(sql, ContractInfoID));
            return Json(dt);
        }

        public JsonResult GetContractDeptList(QueryBuilder qb, string ContractInfoID)
        {
            var data = this.entities.Set<S_C_ManageContract_DeptRelation>().Where(d => d.S_C_ManageContractID == ContractInfoID
                ).Where(qb).ToList();
            return Json(data);
        }
        public JsonResult GetContractProjectList(QueryBuilder qb, string ContractInfoID)
        {
            var sql = @"select S_C_ManageContract_ProjectRelation.*,SumRelationValue from S_C_ManageContract_ProjectRelation
left join (select Sum(RelationValue) as SumRelationValue,ProjectID,ContractInfoID
from S_C_Receipt_ProjectRelation group by ProjectID,ContractInfoID) SumRelationInfo
on  SumRelationInfo.ProjectID = S_C_ManageContract_ProjectRelation.ProjectID 
and SumRelationInfo.ContractInfoID = S_C_ManageContract_ProjectRelation.S_C_ManageContractID
            where S_C_ManageContractID='{0}' order by sortindex";
            var dt = this.SqlHelper.ExecuteDataTable(String.Format(sql, ContractInfoID));
            return Json(dt);
        }
        #endregion

        #region 人员选择页面

        #endregion
    }
}
