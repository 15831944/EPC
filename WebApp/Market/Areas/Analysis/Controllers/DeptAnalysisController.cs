using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Data;
using System.Collections;
using Formula.Helper;
using Formula;
using MvcAdapter;
using Config;
using Config.Logic;
using Market.Logic;
using Market.Logic.Domain;

namespace Market.Areas.Analysis.Controllers
{
    public class DeptAnalysisController : MarketController
    {
        public ActionResult DeptIndicatorInfo()
        {
            ViewBag.ServerYear = DateTime.Now.Year;
            return View();
        }

        public ActionResult ContractView()
        {
            var tab = new Tab();
            var deptCategory = CategoryFactory.GetCategory("Market.ManDept", "部门", "ProductionDept");
            deptCategory.SetDefaultItem();
            tab.Categories.Add(deptCategory);

            var yearCategory = CategoryFactory.GetYearCategory("BelongYear", 7);
            yearCategory.SetDefaultItem();
            tab.Categories.Add(yearCategory);

            tab.IsDisplay = true;
            ViewBag.Tab = tab;
            return View();
        }

        public ActionResult ProjectView()
        {
            var tab = new Tab();
            var deptCategory = CategoryFactory.GetCategory("Market.ManDept", "部门", "ChargerDept");
            deptCategory.SetDefaultItem();
            tab.Categories.Add(deptCategory);

            var yearCategory = CategoryFactory.GetYearCategory("BelongYear", 7);
            yearCategory.SetDefaultItem();
            tab.Categories.Add(yearCategory);

            tab.IsDisplay = true;
            ViewBag.Tab = tab;
            return View();
        }

        public JsonResult GetContractList(QueryBuilder qb, string Year)
        {
            var receiptSQL = @"select isnull(Sum(Amount),0) as Value,ProductionDept from (
select S_C_Receipt.*,ProductionDept,ProductionDeptName from S_C_Receipt left join S_C_ManageContract on ContractInfoID = S_C_ManageContract.ID)
ReceiptInfo where 1=1 {0}  group by ProductionDept";
            var invoiceSQL = @"select isnull(Sum(Amount),0) as Value,ProductionDept from (
select S_C_Invoice.*,ProductionDept,ProductionDeptName from S_C_Invoice left join S_C_ManageContract on ContractInfoID = S_C_ManageContract.ID)
ReceiptInfo where  State='Normal' {0} group by ProductionDept";
            var contractSQL = @"select isnull(Sum(ContractRMBAmount),0) as Value,
isnull(sum(SummaryBadDebtValue),0) as BadValue,ProductionDept from S_C_ManageContract
where IsSigned='Signed' {0} group by ProductionDept";

            string innerWhere = string.Empty;
            if (!String.IsNullOrEmpty(Year) && Year.ToLowerInvariant() != "all")
                innerWhere += " and BelongYear in ('" + Year.Replace(",", "','") + "')";
            receiptSQL = String.Format(receiptSQL, innerWhere);
            invoiceSQL = String.Format(invoiceSQL, innerWhere);
            contractSQL = String.Format(contractSQL, innerWhere);
            var receiptDt = this.SqlHelper.ExecuteDataTable(receiptSQL);
            var invoiceDt = this.SqlHelper.ExecuteDataTable(invoiceSQL);
            var contractDt = this.SqlHelper.ExecuteDataTable(contractSQL);

            var deptField = "ProductionDept";
            var deptNameField = "ProductionDeptName";
            var resultDt = CreateDefaultTable(qb, deptField, deptNameField);
            foreach (DataRow item in resultDt.Rows)
            {
                var receiptRow = receiptDt.Select("ProductionDept='" + item[deptField] + "'").FirstOrDefault();
                if (receiptRow != null)
                    item["ReceiptValue"] = receiptRow["Value"];
                var invoiceRow = invoiceDt.Select("ProductionDept='" + item[deptField] + "'").FirstOrDefault();
                if (invoiceRow != null)
                    item["InvoiceValue"] = invoiceRow["Value"];
                var contractRow = contractDt.Select("ProductionDept='" + item[deptField] + "'").FirstOrDefault();
                if (contractRow != null)
                {
                    item["ContractValue"] = contractRow["Value"];
                    item["BadDebtValue"] = contractRow["BadValue"];
                }
                item["RemainValue"] = Convert.ToDecimal(item["ContractValue"]) -
                    Convert.ToDecimal(item["ReceiptValue"]) - Convert.ToDecimal(item["BadDebtValue"]);
                item["ReciveableValue"] = Convert.ToDecimal(item["InvoiceValue"]) -
                    Convert.ToDecimal(item["ReceiptValue"]) - Convert.ToDecimal(item["BadDebtValue"]);
            }

