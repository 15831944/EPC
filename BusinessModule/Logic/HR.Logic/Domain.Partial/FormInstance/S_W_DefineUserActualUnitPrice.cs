using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Formula;
using System.Text.RegularExpressions;
using Formula.Helper;
using Config;
using Config.Logic;
using System.Data;

namespace HR.Logic.Domain
{
    public partial class S_W_DefineUserActualUnitPrice
    {
        public const string ParamPre = "Param";
        public const string UserID = "UserID";
        public const string RelateValue = "RelateValue";
        public const string BelongYear = "BelongYear";
        public const string BelongMonth = "BelongMonth";
        public const string HrUserSourceSql = "select ID as HrUserID,UserID,Name from T_Employee";

        private List<Dictionary<string, object>> _paramDefineList;
        private List<Dictionary<string, object>> ParamDefineList
        {
            get
            {
                if (_paramDefineList == null)
                {
                    _paramDefineList = String.IsNullOrEmpty(this.ParamSource) ? new List<Dictionary<string, object>>() :
                        JsonHelper.ToList(this.ParamSource);
                }
                return _paramDefineList;
            }
        }

        public void Validate()
        {
            Regex reg = new Regex("\\{[0-9a-zA-Z_\\.]*\\}");
            #region 校验计算公式中定义的带大括号标志内容格式是否正确
            var variateList = reg.Matches(CalcFormula);
            foreach (Match variate in variateList)
            {
                string value = variate.Value.Trim('{', '}');
                var matchDatas = value.Split('.');
                if (matchDatas.Length == 1)
                {
                    throw new Formula.Exceptions.BusinessValidationException("计算数据源中定义变量必须指定变量的来源，以固定格式{XXX.XXX}来定义");
                }
                else if (matchDatas[0] == ParamPre)
                {
                    if (this.ParamDefineList.Count == 0 || this.ParamDefineList.Count(c => c.GetValue("Code") == matchDatas[1]) == 0)
                    {
                        throw new Formula.Exceptions.BusinessValidationException("计算数据源定义中，未能找到编号为【" + matchDatas[1] + "】的参数数据源定义");
                    }
                }
                else
                {
                    throw new Formula.Exceptions.BusinessValidationException("计算数据源定义中，无法识别数据获取对象，获取对象前缀必须是 Param");
                }
            }
            #endregion
            #region 校验参数数据源查询结果必须包含规定字段
            var sqlHelper = SQLHelper.CreateSqlHelper(ConnEnum.HR);
            int paramGroupCount = this.ParamDefineList.GroupBy(a => a.GetValue("Code")).Select(a => a.Key).Count();
            if (paramGroupCount != this.ParamDefineList.Count())
            {
                throw new Formula.Exceptions.BusinessValidationException("参数数据源存在编号重复的行");
            }

            foreach (var item in this.ParamDefineList)
            {
                if (String.IsNullOrEmpty(item.GetValue("SQL")))
                {
                    throw new Formula.Exceptions.BusinessValidationException("编号为【" + item.GetValue("Code") + "】的参数数据源SQL语句未定义");
                }

                string sql = item.GetValue("SQL");

                DataTable dt = null;
                try
                {
                    dt = sqlHelper.ExecuteDataTable(sql);
                }
                catch (Exception exp)
                {
                    throw new Formula.Exceptions.BusinessValidationException("参数数据源【" + item.GetValue("Code") + "】的SQL语句执行错误，请检查配置信息【" + exp.Message + "】");
                }

                if (!dt.Columns.Contains(UserID))
                {
                    throw new Formula.Exceptions.BusinessValidationException("参数数据源【" + item.GetValue("Code") + "】的SQL语句的执行没有包含【" + UserID + "】");
                }
                if (!dt.Columns.Contains(RelateValue))
                {
                    throw new Formula.Exceptions.BusinessValidationException("参数数据源【" + item.GetValue("Code") + "】的SQL语句的执行没有包含【" + RelateValue + "】");
                }
            }
            #endregion
        }

