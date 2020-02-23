using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Project.Logic;
using Project.Logic.Domain;
using Formula.Helper;
using Config;
using Formula;
using System.Data;
using Config.Logic;
using Market.Logic.Domain;

namespace Project.Areas.AutoUI.Controllers
{
    public class TaskNoticeController : ProjectFormContorllor<T_CP_TaskNotice>
    {
        protected override void AfterGetData(DataTable dt, bool isNew, string upperVersionID)
        {
            if (isNew)
            {
                if (!String.IsNullOrEmpty(upperVersionID))
                {
                    var lastVersion = this.GetEntityByID(upperVersionID);
                    if (string.IsNullOrEmpty(lastVersion.ProjectInfoID) || lastVersion.ProjectInfoID.Split(',').Length > 1)
                        throw new Formula.Exceptions.BusinessException("该任务单关联多个项目，无法升版");
                    if (dt.Columns.Contains("SerialNumber") && dt.Rows.Count > 0)
                    {
                        //项目编号不允许修改，故如果是升版每次都从系项目基本信息中获取项目编号
                        var projectInfo = this.GetEntityByID<S_I_ProjectInfo>(lastVersion.ProjectInfoID);
                        if (projectInfo != null)
                        {
                            dt.Rows[0]["SerialNumber"] = projectInfo.Code;
                        }
                        else
                        {
                            dt.Rows[0]["SerialNumber"] = lastVersion.SerialNumber;
                        }
                    }
                }
                else
                {
                    //如果第一次下任务单给予一个初始的版本号
                    string FlowVersionNumberStart = "1";
                    if (!string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["FlowVersionNumberStart"]))
                        FlowVersionNumberStart = System.Configuration.ConfigurationManager.AppSettings["FlowVersionNumberStart"];
                    dt.Rows[0]["VersionNumber"] = FlowVersionNumberStart;
                }
            }
        }

        protected override void BeforeSaveDetail(Dictionary<string, string> dic, string subTableName, Dictionary<string, string> detail, List<Dictionary<string, string>> detailList, Base.Logic.Domain.S_UI_Form formInfo)
        {
            if (subTableName == "PhaseDetail")
            {
                var code = detail.GetValue("Code");
                var sameCode = this.BusinessEntities.Set<S_I_ProjectInfo>().FirstOrDefault(a => a.Code == code);
                if (sameCode != null)
                    throw new Formula.Exceptions.BusinessException("项目编号【" + sameCode + "】重复");
            }
        }

