using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Config;
using System.Data;
using Formula;

namespace Market.Logic
{
    public partial  class MarketHelper
    {
        public static int GetQuarter(int month)
        {
            var quarter = 0;
            quarter = (month - 1) / 3;
            return quarter + 1;
        }

        public static int GetQuarter(DateTime time)
        {
            var quarter = 0;
            var month = time.Month;
            quarter = (month - 1) / 3;
            return quarter + 1;
        }

        public static Tab CreateTab(string enumKeys,string queryfield)
        {
            var tab = new Tab();
            string sql = "select Code,Name from dbo.S_M_EnumDef where Code in ('" + enumKeys.Replace(",", "','") + "')";
            var sqlHelper = SQLHelper.CreateSqlHelper(ConnEnum.Base);
            var dt = sqlHelper.ExecuteDataTable(sql);
            foreach (DataRow dataRow in dt.Rows)
            {
                string name = dataRow["Name"].ToString();
                string key = dataRow["Code"].ToString();
                var category = CategoryFactory.GetCategory(key, queryfield, name);
                tab.Categories.Add(category);
            }
            return tab;      
        }

        public static Tab CreateTab(List<Type> sysEnums)
        {
            throw new NotImplementedException();
        }

        public static Tab CreateTab(string enumKeys,List<Type> sysEnums)
        {
            throw new NotImplementedException();
        }
    }
}
