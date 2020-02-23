using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Data;

using Config;
using Config.Logic;
using Formula;
using Newtonsoft.Json;

namespace Expenses.Logic.Domain
{
    public partial class S_EP_SubContractProgressConfirm : BaseEPModel
    {
        public void Push()
        {
            var dateTime = String.IsNullOrEmpty(this.ModelDic.GetValue("ConfirmDate")) ? DateTime.Now : Convert.ToDateTime(this.ModelDic.GetValue("ConfirmDate"));
            CostFO.ValidatePeriodIsClosed(dateTime);

            var taskEntity = this.GetDataDicByID("S_EP_SubContractTask_CostUnitDetail", this.ModelDic.GetValue("TaskDetailID"));
            var unitDic = this.GetDataDicByID("S_EP_CostUnit", this.ModelDic.GetValue("CostUnitID"));
            var cbsInfoDic = this.GetDataDicByID("S_EP_CBSInfo", unitDic.GetValue("CBSInfoID"));
            if (taskEntity == null)
            {
                throw new Formula.Exceptions.BusinessValidationException("没有找到指定的分包外委任务，无法确认外委进度");
            }
            if (unitDic == null)
            {
                throw new Formula.Exceptions.BusinessValidationException("没有找到指定的成本单元，无法确认外委进度");
            }
            var unitCBSNode = this.GetDataDicByID("S_EP_CBSNode", unitDic.GetValue("CBSNodeID"), ConnEnum.Market, true);
            if (unitCBSNode == null)
            {
                throw new Formula.Exceptions.BusinessValidationException("没有找到成本单元对应的CBS节点，无法确认外委进度");
            }
            if (cbsInfoDic == null)
            {
                throw new Formula.Exceptions.BusinessValidationException("没有找到成本单元对应的核算项目，无法确认外委进度");
            }
            var contractValue = Convert.ToDecimal(taskEntity.GetValue("SubContractValue"));
            var scale = String.IsNullOrEmpty(this.ModelDic.GetValue("TotalProgress")) ? 0 : Convert.ToDecimal(this.ModelDic.GetValue("TotalProgress"));
            var totalValue = contractValue * scale;
            var sumCostValue = Convert.ToDecimal(taskEntity.GetValue("TotalSubContractValue"));
            var currentValue = totalValue - sumCostValue;

            //税金计算
            var taxRate = String.IsNullOrEmpty(this.ModelDic.GetValue("TaxRate")) ? 0m : Convert.ToDecimal(this.ModelDic.GetValue("TaxRate"));
            var taxValue = currentValue / (1 + taxRate) * taxRate;
            var clearCostValue = currentValue - taxValue;

            var costDetailDic = new Dictionary<string, object>();
            var subjectDt = this.DB.ExecuteDataTable(String.Format(@"select * from S_EP_CBSNode with(nolock)
where FullID like '{0}%' order by FullID", unitCBSNode.GetValue("FullID")));
            var costState = String.IsNullOrEmpty(this.ModelDic.GetValue("CostState")) ? "Finish" : this.ModelDic.GetValue("CostState");

            var subjectCode = this.ModelDic.GetValue("SubjectCode");
            var subjectNode = subjectDt.AsEnumerable().FirstOrDefault(c => c["SubjectCode"] != null && c["SubjectCode"] != DBNull.Value &&
                c["SubjectCode"].ToString() == subjectCode);
            if (subjectNode == null)
            {
                subjectNode = subjectDt.AsEnumerable().FirstOrDefault(c => c["SubjectType"] != null && c["SubjectType"] != DBNull.Value
                    && c["SubjectType"].ToString() == SubjectType.SubContractCost.ToString());
                if (subjectNode == null)
                {
                    costDetailDic.SetValue("Name", "采购分包费");
                    costDetailDic.SetValue("Code", unitCBSNode.GetValue("Code"));
                    costDetailDic.SetValue("SubjectCode", unitCBSNode.GetValue("Code"));
                    costDetailDic.SetValue("CBSFullCode", unitCBSNode.GetValue("FullCode"));
                    costDetailDic.SetValue("CBSNodeID", unitCBSNode.GetValue("ID"));
                    costDetailDic.SetValue("CBSNodeFullID", unitCBSNode.GetValue("FullID"));
                }
                else
                {
                    costDetailDic.SetValue("Name", subjectNode["Name"]);
                    costDetailDic.SetValue("Code", subjectNode["Code"]);
                    costDetailDic.SetValue("SubjectCode", subjectNode["SubjectCode"]);
                    costDetailDic.SetValue("CBSFullCode", subjectNode["FullCode"]);
                    costDetailDic.SetValue("CBSNodeID", subjectNode["ID"]);
                    costDetailDic.SetValue("CBSNodeFullID", subjectNode["FullID"]);
                }
            }
            else
            {
                costDetailDic.SetValue("Name", subjectNode["Name"]);
                costDetailDic.SetValue("Code", subjectNode["Code"]);
                costDetailDic.SetValue("SubjectCode", subjectNode["SubjectCode"]);
                costDetailDic.SetValue("CBSFullCode", subjectNode["FullCode"]);
                costDetailDic.SetValue("CBSNodeID", subjectNode["ID"]);
                costDetailDic.SetValue("CBSNodeFullID", subjectNode["FullID"]);
            }

            costDetailDic.SetValue("CostType", SubjectType.SubContractCost.ToString());
            costDetailDic.SetValue("CBSInfoID", unitDic.GetValue("CBSInfoID"));
            costDetailDic.SetValue("CostUnitID", unitDic.GetValue("ID"));
            costDetailDic.SetValue("RelateID", this.ID);
            var costDate = String.IsNullOrEmpty(this.ModelDic.GetValue("ApplyDate")) ? DateTime.Now : Convert.ToDateTime(this.ModelDic.GetValue("ApplyDate"));
            costDetailDic.SetValue("CostDate", costDate);
            costDetailDic.SetValue("BelongYear", costDate.Year);
            costDetailDic.SetValue("BelongMonth", costDate.Month);
            costDetailDic.SetValue("BelongQuarter", (costDate.Month - 1) / 3 + 1);
            costDetailDic.SetValue("TotalPrice", currentValue);
            costDetailDic.SetValue("TaxRate", taxRate);
            costDetailDic.SetValue("TaxValue", taxValue);
            costDetailDic.SetValue("ClearValue", clearCostValue);
            costDetailDic.SetValue("BelongDept", unitCBSNode.GetValue("ChargerDept"));
            costDetailDic.SetValue("BelongDeptName", unitCBSNode.GetValue("ChargerDeptName"));
            costDetailDic.SetValue("BelongUser", unitCBSNode.GetValue("ChargerUser"));
            costDetailDic.SetValue("BelongUserName", unitCBSNode.GetValue("ChargerUserName"));
            costDetailDic.SetValue("State", costState);
            costDetailDic.SetValue("Status", costState);
            costDetailDic.SetValue("CreateUser", this.ModelDic.GetValue("CreateUserID"));
            costDetailDic.SetValue("CreateUserName", this.ModelDic.GetValue("CreateUser"));
            costDetailDic.SetValue("CreateDate", DateTime.Now);
            costDetailDic.InsertDB(this.DB, "S_EP_CostInfo");
            var cbsInfo = new S_EP_CBSInfo(cbsInfoDic);
            cbsInfo.SummaryCostValue();

            this.DB.ExecuteNonQuery(String.Format(@"UPDATE S_EP_SubContractTask_CostUnitDetail 
SET SubContractProgress={0},
TotalSubContractValue='{1}'
WHERE ID='{2}'", scale, totalValue, taskEntity.GetValue("ID")));

        }
    }
}
