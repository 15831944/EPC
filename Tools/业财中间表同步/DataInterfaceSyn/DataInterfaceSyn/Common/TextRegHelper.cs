using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


namespace DataInterfaceSyn.Common
{
    public class TextRegHelper
    {
        public static bool MatchFileStoreFile(string value)
        {
            var regPattern = "[0-9]+_";
            return System.Text.RegularExpressions.Regex.IsMatch(value, regPattern);
        }

        public static List<String> MatchString(string value)
        {
            var regPattern = "[0-9]+_";
            var result = new List<String>();
            Regex ConnoteA = new Regex(regPattern);
            foreach (Match mch in ConnoteA.Matches(value))
            {
                string x = mch.Value.Trim();
                result.Add(x);
            }
            return result;
        }


        public static string ReplaceString(string sql, DataRow engineeringRow = null)
        {
            if (string.IsNullOrEmpty(sql))
                return sql;

            Regex reg = new Regex("\\{[0-9a-zA-Z_\\.]*\\}");
            string result = reg.Replace(sql, (Match m) =>
            {
                string value = m.Value.Trim('{', '}');
                if (engineeringRow != null && engineeringRow.Table.Columns.Contains(value))
                    return engineeringRow[value].ToString();
                switch (value)
                {
                    case "CurrentTime":
                        return DateTime.Now.ToString();
                    case "CurrentDate":
                        return DateTime.Now.Date.ToString("yyyy-MM-dd");
                    case "CurrentYear":
                        return DateTime.Now.Year.ToString();
                    case "CurrentMonth":
                        return DateTime.Now.Month.ToString();
                    case "CurrentQuarter":
                        return ((DateTime.Now.Month + 2) / 3).ToString();
                    default:
                        return "";
                }
            });
            return result;
        }

    }
}
