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
using System.Reflection;

namespace Project.Logic
{
    public class AuditFO
    {
        ProjectEntities instanceEnitites = FormulaHelper.GetEntities<ProjectEntities>();
        BaseConfigEntities configEnitites = FormulaHelper.GetEntities<BaseConfigEntities>();

        public virtual List<S_W_Activity> StartAuditFlow(T_AE_Audit auditForm, string auditFormUrl, List<S_E_Product> products, string wbsID)
        {
            var wbs = instanceEnitites.S_W_WBS.SingleOrDefault(d => d.ID == wbsID);
            if (wbs == null) throw new Exception("未能找到ID为【" + wbsID + "】的WBS对象，无法执行校审");

            var project = instanceEnitites.S_I_ProjectInfo.SingleOrDefault(d => d.ID == wbs.ProjectInfoID);
            if (project == null) throw new Exception("未能找到ID为【" + wbs.ProjectInfoID + "】的项目，无法执行校审");

            //为校审单设置默认值
            SetAuditFormInfo(auditForm, wbs, project);

            //生成流程步骤及启动参数
            var startParam = GetAuditFlowStartParam(auditForm, auditFormUrl, products, wbs, project);

            var service = AuditFlowServiceGenretor.CreateService();
            List<S_W_Activity> list = service.StartAuditFlow(startParam);
            //设置校审单状态
            auditForm.State = list[0].ActivityKey;
            var taskWork=wbs.S_W_TaskWork.FirstOrDefault();
            //设置校审人员到表单
            this.SetAuditUsers(auditForm, startParam.DefSteps,taskWork.DesignerUserID,taskWork.DesignerUserName);
            //设置成果属性
            this.SetProductProperty(products, auditForm.ID, auditForm.State);
            instanceEnitites.T_AE_Audit.Add(auditForm);
            var designAct = list[0];
            if (startParam.ExecNext)
            {
                var actType = ActivityType.Design.ToString();
                designAct = instanceEnitites.Set<S_W_Activity>().FirstOrDefault(c => c.ActivityKey == actType && c.BusniessID == designAct.BusniessID);
            }
            SetAuditSignUser(designAct, products);

            return list;

        }
        /// <summary>
        /// 设置表单默认属性
        /// </summary>
        /// <param name="auditForm"></param>
        /// <param name="wbs"></param>
        /// <param name="projectInfo"></param>
        public virtual void SetAuditFormInfo(T_AE_Audit auditForm, S_W_WBS wbs, S_I_ProjectInfo projectInfo)
        {
            if (String.IsNullOrEmpty(auditForm.ID))
                auditForm.ID = FormulaHelper.CreateGuid();

            if (string.IsNullOrEmpty(auditForm.WBSID))
                auditForm.WBSID = wbs.ID;
            if (string.IsNullOrEmpty(auditForm.ProjectInfoID))
                auditForm.ProjectInfoID = wbs.ProjectInfoID;
            if (string.IsNullOrEmpty(auditForm.State))
                auditForm.State = ProjectFlowState.InFlow.ToString();

            auditForm.CreateDate = DateTime.Now;
            auditForm.ProjectInfoCode = projectInfo.Code;
            auditForm.ProjectInfoName = projectInfo.Name;

            if (wbs.WBSType == WBSNodeType.Work.ToString())
            {
                auditForm.TaskWorkCode = wbs.Code;
                auditForm.TaskWorkName = wbs.Name;
            }

            var subProject = wbs.Seniorities.FirstOrDefault(d => d.WBSType == WBSNodeType.SubProject.ToString());
            if (subProject != null)
            {
                auditForm.SubProjectCode = subProject.Code;
                auditForm.SubProjectName = subProject.Name;
            }
            var major = wbs.Seniorities.FirstOrDefault(d => d.WBSType == WBSNodeType.Major.ToString());
            if (major != null)
            {
                auditForm.MajorCode = major.WBSValue;
                auditForm.MajorName = major.Name;
            }

            auditForm.PhaseCode = projectInfo.PhaseValue;
            var ph = BaseConfigFO.GetWBSEnum(WBSNodeType.Phase).AsEnumerable().FirstOrDefault(c => c["value"].ToString() == projectInfo.PhaseValue);
            if (ph != null)
                auditForm.PhaseName = ph["text"].ToString();
        }

