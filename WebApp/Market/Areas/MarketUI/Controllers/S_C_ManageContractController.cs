using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Market.Logic.Domain;
using Market.Logic;
using Project.Logic.Domain;
using Formula.Helper;
using Config.Logic;
using Formula;
using System.Data;
using Newtonsoft.Json;
using Formula.ImportExport;
using System.Text;
using Config;
using Base.Logic.Domain;
using Workflow.Logic;
using Base.Logic.Model.UI.Form;

namespace Market.Areas.MarketUI.Controllers
{
    public class ManageContractController : MarketFormContorllor<S_C_ManageContract>
    {
        protected override void BeforeSave(Dictionary<string, string> dic, S_UI_Form formInfo, bool isNew)
        {
            //付款方已经有在开票信息中则不能删除
            if (!isNew)
            {
                Expenses.Logic.BusinessFacade.DataInterfaceFo.ValidateDataSyn("S_C_ManageContract", dic.GetValue("ID"));

                string contractID = dic.GetValue("ID");
                var oldContract = this.BusinessEntities.Set<S_C_ManageContract>().Find(contractID);
                string oldCustomerIDs = oldContract.PayerUnit ?? "";
                string oldCustomerIDNames = oldContract.PayerUnitName ?? "";

                string customerIDs = dic.GetValue("PayerUnit") ?? "";

                var oldCustomerIDArr = oldCustomerIDs.Split(',').ToList();
                var oldCustomerNameArr = oldCustomerIDNames.Split(',').ToList();
                for (int i = 0; i < oldCustomerIDArr.Count; i++)
                {
                    string oldCustomerID = oldCustomerIDArr[i];
                    if (!customerIDs.Contains(oldCustomerID))
                    {
                        string customerName = oldCustomerID;
                        if (i < oldCustomerNameArr.Count)
                        {
                            customerName = oldCustomerNameArr[i];
                        }
                        if (BusinessEntities.Set<S_C_Invoice>().Any(a => a.PayerUnitID == oldCustomerID && a.ContractInfoID == contractID))
                        {
                            throw new Formula.Exceptions.BusinessException("付款方【" + customerName + "】已经有开票登记信息,不能删除");
                        }
                        if (BusinessEntities.Set<T_C_InvoiceApply>().Any(a => a.PayerUnit == oldCustomerID && a.Contract == contractID))
                        {
                            throw new Formula.Exceptions.BusinessException("付款方【" + customerName + "】已经有开票申请信息,不能删除");
                        }
                        if (BusinessEntities.Set<S_C_Receipt>().Any(a => a.CustomerID == oldCustomerID && a.ContractInfoID == contractID))
                        {
                            throw new Formula.Exceptions.BusinessException("付款方【" + customerName + "】已经有到款信息,不能删除");
                        }
                    }
                }

                //添加过补充协议的合同状态不能改成登记
                var hasSupp = this.BusinessEntities.Set<S_C_ManageContract_Supplementary>().Any(a => a.ContractInfoID == contractID);
                if (hasSupp && string.IsNullOrEmpty(dic.GetValue("SignDate")))
                    throw new Formula.Exceptions.BusinessException("添加过补充协议的合同必须有签约日期");
            }
            var PaymentDetail = JsonHelper.ToList(dic.GetValue("PaymentDetail"));
            if (PaymentDetail.Count == 0)
                throw new Formula.Exceptions.BusinessException("请选择付款方");
        }

