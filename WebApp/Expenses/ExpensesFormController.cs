using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Formula;
using Config;
using Config.Logic;
using Workflow.Logic.Domain;
using Workflow.Logic;
using Expenses.Logic.Domain;
using System.Web.Mvc;
using MvcAdapter;
using Base.Logic.Domain;
using Formula.Helper;
using System.Transactions;

namespace Expenses
{
    public class ExpensesFormController : BaseAutoFormController
    {
        public virtual string TableName
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        SQLHelper sqlDB;
        public virtual SQLHelper MarketSQLDB
        {
            get
            {
                if (sqlDB == null)
                    sqlDB = SQLHelper.CreateSqlHelper(ConnEnum.Market);
                return sqlDB;
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

        protected virtual void OnFlowEnd(Dictionary<string, object> data, S_WF_InsTaskExec taskExec, S_WF_InsDefRouting routing)
        { }

        protected Dictionary<string, object> GetDataDicByID(string tableName, string id)
        {
            string sql = "select * from {0} WITH(NOLOCK) where ID='{1}'";
            var dt = this.MarketSQLDB.ExecuteDataTable(String.Format(sql, tableName, id));
            if (dt.Rows.Count == 0)
            {
                return null;
            }
            else
            {
                return FormulaHelper.DataRowToDic(dt.Rows[0]);
            }
        }

        protected void ExecuteAction(Action action)
        {
            if (System.Configuration.ConfigurationManager.AppSettings["UseMsdtc"].ToLower() == "true")
            {
                TransactionOptions tranOp = new TransactionOptions();
                tranOp.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, tranOp))
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



    }

    public class ExpensesFormController<T> : BaseAutoFormController where T : BaseEPModel
    {
        protected virtual string TableName
        {
            get
            {
                return typeof(T).Name;
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

        SQLHelper sqlDB;
        public virtual SQLHelper SQLDB
        {
            get
            {
                if (sqlDB == null)
                    sqlDB = SQLHelper.CreateSqlHelper(ConnEnum.Market);
                return sqlDB;
            }
        }

        SQLHelper instraDB;
        public virtual SQLHelper InfrasDB
        {
            get
            {
                if (instraDB == null)
                    instraDB = SQLHelper.CreateSqlHelper(ConnEnum.InfrasBaseConfig);
                return instraDB;
            }
        }

        protected Dictionary<string, object> GetDataDicByID(string tableName, string id, ConnEnum connName= ConnEnum.Market, bool withNolock = false)
        {
            string sql = withNolock ? String.Format("select * from {0} {2} where ID='{1}'", tableName, id, "with(nolock)")
                : String.Format("select * from {0} {2} where ID='{1}'", tableName, id, "");
            var db = SQLHelper.CreateSqlHelper(connName);
            var dt =db.ExecuteDataTable(sql);
            if (dt.Rows.Count == 0)
            {
                return null;
            }
            else
            {
                return FormulaHelper.DataRowToDic(dt.Rows[0]);
            }
        }



        protected override TEntity GetEntityByID<TEntity>(string ID, bool fromDB = false)
        {
            throw new NotImplementedException("此方法在该基类中被弃用，禁止调用");
        }

        protected virtual void OnFlowEnd(T data, S_WF_InsTaskExec taskExec, S_WF_InsDefRouting routing)
        { }

        public override bool ExecTaskExec(S_WF_InsTaskExec taskExec, S_WF_InsDefRouting routing,
          string nextExecUserIDs, string nextExecUserNames, string nextExecUserIDsGroup,
          string nextExecRoleIDs, string nextExecOrgIDs, string execComment)
        {
            string tmplCode = Request["TmplCode"];
            var formInfo = baseEntities.Set<S_UI_Form>().SingleOrDefault(c => c.Code == tmplCode);

            var sql = String.Format("select * from {0}  WITH(NOLOCK) where ID='{1}'", this.TableName, taskExec.S_WF_InsFlow.FormInstanceID);
            var dt = this.SQLDB.ExecuteDataTable(sql);

            bool flag = false;
            Workflow.Logic.BusinessFacade.FlowFO flowFO = new Workflow.Logic.BusinessFacade.FlowFO();
            flag = flowFO.ExecTask(taskExec.ID, routing.ID, nextExecUserIDs, nextExecUserNames, nextExecUserIDsGroup, nextExecRoleIDs, nextExecOrgIDs, execComment, Request["RoutingID"]);
            //流程表单定义的流程逻辑
            ExecFlowLogic(routing.Code, taskExec.S_WF_InsFlow.FormInstanceID, "");
            SetFormFlowInfo(taskExec.S_WF_InsFlow);
            if (flag)
            {
                if (dt.Rows.Count == 0)
                {
                    throw new Formula.Exceptions.BusinessException("实体ID为空，未能找到相关实体对象，无法执行逻辑");
                }
                T entity = (T)typeof(T).GetConstructor(System.Type.EmptyTypes).Invoke(null);
                entity.FillModel(dt.Rows[0]);
                OnFlowEnd(entity, taskExec, routing);
            }
            //执行路由的流程逻辑（部分逻辑执行时需要前后上下文关系数据，故需要放在基类的流程逻辑中执行）
            if (routing != null)
            {
                routing.ExeLogic();
                this.entities.SaveChanges();
            }
            return flag;
        }

        protected void ExecuteAction(Action action)
        {
            if (System.Configuration.ConfigurationManager.AppSettings["UseMsdtc"].ToLower() == "true")
            {
                TransactionOptions tranOp = new TransactionOptions();
                tranOp.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, tranOp))
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


    }
}