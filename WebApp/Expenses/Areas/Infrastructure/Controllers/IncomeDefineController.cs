using Config;
using Config.Logic;
using Expenses.Logic;
using Expenses.Logic.Domain;
using Formula;
using Formula.Helper;
using MvcAdapter;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;

namespace Expenses.Areas.Infrastructure.Controllers
{
    public class IncomeDefineController : InstrasController
    {
        protected override string TableName
        {
            get
            {
                return "S_EP_DefineIncome";
            }
        }

        public JsonResult GetIncomDefineList(string DefineID)
        {
            string sql = @"select S_EP_DefineCBSNode.ID as NodeID,S_EP_DefineIncome.Name,S_EP_DefineCBSNode.FullID as NodeFullID, 
Code,NodeType,SortIndex,
IncomeType,Description,Condition,IsDefault,S_EP_DefineIncome.ID as ID
 from S_EP_DefineIncome  with(nolock)
left join S_EP_DefineCBSNode with(nolock) on S_EP_DefineIncome.NodeID=S_EP_DefineCBSNode.ID
where S_EP_DefineCBSNode.DefineID='{0}'
and IncomeUnit='{1}' ";
            var dt = this.SQLDB.ExecuteDataTable(String.Format(sql, DefineID, true.ToString().ToLower()));
            return Json(dt);
        }

        public JsonResult GetDetailList(string DefineIncomeID)
        {
            var sql = "select * from S_EP_DefineIncome_Detail with(nolock) where DefineIncomeID='{0}' order by sortindex ";
            var dt = this.SQLDB.ExecuteDataTable(String.Format(sql, DefineIncomeID));
            return Json(dt);
        }

        public JsonResult AddIncomeDefine(string ListData, string DefineID)
        {
            var list = JsonHelper.ToList(ListData);
            Action action = () =>
            {
                foreach (var item in list)
                {
                    var dic = new Dictionary<string, object>();
                    dic.SetValue("ID", FormulaHelper.CreateGuid());
                    dic.SetValue("DefineID", DefineID);
                    dic.SetValue("NodeID", item.GetValue("ID"));
                    dic.SetValue("Name", item.GetValue("Name"));
                    dic.SetValue("NodeFullID", item.GetValue("FullID"));
                    dic.SetValue("IncomeType", IncomeType.Progress.ToString());
                    dic.InsertDB(this.SQLDB, this.TableName, dic.GetValue("ID"));
                }
            };
            this.ExecuteAction(action);
            return Json("");
        }

        public JsonResult RemoveIncomeDefine(string ListIDs)
        {
            Action action = () =>
            {
                foreach (var ID in ListIDs.Split(','))
                {
                    this.SQLDB.ExecuteNonQuery(String.Format("delete from {0} where ID='{1}'",
                        this.TableName, ID));
                }
            };
            this.ExecuteAction(action);
            return Json("");
        }

        public JsonResult SaveData(string ListData, string DefineID)
        {
            var list = JsonHelper.ToList(ListData);
            Action action = () =>
            {
                foreach (var item in list)
                {
                    if (item.GetValue("IncomeType") == IncomeType.Cost.ToString())
                    {
                        //如果设置的是成本法，则必须在收入节点下或同层级有成本单元
                        string sql = @"select count(ID) from S_EP_DefineCBSNode
where FullID like  '{0}%' and NodeType!='Subject' and CostUnit='true'";
                        var node = this.GetDataDicByID("S_EP_DefineCBSNode", item.GetValue("NodeID"));
                        if (node == null) throw new Formula.Exceptions.BusinessValidationException("没有对应的CBS节点定义，无法设置收入单元");
                        var obj = this.SQLDB.ExecuteScalar(String.Format(sql, node.GetValue("FullID")));
                        if (Convert.ToInt32(obj) == 0)
                        {
                            throw new Formula.Exceptions.BusinessValidationException("成本法确认的收入单元下，必须定义成本单元");
                        }
                    }
                    if (String.IsNullOrEmpty(item.GetValue("ID")))
                    {
                        item.SetValue("DefineID", DefineID);
                        item.InsertDB(this.SQLDB, this.TableName);
                    }
                    else
                    {
                        item.UpdateDB(this.SQLDB, this.TableName, item.GetValue("ID"));
                    }
                }
            };
            this.ExecuteAction(action);
            return Json("");
        }

