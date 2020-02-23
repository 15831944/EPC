using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Transactions;
using System.Web.Mvc;

using Config;
using Config.Logic;
using Formula.Helper;
using Formula;
using MvcAdapter;
using Workflow.Logic.Domain;
using Workflow.Logic;
using Expenses.Logic.Domain;
using System.Data;

namespace Expenses
{
    public class ExpensesController : Controller
    {
        #region 基本属性
        protected virtual string TableName
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        SQLHelper sqlDB;
        protected virtual SQLHelper SQLDB
        {
            get
            {
                if (sqlDB == null)
                    sqlDB = SQLHelper.CreateSqlHelper(ConnEnum.Market);
                return sqlDB;
            }
        }

        SQLHelper instraDB;
        public virtual SQLHelper InstraDB
        {
            get
            {
                if (instraDB == null)
                    instraDB = SQLHelper.CreateSqlHelper(ConnEnum.InfrasBaseConfig);
                return instraDB;
            }
        }

        UserInfo _userInfo;
        protected UserInfo CurrentUserInfo
        {
            get
            {
                if (_userInfo == null)
                    _userInfo = FormulaHelper.GetUserInfo();
                return _userInfo;
            }
        }

        private Dictionary<string, object> _formDic;
        private Dictionary<string, object> formDic
        {
            get
            {
                if (_formDic == null)
                {
                    if (!string.IsNullOrEmpty(Request["FormData"]))
                        _formDic = JsonHelper.ToObject<Dictionary<string, object>>(Request["FormData"]);
                    else
                        _formDic = new Dictionary<string, object>();
                }
                return _formDic;
            }
        }


        #endregion

        #region MVC基本方法
        protected override void HandleUnknownAction(string actionName)
        {
            if (Request.HttpMethod == "POST")
                throw new Exception("没有Action:" + actionName);

            // 搜索文件是否存在
            var filePath = "";
            if (RouteData.DataTokens["area"] != null)
                filePath = string.Format("~/Areas/{2}/Views/{1}/{0}.cshtml", actionName, RouteData.Values["controller"], RouteData.DataTokens["area"]);
            else
                filePath = string.Format("~/Views/{1}/{0}.cshtml", actionName, RouteData.Values["controller"]);
            if (System.IO.File.Exists(Server.MapPath(filePath)))
            {
                View(filePath).ExecuteResult(ControllerContext);
            }
            else
            {
                base.HandleUnknownAction(actionName);
            }
        }

        protected override JsonResult Json(object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            NewtonJsonResult result = new NewtonJsonResult() { Data = data, ContentType = contentType, ContentEncoding = contentEncoding, JsonRequestBehavior = behavior };
            return result;
        }

        protected override JsonResult Json(object data, string contentType, Encoding contentEncoding)
        {
            NewtonJsonResult result = new NewtonJsonResult() { Data = data, ContentType = contentType, ContentEncoding = contentEncoding };
            return result;
        }
        #endregion

        #region 扩展虚方法
        protected virtual void BeforeSave(Dictionary<string, object> dic, bool isNew)
        {

        }

        protected virtual void AfterSave(Dictionary<string, object> dic, bool isNew)
        {

        }

        protected virtual void BeforeDelete(string[] ids)
        {

        }

        protected virtual void AfterDelete(string[] ids)
        {

        }

        protected virtual void BeforeGetList(QueryBuilder qb)
        {

        }

        protected virtual void BeforeGetTree(QueryBuilder qb)
        {

        }

        protected virtual void AfterGetTree(DataTable resultDt, QueryBuilder qb)
        {

        }

        protected virtual void AfterGetList(DataTable resultDt, GridData result, QueryBuilder qb)
        {

        }

        protected virtual void BeforeGetData()
        {

        }

        protected virtual void AfterGetData(Dictionary<string, object> dic, bool isNew)
        {

        }
        #endregion

        public virtual JsonResult GetModel(string id)
        {
            var dic = new Dictionary<string, object>();
            bool isNew = true;
            if (!String.IsNullOrEmpty(id))
            {
                var sql = String.Format("select * from {0} where ID='{1}'", this.TableName, id);
                var dt = this.SQLDB.ExecuteDataTable(sql);
                if (dt.Rows.Count > 0)
                {
                    dic = FormulaHelper.DataRowToDic(dt.Rows[0]);
                    isNew = false;
                }
                else
                {
                    throw new Formula.Exceptions.BusinessValidationException(String.Format("数据表【{0}】中没有找到ID为【{1}】的记录，无法读取数据", this.TableName, id));
                }
            }
            AfterGetData(dic, isNew);
            return Json(dic);
        }

