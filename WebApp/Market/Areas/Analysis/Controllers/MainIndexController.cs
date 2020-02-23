using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Data;
using Formula.Helper;
using Formula;
using MvcAdapter;
using Config;
using Config.Logic;
using Market.Logic;
using Market.Logic.Domain;
using System.Collections;


namespace Market.Areas.Analysis.Controllers
{
    public class MainIndexController : MarketController
    {
        public ActionResult Index()
        {
            int belongYear = String.IsNullOrEmpty(this.GetQueryString("BelongYear")) ? DateTime.Now.Year : Convert.ToInt32(this.GetQueryString("BelongYear"));
            int belongMonth = DateTime.Now.Month; var lastMonth = belongMonth == 1 ? 12 : belongMonth - 1;
            string sql = @"select isnull(Sum(ContractRMBAmount),0) as DataValue,BelongYear,BelongQuarter,BelongMonth from S_C_ManageContract
where IsSigned='{0}' group by BelongYear,BelongQuarter,BelongMonth ";
            var contractDt = this.SqlHelper.ExecuteDataTable(String.Format(sql, ContractIsSigned.Signed.ToString()));

            sql = @"select isnull(Sum(Amount),0) as DataValue,BelongYear,BelongQuarter,BelongMonth from S_C_Receipt group by BelongYear,BelongQuarter,BelongMonth ";
            var receiptDt = this.SqlHelper.ExecuteDataTable(sql);

            sql = @"select * from S_KPI_IndicatorCompany where IndicatorType = 'YearIndicator' and BelongYear= '{0}'";
            var indicatorDt = this.SqlHelper.ExecuteDataTable(String.Format(sql, belongYear));
            var RecepitBlock = this._getViewBlock(indicatorDt, receiptDt, belongYear, "ReceiptValue", "收款");
            RecepitBlock.MainUrl = "/Market/Analysis/DeptAnalysis/DeptReceiptInfo?BelongYear=" + belongYear + "&BelongMonth=" + belongMonth;
            RecepitBlock.SubViewBlockList[0].LinkUrl = "/MvcConfig/UI/List/PageView?TmplCode=ReceiptInfo&BelongYear=" + belongYear + "&BelongQuarter=" + MarketHelper.GetQuarter(DateTime.Now);
            RecepitBlock.SubViewBlockList[1].LinkUrl = "/MvcConfig/UI/List/PageView?TmplCode=ReceiptInfo&BelongYear=" + belongYear + "&BelongMonth=" + belongMonth;
            RecepitBlock.SubViewBlockList[2].LinkUrl = "/MvcConfig/UI/List/PageView?TmplCode=ReceiptInfo&BelongYear=" + belongYear + "&BelongMonth=" + lastMonth;
            ViewBag.RecepitBlock = RecepitBlock;

            var ContractBlock = this._getViewBlock(indicatorDt, contractDt, belongYear, "ContractValue", "签订金额");
            ContractBlock.MainUrl = "/Market/Analysis/DeptAnalysis/DeptContractInfo?BelongYear=" + belongYear + "&BelongMonth=" + belongMonth;

            ContractBlock.SubViewBlockList[0].LinkUrl = "/MvcConfig/UI/List/PageView?TmplCode=ContractInfo&BelongYear=" + belongYear + "&BelongQuarter=" + MarketHelper.GetQuarter(DateTime.Now);
            ContractBlock.SubViewBlockList[1].LinkUrl = "/MvcConfig/UI/List/PageView?TmplCode=ContractInfo&BelongYear=" + belongYear + "&BelongMonth=" + belongMonth;
            ContractBlock.SubViewBlockList[2].LinkUrl = "/MvcConfig/UI/List/PageView?TmplCode=ContractInfo&BelongYear=" + belongYear + "&BelongMonth=" + lastMonth;

            ViewBag.ContractBlock = ContractBlock;


            sql = @"select  Count(0) as DataValue,Year(CreateDate) as BelongYear,Month(CreateDate) as BelongMonth,[State],SignContractCount 
from (select S_I_Project.ID,S_I_Project.State,S_I_Project.CreateDate,S_I_Project.Name,Code,
isnull(SignContractCount,0) as SignContractCount,
isnull(ProjectValue,0) as ProjectValue from S_I_Project
left join (select sum(case when IsSigned='Signed' then 1 else 0 end) SignContractCount,Sum(ProjectValue) as ProjectValue,ProjectID from S_C_ManageContract_ProjectRelation r
left join S_C_ManageContract c on r.S_C_ManageContractID=c.ID
group by ProjectID) ProjectSignInfo on S_I_Project.ID=ProjectSignInfo.ProjectID) tableInfo
group by Year(CreateDate),Month(CreateDate),[State],SignContractCount ";
            var projectDt = this.SqlHelper.ExecuteDataTable(sql);
            var ProjectBlock = this._getProjectViewBlock(projectDt, belongYear);
            ProjectBlock.MainUrl = "/MvcConfig/UI/List/PageView?TmplCode=ProjectInfo&BelongYear=" + belongYear;
            ProjectBlock.SubViewBlockList[0].LinkUrl = "/MvcConfig/UI/List/PageView?TmplCode=ProjectInfo&State=Plan,Execute,Create";
            ProjectBlock.SubViewBlockList[1].LinkUrl = "/MvcConfig/UI/List/PageView?TmplCode=ProjectInfo&SignState=UnSigned";
            ProjectBlock.SubViewBlockList[2].LinkUrl = "/MvcConfig/UI/List/PageView?TmplCode=ProjectInfo&State=Pause,Terminate";
            ViewBag.ProjectBlock = ProjectBlock;
            ViewBag.SubViewList = this._getSubViewBlockList().OrderBy(d => d.SortIndex).ToList();
            return View();
        }

