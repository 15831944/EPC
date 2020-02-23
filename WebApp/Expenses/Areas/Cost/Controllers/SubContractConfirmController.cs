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
    public class SubContractConfirmController : ExpensesFormController<S_EP_SupplierContractConfirm>
    {
        public JsonResult GetConfirmList(string DetailID)
        {
            string sql = String.Format(@"select FinalConfirm.ID as FinalID,ISNULL(RevertInfo.ID,'0') as RevertID,ConfirmInfo.* 
from (select * from S_EP_SupplierContractConfirm where SubContractDetailID='{0}') ConfirmInfo 
left join (select top 1 ID from S_EP_SupplierContractConfirm where SubContractDetailID='{0}' order by ID desc) FinalConfirm on ConfirmInfo.ID=FinalConfirm.ID 
outer apply( select top 1 ID from S_EP_SupplierContractConfirmRevert a where ConfirmInfo.ID=a.SubContractConfirmID) RevertInfo 
order by ID ", DetailID);
            var data = this.SQLDB.ExecuteDataTable(sql);
            return Json(data);
        }

        protected override void BeforeSave(Dictionary<string, string> dic, S_UI_Form formInfo, bool isNew)
        {
            CheckSubContractConfirmMethod();
            if (String.IsNullOrEmpty(dic.GetValue("ConfirmDate")))
            {
                throw new Formula.Exceptions.BusinessValidationException("请选择确认日期！");
            }
            var dateTime = String.IsNullOrEmpty(dic.GetValue("ConfirmDate")) ? DateTime.Now : Convert.ToDateTime(dic.GetValue("ConfirmDate"));
            CostFO.ValidatePeriodIsClosed(dateTime);

            var totalProgress = 0m;
            decimal.TryParse(dic.GetValue("TotalProgress"), out totalProgress);
            if (totalProgress <= 0 || totalProgress > 1)
            {
                throw new Formula.Exceptions.BusinessValidationException("本期末累计进度必须大于0！");
            }

            if (isNew)
            {
                var sql = string.Format(@"select ContractSplit.ID as SplitID,
ISNULL(ContractSplit.SplitValue,0) as SplitValue,
ContractSplit.ChargerUserName,
ContractSplit.ChargerDeptName,
SupplierContract.Supplier,
SupplierContract.SupplierName,
SupplierContract.ProjectInfo,
SupplierContract.ProjectInfoName,
SupplierContract.Name as SubContracName,
SupplierContract.SerialNumber as SubContracCode,
SupplierContract.TaxName,
isnull(SupplierContract.TaxRate,0) as TaxRate,
ISNULL(ConfirmInfo.TotalProgress,0) as LastProgress,--上期末累计确认比例
ISNULL(ConfirmInfo.TotalValue,0) as LastValue,--上期末累计确认金额
RevertInfo.FlowPhase as RevertFlowPhase,
RevertInfo.ID as ConfirmRevertID
from (select * from S_SP_SupplierContract_ContractSplit where ID='{0}') as ContractSplit
left join S_SP_SupplierContract SupplierContract on S_SP_SupplierContractID = SupplierContract.ID
outer apply(select top 1 * from S_EP_SupplierContractConfirm a where ContractSplit.ID=a.SubContractDetailID order by a.ID desc) ConfirmInfo
left join S_EP_SupplierContractConfirmRevert RevertInfo on ConfirmInfo.ID=RevertInfo.SubContractConfirmID ", dic.GetValue("SubContractDetailID"));
                var dtLastConfirm = SQLDB.ExecuteDataTable(sql);
                if (dtLastConfirm.Rows.Count > 0)
                {
                    var dicLastConfirm = FormulaHelper.DataRowToDic(dtLastConfirm.Rows[0]);
                    var lastProgress = string.IsNullOrEmpty(dicLastConfirm.GetValue("LastProgress")) ? 0m : Convert.ToDecimal(dicLastConfirm.GetValue("LastProgress"));
                    if (lastProgress >= 1)
                    {
                        throw new Formula.Exceptions.BusinessValidationException("已经确认至100%，不能再进行确认！");
                    }
                    if (totalProgress <= lastProgress)
                    {
                        throw new Formula.Exceptions.BusinessValidationException(string.Format("本期末累计确认比例【{0}%】必须大于上期末累计确认比例【{1}%】", Math.Round(totalProgress * 100, 2), Math.Round(lastProgress * 100, 2)));
                    }
                    if (!string.IsNullOrWhiteSpace(dicLastConfirm.GetValue("ConfirmRevertID")))
                    {
                        throw new Formula.Exceptions.BusinessValidationException("确认节点正在撤销中，不能进行确认！");
                    }
                    var splitValue = string.IsNullOrEmpty(dicLastConfirm.GetValue("SplitValue")) ? 0m : Convert.ToDecimal(dicLastConfirm.GetValue("SplitValue"));
                    if (splitValue == 0)
                    {
                        throw new Formula.Exceptions.BusinessValidationException("合同金额为0，不能确认！");
                    }

                    #region 计算【本期末累计确认金额】【本期确认金额】【税金】【去税金额】
                    var lastValue = string.IsNullOrEmpty(dicLastConfirm.GetValue("LastValue")) ? 0m : Convert.ToDecimal(dicLastConfirm.GetValue("LastValue"));
                    var totalValue = splitValue * totalProgress;//本期末累计确认金额
                    var currentValue = totalValue - lastValue;//本期确认金额
                    if (currentValue <= 0)
                    {
                        throw new Formula.Exceptions.BusinessValidationException(string.Format("本期确认金额【{0}】必须大于【0】,请确认！", Math.Round(currentValue, 2)));
                    }
                    var taxRate = string.IsNullOrEmpty(dicLastConfirm.GetValue("TaxRate")) ? 0m : Convert.ToDecimal(dicLastConfirm.GetValue("TaxRate"));//税率
                    var taxValue = currentValue * taxRate / (1 + taxRate);//税金  
                    var clearValue = currentValue / (1 + taxRate);//去税金额

                    dicLastConfirm.SetValue("LastProgress", lastProgress);
                    dicLastConfirm.SetValue("LastValue", lastValue);
                    dicLastConfirm.SetValue("TotalProgress", totalProgress);
                    dicLastConfirm.SetValue("TotalValue", Math.Round(totalValue, 2));
                    dicLastConfirm.SetValue("CurrentValue", Math.Round(currentValue, 2));
                    dicLastConfirm.SetValue("TaxRate", taxRate);
                    dicLastConfirm.SetValue("TaxValue", Math.Round(taxValue, 2));
                    dicLastConfirm.SetValue("ClearValue", Math.Round(clearValue, 2));

                    #endregion

                }
                else
                {
                    throw new Formula.Exceptions.BusinessValidationException("没有找到指定的委外合同，无法进行确认");
                }

            }
            else
            {
                var lastProgress = Convert.ToDecimal(dic.GetValue("LastProgress"));
                if (totalProgress <= lastProgress)
                {
                    throw new Formula.Exceptions.BusinessValidationException(string.Format("本期末累计确认比例【{0}%】必须大于上期末累计确认比例【{1}%】", totalProgress * 100, lastProgress * 100));
                }

                #region 计算【本期末累计确认金额】【本期确认金额】【税金】【去税金额】
                var splitValue = Convert.ToDecimal(dic.GetValue("SplitValue"));
                var lastValue = Convert.ToDecimal(dic.GetValue("LastValue"));
                var totalValue = splitValue * totalProgress;//本期末累计确认金额
                var currentValue = totalValue - lastValue;//本期确认金额
                if (currentValue <= 0)
                {
                    throw new Formula.Exceptions.BusinessValidationException(string.Format("本期确认金额【{0}】必须大于【0】,请确认！", Math.Round(currentValue, 2)));
                }
                var taxRate = string.IsNullOrEmpty(dic.GetValue("TaxRate")) ? 0m : Convert.ToDecimal(dic.GetValue("TaxRate"));//税率
                var taxValue = currentValue * taxRate / (1 + taxRate);//税金  
                var clearValue = currentValue / (1 + taxRate);//去税金额

                dic.SetValue("TotalProgress", totalProgress.ToString());
                dic.SetValue("TotalValue", Math.Round(totalValue, 2).ToString());
                dic.SetValue("CurrentValue", Math.Round(currentValue, 2).ToString());
                dic.SetValue("TaxValue", Math.Round(taxValue, 2).ToString());
                dic.SetValue("ClearValue", Math.Round(clearValue, 2).ToString()); 
                #endregion

            }

        }

        protected override void OnFlowEnd(S_EP_SupplierContractConfirm data, S_WF_InsTaskExec taskExec, S_WF_InsDefRouting routing)
        {
            CheckSubContractConfirmMethod();
            if (data != null)
            {
                data.Push();
            }
        }

        public JsonResult ValidateStart(string DetailID)
        {
            CheckSubContractConfirmMethod();

            var sql = string.Format("select * from S_SP_SupplierContract_ContractSplit where ID='{0}' ", DetailID);
            var dt = SQLDB.ExecuteDataTable(sql);
            if (dt.Rows.Count == 0)
            {
                throw new Formula.Exceptions.BusinessValidationException("没有找到指定的委外合同单元，无法进行确认");
            }
            var detailDic = FormulaHelper.DataRowToDic(dt.Rows[0]);

            var subContractDic = this.GetDataDicByID("S_SP_SupplierContract", detailDic.GetValue("S_SP_SupplierContractID"));
            if (subContractDic == null)
            {
                throw new Formula.Exceptions.BusinessValidationException("没有找到指定的委外合同，无法进行确认");
            }

            var currentID = this.SQLDB.ExecuteScalar(string.Format("SELECT TOP 1 ID FROM S_EP_SupplierContractConfirm WHERE SubContractDetailID='{0}' AND FLOWPHASE='Start'", DetailID));
            if (currentID != null && currentID != DBNull.Value && !String.IsNullOrEmpty(currentID.ToString()))
            {
                return Json(new { ID = currentID.ToString() });
            }

            if (Convert.ToInt32(this.SQLDB.ExecuteScalar(string.Format("SELECT COUNT(ID) FROM S_EP_SupplierContractConfirm WHERE SubContractDetailID='{0}' AND FLOWPHASE!='End'", DetailID))) > 0)
            {
                throw new BusinessValidationException("【" + subContractDic.GetValue("Name") + "】有在审批中的合同确认表，审批完成后才能再次进行确认");
            }

            #region 判断是否已确认100%，是否在撤销中
            sql = string.Format(@"select ConfirmInfo.*,RevertInfo.FlowPhase as RevertFlowPhase, RevertInfo.ID as ConfirmRevertID 
from (select top 1 ID,TotalProgress from S_EP_SupplierContractConfirm where SubContractDetailID='{0}' order by id desc) ConfirmInfo
left join S_EP_SupplierContractConfirmRevert RevertInfo on ConfirmInfo.ID=RevertInfo.SubContractConfirmID ", DetailID);
            var dtConfirm = SQLDB.ExecuteDataTable(sql);
            if (dtConfirm.Rows.Count > 0)
            {
                var dic = FormulaHelper.DataRowToDic(dtConfirm.Rows[0]);
                var totalProgress = 0m;
                decimal.TryParse(dic.GetValue("TotalProgress"), out totalProgress);
                if (totalProgress >= 1)
                {
                    throw new Formula.Exceptions.BusinessValidationException("已经确认至100%，不能再进行确认！");
                }
                if (!string.IsNullOrWhiteSpace(dic.GetValue("ConfirmRevertID")))
                {
                    throw new Formula.Exceptions.BusinessValidationException("确认节点正在撤销中，不能进行确认！");
                }

            } 
            #endregion

            return Json("");
        }

        private void CheckSubContractConfirmMethod()
        {
            if (SysParams.Params.GetValue("SubContractConfirmMethod") != "SubContractConfirm")//委外合同确认
            {
                throw new Formula.Exceptions.BusinessValidationException("【按委外合同确认】不是系统设置的外委成本确认方式！");
            }

        }

    }
}