        protected override void AfterGetData(DataTable dt, bool isNew, string upperVersionID)
        {
            if (isNew)
            {
                var orgService = Formula.FormulaHelper.GetService<IOrgService>();
                var orgList = orgService.GetOrgs(Config.Constant.OrgRootID);
                var rootOrg = orgList.FirstOrDefault(d => d.ID == Config.Constant.OrgRootID);
                SetDtValue(dt, "PartyB", rootOrg.ID);
                SetDtValue(dt, "PartyBName", rootOrg.Name);
            }
            else
            {
                var contractID = dt.Rows[0]["ID"].ToString();
                if (contractID != "" && contractID != null)
                {
                    var SupplierContracts = BusinessEntities.Set<S_SP_SupplierContract>().Where(a => a.ManagerContract == contractID).ToList();
                    var SupplierInvoices = BusinessEntities.Set<S_SP_Invoice>().Where(a => a.ManagerContract == contractID).ToList();
                    var SupplierPayments = BusinessEntities.Set<S_SP_Payment>().Where(a => a.ManagerContract == contractID).ToList();

                    SetDtValue(dt, "SupplierContractValue", SupplierContracts.Sum(a => a.ContractValue));
                    SetDtValue(dt, "SupplierInvoiceValue", SupplierInvoices.Sum(a => a.Amount));
                    SetDtValue(dt, "SupplierPaymentValue", SupplierPayments.Sum(a => a.PaymentValue));
                }
                if (dt.Rows[0]["ReceiptObj"] != DBNull.Value)
                {
                    var list = JsonHelper.ToList(dt.Rows[0]["ReceiptObj"].ToString());
                    foreach (var item in list)
                    {
                        if (!string.IsNullOrEmpty(item.GetValue("MileStoneID")))
                        {
                            var mileStoneID = item.GetValue("MileStoneID");
                            var projectEntities = FormulaHelper.GetEntities<ProjectEntities>();

                            var aboutMileStone = projectEntities.Set<S_P_MileStone>().Where(c => mileStoneID.Contains(c.ID));
                            if (aboutMileStone.Count() > 0)
                            {
                                string mileStoneNames = string.Join(",", aboutMileStone.Select(c => c.Name).Distinct().ToList());
                                var maxPlanFinishDate = aboutMileStone.Max(c => c.PlanFinishDate);
                                var maxFactFinishDate = aboutMileStone.Max(c => c.FactFinishDate);
                                item.SetValue("MileStoneName", mileStoneNames);
                                item.SetValue("MileStonePlanEndDate", maxPlanFinishDate);
                                item.SetValue("MileStoneFactEndDate", maxFactFinishDate);
                            }
                        }
                    }
                    SetDtValue(dt, "ReceiptObj", JsonHelper.ToJson(list));
                }
            }
        }

        protected override void AfterSave(Dictionary<string, string> dic, Base.Logic.Domain.S_UI_Form formInfo, bool isNew)
        {
            var contract = this.GetEntityByID(dic["ID"]);
            if (contract == null) contract = this.BusinessEntities.Set<S_C_ManageContract>().Create();
            this.UpdateEntity(contract, dic);
            contract.Save();
            contract.SynchContractProperties();
            var receiptObjList = contract.S_C_ManageContract_ReceiptObj.ToList();
            var dicList = new List<Dictionary<string, object>>();
            if (!string.IsNullOrEmpty(dic.GetValue("ReceiptObj")))
            {
                dicList = JsonHelper.ToList(dic.GetValue("ReceiptObj"));
            }
            foreach (var receiptObj in contract.S_C_ManageContract_ReceiptObj.ToList())
            {
                var dicItem = dicList.FirstOrDefault(a => a.GetValue("ID") == receiptObj.ID);
                if (dicItem != null)
                {
                    UpdateEntity(receiptObj, dicItem);
                }
                receiptObj.SummaryReceiptValue();
                receiptObj.ResetPlan();
                receiptObj.SyncSupplementary();
            }
            this.BusinessEntities.SaveChanges();

            #region 自动同步核算项目
            Expenses.Logic.CBSInfoFO.SynCBSInfo(FormulaHelper.ModelToDic<S_C_ManageContract>(contract), Expenses.Logic.SetCBSOpportunity.Contract);
            #endregion
        }

