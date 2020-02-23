using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using Config;
using Config.Logic;
using Formula;
using Formula.Helper;
using System.Text.RegularExpressions;


namespace Expenses.Logic.Domain
{
    public partial class S_EP_CBSInfo : BaseEPModel
    {

        DataTable _defineCBSNodes;
        public DataTable DefineCBSNodes
        {
            get
            {
                if (_defineCBSNodes == null)
                {
                    _defineCBSNodes = this.InfrasDB.ExecuteDataTable(String.Format("SELECT * FROM S_EP_DefineCBSNode with(nolock) WHERE DEFINEID='{0}' ORDER BY FULLID",
                   this.ModelDic.GetValue("CBSDefineInfoID")));
                }
                return _defineCBSNodes;
            }
        }

        public S_EP_CBSInfo(Dictionary<string, object> dic)
        {
            if (dic == null)
                throw new Formula.Exceptions.BusinessValidationException("初始化构造S_EP_CBSInfo的键值对不能对空值");
            this.FillModel(dic);
        }

        public void Build(S_EP_DefineCBSInfo define)
        {
            if (String.IsNullOrEmpty(this.ModelDic.GetValue("ID")))
                this.ModelDic.SetValue("ID", FormulaHelper.CreateGuid());
            this.ModelDic.SetValue("Type", define.ModelDic.GetValue("Type"));
            this.ModelDic.SetValue("CBSDefineInfoID", define.ID);
            if (!String.IsNullOrEmpty(this.ModelDic.GetValue("CreateDate")))
            {
                var dateTime = Convert.ToDateTime(this.ModelDic.GetValue("CreateDate"));
                this.ModelDic.SetValue("BelongYear", dateTime.Year);
                this.ModelDic.SetValue("BelongSeason", ((dateTime.Month - 1) / 3 + 1));
                this.ModelDic.SetValue("BelongMonth", dateTime.Month);
            }
            if (String.IsNullOrEmpty(this.ModelDic.GetValue("State")))
            {
                this.ModelDic.SetValue("State", CommonState.Execute.ToString());
            }
            this.ModelDic.InsertDB(this.DB, "S_EP_CBSInfo", this.ModelDic.GetValue("ID"));
        }

        public void Build(string defineID)
        {
            var defineInfoDt = this.DB.ExecuteDataTable(String.Format("SELECT * FROM S_EP_DefineCBSInfo WHERE ID='{0}'", defineID));
            if (defineInfoDt.Rows.Count == 0)
            {
                throw new Formula.Exceptions.BusinessValidationException("没有找到指定的账套信息定义");
            }
            var defineDic = FormulaHelper.DataRowToDic(defineInfoDt.Rows[0]);
            var defineInfo = new S_EP_DefineCBSInfo(defineDic);
            this.Build(defineInfo);
        }

        public void SummaryBudgetValue()
        {

        }

        public void SummaryContractValue()
        {
            var detailCBSList = this.DB.ExecuteDataTable(
               String.Format("SELECT * FROM S_EP_CBSNode with(nolock) where CBSInfoID='{0}' and NodeType!='{1}' ORDER BY FullID",
               this.ID, CBSNodeType.Subject.ToString())).AsEnumerable();
            var parentIDs = detailCBSList.Where(c => c["ParentID"] != null && c["ParentID"] != DBNull.Value).Select(c => c["ParentID"].ToString()).Distinct().ToList();
            //获取所有叶子节点
            var leafNodes = detailCBSList.Where(c => !parentIDs.Contains(c["ID"].ToString())).ToList();
            var leafParentIDs = leafNodes.Where(c => c["ParentID"] != null && c["ParentID"] != DBNull.Value).Select(c => c["ParentID"].ToString()).ToList();
            var leafParents = detailCBSList.Where(c => leafParentIDs.Contains(c["ID"].ToString()));
            foreach (var leaf in leafParents)
            {
                var leafNode = new S_EP_CBSNode(FormulaHelper.DataRowToDic(leaf));
                leafNode.SumContractValueToTop();
            }
        }

        public void SummaryPlanProductionValue()
        {
            var detailCBSList = this.DB.ExecuteDataTable(
            String.Format("SELECT * FROM S_EP_CBSNode with(nolock) where CBSInfoID='{0}' and NodeType!='{1}' ORDER BY FullID",
            this.ID, CBSNodeType.Subject.ToString())).AsEnumerable();
            var parentIDs = detailCBSList.Where(c => c["ParentID"] != null && c["ParentID"] != DBNull.Value).Select(c => c["ParentID"].ToString()).Distinct().ToList();
            //获取所有叶子节点
            var leafNodes = detailCBSList.Where(c => !parentIDs.Contains(c["ID"].ToString())).ToList();
            var leafParentIDs = leafNodes.Where(c => c["ParentID"] != null && c["ParentID"] != DBNull.Value).Select(c => c["ParentID"].ToString()).ToList();
            var leafParents = detailCBSList.Where(c => leafParentIDs.Contains(c["ID"].ToString()));
            foreach (var leaf in leafParents)
            {
                var leafNode = new S_EP_CBSNode(FormulaHelper.DataRowToDic(leaf));
                leafNode.SumProductionValueToTop();
            }
        }

