using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Reflection;
using System.Data;

namespace WeiXinScheduleJob
{
    public static class DictionaryExtend
    {
        public static string GetValue(this Dictionary<string, object> dic, string key)
        {
            string result = string.Empty;
            if (!dic.ContainsKey(key)) return result;
            if (dic[key] == null || dic[key] == DBNull.Value) return result;
            result = dic[key].ToString();
            return result;
        }

        public static object GetObject(this Dictionary<string, object> dic, string key)
        {
            string result = string.Empty;
            if (!dic.ContainsKey(key)) return null;
            if (dic[key] == null || dic[key] == DBNull.Value) return null;
            return dic[key];
        }

        public static void SetValue(this Dictionary<string, object> dic, string key, string value)
        {
            dic[key] = value;
        }

        public static void SetValue(this Dictionary<string, object> dic, string key, object value)
        {
            dic[key] = value;
        }

        //泛型操作
        public static T GetValue<T>(this Dictionary<string, T> dic, string key)
        {
            T result = default(T);
            if (!dic.ContainsKey(key)) return result;
            result = dic[key];
            return result;
        }

        public static void SetValue<T>(this Dictionary<string, T> dic, string key, T value)
        {
            if (!dic.ContainsKey(key))
                dic.Add(key, value);
            else
                dic[key] = value;
        }

        /// <summary>
        /// 用字典填充对象 
        /// 包含try语句
        /// 如果数据匹配问题较多，可能造成性能问题
        /// </summary>
        /// <returns></returns>
        public static T ToModel<T>(this Dictionary<string, object> dic) where T : class,new()
        {
            T result = new T();

            PropertyInfo[] arrPtys = typeof(T).GetProperties();
            foreach (PropertyInfo destPty in arrPtys)
            {
                if (!dic.ContainsKey(destPty.Name))
                    continue;
                if (destPty.CanRead == false)
                    continue;
                try
                {
                    destPty.SetValue(result, dic.GetValue<object>(destPty.Name), null);
                }
                catch { }
            }
            return result;
        }

        #region 生成ID

        static string CreateGuid(int index)
        {
            //CAST(CAST(NEWID() AS BINARY(10)) + CAST(GETDATE() AS BINARY(6)) AS UNIQUEIDENTIFIER)
            byte[] guidArray = Guid.NewGuid().ToByteArray();
            DateTime now = DateTime.Now;

            DateTime baseDate = new DateTime(1900, 1, 1);

            TimeSpan days = new TimeSpan(now.Ticks - baseDate.Ticks);

            TimeSpan msecs = new TimeSpan(now.Ticks - (new DateTime(now.Year, now.Month, now.Day).Ticks));
            byte[] daysArray = BitConverter.GetBytes(days.Days);
            byte[] msecsArray = BitConverter.GetBytes((long)((msecs.TotalMilliseconds + index * 10) / 3.333333));

            Array.Copy(daysArray, 0, guidArray, 2, 2);
            //毫秒高位
            Array.Copy(msecsArray, 2, guidArray, 0, 2);
            //毫秒低位
            Array.Copy(msecsArray, 0, guidArray, 4, 2);
            return new System.Guid(guidArray).ToString();
        }

        /// <summary>
        /// 创建一个按时间排序的Guid
        /// </summary>
        /// <returns></returns>
        static string CreateGuid()
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

        static DateTime GetDateTimeFromGuid(string strGuid)
        {
            Guid guid = Guid.Parse(strGuid);

            DateTime baseDate = new DateTime(1900, 1, 1);
            byte[] daysArray = new byte[4];
            byte[] msecsArray = new byte[4];
            byte[] guidArray = guid.ToByteArray();

            // Copy the date parts of the guid to the respective byte arrays. 
            Array.Copy(guidArray, guidArray.Length - 6, daysArray, 2, 2);
            Array.Copy(guidArray, guidArray.Length - 4, msecsArray, 0, 4);

            // Reverse the arrays to put them into the appropriate order 
            Array.Reverse(daysArray);
            Array.Reverse(msecsArray);

            // Convert the bytes to ints 
            int days = BitConverter.ToInt32(daysArray, 0);
            int msecs = BitConverter.ToInt32(msecsArray, 0);

            DateTime date = baseDate.AddDays(days);
            date = date.AddMilliseconds(msecs * 3.333333);

            return date;
        }

        #endregion
    }
}