        protected override void BeforeSave(Dictionary<string, string> dic, Base.Logic.Domain.S_UI_Form formInfo, bool isNew)
        {
            var entity = this.GetEntityByID(dic["ID"]);
            if (entity == null) entity = new T_CP_TaskNotice();
            this.UpdateEntity(entity, dic);
            if (isNew)
            {
                //如果任务单一开始就有生产项目ID表示这里是任务单变更升版（因为任务单初次启动时，没有生产项目ID，立项完成后回写生产项目ID)
                var projectInfo = this.GetEntityByID<S_I_ProjectInfo>(entity.ProjectInfoID);
                if (projectInfo != null)
                {
                    //任务单升版时，项目编号不能进行变更
                    dic.SetValue("SerialNumber", projectInfo.Code);
                    var phaseNodes = projectInfo.S_W_WBS.ToList().Where(a => a.WBSType == WBSNodeType.Phase.ToString())
                        .Select(a => new { a.Name, a.WBSValue }).Distinct().ToList();
                    foreach (var item in phaseNodes)
                    {
                        if (!entity.Phase.Split(',').Contains(item.WBSValue))
                        {
                            //升版任务单时，不能删除已经存在wbs节点的阶段
                            throw new Formula.Exceptions.BusinessException("阶段【" + item.Name + "】已经存在策划信息，无法删除");
                        }
                    }

                    if (projectInfo.CBSRoot != null && projectInfo.CBSRoot.Children.Count > 0)
                    {
                        var workload = 0m;
                        if (!string.IsNullOrEmpty(dic.GetValue("Workload")))
                            workload = Convert.ToDecimal(dic.GetValue("Workload"));
                        if (workload < projectInfo.CBSRoot.Children.Sum(a => a.Quantity))
                            throw new Formula.Exceptions.BusinessException("项目的下达工时【" + workload + "】不能小于策划单已经策划的工时【" + projectInfo.CBSRoot.Children.Sum(a => a.Quantity) + "】");
                        if (workload < Convert.ToDecimal(projectInfo.CBSRoot.SummaryCostQuantity))
                            throw new Formula.Exceptions.BusinessException("项目的下达工时【" + workload + "】不能小于已经结算的工时【" + Convert.ToDecimal(projectInfo.CBSRoot.SummaryCostQuantity) + "】");
                    }
                }
            }
            Expenses.Logic.BusinessFacade.DataInterfaceFo.ValidateDataSyn("S_I_Project", dic.GetValue("ProjectInfoID"));
            if (string.IsNullOrEmpty(entity.ChargeDept))
            {
                dic.SetValue("ChargeDept", entity.DesignDept);
                dic.SetValue("ChargeDeptName", entity.DesignDeptName);
            }

            //校验合同金额
            if (!String.IsNullOrEmpty(dic.GetValue("ContractValue")) && !String.IsNullOrEmpty(dic.GetValue("ContractInfo")))
            {
                var projectValue = Convert.ToDecimal(dic.GetValue("ContractValue"));
                var db = SQLHelper.CreateSqlHelper(ConnEnum.Market);
                var relationObj = db.ExecuteScalar(String.Format("select isnull(sum(ProjectValue),0) from S_C_ManageContract_ProjectRelation where S_C_ManageContractID='{0}' and ProjectID<>'{1}'"
                     , dic.GetValue("ContractInfo"), dic.GetValue("ProjectInfoID")));
                var contractObj = db.ExecuteScalar(String.Format("select isnull(sum(ContractRMBAmount),0) from S_C_ManageContract where ID='{0}'"
                     , dic.GetValue("ContractInfo")));
                var relationValue = relationObj == null ? 0 : Convert.ToDecimal(relationObj);
                var contractValue = contractObj == null ? 0 : Convert.ToDecimal(contractObj);
                if ((contractValue - relationValue) < projectValue)
                {
                    throw new Formula.Exceptions.BusinessValidationException(String.Format("任务单的合同金额{1}，不能大于合同的剩余可关联金额【{0}】", (contractValue - relationValue), projectValue));
                }
            }
            entity.Validate();
        }

