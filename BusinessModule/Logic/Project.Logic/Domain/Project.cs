

// This file was automatically generated.
// Do not make changes directly to this file - edit the template instead.
// 
// The following connection settings were used to generate this file
// 
//     Connection String Name: "Project"
//     Connection String:      "Data Source=.;Initial Catalog=EPM_Project;User ID=sa;PWD=123.zxc;"

// ReSharper disable RedundantUsingDirective
// ReSharper disable DoNotCallOverridableMethodsInConstructor
// ReSharper disable InconsistentNaming

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using Newtonsoft.Json;
using System.ComponentModel;

//using DatabaseGeneratedOption = System.ComponentModel.DataAnnotations.DatabaseGeneratedOption;

namespace Project.Logic.Domain
{
    // ************************************************************************
    // Database context
    public partial class ProjectEntities : Formula.FormulaDbContext
    {
        public IDbSet<S_AE_AuditAdvice> S_AE_AuditAdvice { get; set; } // S_AE_AuditAdvice
        public IDbSet<S_AE_Mistake> S_AE_Mistake { get; set; } // S_AE_Mistake
        public IDbSet<S_C_Attendance> S_C_Attendance { get; set; } // S_C_Attendance
        public IDbSet<S_C_CBS> S_C_CBS { get; set; } // S_C_CBS
        public IDbSet<S_C_CBS_Budget> S_C_CBS_Budget { get; set; } // S_C_CBS_Budget
        public IDbSet<S_C_CBS_Cost> S_C_CBS_Cost { get; set; } // S_C_CBS_Cost
        public IDbSet<S_D_DataCollection> S_D_DataCollection { get; set; } // S_D_DataCollection
        public IDbSet<S_D_DBS> S_D_DBS { get; set; } // S_D_DBS
        public IDbSet<S_D_DBSArchiveList> S_D_DBSArchiveList { get; set; } // S_D_DBSArchiveList
        public IDbSet<S_D_DBSArchiveList_ArchiveFiles> S_D_DBSArchiveList_ArchiveFiles { get; set; } // S_D_DBSArchiveList_ArchiveFiles
        public IDbSet<S_D_DBSSecurity> S_D_DBSSecurity { get; set; } // S_D_DBSSecurity
        public IDbSet<S_D_Document> S_D_Document { get; set; } // S_D_Document
        public IDbSet<S_D_DocumentVersion> S_D_DocumentVersion { get; set; } // S_D_DocumentVersion
        public IDbSet<S_D_Input> S_D_Input { get; set; } // S_D_Input
        public IDbSet<S_D_InputDocument> S_D_InputDocument { get; set; } // S_D_InputDocument
        public IDbSet<S_D_ShareInfo> S_D_ShareInfo { get; set; } // S_D_ShareInfo
        public IDbSet<S_E_ForceProject> S_E_ForceProject { get; set; } // S_E_ForceProject
        public IDbSet<S_E_ForceProjectChargeUser> S_E_ForceProjectChargeUser { get; set; } // S_E_ForceProjectChargeUser
        public IDbSet<S_E_Product> S_E_Product { get; set; } // S_E_Product
        public IDbSet<S_E_ProductDirectory> S_E_ProductDirectory { get; set; } // S_E_ProductDirectory
        public IDbSet<S_E_ProductVersion> S_E_ProductVersion { get; set; } // S_E_ProductVersion
        public IDbSet<S_E_PublishInfo> S_E_PublishInfo { get; set; } // S_E_PublishInfo
        public IDbSet<S_E_PublishInfoDetail> S_E_PublishInfoDetail { get; set; } // S_E_PublishInfoDetail
        public IDbSet<S_EP_PDFSignLog> S_EP_PDFSignLog { get; set; } // S_EP_PDFSignLog
        public IDbSet<S_EP_PlotSealGroup> S_EP_PlotSealGroup { get; set; } // S_EP_PlotSealGroup
        public IDbSet<S_EP_PlotSealGroup_GroupInfo> S_EP_PlotSealGroup_GroupInfo { get; set; } // S_EP_PlotSealGroup_GroupInfo
        public IDbSet<S_EP_PlotSealInfo> S_EP_PlotSealInfo { get; set; } // S_EP_PlotSealInfo
        public IDbSet<S_EP_PlotSealTemplate> S_EP_PlotSealTemplate { get; set; } // S_EP_PlotSealTemplate
        public IDbSet<S_EP_PlotSealTemplate_GroupList> S_EP_PlotSealTemplate_GroupList { get; set; } // S_EP_PlotSealTemplate_GroupList
        public IDbSet<S_EP_PublishInfo> S_EP_PublishInfo { get; set; } // S_EP_PublishInfo
        public IDbSet<S_EP_PublishInfo_PriceDetail> S_EP_PublishInfo_PriceDetail { get; set; } // S_EP_PublishInfo_PriceDetail
        public IDbSet<S_EP_PublishInfo_Products> S_EP_PublishInfo_Products { get; set; } // S_EP_PublishInfo_Products
        public IDbSet<S_F_BorderConfig> S_F_BorderConfig { get; set; } // S_F_BorderConfig
        public IDbSet<S_F_FrameInfo> S_F_FrameInfo { get; set; } // S_F_FrameInfo
        public IDbSet<S_F_FrameInfo_FrameManageInfo> S_F_FrameInfo_FrameManageInfo { get; set; } // S_F_FrameInfo_FrameManageInfo
        public IDbSet<S_I_FileConverts> S_I_FileConverts { get; set; } // S_I_FileConverts
        public IDbSet<S_I_ProjectGroup> S_I_ProjectGroup { get; set; } // S_I_ProjectGroup
        public IDbSet<S_I_ProjectInfo> S_I_ProjectInfo { get; set; } // S_I_ProjectInfo
        public IDbSet<S_I_ProjectRelation> S_I_ProjectRelation { get; set; } // S_I_ProjectRelation
        public IDbSet<S_I_UserDefaultProjectInfo> S_I_UserDefaultProjectInfo { get; set; } // S_I_UserDefaultProjectInfo
        public IDbSet<S_I_UserFocusProjectInfo> S_I_UserFocusProjectInfo { get; set; } // S_I_UserFocusProjectInfo
        public IDbSet<S_N_Notice> S_N_Notice { get; set; } // S_N_Notice
        public IDbSet<S_N_Notice_ViewDetail> S_N_Notice_ViewDetail { get; set; } // S_N_Notice_ViewDetail
        public IDbSet<S_P_CooperationPlan> S_P_CooperationPlan { get; set; } // S_P_CooperationPlan
        public IDbSet<S_P_MileStone> S_P_MileStone { get; set; } // S_P_MileStone
        public IDbSet<S_P_MileStone_FormDetail> S_P_MileStone_FormDetail { get; set; } // S_P_MileStone_FormDetail
        public IDbSet<S_P_MileStone_ProductDetail> S_P_MileStone_ProductDetail { get; set; } // S_P_MileStone_ProductDetail
        public IDbSet<S_P_MileStoneHistory> S_P_MileStoneHistory { get; set; } // S_P_MileStoneHistory
        public IDbSet<S_P_MileStonePlan> S_P_MileStonePlan { get; set; } // S_P_MileStonePlan
        public IDbSet<S_Q_QBS> S_Q_QBS { get; set; } // S_Q_QBS
        public IDbSet<S_R_Risk> S_R_Risk { get; set; } // S_R_Risk
        public IDbSet<S_T_ToDoList> S_T_ToDoList { get; set; } // S_T_ToDoList
        public IDbSet<S_W_Activity> S_W_Activity { get; set; } // S_W_Activity
        public IDbSet<S_W_CooperationExe> S_W_CooperationExe { get; set; } // S_W_CooperationExe
        public IDbSet<S_W_Monomer> S_W_Monomer { get; set; } // S_W_Monomer
        public IDbSet<S_W_OBSUser> S_W_OBSUser { get; set; } // S_W_OBSUser
        public IDbSet<S_W_RBS> S_W_RBS { get; set; } // S_W_RBS
        public IDbSet<S_W_StandardWorkTime> S_W_StandardWorkTime { get; set; } // S_W_StandardWorkTime
        public IDbSet<S_W_StandardWorkTimeDetail> S_W_StandardWorkTimeDetail { get; set; } // S_W_StandardWorkTimeDetail
        public IDbSet<S_W_TaskWork> S_W_TaskWork { get; set; } // S_W_TaskWork
        public IDbSet<S_W_TaskWork_RoleRate> S_W_TaskWork_RoleRate { get; set; } // S_W_TaskWork_RoleRate
        public IDbSet<S_W_WBS> S_W_WBS { get; set; } // S_W_WBS
        public IDbSet<S_W_WBS_Version> S_W_WBS_Version { get; set; } // S_W_WBS_Version
        public IDbSet<S_W_WBS_Version_Node> S_W_WBS_Version_Node { get; set; } // S_W_WBS_Version_Node
        public IDbSet<T_CAD_WBSAttrView> T_CAD_WBSAttrView { get; set; } // T_CAD_WBSAttrView
        public IDbSet<T_CP_ProjectInfoChange> T_CP_ProjectInfoChange { get; set; } // T_CP_ProjectInfoChange
        public IDbSet<T_CP_TaskNotice> T_CP_TaskNotice { get; set; } // T_CP_TaskNotice
        public IDbSet<T_CP_TaskNotice_PhaseDetail> T_CP_TaskNotice_PhaseDetail { get; set; } // T_CP_TaskNotice_PhaseDetail
        public IDbSet<T_EXE_Audit> T_EXE_Audit { get; set; } // T_EXE_Audit
        public IDbSet<T_EXE_Audit_AdviceDetail> T_EXE_Audit_AdviceDetail { get; set; } // T_EXE_Audit_AdviceDetail
        public IDbSet<T_EXE_AuditReview> T_EXE_AuditReview { get; set; } // T_EXE_AuditReview
        public IDbSet<T_EXE_ChangeAudit> T_EXE_ChangeAudit { get; set; } // T_EXE_ChangeAudit
        public IDbSet<T_EXE_ChangeAudit_AdviceDetail> T_EXE_ChangeAudit_AdviceDetail { get; set; } // T_EXE_ChangeAudit_AdviceDetail
        public IDbSet<T_EXE_DesignChangeApply> T_EXE_DesignChangeApply { get; set; } // T_EXE_DesignChangeApply
        public IDbSet<T_EXE_DesignChangeApply_TaskWork> T_EXE_DesignChangeApply_TaskWork { get; set; } // T_EXE_DesignChangeApply_TaskWork
        public IDbSet<T_EXE_DesignChangeNotice> T_EXE_DesignChangeNotice { get; set; } // T_EXE_DesignChangeNotice
        public IDbSet<T_EXE_MajorPutInfo> T_EXE_MajorPutInfo { get; set; } // T_EXE_MajorPutInfo
        public IDbSet<T_EXE_MajorPutInfo_AdviceDetail> T_EXE_MajorPutInfo_AdviceDetail { get; set; } // T_EXE_MajorPutInfo_AdviceDetail
        public IDbSet<T_EXE_MajorPutInfo_FetchDrawingInfo> T_EXE_MajorPutInfo_FetchDrawingInfo { get; set; } // T_EXE_MajorPutInfo_FetchDrawingInfo
        public IDbSet<T_EXE_ManageWorkloadSettlement> T_EXE_ManageWorkloadSettlement { get; set; } // T_EXE_ManageWorkloadSettlement
        public IDbSet<T_EXE_ManageWorkloadSettlement_ManageWorkloadList> T_EXE_ManageWorkloadSettlement_ManageWorkloadList { get; set; } // T_EXE_ManageWorkloadSettlement_ManageWorkloadList
        public IDbSet<T_EXE_MettingSign> T_EXE_MettingSign { get; set; } // T_EXE_MettingSign
        public IDbSet<T_EXE_MettingSign_AdviceDetail> T_EXE_MettingSign_AdviceDetail { get; set; } // T_EXE_MettingSign_AdviceDetail
        public IDbSet<T_EXE_MettingSign_ProjectGroupMembers> T_EXE_MettingSign_ProjectGroupMembers { get; set; } // T_EXE_MettingSign_ProjectGroupMembers
        public IDbSet<T_EXE_MettingSign_ResultList> T_EXE_MettingSign_ResultList { get; set; } // T_EXE_MettingSign_ResultList
        public IDbSet<T_EXE_ProductPlotDemo> T_EXE_ProductPlotDemo { get; set; } // T_EXE_ProductPlotDemo
        public IDbSet<T_EXE_ProductPlotDemo_ProductDetail> T_EXE_ProductPlotDemo_ProductDetail { get; set; } // T_EXE_ProductPlotDemo_ProductDetail
        public IDbSet<T_EXE_ProductReview> T_EXE_ProductReview { get; set; } // T_EXE_ProductReview
        public IDbSet<T_EXE_ProductReview_ProjectGroupMembers> T_EXE_ProductReview_ProjectGroupMembers { get; set; } // T_EXE_ProductReview_ProjectGroupMembers
        public IDbSet<T_EXE_ProductReview_ResultList> T_EXE_ProductReview_ResultList { get; set; } // T_EXE_ProductReview_ResultList
        public IDbSet<T_EXE_ProjectExamination> T_EXE_ProjectExamination { get; set; } // T_EXE_ProjectExamination
        public IDbSet<T_EXE_ProjectExamination_Content> T_EXE_ProjectExamination_Content { get; set; } // T_EXE_ProjectExamination_Content
        public IDbSet<T_EXE_PublishApply> T_EXE_PublishApply { get; set; } // T_EXE_PublishApply
        public IDbSet<T_EXE_PublishApply_PriceDetail> T_EXE_PublishApply_PriceDetail { get; set; } // T_EXE_PublishApply_PriceDetail
        public IDbSet<T_EXE_PublishApply_Products> T_EXE_PublishApply_Products { get; set; } // T_EXE_PublishApply_Products
        public IDbSet<T_EXE_TaskWorkChange> T_EXE_TaskWorkChange { get; set; } // T_EXE_TaskWorkChange
        public IDbSet<T_EXE_TaskWorkChange_TaskWork> T_EXE_TaskWorkChange_TaskWork { get; set; } // T_EXE_TaskWorkChange_TaskWork
        public IDbSet<T_EXE_TaskWorkPublish> T_EXE_TaskWorkPublish { get; set; } // T_EXE_TaskWorkPublish
        public IDbSet<T_EXE_TaskWorkPublish_TaskWork> T_EXE_TaskWorkPublish_TaskWork { get; set; } // T_EXE_TaskWorkPublish_TaskWork
        public IDbSet<T_EXE_TaskWorkSettlement> T_EXE_TaskWorkSettlement { get; set; } // T_EXE_TaskWorkSettlement
        public IDbSet<T_EXE_TaskWorkSettlement_TaskWorkList> T_EXE_TaskWorkSettlement_TaskWorkList { get; set; } // T_EXE_TaskWorkSettlement_TaskWorkList
        public IDbSet<T_SC_DesignInput> T_SC_DesignInput { get; set; } // T_SC_DesignInput
        public IDbSet<T_SC_DesignInput_DesignInputList> T_SC_DesignInput_DesignInputList { get; set; } // T_SC_DesignInput_DesignInputList
        public IDbSet<T_SC_DesignOutline> T_SC_DesignOutline { get; set; } // T_SC_DesignOutline
        public IDbSet<T_SC_DesignPlan> T_SC_DesignPlan { get; set; } // T_SC_DesignPlan
        public IDbSet<T_SC_DesignPlan_MilestoneList> T_SC_DesignPlan_MilestoneList { get; set; } // T_SC_DesignPlan_MilestoneList
        public IDbSet<T_SC_DesignPlanPetrifaction> T_SC_DesignPlanPetrifaction { get; set; } // T_SC_DesignPlanPetrifaction
        public IDbSet<T_SC_DesignPlanPetrifaction_MilestoneList> T_SC_DesignPlanPetrifaction_MilestoneList { get; set; } // T_SC_DesignPlanPetrifaction_MilestoneList
        public IDbSet<T_SC_ElectricalPowerProjectScheme> T_SC_ElectricalPowerProjectScheme { get; set; } // T_SC_ElectricalPowerProjectScheme
        public IDbSet<T_SC_ElectricalPowerProjectScheme_MajorList> T_SC_ElectricalPowerProjectScheme_MajorList { get; set; } // T_SC_ElectricalPowerProjectScheme_MajorList
        public IDbSet<T_SC_ElectricalPowerProjectScheme_ManageWorkloadList> T_SC_ElectricalPowerProjectScheme_ManageWorkloadList { get; set; } // T_SC_ElectricalPowerProjectScheme_ManageWorkloadList
        public IDbSet<T_SC_ElectricalPowerProjectScheme_MileStoneList> T_SC_ElectricalPowerProjectScheme_MileStoneList { get; set; } // T_SC_ElectricalPowerProjectScheme_MileStoneList
        public IDbSet<T_SC_ElectricalPowerProjectScheme_TaskWorkList> T_SC_ElectricalPowerProjectScheme_TaskWorkList { get; set; } // T_SC_ElectricalPowerProjectScheme_TaskWorkList
        public IDbSet<T_SC_MajorDesignInput> T_SC_MajorDesignInput { get; set; } // T_SC_MajorDesignInput
        public IDbSet<T_SC_MajorDesignInput_DesignInputList> T_SC_MajorDesignInput_DesignInputList { get; set; } // T_SC_MajorDesignInput_DesignInputList
        public IDbSet<T_SC_MixProjectScheme> T_SC_MixProjectScheme { get; set; } // T_SC_MixProjectScheme
        public IDbSet<T_SC_MixProjectScheme_MajorList> T_SC_MixProjectScheme_MajorList { get; set; } // T_SC_MixProjectScheme_MajorList
        public IDbSet<T_SC_MixProjectScheme_MileStoneList> T_SC_MixProjectScheme_MileStoneList { get; set; } // T_SC_MixProjectScheme_MileStoneList
        public IDbSet<T_SC_MixProjectScheme_SubProjectList> T_SC_MixProjectScheme_SubProjectList { get; set; } // T_SC_MixProjectScheme_SubProjectList
        public IDbSet<T_SC_PetrifactionProjectScheme> T_SC_PetrifactionProjectScheme { get; set; } // T_SC_PetrifactionProjectScheme
        public IDbSet<T_SC_PetrifactionProjectScheme_MajorList> T_SC_PetrifactionProjectScheme_MajorList { get; set; } // T_SC_PetrifactionProjectScheme_MajorList
        public IDbSet<T_SC_SchemeForm> T_SC_SchemeForm { get; set; } // T_SC_SchemeForm
        public IDbSet<T_SC_SchemeForm_MajorList> T_SC_SchemeForm_MajorList { get; set; } // T_SC_SchemeForm_MajorList
        public IDbSet<T_SC_SchemeForm_OEM_Szsow> T_SC_SchemeForm_OEM_Szsow { get; set; } // T_SC_SchemeForm_OEM_Szsow
        public IDbSet<T_SC_SchemeForm_OEM_Szsow_MajorList> T_SC_SchemeForm_OEM_Szsow_MajorList { get; set; } // T_SC_SchemeForm_OEM_Szsow_MajorList
        public IDbSet<T_SC_SchemeForm_OEM_Szsow_SubProjectList> T_SC_SchemeForm_OEM_Szsow_SubProjectList { get; set; } // T_SC_SchemeForm_OEM_Szsow_SubProjectList
        public IDbSet<T_SC_SchemeForm_SubProjectList> T_SC_SchemeForm_SubProjectList { get; set; } // T_SC_SchemeForm_SubProjectList
        public IDbSet<T_SC_SimpleProjectSchmea> T_SC_SimpleProjectSchmea { get; set; } // T_SC_SimpleProjectSchmea
        public IDbSet<T_SC_SimpleProjectSchmea_MileStone> T_SC_SimpleProjectSchmea_MileStone { get; set; } // T_SC_SimpleProjectSchmea_MileStone
        public IDbSet<T_SC_SingleProjectScheme> T_SC_SingleProjectScheme { get; set; } // T_SC_SingleProjectScheme
        public IDbSet<T_SC_SingleProjectScheme_MajorList> T_SC_SingleProjectScheme_MajorList { get; set; } // T_SC_SingleProjectScheme_MajorList
        public IDbSet<T_SC_SingleProjectScheme_MileStoneList> T_SC_SingleProjectScheme_MileStoneList { get; set; } // T_SC_SingleProjectScheme_MileStoneList
        public IDbSet<V_I_ProjectUserInfo> V_I_ProjectUserInfo { get; set; } // V_I_ProjectUserInfo
        public IDbSet<V_I_UserRBSInfo> V_I_UserRBSInfo { get; set; } // V_I_UserRBSInfo
        public IDbSet<V_P_MileStone> V_P_MileStone { get; set; } // V_P_MileStone
        public IDbSet<V_T_AE_Audit> V_T_AE_Audit { get; set; } // V_T_AE_Audit

        static ProjectEntities()
        {
            Database.SetInitializer<ProjectEntities>(null);
        }

        public ProjectEntities()
            : base("Name=Project")
        {
        }

        public ProjectEntities(string connectionString) : base(connectionString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new S_AE_AuditAdviceConfiguration());
            modelBuilder.Configurations.Add(new S_AE_MistakeConfiguration());
            modelBuilder.Configurations.Add(new S_C_AttendanceConfiguration());
            modelBuilder.Configurations.Add(new S_C_CBSConfiguration());
            modelBuilder.Configurations.Add(new S_C_CBS_BudgetConfiguration());
            modelBuilder.Configurations.Add(new S_C_CBS_CostConfiguration());
            modelBuilder.Configurations.Add(new S_D_DataCollectionConfiguration());
            modelBuilder.Configurations.Add(new S_D_DBSConfiguration());
            modelBuilder.Configurations.Add(new S_D_DBSArchiveListConfiguration());
            modelBuilder.Configurations.Add(new S_D_DBSArchiveList_ArchiveFilesConfiguration());
            modelBuilder.Configurations.Add(new S_D_DBSSecurityConfiguration());
            modelBuilder.Configurations.Add(new S_D_DocumentConfiguration());
            modelBuilder.Configurations.Add(new S_D_DocumentVersionConfiguration());
            modelBuilder.Configurations.Add(new S_D_InputConfiguration());
            modelBuilder.Configurations.Add(new S_D_InputDocumentConfiguration());
            modelBuilder.Configurations.Add(new S_D_ShareInfoConfiguration());
            modelBuilder.Configurations.Add(new S_E_ForceProjectConfiguration());
            modelBuilder.Configurations.Add(new S_E_ForceProjectChargeUserConfiguration());
            modelBuilder.Configurations.Add(new S_E_ProductConfiguration());
            modelBuilder.Configurations.Add(new S_E_ProductDirectoryConfiguration());
            modelBuilder.Configurations.Add(new S_E_ProductVersionConfiguration());
            modelBuilder.Configurations.Add(new S_E_PublishInfoConfiguration());
            modelBuilder.Configurations.Add(new S_E_PublishInfoDetailConfiguration());
            modelBuilder.Configurations.Add(new S_EP_PDFSignLogConfiguration());
            modelBuilder.Configurations.Add(new S_EP_PlotSealGroupConfiguration());
            modelBuilder.Configurations.Add(new S_EP_PlotSealGroup_GroupInfoConfiguration());
            modelBuilder.Configurations.Add(new S_EP_PlotSealInfoConfiguration());
            modelBuilder.Configurations.Add(new S_EP_PlotSealTemplateConfiguration());
            modelBuilder.Configurations.Add(new S_EP_PlotSealTemplate_GroupListConfiguration());
            modelBuilder.Configurations.Add(new S_EP_PublishInfoConfiguration());
            modelBuilder.Configurations.Add(new S_EP_PublishInfo_PriceDetailConfiguration());
            modelBuilder.Configurations.Add(new S_EP_PublishInfo_ProductsConfiguration());
            modelBuilder.Configurations.Add(new S_F_BorderConfigConfiguration());
            modelBuilder.Configurations.Add(new S_F_FrameInfoConfiguration());
            modelBuilder.Configurations.Add(new S_F_FrameInfo_FrameManageInfoConfiguration());
            modelBuilder.Configurations.Add(new S_I_FileConvertsConfiguration());
            modelBuilder.Configurations.Add(new S_I_ProjectGroupConfiguration());
            modelBuilder.Configurations.Add(new S_I_ProjectInfoConfiguration());
            modelBuilder.Configurations.Add(new S_I_ProjectRelationConfiguration());
            modelBuilder.Configurations.Add(new S_I_UserDefaultProjectInfoConfiguration());
            modelBuilder.Configurations.Add(new S_I_UserFocusProjectInfoConfiguration());
            modelBuilder.Configurations.Add(new S_N_NoticeConfiguration());
            modelBuilder.Configurations.Add(new S_N_Notice_ViewDetailConfiguration());
            modelBuilder.Configurations.Add(new S_P_CooperationPlanConfiguration());
            modelBuilder.Configurations.Add(new S_P_MileStoneConfiguration());
            modelBuilder.Configurations.Add(new S_P_MileStone_FormDetailConfiguration());
            modelBuilder.Configurations.Add(new S_P_MileStone_ProductDetailConfiguration());
            modelBuilder.Configurations.Add(new S_P_MileStoneHistoryConfiguration());
            modelBuilder.Configurations.Add(new S_P_MileStonePlanConfiguration());
            modelBuilder.Configurations.Add(new S_Q_QBSConfiguration());
            modelBuilder.Configurations.Add(new S_R_RiskConfiguration());
            modelBuilder.Configurations.Add(new S_T_ToDoListConfiguration());
            modelBuilder.Configurations.Add(new S_W_ActivityConfiguration());
            modelBuilder.Configurations.Add(new S_W_CooperationExeConfiguration());
            modelBuilder.Configurations.Add(new S_W_MonomerConfiguration());
            modelBuilder.Configurations.Add(new S_W_OBSUserConfiguration());
            modelBuilder.Configurations.Add(new S_W_RBSConfiguration());
            modelBuilder.Configurations.Add(new S_W_StandardWorkTimeConfiguration());
            modelBuilder.Configurations.Add(new S_W_StandardWorkTimeDetailConfiguration());
            modelBuilder.Configurations.Add(new S_W_TaskWorkConfiguration());
            modelBuilder.Configurations.Add(new S_W_TaskWork_RoleRateConfiguration());
            modelBuilder.Configurations.Add(new S_W_WBSConfiguration());
            modelBuilder.Configurations.Add(new S_W_WBS_VersionConfiguration());
            modelBuilder.Configurations.Add(new S_W_WBS_Version_NodeConfiguration());
            modelBuilder.Configurations.Add(new T_CAD_WBSAttrViewConfiguration());
            modelBuilder.Configurations.Add(new T_CP_ProjectInfoChangeConfiguration());
            modelBuilder.Configurations.Add(new T_CP_TaskNoticeConfiguration());
            modelBuilder.Configurations.Add(new T_CP_TaskNotice_PhaseDetailConfiguration());
            modelBuilder.Configurations.Add(new T_EXE_AuditConfiguration());
            modelBuilder.Configurations.Add(new T_EXE_Audit_AdviceDetailConfiguration());
            modelBuilder.Configurations.Add(new T_EXE_AuditReviewConfiguration());
            modelBuilder.Configurations.Add(new T_EXE_ChangeAuditConfiguration());
            modelBuilder.Configurations.Add(new T_EXE_ChangeAudit_AdviceDetailConfiguration());
            modelBuilder.Configurations.Add(new T_EXE_DesignChangeApplyConfiguration());
            modelBuilder.Configurations.Add(new T_EXE_DesignChangeApply_TaskWorkConfiguration());
            modelBuilder.Configurations.Add(new T_EXE_DesignChangeNoticeConfiguration());
            modelBuilder.Configurations.Add(new T_EXE_MajorPutInfoConfiguration());
            modelBuilder.Configurations.Add(new T_EXE_MajorPutInfo_AdviceDetailConfiguration());
            modelBuilder.Configurations.Add(new T_EXE_MajorPutInfo_FetchDrawingInfoConfiguration());
            modelBuilder.Configurations.Add(new T_EXE_ManageWorkloadSettlementConfiguration());
            modelBuilder.Configurations.Add(new T_EXE_ManageWorkloadSettlement_ManageWorkloadListConfiguration());
            modelBuilder.Configurations.Add(new T_EXE_MettingSignConfiguration());
            modelBuilder.Configurations.Add(new T_EXE_MettingSign_AdviceDetailConfiguration());
            modelBuilder.Configurations.Add(new T_EXE_MettingSign_ProjectGroupMembersConfiguration());
            modelBuilder.Configurations.Add(new T_EXE_MettingSign_ResultListConfiguration());
            modelBuilder.Configurations.Add(new T_EXE_ProductPlotDemoConfiguration());
            modelBuilder.Configurations.Add(new T_EXE_ProductPlotDemo_ProductDetailConfiguration());
            modelBuilder.Configurations.Add(new T_EXE_ProductReviewConfiguration());
            modelBuilder.Configurations.Add(new T_EXE_ProductReview_ProjectGroupMembersConfiguration());
            modelBuilder.Configurations.Add(new T_EXE_ProductReview_ResultListConfiguration());
            modelBuilder.Configurations.Add(new T_EXE_ProjectExaminationConfiguration());
            modelBuilder.Configurations.Add(new T_EXE_ProjectExamination_ContentConfiguration());
            modelBuilder.Configurations.Add(new T_EXE_PublishApplyConfiguration());
            modelBuilder.Configurations.Add(new T_EXE_PublishApply_PriceDetailConfiguration());
            modelBuilder.Configurations.Add(new T_EXE_PublishApply_ProductsConfiguration());
            modelBuilder.Configurations.Add(new T_EXE_TaskWorkChangeConfiguration());
            modelBuilder.Configurations.Add(new T_EXE_TaskWorkChange_TaskWorkConfiguration());
            modelBuilder.Configurations.Add(new T_EXE_TaskWorkPublishConfiguration());
            modelBuilder.Configurations.Add(new T_EXE_TaskWorkPublish_TaskWorkConfiguration());
            modelBuilder.Configurations.Add(new T_EXE_TaskWorkSettlementConfiguration());
            modelBuilder.Configurations.Add(new T_EXE_TaskWorkSettlement_TaskWorkListConfiguration());
            modelBuilder.Configurations.Add(new T_SC_DesignInputConfiguration());
            modelBuilder.Configurations.Add(new T_SC_DesignInput_DesignInputListConfiguration());
            modelBuilder.Configurations.Add(new T_SC_DesignOutlineConfiguration());
            modelBuilder.Configurations.Add(new T_SC_DesignPlanConfiguration());
            modelBuilder.Configurations.Add(new T_SC_DesignPlan_MilestoneListConfiguration());
            modelBuilder.Configurations.Add(new T_SC_DesignPlanPetrifactionConfiguration());
            modelBuilder.Configurations.Add(new T_SC_DesignPlanPetrifaction_MilestoneListConfiguration());
            modelBuilder.Configurations.Add(new T_SC_ElectricalPowerProjectSchemeConfiguration());
            modelBuilder.Configurations.Add(new T_SC_ElectricalPowerProjectScheme_MajorListConfiguration());
            modelBuilder.Configurations.Add(new T_SC_ElectricalPowerProjectScheme_ManageWorkloadListConfiguration());
            modelBuilder.Configurations.Add(new T_SC_ElectricalPowerProjectScheme_MileStoneListConfiguration());
            modelBuilder.Configurations.Add(new T_SC_ElectricalPowerProjectScheme_TaskWorkListConfiguration());
            modelBuilder.Configurations.Add(new T_SC_MajorDesignInputConfiguration());
            modelBuilder.Configurations.Add(new T_SC_MajorDesignInput_DesignInputListConfiguration());
            modelBuilder.Configurations.Add(new T_SC_MixProjectSchemeConfiguration());
            modelBuilder.Configurations.Add(new T_SC_MixProjectScheme_MajorListConfiguration());
            modelBuilder.Configurations.Add(new T_SC_MixProjectScheme_MileStoneListConfiguration());
            modelBuilder.Configurations.Add(new T_SC_MixProjectScheme_SubProjectListConfiguration());
            modelBuilder.Configurations.Add(new T_SC_PetrifactionProjectSchemeConfiguration());
            modelBuilder.Configurations.Add(new T_SC_PetrifactionProjectScheme_MajorListConfiguration());
            modelBuilder.Configurations.Add(new T_SC_SchemeFormConfiguration());
            modelBuilder.Configurations.Add(new T_SC_SchemeForm_MajorListConfiguration());
            modelBuilder.Configurations.Add(new T_SC_SchemeForm_OEM_SzsowConfiguration());
            modelBuilder.Configurations.Add(new T_SC_SchemeForm_OEM_Szsow_MajorListConfiguration());
            modelBuilder.Configurations.Add(new T_SC_SchemeForm_OEM_Szsow_SubProjectListConfiguration());
            modelBuilder.Configurations.Add(new T_SC_SchemeForm_SubProjectListConfiguration());
            modelBuilder.Configurations.Add(new T_SC_SimpleProjectSchmeaConfiguration());
            modelBuilder.Configurations.Add(new T_SC_SimpleProjectSchmea_MileStoneConfiguration());
            modelBuilder.Configurations.Add(new T_SC_SingleProjectSchemeConfiguration());
            modelBuilder.Configurations.Add(new T_SC_SingleProjectScheme_MajorListConfiguration());
            modelBuilder.Configurations.Add(new T_SC_SingleProjectScheme_MileStoneListConfiguration());
            modelBuilder.Configurations.Add(new V_I_ProjectUserInfoConfiguration());
            modelBuilder.Configurations.Add(new V_I_UserRBSInfoConfiguration());
            modelBuilder.Configurations.Add(new V_P_MileStoneConfiguration());
            modelBuilder.Configurations.Add(new V_T_AE_AuditConfiguration());
        }
    }

    // ************************************************************************
    // POCO classes

	/// <summary>校审知识库</summary>	
	[Description("校审知识库")]
    public partial class S_AE_AuditAdvice : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? ModifyDate { get; set; } // ModifyDate
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get; set; } // CreateUserID
		/// <summary></summary>	
		[Description("")]
        public string CreateUser { get; set; } // CreateUser
		/// <summary></summary>	
		[Description("")]
        public string ModifyUserID { get; set; } // ModifyUserID
		/// <summary></summary>	
		[Description("")]
        public string ModifyUser { get; set; } // ModifyUser
		/// <summary></summary>	
		[Description("")]
        public string OrgID { get; set; } // OrgID
		/// <summary></summary>	
		[Description("")]
        public string CompanyID { get; set; } // CompanyID
		/// <summary></summary>	
		[Description("")]
        public string FlowPhase { get; set; } // FlowPhase
		/// <summary></summary>	
		[Description("")]
        public string FlowInfo { get; set; } // FlowInfo
		/// <summary></summary>	
		[Description("")]
        public string StepName { get; set; } // StepName
		/// <summary>所属项目</summary>	
		[Description("所属项目")]
        public string ProjectInfo { get; set; } // ProjectInfo
		/// <summary>所属项目名称</summary>	
		[Description("所属项目名称")]
        public string ProjectInfoName { get; set; } // ProjectInfoName
		/// <summary>所属人</summary>	
		[Description("所属人")]
        public string BelongUser { get; set; } // BelongUser
		/// <summary>所属人名称</summary>	
		[Description("所属人名称")]
        public string BelongUserName { get; set; } // BelongUserName
		/// <summary>校审环节</summary>	
		[Description("校审环节")]
        public string AuditStep { get; set; } // AuditStep
		/// <summary>意见</summary>	
		[Description("意见")]
        public string Advice { get; set; } // Advice
		/// <summary>问题类型</summary>	
		[Description("问题类型")]
        public string Type { get; set; } // Type
		/// <summary>专业</summary>	
		[Description("专业")]
        public string Major { get; set; } // Major
		/// <summary>分类</summary>	
		[Description("分类")]
        public string Catalog { get; set; } // Catalog
		/// <summary>是否系统</summary>	
		[Description("是否系统")]
        public string Level { get; set; } // Level
    }

	/// <summary>校审记录表</summary>	
	[Description("校审记录表")]
    public partial class S_AE_Mistake : Formula.BaseModel
    {
		/// <summary>主键ID</summary>	
		[Description("主键ID")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary>关联校审批次记录ID</summary>	
		[Description("关联校审批次记录ID")]
        public string AuditID { get; set; } // AuditID
		/// <summary>项目ID</summary>	
		[Description("项目ID")]
        public string ProjectInfoID { get; set; } // ProjectInfoID
		/// <summary>错误内容</summary>	
		[Description("错误内容")]
        public string MistakeContent { get; set; } // MistakeContent
		/// <summary>活动（审批）类别</summary>	
		[Description("活动（审批）类别")]
        public string AuditActivityType { get; set; } // AuditActivityType
		/// <summary>错误等级</summary>	
		[Description("错误等级")]
        public string MistakeLevel { get; set; } // MistakeLevel
		/// <summary>专业编码</summary>	
		[Description("专业编码")]
        public string MajorCode { get; set; } // MajorCode
		/// <summary>专业名称</summary>	
		[Description("专业名称")]
        public string MajorName { get; set; } // MajorName
		/// <summary></summary>	
		[Description("")]
        public string DeptID { get; set; } // DeptID
		/// <summary></summary>	
		[Description("")]
        public string DeptName { get; set; } // DeptName
		/// <summary>设计人ID</summary>	
		[Description("设计人ID")]
        public string DesignerID { get; set; } // DesignerID
		/// <summary>设计人</summary>	
		[Description("设计人")]
        public string Designer { get; set; } // Designer
		/// <summary></summary>	
		[Description("")]
        public int MistakeYear { get; set; } // MistakeYear
		/// <summary></summary>	
		[Description("")]
        public int MistakeSeason { get; set; } // MistakeSeason
		/// <summary></summary>	
		[Description("")]
        public int MistakeMonth { get; set; } // MistakeMonth
		/// <summary>创建人ID</summary>	
		[Description("创建人ID")]
        public string CreateUserID { get; set; } // CreateUserID
		/// <summary>创建人</summary>	
		[Description("创建人")]
        public string CreateUser { get; set; } // CreateUser
		/// <summary>创建日期</summary>	
		[Description("创建日期")]
        public DateTime CreateDate { get; set; } // CreateDate
		/// <summary>措施方案</summary>	
		[Description("措施方案")]
        public string Measure { get; set; } // Measure
		/// <summary>措施提交人ID</summary>	
		[Description("措施提交人ID")]
        public string MeasureUserID { get; set; } // MeasureUserID
		/// <summary>措施提交人</summary>	
		[Description("措施提交人")]
        public string MeasureUser { get; set; } // MeasureUser
		/// <summary>状态</summary>	
		[Description("状态")]
        public string State { get; set; } // State
		/// <summary>关联图号</summary>	
		[Description("关联图号")]
        public string DrawingNO { get; set; } // DrawingNO
		/// <summary>错误位置</summary>	
		[Description("错误位置")]
        public string Postion { get; set; } // Postion
		/// <summary></summary>	
		[Description("")]
        public string ModifyUserID { get; set; } // ModifyUserID
		/// <summary></summary>	
		[Description("")]
        public string ModifyUser { get; set; } // ModifyUser
		/// <summary></summary>	
		[Description("")]
        public DateTime? ModifyDate { get; set; } // ModifyDate
		/// <summary></summary>	
		[Description("")]
        public string ChangeAuditID { get; set; } // ChangeAuditID
    }

	/// <summary></summary>	
	[Description("")]
    public partial class S_C_Attendance : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string ProjectID { get; set; } // ProjectID
		/// <summary>记录人</summary>	
		[Description("记录人")]
        public string RecorderID { get; set; } // RecorderID
		/// <summary>记录人名称</summary>	
		[Description("记录人名称")]
        public string RecorderName { get; set; } // RecorderName
		/// <summary></summary>	
		[Description("")]
        public DateTime? RecordTime { get; set; } // RecordTime
		/// <summary></summary>	
		[Description("")]
        public string Lat { get; set; } // Lat
		/// <summary></summary>	
		[Description("")]
        public string Long { get; set; } // Long
		/// <summary></summary>	
		[Description("")]
        public string Location { get; set; } // Location
    }

	/// <summary></summary>	
	[Description("")]
    public partial class S_C_CBS : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string ProjectInfoID { get; set; } // ProjectInfoID
		/// <summary></summary>	
		[Description("")]
        public string Name { get; set; } // Name
		/// <summary></summary>	
		[Description("")]
        public string Code { get; set; } // Code
		/// <summary></summary>	
		[Description("")]
        public string ParentID { get; set; } // ParentID
		/// <summary></summary>	
		[Description("")]
        public string FullID { get; set; } // FullID
		/// <summary></summary>	
		[Description("")]
        public string CBSType { get; set; } // CBSType
		/// <summary></summary>	
		[Description("")]
        public string NodeType { get; set; } // NodeType
		/// <summary></summary>	
		[Description("")]
        public decimal? UnitPrice { get; set; } // UnitPrice
		/// <summary></summary>	
		[Description("")]
        public decimal? Quantity { get; set; } // Quantity
		/// <summary></summary>	
		[Description("")]
        public decimal? TotalPrice { get; set; } // TotalPrice
		/// <summary></summary>	
		[Description("")]
        public decimal? SummaryBudgetQuantity { get; set; } // SummaryBudgetQuantity
		/// <summary></summary>	
		[Description("")]
        public decimal? SummaryBudgetPrice { get; set; } // SummaryBudgetPrice
		/// <summary></summary>	
		[Description("")]
        public decimal? SummaryCostQuantity { get; set; } // SummaryCostQuantity
		/// <summary></summary>	
		[Description("")]
        public decimal? SummaryCostPrice { get; set; } // SummaryCostPrice
		/// <summary></summary>	
		[Description("")]
        public string State { get; set; } // State
		/// <summary></summary>	
		[Description("")]
        public string Remark { get; set; } // Remark
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get; set; } // CreateUserID
		/// <summary></summary>	
		[Description("")]
        public string CreateUser { get; set; } // CreateUser
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public string ModifyUserID { get; set; } // ModifyUserID
		/// <summary></summary>	
		[Description("")]
        public string ModifyUser { get; set; } // ModifyUser
		/// <summary></summary>	
		[Description("")]
        public DateTime? ModifyDate { get; set; } // ModifyDate

        // Reverse navigation
		[JsonIgnore]
        public virtual ICollection<S_C_CBS_Budget> S_C_CBS_Budget { get { onS_C_CBS_BudgetGetting(); return _S_C_CBS_Budget;} }
		private ICollection<S_C_CBS_Budget> _S_C_CBS_Budget;
		partial void onS_C_CBS_BudgetGetting();

		[JsonIgnore]
        public virtual ICollection<S_C_CBS_Cost> S_C_CBS_Cost { get { onS_C_CBS_CostGetting(); return _S_C_CBS_Cost;} }
		private ICollection<S_C_CBS_Cost> _S_C_CBS_Cost;
		partial void onS_C_CBS_CostGetting();


        // Foreign keys
		[JsonIgnore]
        public virtual S_I_ProjectInfo S_I_ProjectInfo { get; set; } //  ProjectInfoID - FK_S_C_CBS_S_I_ProjectInfo

        public S_C_CBS()
        {
            _S_C_CBS_Budget = new List<S_C_CBS_Budget>();
            _S_C_CBS_Cost = new List<S_C_CBS_Cost>();
        }
    }

	/// <summary></summary>	
	[Description("")]
    public partial class S_C_CBS_Budget : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string CBSID { get; set; } // CBSID
		/// <summary></summary>	
		[Description("")]
        public string CBSFullID { get; set; } // CBSFullID
		/// <summary></summary>	
		[Description("")]
        public string ProjectInfoID { get; set; } // ProjectInfoID
		/// <summary></summary>	
		[Description("")]
        public string ParentID { get; set; } // ParentID
		/// <summary></summary>	
		[Description("")]
        public string FullID { get; set; } // FullID
		/// <summary></summary>	
		[Description("")]
        public string Name { get; set; } // Name
		/// <summary></summary>	
		[Description("")]
        public string Code { get; set; } // Code
		/// <summary></summary>	
		[Description("")]
        public decimal? UnitPrice { get; set; } // UnitPrice
		/// <summary></summary>	
		[Description("")]
        public decimal? Quantity { get; set; } // Quantity
		/// <summary></summary>	
		[Description("")]
        public decimal? TotalValue { get; set; } // TotalValue
		/// <summary></summary>	
		[Description("")]
        public string Unit { get; set; } // Unit
		/// <summary></summary>	
		[Description("")]
        public string Remark { get; set; } // Remark
		/// <summary></summary>	
		[Description("")]
        public decimal? SummaryCostQuantity { get; set; } // SummaryCostQuantity
		/// <summary></summary>	
		[Description("")]
        public decimal? SummaryCostValue { get; set; } // SummaryCostValue

        // Foreign keys
		[JsonIgnore]
        public virtual S_C_CBS S_C_CBS { get; set; } //  CBSID - FK_S_C_CBS_Budget_S_C_CBS
    }

	/// <summary></summary>	
	[Description("")]
    public partial class S_C_CBS_Cost : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string CBSID { get; set; } // CBSID
		/// <summary></summary>	
		[Description("")]
        public string CBSFullID { get; set; } // CBSFullID
		/// <summary></summary>	
		[Description("")]
        public string ProjectInfoID { get; set; } // ProjectInfoID
		/// <summary></summary>	
		[Description("")]
        public string BudgetID { get; set; } // BudgetID
		/// <summary></summary>	
		[Description("")]
        public string BudgetFullID { get; set; } // BudgetFullID
		/// <summary></summary>	
		[Description("")]
        public string FormID { get; set; } // FormID
		/// <summary></summary>	
		[Description("")]
        public string Name { get; set; } // Name
		/// <summary></summary>	
		[Description("")]
        public string Code { get; set; } // Code
		/// <summary></summary>	
		[Description("")]
        public decimal? UnitPrice { get; set; } // UnitPrice
		/// <summary></summary>	
		[Description("")]
        public decimal? Quantity { get; set; } // Quantity
		/// <summary></summary>	
		[Description("")]
        public decimal? TotalValue { get; set; } // TotalValue
		/// <summary></summary>	
		[Description("")]
        public DateTime? CostDate { get; set; } // CostDate
		/// <summary></summary>	
		[Description("")]
        public string CostUser { get; set; } // CostUser
		/// <summary></summary>	
		[Description("")]
        public string CostUserName { get; set; } // CostUserName
		/// <summary></summary>	
		[Description("")]
        public int? BelongYear { get; set; } // BelongYear
		/// <summary></summary>	
		[Description("")]
        public int? BelongQuarter { get; set; } // BelongQuarter
		/// <summary></summary>	
		[Description("")]
        public int? BelongMonth { get; set; } // BelongMonth
		/// <summary></summary>	
		[Description("")]
        public string BelongDept { get; set; } // BelongDept
		/// <summary></summary>	
		[Description("")]
        public string BelongDeptName { get; set; } // BelongDeptName
		/// <summary></summary>	
		[Description("")]
        public string UserDept { get; set; } // UserDept
		/// <summary></summary>	
		[Description("")]
        public string UserDeptName { get; set; } // UserDeptName
		/// <summary></summary>	
		[Description("")]
        public string RoleCode { get; set; } // RoleCode
		/// <summary></summary>	
		[Description("")]
        public string RoleName { get; set; } // RoleName
		/// <summary></summary>	
		[Description("")]
        public string TaskWorkCode { get; set; } // TaskWorkCode
		/// <summary></summary>	
		[Description("")]
        public string TaskWorkName { get; set; } // TaskWorkName
		/// <summary></summary>	
		[Description("")]
        public string MajorCode { get; set; } // MajorCode
		/// <summary></summary>	
		[Description("")]
        public string MajorName { get; set; } // MajorName

        // Foreign keys
		[JsonIgnore]
        public virtual S_C_CBS S_C_CBS { get; set; } //  CBSID - FK_S_C_CBS_Cost_S_C_CBS
    }

	/// <summary>资料收集</summary>	
	[Description("资料收集")]
    public partial class S_D_DataCollection : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? ModifyDate { get; set; } // ModifyDate
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get; set; } // CreateUserID
		/// <summary></summary>	
		[Description("")]
        public string CreateUser { get; set; } // CreateUser
		/// <summary></summary>	
		[Description("")]
        public string ModifyUserID { get; set; } // ModifyUserID
		/// <summary></summary>	
		[Description("")]
        public string ModifyUser { get; set; } // ModifyUser
		/// <summary></summary>	
		[Description("")]
        public string OrgID { get; set; } // OrgID
		/// <summary></summary>	
		[Description("")]
        public string CompanyID { get; set; } // CompanyID
		/// <summary></summary>	
		[Description("")]
        public string FlowPhase { get; set; } // FlowPhase
		/// <summary></summary>	
		[Description("")]
        public string StepName { get; set; } // StepName
		/// <summary>资料名称</summary>	
		[Description("资料名称")]
        public string DataName { get; set; } // DataName
		/// <summary>文件名称</summary>	
		[Description("文件名称")]
        public string FileName { get; set; } // FileName
		/// <summary>附件</summary>	
		[Description("附件")]
        public string Attachment { get; set; } // Attachment
		/// <summary>GroupInfoID</summary>	
		[Description("GroupInfoID")]
        public string GroupInfoID { get; set; } // GroupInfoID
    }

	/// <summary>DBS文件档结构</summary>	
	[Description("DBS文件档结构")]
    public partial class S_D_DBS : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string ProjectInfoID { get; set; } // ProjectInfoID
		/// <summary></summary>	
		[Description("")]
        public string Name { get; set; } // Name
		/// <summary></summary>	
		[Description("")]
        public string DBSCode { get; set; } // DBSCode
		/// <summary></summary>	
		[Description("")]
        public string DBSType { get; set; } // DBSType
		/// <summary></summary>	
		[Description("")]
        public string ParentID { get; set; } // ParentID
		/// <summary></summary>	
		[Description("")]
        public string FullID { get; set; } // FullID
		/// <summary></summary>	
		[Description("")]
        public string InheritAuth { get; set; } // InheritAuth
		/// <summary></summary>	
		[Description("")]
        public string MappingType { get; set; } // MappingType
		/// <summary></summary>	
		[Description("")]
        public string MappingNodeUrl { get; set; } // MappingNodeUrl
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get; set; } // CreateUserID
		/// <summary></summary>	
		[Description("")]
        public string CreateUser { get; set; } // CreateUser
		/// <summary></summary>	
		[Description("")]
        public DateTime CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public string ModifyUserID { get; set; } // ModifyUserID
		/// <summary></summary>	
		[Description("")]
        public string ModifyUser { get; set; } // ModifyUser
		/// <summary></summary>	
		[Description("")]
        public DateTime? ModifyDate { get; set; } // ModifyDate
		/// <summary></summary>	
		[Description("")]
        public string ConfigDBSID { get; set; } // ConfigDBSID
		/// <summary></summary>	
		[Description("")]
        public int? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string ArchiveFolder { get; set; } // ArchiveFolder
		/// <summary></summary>	
		[Description("")]
        public string ArchiveFolderName { get; set; } // ArchiveFolderName
		/// <summary></summary>	
		[Description("")]
        public bool IsPublic { get; set; } // IsPublic
		/// <summary></summary>	
		[Description("")]
        public string CooperationOrgID { get; set; } // CooperationOrgID

        // Reverse navigation
		[JsonIgnore]
        public virtual ICollection<S_D_DBSSecurity> S_D_DBSSecurity { get { onS_D_DBSSecurityGetting(); return _S_D_DBSSecurity;} }
		private ICollection<S_D_DBSSecurity> _S_D_DBSSecurity;
		partial void onS_D_DBSSecurityGetting();

		[JsonIgnore]
        public virtual ICollection<S_D_Document> S_D_Document { get { onS_D_DocumentGetting(); return _S_D_Document;} }
		private ICollection<S_D_Document> _S_D_Document;
		partial void onS_D_DocumentGetting();


        // Foreign keys
		[JsonIgnore]
        public virtual S_I_ProjectInfo S_I_ProjectInfo { get; set; } //  ProjectInfoID - FK_S_D_DBS_S_I_ProjectInfo

        public S_D_DBS()
        {
            _S_D_DBSSecurity = new List<S_D_DBSSecurity>();
            _S_D_Document = new List<S_D_Document>();
        }
    }

	/// <summary>归档资料清单</summary>	
	[Description("归档资料清单")]
    public partial class S_D_DBSArchiveList : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary>创建日期</summary>	
		[Description("创建日期")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? ModifyDate { get; set; } // ModifyDate
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get; set; } // CreateUserID
		/// <summary>创建人</summary>	
		[Description("创建人")]
        public string CreateUser { get; set; } // CreateUser
		/// <summary></summary>	
		[Description("")]
        public string ModifyUserID { get; set; } // ModifyUserID
		/// <summary></summary>	
		[Description("")]
        public string ModifyUser { get; set; } // ModifyUser
		/// <summary></summary>	
		[Description("")]
        public string OrgID { get; set; } // OrgID
		/// <summary></summary>	
		[Description("")]
        public string CompanyID { get; set; } // CompanyID
		/// <summary></summary>	
		[Description("")]
        public string FlowPhase { get; set; } // FlowPhase
		/// <summary></summary>	
		[Description("")]
        public string FlowInfo { get; set; } // FlowInfo
		/// <summary></summary>	
		[Description("")]
        public string StepName { get; set; } // StepName
		/// <summary>项目名称</summary>	
		[Description("项目名称")]
        public string Name { get; set; } // Name
		/// <summary>项目编号</summary>	
		[Description("项目编号")]
        public string Code { get; set; } // Code
		/// <summary>业务类型</summary>	
		[Description("业务类型")]
        public string ProjectClass { get; set; } // ProjectClass
		/// <summary>创建人名称</summary>	
		[Description("创建人名称")]
        public string CreateUserName { get; set; } // CreateUserName
		/// <summary>ProjectInfoID</summary>	
		[Description("ProjectInfoID")]
        public string ProjectInfoID { get; set; } // ProjectInfoID
		/// <summary>编号</summary>	
		[Description("编号")]
        public string SerialNumber { get; set; } // SerialNumber
		/// <summary>备注</summary>	
		[Description("备注")]
        public string Remark { get; set; } // Remark
		/// <summary>归档方式</summary>	
		[Description("归档方式")]
        public string ArchiveType { get; set; } // ArchiveType

        // Reverse navigation
		[JsonIgnore]
        public virtual ICollection<S_D_DBSArchiveList_ArchiveFiles> S_D_DBSArchiveList_ArchiveFiles { get { onS_D_DBSArchiveList_ArchiveFilesGetting(); return _S_D_DBSArchiveList_ArchiveFiles;} }
		private ICollection<S_D_DBSArchiveList_ArchiveFiles> _S_D_DBSArchiveList_ArchiveFiles;
		partial void onS_D_DBSArchiveList_ArchiveFilesGetting();


        public S_D_DBSArchiveList()
        {
            _S_D_DBSArchiveList_ArchiveFiles = new List<S_D_DBSArchiveList_ArchiveFiles>();
        }
    }

	/// <summary>文件清单</summary>	
	[Description("文件清单")]
    public partial class S_D_DBSArchiveList_ArchiveFiles : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string S_D_DBSArchiveListID { get; set; } // S_D_DBSArchiveListID
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string IsReleased { get; set; } // IsReleased
		/// <summary>名称</summary>	
		[Description("名称")]
        public string Name { get; set; } // Name
		/// <summary>编号</summary>	
		[Description("编号")]
        public string Code { get; set; } // Code
		/// <summary>分类</summary>	
		[Description("分类")]
        public string Catagory { get; set; } // Catagory
		/// <summary>专业</summary>	
		[Description("专业")]
        public string MajorValue { get; set; } // MajorValue
		/// <summary>创建人</summary>	
		[Description("创建人")]
        public string CreateUser { get; set; } // CreateUser
		/// <summary>创建人名称</summary>	
		[Description("创建人名称")]
        public string CreateUserName { get; set; } // CreateUserName
		/// <summary>创建日期</summary>	
		[Description("创建日期")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary>附件</summary>	
		[Description("附件")]
        public string MainFiles { get; set; } // MainFiles
		/// <summary>DocumentID</summary>	
		[Description("DocumentID")]
        public string DocumentID { get; set; } // DocumentID
		/// <summary>NewAdd</summary>	
		[Description("NewAdd")]
        public string NewAdd { get; set; } // NewAdd

        // Foreign keys
		[JsonIgnore]
        public virtual S_D_DBSArchiveList S_D_DBSArchiveList { get; set; } //  S_D_DBSArchiveListID - FK_S_D_DBSArchiveList_ArchiveFiles_S_D_DBSArchiveList
    }

	/// <summary></summary>	
	[Description("")]
    public partial class S_D_DBSSecurity : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string DBSID { get; set; } // DBSID
		/// <summary></summary>	
		[Description("")]
        public string RoleCode { get; set; } // RoleCode
		/// <summary></summary>	
		[Description("")]
        public string RoleName { get; set; } // RoleName
		/// <summary></summary>	
		[Description("")]
        public string AuthType { get; set; } // AuthType
		/// <summary></summary>	
		[Description("")]
        public string RelateValue { get; set; } // RelateValue
		/// <summary></summary>	
		[Description("")]
        public string RoleType { get; set; } // RoleType

        // Foreign keys
		[JsonIgnore]
        public virtual S_D_DBS S_D_DBS { get; set; } //  DBSID - FK_S_D_DBSSecurity_S_D_DBS
    }

	/// <summary>项目文档</summary>	
	[Description("项目文档")]
    public partial class S_D_Document : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string ProjectInfoID { get; set; } // ProjectInfoID
		/// <summary></summary>	
		[Description("")]
        public string DBSID { get; set; } // DBSID
		/// <summary></summary>	
		[Description("")]
        public string DBSFullID { get; set; } // DBSFullID
		/// <summary></summary>	
		[Description("")]
        public string Name { get; set; } // Name
		/// <summary></summary>	
		[Description("")]
        public string Code { get; set; } // Code
		/// <summary></summary>	
		[Description("")]
        public string MajorValue { get; set; } // MajorValue
		/// <summary></summary>	
		[Description("")]
        public string Catagory { get; set; } // Catagory
		/// <summary></summary>	
		[Description("")]
        public string FileType { get; set; } // FileType
		/// <summary></summary>	
		[Description("")]
        public string MainFiles { get; set; } // MainFiles
		/// <summary></summary>	
		[Description("")]
        public string PDFFile { get; set; } // PDFFile
		/// <summary></summary>	
		[Description("")]
        public string PlotFile { get; set; } // PlotFile
		/// <summary></summary>	
		[Description("")]
        public string XrefFile { get; set; } // XrefFile
		/// <summary></summary>	
		[Description("")]
        public string DwfFile { get; set; } // DwfFile
		/// <summary></summary>	
		[Description("")]
        public string TiffFile { get; set; } // TiffFile
		/// <summary></summary>	
		[Description("")]
        public string SignPdfFile { get; set; } // SignPdfFile
		/// <summary></summary>	
		[Description("")]
        public string Attr { get; set; } // Attr
		/// <summary></summary>	
		[Description("")]
        public string Attachments { get; set; } // Attachments
		/// <summary></summary>	
		[Description("")]
        public string RelateTable { get; set; } // RelateTable
		/// <summary></summary>	
		[Description("")]
        public string RelateID { get; set; } // RelateID
		/// <summary></summary>	
		[Description("")]
        public string Version { get; set; } // Version
		/// <summary></summary>	
		[Description("")]
        public string Description { get; set; } // Description
		/// <summary></summary>	
		[Description("")]
        public string State { get; set; } // State
		/// <summary></summary>	
		[Description("")]
        public DateTime? ArchiveDate { get; set; } // ArchiveDate
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get; set; } // CreateUserID
		/// <summary></summary>	
		[Description("")]
        public string CreateUser { get; set; } // CreateUser
		/// <summary></summary>	
		[Description("")]
        public DateTime CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public string ModifyUserID { get; set; } // ModifyUserID
		/// <summary></summary>	
		[Description("")]
        public string ModifyUser { get; set; } // ModifyUser
		/// <summary></summary>	
		[Description("")]
        public DateTime? ModifyDate { get; set; } // ModifyDate
		/// <summary></summary>	
		[Description("")]
        public bool? IsPublic { get; set; } // IsPublic
		/// <summary></summary>	
		[Description("")]
        public string CooperationOrgID { get; set; } // CooperationOrgID
		/// <summary></summary>	
		[Description("")]
        public string FontFile { get; set; } // FontFile

        // Reverse navigation
		[JsonIgnore]
        public virtual ICollection<S_D_DocumentVersion> S_D_DocumentVersion { get { onS_D_DocumentVersionGetting(); return _S_D_DocumentVersion;} }
		private ICollection<S_D_DocumentVersion> _S_D_DocumentVersion;
		partial void onS_D_DocumentVersionGetting();


        // Foreign keys
		[JsonIgnore]
        public virtual S_D_DBS S_D_DBS { get; set; } //  DBSID - FK_S_D_Document_S_D_DBS

        public S_D_Document()
        {
            _S_D_DocumentVersion = new List<S_D_DocumentVersion>();
        }
    }

	/// <summary></summary>	
	[Description("")]
    public partial class S_D_DocumentVersion : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string DocumentID { get; set; } // DocumentID
		/// <summary></summary>	
		[Description("")]
        public string DBSID { get; set; } // DBSID
		/// <summary></summary>	
		[Description("")]
        public string Name { get; set; } // Name
		/// <summary></summary>	
		[Description("")]
        public string Code { get; set; } // Code
		/// <summary></summary>	
		[Description("")]
        public string FileType { get; set; } // FileType
		/// <summary></summary>	
		[Description("")]
        public string MainFiles { get; set; } // MainFiles
		/// <summary></summary>	
		[Description("")]
        public string PDFFile { get; set; } // PDFFile
		/// <summary></summary>	
		[Description("")]
        public string PlotFile { get; set; } // PlotFile
		/// <summary></summary>	
		[Description("")]
        public string XrefFile { get; set; } // XrefFile
		/// <summary></summary>	
		[Description("")]
        public string DwfFile { get; set; } // DwfFile
		/// <summary></summary>	
		[Description("")]
        public string TiffFile { get; set; } // TiffFile
		/// <summary></summary>	
		[Description("")]
        public string SignPdfFile { get; set; } // SignPdfFile
		/// <summary></summary>	
		[Description("")]
        public string Attachments { get; set; } // Attachments
		/// <summary></summary>	
		[Description("")]
        public string Version { get; set; } // Version
		/// <summary></summary>	
		[Description("")]
        public string Description { get; set; } // Description
		/// <summary></summary>	
		[Description("")]
        public string State { get; set; } // State
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get; set; } // CreateUserID
		/// <summary></summary>	
		[Description("")]
        public string CreateUser { get; set; } // CreateUser
		/// <summary></summary>	
		[Description("")]
        public DateTime CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public string ModifyUserID { get; set; } // ModifyUserID
		/// <summary></summary>	
		[Description("")]
        public string ModifyUser { get; set; } // ModifyUser
		/// <summary></summary>	
		[Description("")]
        public DateTime? ModifyDate { get; set; } // ModifyDate
		/// <summary></summary>	
		[Description("")]
        public string FontFile { get; set; } // FontFile

        // Foreign keys
		[JsonIgnore]
        public virtual S_D_Document S_D_Document { get; set; } //  DocumentID - FK_S_D_DocumentVersion_S_D_Document
    }

	/// <summary>设计输入</summary>	
	[Description("设计输入")]
    public partial class S_D_Input : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string ProjectInfoID { get; set; } // ProjectInfoID
		/// <summary></summary>	
		[Description("")]
        public string EngineeringInfoID { get; set; } // EngineeringInfoID
		/// <summary></summary>	
		[Description("")]
        public string InfoName { get; set; } // InfoName
		/// <summary></summary>	
		[Description("")]
        public string Catagory { get; set; } // Catagory
		/// <summary>设计输入类型</summary>	
		[Description("设计输入类型")]
        public string InputType { get; set; } // InputType
		/// <summary>资料来源</summary>	
		[Description("资料来源")]
        public string DocType { get; set; } // DocType
		/// <summary></summary>	
		[Description("")]
        public string DBSCode { get; set; } // DBSCode
		/// <summary></summary>	
		[Description("")]
        public string ModifyUserID { get; set; } // ModifyUserID
		/// <summary></summary>	
		[Description("")]
        public string ModifyUser { get; set; } // ModifyUser
		/// <summary></summary>	
		[Description("")]
        public DateTime? ModifyDate { get; set; } // ModifyDate
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get; set; } // CreateUserID
		/// <summary></summary>	
		[Description("")]
        public string CreateUser { get; set; } // CreateUser
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? RegisteDate { get; set; } // RegisteDate
		/// <summary></summary>	
		[Description("")]
        public string State { get; set; } // State
		/// <summary></summary>	
		[Description("")]
        public int? InputTypeIndex { get; set; } // InputTypeIndex
		/// <summary></summary>	
		[Description("")]
        public int? SortIndex { get; set; } // SortIndex

        // Reverse navigation
		[JsonIgnore]
        public virtual ICollection<S_D_InputDocument> S_D_InputDocument { get { onS_D_InputDocumentGetting(); return _S_D_InputDocument;} }
		private ICollection<S_D_InputDocument> _S_D_InputDocument;
		partial void onS_D_InputDocumentGetting();


        // Foreign keys
		[JsonIgnore]
        public virtual S_I_ProjectInfo S_I_ProjectInfo { get; set; } //  ProjectInfoID - FK_E_W_Input_S_I_ProjectInfo

        public S_D_Input()
        {
            _S_D_InputDocument = new List<S_D_InputDocument>();
        }
    }

	/// <summary></summary>	
	[Description("")]
    public partial class S_D_InputDocument : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string InputID { get; set; } // InputID
		/// <summary></summary>	
		[Description("")]
        public string Name { get; set; } // Name
		/// <summary></summary>	
		[Description("")]
        public string Catagory { get; set; } // Catagory
		/// <summary></summary>	
		[Description("")]
        public string Files { get; set; } // Files
		/// <summary></summary>	
		[Description("")]
        public string AuditState { get; set; } // AuditState
		/// <summary></summary>	
		[Description("")]
        public string Attachments { get; set; } // Attachments
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public string CreateUser { get; set; } // CreateUser
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get; set; } // CreateUserID

        // Foreign keys
		[JsonIgnore]
        public virtual S_D_Input S_D_Input { get; set; } //  InputID - FK_S_D_InputDocument_S_D_Input
    }

	/// <summary>互提资料共享</summary>	
	[Description("互提资料共享")]
    public partial class S_D_ShareInfo : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? ModifyDate { get; set; } // ModifyDate
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get; set; } // CreateUserID
		/// <summary></summary>	
		[Description("")]
        public string CreateUser { get; set; } // CreateUser
		/// <summary></summary>	
		[Description("")]
        public string ModifyUserID { get; set; } // ModifyUserID
		/// <summary></summary>	
		[Description("")]
        public string ModifyUser { get; set; } // ModifyUser
		/// <summary></summary>	
		[Description("")]
        public string OrgID { get; set; } // OrgID
		/// <summary></summary>	
		[Description("")]
        public string CompanyID { get; set; } // CompanyID
		/// <summary></summary>	
		[Description("")]
        public string FlowPhase { get; set; } // FlowPhase
		/// <summary></summary>	
		[Description("")]
        public string StepName { get; set; } // StepName
		/// <summary>资料描述</summary>	
		[Description("资料描述")]
        public string InfoDetial { get; set; } // InfoDetial
		/// <summary>文件名称</summary>	
		[Description("文件名称")]
        public string FileName { get; set; } // FileName
		/// <summary>图纸编号</summary>	
		[Description("图纸编号")]
        public string Code { get; set; } // Code
		/// <summary>类别</summary>	
		[Description("类别")]
        public string Type { get; set; } // Type
		/// <summary>来源</summary>	
		[Description("来源")]
        public string Source { get; set; } // Source
		/// <summary>阶段</summary>	
		[Description("阶段")]
        public string PhaseValue { get; set; } // PhaseValue
		/// <summary>提交专业</summary>	
		[Description("提交专业")]
        public string WBSValue { get; set; } // WBSValue
		/// <summary>提交人</summary>	
		[Description("提交人")]
        public string Register { get; set; } // Register
		/// <summary>提交人名称</summary>	
		[Description("提交人名称")]
        public string RegisterName { get; set; } // RegisterName
		/// <summary>附件</summary>	
		[Description("附件")]
        public string Annex { get; set; } // Annex
		/// <summary>备注</summary>	
		[Description("备注")]
        public string Remarks { get; set; } // Remarks
		/// <summary>子项ID</summary>	
		[Description("子项ID")]
        public string SubProjectWBSID { get; set; } // SubProjectWBSID
		/// <summary>项目ID</summary>	
		[Description("项目ID")]
        public string ProjectInfoID { get; set; } // ProjectInfoID
		/// <summary></summary>	
		[Description("")]
        public string BatchID { get; set; } // BatchID
		/// <summary></summary>	
		[Description("")]
        public string AuditState { get; set; } // AuditState
		/// <summary>提资计划</summary>	
		[Description("提资计划")]
        public string CooperationPlan { get; set; } // CooperationPlan
		/// <summary>提资计划名称</summary>	
		[Description("提资计划名称")]
        public string CooperationPlanName { get; set; } // CooperationPlanName
		/// <summary>提资单ID</summary>	
		[Description("提资单ID")]
        public string FormID { get; set; } // FormID
		/// <summary>创建方式</summary>	
		[Description("创建方式")]
        public string SourceType { get; set; } // SourceType
		/// <summary></summary>	
		[Description("")]
        public string DrawingSource { get; set; } // DrawingSource
		/// <summary></summary>	
		[Description("")]
        public string DrawingID { get; set; } // DrawingID

        // Foreign keys
		[JsonIgnore]
        public virtual S_I_ProjectInfo S_I_ProjectInfo { get; set; } //  ProjectInfoID - FK_S_D_ShareInfo_S_I_ProjectInfo
    }

	/// <summary>关注项目</summary>	
	[Description("关注项目")]
    public partial class S_E_ForceProject : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? ModifyDate { get; set; } // ModifyDate
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get; set; } // CreateUserID
		/// <summary></summary>	
		[Description("")]
        public string CreateUser { get; set; } // CreateUser
		/// <summary></summary>	
		[Description("")]
        public string ModifyUserID { get; set; } // ModifyUserID
		/// <summary></summary>	
		[Description("")]
        public string ModifyUser { get; set; } // ModifyUser
		/// <summary></summary>	
		[Description("")]
        public string OrgID { get; set; } // OrgID
		/// <summary></summary>	
		[Description("")]
        public string CompanyID { get; set; } // CompanyID
		/// <summary></summary>	
		[Description("")]
        public string FlowPhase { get; set; } // FlowPhase
		/// <summary></summary>	
		[Description("")]
        public string FlowInfo { get; set; } // FlowInfo
		/// <summary></summary>	
		[Description("")]
        public string StepName { get; set; } // StepName
		/// <summary>项目</summary>	
		[Description("项目")]
        public string ProjectInfoID { get; set; } // ProjectInfoID
		/// <summary>项目名称</summary>	
		[Description("项目名称")]
        public string ProjectInfoIDName { get; set; } // ProjectInfoIDName
		/// <summary>项目名称</summary>	
		[Description("项目名称")]
        public string ProjectInfoName { get; set; } // ProjectInfoName
		/// <summary>项目Code</summary>	
		[Description("项目Code")]
        public string ProjectInfoCode { get; set; } // ProjectInfoCode
		/// <summary>用户</summary>	
		[Description("用户")]
        public string ForceUser { get; set; } // ForceUser
		/// <summary>用户名称</summary>	
		[Description("用户名称")]
        public string ForceUserName { get; set; } // ForceUserName
		/// <summary>MarketProjectInfoID</summary>	
		[Description("MarketProjectInfoID")]
        public string MarketProjectInfoID { get; set; } // MarketProjectInfoID
		/// <summary>MarketID</summary>	
		[Description("MarketID")]
        public string MarketID { get; set; } // MarketID
    }

	/// <summary>关注项目负责人</summary>	
	[Description("关注项目负责人")]
    public partial class S_E_ForceProjectChargeUser : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? ModifyDate { get; set; } // ModifyDate
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get; set; } // CreateUserID
		/// <summary></summary>	
		[Description("")]
        public string CreateUser { get; set; } // CreateUser
		/// <summary></summary>	
		[Description("")]
        public string ModifyUserID { get; set; } // ModifyUserID
		/// <summary></summary>	
		[Description("")]
        public string ModifyUser { get; set; } // ModifyUser
		/// <summary></summary>	
		[Description("")]
        public string OrgID { get; set; } // OrgID
		/// <summary></summary>	
		[Description("")]
        public string CompanyID { get; set; } // CompanyID
		/// <summary></summary>	
		[Description("")]
        public string FlowPhase { get; set; } // FlowPhase
		/// <summary></summary>	
		[Description("")]
        public string FlowInfo { get; set; } // FlowInfo
		/// <summary></summary>	
		[Description("")]
        public string StepName { get; set; } // StepName
		/// <summary>负责人</summary>	
		[Description("负责人")]
        public string ChargeUser { get; set; } // ChargeUser
		/// <summary>负责人名称</summary>	
		[Description("负责人名称")]
        public string ChargeUserName { get; set; } // ChargeUserName
		/// <summary>关注人</summary>	
		[Description("关注人")]
        public string ForceUser { get; set; } // ForceUser
		/// <summary>关注人名称</summary>	
		[Description("关注人名称")]
        public string ForceUserName { get; set; } // ForceUserName
    }

	/// <summary></summary>	
	[Description("")]
    public partial class S_E_Product : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary>项目信息ID</summary>	
		[Description("项目信息ID")]
        public string ProjectInfoID { get; set; } // ProjectInfoID
		/// <summary>WBS关联ID</summary>	
		[Description("WBS关联ID")]
        public string WBSID { get; set; } // WBSID
		/// <summary></summary>	
		[Description("")]
        public string WBSFullID { get; set; } // WBSFullID
		/// <summary>成果名称</summary>	
		[Description("成果名称")]
        public string Name { get; set; } // Name
		/// <summary>成果编号</summary>	
		[Description("成果编号")]
        public string Code { get; set; } // Code
		/// <summary></summary>	
		[Description("")]
        public string SN { get; set; } // SN
		/// <summary></summary>	
		[Description("")]
        public string MajorValue { get; set; } // MajorValue
		/// <summary></summary>	
		[Description("")]
        public string PhaseValue { get; set; } // PhaseValue
		/// <summary></summary>	
		[Description("")]
        public string SubProjectInfo { get; set; } // SubProjectInfo
		/// <summary></summary>	
		[Description("")]
        public string MonomerInfo { get; set; } // MonomerInfo
		/// <summary>关联校审批次ID</summary>	
		[Description("关联校审批次ID")]
        public string AuditID { get; set; } // AuditID
		/// <summary></summary>	
		[Description("")]
        public string Designer { get; set; } // Designer
		/// <summary></summary>	
		[Description("")]
        public string DesignerName { get; set; } // DesignerName
		/// <summary></summary>	
		[Description("")]
        public string Collactor { get; set; } // Collactor
		/// <summary></summary>	
		[Description("")]
        public string CollactorName { get; set; } // CollactorName
		/// <summary></summary>	
		[Description("")]
        public string Auditor { get; set; } // Auditor
		/// <summary></summary>	
		[Description("")]
        public string AuditorName { get; set; } // AuditorName
		/// <summary></summary>	
		[Description("")]
        public string Approver { get; set; } // Approver
		/// <summary></summary>	
		[Description("")]
        public string ApproverName { get; set; } // ApproverName
		/// <summary>关联会签批次ID</summary>	
		[Description("关联会签批次ID")]
        public string CounterSignAuditID { get; set; } // CounterSignAuditID
		/// <summary>校审状态</summary>	
		[Description("校审状态")]
        public string AuditState { get; set; } // AuditState
		/// <summary>会签状态(SignComplete:会签完成；Sign：会签中)</summary>	
		[Description("会签状态(SignComplete:会签完成；Sign：会签中)")]
        public string CoSignState { get; set; } // CoSignState
		/// <summary>出图状态</summary>	
		[Description("出图状态")]
        public string PrintState { get; set; } // PrintState
		/// <summary>归档状态</summary>	
		[Description("归档状态")]
        public string ArchiveState { get; set; } // ArchiveState
		/// <summary>签字状态</summary>	
		[Description("签字状态")]
        public string SignState { get; set; } // SignState
		/// <summary>提交时间</summary>	
		[Description("提交时间")]
        public DateTime? SubmitDate { get; set; } // SubmitDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? CoSignDate { get; set; } // CoSignDate
		/// <summary>校审通过日期</summary>	
		[Description("校审通过日期")]
        public DateTime? AuditPassDate { get; set; } // AuditPassDate
		/// <summary>归档日期</summary>	
		[Description("归档日期")]
        public DateTime? ArchiveDate { get; set; } // ArchiveDate
		/// <summary>成果状态</summary>	
		[Description("成果状态")]
        public string State { get; set; } // State
		/// <summary>折合A1张数</summary>	
		[Description("折合A1张数")]
        public decimal? ToA1 { get; set; } // ToA1
		/// <summary>文件类型</summary>	
		[Description("文件类型")]
        public string FileType { get; set; } // FileType
		/// <summary>版本号</summary>	
		[Description("版本号")]
        public decimal? Version { get; set; } // Version
		/// <summary>主文件</summary>	
		[Description("主文件")]
        public string MainFile { get; set; } // MainFile
		/// <summary>打印文件</summary>	
		[Description("打印文件")]
        public string PdfFile { get; set; } // PdfFile
		/// <summary></summary>	
		[Description("")]
        public string PlotFile { get; set; } // PlotFile
		/// <summary>浏览文件</summary>	
		[Description("浏览文件")]
        public string SwfFile { get; set; } // SwfFile
		/// <summary>缩略图文件</summary>	
		[Description("缩略图文件")]
        public string ShotSnap { get; set; } // ShotSnap
		/// <summary>引用文件</summary>	
		[Description("引用文件")]
        public string XrefFile { get; set; } // XrefFile
		/// <summary>字体、线型文件</summary>	
		[Description("字体、线型文件")]
        public string FontFile { get; set; } // FontFile
		/// <summary>批注文件</summary>	
		[Description("批注文件")]
        public string MemoData { get; set; } // MemoData
		/// <summary>其他附件</summary>	
		[Description("其他附件")]
        public string Attachments { get; set; } // Attachments
		/// <summary>描述</summary>	
		[Description("描述")]
        public string Description { get; set; } // Description
		/// <summary>全文检索列</summary>	
		[Description("全文检索列")]
        public string FullContext { get; set; } // FullContext
		/// <summary>校审签名信息</summary>	
		[Description("校审签名信息")]
        public string AuditSignUser { get; set; } // AuditSignUser
		/// <summary>会签签名信息</summary>	
		[Description("会签签名信息")]
        public string CoSignUser { get; set; } // CoSignUser
		/// <summary>出图次数</summary>	
		[Description("出图次数")]
        public int? PrintCount { get; set; } // PrintCount
		/// <summary>是否需要会签</summary>	
		[Description("是否需要会签")]
        public string IsCoSign { get; set; } // IsCoSign
		/// <summary>会签专业</summary>	
		[Description("会签专业")]
        public string CoSignMajor { get; set; } // CoSignMajor
		/// <summary>创建人</summary>	
		[Description("创建人")]
        public string CreateUser { get; set; } // CreateUser
		/// <summary>创建人ID</summary>	
		[Description("创建人ID")]
        public string CreateUserID { get; set; } // CreateUserID
		/// <summary>创建日期</summary>	
		[Description("创建日期")]
        public DateTime CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? ModifyDate { get; set; } // ModifyDate
		/// <summary></summary>	
		[Description("")]
        public string ModifyUser { get; set; } // ModifyUser
		/// <summary></summary>	
		[Description("")]
        public string ModifyUserID { get; set; } // ModifyUserID
		/// <summary></summary>	
		[Description("")]
        public string FileSize { get; set; } // FileSize
		/// <summary></summary>	
		[Description("")]
        public string ParentID { get; set; } // ParentID
		/// <summary></summary>	
		[Description("")]
        public string FullID { get; set; } // FullID
		/// <summary></summary>	
		[Description("")]
        public string BatchID { get; set; } // BatchID
		/// <summary></summary>	
		[Description("")]
        public string PDFSignPositionInfo { get; set; } // PDFSignPositionInfo
		/// <summary></summary>	
		[Description("")]
        public string PDFAuditFiles { get; set; } // PDFAuditFiles
		/// <summary></summary>	
		[Description("")]
        public decimal? ParentVersion { get; set; } // ParentVersion
		/// <summary></summary>	
		[Description("")]
        public string PackageCode { get; set; } // PackageCode
		/// <summary></summary>	
		[Description("")]
        public string PackageName { get; set; } // PackageName
		/// <summary></summary>	
		[Description("")]
        public string MonomerCode { get; set; } // MonomerCode
		/// <summary></summary>	
		[Description("")]
        public string ChangeAuditID { get; set; } // ChangeAuditID
		/// <summary></summary>	
		[Description("")]
        public string DwfFile { get; set; } // DwfFile
		/// <summary></summary>	
		[Description("")]
        public string TiffFile { get; set; } // TiffFile
		/// <summary></summary>	
		[Description("")]
        public string PlotSealGroup { get; set; } // PlotSealGroup
		/// <summary></summary>	
		[Description("")]
        public string PlotSealGroupName { get; set; } // PlotSealGroupName
		/// <summary></summary>	
		[Description("")]
        public string SignPdfFile { get; set; } // SignPdfFile
		/// <summary></summary>	
		[Description("")]
        public string FrameAllAttInfo { get; set; } // FrameAllAttInfo
		/// <summary></summary>	
		[Description("")]
        public string AreaInfo { get; set; } // AreaInfo
		/// <summary></summary>	
		[Description("")]
        public string AreaCode { get; set; } // AreaCode
		/// <summary></summary>	
		[Description("")]
        public string DeviceInfo { get; set; } // DeviceInfo
		/// <summary></summary>	
		[Description("")]
        public string DeviceCode { get; set; } // DeviceCode
		/// <summary></summary>	
		[Description("")]
        public string SourceID { get; set; } // SourceID
		/// <summary></summary>	
		[Description("")]
        public string Edition { get; set; } // Edition
		/// <summary></summary>	
		[Description("")]
        public string DrawingCode { get; set; } // DrawingCode
		/// <summary></summary>	
		[Description("")]
        public string PlotSealGroupKey { get; set; } // PlotSealGroupKey

        // Reverse navigation
		[JsonIgnore]
        public virtual ICollection<S_E_ProductVersion> S_E_ProductVersion { get { onS_E_ProductVersionGetting(); return _S_E_ProductVersion;} }
		private ICollection<S_E_ProductVersion> _S_E_ProductVersion;
		partial void onS_E_ProductVersionGetting();


        // Foreign keys
		[JsonIgnore]
        public virtual S_W_WBS S_W_WBS { get; set; } //  WBSID - FK_S_E_Product_S_W_WBS

        public S_E_Product()
        {
            _S_E_ProductVersion = new List<S_E_ProductVersion>();
        }
    }

	/// <summary></summary>	
	[Description("")]
    public partial class S_E_ProductDirectory : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string Code { get; set; } // Code
		/// <summary></summary>	
		[Description("")]
        public string Name { get; set; } // Name
		/// <summary></summary>	
		[Description("")]
        public string FileType { get; set; } // FileType
		/// <summary></summary>	
		[Description("")]
        public decimal? Version { get; set; } // Version
		/// <summary></summary>	
		[Description("")]
        public string FileSize { get; set; } // FileSize
		/// <summary></summary>	
		[Description("")]
        public string Description { get; set; } // Description
		/// <summary></summary>	
		[Description("")]
        public double? Index { get; set; } // Index
		/// <summary></summary>	
		[Description("")]
        public string ProductID { get; set; } // ProductID
		/// <summary></summary>	
		[Description("")]
        public string ProjectInfoID { get; set; } // ProjectInfoID
		/// <summary></summary>	
		[Description("")]
        public string WBSID { get; set; } // WBSID
		/// <summary></summary>	
		[Description("")]
        public string MajorValue { get; set; } // MajorValue
    }

	/// <summary></summary>	
	[Description("")]
    public partial class S_E_ProductVersion : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string Name { get; set; } // Name
		/// <summary></summary>	
		[Description("")]
        public string ProjectInfoID { get; set; } // ProjectInfoID
		/// <summary></summary>	
		[Description("")]
        public string ProductID { get; set; } // ProductID
		/// <summary></summary>	
		[Description("")]
        public string AuditID { get; set; } // AuditID
		/// <summary></summary>	
		[Description("")]
        public decimal? Version { get; set; } // Version
		/// <summary></summary>	
		[Description("")]
        public string MainFile { get; set; } // MainFile
		/// <summary></summary>	
		[Description("")]
        public string PdfFile { get; set; } // PdfFile
		/// <summary></summary>	
		[Description("")]
        public string PlotFile { get; set; } // PlotFile
		/// <summary></summary>	
		[Description("")]
        public string SwfFile { get; set; } // SwfFile
		/// <summary></summary>	
		[Description("")]
        public string ShotSnap { get; set; } // ShotSnap
		/// <summary></summary>	
		[Description("")]
        public string XrefFile { get; set; } // XrefFile
		/// <summary></summary>	
		[Description("")]
        public string FontFile { get; set; } // FontFile
		/// <summary></summary>	
		[Description("")]
        public string Attachments { get; set; } // Attachments
		/// <summary></summary>	
		[Description("")]
        public string AuditSignUser { get; set; } // AuditSignUser
		/// <summary></summary>	
		[Description("")]
        public string CoSignUser { get; set; } // CoSignUser
		/// <summary></summary>	
		[Description("")]
        public string ParentID { get; set; } // ParentID
		/// <summary></summary>	
		[Description("")]
        public string WBSID { get; set; } // WBSID
		/// <summary></summary>	
		[Description("")]
        public string WBSFullID { get; set; } // WBSFullID
		/// <summary></summary>	
		[Description("")]
        public string Code { get; set; } // Code
		/// <summary></summary>	
		[Description("")]
        public string SN { get; set; } // SN
		/// <summary></summary>	
		[Description("")]
        public string MajorValue { get; set; } // MajorValue
		/// <summary></summary>	
		[Description("")]
        public string PhaseValue { get; set; } // PhaseValue
		/// <summary></summary>	
		[Description("")]
        public string SubProjectInfo { get; set; } // SubProjectInfo
		/// <summary></summary>	
		[Description("")]
        public string MonomerInfo { get; set; } // MonomerInfo
		/// <summary></summary>	
		[Description("")]
        public string Designer { get; set; } // Designer
		/// <summary></summary>	
		[Description("")]
        public string DesignerName { get; set; } // DesignerName
		/// <summary></summary>	
		[Description("")]
        public string Collactor { get; set; } // Collactor
		/// <summary></summary>	
		[Description("")]
        public string CollactorName { get; set; } // CollactorName
		/// <summary></summary>	
		[Description("")]
        public string Auditor { get; set; } // Auditor
		/// <summary></summary>	
		[Description("")]
        public string AuditorName { get; set; } // AuditorName
		/// <summary></summary>	
		[Description("")]
        public string Approver { get; set; } // Approver
		/// <summary></summary>	
		[Description("")]
        public string ApproverName { get; set; } // ApproverName
		/// <summary></summary>	
		[Description("")]
        public string CounterSignAuditID { get; set; } // CounterSignAuditID
		/// <summary></summary>	
		[Description("")]
        public string AuditState { get; set; } // AuditState
		/// <summary></summary>	
		[Description("")]
        public string ArchiveState { get; set; } // ArchiveState
		/// <summary></summary>	
		[Description("")]
        public string SignState { get; set; } // SignState
		/// <summary></summary>	
		[Description("")]
        public DateTime? SubmitDate { get; set; } // SubmitDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? CoSignDate { get; set; } // CoSignDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? AuditPassDate { get; set; } // AuditPassDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? ArchiveDate { get; set; } // ArchiveDate
		/// <summary></summary>	
		[Description("")]
        public string State { get; set; } // State
		/// <summary></summary>	
		[Description("")]
        public decimal? ToA1 { get; set; } // ToA1
		/// <summary></summary>	
		[Description("")]
        public string FileType { get; set; } // FileType
		/// <summary></summary>	
		[Description("")]
        public string MemoData { get; set; } // MemoData
		/// <summary></summary>	
		[Description("")]
        public string Description { get; set; } // Description
		/// <summary></summary>	
		[Description("")]
        public string FullContext { get; set; } // FullContext
		/// <summary></summary>	
		[Description("")]
        public int? PrintCount { get; set; } // PrintCount
		/// <summary></summary>	
		[Description("")]
        public string IsCoSign { get; set; } // IsCoSign
		/// <summary></summary>	
		[Description("")]
        public string CoSignMajor { get; set; } // CoSignMajor
		/// <summary></summary>	
		[Description("")]
        public string FileSize { get; set; } // FileSize
		/// <summary></summary>	
		[Description("")]
        public string FullID { get; set; } // FullID
		/// <summary></summary>	
		[Description("")]
        public string BatchID { get; set; } // BatchID
		/// <summary></summary>	
		[Description("")]
        public string PDFSignPositionInfo { get; set; } // PDFSignPositionInfo
		/// <summary></summary>	
		[Description("")]
        public string PDFAuditFiles { get; set; } // PDFAuditFiles
		/// <summary></summary>	
		[Description("")]
        public decimal? ParentVersion { get; set; } // ParentVersion
		/// <summary></summary>	
		[Description("")]
        public string PackageCode { get; set; } // PackageCode
		/// <summary></summary>	
		[Description("")]
        public string PackageName { get; set; } // PackageName
		/// <summary></summary>	
		[Description("")]
        public string MonomerCode { get; set; } // MonomerCode
		/// <summary></summary>	
		[Description("")]
        public string CreateUser { get; set; } // CreateUser
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get; set; } // CreateUserID
		/// <summary></summary>	
		[Description("")]
        public string ModifyUser { get; set; } // ModifyUser
		/// <summary></summary>	
		[Description("")]
        public string ModifyUserID { get; set; } // ModifyUserID
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? ModifyDate { get; set; } // ModifyDate
		/// <summary></summary>	
		[Description("")]
        public string ChangeAuditID { get; set; } // ChangeAuditID
		/// <summary></summary>	
		[Description("")]
        public string CoSignState { get; set; } // CoSignState
		/// <summary></summary>	
		[Description("")]
        public string DwfFile { get; set; } // DwfFile
		/// <summary></summary>	
		[Description("")]
        public string TiffFile { get; set; } // TiffFile
		/// <summary></summary>	
		[Description("")]
        public string PlotSealGroup { get; set; } // PlotSealGroup
		/// <summary></summary>	
		[Description("")]
        public string PlotSealGroupName { get; set; } // PlotSealGroupName
		/// <summary></summary>	
		[Description("")]
        public string PrintState { get; set; } // PrintState
		/// <summary></summary>	
		[Description("")]
        public string SignPdfFile { get; set; } // SignPdfFile
		/// <summary></summary>	
		[Description("")]
        public string FrameAllAttInfo { get; set; } // FrameAllAttInfo
		/// <summary></summary>	
		[Description("")]
        public string SourceID { get; set; } // SourceID
		/// <summary></summary>	
		[Description("")]
        public string Edition { get; set; } // Edition
		/// <summary></summary>	
		[Description("")]
        public string AreaInfo { get; set; } // AreaInfo
		/// <summary></summary>	
		[Description("")]
        public string AreaCode { get; set; } // AreaCode
		/// <summary></summary>	
		[Description("")]
        public string DeviceInfo { get; set; } // DeviceInfo
		/// <summary></summary>	
		[Description("")]
        public string DeviceCode { get; set; } // DeviceCode
		/// <summary></summary>	
		[Description("")]
        public string DrawingCode { get; set; } // DrawingCode
		/// <summary></summary>	
		[Description("")]
        public string PlotSealGroupKey { get; set; } // PlotSealGroupKey

        // Foreign keys
		[JsonIgnore]
        public virtual S_E_Product S_E_Product { get; set; } //  ProductID - FK_S_E_ProductVersion_S_E_Product
    }

	/// <summary></summary>	
	[Description("")]
    public partial class S_E_PublishInfo : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string ProjectInfoID { get; set; } // ProjectInfoID
		/// <summary></summary>	
		[Description("")]
        public string ProjectInfoName { get; set; } // ProjectInfoName
		/// <summary></summary>	
		[Description("")]
        public string ProjectInfoCode { get; set; } // ProjectInfoCode
		/// <summary></summary>	
		[Description("")]
        public string PublishType { get; set; } // PublishType
		/// <summary></summary>	
		[Description("")]
        public string WBSID { get; set; } // WBSID
		/// <summary></summary>	
		[Description("")]
        public string RelateInfoID { get; set; } // RelateInfoID
		/// <summary></summary>	
		[Description("")]
        public int BelongYear { get; set; } // BelongYear
		/// <summary></summary>	
		[Description("")]
        public int BelongQuarter { get; set; } // BelongQuarter
		/// <summary></summary>	
		[Description("")]
        public int BelongMonth { get; set; } // BelongMonth
		/// <summary></summary>	
		[Description("")]
        public DateTime PublishDate { get; set; } // PublishDate
		/// <summary></summary>	
		[Description("")]
        public decimal? A0 { get; set; } // A0
		/// <summary></summary>	
		[Description("")]
        public decimal? A1 { get; set; } // A1
		/// <summary></summary>	
		[Description("")]
        public decimal? A2 { get; set; } // A2
		/// <summary></summary>	
		[Description("")]
        public decimal? A3 { get; set; } // A3
		/// <summary></summary>	
		[Description("")]
        public decimal? A4 { get; set; } // A4
		/// <summary></summary>	
		[Description("")]
        public decimal? ToA1 { get; set; } // ToA1
		/// <summary></summary>	
		[Description("")]
        public decimal? UniPrice { get; set; } // UniPrice
		/// <summary></summary>	
		[Description("")]
        public decimal? SummaryCost { get; set; } // SummaryCost
		/// <summary></summary>	
		[Description("")]
        public string MajorValue { get; set; } // MajorValue
		/// <summary></summary>	
		[Description("")]
        public string MajorName { get; set; } // MajorName
		/// <summary></summary>	
		[Description("")]
        public string PhaseValue { get; set; } // PhaseValue
		/// <summary></summary>	
		[Description("")]
        public string PhaseName { get; set; } // PhaseName
		/// <summary></summary>	
		[Description("")]
        public string SubValue { get; set; } // SubValue
		/// <summary></summary>	
		[Description("")]
        public string SubName { get; set; } // SubName

        // Reverse navigation
		[JsonIgnore]
        public virtual ICollection<S_E_PublishInfoDetail> S_E_PublishInfoDetail { get { onS_E_PublishInfoDetailGetting(); return _S_E_PublishInfoDetail;} }
		private ICollection<S_E_PublishInfoDetail> _S_E_PublishInfoDetail;
		partial void onS_E_PublishInfoDetailGetting();


        // Foreign keys
		[JsonIgnore]
        public virtual S_I_ProjectInfo S_I_ProjectInfo { get; set; } //  ProjectInfoID - FK_S_E_PublishInfo_S_E_PublishInfo

        public S_E_PublishInfo()
        {
            _S_E_PublishInfoDetail = new List<S_E_PublishInfoDetail>();
        }
    }

	/// <summary></summary>	
	[Description("")]
    public partial class S_E_PublishInfoDetail : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string S_E_PublishInfoID { get; set; } // S_E_PublishInfoID
		/// <summary>图纸ID</summary>	
		[Description("图纸ID")]
        public string ProductID { get; set; } // ProductID
		/// <summary>图纸名称</summary>	
		[Description("图纸名称")]
        public string ProductName { get; set; } // ProductName
		/// <summary>图纸编号</summary>	
		[Description("图纸编号")]
        public string ProductCode { get; set; } // ProductCode
		/// <summary>打印数量</summary>	
		[Description("打印数量")]
        public int Count { get; set; } // Count
		/// <summary>是否已打印</summary>	
		[Description("是否已打印")]
        public bool Printed { get; set; } // Printed
		/// <summary>项目名称</summary>	
		[Description("项目名称")]
        public string ProjectInfoName { get; set; } // ProjectInfoName
		/// <summary>项目编号</summary>	
		[Description("项目编号")]
        public string ProjectCode { get; set; } // ProjectCode
		/// <summary>项目负责人</summary>	
		[Description("项目负责人")]
        public string ProjectManager { get; set; } // ProjectManager
		/// <summary>专业编码</summary>	
		[Description("专业编码")]
        public string MajorCode { get; set; } // MajorCode
		/// <summary>阶段</summary>	
		[Description("阶段")]
        public string StepName { get; set; } // StepName
		/// <summary>图纸规格</summary>	
		[Description("图纸规格")]
        public string PaperSize { get; set; } // PaperSize
		/// <summary>是否竖打</summary>	
		[Description("是否竖打")]
        public bool IsVertical { get; set; } // IsVertical
		/// <summary>出图单编号</summary>	
		[Description("出图单编号")]
        public string SerialNumber { get; set; } // SerialNumber
		/// <summary>图纸提供人名称</summary>	
		[Description("图纸提供人名称")]
        public string DesingerName { get; set; } // DesingerName
		/// <summary>提交日期</summary>	
		[Description("提交日期")]
        public DateTime? PublishDate { get; set; } // PublishDate
		/// <summary>项目部门</summary>	
		[Description("项目部门")]
        public string ChargeDeptName { get; set; } // ChargeDeptName
		/// <summary>pdf文件名称</summary>	
		[Description("pdf文件名称")]
        public string PdfFile { get; set; } // PdfFile
		/// <summary>plot文件名称</summary>	
		[Description("plot文件名称")]
        public string PlotFile { get; set; } // PlotFile
		/// <summary></summary>	
		[Description("")]
        public decimal? ProductVersion { get; set; } // ProductVersion

        // Foreign keys
		[JsonIgnore]
        public virtual S_E_PublishInfo S_E_PublishInfo { get; set; } //  S_E_PublishInfoID - FK_S_E_PublishInfoDetail_S_E_PublishInfo
    }

	/// <summary></summary>	
	[Description("")]
    public partial class S_EP_PDFSignLog : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string ProductID { get; set; } // ProductID
		/// <summary></summary>	
		[Description("")]
        public decimal? Version { get; set; } // Version
		/// <summary></summary>	
		[Description("")]
        public string VersionID { get; set; } // VersionID
		/// <summary></summary>	
		[Description("")]
        public string ErrorMessage { get; set; } // ErrorMessage
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateDate { get; set; } // CreateDate
    }

	/// <summary>出图章组合</summary>	
	[Description("出图章组合")]
    public partial class S_EP_PlotSealGroup : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? ModifyDate { get; set; } // ModifyDate
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get; set; } // CreateUserID
		/// <summary></summary>	
		[Description("")]
        public string CreateUser { get; set; } // CreateUser
		/// <summary></summary>	
		[Description("")]
        public string ModifyUserID { get; set; } // ModifyUserID
		/// <summary></summary>	
		[Description("")]
        public string ModifyUser { get; set; } // ModifyUser
		/// <summary></summary>	
		[Description("")]
        public string OrgID { get; set; } // OrgID
		/// <summary></summary>	
		[Description("")]
        public string CompanyID { get; set; } // CompanyID
		/// <summary></summary>	
		[Description("")]
        public string FlowPhase { get; set; } // FlowPhase
		/// <summary></summary>	
		[Description("")]
        public string FlowInfo { get; set; } // FlowInfo
		/// <summary></summary>	
		[Description("")]
        public string StepName { get; set; } // StepName
		/// <summary>名称</summary>	
		[Description("名称")]
        public string Name { get; set; } // Name
		/// <summary>编号</summary>	
		[Description("编号")]
        public string Code { get; set; } // Code
		/// <summary>组合</summary>	
		[Description("组合")]
        public string GroupInfo { get; set; } // GroupInfo
		/// <summary>单章组合主章ID</summary>	
		[Description("单章组合主章ID")]
        public string MainPlotSealID { get; set; } // MainPlotSealID

        // Reverse navigation
		[JsonIgnore]
        public virtual ICollection<S_EP_PlotSealGroup_GroupInfo> S_EP_PlotSealGroup_GroupInfo { get { onS_EP_PlotSealGroup_GroupInfoGetting(); return _S_EP_PlotSealGroup_GroupInfo;} }
		private ICollection<S_EP_PlotSealGroup_GroupInfo> _S_EP_PlotSealGroup_GroupInfo;
		partial void onS_EP_PlotSealGroup_GroupInfoGetting();


        public S_EP_PlotSealGroup()
        {
            _S_EP_PlotSealGroup_GroupInfo = new List<S_EP_PlotSealGroup_GroupInfo>();
        }
    }

	/// <summary>组合</summary>	
	[Description("组合")]
    public partial class S_EP_PlotSealGroup_GroupInfo : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string S_EP_PlotSealGroupID { get; set; } // S_EP_PlotSealGroupID
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string IsReleased { get; set; } // IsReleased
		/// <summary>是否主章</summary>	
		[Description("是否主章")]
        public string IsMain { get; set; } // IsMain
		/// <summary>出图章ID</summary>	
		[Description("出图章ID")]
        public string PlotSeal { get; set; } // PlotSeal
		/// <summary>名称</summary>	
		[Description("名称")]
        public string Name { get; set; } // Name
		/// <summary>编号</summary>	
		[Description("编号")]
        public string Code { get; set; } // Code
		/// <summary>图块Key</summary>	
		[Description("图块Key")]
        public string BlockKey { get; set; } // BlockKey
		/// <summary>类型</summary>	
		[Description("类型")]
        public string Type { get; set; } // Type
		/// <summary>所属人</summary>	
		[Description("所属人")]
        public string BelongUser { get; set; } // BelongUser
		/// <summary>所属人名称</summary>	
		[Description("所属人名称")]
        public string BelongUserName { get; set; } // BelongUserName
		/// <summary>认证信息</summary>	
		[Description("认证信息")]
        public string AuthInfo { get; set; } // AuthInfo
		/// <summary>长度</summary>	
		[Description("长度")]
        public decimal? Width { get; set; } // Width
		/// <summary>高度</summary>	
		[Description("高度")]
        public decimal? Height { get; set; } // Height
		/// <summary>偏移X(mm)</summary>	
		[Description("偏移X(mm)")]
        public decimal? CorrectPosX { get; set; } // CorrectPosX
		/// <summary>偏移Y(mm)</summary>	
		[Description("偏移Y(mm)")]
        public decimal? CorrectPosY { get; set; } // CorrectPosY

        // Foreign keys
		[JsonIgnore]
        public virtual S_EP_PlotSealGroup S_EP_PlotSealGroup { get; set; } //  S_EP_PlotSealGroupID - FK_S_EP_PlotSealGroup_GroupInfo_S_EP_PlotSealGroup
    }

	/// <summary>出图章信息</summary>	
	[Description("出图章信息")]
    public partial class S_EP_PlotSealInfo : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? ModifyDate { get; set; } // ModifyDate
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get; set; } // CreateUserID
		/// <summary></summary>	
		[Description("")]
        public string CreateUser { get; set; } // CreateUser
		/// <summary></summary>	
		[Description("")]
        public string ModifyUserID { get; set; } // ModifyUserID
		/// <summary></summary>	
		[Description("")]
        public string ModifyUser { get; set; } // ModifyUser
		/// <summary></summary>	
		[Description("")]
        public string OrgID { get; set; } // OrgID
		/// <summary></summary>	
		[Description("")]
        public string CompanyID { get; set; } // CompanyID
		/// <summary></summary>	
		[Description("")]
        public string FlowPhase { get; set; } // FlowPhase
		/// <summary></summary>	
		[Description("")]
        public string FlowInfo { get; set; } // FlowInfo
		/// <summary></summary>	
		[Description("")]
        public string StepName { get; set; } // StepName
		/// <summary>印章名称</summary>	
		[Description("印章名称")]
        public string Name { get; set; } // Name
		/// <summary>印章编号</summary>	
		[Description("印章编号")]
        public string Code { get; set; } // Code
		/// <summary>印章类型</summary>	
		[Description("印章类型")]
        public string Type { get; set; } // Type
		/// <summary>所属人</summary>	
		[Description("所属人")]
        public string BelongUser { get; set; } // BelongUser
		/// <summary>所属人名称</summary>	
		[Description("所属人名称")]
        public string BelongUserName { get; set; } // BelongUserName
		/// <summary>印章</summary>	
		[Description("印章")]
        public byte[] SealInfo { get; set; } // SealInfo
		/// <summary>认证信息</summary>	
		[Description("认证信息")]
        public string AuthInfo { get; set; } // AuthInfo
		/// <summary>长度</summary>	
		[Description("长度")]
        public decimal? Width { get; set; } // Width
		/// <summary>高度</summary>	
		[Description("高度")]
        public decimal? Height { get; set; } // Height
		/// <summary>图块Key</summary>	
		[Description("图块Key")]
        public string BlockKey { get; set; } // BlockKey
		/// <summary>资质类型</summary>	
		[Description("资质类型")]
        public string QualityType { get; set; } // QualityType
		/// <summary>备注</summary>	
		[Description("备注")]
        public string Remark { get; set; } // Remark
		/// <summary>过期时间</summary>	
		[Description("过期时间")]
        public DateTime? ExpireTime { get; set; } // ExpireTime
		/// <summary>上海CA中心对应关系</summary>	
		[Description("上海CA中心对应关系")]
        public int? Relational { get; set; } // Relational
    }

	/// <summary>盖章模板</summary>	
	[Description("盖章模板")]
    public partial class S_EP_PlotSealTemplate : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? ModifyDate { get; set; } // ModifyDate
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get; set; } // CreateUserID
		/// <summary></summary>	
		[Description("")]
        public string CreateUser { get; set; } // CreateUser
		/// <summary></summary>	
		[Description("")]
        public string ModifyUserID { get; set; } // ModifyUserID
		/// <summary></summary>	
		[Description("")]
        public string ModifyUser { get; set; } // ModifyUser
		/// <summary></summary>	
		[Description("")]
        public string OrgID { get; set; } // OrgID
		/// <summary></summary>	
		[Description("")]
        public string CompanyID { get; set; } // CompanyID
		/// <summary></summary>	
		[Description("")]
        public string FlowPhase { get; set; } // FlowPhase
		/// <summary></summary>	
		[Description("")]
        public string FlowInfo { get; set; } // FlowInfo
		/// <summary></summary>	
		[Description("")]
        public string StepName { get; set; } // StepName
		/// <summary>名称</summary>	
		[Description("名称")]
        public string Name { get; set; } // Name
		/// <summary>编号</summary>	
		[Description("编号")]
        public string Code { get; set; } // Code
		/// <summary>组合信息</summary>	
		[Description("组合信息")]
        public string GroupList { get; set; } // GroupList
		/// <summary>项目ID</summary>	
		[Description("项目ID")]
        public string ProjectInfoID { get; set; } // ProjectInfoID
		/// <summary>模板背景文件</summary>	
		[Description("模板背景文件")]
        public string TempFile { get; set; } // TempFile
		/// <summary>备注</summary>	
		[Description("备注")]
        public string Remark { get; set; } // Remark
		/// <summary>图签配置</summary>	
		[Description("图签配置")]
        public string BorderConfig { get; set; } // BorderConfig

        // Reverse navigation
		[JsonIgnore]
        public virtual ICollection<S_EP_PlotSealTemplate_GroupList> S_EP_PlotSealTemplate_GroupList { get { onS_EP_PlotSealTemplate_GroupListGetting(); return _S_EP_PlotSealTemplate_GroupList;} }
		private ICollection<S_EP_PlotSealTemplate_GroupList> _S_EP_PlotSealTemplate_GroupList;
		partial void onS_EP_PlotSealTemplate_GroupListGetting();


        public S_EP_PlotSealTemplate()
        {
            _S_EP_PlotSealTemplate_GroupList = new List<S_EP_PlotSealTemplate_GroupList>();
        }
    }

	/// <summary>组合信息</summary>	
	[Description("组合信息")]
    public partial class S_EP_PlotSealTemplate_GroupList : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string S_EP_PlotSealTemplateID { get; set; } // S_EP_PlotSealTemplateID
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string IsReleased { get; set; } // IsReleased
		/// <summary>组合ID</summary>	
		[Description("组合ID")]
        public string GroupID { get; set; } // GroupID
		/// <summary>图章名称</summary>	
		[Description("图章名称")]
        public string GroupName { get; set; } // GroupName
		/// <summary>坐标X(mm)</summary>	
		[Description("坐标X(mm)")]
        public decimal? PositionXs { get; set; } // PositionXs
		/// <summary>坐标Y(mm)</summary>	
		[Description("坐标Y(mm)")]
        public decimal? PositionYs { get; set; } // PositionYs
		/// <summary>块名</summary>	
		[Description("块名")]
        public string BlockKey { get; set; } // BlockKey
		/// <summary>类型</summary>	
		[Description("类型")]
        public string Category { get; set; } // Category
		/// <summary>长度(mm)</summary>	
		[Description("长度(mm)")]
        public decimal? Width { get; set; } // Width
		/// <summary>高度(mm)</summary>	
		[Description("高度(mm)")]
        public decimal? Height { get; set; } // Height
		/// <summary>字高</summary>	
		[Description("字高")]
        public decimal? CharHeight { get; set; } // CharHeight
		/// <summary>角度(单位:°)</summary>	
		[Description("角度(单位:°)")]
        public decimal? Angle { get; set; } // Angle
		/// <summary>页码</summary>	
		[Description("页码")]
        public int? PageNum { get; set; } // PageNum

        // Foreign keys
		[JsonIgnore]
        public virtual S_EP_PlotSealTemplate S_EP_PlotSealTemplate { get; set; } //  S_EP_PlotSealTemplateID - FK_S_EP_PlotSealTemplate_GroupList_S_EP_PlotSealTemplate
    }

	/// <summary>外发管理信息</summary>	
	[Description("外发管理信息")]
    public partial class S_EP_PublishInfo : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? ModifyDate { get; set; } // ModifyDate
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get; set; } // CreateUserID
		/// <summary></summary>	
		[Description("")]
        public string CreateUser { get; set; } // CreateUser
		/// <summary></summary>	
		[Description("")]
        public string ModifyUserID { get; set; } // ModifyUserID
		/// <summary></summary>	
		[Description("")]
        public string ModifyUser { get; set; } // ModifyUser
		/// <summary></summary>	
		[Description("")]
        public string OrgID { get; set; } // OrgID
		/// <summary></summary>	
		[Description("")]
        public string CompanyID { get; set; } // CompanyID
		/// <summary></summary>	
		[Description("")]
        public string FlowPhase { get; set; } // FlowPhase
		/// <summary></summary>	
		[Description("")]
        public string FlowInfo { get; set; } // FlowInfo
		/// <summary></summary>	
		[Description("")]
        public string StepName { get; set; } // StepName
		/// <summary>编号</summary>	
		[Description("编号")]
        public string SerialNumber { get; set; } // SerialNumber
		/// <summary>项目ID</summary>	
		[Description("项目ID")]
        public string ProjectInfoID { get; set; } // ProjectInfoID
		/// <summary>项目名称</summary>	
		[Description("项目名称")]
        public string Project { get; set; } // Project
		/// <summary>项目名称名称</summary>	
		[Description("项目名称名称")]
        public string ProjectName { get; set; } // ProjectName
		/// <summary>项目编号</summary>	
		[Description("项目编号")]
        public string ProjectCode { get; set; } // ProjectCode
		/// <summary>设计专业</summary>	
		[Description("设计专业")]
        public string MajorCode { get; set; } // MajorCode
		/// <summary>MajorName</summary>	
		[Description("MajorName")]
        public string MajorName { get; set; } // MajorName
		/// <summary>申请人</summary>	
		[Description("申请人")]
        public string ApplyUser { get; set; } // ApplyUser
		/// <summary>申请人名称</summary>	
		[Description("申请人名称")]
        public string ApplyUserName { get; set; } // ApplyUserName
		/// <summary>申请部门</summary>	
		[Description("申请部门")]
        public string ApplyDept { get; set; } // ApplyDept
		/// <summary>申请部门名称</summary>	
		[Description("申请部门名称")]
        public string ApplyDeptName { get; set; } // ApplyDeptName
		/// <summary>申请日期</summary>	
		[Description("申请日期")]
        public DateTime? ApplyDate { get; set; } // ApplyDate
		/// <summary>图纸份数</summary>	
		[Description("图纸份数")]
        public int? ProductCount { get; set; } // ProductCount
		/// <summary>成本</summary>	
		[Description("成本")]
        public decimal? CostAmount { get; set; } // CostAmount
		/// <summary>实际成本</summary>	
		[Description("实际成本")]
        public decimal? RealCostAmount { get; set; } // RealCostAmount
		/// <summary>备注</summary>	
		[Description("备注")]
        public string Remark { get; set; } // Remark
		/// <summary>图幅Json</summary>	
		[Description("图幅Json")]
        public string BorderSizeJson { get; set; } // BorderSizeJson
		/// <summary>折合A1</summary>	
		[Description("折合A1")]
        public decimal? ToA1 { get; set; } // ToA1
		/// <summary>操作人</summary>	
		[Description("操作人")]
        public string OperateUser { get; set; } // OperateUser
		/// <summary>操作人名称</summary>	
		[Description("操作人名称")]
        public string OperateUserName { get; set; } // OperateUserName
		/// <summary>提交日期</summary>	
		[Description("提交日期")]
        public DateTime? SubmitTime { get; set; } // SubmitTime
		/// <summary>打印日期</summary>	
		[Description("打印日期")]
        public DateTime? PlotTime { get; set; } // PlotTime
		/// <summary>结算日期</summary>	
		[Description("结算日期")]
        public DateTime? ConfirmTime { get; set; } // ConfirmTime
		/// <summary>出图日期</summary>	
		[Description("出图日期")]
        public DateTime? PublishTime { get; set; } // PublishTime
		/// <summary>费用科目名称</summary>	
		[Description("费用科目名称")]
        public string CostName { get; set; } // CostName
		/// <summary>费用科目编号</summary>	
		[Description("费用科目编号")]
        public string CostCode { get; set; } // CostCode
		/// <summary>BelongYear</summary>	
		[Description("BelongYear")]
        public int? BelongYear { get; set; } // BelongYear
		/// <summary>BelongMonth</summary>	
		[Description("BelongMonth")]
        public int? BelongMonth { get; set; } // BelongMonth
		/// <summary>BelongQuarter</summary>	
		[Description("BelongQuarter")]
        public int? BelongQuarter { get; set; } // BelongQuarter
		/// <summary>FormCode</summary>	
		[Description("FormCode")]
        public string FormCode { get; set; } // FormCode
		/// <summary>送审状态</summary>	
		[Description("送审状态")]
        public string TrialStatus { get; set; } // TrialStatus
		/// <summary>送审人</summary>	
		[Description("送审人")]
        public string TrialPeople { get; set; } // TrialPeople
		/// <summary>送审时间</summary>	
		[Description("送审时间")]
        public string TrialTime { get; set; } // TrialTime
		/// <summary>送审备注</summary>	
		[Description("送审备注")]
        public string TrialRemarks { get; set; } // TrialRemarks
		/// <summary>外发人</summary>	
		[Description("外发人")]
        public string PutOutPeople { get; set; } // PutOutPeople
		/// <summary>外发时间</summary>	
		[Description("外发时间")]
        public DateTime? PutOutTime { get; set; } // PutOutTime
		/// <summary>外发备注</summary>	
		[Description("外发备注")]
        public string PutOutRemarks { get; set; } // PutOutRemarks
		/// <summary>外发状态</summary>	
		[Description("外发状态")]
        public string PutOutStatus { get; set; } // PutOutStatus

        // Reverse navigation
		[JsonIgnore]
        public virtual ICollection<S_EP_PublishInfo_PriceDetail> S_EP_PublishInfo_PriceDetail { get { onS_EP_PublishInfo_PriceDetailGetting(); return _S_EP_PublishInfo_PriceDetail;} }
		private ICollection<S_EP_PublishInfo_PriceDetail> _S_EP_PublishInfo_PriceDetail;
		partial void onS_EP_PublishInfo_PriceDetailGetting();

		[JsonIgnore]
        public virtual ICollection<S_EP_PublishInfo_Products> S_EP_PublishInfo_Products { get { onS_EP_PublishInfo_ProductsGetting(); return _S_EP_PublishInfo_Products;} }
		private ICollection<S_EP_PublishInfo_Products> _S_EP_PublishInfo_Products;
		partial void onS_EP_PublishInfo_ProductsGetting();


        public S_EP_PublishInfo()
        {
            _S_EP_PublishInfo_PriceDetail = new List<S_EP_PublishInfo_PriceDetail>();
            _S_EP_PublishInfo_Products = new List<S_EP_PublishInfo_Products>();
        }
    }

	/// <summary>委托明细</summary>	
	[Description("委托明细")]
    public partial class S_EP_PublishInfo_PriceDetail : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string S_EP_PublishInfoID { get; set; } // S_EP_PublishInfoID
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string IsReleased { get; set; } // IsReleased
		/// <summary>类型</summary>	
		[Description("类型")]
        public string PublicationType { get; set; } // PublicationType
		/// <summary>规格</summary>	
		[Description("规格")]
        public string Specification { get; set; } // Specification
		/// <summary>张数</summary>	
		[Description("张数")]
        public decimal? Num { get; set; } // Num
		/// <summary>单价(元)</summary>	
		[Description("单价(元)")]
        public decimal? Price { get; set; } // Price
		/// <summary>合计(元)</summary>	
		[Description("合计(元)")]
        public decimal? Sum { get; set; } // Sum
		/// <summary>总成本(元)</summary>	
		[Description("总成本(元)")]
        public decimal? SumCost { get; set; } // SumCost
		/// <summary>结算张数</summary>	
		[Description("结算张数")]
        public decimal? RealNum { get; set; } // RealNum
		/// <summary>结算单价(元)</summary>	
		[Description("结算单价(元)")]
        public decimal? RealPrice { get; set; } // RealPrice
		/// <summary>结算合计(元)</summary>	
		[Description("结算合计(元)")]
        public decimal? RealSum { get; set; } // RealSum
		/// <summary>结算总成本(元)</summary>	
		[Description("结算总成本(元)")]
        public decimal? RealSumCost { get; set; } // RealSumCost
		/// <summary>CostID</summary>	
		[Description("CostID")]
        public string CostID { get; set; } // CostID

        // Foreign keys
		[JsonIgnore]
        public virtual S_EP_PublishInfo S_EP_PublishInfo { get; set; } //  S_EP_PublishInfoID - FK_S_EP_PublishInfo_PriceDetail_S_EP_PublishInfo
    }

	/// <summary>成果子表</summary>	
	[Description("成果子表")]
    public partial class S_EP_PublishInfo_Products : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string S_EP_PublishInfoID { get; set; } // S_EP_PublishInfoID
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string IsReleased { get; set; } // IsReleased
		/// <summary>图纸ID</summary>	
		[Description("图纸ID")]
        public string ProductID { get; set; } // ProductID
		/// <summary>版本ID</summary>	
		[Description("版本ID")]
        public string VersionID { get; set; } // VersionID
		/// <summary>成果名称</summary>	
		[Description("成果名称")]
        public string Name { get; set; } // Name
		/// <summary>成果编号</summary>	
		[Description("成果编号")]
        public string Code { get; set; } // Code
		/// <summary>版本号</summary>	
		[Description("版本号")]
        public decimal? Version { get; set; } // Version
		/// <summary>成果文件</summary>	
		[Description("成果文件")]
        public string MainFile { get; set; } // MainFile
		/// <summary>PDF文件</summary>	
		[Description("PDF文件")]
        public string PdfFile { get; set; } // PdfFile
		/// <summary>Plot文件</summary>	
		[Description("Plot文件")]
        public string PlotFile { get; set; } // PlotFile
		/// <summary>Xref文件</summary>	
		[Description("Xref文件")]
        public string XrefFile { get; set; } // XrefFile
		/// <summary>Dwf文件</summary>	
		[Description("Dwf文件")]
        public string DwfFile { get; set; } // DwfFile
		/// <summary>Tiff文件</summary>	
		[Description("Tiff文件")]
        public string TiffFile { get; set; } // TiffFile
		/// <summary>已签章</summary>	
		[Description("已签章")]
        public string SignPdfFile { get; set; } // SignPdfFile
		/// <summary>出图章组合</summary>	
		[Description("出图章组合")]
        public string PlotSealGroupName { get; set; } // PlotSealGroupName

        // Foreign keys
		[JsonIgnore]
        public virtual S_EP_PublishInfo S_EP_PublishInfo { get; set; } //  S_EP_PublishInfoID - FK_S_EP_PublishInfo_Products_S_EP_PublishInfo
    }

	/// <summary>图框配置</summary>	
	[Description("图框配置")]
    public partial class S_F_BorderConfig : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary>图框类型</summary>	
		[Description("图框类型")]
        public string BorderType { get; set; } // BorderType
		/// <summary>图幅</summary>	
		[Description("图幅")]
        public string BorderSize { get; set; } // BorderSize
		/// <summary>类型</summary>	
		[Description("类型")]
        public string Category { get; set; } // Category
		/// <summary>长度</summary>	
		[Description("长度")]
        public decimal? Width { get; set; } // Width
		/// <summary>高度</summary>	
		[Description("高度")]
        public decimal? Height { get; set; } // Height
		/// <summary>角度</summary>	
		[Description("角度")]
        public decimal? Angle { get; set; } // Angle
		/// <summary>偏移X</summary>	
		[Description("偏移X")]
        public decimal? CorrectPosX { get; set; } // CorrectPosX
		/// <summary>偏移Y</summary>	
		[Description("偏移Y")]
        public decimal? CorrectPosY { get; set; } // CorrectPosY
		/// <summary>字高</summary>	
		[Description("字高")]
        public decimal? CharHeight { get; set; } // CharHeight
		/// <summary>日期区域长度</summary>	
		[Description("日期区域长度")]
        public decimal? DateExtWidth { get; set; } // DateExtWidth
		/// <summary>日期区域高度</summary>	
		[Description("日期区域高度")]
        public decimal? DateExtHeight { get; set; } // DateExtHeight
		/// <summary>专业区域长度</summary>	
		[Description("专业区域长度")]
        public decimal? MajorExtWidth { get; set; } // MajorExtWidth
		/// <summary>专业区域高度</summary>	
		[Description("专业区域高度")]
        public decimal? MajorExtHeight { get; set; } // MajorExtHeight
		/// <summary></summary>	
		[Description("")]
        public string ManageInfoID { get; set; } // ManageInfoID
		/// <summary></summary>	
		[Description("")]
        public string DefaultTemplateName { get; set; } // DefaultTemplateName
		/// <summary></summary>	
		[Description("")]
        public string CurrentDefault { get; set; } // CurrentDefault
		/// <summary></summary>	
		[Description("")]
        public decimal? CharHeightCAD { get; set; } // CharHeightCAD
    }

	/// <summary></summary>	
	[Description("")]
    public partial class S_F_FrameInfo : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string FrameTypeName { get; set; } // FrameTypeName
		/// <summary></summary>	
		[Description("")]
        public string ConfigFileID { get; set; } // ConfigFileID
		/// <summary></summary>	
		[Description("")]
        public string Designer { get; set; } // Designer
		/// <summary></summary>	
		[Description("")]
        public string DesignerID { get; set; } // DesignerID

        // Reverse navigation
		[JsonIgnore]
        public virtual ICollection<S_F_FrameInfo_FrameManageInfo> S_F_FrameInfo_FrameManageInfo { get { onS_F_FrameInfo_FrameManageInfoGetting(); return _S_F_FrameInfo_FrameManageInfo;} }
		private ICollection<S_F_FrameInfo_FrameManageInfo> _S_F_FrameInfo_FrameManageInfo;
		partial void onS_F_FrameInfo_FrameManageInfoGetting();


        public S_F_FrameInfo()
        {
            _S_F_FrameInfo_FrameManageInfo = new List<S_F_FrameInfo_FrameManageInfo>();
        }
    }

	/// <summary></summary>	
	[Description("")]
    public partial class S_F_FrameInfo_FrameManageInfo : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string S_F_FrameInfoID { get; set; } // S_F_FrameInfoID
		/// <summary></summary>	
		[Description("")]
        public string FrameType { get; set; } // FrameType
		/// <summary></summary>	
		[Description("")]
        public string TemplateName { get; set; } // TemplateName
		/// <summary></summary>	
		[Description("")]
        public string FileID { get; set; } // FileID
		/// <summary></summary>	
		[Description("")]
        public string Designer { get; set; } // Designer
		/// <summary></summary>	
		[Description("")]
        public string DesignerID { get; set; } // DesignerID
		/// <summary></summary>	
		[Description("")]
        public string Name { get; set; } // Name
		/// <summary></summary>	
		[Description("")]
        public string BorderType { get; set; } // BorderType
		/// <summary></summary>	
		[Description("")]
        public string Size { get; set; } // Size
		/// <summary></summary>	
		[Description("")]
        public int? Version { get; set; } // Version
		/// <summary></summary>	
		[Description("")]
        public string ProjectInfoID { get; set; } // ProjectInfoID
		/// <summary></summary>	
		[Description("")]
        public string ProjectInfoName { get; set; } // ProjectInfoName
		/// <summary></summary>	
		[Description("")]
        public string Remark { get; set; } // Remark
		/// <summary></summary>	
		[Description("")]
        public string FrameInfoJson { get; set; } // FrameInfoJson
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateTime { get; set; } // CreateTime
		/// <summary></summary>	
		[Description("")]
        public string Category { get; set; } // Category

        // Foreign keys
		[JsonIgnore]
        public virtual S_F_FrameInfo S_F_FrameInfo { get; set; } //  S_F_FrameInfoID - FK__S_F_Frame__S_F_F__5DCAEF64
    }

	/// <summary></summary>	
	[Description("")]
    public partial class S_I_FileConverts : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public int ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string DocumentID { get; set; } // DocumentID
		/// <summary></summary>	
		[Description("")]
        public string FileID { get; set; } // FileID
		/// <summary></summary>	
		[Description("")]
        public string FileName { get; set; } // FileName
		/// <summary></summary>	
		[Description("")]
        public string FileFullPath { get; set; } // FileFullPath
		/// <summary></summary>	
		[Description("")]
        public string DBSID { get; set; } // DBSID
		/// <summary></summary>	
		[Description("")]
        public string Suffix { get; set; } // Suffix
		/// <summary></summary>	
		[Description("")]
        public int? Size { get; set; } // Size
		/// <summary></summary>	
		[Description("")]
        public bool? State { get; set; } // State
		/// <summary></summary>	
		[Description("")]
        public string Path { get; set; } // Path
		/// <summary></summary>	
		[Description("")]
        public string FontPath { get; set; } // FontPath
		/// <summary></summary>	
		[Description("")]
        public string XrefPath { get; set; } // XrefPath
		/// <summary></summary>	
		[Description("")]
        public string HtmlPath { get; set; } // HtmlPath
		/// <summary></summary>	
		[Description("")]
        public string ImagePath { get; set; } // ImagePath
		/// <summary></summary>	
		[Description("")]
        public int? ImageZoomLevel { get; set; } // ImageZoomLevel
		/// <summary></summary>	
		[Description("")]
        public string ErrorMessage { get; set; } // ErrorMessage
		/// <summary></summary>	
		[Description("")]
        public bool? IsSysn { get; set; } // IsSysn
		/// <summary></summary>	
		[Description("")]
        public DateTime? SysnTime { get; set; } // SysnTime
		/// <summary></summary>	
		[Description("")]
        public int? ConvertCount { get; set; } // ConvertCount
		/// <summary></summary>	
		[Description("")]
        public string UserID { get; set; } // UserID
		/// <summary></summary>	
		[Description("")]
        public string PrjID { get; set; } // PrjID
		/// <summary></summary>	
		[Description("")]
        public string VersionID { get; set; } // VersionID
		/// <summary></summary>	
		[Description("")]
        public string VersionName { get; set; } // VersionName
		/// <summary></summary>	
		[Description("")]
        public bool? IsUpFile { get; set; } // IsUpFile
    }

	/// <summary></summary>	
	[Description("")]
    public partial class S_I_ProjectGroup : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary>名称</summary>	
		[Description("名称")]
        public string Name { get; set; } // Name
		/// <summary>编号</summary>	
		[Description("编号")]
        public string Code { get; set; } // Code
		/// <summary>关联ID</summary>	
		[Description("关联ID")]
        public string RelateID { get; set; } // RelateID
		/// <summary></summary>	
		[Description("")]
        public string Type { get; set; } // Type
		/// <summary>父节点ID</summary>	
		[Description("父节点ID")]
        public string ParentID { get; set; } // ParentID
		/// <summary>全路径ID</summary>	
		[Description("全路径ID")]
        public string FullID { get; set; } // FullID
		/// <summary></summary>	
		[Description("")]
        public string RootID { get; set; } // RootID
		/// <summary>状态</summary>	
		[Description("状态")]
        public string State { get; set; } // State
		/// <summary></summary>	
		[Description("")]
        public string DefineID { get; set; } // DefineID
		/// <summary></summary>	
		[Description("")]
        public string EngineeringSpaceCode { get; set; } // EngineeringSpaceCode
		/// <summary></summary>	
		[Description("")]
        public string ChargeUser { get; set; } // ChargeUser
		/// <summary></summary>	
		[Description("")]
        public string ChargeUserName { get; set; } // ChargeUserName
		/// <summary></summary>	
		[Description("")]
        public string DeptID { get; set; } // DeptID
		/// <summary></summary>	
		[Description("")]
        public string DeptName { get; set; } // DeptName
		/// <summary></summary>	
		[Description("")]
        public string Country { get; set; } // Country
		/// <summary></summary>	
		[Description("")]
        public string Province { get; set; } // Province
		/// <summary></summary>	
		[Description("")]
        public string City { get; set; } // City
		/// <summary>性质</summary>	
		[Description("性质")]
        public string ProjectClass { get; set; } // ProjectClass
		/// <summary></summary>	
		[Description("")]
        public string Proportion { get; set; } // Proportion
		/// <summary></summary>	
		[Description("")]
        public string PhaseValue { get; set; } // PhaseValue
		/// <summary></summary>	
		[Description("")]
        public string PhaseContent { get; set; } // PhaseContent
		/// <summary></summary>	
		[Description("")]
        public decimal? Investment { get; set; } // Investment
		/// <summary></summary>	
		[Description("")]
        public string Address { get; set; } // Address
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public string Area { get; set; } // Area
    }

	/// <summary>项目信息表</summary>	
	[Description("项目信息表")]
    public partial class S_I_ProjectInfo : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary>管理模式编码</summary>	
		[Description("管理模式编码")]
        public string ModeCode { get; set; } // ModeCode
		/// <summary>项目名称</summary>	
		[Description("项目名称")]
        public string Name { get; set; } // Name
		/// <summary>项目编号</summary>	
		[Description("项目编号")]
        public string Code { get; set; } // Code
		/// <summary>项目状态</summary>	
		[Description("项目状态")]
        public string State { get; set; } // State
		/// <summary></summary>	
		[Description("")]
        public string Status { get; set; } // Status
		/// <summary>阶段值</summary>	
		[Description("阶段值")]
        public string PhaseValue { get; set; } // PhaseValue
		/// <summary>阶段</summary>	
		[Description("阶段")]
        public string PhaseName { get; set; } // PhaseName
		/// <summary></summary>	
		[Description("")]
        public string WorkContent { get; set; } // WorkContent
		/// <summary></summary>	
		[Description("")]
        public string Major { get; set; } // Major
		/// <summary></summary>	
		[Description("")]
        public string CustomerID { get; set; } // CustomerID
		/// <summary></summary>	
		[Description("")]
        public string CustomerName { get; set; } // CustomerName
		/// <summary></summary>	
		[Description("")]
        public string CustomerSub { get; set; } // CustomerSub
		/// <summary></summary>	
		[Description("")]
        public string CustomerSubName { get; set; } // CustomerSubName
		/// <summary>负责人ID</summary>	
		[Description("负责人ID")]
        public string ChargeUserID { get; set; } // ChargeUserID
		/// <summary>负责人</summary>	
		[Description("负责人")]
        public string ChargeUserName { get; set; } // ChargeUserName
		/// <summary>责任部门ID</summary>	
		[Description("责任部门ID")]
        public string ChargeDeptID { get; set; } // ChargeDeptID
		/// <summary>责任部门</summary>	
		[Description("责任部门")]
        public string ChargeDeptName { get; set; } // ChargeDeptName
		/// <summary>其他协作部门ID</summary>	
		[Description("其他协作部门ID")]
        public string OtherDeptID { get; set; } // OtherDeptID
		/// <summary>其他协作部门</summary>	
		[Description("其他协作部门")]
        public string OtherDeptName { get; set; } // OtherDeptName
		/// <summary></summary>	
		[Description("")]
        public string GroupRootID { get; set; } // GroupRootID
		/// <summary></summary>	
		[Description("")]
        public string GroupID { get; set; } // GroupID
		/// <summary></summary>	
		[Description("")]
        public string GroupFullID { get; set; } // GroupFullID
		/// <summary>项目特性</summary>	
		[Description("项目特性")]
        public string ProjectSpecialty { get; set; } // ProjectSpecialty
		/// <summary>业务类型</summary>	
		[Description("业务类型")]
        public string ProjectClass { get; set; } // ProjectClass
		/// <summary>项目等级</summary>	
		[Description("项目等级")]
        public int? ProjectLevel { get; set; } // ProjectLevel
		/// <summary></summary>	
		[Description("")]
        public string Country { get; set; } // Country
		/// <summary></summary>	
		[Description("")]
        public string Province { get; set; } // Province
		/// <summary></summary>	
		[Description("")]
        public string City { get; set; } // City
		/// <summary></summary>	
		[Description("")]
        public decimal? Proportion { get; set; } // Proportion
		/// <summary>计划开始日期</summary>	
		[Description("计划开始日期")]
        public DateTime? PlanStartDate { get; set; } // PlanStartDate
		/// <summary>计划完工日期</summary>	
		[Description("计划完工日期")]
        public DateTime? PlanFinishDate { get; set; } // PlanFinishDate
		/// <summary>实际开始日期</summary>	
		[Description("实际开始日期")]
        public DateTime? FactStartDate { get; set; } // FactStartDate
		/// <summary>实际完工日期</summary>	
		[Description("实际完工日期")]
        public DateTime? FactFinishDate { get; set; } // FactFinishDate
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get; set; } // CreateUserID
		/// <summary></summary>	
		[Description("")]
        public string CreateUser { get; set; } // CreateUser
		/// <summary></summary>	
		[Description("")]
        public DateTime CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public string ModifyUserID { get; set; } // ModifyUserID
		/// <summary></summary>	
		[Description("")]
        public string ModifyUser { get; set; } // ModifyUser
		/// <summary></summary>	
		[Description("")]
        public DateTime? ModifyDate { get; set; } // ModifyDate
		/// <summary></summary>	
		[Description("")]
        public string MarketProjectInfoID { get; set; } // MarketProjectInfoID
		/// <summary></summary>	
		[Description("")]
        public string EngineeringInfoID { get; set; } // EngineeringInfoID
		/// <summary></summary>	
		[Description("")]
        public string CustomerRequireInfoID { get; set; } // CustomerRequireInfoID
		/// <summary>完成百分比，从里程碑自动获得。</summary>	
		[Description("完成百分比，从里程碑自动获得。")]
        public decimal? CompletePercent { get; set; } // CompletePercent
		/// <summary></summary>	
		[Description("")]
        public int? IsSync { get; set; } // IsSync
		/// <summary></summary>	
		[Description("")]
        public DateTime? SyscTime { get; set; } // SyscTime
		/// <summary></summary>	
		[Description("")]
        public string CoopUnitID { get; set; } // CoopUnitID
		/// <summary></summary>	
		[Description("")]
        public string CoopUnitIDName { get; set; } // CoopUnitIDName
		/// <summary></summary>	
		[Description("")]
        public string Extention1 { get; set; } // Extention1
		/// <summary></summary>	
		[Description("")]
        public string Extention2 { get; set; } // Extention2
		/// <summary></summary>	
		[Description("")]
        public string Extention3 { get; set; } // Extention3
		/// <summary></summary>	
		[Description("")]
        public string Extention4 { get; set; } // Extention4
		/// <summary></summary>	
		[Description("")]
        public string Extention5 { get; set; } // Extention5
		/// <summary></summary>	
		[Description("")]
        public string Extention6 { get; set; } // Extention6
		/// <summary></summary>	
		[Description("")]
        public string Extention7 { get; set; } // Extention7
		/// <summary></summary>	
		[Description("")]
        public string Extention8 { get; set; } // Extention8
		/// <summary></summary>	
		[Description("")]
        public string Extention9 { get; set; } // Extention9
		/// <summary></summary>	
		[Description("")]
        public string Extention10 { get; set; } // Extention10
		/// <summary></summary>	
		[Description("")]
        public double? Long { get; set; } // Long
		/// <summary></summary>	
		[Description("")]
        public double? Lat { get; set; } // Lat
		/// <summary></summary>	
		[Description("")]
        public string Address { get; set; } // Address
		/// <summary></summary>	
		[Description("")]
        public string District { get; set; } // District
		/// <summary></summary>	
		[Description("")]
        public string Area { get; set; } // Area

        // Reverse navigation
		[JsonIgnore]
        public virtual ICollection<S_C_CBS> S_C_CBS { get { onS_C_CBSGetting(); return _S_C_CBS;} }
		private ICollection<S_C_CBS> _S_C_CBS;
		partial void onS_C_CBSGetting();

		[JsonIgnore]
        public virtual ICollection<S_D_DBS> S_D_DBS { get { onS_D_DBSGetting(); return _S_D_DBS;} }
		private ICollection<S_D_DBS> _S_D_DBS;
		partial void onS_D_DBSGetting();

		[JsonIgnore]
        public virtual ICollection<S_D_Input> S_D_Input { get { onS_D_InputGetting(); return _S_D_Input;} }
		private ICollection<S_D_Input> _S_D_Input;
		partial void onS_D_InputGetting();

		[JsonIgnore]
        public virtual ICollection<S_D_ShareInfo> S_D_ShareInfo { get { onS_D_ShareInfoGetting(); return _S_D_ShareInfo;} }
		private ICollection<S_D_ShareInfo> _S_D_ShareInfo;
		partial void onS_D_ShareInfoGetting();

		[JsonIgnore]
        public virtual ICollection<S_E_PublishInfo> S_E_PublishInfo { get { onS_E_PublishInfoGetting(); return _S_E_PublishInfo;} }
		private ICollection<S_E_PublishInfo> _S_E_PublishInfo;
		partial void onS_E_PublishInfoGetting();

		[JsonIgnore]
        public virtual ICollection<S_P_CooperationPlan> S_P_CooperationPlan { get { onS_P_CooperationPlanGetting(); return _S_P_CooperationPlan;} }
		private ICollection<S_P_CooperationPlan> _S_P_CooperationPlan;
		partial void onS_P_CooperationPlanGetting();

		[JsonIgnore]
        public virtual ICollection<S_P_MileStone> S_P_MileStone { get { onS_P_MileStoneGetting(); return _S_P_MileStone;} }
		private ICollection<S_P_MileStone> _S_P_MileStone;
		partial void onS_P_MileStoneGetting();

		[JsonIgnore]
        public virtual ICollection<S_Q_QBS> S_Q_QBS { get { onS_Q_QBSGetting(); return _S_Q_QBS;} }
		private ICollection<S_Q_QBS> _S_Q_QBS;
		partial void onS_Q_QBSGetting();

		[JsonIgnore]
        public virtual ICollection<S_R_Risk> S_R_Risk { get { onS_R_RiskGetting(); return _S_R_Risk;} }
		private ICollection<S_R_Risk> _S_R_Risk;
		partial void onS_R_RiskGetting();

		[JsonIgnore]
        public virtual ICollection<S_W_OBSUser> S_W_OBSUser { get { onS_W_OBSUserGetting(); return _S_W_OBSUser;} }
		private ICollection<S_W_OBSUser> _S_W_OBSUser;
		partial void onS_W_OBSUserGetting();

		[JsonIgnore]
        public virtual ICollection<S_W_WBS> S_W_WBS { get { onS_W_WBSGetting(); return _S_W_WBS;} }
		private ICollection<S_W_WBS> _S_W_WBS;
		partial void onS_W_WBSGetting();

		[JsonIgnore]
        public virtual ICollection<S_W_WBS_Version> S_W_WBS_Version { get { onS_W_WBS_VersionGetting(); return _S_W_WBS_Version;} }
		private ICollection<S_W_WBS_Version> _S_W_WBS_Version;
		partial void onS_W_WBS_VersionGetting();


        public S_I_ProjectInfo()
        {
            _S_C_CBS = new List<S_C_CBS>();
            _S_D_DBS = new List<S_D_DBS>();
            _S_D_Input = new List<S_D_Input>();
            _S_D_ShareInfo = new List<S_D_ShareInfo>();
            _S_E_PublishInfo = new List<S_E_PublishInfo>();
            _S_P_CooperationPlan = new List<S_P_CooperationPlan>();
            _S_P_MileStone = new List<S_P_MileStone>();
            _S_Q_QBS = new List<S_Q_QBS>();
            _S_R_Risk = new List<S_R_Risk>();
            _S_W_OBSUser = new List<S_W_OBSUser>();
            _S_W_WBS = new List<S_W_WBS>();
            _S_W_WBS_Version = new List<S_W_WBS_Version>();
        }
    }

	/// <summary></summary>	
	[Description("")]
    public partial class S_I_ProjectRelation : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string ProjectInfoID { get; set; } // ProjectInfoID
		/// <summary></summary>	
		[Description("")]
        public string CoopUnitID { get; set; } // CoopUnitID
		/// <summary></summary>	
		[Description("")]
        public int? IsSync { get; set; } // IsSync
		/// <summary></summary>	
		[Description("")]
        public DateTime? SyscTime { get; set; } // SyscTime
    }

	/// <summary></summary>	
	[Description("")]
    public partial class S_I_UserDefaultProjectInfo : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string UserID { get; set; } // UserID
		/// <summary></summary>	
		[Description("")]
        public string ProjectInfoID { get; set; } // ProjectInfoID
		/// <summary></summary>	
		[Description("")]
        public string EngineeringInfoID { get; set; } // EngineeringInfoID
    }

	/// <summary></summary>	
	[Description("")]
    public partial class S_I_UserFocusProjectInfo : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string UserID { get; set; } // UserID
		/// <summary></summary>	
		[Description("")]
        public string ProjectInfoID { get; set; } // ProjectInfoID
    }

	/// <summary></summary>	
	[Description("")]
    public partial class S_N_Notice : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string GroupInfoID { get; set; } // GroupInfoID
		/// <summary></summary>	
		[Description("")]
        public string Title { get; set; } // Title
		/// <summary></summary>	
		[Description("")]
        public string Content { get; set; } // Content
		/// <summary></summary>	
		[Description("")]
        public string Attachments { get; set; } // Attachments
		/// <summary></summary>	
		[Description("")]
        public string IsFromSys { get; set; } // IsFromSys
		/// <summary></summary>	
		[Description("")]
        public DateTime? ExpiresTime { get; set; } // ExpiresTime
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get; set; } // CreateUserID
		/// <summary></summary>	
		[Description("")]
        public string CreateUserName { get; set; } // CreateUserName
		/// <summary></summary>	
		[Description("")]
        public string LinkUrl { get; set; } // LinkUrl
		/// <summary></summary>	
		[Description("")]
        public string RelateID { get; set; } // RelateID
		/// <summary></summary>	
		[Description("")]
        public string RelateType { get; set; } // RelateType
		/// <summary></summary>	
		[Description("")]
        public string ProjectInfoID { get; set; } // ProjectInfoID
		/// <summary></summary>	
		[Description("")]
        public string WBSID { get; set; } // WBSID
		/// <summary></summary>	
		[Description("")]
        public string RoleCode { get; set; } // RoleCode
		/// <summary></summary>	
		[Description("")]
        public string MajorValue { get; set; } // MajorValue
		/// <summary></summary>	
		[Description("")]
        public string ReceiverIDs { get; set; } // ReceiverIDs
		/// <summary></summary>	
		[Description("")]
        public string ReceiverNames { get; set; } // ReceiverNames
		/// <summary></summary>	
		[Description("")]
        public string NoticeType { get; set; } // NoticeType

        // Reverse navigation
		[JsonIgnore]
        public virtual ICollection<S_N_Notice_ViewDetail> S_N_Notice_ViewDetail { get { onS_N_Notice_ViewDetailGetting(); return _S_N_Notice_ViewDetail;} }
		private ICollection<S_N_Notice_ViewDetail> _S_N_Notice_ViewDetail;
		partial void onS_N_Notice_ViewDetailGetting();


        public S_N_Notice()
        {
            _S_N_Notice_ViewDetail = new List<S_N_Notice_ViewDetail>();
        }
    }

	/// <summary></summary>	
	[Description("")]
    public partial class S_N_Notice_ViewDetail : Formula.BaseModel
    {
		/// <summary>ID</summary>	
		[Description("ID")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary>消息体ID</summary>	
		[Description("消息体ID")]
        public string S_N_NoticeID { get; set; } // S_N_NoticeID
		/// <summary>接收人ID</summary>	
		[Description("接收人ID")]
        public string UserID { get; set; } // UserID
		/// <summary>接收人姓名</summary>	
		[Description("接收人姓名")]
        public string UserName { get; set; } // UserName
		/// <summary>首次查看时间</summary>	
		[Description("首次查看时间")]
        public DateTime? FirstViewTime { get; set; } // FirstViewTime

        // Foreign keys
		[JsonIgnore]
        public virtual S_N_Notice S_N_Notice { get; set; } //  S_N_NoticeID - FK_S_N_Notice_ViewDetail_S_N_Notice
    }

	/// <summary>提资内容计划结构表</summary>	
	[Description("提资内容计划结构表")]
    public partial class S_P_CooperationPlan : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary>项目信息ID（必填）</summary>	
		[Description("项目信息ID（必填）")]
        public string ProjectInfoID { get; set; } // ProjectInfoID
		/// <summary>WBS信息ID（必须）</summary>	
		[Description("WBS信息ID（必须）")]
        public string WBSID { get; set; } // WBSID
		/// <summary></summary>	
		[Description("")]
        public string WBSFullID { get; set; } // WBSFullID
		/// <summary>里程碑ID（必须）</summary>	
		[Description("里程碑ID（必须）")]
        public string MileStoneID { get; set; } // MileStoneID
		/// <summary>策划时wbs节点</summary>	
		[Description("策划时wbs节点")]
        public string SchemeWBSID { get; set; } // SchemeWBSID
		/// <summary>策划时wbs节点名称</summary>	
		[Description("策划时wbs节点名称")]
        public string SchemeWBSName { get; set; } // SchemeWBSName
		/// <summary>提资内容</summary>	
		[Description("提资内容")]
        public string CooperationContent { get; set; } // CooperationContent
		/// <summary></summary>	
		[Description("")]
        public string CooperationValue { get; set; } // CooperationValue
		/// <summary>提资专业</summary>	
		[Description("提资专业")]
        public string InMajorValue { get; set; } // InMajorValue
		/// <summary>接受专业</summary>	
		[Description("接受专业")]
        public string OutMajorValue { get; set; } // OutMajorValue
		/// <summary>计划开始</summary>	
		[Description("计划开始")]
        public DateTime? PlanStartDate { get; set; } // PlanStartDate
		/// <summary>计划完成</summary>	
		[Description("计划完成")]
        public DateTime? PlanFinishDate { get; set; } // PlanFinishDate
		/// <summary>原计划开始</summary>	
		[Description("原计划开始")]
        public DateTime? OrPlanStartDate { get; set; } // OrPlanStartDate
		/// <summary>原计划完成日期</summary>	
		[Description("原计划完成日期")]
        public DateTime? OrPlanFinishDate { get; set; } // OrPlanFinishDate
		/// <summary>实际开始日期</summary>	
		[Description("实际开始日期")]
        public DateTime? FactStartDate { get; set; } // FactStartDate
		/// <summary>实际完成日期</summary>	
		[Description("实际完成日期")]
        public DateTime? FactFinishDate { get; set; } // FactFinishDate
		/// <summary>提资人</summary>	
		[Description("提资人")]
        public string FinishUser { get; set; } // FinishUser
		/// <summary>提资人ID</summary>	
		[Description("提资人ID")]
        public string FinishUserID { get; set; } // FinishUserID
		/// <summary></summary>	
		[Description("")]
        public string State { get; set; } // State
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get; set; } // CreateUserID
		/// <summary></summary>	
		[Description("")]
        public string CreateUser { get; set; } // CreateUser
		/// <summary></summary>	
		[Description("")]
        public DateTime CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public string ModifyUserID { get; set; } // ModifyUserID
		/// <summary></summary>	
		[Description("")]
        public string ModifyUser { get; set; } // ModifyUser
		/// <summary></summary>	
		[Description("")]
        public DateTime ModifyDate { get; set; } // ModifyDate

        // Foreign keys
		[JsonIgnore]
        public virtual S_I_ProjectInfo S_I_ProjectInfo { get; set; } //  ProjectInfoID - FK_S_P_CooperationPlan_S_I_ProjectInfo
    }

	/// <summary>项目里程碑</summary>	
	[Description("项目里程碑")]
    public partial class S_P_MileStone : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary>项目信息ID</summary>	
		[Description("项目信息ID")]
        public string ProjectInfoID { get; set; } // ProjectInfoID
		/// <summary>WBS信息ID</summary>	
		[Description("WBS信息ID")]
        public string WBSID { get; set; } // WBSID
		/// <summary></summary>	
		[Description("")]
        public string TemplateID { get; set; } // TemplateID
		/// <summary>里程碑名称</summary>	
		[Description("里程碑名称")]
        public string Name { get; set; } // Name
		/// <summary>里程碑编号</summary>	
		[Description("里程碑编号")]
        public string Code { get; set; } // Code
		/// <summary></summary>	
		[Description("")]
        public int? Year { get; set; } // Year
		/// <summary></summary>	
		[Description("")]
        public int? Month { get; set; } // Month
		/// <summary></summary>	
		[Description("")]
        public int? Season { get; set; } // Season
		/// <summary></summary>	
		[Description("")]
        public string DeptID { get; set; } // DeptID
		/// <summary></summary>	
		[Description("")]
        public string DeptName { get; set; } // DeptName
		/// <summary></summary>	
		[Description("")]
        public string MileStoneValue { get; set; } // MileStoneValue
		/// <summary></summary>	
		[Description("")]
        public string MileStoneType { get; set; } // MileStoneType
		/// <summary>所属阶段</summary>	
		[Description("所属阶段")]
        public string PhaseValue { get; set; } // PhaseValue
		/// <summary></summary>	
		[Description("")]
        public string MajorValue { get; set; } // MajorValue
		/// <summary></summary>	
		[Description("")]
        public string OutMajorValue { get; set; } // OutMajorValue
		/// <summary></summary>	
		[Description("")]
        public string Necessity { get; set; } // Necessity
		/// <summary></summary>	
		[Description("")]
        public string BindReceiptObjID { get; set; } // BindReceiptObjID
		/// <summary></summary>	
		[Description("")]
        public string BindReceiptObjName { get; set; } // BindReceiptObjName
		/// <summary>计划开始日期</summary>	
		[Description("计划开始日期")]
        public DateTime? PlanStartDate { get; set; } // PlanStartDate
		/// <summary>计划结束日期</summary>	
		[Description("计划结束日期")]
        public DateTime? PlanFinishDate { get; set; } // PlanFinishDate
		/// <summary>原计划开始日期</summary>	
		[Description("原计划开始日期")]
        public DateTime? OrlPlanStartDate { get; set; } // OrlPlanStartDate
		/// <summary>原计划结束日期</summary>	
		[Description("原计划结束日期")]
        public DateTime? OrlPlanFinishDate { get; set; } // OrlPlanFinishDate
		/// <summary>实际开始日期</summary>	
		[Description("实际开始日期")]
        public DateTime? FactStartDate { get; set; } // FactStartDate
		/// <summary>实际完成日期</summary>	
		[Description("实际完成日期")]
        public DateTime? FactFinishDate { get; set; } // FactFinishDate
		/// <summary>比重</summary>	
		[Description("比重")]
        public decimal? Weight { get; set; } // Weight
		/// <summary>状态</summary>	
		[Description("状态")]
        public string State { get; set; } // State
		/// <summary></summary>	
		[Description("")]
        public int? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string Description { get; set; } // Description
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get; set; } // CreateUserID
		/// <summary></summary>	
		[Description("")]
        public string CreateUser { get; set; } // CreateUser
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public string ModifyUserID { get; set; } // ModifyUserID
		/// <summary></summary>	
		[Description("")]
        public string ModifyUser { get; set; } // ModifyUser
		/// <summary></summary>	
		[Description("")]
        public DateTime? ModifyDate { get; set; } // ModifyDate
		/// <summary>计划收款项ID</summary>	
		[Description("计划收款项ID")]
        public string IncomingID { get; set; } // IncomingID
		/// <summary></summary>	
		[Description("")]
        public string ApplyUserID { get; set; } // ApplyUserID
		/// <summary></summary>	
		[Description("")]
        public string ApplyUserName { get; set; } // ApplyUserName
		/// <summary></summary>	
		[Description("")]
        public string ApplyState { get; set; } // ApplyState
		/// <summary></summary>	
		[Description("")]
        public string ApplyFiles { get; set; } // ApplyFiles
		/// <summary></summary>	
		[Description("")]
        public string ApplyRemark { get; set; } // ApplyRemark
		/// <summary></summary>	
		[Description("")]
        public DateTime? ApplyDate { get; set; } // ApplyDate
		/// <summary></summary>	
		[Description("")]
        public string ApplyApprovalUserID { get; set; } // ApplyApprovalUserID
		/// <summary></summary>	
		[Description("")]
        public string ApplyApprovalUserName { get; set; } // ApplyApprovalUserName
		/// <summary></summary>	
		[Description("")]
        public DateTime? ApplyApprovalDate { get; set; } // ApplyApprovalDate
		/// <summary></summary>	
		[Description("")]
        public string IsChecked { get; set; } // IsChecked

        // Reverse navigation
		[JsonIgnore]
        public virtual ICollection<S_P_MileStone_FormDetail> S_P_MileStone_FormDetail { get { onS_P_MileStone_FormDetailGetting(); return _S_P_MileStone_FormDetail;} }
		private ICollection<S_P_MileStone_FormDetail> _S_P_MileStone_FormDetail;
		partial void onS_P_MileStone_FormDetailGetting();

		[JsonIgnore]
        public virtual ICollection<S_P_MileStone_ProductDetail> S_P_MileStone_ProductDetail { get { onS_P_MileStone_ProductDetailGetting(); return _S_P_MileStone_ProductDetail;} }
		private ICollection<S_P_MileStone_ProductDetail> _S_P_MileStone_ProductDetail;
		partial void onS_P_MileStone_ProductDetailGetting();

		[JsonIgnore]
        public virtual ICollection<S_P_MileStonePlan> S_P_MileStonePlan { get { onS_P_MileStonePlanGetting(); return _S_P_MileStonePlan;} }
		private ICollection<S_P_MileStonePlan> _S_P_MileStonePlan;
		partial void onS_P_MileStonePlanGetting();


        // Foreign keys
		[JsonIgnore]
        public virtual S_I_ProjectInfo S_I_ProjectInfo { get; set; } //  ProjectInfoID - FK_S_P_MileStone_S_I_ProjectInfo

        public S_P_MileStone()
        {
			ApplyState = "未申请";
            _S_P_MileStone_FormDetail = new List<S_P_MileStone_FormDetail>();
            _S_P_MileStone_ProductDetail = new List<S_P_MileStone_ProductDetail>();
            _S_P_MileStonePlan = new List<S_P_MileStonePlan>();
        }
    }

	/// <summary></summary>	
	[Description("")]
    public partial class S_P_MileStone_FormDetail : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string S_P_MileStoneID { get; set; } // S_P_MileStoneID
		/// <summary></summary>	
		[Description("")]
        public string FormID { get; set; } // FormID
		/// <summary></summary>	
		[Description("")]
        public DateTime? BindDate { get; set; } // BindDate
		/// <summary></summary>	
		[Description("")]
        public string BindPeople { get; set; } // BindPeople

        // Foreign keys
		[JsonIgnore]
        public virtual S_P_MileStone S_P_MileStone { get; set; } //  S_P_MileStoneID - FK_S_P_MileStone_FormDetail_S_P_MileStone_FormDetail
    }

	/// <summary>测试导出</summary>	
	[Description("测试导出")]
    public partial class S_P_MileStone_ProductDetail : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string S_P_MileStoneID { get; set; } // S_P_MileStoneID
		/// <summary></summary>	
		[Description("")]
        public string ProjectInfoID { get; set; } // ProjectInfoID
		/// <summary></summary>	
		[Description("")]
        public string Name { get; set; } // Name
		/// <summary></summary>	
		[Description("")]
        public string Code { get; set; } // Code
		/// <summary></summary>	
		[Description("")]
        public string CreateUser { get; set; } // CreateUser
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get; set; } // CreateUserID
		/// <summary></summary>	
		[Description("")]
        public string ModifyUser { get; set; } // ModifyUser
		/// <summary></summary>	
		[Description("")]
        public string ModifyUserID { get; set; } // ModifyUserID
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? ModifyDate { get; set; } // ModifyDate
		/// <summary></summary>	
		[Description("")]
        public string FileID { get; set; } // FileID
		/// <summary></summary>	
		[Description("")]
        public string MD5Code { get; set; } // MD5Code
		/// <summary></summary>	
		[Description("")]
        public string ReceiveInfo { get; set; } // ReceiveInfo
		/// <summary></summary>	
		[Description("")]
        public string State { get; set; } // State

        // Foreign keys
		[JsonIgnore]
        public virtual S_P_MileStone S_P_MileStone { get; set; } //  S_P_MileStoneID - FK__S_P_MileS__S_P_M__143CDA05
    }

	/// <summary></summary>	
	[Description("")]
    public partial class S_P_MileStoneHistory : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string MileStoneID { get; set; } // MileStoneID
		/// <summary></summary>	
		[Description("")]
        public string ProjectInfoID { get; set; } // ProjectInfoID
		/// <summary></summary>	
		[Description("")]
        public string WBSID { get; set; } // WBSID
		/// <summary></summary>	
		[Description("")]
        public string ChangeReason { get; set; } // ChangeReason
		/// <summary>里程碑名称</summary>	
		[Description("里程碑名称")]
        public string Name { get; set; } // Name
		/// <summary>里程碑编号</summary>	
		[Description("里程碑编号")]
        public string Code { get; set; } // Code
		/// <summary></summary>	
		[Description("")]
        public string MileStoneLevel { get; set; } // MileStoneLevel
		/// <summary>计划结束日期</summary>	
		[Description("计划结束日期")]
        public DateTime? PlanFinishDate { get; set; } // PlanFinishDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? PreviosPlanFinishDate { get; set; } // PreviosPlanFinishDate
		/// <summary>原计划结束日期</summary>	
		[Description("原计划结束日期")]
        public DateTime? OrlPlanFinishDate { get; set; } // OrlPlanFinishDate
		/// <summary></summary>	
		[Description("")]
        public string MileStoneType { get; set; } // MileStoneType
		/// <summary></summary>	
		[Description("")]
        public decimal? Weight { get; set; } // Weight
		/// <summary></summary>	
		[Description("")]
        public DateTime? ChangeTime { get; set; } // ChangeTime
    }

	/// <summary></summary>	
	[Description("")]
    public partial class S_P_MileStonePlan : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string ProjectInfoID { get; set; } // ProjectInfoID
		/// <summary></summary>	
		[Description("")]
        public string MileStoneID { get; set; } // MileStoneID
		/// <summary></summary>	
		[Description("")]
        public string WBSID { get; set; } // WBSID
		/// <summary></summary>	
		[Description("")]
        public string Name { get; set; } // Name
		/// <summary></summary>	
		[Description("")]
        public string Code { get; set; } // Code
		/// <summary></summary>	
		[Description("")]
        public DateTime? OrlPlanFinishDate { get; set; } // OrlPlanFinishDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? PlanFinishDate { get; set; } // PlanFinishDate
		/// <summary></summary>	
		[Description("")]
        public string DeptID { get; set; } // DeptID
		/// <summary></summary>	
		[Description("")]
        public string DeptName { get; set; } // DeptName
		/// <summary></summary>	
		[Description("")]
        public int? Year { get; set; } // Year
		/// <summary></summary>	
		[Description("")]
        public int? Month { get; set; } // Month
		/// <summary></summary>	
		[Description("")]
        public int? Season { get; set; } // Season
		/// <summary></summary>	
		[Description("")]
        public string MileStoneValue { get; set; } // MileStoneValue
		/// <summary></summary>	
		[Description("")]
        public string MileStoneType { get; set; } // MileStoneType
		/// <summary></summary>	
		[Description("")]
        public string MajorValue { get; set; } // MajorValue
		/// <summary></summary>	
		[Description("")]
        public string MileStoneLevel { get; set; } // MileStoneLevel
		/// <summary></summary>	
		[Description("")]
        public string State { get; set; } // State
		/// <summary></summary>	
		[Description("")]
        public string Stauts { get; set; } // Stauts
		/// <summary></summary>	
		[Description("")]
        public int? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string Description { get; set; } // Description

        // Foreign keys
		[JsonIgnore]
        public virtual S_P_MileStone S_P_MileStone { get; set; } //  MileStoneID - FK_S_P_MileStonePlan_S_P_MileStone
    }

	/// <summary></summary>	
	[Description("")]
    public partial class S_Q_QBS : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string ParentID { get; set; } // ParentID
		/// <summary></summary>	
		[Description("")]
        public string FullID { get; set; } // FullID
		/// <summary></summary>	
		[Description("")]
        public string DefineID { get; set; } // DefineID
		/// <summary></summary>	
		[Description("")]
        public string ProjectInfoID { get; set; } // ProjectInfoID
		/// <summary></summary>	
		[Description("")]
        public string NodeType { get; set; } // NodeType
		/// <summary></summary>	
		[Description("")]
        public string QBSType { get; set; } // QBSType
		/// <summary></summary>	
		[Description("")]
        public string Name { get; set; } // Name
		/// <summary></summary>	
		[Description("")]
        public string Code { get; set; } // Code
		/// <summary></summary>	
		[Description("")]
        public string ConnName { get; set; } // ConnName
		/// <summary></summary>	
		[Description("")]
        public string SQL { get; set; } // SQL
		/// <summary></summary>	
		[Description("")]
        public int? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string RelateFormID { get; set; } // RelateFormID
		/// <summary></summary>	
		[Description("")]
        public string LinkUrl { get; set; } // LinkUrl
		/// <summary></summary>	
		[Description("")]
        public DateTime? FinishDate { get; set; } // FinishDate
		/// <summary></summary>	
		[Description("")]
        public string State { get; set; } // State

        // Foreign keys
		[JsonIgnore]
        public virtual S_I_ProjectInfo S_I_ProjectInfo { get; set; } //  ProjectInfoID - FK_S_Q_QBS_S_I_ProjectInfo
    }

	/// <summary>项目风险</summary>	
	[Description("项目风险")]
    public partial class S_R_Risk : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string ProjectInfoID { get; set; } // ProjectInfoID
		/// <summary></summary>	
		[Description("")]
        public string Name { get; set; } // Name
		/// <summary></summary>	
		[Description("")]
        public string Code { get; set; } // Code
		/// <summary></summary>	
		[Description("")]
        public string RiskLevel { get; set; } // RiskLevel
		/// <summary></summary>	
		[Description("")]
        public string RiskContent { get; set; } // RiskContent
		/// <summary></summary>	
		[Description("")]
        public string Measure { get; set; } // Measure
		/// <summary></summary>	
		[Description("")]
        public string Attachments { get; set; } // Attachments
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get; set; } // CreateUserID
		/// <summary></summary>	
		[Description("")]
        public string CreateUser { get; set; } // CreateUser
		/// <summary></summary>	
		[Description("")]
        public DateTime CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public string LastModifyUserID { get; set; } // LastModifyUserID
		/// <summary></summary>	
		[Description("")]
        public string LastModifyUser { get; set; } // LastModifyUser
		/// <summary></summary>	
		[Description("")]
        public DateTime LastModifyDate { get; set; } // LastModifyDate

        // Foreign keys
		[JsonIgnore]
        public virtual S_I_ProjectInfo S_I_ProjectInfo { get; set; } //  ProjectInfoID - FK_S_R_Risk_S_I_ProjectInfo
    }

	/// <summary></summary>	
	[Description("")]
    public partial class S_T_ToDoList : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string Name { get; set; } // Name
		/// <summary></summary>	
		[Description("")]
        public string ExecUserID { get; set; } // ExecUserID
		/// <summary></summary>	
		[Description("")]
        public string ExecUserName { get; set; } // ExecUserName
		/// <summary></summary>	
		[Description("")]
        public string State { get; set; } // State
		/// <summary></summary>	
		[Description("")]
        public DateTime? FinishTime { get; set; } // FinishTime
		/// <summary></summary>	
		[Description("")]
        public DateTime CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public string LinkUrl { get; set; } // LinkUrl
		/// <summary></summary>	
		[Description("")]
        public string OpenWidth { get; set; } // OpenWidth
		/// <summary></summary>	
		[Description("")]
        public string OpenHeight { get; set; } // OpenHeight
		/// <summary></summary>	
		[Description("")]
        public string DefineNodeID { get; set; } // DefineNodeID
		/// <summary></summary>	
		[Description("")]
        public string DefineNodeName { get; set; } // DefineNodeName
		/// <summary></summary>	
		[Description("")]
        public string DefineID { get; set; } // DefineID
		/// <summary></summary>	
		[Description("")]
        public string DefineName { get; set; } // DefineName
		/// <summary></summary>	
		[Description("")]
        public string ExecData { get; set; } // ExecData
		/// <summary></summary>	
		[Description("")]
        public string FinishData { get; set; } // FinishData
		/// <summary></summary>	
		[Description("")]
        public string EngineeringInfoID { get; set; } // EngineeringInfoID
		/// <summary></summary>	
		[Description("")]
        public string ProjectInfoID { get; set; } // ProjectInfoID
		/// <summary></summary>	
		[Description("")]
        public string ExeAction { get; set; } // ExeAction
    }

	/// <summary></summary>	
	[Description("")]
    public partial class S_W_Activity : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string DisplayName { get; set; } // DisplayName
		/// <summary></summary>	
		[Description("")]
        public string ActvityName { get; set; } // ActvityName
		/// <summary></summary>	
		[Description("")]
        public string ActivityKey { get; set; } // ActivityKey
		/// <summary></summary>	
		[Description("")]
        public string ProjectInfoID { get; set; } // ProjectInfoID
		/// <summary></summary>	
		[Description("")]
        public string BusniessID { get; set; } // BusniessID
		/// <summary></summary>	
		[Description("")]
        public string WBSID { get; set; } // WBSID
		/// <summary>校审单的ID</summary>	
		[Description("校审单的ID")]
        public string AuditPatchID { get; set; } // AuditPatchID
		/// <summary>分组标示</summary>	
		[Description("分组标示")]
        public string GroupID { get; set; } // GroupID
		/// <summary></summary>	
		[Description("")]
        public string OwnerUserID { get; set; } // OwnerUserID
		/// <summary></summary>	
		[Description("")]
        public string OwnerUserName { get; set; } // OwnerUserName
		/// <summary></summary>	
		[Description("")]
        public DateTime CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public string CreateUser { get; set; } // CreateUser
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get; set; } // CreateUserID
		/// <summary></summary>	
		[Description("")]
        public DateTime? FinishDate { get; set; } // FinishDate
		/// <summary></summary>	
		[Description("")]
        public string State { get; set; } // State
		/// <summary></summary>	
		[Description("")]
        public string LinkUrl { get; set; } // LinkUrl
		/// <summary></summary>	
		[Description("")]
        public string Params { get; set; } // Params
		/// <summary></summary>	
		[Description("")]
        public string DefSteps { get; set; } // DefSteps
		/// <summary></summary>	
		[Description("")]
        public string NextStep { get; set; } // NextStep
		/// <summary></summary>	
		[Description("")]
        public string ExeucteOption { get; set; } // ExeucteOption
		/// <summary>执行操作名称</summary>	
		[Description("执行操作名称")]
        public string ExecRoutingName { get; set; } // ExecRoutingName
    }

	/// <summary></summary>	
	[Description("")]
    public partial class S_W_CooperationExe : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string Name { get; set; } // Name
		/// <summary></summary>	
		[Description("")]
        public string ProjectInfoID { get; set; } // ProjectInfoID
		/// <summary></summary>	
		[Description("")]
        public string RelateMileStoneID { get; set; } // RelateMileStoneID
		/// <summary></summary>	
		[Description("")]
        public string BatchID { get; set; } // BatchID
		/// <summary></summary>	
		[Description("")]
        public string RelatePlanID { get; set; } // RelatePlanID
		/// <summary></summary>	
		[Description("")]
        public string RelateWBSID { get; set; } // RelateWBSID
		/// <summary></summary>	
		[Description("")]
        public string OutMajor { get; set; } // OutMajor
		/// <summary></summary>	
		[Description("")]
        public string InMajor { get; set; } // InMajor
		/// <summary></summary>	
		[Description("")]
        public string Files { get; set; } // Files
		/// <summary></summary>	
		[Description("")]
        public string PicNo { get; set; } // PicNo
		/// <summary></summary>	
		[Description("")]
        public string DrawingNo { get; set; } // DrawingNo
		/// <summary></summary>	
		[Description("")]
        public DateTime? FetchOutDate { get; set; } // FetchOutDate
		/// <summary></summary>	
		[Description("")]
        public string FetchOutUser { get; set; } // FetchOutUser
		/// <summary></summary>	
		[Description("")]
        public string FetchOutUserID { get; set; } // FetchOutUserID
		/// <summary></summary>	
		[Description("")]
        public DateTime? ReceiveDate { get; set; } // ReceiveDate
		/// <summary></summary>	
		[Description("")]
        public string ReceiveState { get; set; } // ReceiveState
		/// <summary></summary>	
		[Description("")]
        public string ReceiveUser { get; set; } // ReceiveUser
		/// <summary></summary>	
		[Description("")]
        public string ReceiveUserID { get; set; } // ReceiveUserID
    }

	/// <summary></summary>	
	[Description("")]
    public partial class S_W_Monomer : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string Name { get; set; } // Name
		/// <summary></summary>	
		[Description("")]
        public string Code { get; set; } // Code
		/// <summary></summary>	
		[Description("")]
        public string ProjectInfoID { get; set; } // ProjectInfoID
		/// <summary></summary>	
		[Description("")]
        public string WBSID { get; set; } // WBSID
		/// <summary></summary>	
		[Description("")]
        public string SchemeFormSubID { get; set; } // SchemeFormSubID
		/// <summary></summary>	
		[Description("")]
        public DateTime CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public string CreateUser { get; set; } // CreateUser
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get; set; } // CreateUserID
		/// <summary></summary>	
		[Description("")]
        public double? Area { get; set; } // Area

        // Foreign keys
		[JsonIgnore]
        public virtual S_W_WBS S_W_WBS { get; set; } //  WBSID - FK_S_W_SubProject_S_W_WBS
    }

	/// <summary></summary>	
	[Description("")]
    public partial class S_W_OBSUser : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string ProjectInfoID { get; set; } // ProjectInfoID
		/// <summary></summary>	
		[Description("")]
        public string MajorValue { get; set; } // MajorValue
		/// <summary></summary>	
		[Description("")]
        public string MajorName { get; set; } // MajorName
		/// <summary>岗位角色编码</summary>	
		[Description("岗位角色编码")]
        public string RoleCode { get; set; } // RoleCode
		/// <summary>岗位角色名称</summary>	
		[Description("岗位角色名称")]
        public string RoleName { get; set; } // RoleName
		/// <summary>人员ID</summary>	
		[Description("人员ID")]
        public string UserID { get; set; } // UserID
		/// <summary>人员姓名</summary>	
		[Description("人员姓名")]
        public string UserName { get; set; } // UserName
		/// <summary></summary>	
		[Description("")]
        public string DeptID { get; set; } // DeptID
		/// <summary></summary>	
		[Description("")]
        public string DeptName { get; set; } // DeptName
		/// <summary></summary>	
		[Description("")]
        public string OBSID { get; set; } // OBSID
		/// <summary></summary>	
		[Description("")]
        public string OBSFULLID { get; set; } // OBSFULLID
		/// <summary></summary>	
		[Description("")]
        public string IsCloud { get; set; } // IsCloud

        // Foreign keys
		[JsonIgnore]
        public virtual S_I_ProjectInfo S_I_ProjectInfo { get; set; } //  ProjectInfoID - FK_S_W_OBSUser_S_I_ProjectInfo
    }

	/// <summary>项目资源</summary>	
	[Description("项目资源")]
    public partial class S_W_RBS : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public int ID { get; set; } // ID (Primary key)
		/// <summary>WBS信息ID（必须）</summary>	
		[Description("WBS信息ID（必须）")]
        public string WBSID { get; set; } // WBSID
		/// <summary></summary>	
		[Description("")]
        public string WBSCode { get; set; } // WBSCode
		/// <summary></summary>	
		[Description("")]
        public string WBSType { get; set; } // WBSType
		/// <summary>项目信息ID（必须）</summary>	
		[Description("项目信息ID（必须）")]
        public string ProjectInfoID { get; set; } // ProjectInfoID
		/// <summary>OBS岗位ID</summary>	
		[Description("OBS岗位ID")]
        public string OBSID { get; set; } // OBSID
		/// <summary></summary>	
		[Description("")]
        public string MajorValue { get; set; } // MajorValue
		/// <summary>岗位角色编码</summary>	
		[Description("岗位角色编码")]
        public string RoleCode { get; set; } // RoleCode
		/// <summary>岗位角色名称</summary>	
		[Description("岗位角色名称")]
        public string RoleName { get; set; } // RoleName
		/// <summary>人员ID</summary>	
		[Description("人员ID")]
        public string UserID { get; set; } // UserID
		/// <summary>人员姓名</summary>	
		[Description("人员姓名")]
        public string UserName { get; set; } // UserName
		/// <summary></summary>	
		[Description("")]
        public string UserDeptID { get; set; } // UserDeptID
		/// <summary></summary>	
		[Description("")]
        public string UserDeptName { get; set; } // UserDeptName
		/// <summary></summary>	
		[Description("")]
        public DateTime? PlanStartDate { get; set; } // PlanStartDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? PlanEndDate { get; set; } // PlanEndDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? FactStartDate { get; set; } // FactStartDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? FactEndDate { get; set; } // FactEndDate

        // Foreign keys
		[JsonIgnore]
        public virtual S_W_WBS S_W_WBS { get; set; } //  WBSID - FK_S_W_RBS_S_W_WBS
    }

	/// <summary>工作包的标准工日主表</summary>	
	[Description("工作包的标准工日主表")]
    public partial class S_W_StandardWorkTime : Formula.BaseModel
    {
		/// <summary>主键(GUID)</summary>	
		[Description("主键(GUID)")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary>项目ID</summary>	
		[Description("项目ID")]
        public string ProjectInfoID { get; set; } // ProjectInfoID
		/// <summary>项目编号</summary>	
		[Description("项目编号")]
        public string ProjectInfoCode { get; set; } // ProjectInfoCode
		/// <summary>项目名称</summary>	
		[Description("项目名称")]
        public string ProjectInfoName { get; set; } // ProjectInfoName
		/// <summary>WBSID</summary>	
		[Description("WBSID")]
        public string WBSID { get; set; } // WBSID
		/// <summary>工作包ID</summary>	
		[Description("工作包ID")]
        public string TaskWorkID { get; set; } // TaskWorkID
		/// <summary></summary>	
		[Description("")]
        public string TaskWorkCode { get; set; } // TaskWorkCode
		/// <summary>工日</summary>	
		[Description("工日")]
        public double? WorkDay { get; set; } // WorkDay
		/// <summary>专业编号</summary>	
		[Description("专业编号")]
        public string MajorCode { get; set; } // MajorCode
		/// <summary>专业名称</summary>	
		[Description("专业名称")]
        public string MajorName { get; set; } // MajorName
		/// <summary>设计人IDs</summary>	
		[Description("设计人IDs")]
        public string DesignerUserID { get; set; } // DesignerUserID
		/// <summary>设计人Names</summary>	
		[Description("设计人Names")]
        public string DesignerUserName { get; set; } // DesignerUserName
		/// <summary>校核人Ids</summary>	
		[Description("校核人Ids")]
        public string CollactorUserID { get; set; } // CollactorUserID
		/// <summary>校核人Names</summary>	
		[Description("校核人Names")]
        public string CollactorUserName { get; set; } // CollactorUserName
		/// <summary>校审人Ids</summary>	
		[Description("校审人Ids")]
        public string AuditorUserID { get; set; } // AuditorUserID
		/// <summary>校审人Names</summary>	
		[Description("校审人Names")]
        public string AuditorUserName { get; set; } // AuditorUserName
		/// <summary>审定人IDs</summary>	
		[Description("审定人IDs")]
        public string ApproverUserID { get; set; } // ApproverUserID
		/// <summary>审定人Names</summary>	
		[Description("审定人Names")]
        public string ApproverUserName { get; set; } // ApproverUserName
		/// <summary>制图人Ids</summary>	
		[Description("制图人Ids")]
        public string MapperUserID { get; set; } // MapperUserID
		/// <summary>制图人Names</summary>	
		[Description("制图人Names")]
        public string MapperUserName { get; set; } // MapperUserName
		/// <summary>创建日期</summary>	
		[Description("创建日期")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary>创建人Id</summary>	
		[Description("创建人Id")]
        public string CreateUserID { get; set; } // CreateUserID
		/// <summary>创建人Name</summary>	
		[Description("创建人Name")]
        public string CreateUser { get; set; } // CreateUser
		/// <summary>修改日期</summary>	
		[Description("修改日期")]
        public DateTime? ModifyDate { get; set; } // ModifyDate
		/// <summary>修改人ID</summary>	
		[Description("修改人ID")]
        public string ModifyUserID { get; set; } // ModifyUserID
		/// <summary>修改人</summary>	
		[Description("修改人")]
        public string ModifyUser { get; set; } // ModifyUser

        // Reverse navigation
		[JsonIgnore]
        public virtual ICollection<S_W_StandardWorkTimeDetail> S_W_StandardWorkTimeDetail { get { onS_W_StandardWorkTimeDetailGetting(); return _S_W_StandardWorkTimeDetail;} }
		private ICollection<S_W_StandardWorkTimeDetail> _S_W_StandardWorkTimeDetail;
		partial void onS_W_StandardWorkTimeDetailGetting();


        public S_W_StandardWorkTime()
        {
            _S_W_StandardWorkTimeDetail = new List<S_W_StandardWorkTimeDetail>();
        }
    }

	/// <summary></summary>	
	[Description("")]
    public partial class S_W_StandardWorkTimeDetail : Formula.BaseModel
    {
		/// <summary>主键(ID)</summary>	
		[Description("主键(ID)")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary>主表ID</summary>	
		[Description("主表ID")]
        public string FormID { get; set; } // FormID
		/// <summary>项目ID</summary>	
		[Description("项目ID")]
        public string ProjectInfoID { get; set; } // ProjectInfoID
		/// <summary>WBSID(工作包节点)</summary>	
		[Description("WBSID(工作包节点)")]
        public string WBSID { get; set; } // WBSID
		/// <summary>工作包ID</summary>	
		[Description("工作包ID")]
        public string TaskWorkID { get; set; } // TaskWorkID
		/// <summary>用户ID</summary>	
		[Description("用户ID")]
        public string UserID { get; set; } // UserID
		/// <summary>用户名称</summary>	
		[Description("用户名称")]
        public string UserName { get; set; } // UserName
		/// <summary>项目角色(设、校、审)</summary>	
		[Description("项目角色(设、校、审)")]
        public string RoleCode { get; set; } // RoleCode
		/// <summary></summary>	
		[Description("")]
        public string RoleName { get; set; } // RoleName
		/// <summary>工日</summary>	
		[Description("工日")]
        public double? WorkDay { get; set; } // WorkDay
		/// <summary></summary>	
		[Description("")]
        public double? ScaleRatio { get; set; } // ScaleRatio
		/// <summary>创建日期</summary>	
		[Description("创建日期")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary>创建人ID</summary>	
		[Description("创建人ID")]
        public string CreateUserID { get; set; } // CreateUserID
		/// <summary>创建人</summary>	
		[Description("创建人")]
        public string CreateUser { get; set; } // CreateUser
		/// <summary>修改日期</summary>	
		[Description("修改日期")]
        public DateTime? ModifyDate { get; set; } // ModifyDate
		/// <summary>修改人ID</summary>	
		[Description("修改人ID")]
        public string ModifyUserID { get; set; } // ModifyUserID
		/// <summary>修改人</summary>	
		[Description("修改人")]
        public string ModifyUser { get; set; } // ModifyUser

        // Foreign keys
		[JsonIgnore]
        public virtual S_W_StandardWorkTime S_W_StandardWorkTime { get; set; } //  FormID - FK_S_W_StandardWorkTimeDetail_S_W_StandardWorkTime
    }

	/// <summary>专业卷册策划</summary>	
	[Description("专业卷册策划")]
    public partial class S_W_TaskWork : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string ProjectInfoID { get; set; } // ProjectInfoID
		/// <summary></summary>	
		[Description("")]
        public string WBSID { get; set; } // WBSID
		/// <summary></summary>	
		[Description("")]
        public string WBSFullID { get; set; } // WBSFullID
		/// <summary></summary>	
		[Description("")]
        public string SubProjectCode { get; set; } // SubProjectCode
		/// <summary></summary>	
		[Description("")]
        public string MajorValue { get; set; } // MajorValue
		/// <summary></summary>	
		[Description("")]
        public string PhaseValue { get; set; } // PhaseValue
		/// <summary></summary>	
		[Description("")]
        public string AreaCode { get; set; } // AreaCode
		/// <summary></summary>	
		[Description("")]
        public string DeviceCode { get; set; } // DeviceCode
		/// <summary>名称</summary>	
		[Description("名称")]
        public string Name { get; set; } // Name
		/// <summary>编号</summary>	
		[Description("编号")]
        public string Code { get; set; } // Code
		/// <summary>工时</summary>	
		[Description("工时")]
        public decimal? Workload { get; set; } // Workload
		/// <summary></summary>	
		[Description("")]
        public string WorkloadDistribute { get; set; } // WorkloadDistribute
		/// <summary></summary>	
		[Description("")]
        public decimal? WorkloadFinish { get; set; } // WorkloadFinish
		/// <summary></summary>	
		[Description("")]
        public DateTime? PlanStartDate { get; set; } // PlanStartDate
		/// <summary>计划校审完成日期</summary>	
		[Description("计划校审完成日期")]
        public DateTime? PlanEndDate { get; set; } // PlanEndDate
		/// <summary>计划出版日期</summary>	
		[Description("计划出版日期")]
        public DateTime? PlanPublishDate { get; set; } // PlanPublishDate
		/// <summary>标准工日</summary>	
		[Description("标准工日")]
        public double? StandartWorkDay { get; set; } // StandartWorkDay
		/// <summary>设计时间</summary>	
		[Description("设计时间")]
        public DateTime? DesignDate { get; set; } // DesignDate
		/// <summary>校对时间</summary>	
		[Description("校对时间")]
        public DateTime? CollactDate { get; set; } // CollactDate
		/// <summary>审核时间</summary>	
		[Description("审核时间")]
        public DateTime? AuditDate { get; set; } // AuditDate
		/// <summary>会签时间</summary>	
		[Description("会签时间")]
        public DateTime? CountersignDate { get; set; } // CountersignDate
		/// <summary>审定时间</summary>	
		[Description("审定时间")]
        public DateTime? ApproveDate { get; set; } // ApproveDate
		/// <summary>交付时间</summary>	
		[Description("交付时间")]
        public DateTime? DeliveryDate { get; set; } // DeliveryDate
		/// <summary>归档时间</summary>	
		[Description("归档时间")]
        public DateTime? FileDate { get; set; } // FileDate
		/// <summary></summary>	
		[Description("")]
        public int? PlanYear { get; set; } // PlanYear
		/// <summary></summary>	
		[Description("")]
        public int? PlanSeason { get; set; } // PlanSeason
		/// <summary></summary>	
		[Description("")]
        public int? PlanMonth { get; set; } // PlanMonth
		/// <summary>校审完成日期</summary>	
		[Description("校审完成日期")]
        public DateTime? FactEndDate { get; set; } // FactEndDate
		/// <summary>出版日期</summary>	
		[Description("出版日期")]
        public DateTime? FactPublishDate { get; set; } // FactPublishDate
		/// <summary></summary>	
		[Description("")]
        public int? FactYear { get; set; } // FactYear
		/// <summary></summary>	
		[Description("")]
        public int? FactSeason { get; set; } // FactSeason
		/// <summary></summary>	
		[Description("")]
        public int? FactMonth { get; set; } // FactMonth
		/// <summary></summary>	
		[Description("")]
        public string ChargeUserID { get; set; } // ChargeUserID
		/// <summary></summary>	
		[Description("")]
        public string ChargeUserName { get; set; } // ChargeUserName
		/// <summary></summary>	
		[Description("")]
        public string ChargeDeptID { get; set; } // ChargeDeptID
		/// <summary></summary>	
		[Description("")]
        public string ChargeDeptName { get; set; } // ChargeDeptName
		/// <summary>设计人ID</summary>	
		[Description("设计人ID")]
        public string DesignerUserID { get; set; } // DesignerUserID
		/// <summary>设计人姓名</summary>	
		[Description("设计人姓名")]
        public string DesignerUserName { get; set; } // DesignerUserName
		/// <summary>校核人ID</summary>	
		[Description("校核人ID")]
        public string CollactorUserID { get; set; } // CollactorUserID
		/// <summary>校核人姓名</summary>	
		[Description("校核人姓名")]
        public string CollactorUserName { get; set; } // CollactorUserName
		/// <summary>审核人ID</summary>	
		[Description("审核人ID")]
        public string AuditorUserID { get; set; } // AuditorUserID
		/// <summary>审核人姓名</summary>	
		[Description("审核人姓名")]
        public string AuditorUserName { get; set; } // AuditorUserName
		/// <summary>审定人ID</summary>	
		[Description("审定人ID")]
        public string ApproverUserID { get; set; } // ApproverUserID
		/// <summary>审定人姓名</summary>	
		[Description("审定人姓名")]
        public string ApproverUserName { get; set; } // ApproverUserName
		/// <summary>制图人ID</summary>	
		[Description("制图人ID")]
        public string MapperUserID { get; set; } // MapperUserID
		/// <summary>制图人姓名</summary>	
		[Description("制图人姓名")]
        public string MapperUserName { get; set; } // MapperUserName
		/// <summary></summary>	
		[Description("")]
        public string State { get; set; } // State
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get; set; } // CreateUserID
		/// <summary></summary>	
		[Description("")]
        public string CreateUser { get; set; } // CreateUser
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public string ModifyUserID { get; set; } // ModifyUserID
		/// <summary></summary>	
		[Description("")]
        public string ModifyUser { get; set; } // ModifyUser
		/// <summary></summary>	
		[Description("")]
        public DateTime? ModifyDate { get; set; } // ModifyDate
		/// <summary></summary>	
		[Description("")]
        public string RoleRate { get; set; } // RoleRate
		/// <summary></summary>	
		[Description("")]
        public string PublishID { get; set; } // PublishID
		/// <summary></summary>	
		[Description("")]
        public string ChangeID { get; set; } // ChangeID
		/// <summary></summary>	
		[Description("")]
        public string SettlementID { get; set; } // SettlementID
		/// <summary></summary>	
		[Description("")]
        public string DossierName { get; set; } // DossierName
		/// <summary></summary>	
		[Description("")]
        public string DossierCode { get; set; } // DossierCode
		/// <summary></summary>	
		[Description("")]
        public decimal? Version { get; set; } // Version
		/// <summary></summary>	
		[Description("")]
        public string ChangeState { get; set; } // ChangeState
		/// <summary>预估产值计算类型</summary>	
		[Description("预估产值计算类型")]
        public string EstimateType { get; set; } // EstimateType
		/// <summary>工日或面积</summary>	
		[Description("工日或面积")]
        public decimal? EstimateQuantity { get; set; } // EstimateQuantity
		/// <summary>工日薪酬或计产单价</summary>	
		[Description("工日薪酬或计产单价")]
        public decimal? EstimateUnitPrice { get; set; } // EstimateUnitPrice
		/// <summary>预估产值</summary>	
		[Description("预估产值")]
        public decimal? EstimateValue { get; set; } // EstimateValue
		/// <summary></summary>	
		[Description("")]
        public decimal? TotalValue { get; set; } // TotalValue
		/// <summary></summary>	
		[Description("")]
        public decimal? TemplateWorkload { get; set; } // TemplateWorkload
		/// <summary></summary>	
		[Description("")]
        public decimal? Coefficient { get; set; } // Coefficient

        // Reverse navigation
		[JsonIgnore]
        public virtual ICollection<S_W_TaskWork_RoleRate> S_W_TaskWork_RoleRate { get { onS_W_TaskWork_RoleRateGetting(); return _S_W_TaskWork_RoleRate;} }
		private ICollection<S_W_TaskWork_RoleRate> _S_W_TaskWork_RoleRate;
		partial void onS_W_TaskWork_RoleRateGetting();


        // Foreign keys
		[JsonIgnore]
        public virtual S_W_WBS S_W_WBS { get; set; } //  WBSID - FK_S_W_TaskWork_S_W_WBS

        public S_W_TaskWork()
        {
            _S_W_TaskWork_RoleRate = new List<S_W_TaskWork_RoleRate>();
        }
    }

	/// <summary>角色工时分配</summary>	
	[Description("角色工时分配")]
    public partial class S_W_TaskWork_RoleRate : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string S_W_TaskWorkID { get; set; } // S_W_TaskWorkID
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string IsReleased { get; set; } // IsReleased
		/// <summary>角色</summary>	
		[Description("角色")]
        public string Role { get; set; } // Role
		/// <summary>Member</summary>	
		[Description("Member")]
        public string Member { get; set; } // Member
		/// <summary>Member名称</summary>	
		[Description("Member名称")]
        public string MemberName { get; set; } // MemberName
		/// <summary>比例(%)</summary>	
		[Description("比例(%)")]
        public decimal? Rate { get; set; } // Rate
		/// <summary>工时</summary>	
		[Description("工时")]
        public decimal? Workload { get; set; } // Workload

        // Foreign keys
		[JsonIgnore]
        public virtual S_W_TaskWork S_W_TaskWork { get; set; } //  S_W_TaskWorkID - FK_S_W_TaskWork_RoleRate_S_W_TaskWork
    }

	/// <summary>成果一览左侧WBS树</summary>	
	[Description("成果一览左侧WBS树")]
    public partial class S_W_WBS : Formula.BaseModel
    {
		/// <summary>主键ID</summary>	
		[Description("主键ID")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary>关联项目ID</summary>	
		[Description("关联项目ID")]
        public string ProjectInfoID { get; set; } // ProjectInfoID
		/// <summary>父节点ID</summary>	
		[Description("父节点ID")]
        public string ParentID { get; set; } // ParentID
		/// <summary>全路径ID</summary>	
		[Description("全路径ID")]
        public string FullID { get; set; } // FullID
		/// <summary>WBS名称</summary>	
		[Description("WBS名称")]
        public string Name { get; set; } // Name
		/// <summary>WBS编号</summary>	
		[Description("WBS编号")]
        public string Code { get; set; } // Code
		/// <summary>WBS值</summary>	
		[Description("WBS值")]
        public string WBSValue { get; set; } // WBSValue
		/// <summary>WBS类别</summary>	
		[Description("WBS类别")]
        public string WBSType { get; set; } // WBSType
		/// <summary></summary>	
		[Description("")]
        public double SortIndex { get; set; } // SortIndex
		/// <summary>WBS层级</summary>	
		[Description("WBS层级")]
        public int Level { get; set; } // Level
		/// <summary>WBS状态</summary>	
		[Description("WBS状态")]
        public string State { get; set; } // State
		/// <summary>WBS责任部门ID</summary>	
		[Description("WBS责任部门ID")]
        public string WBSDeptID { get; set; } // WBSDeptID
		/// <summary>责任部门</summary>	
		[Description("责任部门")]
        public string WBSDeptName { get; set; } // WBSDeptName
		/// <summary>责任人ID</summary>	
		[Description("责任人ID")]
        public string ChargeUserID { get; set; } // ChargeUserID
		/// <summary>责任人</summary>	
		[Description("责任人")]
        public string ChargeUserName { get; set; } // ChargeUserName
		/// <summary>关联WBS结构编码</summary>	
		[Description("关联WBS结构编码")]
        public string WBSStructCode { get; set; } // WBSStructCode
		/// <summary>计划开始日期</summary>	
		[Description("计划开始日期")]
        public DateTime? PlanStartDate { get; set; } // PlanStartDate
		/// <summary>计划结束日期</summary>	
		[Description("计划结束日期")]
        public DateTime? PlanEndDate { get; set; } // PlanEndDate
		/// <summary>实际开始日期</summary>	
		[Description("实际开始日期")]
        public DateTime? FactStartDate { get; set; } // FactStartDate
		/// <summary>实际结束日期</summary>	
		[Description("实际结束日期")]
        public DateTime? FactEndDate { get; set; } // FactEndDate
		/// <summary>计划工日</summary>	
		[Description("计划工日")]
        public decimal? PlanWorkLoad { get; set; } // PlanWorkLoad
		/// <summary>实际工日</summary>	
		[Description("实际工日")]
        public decimal? WorkLoad { get; set; } // WorkLoad
		/// <summary>WBS权重</summary>	
		[Description("WBS权重")]
        public decimal? Weight { get; set; } // Weight
		/// <summary></summary>	
		[Description("")]
        public string ProjectCode { get; set; } // ProjectCode
		/// <summary></summary>	
		[Description("")]
        public string SubProjectCode { get; set; } // SubProjectCode
		/// <summary></summary>	
		[Description("")]
        public string PhaseCode { get; set; } // PhaseCode
		/// <summary></summary>	
		[Description("")]
        public string MajorCode { get; set; } // MajorCode
		/// <summary></summary>	
		[Description("")]
        public string WorkCode { get; set; } // WorkCode
		/// <summary></summary>	
		[Description("")]
        public string EntityCode { get; set; } // EntityCode
		/// <summary></summary>	
		[Description("")]
        public string AreaCode { get; set; } // AreaCode
		/// <summary></summary>	
		[Description("")]
        public string DeviceCode { get; set; } // DeviceCode
		/// <summary></summary>	
		[Description("")]
        public int? CodeIndex { get; set; } // CodeIndex
		/// <summary>子项的建筑面积</summary>	
		[Description("子项的建筑面积")]
        public string ExtField1 { get; set; } // ExtField1
		/// <summary>单体</summary>	
		[Description("单体")]
        public string ExtField2 { get; set; } // ExtField2
		/// <summary>WBS关联提资里程碑</summary>	
		[Description("WBS关联提资里程碑")]
        public string ExtField3 { get; set; } // ExtField3
		/// <summary>编号规则扩展</summary>	
		[Description("编号规则扩展")]
        public string ExtField4 { get; set; } // ExtField4
		/// <summary></summary>	
		[Description("")]
        public string CreateUser { get; set; } // CreateUser
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get; set; } // CreateUserID
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public string ModifyUserID { get; set; } // ModifyUserID
		/// <summary></summary>	
		[Description("")]
        public string ModifyUser { get; set; } // ModifyUser
		/// <summary></summary>	
		[Description("")]
        public DateTime? ModifyDate { get; set; } // ModifyDate
		/// <summary></summary>	
		[Description("")]
        public string RelateMileStone { get; set; } // RelateMileStone
		/// <summary></summary>	
		[Description("")]
        public DateTime? BasePlanStartDate { get; set; } // BasePlanStartDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? BasePlanEndDate { get; set; } // BasePlanEndDate

        // Reverse navigation
		[JsonIgnore]
        public virtual ICollection<S_E_Product> S_E_Product { get { onS_E_ProductGetting(); return _S_E_Product;} }
		private ICollection<S_E_Product> _S_E_Product;
		partial void onS_E_ProductGetting();

		[JsonIgnore]
        public virtual ICollection<S_W_Monomer> S_W_Monomer { get { onS_W_MonomerGetting(); return _S_W_Monomer;} }
		private ICollection<S_W_Monomer> _S_W_Monomer;
		partial void onS_W_MonomerGetting();

		[JsonIgnore]
        public virtual ICollection<S_W_RBS> S_W_RBS { get { onS_W_RBSGetting(); return _S_W_RBS;} }
		private ICollection<S_W_RBS> _S_W_RBS;
		partial void onS_W_RBSGetting();

		[JsonIgnore]
        public virtual ICollection<S_W_TaskWork> S_W_TaskWork { get { onS_W_TaskWorkGetting(); return _S_W_TaskWork;} }
		private ICollection<S_W_TaskWork> _S_W_TaskWork;
		partial void onS_W_TaskWorkGetting();


        // Foreign keys
		[JsonIgnore]
        public virtual S_I_ProjectInfo S_I_ProjectInfo { get; set; } //  ProjectInfoID - FK_S_W_WBS_S_I_ProjectInfo

        public S_W_WBS()
        {
            _S_E_Product = new List<S_E_Product>();
            _S_W_Monomer = new List<S_W_Monomer>();
            _S_W_RBS = new List<S_W_RBS>();
            _S_W_TaskWork = new List<S_W_TaskWork>();
        }
    }

	/// <summary></summary>	
	[Description("")]
    public partial class S_W_WBS_Version : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string CreateUser { get; set; } // CreateUser
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get; set; } // CreateUserID
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public string ModifyUserID { get; set; } // ModifyUserID
		/// <summary></summary>	
		[Description("")]
        public string ModifyUser { get; set; } // ModifyUser
		/// <summary></summary>	
		[Description("")]
        public DateTime? ModifyDate { get; set; } // ModifyDate
		/// <summary></summary>	
		[Description("")]
        public string OrgID { get; set; } // OrgID
		/// <summary></summary>	
		[Description("")]
        public string StepName { get; set; } // StepName
		/// <summary></summary>	
		[Description("")]
        public string FlowInfo { get; set; } // FlowInfo
		/// <summary></summary>	
		[Description("")]
        public string FlowPhase { get; set; } // FlowPhase
		/// <summary></summary>	
		[Description("")]
        public string ProjectInfoID { get; set; } // ProjectInfoID
		/// <summary></summary>	
		[Description("")]
        public decimal? VersionNumber { get; set; } // VersionNumber
		/// <summary></summary>	
		[Description("")]
        public string VersionName { get; set; } // VersionName
		/// <summary></summary>	
		[Description("")]
        public string ProjectInfo { get; set; } // ProjectInfo
		/// <summary></summary>	
		[Description("")]
        public string ProjectInfoName { get; set; } // ProjectInfoName
		/// <summary></summary>	
		[Description("")]
        public string ProjectInfoCode { get; set; } // ProjectInfoCode
		/// <summary></summary>	
		[Description("")]
        public string Remark { get; set; } // Remark

        // Reverse navigation
		[JsonIgnore]
        public virtual ICollection<S_W_WBS_Version_Node> S_W_WBS_Version_Node { get { onS_W_WBS_Version_NodeGetting(); return _S_W_WBS_Version_Node;} }
		private ICollection<S_W_WBS_Version_Node> _S_W_WBS_Version_Node;
		partial void onS_W_WBS_Version_NodeGetting();


        // Foreign keys
		[JsonIgnore]
        public virtual S_I_ProjectInfo S_I_ProjectInfo { get; set; } //  ProjectInfoID - FK_S_W_WBS_Version_S_I_ProjectInfo

        public S_W_WBS_Version()
        {
            _S_W_WBS_Version_Node = new List<S_W_WBS_Version_Node>();
        }
    }

	/// <summary></summary>	
	[Description("")]
    public partial class S_W_WBS_Version_Node : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string ProjectInfoID { get; set; } // ProjectInfoID
		/// <summary></summary>	
		[Description("")]
        public string VersionID { get; set; } // VersionID
		/// <summary></summary>	
		[Description("")]
        public string WBSID { get; set; } // WBSID
		/// <summary></summary>	
		[Description("")]
        public string WBSParentID { get; set; } // WBSParentID
		/// <summary></summary>	
		[Description("")]
        public string FullID { get; set; } // FullID
		/// <summary></summary>	
		[Description("")]
        public string Name { get; set; } // Name
		/// <summary></summary>	
		[Description("")]
        public string Code { get; set; } // Code
		/// <summary></summary>	
		[Description("")]
        public string WBSValue { get; set; } // WBSValue
		/// <summary></summary>	
		[Description("")]
        public string WBSType { get; set; } // WBSType
		/// <summary></summary>	
		[Description("")]
        public double SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public int? Level { get; set; } // Level
		/// <summary></summary>	
		[Description("")]
        public string State { get; set; } // State
		/// <summary></summary>	
		[Description("")]
        public string WBSDeptID { get; set; } // WBSDeptID
		/// <summary></summary>	
		[Description("")]
        public string WBSDeptName { get; set; } // WBSDeptName
		/// <summary></summary>	
		[Description("")]
        public string ChargeUserID { get; set; } // ChargeUserID
		/// <summary></summary>	
		[Description("")]
        public string ChargeUserName { get; set; } // ChargeUserName
		/// <summary></summary>	
		[Description("")]
        public string WBSStructCode { get; set; } // WBSStructCode
		/// <summary></summary>	
		[Description("")]
        public DateTime? PlanStartDate { get; set; } // PlanStartDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? PlanEndDate { get; set; } // PlanEndDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? FactStartDate { get; set; } // FactStartDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? FactEndDate { get; set; } // FactEndDate
		/// <summary></summary>	
		[Description("")]
        public decimal? PlanWorkLoad { get; set; } // PlanWorkLoad
		/// <summary></summary>	
		[Description("")]
        public decimal? WorkLoad { get; set; } // WorkLoad
		/// <summary></summary>	
		[Description("")]
        public decimal? Weight { get; set; } // Weight
		/// <summary></summary>	
		[Description("")]
        public string ProjectCode { get; set; } // ProjectCode
		/// <summary></summary>	
		[Description("")]
        public string SubProjectCode { get; set; } // SubProjectCode
		/// <summary></summary>	
		[Description("")]
        public string PhaseCode { get; set; } // PhaseCode
		/// <summary></summary>	
		[Description("")]
        public string MajorCode { get; set; } // MajorCode
		/// <summary></summary>	
		[Description("")]
        public string WorkCode { get; set; } // WorkCode
		/// <summary></summary>	
		[Description("")]
        public string EntityCode { get; set; } // EntityCode
		/// <summary></summary>	
		[Description("")]
        public string AreaCode { get; set; } // AreaCode
		/// <summary></summary>	
		[Description("")]
        public string DeviceCode { get; set; } // DeviceCode
		/// <summary></summary>	
		[Description("")]
        public int? CodeIndex { get; set; } // CodeIndex
		/// <summary></summary>	
		[Description("")]
        public string ModifyState { get; set; } // ModifyState
		/// <summary></summary>	
		[Description("")]
        public string RBSInfo { get; set; } // RBSInfo
		/// <summary></summary>	
		[Description("")]
        public string ExtField1 { get; set; } // ExtField1
		/// <summary></summary>	
		[Description("")]
        public string ExtField2 { get; set; } // ExtField2
		/// <summary></summary>	
		[Description("")]
        public string ExtField3 { get; set; } // ExtField3
		/// <summary></summary>	
		[Description("")]
        public string ExtField4 { get; set; } // ExtField4
		/// <summary></summary>	
		[Description("")]
        public string CreateUser { get; set; } // CreateUser
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get; set; } // CreateUserID
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public string ModifyUserID { get; set; } // ModifyUserID
		/// <summary></summary>	
		[Description("")]
        public string ModifyUser { get; set; } // ModifyUser
		/// <summary></summary>	
		[Description("")]
        public DateTime? ModifyDate { get; set; } // ModifyDate

        // Foreign keys
		[JsonIgnore]
        public virtual S_W_WBS_Version S_W_WBS_Version { get; set; } //  VersionID - FK_S_W_WBS_Version_Node_S_W_WBS_Version
    }

	/// <summary>WBS属性查看（CAD）</summary>	
	[Description("WBS属性查看（CAD）")]
    public partial class T_CAD_WBSAttrView : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? ModifyDate { get; set; } // ModifyDate
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get; set; } // CreateUserID
		/// <summary></summary>	
		[Description("")]
        public string CreateUser { get; set; } // CreateUser
		/// <summary></summary>	
		[Description("")]
        public string ModifyUserID { get; set; } // ModifyUserID
		/// <summary></summary>	
		[Description("")]
        public string ModifyUser { get; set; } // ModifyUser
		/// <summary></summary>	
		[Description("")]
        public string OrgID { get; set; } // OrgID
		/// <summary></summary>	
		[Description("")]
        public string CompanyID { get; set; } // CompanyID
		/// <summary></summary>	
		[Description("")]
        public string FlowPhase { get; set; } // FlowPhase
		/// <summary></summary>	
		[Description("")]
        public string FlowInfo { get; set; } // FlowInfo
		/// <summary></summary>	
		[Description("")]
        public string StepName { get; set; } // StepName
		/// <summary>项目名称</summary>	
		[Description("项目名称")]
        public string ProjectName { get; set; } // ProjectName
		/// <summary>项目编号</summary>	
		[Description("项目编号")]
        public string ProjectCode { get; set; } // ProjectCode
		/// <summary>项目阶段</summary>	
		[Description("项目阶段")]
        public string ProjectPhaseName { get; set; } // ProjectPhaseName
		/// <summary>主责部门</summary>	
		[Description("主责部门")]
        public string ProjectDeptName { get; set; } // ProjectDeptName
		/// <summary>项目负责人</summary>	
		[Description("项目负责人")]
        public string ProjectChangeUser { get; set; } // ProjectChangeUser
		/// <summary>节点名称</summary>	
		[Description("节点名称")]
        public string WBSName { get; set; } // WBSName
		/// <summary>节点编号</summary>	
		[Description("节点编号")]
        public string WBSCode { get; set; } // WBSCode
		/// <summary>负责人</summary>	
		[Description("负责人")]
        public string WBSChargeUser { get; set; } // WBSChargeUser
		/// <summary>所属专业</summary>	
		[Description("所属专业")]
        public string WBSMajorCode { get; set; } // WBSMajorCode
		/// <summary>计划完成时间</summary>	
		[Description("计划完成时间")]
        public string WBSPlanEndDate { get; set; } // WBSPlanEndDate
		/// <summary>实际完成时间</summary>	
		[Description("实际完成时间")]
        public string WBSFactEndDate { get; set; } // WBSFactEndDate
    }

	/// <summary>项目负责人变更</summary>	
	[Description("项目负责人变更")]
    public partial class T_CP_ProjectInfoChange : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? ModifyDate { get; set; } // ModifyDate
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get; set; } // CreateUserID
		/// <summary></summary>	
		[Description("")]
        public string CreateUser { get; set; } // CreateUser
		/// <summary></summary>	
		[Description("")]
        public string ModifyUserID { get; set; } // ModifyUserID
		/// <summary></summary>	
		[Description("")]
        public string ModifyUser { get; set; } // ModifyUser
		/// <summary></summary>	
		[Description("")]
        public string OrgID { get; set; } // OrgID
		/// <summary></summary>	
		[Description("")]
        public string CompanyID { get; set; } // CompanyID
		/// <summary></summary>	
		[Description("")]
        public string FlowPhase { get; set; } // FlowPhase
		/// <summary></summary>	
		[Description("")]
        public string FlowInfo { get; set; } // FlowInfo
		/// <summary></summary>	
		[Description("")]
        public string StepName { get; set; } // StepName
		/// <summary>项目名称</summary>	
		[Description("项目名称")]
        public string ProjectInfoName { get; set; } // ProjectInfoName
		/// <summary>项目编号</summary>	
		[Description("项目编号")]
        public string ProjectCode { get; set; } // ProjectCode
		/// <summary>业务类型</summary>	
		[Description("业务类型")]
        public string ProjectClass { get; set; } // ProjectClass
		/// <summary>项目规模</summary>	
		[Description("项目规模")]
        public string ProjectLevel { get; set; } // ProjectLevel
		/// <summary>责任部门</summary>	
		[Description("责任部门")]
        public string ProjectDept { get; set; } // ProjectDept
		/// <summary>责任部门名称</summary>	
		[Description("责任部门名称")]
        public string ProjectDeptName { get; set; } // ProjectDeptName
		/// <summary>其他参与部门</summary>	
		[Description("其他参与部门")]
        public string ProjectOtherDept { get; set; } // ProjectOtherDept
		/// <summary>其他参与部门名称</summary>	
		[Description("其他参与部门名称")]
        public string ProjectOtherDeptName { get; set; } // ProjectOtherDeptName
		/// <summary>阶段</summary>	
		[Description("阶段")]
        public string Phase { get; set; } // Phase
		/// <summary>原项目负责人</summary>	
		[Description("原项目负责人")]
        public string OrgProjectManger { get; set; } // OrgProjectManger
		/// <summary>原项目负责人名称</summary>	
		[Description("原项目负责人名称")]
        public string OrgProjectMangerName { get; set; } // OrgProjectMangerName
		/// <summary>变更后项目负责人</summary>	
		[Description("变更后项目负责人")]
        public string ProjectManager { get; set; } // ProjectManager
		/// <summary>变更后项目负责人名称</summary>	
		[Description("变更后项目负责人名称")]
        public string ProjectManagerName { get; set; } // ProjectManagerName
		/// <summary>部门领导意见</summary>	
		[Description("部门领导意见")]
        public string DeptLeaderAdvice { get; set; } // DeptLeaderAdvice
		/// <summary>计划经营部意见</summary>	
		[Description("计划经营部意见")]
        public string MarketAdvice { get; set; } // MarketAdvice
		/// <summary>主管领导意见</summary>	
		[Description("主管领导意见")]
        public string LeaderAdvice { get; set; } // LeaderAdvice
		/// <summary>MarketProjectInfo</summary>	
		[Description("MarketProjectInfo")]
        public string MarketProjectInfo { get; set; } // MarketProjectInfo
		/// <summary>ProjectInfoID</summary>	
		[Description("ProjectInfoID")]
        public string ProjectInfoID { get; set; } // ProjectInfoID
		/// <summary>Leader</summary>	
		[Description("Leader")]
        public string Leader { get; set; } // Leader
    }

	/// <summary>建筑任务单</summary>	
	[Description("建筑任务单")]
    public partial class T_CP_TaskNotice : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? ModifyDate { get; set; } // ModifyDate
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get; set; } // CreateUserID
		/// <summary></summary>	
		[Description("")]
        public string CreateUser { get; set; } // CreateUser
		/// <summary></summary>	
		[Description("")]
        public string ModifyUserID { get; set; } // ModifyUserID
		/// <summary></summary>	
		[Description("")]
        public string ModifyUser { get; set; } // ModifyUser
		/// <summary></summary>	
		[Description("")]
        public string OrgID { get; set; } // OrgID
		/// <summary></summary>	
		[Description("")]
        public string CompanyID { get; set; } // CompanyID
		/// <summary></summary>	
		[Description("")]
        public string FlowPhase { get; set; } // FlowPhase
		/// <summary></summary>	
		[Description("")]
        public string FlowInfo { get; set; } // FlowInfo
		/// <summary></summary>	
		[Description("")]
        public string StepName { get; set; } // StepName
		/// <summary>工程编号</summary>	
		[Description("工程编号")]
        public string EngineeringCode { get; set; } // EngineeringCode
		/// <summary>工程名称</summary>	
		[Description("工程名称")]
        public string EngineeringName { get; set; } // EngineeringName
		/// <summary>国家</summary>	
		[Description("国家")]
        public string Country { get; set; } // Country
		/// <summary>省份</summary>	
		[Description("省份")]
        public string Province { get; set; } // Province
		/// <summary>城市</summary>	
		[Description("城市")]
        public string City { get; set; } // City
		/// <summary>建设地点</summary>	
		[Description("建设地点")]
        public string BuildAddress { get; set; } // BuildAddress
		/// <summary>登记人</summary>	
		[Description("登记人")]
        public string LandArea { get; set; } // LandArea
		/// <summary>建筑面积</summary>	
		[Description("建筑面积")]
        public string BuildArea { get; set; } // BuildArea
		/// <summary>工程负责人</summary>	
		[Description("工程负责人")]
        public string EngineeringChargeUser { get; set; } // EngineeringChargeUser
		/// <summary>工程负责人名称</summary>	
		[Description("工程负责人名称")]
        public string EngineeringChargeUserName { get; set; } // EngineeringChargeUserName
		/// <summary>主要承接部门</summary>	
		[Description("主要承接部门")]
        public string MainDeptInfo { get; set; } // MainDeptInfo
		/// <summary>主要承接部门名称</summary>	
		[Description("主要承接部门名称")]
        public string MainDeptInfoName { get; set; } // MainDeptInfoName
		/// <summary>配合部门</summary>	
		[Description("配合部门")]
        public string EngineeringOtherDept { get; set; } // EngineeringOtherDept
		/// <summary>配合部门名称</summary>	
		[Description("配合部门名称")]
        public string EngineeringOtherDeptName { get; set; } // EngineeringOtherDeptName
		/// <summary>项目名称</summary>	
		[Description("项目名称")]
        public string ProjectInfo { get; set; } // ProjectInfo
		/// <summary>项目负责人</summary>	
		[Description("项目负责人")]
        public string ChargeUser { get; set; } // ChargeUser
		/// <summary>项目负责人名称</summary>	
		[Description("项目负责人名称")]
        public string ChargeUserName { get; set; } // ChargeUserName
		/// <summary>主责部门</summary>	
		[Description("主责部门")]
        public string ChargeDept { get; set; } // ChargeDept
		/// <summary>主责部门名称</summary>	
		[Description("主责部门名称")]
        public string ChargeDeptName { get; set; } // ChargeDeptName
		/// <summary>客户</summary>	
		[Description("客户")]
        public string Customer { get; set; } // Customer
		/// <summary>客户名称</summary>	
		[Description("客户名称")]
        public string CustomerName { get; set; } // CustomerName
		/// <summary>客户</summary>	
		[Description("客户")]
        public string CustomerSub { get; set; } // CustomerSub
		/// <summary>客户名称</summary>	
		[Description("客户名称")]
        public string CustomerSubName { get; set; } // CustomerSubName
		/// <summary>配合部门</summary>	
		[Description("配合部门")]
        public string OtherDept { get; set; } // OtherDept
		/// <summary>配合部门名称</summary>	
		[Description("配合部门名称")]
        public string OtherDeptName { get; set; } // OtherDeptName
		/// <summary>计划开始日期</summary>	
		[Description("计划开始日期")]
        public DateTime? PlanStartDate { get; set; } // PlanStartDate
		/// <summary>计划完工日期</summary>	
		[Description("计划完工日期")]
        public DateTime? PlanFinishDate { get; set; } // PlanFinishDate
		/// <summary>参与专业</summary>	
		[Description("参与专业")]
        public string Major { get; set; } // Major
		/// <summary>补充说明</summary>	
		[Description("补充说明")]
        public string Remark { get; set; } // Remark
		/// <summary>设计阶段</summary>	
		[Description("设计阶段")]
        public string Phase { get; set; } // Phase
		/// <summary>业务类型</summary>	
		[Description("业务类型")]
        public string ProjectClass { get; set; } // ProjectClass
		/// <summary>工作内容</summary>	
		[Description("工作内容")]
        public string WorkContent { get; set; } // WorkContent
		/// <summary>项目编号</summary>	
		[Description("项目编号")]
        public string SerialNumber { get; set; } // SerialNumber
		/// <summary>项目规模</summary>	
		[Description("项目规模")]
        public string ProjectLevel { get; set; } // ProjectLevel
		/// <summary>建筑层数（地上）</summary>	
		[Description("建筑层数（地上）")]
        public string UpFloors { get; set; } // UpFloors
		/// <summary>建筑层数（地下）</summary>	
		[Description("建筑层数（地下）")]
        public string DownFloors { get; set; } // DownFloors
		/// <summary>人防等级</summary>	
		[Description("人防等级")]
        public string DefenseLevel { get; set; } // DefenseLevel
		/// <summary>房屋总高度（米）</summary>	
		[Description("房屋总高度（米）")]
        public string Height { get; set; } // Height
		/// <summary>主体结构形式</summary>	
		[Description("主体结构形式")]
        public string StructuralStyle { get; set; } // StructuralStyle
		/// <summary>抗震设防烈度</summary>	
		[Description("抗震设防烈度")]
        public string ERI { get; set; } // ERI
		/// <summary>建筑分类（高层）</summary>	
		[Description("建筑分类（高层）")]
        public string BuildingClass { get; set; } // BuildingClass
		/// <summary>建筑结构的安全等级</summary>	
		[Description("建筑结构的安全等级")]
        public string SafetyLevel { get; set; } // SafetyLevel
		/// <summary>耐火等级</summary>	
		[Description("耐火等级")]
        public string FireLevel { get; set; } // FireLevel
		/// <summary>设计使用年限</summary>	
		[Description("设计使用年限")]
        public string ExpirationDate { get; set; } // ExpirationDate
		/// <summary>投资金额</summary>	
		[Description("投资金额")]
        public string Investment { get; set; } // Investment
		/// <summary>生产项目ID</summary>	
		[Description("生产项目ID")]
        public string ProjectInfoID { get; set; } // ProjectInfoID
		/// <summary>VersionNumber</summary>	
		[Description("VersionNumber")]
        public string VersionNumber { get; set; } // VersionNumber
		/// <summary>分组ID</summary>	
		[Description("分组ID")]
        public string GroupID { get; set; } // GroupID
		/// <summary>EngineeringID</summary>	
		[Description("EngineeringID")]
        public string EngineeringID { get; set; } // EngineeringID
		/// <summary>MarketProjectID</summary>	
		[Description("MarketProjectID")]
        public string MarketProjectID { get; set; } // MarketProjectID
		/// <summary>RelateID</summary>	
		[Description("RelateID")]
        public string RelateID { get; set; } // RelateID
		/// <summary>主管领导</summary>	
		[Description("主管领导")]
        public string Leader { get; set; } // Leader
		/// <summary>主管领导名称</summary>	
		[Description("主管领导名称")]
        public string LeaderName { get; set; } // LeaderName
		/// <summary>管理级别</summary>	
		[Description("管理级别")]
        public string ManageLevel { get; set; } // ManageLevel
		/// <summary>项目来源</summary>	
		[Description("项目来源")]
        public string ProjectSource { get; set; } // ProjectSource
		/// <summary>项目类型</summary>	
		[Description("项目类型")]
        public string ProjectType { get; set; } // ProjectType
		/// <summary>建筑类型</summary>	
		[Description("建筑类型")]
        public string BuildType { get; set; } // BuildType
		/// <summary>业主要求时间</summary>	
		[Description("业主要求时间")]
        public DateTime? CustomerRequireDate { get; set; } // CustomerRequireDate
		/// <summary>登记人名称</summary>	
		[Description("登记人名称")]
        public string LandAreaName { get; set; } // LandAreaName
		/// <summary>容积率</summary>	
		[Description("容积率")]
        public string FloorAreaRatio { get; set; } // FloorAreaRatio
		/// <summary>任务下达时间</summary>	
		[Description("任务下达时间")]
        public DateTime? DelegateDate { get; set; } // DelegateDate
		/// <summary>登记人</summary>	
		[Description("登记人")]
        public string Register { get; set; } // Register
		/// <summary>登记人名称</summary>	
		[Description("登记人名称")]
        public string RegisterName { get; set; } // RegisterName
		/// <summary>任务要求</summary>	
		[Description("任务要求")]
        public string TaskRequest { get; set; } // TaskRequest
		/// <summary>设计任务资料</summary>	
		[Description("设计任务资料")]
        public string DesignDocment { get; set; } // DesignDocment
		/// <summary>项目性质</summary>	
		[Description("项目性质")]
        public string ProjectSpecialty { get; set; } // ProjectSpecialty
		/// <summary>顾客要求评审</summary>	
		[Description("顾客要求评审")]
        public string CustomerRequestReview { get; set; } // CustomerRequestReview
		/// <summary>顾客要求评审名称</summary>	
		[Description("顾客要求评审名称")]
        public string CustomerRequestReviewName { get; set; } // CustomerRequestReviewName
		/// <summary>部门领导意见</summary>	
		[Description("部门领导意见")]
        public string DeptHead { get; set; } // DeptHead
		/// <summary>经营计划部意见</summary>	
		[Description("经营计划部意见")]
        public string BusinessPlanDept { get; set; } // BusinessPlanDept
		/// <summary>主管领导意见</summary>	
		[Description("主管领导意见")]
        public string ManagerSignature { get; set; } // ManagerSignature
		/// <summary>院总工意见</summary>	
		[Description("院总工意见")]
        public string ChiefEngineer { get; set; } // ChiefEngineer
		/// <summary>院领导意见</summary>	
		[Description("院领导意见")]
        public string HospitalLeaders { get; set; } // HospitalLeaders
		/// <summary>合作单位</summary>	
		[Description("合作单位")]
        public string CoopUnitID { get; set; } // CoopUnitID
		/// <summary>合作单位名称</summary>	
		[Description("合作单位名称")]
        public string CoopUnitIDName { get; set; } // CoopUnitIDName
		/// <summary>设总</summary>	
		[Description("设总")]
        public string DesignManager { get; set; } // DesignManager
		/// <summary>设总名称</summary>	
		[Description("设总名称")]
        public string DesignManagerName { get; set; } // DesignManagerName
		/// <summary>联系人</summary>	
		[Description("联系人")]
        public string LinkMan { get; set; } // LinkMan
		/// <summary>联系电话</summary>	
		[Description("联系电话")]
        public string LinkPhone { get; set; } // LinkPhone
		/// <summary>设计所</summary>	
		[Description("设计所")]
        public string DesignDept { get; set; } // DesignDept
		/// <summary>设计所名称</summary>	
		[Description("设计所名称")]
        public string DesignDeptName { get; set; } // DesignDeptName
		/// <summary>任务质量目标</summary>	
		[Description("任务质量目标")]
        public string QuanlityTarget { get; set; } // QuanlityTarget
		/// <summary>合同信息</summary>	
		[Description("合同信息")]
        public string ContractInfo { get; set; } // ContractInfo
		/// <summary>合同信息名称</summary>	
		[Description("合同信息名称")]
        public string ContractInfoName { get; set; } // ContractInfoName
		/// <summary>项目金额</summary>	
		[Description("项目金额")]
        public decimal? ContractValue { get; set; } // ContractValue
		/// <summary>表单编号</summary>	
		[Description("表单编号")]
        public string TmplCode { get; set; } // TmplCode
		/// <summary>下达工时</summary>	
		[Description("下达工时")]
        public decimal? Workload { get; set; } // Workload
		/// <summary>工时单价</summary>	
		[Description("工时单价")]
        public decimal? WorkloadUnitPrice { get; set; } // WorkloadUnitPrice
		/// <summary>设计依据</summary>	
		[Description("设计依据")]
        public string DesignBasis { get; set; } // DesignBasis
		/// <summary>扩展字段1</summary>	
		[Description("扩展字段1")]
        public string Extention1 { get; set; } // Extention1
		/// <summary>扩展字段2</summary>	
		[Description("扩展字段2")]
        public string Extention2 { get; set; } // Extention2
		/// <summary>扩展字段3</summary>	
		[Description("扩展字段3")]
        public string Extention3 { get; set; } // Extention3
		/// <summary>扩展字段4</summary>	
		[Description("扩展字段4")]
        public string Extention4 { get; set; } // Extention4
		/// <summary>扩展字段5</summary>	
		[Description("扩展字段5")]
        public string Extention5 { get; set; } // Extention5
		/// <summary>扩展字段6</summary>	
		[Description("扩展字段6")]
        public string Extention6 { get; set; } // Extention6
		/// <summary>扩展字段7</summary>	
		[Description("扩展字段7")]
        public string Extention7 { get; set; } // Extention7
		/// <summary>扩展字段8</summary>	
		[Description("扩展字段8")]
        public string Extention8 { get; set; } // Extention8
		/// <summary>扩展字段9</summary>	
		[Description("扩展字段9")]
        public string Extention9 { get; set; } // Extention9
		/// <summary>扩展字段10</summary>	
		[Description("扩展字段10")]
        public string Extention10 { get; set; } // Extention10
		/// <summary>经度</summary>	
		[Description("经度")]
        public double? Long { get; set; } // Long
		/// <summary>纬度</summary>	
		[Description("纬度")]
        public double? Lat { get; set; } // Lat
		/// <summary>详细地址</summary>	
		[Description("详细地址")]
        public string Address { get; set; } // Address
		/// <summary>行政区</summary>	
		[Description("行政区")]
        public string District { get; set; } // District
		/// <summary>是否采用一个任务单生成多个项目的模式(值取1采用)</summary>	
		[Description("是否采用一个任务单生成多个项目的模式(值取1采用)")]
        public string MultiProjMode { get; set; } // MultiProjMode
		/// <summary>阶段子表</summary>	
		[Description("阶段子表")]
        public string PhaseDetail { get; set; } // PhaseDetail
		/// <summary>区域</summary>	
		[Description("区域")]
        public string Area { get; set; } // Area
		/// <summary>税名</summary>	
		[Description("税名")]
        public string TaxName { get; set; } // TaxName
		/// <summary>税率</summary>	
		[Description("税率")]
        public decimal? TaxRate { get; set; } // TaxRate

        // Reverse navigation
		[JsonIgnore]
        public virtual ICollection<T_CP_TaskNotice_PhaseDetail> T_CP_TaskNotice_PhaseDetail { get { onT_CP_TaskNotice_PhaseDetailGetting(); return _T_CP_TaskNotice_PhaseDetail;} }
		private ICollection<T_CP_TaskNotice_PhaseDetail> _T_CP_TaskNotice_PhaseDetail;
		partial void onT_CP_TaskNotice_PhaseDetailGetting();


        public T_CP_TaskNotice()
        {
            _T_CP_TaskNotice_PhaseDetail = new List<T_CP_TaskNotice_PhaseDetail>();
        }
    }

	/// <summary>阶段子表</summary>	
	[Description("阶段子表")]
    public partial class T_CP_TaskNotice_PhaseDetail : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string T_CP_TaskNoticeID { get; set; } // T_CP_TaskNoticeID
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string IsReleased { get; set; } // IsReleased
		/// <summary>设计阶段</summary>	
		[Description("设计阶段")]
        public string Phase { get; set; } // Phase
		/// <summary>项目名称</summary>	
		[Description("项目名称")]
        public string Name { get; set; } // Name
		/// <summary>设计号</summary>	
		[Description("设计号")]
        public string Code { get; set; } // Code
		/// <summary>主责部门</summary>	
		[Description("主责部门")]
        public string ChargeDept { get; set; } // ChargeDept
		/// <summary>主责部门名称</summary>	
		[Description("主责部门名称")]
        public string ChargeDeptName { get; set; } // ChargeDeptName
		/// <summary>项目负责人</summary>	
		[Description("项目负责人")]
        public string ChargeUser { get; set; } // ChargeUser
		/// <summary>项目负责人名称</summary>	
		[Description("项目负责人名称")]
        public string ChargeUserName { get; set; } // ChargeUserName
		/// <summary>配合部门</summary>	
		[Description("配合部门")]
        public string OtherDept { get; set; } // OtherDept
		/// <summary>配合部门名称</summary>	
		[Description("配合部门名称")]
        public string OtherDeptName { get; set; } // OtherDeptName
		/// <summary>计划开始时间</summary>	
		[Description("计划开始时间")]
        public DateTime? PlanStartDate { get; set; } // PlanStartDate
		/// <summary>计划完成时间</summary>	
		[Description("计划完成时间")]
        public DateTime? PlanFinishDate { get; set; } // PlanFinishDate
		/// <summary>ProjectInfoID</summary>	
		[Description("ProjectInfoID")]
        public string ProjectInfoID { get; set; } // ProjectInfoID

        // Foreign keys
		[JsonIgnore]
        public virtual T_CP_TaskNotice T_CP_TaskNotice { get; set; } //  T_CP_TaskNoticeID - FK_T_CP_TaskNotice_PhaseDetail_T_CP_TaskNotice
    }

	/// <summary>校审单</summary>	
	[Description("校审单")]
    public partial class T_EXE_Audit : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? ModifyDate { get; set; } // ModifyDate
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get; set; } // CreateUserID
		/// <summary></summary>	
		[Description("")]
        public string CreateUser { get; set; } // CreateUser
		/// <summary></summary>	
		[Description("")]
        public string ModifyUserID { get; set; } // ModifyUserID
		/// <summary></summary>	
		[Description("")]
        public string ModifyUser { get; set; } // ModifyUser
		/// <summary></summary>	
		[Description("")]
        public string OrgID { get; set; } // OrgID
		/// <summary></summary>	
		[Description("")]
        public string CompanyID { get; set; } // CompanyID
		/// <summary></summary>	
		[Description("")]
        public string FlowPhase { get; set; } // FlowPhase
		/// <summary></summary>	
		[Description("")]
        public string FlowInfo { get; set; } // FlowInfo
		/// <summary></summary>	
		[Description("")]
        public string StepName { get; set; } // StepName
		/// <summary>项目名称</summary>	
		[Description("项目名称")]
        public string ProjectInfoName { get; set; } // ProjectInfoName
		/// <summary>项目编号</summary>	
		[Description("项目编号")]
        public string ProjectInfoCode { get; set; } // ProjectInfoCode
		/// <summary>子项名称</summary>	
		[Description("子项名称")]
        public string SubProjectName { get; set; } // SubProjectName
		/// <summary>阶段</summary>	
		[Description("阶段")]
        public string PhaseCode { get; set; } // PhaseCode
		/// <summary>编号</summary>	
		[Description("编号")]
        public string SerialNumber { get; set; } // SerialNumber
		/// <summary>专业</summary>	
		[Description("专业")]
        public string MajorCode { get; set; } // MajorCode
		/// <summary>校核人</summary>	
		[Description("校核人")]
        public string Collactor { get; set; } // Collactor
		/// <summary>校核人名称</summary>	
		[Description("校核人名称")]
        public string CollactorName { get; set; } // CollactorName
		/// <summary>审定人</summary>	
		[Description("审定人")]
        public string Approver { get; set; } // Approver
		/// <summary>审定人名称</summary>	
		[Description("审定人名称")]
        public string ApproverName { get; set; } // ApproverName
		/// <summary>审核人</summary>	
		[Description("审核人")]
        public string Auditor { get; set; } // Auditor
		/// <summary>审核人名称</summary>	
		[Description("审核人名称")]
        public string AuditorName { get; set; } // AuditorName
		/// <summary>校审完成日期</summary>	
		[Description("校审完成日期")]
        public DateTime? FinishDate { get; set; } // FinishDate
		/// <summary>设计人签字</summary>	
		[Description("设计人签字")]
        public string DesignSign { get; set; } // DesignSign
		/// <summary>复核人签字</summary>	
		[Description("复核人签字")]
        public string CollactorSign { get; set; } // CollactorSign
		/// <summary>审核人签字</summary>	
		[Description("审核人签字")]
        public string AuditorSign { get; set; } // AuditorSign
		/// <summary>审定人签字</summary>	
		[Description("审定人签字")]
        public string ApproverSign { get; set; } // ApproverSign
		/// <summary>校审成果IDs</summary>	
		[Description("校审成果IDs")]
        public string ProductIDs { get; set; } // ProductIDs
		/// <summary>项目ID</summary>	
		[Description("项目ID")]
        public string ProjectInfoID { get; set; } // ProjectInfoID
		/// <summary>WBS节点ID</summary>	
		[Description("WBS节点ID")]
        public string WBSID { get; set; } // WBSID
		/// <summary>设计人</summary>	
		[Description("设计人")]
        public string Designer { get; set; } // Designer
		/// <summary>设计人姓名</summary>	
		[Description("设计人姓名")]
        public string DesignerName { get; set; } // DesignerName
		/// <summary>版本号</summary>	
		[Description("版本号")]
        public string VersionNumber { get; set; } // VersionNumber
		/// <summary>项目负责人</summary>	
		[Description("项目负责人")]
        public string ProjectManager { get; set; } // ProjectManager
		/// <summary>项目负责人名称</summary>	
		[Description("项目负责人名称")]
        public string ProjectManagerName { get; set; } // ProjectManagerName
		/// <summary>项目负责人签字</summary>	
		[Description("项目负责人签字")]
        public string ProjectManagerSign { get; set; } // ProjectManagerSign
		/// <summary>设总</summary>	
		[Description("设总")]
        public string DesignManager { get; set; } // DesignManager
		/// <summary>设总名称</summary>	
		[Description("设总名称")]
        public string DesignManagerName { get; set; } // DesignManagerName
		/// <summary>制图人</summary>	
		[Description("制图人")]
        public string Mapper { get; set; } // Mapper
		/// <summary>制图人名称</summary>	
		[Description("制图人名称")]
        public string MapperName { get; set; } // MapperName
		/// <summary>专业负责人</summary>	
		[Description("专业负责人")]
        public string MajorPrinciple { get; set; } // MajorPrinciple
		/// <summary>专业负责人名称</summary>	
		[Description("专业负责人名称")]
        public string MajorPrincipleName { get; set; } // MajorPrincipleName
		/// <summary>专业总工程师</summary>	
		[Description("专业总工程师")]
        public string MajorEngineer { get; set; } // MajorEngineer
		/// <summary>专业总工程师名称</summary>	
		[Description("专业总工程师名称")]
        public string MajorEngineerName { get; set; } // MajorEngineerName
		/// <summary>部门负责人</summary>	
		[Description("部门负责人")]
        public string DeptManager { get; set; } // DeptManager
		/// <summary>部门负责人名称</summary>	
		[Description("部门负责人名称")]
        public string DeptManagerName { get; set; } // DeptManagerName
		/// <summary>成果信息</summary>	
		[Description("成果信息")]
        public string Products { get; set; } // Products
		/// <summary>校审意见</summary>	
		[Description("校审意见")]
        public string AdviceDetailH5 { get; set; } // AdviceDetailH5
		/// <summary>专业负责人签字</summary>	
		[Description("专业负责人签字")]
        public string MajorPrincipleSign { get; set; } // MajorPrincipleSign
		/// <summary>CAD图框签字来源（0:表单、1:流程）同时需修改枚举AuditSignCategory</summary>	
		[Description("CAD图框签字来源（0:表单、1:流程）同时需修改枚举AuditSignCategory")]
        public string AuditSignSource { get; set; } // AuditSignSource

        // Reverse navigation
		[JsonIgnore]
        public virtual ICollection<T_EXE_Audit_AdviceDetail> T_EXE_Audit_AdviceDetail { get { onT_EXE_Audit_AdviceDetailGetting(); return _T_EXE_Audit_AdviceDetail;} }
		private ICollection<T_EXE_Audit_AdviceDetail> _T_EXE_Audit_AdviceDetail;
		partial void onT_EXE_Audit_AdviceDetailGetting();


        public T_EXE_Audit()
        {
            _T_EXE_Audit_AdviceDetail = new List<T_EXE_Audit_AdviceDetail>();
        }
    }

	/// <summary>校审意见信息</summary>	
	[Description("校审意见信息")]
    public partial class T_EXE_Audit_AdviceDetail : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string T_EXE_AuditID { get; set; } // T_EXE_AuditID
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string IsReleased { get; set; } // IsReleased
		/// <summary>提出环节</summary>	
		[Description("提出环节")]
        public string Step { get; set; } // Step
		/// <summary>提出人</summary>	
		[Description("提出人")]
        public string SubmitUser { get; set; } // SubmitUser
		/// <summary>提出人名称</summary>	
		[Description("提出人名称")]
        public string SubmitUserName { get; set; } // SubmitUserName
		/// <summary>文件编号</summary>	
		[Description("文件编号")]
        public string ProductCode { get; set; } // ProductCode
		/// <summary>问题类型</summary>	
		[Description("问题类型")]
        public string MistakeType { get; set; } // MistakeType
		/// <summary>问题内容</summary>	
		[Description("问题内容")]
        public string MsitakeContent { get; set; } // MsitakeContent
		/// <summary>修改情况</summary>	
		[Description("修改情况")]
        public string ResponseContent { get; set; } // ResponseContent
		/// <summary>归属年份</summary>	
		[Description("归属年份")]
        public int? MistakeYear { get; set; } // MistakeYear
		/// <summary>归属月份</summary>	
		[Description("归属月份")]
        public int? MistakeMonth { get; set; } // MistakeMonth
		/// <summary>归属季度</summary>	
		[Description("归属季度")]
        public int? MistakeSeason { get; set; } // MistakeSeason
		/// <summary>创建人</summary>	
		[Description("创建人")]
        public string CreateUser { get; set; } // CreateUser
		/// <summary>创建人名称</summary>	
		[Description("创建人名称")]
        public string CreateUserName { get; set; } // CreateUserName
		/// <summary>创建时间</summary>	
		[Description("创建时间")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary>LPosX</summary>	
		[Description("LPosX")]
        public decimal? LPosX { get; set; } // LPosX
		/// <summary>LPosY</summary>	
		[Description("LPosY")]
        public decimal? LPosY { get; set; } // LPosY
		/// <summary>RPosX</summary>	
		[Description("RPosX")]
        public decimal? RPosX { get; set; } // RPosX
		/// <summary>RPosY</summary>	
		[Description("RPosY")]
        public decimal? RPosY { get; set; } // RPosY
		/// <summary>RevisePosX</summary>	
		[Description("RevisePosX")]
        public decimal? RevisePosX { get; set; } // RevisePosX
		/// <summary>RevisePosY</summary>	
		[Description("RevisePosY")]
        public decimal? RevisePosY { get; set; } // RevisePosY
		/// <summary>ReviseWidth</summary>	
		[Description("ReviseWidth")]
        public decimal? ReviseWidth { get; set; } // ReviseWidth
		/// <summary>ReviseHeight</summary>	
		[Description("ReviseHeight")]
        public decimal? ReviseHeight { get; set; } // ReviseHeight
		/// <summary>ReviseAudit</summary>	
		[Description("ReviseAudit")]
        public string ReviseAudit { get; set; } // ReviseAudit
		/// <summary>DrawingID</summary>	
		[Description("DrawingID")]
        public string DrawingID { get; set; } // DrawingID
		/// <summary>ReviseState</summary>	
		[Description("ReviseState")]
        public string ReviseState { get; set; } // ReviseState
		/// <summary>ReviseBasePoint</summary>	
		[Description("ReviseBasePoint")]
        public string ReviseBasePoint { get; set; } // ReviseBasePoint
		/// <summary>ReviseClass</summary>	
		[Description("ReviseClass")]
        public string ReviseClass { get; set; } // ReviseClass
		/// <summary>AuditPathID</summary>	
		[Description("AuditPathID")]
        public string AuditPathID { get; set; } // AuditPathID
		/// <summary>ReviseType</summary>	
		[Description("ReviseType")]
        public string ReviseType { get; set; } // ReviseType
		/// <summary>UpdateTime</summary>	
		[Description("UpdateTime")]
        public DateTime? UpdateTime { get; set; } // UpdateTime
		/// <summary>SubmitDept</summary>	
		[Description("SubmitDept")]
        public string SubmitDept { get; set; } // SubmitDept
		/// <summary>SubmitDeptName</summary>	
		[Description("SubmitDeptName")]
        public string SubmitDeptName { get; set; } // SubmitDeptName
		/// <summary>IsLead</summary>	
		[Description("IsLead")]
        public string IsLead { get; set; } // IsLead
		/// <summary>MarkType</summary>	
		[Description("MarkType")]
        public string MarkType { get; set; } // MarkType
		/// <summary>OpinionState</summary>	
		[Description("OpinionState")]
        public string OpinionState { get; set; } // OpinionState
		/// <summary>EntityId</summary>	
		[Description("EntityId")]
        public string EntityId { get; set; } // EntityId
		/// <summary>ComeForm</summary>	
		[Description("ComeForm")]
        public string ComeForm { get; set; } // ComeForm
		/// <summary>同步到校审知识库</summary>	
		[Description("同步到校审知识库")]
        public string Sync { get; set; } // Sync
		/// <summary>图框图名</summary>	
		[Description("图框图名")]
        public string DrawingName { get; set; } // DrawingName
		/// <summary>图框图号</summary>	
		[Description("图框图号")]
        public string DrawingCode { get; set; } // DrawingCode
		/// <summary></summary>	
		[Description("")]
        public string EntityInfoJosn { get; set; } // EntityInfoJosn
		/// <summary>批注评论历史列表</summary>	
		[Description("批注评论历史列表")]
        public string CommentInfoJson { get; set; } // CommentInfoJson
		/// <summary>批注评论当前环节列表</summary>	
		[Description("批注评论当前环节列表")]
        public string CommentTempJson { get; set; } // CommentTempJson
		/// <summary></summary>	
		[Description("")]
        public string OriginalPicID { get; set; } // OriginalPicID
		/// <summary></summary>	
		[Description("")]
        public string ModifiedPicID { get; set; } // ModifiedPicID
		/// <summary>修改历史</summary>	
		[Description("修改历史")]
        public string ResponseHistory { get; set; } // ResponseHistory

        // Foreign keys
		[JsonIgnore]
        public virtual T_EXE_Audit T_EXE_Audit { get; set; } //  T_EXE_AuditID - FK_T_EXE_Audit_AdviceDetail_T_EXE_Audit
    }

	/// <summary>方案评审</summary>	
	[Description("方案评审")]
    public partial class T_EXE_AuditReview : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? ModifyDate { get; set; } // ModifyDate
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get; set; } // CreateUserID
		/// <summary></summary>	
		[Description("")]
        public string CreateUser { get; set; } // CreateUser
		/// <summary></summary>	
		[Description("")]
        public string ModifyUserID { get; set; } // ModifyUserID
		/// <summary></summary>	
		[Description("")]
        public string ModifyUser { get; set; } // ModifyUser
		/// <summary></summary>	
		[Description("")]
        public string OrgID { get; set; } // OrgID
		/// <summary></summary>	
		[Description("")]
        public string CompanyID { get; set; } // CompanyID
		/// <summary></summary>	
		[Description("")]
        public string FlowPhase { get; set; } // FlowPhase
		/// <summary></summary>	
		[Description("")]
        public string FlowInfo { get; set; } // FlowInfo
		/// <summary></summary>	
		[Description("")]
        public string StepName { get; set; } // StepName
		/// <summary>项目编号</summary>	
		[Description("项目编号")]
        public string ProjectCode { get; set; } // ProjectCode
		/// <summary>项目名称</summary>	
		[Description("项目名称")]
        public string ProjectName { get; set; } // ProjectName
		/// <summary>设计阶段</summary>	
		[Description("设计阶段")]
        public string DesignPhase { get; set; } // DesignPhase
		/// <summary>项目总负责人</summary>	
		[Description("项目总负责人")]
        public string ProjectChief { get; set; } // ProjectChief
		/// <summary>项目总负责人名称</summary>	
		[Description("项目总负责人名称")]
        public string ProjectChiefName { get; set; } // ProjectChiefName
		/// <summary>编制日期</summary>	
		[Description("编制日期")]
        public DateTime? FilledDate { get; set; } // FilledDate
		/// <summary>评审主题</summary>	
		[Description("评审主题")]
        public string ReviewSubject { get; set; } // ReviewSubject
		/// <summary>评审意见</summary>	
		[Description("评审意见")]
        public string ReviewComments { get; set; } // ReviewComments
		/// <summary>附件</summary>	
		[Description("附件")]
        public string AttendanceChart { get; set; } // AttendanceChart
		/// <summary>所总工审批</summary>	
		[Description("所总工审批")]
        public string Recording { get; set; } // Recording
		/// <summary>所长审批</summary>	
		[Description("所长审批")]
        public string Confirmation { get; set; } // Confirmation
		/// <summary>修改情况</summary>	
		[Description("修改情况")]
        public string Modification { get; set; } // Modification
		/// <summary>技术质量部审批</summary>	
		[Description("技术质量部审批")]
        public string TechnicalDirector { get; set; } // TechnicalDirector
		/// <summary>验证人</summary>	
		[Description("验证人")]
        public string VerificationPerson { get; set; } // VerificationPerson
		/// <summary>修改情况确认</summary>	
		[Description("修改情况确认")]
        public string ChangeConfirmation { get; set; } // ChangeConfirmation
		/// <summary>验证确认人</summary>	
		[Description("验证确认人")]
        public string VerificationConfirmation { get; set; } // VerificationConfirmation
		/// <summary>专业</summary>	
		[Description("专业")]
        public string Major { get; set; } // Major
		/// <summary>评审类型</summary>	
		[Description("评审类型")]
        public string SpaceCode { get; set; } // SpaceCode
		/// <summary>项目ID</summary>	
		[Description("项目ID")]
        public string ProjectInfoID { get; set; } // ProjectInfoID
		/// <summary>流水号</summary>	
		[Description("流水号")]
        public string SerialNumber { get; set; } // SerialNumber
		/// <summary>版本号</summary>	
		[Description("版本号")]
        public string VersionNumber { get; set; } // VersionNumber
		/// <summary>会议地点</summary>	
		[Description("会议地点")]
        public string MeetingRoom { get; set; } // MeetingRoom
		/// <summary>会议时间</summary>	
		[Description("会议时间")]
        public DateTime? MeetingDate { get; set; } // MeetingDate
		/// <summary>出席人员</summary>	
		[Description("出席人员")]
        public string Attendence { get; set; } // Attendence
		/// <summary>出席人员名称</summary>	
		[Description("出席人员名称")]
        public string AttendenceName { get; set; } // AttendenceName
		/// <summary>意见总结</summary>	
		[Description("意见总结")]
        public string summarization { get; set; } // summarization
		/// <summary>项目负责人签字</summary>	
		[Description("项目负责人签字")]
        public string ProjectChiefSign { get; set; } // ProjectChiefSign
		/// <summary>专业总工签字</summary>	
		[Description("专业总工签字")]
        public string EngineerSign { get; set; } // EngineerSign
		/// <summary>项目阶段</summary>	
		[Description("项目阶段")]
        public string XMJD { get; set; } // XMJD
		/// <summary>参加人员</summary>	
		[Description("参加人员")]
        public string JoinUser { get; set; } // JoinUser
		/// <summary>参加人员名称</summary>	
		[Description("参加人员名称")]
        public string JoinUserName { get; set; } // JoinUserName
    }

	/// <summary>设计变更通知单</summary>	
	[Description("设计变更通知单")]
    public partial class T_EXE_ChangeAudit : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? ModifyDate { get; set; } // ModifyDate
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get; set; } // CreateUserID
		/// <summary></summary>	
		[Description("")]
        public string CreateUser { get; set; } // CreateUser
		/// <summary></summary>	
		[Description("")]
        public string ModifyUserID { get; set; } // ModifyUserID
		/// <summary></summary>	
		[Description("")]
        public string ModifyUser { get; set; } // ModifyUser
		/// <summary></summary>	
		[Description("")]
        public string OrgID { get; set; } // OrgID
		/// <summary></summary>	
		[Description("")]
        public string CompanyID { get; set; } // CompanyID
		/// <summary></summary>	
		[Description("")]
        public string FlowPhase { get; set; } // FlowPhase
		/// <summary></summary>	
		[Description("")]
        public string FlowInfo { get; set; } // FlowInfo
		/// <summary></summary>	
		[Description("")]
        public string StepName { get; set; } // StepName
		/// <summary>项目名称</summary>	
		[Description("项目名称")]
        public string ProjectInfoName { get; set; } // ProjectInfoName
		/// <summary>项目编号</summary>	
		[Description("项目编号")]
        public string ProjectInfoCode { get; set; } // ProjectInfoCode
		/// <summary>子项名称</summary>	
		[Description("子项名称")]
        public string SubProjectName { get; set; } // SubProjectName
		/// <summary>阶段</summary>	
		[Description("阶段")]
        public string PhaseCode { get; set; } // PhaseCode
		/// <summary>编号</summary>	
		[Description("编号")]
        public string SerialNumber { get; set; } // SerialNumber
		/// <summary>专业</summary>	
		[Description("专业")]
        public string MajorCode { get; set; } // MajorCode
		/// <summary>校核人</summary>	
		[Description("校核人")]
        public string Collactor { get; set; } // Collactor
		/// <summary>校核人名称</summary>	
		[Description("校核人名称")]
        public string CollactorName { get; set; } // CollactorName
		/// <summary>审定人</summary>	
		[Description("审定人")]
        public string Approver { get; set; } // Approver
		/// <summary>审定人名称</summary>	
		[Description("审定人名称")]
        public string ApproverName { get; set; } // ApproverName
		/// <summary>审核人</summary>	
		[Description("审核人")]
        public string Auditor { get; set; } // Auditor
		/// <summary>审核人名称</summary>	
		[Description("审核人名称")]
        public string AuditorName { get; set; } // AuditorName
		/// <summary>项目负责人</summary>	
		[Description("项目负责人")]
        public string ProjectManager { get; set; } // ProjectManager
		/// <summary>项目负责人名称</summary>	
		[Description("项目负责人名称")]
        public string ProjectManagerName { get; set; } // ProjectManagerName
		/// <summary>校审完成日期</summary>	
		[Description("校审完成日期")]
        public DateTime? FinishDate { get; set; } // FinishDate
		/// <summary>设计人签字</summary>	
		[Description("设计人签字")]
        public string DesignSign { get; set; } // DesignSign
		/// <summary>复核人签字</summary>	
		[Description("复核人签字")]
        public string CollactorSign { get; set; } // CollactorSign
		/// <summary>审核人签字</summary>	
		[Description("审核人签字")]
        public string AuditorSign { get; set; } // AuditorSign
		/// <summary>审定人签字</summary>	
		[Description("审定人签字")]
        public string ApproverSign { get; set; } // ApproverSign
		/// <summary>项目负责人签字</summary>	
		[Description("项目负责人签字")]
        public string ProjectManagerSign { get; set; } // ProjectManagerSign
		/// <summary>项目ID</summary>	
		[Description("项目ID")]
        public string ProjectInfoID { get; set; } // ProjectInfoID
		/// <summary>WBS节点ID</summary>	
		[Description("WBS节点ID")]
        public string WBSID { get; set; } // WBSID
		/// <summary>设计人</summary>	
		[Description("设计人")]
        public string Designer { get; set; } // Designer
		/// <summary>设计人姓名</summary>	
		[Description("设计人姓名")]
        public string DesignerName { get; set; } // DesignerName
		/// <summary>版本号</summary>	
		[Description("版本号")]
        public string VersionNumber { get; set; } // VersionNumber
		/// <summary>制图人</summary>	
		[Description("制图人")]
        public string Mapper { get; set; } // Mapper
		/// <summary>制图人名称</summary>	
		[Description("制图人名称")]
        public string MapperName { get; set; } // MapperName
		/// <summary>设总</summary>	
		[Description("设总")]
        public string DesignManager { get; set; } // DesignManager
		/// <summary>设总名称</summary>	
		[Description("设总名称")]
        public string DesignManagerName { get; set; } // DesignManagerName
		/// <summary>专业负责人</summary>	
		[Description("专业负责人")]
        public string MajorPrinciple { get; set; } // MajorPrinciple
		/// <summary>专业负责人名称</summary>	
		[Description("专业负责人名称")]
        public string MajorPrincipleName { get; set; } // MajorPrincipleName
		/// <summary>专业总工程师</summary>	
		[Description("专业总工程师")]
        public string MajorEngineer { get; set; } // MajorEngineer
		/// <summary>专业总工程师名称</summary>	
		[Description("专业总工程师名称")]
        public string MajorEngineerName { get; set; } // MajorEngineerName
		/// <summary>部门负责人</summary>	
		[Description("部门负责人")]
        public string DeptManager { get; set; } // DeptManager
		/// <summary>部门负责人名称</summary>	
		[Description("部门负责人名称")]
        public string DeptManagerName { get; set; } // DeptManagerName
		/// <summary>CAD图框签字来源（0:表单、1:流程）同时需修改枚举AuditSignCategory</summary>	
		[Description("CAD图框签字来源（0:表单、1:流程）同时需修改枚举AuditSignCategory")]
        public string AuditSignSource { get; set; } // AuditSignSource

        // Reverse navigation
		[JsonIgnore]
        public virtual ICollection<T_EXE_ChangeAudit_AdviceDetail> T_EXE_ChangeAudit_AdviceDetail { get { onT_EXE_ChangeAudit_AdviceDetailGetting(); return _T_EXE_ChangeAudit_AdviceDetail;} }
		private ICollection<T_EXE_ChangeAudit_AdviceDetail> _T_EXE_ChangeAudit_AdviceDetail;
		partial void onT_EXE_ChangeAudit_AdviceDetailGetting();


        public T_EXE_ChangeAudit()
        {
            _T_EXE_ChangeAudit_AdviceDetail = new List<T_EXE_ChangeAudit_AdviceDetail>();
        }
    }

	/// <summary>校审意见信息</summary>	
	[Description("校审意见信息")]
    public partial class T_EXE_ChangeAudit_AdviceDetail : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string T_EXE_ChangeAuditID { get; set; } // T_EXE_ChangeAuditID
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string IsReleased { get; set; } // IsReleased
		/// <summary>提出环节</summary>	
		[Description("提出环节")]
        public string Step { get; set; } // Step
		/// <summary>提出人</summary>	
		[Description("提出人")]
        public string SubmitUser { get; set; } // SubmitUser
		/// <summary>提出人名称</summary>	
		[Description("提出人名称")]
        public string SubmitUserName { get; set; } // SubmitUserName
		/// <summary>图号</summary>	
		[Description("图号")]
        public string ProductCode { get; set; } // ProductCode
		/// <summary>问题类型</summary>	
		[Description("问题类型")]
        public string MistakeType { get; set; } // MistakeType
		/// <summary>问题内容</summary>	
		[Description("问题内容")]
        public string MsitakeContent { get; set; } // MsitakeContent
		/// <summary>修改情况</summary>	
		[Description("修改情况")]
        public string ResponseContent { get; set; } // ResponseContent
		/// <summary>归属年份</summary>	
		[Description("归属年份")]
        public int? MistakeYear { get; set; } // MistakeYear
		/// <summary>归属月份</summary>	
		[Description("归属月份")]
        public int? MistakeMonth { get; set; } // MistakeMonth
		/// <summary>归属季度</summary>	
		[Description("归属季度")]
        public int? MistakeSeason { get; set; } // MistakeSeason
		/// <summary>创建人</summary>	
		[Description("创建人")]
        public string CreateUser { get; set; } // CreateUser
		/// <summary>创建人名称</summary>	
		[Description("创建人名称")]
        public string CreateUserName { get; set; } // CreateUserName
		/// <summary>创建时间</summary>	
		[Description("创建时间")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary>LPosX</summary>	
		[Description("LPosX")]
        public decimal? LPosX { get; set; } // LPosX
		/// <summary>LPosY</summary>	
		[Description("LPosY")]
        public decimal? LPosY { get; set; } // LPosY
		/// <summary>RPosX</summary>	
		[Description("RPosX")]
        public decimal? RPosX { get; set; } // RPosX
		/// <summary>RPosY</summary>	
		[Description("RPosY")]
        public decimal? RPosY { get; set; } // RPosY
		/// <summary>RevisePosX</summary>	
		[Description("RevisePosX")]
        public decimal? RevisePosX { get; set; } // RevisePosX
		/// <summary>RevisePosY</summary>	
		[Description("RevisePosY")]
        public decimal? RevisePosY { get; set; } // RevisePosY
		/// <summary>ReviseWidth</summary>	
		[Description("ReviseWidth")]
        public decimal? ReviseWidth { get; set; } // ReviseWidth
		/// <summary>ReviseHeight</summary>	
		[Description("ReviseHeight")]
        public decimal? ReviseHeight { get; set; } // ReviseHeight
		/// <summary>ReviseAudit</summary>	
		[Description("ReviseAudit")]
        public string ReviseAudit { get; set; } // ReviseAudit
		/// <summary>DrawingID</summary>	
		[Description("DrawingID")]
        public string DrawingID { get; set; } // DrawingID
		/// <summary>ReviseState</summary>	
		[Description("ReviseState")]
        public string ReviseState { get; set; } // ReviseState
		/// <summary>ReviseBasePoint</summary>	
		[Description("ReviseBasePoint")]
        public string ReviseBasePoint { get; set; } // ReviseBasePoint
		/// <summary>ReviseClass</summary>	
		[Description("ReviseClass")]
        public string ReviseClass { get; set; } // ReviseClass
		/// <summary>AuditPathID</summary>	
		[Description("AuditPathID")]
        public string AuditPathID { get; set; } // AuditPathID
		/// <summary>ReviseType</summary>	
		[Description("ReviseType")]
        public string ReviseType { get; set; } // ReviseType
		/// <summary>UpdateTime</summary>	
		[Description("UpdateTime")]
        public DateTime? UpdateTime { get; set; } // UpdateTime
		/// <summary>SubmitDept</summary>	
		[Description("SubmitDept")]
        public string SubmitDept { get; set; } // SubmitDept
		/// <summary>SubmitDeptName</summary>	
		[Description("SubmitDeptName")]
        public string SubmitDeptName { get; set; } // SubmitDeptName
		/// <summary>IsLead</summary>	
		[Description("IsLead")]
        public string IsLead { get; set; } // IsLead
		/// <summary>MarkType</summary>	
		[Description("MarkType")]
        public string MarkType { get; set; } // MarkType
		/// <summary>OpinionState</summary>	
		[Description("OpinionState")]
        public string OpinionState { get; set; } // OpinionState
		/// <summary>EntityId</summary>	
		[Description("EntityId")]
        public string EntityId { get; set; } // EntityId
		/// <summary>ComeForm</summary>	
		[Description("ComeForm")]
        public string ComeForm { get; set; } // ComeForm
		/// <summary></summary>	
		[Description("")]
        public string EntityInfoJosn { get; set; } // EntityInfoJosn
		/// <summary></summary>	
		[Description("")]
        public string DrawingName { get; set; } // DrawingName
		/// <summary></summary>	
		[Description("")]
        public string DrawingCode { get; set; } // DrawingCode
		/// <summary></summary>	
		[Description("")]
        public string OriginalPicID { get; set; } // OriginalPicID
		/// <summary></summary>	
		[Description("")]
        public string ModifiedPicID { get; set; } // ModifiedPicID

        // Foreign keys
		[JsonIgnore]
        public virtual T_EXE_ChangeAudit T_EXE_ChangeAudit { get; set; } //  T_EXE_ChangeAuditID - FK_T_EXE_ChangeAudit_AdviceDetail_T_EXE_ChangeAudit
    }

	/// <summary>设计变更申请</summary>	
	[Description("设计变更申请")]
    public partial class T_EXE_DesignChangeApply : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? ModifyDate { get; set; } // ModifyDate
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get; set; } // CreateUserID
		/// <summary></summary>	
		[Description("")]
        public string CreateUser { get; set; } // CreateUser
		/// <summary></summary>	
		[Description("")]
        public string ModifyUserID { get; set; } // ModifyUserID
		/// <summary></summary>	
		[Description("")]
        public string ModifyUser { get; set; } // ModifyUser
		/// <summary></summary>	
		[Description("")]
        public string OrgID { get; set; } // OrgID
		/// <summary></summary>	
		[Description("")]
        public string CompanyID { get; set; } // CompanyID
		/// <summary></summary>	
		[Description("")]
        public string FlowPhase { get; set; } // FlowPhase
		/// <summary></summary>	
		[Description("")]
        public string FlowInfo { get; set; } // FlowInfo
		/// <summary></summary>	
		[Description("")]
        public string StepName { get; set; } // StepName
		/// <summary>项目名称</summary>	
		[Description("项目名称")]
        public string ProjectInfo { get; set; } // ProjectInfo
		/// <summary>项目名称名称</summary>	
		[Description("项目名称名称")]
        public string ProjectInfoName { get; set; } // ProjectInfoName
		/// <summary>项目编号</summary>	
		[Description("项目编号")]
        public string ProjectCode { get; set; } // ProjectCode
		/// <summary>项目负责人</summary>	
		[Description("项目负责人")]
        public string ProjectManager { get; set; } // ProjectManager
		/// <summary>项目负责人名称</summary>	
		[Description("项目负责人名称")]
        public string ProjectManagerName { get; set; } // ProjectManagerName
		/// <summary>设计阶段</summary>	
		[Description("设计阶段")]
        public string Phase { get; set; } // Phase
		/// <summary>专业</summary>	
		[Description("专业")]
        public string MajorValue { get; set; } // MajorValue
		/// <summary>专业Name</summary>	
		[Description("专业Name")]
        public string MajorName { get; set; } // MajorName
		/// <summary>专业负责人</summary>	
		[Description("专业负责人")]
        public string MajorPrinciple { get; set; } // MajorPrinciple
		/// <summary>专业负责人名称</summary>	
		[Description("专业负责人名称")]
        public string MajorPrincipleName { get; set; } // MajorPrincipleName
		/// <summary>归属部门</summary>	
		[Description("归属部门")]
        public string BelongDept { get; set; } // BelongDept
		/// <summary>归属部门名称</summary>	
		[Description("归属部门名称")]
        public string BelongDeptName { get; set; } // BelongDeptName
		/// <summary>归属日期</summary>	
		[Description("归属日期")]
        public DateTime? BelongDate { get; set; } // BelongDate
		/// <summary>ProjectInfoID</summary>	
		[Description("ProjectInfoID")]
        public string ProjectInfoID { get; set; } // ProjectInfoID
		/// <summary>卷册列表</summary>	
		[Description("卷册列表")]
        public string TaskWork { get; set; } // TaskWork
		/// <summary>备注</summary>	
		[Description("备注")]
        public string Remark { get; set; } // Remark
		/// <summary>项目负责人审批</summary>	
		[Description("项目负责人审批")]
        public string ApprovePM { get; set; } // ApprovePM
		/// <summary>公司领导审批</summary>	
		[Description("公司领导审批")]
        public string ApproveLeader { get; set; } // ApproveLeader
		/// <summary>卷册ID</summary>	
		[Description("卷册ID")]
        public string TaskWorkID { get; set; } // TaskWorkID
		/// <summary>卷册名称</summary>	
		[Description("卷册名称")]
        public string TaskWorkName { get; set; } // TaskWorkName
		/// <summary>卷册编号</summary>	
		[Description("卷册编号")]
        public string TaskWorkCode { get; set; } // TaskWorkCode

        // Reverse navigation
		[JsonIgnore]
        public virtual ICollection<T_EXE_DesignChangeApply_TaskWork> T_EXE_DesignChangeApply_TaskWork { get { onT_EXE_DesignChangeApply_TaskWorkGetting(); return _T_EXE_DesignChangeApply_TaskWork;} }
		private ICollection<T_EXE_DesignChangeApply_TaskWork> _T_EXE_DesignChangeApply_TaskWork;
		partial void onT_EXE_DesignChangeApply_TaskWorkGetting();


        public T_EXE_DesignChangeApply()
        {
            _T_EXE_DesignChangeApply_TaskWork = new List<T_EXE_DesignChangeApply_TaskWork>();
        }
    }

	/// <summary>卷册列表</summary>	
	[Description("卷册列表")]
    public partial class T_EXE_DesignChangeApply_TaskWork : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string T_EXE_DesignChangeApplyID { get; set; } // T_EXE_DesignChangeApplyID
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string IsReleased { get; set; } // IsReleased
		/// <summary>卷册名称</summary>	
		[Description("卷册名称")]
        public string Name { get; set; } // Name
		/// <summary>卷册编号</summary>	
		[Description("卷册编号")]
        public string Code { get; set; } // Code
		/// <summary>专业</summary>	
		[Description("专业")]
        public string Major { get; set; } // Major
		/// <summary>阶段</summary>	
		[Description("阶段")]
        public string Phase { get; set; } // Phase
		/// <summary>定额工时</summary>	
		[Description("定额工时")]
        public decimal? Workload { get; set; } // Workload
		/// <summary>计划完成日期</summary>	
		[Description("计划完成日期")]
        public DateTime? PlanEndDate { get; set; } // PlanEndDate
		/// <summary>卷册ID</summary>	
		[Description("卷册ID")]
        public string TaskWorkID { get; set; } // TaskWorkID
		/// <summary>卷名</summary>	
		[Description("卷名")]
        public string DossierName { get; set; } // DossierName
		/// <summary>卷号</summary>	
		[Description("卷号")]
        public string DossierCode { get; set; } // DossierCode

        // Foreign keys
		[JsonIgnore]
        public virtual T_EXE_DesignChangeApply T_EXE_DesignChangeApply { get; set; } //  T_EXE_DesignChangeApplyID - FK_T_EXE_DesignChangeApply_TaskWork_T_EXE_DesignChangeApply
    }

	/// <summary>设计变更通知单</summary>	
	[Description("设计变更通知单")]
    public partial class T_EXE_DesignChangeNotice : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? ModifyDate { get; set; } // ModifyDate
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get; set; } // CreateUserID
		/// <summary></summary>	
		[Description("")]
        public string CreateUser { get; set; } // CreateUser
		/// <summary></summary>	
		[Description("")]
        public string ModifyUserID { get; set; } // ModifyUserID
		/// <summary></summary>	
		[Description("")]
        public string ModifyUser { get; set; } // ModifyUser
		/// <summary></summary>	
		[Description("")]
        public string OrgID { get; set; } // OrgID
		/// <summary></summary>	
		[Description("")]
        public string CompanyID { get; set; } // CompanyID
		/// <summary></summary>	
		[Description("")]
        public string FlowPhase { get; set; } // FlowPhase
		/// <summary></summary>	
		[Description("")]
        public string FlowInfo { get; set; } // FlowInfo
		/// <summary></summary>	
		[Description("")]
        public string StepName { get; set; } // StepName
		/// <summary>项目业主</summary>	
		[Description("项目业主")]
        public string ProjectOwner { get; set; } // ProjectOwner
		/// <summary>编号</summary>	
		[Description("编号")]
        public string SerialNumber { get; set; } // SerialNumber
		/// <summary>项目名称</summary>	
		[Description("项目名称")]
        public string ProjectInfo { get; set; } // ProjectInfo
		/// <summary>项目名称名称</summary>	
		[Description("项目名称名称")]
        public string ProjectInfoName { get; set; } // ProjectInfoName
		/// <summary>原设计图名称</summary>	
		[Description("原设计图名称")]
        public string OriginalDesignChart { get; set; } // OriginalDesignChart
		/// <summary>子项</summary>	
		[Description("子项")]
        public string SubProject { get; set; } // SubProject
		/// <summary>子项名称</summary>	
		[Description("子项名称")]
        public string SubProjectName { get; set; } // SubProjectName
		/// <summary>专业</summary>	
		[Description("专业")]
        public string Major { get; set; } // Major
		/// <summary>专业名称</summary>	
		[Description("专业名称")]
        public string MajorName { get; set; } // MajorName
		/// <summary>图号</summary>	
		[Description("图号")]
        public string FigureNo { get; set; } // FigureNo
		/// <summary>变更主题</summary>	
		[Description("变更主题")]
        public string ChangeSubject { get; set; } // ChangeSubject
		/// <summary>变更原因</summary>	
		[Description("变更原因")]
        public string ChangeReason { get; set; } // ChangeReason
		/// <summary>变更依据</summary>	
		[Description("变更依据")]
        public string ChangeBasis { get; set; } // ChangeBasis
		/// <summary>变更内容摘要</summary>	
		[Description("变更内容摘要")]
        public string ChangeContentSummary { get; set; } // ChangeContentSummary
		/// <summary>附件附图</summary>	
		[Description("附件附图")]
        public string Attachments { get; set; } // Attachments
		/// <summary>专业负责人意见</summary>	
		[Description("专业负责人意见")]
        public string Managers { get; set; } // Managers
		/// <summary>项目负责人意见</summary>	
		[Description("项目负责人意见")]
        public string TechnicalDirectorComments { get; set; } // TechnicalDirectorComments
		/// <summary>技术质量部意见</summary>	
		[Description("技术质量部意见")]
        public string TechnicalDirectorsComments { get; set; } // TechnicalDirectorsComments
		/// <summary>项目ID</summary>	
		[Description("项目ID")]
        public string ProjectInfoID { get; set; } // ProjectInfoID
		/// <summary>版本号</summary>	
		[Description("版本号")]
        public string VersionNumber { get; set; } // VersionNumber
    }

	/// <summary>互提资料单</summary>	
	[Description("互提资料单")]
    public partial class T_EXE_MajorPutInfo : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? ModifyDate { get; set; } // ModifyDate
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get; set; } // CreateUserID
		/// <summary></summary>	
		[Description("")]
        public string CreateUser { get; set; } // CreateUser
		/// <summary></summary>	
		[Description("")]
        public string ModifyUserID { get; set; } // ModifyUserID
		/// <summary></summary>	
		[Description("")]
        public string ModifyUser { get; set; } // ModifyUser
		/// <summary></summary>	
		[Description("")]
        public string OrgID { get; set; } // OrgID
		/// <summary></summary>	
		[Description("")]
        public string CompanyID { get; set; } // CompanyID
		/// <summary></summary>	
		[Description("")]
        public string FlowPhase { get; set; } // FlowPhase
		/// <summary></summary>	
		[Description("")]
        public string FlowInfo { get; set; } // FlowInfo
		/// <summary></summary>	
		[Description("")]
        public string StepName { get; set; } // StepName
		/// <summary>项目ID</summary>	
		[Description("项目ID")]
        public string ProjectInfoID { get; set; } // ProjectInfoID
		/// <summary>选择的提资包节点ID</summary>	
		[Description("选择的提资包节点ID")]
        public string WBSID { get; set; } // WBSID
		/// <summary>提资计划ID</summary>	
		[Description("提资计划ID")]
        public string CoopPlanID { get; set; } // CoopPlanID
		/// <summary>提资状态</summary>	
		[Description("提资状态")]
        public string State { get; set; } // State
		/// <summary>项目名称</summary>	
		[Description("项目名称")]
        public string ProjectName { get; set; } // ProjectName
		/// <summary>项目编号</summary>	
		[Description("项目编号")]
        public string ProjectCode { get; set; } // ProjectCode
		/// <summary>提出专业</summary>	
		[Description("提出专业")]
        public string OutMajorValue { get; set; } // OutMajorValue
		/// <summary>接受专业</summary>	
		[Description("接受专业")]
        public string InMajorValue { get; set; } // InMajorValue
		/// <summary>资料名称</summary>	
		[Description("资料名称")]
        public string CooperationContent { get; set; } // CooperationContent
		/// <summary>提出人</summary>	
		[Description("提出人")]
        public string Register { get; set; } // Register
		/// <summary>提出人名称</summary>	
		[Description("提出人名称")]
        public string RegisterName { get; set; } // RegisterName
		/// <summary>委托任务书内容</summary>	
		[Description("委托任务书内容")]
        public string TaskContent { get; set; } // TaskContent
		/// <summary>附图（张）</summary>	
		[Description("附图（张）")]
        public string PictureNum { get; set; } // PictureNum
		/// <summary>附件（份）</summary>	
		[Description("附件（份）")]
        public string AnnexNum { get; set; } // AnnexNum
		/// <summary>提出时间</summary>	
		[Description("提出时间")]
        public DateTime? DelegateDate { get; set; } // DelegateDate
		/// <summary>接收时间</summary>	
		[Description("接收时间")]
        public DateTime? RecieveDate { get; set; } // RecieveDate
		/// <summary>附件</summary>	
		[Description("附件")]
        public string Annex { get; set; } // Annex
		/// <summary>子项名称</summary>	
		[Description("子项名称")]
        public string SubProjectName { get; set; } // SubProjectName
		/// <summary>阶段</summary>	
		[Description("阶段")]
        public string Phase { get; set; } // Phase
		/// <summary>接收人</summary>	
		[Description("接收人")]
        public string Receiver { get; set; } // Receiver
		/// <summary>接收人名称</summary>	
		[Description("接收人名称")]
        public string ReceiverName { get; set; } // ReceiverName
		/// <summary>校对人</summary>	
		[Description("校对人")]
        public string Collactor { get; set; } // Collactor
		/// <summary>审核人</summary>	
		[Description("审核人")]
        public string Auditor { get; set; } // Auditor
		/// <summary>审定人</summary>	
		[Description("审定人")]
        public string Checker { get; set; } // Checker
		/// <summary>专业负责人</summary>	
		[Description("专业负责人")]
        public string MajorPrinciple { get; set; } // MajorPrinciple
		/// <summary>版本号</summary>	
		[Description("版本号")]
        public string VersionNumber { get; set; } // VersionNumber
		/// <summary>编号</summary>	
		[Description("编号")]
        public string SerialNumber { get; set; } // SerialNumber
		/// <summary>PassAuditState</summary>	
		[Description("PassAuditState")]
        public string PassAuditState { get; set; } // PassAuditState
		/// <summary>提资计划ID名称</summary>	
		[Description("提资计划ID名称")]
        public string CoopPlanIDName { get; set; } // CoopPlanIDName
		/// <summary>提资包上层节点ID</summary>	
		[Description("提资包上层节点ID")]
        public string RelateWBSID { get; set; } // RelateWBSID

        // Reverse navigation
		[JsonIgnore]
        public virtual ICollection<T_EXE_MajorPutInfo_AdviceDetail> T_EXE_MajorPutInfo_AdviceDetail { get { onT_EXE_MajorPutInfo_AdviceDetailGetting(); return _T_EXE_MajorPutInfo_AdviceDetail;} }
		private ICollection<T_EXE_MajorPutInfo_AdviceDetail> _T_EXE_MajorPutInfo_AdviceDetail;
		partial void onT_EXE_MajorPutInfo_AdviceDetailGetting();

		[JsonIgnore]
        public virtual ICollection<T_EXE_MajorPutInfo_FetchDrawingInfo> T_EXE_MajorPutInfo_FetchDrawingInfo { get { onT_EXE_MajorPutInfo_FetchDrawingInfoGetting(); return _T_EXE_MajorPutInfo_FetchDrawingInfo;} }
		private ICollection<T_EXE_MajorPutInfo_FetchDrawingInfo> _T_EXE_MajorPutInfo_FetchDrawingInfo;
		partial void onT_EXE_MajorPutInfo_FetchDrawingInfoGetting();


        public T_EXE_MajorPutInfo()
        {
            _T_EXE_MajorPutInfo_AdviceDetail = new List<T_EXE_MajorPutInfo_AdviceDetail>();
            _T_EXE_MajorPutInfo_FetchDrawingInfo = new List<T_EXE_MajorPutInfo_FetchDrawingInfo>();
        }
    }

	/// <summary>校审意见信息</summary>	
	[Description("校审意见信息")]
    public partial class T_EXE_MajorPutInfo_AdviceDetail : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string T_EXE_MajorPutInfoID { get; set; } // T_EXE_MajorPutInfoID
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string IsReleased { get; set; } // IsReleased
		/// <summary>提出人</summary>	
		[Description("提出人")]
        public string SubmitUser { get; set; } // SubmitUser
		/// <summary>提出人名称</summary>	
		[Description("提出人名称")]
        public string SubmitUserName { get; set; } // SubmitUserName
		/// <summary>文件编号</summary>	
		[Description("文件编号")]
        public string ProductCode { get; set; } // ProductCode
		/// <summary>图框图名</summary>	
		[Description("图框图名")]
        public string DrawingName { get; set; } // DrawingName
		/// <summary>图框图号</summary>	
		[Description("图框图号")]
        public string DrawingCode { get; set; } // DrawingCode
		/// <summary>问题类型</summary>	
		[Description("问题类型")]
        public string MistakeType { get; set; } // MistakeType
		/// <summary>问题内容</summary>	
		[Description("问题内容")]
        public string MsitakeContent { get; set; } // MsitakeContent
		/// <summary>修改情况</summary>	
		[Description("修改情况")]
        public string ResponseContent { get; set; } // ResponseContent
		/// <summary>归属年份</summary>	
		[Description("归属年份")]
        public int? MistakeYear { get; set; } // MistakeYear
		/// <summary>归属月份</summary>	
		[Description("归属月份")]
        public int? MistakeMonth { get; set; } // MistakeMonth
		/// <summary>归属季度</summary>	
		[Description("归属季度")]
        public int? MistakeSeason { get; set; } // MistakeSeason
		/// <summary>创建人</summary>	
		[Description("创建人")]
        public string CreateUser { get; set; } // CreateUser
		/// <summary>创建人名称</summary>	
		[Description("创建人名称")]
        public string CreateUserName { get; set; } // CreateUserName
		/// <summary>创建时间</summary>	
		[Description("创建时间")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary>LPosX</summary>	
		[Description("LPosX")]
        public decimal? LPosX { get; set; } // LPosX
		/// <summary>LPosY</summary>	
		[Description("LPosY")]
        public decimal? LPosY { get; set; } // LPosY
		/// <summary>RPosX</summary>	
		[Description("RPosX")]
        public decimal? RPosX { get; set; } // RPosX
		/// <summary>RPosY</summary>	
		[Description("RPosY")]
        public decimal? RPosY { get; set; } // RPosY
		/// <summary>RevisePosX</summary>	
		[Description("RevisePosX")]
        public decimal? RevisePosX { get; set; } // RevisePosX
		/// <summary>RevisePosY</summary>	
		[Description("RevisePosY")]
        public decimal? RevisePosY { get; set; } // RevisePosY
		/// <summary>ReviseWidth</summary>	
		[Description("ReviseWidth")]
        public decimal? ReviseWidth { get; set; } // ReviseWidth
		/// <summary>ReviseHeight</summary>	
		[Description("ReviseHeight")]
        public decimal? ReviseHeight { get; set; } // ReviseHeight
		/// <summary>ReviseAudit</summary>	
		[Description("ReviseAudit")]
        public string ReviseAudit { get; set; } // ReviseAudit
		/// <summary>DrawingID</summary>	
		[Description("DrawingID")]
        public string DrawingID { get; set; } // DrawingID
		/// <summary>ReviseState</summary>	
		[Description("ReviseState")]
        public string ReviseState { get; set; } // ReviseState
		/// <summary>ReviseBasePoint</summary>	
		[Description("ReviseBasePoint")]
        public string ReviseBasePoint { get; set; } // ReviseBasePoint
		/// <summary>ReviseClass</summary>	
		[Description("ReviseClass")]
        public string ReviseClass { get; set; } // ReviseClass
		/// <summary>AuditPathID</summary>	
		[Description("AuditPathID")]
        public string AuditPathID { get; set; } // AuditPathID
		/// <summary>ReviseType</summary>	
		[Description("ReviseType")]
        public string ReviseType { get; set; } // ReviseType
		/// <summary>UpdateTime</summary>	
		[Description("UpdateTime")]
        public DateTime? UpdateTime { get; set; } // UpdateTime
		/// <summary>SubmitDept</summary>	
		[Description("SubmitDept")]
        public string SubmitDept { get; set; } // SubmitDept
		/// <summary>SubmitDeptName</summary>	
		[Description("SubmitDeptName")]
        public string SubmitDeptName { get; set; } // SubmitDeptName
		/// <summary>IsLead</summary>	
		[Description("IsLead")]
        public string IsLead { get; set; } // IsLead
		/// <summary>MarkType</summary>	
		[Description("MarkType")]
        public string MarkType { get; set; } // MarkType
		/// <summary>OpinionState</summary>	
		[Description("OpinionState")]
        public string OpinionState { get; set; } // OpinionState
		/// <summary>EntityId</summary>	
		[Description("EntityId")]
        public string EntityId { get; set; } // EntityId
		/// <summary>ComeForm</summary>	
		[Description("ComeForm")]
        public string ComeForm { get; set; } // ComeForm
		/// <summary>EntityInfoJosn</summary>	
		[Description("EntityInfoJosn")]
        public string EntityInfoJosn { get; set; } // EntityInfoJosn
		/// <summary></summary>	
		[Description("")]
        public string OriginalPicID { get; set; } // OriginalPicID
		/// <summary></summary>	
		[Description("")]
        public string ModifiedPicID { get; set; } // ModifiedPicID

        // Foreign keys
		[JsonIgnore]
        public virtual T_EXE_MajorPutInfo T_EXE_MajorPutInfo { get; set; } //  T_EXE_MajorPutInfoID - FK_T_EXE_MajorPutInfo_AdviceDetail_T_EXE_MajorPutInfo
    }

	/// <summary>提资文件</summary>	
	[Description("提资文件")]
    public partial class T_EXE_MajorPutInfo_FetchDrawingInfo : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string T_EXE_MajorPutInfoID { get; set; } // T_EXE_MajorPutInfoID
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string IsReleased { get; set; } // IsReleased
		/// <summary>文件名称</summary>	
		[Description("文件名称")]
        public string FileName { get; set; } // FileName
		/// <summary>文件编号</summary>	
		[Description("文件编号")]
        public string Code { get; set; } // Code
		/// <summary>文件类型</summary>	
		[Description("文件类型")]
        public string Type { get; set; } // Type
		/// <summary>附件</summary>	
		[Description("附件")]
        public string MainFile { get; set; } // MainFile
		/// <summary>附件名称</summary>	
		[Description("附件名称")]
        public string MainFileName { get; set; } // MainFileName
		/// <summary>文件来源</summary>	
		[Description("文件来源")]
        public string Source { get; set; } // Source
		/// <summary>ShareFileID</summary>	
		[Description("ShareFileID")]
        public string ShareFileID { get; set; } // ShareFileID
		/// <summary>SourceType</summary>	
		[Description("SourceType")]
        public string SourceType { get; set; } // SourceType
		/// <summary>DrawingSource</summary>	
		[Description("DrawingSource")]
        public string DrawingSource { get; set; } // DrawingSource
		/// <summary>DrawingID</summary>	
		[Description("DrawingID")]
        public string DrawingID { get; set; } // DrawingID

        // Foreign keys
		[JsonIgnore]
        public virtual T_EXE_MajorPutInfo T_EXE_MajorPutInfo { get; set; } //  T_EXE_MajorPutInfoID - FK_T_EXE_MajorPutInfo_FetchDrawingInfo_T_EXE_MajorPutInfo
    }

	/// <summary>管理工时结算单</summary>	
	[Description("管理工时结算单")]
    public partial class T_EXE_ManageWorkloadSettlement : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? ModifyDate { get; set; } // ModifyDate
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get; set; } // CreateUserID
		/// <summary></summary>	
		[Description("")]
        public string CreateUser { get; set; } // CreateUser
		/// <summary></summary>	
		[Description("")]
        public string ModifyUserID { get; set; } // ModifyUserID
		/// <summary></summary>	
		[Description("")]
        public string ModifyUser { get; set; } // ModifyUser
		/// <summary></summary>	
		[Description("")]
        public string OrgID { get; set; } // OrgID
		/// <summary></summary>	
		[Description("")]
        public string CompanyID { get; set; } // CompanyID
		/// <summary></summary>	
		[Description("")]
        public string FlowPhase { get; set; } // FlowPhase
		/// <summary></summary>	
		[Description("")]
        public string FlowInfo { get; set; } // FlowInfo
		/// <summary></summary>	
		[Description("")]
        public string StepName { get; set; } // StepName
		/// <summary>项目名称</summary>	
		[Description("项目名称")]
        public string ProjectInfo { get; set; } // ProjectInfo
		/// <summary>项目名称名称</summary>	
		[Description("项目名称名称")]
        public string ProjectInfoName { get; set; } // ProjectInfoName
		/// <summary>项目编号</summary>	
		[Description("项目编号")]
        public string ProjectCode { get; set; } // ProjectCode
		/// <summary>项目负责人</summary>	
		[Description("项目负责人")]
        public string ProjectManager { get; set; } // ProjectManager
		/// <summary>项目负责人名称</summary>	
		[Description("项目负责人名称")]
        public string ProjectManagerName { get; set; } // ProjectManagerName
		/// <summary>设计阶段</summary>	
		[Description("设计阶段")]
        public string Phase { get; set; } // Phase
		/// <summary>归属部门</summary>	
		[Description("归属部门")]
        public string BelongDept { get; set; } // BelongDept
		/// <summary>归属部门名称</summary>	
		[Description("归属部门名称")]
        public string BelongDeptName { get; set; } // BelongDeptName
		/// <summary>归属日期</summary>	
		[Description("归属日期")]
        public DateTime? BelongDate { get; set; } // BelongDate
		/// <summary>备注</summary>	
		[Description("备注")]
        public string Remark { get; set; } // Remark
		/// <summary>项目负责人审批</summary>	
		[Description("项目负责人审批")]
        public string ProjectManagerApprove { get; set; } // ProjectManagerApprove
		/// <summary>本次结算汇总</summary>	
		[Description("本次结算汇总")]
        public decimal? SummaySettlement { get; set; } // SummaySettlement
		/// <summary>管理工时</summary>	
		[Description("管理工时")]
        public string ManageWorkloadList { get; set; } // ManageWorkloadList

        // Reverse navigation
		[JsonIgnore]
        public virtual ICollection<T_EXE_ManageWorkloadSettlement_ManageWorkloadList> T_EXE_ManageWorkloadSettlement_ManageWorkloadList { get { onT_EXE_ManageWorkloadSettlement_ManageWorkloadListGetting(); return _T_EXE_ManageWorkloadSettlement_ManageWorkloadList;} }
		private ICollection<T_EXE_ManageWorkloadSettlement_ManageWorkloadList> _T_EXE_ManageWorkloadSettlement_ManageWorkloadList;
		partial void onT_EXE_ManageWorkloadSettlement_ManageWorkloadListGetting();


        public T_EXE_ManageWorkloadSettlement()
        {
            _T_EXE_ManageWorkloadSettlement_ManageWorkloadList = new List<T_EXE_ManageWorkloadSettlement_ManageWorkloadList>();
        }
    }

	/// <summary>管理工时</summary>	
	[Description("管理工时")]
    public partial class T_EXE_ManageWorkloadSettlement_ManageWorkloadList : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string T_EXE_ManageWorkloadSettlementID { get; set; } // T_EXE_ManageWorkloadSettlementID
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string IsReleased { get; set; } // IsReleased
		/// <summary>名称</summary>	
		[Description("名称")]
        public string Name { get; set; } // Name
		/// <summary>编号</summary>	
		[Description("编号")]
        public string Code { get; set; } // Code
		/// <summary>下达工时.工时</summary>	
		[Description("下达工时.工时")]
        public decimal? Workload { get; set; } // Workload
		/// <summary>下达工时.已结算</summary>	
		[Description("下达工时.已结算")]
        public decimal? SummarySettlement { get; set; } // SummarySettlement
		/// <summary>下达工时.本次结算</summary>	
		[Description("下达工时.本次结算")]
        public decimal? Settlement { get; set; } // Settlement
		/// <summary>结算详情.本次结算</summary>	
		[Description("结算详情.本次结算")]
        public string DetailText { get; set; } // DetailText
		/// <summary>明细Json</summary>	
		[Description("明细Json")]
        public string DetailJson { get; set; } // DetailJson
		/// <summary>CBSID</summary>	
		[Description("CBSID")]
        public string CBSID { get; set; } // CBSID
		/// <summary>结算详情.已结算</summary>	
		[Description("结算详情.已结算")]
        public string DetailTextHistory { get; set; } // DetailTextHistory

        // Foreign keys
		[JsonIgnore]
        public virtual T_EXE_ManageWorkloadSettlement T_EXE_ManageWorkloadSettlement { get; set; } //  T_EXE_ManageWorkloadSettlementID - FK_T_EXE_ManageWorkloadSettlement_ManageWorkloadList_T_EXE_ManageWorkloadSettlement
    }

	/// <summary>图纸会签</summary>	
	[Description("图纸会签")]
    public partial class T_EXE_MettingSign : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? ModifyDate { get; set; } // ModifyDate
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get; set; } // CreateUserID
		/// <summary></summary>	
		[Description("")]
        public string CreateUser { get; set; } // CreateUser
		/// <summary></summary>	
		[Description("")]
        public string ModifyUserID { get; set; } // ModifyUserID
		/// <summary></summary>	
		[Description("")]
        public string ModifyUser { get; set; } // ModifyUser
		/// <summary></summary>	
		[Description("")]
        public string OrgID { get; set; } // OrgID
		/// <summary></summary>	
		[Description("")]
        public string CompanyID { get; set; } // CompanyID
		/// <summary></summary>	
		[Description("")]
        public string FlowPhase { get; set; } // FlowPhase
		/// <summary></summary>	
		[Description("")]
        public string FlowInfo { get; set; } // FlowInfo
		/// <summary></summary>	
		[Description("")]
        public string StepName { get; set; } // StepName
		/// <summary>项目名称</summary>	
		[Description("项目名称")]
        public string ProjectInfoName { get; set; } // ProjectInfoName
		/// <summary>项目编号</summary>	
		[Description("项目编号")]
        public string ProjectInfoCode { get; set; } // ProjectInfoCode
		/// <summary>阶段</summary>	
		[Description("阶段")]
        public string Phase { get; set; } // Phase
		/// <summary>提交人</summary>	
		[Description("提交人")]
        public string SubmitUser { get; set; } // SubmitUser
		/// <summary>提交人名称</summary>	
		[Description("提交人名称")]
        public string SubmitUserName { get; set; } // SubmitUserName
		/// <summary>专业方向</summary>	
		[Description("专业方向")]
        public string Major { get; set; } // Major
		/// <summary>专业方向名称</summary>	
		[Description("专业方向名称")]
        public string MajorName { get; set; } // MajorName
		/// <summary>接收人</summary>	
		[Description("接收人")]
        public string ReceiveUser { get; set; } // ReceiveUser
		/// <summary>接收人名称</summary>	
		[Description("接收人名称")]
        public string ReceiveUserName { get; set; } // ReceiveUserName
		/// <summary>复核签字</summary>	
		[Description("复核签字")]
        public string FH { get; set; } // FH
		/// <summary>审核签字</summary>	
		[Description("审核签字")]
        public string SH { get; set; } // SH
		/// <summary>专业审定</summary>	
		[Description("专业审定")]
        public string ZYSD { get; set; } // ZYSD
		/// <summary>ProjectInfoID</summary>	
		[Description("ProjectInfoID")]
        public string ProjectInfoID { get; set; } // ProjectInfoID
		/// <summary>复核人</summary>	
		[Description("复核人")]
        public string Collactor { get; set; } // Collactor
		/// <summary>复核人名称</summary>	
		[Description("复核人名称")]
        public string CollactorName { get; set; } // CollactorName
		/// <summary>审核人</summary>	
		[Description("审核人")]
        public string Auditor { get; set; } // Auditor
		/// <summary>审核人名称</summary>	
		[Description("审核人名称")]
        public string AuditorName { get; set; } // AuditorName
		/// <summary>专业审定人</summary>	
		[Description("专业审定人")]
        public string MajorApprover { get; set; } // MajorApprover
		/// <summary>专业审定人名称</summary>	
		[Description("专业审定人名称")]
        public string MajorApproverName { get; set; } // MajorApproverName
		/// <summary>项目组成员</summary>	
		[Description("项目组成员")]
        public string ProjectUserlist { get; set; } // ProjectUserlist
		/// <summary>项目组成员名称</summary>	
		[Description("项目组成员名称")]
        public string ProjectUserlistName { get; set; } // ProjectUserlistName
		/// <summary>项目审定人</summary>	
		[Description("项目审定人")]
        public string Approver { get; set; } // Approver
		/// <summary>项目审定人名称</summary>	
		[Description("项目审定人名称")]
        public string ApproverName { get; set; } // ApproverName
		/// <summary>编号</summary>	
		[Description("编号")]
        public string SerialNumber { get; set; } // SerialNumber
		/// <summary>子项名称</summary>	
		[Description("子项名称")]
        public string SubProject { get; set; } // SubProject
		/// <summary>子项名称名称</summary>	
		[Description("子项名称名称")]
        public string SubProjectName { get; set; } // SubProjectName
		/// <summary>所属部门</summary>	
		[Description("所属部门")]
        public string Dept { get; set; } // Dept
		/// <summary>所属部门名称</summary>	
		[Description("所属部门名称")]
        public string DeptName { get; set; } // DeptName
		/// <summary>会签专业选择</summary>	
		[Description("会签专业选择")]
        public string SignMajor { get; set; } // SignMajor
		/// <summary>SignName</summary>	
		[Description("SignName")]
        public string SignName { get; set; } // SignName
		/// <summary>SignID</summary>	
		[Description("SignID")]
        public string SignID { get; set; } // SignID
		/// <summary>MettingUser名称</summary>	
		[Description("MettingUser名称")]
        public string MettingUserName { get; set; } // MettingUserName
		/// <summary>MettingUser</summary>	
		[Description("MettingUser")]
        public string MettingUser { get; set; } // MettingUser
		/// <summary>WBSID</summary>	
		[Description("WBSID")]
        public string WBSID { get; set; } // WBSID
		/// <summary>版本号</summary>	
		[Description("版本号")]
        public string VersionNumber { get; set; } // VersionNumber

        // Reverse navigation
		[JsonIgnore]
        public virtual ICollection<T_EXE_MettingSign_AdviceDetail> T_EXE_MettingSign_AdviceDetail { get { onT_EXE_MettingSign_AdviceDetailGetting(); return _T_EXE_MettingSign_AdviceDetail;} }
		private ICollection<T_EXE_MettingSign_AdviceDetail> _T_EXE_MettingSign_AdviceDetail;
		partial void onT_EXE_MettingSign_AdviceDetailGetting();

		[JsonIgnore]
        public virtual ICollection<T_EXE_MettingSign_ProjectGroupMembers> T_EXE_MettingSign_ProjectGroupMembers { get { onT_EXE_MettingSign_ProjectGroupMembersGetting(); return _T_EXE_MettingSign_ProjectGroupMembers;} }
		private ICollection<T_EXE_MettingSign_ProjectGroupMembers> _T_EXE_MettingSign_ProjectGroupMembers;
		partial void onT_EXE_MettingSign_ProjectGroupMembersGetting();

		[JsonIgnore]
        public virtual ICollection<T_EXE_MettingSign_ResultList> T_EXE_MettingSign_ResultList { get { onT_EXE_MettingSign_ResultListGetting(); return _T_EXE_MettingSign_ResultList;} }
		private ICollection<T_EXE_MettingSign_ResultList> _T_EXE_MettingSign_ResultList;
		partial void onT_EXE_MettingSign_ResultListGetting();


        public T_EXE_MettingSign()
        {
            _T_EXE_MettingSign_AdviceDetail = new List<T_EXE_MettingSign_AdviceDetail>();
            _T_EXE_MettingSign_ProjectGroupMembers = new List<T_EXE_MettingSign_ProjectGroupMembers>();
            _T_EXE_MettingSign_ResultList = new List<T_EXE_MettingSign_ResultList>();
        }
    }

	/// <summary>意见信息</summary>	
	[Description("意见信息")]
    public partial class T_EXE_MettingSign_AdviceDetail : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string T_EXE_MettingSignID { get; set; } // T_EXE_MettingSignID
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string IsReleased { get; set; } // IsReleased
		/// <summary>提出人</summary>	
		[Description("提出人")]
        public string SubmitUser { get; set; } // SubmitUser
		/// <summary>提出人名称</summary>	
		[Description("提出人名称")]
        public string SubmitUserName { get; set; } // SubmitUserName
		/// <summary>文件编号</summary>	
		[Description("文件编号")]
        public string ProductCode { get; set; } // ProductCode
		/// <summary>图框图名</summary>	
		[Description("图框图名")]
        public string DrawingName { get; set; } // DrawingName
		/// <summary>图框图号</summary>	
		[Description("图框图号")]
        public string DrawingCode { get; set; } // DrawingCode
		/// <summary>问题类型</summary>	
		[Description("问题类型")]
        public string MistakeType { get; set; } // MistakeType
		/// <summary>问题内容</summary>	
		[Description("问题内容")]
        public string MsitakeContent { get; set; } // MsitakeContent
		/// <summary>修改情况</summary>	
		[Description("修改情况")]
        public string ResponseContent { get; set; } // ResponseContent
		/// <summary>归属年份</summary>	
		[Description("归属年份")]
        public int? MistakeYear { get; set; } // MistakeYear
		/// <summary>归属月份</summary>	
		[Description("归属月份")]
        public int? MistakeMonth { get; set; } // MistakeMonth
		/// <summary>归属季度</summary>	
		[Description("归属季度")]
        public int? MistakeSeason { get; set; } // MistakeSeason
		/// <summary>创建人</summary>	
		[Description("创建人")]
        public string CreateUser { get; set; } // CreateUser
		/// <summary>创建人名称</summary>	
		[Description("创建人名称")]
        public string CreateUserName { get; set; } // CreateUserName
		/// <summary>创建时间</summary>	
		[Description("创建时间")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary>LPosX</summary>	
		[Description("LPosX")]
        public decimal? LPosX { get; set; } // LPosX
		/// <summary>LPosY</summary>	
		[Description("LPosY")]
        public decimal? LPosY { get; set; } // LPosY
		/// <summary>RPosX</summary>	
		[Description("RPosX")]
        public decimal? RPosX { get; set; } // RPosX
		/// <summary>RPosY</summary>	
		[Description("RPosY")]
        public decimal? RPosY { get; set; } // RPosY
		/// <summary>RevisePosX</summary>	
		[Description("RevisePosX")]
        public decimal? RevisePosX { get; set; } // RevisePosX
		/// <summary>RevisePosY</summary>	
		[Description("RevisePosY")]
        public decimal? RevisePosY { get; set; } // RevisePosY
		/// <summary>ReviseWidth</summary>	
		[Description("ReviseWidth")]
        public decimal? ReviseWidth { get; set; } // ReviseWidth
		/// <summary>ReviseHeight</summary>	
		[Description("ReviseHeight")]
        public decimal? ReviseHeight { get; set; } // ReviseHeight
		/// <summary>ReviseAudit</summary>	
		[Description("ReviseAudit")]
        public string ReviseAudit { get; set; } // ReviseAudit
		/// <summary>DrawingID</summary>	
		[Description("DrawingID")]
        public string DrawingID { get; set; } // DrawingID
		/// <summary>ReviseState</summary>	
		[Description("ReviseState")]
        public string ReviseState { get; set; } // ReviseState
		/// <summary>ReviseBasePoint</summary>	
		[Description("ReviseBasePoint")]
        public string ReviseBasePoint { get; set; } // ReviseBasePoint
		/// <summary>ReviseClass</summary>	
		[Description("ReviseClass")]
        public string ReviseClass { get; set; } // ReviseClass
		/// <summary>AuditPathID</summary>	
		[Description("AuditPathID")]
        public string AuditPathID { get; set; } // AuditPathID
		/// <summary>ReviseType</summary>	
		[Description("ReviseType")]
        public string ReviseType { get; set; } // ReviseType
		/// <summary>UpdateTime</summary>	
		[Description("UpdateTime")]
        public DateTime? UpdateTime { get; set; } // UpdateTime
		/// <summary>SubmitDept</summary>	
		[Description("SubmitDept")]
        public string SubmitDept { get; set; } // SubmitDept
		/// <summary>SubmitDeptName</summary>	
		[Description("SubmitDeptName")]
        public string SubmitDeptName { get; set; } // SubmitDeptName
		/// <summary>IsLead</summary>	
		[Description("IsLead")]
        public string IsLead { get; set; } // IsLead
		/// <summary>MarkType</summary>	
		[Description("MarkType")]
        public string MarkType { get; set; } // MarkType
		/// <summary>OpinionState</summary>	
		[Description("OpinionState")]
        public string OpinionState { get; set; } // OpinionState
		/// <summary>EntityId</summary>	
		[Description("EntityId")]
        public string EntityId { get; set; } // EntityId
		/// <summary>ComeForm</summary>	
		[Description("ComeForm")]
        public string ComeForm { get; set; } // ComeForm
		/// <summary>EntityInfoJosn</summary>	
		[Description("EntityInfoJosn")]
        public string EntityInfoJosn { get; set; } // EntityInfoJosn
		/// <summary></summary>	
		[Description("")]
        public string OriginalPicID { get; set; } // OriginalPicID
		/// <summary></summary>	
		[Description("")]
        public string ModifiedPicID { get; set; } // ModifiedPicID

        // Foreign keys
		[JsonIgnore]
        public virtual T_EXE_MettingSign T_EXE_MettingSign { get; set; } //  T_EXE_MettingSignID - FK_T_EXE_MettingSign_AdviceDetail_T_EXE_MettingSign
    }

	/// <summary>项目组成员签收</summary>	
	[Description("项目组成员签收")]
    public partial class T_EXE_MettingSign_ProjectGroupMembers : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string T_EXE_MettingSignID { get; set; } // T_EXE_MettingSignID
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string IsReleased { get; set; } // IsReleased
		/// <summary>会签专业</summary>	
		[Description("会签专业")]
        public string Major { get; set; } // Major
		/// <summary>会签人员</summary>	
		[Description("会签人员")]
        public string MettingUser { get; set; } // MettingUser
		/// <summary>人员名称</summary>	
		[Description("人员名称")]
        public string MettingUserName { get; set; } // MettingUserName
		/// <summary>会签日期</summary>	
		[Description("会签日期")]
        public DateTime? SignDate { get; set; } // SignDate
		/// <summary>会签意见</summary>	
		[Description("会签意见")]
        public string SignComment { get; set; } // SignComment
		/// <summary>专业Code</summary>	
		[Description("专业Code")]
        public string MajorCode { get; set; } // MajorCode
		/// <summary>专业设计人</summary>	
		[Description("专业设计人")]
        public string DesignerByMajor { get; set; } // DesignerByMajor

        // Foreign keys
		[JsonIgnore]
        public virtual T_EXE_MettingSign T_EXE_MettingSign { get; set; } //  T_EXE_MettingSignID - FK_T_EXE_MettingSign_ProjectGroupMembers_T_EXE_MettingSign
    }

	/// <summary>成果列表</summary>	
	[Description("成果列表")]
    public partial class T_EXE_MettingSign_ResultList : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string T_EXE_MettingSignID { get; set; } // T_EXE_MettingSignID
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string IsReleased { get; set; } // IsReleased
		/// <summary>成果ID</summary>	
		[Description("成果ID")]
        public string ProductID { get; set; } // ProductID
		/// <summary>成果编号</summary>	
		[Description("成果编号")]
        public string Code { get; set; } // Code
		/// <summary>成果名称</summary>	
		[Description("成果名称")]
        public string Name { get; set; } // Name
		/// <summary>专业</summary>	
		[Description("专业")]
        public string Major { get; set; } // Major
		/// <summary>专业</summary>	
		[Description("专业")]
        public string MajorValue { get; set; } // MajorValue
		/// <summary>类型</summary>	
		[Description("类型")]
        public string FileType { get; set; } // FileType
		/// <summary>附件</summary>	
		[Description("附件")]
        public string MainFile { get; set; } // MainFile
		/// <summary>版本号</summary>	
		[Description("版本号")]
        public decimal? ProductVersion { get; set; } // ProductVersion

        // Foreign keys
		[JsonIgnore]
        public virtual T_EXE_MettingSign T_EXE_MettingSign { get; set; } //  T_EXE_MettingSignID - FK_T_EXE_MettingSign_ResultList_T_EXE_MettingSign
    }

	/// <summary>成果出版Demo</summary>	
	[Description("成果出版Demo")]
    public partial class T_EXE_ProductPlotDemo : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? ModifyDate { get; set; } // ModifyDate
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get; set; } // CreateUserID
		/// <summary></summary>	
		[Description("")]
        public string CreateUser { get; set; } // CreateUser
		/// <summary></summary>	
		[Description("")]
        public string ModifyUserID { get; set; } // ModifyUserID
		/// <summary></summary>	
		[Description("")]
        public string ModifyUser { get; set; } // ModifyUser
		/// <summary></summary>	
		[Description("")]
        public string OrgID { get; set; } // OrgID
		/// <summary></summary>	
		[Description("")]
        public string CompanyID { get; set; } // CompanyID
		/// <summary></summary>	
		[Description("")]
        public string FlowPhase { get; set; } // FlowPhase
		/// <summary></summary>	
		[Description("")]
        public string FlowInfo { get; set; } // FlowInfo
		/// <summary></summary>	
		[Description("")]
        public string StepName { get; set; } // StepName
		/// <summary>申请人</summary>	
		[Description("申请人")]
        public string ApplyUser { get; set; } // ApplyUser
		/// <summary>申请人名称</summary>	
		[Description("申请人名称")]
        public string ApplyUserName { get; set; } // ApplyUserName
		/// <summary>申请日期</summary>	
		[Description("申请日期")]
        public DateTime? ApplyDate { get; set; } // ApplyDate
		/// <summary>成果明细</summary>	
		[Description("成果明细")]
        public string ProductDetail { get; set; } // ProductDetail
		/// <summary>项目名称</summary>	
		[Description("项目名称")]
        public string ProjectInfoName { get; set; } // ProjectInfoName
		/// <summary>项目编号</summary>	
		[Description("项目编号")]
        public string ProjectInfoCode { get; set; } // ProjectInfoCode
		/// <summary>ProjectInfoID</summary>	
		[Description("ProjectInfoID")]
        public string ProjectInfoID { get; set; } // ProjectInfoID

        // Reverse navigation
		[JsonIgnore]
        public virtual ICollection<T_EXE_ProductPlotDemo_ProductDetail> T_EXE_ProductPlotDemo_ProductDetail { get { onT_EXE_ProductPlotDemo_ProductDetailGetting(); return _T_EXE_ProductPlotDemo_ProductDetail;} }
		private ICollection<T_EXE_ProductPlotDemo_ProductDetail> _T_EXE_ProductPlotDemo_ProductDetail;
		partial void onT_EXE_ProductPlotDemo_ProductDetailGetting();


        public T_EXE_ProductPlotDemo()
        {
            _T_EXE_ProductPlotDemo_ProductDetail = new List<T_EXE_ProductPlotDemo_ProductDetail>();
        }
    }

	/// <summary>成果明细</summary>	
	[Description("成果明细")]
    public partial class T_EXE_ProductPlotDemo_ProductDetail : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string T_EXE_ProductPlotDemoID { get; set; } // T_EXE_ProductPlotDemoID
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string IsReleased { get; set; } // IsReleased
		/// <summary>ProductID</summary>	
		[Description("ProductID")]
        public string ProductID { get; set; } // ProductID
		/// <summary>成果名称</summary>	
		[Description("成果名称")]
        public string Name { get; set; } // Name
		/// <summary>编号</summary>	
		[Description("编号")]
        public string Code { get; set; } // Code
		/// <summary>提交人</summary>	
		[Description("提交人")]
        public string Designer { get; set; } // Designer
		/// <summary>提交日期</summary>	
		[Description("提交日期")]
        public DateTime? SubmitDate { get; set; } // SubmitDate
		/// <summary>提交人名称</summary>	
		[Description("提交人名称")]
        public string DesignerName { get; set; } // DesignerName
		/// <summary>PdfFile</summary>	
		[Description("PdfFile")]
        public string PdfFile { get; set; } // PdfFile
		/// <summary>SignPdfFile</summary>	
		[Description("SignPdfFile")]
        public string SignPdfFile { get; set; } // SignPdfFile

        // Foreign keys
		[JsonIgnore]
        public virtual T_EXE_ProductPlotDemo T_EXE_ProductPlotDemo { get; set; } //  T_EXE_ProductPlotDemoID - FK_T_EXE_ProductPlotDemo_ProductDetail_T_EXE_ProductPlotDemo
    }

	/// <summary>图纸会审</summary>	
	[Description("图纸会审")]
    public partial class T_EXE_ProductReview : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? ModifyDate { get; set; } // ModifyDate
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get; set; } // CreateUserID
		/// <summary></summary>	
		[Description("")]
        public string CreateUser { get; set; } // CreateUser
		/// <summary></summary>	
		[Description("")]
        public string ModifyUserID { get; set; } // ModifyUserID
		/// <summary></summary>	
		[Description("")]
        public string ModifyUser { get; set; } // ModifyUser
		/// <summary></summary>	
		[Description("")]
        public string OrgID { get; set; } // OrgID
		/// <summary></summary>	
		[Description("")]
        public string CompanyID { get; set; } // CompanyID
		/// <summary></summary>	
		[Description("")]
        public string FlowPhase { get; set; } // FlowPhase
		/// <summary></summary>	
		[Description("")]
        public string FlowInfo { get; set; } // FlowInfo
		/// <summary></summary>	
		[Description("")]
        public string StepName { get; set; } // StepName
		/// <summary>项目名称</summary>	
		[Description("项目名称")]
        public string ProjectInfoName { get; set; } // ProjectInfoName
		/// <summary>项目编号</summary>	
		[Description("项目编号")]
        public string ProjectInfoCode { get; set; } // ProjectInfoCode
		/// <summary>阶段</summary>	
		[Description("阶段")]
        public string Phase { get; set; } // Phase
		/// <summary>提交人</summary>	
		[Description("提交人")]
        public string SubmitUser { get; set; } // SubmitUser
		/// <summary>提交人名称</summary>	
		[Description("提交人名称")]
        public string SubmitUserName { get; set; } // SubmitUserName
		/// <summary>专业方向</summary>	
		[Description("专业方向")]
        public string MajorDirection { get; set; } // MajorDirection
		/// <summary>接收人</summary>	
		[Description("接收人")]
        public string ReceiveUser { get; set; } // ReceiveUser
		/// <summary>接收人名称</summary>	
		[Description("接收人名称")]
        public string ReceiveUserName { get; set; } // ReceiveUserName
		/// <summary>复核签字</summary>	
		[Description("复核签字")]
        public string FH { get; set; } // FH
		/// <summary>审核签字</summary>	
		[Description("审核签字")]
        public string SH { get; set; } // SH
		/// <summary>专业审定</summary>	
		[Description("专业审定")]
        public string ZYSD { get; set; } // ZYSD
		/// <summary>ProjectInfoID</summary>	
		[Description("ProjectInfoID")]
        public string ProjectInfoID { get; set; } // ProjectInfoID
		/// <summary>复核人</summary>	
		[Description("复核人")]
        public string Collactor { get; set; } // Collactor
		/// <summary>复核人名称</summary>	
		[Description("复核人名称")]
        public string CollactorName { get; set; } // CollactorName
		/// <summary>审核人</summary>	
		[Description("审核人")]
        public string Auditor { get; set; } // Auditor
		/// <summary>审核人名称</summary>	
		[Description("审核人名称")]
        public string AuditorName { get; set; } // AuditorName
		/// <summary>专业审定人</summary>	
		[Description("专业审定人")]
        public string MajorApprover { get; set; } // MajorApprover
		/// <summary>专业审定人名称</summary>	
		[Description("专业审定人名称")]
        public string MajorApproverName { get; set; } // MajorApproverName
		/// <summary>项目组成员</summary>	
		[Description("项目组成员")]
        public string ProjectUserlist { get; set; } // ProjectUserlist
		/// <summary>项目组成员名称</summary>	
		[Description("项目组成员名称")]
        public string ProjectUserlistName { get; set; } // ProjectUserlistName
		/// <summary>项目审定人</summary>	
		[Description("项目审定人")]
        public string Approver { get; set; } // Approver
		/// <summary>项目审定人名称</summary>	
		[Description("项目审定人名称")]
        public string ApproverName { get; set; } // ApproverName
		/// <summary>版本号</summary>	
		[Description("版本号")]
        public string VersionNumber { get; set; } // VersionNumber

        // Reverse navigation
		[JsonIgnore]
        public virtual ICollection<T_EXE_ProductReview_ProjectGroupMembers> T_EXE_ProductReview_ProjectGroupMembers { get { onT_EXE_ProductReview_ProjectGroupMembersGetting(); return _T_EXE_ProductReview_ProjectGroupMembers;} }
		private ICollection<T_EXE_ProductReview_ProjectGroupMembers> _T_EXE_ProductReview_ProjectGroupMembers;
		partial void onT_EXE_ProductReview_ProjectGroupMembersGetting();

		[JsonIgnore]
        public virtual ICollection<T_EXE_ProductReview_ResultList> T_EXE_ProductReview_ResultList { get { onT_EXE_ProductReview_ResultListGetting(); return _T_EXE_ProductReview_ResultList;} }
		private ICollection<T_EXE_ProductReview_ResultList> _T_EXE_ProductReview_ResultList;
		partial void onT_EXE_ProductReview_ResultListGetting();


        public T_EXE_ProductReview()
        {
            _T_EXE_ProductReview_ProjectGroupMembers = new List<T_EXE_ProductReview_ProjectGroupMembers>();
            _T_EXE_ProductReview_ResultList = new List<T_EXE_ProductReview_ResultList>();
        }
    }

	/// <summary>项目组成员签收</summary>	
	[Description("项目组成员签收")]
    public partial class T_EXE_ProductReview_ProjectGroupMembers : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string T_EXE_ProductReviewID { get; set; } // T_EXE_ProductReviewID
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string IsReleased { get; set; } // IsReleased
		/// <summary>人员</summary>	
		[Description("人员")]
        public string MettingUser { get; set; } // MettingUser
		/// <summary>人员名称</summary>	
		[Description("人员名称")]
        public string MettingUserName { get; set; } // MettingUserName
		/// <summary>专业</summary>	
		[Description("专业")]
        public string Major { get; set; } // Major
		/// <summary>部门</summary>	
		[Description("部门")]
        public string Dept { get; set; } // Dept
		/// <summary>部门名称</summary>	
		[Description("部门名称")]
        public string DeptName { get; set; } // DeptName
		/// <summary>签收日期</summary>	
		[Description("签收日期")]
        public DateTime? SignDate { get; set; } // SignDate
		/// <summary>签收意见</summary>	
		[Description("签收意见")]
        public string SignComment { get; set; } // SignComment
		/// <summary>专业Code</summary>	
		[Description("专业Code")]
        public string MajorCode { get; set; } // MajorCode

        // Foreign keys
		[JsonIgnore]
        public virtual T_EXE_ProductReview T_EXE_ProductReview { get; set; } //  T_EXE_ProductReviewID - FK_T_EXE_ProductReview_ProjectGroupMembers_T_EXE_ProductReview
    }

	/// <summary>成果列表</summary>	
	[Description("成果列表")]
    public partial class T_EXE_ProductReview_ResultList : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string T_EXE_ProductReviewID { get; set; } // T_EXE_ProductReviewID
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string IsReleased { get; set; } // IsReleased
		/// <summary>成果ID</summary>	
		[Description("成果ID")]
        public string ProductID { get; set; } // ProductID
		/// <summary>成果编号</summary>	
		[Description("成果编号")]
        public string Code { get; set; } // Code
		/// <summary>成果名称</summary>	
		[Description("成果名称")]
        public string Name { get; set; } // Name
		/// <summary>专业</summary>	
		[Description("专业")]
        public string Major { get; set; } // Major
		/// <summary>专业Code</summary>	
		[Description("专业Code")]
        public string MajorValue { get; set; } // MajorValue
		/// <summary>类型</summary>	
		[Description("类型")]
        public string FileType { get; set; } // FileType
		/// <summary>附件</summary>	
		[Description("附件")]
        public string MainFile { get; set; } // MainFile

        // Foreign keys
		[JsonIgnore]
        public virtual T_EXE_ProductReview T_EXE_ProductReview { get; set; } //  T_EXE_ProductReviewID - FK_T_EXE_ProductReview_ResultList_T_EXE_ProductReview
    }

	/// <summary>项目考核表</summary>	
	[Description("项目考核表")]
    public partial class T_EXE_ProjectExamination : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary>编制日期</summary>	
		[Description("编制日期")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? ModifyDate { get; set; } // ModifyDate
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get; set; } // CreateUserID
		/// <summary>编制人</summary>	
		[Description("编制人")]
        public string CreateUser { get; set; } // CreateUser
		/// <summary></summary>	
		[Description("")]
        public string ModifyUserID { get; set; } // ModifyUserID
		/// <summary></summary>	
		[Description("")]
        public string ModifyUser { get; set; } // ModifyUser
		/// <summary></summary>	
		[Description("")]
        public string OrgID { get; set; } // OrgID
		/// <summary></summary>	
		[Description("")]
        public string CompanyID { get; set; } // CompanyID
		/// <summary></summary>	
		[Description("")]
        public string FlowPhase { get; set; } // FlowPhase
		/// <summary></summary>	
		[Description("")]
        public string FlowInfo { get; set; } // FlowInfo
		/// <summary></summary>	
		[Description("")]
        public string StepName { get; set; } // StepName
		/// <summary>项目名称名称</summary>	
		[Description("项目名称名称")]
        public string ProjectInfoName { get; set; } // ProjectInfoName
		/// <summary>项目编号</summary>	
		[Description("项目编号")]
        public string ProjectInfoCode { get; set; } // ProjectInfoCode
		/// <summary>项目负责人</summary>	
		[Description("项目负责人")]
        public string ProjectManager { get; set; } // ProjectManager
		/// <summary>项目负责人名称</summary>	
		[Description("项目负责人名称")]
        public string ProjectManagerName { get; set; } // ProjectManagerName
		/// <summary>设计阶段</summary>	
		[Description("设计阶段")]
        public string Phase { get; set; } // Phase
		/// <summary>业务类型</summary>	
		[Description("业务类型")]
        public string ProjectClass { get; set; } // ProjectClass
		/// <summary>综合得分</summary>	
		[Description("综合得分")]
        public decimal? Result { get; set; } // Result
		/// <summary>编制人名称</summary>	
		[Description("编制人名称")]
        public string CreateUserName { get; set; } // CreateUserName
		/// <summary>ProjectInfoID</summary>	
		[Description("ProjectInfoID")]
        public string ProjectInfoID { get; set; } // ProjectInfoID
		/// <summary>EnginneringInfoID</summary>	
		[Description("EnginneringInfoID")]
        public string EnginneringInfoID { get; set; } // EnginneringInfoID
		/// <summary>项目名称</summary>	
		[Description("项目名称")]
        public string ProjectInfo { get; set; } // ProjectInfo
		/// <summary>VersionNumber</summary>	
		[Description("VersionNumber")]
        public string VersionNumber { get; set; } // VersionNumber
		/// <summary>项目负责人意见</summary>	
		[Description("项目负责人意见")]
        public string ProjectLeader { get; set; } // ProjectLeader
		/// <summary>技术质量部意见</summary>	
		[Description("技术质量部意见")]
        public string TechnicalQualityDept { get; set; } // TechnicalQualityDept

        // Reverse navigation
		[JsonIgnore]
        public virtual ICollection<T_EXE_ProjectExamination_Content> T_EXE_ProjectExamination_Content { get { onT_EXE_ProjectExamination_ContentGetting(); return _T_EXE_ProjectExamination_Content;} }
		private ICollection<T_EXE_ProjectExamination_Content> _T_EXE_ProjectExamination_Content;
		partial void onT_EXE_ProjectExamination_ContentGetting();


        public T_EXE_ProjectExamination()
        {
            _T_EXE_ProjectExamination_Content = new List<T_EXE_ProjectExamination_Content>();
        }
    }

	/// <summary>考核内容</summary>	
	[Description("考核内容")]
    public partial class T_EXE_ProjectExamination_Content : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string T_EXE_ProjectExaminationID { get; set; } // T_EXE_ProjectExaminationID
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string IsReleased { get; set; } // IsReleased
		/// <summary>考核内容</summary>	
		[Description("考核内容")]
        public string Content { get; set; } // Content
		/// <summary>得分</summary>	
		[Description("得分")]
        public decimal? Result { get; set; } // Result
		/// <summary>备注</summary>	
		[Description("备注")]
        public string Remark { get; set; } // Remark

        // Foreign keys
		[JsonIgnore]
        public virtual T_EXE_ProjectExamination T_EXE_ProjectExamination { get; set; } //  T_EXE_ProjectExaminationID - FK_T_EXE_ProjectExamination_Content_T_EXE_ProjectExamination
    }

	/// <summary>数字化出版出图单</summary>	
	[Description("数字化出版出图单")]
    public partial class T_EXE_PublishApply : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? ModifyDate { get; set; } // ModifyDate
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get; set; } // CreateUserID
		/// <summary></summary>	
		[Description("")]
        public string CreateUser { get; set; } // CreateUser
		/// <summary></summary>	
		[Description("")]
        public string ModifyUserID { get; set; } // ModifyUserID
		/// <summary></summary>	
		[Description("")]
        public string ModifyUser { get; set; } // ModifyUser
		/// <summary></summary>	
		[Description("")]
        public string OrgID { get; set; } // OrgID
		/// <summary></summary>	
		[Description("")]
        public string CompanyID { get; set; } // CompanyID
		/// <summary></summary>	
		[Description("")]
        public string FlowPhase { get; set; } // FlowPhase
		/// <summary></summary>	
		[Description("")]
        public string FlowInfo { get; set; } // FlowInfo
		/// <summary></summary>	
		[Description("")]
        public string StepName { get; set; } // StepName
		/// <summary>编号</summary>	
		[Description("编号")]
        public string SerialNumber { get; set; } // SerialNumber
		/// <summary>项目ID</summary>	
		[Description("项目ID")]
        public string ProjectInfoID { get; set; } // ProjectInfoID
		/// <summary>项目名称</summary>	
		[Description("项目名称")]
        public string Project { get; set; } // Project
		/// <summary>项目名称名称</summary>	
		[Description("项目名称名称")]
        public string ProjectName { get; set; } // ProjectName
		/// <summary>项目编号</summary>	
		[Description("项目编号")]
        public string ProjectCode { get; set; } // ProjectCode
		/// <summary>设计专业</summary>	
		[Description("设计专业")]
        public string MajorCode { get; set; } // MajorCode
		/// <summary>专业负责人签字</summary>	
		[Description("专业负责人签字")]
        public string ProfessionalChargeView { get; set; } // ProfessionalChargeView
		/// <summary>项目负责人签字</summary>	
		[Description("项目负责人签字")]
        public string ProjectManngerView { get; set; } // ProjectManngerView
		/// <summary>折合A1</summary>	
		[Description("折合A1")]
        public decimal? ToA1 { get; set; } // ToA1
		/// <summary>单价</summary>	
		[Description("单价")]
        public decimal? Price { get; set; } // Price
		/// <summary>成本</summary>	
		[Description("成本")]
        public decimal? CostAmount { get; set; } // CostAmount
		/// <summary>ProjectManager</summary>	
		[Description("ProjectManager")]
        public string ProjectManager { get; set; } // ProjectManager
		/// <summary>ProjectManager名称</summary>	
		[Description("ProjectManager名称")]
        public string ProjectManagerName { get; set; } // ProjectManagerName
		/// <summary>委托明细</summary>	
		[Description("委托明细")]
        public string PriceDetail { get; set; } // PriceDetail
		/// <summary>申请人</summary>	
		[Description("申请人")]
        public string ApplyUser { get; set; } // ApplyUser
		/// <summary>申请人名称</summary>	
		[Description("申请人名称")]
        public string ApplyUserName { get; set; } // ApplyUserName
		/// <summary>申请日期</summary>	
		[Description("申请日期")]
        public DateTime? ApplyDate { get; set; } // ApplyDate
		/// <summary>备注</summary>	
		[Description("备注")]
        public string Remark { get; set; } // Remark
		/// <summary>实际成本</summary>	
		[Description("实际成本")]
        public decimal? RealCostAmount { get; set; } // RealCostAmount
		/// <summary>提交时间</summary>	
		[Description("提交时间")]
        public DateTime? SubmitTime { get; set; } // SubmitTime
		/// <summary>确认时间</summary>	
		[Description("确认时间")]
        public DateTime? ConfirmTime { get; set; } // ConfirmTime
		/// <summary>费用科目名称</summary>	
		[Description("费用科目名称")]
        public string CostName { get; set; } // CostName
		/// <summary>费用科目编号</summary>	
		[Description("费用科目编号")]
        public string CostCode { get; set; } // CostCode
		/// <summary>申请部门</summary>	
		[Description("申请部门")]
        public string ApplyDept { get; set; } // ApplyDept
		/// <summary>申请部门名称</summary>	
		[Description("申请部门名称")]
        public string ApplyDeptName { get; set; } // ApplyDeptName
		/// <summary>图幅Json</summary>	
		[Description("图幅Json")]
        public string BorderSizeJson { get; set; } // BorderSizeJson
		/// <summary>FormCode</summary>	
		[Description("FormCode")]
        public string FormCode { get; set; } // FormCode
		/// <summary>图纸份数</summary>	
		[Description("图纸份数")]
        public int? ProductCount { get; set; } // ProductCount

        // Reverse navigation
		[JsonIgnore]
        public virtual ICollection<T_EXE_PublishApply_PriceDetail> T_EXE_PublishApply_PriceDetail { get { onT_EXE_PublishApply_PriceDetailGetting(); return _T_EXE_PublishApply_PriceDetail;} }
		private ICollection<T_EXE_PublishApply_PriceDetail> _T_EXE_PublishApply_PriceDetail;
		partial void onT_EXE_PublishApply_PriceDetailGetting();

		[JsonIgnore]
        public virtual ICollection<T_EXE_PublishApply_Products> T_EXE_PublishApply_Products { get { onT_EXE_PublishApply_ProductsGetting(); return _T_EXE_PublishApply_Products;} }
		private ICollection<T_EXE_PublishApply_Products> _T_EXE_PublishApply_Products;
		partial void onT_EXE_PublishApply_ProductsGetting();


        public T_EXE_PublishApply()
        {
            _T_EXE_PublishApply_PriceDetail = new List<T_EXE_PublishApply_PriceDetail>();
            _T_EXE_PublishApply_Products = new List<T_EXE_PublishApply_Products>();
        }
    }

	/// <summary>委托明细</summary>	
	[Description("委托明细")]
    public partial class T_EXE_PublishApply_PriceDetail : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string T_EXE_PublishApplyID { get; set; } // T_EXE_PublishApplyID
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string IsReleased { get; set; } // IsReleased
		/// <summary>类型</summary>	
		[Description("类型")]
        public string PublicationType { get; set; } // PublicationType
		/// <summary>规格</summary>	
		[Description("规格")]
        public string Specification { get; set; } // Specification
		/// <summary>张数</summary>	
		[Description("张数")]
        public decimal? Num { get; set; } // Num
		/// <summary>单价(元)</summary>	
		[Description("单价(元)")]
        public decimal? Price { get; set; } // Price
		/// <summary>合计(元)</summary>	
		[Description("合计(元)")]
        public decimal? Sum { get; set; } // Sum
		/// <summary>总成本(元)</summary>	
		[Description("总成本(元)")]
        public decimal? SumCost { get; set; } // SumCost
		/// <summary>实际张数</summary>	
		[Description("实际张数")]
        public decimal? RealNum { get; set; } // RealNum
		/// <summary>实际单价(元)</summary>	
		[Description("实际单价(元)")]
        public decimal? RealPrice { get; set; } // RealPrice
		/// <summary>实际合计(元)</summary>	
		[Description("实际合计(元)")]
        public decimal? RealSum { get; set; } // RealSum
		/// <summary>实际总成本(元)</summary>	
		[Description("实际总成本(元)")]
        public decimal? RealSumCost { get; set; } // RealSumCost
		/// <summary>CostInfoID</summary>	
		[Description("CostInfoID")]
        public string CostInfoID { get; set; } // CostInfoID

        // Foreign keys
		[JsonIgnore]
        public virtual T_EXE_PublishApply T_EXE_PublishApply { get; set; } //  T_EXE_PublishApplyID - FK_T_EXE_PublishApply_PriceDetail_T_EXE_PublishApply
    }

	/// <summary>成果子表</summary>	
	[Description("成果子表")]
    public partial class T_EXE_PublishApply_Products : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string T_EXE_PublishApplyID { get; set; } // T_EXE_PublishApplyID
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string IsReleased { get; set; } // IsReleased
		/// <summary>图纸ID</summary>	
		[Description("图纸ID")]
        public string ProductID { get; set; } // ProductID
		/// <summary>成果信息.名称</summary>	
		[Description("成果信息.名称")]
        public string ProductName { get; set; } // ProductName
		/// <summary>成果信息.编号</summary>	
		[Description("成果信息.编号")]
        public string ProductCode { get; set; } // ProductCode
		/// <summary>成果信息.规格</summary>	
		[Description("成果信息.规格")]
        public string Specifications { get; set; } // Specifications
		/// <summary>张数</summary>	
		[Description("张数")]
        public int? Count { get; set; } // Count
		/// <summary>签章信息.签章</summary>	
		[Description("签章信息.签章")]
        public string PlotSealGroup { get; set; } // PlotSealGroup
		/// <summary>签章信息.签章名称</summary>	
		[Description("签章信息.签章名称")]
        public string PlotSealGroupName { get; set; } // PlotSealGroupName
		/// <summary>成果信息.附件</summary>	
		[Description("成果信息.附件")]
        public string MainAttachments { get; set; } // MainAttachments
		/// <summary>成果信息.版本号</summary>	
		[Description("成果信息.版本号")]
        public decimal? ProductVersion { get; set; } // ProductVersion
		/// <summary>版本ID</summary>	
		[Description("版本ID")]
        public string VersionID { get; set; } // VersionID
		/// <summary>成果信息.PDF附件</summary>	
		[Description("成果信息.PDF附件")]
        public string PdfFile { get; set; } // PdfFile
		/// <summary>成果信息.打印附件</summary>	
		[Description("成果信息.打印附件")]
        public string PlotFile { get; set; } // PlotFile
		/// <summary>成果信息.已盖章附件</summary>	
		[Description("成果信息.已盖章附件")]
        public string SignPdfFile { get; set; } // SignPdfFile
		/// <summary>成果信息.Xref文件</summary>	
		[Description("成果信息.Xref文件")]
        public string XrefFile { get; set; } // XrefFile
		/// <summary>成果信息.Dwf文件</summary>	
		[Description("成果信息.Dwf文件")]
        public string DwfFile { get; set; } // DwfFile
		/// <summary>成果信息.Tiff文件</summary>	
		[Description("成果信息.Tiff文件")]
        public string TiffFile { get; set; } // TiffFile
		/// <summary>签名信息.设计人</summary>	
		[Description("签名信息.设计人")]
        public string Designer { get; set; } // Designer
		/// <summary>签名信息.设计人名称</summary>	
		[Description("签名信息.设计人名称")]
        public string DesignerName { get; set; } // DesignerName
		/// <summary>签名信息.校对人</summary>	
		[Description("签名信息.校对人")]
        public string Collactor { get; set; } // Collactor
		/// <summary>签名信息.校对人名称</summary>	
		[Description("签名信息.校对人名称")]
        public string CollactorName { get; set; } // CollactorName
		/// <summary>签名信息.审核人</summary>	
		[Description("签名信息.审核人")]
        public string Auditor { get; set; } // Auditor
		/// <summary>签名信息.审核人名称</summary>	
		[Description("签名信息.审核人名称")]
        public string AuditorName { get; set; } // AuditorName
		/// <summary>签名信息.审定人</summary>	
		[Description("签名信息.审定人")]
        public string Approver { get; set; } // Approver
		/// <summary>签名信息.审定人名称</summary>	
		[Description("签名信息.审定人名称")]
        public string ApproverName { get; set; } // ApproverName
		/// <summary>签名信息.项目负责人</summary>	
		[Description("签名信息.项目负责人")]
        public string ProjectManager { get; set; } // ProjectManager
		/// <summary>签名信息.项目负责人名称</summary>	
		[Description("签名信息.项目负责人名称")]
        public string ProjectManagerName { get; set; } // ProjectManagerName
		/// <summary>签名信息.设总</summary>	
		[Description("签名信息.设总")]
        public string DesignManager { get; set; } // DesignManager
		/// <summary>签名信息.设总名称</summary>	
		[Description("签名信息.设总名称")]
        public string DesignManagerName { get; set; } // DesignManagerName
		/// <summary>签名信息.制图人</summary>	
		[Description("签名信息.制图人")]
        public string Mapper { get; set; } // Mapper
		/// <summary>签名信息.制图人名称</summary>	
		[Description("签名信息.制图人名称")]
        public string MapperName { get; set; } // MapperName
		/// <summary>签名信息.专业负责人</summary>	
		[Description("签名信息.专业负责人")]
        public string MajorPrinciple { get; set; } // MajorPrinciple
		/// <summary>签名信息.专业负责人名称</summary>	
		[Description("签名信息.专业负责人名称")]
        public string MajorPrincipleName { get; set; } // MajorPrincipleName
		/// <summary>签名信息.专业总工程师</summary>	
		[Description("签名信息.专业总工程师")]
        public string MajorEngineer { get; set; } // MajorEngineer
		/// <summary>签名信息.专业总工程师名称</summary>	
		[Description("签名信息.专业总工程师名称")]
        public string MajorEngineerName { get; set; } // MajorEngineerName
		/// <summary>签名信息.部门负责人</summary>	
		[Description("签名信息.部门负责人")]
        public string DeptManager { get; set; } // DeptManager
		/// <summary>签名信息.部门负责人名称</summary>	
		[Description("签名信息.部门负责人名称")]
        public string DeptManagerName { get; set; } // DeptManagerName
		/// <summary>CanSetUser</summary>	
		[Description("CanSetUser")]
        public int? CanSetUser { get; set; } // CanSetUser
		/// <summary>签章信息.位置</summary>	
		[Description("签章信息.位置")]
        public string PDFSignPositionInfo { get; set; } // PDFSignPositionInfo
		/// <summary>PlotSealGroupKey</summary>	
		[Description("PlotSealGroupKey")]
        public string PlotSealGroupKey { get; set; } // PlotSealGroupKey
		/// <summary>CoSignUser</summary>	
		[Description("CoSignUser")]
        public string CoSignUser { get; set; } // CoSignUser
		/// <summary>成果信息.图号</summary>	
		[Description("成果信息.图号")]
        public string DrawingCode { get; set; } // DrawingCode

        // Foreign keys
		[JsonIgnore]
        public virtual T_EXE_PublishApply T_EXE_PublishApply { get; set; } //  T_EXE_PublishApplyID - FK_T_EXE_PublishApply_Products_T_EXE_PublishApply
    }

	/// <summary>卷册工时变更单</summary>	
	[Description("卷册工时变更单")]
    public partial class T_EXE_TaskWorkChange : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? ModifyDate { get; set; } // ModifyDate
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get; set; } // CreateUserID
		/// <summary></summary>	
		[Description("")]
        public string CreateUser { get; set; } // CreateUser
		/// <summary></summary>	
		[Description("")]
        public string ModifyUserID { get; set; } // ModifyUserID
		/// <summary></summary>	
		[Description("")]
        public string ModifyUser { get; set; } // ModifyUser
		/// <summary></summary>	
		[Description("")]
        public string OrgID { get; set; } // OrgID
		/// <summary></summary>	
		[Description("")]
        public string CompanyID { get; set; } // CompanyID
		/// <summary></summary>	
		[Description("")]
        public string FlowPhase { get; set; } // FlowPhase
		/// <summary></summary>	
		[Description("")]
        public string FlowInfo { get; set; } // FlowInfo
		/// <summary></summary>	
		[Description("")]
        public string StepName { get; set; } // StepName
		/// <summary>项目名称</summary>	
		[Description("项目名称")]
        public string ProjectInfo { get; set; } // ProjectInfo
		/// <summary>项目名称名称</summary>	
		[Description("项目名称名称")]
        public string ProjectInfoName { get; set; } // ProjectInfoName
		/// <summary>项目编号</summary>	
		[Description("项目编号")]
        public string ProjectCode { get; set; } // ProjectCode
		/// <summary>项目负责人</summary>	
		[Description("项目负责人")]
        public string ProjectManager { get; set; } // ProjectManager
		/// <summary>项目负责人名称</summary>	
		[Description("项目负责人名称")]
        public string ProjectManagerName { get; set; } // ProjectManagerName
		/// <summary>设计阶段</summary>	
		[Description("设计阶段")]
        public string Phase { get; set; } // Phase
		/// <summary>专业</summary>	
		[Description("专业")]
        public string MajorValue { get; set; } // MajorValue
		/// <summary>专业Name</summary>	
		[Description("专业Name")]
        public string MajorName { get; set; } // MajorName
		/// <summary>专业负责人</summary>	
		[Description("专业负责人")]
        public string MajorPrinciple { get; set; } // MajorPrinciple
		/// <summary>专业负责人名称</summary>	
		[Description("专业负责人名称")]
        public string MajorPrincipleName { get; set; } // MajorPrincipleName
		/// <summary>归属部门</summary>	
		[Description("归属部门")]
        public string BelongDept { get; set; } // BelongDept
		/// <summary>归属部门名称</summary>	
		[Description("归属部门名称")]
        public string BelongDeptName { get; set; } // BelongDeptName
		/// <summary>归属日期</summary>	
		[Description("归属日期")]
        public DateTime? BelongDate { get; set; } // BelongDate
		/// <summary>ProjectInfoID</summary>	
		[Description("ProjectInfoID")]
        public string ProjectInfoID { get; set; } // ProjectInfoID
		/// <summary>卷册列表</summary>	
		[Description("卷册列表")]
        public string TaskWork { get; set; } // TaskWork
		/// <summary>备注</summary>	
		[Description("备注")]
        public string Remark { get; set; } // Remark

        // Reverse navigation
		[JsonIgnore]
        public virtual ICollection<T_EXE_TaskWorkChange_TaskWork> T_EXE_TaskWorkChange_TaskWork { get { onT_EXE_TaskWorkChange_TaskWorkGetting(); return _T_EXE_TaskWorkChange_TaskWork;} }
		private ICollection<T_EXE_TaskWorkChange_TaskWork> _T_EXE_TaskWorkChange_TaskWork;
		partial void onT_EXE_TaskWorkChange_TaskWorkGetting();


        public T_EXE_TaskWorkChange()
        {
            _T_EXE_TaskWorkChange_TaskWork = new List<T_EXE_TaskWorkChange_TaskWork>();
        }
    }

	/// <summary>卷册列表</summary>	
	[Description("卷册列表")]
    public partial class T_EXE_TaskWorkChange_TaskWork : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string T_EXE_TaskWorkChangeID { get; set; } // T_EXE_TaskWorkChangeID
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string IsReleased { get; set; } // IsReleased
		/// <summary>专业</summary>	
		[Description("专业")]
        public string Major { get; set; } // Major
		/// <summary>卷册名称</summary>	
		[Description("卷册名称")]
        public string Name { get; set; } // Name
		/// <summary>卷册编号</summary>	
		[Description("卷册编号")]
        public string Code { get; set; } // Code
		/// <summary>原定额工时</summary>	
		[Description("原定额工时")]
        public string OldWorkload { get; set; } // OldWorkload
		/// <summary>定额工时</summary>	
		[Description("定额工时")]
        public decimal? Workload { get; set; } // Workload
		/// <summary>原计划完成日期</summary>	
		[Description("原计划完成日期")]
        public DateTime? OldPlanEndDate { get; set; } // OldPlanEndDate
		/// <summary>计划完成日期</summary>	
		[Description("计划完成日期")]
        public DateTime? PlanEndDate { get; set; } // PlanEndDate
		/// <summary>角色分配</summary>	
		[Description("角色分配")]
        public string RoleRate { get; set; } // RoleRate
		/// <summary>卷册ID</summary>	
		[Description("卷册ID")]
        public string TaskWorkID { get; set; } // TaskWorkID
		/// <summary>IsChanged</summary>	
		[Description("IsChanged")]
        public string IsChanged { get; set; } // IsChanged
		/// <summary>阶段</summary>	
		[Description("阶段")]
        public string Phase { get; set; } // Phase

        // Foreign keys
		[JsonIgnore]
        public virtual T_EXE_TaskWorkChange T_EXE_TaskWorkChange { get; set; } //  T_EXE_TaskWorkChangeID - FK_T_EXE_TaskWorkChange_TaskWork_T_EXE_TaskWorkChange
    }

	/// <summary>卷册工时下达单</summary>	
	[Description("卷册工时下达单")]
    public partial class T_EXE_TaskWorkPublish : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? ModifyDate { get; set; } // ModifyDate
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get; set; } // CreateUserID
		/// <summary></summary>	
		[Description("")]
        public string CreateUser { get; set; } // CreateUser
		/// <summary></summary>	
		[Description("")]
        public string ModifyUserID { get; set; } // ModifyUserID
		/// <summary></summary>	
		[Description("")]
        public string ModifyUser { get; set; } // ModifyUser
		/// <summary></summary>	
		[Description("")]
        public string OrgID { get; set; } // OrgID
		/// <summary></summary>	
		[Description("")]
        public string CompanyID { get; set; } // CompanyID
		/// <summary></summary>	
		[Description("")]
        public string FlowPhase { get; set; } // FlowPhase
		/// <summary></summary>	
		[Description("")]
        public string FlowInfo { get; set; } // FlowInfo
		/// <summary></summary>	
		[Description("")]
        public string StepName { get; set; } // StepName
		/// <summary>项目名称</summary>	
		[Description("项目名称")]
        public string ProjectInfo { get; set; } // ProjectInfo
		/// <summary>项目名称名称</summary>	
		[Description("项目名称名称")]
        public string ProjectInfoName { get; set; } // ProjectInfoName
		/// <summary>项目编号</summary>	
		[Description("项目编号")]
        public string ProjectCode { get; set; } // ProjectCode
		/// <summary>项目负责人</summary>	
		[Description("项目负责人")]
        public string ProjectManager { get; set; } // ProjectManager
		/// <summary>项目负责人名称</summary>	
		[Description("项目负责人名称")]
        public string ProjectManagerName { get; set; } // ProjectManagerName
		/// <summary>设计阶段</summary>	
		[Description("设计阶段")]
        public string Phase { get; set; } // Phase
		/// <summary>专业</summary>	
		[Description("专业")]
        public string MajorValue { get; set; } // MajorValue
		/// <summary>专业Name</summary>	
		[Description("专业Name")]
        public string MajorName { get; set; } // MajorName
		/// <summary>专业负责人</summary>	
		[Description("专业负责人")]
        public string MajorPrinciple { get; set; } // MajorPrinciple
		/// <summary>专业负责人名称</summary>	
		[Description("专业负责人名称")]
        public string MajorPrincipleName { get; set; } // MajorPrincipleName
		/// <summary>归属部门</summary>	
		[Description("归属部门")]
        public string BelongDept { get; set; } // BelongDept
		/// <summary>归属部门名称</summary>	
		[Description("归属部门名称")]
        public string BelongDeptName { get; set; } // BelongDeptName
		/// <summary>归属日期</summary>	
		[Description("归属日期")]
        public DateTime? BelongDate { get; set; } // BelongDate
		/// <summary>ProjectInfoID</summary>	
		[Description("ProjectInfoID")]
        public string ProjectInfoID { get; set; } // ProjectInfoID
		/// <summary>卷册列表</summary>	
		[Description("卷册列表")]
        public string TaskWork { get; set; } // TaskWork
		/// <summary>备注</summary>	
		[Description("备注")]
        public string Remark { get; set; } // Remark

        // Reverse navigation
		[JsonIgnore]
        public virtual ICollection<T_EXE_TaskWorkPublish_TaskWork> T_EXE_TaskWorkPublish_TaskWork { get { onT_EXE_TaskWorkPublish_TaskWorkGetting(); return _T_EXE_TaskWorkPublish_TaskWork;} }
		private ICollection<T_EXE_TaskWorkPublish_TaskWork> _T_EXE_TaskWorkPublish_TaskWork;
		partial void onT_EXE_TaskWorkPublish_TaskWorkGetting();


        public T_EXE_TaskWorkPublish()
        {
            _T_EXE_TaskWorkPublish_TaskWork = new List<T_EXE_TaskWorkPublish_TaskWork>();
        }
    }

	/// <summary>卷册列表</summary>	
	[Description("卷册列表")]
    public partial class T_EXE_TaskWorkPublish_TaskWork : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string T_EXE_TaskWorkPublishID { get; set; } // T_EXE_TaskWorkPublishID
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string IsReleased { get; set; } // IsReleased
		/// <summary>专业</summary>	
		[Description("专业")]
        public string Major { get; set; } // Major
		/// <summary>卷册编号</summary>	
		[Description("卷册编号")]
        public string Code { get; set; } // Code
		/// <summary>卷册名称</summary>	
		[Description("卷册名称")]
        public string Name { get; set; } // Name
		/// <summary>定额工时</summary>	
		[Description("定额工时")]
        public decimal? Workload { get; set; } // Workload
		/// <summary>计划完成日期</summary>	
		[Description("计划完成日期")]
        public DateTime? PlanEndDate { get; set; } // PlanEndDate
		/// <summary>角色分配</summary>	
		[Description("角色分配")]
        public string RoleRate { get; set; } // RoleRate
		/// <summary>卷册ID</summary>	
		[Description("卷册ID")]
        public string TaskWorkID { get; set; } // TaskWorkID
		/// <summary>IsChanged</summary>	
		[Description("IsChanged")]
        public string IsChanged { get; set; } // IsChanged
		/// <summary>阶段</summary>	
		[Description("阶段")]
        public string Phase { get; set; } // Phase

        // Foreign keys
		[JsonIgnore]
        public virtual T_EXE_TaskWorkPublish T_EXE_TaskWorkPublish { get; set; } //  T_EXE_TaskWorkPublishID - FK_T_EXE_TaskWorkPublish_TaskWork_T_EXE_TaskWorkPublish
    }

	/// <summary>卷册工时结算单</summary>	
	[Description("卷册工时结算单")]
    public partial class T_EXE_TaskWorkSettlement : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? ModifyDate { get; set; } // ModifyDate
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get; set; } // CreateUserID
		/// <summary></summary>	
		[Description("")]
        public string CreateUser { get; set; } // CreateUser
		/// <summary></summary>	
		[Description("")]
        public string ModifyUserID { get; set; } // ModifyUserID
		/// <summary></summary>	
		[Description("")]
        public string ModifyUser { get; set; } // ModifyUser
		/// <summary></summary>	
		[Description("")]
        public string OrgID { get; set; } // OrgID
		/// <summary></summary>	
		[Description("")]
        public string CompanyID { get; set; } // CompanyID
		/// <summary></summary>	
		[Description("")]
        public string FlowPhase { get; set; } // FlowPhase
		/// <summary></summary>	
		[Description("")]
        public string FlowInfo { get; set; } // FlowInfo
		/// <summary></summary>	
		[Description("")]
        public string StepName { get; set; } // StepName
		/// <summary>项目名称</summary>	
		[Description("项目名称")]
        public string ProjectInfo { get; set; } // ProjectInfo
		/// <summary>项目名称名称</summary>	
		[Description("项目名称名称")]
        public string ProjectInfoName { get; set; } // ProjectInfoName
		/// <summary>项目编号</summary>	
		[Description("项目编号")]
        public string ProjectCode { get; set; } // ProjectCode
		/// <summary>项目负责人</summary>	
		[Description("项目负责人")]
        public string ProjectManager { get; set; } // ProjectManager
		/// <summary>项目负责人名称</summary>	
		[Description("项目负责人名称")]
        public string ProjectManagerName { get; set; } // ProjectManagerName
		/// <summary>设计阶段</summary>	
		[Description("设计阶段")]
        public string Phase { get; set; } // Phase
		/// <summary>专业</summary>	
		[Description("专业")]
        public string Major { get; set; } // Major
		/// <summary>归属部门</summary>	
		[Description("归属部门")]
        public string BelongDept { get; set; } // BelongDept
		/// <summary>归属部门名称</summary>	
		[Description("归属部门名称")]
        public string BelongDeptName { get; set; } // BelongDeptName
		/// <summary>归属日期</summary>	
		[Description("归属日期")]
        public DateTime? BelongDate { get; set; } // BelongDate
		/// <summary>项目负责人审批</summary>	
		[Description("项目负责人审批")]
        public string ProjectManagerApprove { get; set; } // ProjectManagerApprove
		/// <summary>备注</summary>	
		[Description("备注")]
        public string Remark { get; set; } // Remark
		/// <summary>本次结算汇总</summary>	
		[Description("本次结算汇总")]
        public decimal? SummaySettlement { get; set; } // SummaySettlement

        // Reverse navigation
		[JsonIgnore]
        public virtual ICollection<T_EXE_TaskWorkSettlement_TaskWorkList> T_EXE_TaskWorkSettlement_TaskWorkList { get { onT_EXE_TaskWorkSettlement_TaskWorkListGetting(); return _T_EXE_TaskWorkSettlement_TaskWorkList;} }
		private ICollection<T_EXE_TaskWorkSettlement_TaskWorkList> _T_EXE_TaskWorkSettlement_TaskWorkList;
		partial void onT_EXE_TaskWorkSettlement_TaskWorkListGetting();


        public T_EXE_TaskWorkSettlement()
        {
            _T_EXE_TaskWorkSettlement_TaskWorkList = new List<T_EXE_TaskWorkSettlement_TaskWorkList>();
        }
    }

	/// <summary>卷册信息</summary>	
	[Description("卷册信息")]
    public partial class T_EXE_TaskWorkSettlement_TaskWorkList : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string T_EXE_TaskWorkSettlementID { get; set; } // T_EXE_TaskWorkSettlementID
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string IsReleased { get; set; } // IsReleased
		/// <summary>名称</summary>	
		[Description("名称")]
        public string Name { get; set; } // Name
		/// <summary>编号</summary>	
		[Description("编号")]
        public string Code { get; set; } // Code
		/// <summary>额定工时</summary>	
		[Description("额定工时")]
        public decimal? Workload { get; set; } // Workload
		/// <summary>已结算</summary>	
		[Description("已结算")]
        public decimal? SummarySettlement { get; set; } // SummarySettlement
		/// <summary>本次结算</summary>	
		[Description("本次结算")]
        public decimal? Settlement { get; set; } // Settlement
		/// <summary>BudgetID</summary>	
		[Description("BudgetID")]
        public string BudgetID { get; set; } // BudgetID
		/// <summary>专业</summary>	
		[Description("专业")]
        public string Major { get; set; } // Major

        // Foreign keys
		[JsonIgnore]
        public virtual T_EXE_TaskWorkSettlement T_EXE_TaskWorkSettlement { get; set; } //  T_EXE_TaskWorkSettlementID - FK_T_EXE_TaskWorkSettlement_TaskWorkList_T_EXE_TaskWorkSettlement
    }

	/// <summary>规划设计输入评审单</summary>	
	[Description("规划设计输入评审单")]
    public partial class T_SC_DesignInput : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? ModifyDate { get; set; } // ModifyDate
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get; set; } // CreateUserID
		/// <summary></summary>	
		[Description("")]
        public string CreateUser { get; set; } // CreateUser
		/// <summary></summary>	
		[Description("")]
        public string ModifyUserID { get; set; } // ModifyUserID
		/// <summary></summary>	
		[Description("")]
        public string ModifyUser { get; set; } // ModifyUser
		/// <summary></summary>	
		[Description("")]
        public string OrgID { get; set; } // OrgID
		/// <summary></summary>	
		[Description("")]
        public string CompanyID { get; set; } // CompanyID
		/// <summary></summary>	
		[Description("")]
        public string FlowPhase { get; set; } // FlowPhase
		/// <summary></summary>	
		[Description("")]
        public string FlowInfo { get; set; } // FlowInfo
		/// <summary></summary>	
		[Description("")]
        public string StepName { get; set; } // StepName
		/// <summary>项目ID</summary>	
		[Description("项目ID")]
        public string ProjectInfoID { get; set; } // ProjectInfoID
		/// <summary>项目名称</summary>	
		[Description("项目名称")]
        public string ProjectName { get; set; } // ProjectName
		/// <summary>项目编号</summary>	
		[Description("项目编号")]
        public string ProjectCode { get; set; } // ProjectCode
		/// <summary>项目负责人</summary>	
		[Description("项目负责人")]
        public string ProjectManager { get; set; } // ProjectManager
		/// <summary>项目负责人名称</summary>	
		[Description("项目负责人名称")]
        public string ProjectManagerName { get; set; } // ProjectManagerName
		/// <summary>责任部门</summary>	
		[Description("责任部门")]
        public string ChargeDept { get; set; } // ChargeDept
		/// <summary>责任部门名称</summary>	
		[Description("责任部门名称")]
        public string ChargeDeptName { get; set; } // ChargeDeptName
		/// <summary>业务类型</summary>	
		[Description("业务类型")]
        public string ProjectClass { get; set; } // ProjectClass
		/// <summary>项目阶段</summary>	
		[Description("项目阶段")]
        public string PhaseValue { get; set; } // PhaseValue
		/// <summary>计划开始时间</summary>	
		[Description("计划开始时间")]
        public DateTime? PlanStartDate { get; set; } // PlanStartDate
		/// <summary>计划结束时间</summary>	
		[Description("计划结束时间")]
        public DateTime? PlanFinishDate { get; set; } // PlanFinishDate
		/// <summary>设计输入子表</summary>	
		[Description("设计输入子表")]
        public string DesignInputList { get; set; } // DesignInputList
		/// <summary>版本号</summary>	
		[Description("版本号")]
        public string VersionNumber { get; set; } // VersionNumber
		/// <summary>分类</summary>	
		[Description("分类")]
        public string Catagory { get; set; } // Catagory
		/// <summary>编号</summary>	
		[Description("编号")]
        public string SerialNumber { get; set; } // SerialNumber
		/// <summary>输入评审结果</summary>	
		[Description("输入评审结果")]
        public string SRPSJG { get; set; } // SRPSJG
		/// <summary>填表人</summary>	
		[Description("填表人")]
        public string TBR { get; set; } // TBR
		/// <summary>填表人名称</summary>	
		[Description("填表人名称")]
        public string TBRName { get; set; } // TBRName

        // Reverse navigation
		[JsonIgnore]
        public virtual ICollection<T_SC_DesignInput_DesignInputList> T_SC_DesignInput_DesignInputList { get { onT_SC_DesignInput_DesignInputListGetting(); return _T_SC_DesignInput_DesignInputList;} }
		private ICollection<T_SC_DesignInput_DesignInputList> _T_SC_DesignInput_DesignInputList;
		partial void onT_SC_DesignInput_DesignInputListGetting();


        public T_SC_DesignInput()
        {
            _T_SC_DesignInput_DesignInputList = new List<T_SC_DesignInput_DesignInputList>();
        }
    }

	/// <summary>设计输入子表</summary>	
	[Description("设计输入子表")]
    public partial class T_SC_DesignInput_DesignInputList : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string T_SC_DesignInputID { get; set; } // T_SC_DesignInputID
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string IsReleased { get; set; } // IsReleased
		/// <summary>资料分类</summary>	
		[Description("资料分类")]
        public string InputType { get; set; } // InputType
		/// <summary>资料名称</summary>	
		[Description("资料名称")]
        public string InfoName { get; set; } // InfoName
		/// <summary>文件名称</summary>	
		[Description("文件名称")]
        public string Name { get; set; } // Name
		/// <summary>上传日期</summary>	
		[Description("上传日期")]
        public string CreateDate { get; set; } // CreateDate
		/// <summary>上传人</summary>	
		[Description("上传人")]
        public string CreateUser { get; set; } // CreateUser
		/// <summary>InputInfoID</summary>	
		[Description("InputInfoID")]
        public string InputInfoID { get; set; } // InputInfoID
		/// <summary>DocID</summary>	
		[Description("DocID")]
        public string DocID { get; set; } // DocID
		/// <summary>Catagory</summary>	
		[Description("Catagory")]
        public string Catagory { get; set; } // Catagory
		/// <summary>附件</summary>	
		[Description("附件")]
        public string Files { get; set; } // Files
		/// <summary>提供单位</summary>	
		[Description("提供单位")]
        public string TGDW { get; set; } // TGDW
		/// <summary>份数</summary>	
		[Description("份数")]
        public string FS { get; set; } // FS
		/// <summary>页数</summary>	
		[Description("页数")]
        public string YS { get; set; } // YS
		/// <summary>验证名称</summary>	
		[Description("验证名称")]
        public string YZMC { get; set; } // YZMC
		/// <summary>验证数量</summary>	
		[Description("验证数量")]
        public string YZSL { get; set; } // YZSL
		/// <summary>验证页码</summary>	
		[Description("验证页码")]
        public string YZYM { get; set; } // YZYM
		/// <summary>验证人</summary>	
		[Description("验证人")]
        public string YZR { get; set; } // YZR
		/// <summary>备注</summary>	
		[Description("备注")]
        public string BZ { get; set; } // BZ

        // Foreign keys
		[JsonIgnore]
        public virtual T_SC_DesignInput T_SC_DesignInput { get; set; } //  T_SC_DesignInputID - FK_T_SC_DesignInput_DesignInputList_T_SC_DesignInput
    }

	/// <summary>设计大纲编制</summary>	
	[Description("设计大纲编制")]
    public partial class T_SC_DesignOutline : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? ModifyDate { get; set; } // ModifyDate
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get; set; } // CreateUserID
		/// <summary></summary>	
		[Description("")]
        public string CreateUser { get; set; } // CreateUser
		/// <summary></summary>	
		[Description("")]
        public string ModifyUserID { get; set; } // ModifyUserID
		/// <summary></summary>	
		[Description("")]
        public string ModifyUser { get; set; } // ModifyUser
		/// <summary></summary>	
		[Description("")]
        public string OrgID { get; set; } // OrgID
		/// <summary></summary>	
		[Description("")]
        public string CompanyID { get; set; } // CompanyID
		/// <summary></summary>	
		[Description("")]
        public string FlowPhase { get; set; } // FlowPhase
		/// <summary></summary>	
		[Description("")]
        public string FlowInfo { get; set; } // FlowInfo
		/// <summary></summary>	
		[Description("")]
        public string StepName { get; set; } // StepName
		/// <summary>项目编号</summary>	
		[Description("项目编号")]
        public string ItemNumber { get; set; } // ItemNumber
		/// <summary>编号</summary>	
		[Description("编号")]
        public string SerialNumber { get; set; } // SerialNumber
		/// <summary>项目名称</summary>	
		[Description("项目名称")]
        public string ProjectName { get; set; } // ProjectName
		/// <summary>项目名称名称</summary>	
		[Description("项目名称名称")]
        public string ProjectNameName { get; set; } // ProjectNameName
		/// <summary>设计阶段</summary>	
		[Description("设计阶段")]
        public string DesignPhase { get; set; } // DesignPhase
		/// <summary>项目类型</summary>	
		[Description("项目类型")]
        public string ProjectType { get; set; } // ProjectType
		/// <summary>提交人</summary>	
		[Description("提交人")]
        public string Author { get; set; } // Author
		/// <summary>提交人名称</summary>	
		[Description("提交人名称")]
        public string AuthorName { get; set; } // AuthorName
		/// <summary>提交日期</summary>	
		[Description("提交日期")]
        public DateTime? SubmissionDate { get; set; } // SubmissionDate
		/// <summary>附件</summary>	
		[Description("附件")]
        public string attachment { get; set; } // attachment
		/// <summary>备注</summary>	
		[Description("备注")]
        public string Remark { get; set; } // Remark
		/// <summary>审批签字</summary>	
		[Description("审批签字")]
        public string ApproveSign { get; set; } // ApproveSign
    }

	/// <summary>建筑子项进度计划表</summary>	
	[Description("建筑子项进度计划表")]
    public partial class T_SC_DesignPlan : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? ModifyDate { get; set; } // ModifyDate
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get; set; } // CreateUserID
		/// <summary></summary>	
		[Description("")]
        public string CreateUser { get; set; } // CreateUser
		/// <summary></summary>	
		[Description("")]
        public string ModifyUserID { get; set; } // ModifyUserID
		/// <summary></summary>	
		[Description("")]
        public string ModifyUser { get; set; } // ModifyUser
		/// <summary></summary>	
		[Description("")]
        public string OrgID { get; set; } // OrgID
		/// <summary></summary>	
		[Description("")]
        public string CompanyID { get; set; } // CompanyID
		/// <summary></summary>	
		[Description("")]
        public string FlowPhase { get; set; } // FlowPhase
		/// <summary></summary>	
		[Description("")]
        public string FlowInfo { get; set; } // FlowInfo
		/// <summary></summary>	
		[Description("")]
        public string StepName { get; set; } // StepName
		/// <summary>表单编号</summary>	
		[Description("表单编号")]
        public string SerialNumber { get; set; } // SerialNumber
		/// <summary>项目名称</summary>	
		[Description("项目名称")]
        public string ProjectName { get; set; } // ProjectName
		/// <summary>设计号</summary>	
		[Description("设计号")]
        public string ProjectCode { get; set; } // ProjectCode
		/// <summary>子项名称</summary>	
		[Description("子项名称")]
        public string SubProjectName { get; set; } // SubProjectName
		/// <summary>设计阶段</summary>	
		[Description("设计阶段")]
        public string PhaseValue { get; set; } // PhaseValue
		/// <summary>责任部门</summary>	
		[Description("责任部门")]
        public string ChargeDept { get; set; } // ChargeDept
		/// <summary>责任部门名称</summary>	
		[Description("责任部门名称")]
        public string ChargeDeptName { get; set; } // ChargeDeptName
		/// <summary>业务类型</summary>	
		[Description("业务类型")]
        public string ProjectType { get; set; } // ProjectType
		/// <summary>计划开始日期</summary>	
		[Description("计划开始日期")]
        public DateTime? PlanStartDate { get; set; } // PlanStartDate
		/// <summary>计划完成日期</summary>	
		[Description("计划完成日期")]
        public DateTime? PlanFinishDate { get; set; } // PlanFinishDate
		/// <summary>里程碑</summary>	
		[Description("里程碑")]
        public string MilestoneList { get; set; } // MilestoneList
		/// <summary>项目ID</summary>	
		[Description("项目ID")]
        public string ProjectInfoID { get; set; } // ProjectInfoID
		/// <summary>版本号</summary>	
		[Description("版本号")]
        public string VersionNumber { get; set; } // VersionNumber
		/// <summary>设计阶段名称</summary>	
		[Description("设计阶段名称")]
        public string PhaseName { get; set; } // PhaseName
		/// <summary>子项ID</summary>	
		[Description("子项ID")]
        public string SubProjectWBSID { get; set; } // SubProjectWBSID
		/// <summary>项目负责人ID</summary>	
		[Description("项目负责人ID")]
        public string ChargeUser { get; set; } // ChargeUser
		/// <summary>项目负责人意见</summary>	
		[Description("项目负责人意见")]
        public string ProjectLeader { get; set; } // ProjectLeader
		/// <summary>所长意见</summary>	
		[Description("所长意见")]
        public string Director { get; set; } // Director

        // Reverse navigation
		[JsonIgnore]
        public virtual ICollection<T_SC_DesignPlan_MilestoneList> T_SC_DesignPlan_MilestoneList { get { onT_SC_DesignPlan_MilestoneListGetting(); return _T_SC_DesignPlan_MilestoneList;} }
		private ICollection<T_SC_DesignPlan_MilestoneList> _T_SC_DesignPlan_MilestoneList;
		partial void onT_SC_DesignPlan_MilestoneListGetting();


        public T_SC_DesignPlan()
        {
            _T_SC_DesignPlan_MilestoneList = new List<T_SC_DesignPlan_MilestoneList>();
        }
    }

	/// <summary>里程碑</summary>	
	[Description("里程碑")]
    public partial class T_SC_DesignPlan_MilestoneList : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string T_SC_DesignPlanID { get; set; } // T_SC_DesignPlanID
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string IsReleased { get; set; } // IsReleased
		/// <summary>名称</summary>	
		[Description("名称")]
        public string Name { get; set; } // Name
		/// <summary>专业</summary>	
		[Description("专业")]
        public string Major { get; set; } // Major
		/// <summary>计划完成日期</summary>	
		[Description("计划完成日期")]
        public DateTime? PlanEndDate { get; set; } // PlanEndDate
		/// <summary>权重（%）</summary>	
		[Description("权重（%）")]
        public decimal? Weight { get; set; } // Weight
		/// <summary>备注</summary>	
		[Description("备注")]
        public string Remark { get; set; } // Remark
		/// <summary>里程碑类别</summary>	
		[Description("里程碑类别")]
        public string MileStoneType { get; set; } // MileStoneType
		/// <summary>编号</summary>	
		[Description("编号")]
        public string Code { get; set; } // Code
		/// <summary>MileStoneID</summary>	
		[Description("MileStoneID")]
        public string MileStoneID { get; set; } // MileStoneID
		/// <summary>提出专业</summary>	
		[Description("提出专业")]
        public string OutMajor { get; set; } // OutMajor
		/// <summary>接收专业</summary>	
		[Description("接收专业")]
        public string InMajor { get; set; } // InMajor
		/// <summary>TemplateID</summary>	
		[Description("TemplateID")]
        public string TemplateID { get; set; } // TemplateID

        // Foreign keys
		[JsonIgnore]
        public virtual T_SC_DesignPlan T_SC_DesignPlan { get; set; } //  T_SC_DesignPlanID - FK_T_SC_DesignPlan_MilestoneList_T_SC_DesignPlan
    }

	/// <summary>石化院进度计划</summary>	
	[Description("石化院进度计划")]
    public partial class T_SC_DesignPlanPetrifaction : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? ModifyDate { get; set; } // ModifyDate
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get; set; } // CreateUserID
		/// <summary></summary>	
		[Description("")]
        public string CreateUser { get; set; } // CreateUser
		/// <summary></summary>	
		[Description("")]
        public string ModifyUserID { get; set; } // ModifyUserID
		/// <summary></summary>	
		[Description("")]
        public string ModifyUser { get; set; } // ModifyUser
		/// <summary></summary>	
		[Description("")]
        public string OrgID { get; set; } // OrgID
		/// <summary></summary>	
		[Description("")]
        public string CompanyID { get; set; } // CompanyID
		/// <summary></summary>	
		[Description("")]
        public string FlowPhase { get; set; } // FlowPhase
		/// <summary></summary>	
		[Description("")]
        public string FlowInfo { get; set; } // FlowInfo
		/// <summary></summary>	
		[Description("")]
        public string StepName { get; set; } // StepName
		/// <summary>表单编号</summary>	
		[Description("表单编号")]
        public string SerialNumber { get; set; } // SerialNumber
		/// <summary>项目名称</summary>	
		[Description("项目名称")]
        public string ProjectName { get; set; } // ProjectName
		/// <summary>设计号</summary>	
		[Description("设计号")]
        public string ProjectCode { get; set; } // ProjectCode
		/// <summary>责任部门</summary>	
		[Description("责任部门")]
        public string ChargeDept { get; set; } // ChargeDept
		/// <summary>责任部门名称</summary>	
		[Description("责任部门名称")]
        public string ChargeDeptName { get; set; } // ChargeDeptName
		/// <summary>业务类型</summary>	
		[Description("业务类型")]
        public string ProjectType { get; set; } // ProjectType
		/// <summary>计划开始日期</summary>	
		[Description("计划开始日期")]
        public DateTime? PlanStartDate { get; set; } // PlanStartDate
		/// <summary>计划完成日期</summary>	
		[Description("计划完成日期")]
        public DateTime? PlanFinishDate { get; set; } // PlanFinishDate
		/// <summary>项目ID</summary>	
		[Description("项目ID")]
        public string ProjectInfoID { get; set; } // ProjectInfoID
		/// <summary>版本号</summary>	
		[Description("版本号")]
        public string VersionNumber { get; set; } // VersionNumber
		/// <summary>项目负责人ID</summary>	
		[Description("项目负责人ID")]
        public string ChargeUser { get; set; } // ChargeUser
		/// <summary>项目负责人意见</summary>	
		[Description("项目负责人意见")]
        public string ProjectLeader { get; set; } // ProjectLeader

        // Reverse navigation
		[JsonIgnore]
        public virtual ICollection<T_SC_DesignPlanPetrifaction_MilestoneList> T_SC_DesignPlanPetrifaction_MilestoneList { get { onT_SC_DesignPlanPetrifaction_MilestoneListGetting(); return _T_SC_DesignPlanPetrifaction_MilestoneList;} }
		private ICollection<T_SC_DesignPlanPetrifaction_MilestoneList> _T_SC_DesignPlanPetrifaction_MilestoneList;
		partial void onT_SC_DesignPlanPetrifaction_MilestoneListGetting();


        public T_SC_DesignPlanPetrifaction()
        {
            _T_SC_DesignPlanPetrifaction_MilestoneList = new List<T_SC_DesignPlanPetrifaction_MilestoneList>();
        }
    }

	/// <summary>里程碑</summary>	
	[Description("里程碑")]
    public partial class T_SC_DesignPlanPetrifaction_MilestoneList : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string T_SC_DesignPlanPetrifactionID { get; set; } // T_SC_DesignPlanPetrifactionID
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string IsReleased { get; set; } // IsReleased
		/// <summary>名称</summary>	
		[Description("名称")]
        public string Name { get; set; } // Name
		/// <summary>专业</summary>	
		[Description("专业")]
        public string Major { get; set; } // Major
		/// <summary>计划完成日期</summary>	
		[Description("计划完成日期")]
        public DateTime? PlanEndDate { get; set; } // PlanEndDate
		/// <summary>权重（%）</summary>	
		[Description("权重（%）")]
        public decimal? Weight { get; set; } // Weight
		/// <summary>备注</summary>	
		[Description("备注")]
        public string Remark { get; set; } // Remark
		/// <summary>里程碑类别</summary>	
		[Description("里程碑类别")]
        public string MileStoneType { get; set; } // MileStoneType
		/// <summary>编号</summary>	
		[Description("编号")]
        public string Code { get; set; } // Code
		/// <summary>MileStoneID</summary>	
		[Description("MileStoneID")]
        public string MileStoneID { get; set; } // MileStoneID
		/// <summary>提出专业</summary>	
		[Description("提出专业")]
        public string OutMajor { get; set; } // OutMajor
		/// <summary>接收专业</summary>	
		[Description("接收专业")]
        public string InMajor { get; set; } // InMajor
		/// <summary>TemplateID</summary>	
		[Description("TemplateID")]
        public string TemplateID { get; set; } // TemplateID

        // Foreign keys
		[JsonIgnore]
        public virtual T_SC_DesignPlanPetrifaction T_SC_DesignPlanPetrifaction { get; set; } //  T_SC_DesignPlanPetrifactionID - FK_T_SC_DesignPlanPetrifaction_MilestoneList_T_SC_DesignPlanPetrifaction
    }

	/// <summary>电力院标准策划表</summary>	
	[Description("电力院标准策划表")]
    public partial class T_SC_ElectricalPowerProjectScheme : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? ModifyDate { get; set; } // ModifyDate
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get; set; } // CreateUserID
		/// <summary></summary>	
		[Description("")]
        public string CreateUser { get; set; } // CreateUser
		/// <summary></summary>	
		[Description("")]
        public string ModifyUserID { get; set; } // ModifyUserID
		/// <summary></summary>	
		[Description("")]
        public string ModifyUser { get; set; } // ModifyUser
		/// <summary></summary>	
		[Description("")]
        public string OrgID { get; set; } // OrgID
		/// <summary></summary>	
		[Description("")]
        public string CompanyID { get; set; } // CompanyID
		/// <summary></summary>	
		[Description("")]
        public string FlowPhase { get; set; } // FlowPhase
		/// <summary></summary>	
		[Description("")]
        public string FlowInfo { get; set; } // FlowInfo
		/// <summary></summary>	
		[Description("")]
        public string StepName { get; set; } // StepName
		/// <summary>表单编号</summary>	
		[Description("表单编号")]
        public string SerialNumber { get; set; } // SerialNumber
		/// <summary>项目名称</summary>	
		[Description("项目名称")]
        public string Name { get; set; } // Name
		/// <summary>项目编号</summary>	
		[Description("项目编号")]
        public string Code { get; set; } // Code
		/// <summary>设计阶段</summary>	
		[Description("设计阶段")]
        public string PhaseValue { get; set; } // PhaseValue
		/// <summary>项目阶段</summary>	
		[Description("项目阶段")]
        public string PhaseName { get; set; } // PhaseName
		/// <summary>责任部门</summary>	
		[Description("责任部门")]
        public string ChargeDept { get; set; } // ChargeDept
		/// <summary>责任部门名称</summary>	
		[Description("责任部门名称")]
        public string ChargeDeptName { get; set; } // ChargeDeptName
		/// <summary>业务类型</summary>	
		[Description("业务类型")]
        public string ProjectClass { get; set; } // ProjectClass
		/// <summary>建筑面积</summary>	
		[Description("建筑面积")]
        public double? BuildArea { get; set; } // BuildArea
		/// <summary>计划开始日期</summary>	
		[Description("计划开始日期")]
        public DateTime? PlanStartDate { get; set; } // PlanStartDate
		/// <summary>计划结束日期</summary>	
		[Description("计划结束日期")]
        public DateTime? PlanFinishDate { get; set; } // PlanFinishDate
		/// <summary>下达工时</summary>	
		[Description("下达工时")]
        public decimal? ProjectWorkload { get; set; } // ProjectWorkload
		/// <summary>参与专业</summary>	
		[Description("参与专业")]
        public string Major { get; set; } // Major
		/// <summary>项目负责人</summary>	
		[Description("项目负责人")]
        public string ChargeUser { get; set; } // ChargeUser
		/// <summary>项目负责人名称</summary>	
		[Description("项目负责人名称")]
        public string ChargeUserName { get; set; } // ChargeUserName
		/// <summary>专业人员信息</summary>	
		[Description("专业人员信息")]
        public string MajorList { get; set; } // MajorList
		/// <summary>项目ID</summary>	
		[Description("项目ID")]
        public string ProjectInfoID { get; set; } // ProjectInfoID
		/// <summary>版本号</summary>	
		[Description("版本号")]
        public string VersionNumber { get; set; } // VersionNumber
		/// <summary>相关附件</summary>	
		[Description("相关附件")]
        public string Attachments { get; set; } // Attachments
		/// <summary>备注</summary>	
		[Description("备注")]
        public string Remark { get; set; } // Remark
		/// <summary>项目规模</summary>	
		[Description("项目规模")]
        public string ProjectLevel { get; set; } // ProjectLevel
		/// <summary>设计要求</summary>	
		[Description("设计要求")]
        public string DesignRequired { get; set; } // DesignRequired
		/// <summary>输入评审</summary>	
		[Description("输入评审")]
        public string DesignInput { get; set; } // DesignInput
		/// <summary>项目负责人意见</summary>	
		[Description("项目负责人意见")]
        public string ProjectLeader { get; set; } // ProjectLeader
		/// <summary>所长意见</summary>	
		[Description("所长意见")]
        public string Director { get; set; } // Director
		/// <summary>客户</summary>	
		[Description("客户")]
        public string CustomerInfo { get; set; } // CustomerInfo
		/// <summary>客户名称</summary>	
		[Description("客户名称")]
        public string CustomerInfoName { get; set; } // CustomerInfoName
		/// <summary>预留工时</summary>	
		[Description("预留工时")]
        public decimal? ReserveWorkload { get; set; } // ReserveWorkload
		/// <summary>进度计划</summary>	
		[Description("进度计划")]
        public string MileStoneList { get; set; } // MileStoneList
		/// <summary>管理工时</summary>	
		[Description("管理工时")]
        public string ManageWorkloadList { get; set; } // ManageWorkloadList
		/// <summary>管理工时总计</summary>	
		[Description("管理工时总计")]
        public decimal? ManageWorkloadSum { get; set; } // ManageWorkloadSum
		/// <summary>专业总工时</summary>	
		[Description("专业总工时")]
        public decimal? EnableMajorSum { get; set; } // EnableMajorSum
		/// <summary>填报专业工时总计</summary>	
		[Description("填报专业工时总计")]
        public decimal? MajorSum { get; set; } // MajorSum
		/// <summary>卷册策划</summary>	
		[Description("卷册策划")]
        public string TaskWorkList { get; set; } // TaskWorkList
		/// <summary>室主任</summary>	
		[Description("室主任")]
        public string DeptLeader { get; set; } // DeptLeader
		/// <summary>室主任名称</summary>	
		[Description("室主任名称")]
        public string DeptLeaderName { get; set; } // DeptLeaderName

        // Reverse navigation
		[JsonIgnore]
        public virtual ICollection<T_SC_ElectricalPowerProjectScheme_MajorList> T_SC_ElectricalPowerProjectScheme_MajorList { get { onT_SC_ElectricalPowerProjectScheme_MajorListGetting(); return _T_SC_ElectricalPowerProjectScheme_MajorList;} }
		private ICollection<T_SC_ElectricalPowerProjectScheme_MajorList> _T_SC_ElectricalPowerProjectScheme_MajorList;
		partial void onT_SC_ElectricalPowerProjectScheme_MajorListGetting();

		[JsonIgnore]
        public virtual ICollection<T_SC_ElectricalPowerProjectScheme_ManageWorkloadList> T_SC_ElectricalPowerProjectScheme_ManageWorkloadList { get { onT_SC_ElectricalPowerProjectScheme_ManageWorkloadListGetting(); return _T_SC_ElectricalPowerProjectScheme_ManageWorkloadList;} }
		private ICollection<T_SC_ElectricalPowerProjectScheme_ManageWorkloadList> _T_SC_ElectricalPowerProjectScheme_ManageWorkloadList;
		partial void onT_SC_ElectricalPowerProjectScheme_ManageWorkloadListGetting();

		[JsonIgnore]
        public virtual ICollection<T_SC_ElectricalPowerProjectScheme_MileStoneList> T_SC_ElectricalPowerProjectScheme_MileStoneList { get { onT_SC_ElectricalPowerProjectScheme_MileStoneListGetting(); return _T_SC_ElectricalPowerProjectScheme_MileStoneList;} }
		private ICollection<T_SC_ElectricalPowerProjectScheme_MileStoneList> _T_SC_ElectricalPowerProjectScheme_MileStoneList;
		partial void onT_SC_ElectricalPowerProjectScheme_MileStoneListGetting();

		[JsonIgnore]
        public virtual ICollection<T_SC_ElectricalPowerProjectScheme_TaskWorkList> T_SC_ElectricalPowerProjectScheme_TaskWorkList { get { onT_SC_ElectricalPowerProjectScheme_TaskWorkListGetting(); return _T_SC_ElectricalPowerProjectScheme_TaskWorkList;} }
		private ICollection<T_SC_ElectricalPowerProjectScheme_TaskWorkList> _T_SC_ElectricalPowerProjectScheme_TaskWorkList;
		partial void onT_SC_ElectricalPowerProjectScheme_TaskWorkListGetting();


        public T_SC_ElectricalPowerProjectScheme()
        {
            _T_SC_ElectricalPowerProjectScheme_MajorList = new List<T_SC_ElectricalPowerProjectScheme_MajorList>();
            _T_SC_ElectricalPowerProjectScheme_ManageWorkloadList = new List<T_SC_ElectricalPowerProjectScheme_ManageWorkloadList>();
            _T_SC_ElectricalPowerProjectScheme_MileStoneList = new List<T_SC_ElectricalPowerProjectScheme_MileStoneList>();
            _T_SC_ElectricalPowerProjectScheme_TaskWorkList = new List<T_SC_ElectricalPowerProjectScheme_TaskWorkList>();
        }
    }

	/// <summary>专业人员信息</summary>	
	[Description("专业人员信息")]
    public partial class T_SC_ElectricalPowerProjectScheme_MajorList : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string T_SC_ElectricalPowerProjectSchemeID { get; set; } // T_SC_ElectricalPowerProjectSchemeID
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string IsReleased { get; set; } // IsReleased
		/// <summary>专业名称</summary>	
		[Description("专业名称")]
        public string MajorName { get; set; } // MajorName
		/// <summary>专业编码</summary>	
		[Description("专业编码")]
        public string MajorCode { get; set; } // MajorCode
		/// <summary>主设人</summary>	
		[Description("主设人")]
        public string MajorPrinciple { get; set; } // MajorPrinciple
		/// <summary>主设人名称</summary>	
		[Description("主设人名称")]
        public string MajorPrincipleName { get; set; } // MajorPrincipleName
		/// <summary>设计人</summary>	
		[Description("设计人")]
        public string Designer { get; set; } // Designer
		/// <summary>设计人名称</summary>	
		[Description("设计人名称")]
        public string DesignerName { get; set; } // DesignerName
		/// <summary>校对人</summary>	
		[Description("校对人")]
        public string Collactor { get; set; } // Collactor
		/// <summary>校对人名称</summary>	
		[Description("校对人名称")]
        public string CollactorName { get; set; } // CollactorName
		/// <summary>审核人</summary>	
		[Description("审核人")]
        public string Auditor { get; set; } // Auditor
		/// <summary>审核人名称</summary>	
		[Description("审核人名称")]
        public string AuditorName { get; set; } // AuditorName
		/// <summary>审定人</summary>	
		[Description("审定人")]
        public string Approver { get; set; } // Approver
		/// <summary>审定人名称</summary>	
		[Description("审定人名称")]
        public string ApproverName { get; set; } // ApproverName
		/// <summary>下达工时.工时</summary>	
		[Description("下达工时.工时")]
        public decimal? MajorWorkload { get; set; } // MajorWorkload
		/// <summary>备注</summary>	
		[Description("备注")]
        public string Remark { get; set; } // Remark
		/// <summary>专业总工程师</summary>	
		[Description("专业总工程师")]
        public string MajorEngineer { get; set; } // MajorEngineer
		/// <summary>专业总工程师名称</summary>	
		[Description("专业总工程师名称")]
        public string MajorEngineerName { get; set; } // MajorEngineerName
		/// <summary>下达工时.比例%</summary>	
		[Description("下达工时.比例%")]
        public decimal? MajorWorkloadRate { get; set; } // MajorWorkloadRate
		/// <summary>下达工时.已分配</summary>	
		[Description("下达工时.已分配")]
        public decimal? SummaryBudgetQuantity { get; set; } // SummaryBudgetQuantity
		/// <summary>下达工时.已结算</summary>	
		[Description("下达工时.已结算")]
        public decimal? SummaryCostQuantity { get; set; } // SummaryCostQuantity
		/// <summary>部门室主任</summary>	
		[Description("部门室主任")]
        public string DeptLeader { get; set; } // DeptLeader
		/// <summary>部门室主任名称</summary>	
		[Description("部门室主任名称")]
        public string DeptLeaderName { get; set; } // DeptLeaderName

        // Foreign keys
		[JsonIgnore]
        public virtual T_SC_ElectricalPowerProjectScheme T_SC_ElectricalPowerProjectScheme { get; set; } //  T_SC_ElectricalPowerProjectSchemeID - FK_T_SC_ElectricalPowerProjectScheme_MajorList_T_SC_ElectricalPowerProjectScheme
    }

	/// <summary>管理工时</summary>	
	[Description("管理工时")]
    public partial class T_SC_ElectricalPowerProjectScheme_ManageWorkloadList : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string T_SC_ElectricalPowerProjectSchemeID { get; set; } // T_SC_ElectricalPowerProjectSchemeID
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string IsReleased { get; set; } // IsReleased
		/// <summary>名称</summary>	
		[Description("名称")]
        public string ItemName { get; set; } // ItemName
		/// <summary>编号</summary>	
		[Description("编号")]
        public string Item { get; set; } // Item
		/// <summary>下达工时.比例%</summary>	
		[Description("下达工时.比例%")]
        public decimal? ManageWorkloadRate { get; set; } // ManageWorkloadRate
		/// <summary>下达工时.工时</summary>	
		[Description("下达工时.工时")]
        public decimal? ManageWorkload { get; set; } // ManageWorkload
		/// <summary>下达工时.已结算</summary>	
		[Description("下达工时.已结算")]
        public decimal? SummaryCostQuantity { get; set; } // SummaryCostQuantity
		/// <summary>备注</summary>	
		[Description("备注")]
        public string Remark { get; set; } // Remark

        // Foreign keys
		[JsonIgnore]
        public virtual T_SC_ElectricalPowerProjectScheme T_SC_ElectricalPowerProjectScheme { get; set; } //  T_SC_ElectricalPowerProjectSchemeID - FK_T_SC_ElectricalPowerProjectScheme_ManageWorkloadList_T_SC_ElectricalPowerProjectScheme
    }

	/// <summary>进度计划</summary>	
	[Description("进度计划")]
    public partial class T_SC_ElectricalPowerProjectScheme_MileStoneList : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string T_SC_ElectricalPowerProjectSchemeID { get; set; } // T_SC_ElectricalPowerProjectSchemeID
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string IsReleased { get; set; } // IsReleased
		/// <summary>名称</summary>	
		[Description("名称")]
        public string Name { get; set; } // Name
		/// <summary>专业</summary>	
		[Description("专业")]
        public string Major { get; set; } // Major
		/// <summary>计划完成日期</summary>	
		[Description("计划完成日期")]
        public DateTime? PlanEndDate { get; set; } // PlanEndDate
		/// <summary>权重（%）</summary>	
		[Description("权重（%）")]
        public decimal? Weight { get; set; } // Weight
		/// <summary>备注</summary>	
		[Description("备注")]
        public string Remark { get; set; } // Remark
		/// <summary>MileStoneType</summary>	
		[Description("MileStoneType")]
        public string MileStoneType { get; set; } // MileStoneType
		/// <summary>Code</summary>	
		[Description("Code")]
        public string Code { get; set; } // Code
		/// <summary>TemplateID</summary>	
		[Description("TemplateID")]
        public string TemplateID { get; set; } // TemplateID
		/// <summary>OutMajor</summary>	
		[Description("OutMajor")]
        public string OutMajor { get; set; } // OutMajor
		/// <summary>InMajor</summary>	
		[Description("InMajor")]
        public string InMajor { get; set; } // InMajor

        // Foreign keys
		[JsonIgnore]
        public virtual T_SC_ElectricalPowerProjectScheme T_SC_ElectricalPowerProjectScheme { get; set; } //  T_SC_ElectricalPowerProjectSchemeID - FK_T_SC_ElectricalPowerProjectScheme_MileStoneList_T_SC_ElectricalPowerProjectScheme
    }

	/// <summary>卷册策划</summary>	
	[Description("卷册策划")]
    public partial class T_SC_ElectricalPowerProjectScheme_TaskWorkList : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string T_SC_ElectricalPowerProjectSchemeID { get; set; } // T_SC_ElectricalPowerProjectSchemeID
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string IsReleased { get; set; } // IsReleased
		/// <summary>卷册名称</summary>	
		[Description("卷册名称")]
        public string Name { get; set; } // Name
		/// <summary>编号</summary>	
		[Description("编号")]
        public string Code { get; set; } // Code
		/// <summary>专业</summary>	
		[Description("专业")]
        public string Major { get; set; } // Major
		/// <summary>阶段</summary>	
		[Description("阶段")]
        public string Phase { get; set; } // Phase
		/// <summary>备注</summary>	
		[Description("备注")]
        public string Remark { get; set; } // Remark
		/// <summary>TaskWorkID</summary>	
		[Description("TaskWorkID")]
        public string TaskWorkID { get; set; } // TaskWorkID
		/// <summary>卷名</summary>	
		[Description("卷名")]
        public string DossierName { get; set; } // DossierName
		/// <summary>卷号</summary>	
		[Description("卷号")]
        public string DossierCode { get; set; } // DossierCode

        // Foreign keys
		[JsonIgnore]
        public virtual T_SC_ElectricalPowerProjectScheme T_SC_ElectricalPowerProjectScheme { get; set; } //  T_SC_ElectricalPowerProjectSchemeID - FK_T_SC_ElectricalPowerProjectScheme_TaskWorkList_T_SC_ElectricalPowerProjectScheme
    }

	/// <summary>专业设计输入评审</summary>	
	[Description("专业设计输入评审")]
    public partial class T_SC_MajorDesignInput : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? ModifyDate { get; set; } // ModifyDate
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get; set; } // CreateUserID
		/// <summary></summary>	
		[Description("")]
        public string CreateUser { get; set; } // CreateUser
		/// <summary></summary>	
		[Description("")]
        public string ModifyUserID { get; set; } // ModifyUserID
		/// <summary></summary>	
		[Description("")]
        public string ModifyUser { get; set; } // ModifyUser
		/// <summary></summary>	
		[Description("")]
        public string OrgID { get; set; } // OrgID
		/// <summary></summary>	
		[Description("")]
        public string CompanyID { get; set; } // CompanyID
		/// <summary></summary>	
		[Description("")]
        public string FlowPhase { get; set; } // FlowPhase
		/// <summary></summary>	
		[Description("")]
        public string FlowInfo { get; set; } // FlowInfo
		/// <summary></summary>	
		[Description("")]
        public string StepName { get; set; } // StepName
		/// <summary>项目ID</summary>	
		[Description("项目ID")]
        public string ProjectInfoID { get; set; } // ProjectInfoID
		/// <summary>项目名称</summary>	
		[Description("项目名称")]
        public string ProjectName { get; set; } // ProjectName
		/// <summary>项目编号</summary>	
		[Description("项目编号")]
        public string ProjectCode { get; set; } // ProjectCode
		/// <summary>项目负责人</summary>	
		[Description("项目负责人")]
        public string ProjectManager { get; set; } // ProjectManager
		/// <summary>项目负责人名称</summary>	
		[Description("项目负责人名称")]
        public string ProjectManagerName { get; set; } // ProjectManagerName
		/// <summary>责任部门</summary>	
		[Description("责任部门")]
        public string ChargeDept { get; set; } // ChargeDept
		/// <summary>责任部门名称</summary>	
		[Description("责任部门名称")]
        public string ChargeDeptName { get; set; } // ChargeDeptName
		/// <summary>业务类型</summary>	
		[Description("业务类型")]
        public string ProjectClass { get; set; } // ProjectClass
		/// <summary>项目阶段</summary>	
		[Description("项目阶段")]
        public string PhaseValue { get; set; } // PhaseValue
		/// <summary>计划开始时间</summary>	
		[Description("计划开始时间")]
        public DateTime? PlanStartDate { get; set; } // PlanStartDate
		/// <summary>计划结束时间</summary>	
		[Description("计划结束时间")]
        public DateTime? PlanFinishDate { get; set; } // PlanFinishDate
		/// <summary>设计输入子表</summary>	
		[Description("设计输入子表")]
        public string DesignInputList { get; set; } // DesignInputList
		/// <summary>版本号</summary>	
		[Description("版本号")]
        public string VersionNumber { get; set; } // VersionNumber
		/// <summary>分类</summary>	
		[Description("分类")]
        public string MajorValue { get; set; } // MajorValue
		/// <summary>专业负责人</summary>	
		[Description("专业负责人")]
        public string MajorPrinciple { get; set; } // MajorPrinciple
		/// <summary>专业负责人名称</summary>	
		[Description("专业负责人名称")]
        public string MajorPrincipleName { get; set; } // MajorPrincipleName
		/// <summary>编号</summary>	
		[Description("编号")]
        public string SerialNumber { get; set; } // SerialNumber
		/// <summary>专业审核人</summary>	
		[Description("专业审核人")]
        public string Auditor { get; set; } // Auditor
		/// <summary>专业审定人</summary>	
		[Description("专业审定人")]
        public string Approval { get; set; } // Approval

        // Reverse navigation
		[JsonIgnore]
        public virtual ICollection<T_SC_MajorDesignInput_DesignInputList> T_SC_MajorDesignInput_DesignInputList { get { onT_SC_MajorDesignInput_DesignInputListGetting(); return _T_SC_MajorDesignInput_DesignInputList;} }
		private ICollection<T_SC_MajorDesignInput_DesignInputList> _T_SC_MajorDesignInput_DesignInputList;
		partial void onT_SC_MajorDesignInput_DesignInputListGetting();


        public T_SC_MajorDesignInput()
        {
            _T_SC_MajorDesignInput_DesignInputList = new List<T_SC_MajorDesignInput_DesignInputList>();
        }
    }

	/// <summary>设计输入子表</summary>	
	[Description("设计输入子表")]
    public partial class T_SC_MajorDesignInput_DesignInputList : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string T_SC_MajorDesignInputID { get; set; } // T_SC_MajorDesignInputID
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string IsReleased { get; set; } // IsReleased
		/// <summary>资料分类</summary>	
		[Description("资料分类")]
        public string InputType { get; set; } // InputType
		/// <summary>资料名称</summary>	
		[Description("资料名称")]
        public string InfoName { get; set; } // InfoName
		/// <summary>文件名称</summary>	
		[Description("文件名称")]
        public string Name { get; set; } // Name
		/// <summary>上传日期</summary>	
		[Description("上传日期")]
        public string CreateDate { get; set; } // CreateDate
		/// <summary>上传人</summary>	
		[Description("上传人")]
        public string CreateUser { get; set; } // CreateUser
		/// <summary>InputInfoID</summary>	
		[Description("InputInfoID")]
        public string InputInfoID { get; set; } // InputInfoID
		/// <summary>DocID</summary>	
		[Description("DocID")]
        public string DocID { get; set; } // DocID
		/// <summary>Catagory</summary>	
		[Description("Catagory")]
        public string Catagory { get; set; } // Catagory
		/// <summary>附件</summary>	
		[Description("附件")]
        public string Files { get; set; } // Files

        // Foreign keys
		[JsonIgnore]
        public virtual T_SC_MajorDesignInput T_SC_MajorDesignInput { get; set; } //  T_SC_MajorDesignInputID - FK_T_SC_MajorDesignInput_DesignInputList_T_SC_MajorDesignInput
    }

	/// <summary>建筑单项工程策划表</summary>	
	[Description("建筑单项工程策划表")]
    public partial class T_SC_MixProjectScheme : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? ModifyDate { get; set; } // ModifyDate
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get; set; } // CreateUserID
		/// <summary></summary>	
		[Description("")]
        public string CreateUser { get; set; } // CreateUser
		/// <summary></summary>	
		[Description("")]
        public string ModifyUserID { get; set; } // ModifyUserID
		/// <summary></summary>	
		[Description("")]
        public string ModifyUser { get; set; } // ModifyUser
		/// <summary></summary>	
		[Description("")]
        public string OrgID { get; set; } // OrgID
		/// <summary></summary>	
		[Description("")]
        public string CompanyID { get; set; } // CompanyID
		/// <summary></summary>	
		[Description("")]
        public string FlowPhase { get; set; } // FlowPhase
		/// <summary></summary>	
		[Description("")]
        public string FlowInfo { get; set; } // FlowInfo
		/// <summary></summary>	
		[Description("")]
        public string StepName { get; set; } // StepName
		/// <summary>表单编号</summary>	
		[Description("表单编号")]
        public string SerialNumber { get; set; } // SerialNumber
		/// <summary>项目名称</summary>	
		[Description("项目名称")]
        public string Name { get; set; } // Name
		/// <summary>项目编号</summary>	
		[Description("项目编号")]
        public string Code { get; set; } // Code
		/// <summary>工作内容</summary>	
		[Description("工作内容")]
        public string PhaseValue { get; set; } // PhaseValue
		/// <summary>项目阶段</summary>	
		[Description("项目阶段")]
        public string PhaseName { get; set; } // PhaseName
		/// <summary>责任部门</summary>	
		[Description("责任部门")]
        public string ChargeDept { get; set; } // ChargeDept
		/// <summary>责任部门名称</summary>	
		[Description("责任部门名称")]
        public string ChargeDeptName { get; set; } // ChargeDeptName
		/// <summary>业务类型</summary>	
		[Description("业务类型")]
        public string ProjectClass { get; set; } // ProjectClass
		/// <summary>建筑面积</summary>	
		[Description("建筑面积")]
        public double? BuildArea { get; set; } // BuildArea
		/// <summary>计划开始日期</summary>	
		[Description("计划开始日期")]
        public DateTime? PlanStartDate { get; set; } // PlanStartDate
		/// <summary>计划结束日期</summary>	
		[Description("计划结束日期")]
        public DateTime? PlanFinishDate { get; set; } // PlanFinishDate
		/// <summary>参与专业</summary>	
		[Description("参与专业")]
        public string Major { get; set; } // Major
		/// <summary>项目负责人</summary>	
		[Description("项目负责人")]
        public string ChargeUser { get; set; } // ChargeUser
		/// <summary>项目负责人名称</summary>	
		[Description("项目负责人名称")]
        public string ChargeUserName { get; set; } // ChargeUserName
		/// <summary>专业人员信息</summary>	
		[Description("专业人员信息")]
        public string MajorList { get; set; } // MajorList
		/// <summary>子项信息</summary>	
		[Description("子项信息")]
        public string SubProjectList { get; set; } // SubProjectList
		/// <summary>里程碑</summary>	
		[Description("里程碑")]
        public string MileStoneList { get; set; } // MileStoneList
		/// <summary>项目ID</summary>	
		[Description("项目ID")]
        public string ProjectInfoID { get; set; } // ProjectInfoID
		/// <summary>版本号</summary>	
		[Description("版本号")]
        public string VersionNumber { get; set; } // VersionNumber
		/// <summary>相关附件</summary>	
		[Description("相关附件")]
        public string Attachments { get; set; } // Attachments
		/// <summary>备注</summary>	
		[Description("备注")]
        public string Remark { get; set; } // Remark
		/// <summary>项目规模</summary>	
		[Description("项目规模")]
        public string ProjectLevel { get; set; } // ProjectLevel
		/// <summary>项目负责人意见</summary>	
		[Description("项目负责人意见")]
        public string ProjectLeader { get; set; } // ProjectLeader
		/// <summary>所长意见</summary>	
		[Description("所长意见")]
        public string Director { get; set; } // Director

        // Reverse navigation
		[JsonIgnore]
        public virtual ICollection<T_SC_MixProjectScheme_MajorList> T_SC_MixProjectScheme_MajorList { get { onT_SC_MixProjectScheme_MajorListGetting(); return _T_SC_MixProjectScheme_MajorList;} }
		private ICollection<T_SC_MixProjectScheme_MajorList> _T_SC_MixProjectScheme_MajorList;
		partial void onT_SC_MixProjectScheme_MajorListGetting();

		[JsonIgnore]
        public virtual ICollection<T_SC_MixProjectScheme_MileStoneList> T_SC_MixProjectScheme_MileStoneList { get { onT_SC_MixProjectScheme_MileStoneListGetting(); return _T_SC_MixProjectScheme_MileStoneList;} }
		private ICollection<T_SC_MixProjectScheme_MileStoneList> _T_SC_MixProjectScheme_MileStoneList;
		partial void onT_SC_MixProjectScheme_MileStoneListGetting();

		[JsonIgnore]
        public virtual ICollection<T_SC_MixProjectScheme_SubProjectList> T_SC_MixProjectScheme_SubProjectList { get { onT_SC_MixProjectScheme_SubProjectListGetting(); return _T_SC_MixProjectScheme_SubProjectList;} }
		private ICollection<T_SC_MixProjectScheme_SubProjectList> _T_SC_MixProjectScheme_SubProjectList;
		partial void onT_SC_MixProjectScheme_SubProjectListGetting();


        public T_SC_MixProjectScheme()
        {
            _T_SC_MixProjectScheme_MajorList = new List<T_SC_MixProjectScheme_MajorList>();
            _T_SC_MixProjectScheme_MileStoneList = new List<T_SC_MixProjectScheme_MileStoneList>();
            _T_SC_MixProjectScheme_SubProjectList = new List<T_SC_MixProjectScheme_SubProjectList>();
        }
    }

	/// <summary>专业人员信息</summary>	
	[Description("专业人员信息")]
    public partial class T_SC_MixProjectScheme_MajorList : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string T_SC_MixProjectSchemeID { get; set; } // T_SC_MixProjectSchemeID
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string IsReleased { get; set; } // IsReleased
		/// <summary>专业名称</summary>	
		[Description("专业名称")]
        public string MajorName { get; set; } // MajorName
		/// <summary>专业编码</summary>	
		[Description("专业编码")]
        public string MajorCode { get; set; } // MajorCode
		/// <summary>专业负责人</summary>	
		[Description("专业负责人")]
        public string MajorPrinciple { get; set; } // MajorPrinciple
		/// <summary>专业负责人名称</summary>	
		[Description("专业负责人名称")]
        public string MajorPrincipleName { get; set; } // MajorPrincipleName
		/// <summary>设计人</summary>	
		[Description("设计人")]
        public string Designer { get; set; } // Designer
		/// <summary>设计人名称</summary>	
		[Description("设计人名称")]
        public string DesignerName { get; set; } // DesignerName
		/// <summary>校核人</summary>	
		[Description("校核人")]
        public string Collactor { get; set; } // Collactor
		/// <summary>校核人名称</summary>	
		[Description("校核人名称")]
        public string CollactorName { get; set; } // CollactorName
		/// <summary>审核人</summary>	
		[Description("审核人")]
        public string Auditor { get; set; } // Auditor
		/// <summary>审核人名称</summary>	
		[Description("审核人名称")]
        public string AuditorName { get; set; } // AuditorName
		/// <summary>审定人</summary>	
		[Description("审定人")]
        public string Approver { get; set; } // Approver
		/// <summary>审定人名称</summary>	
		[Description("审定人名称")]
        public string ApproverName { get; set; } // ApproverName
		/// <summary>备注</summary>	
		[Description("备注")]
        public string Remark { get; set; } // Remark
		/// <summary>专业总工程师</summary>	
		[Description("专业总工程师")]
        public string MajorEngineer { get; set; } // MajorEngineer
		/// <summary>专业总工程师名称</summary>	
		[Description("专业总工程师名称")]
        public string MajorEngineerName { get; set; } // MajorEngineerName

        // Foreign keys
		[JsonIgnore]
        public virtual T_SC_MixProjectScheme T_SC_MixProjectScheme { get; set; } //  T_SC_MixProjectSchemeID - FK_T_SC_MixProjectScheme_MajorList_T_SC_MixProjectScheme
    }

	/// <summary>里程碑</summary>	
	[Description("里程碑")]
    public partial class T_SC_MixProjectScheme_MileStoneList : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string T_SC_MixProjectSchemeID { get; set; } // T_SC_MixProjectSchemeID
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string IsReleased { get; set; } // IsReleased
		/// <summary>名称</summary>	
		[Description("名称")]
        public string Name { get; set; } // Name
		/// <summary>专业</summary>	
		[Description("专业")]
        public string Major { get; set; } // Major
		/// <summary>计划完成日期</summary>	
		[Description("计划完成日期")]
        public DateTime? PlanEndDate { get; set; } // PlanEndDate
		/// <summary>权重（%）</summary>	
		[Description("权重（%）")]
        public decimal? Weight { get; set; } // Weight
		/// <summary>备注</summary>	
		[Description("备注")]
        public string Remark { get; set; } // Remark
		/// <summary>里程碑类别</summary>	
		[Description("里程碑类别")]
        public string MileStoneType { get; set; } // MileStoneType
		/// <summary>编号</summary>	
		[Description("编号")]
        public string Code { get; set; } // Code
		/// <summary>MileStoneID</summary>	
		[Description("MileStoneID")]
        public string MileStoneID { get; set; } // MileStoneID
		/// <summary>提出专业</summary>	
		[Description("提出专业")]
        public string OutMajor { get; set; } // OutMajor
		/// <summary>接收专业</summary>	
		[Description("接收专业")]
        public string InMajor { get; set; } // InMajor
		/// <summary>TemplateID</summary>	
		[Description("TemplateID")]
        public string TemplateID { get; set; } // TemplateID

        // Foreign keys
		[JsonIgnore]
        public virtual T_SC_MixProjectScheme T_SC_MixProjectScheme { get; set; } //  T_SC_MixProjectSchemeID - FK_T_SC_MixProjectScheme_MilestoneList_T_SC_MixProjectScheme
    }

	/// <summary>子项信息</summary>	
	[Description("子项信息")]
    public partial class T_SC_MixProjectScheme_SubProjectList : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string T_SC_MixProjectSchemeID { get; set; } // T_SC_MixProjectSchemeID
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string IsReleased { get; set; } // IsReleased
		/// <summary>子项名称</summary>	
		[Description("子项名称")]
        public string Name { get; set; } // Name
		/// <summary>设计阶段</summary>	
		[Description("设计阶段")]
        public string PhaseValue { get; set; } // PhaseValue
		/// <summary>建筑面积(㎡)</summary>	
		[Description("建筑面积(㎡)")]
        public double? Area { get; set; } // Area
		/// <summary>单体（多个单体用逗号隔开）</summary>	
		[Description("单体（多个单体用逗号隔开）")]
        public string Unit { get; set; } // Unit
		/// <summary>子项编号</summary>	
		[Description("子项编号")]
        public string Code { get; set; } // Code
		/// <summary>子项人员信息</summary>	
		[Description("子项人员信息")]
        public string RBSJson { get; set; } // RBSJson

        // Foreign keys
		[JsonIgnore]
        public virtual T_SC_MixProjectScheme T_SC_MixProjectScheme { get; set; } //  T_SC_MixProjectSchemeID - FK_T_SC_MixProjectScheme_SubProjectList_T_SC_MixProjectScheme
    }

	/// <summary>石化院策划表</summary>	
	[Description("石化院策划表")]
    public partial class T_SC_PetrifactionProjectScheme : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? ModifyDate { get; set; } // ModifyDate
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get; set; } // CreateUserID
		/// <summary></summary>	
		[Description("")]
        public string CreateUser { get; set; } // CreateUser
		/// <summary></summary>	
		[Description("")]
        public string ModifyUserID { get; set; } // ModifyUserID
		/// <summary></summary>	
		[Description("")]
        public string ModifyUser { get; set; } // ModifyUser
		/// <summary></summary>	
		[Description("")]
        public string OrgID { get; set; } // OrgID
		/// <summary></summary>	
		[Description("")]
        public string CompanyID { get; set; } // CompanyID
		/// <summary></summary>	
		[Description("")]
        public string FlowPhase { get; set; } // FlowPhase
		/// <summary></summary>	
		[Description("")]
        public string FlowInfo { get; set; } // FlowInfo
		/// <summary></summary>	
		[Description("")]
        public string StepName { get; set; } // StepName
		/// <summary>表单编号</summary>	
		[Description("表单编号")]
        public string SerialNumber { get; set; } // SerialNumber
		/// <summary>项目名称</summary>	
		[Description("项目名称")]
        public string Name { get; set; } // Name
		/// <summary>项目编号</summary>	
		[Description("项目编号")]
        public string Code { get; set; } // Code
		/// <summary>工作内容</summary>	
		[Description("工作内容")]
        public string PhaseValue { get; set; } // PhaseValue
		/// <summary>项目阶段</summary>	
		[Description("项目阶段")]
        public string PhaseName { get; set; } // PhaseName
		/// <summary>责任部门</summary>	
		[Description("责任部门")]
        public string ChargeDept { get; set; } // ChargeDept
		/// <summary>责任部门名称</summary>	
		[Description("责任部门名称")]
        public string ChargeDeptName { get; set; } // ChargeDeptName
		/// <summary>业务类型</summary>	
		[Description("业务类型")]
        public string ProjectClass { get; set; } // ProjectClass
		/// <summary>建筑面积</summary>	
		[Description("建筑面积")]
        public double? BuildArea { get; set; } // BuildArea
		/// <summary>计划开始日期</summary>	
		[Description("计划开始日期")]
        public DateTime? PlanStartDate { get; set; } // PlanStartDate
		/// <summary>计划结束日期</summary>	
		[Description("计划结束日期")]
        public DateTime? PlanFinishDate { get; set; } // PlanFinishDate
		/// <summary>参与专业</summary>	
		[Description("参与专业")]
        public string Major { get; set; } // Major
		/// <summary>项目负责人</summary>	
		[Description("项目负责人")]
        public string ChargeUser { get; set; } // ChargeUser
		/// <summary>项目负责人名称</summary>	
		[Description("项目负责人名称")]
        public string ChargeUserName { get; set; } // ChargeUserName
		/// <summary>专业人员信息</summary>	
		[Description("专业人员信息")]
        public string MajorList { get; set; } // MajorList
		/// <summary>项目ID</summary>	
		[Description("项目ID")]
        public string ProjectInfoID { get; set; } // ProjectInfoID
		/// <summary>版本号</summary>	
		[Description("版本号")]
        public string VersionNumber { get; set; } // VersionNumber
		/// <summary>相关附件</summary>	
		[Description("相关附件")]
        public string Attachments { get; set; } // Attachments
		/// <summary>备注</summary>	
		[Description("备注")]
        public string Remark { get; set; } // Remark
		/// <summary>项目规模</summary>	
		[Description("项目规模")]
        public string ProjectLevel { get; set; } // ProjectLevel
		/// <summary>项目负责人意见</summary>	
		[Description("项目负责人意见")]
        public string ProjectLeader { get; set; } // ProjectLeader
		/// <summary>所长意见</summary>	
		[Description("所长意见")]
        public string Director { get; set; } // Director

        // Reverse navigation
		[JsonIgnore]
        public virtual ICollection<T_SC_PetrifactionProjectScheme_MajorList> T_SC_PetrifactionProjectScheme_MajorList { get { onT_SC_PetrifactionProjectScheme_MajorListGetting(); return _T_SC_PetrifactionProjectScheme_MajorList;} }
		private ICollection<T_SC_PetrifactionProjectScheme_MajorList> _T_SC_PetrifactionProjectScheme_MajorList;
		partial void onT_SC_PetrifactionProjectScheme_MajorListGetting();


        public T_SC_PetrifactionProjectScheme()
        {
            _T_SC_PetrifactionProjectScheme_MajorList = new List<T_SC_PetrifactionProjectScheme_MajorList>();
        }
    }

	/// <summary>专业人员信息</summary>	
	[Description("专业人员信息")]
    public partial class T_SC_PetrifactionProjectScheme_MajorList : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string T_SC_PetrifactionProjectSchemeID { get; set; } // T_SC_PetrifactionProjectSchemeID
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string IsReleased { get; set; } // IsReleased
		/// <summary>专业名称</summary>	
		[Description("专业名称")]
        public string MajorName { get; set; } // MajorName
		/// <summary>专业编码</summary>	
		[Description("专业编码")]
        public string MajorCode { get; set; } // MajorCode
		/// <summary>专业负责人</summary>	
		[Description("专业负责人")]
        public string MajorPrinciple { get; set; } // MajorPrinciple
		/// <summary>专业负责人名称</summary>	
		[Description("专业负责人名称")]
        public string MajorPrincipleName { get; set; } // MajorPrincipleName
		/// <summary>设计人</summary>	
		[Description("设计人")]
        public string Designer { get; set; } // Designer
		/// <summary>设计人名称</summary>	
		[Description("设计人名称")]
        public string DesignerName { get; set; } // DesignerName
		/// <summary>校核人</summary>	
		[Description("校核人")]
        public string Collactor { get; set; } // Collactor
		/// <summary>校核人名称</summary>	
		[Description("校核人名称")]
        public string CollactorName { get; set; } // CollactorName
		/// <summary>审核人</summary>	
		[Description("审核人")]
        public string Auditor { get; set; } // Auditor
		/// <summary>审核人名称</summary>	
		[Description("审核人名称")]
        public string AuditorName { get; set; } // AuditorName
		/// <summary>审定人</summary>	
		[Description("审定人")]
        public string Approver { get; set; } // Approver
		/// <summary>审定人名称</summary>	
		[Description("审定人名称")]
        public string ApproverName { get; set; } // ApproverName
		/// <summary>备注</summary>	
		[Description("备注")]
        public string Remark { get; set; } // Remark
		/// <summary>专业总工程师</summary>	
		[Description("专业总工程师")]
        public string MajorEngineer { get; set; } // MajorEngineer
		/// <summary>专业总工程师名称</summary>	
		[Description("专业总工程师名称")]
        public string MajorEngineerName { get; set; } // MajorEngineerName
		/// <summary>制图人</summary>	
		[Description("制图人")]
        public string Mapper { get; set; } // Mapper
		/// <summary>制图人名称</summary>	
		[Description("制图人名称")]
        public string MapperName { get; set; } // MapperName

        // Foreign keys
		[JsonIgnore]
        public virtual T_SC_PetrifactionProjectScheme T_SC_PetrifactionProjectScheme { get; set; } //  T_SC_PetrifactionProjectSchemeID - FK_T_SC_PetrifactionProjectScheme_MajorList_T_SC_PetrifactionProjectScheme
    }

	/// <summary>建筑策划表</summary>	
	[Description("建筑策划表")]
    public partial class T_SC_SchemeForm : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? ModifyDate { get; set; } // ModifyDate
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get; set; } // CreateUserID
		/// <summary></summary>	
		[Description("")]
        public string CreateUser { get; set; } // CreateUser
		/// <summary></summary>	
		[Description("")]
        public string ModifyUserID { get; set; } // ModifyUserID
		/// <summary></summary>	
		[Description("")]
        public string ModifyUser { get; set; } // ModifyUser
		/// <summary></summary>	
		[Description("")]
        public string OrgID { get; set; } // OrgID
		/// <summary></summary>	
		[Description("")]
        public string CompanyID { get; set; } // CompanyID
		/// <summary></summary>	
		[Description("")]
        public string FlowPhase { get; set; } // FlowPhase
		/// <summary></summary>	
		[Description("")]
        public string FlowInfo { get; set; } // FlowInfo
		/// <summary></summary>	
		[Description("")]
        public string StepName { get; set; } // StepName
		/// <summary>表单编号</summary>	
		[Description("表单编号")]
        public string FormCode { get; set; } // FormCode
		/// <summary>项目名称</summary>	
		[Description("项目名称")]
        public string Name { get; set; } // Name
		/// <summary>项目编号</summary>	
		[Description("项目编号")]
        public string Code { get; set; } // Code
		/// <summary>工作内容</summary>	
		[Description("工作内容")]
        public string PhaseValue { get; set; } // PhaseValue
		/// <summary>项目阶段</summary>	
		[Description("项目阶段")]
        public string PhaseName { get; set; } // PhaseName
		/// <summary>责任部门</summary>	
		[Description("责任部门")]
        public string ChargeDept { get; set; } // ChargeDept
		/// <summary>责任部门名称</summary>	
		[Description("责任部门名称")]
        public string ChargeDeptName { get; set; } // ChargeDeptName
		/// <summary>业务类型</summary>	
		[Description("业务类型")]
        public string ProjectClass { get; set; } // ProjectClass
		/// <summary>建筑面积</summary>	
		[Description("建筑面积")]
        public double? BuildArea { get; set; } // BuildArea
		/// <summary>计划开始日期</summary>	
		[Description("计划开始日期")]
        public DateTime? PlanStartDate { get; set; } // PlanStartDate
		/// <summary>计划结束日期</summary>	
		[Description("计划结束日期")]
        public DateTime? PlanFinishDate { get; set; } // PlanFinishDate
		/// <summary>参与专业</summary>	
		[Description("参与专业")]
        public string Major { get; set; } // Major
		/// <summary>项目负责人</summary>	
		[Description("项目负责人")]
        public string ChargeUser { get; set; } // ChargeUser
		/// <summary>项目负责人名称</summary>	
		[Description("项目负责人名称")]
        public string ChargeUserName { get; set; } // ChargeUserName
		/// <summary>专业人员信息</summary>	
		[Description("专业人员信息")]
        public string MajorList { get; set; } // MajorList
		/// <summary>子项信息</summary>	
		[Description("子项信息")]
        public string SubProjectList { get; set; } // SubProjectList
		/// <summary>项目ID</summary>	
		[Description("项目ID")]
        public string ProjectInfoID { get; set; } // ProjectInfoID
		/// <summary>版本号</summary>	
		[Description("版本号")]
        public string VersionNumber { get; set; } // VersionNumber
		/// <summary>表单编号</summary>	
		[Description("表单编号")]
        public string SerialNumber { get; set; } // SerialNumber
		/// <summary>相关附件</summary>	
		[Description("相关附件")]
        public string Attachments { get; set; } // Attachments
		/// <summary>备注</summary>	
		[Description("备注")]
        public string Remark { get; set; } // Remark
		/// <summary>项目规模</summary>	
		[Description("项目规模")]
        public string ProjectLevel { get; set; } // ProjectLevel
		/// <summary>项目负责人意见</summary>	
		[Description("项目负责人意见")]
        public string ProjectLeader { get; set; } // ProjectLeader
		/// <summary>所长意见</summary>	
		[Description("所长意见")]
        public string Director { get; set; } // Director

        // Reverse navigation
		[JsonIgnore]
        public virtual ICollection<T_SC_SchemeForm_MajorList> T_SC_SchemeForm_MajorList { get { onT_SC_SchemeForm_MajorListGetting(); return _T_SC_SchemeForm_MajorList;} }
		private ICollection<T_SC_SchemeForm_MajorList> _T_SC_SchemeForm_MajorList;
		partial void onT_SC_SchemeForm_MajorListGetting();

		[JsonIgnore]
        public virtual ICollection<T_SC_SchemeForm_SubProjectList> T_SC_SchemeForm_SubProjectList { get { onT_SC_SchemeForm_SubProjectListGetting(); return _T_SC_SchemeForm_SubProjectList;} }
		private ICollection<T_SC_SchemeForm_SubProjectList> _T_SC_SchemeForm_SubProjectList;
		partial void onT_SC_SchemeForm_SubProjectListGetting();


        public T_SC_SchemeForm()
        {
            _T_SC_SchemeForm_MajorList = new List<T_SC_SchemeForm_MajorList>();
            _T_SC_SchemeForm_SubProjectList = new List<T_SC_SchemeForm_SubProjectList>();
        }
    }

	/// <summary>专业人员信息</summary>	
	[Description("专业人员信息")]
    public partial class T_SC_SchemeForm_MajorList : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string T_SC_SchemeFormID { get; set; } // T_SC_SchemeFormID
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string IsReleased { get; set; } // IsReleased
		/// <summary>专业名称</summary>	
		[Description("专业名称")]
        public string MajorName { get; set; } // MajorName
		/// <summary>专业编码</summary>	
		[Description("专业编码")]
        public string MajorCode { get; set; } // MajorCode
		/// <summary>专业负责人</summary>	
		[Description("专业负责人")]
        public string MajorPrinciple { get; set; } // MajorPrinciple
		/// <summary>专业负责人名称</summary>	
		[Description("专业负责人名称")]
        public string MajorPrincipleName { get; set; } // MajorPrincipleName
		/// <summary>设计人</summary>	
		[Description("设计人")]
        public string Designer { get; set; } // Designer
		/// <summary>设计人名称</summary>	
		[Description("设计人名称")]
        public string DesignerName { get; set; } // DesignerName
		/// <summary>校核人</summary>	
		[Description("校核人")]
        public string Collactor { get; set; } // Collactor
		/// <summary>校核人名称</summary>	
		[Description("校核人名称")]
        public string CollactorName { get; set; } // CollactorName
		/// <summary>审核人</summary>	
		[Description("审核人")]
        public string Auditor { get; set; } // Auditor
		/// <summary>审核人名称</summary>	
		[Description("审核人名称")]
        public string AuditorName { get; set; } // AuditorName
		/// <summary>审定人</summary>	
		[Description("审定人")]
        public string Approver { get; set; } // Approver
		/// <summary>审定人名称</summary>	
		[Description("审定人名称")]
        public string ApproverName { get; set; } // ApproverName
		/// <summary>备注</summary>	
		[Description("备注")]
        public string Remark { get; set; } // Remark
		/// <summary>专业总工程师</summary>	
		[Description("专业总工程师")]
        public string MajorEngineer { get; set; } // MajorEngineer
		/// <summary>专业总工程师名称</summary>	
		[Description("专业总工程师名称")]
        public string MajorEngineerName { get; set; } // MajorEngineerName
		/// <summary>制图人</summary>	
		[Description("制图人")]
        public string Mapper { get; set; } // Mapper
		/// <summary>制图人名称</summary>	
		[Description("制图人名称")]
        public string MapperName { get; set; } // MapperName

        // Foreign keys
		[JsonIgnore]
        public virtual T_SC_SchemeForm T_SC_SchemeForm { get; set; } //  T_SC_SchemeFormID - FK_T_SC_SchemeForm_MajorList_T_SC_SchemeForm
    }

	/// <summary>四方OEM策划表</summary>	
	[Description("四方OEM策划表")]
    public partial class T_SC_SchemeForm_OEM_Szsow : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? ModifyDate { get; set; } // ModifyDate
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get; set; } // CreateUserID
		/// <summary></summary>	
		[Description("")]
        public string CreateUser { get; set; } // CreateUser
		/// <summary></summary>	
		[Description("")]
        public string ModifyUserID { get; set; } // ModifyUserID
		/// <summary></summary>	
		[Description("")]
        public string ModifyUser { get; set; } // ModifyUser
		/// <summary></summary>	
		[Description("")]
        public string OrgID { get; set; } // OrgID
		/// <summary></summary>	
		[Description("")]
        public string CompanyID { get; set; } // CompanyID
		/// <summary></summary>	
		[Description("")]
        public string FlowPhase { get; set; } // FlowPhase
		/// <summary></summary>	
		[Description("")]
        public string FlowInfo { get; set; } // FlowInfo
		/// <summary></summary>	
		[Description("")]
        public string StepName { get; set; } // StepName
		/// <summary>表单编号</summary>	
		[Description("表单编号")]
        public string SerialNumber { get; set; } // SerialNumber
		/// <summary>项目名称</summary>	
		[Description("项目名称")]
        public string Name { get; set; } // Name
		/// <summary>项目编号</summary>	
		[Description("项目编号")]
        public string Code { get; set; } // Code
		/// <summary>工作内容</summary>	
		[Description("工作内容")]
        public string PhaseValue { get; set; } // PhaseValue
		/// <summary>项目阶段</summary>	
		[Description("项目阶段")]
        public string PhaseName { get; set; } // PhaseName
		/// <summary>责任部门</summary>	
		[Description("责任部门")]
        public string ChargeDept { get; set; } // ChargeDept
		/// <summary>责任部门名称</summary>	
		[Description("责任部门名称")]
        public string ChargeDeptName { get; set; } // ChargeDeptName
		/// <summary>业务类型</summary>	
		[Description("业务类型")]
        public string ProjectClass { get; set; } // ProjectClass
		/// <summary>建筑面积</summary>	
		[Description("建筑面积")]
        public double? BuildArea { get; set; } // BuildArea
		/// <summary>计划开始日期</summary>	
		[Description("计划开始日期")]
        public DateTime? PlanStartDate { get; set; } // PlanStartDate
		/// <summary>计划结束日期</summary>	
		[Description("计划结束日期")]
        public DateTime? PlanFinishDate { get; set; } // PlanFinishDate
		/// <summary>参与专业</summary>	
		[Description("参与专业")]
        public string Major { get; set; } // Major
		/// <summary>项目负责人</summary>	
		[Description("项目负责人")]
        public string ChargeUser { get; set; } // ChargeUser
		/// <summary>项目负责人名称</summary>	
		[Description("项目负责人名称")]
        public string ChargeUserName { get; set; } // ChargeUserName
		/// <summary>专业人员信息</summary>	
		[Description("专业人员信息")]
        public string MajorList { get; set; } // MajorList
		/// <summary>子项信息</summary>	
		[Description("子项信息")]
        public string SubProjectList { get; set; } // SubProjectList
		/// <summary>项目ID</summary>	
		[Description("项目ID")]
        public string ProjectInfoID { get; set; } // ProjectInfoID
		/// <summary>版本号</summary>	
		[Description("版本号")]
        public string VersionNumber { get; set; } // VersionNumber
		/// <summary>相关附件</summary>	
		[Description("相关附件")]
        public string Attachments { get; set; } // Attachments
		/// <summary>备注</summary>	
		[Description("备注")]
        public string Remark { get; set; } // Remark
		/// <summary>项目规模</summary>	
		[Description("项目规模")]
        public string ProjectLevel { get; set; } // ProjectLevel
		/// <summary>项目负责人意见</summary>	
		[Description("项目负责人意见")]
        public string ProjectLeader { get; set; } // ProjectLeader
		/// <summary>所长意见</summary>	
		[Description("所长意见")]
        public string Director { get; set; } // Director
		/// <summary>提醒期限（天）</summary>	
		[Description("提醒期限（天）")]
        public int? AlarmBeforeDays { get; set; } // AlarmBeforeDays

        // Reverse navigation
		[JsonIgnore]
        public virtual ICollection<T_SC_SchemeForm_OEM_Szsow_MajorList> T_SC_SchemeForm_OEM_Szsow_MajorList { get { onT_SC_SchemeForm_OEM_Szsow_MajorListGetting(); return _T_SC_SchemeForm_OEM_Szsow_MajorList;} }
		private ICollection<T_SC_SchemeForm_OEM_Szsow_MajorList> _T_SC_SchemeForm_OEM_Szsow_MajorList;
		partial void onT_SC_SchemeForm_OEM_Szsow_MajorListGetting();

		[JsonIgnore]
        public virtual ICollection<T_SC_SchemeForm_OEM_Szsow_SubProjectList> T_SC_SchemeForm_OEM_Szsow_SubProjectList { get { onT_SC_SchemeForm_OEM_Szsow_SubProjectListGetting(); return _T_SC_SchemeForm_OEM_Szsow_SubProjectList;} }
		private ICollection<T_SC_SchemeForm_OEM_Szsow_SubProjectList> _T_SC_SchemeForm_OEM_Szsow_SubProjectList;
		partial void onT_SC_SchemeForm_OEM_Szsow_SubProjectListGetting();


        public T_SC_SchemeForm_OEM_Szsow()
        {
            _T_SC_SchemeForm_OEM_Szsow_MajorList = new List<T_SC_SchemeForm_OEM_Szsow_MajorList>();
            _T_SC_SchemeForm_OEM_Szsow_SubProjectList = new List<T_SC_SchemeForm_OEM_Szsow_SubProjectList>();
        }
    }

	/// <summary>专业人员信息</summary>	
	[Description("专业人员信息")]
    public partial class T_SC_SchemeForm_OEM_Szsow_MajorList : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string T_SC_SchemeForm_OEM_SzsowID { get; set; } // T_SC_SchemeForm_OEM_SzsowID
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string IsReleased { get; set; } // IsReleased
		/// <summary>专业名称</summary>	
		[Description("专业名称")]
        public string MajorName { get; set; } // MajorName
		/// <summary>专业编码</summary>	
		[Description("专业编码")]
        public string MajorCode { get; set; } // MajorCode
		/// <summary>专业负责人</summary>	
		[Description("专业负责人")]
        public string MajorPrinciple { get; set; } // MajorPrinciple
		/// <summary>专业负责人名称</summary>	
		[Description("专业负责人名称")]
        public string MajorPrincipleName { get; set; } // MajorPrincipleName
		/// <summary>设计人</summary>	
		[Description("设计人")]
        public string Designer { get; set; } // Designer
		/// <summary>设计人名称</summary>	
		[Description("设计人名称")]
        public string DesignerName { get; set; } // DesignerName
		/// <summary>校核人</summary>	
		[Description("校核人")]
        public string Collactor { get; set; } // Collactor
		/// <summary>校核人名称</summary>	
		[Description("校核人名称")]
        public string CollactorName { get; set; } // CollactorName
		/// <summary>审核人</summary>	
		[Description("审核人")]
        public string Auditor { get; set; } // Auditor
		/// <summary>审核人名称</summary>	
		[Description("审核人名称")]
        public string AuditorName { get; set; } // AuditorName
		/// <summary>审定人</summary>	
		[Description("审定人")]
        public string Approver { get; set; } // Approver
		/// <summary>审定人名称</summary>	
		[Description("审定人名称")]
        public string ApproverName { get; set; } // ApproverName
		/// <summary>备注</summary>	
		[Description("备注")]
        public string Remark { get; set; } // Remark
		/// <summary>专业总工程师</summary>	
		[Description("专业总工程师")]
        public string MajorEngineer { get; set; } // MajorEngineer
		/// <summary>专业总工程师名称</summary>	
		[Description("专业总工程师名称")]
        public string MajorEngineerName { get; set; } // MajorEngineerName

        // Foreign keys
		[JsonIgnore]
        public virtual T_SC_SchemeForm_OEM_Szsow T_SC_SchemeForm_OEM_Szsow { get; set; } //  T_SC_SchemeForm_OEM_SzsowID - FK_T_SC_SchemeForm_OEM_Szsow_MajorList_T_SC_SchemeForm_OEM_Szsow
    }

	/// <summary>子项信息</summary>	
	[Description("子项信息")]
    public partial class T_SC_SchemeForm_OEM_Szsow_SubProjectList : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string T_SC_SchemeForm_OEM_SzsowID { get; set; } // T_SC_SchemeForm_OEM_SzsowID
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string IsReleased { get; set; } // IsReleased
		/// <summary>子项名称</summary>	
		[Description("子项名称")]
        public string Name { get; set; } // Name
		/// <summary>设计阶段</summary>	
		[Description("设计阶段")]
        public string PhaseValue { get; set; } // PhaseValue
		/// <summary>建筑面积(㎡)</summary>	
		[Description("建筑面积(㎡)")]
        public double? Area { get; set; } // Area
		/// <summary>单体（多个单体用逗号隔开）</summary>	
		[Description("单体（多个单体用逗号隔开）")]
        public string Unit { get; set; } // Unit
		/// <summary>子项编号</summary>	
		[Description("子项编号")]
        public string Code { get; set; } // Code
		/// <summary>子项人员信息</summary>	
		[Description("子项人员信息")]
        public string RBSJson { get; set; } // RBSJson
		/// <summary>阶段WBSID</summary>	
		[Description("阶段WBSID")]
        public string PhaseWBSID { get; set; } // PhaseWBSID
		/// <summary>子项WBSID</summary>	
		[Description("子项WBSID")]
        public string SubProjectWBSID { get; set; } // SubProjectWBSID

        // Foreign keys
		[JsonIgnore]
        public virtual T_SC_SchemeForm_OEM_Szsow T_SC_SchemeForm_OEM_Szsow { get; set; } //  T_SC_SchemeForm_OEM_SzsowID - FK_T_SC_SchemeForm_OEM_Szsow_SubProjectList_T_SC_SchemeForm_OEM_Szsow
    }

	/// <summary>子项信息</summary>	
	[Description("子项信息")]
    public partial class T_SC_SchemeForm_SubProjectList : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string T_SC_SchemeFormID { get; set; } // T_SC_SchemeFormID
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string IsReleased { get; set; } // IsReleased
		/// <summary>子项名称</summary>	
		[Description("子项名称")]
        public string Name { get; set; } // Name
		/// <summary>子项编号</summary>	
		[Description("子项编号")]
        public string Code { get; set; } // Code
		/// <summary>设计阶段</summary>	
		[Description("设计阶段")]
        public string PhaseValue { get; set; } // PhaseValue
		/// <summary>建筑面积(㎡)</summary>	
		[Description("建筑面积(㎡)")]
        public double? Area { get; set; } // Area
		/// <summary>单体（多个单体用逗号隔开）</summary>	
		[Description("单体（多个单体用逗号隔开）")]
        public string Unit { get; set; } // Unit
		/// <summary>子项人员信息</summary>	
		[Description("子项人员信息")]
        public string RBSJson { get; set; } // RBSJson
		/// <summary>WBSID</summary>	
		[Description("WBSID")]
        public string WBSID { get; set; } // WBSID

        // Foreign keys
		[JsonIgnore]
        public virtual T_SC_SchemeForm T_SC_SchemeForm { get; set; } //  T_SC_SchemeFormID - FK_T_SC_SchemeForm_SubProjectList_T_SC_SchemeForm
    }

	/// <summary>规划设计计划表</summary>	
	[Description("规划设计计划表")]
    public partial class T_SC_SimpleProjectSchmea : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? ModifyDate { get; set; } // ModifyDate
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get; set; } // CreateUserID
		/// <summary></summary>	
		[Description("")]
        public string CreateUser { get; set; } // CreateUser
		/// <summary></summary>	
		[Description("")]
        public string ModifyUserID { get; set; } // ModifyUserID
		/// <summary></summary>	
		[Description("")]
        public string ModifyUser { get; set; } // ModifyUser
		/// <summary></summary>	
		[Description("")]
        public string OrgID { get; set; } // OrgID
		/// <summary></summary>	
		[Description("")]
        public string CompanyID { get; set; } // CompanyID
		/// <summary></summary>	
		[Description("")]
        public string FlowPhase { get; set; } // FlowPhase
		/// <summary></summary>	
		[Description("")]
        public string FlowInfo { get; set; } // FlowInfo
		/// <summary></summary>	
		[Description("")]
        public string StepName { get; set; } // StepName
		/// <summary>项目名称</summary>	
		[Description("项目名称")]
        public string ProjectInfoName { get; set; } // ProjectInfoName
		/// <summary>项目编号</summary>	
		[Description("项目编号")]
        public string ProjectInfoCode { get; set; } // ProjectInfoCode
		/// <summary>项目负责人</summary>	
		[Description("项目负责人")]
        public string ProjectManager { get; set; } // ProjectManager
		/// <summary>项目负责人名称</summary>	
		[Description("项目负责人名称")]
        public string ProjectManagerName { get; set; } // ProjectManagerName
		/// <summary>项目组成员</summary>	
		[Description("项目组成员")]
        public string Designer { get; set; } // Designer
		/// <summary>项目组成员名称</summary>	
		[Description("项目组成员名称")]
        public string DesignerName { get; set; } // DesignerName
		/// <summary>校核人</summary>	
		[Description("校核人")]
        public string Collactor { get; set; } // Collactor
		/// <summary>校核人名称</summary>	
		[Description("校核人名称")]
        public string CollactorName { get; set; } // CollactorName
		/// <summary>审核人</summary>	
		[Description("审核人")]
        public string Auditor { get; set; } // Auditor
		/// <summary>审核人名称</summary>	
		[Description("审核人名称")]
        public string AuditorName { get; set; } // AuditorName
		/// <summary>审定人</summary>	
		[Description("审定人")]
        public string Approver { get; set; } // Approver
		/// <summary>审定人名称</summary>	
		[Description("审定人名称")]
        public string ApproverName { get; set; } // ApproverName
		/// <summary>工作目标</summary>	
		[Description("工作目标")]
        public string Target { get; set; } // Target
		/// <summary>设计范围</summary>	
		[Description("设计范围")]
        public string Scope { get; set; } // Scope
		/// <summary>项目设计依据</summary>	
		[Description("项目设计依据")]
        public string DesignBasic { get; set; } // DesignBasic
		/// <summary>工作内容</summary>	
		[Description("工作内容")]
        public string Content { get; set; } // Content
		/// <summary>任务分工</summary>	
		[Description("任务分工")]
        public string TaskInfo { get; set; } // TaskInfo
		/// <summary></summary>	
		[Description("")]
        public string MileStone { get; set; } // MileStone
		/// <summary>项目负责人签字</summary>	
		[Description("项目负责人签字")]
        public string ProjectManagerSign { get; set; } // ProjectManagerSign
		/// <summary>项目组成员签字</summary>	
		[Description("项目组成员签字")]
        public string ProjectMemberSign { get; set; } // ProjectMemberSign
		/// <summary>专业</summary>	
		[Description("专业")]
        public string Major { get; set; } // Major
		/// <summary>ProjectInfoID</summary>	
		[Description("ProjectInfoID")]
        public string ProjectInfoID { get; set; } // ProjectInfoID
		/// <summary>SerialNumber</summary>	
		[Description("SerialNumber")]
        public string SerialNumber { get; set; } // SerialNumber
		/// <summary>VersionNumber</summary>	
		[Description("VersionNumber")]
        public string VersionNumber { get; set; } // VersionNumber
		/// <summary>计划下达日期</summary>	
		[Description("计划下达日期")]
        public DateTime? PlanDate { get; set; } // PlanDate

        // Reverse navigation
		[JsonIgnore]
        public virtual ICollection<T_SC_SimpleProjectSchmea_MileStone> T_SC_SimpleProjectSchmea_MileStone { get { onT_SC_SimpleProjectSchmea_MileStoneGetting(); return _T_SC_SimpleProjectSchmea_MileStone;} }
		private ICollection<T_SC_SimpleProjectSchmea_MileStone> _T_SC_SimpleProjectSchmea_MileStone;
		partial void onT_SC_SimpleProjectSchmea_MileStoneGetting();


        public T_SC_SimpleProjectSchmea()
        {
            _T_SC_SimpleProjectSchmea_MileStone = new List<T_SC_SimpleProjectSchmea_MileStone>();
        }
    }

	/// <summary>日程安排</summary>	
	[Description("日程安排")]
    public partial class T_SC_SimpleProjectSchmea_MileStone : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string T_SC_SimpleProjectSchmeaID { get; set; } // T_SC_SimpleProjectSchmeaID
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string IsReleased { get; set; } // IsReleased
		/// <summary>名称</summary>	
		[Description("名称")]
        public string Name { get; set; } // Name
		/// <summary>计划开始日期</summary>	
		[Description("计划开始日期")]
        public DateTime? PlanStartDate { get; set; } // PlanStartDate
		/// <summary>计划完成日期</summary>	
		[Description("计划完成日期")]
        public DateTime? PlanEndDate { get; set; } // PlanEndDate
		/// <summary>内容</summary>	
		[Description("内容")]
        public string Content { get; set; } // Content
		/// <summary>MileStoneType</summary>	
		[Description("MileStoneType")]
        public string MileStoneType { get; set; } // MileStoneType
		/// <summary>Code</summary>	
		[Description("Code")]
        public string Code { get; set; } // Code
		/// <summary>TemplateID</summary>	
		[Description("TemplateID")]
        public string TemplateID { get; set; } // TemplateID
		/// <summary>OutMajor</summary>	
		[Description("OutMajor")]
        public string OutMajor { get; set; } // OutMajor
		/// <summary>InMajor</summary>	
		[Description("InMajor")]
        public string InMajor { get; set; } // InMajor

        // Foreign keys
		[JsonIgnore]
        public virtual T_SC_SimpleProjectSchmea T_SC_SimpleProjectSchmea { get; set; } //  T_SC_SimpleProjectSchmeaID - FK_T_SC_SimpleProjectSchmea_MileStone_T_SC_SimpleProjectSchmea
    }

	/// <summary>建筑方案策划表</summary>	
	[Description("建筑方案策划表")]
    public partial class T_SC_SingleProjectScheme : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? ModifyDate { get; set; } // ModifyDate
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get; set; } // CreateUserID
		/// <summary></summary>	
		[Description("")]
        public string CreateUser { get; set; } // CreateUser
		/// <summary></summary>	
		[Description("")]
        public string ModifyUserID { get; set; } // ModifyUserID
		/// <summary></summary>	
		[Description("")]
        public string ModifyUser { get; set; } // ModifyUser
		/// <summary></summary>	
		[Description("")]
        public string OrgID { get; set; } // OrgID
		/// <summary></summary>	
		[Description("")]
        public string CompanyID { get; set; } // CompanyID
		/// <summary></summary>	
		[Description("")]
        public string FlowPhase { get; set; } // FlowPhase
		/// <summary></summary>	
		[Description("")]
        public string FlowInfo { get; set; } // FlowInfo
		/// <summary></summary>	
		[Description("")]
        public string StepName { get; set; } // StepName
		/// <summary>表单编号</summary>	
		[Description("表单编号")]
        public string FormCode { get; set; } // FormCode
		/// <summary>项目名称</summary>	
		[Description("项目名称")]
        public string Name { get; set; } // Name
		/// <summary>项目编号</summary>	
		[Description("项目编号")]
        public string Code { get; set; } // Code
		/// <summary>工作内容</summary>	
		[Description("工作内容")]
        public string PhaseValue { get; set; } // PhaseValue
		/// <summary>项目阶段</summary>	
		[Description("项目阶段")]
        public string PhaseName { get; set; } // PhaseName
		/// <summary>责任部门</summary>	
		[Description("责任部门")]
        public string ChargeDept { get; set; } // ChargeDept
		/// <summary>责任部门名称</summary>	
		[Description("责任部门名称")]
        public string ChargeDeptName { get; set; } // ChargeDeptName
		/// <summary>业务类型</summary>	
		[Description("业务类型")]
        public string ProjectClass { get; set; } // ProjectClass
		/// <summary>建筑面积</summary>	
		[Description("建筑面积")]
        public double? BuildArea { get; set; } // BuildArea
		/// <summary>计划开始日期</summary>	
		[Description("计划开始日期")]
        public DateTime? PlanStartDate { get; set; } // PlanStartDate
		/// <summary>计划结束日期</summary>	
		[Description("计划结束日期")]
        public DateTime? PlanFinishDate { get; set; } // PlanFinishDate
		/// <summary>参与专业</summary>	
		[Description("参与专业")]
        public string Major { get; set; } // Major
		/// <summary>项目负责人</summary>	
		[Description("项目负责人")]
        public string ChargeUser { get; set; } // ChargeUser
		/// <summary>项目负责人名称</summary>	
		[Description("项目负责人名称")]
        public string ChargeUserName { get; set; } // ChargeUserName
		/// <summary>专业人员信息</summary>	
		[Description("专业人员信息")]
        public string MajorList { get; set; } // MajorList
		/// <summary></summary>	
		[Description("")]
        public string MileStoneList { get; set; } // MileStoneList
		/// <summary>项目ID</summary>	
		[Description("项目ID")]
        public string ProjectInfoID { get; set; } // ProjectInfoID
		/// <summary>版本号</summary>	
		[Description("版本号")]
        public string VersionNumber { get; set; } // VersionNumber
		/// <summary>相关附件</summary>	
		[Description("相关附件")]
        public string Attachments { get; set; } // Attachments
		/// <summary>备注</summary>	
		[Description("备注")]
        public string Remark { get; set; } // Remark
		/// <summary>表单编号</summary>	
		[Description("表单编号")]
        public string SerialNumber { get; set; } // SerialNumber
		/// <summary>项目规模</summary>	
		[Description("项目规模")]
        public string ProjectLevel { get; set; } // ProjectLevel
		/// <summary>设计要求</summary>	
		[Description("设计要求")]
        public string DesignRequired { get; set; } // DesignRequired
		/// <summary>输入评审</summary>	
		[Description("输入评审")]
        public string DesignInput { get; set; } // DesignInput
		/// <summary>部门领导审批</summary>	
		[Description("部门领导审批")]
        public string DeptLeaderSign { get; set; } // DeptLeaderSign
		/// <summary>院领导审批</summary>	
		[Description("院领导审批")]
        public string LeaderSign { get; set; } // LeaderSign
		/// <summary>总工审批</summary>	
		[Description("总工审批")]
        public string MainEngineerSign { get; set; } // MainEngineerSign
		/// <summary>评审人审批</summary>	
		[Description("评审人审批")]
        public string CheckManSign { get; set; } // CheckManSign
		/// <summary>客户</summary>	
		[Description("客户")]
        public string CustomerInfo { get; set; } // CustomerInfo
		/// <summary>客户名称</summary>	
		[Description("客户名称")]
        public string CustomerInfoName { get; set; } // CustomerInfoName
		/// <summary>测试岗位</summary>	
		[Description("测试岗位")]
        public string test { get; set; } // test
		/// <summary>测试岗位名称</summary>	
		[Description("测试岗位名称")]
        public string testName { get; set; } // testName

        // Reverse navigation
		[JsonIgnore]
        public virtual ICollection<T_SC_SingleProjectScheme_MajorList> T_SC_SingleProjectScheme_MajorList { get { onT_SC_SingleProjectScheme_MajorListGetting(); return _T_SC_SingleProjectScheme_MajorList;} }
		private ICollection<T_SC_SingleProjectScheme_MajorList> _T_SC_SingleProjectScheme_MajorList;
		partial void onT_SC_SingleProjectScheme_MajorListGetting();

		[JsonIgnore]
        public virtual ICollection<T_SC_SingleProjectScheme_MileStoneList> T_SC_SingleProjectScheme_MileStoneList { get { onT_SC_SingleProjectScheme_MileStoneListGetting(); return _T_SC_SingleProjectScheme_MileStoneList;} }
		private ICollection<T_SC_SingleProjectScheme_MileStoneList> _T_SC_SingleProjectScheme_MileStoneList;
		partial void onT_SC_SingleProjectScheme_MileStoneListGetting();


        public T_SC_SingleProjectScheme()
        {
            _T_SC_SingleProjectScheme_MajorList = new List<T_SC_SingleProjectScheme_MajorList>();
            _T_SC_SingleProjectScheme_MileStoneList = new List<T_SC_SingleProjectScheme_MileStoneList>();
        }
    }

	/// <summary>专业人员信息</summary>	
	[Description("专业人员信息")]
    public partial class T_SC_SingleProjectScheme_MajorList : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string T_SC_SingleProjectSchemeID { get; set; } // T_SC_SingleProjectSchemeID
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string IsReleased { get; set; } // IsReleased
		/// <summary>专业名称</summary>	
		[Description("专业名称")]
        public string MajorName { get; set; } // MajorName
		/// <summary>专业编码</summary>	
		[Description("专业编码")]
        public string MajorCode { get; set; } // MajorCode
		/// <summary>专业负责人</summary>	
		[Description("专业负责人")]
        public string MajorPrinciple { get; set; } // MajorPrinciple
		/// <summary>专业负责人名称</summary>	
		[Description("专业负责人名称")]
        public string MajorPrincipleName { get; set; } // MajorPrincipleName
		/// <summary>设计人</summary>	
		[Description("设计人")]
        public string Designer { get; set; } // Designer
		/// <summary>设计人名称</summary>	
		[Description("设计人名称")]
        public string DesignerName { get; set; } // DesignerName
		/// <summary>校对人</summary>	
		[Description("校对人")]
        public string Collactor { get; set; } // Collactor
		/// <summary>校对人名称</summary>	
		[Description("校对人名称")]
        public string CollactorName { get; set; } // CollactorName
		/// <summary>审核人</summary>	
		[Description("审核人")]
        public string Auditor { get; set; } // Auditor
		/// <summary>审核人名称</summary>	
		[Description("审核人名称")]
        public string AuditorName { get; set; } // AuditorName
		/// <summary>审定人</summary>	
		[Description("审定人")]
        public string Approver { get; set; } // Approver
		/// <summary>审定人名称</summary>	
		[Description("审定人名称")]
        public string ApproverName { get; set; } // ApproverName
		/// <summary>备注</summary>	
		[Description("备注")]
        public string Remark { get; set; } // Remark
		/// <summary>专业总工程师</summary>	
		[Description("专业总工程师")]
        public string MajorEngineer { get; set; } // MajorEngineer
		/// <summary>专业总工程师名称</summary>	
		[Description("专业总工程师名称")]
        public string MajorEngineerName { get; set; } // MajorEngineerName

        // Foreign keys
		[JsonIgnore]
        public virtual T_SC_SingleProjectScheme T_SC_SingleProjectScheme { get; set; } //  T_SC_SingleProjectSchemeID - FK_T_SC_SingleProjectScheme_MajorList_T_SC_SingleProjectScheme
    }

	/// <summary>进度计划</summary>	
	[Description("进度计划")]
    public partial class T_SC_SingleProjectScheme_MileStoneList : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string T_SC_SingleProjectSchemeID { get; set; } // T_SC_SingleProjectSchemeID
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string IsReleased { get; set; } // IsReleased
		/// <summary>名称</summary>	
		[Description("名称")]
        public string Name { get; set; } // Name
		/// <summary>专业</summary>	
		[Description("专业")]
        public string Major { get; set; } // Major
		/// <summary>计划完成日期</summary>	
		[Description("计划完成日期")]
        public DateTime? PlanEndDate { get; set; } // PlanEndDate
		/// <summary>权重（%）</summary>	
		[Description("权重（%）")]
        public decimal? Weight { get; set; } // Weight
		/// <summary>备注</summary>	
		[Description("备注")]
        public string Remark { get; set; } // Remark
		/// <summary>MileStoneType</summary>	
		[Description("MileStoneType")]
        public string MileStoneType { get; set; } // MileStoneType
		/// <summary>Code</summary>	
		[Description("Code")]
        public string Code { get; set; } // Code
		/// <summary>TemplateID</summary>	
		[Description("TemplateID")]
        public string TemplateID { get; set; } // TemplateID
		/// <summary>OutMajor</summary>	
		[Description("OutMajor")]
        public string OutMajor { get; set; } // OutMajor
		/// <summary>InMajor</summary>	
		[Description("InMajor")]
        public string InMajor { get; set; } // InMajor

        // Foreign keys
		[JsonIgnore]
        public virtual T_SC_SingleProjectScheme T_SC_SingleProjectScheme { get; set; } //  T_SC_SingleProjectSchemeID - FK_T_SC_SingleProjectScheme_MileStoneList_T_SC_SingleProjectScheme
    }

	/// <summary></summary>	
	[Description("")]
    public partial class V_I_ProjectUserInfo : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID
		/// <summary></summary>	
		[Description("")]
        public string ModeCode { get; set; } // ModeCode
		/// <summary></summary>	
		[Description("")]
        public string Name { get; set; } // Name
		/// <summary></summary>	
		[Description("")]
        public string Code { get; set; } // Code
		/// <summary></summary>	
		[Description("")]
        public string State { get; set; } // State
		/// <summary></summary>	
		[Description("")]
        public string Status { get; set; } // Status
		/// <summary></summary>	
		[Description("")]
        public string PhaseValue { get; set; } // PhaseValue
		/// <summary></summary>	
		[Description("")]
        public string PhaseName { get; set; } // PhaseName
		/// <summary></summary>	
		[Description("")]
        public string Major { get; set; } // Major
		/// <summary></summary>	
		[Description("")]
        public string CustomerID { get; set; } // CustomerID
		/// <summary></summary>	
		[Description("")]
        public string CustomerName { get; set; } // CustomerName
		/// <summary></summary>	
		[Description("")]
        public string ChargeUserID { get; set; } // ChargeUserID
		/// <summary></summary>	
		[Description("")]
        public string ChargeUserName { get; set; } // ChargeUserName
		/// <summary></summary>	
		[Description("")]
        public string ChargeDeptID { get; set; } // ChargeDeptID
		/// <summary></summary>	
		[Description("")]
        public string ChargeDeptName { get; set; } // ChargeDeptName
		/// <summary></summary>	
		[Description("")]
        public string OtherDeptID { get; set; } // OtherDeptID
		/// <summary></summary>	
		[Description("")]
        public string OtherDeptName { get; set; } // OtherDeptName
		/// <summary></summary>	
		[Description("")]
        public string GroupRootID { get; set; } // GroupRootID
		/// <summary></summary>	
		[Description("")]
        public string GroupID { get; set; } // GroupID
		/// <summary></summary>	
		[Description("")]
        public string ProjectClass { get; set; } // ProjectClass
		/// <summary></summary>	
		[Description("")]
        public decimal? Proportion { get; set; } // Proportion
		/// <summary></summary>	
		[Description("")]
        public DateTime? PlanStartDate { get; set; } // PlanStartDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? PlanFinishDate { get; set; } // PlanFinishDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? FactStartDate { get; set; } // FactStartDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? FactFinishDate { get; set; } // FactFinishDate
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get; set; } // CreateUserID
		/// <summary></summary>	
		[Description("")]
        public string CreateUser { get; set; } // CreateUser
		/// <summary></summary>	
		[Description("")]
        public DateTime CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public string ModifyUserID { get; set; } // ModifyUserID
		/// <summary></summary>	
		[Description("")]
        public string ModifyUser { get; set; } // ModifyUser
		/// <summary></summary>	
		[Description("")]
        public DateTime? ModifyDate { get; set; } // ModifyDate
		/// <summary></summary>	
		[Description("")]
        public string MarketProjectInfoID { get; set; } // MarketProjectInfoID
		/// <summary></summary>	
		[Description("")]
        public string EngineeringInfoID { get; set; } // EngineeringInfoID
		/// <summary></summary>	
		[Description("")]
        public decimal? CompletePercent { get; set; } // CompletePercent
		/// <summary></summary>	
		[Description("")]
        public string UserID { get; set; } // UserID
    }

	/// <summary></summary>	
	[Description("")]
    public partial class V_I_UserRBSInfo : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ProjectInfoID { get; set; } // ProjectInfoID
		/// <summary></summary>	
		[Description("")]
        public string WBSID { get; set; } // WBSID
		/// <summary></summary>	
		[Description("")]
        public string RoleCode { get; set; } // RoleCode
		/// <summary></summary>	
		[Description("")]
        public string RoleName { get; set; } // RoleName
		/// <summary></summary>	
		[Description("")]
        public string MajorValue { get; set; } // MajorValue
		/// <summary></summary>	
		[Description("")]
        public string UserIDs { get; set; } // UserIDs
		/// <summary></summary>	
		[Description("")]
        public string UserNames { get; set; } // UserNames
    }

	/// <summary></summary>	
	[Description("")]
    public partial class V_P_MileStone : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID
		/// <summary></summary>	
		[Description("")]
        public string ProjectInfoID { get; set; } // ProjectInfoID
		/// <summary></summary>	
		[Description("")]
        public string WBSID { get; set; } // WBSID
		/// <summary></summary>	
		[Description("")]
        public string TemplateID { get; set; } // TemplateID
		/// <summary></summary>	
		[Description("")]
        public string Name { get; set; } // Name
		/// <summary></summary>	
		[Description("")]
        public string Code { get; set; } // Code
		/// <summary></summary>	
		[Description("")]
        public int? Year { get; set; } // Year
		/// <summary></summary>	
		[Description("")]
        public int? Month { get; set; } // Month
		/// <summary></summary>	
		[Description("")]
        public int? Season { get; set; } // Season
		/// <summary></summary>	
		[Description("")]
        public string DeptID { get; set; } // DeptID
		/// <summary></summary>	
		[Description("")]
        public string DeptName { get; set; } // DeptName
		/// <summary></summary>	
		[Description("")]
        public string MileStoneValue { get; set; } // MileStoneValue
		/// <summary></summary>	
		[Description("")]
        public string MileStoneType { get; set; } // MileStoneType
		/// <summary></summary>	
		[Description("")]
        public string PhaseValue { get; set; } // PhaseValue
		/// <summary></summary>	
		[Description("")]
        public string MajorValue { get; set; } // MajorValue
		/// <summary></summary>	
		[Description("")]
        public string OutMajorValue { get; set; } // OutMajorValue
		/// <summary></summary>	
		[Description("")]
        public string Necessity { get; set; } // Necessity
		/// <summary></summary>	
		[Description("")]
        public string BindReceiptObjID { get; set; } // BindReceiptObjID
		/// <summary></summary>	
		[Description("")]
        public string BindReceiptObjName { get; set; } // BindReceiptObjName
		/// <summary></summary>	
		[Description("")]
        public DateTime? PlanStartDate { get; set; } // PlanStartDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? PlanFinishDate { get; set; } // PlanFinishDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? OrlPlanStartDate { get; set; } // OrlPlanStartDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? OrlPlanFinishDate { get; set; } // OrlPlanFinishDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? FactStartDate { get; set; } // FactStartDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? FactFinishDate { get; set; } // FactFinishDate
		/// <summary></summary>	
		[Description("")]
        public decimal? Weight { get; set; } // Weight
		/// <summary></summary>	
		[Description("")]
        public string State { get; set; } // State
		/// <summary></summary>	
		[Description("")]
        public int? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string Description { get; set; } // Description
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get; set; } // CreateUserID
		/// <summary></summary>	
		[Description("")]
        public string CreateUser { get; set; } // CreateUser
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public string ModifyUserID { get; set; } // ModifyUserID
		/// <summary></summary>	
		[Description("")]
        public string ModifyUser { get; set; } // ModifyUser
		/// <summary></summary>	
		[Description("")]
        public DateTime? ModifyDate { get; set; } // ModifyDate
		/// <summary></summary>	
		[Description("")]
        public string IncomingID { get; set; } // IncomingID
		/// <summary></summary>	
		[Description("")]
        public string ApplyUserID { get; set; } // ApplyUserID
		/// <summary></summary>	
		[Description("")]
        public string ApplyUserName { get; set; } // ApplyUserName
		/// <summary></summary>	
		[Description("")]
        public string ApplyState { get; set; } // ApplyState
		/// <summary></summary>	
		[Description("")]
        public string ApplyFiles { get; set; } // ApplyFiles
		/// <summary></summary>	
		[Description("")]
        public string ApplyRemark { get; set; } // ApplyRemark
		/// <summary></summary>	
		[Description("")]
        public DateTime? ApplyDate { get; set; } // ApplyDate
		/// <summary></summary>	
		[Description("")]
        public string ApplyApprovalUserID { get; set; } // ApplyApprovalUserID
		/// <summary></summary>	
		[Description("")]
        public string ApplyApprovalUserName { get; set; } // ApplyApprovalUserName
		/// <summary></summary>	
		[Description("")]
        public DateTime? ApplyApprovalDate { get; set; } // ApplyApprovalDate
		/// <summary></summary>	
		[Description("")]
        public string IsChecked { get; set; } // IsChecked
		/// <summary></summary>	
		[Description("")]
        public string ProjectInfoName { get; set; } // ProjectInfoName
		/// <summary></summary>	
		[Description("")]
        public int AlarmBeforeDays { get; set; } // AlarmBeforeDays
    }

	/// <summary></summary>	
	[Description("")]
    public partial class V_T_AE_Audit : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID
		/// <summary></summary>	
		[Description("")]
        public string ProjectInfoID { get; set; } // ProjectInfoID
		/// <summary></summary>	
		[Description("")]
        public string WBSID { get; set; } // WBSID
		/// <summary></summary>	
		[Description("")]
        public string FullID { get; set; } // FullID
		/// <summary></summary>	
		[Description("")]
        public string FlowDefine { get; set; } // FlowDefine
		/// <summary></summary>	
		[Description("")]
        public string FlowID { get; set; } // FlowID
		/// <summary></summary>	
		[Description("")]
        public string Name { get; set; } // Name
		/// <summary></summary>	
		[Description("")]
        public string SubmitUserID { get; set; } // SubmitUserID
		/// <summary></summary>	
		[Description("")]
        public string SubmitUser { get; set; } // SubmitUser
		/// <summary></summary>	
		[Description("")]
        public string DeptID { get; set; } // DeptID
		/// <summary></summary>	
		[Description("")]
        public string DeptName { get; set; } // DeptName
		/// <summary></summary>	
		[Description("")]
        public string DesignerID { get; set; } // DesignerID
		/// <summary></summary>	
		[Description("")]
        public string DesignerName { get; set; } // DesignerName
		/// <summary></summary>	
		[Description("")]
        public string CollactorID { get; set; } // CollactorID
		/// <summary></summary>	
		[Description("")]
        public string CollactorName { get; set; } // CollactorName
		/// <summary></summary>	
		[Description("")]
        public string AuditorID { get; set; } // AuditorID
		/// <summary></summary>	
		[Description("")]
        public string AuditorName { get; set; } // AuditorName
		/// <summary></summary>	
		[Description("")]
        public string ApproverID { get; set; } // ApproverID
		/// <summary></summary>	
		[Description("")]
        public string ApproverName { get; set; } // ApproverName
		/// <summary></summary>	
		[Description("")]
        public string State { get; set; } // State
		/// <summary></summary>	
		[Description("")]
        public string CurrentTask { get; set; } // CurrentTask
		/// <summary></summary>	
		[Description("")]
        public string CurrentUserID { get; set; } // CurrentUserID
		/// <summary></summary>	
		[Description("")]
        public string CurrentUserName { get; set; } // CurrentUserName
		/// <summary></summary>	
		[Description("")]
        public string ProjectInfoCode { get; set; } // ProjectInfoCode
		/// <summary></summary>	
		[Description("")]
        public string ProjectInfoName { get; set; } // ProjectInfoName
		/// <summary></summary>	
		[Description("")]
        public string PhaseCode { get; set; } // PhaseCode
		/// <summary></summary>	
		[Description("")]
        public string PhaseName { get; set; } // PhaseName
		/// <summary></summary>	
		[Description("")]
        public string SubProjectCode { get; set; } // SubProjectCode
		/// <summary></summary>	
		[Description("")]
        public string SubProjectName { get; set; } // SubProjectName
		/// <summary></summary>	
		[Description("")]
        public string MajorCode { get; set; } // MajorCode
		/// <summary></summary>	
		[Description("")]
        public string MajorName { get; set; } // MajorName
		/// <summary></summary>	
		[Description("")]
        public string TaskWorkCode { get; set; } // TaskWorkCode
		/// <summary></summary>	
		[Description("")]
        public string TaskWorkName { get; set; } // TaskWorkName
		/// <summary></summary>	
		[Description("")]
        public string Code { get; set; } // Code
		/// <summary></summary>	
		[Description("")]
        public string Mistake { get; set; } // Mistake
		/// <summary></summary>	
		[Description("")]
        public double? TotalToA1 { get; set; } // TotalToA1
		/// <summary></summary>	
		[Description("")]
        public int? Calculation { get; set; } // Calculation
		/// <summary></summary>	
		[Description("")]
        public int? Instruction { get; set; } // Instruction
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? FinishDate { get; set; } // FinishDate
		/// <summary></summary>	
		[Description("")]
        public string CoSignState { get; set; } // CoSignState
		/// <summary></summary>	
		[Description("")]
        public DateTime? CoSignFinishDate { get; set; } // CoSignFinishDate
    }


    // ************************************************************************
    // POCO Configuration

    // S_AE_AuditAdvice
    internal partial class S_AE_AuditAdviceConfiguration : EntityTypeConfiguration<S_AE_AuditAdvice>
    {
        public S_AE_AuditAdviceConfiguration()
        {
			ToTable("S_AE_AUDITADVICE");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.ModifyDate).HasColumnName("MODIFYDATE").IsOptional();
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUser).HasColumnName("MODIFYUSER").IsOptional().HasMaxLength(50);
            Property(x => x.OrgID).HasColumnName("ORGID").IsOptional().HasMaxLength(50);
            Property(x => x.CompanyID).HasColumnName("COMPANYID").IsOptional().HasMaxLength(50);
            Property(x => x.FlowPhase).HasColumnName("FLOWPHASE").IsOptional().HasMaxLength(50);
            Property(x => x.FlowInfo).HasColumnName("FLOWINFO").IsOptional().HasMaxLength(1073741823);
            Property(x => x.StepName).HasColumnName("STEPNAME").IsOptional().HasMaxLength(500);
            Property(x => x.ProjectInfo).HasColumnName("PROJECTINFO").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectInfoName).HasColumnName("PROJECTINFONAME").IsOptional().HasMaxLength(200);
            Property(x => x.BelongUser).HasColumnName("BELONGUSER").IsOptional().HasMaxLength(200);
            Property(x => x.BelongUserName).HasColumnName("BELONGUSERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.AuditStep).HasColumnName("AUDITSTEP").IsOptional().HasMaxLength(200);
            Property(x => x.Advice).HasColumnName("ADVICE").IsOptional().HasMaxLength(500);
            Property(x => x.Type).HasColumnName("TYPE").IsOptional().HasMaxLength(200);
            Property(x => x.Major).HasColumnName("MAJOR").IsOptional().HasMaxLength(200);
            Property(x => x.Catalog).HasColumnName("CATALOG").IsOptional().HasMaxLength(200);
            Property(x => x.Level).HasColumnName("LEVEL").IsOptional().HasMaxLength(50);
        }
    }

    // S_AE_Mistake
    internal partial class S_AE_MistakeConfiguration : EntityTypeConfiguration<S_AE_Mistake>
    {
        public S_AE_MistakeConfiguration()
        {
			ToTable("S_AE_MISTAKE");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.AuditID).HasColumnName("AUDITID").IsOptional().HasMaxLength(50);
            Property(x => x.ProjectInfoID).HasColumnName("PROJECTINFOID").IsRequired().HasMaxLength(50);
            Property(x => x.MistakeContent).HasColumnName("MISTAKECONTENT").IsOptional().HasMaxLength(500);
            Property(x => x.AuditActivityType).HasColumnName("AUDITACTIVITYTYPE").IsRequired().HasMaxLength(50);
            Property(x => x.MistakeLevel).HasColumnName("MISTAKELEVEL").IsRequired().HasMaxLength(50);
            Property(x => x.MajorCode).HasColumnName("MAJORCODE").IsRequired().HasMaxLength(50);
            Property(x => x.MajorName).HasColumnName("MAJORNAME").IsRequired().HasMaxLength(50);
            Property(x => x.DeptID).HasColumnName("DEPTID").IsOptional().HasMaxLength(200);
            Property(x => x.DeptName).HasColumnName("DEPTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.DesignerID).HasColumnName("DESIGNERID").IsRequired().HasMaxLength(50);
            Property(x => x.Designer).HasColumnName("DESIGNER").IsRequired().HasMaxLength(50);
            Property(x => x.MistakeYear).HasColumnName("MISTAKEYEAR").IsRequired();
            Property(x => x.MistakeSeason).HasColumnName("MISTAKESEASON").IsRequired();
            Property(x => x.MistakeMonth).HasColumnName("MISTAKEMONTH").IsRequired();
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsOptional().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsRequired();
            Property(x => x.Measure).HasColumnName("MEASURE").IsOptional().HasMaxLength(200);
            Property(x => x.MeasureUserID).HasColumnName("MEASUREUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.MeasureUser).HasColumnName("MEASUREUSER").IsOptional().HasMaxLength(50);
            Property(x => x.State).HasColumnName("STATE").IsOptional().HasMaxLength(50);
            Property(x => x.DrawingNO).HasColumnName("DRAWINGNO").IsOptional().HasMaxLength(500);
            Property(x => x.Postion).HasColumnName("POSTION").IsOptional().HasMaxLength(500);
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUser).HasColumnName("MODIFYUSER").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyDate).HasColumnName("MODIFYDATE").IsOptional();
            Property(x => x.ChangeAuditID).HasColumnName("CHANGEAUDITID").IsOptional().HasMaxLength(50);
        }
    }

    // S_C_Attendance
    internal partial class S_C_AttendanceConfiguration : EntityTypeConfiguration<S_C_Attendance>
    {
        public S_C_AttendanceConfiguration()
        {
			ToTable("S_C_ATTENDANCE");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.ProjectID).HasColumnName("PROJECTID").IsOptional().HasMaxLength(50);
            Property(x => x.RecorderID).HasColumnName("RECORDERID").IsOptional().HasMaxLength(50);
            Property(x => x.RecorderName).HasColumnName("RECORDERNAME").IsOptional().HasMaxLength(50);
            Property(x => x.RecordTime).HasColumnName("RECORDTIME").IsOptional();
            Property(x => x.Lat).HasColumnName("LAT").IsOptional().HasMaxLength(200);
            Property(x => x.Long).HasColumnName("LONG").IsOptional().HasMaxLength(200);
            Property(x => x.Location).HasColumnName("LOCATION").IsOptional().HasMaxLength(200);
        }
    }

    // S_C_CBS
    internal partial class S_C_CBSConfiguration : EntityTypeConfiguration<S_C_CBS>
    {
        public S_C_CBSConfiguration()
        {
			ToTable("S_C_CBS");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.ProjectInfoID).HasColumnName("PROJECTINFOID").IsOptional().HasMaxLength(50);
            Property(x => x.Name).HasColumnName("NAME").IsRequired().HasMaxLength(500);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(500);
            Property(x => x.ParentID).HasColumnName("PARENTID").IsOptional().HasMaxLength(50);
            Property(x => x.FullID).HasColumnName("FULLID").IsRequired().HasMaxLength(2000);
            Property(x => x.CBSType).HasColumnName("CBSTYPE").IsOptional().HasMaxLength(50);
            Property(x => x.NodeType).HasColumnName("NODETYPE").IsRequired().HasMaxLength(50);
            Property(x => x.UnitPrice).HasColumnName("UNITPRICE").IsOptional().HasPrecision(18,2);
            Property(x => x.Quantity).HasColumnName("QUANTITY").IsOptional().HasPrecision(18,2);
            Property(x => x.TotalPrice).HasColumnName("TOTALPRICE").IsOptional().HasPrecision(18,6);
            Property(x => x.SummaryBudgetQuantity).HasColumnName("SUMMARYBUDGETQUANTITY").IsOptional().HasPrecision(18,2);
            Property(x => x.SummaryBudgetPrice).HasColumnName("SUMMARYBUDGETPRICE").IsOptional().HasPrecision(18,6);
            Property(x => x.SummaryCostQuantity).HasColumnName("SUMMARYCOSTQUANTITY").IsOptional().HasPrecision(18,2);
            Property(x => x.SummaryCostPrice).HasColumnName("SUMMARYCOSTPRICE").IsOptional().HasPrecision(18,6);
            Property(x => x.State).HasColumnName("STATE").IsOptional().HasMaxLength(50);
            Property(x => x.Remark).HasColumnName("REMARK").IsOptional().HasMaxLength(500);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsOptional().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUser).HasColumnName("MODIFYUSER").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyDate).HasColumnName("MODIFYDATE").IsOptional();

            // Foreign keys
            HasOptional(a => a.S_I_ProjectInfo).WithMany(b => b.S_C_CBS).HasForeignKey(c => c.ProjectInfoID); // FK_S_C_CBS_S_I_ProjectInfo
        }
    }

    // S_C_CBS_Budget
    internal partial class S_C_CBS_BudgetConfiguration : EntityTypeConfiguration<S_C_CBS_Budget>
    {
        public S_C_CBS_BudgetConfiguration()
        {
			ToTable("S_C_CBS_BUDGET");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.CBSID).HasColumnName("CBSID").IsRequired().HasMaxLength(50);
            Property(x => x.CBSFullID).HasColumnName("CBSFULLID").IsRequired().HasMaxLength(2000);
            Property(x => x.ProjectInfoID).HasColumnName("PROJECTINFOID").IsRequired().HasMaxLength(50);
            Property(x => x.ParentID).HasColumnName("PARENTID").IsOptional().HasMaxLength(50);
            Property(x => x.FullID).HasColumnName("FULLID").IsRequired().HasMaxLength(2000);
            Property(x => x.Name).HasColumnName("NAME").IsRequired().HasMaxLength(500);
            Property(x => x.Code).HasColumnName("CODE").IsRequired().HasMaxLength(500);
            Property(x => x.UnitPrice).HasColumnName("UNITPRICE").IsOptional().HasPrecision(18,6);
            Property(x => x.Quantity).HasColumnName("QUANTITY").IsOptional().HasPrecision(18,2);
            Property(x => x.TotalValue).HasColumnName("TOTALVALUE").IsOptional().HasPrecision(18,6);
            Property(x => x.Unit).HasColumnName("UNIT").IsOptional().HasMaxLength(50);
            Property(x => x.Remark).HasColumnName("REMARK").IsOptional().HasMaxLength(500);
            Property(x => x.SummaryCostQuantity).HasColumnName("SUMMARYCOSTQUANTITY").IsOptional().HasPrecision(18,2);
            Property(x => x.SummaryCostValue).HasColumnName("SUMMARYCOSTVALUE").IsOptional().HasPrecision(18,6);

            // Foreign keys
            HasRequired(a => a.S_C_CBS).WithMany(b => b.S_C_CBS_Budget).HasForeignKey(c => c.CBSID); // FK_S_C_CBS_Budget_S_C_CBS
        }
    }

    // S_C_CBS_Cost
    internal partial class S_C_CBS_CostConfiguration : EntityTypeConfiguration<S_C_CBS_Cost>
    {
        public S_C_CBS_CostConfiguration()
        {
			ToTable("S_C_CBS_COST");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.CBSID).HasColumnName("CBSID").IsOptional().HasMaxLength(50);
            Property(x => x.CBSFullID).HasColumnName("CBSFULLID").IsOptional().HasMaxLength(2000);
            Property(x => x.ProjectInfoID).HasColumnName("PROJECTINFOID").IsOptional().HasMaxLength(50);
            Property(x => x.BudgetID).HasColumnName("BUDGETID").IsOptional().HasMaxLength(50);
            Property(x => x.BudgetFullID).HasColumnName("BUDGETFULLID").IsOptional().HasMaxLength(2000);
            Property(x => x.FormID).HasColumnName("FORMID").IsOptional().HasMaxLength(50);
            Property(x => x.Name).HasColumnName("NAME").IsRequired().HasMaxLength(500);
            Property(x => x.Code).HasColumnName("CODE").IsRequired().HasMaxLength(500);
            Property(x => x.UnitPrice).HasColumnName("UNITPRICE").IsOptional().HasPrecision(18,6);
            Property(x => x.Quantity).HasColumnName("QUANTITY").IsOptional().HasPrecision(18,2);
            Property(x => x.TotalValue).HasColumnName("TOTALVALUE").IsOptional().HasPrecision(18,6);
            Property(x => x.CostDate).HasColumnName("COSTDATE").IsOptional();
            Property(x => x.CostUser).HasColumnName("COSTUSER").IsOptional().HasMaxLength(50);
            Property(x => x.CostUserName).HasColumnName("COSTUSERNAME").IsOptional().HasMaxLength(50);
            Property(x => x.BelongYear).HasColumnName("BELONGYEAR").IsOptional();
            Property(x => x.BelongQuarter).HasColumnName("BELONGQUARTER").IsOptional();
            Property(x => x.BelongMonth).HasColumnName("BELONGMONTH").IsOptional();
            Property(x => x.BelongDept).HasColumnName("BELONGDEPT").IsOptional().HasMaxLength(500);
            Property(x => x.BelongDeptName).HasColumnName("BELONGDEPTNAME").IsOptional().HasMaxLength(500);
            Property(x => x.UserDept).HasColumnName("USERDEPT").IsOptional().HasMaxLength(500);
            Property(x => x.UserDeptName).HasColumnName("USERDEPTNAME").IsOptional().HasMaxLength(500);
            Property(x => x.RoleCode).HasColumnName("ROLECODE").IsOptional().HasMaxLength(200);
            Property(x => x.RoleName).HasColumnName("ROLENAME").IsOptional().HasMaxLength(200);
            Property(x => x.TaskWorkCode).HasColumnName("TASKWORKCODE").IsOptional().HasMaxLength(500);
            Property(x => x.TaskWorkName).HasColumnName("TASKWORKNAME").IsOptional().HasMaxLength(500);
            Property(x => x.MajorCode).HasColumnName("MAJORCODE").IsOptional().HasMaxLength(500);
            Property(x => x.MajorName).HasColumnName("MAJORNAME").IsOptional().HasMaxLength(500);

            // Foreign keys
            HasOptional(a => a.S_C_CBS).WithMany(b => b.S_C_CBS_Cost).HasForeignKey(c => c.CBSID); // FK_S_C_CBS_Cost_S_C_CBS
        }
    }

    // S_D_DataCollection
    internal partial class S_D_DataCollectionConfiguration : EntityTypeConfiguration<S_D_DataCollection>
    {
        public S_D_DataCollectionConfiguration()
        {
			ToTable("S_D_DATACOLLECTION");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.ModifyDate).HasColumnName("MODIFYDATE").IsOptional();
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUser).HasColumnName("MODIFYUSER").IsOptional().HasMaxLength(50);
            Property(x => x.OrgID).HasColumnName("ORGID").IsOptional().HasMaxLength(50);
            Property(x => x.CompanyID).HasColumnName("COMPANYID").IsOptional().HasMaxLength(50);
            Property(x => x.FlowPhase).HasColumnName("FLOWPHASE").IsOptional().HasMaxLength(50);
            Property(x => x.StepName).HasColumnName("STEPNAME").IsOptional().HasMaxLength(500);
            Property(x => x.DataName).HasColumnName("DATANAME").IsOptional().HasMaxLength(200);
            Property(x => x.FileName).HasColumnName("FILENAME").IsOptional().HasMaxLength(200);
            Property(x => x.Attachment).HasColumnName("ATTACHMENT").IsOptional().HasMaxLength(200);
            Property(x => x.GroupInfoID).HasColumnName("GROUPINFOID").IsOptional().HasMaxLength(200);
        }
    }

    // S_D_DBS
    internal partial class S_D_DBSConfiguration : EntityTypeConfiguration<S_D_DBS>
    {
        public S_D_DBSConfiguration()
        {
			ToTable("S_D_DBS");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.ProjectInfoID).HasColumnName("PROJECTINFOID").IsRequired().HasMaxLength(50);
            Property(x => x.Name).HasColumnName("NAME").IsRequired().HasMaxLength(500);
            Property(x => x.DBSCode).HasColumnName("DBSCODE").IsOptional().HasMaxLength(500);
            Property(x => x.DBSType).HasColumnName("DBSTYPE").IsRequired().HasMaxLength(50);
            Property(x => x.ParentID).HasColumnName("PARENTID").IsOptional().HasMaxLength(50);
            Property(x => x.FullID).HasColumnName("FULLID").IsRequired().HasMaxLength(500);
            Property(x => x.InheritAuth).HasColumnName("INHERITAUTH").IsRequired().HasMaxLength(50);
            Property(x => x.MappingType).HasColumnName("MAPPINGTYPE").IsOptional().HasMaxLength(50);
            Property(x => x.MappingNodeUrl).HasColumnName("MAPPINGNODEURL").IsOptional().HasMaxLength(500);
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsRequired().HasMaxLength(50);
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsRequired().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsRequired();
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUser).HasColumnName("MODIFYUSER").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyDate).HasColumnName("MODIFYDATE").IsOptional();
            Property(x => x.ConfigDBSID).HasColumnName("CONFIGDBSID").IsOptional().HasMaxLength(500);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.ArchiveFolder).HasColumnName("ARCHIVEFOLDER").IsOptional().HasMaxLength(50);
            Property(x => x.ArchiveFolderName).HasColumnName("ARCHIVEFOLDERNAME").IsOptional().HasMaxLength(500);
            Property(x => x.IsPublic).HasColumnName("ISPUBLIC").IsRequired();
            Property(x => x.CooperationOrgID).HasColumnName("COOPERATIONORGID").IsOptional().HasMaxLength(50);

            // Foreign keys
            HasRequired(a => a.S_I_ProjectInfo).WithMany(b => b.S_D_DBS).HasForeignKey(c => c.ProjectInfoID); // FK_S_D_DBS_S_I_ProjectInfo
        }
    }

    // S_D_DBSArchiveList
    internal partial class S_D_DBSArchiveListConfiguration : EntityTypeConfiguration<S_D_DBSArchiveList>
    {
        public S_D_DBSArchiveListConfiguration()
        {
			ToTable("S_D_DBSARCHIVELIST");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.ModifyDate).HasColumnName("MODIFYDATE").IsOptional();
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsOptional().HasMaxLength(200);
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUser).HasColumnName("MODIFYUSER").IsOptional().HasMaxLength(50);
            Property(x => x.OrgID).HasColumnName("ORGID").IsOptional().HasMaxLength(50);
            Property(x => x.CompanyID).HasColumnName("COMPANYID").IsOptional().HasMaxLength(50);
            Property(x => x.FlowPhase).HasColumnName("FLOWPHASE").IsOptional().HasMaxLength(50);
            Property(x => x.FlowInfo).HasColumnName("FLOWINFO").IsOptional().HasMaxLength(1073741823);
            Property(x => x.StepName).HasColumnName("STEPNAME").IsOptional().HasMaxLength(500);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(200);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectClass).HasColumnName("PROJECTCLASS").IsOptional().HasMaxLength(200);
            Property(x => x.CreateUserName).HasColumnName("CREATEUSERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectInfoID).HasColumnName("PROJECTINFOID").IsOptional().HasMaxLength(200);
            Property(x => x.SerialNumber).HasColumnName("SERIALNUMBER").IsOptional().HasMaxLength(200);
            Property(x => x.Remark).HasColumnName("REMARK").IsOptional().HasMaxLength(500);
            Property(x => x.ArchiveType).HasColumnName("ARCHIVETYPE").IsOptional().HasMaxLength(200);
        }
    }

    // S_D_DBSArchiveList_ArchiveFiles
    internal partial class S_D_DBSArchiveList_ArchiveFilesConfiguration : EntityTypeConfiguration<S_D_DBSArchiveList_ArchiveFiles>
    {
        public S_D_DBSArchiveList_ArchiveFilesConfiguration()
        {
			ToTable("S_D_DBSARCHIVELIST_ARCHIVEFILES");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.S_D_DBSArchiveListID).HasColumnName("S_D_DBSARCHIVELISTID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(200);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(200);
            Property(x => x.Catagory).HasColumnName("CATAGORY").IsOptional().HasMaxLength(200);
            Property(x => x.MajorValue).HasColumnName("MAJORVALUE").IsOptional().HasMaxLength(200);
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsOptional().HasMaxLength(200);
            Property(x => x.CreateUserName).HasColumnName("CREATEUSERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.MainFiles).HasColumnName("MAINFILES").IsOptional().HasMaxLength(200);
            Property(x => x.DocumentID).HasColumnName("DOCUMENTID").IsOptional().HasMaxLength(200);
            Property(x => x.NewAdd).HasColumnName("NEWADD").IsOptional().HasMaxLength(200);

            // Foreign keys
            HasOptional(a => a.S_D_DBSArchiveList).WithMany(b => b.S_D_DBSArchiveList_ArchiveFiles).HasForeignKey(c => c.S_D_DBSArchiveListID); // FK_S_D_DBSArchiveList_ArchiveFiles_S_D_DBSArchiveList
        }
    }

    // S_D_DBSSecurity
    internal partial class S_D_DBSSecurityConfiguration : EntityTypeConfiguration<S_D_DBSSecurity>
    {
        public S_D_DBSSecurityConfiguration()
        {
			ToTable("S_D_DBSSECURITY");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.DBSID).HasColumnName("DBSID").IsRequired().HasMaxLength(50);
            Property(x => x.RoleCode).HasColumnName("ROLECODE").IsRequired().HasMaxLength(50);
            Property(x => x.RoleName).HasColumnName("ROLENAME").IsRequired().HasMaxLength(50);
            Property(x => x.AuthType).HasColumnName("AUTHTYPE").IsRequired().HasMaxLength(50);
            Property(x => x.RelateValue).HasColumnName("RELATEVALUE").IsOptional().HasMaxLength(10);
            Property(x => x.RoleType).HasColumnName("ROLETYPE").IsOptional().HasMaxLength(50);

            // Foreign keys
            HasRequired(a => a.S_D_DBS).WithMany(b => b.S_D_DBSSecurity).HasForeignKey(c => c.DBSID); // FK_S_D_DBSSecurity_S_D_DBS
        }
    }

    // S_D_Document
    internal partial class S_D_DocumentConfiguration : EntityTypeConfiguration<S_D_Document>
    {
        public S_D_DocumentConfiguration()
        {
			ToTable("S_D_DOCUMENT");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.ProjectInfoID).HasColumnName("PROJECTINFOID").IsRequired().HasMaxLength(50);
            Property(x => x.DBSID).HasColumnName("DBSID").IsRequired().HasMaxLength(50);
            Property(x => x.DBSFullID).HasColumnName("DBSFULLID").IsRequired().HasMaxLength(500);
            Property(x => x.Name).HasColumnName("NAME").IsRequired().HasMaxLength(500);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(200);
            Property(x => x.MajorValue).HasColumnName("MAJORVALUE").IsOptional().HasMaxLength(50);
            Property(x => x.Catagory).HasColumnName("CATAGORY").IsOptional().HasMaxLength(50);
            Property(x => x.FileType).HasColumnName("FILETYPE").IsOptional().HasMaxLength(50);
            Property(x => x.MainFiles).HasColumnName("MAINFILES").IsOptional().HasMaxLength(500);
            Property(x => x.PDFFile).HasColumnName("PDFFILE").IsOptional().HasMaxLength(500);
            Property(x => x.PlotFile).HasColumnName("PLOTFILE").IsOptional().HasMaxLength(500);
            Property(x => x.XrefFile).HasColumnName("XREFFILE").IsOptional().HasMaxLength(500);
            Property(x => x.DwfFile).HasColumnName("DWFFILE").IsOptional().HasMaxLength(500);
            Property(x => x.TiffFile).HasColumnName("TIFFFILE").IsOptional().HasMaxLength(500);
            Property(x => x.SignPdfFile).HasColumnName("SIGNPDFFILE").IsOptional().HasMaxLength(500);
            Property(x => x.Attr).HasColumnName("ATTR").IsOptional().HasMaxLength(1073741823);
            Property(x => x.Attachments).HasColumnName("ATTACHMENTS").IsOptional().HasMaxLength(500);
            Property(x => x.RelateTable).HasColumnName("RELATETABLE").IsOptional().HasMaxLength(50);
            Property(x => x.RelateID).HasColumnName("RELATEID").IsOptional().HasMaxLength(50);
            Property(x => x.Version).HasColumnName("VERSION").IsOptional().HasMaxLength(50);
            Property(x => x.Description).HasColumnName("DESCRIPTION").IsOptional();
            Property(x => x.State).HasColumnName("STATE").IsRequired().HasMaxLength(50);
            Property(x => x.ArchiveDate).HasColumnName("ARCHIVEDATE").IsOptional();
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsRequired().HasMaxLength(50);
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsRequired().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsRequired();
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUser).HasColumnName("MODIFYUSER").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyDate).HasColumnName("MODIFYDATE").IsOptional();
            Property(x => x.IsPublic).HasColumnName("ISPUBLIC").IsOptional();
            Property(x => x.CooperationOrgID).HasColumnName("COOPERATIONORGID").IsOptional().HasMaxLength(50);
            Property(x => x.FontFile).HasColumnName("FONTFILE").IsOptional().HasMaxLength(500);

            // Foreign keys
            HasRequired(a => a.S_D_DBS).WithMany(b => b.S_D_Document).HasForeignKey(c => c.DBSID); // FK_S_D_Document_S_D_DBS
        }
    }

    // S_D_DocumentVersion
    internal partial class S_D_DocumentVersionConfiguration : EntityTypeConfiguration<S_D_DocumentVersion>
    {
        public S_D_DocumentVersionConfiguration()
        {
			ToTable("S_D_DOCUMENTVERSION");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.DocumentID).HasColumnName("DOCUMENTID").IsOptional().HasMaxLength(50);
            Property(x => x.DBSID).HasColumnName("DBSID").IsRequired().HasMaxLength(50);
            Property(x => x.Name).HasColumnName("NAME").IsRequired().HasMaxLength(500);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(200);
            Property(x => x.FileType).HasColumnName("FILETYPE").IsOptional().HasMaxLength(50);
            Property(x => x.MainFiles).HasColumnName("MAINFILES").IsOptional().HasMaxLength(500);
            Property(x => x.PDFFile).HasColumnName("PDFFILE").IsOptional().HasMaxLength(500);
            Property(x => x.PlotFile).HasColumnName("PLOTFILE").IsOptional().HasMaxLength(500);
            Property(x => x.XrefFile).HasColumnName("XREFFILE").IsOptional().HasMaxLength(500);
            Property(x => x.DwfFile).HasColumnName("DWFFILE").IsOptional().HasMaxLength(500);
            Property(x => x.TiffFile).HasColumnName("TIFFFILE").IsOptional().HasMaxLength(500);
            Property(x => x.SignPdfFile).HasColumnName("SIGNPDFFILE").IsOptional().HasMaxLength(500);
            Property(x => x.Attachments).HasColumnName("ATTACHMENTS").IsOptional().HasMaxLength(500);
            Property(x => x.Version).HasColumnName("VERSION").IsOptional().HasMaxLength(50);
            Property(x => x.Description).HasColumnName("DESCRIPTION").IsOptional();
            Property(x => x.State).HasColumnName("STATE").IsRequired().HasMaxLength(50);
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsRequired().HasMaxLength(50);
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsRequired().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsRequired();
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUser).HasColumnName("MODIFYUSER").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyDate).HasColumnName("MODIFYDATE").IsOptional();
            Property(x => x.FontFile).HasColumnName("FONTFILE").IsOptional().HasMaxLength(500);

            // Foreign keys
            HasOptional(a => a.S_D_Document).WithMany(b => b.S_D_DocumentVersion).HasForeignKey(c => c.DocumentID); // FK_S_D_DocumentVersion_S_D_Document
        }
    }

    // S_D_Input
    internal partial class S_D_InputConfiguration : EntityTypeConfiguration<S_D_Input>
    {
        public S_D_InputConfiguration()
        {
			ToTable("S_D_INPUT");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.ProjectInfoID).HasColumnName("PROJECTINFOID").IsRequired().HasMaxLength(50);
            Property(x => x.EngineeringInfoID).HasColumnName("ENGINEERINGINFOID").IsRequired().HasMaxLength(50);
            Property(x => x.InfoName).HasColumnName("INFONAME").IsRequired().HasMaxLength(200);
            Property(x => x.Catagory).HasColumnName("CATAGORY").IsRequired().HasMaxLength(50);
            Property(x => x.InputType).HasColumnName("INPUTTYPE").IsOptional().HasMaxLength(50);
            Property(x => x.DocType).HasColumnName("DOCTYPE").IsOptional().HasMaxLength(50);
            Property(x => x.DBSCode).HasColumnName("DBSCODE").IsOptional().HasMaxLength(200);
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUser).HasColumnName("MODIFYUSER").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyDate).HasColumnName("MODIFYDATE").IsOptional();
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsOptional().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.RegisteDate).HasColumnName("REGISTEDATE").IsOptional();
            Property(x => x.State).HasColumnName("STATE").IsOptional().HasMaxLength(50);
            Property(x => x.InputTypeIndex).HasColumnName("INPUTTYPEINDEX").IsOptional();
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();

            // Foreign keys
            HasRequired(a => a.S_I_ProjectInfo).WithMany(b => b.S_D_Input).HasForeignKey(c => c.ProjectInfoID); // FK_E_W_Input_S_I_ProjectInfo
        }
    }

    // S_D_InputDocument
    internal partial class S_D_InputDocumentConfiguration : EntityTypeConfiguration<S_D_InputDocument>
    {
        public S_D_InputDocumentConfiguration()
        {
			ToTable("S_D_INPUTDOCUMENT");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.InputID).HasColumnName("INPUTID").IsRequired().HasMaxLength(50);
            Property(x => x.Name).HasColumnName("NAME").IsRequired().HasMaxLength(500);
            Property(x => x.Catagory).HasColumnName("CATAGORY").IsRequired().HasMaxLength(50);
            Property(x => x.Files).HasColumnName("FILES").IsRequired().HasMaxLength(500);
            Property(x => x.AuditState).HasColumnName("AUDITSTATE").IsOptional().HasMaxLength(50);
            Property(x => x.Attachments).HasColumnName("ATTACHMENTS").IsOptional().HasMaxLength(500);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);

            // Foreign keys
            HasRequired(a => a.S_D_Input).WithMany(b => b.S_D_InputDocument).HasForeignKey(c => c.InputID); // FK_S_D_InputDocument_S_D_Input
        }
    }

    // S_D_ShareInfo
    internal partial class S_D_ShareInfoConfiguration : EntityTypeConfiguration<S_D_ShareInfo>
    {
        public S_D_ShareInfoConfiguration()
        {
			ToTable("S_D_SHAREINFO");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.ModifyDate).HasColumnName("MODIFYDATE").IsOptional();
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUser).HasColumnName("MODIFYUSER").IsOptional().HasMaxLength(50);
            Property(x => x.OrgID).HasColumnName("ORGID").IsOptional().HasMaxLength(50);
            Property(x => x.CompanyID).HasColumnName("COMPANYID").IsOptional().HasMaxLength(50);
            Property(x => x.FlowPhase).HasColumnName("FLOWPHASE").IsOptional().HasMaxLength(50);
            Property(x => x.StepName).HasColumnName("STEPNAME").IsOptional().HasMaxLength(500);
            Property(x => x.InfoDetial).HasColumnName("INFODETIAL").IsOptional().HasMaxLength(500);
            Property(x => x.FileName).HasColumnName("FILENAME").IsOptional().HasMaxLength(200);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(200);
            Property(x => x.Type).HasColumnName("TYPE").IsOptional().HasMaxLength(200);
            Property(x => x.Source).HasColumnName("SOURCE").IsOptional().HasMaxLength(200);
            Property(x => x.PhaseValue).HasColumnName("PHASEVALUE").IsOptional().HasMaxLength(200);
            Property(x => x.WBSValue).HasColumnName("WBSVALUE").IsOptional().HasMaxLength(200);
            Property(x => x.Register).HasColumnName("REGISTER").IsOptional().HasMaxLength(200);
            Property(x => x.RegisterName).HasColumnName("REGISTERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Annex).HasColumnName("ANNEX").IsOptional().HasMaxLength(500);
            Property(x => x.Remarks).HasColumnName("REMARKS").IsOptional().HasMaxLength(500);
            Property(x => x.SubProjectWBSID).HasColumnName("SUBPROJECTWBSID").IsOptional().HasMaxLength(50);
            Property(x => x.ProjectInfoID).HasColumnName("PROJECTINFOID").IsOptional().HasMaxLength(50);
            Property(x => x.BatchID).HasColumnName("BATCHID").IsOptional().HasMaxLength(50);
            Property(x => x.AuditState).HasColumnName("AUDITSTATE").IsOptional().HasMaxLength(50);
            Property(x => x.CooperationPlan).HasColumnName("COOPERATIONPLAN").IsOptional().HasMaxLength(200);
            Property(x => x.CooperationPlanName).HasColumnName("COOPERATIONPLANNAME").IsOptional().HasMaxLength(200);
            Property(x => x.FormID).HasColumnName("FORMID").IsOptional().HasMaxLength(200);
            Property(x => x.SourceType).HasColumnName("SOURCETYPE").IsOptional().HasMaxLength(200);
            Property(x => x.DrawingSource).HasColumnName("DRAWINGSOURCE").IsOptional().HasMaxLength(50);
            Property(x => x.DrawingID).HasColumnName("DRAWINGID").IsOptional().HasMaxLength(50);

            // Foreign keys
            HasOptional(a => a.S_I_ProjectInfo).WithMany(b => b.S_D_ShareInfo).HasForeignKey(c => c.ProjectInfoID); // FK_S_D_ShareInfo_S_I_ProjectInfo
        }
    }

    // S_E_ForceProject
    internal partial class S_E_ForceProjectConfiguration : EntityTypeConfiguration<S_E_ForceProject>
    {
        public S_E_ForceProjectConfiguration()
        {
			ToTable("S_E_FORCEPROJECT");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.ModifyDate).HasColumnName("MODIFYDATE").IsOptional();
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUser).HasColumnName("MODIFYUSER").IsOptional().HasMaxLength(50);
            Property(x => x.OrgID).HasColumnName("ORGID").IsOptional().HasMaxLength(50);
            Property(x => x.CompanyID).HasColumnName("COMPANYID").IsOptional().HasMaxLength(50);
            Property(x => x.FlowPhase).HasColumnName("FLOWPHASE").IsOptional().HasMaxLength(50);
            Property(x => x.FlowInfo).HasColumnName("FLOWINFO").IsOptional().HasMaxLength(1073741823);
            Property(x => x.StepName).HasColumnName("STEPNAME").IsOptional().HasMaxLength(500);
            Property(x => x.ProjectInfoID).HasColumnName("PROJECTINFOID").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectInfoIDName).HasColumnName("PROJECTINFOIDNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectInfoName).HasColumnName("PROJECTINFONAME").IsOptional().HasMaxLength(500);
            Property(x => x.ProjectInfoCode).HasColumnName("PROJECTINFOCODE").IsOptional().HasMaxLength(500);
            Property(x => x.ForceUser).HasColumnName("FORCEUSER").IsOptional().HasMaxLength(50);
            Property(x => x.ForceUserName).HasColumnName("FORCEUSERNAME").IsOptional().HasMaxLength(50);
            Property(x => x.MarketProjectInfoID).HasColumnName("MARKETPROJECTINFOID").IsOptional().HasMaxLength(50);
            Property(x => x.MarketID).HasColumnName("MARKETID").IsOptional().HasMaxLength(50);
        }
    }

    // S_E_ForceProjectChargeUser
    internal partial class S_E_ForceProjectChargeUserConfiguration : EntityTypeConfiguration<S_E_ForceProjectChargeUser>
    {
        public S_E_ForceProjectChargeUserConfiguration()
        {
			ToTable("S_E_FORCEPROJECTCHARGEUSER");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.ModifyDate).HasColumnName("MODIFYDATE").IsOptional();
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUser).HasColumnName("MODIFYUSER").IsOptional().HasMaxLength(50);
            Property(x => x.OrgID).HasColumnName("ORGID").IsOptional().HasMaxLength(50);
            Property(x => x.CompanyID).HasColumnName("COMPANYID").IsOptional().HasMaxLength(50);
            Property(x => x.FlowPhase).HasColumnName("FLOWPHASE").IsOptional().HasMaxLength(50);
            Property(x => x.FlowInfo).HasColumnName("FLOWINFO").IsOptional().HasMaxLength(1073741823);
            Property(x => x.StepName).HasColumnName("STEPNAME").IsOptional().HasMaxLength(500);
            Property(x => x.ChargeUser).HasColumnName("CHARGEUSER").IsOptional().HasMaxLength(200);
            Property(x => x.ChargeUserName).HasColumnName("CHARGEUSERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ForceUser).HasColumnName("FORCEUSER").IsOptional().HasMaxLength(200);
            Property(x => x.ForceUserName).HasColumnName("FORCEUSERNAME").IsOptional().HasMaxLength(200);
        }
    }

    // S_E_Product
    internal partial class S_E_ProductConfiguration : EntityTypeConfiguration<S_E_Product>
    {
        public S_E_ProductConfiguration()
        {
			ToTable("S_E_PRODUCT");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.ProjectInfoID).HasColumnName("PROJECTINFOID").IsRequired().HasMaxLength(50);
            Property(x => x.WBSID).HasColumnName("WBSID").IsRequired().HasMaxLength(50);
            Property(x => x.WBSFullID).HasColumnName("WBSFULLID").IsRequired().HasMaxLength(500);
            Property(x => x.Name).HasColumnName("NAME").IsRequired().HasMaxLength(500);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(200);
            Property(x => x.SN).HasColumnName("SN").IsOptional().HasMaxLength(200);
            Property(x => x.MajorValue).HasColumnName("MAJORVALUE").IsRequired().HasMaxLength(50);
            Property(x => x.PhaseValue).HasColumnName("PHASEVALUE").IsOptional().HasMaxLength(50);
            Property(x => x.SubProjectInfo).HasColumnName("SUBPROJECTINFO").IsOptional().HasMaxLength(500);
            Property(x => x.MonomerInfo).HasColumnName("MONOMERINFO").IsOptional().HasMaxLength(500);
            Property(x => x.AuditID).HasColumnName("AUDITID").IsOptional().HasMaxLength(50);
            Property(x => x.Designer).HasColumnName("DESIGNER").IsOptional().HasMaxLength(200);
            Property(x => x.DesignerName).HasColumnName("DESIGNERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Collactor).HasColumnName("COLLACTOR").IsOptional().HasMaxLength(200);
            Property(x => x.CollactorName).HasColumnName("COLLACTORNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Auditor).HasColumnName("AUDITOR").IsOptional().HasMaxLength(200);
            Property(x => x.AuditorName).HasColumnName("AUDITORNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Approver).HasColumnName("APPROVER").IsOptional().HasMaxLength(200);
            Property(x => x.ApproverName).HasColumnName("APPROVERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.CounterSignAuditID).HasColumnName("COUNTERSIGNAUDITID").IsOptional().HasMaxLength(50);
            Property(x => x.AuditState).HasColumnName("AUDITSTATE").IsRequired().HasMaxLength(50);
            Property(x => x.CoSignState).HasColumnName("COSIGNSTATE").IsOptional().HasMaxLength(50);
            Property(x => x.PrintState).HasColumnName("PRINTSTATE").IsRequired().HasMaxLength(50);
            Property(x => x.ArchiveState).HasColumnName("ARCHIVESTATE").IsRequired().HasMaxLength(50);
            Property(x => x.SignState).HasColumnName("SIGNSTATE").IsOptional().HasMaxLength(500);
            Property(x => x.SubmitDate).HasColumnName("SUBMITDATE").IsOptional();
            Property(x => x.CoSignDate).HasColumnName("COSIGNDATE").IsOptional();
            Property(x => x.AuditPassDate).HasColumnName("AUDITPASSDATE").IsOptional();
            Property(x => x.ArchiveDate).HasColumnName("ARCHIVEDATE").IsOptional();
            Property(x => x.State).HasColumnName("STATE").IsRequired().HasMaxLength(50);
            Property(x => x.ToA1).HasColumnName("TOA1").IsOptional().HasPrecision(18,4);
            Property(x => x.FileType).HasColumnName("FILETYPE").IsRequired().HasMaxLength(50);
            Property(x => x.Version).HasColumnName("VERSION").IsOptional().HasPrecision(18,2);
            Property(x => x.MainFile).HasColumnName("MAINFILE").IsOptional();
            Property(x => x.PdfFile).HasColumnName("PDFFILE").IsOptional();
            Property(x => x.PlotFile).HasColumnName("PLOTFILE").IsOptional();
            Property(x => x.SwfFile).HasColumnName("SWFFILE").IsOptional().HasMaxLength(50);
            Property(x => x.ShotSnap).HasColumnName("SHOTSNAP").IsOptional().HasMaxLength(50);
            Property(x => x.XrefFile).HasColumnName("XREFFILE").IsOptional();
            Property(x => x.FontFile).HasColumnName("FONTFILE").IsOptional();
            Property(x => x.MemoData).HasColumnName("MEMODATA").IsOptional().HasMaxLength(1073741823);
            Property(x => x.Attachments).HasColumnName("ATTACHMENTS").IsOptional();
            Property(x => x.Description).HasColumnName("DESCRIPTION").IsOptional();
            Property(x => x.FullContext).HasColumnName("FULLCONTEXT").IsOptional().HasMaxLength(1073741823);
            Property(x => x.AuditSignUser).HasColumnName("AUDITSIGNUSER").IsOptional().HasMaxLength(1073741823);
            Property(x => x.CoSignUser).HasColumnName("COSIGNUSER").IsOptional().HasMaxLength(1073741823);
            Property(x => x.PrintCount).HasColumnName("PRINTCOUNT").IsOptional();
            Property(x => x.IsCoSign).HasColumnName("ISCOSIGN").IsOptional().HasMaxLength(50);
            Property(x => x.CoSignMajor).HasColumnName("COSIGNMAJOR").IsOptional().HasMaxLength(500);
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsRequired().HasMaxLength(50);
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsRequired().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsRequired();
            Property(x => x.ModifyDate).HasColumnName("MODIFYDATE").IsOptional();
            Property(x => x.ModifyUser).HasColumnName("MODIFYUSER").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.FileSize).HasColumnName("FILESIZE").IsOptional().HasMaxLength(50);
            Property(x => x.ParentID).HasColumnName("PARENTID").IsOptional().HasMaxLength(50);
            Property(x => x.FullID).HasColumnName("FULLID").IsOptional().HasMaxLength(500);
            Property(x => x.BatchID).HasColumnName("BATCHID").IsOptional().HasMaxLength(50);
            Property(x => x.PDFSignPositionInfo).HasColumnName("PDFSIGNPOSITIONINFO").IsOptional();
            Property(x => x.PDFAuditFiles).HasColumnName("PDFAUDITFILES").IsOptional();
            Property(x => x.ParentVersion).HasColumnName("PARENTVERSION").IsOptional().HasPrecision(18,2);
            Property(x => x.PackageCode).HasColumnName("PACKAGECODE").IsOptional().HasMaxLength(200);
            Property(x => x.PackageName).HasColumnName("PACKAGENAME").IsOptional().HasMaxLength(200);
            Property(x => x.MonomerCode).HasColumnName("MONOMERCODE").IsOptional().HasMaxLength(200);
            Property(x => x.ChangeAuditID).HasColumnName("CHANGEAUDITID").IsOptional().HasMaxLength(50);
            Property(x => x.DwfFile).HasColumnName("DWFFILE").IsOptional();
            Property(x => x.TiffFile).HasColumnName("TIFFFILE").IsOptional();
            Property(x => x.PlotSealGroup).HasColumnName("PLOTSEALGROUP").IsOptional().HasMaxLength(2000);
            Property(x => x.PlotSealGroupName).HasColumnName("PLOTSEALGROUPNAME").IsOptional().HasMaxLength(500);
            Property(x => x.SignPdfFile).HasColumnName("SIGNPDFFILE").IsOptional();
            Property(x => x.FrameAllAttInfo).HasColumnName("FRAMEALLATTINFO").IsOptional();
            Property(x => x.AreaInfo).HasColumnName("AREAINFO").IsOptional().HasMaxLength(200);
            Property(x => x.AreaCode).HasColumnName("AREACODE").IsOptional().HasMaxLength(200);
            Property(x => x.DeviceInfo).HasColumnName("DEVICEINFO").IsOptional().HasMaxLength(200);
            Property(x => x.DeviceCode).HasColumnName("DEVICECODE").IsOptional().HasMaxLength(200);
            Property(x => x.SourceID).HasColumnName("SOURCEID").IsOptional().HasMaxLength(50);
            Property(x => x.Edition).HasColumnName("EDITION").IsOptional().HasMaxLength(50);
            Property(x => x.DrawingCode).HasColumnName("DRAWINGCODE").IsOptional().HasMaxLength(500);
            Property(x => x.PlotSealGroupKey).HasColumnName("PLOTSEALGROUPKEY").IsOptional().HasMaxLength(500);

            // Foreign keys
            HasRequired(a => a.S_W_WBS).WithMany(b => b.S_E_Product).HasForeignKey(c => c.WBSID); // FK_S_E_Product_S_W_WBS
        }
    }

    // S_E_ProductDirectory
    internal partial class S_E_ProductDirectoryConfiguration : EntityTypeConfiguration<S_E_ProductDirectory>
    {
        public S_E_ProductDirectoryConfiguration()
        {
			ToTable("S_E_PRODUCTDIRECTORY");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(50);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(50);
            Property(x => x.FileType).HasColumnName("FILETYPE").IsOptional().HasMaxLength(50);
            Property(x => x.Version).HasColumnName("VERSION").IsOptional().HasPrecision(18,2);
            Property(x => x.FileSize).HasColumnName("FILESIZE").IsOptional().HasMaxLength(50);
            Property(x => x.Description).HasColumnName("DESCRIPTION").IsOptional().HasMaxLength(500);
            Property(x => x.Index).HasColumnName("INDEX").IsOptional();
            Property(x => x.ProductID).HasColumnName("PRODUCTID").IsOptional().HasMaxLength(50);
            Property(x => x.ProjectInfoID).HasColumnName("PROJECTINFOID").IsOptional().HasMaxLength(50);
            Property(x => x.WBSID).HasColumnName("WBSID").IsOptional().HasMaxLength(50);
            Property(x => x.MajorValue).HasColumnName("MAJORVALUE").IsOptional().HasMaxLength(50);
        }
    }

    // S_E_ProductVersion
    internal partial class S_E_ProductVersionConfiguration : EntityTypeConfiguration<S_E_ProductVersion>
    {
        public S_E_ProductVersionConfiguration()
        {
			ToTable("S_E_PRODUCTVERSION");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.Name).HasColumnName("NAME").IsRequired().HasMaxLength(500);
            Property(x => x.ProjectInfoID).HasColumnName("PROJECTINFOID").IsRequired().HasMaxLength(50);
            Property(x => x.ProductID).HasColumnName("PRODUCTID").IsRequired().HasMaxLength(50);
            Property(x => x.AuditID).HasColumnName("AUDITID").IsOptional().HasMaxLength(50);
            Property(x => x.Version).HasColumnName("VERSION").IsOptional().HasPrecision(18,2);
            Property(x => x.MainFile).HasColumnName("MAINFILE").IsOptional().HasMaxLength(500);
            Property(x => x.PdfFile).HasColumnName("PDFFILE").IsOptional().HasMaxLength(500);
            Property(x => x.PlotFile).HasColumnName("PLOTFILE").IsOptional().HasMaxLength(500);
            Property(x => x.SwfFile).HasColumnName("SWFFILE").IsOptional().HasMaxLength(500);
            Property(x => x.ShotSnap).HasColumnName("SHOTSNAP").IsOptional().HasMaxLength(500);
            Property(x => x.XrefFile).HasColumnName("XREFFILE").IsOptional().HasMaxLength(500);
            Property(x => x.FontFile).HasColumnName("FONTFILE").IsOptional().HasMaxLength(500);
            Property(x => x.Attachments).HasColumnName("ATTACHMENTS").IsOptional().HasMaxLength(500);
            Property(x => x.AuditSignUser).HasColumnName("AUDITSIGNUSER").IsOptional().HasMaxLength(1073741823);
            Property(x => x.CoSignUser).HasColumnName("COSIGNUSER").IsOptional().HasMaxLength(1073741823);
            Property(x => x.ParentID).HasColumnName("PARENTID").IsOptional().HasMaxLength(50);
            Property(x => x.WBSID).HasColumnName("WBSID").IsOptional().HasMaxLength(50);
            Property(x => x.WBSFullID).HasColumnName("WBSFULLID").IsOptional().HasMaxLength(500);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(500);
            Property(x => x.SN).HasColumnName("SN").IsOptional().HasMaxLength(500);
            Property(x => x.MajorValue).HasColumnName("MAJORVALUE").IsOptional().HasMaxLength(50);
            Property(x => x.PhaseValue).HasColumnName("PHASEVALUE").IsOptional().HasMaxLength(50);
            Property(x => x.SubProjectInfo).HasColumnName("SUBPROJECTINFO").IsOptional().HasMaxLength(500);
            Property(x => x.MonomerInfo).HasColumnName("MONOMERINFO").IsOptional().HasMaxLength(500);
            Property(x => x.Designer).HasColumnName("DESIGNER").IsOptional().HasMaxLength(200);
            Property(x => x.DesignerName).HasColumnName("DESIGNERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Collactor).HasColumnName("COLLACTOR").IsOptional().HasMaxLength(200);
            Property(x => x.CollactorName).HasColumnName("COLLACTORNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Auditor).HasColumnName("AUDITOR").IsOptional().HasMaxLength(200);
            Property(x => x.AuditorName).HasColumnName("AUDITORNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Approver).HasColumnName("APPROVER").IsOptional().HasMaxLength(200);
            Property(x => x.ApproverName).HasColumnName("APPROVERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.CounterSignAuditID).HasColumnName("COUNTERSIGNAUDITID").IsOptional().HasMaxLength(50);
            Property(x => x.AuditState).HasColumnName("AUDITSTATE").IsOptional().HasMaxLength(50);
            Property(x => x.ArchiveState).HasColumnName("ARCHIVESTATE").IsOptional().HasMaxLength(50);
            Property(x => x.SignState).HasColumnName("SIGNSTATE").IsOptional().HasMaxLength(500);
            Property(x => x.SubmitDate).HasColumnName("SUBMITDATE").IsOptional();
            Property(x => x.CoSignDate).HasColumnName("COSIGNDATE").IsOptional();
            Property(x => x.AuditPassDate).HasColumnName("AUDITPASSDATE").IsOptional();
            Property(x => x.ArchiveDate).HasColumnName("ARCHIVEDATE").IsOptional();
            Property(x => x.State).HasColumnName("STATE").IsOptional().HasMaxLength(50);
            Property(x => x.ToA1).HasColumnName("TOA1").IsOptional().HasPrecision(18,4);
            Property(x => x.FileType).HasColumnName("FILETYPE").IsOptional().HasMaxLength(50);
            Property(x => x.MemoData).HasColumnName("MEMODATA").IsOptional().HasMaxLength(1073741823);
            Property(x => x.Description).HasColumnName("DESCRIPTION").IsOptional();
            Property(x => x.FullContext).HasColumnName("FULLCONTEXT").IsOptional().HasMaxLength(1073741823);
            Property(x => x.PrintCount).HasColumnName("PRINTCOUNT").IsOptional();
            Property(x => x.IsCoSign).HasColumnName("ISCOSIGN").IsOptional().HasMaxLength(50);
            Property(x => x.CoSignMajor).HasColumnName("COSIGNMAJOR").IsOptional().HasMaxLength(500);
            Property(x => x.FileSize).HasColumnName("FILESIZE").IsOptional().HasMaxLength(50);
            Property(x => x.FullID).HasColumnName("FULLID").IsOptional().HasMaxLength(500);
            Property(x => x.BatchID).HasColumnName("BATCHID").IsOptional().HasMaxLength(50);
            Property(x => x.PDFSignPositionInfo).HasColumnName("PDFSIGNPOSITIONINFO").IsOptional();
            Property(x => x.PDFAuditFiles).HasColumnName("PDFAUDITFILES").IsOptional();
            Property(x => x.ParentVersion).HasColumnName("PARENTVERSION").IsOptional().HasPrecision(18,2);
            Property(x => x.PackageCode).HasColumnName("PACKAGECODE").IsOptional().HasMaxLength(200);
            Property(x => x.PackageName).HasColumnName("PACKAGENAME").IsOptional().HasMaxLength(200);
            Property(x => x.MonomerCode).HasColumnName("MONOMERCODE").IsOptional().HasMaxLength(200);
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUser).HasColumnName("MODIFYUSER").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.ModifyDate).HasColumnName("MODIFYDATE").IsOptional();
            Property(x => x.ChangeAuditID).HasColumnName("CHANGEAUDITID").IsOptional().HasMaxLength(50);
            Property(x => x.CoSignState).HasColumnName("COSIGNSTATE").IsOptional().HasMaxLength(50);
            Property(x => x.DwfFile).HasColumnName("DWFFILE").IsOptional();
            Property(x => x.TiffFile).HasColumnName("TIFFFILE").IsOptional();
            Property(x => x.PlotSealGroup).HasColumnName("PLOTSEALGROUP").IsOptional().HasMaxLength(2000);
            Property(x => x.PlotSealGroupName).HasColumnName("PLOTSEALGROUPNAME").IsOptional().HasMaxLength(500);
            Property(x => x.PrintState).HasColumnName("PRINTSTATE").IsOptional().HasMaxLength(50);
            Property(x => x.SignPdfFile).HasColumnName("SIGNPDFFILE").IsOptional();
            Property(x => x.FrameAllAttInfo).HasColumnName("FRAMEALLATTINFO").IsOptional();
            Property(x => x.SourceID).HasColumnName("SOURCEID").IsOptional().HasMaxLength(50);
            Property(x => x.Edition).HasColumnName("EDITION").IsOptional().HasMaxLength(50);
            Property(x => x.AreaInfo).HasColumnName("AREAINFO").IsOptional().HasMaxLength(200);
            Property(x => x.AreaCode).HasColumnName("AREACODE").IsOptional().HasMaxLength(200);
            Property(x => x.DeviceInfo).HasColumnName("DEVICEINFO").IsOptional().HasMaxLength(200);
            Property(x => x.DeviceCode).HasColumnName("DEVICECODE").IsOptional().HasMaxLength(200);
            Property(x => x.DrawingCode).HasColumnName("DRAWINGCODE").IsOptional().HasMaxLength(500);
            Property(x => x.PlotSealGroupKey).HasColumnName("PLOTSEALGROUPKEY").IsOptional().HasMaxLength(500);

            // Foreign keys
            HasRequired(a => a.S_E_Product).WithMany(b => b.S_E_ProductVersion).HasForeignKey(c => c.ProductID); // FK_S_E_ProductVersion_S_E_Product
        }
    }

    // S_E_PublishInfo
    internal partial class S_E_PublishInfoConfiguration : EntityTypeConfiguration<S_E_PublishInfo>
    {
        public S_E_PublishInfoConfiguration()
        {
			ToTable("S_E_PUBLISHINFO");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.ProjectInfoID).HasColumnName("PROJECTINFOID").IsRequired().HasMaxLength(50);
            Property(x => x.ProjectInfoName).HasColumnName("PROJECTINFONAME").IsOptional().HasMaxLength(500);
            Property(x => x.ProjectInfoCode).HasColumnName("PROJECTINFOCODE").IsOptional().HasMaxLength(500);
            Property(x => x.PublishType).HasColumnName("PUBLISHTYPE").IsOptional().HasMaxLength(50);
            Property(x => x.WBSID).HasColumnName("WBSID").IsOptional().HasMaxLength(50);
            Property(x => x.RelateInfoID).HasColumnName("RELATEINFOID").IsOptional().HasMaxLength(50);
            Property(x => x.BelongYear).HasColumnName("BELONGYEAR").IsRequired();
            Property(x => x.BelongQuarter).HasColumnName("BELONGQUARTER").IsRequired();
            Property(x => x.BelongMonth).HasColumnName("BELONGMONTH").IsRequired();
            Property(x => x.PublishDate).HasColumnName("PUBLISHDATE").IsRequired();
            Property(x => x.A0).HasColumnName("A0").IsOptional().HasPrecision(18,4);
            Property(x => x.A1).HasColumnName("A1").IsOptional().HasPrecision(18,4);
            Property(x => x.A2).HasColumnName("A2").IsOptional().HasPrecision(18,4);
            Property(x => x.A3).HasColumnName("A3").IsOptional().HasPrecision(18,4);
            Property(x => x.A4).HasColumnName("A4").IsOptional().HasPrecision(18,4);
            Property(x => x.ToA1).HasColumnName("TOA1").IsOptional().HasPrecision(18,4);
            Property(x => x.UniPrice).HasColumnName("UNIPRICE").IsOptional().HasPrecision(18,4);
            Property(x => x.SummaryCost).HasColumnName("SUMMARYCOST").IsOptional().HasPrecision(18,4);
            Property(x => x.MajorValue).HasColumnName("MAJORVALUE").IsOptional().HasMaxLength(50);
            Property(x => x.MajorName).HasColumnName("MAJORNAME").IsOptional().HasMaxLength(50);
            Property(x => x.PhaseValue).HasColumnName("PHASEVALUE").IsOptional().HasMaxLength(50);
            Property(x => x.PhaseName).HasColumnName("PHASENAME").IsOptional().HasMaxLength(50);
            Property(x => x.SubValue).HasColumnName("SUBVALUE").IsOptional().HasMaxLength(50);
            Property(x => x.SubName).HasColumnName("SUBNAME").IsOptional().HasMaxLength(50);

            // Foreign keys
            HasRequired(a => a.S_I_ProjectInfo).WithMany(b => b.S_E_PublishInfo).HasForeignKey(c => c.ProjectInfoID); // FK_S_E_PublishInfo_S_E_PublishInfo
        }
    }

    // S_E_PublishInfoDetail
    internal partial class S_E_PublishInfoDetailConfiguration : EntityTypeConfiguration<S_E_PublishInfoDetail>
    {
        public S_E_PublishInfoDetailConfiguration()
        {
			ToTable("S_E_PUBLISHINFODETAIL");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.S_E_PublishInfoID).HasColumnName("S_E_PUBLISHINFOID").IsOptional().HasMaxLength(50);
            Property(x => x.ProductID).HasColumnName("PRODUCTID").IsOptional().HasMaxLength(200);
            Property(x => x.ProductName).HasColumnName("PRODUCTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ProductCode).HasColumnName("PRODUCTCODE").IsOptional().HasMaxLength(200);
            Property(x => x.Count).HasColumnName("COUNT").IsRequired();
            Property(x => x.Printed).HasColumnName("PRINTED").IsRequired();
            Property(x => x.ProjectInfoName).HasColumnName("PROJECTINFONAME").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectCode).HasColumnName("PROJECTCODE").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectManager).HasColumnName("PROJECTMANAGER").IsOptional().HasMaxLength(200);
            Property(x => x.MajorCode).HasColumnName("MAJORCODE").IsOptional().HasMaxLength(200);
            Property(x => x.StepName).HasColumnName("STEPNAME").IsOptional().HasMaxLength(500);
            Property(x => x.PaperSize).HasColumnName("PAPERSIZE").IsOptional().HasMaxLength(100);
            Property(x => x.IsVertical).HasColumnName("ISVERTICAL").IsRequired();
            Property(x => x.SerialNumber).HasColumnName("SERIALNUMBER").IsOptional().HasMaxLength(200);
            Property(x => x.DesingerName).HasColumnName("DESINGERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.PublishDate).HasColumnName("PUBLISHDATE").IsOptional();
            Property(x => x.ChargeDeptName).HasColumnName("CHARGEDEPTNAME").IsOptional().HasMaxLength(500);
            Property(x => x.PdfFile).HasColumnName("PDFFILE").IsOptional();
            Property(x => x.PlotFile).HasColumnName("PLOTFILE").IsOptional();
            Property(x => x.ProductVersion).HasColumnName("PRODUCTVERSION").IsOptional().HasPrecision(18,2);

            // Foreign keys
            HasOptional(a => a.S_E_PublishInfo).WithMany(b => b.S_E_PublishInfoDetail).HasForeignKey(c => c.S_E_PublishInfoID); // FK_S_E_PublishInfoDetail_S_E_PublishInfo
        }
    }

    // S_EP_PDFSignLog
    internal partial class S_EP_PDFSignLogConfiguration : EntityTypeConfiguration<S_EP_PDFSignLog>
    {
        public S_EP_PDFSignLogConfiguration()
        {
			ToTable("S_EP_PDFSIGNLOG");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.ProductID).HasColumnName("PRODUCTID").IsOptional().HasMaxLength(50);
            Property(x => x.Version).HasColumnName("VERSION").IsOptional().HasPrecision(18,2);
            Property(x => x.VersionID).HasColumnName("VERSIONID").IsOptional().HasMaxLength(50);
            Property(x => x.ErrorMessage).HasColumnName("ERRORMESSAGE").IsOptional();
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
        }
    }

    // S_EP_PlotSealGroup
    internal partial class S_EP_PlotSealGroupConfiguration : EntityTypeConfiguration<S_EP_PlotSealGroup>
    {
        public S_EP_PlotSealGroupConfiguration()
        {
			ToTable("S_EP_PLOTSEALGROUP");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.ModifyDate).HasColumnName("MODIFYDATE").IsOptional();
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUser).HasColumnName("MODIFYUSER").IsOptional().HasMaxLength(50);
            Property(x => x.OrgID).HasColumnName("ORGID").IsOptional().HasMaxLength(50);
            Property(x => x.CompanyID).HasColumnName("COMPANYID").IsOptional().HasMaxLength(50);
            Property(x => x.FlowPhase).HasColumnName("FLOWPHASE").IsOptional().HasMaxLength(50);
            Property(x => x.FlowInfo).HasColumnName("FLOWINFO").IsOptional().HasMaxLength(1073741823);
            Property(x => x.StepName).HasColumnName("STEPNAME").IsOptional().HasMaxLength(500);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(200);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(200);
            Property(x => x.GroupInfo).HasColumnName("GROUPINFO").IsOptional().HasMaxLength(1073741823);
            Property(x => x.MainPlotSealID).HasColumnName("MAINPLOTSEALID").IsOptional().HasMaxLength(200);
        }
    }

    // S_EP_PlotSealGroup_GroupInfo
    internal partial class S_EP_PlotSealGroup_GroupInfoConfiguration : EntityTypeConfiguration<S_EP_PlotSealGroup_GroupInfo>
    {
        public S_EP_PlotSealGroup_GroupInfoConfiguration()
        {
			ToTable("S_EP_PLOTSEALGROUP_GROUPINFO");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.S_EP_PlotSealGroupID).HasColumnName("S_EP_PLOTSEALGROUPID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.IsMain).HasColumnName("ISMAIN").IsOptional().HasMaxLength(200);
            Property(x => x.PlotSeal).HasColumnName("PLOTSEAL").IsOptional().HasMaxLength(200);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(200);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(200);
            Property(x => x.BlockKey).HasColumnName("BLOCKKEY").IsOptional().HasMaxLength(200);
            Property(x => x.Type).HasColumnName("TYPE").IsOptional().HasMaxLength(200);
            Property(x => x.BelongUser).HasColumnName("BELONGUSER").IsOptional().HasMaxLength(200);
            Property(x => x.BelongUserName).HasColumnName("BELONGUSERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.AuthInfo).HasColumnName("AUTHINFO").IsOptional().HasMaxLength(200);
            Property(x => x.Width).HasColumnName("WIDTH").IsOptional().HasPrecision(18,2);
            Property(x => x.Height).HasColumnName("HEIGHT").IsOptional().HasPrecision(18,2);
            Property(x => x.CorrectPosX).HasColumnName("CORRECTPOSX").IsOptional().HasPrecision(18,2);
            Property(x => x.CorrectPosY).HasColumnName("CORRECTPOSY").IsOptional().HasPrecision(18,2);

            // Foreign keys
            HasOptional(a => a.S_EP_PlotSealGroup).WithMany(b => b.S_EP_PlotSealGroup_GroupInfo).HasForeignKey(c => c.S_EP_PlotSealGroupID); // FK_S_EP_PlotSealGroup_GroupInfo_S_EP_PlotSealGroup
        }
    }

    // S_EP_PlotSealInfo
    internal partial class S_EP_PlotSealInfoConfiguration : EntityTypeConfiguration<S_EP_PlotSealInfo>
    {
        public S_EP_PlotSealInfoConfiguration()
        {
			ToTable("S_EP_PLOTSEALINFO");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.ModifyDate).HasColumnName("MODIFYDATE").IsOptional();
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUser).HasColumnName("MODIFYUSER").IsOptional().HasMaxLength(50);
            Property(x => x.OrgID).HasColumnName("ORGID").IsOptional().HasMaxLength(50);
            Property(x => x.CompanyID).HasColumnName("COMPANYID").IsOptional().HasMaxLength(50);
            Property(x => x.FlowPhase).HasColumnName("FLOWPHASE").IsOptional().HasMaxLength(50);
            Property(x => x.FlowInfo).HasColumnName("FLOWINFO").IsOptional().HasMaxLength(1073741823);
            Property(x => x.StepName).HasColumnName("STEPNAME").IsOptional().HasMaxLength(500);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(200);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(200);
            Property(x => x.Type).HasColumnName("TYPE").IsOptional().HasMaxLength(200);
            Property(x => x.BelongUser).HasColumnName("BELONGUSER").IsOptional().HasMaxLength(200);
            Property(x => x.BelongUserName).HasColumnName("BELONGUSERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.SealInfo).HasColumnName("SEALINFO").IsOptional().HasMaxLength(2147483647);
            Property(x => x.AuthInfo).HasColumnName("AUTHINFO").IsOptional().HasMaxLength(200);
            Property(x => x.Width).HasColumnName("WIDTH").IsOptional().HasPrecision(18,2);
            Property(x => x.Height).HasColumnName("HEIGHT").IsOptional().HasPrecision(18,2);
            Property(x => x.BlockKey).HasColumnName("BLOCKKEY").IsOptional().HasMaxLength(200);
            Property(x => x.QualityType).HasColumnName("QUALITYTYPE").IsOptional().HasMaxLength(500);
            Property(x => x.Remark).HasColumnName("REMARK").IsOptional().HasMaxLength(500);
            Property(x => x.ExpireTime).HasColumnName("EXPIRETIME").IsOptional();
            Property(x => x.Relational).HasColumnName("RELATIONAL").IsOptional();
        }
    }

    // S_EP_PlotSealTemplate
    internal partial class S_EP_PlotSealTemplateConfiguration : EntityTypeConfiguration<S_EP_PlotSealTemplate>
    {
        public S_EP_PlotSealTemplateConfiguration()
        {
			ToTable("S_EP_PLOTSEALTEMPLATE");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.ModifyDate).HasColumnName("MODIFYDATE").IsOptional();
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUser).HasColumnName("MODIFYUSER").IsOptional().HasMaxLength(50);
            Property(x => x.OrgID).HasColumnName("ORGID").IsOptional().HasMaxLength(50);
            Property(x => x.CompanyID).HasColumnName("COMPANYID").IsOptional().HasMaxLength(50);
            Property(x => x.FlowPhase).HasColumnName("FLOWPHASE").IsOptional().HasMaxLength(50);
            Property(x => x.FlowInfo).HasColumnName("FLOWINFO").IsOptional().HasMaxLength(1073741823);
            Property(x => x.StepName).HasColumnName("STEPNAME").IsOptional().HasMaxLength(500);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(200);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(200);
            Property(x => x.GroupList).HasColumnName("GROUPLIST").IsOptional().HasMaxLength(1073741823);
            Property(x => x.ProjectInfoID).HasColumnName("PROJECTINFOID").IsOptional().HasMaxLength(200);
            Property(x => x.TempFile).HasColumnName("TEMPFILE").IsOptional().HasMaxLength(500);
            Property(x => x.Remark).HasColumnName("REMARK").IsOptional().HasMaxLength(500);
            Property(x => x.BorderConfig).HasColumnName("BORDERCONFIG").IsOptional().HasMaxLength(1073741823);
        }
    }

    // S_EP_PlotSealTemplate_GroupList
    internal partial class S_EP_PlotSealTemplate_GroupListConfiguration : EntityTypeConfiguration<S_EP_PlotSealTemplate_GroupList>
    {
        public S_EP_PlotSealTemplate_GroupListConfiguration()
        {
			ToTable("S_EP_PLOTSEALTEMPLATE_GROUPLIST");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.S_EP_PlotSealTemplateID).HasColumnName("S_EP_PLOTSEALTEMPLATEID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.GroupID).HasColumnName("GROUPID").IsOptional().HasMaxLength(200);
            Property(x => x.GroupName).HasColumnName("GROUPNAME").IsOptional().HasMaxLength(200);
            Property(x => x.PositionXs).HasColumnName("POSITIONXS").IsOptional().HasPrecision(18,4);
            Property(x => x.PositionYs).HasColumnName("POSITIONYS").IsOptional().HasPrecision(18,4);
            Property(x => x.BlockKey).HasColumnName("BLOCKKEY").IsOptional().HasMaxLength(200);
            Property(x => x.Category).HasColumnName("CATEGORY").IsOptional().HasMaxLength(200);
            Property(x => x.Width).HasColumnName("WIDTH").IsOptional().HasPrecision(18,4);
            Property(x => x.Height).HasColumnName("HEIGHT").IsOptional().HasPrecision(18,4);
            Property(x => x.CharHeight).HasColumnName("CHARHEIGHT").IsOptional().HasPrecision(18,4);
            Property(x => x.Angle).HasColumnName("ANGLE").IsOptional().HasPrecision(18,4);
            Property(x => x.PageNum).HasColumnName("PAGENUM").IsOptional();

            // Foreign keys
            HasOptional(a => a.S_EP_PlotSealTemplate).WithMany(b => b.S_EP_PlotSealTemplate_GroupList).HasForeignKey(c => c.S_EP_PlotSealTemplateID); // FK_S_EP_PlotSealTemplate_GroupList_S_EP_PlotSealTemplate
        }
    }

    // S_EP_PublishInfo
    internal partial class S_EP_PublishInfoConfiguration : EntityTypeConfiguration<S_EP_PublishInfo>
    {
        public S_EP_PublishInfoConfiguration()
        {
			ToTable("S_EP_PUBLISHINFO");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.ModifyDate).HasColumnName("MODIFYDATE").IsOptional();
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUser).HasColumnName("MODIFYUSER").IsOptional().HasMaxLength(50);
            Property(x => x.OrgID).HasColumnName("ORGID").IsOptional().HasMaxLength(50);
            Property(x => x.CompanyID).HasColumnName("COMPANYID").IsOptional().HasMaxLength(50);
            Property(x => x.FlowPhase).HasColumnName("FLOWPHASE").IsOptional().HasMaxLength(50);
            Property(x => x.FlowInfo).HasColumnName("FLOWINFO").IsOptional().HasMaxLength(1073741823);
            Property(x => x.StepName).HasColumnName("STEPNAME").IsOptional().HasMaxLength(500);
            Property(x => x.SerialNumber).HasColumnName("SERIALNUMBER").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectInfoID).HasColumnName("PROJECTINFOID").IsOptional().HasMaxLength(200);
            Property(x => x.Project).HasColumnName("PROJECT").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectName).HasColumnName("PROJECTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectCode).HasColumnName("PROJECTCODE").IsOptional().HasMaxLength(200);
            Property(x => x.MajorCode).HasColumnName("MAJORCODE").IsOptional().HasMaxLength(200);
            Property(x => x.MajorName).HasColumnName("MAJORNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ApplyUser).HasColumnName("APPLYUSER").IsOptional().HasMaxLength(200);
            Property(x => x.ApplyUserName).HasColumnName("APPLYUSERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ApplyDept).HasColumnName("APPLYDEPT").IsOptional().HasMaxLength(200);
            Property(x => x.ApplyDeptName).HasColumnName("APPLYDEPTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ApplyDate).HasColumnName("APPLYDATE").IsOptional();
            Property(x => x.ProductCount).HasColumnName("PRODUCTCOUNT").IsOptional();
            Property(x => x.CostAmount).HasColumnName("COSTAMOUNT").IsOptional().HasPrecision(18,2);
            Property(x => x.RealCostAmount).HasColumnName("REALCOSTAMOUNT").IsOptional().HasPrecision(18,2);
            Property(x => x.Remark).HasColumnName("REMARK").IsOptional().HasMaxLength(500);
            Property(x => x.BorderSizeJson).HasColumnName("BORDERSIZEJSON").IsOptional();
            Property(x => x.ToA1).HasColumnName("TOA1").IsOptional().HasPrecision(18,4);
            Property(x => x.OperateUser).HasColumnName("OPERATEUSER").IsOptional().HasMaxLength(200);
            Property(x => x.OperateUserName).HasColumnName("OPERATEUSERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.SubmitTime).HasColumnName("SUBMITTIME").IsOptional();
            Property(x => x.PlotTime).HasColumnName("PLOTTIME").IsOptional();
            Property(x => x.ConfirmTime).HasColumnName("CONFIRMTIME").IsOptional();
            Property(x => x.PublishTime).HasColumnName("PUBLISHTIME").IsOptional();
            Property(x => x.CostName).HasColumnName("COSTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.CostCode).HasColumnName("COSTCODE").IsOptional().HasMaxLength(200);
            Property(x => x.BelongYear).HasColumnName("BELONGYEAR").IsOptional();
            Property(x => x.BelongMonth).HasColumnName("BELONGMONTH").IsOptional();
            Property(x => x.BelongQuarter).HasColumnName("BELONGQUARTER").IsOptional();
            Property(x => x.FormCode).HasColumnName("FORMCODE").IsOptional().HasMaxLength(200);
            Property(x => x.TrialStatus).HasColumnName("TRIALSTATUS").IsOptional().HasMaxLength(200);
            Property(x => x.TrialPeople).HasColumnName("TRIALPEOPLE").IsOptional().HasMaxLength(200);
            Property(x => x.TrialTime).HasColumnName("TRIALTIME").IsOptional().HasMaxLength(200);
            Property(x => x.TrialRemarks).HasColumnName("TRIALREMARKS").IsOptional().HasMaxLength(500);
            Property(x => x.PutOutPeople).HasColumnName("PUTOUTPEOPLE").IsOptional().HasMaxLength(200);
            Property(x => x.PutOutTime).HasColumnName("PUTOUTTIME").IsOptional();
            Property(x => x.PutOutRemarks).HasColumnName("PUTOUTREMARKS").IsOptional().HasMaxLength(500);
            Property(x => x.PutOutStatus).HasColumnName("PUTOUTSTATUS").IsOptional().HasMaxLength(200);
        }
    }

    // S_EP_PublishInfo_PriceDetail
    internal partial class S_EP_PublishInfo_PriceDetailConfiguration : EntityTypeConfiguration<S_EP_PublishInfo_PriceDetail>
    {
        public S_EP_PublishInfo_PriceDetailConfiguration()
        {
			ToTable("S_EP_PUBLISHINFO_PRICEDETAIL");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.S_EP_PublishInfoID).HasColumnName("S_EP_PUBLISHINFOID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.PublicationType).HasColumnName("PUBLICATIONTYPE").IsOptional().HasMaxLength(200);
            Property(x => x.Specification).HasColumnName("SPECIFICATION").IsOptional().HasMaxLength(200);
            Property(x => x.Num).HasColumnName("NUM").IsOptional().HasPrecision(18,2);
            Property(x => x.Price).HasColumnName("PRICE").IsOptional().HasPrecision(18,2);
            Property(x => x.Sum).HasColumnName("SUM").IsOptional().HasPrecision(18,2);
            Property(x => x.SumCost).HasColumnName("SUMCOST").IsOptional().HasPrecision(18,2);
            Property(x => x.RealNum).HasColumnName("REALNUM").IsOptional().HasPrecision(18,2);
            Property(x => x.RealPrice).HasColumnName("REALPRICE").IsOptional().HasPrecision(18,2);
            Property(x => x.RealSum).HasColumnName("REALSUM").IsOptional().HasPrecision(18,2);
            Property(x => x.RealSumCost).HasColumnName("REALSUMCOST").IsOptional().HasPrecision(18,2);
            Property(x => x.CostID).HasColumnName("COSTID").IsOptional().HasMaxLength(200);

            // Foreign keys
            HasOptional(a => a.S_EP_PublishInfo).WithMany(b => b.S_EP_PublishInfo_PriceDetail).HasForeignKey(c => c.S_EP_PublishInfoID); // FK_S_EP_PublishInfo_PriceDetail_S_EP_PublishInfo
        }
    }

    // S_EP_PublishInfo_Products
    internal partial class S_EP_PublishInfo_ProductsConfiguration : EntityTypeConfiguration<S_EP_PublishInfo_Products>
    {
        public S_EP_PublishInfo_ProductsConfiguration()
        {
			ToTable("S_EP_PUBLISHINFO_PRODUCTS");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.S_EP_PublishInfoID).HasColumnName("S_EP_PUBLISHINFOID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.ProductID).HasColumnName("PRODUCTID").IsOptional().HasMaxLength(200);
            Property(x => x.VersionID).HasColumnName("VERSIONID").IsOptional().HasMaxLength(200);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(200);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(200);
            Property(x => x.Version).HasColumnName("VERSION").IsOptional().HasPrecision(18,2);
            Property(x => x.MainFile).HasColumnName("MAINFILE").IsOptional().HasMaxLength(200);
            Property(x => x.PdfFile).HasColumnName("PDFFILE").IsOptional().HasMaxLength(200);
            Property(x => x.PlotFile).HasColumnName("PLOTFILE").IsOptional().HasMaxLength(200);
            Property(x => x.XrefFile).HasColumnName("XREFFILE").IsOptional().HasMaxLength(200);
            Property(x => x.DwfFile).HasColumnName("DWFFILE").IsOptional().HasMaxLength(200);
            Property(x => x.TiffFile).HasColumnName("TIFFFILE").IsOptional().HasMaxLength(200);
            Property(x => x.SignPdfFile).HasColumnName("SIGNPDFFILE").IsOptional().HasMaxLength(200);
            Property(x => x.PlotSealGroupName).HasColumnName("PLOTSEALGROUPNAME").IsOptional().HasMaxLength(200);

            // Foreign keys
            HasOptional(a => a.S_EP_PublishInfo).WithMany(b => b.S_EP_PublishInfo_Products).HasForeignKey(c => c.S_EP_PublishInfoID); // FK_S_EP_PublishInfo_Products_S_EP_PublishInfo
        }
    }

    // S_F_BorderConfig
    internal partial class S_F_BorderConfigConfiguration : EntityTypeConfiguration<S_F_BorderConfig>
    {
        public S_F_BorderConfigConfiguration()
        {
			ToTable("S_F_BORDERCONFIG");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.BorderType).HasColumnName("BORDERTYPE").IsOptional().HasMaxLength(200);
            Property(x => x.BorderSize).HasColumnName("BORDERSIZE").IsOptional().HasMaxLength(200);
            Property(x => x.Category).HasColumnName("CATEGORY").IsOptional().HasMaxLength(200);
            Property(x => x.Width).HasColumnName("WIDTH").IsOptional().HasPrecision(18,2);
            Property(x => x.Height).HasColumnName("HEIGHT").IsOptional().HasPrecision(18,2);
            Property(x => x.Angle).HasColumnName("ANGLE").IsOptional().HasPrecision(18,2);
            Property(x => x.CorrectPosX).HasColumnName("CORRECTPOSX").IsOptional().HasPrecision(18,2);
            Property(x => x.CorrectPosY).HasColumnName("CORRECTPOSY").IsOptional().HasPrecision(18,2);
            Property(x => x.CharHeight).HasColumnName("CHARHEIGHT").IsOptional().HasPrecision(18,2);
            Property(x => x.DateExtWidth).HasColumnName("DATEEXTWIDTH").IsOptional().HasPrecision(18,2);
            Property(x => x.DateExtHeight).HasColumnName("DATEEXTHEIGHT").IsOptional().HasPrecision(18,2);
            Property(x => x.MajorExtWidth).HasColumnName("MAJOREXTWIDTH").IsOptional().HasPrecision(18,2);
            Property(x => x.MajorExtHeight).HasColumnName("MAJOREXTHEIGHT").IsOptional().HasPrecision(18,2);
            Property(x => x.ManageInfoID).HasColumnName("MANAGEINFOID").IsOptional().HasMaxLength(50);
            Property(x => x.DefaultTemplateName).HasColumnName("DEFAULTTEMPLATENAME").IsOptional().HasMaxLength(200);
            Property(x => x.CurrentDefault).HasColumnName("CURRENTDEFAULT").IsOptional().HasMaxLength(50);
            Property(x => x.CharHeightCAD).HasColumnName("CHARHEIGHTCAD").IsOptional().HasPrecision(18,2);
        }
    }

    // S_F_FrameInfo
    internal partial class S_F_FrameInfoConfiguration : EntityTypeConfiguration<S_F_FrameInfo>
    {
        public S_F_FrameInfoConfiguration()
        {
			ToTable("S_F_FRAMEINFO");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.FrameTypeName).HasColumnName("FRAMETYPENAME").IsRequired().HasMaxLength(200);
            Property(x => x.ConfigFileID).HasColumnName("CONFIGFILEID").IsOptional().HasMaxLength(50);
            Property(x => x.Designer).HasColumnName("DESIGNER").IsOptional().HasMaxLength(50);
            Property(x => x.DesignerID).HasColumnName("DESIGNERID").IsOptional().HasMaxLength(50);
        }
    }

    // S_F_FrameInfo_FrameManageInfo
    internal partial class S_F_FrameInfo_FrameManageInfoConfiguration : EntityTypeConfiguration<S_F_FrameInfo_FrameManageInfo>
    {
        public S_F_FrameInfo_FrameManageInfoConfiguration()
        {
			ToTable("S_F_FRAMEINFO_FRAMEMANAGEINFO");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.S_F_FrameInfoID).HasColumnName("S_F_FRAMEINFOID").IsOptional().HasMaxLength(50);
            Property(x => x.FrameType).HasColumnName("FRAMETYPE").IsOptional().HasMaxLength(200);
            Property(x => x.TemplateName).HasColumnName("TEMPLATENAME").IsOptional().HasMaxLength(200);
            Property(x => x.FileID).HasColumnName("FILEID").IsOptional().HasMaxLength(200);
            Property(x => x.Designer).HasColumnName("DESIGNER").IsOptional().HasMaxLength(50);
            Property(x => x.DesignerID).HasColumnName("DESIGNERID").IsOptional().HasMaxLength(50);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(200);
            Property(x => x.BorderType).HasColumnName("BORDERTYPE").IsOptional().HasMaxLength(50);
            Property(x => x.Size).HasColumnName("SIZE").IsOptional().HasMaxLength(50);
            Property(x => x.Version).HasColumnName("VERSION").IsOptional();
            Property(x => x.ProjectInfoID).HasColumnName("PROJECTINFOID").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectInfoName).HasColumnName("PROJECTINFONAME").IsOptional().HasMaxLength(200);
            Property(x => x.Remark).HasColumnName("REMARK").IsOptional().HasMaxLength(500);
            Property(x => x.FrameInfoJson).HasColumnName("FRAMEINFOJSON").IsOptional();
            Property(x => x.CreateTime).HasColumnName("CREATETIME").IsOptional();
            Property(x => x.Category).HasColumnName("CATEGORY").IsOptional().HasMaxLength(50);

            // Foreign keys
            HasOptional(a => a.S_F_FrameInfo).WithMany(b => b.S_F_FrameInfo_FrameManageInfo).HasForeignKey(c => c.S_F_FrameInfoID); // FK__S_F_Frame__S_F_F__5DCAEF64
        }
    }

    // S_I_FileConverts
    internal partial class S_I_FileConvertsConfiguration : EntityTypeConfiguration<S_I_FileConverts>
    {
        public S_I_FileConvertsConfiguration()
        {
			ToTable("S_I_FILECONVERTS");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.DocumentID).HasColumnName("DOCUMENTID").IsOptional().HasMaxLength(50);
            Property(x => x.FileID).HasColumnName("FILEID").IsRequired().HasMaxLength(50);
            Property(x => x.FileName).HasColumnName("FILENAME").IsRequired().HasMaxLength(100);
            Property(x => x.FileFullPath).HasColumnName("FILEFULLPATH").IsOptional().HasMaxLength(300);
            Property(x => x.DBSID).HasColumnName("DBSID").IsOptional().HasMaxLength(50);
            Property(x => x.Suffix).HasColumnName("SUFFIX").IsOptional().HasMaxLength(10);
            Property(x => x.Size).HasColumnName("SIZE").IsOptional();
            Property(x => x.State).HasColumnName("STATE").IsOptional();
            Property(x => x.Path).HasColumnName("PATH").IsOptional();
            Property(x => x.FontPath).HasColumnName("FONTPATH").IsOptional().HasMaxLength(1000);
            Property(x => x.XrefPath).HasColumnName("XREFPATH").IsOptional().HasMaxLength(1000);
            Property(x => x.HtmlPath).HasColumnName("HTMLPATH").IsOptional();
            Property(x => x.ImagePath).HasColumnName("IMAGEPATH").IsOptional();
            Property(x => x.ImageZoomLevel).HasColumnName("IMAGEZOOMLEVEL").IsOptional();
            Property(x => x.ErrorMessage).HasColumnName("ERRORMESSAGE").IsOptional();
            Property(x => x.IsSysn).HasColumnName("ISSYSN").IsOptional();
            Property(x => x.SysnTime).HasColumnName("SYSNTIME").IsOptional();
            Property(x => x.ConvertCount).HasColumnName("CONVERTCOUNT").IsOptional();
            Property(x => x.UserID).HasColumnName("USERID").IsOptional().HasMaxLength(50);
            Property(x => x.PrjID).HasColumnName("PRJID").IsOptional().HasMaxLength(50);
            Property(x => x.VersionID).HasColumnName("VERSIONID").IsOptional().HasMaxLength(50);
            Property(x => x.VersionName).HasColumnName("VERSIONNAME").IsOptional().HasMaxLength(500);
            Property(x => x.IsUpFile).HasColumnName("ISUPFILE").IsOptional();
        }
    }

    // S_I_ProjectGroup
    internal partial class S_I_ProjectGroupConfiguration : EntityTypeConfiguration<S_I_ProjectGroup>
    {
        public S_I_ProjectGroupConfiguration()
        {
			ToTable("S_I_PROJECTGROUP");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.Name).HasColumnName("NAME").IsRequired().HasMaxLength(500);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(500);
            Property(x => x.RelateID).HasColumnName("RELATEID").IsOptional().HasMaxLength(50);
            Property(x => x.Type).HasColumnName("TYPE").IsOptional().HasMaxLength(50);
            Property(x => x.ParentID).HasColumnName("PARENTID").IsOptional().HasMaxLength(200);
            Property(x => x.FullID).HasColumnName("FULLID").IsRequired().HasMaxLength(500);
            Property(x => x.RootID).HasColumnName("ROOTID").IsRequired().HasMaxLength(50);
            Property(x => x.State).HasColumnName("STATE").IsOptional().HasMaxLength(50);
            Property(x => x.DefineID).HasColumnName("DEFINEID").IsOptional().HasMaxLength(50);
            Property(x => x.EngineeringSpaceCode).HasColumnName("ENGINEERINGSPACECODE").IsOptional().HasMaxLength(50);
            Property(x => x.ChargeUser).HasColumnName("CHARGEUSER").IsOptional().HasMaxLength(500);
            Property(x => x.ChargeUserName).HasColumnName("CHARGEUSERNAME").IsOptional().HasMaxLength(500);
            Property(x => x.DeptID).HasColumnName("DEPTID").IsOptional().HasMaxLength(500);
            Property(x => x.DeptName).HasColumnName("DEPTNAME").IsOptional().HasMaxLength(500);
            Property(x => x.Country).HasColumnName("COUNTRY").IsOptional().HasMaxLength(50);
            Property(x => x.Province).HasColumnName("PROVINCE").IsOptional().HasMaxLength(50);
            Property(x => x.City).HasColumnName("CITY").IsOptional().HasMaxLength(50);
            Property(x => x.ProjectClass).HasColumnName("PROJECTCLASS").IsOptional().HasMaxLength(200);
            Property(x => x.Proportion).HasColumnName("PROPORTION").IsOptional().HasMaxLength(500);
            Property(x => x.PhaseValue).HasColumnName("PHASEVALUE").IsOptional().HasMaxLength(500);
            Property(x => x.PhaseContent).HasColumnName("PHASECONTENT").IsOptional().HasMaxLength(500);
            Property(x => x.Investment).HasColumnName("INVESTMENT").IsOptional().HasPrecision(18,4);
            Property(x => x.Address).HasColumnName("ADDRESS").IsOptional().HasMaxLength(500);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.Area).HasColumnName("AREA").IsOptional().HasMaxLength(200);
        }
    }

    // S_I_ProjectInfo
    internal partial class S_I_ProjectInfoConfiguration : EntityTypeConfiguration<S_I_ProjectInfo>
    {
        public S_I_ProjectInfoConfiguration()
        {
			ToTable("S_I_PROJECTINFO");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.ModeCode).HasColumnName("MODECODE").IsRequired().HasMaxLength(50);
            Property(x => x.Name).HasColumnName("NAME").IsRequired().HasMaxLength(500);
            Property(x => x.Code).HasColumnName("CODE").IsRequired().HasMaxLength(200);
            Property(x => x.State).HasColumnName("STATE").IsRequired().HasMaxLength(50);
            Property(x => x.Status).HasColumnName("STATUS").IsOptional().HasMaxLength(50);
            Property(x => x.PhaseValue).HasColumnName("PHASEVALUE").IsOptional().HasMaxLength(50);
            Property(x => x.PhaseName).HasColumnName("PHASENAME").IsOptional().HasMaxLength(50);
            Property(x => x.WorkContent).HasColumnName("WORKCONTENT").IsOptional().HasMaxLength(500);
            Property(x => x.Major).HasColumnName("MAJOR").IsOptional();
            Property(x => x.CustomerID).HasColumnName("CUSTOMERID").IsOptional().HasMaxLength(50);
            Property(x => x.CustomerName).HasColumnName("CUSTOMERNAME").IsOptional().HasMaxLength(500);
            Property(x => x.CustomerSub).HasColumnName("CUSTOMERSUB").IsOptional().HasMaxLength(50);
            Property(x => x.CustomerSubName).HasColumnName("CUSTOMERSUBNAME").IsOptional().HasMaxLength(50);
            Property(x => x.ChargeUserID).HasColumnName("CHARGEUSERID").IsOptional().HasMaxLength(500);
            Property(x => x.ChargeUserName).HasColumnName("CHARGEUSERNAME").IsOptional().HasMaxLength(500);
            Property(x => x.ChargeDeptID).HasColumnName("CHARGEDEPTID").IsOptional().HasMaxLength(200);
            Property(x => x.ChargeDeptName).HasColumnName("CHARGEDEPTNAME").IsOptional().HasMaxLength(500);
            Property(x => x.OtherDeptID).HasColumnName("OTHERDEPTID").IsOptional().HasMaxLength(500);
            Property(x => x.OtherDeptName).HasColumnName("OTHERDEPTNAME").IsOptional().HasMaxLength(500);
            Property(x => x.GroupRootID).HasColumnName("GROUPROOTID").IsOptional().HasMaxLength(50);
            Property(x => x.GroupID).HasColumnName("GROUPID").IsOptional().HasMaxLength(50);
            Property(x => x.GroupFullID).HasColumnName("GROUPFULLID").IsOptional().HasMaxLength(500);
            Property(x => x.ProjectSpecialty).HasColumnName("PROJECTSPECIALTY").IsOptional().HasMaxLength(50);
            Property(x => x.ProjectClass).HasColumnName("PROJECTCLASS").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectLevel).HasColumnName("PROJECTLEVEL").IsOptional();
            Property(x => x.Country).HasColumnName("COUNTRY").IsOptional().HasMaxLength(50);
            Property(x => x.Province).HasColumnName("PROVINCE").IsOptional().HasMaxLength(50);
            Property(x => x.City).HasColumnName("CITY").IsOptional().HasMaxLength(50);
            Property(x => x.Proportion).HasColumnName("PROPORTION").IsOptional().HasPrecision(18,4);
            Property(x => x.PlanStartDate).HasColumnName("PLANSTARTDATE").IsOptional();
            Property(x => x.PlanFinishDate).HasColumnName("PLANFINISHDATE").IsOptional();
            Property(x => x.FactStartDate).HasColumnName("FACTSTARTDATE").IsOptional();
            Property(x => x.FactFinishDate).HasColumnName("FACTFINISHDATE").IsOptional();
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsRequired().HasMaxLength(50);
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsRequired().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsRequired();
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUser).HasColumnName("MODIFYUSER").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyDate).HasColumnName("MODIFYDATE").IsOptional();
            Property(x => x.MarketProjectInfoID).HasColumnName("MARKETPROJECTINFOID").IsOptional().HasMaxLength(50);
            Property(x => x.EngineeringInfoID).HasColumnName("ENGINEERINGINFOID").IsOptional().HasMaxLength(50);
            Property(x => x.CustomerRequireInfoID).HasColumnName("CUSTOMERREQUIREINFOID").IsOptional().HasMaxLength(50);
            Property(x => x.CompletePercent).HasColumnName("COMPLETEPERCENT").IsOptional().HasPrecision(18,2);
            Property(x => x.IsSync).HasColumnName("ISSYNC").IsOptional();
            Property(x => x.SyscTime).HasColumnName("SYSCTIME").IsOptional();
            Property(x => x.CoopUnitID).HasColumnName("COOPUNITID").IsOptional();
            Property(x => x.CoopUnitIDName).HasColumnName("COOPUNITIDNAME").IsOptional();
            Property(x => x.Extention1).HasColumnName("EXTENTION1").IsOptional().HasMaxLength(500);
            Property(x => x.Extention2).HasColumnName("EXTENTION2").IsOptional().HasMaxLength(500);
            Property(x => x.Extention3).HasColumnName("EXTENTION3").IsOptional().HasMaxLength(500);
            Property(x => x.Extention4).HasColumnName("EXTENTION4").IsOptional().HasMaxLength(500);
            Property(x => x.Extention5).HasColumnName("EXTENTION5").IsOptional().HasMaxLength(500);
            Property(x => x.Extention6).HasColumnName("EXTENTION6").IsOptional().HasMaxLength(500);
            Property(x => x.Extention7).HasColumnName("EXTENTION7").IsOptional().HasMaxLength(500);
            Property(x => x.Extention8).HasColumnName("EXTENTION8").IsOptional().HasMaxLength(500);
            Property(x => x.Extention9).HasColumnName("EXTENTION9").IsOptional().HasMaxLength(500);
            Property(x => x.Extention10).HasColumnName("EXTENTION10").IsOptional().HasMaxLength(500);
            Property(x => x.Long).HasColumnName("LONG").IsOptional();
            Property(x => x.Lat).HasColumnName("LAT").IsOptional();
            Property(x => x.Address).HasColumnName("ADDRESS").IsOptional().HasMaxLength(500);
            Property(x => x.District).HasColumnName("DISTRICT").IsOptional().HasMaxLength(200);
            Property(x => x.Area).HasColumnName("AREA").IsOptional().HasMaxLength(200);
        }
    }

    // S_I_ProjectRelation
    internal partial class S_I_ProjectRelationConfiguration : EntityTypeConfiguration<S_I_ProjectRelation>
    {
        public S_I_ProjectRelationConfiguration()
        {
			ToTable("S_I_PROJECTRELATION");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.ProjectInfoID).HasColumnName("PROJECTINFOID").IsRequired().HasMaxLength(50);
            Property(x => x.CoopUnitID).HasColumnName("COOPUNITID").IsOptional().HasMaxLength(2000);
            Property(x => x.IsSync).HasColumnName("ISSYNC").IsOptional();
            Property(x => x.SyscTime).HasColumnName("SYSCTIME").IsOptional();
        }
    }

    // S_I_UserDefaultProjectInfo
    internal partial class S_I_UserDefaultProjectInfoConfiguration : EntityTypeConfiguration<S_I_UserDefaultProjectInfo>
    {
        public S_I_UserDefaultProjectInfoConfiguration()
        {
			ToTable("S_I_USERDEFAULTPROJECTINFO");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.UserID).HasColumnName("USERID").IsRequired().HasMaxLength(50);
            Property(x => x.ProjectInfoID).HasColumnName("PROJECTINFOID").IsOptional().HasMaxLength(50);
            Property(x => x.EngineeringInfoID).HasColumnName("ENGINEERINGINFOID").IsRequired().HasMaxLength(50);
        }
    }

    // S_I_UserFocusProjectInfo
    internal partial class S_I_UserFocusProjectInfoConfiguration : EntityTypeConfiguration<S_I_UserFocusProjectInfo>
    {
        public S_I_UserFocusProjectInfoConfiguration()
        {
			ToTable("S_I_USERFOCUSPROJECTINFO");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.UserID).HasColumnName("USERID").IsRequired().HasMaxLength(50);
            Property(x => x.ProjectInfoID).HasColumnName("PROJECTINFOID").IsRequired().HasMaxLength(50);
        }
    }

    // S_N_Notice
    internal partial class S_N_NoticeConfiguration : EntityTypeConfiguration<S_N_Notice>
    {
        public S_N_NoticeConfiguration()
        {
			ToTable("S_N_NOTICE");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.GroupInfoID).HasColumnName("GROUPINFOID").IsOptional().HasMaxLength(50);
            Property(x => x.Title).HasColumnName("TITLE").IsOptional().HasMaxLength(500);
            Property(x => x.Content).HasColumnName("CONTENT").IsOptional();
            Property(x => x.Attachments).HasColumnName("ATTACHMENTS").IsOptional();
            Property(x => x.IsFromSys).HasColumnName("ISFROMSYS").IsOptional().HasMaxLength(50);
            Property(x => x.ExpiresTime).HasColumnName("EXPIRESTIME").IsOptional();
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUserName).HasColumnName("CREATEUSERNAME").IsOptional().HasMaxLength(50);
            Property(x => x.LinkUrl).HasColumnName("LINKURL").IsOptional().HasMaxLength(1073741823);
            Property(x => x.RelateID).HasColumnName("RELATEID").IsOptional().HasMaxLength(50);
            Property(x => x.RelateType).HasColumnName("RELATETYPE").IsOptional().HasMaxLength(50);
            Property(x => x.ProjectInfoID).HasColumnName("PROJECTINFOID").IsOptional().HasMaxLength(50);
            Property(x => x.WBSID).HasColumnName("WBSID").IsOptional().HasMaxLength(50);
            Property(x => x.RoleCode).HasColumnName("ROLECODE").IsOptional().HasMaxLength(50);
            Property(x => x.MajorValue).HasColumnName("MAJORVALUE").IsOptional().HasMaxLength(50);
            Property(x => x.ReceiverIDs).HasColumnName("RECEIVERIDS").IsOptional();
            Property(x => x.ReceiverNames).HasColumnName("RECEIVERNAMES").IsOptional();
            Property(x => x.NoticeType).HasColumnName("NOTICETYPE").IsOptional().HasMaxLength(50);
        }
    }

    // S_N_Notice_ViewDetail
    internal partial class S_N_Notice_ViewDetailConfiguration : EntityTypeConfiguration<S_N_Notice_ViewDetail>
    {
        public S_N_Notice_ViewDetailConfiguration()
        {
			ToTable("S_N_NOTICE_VIEWDETAIL");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.S_N_NoticeID).HasColumnName("S_N_NOTICEID").IsOptional().HasMaxLength(50);
            Property(x => x.UserID).HasColumnName("USERID").IsOptional().HasMaxLength(50);
            Property(x => x.UserName).HasColumnName("USERNAME").IsOptional().HasMaxLength(50);
            Property(x => x.FirstViewTime).HasColumnName("FIRSTVIEWTIME").IsOptional();

            // Foreign keys
            HasOptional(a => a.S_N_Notice).WithMany(b => b.S_N_Notice_ViewDetail).HasForeignKey(c => c.S_N_NoticeID); // FK_S_N_Notice_ViewDetail_S_N_Notice
        }
    }

    // S_P_CooperationPlan
    internal partial class S_P_CooperationPlanConfiguration : EntityTypeConfiguration<S_P_CooperationPlan>
    {
        public S_P_CooperationPlanConfiguration()
        {
			ToTable("S_P_COOPERATIONPLAN");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.ProjectInfoID).HasColumnName("PROJECTINFOID").IsRequired().HasMaxLength(50);
            Property(x => x.WBSID).HasColumnName("WBSID").IsRequired().HasMaxLength(50);
            Property(x => x.WBSFullID).HasColumnName("WBSFULLID").IsRequired().HasMaxLength(500);
            Property(x => x.MileStoneID).HasColumnName("MILESTONEID").IsOptional().HasMaxLength(50);
            Property(x => x.SchemeWBSID).HasColumnName("SCHEMEWBSID").IsOptional().HasMaxLength(50);
            Property(x => x.SchemeWBSName).HasColumnName("SCHEMEWBSNAME").IsOptional().HasMaxLength(500);
            Property(x => x.CooperationContent).HasColumnName("COOPERATIONCONTENT").IsRequired().HasMaxLength(500);
            Property(x => x.CooperationValue).HasColumnName("COOPERATIONVALUE").IsRequired().HasMaxLength(500);
            Property(x => x.InMajorValue).HasColumnName("INMAJORVALUE").IsRequired().HasMaxLength(500);
            Property(x => x.OutMajorValue).HasColumnName("OUTMAJORVALUE").IsOptional().HasMaxLength(500);
            Property(x => x.PlanStartDate).HasColumnName("PLANSTARTDATE").IsOptional();
            Property(x => x.PlanFinishDate).HasColumnName("PLANFINISHDATE").IsOptional();
            Property(x => x.OrPlanStartDate).HasColumnName("ORPLANSTARTDATE").IsOptional();
            Property(x => x.OrPlanFinishDate).HasColumnName("ORPLANFINISHDATE").IsOptional();
            Property(x => x.FactStartDate).HasColumnName("FACTSTARTDATE").IsOptional();
            Property(x => x.FactFinishDate).HasColumnName("FACTFINISHDATE").IsOptional();
            Property(x => x.FinishUser).HasColumnName("FINISHUSER").IsOptional().HasMaxLength(50);
            Property(x => x.FinishUserID).HasColumnName("FINISHUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.State).HasColumnName("STATE").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsRequired().HasMaxLength(50);
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsOptional().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsRequired();
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsRequired().HasMaxLength(50);
            Property(x => x.ModifyUser).HasColumnName("MODIFYUSER").IsRequired().HasMaxLength(50);
            Property(x => x.ModifyDate).HasColumnName("MODIFYDATE").IsRequired();

            // Foreign keys
            HasRequired(a => a.S_I_ProjectInfo).WithMany(b => b.S_P_CooperationPlan).HasForeignKey(c => c.ProjectInfoID); // FK_S_P_CooperationPlan_S_I_ProjectInfo
        }
    }

    // S_P_MileStone
    internal partial class S_P_MileStoneConfiguration : EntityTypeConfiguration<S_P_MileStone>
    {
        public S_P_MileStoneConfiguration()
        {
			ToTable("S_P_MILESTONE");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.ProjectInfoID).HasColumnName("PROJECTINFOID").IsRequired().HasMaxLength(50);
            Property(x => x.WBSID).HasColumnName("WBSID").IsOptional().HasMaxLength(50);
            Property(x => x.TemplateID).HasColumnName("TEMPLATEID").IsOptional().HasMaxLength(50);
            Property(x => x.Name).HasColumnName("NAME").IsRequired().HasMaxLength(500);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(500);
            Property(x => x.Year).HasColumnName("YEAR").IsOptional();
            Property(x => x.Month).HasColumnName("MONTH").IsOptional();
            Property(x => x.Season).HasColumnName("SEASON").IsOptional();
            Property(x => x.DeptID).HasColumnName("DEPTID").IsOptional().HasMaxLength(500);
            Property(x => x.DeptName).HasColumnName("DEPTNAME").IsOptional().HasMaxLength(500);
            Property(x => x.MileStoneValue).HasColumnName("MILESTONEVALUE").IsOptional().HasMaxLength(500);
            Property(x => x.MileStoneType).HasColumnName("MILESTONETYPE").IsOptional().HasMaxLength(50);
            Property(x => x.PhaseValue).HasColumnName("PHASEVALUE").IsOptional().HasMaxLength(50);
            Property(x => x.MajorValue).HasColumnName("MAJORVALUE").IsOptional().HasMaxLength(500);
            Property(x => x.OutMajorValue).HasColumnName("OUTMAJORVALUE").IsOptional().HasMaxLength(500);
            Property(x => x.Necessity).HasColumnName("NECESSITY").IsOptional().HasMaxLength(50);
            Property(x => x.BindReceiptObjID).HasColumnName("BINDRECEIPTOBJID").IsOptional().HasMaxLength(500);
            Property(x => x.BindReceiptObjName).HasColumnName("BINDRECEIPTOBJNAME").IsOptional().HasMaxLength(500);
            Property(x => x.PlanStartDate).HasColumnName("PLANSTARTDATE").IsOptional();
            Property(x => x.PlanFinishDate).HasColumnName("PLANFINISHDATE").IsOptional();
            Property(x => x.OrlPlanStartDate).HasColumnName("ORLPLANSTARTDATE").IsOptional();
            Property(x => x.OrlPlanFinishDate).HasColumnName("ORLPLANFINISHDATE").IsOptional();
            Property(x => x.FactStartDate).HasColumnName("FACTSTARTDATE").IsOptional();
            Property(x => x.FactFinishDate).HasColumnName("FACTFINISHDATE").IsOptional();
            Property(x => x.Weight).HasColumnName("WEIGHT").IsOptional().HasPrecision(18,2);
            Property(x => x.State).HasColumnName("STATE").IsRequired().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.Description).HasColumnName("DESCRIPTION").IsOptional().HasMaxLength(500);
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsOptional().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUser).HasColumnName("MODIFYUSER").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyDate).HasColumnName("MODIFYDATE").IsOptional();
            Property(x => x.IncomingID).HasColumnName("INCOMINGID").IsOptional().HasMaxLength(50);
            Property(x => x.ApplyUserID).HasColumnName("APPLYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ApplyUserName).HasColumnName("APPLYUSERNAME").IsOptional().HasMaxLength(50);
            Property(x => x.ApplyState).HasColumnName("APPLYSTATE").IsOptional().HasMaxLength(50);
            Property(x => x.ApplyFiles).HasColumnName("APPLYFILES").IsOptional().HasMaxLength(500);
            Property(x => x.ApplyRemark).HasColumnName("APPLYREMARK").IsOptional().HasMaxLength(500);
            Property(x => x.ApplyDate).HasColumnName("APPLYDATE").IsOptional();
            Property(x => x.ApplyApprovalUserID).HasColumnName("APPLYAPPROVALUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ApplyApprovalUserName).HasColumnName("APPLYAPPROVALUSERNAME").IsOptional().HasMaxLength(50);
            Property(x => x.ApplyApprovalDate).HasColumnName("APPLYAPPROVALDATE").IsOptional();
            Property(x => x.IsChecked).HasColumnName("ISCHECKED").IsOptional().HasMaxLength(20);

            // Foreign keys
            HasRequired(a => a.S_I_ProjectInfo).WithMany(b => b.S_P_MileStone).HasForeignKey(c => c.ProjectInfoID); // FK_S_P_MileStone_S_I_ProjectInfo
        }
    }

    // S_P_MileStone_FormDetail
    internal partial class S_P_MileStone_FormDetailConfiguration : EntityTypeConfiguration<S_P_MileStone_FormDetail>
    {
        public S_P_MileStone_FormDetailConfiguration()
        {
			ToTable("S_P_MILESTONE_FORMDETAIL");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.S_P_MileStoneID).HasColumnName("S_P_MILESTONEID").IsOptional().HasMaxLength(50);
            Property(x => x.FormID).HasColumnName("FORMID").IsOptional().HasMaxLength(50);
            Property(x => x.BindDate).HasColumnName("BINDDATE").IsOptional();
            Property(x => x.BindPeople).HasColumnName("BINDPEOPLE").IsOptional().HasMaxLength(10);

            // Foreign keys
            HasOptional(a => a.S_P_MileStone).WithMany(b => b.S_P_MileStone_FormDetail).HasForeignKey(c => c.S_P_MileStoneID); // FK_S_P_MileStone_FormDetail_S_P_MileStone_FormDetail
        }
    }

    // S_P_MileStone_ProductDetail
    internal partial class S_P_MileStone_ProductDetailConfiguration : EntityTypeConfiguration<S_P_MileStone_ProductDetail>
    {
        public S_P_MileStone_ProductDetailConfiguration()
        {
			ToTable("S_P_MILESTONE_PRODUCTDETAIL");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.S_P_MileStoneID).HasColumnName("S_P_MILESTONEID").IsOptional().HasMaxLength(50);
            Property(x => x.ProjectInfoID).HasColumnName("PROJECTINFOID").IsOptional().HasMaxLength(50);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(200);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(200);
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUser).HasColumnName("MODIFYUSER").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.ModifyDate).HasColumnName("MODIFYDATE").IsOptional();
            Property(x => x.FileID).HasColumnName("FILEID").IsOptional().HasMaxLength(200);
            Property(x => x.MD5Code).HasColumnName("MD5CODE").IsOptional().HasMaxLength(200);
            Property(x => x.ReceiveInfo).HasColumnName("RECEIVEINFO").IsOptional().HasMaxLength(1073741823);
            Property(x => x.State).HasColumnName("STATE").IsOptional().HasMaxLength(50);

            // Foreign keys
            HasOptional(a => a.S_P_MileStone).WithMany(b => b.S_P_MileStone_ProductDetail).HasForeignKey(c => c.S_P_MileStoneID); // FK__S_P_MileS__S_P_M__143CDA05
        }
    }

    // S_P_MileStoneHistory
    internal partial class S_P_MileStoneHistoryConfiguration : EntityTypeConfiguration<S_P_MileStoneHistory>
    {
        public S_P_MileStoneHistoryConfiguration()
        {
			ToTable("S_P_MILESTONEHISTORY");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.MileStoneID).HasColumnName("MILESTONEID").IsOptional().HasMaxLength(50);
            Property(x => x.ProjectInfoID).HasColumnName("PROJECTINFOID").IsOptional().HasMaxLength(50);
            Property(x => x.WBSID).HasColumnName("WBSID").IsOptional().HasMaxLength(50);
            Property(x => x.ChangeReason).HasColumnName("CHANGEREASON").IsOptional().HasMaxLength(2000);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(500);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(500);
            Property(x => x.MileStoneLevel).HasColumnName("MILESTONELEVEL").IsOptional().HasMaxLength(50);
            Property(x => x.PlanFinishDate).HasColumnName("PLANFINISHDATE").IsOptional();
            Property(x => x.PreviosPlanFinishDate).HasColumnName("PREVIOSPLANFINISHDATE").IsOptional();
            Property(x => x.OrlPlanFinishDate).HasColumnName("ORLPLANFINISHDATE").IsOptional();
            Property(x => x.MileStoneType).HasColumnName("MILESTONETYPE").IsOptional().HasMaxLength(50);
            Property(x => x.Weight).HasColumnName("WEIGHT").IsOptional().HasPrecision(18,2);
            Property(x => x.ChangeTime).HasColumnName("CHANGETIME").IsOptional();
        }
    }

    // S_P_MileStonePlan
    internal partial class S_P_MileStonePlanConfiguration : EntityTypeConfiguration<S_P_MileStonePlan>
    {
        public S_P_MileStonePlanConfiguration()
        {
			ToTable("S_P_MILESTONEPLAN");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.ProjectInfoID).HasColumnName("PROJECTINFOID").IsRequired().HasMaxLength(50);
            Property(x => x.MileStoneID).HasColumnName("MILESTONEID").IsRequired().HasMaxLength(50);
            Property(x => x.WBSID).HasColumnName("WBSID").IsOptional().HasMaxLength(50);
            Property(x => x.Name).HasColumnName("NAME").IsRequired().HasMaxLength(500);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(500);
            Property(x => x.OrlPlanFinishDate).HasColumnName("ORLPLANFINISHDATE").IsOptional();
            Property(x => x.PlanFinishDate).HasColumnName("PLANFINISHDATE").IsOptional();
            Property(x => x.DeptID).HasColumnName("DEPTID").IsOptional().HasMaxLength(500);
            Property(x => x.DeptName).HasColumnName("DEPTNAME").IsOptional().HasMaxLength(500);
            Property(x => x.Year).HasColumnName("YEAR").IsOptional();
            Property(x => x.Month).HasColumnName("MONTH").IsOptional();
            Property(x => x.Season).HasColumnName("SEASON").IsOptional();
            Property(x => x.MileStoneValue).HasColumnName("MILESTONEVALUE").IsOptional().HasMaxLength(500);
            Property(x => x.MileStoneType).HasColumnName("MILESTONETYPE").IsOptional().HasMaxLength(50);
            Property(x => x.MajorValue).HasColumnName("MAJORVALUE").IsOptional().HasMaxLength(500);
            Property(x => x.MileStoneLevel).HasColumnName("MILESTONELEVEL").IsOptional().HasMaxLength(50);
            Property(x => x.State).HasColumnName("STATE").IsOptional().HasMaxLength(50);
            Property(x => x.Stauts).HasColumnName("STAUTS").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.Description).HasColumnName("DESCRIPTION").IsOptional().HasMaxLength(500);

            // Foreign keys
            HasRequired(a => a.S_P_MileStone).WithMany(b => b.S_P_MileStonePlan).HasForeignKey(c => c.MileStoneID); // FK_S_P_MileStonePlan_S_P_MileStone
        }
    }

    // S_Q_QBS
    internal partial class S_Q_QBSConfiguration : EntityTypeConfiguration<S_Q_QBS>
    {
        public S_Q_QBSConfiguration()
        {
			ToTable("S_Q_QBS");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.ParentID).HasColumnName("PARENTID").IsOptional().HasMaxLength(50);
            Property(x => x.FullID).HasColumnName("FULLID").IsRequired().HasMaxLength(50);
            Property(x => x.DefineID).HasColumnName("DEFINEID").IsOptional().HasMaxLength(50);
            Property(x => x.ProjectInfoID).HasColumnName("PROJECTINFOID").IsRequired().HasMaxLength(50);
            Property(x => x.NodeType).HasColumnName("NODETYPE").IsOptional().HasMaxLength(50);
            Property(x => x.QBSType).HasColumnName("QBSTYPE").IsOptional().HasMaxLength(50);
            Property(x => x.Name).HasColumnName("NAME").IsRequired().HasMaxLength(50);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(50);
            Property(x => x.ConnName).HasColumnName("CONNNAME").IsOptional().HasMaxLength(50);
            Property(x => x.SQL).HasColumnName("SQL").IsOptional().HasMaxLength(1073741823);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.RelateFormID).HasColumnName("RELATEFORMID").IsOptional().HasMaxLength(50);
            Property(x => x.LinkUrl).HasColumnName("LINKURL").IsOptional().HasMaxLength(500);
            Property(x => x.FinishDate).HasColumnName("FINISHDATE").IsOptional();
            Property(x => x.State).HasColumnName("STATE").IsRequired().HasMaxLength(50);

            // Foreign keys
            HasRequired(a => a.S_I_ProjectInfo).WithMany(b => b.S_Q_QBS).HasForeignKey(c => c.ProjectInfoID); // FK_S_Q_QBS_S_I_ProjectInfo
        }
    }

    // S_R_Risk
    internal partial class S_R_RiskConfiguration : EntityTypeConfiguration<S_R_Risk>
    {
        public S_R_RiskConfiguration()
        {
			ToTable("S_R_RISK");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.ProjectInfoID).HasColumnName("PROJECTINFOID").IsRequired().HasMaxLength(50);
            Property(x => x.Name).HasColumnName("NAME").IsRequired().HasMaxLength(500);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(500);
            Property(x => x.RiskLevel).HasColumnName("RISKLEVEL").IsOptional().HasMaxLength(50);
            Property(x => x.RiskContent).HasColumnName("RISKCONTENT").IsOptional();
            Property(x => x.Measure).HasColumnName("MEASURE").IsOptional();
            Property(x => x.Attachments).HasColumnName("ATTACHMENTS").IsOptional().HasMaxLength(500);
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsRequired().HasMaxLength(50);
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsRequired().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsRequired();
            Property(x => x.LastModifyUserID).HasColumnName("LASTMODIFYUSERID").IsRequired().HasMaxLength(50);
            Property(x => x.LastModifyUser).HasColumnName("LASTMODIFYUSER").IsRequired().HasMaxLength(50);
            Property(x => x.LastModifyDate).HasColumnName("LASTMODIFYDATE").IsRequired();

            // Foreign keys
            HasRequired(a => a.S_I_ProjectInfo).WithMany(b => b.S_R_Risk).HasForeignKey(c => c.ProjectInfoID); // FK_S_R_Risk_S_I_ProjectInfo
        }
    }

    // S_T_ToDoList
    internal partial class S_T_ToDoListConfiguration : EntityTypeConfiguration<S_T_ToDoList>
    {
        public S_T_ToDoListConfiguration()
        {
			ToTable("S_T_TODOLIST");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.Name).HasColumnName("NAME").IsRequired().HasMaxLength(500);
            Property(x => x.ExecUserID).HasColumnName("EXECUSERID").IsRequired().HasMaxLength(500);
            Property(x => x.ExecUserName).HasColumnName("EXECUSERNAME").IsRequired().HasMaxLength(500);
            Property(x => x.State).HasColumnName("STATE").IsRequired().HasMaxLength(50);
            Property(x => x.FinishTime).HasColumnName("FINISHTIME").IsOptional();
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsRequired();
            Property(x => x.LinkUrl).HasColumnName("LINKURL").IsOptional().HasMaxLength(500);
            Property(x => x.OpenWidth).HasColumnName("OPENWIDTH").IsOptional().HasMaxLength(50);
            Property(x => x.OpenHeight).HasColumnName("OPENHEIGHT").IsOptional().HasMaxLength(50);
            Property(x => x.DefineNodeID).HasColumnName("DEFINENODEID").IsOptional().HasMaxLength(50);
            Property(x => x.DefineNodeName).HasColumnName("DEFINENODENAME").IsOptional().HasMaxLength(500);
            Property(x => x.DefineID).HasColumnName("DEFINEID").IsOptional().HasMaxLength(50);
            Property(x => x.DefineName).HasColumnName("DEFINENAME").IsOptional().HasMaxLength(500);
            Property(x => x.ExecData).HasColumnName("EXECDATA").IsOptional().HasMaxLength(500);
            Property(x => x.FinishData).HasColumnName("FINISHDATA").IsOptional().HasMaxLength(500);
            Property(x => x.EngineeringInfoID).HasColumnName("ENGINEERINGINFOID").IsOptional().HasMaxLength(50);
            Property(x => x.ProjectInfoID).HasColumnName("PROJECTINFOID").IsOptional().HasMaxLength(50);
            Property(x => x.ExeAction).HasColumnName("EXEACTION").IsOptional().HasMaxLength(1073741823);
        }
    }

    // S_W_Activity
    internal partial class S_W_ActivityConfiguration : EntityTypeConfiguration<S_W_Activity>
    {
        public S_W_ActivityConfiguration()
        {
			ToTable("S_W_ACTIVITY");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.DisplayName).HasColumnName("DISPLAYNAME").IsRequired().HasMaxLength(500);
            Property(x => x.ActvityName).HasColumnName("ACTVITYNAME").IsRequired().HasMaxLength(500);
            Property(x => x.ActivityKey).HasColumnName("ACTIVITYKEY").IsRequired().HasMaxLength(50);
            Property(x => x.ProjectInfoID).HasColumnName("PROJECTINFOID").IsRequired().HasMaxLength(50);
            Property(x => x.BusniessID).HasColumnName("BUSNIESSID").IsOptional().HasMaxLength(50);
            Property(x => x.WBSID).HasColumnName("WBSID").IsOptional().HasMaxLength(50);
            Property(x => x.AuditPatchID).HasColumnName("AUDITPATCHID").IsOptional().HasMaxLength(50);
            Property(x => x.GroupID).HasColumnName("GROUPID").IsOptional().HasMaxLength(50);
            Property(x => x.OwnerUserID).HasColumnName("OWNERUSERID").IsRequired().HasMaxLength(500);
            Property(x => x.OwnerUserName).HasColumnName("OWNERUSERNAME").IsRequired().HasMaxLength(500);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsRequired();
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsRequired().HasMaxLength(50);
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsRequired().HasMaxLength(50);
            Property(x => x.FinishDate).HasColumnName("FINISHDATE").IsOptional();
            Property(x => x.State).HasColumnName("STATE").IsRequired().HasMaxLength(50);
            Property(x => x.LinkUrl).HasColumnName("LINKURL").IsOptional();
            Property(x => x.Params).HasColumnName("PARAMS").IsOptional();
            Property(x => x.DefSteps).HasColumnName("DEFSTEPS").IsOptional();
            Property(x => x.NextStep).HasColumnName("NEXTSTEP").IsOptional();
            Property(x => x.ExeucteOption).HasColumnName("EXEUCTEOPTION").IsOptional().HasMaxLength(50);
            Property(x => x.ExecRoutingName).HasColumnName("EXECROUTINGNAME").IsOptional().HasMaxLength(100);
        }
    }

    // S_W_CooperationExe
    internal partial class S_W_CooperationExeConfiguration : EntityTypeConfiguration<S_W_CooperationExe>
    {
        public S_W_CooperationExeConfiguration()
        {
			ToTable("S_W_COOPERATIONEXE");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.Name).HasColumnName("NAME").IsRequired().HasMaxLength(500);
            Property(x => x.ProjectInfoID).HasColumnName("PROJECTINFOID").IsRequired().HasMaxLength(50);
            Property(x => x.RelateMileStoneID).HasColumnName("RELATEMILESTONEID").IsOptional().HasMaxLength(50);
            Property(x => x.BatchID).HasColumnName("BATCHID").IsOptional().HasMaxLength(50);
            Property(x => x.RelatePlanID).HasColumnName("RELATEPLANID").IsOptional().HasMaxLength(50);
            Property(x => x.RelateWBSID).HasColumnName("RELATEWBSID").IsOptional().HasMaxLength(50);
            Property(x => x.OutMajor).HasColumnName("OUTMAJOR").IsRequired().HasMaxLength(50);
            Property(x => x.InMajor).HasColumnName("INMAJOR").IsOptional().HasMaxLength(500);
            Property(x => x.Files).HasColumnName("FILES").IsOptional().HasMaxLength(500);
            Property(x => x.PicNo).HasColumnName("PICNO").IsOptional().HasMaxLength(50);
            Property(x => x.DrawingNo).HasColumnName("DRAWINGNO").IsOptional().HasMaxLength(50);
            Property(x => x.FetchOutDate).HasColumnName("FETCHOUTDATE").IsOptional();
            Property(x => x.FetchOutUser).HasColumnName("FETCHOUTUSER").IsOptional().HasMaxLength(50);
            Property(x => x.FetchOutUserID).HasColumnName("FETCHOUTUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ReceiveDate).HasColumnName("RECEIVEDATE").IsOptional();
            Property(x => x.ReceiveState).HasColumnName("RECEIVESTATE").IsOptional().HasMaxLength(50);
            Property(x => x.ReceiveUser).HasColumnName("RECEIVEUSER").IsOptional().HasMaxLength(50);
            Property(x => x.ReceiveUserID).HasColumnName("RECEIVEUSERID").IsOptional().HasMaxLength(50);
        }
    }

    // S_W_Monomer
    internal partial class S_W_MonomerConfiguration : EntityTypeConfiguration<S_W_Monomer>
    {
        public S_W_MonomerConfiguration()
        {
			ToTable("S_W_MONOMER");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.Name).HasColumnName("NAME").IsRequired().HasMaxLength(500);
            Property(x => x.Code).HasColumnName("CODE").IsRequired().HasMaxLength(500);
            Property(x => x.ProjectInfoID).HasColumnName("PROJECTINFOID").IsRequired().HasMaxLength(50);
            Property(x => x.WBSID).HasColumnName("WBSID").IsRequired().HasMaxLength(50);
            Property(x => x.SchemeFormSubID).HasColumnName("SCHEMEFORMSUBID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsRequired();
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsRequired().HasMaxLength(50);
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsRequired().HasMaxLength(50);
            Property(x => x.Area).HasColumnName("AREA").IsOptional();

            // Foreign keys
            HasRequired(a => a.S_W_WBS).WithMany(b => b.S_W_Monomer).HasForeignKey(c => c.WBSID); // FK_S_W_SubProject_S_W_WBS
        }
    }

    // S_W_OBSUser
    internal partial class S_W_OBSUserConfiguration : EntityTypeConfiguration<S_W_OBSUser>
    {
        public S_W_OBSUserConfiguration()
        {
			ToTable("S_W_OBSUSER");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.ProjectInfoID).HasColumnName("PROJECTINFOID").IsRequired().HasMaxLength(50);
            Property(x => x.MajorValue).HasColumnName("MAJORVALUE").IsOptional().HasMaxLength(50);
            Property(x => x.MajorName).HasColumnName("MAJORNAME").IsOptional().HasMaxLength(50);
            Property(x => x.RoleCode).HasColumnName("ROLECODE").IsRequired().HasMaxLength(50);
            Property(x => x.RoleName).HasColumnName("ROLENAME").IsRequired().HasMaxLength(50);
            Property(x => x.UserID).HasColumnName("USERID").IsOptional().HasMaxLength(50);
            Property(x => x.UserName).HasColumnName("USERNAME").IsOptional().HasMaxLength(50);
            Property(x => x.DeptID).HasColumnName("DEPTID").IsOptional().HasMaxLength(50);
            Property(x => x.DeptName).HasColumnName("DEPTNAME").IsOptional().HasMaxLength(50);
            Property(x => x.OBSID).HasColumnName("OBSID").IsOptional().HasMaxLength(100);
            Property(x => x.OBSFULLID).HasColumnName("OBSFULLID").IsOptional().HasMaxLength(100);
            Property(x => x.IsCloud).HasColumnName("ISCLOUD").IsOptional().HasMaxLength(50);

            // Foreign keys
            HasRequired(a => a.S_I_ProjectInfo).WithMany(b => b.S_W_OBSUser).HasForeignKey(c => c.ProjectInfoID); // FK_S_W_OBSUser_S_I_ProjectInfo
        }
    }

    // S_W_RBS
    internal partial class S_W_RBSConfiguration : EntityTypeConfiguration<S_W_RBS>
    {
        public S_W_RBSConfiguration()
        {
			ToTable("S_W_RBS");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.WBSID).HasColumnName("WBSID").IsRequired().HasMaxLength(50);
            Property(x => x.WBSCode).HasColumnName("WBSCODE").IsOptional().HasMaxLength(500);
            Property(x => x.WBSType).HasColumnName("WBSTYPE").IsRequired().HasMaxLength(50);
            Property(x => x.ProjectInfoID).HasColumnName("PROJECTINFOID").IsRequired().HasMaxLength(50);
            Property(x => x.OBSID).HasColumnName("OBSID").IsOptional().HasMaxLength(50);
            Property(x => x.MajorValue).HasColumnName("MAJORVALUE").IsOptional().HasMaxLength(50);
            Property(x => x.RoleCode).HasColumnName("ROLECODE").IsRequired().HasMaxLength(50);
            Property(x => x.RoleName).HasColumnName("ROLENAME").IsRequired().HasMaxLength(50);
            Property(x => x.UserID).HasColumnName("USERID").IsRequired().HasMaxLength(200);
            Property(x => x.UserName).HasColumnName("USERNAME").IsRequired().HasMaxLength(200);
            Property(x => x.UserDeptID).HasColumnName("USERDEPTID").IsOptional().HasMaxLength(500);
            Property(x => x.UserDeptName).HasColumnName("USERDEPTNAME").IsOptional().HasMaxLength(500);
            Property(x => x.PlanStartDate).HasColumnName("PLANSTARTDATE").IsOptional();
            Property(x => x.PlanEndDate).HasColumnName("PLANENDDATE").IsOptional();
            Property(x => x.FactStartDate).HasColumnName("FACTSTARTDATE").IsOptional();
            Property(x => x.FactEndDate).HasColumnName("FACTENDDATE").IsOptional();

            // Foreign keys
            HasRequired(a => a.S_W_WBS).WithMany(b => b.S_W_RBS).HasForeignKey(c => c.WBSID); // FK_S_W_RBS_S_W_WBS
        }
    }

    // S_W_StandardWorkTime
    internal partial class S_W_StandardWorkTimeConfiguration : EntityTypeConfiguration<S_W_StandardWorkTime>
    {
        public S_W_StandardWorkTimeConfiguration()
        {
			ToTable("S_W_STANDARDWORKTIME");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.ProjectInfoID).HasColumnName("PROJECTINFOID").IsOptional().HasMaxLength(50);
            Property(x => x.ProjectInfoCode).HasColumnName("PROJECTINFOCODE").IsOptional().HasMaxLength(50);
            Property(x => x.ProjectInfoName).HasColumnName("PROJECTINFONAME").IsOptional().HasMaxLength(200);
            Property(x => x.WBSID).HasColumnName("WBSID").IsOptional().HasMaxLength(50);
            Property(x => x.TaskWorkID).HasColumnName("TASKWORKID").IsOptional().HasMaxLength(50);
            Property(x => x.TaskWorkCode).HasColumnName("TASKWORKCODE").IsOptional().HasMaxLength(50);
            Property(x => x.WorkDay).HasColumnName("WORKDAY").IsOptional();
            Property(x => x.MajorCode).HasColumnName("MAJORCODE").IsOptional().HasMaxLength(50);
            Property(x => x.MajorName).HasColumnName("MAJORNAME").IsOptional().HasMaxLength(50);
            Property(x => x.DesignerUserID).HasColumnName("DESIGNERUSERID").IsOptional().HasMaxLength(500);
            Property(x => x.DesignerUserName).HasColumnName("DESIGNERUSERNAME").IsOptional().HasMaxLength(500);
            Property(x => x.CollactorUserID).HasColumnName("COLLACTORUSERID").IsOptional().HasMaxLength(500);
            Property(x => x.CollactorUserName).HasColumnName("COLLACTORUSERNAME").IsOptional().HasMaxLength(500);
            Property(x => x.AuditorUserID).HasColumnName("AUDITORUSERID").IsOptional().HasMaxLength(500);
            Property(x => x.AuditorUserName).HasColumnName("AUDITORUSERNAME").IsOptional().HasMaxLength(500);
            Property(x => x.ApproverUserID).HasColumnName("APPROVERUSERID").IsOptional().HasMaxLength(500);
            Property(x => x.ApproverUserName).HasColumnName("APPROVERUSERNAME").IsOptional().HasMaxLength(500);
            Property(x => x.MapperUserID).HasColumnName("MAPPERUSERID").IsOptional().HasMaxLength(500);
            Property(x => x.MapperUserName).HasColumnName("MAPPERUSERNAME").IsOptional().HasMaxLength(500);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyDate).HasColumnName("MODIFYDATE").IsOptional();
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUser).HasColumnName("MODIFYUSER").IsOptional().HasMaxLength(50);
        }
    }

    // S_W_StandardWorkTimeDetail
    internal partial class S_W_StandardWorkTimeDetailConfiguration : EntityTypeConfiguration<S_W_StandardWorkTimeDetail>
    {
        public S_W_StandardWorkTimeDetailConfiguration()
        {
			ToTable("S_W_STANDARDWORKTIMEDETAIL");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.FormID).HasColumnName("FORMID").IsOptional().HasMaxLength(50);
            Property(x => x.ProjectInfoID).HasColumnName("PROJECTINFOID").IsOptional().HasMaxLength(50);
            Property(x => x.WBSID).HasColumnName("WBSID").IsOptional().HasMaxLength(50);
            Property(x => x.TaskWorkID).HasColumnName("TASKWORKID").IsOptional().HasMaxLength(50);
            Property(x => x.UserID).HasColumnName("USERID").IsOptional().HasMaxLength(50);
            Property(x => x.UserName).HasColumnName("USERNAME").IsOptional().HasMaxLength(50);
            Property(x => x.RoleCode).HasColumnName("ROLECODE").IsOptional().HasMaxLength(50);
            Property(x => x.RoleName).HasColumnName("ROLENAME").IsOptional().HasMaxLength(50);
            Property(x => x.WorkDay).HasColumnName("WORKDAY").IsOptional();
            Property(x => x.ScaleRatio).HasColumnName("SCALERATIO").IsOptional();
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyDate).HasColumnName("MODIFYDATE").IsOptional();
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUser).HasColumnName("MODIFYUSER").IsOptional().HasMaxLength(50);

            // Foreign keys
            HasOptional(a => a.S_W_StandardWorkTime).WithMany(b => b.S_W_StandardWorkTimeDetail).HasForeignKey(c => c.FormID); // FK_S_W_StandardWorkTimeDetail_S_W_StandardWorkTime
        }
    }

    // S_W_TaskWork
    internal partial class S_W_TaskWorkConfiguration : EntityTypeConfiguration<S_W_TaskWork>
    {
        public S_W_TaskWorkConfiguration()
        {
			ToTable("S_W_TASKWORK");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.ProjectInfoID).HasColumnName("PROJECTINFOID").IsRequired().HasMaxLength(50);
            Property(x => x.WBSID).HasColumnName("WBSID").IsRequired().HasMaxLength(50);
            Property(x => x.WBSFullID).HasColumnName("WBSFULLID").IsRequired().HasMaxLength(500);
            Property(x => x.SubProjectCode).HasColumnName("SUBPROJECTCODE").IsOptional().HasMaxLength(500);
            Property(x => x.MajorValue).HasColumnName("MAJORVALUE").IsOptional().HasMaxLength(50);
            Property(x => x.PhaseValue).HasColumnName("PHASEVALUE").IsOptional().HasMaxLength(50);
            Property(x => x.AreaCode).HasColumnName("AREACODE").IsOptional().HasMaxLength(500);
            Property(x => x.DeviceCode).HasColumnName("DEVICECODE").IsOptional().HasMaxLength(500);
            Property(x => x.Name).HasColumnName("NAME").IsRequired().HasMaxLength(500);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(500);
            Property(x => x.Workload).HasColumnName("WORKLOAD").IsOptional().HasPrecision(18,2);
            Property(x => x.WorkloadDistribute).HasColumnName("WORKLOADDISTRIBUTE").IsOptional().HasMaxLength(500);
            Property(x => x.WorkloadFinish).HasColumnName("WORKLOADFINISH").IsOptional().HasPrecision(18,2);
            Property(x => x.PlanStartDate).HasColumnName("PLANSTARTDATE").IsOptional();
            Property(x => x.PlanEndDate).HasColumnName("PLANENDDATE").IsOptional();
            Property(x => x.PlanPublishDate).HasColumnName("PLANPUBLISHDATE").IsOptional();
            Property(x => x.StandartWorkDay).HasColumnName("STANDARTWORKDAY").IsOptional();
            Property(x => x.DesignDate).HasColumnName("DESIGNDATE").IsOptional();
            Property(x => x.CollactDate).HasColumnName("COLLACTDATE").IsOptional();
            Property(x => x.AuditDate).HasColumnName("AUDITDATE").IsOptional();
            Property(x => x.CountersignDate).HasColumnName("COUNTERSIGNDATE").IsOptional();
            Property(x => x.ApproveDate).HasColumnName("APPROVEDATE").IsOptional();
            Property(x => x.DeliveryDate).HasColumnName("DELIVERYDATE").IsOptional();
            Property(x => x.FileDate).HasColumnName("FILEDATE").IsOptional();
            Property(x => x.PlanYear).HasColumnName("PLANYEAR").IsOptional();
            Property(x => x.PlanSeason).HasColumnName("PLANSEASON").IsOptional();
            Property(x => x.PlanMonth).HasColumnName("PLANMONTH").IsOptional();
            Property(x => x.FactEndDate).HasColumnName("FACTENDDATE").IsOptional();
            Property(x => x.FactPublishDate).HasColumnName("FACTPUBLISHDATE").IsOptional();
            Property(x => x.FactYear).HasColumnName("FACTYEAR").IsOptional();
            Property(x => x.FactSeason).HasColumnName("FACTSEASON").IsOptional();
            Property(x => x.FactMonth).HasColumnName("FACTMONTH").IsOptional();
            Property(x => x.ChargeUserID).HasColumnName("CHARGEUSERID").IsOptional().HasMaxLength(500);
            Property(x => x.ChargeUserName).HasColumnName("CHARGEUSERNAME").IsOptional().HasMaxLength(500);
            Property(x => x.ChargeDeptID).HasColumnName("CHARGEDEPTID").IsOptional().HasMaxLength(500);
            Property(x => x.ChargeDeptName).HasColumnName("CHARGEDEPTNAME").IsOptional().HasMaxLength(500);
            Property(x => x.DesignerUserID).HasColumnName("DESIGNERUSERID").IsOptional().HasMaxLength(500);
            Property(x => x.DesignerUserName).HasColumnName("DESIGNERUSERNAME").IsOptional().HasMaxLength(500);
            Property(x => x.CollactorUserID).HasColumnName("COLLACTORUSERID").IsOptional().HasMaxLength(500);
            Property(x => x.CollactorUserName).HasColumnName("COLLACTORUSERNAME").IsOptional().HasMaxLength(500);
            Property(x => x.AuditorUserID).HasColumnName("AUDITORUSERID").IsOptional().HasMaxLength(500);
            Property(x => x.AuditorUserName).HasColumnName("AUDITORUSERNAME").IsOptional().HasMaxLength(500);
            Property(x => x.ApproverUserID).HasColumnName("APPROVERUSERID").IsOptional().HasMaxLength(500);
            Property(x => x.ApproverUserName).HasColumnName("APPROVERUSERNAME").IsOptional().HasMaxLength(500);
            Property(x => x.MapperUserID).HasColumnName("MAPPERUSERID").IsOptional().HasMaxLength(500);
            Property(x => x.MapperUserName).HasColumnName("MAPPERUSERNAME").IsOptional().HasMaxLength(500);
            Property(x => x.State).HasColumnName("STATE").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsOptional().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUser).HasColumnName("MODIFYUSER").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyDate).HasColumnName("MODIFYDATE").IsOptional();
            Property(x => x.RoleRate).HasColumnName("ROLERATE").IsOptional();
            Property(x => x.PublishID).HasColumnName("PUBLISHID").IsOptional().HasMaxLength(50);
            Property(x => x.ChangeID).HasColumnName("CHANGEID").IsOptional().HasMaxLength(50);
            Property(x => x.SettlementID).HasColumnName("SETTLEMENTID").IsOptional().HasMaxLength(50);
            Property(x => x.DossierName).HasColumnName("DOSSIERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.DossierCode).HasColumnName("DOSSIERCODE").IsOptional().HasMaxLength(200);
            Property(x => x.Version).HasColumnName("VERSION").IsOptional().HasPrecision(18,2);
            Property(x => x.ChangeState).HasColumnName("CHANGESTATE").IsOptional().HasMaxLength(50);
            Property(x => x.EstimateType).HasColumnName("ESTIMATETYPE").IsOptional().HasMaxLength(200);
            Property(x => x.EstimateQuantity).HasColumnName("ESTIMATEQUANTITY").IsOptional().HasPrecision(18,2);
            Property(x => x.EstimateUnitPrice).HasColumnName("ESTIMATEUNITPRICE").IsOptional().HasPrecision(18,2);
            Property(x => x.EstimateValue).HasColumnName("ESTIMATEVALUE").IsOptional().HasPrecision(18,6);
            Property(x => x.TotalValue).HasColumnName("TOTALVALUE").IsOptional().HasPrecision(18,6);
            Property(x => x.TemplateWorkload).HasColumnName("TEMPLATEWORKLOAD").IsOptional().HasPrecision(18,2);
            Property(x => x.Coefficient).HasColumnName("COEFFICIENT").IsOptional().HasPrecision(18,2);

            // Foreign keys
            HasRequired(a => a.S_W_WBS).WithMany(b => b.S_W_TaskWork).HasForeignKey(c => c.WBSID); // FK_S_W_TaskWork_S_W_WBS
        }
    }

    // S_W_TaskWork_RoleRate
    internal partial class S_W_TaskWork_RoleRateConfiguration : EntityTypeConfiguration<S_W_TaskWork_RoleRate>
    {
        public S_W_TaskWork_RoleRateConfiguration()
        {
			ToTable("S_W_TASKWORK_ROLERATE");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.S_W_TaskWorkID).HasColumnName("S_W_TASKWORKID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.Role).HasColumnName("ROLE").IsOptional().HasMaxLength(200);
            Property(x => x.Member).HasColumnName("MEMBER").IsOptional().HasMaxLength(200);
            Property(x => x.MemberName).HasColumnName("MEMBERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Rate).HasColumnName("RATE").IsOptional().HasPrecision(18,2);
            Property(x => x.Workload).HasColumnName("WORKLOAD").IsOptional().HasPrecision(18,2);

            // Foreign keys
            HasOptional(a => a.S_W_TaskWork).WithMany(b => b.S_W_TaskWork_RoleRate).HasForeignKey(c => c.S_W_TaskWorkID); // FK_S_W_TaskWork_RoleRate_S_W_TaskWork
        }
    }

    // S_W_WBS
    internal partial class S_W_WBSConfiguration : EntityTypeConfiguration<S_W_WBS>
    {
        public S_W_WBSConfiguration()
        {
			ToTable("S_W_WBS");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.ProjectInfoID).HasColumnName("PROJECTINFOID").IsRequired().HasMaxLength(50);
            Property(x => x.ParentID).HasColumnName("PARENTID").IsOptional().HasMaxLength(50);
            Property(x => x.FullID).HasColumnName("FULLID").IsRequired().HasMaxLength(500);
            Property(x => x.Name).HasColumnName("NAME").IsRequired().HasMaxLength(500);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(200);
            Property(x => x.WBSValue).HasColumnName("WBSVALUE").IsRequired().HasMaxLength(200);
            Property(x => x.WBSType).HasColumnName("WBSTYPE").IsRequired().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsRequired();
            Property(x => x.Level).HasColumnName("LEVEL").IsRequired();
            Property(x => x.State).HasColumnName("STATE").IsRequired().HasMaxLength(50);
            Property(x => x.WBSDeptID).HasColumnName("WBSDEPTID").IsOptional().HasMaxLength(500);
            Property(x => x.WBSDeptName).HasColumnName("WBSDEPTNAME").IsOptional().HasMaxLength(500);
            Property(x => x.ChargeUserID).HasColumnName("CHARGEUSERID").IsOptional().HasMaxLength(500);
            Property(x => x.ChargeUserName).HasColumnName("CHARGEUSERNAME").IsOptional().HasMaxLength(500);
            Property(x => x.WBSStructCode).HasColumnName("WBSSTRUCTCODE").IsRequired().HasMaxLength(50);
            Property(x => x.PlanStartDate).HasColumnName("PLANSTARTDATE").IsOptional();
            Property(x => x.PlanEndDate).HasColumnName("PLANENDDATE").IsOptional();
            Property(x => x.FactStartDate).HasColumnName("FACTSTARTDATE").IsOptional();
            Property(x => x.FactEndDate).HasColumnName("FACTENDDATE").IsOptional();
            Property(x => x.PlanWorkLoad).HasColumnName("PLANWORKLOAD").IsOptional().HasPrecision(18,2);
            Property(x => x.WorkLoad).HasColumnName("WORKLOAD").IsOptional().HasPrecision(18,2);
            Property(x => x.Weight).HasColumnName("WEIGHT").IsOptional().HasPrecision(18,2);
            Property(x => x.ProjectCode).HasColumnName("PROJECTCODE").IsOptional().HasMaxLength(500);
            Property(x => x.SubProjectCode).HasColumnName("SUBPROJECTCODE").IsOptional().HasMaxLength(500);
            Property(x => x.PhaseCode).HasColumnName("PHASECODE").IsOptional().HasMaxLength(500);
            Property(x => x.MajorCode).HasColumnName("MAJORCODE").IsOptional().HasMaxLength(500);
            Property(x => x.WorkCode).HasColumnName("WORKCODE").IsOptional().HasMaxLength(500);
            Property(x => x.EntityCode).HasColumnName("ENTITYCODE").IsOptional().HasMaxLength(500);
            Property(x => x.AreaCode).HasColumnName("AREACODE").IsOptional().HasMaxLength(500);
            Property(x => x.DeviceCode).HasColumnName("DEVICECODE").IsOptional().HasMaxLength(500);
            Property(x => x.CodeIndex).HasColumnName("CODEINDEX").IsOptional();
            Property(x => x.ExtField1).HasColumnName("EXTFIELD1").IsOptional().HasMaxLength(500);
            Property(x => x.ExtField2).HasColumnName("EXTFIELD2").IsOptional().HasMaxLength(500);
            Property(x => x.ExtField3).HasColumnName("EXTFIELD3").IsOptional().HasMaxLength(2000);
            Property(x => x.ExtField4).HasColumnName("EXTFIELD4").IsOptional().HasMaxLength(500);
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUser).HasColumnName("MODIFYUSER").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyDate).HasColumnName("MODIFYDATE").IsOptional();
            Property(x => x.RelateMileStone).HasColumnName("RELATEMILESTONE").IsOptional().HasMaxLength(2000);
            Property(x => x.BasePlanStartDate).HasColumnName("BASEPLANSTARTDATE").IsOptional();
            Property(x => x.BasePlanEndDate).HasColumnName("BASEPLANENDDATE").IsOptional();

            // Foreign keys
            HasRequired(a => a.S_I_ProjectInfo).WithMany(b => b.S_W_WBS).HasForeignKey(c => c.ProjectInfoID); // FK_S_W_WBS_S_I_ProjectInfo
        }
    }

    // S_W_WBS_Version
    internal partial class S_W_WBS_VersionConfiguration : EntityTypeConfiguration<S_W_WBS_Version>
    {
        public S_W_WBS_VersionConfiguration()
        {
			ToTable("S_W_WBS_VERSION");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUser).HasColumnName("MODIFYUSER").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyDate).HasColumnName("MODIFYDATE").IsOptional();
            Property(x => x.OrgID).HasColumnName("ORGID").IsOptional().HasMaxLength(50);
            Property(x => x.StepName).HasColumnName("STEPNAME").IsOptional().HasMaxLength(50);
            Property(x => x.FlowInfo).HasColumnName("FLOWINFO").IsOptional().HasMaxLength(1073741823);
            Property(x => x.FlowPhase).HasColumnName("FLOWPHASE").IsOptional().HasMaxLength(50);
            Property(x => x.ProjectInfoID).HasColumnName("PROJECTINFOID").IsRequired().HasMaxLength(50);
            Property(x => x.VersionNumber).HasColumnName("VERSIONNUMBER").IsOptional().HasPrecision(18,2);
            Property(x => x.VersionName).HasColumnName("VERSIONNAME").IsOptional().HasMaxLength(500);
            Property(x => x.ProjectInfo).HasColumnName("PROJECTINFO").IsOptional().HasMaxLength(500);
            Property(x => x.ProjectInfoName).HasColumnName("PROJECTINFONAME").IsOptional().HasMaxLength(500);
            Property(x => x.ProjectInfoCode).HasColumnName("PROJECTINFOCODE").IsOptional().HasMaxLength(500);
            Property(x => x.Remark).HasColumnName("REMARK").IsOptional().HasMaxLength(500);

            // Foreign keys
            HasRequired(a => a.S_I_ProjectInfo).WithMany(b => b.S_W_WBS_Version).HasForeignKey(c => c.ProjectInfoID); // FK_S_W_WBS_Version_S_I_ProjectInfo
        }
    }

    // S_W_WBS_Version_Node
    internal partial class S_W_WBS_Version_NodeConfiguration : EntityTypeConfiguration<S_W_WBS_Version_Node>
    {
        public S_W_WBS_Version_NodeConfiguration()
        {
			ToTable("S_W_WBS_VERSION_NODE");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.ProjectInfoID).HasColumnName("PROJECTINFOID").IsRequired().HasMaxLength(50);
            Property(x => x.VersionID).HasColumnName("VERSIONID").IsRequired().HasMaxLength(50);
            Property(x => x.WBSID).HasColumnName("WBSID").IsRequired().HasMaxLength(50);
            Property(x => x.WBSParentID).HasColumnName("WBSPARENTID").IsOptional().HasMaxLength(50);
            Property(x => x.FullID).HasColumnName("FULLID").IsRequired().HasMaxLength(500);
            Property(x => x.Name).HasColumnName("NAME").IsRequired().HasMaxLength(500);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(200);
            Property(x => x.WBSValue).HasColumnName("WBSVALUE").IsRequired().HasMaxLength(200);
            Property(x => x.WBSType).HasColumnName("WBSTYPE").IsRequired().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsRequired();
            Property(x => x.Level).HasColumnName("LEVEL").IsOptional();
            Property(x => x.State).HasColumnName("STATE").IsRequired().HasMaxLength(50);
            Property(x => x.WBSDeptID).HasColumnName("WBSDEPTID").IsOptional().HasMaxLength(500);
            Property(x => x.WBSDeptName).HasColumnName("WBSDEPTNAME").IsOptional().HasMaxLength(500);
            Property(x => x.ChargeUserID).HasColumnName("CHARGEUSERID").IsOptional().HasMaxLength(500);
            Property(x => x.ChargeUserName).HasColumnName("CHARGEUSERNAME").IsOptional().HasMaxLength(500);
            Property(x => x.WBSStructCode).HasColumnName("WBSSTRUCTCODE").IsOptional().HasMaxLength(500);
            Property(x => x.PlanStartDate).HasColumnName("PLANSTARTDATE").IsOptional();
            Property(x => x.PlanEndDate).HasColumnName("PLANENDDATE").IsOptional();
            Property(x => x.FactStartDate).HasColumnName("FACTSTARTDATE").IsOptional();
            Property(x => x.FactEndDate).HasColumnName("FACTENDDATE").IsOptional();
            Property(x => x.PlanWorkLoad).HasColumnName("PLANWORKLOAD").IsOptional().HasPrecision(18,2);
            Property(x => x.WorkLoad).HasColumnName("WORKLOAD").IsOptional().HasPrecision(18,2);
            Property(x => x.Weight).HasColumnName("WEIGHT").IsOptional().HasPrecision(18,2);
            Property(x => x.ProjectCode).HasColumnName("PROJECTCODE").IsOptional().HasMaxLength(500);
            Property(x => x.SubProjectCode).HasColumnName("SUBPROJECTCODE").IsOptional().HasMaxLength(500);
            Property(x => x.PhaseCode).HasColumnName("PHASECODE").IsOptional().HasMaxLength(500);
            Property(x => x.MajorCode).HasColumnName("MAJORCODE").IsOptional().HasMaxLength(500);
            Property(x => x.WorkCode).HasColumnName("WORKCODE").IsOptional().HasMaxLength(500);
            Property(x => x.EntityCode).HasColumnName("ENTITYCODE").IsOptional().HasMaxLength(500);
            Property(x => x.AreaCode).HasColumnName("AREACODE").IsOptional().HasMaxLength(500);
            Property(x => x.DeviceCode).HasColumnName("DEVICECODE").IsOptional().HasMaxLength(500);
            Property(x => x.CodeIndex).HasColumnName("CODEINDEX").IsOptional();
            Property(x => x.ModifyState).HasColumnName("MODIFYSTATE").IsRequired().HasMaxLength(50);
            Property(x => x.RBSInfo).HasColumnName("RBSINFO").IsOptional();
            Property(x => x.ExtField1).HasColumnName("EXTFIELD1").IsOptional().HasMaxLength(500);
            Property(x => x.ExtField2).HasColumnName("EXTFIELD2").IsOptional().HasMaxLength(500);
            Property(x => x.ExtField3).HasColumnName("EXTFIELD3").IsOptional().HasMaxLength(500);
            Property(x => x.ExtField4).HasColumnName("EXTFIELD4").IsOptional().HasMaxLength(500);
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUser).HasColumnName("MODIFYUSER").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyDate).HasColumnName("MODIFYDATE").IsOptional();

            // Foreign keys
            HasRequired(a => a.S_W_WBS_Version).WithMany(b => b.S_W_WBS_Version_Node).HasForeignKey(c => c.VersionID); // FK_S_W_WBS_Version_Node_S_W_WBS_Version
        }
    }

    // T_CAD_WBSAttrView
    internal partial class T_CAD_WBSAttrViewConfiguration : EntityTypeConfiguration<T_CAD_WBSAttrView>
    {
        public T_CAD_WBSAttrViewConfiguration()
        {
			ToTable("T_CAD_WBSATTRVIEW");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.ModifyDate).HasColumnName("MODIFYDATE").IsOptional();
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUser).HasColumnName("MODIFYUSER").IsOptional().HasMaxLength(50);
            Property(x => x.OrgID).HasColumnName("ORGID").IsOptional().HasMaxLength(50);
            Property(x => x.CompanyID).HasColumnName("COMPANYID").IsOptional().HasMaxLength(50);
            Property(x => x.FlowPhase).HasColumnName("FLOWPHASE").IsOptional().HasMaxLength(50);
            Property(x => x.FlowInfo).HasColumnName("FLOWINFO").IsOptional().HasMaxLength(1073741823);
            Property(x => x.StepName).HasColumnName("STEPNAME").IsOptional().HasMaxLength(500);
            Property(x => x.ProjectName).HasColumnName("PROJECTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectCode).HasColumnName("PROJECTCODE").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectPhaseName).HasColumnName("PROJECTPHASENAME").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectDeptName).HasColumnName("PROJECTDEPTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectChangeUser).HasColumnName("PROJECTCHANGEUSER").IsOptional().HasMaxLength(200);
            Property(x => x.WBSName).HasColumnName("WBSNAME").IsOptional().HasMaxLength(200);
            Property(x => x.WBSCode).HasColumnName("WBSCODE").IsOptional().HasMaxLength(200);
            Property(x => x.WBSChargeUser).HasColumnName("WBSCHARGEUSER").IsOptional().HasMaxLength(200);
            Property(x => x.WBSMajorCode).HasColumnName("WBSMAJORCODE").IsOptional().HasMaxLength(200);
            Property(x => x.WBSPlanEndDate).HasColumnName("WBSPLANENDDATE").IsOptional().HasMaxLength(200);
            Property(x => x.WBSFactEndDate).HasColumnName("WBSFACTENDDATE").IsOptional().HasMaxLength(200);
        }
    }

    // T_CP_ProjectInfoChange
    internal partial class T_CP_ProjectInfoChangeConfiguration : EntityTypeConfiguration<T_CP_ProjectInfoChange>
    {
        public T_CP_ProjectInfoChangeConfiguration()
        {
			ToTable("T_CP_PROJECTINFOCHANGE");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.ModifyDate).HasColumnName("MODIFYDATE").IsOptional();
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUser).HasColumnName("MODIFYUSER").IsOptional().HasMaxLength(50);
            Property(x => x.OrgID).HasColumnName("ORGID").IsOptional().HasMaxLength(50);
            Property(x => x.CompanyID).HasColumnName("COMPANYID").IsOptional().HasMaxLength(50);
            Property(x => x.FlowPhase).HasColumnName("FLOWPHASE").IsOptional().HasMaxLength(50);
            Property(x => x.FlowInfo).HasColumnName("FLOWINFO").IsOptional().HasMaxLength(1073741823);
            Property(x => x.StepName).HasColumnName("STEPNAME").IsOptional().HasMaxLength(500);
            Property(x => x.ProjectInfoName).HasColumnName("PROJECTINFONAME").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectCode).HasColumnName("PROJECTCODE").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectClass).HasColumnName("PROJECTCLASS").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectLevel).HasColumnName("PROJECTLEVEL").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectDept).HasColumnName("PROJECTDEPT").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectDeptName).HasColumnName("PROJECTDEPTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectOtherDept).HasColumnName("PROJECTOTHERDEPT").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectOtherDeptName).HasColumnName("PROJECTOTHERDEPTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Phase).HasColumnName("PHASE").IsOptional().HasMaxLength(200);
            Property(x => x.OrgProjectManger).HasColumnName("ORGPROJECTMANGER").IsOptional().HasMaxLength(500);
            Property(x => x.OrgProjectMangerName).HasColumnName("ORGPROJECTMANGERNAME").IsOptional().HasMaxLength(500);
            Property(x => x.ProjectManager).HasColumnName("PROJECTMANAGER").IsOptional().HasMaxLength(500);
            Property(x => x.ProjectManagerName).HasColumnName("PROJECTMANAGERNAME").IsOptional().HasMaxLength(500);
            Property(x => x.DeptLeaderAdvice).HasColumnName("DEPTLEADERADVICE").IsOptional();
            Property(x => x.MarketAdvice).HasColumnName("MARKETADVICE").IsOptional();
            Property(x => x.LeaderAdvice).HasColumnName("LEADERADVICE").IsOptional();
            Property(x => x.MarketProjectInfo).HasColumnName("MARKETPROJECTINFO").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectInfoID).HasColumnName("PROJECTINFOID").IsOptional().HasMaxLength(200);
            Property(x => x.Leader).HasColumnName("LEADER").IsOptional().HasMaxLength(200);
        }
    }

    // T_CP_TaskNotice
    internal partial class T_CP_TaskNoticeConfiguration : EntityTypeConfiguration<T_CP_TaskNotice>
    {
        public T_CP_TaskNoticeConfiguration()
        {
			ToTable("T_CP_TASKNOTICE");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.ModifyDate).HasColumnName("MODIFYDATE").IsOptional();
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUser).HasColumnName("MODIFYUSER").IsOptional().HasMaxLength(50);
            Property(x => x.OrgID).HasColumnName("ORGID").IsOptional().HasMaxLength(50);
            Property(x => x.CompanyID).HasColumnName("COMPANYID").IsOptional().HasMaxLength(50);
            Property(x => x.FlowPhase).HasColumnName("FLOWPHASE").IsOptional().HasMaxLength(50);
            Property(x => x.FlowInfo).HasColumnName("FLOWINFO").IsOptional().HasMaxLength(1073741823);
            Property(x => x.StepName).HasColumnName("STEPNAME").IsOptional().HasMaxLength(500);
            Property(x => x.EngineeringCode).HasColumnName("ENGINEERINGCODE").IsOptional().HasMaxLength(200);
            Property(x => x.EngineeringName).HasColumnName("ENGINEERINGNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Country).HasColumnName("COUNTRY").IsOptional().HasMaxLength(200);
            Property(x => x.Province).HasColumnName("PROVINCE").IsOptional().HasMaxLength(200);
            Property(x => x.City).HasColumnName("CITY").IsOptional().HasMaxLength(200);
            Property(x => x.BuildAddress).HasColumnName("BUILDADDRESS").IsOptional().HasMaxLength(200);
            Property(x => x.LandArea).HasColumnName("LANDAREA").IsOptional().HasMaxLength(200);
            Property(x => x.BuildArea).HasColumnName("BUILDAREA").IsOptional().HasMaxLength(200);
            Property(x => x.EngineeringChargeUser).HasColumnName("ENGINEERINGCHARGEUSER").IsOptional().HasMaxLength(500);
            Property(x => x.EngineeringChargeUserName).HasColumnName("ENGINEERINGCHARGEUSERNAME").IsOptional().HasMaxLength(500);
            Property(x => x.MainDeptInfo).HasColumnName("MAINDEPTINFO").IsOptional().HasMaxLength(200);
            Property(x => x.MainDeptInfoName).HasColumnName("MAINDEPTINFONAME").IsOptional().HasMaxLength(200);
            Property(x => x.EngineeringOtherDept).HasColumnName("ENGINEERINGOTHERDEPT").IsOptional().HasMaxLength(200);
            Property(x => x.EngineeringOtherDeptName).HasColumnName("ENGINEERINGOTHERDEPTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectInfo).HasColumnName("PROJECTINFO").IsOptional().HasMaxLength(200);
            Property(x => x.ChargeUser).HasColumnName("CHARGEUSER").IsOptional().HasMaxLength(500);
            Property(x => x.ChargeUserName).HasColumnName("CHARGEUSERNAME").IsOptional().HasMaxLength(500);
            Property(x => x.ChargeDept).HasColumnName("CHARGEDEPT").IsOptional().HasMaxLength(200);
            Property(x => x.ChargeDeptName).HasColumnName("CHARGEDEPTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Customer).HasColumnName("CUSTOMER").IsOptional().HasMaxLength(200);
            Property(x => x.CustomerName).HasColumnName("CUSTOMERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.CustomerSub).HasColumnName("CUSTOMERSUB").IsOptional().HasMaxLength(200);
            Property(x => x.CustomerSubName).HasColumnName("CUSTOMERSUBNAME").IsOptional().HasMaxLength(200);
            Property(x => x.OtherDept).HasColumnName("OTHERDEPT").IsOptional().HasMaxLength(200);
            Property(x => x.OtherDeptName).HasColumnName("OTHERDEPTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.PlanStartDate).HasColumnName("PLANSTARTDATE").IsOptional();
            Property(x => x.PlanFinishDate).HasColumnName("PLANFINISHDATE").IsOptional();
            Property(x => x.Major).HasColumnName("MAJOR").IsOptional().HasMaxLength(200);
            Property(x => x.Remark).HasColumnName("REMARK").IsOptional().HasMaxLength(500);
            Property(x => x.Phase).HasColumnName("PHASE").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectClass).HasColumnName("PROJECTCLASS").IsOptional().HasMaxLength(200);
            Property(x => x.WorkContent).HasColumnName("WORKCONTENT").IsOptional().HasMaxLength(200);
            Property(x => x.SerialNumber).HasColumnName("SERIALNUMBER").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectLevel).HasColumnName("PROJECTLEVEL").IsOptional().HasMaxLength(200);
            Property(x => x.UpFloors).HasColumnName("UPFLOORS").IsOptional().HasMaxLength(200);
            Property(x => x.DownFloors).HasColumnName("DOWNFLOORS").IsOptional().HasMaxLength(200);
            Property(x => x.DefenseLevel).HasColumnName("DEFENSELEVEL").IsOptional().HasMaxLength(200);
            Property(x => x.Height).HasColumnName("HEIGHT").IsOptional().HasMaxLength(200);
            Property(x => x.StructuralStyle).HasColumnName("STRUCTURALSTYLE").IsOptional().HasMaxLength(200);
            Property(x => x.ERI).HasColumnName("ERI").IsOptional().HasMaxLength(200);
            Property(x => x.BuildingClass).HasColumnName("BUILDINGCLASS").IsOptional().HasMaxLength(200);
            Property(x => x.SafetyLevel).HasColumnName("SAFETYLEVEL").IsOptional().HasMaxLength(200);
            Property(x => x.FireLevel).HasColumnName("FIRELEVEL").IsOptional().HasMaxLength(200);
            Property(x => x.ExpirationDate).HasColumnName("EXPIRATIONDATE").IsOptional().HasMaxLength(200);
            Property(x => x.Investment).HasColumnName("INVESTMENT").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectInfoID).HasColumnName("PROJECTINFOID").IsOptional().HasMaxLength(200);
            Property(x => x.VersionNumber).HasColumnName("VERSIONNUMBER").IsOptional().HasMaxLength(200);
            Property(x => x.GroupID).HasColumnName("GROUPID").IsOptional().HasMaxLength(200);
            Property(x => x.EngineeringID).HasColumnName("ENGINEERINGID").IsOptional().HasMaxLength(200);
            Property(x => x.MarketProjectID).HasColumnName("MARKETPROJECTID").IsOptional().HasMaxLength(200);
            Property(x => x.RelateID).HasColumnName("RELATEID").IsOptional().HasMaxLength(200);
            Property(x => x.Leader).HasColumnName("LEADER").IsOptional().HasMaxLength(200);
            Property(x => x.LeaderName).HasColumnName("LEADERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ManageLevel).HasColumnName("MANAGELEVEL").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectSource).HasColumnName("PROJECTSOURCE").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectType).HasColumnName("PROJECTTYPE").IsOptional().HasMaxLength(200);
            Property(x => x.BuildType).HasColumnName("BUILDTYPE").IsOptional().HasMaxLength(200);
            Property(x => x.CustomerRequireDate).HasColumnName("CUSTOMERREQUIREDATE").IsOptional();
            Property(x => x.LandAreaName).HasColumnName("LANDAREANAME").IsOptional().HasMaxLength(200);
            Property(x => x.FloorAreaRatio).HasColumnName("FLOORAREARATIO").IsOptional().HasMaxLength(200);
            Property(x => x.DelegateDate).HasColumnName("DELEGATEDATE").IsOptional();
            Property(x => x.Register).HasColumnName("REGISTER").IsOptional().HasMaxLength(200);
            Property(x => x.RegisterName).HasColumnName("REGISTERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.TaskRequest).HasColumnName("TASKREQUEST").IsOptional().HasMaxLength(500);
            Property(x => x.DesignDocment).HasColumnName("DESIGNDOCMENT").IsOptional().HasMaxLength(500);
            Property(x => x.ProjectSpecialty).HasColumnName("PROJECTSPECIALTY").IsOptional().HasMaxLength(200);
            Property(x => x.CustomerRequestReview).HasColumnName("CUSTOMERREQUESTREVIEW").IsOptional().HasMaxLength(200);
            Property(x => x.CustomerRequestReviewName).HasColumnName("CUSTOMERREQUESTREVIEWNAME").IsOptional().HasMaxLength(200);
            Property(x => x.DeptHead).HasColumnName("DEPTHEAD").IsOptional();
            Property(x => x.BusinessPlanDept).HasColumnName("BUSINESSPLANDEPT").IsOptional();
            Property(x => x.ManagerSignature).HasColumnName("MANAGERSIGNATURE").IsOptional();
            Property(x => x.ChiefEngineer).HasColumnName("CHIEFENGINEER").IsOptional();
            Property(x => x.HospitalLeaders).HasColumnName("HOSPITALLEADERS").IsOptional();
            Property(x => x.CoopUnitID).HasColumnName("COOPUNITID").IsOptional();
            Property(x => x.CoopUnitIDName).HasColumnName("COOPUNITIDNAME").IsOptional();
            Property(x => x.DesignManager).HasColumnName("DESIGNMANAGER").IsOptional().HasMaxLength(200);
            Property(x => x.DesignManagerName).HasColumnName("DESIGNMANAGERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.LinkMan).HasColumnName("LINKMAN").IsOptional().HasMaxLength(200);
            Property(x => x.LinkPhone).HasColumnName("LINKPHONE").IsOptional().HasMaxLength(200);
            Property(x => x.DesignDept).HasColumnName("DESIGNDEPT").IsOptional().HasMaxLength(200);
            Property(x => x.DesignDeptName).HasColumnName("DESIGNDEPTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.QuanlityTarget).HasColumnName("QUANLITYTARGET").IsOptional().HasMaxLength(500);
            Property(x => x.ContractInfo).HasColumnName("CONTRACTINFO").IsOptional().HasMaxLength(200);
            Property(x => x.ContractInfoName).HasColumnName("CONTRACTINFONAME").IsOptional().HasMaxLength(200);
            Property(x => x.ContractValue).HasColumnName("CONTRACTVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.TmplCode).HasColumnName("TMPLCODE").IsOptional().HasMaxLength(200);
            Property(x => x.Workload).HasColumnName("WORKLOAD").IsOptional().HasPrecision(18,2);
            Property(x => x.WorkloadUnitPrice).HasColumnName("WORKLOADUNITPRICE").IsOptional().HasPrecision(18,2);
            Property(x => x.DesignBasis).HasColumnName("DESIGNBASIS").IsOptional();
            Property(x => x.Extention1).HasColumnName("EXTENTION1").IsOptional().HasMaxLength(500);
            Property(x => x.Extention2).HasColumnName("EXTENTION2").IsOptional().HasMaxLength(500);
            Property(x => x.Extention3).HasColumnName("EXTENTION3").IsOptional().HasMaxLength(500);
            Property(x => x.Extention4).HasColumnName("EXTENTION4").IsOptional().HasMaxLength(500);
            Property(x => x.Extention5).HasColumnName("EXTENTION5").IsOptional().HasMaxLength(500);
            Property(x => x.Extention6).HasColumnName("EXTENTION6").IsOptional().HasMaxLength(500);
            Property(x => x.Extention7).HasColumnName("EXTENTION7").IsOptional().HasMaxLength(500);
            Property(x => x.Extention8).HasColumnName("EXTENTION8").IsOptional().HasMaxLength(500);
            Property(x => x.Extention9).HasColumnName("EXTENTION9").IsOptional().HasMaxLength(500);
            Property(x => x.Extention10).HasColumnName("EXTENTION10").IsOptional().HasMaxLength(500);
            Property(x => x.Long).HasColumnName("LONG").IsOptional();
            Property(x => x.Lat).HasColumnName("LAT").IsOptional();
            Property(x => x.Address).HasColumnName("ADDRESS").IsOptional().HasMaxLength(200);
            Property(x => x.District).HasColumnName("DISTRICT").IsOptional().HasMaxLength(200);
            Property(x => x.MultiProjMode).HasColumnName("MULTIPROJMODE").IsOptional().HasMaxLength(200);
            Property(x => x.PhaseDetail).HasColumnName("PHASEDETAIL").IsOptional();
            Property(x => x.Area).HasColumnName("AREA").IsOptional().HasMaxLength(200);
            Property(x => x.TaxName).HasColumnName("TAXNAME").IsOptional().HasMaxLength(50);
            Property(x => x.TaxRate).HasColumnName("TAXRATE").IsOptional().HasPrecision(18,4);
        }
    }

    // T_CP_TaskNotice_PhaseDetail
    internal partial class T_CP_TaskNotice_PhaseDetailConfiguration : EntityTypeConfiguration<T_CP_TaskNotice_PhaseDetail>
    {
        public T_CP_TaskNotice_PhaseDetailConfiguration()
        {
			ToTable("T_CP_TASKNOTICE_PHASEDETAIL");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.T_CP_TaskNoticeID).HasColumnName("T_CP_TASKNOTICEID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.Phase).HasColumnName("PHASE").IsOptional().HasMaxLength(200);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(200);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(200);
            Property(x => x.ChargeDept).HasColumnName("CHARGEDEPT").IsOptional().HasMaxLength(200);
            Property(x => x.ChargeDeptName).HasColumnName("CHARGEDEPTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ChargeUser).HasColumnName("CHARGEUSER").IsOptional().HasMaxLength(200);
            Property(x => x.ChargeUserName).HasColumnName("CHARGEUSERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.OtherDept).HasColumnName("OTHERDEPT").IsOptional().HasMaxLength(200);
            Property(x => x.OtherDeptName).HasColumnName("OTHERDEPTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.PlanStartDate).HasColumnName("PLANSTARTDATE").IsOptional();
            Property(x => x.PlanFinishDate).HasColumnName("PLANFINISHDATE").IsOptional();
            Property(x => x.ProjectInfoID).HasColumnName("PROJECTINFOID").IsOptional().HasMaxLength(200);

            // Foreign keys
            HasOptional(a => a.T_CP_TaskNotice).WithMany(b => b.T_CP_TaskNotice_PhaseDetail).HasForeignKey(c => c.T_CP_TaskNoticeID); // FK_T_CP_TaskNotice_PhaseDetail_T_CP_TaskNotice
        }
    }

    // T_EXE_Audit
    internal partial class T_EXE_AuditConfiguration : EntityTypeConfiguration<T_EXE_Audit>
    {
        public T_EXE_AuditConfiguration()
        {
			ToTable("T_EXE_AUDIT");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.ModifyDate).HasColumnName("MODIFYDATE").IsOptional();
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUser).HasColumnName("MODIFYUSER").IsOptional().HasMaxLength(50);
            Property(x => x.OrgID).HasColumnName("ORGID").IsOptional().HasMaxLength(50);
            Property(x => x.CompanyID).HasColumnName("COMPANYID").IsOptional().HasMaxLength(50);
            Property(x => x.FlowPhase).HasColumnName("FLOWPHASE").IsOptional().HasMaxLength(50);
            Property(x => x.FlowInfo).HasColumnName("FLOWINFO").IsOptional().HasMaxLength(1073741823);
            Property(x => x.StepName).HasColumnName("STEPNAME").IsOptional().HasMaxLength(500);
            Property(x => x.ProjectInfoName).HasColumnName("PROJECTINFONAME").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectInfoCode).HasColumnName("PROJECTINFOCODE").IsOptional().HasMaxLength(200);
            Property(x => x.SubProjectName).HasColumnName("SUBPROJECTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.PhaseCode).HasColumnName("PHASECODE").IsOptional().HasMaxLength(200);
            Property(x => x.SerialNumber).HasColumnName("SERIALNUMBER").IsOptional().HasMaxLength(200);
            Property(x => x.MajorCode).HasColumnName("MAJORCODE").IsOptional().HasMaxLength(200);
            Property(x => x.Collactor).HasColumnName("COLLACTOR").IsOptional().HasMaxLength(200);
            Property(x => x.CollactorName).HasColumnName("COLLACTORNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Approver).HasColumnName("APPROVER").IsOptional().HasMaxLength(200);
            Property(x => x.ApproverName).HasColumnName("APPROVERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Auditor).HasColumnName("AUDITOR").IsOptional().HasMaxLength(200);
            Property(x => x.AuditorName).HasColumnName("AUDITORNAME").IsOptional().HasMaxLength(200);
            Property(x => x.FinishDate).HasColumnName("FINISHDATE").IsOptional();
            Property(x => x.DesignSign).HasColumnName("DESIGNSIGN").IsOptional();
            Property(x => x.CollactorSign).HasColumnName("COLLACTORSIGN").IsOptional();
            Property(x => x.AuditorSign).HasColumnName("AUDITORSIGN").IsOptional();
            Property(x => x.ApproverSign).HasColumnName("APPROVERSIGN").IsOptional();
            Property(x => x.ProductIDs).HasColumnName("PRODUCTIDS").IsOptional();
            Property(x => x.ProjectInfoID).HasColumnName("PROJECTINFOID").IsOptional().HasMaxLength(200);
            Property(x => x.WBSID).HasColumnName("WBSID").IsOptional().HasMaxLength(200);
            Property(x => x.Designer).HasColumnName("DESIGNER").IsOptional().HasMaxLength(200);
            Property(x => x.DesignerName).HasColumnName("DESIGNERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.VersionNumber).HasColumnName("VERSIONNUMBER").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectManager).HasColumnName("PROJECTMANAGER").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectManagerName).HasColumnName("PROJECTMANAGERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectManagerSign).HasColumnName("PROJECTMANAGERSIGN").IsOptional();
            Property(x => x.DesignManager).HasColumnName("DESIGNMANAGER").IsOptional().HasMaxLength(200);
            Property(x => x.DesignManagerName).HasColumnName("DESIGNMANAGERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Mapper).HasColumnName("MAPPER").IsOptional().HasMaxLength(200);
            Property(x => x.MapperName).HasColumnName("MAPPERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.MajorPrinciple).HasColumnName("MAJORPRINCIPLE").IsOptional().HasMaxLength(200);
            Property(x => x.MajorPrincipleName).HasColumnName("MAJORPRINCIPLENAME").IsOptional().HasMaxLength(200);
            Property(x => x.MajorEngineer).HasColumnName("MAJORENGINEER").IsOptional().HasMaxLength(200);
            Property(x => x.MajorEngineerName).HasColumnName("MAJORENGINEERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.DeptManager).HasColumnName("DEPTMANAGER").IsOptional().HasMaxLength(200);
            Property(x => x.DeptManagerName).HasColumnName("DEPTMANAGERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Products).HasColumnName("PRODUCTS").IsOptional().HasMaxLength(2000);
            Property(x => x.AdviceDetailH5).HasColumnName("ADVICEDETAILH5").IsOptional().HasMaxLength(2000);
            Property(x => x.MajorPrincipleSign).HasColumnName("MAJORPRINCIPLESIGN").IsOptional();
            Property(x => x.AuditSignSource).HasColumnName("AUDITSIGNSOURCE").IsOptional().HasMaxLength(200);
        }
    }

    // T_EXE_Audit_AdviceDetail
    internal partial class T_EXE_Audit_AdviceDetailConfiguration : EntityTypeConfiguration<T_EXE_Audit_AdviceDetail>
    {
        public T_EXE_Audit_AdviceDetailConfiguration()
        {
			ToTable("T_EXE_AUDIT_ADVICEDETAIL");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.T_EXE_AuditID).HasColumnName("T_EXE_AUDITID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.Step).HasColumnName("STEP").IsOptional().HasMaxLength(200);
            Property(x => x.SubmitUser).HasColumnName("SUBMITUSER").IsOptional().HasMaxLength(200);
            Property(x => x.SubmitUserName).HasColumnName("SUBMITUSERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ProductCode).HasColumnName("PRODUCTCODE").IsOptional().HasMaxLength(200);
            Property(x => x.MistakeType).HasColumnName("MISTAKETYPE").IsOptional().HasMaxLength(200);
            Property(x => x.MsitakeContent).HasColumnName("MSITAKECONTENT").IsOptional().HasMaxLength(2000);
            Property(x => x.ResponseContent).HasColumnName("RESPONSECONTENT").IsOptional().HasMaxLength(2000);
            Property(x => x.MistakeYear).HasColumnName("MISTAKEYEAR").IsOptional();
            Property(x => x.MistakeMonth).HasColumnName("MISTAKEMONTH").IsOptional();
            Property(x => x.MistakeSeason).HasColumnName("MISTAKESEASON").IsOptional();
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsOptional().HasMaxLength(200);
            Property(x => x.CreateUserName).HasColumnName("CREATEUSERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.LPosX).HasColumnName("LPOSX").IsOptional().HasPrecision(18,4);
            Property(x => x.LPosY).HasColumnName("LPOSY").IsOptional().HasPrecision(18,4);
            Property(x => x.RPosX).HasColumnName("RPOSX").IsOptional().HasPrecision(18,4);
            Property(x => x.RPosY).HasColumnName("RPOSY").IsOptional().HasPrecision(18,4);
            Property(x => x.RevisePosX).HasColumnName("REVISEPOSX").IsOptional().HasPrecision(18,4);
            Property(x => x.RevisePosY).HasColumnName("REVISEPOSY").IsOptional().HasPrecision(18,4);
            Property(x => x.ReviseWidth).HasColumnName("REVISEWIDTH").IsOptional().HasPrecision(18,4);
            Property(x => x.ReviseHeight).HasColumnName("REVISEHEIGHT").IsOptional().HasPrecision(18,4);
            Property(x => x.ReviseAudit).HasColumnName("REVISEAUDIT").IsOptional().HasMaxLength(200);
            Property(x => x.DrawingID).HasColumnName("DRAWINGID").IsOptional().HasMaxLength(200);
            Property(x => x.ReviseState).HasColumnName("REVISESTATE").IsOptional().HasMaxLength(200);
            Property(x => x.ReviseBasePoint).HasColumnName("REVISEBASEPOINT").IsOptional().HasMaxLength(200);
            Property(x => x.ReviseClass).HasColumnName("REVISECLASS").IsOptional().HasMaxLength(200);
            Property(x => x.AuditPathID).HasColumnName("AUDITPATHID").IsOptional().HasMaxLength(200);
            Property(x => x.ReviseType).HasColumnName("REVISETYPE").IsOptional().HasMaxLength(200);
            Property(x => x.UpdateTime).HasColumnName("UPDATETIME").IsOptional();
            Property(x => x.SubmitDept).HasColumnName("SUBMITDEPT").IsOptional().HasMaxLength(200);
            Property(x => x.SubmitDeptName).HasColumnName("SUBMITDEPTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.IsLead).HasColumnName("ISLEAD").IsOptional().HasMaxLength(200);
            Property(x => x.MarkType).HasColumnName("MARKTYPE").IsOptional().HasMaxLength(200);
            Property(x => x.OpinionState).HasColumnName("OPINIONSTATE").IsOptional().HasMaxLength(200);
            Property(x => x.EntityId).HasColumnName("ENTITYID").IsOptional().HasMaxLength(200);
            Property(x => x.ComeForm).HasColumnName("COMEFORM").IsOptional().HasMaxLength(200);
            Property(x => x.Sync).HasColumnName("SYNC").IsOptional().HasMaxLength(200);
            Property(x => x.DrawingName).HasColumnName("DRAWINGNAME").IsOptional().HasMaxLength(200);
            Property(x => x.DrawingCode).HasColumnName("DRAWINGCODE").IsOptional().HasMaxLength(200);
            Property(x => x.EntityInfoJosn).HasColumnName("ENTITYINFOJOSN").IsOptional();
            Property(x => x.CommentInfoJson).HasColumnName("COMMENTINFOJSON").IsOptional().HasMaxLength(1073741823);
            Property(x => x.CommentTempJson).HasColumnName("COMMENTTEMPJSON").IsOptional().HasMaxLength(1073741823);
            Property(x => x.OriginalPicID).HasColumnName("ORIGINALPICID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifiedPicID).HasColumnName("MODIFIEDPICID").IsOptional().HasMaxLength(50);
            Property(x => x.ResponseHistory).HasColumnName("RESPONSEHISTORY").IsOptional().HasMaxLength(50);

            // Foreign keys
            HasOptional(a => a.T_EXE_Audit).WithMany(b => b.T_EXE_Audit_AdviceDetail).HasForeignKey(c => c.T_EXE_AuditID); // FK_T_EXE_Audit_AdviceDetail_T_EXE_Audit
        }
    }

    // T_EXE_AuditReview
    internal partial class T_EXE_AuditReviewConfiguration : EntityTypeConfiguration<T_EXE_AuditReview>
    {
        public T_EXE_AuditReviewConfiguration()
        {
			ToTable("T_EXE_AUDITREVIEW");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.ModifyDate).HasColumnName("MODIFYDATE").IsOptional();
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUser).HasColumnName("MODIFYUSER").IsOptional().HasMaxLength(50);
            Property(x => x.OrgID).HasColumnName("ORGID").IsOptional().HasMaxLength(50);
            Property(x => x.CompanyID).HasColumnName("COMPANYID").IsOptional().HasMaxLength(50);
            Property(x => x.FlowPhase).HasColumnName("FLOWPHASE").IsOptional().HasMaxLength(50);
            Property(x => x.FlowInfo).HasColumnName("FLOWINFO").IsOptional().HasMaxLength(1073741823);
            Property(x => x.StepName).HasColumnName("STEPNAME").IsOptional().HasMaxLength(500);
            Property(x => x.ProjectCode).HasColumnName("PROJECTCODE").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectName).HasColumnName("PROJECTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.DesignPhase).HasColumnName("DESIGNPHASE").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectChief).HasColumnName("PROJECTCHIEF").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectChiefName).HasColumnName("PROJECTCHIEFNAME").IsOptional().HasMaxLength(200);
            Property(x => x.FilledDate).HasColumnName("FILLEDDATE").IsOptional();
            Property(x => x.ReviewSubject).HasColumnName("REVIEWSUBJECT").IsOptional().HasMaxLength(500);
            Property(x => x.ReviewComments).HasColumnName("REVIEWCOMMENTS").IsOptional().HasMaxLength(500);
            Property(x => x.AttendanceChart).HasColumnName("ATTENDANCECHART").IsOptional().HasMaxLength(500);
            Property(x => x.Recording).HasColumnName("RECORDING").IsOptional();
            Property(x => x.Confirmation).HasColumnName("CONFIRMATION").IsOptional();
            Property(x => x.Modification).HasColumnName("MODIFICATION").IsOptional().HasMaxLength(500);
            Property(x => x.TechnicalDirector).HasColumnName("TECHNICALDIRECTOR").IsOptional();
            Property(x => x.VerificationPerson).HasColumnName("VERIFICATIONPERSON").IsOptional();
            Property(x => x.ChangeConfirmation).HasColumnName("CHANGECONFIRMATION").IsOptional().HasMaxLength(500);
            Property(x => x.VerificationConfirmation).HasColumnName("VERIFICATIONCONFIRMATION").IsOptional();
            Property(x => x.Major).HasColumnName("MAJOR").IsOptional().HasMaxLength(200);
            Property(x => x.SpaceCode).HasColumnName("SPACECODE").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectInfoID).HasColumnName("PROJECTINFOID").IsOptional().HasMaxLength(200);
            Property(x => x.SerialNumber).HasColumnName("SERIALNUMBER").IsOptional().HasMaxLength(200);
            Property(x => x.VersionNumber).HasColumnName("VERSIONNUMBER").IsOptional().HasMaxLength(200);
            Property(x => x.MeetingRoom).HasColumnName("MEETINGROOM").IsOptional().HasMaxLength(200);
            Property(x => x.MeetingDate).HasColumnName("MEETINGDATE").IsOptional();
            Property(x => x.Attendence).HasColumnName("ATTENDENCE").IsOptional();
            Property(x => x.AttendenceName).HasColumnName("ATTENDENCENAME").IsOptional();
            Property(x => x.summarization).HasColumnName("SUMMARIZATION").IsOptional().HasMaxLength(500);
            Property(x => x.ProjectChiefSign).HasColumnName("PROJECTCHIEFSIGN").IsOptional();
            Property(x => x.EngineerSign).HasColumnName("ENGINEERSIGN").IsOptional();
            Property(x => x.XMJD).HasColumnName("XMJD").IsOptional().HasMaxLength(200);
            Property(x => x.JoinUser).HasColumnName("JOINUSER").IsOptional().HasMaxLength(500);
            Property(x => x.JoinUserName).HasColumnName("JOINUSERNAME").IsOptional().HasMaxLength(500);
        }
    }

    // T_EXE_ChangeAudit
    internal partial class T_EXE_ChangeAuditConfiguration : EntityTypeConfiguration<T_EXE_ChangeAudit>
    {
        public T_EXE_ChangeAuditConfiguration()
        {
			ToTable("T_EXE_CHANGEAUDIT");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.ModifyDate).HasColumnName("MODIFYDATE").IsOptional();
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUser).HasColumnName("MODIFYUSER").IsOptional().HasMaxLength(50);
            Property(x => x.OrgID).HasColumnName("ORGID").IsOptional().HasMaxLength(50);
            Property(x => x.CompanyID).HasColumnName("COMPANYID").IsOptional().HasMaxLength(50);
            Property(x => x.FlowPhase).HasColumnName("FLOWPHASE").IsOptional().HasMaxLength(50);
            Property(x => x.FlowInfo).HasColumnName("FLOWINFO").IsOptional().HasMaxLength(1073741823);
            Property(x => x.StepName).HasColumnName("STEPNAME").IsOptional().HasMaxLength(500);
            Property(x => x.ProjectInfoName).HasColumnName("PROJECTINFONAME").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectInfoCode).HasColumnName("PROJECTINFOCODE").IsOptional().HasMaxLength(200);
            Property(x => x.SubProjectName).HasColumnName("SUBPROJECTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.PhaseCode).HasColumnName("PHASECODE").IsOptional().HasMaxLength(200);
            Property(x => x.SerialNumber).HasColumnName("SERIALNUMBER").IsOptional().HasMaxLength(200);
            Property(x => x.MajorCode).HasColumnName("MAJORCODE").IsOptional().HasMaxLength(200);
            Property(x => x.Collactor).HasColumnName("COLLACTOR").IsOptional().HasMaxLength(200);
            Property(x => x.CollactorName).HasColumnName("COLLACTORNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Approver).HasColumnName("APPROVER").IsOptional().HasMaxLength(200);
            Property(x => x.ApproverName).HasColumnName("APPROVERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Auditor).HasColumnName("AUDITOR").IsOptional().HasMaxLength(200);
            Property(x => x.AuditorName).HasColumnName("AUDITORNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectManager).HasColumnName("PROJECTMANAGER").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectManagerName).HasColumnName("PROJECTMANAGERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.FinishDate).HasColumnName("FINISHDATE").IsOptional();
            Property(x => x.DesignSign).HasColumnName("DESIGNSIGN").IsOptional();
            Property(x => x.CollactorSign).HasColumnName("COLLACTORSIGN").IsOptional();
            Property(x => x.AuditorSign).HasColumnName("AUDITORSIGN").IsOptional();
            Property(x => x.ApproverSign).HasColumnName("APPROVERSIGN").IsOptional();
            Property(x => x.ProjectManagerSign).HasColumnName("PROJECTMANAGERSIGN").IsOptional();
            Property(x => x.ProjectInfoID).HasColumnName("PROJECTINFOID").IsOptional().HasMaxLength(200);
            Property(x => x.WBSID).HasColumnName("WBSID").IsOptional().HasMaxLength(200);
            Property(x => x.Designer).HasColumnName("DESIGNER").IsOptional().HasMaxLength(200);
            Property(x => x.DesignerName).HasColumnName("DESIGNERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.VersionNumber).HasColumnName("VERSIONNUMBER").IsOptional().HasMaxLength(200);
            Property(x => x.Mapper).HasColumnName("MAPPER").IsOptional().HasMaxLength(200);
            Property(x => x.MapperName).HasColumnName("MAPPERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.DesignManager).HasColumnName("DESIGNMANAGER").IsOptional().HasMaxLength(200);
            Property(x => x.DesignManagerName).HasColumnName("DESIGNMANAGERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.MajorPrinciple).HasColumnName("MAJORPRINCIPLE").IsOptional().HasMaxLength(200);
            Property(x => x.MajorPrincipleName).HasColumnName("MAJORPRINCIPLENAME").IsOptional().HasMaxLength(200);
            Property(x => x.MajorEngineer).HasColumnName("MAJORENGINEER").IsOptional().HasMaxLength(200);
            Property(x => x.MajorEngineerName).HasColumnName("MAJORENGINEERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.DeptManager).HasColumnName("DEPTMANAGER").IsOptional().HasMaxLength(200);
            Property(x => x.DeptManagerName).HasColumnName("DEPTMANAGERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.AuditSignSource).HasColumnName("AUDITSIGNSOURCE").IsOptional().HasMaxLength(200);
        }
    }

    // T_EXE_ChangeAudit_AdviceDetail
    internal partial class T_EXE_ChangeAudit_AdviceDetailConfiguration : EntityTypeConfiguration<T_EXE_ChangeAudit_AdviceDetail>
    {
        public T_EXE_ChangeAudit_AdviceDetailConfiguration()
        {
			ToTable("T_EXE_CHANGEAUDIT_ADVICEDETAIL");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.T_EXE_ChangeAuditID).HasColumnName("T_EXE_CHANGEAUDITID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.Step).HasColumnName("STEP").IsOptional().HasMaxLength(200);
            Property(x => x.SubmitUser).HasColumnName("SUBMITUSER").IsOptional().HasMaxLength(200);
            Property(x => x.SubmitUserName).HasColumnName("SUBMITUSERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ProductCode).HasColumnName("PRODUCTCODE").IsOptional().HasMaxLength(200);
            Property(x => x.MistakeType).HasColumnName("MISTAKETYPE").IsOptional().HasMaxLength(200);
            Property(x => x.MsitakeContent).HasColumnName("MSITAKECONTENT").IsOptional().HasMaxLength(2000);
            Property(x => x.ResponseContent).HasColumnName("RESPONSECONTENT").IsOptional().HasMaxLength(2000);
            Property(x => x.MistakeYear).HasColumnName("MISTAKEYEAR").IsOptional();
            Property(x => x.MistakeMonth).HasColumnName("MISTAKEMONTH").IsOptional();
            Property(x => x.MistakeSeason).HasColumnName("MISTAKESEASON").IsOptional();
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsOptional().HasMaxLength(200);
            Property(x => x.CreateUserName).HasColumnName("CREATEUSERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.LPosX).HasColumnName("LPOSX").IsOptional().HasPrecision(18,4);
            Property(x => x.LPosY).HasColumnName("LPOSY").IsOptional().HasPrecision(18,4);
            Property(x => x.RPosX).HasColumnName("RPOSX").IsOptional().HasPrecision(18,4);
            Property(x => x.RPosY).HasColumnName("RPOSY").IsOptional().HasPrecision(18,4);
            Property(x => x.RevisePosX).HasColumnName("REVISEPOSX").IsOptional().HasPrecision(18,4);
            Property(x => x.RevisePosY).HasColumnName("REVISEPOSY").IsOptional().HasPrecision(18,4);
            Property(x => x.ReviseWidth).HasColumnName("REVISEWIDTH").IsOptional().HasPrecision(18,4);
            Property(x => x.ReviseHeight).HasColumnName("REVISEHEIGHT").IsOptional().HasPrecision(18,4);
            Property(x => x.ReviseAudit).HasColumnName("REVISEAUDIT").IsOptional().HasMaxLength(200);
            Property(x => x.DrawingID).HasColumnName("DRAWINGID").IsOptional().HasMaxLength(200);
            Property(x => x.ReviseState).HasColumnName("REVISESTATE").IsOptional().HasMaxLength(200);
            Property(x => x.ReviseBasePoint).HasColumnName("REVISEBASEPOINT").IsOptional().HasMaxLength(200);
            Property(x => x.ReviseClass).HasColumnName("REVISECLASS").IsOptional().HasMaxLength(200);
            Property(x => x.AuditPathID).HasColumnName("AUDITPATHID").IsOptional().HasMaxLength(200);
            Property(x => x.ReviseType).HasColumnName("REVISETYPE").IsOptional().HasMaxLength(200);
            Property(x => x.UpdateTime).HasColumnName("UPDATETIME").IsOptional();
            Property(x => x.SubmitDept).HasColumnName("SUBMITDEPT").IsOptional().HasMaxLength(200);
            Property(x => x.SubmitDeptName).HasColumnName("SUBMITDEPTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.IsLead).HasColumnName("ISLEAD").IsOptional().HasMaxLength(200);
            Property(x => x.MarkType).HasColumnName("MARKTYPE").IsOptional().HasMaxLength(200);
            Property(x => x.OpinionState).HasColumnName("OPINIONSTATE").IsOptional().HasMaxLength(200);
            Property(x => x.EntityId).HasColumnName("ENTITYID").IsOptional().HasMaxLength(200);
            Property(x => x.ComeForm).HasColumnName("COMEFORM").IsOptional().HasMaxLength(200);
            Property(x => x.EntityInfoJosn).HasColumnName("ENTITYINFOJOSN").IsOptional();
            Property(x => x.DrawingName).HasColumnName("DRAWINGNAME").IsOptional().HasMaxLength(200);
            Property(x => x.DrawingCode).HasColumnName("DRAWINGCODE").IsOptional().HasMaxLength(200);
            Property(x => x.OriginalPicID).HasColumnName("ORIGINALPICID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifiedPicID).HasColumnName("MODIFIEDPICID").IsOptional().HasMaxLength(50);

            // Foreign keys
            HasOptional(a => a.T_EXE_ChangeAudit).WithMany(b => b.T_EXE_ChangeAudit_AdviceDetail).HasForeignKey(c => c.T_EXE_ChangeAuditID); // FK_T_EXE_ChangeAudit_AdviceDetail_T_EXE_ChangeAudit
        }
    }

    // T_EXE_DesignChangeApply
    internal partial class T_EXE_DesignChangeApplyConfiguration : EntityTypeConfiguration<T_EXE_DesignChangeApply>
    {
        public T_EXE_DesignChangeApplyConfiguration()
        {
			ToTable("T_EXE_DESIGNCHANGEAPPLY");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.ModifyDate).HasColumnName("MODIFYDATE").IsOptional();
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUser).HasColumnName("MODIFYUSER").IsOptional().HasMaxLength(50);
            Property(x => x.OrgID).HasColumnName("ORGID").IsOptional().HasMaxLength(50);
            Property(x => x.CompanyID).HasColumnName("COMPANYID").IsOptional().HasMaxLength(50);
            Property(x => x.FlowPhase).HasColumnName("FLOWPHASE").IsOptional().HasMaxLength(50);
            Property(x => x.FlowInfo).HasColumnName("FLOWINFO").IsOptional().HasMaxLength(1073741823);
            Property(x => x.StepName).HasColumnName("STEPNAME").IsOptional().HasMaxLength(500);
            Property(x => x.ProjectInfo).HasColumnName("PROJECTINFO").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectInfoName).HasColumnName("PROJECTINFONAME").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectCode).HasColumnName("PROJECTCODE").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectManager).HasColumnName("PROJECTMANAGER").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectManagerName).HasColumnName("PROJECTMANAGERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Phase).HasColumnName("PHASE").IsOptional().HasMaxLength(200);
            Property(x => x.MajorValue).HasColumnName("MAJORVALUE").IsOptional().HasMaxLength(200);
            Property(x => x.MajorName).HasColumnName("MAJORNAME").IsOptional().HasMaxLength(200);
            Property(x => x.MajorPrinciple).HasColumnName("MAJORPRINCIPLE").IsOptional().HasMaxLength(200);
            Property(x => x.MajorPrincipleName).HasColumnName("MAJORPRINCIPLENAME").IsOptional().HasMaxLength(200);
            Property(x => x.BelongDept).HasColumnName("BELONGDEPT").IsOptional().HasMaxLength(200);
            Property(x => x.BelongDeptName).HasColumnName("BELONGDEPTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.BelongDate).HasColumnName("BELONGDATE").IsOptional();
            Property(x => x.ProjectInfoID).HasColumnName("PROJECTINFOID").IsOptional().HasMaxLength(200);
            Property(x => x.TaskWork).HasColumnName("TASKWORK").IsOptional();
            Property(x => x.Remark).HasColumnName("REMARK").IsOptional().HasMaxLength(500);
            Property(x => x.ApprovePM).HasColumnName("APPROVEPM").IsOptional();
            Property(x => x.ApproveLeader).HasColumnName("APPROVELEADER").IsOptional();
            Property(x => x.TaskWorkID).HasColumnName("TASKWORKID").IsOptional().HasMaxLength(200);
            Property(x => x.TaskWorkName).HasColumnName("TASKWORKNAME").IsOptional().HasMaxLength(200);
            Property(x => x.TaskWorkCode).HasColumnName("TASKWORKCODE").IsOptional().HasMaxLength(200);
        }
    }

    // T_EXE_DesignChangeApply_TaskWork
    internal partial class T_EXE_DesignChangeApply_TaskWorkConfiguration : EntityTypeConfiguration<T_EXE_DesignChangeApply_TaskWork>
    {
        public T_EXE_DesignChangeApply_TaskWorkConfiguration()
        {
			ToTable("T_EXE_DESIGNCHANGEAPPLY_TASKWORK");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.T_EXE_DesignChangeApplyID).HasColumnName("T_EXE_DESIGNCHANGEAPPLYID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(200);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(200);
            Property(x => x.Major).HasColumnName("MAJOR").IsOptional().HasMaxLength(200);
            Property(x => x.Phase).HasColumnName("PHASE").IsOptional().HasMaxLength(200);
            Property(x => x.Workload).HasColumnName("WORKLOAD").IsOptional().HasPrecision(18,2);
            Property(x => x.PlanEndDate).HasColumnName("PLANENDDATE").IsOptional();
            Property(x => x.TaskWorkID).HasColumnName("TASKWORKID").IsOptional().HasMaxLength(200);
            Property(x => x.DossierName).HasColumnName("DOSSIERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.DossierCode).HasColumnName("DOSSIERCODE").IsOptional().HasMaxLength(200);

            // Foreign keys
            HasOptional(a => a.T_EXE_DesignChangeApply).WithMany(b => b.T_EXE_DesignChangeApply_TaskWork).HasForeignKey(c => c.T_EXE_DesignChangeApplyID); // FK_T_EXE_DesignChangeApply_TaskWork_T_EXE_DesignChangeApply
        }
    }

    // T_EXE_DesignChangeNotice
    internal partial class T_EXE_DesignChangeNoticeConfiguration : EntityTypeConfiguration<T_EXE_DesignChangeNotice>
    {
        public T_EXE_DesignChangeNoticeConfiguration()
        {
			ToTable("T_EXE_DESIGNCHANGENOTICE");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.ModifyDate).HasColumnName("MODIFYDATE").IsOptional();
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUser).HasColumnName("MODIFYUSER").IsOptional().HasMaxLength(50);
            Property(x => x.OrgID).HasColumnName("ORGID").IsOptional().HasMaxLength(50);
            Property(x => x.CompanyID).HasColumnName("COMPANYID").IsOptional().HasMaxLength(50);
            Property(x => x.FlowPhase).HasColumnName("FLOWPHASE").IsOptional().HasMaxLength(50);
            Property(x => x.FlowInfo).HasColumnName("FLOWINFO").IsOptional().HasMaxLength(1073741823);
            Property(x => x.StepName).HasColumnName("STEPNAME").IsOptional().HasMaxLength(500);
            Property(x => x.ProjectOwner).HasColumnName("PROJECTOWNER").IsOptional().HasMaxLength(200);
            Property(x => x.SerialNumber).HasColumnName("SERIALNUMBER").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectInfo).HasColumnName("PROJECTINFO").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectInfoName).HasColumnName("PROJECTINFONAME").IsOptional().HasMaxLength(200);
            Property(x => x.OriginalDesignChart).HasColumnName("ORIGINALDESIGNCHART").IsOptional().HasMaxLength(200);
            Property(x => x.SubProject).HasColumnName("SUBPROJECT").IsOptional().HasMaxLength(200);
            Property(x => x.SubProjectName).HasColumnName("SUBPROJECTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Major).HasColumnName("MAJOR").IsOptional().HasMaxLength(200);
            Property(x => x.MajorName).HasColumnName("MAJORNAME").IsOptional().HasMaxLength(200);
            Property(x => x.FigureNo).HasColumnName("FIGURENO").IsOptional().HasMaxLength(200);
            Property(x => x.ChangeSubject).HasColumnName("CHANGESUBJECT").IsOptional().HasMaxLength(500);
            Property(x => x.ChangeReason).HasColumnName("CHANGEREASON").IsOptional().HasMaxLength(500);
            Property(x => x.ChangeBasis).HasColumnName("CHANGEBASIS").IsOptional().HasMaxLength(500);
            Property(x => x.ChangeContentSummary).HasColumnName("CHANGECONTENTSUMMARY").IsOptional().HasMaxLength(500);
            Property(x => x.Attachments).HasColumnName("ATTACHMENTS").IsOptional().HasMaxLength(500);
            Property(x => x.Managers).HasColumnName("MANAGERS").IsOptional();
            Property(x => x.TechnicalDirectorComments).HasColumnName("TECHNICALDIRECTORCOMMENTS").IsOptional();
            Property(x => x.TechnicalDirectorsComments).HasColumnName("TECHNICALDIRECTORSCOMMENTS").IsOptional();
            Property(x => x.ProjectInfoID).HasColumnName("PROJECTINFOID").IsOptional().HasMaxLength(200);
            Property(x => x.VersionNumber).HasColumnName("VERSIONNUMBER").IsOptional().HasMaxLength(200);
        }
    }

    // T_EXE_MajorPutInfo
    internal partial class T_EXE_MajorPutInfoConfiguration : EntityTypeConfiguration<T_EXE_MajorPutInfo>
    {
        public T_EXE_MajorPutInfoConfiguration()
        {
			ToTable("T_EXE_MAJORPUTINFO");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.ModifyDate).HasColumnName("MODIFYDATE").IsOptional();
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUser).HasColumnName("MODIFYUSER").IsOptional().HasMaxLength(50);
            Property(x => x.OrgID).HasColumnName("ORGID").IsOptional().HasMaxLength(50);
            Property(x => x.CompanyID).HasColumnName("COMPANYID").IsOptional().HasMaxLength(50);
            Property(x => x.FlowPhase).HasColumnName("FLOWPHASE").IsOptional().HasMaxLength(50);
            Property(x => x.FlowInfo).HasColumnName("FLOWINFO").IsOptional().HasMaxLength(1073741823);
            Property(x => x.StepName).HasColumnName("STEPNAME").IsOptional().HasMaxLength(500);
            Property(x => x.ProjectInfoID).HasColumnName("PROJECTINFOID").IsOptional().HasMaxLength(50);
            Property(x => x.WBSID).HasColumnName("WBSID").IsOptional().HasMaxLength(500);
            Property(x => x.CoopPlanID).HasColumnName("COOPPLANID").IsOptional().HasMaxLength(500);
            Property(x => x.State).HasColumnName("STATE").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectName).HasColumnName("PROJECTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectCode).HasColumnName("PROJECTCODE").IsOptional().HasMaxLength(200);
            Property(x => x.OutMajorValue).HasColumnName("OUTMAJORVALUE").IsOptional().HasMaxLength(200);
            Property(x => x.InMajorValue).HasColumnName("INMAJORVALUE").IsOptional().HasMaxLength(200);
            Property(x => x.CooperationContent).HasColumnName("COOPERATIONCONTENT").IsOptional().HasMaxLength(500);
            Property(x => x.Register).HasColumnName("REGISTER").IsOptional().HasMaxLength(200);
            Property(x => x.RegisterName).HasColumnName("REGISTERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.TaskContent).HasColumnName("TASKCONTENT").IsOptional().HasMaxLength(500);
            Property(x => x.PictureNum).HasColumnName("PICTURENUM").IsOptional().HasMaxLength(200);
            Property(x => x.AnnexNum).HasColumnName("ANNEXNUM").IsOptional().HasMaxLength(200);
            Property(x => x.DelegateDate).HasColumnName("DELEGATEDATE").IsOptional();
            Property(x => x.RecieveDate).HasColumnName("RECIEVEDATE").IsOptional();
            Property(x => x.Annex).HasColumnName("ANNEX").IsOptional().HasMaxLength(500);
            Property(x => x.SubProjectName).HasColumnName("SUBPROJECTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Phase).HasColumnName("PHASE").IsOptional().HasMaxLength(200);
            Property(x => x.Receiver).HasColumnName("RECEIVER").IsOptional().HasMaxLength(500);
            Property(x => x.ReceiverName).HasColumnName("RECEIVERNAME").IsOptional().HasMaxLength(500);
            Property(x => x.Collactor).HasColumnName("COLLACTOR").IsOptional().HasMaxLength(200);
            Property(x => x.Auditor).HasColumnName("AUDITOR").IsOptional().HasMaxLength(200);
            Property(x => x.Checker).HasColumnName("CHECKER").IsOptional().HasMaxLength(200);
            Property(x => x.MajorPrinciple).HasColumnName("MAJORPRINCIPLE").IsOptional().HasMaxLength(200);
            Property(x => x.VersionNumber).HasColumnName("VERSIONNUMBER").IsOptional().HasMaxLength(200);
            Property(x => x.SerialNumber).HasColumnName("SERIALNUMBER").IsOptional().HasMaxLength(200);
            Property(x => x.PassAuditState).HasColumnName("PASSAUDITSTATE").IsOptional().HasMaxLength(200);
            Property(x => x.CoopPlanIDName).HasColumnName("COOPPLANIDNAME").IsOptional().HasMaxLength(500);
            Property(x => x.RelateWBSID).HasColumnName("RELATEWBSID").IsOptional().HasMaxLength(200);
        }
    }

    // T_EXE_MajorPutInfo_AdviceDetail
    internal partial class T_EXE_MajorPutInfo_AdviceDetailConfiguration : EntityTypeConfiguration<T_EXE_MajorPutInfo_AdviceDetail>
    {
        public T_EXE_MajorPutInfo_AdviceDetailConfiguration()
        {
			ToTable("T_EXE_MAJORPUTINFO_ADVICEDETAIL");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.T_EXE_MajorPutInfoID).HasColumnName("T_EXE_MAJORPUTINFOID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.SubmitUser).HasColumnName("SUBMITUSER").IsOptional().HasMaxLength(200);
            Property(x => x.SubmitUserName).HasColumnName("SUBMITUSERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ProductCode).HasColumnName("PRODUCTCODE").IsOptional().HasMaxLength(200);
            Property(x => x.DrawingName).HasColumnName("DRAWINGNAME").IsOptional().HasMaxLength(200);
            Property(x => x.DrawingCode).HasColumnName("DRAWINGCODE").IsOptional().HasMaxLength(200);
            Property(x => x.MistakeType).HasColumnName("MISTAKETYPE").IsOptional().HasMaxLength(200);
            Property(x => x.MsitakeContent).HasColumnName("MSITAKECONTENT").IsOptional().HasMaxLength(2000);
            Property(x => x.ResponseContent).HasColumnName("RESPONSECONTENT").IsOptional().HasMaxLength(2000);
            Property(x => x.MistakeYear).HasColumnName("MISTAKEYEAR").IsOptional();
            Property(x => x.MistakeMonth).HasColumnName("MISTAKEMONTH").IsOptional();
            Property(x => x.MistakeSeason).HasColumnName("MISTAKESEASON").IsOptional();
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsOptional().HasMaxLength(200);
            Property(x => x.CreateUserName).HasColumnName("CREATEUSERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.LPosX).HasColumnName("LPOSX").IsOptional().HasPrecision(18,4);
            Property(x => x.LPosY).HasColumnName("LPOSY").IsOptional().HasPrecision(18,4);
            Property(x => x.RPosX).HasColumnName("RPOSX").IsOptional().HasPrecision(18,4);
            Property(x => x.RPosY).HasColumnName("RPOSY").IsOptional().HasPrecision(18,4);
            Property(x => x.RevisePosX).HasColumnName("REVISEPOSX").IsOptional().HasPrecision(18,4);
            Property(x => x.RevisePosY).HasColumnName("REVISEPOSY").IsOptional().HasPrecision(18,4);
            Property(x => x.ReviseWidth).HasColumnName("REVISEWIDTH").IsOptional().HasPrecision(18,4);
            Property(x => x.ReviseHeight).HasColumnName("REVISEHEIGHT").IsOptional().HasPrecision(18,4);
            Property(x => x.ReviseAudit).HasColumnName("REVISEAUDIT").IsOptional().HasMaxLength(200);
            Property(x => x.DrawingID).HasColumnName("DRAWINGID").IsOptional().HasMaxLength(200);
            Property(x => x.ReviseState).HasColumnName("REVISESTATE").IsOptional().HasMaxLength(200);
            Property(x => x.ReviseBasePoint).HasColumnName("REVISEBASEPOINT").IsOptional().HasMaxLength(200);
            Property(x => x.ReviseClass).HasColumnName("REVISECLASS").IsOptional().HasMaxLength(200);
            Property(x => x.AuditPathID).HasColumnName("AUDITPATHID").IsOptional().HasMaxLength(200);
            Property(x => x.ReviseType).HasColumnName("REVISETYPE").IsOptional().HasMaxLength(200);
            Property(x => x.UpdateTime).HasColumnName("UPDATETIME").IsOptional();
            Property(x => x.SubmitDept).HasColumnName("SUBMITDEPT").IsOptional().HasMaxLength(200);
            Property(x => x.SubmitDeptName).HasColumnName("SUBMITDEPTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.IsLead).HasColumnName("ISLEAD").IsOptional().HasMaxLength(200);
            Property(x => x.MarkType).HasColumnName("MARKTYPE").IsOptional().HasMaxLength(200);
            Property(x => x.OpinionState).HasColumnName("OPINIONSTATE").IsOptional().HasMaxLength(200);
            Property(x => x.EntityId).HasColumnName("ENTITYID").IsOptional().HasMaxLength(200);
            Property(x => x.ComeForm).HasColumnName("COMEFORM").IsOptional().HasMaxLength(200);
            Property(x => x.EntityInfoJosn).HasColumnName("ENTITYINFOJOSN").IsOptional();
            Property(x => x.OriginalPicID).HasColumnName("ORIGINALPICID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifiedPicID).HasColumnName("MODIFIEDPICID").IsOptional().HasMaxLength(50);

            // Foreign keys
            HasOptional(a => a.T_EXE_MajorPutInfo).WithMany(b => b.T_EXE_MajorPutInfo_AdviceDetail).HasForeignKey(c => c.T_EXE_MajorPutInfoID); // FK_T_EXE_MajorPutInfo_AdviceDetail_T_EXE_MajorPutInfo
        }
    }

    // T_EXE_MajorPutInfo_FetchDrawingInfo
    internal partial class T_EXE_MajorPutInfo_FetchDrawingInfoConfiguration : EntityTypeConfiguration<T_EXE_MajorPutInfo_FetchDrawingInfo>
    {
        public T_EXE_MajorPutInfo_FetchDrawingInfoConfiguration()
        {
			ToTable("T_EXE_MAJORPUTINFO_FETCHDRAWINGINFO");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.T_EXE_MajorPutInfoID).HasColumnName("T_EXE_MAJORPUTINFOID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.FileName).HasColumnName("FILENAME").IsOptional().HasMaxLength(200);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(200);
            Property(x => x.Type).HasColumnName("TYPE").IsOptional().HasMaxLength(200);
            Property(x => x.MainFile).HasColumnName("MAINFILE").IsOptional().HasMaxLength(200);
            Property(x => x.MainFileName).HasColumnName("MAINFILENAME").IsOptional().HasMaxLength(200);
            Property(x => x.Source).HasColumnName("SOURCE").IsOptional().HasMaxLength(200);
            Property(x => x.ShareFileID).HasColumnName("SHAREFILEID").IsOptional().HasMaxLength(200);
            Property(x => x.SourceType).HasColumnName("SOURCETYPE").IsOptional().HasMaxLength(200);
            Property(x => x.DrawingSource).HasColumnName("DRAWINGSOURCE").IsOptional().HasMaxLength(200);
            Property(x => x.DrawingID).HasColumnName("DRAWINGID").IsOptional().HasMaxLength(200);

            // Foreign keys
            HasOptional(a => a.T_EXE_MajorPutInfo).WithMany(b => b.T_EXE_MajorPutInfo_FetchDrawingInfo).HasForeignKey(c => c.T_EXE_MajorPutInfoID); // FK_T_EXE_MajorPutInfo_FetchDrawingInfo_T_EXE_MajorPutInfo
        }
    }

    // T_EXE_ManageWorkloadSettlement
    internal partial class T_EXE_ManageWorkloadSettlementConfiguration : EntityTypeConfiguration<T_EXE_ManageWorkloadSettlement>
    {
        public T_EXE_ManageWorkloadSettlementConfiguration()
        {
			ToTable("T_EXE_MANAGEWORKLOADSETTLEMENT");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.ModifyDate).HasColumnName("MODIFYDATE").IsOptional();
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUser).HasColumnName("MODIFYUSER").IsOptional().HasMaxLength(50);
            Property(x => x.OrgID).HasColumnName("ORGID").IsOptional().HasMaxLength(50);
            Property(x => x.CompanyID).HasColumnName("COMPANYID").IsOptional().HasMaxLength(50);
            Property(x => x.FlowPhase).HasColumnName("FLOWPHASE").IsOptional().HasMaxLength(50);
            Property(x => x.FlowInfo).HasColumnName("FLOWINFO").IsOptional().HasMaxLength(1073741823);
            Property(x => x.StepName).HasColumnName("STEPNAME").IsOptional().HasMaxLength(500);
            Property(x => x.ProjectInfo).HasColumnName("PROJECTINFO").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectInfoName).HasColumnName("PROJECTINFONAME").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectCode).HasColumnName("PROJECTCODE").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectManager).HasColumnName("PROJECTMANAGER").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectManagerName).HasColumnName("PROJECTMANAGERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Phase).HasColumnName("PHASE").IsOptional().HasMaxLength(200);
            Property(x => x.BelongDept).HasColumnName("BELONGDEPT").IsOptional().HasMaxLength(200);
            Property(x => x.BelongDeptName).HasColumnName("BELONGDEPTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.BelongDate).HasColumnName("BELONGDATE").IsOptional();
            Property(x => x.Remark).HasColumnName("REMARK").IsOptional().HasMaxLength(500);
            Property(x => x.ProjectManagerApprove).HasColumnName("PROJECTMANAGERAPPROVE").IsOptional();
            Property(x => x.SummaySettlement).HasColumnName("SUMMAYSETTLEMENT").IsOptional().HasPrecision(18,2);
            Property(x => x.ManageWorkloadList).HasColumnName("MANAGEWORKLOADLIST").IsOptional();
        }
    }

    // T_EXE_ManageWorkloadSettlement_ManageWorkloadList
    internal partial class T_EXE_ManageWorkloadSettlement_ManageWorkloadListConfiguration : EntityTypeConfiguration<T_EXE_ManageWorkloadSettlement_ManageWorkloadList>
    {
        public T_EXE_ManageWorkloadSettlement_ManageWorkloadListConfiguration()
        {
			ToTable("T_EXE_MANAGEWORKLOADSETTLEMENT_MANAGEWORKLOADLIST");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.T_EXE_ManageWorkloadSettlementID).HasColumnName("T_EXE_MANAGEWORKLOADSETTLEMENTID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(200);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(200);
            Property(x => x.Workload).HasColumnName("WORKLOAD").IsOptional().HasPrecision(18,2);
            Property(x => x.SummarySettlement).HasColumnName("SUMMARYSETTLEMENT").IsOptional().HasPrecision(18,2);
            Property(x => x.Settlement).HasColumnName("SETTLEMENT").IsOptional().HasPrecision(18,2);
            Property(x => x.DetailText).HasColumnName("DETAILTEXT").IsOptional().HasMaxLength(2000);
            Property(x => x.DetailJson).HasColumnName("DETAILJSON").IsOptional().HasMaxLength(1073741823);
            Property(x => x.CBSID).HasColumnName("CBSID").IsOptional().HasMaxLength(200);
            Property(x => x.DetailTextHistory).HasColumnName("DETAILTEXTHISTORY").IsOptional().HasMaxLength(2000);

            // Foreign keys
            HasOptional(a => a.T_EXE_ManageWorkloadSettlement).WithMany(b => b.T_EXE_ManageWorkloadSettlement_ManageWorkloadList).HasForeignKey(c => c.T_EXE_ManageWorkloadSettlementID); // FK_T_EXE_ManageWorkloadSettlement_ManageWorkloadList_T_EXE_ManageWorkloadSettlement
        }
    }

    // T_EXE_MettingSign
    internal partial class T_EXE_MettingSignConfiguration : EntityTypeConfiguration<T_EXE_MettingSign>
    {
        public T_EXE_MettingSignConfiguration()
        {
			ToTable("T_EXE_METTINGSIGN");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.ModifyDate).HasColumnName("MODIFYDATE").IsOptional();
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUser).HasColumnName("MODIFYUSER").IsOptional().HasMaxLength(50);
            Property(x => x.OrgID).HasColumnName("ORGID").IsOptional().HasMaxLength(50);
            Property(x => x.CompanyID).HasColumnName("COMPANYID").IsOptional().HasMaxLength(50);
            Property(x => x.FlowPhase).HasColumnName("FLOWPHASE").IsOptional().HasMaxLength(50);
            Property(x => x.FlowInfo).HasColumnName("FLOWINFO").IsOptional().HasMaxLength(1073741823);
            Property(x => x.StepName).HasColumnName("STEPNAME").IsOptional().HasMaxLength(500);
            Property(x => x.ProjectInfoName).HasColumnName("PROJECTINFONAME").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectInfoCode).HasColumnName("PROJECTINFOCODE").IsOptional().HasMaxLength(200);
            Property(x => x.Phase).HasColumnName("PHASE").IsOptional().HasMaxLength(200);
            Property(x => x.SubmitUser).HasColumnName("SUBMITUSER").IsOptional().HasMaxLength(200);
            Property(x => x.SubmitUserName).HasColumnName("SUBMITUSERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Major).HasColumnName("MAJOR").IsOptional().HasMaxLength(200);
            Property(x => x.MajorName).HasColumnName("MAJORNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ReceiveUser).HasColumnName("RECEIVEUSER").IsOptional().HasMaxLength(200);
            Property(x => x.ReceiveUserName).HasColumnName("RECEIVEUSERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.FH).HasColumnName("FH").IsOptional();
            Property(x => x.SH).HasColumnName("SH").IsOptional();
            Property(x => x.ZYSD).HasColumnName("ZYSD").IsOptional();
            Property(x => x.ProjectInfoID).HasColumnName("PROJECTINFOID").IsOptional().HasMaxLength(50);
            Property(x => x.Collactor).HasColumnName("COLLACTOR").IsOptional().HasMaxLength(200);
            Property(x => x.CollactorName).HasColumnName("COLLACTORNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Auditor).HasColumnName("AUDITOR").IsOptional().HasMaxLength(200);
            Property(x => x.AuditorName).HasColumnName("AUDITORNAME").IsOptional().HasMaxLength(200);
            Property(x => x.MajorApprover).HasColumnName("MAJORAPPROVER").IsOptional().HasMaxLength(200);
            Property(x => x.MajorApproverName).HasColumnName("MAJORAPPROVERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectUserlist).HasColumnName("PROJECTUSERLIST").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectUserlistName).HasColumnName("PROJECTUSERLISTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Approver).HasColumnName("APPROVER").IsOptional().HasMaxLength(200);
            Property(x => x.ApproverName).HasColumnName("APPROVERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.SerialNumber).HasColumnName("SERIALNUMBER").IsOptional().HasMaxLength(200);
            Property(x => x.SubProject).HasColumnName("SUBPROJECT").IsOptional().HasMaxLength(200);
            Property(x => x.SubProjectName).HasColumnName("SUBPROJECTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Dept).HasColumnName("DEPT").IsOptional().HasMaxLength(200);
            Property(x => x.DeptName).HasColumnName("DEPTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.SignMajor).HasColumnName("SIGNMAJOR").IsOptional().HasMaxLength(200);
            Property(x => x.SignName).HasColumnName("SIGNNAME").IsOptional().HasMaxLength(200);
            Property(x => x.SignID).HasColumnName("SIGNID").IsOptional().HasMaxLength(200);
            Property(x => x.MettingUserName).HasColumnName("METTINGUSERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.MettingUser).HasColumnName("METTINGUSER").IsOptional().HasMaxLength(200);
            Property(x => x.WBSID).HasColumnName("WBSID").IsOptional().HasMaxLength(200);
            Property(x => x.VersionNumber).HasColumnName("VERSIONNUMBER").IsOptional().HasMaxLength(200);
        }
    }

    // T_EXE_MettingSign_AdviceDetail
    internal partial class T_EXE_MettingSign_AdviceDetailConfiguration : EntityTypeConfiguration<T_EXE_MettingSign_AdviceDetail>
    {
        public T_EXE_MettingSign_AdviceDetailConfiguration()
        {
			ToTable("T_EXE_METTINGSIGN_ADVICEDETAIL");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.T_EXE_MettingSignID).HasColumnName("T_EXE_METTINGSIGNID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.SubmitUser).HasColumnName("SUBMITUSER").IsOptional().HasMaxLength(200);
            Property(x => x.SubmitUserName).HasColumnName("SUBMITUSERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ProductCode).HasColumnName("PRODUCTCODE").IsOptional().HasMaxLength(200);
            Property(x => x.DrawingName).HasColumnName("DRAWINGNAME").IsOptional().HasMaxLength(200);
            Property(x => x.DrawingCode).HasColumnName("DRAWINGCODE").IsOptional().HasMaxLength(200);
            Property(x => x.MistakeType).HasColumnName("MISTAKETYPE").IsOptional().HasMaxLength(200);
            Property(x => x.MsitakeContent).HasColumnName("MSITAKECONTENT").IsOptional().HasMaxLength(2000);
            Property(x => x.ResponseContent).HasColumnName("RESPONSECONTENT").IsOptional().HasMaxLength(2000);
            Property(x => x.MistakeYear).HasColumnName("MISTAKEYEAR").IsOptional();
            Property(x => x.MistakeMonth).HasColumnName("MISTAKEMONTH").IsOptional();
            Property(x => x.MistakeSeason).HasColumnName("MISTAKESEASON").IsOptional();
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsOptional().HasMaxLength(200);
            Property(x => x.CreateUserName).HasColumnName("CREATEUSERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.LPosX).HasColumnName("LPOSX").IsOptional().HasPrecision(18,4);
            Property(x => x.LPosY).HasColumnName("LPOSY").IsOptional().HasPrecision(18,4);
            Property(x => x.RPosX).HasColumnName("RPOSX").IsOptional().HasPrecision(18,4);
            Property(x => x.RPosY).HasColumnName("RPOSY").IsOptional().HasPrecision(18,4);
            Property(x => x.RevisePosX).HasColumnName("REVISEPOSX").IsOptional().HasPrecision(18,4);
            Property(x => x.RevisePosY).HasColumnName("REVISEPOSY").IsOptional().HasPrecision(18,4);
            Property(x => x.ReviseWidth).HasColumnName("REVISEWIDTH").IsOptional().HasPrecision(18,4);
            Property(x => x.ReviseHeight).HasColumnName("REVISEHEIGHT").IsOptional().HasPrecision(18,4);
            Property(x => x.ReviseAudit).HasColumnName("REVISEAUDIT").IsOptional().HasMaxLength(200);
            Property(x => x.DrawingID).HasColumnName("DRAWINGID").IsOptional().HasMaxLength(200);
            Property(x => x.ReviseState).HasColumnName("REVISESTATE").IsOptional().HasMaxLength(200);
            Property(x => x.ReviseBasePoint).HasColumnName("REVISEBASEPOINT").IsOptional().HasMaxLength(200);
            Property(x => x.ReviseClass).HasColumnName("REVISECLASS").IsOptional().HasMaxLength(200);
            Property(x => x.AuditPathID).HasColumnName("AUDITPATHID").IsOptional().HasMaxLength(200);
            Property(x => x.ReviseType).HasColumnName("REVISETYPE").IsOptional().HasMaxLength(200);
            Property(x => x.UpdateTime).HasColumnName("UPDATETIME").IsOptional();
            Property(x => x.SubmitDept).HasColumnName("SUBMITDEPT").IsOptional().HasMaxLength(200);
            Property(x => x.SubmitDeptName).HasColumnName("SUBMITDEPTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.IsLead).HasColumnName("ISLEAD").IsOptional().HasMaxLength(200);
            Property(x => x.MarkType).HasColumnName("MARKTYPE").IsOptional().HasMaxLength(200);
            Property(x => x.OpinionState).HasColumnName("OPINIONSTATE").IsOptional().HasMaxLength(200);
            Property(x => x.EntityId).HasColumnName("ENTITYID").IsOptional().HasMaxLength(200);
            Property(x => x.ComeForm).HasColumnName("COMEFORM").IsOptional().HasMaxLength(200);
            Property(x => x.EntityInfoJosn).HasColumnName("ENTITYINFOJOSN").IsOptional();
            Property(x => x.OriginalPicID).HasColumnName("ORIGINALPICID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifiedPicID).HasColumnName("MODIFIEDPICID").IsOptional().HasMaxLength(50);

            // Foreign keys
            HasOptional(a => a.T_EXE_MettingSign).WithMany(b => b.T_EXE_MettingSign_AdviceDetail).HasForeignKey(c => c.T_EXE_MettingSignID); // FK_T_EXE_MettingSign_AdviceDetail_T_EXE_MettingSign
        }
    }

    // T_EXE_MettingSign_ProjectGroupMembers
    internal partial class T_EXE_MettingSign_ProjectGroupMembersConfiguration : EntityTypeConfiguration<T_EXE_MettingSign_ProjectGroupMembers>
    {
        public T_EXE_MettingSign_ProjectGroupMembersConfiguration()
        {
			ToTable("T_EXE_METTINGSIGN_PROJECTGROUPMEMBERS");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.T_EXE_MettingSignID).HasColumnName("T_EXE_METTINGSIGNID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.Major).HasColumnName("MAJOR").IsOptional().HasMaxLength(200);
            Property(x => x.MettingUser).HasColumnName("METTINGUSER").IsOptional().HasMaxLength(200);
            Property(x => x.MettingUserName).HasColumnName("METTINGUSERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.SignDate).HasColumnName("SIGNDATE").IsOptional();
            Property(x => x.SignComment).HasColumnName("SIGNCOMMENT").IsOptional().HasMaxLength(2000);
            Property(x => x.MajorCode).HasColumnName("MAJORCODE").IsOptional().HasMaxLength(200);
            Property(x => x.DesignerByMajor).HasColumnName("DESIGNERBYMAJOR").IsOptional().HasMaxLength(200);

            // Foreign keys
            HasOptional(a => a.T_EXE_MettingSign).WithMany(b => b.T_EXE_MettingSign_ProjectGroupMembers).HasForeignKey(c => c.T_EXE_MettingSignID); // FK_T_EXE_MettingSign_ProjectGroupMembers_T_EXE_MettingSign
        }
    }

    // T_EXE_MettingSign_ResultList
    internal partial class T_EXE_MettingSign_ResultListConfiguration : EntityTypeConfiguration<T_EXE_MettingSign_ResultList>
    {
        public T_EXE_MettingSign_ResultListConfiguration()
        {
			ToTable("T_EXE_METTINGSIGN_RESULTLIST");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.T_EXE_MettingSignID).HasColumnName("T_EXE_METTINGSIGNID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.ProductID).HasColumnName("PRODUCTID").IsOptional().HasMaxLength(200);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(200);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(200);
            Property(x => x.Major).HasColumnName("MAJOR").IsOptional().HasMaxLength(200);
            Property(x => x.MajorValue).HasColumnName("MAJORVALUE").IsOptional().HasMaxLength(200);
            Property(x => x.FileType).HasColumnName("FILETYPE").IsOptional().HasMaxLength(200);
            Property(x => x.MainFile).HasColumnName("MAINFILE").IsOptional().HasMaxLength(200);
            Property(x => x.ProductVersion).HasColumnName("PRODUCTVERSION").IsOptional().HasPrecision(18,2);

            // Foreign keys
            HasOptional(a => a.T_EXE_MettingSign).WithMany(b => b.T_EXE_MettingSign_ResultList).HasForeignKey(c => c.T_EXE_MettingSignID); // FK_T_EXE_MettingSign_ResultList_T_EXE_MettingSign
        }
    }

    // T_EXE_ProductPlotDemo
    internal partial class T_EXE_ProductPlotDemoConfiguration : EntityTypeConfiguration<T_EXE_ProductPlotDemo>
    {
        public T_EXE_ProductPlotDemoConfiguration()
        {
			ToTable("T_EXE_PRODUCTPLOTDEMO");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.ModifyDate).HasColumnName("MODIFYDATE").IsOptional();
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUser).HasColumnName("MODIFYUSER").IsOptional().HasMaxLength(50);
            Property(x => x.OrgID).HasColumnName("ORGID").IsOptional().HasMaxLength(50);
            Property(x => x.CompanyID).HasColumnName("COMPANYID").IsOptional().HasMaxLength(50);
            Property(x => x.FlowPhase).HasColumnName("FLOWPHASE").IsOptional().HasMaxLength(50);
            Property(x => x.FlowInfo).HasColumnName("FLOWINFO").IsOptional().HasMaxLength(1073741823);
            Property(x => x.StepName).HasColumnName("STEPNAME").IsOptional().HasMaxLength(500);
            Property(x => x.ApplyUser).HasColumnName("APPLYUSER").IsOptional().HasMaxLength(200);
            Property(x => x.ApplyUserName).HasColumnName("APPLYUSERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ApplyDate).HasColumnName("APPLYDATE").IsOptional();
            Property(x => x.ProductDetail).HasColumnName("PRODUCTDETAIL").IsOptional();
            Property(x => x.ProjectInfoName).HasColumnName("PROJECTINFONAME").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectInfoCode).HasColumnName("PROJECTINFOCODE").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectInfoID).HasColumnName("PROJECTINFOID").IsOptional().HasMaxLength(200);
        }
    }

    // T_EXE_ProductPlotDemo_ProductDetail
    internal partial class T_EXE_ProductPlotDemo_ProductDetailConfiguration : EntityTypeConfiguration<T_EXE_ProductPlotDemo_ProductDetail>
    {
        public T_EXE_ProductPlotDemo_ProductDetailConfiguration()
        {
			ToTable("T_EXE_PRODUCTPLOTDEMO_PRODUCTDETAIL");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.T_EXE_ProductPlotDemoID).HasColumnName("T_EXE_PRODUCTPLOTDEMOID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.ProductID).HasColumnName("PRODUCTID").IsOptional().HasMaxLength(200);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(200);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(200);
            Property(x => x.Designer).HasColumnName("DESIGNER").IsOptional().HasMaxLength(200);
            Property(x => x.SubmitDate).HasColumnName("SUBMITDATE").IsOptional();
            Property(x => x.DesignerName).HasColumnName("DESIGNERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.PdfFile).HasColumnName("PDFFILE").IsOptional().HasMaxLength(200);
            Property(x => x.SignPdfFile).HasColumnName("SIGNPDFFILE").IsOptional().HasMaxLength(200);

            // Foreign keys
            HasOptional(a => a.T_EXE_ProductPlotDemo).WithMany(b => b.T_EXE_ProductPlotDemo_ProductDetail).HasForeignKey(c => c.T_EXE_ProductPlotDemoID); // FK_T_EXE_ProductPlotDemo_ProductDetail_T_EXE_ProductPlotDemo
        }
    }

    // T_EXE_ProductReview
    internal partial class T_EXE_ProductReviewConfiguration : EntityTypeConfiguration<T_EXE_ProductReview>
    {
        public T_EXE_ProductReviewConfiguration()
        {
			ToTable("T_EXE_PRODUCTREVIEW");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.ModifyDate).HasColumnName("MODIFYDATE").IsOptional();
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUser).HasColumnName("MODIFYUSER").IsOptional().HasMaxLength(50);
            Property(x => x.OrgID).HasColumnName("ORGID").IsOptional().HasMaxLength(50);
            Property(x => x.CompanyID).HasColumnName("COMPANYID").IsOptional().HasMaxLength(50);
            Property(x => x.FlowPhase).HasColumnName("FLOWPHASE").IsOptional().HasMaxLength(50);
            Property(x => x.FlowInfo).HasColumnName("FLOWINFO").IsOptional().HasMaxLength(1073741823);
            Property(x => x.StepName).HasColumnName("STEPNAME").IsOptional().HasMaxLength(500);
            Property(x => x.ProjectInfoName).HasColumnName("PROJECTINFONAME").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectInfoCode).HasColumnName("PROJECTINFOCODE").IsOptional().HasMaxLength(200);
            Property(x => x.Phase).HasColumnName("PHASE").IsOptional().HasMaxLength(200);
            Property(x => x.SubmitUser).HasColumnName("SUBMITUSER").IsOptional().HasMaxLength(200);
            Property(x => x.SubmitUserName).HasColumnName("SUBMITUSERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.MajorDirection).HasColumnName("MAJORDIRECTION").IsOptional().HasMaxLength(200);
            Property(x => x.ReceiveUser).HasColumnName("RECEIVEUSER").IsOptional().HasMaxLength(200);
            Property(x => x.ReceiveUserName).HasColumnName("RECEIVEUSERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.FH).HasColumnName("FH").IsOptional();
            Property(x => x.SH).HasColumnName("SH").IsOptional();
            Property(x => x.ZYSD).HasColumnName("ZYSD").IsOptional();
            Property(x => x.ProjectInfoID).HasColumnName("PROJECTINFOID").IsOptional().HasMaxLength(50);
            Property(x => x.Collactor).HasColumnName("COLLACTOR").IsOptional().HasMaxLength(200);
            Property(x => x.CollactorName).HasColumnName("COLLACTORNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Auditor).HasColumnName("AUDITOR").IsOptional().HasMaxLength(200);
            Property(x => x.AuditorName).HasColumnName("AUDITORNAME").IsOptional().HasMaxLength(200);
            Property(x => x.MajorApprover).HasColumnName("MAJORAPPROVER").IsOptional().HasMaxLength(200);
            Property(x => x.MajorApproverName).HasColumnName("MAJORAPPROVERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectUserlist).HasColumnName("PROJECTUSERLIST").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectUserlistName).HasColumnName("PROJECTUSERLISTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Approver).HasColumnName("APPROVER").IsOptional().HasMaxLength(200);
            Property(x => x.ApproverName).HasColumnName("APPROVERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.VersionNumber).HasColumnName("VERSIONNUMBER").IsOptional().HasMaxLength(200);
        }
    }

    // T_EXE_ProductReview_ProjectGroupMembers
    internal partial class T_EXE_ProductReview_ProjectGroupMembersConfiguration : EntityTypeConfiguration<T_EXE_ProductReview_ProjectGroupMembers>
    {
        public T_EXE_ProductReview_ProjectGroupMembersConfiguration()
        {
			ToTable("T_EXE_PRODUCTREVIEW_PROJECTGROUPMEMBERS");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.T_EXE_ProductReviewID).HasColumnName("T_EXE_PRODUCTREVIEWID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.MettingUser).HasColumnName("METTINGUSER").IsOptional().HasMaxLength(200);
            Property(x => x.MettingUserName).HasColumnName("METTINGUSERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Major).HasColumnName("MAJOR").IsOptional().HasMaxLength(200);
            Property(x => x.Dept).HasColumnName("DEPT").IsOptional().HasMaxLength(200);
            Property(x => x.DeptName).HasColumnName("DEPTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.SignDate).HasColumnName("SIGNDATE").IsOptional();
            Property(x => x.SignComment).HasColumnName("SIGNCOMMENT").IsOptional().HasMaxLength(2000);
            Property(x => x.MajorCode).HasColumnName("MAJORCODE").IsOptional().HasMaxLength(200);

            // Foreign keys
            HasOptional(a => a.T_EXE_ProductReview).WithMany(b => b.T_EXE_ProductReview_ProjectGroupMembers).HasForeignKey(c => c.T_EXE_ProductReviewID); // FK_T_EXE_ProductReview_ProjectGroupMembers_T_EXE_ProductReview
        }
    }

    // T_EXE_ProductReview_ResultList
    internal partial class T_EXE_ProductReview_ResultListConfiguration : EntityTypeConfiguration<T_EXE_ProductReview_ResultList>
    {
        public T_EXE_ProductReview_ResultListConfiguration()
        {
			ToTable("T_EXE_PRODUCTREVIEW_RESULTLIST");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.T_EXE_ProductReviewID).HasColumnName("T_EXE_PRODUCTREVIEWID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.ProductID).HasColumnName("PRODUCTID").IsOptional().HasMaxLength(200);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(200);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(200);
            Property(x => x.Major).HasColumnName("MAJOR").IsOptional().HasMaxLength(200);
            Property(x => x.MajorValue).HasColumnName("MAJORVALUE").IsOptional().HasMaxLength(200);
            Property(x => x.FileType).HasColumnName("FILETYPE").IsOptional().HasMaxLength(200);
            Property(x => x.MainFile).HasColumnName("MAINFILE").IsOptional().HasMaxLength(200);

            // Foreign keys
            HasOptional(a => a.T_EXE_ProductReview).WithMany(b => b.T_EXE_ProductReview_ResultList).HasForeignKey(c => c.T_EXE_ProductReviewID); // FK_T_EXE_ProductReview_ResultList_T_EXE_ProductReview
        }
    }

    // T_EXE_ProjectExamination
    internal partial class T_EXE_ProjectExaminationConfiguration : EntityTypeConfiguration<T_EXE_ProjectExamination>
    {
        public T_EXE_ProjectExaminationConfiguration()
        {
			ToTable("T_EXE_PROJECTEXAMINATION");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.ModifyDate).HasColumnName("MODIFYDATE").IsOptional();
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsOptional().HasMaxLength(200);
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUser).HasColumnName("MODIFYUSER").IsOptional().HasMaxLength(50);
            Property(x => x.OrgID).HasColumnName("ORGID").IsOptional().HasMaxLength(50);
            Property(x => x.CompanyID).HasColumnName("COMPANYID").IsOptional().HasMaxLength(50);
            Property(x => x.FlowPhase).HasColumnName("FLOWPHASE").IsOptional().HasMaxLength(50);
            Property(x => x.FlowInfo).HasColumnName("FLOWINFO").IsOptional().HasMaxLength(1073741823);
            Property(x => x.StepName).HasColumnName("STEPNAME").IsOptional().HasMaxLength(500);
            Property(x => x.ProjectInfoName).HasColumnName("PROJECTINFONAME").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectInfoCode).HasColumnName("PROJECTINFOCODE").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectManager).HasColumnName("PROJECTMANAGER").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectManagerName).HasColumnName("PROJECTMANAGERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Phase).HasColumnName("PHASE").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectClass).HasColumnName("PROJECTCLASS").IsOptional().HasMaxLength(200);
            Property(x => x.Result).HasColumnName("RESULT").IsOptional().HasPrecision(18,4);
            Property(x => x.CreateUserName).HasColumnName("CREATEUSERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectInfoID).HasColumnName("PROJECTINFOID").IsOptional().HasMaxLength(200);
            Property(x => x.EnginneringInfoID).HasColumnName("ENGINNERINGINFOID").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectInfo).HasColumnName("PROJECTINFO").IsOptional().HasMaxLength(200);
            Property(x => x.VersionNumber).HasColumnName("VERSIONNUMBER").IsOptional().HasMaxLength(50);
            Property(x => x.ProjectLeader).HasColumnName("PROJECTLEADER").IsOptional();
            Property(x => x.TechnicalQualityDept).HasColumnName("TECHNICALQUALITYDEPT").IsOptional();
        }
    }

    // T_EXE_ProjectExamination_Content
    internal partial class T_EXE_ProjectExamination_ContentConfiguration : EntityTypeConfiguration<T_EXE_ProjectExamination_Content>
    {
        public T_EXE_ProjectExamination_ContentConfiguration()
        {
			ToTable("T_EXE_PROJECTEXAMINATION_CONTENT");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.T_EXE_ProjectExaminationID).HasColumnName("T_EXE_PROJECTEXAMINATIONID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.Content).HasColumnName("CONTENT").IsOptional().HasMaxLength(200);
            Property(x => x.Result).HasColumnName("RESULT").IsOptional().HasPrecision(18,4);
            Property(x => x.Remark).HasColumnName("REMARK").IsOptional().HasMaxLength(200);

            // Foreign keys
            HasOptional(a => a.T_EXE_ProjectExamination).WithMany(b => b.T_EXE_ProjectExamination_Content).HasForeignKey(c => c.T_EXE_ProjectExaminationID); // FK_T_EXE_ProjectExamination_Content_T_EXE_ProjectExamination
        }
    }

    // T_EXE_PublishApply
    internal partial class T_EXE_PublishApplyConfiguration : EntityTypeConfiguration<T_EXE_PublishApply>
    {
        public T_EXE_PublishApplyConfiguration()
        {
			ToTable("T_EXE_PUBLISHAPPLY");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.ModifyDate).HasColumnName("MODIFYDATE").IsOptional();
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUser).HasColumnName("MODIFYUSER").IsOptional().HasMaxLength(50);
            Property(x => x.OrgID).HasColumnName("ORGID").IsOptional().HasMaxLength(50);
            Property(x => x.CompanyID).HasColumnName("COMPANYID").IsOptional().HasMaxLength(50);
            Property(x => x.FlowPhase).HasColumnName("FLOWPHASE").IsOptional().HasMaxLength(50);
            Property(x => x.FlowInfo).HasColumnName("FLOWINFO").IsOptional().HasMaxLength(1073741823);
            Property(x => x.StepName).HasColumnName("STEPNAME").IsOptional().HasMaxLength(500);
            Property(x => x.SerialNumber).HasColumnName("SERIALNUMBER").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectInfoID).HasColumnName("PROJECTINFOID").IsOptional().HasMaxLength(200);
            Property(x => x.Project).HasColumnName("PROJECT").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectName).HasColumnName("PROJECTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectCode).HasColumnName("PROJECTCODE").IsOptional().HasMaxLength(200);
            Property(x => x.MajorCode).HasColumnName("MAJORCODE").IsOptional().HasMaxLength(200);
            Property(x => x.ProfessionalChargeView).HasColumnName("PROFESSIONALCHARGEVIEW").IsOptional();
            Property(x => x.ProjectManngerView).HasColumnName("PROJECTMANNGERVIEW").IsOptional();
            Property(x => x.ToA1).HasColumnName("TOA1").IsOptional().HasPrecision(18,4);
            Property(x => x.Price).HasColumnName("PRICE").IsOptional().HasPrecision(18,2);
            Property(x => x.CostAmount).HasColumnName("COSTAMOUNT").IsOptional().HasPrecision(18,2);
            Property(x => x.ProjectManager).HasColumnName("PROJECTMANAGER").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectManagerName).HasColumnName("PROJECTMANAGERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.PriceDetail).HasColumnName("PRICEDETAIL").IsOptional();
            Property(x => x.ApplyUser).HasColumnName("APPLYUSER").IsOptional().HasMaxLength(200);
            Property(x => x.ApplyUserName).HasColumnName("APPLYUSERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ApplyDate).HasColumnName("APPLYDATE").IsOptional();
            Property(x => x.Remark).HasColumnName("REMARK").IsOptional().HasMaxLength(500);
            Property(x => x.RealCostAmount).HasColumnName("REALCOSTAMOUNT").IsOptional().HasPrecision(18,2);
            Property(x => x.SubmitTime).HasColumnName("SUBMITTIME").IsOptional();
            Property(x => x.ConfirmTime).HasColumnName("CONFIRMTIME").IsOptional();
            Property(x => x.CostName).HasColumnName("COSTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.CostCode).HasColumnName("COSTCODE").IsOptional().HasMaxLength(200);
            Property(x => x.ApplyDept).HasColumnName("APPLYDEPT").IsOptional().HasMaxLength(200);
            Property(x => x.ApplyDeptName).HasColumnName("APPLYDEPTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.BorderSizeJson).HasColumnName("BORDERSIZEJSON").IsOptional();
            Property(x => x.FormCode).HasColumnName("FORMCODE").IsOptional().HasMaxLength(200);
            Property(x => x.ProductCount).HasColumnName("PRODUCTCOUNT").IsOptional();
        }
    }

    // T_EXE_PublishApply_PriceDetail
    internal partial class T_EXE_PublishApply_PriceDetailConfiguration : EntityTypeConfiguration<T_EXE_PublishApply_PriceDetail>
    {
        public T_EXE_PublishApply_PriceDetailConfiguration()
        {
			ToTable("T_EXE_PUBLISHAPPLY_PRICEDETAIL");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.T_EXE_PublishApplyID).HasColumnName("T_EXE_PUBLISHAPPLYID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.PublicationType).HasColumnName("PUBLICATIONTYPE").IsOptional().HasMaxLength(200);
            Property(x => x.Specification).HasColumnName("SPECIFICATION").IsOptional().HasMaxLength(200);
            Property(x => x.Num).HasColumnName("NUM").IsOptional().HasPrecision(18,2);
            Property(x => x.Price).HasColumnName("PRICE").IsOptional().HasPrecision(18,2);
            Property(x => x.Sum).HasColumnName("SUM").IsOptional().HasPrecision(18,2);
            Property(x => x.SumCost).HasColumnName("SUMCOST").IsOptional().HasPrecision(18,2);
            Property(x => x.RealNum).HasColumnName("REALNUM").IsOptional().HasPrecision(18,2);
            Property(x => x.RealPrice).HasColumnName("REALPRICE").IsOptional().HasPrecision(18,2);
            Property(x => x.RealSum).HasColumnName("REALSUM").IsOptional().HasPrecision(18,2);
            Property(x => x.RealSumCost).HasColumnName("REALSUMCOST").IsOptional().HasPrecision(18,2);
            Property(x => x.CostInfoID).HasColumnName("COSTINFOID").IsOptional().HasMaxLength(200);

            // Foreign keys
            HasOptional(a => a.T_EXE_PublishApply).WithMany(b => b.T_EXE_PublishApply_PriceDetail).HasForeignKey(c => c.T_EXE_PublishApplyID); // FK_T_EXE_PublishApply_PriceDetail_T_EXE_PublishApply
        }
    }

    // T_EXE_PublishApply_Products
    internal partial class T_EXE_PublishApply_ProductsConfiguration : EntityTypeConfiguration<T_EXE_PublishApply_Products>
    {
        public T_EXE_PublishApply_ProductsConfiguration()
        {
			ToTable("T_EXE_PUBLISHAPPLY_PRODUCTS");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.T_EXE_PublishApplyID).HasColumnName("T_EXE_PUBLISHAPPLYID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.ProductID).HasColumnName("PRODUCTID").IsOptional().HasMaxLength(200);
            Property(x => x.ProductName).HasColumnName("PRODUCTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ProductCode).HasColumnName("PRODUCTCODE").IsOptional().HasMaxLength(200);
            Property(x => x.Specifications).HasColumnName("SPECIFICATIONS").IsOptional().HasMaxLength(200);
            Property(x => x.Count).HasColumnName("COUNT").IsOptional();
            Property(x => x.PlotSealGroup).HasColumnName("PLOTSEALGROUP").IsOptional().HasMaxLength(2000);
            Property(x => x.PlotSealGroupName).HasColumnName("PLOTSEALGROUPNAME").IsOptional().HasMaxLength(2000);
            Property(x => x.MainAttachments).HasColumnName("MAINATTACHMENTS").IsOptional().HasMaxLength(200);
            Property(x => x.ProductVersion).HasColumnName("PRODUCTVERSION").IsOptional().HasPrecision(18,2);
            Property(x => x.VersionID).HasColumnName("VERSIONID").IsOptional().HasMaxLength(200);
            Property(x => x.PdfFile).HasColumnName("PDFFILE").IsOptional().HasMaxLength(200);
            Property(x => x.PlotFile).HasColumnName("PLOTFILE").IsOptional().HasMaxLength(200);
            Property(x => x.SignPdfFile).HasColumnName("SIGNPDFFILE").IsOptional().HasMaxLength(200);
            Property(x => x.XrefFile).HasColumnName("XREFFILE").IsOptional().HasMaxLength(200);
            Property(x => x.DwfFile).HasColumnName("DWFFILE").IsOptional().HasMaxLength(200);
            Property(x => x.TiffFile).HasColumnName("TIFFFILE").IsOptional().HasMaxLength(200);
            Property(x => x.Designer).HasColumnName("DESIGNER").IsOptional().HasMaxLength(200);
            Property(x => x.DesignerName).HasColumnName("DESIGNERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Collactor).HasColumnName("COLLACTOR").IsOptional().HasMaxLength(200);
            Property(x => x.CollactorName).HasColumnName("COLLACTORNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Auditor).HasColumnName("AUDITOR").IsOptional().HasMaxLength(200);
            Property(x => x.AuditorName).HasColumnName("AUDITORNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Approver).HasColumnName("APPROVER").IsOptional().HasMaxLength(200);
            Property(x => x.ApproverName).HasColumnName("APPROVERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectManager).HasColumnName("PROJECTMANAGER").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectManagerName).HasColumnName("PROJECTMANAGERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.DesignManager).HasColumnName("DESIGNMANAGER").IsOptional().HasMaxLength(200);
            Property(x => x.DesignManagerName).HasColumnName("DESIGNMANAGERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Mapper).HasColumnName("MAPPER").IsOptional().HasMaxLength(200);
            Property(x => x.MapperName).HasColumnName("MAPPERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.MajorPrinciple).HasColumnName("MAJORPRINCIPLE").IsOptional().HasMaxLength(200);
            Property(x => x.MajorPrincipleName).HasColumnName("MAJORPRINCIPLENAME").IsOptional().HasMaxLength(200);
            Property(x => x.MajorEngineer).HasColumnName("MAJORENGINEER").IsOptional().HasMaxLength(200);
            Property(x => x.MajorEngineerName).HasColumnName("MAJORENGINEERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.DeptManager).HasColumnName("DEPTMANAGER").IsOptional().HasMaxLength(200);
            Property(x => x.DeptManagerName).HasColumnName("DEPTMANAGERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.CanSetUser).HasColumnName("CANSETUSER").IsOptional();
            Property(x => x.PDFSignPositionInfo).HasColumnName("PDFSIGNPOSITIONINFO").IsOptional();
            Property(x => x.PlotSealGroupKey).HasColumnName("PLOTSEALGROUPKEY").IsOptional().HasMaxLength(200);
            Property(x => x.CoSignUser).HasColumnName("COSIGNUSER").IsOptional();
            Property(x => x.DrawingCode).HasColumnName("DRAWINGCODE").IsOptional().HasMaxLength(200);

            // Foreign keys
            HasOptional(a => a.T_EXE_PublishApply).WithMany(b => b.T_EXE_PublishApply_Products).HasForeignKey(c => c.T_EXE_PublishApplyID); // FK_T_EXE_PublishApply_Products_T_EXE_PublishApply
        }
    }

    // T_EXE_TaskWorkChange
    internal partial class T_EXE_TaskWorkChangeConfiguration : EntityTypeConfiguration<T_EXE_TaskWorkChange>
    {
        public T_EXE_TaskWorkChangeConfiguration()
        {
			ToTable("T_EXE_TASKWORKCHANGE");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.ModifyDate).HasColumnName("MODIFYDATE").IsOptional();
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUser).HasColumnName("MODIFYUSER").IsOptional().HasMaxLength(50);
            Property(x => x.OrgID).HasColumnName("ORGID").IsOptional().HasMaxLength(50);
            Property(x => x.CompanyID).HasColumnName("COMPANYID").IsOptional().HasMaxLength(50);
            Property(x => x.FlowPhase).HasColumnName("FLOWPHASE").IsOptional().HasMaxLength(50);
            Property(x => x.FlowInfo).HasColumnName("FLOWINFO").IsOptional().HasMaxLength(1073741823);
            Property(x => x.StepName).HasColumnName("STEPNAME").IsOptional().HasMaxLength(500);
            Property(x => x.ProjectInfo).HasColumnName("PROJECTINFO").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectInfoName).HasColumnName("PROJECTINFONAME").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectCode).HasColumnName("PROJECTCODE").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectManager).HasColumnName("PROJECTMANAGER").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectManagerName).HasColumnName("PROJECTMANAGERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Phase).HasColumnName("PHASE").IsOptional().HasMaxLength(200);
            Property(x => x.MajorValue).HasColumnName("MAJORVALUE").IsOptional().HasMaxLength(200);
            Property(x => x.MajorName).HasColumnName("MAJORNAME").IsOptional().HasMaxLength(200);
            Property(x => x.MajorPrinciple).HasColumnName("MAJORPRINCIPLE").IsOptional().HasMaxLength(200);
            Property(x => x.MajorPrincipleName).HasColumnName("MAJORPRINCIPLENAME").IsOptional().HasMaxLength(200);
            Property(x => x.BelongDept).HasColumnName("BELONGDEPT").IsOptional().HasMaxLength(200);
            Property(x => x.BelongDeptName).HasColumnName("BELONGDEPTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.BelongDate).HasColumnName("BELONGDATE").IsOptional();
            Property(x => x.ProjectInfoID).HasColumnName("PROJECTINFOID").IsOptional().HasMaxLength(200);
            Property(x => x.TaskWork).HasColumnName("TASKWORK").IsOptional();
            Property(x => x.Remark).HasColumnName("REMARK").IsOptional().HasMaxLength(500);
        }
    }

    // T_EXE_TaskWorkChange_TaskWork
    internal partial class T_EXE_TaskWorkChange_TaskWorkConfiguration : EntityTypeConfiguration<T_EXE_TaskWorkChange_TaskWork>
    {
        public T_EXE_TaskWorkChange_TaskWorkConfiguration()
        {
			ToTable("T_EXE_TASKWORKCHANGE_TASKWORK");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.T_EXE_TaskWorkChangeID).HasColumnName("T_EXE_TASKWORKCHANGEID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.Major).HasColumnName("MAJOR").IsOptional().HasMaxLength(200);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(200);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(200);
            Property(x => x.OldWorkload).HasColumnName("OLDWORKLOAD").IsOptional().HasMaxLength(200);
            Property(x => x.Workload).HasColumnName("WORKLOAD").IsOptional().HasPrecision(18,2);
            Property(x => x.OldPlanEndDate).HasColumnName("OLDPLANENDDATE").IsOptional();
            Property(x => x.PlanEndDate).HasColumnName("PLANENDDATE").IsOptional();
            Property(x => x.RoleRate).HasColumnName("ROLERATE").IsOptional();
            Property(x => x.TaskWorkID).HasColumnName("TASKWORKID").IsOptional().HasMaxLength(200);
            Property(x => x.IsChanged).HasColumnName("ISCHANGED").IsOptional().HasMaxLength(50);
            Property(x => x.Phase).HasColumnName("PHASE").IsOptional().HasMaxLength(200);

            // Foreign keys
            HasOptional(a => a.T_EXE_TaskWorkChange).WithMany(b => b.T_EXE_TaskWorkChange_TaskWork).HasForeignKey(c => c.T_EXE_TaskWorkChangeID); // FK_T_EXE_TaskWorkChange_TaskWork_T_EXE_TaskWorkChange
        }
    }

    // T_EXE_TaskWorkPublish
    internal partial class T_EXE_TaskWorkPublishConfiguration : EntityTypeConfiguration<T_EXE_TaskWorkPublish>
    {
        public T_EXE_TaskWorkPublishConfiguration()
        {
			ToTable("T_EXE_TASKWORKPUBLISH");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.ModifyDate).HasColumnName("MODIFYDATE").IsOptional();
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUser).HasColumnName("MODIFYUSER").IsOptional().HasMaxLength(50);
            Property(x => x.OrgID).HasColumnName("ORGID").IsOptional().HasMaxLength(50);
            Property(x => x.CompanyID).HasColumnName("COMPANYID").IsOptional().HasMaxLength(50);
            Property(x => x.FlowPhase).HasColumnName("FLOWPHASE").IsOptional().HasMaxLength(50);
            Property(x => x.FlowInfo).HasColumnName("FLOWINFO").IsOptional().HasMaxLength(1073741823);
            Property(x => x.StepName).HasColumnName("STEPNAME").IsOptional().HasMaxLength(500);
            Property(x => x.ProjectInfo).HasColumnName("PROJECTINFO").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectInfoName).HasColumnName("PROJECTINFONAME").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectCode).HasColumnName("PROJECTCODE").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectManager).HasColumnName("PROJECTMANAGER").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectManagerName).HasColumnName("PROJECTMANAGERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Phase).HasColumnName("PHASE").IsOptional().HasMaxLength(200);
            Property(x => x.MajorValue).HasColumnName("MAJORVALUE").IsOptional().HasMaxLength(200);
            Property(x => x.MajorName).HasColumnName("MAJORNAME").IsOptional().HasMaxLength(200);
            Property(x => x.MajorPrinciple).HasColumnName("MAJORPRINCIPLE").IsOptional().HasMaxLength(200);
            Property(x => x.MajorPrincipleName).HasColumnName("MAJORPRINCIPLENAME").IsOptional().HasMaxLength(200);
            Property(x => x.BelongDept).HasColumnName("BELONGDEPT").IsOptional().HasMaxLength(200);
            Property(x => x.BelongDeptName).HasColumnName("BELONGDEPTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.BelongDate).HasColumnName("BELONGDATE").IsOptional();
            Property(x => x.ProjectInfoID).HasColumnName("PROJECTINFOID").IsOptional().HasMaxLength(200);
            Property(x => x.TaskWork).HasColumnName("TASKWORK").IsOptional();
            Property(x => x.Remark).HasColumnName("REMARK").IsOptional().HasMaxLength(500);
        }
    }

    // T_EXE_TaskWorkPublish_TaskWork
    internal partial class T_EXE_TaskWorkPublish_TaskWorkConfiguration : EntityTypeConfiguration<T_EXE_TaskWorkPublish_TaskWork>
    {
        public T_EXE_TaskWorkPublish_TaskWorkConfiguration()
        {
			ToTable("T_EXE_TASKWORKPUBLISH_TASKWORK");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.T_EXE_TaskWorkPublishID).HasColumnName("T_EXE_TASKWORKPUBLISHID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.Major).HasColumnName("MAJOR").IsOptional().HasMaxLength(200);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(200);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(200);
            Property(x => x.Workload).HasColumnName("WORKLOAD").IsOptional().HasPrecision(18,2);
            Property(x => x.PlanEndDate).HasColumnName("PLANENDDATE").IsOptional();
            Property(x => x.RoleRate).HasColumnName("ROLERATE").IsOptional();
            Property(x => x.TaskWorkID).HasColumnName("TASKWORKID").IsOptional().HasMaxLength(200);
            Property(x => x.IsChanged).HasColumnName("ISCHANGED").IsOptional().HasMaxLength(50);
            Property(x => x.Phase).HasColumnName("PHASE").IsOptional().HasMaxLength(200);

            // Foreign keys
            HasOptional(a => a.T_EXE_TaskWorkPublish).WithMany(b => b.T_EXE_TaskWorkPublish_TaskWork).HasForeignKey(c => c.T_EXE_TaskWorkPublishID); // FK_T_EXE_TaskWorkPublish_TaskWork_T_EXE_TaskWorkPublish
        }
    }

    // T_EXE_TaskWorkSettlement
    internal partial class T_EXE_TaskWorkSettlementConfiguration : EntityTypeConfiguration<T_EXE_TaskWorkSettlement>
    {
        public T_EXE_TaskWorkSettlementConfiguration()
        {
			ToTable("T_EXE_TASKWORKSETTLEMENT");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.ModifyDate).HasColumnName("MODIFYDATE").IsOptional();
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUser).HasColumnName("MODIFYUSER").IsOptional().HasMaxLength(50);
            Property(x => x.OrgID).HasColumnName("ORGID").IsOptional().HasMaxLength(50);
            Property(x => x.CompanyID).HasColumnName("COMPANYID").IsOptional().HasMaxLength(50);
            Property(x => x.FlowPhase).HasColumnName("FLOWPHASE").IsOptional().HasMaxLength(50);
            Property(x => x.FlowInfo).HasColumnName("FLOWINFO").IsOptional().HasMaxLength(1073741823);
            Property(x => x.StepName).HasColumnName("STEPNAME").IsOptional().HasMaxLength(500);
            Property(x => x.ProjectInfo).HasColumnName("PROJECTINFO").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectInfoName).HasColumnName("PROJECTINFONAME").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectCode).HasColumnName("PROJECTCODE").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectManager).HasColumnName("PROJECTMANAGER").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectManagerName).HasColumnName("PROJECTMANAGERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Phase).HasColumnName("PHASE").IsOptional().HasMaxLength(200);
            Property(x => x.Major).HasColumnName("MAJOR").IsOptional().HasMaxLength(200);
            Property(x => x.BelongDept).HasColumnName("BELONGDEPT").IsOptional().HasMaxLength(200);
            Property(x => x.BelongDeptName).HasColumnName("BELONGDEPTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.BelongDate).HasColumnName("BELONGDATE").IsOptional();
            Property(x => x.ProjectManagerApprove).HasColumnName("PROJECTMANAGERAPPROVE").IsOptional();
            Property(x => x.Remark).HasColumnName("REMARK").IsOptional().HasMaxLength(500);
            Property(x => x.SummaySettlement).HasColumnName("SUMMAYSETTLEMENT").IsOptional().HasPrecision(18,2);
        }
    }

    // T_EXE_TaskWorkSettlement_TaskWorkList
    internal partial class T_EXE_TaskWorkSettlement_TaskWorkListConfiguration : EntityTypeConfiguration<T_EXE_TaskWorkSettlement_TaskWorkList>
    {
        public T_EXE_TaskWorkSettlement_TaskWorkListConfiguration()
        {
			ToTable("T_EXE_TASKWORKSETTLEMENT_TASKWORKLIST");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.T_EXE_TaskWorkSettlementID).HasColumnName("T_EXE_TASKWORKSETTLEMENTID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(200);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(200);
            Property(x => x.Workload).HasColumnName("WORKLOAD").IsOptional().HasPrecision(18,2);
            Property(x => x.SummarySettlement).HasColumnName("SUMMARYSETTLEMENT").IsOptional().HasPrecision(18,2);
            Property(x => x.Settlement).HasColumnName("SETTLEMENT").IsOptional().HasPrecision(18,2);
            Property(x => x.BudgetID).HasColumnName("BUDGETID").IsOptional().HasMaxLength(200);
            Property(x => x.Major).HasColumnName("MAJOR").IsOptional().HasMaxLength(200);

            // Foreign keys
            HasOptional(a => a.T_EXE_TaskWorkSettlement).WithMany(b => b.T_EXE_TaskWorkSettlement_TaskWorkList).HasForeignKey(c => c.T_EXE_TaskWorkSettlementID); // FK_T_EXE_TaskWorkSettlement_TaskWorkList_T_EXE_TaskWorkSettlement
        }
    }

    // T_SC_DesignInput
    internal partial class T_SC_DesignInputConfiguration : EntityTypeConfiguration<T_SC_DesignInput>
    {
        public T_SC_DesignInputConfiguration()
        {
			ToTable("T_SC_DESIGNINPUT");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.ModifyDate).HasColumnName("MODIFYDATE").IsOptional();
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUser).HasColumnName("MODIFYUSER").IsOptional().HasMaxLength(50);
            Property(x => x.OrgID).HasColumnName("ORGID").IsOptional().HasMaxLength(50);
            Property(x => x.CompanyID).HasColumnName("COMPANYID").IsOptional().HasMaxLength(50);
            Property(x => x.FlowPhase).HasColumnName("FLOWPHASE").IsOptional().HasMaxLength(50);
            Property(x => x.FlowInfo).HasColumnName("FLOWINFO").IsOptional().HasMaxLength(1073741823);
            Property(x => x.StepName).HasColumnName("STEPNAME").IsOptional().HasMaxLength(500);
            Property(x => x.ProjectInfoID).HasColumnName("PROJECTINFOID").IsOptional().HasMaxLength(50);
            Property(x => x.ProjectName).HasColumnName("PROJECTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectCode).HasColumnName("PROJECTCODE").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectManager).HasColumnName("PROJECTMANAGER").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectManagerName).HasColumnName("PROJECTMANAGERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ChargeDept).HasColumnName("CHARGEDEPT").IsOptional().HasMaxLength(200);
            Property(x => x.ChargeDeptName).HasColumnName("CHARGEDEPTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectClass).HasColumnName("PROJECTCLASS").IsOptional().HasMaxLength(200);
            Property(x => x.PhaseValue).HasColumnName("PHASEVALUE").IsOptional().HasMaxLength(200);
            Property(x => x.PlanStartDate).HasColumnName("PLANSTARTDATE").IsOptional();
            Property(x => x.PlanFinishDate).HasColumnName("PLANFINISHDATE").IsOptional();
            Property(x => x.DesignInputList).HasColumnName("DESIGNINPUTLIST").IsOptional();
            Property(x => x.VersionNumber).HasColumnName("VERSIONNUMBER").IsOptional().HasMaxLength(200);
            Property(x => x.Catagory).HasColumnName("CATAGORY").IsOptional().HasMaxLength(200);
            Property(x => x.SerialNumber).HasColumnName("SERIALNUMBER").IsOptional().HasMaxLength(200);
            Property(x => x.SRPSJG).HasColumnName("SRPSJG").IsOptional().HasMaxLength(500);
            Property(x => x.TBR).HasColumnName("TBR").IsOptional().HasMaxLength(200);
            Property(x => x.TBRName).HasColumnName("TBRNAME").IsOptional().HasMaxLength(200);
        }
    }

    // T_SC_DesignInput_DesignInputList
    internal partial class T_SC_DesignInput_DesignInputListConfiguration : EntityTypeConfiguration<T_SC_DesignInput_DesignInputList>
    {
        public T_SC_DesignInput_DesignInputListConfiguration()
        {
			ToTable("T_SC_DESIGNINPUT_DESIGNINPUTLIST");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.T_SC_DesignInputID).HasColumnName("T_SC_DESIGNINPUTID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.InputType).HasColumnName("INPUTTYPE").IsOptional().HasMaxLength(200);
            Property(x => x.InfoName).HasColumnName("INFONAME").IsOptional().HasMaxLength(200);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(200);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional().HasMaxLength(200);
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsOptional().HasMaxLength(200);
            Property(x => x.InputInfoID).HasColumnName("INPUTINFOID").IsOptional().HasMaxLength(200);
            Property(x => x.DocID).HasColumnName("DOCID").IsOptional().HasMaxLength(200);
            Property(x => x.Catagory).HasColumnName("CATAGORY").IsOptional().HasMaxLength(200);
            Property(x => x.Files).HasColumnName("FILES").IsOptional().HasMaxLength(500);
            Property(x => x.TGDW).HasColumnName("TGDW").IsOptional().HasMaxLength(200);
            Property(x => x.FS).HasColumnName("FS").IsOptional().HasMaxLength(200);
            Property(x => x.YS).HasColumnName("YS").IsOptional().HasMaxLength(200);
            Property(x => x.YZMC).HasColumnName("YZMC").IsOptional().HasMaxLength(200);
            Property(x => x.YZSL).HasColumnName("YZSL").IsOptional().HasMaxLength(200);
            Property(x => x.YZYM).HasColumnName("YZYM").IsOptional().HasMaxLength(200);
            Property(x => x.YZR).HasColumnName("YZR").IsOptional().HasMaxLength(200);
            Property(x => x.BZ).HasColumnName("BZ").IsOptional().HasMaxLength(200);

            // Foreign keys
            HasOptional(a => a.T_SC_DesignInput).WithMany(b => b.T_SC_DesignInput_DesignInputList).HasForeignKey(c => c.T_SC_DesignInputID); // FK_T_SC_DesignInput_DesignInputList_T_SC_DesignInput
        }
    }

    // T_SC_DesignOutline
    internal partial class T_SC_DesignOutlineConfiguration : EntityTypeConfiguration<T_SC_DesignOutline>
    {
        public T_SC_DesignOutlineConfiguration()
        {
			ToTable("T_SC_DESIGNOUTLINE");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.ModifyDate).HasColumnName("MODIFYDATE").IsOptional();
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUser).HasColumnName("MODIFYUSER").IsOptional().HasMaxLength(50);
            Property(x => x.OrgID).HasColumnName("ORGID").IsOptional().HasMaxLength(50);
            Property(x => x.CompanyID).HasColumnName("COMPANYID").IsOptional().HasMaxLength(50);
            Property(x => x.FlowPhase).HasColumnName("FLOWPHASE").IsOptional().HasMaxLength(50);
            Property(x => x.FlowInfo).HasColumnName("FLOWINFO").IsOptional().HasMaxLength(1073741823);
            Property(x => x.StepName).HasColumnName("STEPNAME").IsOptional().HasMaxLength(500);
            Property(x => x.ItemNumber).HasColumnName("ITEMNUMBER").IsOptional().HasMaxLength(200);
            Property(x => x.SerialNumber).HasColumnName("SERIALNUMBER").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectName).HasColumnName("PROJECTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectNameName).HasColumnName("PROJECTNAMENAME").IsOptional().HasMaxLength(200);
            Property(x => x.DesignPhase).HasColumnName("DESIGNPHASE").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectType).HasColumnName("PROJECTTYPE").IsOptional().HasMaxLength(200);
            Property(x => x.Author).HasColumnName("AUTHOR").IsOptional().HasMaxLength(200);
            Property(x => x.AuthorName).HasColumnName("AUTHORNAME").IsOptional().HasMaxLength(200);
            Property(x => x.SubmissionDate).HasColumnName("SUBMISSIONDATE").IsOptional();
            Property(x => x.attachment).HasColumnName("ATTACHMENT").IsOptional().HasMaxLength(500);
            Property(x => x.Remark).HasColumnName("REMARK").IsOptional().HasMaxLength(500);
            Property(x => x.ApproveSign).HasColumnName("APPROVESIGN").IsOptional();
        }
    }

    // T_SC_DesignPlan
    internal partial class T_SC_DesignPlanConfiguration : EntityTypeConfiguration<T_SC_DesignPlan>
    {
        public T_SC_DesignPlanConfiguration()
        {
			ToTable("T_SC_DESIGNPLAN");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.ModifyDate).HasColumnName("MODIFYDATE").IsOptional();
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUser).HasColumnName("MODIFYUSER").IsOptional().HasMaxLength(50);
            Property(x => x.OrgID).HasColumnName("ORGID").IsOptional().HasMaxLength(50);
            Property(x => x.CompanyID).HasColumnName("COMPANYID").IsOptional().HasMaxLength(50);
            Property(x => x.FlowPhase).HasColumnName("FLOWPHASE").IsOptional().HasMaxLength(50);
            Property(x => x.FlowInfo).HasColumnName("FLOWINFO").IsOptional().HasMaxLength(1073741823);
            Property(x => x.StepName).HasColumnName("STEPNAME").IsOptional().HasMaxLength(500);
            Property(x => x.SerialNumber).HasColumnName("SERIALNUMBER").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectName).HasColumnName("PROJECTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectCode).HasColumnName("PROJECTCODE").IsOptional().HasMaxLength(200);
            Property(x => x.SubProjectName).HasColumnName("SUBPROJECTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.PhaseValue).HasColumnName("PHASEVALUE").IsOptional().HasMaxLength(200);
            Property(x => x.ChargeDept).HasColumnName("CHARGEDEPT").IsOptional().HasMaxLength(50);
            Property(x => x.ChargeDeptName).HasColumnName("CHARGEDEPTNAME").IsOptional().HasMaxLength(50);
            Property(x => x.ProjectType).HasColumnName("PROJECTTYPE").IsOptional().HasMaxLength(200);
            Property(x => x.PlanStartDate).HasColumnName("PLANSTARTDATE").IsOptional();
            Property(x => x.PlanFinishDate).HasColumnName("PLANFINISHDATE").IsOptional();
            Property(x => x.MilestoneList).HasColumnName("MILESTONELIST").IsOptional();
            Property(x => x.ProjectInfoID).HasColumnName("PROJECTINFOID").IsOptional().HasMaxLength(200);
            Property(x => x.VersionNumber).HasColumnName("VERSIONNUMBER").IsOptional().HasMaxLength(50);
            Property(x => x.PhaseName).HasColumnName("PHASENAME").IsOptional().HasMaxLength(200);
            Property(x => x.SubProjectWBSID).HasColumnName("SUBPROJECTWBSID").IsOptional().HasMaxLength(200);
            Property(x => x.ChargeUser).HasColumnName("CHARGEUSER").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectLeader).HasColumnName("PROJECTLEADER").IsOptional();
            Property(x => x.Director).HasColumnName("DIRECTOR").IsOptional();
        }
    }

    // T_SC_DesignPlan_MilestoneList
    internal partial class T_SC_DesignPlan_MilestoneListConfiguration : EntityTypeConfiguration<T_SC_DesignPlan_MilestoneList>
    {
        public T_SC_DesignPlan_MilestoneListConfiguration()
        {
			ToTable("T_SC_DESIGNPLAN_MILESTONELIST");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.T_SC_DesignPlanID).HasColumnName("T_SC_DESIGNPLANID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(200);
            Property(x => x.Major).HasColumnName("MAJOR").IsOptional().HasMaxLength(200);
            Property(x => x.PlanEndDate).HasColumnName("PLANENDDATE").IsOptional();
            Property(x => x.Weight).HasColumnName("WEIGHT").IsOptional().HasPrecision(18,2);
            Property(x => x.Remark).HasColumnName("REMARK").IsOptional().HasMaxLength(2000);
            Property(x => x.MileStoneType).HasColumnName("MILESTONETYPE").IsOptional().HasMaxLength(200);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(200);
            Property(x => x.MileStoneID).HasColumnName("MILESTONEID").IsOptional().HasMaxLength(50);
            Property(x => x.OutMajor).HasColumnName("OUTMAJOR").IsOptional().HasMaxLength(200);
            Property(x => x.InMajor).HasColumnName("INMAJOR").IsOptional().HasMaxLength(200);
            Property(x => x.TemplateID).HasColumnName("TEMPLATEID").IsOptional().HasMaxLength(200);

            // Foreign keys
            HasOptional(a => a.T_SC_DesignPlan).WithMany(b => b.T_SC_DesignPlan_MilestoneList).HasForeignKey(c => c.T_SC_DesignPlanID); // FK_T_SC_DesignPlan_MilestoneList_T_SC_DesignPlan
        }
    }

    // T_SC_DesignPlanPetrifaction
    internal partial class T_SC_DesignPlanPetrifactionConfiguration : EntityTypeConfiguration<T_SC_DesignPlanPetrifaction>
    {
        public T_SC_DesignPlanPetrifactionConfiguration()
        {
			ToTable("T_SC_DESIGNPLANPETRIFACTION");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.ModifyDate).HasColumnName("MODIFYDATE").IsOptional();
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUser).HasColumnName("MODIFYUSER").IsOptional().HasMaxLength(50);
            Property(x => x.OrgID).HasColumnName("ORGID").IsOptional().HasMaxLength(50);
            Property(x => x.CompanyID).HasColumnName("COMPANYID").IsOptional().HasMaxLength(50);
            Property(x => x.FlowPhase).HasColumnName("FLOWPHASE").IsOptional().HasMaxLength(50);
            Property(x => x.FlowInfo).HasColumnName("FLOWINFO").IsOptional().HasMaxLength(1073741823);
            Property(x => x.StepName).HasColumnName("STEPNAME").IsOptional().HasMaxLength(500);
            Property(x => x.SerialNumber).HasColumnName("SERIALNUMBER").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectName).HasColumnName("PROJECTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectCode).HasColumnName("PROJECTCODE").IsOptional().HasMaxLength(200);
            Property(x => x.ChargeDept).HasColumnName("CHARGEDEPT").IsOptional().HasMaxLength(50);
            Property(x => x.ChargeDeptName).HasColumnName("CHARGEDEPTNAME").IsOptional().HasMaxLength(50);
            Property(x => x.ProjectType).HasColumnName("PROJECTTYPE").IsOptional().HasMaxLength(200);
            Property(x => x.PlanStartDate).HasColumnName("PLANSTARTDATE").IsOptional();
            Property(x => x.PlanFinishDate).HasColumnName("PLANFINISHDATE").IsOptional();
            Property(x => x.ProjectInfoID).HasColumnName("PROJECTINFOID").IsOptional().HasMaxLength(200);
            Property(x => x.VersionNumber).HasColumnName("VERSIONNUMBER").IsOptional().HasMaxLength(50);
            Property(x => x.ChargeUser).HasColumnName("CHARGEUSER").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectLeader).HasColumnName("PROJECTLEADER").IsOptional();
        }
    }

    // T_SC_DesignPlanPetrifaction_MilestoneList
    internal partial class T_SC_DesignPlanPetrifaction_MilestoneListConfiguration : EntityTypeConfiguration<T_SC_DesignPlanPetrifaction_MilestoneList>
    {
        public T_SC_DesignPlanPetrifaction_MilestoneListConfiguration()
        {
			ToTable("T_SC_DESIGNPLANPETRIFACTION_MILESTONELIST");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.T_SC_DesignPlanPetrifactionID).HasColumnName("T_SC_DESIGNPLANPETRIFACTIONID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(200);
            Property(x => x.Major).HasColumnName("MAJOR").IsOptional().HasMaxLength(200);
            Property(x => x.PlanEndDate).HasColumnName("PLANENDDATE").IsOptional();
            Property(x => x.Weight).HasColumnName("WEIGHT").IsOptional().HasPrecision(18,2);
            Property(x => x.Remark).HasColumnName("REMARK").IsOptional().HasMaxLength(2000);
            Property(x => x.MileStoneType).HasColumnName("MILESTONETYPE").IsOptional().HasMaxLength(200);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(200);
            Property(x => x.MileStoneID).HasColumnName("MILESTONEID").IsOptional().HasMaxLength(50);
            Property(x => x.OutMajor).HasColumnName("OUTMAJOR").IsOptional().HasMaxLength(200);
            Property(x => x.InMajor).HasColumnName("INMAJOR").IsOptional().HasMaxLength(200);
            Property(x => x.TemplateID).HasColumnName("TEMPLATEID").IsOptional().HasMaxLength(200);

            // Foreign keys
            HasOptional(a => a.T_SC_DesignPlanPetrifaction).WithMany(b => b.T_SC_DesignPlanPetrifaction_MilestoneList).HasForeignKey(c => c.T_SC_DesignPlanPetrifactionID); // FK_T_SC_DesignPlanPetrifaction_MilestoneList_T_SC_DesignPlanPetrifaction
        }
    }

    // T_SC_ElectricalPowerProjectScheme
    internal partial class T_SC_ElectricalPowerProjectSchemeConfiguration : EntityTypeConfiguration<T_SC_ElectricalPowerProjectScheme>
    {
        public T_SC_ElectricalPowerProjectSchemeConfiguration()
        {
			ToTable("T_SC_ELECTRICALPOWERPROJECTSCHEME");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.ModifyDate).HasColumnName("MODIFYDATE").IsOptional();
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUser).HasColumnName("MODIFYUSER").IsOptional().HasMaxLength(50);
            Property(x => x.OrgID).HasColumnName("ORGID").IsOptional().HasMaxLength(50);
            Property(x => x.CompanyID).HasColumnName("COMPANYID").IsOptional().HasMaxLength(50);
            Property(x => x.FlowPhase).HasColumnName("FLOWPHASE").IsOptional().HasMaxLength(50);
            Property(x => x.FlowInfo).HasColumnName("FLOWINFO").IsOptional().HasMaxLength(1073741823);
            Property(x => x.StepName).HasColumnName("STEPNAME").IsOptional().HasMaxLength(500);
            Property(x => x.SerialNumber).HasColumnName("SERIALNUMBER").IsOptional().HasMaxLength(200);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(200);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(200);
            Property(x => x.PhaseValue).HasColumnName("PHASEVALUE").IsOptional().HasMaxLength(50);
            Property(x => x.PhaseName).HasColumnName("PHASENAME").IsOptional().HasMaxLength(200);
            Property(x => x.ChargeDept).HasColumnName("CHARGEDEPT").IsOptional().HasMaxLength(50);
            Property(x => x.ChargeDeptName).HasColumnName("CHARGEDEPTNAME").IsOptional().HasMaxLength(50);
            Property(x => x.ProjectClass).HasColumnName("PROJECTCLASS").IsOptional().HasMaxLength(200);
            Property(x => x.BuildArea).HasColumnName("BUILDAREA").IsOptional();
            Property(x => x.PlanStartDate).HasColumnName("PLANSTARTDATE").IsOptional();
            Property(x => x.PlanFinishDate).HasColumnName("PLANFINISHDATE").IsOptional();
            Property(x => x.ProjectWorkload).HasColumnName("PROJECTWORKLOAD").IsOptional().HasPrecision(18,2);
            Property(x => x.Major).HasColumnName("MAJOR").IsOptional().HasMaxLength(500);
            Property(x => x.ChargeUser).HasColumnName("CHARGEUSER").IsOptional().HasMaxLength(500);
            Property(x => x.ChargeUserName).HasColumnName("CHARGEUSERNAME").IsOptional().HasMaxLength(500);
            Property(x => x.MajorList).HasColumnName("MAJORLIST").IsOptional();
            Property(x => x.ProjectInfoID).HasColumnName("PROJECTINFOID").IsOptional().HasMaxLength(200);
            Property(x => x.VersionNumber).HasColumnName("VERSIONNUMBER").IsOptional().HasMaxLength(50);
            Property(x => x.Attachments).HasColumnName("ATTACHMENTS").IsOptional().HasMaxLength(500);
            Property(x => x.Remark).HasColumnName("REMARK").IsOptional().HasMaxLength(500);
            Property(x => x.ProjectLevel).HasColumnName("PROJECTLEVEL").IsOptional().HasMaxLength(500);
            Property(x => x.DesignRequired).HasColumnName("DESIGNREQUIRED").IsOptional().HasMaxLength(500);
            Property(x => x.DesignInput).HasColumnName("DESIGNINPUT").IsOptional().HasMaxLength(500);
            Property(x => x.ProjectLeader).HasColumnName("PROJECTLEADER").IsOptional();
            Property(x => x.Director).HasColumnName("DIRECTOR").IsOptional();
            Property(x => x.CustomerInfo).HasColumnName("CUSTOMERINFO").IsOptional().HasMaxLength(200);
            Property(x => x.CustomerInfoName).HasColumnName("CUSTOMERINFONAME").IsOptional().HasMaxLength(200);
            Property(x => x.ReserveWorkload).HasColumnName("RESERVEWORKLOAD").IsOptional().HasPrecision(18,2);
            Property(x => x.MileStoneList).HasColumnName("MILESTONELIST").IsOptional();
            Property(x => x.ManageWorkloadList).HasColumnName("MANAGEWORKLOADLIST").IsOptional();
            Property(x => x.ManageWorkloadSum).HasColumnName("MANAGEWORKLOADSUM").IsOptional().HasPrecision(18,2);
            Property(x => x.EnableMajorSum).HasColumnName("ENABLEMAJORSUM").IsOptional().HasPrecision(18,2);
            Property(x => x.MajorSum).HasColumnName("MAJORSUM").IsOptional().HasPrecision(18,2);
            Property(x => x.TaskWorkList).HasColumnName("TASKWORKLIST").IsOptional();
            Property(x => x.DeptLeader).HasColumnName("DEPTLEADER").IsOptional().HasMaxLength(2000);
            Property(x => x.DeptLeaderName).HasColumnName("DEPTLEADERNAME").IsOptional().HasMaxLength(2000);
        }
    }

    // T_SC_ElectricalPowerProjectScheme_MajorList
    internal partial class T_SC_ElectricalPowerProjectScheme_MajorListConfiguration : EntityTypeConfiguration<T_SC_ElectricalPowerProjectScheme_MajorList>
    {
        public T_SC_ElectricalPowerProjectScheme_MajorListConfiguration()
        {
			ToTable("T_SC_ELECTRICALPOWERPROJECTSCHEME_MAJORLIST");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.T_SC_ElectricalPowerProjectSchemeID).HasColumnName("T_SC_ELECTRICALPOWERPROJECTSCHEMEID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.MajorName).HasColumnName("MAJORNAME").IsOptional().HasMaxLength(200);
            Property(x => x.MajorCode).HasColumnName("MAJORCODE").IsOptional().HasMaxLength(200);
            Property(x => x.MajorPrinciple).HasColumnName("MAJORPRINCIPLE").IsOptional().HasMaxLength(200);
            Property(x => x.MajorPrincipleName).HasColumnName("MAJORPRINCIPLENAME").IsOptional().HasMaxLength(200);
            Property(x => x.Designer).HasColumnName("DESIGNER").IsOptional().HasMaxLength(200);
            Property(x => x.DesignerName).HasColumnName("DESIGNERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Collactor).HasColumnName("COLLACTOR").IsOptional().HasMaxLength(200);
            Property(x => x.CollactorName).HasColumnName("COLLACTORNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Auditor).HasColumnName("AUDITOR").IsOptional().HasMaxLength(200);
            Property(x => x.AuditorName).HasColumnName("AUDITORNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Approver).HasColumnName("APPROVER").IsOptional().HasMaxLength(200);
            Property(x => x.ApproverName).HasColumnName("APPROVERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.MajorWorkload).HasColumnName("MAJORWORKLOAD").IsOptional().HasPrecision(18,2);
            Property(x => x.Remark).HasColumnName("REMARK").IsOptional().HasMaxLength(200);
            Property(x => x.MajorEngineer).HasColumnName("MAJORENGINEER").IsOptional().HasMaxLength(200);
            Property(x => x.MajorEngineerName).HasColumnName("MAJORENGINEERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.MajorWorkloadRate).HasColumnName("MAJORWORKLOADRATE").IsOptional().HasPrecision(18,2);
            Property(x => x.SummaryBudgetQuantity).HasColumnName("SUMMARYBUDGETQUANTITY").IsOptional().HasPrecision(18,2);
            Property(x => x.SummaryCostQuantity).HasColumnName("SUMMARYCOSTQUANTITY").IsOptional().HasPrecision(18,2);
            Property(x => x.DeptLeader).HasColumnName("DEPTLEADER").IsOptional().HasMaxLength(200);
            Property(x => x.DeptLeaderName).HasColumnName("DEPTLEADERNAME").IsOptional().HasMaxLength(200);

            // Foreign keys
            HasOptional(a => a.T_SC_ElectricalPowerProjectScheme).WithMany(b => b.T_SC_ElectricalPowerProjectScheme_MajorList).HasForeignKey(c => c.T_SC_ElectricalPowerProjectSchemeID); // FK_T_SC_ElectricalPowerProjectScheme_MajorList_T_SC_ElectricalPowerProjectScheme
        }
    }

    // T_SC_ElectricalPowerProjectScheme_ManageWorkloadList
    internal partial class T_SC_ElectricalPowerProjectScheme_ManageWorkloadListConfiguration : EntityTypeConfiguration<T_SC_ElectricalPowerProjectScheme_ManageWorkloadList>
    {
        public T_SC_ElectricalPowerProjectScheme_ManageWorkloadListConfiguration()
        {
			ToTable("T_SC_ELECTRICALPOWERPROJECTSCHEME_MANAGEWORKLOADLIST");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.T_SC_ElectricalPowerProjectSchemeID).HasColumnName("T_SC_ELECTRICALPOWERPROJECTSCHEMEID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.ItemName).HasColumnName("ITEMNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Item).HasColumnName("ITEM").IsOptional().HasMaxLength(200);
            Property(x => x.ManageWorkloadRate).HasColumnName("MANAGEWORKLOADRATE").IsOptional().HasPrecision(18,2);
            Property(x => x.ManageWorkload).HasColumnName("MANAGEWORKLOAD").IsOptional().HasPrecision(18,2);
            Property(x => x.SummaryCostQuantity).HasColumnName("SUMMARYCOSTQUANTITY").IsOptional().HasPrecision(18,2);
            Property(x => x.Remark).HasColumnName("REMARK").IsOptional().HasMaxLength(200);

            // Foreign keys
            HasOptional(a => a.T_SC_ElectricalPowerProjectScheme).WithMany(b => b.T_SC_ElectricalPowerProjectScheme_ManageWorkloadList).HasForeignKey(c => c.T_SC_ElectricalPowerProjectSchemeID); // FK_T_SC_ElectricalPowerProjectScheme_ManageWorkloadList_T_SC_ElectricalPowerProjectScheme
        }
    }

    // T_SC_ElectricalPowerProjectScheme_MileStoneList
    internal partial class T_SC_ElectricalPowerProjectScheme_MileStoneListConfiguration : EntityTypeConfiguration<T_SC_ElectricalPowerProjectScheme_MileStoneList>
    {
        public T_SC_ElectricalPowerProjectScheme_MileStoneListConfiguration()
        {
			ToTable("T_SC_ELECTRICALPOWERPROJECTSCHEME_MILESTONELIST");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.T_SC_ElectricalPowerProjectSchemeID).HasColumnName("T_SC_ELECTRICALPOWERPROJECTSCHEMEID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(200);
            Property(x => x.Major).HasColumnName("MAJOR").IsOptional().HasMaxLength(200);
            Property(x => x.PlanEndDate).HasColumnName("PLANENDDATE").IsOptional();
            Property(x => x.Weight).HasColumnName("WEIGHT").IsOptional().HasPrecision(18,2);
            Property(x => x.Remark).HasColumnName("REMARK").IsOptional().HasMaxLength(200);
            Property(x => x.MileStoneType).HasColumnName("MILESTONETYPE").IsOptional().HasMaxLength(200);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(200);
            Property(x => x.TemplateID).HasColumnName("TEMPLATEID").IsOptional().HasMaxLength(200);
            Property(x => x.OutMajor).HasColumnName("OUTMAJOR").IsOptional().HasMaxLength(200);
            Property(x => x.InMajor).HasColumnName("INMAJOR").IsOptional().HasMaxLength(200);

            // Foreign keys
            HasOptional(a => a.T_SC_ElectricalPowerProjectScheme).WithMany(b => b.T_SC_ElectricalPowerProjectScheme_MileStoneList).HasForeignKey(c => c.T_SC_ElectricalPowerProjectSchemeID); // FK_T_SC_ElectricalPowerProjectScheme_MileStoneList_T_SC_ElectricalPowerProjectScheme
        }
    }

    // T_SC_ElectricalPowerProjectScheme_TaskWorkList
    internal partial class T_SC_ElectricalPowerProjectScheme_TaskWorkListConfiguration : EntityTypeConfiguration<T_SC_ElectricalPowerProjectScheme_TaskWorkList>
    {
        public T_SC_ElectricalPowerProjectScheme_TaskWorkListConfiguration()
        {
			ToTable("T_SC_ELECTRICALPOWERPROJECTSCHEME_TASKWORKLIST");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.T_SC_ElectricalPowerProjectSchemeID).HasColumnName("T_SC_ELECTRICALPOWERPROJECTSCHEMEID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(200);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(200);
            Property(x => x.Major).HasColumnName("MAJOR").IsOptional().HasMaxLength(200);
            Property(x => x.Phase).HasColumnName("PHASE").IsOptional().HasMaxLength(200);
            Property(x => x.Remark).HasColumnName("REMARK").IsOptional().HasMaxLength(200);
            Property(x => x.TaskWorkID).HasColumnName("TASKWORKID").IsOptional().HasMaxLength(50);
            Property(x => x.DossierName).HasColumnName("DOSSIERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.DossierCode).HasColumnName("DOSSIERCODE").IsOptional().HasMaxLength(200);

            // Foreign keys
            HasOptional(a => a.T_SC_ElectricalPowerProjectScheme).WithMany(b => b.T_SC_ElectricalPowerProjectScheme_TaskWorkList).HasForeignKey(c => c.T_SC_ElectricalPowerProjectSchemeID); // FK_T_SC_ElectricalPowerProjectScheme_TaskWorkList_T_SC_ElectricalPowerProjectScheme
        }
    }

    // T_SC_MajorDesignInput
    internal partial class T_SC_MajorDesignInputConfiguration : EntityTypeConfiguration<T_SC_MajorDesignInput>
    {
        public T_SC_MajorDesignInputConfiguration()
        {
			ToTable("T_SC_MAJORDESIGNINPUT");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.ModifyDate).HasColumnName("MODIFYDATE").IsOptional();
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUser).HasColumnName("MODIFYUSER").IsOptional().HasMaxLength(50);
            Property(x => x.OrgID).HasColumnName("ORGID").IsOptional().HasMaxLength(50);
            Property(x => x.CompanyID).HasColumnName("COMPANYID").IsOptional().HasMaxLength(50);
            Property(x => x.FlowPhase).HasColumnName("FLOWPHASE").IsOptional().HasMaxLength(50);
            Property(x => x.FlowInfo).HasColumnName("FLOWINFO").IsOptional().HasMaxLength(1073741823);
            Property(x => x.StepName).HasColumnName("STEPNAME").IsOptional().HasMaxLength(500);
            Property(x => x.ProjectInfoID).HasColumnName("PROJECTINFOID").IsOptional().HasMaxLength(50);
            Property(x => x.ProjectName).HasColumnName("PROJECTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectCode).HasColumnName("PROJECTCODE").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectManager).HasColumnName("PROJECTMANAGER").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectManagerName).HasColumnName("PROJECTMANAGERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ChargeDept).HasColumnName("CHARGEDEPT").IsOptional().HasMaxLength(200);
            Property(x => x.ChargeDeptName).HasColumnName("CHARGEDEPTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectClass).HasColumnName("PROJECTCLASS").IsOptional().HasMaxLength(200);
            Property(x => x.PhaseValue).HasColumnName("PHASEVALUE").IsOptional().HasMaxLength(200);
            Property(x => x.PlanStartDate).HasColumnName("PLANSTARTDATE").IsOptional();
            Property(x => x.PlanFinishDate).HasColumnName("PLANFINISHDATE").IsOptional();
            Property(x => x.DesignInputList).HasColumnName("DESIGNINPUTLIST").IsOptional();
            Property(x => x.VersionNumber).HasColumnName("VERSIONNUMBER").IsOptional().HasMaxLength(200);
            Property(x => x.MajorValue).HasColumnName("MAJORVALUE").IsOptional().HasMaxLength(200);
            Property(x => x.MajorPrinciple).HasColumnName("MAJORPRINCIPLE").IsOptional().HasMaxLength(200);
            Property(x => x.MajorPrincipleName).HasColumnName("MAJORPRINCIPLENAME").IsOptional().HasMaxLength(200);
            Property(x => x.SerialNumber).HasColumnName("SERIALNUMBER").IsOptional().HasMaxLength(200);
            Property(x => x.Auditor).HasColumnName("AUDITOR").IsOptional().HasMaxLength(200);
            Property(x => x.Approval).HasColumnName("APPROVAL").IsOptional().HasMaxLength(200);
        }
    }

    // T_SC_MajorDesignInput_DesignInputList
    internal partial class T_SC_MajorDesignInput_DesignInputListConfiguration : EntityTypeConfiguration<T_SC_MajorDesignInput_DesignInputList>
    {
        public T_SC_MajorDesignInput_DesignInputListConfiguration()
        {
			ToTable("T_SC_MAJORDESIGNINPUT_DESIGNINPUTLIST");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.T_SC_MajorDesignInputID).HasColumnName("T_SC_MAJORDESIGNINPUTID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.InputType).HasColumnName("INPUTTYPE").IsOptional().HasMaxLength(200);
            Property(x => x.InfoName).HasColumnName("INFONAME").IsOptional().HasMaxLength(200);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(200);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional().HasMaxLength(200);
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsOptional().HasMaxLength(200);
            Property(x => x.InputInfoID).HasColumnName("INPUTINFOID").IsOptional().HasMaxLength(200);
            Property(x => x.DocID).HasColumnName("DOCID").IsOptional().HasMaxLength(200);
            Property(x => x.Catagory).HasColumnName("CATAGORY").IsOptional().HasMaxLength(200);
            Property(x => x.Files).HasColumnName("FILES").IsOptional().HasMaxLength(500);

            // Foreign keys
            HasOptional(a => a.T_SC_MajorDesignInput).WithMany(b => b.T_SC_MajorDesignInput_DesignInputList).HasForeignKey(c => c.T_SC_MajorDesignInputID); // FK_T_SC_MajorDesignInput_DesignInputList_T_SC_MajorDesignInput
        }
    }

    // T_SC_MixProjectScheme
    internal partial class T_SC_MixProjectSchemeConfiguration : EntityTypeConfiguration<T_SC_MixProjectScheme>
    {
        public T_SC_MixProjectSchemeConfiguration()
        {
			ToTable("T_SC_MIXPROJECTSCHEME");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.ModifyDate).HasColumnName("MODIFYDATE").IsOptional();
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUser).HasColumnName("MODIFYUSER").IsOptional().HasMaxLength(50);
            Property(x => x.OrgID).HasColumnName("ORGID").IsOptional().HasMaxLength(50);
            Property(x => x.CompanyID).HasColumnName("COMPANYID").IsOptional().HasMaxLength(50);
            Property(x => x.FlowPhase).HasColumnName("FLOWPHASE").IsOptional().HasMaxLength(50);
            Property(x => x.FlowInfo).HasColumnName("FLOWINFO").IsOptional().HasMaxLength(1073741823);
            Property(x => x.StepName).HasColumnName("STEPNAME").IsOptional().HasMaxLength(500);
            Property(x => x.SerialNumber).HasColumnName("SERIALNUMBER").IsOptional().HasMaxLength(200);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(200);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(200);
            Property(x => x.PhaseValue).HasColumnName("PHASEVALUE").IsOptional().HasMaxLength(50);
            Property(x => x.PhaseName).HasColumnName("PHASENAME").IsOptional().HasMaxLength(200);
            Property(x => x.ChargeDept).HasColumnName("CHARGEDEPT").IsOptional().HasMaxLength(50);
            Property(x => x.ChargeDeptName).HasColumnName("CHARGEDEPTNAME").IsOptional().HasMaxLength(50);
            Property(x => x.ProjectClass).HasColumnName("PROJECTCLASS").IsOptional().HasMaxLength(200);
            Property(x => x.BuildArea).HasColumnName("BUILDAREA").IsOptional();
            Property(x => x.PlanStartDate).HasColumnName("PLANSTARTDATE").IsOptional();
            Property(x => x.PlanFinishDate).HasColumnName("PLANFINISHDATE").IsOptional();
            Property(x => x.Major).HasColumnName("MAJOR").IsOptional().HasMaxLength(500);
            Property(x => x.ChargeUser).HasColumnName("CHARGEUSER").IsOptional().HasMaxLength(500);
            Property(x => x.ChargeUserName).HasColumnName("CHARGEUSERNAME").IsOptional().HasMaxLength(500);
            Property(x => x.MajorList).HasColumnName("MAJORLIST").IsOptional();
            Property(x => x.SubProjectList).HasColumnName("SUBPROJECTLIST").IsOptional();
            Property(x => x.MileStoneList).HasColumnName("MILESTONELIST").IsOptional();
            Property(x => x.ProjectInfoID).HasColumnName("PROJECTINFOID").IsOptional().HasMaxLength(200);
            Property(x => x.VersionNumber).HasColumnName("VERSIONNUMBER").IsOptional().HasMaxLength(50);
            Property(x => x.Attachments).HasColumnName("ATTACHMENTS").IsOptional().HasMaxLength(500);
            Property(x => x.Remark).HasColumnName("REMARK").IsOptional().HasMaxLength(500);
            Property(x => x.ProjectLevel).HasColumnName("PROJECTLEVEL").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectLeader).HasColumnName("PROJECTLEADER").IsOptional();
            Property(x => x.Director).HasColumnName("DIRECTOR").IsOptional();
        }
    }

    // T_SC_MixProjectScheme_MajorList
    internal partial class T_SC_MixProjectScheme_MajorListConfiguration : EntityTypeConfiguration<T_SC_MixProjectScheme_MajorList>
    {
        public T_SC_MixProjectScheme_MajorListConfiguration()
        {
			ToTable("T_SC_MIXPROJECTSCHEME_MAJORLIST");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.T_SC_MixProjectSchemeID).HasColumnName("T_SC_MIXPROJECTSCHEMEID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.MajorName).HasColumnName("MAJORNAME").IsOptional().HasMaxLength(200);
            Property(x => x.MajorCode).HasColumnName("MAJORCODE").IsOptional().HasMaxLength(200);
            Property(x => x.MajorPrinciple).HasColumnName("MAJORPRINCIPLE").IsOptional().HasMaxLength(200);
            Property(x => x.MajorPrincipleName).HasColumnName("MAJORPRINCIPLENAME").IsOptional().HasMaxLength(200);
            Property(x => x.Designer).HasColumnName("DESIGNER").IsOptional().HasMaxLength(2000);
            Property(x => x.DesignerName).HasColumnName("DESIGNERNAME").IsOptional().HasMaxLength(2000);
            Property(x => x.Collactor).HasColumnName("COLLACTOR").IsOptional().HasMaxLength(2000);
            Property(x => x.CollactorName).HasColumnName("COLLACTORNAME").IsOptional().HasMaxLength(2000);
            Property(x => x.Auditor).HasColumnName("AUDITOR").IsOptional().HasMaxLength(2000);
            Property(x => x.AuditorName).HasColumnName("AUDITORNAME").IsOptional().HasMaxLength(2000);
            Property(x => x.Approver).HasColumnName("APPROVER").IsOptional().HasMaxLength(2000);
            Property(x => x.ApproverName).HasColumnName("APPROVERNAME").IsOptional().HasMaxLength(2000);
            Property(x => x.Remark).HasColumnName("REMARK").IsOptional().HasMaxLength(200);
            Property(x => x.MajorEngineer).HasColumnName("MAJORENGINEER").IsOptional().HasMaxLength(200);
            Property(x => x.MajorEngineerName).HasColumnName("MAJORENGINEERNAME").IsOptional().HasMaxLength(200);

            // Foreign keys
            HasOptional(a => a.T_SC_MixProjectScheme).WithMany(b => b.T_SC_MixProjectScheme_MajorList).HasForeignKey(c => c.T_SC_MixProjectSchemeID); // FK_T_SC_MixProjectScheme_MajorList_T_SC_MixProjectScheme
        }
    }

    // T_SC_MixProjectScheme_MileStoneList
    internal partial class T_SC_MixProjectScheme_MileStoneListConfiguration : EntityTypeConfiguration<T_SC_MixProjectScheme_MileStoneList>
    {
        public T_SC_MixProjectScheme_MileStoneListConfiguration()
        {
			ToTable("T_SC_MIXPROJECTSCHEME_MILESTONELIST");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.T_SC_MixProjectSchemeID).HasColumnName("T_SC_MIXPROJECTSCHEMEID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(200);
            Property(x => x.Major).HasColumnName("MAJOR").IsOptional().HasMaxLength(200);
            Property(x => x.PlanEndDate).HasColumnName("PLANENDDATE").IsOptional();
            Property(x => x.Weight).HasColumnName("WEIGHT").IsOptional().HasPrecision(18,2);
            Property(x => x.Remark).HasColumnName("REMARK").IsOptional().HasMaxLength(200);
            Property(x => x.MileStoneType).HasColumnName("MILESTONETYPE").IsOptional().HasMaxLength(200);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(200);
            Property(x => x.MileStoneID).HasColumnName("MILESTONEID").IsOptional().HasMaxLength(200);
            Property(x => x.OutMajor).HasColumnName("OUTMAJOR").IsOptional().HasMaxLength(200);
            Property(x => x.InMajor).HasColumnName("INMAJOR").IsOptional().HasMaxLength(200);
            Property(x => x.TemplateID).HasColumnName("TEMPLATEID").IsOptional().HasMaxLength(200);

            // Foreign keys
            HasOptional(a => a.T_SC_MixProjectScheme).WithMany(b => b.T_SC_MixProjectScheme_MileStoneList).HasForeignKey(c => c.T_SC_MixProjectSchemeID); // FK_T_SC_MixProjectScheme_MilestoneList_T_SC_MixProjectScheme
        }
    }

    // T_SC_MixProjectScheme_SubProjectList
    internal partial class T_SC_MixProjectScheme_SubProjectListConfiguration : EntityTypeConfiguration<T_SC_MixProjectScheme_SubProjectList>
    {
        public T_SC_MixProjectScheme_SubProjectListConfiguration()
        {
			ToTable("T_SC_MIXPROJECTSCHEME_SUBPROJECTLIST");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.T_SC_MixProjectSchemeID).HasColumnName("T_SC_MIXPROJECTSCHEMEID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(200);
            Property(x => x.PhaseValue).HasColumnName("PHASEVALUE").IsOptional().HasMaxLength(200);
            Property(x => x.Area).HasColumnName("AREA").IsOptional();
            Property(x => x.Unit).HasColumnName("UNIT").IsOptional().HasMaxLength(200);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(200);
            Property(x => x.RBSJson).HasColumnName("RBSJSON").IsOptional().HasMaxLength(1073741823);

            // Foreign keys
            HasOptional(a => a.T_SC_MixProjectScheme).WithMany(b => b.T_SC_MixProjectScheme_SubProjectList).HasForeignKey(c => c.T_SC_MixProjectSchemeID); // FK_T_SC_MixProjectScheme_SubProjectList_T_SC_MixProjectScheme
        }
    }

    // T_SC_PetrifactionProjectScheme
    internal partial class T_SC_PetrifactionProjectSchemeConfiguration : EntityTypeConfiguration<T_SC_PetrifactionProjectScheme>
    {
        public T_SC_PetrifactionProjectSchemeConfiguration()
        {
			ToTable("T_SC_PETRIFACTIONPROJECTSCHEME");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.ModifyDate).HasColumnName("MODIFYDATE").IsOptional();
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUser).HasColumnName("MODIFYUSER").IsOptional().HasMaxLength(50);
            Property(x => x.OrgID).HasColumnName("ORGID").IsOptional().HasMaxLength(50);
            Property(x => x.CompanyID).HasColumnName("COMPANYID").IsOptional().HasMaxLength(50);
            Property(x => x.FlowPhase).HasColumnName("FLOWPHASE").IsOptional().HasMaxLength(50);
            Property(x => x.FlowInfo).HasColumnName("FLOWINFO").IsOptional().HasMaxLength(1073741823);
            Property(x => x.StepName).HasColumnName("STEPNAME").IsOptional().HasMaxLength(500);
            Property(x => x.SerialNumber).HasColumnName("SERIALNUMBER").IsOptional().HasMaxLength(200);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(200);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(200);
            Property(x => x.PhaseValue).HasColumnName("PHASEVALUE").IsOptional().HasMaxLength(50);
            Property(x => x.PhaseName).HasColumnName("PHASENAME").IsOptional().HasMaxLength(200);
            Property(x => x.ChargeDept).HasColumnName("CHARGEDEPT").IsOptional().HasMaxLength(50);
            Property(x => x.ChargeDeptName).HasColumnName("CHARGEDEPTNAME").IsOptional().HasMaxLength(50);
            Property(x => x.ProjectClass).HasColumnName("PROJECTCLASS").IsOptional().HasMaxLength(200);
            Property(x => x.BuildArea).HasColumnName("BUILDAREA").IsOptional();
            Property(x => x.PlanStartDate).HasColumnName("PLANSTARTDATE").IsOptional();
            Property(x => x.PlanFinishDate).HasColumnName("PLANFINISHDATE").IsOptional();
            Property(x => x.Major).HasColumnName("MAJOR").IsOptional().HasMaxLength(500);
            Property(x => x.ChargeUser).HasColumnName("CHARGEUSER").IsOptional().HasMaxLength(500);
            Property(x => x.ChargeUserName).HasColumnName("CHARGEUSERNAME").IsOptional().HasMaxLength(500);
            Property(x => x.MajorList).HasColumnName("MAJORLIST").IsOptional();
            Property(x => x.ProjectInfoID).HasColumnName("PROJECTINFOID").IsOptional().HasMaxLength(200);
            Property(x => x.VersionNumber).HasColumnName("VERSIONNUMBER").IsOptional().HasMaxLength(50);
            Property(x => x.Attachments).HasColumnName("ATTACHMENTS").IsOptional().HasMaxLength(500);
            Property(x => x.Remark).HasColumnName("REMARK").IsOptional().HasMaxLength(500);
            Property(x => x.ProjectLevel).HasColumnName("PROJECTLEVEL").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectLeader).HasColumnName("PROJECTLEADER").IsOptional();
            Property(x => x.Director).HasColumnName("DIRECTOR").IsOptional();
        }
    }

    // T_SC_PetrifactionProjectScheme_MajorList
    internal partial class T_SC_PetrifactionProjectScheme_MajorListConfiguration : EntityTypeConfiguration<T_SC_PetrifactionProjectScheme_MajorList>
    {
        public T_SC_PetrifactionProjectScheme_MajorListConfiguration()
        {
			ToTable("T_SC_PETRIFACTIONPROJECTSCHEME_MAJORLIST");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.T_SC_PetrifactionProjectSchemeID).HasColumnName("T_SC_PETRIFACTIONPROJECTSCHEMEID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.MajorName).HasColumnName("MAJORNAME").IsOptional().HasMaxLength(200);
            Property(x => x.MajorCode).HasColumnName("MAJORCODE").IsOptional().HasMaxLength(200);
            Property(x => x.MajorPrinciple).HasColumnName("MAJORPRINCIPLE").IsOptional().HasMaxLength(200);
            Property(x => x.MajorPrincipleName).HasColumnName("MAJORPRINCIPLENAME").IsOptional().HasMaxLength(200);
            Property(x => x.Designer).HasColumnName("DESIGNER").IsOptional().HasMaxLength(2000);
            Property(x => x.DesignerName).HasColumnName("DESIGNERNAME").IsOptional().HasMaxLength(2000);
            Property(x => x.Collactor).HasColumnName("COLLACTOR").IsOptional().HasMaxLength(2000);
            Property(x => x.CollactorName).HasColumnName("COLLACTORNAME").IsOptional().HasMaxLength(2000);
            Property(x => x.Auditor).HasColumnName("AUDITOR").IsOptional().HasMaxLength(2000);
            Property(x => x.AuditorName).HasColumnName("AUDITORNAME").IsOptional().HasMaxLength(2000);
            Property(x => x.Approver).HasColumnName("APPROVER").IsOptional().HasMaxLength(2000);
            Property(x => x.ApproverName).HasColumnName("APPROVERNAME").IsOptional().HasMaxLength(2000);
            Property(x => x.Remark).HasColumnName("REMARK").IsOptional().HasMaxLength(200);
            Property(x => x.MajorEngineer).HasColumnName("MAJORENGINEER").IsOptional().HasMaxLength(200);
            Property(x => x.MajorEngineerName).HasColumnName("MAJORENGINEERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Mapper).HasColumnName("MAPPER").IsOptional().HasMaxLength(200);
            Property(x => x.MapperName).HasColumnName("MAPPERNAME").IsOptional().HasMaxLength(200);

            // Foreign keys
            HasOptional(a => a.T_SC_PetrifactionProjectScheme).WithMany(b => b.T_SC_PetrifactionProjectScheme_MajorList).HasForeignKey(c => c.T_SC_PetrifactionProjectSchemeID); // FK_T_SC_PetrifactionProjectScheme_MajorList_T_SC_PetrifactionProjectScheme
        }
    }

    // T_SC_SchemeForm
    internal partial class T_SC_SchemeFormConfiguration : EntityTypeConfiguration<T_SC_SchemeForm>
    {
        public T_SC_SchemeFormConfiguration()
        {
			ToTable("T_SC_SCHEMEFORM");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.ModifyDate).HasColumnName("MODIFYDATE").IsOptional();
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUser).HasColumnName("MODIFYUSER").IsOptional().HasMaxLength(50);
            Property(x => x.OrgID).HasColumnName("ORGID").IsOptional().HasMaxLength(50);
            Property(x => x.CompanyID).HasColumnName("COMPANYID").IsOptional().HasMaxLength(50);
            Property(x => x.FlowPhase).HasColumnName("FLOWPHASE").IsOptional().HasMaxLength(50);
            Property(x => x.FlowInfo).HasColumnName("FLOWINFO").IsOptional().HasMaxLength(1073741823);
            Property(x => x.StepName).HasColumnName("STEPNAME").IsOptional().HasMaxLength(500);
            Property(x => x.FormCode).HasColumnName("FORMCODE").IsOptional().HasMaxLength(200);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(200);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(200);
            Property(x => x.PhaseValue).HasColumnName("PHASEVALUE").IsOptional().HasMaxLength(50);
            Property(x => x.PhaseName).HasColumnName("PHASENAME").IsOptional().HasMaxLength(200);
            Property(x => x.ChargeDept).HasColumnName("CHARGEDEPT").IsOptional().HasMaxLength(50);
            Property(x => x.ChargeDeptName).HasColumnName("CHARGEDEPTNAME").IsOptional().HasMaxLength(50);
            Property(x => x.ProjectClass).HasColumnName("PROJECTCLASS").IsOptional().HasMaxLength(200);
            Property(x => x.BuildArea).HasColumnName("BUILDAREA").IsOptional();
            Property(x => x.PlanStartDate).HasColumnName("PLANSTARTDATE").IsOptional();
            Property(x => x.PlanFinishDate).HasColumnName("PLANFINISHDATE").IsOptional();
            Property(x => x.Major).HasColumnName("MAJOR").IsOptional().HasMaxLength(500);
            Property(x => x.ChargeUser).HasColumnName("CHARGEUSER").IsOptional().HasMaxLength(500);
            Property(x => x.ChargeUserName).HasColumnName("CHARGEUSERNAME").IsOptional().HasMaxLength(500);
            Property(x => x.MajorList).HasColumnName("MAJORLIST").IsOptional();
            Property(x => x.SubProjectList).HasColumnName("SUBPROJECTLIST").IsOptional();
            Property(x => x.ProjectInfoID).HasColumnName("PROJECTINFOID").IsOptional().HasMaxLength(200);
            Property(x => x.VersionNumber).HasColumnName("VERSIONNUMBER").IsOptional().HasMaxLength(50);
            Property(x => x.SerialNumber).HasColumnName("SERIALNUMBER").IsOptional().HasMaxLength(200);
            Property(x => x.Attachments).HasColumnName("ATTACHMENTS").IsOptional().HasMaxLength(500);
            Property(x => x.Remark).HasColumnName("REMARK").IsOptional().HasMaxLength(500);
            Property(x => x.ProjectLevel).HasColumnName("PROJECTLEVEL").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectLeader).HasColumnName("PROJECTLEADER").IsOptional();
            Property(x => x.Director).HasColumnName("DIRECTOR").IsOptional();
        }
    }

    // T_SC_SchemeForm_MajorList
    internal partial class T_SC_SchemeForm_MajorListConfiguration : EntityTypeConfiguration<T_SC_SchemeForm_MajorList>
    {
        public T_SC_SchemeForm_MajorListConfiguration()
        {
			ToTable("T_SC_SCHEMEFORM_MAJORLIST");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.T_SC_SchemeFormID).HasColumnName("T_SC_SCHEMEFORMID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.MajorName).HasColumnName("MAJORNAME").IsOptional().HasMaxLength(200);
            Property(x => x.MajorCode).HasColumnName("MAJORCODE").IsOptional().HasMaxLength(200);
            Property(x => x.MajorPrinciple).HasColumnName("MAJORPRINCIPLE").IsOptional().HasMaxLength(200);
            Property(x => x.MajorPrincipleName).HasColumnName("MAJORPRINCIPLENAME").IsOptional().HasMaxLength(200);
            Property(x => x.Designer).HasColumnName("DESIGNER").IsOptional().HasMaxLength(2000);
            Property(x => x.DesignerName).HasColumnName("DESIGNERNAME").IsOptional().HasMaxLength(2000);
            Property(x => x.Collactor).HasColumnName("COLLACTOR").IsOptional().HasMaxLength(2000);
            Property(x => x.CollactorName).HasColumnName("COLLACTORNAME").IsOptional().HasMaxLength(2000);
            Property(x => x.Auditor).HasColumnName("AUDITOR").IsOptional().HasMaxLength(2000);
            Property(x => x.AuditorName).HasColumnName("AUDITORNAME").IsOptional().HasMaxLength(2000);
            Property(x => x.Approver).HasColumnName("APPROVER").IsOptional().HasMaxLength(2000);
            Property(x => x.ApproverName).HasColumnName("APPROVERNAME").IsOptional().HasMaxLength(2000);
            Property(x => x.Remark).HasColumnName("REMARK").IsOptional().HasMaxLength(200);
            Property(x => x.MajorEngineer).HasColumnName("MAJORENGINEER").IsOptional().HasMaxLength(200);
            Property(x => x.MajorEngineerName).HasColumnName("MAJORENGINEERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Mapper).HasColumnName("MAPPER").IsOptional().HasMaxLength(200);
            Property(x => x.MapperName).HasColumnName("MAPPERNAME").IsOptional().HasMaxLength(200);

            // Foreign keys
            HasOptional(a => a.T_SC_SchemeForm).WithMany(b => b.T_SC_SchemeForm_MajorList).HasForeignKey(c => c.T_SC_SchemeFormID); // FK_T_SC_SchemeForm_MajorList_T_SC_SchemeForm
        }
    }

    // T_SC_SchemeForm_OEM_Szsow
    internal partial class T_SC_SchemeForm_OEM_SzsowConfiguration : EntityTypeConfiguration<T_SC_SchemeForm_OEM_Szsow>
    {
        public T_SC_SchemeForm_OEM_SzsowConfiguration()
        {
			ToTable("T_SC_SCHEMEFORM_OEM_SZSOW");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.ModifyDate).HasColumnName("MODIFYDATE").IsOptional();
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUser).HasColumnName("MODIFYUSER").IsOptional().HasMaxLength(50);
            Property(x => x.OrgID).HasColumnName("ORGID").IsOptional().HasMaxLength(50);
            Property(x => x.CompanyID).HasColumnName("COMPANYID").IsOptional().HasMaxLength(50);
            Property(x => x.FlowPhase).HasColumnName("FLOWPHASE").IsOptional().HasMaxLength(50);
            Property(x => x.FlowInfo).HasColumnName("FLOWINFO").IsOptional().HasMaxLength(1073741823);
            Property(x => x.StepName).HasColumnName("STEPNAME").IsOptional().HasMaxLength(500);
            Property(x => x.SerialNumber).HasColumnName("SERIALNUMBER").IsOptional().HasMaxLength(200);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(200);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(200);
            Property(x => x.PhaseValue).HasColumnName("PHASEVALUE").IsOptional().HasMaxLength(50);
            Property(x => x.PhaseName).HasColumnName("PHASENAME").IsOptional().HasMaxLength(200);
            Property(x => x.ChargeDept).HasColumnName("CHARGEDEPT").IsOptional().HasMaxLength(50);
            Property(x => x.ChargeDeptName).HasColumnName("CHARGEDEPTNAME").IsOptional().HasMaxLength(50);
            Property(x => x.ProjectClass).HasColumnName("PROJECTCLASS").IsOptional().HasMaxLength(200);
            Property(x => x.BuildArea).HasColumnName("BUILDAREA").IsOptional();
            Property(x => x.PlanStartDate).HasColumnName("PLANSTARTDATE").IsOptional();
            Property(x => x.PlanFinishDate).HasColumnName("PLANFINISHDATE").IsOptional();
            Property(x => x.Major).HasColumnName("MAJOR").IsOptional().HasMaxLength(500);
            Property(x => x.ChargeUser).HasColumnName("CHARGEUSER").IsOptional().HasMaxLength(500);
            Property(x => x.ChargeUserName).HasColumnName("CHARGEUSERNAME").IsOptional().HasMaxLength(500);
            Property(x => x.MajorList).HasColumnName("MAJORLIST").IsOptional();
            Property(x => x.SubProjectList).HasColumnName("SUBPROJECTLIST").IsOptional();
            Property(x => x.ProjectInfoID).HasColumnName("PROJECTINFOID").IsOptional().HasMaxLength(200);
            Property(x => x.VersionNumber).HasColumnName("VERSIONNUMBER").IsOptional().HasMaxLength(50);
            Property(x => x.Attachments).HasColumnName("ATTACHMENTS").IsOptional().HasMaxLength(500);
            Property(x => x.Remark).HasColumnName("REMARK").IsOptional().HasMaxLength(500);
            Property(x => x.ProjectLevel).HasColumnName("PROJECTLEVEL").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectLeader).HasColumnName("PROJECTLEADER").IsOptional();
            Property(x => x.Director).HasColumnName("DIRECTOR").IsOptional();
            Property(x => x.AlarmBeforeDays).HasColumnName("ALARMBEFOREDAYS").IsOptional();
        }
    }

    // T_SC_SchemeForm_OEM_Szsow_MajorList
    internal partial class T_SC_SchemeForm_OEM_Szsow_MajorListConfiguration : EntityTypeConfiguration<T_SC_SchemeForm_OEM_Szsow_MajorList>
    {
        public T_SC_SchemeForm_OEM_Szsow_MajorListConfiguration()
        {
			ToTable("T_SC_SCHEMEFORM_OEM_SZSOW_MAJORLIST");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.T_SC_SchemeForm_OEM_SzsowID).HasColumnName("T_SC_SCHEMEFORM_OEM_SZSOWID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.MajorName).HasColumnName("MAJORNAME").IsOptional().HasMaxLength(200);
            Property(x => x.MajorCode).HasColumnName("MAJORCODE").IsOptional().HasMaxLength(200);
            Property(x => x.MajorPrinciple).HasColumnName("MAJORPRINCIPLE").IsOptional().HasMaxLength(200);
            Property(x => x.MajorPrincipleName).HasColumnName("MAJORPRINCIPLENAME").IsOptional().HasMaxLength(200);
            Property(x => x.Designer).HasColumnName("DESIGNER").IsOptional().HasMaxLength(2000);
            Property(x => x.DesignerName).HasColumnName("DESIGNERNAME").IsOptional().HasMaxLength(2000);
            Property(x => x.Collactor).HasColumnName("COLLACTOR").IsOptional().HasMaxLength(2000);
            Property(x => x.CollactorName).HasColumnName("COLLACTORNAME").IsOptional().HasMaxLength(2000);
            Property(x => x.Auditor).HasColumnName("AUDITOR").IsOptional().HasMaxLength(2000);
            Property(x => x.AuditorName).HasColumnName("AUDITORNAME").IsOptional().HasMaxLength(2000);
            Property(x => x.Approver).HasColumnName("APPROVER").IsOptional().HasMaxLength(2000);
            Property(x => x.ApproverName).HasColumnName("APPROVERNAME").IsOptional().HasMaxLength(2000);
            Property(x => x.Remark).HasColumnName("REMARK").IsOptional().HasMaxLength(200);
            Property(x => x.MajorEngineer).HasColumnName("MAJORENGINEER").IsOptional().HasMaxLength(200);
            Property(x => x.MajorEngineerName).HasColumnName("MAJORENGINEERNAME").IsOptional().HasMaxLength(200);

            // Foreign keys
            HasOptional(a => a.T_SC_SchemeForm_OEM_Szsow).WithMany(b => b.T_SC_SchemeForm_OEM_Szsow_MajorList).HasForeignKey(c => c.T_SC_SchemeForm_OEM_SzsowID); // FK_T_SC_SchemeForm_OEM_Szsow_MajorList_T_SC_SchemeForm_OEM_Szsow
        }
    }

    // T_SC_SchemeForm_OEM_Szsow_SubProjectList
    internal partial class T_SC_SchemeForm_OEM_Szsow_SubProjectListConfiguration : EntityTypeConfiguration<T_SC_SchemeForm_OEM_Szsow_SubProjectList>
    {
        public T_SC_SchemeForm_OEM_Szsow_SubProjectListConfiguration()
        {
			ToTable("T_SC_SCHEMEFORM_OEM_SZSOW_SUBPROJECTLIST");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.T_SC_SchemeForm_OEM_SzsowID).HasColumnName("T_SC_SCHEMEFORM_OEM_SZSOWID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(200);
            Property(x => x.PhaseValue).HasColumnName("PHASEVALUE").IsOptional().HasMaxLength(200);
            Property(x => x.Area).HasColumnName("AREA").IsOptional();
            Property(x => x.Unit).HasColumnName("UNIT").IsOptional().HasMaxLength(200);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(200);
            Property(x => x.RBSJson).HasColumnName("RBSJSON").IsOptional().HasMaxLength(1073741823);
            Property(x => x.PhaseWBSID).HasColumnName("PHASEWBSID").IsOptional().HasMaxLength(200);
            Property(x => x.SubProjectWBSID).HasColumnName("SUBPROJECTWBSID").IsOptional().HasMaxLength(200);

            // Foreign keys
            HasOptional(a => a.T_SC_SchemeForm_OEM_Szsow).WithMany(b => b.T_SC_SchemeForm_OEM_Szsow_SubProjectList).HasForeignKey(c => c.T_SC_SchemeForm_OEM_SzsowID); // FK_T_SC_SchemeForm_OEM_Szsow_SubProjectList_T_SC_SchemeForm_OEM_Szsow
        }
    }

    // T_SC_SchemeForm_SubProjectList
    internal partial class T_SC_SchemeForm_SubProjectListConfiguration : EntityTypeConfiguration<T_SC_SchemeForm_SubProjectList>
    {
        public T_SC_SchemeForm_SubProjectListConfiguration()
        {
			ToTable("T_SC_SCHEMEFORM_SUBPROJECTLIST");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.T_SC_SchemeFormID).HasColumnName("T_SC_SCHEMEFORMID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(200);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(200);
            Property(x => x.PhaseValue).HasColumnName("PHASEVALUE").IsOptional().HasMaxLength(200);
            Property(x => x.Area).HasColumnName("AREA").IsOptional();
            Property(x => x.Unit).HasColumnName("UNIT").IsOptional().HasMaxLength(200);
            Property(x => x.RBSJson).HasColumnName("RBSJSON").IsOptional().HasMaxLength(1073741823);
            Property(x => x.WBSID).HasColumnName("WBSID").IsOptional().HasMaxLength(50);

            // Foreign keys
            HasOptional(a => a.T_SC_SchemeForm).WithMany(b => b.T_SC_SchemeForm_SubProjectList).HasForeignKey(c => c.T_SC_SchemeFormID); // FK_T_SC_SchemeForm_SubProjectList_T_SC_SchemeForm
        }
    }

    // T_SC_SimpleProjectSchmea
    internal partial class T_SC_SimpleProjectSchmeaConfiguration : EntityTypeConfiguration<T_SC_SimpleProjectSchmea>
    {
        public T_SC_SimpleProjectSchmeaConfiguration()
        {
			ToTable("T_SC_SIMPLEPROJECTSCHMEA");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.ModifyDate).HasColumnName("MODIFYDATE").IsOptional();
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUser).HasColumnName("MODIFYUSER").IsOptional().HasMaxLength(50);
            Property(x => x.OrgID).HasColumnName("ORGID").IsOptional().HasMaxLength(50);
            Property(x => x.CompanyID).HasColumnName("COMPANYID").IsOptional().HasMaxLength(50);
            Property(x => x.FlowPhase).HasColumnName("FLOWPHASE").IsOptional().HasMaxLength(50);
            Property(x => x.FlowInfo).HasColumnName("FLOWINFO").IsOptional().HasMaxLength(1073741823);
            Property(x => x.StepName).HasColumnName("STEPNAME").IsOptional().HasMaxLength(500);
            Property(x => x.ProjectInfoName).HasColumnName("PROJECTINFONAME").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectInfoCode).HasColumnName("PROJECTINFOCODE").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectManager).HasColumnName("PROJECTMANAGER").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectManagerName).HasColumnName("PROJECTMANAGERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Designer).HasColumnName("DESIGNER").IsOptional().HasMaxLength(500);
            Property(x => x.DesignerName).HasColumnName("DESIGNERNAME").IsOptional().HasMaxLength(500);
            Property(x => x.Collactor).HasColumnName("COLLACTOR").IsOptional().HasMaxLength(200);
            Property(x => x.CollactorName).HasColumnName("COLLACTORNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Auditor).HasColumnName("AUDITOR").IsOptional().HasMaxLength(200);
            Property(x => x.AuditorName).HasColumnName("AUDITORNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Approver).HasColumnName("APPROVER").IsOptional().HasMaxLength(200);
            Property(x => x.ApproverName).HasColumnName("APPROVERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Target).HasColumnName("TARGET").IsOptional().HasMaxLength(500);
            Property(x => x.Scope).HasColumnName("SCOPE").IsOptional().HasMaxLength(500);
            Property(x => x.DesignBasic).HasColumnName("DESIGNBASIC").IsOptional().HasMaxLength(500);
            Property(x => x.Content).HasColumnName("CONTENT").IsOptional().HasMaxLength(500);
            Property(x => x.TaskInfo).HasColumnName("TASKINFO").IsOptional().HasMaxLength(500);
            Property(x => x.MileStone).HasColumnName("MILESTONE").IsOptional();
            Property(x => x.ProjectManagerSign).HasColumnName("PROJECTMANAGERSIGN").IsOptional();
            Property(x => x.ProjectMemberSign).HasColumnName("PROJECTMEMBERSIGN").IsOptional();
            Property(x => x.Major).HasColumnName("MAJOR").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectInfoID).HasColumnName("PROJECTINFOID").IsOptional().HasMaxLength(200);
            Property(x => x.SerialNumber).HasColumnName("SERIALNUMBER").IsOptional().HasMaxLength(200);
            Property(x => x.VersionNumber).HasColumnName("VERSIONNUMBER").IsOptional().HasMaxLength(200);
            Property(x => x.PlanDate).HasColumnName("PLANDATE").IsOptional();
        }
    }

    // T_SC_SimpleProjectSchmea_MileStone
    internal partial class T_SC_SimpleProjectSchmea_MileStoneConfiguration : EntityTypeConfiguration<T_SC_SimpleProjectSchmea_MileStone>
    {
        public T_SC_SimpleProjectSchmea_MileStoneConfiguration()
        {
			ToTable("T_SC_SIMPLEPROJECTSCHMEA_MILESTONE");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.T_SC_SimpleProjectSchmeaID).HasColumnName("T_SC_SIMPLEPROJECTSCHMEAID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(200);
            Property(x => x.PlanStartDate).HasColumnName("PLANSTARTDATE").IsOptional();
            Property(x => x.PlanEndDate).HasColumnName("PLANENDDATE").IsOptional();
            Property(x => x.Content).HasColumnName("CONTENT").IsOptional().HasMaxLength(200);
            Property(x => x.MileStoneType).HasColumnName("MILESTONETYPE").IsOptional().HasMaxLength(200);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(200);
            Property(x => x.TemplateID).HasColumnName("TEMPLATEID").IsOptional().HasMaxLength(200);
            Property(x => x.OutMajor).HasColumnName("OUTMAJOR").IsOptional().HasMaxLength(200);
            Property(x => x.InMajor).HasColumnName("INMAJOR").IsOptional().HasMaxLength(200);

            // Foreign keys
            HasOptional(a => a.T_SC_SimpleProjectSchmea).WithMany(b => b.T_SC_SimpleProjectSchmea_MileStone).HasForeignKey(c => c.T_SC_SimpleProjectSchmeaID); // FK_T_SC_SimpleProjectSchmea_MileStone_T_SC_SimpleProjectSchmea
        }
    }

    // T_SC_SingleProjectScheme
    internal partial class T_SC_SingleProjectSchemeConfiguration : EntityTypeConfiguration<T_SC_SingleProjectScheme>
    {
        public T_SC_SingleProjectSchemeConfiguration()
        {
			ToTable("T_SC_SINGLEPROJECTSCHEME");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.ModifyDate).HasColumnName("MODIFYDATE").IsOptional();
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUser).HasColumnName("MODIFYUSER").IsOptional().HasMaxLength(50);
            Property(x => x.OrgID).HasColumnName("ORGID").IsOptional().HasMaxLength(50);
            Property(x => x.CompanyID).HasColumnName("COMPANYID").IsOptional().HasMaxLength(50);
            Property(x => x.FlowPhase).HasColumnName("FLOWPHASE").IsOptional().HasMaxLength(50);
            Property(x => x.FlowInfo).HasColumnName("FLOWINFO").IsOptional().HasMaxLength(1073741823);
            Property(x => x.StepName).HasColumnName("STEPNAME").IsOptional().HasMaxLength(500);
            Property(x => x.FormCode).HasColumnName("FORMCODE").IsOptional().HasMaxLength(200);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(200);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(200);
            Property(x => x.PhaseValue).HasColumnName("PHASEVALUE").IsOptional().HasMaxLength(50);
            Property(x => x.PhaseName).HasColumnName("PHASENAME").IsOptional().HasMaxLength(200);
            Property(x => x.ChargeDept).HasColumnName("CHARGEDEPT").IsOptional().HasMaxLength(50);
            Property(x => x.ChargeDeptName).HasColumnName("CHARGEDEPTNAME").IsOptional().HasMaxLength(50);
            Property(x => x.ProjectClass).HasColumnName("PROJECTCLASS").IsOptional().HasMaxLength(200);
            Property(x => x.BuildArea).HasColumnName("BUILDAREA").IsOptional();
            Property(x => x.PlanStartDate).HasColumnName("PLANSTARTDATE").IsOptional();
            Property(x => x.PlanFinishDate).HasColumnName("PLANFINISHDATE").IsOptional();
            Property(x => x.Major).HasColumnName("MAJOR").IsOptional().HasMaxLength(500);
            Property(x => x.ChargeUser).HasColumnName("CHARGEUSER").IsOptional().HasMaxLength(500);
            Property(x => x.ChargeUserName).HasColumnName("CHARGEUSERNAME").IsOptional().HasMaxLength(500);
            Property(x => x.MajorList).HasColumnName("MAJORLIST").IsOptional();
            Property(x => x.MileStoneList).HasColumnName("MILESTONELIST").IsOptional();
            Property(x => x.ProjectInfoID).HasColumnName("PROJECTINFOID").IsOptional().HasMaxLength(200);
            Property(x => x.VersionNumber).HasColumnName("VERSIONNUMBER").IsOptional().HasMaxLength(50);
            Property(x => x.Attachments).HasColumnName("ATTACHMENTS").IsOptional().HasMaxLength(500);
            Property(x => x.Remark).HasColumnName("REMARK").IsOptional().HasMaxLength(500);
            Property(x => x.SerialNumber).HasColumnName("SERIALNUMBER").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectLevel).HasColumnName("PROJECTLEVEL").IsOptional().HasMaxLength(200);
            Property(x => x.DesignRequired).HasColumnName("DESIGNREQUIRED").IsOptional().HasMaxLength(500);
            Property(x => x.DesignInput).HasColumnName("DESIGNINPUT").IsOptional().HasMaxLength(500);
            Property(x => x.DeptLeaderSign).HasColumnName("DEPTLEADERSIGN").IsOptional();
            Property(x => x.LeaderSign).HasColumnName("LEADERSIGN").IsOptional();
            Property(x => x.MainEngineerSign).HasColumnName("MAINENGINEERSIGN").IsOptional();
            Property(x => x.CheckManSign).HasColumnName("CHECKMANSIGN").IsOptional();
            Property(x => x.CustomerInfo).HasColumnName("CUSTOMERINFO").IsOptional().HasMaxLength(200);
            Property(x => x.CustomerInfoName).HasColumnName("CUSTOMERINFONAME").IsOptional().HasMaxLength(200);
            Property(x => x.test).HasColumnName("TEST").IsOptional().HasMaxLength(200);
            Property(x => x.testName).HasColumnName("TESTNAME").IsOptional().HasMaxLength(200);
        }
    }

    // T_SC_SingleProjectScheme_MajorList
    internal partial class T_SC_SingleProjectScheme_MajorListConfiguration : EntityTypeConfiguration<T_SC_SingleProjectScheme_MajorList>
    {
        public T_SC_SingleProjectScheme_MajorListConfiguration()
        {
			ToTable("T_SC_SINGLEPROJECTSCHEME_MAJORLIST");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.T_SC_SingleProjectSchemeID).HasColumnName("T_SC_SINGLEPROJECTSCHEMEID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.MajorName).HasColumnName("MAJORNAME").IsOptional().HasMaxLength(200);
            Property(x => x.MajorCode).HasColumnName("MAJORCODE").IsOptional().HasMaxLength(200);
            Property(x => x.MajorPrinciple).HasColumnName("MAJORPRINCIPLE").IsOptional().HasMaxLength(200);
            Property(x => x.MajorPrincipleName).HasColumnName("MAJORPRINCIPLENAME").IsOptional().HasMaxLength(200);
            Property(x => x.Designer).HasColumnName("DESIGNER").IsOptional().HasMaxLength(200);
            Property(x => x.DesignerName).HasColumnName("DESIGNERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Collactor).HasColumnName("COLLACTOR").IsOptional().HasMaxLength(200);
            Property(x => x.CollactorName).HasColumnName("COLLACTORNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Auditor).HasColumnName("AUDITOR").IsOptional().HasMaxLength(200);
            Property(x => x.AuditorName).HasColumnName("AUDITORNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Approver).HasColumnName("APPROVER").IsOptional().HasMaxLength(200);
            Property(x => x.ApproverName).HasColumnName("APPROVERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Remark).HasColumnName("REMARK").IsOptional().HasMaxLength(200);
            Property(x => x.MajorEngineer).HasColumnName("MAJORENGINEER").IsOptional().HasMaxLength(200);
            Property(x => x.MajorEngineerName).HasColumnName("MAJORENGINEERNAME").IsOptional().HasMaxLength(200);

            // Foreign keys
            HasOptional(a => a.T_SC_SingleProjectScheme).WithMany(b => b.T_SC_SingleProjectScheme_MajorList).HasForeignKey(c => c.T_SC_SingleProjectSchemeID); // FK_T_SC_SingleProjectScheme_MajorList_T_SC_SingleProjectScheme
        }
    }

    // T_SC_SingleProjectScheme_MileStoneList
    internal partial class T_SC_SingleProjectScheme_MileStoneListConfiguration : EntityTypeConfiguration<T_SC_SingleProjectScheme_MileStoneList>
    {
        public T_SC_SingleProjectScheme_MileStoneListConfiguration()
        {
			ToTable("T_SC_SINGLEPROJECTSCHEME_MILESTONELIST");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.T_SC_SingleProjectSchemeID).HasColumnName("T_SC_SINGLEPROJECTSCHEMEID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(200);
            Property(x => x.Major).HasColumnName("MAJOR").IsOptional().HasMaxLength(200);
            Property(x => x.PlanEndDate).HasColumnName("PLANENDDATE").IsOptional();
            Property(x => x.Weight).HasColumnName("WEIGHT").IsOptional().HasPrecision(18,2);
            Property(x => x.Remark).HasColumnName("REMARK").IsOptional().HasMaxLength(200);
            Property(x => x.MileStoneType).HasColumnName("MILESTONETYPE").IsOptional().HasMaxLength(200);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(200);
            Property(x => x.TemplateID).HasColumnName("TEMPLATEID").IsOptional().HasMaxLength(200);
            Property(x => x.OutMajor).HasColumnName("OUTMAJOR").IsOptional().HasMaxLength(200);
            Property(x => x.InMajor).HasColumnName("INMAJOR").IsOptional().HasMaxLength(200);

            // Foreign keys
            HasOptional(a => a.T_SC_SingleProjectScheme).WithMany(b => b.T_SC_SingleProjectScheme_MileStoneList).HasForeignKey(c => c.T_SC_SingleProjectSchemeID); // FK_T_SC_SingleProjectScheme_MileStoneList_T_SC_SingleProjectScheme
        }
    }

    // V_I_ProjectUserInfo
    internal partial class V_I_ProjectUserInfoConfiguration : EntityTypeConfiguration<V_I_ProjectUserInfo>
    {
        public V_I_ProjectUserInfoConfiguration()
        {
			ToTable("V_I_PROJECTUSERINFO");
            HasKey(x => new { x.State, x.CreateUserID, x.CreateDate, x.ID, x.ModeCode, x.Code, x.Name, x.CreateUser });

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.ModeCode).HasColumnName("MODECODE").IsRequired().HasMaxLength(50);
            Property(x => x.Name).HasColumnName("NAME").IsRequired().HasMaxLength(500);
            Property(x => x.Code).HasColumnName("CODE").IsRequired().HasMaxLength(500);
            Property(x => x.State).HasColumnName("STATE").IsRequired().HasMaxLength(50);
            Property(x => x.Status).HasColumnName("STATUS").IsOptional().HasMaxLength(50);
            Property(x => x.PhaseValue).HasColumnName("PHASEVALUE").IsOptional().HasMaxLength(50);
            Property(x => x.PhaseName).HasColumnName("PHASENAME").IsOptional().HasMaxLength(50);
            Property(x => x.Major).HasColumnName("MAJOR").IsOptional();
            Property(x => x.CustomerID).HasColumnName("CUSTOMERID").IsOptional().HasMaxLength(50);
            Property(x => x.CustomerName).HasColumnName("CUSTOMERNAME").IsOptional().HasMaxLength(500);
            Property(x => x.ChargeUserID).HasColumnName("CHARGEUSERID").IsOptional().HasMaxLength(500);
            Property(x => x.ChargeUserName).HasColumnName("CHARGEUSERNAME").IsOptional().HasMaxLength(500);
            Property(x => x.ChargeDeptID).HasColumnName("CHARGEDEPTID").IsOptional().HasMaxLength(200);
            Property(x => x.ChargeDeptName).HasColumnName("CHARGEDEPTNAME").IsOptional().HasMaxLength(500);
            Property(x => x.OtherDeptID).HasColumnName("OTHERDEPTID").IsOptional().HasMaxLength(500);
            Property(x => x.OtherDeptName).HasColumnName("OTHERDEPTNAME").IsOptional().HasMaxLength(500);
            Property(x => x.GroupRootID).HasColumnName("GROUPROOTID").IsOptional().HasMaxLength(200);
            Property(x => x.GroupID).HasColumnName("GROUPID").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectClass).HasColumnName("PROJECTCLASS").IsOptional().HasMaxLength(200);
            Property(x => x.Proportion).HasColumnName("PROPORTION").IsOptional().HasPrecision(18,4);
            Property(x => x.PlanStartDate).HasColumnName("PLANSTARTDATE").IsOptional();
            Property(x => x.PlanFinishDate).HasColumnName("PLANFINISHDATE").IsOptional();
            Property(x => x.FactStartDate).HasColumnName("FACTSTARTDATE").IsOptional();
            Property(x => x.FactFinishDate).HasColumnName("FACTFINISHDATE").IsOptional();
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsRequired().HasMaxLength(50);
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsRequired().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsRequired();
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUser).HasColumnName("MODIFYUSER").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyDate).HasColumnName("MODIFYDATE").IsOptional();
            Property(x => x.MarketProjectInfoID).HasColumnName("MARKETPROJECTINFOID").IsOptional().HasMaxLength(50);
            Property(x => x.EngineeringInfoID).HasColumnName("ENGINEERINGINFOID").IsOptional().HasMaxLength(50);
            Property(x => x.CompletePercent).HasColumnName("COMPLETEPERCENT").IsOptional().HasPrecision(18,2);
            Property(x => x.UserID).HasColumnName("USERID").IsOptional().HasMaxLength(50);
        }
    }

    // V_I_UserRBSInfo
    internal partial class V_I_UserRBSInfoConfiguration : EntityTypeConfiguration<V_I_UserRBSInfo>
    {
        public V_I_UserRBSInfoConfiguration()
        {
			ToTable("V_I_USERRBSINFO");
            HasKey(x => new { x.ProjectInfoID, x.RoleCode, x.WBSID, x.RoleName });

            Property(x => x.ProjectInfoID).HasColumnName("PROJECTINFOID").IsRequired().HasMaxLength(50);
            Property(x => x.WBSID).HasColumnName("WBSID").IsRequired().HasMaxLength(50);
            Property(x => x.RoleCode).HasColumnName("ROLECODE").IsRequired().HasMaxLength(50);
            Property(x => x.RoleName).HasColumnName("ROLENAME").IsRequired().HasMaxLength(50);
            Property(x => x.MajorValue).HasColumnName("MAJORVALUE").IsOptional().HasMaxLength(50);
            Property(x => x.UserIDs).HasColumnName("USERIDS").IsOptional();
            Property(x => x.UserNames).HasColumnName("USERNAMES").IsOptional();
        }
    }

    // V_P_MileStone
    internal partial class V_P_MileStoneConfiguration : EntityTypeConfiguration<V_P_MileStone>
    {
        public V_P_MileStoneConfiguration()
        {
			ToTable("V_P_MILESTONE");
            HasKey(x => new { x.ID, x.AlarmBeforeDays, x.State, x.ProjectInfoID, x.Name });

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.ProjectInfoID).HasColumnName("PROJECTINFOID").IsRequired().HasMaxLength(50);
            Property(x => x.WBSID).HasColumnName("WBSID").IsOptional().HasMaxLength(50);
            Property(x => x.TemplateID).HasColumnName("TEMPLATEID").IsOptional().HasMaxLength(50);
            Property(x => x.Name).HasColumnName("NAME").IsRequired().HasMaxLength(500);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(500);
            Property(x => x.Year).HasColumnName("YEAR").IsOptional();
            Property(x => x.Month).HasColumnName("MONTH").IsOptional();
            Property(x => x.Season).HasColumnName("SEASON").IsOptional();
            Property(x => x.DeptID).HasColumnName("DEPTID").IsOptional().HasMaxLength(500);
            Property(x => x.DeptName).HasColumnName("DEPTNAME").IsOptional().HasMaxLength(500);
            Property(x => x.MileStoneValue).HasColumnName("MILESTONEVALUE").IsOptional().HasMaxLength(500);
            Property(x => x.MileStoneType).HasColumnName("MILESTONETYPE").IsOptional().HasMaxLength(50);
            Property(x => x.PhaseValue).HasColumnName("PHASEVALUE").IsOptional().HasMaxLength(50);
            Property(x => x.MajorValue).HasColumnName("MAJORVALUE").IsOptional().HasMaxLength(500);
            Property(x => x.OutMajorValue).HasColumnName("OUTMAJORVALUE").IsOptional().HasMaxLength(500);
            Property(x => x.Necessity).HasColumnName("NECESSITY").IsOptional().HasMaxLength(50);
            Property(x => x.BindReceiptObjID).HasColumnName("BINDRECEIPTOBJID").IsOptional().HasMaxLength(500);
            Property(x => x.BindReceiptObjName).HasColumnName("BINDRECEIPTOBJNAME").IsOptional().HasMaxLength(500);
            Property(x => x.PlanStartDate).HasColumnName("PLANSTARTDATE").IsOptional();
            Property(x => x.PlanFinishDate).HasColumnName("PLANFINISHDATE").IsOptional();
            Property(x => x.OrlPlanStartDate).HasColumnName("ORLPLANSTARTDATE").IsOptional();
            Property(x => x.OrlPlanFinishDate).HasColumnName("ORLPLANFINISHDATE").IsOptional();
            Property(x => x.FactStartDate).HasColumnName("FACTSTARTDATE").IsOptional();
            Property(x => x.FactFinishDate).HasColumnName("FACTFINISHDATE").IsOptional();
            Property(x => x.Weight).HasColumnName("WEIGHT").IsOptional().HasPrecision(18,2);
            Property(x => x.State).HasColumnName("STATE").IsRequired().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.Description).HasColumnName("DESCRIPTION").IsOptional().HasMaxLength(500);
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsOptional().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUser).HasColumnName("MODIFYUSER").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyDate).HasColumnName("MODIFYDATE").IsOptional();
            Property(x => x.IncomingID).HasColumnName("INCOMINGID").IsOptional().HasMaxLength(50);
            Property(x => x.ApplyUserID).HasColumnName("APPLYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ApplyUserName).HasColumnName("APPLYUSERNAME").IsOptional().HasMaxLength(50);
            Property(x => x.ApplyState).HasColumnName("APPLYSTATE").IsOptional().HasMaxLength(50);
            Property(x => x.ApplyFiles).HasColumnName("APPLYFILES").IsOptional().HasMaxLength(500);
            Property(x => x.ApplyRemark).HasColumnName("APPLYREMARK").IsOptional().HasMaxLength(500);
            Property(x => x.ApplyDate).HasColumnName("APPLYDATE").IsOptional();
            Property(x => x.ApplyApprovalUserID).HasColumnName("APPLYAPPROVALUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ApplyApprovalUserName).HasColumnName("APPLYAPPROVALUSERNAME").IsOptional().HasMaxLength(50);
            Property(x => x.ApplyApprovalDate).HasColumnName("APPLYAPPROVALDATE").IsOptional();
            Property(x => x.IsChecked).HasColumnName("ISCHECKED").IsOptional().HasMaxLength(20);
            Property(x => x.ProjectInfoName).HasColumnName("PROJECTINFONAME").IsOptional().HasMaxLength(500);
            Property(x => x.AlarmBeforeDays).HasColumnName("ALARMBEFOREDAYS").IsRequired();
        }
    }

    // V_T_AE_Audit
    internal partial class V_T_AE_AuditConfiguration : EntityTypeConfiguration<V_T_AE_Audit>
    {
        public V_T_AE_AuditConfiguration()
        {
			ToTable("V_T_AE_AUDIT");
            HasKey(x => new { x.State, x.ProjectInfoID, x.ID, x.Name, x.WBSID });

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.ProjectInfoID).HasColumnName("PROJECTINFOID").IsRequired().HasMaxLength(50);
            Property(x => x.WBSID).HasColumnName("WBSID").IsRequired().HasMaxLength(50);
            Property(x => x.FullID).HasColumnName("FULLID").IsOptional().HasMaxLength(500);
            Property(x => x.FlowDefine).HasColumnName("FLOWDEFINE").IsOptional().HasMaxLength(500);
            Property(x => x.FlowID).HasColumnName("FLOWID").IsOptional().HasMaxLength(50);
            Property(x => x.Name).HasColumnName("NAME").IsRequired().HasMaxLength(500);
            Property(x => x.SubmitUserID).HasColumnName("SUBMITUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.SubmitUser).HasColumnName("SUBMITUSER").IsOptional().HasMaxLength(500);
            Property(x => x.DeptID).HasColumnName("DEPTID").IsOptional().HasMaxLength(500);
            Property(x => x.DeptName).HasColumnName("DEPTNAME").IsOptional().HasMaxLength(500);
            Property(x => x.DesignerID).HasColumnName("DESIGNERID").IsOptional().HasMaxLength(500);
            Property(x => x.DesignerName).HasColumnName("DESIGNERNAME").IsOptional().HasMaxLength(500);
            Property(x => x.CollactorID).HasColumnName("COLLACTORID").IsOptional().HasMaxLength(500);
            Property(x => x.CollactorName).HasColumnName("COLLACTORNAME").IsOptional().HasMaxLength(500);
            Property(x => x.AuditorID).HasColumnName("AUDITORID").IsOptional().HasMaxLength(500);
            Property(x => x.AuditorName).HasColumnName("AUDITORNAME").IsOptional().HasMaxLength(500);
            Property(x => x.ApproverID).HasColumnName("APPROVERID").IsOptional().HasMaxLength(500);
            Property(x => x.ApproverName).HasColumnName("APPROVERNAME").IsOptional().HasMaxLength(500);
            Property(x => x.State).HasColumnName("STATE").IsRequired().HasMaxLength(50);
            Property(x => x.CurrentTask).HasColumnName("CURRENTTASK").IsOptional().HasMaxLength(500);
            Property(x => x.CurrentUserID).HasColumnName("CURRENTUSERID").IsOptional().HasMaxLength(500);
            Property(x => x.CurrentUserName).HasColumnName("CURRENTUSERNAME").IsOptional().HasMaxLength(500);
            Property(x => x.ProjectInfoCode).HasColumnName("PROJECTINFOCODE").IsOptional().HasMaxLength(50);
            Property(x => x.ProjectInfoName).HasColumnName("PROJECTINFONAME").IsOptional().HasMaxLength(500);
            Property(x => x.PhaseCode).HasColumnName("PHASECODE").IsOptional().HasMaxLength(50);
            Property(x => x.PhaseName).HasColumnName("PHASENAME").IsOptional().HasMaxLength(500);
            Property(x => x.SubProjectCode).HasColumnName("SUBPROJECTCODE").IsOptional().HasMaxLength(50);
            Property(x => x.SubProjectName).HasColumnName("SUBPROJECTNAME").IsOptional().HasMaxLength(500);
            Property(x => x.MajorCode).HasColumnName("MAJORCODE").IsOptional().HasMaxLength(50);
            Property(x => x.MajorName).HasColumnName("MAJORNAME").IsOptional().HasMaxLength(500);
            Property(x => x.TaskWorkCode).HasColumnName("TASKWORKCODE").IsOptional().HasMaxLength(50);
            Property(x => x.TaskWorkName).HasColumnName("TASKWORKNAME").IsOptional().HasMaxLength(500);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(50);
            Property(x => x.Mistake).HasColumnName("MISTAKE").IsOptional();
            Property(x => x.TotalToA1).HasColumnName("TOTALTOA1").IsOptional();
            Property(x => x.Calculation).HasColumnName("CALCULATION").IsOptional();
            Property(x => x.Instruction).HasColumnName("INSTRUCTION").IsOptional();
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.FinishDate).HasColumnName("FINISHDATE").IsOptional();
            Property(x => x.CoSignState).HasColumnName("COSIGNSTATE").IsOptional().HasMaxLength(50);
            Property(x => x.CoSignFinishDate).HasColumnName("COSIGNFINISHDATE").IsOptional();
        }
    }

}

