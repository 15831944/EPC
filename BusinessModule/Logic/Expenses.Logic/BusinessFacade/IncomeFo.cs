using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using Config;
using Config.Logic;
using Formula;
using Formula.Helper;
using Formula.Exceptions;

namespace Expenses.Logic
{
    public class IncomeFo
    {
        public DataTable Calculate(string belongYear, string belongMonth)
        {
            var currentUserInfo = FormulaHelper.GetUserInfo();
            var db = SQLHelper.CreateSqlHelper(ConnEnum.Market);
            var infrasDB = SQLHelper.CreateSqlHelper(ConnEnum.InfrasBaseConfig);
            var sql = "SELECT * FROM S_EP_RevenueInfo_Detail WHERE 1<>1";
            var resultDt = db.ExecuteDataTable(sql);

            //获取会计期间
            string realDateFrom = "";
            string realDateTo = "";
            string defineMonthPeriodSql = "select * from S_EP_DefineMonthPeriod where Year = {0} and Month = {1}";
            var defineMonthPeriodDt = infrasDB.ExecuteDataTable(string.Format(defineMonthPeriodSql, belongYear, belongMonth));
            if (defineMonthPeriodDt.Rows.Count > 0)
            {
                realDateFrom = defineMonthPeriodDt.Rows[0]["StartDate"].ToString();
                realDateTo = defineMonthPeriodDt.Rows[0]["EndDate"].ToString();
            }
            else
            {
                throw new BusinessException("无法获取会计期间");
            }
            //上一期
            DateTime date = new DateTime(Convert.ToInt32(belongYear), Convert.ToInt32(belongMonth), 1);
            date = date.AddMonths(-1);
            string lastDateYearStr = date.Year.ToString();
            string lastDateMonthStr = date.Month.ToString();

            #region 节点法计算确认收入
            sql = @"select 
isnull(LastIncomeValue,0) as LastIncomeValue,
progressNode.ProgressScale,S_EP_IncomeUnit.*,ContractValue,progressNode.FactEndDate,
S_EP_CBSNode.ChargerUser,S_EP_CBSNode.ChargerUserName,--S_EP_CBSNode.Area,
S_EP_CBSNode.ChargerDept,S_EP_CBSNode.ChargerDeptName--,S_EP_CBSNode.BusinessType
from 
(select Max(AllIncomeScale) as ProgressScale,IncomeUnitID, max(FactEndDate) as FactEndDate 
from S_EP_IncomeUnit_ProgressNode
where State='{2}' and FactEndDate >= cast('{0}' as datetime) and FactEndDate <= cast('{1}' as datetime) and AllIncomeScale>0
group by IncomeUnitID) progressNode
left join S_EP_IncomeUnit on S_EP_IncomeUnit.ID = progressNode.IncomeUnitID
left join S_EP_CBSNode on S_EP_CBSNode.ID=S_EP_IncomeUnit.CBSNodeID
left join {5}..S_EP_DefineIncome on {5}..S_EP_DefineIncome.ID = S_EP_IncomeUnit.UnitDefineID
left join (select Sum(CurrentIncomeTotalValue) as LastIncomeValue,IncomeUnitID
from S_EP_RevenueInfo_Detail left join S_EP_RevenueInfo on S_EP_RevenueInfoID=S_EP_RevenueInfo.ID
where S_EP_RevenueInfo.IncomeDate<'{0}' and S_EP_RevenueInfo.State<>'{3}'
group by IncomeUnitID) LastIncomeInfo on LastIncomeInfo.IncomeUnitID=S_EP_IncomeUnit.ID
where {5}..S_EP_DefineIncome.IncomeType ='{4}' and ContractValue>0  and (S_EP_CBSNode.IsClosed is null or S_EP_CBSNode.IsClosed != 'true')";
            var inComeUnitDt = db.ExecuteDataTable(string.Format(sql, realDateFrom, realDateTo, "Finish",
                ModifyState.Removed.ToString(), IncomeType.Progress.ToString(), infrasDB.DbName));
            foreach (DataRow row in inComeUnitDt.Rows)
            {
                var contractValue = row["ContractValue"] == null || row["ContractValue"] == DBNull.Value ? 0m : Convert.ToDecimal(row["ContractValue"]);
                var progress = row["ProgressScale"] == null || row["ProgressScale"] == DBNull.Value ? 0m : Convert.ToDecimal(row["ProgressScale"]);
                var currentTotalValue = progress / 100 * contractValue;
                var lastIncomeTotalValue = row["LastIncomeValue"] == null || row["LastIncomeValue"] == DBNull.Value ? 0m : Convert.ToDecimal(row["LastIncomeValue"]);
                var currentValue = currentTotalValue - lastIncomeTotalValue;
                if (currentValue == 0) continue;  //当期收入为0的数据需要过滤掉
                var incomeRow = resultDt.NewRow();
                incomeRow["IncomeUnitID"] = row["ID"];
                incomeRow["CBSInfoID"] = row["CBSInfoID"];
                incomeRow["CBSNodeID"] = row["CBSNodeID"];
                incomeRow["CBSNodeFullID"] = row["CBSNodeFullID"];
                incomeRow["UnitName"] = row["Name"];
                incomeRow["UnitCode"] = row["Code"];
                incomeRow["TotalScale"] = progress;
                incomeRow["LastIncomeTotalValue"] = lastIncomeTotalValue;
                incomeRow["IncomeValue"] = currentValue;
                incomeRow["CurrentIncomeTotalValue"] = currentTotalValue;
                incomeRow["Method"] = IncomeType.Progress.ToString();
                incomeRow["ContractValue"] = row["ContractValue"];
                incomeRow["FactEndDate"] = row["FactEndDate"];
                //incomeRow["Area"] = row["Area"];
                //incomeRow["BusinessType"] = row["BusinessType"];
                incomeRow["TaxRate"] = row["TaxRate"];//当期税率
                decimal taxRate = 0;
                decimal.TryParse(row["TaxRate"].ToString(), out taxRate);
                var taxValue = taxRate * currentValue / (1 + taxRate);
                var clearValue = currentValue - taxValue;
                incomeRow["TaxValue"] = taxValue;//当期税金
                incomeRow["ClearIncomeValue"] = clearValue;//当期去税金额
                incomeRow["ChargerDept"] = row["ChargerDept"];
                incomeRow["ChargerDeptName"] = row["ChargerDeptName"];
                incomeRow["ChargerUser"] = row["ChargerUser"];
                incomeRow["ChargerUserName"] = row["ChargerUserName"];
                resultDt.Rows.Add(incomeRow);
            }
            #endregion

            #region 成本法计算确认收入
            sql = String.Format(@"select isnull(LastIncomeValue,0) as LastIncomeValue, ContractValue, S_EP_IncomeUnit.*,
S_EP_CBSNode.ChargerUser,S_EP_CBSNode.ChargerUserName,--S_EP_CBSNode.Area,
S_EP_CBSNode.ChargerDept,S_EP_CBSNode.ChargerDeptName,--S_EP_CBSNode.BusinessType,
isnull(BudgetValue,0) as BudgetValue
from S_EP_IncomeUnit
left join S_EP_CBSNode on S_EP_CBSNode.ID=S_EP_IncomeUnit.CBSNodeID
left join {3}..S_EP_DefineIncome on {3}..S_EP_DefineIncome.ID = S_EP_IncomeUnit.UnitDefineID
left join (select Sum(CurrentIncomeTotalValue) as LastIncomeValue,IncomeUnitID
from S_EP_RevenueInfo_Detail left join S_EP_RevenueInfo on S_EP_RevenueInfoID=S_EP_RevenueInfo.ID
where S_EP_RevenueInfo.IncomeDate<'{0}' and S_EP_RevenueInfo.State<>'{1}'
group by IncomeUnitID) LastIncomeInfo on LastIncomeInfo.IncomeUnitID=S_EP_IncomeUnit.ID
where {3}..S_EP_DefineIncome.IncomeType ='{2}' and (S_EP_CBSNode.IsClosed is null or S_EP_CBSNode.IsClosed != 'true')",
            realDateFrom, ModifyState.Removed.ToString(), IncomeType.Cost.ToString(), infrasDB.DbName);
            var incomeUnitOfCostDT = db.ExecuteDataTable(sql);
            if (incomeUnitOfCostDT.Rows.Count > 0)
            {
                sql = String.Format(@"select Sum(TotalPrice) as CostValue,CBSNodeFullID from S_EP_CostInfo
where CostDate<='{0}' group by CBSNodeFullID", realDateTo);
                var costDt = db.ExecuteDataTable(sql);
                var costList = costDt.AsEnumerable();
                foreach (DataRow row in incomeUnitOfCostDT.Rows)
                {
                    decimal costValue = 0;
                    var budgetValue = row["BudgetValue"] == null || row["BudgetValue"] == DBNull.Value ? 0m : Convert.ToDecimal(row["BudgetValue"]);
                    if (budgetValue <= 0)
                        continue;
                    if (row["CBSNodeFullID"] == null || row["CBSNodeFullID"] == DBNull.Value || String.IsNullOrEmpty(row["CBSNodeFullID"].ToString())) continue;
                    var costCalInfo = costList.Where(c => c["CostValue"] != null && c["CostValue"] != DBNull.Value && c["CBSNodeFullID"] != null && c["CBSNodeFullID"] != DBNull.Value &&
                        c["CBSNodeFullID"].ToString().StartsWith(row["CBSNodeFullID"].ToString()));
                    if (costCalInfo.Count() == 0)
                        continue;
                    costValue = costCalInfo.Sum(c => Convert.ToDecimal(c["CostValue"]));
                    var contractValue = row["ContractValue"] == null || row["ContractValue"] == DBNull.Value ? 0m : Convert.ToDecimal(row["ContractValue"]);
                    //上一期的累计值lastValue计算
                    var lastIncomeTotalValue = row["LastIncomeValue"] == null || row["LastIncomeValue"] == DBNull.Value ? 0m : Convert.ToDecimal(row["LastIncomeValue"]);
                    decimal progress = costValue / budgetValue * 100;
                    var currentTotalValue = costValue / budgetValue * contractValue;
                    var currentValue = currentTotalValue - lastIncomeTotalValue;
                    if (currentValue == 0) continue;  //当期收入为0的数据需要过滤掉
                    var incomeRow = resultDt.NewRow();
                    incomeRow["IncomeUnitID"] = row["ID"];
                    incomeRow["CBSInfoID"] = row["CBSInfoID"];
                    incomeRow["CBSNodeID"] = row["CBSNodeID"];
                    incomeRow["CBSNodeFullID"] = row["CBSNodeFullID"];
                    incomeRow["UnitName"] = row["Name"];
                    incomeRow["UnitCode"] = row["Code"];
                    incomeRow["TotalScale"] = progress;
                    incomeRow["LastIncomeTotalValue"] = lastIncomeTotalValue;
                    incomeRow["IncomeValue"] = currentValue;
                    incomeRow["CurrentIncomeTotalValue"] = currentTotalValue;
                    incomeRow["Method"] = IncomeType.Cost.ToString();
                    incomeRow["ContractValue"] = row["ContractValue"];
                    //incomeRow["Area"] = row["Area"];
                    //incomeRow["BusinessType"] = row["BusinessType"];
                    incomeRow["TaxRate"] = row["TaxRate"];//当期税率
                    decimal taxRate = 0;
                    decimal.TryParse(row["TaxRate"].ToString(), out taxRate);
                    var taxValue = taxRate * currentValue / (1 + taxRate);
                    var clearValue = currentValue - taxValue;
                    incomeRow["TaxValue"] = taxValue;//当期税金
                    incomeRow["ClearIncomeValue"] = clearValue;//当期去税金额
                    incomeRow["ChargerDept"] = row["ChargerDept"];
                    incomeRow["ChargerDeptName"] = row["ChargerDeptName"];
                    incomeRow["ChargerUser"] = row["ChargerUser"];
                    incomeRow["ChargerUserName"] = row["ChargerUserName"];
                    resultDt.Rows.Add(incomeRow);
                }
            }
            #endregion

            #region 报量法计算确认收入
            {
                string tmpSql = String.Format(@" select isnull(LastIncomeValue,0) as LastIncomeValue, SubmitInfo.ProgressScale,
S_EP_IncomeUnit.*,ContractValue,SubmitInfo.FactEndDate,
S_EP_CBSNode.ChargerUser,S_EP_CBSNode.ChargerUserName,--S_EP_CBSNode.Area,
S_EP_CBSNode.ChargerDept,S_EP_CBSNode.ChargerDeptName--,S_EP_CBSNode.BusinessType
from (select Sum(CurrentScale) as ProgressScale,IncomeUnitID, max(FactEndDate) as FactEndDate  from S_EP_IncomeSubmit
where FactEndDate <='{1}' group by IncomeUnitID) SubmitInfo
left join S_EP_IncomeUnit on S_EP_IncomeUnit.ID = SubmitInfo.IncomeUnitID
left join S_EP_CBSNode on S_EP_CBSNode.ID=S_EP_IncomeUnit.CBSNodeID
left join (select Sum(CurrentIncomeTotalValue) as LastIncomeValue,IncomeUnitID
from S_EP_RevenueInfo_Detail left join S_EP_RevenueInfo on S_EP_RevenueInfoID=S_EP_RevenueInfo.ID
where S_EP_RevenueInfo.IncomeDate<'{0}' and S_EP_RevenueInfo.State<>'{2}'
group by IncomeUnitID) LastIncomeInfo on LastIncomeInfo.IncomeUnitID=S_EP_IncomeUnit.ID
where ContractValue>0 and (S_EP_CBSNode.IsClosed is null or S_EP_CBSNode.IsClosed != 'true')", realDateFrom, realDateTo, ModifyState.Removed.ToString());

                inComeUnitDt = db.ExecuteDataTable(tmpSql);
                foreach (DataRow row in inComeUnitDt.Rows)
                {
                    var contractValue = row["ContractValue"] == null || row["ContractValue"] == DBNull.Value ? 0m : Convert.ToDecimal(row["ContractValue"]);
                    var progress = row["ProgressScale"] == null || row["ProgressScale"] == DBNull.Value ? 0m : Convert.ToDecimal(row["ProgressScale"]);
                    if (progress == 0) continue;
                    var currentTotalValue = progress / 100 * contractValue;
                    //上一期的累计值lastValue计算
                    decimal lastValue = row["LastIncomeValue"] == null || row["LastIncomeValue"] == DBNull.Value ? 0m : Convert.ToDecimal(row["LastIncomeValue"]); ;
                    var currentValue = currentTotalValue - lastValue;
                    if (currentValue == 0) continue;  //当期收入为0的数据需要过滤掉
                    var incomeRow = resultDt.NewRow();
                    incomeRow["IncomeUnitID"] = row["ID"];
                    incomeRow["CBSInfoID"] = row["CBSInfoID"];
                    incomeRow["CBSNodeID"] = row["CBSNodeID"];
                    incomeRow["CBSNodeFullID"] = row["CBSNodeFullID"];
                    incomeRow["UnitName"] = row["Name"];
                    incomeRow["UnitCode"] = row["Code"];
                    incomeRow["FactEndDate"] = row["FactEndDate"];
                    incomeRow["TotalScale"] = progress;
                    incomeRow["LastIncomeTotalValue"] = lastValue;
                    incomeRow["IncomeValue"] = currentValue;
                    incomeRow["CurrentIncomeTotalValue"] = currentTotalValue;
                    incomeRow["Method"] = IncomeType.Submit.ToString();
                    incomeRow["ContractValue"] = row["ContractValue"];
                    //incomeRow["Area"] = row["Area"];
                    //incomeRow["BusinessType"] = row["BusinessType"];
                    incomeRow["TaxRate"] = row["TaxRate"];//当期税率
                    decimal taxRate = 0;
                    decimal.TryParse(row["TaxRate"].ToString(), out taxRate);
                    var taxValue = taxRate * currentValue / (1 + taxRate);
                    var clearValue = currentValue - taxValue;
                    incomeRow["TaxValue"] = taxValue;//当期税金
                    incomeRow["ClearIncomeValue"] = clearValue;
                    incomeRow["ChargerDept"] = row["ChargerDept"];
                    incomeRow["ChargerDeptName"] = row["ChargerDeptName"];
                    incomeRow["ChargerUser"] = row["ChargerUser"];
                    incomeRow["ChargerUserName"] = row["ChargerUserName"];
                    resultDt.Rows.Add(incomeRow);
                }
            }
            #endregion

            #region 发票申报确认收入
            {
                string tmpSql = @"select isnull(LastIncomeValue,0) as LastIncomeValue,isnull(LastClearIncomeValue,0) as LastClearIncomeValue, SubmitInfo.CurrentTotalValue,SubmitInfo.CurrentTotalClearValue,
S_EP_IncomeUnit.*,ContractValue,
S_EP_CBSNode.ChargerUser,S_EP_CBSNode.ChargerUserName,--S_EP_CBSNode.Area,
S_EP_CBSNode.ChargerDept,S_EP_CBSNode.ChargerDeptName--,S_EP_CBSNode.BusinessType
from (select Sum(CurrentValue) as CurrentTotalValue,Sum(ClearValue) as CurrentTotalClearValue,IncomeUnitID from S_EP_IncomeInvoiceConfirm
where ConfirmDate <='{1}' group by IncomeUnitID) SubmitInfo
left join S_EP_IncomeUnit on S_EP_IncomeUnit.ID = SubmitInfo.IncomeUnitID
left join S_EP_CBSNode on S_EP_CBSNode.ID=S_EP_IncomeUnit.CBSNodeID
left join (select Sum(CurrentIncomeTotalValue) as LastIncomeValue,Sum(ClearIncomeValue) as LastClearIncomeValue,IncomeUnitID
from S_EP_RevenueInfo_Detail left join S_EP_RevenueInfo on S_EP_RevenueInfoID=S_EP_RevenueInfo.ID
where S_EP_RevenueInfo.IncomeDate<'{1}' and S_EP_RevenueInfo.State<>'{2}' and S_EP_RevenueInfo_Detail.Method = '{3}'
group by IncomeUnitID) LastIncomeInfo on LastIncomeInfo.IncomeUnitID=S_EP_IncomeUnit.ID
where ContractValue>0 and (S_EP_CBSNode.IsClosed is null or S_EP_CBSNode.IsClosed != 'true')";

                var dt = db.ExecuteDataTable(string.Format(tmpSql, realDateFrom, realDateTo, ModifyState.Removed.ToString(), IncomeType.Invoice.ToString()));
                foreach (DataRow row in dt.Rows)
                {
                    var contractValue = row["ContractValue"] == null || row["ContractValue"] == DBNull.Value ? 0m : Convert.ToDecimal(row["ContractValue"]);
                    var currentTotalValue = row["CurrentTotalValue"] == null || row["CurrentTotalValue"] == DBNull.Value ? 0m : Convert.ToDecimal(row["CurrentTotalValue"]);
                    var currentTotalClearValue = row["CurrentTotalClearValue"] == null || row["CurrentTotalClearValue"] == DBNull.Value ? 0m : Convert.ToDecimal(row["CurrentTotalClearValue"]);
                    var lastIncomeTotalValue = row["LastIncomeValue"] == null || row["LastIncomeValue"] == DBNull.Value ? 0m : Convert.ToDecimal(row["LastIncomeValue"]);
                    var lastClearValue = row["LastClearIncomeValue"] == null || row["LastClearIncomeValue"] == DBNull.Value ? 0m : Convert.ToDecimal(row["LastClearIncomeValue"]);
                    var currentValue = currentTotalValue - lastIncomeTotalValue;
                    var currentClearValue = currentTotalClearValue - lastClearValue;
                    if (contractValue == 0 || currentValue == 0) continue;  //合同未0当期收入为0的数据需要过滤掉
                    var incomeRow = resultDt.NewRow();
                    incomeRow["IncomeUnitID"] = row["ID"];
                    incomeRow["CBSInfoID"] = row["CBSInfoID"];
                    incomeRow["CBSNodeID"] = row["CBSNodeID"];
                    incomeRow["CBSNodeFullID"] = row["CBSNodeFullID"];
                    incomeRow["UnitName"] = row["Name"];
                    incomeRow["UnitCode"] = row["Code"];
                    incomeRow["TotalScale"] = ((currentTotalValue / contractValue) * 100).ToString("0.00");
                    incomeRow["LastIncomeTotalValue"] = lastIncomeTotalValue;
                    incomeRow["IncomeValue"] = currentValue;
                    incomeRow["CurrentIncomeTotalValue"] = currentTotalValue;
                    incomeRow["Method"] = IncomeType.Invoice.ToString();
                    incomeRow["ContractValue"] = row["ContractValue"];
                    //incomeRow["Area"] = row["Area"];
                    //incomeRow["BusinessType"] = row["BusinessType"];
                    incomeRow["TaxRate"] = row["TaxRate"];//当期税率
                    decimal taxRate = 0;
                    decimal.TryParse(row["TaxRate"].ToString(), out taxRate);
                    var taxValue = taxRate * currentValue / (1 + taxRate);
                    incomeRow["TaxValue"] = taxValue;//当期税金(合同税金非发票税金)
                    incomeRow["ClearIncomeValue"] = currentClearValue;//当期去税金额
                    incomeRow["ChargerDept"] = row["ChargerDept"];
                    incomeRow["ChargerDeptName"] = row["ChargerDeptName"];
                    incomeRow["ChargerUser"] = row["ChargerUser"];
                    incomeRow["ChargerUserName"] = row["ChargerUserName"];
                    resultDt.Rows.Add(incomeRow);
                }
            }
            #endregion
            return resultDt;
        }

        public void ValidateIncome(int BelongYear, int BelongMonth)
        {
            var db = SQLHelper.CreateSqlHelper(ConnEnum.Market);
            var dateTime = new DateTime(BelongYear, BelongMonth, 1);
            var validateDt = new DataTable();

            if (SysParams.Params.GetValue("IncomeValidateMode") == IncomeValidateMode.BeforeAndAfter.ToString() ||
                SysParams.Params.GetValue("IncomeValidateMode") == IncomeValidateMode.OnlyBefore.ToString())
            {
                //如果之前的月份有未发布的收入信息，则当前不允许编制收入
                validateDt = db.ExecuteDataTable(String.Format(
                    "SELECT TOP 1 ID,BelongYear,BelongMonth FROM S_EP_RevenueInfo with(nolock) where State<>'{1}' and State<>'{2}' and IncomeDate<'{0}' order by IncomeDate desc",
                    dateTime.ToShortDateString(), "Finish", "Removed"));
                if (validateDt.Rows.Count > 0)
                {
                    throw new Formula.Exceptions.BusinessValidationException(String.Format("{0}年{1}月的收入尚未被确认发布，无法编制收入",
                        validateDt.Rows[0]["BelongYear"], validateDt.Rows[0]["BelongMonth"]
                        ));
                }
            }

            if (SysParams.Params.GetValue("IncomeValidateMode") == IncomeValidateMode.BeforeAndAfter.ToString() ||
               SysParams.Params.GetValue("IncomeValidateMode") == IncomeValidateMode.OnlyAfter.ToString())
            {
                //如果之后的月份已经有发布过的收入信息，则当前不允许编制收入
                validateDt = db.ExecuteDataTable(String.Format(
                   "SELECT TOP 1 ID,BelongYear,BelongMonth FROM S_EP_RevenueInfo with(nolock) where State='{1}' and IncomeDate>'{0}' order by IncomeDate desc",
                   dateTime.ToShortDateString(), "Finish"));
                if (validateDt.Rows.Count > 0)
                {
                    throw new Formula.Exceptions.BusinessValidationException(String.Format("{0}年{1}月的收入已被确认发布，无法编制收入",
                        validateDt.Rows[0]["BelongYear"], validateDt.Rows[0]["BelongMonth"]
                        ));
                }
            }
        }
    }
}
