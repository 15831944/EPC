using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Market.Logic.Domain;
using Market.Logic;
using Formula;
using Config;
using Config.Logic;
using Formula.Helper;
using Base.Logic.Domain;
using Formula.Exceptions;

namespace Market.Areas.MarketUI.Controllers
{
    public class ManageContractSupplementaryController : MarketFormContorllor<S_C_ManageContract_Supplementary>
    {
        protected override void BeforeSave(Dictionary<string, string> dic, Base.Logic.Domain.S_UI_Form formInfo, bool isNew)
        {
            //base.BeforeSave(dic, formInfo, isNew);
            //补充协议为负值时，1.不能大于剩余可开票金额
            var currentValue = Convert.ToDecimal(dic.GetValue("SupplementaryRMBAmount"));
            var contractID = dic.GetValue("ContractInfoID");
            if (currentValue < 0)
            {
                var id = dic.GetValue("ID");
                var detailIstStr = dic.GetValue("ReceiptObj");
                if (!string.IsNullOrEmpty(detailIstStr))
                {
                    var detailIst = JsonHelper.ToList(detailIstStr);
                    if (detailIst.Count > 1)
                        throw new Formula.Exceptions.BusinessException("协议金额为负数时，不能增加收款项");
                }
                ValidateDelete(contractID, currentValue);
            }
            //补充协议关联的合同必须有签约日期
            var contract = this.GetEntityByID<S_C_ManageContract>(contractID);
            if (contract.SignDate == null)
                throw new Formula.Exceptions.BusinessException("未签约的合同不能添加补充协议");
        }

        protected override void AfterSave(Dictionary<string, string> dic, Base.Logic.Domain.S_UI_Form formInfo, bool isNew)
        {
            var entity = this.GetEntityByID<S_C_ManageContract_Supplementary>(dic.GetValue("ID"));
            SyncContract(entity);

            if (entity.SignDate.HasValue)
            {
                var date = Convert.ToDateTime(entity.SignDate);
                entity.BelongQuarter = MarketHelper.GetQuarter(date);
                entity.BelongYear = date.Year;
                entity.BelongMonth = date.Month;
            }

            Action action = () => {
                this.BusinessEntities.SaveChanges();
                #region 修改补充协议需要同步业财的合同信息
                if (entity.S_C_ManageContract != null)
                {
                    var contractInfoDic = FormulaHelper.ModelToDic<S_C_ManageContract>(entity.S_C_ManageContract);
                    Expenses.Logic.CBSInfoFO.SynCBSInfo(contractInfoDic, Expenses.Logic.SetCBSOpportunity.Contract);
                }
                #endregion
            };
            this.ExecuteAction(action);
        }

        protected override void BeforeSaveSubTable(Dictionary<string, string> dic, string subTableName, List<Dictionary<string, string>> detailList, S_UI_Form formInfo)
        {
            if (subTableName == "ReceiptObj" && detailList.Count > 0)
            {
                var sumReceiptObjValue = 0m;
                foreach (var item in detailList)
                {
                    sumReceiptObjValue += String.IsNullOrEmpty(item.GetValue("ReceiptValue")) ? 0m : Convert.ToDecimal(item.GetValue("ReceiptValue"));
                }
                var contractRMBValue = String.IsNullOrEmpty(dic.GetValue("SupplementaryRMBAmount")) ? 0m : Convert.ToDecimal(dic.GetValue("SupplementaryRMBAmount"));
                if (sumReceiptObjValue > contractRMBValue)
                {
                    throw new Formula.Exceptions.BusinessException("当前协议收款项金额总计不能大于折合人民币金额");
                }
            }
            else if (subTableName == "DeptRelation")
            {
                var sumValue = 0m;
                foreach (var item in detailList)
                {
                    sumValue += String.IsNullOrEmpty(item.GetValue("DeptValue")) ? 0m : Convert.ToDecimal(item.GetValue("DeptValue"));
                }
                var contractRMBValue = String.IsNullOrEmpty(dic.GetValue("SupplementaryRMBAmount")) ? 0m : Convert.ToDecimal(dic.GetValue("SupplementaryRMBAmount"));
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
                var id = detail.GetValue("ID");
                var entity = this.BusinessEntities.Set<S_C_ManageContract_ReceiptObj>().FirstOrDefault(a => a.SupplementaryID == id);
                var receiptValue = String.IsNullOrEmpty(detail.GetValue("ReceiptValue")) ? 0m : Convert.ToDecimal(detail.GetValue("ReceiptValue"));
                if (entity != null)
                {
                    var sumInvoice = entity.S_C_Invoice_ReceiptObject.Sum(a => a.RelationValue);
                    var sumReceipt = entity.S_C_PlanReceipt.Sum(a => a.FactReceiptValue);
                    if (receiptValue < sumInvoice || receiptValue < sumReceipt)
                        throw new Formula.Exceptions.BusinessException("当前协议收款项金额不能小于已开票或已收款金额");
                }
            }
        }

