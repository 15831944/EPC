using DataInterfaceSyn.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInterfaceSyn
{
    public class SourceTableStore
    {
        public static List<Dictionary<string, object>> GetTableData(string source, string sourceConnStr)
        {
            List<Dictionary<string, object>> res = new List<Dictionary<string, object>>();
            var enumConn = (EnumConn)Enum.Parse(typeof(EnumConn), sourceConnStr, true);
            var sqlHelper = SQLHelper.CreateSqlHelper(enumConn);

            //source支持sql语句
            if (source.ToLower().StartsWith("select"))
            {
                source = "(" + source + ") _tmpppppt";
            }

            string sql = "select * from " + source + "";


            var dt = sqlHelper.ExecuteDataTable(sql);
            foreach (DataRow dr in dt.Rows)
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                foreach (DataColumn dc in dt.Columns)
                {
                    dic.SetValue(dc.ColumnName, dr[dc.ColumnName].ToString());
                }
                res.Add(dic);
            }
            return res;
        }

        public static int GetTableDataTotalCount(string source, string sourceConnStr)
        {
            List<Dictionary<string, object>> res = new List<Dictionary<string, object>>();
            var enumConn = (EnumConn)Enum.Parse(typeof(EnumConn), sourceConnStr, true);
            var sqlHelper = SQLHelper.CreateSqlHelper(enumConn);

            //source支持sql语句
            if (source.ToLower().StartsWith("select"))
            {
                source = "(" + source + ") _tmpppppt";
            }

            var totalCountObj = sqlHelper.ExecuteScalar(string.Format("select count(*) FROM ({0})tmp", source));
            if(totalCountObj != null && totalCountObj != DBNull.Value)
            {
                return (int)totalCountObj;
            }
            return 0;
        }

        public static List<Dictionary<string, object>> GetTableDataByPage(string source, string sourceConnStr, int pageId, int pageSize)
        {
            List<Dictionary<string, object>> res = new List<Dictionary<string, object>>();
            var enumConn = (EnumConn)Enum.Parse(typeof(EnumConn), sourceConnStr, true);
            var sqlHelper = SQLHelper.CreateSqlHelper(enumConn);

            //source支持sql语句
            if (source.ToLower().StartsWith("select"))
            {
                source = "(" + source + ") _tmpppppt";
            }

            string sql = "select * from " + source + "";
            sql = string.Format("select newid() as _id ,_tmpTable.* from ({0}) _tmpTable", sql);

            int startRowNumber = (pageId - 1) * pageSize;
            int endRowNumber = pageId * pageSize;
            string pageSql = string.Format("select * from (select  ROW_NUMBER() over(order by son._id) as rows, son.*  from ({0}) son) tmp where rows between {1} and {2}", sql, startRowNumber, endRowNumber);

            var dt = sqlHelper.ExecuteDataTable(sql);
            foreach (DataRow dr in dt.Rows)
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                foreach (DataColumn dc in dt.Columns)
                {
                    dic.SetValue(dc.ColumnName, dr[dc.ColumnName].ToString());
                }
                res.Add(dic);
            }
            return res;
        }
    }
}
