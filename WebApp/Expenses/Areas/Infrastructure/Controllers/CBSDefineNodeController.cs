using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
    public class CBSDefineNodeController : InstrasController
    {
        public ActionResult NodeSettings()
        {
            string defineID = this.GetQueryString("DefineID");
            var define = this.GetDataDicByID("S_EP_DefineCBSInfo", defineID);
            if (define == null)
                throw new Exception("没有找到核算项目定义对象，无法设置");
            ViewBag.AutoCreateMethod = define.GetValue("AutoCreateMethod");
            return View();
        }

        protected override string TableName
        {
            get
            {
                return "S_EP_DefineCBSNode";
            }
        }

        public JsonResult AddNode(string NodeID, string AddMode, string DefineID)
        {
            var result = new Dictionary<string, object>();
            Action action = () =>
            {
                if (String.IsNullOrEmpty(NodeID))
                {
                    //默认增加根节点
                    result.SetValue("ID", FormulaHelper.CreateGuid());
                    result.SetValue("Name", "新建节点");
                    result.SetValue("FullID", result.GetValue("ID"));
                    var sql = "select isnull(max(sortindex),0) from {0} with(nolock) where (ParentID='' or ParentID is null)";
                    var maxSortIndex = this.SQLDB.ExecuteScalar(String.Format(sql, this.TableName));
                    var defineSql = "select top 1 Type from S_EP_DefineCBSInfo where id = '{0}'";
                    var cbsType = this.SQLDB.ExecuteScalar(String.Format(defineSql, DefineID));
                    result.SetValue("SortIndex", Convert.ToDecimal(maxSortIndex) + 1);
                    result.SetValue("CBSType", cbsType.ToString());
                    result.SetValue("DefineID", DefineID);
                    result.SetValue("IsDynamic", true.ToString().ToLower());
                    result.InsertDB(this.SQLDB, this.TableName, result.GetValue("ID"));
                }
                else
                {
                    var node = this.GetDataDicByID(this.TableName, NodeID);
                    if (node == null)
                        throw new Formula.Exceptions.BusinessValidationException("没有找到ID为【" + NodeID + "】的科目信息，无法新增科目");
                    if (node.GetValue("NodeType") == CBSNodeType.Subject.ToString())
                    {
                        throw new Formula.Exceptions.BusinessValidationException("科目下不能新增CBS节点");
                    }
                    var subjectNode = new S_EP_DefineCBSNode(node);
                    if (AddMode.ToLower() == "after")
                    {
                        result = subjectNode.AddBrotherNode().ModelDic;
                    }
                    else
                    {
                        result = subjectNode.AddChildNode().ModelDic;
                    }
                }
            };
            this.ExecuteAction(action);
            return Json(result);
        }

        public JsonResult AddSubject(string ParentID, string ListData)
        {
            var result = new List<Dictionary<string, object>>();
            Action action = () =>
            {
                var list = JsonHelper.ToList(ListData);
                list = list.OrderBy(c => c.GetValue("FullID")).ToList();
                var rootNodeDic = this.GetDataDicByID(this.TableName, ParentID);
                if (rootNodeDic == null)
                {
                    throw new Formula.Exceptions.BusinessValidationException("没有找到选中的节点，无法新增科目");
                }
                var dt = this.SQLDB.ExecuteDataTable(String.Format("select ID,SubjectID from S_EP_DefineCBSNode with(nolock) where DefineID='{0}' and ParentID='{1}'"
                    , rootNodeDic.GetValue("DefineID"), ParentID));
                var defineSql = "select top 1 Type from S_EP_DefineCBSInfo where id = '{0}'";
                var cbsType = this.SQLDB.ExecuteScalar(String.Format(defineSql, rootNodeDic.GetValue("DefineID")));

                var addedList = new List<Dictionary<string, object>>();
                foreach (var item in list)
                {
                    if (item.GetValue("NodeType") == "Root") continue;
                    if (dt.Select("SubjectID='" + item.GetValue("ID") + "'").Length > 0)
                        continue;
                    var parentDefineNodeDic = addedList.FirstOrDefault(c => c.GetValue("SubjectID") == item.GetValue("ParentID"));
                    if (parentDefineNodeDic == null)
                    {
                        parentDefineNodeDic = rootNodeDic;
                    }
                    var parentNode = new S_EP_DefineCBSNode(parentDefineNodeDic);
                    var childDic = new Dictionary<string, object>();
                    childDic.SetValue("ID", FormulaHelper.CreateGuid());
                    childDic.SetValue("Name", item.GetValue("Name"));
                    childDic.SetValue("Code", item.GetValue("Code"));
                    childDic.SetValue("NodeType", CBSNodeType.Subject.ToString());
                    childDic.SetValue("CBSType", cbsType.ToString());
                    childDic.SetValue("SubjectType", item.GetValue("SubjectType"));
                    childDic.SetValue("SubjectID", item.GetValue("ID"));
                    childDic.SetValue("SubjectFullID", item.GetValue("FullID"));
                    childDic.SetValue("SubjectCode", item.GetValue("Code"));
                    childDic.SetValue("SubjectFullCode", item.GetValue("FullCode"));
                    childDic.SetValue("AccountNodeType", AccountNodeType.Account.ToString());
                    this._createLogic(childDic);
                    var node = parentNode.AddChildNode(childDic);
                    addedList.Add(node.ModelDic);
                }
            };
            ExecuteAction(action);
            return Json(result);
        }

        public JsonResult SaveNodes(string ListData)
        {
            var list = JsonHelper.ToList(ListData);

            Action action = () =>
            {
                var uiFo = FormulaHelper.CreateFO<Base.Logic.BusinessFacade.UIFO>();
                if (list.Where(c => c.GetValue("IncomeUnit") == true.ToString().ToLower()).Select(c => c.GetValue("NodeType")).Distinct().Count() > 1)
                    throw new Formula.Exceptions.BusinessValidationException("不允许存在多个类型的收入单元");

                if (list.Where(c => c.GetValue("CostUnit") == true.ToString().ToLower()).Select(c => c.GetValue("NodeType")).Distinct().Count() > 1)
                    throw new Formula.Exceptions.BusinessValidationException("不允许存在多个类型的成本单元");

                if (list.Where(c => c.GetValue("ProductionUnit") == true.ToString().ToLower()).Select(c => c.GetValue("NodeType")).Distinct().Count() > 1)
                    throw new Formula.Exceptions.BusinessValidationException("不允许存在多个类型的产值单元");
                foreach (var item in list)
                {
                    this._modifyLogic(item);
                    if (String.IsNullOrEmpty(item.GetValue("Code")))
                    {
                        item.SetValue("Code", uiFo.GetHanZiPinYinString(item.GetValue("Name")));
                    }
                    if (!String.IsNullOrEmpty(item.GetValue("DynamicSettings")))
                    {
                        var synData = "";
                        if (item.GetValue("IsDynamic") == true.ToString().ToLower())
                        {
                            var dynSettings = JsonHelper.ToList(item.GetValue("DynamicSettings"));
                            foreach (var setting in dynSettings)
                            {
                                synData += setting.GetValue("SynDataMethod") + ","; ;
                            }
                            item.SetValue("SynData", synData.TrimEnd(','));
                        }
                    }
                    item.UpdateDB(this.SQLDB, this.TableName, item.GetValue("ID"));
                    this.SQLDB.ExecuteNonQuery(String.Format("UPDATE S_EP_DefineBudget_Subject set Code='{2}' where DefineID='{0}' and CBSDefineID='{1}'",
                        item.GetValue("DefineID"), item.GetValue("ID"), item.GetValue("Code")));
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
                    var node = this.GetDataDicByID(this.TableName, id);
                    if (node == null) continue;
                    var subjectNode = new S_EP_DefineCBSNode(node);
                    subjectNode.Delete();
                }
            };
            ExecuteAction(action);
            return Json("");
        }


    }
}