            var result = new Dictionary<string, object>();
            result.SetValue("data", resultDt);
            var series = "合同总额,收款总额,开票总额,坏账总额,应收款总额,剩余合同额";
            var serieFields = "ContractValue,ReceiptValue,InvoiceValue,BadDebtValue,ReciveableValue,RemainValue";
            var chartData = HighChartHelper.CreateColumnChart("", resultDt, deptNameField, series.Split(','), serieFields.Split(','));
            result.SetValue("chartData", chartData.Render());
            return Json(result);
        }

        public JsonResult GetProjectList(QueryBuilder qb, string Year)
        {
            var receiptSQL = @"select isnull(Sum(RelationValue),0) as Value,ChargerDept
from S_C_ReceiptPlanRelation left join S_C_Receipt on ReceiptID = S_C_Receipt.ID
left join S_C_ManageContract_ReceiptObj on ReceiptObjectID = S_C_ManageContract_ReceiptObj.ID
left join S_I_Project on ProjectInfo = S_I_Project.ID
where 1=1 {0} group by ChargerDept";
            var invoiceSQL = @"select isnull(Sum(RelationValue),0) as Value,ChargerDept
from S_C_Invoice_ReceiptObject left join S_C_Invoice on S_C_InvoiceID = S_C_Invoice.ID
left join S_C_ManageContract_ReceiptObj on ReceiptObjectID = S_C_ManageContract_ReceiptObj.ID
left join S_I_Project on ProjectInfo = S_I_Project.ID
where S_C_Invoice.State = 'Normal' {0} group by ChargerDept";
            var contractSQL = @"select isnull(Sum(ReceiptValue),0) as Value,isnull(Sum(FactBadValue),0) as BadValue,S_I_Project.ChargerDept  from S_C_ManageContract_ReceiptObj
left join S_I_Project on ProjectInfo = S_I_Project.ID
left join S_C_ManageContract on ContractInfoID = S_C_ManageContract.ID
where S_C_ManageContract.IsSigned='Signed' {0} group by S_I_Project.ChargerDept";

            string innerWhere = string.Empty;
            if (!String.IsNullOrEmpty(Year) && Year.ToLowerInvariant() != "all")
                innerWhere += " and BelongYear in ('" + Year.Replace(",", "','") + "')";
            receiptSQL = String.Format(receiptSQL, innerWhere);
            invoiceSQL = String.Format(invoiceSQL, innerWhere);
            contractSQL = String.Format(contractSQL, innerWhere);
            var receiptDt = this.SqlHelper.ExecuteDataTable(receiptSQL);
            var invoiceDt = this.SqlHelper.ExecuteDataTable(invoiceSQL);
            var contractDt = this.SqlHelper.ExecuteDataTable(contractSQL);

            var resultDt = CreateDefaultTable(qb);
            foreach (DataRow item in resultDt.Rows)
            {
                var receiptRow = receiptDt.Select("ChargerDept='" + item["ChargerDept"] + "'").FirstOrDefault();
                if (receiptRow != null)
                    item["ReceiptValue"] = receiptRow["Value"];
                var invoiceRow = invoiceDt.Select("ChargerDept='" + item["ChargerDept"] + "'").FirstOrDefault();
                if (invoiceRow != null)
                    item["InvoiceValue"] = invoiceRow["Value"];
                var contractRow = contractDt.Select("ChargerDept='" + item["ChargerDept"] + "'").FirstOrDefault();
                if (contractRow != null)
                {
                    item["ContractValue"] = contractRow["Value"];
                    item["BadDebtValue"] = contractRow["BadValue"];
                }
                item["RemainValue"] = Convert.ToDecimal(item["ContractValue"]) -
                    Convert.ToDecimal(item["ReceiptValue"]) - Convert.ToDecimal(item["BadDebtValue"]);
                item["ReciveableValue"] = Convert.ToDecimal(item["InvoiceValue"]) -
                    Convert.ToDecimal(item["ReceiptValue"]) - Convert.ToDecimal(item["BadDebtValue"]);
            }

            var result = new Dictionary<string, object>();
            result.SetValue("data", resultDt);
            var series = "合同总额,收款总额,开票总额,坏账总额,应收款总额,剩余合同额";
            var serieFields = "ContractValue,ReceiptValue,InvoiceValue,BadDebtValue,ReciveableValue,RemainValue";
            var chartData = HighChartHelper.CreateColumnChart("", resultDt, "ChargerDeptName", series.Split(','), serieFields.Split(','));
            result.SetValue("chartData", chartData.Render());
            return Json(result);
        }

        private DataTable CreateDefaultTable(QueryBuilder qb, string deptField = "ChargerDept", string deptNameField = "ChargerDeptName")
        {
            string sql = @"select * from (select ID as " + deptField + ",Name as " + deptNameField + @",SortIndex,0 as ContractValue,
0 as ReceiptValue,0 as InvoiceValue,0 as BadDebtValue,0 as RemainValue,0 as ReciveableValue
from S_A_Org where  Type='ManufactureDept' ) TableInfo ";
            var baseSQLHelper = SQLHelper.CreateSqlHelper(ConnEnum.Base);
            var dt = baseSQLHelper.ExecuteDataTable(sql, qb);
            return dt;
        }

        #region 各部门目标完成情况统计表

        public JsonResult GetDeptIndicatorList(string anlysisValue = "ReceiptValue")
        {
            var dt = EnumBaseHelper.GetEnumTable("System.ManDept");
            dt.Columns.Add("ContractKPI", typeof(decimal));
            dt.Columns.Add("ContractValue", typeof(decimal));
            dt.Columns.Add("ContractComplateRate", typeof(decimal));
            dt.Columns.Add("UnContractValue", typeof(decimal));
            dt.Columns.Add("ContractKPIRemain", typeof(decimal));
            dt.Columns.Add("ReceiptKPI", typeof(decimal));
            dt.Columns.Add("ReceiptValue", typeof(decimal));
            dt.Columns.Add("RecepitComplateRate", typeof(decimal));
            dt.Columns.Add("CanReceiptValue", typeof(decimal));
            dt.Columns.Add("RemaintContractValue", typeof(decimal));
            dt.Columns.Add("ReceiptKPIRemain", typeof(decimal));
            dt.Columns.Add("TimeRate", typeof(decimal));

            var belongYear = String.IsNullOrEmpty(GetQueryString("BelongYear")) ? DateTime.Now.Year : Convert.ToInt32(GetQueryString("BelongYear"));
            var sql = "select * from S_KPI_IndicatorOrg where IndicatorType = 'YearIndicator' and BelongYear= '{0}'";
            var indicatorDt = this.SqlHelper.ExecuteDataTable(String.Format(sql, belongYear));

            sql = @"select Sum(dept.RelationValue) as DataValue,Dept as ChargeDeptID,DeptName as ChargeDeptName
from S_C_Receipt_DeptRelation dept left join S_C_Receipt receipt on dept.S_C_Receipt_ID = receipt.ID
where BelongYear='{0}' group by Dept,DeptName";
            var receiptDt = this.SqlHelper.ExecuteDataTable(String.Format(sql, belongYear));

            sql = @"select isnull(Sum(isnull(ReceiptValue,0)-isnull(FactReceiptValue,0)),0)  as DataValue,
ChargerDept as ChargeDeptID,ChargerDeptName as ChargeDeptName from S_C_ManageContract_ReceiptObj
left join S_I_Project on S_C_ManageContract_ReceiptObj.ProjectInfo=S_I_Project.ID
where MileStoneState='{0}' group by ChargerDept,ChargerDeptName";
            var canReceiptDt = this.SqlHelper.ExecuteDataTable(String.Format(sql, true.ToString()));

            sql = @"select Sum(isnull(DeptValue,0)-isnull(SumDeptReceiptValue,0)) as DataValue,
Dept as ChargeDeptID,DeptName as ChargeDeptName 
 from S_C_ManageContract_DeptRelation dept 
left join S_C_ManageContract con on dept.S_C_ManageContractID = con.ID
where IsSigned='{0}' group by Dept,DeptName";
            var remainContractDt = this.SqlHelper.ExecuteDataTable(String.Format(sql, ContractIsSigned.Signed.ToString()));

            sql = @"select isnull(sum(DataValue),0) as DataValue,Dept as ChargeDeptID,DeptName as ChargeDeptName,BelongYear,BelongQuarter,BelongMonth from
(select isnull(DeptValue,0) as DataValue,Dept,DeptName,BelongYear,BelongQuarter,BelongMonth
from S_C_ManageContract_DeptRelation dept
left join S_C_ManageContract con on dept.S_C_ManageContractID = con.ID  
where IsSigned='{1}' and BelongYear='{0}'
union all
select isnull(DeptValue,0) as DataValue,Dept,DeptName,ad.BelongYear,ad.BelongQuarter,ad.BelongMonth
from S_C_ManageContract_Supplementary_DeptRelation dept
left join S_C_ManageContract_Supplementary ad on ad.ID=dept.S_C_ManageContract_SupplementaryID 
inner join S_C_ManageContract con on ad.ContractInfoID=con.ID where IsSigned='{1}' and ad.BelongYear='{0}'
)tb group by BelongYear,BelongQuarter,BelongMonth,Dept,DeptName";
            var contractDt = this.SqlHelper.ExecuteDataTable(String.Format(sql, belongYear, ContractIsSigned.Signed.ToString()));

            sql = @"select isnull(sum(DataValue),0) as DataValue,Dept as ChargeDeptID,DeptName as ChargeDeptName,BelongYear,BelongQuarter,BelongMonth from
(select isnull(DeptValue,0) as DataValue,Dept,DeptName,BelongYear,BelongQuarter,BelongMonth
from S_C_ManageContract_DeptRelation dept
left join S_C_ManageContract con on dept.S_C_ManageContractID = con.ID  
where (IsSigned='{0}' or IsSigned is null)
union all
select isnull(DeptValue,0) as DataValue,Dept,DeptName,ad.BelongYear,ad.BelongQuarter,ad.BelongMonth
from S_C_ManageContract_Supplementary_DeptRelation dept
left join S_C_ManageContract_Supplementary ad on ad.ID=dept.S_C_ManageContract_SupplementaryID 
inner join S_C_ManageContract con on ad.ContractInfoID=con.ID where (IsSigned='{0}' or IsSigned is null)
)tb group by BelongYear,BelongQuarter,BelongMonth,Dept,DeptName";
            var UncCntractDt = this.SqlHelper.ExecuteDataTable(String.Format(sql, ContractIsSigned.UnSigned.ToString()));

            var sumReceptKpi = 0m; var sumReceiptValue = 0m; var sumContractKpi = 0m; var sumContractValue = 0m;
            foreach (DataRow row in dt.Rows)
            {
                var obj = indicatorDt.Compute("Sum(ReceiptValue)", "OrgID='" + row["value"] + "'");
                var receptkpi = obj == null || obj == DBNull.Value ? 0 : Convert.ToDecimal(obj);
                row["ReceiptKPI"] = receptkpi;
                sumReceptKpi += receptkpi;
                obj = receiptDt.Compute("Sum(DataValue)", "ChargeDeptID='" + row["value"] + "'");
                var recepitValue = obj == null || obj == DBNull.Value ? 0 : Convert.ToDecimal(obj);
                row["ReceiptValue"] = recepitValue;
                sumReceiptValue += recepitValue;
                row["RecepitComplateRate"] = receptkpi == 0 ? 100 : Math.Round(recepitValue / receptkpi * 100, 2);

                obj = canReceiptDt.Compute("Sum(DataValue)", "ChargeDeptID='" + row["value"] + "'");
                row["CanReceiptValue"] = obj == null || obj == DBNull.Value ? 0 : Convert.ToDecimal(obj);
                row["RemaintContractValue"] = remainContractDt.Compute("Sum(DataValue)", "ChargeDeptID='" + row["value"] + "'");
                row["ReceiptKPIRemain"] = receptkpi - recepitValue;
                //row["TimeRate"] = Math.Round(Convert.ToDecimal(DateTime.Now.DayOfYear) / 365 * 100);

                obj = indicatorDt.Compute("Sum(ContractValue)", "OrgID='" + row["value"] + "'");
                var contractKpi = obj == null || obj == DBNull.Value ? 0 : Convert.ToDecimal(obj);
                row["ContractKPI"] = contractKpi;
                sumContractKpi += contractKpi;

                obj = contractDt.Compute("Sum(DataValue)", "ChargeDeptID='" + row["value"] + "'");
                var value = obj == null || obj == DBNull.Value ? 0 : Convert.ToDecimal(obj);
                row["ContractValue"] = value;
                sumContractValue += value;
                row["ContractComplateRate"] = contractKpi == 0 ? 100 : Math.Round(value / contractKpi * 100, 2);

                obj = UncCntractDt.Compute("Sum(DataValue)", "ChargeDeptID='" + row["value"] + "'");
                row["UnContractValue"] = obj == null || obj == DBNull.Value ? 0 : Convert.ToDecimal(obj);
                row["ContractKPIRemain"] = contractKpi - value;
            }
            var result = new Dictionary<string, object>();
            result.SetValue("data", dt);
            var sumData = new Dictionary<string, object>();
            sumData.SetValue("RecepitComplateRate", sumReceptKpi == 0 ? 100 : Math.Round(sumReceiptValue / sumReceptKpi * 100, 2));
            sumData.SetValue("ContractComplateRate", sumContractKpi == 0 ? 100 : Math.Round(sumReceiptValue / sumContractKpi * 100, 2));
            result.SetValue("sumData", sumData);

            #region 生成图表
            var yAxies = new List<yAxis>();
            var y1 = new yAxis { MiniValue = 0, TitleInfo = new Dictionary<string, object>(), Lable = new Dictionary<string, object>() };
            y1.TitleInfo.SetValue("text", "金额");
            y1.Lable.SetValue("format", "{value}元");
            var y2 = new yAxis { MiniValue = 0, TitleInfo = new Dictionary<string, object>(), Lable = new Dictionary<string, object>() };
            y2.TitleInfo.SetValue("text", "完成率");
            y2.Lable.SetValue("format", "{value}%"); y2.opposite = true;
            yAxies.Add(y1);
            yAxies.Add(y2);

            var serDefines = new List<Series>();
            if (anlysisValue == AnlysisValue.ReceiptValue.ToString())
            {
                var ReceiptKPISer = new Series { Name = "收款目标", Field = "ReceiptKPI", Type = "column", yAxis = 0, Tooltip = new Dictionary<string, object>() };
                ReceiptKPISer.Tooltip.SetValue("valueSuffix", "元");
                serDefines.Add(ReceiptKPISer);

                var ReceiptValueSer = new Series { Name = "已收款", Field = "ReceiptValue", Type = "column", yAxis = 0, Tooltip = new Dictionary<string, object>() };
                ReceiptValueSer.Tooltip.SetValue("valueSuffix", "元");
                serDefines.Add(ReceiptValueSer);

                var CanReceiptValueSer = new Series { Name = "经营应收款", Field = "CanReceiptValue", Type = "column", yAxis = 0, Tooltip = new Dictionary<string, object>() };
                CanReceiptValueSer.Tooltip.SetValue("valueSuffix", "元");
                serDefines.Add(CanReceiptValueSer);

                var RecepitComplateRateSer = new Series { Name = "收款完成率", Field = "RecepitComplateRate", Type = "spline", yAxis = 1, Tooltip = new Dictionary<string, object>() };
                RecepitComplateRateSer.Tooltip.SetValue("valueSuffix", "%");
                serDefines.Add(RecepitComplateRateSer);
            }
            else
            {
                var contractKPISer = new Series { Name = "合同目标", Field = "ContractKPI", Type = "column", yAxis = 0, Tooltip = new Dictionary<string, object>() };
                contractKPISer.Tooltip.SetValue("valueSuffix", "元");
                serDefines.Add(contractKPISer);

                var contractValueSer = new Series { Name = "已签订金额", Field = "ContractValue", Type = "column", yAxis = 0, Tooltip = new Dictionary<string, object>() };
                contractValueSer.Tooltip.SetValue("valueSuffix", "元");
                serDefines.Add(contractValueSer);

                var unContractValueSer = new Series { Name = "待签约金额", Field = "UnContractValue", Type = "column", yAxis = 0, Tooltip = new Dictionary<string, object>() };
                unContractValueSer.Tooltip.SetValue("valueSuffix", "元");
                serDefines.Add(unContractValueSer);

                var contractComplateRateSer = new Series { Name = "合同完成率", Field = "ContractComplateRate", Type = "spline", yAxis = 1, Tooltip = new Dictionary<string, object>() };
                contractComplateRateSer.Tooltip.SetValue("valueSuffix", "%");
                serDefines.Add(contractComplateRateSer);

            }

            //var TimeRateSer = new Series { Name = "时间", Field = "TimeRate", Type = "spline", yAxis = 1, Tooltip = new Dictionary<string, object>() };
            //TimeRateSer.Tooltip.SetValue("valueSuffix", "%");
            //serDefines.Add(TimeRateSer);
            string title = belongYear + "年各部门收款完成情况";
            if (anlysisValue == AnlysisValue.ContractValue.ToString())
            {
                title = belongYear + "年各部门合同完成情况";
            }
            var chart = HighChartHelper.CreateColumnXYChart(title, "", dt, "text", yAxies, serDefines, null);
            result.SetValue("chart", chart);
            #endregion

            return Json(result);
        }

        #endregion

        #region 部门收款情况统计表

        public JsonResult GetDeptReceiptList()
        {

            var dt = EnumBaseHelper.GetEnumTable("System.ManDept");
            dt.Columns.Add("ReceiptKPI", typeof(decimal));
            dt.Columns.Add("ReceiptValue", typeof(decimal));
            dt.Columns.Add("RecepitComplateRate", typeof(decimal));
            dt.Columns.Add("CanReceiptValue", typeof(decimal));
            dt.Columns.Add("RemaintContractValue", typeof(decimal));
            dt.Columns.Add("ReceiptKPIRemain", typeof(decimal));
            dt.Columns.Add("TimeRate", typeof(decimal));

            var belongYear = String.IsNullOrEmpty(GetQueryString("BelongYear")) ? DateTime.Now.Year : Convert.ToInt32(GetQueryString("BelongYear"));
            var sql = "select * from S_KPI_IndicatorOrg where IndicatorType = 'YearIndicator' and BelongYear= '{0}'";
            var indicatorDt = this.SqlHelper.ExecuteDataTable(String.Format(sql, belongYear));

            sql = @"select Sum(dept.RelationValue) as DataValue,Dept as ChargeDeptID,DeptName as ChargeDeptName
from S_C_Receipt_DeptRelation dept left join S_C_Receipt receipt on dept.S_C_Receipt_ID = receipt.ID
where BelongYear='{0}' group by Dept,DeptName";
            var receiptDt = this.SqlHelper.ExecuteDataTable(String.Format(sql, belongYear));

            sql = @"select isnull(Sum(isnull(ReceiptValue,0)-isnull(FactReceiptValue,0)),0)  as DataValue,
ChargerDept as ChargeDeptID,ChargerDeptName as ChargeDeptName from S_C_ManageContract_ReceiptObj
left join S_I_Project on S_C_ManageContract_ReceiptObj.ProjectInfo=S_I_Project.ID
where MileStoneState='{0}' group by ChargerDept,ChargerDeptName";
            var canReceiptDt = this.SqlHelper.ExecuteDataTable(String.Format(sql, true.ToString()));

            sql = @"select Sum(isnull(SumDeptReceiptValue,0)-isnull(SumDeptReceiptValue,0)) as DataValue,
Dept as ChargeDeptID,DeptName as ChargeDeptName 
 from S_C_ManageContract_DeptRelation dept
left join S_C_ManageContract con on dept.S_C_ManageContractID = con.ID
where IsSigned='{0}' group by Dept,DeptName";
            var remainContractDt = this.SqlHelper.ExecuteDataTable(String.Format(sql, ContractIsSigned.Signed.ToString()));
            var sumKpi = 0m; var sumReceiptValue = 0m;
            foreach (DataRow row in dt.Rows)
            {
                var obj = indicatorDt.Compute("Sum(ReceiptValue)", "OrgID='" + row["value"] + "'");
                var kpi = obj == null || obj == DBNull.Value ? 0 : Convert.ToDecimal(obj);
                row["ReceiptKPI"] = kpi;
                sumKpi += kpi;
                obj = receiptDt.Compute("Sum(DataValue)", "ChargeDeptID='" + row["value"] + "'");
                var recepitValue = obj == null || obj == DBNull.Value ? 0 : Convert.ToDecimal(obj);
                row["ReceiptValue"] = recepitValue;
                sumReceiptValue += recepitValue;
                row["RecepitComplateRate"] = kpi == 0 ? 100 : Math.Round(recepitValue / kpi * 100, 2);

                obj = canReceiptDt.Compute("Sum(DataValue)", "ChargeDeptID='" + row["value"] + "'");
                row["CanReceiptValue"] = obj == null || obj == DBNull.Value ? 0 : Convert.ToDecimal(obj);
                row["RemaintContractValue"] = remainContractDt.Compute("Sum(DataValue)", "ChargeDeptID='" + row["value"] + "'");
                row["ReceiptKPIRemain"] = kpi - recepitValue;
                row["TimeRate"] = Math.Round(Convert.ToDecimal(DateTime.Now.DayOfYear) / 365 * 100);
            }
            var result = new Dictionary<string, object>();
            result.SetValue("data", dt);
            var sumData = new Dictionary<string, object>();
            sumData.SetValue("RecepitComplateRate", sumKpi == 0 ? 100 : Math.Round(sumReceiptValue / sumKpi * 100, 2));
            result.SetValue("sumData", sumData);

            var yAxies = new List<yAxis>();
            var y1 = new yAxis { MiniValue = 0, TitleInfo = new Dictionary<string, object>(), Lable = new Dictionary<string, object>() };
            y1.TitleInfo.SetValue("text", "收款金额");
            y1.Lable.SetValue("format", "{value}元");
            var y2 = new yAxis { MiniValue = 0, TitleInfo = new Dictionary<string, object>(), Lable = new Dictionary<string, object>() };
            y2.TitleInfo.SetValue("text", "完成率");
            y2.Lable.SetValue("format", "{value}%"); y2.opposite = true;
            yAxies.Add(y1);
            yAxies.Add(y2);

            var serDefines = new List<Series>();
            var ReceiptKPISer = new Series { Name = "收款目标", Field = "ReceiptKPI", Type = "column", yAxis = 0, Tooltip = new Dictionary<string, object>() };
            ReceiptKPISer.Tooltip.SetValue("valueSuffix", "元");
            serDefines.Add(ReceiptKPISer);

            var ReceiptValueSer = new Series { Name = "已收款", Field = "ReceiptValue", Type = "column", yAxis = 0, Tooltip = new Dictionary<string, object>() };
            ReceiptValueSer.Tooltip.SetValue("valueSuffix", "元");
            serDefines.Add(ReceiptValueSer);

            var CanReceiptValueSer = new Series { Name = "经营应收款", Field = "CanReceiptValue", Type = "column", yAxis = 0, Tooltip = new Dictionary<string, object>() };
            CanReceiptValueSer.Tooltip.SetValue("valueSuffix", "元");
            serDefines.Add(CanReceiptValueSer);

            var RecepitComplateRateSer = new Series { Name = "完成率", Field = "RecepitComplateRate", Type = "spline", yAxis = 1, Tooltip = new Dictionary<string, object>() };
            RecepitComplateRateSer.Tooltip.SetValue("valueSuffix", "%");
            serDefines.Add(RecepitComplateRateSer);

            var chart = HighChartHelper.CreateColumnXYChart(belongYear + "年各部门收款情况", "", dt, "text", yAxies, serDefines, null);
            result.SetValue("chart", chart);
            return Json(result);
        }

        #endregion

        #region 部门合同情况统计
        public JsonResult GetDeptContractList()
        {
            var dt = EnumBaseHelper.GetEnumTable("System.ManDept");
            dt.Columns.Add("ContractKPI", typeof(decimal));
            dt.Columns.Add("ContractValue", typeof(decimal));
            dt.Columns.Add("ContractComplateRate", typeof(decimal));
            dt.Columns.Add("UnContractValue", typeof(decimal));
            dt.Columns.Add("ContractKPIRemain", typeof(decimal));
            dt.Columns.Add("TimeRate", typeof(decimal));

            var belongYear = String.IsNullOrEmpty(GetQueryString("BelongYear")) ? DateTime.Now.Year : Convert.ToInt32(GetQueryString("BelongYear"));
            var sql = "select * from S_KPI_IndicatorOrg where IndicatorType = 'YearIndicator' and BelongYear= '{0}'";
            var indicatorDt = this.SqlHelper.ExecuteDataTable(String.Format(sql, belongYear));

            sql = @"select SUM(DataValue) DataValue,BelongYear,BelongQuarter,BelongMonth,ChargeDeptID,ChargeDeptName from (
select isnull(DeptValue,0) as DataValue,
Dept as ChargeDeptID,DeptName as ChargeDeptName,
BelongYear,BelongQuarter,BelongMonth from S_C_ManageContract_DeptRelation dept
left join S_C_ManageContract con on dept.S_C_ManageContractID = con.ID 
where IsSigned='{1}' and BelongYear='{0}' 
union 
select isnull(DeptValue,0) as DataValue,
Dept as ChargeDeptID,DeptName as ChargeDeptName,
ad.BelongYear,ad.BelongQuarter,ad.BelongMonth from  S_C_ManageContract_Supplementary_DeptRelation dept
left join S_C_ManageContract_Supplementary ad on ad.ID=dept.S_C_ManageContract_SupplementaryID 
where ad.SignDate is not null and ad.SignDate !='' and ad.BelongYear='{0}' 
) tb
group by BelongYear,BelongQuarter,BelongMonth,ChargeDeptID,ChargeDeptName
";
            var contractDt = this.SqlHelper.ExecuteDataTable(String.Format(sql, belongYear, ContractIsSigned.Signed.ToString()));

            sql = @"select SUM(DataValue) DataValue,BelongYear,BelongQuarter,BelongMonth,ChargeDeptID,ChargeDeptName from (