        public void SummaryCostValue()
        {
            var sql = @"update S_EP_CBSNode
set CostValue= (select isnull(Sum(TotalPrice),0) from S_EP_CostInfo
where S_EP_CostInfo.CBSNodeFullID like S_EP_CBSNode.FullID+'%'),
CostTaxValue= (select isnull(Sum(TaxValue),0) from S_EP_CostInfo
where S_EP_CostInfo.CBSNodeFullID like S_EP_CBSNode.FullID+'%'),
CostClearValue= (select isnull(Sum(ClearValue),0) from S_EP_CostInfo
where S_EP_CostInfo.CBSNodeFullID like S_EP_CBSNode.FullID+'%')
where CBSInfoID='{0}'";
            var cbsInfoSql = @"update S_EP_CBSInfo
set CostValue= (select isnull(Sum(TotalPrice),0) from S_EP_CostInfo
where S_EP_CostInfo.CBSInfoID = S_EP_CBSInfo.ID),
CostTaxValue= (select isnull(Sum(TaxValue),0) from S_EP_CostInfo
where S_EP_CostInfo.CBSInfoID = S_EP_CBSInfo.ID),
CostClearValue= (select isnull(Sum(ClearValue),0) from S_EP_CostInfo
where S_EP_CostInfo.CBSInfoID = S_EP_CBSInfo.ID)
where ID='{0}'";

            this.DB.ExecuteNonQuery(String.Format(sql, this.ID));
            this.DB.ExecuteNonQuery(String.Format(cbsInfoSql, this.ID));


            var costUnitIDs = this.DB.ExecuteDataTable("SELECT ID FROM S_EP_CostUnit WHERE CBSInfoID='" + this.ID + "'").
                AsEnumerable().Select(c => c["ID"].ToString()).ToList();

            //汇总成本单元上的总成本数据
            foreach (var unitID in costUnitIDs)
            {
                this.DB.ExecuteNonQuery(String.Format(@"update S_EP_CostUnit 
set TotalCostValue = (select isnull(Sum(TotalPrice),0) from S_EP_CostInfo with(nolock) where S_EP_CostInfo.CostUnitID=S_EP_CostUnit.ID),
CostTaxValue = (select isnull(Sum(TaxValue),0) from S_EP_CostInfo with(nolock) where S_EP_CostInfo.CostUnitID=S_EP_CostUnit.ID),
CostClearValue = (select isnull(Sum(ClearValue),0) from S_EP_CostInfo with(nolock) where S_EP_CostInfo.CostUnitID=S_EP_CostUnit.ID),
 DirectCostValue = (select isnull(Sum(TotalPrice),0) from S_EP_CostInfo with(nolock) where S_EP_CostInfo.CostUnitID=S_EP_CostUnit.ID and CostType='{1}'),
 HRCostValue = (select isnull(Sum(TotalPrice),0) from S_EP_CostInfo with(nolock) where S_EP_CostInfo.CostUnitID=S_EP_CostUnit.ID and CostType='{2}'),
 InDirectCostValue = (select isnull(Sum(TotalPrice),0) from S_EP_CostInfo with(nolock) where S_EP_CostInfo.CostUnitID=S_EP_CostUnit.ID and CostType='{3}'),
 SubContractCostValue = (select isnull(Sum(TotalPrice),0) from S_EP_CostInfo with(nolock) where S_EP_CostInfo.CostUnitID=S_EP_CostUnit.ID and CostType='{4}')
where ID='{0}'", unitID, SubjectType.DirectCost.ToString(),
               SubjectType.HRCost.ToString(), SubjectType.InDirectCost.ToString(),
               SubjectType.SubContractCost.ToString()));
            }
        }

        /// <summary>
        /// 根据操作过滤来同步更新节点信息
        /// </summary>
        /// <param name="opportunity"></param>
        /// <param name="paramDic"></param>
        public void SynNodeData(SetCBSOpportunity opportunity, Dictionary<string, object> paramDic = null)
        {
            var cbsNodeDt = this.DB.ExecuteDataTable(String.Format("SELECT * FROM S_EP_CBSNode WHERE CBSInfoID='{0}'", this.ID));
            var defineNodesList = this.DefineCBSNodes.AsEnumerable().ToList();
            var defineNodes = defineNodesList.Where(c => c["SynData"] != DBNull.Value && !String.IsNullOrEmpty(c["SynData"].ToString())
                && c["IsDynamic"] != DBNull.Value && c["IsDynamic"].ToString() == "true"
                 && c["SynData"].ToString().Contains(opportunity.ToString())).ToList();
            foreach (var defineNode in defineNodes)
            {
                this.SynNodeWithDefineNode(opportunity, defineNode, cbsNodeDt, defineNodesList, paramDic);
            }
        }


