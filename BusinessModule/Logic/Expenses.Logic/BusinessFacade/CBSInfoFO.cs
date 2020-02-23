using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using Config;
using Config.Logic;
using Formula;
using Expenses.Logic.Domain;
using System.Text.RegularExpressions;

namespace Expenses.Logic
{
    public partial class CBSInfoFO
    {
        public static void SynCBSInfo(Dictionary<string, object> paramDic, Expenses.Logic.SetCBSOpportunity opp)
        {
            S_EP_CBSInfo newCBSInfo = null;
            var db = SQLHelper.CreateSqlHelper(ConnEnum.Market);
            var infrasDB = SQLHelper.CreateSqlHelper(ConnEnum.InfrasBaseConfig);
            var defineDt = infrasDB.ExecuteDataTable(
                String.Format("SELECT * FROM S_EP_DefineCBSInfo with(nolock) WHERE AutoCreateCBSInfo='{0}' AND AutoCreateMethod='{1}'",
                true.ToString().ToLower(), opp.ToString()));
            foreach (DataRow defineRow in defineDt.Rows)
            {
                var define = new Expenses.Logic.Domain.S_EP_DefineCBSInfo(FormulaHelper.DataRowToDic(defineRow));
                if (define.ValidateAutoSet(paramDic))
                {
                    newCBSInfo = define.AutoSetCBSInfo(paramDic, opp);
                    break;
                }
            }
            if (newCBSInfo == null)
            {
                //由于上面没有触发自动立项（自动立项时会去做自动同步CBS节点的数据动作)
                //这里需要再次搜索所有的符合自动更新节点数据的动作
                string sql = String.Format(@"select * from S_EP_DefineCBSNode where SynData like '%{0}%'", opp.ToString());
                var dt = infrasDB.ExecuteDataTable(sql);
                foreach (DataRow defineNode in dt.Rows)
                {
                    if (defineNode["DynamicSettings"] == DBNull.Value || String.IsNullOrEmpty(defineNode["DynamicSettings"].ToString()))
                    {
                        continue;
                    }
                    var list = Formula.Helper.JsonHelper.ToList(defineNode["DynamicSettings"].ToString());
                    var item = list.FirstOrDefault(c => c.GetValue("SynDataMethod") == opp.ToString());
                    if (item == null || String.IsNullOrEmpty(item.GetValue("SynDataMethodSettings")))
                        continue;
                    var settings = Formula.Helper.JsonHelper.ToObject(item.GetValue("SynDataMethodSettings"));
                    //如果没有定位核算项目的条件，则默认不做任何操作
                    //同步节点一定需要有定位到核算项目的条件设置
                    if (String.IsNullOrEmpty(settings.GetValue("LocationCondition")))
                        continue;
                    var locationCondition = Formula.Helper.JsonHelper.ToList(settings.GetValue("LocationCondition"));
                    if (locationCondition.Count == 0) continue;
                    var locationSQL = "SELECT TOP 1 * FROM S_EP_CBSInfo WITH(NOLOCK) WHERE CBSDefineInfoID='" + defineNode["DefineID"] + "' ";
                    foreach (var condition in locationCondition)
                    {
                        var fieldValue = "";
                        if (!String.IsNullOrEmpty(condition.GetValue("Value")))
                        {
                            Regex reg = new Regex("\\{[0-9a-zA-Z_\\.]*\\}");
                            fieldValue = reg.Replace(condition.GetValue("Value"), (Match m) =>
                            {
                                #region 处理定义配置信息
                                var value = m.Value.Trim('{', '}');
                                var fields = value.Split('.');
                                if (fields.Length == 1)
                                {
                                    return paramDic.GetValue(fields[0]);
                                }
                                else
                                {
                                    if (fields[0].ToLower() == "param" && paramDic != null)
                                    {
                                        return paramDic.GetValue(fields[1]);
                                    }
                                    else
                                    {
                                        return "";
                                    }
                                }
                                #endregion
                            });
                        }
                        locationSQL += getQueryMode(condition.GetValue("Field"), condition.GetValue("QueryMode"), fieldValue);
                    }
                    var cbsInfoDt = db.ExecuteDataTable(locationSQL);
                    if (cbsInfoDt.Rows.Count == 0) continue;
                    newCBSInfo = new S_EP_CBSInfo(FormulaHelper.DataRowToDic(cbsInfoDt.Rows[0]));
                    var cbsNodeDt = db.ExecuteDataTable(String.Format("SELECT * FROM S_EP_CBSNode WHERE CBSInfoID='{0}'", newCBSInfo.ID));
                    var defineNodesList = newCBSInfo.DefineCBSNodes.AsEnumerable().ToList();
                    newCBSInfo.SynNodeWithDefineNode(opp, defineNode, cbsNodeDt, defineNodesList, paramDic);
                    var sumDataDefine =  settings.GetValue("SummaryData").Split(',');
                    if (sumDataDefine.Contains("ContractValue"))
                    {
                        newCBSInfo.SummaryContractValue();
                    }
                    if (sumDataDefine.Contains("BudgetValue"))
                    {
                        newCBSInfo.SummaryBudgetValue();
                    }
                    if (sumDataDefine.Contains("ProductionValue"))
                    {
                        newCBSInfo.SummaryPlanProductionValue();
                    }
                }
            }
        }

        public static void ValidateDeleteRelateData(string relateID)
        {
            var db = SQLHelper.CreateSqlHelper(ConnEnum.Market);
            //S_EP_CBSNode预算、成本、收入任意一个大于0，不能删除
            var sql = @"select 1 from S_EP_CBSNode 
                                    where (BudgetValue=0 or BudgetValue is null
                                    or CostValue=0 or CostValue is null
                                    or IncomeValue=0 or IncomeValue is null)  and RelateID = '" + relateID + "'";
            var dt = db.ExecuteDataTable(sql);
            if (dt.Rows.Count > 0)
                throw new Formula.Exceptions.BusinessException("不能删除已经产生预算或成本或收入的数据");
        }

        static string getQueryMode(string field, string queryMode, string fieldValue)
        {
            if (String.IsNullOrEmpty(field)) return "";
            var result = "";
            switch (queryMode)
            {
                default:
                case "In":
                    result = " AND " + field + " in ('" + fieldValue.Replace(",", "','") + "')";
                    break;
                case "Like":
                    result = " AND " + field + " LIKE '%" + fieldValue + "%'";
                    break;
                case "GreaterThanOrEqual":
                    if (!String.IsNullOrEmpty(fieldValue))
                    {
                        result = " AND " + field + " >= '" + fieldValue + "'";
                    }
                    break;
                case "LessThanOrEqual":
                    if (!String.IsNullOrEmpty(fieldValue))
                    {
                        result = " AND " + field + " <= '" + fieldValue + "'";
                    }
                    break;
                case "Equal":
                    result = " AND " + field + " = '" + fieldValue + "'";
                    break;
                case "LessThan":
                    if (!String.IsNullOrEmpty(fieldValue))
                    {
                        result = " AND " + field + " < '" + fieldValue + "'";
                    }
                    break;
                case "GreaterThan":
                    if (!String.IsNullOrEmpty(fieldValue))
                    {
                        result = " AND " + field + " > '" + fieldValue + "'";
                    }
                    break;
            }
            return result;
        }
    }
}
