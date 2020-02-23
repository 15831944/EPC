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
    public partial class S_EP_DefineSubject : BaseEPModel
    {
        public S_EP_DefineSubject(Dictionary<string, object> dic)
        {
            this.FillModel(dic);
        }

        S_EP_DefineSubject _parent;
        public S_EP_DefineSubject ParentNode
        {
            get
            {
                if (_parent == null)
                {
                    string sql = "select * from S_EP_DefineSubject WITH(NOLOCK) where ID='{0}'";
                    var dt = this.DB.ExecuteDataTable(String.Format(sql, this.ModelDic.GetValue("ParentID")));
                    if (dt.Rows.Count > 0)
                    {
                        var dic = FormulaHelper.DataRowToDic(dt.Rows[0]);
                        _parent = new S_EP_DefineSubject(dic);
                    }
                }
                return _parent;
            }
        }

        public S_EP_DefineSubject AddChildNode()
        {
            var child = new Dictionary<string, object>();
            child.SetValue("ID", FormulaHelper.CreateGuid());
            child.SetValue("Name", "新建科目");
            return this.AddChildNode(child);
        }

        public S_EP_DefineSubject AddBrotherNode()
        {
            var child = new Dictionary<string, object>();
            child.SetValue("ID", FormulaHelper.CreateGuid());
            child.SetValue("Name", "新建科目");
            return this.AddBrotherNode(child);
        }

        public S_EP_DefineSubject AddBrotherNode(Dictionary<string, object> child)
        {
            if (this.ParentNode == null)
            {
                throw new Formula.Exceptions.BusinessValidationException("没有找到父节点，无法新增科目");
            }
            var sortIndex = String.IsNullOrEmpty(this.ModelDic.GetValue("SortIndex")) ? 0m : Convert.ToDecimal(this.ModelDic.GetValue("SortIndex"));
            child.SetValue("SortIndex", sortIndex + 1);
            string sql = "update S_EP_DefineSubject set SortIndex= SortIndex+1 where ParentID='{0}' and SortIndex>{1}";
            this.InfrasDB.ExecuteNonQuery(String.Format(sql, this.ModelDic.GetValue("ParentID"), sortIndex));
            return this.ParentNode.AddChildNode(child);
        }

        public S_EP_DefineSubject AddChildNode(Dictionary<string, object> child)
        {
            if (String.IsNullOrEmpty(child.GetValue("ID")))
                child.SetValue("ID", FormulaHelper.CreateGuid());
            child.SetValue("ParentID", this.ModelDic.GetValue("ID"));
            child.SetValue("FullID", this.ModelDic.GetValue("FullID") + "." + child.GetValue("ID"));
            child.SetValue("NodeType", "Subject");
            child.SetValue("Level", child.GetValue("FullID").Split('.').Length);
            if (String.IsNullOrEmpty(child.GetValue("SortIndex")))
            {
                var sql = "select isnull(max(sortindex),0) from S_EP_DefineSubject with(nolock) where ParentID='{0}'";
                var maxSortIndex = this.InfrasDB.ExecuteScalar(String.Format(sql, this.ModelDic.GetValue("ID")));
                child.SetValue("SortIndex", Convert.ToDecimal(maxSortIndex) + 1);
            }
            child.InsertDB(this.InfrasDB, "S_EP_DefineSubject", child.GetValue("ID"));
            return new S_EP_DefineSubject(child);
        }

        public void Delete()
        {
            if (this.ModelDic.GetValue("NodeType") == "Root" || String.IsNullOrEmpty(this.ModelDic.GetValue("ParentID")))
                throw new Formula.Exceptions.BusinessValidationException("根节点不允许删除");
            string sql = "delete from S_EP_DefineSubject where FullID like '{0}%'";
            this.InfrasDB.ExecuteNonQuery(String.Format(sql, this.ModelDic.GetValue("FullID")));
        }
    }
}