        public ActionResult MainPage()
        {
            int belongYear = String.IsNullOrEmpty(this.GetQueryString("BelongYear")) ? DateTime.Now.Year : Convert.ToInt32(this.GetQueryString("BelongYear"));
            int belongMonth = DateTime.Now.Month;
            var lastMonth = belongMonth == 1 ? 12 : belongMonth - 1;
            var lastYear = belongYear - 1;
            string sql = @"select isnull(sum(DataValue),0) as DataValue,BelongYear,BelongQuarter,BelongMonth from
(select isnull(ThisContractRMBAmount,0) as DataValue,BelongYear,BelongQuarter,BelongMonth
from S_C_ManageContract where IsSigned='{0}'
union all
select isnull(SupplementaryRMBAmount,0) as DataValue,ad.BelongYear,ad.BelongQuarter,ad.BelongMonth
from S_C_ManageContract_Supplementary ad
inner join S_C_ManageContract con on ad.ContractInfoID=con.ID where IsSigned='{0}'
)tb group by BelongYear,BelongQuarter,BelongMonth";
            var contractDt = this.SqlHelper.ExecuteDataTable(String.Format(sql, ContractIsSigned.Signed.ToString()));

            sql = @"select isnull(Sum(Amount),0) as DataValue,BelongYear,BelongQuarter,BelongMonth from S_C_Receipt group by BelongYear,BelongQuarter,BelongMonth ";
            var receiptDt = this.SqlHelper.ExecuteDataTable(sql);

            sql = @"select * from S_KPI_IndicatorCompany where IndicatorType = 'YearIndicator' and BelongYear= '{0}'";
            var indicatorDt = this.SqlHelper.ExecuteDataTable(String.Format(sql, belongYear));
            var RecepitBlock = this._getViewBlock(indicatorDt, receiptDt, belongYear, "ReceiptValue", "收款");
            RecepitBlock.MainUrl = "/Market/Analysis/DeptAnalysis/DeptReceiptInfo?BelongYear=" + belongYear + "&BelongMonth=" + belongMonth;
            RecepitBlock.SubViewBlockList[0].LinkUrl = "/MvcConfig/UI/List/PageView?TmplCode=ReceiptInfo&BelongYear=" + belongYear + "&BelongQuarter=" + MarketHelper.GetQuarter(DateTime.Now);
            RecepitBlock.SubViewBlockList[1].LinkUrl = "/MvcConfig/UI/List/PageView?TmplCode=ReceiptInfo&BelongYear=" + belongYear + "&BelongMonth=" + belongMonth;
            RecepitBlock.SubViewBlockList[2].LinkUrl = "/MvcConfig/UI/List/PageView?TmplCode=ReceiptInfo&BelongYear=" + (lastMonth == 12 ? lastYear : belongYear) + "&BelongMonth=" + lastMonth;
            ViewBag.RecepitBlock = RecepitBlock;

            var ContractBlock = this._getViewBlock(indicatorDt, contractDt, belongYear, "ContractValue", "签订");
            ContractBlock.MainUrl = "/Market/Analysis/DeptAnalysis/DeptContractInfo?BelongYear=" + belongYear + "&BelongMonth=" + belongMonth;

            ContractBlock.SubViewBlockList[0].LinkUrl = "/MvcConfig/UI/List/PageView?TmplCode=ContractInfo&BelongYear=" + belongYear + "&BelongQuarter=" + MarketHelper.GetQuarter(DateTime.Now) + "&IsSigned=Signed";
            ContractBlock.SubViewBlockList[1].LinkUrl = "/MvcConfig/UI/List/PageView?TmplCode=ContractInfo&BelongYear=" + belongYear + "&BelongMonth=" + belongMonth + "&IsSigned=Signed";
            ContractBlock.SubViewBlockList[2].LinkUrl = "/MvcConfig/UI/List/PageView?TmplCode=ContractInfo&BelongYear=" + (lastMonth == 12 ? lastYear : belongYear) + "&BelongMonth=" + lastMonth + "&IsSigned=Signed";

            ViewBag.ContractBlock = ContractBlock;


            sql = @"select  Count(0) as DataValue,Year(CreateDate) as BelongYear,Month(CreateDate) as BelongMonth,[State],SignContractCount 
from (select S_I_Project.ID,S_I_Project.State,S_I_Project.CreateDate,S_I_Project.Name,Code,
isnull(SignContractCount,0) as SignContractCount,
isnull(ProjectValue,0) as ProjectValue from S_I_Project
left join (select sum(case when IsSigned='Signed' then 1 else 0 end) SignContractCount,Sum(ProjectValue) as ProjectValue,ProjectID from S_C_ManageContract_ProjectRelation r
left join S_C_ManageContract c on r.S_C_ManageContractID=c.ID
group by ProjectID) ProjectSignInfo on S_I_Project.ID=ProjectSignInfo.ProjectID) tableInfo
group by Year(CreateDate),Month(CreateDate),[State],SignContractCount ";
            var projectDt = this.SqlHelper.ExecuteDataTable(sql);
            var ProjectBlock = this._getProjectViewBlock(projectDt, belongYear);
            ProjectBlock.MainUrl = "/MvcConfig/UI/List/PageView?TmplCode=ProjectInfo&BelongYear=" + belongYear;
            ProjectBlock.SubViewBlockList[0].LinkUrl = "/MvcConfig/UI/List/PageView?TmplCode=ProjectInfo&State=Plan,Execute,Create";
            ProjectBlock.SubViewBlockList[1].LinkUrl = "/MvcConfig/UI/List/PageView?TmplCode=ProjectInfo&SignState=UnSigned";
            ProjectBlock.SubViewBlockList[2].LinkUrl = "/MvcConfig/UI/List/PageView?TmplCode=ProjectInfo&State=Pause,Terminate";
            ViewBag.ProjectBlock = ProjectBlock;
            ViewBag.SubViewList = this._getSubViewBlockList().OrderBy(d => d.SortIndex).ToList();
            return View();
        }

