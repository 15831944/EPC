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
    public partial class S_EP_ProductionUnit : BaseEPModel
    {
        public S_EP_ProductionUnit()
        {

        }

        public S_EP_ProductionUnit(Dictionary<string, object> dic)
        {
            if (dic == null)
                throw new Formula.Exceptions.BusinessValidationException("初始化构造S_EP_ProductionUnit的键值对不能对空值");
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

        S_EP_DefineProduction _defineProduction;
        [NotMapped]
        [JsonIgnore]
        public S_EP_DefineProduction DefineProduction
        {
            get
            {
                if (_defineProduction == null)
                {
                    var dic = this.GetDataDicByID("S_EP_DefineProduction", this.ModelDic.GetValue("DefineProductionID"), ConnEnum.InfrasBaseConfig);
                    _defineProduction = new S_EP_DefineProduction(dic);
                }
                return _defineProduction;
            }
        }

        public void SetProductionDefineID()
        {
            var sql = "SELECT * FROM S_EP_DefineProduction WITH(NOLOCK) WHERE NodeID='" + this.CBSNode.ModelDic.GetValue("DefineID") + "'";
            var productionDefineDt = this.InfrasDB.ExecuteDataTable(sql);
            var resID = string.Empty; string productionType = string.Empty;
            foreach (DataRow productionDefine in productionDefineDt.Rows)
            {
                if (productionDefine["Condition"] != null && productionDefine["Condition"] != DBNull.Value &&
                    !String.IsNullOrEmpty(productionDefine["Condition"].ToString()))
                {
                    var resultList = new List<ProductionCondition>();
                    bool result = false;
                    var conditions = JsonHelper.ToList(productionDefine["Condition"].ToString());
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
                        resultList.Add(new ProductionCondition
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
                        resID = productionDefine["ID"].ToString();
                        productionType = productionDefine["ProductionType"].ToString();
                        break;
                    }
                }
            }
            if (String.IsNullOrEmpty(resID))
            {
                var defaultMode = productionDefineDt.AsEnumerable().FirstOrDefault(d => d["IsDefault"].ToString() == true.ToString().ToLower());
                if (defaultMode != null)
                {
                    resID = defaultMode["ID"].ToString();
                    productionType = defaultMode["ProductionType"].ToString();
                }
            }
            if (String.IsNullOrEmpty(resID))
            {
                throw new Formula.Exceptions.BusinessValidationException("没有匹配到对应的产值确认方式，无法建立产值单元，请确定产值节点定义配置中是否有满足条件的配置或默认值");
            }
            this.DB.ExecuteNonQuery(String.Format("UPDATE S_EP_ProductionUnit SET DefineProductionID='{0}',ProductionType='{2}' WHERE ID='{1}'", resID, this.ModelDic.GetValue("ID"), productionType));
            this.ModelDic.SetValue("DefineProductionID", resID);
        }

        public void ReInitProgress(bool isReinit = false)
        {
            #region 如果是节点法，则根据模板初始化节点实例
            if (this.DefineProduction.ModelDic.GetValue("ProductionType") == ProductionType.Progress.ToString())
            {
                if (isReinit)
                {
                    //如果需要重设进度模板，则需要删除未确认的节点和节点分组
                    this.DB.ExecuteNonQuery("delete from S_EP_ProductionUnit_ProgressNode where State!='Finish' and ProductionUnitID='" + this.ID + "'");
                    this.DB.ExecuteNonQuery(String.Format(@"delete from S_EP_ProductionUnit_Progress
where ID not in (select ProgressInfoID from S_EP_ProductionUnit_ProgressNode where ProductionUnitID='{0}') and ProductionUnitID='{0}'", this.ID));
                }
                //查询出已经存在的节点分组
                var existProgressInfoDt = this.DB.ExecuteDataTable(String.Format(@"select * from S_EP_ProductionUnit_Progress 
where ProductionUnitID='{0}'", this.ID));

                //获取已经存在的节点
                var existProgressNodeDt = this.DB.ExecuteDataTable(String.Format(@"select * from dbo.S_EP_ProductionUnit_ProgressNode
where ProductionUnitID='{0}'", this.ID));

                //获取产值单元定义信息
                var productionDetailDefineDt = this.InfrasDB.ExecuteDataTable(String.Format(@"select * from S_EP_DefineProduction_Detail with(nolock) 
                where DefineProductionID='{0}' order by sortindex ", this.DefineProduction.ID));

                //获取节点模板
                var progressDefineNodeDt = this.InfrasDB.ExecuteDataTable(String.Format(@"select S_EP_DefineProgress_ProgressNode.*
,S_EP_DefineProduction_Detail.ProgressDefineName, DefineProductionID 
from S_EP_DefineProduction_Detail inner join S_EP_DefineProgress_ProgressNode
on S_EP_DefineProduction_Detail.ProgressDefineID = S_EP_DefineProgress_ProgressNode.ProgressDefineID
where S_EP_DefineProduction_Detail.DefineProductionID='{0}' order by sortindex", this.DefineProduction.ID));
                foreach (DataRow productionDetailDefine in productionDetailDefineDt.Rows)
                {
                    if (productionDetailDefine["ProgressDefineID"] == null || productionDetailDefine["ProgressDefineID"] == DBNull.Value ||
                        String.IsNullOrEmpty(productionDetailDefine["ProgressDefineID"].ToString()))
                        continue;

                    var row = existProgressInfoDt.AsEnumerable().FirstOrDefault(c => c["DefineProgressID"] != DBNull.Value && c["DefineProgressID"].ToString() == productionDetailDefine["ProgressDefineID"].ToString());
                    if (row == null)
                    {
                        //如果产值单元没有节点分组,则新增
                        var dic = FormulaHelper.DataRowToDic(productionDetailDefine);
                        dic.SetValue("ID", FormulaHelper.CreateGuid());
                        dic.SetValue("Name", productionDetailDefine["ProgressDefineName"]);
                        dic.SetValue("ProductionUnitID", this.ID);
                        dic.SetValue("Scale", productionDetailDefine["Scale"]);
                        dic.SetValue("DefineProgressID", productionDetailDefine["ProgressDefineID"]);
                        dic.SetValue("SortIndex", productionDetailDefine["SortIndex"]);
                        dic.InsertDB(this.DB, "S_EP_ProductionUnit_Progress", dic.GetValue("ID"));
                        _initProgressNode(progressDefineNodeDt, existProgressNodeDt, productionDetailDefine, dic);
                    }
                    else
                    {
                        //如果已经有节点分组，则获取节点分组后直接初始化节点信息
                        var dic = FormulaHelper.DataRowToDic(row);
                        _initProgressNode(progressDefineNodeDt, existProgressNodeDt, productionDetailDefine, dic);
                    }
                }

                var nodesDt = this.DB.ExecuteDataTable(String.Format("select * from S_EP_ProductionUnit_ProgressNode with(nolock) where ProductionUnitID='{0}'", this.ID));
                var nodes = nodesDt.AsEnumerable().ToList();
                foreach (DataRow node in nodes)
                {
                    var totalScaleValue = nodes.Where(c => Convert.ToInt32(c["SortIndex"]) <= Convert.ToInt32(node["SortIndex"])).Sum(c => Convert.ToDecimal(c["AllScale"]));

                    this.DB.ExecuteNonQuery(String.Format("update S_EP_ProductionUnit_ProgressNode set TotalAllScale={1} where ID='{0}'", node["ID"], totalScaleValue));
                }
            } 
            #endregion
        }

        void _initProgressNode(DataTable defineNodeDt, DataTable currentProgressNodeDt, DataRow productionDetailDefine, Dictionary<string, object> progressInfo)
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
                    dic.SetValue("ProductionUnitID", this.ID);
                    dic.SetValue("ProgressInfoID", progressInfo.GetValue("ID"));
                    dic.SetValue("NodeName", defineNode["Name"]);
                    dic.SetValue("NodeCode", defineNode["Code"]);
                    dic.SetValue("Scale", defineNode["Scale"]);
                    dic.SetValue("TotalScale", defineNode["TotalScale"]);
                    var allScale = Convert.ToDecimal(defineNode["Scale"]) * Convert.ToDecimal(progressInfo.GetValue("Scale")) / 100;
                    dic.SetValue("AllScale", allScale);
                    dic.SetValue("TotalAllScale", 0);
                    dic.SetValue("ProgressNodeDefineID", defineNode["ID"]);
                    dic.SetValue("ProductionDefineDetailID", productionDetailDefine["ID"]);
                    var sortIndex = Convert.ToDecimal(progressInfo.GetValue("SortIndex")) * 10 + Convert.ToDecimal(defineNode["SortIndex"]);
                    dic.SetValue("SortIndex", sortIndex);
                    dic.SetValue("State", "Create");
                    dic.SetValue("Status", "Create");
                    dic.InsertDB(this.DB, "S_EP_ProductionUnit_ProgressNode", dic.GetValue("ID"));
                }
                else
                {

                }
            }
        }

    }

    public struct ProductionCondition
    {
        public string GroupName { get; set; }
        public bool Result { get; set; }
        public string FieldName { get; set; }
    }
}
