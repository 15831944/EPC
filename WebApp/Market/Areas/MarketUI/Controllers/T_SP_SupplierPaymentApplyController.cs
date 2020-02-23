using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Market.Logic.Domain;
using Config.Logic;
using Expenses.Logic;
using Formula.Exceptions;
using Base.Logic.Domain;
using Formula;
using Expenses.Logic.Domain;
using System.Data;

namespace Market.Areas.MarketUI.Controllers
{
    public class SupplierPaymentApplyController : MarketFormContorllor<T_SP_PaymentApply>
    {
        protected override void BeforeSave(Dictionary<string, string> dic, Base.Logic.Domain.S_UI_Form formInfo, bool isNew)
        {
            var entity = this.GetEntityByID(dic["ID"]) ?? new T_SP_PaymentApply();
            this.UpdateEntity(entity, dic);
            entity.Validate();
            base.BeforeSave(dic, formInfo, isNew);
        }

        protected override void BeforeSaveSubTable(Dictionary<string, string> dic, string subTableName, List<Dictionary<string, string>> detailList, Base.Logic.Domain.S_UI_Form formInfo)
        {
            if (SysParams.Params.GetValue("SubContractConfirmMethod") == "PaymentConfirm" && subTableName == "CostInfo")//委外付款确认
            {
                var costValue = 0m;
                var contractValue = 0m;
                foreach (var item in detailList)
                {
                    costValue = 0m;
                    decimal.TryParse(item.GetValue("CostValue"), out costValue);
                    if (costValue <= 0)
                    {
                        throw new Formula.Exceptions.BusinessValidationException("【付款金额】必须大于0！");
                    }
                    contractValue = 0m;
                    decimal.TryParse(item.GetValue("ContractValue"), out contractValue);
                    if (costValue > contractValue)
                    {
                        throw new Formula.Exceptions.BusinessValidationException("【付款金额】不能超过【合同金额】");
                    }
                }
            }

            if (subTableName == "InvoiceRelation")
            {
                var sumRelation = 0M;
                var ApplyValue = String.IsNullOrEmpty(dic.GetValue("ApplyValue")) ? 0m : Convert.ToDecimal(dic.GetValue("ApplyValue"));
                foreach (var item in detailList)
                {
                    var relationValue = String.IsNullOrEmpty(item.GetValue("RelationValue")) ? 0m : Convert.ToDecimal(item.GetValue("RelationValue"));
                    sumRelation += relationValue;
                    var remainRelationValue = String.IsNullOrEmpty(item.GetValue("RemainInvoiceValue")) ? 0m : Convert.ToDecimal(item.GetValue("RemainInvoiceValue"));
                    if (relationValue > remainRelationValue)
                    {
                        throw new Formula.Exceptions.BusinessValidationException("本次对应金额【" + relationValue + "】不能大于可对应金额【" + remainRelationValue + "】");
                    }
                }
                if (sumRelation > ApplyValue)
                {
                    throw new Formula.Exceptions.BusinessValidationException("对应的发票金额总和不能大于本次付款金额");
                }
            }
            else if (subTableName == "AcceptanceBill")
            {
                var applyValue = String.IsNullOrEmpty(dic.GetValue("ApplyValue")) ? 0m : Convert.ToDecimal(dic.GetValue("ApplyValue"));
                var billValue = String.IsNullOrEmpty(dic.GetValue("BillValue")) ? 0m : Convert.ToDecimal(dic.GetValue("BillValue"));
                if (billValue > applyValue)
                {
                    throw new Formula.Exceptions.BusinessValidationException("承兑汇票金额总和不能大于本次申请付款的金额");
                }

                foreach (var detail in detailList)
                {
                    string billID = detail.GetValue("AcceptanceBillID");
                    string spID = dic.GetValue("ID");
                    if (BusinessEntities.Set<T_SP_PaymentApply_AcceptanceBill>().Any(a => billID == a.AcceptanceBillID && a.T_SP_PaymentApplyID != spID))
                    {
                        throw new Formula.Exceptions.BusinessValidationException("汇票号【" + detail.GetValue("SerialNumber") + "】已经被流程被其他支付申请流程占用");
                    }
                }
                
            }
        }

        protected override void OnFlowEnd(T_SP_PaymentApply entity, Workflow.Logic.Domain.S_WF_InsTaskExec taskExec, Workflow.Logic.Domain.S_WF_InsDefRouting routing)
        {
            if (entity != null)
            {
                if ((entity.BillValue ?? 0) > (entity.PaymentValue ?? 0))
                {
                    throw new Formula.Exceptions.BusinessValidationException("承兑汇票金额总和不能大于本次实际付款的金额");
                }

                entity.SavePayment();
                if (SysParams.Params.GetValue("SubContractConfirmMethod") == "PaymentConfirm")//委外付款确认
                {
                    ToCostInfo(entity);
                }
            }
            this.BusinessEntities.SaveChanges();
        }

        public JsonResult GetCostUnitInfo(string SupplierContractID)
        {
            var dt = this.MarketSQLDB.ExecuteDataTable(String.Format("select * from S_SP_SupplierContract_ContractSplit where S_SP_SupplierContractID='{0}' ", SupplierContractID));
            if (dt.Rows.Count == 0)
            {
                return Json("");
            }
            return Json(dt);
        }