        protected override void OnFlowEnd(T_CP_TaskNotice entity, Workflow.Logic.Domain.S_WF_InsTaskExec taskExec, Workflow.Logic.Domain.S_WF_InsDefRouting routing)
        {
            if (entity == null) throw new Formula.Exceptions.BusinessException("没有找到指定的任务单，立项失败");
            var marketEntities = FormulaHelper.GetEntities<Market.Logic.Domain.MarketEntities>();

            var projectList = new List<S_I_ProjectInfo>();
            if (!String.IsNullOrEmpty(entity.ProjectInfoID)
                && this.BusinessEntities.Set<S_I_ProjectInfo>().Any(a => a.ID == entity.ProjectInfoID))
            {
                #region 任务单升版
                S_I_ProjectInfo projectInfo = null;
                S_I_Project project = null;
                projectInfo = entity.UpGrade();
                project = marketEntities.S_I_Project.FirstOrDefault(d => d.ID == projectInfo.MarketProjectInfoID);
                project.Name = entity.ProjectInfo;
                project.Code = entity.SerialNumber;
                project.Phase = entity.Phase;
                project.ProjectClass = entity.ProjectClass;
                project.Customer = entity.Customer;
                project.CustomerName = entity.CustomerName;
                project.CreateDate = DateTime.Now;
                project.EngineeringInfo = entity.EngineeringID;
                project.ChargerDept = string.IsNullOrEmpty(entity.ChargeDept) ? entity.DesignDept : entity.ChargeDept;
                project.ChargerDeptName = string.IsNullOrEmpty(entity.ChargeDeptName) ? entity.DesignDeptName : entity.ChargeDeptName;
                project.ChargerUser = entity.ChargeUser;
                project.ChargerUserName = entity.ChargeUserName;
                project.Country = entity.Country;
                project.Province = entity.Province;
                project.Area = entity.Area;

                if (string.IsNullOrEmpty(project.Area))
                {
                    //获取省份对应的地区(SubCategory)
                    var enumItem = EnumBaseHelper.GetEnumDef("System.Province").EnumItem.FirstOrDefault(a => a.Code == entity.Province);
                    if (enumItem != null)
                    {
                        project.Area = enumItem.SubCategory;
                    }
                }
                project.City = entity.City;
                project.ProjectScale = entity.ProjectLevel;
                projectInfo.ModifyDate = DateTime.Now;
                projectInfo.ModifyUser = entity.CreateUser;
                projectInfo.ModifyUserID = entity.CreateUserID;

                //修改经营库项目名称冗余字段
                if (!string.IsNullOrEmpty(projectInfo.MarketProjectInfoID))
                {
                    if (entity.ContractValue.HasValue && String.IsNullOrEmpty(entity.ContractInfo))
                    {
                        marketEntities.S_C_ManageContract_ProjectRelation.Where(a => a.ProjectID == projectInfo.MarketProjectInfoID &&
                            a.S_C_ManageContractID == entity.ContractInfo).Update(a => a.ProjectValue = entity.ContractValue);
                    }
                    marketEntities.S_C_ManageContract_ProjectRelation.Where(a => a.ProjectID == projectInfo.MarketProjectInfoID).Update(a => a.ProjectName = entity.ProjectInfo);
                    marketEntities.S_C_ManageContract_ReceiptObj.Where(a => a.ProjectInfo == projectInfo.MarketProjectInfoID).Update(a => a.ProjectInfoName = entity.ProjectInfo);
                    marketEntities.S_C_PlanReceipt.Where(a => a.ProjectID == projectInfo.MarketProjectInfoID).Update(a => a.ProjectName = entity.ProjectInfo);
                    marketEntities.S_FC_CostInfo.Where(a => a.ProjectID == projectInfo.MarketProjectInfoID).Update(a => a.ProjectName = entity.ProjectInfo);
                    marketEntities.S_SP_SupplierContract.Where(a => a.ProjectInfo == projectInfo.MarketProjectInfoID).Update(a => a.ProjectInfoName = entity.ProjectInfo);
                }
                projectList.Add(projectInfo);
                #endregion
            }
            else
            {
                #region 新建任务单立项

                #region 如果工程信息为空，则默认创建工程
                var engineering = marketEntities.S_I_Engineering.Find(entity.EngineeringID);
                bool newEngineer = false;
                if (engineering == null)
                {
                    engineering = new S_I_Engineering();
                    engineering.ID = FormulaHelper.CreateGuid();
                    marketEntities.S_I_Engineering.Add(engineering);
                    newEngineer = true;
                }
                engineering.Name = String.IsNullOrEmpty(entity.EngineeringName) ? entity.ProjectInfo : entity.EngineeringName;
                if (!String.IsNullOrEmpty(entity.Customer))
                {
                    engineering.CustomerInfo = entity.Customer;
                    engineering.CustomerInfoName = entity.CustomerName;
                }
                engineering.Code = String.IsNullOrEmpty(entity.EngineeringCode) ? entity.SerialNumber : entity.EngineeringCode;
                if (!string.IsNullOrEmpty(entity.MainDeptInfo))
                {
                    engineering.MainDept = entity.MainDeptInfo;
                    engineering.MainDeptName = entity.MainDeptInfoName;
                }
                else if (string.IsNullOrEmpty(engineering.MainDept))
                {
                    engineering.MainDept = entity.ChargeDept;
                    engineering.MainDeptName = entity.ChargeDeptName;
                }
                engineering.OtherDept = String.IsNullOrEmpty(entity.EngineeringOtherDept) ? entity.OtherDept : entity.EngineeringOtherDept;
                engineering.OtherDeptName = String.IsNullOrEmpty(entity.EngineeringOtherDeptName) ? entity.OtherDeptName : entity.EngineeringOtherDeptName;
                engineering.ChargerUser = String.IsNullOrEmpty(entity.EngineeringChargeUser) ? entity.ChargeUser : entity.EngineeringChargeUser;
                engineering.ChargerUserName = String.IsNullOrEmpty(entity.EngineeringChargeUserName) ? entity.ChargeUserName : entity.EngineeringChargeUserName;
                engineering.Country = entity.Country;
                engineering.Province = entity.Province;
                engineering.City = entity.City;
                engineering.Area = entity.Area;
                engineering.Proportion = entity.BuildArea;
                engineering.Address = entity.BuildAddress;
                engineering.LandingArea = entity.LandArea;
                engineering.CreateDate = DateTime.Now;
                if (!String.IsNullOrEmpty(entity.Investment))
                    engineering.Investment = Convert.ToDecimal(entity.Investment);
                if (newEngineer)
                {
                    entity.EngineeringID = engineering.ID;
                    entity.EngineeringName = engineering.Name;
                    entity.EngineeringCode = engineering.Code;
                    entity.EngineeringChargeUser = engineering.ChargerUser;
                    entity.EngineeringChargeUserName = engineering.ChargerUserName;
                    entity.MainDeptInfo = engineering.MainDept;
                    entity.MainDeptInfoName = engineering.MainDeptName;
                    entity.EngineeringOtherDept = engineering.OtherDept;
                    entity.EngineeringOtherDeptName = engineering.OtherDeptName;
                }

                var phaseName = "";
                var phaseList = BaseConfigFO.GetWBSAttrList(WBSNodeType.Phase);
                var entityPhaseList = phaseList.Where(d => entity.Phase.Contains(d.Code)).ToList();
                foreach (var item in entityPhaseList)
                {
                    phaseName += item.Name + ",";
                }
                engineering.PhaseContent = phaseName.TrimEnd(',');
                engineering.PhaseValue = entity.Phase;
                #endregion

                if (!string.IsNullOrEmpty(entity.MultiProjMode) && entity.MultiProjMode.ToLower() == "1")
                {
                    var list = entity.Phase.Split(',');
                    if (list.Length == 0)
                        throw new Formula.Exceptions.BusinessException("没有指定阶段");

                    string projIds = "";
                    foreach (var item in entity.T_CP_TaskNotice_PhaseDetail)
                    {
                        var prj = AddProject(entity, engineering, item);
                        projIds += prj.ID + ",";
                        item.ProjectInfoID = prj.ID;
                        projectList.Add(prj);
                    }
                    entity.ProjectInfoID = projIds.TrimEnd(',');
                }
                else
                {
                    var prj = AddProject(entity, engineering);
                    projectList.Add(prj);
                }

                #endregion
            }

            this.BusinessEntities.SaveChanges();
            marketEntities.SaveChanges();


            #region 自动同步核算项目
            Expenses.Logic.CBSInfoFO.SynCBSInfo(FormulaHelper.ModelToDic<T_CP_TaskNotice>(entity), Expenses.Logic.SetCBSOpportunity.TaskNoticeComplete);
            #endregion
        }