        protected override void BeforeSaveSubTable(Dictionary<string, string> dic, string subTableName, List<Dictionary<string, string>> detailList, Base.Logic.Domain.S_UI_Form formInfo)
        {
            if (subTableName == "ReceiptObj")
            {
                var sumReceiptObjValue = 0m;
                foreach (var item in detailList)
                {
                    sumReceiptObjValue += String.IsNullOrEmpty(item.GetValue("ReceiptValue")) ? 0m : Convert.ToDecimal(item.GetValue("ReceiptValue"));
                }
                var contractRMBValue = String.IsNullOrEmpty(dic.GetValue("ContractRMBAmount")) ? 0m : Convert.ToDecimal(dic.GetValue("ContractRMBAmount"));
                if (sumReceiptObjValue > contractRMBValue)
                {
                    throw new Formula.Exceptions.BusinessException("收款项金额总计不能大于合同结算金额");
                }
            }
            else if (subTableName == "ContractSplit")
            {
                var sumValue = 0m;
                foreach (var item in detailList)
                {
                    sumValue += String.IsNullOrEmpty(item.GetValue("SplitValue")) ? 0m : Convert.ToDecimal(item.GetValue("SplitValue"));
                }
                var contractRMBValue = String.IsNullOrEmpty(dic.GetValue("ContractRMBAmount")) ? 0m : Convert.ToDecimal(dic.GetValue("ContractRMBAmount"));
                if (sumValue > contractRMBValue)
                {
                    throw new Formula.Exceptions.BusinessException("合同分解金额总计不能大于结算人民币金额");
                }
            }
            else if (subTableName == "ProjectRelation")
            {
                var sumValue = 0m;
                foreach (var item in detailList)
                {
                    sumValue += String.IsNullOrEmpty(item.GetValue("ProjectValue")) ? 0m : Convert.ToDecimal(item.GetValue("ProjectValue"));
                }
                var contractRMBValue = String.IsNullOrEmpty(dic.GetValue("ContractRMBAmount")) ? 0m : Convert.ToDecimal(dic.GetValue("ContractRMBAmount"));
                if (sumValue > contractRMBValue)
                {
                    throw new Formula.Exceptions.BusinessException("关联项目金额总计不能大于结算人民币金额");
                }
            }
            else if (subTableName == "DeptRelation")
            {
                var sumValue = 0m;
                foreach (var item in detailList)
                {
                    sumValue += String.IsNullOrEmpty(item.GetValue("DeptValue")) ? 0m : Convert.ToDecimal(item.GetValue("DeptValue"));
                }
                var contractRMBValue = String.IsNullOrEmpty(dic.GetValue("ThisContractRMBAmount")) ? 0m : Convert.ToDecimal(dic.GetValue("ThisContractRMBAmount"));
                if (sumValue != contractRMBValue)
                {
                    throw new Formula.Exceptions.BusinessException("部门分解总额总计必须等于折合人民币金额");
                }
            }
        }

        protected override void BeforeSaveDetail(Dictionary<string, string> dic, string subTableName, Dictionary<string, string> detail, List<Dictionary<string, string>> detailList, S_UI_Form formInfo)
        {
            if (subTableName == "ReceiptObj")
            {
                var entity = this.GetEntityByID<S_C_ManageContract_ReceiptObj>(detail.GetValue("ID"));

                var receiptValue = String.IsNullOrEmpty(detail.GetValue("ReceiptValue")) ? 0m : Convert.ToDecimal(detail.GetValue("ReceiptValue"));
                //entity.ReceiptValue = receiptValue;
                if (entity != null)
                {
                    var sumInvoice = entity.S_C_Invoice_ReceiptObject.Sum(a => a.RelationValue);
                    var sumReceipt = entity.S_C_PlanReceipt.Sum(a => a.FactReceiptValue);
                    if (receiptValue < sumInvoice || receiptValue < sumReceipt)
                        throw new Formula.Exceptions.BusinessException("收款项金额不能小于已开票或已收款金额");
                }
            }
        }

        protected override void BeforeDelete(string[] Ids)
        {
            foreach (var Id in Ids)
            {
                Expenses.Logic.BusinessFacade.DataInterfaceFo.ValidateDataSyn("S_C_ManageContract", Id);
                var contract = this.GetEntityByID(Id);
                if (contract != null)
                {
                    var count = this.BusinessEntities.Set<T_C_ContractReview>().Count(c => c.ContractID == contract.ID);
                    if (count > 0)
                    {
                        throw new Formula.Exceptions.BusinessValidationException("已经发起评审的合同不能进行删除操作");
                    }
                    contract.Delete();
                }
            }
            this.BusinessEntities.SaveChanges();
        }

