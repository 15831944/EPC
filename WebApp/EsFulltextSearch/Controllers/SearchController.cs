using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Config.Logic;
using MvcAdapter;
using System.Configuration;
using EsHelper;
using Config;
using System.Data;
using Formula;
using Formula.Helper;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace EsFulltextSearch.Controllers
{
    public class SearchController : EsFulltextSearchController
    {
        string EsNodeUrl = ConfigurationManager.AppSettings["EsNodeUrl"];

        //string EsNodeUrl = "http://10.10.0.88:8085";
        public ActionResult QueryList()
        {
            ViewBag.SpaceID = GetQueryString("SpaceID");
            ViewBag.QueryValue = GetQueryString("QueryValue");
            if (string.IsNullOrEmpty(EsNodeUrl))
                throw new Formula.Exceptions.BusinessException("请先在Web.config中配置EsNodeUrl");
            //档案空间列表
            SQLHelper configSqlHelper = SQLHelper.CreateSqlHelper(ConnEnum.InfrasBaseConfig);
            DataTable spaceTable = configSqlHelper.ExecuteDataTable("select ID,Name from S_DOC_Space");
            ViewBag.SpaceTable = spaceTable;
            //购物车数量
            ViewBag.CarCount = getCarCount().ToString();
            return View();
        }

        public JsonResult GetList(QueryBuilder qb)
        {
            var pi = qb.PageIndex;
            var ps = qb.PageSize;
            var qbItem = qb.Items.FirstOrDefault(a => a.Field == "QueryText");
            var queryValue = string.Empty;
            var spaceID = string.Empty;
            if (qbItem != null)
                queryValue = qbItem.Value.ToString();
            qbItem = qb.Items.FirstOrDefault(a => a.Field == "SpaceID");
            if (qbItem != null)
                spaceID = qbItem.Value.ToString();
            var dataList = new List<Dictionary<string, object>>();

            var esHelper = EsBaseHelper.CreateEsHelper(EsNodeUrl); //查询
            esHelper.DefaultIndex = EsConst.defaultEsFileIndex;
            var queryResult = esHelper.QueryDocument(queryValue, spaceID, pi, ps);//查询结果
            var hitsList = queryResult.Hits;
            foreach (var item in hitsList)
            {
                var dic = new Dictionary<string, object>();
                var hitSource = item.Source;
                if (hitSource != null)
                    dic = FormulaHelper.ModelToDic(hitSource);
                //获得高亮数据
                var content = string.Empty;//文档内容
                string title = string.Empty;//标题
                string propertyStr = string.Empty;//属性
                if (item.Highlight.Count > 0)
                {
                    foreach (var highlight in item.Highlight)
                    {
                        if (highlight.Key == "title")
                            title += string.Join("", highlight.Value.ToArray());
                        else if (highlight.Key == "content")
                            content += string.Join(" ", highlight.Value.ToArray()).Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                        else if (highlight.Key == "propertyJson")
                            propertyStr += string.Join("", highlight.Value.ToArray());
                    }
                }

                if (string.IsNullOrEmpty(title) && hitSource != null)
                    title = hitSource.Title;
                if (string.IsNullOrEmpty(propertyStr) && hitSource != null)
                    propertyStr = hitSource.PropertyJson;
                dic.SetValue("title", title);
                dic.SetValue("propertyStr", propertyStr);
                dic.SetValue("content", content);
                dataList.Add(dic);
            }
            var gridData = new GridData(dataList);
            var total = 0;
            if(queryResult.Total!=null)
                total = Convert.ToInt32( queryResult.Total.Value);
            gridData.total = total;
            return Json(gridData);
        }

        public ActionResult GetCarCount()
        {
            return Json(new { carCount = getCarCount() });
        }

        private int getCarCount()
        {
            SQLHelper constSqlHelper = SQLHelper.CreateSqlHelper(ConnEnum.DocConst);
            var carCount = constSqlHelper.ExecuteScalar(string.Format("select count(1) from S_CarInfo where CreateUserId='{0}' and State='New'", FormulaHelper.GetUserInfo().UserID));
            //购物车数量
            return Convert.ToInt32(carCount);
        }
    }
}