        private S_I_ProjectInfo AddProject(T_CP_TaskNotice entity, S_I_Engineering engineering, T_CP_TaskNotice_PhaseDetail singlePhase = null)
        {
            var marketEntities = FormulaHelper.GetEntities<Market.Logic.Domain.MarketEntities>();

            S_I_ProjectInfo projectInfo = entity.Push();
            projectInfo.ModifyDate = projectInfo.CreateDate;
            projectInfo.ModifyUser = projectInfo.CreateUser;
            projectInfo.ModifyUserID = projectInfo.CreateUserID;

            //重新修改phaseValue、phaseName、Name、Code等信息
            if (singlePhase != null)
            {
                projectInfo.PhaseValue = singlePhase.Phase;
                projectInfo.WBSRoot.PhaseCode = singlePhase.Phase;
                var phaseList = BaseConfigFO.GetWBSAttrList(WBSNodeType.Phase);
                var phaseItem = phaseList.FirstOrDefault(d => projectInfo.PhaseValue == d.Code);
                projectInfo.PhaseName = phaseItem.Name;
                projectInfo.Name = singlePhase.Name;
                projectInfo.Code = singlePhase.Code;
                projectInfo.ChargeDeptID = singlePhase.ChargeDept ?? entity.ChargeDept;
                projectInfo.ChargeDeptName = singlePhase.ChargeDeptName ?? entity.ChargeDeptName;
                projectInfo.ChargeUserID = singlePhase.ChargeUser ?? entity.ChargeUser;
                projectInfo.ChargeUserName = singlePhase.ChargeUserName ?? entity.ChargeUserName;
                projectInfo.OtherDeptID = singlePhase.OtherDept ?? entity.OtherDept;
                projectInfo.OtherDeptName = singlePhase.OtherDeptName ?? entity.OtherDeptName;
                projectInfo.PlanStartDate = singlePhase.PlanStartDate ?? entity.PlanStartDate;
                projectInfo.PlanFinishDate = singlePhase.PlanFinishDate ?? entity.PlanFinishDate;
            }

            projectInfo.ModifyDate = projectInfo.CreateDate;
            projectInfo.ModifyUser = projectInfo.CreateUser;
            projectInfo.ModifyUserID = projectInfo.CreateUserID;
            #region 同步创建经营库的项目信息
            S_I_Project project = null;
            var marketClue = marketEntities.S_P_MarketClue.Find(entity.RelateID);
            project = marketEntities.S_I_Project.FirstOrDefault(d => d.TasKNoticeID == entity.ID);
            if (project != null) throw new Formula.Exceptions.BusinessException("任务单已经下达过项目，无法重复下达");
            project = new S_I_Project();
            project.ID = projectInfo.ID;
            project.TasKNoticeID = entity.ID;
            project.TasKNoticeTmplCode = entity.TmplCode;
            project.Name = projectInfo.Name;
            project.Code = projectInfo.Code;
            project.Phase = projectInfo.PhaseValue;
            project.ProjectClass = projectInfo.ProjectClass;
            project.Customer = projectInfo.CustomerID;
            project.CustomerName = projectInfo.CustomerName;
            project.CreateDate = DateTime.Now;
            project.EngineeringInfo = entity.EngineeringID;
            project.ChargerDept = projectInfo.ChargeDeptID; 
            project.ChargerDeptName = projectInfo.ChargeDeptName;
            project.ChargerUser = projectInfo.ChargeUserID;
            project.ChargerUserName = projectInfo.ChargeUserName;
            project.Country = projectInfo.Country;
            project.Province = projectInfo.Province;
            project.Area = projectInfo.Area;
            project.City = projectInfo.City;
            project.State = projectInfo.State;
            project.ProjectScale = entity.ProjectLevel;
            if (marketClue != null)
            {
                project.MakertClueID = marketClue.ID;
                marketClue.State = Market.Logic.ClueState.Succeed.ToString();
            }
            marketEntities.S_I_Project.Add(project);
            entity.MarketProjectID = project.ID;
            #endregion

            #region 同步绑定合同

            if (!String.IsNullOrEmpty(entity.ContractInfo))
            {
                var contract = marketEntities.Set<S_C_ManageContract>().Include("S_C_ManageContract_ProjectRelation")
                    .Include("S_C_ManageContract_ReceiptObj").FirstOrDefault(d => d.ID == entity.ContractInfo);
                if (contract != null)
                {
                    var relation = contract.S_C_ManageContract_ProjectRelation.FirstOrDefault(a => a.ProjectID == project.ID);
                    if (relation == null)
                    {
                        relation = new S_C_ManageContract_ProjectRelation();
                        relation.ID = FormulaHelper.CreateGuid();
                        relation.ProjectID = project.ID;
                        relation.S_C_ManageContractID = contract.ID;
                        relation.ProjectCode = project.Code;
                        relation.ProjectName = project.Name;
                        if (contract.S_C_ManageContract_ProjectRelation.Count == 0)
                        {
                            if (entity.ContractValue.HasValue && contract.ContractRMBAmount > 0)
                            {
                                if (contract.ContractRMBAmount > 0)
                                {
                                    relation.Scale = entity.ContractValue.Value / contract.ContractRMBAmount * 100;
                                }
                                else
                                {
                                    relation.Scale = 0;
                                }
                                relation.ProjectValue = entity.ContractValue;
                                relation.TaxRate = contract.TaxRate;
                                relation.TaxValue = relation.ProjectValue / (1 + contract.TaxRate) * contract.TaxRate;
                                relation.ClearValue = relation.ProjectValue - relation.TaxValue;
                            }
                            else
                            {
                                relation.Scale = 100;
                                relation.ProjectValue = contract.ContractRMBAmount;
                                var taxRate = contract.TaxRate.HasValue ? contract.TaxRate.Value : 0;
                                var taxValue = contract.ContractRMBAmount / (1 + taxRate) * taxRate;
                                var clearVlaue = contract.ContractRMBAmount - taxValue;
                                relation.TaxRate = taxRate;
                                relation.TaxValue = taxValue;
                                relation.ClearValue = contract.ContractRMBAmount - taxValue;
                            }
                            foreach (var item in contract.S_C_ManageContract_ReceiptObj.ToList())
                            {
                                item.ProjectInfo = project.ID;
                                item.ProjectInfoName = project.Name;
                                relation.TaxRate = contract.TaxRate;
                            }
                        }
                        else
                        {
                            if (entity.ContractValue.HasValue && contract.ContractRMBAmount > 0)
                            {
                                if (contract.ContractRMBAmount > 0)
                                {
                                    relation.Scale = entity.ContractValue.Value / contract.ContractRMBAmount * 100;
                                }
                                else
                                {
                                    relation.Scale = 0;
                                }
                                relation.ProjectValue = entity.ContractValue;
                                relation.TaxRate = contract.TaxRate;
                                relation.TaxValue = relation.ProjectValue / (1 + contract.TaxRate) * contract.TaxRate;
                                relation.ClearValue = relation.ProjectValue - relation.TaxValue;
                            }
                            else
                            {
                                relation.Scale = 0;
                                relation.ProjectValue = 0;
                                relation.TaxRate = contract.TaxRate;
                            }
                        }
                        contract.S_C_ManageContract_ProjectRelation.Add(relation);
                        if (String.IsNullOrEmpty(contract.EngineeringInfo) && engineering != null)
                        {
                            contract.EngineeringInfo = engineering.ID;
                            contract.EngineeringInfoName = engineering.Name;
                        }
                    }
                }
            }
            else
            {
                var contractList = marketEntities.Set<S_C_ManageContract>().Include("S_C_ManageContract_ProjectRelation")
                .Include("S_C_ManageContract_ReceiptObj").Where(d => d.EngineeringInfo == entity.EngineeringID).ToList();
                foreach (var contract in contractList)
                {
                    var relation = contract.S_C_ManageContract_ProjectRelation.FirstOrDefault(a => a.ProjectID == project.ID);
                    if (relation == null)
                    {
                        relation = new S_C_ManageContract_ProjectRelation();
                        relation.ID = FormulaHelper.CreateGuid();
                        relation.ProjectID = project.ID;
                        relation.S_C_ManageContractID = contract.ID;
                        relation.ProjectCode = project.Code;
                        relation.ProjectName = project.Name;
                        if (contract.S_C_ManageContract_ProjectRelation.Count == 0)
                        {
                            relation.Scale = 100;
                            relation.ProjectValue = contract.ContractRMBAmount;
                            foreach (var item in contract.S_C_ManageContract_ReceiptObj.ToList())
                            {
                                item.ProjectInfo = project.ID;
                                item.ProjectInfoName = project.Name;
                            }
                        }
                        else
                        {
                            relation.Scale = 0;
                            relation.ProjectValue = 0;
                        }
                        contract.S_C_ManageContract_ProjectRelation.Add(relation);
                    }
                }
            }


            #endregion

            projectInfo.MarketProjectInfoID = project.ID;

            #region 默认创建EPS结构
            var group = this.BusinessEntities.Set<S_I_ProjectGroup>().FirstOrDefault(d => d.RelateID == entity.EngineeringID && d.Type == "Engineering");
            if (group == null)
            {
                group = new S_I_ProjectGroup();
                group.ID = Formula.FormulaHelper.CreateGuid();
                group.Name = engineering.Name;
                group.Code = engineering.Code;
                group.City = engineering.City;
                group.Country = engineering.Country;
                group.Province = engineering.Province;
                group.Area = engineering.Area;
                group.ProjectClass = engineering.Class;
                group.Investment = engineering.Investment;
                group.Proportion = engineering.Proportion;
                group.PhaseContent = engineering.PhaseContent;
                group.Address = engineering.Address;
                group.DeptID = engineering.MainDept;
                group.DeptName = engineering.MainDeptName;
                group.RelateID = engineering.ID;
                group.EngineeringSpaceCode = ProjectMode.Standard.ToString();
                group.CreateDate = DateTime.Now;
                var fo = Formula.FormulaHelper.CreateFO<EPSFO>();
                fo.BuildEngineering(group);
            }
            group.BindingProject(projectInfo);
            entity.GroupID = group.ID;
            group.PhaseContent = engineering.PhaseContent;
            group.PhaseValue = engineering.PhaseValue;
            #endregion

            projectInfo.InitDeisgnInputTemplate(true);

            //把设总放进RBS中
            if (projectInfo != null && !string.IsNullOrEmpty(entity.DesignManager))
            {
                projectInfo.WBSRoot.SetUsers(ProjectRole.DesignManager.ToString(), entity.DesignManager.Split(','), true, true, true, true);
            }

            var customer = marketEntities.Set<S_F_Customer>().FirstOrDefault(a => a.ID == entity.Customer);
            if (customer != null)
            {
                if (string.IsNullOrEmpty(entity.Country))
                {
                    project.Country = customer.Country;
                    projectInfo.Country = customer.Country;
                }
                if (string.IsNullOrEmpty(entity.Province))
                {
                    project.Province = customer.Province;
                    projectInfo.Province = customer.Province;
                }
                if (string.IsNullOrEmpty(entity.City))
                {
                    project.City = customer.City;
                    projectInfo.City = customer.City;
                }
                if (string.IsNullOrEmpty(entity.Area))
                {
                    project.Area = customer.Area;
                    projectInfo.Area = customer.Area;
                }
            }

            return projectInfo;
        }