        public JsonResult SetContractState(string ListData, string State)
        {
            var list = Formula.Helper.JsonHelper.ToList(ListData);
            if (String.IsNullOrEmpty(State)) throw new Formula.Exceptions.BusinessValidationException("必须指定合同的状态");
            var sb = new System.Text.StringBuilder();
            foreach (var item in list)
            {
                var contract = this.GetEntityByID(item.GetValue("ID"));
                if (contract == null) continue;
                if (State == ContractState.Sign.ToString())
                {
                    if (contract.ContractState != ContractState.Pause.ToString() && contract.ContractState != ContractState.Terminate.ToString())
                    {
                        throw new Formula.Exceptions.BusinessValidationException(String.Format("合同编号为【{0}】的合同不能恢复为履行状态，请确认该合同是否暂停或终止", contract.SerialNumber));
                    }
                }
                if (contract.ContractState == ContractState.Regist.ToString() || contract.ContractState == ContractState.WaitSign.ToString())
                {
                    throw new Formula.Exceptions.BusinessValidationException(String.Format("合同编号为【{0}】的合同未签约，不能设置合同装填", contract.SerialNumber));
                }
                contract.ContractState = State;
            }
            this.BusinessEntities.SaveChanges();
            return Json("");
        }

        public JsonResult ValidateChange(string ContractID)
        {
            var contractInfo = this.GetEntityByID<S_C_ManageContract>(ContractID);
            if (!contractInfo.SignDate.HasValue)
            {
                throw new Formula.Exceptions.BusinessValidationException("尚未签约的合同不能进行合同变更，请直接通过编辑来修改未签订的合同");
            }
            var result = new Dictionary<string, object>();
            string sql = String.Format("select ID from T_C_ContractChange where ContractID='{0}' and FlowPhase='{1}'", ContractID, "Start");
            var dt = this.MarketSQLDB.ExecuteDataTable(sql);
            if (dt.Rows.Count > 0)
            {
                result.SetValue("ID", dt.Rows[0]["ID"]);
                return Json(result);
            }
            sql = String.Format("select Count(ID) from T_C_ContractChange where ContractID='{0}' and FlowPhase='{1}'", ContractID, "Processing");
            var obj = Convert.ToInt32(this.MarketSQLDB.ExecuteScalar(sql));
            if (obj > 0)
            {
                throw new Formula.Exceptions.BusinessValidationException("合同正在变更中，无法重复启动变更，请在变更结束后再启动变更");
            }
            Expenses.Logic.BusinessFacade.DataInterfaceFo.ValidateDataSyn("S_C_ManageContract", ContractID);
            return Json(result);
        }

        public JsonResult SetEngineeringInfo(string ContractInfoID)
        {
            var contractInfo = this.GetEntityByID(ContractInfoID);
            if (contractInfo == null) throw new Formula.Exceptions.BusinessException("未能找到指定的合同信息，无法启动立项");
            var engineering = this.GetEntityByID<S_I_Engineering>(contractInfo.EngineeringInfo);
            if (engineering == null)
            {
                engineering = new S_I_Engineering();
                engineering.ID = FormulaHelper.CreateGuid();
                engineering.Name = contractInfo.Name;
                engineering.Code = contractInfo.SerialNumber;
                engineering.CustomerInfo = contractInfo.PartyA;
                engineering.CustomerInfoName = contractInfo.PartyAName;
                engineering.MainDept = contractInfo.ProductionDept;
                engineering.MainDeptName = contractInfo.ProductionDeptName;
                engineering.BusinessManager = contractInfo.BusinessManager;
                engineering.BusinessManagerName = contractInfo.BusinessManagerName;
                engineering.ChargerUser = contractInfo.ProductionManager;
                engineering.ChargerUserName = contractInfo.ProductionManagerName;
                this.BusinessEntities.Set<S_I_Engineering>().Add(engineering);
                contractInfo.EngineeringInfo = engineering.ID;
                contractInfo.EngineeringInfoName = engineering.Name;
            }
            this.BusinessEntities.SaveChanges();
            return Json(engineering);
        }

        public JsonResult GetManageContractExamine(string contractID)
        {
            string sql = @"select TOP 1 ID from T_C_ContractReview where  ContractID = '{0}' ORDER BY id desc";
            sql = string.Format(sql, contractID);
            DataTable dt = this.MarketSQLDB.ExecuteDataTable(sql);
            return Json(dt);

        }

        public JsonResult RemoveReceiptObj(string ReceiptData)
        {
            var list = JsonHelper.ToList(ReceiptData);
            foreach (var item in list)
            {
                var receiptObject = this.GetEntityByID<S_C_ManageContract_ReceiptObj>(item.GetValue("ID"));
                if (receiptObject != null)
                {
                    receiptObject.Delete();
                }
            }
            this.BusinessEntities.SaveChanges();
            return Json("");
        }

