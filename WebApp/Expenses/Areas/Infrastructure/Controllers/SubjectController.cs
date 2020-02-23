using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Config;
using Config.Logic;
using Formula;
using Formula.Helper;
using MvcAdapter;
using Expenses.Logic;
using Expenses.Logic.Domain;
using System.Transactions;
using System.Data;

namespace Expenses.Areas.Infrastructure.Controllers
{
    public class SubjectController : InstrasController
    {
        protected override string TableName
        {
            get
            {
                return "S_EP_DefineSubject";
            }
        }

        public ActionResult TreeList()
        {
            var maxLevel = this.SQLDB.ExecuteScalar(String.Format("select isnull(Max(Level),0) from {0} WITH(NOLOCK)", this.TableName));
            ViewBag.ExpandLevel = maxLevel;
            var root = this.SQLDB.ExecuteDataTable(String.Format(" select ID from {0}  WITH(NOLOCK) where ParentID is null or ParentID=''", this.TableName));
            if (root.Rows.Count == 0)
            {
                var rootDic = new Dictionary<string, object>();
                rootDic.SetValue("ID", FormulaHelper.CreateGuid());
                rootDic.SetValue("ParentID", string.Empty);
                rootDic.SetValue("FullID", rootDic.GetValue("ID"));
                rootDic.SetValue("Level", rootDic.GetValue("FullID").Split('.').Length);
                rootDic.SetValue("Name", "根科目");
                rootDic.SetValue("NodeType", "Root");
                rootDic.SetValue("SortIndex", "0");
                this._createLogic(rootDic);
                rootDic.InsertDB(this.SQLDB, this.TableName, rootDic.GetValue("ID"));
            }
            return View();
        }

        public JsonResult AddNode(string NodeID, string AddMode)
        {
            var result = new Dictionary<string, object>();
            Action action = () =>
            {
                var node = this.GetDataDicByID(this.TableName, NodeID);
                if (node == null)
                    throw new Formula.Exceptions.BusinessValidationException("没有找到ID为【" + NodeID + "】的科目信息，无法新增科目");
                var subjectNode = new S_EP_DefineSubject(node);
                if (AddMode.ToLower() == "after")
                {
                    result = subjectNode.AddBrotherNode().ModelDic;
                }
                else
                {
                    result = subjectNode.AddChildNode().ModelDic;
                }
            };
            this.ExecuteAction(action);
            return Json(result);
        }