select isnull(DeptValue,0) as DataValue,
Dept as ChargeDeptID,DeptName as ChargeDeptName,
BelongYear,BelongQuarter,BelongMonth from S_C_ManageContract_DeptRelation dept
left join S_C_ManageContract con on dept.S_C_ManageContractID = con.ID 
where IsSigned='{0}' or  IsSigned is null
union 
select isnull(DeptValue,0) as DataValue,
Dept as ChargeDeptID,DeptName as ChargeDeptName,
ad.BelongYear,ad.BelongQuarter,ad.BelongMonth from  S_C_ManageContract_Supplementary_DeptRelation dept
left join S_C_ManageContract_Supplementary ad on ad.ID=dept.S_C_ManageContract_SupplementaryID 
where ad.SignDate is null or ad.SignDate =''
) tb
group by BelongYear,BelongQuarter,BelongMonth,ChargeDeptID,ChargeDeptName
";
            var UncCntractDt = this.SqlHelper.ExecuteDataTable(String.Format(sql, ContractIsSigned.UnSigned.ToString()));

            var sumKpi = 0m; var sumValue = 0m;
            foreach (DataRow row in dt.Rows)
            {
                var obj = indicatorDt.Compute("Sum(ContractValue)", "OrgID='" + row["value"] + "'");
                var kpi = obj == null || obj == DBNull.Value ? 0 : Convert.ToDecimal(obj);
                row["ContractKPI"] = kpi;
                sumKpi += kpi;
                obj = contractDt.Compute("Sum(DataValue)", "ChargeDeptID='" + row["value"] + "'");
                var value = obj == null || obj == DBNull.Value ? 0 : Convert.ToDecimal(obj);
                row["ContractValue"] = value;
                sumValue += value;
                row["ContractComplateRate"] = kpi == 0 ? 100 : Math.Round(value / kpi * 100, 2);

                obj = UncCntractDt.Compute("Sum(DataValue)", "ChargeDeptID='" + row["value"] + "'");
                row["UnContractValue"] = obj == null || obj == DBNull.Value ? 0 : Convert.ToDecimal(obj);
                row["ContractKPIRemain"] = kpi - value;
                row["TimeRate"] = Math.Round(Convert.ToDecimal(DateTime.Now.DayOfYear) / 365 * 100);
            }
            var result = new Dictionary<string, object>();
            result.SetValue("data", dt);
            var sumData = new Dictionary<string, object>();
            sumData.SetValue("ContractComplateRate", sumKpi == 0 ? 100 : Math.Round(sumValue / sumKpi * 100, 2));
            result.SetValue("sumData", sumData);

            var yAxies = new List<yAxis>();
            var y1 = new yAxis { MiniValue = 0, TitleInfo = new Dictionary<string, object>(), Lable = new Dictionary<string, object>() };
            y1.TitleInfo.SetValue("text", "合同金额");
            y1.Lable.SetValue("format", "{value}元");
            var y2 = new yAxis { MiniValue = 0, TitleInfo = new Dictionary<string, object>(), Lable = new Dictionary<string, object>() };
            y2.TitleInfo.SetValue("text", "完成率");
            y2.Lable.SetValue("format", "{value}%"); y2.opposite = true;
            yAxies.Add(y1);
            yAxies.Add(y2);

            var serDefines = new List<Series>();
            var ReceiptKPISer = new Series { Name = "合同目标", Field = "ContractKPI", Type = "column", yAxis = 0, Tooltip = new Dictionary<string, object>() };
            ReceiptKPISer.Tooltip.SetValue("valueSuffix", "元");
            serDefines.Add(ReceiptKPISer);

            var ReceiptValueSer = new Series { Name = "已签订金额", Field = "ContractValue", Type = "column", yAxis = 0, Tooltip = new Dictionary<string, object>() };
            ReceiptValueSer.Tooltip.SetValue("valueSuffix", "元");
            serDefines.Add(ReceiptValueSer);

            var CanReceiptValueSer = new Series { Name = "待签约金额", Field = "UnContractValue", Type = "column", yAxis = 0, Tooltip = new Dictionary<string, object>() };
            CanReceiptValueSer.Tooltip.SetValue("valueSuffix", "元");
            serDefines.Add(CanReceiptValueSer);

            var RecepitComplateRateSer = new Series { Name = "完成率", Field = "ContractComplateRate", Type = "spline", yAxis = 1, Tooltip = new Dictionary<string, object>() };
            RecepitComplateRateSer.Tooltip.SetValue("valueSuffix", "%");
            serDefines.Add(RecepitComplateRateSer);

            //var TimeRateSer = new Series { Name = "时间", Field = "TimeRate", Type = "spline", yAxis = 1, Tooltip = new Dictionary<string, object>() };
            //TimeRateSer.Tooltip.SetValue("valueSuffix", "%");
            //serDefines.Add(TimeRateSer);

            var chart = HighChartHelper.CreateColumnXYChart(belongYear + "年各部门合同情况", "", dt, "text", yAxies, serDefines, null);
            result.SetValue("chart", chart);
            return Json(result);
        }
        #endregion

        public JsonResult GetDeptMonthReceiptList()
        {
            var dt = EnumBaseHelper.GetEnumTable("System.ManDept");
            for (int i = 1; i <= 12; i++)
            {
                var field = i + "_Month";
                dt.Columns.Add(field, typeof(decimal));
            }
            dt.Columns.Add("TotalValue", typeof(decimal));
            var belongYear = String.IsNullOrEmpty(this.GetQueryString("BelongYear")) ? DateTime.Now.Year : Convert.ToInt32(this.GetQueryString("BelongYear"));
            var sql = @"select isnull(Sum(Amount),0) as ReceiptValue,BelongMonth,BelongYear,ProductUnit from S_C_Receipt
where BelongYear='{0}'  group by ProductUnit,BelongMonth,BelongYear ";
            var receiptDt = this.SqlHelper.ExecuteDataTable(String.Format(sql, belongYear));
            foreach (DataRow row in dt.Rows)
            {
                var sumValue = 0m;
                for (int i = 1; i <= 12; i++)
                {
                    if (belongYear == DateTime.Now.Year && i > DateTime.Now.Month)
                    { row[i + "_Month"] = DBNull.Value; continue; }
                    var monthData = receiptDt.Select(" ProductUnit='" + row["value"] + "' and BelongMonth = '" + i + "'").FirstOrDefault();
                    if (monthData == null)
                    {
                        row[i + "_Month"] = 0;
                        continue;
                    }
                    row[i + "_Month"] = monthData["ReceiptValue"];
                    sumValue += Convert.ToDecimal(monthData["ReceiptValue"]);
                }
                row["TotalValue"] = sumValue;
            }
            return Json(dt);
        }

        public JsonResult GetDeptMonthContractList()
        {
            var dt = EnumBaseHelper.GetEnumTable("System.ManDept");
            for (int i = 1; i <= 12; i++)
            {
                var field = i + "_Month";
                dt.Columns.Add(field, typeof(decimal));
            }
            dt.Columns.Add("TotalValue", typeof(decimal));
            var belongYear = String.IsNullOrEmpty(this.GetQueryString("BelongYear")) ? DateTime.Now.Year : Convert.ToInt32(this.GetQueryString("BelongYear"));
            var sql = @"select isnull(Sum(ContractRMBAmount),0) as ContractValue,BelongMonth,BelongYear,ProductionDept from S_C_ManageContract
where BelongYear='{0}' and IsSigned='{1}' 
group by ProductionDept,BelongMonth,BelongYear ";
            var receiptDt = this.SqlHelper.ExecuteDataTable(String.Format(sql, belongYear, ContractIsSigned.Signed.ToString()));
            foreach (DataRow row in dt.Rows)
            {
                var sumValue = 0m;
                for (int i = 1; i <= 12; i++)
                {
                    if (belongYear == DateTime.Now.Year && i > DateTime.Now.Month)
                    { row[i + "_Month"] = DBNull.Value; continue; }
                    var monthData = receiptDt.Select(" ProductionDept='" + row["value"] + "' and BelongMonth = '" + i + "'").FirstOrDefault();
                    if (monthData == null)
                    {
                        row[i + "_Month"] = 0;
                        continue;
                    }
                    row[i + "_Month"] = monthData["ContractValue"];
                    sumValue += Convert.ToDecimal(monthData["ContractValue"]);
                }
                row["TotalValue"] = sumValue;
            }
            return Json(dt);
        }
    }
}
