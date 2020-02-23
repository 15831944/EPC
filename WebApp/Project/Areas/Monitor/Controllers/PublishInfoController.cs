using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Data;
using System.Reflection;
using Formula;
using Formula.Helper;
using MvcAdapter;
using Config;
using Config.Logic;
using Project.Logic.Domain;
using Project.Logic;
using System.Linq.Expressions;
using Formula.DynConditionObject;
using System.Text;

namespace Project.Areas.Monitor.Controllers
{
    public class PublishInfoController : ProjectController
    {

        public ActionResult PublishInfoAnylise()
        {
            var picFileType = EnumBaseHelper.GetEnumDef("Project.PublicationType").EnumItem.ToList();
            var picSpecifications = EnumBaseHelper.GetEnumDef("Project.BorderSize").EnumItem.ToList();
            ViewBag.PicFileType = picFileType;
            ViewBag.PicSpecifications = picSpecifications;
            return View();
        }
        public ActionResult ProjectMappingStatistics()
        {
            var tab = new Formula.Tab();
            var category = CategoryFactory.GetYearCategory("Year");
            category.SetDefaultItem(DateTime.Now.Year.ToString());
            tab.Categories.Add(category);
            tab.IsDisplay = true;
            ViewBag.Tab = tab;
            return View();
        }
        public ActionResult ProjectMappingStaChild()
        {
            var picSpecifications = EnumBaseHelper.GetEnumDef("Project.BorderSize").EnumItem.ToList();
            ViewBag.PicSpecifications = picSpecifications;
            return View();
        }
        public JsonResult GetProjectMappingList(QueryBuilder qb)
        {
            SQLHelper sqlHelp = SQLHelper.CreateSqlHelper("Project");
            string sqlStr = @"select MajorCode,";
            for (int i = 1; i < 13; i++)
            {
                sqlStr +=string.Format( @"SUM(case when BelongMonth={0} then ApplyNum end) as m{0}_ApplyNum ,
                SUM(case when BelongMonth={0} then ApplyTotalCost end) as m{0}_ApplyTotalCost ,
               SUM(case when BelongMonth={0} then RealNum end) as m{0}_RealNum ,
               SUM(case when BelongMonth={0} then RealTotalCost end) as m{0}_RealTotalCost ,",i);
            }
            sqlStr = sqlStr.TrimEnd(',') + @"from (select BelongYear,BelongMonth,ProjectInfoID,MajorCode,
                SUM(d.Num) ApplyNum,SUM(d.SumCost) ApplyTotalCost,
                SUM(d.RealNum) RealNum,SUM(d.RealSumCost) RealTotalCost
                 from S_EP_PublishInfo_PriceDetail d with(nolock)
                 left join S_EP_PublishInfo m with(nolock) on d.S_EP_PublishInfoID = m.ID
                 where 1=1 {0}
                 group by BelongYear,BelongMonth,ProjectInfoID,MajorCode) dt group by MajorCode order by MajorCode desc";

            var subWhereStr = "";
            foreach (var condition in qb.Items.Where(a => a.Field == "Year"))
            {
                // BelongYear='2019'
                subWhereStr += "and BelongYear in (" + condition.Value + ")";
            }
            qb.Items.RemoveWhere(a => a.Field == "Year");
            sqlStr = string.Format(sqlStr, subWhereStr);
            DataTable data = sqlHelp.ExecuteDataTable(sqlStr);
            return Json(data);
        }
        public JsonResult GetProjectMappingStaChildList()
        {
            var sqlHelp = SQLHelper.CreateSqlHelper(ConnEnum.Project);
            string sql = @"select PublicationType,";
            var picSpecifications = EnumBaseHelper.GetEnumDef("Project.BorderSize").EnumItem.ToList();
            foreach (var item in picSpecifications)
            {
                sql +=string.Format( @"SUM(case when Specification='{0}' then ApplyNum end) as [{0}_ApplyNum] ,
                    SUM(case when Specification='{0}' then ApplyTotalCost end) as [{0}_ApplyTotalCost] ,
                    SUM(case when Specification='{0}' then RealNum end) as [{0}_RealNum] ,
                    SUM(case when Specification='{0}' then RealTotalCost end) as [{0}_RealTotalCost],", item.Code);
            }
            sql = sql.TrimEnd(',') + @" from (select PublicationType,Specification,
                 SUM(Num) ApplyNum,SUM(SumCost) ApplyTotalCost,
                 SUM(RealNum) RealNum,SUM(RealSumCost) RealTotalCost
     from (select PubliDetail.ID,PublicationType,Specification,PubliDetail.Num,PubliDetail.SumCost,
  RealNum,PubliDetail.RealSumCost,BelongYear,BelongMonth,MajorCode from S_EP_PublishInfo_PriceDetail PubliDetail
  left join S_EP_PublishInfo d on PubliDetail.S_EP_PublishInfoID=d.ID where 1=1 {0}
   ) datatable group by PublicationType,Specification)  dtChild group by PublicationType";
            string year = GetQueryString("Year");
            string month = GetQueryString("Month");
            string majorCode = GetQueryString("MajorCode");
            string strTemp = "";
            if (month == "")
                strTemp = "";
            else  if (year == "")
                strTemp = "and BelongMonth in (" + month + ") and MajorCode ='" + majorCode+"'";
            else
                strTemp = "and BelongYear in (" + year + ") and BelongMonth in (" + month + ") and MajorCode ='" + majorCode + "'";

            sql = string.Format(sql, strTemp);
            DataTable data = sqlHelp.ExecuteDataTable(sql);
            return Json(data);
        }