        /// <summary>
        /// 获取校审流程启动参数
        /// </summary>
        /// <param name="auditForm"></param>
        /// <param name="auditFormUrl"></param>
        /// <param name="stepList"></param>
        /// <returns></returns>
        public virtual AuditFlowStartParam GetAuditFlowStartParam(T_AE_Audit auditForm, string auditFormUrl, List<S_E_Product> productList, S_W_WBS wbs, S_I_ProjectInfo projectInfo)
        {
            var startParam = new AuditFlowStartParam();
            startParam.DisplayName = EnumBaseHelper.GetEnumDescription(typeof(ActivityType), ActivityType.Design.ToString()) + "(" + auditForm.Name+")";
            startParam.AuditFormUrl = auditFormUrl;
            startParam.AuditFormID = auditForm.ID;
            startParam.WBSID = auditForm.WBSID;

            //参与校审的人员
            var userInfo = GetAuditRoleUser(auditForm.WBSID);
            //本次校审的环节
            var stepList = GetAuditSteps(userInfo, productList, wbs, projectInfo);
            foreach (var auditStep in stepList)
            {
                startParam.AddStep(auditStep);
            }
            return startParam;
        }

        /// <summary>
        /// 取本次校审的所有环节
        /// </summary>
        /// <param name="auditForm"></param>
        /// <returns></returns>
        public virtual List<AuditStep> GetAuditSteps(Dictionary<string, Dictionary<string, string>> userInfo, List<S_E_Product> productList, S_W_WBS wbs, S_I_ProjectInfo projectInfo)
        {
            List<AuditStep> stepList = new List<AuditStep>();
            var configInfo = configEnitites.S_C_ProofreadConfiguration.OrderBy(c=>c.Level).ToList();
            foreach (var config in configInfo)
            {
                if (string.IsNullOrEmpty(config.AuditSteps)) continue;
                //只取第一个符合条件的流程
                if (IsMatching(config, wbs, projectInfo, productList))
                {
                    List<AuditStep> defStepList = GetDefStepList(config, userInfo);
                    return defStepList;
                    //MergeStepList(stepList, defStepList);
                }

            }
            return stepList;
            //return OrderByStep(stepList);
        }
        public virtual bool IsMatching(S_C_ProofreadConfiguration auditConfig,S_W_WBS wbs,S_I_ProjectInfo projectInfo, List<S_E_Product> productList)
        {
            if (string.IsNullOrEmpty(auditConfig.AuditParams))
                return true;
            List<AuditConfigParam> list = JsonHelper.ToObject<List<AuditConfigParam>>(auditConfig.AuditParams);
            bool isTure=true;
            var taskWork = wbs.S_W_TaskWork.FirstOrDefault();
            foreach (var param in list)
            {
                //如果值空，忽略此条件
                if (string.IsNullOrEmpty(param.PropertyKey))
                    continue;
                Type t = Type.GetType("Project.Logic.Domain."+param.TableKey);
                if (t == null) return false;

                PropertyInfo pi = t.GetProperty(param.ColumnKey);
                if (pi == null) return false;
                switch (param.TableKey)
                {
                    case "S_I_ProjectInfo":
                        string value=(string)pi.GetValue(projectInfo, null);
                        if (value==null|| !param.PropertyKey.Split(',').Contains(value)) return false;
                        break;
                    case "S_W_TaskWork":
                        //var taskWork = wbs.S_W_TaskWork.FirstOrDefault();
                        if (taskWork == null)
                            return false;
                        value = (string)pi.GetValue(taskWork, null);
                        if (value == null || !param.PropertyKey.Split(',').Contains(value)) return false;
                        break;
                    case "S_E_Product":
                        bool exists = false;
                        foreach (var product in productList)
                        {
                            value = (string)pi.GetValue(product, null);
                            if (value != null && param.PropertyKey.Split(',').Contains(value)) 
                            {
                                exists = true;
                                break;
                            }
                        }
                        if (!exists) return false;
                        break;
                }
            }
            return isTure;
        }
        /// <summary>
        /// 取定义的校审步骤
        /// </summary>
        /// <param name="auditConfig"></param>
        /// <returns></returns>
        public virtual List<AuditStep> GetDefStepList(S_C_ProofreadConfiguration auditConfig, Dictionary<string, Dictionary<string, string>> userInfo)
        {
            List<AuditStep> stepList = new List<AuditStep>();

            var auditList = JsonHelper.ToObject<List<Dictionary<string, object>>>(auditConfig.AuditSteps);
            foreach (var dic in auditList)
            {
                var step = new AuditStep(dic);
                
                //设置人员
                if (!string.IsNullOrEmpty(dic.GetValue("AuditRole")))
                {
                    foreach (string role in dic.GetValue("AuditRole").Split(','))
                    {
                        var user = userInfo.GetValue(role);
                        if (user == null) continue;
                        string userID = user.GetValue("UserID");
                        //如果配置的是角色顺序优先，且第一个角色有人，则直接赋值，退出循环
                        //if (dic.GetValue("OnlyFirst") == "是" && !string.IsNullOrEmpty(userID))
                        //{
                        //    step.UserID = user.GetValue("UserID");
                        //    step.UserName = user.GetValue("UserName");
                        //    break;
                        //}
                        //else 
                        if (!string.IsNullOrEmpty(userID) && step.UserID.IndexOf(userID)<0)
                        {
                            string[] ids = userID.Split(',');
                            string[] names = user.GetValue("UserName").Split(',');
                            for (int i=0;i<ids.Length;i++)
                            {
                                if (step.UserID.IndexOf(ids[i]) < 0)
                                {
                                    step.UserID = step.UserID + ids[i] + ",";
                                    step.UserName = step.UserName + names[i] + ",";
                                }
                            }
                        }
                    }
                    step.UserID = step.UserID.TrimEnd(',');
                    step.UserName = step.UserName.TrimEnd(',');
                }
                //如果配置了人员为空，跳过本环节，则进行下个循环
                if (!string.IsNullOrEmpty(dic.GetValue("EmptyToNext")) && dic.GetValue("EmptyToNext") == "是" && string.IsNullOrEmpty(step.UserID))
                    continue;

                stepList.Add(step);
            }

            return stepList;
        }
        /// <summary>
        /// 合并第二个流程步骤到第一个
        /// </summary>
        /// <param name="firstStepList"></param>
        /// <param name="secondStepList"></param>
        public void MergeStepList(List<AuditStep> firstStepList, List<AuditStep> secondStepList)
        {
            if (secondStepList == null || secondStepList.Count() == 0) return;

            foreach (var step in secondStepList)
            {
                var existsStep = firstStepList.Where(c => c.StepKey == step.StepKey).FirstOrDefault();
                if (existsStep == null)
                {
                    firstStepList.Add(step);
                }
                else
                {
                    if (string.IsNullOrEmpty(step.UserID)) continue;
                    //如果人员不在此环节中，则加入执行人员
                    var userIDs = step.UserID.Split(',');
                    var userNames = step.UserName.Split(',');
                    for (int i = 0; i < userIDs.Length; i++)
                    {
                        string userID = userIDs[i];
                        if (existsStep.UserID.IndexOf(userID) < 0)
                        {
                            existsStep.UserID = existsStep.UserID + "," + userID;
                            existsStep.UserName = existsStep.UserName + "," + userNames[i];
                        }
                    }
                    existsStep.UserID = existsStep.UserID.TrimStart(',');
                    existsStep.UserName = existsStep.UserName.TrimStart(',');
                    //如果有并签，则此环节变为人员并签
                    if (step.AuditModel == AuditModel.multi)
                        existsStep.AuditModel = AuditModel.multi;
                }
            }
        }
        /// <summary>
        /// 根据校审步骤及校审角色顺序，重新调整校审流程顺序
        /// </summary>
        /// <param name="srcList"></param>
        /// <returns></returns>
        private List<AuditStep> OrderByStep(List<AuditStep> srcList)
        {
            List<AuditStep> stepList = new List<AuditStep>();
            //校审角色
            Array auditRoles = Enum.GetNames(typeof(AuditRoles));
            //校审类型（要和ActivityType中定义的类型匹配）
            string[] auditTypes = new string[] { "Design", "Collact", "Audit", "Approve" };

            int i = 1;
            foreach (string auditType in auditTypes)
            {
                AuditStep step = srcList.Where(c => c.StepKey.ToString() == auditType).FirstOrDefault();
                if (step != null)
                {
                    step.StepIndex = i;
                    if (string.IsNullOrEmpty(step.UserID))
                    {
                        step.UserID = "SelectOneUser";
                        step.UserName = "单选人";
                    }
                    stepList.Add(step);
                    i++;
                }
            }

            return stepList;
        }

