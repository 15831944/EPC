using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Config;
using Config.Logic;
using Formula;
using Formula.Helper;
using Market.Logic.Domain;
using System.Data;


namespace Market.Logic
{
    public class IndicatorFO
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orgType"></param>
        /// <returns></returns>
        public List<S_KPI_IndicatorConfig> GetIndicatorDefine(IndicatorOrgType orgType)
        {
            var entities = FormulaHelper.GetEntities<MarketEntities>();
            var state = YesOrNo.Yes.ToString();
            var orgtype = orgType.ToString();
            var list = entities.Set<S_KPI_IndicatorConfig>().Where(d => d.State == state && d.OrgType.Contains(orgtype)).OrderBy(d => d.SortIndex).ToList();
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orgType"></param>
        /// <returns></returns>
        public List<S_KPI_Category> GetIndicatorCategory(IndicatorOrgType orgType)
        {
            var entities = FormulaHelper.GetEntities<MarketEntities>();
            var state = YesOrNo.Yes.ToString();
            var orgtype = orgType.ToString();
            var list = entities.Set<S_KPI_Category>().Where(d => d.OrgType.Contains(orgtype)).OrderBy(d => d.SortIndex).ToList();
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Year"></param>
        /// <param name="IndicatorType"></param>
        /// <returns></returns>
        public int GetTmpMaxVersion(int Year, string IndicatorType, IndicatorOrgType orgType)
        {
            string tableName = "S_KPITEMP_INDICATORCOMPANY";
            if (orgType == IndicatorOrgType.Org)
                tableName = "S_KPITEMP_INDICATORORG";
            else if (orgType == IndicatorOrgType.Person)
                tableName = "S_KPITEMP_INDICATORPERSON";
            var sqlDB = SQLHelper.CreateSqlHelper(ConnEnum.Market);
            string sql = "SELECT ISNULL(MAX(VERSION),1) FROM {2} WHERE BELONGYEAR='{0}' AND INDICATORTYPE='{1}'";
            var version = Convert.ToInt32(sqlDB.ExecuteScalar(String.Format(sql, Year, IndicatorType, tableName)));
            return version;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Year"></param>
        /// <param name="IndicatorType"></param>
        /// <returns></returns>
        public int GetTmpCurrentVersion(int Year, string IndicatorType, IndicatorOrgType orgType)
        {
            string tableName = "S_KPITEMP_INDICATORCOMPANY";
            if (orgType == IndicatorOrgType.Org)
                tableName = "S_KPITEMP_INDICATORORG";
            else if (orgType == IndicatorOrgType.Person)
                tableName = "S_KPITEMP_INDICATORPERSON";
            var sqlDB = SQLHelper.CreateSqlHelper(ConnEnum.Market);
            string sql = "SELECT isnull(Max(VERSION),-1) FROM {3} WHERE CURRENTVERSION='{2}' and BELONGYEAR='{0}' AND INDICATORTYPE='{1}'";
            var version = Convert.ToInt32(sqlDB.ExecuteScalar(String.Format(sql, Year, IndicatorType, YesNo.Yes.ToString(), tableName)));
            return version;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="Year"></param>
        /// <param name="IndicatorType"></param>
        /// <param name="orgType"></param>
        /// <returns></returns>
        public List<Dictionary<string, object>> GetTmpMaxVersionData(int Year, string IndicatorType, IndicatorOrgType orgType)
        {
            var dt = GetTmpMaxVersionTable(Year, IndicatorType, orgType);
            return FormulaHelper.DataTableToListDic(dt);
        }

        public List<Dictionary<string, object>> GetTmpMaxVersionData(int Year, string Quarter, string Month, string IndicatorType, IndicatorOrgType orgType, string whereStr = "")
        {
            var dt = GetTmpMaxVersionTable(Year, Quarter, Month, IndicatorType, orgType,whereStr);
            return FormulaHelper.DataTableToListDic(dt);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Year"></param>
        /// <param name="IndicatorType"></param>
        /// <param name="orgType"></param>
        /// <param name="Version"></param>
        /// <returns></returns>
        public List<Dictionary<string, object>> GetTmpVersionData(int Year, string IndicatorType, IndicatorOrgType orgType, int Version)
        {
            var dt = GetTmpVersionTable(Year, IndicatorType, orgType, Version);
            return FormulaHelper.DataTableToListDic(dt);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Year"></param>
        /// <param name="IndicatorType"></param>
        /// <param name="orgType"></param>
        /// <param name="Version"></param>
        /// <returns></returns>
        public DataTable GetTmpVersionTable(int Year, string IndicatorType, IndicatorOrgType orgType,int Version)
        {
            string orderStr = "ORDER BY BELONGYEAR,BELONGQUARTER,BELONGMONTH";
            string tableName = "S_KPITEMP_INDICATORCOMPANY";
            if (orgType == IndicatorOrgType.Org)
            {
                tableName = "S_KPITEMP_INDICATORORG";
                orderStr += ",SORTINDEX";
            }
            else if (orgType == IndicatorOrgType.Person)
                tableName = "S_KPITEMP_INDICATORPERSON";
            string sql = "SELECT * FROM {0} WHERE BELONGYEAR='{1}' AND INDICATORTYPE='{2}' AND VERSION='{3}' " + orderStr;
            var db = SQLHelper.CreateSqlHelper(ConnEnum.Market);
            var dt = db.ExecuteDataTable(String.Format(sql, tableName, Year, IndicatorType, Version));
            return dt;
        }

        public DataTable GetTmpVersionTable(int Year, string Quarter, string Month, string IndicatorType, IndicatorOrgType orgType, int Version, string whereStr = "")
        {
            string orderStr = "ORDER BY BELONGYEAR,BELONGQUARTER,BELONGMONTH";
            string tableName = "S_KPITEMP_INDICATORCOMPANY";
            if (orgType == IndicatorOrgType.Org)
            {
                tableName = "S_KPITEMP_INDICATORORG";
                orderStr += ",SORTINDEX";
            }
            else if (orgType == IndicatorOrgType.Person)
            {
                tableName = "S_KPITEMP_INDICATORPERSON";
                orderStr += ",USERNAME";
            }
            string sql = "SELECT * FROM {0} WHERE BELONGYEAR='{1}' AND INDICATORTYPE='{2}' {4} AND VERSION='{3}' " + orderStr;
            string tmpWhereStr = String.Empty;
            if (!String.IsNullOrEmpty(Quarter))
            {
                tmpWhereStr += " AND BELONGQUARTER IN ('" + Quarter.Replace(",", "','") + "')";
            }
            if (!String.IsNullOrEmpty(Month))
            {
                tmpWhereStr += " AND BELONGMONTH IN ('" + Month.Replace(",", "','") + "')";
            }
            if (!String.IsNullOrEmpty(whereStr)) tmpWhereStr += "  " + whereStr;
            var db = SQLHelper.CreateSqlHelper(ConnEnum.Market);
            var dt = db.ExecuteDataTable(String.Format(sql, tableName, Year, IndicatorType, Version, tmpWhereStr));
            return dt;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Year"></param>
        /// <param name="IndicatorType"></param>
        /// <param name="orgType"></param>
        /// <returns></returns>
        public DataTable GetTmpMaxVersionTable(int Year, string IndicatorType, IndicatorOrgType orgType)
        {
            var maxVersion = this.GetTmpMaxVersion(Year, IndicatorType, orgType);
            var dt = GetTmpVersionTable(Year, IndicatorType, orgType, maxVersion);
            return dt;
        }


        public DataTable GetTmpMaxVersionTable(int Year, string Quarter, string Month, string IndicatorType, IndicatorOrgType orgType, string whereStr = "")
        {
            var maxVersion = this.GetTmpMaxVersion(Year, IndicatorType, orgType);
            var dt = GetTmpVersionTable(Year, Quarter, Month, IndicatorType, orgType, maxVersion,whereStr);
            return dt;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Year"></param>
        /// <param name="IndicatorType"></param>
        /// <param name="orgType"></param>
        /// <returns></returns>
        public List<Dictionary<string, object>> GetIndicateData(int Year, string IndicatorType, IndicatorOrgType orgType)
        {
            string orderStr = "ORDER BY BELONGYEAR,BELONGQUARTER,BELONGMONTH";
            string tableName = "S_KPI_INDICATORCOMPANY";
            if (orgType == IndicatorOrgType.Org)
            {
                tableName = "S_KPI_INDICATORORG";
                orderStr += ",SORTINDEX";
            }
            else if (orgType == IndicatorOrgType.Person)
                tableName = "S_KPI_INDICATORPERSON";
            string sql = "SELECT * FROM {0} WHERE BELONGYEAR='{1}' AND INDICATORTYPE='{2}' " + orderStr;
            var db = SQLHelper.CreateSqlHelper(ConnEnum.Market);
            var dt = db.ExecuteDataTable(String.Format(sql, tableName, Year, IndicatorType));
            return FormulaHelper.DataTableToListDic(dt);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="BelongYear"></param>
        /// <param name="versionNo"></param>
        /// <param name="IndicatorType"></param>
        /// <returns></returns>
        public bool ValidateCurrentVersion(int BelongYear, int versionNo, string IndicatorType, IndicatorOrgType orgType)
        {
            string tableName = "S_KPITEMP_INDICATORCOMPANY";
            if (orgType == IndicatorOrgType.Org)
                tableName = "S_KPITEMP_INDICATORORG";
            else if (orgType == IndicatorOrgType.Person)
                tableName = "S_KPITEMP_INDICATORPERSON";
            var sqlDB = SQLHelper.CreateSqlHelper(ConnEnum.Market);
            var result = false;
            var sql = "SELECT * FROM {3} WHERE BELONGYEAR='{0}' AND INDICATORTYPE='{1}' AND VERSION='{2}' ";
            var dt = sqlDB.ExecuteDataTable(String.Format(sql, BelongYear, IndicatorType, versionNo, tableName));
            if (dt.Rows.Count == 0) return false;
            var currentVersion = this.GetTmpCurrentVersion(BelongYear, IndicatorType, orgType);
            if (currentVersion == versionNo) result = true;
            else result = false;
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="BelongYear"></param>
        /// <param name="IndicatorType"></param>
        /// <returns></returns>
        public bool ValidateVersion(int BelongYear, string IndicatorType, IndicatorOrgType orgType)
        {
            var maxVersion = this.GetTmpMaxVersion(BelongYear, IndicatorType, orgType);
            return ValidateCurrentVersion(BelongYear, maxVersion, IndicatorType, orgType);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="belongYear"></param>
        /// <param name="list"></param>
        public void SaveIndicator(int belongYear, List<Dictionary<string, object>> list, IndicatorType indicatorType, IndicatorOrgType orgType)
        {
            string tableName = "S_KPITEMP_INDICATORCOMPANY";
            if (orgType == IndicatorOrgType.Org)
                tableName = "S_KPITEMP_INDICATORORG";
            else if (orgType == IndicatorOrgType.Person)
                tableName = "S_KPITEMP_INDICATORPERSON";
            var db = SQLHelper.CreateSqlHelper(ConnEnum.Market);
            var version = this.GetTmpMaxVersion(belongYear, IndicatorType.YearIndicator.ToString(), orgType);
            foreach (var item in list)
            {
                item.SetValue("BelongYear", belongYear);
                item.SetValue("IndicatorType", indicatorType.ToString());
                if (String.IsNullOrEmpty(item.GetValue("BelongQuarter")) && !String.IsNullOrEmpty(item.GetValue("BelongMonth")))
                {
                    var belongMonth = Convert.ToInt32(item.GetValue("BelongMonth"));
                    var quarter = MarketHelper.GetQuarter(belongMonth);
                    item.SetValue("BelongQuarter", quarter);
                }
                if (String.IsNullOrEmpty(item.GetValue("Version")))
                {
                    item.SetValue("Version", version);
                    item.SetValue("CurrentVersion", YesNo.No.ToString());
                }
                if (String.IsNullOrEmpty(item.GetValue("ID")))
                    item.InsertDB(db, tableName);
                else
                    item.UpdateDB(db, tableName, item.GetValue("ID"));
            }
        }

        public void UpgradeIndicator(int belongYear, IndicatorType indicatorType, IndicatorOrgType orgType)
        {
            var categories = this.GetIndicatorCategory(orgType);
            string tableName = "S_KPITEMP_INDICATORCOMPANY";
            if (orgType == IndicatorOrgType.Org)
                tableName = "S_KPITEMP_INDICATORORG";
            else if (orgType == IndicatorOrgType.Person)
                tableName = "S_KPITEMP_INDICATORPERSON";
            var db = SQLHelper.CreateSqlHelper(ConnEnum.Market);
            var data = this.GetTmpMaxVersionData(belongYear, indicatorType.ToString(), orgType);
            var maxVersion = this.GetTmpMaxVersion(belongYear, indicatorType.ToString(), orgType) + 1;
            foreach (var item in data)
            {
                item.SetValue("Version", maxVersion);
                item.SetValue("CurrentVersion", YesNo.No.ToString());
                item.InsertDB(db, tableName, FormulaHelper.CreateGuid());
            }

            if ((orgType == IndicatorOrgType.Person || orgType == IndicatorOrgType.Org)
                &&(indicatorType== IndicatorType.QuarterIndicator|| indicatorType== IndicatorType.MonthIndicator))
            {
                var maxDt = this.GetTmpMaxVersionTable(belongYear, indicatorType.ToString(), orgType);
                var yearData = this.GetIndicateData(belongYear, IndicatorType.YearIndicator.ToString(), orgType);
                var key = orgType == IndicatorOrgType.Org ? "OrgID" : "UserID";
                var keyName = orgType == IndicatorOrgType.Org ? "OrgName" : "UserName";
                var count = indicatorType == IndicatorType.QuarterIndicator ? 4 : 12;
                foreach (var item in yearData)
                {
                    var rows = maxDt.Select(key + "='" + item.GetValue(key) + "'");
                    if (rows.Length > 0) continue;
                    for (int i = 1; i <= count; i++)
                    {
                        if (categories.Count > 0)
                        {
                            foreach (var category in categories)
                            {
                                var dic = new Dictionary<string, object>();
                                dic.SetValue("BelongYear", belongYear);
                                if (indicatorType == IndicatorType.MonthIndicator)
                                {
                                    dic.SetValue("BelongQuarter", MarketHelper.GetQuarter(i));
                                    dic.SetValue("BelongMonth", i);
                                }
                                else if (indicatorType == IndicatorType.QuarterIndicator)
                                {
                                    dic.SetValue("BelongQuarter", i);
                                }
                                dic.SetValue(key, item.GetValue(key));
                                dic.SetValue(keyName, item.GetValue(keyName));
                                dic.SetValue("Version", maxVersion);
                                dic.SetValue("BusiniessCategory", category.Code);
                                dic.SetValue("IndicatorType", indicatorType);
                                dic.SetValue("CurrentVersion", YesNo.No.ToString());
                                dic.SetValue("SortIndex", item.GetValue("SortIndex"));
                                dic.InsertDB(db, tableName);
                            }
                        }
                        else
                        {
                            var dic = new Dictionary<string, object>();
                            dic.SetValue("BelongYear", belongYear);
                            if (indicatorType == IndicatorType.MonthIndicator)
                            {
                                dic.SetValue("BelongQuarter", MarketHelper.GetQuarter(i));
                                dic.SetValue("BelongMonth", i);
                            }
                            else if (indicatorType == IndicatorType.QuarterIndicator)
                            {
                                dic.SetValue("BelongQuarter", i);
                            }
                            dic.SetValue(key, item.GetValue(key));
                            dic.SetValue(keyName, item.GetValue(keyName));
                            dic.SetValue("Version", maxVersion);
                            dic.SetValue("IndicatorType", indicatorType);
                            dic.SetValue("CurrentVersion", YesNo.No.ToString());
                            dic.SetValue("SortIndex", item.GetValue("SortIndex"));
                            dic.InsertDB(db, tableName);
                        }
                    }                  
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="belongYear"></param>
        /// <param name="list"></param>
        /// <param name="indicatorType"></param>
        /// <param name="orgType"></param>
        public void PublishIndicator(int belongYear, List<Dictionary<string, object>> list, IndicatorType indicatorType, IndicatorOrgType orgType)
        {
            var indicatorDefines = this.GetIndicatorDefine(orgType);
            var db = SQLHelper.CreateSqlHelper(ConnEnum.Market);
            string tempTableName = "S_KPITEMP_INDICATORCOMPANY";
            string tableName = "S_KPI_INDICATORCOMPANY";
            if (orgType == IndicatorOrgType.Org)
            {
                tableName = "S_KPI_INDICATORORG";
                tempTableName = "S_KPITEMP_INDICATORORG";
            }
            else if (orgType == IndicatorOrgType.Person)
            {
                tableName = "S_KPI_INDICATORPERSON";
                tempTableName = "S_KPITEMP_INDICATORPERSON";
            }
            this.SaveIndicator(belongYear, list, indicatorType, orgType);
            var maxVersion = this.GetTmpMaxVersion(belongYear, indicatorType.ToString(), orgType);
            var data = this.GetTmpMaxVersionData(belongYear, indicatorType.ToString(), orgType);
            var indicatorList = this.GetIndicateData(belongYear, indicatorType.ToString(), orgType);
            var sql = "UPDATE {0} SET CURRENTVERSION='{1}' WHERE BELONGYEAR={2} AND INDICATORTYPE='{3}'";
            db.ExecuteNonQuery(String.Format(sql,tempTableName,YesNo.No.ToString(),belongYear,indicatorType));
            foreach (var item in data)
            {
                Dictionary<string, object> dataItem;
                if (orgType == IndicatorOrgType.Org)
                {
                    dataItem = indicatorList.FirstOrDefault(delegate(Dictionary<string, object> dic)
                    {
                        if (dic.GetValue("BelongYear") == item.GetValue("BelongYear")
                            && dic.GetValue("BelongMonth") == item.GetValue("BelongMonth")
                            && dic.GetValue("BelongQuarter") == item.GetValue("BelongQuarter")
                            && dic.GetValue("IndicatorType") == item.GetValue("IndicatorType")
                            && dic.GetValue("OrgID") == item.GetValue("OrgID")
                            && dic.GetValue("BusiniessCategory") == item.GetValue("BusiniessCategory"))
                            return true;
                        else return false;
                    });
                }
                else if (orgType == IndicatorOrgType.Person)
                {
                    dataItem = indicatorList.FirstOrDefault(delegate(Dictionary<string, object> dic)
                    {
                        if (dic.GetValue("BelongYear") == item.GetValue("BelongYear")
                            && dic.GetValue("BelongMonth") == item.GetValue("BelongMonth")
                            && dic.GetValue("BelongQuarter") == item.GetValue("BelongQuarter")
                            && dic.GetValue("IndicatorType") == item.GetValue("IndicatorType")
                            && dic.GetValue("UserID") == item.GetValue("UserID")
                            && dic.GetValue("BusiniessCategory") == item.GetValue("BusiniessCategory"))
                            return true;
                        else return false;
                    });
                }
                else
                {
                    dataItem = indicatorList.FirstOrDefault(delegate(Dictionary<string, object> dic)
                    {
                        if (dic.GetValue("BelongYear") == item.GetValue("BelongYear")
                            && dic.GetValue("BelongMonth") == item.GetValue("BelongMonth")
                            && dic.GetValue("BelongQuarter") == item.GetValue("BelongQuarter")
                            && dic.GetValue("IndicatorType") == item.GetValue("IndicatorType")
                            && dic.GetValue("BusiniessCategory") == item.GetValue("BusiniessCategory"))
                            return true;
                        else return false;
                    });
                }
                if (dataItem == null)
                {
                    item.InsertDB(db,tableName,item.GetValue("ID"));
                }
                else
                {
                    foreach (var define in indicatorDefines)
                    {
                        var field = define.IndicatorCode;
                        dataItem.SetValue(field, item.GetValue(field));                    
                    }
                    dataItem.UpdateDB(db, tableName, dataItem.GetValue("ID"));
                }
                item.SetValue("CurrentVersion",YesNo.Yes.ToString());
                item.SetValue("CreateDate", DateTime.Now);
                item.UpdateDB(db, tempTableName, item.GetValue("ID"));
            }
            if (indicatorType == IndicatorType.YearIndicator)
            {
                CreateQuarterTmpData(belongYear, orgType);
                CreateMonthTmpData(belongYear, orgType);
            }
        }

        private void CreateQuarterTmpData(int belongYear,IndicatorOrgType orgType)
        {
            var indicatorDefines = this.GetIndicatorDefine(orgType);
            var db = SQLHelper.CreateSqlHelper(ConnEnum.Market);
            string tempTableName = "S_KPITEMP_INDICATORCOMPANY";
            string tableName = "S_KPI_INDICATORCOMPANY";
            if (orgType == IndicatorOrgType.Org)
            {
                tableName = "S_KPI_INDICATORORG";
                tempTableName = "S_KPITEMP_INDICATORORG";
            }
            else if (orgType == IndicatorOrgType.Person)
            {
                tableName = "S_KPI_INDICATORPERSON";
                tempTableName = "S_KPITEMP_INDICATORPERSON";
            }

            string sql = "SELECT * FROM {0} WHERE BELONGYEAR='{1}' AND INDICATORTYPE='{2}'  {3}";
            var dt = db.ExecuteDataTable(String.Format(sql, tempTableName, belongYear, IndicatorType.QuarterIndicator.ToString(), "AND CURRENTVERSION='" + YesNo.Yes.ToString() + "'"));
            if (dt.Rows.Count > 0) return;
            dt = this.GetTmpMaxVersionTable(belongYear, IndicatorType.QuarterIndicator.ToString(), orgType);
            var dataTable = db.ExecuteDataTable(String.Format(sql, tableName, belongYear, IndicatorType.YearIndicator.ToString(), ""));
            foreach (DataRow row in dataTable.Rows)
            {
                if (orgType == IndicatorOrgType.Org)
                {
                    if (Convert.ToInt32(dt.Compute("Count(ID)", " OrgID='" + row["OrgID"] + "'")) > 0) continue;
                }
                else if (orgType == IndicatorOrgType.Person)
                {
                    if (Convert.ToInt32(dt.Compute("Count(ID)", " UserID='" + row["UserID"] + "'")) > 0) continue;
                }
                for (int i = 1; i <= 4; i++)
                {
                    var dic = new Dictionary<string, object>();
                    dic.SetValue("BelongYear", belongYear);
                    dic.SetValue("BelongQuarter", i);
                    dic.SetValue("IndicatorType", IndicatorType.QuarterIndicator.ToString());
                    dic.SetValue("Version", 1);
                    dic.SetValue("CurrentVersion", YesNo.No.ToString());
                    dic.SetValue("CreateDate", DateTime.Now);
                    dic.SetValue("BusiniessCategory", row["BusiniessCategory"]);
                    if (orgType == IndicatorOrgType.Org)
                    {
                        dic.SetValue("OrgID", row["OrgID"]);
                        dic.SetValue("OrgName", row["OrgName"]);
                        dic.SetValue("SortIndex", row["SortIndex"]);
                    }
                    else if (orgType == IndicatorOrgType.Person)
                    {
                        dic.SetValue("UserID", row["UserID"]);
                        dic.SetValue("UserName", row["UserName"]);
                    }
                    dic.InsertDB(db, tempTableName,FormulaHelper.CreateGuid());
                }
            }
           
        }

        public void CreateMonthTmpData(int belongYear,IndicatorOrgType orgType)
        {
            var indicatorDefines = this.GetIndicatorDefine(orgType);
            var db = SQLHelper.CreateSqlHelper(ConnEnum.Market);
            string tempTableName = "S_KPITEMP_INDICATORCOMPANY";
            string tableName = "S_KPI_INDICATORCOMPANY";
            if (orgType == IndicatorOrgType.Org)
            {
                tableName = "S_KPI_INDICATORORG";
                tempTableName = "S_KPITEMP_INDICATORORG";
            }
            else if (orgType == IndicatorOrgType.Person)
            {
                tableName = "S_KPI_INDICATORPERSON";
                tempTableName = "S_KPITEMP_INDICATORPERSON";
            }
            string sql = "SELECT * FROM {0} WHERE BELONGYEAR='{1}' AND INDICATORTYPE='{2}'  {3}";
            var dt = db.ExecuteDataTable(String.Format(sql, tempTableName, belongYear, IndicatorType.MonthIndicator.ToString(), "AND CURRENTVERSION='" + YesNo.Yes.ToString() + "'"));
            if (dt.Rows.Count > 0) return;
            dt = this.GetTmpMaxVersionTable(belongYear, IndicatorType.MonthIndicator.ToString(), orgType);
            var dataTable = db.ExecuteDataTable(String.Format(sql, tableName, belongYear, IndicatorType.YearIndicator.ToString(), ""));
            foreach (DataRow row in dataTable.Rows)
            {
                if (orgType == IndicatorOrgType.Org)
                {
                    if (Convert.ToInt32(dt.Compute("Count(ID)", " OrgID='" + row["OrgID"] + "'")) > 0) continue;
                }
                else if (orgType == IndicatorOrgType.Person)
                {
                    if (Convert.ToInt32(dt.Compute("Count(ID)", " UserID='" + row["UserID"] + "'")) > 0) continue;
                }
                for (int i = 1; i <= 12; i++)
                {
                    var dic = new Dictionary<string, object>();
                    dic.SetValue("BelongYear", belongYear);
                    dic.SetValue("BelongMonth", i);
                    dic.SetValue("BelongQuarter", MarketHelper.GetQuarter(i));
                    dic.SetValue("IndicatorType", IndicatorType.MonthIndicator.ToString());
                    dic.SetValue("CurrentVersion", YesNo.No.ToString());
                    dic.SetValue("Version", 1);
                    dic.SetValue("CreateDate", DateTime.Now);
                    dic.SetValue("BusiniessCategory", row["BusiniessCategory"]);
                    if (orgType == IndicatorOrgType.Org)
                    {
                        dic.SetValue("OrgID", row["OrgID"]);
                        dic.SetValue("OrgName", row["OrgName"]);
                        dic.SetValue("SortIndex", row["SortIndex"]);
                    }
                    else if (orgType == IndicatorOrgType.Person)
                    {
                        dic.SetValue("UserID", row["UserID"]);
                        dic.SetValue("UserName", row["UserName"]);
                    }
                    dic.InsertDB(db, tempTableName, FormulaHelper.CreateGuid());
                }
            }
        }

    }
}