        public JsonResult SaveNodes(string ListData)
        {
            var list = JsonHelper.ToList(ListData).Where(a => a.GetValue("_state") != "removed");
            var marketHelper = SQLHelper.CreateSqlHelper(ConnEnum.Market);

            {
                //编码重复校验
                //本次变更的编码
                var codeCheckList = list.GroupBy(a => new { key1 = a.GetValue("Code") }).Select(x => new { Code = x.Key.key1 });
                if (codeCheckList.Count() != list.Count())
                {
                    throw new Formula.Exceptions.BusinessValidationException("编码【" + codeCheckList.First().Code + "】重复,无法保存");
                }
            }

            {
                //财务编码重复校验
                var codeCheckList = list.GroupBy(a => new { key1 = a.GetValue("BindFinaceCBSCode") }).Select(x => new { Code = x.Key.key1 });
                if (codeCheckList.Count() != list.Count())
                {
                    throw new Formula.Exceptions.BusinessValidationException("财务编码【" + codeCheckList.First().Code + "】重复,无法保存");
                }
            }

            foreach (var existCode in list)
            {
                {
                    var coutObj = this.SQLDB.ExecuteScalar(string.Format("select count(*) from S_EP_DefineSubject where Code = '{0}' and ID != '{1}'",
                        existCode.GetValue("Code"),  existCode.GetValue("ID")));
                    if (coutObj != null && (int)coutObj > 0)
                    {
                        throw new Formula.Exceptions.BusinessValidationException("编码【" + existCode.GetValue("Code") + "】重复,无法保存");
                    }
                }

                {
                    var coutObj = this.SQLDB.ExecuteScalar(string.Format("select count(*) from S_EP_DefineSubject where BindFinaceCBSCode = '{0}' and ID != '{1}'",
                        existCode.GetValue("BindFinaceCBSCode"), existCode.GetValue("ID")));
                    if (coutObj != null && (int)coutObj > 0)
                    {
                        throw new Formula.Exceptions.BusinessValidationException("财务编码【" + existCode.GetValue("BindFinaceCBSCode") + "】重复,无法保存");
                    }
                }
            }

            ////同数据库数据的编码
            //string codeStr = string.Join("','", codeCheckList);
            //string codeCheckSql = "select count(*) from "

            Action action = () =>
            {
                foreach (var item in list)
                {
                    this._modifyLogic(item);
                    if (!String.IsNullOrEmpty(item.GetValue("ExpenseType")))
                    {
                        this.SQLDB.ExecuteNonQuery(String.Format(@"UPDATE S_EP_DefineSubject SET ExpenseType='{0}',ModifyDate='{1}',ModifyUserID='{2}',ModifyUserName = '{3}'
                                                                 where FullID like '{4}%' and (ExpenseType='' or ExpenseType is null)"
                            , item.GetValue("ExpenseType"), DateTime.Now.ToString("yyyy-MM-dd"), CurrentUserInfo.UserID, CurrentUserInfo.UserName, item.GetValue("FullID")));

                        //根据ExpenseType 查找 S_EP_DefineExpenseType 获取SubjectType
                        var subjectTypeObj = this.SQLDB.ExecuteScalar("select SubjectType from S_EP_DefineExpenseType where Code = '" + item.GetValue("ExpenseType") + "'");
                        if (subjectTypeObj != null && subjectTypeObj != DBNull.Value)
                            item.SetValue("SubjectType", subjectTypeObj);
                    }
                    item.UpdateDB(this.SQLDB, this.TableName, item.GetValue("ID"));
                }
                #region 重新刷新FULLCODE
                var dt = this.SQLDB.ExecuteDataTable("SELECT * FROM S_EP_DefineSubject WITH(NOLOCK) ORDER BY FULLID");
                foreach (DataRow row in dt.Rows)
                {
                    if (row["FullID"].ToString() == row["ID"].ToString())
                    {
                        //根节点 FULLCODE 等于CODE本身
                        this.SQLDB.ExecuteNonQuery(String.Format("UPDATE S_EP_DefineSubject SET FULLCODE='{0}' WHERE ID='{1}'", row["Code"], row["ID"]));
                        this.SQLDB.ExecuteNonQuery(String.Format("UPDATE S_EP_DefineCBSNode SET SubjectCode='{0}',SubjectFullCode='{1}' WHERE SubjectID='{2}'", row["Code"].ToString(),
                               row["Code"], row["ID"]));
                    }
                    else
                    {
                        var ancestors = dt.Select("'" + row["FULLID"].ToString() + "' LIKE FULLID+'%'", "FULLID");
                        var fullCode = "";
                        foreach (DataRow ancestor in ancestors)
                        {
                            fullCode += ancestor["Code"].ToString() + ".";
                        }
                        this.SQLDB.ExecuteNonQuery(String.Format("UPDATE S_EP_DefineSubject SET FULLCODE='{0}' WHERE ID='{1}'", fullCode.TrimEnd('.'), row["ID"]));
                        this.SQLDB.ExecuteNonQuery(String.Format("UPDATE S_EP_DefineCBSNode SET SubjectCode='{0}',SubjectFullCode='{1}' WHERE SubjectID='{2}'", row["Code"].ToString(),
                            fullCode.TrimEnd('.'), row["ID"]));
                    }
                }
                #endregion

                this.SQLDB.ExecuteNonQuery(@"UPDATE S_EP_DefineCBSNode
SET SubjectCode=(SELECT CODE FROM S_EP_DefineSubject WHERE ID=S_EP_DefineCBSNode.SubjectID),
Code = (SELECT CODE FROM S_EP_DefineSubject WHERE ID=S_EP_DefineCBSNode.SubjectID),
SubjectType=(SELECT SubjectType FROM S_EP_DefineSubject WHERE ID=S_EP_DefineCBSNode.SubjectID),
ExpenseType=(SELECT ExpenseType FROM S_EP_DefineSubject WHERE ID=S_EP_DefineCBSNode.SubjectID)
WHERE NODETYPE='Subject'
and SubjectID in (select ID from S_EP_DefineSubject) ");

                this.SQLDB.ExecuteNonQuery(@"UPDATE S_EP_DefineBudget_Subject
SET Code = (SELECT CODE FROM S_EP_DefineSubject WHERE ID=S_EP_DefineBudget_Subject.SubjectID),
SubjectType=(SELECT SubjectType FROM S_EP_DefineSubject WHERE ID=S_EP_DefineBudget_Subject.SubjectID)
WHERE NODETYPE='Subject'
and SubjectID in (select ID from S_EP_DefineSubject) ");

                this.SQLDB.ExecuteNonQuery(string.Format(@"UPDATE {0}..S_EP_CBSNode
SET SubjectCode=(SELECT CODE FROM S_EP_DefineSubject WHERE ID={0}..S_EP_CBSNode.SubjectID),
Code = (SELECT CODE FROM S_EP_DefineSubject WHERE ID={0}..S_EP_CBSNode.SubjectID),
SubjectType=(SELECT SubjectType FROM S_EP_DefineSubject WHERE ID={0}..S_EP_CBSNode.SubjectID),
ExpenseType=(SELECT ExpenseType FROM S_EP_DefineSubject WHERE ID={0}..S_EP_CBSNode.SubjectID)
WHERE NODETYPE='Subject'
and SubjectID in (select ID from S_EP_DefineSubject)", marketHelper.DbName));

                this.SQLDB.ExecuteNonQuery(string.Format(@"UPDATE {0}..S_EP_BudgetVersion_Detail
SET SubjectCode=(SELECT CODE FROM S_EP_DefineSubject WHERE ID={0}..S_EP_BudgetVersion_Detail.SubjectID),
Code = (SELECT CODE FROM S_EP_DefineSubject WHERE ID={0}..S_EP_BudgetVersion_Detail.SubjectID),
SubjectType=(SELECT SubjectType FROM S_EP_DefineSubject WHERE ID={0}..S_EP_BudgetVersion_Detail.SubjectID)
WHERE NODETYPE='Subject'
and SubjectID in (select ID from S_EP_DefineSubject)", marketHelper.DbName));

            };
            this.ExecuteAction(action);
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
                    var subjectNode = new S_EP_DefineSubject(node);
                    subjectNode.Delete();
                }
            };
            this.ExecuteAction(action);
            return Json("");
        }

    }
}