        ViewBlock _getViewBlock(DataTable indicatorDt, DataTable dataSource, int belongYear, string indicatorField, string title = "", bool formatCurreny = true)
        {
            var result = new ViewBlock();
            var obj = dataSource.Compute("Sum(DataValue)", " BelongYear = '" + belongYear + "' and BelongMonth<='" + DateTime.Now.Month + "'");
            result.Main = obj != null && obj != DBNull.Value ? Math.Round(Convert.ToDecimal(obj), 2) : 0;
            if (formatCurreny)
            {
                result.Main = Math.Round(result.Main / 10000, 2);
            }
            var lastYear = belongYear - 1;
            obj = dataSource.Compute("Sum(DataValue)", " BelongYear = '" + lastYear + "' and BelongMonth<='" + DateTime.Now.Month + "'");
            var lastYearValue = obj != null && obj != DBNull.Value ? Convert.ToDecimal(obj) : 0;
            if (formatCurreny)
            {
                lastYearValue = Math.Round(lastYearValue / 10000, 2);
            }
            result.SubAreaTip = "去年同期：{0}；同比{1}：{2}";
            var scale = lastYearValue == 0 ? 100 : Math.Round((result.Main - lastYearValue) / lastYearValue * 100, 0);
            result.Sub = Math.Round(scale);
            if (result.Sub > 0)
            {
                result.SubAreaTip = String.Format(result.SubAreaTip, lastYearValue, "上升", Math.Abs(scale) + "%");
            }
            else
            {
                result.SubAreaTip = String.Format(result.SubAreaTip, lastYearValue, "下降", Math.Abs(scale) + "%");
            }
            if (indicatorDt.Rows.Count > 0)
            {
                var objSubRight = indicatorDt.Compute("sum(" + indicatorField + ")", "");
                if (objSubRight != null && objSubRight != DBNull.Value)
                    result.SubRight = Math.Round(Convert.ToDecimal(objSubRight), 0);
                //result.SubRight = indicatorDt.Rows[0][indicatorField] == null || indicatorDt.Rows[0][indicatorField] == DBNull.Value ? 0 :
                //Math.Round(Convert.ToDecimal(indicatorDt.Rows[0][indicatorField]), 0);
            }
            else
            {
                result.SubRight = 0;
            }
            if (formatCurreny)
            {
                result.SubRight = result.SubRight / 10000;
            }

            result.progressMain = result.SubRight == 0 ? 0 : Math.Round(result.Main / result.SubRight * 100);
            result.progressSub = Math.Round(Convert.ToDecimal(DateTime.Now.DayOfYear) / 365 * 100);
            obj = dataSource.Compute("Sum(DataValue)", " BelongYear = '" + belongYear + "' and BelongQuarter='" + MarketHelper.GetQuarter(DateTime.Now) + "'");
            var subValue = obj == null || obj == DBNull.Value ? 0 : Math.Round(Convert.ToDecimal(obj), 2);
            if (formatCurreny) subValue = Math.Round(subValue / 10000, 2);
            var quater = this._createSubViewBlock(subValue.ToString(), "本季度" + title, "万元", 100);
            result.SubViewBlockList.Add(this._createSubViewBlock(subValue.ToString(), "本季度" + title, "万元", 100));

            obj = dataSource.Compute("Sum(DataValue)", " BelongYear = '" + belongYear + "' and BelongMonth='" + DateTime.Now.Month + "'");
            subValue = obj == null || obj == DBNull.Value ? 0 : Math.Round(Convert.ToDecimal(obj), 2);
            if (formatCurreny) subValue = Math.Round(subValue / 10000, 2);
            result.SubViewBlockList.Add(this._createSubViewBlock(subValue.ToString(), "本月" + title, "万元", 100));

            var lastMonth = DateTime.Now.Month == 1 ? 12 : DateTime.Now.Month - 1;
            if (lastMonth == 12)
                obj = dataSource.Compute("Sum(DataValue)", " BelongYear = '" + lastYear + "' and BelongMonth='" + lastMonth + "'");
            else
                obj = dataSource.Compute("Sum(DataValue)", " BelongYear = '" + belongYear + "' and BelongMonth='" + lastMonth + "'");
            subValue = obj == null || obj == DBNull.Value ? 0 : Math.Round(Convert.ToDecimal(obj), 2);
            if (formatCurreny) subValue = Math.Round(subValue / 10000, 2);
            result.SubViewBlockList.Add(this._createSubViewBlock(subValue.ToString(), "上月" + title, "万元", 100));
            return result;
        }

