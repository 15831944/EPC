using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Config;
using Config.Logic;
using Formula.Helper;
using MvcAdapter;
using Expenses.Logic;
using Expenses.Logic.Domain;
using Formula;
using System.Data;
using Formula.Exceptions;
using Base.Logic.Domain;
using Workflow.Logic.Domain;

namespace Expenses.Areas.Cost.Controllers
{
    public class SubInvoiceConfirmController : ExpensesFormController<S_EP_InvoiceConfirm>
    {
        public JsonResult GetList(QueryBuilder qb, string DetailID)
        {
            string sql = String.Format(@"select FinalConfirm.ID as FinalID,ISNULL(InvoiceConfirmRevert.ID,'0') as InvoiceConfirmRevertID,InvoiceConfirm.* 
from (select * from S_EP_InvoiceConfirm where InvoiceID='{0}')InvoiceConfirm 
left join (select top 1 ID from S_EP_InvoiceConfirm where InvoiceID='{0}' order by ID desc) FinalConfirm on InvoiceConfirm.ID=FinalConfirm.ID 
outer apply( select top 1 * from S_EP_InvoiceConfirmRevert a where InvoiceConfirm.ID=a.InvoiceConfirmID) InvoiceConfirmRevert 
order by ID", DetailID);
            var data = this.SQLDB.ExecuteDataTable(sql);
            return Json(data);
        }

        public JsonResult ValidateStart(string DetailID)
        {
            CheckSubContractConfirmMethod();
            ValidateInvoice(DetailID);
            return Json("");
        }

        protected override void BeforeSave(Dictionary<string, string> dic, S_UI_Form formInfo, bool isNew)
        {
            CheckSubContractConfirmMethod();

            if (String.IsNullOrEmpty(dic.GetValue("ConfirmDate")))
            {
                throw new Formula.Exceptions.BusinessValidationException("必须填写完成日期");
            }
            var dateTime = String.IsNullOrEmpty(dic.GetValue("ConfirmDate")) ? DateTime.Now : Convert.ToDateTime(dic.GetValue("ConfirmDate"));
            CostFO.ValidatePeriodIsClosed(dateTime);
            ValidateInvoice(dic.GetValue("InvoiceID"));
        }

        protected override void AfterSave(Dictionary<string, string> dic, S_UI_Form formInfo, bool isNew)
        {
            CheckSubContractConfirmMethod();
            ToCostInfo(dic);
        }

        /// <summary>
        /// 成本核算
        /// </summary>
        /// <param name="dic"></param>
        private void ToCostInfo(Dictionary<string, string> dic)
        {
            var invoiceEntity = this.GetDataDicByID("S_SP_Invoice", dic.GetValue("InvoiceID"));
            if (invoiceEntity == null)
            {
                throw new Formula.Exceptions.BusinessValidationException("没有找到指定的委外开票信息，无法进行委外开票确认");
            }
            var unitDic = this.GetDataDicByID("S_EP_CostUnit", dic.GetValue("CostUnitID"));
            if (unitDic == null)
            {
                throw new Formula.Exceptions.BusinessValidationException("没有找到指定的成本单元，无法确认成本");
            }
            var unitCBSNode = this.GetDataDicByID("S_EP_CBSNode", unitDic.GetValue("CBSNodeID"), Config.ConnEnum.Market, true);
            if (unitCBSNode == null)
            {
                throw new Formula.Exceptions.BusinessValidationException("没有找到成本单元对应的CBS节点，无法确认成本");
            }
            var cbsInfoDic = this.GetDataDicByID("S_EP_CBSInfo", unitDic.GetValue("CBSInfoID"));
            if (cbsInfoDic == null)
            {
                throw new Formula.Exceptions.BusinessValidationException("没有找到成本单元对应的核算项目，无法确认成本");
            }

            var costDetailDic = new Dictionary<string, object>();
            var subjectDt = this.SQLDB.ExecuteDataTable(String.Format(@"select * from S_EP_CBSNode with(nolock)
where FullID like '{0}%' order by FullID", unitCBSNode.GetValue("FullID"))); 
            var costState = "Finish";
            var subjectNode = subjectDt.AsEnumerable().FirstOrDefault(c => c["SubjectType"] != null && c["SubjectType"] != DBNull.Value
                                       && c["SubjectType"].ToString() == SubjectType.SubContractCost.ToString());

            if (subjectNode == null) throw new BusinessException("未找到SubjectType为【" + SubjectType.SubContractCost.ToString() + "】的节点");
            costDetailDic.SetValue("Name", subjectNode["Name"]);
            costDetailDic.SetValue("CBSFullCode", subjectNode["FullCode"]);
            costDetailDic.SetValue("CBSNodeID", subjectNode["ID"]);
            costDetailDic.SetValue("CBSNodeFullID", subjectNode["FullID"]);

            if (String.IsNullOrEmpty(costDetailDic.GetValue("Name")))
            {
                costDetailDic.SetValue("Name", "采购分包费");
            }
            costDetailDic.SetValue("Code", subjectNode["Code"]);
            if (String.IsNullOrEmpty(costDetailDic.GetValue("Code")))
            {
                costDetailDic.SetValue("Code", unitCBSNode.GetValue("Code"));
            }
            costDetailDic.SetValue("CostType", SubjectType.SubContractCost.ToString());
            costDetailDic.SetValue("CBSInfoID", unitDic.GetValue("CBSInfoID"));
            costDetailDic.SetValue("CostUnitID", unitDic.GetValue("ID"));
            costDetailDic.SetValue("SubjectCode", subjectNode["SubjectCode"]);
            costDetailDic.SetValue("RelateID", dic.GetValue("ID"));
            var confirmDate = Convert.ToDateTime(dic.GetValue("ConfirmDate"));
            costDetailDic.SetValue("CostDate", confirmDate);
            costDetailDic.SetValue("BelongYear", confirmDate.Year);
            costDetailDic.SetValue("BelongMonth", confirmDate.Month);
            costDetailDic.SetValue("BelongQuarter", (confirmDate.Month + 2) / 3);
            costDetailDic.SetValue("TotalPrice", dic.GetValue("CurrentValue"));
            costDetailDic.SetValue("TaxRate", dic.GetValue("TaxRate"));
            costDetailDic.SetValue("TaxValue", dic.GetValue("TaxValue"));
            costDetailDic.SetValue("ClearValue", dic.GetValue("ClearValue"));
            costDetailDic.SetValue("BelongDept", unitCBSNode.GetValue("ChargerDept"));
            costDetailDic.SetValue("BelongDeptName", unitCBSNode.GetValue("ChargerDeptName"));
            costDetailDic.SetValue("BelongUser", unitCBSNode.GetValue("ChargerUser"));
            costDetailDic.SetValue("BelongUserName", unitCBSNode.GetValue("ChargerUserName"));
            costDetailDic.SetValue("State", costState);
            costDetailDic.SetValue("Status", costState);
            costDetailDic.SetValue("CreateUser", dic.GetValue("ApplyUser"));
            costDetailDic.SetValue("CreateUserName", dic.GetValue("ApplyUserName"));
            costDetailDic.SetValue("CreateDate", DateTime.Now);
            costDetailDic.InsertDB(this.SQLDB, "S_EP_CostInfo");
            var cbsInfo = new S_EP_CBSInfo(cbsInfoDic);
            cbsInfo.SummaryCostValue();

        }

        //批量确认开票
        public JsonResult BatchConfirm()
        {
            var totalProgress = 0m;
            decimal.TryParse(Request["ConfirmProgress"], out totalProgress);
            if (totalProgress <= 0)
            {
                throw new Formula.Exceptions.BusinessValidationException("确认比例必须大于0！");
            }
            var confirmDate = DateTime.Now.Date;
            if (!DateTime.TryParse(Request["ConfirmDate"], out confirmDate))
            {
                throw new Formula.Exceptions.BusinessValidationException("请选择确认日期！");
            }
            CostFO.ValidatePeriodIsClosed(confirmDate);

            var invoiceValue = 0m;
            var lastProgress = 0m;
            var lastValue = 0m;
            var totalValue = 0m;
            var currentValue = 0m;
            var taxRate = 0m;
            var clearValue = 0m;
            var taxValue = 0m;
            var invoiceConfirmID = string.Empty;
            var list = JsonHelper.ToList(Request["ListData"]);
            foreach (var item in list)
            {
                #region 验证数据，变量赋值
                //上期开票确认信息
                var sql = string.Format(@"select Invoice.ID,
ISNULL(Invoice.Amount,0) as InvoiceValue,
Invoice.InvoiceReciever as ChargerUser,
Invoice.InvoiceRecieverName as ChargerUserName,
Invoice.Supplier,
Invoice.SupplierName,
SupplierContract.SupplierDept as ChargerDept,
SupplierContract.SupplierDeptName as ChargerDeptName,
SupplierContract.ProjectInfo,
SupplierContract.ProjectInfoName,
SupplierContract.Name as SubContracName,
SupplierContract.SerialNumber as SubContracCode,
ISNULL(SupplierContract.TaxRate,0) as TaxRate,
ConfirmInfo.ID as InvoiceConfirmID,
ISNULL(ConfirmInfo.TotalProgress,0) as LastProgress,
ISNULL(ConfirmInfo.TotalValue,0) as LastValue,
S_EP_CostUnit.ID as CostUnitID,
S_EP_CostUnit.Name,
S_EP_CostUnit.Code,
S_EP_CostUnit.CBSNodeID,
S_EP_CostUnit.CBSInfoID,
S_EP_CBSInfo.Name as CBSInfoName,
S_EP_CBSInfo.Code as CBSInfoCode
from (select * from S_SP_Invoice where ID='{0}') as Invoice
left join S_SP_SupplierContract SupplierContract on Invoice.SupplierContract = SupplierContract.ID
outer apply(select top 1 * from S_EP_InvoiceConfirm a where Invoice.ID=a.InvoiceID order by a.ID desc) ConfirmInfo
left join S_EP_CostUnit on S_EP_CostUnit.ID=Invoice.CostUnit
left join S_EP_CBSInfo on S_EP_CostUnit.CBSInfoID=S_EP_CBSInfo.ID", item.GetValue("ID"));
                var dt = SQLDB.ExecuteDataTable(sql);
                if (dt.Rows.Count == 0)
                {
                    throw new Formula.Exceptions.BusinessValidationException(string.Format("项目【{0}】没有找到指定的委外开票信息，无法进行确认", item.GetValue("Name")));
                }
                var dic = FormulaHelper.DataRowToDic(dt.Rows[0]);

                lastProgress = 0m;
                lastValue = 0m;
                //非首次确认
                if (!string.IsNullOrEmpty(dic.GetValue("InvoiceConfirmID")))
                {
                    lastProgress = string.IsNullOrEmpty(dic.GetValue("LastProgress")) ? 0m : Convert.ToDecimal(dic.GetValue("LastProgress"));
                    //是否确认至100%
                    if (lastProgress >= 1)
                    {
                        continue;
                    }
                    lastValue = string.IsNullOrEmpty(dic.GetValue("LastValue")) ? 0m : Convert.ToDecimal(dic.GetValue("LastValue"));
                }

                //【本期末累计确认比例】是否大于 【上期末累计确认比例】
                if (totalProgress <= lastProgress)
                {
                    throw new Formula.Exceptions.BusinessValidationException("【本期末累计确认比例】必须大于【上期末累计确认比例】");
                }

                //【发票金额】不能为0
                invoiceValue = string.IsNullOrEmpty(dic.GetValue("InvoiceValue")) ? 0m : Convert.ToDecimal(dic.GetValue("InvoiceValue"));
                if (invoiceValue == 0)
                {
                    throw new Formula.Exceptions.BusinessValidationException(string.Format("项目【{0}】的【开票金额】为0，不能确认！", item.GetValue("Name")));
                }
                #endregion

                #region 计算【本期末累计确认金额】【本期确认金额】【税金】【去税金额】
                totalValue = invoiceValue * totalProgress;//本期末累计确认金额
                currentValue = totalValue - lastValue;//本期确认金额
                taxRate = string.IsNullOrEmpty(dic.GetValue("TaxRate")) ? 0m : Convert.ToDecimal(dic.GetValue("TaxRate"));//税率
                taxValue = currentValue * taxRate / (1 + taxRate);//税金  
                clearValue = currentValue / (1 + taxRate);//去税金额

                totalValue = Math.Round(totalValue, 2);
                currentValue = Math.Round(currentValue, 2);
                taxValue = Math.Round(taxValue, 2);
                clearValue = Math.Round(clearValue, 2);
                #endregion

                #region 保存开票确认信息
                var dicConfirm = new Dictionary<string, object>();
                dicConfirm.SetValue("InvoiceID", item.GetValue("ID"));
                dicConfirm.SetValue("InvoiceValue", invoiceValue);
                dicConfirm.SetValue("LastProgress", lastProgress);
                dicConfirm.SetValue("LastValue", lastValue);
                dicConfirm.SetValue("TotalProgress", totalProgress);
                dicConfirm.SetValue("TotalValue", totalValue);
                dicConfirm.SetValue("CurrentValue", currentValue);
                dicConfirm.SetValue("ApplyUser", CurrentUserInfo.UserID);
                dicConfirm.SetValue("ApplyUserName", CurrentUserInfo.UserName);
                dicConfirm.SetValue("ApplyDate", DateTime.Now);
                dicConfirm.SetValue("TaxRate", taxRate);
                dicConfirm.SetValue("TaxValue", taxValue);
                dicConfirm.SetValue("ClearValue", clearValue);
                dicConfirm.SetValue("ConfirmDate", confirmDate);
                dicConfirm.SetValue("CBSInfoID", item.GetValue("CBSInfoID"));
                dicConfirm.SetValue("CBSInfoName", dic.GetValue("CBSInfoName"));
                dicConfirm.SetValue("CBSInfoCode", dic.GetValue("CBSInfoCode"));
                dicConfirm.SetValue("CBSNodeID", item.GetValue("CBSNodeID"));
                dicConfirm.SetValue("CostUnitID", item.GetValue("CostUnitID"));
                //dicConfirm.SetValue("InvoiceValue", invoiceValue);
                invoiceConfirmID = dicConfirm.InsertDB(SQLDB, "S_EP_InvoiceConfirm");
                #endregion

                //continue;

                #region 核算成本
                var unitDic = this.GetDataDicByID("S_EP_CostUnit", item.GetValue("CostUnitID"));
                if (unitDic == null)
                {
                    throw new Formula.Exceptions.BusinessValidationException("没有找到指定的成本单元，无法确认成本！");
                }
                var unitCBSNode = this.GetDataDicByID("S_EP_CBSNode", unitDic.GetValue("CBSNodeID"));
                if (unitCBSNode == null)
                {
                    throw new Formula.Exceptions.BusinessValidationException("没有找到成本单元对应的CBS节点，无法确认成本！");
                }
                var cbsInfoDic = this.GetDataDicByID("S_EP_CBSInfo", unitDic.GetValue("CBSInfoID"));
                if (cbsInfoDic == null)
                {
                    throw new Formula.Exceptions.BusinessValidationException("没有找到成本单元对应的核算项目，无法确认成本！");
                }

                var costDetailDic = new Dictionary<string, object>();
                var subjectDt = this.SQLDB.ExecuteDataTable(String.Format(@"select * from S_EP_CBSNode with(nolock)
where FullID like '{0}%' order by FullID", unitCBSNode.GetValue("FullID")));
                var costState = "Finish";

                var subjectNode = subjectDt.AsEnumerable().FirstOrDefault(c => c["SubjectType"] != null && c["SubjectType"] != DBNull.Value
                                        && c["SubjectType"].ToString() == SubjectType.SubContractCost.ToString());

                if (subjectNode == null) throw new BusinessException("未找到SubjectType为【" + SubjectType.SubContractCost.ToString() + "】的节点");
                costDetailDic.SetValue("Name", subjectNode["Name"]);
                costDetailDic.SetValue("CBSFullCode", subjectNode["FullCode"]);
                costDetailDic.SetValue("CBSNodeID", subjectNode["ID"]);
                costDetailDic.SetValue("CBSNodeFullID", subjectNode["FullID"]);

                if (String.IsNullOrEmpty(costDetailDic.GetValue("Name")))
                {
                    costDetailDic.SetValue("Name", "采购分包费");
                }
                costDetailDic.SetValue("Code", subjectNode["Code"]);
                if (String.IsNullOrEmpty(costDetailDic.GetValue("Code")))
                {
                    costDetailDic.SetValue("Code", unitCBSNode.GetValue("Code"));
                }
                costDetailDic.SetValue("CostType", SubjectType.SubContractCost.ToString());
                costDetailDic.SetValue("CBSInfoID", unitDic.GetValue("CBSInfoID"));
                costDetailDic.SetValue("CostUnitID", unitDic.GetValue("ID"));
                costDetailDic.SetValue("SubjectCode", subjectNode["SubjectCode"]);
                costDetailDic.SetValue("RelateID", invoiceConfirmID);
                costDetailDic.SetValue("CostDate", confirmDate);
                costDetailDic.SetValue("BelongYear", confirmDate.Year);
                costDetailDic.SetValue("BelongMonth", confirmDate.Month);
                costDetailDic.SetValue("BelongQuarter", (confirmDate.Month + 2) / 3);
                costDetailDic.SetValue("TotalPrice", currentValue);
                costDetailDic.SetValue("TaxRate", taxRate);
                costDetailDic.SetValue("TaxValue", taxValue);
                costDetailDic.SetValue("ClearValue", clearValue);
                costDetailDic.SetValue("BelongDept", unitCBSNode.GetValue("ChargerDept"));
                costDetailDic.SetValue("BelongDeptName", unitCBSNode.GetValue("ChargerDeptName"));
                costDetailDic.SetValue("BelongUser", unitCBSNode.GetValue("ChargerUser"));
                costDetailDic.SetValue("BelongUserName", unitCBSNode.GetValue("ChargerUserName"));
                costDetailDic.SetValue("State", costState);
                costDetailDic.SetValue("Status", costState);
                costDetailDic.SetValue("CreateUser", CurrentUserInfo.UserID);
                costDetailDic.SetValue("CreateUserName", CurrentUserInfo.UserName);
                costDetailDic.SetValue("CreateDate", DateTime.Now);
                costDetailDic.InsertDB(this.SQLDB, "S_EP_CostInfo");
                var cbsInfo = new S_EP_CBSInfo(cbsInfoDic);
                cbsInfo.SummaryCostValue();


                #endregion

            }
            return Json("");
        }


        /// <summary>
        /// 验证委外开票信息和开票确认信息
        /// </summary>
        /// <param name="id">ID</param>
        private void ValidateInvoice(string id)
        {
            var dic = this.GetDataDicByID("S_SP_Invoice", id);
            if (dic == null)
            {
                throw new Formula.Exceptions.BusinessValidationException("没有找到指定的委外开票信息，无法进行确认");
            }
            var data = this.SQLDB.ExecuteDataTable(string.Format(@"select InvoiceConfirm.*,InvoiceConfirmRevert.ID as InvoiceConfirmRevertID 
from(select top 1 * from S_EP_InvoiceConfirm where InvoiceID = '{0}' order by ID desc)InvoiceConfirm
left join S_EP_InvoiceConfirmRevert InvoiceConfirmRevert on InvoiceConfirm.ID = InvoiceConfirmRevert.InvoiceConfirmID ", id));
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

        }

        /// <summary>
        /// 验证外委成本确认方式
        /// </summary>
        private void CheckSubContractConfirmMethod()
        {
            if (SysParams.Params.GetValue("SubContractConfirmMethod") != "InvoiceConfirm")//委外开票确认
            {
                throw new Formula.Exceptions.BusinessValidationException("【按开票确认】不是系统设置的外委成本确认方式！");
            }

        }

    }
}
