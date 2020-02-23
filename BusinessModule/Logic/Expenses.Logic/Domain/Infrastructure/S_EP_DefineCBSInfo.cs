
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Config;
using Config.Logic;
using Formula;
using Formula.Helper;
using System.Data;
using System.Text.RegularExpressions;
using System.Web;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Expenses.Logic.Domain
{
    public partial class S_EP_DefineCBSInfo : BaseEPModel
    {

        public S_EP_DefineCBSInfo(Dictionary<string, object> dic)
        {
            this.FillModel(dic);
        }

        S_EP_DefineCBSNode _RootNode;
        [NotMapped]
        [JsonIgnore]
        public S_EP_DefineCBSNode RootNode
        {
            get
            {
                if (_RootNode == null)
                {
                    var sql = "select * from S_EP_DefineCBSNode WITH(NOLOCK)  where DefineID='" + this.ID + "'";
                    var dt = this.InfrasDB.ExecuteDataTable(sql);
                    if (dt.Rows.Count == 0)
                    {
                        return null;
                    }
                    _RootNode = new S_EP_DefineCBSNode(FormulaHelper.DataRowToDic(dt.Rows[0]));
                }
                return _RootNode;
            }
        }

        public void SetRootDefineNode()
        {
            var dt = this.InfrasDB.ExecuteDataTable(String.Format("Select * from S_EP_DefineCBSNode WITH(NOLOCK) where DefineID='{0}' and NodeType='{1}'"
                , this.ModelDic.GetValue("ID"), CBSNodeType.Root.ToString()));
            if (dt.Rows.Count == 0)
            {
                var defineNode = new Dictionary<string, object>();
                defineNode.SetValue("ID", FormulaHelper.CreateGuid());
                defineNode.SetValue("Name", this.ModelDic.GetValue("Name"));
                defineNode.SetValue("Code", this.ModelDic.GetValue("Code"));
                defineNode.SetValue("ParentID", "");
                defineNode.SetValue("FullID", defineNode.GetValue("ID"));
                defineNode.SetValue("DefineID", this.ModelDic.GetValue("ID"));
                defineNode.SetValue("NodeType", CBSNodeType.Root.ToString());
                defineNode.SetValue("CBSType", CBSNodeType.Root.ToString());
                defineNode.SetValue("SortIndex", 0);
                defineNode.SetValue("SubjectID", "");
                defineNode.SetValue("SubjectFulID", "");
                defineNode.SetValue("IsDynamic", false.ToString().ToLower());
                defineNode.InsertDB(this.DB, "S_EP_DefineCBSNode", defineNode.GetValue("ID"));
            }
            else
            {
                this.InfrasDB.ExecuteNonQuery(String.Format("update S_EP_DefineCBSNode set Name='{0}',Code='{3}' where  DefineID='{1}' and NodeType='{2}'",
                    this.ModelDic.GetValue("Name"), this.ModelDic.GetValue("ID"), CBSNodeType.Root.ToString(), this.ModelDic.GetValue("Code")));
            }
        }

        public bool ValidateAutoSet(Dictionary<string, object> formDic)
        {
            var result = false;
            if (formDic == null) throw new Formula.Exceptions.BusinessValidationException("匹配核算模式时，输入的立项对象不能对空值");
            if (String.IsNullOrEmpty(this.ModelDic.GetValue("AutoCreateCondition")))
            {
                result = true;
            }
            else
            {
                var conditions = JsonHelper.ToList(this.ModelDic.GetValue("AutoCreateCondition"));
                if (conditions.Count == 0)
                    result = true;
                else
                {
                    var resultList = new List<Condition>();
                    foreach (var condition in conditions)
                    {
                        var conditionResult = false;
                        #region 校验条件定义
                        var propertyValue = formDic.GetValue(condition.GetValue("Field"));
                        if (propertyValue == null)
                            continue;
                        var condiftionValue = condition.GetValue("Value");
                        switch (condition.GetValue("QueryMode"))
                        {
                            default:
                            case "In":
                                conditionResult = condiftionValue.Split(',').Contains(propertyValue);
                                break;
                            case "Like":
                                conditionResult = propertyValue.Contains(condiftionValue);
                                break;
                            case "GreaterThanOrEqual":
                                conditionResult = Convert.ToDecimal(propertyValue) >= Convert.ToDecimal(condiftionValue);
                                break;
                            case "LessThanOrEqual":
                                conditionResult = Convert.ToDecimal(propertyValue) <= Convert.ToDecimal(condiftionValue);
                                break;
                            case "Equal":
                                conditionResult = propertyValue.Trim() == condiftionValue.Trim();
                                break;
                            case "LessThan":
                                conditionResult = Convert.ToDecimal(propertyValue) < Convert.ToDecimal(condiftionValue);
                                break;
                            case "GreaterThan":
                                conditionResult = Convert.ToDecimal(propertyValue) > Convert.ToDecimal(condiftionValue);
                                break;
                        }
                        #endregion
                        condition.SetValue("Result", conditionResult);
                        resultList.Add(new Condition
                        {
                            FieldName = condition.GetValue("FieldName"),
                            GroupName = condition.GetValue("Group"),
                            Result = conditionResult
                        });
                    }
                    var groupInfoList = resultList.Select(d => d.GroupName).Distinct().ToList();
                    foreach (var groupInfo in groupInfoList)
                    {
                        if (resultList.Where(d => d.GroupName == groupInfo).Count(d => !d.Result) == 0)
                        {
                            result = true; break;
                        }
                    }
                }
            }
            return result;
        }

        public S_EP_CBSInfo AutoSetCBSInfo(Dictionary<string, object> paramDic, SetCBSOpportunity opp)
        {
            #region 校验数据
            if (this.ModelDic.GetValue("AutoCreateCBSInfo").ToLower() != true.ToString().ToLower())
                throw new Formula.Exceptions.BusinessValidationException("配置定义未设置自动立项属性，无法自理设置核算项目信息");
            if (String.IsNullOrEmpty(this.ModelDic.GetValue("AutoCreateMethod")))
            {
                throw new Formula.Exceptions.BusinessValidationException("自动设置核算项目必须指定立项依据，请检查配置项是否设置完成");
            }
            if (!paramDic.ContainsKey("ID"))
            {
                throw new Formula.Exceptions.BusinessValidationException("输入对象的ID不能为空，RelateID绑定不允许为空");
            }
            var dataSourceDefine = new List<Dictionary<string, object>>();
            if (!String.IsNullOrEmpty(this.ModelDic.GetValue("AutoCreateDataSource")))
            {
                dataSourceDefine = JsonHelper.ToList(this.ModelDic.GetValue("AutoCreateDataSource"));
            }
            #endregion

            var dataSourceList = new Dictionary<string, DataTable>();
            foreach (var item in dataSourceDefine)
            {
                var key = item.GetValue("Code");
                if (String.IsNullOrEmpty(item.GetValue("SQL"))) continue;
                var value = SQLHelper.CreateSqlHelper(item.GetValue("ConnName")).
                    ExecuteDataTable(this.ReplaceString(item.GetValue("SQL"), null, paramDic));
                dataSourceList.Add(key, value);
            }
            string sql = "select * from S_EP_CBSInfo where CBSDefineInfoID='" + this.ModelDic.GetValue("ID") + "' ";
            if (!String.IsNullOrEmpty(this.ModelDic.GetValue("LocationCondition")))
            {
                var locationCondition = JsonHelper.ToList(this.ModelDic.GetValue("LocationCondition"));
                if (locationCondition.Count == 0)
                {
                    sql += " AND RelateID='" + paramDic.GetValue("ID") + "'";
                }
                else
                {
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
                        sql += getQueryMode(condition.GetValue("Field"), condition.GetValue("QueryMode"), fieldValue);
                    }
                }
            }

            var cbsInfoDt = this.DB.ExecuteDataTable(sql);
            S_EP_CBSInfo cbsInfo;
            if (cbsInfoDt.Rows.Count == 0)
            {
                //新建核算项目
                var cbsInfoDic = new Dictionary<string, object>();
                _setAdaptertValue(cbsInfoDic, paramDic, dataSourceList);
                if (String.IsNullOrEmpty(cbsInfoDic.GetValue("RelateID")))
                {
                    cbsInfoDic.SetValue("RelateID", paramDic.GetValue("ID"));
                }
                if (String.IsNullOrEmpty(cbsInfoDic.GetValue("RelateID")))
                {
                    throw new Formula.Exceptions.BusinessValidationException("必须指定业务关联ID，否则无法立项");
                }
                cbsInfoDic.SetValue("ID", FormulaHelper.CreateGuid());
                cbsInfoDic.SetValue("Type", CBSDefineType.Produce.ToString());
                cbsInfoDic.SetValue("BuildBasis", this.ModelDic.GetValue("AutoCreateMethod"));
                cbsInfo = new S_EP_CBSInfo(cbsInfoDic);
                cbsInfo.Build(this);
                cbsInfo.SynNodeData(opp, paramDic);
            }
            else
            {
                //更新核算项目
                var cbsInfoDic = Formula.FormulaHelper.DataRowToDic(cbsInfoDt.Rows[0]);
                _setAdaptertValue(cbsInfoDic, paramDic, dataSourceList);
                cbsInfoDic.UpdateDB(this.DB, "S_EP_CBSInfo", cbsInfoDic.GetValue("ID"));
                cbsInfo = new S_EP_CBSInfo(cbsInfoDic);
                cbsInfo.SynNodeData(opp, paramDic);
            }

            var sumDataDefine = this.ModelDic.GetValue("SummaryData").Split(',');
            if (sumDataDefine.Contains("ContractValue"))
            {
                cbsInfo.SummaryContractValue();
            }
            if (sumDataDefine.Contains("BudgetValue"))
            {
                cbsInfo.SummaryBudgetValue();
            }
            if (sumDataDefine.Contains("ProductionValue"))
            {
                cbsInfo.SummaryPlanProductionValue();
            }
            return cbsInfo;
        }

        void _setAdaptertValue(Dictionary<string, object> dic, Dictionary<string, object> paramDic, Dictionary<string, DataTable> dataSourceList)
        {
            var stringFo = FormulaHelper.CreateFO<StringFuncFo>();
            var adapter = this.ModelDic.GetValue("AutoDataAdapter");
            var defaultValuesAdapter = new List<Dictionary<string, object>>();
            if (!String.IsNullOrEmpty(adapter))
            {
                defaultValuesAdapter = JsonHelper.ToList(adapter);
                var list = defaultValuesAdapter.Where(c => !String.IsNullOrEmpty(c.GetValue("DefaultValue"))).ToList();
                foreach (var item in list)
                {
                    var formatExpress = item.GetValue("DefaultValue");
                    Regex reg = new Regex("\\{[0-9a-zA-Z_\\.]*\\}");
                    var result = reg.Replace(formatExpress, (Match m) =>
                    {
                        #region 处理定义配置信息
                        var value = m.Value.Trim('{', '}');
                        var fields = value.Split('.');
                        if (fields.Length == 1)
                        {
                            if (paramDic != null)
                                return paramDic.GetValue(fields[0]);
                            else
                                return "";
                        }
                        else
                        {
                            if (fields[0].ToLower() == "param" && paramDic != null)
                            {
                                return paramDic.GetValue(fields[1]);
                            }
                            else
                            {
                                if (dataSourceList.ContainsKey(fields[0]))
                                {
                                    if (dataSourceList[fields[0]] == null)
                                        return "";
                                    var sourceDt = dataSourceList[fields[0]] as DataTable;
                                    if (sourceDt.Rows.Count == 0) return "NoneData";
                                    if (!sourceDt.Columns.Contains(fields[1])) return "NoneData";
                                    if (sourceDt.Rows[0][fields[1]] == DBNull.Value) return "";
                                    return sourceDt.Rows[0][fields[1]].ToString();
                                }
                                else
                                {
                                    return "";
                                }
                            }
                        }
                        #endregion
                    });
                    dic.SetValue(item.GetValue("Field"), result);
                }
            }
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
