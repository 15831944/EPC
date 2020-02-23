using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Config;
using Formula;
using Formula.Helper;
using Config.Logic;
using Market.Logic;
namespace Market.Logic.Domain
{
    public partial class S_KPI_IndicatorPerson
    {
        /// <summary>
        /// 获取某年某类型当前版本的指标
        /// </summary>
        /// <param name="belongYear"></param>
        /// <param name="indicatorType"></param>
        /// <returns></returns>
        public static DataTable GetCurrentVersionIndicatorPerson(string belongYear, string indicatorType)
        {
            SQLHelper SqlHelper = SQLHelper.CreateSqlHelper(ConnEnum.Market);
            var sql = @"select * from S_KPI_IndicatorPerson where BelongYear= '{0}' and IndicatorType='{1}' ";
            sql = string.Format(sql, belongYear, indicatorType);
            DataTable indicatorPersonQuarterdt = SqlHelper.ExecuteDataTable(sql);
            return indicatorPersonQuarterdt;
        }

        /// <summary>
        /// 验证某年某业务类型的指标是否已经发布
        /// </summary>
        /// <param name="belongYear"></param>
        /// <param name="indicatorType"></param>
        /// <returns></returns>
        public static bool IsIndicatorPersonPublish(string belongYear, string indicatorType)
        {
            DataTable dt = GetCurrentVersionIndicatorPerson(belongYear, indicatorType);
            if (dt != null && dt.Rows != null && dt.Rows.Count > 0)
                return true;
            else
                return false;
        }
    }
}
