using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.Entity;
using System.Reflection;
using Formula.DynConditionObject;
using Formula.Helper;
using Formula;
using Market.Logic.Domain;
using Market.Logic;
using Config;
using Workflow.Logic.BusinessFacade;
using Workflow.Logic.Domain;
using System.Web.Mvc;
using System.Transactions;

namespace Market
{
    public class MarketFlowController<T> : MvcAdapter.BaseFlowController<T> where T : class, new()
    {
        private DbContext _flowEntities = null;
        protected virtual System.Data.Entity.DbContext flowEntities
        {
            get
            {
                if (_flowEntities == null)
                {
                    _flowEntities = FormulaHelper.GetEntities<Workflow.Logic.Domain.WorkflowEntities>();
                }
                return _flowEntities;
            }
        }


        private DbContext _entities = null;
        protected override System.Data.Entity.DbContext entities
        {
            get
            {
                if (_entities == null)
                {
                    _entities = FormulaHelper.GetEntities<MarketEntities>();
                }
                return _entities;
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

        protected virtual List<Dictionary<string, object>> GetRequestList(string paramName)
        {
            string data = this.Request[paramName];
            if (String.IsNullOrEmpty(data))
                return new List<Dictionary<string, object>>();
            return JsonHelper.ToList(data);
        }

        protected virtual Dictionary<string, object> GetRequestForm(string paramName)
        {
            string data = this.Request[paramName];
            if (String.IsNullOrEmpty(data))
                return new Dictionary<string, object>();
            return JsonHelper.ToObject(data);
        }

        protected virtual void BeforeDelete(List<T> entityList) { }

        protected virtual void AfterDelete(List<T> entityList) { }

        protected virtual void BeforeSave(T entity) { }

        protected virtual void AfterSave(T entity) { }

        protected virtual void BeforeGetMode(string id) { }

        protected virtual void AfterGetMode(T entity) { }

        protected virtual void OnExecute(T entity, S_WF_InsTaskExec taskExec, S_WF_InsDefRouting routing,
            string nextExecUserIDs, string nextExecUserNames,
            string nextExecUserIDsGroup, string nextExecRoleIDs,
            string nextExecOrgIDs, string execComment)
        { }

        protected virtual void OnFlowEnd(T entity, S_WF_InsTaskExec taskExec, S_WF_InsDefRouting routing,
            string nextExecUserIDs,string nextExecUserNames,
            string nextExecUserIDsGroup, string nextExecRoleIDs,
            string nextExecOrgIDs, string execComment)
        { }

        public override System.Web.Mvc.JsonResult GetModel(string id)
        {
            BeforeGetMode(id);
            var entity = this.GetEntityByID<T>(id);
            AfterGetMode(entity);
            return Json(entity);
        }

        public override System.Web.Mvc.JsonResult Delete()
        {
            Func<JsonResult> deleteAction = () =>
            {
                string id = Request["ID"];
                string taskExecID = Request["TaskExecID"];
                var flowEntities = FormulaHelper.GetEntities<WorkflowEntities>();
                if (!string.IsNullOrEmpty(id)) //流程启动时，从表单打开有地址栏参数ID
                {
                    flowEntities.S_WF_InsFlow.Delete(c => c.FormInstanceID == id);
                    flowEntities.SaveChanges();
                    return base.JsonDelete<T>(id);
                }
                else if (!string.IsNullOrEmpty(taskExecID)) //从任务列表打开删除
                {
                    S_WF_InsFlow flow = flowEntities.S_WF_InsTaskExec.Where(c => c.ID == taskExecID).SingleOrDefault().S_WF_InsTask.S_WF_InsFlow;
                    id = flow.FormInstanceID;
                    flowEntities.S_WF_InsFlow.Remove(flow);
                    flowEntities.SaveChanges();

                    return base.JsonDelete<T>(id);
                }
                else //列表上的删除按钮
                {
                    var ids = Request["ListIDs"].Split(',');
                    flowEntities.S_WF_InsFlow.Delete(c => ids.Contains(c.FormInstanceID));
                    flowEntities.SaveChanges();
                    Specifications res = new Specifications();
                    res.AndAlso("ID", ids, QueryMethod.In);
                    var entitylist = this.entities.Set<T>().Where(res.GetExpression<T>()).ToList();
                    this.BeforeDelete(entitylist);
                    return base.JsonDelete<T>(Request["ListIDs"]);
                }
            };

            if (System.Configuration.ConfigurationManager.AppSettings["UseMsdtc"].ToLower() == "true")
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    var result = deleteAction();
                    ts.Complete();
                    return result;
                }
            }
            else
            {
                return deleteAction();
            }
        }

        public override System.Web.Mvc.JsonResult Save()
        {
            var entity = UpdateEntity<T>();
            BeforeSave(entity);
            entities.SaveChanges();
            AfterSave(entity);
            PropertyInfo pi = typeof(T).GetProperty("ID");
            if (pi != null)
                return Json(new { ID = pi.GetValue(entity, null) });
            return Json(new { ID = "" });
        }

        public override bool ExecTaskExec(S_WF_InsTaskExec taskExec, S_WF_InsDefRouting routing, string nextExecUserIDs, string nextExecUserNames, string nextExecUserIDsGroup, string nextExecRoleIDs, string nextExecOrgIDs, string execComment)
        {
            var result = base.ExecTaskExec(taskExec, routing, nextExecUserIDs, nextExecUserNames,
                nextExecUserIDsGroup, nextExecRoleIDs, nextExecOrgIDs, execComment);
            var entity = this.GetEntityByID(taskExec.S_WF_InsFlow.FormInstanceID);
            if (entity == null) throw new Formula.Exceptions.BusinessException("实体ID为空，未能找到相关实体对象，无法执行逻辑");
            if (result)
            {
                this.OnFlowEnd(entity, taskExec, routing, nextExecUserIDs, nextExecUserNames,
                nextExecUserIDsGroup, nextExecRoleIDs, nextExecOrgIDs, execComment);
            }
            else
            {
                this.OnExecute(entity, taskExec, routing, nextExecUserIDs, nextExecUserNames,
                nextExecUserIDsGroup, nextExecRoleIDs, nextExecOrgIDs, execComment);
            }
            return result;
        }

        public virtual T GetEntityByID<T>(string ID) where T : class, new()
        {
            var spec = new Specifications();
            spec.AndAlso("ID", ID, QueryMethod.Equal);
            var result = this.entities.Set<T>().FirstOrDefault(spec.GetExpression<T>());
            return result;
        }

        public virtual T GetEntityByID(string ID)
        {
            var spec = new Specifications();
            spec.AndAlso("ID", ID, QueryMethod.Equal);
            var result = this.entities.Set<T>().FirstOrDefault(spec.GetExpression<T>());
            return result;
        }

        public virtual T CreateEmptyEntity<T>(string ID = "") where T : class, new()
        {
            var result = new T();
            if (String.IsNullOrEmpty(ID))
                FormulaHelper.SetProperty(result, "ID", FormulaHelper.CreateGuid());
            else
                FormulaHelper.SetProperty(result, "ID", ID);
            return result;
        }

        public virtual T CreateEmptyEntity(string ID = "")
        {
            var result = new T();
            if (String.IsNullOrEmpty(ID))
                FormulaHelper.SetProperty(result, "ID", FormulaHelper.CreateGuid());
            else
                FormulaHelper.SetProperty(result, "ID", ID);
            return result;
        }
    }

}