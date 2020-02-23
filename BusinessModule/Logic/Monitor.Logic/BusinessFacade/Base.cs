using System;
using System.Configuration;

namespace Monitor.Logic.BusinessFacade
{
    public class Base
    {
        //获取数据库名称
        public static string GetDbName(string name)
        {
            string dbName = ConfigurationManager.ConnectionStrings[name].ConnectionString;
            var m = dbName.IndexOf("=", dbName.IndexOf("=") + 1);
            var n = dbName.IndexOf(";", dbName.IndexOf(";") + 1);
            return dbName.Substring(m + 1, n - m - 1);
        }

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
    }
}
