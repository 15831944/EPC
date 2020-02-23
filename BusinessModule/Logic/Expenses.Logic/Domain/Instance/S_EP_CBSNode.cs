using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Config;
using Config.Logic;
using Formula;
using Formula.Helper;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using System.Data;


namespace Expenses.Logic.Domain
{
    public partial class S_EP_CBSNode : BaseEPModel
    {
        S_EP_DefineCBSNode _defineNode;
        [NotMapped]
        [JsonIgnore]
        public S_EP_DefineCBSNode DefineNode
        {
            get
            {
                if (_defineNode == null)
                {
                    var dt = this.InfrasDB.ExecuteDataTable(String.Format("SELECT * FROM S_EP_DefineCBSNode WHERE ID='{0}'", this.ModelDic.GetValue("DefineID")));
                    if (dt.Rows.Count > 0)
                    {
                        _defineNode = new S_EP_DefineCBSNode(FormulaHelper.DataRowToDic(dt.Rows[0]));
                    }
                }
                return _defineNode;
            }
        }

        S_EP_CBSNode _parent;
        [NotMapped]
        [JsonIgnore]
        public S_EP_CBSNode ParentNode
        {
            get
            {
                if (_parent == null)
                {
                    var dt = this.DB.ExecuteDataTable(String.Format("SELECT * FROM {0} WHERE ID='{1}'", this.GetType().Name, this.ModelDic.GetValue("ParentID")));
                    if (dt.Rows.Count > 0)
                    {
                        _parent = new S_EP_CBSNode(FormulaHelper.DataRowToDic(dt.Rows[0]));
                    }
                }
                return _parent;
            }
        }

        List<S_EP_CBSNode> _ancestors;
        [NotMapped]
        [JsonIgnore]
        public List<S_EP_CBSNode> Ancestors
        {
            get
            {
                if (_ancestors == null)
                {
                    _ancestors = new List<S_EP_CBSNode>();
                    var ancestorDt = this.DB.ExecuteDataTable(
                        String.Format("SELECT * FROM S_EP_CBSNODE WHERE '{0}' LIKE FULLID+'%' AND ID!='{1}' ORDER BY FULLID"
                        , this.ModelDic.GetValue("FullID"), this.ModelDic.GetValue("ID")));
                    foreach (DataRow item in ancestorDt.Rows)
                    {
                        _ancestors.Add(new S_EP_CBSNode(FormulaHelper.DataRowToDic(item)));
                    }
                }
                return _ancestors;
            }
        }

        List<S_EP_CBSNode> _Children;
        [NotMapped]
        [JsonIgnore]
        public List<S_EP_CBSNode> Children
        {
            get
            {
                if (_Children == null)
                {
                    _Children = new List<S_EP_CBSNode>();
                    var dt = this.DB.ExecuteDataTable(String.Format("SELECT * FROM {0}  WITH(NOLOCK) WHERE ParentID='{1}'", this.GetType().Name, this.ModelDic.GetValue("ID")));
                    foreach (DataRow row in dt.Rows)
                    {
                        _Children.Add(new S_EP_CBSNode(FormulaHelper.DataRowToDic(row)));
                    }
                }
                return _Children;
            }
        }

        List<S_EP_CBSNode> _allChildren;
        [NotMapped]
        [JsonIgnore]
        public List<S_EP_CBSNode> AllChildren
        {
            get
            {
                if (_allChildren == null)
                {
                    _allChildren = new List<S_EP_CBSNode>();
                    var dt = this.DB.ExecuteDataTable(String.Format("SELECT * FROM {0}  WITH(NOLOCK) WHERE FullID like '{1}%' and ID<>'{2}'", this.GetType().Name,
                        this.ModelDic.GetValue("FullID"), this.ModelDic.GetValue("ID")));
                    foreach (DataRow row in dt.Rows)
                    {
                        _allChildren.Add(new S_EP_CBSNode(FormulaHelper.DataRowToDic(row)));
                    }
                }
                return _allChildren;
            }
        }

        S_EP_CBSInfo _cbsInfo;
        [NotMapped]
        [JsonIgnore]
        public S_EP_CBSInfo CBSInfo
        {
            get
            {
                if (_cbsInfo == null)
                {
                    var dt = this.DB.ExecuteDataTable(String.Format("SELECT * FROM {0} WHERE ID='{1}'", "S_EP_CBSInfo", this.ModelDic.GetValue("CBSInfoID")));
                    if (dt.Rows.Count > 0)
                    {
                        _cbsInfo = new S_EP_CBSInfo(FormulaHelper.DataRowToDic(dt.Rows[0]));
                    }
                }
                return _cbsInfo;
            }
        }

