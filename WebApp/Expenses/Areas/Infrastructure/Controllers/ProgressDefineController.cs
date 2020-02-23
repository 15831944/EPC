using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

using System.Transactions;
using Config;
using Config.Logic;
using Formula;
using Formula.Helper;
using MvcAdapter;
using Expenses.Logic;
using Expenses.Logic.Domain;
using System.Data;

namespace Expenses.Areas.Infrastructure.Controllers
{
    public class ProgressDefineController : InstrasController
    {
        protected override string TableName
        {
            get
            {
                return "S_EP_DefineProgress";
            }
        }

        protected override void BeforeSave(Dictionary<string, object> dic, bool isNew)
        {
            if (isNew)
            {
                var maxSortIndex = Convert.ToDecimal(this.SQLDB.ExecuteScalar("select isnull(max(sortindex),0) from S_EP_DefineProgress with(nolock) "));
                dic.SetValue("SortIndex", maxSortIndex + 1);
            }
        }
        public JsonResult GetDetailList(string DefineID)
        {
            string sql = "select * from S_EP_DefineProgress_ProgressNode with(nolock) where ProgressDefineID='{0}' order by SortIndex";
            var dt = this.SQLDB.ExecuteDataTable(String.Format(sql, DefineID));
            return Json(dt);
        }

        public JsonResult AddProgressNode(string DefineID)
        {
            var dic = new Dictionary<string, object>();
            Action action = () =>
            {
                dic.SetValue("ID", FormulaHelper.CreateGuid());
                dic.SetValue("Name", "新建进度节点");
                dic.SetValue("Code", "");
                dic.SetValue("Scale", 0);
                var maxSortIndex = Convert.ToDecimal(this.SQLDB.ExecuteScalar(
                    String.Format("select isnull(max(SortIndex),0) from S_EP_DefineProgress_ProgressNode with(nolock) where ProgressDefineID='{0}'", DefineID)));
                var sortIndex = maxSortIndex + 1;
                var defaultTotalScale = Convert.ToDecimal(this.SQLDB.ExecuteScalar(
                    String.Format("select isnull(sum(Scale),0) from S_EP_DefineProgress_ProgressNode with(nolock) where ProgressDefineID='{0}' and SortIndex<{1}", DefineID, sortIndex)));
                dic.SetValue("TotalScale", defaultTotalScale);
                dic.SetValue("SortIndex", sortIndex);
                dic.SetValue("ProgressDefineID", DefineID);
                dic.SetValue("IsIncomeNode", false.ToString().ToLower());
                dic.InsertDB(this.SQLDB, "S_EP_DefineProgress_ProgressNode", dic.GetValue("ID"));
            };
            this.ExecuteAction(action);
            return Json(dic);
        }