        public void SynNodeWithDefineNode(SetCBSOpportunity opportunity, DataRow defineNode, DataTable cbsNodeDt,
           List<DataRow> defineNodesList, Dictionary<string, object> paramDic)
        {
            DataRow parentDefineNode = null;
            List<DataRow> parentCBSNodes = null;
            if (defineNode["DynamicSettings"] == DBNull.Value || String.IsNullOrEmpty(defineNode["DynamicSettings"].ToString())) return;
            var dynamicSettings = JsonHelper.ToList(defineNode["DynamicSettings"].ToString()).Where(c => c.GetValue("SynDataMethod") == opportunity.ToString()).ToList();
            foreach (var dynamicSetting in dynamicSettings)
            {
                var settings = JsonHelper.ToObject(dynamicSetting.GetValue("SynDataMethodSettings"));
                var dataSourceInfo = JsonHelper.ToList(settings.GetValue("DataSourceInfo"));
                if (defineNode["ParentID"] != DBNull.Value && !String.IsNullOrEmpty(defineNode["ParentID"].ToString()))
                {
                    parentDefineNode = defineNodesList.FirstOrDefault(c => c["ID"].ToString() == defineNode["ParentID"].ToString());
                }
                if (parentDefineNode != null)
                {
                    var nodeQuery = cbsNodeDt.AsEnumerable().Where(c => c["DefineID"] != DBNull.Value
                        && c["DefineID"].ToString() == parentDefineNode["ID"].ToString());
                    if (!String.IsNullOrEmpty(settings.GetValue("ParentFilter")))
                    {
                        var parentFilters = JsonHelper.ToList(settings.GetValue("ParentFilter"));
                        foreach (var filter in parentFilters)
                        {
                            #region 父节点过滤
                            var field = filter.GetValue("Field");
                            var queryMode = filter.GetValue("QueryMode");
                            var fieldValue = filter.GetValue("Value");
                            if (String.IsNullOrEmpty(field) || String.IsNullOrEmpty(queryMode) || String.IsNullOrEmpty(fieldValue))
                            {
                                continue;
                            }
                            string value = "";
                            if (fieldValue.StartsWith("{"))
                            {
                                var valArr = fieldValue.Trim('{', '}').Split('.');
                                if (valArr.Length < 2) continue;
                                var fieldVal = "{" + valArr[1] + "}";
                                value = FormulaHelper.CreateFO<StringFuncFo>().ReplaceString(fieldVal, null, paramDic);
                            }
                            else
                            {
                                value = fieldValue;
                            }
                            switch (queryMode)
                            {
                                case "In":
                                    nodeQuery = nodeQuery.Where(c => c[filter.GetValue("Field")] != DBNull.Value &&
                                        value.Contains(c[filter.GetValue("Field")].ToString()));
                                    break;
                                case "Like":
                                    nodeQuery = nodeQuery.Where(c => c[filter.GetValue("Field")] != DBNull.Value &&
                                      c[filter.GetValue("Field")].ToString().Contains(value));
                                    break;
                                case "GreaterThanOrEqual":
                                    if (Regex.IsMatch(value, "^[0-9]*$"))
                                    {
                                        nodeQuery = nodeQuery.Where(c => c[filter.GetValue("Field")] != DBNull.Value &&
                                 Convert.ToDecimal(c[filter.GetValue("Field")]) >= Convert.ToDecimal(value));
                                    }
                                    break;
                                case "LessThanOrEqual":
                                    if (Regex.IsMatch(value, "^[0-9]*$"))
                                    {
                                        nodeQuery = nodeQuery.Where(c => c[filter.GetValue("Field")] != DBNull.Value &&
                                 Convert.ToDecimal(c[filter.GetValue("Field")]) <= Convert.ToDecimal(value));
                                    }
                                    break;
                                default:
                                case "Equal":
                                    nodeQuery = nodeQuery.Where(c => c[filter.GetValue("Field")] != DBNull.Value &&
                                      c[filter.GetValue("Field")].ToString() == value);
                                    break;
                                case "LessThan":
                                    if (Regex.IsMatch(value, "^[0-9]*$"))
                                    {
                                        nodeQuery = nodeQuery.Where(c => c[filter.GetValue("Field")] != DBNull.Value &&
                                 Convert.ToDecimal(c[filter.GetValue("Field")]) < Convert.ToDecimal(value));
                                    }
                                    break;
                                case "GreaterThan":
                                    if (Regex.IsMatch(value, "^[0-9]*$"))
                                    {
                                        nodeQuery = nodeQuery.Where(c => c[filter.GetValue("Field")] != DBNull.Value &&
                                 Convert.ToDecimal(c[filter.GetValue("Field")]) > Convert.ToDecimal(value));
                                    }
                                    break;
                            }
                            #endregion
                        }
                    }
                    parentCBSNodes = nodeQuery.ToList();
                    foreach (var parentNodeRow in parentCBSNodes)
                    {
                        var dataSourceValueDic = new Dictionary<string, object>();
                        var mainDataSourceName = ""; DataTable mainDataSourceDt = null;
                        foreach (var dataSource in dataSourceInfo)
                        {
                            _setDataSourceDic(dataSourceValueDic, dataSource, parentNodeRow, paramDic);
                            //判定主数据源是否有，有的话后续要循环主数据源来新增CBS节点
                            if (dataSource.GetValue("SourceType") == "Main" && dataSourceValueDic.ContainsKey(dataSource.GetValue("Code")) &&
                                dataSourceValueDic[dataSource.GetValue("Code")] != null)
                            {
                                mainDataSourceName = dataSource.GetValue("Code");
                                mainDataSourceDt = dataSourceValueDic[mainDataSourceName] as DataTable;
                            }
                        }
                        var autoDataAdapter = JsonHelper.ToList(settings.GetValue("AutoDataAdapter"));
                        if (mainDataSourceDt == null)
                        {
                            var nodeDic = _synNode(settings, autoDataAdapter, dataSourceValueDic, defineNode, cbsNodeDt, parentNodeRow, paramDic);
                            if (nodeDic != null)
                                setUnit(nodeDic, null, defineNode);
                        }
                        else
                        {
                            //此处判定是否具有主数据源，如果有，则说明动态节点需要根据主数据源数据循环创建CBS节点
                            //否则就只建1个CBS节点
                            foreach (DataRow mainRow in mainDataSourceDt.Rows)
                            {
                                var nodeDic = this._synNodeFormMainSource(settings, autoDataAdapter, mainRow, mainDataSourceName, dataSourceValueDic, defineNode,
                                    cbsNodeDt, parentNodeRow, paramDic);
                                if (nodeDic != null)
                                    setUnit(nodeDic, mainRow, defineNode);
                            }
                        }
                    }
                }
                else
                {
                    //表示当前节点是根节点，因为没有上级节点
                    var dataSourceValueDic = new Dictionary<string, object>();
                    var mainDataSourceName = ""; DataTable mainDataSourceDt = null;
                    foreach (var dataSource in dataSourceInfo)
                    {
                        _setDataSourceDic(dataSourceValueDic, dataSource, null, paramDic);
                        if (dataSource.GetValue("SourceType") == "Main" && dataSourceValueDic.ContainsKey(dataSource.GetValue("Code")) &&
                               dataSourceValueDic[dataSource.GetValue("Code")] != null)
                        {
                            mainDataSourceName = dataSource.GetValue("Code");
                            mainDataSourceDt = dataSourceValueDic[mainDataSourceName] as DataTable;
                        }
                    }
                    var autoDataAdapter = JsonHelper.ToList(settings.GetValue("AutoDataAdapter"));
                    if (mainDataSourceDt == null)
                    {
                        var nodeDic = _synNode(settings, autoDataAdapter, dataSourceValueDic, defineNode, cbsNodeDt, null, paramDic);
                        if (nodeDic != null)
                            setUnit(nodeDic, null, defineNode);
                    }
                    else
                    {
                        foreach (DataRow mainRow in mainDataSourceDt.Rows)
                        {
                            var nodeDic = this._synNodeFormMainSource(settings, autoDataAdapter, mainRow, mainDataSourceName, dataSourceValueDic, defineNode,
                                cbsNodeDt, null, paramDic);
                            if (nodeDic != null)
                                setUnit(nodeDic, mainRow, defineNode);
                        }
                    }
                }
            }
        }

