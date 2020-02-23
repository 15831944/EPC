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
    public partial class S_EP_DefineCBSNode : BaseEPModel
    {
        public S_EP_DefineCBSNode(Dictionary<string, object> dic)
        {
            this.FillModel(dic);
        }

        S_EP_DefineCBSNode _parent;
        [NotMapped]
        [JsonIgnore]
        public S_EP_DefineCBSNode ParentNode
        {
            get
            {
                if (_parent == null)
                {
                    string sql = "select * from {1} WITH(NOLOCK) where ID='{0}'";
                    var dt = this.InfrasDB.ExecuteDataTable(String.Format(sql, this.ModelDic.GetValue("ParentID"), this.GetType().Name));
                    if (dt.Rows.Count > 0)
                    {
                        var dic = FormulaHelper.DataRowToDic(dt.Rows[0]);
                        _parent = new S_EP_DefineCBSNode(dic);
                    }
                }
                return _parent;
            }
        }
        List<S_EP_DefineCBSNode> _chindren;
        [NotMapped]
        [JsonIgnore]
        public List<S_EP_DefineCBSNode> ChildrenNode
        {
            get
            {
                if (_chindren == null)
                {
                    _chindren = new List<S_EP_DefineCBSNode>();
                    string sql = "select * from {1} WITH(NOLOCK) where ParentID='{0}'";
                    var dt = this.InfrasDB.ExecuteDataTable(String.Format(sql, this.ModelDic.GetValue("ID"), this.GetType().Name));
                    foreach (DataRow dr in dt.Rows)
                    {
                        var dic = FormulaHelper.DataRowToDic(dr);
                        var node = new S_EP_DefineCBSNode(dic);
                        _chindren.Add(node);
                    }
                }
                return _chindren;
            }
        }

        List<S_EP_DefineCBSNode> _allchindren;
        [NotMapped]
        [JsonIgnore]
        public List<S_EP_DefineCBSNode> AllChildrenNode
        {
            get
            {
                if (_allchindren == null)
                {
                    _allchindren = new List<S_EP_DefineCBSNode>();
                    string sql = "select * from {1} WITH(NOLOCK) where FullID like '{0}%' and ID<>'{2}'";
                    var dt = this.InfrasDB.ExecuteDataTable(String.Format(sql, this.GetValue("FullID"), this.GetType().Name, this.GetValue("ID")));
                    foreach (DataRow dr in dt.Rows)
                    {
                        var dic = FormulaHelper.DataRowToDic(dr);
                        var node = new S_EP_DefineCBSNode(dic);
                        _allchindren.Add(node);
                    }
                }
                return _allchindren;
            }
        }

        public S_EP_DefineCBSNode AddChildNode()
        {
            var child = new Dictionary<string, object>();
            child.SetValue("ID", FormulaHelper.CreateGuid());
            child.SetValue("Name", "新建节点");
            var defineSql = "select top 1 Type from S_EP_DefineCBSInfo where id = '{0}'";
            var cbsType = this.InfrasDB.ExecuteScalar(String.Format(defineSql, this.ModelDic.GetValue("DefineID")));
            child.SetValue("CBSType", cbsType.ToString());
            return this.AddChildNode(child);
        }

        public S_EP_DefineCBSNode AddBrotherNode()
        {
            var child = new Dictionary<string, object>();
            child.SetValue("ID", FormulaHelper.CreateGuid());
            child.SetValue("Name", "新建节点");
            var defineSql = "select top 1 Type from S_EP_DefineCBSInfo where id = '{0}'";
            var cbsType = this.InfrasDB.ExecuteScalar(String.Format(defineSql, this.ModelDic.GetValue("DefineID")));
            child.SetValue("CBSType", cbsType.ToString());
            return this.AddBrotherNode(child);
        }

        public S_EP_DefineCBSNode AddBrotherNode(Dictionary<string, object> child)
        {
            if (this.ParentNode == null)
            {
                throw new Formula.Exceptions.BusinessValidationException("没有找到父节点，无法新增科目");
            }
            var sortIndex = String.IsNullOrEmpty(this.ModelDic.GetValue("SortIndex")) ? 0m : Convert.ToDecimal(this.ModelDic.GetValue("SortIndex"));
            child.SetValue("SortIndex", sortIndex + 1);
            string sql = "update {2} set SortIndex= SortIndex+1 where ParentID='{0}' and SortIndex>{1}";
            this.InfrasDB.ExecuteNonQuery(String.Format(sql, this.ModelDic.GetValue("ParentID"), sortIndex, this.GetType().Name));
            return this.ParentNode.AddChildNode(child);
        }

        public S_EP_DefineCBSNode AddChildNode(Dictionary<string, object> child)
        {
            if (String.IsNullOrEmpty(child.GetValue("ID")))
                child.SetValue("ID", FormulaHelper.CreateGuid());
            child.SetValue("ParentID", this.ModelDic.GetValue("ID"));
            child.SetValue("FullID", this.ModelDic.GetValue("FullID") + "." + child.GetValue("ID"));
            child.SetValue("DefineID", this.ModelDic.GetValue("DefineID"));
            if (String.IsNullOrEmpty(child.GetValue("IsDynamic")))
                child.SetValue("IsDynamic", false.ToString().ToLower());
            if (String.IsNullOrEmpty(child.GetValue("SortIndex")))
            {
                var sql = "select isnull(max(sortindex),0) from {1} with(nolock) where ParentID='{0}'";
                var maxSortIndex = this.InfrasDB.ExecuteScalar(String.Format(sql, this.ModelDic.GetValue("ID"), this.GetType().Name));
                child.SetValue("SortIndex", Convert.ToDecimal(maxSortIndex) + 1);
            }
            child.InsertDB(this.InfrasDB, this.GetType().Name, child.GetValue("ID"));
            return new S_EP_DefineCBSNode(child);
        }

        public void Delete()
        {
            if (this.ModelDic.GetValue("NodeType") == "Root" || String.IsNullOrEmpty(this.ModelDic.GetValue("ParentID")))
                throw new Formula.Exceptions.BusinessValidationException("根节点不允许删除");
            string sql = "delete from {1} where FullID like '{0}%'";
            this.InfrasDB.ExecuteNonQuery(String.Format(sql, this.ModelDic.GetValue("FullID"), this.GetType().Name));
        }
    }
}