        public JsonResult GetProjectInfoListByEngineeringID(string EngineeringInfoID)
        {
            var list = this.BusinessEntities.Set<S_I_Project>().Where(d => d.EngineeringInfo == EngineeringInfoID).ToList();
            return Json(list);
        }

        public JsonResult DeleteContractRelateTBForever(string ListIDs)
        {
            BusinessEntities.Set<S_C_Invoice>().Delete(a => ListIDs.Contains(a.ContractInfoID));
            BusinessEntities.Set<S_C_Invoice_InvoiceDatail>().Delete(a => BusinessEntities.Set<S_C_Invoice>().Where(b => ListIDs.Contains(b.ContractInfoID)).Any(c => c.ID == a.S_C_InvoiceID));
            BusinessEntities.Set<S_C_Invoice_ReceiptObject>().Delete(a => ListIDs.Contains(a.ContractInfoID));
            BusinessEntities.Set<S_C_InvoiceReceiptRelation>().Delete(a => ListIDs.Contains(a.ContractInfoID));
            BusinessEntities.Set<S_C_ManageContract>().Delete(a => ListIDs.Contains(a.ID));
            BusinessEntities.Set<S_C_ManageContract_ContractSplit>().Delete(a => ListIDs.Contains(a.S_C_ManageContractID));
            BusinessEntities.Set<S_C_ManageContract_ProjectRelation>().Delete(a => ListIDs.Contains(a.S_C_ManageContractID));
            BusinessEntities.Set<S_C_ManageContract_ReceiptObj>().Delete(a => ListIDs.Contains(a.S_C_ManageContractID));
            BusinessEntities.Set<S_C_ManageContract_Supplementary>().Delete(a => ListIDs.Contains(a.ContractInfoID));
            BusinessEntities.Set<S_C_PlanReceipt>().Delete(a => ListIDs.Contains(a.ContractInfoID));
            BusinessEntities.Set<S_C_Receipt>().Delete(a => ListIDs.Contains(a.ContractInfoID));
            BusinessEntities.Set<S_C_AcceptanceBill>().Delete(a => BusinessEntities.Set<S_C_Receipt>().Where(b => ListIDs.Contains(b.ContractInfoID)).Any(c => a.ReceiptIDs.Contains(c.ID)));
            BusinessEntities.Set<S_C_ReceiptPlanRelation>().Delete(a => BusinessEntities.Set<S_C_Receipt>().Where(b => ListIDs.Contains(b.ContractInfoID)).Any(c => c.ID == a.ReceiptID));
            BusinessEntities.Set<T_C_ContractChange>().Delete(a => ListIDs.Contains(a.ContractID));
            BusinessEntities.Set<T_C_ContractChange_ContractSplit>().Delete(a => BusinessEntities.Set<T_C_ContractChange>().Where(b => ListIDs.Contains(b.ContractID)).Any(c => c.ID == a.T_C_ContractChangeID));
            BusinessEntities.Set<T_C_ContractChange_ProjectRelation>().Delete(a => BusinessEntities.Set<T_C_ContractChange>().Where(b => ListIDs.Contains(b.ContractID)).Any(c => c.ID == a.T_C_ContractChangeID));
            BusinessEntities.Set<T_C_ContractChange_ReceiptObj>().Delete(a => BusinessEntities.Set<T_C_ContractChange>().Where(b => ListIDs.Contains(b.ContractID)).Any(c => c.ID == a.T_C_ContractChangeID));
            BusinessEntities.Set<T_C_ContractReview>().Delete(a => ListIDs.Contains(a.ContractID));
            BusinessEntities.Set<T_C_CreditNoteApply>().Delete(a => ListIDs.Contains(a.Contract));
            BusinessEntities.Set<T_C_CreditNoteApply_Detail>().Delete(a => BusinessEntities.Set<T_C_CreditNoteApply>().Where(b => ListIDs.Contains(b.Contract)).Any(c => c.ID == a.T_C_CreditNoteApplyID));
            BusinessEntities.Set<T_C_GuaranteeLetter>().Delete(a => ListIDs.Contains(a.Contract));
            BusinessEntities.Set<T_C_InvoiceApply>().Delete(a => ListIDs.Contains(a.Contract));
            BusinessEntities.Set<T_C_InvoiceApply_Detail>().Delete(a => BusinessEntities.Set<T_C_InvoiceApply>().Where(b => ListIDs.Contains(b.Contract)).Any(c => c.ID == a.T_C_InvoiceApplyID));
            BusinessEntities.Set<T_C_InvoiceMakeUpApply>().Delete(a => ListIDs.Contains(a.Contract));
            BusinessEntities.Set<T_F_GuaranteeLetterApply>().Delete(a => ListIDs.Contains(a.Contract));
            BusinessEntities.Set<T_SP_DesignApproval>().Delete(a => ListIDs.Contains(a.Contract));
            BusinessEntities.Set<T_SP_DesignApproval_CoopProjectInfo>().Delete(a => BusinessEntities.Set<T_SP_DesignApproval>().Where(b => ListIDs.Contains(b.Contract)).Any(c => c.ID == a.T_SP_DesignApprovalID));
            BusinessEntities.Set<T_SP_DesignApproval_CredentialInfo>().Delete(a => BusinessEntities.Set<T_SP_DesignApproval>().Where(b => ListIDs.Contains(b.Contract)).Any(c => c.ID == a.T_SP_DesignApprovalID));
            BusinessEntities.Set<S_SP_SupplierContract>().Delete(a => ListIDs.Contains(a.ManagerContract));
            var supplierContract = BusinessEntities.Set<S_SP_SupplierContract>().Where(b => ListIDs.Contains(b.ManagerContract));
            BusinessEntities.Set<S_SP_Invoice>().Delete(a => supplierContract.Any(c => c.ID == a.SupplierContract));
            BusinessEntities.Set<S_SP_Payment>().Delete(a => supplierContract.Any(c => c.ID == a.Contract));
            BusinessEntities.Set<S_SP_Payment_AcceptanceBill>().Delete(a => BusinessEntities.Set<S_SP_Payment>().Where(b => supplierContract.Any(e => e.ID == b.Contract)).Any(c => c.ID == a.S_SP_PaymentID));
            BusinessEntities.Set<S_SP_Payment_InvoiceRelation>().Delete(a => BusinessEntities.Set<S_SP_Payment>().Where(b => supplierContract.Any(e => e.ID == b.Contract)).Any(c => c.ID == a.S_SP_PaymentID));
            BusinessEntities.Set<S_SP_PaymentPlan>().Delete(a => supplierContract.Any(c => c.ID == a.SupplierContract));
            BusinessEntities.Set<T_SP_PaymentApply>().Delete(a => supplierContract.Any(c => c.ID == a.Contract));
            BusinessEntities.Set<T_SP_PaymentApply_AcceptanceBill>().Delete(a => BusinessEntities.Set<T_SP_PaymentApply>().Where(b => supplierContract.Any(e => e.ID == b.Contract)).Any(c => c.ID == a.T_SP_PaymentApplyID));
            BusinessEntities.Set<T_SP_PaymentApply_InvoiceRelation>().Delete(a => BusinessEntities.Set<T_SP_PaymentApply>().Where(b => supplierContract.Any(e => e.ID == b.Contract)).Any(c => c.ID == a.T_SP_PaymentApplyID));
            BusinessEntities.SaveChanges();
            return Json("");
        }

        public JsonResult GetTaxRate(string TaxCode)
        {
            var dt = this.MarketSQLDB.ExecuteDataTable(String.Format("SELECT * FROM S_EP_DefineTaxRate WHERE Code='{0}'", TaxCode));
            var result = new Dictionary<string, object>();
            if (dt.Rows.Count == 0)
            {
                result.SetValue("TaxRate", 0);
            }
            else
            {
                result.SetValue("TaxRate", dt.Rows[0]["TaxRate"] == null || dt.Rows[0]["TaxRate"] == DBNull.Value
                    ? 0m : Convert.ToDecimal(dt.Rows[0]["TaxRate"]));
            }
            return Json(result);
        }
    }

    class FieldEnum
    {
        public string Field { get; set; }
        public IList<Config.DicItem> EnumItems { get; set; }
    }

    class FieldText
    {
        public string Field { get; set; }
        public int MaxLength { get; set; }
    }
}
