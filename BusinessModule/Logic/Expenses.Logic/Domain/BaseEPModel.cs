using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Config;
using Config.Logic;
using Formula;
using Formula.Helper;
using System.Data;
using System.Reflection;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Expenses.Logic.Domain
{
    public class BaseEPModel
    {

        bool _isNewModel = true;
        /// <summary>
        /// 模型标记状态
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        public bool IsNewModel
        {
            get { return _isNewModel; }
        }

        [NotMapped]
        [JsonIgnore]
        public string TableName
        {
            get
            {
                return this.GetType().Name;
            }
        }

        protected string _ID;
        /// <summary>
        /// 主键ID
        /// </summary>
        public string ID
        {
            get
            {
                return this._ID;
            }
            set
            {
                this._ID = value;
            }
        }

        /// <summary>
        /// 数据库访问对象
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        public virtual SQLHelper DB
        {
            get
            {
                return SQLHelper.CreateSqlHelper(ConnEnum.Market);
            }
        }

        /// <summary>
        /// 数据库访问对象
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        public virtual SQLHelper InfrasDB
        {
            get
            {
                return SQLHelper.CreateSqlHelper(ConnEnum.InfrasBaseConfig);
            }
        }

        Dictionary<string, object> _modelDic;
        /// <summary>
        /// 
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        public Dictionary<string, object> ModelDic
        {
            get
            {
                return _modelDic;
            }
        }

        public void SetValue(string key, object value)
        {
            this.ModelDic.SetValue(key, value);
        }

        public string GetValue(string key)
        {
            if (this.ModelDic == null) return "";
            return this.ModelDic.GetValue(key);
        }

        public object GetValueObj(string key)
        {
            if (this.ModelDic == null) return "";
            if (!this.ModelDic.ContainsKey(key)) return "";
            return this.ModelDic[key];
        }

        public void FillModel(DataRow row)
        {
            var dic = FormulaHelper.DataRowToDic(row);
            this._modelDic = dic;
            this._isNewModel = false;
            var type = this.GetType();
            var properties = type.GetProperties();
            foreach (var pi in properties)
            {
                if (!row.Table.Columns.Contains(pi.Name))
                    continue;
                if (pi.PropertyType.FullName == "System.String")
                {
                    //为兼容Oracle，不能使用bool型，因此使用char(1)
                    string value = "";
                    if (row[pi.Name] != null && row[pi.Name] != DBNull.Value)
                    {
                        value = row[pi.Name].ToString().Trim();
                    }
                    pi.SetValue(this, value, null);
                }
                else if (pi.PropertyType == typeof(bool) || pi.PropertyType == typeof(Nullable<bool>))
                {
                    string value = row[pi.Name].ToString();
                    if (value.ToLower() == "true" || value == "1")
                        pi.SetValue(this, true, null);
                    else
                        pi.SetValue(this, false, null);
                }
                else if (pi.PropertyType.IsValueType)
                {
                    Object value = null;
                    Type t = System.Nullable.GetUnderlyingType(pi.PropertyType);
                    if (t == null)
                        t = pi.PropertyType;
                    MethodInfo mis = t.GetMethod("Parse", new Type[] { typeof(string) });
                    try
                    {
                        value = mis.Invoke(null, new object[] { row[pi.Name].ToString() });
                    }
                    catch
                    {
                        throw new Exception(string.Format("数据类型转换失败:将‘{0}’转换为{1}类型时.", row[pi.Name].ToString(), t.Name));
                    }
                    pi.SetValue(this, value, null);
                }
            }
        }

        public void FillModel(Dictionary<string, object> Dic)
        {
            this._modelDic = Dic;
            this._isNewModel = false;
            var type = this.GetType();
            var properties = type.GetProperties();
            foreach (var pi in properties)
            {
                if (!Dic.ContainsKey(pi.Name))
                    continue;
                if (pi.PropertyType.FullName == "System.String")
                {
                    //为兼容Oracle，不能使用bool型，因此使用char(1)
                    string value = "";
                    if (!String.IsNullOrEmpty(Dic.GetValue(pi.Name)))
                    {
                        value = Dic.GetValue(pi.Name).Trim();
                    }
                    pi.SetValue(this, value, null);
                }
                else if (pi.PropertyType == typeof(bool) || pi.PropertyType == typeof(Nullable<bool>))
                {
                    string value = Dic.GetValue(pi.Name);
                    if (value.ToLower() == "true" || value == "1")
                        pi.SetValue(this, true, null);
                    else
                        pi.SetValue(this, false, null);
                }
                else if (pi.PropertyType.IsValueType)
                {
                    Object value = null;
                    Type t = System.Nullable.GetUnderlyingType(pi.PropertyType);
                    if (t == null)
                        t = pi.PropertyType;
                    MethodInfo mis = t.GetMethod("Parse", new Type[] { typeof(string) });
                    try
                    {
                        value = mis.Invoke(null, new object[] { Dic.GetValue(pi.Name) });
                    }
                    catch
                    {
                        throw new Exception(string.Format("数据类型转换失败:将‘{0}’转换为{1}类型时.", Dic.GetValue(pi.Name), t.Name));
                    }
                    pi.SetValue(this, value, null);
                }
            }
        }

        protected Dictionary<string, object> GetDataDicByID(string tableName, string id, ConnEnum connName = ConnEnum.Market, bool withNolock = false)
        {
            string sql = withNolock ? String.Format("select * from {0} {2} where ID='{1}'", tableName, id, "with(nolock)")
               : String.Format("select * from {0} {2} where ID='{1}'", tableName, id, "");
            var db = SQLHelper.CreateSqlHelper(connName);
            var dt = db.ExecuteDataTable(sql);
            if (dt.Rows.Count == 0)
            {
                return null;
            }
            else
            {
                return FormulaHelper.DataRowToDic(dt.Rows[0]);
            }
        }


        #region String替换

        /// <summary>
        /// 替换{}内容为当前地址栏参数或当前人信息
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        protected string ReplaceString(string sql, DataRow row = null, Dictionary<string, string> dic = null, Dictionary<string, DataTable> dtDic = null)
        {
            if (string.IsNullOrEmpty(sql))
                return sql;

            var user = FormulaHelper.GetUserInfo();
            Regex reg = new Regex("\\{[0-9a-zA-Z_\\.]*\\}");
            string result = reg.Replace(sql, (Match m) =>
            {
                string value = m.Value.Trim('{', '}');

                if (!string.IsNullOrEmpty(HttpContext.Current.Request[value]))
                    return HttpContext.Current.Request[value];

                if (dtDic != null && dtDic.Count > 0)
                {
                    var arr = value.Split('.');
                    if (arr.Length == 1)
                    {
                        if (dtDic.ContainsKey(value)) //默认值为整个表
                            return JsonHelper.ToJson(dtDic[value]);
                    }
                    else if (arr.Length == 2) //默认子编号名.字段名
                    {
                        if (dtDic.ContainsKey(arr[0]))
                        {
                            var dt = dtDic[arr[0]];
                            if (dt.Rows.Count > 0 && dt.Columns.Contains(arr[1]))
                            {
                                return dt.Rows[0][arr[1]].ToString();
                            }
                        }
                    }

                }
                if (row != null && row.Table.Columns.Contains(value))
                    return row[value].ToString();
                if (dic != null && dic.ContainsKey(value))
                    return dic[value];

                switch (value)
                {
                    case Formula.Constant.CurrentUserID:
                        return user.UserID;
                    case Formula.Constant.CurrentUserName:
                        return user.UserName;
                    case Formula.Constant.CurrentUserOrgID:
                        return user.UserOrgID;
                    case Formula.Constant.CurrentUserOrgCode:
                        return user.UserOrgCode;
                    case Formula.Constant.CurrentUserOrgName:
                        return user.UserOrgName;
                    case Formula.Constant.CurrentUserPrjID:
                        return user.UserPrjID;
                    case Formula.Constant.CurrentUserPrjName:
                        return user.UserPrjName;
                    case "CurrentUserOrgFullName":
                        return user.UserFullOrgName;
                    case "CurrentUserCorpID":
                        return user.UserCompanyID;
                    case "CurrentUserCorpName":
                        return user.UserCompanyName;
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
                    case "CurrentUserOrgFullID":
                        return user.UserFullOrgID;
                    default:
                        return "";
                }
            });

            return result;
        }


        /// <summary>
        /// 替换{}内容为当前地址栏参数或当前人信息
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        protected string ReplaceString(string sql, DataRow row = null, Dictionary<string, object> dic = null, Dictionary<string, DataTable> dtDic = null)
        {
            if (string.IsNullOrEmpty(sql))
                return sql;

            var user = FormulaHelper.GetUserInfo();
            Regex reg = new Regex("\\{[0-9a-zA-Z_\\.]*\\}");
            string result = reg.Replace(sql, (Match m) =>
            {
                string value = m.Value.Trim('{', '}');

                if (!string.IsNullOrEmpty(HttpContext.Current.Request[value]))
                    return HttpContext.Current.Request[value];

                if (dtDic != null && dtDic.Count > 0)
                {
                    var arr = value.Split('.');
                    if (arr.Length == 1)
                    {
                        if (dtDic.ContainsKey(value)) //默认值为整个表
                            return JsonHelper.ToJson(dtDic[value]);
                    }
                    else if (arr.Length == 2) //默认子编号名.字段名
                    {
                        if (dtDic.ContainsKey(arr[0]))
                        {
                            var dt = dtDic[arr[0]];
                            if (dt.Rows.Count > 0 && dt.Columns.Contains(arr[1]))
                            {
                                return dt.Rows[0][arr[1]].ToString();
                            }
                        }
                    }

                }
                if (row != null && row.Table.Columns.Contains(value))
                    return row[value].ToString();
                if (dic != null && dic.ContainsKey(value))
                    return dic.GetValue(value);

                switch (value)
                {
                    case Formula.Constant.CurrentUserID:
                        return user.UserID;
                    case Formula.Constant.CurrentUserName:
                        return user.UserName;
                    case Formula.Constant.CurrentUserOrgID:
                        return user.UserOrgID;
                    case Formula.Constant.CurrentUserOrgCode:
                        return user.UserOrgCode;
                    case Formula.Constant.CurrentUserOrgName:
                        return user.UserOrgName;
                    case Formula.Constant.CurrentUserPrjID:
                        return user.UserPrjID;
                    case Formula.Constant.CurrentUserPrjName:
                        return user.UserPrjName;
                    case "CurrentUserOrgFullName":
                        return user.UserFullOrgName;
                    case "CurrentUserCorpID":
                        return user.UserCompanyID;
                    case "CurrentUserCorpName":
                        return user.UserCompanyName;
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
                    case "CurrentUserOrgFullID":
                        return user.UserFullOrgID;
                    default:
                        return "";
                }
            });

            return result;
        }

        #endregion
    }
}
