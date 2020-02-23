using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.Entity;
using System.Collections;
using Formula;
using Formula.Helper;
using MvcAdapter;
using Config;
using Config.Logic;
using Project.Logic.Domain;
using System.Linq.Expressions;
using Formula.DynConditionObject;

namespace Project.Logic
{
    public class AuditFlowService : IAuditFlowService
    {
        ProjectEntities instanceEnitites = FormulaHelper.GetEntities<ProjectEntities>();

        /// <summary>
        /// 启动校审
        /// </summary>
        /// <param name="startParam">启动参数</param>
        /// <param name="execNext">是否执行到下一环节</param>
        /// <returns>当前环节的活动</returns>
        public virtual List<S_W_Activity> StartAuditFlow(AuditFlowStartParam startParam)
        {
            var wbs = instanceEnitites.S_W_WBS.FirstOrDefault(d => d.ID == startParam.WBSID);
            if (wbs == null) throw new Exception("未能找到ID为【" + startParam.WBSID + "】的WBS对象，无法启动校审");
            this.validateStart(startParam);
            var designStep = startParam.DefSteps.FirstOrDefault(d => d.StepKey == ActivityType.Design);
            var designActivity = wbs.ActivityList.FirstOrDefault(d => d.OwnerUserID == designStep.UserID
                && d.State == ProjectCommoneState.Create.ToString() && d.AuditPatchID == startParam.AuditFormID);
            if (designActivity == null)
            {
                designActivity = new S_W_Activity();
                designActivity.ActvityName = EnumBaseHelper.GetEnumDescription(typeof(ActivityType), ActivityType.Design.ToString());
                designActivity.ActivityKey = ActivityType.Design.ToString();
                designActivity.OwnerUserID = designStep.UserID;
                designActivity.OwnerUserName = designStep.UserName;
                wbs.AddActivity(designActivity);
            }
            designActivity.DisplayName = startParam.DisplayName;
            designActivity.LinkUrl = startParam.AuditFormUrl;
            designActivity.DefSteps = JsonHelper.ToJson(startParam.DefStepsToCollection());
            designActivity.AuditPatchID = startParam.AuditFormID;
            designActivity.BusniessID = startParam.AuditFormID;
            designActivity.SetParam("ID", startParam.AuditFormID);

            List<S_W_Activity> rtnList = new List<S_W_Activity>();
            if (startParam.ExecNext)
            {
                designActivity.Finish();

                var nextStep = startParam.DefSteps.Where(d => d.StepIndex > designStep.StepIndex).OrderBy(d => d.StepIndex).FirstOrDefault();
                var execParam = new AuditFlowExecuteParam();
                execParam.DisplayName = designActivity.DisplayName.Replace(designActivity.DisplayName.Split('(').FirstOrDefault(), nextStep.StepName); ;
                execParam.NextStep = nextStep;
                execParam.WBSID = wbs.ID;
                execParam.ExecuteOption = AuditOption.Pass;
                designActivity.NextStep = JsonHelper.ToJson(nextStep.ToDic());
                var exeResult = this.Execute(designActivity, execParam);
                rtnList = exeResult.Activities;
            }
            else
            {
                rtnList.Add(designActivity);
            }

            return rtnList;
        }

        /// <summary>
        /// 执行校审
        /// </summary>
        /// <param name="activity">当前活动</param>
        /// <param name="exeOption">执行结果(同意或驳回)</param>
        /// <returns>执行结果对象</returns>
        public virtual ExeResult Execute(S_W_Activity activity, string exeOption, string SelectUserData)
        {
            var exeParam = new AuditFlowExecuteParam();
            exeParam.WBSID = activity.WBSID;
            exeParam.ExecuteOption = (AuditOption)Enum.Parse(typeof(AuditOption), exeOption);

            //获得活动中定义的所有校审流程步骤
            var steps = activity.GetSteps();

            //获得活动记录上记录的下一步校审步骤
            var nextStep = activity.GetNextStep();
            if (nextStep != null)
                nextStep = steps.FirstOrDefault(c => c.StepKey == nextStep.StepKey);

            //如果审批方式是驳回或者驳回返回类别的，则默认将活动任务返回给设计人
            if (exeOption == AuditOption.Back.ToString() || exeOption == AuditOption.Return.ToString())
            {
                nextStep = steps.FirstOrDefault(d => d.StepKey == ActivityType.Design);
            }
            //如果是结束则直接执行当前环节
            else if (exeParam.ExecuteOption == AuditOption.Over)
            {
                return this.Execute(activity, exeParam);
            }
            //如果活动定义上没有定义下一步骤的活动，则需要自动根据序号获取下移步骤
            else if (nextStep == null)
            {
                var currentStep = steps.FirstOrDefault(d => d.StepKey.ToString() == activity.ActivityKey);
                if (currentStep == null) throw new Exception("环节定义中未能找到【" + activity.ActivityKey
                    + "】的环节，内容，请确认Activity中的DefStep字段中是否存在【" + activity.ActivityKey + "】");
                var currentStepIndex = steps.IndexOf(currentStep) + 1;
                if (currentStepIndex >= steps.Count)
                    return this.Execute(activity, exeParam);
                nextStep = steps[currentStepIndex];
            }
            if (!string.IsNullOrEmpty(SelectUserData))
            {
                var users = JsonHelper.ToObject<List<Dictionary<string, string>>>(SelectUserData);
                nextStep.UserID = string.Join(",", users.Select(c => c.GetValue("ID")));
                nextStep.UserName = string.Join(",", users.Select(c => c.GetValue("Name")));
                //修改定义
                string defSteps = JsonHelper.ToJson(steps.Select(c => c.ToDic()));
                activity.DefSteps = defSteps;
                this.instanceEnitites.S_W_Activity.Where(c => c.BusniessID == activity.BusniessID && c.ActivityKey == activity.ActivityKey).Update(c => c.DefSteps = defSteps);
            }

            exeParam.NextStep = nextStep;
            exeParam.DisplayName = activity.DisplayName.Replace(activity.DisplayName.Split('(').FirstOrDefault(), nextStep.StepName);
            return this.Execute(activity, exeParam);
        }