        public void setUnit(Dictionary<string, object> node, DataRow sourceRow, DataRow defNode, bool resetProgressNode = true)
        {
            if (defNode["BudgetUnit"] != null && defNode["BudgetUnit"] != DBNull.Value &&
                defNode["BudgetUnit"].ToString() == true.ToString().ToLower())
            {
                //设置预算单元
                var unitDic = _setUnit(node, sourceRow, defNode, "S_EP_BudgetUnit", false);
                var budgetUnit = new S_EP_BudgetUnit(unitDic);
                budgetUnit.SetBudgetDefineID();
            }
            if (defNode["CostUnit"] != null && defNode["CostUnit"] != DBNull.Value &&
                defNode["CostUnit"].ToString() == true.ToString().ToLower())
            {
                //设置成本单元
                _setUnit(node, sourceRow, defNode, "S_EP_CostUnit", false);
            }
            if (defNode["IncomeUnit"] != null && defNode["IncomeUnit"] != DBNull.Value &&
              defNode["IncomeUnit"].ToString() == true.ToString().ToLower())
            {
                //设置收入单元
                var unitDic = _setUnit(node, sourceRow, defNode, "S_EP_IncomeUnit", false);
                if (resetProgressNode)
                {
                    var incomeUnit = new S_EP_IncomeUnit(unitDic);
                    incomeUnit.SetIncomeDefineID();
                    incomeUnit.ReInitProgress(true);
                }
            }
            if (defNode["ProductionUnit"] != null && defNode["ProductionUnit"] != DBNull.Value &&
            defNode["ProductionUnit"].ToString() == true.ToString().ToLower())
            {
                //设置产值单元
                var unitDic = _setUnit(node, sourceRow, defNode, "S_EP_ProductionUnit", false);
                if (resetProgressNode)
                {
                    var productionUnit = new S_EP_ProductionUnit(unitDic);
                    productionUnit.SetProductionDefineID();
                    productionUnit.ReInitProgress(true);
                }
            }
        }


