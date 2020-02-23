using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Config;
using Config.Logic;
using Formula;
using Formula.Helper;

namespace Expenses.Logic.Domain
{
    public partial class S_EP_DefineBudget_Subject : BaseEPModel
    {
        public S_EP_DefineBudget_Subject(Dictionary<string, object> dic)
        {
            this.FillModel(dic);
        }

        S_EP_DefineBudget_Subject _parent;
        public S_EP_DefineBudget_Subject ParentNode
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
                        _parent = new S_EP_DefineBudget_Subject(dic);
                    }
                }
                return _parent;
            }
        }


        public S_EP_DefineBudget_Subject AddChildNode()
        {
            var child = new Dictionary<string, object>();
            child.SetValue("ID", FormulaHelper.CreateGuid());
            child.SetValue("Name", "新建节点");
            return this.AddChildNode(child);
        }


        public S_EP_DefineBudget_Subject AddBrotherNode()
        {
            var child = new Dictionary<string, object>();
            child.SetValue("ID", FormulaHelper.CreateGuid());
            child.SetValue("Name", "新建节点");
            return this.AddBrotherNode(child);
        }

        public S_EP_DefineBudget_Subject AddBrotherNode(Dictionary<string, object> child)
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

        public S_EP_DefineBudget_Subject AddChildNode(Dictionary<string, object> child)
        {
            if (String.IsNullOrEmpty(child.GetValue("ID")))
                child.SetValue("ID", FormulaHelper.CreateGuid());
            child.SetValue("ParentID", this.ModelDic.GetValue("ID"));
            child.SetValue("FullID", this.ModelDic.GetValue("FullID") + "." + child.GetValue("ID"));
            child.SetValue("DefineID", this.ModelDic.GetValue("DefineID"));
            if (String.IsNullOrEmpty(child.GetValue("SortIndex")))
            {
                var sql = "select isnull(max(sortindex),0) from {1} with(nolock) where ParentID='{0}'";
                var maxSortIndex = this.DB.ExecuteScalar(String.Format(sql, this.ModelDic.GetValue("ID"), this.GetType().Name));
                child.SetValue("SortIndex", Convert.ToDecimal(maxSortIndex) + 1);
            }
            child.InsertDB(this.InfrasDB, this.GetType().Name, child.GetValue("ID"));
            return new S_EP_DefineBudget_Subject(child);
        }

        public void Delete()
        {
            string sql = "delete from {1} where FullID like '{0}%'";
            this.InfrasDB.ExecuteNonQuery(String.Format(sql, this.ModelDic.GetValue("FullID"), this.GetType().Name));
        }
    }
}