        public JsonResult GetList()
        {
            var projectInfo = this.GetEntityByID<S_I_ProjectInfo>(this.Request["ProjectInfoID"]);
            if (projectInfo == null) throw new Formula.Exceptions.BusinessException("未能找到指定的项目信息");
            string sql = @"select distinct Name,WBSValue,ProjectInfoID,SortIndex from S_W_WBS where ProjectInfoID='{0}' and WBSType='Major' order by SortIndex";
            var resultDt = this.SqlHelper.ExecuteDataTable(String.Format(sql, projectInfo.ID));
            var picFileType = EnumBaseHelper.GetEnumDef("Project.PublicationType").EnumItem.ToList();
            var picSpecifications = EnumBaseHelper.GetEnumDef("Project.BorderSize").EnumItem.ToList();
            var colList = new List<string>();
            foreach (var item in picFileType)
            {
                resultDt.Columns.Add(item.Code, typeof(decimal));
            }
            foreach (var item in picSpecifications)
            {
                resultDt.Columns.Add(item.Code, typeof(decimal));
                colList.Add("sum(" + item.Code + ") as " + item.Code);
            }
            resultDt.Columns.Add("ToA1", typeof(decimal));

            var marketSqlHepler = SQLHelper.CreateSqlHelper(ConnEnum.Market);
            sql = @"select SUM(CostValue) CostValue,Extend5 MajorValue,Extend1 PublishType from S_FC_CostInfo
where ProjectID = '" + projectInfo.MarketProjectInfoID + "' and SubjectCode = 'PublishFee' group by Extend5,Extend1";
            var dt = marketSqlHepler.ExecuteDataTable(sql);
            sql = @"select " + string.Join(",", colList) + @",
Sum(ToA1) as ToA1,MajorCode as MajorValue from S_EP_PublishInfo 
where ProjectInfoID='" + projectInfo.ID + "' and PublishTime is not null group by MajorCode";
            var specDt = this.SqlHelper.ExecuteDataTable(sql);
            foreach (DataRow row in resultDt.Rows)
            {
                foreach (var item in picFileType)
                {
                    var value = 0m;
                    var rows = dt.Select("MajorValue='" + row["WBSValue"] + "' and PublishType='" + item.Code + "'");
                    if (rows.Length > 0)
                    {
                        value = rows[0]["CostValue"] == null || rows[0]["CostValue"] == DBNull.Value ? 0m : Convert.ToDecimal(rows[0]["CostValue"]);
                    }
                    row[item.Code] = value;
                }
                var specRows = specDt.Select("MajorValue='" + row["WBSValue"] + "'");
                foreach (var item in picSpecifications)
                {
                    var value = 0m;
                    if (specRows.Length > 0)
                    {
                        value = specRows[0][item.Code] == null || specRows[0][item.Code] == DBNull.Value ? 0m : Convert.ToDecimal(specRows[0][item.Code]);
                    }
                    row[item.Code] = value;
                }
                var toA1 = 0m;
                if (specRows.Length > 0)
                {
                    toA1 = specRows[0]["ToA1"] == null || specRows[0]["ToA1"] == DBNull.Value ? 0m : Convert.ToDecimal(specRows[0]["ToA1"]);
                }
                row["ToA1"] = toA1;
            }

            var result = new Dictionary<string, object>();
            result.SetValue("data", resultDt);
            result.SetValue("pieChartData", GetPieChart(projectInfo.ID));
            result.SetValue("chartData", GetColumnChart(projectInfo));
            return Json(result);
        }

