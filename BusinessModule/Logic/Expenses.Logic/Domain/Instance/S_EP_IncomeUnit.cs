using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Config;
using Config.Logic;
using Formula;
using Formula.Helper;
using System.Data;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Expenses.Logic.Domain
{
    public partial class S_EP_IncomeUnit : BaseEPModel
    {
        public S_EP_IncomeUnit()
        {

        }

        public S_EP_IncomeUnit(Dictionary<string, object> dic)
        {
            if (dic == null)
                throw new Formula.Exceptions.BusinessValidationException("初始化构造S_EP_IncomeUnit的键值对不能对空值");
            this.FillModel(dic);
        }

        public S_EP_CBSNode _node;
        [NotMapped]
        [JsonIgnore]
        public S_EP_CBSNode CBSNode
        {
            get
            {
                if (_node == null)
                {
                    var cbsDic = this.GetDataDicByID("S_EP_CBSNode", this.ModelDic.GetValue("CBSNodeID"));
                    if (cbsDic != null)
                    {
                        _node = new S_EP_CBSNode(cbsDic);
                    }
                }
                return _node;
            }
        }

        S_EP_DefineIncome _defineInCome;
        [NotMapped]
        [JsonIgnore]
        public S_EP_DefineIncome DefineInCome
        {
            get
            {
                if (_defineInCome == null)
                {
                    var dic = this.GetDataDicByID("S_EP_DefineIncome", this.ModelDic.GetValue("UnitDefineID"), ConnEnum.InfrasBaseConfig);
                    _defineInCome = new S_EP_DefineIncome(dic);
                }
                return _defineInCome;
            }
        }

        public void SetIncomeDefineID()
        {
            var sql = "SELECT * FROM S_EP_DefineIncome WITH(NOLOCK) WHERE NodeID='" + this.CBSNode.ModelDic.GetValue("DefineID") + "'";
            var inComeDefineDt = this.InfrasDB.ExecuteDataTable(sql);
            var resID = string.Empty; string incomeType = string.Empty;
            foreach (DataRow inComeDefine in inComeDefineDt.Rows)
            {
                if (inComeDefine["Condition"] != null && inComeDefine["Condition"] != DBNull.Value &&
                    !String.IsNullOrEmpty(inComeDefine["Condition"].ToString()))
                {
                    var resultList = new List<InComeCondition>();
                    bool result = false;
                    var conditions = JsonHelper.ToList(inComeDefine["Condition"].ToString());
                    foreach (var condition in conditions)
                    {
                        var conditionResult = false;
                        #region 校验条件定义
                        var propertyValue = this.CBSNode.ModelDic.GetValue(condition.GetValue("FieldName"));
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
                                if (String.IsNullOrEmpty(propertyValue) || String.IsNullOrEmpty(condiftionValue))
                                {
                                    conditionResult = false;
                                }
                                else
                                {
                                    conditionResult = Convert.ToDecimal(propertyValue) >= Convert.ToDecimal(condiftionValue);
                                }
                                break;
                            case "LessThanOrEqual":
                                if (String.IsNullOrEmpty(propertyValue) || String.IsNullOrEmpty(condiftionValue))
                                {
                                    conditionResult = false;
                                }
                                else
                                {
                                    conditionResult = Convert.ToDecimal(propertyValue) <= Convert.ToDecimal(condiftionValue);
                                }
                                break;
                            case "Equal":
                                if (String.IsNullOrEmpty(propertyValue) || String.IsNullOrEmpty(condiftionValue))
                                {
                                    conditionResult = false;
                                }
                                else
                                {
                                    conditionResult = propertyValue.Trim() == condiftionValue.Trim();
                                }
                                break;
                            case "LessThan":
                                if (String.IsNullOrEmpty(propertyValue) || String.IsNullOrEmpty(condiftionValue))
                                {
                                    conditionResult = false;
                                }
                                else
                                {
                                    conditionResult = Convert.ToDecimal(propertyValue) < Convert.ToDecimal(condiftionValue);
                                }
                                break;
                            case "GreaterThan":
                                if (String.IsNullOrEmpty(propertyValue) || String.IsNullOrEmpty(condiftionValue))
                                {
                                    conditionResult = false;
                                }
                                else
                                {
                                    conditionResult = Convert.ToDecimal(propertyValue) > Convert.ToDecimal(condiftionValue);
                                }
                                break;
                        }
                        #endregion
                        condition.SetValue("Result", conditionResult);
                        resultList.Add(new InComeCondition
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
                    if (result)
                    {
                        resID = inComeDefine["ID"].ToString();
                        incomeType = inComeDefine["IncomeType"].ToString();
                        break;
                    }
                }
            }
            if (String.IsNullOrEmpty(resID))
            {
                var defaultMode = inComeDefineDt.AsEnumerable().FirstOrDefault(d => d["IsDefault"].ToString() == true.ToString().ToLower());
                if (defaultMode != null)
                {
                    resID = defaultMode["ID"].ToString();
                    incomeType = defaultMode["IncomeType"].ToString();
                }                   
            }
            if (String.IsNullOrEmpty(resID))
            {
                throw new Formula.Exceptions.BusinessValidationException("没有匹配到对应的权责收入确认定义方式，无法建立收入单元，请确定权责收入配置中是否有满足条件的配置或默认值");
            }
            this.DB.ExecuteNonQuery(String.Format("UPDATE S_EP_IncomeUnit SET UnitDefineID='{0}',IncomeType='{2}' WHERE ID='{1}'", resID, this.ModelDic.GetValue("ID"), incomeType));
            this.ModelDic.SetValue("UnitDefineID", resID);
        }

        public void ReInitProgress(bool isReinit = false)
        {
            //如果是节点法，则根据模板初始化节点实例
            if (this.DefineInCome.ModelDic.GetValue("IncomeType") == IncomeType.Progress.ToString())
            {
                if (isReinit)
                {
                    //如果需要重设进度模板，则需要删除未确认的节点和节点分组
                    this.DB.ExecuteNonQuery("delete from S_EP_IncomeUnit_ProgressNode where State!='Finish' and IncomeUnitID='" + this.ID + "'");
                    this.DB.ExecuteNonQuery(String.Format(@"delete from S_EP_IncomeUnit_Progress
where ID not in (select ProgressInfoID from S_EP_IncomeUnit_ProgressNode where IncomeUnitID='{0}') and UnitID='{0}'", this.ID));
                }
                //查询出已经存在的节点分组
                var existProgressInfoDt = this.DB.ExecuteDataTable(String.Format(@"select * from S_EP_IncomeUnit_Progress 
where UnitID='{0}'", this.ID));

                //获取已经存在的节点
                var existProgressNodeDt = this.DB.ExecuteDataTable(String.Format(@"select * from dbo.S_EP_IncomeUnit_ProgressNode
where IncomeUnitID='{0}'", this.ID));

                //获取收入单元定义信息
                var incomeDetailDefineDt = this.InfrasDB.ExecuteDataTable(String.Format(@"select * from S_EP_DefineIncome_Detail with(nolock) 
                where DefineIncomeID='{0}' order by sortindex ", this.DefineInCome.ID));

                //获取节点模板
                var progressDefineNodeDt = this.InfrasDB.ExecuteDataTable(String.Format(@"select S_EP_DefineProgress_ProgressNode.*
,S_EP_DefineIncome_Detail.ProgressDefineName, DefineIncomeID 
from S_EP_DefineIncome_Detail inner join S_EP_DefineProgress_ProgressNode
on S_EP_DefineIncome_Detail.ProgressDefineID = S_EP_DefineProgress_ProgressNode.ProgressDefineID
where S_EP_DefineIncome_Detail.DefineIncomeID='{0}' order by sortindex", this.DefineInCome.ID));
                foreach (DataRow incomDetailDefine in incomeDetailDefineDt.Rows)
                {
                    if (incomDetailDefine["ProgressDefineID"] == null || incomDetailDefine["ProgressDefineID"] == DBNull.Value ||
                        String.IsNullOrEmpty(incomDetailDefine["ProgressDefineID"].ToString()))
                        continue;

                    var row = existProgressInfoDt.AsEnumerable().FirstOrDefault(c => c["DefineProgressID"] != DBNull.Value && c["DefineProgressID"].ToString() == incomDetailDefine["ProgressDefineID"].ToString());
                    if (row == null)
                    {
                        //如果收入单元没有节点分组,则新增
                        var dic = FormulaHelper.DataRowToDic(incomDetailDefine);
                        dic.SetValue("ID", FormulaHelper.CreateGuid());
                        dic.SetValue("Name", incomDetailDefine["ProgressDefineName"]);
                        dic.SetValue("UnitID", this.ID);
                        dic.SetValue("Scale", incomDetailDefine["Scale"]);
                        dic.SetValue("DefineProgressID", incomDetailDefine["ProgressDefineID"]);
                        dic.SetValue("SortIndex", incomDetailDefine["SortIndex"]);
                        dic.InsertDB(this.DB, "S_EP_IncomeUnit_Progress", dic.GetValue("ID"));
                        _initProgressNode(progressDefineNodeDt, existProgressNodeDt, incomDetailDefine, dic);
                    }
                    else
                    {
                        //如果已经有节点分组，则获取节点分组后直接初始化节点信息
                        var dic = FormulaHelper.DataRowToDic(row);
                        _initProgressNode(progressDefineNodeDt, existProgressNodeDt, incomDetailDefine, dic);
                    }
                }

                var nodesDt = this.DB.ExecuteDataTable(String.Format("select * from S_EP_IncomeUnit_ProgressNode with(nolock) where IncomeUnitID='{0}'", this.ID));
                var nodes = nodesDt.AsEnumerable().ToList();
                foreach (DataRow node in nodes)
                {
                    var totalScaleValue = nodes.Where(c => Convert.ToInt32(c["SortIndex"]) <= Convert.ToInt32(node["SortIndex"])).Sum(c => Convert.ToDecimal(c["AllScale"]));
                    var allIncomeScale = 0m;
                    if (node["IsIncomeNode"].ToString() == true.ToString().ToLower())
                    {
                        allIncomeScale = totalScaleValue;
                    }
                    this.DB.ExecuteNonQuery(String.Format("update S_EP_IncomeUnit_ProgressNode set TotalAllScale={0}, AllIncomeScale={1} where ID='{2}'", totalScaleValue,
allIncomeScale, node["ID"]));
                }
            }
        }

        void _initProgressNode(DataTable defineNodeDt, DataTable currentProgressNodeDt, DataRow incomDetailDefine, Dictionary<string, object> progressInfo)
        {
            var defineNodes = defineNodeDt.AsEnumerable().Where(c => c["ProgressDefineID"].ToString() ==
                progressInfo.GetValue("DefineProgressID")).OrderBy(c => c["SortIndex"]).ToList();
            foreach (DataRow defineNode in defineNodes)
            {
                var node = currentProgressNodeDt.AsEnumerable().FirstOrDefault(c => c["ProgressInfoID"].ToString() == progressInfo.GetValue("ID") &&
                    c["ProgressNodeDefineID"].ToString() == defineNode["ID"].ToString());
                if (node == null)
                {
                    var dic = new Dictionary<string, object>();
                    dic.SetValue("ID", FormulaHelper.CreateGuid());
                    dic.SetValue("IncomeUnitID", this.ID);
                    dic.SetValue("ProgressInfoID", progressInfo.GetValue("ID"));
                    dic.SetValue("NodeName", defineNode["Name"]);
                    dic.SetValue("NodeCode", defineNode["Code"]);
                    dic.SetValue("Scale", defineNode["Scale"]);
                    dic.SetValue("TotalScale", defineNode["TotalScale"]);
                    var allScale = Convert.ToDecimal(defineNode["Scale"]) * Convert.ToDecimal(progressInfo.GetValue("Scale")) / 100;
                    dic.SetValue("AllScale", allScale);
                    dic.SetValue("TotalAllScale", 0);
                    dic.SetValue("AllIncomeScale", 0);
                    dic.SetValue("ProgressNodeDefineID", defineNode["ID"]);
                    dic.SetValue("IncomDefineDetailID", incomDetailDefine["ID"]);
                    var sortIndex = Convert.ToDecimal(progressInfo.GetValue("SortIndex")) * 10 + Convert.ToDecimal(defineNode["SortIndex"]);
                    dic.SetValue("SortIndex", sortIndex);
                    dic.SetValue("IsIncomeNode", defineNode["IsIncomeNode"]);
                    dic.SetValue("State", "Create");
                    dic.SetValue("Status", "Create");
                    dic.InsertDB(this.DB, "S_EP_IncomeUnit_ProgressNode", dic.GetValue("ID"));
                }
                else
                {

                }
            }
        }



    }
}