        public JsonResult AddDetail(string ListData, string DefineIncomeID)
        {
            var list = JsonHelper.ToList(ListData);
            var incomeDefine = this.GetDataDicByID("S_EP_DefineIncome", DefineIncomeID);
            if (incomeDefine == null) throw new Formula.Exceptions.BusinessValidationException("没有找到收入定义对象，操作失败");
            Action action = () =>
            {
                var sql = "select ID,ProgressDefineID from S_EP_DefineIncome_Detail with(nolock) where DefineIncomeID='{0}' ";
                var existDt = this.SQLDB.ExecuteDataTable(String.Format(sql, DefineIncomeID));
                foreach (var item in list)
                {
                    if (existDt.Select("ProgressDefineID='" + item.GetValue("ProgressDefineID") + "'").Length > 0)
                        continue;
                    var maxSortIndex = Convert.ToDecimal(this.SQLDB.ExecuteScalar(
                        String.Format("SELECT isnull(MAX(SortIndex),0) from S_EP_DefineIncome_Detail where DefineIncomeID='{0}'", DefineIncomeID)));
                    var dic = new Dictionary<string, object>();
                    dic.SetValue("ID", FormulaHelper.CreateGuid());
                    dic.SetValue("DefineIncomeID", DefineIncomeID);
                    dic.SetValue("DefineID", incomeDefine.GetValue("DefineID"));
                    dic.SetValue("DefineNodeID", incomeDefine.GetValue("NodeID"));
                    dic.SetValue("ProgressDefineID", item.GetValue("ID"));
                    dic.SetValue("ProgressDefineName", item.GetValue("Name"));
                    dic.SetValue("Scale", 0);
                    dic.SetValue("SortIndex", maxSortIndex + 1);
                    dic.InsertDB(this.SQLDB, "S_EP_DefineIncome_Detail", dic.GetValue("ID"));
                }
            };
            this.ExecuteAction(action);
            return Json("");
        }

        public JsonResult SaveDetail(string ListData)
        {
            var list = JsonHelper.ToList(ListData);
            Action action = () =>
            {
                foreach (var item in list)
                {
                    item.UpdateDB(this.SQLDB, "S_EP_DefineIncome_Detail", item.GetValue("ID"));
                }
            };
            this.ExecuteAction(action);
            return Json("");
        }

        public JsonResult RemoveDetail(string ListIDs)
        {
            Action action = () =>
            {
                foreach (var ID in ListIDs.Split(','))
                {
                    this.SQLDB.ExecuteNonQuery(String.Format("delete from S_EP_DefineIncome_Detail where ID='{0}'", ID));
                }
            };
            this.ExecuteAction(action);
            return Json("");
        }

        public JsonResult MoveUp(string detailID)
        {
            var dic = this.GetDataDicByID("S_EP_DefineIncome_Detail", detailID);
            Action action = () =>
            {
                var sql = "select top 1 ID,SortIndex from S_EP_DefineIncome_Detail where DefineIncomeID='{0}' and SortIndex<{1} order by sortindex desc";
                var dt = this.SQLDB.ExecuteDataTable(String.Format(sql, dic.GetValue("DefineIncomeID"), dic.GetValue("SortIndex")));
                if (dt.Rows.Count > 0)
                {
                    var sortIndex = String.IsNullOrEmpty(dic.GetValue("SortIndex")) ? 0m : Convert.ToDecimal(dic.GetValue("SortIndex"));
                    dic.SetValue("SortIndex", dt.Rows[0]["SortIndex"]);

                    this.SQLDB.ExecuteNonQuery(String.Format("update S_EP_DefineIncome_Detail set SortIndex={0} where ID='{1}'"
                        , dt.Rows[0]["SortIndex"], dic.GetValue("ID")));
                    this.SQLDB.ExecuteNonQuery(String.Format("update S_EP_DefineIncome_Detail set SortIndex={0} where ID='{1}'", sortIndex,
                        dt.Rows[0]["ID"]));
                }
            };
            this.ExecuteAction(action);
            return Json(dic);
        }

        public JsonResult MoveDown(string detailID)
        {
            var dic = this.GetDataDicByID("S_EP_DefineIncome_Detail", detailID);
            Action action = () =>
            {
                var sql = "select top 1 ID,SortIndex from S_EP_DefineIncome_Detail where DefineIncomeID='{0}' and SortIndex>{1} order by sortindex asc";
                var dt = this.SQLDB.ExecuteDataTable(String.Format(sql, dic.GetValue("DefineIncomeID"), dic.GetValue("SortIndex")));
                if (dt.Rows.Count > 0)
                {
                    var sortIndex = String.IsNullOrEmpty(dic.GetValue("SortIndex")) ? 0m : Convert.ToDecimal(dic.GetValue("SortIndex"));
                    dic.SetValue("SortIndex", dt.Rows[0]["SortIndex"]);

                    this.SQLDB.ExecuteNonQuery(String.Format("update S_EP_DefineIncome_Detail set SortIndex={0} where ID='{1}'"
                        , dt.Rows[0]["SortIndex"], dic.GetValue("ID")));
                    this.SQLDB.ExecuteNonQuery(String.Format("update S_EP_DefineIncome_Detail set SortIndex={0} where ID='{1}'", sortIndex,
                        dt.Rows[0]["ID"]));
                }
            };
            this.ExecuteAction(action);
            return Json(dic);
        }

        public JsonResult GetProgressNodeDefine(string ProgressDefineID)
        {
            string sql = String.Format("SELECT * FROM S_EP_DefineProgress_ProgressNode WHERE ProgressDefineID='{0}' ORDER BY SortIndex",
                ProgressDefineID);
            var dt = this.SQLDB.ExecuteDataTable(sql);
            return Json(dt);
        }
    }
}
