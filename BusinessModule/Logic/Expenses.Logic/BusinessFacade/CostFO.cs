using Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Expenses.Logic
{
    public class CostFO
    {
        public static decimal GetUserUnitPrice(string userID, int belongYear, int belongMonth, bool bGetActualUnitPrice = false)
        {
            var dateTime = new DateTime(belongYear, belongMonth, 1);

            string actualPriceWhere = " and PriceType = '" + (bGetActualUnitPrice ? "Actual" : "Budget") + "'";

            string sql = String.Format(@"select TOP 1 * from S_W_UserUnitPrice
where StartDate<='{1}'  and UserID='{0}' {2}
order by StartDate desc ", userID, dateTime, actualPriceWhere);

            var db = SQLHelper.CreateSqlHelper(ConnEnum.HR);
            var dt = db.ExecuteDataTable(sql);
            if (dt.Rows.Count == 0)
            {
                return 0;
            }
            else
            {
                if (dt.Rows[0]["UnitPrice"] == DBNull.Value || dt.Rows[0]["UnitPrice"] == null)
                    return 0;
                else
                    return Convert.ToDecimal(dt.Rows[0]["UnitPrice"]);
            }
        }

        /// <summary>
        /// 各单据验证会计期间，并确认是否已经关账
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static void ValidatePeriodIsClosed(DateTime date, string moreExceptionInfo = "")
        {
            if (string.IsNullOrEmpty(moreExceptionInfo)) moreExceptionInfo = "无法再发起当月的申请";

            var db = SQLHelper.CreateSqlHelper(ConnEnum.Market);
            var InfrasDB = SQLHelper.CreateSqlHelper(ConnEnum.InfrasBaseConfig);
            string sql = String.Format("select top 1 * from S_EP_DefineMonthPeriod where StartDate<='{0}' and EndDate>='{0}'", date);
            var dt = InfrasDB.ExecuteDataTable(sql);
            if (dt.Rows.Count == 0)
            {
                throw new Formula.Exceptions.BusinessValidationException(String.Format("未能设置指定日期【{0}】所在年份的会计期间，请在基础数据设置中先设计会计期间", date.Year));
            }
            var belongYear = dt.Rows[0]["Year"];
            var belongMonth = dt.Rows[0]["Month"];
            sql = String.Format("select count(ID) from S_EP_LockAccount where State<>'{2}' and BelongYear={0} and BelongMonth>={1}", belongYear, belongMonth, ModifyState.Removed.ToString());
            var obj = Convert.ToInt32(db.ExecuteScalar(sql));
            if (obj > 0)
            {
                throw new Formula.Exceptions.BusinessValidationException(String.Format("【{0}】年【{1}】月已经关账，{2}", belongYear, belongMonth, moreExceptionInfo));
            }
        }

        /// <summary>
        /// 各单据验证会计期间，并确认是否已经结账
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static void ValidatePeriodIsSettled(DateTime date, string moreExceptionInfo = "")
        {
            if (string.IsNullOrEmpty(moreExceptionInfo)) moreExceptionInfo = "无法再发起当月的申请";

            var db = SQLHelper.CreateSqlHelper(ConnEnum.Market);
            var InfrasDB = SQLHelper.CreateSqlHelper(ConnEnum.InfrasBaseConfig);
            string sql = String.Format("select top 1 * from S_EP_DefineMonthPeriod where StartDate<='{0}' and EndDate>='{0}'", date);
            var dt = InfrasDB.ExecuteDataTable(sql);
            if (dt.Rows.Count == 0)
            {
                throw new Formula.Exceptions.BusinessValidationException(String.Format("未能设置指定日期【{0}】所在年份的会计期间，请在基础数据设置中先设计会计期间", date.Year));
            }
            var belongYear = dt.Rows[0]["Year"];
            var belongMonth = dt.Rows[0]["Month"];
            sql = String.Format("select count(ID) from S_EP_SettleAccount where State<>'{2}' and BelongYear={0} and BelongMonth>={1}", belongYear, belongMonth, ModifyState.Removed.ToString());
            var obj = Convert.ToInt32(db.ExecuteScalar(sql));
            if (obj > 0)
            {
                throw new Formula.Exceptions.BusinessValidationException(String.Format("【{0}】年【{1}】月已经结账，{2}", belongYear, belongMonth, moreExceptionInfo));
            }
        }
    }
}
