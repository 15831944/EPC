using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Config;
using Formula;
using Formula.Helper;
using Config.Logic;
using System.Data;

namespace Market.Logic.Domain
{
    public partial class S_KPI_IndicatorCompany
    {

        public static DataTable TransFormCurrentVersionIndicatorCompanyToDt(string belongYear, string indicatorType)
        {
            SQLHelper sqlHelper = SQLHelper.CreateSqlHelper(ConnEnum.Market);
            string sql = @" select * from  S_KPI_IndicatorCompany where BelongYear = '{0}' and IndicatorType = '{1}'";
            sql = string.Format(sql, belongYear, indicatorType);
            DataTable dt = sqlHelper.ExecuteDataTable(sql);
            return dt;
        }
    }
}