        ViewBlock _getProjectViewBlock(DataTable dataSource, int belongYear)
        {
            var result = new ViewBlock();
            var obj = dataSource.Compute("Sum(DataValue)", " BelongYear = '" + belongYear + "'");
            result.Main = obj != null && obj != DBNull.Value ? Math.Round(Convert.ToDecimal(obj), 2) : 0;

            var lastYear = belongYear - 1;
            obj = dataSource.Compute("Sum(DataValue)", " BelongYear = '" + lastYear + "' and BelongMonth<='" + DateTime.Now.Month + "'");
            var lastYearValue = obj != null && obj != DBNull.Value ? Convert.ToDecimal(obj) : 0;
            result.SubAreaTip = "去年同期：{0}；同比{1}：{2}";
            var scale = lastYearValue == 0 ? 100 : Math.Round((result.Main - lastYearValue) / lastYearValue * 100, 0);
            result.Sub = Math.Round(scale);
            if (result.Sub > 0)
            {
                result.SubAreaTip = String.Format(result.SubAreaTip, lastYearValue, "上升", Math.Abs(scale) + "%");
            }
            else
            {
                result.SubAreaTip = String.Format(result.SubAreaTip, lastYearValue, "下降", Math.Abs(scale) + "%");
            }
            obj = dataSource.Compute("Sum(DataValue)", "");
            result.SubRight = obj != null && obj != DBNull.Value ? Math.Round(Convert.ToDecimal(obj), 2) : 0;

            obj = dataSource.Compute("Sum(DataValue)", " SignContractCount>0 ");
            var value = obj != null && obj != DBNull.Value ? Math.Round(Convert.ToDecimal(obj), 2) : 0;
            result.progressMain = result.SubRight == 0 ? 0 : Math.Round(value / result.SubRight * 100, 0);

            obj = dataSource.Compute("Sum(DataValue)", " State in ('" + ProjectState.Create.ToString() + "','" + ProjectState.Plan.ToString() + "','" + ProjectState.Execute.ToString() + "') ");
            value = obj != null && obj != DBNull.Value ? Math.Round(Convert.ToDecimal(obj)) : 0;
            result.SubViewBlockList.Add(this._createSubViewBlock(value.ToString(), "在建项目数", "个", 100));

            obj = dataSource.Compute("Sum(DataValue)", " SignContractCount=0 ");
            value = obj != null && obj != DBNull.Value ? Math.Round(Convert.ToDecimal(obj)) : 0;
            result.SubViewBlockList.Add(this._createSubViewBlock(value.ToString(), "未签约项目数", "个", 200));

            obj = dataSource.Compute("Sum(DataValue)", " State in ('" + ProjectState.Pause.ToString() + "','" + ProjectState.Terminate.ToString() + "') ");
            value = obj != null && obj != DBNull.Value ? Math.Round(Convert.ToDecimal(obj)) : 0;
            result.SubViewBlockList.Add(this._createSubViewBlock(value.ToString(), "暂停项目数", "个", 300));
            return result;
        }

