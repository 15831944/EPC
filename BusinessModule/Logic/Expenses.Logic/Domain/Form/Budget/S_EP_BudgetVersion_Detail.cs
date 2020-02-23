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
    public partial class S_EP_BudgetVersion_Detail : BaseEPModel
    {
        public S_EP_BudgetVersion_Detail()
        {

        }

        public S_EP_BudgetVersion_Detail(Dictionary<string, object> dic)
        {
            this.FillModel(dic);
        }


        S_EP_BudgetVersion_Detail _parent;
        [NotMapped]
        [JsonIgnore]
        public S_EP_BudgetVersion_Detail ParentNode
        {
            get
            {
                if (_parent == null)
                {
                    string sql = "select * from {1} WITH(NOLOCK) where CBSID='{0}'";
                    var dt = this.DB.ExecuteDataTable(String.Format(sql, this.ModelDic.GetValue("CBSParentID"), this.GetType().Name));
                    if (dt.Rows.Count > 0)
                    {
                        var dic = FormulaHelper.DataRowToDic(dt.Rows[0]);
                        _parent = new S_EP_BudgetVersion_Detail(dic);
                    }
                }
                return _parent;
            }
        }

        public S_EP_BudgetVersion_Detail AddChildNode(bool isDetail = false)
        {
            var allowAddChild = this.DB.ExecuteScalar(String.Format("SELECT AllowAddChild FROM {1}.dbo.S_EP_DefineBudget_Subject WHERE ID='{0}'",
                this.ModelDic.GetValue("BudgetDetailDefineID"), this.InfrasDB.DbName));
            if (!isDetail)
            {
                //新增明细数据的时候不校验定义结构
                //任意CBS节点都允许添加明细
                if (allowAddChild != null && allowAddChild != DBNull.Value)
                {
                    if (allowAddChild.ToString() == "0")
                    {
                        throw new Formula.Exceptions.BusinessValidationException("【" + this.ModelDic.GetValue("Name") + "】不允许新增子节点");
                    }
                }
            }
            var child = new Dictionary<string, object>();
            child.SetValue("ID", FormulaHelper.CreateGuid());
            child.SetValue("Name", "新建节点");
            return this.AddChildNode(child, isDetail);
        }

        public S_EP_BudgetVersion_Detail AddBrotherNode(bool isDetail = false)
        {
            var child = new Dictionary<string, object>();
            child.SetValue("ID", FormulaHelper.CreateGuid());
            child.SetValue("Name", "新建节点");
            return this.AddBrotherNode(child, isDetail);
        }

        public S_EP_BudgetVersion_Detail AddChildNode(Dictionary<string, object> child, bool isDetail = false)
        {
            if (isDetail && this.ModelDic.GetValue("NodeType") == CBSNodeType.Detail.ToString())
            {
                throw new Formula.Exceptions.BusinessValidationException("明细节点不能再增加明细节点");
            }

            if (String.IsNullOrEmpty(child.GetValue("ID")))
                child.SetValue("ID", FormulaHelper.CreateGuid());
            if (String.IsNullOrEmpty(child.GetValue("CBSID")))
            {
                child.SetValue("CBSID", child.GetValue("ID"));
            }
            if (String.IsNullOrEmpty(child.GetValue("NodeType")))
            {
                if (isDetail)
                {
                    child.SetValue("NodeType", CBSNodeType.Detail.ToString());
                }
                else
                {
                    child.SetValue("NodeType", CBSNodeType.Subject.ToString());
                }
            }
            child.SetValue("CBSParentID", this.ModelDic.GetValue("CBSID"));
            child.SetValue("CBSFullID", this.ModelDic.GetValue("CBSFullID") + "." + child.GetValue("CBSID"));
            child.SetValue("S_EP_BudgetVersionID", this.ModelDic.GetValue("S_EP_BudgetVersionID"));
            child.SetValue("CBSInfoID", this.ModelDic.GetValue("CBSInfoID"));
            child.SetValue("CBSType", this.ModelDic.GetValue("CBSType"));
            if (String.IsNullOrEmpty(child.GetValue("Code")))
            {
                child.SetValue("Code", FormulaHelper.CreateGuid());
            }
            if (String.IsNullOrEmpty(child.GetValue("SubjectCode")))
            {
                child.SetValue("SubjectCode", child.GetValue("Code"));
            }
            child.SetValue("SubjectFullCode", this.ModelDic.GetValue("SubjectFullCode") + "." + child.GetValue("SubjectCode"));
            if (String.IsNullOrEmpty(child.GetValue("SubjectType")))
            {
                child.SetValue("SubjectType", this.ModelDic.GetValue("SubjectType"));
            }
            if (String.IsNullOrEmpty(child.GetValue("SortIndex")))
            {
                var sql = "select isnull(max(sortindex),0) from {1} with(nolock) where CBSParentID='{0}'";
                var maxSortIndex = this.DB.ExecuteScalar(String.Format(sql, this.ModelDic.GetValue("CBSID"), this.GetType().Name));
                child.SetValue("SortIndex", Convert.ToDecimal(maxSortIndex) + 1);
            }
            child.SetValue("ModifyState", ModifyState.Added.ToString());
            child.SetValue("CustomEdit", "False");
            child.InsertDB(this.DB, this.GetType().Name, child.GetValue("ID"));
            return new S_EP_BudgetVersion_Detail(child);
        }

        public S_EP_BudgetVersion_Detail AddBrotherNode(Dictionary<string, object> child, bool isDetail = false)
        {
            if (this.ParentNode == null)
            {
                if (isDetail)
                {
                    throw new Formula.Exceptions.BusinessValidationException("明细节点必须属于一个CBS节点，不能凭空新增");
                }
                if (String.IsNullOrEmpty(child.GetValue("CBSID")))
                {
                    child.SetValue("CBSID", child.GetValue("ID"));
                }
                if (String.IsNullOrEmpty(child.GetValue("NodeType")))
                {
                    if (isDetail)
                    {
                        child.SetValue("NodeType", CBSNodeType.Detail.ToString());
                    }
                    else
                    {
                        child.SetValue("NodeType", CBSNodeType.Subject.ToString());
                    }
                }
                child.SetValue("S_EP_BudgetVersionID", this.ModelDic.GetValue("S_EP_BudgetVersionID"));
                child.SetValue("CBSParentID", this.ModelDic.GetValue("CBSParentID"));
                var fullID = this.ModelDic.GetValue("CBSParentID").Replace("." + this.ModelDic.GetValue("CBSID"), "." + child.GetValue("CBSID"));
                child.SetValue("CBSInfoID", this.ModelDic.GetValue("CBSInfoID"));
                child.SetValue("CBSFullID", fullID);
                if (String.IsNullOrEmpty(child.GetValue("Code")))
                {
                    child.SetValue("Code", FormulaHelper.CreateGuid());
                }
                if (String.IsNullOrEmpty(child.GetValue("SubjectCode")))
                {
                    child.SetValue("SubjectCode", child.GetValue("Code"));
                }
                var thisSubjectFullCode = this.ModelDic.GetValue("SubjectFullCode");
                child.SetValue("SubjectFullCode", thisSubjectFullCode.Substring(0, thisSubjectFullCode.LastIndexOf('.')) + "." + child.GetValue("SubjectCode"));
                var sortIndex = String.IsNullOrEmpty(this.ModelDic.GetValue("SortIndex")) ? 0m : Convert.ToDecimal(this.ModelDic.GetValue("SortIndex"));
                child.SetValue("SortIndex", sortIndex + 1);
                child.SetValue("ModifyState", ModifyState.Added.ToString());
                child.SetValue("CustomEdit", false.ToString().ToLower());
                string sql = "update {2} set SortIndex= SortIndex+1 where CBSParentID='{0}' and SortIndex>{1}";
                this.DB.ExecuteNonQuery(String.Format(sql, this.ModelDic.GetValue("CBSParentID"), sortIndex, this.GetType().Name));
                child.InsertDB(this.DB, this.GetType().Name, child.GetValue("ID"));
                return new S_EP_BudgetVersion_Detail(child);
            }
            else
            {
                var sortIndex = String.IsNullOrEmpty(this.ModelDic.GetValue("SortIndex")) ? 0m : Convert.ToDecimal(this.ModelDic.GetValue("SortIndex"));
                child.SetValue("SortIndex", sortIndex + 1);
                string sql = "update {2} set SortIndex= SortIndex+1 where CBSParentID='{0}' and SortIndex>{1}";
                this.DB.ExecuteNonQuery(String.Format(sql, this.ModelDic.GetValue("CBSParentID"), sortIndex, this.GetType().Name));
                if (this.ParentNode.ModelDic.GetValue("NodeType") == CBSNodeType.Detail.ToString() && isDetail)
                {
                    throw new Formula.Exceptions.BusinessValidationException("明细节点下不能再增加明细数据");
                }
                return this.ParentNode.AddChildNode(child);
            }
        }

        public void Delete()
        {
            var sql = @"SELECT S_EP_BudgetVersion_Detail.ID,S_EP_BudgetVersion_Detail.ModifyState,S_EP_BudgetVersion_Detail.Name,
CalExpression,InputType,isnull(AllowDelete,'true') as AllowDelete FROM S_EP_BudgetVersion_Detail WITH(NOLOCK) 
LEFT JOIN {1}.dbo.S_EP_DefineBudget_Subject ON S_EP_BudgetVersion_Detail.BudgetDetailDefineID= S_EP_DefineBudget_Subject.id
where S_EP_BudgetVersion_Detail.CBSFullID like '{0}%'";
            var nodeDt = this.DB.ExecuteDataTable(String.Format(sql, this.ModelDic.GetValue("CBSFullID"), this.InfrasDB.DbName));
            string removedIDs = string.Empty;
            string modifyIDs = string.Empty;

            foreach (DataRow row in nodeDt.Rows)
            {
                if (row["AllowDelete"].ToString().ToLower() == "false")
                {
                    throw new Formula.Exceptions.BusinessValidationException("【" + row["Name"] + "】节点不允许删除");
                }
                if (row["ModifyState"].ToString() != ModifyState.Added.ToString())
                {
                    modifyIDs += row["ID"].ToString() + ",";
                }
                else
                {
                    removedIDs += row["ID"].ToString() + ",";
                }
            }
            this.DB.ExecuteNonQuery(String.Format("UPDATE S_EP_BudgetVersion_Detail SET ModifyState='{0}' WHERE ID IN ('{1}')", ModifyState.Removed.ToString(), modifyIDs.TrimEnd(',').Replace(",", "','")));
            this.DB.ExecuteNonQuery(String.Format("DELETE FROM S_EP_BudgetVersion_Detail  WHERE ID IN ('{0}')", removedIDs.TrimEnd(',').Replace(",", "','")));
        }

        public void SumTotalValue(decimal basisBudgetValue = 0)
        {
            this.SummaryChildrenTotalValue(basisBudgetValue);
            var parentNode = this.ParentNode;
            while (parentNode != null)
            {
                parentNode.SummaryChildrenTotalValue(basisBudgetValue);
                parentNode = parentNode.ParentNode;
            }
        }

        public void SummaryChildrenTotalValue(decimal basisBudgetValue = 0)
        {
            if (this.ModelDic.GetValue("ModifyState") == ModifyState.Removed.ToString()) { return; }
            if (String.IsNullOrEmpty(this.ModelDic.GetValue("CustomEdit")) || this.ModelDic.GetValue("CustomEdit").ToLower() != true.ToString().ToLower())
            {
                var lastTotalValue = String.IsNullOrEmpty(this.ModelDic.GetValue("LastVersionValue")) ? 0m : Convert.ToDecimal(this.ModelDic.GetValue("LastVersionValue"));
                var currentTotalValue = 0m;
                var adjustValue = currentTotalValue - lastTotalValue;
                if (Convert.ToInt32(this.DB.ExecuteScalar(String.Format(@"SELECT COUNT(ID) FROM S_EP_BudgetVersion_Detail 
WHERE MODIFYSTATE!='{0}' AND CBSParentID='{1}' AND TotalValue IS NOT NULL", ModifyState.Removed.ToString(), this.ModelDic.GetValue("CBSID")))) > 0)
                {
                    currentTotalValue = Convert.ToDecimal(this.DB.ExecuteScalar(String.Format(@"SELECT ISNULL(SUM(TotalValue),0) FROM S_EP_BudgetVersion_Detail 
WHERE MODIFYSTATE!='{0}' AND CBSParentID='{1}' AND TotalValue IS NOT NULL", ModifyState.Removed.ToString(), this.ModelDic.GetValue("CBSID"))));
                }
                if (currentTotalValue != lastTotalValue)
                {
                    if (this.ModelDic.GetValue("ModifyState") == ModifyState.Normal.ToString())
                    {
                        this.ModelDic.SetValue("ModifyState", ModifyState.Modified.ToString());
                    }
                }
                else if (!String.IsNullOrEmpty(this.ModelDic.GetValue("LastVersionValue")) &&
                    this.ModelDic.GetValue("ModifyState") == ModifyState.Modified.ToString())
                {
                    this.ModelDic.SetValue("ModifyState", ModifyState.Normal.ToString());
                }
                if (basisBudgetValue > 0)
                {
                    var totalScale = currentTotalValue / basisBudgetValue * 100;
                    this.ModelDic.SetValue("TotalScale", totalScale);
                }
                this.ModelDic.SetValue("AdjustValue", adjustValue);
                this.ModelDic.SetValue("TotalValue", currentTotalValue);
                this.ModelDic.UpdateDB(this.DB, "S_EP_BudgetVersion_Detail", this.ModelDic.GetValue("ID"));
            }
        }
    }
}
