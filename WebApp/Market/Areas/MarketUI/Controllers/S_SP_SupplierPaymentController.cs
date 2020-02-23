using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Market.Logic.Domain;
using Formula.Helper;
using Formula;
using System.Data;
using System.Data.Entity;
using Base.Logic.BusinessFacade;
using Config;
using Config.Logic;
using Expenses.Logic;
using Formula.Exceptions;
using Base.Logic.Domain;
using Expenses.Logic.Domain;

namespace Market.Areas.MarketUI.Controllers
{
    public class SupplierPaymentController : MarketFormContorllor<S_SP_Payment>
    {
        private string paymentid = "";

        protected override void BeforeSave(Dictionary<string, string> dic, Base.Logic.Domain.S_UI_Form formInfo, bool isNew)
        {
            if (string.IsNullOrEmpty(dic.GetValue("PaymentDate")))
            {
                throw new Formula.Exceptions.BusinessValidationException("请填写付款日期");
            }
            CostFO.ValidatePeriodIsClosed(Convert.ToDateTime(dic.GetValue("PaymentDate")));
            Expenses.Logic.BusinessFacade.DataInterfaceFo.ValidateDataSyn("S_SP_Payment", dic.GetValue("ID"));
            var entity = this.GetEntityByID(dic["ID"]) ?? new S_SP_Payment();
            this.UpdateEntity(entity, dic);
            entity.Validate();
            paymentid = dic["ID"];
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
                var paymentValue = String.IsNullOrEmpty(dic.GetValue("PaymentValue")) ? 0m : Convert.ToDecimal(dic.GetValue("PaymentValue"));
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
                if (sumRelation > paymentValue)
                {
                    throw new Formula.Exceptions.BusinessValidationException("对应的发票金额总和不能大于本次付款金额");
                }                
            }
            else if (subTableName == "AcceptanceBill")
            {
                var sumRelation = 0M;
                var ids = new List<string>();
                var paymentValue = String.IsNullOrEmpty(dic.GetValue("PaymentValue")) ? 0m : Convert.ToDecimal(dic.GetValue("PaymentValue"));
                foreach (var item in detailList)
                {
                    var relationValue = String.IsNullOrEmpty(item.GetValue("Amount")) ? 0m : Convert.ToDecimal(item.GetValue("Amount"));
                    sumRelation += relationValue;
                    ids.Add(item.GetValue("AcceptanceBillID"));
                }
                if (sumRelation > paymentValue)
                {
                    throw new Formula.Exceptions.BusinessValidationException("承兑汇票金额总和不能大于本次付款的金额");
                }

                //修改承兑汇票支付状态
                var mentity = FormulaHelper.GetEntities<MarketEntities>();
                mentity.Set<S_C_AcceptanceBill>().Where(a => ids.Contains(a.ID)).Update(a => { a.State = "AlreadyPaid"; });
            }
            else if (subTableName == "CostInfo")
            {
                var paymentValue = String.IsNullOrEmpty(dic.GetValue("PaymentValue")) ? 0m : Convert.ToDecimal(dic.GetValue("PaymentValue"));
                var sumRelation = 0M;
                foreach (var item in detailList)
                {
                    var relationValue = String.IsNullOrEmpty(item.GetValue("CostValue")) ? 0m : Convert.ToDecimal(item.GetValue("CostValue"));
                    sumRelation += relationValue;
                }
                if (sumRelation > paymentValue)
                {
                    throw new Formula.Exceptions.BusinessValidationException("成本明细总额不能大于实付总金额");
                }
            }
        }

        public override JsonResult Save()
        {
            var rtn = base.Save();
            var mentity = FormulaHelper.GetEntities<MarketEntities>();
            var model = mentity.S_SP_Payment.FirstOrDefault(t => t.ID == paymentid);
            if (model == null)
                return rtn;
            model.Save();

            mentity.SaveChanges();
            return rtn;
        }

        protected override void AfterSave(Dictionary<string, string> dic, S_UI_Form formInfo, bool isNew)
        {
            if (SysParams.Params.GetValue("SubContractConfirmMethod") == "PaymentConfirm")//委外登记确认
            {
                ToCostInfo(dic);
            }
        }

