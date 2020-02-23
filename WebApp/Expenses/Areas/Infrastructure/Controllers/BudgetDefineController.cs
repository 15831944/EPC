using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Data;
using System.Transactions;
using Config;
using Config.Logic;
using Formula;
using Formula.Helper;
using MvcAdapter;
using Expenses.Logic;
using Expenses.Logic.Domain;

namespace Expenses.Areas.Infrastructure.Controllers
{
    public class BudgetDefineController : InstrasController
    {
        protected override string TableName
        {
            get
            {
                return "S_EP_DefineBudget";
            }
        }

        public JsonResult AddBudgetDefine(string ListData, string DefineID)
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
                    dic.SetValue("IsDefault", false.ToString().ToLower());
                    dic.InsertDB(this.SQLDB, this.TableName, dic.GetValue("ID"));
                }
            };
            this.ExecuteAction(action);
            return Json("");
        }

        public JsonResult RemoveBudgetDefine(string ListIDs)
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

        public JsonResult AddNode(string ParentNodeID, string Nodes, string DefineID, string DefineBudgetID, string DefineUnitNodeID)
        {
            var result = new Dictionary<string, object>();
            Action action = () =>
            {
                var node = this.GetDataDicByID("S_EP_DefineBudget_Subject", ParentNodeID);
                var list = JsonHelper.ToList(Nodes);
                var nodeList = list.OrderBy(c => c.GetValue("FullID")).ToList();
                var dt = this.SQLDB.ExecuteDataTable(
                    String.Format("select * from S_EP_DefineBudget_Subject with(nolock) where DefineBudgetID='{0}'", DefineBudgetID));
                var existNodeList = FormulaHelper.DataTableToListDic(dt);
                var addedList = new List<Dictionary<string, object>>();
                if (node == null)
                {
                    foreach (var item in nodeList)
                    {
                        //如果item是在某个最近的预算单元下的子节点，则在该预算单元下增加item
                        if (existNodeList.Count(c => c.GetValue("CBSDefineID") == item.GetValue("ID")) > 0)
                            continue;
                        var parentNode = existNodeList.FirstOrDefault(c => c.GetValue("CBSDefineID") == item.GetValue("ParentID"));
                        var budgetDefine = new Dictionary<string, object>();
                        budgetDefine.SetValue("ID", FormulaHelper.CreateGuid());
                        if (parentNode == null)
                        {
                            budgetDefine.SetValue("ParentID", "");
                            budgetDefine.SetValue("FullID", budgetDefine.GetValue("ID"));
                        }
                        else
                        {
                            budgetDefine.SetValue("ParentID", parentNode.GetValue("ID"));
                            budgetDefine.SetValue("FullID", parentNode.GetValue("FullID") + "." + budgetDefine.GetValue("ID"));
                        }

                        budgetDefine.SetValue("Name", item.GetValue("Name"));
                        budgetDefine.SetValue("Code", item.GetValue("Code"));
                        budgetDefine.SetValue("CBSDefineID", item.GetValue("ID"));
                        budgetDefine.SetValue("CBSDefineFullID", item.GetValue("FullID"));
                        budgetDefine.SetValue("DefineUnitNodeID", DefineUnitNodeID);
                        budgetDefine.SetValue("SubjectID", item.GetValue("SubjectID"));
                        budgetDefine.SetValue("SubjectType", item.GetValue("SubjectType"));
                        budgetDefine.SetValue("SubjectFullID", item.GetValue("SubjectFullID"));
                        budgetDefine.SetValue("SubjectFullCode", item.GetValue("SubjectFullCode"));
                        budgetDefine.SetValue("NodeType", item.GetValue("NodeType"));
                        budgetDefine.SetValue("CBSType", item.GetValue("CBSType"));
                        budgetDefine.SetValue("SortIndex", item.GetValue("SortIndex"));
                        budgetDefine.SetValue("DefineBudgetID", DefineBudgetID);
                        budgetDefine.SetValue("DefineID", DefineID);
                        budgetDefine.SetValue("InputType", "Input");
                        budgetDefine.SetValue("AllowAddChild", true.ToString());
                        budgetDefine.SetValue("AllowEdit", true.ToString());
                        budgetDefine.SetValue("AllowDelete", false.ToString());
                        budgetDefine.SetValue("Constraints", false.ToString());
                        budgetDefine.InsertDB(this.SQLDB, "S_EP_DefineBudget_Subject", budgetDefine.GetValue("ID"));
                        existNodeList.Add(budgetDefine);
                    }
                }
                else
                {
                    foreach (var item in nodeList)
                    {
                        if (existNodeList.Count(c => c.GetValue("CBSDefineID") == item.GetValue("ID")) > 0)
                            continue;
                        var parentNode = addedList.FirstOrDefault(c => c.GetValue("CBSDefineID") == item.GetValue("ParentID"));
                        if (parentNode == null)
                            parentNode = node;
                        var budgetDefine = new Dictionary<string, object>();
                        budgetDefine.SetValue("ID", FormulaHelper.CreateGuid());
                        if (parentNode == null)
                        {
                            budgetDefine.SetValue("ParentID", "");
                            budgetDefine.SetValue("FullID", budgetDefine.GetValue("ID"));
                        }
                        else
                        {
                            budgetDefine.SetValue("ParentID", parentNode.GetValue("ID"));
                            budgetDefine.SetValue("FullID", parentNode.GetValue("FullID") + "." + budgetDefine.GetValue("ID"));
                        }
                        budgetDefine.SetValue("Name", item.GetValue("Name"));
                        budgetDefine.SetValue("Code", item.GetValue("Code"));
                        budgetDefine.SetValue("CBSDefineID", item.GetValue("ID"));
                        budgetDefine.SetValue("CBSDefineFullID", item.GetValue("FullID"));
                        budgetDefine.SetValue("DefineUnitNodeID", DefineUnitNodeID);
                        budgetDefine.SetValue("SubjectID", item.GetValue("SubjectID"));
                        budgetDefine.SetValue("SubjectFullID", item.GetValue("SubjectFullID"));
                        budgetDefine.SetValue("SubjectFullCode", item.GetValue("SubjectFullCode"));
                        budgetDefine.SetValue("NodeType", item.GetValue("NodeType"));
                        budgetDefine.SetValue("CBSType", item.GetValue("CBSType"));
                        budgetDefine.SetValue("SortIndex", item.GetValue("SortIndex"));
                        budgetDefine.SetValue("DefineBudgetID", DefineBudgetID);
                        budgetDefine.SetValue("DefineID", DefineID);
                        budgetDefine.SetValue("AllowAddChild", true.ToString().ToLower());
                        budgetDefine.SetValue("AllowEdit", true.ToString().ToLower());
                        budgetDefine.SetValue("AllowDelete", false.ToString().ToLower());
                        budgetDefine.SetValue("InputType", "Input");
                        budgetDefine.SetValue("Constraints", false.ToString());
                        budgetDefine.InsertDB(this.SQLDB, "S_EP_DefineBudget_Subject", budgetDefine.GetValue("ID"));
                        existNodeList.Add(budgetDefine);
                        addedList.Add(budgetDefine);
                    }
                }
            };
            this.ExecuteAction(action);
            return Json(result);
        }

        public JsonResult SaveNodes(string ListData)
        {
            var list = JsonHelper.ToList(ListData);

            Action action = () =>
            {
                foreach (var item in list)
                {
                    this._modifyLogic(item);
                    item.UpdateDB(this.SQLDB, "S_EP_DefineBudget_Subject", item.GetValue("ID"));
                }
            };
            ExecuteAction(action);
            return Json("");
        }

        public JsonResult DeleteNodes(string Ids)
        {
            Action action = () =>
            {
                var nodeIDs = Ids.Split(',');
                foreach (var id in nodeIDs)
                {
                    var node = this.GetDataDicByID("S_EP_DefineBudget_Subject", id);
                    if (node == null) continue;
                    var subjectNode = new S_EP_DefineBudget_Subject(node);
                    subjectNode.Delete();
                }
            };
            ExecuteAction(action);
            return Json("");
        }

        public JsonResult GetBudgetUnit(string DefineID)
        {
            string sql = String.Format(@"select * from S_EP_DefineBudget WHERE DEFINEID='{0}'", DefineID);
            var dt = this.SQLDB.ExecuteDataTable(sql);
            return Json(dt);
        }

        public JsonResult GetSubjectTree(string DefineBudgetID)
        {
            string sql = String.Format("SELECT * FROM S_EP_DefineBudget_Subject WHERE DefineBudgetID='{0}' ORDER BY SORTINDEX", DefineBudgetID);
            var dt = this.SQLDB.ExecuteDataTable(sql);
            return Json(dt);
        }
    }
}