        public JsonResult RemoveReceiptObj(string ReceiptData)
        {
            var list = JsonHelper.ToList(ReceiptData);
            foreach (var item in list)
            {
                var id = item.GetValue("ID");
                var receiptObject = this.GetEntityByID<S_C_ManageContract_ReceiptObj>(id);
                if (receiptObject != null)
                    receiptObject.Delete();
                this.BusinessEntities.Set<S_C_ManageContract_Supplementary_ReceiptObj>().Delete(a => a.ID == id);
            }
            this.BusinessEntities.SaveChanges();
            return Json("");
        }

        public override JsonResult Delete()
        {
            var Ids = this.GetQueryString("ListIDs").Split(',');
            var details = this.BusinessEntities.Set<S_C_ManageContract_Supplementary>().Where(a => Ids.Contains(a.ID)).ToList();
            var receiptObjs = this.BusinessEntities.Set<S_C_ManageContract_ReceiptObj>().Where(a => Ids.Contains(a.SupplementaryID)).ToList();
            foreach (var receiptObj in receiptObjs)
            {
                receiptObj.Delete();
            }
            var contractIDList = details.Select(c => c.ContractInfoID).Distinct().ToList();
            foreach (var item in details)
            {
                if (item.SupplementaryRMBAmount.HasValue && item.SupplementaryRMBAmount.Value > 0)
                    ValidateDelete(item.ContractInfoID, item.SupplementaryRMBAmount);
                SyncContract(item, true);
                this.BusinessEntities.Set<S_C_ManageContract_Supplementary>().Remove(item);
            }

            Action action = () => {
                BusinessEntities.SaveChanges();
                #region 删除补充协议需要同步业财的合同信息
                foreach (var contractInfoID in contractIDList)
                {
                    var contract = this.GetEntityByID<S_C_ManageContract>(contractInfoID);
                    if (contract != null)
                    {
                        var contractInfoDic = FormulaHelper.ModelToDic<S_C_ManageContract>(contract);
                        Expenses.Logic.CBSInfoFO.SynCBSInfo(contractInfoDic, Expenses.Logic.SetCBSOpportunity.Contract);
                    }
                }
                #endregion
            };
            this.ExecuteAction(action);
            return Json("");
        }

        private void ValidateDelete(string ContractInfoID, decimal? SupplementaryRMBAmount)
        {
            var contract = this.GetEntityByID<S_C_ManageContract>(ContractInfoID);
            if (contract == null) throw new Formula.Exceptions.BusinessException("未能找到指定的收入合同");
            var currentValue = Math.Abs(SupplementaryRMBAmount ?? 0m);
            if (currentValue > contract.RemainInvoiceValue)
                throw new BusinessException("当前协议金额为【" + currentValue.ToString() + "】，主合同的未开票金额总计为【" + contract.RemainInvoiceValue.ToString() + "】不足以冲抵，请先增加主合同金额");
        }