        public virtual JsonResult GetList(QueryBuilder qb)
        {
            string sql = "select * from {0}  WITH(NOLOCK)";
            DataTable dtTmpl = this.SQLDB.ExecuteDataTable(String.Format("select * from {0}  WITH(NOLOCK) where 1!=1", this.TableName));
            foreach (string key in Request.QueryString.Keys)
            {
                if (string.IsNullOrEmpty(key))
                    continue;
                if ("ID,FullID,FULLID,TmplCode,IsPreView,_winid,_t".Split(',').Contains(key) || key.StartsWith("$"))
                    continue;
                if (dtTmpl.Columns.Contains(key))
                    qb.Add(key, QueryMethod.In, Request[key]); ;
            }

            this.BeforeGetList(qb);
            var dt = this.SQLDB.ExecuteDataTable(String.Format(sql, this.TableName), qb);
            var gridData = new GridData(dt);
            gridData.total = qb.TotolCount;
            this.AfterGetList(dt, gridData, qb);
            return Json(gridData);
        }

        public virtual JsonResult GetTree(QueryBuilder qb)
        {
            string sql = "select * from {0}  WITH(NOLOCK) ";
            qb.PageSize = 0;

            DataTable dtTmpl = this.SQLDB.ExecuteDataTable(String.Format("select * from {0}  WITH(NOLOCK) where 1!=1", this.TableName));
            foreach (string key in Request.QueryString.Keys)
            {
                if (string.IsNullOrEmpty(key))
                    continue;
                if ("ID,FullID,FULLID,TmplCode,IsPreView,_winid,_t".Split(',').Contains(key) || key.StartsWith("$"))
                    continue;
                if (dtTmpl.Columns.Contains(key))
                    qb.Add(key, QueryMethod.In, Request[key]); ;
            }

            this.BeforeGetTree(qb);
            var dt = this.SQLDB.ExecuteDataTable(String.Format(sql, this.TableName), qb);
            this.AfterGetTree(dt, qb);
            return Json(dt);
        }

        public virtual JsonResult Save()
        {
            var dic = new Dictionary<string, object>();
            Action action = () =>
            {
                var formData = this.Request["FormData"];
                if (!String.IsNullOrEmpty(formData))
                {
                    bool isNew = false;
                    dic = JsonHelper.ToObject(formData);
                    var id = dic.GetValue("ID");
                    var dt = this.SQLDB.ExecuteDataTable(String.Format("select * from {0} where ID='{1}'", this.TableName, id));
                    if (dt.Rows.Count == 0)
                    {
                        //新增
                        if (string.IsNullOrEmpty(id))
                        {
                            id = FormulaHelper.CreateGuid();
                            dic.SetValue("ID", id);
                        }
                        isNew = true;
                        _createLogic(dic);
                        _setSortIndexDefaultValue(dic);
                        this.BeforeSave(dic, isNew);
                        dic.InsertDB(this.SQLDB, this.TableName, id);
                        this.AfterSave(dic, isNew);
                    }
                    else
                    {
                        //修改
                        _modifyLogic(dic);
                        this.BeforeSave(dic, isNew);
                        dic.UpdateDB(this.SQLDB, this.TableName, id);
                        this.AfterSave(dic, isNew);
                    }
                }
            };
            this.ExecuteAction(action);
            return Json(dic);
        }

        public virtual JsonResult Delete()
        {
            Action action = () =>
            {
                string Ids = this.Request["ListIDs"];
                if (!string.IsNullOrEmpty(Ids))
                {
                    var sb = new StringBuilder();
                    foreach (var Id in Ids.Split(','))
                    {
                        sb.Append(String.Format("delete from {0} where ID='{1}';", this.TableName, Id));
                    }
                    this.BeforeDelete(Ids.Split(','));
                    this.SQLDB.ExecuteNonQuery(sb.ToString());
                    this.AfterDelete(Ids.Split(','));
                }
            };
            this.ExecuteAction(action);
            return Json("");
        }