        /// <summary>
        /// 执行校审
        /// </summary>
        /// <param name="exeParam">流程执行参数</param>
        /// <returns>执行结果对象</returns>
        public virtual ExeResult Execute(S_W_Activity activity, AuditFlowExecuteParam exeParam)
        {
            var exeResult = new ExeResult();
            var wbs = this.instanceEnitites.S_W_WBS.SingleOrDefault(d => d.ID == exeParam.WBSID);
            if (wbs == null) throw new Exception("未能找到ID为【" + exeParam.WBSID + "】的WBS对象，无法执行校审");

            //结束当前步骤
            activity.ExeucteOption = exeParam.ExecuteOption.ToString();
            activity.Finish();

            //结束属于一组的所有活动
            string state = ProjectCommoneState.Finish.ToString();
            instanceEnitites.S_W_Activity.Where(c => c.BusniessID == activity.BusniessID && c.GroupID == activity.GroupID).Update(c => c.State = state);

            string createState = ProjectCommoneState.Create.ToString();
            var currentStep = activity.GetCurrentStep();
            //如果是打回，结束所有本环节流程
            if (exeParam.ExecuteOption == AuditOption.Back || exeParam.ExecuteOption == AuditOption.Return)
            {
                instanceEnitites.S_W_Activity.Where(c => c.BusniessID == activity.BusniessID && c.ActivityKey == activity.ActivityKey).Update(c => c.State = state);
            }//并签时，查找本环节是否还有未结束的活动，如果有，直接返回
            else if (currentStep.AuditModel == AuditModel.multi && instanceEnitites.S_W_Activity.Where(c => c.BusniessID == activity.BusniessID && c.State == createState && c.ActivityKey == activity.ActivityKey && c.GroupID != activity.GroupID).Count() > 0)
            {
                exeResult.IsComplete = false;
                exeResult.ExecStatus = "Wait";
                return exeResult;
            }//串签时，如果还有等待的环节，直接把等待环节new出来
            else if (currentStep.AuditModel == AuditModel.Follow && instanceEnitites.S_W_Activity.Where(c => c.BusniessID == activity.BusniessID && c.State == "Wait" && c.ActivityKey == activity.ActivityKey && c.GroupID != activity.GroupID).Count() > 0)
            {
                var waitAct = instanceEnitites.S_W_Activity.Where(c => c.BusniessID == activity.BusniessID && c.State == "Wait" && c.ActivityKey == activity.ActivityKey && c.GroupID != activity.GroupID).OrderBy("ID", true).FirstOrDefault();
                waitAct.State = ProjectCommoneState.Create.ToString();

                exeResult.IsComplete = false;
                exeResult.ExecStatus = "Wait";
                return exeResult;
            }


            //活动环节上定义的所有校审步骤
            var steps = activity.GetSteps();
            var lastStep = steps.OrderByDescending(d => d.StepIndex).FirstOrDefault();
            //如果执行参数中的执行结果为结束校审，则直接结束校审
            if (exeParam.ExecuteOption == AuditOption.Over)
            {
                //如果是第一步直接结束，则认为是设计人直接撤回校审单
                if (activity.ActivityKey == ActivityType.Design.ToString())
                    exeResult.IsCancel = true;
                exeResult.IsComplete = true;
            }
            //如果当前步骤是活动步骤定义中的最后一个步骤，并且执行参数中的执行结果是通过的，则直接结束校审
            else if (lastStep.StepKey.ToString() == activity.ActivityKey && exeParam.ExecuteOption == AuditOption.Pass)
                exeResult.IsComplete = true;
            else
            {
                //正常向下执行校审
                //活动执行参数中的下一步骤对象
                var nextStep = exeParam.NextStep;
                if (nextStep == null) throw new Exception("下一环节的活动未找到，无法执行校审流程");
                if (nextStep.UserID == "SelectOneUser" || nextStep.UserID == "SelectMultiUser" || string.IsNullOrEmpty(nextStep.UserID))
                    throw new Exception("未指定下环节[" + nextStep.StepName + "]人员，不能执行到下一步。");
                exeResult.IsComplete = false;
                var nextUserIDs = nextStep.UserID.Split(',');
                var nextUserNames = nextStep.UserName.Split(',');
                //步骤上可存在多人，如果步骤中定义的是单签,则创建相同GroupID的活动，否则GroupID不相同
                string groupID = FormulaHelper.CreateGuid();
                for (int i = 0; i < nextUserIDs.Length; i++)
                {
                    var userID = nextUserIDs[i];
                    var userName = nextUserNames[i];
                    var nextActivity = new S_W_Activity();
                    if (String.IsNullOrEmpty(exeParam.DisplayName))
                        nextActivity.DisplayName = activity.DisplayName;
                    else
                        nextActivity.DisplayName = exeParam.DisplayName;
                    nextActivity.ActvityName = EnumBaseHelper.GetEnumDescription(typeof(ActivityType), exeParam.NextStep.StepKey.ToString());
                    nextActivity.ActivityKey = nextStep.StepKey.ToString();
                    nextActivity.OwnerUserID = userID;
                    nextActivity.OwnerUserName = userName;
                    nextActivity.DefSteps = activity.DefSteps;
                    nextActivity.AuditPatchID = activity.AuditPatchID;
                    nextActivity.BusniessID = activity.BusniessID;
                    nextActivity.LinkUrl = activity.LinkUrl;
                    nextActivity.Params = activity.Params;

                    //单签
                    if (nextStep.AuditModel == AuditModel.single)
                        nextActivity.GroupID = groupID;
                    else
                    {
                        nextActivity.GroupID = FormulaHelper.CreateGuid();
                        //如果是串签，则除去第一个外，都为等待状态
                        if (nextStep.AuditModel == AuditModel.Follow && i != 0)
                        {
                            nextActivity.State = "Wait";
                        }
                    }

                    if (nextStep.StepKey == ActivityType.Design)
                    {
                        if (exeParam.ExecuteOption == AuditOption.Return)
                        {
                            //如果存在中间必须经过的环节，则取必须经过的环节
                            var mustStep = steps.Where(c => c.StepIndex < currentStep.StepIndex&&c.MustStep=="是").FirstOrDefault();
                            if (mustStep != null)
                                nextActivity.NextStep = JsonHelper.ToJson(mustStep.ToDic());
                            else
                                nextActivity.NextStep = JsonHelper.ToJson(activity.GetCurrentStep().ToDic());
                        }
                        else
                        {
                            var nextActivityStep = steps.FirstOrDefault(d => d.StepKey.ToString() == nextActivity.ActivityKey);
                            var afterStepIndex = steps.IndexOf(nextActivityStep) + 1;
                            if (afterStepIndex >= steps.Count)
                                throw new Exception("");
                            nextStep = steps[afterStepIndex];
                            nextActivity.NextStep = JsonHelper.ToJson(nextStep.ToDic());
                        }
                    }

                    wbs.AddActivity(nextActivity);
                    exeResult.Activities.Add(nextActivity);
                }
            }
            return exeResult;
        }


        private void validateStart(AuditFlowStartParam startParam)
        {
            if (startParam.DefSteps.Count < 2) throw new Exception("未能指定正确的设校审步骤，无法启动校审");
            var designStep = startParam.DefSteps.FirstOrDefault(d => d.StepKey == ActivityType.Design);
            if (designStep == null) throw new Exception("校审步骤必须包含设计步骤");
            if (String.IsNullOrEmpty(designStep.UserID) || String.IsNullOrEmpty(designStep.UserName))
                throw new Exception("设计环节必须指定设计人才能启动校审");
            if (String.IsNullOrEmpty(startParam.AuditFormUrl))
                throw new Exception("请指定校审单的URL链接地址");
            if (String.IsNullOrEmpty(startParam.AuditFormID))
                throw new Exception("启动设校审时，必须指定校审单ID，请先创建校审单对象");
        }

        private void validateExec(AuditFlowExecuteParam execParam)
        {

        }
    }
}
