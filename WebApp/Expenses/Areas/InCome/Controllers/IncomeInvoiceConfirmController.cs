using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Config.Logic;
using Formula.Helper;
using Expenses.Logic;
using Expenses.Logic.Domain;
using System.Data;
using MvcAdapter;
using Base.Logic.Domain;
using Formula;

namespace Expenses.Areas.InCome.Controllers
{
    public class IncomeInvoiceConfirmController : ExpensesFormController<S_EP_IncomeInvoiceConfirm>
    {
        public JsonResult GetList(QueryBuilder qb, string DetailID)
        {
            string sql = String.Format(@"select FinalConfirm.ID as FinalID,ISNULL(InvoiceConfirmRevert.ID,'0') as InvoiceConfirmRevertID,InvoiceConfirm.* 
from (select * from S_EP_IncomeInvoiceConfirm where InvoiceID='{0}')InvoiceConfirm 
left join (select top 1 ID from S_EP_IncomeInvoiceConfirm where InvoiceID='{0}' order by ID desc) FinalConfirm on InvoiceConfirm.ID=FinalConfirm.ID 
outer apply( select top 1 * from S_EP_IncomeInvoiceConfirmRevert a where InvoiceConfirm.ID=a.InvoiceConfirmID) InvoiceConfirmRevert 
order by ID", DetailID);
            var data = this.SQLDB.ExecuteDataTable(sql);
            return Json(data);
        }

        public JsonResult ValidateStart(string DetailID)
        {
            if (SysParams.Params.GetValue("IncomeInvoiceConfirm").ToLower() != "true")//开票确认
            {
                throw new Formula.Exceptions.BusinessValidationException("系统参数设置中【开票收入确认】为否！");
            }

            var dic = this.GetDataDicByID("S_C_Invoice", DetailID);
            if (dic == null)
            {
                throw new Formula.Exceptions.BusinessValidationException("没有找到指定的开票信息，无法进行确认");
            }
            var data = this.SQLDB.ExecuteDataTable(string.Format(@"select InvoiceConfirm.*,InvoiceConfirmRevert.ID as InvoiceConfirmRevertID 
from(select top 1 * from S_EP_InvoiceConfirm where InvoiceID = '{0}' order by ID desc)InvoiceConfirm
left join S_EP_InvoiceConfirmRevert InvoiceConfirmRevert on InvoiceConfirm.ID = InvoiceConfirmRevert.InvoiceConfirmID ", DetailID));
            if (data.Rows.Count > 0)
            {
                if (data.Rows[0]["InvoiceConfirmRevertID"] != null && !string.IsNullOrEmpty(data.Rows[0]["InvoiceConfirmRevertID"].ToString()))
                {
                    throw new Formula.Exceptions.BusinessValidationException("此发票正在撤销中，不能进行确认！");
                }

                var totalProgress = 0m;
                decimal.TryParse(data.Rows[0]["TotalProgress"].ToString(), out totalProgress);
                if (totalProgress >= 1)
                {
                    throw new Formula.Exceptions.BusinessValidationException("已经确认至100%，不能再进行确认！");
                }
            }
            return Json("");
        }

        protected override void BeforeSave(Dictionary<string, string> dic, S_UI_Form formInfo, bool isNew)
        {
            if (String.IsNullOrEmpty(dic.GetValue("ConfirmDate")))
            {
                throw new Formula.Exceptions.BusinessValidationException("必须填写完成日期");
            }
            var dateTime = String.IsNullOrEmpty(dic.GetValue("ConfirmDate")) ? DateTime.Now : Convert.ToDateTime(dic.GetValue("ConfirmDate"));
            CostFO.ValidatePeriodIsClosed(dateTime);
            ValidateStart(dic.GetValue("InvoiceID"));
        }

        //批量确认开票
        public JsonResult BatchConfirm()
        {
            if (SysParams.Params.GetValue("IncomeInvoiceConfirm").ToLower() != "true")//开票确认
            {
                throw new Formula.Exceptions.BusinessValidationException("系统参数设置中【开票收入确认】为否！");
            }

            var confirmDate = DateTime.Now.Date;
            if (!DateTime.TryParse(Request["ConfirmDate"], out confirmDate))
            {
                throw new Formula.Exceptions.BusinessValidationException("请选择确认日期！");
            }
            CostFO.ValidatePeriodIsClosed(confirmDate);

            var list = JsonHelper.ToList(Request["ListData"]);
            string mainSql = "";
            foreach (var item in list)
            {
                #region 验证数据，变量赋值
                //上期开票确认信息
                var sql = string.Format(@"select * ,
1 as TotalProgress,--本期确认进度_默认100
InvoiceValue as TotalValue,--本期末累计确认金额_默认为合同额(全确认)
CurrentValue/(1+TaxRate)*TaxRate as TaxValue,--税金
CurrentValue/(1+TaxRate) as ClearValue,--去税金额
InvoiceValue
from (

select Invoice.ID,
ISNULL(Invoice.Amount,0) as InvoiceValue,
Invoice.PayerUnit,
Invoice.TaxRate,
Invoice.IncomeUnit,
Invoice.InvoiceType,
Invoice.ContractInfoID,
Invoice.ContractName,
Invoice.ContractCode,
ISNULL(ConfirmInfo.TotalProgress,0) as LastProgress,--上期末累计确认比例
ISNULL(ConfirmInfo.TotalValue,0) as LastValue,--上期末累计确认金额
case when ISNULL(Invoice.Amount,0)-ISNULL(ConfirmInfo.TotalValue,0)<=0 then 0 else 
ISNULL(Invoice.Amount,0)-ISNULL(ConfirmInfo.TotalValue,0) end as CurrentValue--本期确认金额
from (select * from S_C_Invoice  where ID='{0}') as Invoice
outer apply(select top 1 * from S_EP_IncomeInvoiceConfirm a where Invoice.ID=a.InvoiceID order by a.ID desc) ConfirmInfo
) as a
", item.GetValue("ID"));
                var dt = SQLDB.ExecuteDataTable(sql);
                if (dt.Rows.Count == 0)
                {
                    throw new Formula.Exceptions.BusinessValidationException(string.Format("项目【{0}】没有找到指定的开票信息，无法进行确认", item.GetValue("Name")));
                }
                var dic = FormulaHelper.DataRowToDic(dt.Rows[0]);

                var dicConfirm = new Dictionary<string, object>();
                dicConfirm.SetValue("ID", FormulaHelper.CreateGuid());
                dicConfirm.SetValue("InvoiceID", dic.GetValue("ID"));
                dicConfirm.SetValue("Contract", dic.GetValue("ContractInfoID"));
                dicConfirm.SetValue("ContractName", dic.GetValue("ContractName"));
                dicConfirm.SetValue("ContractCode", dic.GetValue("ContractCode"));
                dicConfirm.SetValue("PayerUnit", dic.GetValue("PayerUnit"));
                dicConfirm.SetValue("InvoiceValue", dic.GetValue("InvoiceValue"));
                dicConfirm.SetValue("InvoiceType", dic.GetValue("InvoiceType"));
                dicConfirm.SetValue("LastProgress", dic.GetValue("LastProgress"));
                dicConfirm.SetValue("LastValue", dic.GetValue("LastValue"));
                dicConfirm.SetValue("TotalProgress", dic.GetValue("TotalProgress"));
                dicConfirm.SetValue("TotalValue", dic.GetValue("TotalValue"));
                dicConfirm.SetValue("CurrentValue", dic.GetValue("CurrentValue"));
                dicConfirm.SetValue("TaxRate", dic.GetValue("TaxRate"));
                dicConfirm.SetValue("TaxValue", dic.GetValue("TaxValue"));
                dicConfirm.SetValue("ClearValue", dic.GetValue("ClearValue"));
                dicConfirm.SetValue("ApplyUser", CurrentUserInfo.UserID);
                dicConfirm.SetValue("ApplyUserName", CurrentUserInfo.UserName);
                dicConfirm.SetValue("ApplyDate", DateTime.Now.ToString("yyyy-MM-dd"));
                dicConfirm.SetValue("ConfirmDate", DateTime.Now.ToString("yyyy-MM-dd"));
                dicConfirm.SetValue("IncomeUnitID", dic.GetValue("IncomeUnit"));
                dicConfirm.SetValue("FlowPhase", "End");
                #endregion
                mainSql += dicConfirm.CreateInsertSql(this.SQLDB, "S_EP_IncomeInvoiceConfirm", dicConfirm.GetValue("ID"));
            }
            this.SQLDB.ExecuteNonQuery(mainSql);
            return Json("");
        }
    }
}