        public S_EP_CBSNode()
        {
        }

        public S_EP_CBSNode(Dictionary<string, object> dic)
        {
            if (dic == null)
                throw new Formula.Exceptions.BusinessValidationException("初始化构造S_EP_CBSNode的键值对不能对空值");
            this.FillModel(dic);
        }

        public S_EP_CBSNode AddEmptyChild(String nodeType = "")
        {
            var child = new Dictionary<string, object>();
            child.SetValue("Name", "新建CBS节点");
            child.SetValue("ID", FormulaHelper.CreateGuid());
            child.SetValue("Code", child.GetValue("ID"));
            child.SetValue("FullCode", this.ModelDic.GetValue("FullCode") + "." + child.GetValue("Code"));
            child.SetValue("FullID", this.ModelDic.GetValue("FullID") + "." + child.GetValue("ID"));
            child.SetValue("ParentID", this.ModelDic.GetValue("ID"));
            child.SetValue("CBSInfoID", this.ModelDic.GetValue("CBSInfoID"));
            child.SetValue("DefineID", "");
            child.SetValue("ProjectInfoID", this.ModelDic.GetValue("ProjectInfoID"));
            child.SetValue("EngineeringInfoID", this.ModelDic.GetValue("EngineeringInfoID"));
            child.SetValue("ContractInfoID", this.ModelDic.GetValue("ContractInfoID"));
            child.SetValue("OrgID", this.ModelDic.GetValue("OrgID"));
            if (String.IsNullOrEmpty(nodeType))
                nodeType = CBSNodeType.Subject.ToString();
            child.SetValue("NodeType", nodeType);
            child.SetValue("CBSType", this.ModelDic.GetValue("CBSType"));
            var maxSortIndex = Convert.ToInt32(this.DB.ExecuteScalar("SELECT ISNULL(MAX(SortIndex),0) FROM S_EP_CBSNode WITH(NOLOCK) WHERE PARENTID='" + this.ModelDic.GetValue("ID") + "'"));
            var sortIndex = maxSortIndex + 1;
            child.SetValue("SortIndex", sortIndex);
            child.InsertDB(this.DB, "S_EP_CBSNode", child.GetValue("ID"));
            var result = new S_EP_CBSNode(child);
            return result;
        }

        public void AddChild(S_EP_CBSNode childNode)
        {
            this.AddChild(childNode.ModelDic);
        }

        public void AddChild(Dictionary<string, object> child)
        {
            if (String.IsNullOrEmpty(child.GetValue("ID")))
            {
                child.SetValue("ID", FormulaHelper.CreateGuid());
            }
            if (String.IsNullOrEmpty(child.GetValue("Name")))
                throw new Formula.Exceptions.BusinessValidationException("必须指定CBS节点的名称");
            if (String.IsNullOrEmpty(child.GetValue("Code")))
                throw new Formula.Exceptions.BusinessValidationException("必须指定CBS节点的编码");
            if (String.IsNullOrEmpty(child.GetValue("NodeType")))
            {
                child.SetValue("NodeType", CBSNodeType.Subject.ToString());
            }
            else if (child.GetValue("NodeType") != CBSNodeType.Subject.ToString())
            {
                if (child.GetValue("NodeType") != CBSNodeType.CBS.ToString())
                {
                    S_EP_DefineCBSNode childDefineNode = null;
                    if (String.IsNullOrEmpty(child.GetValue("DefineID")))
                    {
                        childDefineNode = this.DefineNode.ChildrenNode.FirstOrDefault(c => c.GetValue("NodeType") == child.GetValue("NodeType"));
                    }
                    else
                    {
                        childDefineNode = this.DefineNode.ChildrenNode.FirstOrDefault(c => c.GetValue("ID") == child.GetValue("DefineID"));
                    }

                    if (childDefineNode == null)
                    {
                        throw new Formula.Exceptions.BusinessValidationException("结构定义下不允许新增类型为【" + child.GetValue("NodeType") + "】的CBS节点");
                    }
                    child.SetValue("DefineID", childDefineNode.GetValue("ID"));
                    child.SetValue("AccountNodeType", childDefineNode.GetValue("AccountNodeType"));
                }
                else
                {
                    var childDefineNode = this.DefineNode.ChildrenNode.FirstOrDefault(c => c.GetValue("ID") == child.GetValue("DefineID"));
                    if (childDefineNode == null)
                    {
                        throw new Formula.Exceptions.BusinessValidationException("非科目类节点新增必须指定节点定义ID");
                    }
                    child.SetValue("AccountNodeType", childDefineNode.GetValue("AccountNodeType"));
                }
            }
            child.SetValue("ParentID", this.ModelDic.GetValue("ID"));
            child.SetValue("CBSInfoID", this.ModelDic.GetValue("CBSInfoID"));
            if (String.IsNullOrEmpty(child.GetValue("FullCode")))
            {
                child.SetValue("FullCode", this.ModelDic.GetValue("FullCode") + "." + child.GetValue("Code"));
            }
            child.SetValue("FullID", this.ModelDic.GetValue("FullID") + "." + child.GetValue("ID"));
            child.SetValue("CBSType", this.ModelDic.GetValue("CBSType"));
            if (String.IsNullOrEmpty(child.GetValue("SortIndex")))
            {
                var maxSortIndex = Convert.ToInt32(this.DB.ExecuteScalar("SELECT ISNULL(MAX(SortIndex),0) FROM S_EP_CBSNode WITH(NOLOCK) WHERE PARENTID='" + this.ModelDic.GetValue("ID") + "'"));
                var sortIndex = maxSortIndex + 1;
                child.SetValue("SortIndex", sortIndex);
            }
            child.SetValue("EngineeringInfoID", this.ModelDic.GetValue("EngineeringInfoID"));
            child.SetValue("ContractInfoID", this.ModelDic.GetValue("ContractInfoID"));
            child.SetValue("OrgID", this.ModelDic.GetValue("OrgID"));
            child.InsertDB(this.DB, "S_EP_CBSNode", child.GetValue("ID"));
        }

