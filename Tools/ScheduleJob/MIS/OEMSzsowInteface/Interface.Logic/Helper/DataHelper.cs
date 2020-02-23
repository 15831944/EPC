using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Interface.Logic
{
    public class DataHelper
    {
        /// <summary>
        /// DataRow转换成Hash对象
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public static Dictionary<string, object> DataRowToDic(DataRow row)
        {
            var record = new Dictionary<string, object>();
            for (int j = 0; j < row.Table.Columns.Count; j++)
            {
                object cellValue = row[j];
                if (cellValue.GetType() == typeof(DBNull))
                    cellValue = null;
                record[row.Table.Columns[j].ColumnName] = cellValue;
            }
            return record;
        }

        /// <summary>
        /// DataRow转换成Hash对象
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public static Dictionary<string, object> DataRowToDic(DataRow row, Dictionary<string, object> record)
        {
            for (int j = 0; j < row.Table.Columns.Count; j++)
            {
                object cellValue = row[j];
                if (cellValue.GetType() == typeof(DBNull))
                    cellValue = null;
                record[row.Table.Columns[j].ColumnName] = cellValue;
            }
            return record;
        }

        public static List<Dictionary<string, object>> DataTableToListDic(DataTable data)
        {
            try
            {
                var array = new List<Dictionary<string, object>>();
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    DataRow row = data.Rows[i];

                    Dictionary<string, object> record = new Dictionary<string, object>();
                    for (int j = 0; j < data.Columns.Count; j++)
                    {
                        object cellValue = row[j];
                        if (cellValue.GetType() == typeof(DBNull))
                        {
                            cellValue = null;
                        }
                        record[data.Columns[j].ColumnName] = cellValue;
                    }
                    array.Add(record);
                }
                return array;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        
        public static DateTime FormatTime(string str)
        {
            DateTime dt = DateTime.Now;
            IFormatProvider ifp = new System.Globalization.CultureInfo("zh-CN", true);
            var formatStr = "yyyyMMddHHmmssfff";
            if (str.Length == 14)
                formatStr = "yyyyMMddHHmmss";
            DateTime.TryParseExact(str, formatStr, ifp, System.Globalization.DateTimeStyles.None, out dt);
            return dt;
        }

    }
}