        #region 私有方法
        Dictionary<string, object> _synNodeFormMainSource(Dictionary<string, object> setting, List<Dictionary<string, object>> autoDataAdapter, DataRow mainRow, string mainDataSourceName, Dictionary<string, object> dataSourceValueDic,
            DataRow DefineNode, DataTable cbsNodeDt, DataRow parentCBSNodeRow = null, Dictionary<string, object> ParamDic = null)
        {
            var relateIDAdapter = autoDataAdapter.FirstOrDefault(c => c.GetValue("Field") == "RelateID");
            if (relateIDAdapter == null) throw new Formula.Exceptions.BusinessValidationException("节点必须指定RelateID业务关联ID 字段的值");
            var relateID = _getAdapterValue(relateIDAdapter, dataSourceValueDic, mainRow, mainDataSourceName, parentCBSNodeRow, ParamDic);
            if (String.IsNullOrEmpty(relateID)) throw new Formula.Exceptions.BusinessValidationException("绑定关联数据ID不能为空值，无法自动获取数据创建节点");
            var cbsNodeRow = cbsNodeDt.AsEnumerable().FirstOrDefault(c => c["RelateID"] != DBNull.Value && c["RelateID"].ToString() == relateID);
            var cbsNodeDic = new Dictionary<string, object>();
            cbsNodeDic.SetValue("RelateID", relateID);
            bool addNew = true;
            if (cbsNodeRow != null)
            {
                cbsNodeDic = FormulaHelper.DataRowToDic(cbsNodeRow);
                addNew = false;
            }
            foreach (var adapter in autoDataAdapter)
            {
                if (String.IsNullOrEmpty(adapter.GetValue("Field"))) continue;
                if (adapter.GetValue("Field") == "RelateID") continue;
                var value = _getAdapterValue(adapter, dataSourceValueDic, mainRow, mainDataSourceName, parentCBSNodeRow, ParamDic);
                //判定如果数据源中没有找到该默认值的对应数据（对应数据源前缀的数据为空，或者数据源的DataTable中没有指定的字段列）
                //则不做任何操作
                if (value.ToLower() == "nonedata") continue;
                cbsNodeDic.SetValue(adapter.GetValue("Field"), value);
            }
            cbsNodeDic.SetValue("DefineID", DefineNode["ID"]);
            cbsNodeDic.SetValue("NodeType", DefineNode["NodeType"]);
            cbsNodeDic.SetValue("CBSType", DefineNode["CBSType"]);
            cbsNodeDic.SetValue("AccountNodeType", DefineNode["AccountNodeType"]);
            if (addNew)
            {
                if (setting.GetValue("OnlyUpdate") == true.ToString().ToLower())
                {
                    return null;
                }
                #region 新增节点
                if (parentCBSNodeRow == null)
                {
                    _createRootCBS(cbsNodeDic, DefineNode);
                }
                else
                {
                    var parentCBSNode = new S_EP_CBSNode(FormulaHelper.DataRowToDic(parentCBSNodeRow));
                    parentCBSNode.AddChild(cbsNodeDic);
                }
                var row = cbsNodeDt.NewRow();
                foreach (var key in cbsNodeDic.Keys)
                {
                    if (cbsNodeDt.Columns.Contains(key))
                    {
                        if (cbsNodeDt.Columns[key].DataType == typeof(decimal))
                        {
                            row[key] = String.IsNullOrEmpty(cbsNodeDic.GetValue(key)) ? 0m : Convert.ToDecimal(cbsNodeDic.GetValue(key));
                        }
                        else if (cbsNodeDt.Columns[key].DataType == typeof(double) || cbsNodeDt.Columns[key].DataType == typeof(float))
                        {
                            row[key] = String.IsNullOrEmpty(cbsNodeDic.GetValue(key)) ? 0 : Convert.ToDouble(cbsNodeDic.GetValue(key));
                        }
                        else if (cbsNodeDt.Columns[key].DataType == typeof(int))
                        {
                            row[key] = String.IsNullOrEmpty(cbsNodeDic.GetValue(key)) ? 0 : Convert.ToInt32(cbsNodeDic.GetValue(key));
                        }
                        else
                        {
                            row[key] = cbsNodeDic[key];
                        }
                    }
                }
                cbsNodeDt.Rows.Add(row);

                var staticDefineNodeList = this.DefineCBSNodes.AsEnumerable().Where(c => c["FullID"] != DBNull.Value
                     && c["FullID"].ToString().StartsWith(DefineNode["FullID"].ToString()) && c["IsDynamic"].ToString() != "true").
                     OrderBy(c => c["FullID"].ToString()).ToList();
                foreach (var staticDefineNodeRow in staticDefineNodeList)
                {
                    var parentCBSNodeRows = cbsNodeDt.AsEnumerable().Where(c => c["DefineID"] != DBNull.Value &&
                        c["DefineID"].ToString() == staticDefineNodeRow["ParentID"].ToString()).ToList();
                    foreach (var parentNodeRow in parentCBSNodeRows)
                    {
                        var staticNode = cbsNodeDt.AsEnumerable().FirstOrDefault(c => c["ParentID"] != DBNull.Value &&
                              c["ParentID"].ToString() == parentNodeRow["ID"].ToString() && c["DefineID"] != DBNull.Value &&
                              c["DefineID"].ToString() == staticDefineNodeRow["ID"].ToString());
                        if (staticNode == null)
                        {
                            var staticDefineNodeDic = FormulaHelper.DataRowToDic(staticDefineNodeRow);
                            if (!String.IsNullOrEmpty(staticDefineNodeDic.GetValue("DynamicSettings")))
                            {
                                var defaultValueSettings = JsonHelper.ToObject(staticDefineNodeDic.GetValue("DynamicSettings"));
                                if (!String.IsNullOrEmpty(defaultValueSettings.GetValue("AutoDataAdapter")))
                                {
                                    var dataSourceList = new List<Dictionary<string, object>>();
                                    if (!String.IsNullOrEmpty(defaultValueSettings.GetValue("DataSourceInfo")))
                                        dataSourceList = JsonHelper.ToList(defaultValueSettings.GetValue("DataSourceInfo"));
                                    var dataSource = new Dictionary<string, object>();
                                    foreach (var dataSourceDefine in dataSourceList)
                                    {
                                        this._setDataSourceDic(dataSource, dataSourceDefine, parentNodeRow, ParamDic);
                                    }
                                    var dataAdpList = JsonHelper.ToList(defaultValueSettings.GetValue("AutoDataAdapter"));
                                    foreach (var defaultAdp in dataAdpList)
                                    {
                                        var value = _getAdapterValue(defaultAdp, dataSource, null, "", parentCBSNodeRow, ParamDic);
                                        if (value.ToLower() == "nonedata") continue;
                                        staticDefineNodeDic.SetValue(defaultAdp.GetValue("Field"), value);
                                    }
                                }
                            }
                            var staticCBSNodeDic = this._addCBSNode(FormulaHelper.DataRowToDic(parentNodeRow), staticDefineNodeDic, staticDefineNodeRow);
                            var staticRow = cbsNodeDt.NewRow();
                            foreach (var key in staticCBSNodeDic.Keys)
                            {
                                if (cbsNodeDt.Columns.Contains(key))
                                {
                                    staticRow[key] = staticCBSNodeDic[key];
                                }
                            }
                            cbsNodeDt.Rows.Add(staticRow);
                        }
                    }
                }
                #endregion
            }
            else
            {
                cbsNodeDic.UpdateDB(this.DB, "S_EP_CBSNode", cbsNodeDic.GetValue("ID"));
            }
            return cbsNodeDic;
        }