        public void SetUnit(bool resetProgressNode = true)
        {
            var dt = this.InfrasDB.ExecuteDataTable(String.Format("SELECT * FROM S_EP_DefineCBSNode WHERE ID='{0}'", this.ModelDic.GetValue("DefineID")));
            if (dt.Rows.Count == 0)
            {
                return;
            }
            this.CBSInfo.setUnit(this.ModelDic, null, dt.Rows[0], resetProgressNode);
        }

        public void AutoSplitProductionValueToReversed(decimal changeValue, bool igoreReserved = false, bool loopDeep = true)
        {
            var sql = String.Format(@"select * from S_EP_CBSNode with(nolock) where ParentID='{0}' and NodeType='{1}'  ", this.GetValue("ID"), CBSNodeType.Reserved.ToString());
            DataTable dt = new DataTable();

            #region 判定是否直接将变更金额分解至子节点
            var splitToChildren = false;
            if (igoreReserved)
                splitToChildren = true;
            else
            {
                dt = this.DB.ExecuteDataTable(sql);
                if (dt.Rows.Count == 0)
                    splitToChildren = true;
            }
            #endregion

            if (splitToChildren)
            {
                //说明没有定义预留节点
                sql = String.Format(@"select S_EP_CBSNode.*,isnull(ReservedValue,0) as ReservedValue,
isnull(ProductionValue,0)- ISNULL(ReservedValue,0) SplitProductionValue,
isnull(ProductionValue,0)- ISNULL(ReservedValue,0) -ISNULL(ProductionSettleValue,0) as RemainProductionValue from S_EP_CBSNode
left join (select Sum(ProductionValue) as ReservedValue,ParentID from S_EP_CBSNode
where NodeType='{1}' group by ParentID) ReservedInfo on ReservedInfo.ParentID=S_EP_CBSNode.ID
where NodeType<>'{2}' and  S_EP_CBSNode.ParentID= '{0}' 
and AccountNodeType like '%Production%' and NodeType<>'{1}' order by SortIndex",
               this.GetValue("ID"), CBSNodeType.Reserved.ToString(), CBSNodeType.Communal.ToString());
                var children = this.DB.ExecuteDataTable(sql);
                var obj = children.Compute(" Sum(RemainProductionValue) ", "");
                var sumRemain = 0m;
                if (obj != null && obj != DBNull.Value)
                {
                    sumRemain = Convert.ToDecimal(obj);
                }
                if (sumRemain == 0) return;
                foreach (DataRow item in children.Rows)
                {
                    var cbsDic = FormulaHelper.DataRowToDic(item);
                    var remainValue = String.IsNullOrEmpty(cbsDic.GetValue("RemainProductionValue")) ? 0m : Convert.ToDecimal(cbsDic.GetValue("RemainProductionValue"));
                    var addValue = changeValue * remainValue / sumRemain;
                    var productionValue = String.IsNullOrEmpty(cbsDic.GetValue("ProductionValue")) ? 0m : Convert.ToDecimal(cbsDic.GetValue("ProductionValue"));
                    var newValue = productionValue + addValue;
                    cbsDic.SetValue("ProductionValue", newValue);
                    cbsDic.UpdateDB(this.DB, "S_EP_CBSNode", cbsDic.GetValue("ID"));
                    var cbsNode = new S_EP_CBSNode(cbsDic);
                    if (loopDeep)
                        cbsNode.AutoSplitProductionValueToReversed(addValue, igoreReserved, loopDeep);
                }
            }
            else
            {
                var reservedNodeDic = FormulaHelper.DataRowToDic(dt.Rows[0]);
                var productionValue = String.IsNullOrEmpty(reservedNodeDic.GetValue("ProductionValue")) ? 0 : Convert.ToDecimal(reservedNodeDic.GetValue("ProductionValue"));
                var sumValue = productionValue + changeValue;
                reservedNodeDic.SetValue("ProductionValue", sumValue);
                reservedNodeDic.UpdateDB(this.DB, "S_EP_CBSNode", reservedNodeDic.GetValue("ID"));
            }
        }