        public virtual JsonResult MoveNode(string sourceID, string targetID, string dragAction)
        {
            var result = new Dictionary<string, object>();
            Action action = () =>
            {
                result = this.MoveTreeNode(this.TableName, sourceID, targetID, dragAction);
            };
            this.ExecuteAction(action);
            return Json(result);
        }

        protected Dictionary<string, object> GetDataDicByID(string tableName, string id, bool withNolock = false)
        {
            string sql = withNolock ? String.Format("select * from {0} {2} where ID='{1}'", tableName, id, "with(nolock)")
                : String.Format("select * from {0} {2} where ID='{1}'", tableName, id, "");
            var dt = this.SQLDB.ExecuteDataTable(sql);
            if (dt.Rows.Count == 0)
            {
                return null;
            }
            else
            {
                return FormulaHelper.DataRowToDic(dt.Rows[0]);
            }
        }


        protected Dictionary<string, object> MoveTreeNode(string tableName, string sourceID, string targetID, string dragAction)
        {
            var sourceNode = this.GetDataDicByID(tableName, sourceID);
            if (sourceNode == null) throw new Formula.Exceptions.BusinessValidationException("没有找到指定的节点，无法移动");
            var target = this.GetDataDicByID(tableName, targetID);
            if (target == null) throw new Formula.Exceptions.BusinessValidationException("没有找到指定的目标节点，无法移动");
            Action action = () =>
            {
                if (dragAction.ToLower() == "add")
                {
                    sourceNode.SetValue("ParentID", target.GetValue("ID"));
                    var sourceFullID = sourceNode.GetValue("FullID");
                    var newFullID = target.GetValue("FullID") + "." + sourceNode.GetValue("ID");
                    this.SQLDB.ExecuteNonQuery(String.Format("update {0} set FullID=Replace(FullID,'{1}','{2}') where FullID like '{1}%' and ID !='{3}'", tableName,
                        sourceFullID, newFullID, sourceNode.GetValue("ID")));
                    sourceNode.SetValue("FullID", target.GetValue("FullID") + "." + sourceNode.GetValue("ID"));
                    sourceNode.SetValue("Level", sourceNode.GetValue("FullID").Split('.').Length);
                    var maxSortIndex = Convert.ToDecimal(this.SQLDB.ExecuteScalar(
                        String.Format("select isnull(max(sortindex),0) from {1} with(nolock) where ParentID='{0}'"
                        , target.GetValue("ID"), tableName)));
                    sourceNode.SetValue("SortIndex", maxSortIndex + 1);
                    sourceNode.UpdateDB(this.SQLDB, tableName, sourceNode.GetValue("ID"));
                }
                else if (dragAction.ToLower() == "before")
                {
                    if (!String.IsNullOrEmpty(target.GetValue("ParentID")))
                    {
                        string sql = "update " + tableName + " set SortIndex=SortIndex-1 where ParentID='{0}' and SortIndex<{1}";
                        this.SQLDB.ExecuteNonQuery(String.Format(sql, target.GetValue("ParentID"), target.GetValue("SortIndex")));
                    }
                    else
                    {
                        string sql = "update " + tableName + " set SortIndex=SortIndex-1 where (ParentID='' or ParentID is null) and SortIndex<{0}";
                        this.SQLDB.ExecuteNonQuery(String.Format(sql, target.GetValue("SortIndex")));
                    }
                    var sortIndex = String.IsNullOrEmpty(target.GetValue("SortIndex")) ? 0 : Convert.ToDecimal(target.GetValue("SortIndex"));
                    sourceNode.SetValue("SortIndex", sortIndex - 1);
                    sourceNode.SetValue("ParentID", target.GetValue("ParentID"));
                    var sourceFullID = sourceNode.GetValue("FullID");
                    if (!String.IsNullOrEmpty(sourceFullID))
                    {
                        var newFullID = target.GetValue("FullID").Replace("." + target.GetValue("ID"), "") + "." + sourceNode.GetValue("ID");
                        this.SQLDB.ExecuteNonQuery(String.Format("update {0} set FullID=Replace(FullID,'{1}','{2}') where FullID like '{1}%' and ID !='{3}'", tableName,
                            sourceFullID, newFullID, sourceNode.GetValue("ID")));
                        sourceNode.SetValue("FullID", newFullID);
                    }
                    sourceNode.UpdateDB(this.SQLDB, tableName, sourceNode.GetValue("ID"));
                }
                else if (dragAction.ToLower() == "after")
                {
                    if (!String.IsNullOrEmpty(target.GetValue("ParentID")))
                    {
                        string sql = "update " + tableName + " set SortIndex=SortIndex-1 where ParentID='{0}' and SortIndex>{1}";
                        this.SQLDB.ExecuteNonQuery(String.Format(sql, target.GetValue("ParentID"), target.GetValue("SortIndex")));
                    }
                    else
                    {
                        string sql = "update " + tableName + " set SortIndex=SortIndex-1 where (ParentID='' or ParentID is null) and SortIndex<{0}";
                        this.SQLDB.ExecuteNonQuery(String.Format(sql, target.GetValue("SortIndex")));
                    }
                    var sortIndex = String.IsNullOrEmpty(target.GetValue("SortIndex")) ? 0 : Convert.ToDecimal(target.GetValue("SortIndex"));
                    sourceNode.SetValue("SortIndex", sortIndex + 1);
                    sourceNode.SetValue("ParentID", target.GetValue("ParentID"));

                    var sourceFullID = sourceNode.GetValue("FullID");
                    if (!String.IsNullOrEmpty(sourceFullID))
                    {
                        var newFullID = target.GetValue("FullID").Replace("." + target.GetValue("ID"), "") + "." + sourceNode.GetValue("ID");
                        this.SQLDB.ExecuteNonQuery(String.Format("update {0} set FullID=Replace(FullID,'{1}','{2}') where FullID like '{1}%' and ID !='{3}'", tableName,
                            sourceFullID, newFullID, sourceNode.GetValue("ID")));
                        sourceNode.SetValue("FullID", newFullID);
                    }
                    sourceNode.UpdateDB(this.SQLDB, tableName, sourceNode.GetValue("ID"));
                }
            };
            ExecuteAction(action);
            return sourceNode;
        }

