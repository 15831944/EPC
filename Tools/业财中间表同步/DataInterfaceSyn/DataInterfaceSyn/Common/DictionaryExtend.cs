using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Reflection;
using System.Data;

namespace DataInterfaceSyn.Common
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


        private static DataTable GetFieldTable(SQLHelper access, string tableName)
        {
            string sql = string.Format("select FieldCode=a.name from syscolumns a  inner join sysobjects d on a.id=d.id and d.name='{0}'", tableName);
            return access.ExecuteDataTable(sql);
        }

        public static string CreateInsertSql(this Dictionary<string, object> entity, SQLHelper access, string tableName, string ID)
        {
            var fields = GetFieldTable(access, tableName).AsEnumerable().Select(c => c[0].ToString()).ToArray();
            StringBuilder sbField = new StringBuilder();
            StringBuilder sbValue = new StringBuilder();

            foreach (var item in entity)
            {
                string name = item.Key.ToString();
                if (name == "ID")
                    continue;
                if (!fields.Contains(name))
                    continue;
                if (IsNullOrEmpty(item.Value)) continue;
                string value = item.Value.ToString();
                sbField.AppendFormat(",{0}", name);
                sbValue.AppendFormat(",'{0}'", value.Replace("'", "''"));
            }
            string sql = "";
            if (String.IsNullOrEmpty(ID))
                sql = string.Format(@"INSERT INTO {0} ({1}) VALUES ({2})", tableName, sbField.ToString().TrimStart(','), sbValue.ToString().TrimStart(','));
            else
                sql = string.Format(@"INSERT INTO {0} (ID{2}) VALUES ('{1}'{3})", tableName, ID, sbField, sbValue);
            return sql;
        }

        public static string CreateUpdateSql(this Dictionary<string, object> entity, SQLHelper access, string tableName, string ID)
        {
            var fields = GetFieldTable(access, tableName).AsEnumerable().Select(c => c[0].ToString()).ToArray();
            StringBuilder sb = new StringBuilder();
            foreach (var item in entity)
            {
                string name = item.Key.ToString();
                if (name == "ID")
                    continue;
                if (!fields.Contains(name))
                    continue;
                if (item.Value == null)
                    continue;
                string value = item.Value.ToString();
                if (String.IsNullOrEmpty(value))
                    sb.AppendFormat(",{0}=NULL", name);
                else
                    sb.AppendFormat(",{0}='{1}'", name, value.Replace("'", "''"));
            }
            string sql = string.Format(@"UPDATE {0} SET {2} WHERE ID='{1}'", tableName, ID, sb.ToString().Trim(','));
            return sql;
        }

        public static string CreateDeleteSql(this Dictionary<string, object> entity, string tableName)
        {
            string sql = " delete from {0} where ID = '{1}' ";
            if (!entity.ContainsKey("ID") || IsNullOrEmpty(entity["ID"]))
                throw new Exception("删除语句必须指定主键");
            sql = String.Format(sql, tableName, entity["ID"].ToString());
            return sql;
        }

        public static string InsertDB(this Dictionary<string, object> entity, SQLHelper access, string tableName, string ID = "", bool isIntID = false)
        {
            string id = ID;
            if (String.IsNullOrEmpty(id) && !isIntID)
            {
                id = CreateGuid();
                entity["ID"] = id;
            }
            if (string.IsNullOrEmpty(entity.GetValue("CreateDate")))
                entity.SetValue("CreateDate", DateTime.Now);
            string sql = entity.CreateInsertSql(access, tableName, id);
            var result = access.ExecuteNonQuery(sql);
            if (Convert.ToInt32(result) < 0)
                throw new Exception("创建表单实例错误;SQL语句执行错误:" + sql);
            return id;
        }

        public static void UpdateDB(this Dictionary<string, object> entity, SQLHelper access, string tableName, string ID)
        {
            string sql = entity.CreateUpdateSql(access, tableName, ID);
            int result = Convert.ToInt32(access.ExecuteNonQuery(sql));
            if (result < 0)
                throw new Exception("更新表单实例错误;SQL语句执行错误:" + sql);

        }

        public static void DeleteDB(this Dictionary<string, object> entity, SQLHelper access, string tableName, string ID)
        {
            string sql = entity.CreateDeleteSql(tableName);
            access.ExecuteNonQuery(sql);
        }

        public static bool ValidateExist(this Dictionary<string, object> entity, SQLHelper access, string tableName, string ID, string field, string scopes)
        {
            if (ID == null)
                ID = "";
            if (String.IsNullOrEmpty(field)) throw new Exception("Validate字段,field参数不能为空");
            if (!entity.ContainsKey(field)) throw new Exception("字段为【" + field + "】的KEY不存在");
            string value = string.Empty;
            if (!IsNullOrEmpty(entity[field]))
                value = entity[field].ToString();
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" ID<>'{0}' and {1}='{2}'", ID, field, entity[field].ToString());
            foreach (string item in scopes.Split(','))
            {
                if (string.IsNullOrEmpty(item) || !entity.ContainsKey(item) || IsNullOrEmpty(entity[item]))
                    continue;
                sb.AppendFormat(" and {0}='{1}'", item, entity[item].ToString());
            }

            string sql = string.Format("select count(1) from {0} where {1}", tableName, sb);
            object obj = access.ExecuteScalar(sql);
            return Convert.ToInt32(obj) > 0;
        }

        static bool IsNullOrEmpty(object value)
        {
            if (value == null || value == DBNull.Value)
                return true;
            if (String.IsNullOrEmpty(value.ToString()))
                return true;
            return false;
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