        Dictionary<string, object> _synNode(Dictionary<string, object> setting, List<Dictionary<string, object>> autoDataAdapter, Dictionary<string, object> dataSourceValueDic, DataRow DefineNode, DataTable cbsNodeDt,
            DataRow parentCBSNodeRow = null, Dictionary<string, object> ParamDic = null)
        {
            var relateIDAdapter = autoDataAdapter.FirstOrDefault(c => c.GetValue("Field") == "RelateID");
            if (relateIDAdapter == null) throw new Formula.Exceptions.BusinessValidationException(String.Format("【{0}】节点必须指定RelateID业务关联ID 字段的值",DefineNode["Name"]));
            var relateID = _getAdapterValue(relateIDAdapter, dataSourceValueDic, null, "", parentCBSNodeRow, ParamDic);
            if (String.IsNullOrEmpty(relateID)) throw new Formula.Exceptions.BusinessValidationException("绑定关联数据ID不能为空值，无法自动获取数据创建节点");
            var cbsNodeRow = cbsNodeDt.AsEnumerable().FirstOrDefault(c => c["RelateID"] != DBNull.Value && c["RelateID"].ToString() == relateID
                && c["DefineID"] != DBNull.Value && c["DefineID"].ToString() == DefineNode["ID"].ToString());
            var cbsNodeDic = new Dictionary<string, object>();
            cbsNodeDic.SetValue("RelateID", relateID);
            bool addNew = true;
            if (cbsNodeRow != null)
            {
                cbsNodeDic = FormulaHelper.DataRowToDic(cbsNodeRow);
                addNew = false;
            }
            foreach (var adapter in autoDataAdapter)
            {
                if (String.IsNullOrEmpty(adapter.GetValue("Field"))) continue;
                if (adapter.GetValue("Field") == "RelateID") continue;
                var value = _getAdapterValue(adapter, dataSourceValueDic, null, "", parentCBSNodeRow, ParamDic);
                //判定如果数据源中没有找到该默认值的对应数据（对应数据源前缀的数据未空，或者数据源的DataTable中没有指定的字段列）
                //则不做任何操作
                if (value.ToLower() == "nonedata") continue;
                cbsNodeDic.SetValue(adapter.GetValue("Field"), value);
            }
            cbsNodeDic.SetValue("DefineID", DefineNode["ID"]);
            cbsNodeDic.SetValue("NodeType", DefineNode["NodeType"]);
            cbsNodeDic.SetValue("CBSType", DefineNode["CBSType"]);
            cbsNodeDic.SetValue("AccountNodeType", DefineNode["AccountNodeType"]);
            if (addNew)
            {
                if (setting.GetValue("OnlyUpdate") == true.ToString().ToLower())
                {
                    return null;
                }
                #region 新增节点
                if (parentCBSNodeRow == null)
                {
                    _createRootCBS(cbsNodeDic, DefineNode);
                }
                else
                {
                    var parentCBSNode = new S_EP_CBSNode(FormulaHelper.DataRowToDic(parentCBSNodeRow));
                    parentCBSNode.AddChild(cbsNodeDic);
                }
                var row = cbsNodeDt.NewRow();
                foreach (var key in cbsNodeDic.Keys)
                {
                    if (cbsNodeDt.Columns.Contains(key))
                    {
                        if (cbsNodeDt.Columns[key].DataType == typeof(decimal))
                        {
                            row[key] = String.IsNullOrEmpty(cbsNodeDic.GetValue(key)) ? 0m : Convert.ToDecimal(cbsNodeDic.GetValue(key));
                        }
                        else if (cbsNodeDt.Columns[key].DataType == typeof(double) || cbsNodeDt.Columns[key].DataType == typeof(float))
                        {
                            row[key] = String.IsNullOrEmpty(cbsNodeDic.GetValue(key)) ? 0 : Convert.ToDouble(cbsNodeDic.GetValue(key));
                        }
                        else if (cbsNodeDt.Columns[key].DataType == typeof(int))
                        {
                            row[key] = String.IsNullOrEmpty(cbsNodeDic.GetValue(key)) ? 0 : Convert.ToInt32(cbsNodeDic.GetValue(key));
                        }
                        else
                        {
                            row[key] = cbsNodeDic[key];
                        }
                    }
                }
                cbsNodeDt.Rows.Add(row);

                var staticDefineNodeList = this.DefineCBSNodes.AsEnumerable().Where(c => c["FullID"] != DBNull.Value
                     && c["FullID"].ToString().StartsWith(DefineNode["FullID"].ToString()) && c["IsDynamic"].ToString() != "true").
                     OrderBy(c => c["FullID"].ToString()).ToList();
                foreach (var staticDefineNodeRow in staticDefineNodeList)
                {
                    var parentCBSNodeRows = cbsNodeDt.AsEnumerable().Where(c => c["DefineID"] != DBNull.Value &&
                        c["DefineID"].ToString() == staticDefineNodeRow["ParentID"].ToString()).ToList();
                    foreach (var parentNodeRow in parentCBSNodeRows)
                    {
                        var staticNode = cbsNodeDt.AsEnumerable().FirstOrDefault(c => c["ParentID"] != DBNull.Value &&
                              c["ParentID"].ToString() == parentNodeRow["ID"].ToString() && c["DefineID"] != DBNull.Value &&
                              c["DefineID"].ToString() == staticDefineNodeRow["ID"].ToString());
                        if (staticNode == null)
                        {
                            var staticDefineNodeDic = FormulaHelper.DataRowToDic(staticDefineNodeRow);
                            if (!String.IsNullOrEmpty(staticDefineNodeDic.GetValue("DynamicSettings")))
                            {
                                var defaultValueSettings = JsonHelper.ToObject(staticDefineNodeDic.GetValue("DynamicSettings"));
                                if (!String.IsNullOrEmpty(defaultValueSettings.GetValue("AutoDataAdapter")))
                                {
                                    var dataSourceList = new List<Dictionary<string, object>>();
                                    if (!String.IsNullOrEmpty(defaultValueSettings.GetValue("DataSourceInfo")))
                                        dataSourceList = JsonHelper.ToList(defaultValueSettings.GetValue("DataSourceInfo"));
                                    var dataSource = new Dictionary<string, object>();
                                    foreach (var dataSourceDefine in dataSourceList)
                                    {
                                        this._setDataSourceDic(dataSource, dataSourceDefine, parentNodeRow, ParamDic);
                                    }
                                    var dataAdpList = JsonHelper.ToList(defaultValueSettings.GetValue("AutoDataAdapter"));
                                    foreach (var defaultAdp in dataAdpList)
                                    {
                                        var value = _getAdapterValue(defaultAdp, dataSource, null, "", parentCBSNodeRow, ParamDic);
                                        if (value.ToLower() == "nonedata") continue;
                                        staticDefineNodeDic.SetValue(defaultAdp.GetValue("Field"), value);
                                    }
                                }
                            }
                            var staticCBSNodeDic = this._addCBSNode(FormulaHelper.DataRowToDic(parentNodeRow), staticDefineNodeDic, staticDefineNodeRow);
                            var staticRow = cbsNodeDt.NewRow();
                            foreach (var key in staticCBSNodeDic.Keys)
                            {
                                if (cbsNodeDt.Columns.Contains(key))
                                {
                                    staticRow[key] = staticCBSNodeDic[key];
                                }
                            }
                            cbsNodeDt.Rows.Add(staticRow);
                        }
                    }
                }
                #endregion
            }
            else
            {
                cbsNodeDic.UpdateDB(this.DB, "S_EP_CBSNode", cbsNodeDic.GetValue("ID"));
            }
            return cbsNodeDic;
        }

