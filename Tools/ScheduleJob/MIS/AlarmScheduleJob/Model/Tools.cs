using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Alarm.Model
{
    public class Tools
    {
        public static string ReplaceString(string str, DataRow dataRow = null)
        {
            Regex reg = new Regex("\\{[0-9a-zA-Z_\\.]*\\}");
            string result = reg.Replace(str, (Match m) =>
            {
                string value = m.Value.Trim('{', '}');
                if (dataRow != null && dataRow.Table.Columns.Contains(value))
                {
                    DateTime date;
                    if (DateTime.TryParse(dataRow[value].ToString(), out date))
                        return date.ToString("yyyy-MM-dd HH:mm");
                    else
                        return dataRow[value].ToString();
                }
                switch (value)
                {
                    case "CurrentTime":
                        return DateTime.Now.ToString();
                    case "CurrentDate":
                        return DateTime.Now.Date.ToString("yyyy-MM-dd HH:mm");
                    default:
                        return "";
                }
            });
            return result;
        }
    }
}