        List<SubViewBlock> _getSubViewBlockList()
        {
            var result = new List<SubViewBlock>();
            string sql = @"select isnull(Sum( SumInvoiceValue-SumReceiptValue),0) as  DataValue from S_C_ManageContract where IsSigned='" + ContractIsSigned.Signed.ToString() + "'";
            var obj = this.SqlHelper.ExecuteScalar(sql);
            var value = obj == null || obj == DBNull.Value ? 0 : Math.Round(Convert.ToDecimal(obj) / 10000, 2);
            result.Add(_createSubViewBlock(value.ToString(), "财务应收款", "万元", 100, "/MvcConfig/UI/List/PageView?TmplCode=ContractInfo&IsSigned=Signed&HasCanReceiptValue=True"));

            sql = @"select isnull(Sum(ReceiptValue-FactReceiptValue),0) as DataValue from S_C_ManageContract_ReceiptObj where MileStoneState='True'";
            obj = this.SqlHelper.ExecuteScalar(sql);
            value = obj == null || obj == DBNull.Value ? 0 : Math.Round(Convert.ToDecimal(obj) / 10000, 2);
            result.Add(_createSubViewBlock(value.ToString(), "经营应收款", "万元", 200, "/MvcConfig/UI/List/PageView?TmplCode=ReceiptObjInfo&CanReceipt=True&MileStoneState=True&IsSigned=Signed"));

            sql = @"select isnull(Sum(ContractRMBAmount-SumReceiptValue),0) as  DataValue from S_C_ManageContract where IsSigned='Signed'";
            obj = this.SqlHelper.ExecuteScalar(sql);
            value = obj == null || obj == DBNull.Value ? 0 : Math.Round(Convert.ToDecimal(obj) / 10000, 2);
            result.Add(_createSubViewBlock(value.ToString(), "当前剩余合同额", "万元", 300, "/MvcConfig/UI/List/PageView?TmplCode=ContractInfo&IsSigned=Signed&HasRemainContract=True"));

            sql = @"select isnull(Sum(ContractRMBAmount),0) as DataValue from S_C_ManageContract where IsSigned!='Signed'";
            obj = this.SqlHelper.ExecuteScalar(sql);
            value = obj == null || obj == DBNull.Value ? 0 : Math.Round(Convert.ToDecimal(obj) / 10000, 2);
            result.Add(_createSubViewBlock(value.ToString(), "待签合同额", "万元", 400, "/MvcConfig/UI/List/PageView?TmplCode=ContractInfo&IsSigned=UnSigned"));
            return result;
        }