        /// <summary>
        /// 设置成果属性（状态等）
        /// </summary>
        /// <param name="products"></param>
        /// <param name="auditFormID"></param>
        public virtual void SetProductProperty(List<S_E_Product> products, string auditFormID, string auditState)
        {
            foreach (var product in products)
            {
                product.AuditID = auditFormID;
                product.AuditState = auditState;
                product.State = ProjectFlowState.InFlow.ToString();
            }
        }


        /// <summary>
        /// 取校审用到的所有人员角色（项目根据需要调整）
        /// </summary>
        /// <param name="wbsID"></param>
        /// <returns></returns>
        public virtual Dictionary<string, Dictionary<string, string>> GetAuditRoleUser(string wbsID)
        {
            Dictionary<string, Dictionary<string, string>> roleUser = new Dictionary<string, Dictionary<string, string>>();
            var wbs = instanceEnitites.S_W_WBS.SingleOrDefault(d => d.ID == wbsID);
            var projectWBS = wbs.S_I_ProjectInfo.WBSRoot;
            var taskWork = wbs.S_W_TaskWork.FirstOrDefault();
            var majorWBS = instanceEnitites.S_W_WBS.Where(c => wbs.FullID.Contains(c.ID)).Where(c => c.WBSType == "Major").FirstOrDefault();

            //提交人
            var submitUser = FormulaHelper.GetUserInfo();
            Dictionary<string, string> dicSubmitUser = new Dictionary<string, string>();
            dicSubmitUser["UserID"] = submitUser.UserID;
            dicSubmitUser["UserName"] = submitUser.UserName;
            roleUser.Add(AuditRoles.SubmitUser.ToString(), dicSubmitUser);

            //设计人
            Dictionary<string, string> dicDesign = new Dictionary<string, string>();
            dicDesign["UserID"] = taskWork.DesignerUserID;
            dicDesign["UserName"] = taskWork.DesignerUserName;
            roleUser.Add(AuditRoles.Designer.ToString(), dicDesign);
            //校核人
            Dictionary<string, string> dicCollact = new Dictionary<string, string>();
            dicCollact["UserID"] = taskWork.CollactorUserID;
            dicCollact["UserName"] = taskWork.CollactorUserName;
            roleUser.Add(AuditRoles.Collactor.ToString(), dicCollact);
            //审核人
            Dictionary<string, string> dicAudit = new Dictionary<string, string>();
            dicAudit["UserID"] = taskWork.AuditorUserID;
            dicAudit["UserName"] = taskWork.AuditorUserName;
            roleUser.Add(AuditRoles.Auditor.ToString(), dicAudit);
            //审定人
            Dictionary<string, string> dicApprove = new Dictionary<string, string>();
            dicApprove["UserID"] = taskWork.ApproverUserID;
            dicApprove["UserName"] = taskWork.ApproverUserName;
            roleUser.Add(AuditRoles.Approver.ToString(), dicApprove);

            //主设（专业负责）人
            Dictionary<string, string> dicChiefDesign = new Dictionary<string, string>();
            var majorPrinciple = majorWBS.GetUser(ProjectRole.MajorPrinciple.ToString());
            if (majorPrinciple != null)
            {
                dicChiefDesign["UserID"] = majorPrinciple.UserID;
                dicChiefDesign["UserName"] = majorPrinciple.UserName;

                roleUser.Add(AuditRoles.MajorPrinciple.ToString(), dicChiefDesign);
            }
            //设总
            Dictionary<string, string> dicDesignManager = new Dictionary<string, string>();
            var designManager = projectWBS.GetUser(ProjectRole.ProjectManager.ToString());
            if (designManager != null)
            {
                dicDesignManager["UserID"] = designManager.UserID;
                dicDesignManager["UserName"] = designManager.UserName;

                roleUser.Add(AuditRoles.ProjectManager.ToString(), dicDesignManager);
            }
            //总工
            string ChiefEngineerRoleID = "a2a000f1-00e0-4c85-88bf-c790ad6a3e2c";
            Dictionary<string, string> dicChiefEngineer = new Dictionary<string, string>();
            var orgService = new Formula.OrgService();
            var chiefEngineer = orgService.GetOrgUsers(ChiefEngineerRoleID).FirstOrDefault();
            if (chiefEngineer != null)
            {
                dicChiefEngineer["UserID"] = chiefEngineer.UserID;
                dicChiefEngineer["UserName"] = chiefEngineer.UserName;

                roleUser.Add(AuditRoles.ChiefEngineer.ToString(), dicChiefEngineer);
            }
            //总经理（院长）
            string MainManagerRoleID = "a2a000f1-ef70-4dca-9dde-acce0ef5a047";
            Dictionary<string, string> dicMainManager = new Dictionary<string, string>();
            var mainManager = orgService.GetOrgUsers(MainManagerRoleID).FirstOrDefault();
            if (mainManager != null)
            {
                dicMainManager["UserID"] = mainManager.UserID;
                dicMainManager["UserName"] = mainManager.UserName;

                roleUser.Add(AuditRoles.MainManager.ToString(), dicMainManager);
            }
            return roleUser;
        }