        public Dictionary<string, object> GetPieChart(string projectInfoID)
        {
            string sql = @"select  Sum(ToA1) as ToA1,MajorName from S_EP_PublishInfo 
where ProjectInfoID='" + projectInfoID + "' and PublishTime is not null group by MajorName";
            var dt = this.SqlHelper.ExecuteDataTable(sql);
            var chart = HighChartHelper.CreatePieChart("各专业出图情况", "折A1张数", dt, "MajorName", "ToA1");
            var result = chart.Render();
            var credits = new Dictionary<string, object>();
            credits.SetValue("enabled", false);
            result.SetValue("credits", credits);
            return result;
        }

        public Dictionary<string, object> GetColumnChart(S_I_ProjectInfo projectInfo)
        {
            var majorList = projectInfo.GetMajors();
            var picFileType = EnumBaseHelper.GetEnumDef("Project.PublicationType").EnumItem.ToList();
            var dataSource = new DataTable();
            dataSource.Columns.Add("MajorName");
            var seriesNames = "出图费用(元)";
            var seriesFields = "CostValue";
            //foreach (var item in picFileType)
            //{
            //    dataSource.Columns.Add(item.Code, typeof(decimal));
            //    seriesNames += item.Name + "折A1,";
            //    seriesFields += item.Code + ",";
            //}
            dataSource.Columns.Add("CostValue", typeof(decimal));

            string sql = @"select SUM(CostValue) CostValue,Extend5 MajorValue from S_FC_CostInfo
where ProjectID = '" + projectInfo.MarketProjectInfoID + "' and SubjectCode = 'PublishFee' group by Extend5";
            var marketSqlHepler = SQLHelper.CreateSqlHelper(ConnEnum.Market);
            var dt = marketSqlHepler.ExecuteDataTable(sql);
            foreach (var major in majorList)
            {
                var row = dataSource.NewRow();
                row["MajorName"] = major.GetValue("Name");
                //foreach (var item in picFileType)
                //{
                //    var value = 0m;
                //    var list = dt.Select("MajorValue='" + major.GetValue("Value") + "' and PublishType='" + item.Code + "'");
                //    if (list.Length > 0)
                //    {
                //        value = list[0]["ToA1"] == null || list[0]["ToA1"] == DBNull.Value ? 0m : Convert.ToDecimal(list[0]["ToA1"]);
                //    }
                //    row[item.Code] = value;
                //}
                var value = 0m;
                var list = dt.Select("MajorValue='" + major.GetValue("Value") + "'");
                if (list.Length > 0)
                {
                    value = list[0]["CostValue"] == null || list[0]["CostValue"] == DBNull.Value ? 0m : Convert.ToDecimal(list[0]["CostValue"]);
                }
                row["CostValue"] = value;
                dataSource.Rows.Add(row);
            }
            var chart = HighChartHelper.CreateColumnChart("各专业出图费用统计", dataSource, "MajorName", seriesNames.TrimEnd(',').Split(','), seriesFields.TrimEnd(',').Split(','));
            var result = chart.Render();
            var credits = new Dictionary<string, object>();
            credits.SetValue("enabled", false);
            result.SetValue("credits", credits);
            return result;
        }
    }
}