        public void AutoSplitProductionValue(string exceptNodeTypes, bool loopDeep = true)
        {
            var sql = @"select *,isnull(ProductionValue,0)-isnull(ProductionSettleValue,0) as RemainProductionValue
            from S_EP_CBSNode with(nolock) where ParentID='{0}' ";
            foreach (var item in exceptNodeTypes.Split(','))
            {
                sql += " and NodeType<>'" + item + "'";
            }
            var dt = this.DB.ExecuteDataTable(String.Format(sql, this.ID));
            if (dt.Rows.Count == 0) return;
            var sumValue = Convert.ToDecimal(dt.Compute(" Sum(RemainProductionValue) ", ""));
            var currentValue = String.IsNullOrEmpty(this.GetValue("ProductionValue")) ? 0m : Convert.ToDecimal(this.GetValue("ProductionValue"));
            foreach (DataRow row in dt.Rows)
            {
                var value = row["RemainProductionValue"] == DBNull.Value ? 0m : Convert.ToDecimal(row["RemainProductionValue"]);
                var splitValue = value / sumValue * currentValue;
                var item = FormulaHelper.DataRowToDic(row);
                item.SetValue("ProductionValue", splitValue);
                item.UpdateDB(this.DB, "S_EP_CBSNode", item.GetValue("ID"));
                var cbsNode = new S_EP_CBSNode(item);
                cbsNode.SetUnit(false);
                if (loopDeep)
                {
                    cbsNode.AutoSplitProductionValue(exceptNodeTypes, loopDeep);
                }
            }
        }

        public void SumContractValueToTop()
        {
            this.SumContractValue();
            if (this.ParentNode != null)
            {
                this.ParentNode.SumContractValueToTop();
            }
            else
            {
                var infoDic = this.CBSInfo.ModelDic;
                infoDic.SetValue("ContractValue", this.ModelDic.GetValue("ContractValue"));
                infoDic.UpdateDB(this.DB, "S_EP_CBSInfo", infoDic.GetValue("ID"));
            }
        }

        public void SumProductionValueToTop()
        {
            this.SumProductionValue();
            if (this.ParentNode != null)
            {
                this.ParentNode.SumProductionValueToTop();
            }
            else
            {
                var infoDic = this.CBSInfo.ModelDic;
                infoDic.SetValue("ProductionValue", this.ModelDic.GetValue("ProductionValue"));
                infoDic.UpdateDB(this.DB, "S_EP_CBSInfo", infoDic.GetValue("ID"));
            }
        }

        public void SumProductionSettleValueToTop()
        {
            this.SumProductionSettleValue();
            if (this.ParentNode != null)
            {
                this.ParentNode.SumProductionSettleValueToTop();
            }
            else
            {
                var infoDic = this.CBSInfo.ModelDic;
                infoDic.SetValue("ProductionSettleValue", this.ModelDic.GetValue("ProductionSettleValue"));
                infoDic.UpdateDB(this.DB, "S_EP_CBSInfo", infoDic.GetValue("ID"));
            }
        }