        /// <summary>
        /// 设置校审人员到表单上
        /// </summary>
        /// <param name="auditForm">校审单</param>
        /// <param name="auditSteps">流程环节信息</param>
        /// <param name="designerID">设计人ID（非必要，流程启动人员非设计人时使用）</param>
        /// <param name="designerName"></param>
        public virtual void SetAuditUsers(T_AE_Audit auditForm, List<AuditStep> auditSteps, string designerID = "", string designerName = "")
        {
            foreach (var step in auditSteps)
            {
                //设计
                if (step.StepKey == ActivityType.Design)
                {
                    //流程中启动（首环节）人员可能不是设计人，所有需要参数确定设计人
                    if (!string.IsNullOrEmpty(step.UserID) && string.IsNullOrEmpty(designerID))
                    {
                        auditForm.DesignerID = step.UserID;
                        auditForm.DesignerName = step.UserName;
                    }
                    else if (!string.IsNullOrEmpty(designerID))
                    {
                        auditForm.DesignerID = designerID;
                        auditForm.DesignerName = designerName;
                    }
                }//校核
                else if (step.StepKey == ActivityType.Collact)
                {
                    if (!string.IsNullOrEmpty(step.UserID))
                    {
                        auditForm.CollactorID = step.UserID;
                        auditForm.CollactorName = step.UserName;
                    }
                }//审核
                else if (step.StepKey == ActivityType.Audit)
                {
                    if (!string.IsNullOrEmpty(step.UserID))
                    {
                        auditForm.AuditorID = step.UserID;
                        auditForm.AuditorName = step.UserName;
                    }
                }//审定
                else if (step.StepKey == ActivityType.Approve)
                {
                    if (!string.IsNullOrEmpty(step.UserID))
                    {
                        auditForm.ApproverID = step.UserID;
                        auditForm.ApproverName = step.UserName;
                    }
                }
            }
        }