        string _getAdapterValue(Dictionary<string, object> adapterDic, Dictionary<string, object> dataSourceDic, DataRow forceRow = null, string mainSourceName = "",
            DataRow parentCBSNode = null, Dictionary<string, object> ParamDic = null)
        {
            if (forceRow != null && String.IsNullOrEmpty(mainSourceName))
                throw new Formula.Exceptions.BusinessValidationException("强制根据主要数据源获取默认值时，必须指定主要数据源的数据源名称");
            var formatExpress = adapterDic.GetValue("DefaultValue");
            if (String.IsNullOrEmpty(formatExpress)) return "";
            Regex reg = new Regex("\\{[0-9a-zA-Z_\\.]*\\}");
            var result = reg.Replace(formatExpress, (Match m) =>
            {
                #region 处理定义配置信息
                var value = m.Value.Trim('{', '}');
                var fields = value.Split('.');
                if (fields.Length == 1)
                {
                    if (forceRow == null)
                    {
                        if (ParamDic != null)
                            return ParamDic.GetValue(fields[0]);
                        else
                            return this.ModelDic.GetValue(fields[0]);
                    }
                    else if (forceRow[fields[0]] == null || forceRow[fields[0]] == DBNull.Value)
                        return "";
                    else
                        return forceRow[fields[0]].ToString();
                }
                else
                {
                    if (fields[0].ToLower() == "cbsinfo")
                    {
                        return this.ModelDic.GetValue(fields[1]);
                    }
                    else if (fields[0].ToLower() == "param" && ParamDic != null)
                    {
                        return ParamDic.GetValue(fields[1]);
                    }
                    else if (fields[0].ToLower() == "parent")
                    {
                        if (parentCBSNode == null) return "";
                        if (!parentCBSNode.Table.Columns.Contains(fields[0])) return "";
                        if (parentCBSNode[fields[1]] == null || parentCBSNode[fields[1]] == DBNull.Value) return "";
                        return parentCBSNode[fields[1]].ToString();
                    }
                    else
                    {
                        if (forceRow != null)
                        {
                            if (fields[0] == mainSourceName)
                            {
                                if (forceRow.Table.Columns.Contains(fields[1]))
                                {
                                    if (forceRow[fields[1]] == null || forceRow[fields[1]] == DBNull.Value)
                                        return "";
                                    else
                                        return forceRow[fields[1]].ToString();
                                }
                                else
                                {
                                    return "";
                                }
                            }
                        }
                        if (dataSourceDic.ContainsKey(fields[0]))
                        {
                            if (dataSourceDic[fields[0]] == null)
                                return "";
                            var sourceDt = dataSourceDic[fields[0]] as DataTable;
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
            return result;
        }

        void _setDataSourceDic(Dictionary<string, object> dataSourceDic, Dictionary<string, object> dataSourceDefine,
            DataRow parentCBSNode = null, Dictionary<string, object> ParamDic = null)
        {
            if (String.IsNullOrEmpty(dataSourceDefine.GetValue("Code"))) return;
            if (String.IsNullOrEmpty(dataSourceDefine.GetValue("ConnName"))) return;
            if (String.IsNullOrEmpty(dataSourceDefine.GetValue("SQL"))) return;
            var db = SQLHelper.CreateSqlHelper(dataSourceDefine.GetValue("ConnName"));
            var sql = dataSourceDefine.GetValue("SQL");
            Regex reg = new Regex("\\{[0-9a-zA-Z_\\.]*\\}");
            sql = reg.Replace(sql, (Match m) =>
            {
                #region 处理定义配置信息
                var value = m.Value.Trim('{', '}');
                var fields = value.Split('.');
                if (fields.Length == 1)
                {
                    return this.ModelDic.GetValue(fields[0]);
                }
                else
                {
                    if (fields[0].ToLower() == "cbsinfo")
                    {
                        return this.ModelDic.GetValue(fields[1]);
                    }
                    else if (fields[0].ToLower() == "param" && ParamDic != null)
                    {
                        return ParamDic.GetValue(fields[1]);
                    }
                    else if (fields[0].ToLower() == "parent")
                    {
                        if (parentCBSNode == null) return "";
                        if (!parentCBSNode.Table.Columns.Contains(fields[1])) return "";
                        if (parentCBSNode[fields[1]] == null || parentCBSNode[fields[1]] == DBNull.Value) return "";
                        return parentCBSNode[fields[1]].ToString();
                    }
                    else
                    {
                        if (dataSourceDic.ContainsKey(fields[0]))
                        {
                            if (dataSourceDic[fields[0]] == null)
                                return "";
                            var sourceDt = dataSourceDic[fields[0]] as DataTable;
                            if (sourceDt.Rows.Count == 0) return "";
                            if (!sourceDt.Columns.Contains(fields[1])) return "";
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
            var dt = db.ExecuteDataTable(sql);
            dataSourceDic.SetValue(dataSourceDefine.GetValue("Code"), dt);
        }

        Dictionary<string, object> _createRootCBS(Dictionary<string, object> root, DataRow defNode)
        {
            root.SetValue("ID", FormulaHelper.CreateGuid());
            root.SetValue("ParentID", "Root");
            root.SetValue("FullID", root.GetValue("ID"));
            root.SetValue("FullCode", this.ModelDic.GetValue("Code"));
            root.SetValue("DefineID", defNode["ID"]);
            root.SetValue("NodeType", defNode["NodeType"]);
            if (root.GetValue("NodeType") == CBSNodeType.Subject.ToString())
            {
                root.SetValue("SubjectID", defNode["SubjectID"]);
                root.SetValue("Code", defNode["SubjectCode"]);
                root.SetValue("SubjectFullCode", defNode["SubjectFullCode"]);
            }
            root.SetValue("CBSType", this.ModelDic.GetValue("Type"));

            root.SetValue("ChargerUser", this.ModelDic.GetValue("ChargerUser"));
            root.SetValue("ChargerUserName", this.ModelDic.GetValue("ChargerUserName"));
            root.SetValue("ChargerDept", this.ModelDic.GetValue("ChargerDept"));
            root.SetValue("ChargerDeptName", this.ModelDic.GetValue("ChargerDeptName"));

            root.SetValue("CBSInfoID", this.ModelDic.GetValue("ID"));
            root.SetValue("ContractInfoID", this.ModelDic.GetValue("ContractInfoID"));
            root.SetValue("ProjectInfoID", this.ModelDic.GetValue("ProjectInfoID"));
            root.SetValue("EngineeringInfoID", this.ModelDic.GetValue("EngineeringInfoID"));
            root.SetValue("ClueInfoID", this.ModelDic.GetValue("ClueInfoID"));
            var maxSortIndex = Convert.ToInt32(this.DB.ExecuteScalar("SELECT ISNULL(MAX(SortIndex),0) FROM S_EP_CBSNode WITH(NOLOCK) WHERE PARENTID='' OR PARENTID IS NULL"));
            var sortIndex = maxSortIndex + 1;
            root.SetValue("SortIndex", sortIndex);
            root.InsertDB(this.DB, "S_EP_CBSNode", root.GetValue("ID"));
            return root;
        }

        Dictionary<string, object> _addCBSNode(Dictionary<string, object> parent, Dictionary<string, object> node, DataRow defNode)
        {
            var parentCBS = new S_EP_CBSNode(parent);
            node.SetValue("ID", FormulaHelper.CreateGuid());
            node.SetValue("DefineID", defNode["ID"]);
            node.SetValue("NodeType", defNode["NodeType"]);
            if (node.GetValue("NodeType") == CBSNodeType.Subject.ToString())
            {
                node.SetValue("SubjectID", defNode["SubjectID"]);
                node.SetValue("SubjectType", defNode["SubjectType"]);
                node.SetValue("Code", defNode["SubjectCode"]);
                node.SetValue("SubjectFullCode", defNode["SubjectFullCode"]);
            }
            node.SetValue("CBSType", this.ModelDic.GetValue("Type"));

            node.SetValue("ChargerUser", this.ModelDic.GetValue("ChargerUser"));
            node.SetValue("ChargerUserName", this.ModelDic.GetValue("ChargerUserName"));
            node.SetValue("ChargerDept", this.ModelDic.GetValue("ChargerDept"));
            node.SetValue("ChargerDeptName", this.ModelDic.GetValue("ChargerDeptName"));

            node.SetValue("ContractInfoID", this.ModelDic.GetValue("ContractInfoID"));
            node.SetValue("ProjectInfoID", this.ModelDic.GetValue("ProjectInfoID"));
            node.SetValue("EngineeringInfoID", this.ModelDic.GetValue("EngineeringInfoID"));
            node.SetValue("ClueInfoID", this.ModelDic.GetValue("ClueInfoID"));
            parentCBS.AddChild(node);
            return node;
        }

        Dictionary<string, object> _setUnit(Dictionary<string, object> node, DataRow sourceRow, DataRow defNode, string tableName, bool immediateAdd = false)
        {
            if (String.IsNullOrEmpty(tableName)) throw new Exception("各单元的表名不允许为空");
            var source = new Dictionary<string, object>();//node;
            if (sourceRow != null)
            {
                source = FormulaHelper.DataRowToDic(sourceRow);
            }
            var dt = this.DB.ExecuteDataTable(String.Format("SELECT * FROM {1} WITH(NOLOCK) WHERE CBSNodeID='{0}'", node.GetValue("ID"), tableName));
            var unit = new Dictionary<string, object>();
            if (dt.Rows.Count == 0)
            {
                foreach (var key in this.ModelDic.Keys)
                {
                    if (key == "ID") continue;
                    if (key == "CreateUser") continue;
                    if (key == "CreateUserID") continue;
                    if (key == "CreateDate") continue;
                    if (key == "ModifyUser") continue;
                    if (key == "ModifyUserID") continue;
                    if (key == "ModifyDate") continue;
                    if (this.ModelDic[key] != null)
                        unit.SetValue(key, this.ModelDic[key]);
                }
                foreach (var key in source.Keys)
                {
                    if (key == "ID") continue;
                    if (key == "CBSNodeID") continue;
                    if (key == "CBSNodeFullID") continue;
                    if (key == "CBSInfoID") continue;
                    if (source[key] != null)
                        unit.SetValue(key, source[key]);
                }
                foreach (var key in node.Keys)
                {
                    if (key == "ID") continue;
                    if (key == "CBSNodeID") continue;
                    if (key == "CBSNodeFullID") continue;
                    if (key == "CBSInfoID") continue;
                    if (node[key] != null)
                        unit.SetValue(key, node[key]);
                }              
                unit.SetValue("CBSNodeID", node.GetValue("ID"));
                unit.SetValue("CBSNodeFullID", node.GetValue("FullID"));
                unit.SetValue("CBSInfoID", node.GetValue("CBSInfoID"));
                //unit.SetValue("Name", node.GetValue("Name"));
                //unit.SetValue("Code", node.GetValue("Code"));
                unit.InsertDB(this.DB, tableName);
            }
            else
            {
                unit = FormulaHelper.DataRowToDic(dt.Rows[0]);
                foreach (var key in this.ModelDic.Keys)
                {
                    if (key == "ID") continue;
                    if (key == "CreateUser") continue;
                    if (key == "CreateUserID") continue;
                    if (key == "CreateDate") continue;
                    if (key == "ModifyUser") continue;
                    if (key == "ModifyUserID") continue;
                    if (key == "ModifyDate") continue;
                    unit.SetValue(key, this.ModelDic[key]);
                }
                foreach (var key in node.Keys)
                {
                    if (key == "ID") continue;
                    if (key == "CBSNodeID") continue;
                    if (key == "CBSNodeFullID") continue;
                    if (key == "CBSInfoID") continue;
                    if (node[key] != null)
                        unit.SetValue(key, node[key]);
                }
                foreach (var key in source.Keys)
                {
                    if (key == "ID") continue;
                    if (key == "CBSNodeID") continue;
                    if (key == "CBSNodeFullID") continue;
                    if (key == "CBSInfoID") continue;
                    unit.SetValue(key, source[key]);
                }
                //unit.SetValue("Name", node.GetValue("Name"));
                //unit.SetValue("Code", node.GetValue("Code"));
                unit.UpdateDB(this.DB, tableName, unit.GetValue("ID"));
            }
            return unit;
        }
        #endregion
    }
}