        public JsonResult SaveProgressNode(string DefineID, string ListData)
        {
            Action action = () =>
            {
                var list = JsonHelper.ToList(ListData);
                var scaleList = list.Select(c => c.GetValue("Scale")).ToList();
                var sumScale = 0m;
                foreach (var scale in scaleList)
                {
                    if (String.IsNullOrEmpty(scale))
                        continue;
                    sumScale += Convert.ToDecimal(scale);
                }
                if (sumScale > 100)
                {
                    throw new Formula.Exceptions.BusinessValidationException("节点进度合计不能大于100");
                }
                foreach (var item in list)
                {
                    item.UpdateDB(this.SQLDB, "S_EP_DefineProgress_ProgressNode", item.GetValue("ID"));
                }
                var updateSql = String.Format(@"update S_EP_DefineProgress_ProgressNode  
set TotalScale = (select isnull(sum(Scale),0) from S_EP_DefineProgress_ProgressNode A with(nolock) 
where ProgressDefineID='{0}' and A.SortIndex<=S_EP_DefineProgress_ProgressNode.SortIndex)
where ProgressDefineID='{0}' ", DefineID);
                this.SQLDB.ExecuteNonQuery(updateSql);
            };
            this.ExecuteAction(action);
            return Json("");
        }

        public JsonResult DelProgressNode(string ListIDs, string DefineID)
        {
            var IDList = ListIDs.Split(',');
            Action action = () =>
            {
                foreach (var ID in IDList)
                {
                    string sql = "delete from S_EP_DefineProgress_ProgressNode where ID='{0}'";
                    this.SQLDB.ExecuteNonQuery(String.Format(sql, ID));
                }
                var updateSql = String.Format(@"update S_EP_DefineProgress_ProgressNode  
set TotalScale = (select isnull(sum(Scale),0) from S_EP_DefineProgress_ProgressNode A with(nolock) 
where ProgressDefineID='{0}' and A.SortIndex<=S_EP_DefineProgress_ProgressNode.SortIndex)
where ProgressDefineID='{0}' ", DefineID);
                this.SQLDB.ExecuteNonQuery(updateSql);
            };
            this.ExecuteAction(action);
            return Json("");
        }

        public override JsonResult MoveNode(string sourceID, string targetID, string dragAction)
        {
            string tableName = "S_EP_DefineProgress_ProgressNode";
            var sourceNode = this.GetDataDicByID(tableName, sourceID);
            if (sourceNode == null) throw new Formula.Exceptions.BusinessValidationException("没有找到指定的节点，无法移动");
            var target = this.GetDataDicByID(tableName, targetID);
            if (target == null) throw new Formula.Exceptions.BusinessValidationException("没有找到指定的目标节点，无法移动");

            Action action = () =>
            {
                if (dragAction.ToLower() == "before")
                {
                    string sql = "update " + tableName + " set SortIndex=SortIndex-1 where ProgressDefineID='{0}' and SortIndex<{1}";
                    this.SQLDB.ExecuteNonQuery(String.Format(sql, target.GetValue("ProgressDefineID"), target.GetValue("SortIndex")));
                    var sortIndex = String.IsNullOrEmpty(target.GetValue("SortIndex")) ? 0 : Convert.ToDecimal(target.GetValue("SortIndex"));
                    sourceNode.SetValue("SortIndex", sortIndex - 1);
                    sourceNode.UpdateDB(this.SQLDB, tableName, sourceNode.GetValue("ID"));
                }
                else if (dragAction.ToLower() == "after")
                {
                    string sql = "update " + tableName + " set SortIndex=SortIndex-1 where ProgressDefineID='{0}' and SortIndex>{1}";
                    this.SQLDB.ExecuteNonQuery(String.Format(sql, target.GetValue("ProgressDefineID"), target.GetValue("SortIndex")));
                    var sortIndex = String.IsNullOrEmpty(target.GetValue("SortIndex")) ? 0 : Convert.ToDecimal(target.GetValue("SortIndex"));
                    sourceNode.SetValue("SortIndex", sortIndex + 1);
                    sourceNode.UpdateDB(this.SQLDB, tableName, sourceNode.GetValue("ID"));
                }
            };
            this.ExecuteAction(action);
            return Json(sourceNode);
        }

        public JsonResult GetEvidenceDefineList(QueryBuilder qb, string ProgressNodeID)
        {

            string sql = "select * from S_EP_DefineProgress_ProgressNode_EvidenceDefine with(nolock) where ProgressNodeID='" + ProgressNodeID + "'";
            return Json(this.SQLDB.ExecuteDataTable(sql, qb));
        }

        public JsonResult SaveEvidenceDefine(string ListData, string ProgressNodeID)
        {
            var progressNodeDic = this.GetDataDicByID("S_EP_DefineProgress_ProgressNode", ProgressNodeID);
            if (progressNodeDic == null) throw new Formula.Exceptions.BusinessValidationException("没有找到进度定义节点，无法保存凭证定义");
            var list = JsonHelper.ToList(ListData);
            string sql = "select ID from S_EP_DefineProgress_ProgressNode_EvidenceDefine with(nolock) where ProgressNodeID='" + ProgressNodeID + "'";
            var removeIDList = this.SQLDB.ExecuteDataTable(sql).AsEnumerable().Select(c => c["ID"].ToString()).ToList();
            Action action = () =>
            {
                foreach (var item in list)
                {
                    if (!String.IsNullOrEmpty(item.GetValue("ID")))
                    {
                        var id = removeIDList.FirstOrDefault(c => c == item.GetValue("ID"));
                        if (!String.IsNullOrEmpty(id))
                            removeIDList.Remove(id);
                        item.UpdateDB(this.SQLDB, "S_EP_DefineProgress_ProgressNode_EvidenceDefine", item.GetValue("ID"));
                    }
                    else
                    {
                        item.SetValue("ID", FormulaHelper.CreateGuid());
                        item.SetValue("ProgressNodeID", ProgressNodeID);
                        item.SetValue("DefineID", progressNodeDic.GetValue("ProgressDefineID"));
                        item.InsertDB(this.SQLDB, "S_EP_DefineProgress_ProgressNode_EvidenceDefine", item.GetValue("ID"));
                    }
                }
                var removeIDs = String.Join(",", removeIDList);
                sql =String.Format(@"delete from S_EP_DefineProgress_ProgressNode_EvidenceDefine where ProgressNodeID='{0}' and ID in ('" 
                    + removeIDs.Replace(",","','")+ "')",ProgressNodeID);
                this.SQLDB.ExecuteNonQuery(sql);
            };
            this.ExecuteAction(action);
            return Json("");
        }
    }
}