        /// <summary>
        /// 设置设校审人员至校审单上
        /// </summary>
        /// <param name="audit">校审单对象</param>
        /// <param name="Users">设校审人员信息</param>
        private void SetAuditUsers(T_AE_Audit audit, List<Dictionary<string, object>> Users)
        {
            var enumDef = EnumBaseHelper.GetEnumDef(typeof(ProjectRole));
            //设置校审单上的校审人员角色
            foreach (var item in enumDef.EnumItem)
            {
                string idKey = item.Code + "ID";
                string nameKey = item.Code + "Name";
                var roleUser = Users.FirstOrDefault(delegate(Dictionary<string, object> user)
                {
                    if (String.IsNullOrEmpty(user.GetValue(idKey)))
                        return false;
                    return true;
                });
                if (roleUser != null)
                {
                    audit.SetProperty(idKey, roleUser.GetValue(idKey));
                    audit.SetProperty(nameKey, roleUser.GetValue(nameKey));
                }
            }
        }

        private AuditStep CreateStep(string userID, string userName, ActivityType actType, params AuditOption[] options)
        {
            var step = new AuditStep();
            step.StepKey = actType;
            step.UserID = userID;
            step.UserName = userName;
            if (options != null)
            {
                foreach (var item in options)
                    step.Options.Add(item);
            }
            return step;
        }

        public void AppendStep(string oldDefSteps, string newStep)
        { 
        
        }

        /// <summary>
        /// 设置校审签名信息
        /// </summary>
        /// <param name="act"></param>
        /// <param name="products"></param>
        public void SetAuditSignUser(S_W_Activity act, List<S_E_Product> products)
        {
            if (act == null)
                return;
            List<AuditSignUserDTO> list = JsonHelper.ToObject<List<AuditSignUserDTO>>(products[0].AuditSignUser);
            AuditSignUserDTO auditSign = null;
            if (list == null || list.Where(c => c.StepKey == act.ActivityKey && c.SignUserID.Contains(act.OwnerUserID)).Count() <= 0)
            {
                auditSign = new AuditSignUserDTO();
                if(list==null)
                    list = new List<AuditSignUserDTO>();
                list.Add(auditSign);
            }
            else
            {
                auditSign = list.Where(c => c.StepKey == act.ActivityKey && c.SignUserID.Contains(act.OwnerUserID)).FirstOrDefault();
            }

            auditSign.StepKey = act.ActivityKey;
            auditSign.SignUserID = act.OwnerUserID;
            auditSign.SignUserName = act.OwnerUserName;
            auditSign.SignDate = act.FinishDate;

            foreach (var product in products)
            {
                auditSign.ProductID = product.ID;
                auditSign.ProductCode = product.Code;

                product.AuditSignUser = JsonHelper.ToJson(list);

            }

        }
    }
}