        /// <summary>
        /// Key：UserID、UnitPrice
        /// </summary>
        /// <param name="belongYear"></param>
        /// <param name="belongMonth"></param>
        /// <returns></returns>
        public void GetActualUnitPrice(int belongYear, int belongMonth)
        {
            Regex reg = new Regex("\\{[0-9a-zA-Z_\\.]*\\}");
            Regex sReg = new Regex("[\\+\\-\\*/]");
            var calDt = new DataTable();
            var hrDB = SQLHelper.CreateSqlHelper(ConnEnum.HR);
            var userDt = hrDB.ExecuteDataTable(HrUserSourceSql);
            var unitPriceDt = hrDB.ExecuteDataTable(
                String.Format("SELECT * FROM S_W_UserUnitPrice WHERE BelongYear='{0}' AND BelongMonth='{1}' and PriceType='{2}'",
                belongYear, belongMonth, "Actual"
                ));
            unitPriceDt.PrimaryKey = new DataColumn[] { unitPriceDt.Columns["UserID"] };
            var insertDt = hrDB.ExecuteDataTable("SELECT * FROM S_W_UserUnitPrice WHERE 1<>1");
            var paraSourceDTDic = GetParamSourceDTDic(belongYear, belongMonth);
            StringBuilder updateCommand = new StringBuilder();
            foreach (DataRow userRow in userDt.Rows)
            {
                string userID = userRow[UserID].ToString();
                if (String.IsNullOrEmpty(userID))
                {
                    continue;
                    //throw new Formula.Exceptions.BusinessValidationException(String.Format("用户【{0}】没有账号ID，请确认该用户已经分配了系统账号，否则无法计算人工单价", userRow["Name"]));
                }
                var calFormulaAfterReplace = reg.Replace(CalcFormula, (Match m) =>
                {
                    var value = m.Value.Trim('{', '}');
                    var fields = value.Split('.');
                    decimal relateValue = 0;
                    if (fields[0] == ParamPre)
                    {
                        var paramDT = paraSourceDTDic[fields[1]] as DataTable;
                        var relateValueObj = paramDT.Compute("Sum(RelateValue)", string.Format("{0} = '{1}'", UserID, userID));
                        if (relateValueObj != null)
                            Decimal.TryParse(relateValueObj.ToString(), out relateValue);
                    }
                    return relateValue.ToString();
                });
                Decimal actualUnitPrice = 0;
                object objValue = null;
                try
                {
                    if (sReg.IsMatch(calFormulaAfterReplace))
                    {
                        objValue = calDt.Compute(calFormulaAfterReplace, "");
                    }
                    else
                    {
                        objValue = calFormulaAfterReplace;
                    }
                }
                catch
                {
                    objValue = calFormulaAfterReplace;
                }
                Decimal.TryParse(objValue.ToString(), out actualUnitPrice);
                var row = unitPriceDt.Rows.Find(userID);
                //if (actualUnitPrice > 0)
                {
                    if (row == null)
                    {
                        //insert
                        var newRow = insertDt.NewRow();
                        newRow["ID"] = FormulaHelper.CreateGuid();
                        newRow["HRUserID"] = userRow["HrUserID"];
                        newRow["UserID"] = userRow["UserID"];
                        newRow["UserName"] = userRow["Name"];
                        newRow["PriceType"] = "Actual";
                        newRow["UnitPrice"] = actualUnitPrice;
                        newRow["ResourceCode"] = "";
                        newRow["BelongYear"] = belongYear;
                        newRow["BelongMonth"] = belongMonth;
                        newRow["StartDate"] = new DateTime(belongYear, belongMonth, 1);
                        newRow["CreateDate"] = DateTime.Now;
                        newRow["ModifyDate"] = DateTime.Now; ;
                        insertDt.Rows.Add(newRow);
                    }
                    else
                    {
                        updateCommand.AppendLine(String.Format("UPDATE S_W_UserUnitPrice SET UnitPrice={0} WHERE ID='{1}'", actualUnitPrice, row["ID"]));
                    }
                }
            }
            if (!String.IsNullOrEmpty(updateCommand.ToString().Trim()))
            {
                hrDB.ExecuteNonQuery(updateCommand.ToString());
            }
            if (insertDt.Rows.Count > 0)
            {
                hrDB.BulkInsertToDB(insertDt, "S_W_UserUnitPrice");
            }
        }

        private Dictionary<string, DataTable> GetParamSourceDTDic(int belongYear, int belongMonth)
        {
            var res = new Dictionary<string, DataTable>();            
            int paramGroupCount = this.ParamDefineList.GroupBy(a => a.GetValue("Code")).Select(a => a.Key).Count();
            if (paramGroupCount != this.ParamDefineList.Count())
            {
                throw new Formula.Exceptions.BusinessValidationException("参数数据源存在编号重复的行");
            }
            foreach (var item in this.ParamDefineList)
            {
                if (String.IsNullOrEmpty(item.GetValue("SQL")))
                {
                    throw new Formula.Exceptions.BusinessValidationException("编号为【" + item.GetValue("Code") + "】的参数数据源SQL语句未定义");
                }

                if (String.IsNullOrEmpty(item.GetValue("ConnName")))
                {
                    throw new Formula.Exceptions.BusinessValidationException("编号为【" + item.GetValue("Code") + "】的参数数据源数据库链接未定义");
                }

                string sql = item.GetValue("SQL");
                sql = sql.Replace("{" + BelongYear + "}", belongYear.ToString()).Replace("{" + BelongMonth + "}", belongMonth.ToString());
                try
                {
                    var sqlHelper = SQLHelper.CreateSqlHelper(item.GetValue("ConnName"));
                    var dt = sqlHelper.ExecuteDataTable(sql);
                    if (!dt.Columns.Contains(UserID))
                    {
                        throw new Formula.Exceptions.BusinessValidationException("参数数据源【" + item.GetValue("Code") + "】的SQL语句的执行没有包含【" + UserID + "】");
                    }
                    if (!dt.Columns.Contains(RelateValue))
                    {
                        throw new Formula.Exceptions.BusinessValidationException("参数数据源【" + item.GetValue("Code") + "】的SQL语句的执行没有包含【" + RelateValue + "】");
                    }

                    res.SetValue(item.GetValue("Code"), dt);
                }
                catch (Exception exp)
                {
                    throw new Formula.Exceptions.BusinessValidationException("参数数据源【" + item.GetValue("Code") + "】的SQL语句执行错误，请检查配置信息【" + exp.Message + "】");
                }
            }
            return res;
        }


    }
}
