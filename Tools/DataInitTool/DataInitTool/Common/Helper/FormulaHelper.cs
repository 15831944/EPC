using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Common.Logic
{
    public class FormulaHelper
    {
        /// <summary>
        /// 创建一个按时间排序的Guid
        /// </summary>
        /// <returns></returns>
        public static string CreateGuid()
        {
            //CAST(CAST(NEWID() AS BINARY(10)) + CAST(GETDATE() AS BINARY(6)) AS UNIQUEIDENTIFIER)
            byte[] guidArray = Guid.NewGuid().ToByteArray();
            DateTime now = DateTime.Now;

            DateTime baseDate = new DateTime(1900, 1, 1);

            TimeSpan days = new TimeSpan(now.Ticks - baseDate.Ticks);

            TimeSpan msecs = new TimeSpan(now.Ticks - (new DateTime(now.Year, now.Month, now.Day).Ticks));
            byte[] daysArray = BitConverter.GetBytes(days.Days);
            byte[] msecsArray = BitConverter.GetBytes((long)(msecs.TotalMilliseconds / 3.333333));

            Array.Copy(daysArray, 0, guidArray, 2, 2);
            //毫秒高位
            Array.Copy(msecsArray, 2, guidArray, 0, 2);
            //毫秒低位
            Array.Copy(msecsArray, 0, guidArray, 4, 2);
            return new System.Guid(guidArray).ToString();
        }

        /// <summary>
        /// 获取枚举
        /// </summary>
        /// <param name="enumKey"></param>
        /// <returns></returns>
        //public static List<EnumItem> GetEnumItem(string enumKey)
        //{
        //    if (!GlobalData.DBList.ContainsKey("Base")) throw new Exception("配置文件中不包含Base库的连接字符串");
        //    var baseSqlHelper = SqlHelper.Create(GlobalData.ConnectionDic["Base"]);
        //    var enumDef = baseSqlHelper.ExecuteObject<EnumDef>(string.Format("select * from S_M_EnumDef where Code = '{0}'", enumKey));
        //    if (enumDef.Type == "Table")
        //    {
        //        if (!GlobalData.ConnectionDic.ContainsKey(enumDef.ConnName)) throw new Exception(string.Format("配置文件中不包含{0}库的连接字符串", enumDef.ConnName));
        //        var sqlHelper = SqlHelper.Create(GlobalData.ConnectionDic[enumDef.ConnName]);
        //        return sqlHelper.ExecuteList<EnumItem>(enumDef.Sql).ToList();
        //    }
        //    else
        //    {
        //        return baseSqlHelper.ExecuteList<EnumItem>(string.Format("select Code value,Name text from S_M_EnumItem where EnumDefID = '{0}'", enumDef.ID));
        //    }
        //}
    }

    public class EnumDef
    {
        public string ID { get; set; }
        public string Code { get; set; }
        public string Type { get; set; }
        public string ConnName { get; set; }
        public string Sql { get; set; }
    }

    public class EnumItem
    {
        public string value { get; set; }
        public string text { get; set; }
    }
}