        private void SyncContract(S_C_ManageContract_Supplementary entity, bool isDelete = false)
        {
            var contract = this.GetEntityByID<S_C_ManageContract>(entity.ContractInfoID);
            if (contract == null) throw new Formula.Exceptions.BusinessException("未能找到指定的收入合同");
            var sumSettle = this.BusinessEntities.Set<S_C_ManageContract_Supplementary>()
                .Where(a => a.ContractInfoID == contract.ID).Select(a => a.SupplementaryRMBAmount).Sum();
            contract.ContractRMBAmount = (contract.ThisContractRMBAmount ?? 0m) + (sumSettle ?? 0m);
            if (isDelete)
                contract.ContractRMBAmount = contract.ContractRMBAmount - (entity.SupplementaryRMBAmount ?? 0m);

            #region 合同收款项逻辑
            if (!isDelete && entity.SupplementaryRMBAmount > 0)
            {
                S_C_ManageContract_ProjectRelation projectRelation = null;
                if (contract.S_C_ManageContract_ProjectRelation.Count == 1)
                    projectRelation = contract.S_C_ManageContract_ProjectRelation.FirstOrDefault();

                #region 生成收款项，并同步到主合同
                var supplementaryReceiptObjList = entity.S_C_ManageContract_Supplementary_ReceiptObj.ToList();
                if (supplementaryReceiptObjList.Count == 0)
                {
                    //没有收款项得时候默认增加一个收款项
                    var supplementaryReceiptObj = new S_C_ManageContract_Supplementary_ReceiptObj();
                    supplementaryReceiptObj.ID = FormulaHelper.CreateGuid();
                    supplementaryReceiptObj.S_C_ManageContract_SupplementaryID = entity.ID;
                    supplementaryReceiptObj.SortIndex = 1;
                    supplementaryReceiptObj.ReceiptPercent = 100;
                    supplementaryReceiptObj.Name = entity.Name;
                    supplementaryReceiptObj.ReceiptValue = entity.SupplementaryValue;
                    if (projectRelation != null)
                    {
                        supplementaryReceiptObj.ProjectInfo = projectRelation.ProjectID;
                        supplementaryReceiptObj.ProjectInfoName = projectRelation.ProjectName;
                    }

                    supplementaryReceiptObjList.Add(supplementaryReceiptObj);
                    entity.S_C_ManageContract_Supplementary_ReceiptObj.Add(supplementaryReceiptObj);
                }

                //同步到收入合同的收款项
                foreach (var supplementaryReceiptObj in supplementaryReceiptObjList)
                {
                    var receiptObj = this.GetEntityByID<S_C_ManageContract_ReceiptObj>(supplementaryReceiptObj.ID);
                    if (receiptObj == null)
                    {
                        receiptObj = new S_C_ManageContract_ReceiptObj();
                        receiptObj.ID = supplementaryReceiptObj.ID;
                        receiptObj.S_C_ManageContractID = contract.ID;
                        receiptObj.SupplementaryID = entity.ID;
                        receiptObj.SortIndex = 1;
                        contract.S_C_ManageContract_ReceiptObj.Add(receiptObj);
                        receiptObj.S_C_ManageContract = contract;
                        this.BusinessEntities.Set<S_C_ManageContract_ReceiptObj>().Add(receiptObj);
                    }
                    receiptObj.Name = supplementaryReceiptObj.Name;
                    receiptObj.ReceiptPercent = 0;
                    receiptObj.ReceiptValue = supplementaryReceiptObj.ReceiptValue;
                    receiptObj.ProjectInfo = supplementaryReceiptObj.ProjectInfo;
                    receiptObj.ProjectInfoName = supplementaryReceiptObj.ProjectInfoName;
                    receiptObj.PlanFinishDate = supplementaryReceiptObj.PlanFinishDate;
                    receiptObj.SummaryReceiptValue();
                    receiptObj.ResetPlan();
                }
                #endregion
            }
            #endregion

            #region 合同自动按权重计算关联项目明细金额
            var relationList = contract.S_C_ManageContract_ProjectRelation.OrderBy(a => a.SortIndex).ToList();
            var relationTotalScale = relationList.Sum(a => a.Scale);
            if (relationTotalScale.HasValue)
            {
                var totalValue = Math.Round(Convert.ToDecimal(relationTotalScale * contract.ContractRMBAmount / 100), 2);
                var sumValue = 0m;
                foreach (var item in relationList)
                {
                    if (item == relationList.LastOrDefault())
                    {
                        item.ProjectValue = totalValue - sumValue;
                    }
                    else
                    {
                        item.ProjectValue = Math.Round(Convert.ToDecimal(item.Scale * contract.ContractRMBAmount / 100), 2);

                    }
                    if (contract.TaxRate.HasValue)
                    {
                        item.TaxValue = item.ProjectValue / (1 + contract.TaxRate.Value) * contract.TaxRate.Value;
                        item.ClearValue = item.ProjectValue - item.TaxValue;
                    }
                    sumValue += (item.ProjectValue ?? 0m);
                }
            }
            #endregion

            #region 合同自动按权重计算合同拆分子表金额
            var splitList = contract.S_C_ManageContract_ContractSplit.OrderBy(a => a.SortIndex).ToList();
            var splitTotalScale = splitList.Sum(a => a.Scale);
            if (splitTotalScale.HasValue)
            {
                var totalValue = Math.Round(Convert.ToDecimal(splitTotalScale * contract.ContractRMBAmount / 100), 2);
                var sumValue = 0m;
                foreach (var item in splitList)
                {
                    if (item == splitList.LastOrDefault())
                    {
                        item.SplitValue = totalValue - sumValue;
                    }
                    else
                    {
                        item.SplitValue = Math.Round(Convert.ToDecimal(item.Scale * contract.ContractRMBAmount / 100), 2);
                    }
                    sumValue += (item.SplitValue ?? 0m);
                }
            }
            #endregion

            #region 重新计算合同部门分解补充协议金额
            var deptList = contract.S_C_ManageContract_DeptRelation.OrderBy(a => a.SortIndex).ToList();
            foreach (var item in deptList)
            {
                var sValue = 0m;
                foreach (var supplementary in contract.S_C_ManageContract_Supplementary.ToList())
                    sValue += Convert.ToDecimal(supplementary.S_C_ManageContract_Supplementary_DeptRelation.Where(a => a.Dept == item.Dept).Sum(a => a.DeptValue));
                item.SumSupplementaryValue = sValue;
            }
            #endregion
        }
    }
}