        private void ToCostInfo(T_SP_PaymentApply entity)
        {
            if (entity.PaymentDate == null)
            {
                throw new Formula.Exceptions.BusinessValidationException("请填写付款日期");
            }
            CostFO.ValidatePeriodIsClosed((DateTime)entity.PaymentDate);

            var sql = string.Format(@"select PaymentApplyCostInfo.ID,
ISNULL(PaymentApplyCostInfo.CostValue,0) as CostValue,
PaymentApply.PaymentDate,
PaymentApply.PaymentUser,
PaymentApply.PaymentUserName,
isnull(SupplierContract.TaxRate,0) as TaxRate
from (select * from T_SP_PaymentApply where ID='{0}') as PaymentApply
inner join T_SP_PaymentApply_CostInfo PaymentApplyCostInfo on PaymentApply.ID=PaymentApplyCostInfo.T_SP_PaymentApplyID
left join S_SP_SupplierContract SupplierContract on PaymentApply.Contract = SupplierContract.ID", entity.ID);
            var dicList = FormulaHelper.DataTableToListDic(MarketSQLDB.ExecuteDataTable(sql));
            foreach (var dic in dicList)
            {
                var dt = MarketSQLDB.ExecuteDataTable(string.Format(@"select * from S_EP_CostUnit where ID='{0}' ", dic.GetValue("CostUnit")));
                if (dt.Rows.Count == 0)
                {
                    throw new Formula.Exceptions.BusinessValidationException("没有找到指定的成本单元，无法确认成本");
                }
                var unitDic = FormulaHelper.DataRowToDic(dt.Rows[0]);

                dt = MarketSQLDB.ExecuteDataTable(string.Format(@"select * from S_EP_CBSNode where ID='{0}' ", unitDic.GetValue("CBSNodeID")));
                if (dt.Rows.Count == 0)
                {
                    throw new Formula.Exceptions.BusinessValidationException("没有找到成本单元对应的CBS节点，无法确认成本");
                }
                var unitCBSNode = FormulaHelper.DataRowToDic(dt.Rows[0]);

                dt = MarketSQLDB.ExecuteDataTable(string.Format(@"select * from S_EP_CBSInfo where ID='{0}' ", unitDic.GetValue("CBSInfoID")));
                if (dt.Rows.Count == 0)
                {
                    throw new Formula.Exceptions.BusinessValidationException("没有找到成本单元对应的核算项目，无法确认成本");
                }
                var cbsInfoDic = FormulaHelper.DataRowToDic(dt.Rows[0]);

                var costValue = string.IsNullOrEmpty(dic.GetValue("CostValue")) ? 0m : Convert.ToDecimal(dic.GetValue("CostValue"));
                var taxRate = string.IsNullOrEmpty(dic.GetValue("TaxRate")) ? 0m : Convert.ToDecimal(dic.GetValue("TaxRate"));
                var taxValue = costValue / (1 + taxRate) * taxRate;
                var clearCostValue = costValue - taxValue;

                var costDetailDic = new Dictionary<string, object>();
                var subjectDt = this.MarketSQLDB.ExecuteDataTable(String.Format(@"select * from S_EP_CBSNode with(nolock)
where FullID like '{0}%' order by FullID", unitCBSNode.GetValue("FullID")));
                var subjectNode = subjectDt.AsEnumerable().FirstOrDefault(c => c["SubjectType"] != null && c["SubjectType"] != DBNull.Value
                   && c["SubjectType"].ToString() == SubjectType.SubContractCost.ToString());

                if (subjectNode == null) throw new BusinessException("未找到SubjectType为【" + SubjectType.SubContractCost.ToString() + "】的节点");

                var costState = "Finish";
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
                var costDate = String.IsNullOrEmpty(dic.GetValue("PaymentDate")) ? DateTime.Now : Convert.ToDateTime(dic.GetValue("PaymentDate"));
                costDetailDic.SetValue("CostDate", costDate);
                costDetailDic.SetValue("BelongYear", costDate.Year);
                costDetailDic.SetValue("BelongMonth", costDate.Month);
                costDetailDic.SetValue("BelongQuarter", (costDate.Month - 1) / 3 + 1);
                costDetailDic.SetValue("TotalPrice", costValue);
                costDetailDic.SetValue("TaxRate", taxRate);
                costDetailDic.SetValue("TaxValue", taxValue);
                costDetailDic.SetValue("ClearValue", clearCostValue);
                costDetailDic.SetValue("BelongDept", unitCBSNode.GetValue("ChargerDept"));
                costDetailDic.SetValue("BelongDeptName", unitCBSNode.GetValue("ChargerDeptName"));
                costDetailDic.SetValue("BelongUser", unitCBSNode.GetValue("ChargerUser"));
                costDetailDic.SetValue("BelongUserName", unitCBSNode.GetValue("ChargerUserName"));
                costDetailDic.SetValue("State", costState);
                costDetailDic.SetValue("Status", costState);
                costDetailDic.SetValue("CreateUser", dic.GetValue("PaymentUser"));
                costDetailDic.SetValue("CreateUserName", dic.GetValue("PaymentUserName"));
                costDetailDic.SetValue("CreateDate", DateTime.Now);
                costDetailDic.InsertDB(this.MarketSQLDB, "S_EP_CostInfo");
                var cbsInfo = new S_EP_CBSInfo(cbsInfoDic);
                cbsInfo.SummaryCostValue();

            }

        }
    }
}
