using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Config;
using Config.Logic;
using Formula;
using Formula.Helper;
using System.Data;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Expenses.Logic.Domain
{
    public partial class S_EP_BudgetVersion : BaseEPModel
    {
        S_EP_BudgetUnit _BudgetUnit;
        [NotMapped]
        [JsonIgnore]
        public S_EP_BudgetUnit BudgetUnit
        {
            get
            {
                if (_BudgetUnit == null)
                {
                    var dt = this.DB.ExecuteDataTable(String.Format("SELECT * FROM S_EP_BudgetUnit WHERE ID='{0}'", this.ModelDic.GetValue("BudgetUnitID")));
                    if (dt.Rows.Count > 0)
                    {
                        _BudgetUnit = new S_EP_BudgetUnit(FormulaHelper.DataRowToDic(dt.Rows[0]));
                    }
                }
                return _BudgetUnit;
            }
        }

        S_EP_CBSNode _BudgetNode;
        [NotMapped]
        [JsonIgnore]
        public S_EP_CBSNode BudgetNode
        {
            get
            {
                if (_BudgetNode == null && this.BudgetUnit != null)
                {
                    var dt = this.DB.ExecuteDataTable(String.Format("SELECT * FROM S_EP_CBSNode  WHERE ID='{0}'", this.BudgetUnit.ModelDic.GetValue("CBSNodeID")));
                    if (dt.Rows.Count > 0)
                    {
                        _BudgetNode = new S_EP_CBSNode(FormulaHelper.DataRowToDic(dt.Rows[0]));
                    }
                }
                return _BudgetNode;
            }
        }

        decimal _basisBudgetValue = 0;
        [NotMapped]
        [JsonIgnore]
        public Decimal BasisBudgetValue
        {
            get
            {
                if (_basisBudgetValue == 0)
                {
                    var budgetUnitDic = this.GetDataDicByID("S_EP_BudgetUnit", this.ModelDic.GetValue("BudgetUnitID"));
                    if (budgetUnitDic == null)
                    {
                        throw new Formula.Exceptions.BusinessValidationException("没有找到预算单元，无法获取预算依据");
                    }
                    var defineDic = this.GetDataDicByID("S_EP_DefineBudget", budgetUnitDic.GetValue("UnitDefineID"), ConnEnum.InfrasBaseConfig);
                    if (defineDic == null)
                    {
                        throw new Formula.Exceptions.BusinessValidationException("没有找到预算定义数据，无法获取预算依据");
                    }
                    var basisField = defineDic.GetValue("BudgetBasisField");
                    if (this.BudgetNode == null)
                    {
                        throw new Formula.Exceptions.BusinessValidationException("没有找到预算单元对应的CBS节点，无法获取预算依据");
                    }
                    var value = this.BudgetNode.ModelDic.GetValue(basisField);
                    if (System.Text.RegularExpressions.Regex.IsMatch(value, "^(-?\\d+)(\\.\\d+)?$"))
                    {
                        _basisBudgetValue = Convert.ToDecimal(value);
                    }
                }
                return _basisBudgetValue;
            }
        }

        public S_EP_BudgetVersion()
        {
        }

        public S_EP_BudgetVersion(Dictionary<string, object> model)
        {
            if (model == null)
                throw new Formula.Exceptions.BusinessValidationException("初始化构造S_EP_BudgetVersion的键值对不能对空值");
            this.FillModel(model);
        }

        public void InitBudgetDetail()
        {
            var budgetDetailDt = this.DB.ExecuteDataTable(String.Format("SELECT * FROM S_EP_BudgetVersion_Detail WHERE S_EP_BudgetVersionID='{0}'", this.ID));
            var existList = FormulaHelper.DataTableToListDic(budgetDetailDt);
            var budgetUnitDic = this.GetDataDicByID("S_EP_BudgetUnit", this.ModelDic.GetValue("BudgetUnitID"));
            if (budgetUnitDic == null)
            {
                throw new Formula.Exceptions.BusinessValidationException("没有找到预算单元，无法初始化预算明细");
            }

            var sql = @"select * from dbo.S_EP_DefineBudget_Subject
where DefineBudgetID='{0}' ORDER BY FULLID";
            var defineDt = this.InfrasDB.ExecuteDataTable(String.Format(sql, budgetUnitDic.GetValue("UnitDefineID")));
            if (this.BudgetNode == null) throw new Formula.Exceptions.BusinessValidationException("没有找到预算单元对应的CBS节点，无法初始化预算明细");
            var cbsNodeDt = this.DB.ExecuteDataTable(String.Format("SELECT * FROM S_EP_CBSNode WHERE FULLID LIKE '{0}%'", this.BudgetNode.ModelDic.GetValue("FullID")));
            foreach (DataRow defineNode in defineDt.Rows)
            {
                //判定该预算单元下，有没有CBS节点，如果没有，则不添加                
                var cbsRows = cbsNodeDt.Select("DefineID='" + defineNode["CBSDefineID"].ToString() + "'");
                //只添加节点类型为“核算节点”的CBS节点
                cbsRows = cbsRows.Where(a => a["AccountNodeType"] != null && a["AccountNodeType"] != DBNull.Value
                    && a["AccountNodeType"].ToString().Contains(AccountNodeType.Account.ToString())).ToArray();
                if (cbsRows.Length == 0)
                    continue;
                var sortIndex = Convert.ToDecimal(defineNode["SortIndex"] == null || defineNode["SortIndex"] == DBNull.Value ? 0 : defineNode["SortIndex"]);
                foreach (DataRow cbsRow in cbsRows)
                {
                    if (budgetDetailDt.Select("CBSID='" + cbsRow["ID"].ToString() + "'").Length > 0)
                    {
                        //CBS节点已经在预算中增加过了，不重复增加
                        continue;
                    }
                    if (cbsRows.Length > 1)
                        sortIndex += 0.01m;
                    var detail = new Dictionary<string, object>();
                    detail.SetValue("ID", FormulaHelper.CreateGuid());
                    detail.SetValue("Name", cbsRow["Name"]);
                    detail.SetValue("Code", cbsRow["Code"]);
                    detail.SetValue("CBSInfoID", cbsRow["CBSInfoID"]);
                    detail.SetValue("S_EP_BudgetVersionID", this.ID);
                    detail.SetValue("NodeType", cbsRow["NodeType"]);
                    if (detail.GetValue("NodeType") == CBSNodeType.Subject.ToString())
                    {
                        detail.SetValue("SubjectFullCode", cbsRow["SubjectFullCode"]);
                        detail.SetValue("SubjectType", cbsRow["SubjectType"]);
                        detail.SetValue("SubjectCode", cbsRow["Code"]);
                        detail.SetValue("SubjectID", cbsRow["SubjectID"]);
                    }
                    detail.SetValue("CBSType", cbsRow["CBSType"]);
                    detail.SetValue("CBSID", cbsRow["ID"]);
                    detail.SetValue("TotalScale", defineNode["DefaultScale"]);
                    detail.SetValue("ModifyState", ModifyState.Normal.ToString());
                    if (!String.IsNullOrEmpty(detail.GetValue("TotalScale")))
                    {
                        //默认根据比例计算
                        var scale = Convert.ToDecimal(detail.GetValue("TotalScale")) / 100;
                        var totalValue = scale * this.BasisBudgetValue;
                        if (totalValue > 0)
                        {
                            //首次初始化数据时，默认的预算都是0，如果有默认比例模板来初始化数据，并且预算不为0的时候
                            //设置修订状态为修改
                            detail.SetValue("ModifyState", ModifyState.Modified.ToString());
                        }

                        detail.SetValue("TotalValue", totalValue);
                    }
                    detail.SetValue("SortIndex", sortIndex);
                    detail.SetValue("BudgetDetailDefineID", defineNode["ID"]);
                    detail.SetValue("CBSParentID", cbsRow["ParentID"]);
                    detail.SetValue("CBSFullID", cbsRow["FullID"]);
                    detail.SetValue("CustomEdit", "False");
                    detail.InsertDB(this.DB, "S_EP_BudgetVersion_Detail", detail.GetValue("ID"));
                    existList.Add(detail);
                }
            }
            this.SummaryTotalValue();
        }

        public void SummaryTotalValue()
        {
            var detailDt = this.DB.ExecuteDataTable(String.Format("SELECT * FROM S_EP_BudgetVersion_Detail WHERE S_EP_BudgetVersionID='{0}'", this.ID));
            string sql = @"SELECT * FROM (
SELECT B.CBSID as PARENTID, A.* FROM dbo.S_EP_BudgetVersion_Detail A
left join S_EP_BudgetVersion_Detail B
on A.CBSID=B.CBSParentID) TABLEINFO
WHERE S_EP_BudgetVersionID='{0}' AND PARENTID IS NULL";
            var roots = this.DB.ExecuteDataTable(String.Format(sql, this.ID));
            var totalBudgetValue = 0m;
            foreach (DataRow root in roots.Rows)
            {
                if (root["CustomEdit"].ToString() != "True" && detailDt.AsEnumerable().Count(c => c["CBSParentID"] != DBNull.Value
                    && c["CBSParentID"].ToString() == c["CBSID"].ToString()) > 0)
                {
                    var sumFee = 0m;
                    var rootChildren = detailDt.AsEnumerable().Where(c => c["CBSPARENTID"] != DBNull.Value
                        && c["CBSPARENTID"].ToString() == root["CBSID"].ToString());
                    foreach (var childNode in rootChildren)
                    {
                        if (childNode["TotalValue"] != DBNull.Value)
                            sumFee += Convert.ToDecimal(childNode["TotalValue"]);
                    }
                    root["TotalValue"] = sumFee;
                    this.DB.ExecuteNonQuery(String.Format("UPDATE S_EP_BudgetVersion_Detail SET TotalValue={0} WHERE ID='{1}'", sumFee, root["ID"]));
                }
                totalBudgetValue += root["TotalValue"] == DBNull.Value ? 0 : Convert.ToDecimal(root["TotalValue"]);
            }
            this.ModelDic.SetValue("BudgetValue", totalBudgetValue);

            //从定义中读取预算依据取自CBS节点中的哪个字段
            var unitDefine = this.GetDataDicByID("S_EP_DefineBudget", this.BudgetUnit.ModelDic.GetValue("UnitDefineID"), ConnEnum.InfrasBaseConfig);
            if (unitDefine == null) throw new Formula.Exceptions.BusinessValidationException("没有找到对应的预算定义数据，无法汇总预算信息");
            var basisField = String.IsNullOrEmpty(unitDefine.GetValue("BudgetBasisField")) ? "ContractValue" : unitDefine.GetValue("BudgetBasisField");
            var nodeObj = this.BudgetNode.ModelDic.GetValue(basisField);
            var targetValue = String.IsNullOrEmpty(nodeObj)
                || !System.Text.RegularExpressions.Regex.IsMatch(nodeObj, "^(-?\\d+)(\\.\\d+)?$") ? 0m : Convert.ToDecimal(nodeObj);
            var budgetScale = 0m;
            var budgetProfit = 0m;
            var budgetProfitScale = 0m;
            if (targetValue > 0)
            {
                budgetScale = totalBudgetValue / targetValue * 100;
            }
            this.ModelDic.SetValue("BudgetScale", Math.Round(budgetScale));
            budgetProfit = targetValue - totalBudgetValue;
            this.ModelDic.SetValue("BudgetProfit", budgetProfit);
            if (targetValue > 0)
            {
                budgetProfitScale = budgetProfit / targetValue * 100;
            }
            this.ModelDic.SetValue("BudgetProfitScale", Math.Round(budgetProfitScale, 2));
            this.ModelDic.UpdateDB(this.DB, "S_EP_BudgetVersion", this.ModelDic.GetValue("ID"));
        }

        public void SumToTop()
        {

            var detailCBSList = this.DB.ExecuteDataTable(
               String.Format("SELECT * FROM S_EP_BudgetVersion_Detail where S_EP_BudgetVersionID='{0}' and NodeType!='{1}' ORDER BY CBSFULLID",
               this.ID, CBSNodeType.Detail.ToString())).AsEnumerable();
            var parentIDs = detailCBSList.Where(c => c["CBSParentID"] != null && c["CBSParentID"] != DBNull.Value).Select(c => c["CBSParentID"].ToString()).Distinct().ToList();
            //获取所有叶子节点
            var leafNodes = detailCBSList.Where(c => !parentIDs.Contains(c["CBSID"].ToString())).ToList();
            var leafParentIDs = leafNodes.Where(c => c["CBSParentID"] != null && c["CBSParentID"] != DBNull.Value).Select(c => c["CBSParentID"].ToString()).ToList();
            var leafParents = detailCBSList.Where(c => leafParentIDs.Contains(c["CBSID"].ToString()));
            foreach (var leaf in leafParents)
            {
                var leafBudgetNode = new S_EP_BudgetVersion_Detail(FormulaHelper.DataRowToDic(leaf));
                leafBudgetNode.SumTotalValue(this.BasisBudgetValue);
            }
        }

        public void Push()
        {
            var detailDt = this.DB.ExecuteDataTable(String.Format("SELECT * FROM S_EP_BudgetVersion_Detail WHERE S_EP_BudgetVersionID='{0}'",
                this.ModelDic.GetValue("ID")));
            var detailList = FormulaHelper.DataTableToListDic(detailDt);
            foreach (var detail in detailList)
            {
                var modifyState = detail.GetValue("ModifyState");
                if (modifyState == ModifyState.Added.ToString())
                {
                    //新增节点
                    var parentNodeDic = this.GetDataDicByID("S_EP_CBSNode", detail.GetValue("CBSParentID"));
                    if (parentNodeDic == null)
                    {
                        throw new Formula.Exceptions.BusinessValidationException("没有找到CBS节点，无法同步新增");
                    }
                    var parentNode = new S_EP_CBSNode(parentNodeDic);
                    var cbsNode = new Dictionary<string, object>();
                    cbsNode.SetValue("ID", detail.GetValue("CBSID"));
                    cbsNode.SetValue("Code", detail.GetValue("Code"));
                    cbsNode.SetValue("Name", detail.GetValue("Name"));
                    cbsNode.SetValue("BudgetValue", detail.GetValue("TotalValue"));
                    cbsNode.SetValue("SortIndex", detail.GetValue("SortIndex"));
                    parentNode.AddChild(cbsNode);
                }
                else if (modifyState == ModifyState.Removed.ToString())
                {
                    //删除节点
                    this.DB.ExecuteNonQuery(String.Format("DELETE FROM S_EP_CBSNode WHERE ID='{0}'", detail.GetValue("CBSID")));
                }
                else
                {
                    //更新节点
                    var nodeDic = new Dictionary<string, object>();
                    nodeDic.SetValue("ID", detail.GetValue("CBSID"));
                    nodeDic.SetValue("Name", detail.GetValue("Name"));
                    nodeDic.SetValue("BudgetValue", detail.GetValue("TotalValue"));
                    nodeDic.SetValue("UnitPrice", detail.GetValue("UnitPrice"));
                    nodeDic.SetValue("Quantity", detail.GetValue("Quantity"));
                    nodeDic.UpdateDB(this.DB, "S_EP_CBSNode", nodeDic.GetValue("ID"));
                }
            }
            var budgetDic = this.BudgetUnit.ModelDic;
            if (String.IsNullOrEmpty(budgetDic.GetValue("CreateUser")))
            {
                budgetDic.SetValue("CreateUser", this.ModelDic.GetValue("RegisterUserName"));
                budgetDic.SetValue("CreateUserID", this.ModelDic.GetValue("RegisterUser"));
                budgetDic.SetValue("CreateDate", DateTime.Now);
            }
            this.BudgetNode.SummaryBudgetValue();
            budgetDic.SetValue("ModifyUser", this.ModelDic.GetValue("RegisterUserName"));
            budgetDic.SetValue("ModifyUserID", this.ModelDic.GetValue("RegisterUser"));
            budgetDic.SetValue("ModifyDate", DateTime.Now);
            budgetDic.SetValue("TotalBudgetValue", this.ModelDic.GetValue("BudgetValue"));
            budgetDic.UpdateDB(this.DB, "S_EP_BudgetUnit", budgetDic.GetValue("ID"));
            this._SumBudgetToTop(this.BudgetNode.ParentNode);
            this._UpdateChildren(this.BudgetNode.Children);
        }

        void _SumBudgetToTop(S_EP_CBSNode cbsNode)
        {
            if (cbsNode == null)
                return;

            cbsNode.SummaryBudgetValue();
            if (cbsNode.ParentNode != null)
            {
                _SumBudgetToTop(cbsNode.ParentNode);
            }
            else
            {
                var infoDic = cbsNode.CBSInfo.ModelDic;
                infoDic.SetValue("BudgetValue", cbsNode.ModelDic.GetValue("BudgetValue"));
                infoDic.UpdateDB(this.DB, "S_EP_CBSInfo", infoDic.GetValue("ID"));
            }
        }

        void _UpdateChildren(List<S_EP_CBSNode> children)
        {
            foreach (var child in children)
            {
                var budgetUnitDt = this.DB.ExecuteDataTable(String.Format("SELECT * FROM S_EP_BudgetUnit WHERE CBSNodeID ='{0}'", child.ModelDic.GetValue("ID")));
                if (budgetUnitDt != null && budgetUnitDt.Rows.Count > 0)
                {
                    var budgetUnitDic = FormulaHelper.DataRowToDic(budgetUnitDt.Rows[0]);
                    budgetUnitDic.SetValue("TotalBudgetValue", child.ModelDic.GetValue("BudgetValue"));
                    budgetUnitDic.UpdateDB(this.DB, "S_EP_BudgetUnit", budgetUnitDic.GetValue("ID"));
                }

                _UpdateChildren(child.Children);
            }
        }
    }
}