        SubViewBlock _createSubViewBlock(string mainValue, string title, string Unit, int sortIndex, string linkUrl = "")
        {
            var result = new SubViewBlock();
            result.MainValue = mainValue;
            result.SortIndex = sortIndex;
            result.Title = title;
            result.Unit = Unit;
            result.LinkUrl = linkUrl;
            return result;
        }

        List<ReportMenu> GetMenuList()
        {
            return null;
        }

    }

    public class ViewBlock
    {
        public decimal Main { get; set; }

        public string MainUrl { get; set; }

        public decimal Sub { get; set; }

        public string SubAreaTip { get; set; }

        public decimal SubRight { get; set; }

        public decimal progressMain { get; set; }

        public decimal progressSub { get; set; }

        List<SubViewBlock> _SubViewBlockList;
        public List<SubViewBlock> SubViewBlockList
        {
            get
            {
                if (_SubViewBlockList == null)
                    _SubViewBlockList = new List<SubViewBlock>();
                return _SubViewBlockList;
            }
        }
    }

    public class SubViewBlock
    {

        public string MainValue { get; set; }

        public string Title { get; set; }

        public string Unit { get; set; }

        public int SortIndex { get; set; }

        public string LinkUrl { get; set; }
    }

    public class ReportMenu
    {
        public string Title { get; set; }

        public string LinkUrl { get; set; }

        public string Img { get; set; }
    }

}