        public JsonResult ValidateUpVersion(string Data)
        {
            var data = JsonHelper.ToObject(Data);
            var taskNotice = this.GetEntityByID(data.GetValue("ID"));
            if (taskNotice == null) throw new Formula.Exceptions.BusinessException("没有找到指定对的任务单，不能进行升版操作");
            if (taskNotice.FlowPhase != "End" || String.IsNullOrEmpty(taskNotice.ProjectInfoID))
            {
                throw new Formula.Exceptions.BusinessException("不能对没有审批完成的任务单进行升版操作");
            }
            Expenses.Logic.BusinessFacade.DataInterfaceFo.ValidateDataSyn("S_I_Project", taskNotice.ProjectInfoID);
            string FlowVersionNumberStart = "1";
            if (!string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["FlowVersionNumberStart"]))
                FlowVersionNumberStart = System.Configuration.ConfigurationManager.AppSettings["FlowVersionNumberStart"];
            SQLHelper sqlHelper = SQLHelper.CreateSqlHelper(ConnEnum.Project);
            string sql = string.Format("select ID,VersionNumber,FlowPhase from {0} where 1=1 and ProjectInfoID='{1}'", "T_CP_TaskNotice", taskNotice.ProjectInfoID);
            sql += " order by ID desc";
            var dt = sqlHelper.ExecuteDataTable(sql);
            var versions = string.Join(",", dt.AsEnumerable().Select(c => c["VersionNumber"].ToString()));
            return Json(new
            {
                ID = taskNotice.ID,
                ProjectInfoID = taskNotice.ProjectInfoID,
                VersionNumber = taskNotice.VersionNumber,
                Versions = versions
            });
        }

        public JsonResult GetRestContractValue()
        {
            string engineeringInfoID = GetQueryString("EngineeringInfoID");
            string sql = @"select case when  isnull(ContractRMBAmount,0)-isnull(ProjectValue,0)<0 then 0
else  isnull(ContractRMBAmount,0)-isnull(ProjectValue,0) end as RemainContractValue
 from S_C_ManageContract
left join (select Sum(ProjectValue) as ProjectValue,S_C_ManageContractID from S_C_ManageContract_ProjectRelation
group by S_C_ManageContractID) SplitInfo
on S_C_ManageContract.ID= SplitInfo.S_C_ManageContractID
where EngineeringInfo='{0}' 
and EngineeringInfo is not null and EngineeringInfo != ''";
            var marketSql = SQLHelper.CreateSqlHelper(ConnEnum.Market);
            var valObj = marketSql.ExecuteScalar(string.Format(sql, engineeringInfoID));
            if (valObj != null && valObj != DBNull.Value)
            {
                return Json(valObj);
            }

            return Json("");
        }
    }
}