        private void ToCostInfo(Dictionary<string, string> entity)
        {
            if (string.IsNullOrEmpty(entity.GetValue("PaymentDate")))
            {
                throw new Formula.Exceptions.BusinessValidationException("请填写付款日期");
            }
            CostFO.ValidatePeriodIsClosed(Convert.ToDateTime(entity.GetValue("PaymentDate")));

            var sql = string.Format(@"select PaymentCostInfo.ID,
PaymentCostInfo.CostUnitID as CostUnit,
ISNULL(PaymentCostInfo.CostValue,0) as CostValue,
Payment.PaymentDate,
Payment.PaymentUser,
Payment.PaymentUserName,
isnull(SupplierContract.TaxRate,0) as TaxRate
from (select * from S_SP_Payment where ID='{0}') as Payment
inner join S_SP_Payment_CostInfo PaymentCostInfo on Payment.ID=PaymentCostInfo.S_SP_PaymentID
left join S_SP_SupplierContract SupplierContract on Payment.Contract = SupplierContract.ID ", entity.GetValue("ID"));
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
                sql = string.Format(@"delete from S_EP_CostInfo where RelateID='{0}' ", dic.GetValue("ID"));
                MarketSQLDB.ExecuteNonQuery(sql);
                costDetailDic.InsertDB(this.MarketSQLDB, "S_EP_CostInfo");
                var cbsInfo = new S_EP_CBSInfo(cbsInfoDic);
                cbsInfo.SummaryCostValue();

            }

        }

        protected override void BeforeDelete(string[] Ids)
        {
            var sql = string.Format(@"select PaymentCostInfo.ID,
PaymentCostInfo.CostUnitID as CostUnit,
ISNULL(PaymentCostInfo.CostValue,0) as CostValue,
Payment.ID as PaymentID,
Payment.PaymentDate,
Payment.PaymentUser,
Payment.PaymentUserName,
isnull(SupplierContract.TaxRate,0) as TaxRate
from (select * from S_SP_Payment where ID in ('{0}') ) as Payment
inner join S_SP_Payment_CostInfo PaymentCostInfo on Payment.ID=PaymentCostInfo.S_SP_PaymentID
left join S_SP_SupplierContract SupplierContract on Payment.Contract = SupplierContract.ID ", string.Join("','", Ids));
            var dicList = FormulaHelper.DataTableToListDic(MarketSQLDB.ExecuteDataTable(sql));
            foreach (var dic in dicList)
            {
                Expenses.Logic.BusinessFacade.DataInterfaceFo.ValidateDataSyn("S_SP_Payment", dic.GetValue("PaymentID"));
                if (string.IsNullOrEmpty(dic.GetValue("PaymentDate")))
                {
                    throw new Formula.Exceptions.BusinessValidationException("付款日期为空，请确认！");
                }
                CostFO.ValidatePeriodIsClosed(Convert.ToDateTime(dic.GetValue("PaymentDate")));

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

                var costDetailDic = new Dictionary<string, object>();

                sql = string.Format(@"delete from S_EP_CostInfo where RelateID='{0}' ", dic.GetValue("ID"));
                MarketSQLDB.ExecuteNonQuery(sql);
                var cbsInfo = new Expenses.Logic.Domain.S_EP_CBSInfo(cbsInfoDic);
                cbsInfo.SummaryCostValue();
            }

            var cons = this.BusinessEntities.Set<S_SP_Payment>().Where(t => Ids.Contains(t.ID))
                .Select(t => t.S_SP_SupplierContract).ToList();
            //承兑汇票
            var payMents = this.BusinessEntities.Set<S_SP_Payment>().Where(t => Ids.Contains(t.ID)).ToList();
            //修改承兑汇票支付状态            
            var acceptanceBillIDs = new List<string>();
            foreach (var payMent in payMents)
            {
                var billIDs = payMent.S_SP_Payment_AcceptanceBill.Select(a => a.AcceptanceBillID).ToList();
                var tmpList = this.BusinessEntities.Set<S_C_AcceptanceBill>().Where(a => billIDs.Contains(a.ID)).ToList();
                foreach (var tmp in tmpList)
                {
                    tmp.State = "UnPaid";
                }
            }

            this.BusinessEntities.Set<S_FC_CostInfo>().Delete(d => Ids.Contains(d.RelateID));
            foreach (var con in cons)
            {
                con.SumPaymentValue = con.S_SP_Payment.Where(a => !Ids.Contains(a.ID)).Sum(a => a.PaymentValue);
            }
            this.BusinessEntities.SaveChanges();

            var OADB = SQLHelper.CreateSqlHelper(ConnEnum.OfficeAuto);
            OADB.ExecuteNonQuery("Delete from S_FC_CostInfo where RelateID in ('" + string.Join("','", Ids) + "')");
        }

    }
}