        public void SummaryBudgetValue()
        {
            if (this.Children.Count > 0)
            {
                string sql = "SELECT isnull(Sum(BudgetValue),0) FROM {0} WITH(NOLOCK) WHERE ParentID='{1}'";
                var sumBudgetValue = Convert.ToDecimal(this.DB.ExecuteScalar(String.Format(sql, this.GetType().Name, this.ModelDic.GetValue("ID"))));
                this.ModelDic.SetValue("BudgetValue", sumBudgetValue);
                this.ModelDic.UpdateDB(this.DB, this.GetType().Name, this.ModelDic.GetValue("ID"));
            }
        }

        public void SumProductionSettleValue()
        {
            if (this.Children.Count > 0)
            {
                string sql = "SELECT isnull(Sum(ProductionSettleValue),0) FROM {0} WITH(NOLOCK) WHERE ParentID='{1}'";
                var sumValue = Convert.ToDecimal(this.DB.ExecuteScalar(String.Format(sql, this.GetType().Name, this.ModelDic.GetValue("ID"))));
                if (sumValue > 0)
                {
                    this.ModelDic.SetValue("ProductionSettleValue", sumValue);
                    this.ModelDic.UpdateDB(this.DB, this.GetType().Name, this.ModelDic.GetValue("ID"));
                }
            }
        }

        public void SummaryCostValue()
        {

        }

        public void Delete(bool validate = true)
        {
            if (validate)
            {
                //CBS节点或下级节点未预算单元，且预算编制完成的节点不允许删除
                var budgetCount = Convert.ToInt32(this.DB.ExecuteScalar(String.Format(@"select count(0) from S_EP_BudgetUnit  WITH(NOLOCK)
left join (select count(ID) as BudgetCount,BudgetUnitID from S_EP_BudgetVersion WITH(NOLOCK)
group by BudgetUnitID) VERSIONTABLE
on S_EP_BudgetUnit.ID=VERSIONTABLE.BudgetUnitID
where BudgetCount>0 and CBSNodeFullID like '{0}%'", this.ModelDic.GetValue("FullID"))));

                //已经有结算成本数据的节点，不允许进行删除
                if (!String.IsNullOrEmpty(this.ModelDic.GetValue("CostValue")) && Convert.ToDecimal(this.ModelDic.GetValue("CostValue")) > 0)
                {
                    throw new Formula.Exceptions.BusinessValidationException("已经有结算金额的节点不允许进行删除");
                }
            }
            if (String.IsNullOrEmpty(this.ModelDic.GetValue("FullID")))
            {
                this.DB.ExecuteNonQuery(String.Format("DELETE FROM S_EP_CBSNode WHERE ID = '{0}'", this.ModelDic.GetValue("ID")));
            }
            else
            {
                this.DB.ExecuteNonQuery(String.Format("DELETE FROM S_EP_CBSNode WHERE FULLID LIKE '{0}%'", this.ModelDic.GetValue("FullID")));
            }
        }

        public int GetBrothersCount()
        {
            var brothers = this.DB.ExecuteScalar(String.Format(String.Format("select count(1) from S_EP_CBSNode with(nolock) where ParentID='{0}' and ID<>'{1}'",
                this.ModelDic.GetValue("ParentID"), this.ModelDic.GetValue("ID"))));
            return Convert.ToInt32(brothers);
        }

        public void SumContractValue()
        {
            if (this.Children.Count > 0)
            {
                string sql = "SELECT isnull(Sum(ContractValue),0) FROM {0} WITH(NOLOCK) WHERE ParentID='{1}'";
                var sumContractValue = Convert.ToDecimal(this.DB.ExecuteScalar(String.Format(sql, this.GetType().Name, this.ModelDic.GetValue("ID"))));
                if (sumContractValue > 0)
                {
                    this.ModelDic.SetValue("ContractValue", sumContractValue);
                    this.ModelDic.UpdateDB(this.DB, this.GetType().Name, this.ModelDic.GetValue("ID"));
                }
            }
        }

        public void SumProductionValue()
        {
            if (this.Children.Count > 0)
            {
                string sql = "SELECT isnull(Sum([ProductionValue]),0) FROM {0} WITH(NOLOCK) WHERE ParentID='{1}'";
                var sumValue = Convert.ToDecimal(this.DB.ExecuteScalar(String.Format(sql, this.GetType().Name, this.ModelDic.GetValue("ID"))));
                if (sumValue > 0)
                {
                    this.ModelDic.SetValue("ProductionValue", sumValue);
                    this.ModelDic.UpdateDB(this.DB, this.GetType().Name, this.ModelDic.GetValue("ID"));
                }
            }
        }
    }

    public struct InComeCondition
    {
        public string GroupName { get; set; }
        public bool Result { get; set; }
        public string FieldName { get; set; }
    }
}