        protected void ExecuteAction(Action action)
        {
            if (System.Configuration.ConfigurationManager.AppSettings["UseMsdtc"].ToLower() == "true")
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    action();
                    ts.Complete();
                }
            }
            else
            {
                action();
            }
        }

        protected string GetQueryString(string key)
        {
            string value = Request.QueryString[key];
            if (string.IsNullOrEmpty(value))
                value = Request.Form[key];
            if (string.IsNullOrEmpty(value))
            {
                if (formDic.ContainsKey(key))
                    value = formDic[key].ToString();
            }

            if (value != null)
                return value;
            else
                return "";
        }

        protected void _createLogic(Dictionary<string, object> dic)
        {
            dic.SetValue("CreateDate", DateTime.Now);
            dic.SetValue("CreateTime", DateTime.Now);
            dic.SetValue("CreateUser", this.CurrentUserInfo.UserName);
            dic.SetValue("CreateUserName", this.CurrentUserInfo.UserName);
            dic.SetValue("CreateUserID", this.CurrentUserInfo.UserID);
            dic.SetValue("OrgID", this.CurrentUserInfo.UserOrgID);
            dic.SetValue("OrgName", this.CurrentUserInfo.UserOrgName);
            dic.SetValue("Org", this.CurrentUserInfo.UserOrgID);
            dic.SetValue("CompanyID", this.CurrentUserInfo.UserCompanyID);
            dic.SetValue("FlowPhase", "Start");
            dic.SetValue("ModifyDate", DateTime.Now);
            dic.SetValue("ModifyTime", DateTime.Now);
            dic.SetValue("ModifyUser", this.CurrentUserInfo.UserName);
            dic.SetValue("ModifyUserName", this.CurrentUserInfo.UserName);
            dic.SetValue("ModifyUserID", this.CurrentUserInfo.UserID);
            dic.SetValue("CompanyID", this.CurrentUserInfo.UserCompanyID);
        }

        protected void _modifyLogic(Dictionary<string, object> dic)
        {
            dic.SetValue("ModifyDate", DateTime.Now);
            dic.SetValue("ModifyTime", DateTime.Now);
            dic.SetValue("ModifyUser", this.CurrentUserInfo.UserName);
            dic.SetValue("ModifyUserName", this.CurrentUserInfo.UserName);
            dic.SetValue("ModifyUserID", this.CurrentUserInfo.UserID);
        }

        protected void _setSortIndexDefaultValue(Dictionary<string, object> dic)
        {
            dic.SetValue("SortIndex", 0);
        }
    }
}