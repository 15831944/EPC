using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Reflection;
using System.Data;

namespace QrCodeView
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
    }
}
