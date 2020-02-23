using System;
using System.Collections.Generic;
using System.Linq;
using Monitor.Logic.Domain;
using Monitor.Logic.Enum;
using Monitor.Logic.Helper;

namespace Monitor.Logic.BusinessFacade
{
    public class BusinessOverviewBll
    {
        public static ResultDTO GetData(string type)
        {
            var dt = DateTime.Now;
            var year = dt.Year;
            var month = dt.Month;
            var db = new MarketContext();
            string sql = "";
            if (type == EnumBusiness.Receipt.ToString())
            {
                sql = @"SELECT BelongYear,BelongMonth,
                        Convert(decimal(18,2),isnull(SUM(Amount),0)/10000) AS SumAmount 
                        FROM S_C_Receipt
                        WHERE BelongYear > (year(getdate())-5) and BelongMonth<=datepart(month,getdate())
                        GROUP BY BelongMonth,BelongYear
                        ORDER BY BelongYear DESC,BelongMonth ASC";
            }
            else
            {
                sql = @"select BelongYear,BelongMonth,
                        Convert(decimal(18,2),isnull(Sum(ContractRMBAmount),0)/10000) AS SumAmount  from S_C_ManageContract
                        WHERE BelongYear > (year(getdate())-5) and BelongMonth<=datepart(month,getdate()) and IsSigned='Signed'
                        GROUP BY BelongMonth,BelongYear
                        ORDER BY BelongYear DESC,BelongMonth ASC";
            }

            var list = db.Database.SqlQuery<BusinessOverviewBll>(sql);

            var single = new List<Dictionary<string, object>>();
            var cumulation = new List<Dictionary<string, object>>();
            for (var i = year; i > year - 5; i--)
            {
                var temp = list.Where(p => p.BelongYear == i);
                var singledata = new List<decimal>();
                var cumulationdata = new List<decimal>();
                decimal sum = 0;

                for (var j = 1; j <= month; j++)
                {
                    var item = temp.FirstOrDefault(p => p.BelongMonth == j);
                    var account = item == null ? 0 : item.SumAmount;
                    sum += account;
                    singledata.Add(account);
                    cumulationdata.Add(sum);
                }

                single.Add(new Dictionary<string, object> { { "year", i }, { "data", singledata } });
                cumulation.Add(new Dictionary<string, object> { { "year", i }, { "data", cumulationdata } });
            }
            var result = new Dictionary<string, object> { { "single", single }, { "cumulation", cumulation } };
            return WebApi.Success(result);
        }

        public int BelongYear { get; set; }
        public int BelongMonth { get; set; }
        public decimal SumAmount { get; set; }
    }
}
