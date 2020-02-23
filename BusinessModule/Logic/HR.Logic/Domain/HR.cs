

// This file was automatically generated.
// Do not make changes directly to this file - edit the template instead.
// 
// The following connection settings were used to generate this file
// 
//     Connection String Name: "HR"
//     Connection String:      "Data Source=.;Initial Catalog=EPM_HR;User ID=sa;PWD=123.zxc;"

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

namespace HR.Logic.Domain
{
    // ************************************************************************
    // Database context
    public partial class HREntities : Formula.FormulaDbContext
    {
        public IDbSet<R_W_AttendanceInfo> R_W_AttendanceInfo { get; set; } // R_W_AttendanceInfo
        public IDbSet<S_C_Award> S_C_Award { get; set; } // S_C_Award
        public IDbSet<S_C_AwardProject> S_C_AwardProject { get; set; } // S_C_AwardProject
        public IDbSet<S_C_Certificate> S_C_Certificate { get; set; } // S_C_Certificate
        public IDbSet<S_C_Patent> S_C_Patent { get; set; } // S_C_Patent
        public IDbSet<S_C_QualificationApplyLog> S_C_QualificationApplyLog { get; set; } // S_C_QualificationApplyLog
        public IDbSet<S_D_UserAptitude> S_D_UserAptitude { get; set; } // S_D_UserAptitude
        public IDbSet<S_D_UserAptitudeApply> S_D_UserAptitudeApply { get; set; } // S_D_UserAptitudeApply
        public IDbSet<S_P_Performance> S_P_Performance { get; set; } // S_P_Performance
        public IDbSet<S_P_Performance_JoinPeople> S_P_Performance_JoinPeople { get; set; } // S_P_Performance_JoinPeople
        public IDbSet<S_S_PostLevelTemplate> S_S_PostLevelTemplate { get; set; } // S_S_PostLevelTemplate
        public IDbSet<S_S_PostLevelTemplate_PostList> S_S_PostLevelTemplate_PostList { get; set; } // S_S_PostLevelTemplate_PostList
        public IDbSet<S_S_TaxThreshold> S_S_TaxThreshold { get; set; } // S_S_TaxThreshold
        public IDbSet<S_W_AttendanceAnnualLeave> S_W_AttendanceAnnualLeave { get; set; } // S_W_AttendanceAnnualLeave
        public IDbSet<S_W_AttendanceConfig> S_W_AttendanceConfig { get; set; } // S_W_AttendanceConfig
        public IDbSet<S_W_AttendanceEditLog> S_W_AttendanceEditLog { get; set; } // S_W_AttendanceEditLog
        public IDbSet<S_W_AttendanceInfo> S_W_AttendanceInfo { get; set; } // S_W_AttendanceInfo
        public IDbSet<S_W_AttendanceInfoMultiEdit> S_W_AttendanceInfoMultiEdit { get; set; } // S_W_AttendanceInfoMultiEdit
        public IDbSet<S_W_DefineUserActualUnitPrice> S_W_DefineUserActualUnitPrice { get; set; } // S_W_DefineUserActualUnitPrice
        public IDbSet<S_W_ProjectInfo> S_W_ProjectInfo { get; set; } // S_W_ProjectInfo
        public IDbSet<S_W_ProjectInfo_SubProjectInfo> S_W_ProjectInfo_SubProjectInfo { get; set; } // S_W_ProjectInfo_SubProjectInfo
        public IDbSet<S_W_ResourcePrice> S_W_ResourcePrice { get; set; } // S_W_ResourcePrice
        public IDbSet<S_W_UserSalary> S_W_UserSalary { get; set; } // S_W_UserSalary
        public IDbSet<S_W_UserUnitPrice> S_W_UserUnitPrice { get; set; } // S_W_UserUnitPrice
        public IDbSet<S_W_UserWorkHour> S_W_UserWorkHour { get; set; } // S_W_UserWorkHour
        public IDbSet<S_W_UserWorkHour_ApproveDetail> S_W_UserWorkHour_ApproveDetail { get; set; } // S_W_UserWorkHour_ApproveDetail
        public IDbSet<S_W_UserWorkHourSupplement> S_W_UserWorkHourSupplement { get; set; } // S_W_UserWorkHourSupplement
        public IDbSet<T_AttendanceBusinessApply> T_AttendanceBusinessApply { get; set; } // T_AttendanceBusinessApply
        public IDbSet<T_AttendanceLeaveApply> T_AttendanceLeaveApply { get; set; } // T_AttendanceLeaveApply
        public IDbSet<T_C_QualificationApply> T_C_QualificationApply { get; set; } // T_C_QualificationApply
        public IDbSet<T_C_QualificationApply_BorrowInfo> T_C_QualificationApply_BorrowInfo { get; set; } // T_C_QualificationApply_BorrowInfo
        public IDbSet<T_D_UserAptitudeApply> T_D_UserAptitudeApply { get; set; } // T_D_UserAptitudeApply
        public IDbSet<T_D_UserAptitudeApply_ApplyInfo> T_D_UserAptitudeApply_ApplyInfo { get; set; } // T_D_UserAptitudeApply_ApplyInfo
        public IDbSet<T_Employee> T_Employee { get; set; } // T_Employee
        public IDbSet<T_EmployeeAcademicDegree> T_EmployeeAcademicDegree { get; set; } // T_EmployeeAcademicDegree
        public IDbSet<T_EmployeeAcademicTitle> T_EmployeeAcademicTitle { get; set; } // T_EmployeeAcademicTitle
        public IDbSet<T_EmployeeBaseSet> T_EmployeeBaseSet { get; set; } // T_EmployeeBaseSet
        public IDbSet<T_EmployeeContract> T_EmployeeContract { get; set; } // T_EmployeeContract
        public IDbSet<T_EmployeeHonour> T_EmployeeHonour { get; set; } // T_EmployeeHonour
        public IDbSet<T_EmployeeJob> T_EmployeeJob { get; set; } // T_EmployeeJob
        public IDbSet<T_EmployeeJobChange> T_EmployeeJobChange { get; set; } // T_EmployeeJobChange
        public IDbSet<T_EmployeeJobChange_ChangeJobList> T_EmployeeJobChange_ChangeJobList { get; set; } // T_EmployeeJobChange_ChangeJobList
        public IDbSet<T_EmployeeJobChange_CurrentJobList> T_EmployeeJobChange_CurrentJobList { get; set; } // T_EmployeeJobChange_CurrentJobList
        public IDbSet<T_EmployeePersonalRecords> T_EmployeePersonalRecords { get; set; } // T_EmployeePersonalRecords
        public IDbSet<T_EmployeeQualification> T_EmployeeQualification { get; set; } // T_EmployeeQualification
        public IDbSet<T_EmployeeRetired> T_EmployeeRetired { get; set; } // T_EmployeeRetired
        public IDbSet<T_EmployeeSocialSecurity> T_EmployeeSocialSecurity { get; set; } // T_EmployeeSocialSecurity
        public IDbSet<T_EmployeeWorkHistory> T_EmployeeWorkHistory { get; set; } // T_EmployeeWorkHistory
        public IDbSet<T_EmployeeWorkPerformance> T_EmployeeWorkPerformance { get; set; } // T_EmployeeWorkPerformance
        public IDbSet<T_EmployeeWorkPost> T_EmployeeWorkPost { get; set; } // T_EmployeeWorkPost
        public IDbSet<V_All_Attendance> V_All_Attendance { get; set; } // V_All_Attendance
        public IDbSet<V_All_AttendanceInfo> V_All_AttendanceInfo { get; set; } // V_All_AttendanceInfo
        public IDbSet<V_S_D_UserAptitude> V_S_D_UserAptitude { get; set; } // V_S_D_UserAptitude
        public IDbSet<V_S_W_AttendanceEditLog> V_S_W_AttendanceEditLog { get; set; } // V_S_W_AttendanceEditLog
        public IDbSet<V_S_W_AttendanceInfo> V_S_W_AttendanceInfo { get; set; } // V_S_W_AttendanceInfo
        public IDbSet<V_T_QualificationNoReturn> V_T_QualificationNoReturn { get; set; } // V_T_QualificationNoReturn
        public IDbSet<V_T_QualificationReturn> V_T_QualificationReturn { get; set; } // V_T_QualificationReturn
        public IDbSet<V_T_WIFixedSalary> V_T_WIFixedSalary { get; set; } // V_T_WIFixedSalary
        public IDbSet<V_T_WIFundPay> V_T_WIFundPay { get; set; } // V_T_WIFundPay
        public IDbSet<V_T_WISocialSecurityPay> V_T_WISocialSecurityPay { get; set; } // V_T_WISocialSecurityPay

        static HREntities()
        {
            Database.SetInitializer<HREntities>(null);
        }

        public HREntities()
            : base("Name=HR")
        {
        }

        public HREntities(string connectionString) : base(connectionString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new R_W_AttendanceInfoConfiguration());
            modelBuilder.Configurations.Add(new S_C_AwardConfiguration());
            modelBuilder.Configurations.Add(new S_C_AwardProjectConfiguration());
            modelBuilder.Configurations.Add(new S_C_CertificateConfiguration());
            modelBuilder.Configurations.Add(new S_C_PatentConfiguration());
            modelBuilder.Configurations.Add(new S_C_QualificationApplyLogConfiguration());
            modelBuilder.Configurations.Add(new S_D_UserAptitudeConfiguration());
            modelBuilder.Configurations.Add(new S_D_UserAptitudeApplyConfiguration());
            modelBuilder.Configurations.Add(new S_P_PerformanceConfiguration());
            modelBuilder.Configurations.Add(new S_P_Performance_JoinPeopleConfiguration());
            modelBuilder.Configurations.Add(new S_S_PostLevelTemplateConfiguration());
            modelBuilder.Configurations.Add(new S_S_PostLevelTemplate_PostListConfiguration());
            modelBuilder.Configurations.Add(new S_S_TaxThresholdConfiguration());
            modelBuilder.Configurations.Add(new S_W_AttendanceAnnualLeaveConfiguration());
            modelBuilder.Configurations.Add(new S_W_AttendanceConfigConfiguration());
            modelBuilder.Configurations.Add(new S_W_AttendanceEditLogConfiguration());
            modelBuilder.Configurations.Add(new S_W_AttendanceInfoConfiguration());
            modelBuilder.Configurations.Add(new S_W_AttendanceInfoMultiEditConfiguration());
            modelBuilder.Configurations.Add(new S_W_DefineUserActualUnitPriceConfiguration());
            modelBuilder.Configurations.Add(new S_W_ProjectInfoConfiguration());
            modelBuilder.Configurations.Add(new S_W_ProjectInfo_SubProjectInfoConfiguration());
            modelBuilder.Configurations.Add(new S_W_ResourcePriceConfiguration());
            modelBuilder.Configurations.Add(new S_W_UserSalaryConfiguration());
            modelBuilder.Configurations.Add(new S_W_UserUnitPriceConfiguration());
            modelBuilder.Configurations.Add(new S_W_UserWorkHourConfiguration());
            modelBuilder.Configurations.Add(new S_W_UserWorkHour_ApproveDetailConfiguration());
            modelBuilder.Configurations.Add(new S_W_UserWorkHourSupplementConfiguration());
            modelBuilder.Configurations.Add(new T_AttendanceBusinessApplyConfiguration());
            modelBuilder.Configurations.Add(new T_AttendanceLeaveApplyConfiguration());
            modelBuilder.Configurations.Add(new T_C_QualificationApplyConfiguration());
            modelBuilder.Configurations.Add(new T_C_QualificationApply_BorrowInfoConfiguration());
            modelBuilder.Configurations.Add(new T_D_UserAptitudeApplyConfiguration());
            modelBuilder.Configurations.Add(new T_D_UserAptitudeApply_ApplyInfoConfiguration());
            modelBuilder.Configurations.Add(new T_EmployeeConfiguration());
            modelBuilder.Configurations.Add(new T_EmployeeAcademicDegreeConfiguration());
            modelBuilder.Configurations.Add(new T_EmployeeAcademicTitleConfiguration());
            modelBuilder.Configurations.Add(new T_EmployeeBaseSetConfiguration());
            modelBuilder.Configurations.Add(new T_EmployeeContractConfiguration());
            modelBuilder.Configurations.Add(new T_EmployeeHonourConfiguration());
            modelBuilder.Configurations.Add(new T_EmployeeJobConfiguration());
            modelBuilder.Configurations.Add(new T_EmployeeJobChangeConfiguration());
            modelBuilder.Configurations.Add(new T_EmployeeJobChange_ChangeJobListConfiguration());
            modelBuilder.Configurations.Add(new T_EmployeeJobChange_CurrentJobListConfiguration());
            modelBuilder.Configurations.Add(new T_EmployeePersonalRecordsConfiguration());
            modelBuilder.Configurations.Add(new T_EmployeeQualificationConfiguration());
            modelBuilder.Configurations.Add(new T_EmployeeRetiredConfiguration());
            modelBuilder.Configurations.Add(new T_EmployeeSocialSecurityConfiguration());
            modelBuilder.Configurations.Add(new T_EmployeeWorkHistoryConfiguration());
            modelBuilder.Configurations.Add(new T_EmployeeWorkPerformanceConfiguration());
            modelBuilder.Configurations.Add(new T_EmployeeWorkPostConfiguration());
            modelBuilder.Configurations.Add(new V_All_AttendanceConfiguration());
            modelBuilder.Configurations.Add(new V_All_AttendanceInfoConfiguration());
            modelBuilder.Configurations.Add(new V_S_D_UserAptitudeConfiguration());
            modelBuilder.Configurations.Add(new V_S_W_AttendanceEditLogConfiguration());
            modelBuilder.Configurations.Add(new V_S_W_AttendanceInfoConfiguration());
            modelBuilder.Configurations.Add(new V_T_QualificationNoReturnConfiguration());
            modelBuilder.Configurations.Add(new V_T_QualificationReturnConfiguration());
            modelBuilder.Configurations.Add(new V_T_WIFixedSalaryConfiguration());
            modelBuilder.Configurations.Add(new V_T_WIFundPayConfiguration());
            modelBuilder.Configurations.Add(new V_T_WISocialSecurityPayConfiguration());
        }
    }

    // ************************************************************************
    // POCO classes

	/// <summary>功能_考勤管理_考勤信息</summary>	
	[Description("功能_考勤管理_考勤信息")]
    public partial class R_W_AttendanceInfo : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID (Primary key)
		private string _ID="";
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? ModifyDate { get; set; } // ModifyDate
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get{return _CreateUserID;} set{_CreateUserID = value??"";} } // CreateUserID
		private string _CreateUserID="";
		/// <summary></summary>	
		[Description("")]
        public string CreateUser { get{return _CreateUser;} set{_CreateUser = value??"";} } // CreateUser
		private string _CreateUser="";
		/// <summary></summary>	
		[Description("")]
        public string ModifyUserID { get{return _ModifyUserID;} set{_ModifyUserID = value??"";} } // ModifyUserID
		private string _ModifyUserID="";
		/// <summary></summary>	
		[Description("")]
        public string ModifyUser { get{return _ModifyUser;} set{_ModifyUser = value??"";} } // ModifyUser
		private string _ModifyUser="";
		/// <summary></summary>	
		[Description("")]
        public string OrgID { get{return _OrgID;} set{_OrgID = value??"";} } // OrgID
		private string _OrgID="";
		/// <summary></summary>	
		[Description("")]
        public string CompanyID { get{return _CompanyID;} set{_CompanyID = value??"";} } // CompanyID
		private string _CompanyID="";
		/// <summary></summary>	
		[Description("")]
        public string FlowPhase { get{return _FlowPhase;} set{_FlowPhase = value??"";} } // FlowPhase
		private string _FlowPhase="";
		/// <summary></summary>	
		[Description("")]
        public string FlowInfo { get{return _FlowInfo;} set{_FlowInfo = value??"";} } // FlowInfo
		private string _FlowInfo="";
		/// <summary></summary>	
		[Description("")]
        public string StepName { get{return _StepName;} set{_StepName = value??"";} } // StepName
		private string _StepName="";
		/// <summary>姓名</summary>	
		[Description("姓名")]
        public string UserID { get{return _UserID;} set{_UserID = value??"";} } // UserID
		private string _UserID="";
		/// <summary>姓名名称</summary>	
		[Description("姓名名称")]
        public string UserIDName { get{return _UserIDName;} set{_UserIDName = value??"";} } // UserIDName
		private string _UserIDName="";
		/// <summary>年度</summary>	
		[Description("年度")]
        public int? Year { get; set; } // Year
		/// <summary>月份</summary>	
		[Description("月份")]
        public int? Month { get; set; } // Month
		/// <summary>日期</summary>	
		[Description("日期")]
        public DateTime? Date { get; set; } // Date
		/// <summary>上午</summary>	
		[Description("上午")]
        public string Morning { get{return _Morning;} set{_Morning = value??"";} } // Morning
		private string _Morning="";
		/// <summary>上午请假类别</summary>	
		[Description("上午请假类别")]
        public string MorningType { get{return _MorningType;} set{_MorningType = value??"";} } // MorningType
		private string _MorningType="";
		/// <summary>到岗时间</summary>	
		[Description("到岗时间")]
        public DateTime? PostTime { get; set; } // PostTime
		/// <summary>下午</summary>	
		[Description("下午")]
        public string Afternoon { get{return _Afternoon;} set{_Afternoon = value??"";} } // Afternoon
		private string _Afternoon="";
		/// <summary>下午请假类别</summary>	
		[Description("下午请假类别")]
        public string AfternoonType { get{return _AfternoonType;} set{_AfternoonType = value??"";} } // AfternoonType
		private string _AfternoonType="";
		/// <summary>离岗时间</summary>	
		[Description("离岗时间")]
        public DateTime? LeaveTime { get; set; } // LeaveTime
    }

	/// <summary>企业获奖</summary>	
	[Description("企业获奖")]
    public partial class S_C_Award : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID (Primary key)
		private string _ID="";
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? ModifyDate { get; set; } // ModifyDate
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get{return _CreateUserID;} set{_CreateUserID = value??"";} } // CreateUserID
		private string _CreateUserID="";
		/// <summary></summary>	
		[Description("")]
        public string CreateUser { get{return _CreateUser;} set{_CreateUser = value??"";} } // CreateUser
		private string _CreateUser="";
		/// <summary></summary>	
		[Description("")]
        public string ModifyUserID { get{return _ModifyUserID;} set{_ModifyUserID = value??"";} } // ModifyUserID
		private string _ModifyUserID="";
		/// <summary></summary>	
		[Description("")]
        public string ModifyUser { get{return _ModifyUser;} set{_ModifyUser = value??"";} } // ModifyUser
		private string _ModifyUser="";
		/// <summary></summary>	
		[Description("")]
        public string OrgID { get{return _OrgID;} set{_OrgID = value??"";} } // OrgID
		private string _OrgID="";
		/// <summary></summary>	
		[Description("")]
        public string CompanyID { get{return _CompanyID;} set{_CompanyID = value??"";} } // CompanyID
		private string _CompanyID="";
		/// <summary></summary>	
		[Description("")]
        public string FlowPhase { get{return _FlowPhase;} set{_FlowPhase = value??"";} } // FlowPhase
		private string _FlowPhase="";
		/// <summary></summary>	
		[Description("")]
        public string FlowInfo { get{return _FlowInfo;} set{_FlowInfo = value??"";} } // FlowInfo
		private string _FlowInfo="";
		/// <summary></summary>	
		[Description("")]
        public string StepName { get{return _StepName;} set{_StepName = value??"";} } // StepName
		private string _StepName="";
		/// <summary>登记号</summary>	
		[Description("登记号")]
        public string SerialNumber { get{return _SerialNumber;} set{_SerialNumber = value??"";} } // SerialNumber
		private string _SerialNumber="";
		/// <summary>获奖年度</summary>	
		[Description("获奖年度")]
        public string AwardYear { get{return _AwardYear;} set{_AwardYear = value??"";} } // AwardYear
		private string _AwardYear="";
		/// <summary>奖励名称</summary>	
		[Description("奖励名称")]
        public string Name { get{return _Name;} set{_Name = value??"";} } // Name
		private string _Name="";
		/// <summary>奖励等级</summary>	
		[Description("奖励等级")]
        public string AwardLevel { get{return _AwardLevel;} set{_AwardLevel = value??"";} } // AwardLevel
		private string _AwardLevel="";
		/// <summary>奖励级别</summary>	
		[Description("奖励级别")]
        public string AwardGrade { get{return _AwardGrade;} set{_AwardGrade = value??"";} } // AwardGrade
		private string _AwardGrade="";
		/// <summary>奖励类型</summary>	
		[Description("奖励类型")]
        public string AwardType { get{return _AwardType;} set{_AwardType = value??"";} } // AwardType
		private string _AwardType="";
		/// <summary>获奖人员</summary>	
		[Description("获奖人员")]
        public string CompletePerson { get{return _CompletePerson;} set{_CompletePerson = value??"";} } // CompletePerson
		private string _CompletePerson="";
		/// <summary>获奖人员名称</summary>	
		[Description("获奖人员名称")]
        public string CompletePersonName { get{return _CompletePersonName;} set{_CompletePersonName = value??"";} } // CompletePersonName
		private string _CompletePersonName="";
		/// <summary>完成单位</summary>	
		[Description("完成单位")]
        public string CompleteUnit { get{return _CompleteUnit;} set{_CompleteUnit = value??"";} } // CompleteUnit
		private string _CompleteUnit="";
		/// <summary>颁奖时间</summary>	
		[Description("颁奖时间")]
        public DateTime? AwardDate { get; set; } // AwardDate
		/// <summary>奖励金额(元)</summary>	
		[Description("奖励金额(元)")]
        public string AwardAmount { get{return _AwardAmount;} set{_AwardAmount = value??"";} } // AwardAmount
		private string _AwardAmount="";
		/// <summary>颁奖单位</summary>	
		[Description("颁奖单位")]
        public string AwardAUnit { get{return _AwardAUnit;} set{_AwardAUnit = value??"";} } // AwardAUnit
		private string _AwardAUnit="";
		/// <summary>内部其它参与人</summary>	
		[Description("内部其它参与人")]
        public string InsideAffiliatedPerson { get{return _InsideAffiliatedPerson;} set{_InsideAffiliatedPerson = value??"";} } // InsideAffiliatedPerson
		private string _InsideAffiliatedPerson="";
		/// <summary>内部其它参与人名称</summary>	
		[Description("内部其它参与人名称")]
        public string InsideAffiliatedPersonName { get{return _InsideAffiliatedPersonName;} set{_InsideAffiliatedPersonName = value??"";} } // InsideAffiliatedPersonName
		private string _InsideAffiliatedPersonName="";
		/// <summary>外部人员</summary>	
		[Description("外部人员")]
        public string OutsideAffiliatedPerson { get{return _OutsideAffiliatedPerson;} set{_OutsideAffiliatedPerson = value??"";} } // OutsideAffiliatedPerson
		private string _OutsideAffiliatedPerson="";
		/// <summary>证书扫描件</summary>	
		[Description("证书扫描件")]
        public string Attachment { get{return _Attachment;} set{_Attachment = value??"";} } // Attachment
		private string _Attachment="";
		/// <summary>备注</summary>	
		[Description("备注")]
        public string Remark { get{return _Remark;} set{_Remark = value??"";} } // Remark
		private string _Remark="";
		/// <summary>持证部门</summary>	
		[Description("持证部门")]
        public string HoldDept { get{return _HoldDept;} set{_HoldDept = value??"";} } // HoldDept
		private string _HoldDept="";
		/// <summary>持证部门名称</summary>	
		[Description("持证部门名称")]
        public string HoldDeptName { get{return _HoldDeptName;} set{_HoldDeptName = value??"";} } // HoldDeptName
		private string _HoldDeptName="";
		/// <summary>证书保管人</summary>	
		[Description("证书保管人")]
        public string Keeper { get{return _Keeper;} set{_Keeper = value??"";} } // Keeper
		private string _Keeper="";
		/// <summary>证书保管人名称</summary>	
		[Description("证书保管人名称")]
        public string KeeperName { get{return _KeeperName;} set{_KeeperName = value??"";} } // KeeperName
		private string _KeeperName="";
		/// <summary>证书类型</summary>	
		[Description("证书类型")]
        public string Type { get{return _Type;} set{_Type = value??"";} } // Type
		private string _Type="";
		/// <summary>实物证书状态</summary>	
		[Description("实物证书状态")]
        public string State { get{return _State;} set{_State = value??"";} } // State
		private string _State="";
		/// <summary>借用人</summary>	
		[Description("借用人")]
        public string Borrower { get{return _Borrower;} set{_Borrower = value??"";} } // Borrower
		private string _Borrower="";
		/// <summary>借用人名称</summary>	
		[Description("借用人名称")]
        public string BorrowerName { get{return _BorrowerName;} set{_BorrowerName = value??"";} } // BorrowerName
		private string _BorrowerName="";
    }

	/// <summary>项目获奖</summary>	
	[Description("项目获奖")]
    public partial class S_C_AwardProject : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID (Primary key)
		private string _ID="";
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? ModifyDate { get; set; } // ModifyDate
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get{return _CreateUserID;} set{_CreateUserID = value??"";} } // CreateUserID
		private string _CreateUserID="";
		/// <summary></summary>	
		[Description("")]
        public string CreateUser { get{return _CreateUser;} set{_CreateUser = value??"";} } // CreateUser
		private string _CreateUser="";
		/// <summary></summary>	
		[Description("")]
        public string ModifyUserID { get{return _ModifyUserID;} set{_ModifyUserID = value??"";} } // ModifyUserID
		private string _ModifyUserID="";
		/// <summary></summary>	
		[Description("")]
        public string ModifyUser { get{return _ModifyUser;} set{_ModifyUser = value??"";} } // ModifyUser
		private string _ModifyUser="";
		/// <summary></summary>	
		[Description("")]
        public string OrgID { get{return _OrgID;} set{_OrgID = value??"";} } // OrgID
		private string _OrgID="";
		/// <summary></summary>	
		[Description("")]
        public string CompanyID { get{return _CompanyID;} set{_CompanyID = value??"";} } // CompanyID
		private string _CompanyID="";
		/// <summary></summary>	
		[Description("")]
        public string FlowPhase { get{return _FlowPhase;} set{_FlowPhase = value??"";} } // FlowPhase
		private string _FlowPhase="";
		/// <summary></summary>	
		[Description("")]
        public string FlowInfo { get{return _FlowInfo;} set{_FlowInfo = value??"";} } // FlowInfo
		private string _FlowInfo="";
		/// <summary></summary>	
		[Description("")]
        public string StepName { get{return _StepName;} set{_StepName = value??"";} } // StepName
		private string _StepName="";
		/// <summary>登记号</summary>	
		[Description("登记号")]
        public string SerialNumber { get{return _SerialNumber;} set{_SerialNumber = value??"";} } // SerialNumber
		private string _SerialNumber="";
		/// <summary>获奖年度</summary>	
		[Description("获奖年度")]
        public string AwardYear { get{return _AwardYear;} set{_AwardYear = value??"";} } // AwardYear
		private string _AwardYear="";
		/// <summary>项目名称</summary>	
		[Description("项目名称")]
        public string ProjectName { get{return _ProjectName;} set{_ProjectName = value??"";} } // ProjectName
		private string _ProjectName="";
		/// <summary>项目名称名称</summary>	
		[Description("项目名称名称")]
        public string ProjectNameName { get{return _ProjectNameName;} set{_ProjectNameName = value??"";} } // ProjectNameName
		private string _ProjectNameName="";
		/// <summary>奖励名称</summary>	
		[Description("奖励名称")]
        public string Name { get{return _Name;} set{_Name = value??"";} } // Name
		private string _Name="";
		/// <summary>奖励等级</summary>	
		[Description("奖励等级")]
        public string AwardLevel { get{return _AwardLevel;} set{_AwardLevel = value??"";} } // AwardLevel
		private string _AwardLevel="";
		/// <summary>奖励级别</summary>	
		[Description("奖励级别")]
        public string AwardGrade { get{return _AwardGrade;} set{_AwardGrade = value??"";} } // AwardGrade
		private string _AwardGrade="";
		/// <summary>奖励类型</summary>	
		[Description("奖励类型")]
        public string AwardType { get{return _AwardType;} set{_AwardType = value??"";} } // AwardType
		private string _AwardType="";
		/// <summary>获奖人员</summary>	
		[Description("获奖人员")]
        public string CompletePerson { get{return _CompletePerson;} set{_CompletePerson = value??"";} } // CompletePerson
		private string _CompletePerson="";
		/// <summary>获奖人员名称</summary>	
		[Description("获奖人员名称")]
        public string CompletePersonName { get{return _CompletePersonName;} set{_CompletePersonName = value??"";} } // CompletePersonName
		private string _CompletePersonName="";
		/// <summary>完成单位</summary>	
		[Description("完成单位")]
        public string CompleteUnit { get{return _CompleteUnit;} set{_CompleteUnit = value??"";} } // CompleteUnit
		private string _CompleteUnit="";
		/// <summary>颁奖时间</summary>	
		[Description("颁奖时间")]
        public DateTime? AwardDate { get; set; } // AwardDate
		/// <summary>奖励金额(元)</summary>	
		[Description("奖励金额(元)")]
        public string AwardAmount { get{return _AwardAmount;} set{_AwardAmount = value??"";} } // AwardAmount
		private string _AwardAmount="";
		/// <summary>颁奖单位</summary>	
		[Description("颁奖单位")]
        public string AwardAUnit { get{return _AwardAUnit;} set{_AwardAUnit = value??"";} } // AwardAUnit
		private string _AwardAUnit="";
		/// <summary>内部其它参与人</summary>	
		[Description("内部其它参与人")]
        public string InsideAffiliatedPerson { get{return _InsideAffiliatedPerson;} set{_InsideAffiliatedPerson = value??"";} } // InsideAffiliatedPerson
		private string _InsideAffiliatedPerson="";
		/// <summary>内部其它参与人名称</summary>	
		[Description("内部其它参与人名称")]
        public string InsideAffiliatedPersonName { get{return _InsideAffiliatedPersonName;} set{_InsideAffiliatedPersonName = value??"";} } // InsideAffiliatedPersonName
		private string _InsideAffiliatedPersonName="";
		/// <summary>外部人员</summary>	
		[Description("外部人员")]
        public string OutsideAffiliatedPerson { get{return _OutsideAffiliatedPerson;} set{_OutsideAffiliatedPerson = value??"";} } // OutsideAffiliatedPerson
		private string _OutsideAffiliatedPerson="";
		/// <summary>证书扫描件</summary>	
		[Description("证书扫描件")]
        public string Attachment { get{return _Attachment;} set{_Attachment = value??"";} } // Attachment
		private string _Attachment="";
		/// <summary>备注</summary>	
		[Description("备注")]
        public string Remark { get{return _Remark;} set{_Remark = value??"";} } // Remark
		private string _Remark="";
		/// <summary>持证部门</summary>	
		[Description("持证部门")]
        public string HoldDept { get{return _HoldDept;} set{_HoldDept = value??"";} } // HoldDept
		private string _HoldDept="";
		/// <summary>持证部门名称</summary>	
		[Description("持证部门名称")]
        public string HoldDeptName { get{return _HoldDeptName;} set{_HoldDeptName = value??"";} } // HoldDeptName
		private string _HoldDeptName="";
		/// <summary>证书保管人</summary>	
		[Description("证书保管人")]
        public string Keeper { get{return _Keeper;} set{_Keeper = value??"";} } // Keeper
		private string _Keeper="";
		/// <summary>证书保管人名称</summary>	
		[Description("证书保管人名称")]
        public string KeeperName { get{return _KeeperName;} set{_KeeperName = value??"";} } // KeeperName
		private string _KeeperName="";
		/// <summary>证书类型</summary>	
		[Description("证书类型")]
        public string Type { get{return _Type;} set{_Type = value??"";} } // Type
		private string _Type="";
		/// <summary>实物证书状态</summary>	
		[Description("实物证书状态")]
        public string State { get{return _State;} set{_State = value??"";} } // State
		private string _State="";
		/// <summary>借用人</summary>	
		[Description("借用人")]
        public string Borrower { get{return _Borrower;} set{_Borrower = value??"";} } // Borrower
		private string _Borrower="";
		/// <summary>借用人名称</summary>	
		[Description("借用人名称")]
        public string BorrowerName { get{return _BorrowerName;} set{_BorrowerName = value??"";} } // BorrowerName
		private string _BorrowerName="";
    }

	/// <summary>企业资质</summary>	
	[Description("企业资质")]
    public partial class S_C_Certificate : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID (Primary key)
		private string _ID="";
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? ModifyDate { get; set; } // ModifyDate
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get{return _CreateUserID;} set{_CreateUserID = value??"";} } // CreateUserID
		private string _CreateUserID="";
		/// <summary></summary>	
		[Description("")]
        public string CreateUser { get{return _CreateUser;} set{_CreateUser = value??"";} } // CreateUser
		private string _CreateUser="";
		/// <summary></summary>	
		[Description("")]
        public string ModifyUserID { get{return _ModifyUserID;} set{_ModifyUserID = value??"";} } // ModifyUserID
		private string _ModifyUserID="";
		/// <summary></summary>	
		[Description("")]
        public string ModifyUser { get{return _ModifyUser;} set{_ModifyUser = value??"";} } // ModifyUser
		private string _ModifyUser="";
		/// <summary></summary>	
		[Description("")]
        public string OrgID { get{return _OrgID;} set{_OrgID = value??"";} } // OrgID
		private string _OrgID="";
		/// <summary></summary>	
		[Description("")]
        public string CompanyID { get{return _CompanyID;} set{_CompanyID = value??"";} } // CompanyID
		private string _CompanyID="";
		/// <summary></summary>	
		[Description("")]
        public string FlowPhase { get{return _FlowPhase;} set{_FlowPhase = value??"";} } // FlowPhase
		private string _FlowPhase="";
		/// <summary></summary>	
		[Description("")]
        public string FlowInfo { get{return _FlowInfo;} set{_FlowInfo = value??"";} } // FlowInfo
		private string _FlowInfo="";
		/// <summary></summary>	
		[Description("")]
        public string StepName { get{return _StepName;} set{_StepName = value??"";} } // StepName
		private string _StepName="";
		/// <summary>序号</summary>	
		[Description("序号")]
        public string OrdinalNumber { get{return _OrdinalNumber;} set{_OrdinalNumber = value??"";} } // OrdinalNumber
		private string _OrdinalNumber="";
		/// <summary>证书等级</summary>	
		[Description("证书等级")]
        public string CertificateLevel { get{return _CertificateLevel;} set{_CertificateLevel = value??"";} } // CertificateLevel
		private string _CertificateLevel="";
		/// <summary>证书名称</summary>	
		[Description("证书名称")]
        public string Name { get{return _Name;} set{_Name = value??"";} } // Name
		private string _Name="";
		/// <summary>证书编号</summary>	
		[Description("证书编号")]
        public string Code { get{return _Code;} set{_Code = value??"";} } // Code
		private string _Code="";
		/// <summary>发证机关</summary>	
		[Description("发证机关")]
        public string IssuingAuthority { get{return _IssuingAuthority;} set{_IssuingAuthority = value??"";} } // IssuingAuthority
		private string _IssuingAuthority="";
		/// <summary>发证时间</summary>	
		[Description("发证时间")]
        public DateTime? IssueDate { get; set; } // IssueDate
		/// <summary>有效期限</summary>	
		[Description("有效期限")]
        public DateTime? ValidPeriod { get; set; } // ValidPeriod
		/// <summary>申报日期</summary>	
		[Description("申报日期")]
        public DateTime? DeclareDate { get; set; } // DeclareDate
		/// <summary>年检日期</summary>	
		[Description("年检日期")]
        public DateTime? InspectionDate { get; set; } // InspectionDate
		/// <summary>换证日期</summary>	
		[Description("换证日期")]
        public DateTime? ChangeDate { get; set; } // ChangeDate
		/// <summary>转正日期</summary>	
		[Description("转正日期")]
        public DateTime? PromotionDate { get; set; } // PromotionDate
		/// <summary>升级日期</summary>	
		[Description("升级日期")]
        public DateTime? UpgradeDate { get; set; } // UpgradeDate
		/// <summary>业务范围</summary>	
		[Description("业务范围")]
        public string BusinessRange { get{return _BusinessRange;} set{_BusinessRange = value??"";} } // BusinessRange
		private string _BusinessRange="";
		/// <summary>资质管理办法注册人员配置要求</summary>	
		[Description("资质管理办法注册人员配置要求")]
        public string ConfigureDemand { get{return _ConfigureDemand;} set{_ConfigureDemand = value??"";} } // ConfigureDemand
		private string _ConfigureDemand="";
		/// <summary>技术装备以及管理水平</summary>	
		[Description("技术装备以及管理水平")]
        public string ManagementLevel { get{return _ManagementLevel;} set{_ManagementLevel = value??"";} } // ManagementLevel
		private string _ManagementLevel="";
		/// <summary>业务要求</summary>	
		[Description("业务要求")]
        public string BusinessDemand { get{return _BusinessDemand;} set{_BusinessDemand = value??"";} } // BusinessDemand
		private string _BusinessDemand="";
		/// <summary>专业人员配置</summary>	
		[Description("专业人员配置")]
        public string PeopleConfigure { get{return _PeopleConfigure;} set{_PeopleConfigure = value??"";} } // PeopleConfigure
		private string _PeopleConfigure="";
		/// <summary>目前业绩情况</summary>	
		[Description("目前业绩情况")]
        public string BusinessSituation { get{return _BusinessSituation;} set{_BusinessSituation = value??"";} } // BusinessSituation
		private string _BusinessSituation="";
		/// <summary>存在问题</summary>	
		[Description("存在问题")]
        public string ExistingProblem { get{return _ExistingProblem;} set{_ExistingProblem = value??"";} } // ExistingProblem
		private string _ExistingProblem="";
		/// <summary>正副本扫描附件</summary>	
		[Description("正副本扫描附件")]
        public string Attachment { get{return _Attachment;} set{_Attachment = value??"";} } // Attachment
		private string _Attachment="";
		/// <summary>备注</summary>	
		[Description("备注")]
        public string Remark { get{return _Remark;} set{_Remark = value??"";} } // Remark
		private string _Remark="";
		/// <summary>持证部门</summary>	
		[Description("持证部门")]
        public string HoldDept { get{return _HoldDept;} set{_HoldDept = value??"";} } // HoldDept
		private string _HoldDept="";
		/// <summary>持证部门名称</summary>	
		[Description("持证部门名称")]
        public string HoldDeptName { get{return _HoldDeptName;} set{_HoldDeptName = value??"";} } // HoldDeptName
		private string _HoldDeptName="";
		/// <summary>证书保管人</summary>	
		[Description("证书保管人")]
        public string Keeper { get{return _Keeper;} set{_Keeper = value??"";} } // Keeper
		private string _Keeper="";
		/// <summary>证书保管人名称</summary>	
		[Description("证书保管人名称")]
        public string KeeperName { get{return _KeeperName;} set{_KeeperName = value??"";} } // KeeperName
		private string _KeeperName="";
		/// <summary>证书类型</summary>	
		[Description("证书类型")]
        public string Type { get{return _Type;} set{_Type = value??"";} } // Type
		private string _Type="";
		/// <summary>实物证书状态</summary>	
		[Description("实物证书状态")]
        public string State { get{return _State;} set{_State = value??"";} } // State
		private string _State="";
		/// <summary>借用人</summary>	
		[Description("借用人")]
        public string Borrower { get{return _Borrower;} set{_Borrower = value??"";} } // Borrower
		private string _Borrower="";
		/// <summary>借用人名称</summary>	
		[Description("借用人名称")]
        public string BorrowerName { get{return _BorrowerName;} set{_BorrowerName = value??"";} } // BorrowerName
		private string _BorrowerName="";
    }

	/// <summary>专利信息</summary>	
	[Description("专利信息")]
    public partial class S_C_Patent : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID (Primary key)
		private string _ID="";
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? ModifyDate { get; set; } // ModifyDate
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get{return _CreateUserID;} set{_CreateUserID = value??"";} } // CreateUserID
		private string _CreateUserID="";
		/// <summary></summary>	
		[Description("")]
        public string CreateUser { get{return _CreateUser;} set{_CreateUser = value??"";} } // CreateUser
		private string _CreateUser="";
		/// <summary></summary>	
		[Description("")]
        public string ModifyUserID { get{return _ModifyUserID;} set{_ModifyUserID = value??"";} } // ModifyUserID
		private string _ModifyUserID="";
		/// <summary></summary>	
		[Description("")]
        public string ModifyUser { get{return _ModifyUser;} set{_ModifyUser = value??"";} } // ModifyUser
		private string _ModifyUser="";
		/// <summary></summary>	
		[Description("")]
        public string OrgID { get{return _OrgID;} set{_OrgID = value??"";} } // OrgID
		private string _OrgID="";
		/// <summary></summary>	
		[Description("")]
        public string CompanyID { get{return _CompanyID;} set{_CompanyID = value??"";} } // CompanyID
		private string _CompanyID="";
		/// <summary></summary>	
		[Description("")]
        public string FlowPhase { get{return _FlowPhase;} set{_FlowPhase = value??"";} } // FlowPhase
		private string _FlowPhase="";
		/// <summary></summary>	
		[Description("")]
        public string FlowInfo { get{return _FlowInfo;} set{_FlowInfo = value??"";} } // FlowInfo
		private string _FlowInfo="";
		/// <summary></summary>	
		[Description("")]
        public string StepName { get{return _StepName;} set{_StepName = value??"";} } // StepName
		private string _StepName="";
		/// <summary>序号</summary>	
		[Description("序号")]
        public string SerialNumber { get{return _SerialNumber;} set{_SerialNumber = value??"";} } // SerialNumber
		private string _SerialNumber="";
		/// <summary>档案号</summary>	
		[Description("档案号")]
        public string ArchiveNumber { get{return _ArchiveNumber;} set{_ArchiveNumber = value??"";} } // ArchiveNumber
		private string _ArchiveNumber="";
		/// <summary>申请日期</summary>	
		[Description("申请日期")]
        public DateTime? ApplyDate { get; set; } // ApplyDate
		/// <summary>申请号</summary>	
		[Description("申请号")]
        public string ApplyNumber { get{return _ApplyNumber;} set{_ApplyNumber = value??"";} } // ApplyNumber
		private string _ApplyNumber="";
		/// <summary>专利名称</summary>	
		[Description("专利名称")]
        public string Name { get{return _Name;} set{_Name = value??"";} } // Name
		private string _Name="";
		/// <summary>发明人</summary>	
		[Description("发明人")]
        public string Inventor { get{return _Inventor;} set{_Inventor = value??"";} } // Inventor
		private string _Inventor="";
		/// <summary>法律状态</summary>	
		[Description("法律状态")]
        public string LawState { get{return _LawState;} set{_LawState = value??"";} } // LawState
		private string _LawState="";
		/// <summary>证书扫描附件</summary>	
		[Description("证书扫描附件")]
        public string Attachment { get{return _Attachment;} set{_Attachment = value??"";} } // Attachment
		private string _Attachment="";
		/// <summary>备注</summary>	
		[Description("备注")]
        public string Remark { get{return _Remark;} set{_Remark = value??"";} } // Remark
		private string _Remark="";
		/// <summary>证书保管人</summary>	
		[Description("证书保管人")]
        public string Keeper { get{return _Keeper;} set{_Keeper = value??"";} } // Keeper
		private string _Keeper="";
		/// <summary>证书保管人名称</summary>	
		[Description("证书保管人名称")]
        public string KeeperName { get{return _KeeperName;} set{_KeeperName = value??"";} } // KeeperName
		private string _KeeperName="";
		/// <summary>持证部门</summary>	
		[Description("持证部门")]
        public string HoldDept { get{return _HoldDept;} set{_HoldDept = value??"";} } // HoldDept
		private string _HoldDept="";
		/// <summary>持证部门名称</summary>	
		[Description("持证部门名称")]
        public string HoldDeptName { get{return _HoldDeptName;} set{_HoldDeptName = value??"";} } // HoldDeptName
		private string _HoldDeptName="";
		/// <summary>证书类型</summary>	
		[Description("证书类型")]
        public string Type { get{return _Type;} set{_Type = value??"";} } // Type
		private string _Type="";
		/// <summary>实物证书状态</summary>	
		[Description("实物证书状态")]
        public string State { get{return _State;} set{_State = value??"";} } // State
		private string _State="";
		/// <summary>借用人</summary>	
		[Description("借用人")]
        public string Borrower { get{return _Borrower;} set{_Borrower = value??"";} } // Borrower
		private string _Borrower="";
		/// <summary>借用人名称</summary>	
		[Description("借用人名称")]
        public string BorrowerName { get{return _BorrowerName;} set{_BorrowerName = value??"";} } // BorrowerName
		private string _BorrowerName="";
		/// <summary>专利号</summary>	
		[Description("专利号")]
        public string PatentNo { get{return _PatentNo;} set{_PatentNo = value??"";} } // PatentNo
		private string _PatentNo="";
		/// <summary>专利权人</summary>	
		[Description("专利权人")]
        public string PatentPerson { get{return _PatentPerson;} set{_PatentPerson = value??"";} } // PatentPerson
		private string _PatentPerson="";
		/// <summary>授权公告日</summary>	
		[Description("授权公告日")]
        public DateTime? AuthorizationNum { get; set; } // AuthorizationNum
		/// <summary>专利类型</summary>	
		[Description("专利类型")]
        public string PatentType { get{return _PatentType;} set{_PatentType = value??"";} } // PatentType
		private string _PatentType="";
    }

	/// <summary></summary>	
	[Description("")]
    public partial class S_C_QualificationApplyLog : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID (Primary key)
		private string _ID="";
		/// <summary></summary>	
		[Description("")]
        public string QualificationID { get{return _QualificationID;} set{_QualificationID = value??"";} } // QualificationID
		private string _QualificationID="";
		/// <summary></summary>	
		[Description("")]
        public string Name { get{return _Name;} set{_Name = value??"";} } // Name
		private string _Name="";
		/// <summary></summary>	
		[Description("")]
        public string QualificationType { get{return _QualificationType;} set{_QualificationType = value??"";} } // QualificationType
		private string _QualificationType="";
		/// <summary></summary>	
		[Description("")]
        public string BorrowUserID { get{return _BorrowUserID;} set{_BorrowUserID = value??"";} } // BorrowUserID
		private string _BorrowUserID="";
		/// <summary></summary>	
		[Description("")]
        public string BorrowUserName { get{return _BorrowUserName;} set{_BorrowUserName = value??"";} } // BorrowUserName
		private string _BorrowUserName="";
		/// <summary></summary>	
		[Description("")]
        public string BorrowerDeptID { get{return _BorrowerDeptID;} set{_BorrowerDeptID = value??"";} } // BorrowerDeptID
		private string _BorrowerDeptID="";
		/// <summary></summary>	
		[Description("")]
        public string BorrowerDeptName { get{return _BorrowerDeptName;} set{_BorrowerDeptName = value??"";} } // BorrowerDeptName
		private string _BorrowerDeptName="";
		/// <summary></summary>	
		[Description("")]
        public string BorrowerTel { get{return _BorrowerTel;} set{_BorrowerTel = value??"";} } // BorrowerTel
		private string _BorrowerTel="";
		/// <summary></summary>	
		[Description("")]
        public DateTime? BorrowDate { get; set; } // BorrowDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? PlanReturnDate { get; set; } // PlanReturnDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? ReturnDate { get; set; } // ReturnDate
		/// <summary></summary>	
		[Description("")]
        public string QualificationApplyID { get{return _QualificationApplyID;} set{_QualificationApplyID = value??"";} } // QualificationApplyID
		private string _QualificationApplyID="";
    }

	/// <summary></summary>	
	[Description("")]
    public partial class S_D_UserAptitude : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID (Primary key)
		private string _ID="";
		/// <summary></summary>	
		[Description("")]
        public string UserID { get{return _UserID;} set{_UserID = value??"";} } // UserID
		private string _UserID="";
		/// <summary></summary>	
		[Description("")]
        public string UserName { get{return _UserName;} set{_UserName = value??"";} } // UserName
		private string _UserName="";
		/// <summary></summary>	
		[Description("")]
        public string HREmployeeID { get{return _HREmployeeID;} set{_HREmployeeID = value??"";} } // HREmployeeID
		private string _HREmployeeID="";
		/// <summary></summary>	
		[Description("")]
        public string DeptID { get{return _DeptID;} set{_DeptID = value??"";} } // DeptID
		private string _DeptID="";
		/// <summary></summary>	
		[Description("")]
        public string DeptName { get{return _DeptName;} set{_DeptName = value??"";} } // DeptName
		private string _DeptName="";
		/// <summary></summary>	
		[Description("")]
        public string Major { get{return _Major;} set{_Major = value??"";} } // Major
		private string _Major="";
		/// <summary></summary>	
		[Description("")]
        public string ProjectClass { get{return _ProjectClass;} set{_ProjectClass = value??"";} } // ProjectClass
		private string _ProjectClass="";
		/// <summary></summary>	
		[Description("")]
        public string Position { get{return _Position;} set{_Position = value??"";} } // Position
		private string _Position="";
		/// <summary></summary>	
		[Description("")]
        public int AptitudeLevel { get; set; } // AptitudeLevel
    }

	/// <summary></summary>	
	[Description("")]
    public partial class S_D_UserAptitudeApply : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID (Primary key)
		private string _ID="";
		/// <summary></summary>	
		[Description("")]
        public string UserAptitudeApplyID { get{return _UserAptitudeApplyID;} set{_UserAptitudeApplyID = value??"";} } // UserAptitudeApplyID
		private string _UserAptitudeApplyID="";
		/// <summary></summary>	
		[Description("")]
        public string UserID { get{return _UserID;} set{_UserID = value??"";} } // UserID
		private string _UserID="";
		/// <summary></summary>	
		[Description("")]
        public string UserName { get{return _UserName;} set{_UserName = value??"";} } // UserName
		private string _UserName="";
		/// <summary></summary>	
		[Description("")]
        public string HREmployeeID { get{return _HREmployeeID;} set{_HREmployeeID = value??"";} } // HREmployeeID
		private string _HREmployeeID="";
		/// <summary></summary>	
		[Description("")]
        public string DeptID { get{return _DeptID;} set{_DeptID = value??"";} } // DeptID
		private string _DeptID="";
		/// <summary></summary>	
		[Description("")]
        public string DeptName { get{return _DeptName;} set{_DeptName = value??"";} } // DeptName
		private string _DeptName="";
		/// <summary></summary>	
		[Description("")]
        public string Major { get{return _Major;} set{_Major = value??"";} } // Major
		private string _Major="";
		/// <summary></summary>	
		[Description("")]
        public string ProjectClass { get{return _ProjectClass;} set{_ProjectClass = value??"";} } // ProjectClass
		private string _ProjectClass="";
		/// <summary></summary>	
		[Description("")]
        public string Position { get{return _Position;} set{_Position = value??"";} } // Position
		private string _Position="";
		/// <summary></summary>	
		[Description("")]
        public int AptitudeLevel { get; set; } // AptitudeLevel
		/// <summary></summary>	
		[Description("")]
        public string ProjectInfoID { get{return _ProjectInfoID;} set{_ProjectInfoID = value??"";} } // ProjectInfoID
		private string _ProjectInfoID="";
		/// <summary></summary>	
		[Description("")]
        public string ProjectInfoName { get{return _ProjectInfoName;} set{_ProjectInfoName = value??"";} } // ProjectInfoName
		private string _ProjectInfoName="";
    }

	/// <summary>业绩管理</summary>	
	[Description("业绩管理")]
    public partial class S_P_Performance : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID (Primary key)
		private string _ID="";
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? ModifyDate { get; set; } // ModifyDate
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get{return _CreateUserID;} set{_CreateUserID = value??"";} } // CreateUserID
		private string _CreateUserID="";
		/// <summary></summary>	
		[Description("")]
        public string CreateUser { get{return _CreateUser;} set{_CreateUser = value??"";} } // CreateUser
		private string _CreateUser="";
		/// <summary></summary>	
		[Description("")]
        public string ModifyUserID { get{return _ModifyUserID;} set{_ModifyUserID = value??"";} } // ModifyUserID
		private string _ModifyUserID="";
		/// <summary></summary>	
		[Description("")]
        public string ModifyUser { get{return _ModifyUser;} set{_ModifyUser = value??"";} } // ModifyUser
		private string _ModifyUser="";
		/// <summary></summary>	
		[Description("")]
        public string OrgID { get{return _OrgID;} set{_OrgID = value??"";} } // OrgID
		private string _OrgID="";
		/// <summary></summary>	
		[Description("")]
        public string CompanyID { get{return _CompanyID;} set{_CompanyID = value??"";} } // CompanyID
		private string _CompanyID="";
		/// <summary></summary>	
		[Description("")]
        public string FlowPhase { get{return _FlowPhase;} set{_FlowPhase = value??"";} } // FlowPhase
		private string _FlowPhase="";
		/// <summary></summary>	
		[Description("")]
        public string FlowInfo { get{return _FlowInfo;} set{_FlowInfo = value??"";} } // FlowInfo
		private string _FlowInfo="";
		/// <summary></summary>	
		[Description("")]
        public string StepName { get{return _StepName;} set{_StepName = value??"";} } // StepName
		private string _StepName="";
		/// <summary>项目名称</summary>	
		[Description("项目名称")]
        public string Name { get{return _Name;} set{_Name = value??"";} } // Name
		private string _Name="";
		/// <summary>项目ID</summary>	
		[Description("项目ID")]
        public string ProjectInfoID { get{return _ProjectInfoID;} set{_ProjectInfoID = value??"";} } // ProjectInfoID
		private string _ProjectInfoID="";
		/// <summary>项目编号</summary>	
		[Description("项目编号")]
        public string Code { get{return _Code;} set{_Code = value??"";} } // Code
		private string _Code="";
		/// <summary>客户ID</summary>	
		[Description("客户ID")]
        public string CustomerID { get{return _CustomerID;} set{_CustomerID = value??"";} } // CustomerID
		private string _CustomerID="";
		/// <summary>客户名称</summary>	
		[Description("客户名称")]
        public string CustomerName { get{return _CustomerName;} set{_CustomerName = value??"";} } // CustomerName
		private string _CustomerName="";
		/// <summary>设计阶段</summary>	
		[Description("设计阶段")]
        public string PhaseName { get{return _PhaseName;} set{_PhaseName = value??"";} } // PhaseName
		private string _PhaseName="";
		/// <summary>所属行业</summary>	
		[Description("所属行业")]
        public string Industry { get{return _Industry;} set{_Industry = value??"";} } // Industry
		private string _Industry="";
		/// <summary>所在国家</summary>	
		[Description("所在国家")]
        public string Country { get{return _Country;} set{_Country = value??"";} } // Country
		private string _Country="";
		/// <summary>所在省份</summary>	
		[Description("所在省份")]
        public string Province { get{return _Province;} set{_Province = value??"";} } // Province
		private string _Province="";
		/// <summary>所在城市</summary>	
		[Description("所在城市")]
        public string City { get{return _City;} set{_City = value??"";} } // City
		private string _City="";
		/// <summary>建设地点</summary>	
		[Description("建设地点")]
        public string BuildAddress { get{return _BuildAddress;} set{_BuildAddress = value??"";} } // BuildAddress
		private string _BuildAddress="";
		/// <summary>项目类型</summary>	
		[Description("项目类型")]
        public string ProjectClass { get{return _ProjectClass;} set{_ProjectClass = value??"";} } // ProjectClass
		private string _ProjectClass="";
		/// <summary>项目规模</summary>	
		[Description("项目规模")]
        public string ProjectLevel { get{return _ProjectLevel;} set{_ProjectLevel = value??"";} } // ProjectLevel
		private string _ProjectLevel="";
		/// <summary>项目负责人</summary>	
		[Description("项目负责人")]
        public string ChargeUser { get{return _ChargeUser;} set{_ChargeUser = value??"";} } // ChargeUser
		private string _ChargeUser="";
		/// <summary>项目负责人名称</summary>	
		[Description("项目负责人名称")]
        public string ChargeUserName { get{return _ChargeUserName;} set{_ChargeUserName = value??"";} } // ChargeUserName
		private string _ChargeUserName="";
		/// <summary>项目开始日期</summary>	
		[Description("项目开始日期")]
        public DateTime? PlanStartDate { get; set; } // PlanStartDate
		/// <summary>项目结束日期</summary>	
		[Description("项目结束日期")]
        public DateTime? FactFinishDate { get; set; } // FactFinishDate
		/// <summary>生产项目状态</summary>	
		[Description("生产项目状态")]
        public string ProjectState { get{return _ProjectState;} set{_ProjectState = value??"";} } // ProjectState
		private string _ProjectState="";
		/// <summary>合同签约日期</summary>	
		[Description("合同签约日期")]
        public DateTime? SignDate { get; set; } // SignDate
		/// <summary>合同归属年月</summary>	
		[Description("合同归属年月")]
        public string ContractBelongYearMonth { get{return _ContractBelongYearMonth;} set{_ContractBelongYearMonth = value??"";} } // ContractBelongYearMonth
		private string _ContractBelongYearMonth="";
		/// <summary>合同截止日期</summary>	
		[Description("合同截止日期")]
        public DateTime? EndDate { get; set; } // EndDate
		/// <summary>合同金额(元)</summary>	
		[Description("合同金额(元)")]
        public double? ContractRMBAmount { get; set; } // ContractRMBAmount
		/// <summary>获奖情况</summary>	
		[Description("获奖情况")]
        public string AwardInfo { get{return _AwardInfo;} set{_AwardInfo = value??"";} } // AwardInfo
		private string _AwardInfo="";
		/// <summary>设计阶段</summary>	
		[Description("设计阶段")]
        public string PhaseValue { get{return _PhaseValue;} set{_PhaseValue = value??"";} } // PhaseValue
		private string _PhaseValue="";
		/// <summary></summary>	
		[Description("")]
        public string ContractID { get{return _ContractID;} set{_ContractID = value??"";} } // ContractID
		private string _ContractID="";

        // Reverse navigation
		[JsonIgnore]
        public virtual ICollection<S_P_Performance_JoinPeople> S_P_Performance_JoinPeople { get { onS_P_Performance_JoinPeopleGetting(); return _S_P_Performance_JoinPeople;} }
		private ICollection<S_P_Performance_JoinPeople> _S_P_Performance_JoinPeople;
		partial void onS_P_Performance_JoinPeopleGetting();


        public S_P_Performance()
        {
            _S_P_Performance_JoinPeople = new List<S_P_Performance_JoinPeople>();
        }
    }

	/// <summary>项目参与人员</summary>	
	[Description("项目参与人员")]
    public partial class S_P_Performance_JoinPeople : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID (Primary key)
		private string _ID="";
		/// <summary></summary>	
		[Description("")]
        public string S_P_PerformanceID { get{return _S_P_PerformanceID;} set{_S_P_PerformanceID = value??"";} } // S_P_PerformanceID
		private string _S_P_PerformanceID="";
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string IsReleased { get{return _IsReleased;} set{_IsReleased = value??"";} } // IsReleased
		private string _IsReleased="";
		/// <summary>专业</summary>	
		[Description("专业")]
        public string Major { get{return _Major;} set{_Major = value??"";} } // Major
		private string _Major="";
		/// <summary>专业负责人</summary>	
		[Description("专业负责人")]
        public string MajorPrinciple { get{return _MajorPrinciple;} set{_MajorPrinciple = value??"";} } // MajorPrinciple
		private string _MajorPrinciple="";
		/// <summary>专业负责人名称</summary>	
		[Description("专业负责人名称")]
        public string MajorPrincipleName { get{return _MajorPrincipleName;} set{_MajorPrincipleName = value??"";} } // MajorPrincipleName
		private string _MajorPrincipleName="";
		/// <summary>设计人</summary>	
		[Description("设计人")]
        public string Designer { get{return _Designer;} set{_Designer = value??"";} } // Designer
		private string _Designer="";
		/// <summary>设计人名称</summary>	
		[Description("设计人名称")]
        public string DesignerName { get{return _DesignerName;} set{_DesignerName = value??"";} } // DesignerName
		private string _DesignerName="";
		/// <summary>校对人</summary>	
		[Description("校对人")]
        public string Collactor { get{return _Collactor;} set{_Collactor = value??"";} } // Collactor
		private string _Collactor="";
		/// <summary>校对人名称</summary>	
		[Description("校对人名称")]
        public string CollactorName { get{return _CollactorName;} set{_CollactorName = value??"";} } // CollactorName
		private string _CollactorName="";
		/// <summary>审核人</summary>	
		[Description("审核人")]
        public string Auditor { get{return _Auditor;} set{_Auditor = value??"";} } // Auditor
		private string _Auditor="";
		/// <summary>审核人名称</summary>	
		[Description("审核人名称")]
        public string AuditorName { get{return _AuditorName;} set{_AuditorName = value??"";} } // AuditorName
		private string _AuditorName="";
		/// <summary>审定人</summary>	
		[Description("审定人")]
        public string Approver { get{return _Approver;} set{_Approver = value??"";} } // Approver
		private string _Approver="";
		/// <summary>审定人名称</summary>	
		[Description("审定人名称")]
        public string ApproverName { get{return _ApproverName;} set{_ApproverName = value??"";} } // ApproverName
		private string _ApproverName="";

        // Foreign keys
		[JsonIgnore]
        public virtual S_P_Performance S_P_Performance { get; set; } //  S_P_PerformanceID - FK_S_P_Performance_JoinPeople_S_P_Performance
    }

	/// <summary>岗位岗级模板管理</summary>	
	[Description("岗位岗级模板管理")]
    public partial class S_S_PostLevelTemplate : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID (Primary key)
		private string _ID="";
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? ModifyDate { get; set; } // ModifyDate
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get{return _CreateUserID;} set{_CreateUserID = value??"";} } // CreateUserID
		private string _CreateUserID="";
		/// <summary></summary>	
		[Description("")]
        public string CreateUser { get{return _CreateUser;} set{_CreateUser = value??"";} } // CreateUser
		private string _CreateUser="";
		/// <summary></summary>	
		[Description("")]
        public string ModifyUserID { get{return _ModifyUserID;} set{_ModifyUserID = value??"";} } // ModifyUserID
		private string _ModifyUserID="";
		/// <summary></summary>	
		[Description("")]
        public string ModifyUser { get{return _ModifyUser;} set{_ModifyUser = value??"";} } // ModifyUser
		private string _ModifyUser="";
		/// <summary></summary>	
		[Description("")]
        public string OrgID { get{return _OrgID;} set{_OrgID = value??"";} } // OrgID
		private string _OrgID="";
		/// <summary></summary>	
		[Description("")]
        public string FlowPhase { get{return _FlowPhase;} set{_FlowPhase = value??"";} } // FlowPhase
		private string _FlowPhase="";
		/// <summary></summary>	
		[Description("")]
        public string StepName { get{return _StepName;} set{_StepName = value??"";} } // StepName
		private string _StepName="";
		/// <summary>年份</summary>	
		[Description("年份")]
        public string BelongYear { get{return _BelongYear;} set{_BelongYear = value??"";} } // BelongYear
		private string _BelongYear="";
		/// <summary>月份</summary>	
		[Description("月份")]
        public string BelongMonth { get{return _BelongMonth;} set{_BelongMonth = value??"";} } // BelongMonth
		private string _BelongMonth="";
		/// <summary>季度</summary>	
		[Description("季度")]
        public string BelongQuarter { get{return _BelongQuarter;} set{_BelongQuarter = value??"";} } // BelongQuarter
		private string _BelongQuarter="";
		/// <summary>备注</summary>	
		[Description("备注")]
        public string Remark { get{return _Remark;} set{_Remark = value??"";} } // Remark
		private string _Remark="";
		/// <summary>模板编号</summary>	
		[Description("模板编号")]
        public string TemplateCode { get{return _TemplateCode;} set{_TemplateCode = value??"";} } // TemplateCode
		private string _TemplateCode="";
		/// <summary>模板名称</summary>	
		[Description("模板名称")]
        public string TemplateName { get{return _TemplateName;} set{_TemplateName = value??"";} } // TemplateName
		private string _TemplateName="";
		/// <summary>启用日期</summary>	
		[Description("启用日期")]
        public DateTime? StartUseDate { get; set; } // StartUseDate

        // Reverse navigation
		[JsonIgnore]
        public virtual ICollection<S_S_PostLevelTemplate_PostList> S_S_PostLevelTemplate_PostList { get { onS_S_PostLevelTemplate_PostListGetting(); return _S_S_PostLevelTemplate_PostList;} }
		private ICollection<S_S_PostLevelTemplate_PostList> _S_S_PostLevelTemplate_PostList;
		partial void onS_S_PostLevelTemplate_PostListGetting();


        public S_S_PostLevelTemplate()
        {
            _S_S_PostLevelTemplate_PostList = new List<S_S_PostLevelTemplate_PostList>();
        }
    }

	/// <summary>岗位岗级</summary>	
	[Description("岗位岗级")]
    public partial class S_S_PostLevelTemplate_PostList : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID (Primary key)
		private string _ID="";
		/// <summary></summary>	
		[Description("")]
        public string S_S_PostLevelTemplateID { get{return _S_S_PostLevelTemplateID;} set{_S_S_PostLevelTemplateID = value??"";} } // S_S_PostLevelTemplateID
		private string _S_S_PostLevelTemplateID="";
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string IsReleased { get{return _IsReleased;} set{_IsReleased = value??"";} } // IsReleased
		private string _IsReleased="";
		/// <summary>岗位</summary>	
		[Description("岗位")]
        public string PostCode { get{return _PostCode;} set{_PostCode = value??"";} } // PostCode
		private string _PostCode="";
		/// <summary>岗级</summary>	
		[Description("岗级")]
        public string PostLevelCode { get{return _PostLevelCode;} set{_PostLevelCode = value??"";} } // PostLevelCode
		private string _PostLevelCode="";
		/// <summary>年份</summary>	
		[Description("年份")]
        public string BelongYear { get{return _BelongYear;} set{_BelongYear = value??"";} } // BelongYear
		private string _BelongYear="";
		/// <summary>月份</summary>	
		[Description("月份")]
        public string BelongMonth { get{return _BelongMonth;} set{_BelongMonth = value??"";} } // BelongMonth
		private string _BelongMonth="";
		/// <summary>季度</summary>	
		[Description("季度")]
        public string BelongQuarter { get{return _BelongQuarter;} set{_BelongQuarter = value??"";} } // BelongQuarter
		private string _BelongQuarter="";
		/// <summary>备注</summary>	
		[Description("备注")]
        public string ReMark { get{return _ReMark;} set{_ReMark = value??"";} } // ReMark
		private string _ReMark="";
		/// <summary>PostName</summary>	
		[Description("PostName")]
        public string PostName { get{return _PostName;} set{_PostName = value??"";} } // PostName
		private string _PostName="";
		/// <summary>PostLevelName</summary>	
		[Description("PostLevelName")]
        public string PostLevelName { get{return _PostLevelName;} set{_PostLevelName = value??"";} } // PostLevelName
		private string _PostLevelName="";
		/// <summary>薪资(元)</summary>	
		[Description("薪资(元)")]
        public decimal? BaseSalary { get; set; } // BaseSalary
		/// <summary>启用日期</summary>	
		[Description("启用日期")]
        public DateTime? StartUseDate { get; set; } // StartUseDate

        // Foreign keys
		[JsonIgnore]
        public virtual S_S_PostLevelTemplate S_S_PostLevelTemplate { get; set; } //  S_S_PostLevelTemplateID - FK_S_S_PostLevelTemplate_PostList_S_S_PostLevelTemplate
    }

	/// <summary>个税起征点维护</summary>	
	[Description("个税起征点维护")]
    public partial class S_S_TaxThreshold : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID (Primary key)
		private string _ID="";
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? ModifyDate { get; set; } // ModifyDate
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get{return _CreateUserID;} set{_CreateUserID = value??"";} } // CreateUserID
		private string _CreateUserID="";
		/// <summary></summary>	
		[Description("")]
        public string CreateUser { get{return _CreateUser;} set{_CreateUser = value??"";} } // CreateUser
		private string _CreateUser="";
		/// <summary></summary>	
		[Description("")]
        public string ModifyUserID { get{return _ModifyUserID;} set{_ModifyUserID = value??"";} } // ModifyUserID
		private string _ModifyUserID="";
		/// <summary></summary>	
		[Description("")]
        public string ModifyUser { get{return _ModifyUser;} set{_ModifyUser = value??"";} } // ModifyUser
		private string _ModifyUser="";
		/// <summary></summary>	
		[Description("")]
        public string OrgID { get{return _OrgID;} set{_OrgID = value??"";} } // OrgID
		private string _OrgID="";
		/// <summary></summary>	
		[Description("")]
        public string FlowPhase { get{return _FlowPhase;} set{_FlowPhase = value??"";} } // FlowPhase
		private string _FlowPhase="";
		/// <summary></summary>	
		[Description("")]
        public string StepName { get{return _StepName;} set{_StepName = value??"";} } // StepName
		private string _StepName="";
		/// <summary>年份</summary>	
		[Description("年份")]
        public string BelongYear { get{return _BelongYear;} set{_BelongYear = value??"";} } // BelongYear
		private string _BelongYear="";
		/// <summary>月份</summary>	
		[Description("月份")]
        public string BelongMonth { get{return _BelongMonth;} set{_BelongMonth = value??"";} } // BelongMonth
		private string _BelongMonth="";
		/// <summary>个税起征点</summary>	
		[Description("个税起征点")]
        public string TaxThreshold { get{return _TaxThreshold;} set{_TaxThreshold = value??"";} } // TaxThreshold
		private string _TaxThreshold="";
		/// <summary>开始使用日期</summary>	
		[Description("开始使用日期")]
        public DateTime? StartUseDate { get; set; } // StartUseDate
		/// <summary>个税税率</summary>	
		[Description("个税税率")]
        public double? TaxRate { get; set; } // TaxRate
    }

	/// <summary>年假管理</summary>	
	[Description("年假管理")]
    public partial class S_W_AttendanceAnnualLeave : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID (Primary key)
		private string _ID="";
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? ModifyDate { get; set; } // ModifyDate
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get{return _CreateUserID;} set{_CreateUserID = value??"";} } // CreateUserID
		private string _CreateUserID="";
		/// <summary></summary>	
		[Description("")]
        public string CreateUser { get{return _CreateUser;} set{_CreateUser = value??"";} } // CreateUser
		private string _CreateUser="";
		/// <summary></summary>	
		[Description("")]
        public string ModifyUserID { get{return _ModifyUserID;} set{_ModifyUserID = value??"";} } // ModifyUserID
		private string _ModifyUserID="";
		/// <summary></summary>	
		[Description("")]
        public string ModifyUser { get{return _ModifyUser;} set{_ModifyUser = value??"";} } // ModifyUser
		private string _ModifyUser="";
		/// <summary></summary>	
		[Description("")]
        public string OrgID { get{return _OrgID;} set{_OrgID = value??"";} } // OrgID
		private string _OrgID="";
		/// <summary></summary>	
		[Description("")]
        public string CompanyID { get{return _CompanyID;} set{_CompanyID = value??"";} } // CompanyID
		private string _CompanyID="";
		/// <summary></summary>	
		[Description("")]
        public string FlowPhase { get{return _FlowPhase;} set{_FlowPhase = value??"";} } // FlowPhase
		private string _FlowPhase="";
		/// <summary></summary>	
		[Description("")]
        public string FlowInfo { get{return _FlowInfo;} set{_FlowInfo = value??"";} } // FlowInfo
		private string _FlowInfo="";
		/// <summary></summary>	
		[Description("")]
        public string StepName { get{return _StepName;} set{_StepName = value??"";} } // StepName
		private string _StepName="";
		/// <summary>员工</summary>	
		[Description("员工")]
        public string UserID { get{return _UserID;} set{_UserID = value??"";} } // UserID
		private string _UserID="";
		/// <summary>员工名称</summary>	
		[Description("员工名称")]
        public string UserIDName { get{return _UserIDName;} set{_UserIDName = value??"";} } // UserIDName
		private string _UserIDName="";
		/// <summary>部门</summary>	
		[Description("部门")]
        public string Dept { get{return _Dept;} set{_Dept = value??"";} } // Dept
		private string _Dept="";
		/// <summary>部门名称</summary>	
		[Description("部门名称")]
        public string DeptName { get{return _DeptName;} set{_DeptName = value??"";} } // DeptName
		private string _DeptName="";
		/// <summary>年份</summary>	
		[Description("年份")]
        public int? Year { get; set; } // Year
		/// <summary>年假天数</summary>	
		[Description("年假天数")]
        public int? Days { get; set; } // Days
    }

	/// <summary></summary>	
	[Description("")]
    public partial class S_W_AttendanceConfig : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID (Primary key)
		private string _ID="";
		/// <summary></summary>	
		[Description("")]
        public DateTime? AmCheckDate { get; set; } // AmCheckDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? AmLateCheckDate { get; set; } // AmLateCheckDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? PmCheckDate { get; set; } // PmCheckDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? PmEarlyDate { get; set; } // PmEarlyDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? PmEndDate { get; set; } // PmEndDate
		/// <summary></summary>	
		[Description("")]
        public bool? UseAttendanceInfo { get; set; } // UseAttendanceInfo
		/// <summary></summary>	
		[Description("")]
        public string Description { get{return _Description;} set{_Description = value??"";} } // Description
		private string _Description="";
    }

	/// <summary></summary>	
	[Description("")]
    public partial class S_W_AttendanceEditLog : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID (Primary key)
		private string _ID="";
		/// <summary></summary>	
		[Description("")]
        public string SourceID { get{return _SourceID;} set{_SourceID = value??"";} } // SourceID
		private string _SourceID="";
		/// <summary></summary>	
		[Description("")]
        public DateTime? WorkDay { get; set; } // WorkDay
		/// <summary></summary>	
		[Description("")]
        public DateTime? CheckDate { get; set; } // CheckDate
		/// <summary></summary>	
		[Description("")]
        public string ModifyReason { get{return _ModifyReason;} set{_ModifyReason = value??"";} } // ModifyReason
		private string _ModifyReason="";
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get{return _CreateUserID;} set{_CreateUserID = value??"";} } // CreateUserID
		private string _CreateUserID="";
		/// <summary></summary>	
		[Description("")]
        public string CreateUserName { get{return _CreateUserName;} set{_CreateUserName = value??"";} } // CreateUserName
		private string _CreateUserName="";
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateDate { get; set; } // CreateDate
    }

	/// <summary></summary>	
	[Description("")]
    public partial class S_W_AttendanceInfo : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID (Primary key)
		private string _ID="";
		/// <summary></summary>	
		[Description("")]
        public string UserID { get{return _UserID;} set{_UserID = value??"";} } // UserID
		private string _UserID="";
		/// <summary></summary>	
		[Description("")]
        public string UserName { get{return _UserName;} set{_UserName = value??"";} } // UserName
		private string _UserName="";
		/// <summary></summary>	
		[Description("")]
        public string WorkNo { get{return _WorkNo;} set{_WorkNo = value??"";} } // WorkNo
		private string _WorkNo="";
		/// <summary></summary>	
		[Description("")]
        public DateTime? WorkDay { get; set; } // WorkDay
		/// <summary></summary>	
		[Description("")]
        public DateTime? CheckDate { get; set; } // CheckDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? LastModifyCheckDate { get; set; } // LastModifyCheckDate
		/// <summary></summary>	
		[Description("")]
        public string CheckType { get{return _CheckType;} set{_CheckType = value??"";} } // CheckType
		private string _CheckType="";
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get{return _CreateUserID;} set{_CreateUserID = value??"";} } // CreateUserID
		private string _CreateUserID="";
		/// <summary></summary>	
		[Description("")]
        public string CreateUserName { get{return _CreateUserName;} set{_CreateUserName = value??"";} } // CreateUserName
		private string _CreateUserName="";
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public string ModifyUserID { get{return _ModifyUserID;} set{_ModifyUserID = value??"";} } // ModifyUserID
		private string _ModifyUserID="";
		/// <summary></summary>	
		[Description("")]
        public string ModifyUserName { get{return _ModifyUserName;} set{_ModifyUserName = value??"";} } // ModifyUserName
		private string _ModifyUserName="";
		/// <summary></summary>	
		[Description("")]
        public DateTime? ModifyDate { get; set; } // ModifyDate
		/// <summary></summary>	
		[Description("")]
        public string OrgID { get{return _OrgID;} set{_OrgID = value??"";} } // OrgID
		private string _OrgID="";
		/// <summary></summary>	
		[Description("")]
        public string OrgName { get{return _OrgName;} set{_OrgName = value??"";} } // OrgName
		private string _OrgName="";
		/// <summary></summary>	
		[Description("")]
        public string YDTID { get{return _YDTID;} set{_YDTID = value??"";} } // YDTID
		private string _YDTID="";
    }

	/// <summary>考勤报表批量修改</summary>	
	[Description("考勤报表批量修改")]
    public partial class S_W_AttendanceInfoMultiEdit : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID (Primary key)
		private string _ID="";
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? ModifyDate { get; set; } // ModifyDate
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get{return _CreateUserID;} set{_CreateUserID = value??"";} } // CreateUserID
		private string _CreateUserID="";
		/// <summary></summary>	
		[Description("")]
        public string CreateUser { get{return _CreateUser;} set{_CreateUser = value??"";} } // CreateUser
		private string _CreateUser="";
		/// <summary></summary>	
		[Description("")]
        public string ModifyUserID { get{return _ModifyUserID;} set{_ModifyUserID = value??"";} } // ModifyUserID
		private string _ModifyUserID="";
		/// <summary></summary>	
		[Description("")]
        public string ModifyUser { get{return _ModifyUser;} set{_ModifyUser = value??"";} } // ModifyUser
		private string _ModifyUser="";
		/// <summary></summary>	
		[Description("")]
        public string OrgID { get{return _OrgID;} set{_OrgID = value??"";} } // OrgID
		private string _OrgID="";
		/// <summary></summary>	
		[Description("")]
        public string CompanyID { get{return _CompanyID;} set{_CompanyID = value??"";} } // CompanyID
		private string _CompanyID="";
		/// <summary></summary>	
		[Description("")]
        public string FlowPhase { get{return _FlowPhase;} set{_FlowPhase = value??"";} } // FlowPhase
		private string _FlowPhase="";
		/// <summary></summary>	
		[Description("")]
        public string FlowInfo { get{return _FlowInfo;} set{_FlowInfo = value??"";} } // FlowInfo
		private string _FlowInfo="";
		/// <summary></summary>	
		[Description("")]
        public string StepName { get{return _StepName;} set{_StepName = value??"";} } // StepName
		private string _StepName="";
		/// <summary>人员</summary>	
		[Description("人员")]
        public string UserIDs { get{return _UserIDs;} set{_UserIDs = value??"";} } // UserIDs
		private string _UserIDs="";
		/// <summary>人员名称</summary>	
		[Description("人员名称")]
        public string UserIDsName { get{return _UserIDsName;} set{_UserIDsName = value??"";} } // UserIDsName
		private string _UserIDsName="";
		/// <summary>开始日期</summary>	
		[Description("开始日期")]
        public DateTime? StartDate { get; set; } // StartDate
		/// <summary>结束日期</summary>	
		[Description("结束日期")]
        public DateTime? EndDate { get; set; } // EndDate
		/// <summary>上午</summary>	
		[Description("上午")]
        public string Morning { get{return _Morning;} set{_Morning = value??"";} } // Morning
		private string _Morning="";
		/// <summary>上午请假类别</summary>	
		[Description("上午请假类别")]
        public string MorningType { get{return _MorningType;} set{_MorningType = value??"";} } // MorningType
		private string _MorningType="";
		/// <summary>下午</summary>	
		[Description("下午")]
        public string Afternoon { get{return _Afternoon;} set{_Afternoon = value??"";} } // Afternoon
		private string _Afternoon="";
		/// <summary>下午请假类别</summary>	
		[Description("下午请假类别")]
        public string AfternoonType { get{return _AfternoonType;} set{_AfternoonType = value??"";} } // AfternoonType
		private string _AfternoonType="";
    }

	/// <summary></summary>	
	[Description("")]
    public partial class S_W_DefineUserActualUnitPrice : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID (Primary key)
		private string _ID="";
		/// <summary></summary>	
		[Description("")]
        public string Name { get{return _Name;} set{_Name = value??"";} } // Name
		private string _Name="";
		/// <summary></summary>	
		[Description("")]
        public string Code { get{return _Code;} set{_Code = value??"";} } // Code
		private string _Code="";
		/// <summary></summary>	
		[Description("")]
        public string ParamSource { get{return _ParamSource;} set{_ParamSource = value??"";} } // ParamSource
		private string _ParamSource="";
		/// <summary></summary>	
		[Description("")]
        public string UserSQL { get{return _UserSQL;} set{_UserSQL = value??"";} } // UserSQL
		private string _UserSQL="";
		/// <summary></summary>	
		[Description("")]
        public string CalcFormula { get{return _CalcFormula;} set{_CalcFormula = value??"";} } // CalcFormula
		private string _CalcFormula="";
		/// <summary></summary>	
		[Description("")]
        public string Remark { get{return _Remark;} set{_Remark = value??"";} } // Remark
		private string _Remark="";
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get{return _CreateUserID;} set{_CreateUserID = value??"";} } // CreateUserID
		private string _CreateUserID="";
		/// <summary></summary>	
		[Description("")]
        public string CreateUser { get{return _CreateUser;} set{_CreateUser = value??"";} } // CreateUser
		private string _CreateUser="";
		/// <summary></summary>	
		[Description("")]
        public DateTime? ModifyDate { get; set; } // ModifyDate
		/// <summary></summary>	
		[Description("")]
        public string ModifyUserID { get{return _ModifyUserID;} set{_ModifyUserID = value??"";} } // ModifyUserID
		private string _ModifyUserID="";
		/// <summary></summary>	
		[Description("")]
        public string ModifyUser { get{return _ModifyUser;} set{_ModifyUser = value??"";} } // ModifyUser
		private string _ModifyUser="";
    }

	/// <summary>工时项目维护</summary>	
	[Description("工时项目维护")]
    public partial class S_W_ProjectInfo : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID (Primary key)
		private string _ID="";
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? ModifyDate { get; set; } // ModifyDate
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get{return _CreateUserID;} set{_CreateUserID = value??"";} } // CreateUserID
		private string _CreateUserID="";
		/// <summary></summary>	
		[Description("")]
        public string CreateUser { get{return _CreateUser;} set{_CreateUser = value??"";} } // CreateUser
		private string _CreateUser="";
		/// <summary></summary>	
		[Description("")]
        public string ModifyUserID { get{return _ModifyUserID;} set{_ModifyUserID = value??"";} } // ModifyUserID
		private string _ModifyUserID="";
		/// <summary></summary>	
		[Description("")]
        public string ModifyUser { get{return _ModifyUser;} set{_ModifyUser = value??"";} } // ModifyUser
		private string _ModifyUser="";
		/// <summary></summary>	
		[Description("")]
        public string OrgID { get{return _OrgID;} set{_OrgID = value??"";} } // OrgID
		private string _OrgID="";
		/// <summary></summary>	
		[Description("")]
        public string CompanyID { get{return _CompanyID;} set{_CompanyID = value??"";} } // CompanyID
		private string _CompanyID="";
		/// <summary></summary>	
		[Description("")]
        public string FlowPhase { get{return _FlowPhase;} set{_FlowPhase = value??"";} } // FlowPhase
		private string _FlowPhase="";
		/// <summary></summary>	
		[Description("")]
        public string FlowInfo { get{return _FlowInfo;} set{_FlowInfo = value??"";} } // FlowInfo
		private string _FlowInfo="";
		/// <summary></summary>	
		[Description("")]
        public string StepName { get{return _StepName;} set{_StepName = value??"";} } // StepName
		private string _StepName="";
		/// <summary>项目名称</summary>	
		[Description("项目名称")]
        public string ProjectName { get{return _ProjectName;} set{_ProjectName = value??"";} } // ProjectName
		private string _ProjectName="";
		/// <summary>项目编号</summary>	
		[Description("项目编号")]
        public string ProjectCode { get{return _ProjectCode;} set{_ProjectCode = value??"";} } // ProjectCode
		private string _ProjectCode="";
		/// <summary>责任部门</summary>	
		[Description("责任部门")]
        public string ChargerDept { get{return _ChargerDept;} set{_ChargerDept = value??"";} } // ChargerDept
		private string _ChargerDept="";
		/// <summary>责任部门名称</summary>	
		[Description("责任部门名称")]
        public string ChargerDeptName { get{return _ChargerDeptName;} set{_ChargerDeptName = value??"";} } // ChargerDeptName
		private string _ChargerDeptName="";
		/// <summary>项目负责人</summary>	
		[Description("项目负责人")]
        public string ChargerUser { get{return _ChargerUser;} set{_ChargerUser = value??"";} } // ChargerUser
		private string _ChargerUser="";
		/// <summary>项目负责人名称</summary>	
		[Description("项目负责人名称")]
        public string ChargerUserName { get{return _ChargerUserName;} set{_ChargerUserName = value??"";} } // ChargerUserName
		private string _ChargerUserName="";
		/// <summary>分类</summary>	
		[Description("分类")]
        public string WorkHourType { get{return _WorkHourType;} set{_WorkHourType = value??"";} } // WorkHourType
		private string _WorkHourType="";
		/// <summary>是否有效</summary>	
		[Description("是否有效")]
        public string IsValid { get{return _IsValid;} set{_IsValid = value??"";} } // IsValid
		private string _IsValid="";

        // Reverse navigation
		[JsonIgnore]
        public virtual ICollection<S_W_ProjectInfo_SubProjectInfo> S_W_ProjectInfo_SubProjectInfo { get { onS_W_ProjectInfo_SubProjectInfoGetting(); return _S_W_ProjectInfo_SubProjectInfo;} }
		private ICollection<S_W_ProjectInfo_SubProjectInfo> _S_W_ProjectInfo_SubProjectInfo;
		partial void onS_W_ProjectInfo_SubProjectInfoGetting();


        public S_W_ProjectInfo()
        {
            _S_W_ProjectInfo_SubProjectInfo = new List<S_W_ProjectInfo_SubProjectInfo>();
        }
    }

	/// <summary>子项信息</summary>	
	[Description("子项信息")]
    public partial class S_W_ProjectInfo_SubProjectInfo : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID (Primary key)
		private string _ID="";
		/// <summary></summary>	
		[Description("")]
        public string S_W_ProjectInfoID { get{return _S_W_ProjectInfoID;} set{_S_W_ProjectInfoID = value??"";} } // S_W_ProjectInfoID
		private string _S_W_ProjectInfoID="";
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string IsReleased { get{return _IsReleased;} set{_IsReleased = value??"";} } // IsReleased
		private string _IsReleased="";
		/// <summary>子项名称</summary>	
		[Description("子项名称")]
        public string SubProjectName { get{return _SubProjectName;} set{_SubProjectName = value??"";} } // SubProjectName
		private string _SubProjectName="";
		/// <summary>子项编号</summary>	
		[Description("子项编号")]
        public string SubProjectCode { get{return _SubProjectCode;} set{_SubProjectCode = value??"";} } // SubProjectCode
		private string _SubProjectCode="";
		/// <summary>专业名称</summary>	
		[Description("专业名称")]
        public string MajorName { get{return _MajorName;} set{_MajorName = value??"";} } // MajorName
		private string _MajorName="";
		/// <summary>专业编号</summary>	
		[Description("专业编号")]
        public string MajorCode { get{return _MajorCode;} set{_MajorCode = value??"";} } // MajorCode
		private string _MajorCode="";

        // Foreign keys
		[JsonIgnore]
        public virtual S_W_ProjectInfo S_W_ProjectInfo { get; set; } //  S_W_ProjectInfoID - FK_S_W_ProjectInfo_SubProjectInfo_S_W_ProjectInfo
    }

	/// <summary></summary>	
	[Description("")]
    public partial class S_W_ResourcePrice : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID (Primary key)
		private string _ID="";
		/// <summary></summary>	
		[Description("")]
        public string ResourceCode { get{return _ResourceCode;} set{_ResourceCode = value??"";} } // ResourceCode
		private string _ResourceCode="";
		/// <summary></summary>	
		[Description("")]
        public string ResourceName { get{return _ResourceName;} set{_ResourceName = value??"";} } // ResourceName
		private string _ResourceName="";
		/// <summary></summary>	
		[Description("")]
        public decimal UnitPrice { get; set; } // UnitPrice
		/// <summary></summary>	
		[Description("")]
        public int BelongYear { get; set; } // BelongYear
		/// <summary></summary>	
		[Description("")]
        public int BelongMonth { get; set; } // BelongMonth
		/// <summary></summary>	
		[Description("")]
        public DateTime StartDate { get; set; } // StartDate
		/// <summary></summary>	
		[Description("")]
        public string Level { get{return _Level;} set{_Level = value??"";} } // Level
		private string _Level="";
		/// <summary></summary>	
		[Description("")]
        public string Postion { get{return _Postion;} set{_Postion = value??"";} } // Postion
		private string _Postion="";
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public string CreateUser { get{return _CreateUser;} set{_CreateUser = value??"";} } // CreateUser
		private string _CreateUser="";
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get{return _CreateUserID;} set{_CreateUserID = value??"";} } // CreateUserID
		private string _CreateUserID="";
		/// <summary></summary>	
		[Description("")]
        public DateTime? ModifyDate { get; set; } // ModifyDate
		/// <summary></summary>	
		[Description("")]
        public string ModifyUser { get{return _ModifyUser;} set{_ModifyUser = value??"";} } // ModifyUser
		private string _ModifyUser="";
		/// <summary></summary>	
		[Description("")]
        public string ModifyUserID { get{return _ModifyUserID;} set{_ModifyUserID = value??"";} } // ModifyUserID
		private string _ModifyUserID="";
    }

	/// <summary>人员薪资</summary>	
	[Description("人员薪资")]
    public partial class S_W_UserSalary : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID (Primary key)
		private string _ID="";
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? ModifyDate { get; set; } // ModifyDate
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get{return _CreateUserID;} set{_CreateUserID = value??"";} } // CreateUserID
		private string _CreateUserID="";
		/// <summary></summary>	
		[Description("")]
        public string CreateUser { get{return _CreateUser;} set{_CreateUser = value??"";} } // CreateUser
		private string _CreateUser="";
		/// <summary></summary>	
		[Description("")]
        public string ModifyUserID { get{return _ModifyUserID;} set{_ModifyUserID = value??"";} } // ModifyUserID
		private string _ModifyUserID="";
		/// <summary></summary>	
		[Description("")]
        public string ModifyUser { get{return _ModifyUser;} set{_ModifyUser = value??"";} } // ModifyUser
		private string _ModifyUser="";
		/// <summary></summary>	
		[Description("")]
        public string OrgID { get{return _OrgID;} set{_OrgID = value??"";} } // OrgID
		private string _OrgID="";
		/// <summary></summary>	
		[Description("")]
        public string CompanyID { get{return _CompanyID;} set{_CompanyID = value??"";} } // CompanyID
		private string _CompanyID="";
		/// <summary></summary>	
		[Description("")]
        public string FlowPhase { get{return _FlowPhase;} set{_FlowPhase = value??"";} } // FlowPhase
		private string _FlowPhase="";
		/// <summary></summary>	
		[Description("")]
        public string FlowInfo { get{return _FlowInfo;} set{_FlowInfo = value??"";} } // FlowInfo
		private string _FlowInfo="";
		/// <summary></summary>	
		[Description("")]
        public string StepName { get{return _StepName;} set{_StepName = value??"";} } // StepName
		private string _StepName="";
		/// <summary>年份</summary>	
		[Description("年份")]
        public string BelongYear { get{return _BelongYear;} set{_BelongYear = value??"";} } // BelongYear
		private string _BelongYear="";
		/// <summary>月份</summary>	
		[Description("月份")]
        public string BelongMonth { get{return _BelongMonth;} set{_BelongMonth = value??"";} } // BelongMonth
		private string _BelongMonth="";
		/// <summary>人员</summary>	
		[Description("人员")]
        public string UserInfo { get{return _UserInfo;} set{_UserInfo = value??"";} } // UserInfo
		private string _UserInfo="";
		/// <summary>人员名称</summary>	
		[Description("人员名称")]
        public string UserInfoName { get{return _UserInfoName;} set{_UserInfoName = value??"";} } // UserInfoName
		private string _UserInfoName="";
		/// <summary>部门</summary>	
		[Description("部门")]
        public string DepartInfo { get{return _DepartInfo;} set{_DepartInfo = value??"";} } // DepartInfo
		private string _DepartInfo="";
		/// <summary>部门名称</summary>	
		[Description("部门名称")]
        public string DepartInfoName { get{return _DepartInfoName;} set{_DepartInfoName = value??"";} } // DepartInfoName
		private string _DepartInfoName="";
		/// <summary>基本工资（元）</summary>	
		[Description("基本工资（元）")]
        public decimal? BasicSalary { get; set; } // BasicSalary
		/// <summary>奖金（元）</summary>	
		[Description("奖金（元）")]
        public decimal? Bonus { get; set; } // Bonus
		/// <summary>福利费（元）</summary>	
		[Description("福利费（元）")]
        public decimal? Benefit { get; set; } // Benefit
		/// <summary>五险一金（元）</summary>	
		[Description("五险一金（元）")]
        public decimal? FiveOne { get; set; } // FiveOne
		/// <summary>其他（元）</summary>	
		[Description("其他（元）")]
        public decimal? OtherValue { get; set; } // OtherValue
		/// <summary>总计</summary>	
		[Description("总计")]
        public decimal? TotalValue { get; set; } // TotalValue
    }

	/// <summary></summary>	
	[Description("")]
    public partial class S_W_UserUnitPrice : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID (Primary key)
		private string _ID="";
		/// <summary></summary>	
		[Description("")]
        public string HRUserID { get{return _HRUserID;} set{_HRUserID = value??"";} } // HRUserID
		private string _HRUserID="";
		/// <summary></summary>	
		[Description("")]
        public string UserID { get{return _UserID;} set{_UserID = value??"";} } // UserID
		private string _UserID="";
		/// <summary></summary>	
		[Description("")]
        public string UserName { get{return _UserName;} set{_UserName = value??"";} } // UserName
		private string _UserName="";
		/// <summary></summary>	
		[Description("")]
        public decimal UnitPrice { get; set; } // UnitPrice
		/// <summary></summary>	
		[Description("")]
        public string ResourceCode { get{return _ResourceCode;} set{_ResourceCode = value??"";} } // ResourceCode
		private string _ResourceCode="";
		/// <summary></summary>	
		[Description("")]
        public string PriceType { get{return _PriceType;} set{_PriceType = value??"";} } // PriceType
		private string _PriceType="";
		/// <summary></summary>	
		[Description("")]
        public int BelongYear { get; set; } // BelongYear
		/// <summary></summary>	
		[Description("")]
        public int BelongMonth { get; set; } // BelongMonth
		/// <summary></summary>	
		[Description("")]
        public DateTime StartDate { get; set; } // StartDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? ModifyDate { get; set; } // ModifyDate
		/// <summary></summary>	
		[Description("")]
        public string ModifyUser { get{return _ModifyUser;} set{_ModifyUser = value??"";} } // ModifyUser
		private string _ModifyUser="";
		/// <summary></summary>	
		[Description("")]
        public string ModifyUserID { get{return _ModifyUserID;} set{_ModifyUserID = value??"";} } // ModifyUserID
		private string _ModifyUserID="";
    }

	/// <summary>工时审批增加字段</summary>	
	[Description("工时审批增加字段")]
    public partial class S_W_UserWorkHour : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID (Primary key)
		private string _ID="";
		/// <summary></summary>	
		[Description("")]
        public string UserID { get{return _UserID;} set{_UserID = value??"";} } // UserID
		private string _UserID="";
		/// <summary></summary>	
		[Description("")]
        public string UserName { get{return _UserName;} set{_UserName = value??"";} } // UserName
		private string _UserName="";
		/// <summary></summary>	
		[Description("")]
        public string UserCode { get{return _UserCode;} set{_UserCode = value??"";} } // UserCode
		private string _UserCode="";
		/// <summary></summary>	
		[Description("")]
        public string EmployeeID { get{return _EmployeeID;} set{_EmployeeID = value??"";} } // EmployeeID
		private string _EmployeeID="";
		/// <summary></summary>	
		[Description("")]
        public string UserDeptID { get{return _UserDeptID;} set{_UserDeptID = value??"";} } // UserDeptID
		private string _UserDeptID="";
		/// <summary></summary>	
		[Description("")]
        public string UserDeptName { get{return _UserDeptName;} set{_UserDeptName = value??"";} } // UserDeptName
		private string _UserDeptName="";
		/// <summary></summary>	
		[Description("")]
        public DateTime WorkHourDate { get; set; } // WorkHourDate
		/// <summary></summary>	
		[Description("")]
        public decimal? NormalValue { get; set; } // NormalValue
		/// <summary></summary>	
		[Description("")]
        public decimal? AdditionalValue { get; set; } // AdditionalValue
		/// <summary></summary>	
		[Description("")]
        public decimal? WorkHourValue { get; set; } // WorkHourValue
		/// <summary></summary>	
		[Description("")]
        public decimal? ConfirmValue { get; set; } // ConfirmValue
		/// <summary></summary>	
		[Description("")]
        public string State { get{return _State;} set{_State = value??"";} } // State
		private string _State="";
		/// <summary></summary>	
		[Description("")]
        public int BelongYear { get; set; } // BelongYear
		/// <summary></summary>	
		[Description("")]
        public int BelongMonth { get; set; } // BelongMonth
		/// <summary></summary>	
		[Description("")]
        public int BelongQuarter { get; set; } // BelongQuarter
		/// <summary></summary>	
		[Description("")]
        public string WorkHourType { get{return _WorkHourType;} set{_WorkHourType = value??"";} } // WorkHourType
		private string _WorkHourType="";
		/// <summary></summary>	
		[Description("")]
        public string ProjectID { get{return _ProjectID;} set{_ProjectID = value??"";} } // ProjectID
		private string _ProjectID="";
		/// <summary></summary>	
		[Description("")]
        public string ProjectCode { get{return _ProjectCode;} set{_ProjectCode = value??"";} } // ProjectCode
		private string _ProjectCode="";
		/// <summary></summary>	
		[Description("")]
        public string ProjectName { get{return _ProjectName;} set{_ProjectName = value??"";} } // ProjectName
		private string _ProjectName="";
		/// <summary></summary>	
		[Description("")]
        public string ProjectDept { get{return _ProjectDept;} set{_ProjectDept = value??"";} } // ProjectDept
		private string _ProjectDept="";
		/// <summary></summary>	
		[Description("")]
        public string ProjectDeptName { get{return _ProjectDeptName;} set{_ProjectDeptName = value??"";} } // ProjectDeptName
		private string _ProjectDeptName="";
		/// <summary></summary>	
		[Description("")]
        public string ProjectChargerUser { get{return _ProjectChargerUser;} set{_ProjectChargerUser = value??"";} } // ProjectChargerUser
		private string _ProjectChargerUser="";
		/// <summary></summary>	
		[Description("")]
        public string ProjectChargerUserName { get{return _ProjectChargerUserName;} set{_ProjectChargerUserName = value??"";} } // ProjectChargerUserName
		private string _ProjectChargerUserName="";
		/// <summary></summary>	
		[Description("")]
        public string SubProjectName { get{return _SubProjectName;} set{_SubProjectName = value??"";} } // SubProjectName
		private string _SubProjectName="";
		/// <summary></summary>	
		[Description("")]
        public string SubProjectCode { get{return _SubProjectCode;} set{_SubProjectCode = value??"";} } // SubProjectCode
		private string _SubProjectCode="";
		/// <summary></summary>	
		[Description("")]
        public string MajorCode { get{return _MajorCode;} set{_MajorCode = value??"";} } // MajorCode
		private string _MajorCode="";
		/// <summary></summary>	
		[Description("")]
        public string MajorName { get{return _MajorName;} set{_MajorName = value??"";} } // MajorName
		private string _MajorName="";
		/// <summary></summary>	
		[Description("")]
        public string TaskWorkCode { get{return _TaskWorkCode;} set{_TaskWorkCode = value??"";} } // TaskWorkCode
		private string _TaskWorkCode="";
		/// <summary></summary>	
		[Description("")]
        public string TaskWorkName { get{return _TaskWorkName;} set{_TaskWorkName = value??"";} } // TaskWorkName
		private string _TaskWorkName="";
		/// <summary></summary>	
		[Description("")]
        public string WorkContent { get{return _WorkContent;} set{_WorkContent = value??"";} } // WorkContent
		private string _WorkContent="";
		/// <summary></summary>	
		[Description("")]
        public decimal? WorkHourDay { get; set; } // WorkHourDay
		/// <summary></summary>	
		[Description("")]
        public decimal? ConfirmDay { get; set; } // ConfirmDay
		/// <summary>第一步审批工时</summary>	
		[Description("第一步审批工时")]
        public decimal? Step1Value { get; set; } // Step1Value
		/// <summary>第一步审批工日</summary>	
		[Description("第一步审批工日")]
        public decimal? Step1Day { get; set; } // Step1Day
		/// <summary>第二步审批工时</summary>	
		[Description("第二步审批工时")]
        public decimal? Step2Value { get; set; } // Step2Value
		/// <summary>第二步审批工日</summary>	
		[Description("第二步审批工日")]
        public decimal? Step2Day { get; set; } // Step2Day
		/// <summary>第一步审批人</summary>	
		[Description("第一步审批人")]
        public string Step1User { get{return _Step1User;} set{_Step1User = value??"";} } // Step1User
		private string _Step1User="";
		/// <summary>第一步审批人名称</summary>	
		[Description("第一步审批人名称")]
        public string Step1UserName { get{return _Step1UserName;} set{_Step1UserName = value??"";} } // Step1UserName
		private string _Step1UserName="";
		/// <summary>第一步审批时间</summary>	
		[Description("第一步审批时间")]
        public DateTime? Step1Date { get; set; } // Step1Date
		/// <summary>第一步是否审批(0、1)</summary>	
		[Description("第一步是否审批(0、1)")]
        public string IsStep1 { get{return _IsStep1;} set{_IsStep1 = value??"";} } // IsStep1
		private string _IsStep1="";
		/// <summary>第二步审批人</summary>	
		[Description("第二步审批人")]
        public string Step2User { get{return _Step2User;} set{_Step2User = value??"";} } // Step2User
		private string _Step2User="";
		/// <summary>第二步审批人名称</summary>	
		[Description("第二步审批人名称")]
        public string Step2UserName { get{return _Step2UserName;} set{_Step2UserName = value??"";} } // Step2UserName
		private string _Step2UserName="";
		/// <summary>第二步审批时间</summary>	
		[Description("第二步审批时间")]
        public DateTime? Step2Date { get; set; } // Step2Date
		/// <summary>第二步是否审批(0、1)</summary>	
		[Description("第二步是否审批(0、1)")]
        public string IsStep2 { get{return _IsStep2;} set{_IsStep2 = value??"";} } // IsStep2
		private string _IsStep2="";
		/// <summary>最终确认人</summary>	
		[Description("最终确认人")]
        public string ConfirmUser { get{return _ConfirmUser;} set{_ConfirmUser = value??"";} } // ConfirmUser
		private string _ConfirmUser="";
		/// <summary>最终确认人名称</summary>	
		[Description("最终确认人名称")]
        public string ConfirmUserName { get{return _ConfirmUserName;} set{_ConfirmUserName = value??"";} } // ConfirmUserName
		private string _ConfirmUserName="";
		/// <summary>最终确认时间</summary>	
		[Description("最终确认时间")]
        public DateTime? ConfirmDate { get; set; } // ConfirmDate
		/// <summary>是否最终确认(0、1)</summary>	
		[Description("是否最终确认(0、1)")]
        public string IsConfirm { get{return _IsConfirm;} set{_IsConfirm = value??"";} } // IsConfirm
		private string _IsConfirm="";
		/// <summary>CreateUserID</summary>	
		[Description("CreateUserID")]
        public string CreateUserID { get{return _CreateUserID;} set{_CreateUserID = value??"";} } // CreateUserID
		private string _CreateUserID="";
		/// <summary>CreateUser</summary>	
		[Description("CreateUser")]
        public string CreateUser { get{return _CreateUser;} set{_CreateUser = value??"";} } // CreateUser
		private string _CreateUser="";
		/// <summary>补填记录ID</summary>	
		[Description("补填记录ID")]
        public string SupplementID { get{return _SupplementID;} set{_SupplementID = value??"";} } // SupplementID
		private string _SupplementID="";

        // Reverse navigation
		[JsonIgnore]
        public virtual ICollection<S_W_UserWorkHour_ApproveDetail> S_W_UserWorkHour_ApproveDetail { get { onS_W_UserWorkHour_ApproveDetailGetting(); return _S_W_UserWorkHour_ApproveDetail;} }
		private ICollection<S_W_UserWorkHour_ApproveDetail> _S_W_UserWorkHour_ApproveDetail;
		partial void onS_W_UserWorkHour_ApproveDetailGetting();


        public S_W_UserWorkHour()
        {
            _S_W_UserWorkHour_ApproveDetail = new List<S_W_UserWorkHour_ApproveDetail>();
        }
    }

	/// <summary>审批意见</summary>	
	[Description("审批意见")]
    public partial class S_W_UserWorkHour_ApproveDetail : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID (Primary key)
		private string _ID="";
		/// <summary></summary>	
		[Description("")]
        public string S_W_UserWorkHourID { get{return _S_W_UserWorkHourID;} set{_S_W_UserWorkHourID = value??"";} } // S_W_UserWorkHourID
		private string _S_W_UserWorkHourID="";
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string IsReleased { get{return _IsReleased;} set{_IsReleased = value??"";} } // IsReleased
		private string _IsReleased="";
		/// <summary>审批工时</summary>	
		[Description("审批工时")]
        public decimal? ApproveValue { get; set; } // ApproveValue
		/// <summary>审批工日</summary>	
		[Description("审批工日")]
        public decimal? ApproveDay { get; set; } // ApproveDay
		/// <summary>审批时间</summary>	
		[Description("审批时间")]
        public DateTime? ApproveDate { get; set; } // ApproveDate
		/// <summary>审批环节</summary>	
		[Description("审批环节")]
        public string ApproveStep { get{return _ApproveStep;} set{_ApproveStep = value??"";} } // ApproveStep
		private string _ApproveStep="";
		/// <summary>审批人</summary>	
		[Description("审批人")]
        public string ApproveUser { get{return _ApproveUser;} set{_ApproveUser = value??"";} } // ApproveUser
		private string _ApproveUser="";
		/// <summary>审批人名称</summary>	
		[Description("审批人名称")]
        public string ApproveUserName { get{return _ApproveUserName;} set{_ApproveUserName = value??"";} } // ApproveUserName
		private string _ApproveUserName="";

        // Foreign keys
		[JsonIgnore]
        public virtual S_W_UserWorkHour S_W_UserWorkHour { get; set; } //  S_W_UserWorkHourID - FK_S_W_UserWorkHour_ApproveDetail_S_W_UserWorkHour
    }

	/// <summary>工时批量补填</summary>	
	[Description("工时批量补填")]
    public partial class S_W_UserWorkHourSupplement : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID (Primary key)
		private string _ID="";
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? ModifyDate { get; set; } // ModifyDate
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get{return _CreateUserID;} set{_CreateUserID = value??"";} } // CreateUserID
		private string _CreateUserID="";
		/// <summary></summary>	
		[Description("")]
        public string CreateUser { get{return _CreateUser;} set{_CreateUser = value??"";} } // CreateUser
		private string _CreateUser="";
		/// <summary></summary>	
		[Description("")]
        public string ModifyUserID { get{return _ModifyUserID;} set{_ModifyUserID = value??"";} } // ModifyUserID
		private string _ModifyUserID="";
		/// <summary></summary>	
		[Description("")]
        public string ModifyUser { get{return _ModifyUser;} set{_ModifyUser = value??"";} } // ModifyUser
		private string _ModifyUser="";
		/// <summary></summary>	
		[Description("")]
        public string OrgID { get{return _OrgID;} set{_OrgID = value??"";} } // OrgID
		private string _OrgID="";
		/// <summary></summary>	
		[Description("")]
        public string CompanyID { get{return _CompanyID;} set{_CompanyID = value??"";} } // CompanyID
		private string _CompanyID="";
		/// <summary></summary>	
		[Description("")]
        public string FlowPhase { get{return _FlowPhase;} set{_FlowPhase = value??"";} } // FlowPhase
		private string _FlowPhase="";
		/// <summary></summary>	
		[Description("")]
        public string FlowInfo { get{return _FlowInfo;} set{_FlowInfo = value??"";} } // FlowInfo
		private string _FlowInfo="";
		/// <summary></summary>	
		[Description("")]
        public string StepName { get{return _StepName;} set{_StepName = value??"";} } // StepName
		private string _StepName="";
		/// <summary>人员</summary>	
		[Description("人员")]
        public string UserID { get{return _UserID;} set{_UserID = value??"";} } // UserID
		private string _UserID="";
		/// <summary>人员名称</summary>	
		[Description("人员名称")]
        public string UserIDName { get{return _UserIDName;} set{_UserIDName = value??"";} } // UserIDName
		private string _UserIDName="";
		/// <summary>部门</summary>	
		[Description("部门")]
        public string UserDept { get{return _UserDept;} set{_UserDept = value??"";} } // UserDept
		private string _UserDept="";
		/// <summary>部门名称</summary>	
		[Description("部门名称")]
        public string UserDeptName { get{return _UserDeptName;} set{_UserDeptName = value??"";} } // UserDeptName
		private string _UserDeptName="";
		/// <summary>项目</summary>	
		[Description("项目")]
        public string ProjectID { get{return _ProjectID;} set{_ProjectID = value??"";} } // ProjectID
		private string _ProjectID="";
		/// <summary>项目名称</summary>	
		[Description("项目名称")]
        public string ProjectIDName { get{return _ProjectIDName;} set{_ProjectIDName = value??"";} } // ProjectIDName
		private string _ProjectIDName="";
		/// <summary>类别</summary>	
		[Description("类别")]
        public string WorkHourType { get{return _WorkHourType;} set{_WorkHourType = value??"";} } // WorkHourType
		private string _WorkHourType="";
		/// <summary>子项</summary>	
		[Description("子项")]
        public string SubProjectCode { get{return _SubProjectCode;} set{_SubProjectCode = value??"";} } // SubProjectCode
		private string _SubProjectCode="";
		/// <summary>专业</summary>	
		[Description("专业")]
        public string MajorCode { get{return _MajorCode;} set{_MajorCode = value??"";} } // MajorCode
		private string _MajorCode="";
		/// <summary>专业名称</summary>	
		[Description("专业名称")]
        public string MajorName { get{return _MajorName;} set{_MajorName = value??"";} } // MajorName
		private string _MajorName="";
		/// <summary>工作内容</summary>	
		[Description("工作内容")]
        public string WorkContent { get{return _WorkContent;} set{_WorkContent = value??"";} } // WorkContent
		private string _WorkContent="";
		/// <summary>工时日期开始</summary>	
		[Description("工时日期开始")]
        public DateTime? WorkHourDateStart { get; set; } // WorkHourDateStart
		/// <summary>工时日期结束</summary>	
		[Description("工时日期结束")]
        public DateTime? WorkHourDateEnd { get; set; } // WorkHourDateEnd
		/// <summary>正班工时</summary>	
		[Description("正班工时")]
        public decimal? NormalValue { get; set; } // NormalValue
		/// <summary>加班工时</summary>	
		[Description("加班工时")]
        public decimal? AdditionalValue { get; set; } // AdditionalValue
		/// <summary>合计</summary>	
		[Description("合计")]
        public decimal? WorkHourValue { get; set; } // WorkHourValue
		/// <summary>UserName</summary>	
		[Description("UserName")]
        public string UserName { get{return _UserName;} set{_UserName = value??"";} } // UserName
		private string _UserName="";
		/// <summary>UserDeptID</summary>	
		[Description("UserDeptID")]
        public string UserDeptID { get{return _UserDeptID;} set{_UserDeptID = value??"";} } // UserDeptID
		private string _UserDeptID="";
		/// <summary>项目编号</summary>	
		[Description("项目编号")]
        public string ProjectCode { get{return _ProjectCode;} set{_ProjectCode = value??"";} } // ProjectCode
		private string _ProjectCode="";
		/// <summary>项目名称</summary>	
		[Description("项目名称")]
        public string ProjectName { get{return _ProjectName;} set{_ProjectName = value??"";} } // ProjectName
		private string _ProjectName="";
		/// <summary>项目所属部门</summary>	
		[Description("项目所属部门")]
        public string ProjectDept { get{return _ProjectDept;} set{_ProjectDept = value??"";} } // ProjectDept
		private string _ProjectDept="";
		/// <summary>项目所属部门名称</summary>	
		[Description("项目所属部门名称")]
        public string ProjectDeptName { get{return _ProjectDeptName;} set{_ProjectDeptName = value??"";} } // ProjectDeptName
		private string _ProjectDeptName="";
		/// <summary>项目负责人</summary>	
		[Description("项目负责人")]
        public string ProjectChargerUser { get{return _ProjectChargerUser;} set{_ProjectChargerUser = value??"";} } // ProjectChargerUser
		private string _ProjectChargerUser="";
		/// <summary>项目负责人名称</summary>	
		[Description("项目负责人名称")]
        public string ProjectChargerUserName { get{return _ProjectChargerUserName;} set{_ProjectChargerUserName = value??"";} } // ProjectChargerUserName
		private string _ProjectChargerUserName="";
		/// <summary>子项名称</summary>	
		[Description("子项名称")]
        public string SubProjectName { get{return _SubProjectName;} set{_SubProjectName = value??"";} } // SubProjectName
		private string _SubProjectName="";
		/// <summary>工作包编号</summary>	
		[Description("工作包编号")]
        public string TaskWorkCode { get{return _TaskWorkCode;} set{_TaskWorkCode = value??"";} } // TaskWorkCode
		private string _TaskWorkCode="";
		/// <summary>工作包名称</summary>	
		[Description("工作包名称")]
        public string TaskWorkName { get{return _TaskWorkName;} set{_TaskWorkName = value??"";} } // TaskWorkName
		private string _TaskWorkName="";
    }

	/// <summary>出差申请</summary>	
	[Description("出差申请")]
    public partial class T_AttendanceBusinessApply : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID (Primary key)
		private string _ID="";
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? ModifyDate { get; set; } // ModifyDate
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get{return _CreateUserID;} set{_CreateUserID = value??"";} } // CreateUserID
		private string _CreateUserID="";
		/// <summary></summary>	
		[Description("")]
        public string CreateUser { get{return _CreateUser;} set{_CreateUser = value??"";} } // CreateUser
		private string _CreateUser="";
		/// <summary></summary>	
		[Description("")]
        public string ModifyUserID { get{return _ModifyUserID;} set{_ModifyUserID = value??"";} } // ModifyUserID
		private string _ModifyUserID="";
		/// <summary></summary>	
		[Description("")]
        public string ModifyUser { get{return _ModifyUser;} set{_ModifyUser = value??"";} } // ModifyUser
		private string _ModifyUser="";
		/// <summary></summary>	
		[Description("")]
        public string OrgID { get{return _OrgID;} set{_OrgID = value??"";} } // OrgID
		private string _OrgID="";
		/// <summary></summary>	
		[Description("")]
        public string CompanyID { get{return _CompanyID;} set{_CompanyID = value??"";} } // CompanyID
		private string _CompanyID="";
		/// <summary>FlowPhase</summary>	
		[Description("FlowPhase")]
        public string FlowPhase { get{return _FlowPhase;} set{_FlowPhase = value??"";} } // FlowPhase
		private string _FlowPhase="";
		/// <summary></summary>	
		[Description("")]
        public string FlowInfo { get{return _FlowInfo;} set{_FlowInfo = value??"";} } // FlowInfo
		private string _FlowInfo="";
		/// <summary></summary>	
		[Description("")]
        public string StepName { get{return _StepName;} set{_StepName = value??"";} } // StepName
		private string _StepName="";
		/// <summary>申请人</summary>	
		[Description("申请人")]
        public string ApplyUser { get{return _ApplyUser;} set{_ApplyUser = value??"";} } // ApplyUser
		private string _ApplyUser="";
		/// <summary>申请人名称</summary>	
		[Description("申请人名称")]
        public string ApplyUserName { get{return _ApplyUserName;} set{_ApplyUserName = value??"";} } // ApplyUserName
		private string _ApplyUserName="";
		/// <summary>申请时间</summary>	
		[Description("申请时间")]
        public DateTime? ApplyDate { get; set; } // ApplyDate
		/// <summary>申请人部门</summary>	
		[Description("申请人部门")]
        public string ApplyUserDept { get{return _ApplyUserDept;} set{_ApplyUserDept = value??"";} } // ApplyUserDept
		private string _ApplyUserDept="";
		/// <summary>申请人部门名称</summary>	
		[Description("申请人部门名称")]
        public string ApplyUserDeptName { get{return _ApplyUserDeptName;} set{_ApplyUserDeptName = value??"";} } // ApplyUserDeptName
		private string _ApplyUserDeptName="";
		/// <summary>申请项目</summary>	
		[Description("申请项目")]
        public string ProjectInfo { get{return _ProjectInfo;} set{_ProjectInfo = value??"";} } // ProjectInfo
		private string _ProjectInfo="";
		/// <summary>申请项目名称</summary>	
		[Description("申请项目名称")]
        public string ProjectInfoName { get{return _ProjectInfoName;} set{_ProjectInfoName = value??"";} } // ProjectInfoName
		private string _ProjectInfoName="";
		/// <summary>出差目的地</summary>	
		[Description("出差目的地")]
        public string Destination { get{return _Destination;} set{_Destination = value??"";} } // Destination
		private string _Destination="";
		/// <summary>同行人员</summary>	
		[Description("同行人员")]
        public string Partners { get{return _Partners;} set{_Partners = value??"";} } // Partners
		private string _Partners="";
		/// <summary>同行人员名称</summary>	
		[Description("同行人员名称")]
        public string PartnersName { get{return _PartnersName;} set{_PartnersName = value??"";} } // PartnersName
		private string _PartnersName="";
		/// <summary>出差事由</summary>	
		[Description("出差事由")]
        public string Reason { get{return _Reason;} set{_Reason = value??"";} } // Reason
		private string _Reason="";
		/// <summary>出差乘坐交通工具</summary>	
		[Description("出差乘坐交通工具")]
        public string Transportation { get{return _Transportation;} set{_Transportation = value??"";} } // Transportation
		private string _Transportation="";
		/// <summary>附件</summary>	
		[Description("附件")]
        public string Attachments { get{return _Attachments;} set{_Attachments = value??"";} } // Attachments
		private string _Attachments="";
		/// <summary>备注</summary>	
		[Description("备注")]
        public string Remark { get{return _Remark;} set{_Remark = value??"";} } // Remark
		private string _Remark="";
		/// <summary>申请单编号</summary>	
		[Description("申请单编号")]
        public string SerialNumber { get{return _SerialNumber;} set{_SerialNumber = value??"";} } // SerialNumber
		private string _SerialNumber="";
		/// <summary>预计出差天数</summary>	
		[Description("预计出差天数")]
        public double? TravelDays { get; set; } // TravelDays
		/// <summary>出差时间起</summary>	
		[Description("出差时间起")]
        public DateTime? StartDate { get; set; } // StartDate
		/// <summary>出差时间起上下午</summary>	
		[Description("出差时间起上下午")]
        public string StartFlag { get{return _StartFlag;} set{_StartFlag = value??"";} } // StartFlag
		private string _StartFlag="";
		/// <summary>出差时间止</summary>	
		[Description("出差时间止")]
        public DateTime? EndDate { get; set; } // EndDate
		/// <summary>出差时间止上下午</summary>	
		[Description("出差时间止上下午")]
        public string EndFlag { get{return _EndFlag;} set{_EndFlag = value??"";} } // EndFlag
		private string _EndFlag="";
		/// <summary>公司领导意见</summary>	
		[Description("公司领导意见")]
        public string GSLDYJ { get{return _GSLDYJ;} set{_GSLDYJ = value??"";} } // GSLDYJ
		private string _GSLDYJ="";
    }

	/// <summary>请假申请</summary>	
	[Description("请假申请")]
    public partial class T_AttendanceLeaveApply : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID (Primary key)
		private string _ID="";
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? ModifyDate { get; set; } // ModifyDate
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get{return _CreateUserID;} set{_CreateUserID = value??"";} } // CreateUserID
		private string _CreateUserID="";
		/// <summary></summary>	
		[Description("")]
        public string CreateUser { get{return _CreateUser;} set{_CreateUser = value??"";} } // CreateUser
		private string _CreateUser="";
		/// <summary></summary>	
		[Description("")]
        public string ModifyUserID { get{return _ModifyUserID;} set{_ModifyUserID = value??"";} } // ModifyUserID
		private string _ModifyUserID="";
		/// <summary></summary>	
		[Description("")]
        public string ModifyUser { get{return _ModifyUser;} set{_ModifyUser = value??"";} } // ModifyUser
		private string _ModifyUser="";
		/// <summary></summary>	
		[Description("")]
        public string OrgID { get{return _OrgID;} set{_OrgID = value??"";} } // OrgID
		private string _OrgID="";
		/// <summary></summary>	
		[Description("")]
        public string CompanyID { get{return _CompanyID;} set{_CompanyID = value??"";} } // CompanyID
		private string _CompanyID="";
		/// <summary></summary>	
		[Description("")]
        public string FlowPhase { get{return _FlowPhase;} set{_FlowPhase = value??"";} } // FlowPhase
		private string _FlowPhase="";
		/// <summary></summary>	
		[Description("")]
        public string FlowInfo { get{return _FlowInfo;} set{_FlowInfo = value??"";} } // FlowInfo
		private string _FlowInfo="";
		/// <summary></summary>	
		[Description("")]
        public string StepName { get{return _StepName;} set{_StepName = value??"";} } // StepName
		private string _StepName="";
		/// <summary>申请人</summary>	
		[Description("申请人")]
        public string ApplyUser { get{return _ApplyUser;} set{_ApplyUser = value??"";} } // ApplyUser
		private string _ApplyUser="";
		/// <summary>申请人名称</summary>	
		[Description("申请人名称")]
        public string ApplyUserName { get{return _ApplyUserName;} set{_ApplyUserName = value??"";} } // ApplyUserName
		private string _ApplyUserName="";
		/// <summary>申请部门</summary>	
		[Description("申请部门")]
        public string ApplyDept { get{return _ApplyDept;} set{_ApplyDept = value??"";} } // ApplyDept
		private string _ApplyDept="";
		/// <summary>申请部门名称</summary>	
		[Description("申请部门名称")]
        public string ApplyDeptName { get{return _ApplyDeptName;} set{_ApplyDeptName = value??"";} } // ApplyDeptName
		private string _ApplyDeptName="";
		/// <summary>请假时间起</summary>	
		[Description("请假时间起")]
        public DateTime? StartDate { get; set; } // StartDate
		/// <summary>请假时间起上下午</summary>	
		[Description("请假时间起上下午")]
        public string StartFlag { get{return _StartFlag;} set{_StartFlag = value??"";} } // StartFlag
		private string _StartFlag="";
		/// <summary>请假时间止</summary>	
		[Description("请假时间止")]
        public DateTime? EndDate { get; set; } // EndDate
		/// <summary>请假时间止上下午</summary>	
		[Description("请假时间止上下午")]
        public string EndFlag { get{return _EndFlag;} set{_EndFlag = value??"";} } // EndFlag
		private string _EndFlag="";
		/// <summary>请假类别</summary>	
		[Description("请假类别")]
        public string LeaveType { get{return _LeaveType;} set{_LeaveType = value??"";} } // LeaveType
		private string _LeaveType="";
		/// <summary>请假原因</summary>	
		[Description("请假原因")]
        public string LeaveReason { get{return _LeaveReason;} set{_LeaveReason = value??"";} } // LeaveReason
		private string _LeaveReason="";
		/// <summary>部门领导审批</summary>	
		[Description("部门领导审批")]
        public string DeptSign { get{return _DeptSign;} set{_DeptSign = value??"";} } // DeptSign
		private string _DeptSign="";
		/// <summary>公司领导审批</summary>	
		[Description("公司领导审批")]
        public string CompanySign { get{return _CompanySign;} set{_CompanySign = value??"";} } // CompanySign
		private string _CompanySign="";
		/// <summary>请假天数</summary>	
		[Description("请假天数")]
        public string ApplyDays { get{return _ApplyDays;} set{_ApplyDays = value??"";} } // ApplyDays
		private string _ApplyDays="";
		/// <summary>综合管理部审批</summary>	
		[Description("综合管理部审批")]
        public string ManagementSign { get{return _ManagementSign;} set{_ManagementSign = value??"";} } // ManagementSign
		private string _ManagementSign="";
    }

	/// <summary>资质申请</summary>	
	[Description("资质申请")]
    public partial class T_C_QualificationApply : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID (Primary key)
		private string _ID="";
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? ModifyDate { get; set; } // ModifyDate
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get{return _CreateUserID;} set{_CreateUserID = value??"";} } // CreateUserID
		private string _CreateUserID="";
		/// <summary></summary>	
		[Description("")]
        public string CreateUser { get{return _CreateUser;} set{_CreateUser = value??"";} } // CreateUser
		private string _CreateUser="";
		/// <summary></summary>	
		[Description("")]
        public string ModifyUserID { get{return _ModifyUserID;} set{_ModifyUserID = value??"";} } // ModifyUserID
		private string _ModifyUserID="";
		/// <summary></summary>	
		[Description("")]
        public string ModifyUser { get{return _ModifyUser;} set{_ModifyUser = value??"";} } // ModifyUser
		private string _ModifyUser="";
		/// <summary></summary>	
		[Description("")]
        public string OrgID { get{return _OrgID;} set{_OrgID = value??"";} } // OrgID
		private string _OrgID="";
		/// <summary></summary>	
		[Description("")]
        public string CompanyID { get{return _CompanyID;} set{_CompanyID = value??"";} } // CompanyID
		private string _CompanyID="";
		/// <summary></summary>	
		[Description("")]
        public string FlowPhase { get{return _FlowPhase;} set{_FlowPhase = value??"";} } // FlowPhase
		private string _FlowPhase="";
		/// <summary></summary>	
		[Description("")]
        public string FlowInfo { get{return _FlowInfo;} set{_FlowInfo = value??"";} } // FlowInfo
		private string _FlowInfo="";
		/// <summary></summary>	
		[Description("")]
        public string StepName { get{return _StepName;} set{_StepName = value??"";} } // StepName
		private string _StepName="";
		/// <summary>借用人</summary>	
		[Description("借用人")]
        public string Borrower { get{return _Borrower;} set{_Borrower = value??"";} } // Borrower
		private string _Borrower="";
		/// <summary>借用人名称</summary>	
		[Description("借用人名称")]
        public string BorrowerName { get{return _BorrowerName;} set{_BorrowerName = value??"";} } // BorrowerName
		private string _BorrowerName="";
		/// <summary>实际借用日期</summary>	
		[Description("实际借用日期")]
        public DateTime? BorrowDate { get; set; } // BorrowDate
		/// <summary>借用用途</summary>	
		[Description("借用用途")]
        public string Purpose { get{return _Purpose;} set{_Purpose = value??"";} } // Purpose
		private string _Purpose="";
		/// <summary>证书类别</summary>	
		[Description("证书类别")]
        public string CertificateType { get{return _CertificateType;} set{_CertificateType = value??"";} } // CertificateType
		private string _CertificateType="";
		/// <summary>借用部门</summary>	
		[Description("借用部门")]
        public string BorrowerDept { get{return _BorrowerDept;} set{_BorrowerDept = value??"";} } // BorrowerDept
		private string _BorrowerDept="";
		/// <summary>借用部门名称</summary>	
		[Description("借用部门名称")]
        public string BorrowerDeptName { get{return _BorrowerDeptName;} set{_BorrowerDeptName = value??"";} } // BorrowerDeptName
		private string _BorrowerDeptName="";
		/// <summary>部门领导审批</summary>	
		[Description("部门领导审批")]
        public string DeptSign { get{return _DeptSign;} set{_DeptSign = value??"";} } // DeptSign
		private string _DeptSign="";
		/// <summary>院领导审批</summary>	
		[Description("院领导审批")]
        public string LeaderSign { get{return _LeaderSign;} set{_LeaderSign = value??"";} } // LeaderSign
		private string _LeaderSign="";
		/// <summary>借用状态</summary>	
		[Description("借用状态")]
        public string BorrowState { get{return _BorrowState;} set{_BorrowState = value??"";} } // BorrowState
		private string _BorrowState="";
		/// <summary>实际归还日期</summary>	
		[Description("实际归还日期")]
        public DateTime? ReturnDate { get; set; } // ReturnDate
		/// <summary>预计归还日期</summary>	
		[Description("预计归还日期")]
        public DateTime? PlanReturnDate { get; set; } // PlanReturnDate
		/// <summary>借用人联系电话</summary>	
		[Description("借用人联系电话")]
        public string BorrowerTel { get{return _BorrowerTel;} set{_BorrowerTel = value??"";} } // BorrowerTel
		private string _BorrowerTel="";
		/// <summary>资深管理员审批</summary>	
		[Description("资深管理员审批")]
        public string Senior { get{return _Senior;} set{_Senior = value??"";} } // Senior
		private string _Senior="";

        // Reverse navigation
		[JsonIgnore]
        public virtual ICollection<T_C_QualificationApply_BorrowInfo> T_C_QualificationApply_BorrowInfo { get { onT_C_QualificationApply_BorrowInfoGetting(); return _T_C_QualificationApply_BorrowInfo;} }
		private ICollection<T_C_QualificationApply_BorrowInfo> _T_C_QualificationApply_BorrowInfo;
		partial void onT_C_QualificationApply_BorrowInfoGetting();


        public T_C_QualificationApply()
        {
            _T_C_QualificationApply_BorrowInfo = new List<T_C_QualificationApply_BorrowInfo>();
        }
    }

	/// <summary>借用信息</summary>	
	[Description("借用信息")]
    public partial class T_C_QualificationApply_BorrowInfo : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID (Primary key)
		private string _ID="";
		/// <summary></summary>	
		[Description("")]
        public string T_C_QualificationApplyID { get{return _T_C_QualificationApplyID;} set{_T_C_QualificationApplyID = value??"";} } // T_C_QualificationApplyID
		private string _T_C_QualificationApplyID="";
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string IsReleased { get{return _IsReleased;} set{_IsReleased = value??"";} } // IsReleased
		private string _IsReleased="";
		/// <summary>证书名称</summary>	
		[Description("证书名称")]
        public string Name { get{return _Name;} set{_Name = value??"";} } // Name
		private string _Name="";
		/// <summary>证书编号</summary>	
		[Description("证书编号")]
        public string Code { get{return _Code;} set{_Code = value??"";} } // Code
		private string _Code="";
		/// <summary>持证部门</summary>	
		[Description("持证部门")]
        public string HoldDept { get{return _HoldDept;} set{_HoldDept = value??"";} } // HoldDept
		private string _HoldDept="";
		/// <summary>持证部门名称</summary>	
		[Description("持证部门名称")]
        public string HoldDeptName { get{return _HoldDeptName;} set{_HoldDeptName = value??"";} } // HoldDeptName
		private string _HoldDeptName="";
		/// <summary>证书保管人</summary>	
		[Description("证书保管人")]
        public string Keeper { get{return _Keeper;} set{_Keeper = value??"";} } // Keeper
		private string _Keeper="";
		/// <summary>证书保管人名称</summary>	
		[Description("证书保管人名称")]
        public string KeeperName { get{return _KeeperName;} set{_KeeperName = value??"";} } // KeeperName
		private string _KeeperName="";
		/// <summary>证书类型</summary>	
		[Description("证书类型")]
        public string Type { get{return _Type;} set{_Type = value??"";} } // Type
		private string _Type="";
		/// <summary>证书ID</summary>	
		[Description("证书ID")]
        public string BorrowID { get{return _BorrowID;} set{_BorrowID = value??"";} } // BorrowID
		private string _BorrowID="";

        // Foreign keys
		[JsonIgnore]
        public virtual T_C_QualificationApply T_C_QualificationApply { get; set; } //  T_C_QualificationApplyID - FK_T_C_QualificationApply_BorrowInfo_T_C_QualificationApply
    }

	/// <summary>资质破格申请</summary>	
	[Description("资质破格申请")]
    public partial class T_D_UserAptitudeApply : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID (Primary key)
		private string _ID="";
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? ModifyDate { get; set; } // ModifyDate
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get{return _CreateUserID;} set{_CreateUserID = value??"";} } // CreateUserID
		private string _CreateUserID="";
		/// <summary></summary>	
		[Description("")]
        public string CreateUser { get{return _CreateUser;} set{_CreateUser = value??"";} } // CreateUser
		private string _CreateUser="";
		/// <summary></summary>	
		[Description("")]
        public string ModifyUserID { get{return _ModifyUserID;} set{_ModifyUserID = value??"";} } // ModifyUserID
		private string _ModifyUserID="";
		/// <summary></summary>	
		[Description("")]
        public string ModifyUser { get{return _ModifyUser;} set{_ModifyUser = value??"";} } // ModifyUser
		private string _ModifyUser="";
		/// <summary></summary>	
		[Description("")]
        public string OrgID { get{return _OrgID;} set{_OrgID = value??"";} } // OrgID
		private string _OrgID="";
		/// <summary></summary>	
		[Description("")]
        public string CompanyID { get{return _CompanyID;} set{_CompanyID = value??"";} } // CompanyID
		private string _CompanyID="";
		/// <summary></summary>	
		[Description("")]
        public string FlowPhase { get{return _FlowPhase;} set{_FlowPhase = value??"";} } // FlowPhase
		private string _FlowPhase="";
		/// <summary></summary>	
		[Description("")]
        public string FlowInfo { get{return _FlowInfo;} set{_FlowInfo = value??"";} } // FlowInfo
		private string _FlowInfo="";
		/// <summary></summary>	
		[Description("")]
        public string StepName { get{return _StepName;} set{_StepName = value??"";} } // StepName
		private string _StepName="";
		/// <summary>申请人</summary>	
		[Description("申请人")]
        public string ApplyUser { get{return _ApplyUser;} set{_ApplyUser = value??"";} } // ApplyUser
		private string _ApplyUser="";
		/// <summary>申请人名称</summary>	
		[Description("申请人名称")]
        public string ApplyUserName { get{return _ApplyUserName;} set{_ApplyUserName = value??"";} } // ApplyUserName
		private string _ApplyUserName="";
		/// <summary>所属部门</summary>	
		[Description("所属部门")]
        public string ApplyDept { get{return _ApplyDept;} set{_ApplyDept = value??"";} } // ApplyDept
		private string _ApplyDept="";
		/// <summary>所属部门名称</summary>	
		[Description("所属部门名称")]
        public string ApplyDeptName { get{return _ApplyDeptName;} set{_ApplyDeptName = value??"";} } // ApplyDeptName
		private string _ApplyDeptName="";
		/// <summary>申请日期</summary>	
		[Description("申请日期")]
        public DateTime? ApplyDate { get; set; } // ApplyDate
		/// <summary>破格原由</summary>	
		[Description("破格原由")]
        public string ApplyReason { get{return _ApplyReason;} set{_ApplyReason = value??"";} } // ApplyReason
		private string _ApplyReason="";
		/// <summary>院长意见</summary>	
		[Description("院长意见")]
        public string LeaderSign { get{return _LeaderSign;} set{_LeaderSign = value??"";} } // LeaderSign
		private string _LeaderSign="";
		/// <summary>申请项目</summary>	
		[Description("申请项目")]
        public string ProjectID { get{return _ProjectID;} set{_ProjectID = value??"";} } // ProjectID
		private string _ProjectID="";
		/// <summary>申请项目</summary>	
		[Description("申请项目")]
        public string ProjectIDName { get{return _ProjectIDName;} set{_ProjectIDName = value??"";} } // ProjectIDName
		private string _ProjectIDName="";
		/// <summary>申请信息</summary>	
		[Description("申请信息")]
        public string ApplyInfo { get{return _ApplyInfo;} set{_ApplyInfo = value??"";} } // ApplyInfo
		private string _ApplyInfo="";

        // Reverse navigation
		[JsonIgnore]
        public virtual ICollection<T_D_UserAptitudeApply_ApplyInfo> T_D_UserAptitudeApply_ApplyInfo { get { onT_D_UserAptitudeApply_ApplyInfoGetting(); return _T_D_UserAptitudeApply_ApplyInfo;} }
		private ICollection<T_D_UserAptitudeApply_ApplyInfo> _T_D_UserAptitudeApply_ApplyInfo;
		partial void onT_D_UserAptitudeApply_ApplyInfoGetting();


        public T_D_UserAptitudeApply()
        {
            _T_D_UserAptitudeApply_ApplyInfo = new List<T_D_UserAptitudeApply_ApplyInfo>();
        }
    }

	/// <summary>申请信息</summary>	
	[Description("申请信息")]
    public partial class T_D_UserAptitudeApply_ApplyInfo : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID (Primary key)
		private string _ID="";
		/// <summary></summary>	
		[Description("")]
        public string T_D_UserAptitudeApplyID { get{return _T_D_UserAptitudeApplyID;} set{_T_D_UserAptitudeApplyID = value??"";} } // T_D_UserAptitudeApplyID
		private string _T_D_UserAptitudeApplyID="";
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string IsReleased { get{return _IsReleased;} set{_IsReleased = value??"";} } // IsReleased
		private string _IsReleased="";
		/// <summary>破格人员</summary>	
		[Description("破格人员")]
        public string UserID { get{return _UserID;} set{_UserID = value??"";} } // UserID
		private string _UserID="";
		/// <summary>破格人员名称</summary>	
		[Description("破格人员名称")]
        public string UserIDName { get{return _UserIDName;} set{_UserIDName = value??"";} } // UserIDName
		private string _UserIDName="";
		/// <summary>破格专业</summary>	
		[Description("破格专业")]
        public string Major { get{return _Major;} set{_Major = value??"";} } // Major
		private string _Major="";
		/// <summary>业务类型</summary>	
		[Description("业务类型")]
        public string ProjectClass { get{return _ProjectClass;} set{_ProjectClass = value??"";} } // ProjectClass
		private string _ProjectClass="";
		/// <summary>设计资质</summary>	
		[Description("设计资质")]
        public string Position { get{return _Position;} set{_Position = value??"";} } // Position
		private string _Position="";
		/// <summary>项目规模</summary>	
		[Description("项目规模")]
        public string AptitudeLevel { get{return _AptitudeLevel;} set{_AptitudeLevel = value??"";} } // AptitudeLevel
		private string _AptitudeLevel="";

        // Foreign keys
		[JsonIgnore]
        public virtual T_D_UserAptitudeApply T_D_UserAptitudeApply { get; set; } //  T_D_UserAptitudeApplyID - FK_T_D_UserAptitudeApply_ApplyInfo_T_D_UserAptitudeApply
    }

	/// <summary>基本信息</summary>	
	[Description("基本信息")]
    public partial class T_Employee : Formula.BaseModel
    {
		/// <summary>主键id</summary>	
		[Description("主键id")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID (Primary key)
		private string _ID="";
		/// <summary>员工工号</summary>	
		[Description("员工工号")]
        public string Code { get{return _Code;} set{_Code = value??"";} } // Code
		private string _Code="";
		/// <summary>曾用名</summary>	
		[Description("曾用名")]
        public string OldName { get{return _OldName;} set{_OldName = value??"";} } // OldName
		private string _OldName="";
		/// <summary>员工头像</summary>	
		[Description("员工头像")]
        public byte[] Portrait { get; set; } // Portrait
		/// <summary>员工姓名</summary>	
		[Description("员工姓名")]
        public string Name { get{return _Name;} set{_Name = value??"";} } // Name
		private string _Name="";
		/// <summary>民族</summary>	
		[Description("民族")]
        public string Nation { get{return _Nation;} set{_Nation = value??"";} } // Nation
		private string _Nation="";
		/// <summary>性别</summary>	
		[Description("性别")]
        public string Sex { get{return _Sex;} set{_Sex = value??"";} } // Sex
		private string _Sex="";
		/// <summary>移动电话</summary>	
		[Description("移动电话")]
        public string MobilePhone { get{return _MobilePhone;} set{_MobilePhone = value??"";} } // MobilePhone
		private string _MobilePhone="";
		/// <summary>办公电话</summary>	
		[Description("办公电话")]
        public string OfficePhone { get{return _OfficePhone;} set{_OfficePhone = value??"";} } // OfficePhone
		private string _OfficePhone="";
		/// <summary>家庭电话</summary>	
		[Description("家庭电话")]
        public string HomePhone { get{return _HomePhone;} set{_HomePhone = value??"";} } // HomePhone
		private string _HomePhone="";
		/// <summary>Email</summary>	
		[Description("Email")]
        public string Email { get{return _Email;} set{_Email = value??"";} } // Email
		private string _Email="";
		/// <summary>联系地址</summary>	
		[Description("联系地址")]
        public string Address { get{return _Address;} set{_Address = value??"";} } // Address
		private string _Address="";
		/// <summary>政治面貌</summary>	
		[Description("政治面貌")]
        public string Political { get{return _Political;} set{_Political = value??"";} } // Political
		private string _Political="";
		/// <summary>健康程度</summary>	
		[Description("健康程度")]
        public string HealthStatus { get{return _HealthStatus;} set{_HealthStatus = value??"";} } // HealthStatus
		private string _HealthStatus="";
		/// <summary>出生日期</summary>	
		[Description("出生日期")]
        public DateTime? Birthday { get; set; } // Birthday
		/// <summary>身份证</summary>	
		[Description("身份证")]
        public string IdentityCardCode { get{return _IdentityCardCode;} set{_IdentityCardCode = value??"";} } // IdentityCardCode
		private string _IdentityCardCode="";
		/// <summary>籍贯</summary>	
		[Description("籍贯")]
        public string NativePlace { get{return _NativePlace;} set{_NativePlace = value??"";} } // NativePlace
		private string _NativePlace="";
		/// <summary>婚姻状况</summary>	
		[Description("婚姻状况")]
        public string MaritalStatus { get{return _MaritalStatus;} set{_MaritalStatus = value??"";} } // MaritalStatus
		private string _MaritalStatus="";
		/// <summary>爱人姓名</summary>	
		[Description("爱人姓名")]
        public string LoverName { get{return _LoverName;} set{_LoverName = value??"";} } // LoverName
		private string _LoverName="";
		/// <summary>爱人单位</summary>	
		[Description("爱人单位")]
        public string LoverUnit { get{return _LoverUnit;} set{_LoverUnit = value??"";} } // LoverUnit
		private string _LoverUnit="";
		/// <summary>第一外语</summary>	
		[Description("第一外语")]
        public string FirstForeignLanguage { get{return _FirstForeignLanguage;} set{_FirstForeignLanguage = value??"";} } // FirstForeignLanguage
		private string _FirstForeignLanguage="";
		/// <summary>第二外语</summary>	
		[Description("第二外语")]
        public string TwoForeignLanguage { get{return _TwoForeignLanguage;} set{_TwoForeignLanguage = value??"";} } // TwoForeignLanguage
		private string _TwoForeignLanguage="";
		/// <summary>第一外语程度</summary>	
		[Description("第一外语程度")]
        public string FirstForeignLanguageLevel { get{return _FirstForeignLanguageLevel;} set{_FirstForeignLanguageLevel = value??"";} } // FirstForeignLanguageLevel
		private string _FirstForeignLanguageLevel="";
		/// <summary>第二外语程度</summary>	
		[Description("第二外语程度")]
        public string TwoForeignLanguageLevel { get{return _TwoForeignLanguageLevel;} set{_TwoForeignLanguageLevel = value??"";} } // TwoForeignLanguageLevel
		private string _TwoForeignLanguageLevel="";
		/// <summary>参加工作日期</summary>	
		[Description("参加工作日期")]
        public DateTime? JoinWorkDate { get; set; } // JoinWorkDate
		/// <summary>入司时间</summary>	
		[Description("入司时间")]
        public DateTime? JoinCompanyDate { get; set; } // JoinCompanyDate
		/// <summary>用工形式</summary>	
		[Description("用工形式")]
        public string EmploymentWay { get{return _EmploymentWay;} set{_EmploymentWay = value??"";} } // EmploymentWay
		private string _EmploymentWay="";
		/// <summary>员工来源</summary>	
		[Description("员工来源")]
        public string EmployeeSource { get{return _EmployeeSource;} set{_EmployeeSource = value??"";} } // EmployeeSource
		private string _EmployeeSource="";
		/// <summary>人员类别（大类）</summary>	
		[Description("人员类别（大类）")]
        public string EmployeeBigType { get{return _EmployeeBigType;} set{_EmployeeBigType = value??"";} } // EmployeeBigType
		private string _EmployeeBigType="";
		/// <summary>人员类别（小类）</summary>	
		[Description("人员类别（小类）")]
        public string EmployeeSmallType { get{return _EmployeeSmallType;} set{_EmployeeSmallType = value??"";} } // EmployeeSmallType
		private string _EmployeeSmallType="";
		/// <summary>岗位</summary>	
		[Description("岗位")]
        public string Post { get{return _Post;} set{_Post = value??"";} } // Post
		private string _Post="";
		/// <summary>岗级</summary>	
		[Description("岗级")]
        public string PostLevel { get{return _PostLevel;} set{_PostLevel = value??"";} } // PostLevel
		private string _PostLevel="";
		/// <summary>职称</summary>	
		[Description("职称")]
        public string PositionalTitles { get{return _PositionalTitles;} set{_PositionalTitles = value??"";} } // PositionalTitles
		private string _PositionalTitles="";
		/// <summary>学历</summary>	
		[Description("学历")]
        public string Educational { get{return _Educational;} set{_Educational = value??"";} } // Educational
		private string _Educational="";
		/// <summary>从事专业</summary>	
		[Description("从事专业")]
        public string EngageMajor { get{return _EngageMajor;} set{_EngageMajor = value??"";} } // EngageMajor
		private string _EngageMajor="";
		/// <summary>学历专业</summary>	
		[Description("学历专业")]
        public string EducationalMajor { get{return _EducationalMajor;} set{_EducationalMajor = value??"";} } // EducationalMajor
		private string _EducationalMajor="";
		/// <summary>合同类型</summary>	
		[Description("合同类型")]
        public string ContractType { get{return _ContractType;} set{_ContractType = value??"";} } // ContractType
		private string _ContractType="";
		/// <summary>定岗时间</summary>	
		[Description("定岗时间")]
        public DateTime? DeterminePostsDate { get; set; } // DeterminePostsDate
		/// <summary>是否发放工资</summary>	
		[Description("是否发放工资")]
        public string IsPaymentInterval { get{return _IsPaymentInterval;} set{_IsPaymentInterval = value??"";} } // IsPaymentInterval
		private string _IsPaymentInterval="";
		/// <summary>是否缴纳社保</summary>	
		[Description("是否缴纳社保")]
        public string IsPaymentSheBao { get{return _IsPaymentSheBao;} set{_IsPaymentSheBao = value??"";} } // IsPaymentSheBao
		private string _IsPaymentSheBao="";
		/// <summary>是否缴纳公积金</summary>	
		[Description("是否缴纳公积金")]
        public string IsPaymentGongJiJin { get{return _IsPaymentGongJiJin;} set{_IsPaymentGongJiJin = value??"";} } // IsPaymentGongJiJin
		private string _IsPaymentGongJiJin="";
		/// <summary>是否缴纳综合保险</summary>	
		[Description("是否缴纳综合保险")]
        public string IsPaymentZongHeBaoXian { get{return _IsPaymentZongHeBaoXian;} set{_IsPaymentZongHeBaoXian = value??"";} } // IsPaymentZongHeBaoXian
		private string _IsPaymentZongHeBaoXian="";
		/// <summary>身份证(正面)</summary>	
		[Description("身份证(正面)")]
        public byte[] IdentityCardFace { get; set; } // IdentityCardFace
		/// <summary>身份证(反面)</summary>	
		[Description("身份证(反面)")]
        public byte[] IdentityCardBack { get; set; } // IdentityCardBack
		/// <summary>签名图片</summary>	
		[Description("签名图片")]
        public byte[] SignImage { get; set; } // SignImage
		/// <summary>备注</summary>	
		[Description("备注")]
        public string Remark { get{return _Remark;} set{_Remark = value??"";} } // Remark
		private string _Remark="";
		/// <summary>当前部门</summary>	
		[Description("当前部门")]
        public string DeptID { get{return _DeptID;} set{_DeptID = value??"";} } // DeptID
		private string _DeptID="";
		/// <summary>当前部门Name</summary>	
		[Description("当前部门Name")]
        public string DeptName { get{return _DeptName;} set{_DeptName = value??"";} } // DeptName
		private string _DeptName="";
		/// <summary>兼职部门</summary>	
		[Description("兼职部门")]
        public string ParttimeDeptID { get{return _ParttimeDeptID;} set{_ParttimeDeptID = value??"";} } // ParttimeDeptID
		private string _ParttimeDeptID="";
		/// <summary>兼职部门名称</summary>	
		[Description("兼职部门名称")]
        public string ParttimeDeptName { get{return _ParttimeDeptName;} set{_ParttimeDeptName = value??"";} } // ParttimeDeptName
		private string _ParttimeDeptName="";
		/// <summary>员工状态</summary>	
		[Description("员工状态")]
        public string EmployeeState { get{return _EmployeeState;} set{_EmployeeState = value??"";} } // EmployeeState
		private string _EmployeeState="";
		/// <summary>是否删除(0未删除;1删除)</summary>	
		[Description("是否删除(0未删除;1删除)")]
        public string IsDeleted { get{return _IsDeleted;} set{_IsDeleted = value??"";} } // IsDeleted
		private string _IsDeleted="";
		/// <summary>删除时间</summary>	
		[Description("删除时间")]
        public DateTime? DeleteTime { get; set; } // DeleteTime
		/// <summary>Base库S_A_User表ID，同步后填写</summary>	
		[Description("Base库S_A_User表ID，同步后填写")]
        public string UserID { get{return _UserID;} set{_UserID = value??"";} } // UserID
		private string _UserID="";
		/// <summary>是否开通系统账号</summary>	
		[Description("是否开通系统账号")]
        public string IsHaveAccount { get{return _IsHaveAccount;} set{_IsHaveAccount = value??"";} } // IsHaveAccount
		private string _IsHaveAccount="";
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get{return _CreateUserID;} set{_CreateUserID = value??"";} } // CreateUserID
		private string _CreateUserID="";
		/// <summary></summary>	
		[Description("")]
        public string CreateUser { get{return _CreateUser;} set{_CreateUser = value??"";} } // CreateUser
		private string _CreateUser="";
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public string ModifyUserID { get{return _ModifyUserID;} set{_ModifyUserID = value??"";} } // ModifyUserID
		private string _ModifyUserID="";
		/// <summary></summary>	
		[Description("")]
        public string ModifyUser { get{return _ModifyUser;} set{_ModifyUser = value??"";} } // ModifyUser
		private string _ModifyUser="";
		/// <summary></summary>	
		[Description("")]
        public DateTime? ModifyDate { get; set; } // ModifyDate
		/// <summary></summary>	
		[Description("")]
        public string PostTemplateID { get{return _PostTemplateID;} set{_PostTemplateID = value??"";} } // PostTemplateID
		private string _PostTemplateID="";
		/// <summary></summary>	
		[Description("")]
        public string JobID { get{return _JobID;} set{_JobID = value??"";} } // JobID
		private string _JobID="";
		/// <summary></summary>	
		[Description("")]
        public string JobName { get{return _JobName;} set{_JobName = value??"";} } // JobName
		private string _JobName="";
		/// <summary>当前部门名称</summary>	
		[Description("当前部门名称")]
        public string DeptIDName { get{return _DeptIDName;} set{_DeptIDName = value??"";} } // DeptIDName
		private string _DeptIDName="";
		/// <summary>员工签名</summary>	
		[Description("员工签名")]
        public byte[] Sign { get; set; } // Sign
		/// <summary></summary>	
		[Description("")]
        public decimal? Ext1 { get; set; } // Ext1
		/// <summary>员工签名Dwg</summary>	
		[Description("员工签名Dwg")]
        public byte[] SignDwg { get; set; } // SignDwg
		/// <summary>兼职部门名称</summary>	
		[Description("兼职部门名称")]
        public string ParttimeDeptIDName { get{return _ParttimeDeptIDName;} set{_ParttimeDeptIDName = value??"";} } // ParttimeDeptIDName
		private string _ParttimeDeptIDName="";
    }

	/// <summary>T</summary>	
	[Description("T")]
    public partial class T_EmployeeAcademicDegree : Formula.BaseModel
    {
		/// <summary>ID</summary>	
		[Description("ID")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID (Primary key)
		private string _ID="";
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? ModifyDate { get; set; } // ModifyDate
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get{return _CreateUserID;} set{_CreateUserID = value??"";} } // CreateUserID
		private string _CreateUserID="";
		/// <summary></summary>	
		[Description("")]
        public string CreateUser { get{return _CreateUser;} set{_CreateUser = value??"";} } // CreateUser
		private string _CreateUser="";
		/// <summary></summary>	
		[Description("")]
        public string ModifyUserID { get{return _ModifyUserID;} set{_ModifyUserID = value??"";} } // ModifyUserID
		private string _ModifyUserID="";
		/// <summary></summary>	
		[Description("")]
        public string ModifyUser { get{return _ModifyUser;} set{_ModifyUser = value??"";} } // ModifyUser
		private string _ModifyUser="";
		/// <summary></summary>	
		[Description("")]
        public string OrgID { get{return _OrgID;} set{_OrgID = value??"";} } // OrgID
		private string _OrgID="";
		/// <summary></summary>	
		[Description("")]
        public string PrjID { get{return _PrjID;} set{_PrjID = value??"";} } // PrjID
		private string _PrjID="";
		/// <summary></summary>	
		[Description("")]
        public string FlowPhase { get{return _FlowPhase;} set{_FlowPhase = value??"";} } // FlowPhase
		private string _FlowPhase="";
		/// <summary></summary>	
		[Description("")]
        public string StepName { get{return _StepName;} set{_StepName = value??"";} } // StepName
		private string _StepName="";
		/// <summary>员工表ID</summary>	
		[Description("员工表ID")]
        public string EmployeeID { get{return _EmployeeID;} set{_EmployeeID = value??"";} } // EmployeeID
		private string _EmployeeID="";
		/// <summary>入学时间</summary>	
		[Description("入学时间")]
        public DateTime? EntrancelDate { get; set; } // EntrancelDate
		/// <summary>毕业时间</summary>	
		[Description("毕业时间")]
        public DateTime? GraduationDate { get; set; } // GraduationDate
		/// <summary>毕业学校</summary>	
		[Description("毕业学校")]
        public string School { get{return _School;} set{_School = value??"";} } // School
		private string _School="";
		/// <summary>专业</summary>	
		[Description("专业")]
        public string FirstProfession { get{return _FirstProfession;} set{_FirstProfession = value??"";} } // FirstProfession
		private string _FirstProfession="";
		/// <summary>第二专业</summary>	
		[Description("第二专业")]
        public string TwoProfession { get{return _TwoProfession;} set{_TwoProfession = value??"";} } // TwoProfession
		private string _TwoProfession="";
		/// <summary>学制</summary>	
		[Description("学制")]
        public string SchoolingLength { get{return _SchoolingLength;} set{_SchoolingLength = value??"";} } // SchoolingLength
		private string _SchoolingLength="";
		/// <summary>学习形式</summary>	
		[Description("学习形式")]
        public string SchoolShape { get{return _SchoolShape;} set{_SchoolShape = value??"";} } // SchoolShape
		private string _SchoolShape="";
		/// <summary>学位</summary>	
		[Description("学位")]
        public string Degree { get{return _Degree;} set{_Degree = value??"";} } // Degree
		private string _Degree="";
		/// <summary>学历</summary>	
		[Description("学历")]
        public string Education { get{return _Education;} set{_Education = value??"";} } // Education
		private string _Education="";
		/// <summary>学位授予时间</summary>	
		[Description("学位授予时间")]
        public DateTime? DegreeGiveDate { get; set; } // DegreeGiveDate
		/// <summary>学位授予国家</summary>	
		[Description("学位授予国家")]
        public string DegreeGiveCountry { get{return _DegreeGiveCountry;} set{_DegreeGiveCountry = value??"";} } // DegreeGiveCountry
		private string _DegreeGiveCountry="";
		/// <summary>学历证书</summary>	
		[Description("学历证书")]
        public string DegreeAttachment { get{return _DegreeAttachment;} set{_DegreeAttachment = value??"";} } // DegreeAttachment
		private string _DegreeAttachment="";
		/// <summary>学位证书</summary>	
		[Description("学位证书")]
        public string EducationAttachment { get{return _EducationAttachment;} set{_EducationAttachment = value??"";} } // EducationAttachment
		private string _EducationAttachment="";
    }

	/// <summary>T</summary>	
	[Description("T")]
    public partial class T_EmployeeAcademicTitle : Formula.BaseModel
    {
		/// <summary>ID</summary>	
		[Description("ID")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID (Primary key)
		private string _ID="";
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? ModifyDate { get; set; } // ModifyDate
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get{return _CreateUserID;} set{_CreateUserID = value??"";} } // CreateUserID
		private string _CreateUserID="";
		/// <summary></summary>	
		[Description("")]
        public string CreateUser { get{return _CreateUser;} set{_CreateUser = value??"";} } // CreateUser
		private string _CreateUser="";
		/// <summary></summary>	
		[Description("")]
        public string ModifyUserID { get{return _ModifyUserID;} set{_ModifyUserID = value??"";} } // ModifyUserID
		private string _ModifyUserID="";
		/// <summary></summary>	
		[Description("")]
        public string ModifyUser { get{return _ModifyUser;} set{_ModifyUser = value??"";} } // ModifyUser
		private string _ModifyUser="";
		/// <summary></summary>	
		[Description("")]
        public string OrgID { get{return _OrgID;} set{_OrgID = value??"";} } // OrgID
		private string _OrgID="";
		/// <summary></summary>	
		[Description("")]
        public string PrjID { get{return _PrjID;} set{_PrjID = value??"";} } // PrjID
		private string _PrjID="";
		/// <summary></summary>	
		[Description("")]
        public string FlowPhase { get{return _FlowPhase;} set{_FlowPhase = value??"";} } // FlowPhase
		private string _FlowPhase="";
		/// <summary></summary>	
		[Description("")]
        public string StepName { get{return _StepName;} set{_StepName = value??"";} } // StepName
		private string _StepName="";
		/// <summary>职称级别</summary>	
		[Description("职称级别")]
        public string Level { get{return _Level;} set{_Level = value??"";} } // Level
		private string _Level="";
		/// <summary>职称</summary>	
		[Description("职称")]
        public string Title { get{return _Title;} set{_Title = value??"";} } // Title
		private string _Title="";
		/// <summary>专业</summary>	
		[Description("专业")]
        public string Major { get{return _Major;} set{_Major = value??"";} } // Major
		private string _Major="";
		/// <summary>审批部门</summary>	
		[Description("审批部门")]
        public string AuditDept { get{return _AuditDept;} set{_AuditDept = value??"";} } // AuditDept
		private string _AuditDept="";
		/// <summary>证书编号</summary>	
		[Description("证书编号")]
        public string CertificateNumber { get{return _CertificateNumber;} set{_CertificateNumber = value??"";} } // CertificateNumber
		private string _CertificateNumber="";
		/// <summary>发证日期</summary>	
		[Description("发证日期")]
        public DateTime? IssueDate { get; set; } // IssueDate
		/// <summary>聘用时间</summary>	
		[Description("聘用时间")]
        public DateTime? EmployDate { get; set; } // EmployDate
		/// <summary>证书</summary>	
		[Description("证书")]
        public string Certificate { get{return _Certificate;} set{_Certificate = value??"";} } // Certificate
		private string _Certificate="";
		/// <summary>备注</summary>	
		[Description("备注")]
        public string Remark { get{return _Remark;} set{_Remark = value??"";} } // Remark
		private string _Remark="";
		/// <summary>员工id</summary>	
		[Description("员工id")]
        public string EmployeeID { get{return _EmployeeID;} set{_EmployeeID = value??"";} } // EmployeeID
		private string _EmployeeID="";
    }

	/// <summary>人员基本信息子集配置</summary>	
	[Description("人员基本信息子集配置")]
    public partial class T_EmployeeBaseSet : Formula.BaseModel
    {
		/// <summary>ID</summary>	
		[Description("ID")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID (Primary key)
		private string _ID="";
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? ModifyDate { get; set; } // ModifyDate
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get{return _CreateUserID;} set{_CreateUserID = value??"";} } // CreateUserID
		private string _CreateUserID="";
		/// <summary></summary>	
		[Description("")]
        public string CreateUser { get{return _CreateUser;} set{_CreateUser = value??"";} } // CreateUser
		private string _CreateUser="";
		/// <summary></summary>	
		[Description("")]
        public string ModifyUserID { get{return _ModifyUserID;} set{_ModifyUserID = value??"";} } // ModifyUserID
		private string _ModifyUserID="";
		/// <summary></summary>	
		[Description("")]
        public string ModifyUser { get{return _ModifyUser;} set{_ModifyUser = value??"";} } // ModifyUser
		private string _ModifyUser="";
		/// <summary></summary>	
		[Description("")]
        public string OrgID { get{return _OrgID;} set{_OrgID = value??"";} } // OrgID
		private string _OrgID="";
		/// <summary></summary>	
		[Description("")]
        public string PrjID { get{return _PrjID;} set{_PrjID = value??"";} } // PrjID
		private string _PrjID="";
		/// <summary></summary>	
		[Description("")]
        public string FlowPhase { get{return _FlowPhase;} set{_FlowPhase = value??"";} } // FlowPhase
		private string _FlowPhase="";
		/// <summary></summary>	
		[Description("")]
        public string StepName { get{return _StepName;} set{_StepName = value??"";} } // StepName
		private string _StepName="";
		/// <summary>名称</summary>	
		[Description("名称")]
        public string Title { get{return _Title;} set{_Title = value??"";} } // Title
		private string _Title="";
		/// <summary>页面URl</summary>	
		[Description("页面URl")]
        public string Url { get{return _Url;} set{_Url = value??"";} } // Url
		private string _Url="";
		/// <summary>排序号</summary>	
		[Description("排序号")]
        public int? SortIndex { get; set; } // SortIndex
		/// <summary>标识</summary>	
		[Description("标识")]
        public string Code { get{return _Code;} set{_Code = value??"";} } // Code
		private string _Code="";
		/// <summary>过滤字段</summary>	
		[Description("过滤字段")]
        public string FilterField { get{return _FilterField;} set{_FilterField = value??"";} } // FilterField
		private string _FilterField="";
		/// <summary>用工形式</summary>	
		[Description("用工形式")]
        public string EmploymentWay { get{return _EmploymentWay;} set{_EmploymentWay = value??"";} } // EmploymentWay
		private string _EmploymentWay="";
    }

	/// <summary>T</summary>	
	[Description("T")]
    public partial class T_EmployeeContract : Formula.BaseModel
    {
		/// <summary>ID</summary>	
		[Description("ID")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID (Primary key)
		private string _ID="";
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? ModifyDate { get; set; } // ModifyDate
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get{return _CreateUserID;} set{_CreateUserID = value??"";} } // CreateUserID
		private string _CreateUserID="";
		/// <summary></summary>	
		[Description("")]
        public string CreateUser { get{return _CreateUser;} set{_CreateUser = value??"";} } // CreateUser
		private string _CreateUser="";
		/// <summary></summary>	
		[Description("")]
        public string ModifyUserID { get{return _ModifyUserID;} set{_ModifyUserID = value??"";} } // ModifyUserID
		private string _ModifyUserID="";
		/// <summary></summary>	
		[Description("")]
        public string ModifyUser { get{return _ModifyUser;} set{_ModifyUser = value??"";} } // ModifyUser
		private string _ModifyUser="";
		/// <summary></summary>	
		[Description("")]
        public string OrgID { get{return _OrgID;} set{_OrgID = value??"";} } // OrgID
		private string _OrgID="";
		/// <summary></summary>	
		[Description("")]
        public string PrjID { get{return _PrjID;} set{_PrjID = value??"";} } // PrjID
		private string _PrjID="";
		/// <summary></summary>	
		[Description("")]
        public string FlowPhase { get{return _FlowPhase;} set{_FlowPhase = value??"";} } // FlowPhase
		private string _FlowPhase="";
		/// <summary></summary>	
		[Description("")]
        public string StepName { get{return _StepName;} set{_StepName = value??"";} } // StepName
		private string _StepName="";
		/// <summary>员工id</summary>	
		[Description("员工id")]
        public string EmployeeID { get{return _EmployeeID;} set{_EmployeeID = value??"";} } // EmployeeID
		private string _EmployeeID="";
		/// <summary>合同编号</summary>	
		[Description("合同编号")]
        public string Code { get{return _Code;} set{_Code = value??"";} } // Code
		private string _Code="";
		/// <summary>乙方</summary>	
		[Description("乙方")]
        public string EmployeeName { get{return _EmployeeName;} set{_EmployeeName = value??"";} } // EmployeeName
		private string _EmployeeName="";
		/// <summary>合同类别</summary>	
		[Description("合同类别")]
        public string ContractCategory { get{return _ContractCategory;} set{_ContractCategory = value??"";} } // ContractCategory
		private string _ContractCategory="";
		/// <summary>合同形式</summary>	
		[Description("合同形式")]
        public string ContractShape { get{return _ContractShape;} set{_ContractShape = value??"";} } // ContractShape
		private string _ContractShape="";
		/// <summary>合同主体</summary>	
		[Description("合同主体")]
        public string ContractBody { get{return _ContractBody;} set{_ContractBody = value??"";} } // ContractBody
		private string _ContractBody="";
		/// <summary>合同开始日期</summary>	
		[Description("合同开始日期")]
        public DateTime? ContractStartDate { get; set; } // ContractStartDate
		/// <summary>合同结束日期</summary>	
		[Description("合同结束日期")]
        public DateTime? ContractEndDate { get; set; } // ContractEndDate
		/// <summary>试用期开始日期</summary>	
		[Description("试用期开始日期")]
        public DateTime? PeriodStartDate { get; set; } // PeriodStartDate
		/// <summary>试用结束日期</summary>	
		[Description("试用结束日期")]
        public DateTime? PeriodEndDate { get; set; } // PeriodEndDate
		/// <summary>实习期满时间</summary>	
		[Description("实习期满时间")]
        public DateTime? PracticeEndDate { get; set; } // PracticeEndDate
		/// <summary>合同期限</summary>	
		[Description("合同期限")]
        public string ContractPeriod { get{return _ContractPeriod;} set{_ContractPeriod = value??"";} } // ContractPeriod
		private string _ContractPeriod="";
		/// <summary>定岗时间</summary>	
		[Description("定岗时间")]
        public DateTime? PostDate { get; set; } // PostDate
		/// <summary>公积金缴纳基数</summary>	
		[Description("公积金缴纳基数")]
        public string GongJiJinBasePay { get{return _GongJiJinBasePay;} set{_GongJiJinBasePay = value??"";} } // GongJiJinBasePay
		private string _GongJiJinBasePay="";
		/// <summary>合同及附件</summary>	
		[Description("合同及附件")]
        public string ContractAttachment { get{return _ContractAttachment;} set{_ContractAttachment = value??"";} } // ContractAttachment
		private string _ContractAttachment="";
		/// <summary>是否签订保密协议</summary>	
		[Description("是否签订保密协议")]
        public string IsConfidentialityAgreement { get{return _IsConfidentialityAgreement;} set{_IsConfidentialityAgreement = value??"";} } // IsConfidentialityAgreement
		private string _IsConfidentialityAgreement="";
		/// <summary>保密协议开始时间</summary>	
		[Description("保密协议开始时间")]
        public DateTime? ConfidentialityAgreementStart { get; set; } // ConfidentialityAgreementStart
		/// <summary>保密协议结束时间</summary>	
		[Description("保密协议结束时间")]
        public DateTime? ConfidentialityAgreementEnd { get; set; } // ConfidentialityAgreementEnd
		/// <summary>保密协议附件</summary>	
		[Description("保密协议附件")]
        public string ConfidentialityAttachment { get{return _ConfidentialityAttachment;} set{_ConfidentialityAttachment = value??"";} } // ConfidentialityAttachment
		private string _ConfidentialityAttachment="";
		/// <summary>教育培训协议开始时间</summary>	
		[Description("教育培训协议开始时间")]
        public DateTime? TrainAgreementStartDate { get; set; } // TrainAgreementStartDate
		/// <summary>教育培训结束时间</summary>	
		[Description("教育培训结束时间")]
        public DateTime? TrainAgreementEndDate { get; set; } // TrainAgreementEndDate
		/// <summary>教育培训附件</summary>	
		[Description("教育培训附件")]
        public string TrainAttachment { get{return _TrainAttachment;} set{_TrainAttachment = value??"";} } // TrainAttachment
		private string _TrainAttachment="";
		/// <summary>股权协议开始时间</summary>	
		[Description("股权协议开始时间")]
        public DateTime? StockAgreementStartDate { get; set; } // StockAgreementStartDate
		/// <summary>股权协议结束时间</summary>	
		[Description("股权协议结束时间")]
        public DateTime? StockAgreementEndDate { get; set; } // StockAgreementEndDate
		/// <summary>股权附件</summary>	
		[Description("股权附件")]
        public string StockAttachment { get{return _StockAttachment;} set{_StockAttachment = value??"";} } // StockAttachment
		private string _StockAttachment="";
		/// <summary>备注</summary>	
		[Description("备注")]
        public string Remark { get{return _Remark;} set{_Remark = value??"";} } // Remark
		private string _Remark="";
    }

	/// <summary>T</summary>	
	[Description("T")]
    public partial class T_EmployeeHonour : Formula.BaseModel
    {
		/// <summary>ID</summary>	
		[Description("ID")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID (Primary key)
		private string _ID="";
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? ModifyDate { get; set; } // ModifyDate
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get{return _CreateUserID;} set{_CreateUserID = value??"";} } // CreateUserID
		private string _CreateUserID="";
		/// <summary></summary>	
		[Description("")]
        public string CreateUser { get{return _CreateUser;} set{_CreateUser = value??"";} } // CreateUser
		private string _CreateUser="";
		/// <summary></summary>	
		[Description("")]
        public string ModifyUserID { get{return _ModifyUserID;} set{_ModifyUserID = value??"";} } // ModifyUserID
		private string _ModifyUserID="";
		/// <summary></summary>	
		[Description("")]
        public string ModifyUser { get{return _ModifyUser;} set{_ModifyUser = value??"";} } // ModifyUser
		private string _ModifyUser="";
		/// <summary></summary>	
		[Description("")]
        public string OrgID { get{return _OrgID;} set{_OrgID = value??"";} } // OrgID
		private string _OrgID="";
		/// <summary></summary>	
		[Description("")]
        public string PrjID { get{return _PrjID;} set{_PrjID = value??"";} } // PrjID
		private string _PrjID="";
		/// <summary></summary>	
		[Description("")]
        public string FlowPhase { get{return _FlowPhase;} set{_FlowPhase = value??"";} } // FlowPhase
		private string _FlowPhase="";
		/// <summary></summary>	
		[Description("")]
        public string StepName { get{return _StepName;} set{_StepName = value??"";} } // StepName
		private string _StepName="";
		/// <summary>员工ID</summary>	
		[Description("员工ID")]
        public string EmployeeID { get{return _EmployeeID;} set{_EmployeeID = value??"";} } // EmployeeID
		private string _EmployeeID="";
		/// <summary>获奖名称</summary>	
		[Description("获奖名称")]
        public string AwardName { get{return _AwardName;} set{_AwardName = value??"";} } // AwardName
		private string _AwardName="";
		/// <summary>获奖年份</summary>	
		[Description("获奖年份")]
        public string AwardYear { get{return _AwardYear;} set{_AwardYear = value??"";} } // AwardYear
		private string _AwardYear="";
		/// <summary>发证日期</summary>	
		[Description("发证日期")]
        public DateTime? CertificationDate { get; set; } // CertificationDate
		/// <summary>发证单位</summary>	
		[Description("发证单位")]
        public string CertificationUnit { get{return _CertificationUnit;} set{_CertificationUnit = value??"";} } // CertificationUnit
		private string _CertificationUnit="";
		/// <summary>备注</summary>	
		[Description("备注")]
        public string Remark { get{return _Remark;} set{_Remark = value??"";} } // Remark
		private string _Remark="";
		/// <summary>附件</summary>	
		[Description("附件")]
        public string Attachment { get{return _Attachment;} set{_Attachment = value??"";} } // Attachment
		private string _Attachment="";
    }

	/// <summary>T</summary>	
	[Description("T")]
    public partial class T_EmployeeJob : Formula.BaseModel
    {
		/// <summary>ID</summary>	
		[Description("ID")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID (Primary key)
		private string _ID="";
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? ModifyDate { get; set; } // ModifyDate
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get{return _CreateUserID;} set{_CreateUserID = value??"";} } // CreateUserID
		private string _CreateUserID="";
		/// <summary></summary>	
		[Description("")]
        public string CreateUser { get{return _CreateUser;} set{_CreateUser = value??"";} } // CreateUser
		private string _CreateUser="";
		/// <summary></summary>	
		[Description("")]
        public string ModifyUserID { get{return _ModifyUserID;} set{_ModifyUserID = value??"";} } // ModifyUserID
		private string _ModifyUserID="";
		/// <summary></summary>	
		[Description("")]
        public string ModifyUser { get{return _ModifyUser;} set{_ModifyUser = value??"";} } // ModifyUser
		private string _ModifyUser="";
		/// <summary></summary>	
		[Description("")]
        public string OrgID { get{return _OrgID;} set{_OrgID = value??"";} } // OrgID
		private string _OrgID="";
		/// <summary></summary>	
		[Description("")]
        public string PrjID { get{return _PrjID;} set{_PrjID = value??"";} } // PrjID
		private string _PrjID="";
		/// <summary></summary>	
		[Description("")]
        public string FlowPhase { get{return _FlowPhase;} set{_FlowPhase = value??"";} } // FlowPhase
		private string _FlowPhase="";
		/// <summary></summary>	
		[Description("")]
        public string StepName { get{return _StepName;} set{_StepName = value??"";} } // StepName
		private string _StepName="";
		/// <summary>员工ID</summary>	
		[Description("员工ID")]
        public string EmployeeID { get{return _EmployeeID;} set{_EmployeeID = value??"";} } // EmployeeID
		private string _EmployeeID="";
		/// <summary>是否删除（0未删除，1删除）</summary>	
		[Description("是否删除（0未删除，1删除）")]
        public string IsDeleted { get{return _IsDeleted;} set{_IsDeleted = value??"";} } // IsDeleted
		private string _IsDeleted="";
		/// <summary>人员类别</summary>	
		[Description("人员类别")]
        public string EmployeeCategory { get{return _EmployeeCategory;} set{_EmployeeCategory = value??"";} } // EmployeeCategory
		private string _EmployeeCategory="";
		/// <summary>职务类型</summary>	
		[Description("职务类型")]
        public string JobCategory { get{return _JobCategory;} set{_JobCategory = value??"";} } // JobCategory
		private string _JobCategory="";
		/// <summary>集团</summary>	
		[Description("集团")]
        public string Clique { get{return _Clique;} set{_Clique = value??"";} } // Clique
		private string _Clique="";
		/// <summary>子公司</summary>	
		[Description("子公司")]
        public string SubCompany { get{return _SubCompany;} set{_SubCompany = value??"";} } // SubCompany
		private string _SubCompany="";
		/// <summary>部门</summary>	
		[Description("部门")]
        public string DeptID { get{return _DeptID;} set{_DeptID = value??"";} } // DeptID
		private string _DeptID="";
		/// <summary>部门名称</summary>	
		[Description("部门名称")]
        public string DeptIDName { get{return _DeptIDName;} set{_DeptIDName = value??"";} } // DeptIDName
		private string _DeptIDName="";
		/// <summary>职务</summary>	
		[Description("职务")]
        public string JobID { get{return _JobID;} set{_JobID = value??"";} } // JobID
		private string _JobID="";
		/// <summary>是否主责</summary>	
		[Description("是否主责")]
        public string IsMain { get{return _IsMain;} set{_IsMain = value??"";} } // IsMain
		private string _IsMain="";
		/// <summary>专业</summary>	
		[Description("专业")]
        public string Major { get{return _Major;} set{_Major = value??"";} } // Major
		private string _Major="";
		/// <summary>聘任时间</summary>	
		[Description("聘任时间")]
        public DateTime? EmployDate { get; set; } // EmployDate
		/// <summary>职批准文号</summary>	
		[Description("职批准文号")]
        public string JobAgreeCode { get{return _JobAgreeCode;} set{_JobAgreeCode = value??"";} } // JobAgreeCode
		private string _JobAgreeCode="";
		/// <summary>聘任批准文号</summary>	
		[Description("聘任批准文号")]
        public string EmployAgreeCode { get{return _EmployAgreeCode;} set{_EmployAgreeCode = value??"";} } // EmployAgreeCode
		private string _EmployAgreeCode="";
		/// <summary>任满时间</summary>	
		[Description("任满时间")]
        public DateTime? EmployEndDate { get; set; } // EmployEndDate
		/// <summary>免职日期</summary>	
		[Description("免职日期")]
        public DateTime? ClearEmployDate { get; set; } // ClearEmployDate
		/// <summary>附件</summary>	
		[Description("附件")]
        public string Attachment { get{return _Attachment;} set{_Attachment = value??"";} } // Attachment
		private string _Attachment="";
		/// <summary>备注</summary>	
		[Description("备注")]
        public string Remark { get{return _Remark;} set{_Remark = value??"";} } // Remark
		private string _Remark="";
		/// <summary>职务名称</summary>	
		[Description("职务名称")]
        public string JobName { get{return _JobName;} set{_JobName = value??"";} } // JobName
		private string _JobName="";
    }

	/// <summary>人员调动</summary>	
	[Description("人员调动")]
    public partial class T_EmployeeJobChange : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID (Primary key)
		private string _ID="";
		/// <summary>员工ID</summary>	
		[Description("员工ID")]
        public string EmployeeID { get{return _EmployeeID;} set{_EmployeeID = value??"";} } // EmployeeID
		private string _EmployeeID="";
		/// <summary>员工编号</summary>	
		[Description("员工编号")]
        public string EmployeeCode { get{return _EmployeeCode;} set{_EmployeeCode = value??"";} } // EmployeeCode
		private string _EmployeeCode="";
		/// <summary>员工姓名</summary>	
		[Description("员工姓名")]
        public string EmployeeName { get{return _EmployeeName;} set{_EmployeeName = value??"";} } // EmployeeName
		private string _EmployeeName="";
		/// <summary>变动时间</summary>	
		[Description("变动时间")]
        public DateTime? ChangeDate { get; set; } // ChangeDate
		/// <summary>变动原因</summary>	
		[Description("变动原因")]
        public string ChangeReason { get{return _ChangeReason;} set{_ChangeReason = value??"";} } // ChangeReason
		private string _ChangeReason="";
		/// <summary>备注</summary>	
		[Description("备注")]
        public string Remark { get{return _Remark;} set{_Remark = value??"";} } // Remark
		private string _Remark="";
		/// <summary>在岗信息</summary>	
		[Description("在岗信息")]
        public string CurrentJobList { get{return _CurrentJobList;} set{_CurrentJobList = value??"";} } // CurrentJobList
		private string _CurrentJobList="";
		/// <summary>变动信息</summary>	
		[Description("变动信息")]
        public string ChangeJobList { get{return _ChangeJobList;} set{_ChangeJobList = value??"";} } // ChangeJobList
		private string _ChangeJobList="";
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get{return _CreateUserID;} set{_CreateUserID = value??"";} } // CreateUserID
		private string _CreateUserID="";
		/// <summary></summary>	
		[Description("")]
        public string CreateUser { get{return _CreateUser;} set{_CreateUser = value??"";} } // CreateUser
		private string _CreateUser="";
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public string ModifyUserID { get{return _ModifyUserID;} set{_ModifyUserID = value??"";} } // ModifyUserID
		private string _ModifyUserID="";
		/// <summary></summary>	
		[Description("")]
        public string ModifyUser { get{return _ModifyUser;} set{_ModifyUser = value??"";} } // ModifyUser
		private string _ModifyUser="";
		/// <summary></summary>	
		[Description("")]
        public DateTime? ModifyDate { get; set; } // ModifyDate

        // Reverse navigation
		[JsonIgnore]
        public virtual ICollection<T_EmployeeJobChange_ChangeJobList> T_EmployeeJobChange_ChangeJobList { get { onT_EmployeeJobChange_ChangeJobListGetting(); return _T_EmployeeJobChange_ChangeJobList;} }
		private ICollection<T_EmployeeJobChange_ChangeJobList> _T_EmployeeJobChange_ChangeJobList;
		partial void onT_EmployeeJobChange_ChangeJobListGetting();

		[JsonIgnore]
        public virtual ICollection<T_EmployeeJobChange_CurrentJobList> T_EmployeeJobChange_CurrentJobList { get { onT_EmployeeJobChange_CurrentJobListGetting(); return _T_EmployeeJobChange_CurrentJobList;} }
		private ICollection<T_EmployeeJobChange_CurrentJobList> _T_EmployeeJobChange_CurrentJobList;
		partial void onT_EmployeeJobChange_CurrentJobListGetting();


        public T_EmployeeJobChange()
        {
            _T_EmployeeJobChange_ChangeJobList = new List<T_EmployeeJobChange_ChangeJobList>();
            _T_EmployeeJobChange_CurrentJobList = new List<T_EmployeeJobChange_CurrentJobList>();
        }
    }

	/// <summary>变动信息</summary>	
	[Description("变动信息")]
    public partial class T_EmployeeJobChange_ChangeJobList : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID (Primary key)
		private string _ID="";
		/// <summary></summary>	
		[Description("")]
        public string T_EmployeeJobChangeID { get{return _T_EmployeeJobChangeID;} set{_T_EmployeeJobChangeID = value??"";} } // T_EmployeeJobChangeID
		private string _T_EmployeeJobChangeID="";
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string IsReleased { get{return _IsReleased;} set{_IsReleased = value??"";} } // IsReleased
		private string _IsReleased="";
		/// <summary>人员类别</summary>	
		[Description("人员类别")]
        public string EmployeeCategory { get{return _EmployeeCategory;} set{_EmployeeCategory = value??"";} } // EmployeeCategory
		private string _EmployeeCategory="";
		/// <summary>职务类别</summary>	
		[Description("职务类别")]
        public string JobCategory { get{return _JobCategory;} set{_JobCategory = value??"";} } // JobCategory
		private string _JobCategory="";
		/// <summary>所属集团</summary>	
		[Description("所属集团")]
        public string Clique { get{return _Clique;} set{_Clique = value??"";} } // Clique
		private string _Clique="";
		/// <summary>所属子公司</summary>	
		[Description("所属子公司")]
        public string SubCompany { get{return _SubCompany;} set{_SubCompany = value??"";} } // SubCompany
		private string _SubCompany="";
		/// <summary>所属部门</summary>	
		[Description("所属部门")]
        public string DeptID { get{return _DeptID;} set{_DeptID = value??"";} } // DeptID
		private string _DeptID="";
		/// <summary>所属部门名称</summary>	
		[Description("所属部门名称")]
        public string DeptIDName { get{return _DeptIDName;} set{_DeptIDName = value??"";} } // DeptIDName
		private string _DeptIDName="";
		/// <summary>是否主责</summary>	
		[Description("是否主责")]
        public string IsMain { get{return _IsMain;} set{_IsMain = value??"";} } // IsMain
		private string _IsMain="";
		/// <summary>职务名称</summary>	
		[Description("职务名称")]
        public string JobName { get{return _JobName;} set{_JobName = value??"";} } // JobName
		private string _JobName="";
		/// <summary>专业</summary>	
		[Description("专业")]
        public string Major { get{return _Major;} set{_Major = value??"";} } // Major
		private string _Major="";
		/// <summary>聘任日期</summary>	
		[Description("聘任日期")]
        public DateTime? EmployDate { get; set; } // EmployDate
		/// <summary>职批准文号</summary>	
		[Description("职批准文号")]
        public string JobAgreeCode { get{return _JobAgreeCode;} set{_JobAgreeCode = value??"";} } // JobAgreeCode
		private string _JobAgreeCode="";
		/// <summary>聘任批准文号</summary>	
		[Description("聘任批准文号")]
        public string EmployAgreeCode { get{return _EmployAgreeCode;} set{_EmployAgreeCode = value??"";} } // EmployAgreeCode
		private string _EmployAgreeCode="";
		/// <summary>职务</summary>	
		[Description("职务")]
        public string JobID { get{return _JobID;} set{_JobID = value??"";} } // JobID
		private string _JobID="";
		/// <summary>IsDeleted</summary>	
		[Description("IsDeleted")]
        public string IsDeleted { get{return _IsDeleted;} set{_IsDeleted = value??"";} } // IsDeleted
		private string _IsDeleted="";

        // Foreign keys
		[JsonIgnore]
        public virtual T_EmployeeJobChange T_EmployeeJobChange { get; set; } //  T_EmployeeJobChangeID - FK_T_EmployeeJobChange_ChangeJobList_T_EmployeeJobChange
    }

	/// <summary>在岗信息</summary>	
	[Description("在岗信息")]
    public partial class T_EmployeeJobChange_CurrentJobList : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID (Primary key)
		private string _ID="";
		/// <summary></summary>	
		[Description("")]
        public string T_EmployeeJobChangeID { get{return _T_EmployeeJobChangeID;} set{_T_EmployeeJobChangeID = value??"";} } // T_EmployeeJobChangeID
		private string _T_EmployeeJobChangeID="";
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string IsReleased { get{return _IsReleased;} set{_IsReleased = value??"";} } // IsReleased
		private string _IsReleased="";
		/// <summary>人员类别</summary>	
		[Description("人员类别")]
        public string EmployeeCategory { get{return _EmployeeCategory;} set{_EmployeeCategory = value??"";} } // EmployeeCategory
		private string _EmployeeCategory="";
		/// <summary>职务类别</summary>	
		[Description("职务类别")]
        public string JobCategory { get{return _JobCategory;} set{_JobCategory = value??"";} } // JobCategory
		private string _JobCategory="";
		/// <summary>所属集团</summary>	
		[Description("所属集团")]
        public string Clique { get{return _Clique;} set{_Clique = value??"";} } // Clique
		private string _Clique="";
		/// <summary>所属子公司</summary>	
		[Description("所属子公司")]
        public string SubCompany { get{return _SubCompany;} set{_SubCompany = value??"";} } // SubCompany
		private string _SubCompany="";
		/// <summary>所属部门</summary>	
		[Description("所属部门")]
        public string DeptID { get{return _DeptID;} set{_DeptID = value??"";} } // DeptID
		private string _DeptID="";
		/// <summary>所属部门名称</summary>	
		[Description("所属部门名称")]
        public string DeptIDName { get{return _DeptIDName;} set{_DeptIDName = value??"";} } // DeptIDName
		private string _DeptIDName="";
		/// <summary>是否主责</summary>	
		[Description("是否主责")]
        public string IsMain { get{return _IsMain;} set{_IsMain = value??"";} } // IsMain
		private string _IsMain="";
		/// <summary>职务</summary>	
		[Description("职务")]
        public string JobName { get{return _JobName;} set{_JobName = value??"";} } // JobName
		private string _JobName="";
		/// <summary>专业</summary>	
		[Description("专业")]
        public string Major { get{return _Major;} set{_Major = value??"";} } // Major
		private string _Major="";
		/// <summary>聘任日期</summary>	
		[Description("聘任日期")]
        public DateTime? EmployDate { get; set; } // EmployDate
		/// <summary>职批准文号</summary>	
		[Description("职批准文号")]
        public string JobAgreeCode { get{return _JobAgreeCode;} set{_JobAgreeCode = value??"";} } // JobAgreeCode
		private string _JobAgreeCode="";
		/// <summary>聘任批准文号</summary>	
		[Description("聘任批准文号")]
        public string EmployAgreeCode { get{return _EmployAgreeCode;} set{_EmployAgreeCode = value??"";} } // EmployAgreeCode
		private string _EmployAgreeCode="";
		/// <summary>删除</summary>	
		[Description("删除")]
        public string NeedDelete { get{return _NeedDelete;} set{_NeedDelete = value??"";} } // NeedDelete
		private string _NeedDelete="";

        // Foreign keys
		[JsonIgnore]
        public virtual T_EmployeeJobChange T_EmployeeJobChange { get; set; } //  T_EmployeeJobChangeID - FK_T_EmployeeJobChange_CurrentJobList_T_EmployeeJobChange
    }

	/// <summary>T</summary>	
	[Description("T")]
    public partial class T_EmployeePersonalRecords : Formula.BaseModel
    {
		/// <summary>ID</summary>	
		[Description("ID")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID (Primary key)
		private string _ID="";
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? ModifyDate { get; set; } // ModifyDate
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get{return _CreateUserID;} set{_CreateUserID = value??"";} } // CreateUserID
		private string _CreateUserID="";
		/// <summary></summary>	
		[Description("")]
        public string CreateUser { get{return _CreateUser;} set{_CreateUser = value??"";} } // CreateUser
		private string _CreateUser="";
		/// <summary></summary>	
		[Description("")]
        public string ModifyUserID { get{return _ModifyUserID;} set{_ModifyUserID = value??"";} } // ModifyUserID
		private string _ModifyUserID="";
		/// <summary></summary>	
		[Description("")]
        public string ModifyUser { get{return _ModifyUser;} set{_ModifyUser = value??"";} } // ModifyUser
		private string _ModifyUser="";
		/// <summary></summary>	
		[Description("")]
        public string OrgID { get{return _OrgID;} set{_OrgID = value??"";} } // OrgID
		private string _OrgID="";
		/// <summary></summary>	
		[Description("")]
        public string PrjID { get{return _PrjID;} set{_PrjID = value??"";} } // PrjID
		private string _PrjID="";
		/// <summary></summary>	
		[Description("")]
        public string FlowPhase { get{return _FlowPhase;} set{_FlowPhase = value??"";} } // FlowPhase
		private string _FlowPhase="";
		/// <summary></summary>	
		[Description("")]
        public string StepName { get{return _StepName;} set{_StepName = value??"";} } // StepName
		private string _StepName="";
		/// <summary>员工ID</summary>	
		[Description("员工ID")]
        public string EmployeeID { get{return _EmployeeID;} set{_EmployeeID = value??"";} } // EmployeeID
		private string _EmployeeID="";
		/// <summary>档案编号</summary>	
		[Description("档案编号")]
        public string Code { get{return _Code;} set{_Code = value??"";} } // Code
		private string _Code="";
		/// <summary>档案类型</summary>	
		[Description("档案类型")]
        public string Type { get{return _Type;} set{_Type = value??"";} } // Type
		private string _Type="";
		/// <summary>档案保存单位</summary>	
		[Description("档案保存单位")]
        public string KeepUnit { get{return _KeepUnit;} set{_KeepUnit = value??"";} } // KeepUnit
		private string _KeepUnit="";
		/// <summary>档案来源单位</summary>	
		[Description("档案来源单位")]
        public string SourceUnit { get{return _SourceUnit;} set{_SourceUnit = value??"";} } // SourceUnit
		private string _SourceUnit="";
		/// <summary>档案转出单位</summary>	
		[Description("档案转出单位")]
        public string ExitUnit { get{return _ExitUnit;} set{_ExitUnit = value??"";} } // ExitUnit
		private string _ExitUnit="";
		/// <summary>报到证提交时间</summary>	
		[Description("报到证提交时间")]
        public DateTime? ReportCardSubDate { get; set; } // ReportCardSubDate
		/// <summary>转入时间</summary>	
		[Description("转入时间")]
        public DateTime? EnterDate { get; set; } // EnterDate
		/// <summary>转出时间</summary>	
		[Description("转出时间")]
        public DateTime? ExitDate { get; set; } // ExitDate
		/// <summary>户口类型</summary>	
		[Description("户口类型")]
        public string ResidentAccountsType { get{return _ResidentAccountsType;} set{_ResidentAccountsType = value??"";} } // ResidentAccountsType
		private string _ResidentAccountsType="";
		/// <summary>户口所属街道</summary>	
		[Description("户口所属街道")]
        public string ResidentAccountsStreet { get{return _ResidentAccountsStreet;} set{_ResidentAccountsStreet = value??"";} } // ResidentAccountsStreet
		private string _ResidentAccountsStreet="";
    }

	/// <summary>T</summary>	
	[Description("T")]
    public partial class T_EmployeeQualification : Formula.BaseModel
    {
		/// <summary>ID</summary>	
		[Description("ID")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID (Primary key)
		private string _ID="";
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? ModifyDate { get; set; } // ModifyDate
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get{return _CreateUserID;} set{_CreateUserID = value??"";} } // CreateUserID
		private string _CreateUserID="";
		/// <summary></summary>	
		[Description("")]
        public string CreateUser { get{return _CreateUser;} set{_CreateUser = value??"";} } // CreateUser
		private string _CreateUser="";
		/// <summary></summary>	
		[Description("")]
        public string ModifyUserID { get{return _ModifyUserID;} set{_ModifyUserID = value??"";} } // ModifyUserID
		private string _ModifyUserID="";
		/// <summary></summary>	
		[Description("")]
        public string ModifyUser { get{return _ModifyUser;} set{_ModifyUser = value??"";} } // ModifyUser
		private string _ModifyUser="";
		/// <summary></summary>	
		[Description("")]
        public string OrgID { get{return _OrgID;} set{_OrgID = value??"";} } // OrgID
		private string _OrgID="";
		/// <summary></summary>	
		[Description("")]
        public string PrjID { get{return _PrjID;} set{_PrjID = value??"";} } // PrjID
		private string _PrjID="";
		/// <summary></summary>	
		[Description("")]
        public string FlowPhase { get{return _FlowPhase;} set{_FlowPhase = value??"";} } // FlowPhase
		private string _FlowPhase="";
		/// <summary></summary>	
		[Description("")]
        public string StepName { get{return _StepName;} set{_StepName = value??"";} } // StepName
		private string _StepName="";
		/// <summary>登记人</summary>	
		[Description("登记人")]
        public string RegisterID { get{return _RegisterID;} set{_RegisterID = value??"";} } // RegisterID
		private string _RegisterID="";
		/// <summary>登记人名称</summary>	
		[Description("登记人名称")]
        public string RegisterIDName { get{return _RegisterIDName;} set{_RegisterIDName = value??"";} } // RegisterIDName
		private string _RegisterIDName="";
		/// <summary>登记日期</summary>	
		[Description("登记日期")]
        public DateTime? RegisteDate { get; set; } // RegisteDate
		/// <summary>执业资格名称</summary>	
		[Description("执业资格名称")]
        public string QualificationName { get{return _QualificationName;} set{_QualificationName = value??"";} } // QualificationName
		private string _QualificationName="";
		/// <summary>执业资格证书编号</summary>	
		[Description("执业资格证书编号")]
        public string QualificationCode { get{return _QualificationCode;} set{_QualificationCode = value??"";} } // QualificationCode
		private string _QualificationCode="";
		/// <summary>第一专业</summary>	
		[Description("第一专业")]
        public string FirstMajor { get{return _FirstMajor;} set{_FirstMajor = value??"";} } // FirstMajor
		private string _FirstMajor="";
		/// <summary>第二专业</summary>	
		[Description("第二专业")]
        public string TwoMajor { get{return _TwoMajor;} set{_TwoMajor = value??"";} } // TwoMajor
		private string _TwoMajor="";
		/// <summary>执业资格证书发证时间</summary>	
		[Description("执业资格证书发证时间")]
        public DateTime? QualificationIssueDate { get; set; } // QualificationIssueDate
		/// <summary>执业证书保管人</summary>	
		[Description("执业证书保管人")]
        public string QualificationKeeperID { get{return _QualificationKeeperID;} set{_QualificationKeeperID = value??"";} } // QualificationKeeperID
		private string _QualificationKeeperID="";
		/// <summary>执业证书保管人名称</summary>	
		[Description("执业证书保管人名称")]
        public string QualificationKeeperIDName { get{return _QualificationKeeperIDName;} set{_QualificationKeeperIDName = value??"";} } // QualificationKeeperIDName
		private string _QualificationKeeperIDName="";
		/// <summary>注册证书编号</summary>	
		[Description("注册证书编号")]
        public string RegisteCode { get{return _RegisteCode;} set{_RegisteCode = value??"";} } // RegisteCode
		private string _RegisteCode="";
		/// <summary>注册证发证日期</summary>	
		[Description("注册证发证日期")]
        public DateTime? RegisteIssueDate { get; set; } // RegisteIssueDate
		/// <summary>注册证书失效时间</summary>	
		[Description("注册证书失效时间")]
        public DateTime? RegistelLoseDate { get; set; } // RegistelLoseDate
		/// <summary>注册证书保管人</summary>	
		[Description("注册证书保管人")]
        public string RegisteKeeperID { get{return _RegisteKeeperID;} set{_RegisteKeeperID = value??"";} } // RegisteKeeperID
		private string _RegisteKeeperID="";
		/// <summary>注册证书保管人名称</summary>	
		[Description("注册证书保管人名称")]
        public string RegisteKeeperIDName { get{return _RegisteKeeperIDName;} set{_RegisteKeeperIDName = value??"";} } // RegisteKeeperIDName
		private string _RegisteKeeperIDName="";
		/// <summary>注册印章编号</summary>	
		[Description("注册印章编号")]
        public string SealCode { get{return _SealCode;} set{_SealCode = value??"";} } // SealCode
		private string _SealCode="";
		/// <summary>注册印章失效日期</summary>	
		[Description("注册印章失效日期")]
        public DateTime? SealLoseDate { get; set; } // SealLoseDate
		/// <summary>注册印章保管者</summary>	
		[Description("注册印章保管者")]
        public string SealKeeperID { get{return _SealKeeperID;} set{_SealKeeperID = value??"";} } // SealKeeperID
		private string _SealKeeperID="";
		/// <summary>注册印章保管者名称</summary>	
		[Description("注册印章保管者名称")]
        public string SealKeeperIDName { get{return _SealKeeperIDName;} set{_SealKeeperIDName = value??"";} } // SealKeeperIDName
		private string _SealKeeperIDName="";
		/// <summary>继续教育参加时间</summary>	
		[Description("继续教育参加时间")]
        public DateTime? ContinueTrainDate { get; set; } // ContinueTrainDate
		/// <summary>继续教育学时</summary>	
		[Description("继续教育学时")]
        public double? ContinueTrainLength { get; set; } // ContinueTrainLength
		/// <summary>继续教育已完成学时</summary>	
		[Description("继续教育已完成学时")]
        public double? ContinueTrainCompleteLength { get; set; } // ContinueTrainCompleteLength
		/// <summary>执业资格证书附件</summary>	
		[Description("执业资格证书附件")]
        public string QualificationAttachment { get{return _QualificationAttachment;} set{_QualificationAttachment = value??"";} } // QualificationAttachment
		private string _QualificationAttachment="";
		/// <summary>注册证附件</summary>	
		[Description("注册证附件")]
        public string RegisteAttachment { get{return _RegisteAttachment;} set{_RegisteAttachment = value??"";} } // RegisteAttachment
		private string _RegisteAttachment="";
		/// <summary>员工ID</summary>	
		[Description("员工ID")]
        public string EmployeeID { get{return _EmployeeID;} set{_EmployeeID = value??"";} } // EmployeeID
		private string _EmployeeID="";
    }

	/// <summary>T</summary>	
	[Description("T")]
    public partial class T_EmployeeRetired : Formula.BaseModel
    {
		/// <summary>ID</summary>	
		[Description("ID")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID (Primary key)
		private string _ID="";
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? ModifyDate { get; set; } // ModifyDate
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get{return _CreateUserID;} set{_CreateUserID = value??"";} } // CreateUserID
		private string _CreateUserID="";
		/// <summary></summary>	
		[Description("")]
        public string CreateUser { get{return _CreateUser;} set{_CreateUser = value??"";} } // CreateUser
		private string _CreateUser="";
		/// <summary></summary>	
		[Description("")]
        public string ModifyUserID { get{return _ModifyUserID;} set{_ModifyUserID = value??"";} } // ModifyUserID
		private string _ModifyUserID="";
		/// <summary></summary>	
		[Description("")]
        public string ModifyUser { get{return _ModifyUser;} set{_ModifyUser = value??"";} } // ModifyUser
		private string _ModifyUser="";
		/// <summary></summary>	
		[Description("")]
        public string OrgID { get{return _OrgID;} set{_OrgID = value??"";} } // OrgID
		private string _OrgID="";
		/// <summary></summary>	
		[Description("")]
        public string PrjID { get{return _PrjID;} set{_PrjID = value??"";} } // PrjID
		private string _PrjID="";
		/// <summary></summary>	
		[Description("")]
        public string FlowPhase { get{return _FlowPhase;} set{_FlowPhase = value??"";} } // FlowPhase
		private string _FlowPhase="";
		/// <summary></summary>	
		[Description("")]
        public string StepName { get{return _StepName;} set{_StepName = value??"";} } // StepName
		private string _StepName="";
		/// <summary>员工编号</summary>	
		[Description("员工编号")]
        public string EmployeeCode { get{return _EmployeeCode;} set{_EmployeeCode = value??"";} } // EmployeeCode
		private string _EmployeeCode="";
		/// <summary>员工姓名</summary>	
		[Description("员工姓名")]
        public string EmployeeName { get{return _EmployeeName;} set{_EmployeeName = value??"";} } // EmployeeName
		private string _EmployeeName="";
		/// <summary>离退时间</summary>	
		[Description("离退时间")]
        public DateTime? RetiredDate { get; set; } // RetiredDate
		/// <summary>离退类型</summary>	
		[Description("离退类型")]
        public string Type { get{return _Type;} set{_Type = value??"";} } // Type
		private string _Type="";
		/// <summary>离退去向</summary>	
		[Description("离退去向")]
        public string Direction { get{return _Direction;} set{_Direction = value??"";} } // Direction
		private string _Direction="";
		/// <summary>离退原因</summary>	
		[Description("离退原因")]
        public string Reason { get{return _Reason;} set{_Reason = value??"";} } // Reason
		private string _Reason="";
		/// <summary>备注</summary>	
		[Description("备注")]
        public string Remark { get{return _Remark;} set{_Remark = value??"";} } // Remark
		private string _Remark="";
		/// <summary>员工ID</summary>	
		[Description("员工ID")]
        public string EmployeeID { get{return _EmployeeID;} set{_EmployeeID = value??"";} } // EmployeeID
		private string _EmployeeID="";
    }

	/// <summary>T</summary>	
	[Description("T")]
    public partial class T_EmployeeSocialSecurity : Formula.BaseModel
    {
		/// <summary>ID</summary>	
		[Description("ID")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID (Primary key)
		private string _ID="";
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? ModifyDate { get; set; } // ModifyDate
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get{return _CreateUserID;} set{_CreateUserID = value??"";} } // CreateUserID
		private string _CreateUserID="";
		/// <summary></summary>	
		[Description("")]
        public string CreateUser { get{return _CreateUser;} set{_CreateUser = value??"";} } // CreateUser
		private string _CreateUser="";
		/// <summary></summary>	
		[Description("")]
        public string ModifyUserID { get{return _ModifyUserID;} set{_ModifyUserID = value??"";} } // ModifyUserID
		private string _ModifyUserID="";
		/// <summary></summary>	
		[Description("")]
        public string ModifyUser { get{return _ModifyUser;} set{_ModifyUser = value??"";} } // ModifyUser
		private string _ModifyUser="";
		/// <summary></summary>	
		[Description("")]
        public string OrgID { get{return _OrgID;} set{_OrgID = value??"";} } // OrgID
		private string _OrgID="";
		/// <summary></summary>	
		[Description("")]
        public string PrjID { get{return _PrjID;} set{_PrjID = value??"";} } // PrjID
		private string _PrjID="";
		/// <summary></summary>	
		[Description("")]
        public string FlowPhase { get{return _FlowPhase;} set{_FlowPhase = value??"";} } // FlowPhase
		private string _FlowPhase="";
		/// <summary></summary>	
		[Description("")]
        public string StepName { get{return _StepName;} set{_StepName = value??"";} } // StepName
		private string _StepName="";
		/// <summary>姓名</summary>	
		[Description("姓名")]
        public string Name { get{return _Name;} set{_Name = value??"";} } // Name
		private string _Name="";
		/// <summary>与员工关系</summary>	
		[Description("与员工关系")]
        public string Relation { get{return _Relation;} set{_Relation = value??"";} } // Relation
		private string _Relation="";
		/// <summary>出生日期</summary>	
		[Description("出生日期")]
        public DateTime? BirthDate { get; set; } // BirthDate
		/// <summary>性别</summary>	
		[Description("性别")]
        public string Sex { get{return _Sex;} set{_Sex = value??"";} } // Sex
		private string _Sex="";
		/// <summary>工作单位</summary>	
		[Description("工作单位")]
        public string WorkUnit { get{return _WorkUnit;} set{_WorkUnit = value??"";} } // WorkUnit
		private string _WorkUnit="";
		/// <summary>职务</summary>	
		[Description("职务")]
        public string Job { get{return _Job;} set{_Job = value??"";} } // Job
		private string _Job="";
		/// <summary>联系电话</summary>	
		[Description("联系电话")]
        public string Phone { get{return _Phone;} set{_Phone = value??"";} } // Phone
		private string _Phone="";
		/// <summary>备注</summary>	
		[Description("备注")]
        public string Remark { get{return _Remark;} set{_Remark = value??"";} } // Remark
		private string _Remark="";
		/// <summary>员工ID</summary>	
		[Description("员工ID")]
        public string EmployeeID { get{return _EmployeeID;} set{_EmployeeID = value??"";} } // EmployeeID
		private string _EmployeeID="";
    }

	/// <summary>T</summary>	
	[Description("T")]
    public partial class T_EmployeeWorkHistory : Formula.BaseModel
    {
		/// <summary>ID</summary>	
		[Description("ID")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID (Primary key)
		private string _ID="";
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? ModifyDate { get; set; } // ModifyDate
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get{return _CreateUserID;} set{_CreateUserID = value??"";} } // CreateUserID
		private string _CreateUserID="";
		/// <summary></summary>	
		[Description("")]
        public string CreateUser { get{return _CreateUser;} set{_CreateUser = value??"";} } // CreateUser
		private string _CreateUser="";
		/// <summary></summary>	
		[Description("")]
        public string ModifyUserID { get{return _ModifyUserID;} set{_ModifyUserID = value??"";} } // ModifyUserID
		private string _ModifyUserID="";
		/// <summary></summary>	
		[Description("")]
        public string ModifyUser { get{return _ModifyUser;} set{_ModifyUser = value??"";} } // ModifyUser
		private string _ModifyUser="";
		/// <summary></summary>	
		[Description("")]
        public string OrgID { get{return _OrgID;} set{_OrgID = value??"";} } // OrgID
		private string _OrgID="";
		/// <summary></summary>	
		[Description("")]
        public string PrjID { get{return _PrjID;} set{_PrjID = value??"";} } // PrjID
		private string _PrjID="";
		/// <summary></summary>	
		[Description("")]
        public string FlowPhase { get{return _FlowPhase;} set{_FlowPhase = value??"";} } // FlowPhase
		private string _FlowPhase="";
		/// <summary></summary>	
		[Description("")]
        public string StepName { get{return _StepName;} set{_StepName = value??"";} } // StepName
		private string _StepName="";
		/// <summary>员工ID</summary>	
		[Description("员工ID")]
        public string EmployeeID { get{return _EmployeeID;} set{_EmployeeID = value??"";} } // EmployeeID
		private string _EmployeeID="";
		/// <summary>入职时间</summary>	
		[Description("入职时间")]
        public DateTime? JoinCompanyDate { get; set; } // JoinCompanyDate
		/// <summary>离职时间</summary>	
		[Description("离职时间")]
        public DateTime? LeaveCompanyDate { get; set; } // LeaveCompanyDate
		/// <summary>公司名称</summary>	
		[Description("公司名称")]
        public string CompanyName { get{return _CompanyName;} set{_CompanyName = value??"";} } // CompanyName
		private string _CompanyName="";
		/// <summary>部门和职务</summary>	
		[Description("部门和职务")]
        public string DeptAndPost { get{return _DeptAndPost;} set{_DeptAndPost = value??"";} } // DeptAndPost
		private string _DeptAndPost="";
		/// <summary>职责描述</summary>	
		[Description("职责描述")]
        public string Description { get{return _Description;} set{_Description = value??"";} } // Description
		private string _Description="";
		/// <summary>业绩</summary>	
		[Description("业绩")]
        public string Achievement { get{return _Achievement;} set{_Achievement = value??"";} } // Achievement
		private string _Achievement="";
		/// <summary>附件</summary>	
		[Description("附件")]
        public string Attachment { get{return _Attachment;} set{_Attachment = value??"";} } // Attachment
		private string _Attachment="";
		/// <summary>备注</summary>	
		[Description("备注")]
        public string Remark { get{return _Remark;} set{_Remark = value??"";} } // Remark
		private string _Remark="";
    }

	/// <summary>工作业绩</summary>	
	[Description("工作业绩")]
    public partial class T_EmployeeWorkPerformance : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID (Primary key)
		private string _ID="";
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? ModifyDate { get; set; } // ModifyDate
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get{return _CreateUserID;} set{_CreateUserID = value??"";} } // CreateUserID
		private string _CreateUserID="";
		/// <summary></summary>	
		[Description("")]
        public string CreateUser { get{return _CreateUser;} set{_CreateUser = value??"";} } // CreateUser
		private string _CreateUser="";
		/// <summary></summary>	
		[Description("")]
        public string ModifyUserID { get{return _ModifyUserID;} set{_ModifyUserID = value??"";} } // ModifyUserID
		private string _ModifyUserID="";
		/// <summary></summary>	
		[Description("")]
        public string ModifyUser { get{return _ModifyUser;} set{_ModifyUser = value??"";} } // ModifyUser
		private string _ModifyUser="";
		/// <summary></summary>	
		[Description("")]
        public string OrgID { get{return _OrgID;} set{_OrgID = value??"";} } // OrgID
		private string _OrgID="";
		/// <summary></summary>	
		[Description("")]
        public string CompanyID { get{return _CompanyID;} set{_CompanyID = value??"";} } // CompanyID
		private string _CompanyID="";
		/// <summary></summary>	
		[Description("")]
        public string FlowPhase { get{return _FlowPhase;} set{_FlowPhase = value??"";} } // FlowPhase
		private string _FlowPhase="";
		/// <summary></summary>	
		[Description("")]
        public string FlowInfo { get{return _FlowInfo;} set{_FlowInfo = value??"";} } // FlowInfo
		private string _FlowInfo="";
		/// <summary></summary>	
		[Description("")]
        public string StepName { get{return _StepName;} set{_StepName = value??"";} } // StepName
		private string _StepName="";
		/// <summary>员工ID</summary>	
		[Description("员工ID")]
        public string EmployeeID { get{return _EmployeeID;} set{_EmployeeID = value??"";} } // EmployeeID
		private string _EmployeeID="";
		/// <summary>项目编号</summary>	
		[Description("项目编号")]
        public string Code { get{return _Code;} set{_Code = value??"";} } // Code
		private string _Code="";
		/// <summary>项目名称</summary>	
		[Description("项目名称")]
        public string Name { get{return _Name;} set{_Name = value??"";} } // Name
		private string _Name="";
		/// <summary>项目类型</summary>	
		[Description("项目类型")]
        public string ProjectClass { get{return _ProjectClass;} set{_ProjectClass = value??"";} } // ProjectClass
		private string _ProjectClass="";
		/// <summary>项目规模</summary>	
		[Description("项目规模")]
        public string ProjectLevel { get{return _ProjectLevel;} set{_ProjectLevel = value??"";} } // ProjectLevel
		private string _ProjectLevel="";
		/// <summary>生产项目状态</summary>	
		[Description("生产项目状态")]
        public string ProjectState { get{return _ProjectState;} set{_ProjectState = value??"";} } // ProjectState
		private string _ProjectState="";
		/// <summary>项目开始日期</summary>	
		[Description("项目开始日期")]
        public DateTime? PlanStartDate { get; set; } // PlanStartDate
		/// <summary>项目结束日期</summary>	
		[Description("项目结束日期")]
        public DateTime? FactFinishDate { get; set; } // FactFinishDate
		/// <summary>项目范围及任务描述</summary>	
		[Description("项目范围及任务描述")]
        public string ProjectDescription { get{return _ProjectDescription;} set{_ProjectDescription = value??"";} } // ProjectDescription
		private string _ProjectDescription="";
		/// <summary>备注</summary>	
		[Description("备注")]
        public string Remark { get{return _Remark;} set{_Remark = value??"";} } // Remark
		private string _Remark="";
		/// <summary>附件</summary>	
		[Description("附件")]
        public string Attachment { get{return _Attachment;} set{_Attachment = value??"";} } // Attachment
		private string _Attachment="";
		/// <summary>用户ID</summary>	
		[Description("用户ID")]
        public string UserID { get{return _UserID;} set{_UserID = value??"";} } // UserID
		private string _UserID="";
		/// <summary>关联业绩ID</summary>	
		[Description("关联业绩ID")]
        public string RelateID { get{return _RelateID;} set{_RelateID = value??"";} } // RelateID
		private string _RelateID="";
		/// <summary>担任项目角色</summary>	
		[Description("担任项目角色")]
        public string ProjectRole { get{return _ProjectRole;} set{_ProjectRole = value??"";} } // ProjectRole
		private string _ProjectRole="";
    }

	/// <summary>岗位/岗级</summary>	
	[Description("岗位/岗级")]
    public partial class T_EmployeeWorkPost : Formula.BaseModel
    {
		/// <summary>ID</summary>	
		[Description("ID")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID (Primary key)
		private string _ID="";
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? ModifyDate { get; set; } // ModifyDate
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get{return _CreateUserID;} set{_CreateUserID = value??"";} } // CreateUserID
		private string _CreateUserID="";
		/// <summary></summary>	
		[Description("")]
        public string CreateUser { get{return _CreateUser;} set{_CreateUser = value??"";} } // CreateUser
		private string _CreateUser="";
		/// <summary></summary>	
		[Description("")]
        public string ModifyUserID { get{return _ModifyUserID;} set{_ModifyUserID = value??"";} } // ModifyUserID
		private string _ModifyUserID="";
		/// <summary></summary>	
		[Description("")]
        public string ModifyUser { get{return _ModifyUser;} set{_ModifyUser = value??"";} } // ModifyUser
		private string _ModifyUser="";
		/// <summary></summary>	
		[Description("")]
        public string OrgID { get{return _OrgID;} set{_OrgID = value??"";} } // OrgID
		private string _OrgID="";
		/// <summary></summary>	
		[Description("")]
        public string PrjID { get{return _PrjID;} set{_PrjID = value??"";} } // PrjID
		private string _PrjID="";
		/// <summary></summary>	
		[Description("")]
        public string FlowPhase { get{return _FlowPhase;} set{_FlowPhase = value??"";} } // FlowPhase
		private string _FlowPhase="";
		/// <summary></summary>	
		[Description("")]
        public string StepName { get{return _StepName;} set{_StepName = value??"";} } // StepName
		private string _StepName="";
		/// <summary>岗位</summary>	
		[Description("岗位")]
        public string Post { get{return _Post;} set{_Post = value??"";} } // Post
		private string _Post="";
		/// <summary>岗级</summary>	
		[Description("岗级")]
        public string PostLevel { get{return _PostLevel;} set{_PostLevel = value??"";} } // PostLevel
		private string _PostLevel="";
		/// <summary>生效时间</summary>	
		[Description("生效时间")]
        public DateTime? EffectiveDate { get; set; } // EffectiveDate
		/// <summary>员工ID</summary>	
		[Description("员工ID")]
        public string EmployeeID { get{return _EmployeeID;} set{_EmployeeID = value??"";} } // EmployeeID
		private string _EmployeeID="";
		/// <summary></summary>	
		[Description("")]
        public string IsTheNewest { get{return _IsTheNewest;} set{_IsTheNewest = value??"";} } // IsTheNewest
		private string _IsTheNewest="";
		/// <summary>岗位名称</summary>	
		[Description("岗位名称")]
        public string PostName { get{return _PostName;} set{_PostName = value??"";} } // PostName
		private string _PostName="";
		/// <summary>test</summary>	
		[Description("test")]
        public string test { get{return _test;} set{_test = value??"";} } // test
		private string _test="";
    }

	/// <summary></summary>	
	[Description("")]
    public partial class V_All_Attendance : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public int ID { get; set; } // ID
		/// <summary></summary>	
		[Description("")]
        public string UserName { get{return _UserName;} set{_UserName = value??"";} } // UserName
		private string _UserName="";
		/// <summary></summary>	
		[Description("")]
        public string WorkNo { get{return _WorkNo;} set{_WorkNo = value??"";} } // WorkNo
		private string _WorkNo="";
		/// <summary></summary>	
		[Description("")]
        public string OrgName { get{return _OrgName;} set{_OrgName = value??"";} } // OrgName
		private string _OrgName="";
		/// <summary></summary>	
		[Description("")]
        public string RQ { get{return _RQ;} set{_RQ = value??"";} } // RQ
		private string _RQ="";
		/// <summary></summary>	
		[Description("")]
        public string SingInTime { get{return _SingInTime;} set{_SingInTime = value??"";} } // SingInTime
		private string _SingInTime="";
		/// <summary></summary>	
		[Description("")]
        public string ModifySingInTime { get{return _ModifySingInTime;} set{_ModifySingInTime = value??"";} } // ModifySingInTime
		private string _ModifySingInTime="";
		/// <summary></summary>	
		[Description("")]
        public string SingOutTime { get{return _SingOutTime;} set{_SingOutTime = value??"";} } // SingOutTime
		private string _SingOutTime="";
		/// <summary></summary>	
		[Description("")]
        public string ModifySingOutTime { get{return _ModifySingOutTime;} set{_ModifySingOutTime = value??"";} } // ModifySingOutTime
		private string _ModifySingOutTime="";
		/// <summary></summary>	
		[Description("")]
        public string Status { get{return _Status;} set{_Status = value??"";} } // Status
		private string _Status="";
    }

	/// <summary></summary>	
	[Description("")]
    public partial class V_All_AttendanceInfo : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID
		private string _ID="";
		/// <summary></summary>	
		[Description("")]
        public string UserID { get{return _UserID;} set{_UserID = value??"";} } // UserID
		private string _UserID="";
		/// <summary></summary>	
		[Description("")]
        public DateTime? CheckDate { get; set; } // CheckDate
		/// <summary></summary>	
		[Description("")]
        public string UserName { get{return _UserName;} set{_UserName = value??"";} } // UserName
		private string _UserName="";
		/// <summary></summary>	
		[Description("")]
        public string WorkNo { get{return _WorkNo;} set{_WorkNo = value??"";} } // WorkNo
		private string _WorkNo="";
		/// <summary></summary>	
		[Description("")]
        public string OrgID { get{return _OrgID;} set{_OrgID = value??"";} } // OrgID
		private string _OrgID="";
		/// <summary></summary>	
		[Description("")]
        public string OrgName { get{return _OrgName;} set{_OrgName = value??"";} } // OrgName
		private string _OrgName="";
		/// <summary></summary>	
		[Description("")]
        public DateTime? AMCheckDate { get; set; } // AMCheckDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? AMModifyCheckDate { get; set; } // AMModifyCheckDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? PMCheckDate { get; set; } // PMCheckDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? PMModifyCheckDate { get; set; } // PMModifyCheckDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? AMDate { get; set; } // AMDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? PMDate { get; set; } // PMDate
		/// <summary></summary>	
		[Description("")]
        public string IsToday { get{return _IsToday;} set{_IsToday = value??"";} } // IsToday
		private string _IsToday="";
		/// <summary></summary>	
		[Description("")]
        public string AttendanceState { get{return _AttendanceState;} set{_AttendanceState = value??"";} } // AttendanceState
		private string _AttendanceState="";
    }

	/// <summary></summary>	
	[Description("")]
    public partial class V_S_D_UserAptitude : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID
		private string _ID="";
		/// <summary></summary>	
		[Description("")]
        public string UserID { get{return _UserID;} set{_UserID = value??"";} } // UserID
		private string _UserID="";
		/// <summary></summary>	
		[Description("")]
        public string Name { get{return _Name;} set{_Name = value??"";} } // Name
		private string _Name="";
		/// <summary></summary>	
		[Description("")]
        public string EngageMajor { get{return _EngageMajor;} set{_EngageMajor = value??"";} } // EngageMajor
		private string _EngageMajor="";
		/// <summary></summary>	
		[Description("")]
        public string Postion { get{return _Postion;} set{_Postion = value??"";} } // Postion
		private string _Postion="";
		/// <summary></summary>	
		[Description("")]
        public string AptitudeLevel { get{return _AptitudeLevel;} set{_AptitudeLevel = value??"";} } // AptitudeLevel
		private string _AptitudeLevel="";
		/// <summary></summary>	
		[Description("")]
        public string Major { get{return _Major;} set{_Major = value??"";} } // Major
		private string _Major="";
    }

	/// <summary></summary>	
	[Description("")]
    public partial class V_S_W_AttendanceEditLog : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID
		private string _ID="";
		/// <summary></summary>	
		[Description("")]
        public string SourceID { get{return _SourceID;} set{_SourceID = value??"";} } // SourceID
		private string _SourceID="";
		/// <summary></summary>	
		[Description("")]
        public string ModifyReason { get{return _ModifyReason;} set{_ModifyReason = value??"";} } // ModifyReason
		private string _ModifyReason="";
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get{return _CreateUserID;} set{_CreateUserID = value??"";} } // CreateUserID
		private string _CreateUserID="";
		/// <summary></summary>	
		[Description("")]
        public string CreateUserName { get{return _CreateUserName;} set{_CreateUserName = value??"";} } // CreateUserName
		private string _CreateUserName="";
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public string UserID { get{return _UserID;} set{_UserID = value??"";} } // UserID
		private string _UserID="";
		/// <summary></summary>	
		[Description("")]
        public string UserName { get{return _UserName;} set{_UserName = value??"";} } // UserName
		private string _UserName="";
		/// <summary></summary>	
		[Description("")]
        public string WorkNo { get{return _WorkNo;} set{_WorkNo = value??"";} } // WorkNo
		private string _WorkNo="";
		/// <summary></summary>	
		[Description("")]
        public string OrgID { get{return _OrgID;} set{_OrgID = value??"";} } // OrgID
		private string _OrgID="";
		/// <summary></summary>	
		[Description("")]
        public string OrgName { get{return _OrgName;} set{_OrgName = value??"";} } // OrgName
		private string _OrgName="";
		/// <summary></summary>	
		[Description("")]
        public string CheckType { get{return _CheckType;} set{_CheckType = value??"";} } // CheckType
		private string _CheckType="";
		/// <summary></summary>	
		[Description("")]
        public DateTime? CheckDate { get; set; } // CheckDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? LastModifyCheckDate { get; set; } // LastModifyCheckDate
    }

	/// <summary></summary>	
	[Description("")]
    public partial class V_S_W_AttendanceInfo : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID
		private string _ID="";
		/// <summary></summary>	
		[Description("")]
        public string UserID { get{return _UserID;} set{_UserID = value??"";} } // UserID
		private string _UserID="";
		/// <summary></summary>	
		[Description("")]
        public string UserName { get{return _UserName;} set{_UserName = value??"";} } // UserName
		private string _UserName="";
		/// <summary></summary>	
		[Description("")]
        public string WorkNo { get{return _WorkNo;} set{_WorkNo = value??"";} } // WorkNo
		private string _WorkNo="";
		/// <summary></summary>	
		[Description("")]
        public DateTime? WorkDay { get; set; } // WorkDay
		/// <summary></summary>	
		[Description("")]
        public DateTime? CheckDate { get; set; } // CheckDate
		/// <summary></summary>	
		[Description("")]
        public string CheckType { get{return _CheckType;} set{_CheckType = value??"";} } // CheckType
		private string _CheckType="";
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get{return _CreateUserID;} set{_CreateUserID = value??"";} } // CreateUserID
		private string _CreateUserID="";
		/// <summary></summary>	
		[Description("")]
        public string CreateUserName { get{return _CreateUserName;} set{_CreateUserName = value??"";} } // CreateUserName
		private string _CreateUserName="";
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public string ModifyUserID { get{return _ModifyUserID;} set{_ModifyUserID = value??"";} } // ModifyUserID
		private string _ModifyUserID="";
		/// <summary></summary>	
		[Description("")]
        public string ModifyUserName { get{return _ModifyUserName;} set{_ModifyUserName = value??"";} } // ModifyUserName
		private string _ModifyUserName="";
		/// <summary></summary>	
		[Description("")]
        public DateTime? ModifyDate { get; set; } // ModifyDate
		/// <summary></summary>	
		[Description("")]
        public string OrgID { get{return _OrgID;} set{_OrgID = value??"";} } // OrgID
		private string _OrgID="";
		/// <summary></summary>	
		[Description("")]
        public string OrgName { get{return _OrgName;} set{_OrgName = value??"";} } // OrgName
		private string _OrgName="";
    }

	/// <summary></summary>	
	[Description("")]
    public partial class V_T_QualificationNoReturn : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID
		private string _ID="";
		/// <summary></summary>	
		[Description("")]
        public string SerialNumber { get{return _SerialNumber;} set{_SerialNumber = value??"";} } // SerialNumber
		private string _SerialNumber="";
		/// <summary></summary>	
		[Description("")]
        public string LendID { get{return _LendID;} set{_LendID = value??"";} } // LendID
		private string _LendID="";
		/// <summary></summary>	
		[Description("")]
        public string ApplyUserID { get{return _ApplyUserID;} set{_ApplyUserID = value??"";} } // ApplyUserID
		private string _ApplyUserID="";
		/// <summary></summary>	
		[Description("")]
        public string ApplyUserIDName { get{return _ApplyUserIDName;} set{_ApplyUserIDName = value??"";} } // ApplyUserIDName
		private string _ApplyUserIDName="";
		/// <summary></summary>	
		[Description("")]
        public DateTime? ApplyDate { get; set; } // ApplyDate
		/// <summary></summary>	
		[Description("")]
        public string Name { get{return _Name;} set{_Name = value??"";} } // Name
		private string _Name="";
		/// <summary></summary>	
		[Description("")]
        public string CertificateNo { get{return _CertificateNo;} set{_CertificateNo = value??"";} } // CertificateNo
		private string _CertificateNo="";
		/// <summary></summary>	
		[Description("")]
        public string QualificationLevel { get{return _QualificationLevel;} set{_QualificationLevel = value??"";} } // QualificationLevel
		private string _QualificationLevel="";
		/// <summary></summary>	
		[Description("")]
        public string QualificationID { get{return _QualificationID;} set{_QualificationID = value??"";} } // QualificationID
		private string _QualificationID="";
		/// <summary></summary>	
		[Description("")]
        public int? OriginalNum { get; set; } // OriginalNum
		/// <summary></summary>	
		[Description("")]
        public int? CopyNum { get; set; } // CopyNum
    }

	/// <summary></summary>	
	[Description("")]
    public partial class V_T_QualificationReturn : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID
		private string _ID="";
		/// <summary></summary>	
		[Description("")]
        public string ReturnID { get{return _ReturnID;} set{_ReturnID = value??"";} } // ReturnID
		private string _ReturnID="";
		/// <summary></summary>	
		[Description("")]
        public string SerialNumber { get{return _SerialNumber;} set{_SerialNumber = value??"";} } // SerialNumber
		private string _SerialNumber="";
		/// <summary></summary>	
		[Description("")]
        public DateTime? HandleDate { get; set; } // HandleDate
		/// <summary></summary>	
		[Description("")]
        public string HandleUserID { get{return _HandleUserID;} set{_HandleUserID = value??"";} } // HandleUserID
		private string _HandleUserID="";
		/// <summary></summary>	
		[Description("")]
        public string HandleUserIDName { get{return _HandleUserIDName;} set{_HandleUserIDName = value??"";} } // HandleUserIDName
		private string _HandleUserIDName="";
		/// <summary></summary>	
		[Description("")]
        public DateTime? ReturnDate { get; set; } // ReturnDate
		/// <summary></summary>	
		[Description("")]
        public string ReturnUserID { get{return _ReturnUserID;} set{_ReturnUserID = value??"";} } // ReturnUserID
		private string _ReturnUserID="";
		/// <summary></summary>	
		[Description("")]
        public string ReturnUserIDName { get{return _ReturnUserIDName;} set{_ReturnUserIDName = value??"";} } // ReturnUserIDName
		private string _ReturnUserIDName="";
		/// <summary></summary>	
		[Description("")]
        public DateTime? ApplyDate { get; set; } // ApplyDate
		/// <summary></summary>	
		[Description("")]
        public string ApplyUserID { get{return _ApplyUserID;} set{_ApplyUserID = value??"";} } // ApplyUserID
		private string _ApplyUserID="";
		/// <summary></summary>	
		[Description("")]
        public string ApplyUserIDName { get{return _ApplyUserIDName;} set{_ApplyUserIDName = value??"";} } // ApplyUserIDName
		private string _ApplyUserIDName="";
		/// <summary></summary>	
		[Description("")]
        public string CertificateNo { get{return _CertificateNo;} set{_CertificateNo = value??"";} } // CertificateNo
		private string _CertificateNo="";
		/// <summary></summary>	
		[Description("")]
        public string Name { get{return _Name;} set{_Name = value??"";} } // Name
		private string _Name="";
		/// <summary></summary>	
		[Description("")]
        public string QualificationID { get{return _QualificationID;} set{_QualificationID = value??"";} } // QualificationID
		private string _QualificationID="";
		/// <summary></summary>	
		[Description("")]
        public string DetailID { get{return _DetailID;} set{_DetailID = value??"";} } // DetailID
		private string _DetailID="";
		/// <summary></summary>	
		[Description("")]
        public string LendID { get{return _LendID;} set{_LendID = value??"";} } // LendID
		private string _LendID="";
		/// <summary></summary>	
		[Description("")]
        public string LendSerialNumber { get{return _LendSerialNumber;} set{_LendSerialNumber = value??"";} } // LendSerialNumber
		private string _LendSerialNumber="";
		/// <summary></summary>	
		[Description("")]
        public int? OriginalNum { get; set; } // OriginalNum
		/// <summary></summary>	
		[Description("")]
        public int? CopyNum { get; set; } // CopyNum
    }

	/// <summary></summary>	
	[Description("")]
    public partial class V_T_WIFixedSalary : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string EmployeeCode { get{return _EmployeeCode;} set{_EmployeeCode = value??"";} } // EmployeeCode
		private string _EmployeeCode="";
		/// <summary></summary>	
		[Description("")]
        public string EmployeeName { get{return _EmployeeName;} set{_EmployeeName = value??"";} } // EmployeeName
		private string _EmployeeName="";
		/// <summary></summary>	
		[Description("")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID
		private string _ID="";
		/// <summary></summary>	
		[Description("")]
        public string Remark { get{return _Remark;} set{_Remark = value??"";} } // Remark
		private string _Remark="";
		/// <summary></summary>	
		[Description("")]
        public string Year { get{return _Year;} set{_Year = value??"";} } // Year
		private string _Year="";
		/// <summary></summary>	
		[Description("")]
        public string Month { get{return _Month;} set{_Month = value??"";} } // Month
		private string _Month="";
		/// <summary></summary>	
		[Description("")]
        public decimal? BaseSalaryTotal { get; set; } // BaseSalaryTotal
		/// <summary></summary>	
		[Description("")]
        public decimal? PostSalary { get; set; } // PostSalary
		/// <summary></summary>	
		[Description("")]
        public decimal? GradeSalary { get; set; } // GradeSalary
		/// <summary></summary>	
		[Description("")]
        public decimal? NormalSalary { get; set; } // NormalSalary
		/// <summary></summary>	
		[Description("")]
        public decimal? AboveReservedSalary { get; set; } // AboveReservedSalary
		/// <summary></summary>	
		[Description("")]
        public decimal? SubsidyTotal { get; set; } // SubsidyTotal
		/// <summary></summary>	
		[Description("")]
        public decimal? PostSubsidy { get; set; } // PostSubsidy
		/// <summary></summary>	
		[Description("")]
        public decimal? ReservedSubsidy { get; set; } // ReservedSubsidy
		/// <summary></summary>	
		[Description("")]
        public decimal? LocalSubsidy { get; set; } // LocalSubsidy
		/// <summary></summary>	
		[Description("")]
        public decimal? YuanPostSubsidy { get; set; } // YuanPostSubsidy
		/// <summary></summary>	
		[Description("")]
        public decimal? RentingSubsidy { get; set; } // RentingSubsidy
		/// <summary></summary>	
		[Description("")]
        public decimal? RegisterSubsidy { get; set; } // RegisterSubsidy
		/// <summary></summary>	
		[Description("")]
        public decimal? LifeSubsidy { get; set; } // LifeSubsidy
		/// <summary></summary>	
		[Description("")]
        public decimal? MonthSubsidyTotal { get; set; } // MonthSubsidyTotal
		/// <summary></summary>	
		[Description("")]
        public string Sex { get{return _Sex;} set{_Sex = value??"";} } // Sex
		private string _Sex="";
		/// <summary></summary>	
		[Description("")]
        public DateTime? Birthday { get; set; } // Birthday
		/// <summary></summary>	
		[Description("")]
        public DateTime? JoinWorkDate { get; set; } // JoinWorkDate
		/// <summary></summary>	
		[Description("")]
        public string Educational { get{return _Educational;} set{_Educational = value??"";} } // Educational
		private string _Educational="";
		/// <summary></summary>	
		[Description("")]
        public string DeptID { get{return _DeptID;} set{_DeptID = value??"";} } // DeptID
		private string _DeptID="";
		/// <summary></summary>	
		[Description("")]
        public string DeptName { get{return _DeptName;} set{_DeptName = value??"";} } // DeptName
		private string _DeptName="";
		/// <summary></summary>	
		[Description("")]
        public DateTime? MedicalInsurancePayDate { get; set; } // MedicalInsurancePayDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? EndowmentInsurancePayDate { get; set; } // EndowmentInsurancePayDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? FundPayDate { get; set; } // FundPayDate
    }

	/// <summary></summary>	
	[Description("")]
    public partial class V_T_WIFundPay : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID
		private string _ID="";
		/// <summary></summary>	
		[Description("")]
        public string EmployeeName { get{return _EmployeeName;} set{_EmployeeName = value??"";} } // EmployeeName
		private string _EmployeeName="";
		/// <summary></summary>	
		[Description("")]
        public string EmployeeCode { get{return _EmployeeCode;} set{_EmployeeCode = value??"";} } // EmployeeCode
		private string _EmployeeCode="";
		/// <summary></summary>	
		[Description("")]
        public string Sex { get{return _Sex;} set{_Sex = value??"";} } // Sex
		private string _Sex="";
		/// <summary></summary>	
		[Description("")]
        public string Post { get{return _Post;} set{_Post = value??"";} } // Post
		private string _Post="";
		/// <summary></summary>	
		[Description("")]
        public string EmploymentWay { get{return _EmploymentWay;} set{_EmploymentWay = value??"";} } // EmploymentWay
		private string _EmploymentWay="";
		/// <summary></summary>	
		[Description("")]
        public string Month { get{return _Month;} set{_Month = value??"";} } // Month
		private string _Month="";
		/// <summary></summary>	
		[Description("")]
        public string Year { get{return _Year;} set{_Year = value??"";} } // Year
		private string _Year="";
		/// <summary></summary>	
		[Description("")]
        public decimal ProvidentFundCardinality { get; set; } // ProvidentFundCardinality
		/// <summary></summary>	
		[Description("")]
        public string DeptName { get{return _DeptName;} set{_DeptName = value??"";} } // DeptName
		private string _DeptName="";
		/// <summary></summary>	
		[Description("")]
        public string DeptID { get{return _DeptID;} set{_DeptID = value??"";} } // DeptID
		private string _DeptID="";
		/// <summary></summary>	
		[Description("")]
        public decimal PersonProportion { get; set; } // PersonProportion
		/// <summary></summary>	
		[Description("")]
        public decimal DeptProportion { get; set; } // DeptProportion
		/// <summary></summary>	
		[Description("")]
        public decimal PersonalCardinality { get; set; } // PersonalCardinality
		/// <summary></summary>	
		[Description("")]
        public decimal Coprcardinality { get; set; } // Coprcardinality
		/// <summary></summary>	
		[Description("")]
        public decimal? TotalMoney { get; set; } // TotalMoney
		/// <summary></summary>	
		[Description("")]
        public string ProvidentFundAccount { get{return _ProvidentFundAccount;} set{_ProvidentFundAccount = value??"";} } // ProvidentFundAccount
		private string _ProvidentFundAccount="";
		/// <summary></summary>	
		[Description("")]
        public DateTime? JoinDate { get; set; } // JoinDate
		/// <summary></summary>	
		[Description("")]
        public string Quarter { get{return _Quarter;} set{_Quarter = value??"";} } // Quarter
		private string _Quarter="";
    }

	/// <summary></summary>	
	[Description("")]
    public partial class V_T_WISocialSecurityPay : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID
		private string _ID="";
		/// <summary></summary>	
		[Description("")]
        public string DeptName { get{return _DeptName;} set{_DeptName = value??"";} } // DeptName
		private string _DeptName="";
		/// <summary></summary>	
		[Description("")]
        public string Year { get{return _Year;} set{_Year = value??"";} } // Year
		private string _Year="";
		/// <summary></summary>	
		[Description("")]
        public string Month { get{return _Month;} set{_Month = value??"";} } // Month
		private string _Month="";
		/// <summary></summary>	
		[Description("")]
        public string EmployeeID { get{return _EmployeeID;} set{_EmployeeID = value??"";} } // EmployeeID
		private string _EmployeeID="";
		/// <summary></summary>	
		[Description("")]
        public decimal? MedicalInsuranceCardinality { get; set; } // MedicalInsuranceCardinality
		/// <summary></summary>	
		[Description("")]
        public string EmployeeName { get{return _EmployeeName;} set{_EmployeeName = value??"";} } // EmployeeName
		private string _EmployeeName="";
		/// <summary></summary>	
		[Description("")]
        public decimal? EndowmentInsuranceCardinality { get; set; } // EndowmentInsuranceCardinality
		/// <summary></summary>	
		[Description("")]
        public decimal? PensionDeptProportion { get; set; } // PensionDeptProportion
		/// <summary></summary>	
		[Description("")]
        public decimal? PensionPersonalBase { get; set; } // PensionPersonalBase
		/// <summary></summary>	
		[Description("")]
        public decimal? PensionpersonalProportion { get; set; } // PensionpersonalProportion
		/// <summary></summary>	
		[Description("")]
        public decimal? MedicalDeptProportion { get; set; } // MedicalDeptProportion
		/// <summary></summary>	
		[Description("")]
        public decimal? MedicalPersonalBase { get; set; } // MedicalPersonalBase
		/// <summary></summary>	
		[Description("")]
        public decimal? MedicalPersonalProportion { get; set; } // MedicalPersonalProportion
		/// <summary></summary>	
		[Description("")]
        public string Sex { get{return _Sex;} set{_Sex = value??"";} } // Sex
		private string _Sex="";
		/// <summary></summary>	
		[Description("")]
        public string EmployeeCode { get{return _EmployeeCode;} set{_EmployeeCode = value??"";} } // EmployeeCode
		private string _EmployeeCode="";
		/// <summary></summary>	
		[Description("")]
        public string MedicalInsuranceAccount { get{return _MedicalInsuranceAccount;} set{_MedicalInsuranceAccount = value??"";} } // MedicalInsuranceAccount
		private string _MedicalInsuranceAccount="";
		/// <summary></summary>	
		[Description("")]
        public string EndowmentInsuranceAccount { get{return _EndowmentInsuranceAccount;} set{_EndowmentInsuranceAccount = value??"";} } // EndowmentInsuranceAccount
		private string _EndowmentInsuranceAccount="";
		/// <summary></summary>	
		[Description("")]
        public DateTime? JoinDate { get; set; } // JoinDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? EndowmentJoinDate { get; set; } // EndowmentJoinDate
		/// <summary></summary>	
		[Description("")]
        public string Quarter { get{return _Quarter;} set{_Quarter = value??"";} } // Quarter
		private string _Quarter="";
		/// <summary></summary>	
		[Description("")]
        public decimal? EicoprCardinality { get; set; } // EicoprCardinality
		/// <summary></summary>	
		[Description("")]
        public decimal? EipCardinality { get; set; } // EipCardinality
		/// <summary></summary>	
		[Description("")]
        public decimal? MicoprCardinality { get; set; } // MicoprCardinality
		/// <summary></summary>	
		[Description("")]
        public decimal? MipCardinality { get; set; } // MipCardinality
		/// <summary></summary>	
		[Description("")]
        public string Post { get{return _Post;} set{_Post = value??"";} } // Post
		private string _Post="";
		/// <summary></summary>	
		[Description("")]
        public decimal? TotalMoney { get; set; } // TotalMoney
    }


    // ************************************************************************
    // POCO Configuration

    // R_W_AttendanceInfo
    internal partial class R_W_AttendanceInfoConfiguration : EntityTypeConfiguration<R_W_AttendanceInfo>
    {
        public R_W_AttendanceInfoConfiguration()
        {
            ToTable("R_W_ATTENDANCEINFO");
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
            Property(x => x.UserID).HasColumnName("USERID").IsOptional().HasMaxLength(200);
            Property(x => x.UserIDName).HasColumnName("USERIDNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Year).HasColumnName("YEAR").IsOptional();
            Property(x => x.Month).HasColumnName("MONTH").IsOptional();
            Property(x => x.Date).HasColumnName("DATE").IsOptional();
            Property(x => x.Morning).HasColumnName("MORNING").IsOptional().HasMaxLength(200);
            Property(x => x.MorningType).HasColumnName("MORNINGTYPE").IsOptional().HasMaxLength(200);
            Property(x => x.PostTime).HasColumnName("POSTTIME").IsOptional();
            Property(x => x.Afternoon).HasColumnName("AFTERNOON").IsOptional().HasMaxLength(200);
            Property(x => x.AfternoonType).HasColumnName("AFTERNOONTYPE").IsOptional().HasMaxLength(200);
            Property(x => x.LeaveTime).HasColumnName("LEAVETIME").IsOptional();
        }
    }

    // S_C_Award
    internal partial class S_C_AwardConfiguration : EntityTypeConfiguration<S_C_Award>
    {
        public S_C_AwardConfiguration()
        {
            ToTable("S_C_AWARD");
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
            Property(x => x.AwardYear).HasColumnName("AWARDYEAR").IsOptional().HasMaxLength(200);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(200);
            Property(x => x.AwardLevel).HasColumnName("AWARDLEVEL").IsOptional().HasMaxLength(200);
            Property(x => x.AwardGrade).HasColumnName("AWARDGRADE").IsOptional().HasMaxLength(200);
            Property(x => x.AwardType).HasColumnName("AWARDTYPE").IsOptional().HasMaxLength(200);
            Property(x => x.CompletePerson).HasColumnName("COMPLETEPERSON").IsOptional().HasMaxLength(200);
            Property(x => x.CompletePersonName).HasColumnName("COMPLETEPERSONNAME").IsOptional().HasMaxLength(200);
            Property(x => x.CompleteUnit).HasColumnName("COMPLETEUNIT").IsOptional().HasMaxLength(200);
            Property(x => x.AwardDate).HasColumnName("AWARDDATE").IsOptional();
            Property(x => x.AwardAmount).HasColumnName("AWARDAMOUNT").IsOptional().HasMaxLength(200);
            Property(x => x.AwardAUnit).HasColumnName("AWARDAUNIT").IsOptional().HasMaxLength(200);
            Property(x => x.InsideAffiliatedPerson).HasColumnName("INSIDEAFFILIATEDPERSON").IsOptional();
            Property(x => x.InsideAffiliatedPersonName).HasColumnName("INSIDEAFFILIATEDPERSONNAME").IsOptional();
            Property(x => x.OutsideAffiliatedPerson).HasColumnName("OUTSIDEAFFILIATEDPERSON").IsOptional().HasMaxLength(200);
            Property(x => x.Attachment).HasColumnName("ATTACHMENT").IsOptional().HasMaxLength(500);
            Property(x => x.Remark).HasColumnName("REMARK").IsOptional().HasMaxLength(500);
            Property(x => x.HoldDept).HasColumnName("HOLDDEPT").IsOptional().HasMaxLength(200);
            Property(x => x.HoldDeptName).HasColumnName("HOLDDEPTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Keeper).HasColumnName("KEEPER").IsOptional().HasMaxLength(200);
            Property(x => x.KeeperName).HasColumnName("KEEPERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Type).HasColumnName("TYPE").IsOptional().HasMaxLength(200);
            Property(x => x.State).HasColumnName("STATE").IsOptional().HasMaxLength(200);
            Property(x => x.Borrower).HasColumnName("BORROWER").IsOptional().HasMaxLength(200);
            Property(x => x.BorrowerName).HasColumnName("BORROWERNAME").IsOptional().HasMaxLength(200);
        }
    }

    // S_C_AwardProject
    internal partial class S_C_AwardProjectConfiguration : EntityTypeConfiguration<S_C_AwardProject>
    {
        public S_C_AwardProjectConfiguration()
        {
            ToTable("S_C_AWARDPROJECT");
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
            Property(x => x.AwardYear).HasColumnName("AWARDYEAR").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectName).HasColumnName("PROJECTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectNameName).HasColumnName("PROJECTNAMENAME").IsOptional().HasMaxLength(200);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(200);
            Property(x => x.AwardLevel).HasColumnName("AWARDLEVEL").IsOptional().HasMaxLength(200);
            Property(x => x.AwardGrade).HasColumnName("AWARDGRADE").IsOptional().HasMaxLength(200);
            Property(x => x.AwardType).HasColumnName("AWARDTYPE").IsOptional().HasMaxLength(200);
            Property(x => x.CompletePerson).HasColumnName("COMPLETEPERSON").IsOptional().HasMaxLength(200);
            Property(x => x.CompletePersonName).HasColumnName("COMPLETEPERSONNAME").IsOptional().HasMaxLength(200);
            Property(x => x.CompleteUnit).HasColumnName("COMPLETEUNIT").IsOptional().HasMaxLength(200);
            Property(x => x.AwardDate).HasColumnName("AWARDDATE").IsOptional();
            Property(x => x.AwardAmount).HasColumnName("AWARDAMOUNT").IsOptional().HasMaxLength(200);
            Property(x => x.AwardAUnit).HasColumnName("AWARDAUNIT").IsOptional().HasMaxLength(200);
            Property(x => x.InsideAffiliatedPerson).HasColumnName("INSIDEAFFILIATEDPERSON").IsOptional();
            Property(x => x.InsideAffiliatedPersonName).HasColumnName("INSIDEAFFILIATEDPERSONNAME").IsOptional();
            Property(x => x.OutsideAffiliatedPerson).HasColumnName("OUTSIDEAFFILIATEDPERSON").IsOptional().HasMaxLength(200);
            Property(x => x.Attachment).HasColumnName("ATTACHMENT").IsOptional().HasMaxLength(500);
            Property(x => x.Remark).HasColumnName("REMARK").IsOptional().HasMaxLength(500);
            Property(x => x.HoldDept).HasColumnName("HOLDDEPT").IsOptional().HasMaxLength(200);
            Property(x => x.HoldDeptName).HasColumnName("HOLDDEPTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Keeper).HasColumnName("KEEPER").IsOptional().HasMaxLength(200);
            Property(x => x.KeeperName).HasColumnName("KEEPERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Type).HasColumnName("TYPE").IsOptional().HasMaxLength(200);
            Property(x => x.State).HasColumnName("STATE").IsOptional().HasMaxLength(200);
            Property(x => x.Borrower).HasColumnName("BORROWER").IsOptional().HasMaxLength(200);
            Property(x => x.BorrowerName).HasColumnName("BORROWERNAME").IsOptional().HasMaxLength(200);
        }
    }

    // S_C_Certificate
    internal partial class S_C_CertificateConfiguration : EntityTypeConfiguration<S_C_Certificate>
    {
        public S_C_CertificateConfiguration()
        {
            ToTable("S_C_CERTIFICATE");
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
            Property(x => x.OrdinalNumber).HasColumnName("ORDINALNUMBER").IsOptional().HasMaxLength(200);
            Property(x => x.CertificateLevel).HasColumnName("CERTIFICATELEVEL").IsOptional().HasMaxLength(200);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(200);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(200);
            Property(x => x.IssuingAuthority).HasColumnName("ISSUINGAUTHORITY").IsOptional().HasMaxLength(200);
            Property(x => x.IssueDate).HasColumnName("ISSUEDATE").IsOptional();
            Property(x => x.ValidPeriod).HasColumnName("VALIDPERIOD").IsOptional();
            Property(x => x.DeclareDate).HasColumnName("DECLAREDATE").IsOptional();
            Property(x => x.InspectionDate).HasColumnName("INSPECTIONDATE").IsOptional();
            Property(x => x.ChangeDate).HasColumnName("CHANGEDATE").IsOptional();
            Property(x => x.PromotionDate).HasColumnName("PROMOTIONDATE").IsOptional();
            Property(x => x.UpgradeDate).HasColumnName("UPGRADEDATE").IsOptional();
            Property(x => x.BusinessRange).HasColumnName("BUSINESSRANGE").IsOptional().HasMaxLength(500);
            Property(x => x.ConfigureDemand).HasColumnName("CONFIGUREDEMAND").IsOptional().HasMaxLength(500);
            Property(x => x.ManagementLevel).HasColumnName("MANAGEMENTLEVEL").IsOptional().HasMaxLength(500);
            Property(x => x.BusinessDemand).HasColumnName("BUSINESSDEMAND").IsOptional().HasMaxLength(500);
            Property(x => x.PeopleConfigure).HasColumnName("PEOPLECONFIGURE").IsOptional().HasMaxLength(500);
            Property(x => x.BusinessSituation).HasColumnName("BUSINESSSITUATION").IsOptional().HasMaxLength(500);
            Property(x => x.ExistingProblem).HasColumnName("EXISTINGPROBLEM").IsOptional().HasMaxLength(500);
            Property(x => x.Attachment).HasColumnName("ATTACHMENT").IsOptional().HasMaxLength(500);
            Property(x => x.Remark).HasColumnName("REMARK").IsOptional().HasMaxLength(500);
            Property(x => x.HoldDept).HasColumnName("HOLDDEPT").IsOptional().HasMaxLength(200);
            Property(x => x.HoldDeptName).HasColumnName("HOLDDEPTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Keeper).HasColumnName("KEEPER").IsOptional().HasMaxLength(200);
            Property(x => x.KeeperName).HasColumnName("KEEPERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Type).HasColumnName("TYPE").IsOptional().HasMaxLength(200);
            Property(x => x.State).HasColumnName("STATE").IsOptional().HasMaxLength(200);
            Property(x => x.Borrower).HasColumnName("BORROWER").IsOptional().HasMaxLength(200);
            Property(x => x.BorrowerName).HasColumnName("BORROWERNAME").IsOptional().HasMaxLength(200);
        }
    }

    // S_C_Patent
    internal partial class S_C_PatentConfiguration : EntityTypeConfiguration<S_C_Patent>
    {
        public S_C_PatentConfiguration()
        {
            ToTable("S_C_PATENT");
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
            Property(x => x.ArchiveNumber).HasColumnName("ARCHIVENUMBER").IsOptional().HasMaxLength(200);
            Property(x => x.ApplyDate).HasColumnName("APPLYDATE").IsOptional();
            Property(x => x.ApplyNumber).HasColumnName("APPLYNUMBER").IsOptional().HasMaxLength(200);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(200);
            Property(x => x.Inventor).HasColumnName("INVENTOR").IsOptional().HasMaxLength(200);
            Property(x => x.LawState).HasColumnName("LAWSTATE").IsOptional().HasMaxLength(200);
            Property(x => x.Attachment).HasColumnName("ATTACHMENT").IsOptional().HasMaxLength(500);
            Property(x => x.Remark).HasColumnName("REMARK").IsOptional().HasMaxLength(500);
            Property(x => x.Keeper).HasColumnName("KEEPER").IsOptional().HasMaxLength(200);
            Property(x => x.KeeperName).HasColumnName("KEEPERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.HoldDept).HasColumnName("HOLDDEPT").IsOptional().HasMaxLength(200);
            Property(x => x.HoldDeptName).HasColumnName("HOLDDEPTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Type).HasColumnName("TYPE").IsOptional().HasMaxLength(200);
            Property(x => x.State).HasColumnName("STATE").IsOptional().HasMaxLength(200);
            Property(x => x.Borrower).HasColumnName("BORROWER").IsOptional().HasMaxLength(200);
            Property(x => x.BorrowerName).HasColumnName("BORROWERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.PatentNo).HasColumnName("PATENTNO").IsOptional().HasMaxLength(200);
            Property(x => x.PatentPerson).HasColumnName("PATENTPERSON").IsOptional().HasMaxLength(200);
            Property(x => x.AuthorizationNum).HasColumnName("AUTHORIZATIONNUM").IsOptional();
            Property(x => x.PatentType).HasColumnName("PATENTTYPE").IsOptional().HasMaxLength(200);
        }
    }

    // S_C_QualificationApplyLog
    internal partial class S_C_QualificationApplyLogConfiguration : EntityTypeConfiguration<S_C_QualificationApplyLog>
    {
        public S_C_QualificationApplyLogConfiguration()
        {
            ToTable("S_C_QUALIFICATIONAPPLYLOG");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.QualificationID).HasColumnName("QUALIFICATIONID").IsOptional().HasMaxLength(50);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(50);
            Property(x => x.QualificationType).HasColumnName("QUALIFICATIONTYPE").IsOptional().HasMaxLength(50);
            Property(x => x.BorrowUserID).HasColumnName("BORROWUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.BorrowUserName).HasColumnName("BORROWUSERNAME").IsOptional().HasMaxLength(50);
            Property(x => x.BorrowerDeptID).HasColumnName("BORROWERDEPTID").IsOptional().HasMaxLength(50);
            Property(x => x.BorrowerDeptName).HasColumnName("BORROWERDEPTNAME").IsOptional().HasMaxLength(50);
            Property(x => x.BorrowerTel).HasColumnName("BORROWERTEL").IsOptional().HasMaxLength(50);
            Property(x => x.BorrowDate).HasColumnName("BORROWDATE").IsOptional();
            Property(x => x.PlanReturnDate).HasColumnName("PLANRETURNDATE").IsOptional();
            Property(x => x.ReturnDate).HasColumnName("RETURNDATE").IsOptional();
            Property(x => x.QualificationApplyID).HasColumnName("QUALIFICATIONAPPLYID").IsOptional().HasMaxLength(50);
        }
    }

    // S_D_UserAptitude
    internal partial class S_D_UserAptitudeConfiguration : EntityTypeConfiguration<S_D_UserAptitude>
    {
        public S_D_UserAptitudeConfiguration()
        {
            ToTable("S_D_USERAPTITUDE");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.UserID).HasColumnName("USERID").IsRequired().HasMaxLength(50);
            Property(x => x.UserName).HasColumnName("USERNAME").IsRequired().HasMaxLength(50);
            Property(x => x.HREmployeeID).HasColumnName("HREMPLOYEEID").IsOptional().HasMaxLength(50);
            Property(x => x.DeptID).HasColumnName("DEPTID").IsOptional().HasMaxLength(50);
            Property(x => x.DeptName).HasColumnName("DEPTNAME").IsOptional().HasMaxLength(50);
            Property(x => x.Major).HasColumnName("MAJOR").IsOptional().HasMaxLength(50);
            Property(x => x.ProjectClass).HasColumnName("PROJECTCLASS").IsOptional().HasMaxLength(50);
            Property(x => x.Position).HasColumnName("POSITION").IsRequired().HasMaxLength(50);
            Property(x => x.AptitudeLevel).HasColumnName("APTITUDELEVEL").IsRequired();
        }
    }

    // S_D_UserAptitudeApply
    internal partial class S_D_UserAptitudeApplyConfiguration : EntityTypeConfiguration<S_D_UserAptitudeApply>
    {
        public S_D_UserAptitudeApplyConfiguration()
        {
            ToTable("S_D_USERAPTITUDEAPPLY");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.UserAptitudeApplyID).HasColumnName("USERAPTITUDEAPPLYID").IsOptional().HasMaxLength(50);
            Property(x => x.UserID).HasColumnName("USERID").IsRequired().HasMaxLength(50);
            Property(x => x.UserName).HasColumnName("USERNAME").IsRequired().HasMaxLength(50);
            Property(x => x.HREmployeeID).HasColumnName("HREMPLOYEEID").IsOptional().HasMaxLength(50);
            Property(x => x.DeptID).HasColumnName("DEPTID").IsOptional().HasMaxLength(50);
            Property(x => x.DeptName).HasColumnName("DEPTNAME").IsOptional().HasMaxLength(50);
            Property(x => x.Major).HasColumnName("MAJOR").IsOptional().HasMaxLength(50);
            Property(x => x.ProjectClass).HasColumnName("PROJECTCLASS").IsOptional().HasMaxLength(50);
            Property(x => x.Position).HasColumnName("POSITION").IsRequired().HasMaxLength(50);
            Property(x => x.AptitudeLevel).HasColumnName("APTITUDELEVEL").IsRequired();
            Property(x => x.ProjectInfoID).HasColumnName("PROJECTINFOID").IsOptional().HasMaxLength(50);
            Property(x => x.ProjectInfoName).HasColumnName("PROJECTINFONAME").IsOptional().HasMaxLength(50);
        }
    }

    // S_P_Performance
    internal partial class S_P_PerformanceConfiguration : EntityTypeConfiguration<S_P_Performance>
    {
        public S_P_PerformanceConfiguration()
        {
            ToTable("S_P_PERFORMANCE");
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
            Property(x => x.ProjectInfoID).HasColumnName("PROJECTINFOID").IsOptional().HasMaxLength(200);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(200);
            Property(x => x.CustomerID).HasColumnName("CUSTOMERID").IsOptional().HasMaxLength(200);
            Property(x => x.CustomerName).HasColumnName("CUSTOMERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.PhaseName).HasColumnName("PHASENAME").IsOptional().HasMaxLength(200);
            Property(x => x.Industry).HasColumnName("INDUSTRY").IsOptional().HasMaxLength(200);
            Property(x => x.Country).HasColumnName("COUNTRY").IsOptional().HasMaxLength(200);
            Property(x => x.Province).HasColumnName("PROVINCE").IsOptional().HasMaxLength(200);
            Property(x => x.City).HasColumnName("CITY").IsOptional().HasMaxLength(200);
            Property(x => x.BuildAddress).HasColumnName("BUILDADDRESS").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectClass).HasColumnName("PROJECTCLASS").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectLevel).HasColumnName("PROJECTLEVEL").IsOptional().HasMaxLength(200);
            Property(x => x.ChargeUser).HasColumnName("CHARGEUSER").IsOptional().HasMaxLength(500);
            Property(x => x.ChargeUserName).HasColumnName("CHARGEUSERNAME").IsOptional().HasMaxLength(500);
            Property(x => x.PlanStartDate).HasColumnName("PLANSTARTDATE").IsOptional();
            Property(x => x.FactFinishDate).HasColumnName("FACTFINISHDATE").IsOptional();
            Property(x => x.ProjectState).HasColumnName("PROJECTSTATE").IsOptional().HasMaxLength(200);
            Property(x => x.SignDate).HasColumnName("SIGNDATE").IsOptional();
            Property(x => x.ContractBelongYearMonth).HasColumnName("CONTRACTBELONGYEARMONTH").IsOptional().HasMaxLength(200);
            Property(x => x.EndDate).HasColumnName("ENDDATE").IsOptional();
            Property(x => x.ContractRMBAmount).HasColumnName("CONTRACTRMBAMOUNT").IsOptional();
            Property(x => x.AwardInfo).HasColumnName("AWARDINFO").IsOptional().HasMaxLength(500);
            Property(x => x.PhaseValue).HasColumnName("PHASEVALUE").IsOptional().HasMaxLength(200);
            Property(x => x.ContractID).HasColumnName("CONTRACTID").IsOptional().HasMaxLength(200);
        }
    }

    // S_P_Performance_JoinPeople
    internal partial class S_P_Performance_JoinPeopleConfiguration : EntityTypeConfiguration<S_P_Performance_JoinPeople>
    {
        public S_P_Performance_JoinPeopleConfiguration()
        {
            ToTable("S_P_PERFORMANCE_JOINPEOPLE");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.S_P_PerformanceID).HasColumnName("S_P_PERFORMANCEID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.Major).HasColumnName("MAJOR").IsOptional().HasMaxLength(200);
            Property(x => x.MajorPrinciple).HasColumnName("MAJORPRINCIPLE").IsOptional().HasMaxLength(500);
            Property(x => x.MajorPrincipleName).HasColumnName("MAJORPRINCIPLENAME").IsOptional().HasMaxLength(500);
            Property(x => x.Designer).HasColumnName("DESIGNER").IsOptional().HasMaxLength(500);
            Property(x => x.DesignerName).HasColumnName("DESIGNERNAME").IsOptional().HasMaxLength(500);
            Property(x => x.Collactor).HasColumnName("COLLACTOR").IsOptional().HasMaxLength(500);
            Property(x => x.CollactorName).HasColumnName("COLLACTORNAME").IsOptional().HasMaxLength(500);
            Property(x => x.Auditor).HasColumnName("AUDITOR").IsOptional().HasMaxLength(500);
            Property(x => x.AuditorName).HasColumnName("AUDITORNAME").IsOptional().HasMaxLength(500);
            Property(x => x.Approver).HasColumnName("APPROVER").IsOptional().HasMaxLength(500);
            Property(x => x.ApproverName).HasColumnName("APPROVERNAME").IsOptional().HasMaxLength(500);

            // Foreign keys
            HasOptional(a => a.S_P_Performance).WithMany(b => b.S_P_Performance_JoinPeople).HasForeignKey(c => c.S_P_PerformanceID); // FK_S_P_Performance_JoinPeople_S_P_Performance
        }
    }

    // S_S_PostLevelTemplate
    internal partial class S_S_PostLevelTemplateConfiguration : EntityTypeConfiguration<S_S_PostLevelTemplate>
    {
        public S_S_PostLevelTemplateConfiguration()
        {
            ToTable("S_S_POSTLEVELTEMPLATE");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.ModifyDate).HasColumnName("MODIFYDATE").IsOptional();
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUser).HasColumnName("MODIFYUSER").IsOptional().HasMaxLength(50);
            Property(x => x.OrgID).HasColumnName("ORGID").IsOptional().HasMaxLength(50);
            Property(x => x.FlowPhase).HasColumnName("FLOWPHASE").IsOptional().HasMaxLength(50);
            Property(x => x.StepName).HasColumnName("STEPNAME").IsOptional().HasMaxLength(500);
            Property(x => x.BelongYear).HasColumnName("BELONGYEAR").IsOptional().HasMaxLength(50);
            Property(x => x.BelongMonth).HasColumnName("BELONGMONTH").IsOptional().HasMaxLength(50);
            Property(x => x.BelongQuarter).HasColumnName("BELONGQUARTER").IsOptional().HasMaxLength(50);
            Property(x => x.Remark).HasColumnName("REMARK").IsOptional().HasMaxLength(2000);
            Property(x => x.TemplateCode).HasColumnName("TEMPLATECODE").IsOptional().HasMaxLength(200);
            Property(x => x.TemplateName).HasColumnName("TEMPLATENAME").IsOptional().HasMaxLength(200);
            Property(x => x.StartUseDate).HasColumnName("STARTUSEDATE").IsOptional();
        }
    }

    // S_S_PostLevelTemplate_PostList
    internal partial class S_S_PostLevelTemplate_PostListConfiguration : EntityTypeConfiguration<S_S_PostLevelTemplate_PostList>
    {
        public S_S_PostLevelTemplate_PostListConfiguration()
        {
            ToTable("S_S_POSTLEVELTEMPLATE_POSTLIST");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.S_S_PostLevelTemplateID).HasColumnName("S_S_POSTLEVELTEMPLATEID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.PostCode).HasColumnName("POSTCODE").IsOptional().HasMaxLength(200);
            Property(x => x.PostLevelCode).HasColumnName("POSTLEVELCODE").IsOptional().HasMaxLength(200);
            Property(x => x.BelongYear).HasColumnName("BELONGYEAR").IsOptional().HasMaxLength(200);
            Property(x => x.BelongMonth).HasColumnName("BELONGMONTH").IsOptional().HasMaxLength(200);
            Property(x => x.BelongQuarter).HasColumnName("BELONGQUARTER").IsOptional().HasMaxLength(200);
            Property(x => x.ReMark).HasColumnName("REMARK").IsOptional().HasMaxLength(2000);
            Property(x => x.PostName).HasColumnName("POSTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.PostLevelName).HasColumnName("POSTLEVELNAME").IsOptional().HasMaxLength(200);
            Property(x => x.BaseSalary).HasColumnName("BASESALARY").IsOptional().HasPrecision(18,4);
            Property(x => x.StartUseDate).HasColumnName("STARTUSEDATE").IsOptional();

            // Foreign keys
            HasOptional(a => a.S_S_PostLevelTemplate).WithMany(b => b.S_S_PostLevelTemplate_PostList).HasForeignKey(c => c.S_S_PostLevelTemplateID); // FK_S_S_PostLevelTemplate_PostList_S_S_PostLevelTemplate
        }
    }

    // S_S_TaxThreshold
    internal partial class S_S_TaxThresholdConfiguration : EntityTypeConfiguration<S_S_TaxThreshold>
    {
        public S_S_TaxThresholdConfiguration()
        {
            ToTable("S_S_TAXTHRESHOLD");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.ModifyDate).HasColumnName("MODIFYDATE").IsOptional();
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUser).HasColumnName("MODIFYUSER").IsOptional().HasMaxLength(50);
            Property(x => x.OrgID).HasColumnName("ORGID").IsOptional().HasMaxLength(50);
            Property(x => x.FlowPhase).HasColumnName("FLOWPHASE").IsOptional().HasMaxLength(50);
            Property(x => x.StepName).HasColumnName("STEPNAME").IsOptional().HasMaxLength(500);
            Property(x => x.BelongYear).HasColumnName("BELONGYEAR").IsOptional().HasMaxLength(200);
            Property(x => x.BelongMonth).HasColumnName("BELONGMONTH").IsOptional().HasMaxLength(200);
            Property(x => x.TaxThreshold).HasColumnName("TAXTHRESHOLD").IsOptional().HasMaxLength(200);
            Property(x => x.StartUseDate).HasColumnName("STARTUSEDATE").IsOptional();
            Property(x => x.TaxRate).HasColumnName("TAXRATE").IsOptional();
        }
    }

    // S_W_AttendanceAnnualLeave
    internal partial class S_W_AttendanceAnnualLeaveConfiguration : EntityTypeConfiguration<S_W_AttendanceAnnualLeave>
    {
        public S_W_AttendanceAnnualLeaveConfiguration()
        {
            ToTable("S_W_ATTENDANCEANNUALLEAVE");
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
            Property(x => x.UserID).HasColumnName("USERID").IsOptional().HasMaxLength(200);
            Property(x => x.UserIDName).HasColumnName("USERIDNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Dept).HasColumnName("DEPT").IsOptional().HasMaxLength(200);
            Property(x => x.DeptName).HasColumnName("DEPTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Year).HasColumnName("YEAR").IsOptional();
            Property(x => x.Days).HasColumnName("DAYS").IsOptional();
        }
    }

    // S_W_AttendanceConfig
    internal partial class S_W_AttendanceConfigConfiguration : EntityTypeConfiguration<S_W_AttendanceConfig>
    {
        public S_W_AttendanceConfigConfiguration()
        {
            ToTable("S_W_ATTENDANCECONFIG");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.AmCheckDate).HasColumnName("AMCHECKDATE").IsOptional();
            Property(x => x.AmLateCheckDate).HasColumnName("AMLATECHECKDATE").IsOptional();
            Property(x => x.PmCheckDate).HasColumnName("PMCHECKDATE").IsOptional();
            Property(x => x.PmEarlyDate).HasColumnName("PMEARLYDATE").IsOptional();
            Property(x => x.PmEndDate).HasColumnName("PMENDDATE").IsOptional();
            Property(x => x.UseAttendanceInfo).HasColumnName("USEATTENDANCEINFO").IsOptional();
            Property(x => x.Description).HasColumnName("DESCRIPTION").IsOptional().HasMaxLength(500);
        }
    }

    // S_W_AttendanceEditLog
    internal partial class S_W_AttendanceEditLogConfiguration : EntityTypeConfiguration<S_W_AttendanceEditLog>
    {
        public S_W_AttendanceEditLogConfiguration()
        {
            ToTable("S_W_ATTENDANCEEDITLOG");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.SourceID).HasColumnName("SOURCEID").IsOptional().HasMaxLength(50);
            Property(x => x.WorkDay).HasColumnName("WORKDAY").IsOptional();
            Property(x => x.CheckDate).HasColumnName("CHECKDATE").IsOptional();
            Property(x => x.ModifyReason).HasColumnName("MODIFYREASON").IsOptional().HasMaxLength(500);
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUserName).HasColumnName("CREATEUSERNAME").IsOptional().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
        }
    }

    // S_W_AttendanceInfo
    internal partial class S_W_AttendanceInfoConfiguration : EntityTypeConfiguration<S_W_AttendanceInfo>
    {
        public S_W_AttendanceInfoConfiguration()
        {
            ToTable("S_W_ATTENDANCEINFO");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.UserID).HasColumnName("USERID").IsOptional().HasMaxLength(50);
            Property(x => x.UserName).HasColumnName("USERNAME").IsOptional().HasMaxLength(50);
            Property(x => x.WorkNo).HasColumnName("WORKNO").IsOptional().HasMaxLength(50);
            Property(x => x.WorkDay).HasColumnName("WORKDAY").IsOptional();
            Property(x => x.CheckDate).HasColumnName("CHECKDATE").IsOptional();
            Property(x => x.LastModifyCheckDate).HasColumnName("LASTMODIFYCHECKDATE").IsOptional();
            Property(x => x.CheckType).HasColumnName("CHECKTYPE").IsOptional().HasMaxLength(10);
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUserName).HasColumnName("CREATEUSERNAME").IsOptional().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUserName).HasColumnName("MODIFYUSERNAME").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyDate).HasColumnName("MODIFYDATE").IsOptional();
            Property(x => x.OrgID).HasColumnName("ORGID").IsOptional().HasMaxLength(50);
            Property(x => x.OrgName).HasColumnName("ORGNAME").IsOptional().HasMaxLength(200);
            Property(x => x.YDTID).HasColumnName("YDTID").IsOptional().HasMaxLength(50);
        }
    }

    // S_W_AttendanceInfoMultiEdit
    internal partial class S_W_AttendanceInfoMultiEditConfiguration : EntityTypeConfiguration<S_W_AttendanceInfoMultiEdit>
    {
        public S_W_AttendanceInfoMultiEditConfiguration()
        {
            ToTable("S_W_ATTENDANCEINFOMULTIEDIT");
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
            Property(x => x.UserIDs).HasColumnName("USERIDS").IsOptional().HasMaxLength(2000);
            Property(x => x.UserIDsName).HasColumnName("USERIDSNAME").IsOptional().HasMaxLength(2000);
            Property(x => x.StartDate).HasColumnName("STARTDATE").IsOptional();
            Property(x => x.EndDate).HasColumnName("ENDDATE").IsOptional();
            Property(x => x.Morning).HasColumnName("MORNING").IsOptional().HasMaxLength(200);
            Property(x => x.MorningType).HasColumnName("MORNINGTYPE").IsOptional().HasMaxLength(200);
            Property(x => x.Afternoon).HasColumnName("AFTERNOON").IsOptional().HasMaxLength(200);
            Property(x => x.AfternoonType).HasColumnName("AFTERNOONTYPE").IsOptional().HasMaxLength(200);
        }
    }

    // S_W_DefineUserActualUnitPrice
    internal partial class S_W_DefineUserActualUnitPriceConfiguration : EntityTypeConfiguration<S_W_DefineUserActualUnitPrice>
    {
        public S_W_DefineUserActualUnitPriceConfiguration()
        {
            ToTable("S_W_DEFINEUSERACTUALUNITPRICE");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.Name).HasColumnName("NAME").IsRequired().HasMaxLength(500);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(500);
            Property(x => x.ParamSource).HasColumnName("PARAMSOURCE").IsOptional();
            Property(x => x.UserSQL).HasColumnName("USERSQL").IsOptional();
            Property(x => x.CalcFormula).HasColumnName("CALCFORMULA").IsOptional();
            Property(x => x.Remark).HasColumnName("REMARK").IsOptional().HasMaxLength(500);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyDate).HasColumnName("MODIFYDATE").IsOptional();
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUser).HasColumnName("MODIFYUSER").IsOptional().HasMaxLength(50);
        }
    }

    // S_W_ProjectInfo
    internal partial class S_W_ProjectInfoConfiguration : EntityTypeConfiguration<S_W_ProjectInfo>
    {
        public S_W_ProjectInfoConfiguration()
        {
            ToTable("S_W_PROJECTINFO");
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
            Property(x => x.ChargerDept).HasColumnName("CHARGERDEPT").IsOptional().HasMaxLength(200);
            Property(x => x.ChargerDeptName).HasColumnName("CHARGERDEPTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ChargerUser).HasColumnName("CHARGERUSER").IsOptional().HasMaxLength(200);
            Property(x => x.ChargerUserName).HasColumnName("CHARGERUSERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.WorkHourType).HasColumnName("WORKHOURTYPE").IsOptional().HasMaxLength(200);
            Property(x => x.IsValid).HasColumnName("ISVALID").IsOptional().HasMaxLength(200);
        }
    }

    // S_W_ProjectInfo_SubProjectInfo
    internal partial class S_W_ProjectInfo_SubProjectInfoConfiguration : EntityTypeConfiguration<S_W_ProjectInfo_SubProjectInfo>
    {
        public S_W_ProjectInfo_SubProjectInfoConfiguration()
        {
            ToTable("S_W_PROJECTINFO_SUBPROJECTINFO");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.S_W_ProjectInfoID).HasColumnName("S_W_PROJECTINFOID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.SubProjectName).HasColumnName("SUBPROJECTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.SubProjectCode).HasColumnName("SUBPROJECTCODE").IsOptional().HasMaxLength(200);
            Property(x => x.MajorName).HasColumnName("MAJORNAME").IsOptional().HasMaxLength(200);
            Property(x => x.MajorCode).HasColumnName("MAJORCODE").IsOptional().HasMaxLength(200);

            // Foreign keys
            HasOptional(a => a.S_W_ProjectInfo).WithMany(b => b.S_W_ProjectInfo_SubProjectInfo).HasForeignKey(c => c.S_W_ProjectInfoID); // FK_S_W_ProjectInfo_SubProjectInfo_S_W_ProjectInfo
        }
    }

    // S_W_ResourcePrice
    internal partial class S_W_ResourcePriceConfiguration : EntityTypeConfiguration<S_W_ResourcePrice>
    {
        public S_W_ResourcePriceConfiguration()
        {
            ToTable("S_W_RESOURCEPRICE");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.ResourceCode).HasColumnName("RESOURCECODE").IsRequired().HasMaxLength(50);
            Property(x => x.ResourceName).HasColumnName("RESOURCENAME").IsRequired().HasMaxLength(50);
            Property(x => x.UnitPrice).HasColumnName("UNITPRICE").IsRequired().HasPrecision(18,2);
            Property(x => x.BelongYear).HasColumnName("BELONGYEAR").IsRequired();
            Property(x => x.BelongMonth).HasColumnName("BELONGMONTH").IsRequired();
            Property(x => x.StartDate).HasColumnName("STARTDATE").IsRequired();
            Property(x => x.Level).HasColumnName("LEVEL").IsOptional().HasMaxLength(50);
            Property(x => x.Postion).HasColumnName("POSTION").IsOptional().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyDate).HasColumnName("MODIFYDATE").IsOptional();
            Property(x => x.ModifyUser).HasColumnName("MODIFYUSER").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
        }
    }

    // S_W_UserSalary
    internal partial class S_W_UserSalaryConfiguration : EntityTypeConfiguration<S_W_UserSalary>
    {
        public S_W_UserSalaryConfiguration()
        {
            ToTable("S_W_USERSALARY");
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
            Property(x => x.BelongYear).HasColumnName("BELONGYEAR").IsOptional().HasMaxLength(200);
            Property(x => x.BelongMonth).HasColumnName("BELONGMONTH").IsOptional().HasMaxLength(200);
            Property(x => x.UserInfo).HasColumnName("USERINFO").IsOptional().HasMaxLength(200);
            Property(x => x.UserInfoName).HasColumnName("USERINFONAME").IsOptional().HasMaxLength(200);
            Property(x => x.DepartInfo).HasColumnName("DEPARTINFO").IsOptional().HasMaxLength(200);
            Property(x => x.DepartInfoName).HasColumnName("DEPARTINFONAME").IsOptional().HasMaxLength(200);
            Property(x => x.BasicSalary).HasColumnName("BASICSALARY").IsOptional().HasPrecision(18,4);
            Property(x => x.Bonus).HasColumnName("BONUS").IsOptional().HasPrecision(18,4);
            Property(x => x.Benefit).HasColumnName("BENEFIT").IsOptional().HasPrecision(18,4);
            Property(x => x.FiveOne).HasColumnName("FIVEONE").IsOptional().HasPrecision(18,4);
            Property(x => x.OtherValue).HasColumnName("OTHERVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.TotalValue).HasColumnName("TOTALVALUE").IsOptional().HasPrecision(18,4);
        }
    }

    // S_W_UserUnitPrice
    internal partial class S_W_UserUnitPriceConfiguration : EntityTypeConfiguration<S_W_UserUnitPrice>
    {
        public S_W_UserUnitPriceConfiguration()
        {
            ToTable("S_W_USERUNITPRICE");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.HRUserID).HasColumnName("HRUSERID").IsRequired().HasMaxLength(50);
            Property(x => x.UserID).HasColumnName("USERID").IsRequired().HasMaxLength(50);
            Property(x => x.UserName).HasColumnName("USERNAME").IsRequired().HasMaxLength(50);
            Property(x => x.UnitPrice).HasColumnName("UNITPRICE").IsRequired().HasPrecision(18,2);
            Property(x => x.ResourceCode).HasColumnName("RESOURCECODE").IsOptional().HasMaxLength(50);
            Property(x => x.PriceType).HasColumnName("PRICETYPE").IsRequired().HasMaxLength(50);
            Property(x => x.BelongYear).HasColumnName("BELONGYEAR").IsRequired();
            Property(x => x.BelongMonth).HasColumnName("BELONGMONTH").IsRequired();
            Property(x => x.StartDate).HasColumnName("STARTDATE").IsRequired();
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.ModifyDate).HasColumnName("MODIFYDATE").IsOptional();
            Property(x => x.ModifyUser).HasColumnName("MODIFYUSER").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
        }
    }

    // S_W_UserWorkHour
    internal partial class S_W_UserWorkHourConfiguration : EntityTypeConfiguration<S_W_UserWorkHour>
    {
        public S_W_UserWorkHourConfiguration()
        {
            ToTable("S_W_USERWORKHOUR");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.UserID).HasColumnName("USERID").IsRequired().HasMaxLength(50);
            Property(x => x.UserName).HasColumnName("USERNAME").IsRequired().HasMaxLength(50);
            Property(x => x.UserCode).HasColumnName("USERCODE").IsOptional().HasMaxLength(50);
            Property(x => x.EmployeeID).HasColumnName("EMPLOYEEID").IsOptional().HasMaxLength(50);
            Property(x => x.UserDeptID).HasColumnName("USERDEPTID").IsOptional().HasMaxLength(50);
            Property(x => x.UserDeptName).HasColumnName("USERDEPTNAME").IsOptional().HasMaxLength(500);
            Property(x => x.WorkHourDate).HasColumnName("WORKHOURDATE").IsRequired();
            Property(x => x.NormalValue).HasColumnName("NORMALVALUE").IsOptional().HasPrecision(18,2);
            Property(x => x.AdditionalValue).HasColumnName("ADDITIONALVALUE").IsOptional().HasPrecision(18,2);
            Property(x => x.WorkHourValue).HasColumnName("WORKHOURVALUE").IsOptional().HasPrecision(18,2);
            Property(x => x.ConfirmValue).HasColumnName("CONFIRMVALUE").IsOptional().HasPrecision(18,2);
            Property(x => x.State).HasColumnName("STATE").IsRequired().HasMaxLength(50);
            Property(x => x.BelongYear).HasColumnName("BELONGYEAR").IsRequired();
            Property(x => x.BelongMonth).HasColumnName("BELONGMONTH").IsRequired();
            Property(x => x.BelongQuarter).HasColumnName("BELONGQUARTER").IsRequired();
            Property(x => x.WorkHourType).HasColumnName("WORKHOURTYPE").IsRequired().HasMaxLength(50);
            Property(x => x.ProjectID).HasColumnName("PROJECTID").IsOptional().HasMaxLength(50);
            Property(x => x.ProjectCode).HasColumnName("PROJECTCODE").IsOptional().HasMaxLength(500);
            Property(x => x.ProjectName).HasColumnName("PROJECTNAME").IsOptional().HasMaxLength(500);
            Property(x => x.ProjectDept).HasColumnName("PROJECTDEPT").IsOptional().HasMaxLength(500);
            Property(x => x.ProjectDeptName).HasColumnName("PROJECTDEPTNAME").IsOptional().HasMaxLength(500);
            Property(x => x.ProjectChargerUser).HasColumnName("PROJECTCHARGERUSER").IsOptional().HasMaxLength(500);
            Property(x => x.ProjectChargerUserName).HasColumnName("PROJECTCHARGERUSERNAME").IsOptional().HasMaxLength(500);
            Property(x => x.SubProjectName).HasColumnName("SUBPROJECTNAME").IsOptional().HasMaxLength(500);
            Property(x => x.SubProjectCode).HasColumnName("SUBPROJECTCODE").IsOptional().HasMaxLength(500);
            Property(x => x.MajorCode).HasColumnName("MAJORCODE").IsOptional().HasMaxLength(500);
            Property(x => x.MajorName).HasColumnName("MAJORNAME").IsOptional().HasMaxLength(500);
            Property(x => x.TaskWorkCode).HasColumnName("TASKWORKCODE").IsOptional().HasMaxLength(500);
            Property(x => x.TaskWorkName).HasColumnName("TASKWORKNAME").IsOptional().HasMaxLength(500);
            Property(x => x.WorkContent).HasColumnName("WORKCONTENT").IsOptional();
            Property(x => x.WorkHourDay).HasColumnName("WORKHOURDAY").IsOptional().HasPrecision(18,2);
            Property(x => x.ConfirmDay).HasColumnName("CONFIRMDAY").IsOptional().HasPrecision(18,2);
            Property(x => x.Step1Value).HasColumnName("STEP1VALUE").IsOptional().HasPrecision(18,2);
            Property(x => x.Step1Day).HasColumnName("STEP1DAY").IsOptional().HasPrecision(18,2);
            Property(x => x.Step2Value).HasColumnName("STEP2VALUE").IsOptional().HasPrecision(18,2);
            Property(x => x.Step2Day).HasColumnName("STEP2DAY").IsOptional().HasPrecision(18,2);
            Property(x => x.Step1User).HasColumnName("STEP1USER").IsOptional().HasMaxLength(200);
            Property(x => x.Step1UserName).HasColumnName("STEP1USERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Step1Date).HasColumnName("STEP1DATE").IsOptional();
            Property(x => x.IsStep1).HasColumnName("ISSTEP1").IsOptional().HasMaxLength(200);
            Property(x => x.Step2User).HasColumnName("STEP2USER").IsOptional().HasMaxLength(200);
            Property(x => x.Step2UserName).HasColumnName("STEP2USERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Step2Date).HasColumnName("STEP2DATE").IsOptional();
            Property(x => x.IsStep2).HasColumnName("ISSTEP2").IsOptional().HasMaxLength(200);
            Property(x => x.ConfirmUser).HasColumnName("CONFIRMUSER").IsOptional().HasMaxLength(200);
            Property(x => x.ConfirmUserName).HasColumnName("CONFIRMUSERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ConfirmDate).HasColumnName("CONFIRMDATE").IsOptional();
            Property(x => x.IsConfirm).HasColumnName("ISCONFIRM").IsOptional().HasMaxLength(200);
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsOptional().HasMaxLength(50);
            Property(x => x.SupplementID).HasColumnName("SUPPLEMENTID").IsOptional().HasMaxLength(200);
        }
    }

    // S_W_UserWorkHour_ApproveDetail
    internal partial class S_W_UserWorkHour_ApproveDetailConfiguration : EntityTypeConfiguration<S_W_UserWorkHour_ApproveDetail>
    {
        public S_W_UserWorkHour_ApproveDetailConfiguration()
        {
            ToTable("S_W_USERWORKHOUR_APPROVEDETAIL");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.S_W_UserWorkHourID).HasColumnName("S_W_USERWORKHOURID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.ApproveValue).HasColumnName("APPROVEVALUE").IsOptional().HasPrecision(18,2);
            Property(x => x.ApproveDay).HasColumnName("APPROVEDAY").IsOptional().HasPrecision(18,2);
            Property(x => x.ApproveDate).HasColumnName("APPROVEDATE").IsOptional();
            Property(x => x.ApproveStep).HasColumnName("APPROVESTEP").IsOptional().HasMaxLength(200);
            Property(x => x.ApproveUser).HasColumnName("APPROVEUSER").IsOptional().HasMaxLength(200);
            Property(x => x.ApproveUserName).HasColumnName("APPROVEUSERNAME").IsOptional().HasMaxLength(200);

            // Foreign keys
            HasOptional(a => a.S_W_UserWorkHour).WithMany(b => b.S_W_UserWorkHour_ApproveDetail).HasForeignKey(c => c.S_W_UserWorkHourID); // FK_S_W_UserWorkHour_ApproveDetail_S_W_UserWorkHour
        }
    }

    // S_W_UserWorkHourSupplement
    internal partial class S_W_UserWorkHourSupplementConfiguration : EntityTypeConfiguration<S_W_UserWorkHourSupplement>
    {
        public S_W_UserWorkHourSupplementConfiguration()
        {
            ToTable("S_W_USERWORKHOURSUPPLEMENT");
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
            Property(x => x.UserID).HasColumnName("USERID").IsOptional().HasMaxLength(200);
            Property(x => x.UserIDName).HasColumnName("USERIDNAME").IsOptional().HasMaxLength(200);
            Property(x => x.UserDept).HasColumnName("USERDEPT").IsOptional().HasMaxLength(200);
            Property(x => x.UserDeptName).HasColumnName("USERDEPTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectID).HasColumnName("PROJECTID").IsOptional().HasMaxLength(500);
            Property(x => x.ProjectIDName).HasColumnName("PROJECTIDNAME").IsOptional().HasMaxLength(500);
            Property(x => x.WorkHourType).HasColumnName("WORKHOURTYPE").IsOptional().HasMaxLength(200);
            Property(x => x.SubProjectCode).HasColumnName("SUBPROJECTCODE").IsOptional().HasMaxLength(500);
            Property(x => x.MajorCode).HasColumnName("MAJORCODE").IsOptional().HasMaxLength(500);
            Property(x => x.MajorName).HasColumnName("MAJORNAME").IsOptional().HasMaxLength(500);
            Property(x => x.WorkContent).HasColumnName("WORKCONTENT").IsOptional().HasMaxLength(500);
            Property(x => x.WorkHourDateStart).HasColumnName("WORKHOURDATESTART").IsOptional();
            Property(x => x.WorkHourDateEnd).HasColumnName("WORKHOURDATEEND").IsOptional();
            Property(x => x.NormalValue).HasColumnName("NORMALVALUE").IsOptional().HasPrecision(18,2);
            Property(x => x.AdditionalValue).HasColumnName("ADDITIONALVALUE").IsOptional().HasPrecision(18,2);
            Property(x => x.WorkHourValue).HasColumnName("WORKHOURVALUE").IsOptional().HasPrecision(18,2);
            Property(x => x.UserName).HasColumnName("USERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.UserDeptID).HasColumnName("USERDEPTID").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectCode).HasColumnName("PROJECTCODE").IsOptional().HasMaxLength(500);
            Property(x => x.ProjectName).HasColumnName("PROJECTNAME").IsOptional().HasMaxLength(500);
            Property(x => x.ProjectDept).HasColumnName("PROJECTDEPT").IsOptional().HasMaxLength(500);
            Property(x => x.ProjectDeptName).HasColumnName("PROJECTDEPTNAME").IsOptional().HasMaxLength(500);
            Property(x => x.ProjectChargerUser).HasColumnName("PROJECTCHARGERUSER").IsOptional().HasMaxLength(500);
            Property(x => x.ProjectChargerUserName).HasColumnName("PROJECTCHARGERUSERNAME").IsOptional().HasMaxLength(500);
            Property(x => x.SubProjectName).HasColumnName("SUBPROJECTNAME").IsOptional().HasMaxLength(500);
            Property(x => x.TaskWorkCode).HasColumnName("TASKWORKCODE").IsOptional().HasMaxLength(500);
            Property(x => x.TaskWorkName).HasColumnName("TASKWORKNAME").IsOptional().HasMaxLength(500);
        }
    }

    // T_AttendanceBusinessApply
    internal partial class T_AttendanceBusinessApplyConfiguration : EntityTypeConfiguration<T_AttendanceBusinessApply>
    {
        public T_AttendanceBusinessApplyConfiguration()
        {
            ToTable("T_ATTENDANCEBUSINESSAPPLY");
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
            Property(x => x.ApplyUserDept).HasColumnName("APPLYUSERDEPT").IsOptional().HasMaxLength(200);
            Property(x => x.ApplyUserDeptName).HasColumnName("APPLYUSERDEPTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectInfo).HasColumnName("PROJECTINFO").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectInfoName).HasColumnName("PROJECTINFONAME").IsOptional().HasMaxLength(200);
            Property(x => x.Destination).HasColumnName("DESTINATION").IsOptional().HasMaxLength(200);
            Property(x => x.Partners).HasColumnName("PARTNERS").IsOptional().HasMaxLength(200);
            Property(x => x.PartnersName).HasColumnName("PARTNERSNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Reason).HasColumnName("REASON").IsOptional().HasMaxLength(500);
            Property(x => x.Transportation).HasColumnName("TRANSPORTATION").IsOptional().HasMaxLength(200);
            Property(x => x.Attachments).HasColumnName("ATTACHMENTS").IsOptional().HasMaxLength(500);
            Property(x => x.Remark).HasColumnName("REMARK").IsOptional().HasMaxLength(500);
            Property(x => x.SerialNumber).HasColumnName("SERIALNUMBER").IsOptional().HasMaxLength(200);
            Property(x => x.TravelDays).HasColumnName("TRAVELDAYS").IsOptional();
            Property(x => x.StartDate).HasColumnName("STARTDATE").IsOptional();
            Property(x => x.StartFlag).HasColumnName("STARTFLAG").IsOptional().HasMaxLength(200);
            Property(x => x.EndDate).HasColumnName("ENDDATE").IsOptional();
            Property(x => x.EndFlag).HasColumnName("ENDFLAG").IsOptional().HasMaxLength(200);
            Property(x => x.GSLDYJ).HasColumnName("GSLDYJ").IsOptional();
        }
    }

    // T_AttendanceLeaveApply
    internal partial class T_AttendanceLeaveApplyConfiguration : EntityTypeConfiguration<T_AttendanceLeaveApply>
    {
        public T_AttendanceLeaveApplyConfiguration()
        {
            ToTable("T_ATTENDANCELEAVEAPPLY");
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
            Property(x => x.ApplyDept).HasColumnName("APPLYDEPT").IsOptional().HasMaxLength(200);
            Property(x => x.ApplyDeptName).HasColumnName("APPLYDEPTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.StartDate).HasColumnName("STARTDATE").IsOptional();
            Property(x => x.StartFlag).HasColumnName("STARTFLAG").IsOptional().HasMaxLength(200);
            Property(x => x.EndDate).HasColumnName("ENDDATE").IsOptional();
            Property(x => x.EndFlag).HasColumnName("ENDFLAG").IsOptional().HasMaxLength(200);
            Property(x => x.LeaveType).HasColumnName("LEAVETYPE").IsOptional().HasMaxLength(200);
            Property(x => x.LeaveReason).HasColumnName("LEAVEREASON").IsOptional().HasMaxLength(500);
            Property(x => x.DeptSign).HasColumnName("DEPTSIGN").IsOptional();
            Property(x => x.CompanySign).HasColumnName("COMPANYSIGN").IsOptional();
            Property(x => x.ApplyDays).HasColumnName("APPLYDAYS").IsOptional().HasMaxLength(200);
            Property(x => x.ManagementSign).HasColumnName("MANAGEMENTSIGN").IsOptional();
        }
    }

    // T_C_QualificationApply
    internal partial class T_C_QualificationApplyConfiguration : EntityTypeConfiguration<T_C_QualificationApply>
    {
        public T_C_QualificationApplyConfiguration()
        {
            ToTable("T_C_QUALIFICATIONAPPLY");
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
            Property(x => x.Borrower).HasColumnName("BORROWER").IsOptional().HasMaxLength(200);
            Property(x => x.BorrowerName).HasColumnName("BORROWERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.BorrowDate).HasColumnName("BORROWDATE").IsOptional();
            Property(x => x.Purpose).HasColumnName("PURPOSE").IsOptional().HasMaxLength(500);
            Property(x => x.CertificateType).HasColumnName("CERTIFICATETYPE").IsOptional().HasMaxLength(200);
            Property(x => x.BorrowerDept).HasColumnName("BORROWERDEPT").IsOptional().HasMaxLength(200);
            Property(x => x.BorrowerDeptName).HasColumnName("BORROWERDEPTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.DeptSign).HasColumnName("DEPTSIGN").IsOptional();
            Property(x => x.LeaderSign).HasColumnName("LEADERSIGN").IsOptional();
            Property(x => x.BorrowState).HasColumnName("BORROWSTATE").IsOptional().HasMaxLength(200);
            Property(x => x.ReturnDate).HasColumnName("RETURNDATE").IsOptional();
            Property(x => x.PlanReturnDate).HasColumnName("PLANRETURNDATE").IsOptional();
            Property(x => x.BorrowerTel).HasColumnName("BORROWERTEL").IsOptional().HasMaxLength(200);
            Property(x => x.Senior).HasColumnName("SENIOR").IsOptional();
        }
    }

    // T_C_QualificationApply_BorrowInfo
    internal partial class T_C_QualificationApply_BorrowInfoConfiguration : EntityTypeConfiguration<T_C_QualificationApply_BorrowInfo>
    {
        public T_C_QualificationApply_BorrowInfoConfiguration()
        {
            ToTable("T_C_QUALIFICATIONAPPLY_BORROWINFO");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.T_C_QualificationApplyID).HasColumnName("T_C_QUALIFICATIONAPPLYID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(200);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(200);
            Property(x => x.HoldDept).HasColumnName("HOLDDEPT").IsOptional().HasMaxLength(200);
            Property(x => x.HoldDeptName).HasColumnName("HOLDDEPTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Keeper).HasColumnName("KEEPER").IsOptional().HasMaxLength(200);
            Property(x => x.KeeperName).HasColumnName("KEEPERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Type).HasColumnName("TYPE").IsOptional().HasMaxLength(200);
            Property(x => x.BorrowID).HasColumnName("BORROWID").IsOptional().HasMaxLength(200);

            // Foreign keys
            HasOptional(a => a.T_C_QualificationApply).WithMany(b => b.T_C_QualificationApply_BorrowInfo).HasForeignKey(c => c.T_C_QualificationApplyID); // FK_T_C_QualificationApply_BorrowInfo_T_C_QualificationApply
        }
    }

    // T_D_UserAptitudeApply
    internal partial class T_D_UserAptitudeApplyConfiguration : EntityTypeConfiguration<T_D_UserAptitudeApply>
    {
        public T_D_UserAptitudeApplyConfiguration()
        {
            ToTable("T_D_USERAPTITUDEAPPLY");
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
            Property(x => x.ApplyDept).HasColumnName("APPLYDEPT").IsOptional().HasMaxLength(200);
            Property(x => x.ApplyDeptName).HasColumnName("APPLYDEPTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ApplyDate).HasColumnName("APPLYDATE").IsOptional();
            Property(x => x.ApplyReason).HasColumnName("APPLYREASON").IsOptional().HasMaxLength(500);
            Property(x => x.LeaderSign).HasColumnName("LEADERSIGN").IsOptional();
            Property(x => x.ProjectID).HasColumnName("PROJECTID").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectIDName).HasColumnName("PROJECTIDNAME").IsOptional().HasMaxLength(2000);
            Property(x => x.ApplyInfo).HasColumnName("APPLYINFO").IsOptional();
        }
    }

    // T_D_UserAptitudeApply_ApplyInfo
    internal partial class T_D_UserAptitudeApply_ApplyInfoConfiguration : EntityTypeConfiguration<T_D_UserAptitudeApply_ApplyInfo>
    {
        public T_D_UserAptitudeApply_ApplyInfoConfiguration()
        {
            ToTable("T_D_USERAPTITUDEAPPLY_APPLYINFO");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.T_D_UserAptitudeApplyID).HasColumnName("T_D_USERAPTITUDEAPPLYID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.UserID).HasColumnName("USERID").IsOptional().HasMaxLength(200);
            Property(x => x.UserIDName).HasColumnName("USERIDNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Major).HasColumnName("MAJOR").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectClass).HasColumnName("PROJECTCLASS").IsOptional().HasMaxLength(200);
            Property(x => x.Position).HasColumnName("POSITION").IsOptional().HasMaxLength(200);
            Property(x => x.AptitudeLevel).HasColumnName("APTITUDELEVEL").IsOptional().HasMaxLength(200);

            // Foreign keys
            HasOptional(a => a.T_D_UserAptitudeApply).WithMany(b => b.T_D_UserAptitudeApply_ApplyInfo).HasForeignKey(c => c.T_D_UserAptitudeApplyID); // FK_T_D_UserAptitudeApply_ApplyInfo_T_D_UserAptitudeApply
        }
    }

    // T_Employee
    internal partial class T_EmployeeConfiguration : EntityTypeConfiguration<T_Employee>
    {
        public T_EmployeeConfiguration()
        {
            ToTable("T_EMPLOYEE");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(200);
            Property(x => x.OldName).HasColumnName("OLDNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Portrait).HasColumnName("PORTRAIT").IsOptional().HasMaxLength(2147483647);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(200);
            Property(x => x.Nation).HasColumnName("NATION").IsOptional().HasMaxLength(200);
            Property(x => x.Sex).HasColumnName("SEX").IsOptional().HasMaxLength(200);
            Property(x => x.MobilePhone).HasColumnName("MOBILEPHONE").IsOptional().HasMaxLength(200);
            Property(x => x.OfficePhone).HasColumnName("OFFICEPHONE").IsOptional().HasMaxLength(200);
            Property(x => x.HomePhone).HasColumnName("HOMEPHONE").IsOptional().HasMaxLength(200);
            Property(x => x.Email).HasColumnName("EMAIL").IsOptional().HasMaxLength(200);
            Property(x => x.Address).HasColumnName("ADDRESS").IsOptional().HasMaxLength(200);
            Property(x => x.Political).HasColumnName("POLITICAL").IsOptional().HasMaxLength(200);
            Property(x => x.HealthStatus).HasColumnName("HEALTHSTATUS").IsOptional().HasMaxLength(200);
            Property(x => x.Birthday).HasColumnName("BIRTHDAY").IsOptional();
            Property(x => x.IdentityCardCode).HasColumnName("IDENTITYCARDCODE").IsOptional().HasMaxLength(200);
            Property(x => x.NativePlace).HasColumnName("NATIVEPLACE").IsOptional().HasMaxLength(200);
            Property(x => x.MaritalStatus).HasColumnName("MARITALSTATUS").IsOptional().HasMaxLength(200);
            Property(x => x.LoverName).HasColumnName("LOVERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.LoverUnit).HasColumnName("LOVERUNIT").IsOptional().HasMaxLength(200);
            Property(x => x.FirstForeignLanguage).HasColumnName("FIRSTFOREIGNLANGUAGE").IsOptional().HasMaxLength(200);
            Property(x => x.TwoForeignLanguage).HasColumnName("TWOFOREIGNLANGUAGE").IsOptional().HasMaxLength(200);
            Property(x => x.FirstForeignLanguageLevel).HasColumnName("FIRSTFOREIGNLANGUAGELEVEL").IsOptional().HasMaxLength(200);
            Property(x => x.TwoForeignLanguageLevel).HasColumnName("TWOFOREIGNLANGUAGELEVEL").IsOptional().HasMaxLength(200);
            Property(x => x.JoinWorkDate).HasColumnName("JOINWORKDATE").IsOptional();
            Property(x => x.JoinCompanyDate).HasColumnName("JOINCOMPANYDATE").IsOptional();
            Property(x => x.EmploymentWay).HasColumnName("EMPLOYMENTWAY").IsOptional().HasMaxLength(200);
            Property(x => x.EmployeeSource).HasColumnName("EMPLOYEESOURCE").IsOptional().HasMaxLength(200);
            Property(x => x.EmployeeBigType).HasColumnName("EMPLOYEEBIGTYPE").IsOptional().HasMaxLength(200);
            Property(x => x.EmployeeSmallType).HasColumnName("EMPLOYEESMALLTYPE").IsOptional().HasMaxLength(200);
            Property(x => x.Post).HasColumnName("POST").IsOptional().HasMaxLength(200);
            Property(x => x.PostLevel).HasColumnName("POSTLEVEL").IsOptional().HasMaxLength(200);
            Property(x => x.PositionalTitles).HasColumnName("POSITIONALTITLES").IsOptional().HasMaxLength(200);
            Property(x => x.Educational).HasColumnName("EDUCATIONAL").IsOptional().HasMaxLength(200);
            Property(x => x.EngageMajor).HasColumnName("ENGAGEMAJOR").IsOptional().HasMaxLength(200);
            Property(x => x.EducationalMajor).HasColumnName("EDUCATIONALMAJOR").IsOptional().HasMaxLength(200);
            Property(x => x.ContractType).HasColumnName("CONTRACTTYPE").IsOptional().HasMaxLength(200);
            Property(x => x.DeterminePostsDate).HasColumnName("DETERMINEPOSTSDATE").IsOptional();
            Property(x => x.IsPaymentInterval).HasColumnName("ISPAYMENTINTERVAL").IsOptional().HasMaxLength(200);
            Property(x => x.IsPaymentSheBao).HasColumnName("ISPAYMENTSHEBAO").IsOptional().HasMaxLength(200);
            Property(x => x.IsPaymentGongJiJin).HasColumnName("ISPAYMENTGONGJIJIN").IsOptional().HasMaxLength(200);
            Property(x => x.IsPaymentZongHeBaoXian).HasColumnName("ISPAYMENTZONGHEBAOXIAN").IsOptional().HasMaxLength(200);
            Property(x => x.IdentityCardFace).HasColumnName("IDENTITYCARDFACE").IsOptional().HasMaxLength(2147483647);
            Property(x => x.IdentityCardBack).HasColumnName("IDENTITYCARDBACK").IsOptional().HasMaxLength(2147483647);
            Property(x => x.SignImage).HasColumnName("SIGNIMAGE").IsOptional().HasMaxLength(2147483647);
            Property(x => x.Remark).HasColumnName("REMARK").IsOptional().HasMaxLength(2000);
            Property(x => x.DeptID).HasColumnName("DEPTID").IsOptional().HasMaxLength(200);
            Property(x => x.DeptName).HasColumnName("DEPTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ParttimeDeptID).HasColumnName("PARTTIMEDEPTID").IsOptional().HasMaxLength(500);
            Property(x => x.ParttimeDeptName).HasColumnName("PARTTIMEDEPTNAME").IsOptional().HasMaxLength(500);
            Property(x => x.EmployeeState).HasColumnName("EMPLOYEESTATE").IsOptional().HasMaxLength(200);
            Property(x => x.IsDeleted).HasColumnName("ISDELETED").IsOptional().HasMaxLength(1);
            Property(x => x.DeleteTime).HasColumnName("DELETETIME").IsOptional();
            Property(x => x.UserID).HasColumnName("USERID").IsOptional().HasMaxLength(50);
            Property(x => x.IsHaveAccount).HasColumnName("ISHAVEACCOUNT").IsOptional().HasMaxLength(200);
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsOptional().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUser).HasColumnName("MODIFYUSER").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyDate).HasColumnName("MODIFYDATE").IsOptional();
            Property(x => x.PostTemplateID).HasColumnName("POSTTEMPLATEID").IsOptional().HasMaxLength(50);
            Property(x => x.JobID).HasColumnName("JOBID").IsOptional().HasMaxLength(50);
            Property(x => x.JobName).HasColumnName("JOBNAME").IsOptional().HasMaxLength(50);
            Property(x => x.DeptIDName).HasColumnName("DEPTIDNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Sign).HasColumnName("SIGN").IsOptional().HasMaxLength(2147483647);
            Property(x => x.Ext1).HasColumnName("EXT1").IsOptional().HasPrecision(18,2);
            Property(x => x.SignDwg).HasColumnName("SIGNDWG").IsOptional().HasMaxLength(2147483647);
            Property(x => x.ParttimeDeptIDName).HasColumnName("PARTTIMEDEPTIDNAME").IsOptional().HasMaxLength(500);
        }
    }

    // T_EmployeeAcademicDegree
    internal partial class T_EmployeeAcademicDegreeConfiguration : EntityTypeConfiguration<T_EmployeeAcademicDegree>
    {
        public T_EmployeeAcademicDegreeConfiguration()
        {
            ToTable("T_EMPLOYEEACADEMICDEGREE");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.ModifyDate).HasColumnName("MODIFYDATE").IsOptional();
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUser).HasColumnName("MODIFYUSER").IsOptional().HasMaxLength(50);
            Property(x => x.OrgID).HasColumnName("ORGID").IsOptional().HasMaxLength(50);
            Property(x => x.PrjID).HasColumnName("PRJID").IsOptional().HasMaxLength(50);
            Property(x => x.FlowPhase).HasColumnName("FLOWPHASE").IsOptional().HasMaxLength(50);
            Property(x => x.StepName).HasColumnName("STEPNAME").IsOptional().HasMaxLength(500);
            Property(x => x.EmployeeID).HasColumnName("EMPLOYEEID").IsOptional().HasMaxLength(50);
            Property(x => x.EntrancelDate).HasColumnName("ENTRANCELDATE").IsOptional();
            Property(x => x.GraduationDate).HasColumnName("GRADUATIONDATE").IsOptional();
            Property(x => x.School).HasColumnName("SCHOOL").IsOptional().HasMaxLength(50);
            Property(x => x.FirstProfession).HasColumnName("FIRSTPROFESSION").IsOptional().HasMaxLength(50);
            Property(x => x.TwoProfession).HasColumnName("TWOPROFESSION").IsOptional().HasMaxLength(50);
            Property(x => x.SchoolingLength).HasColumnName("SCHOOLINGLENGTH").IsOptional().HasMaxLength(50);
            Property(x => x.SchoolShape).HasColumnName("SCHOOLSHAPE").IsOptional().HasMaxLength(50);
            Property(x => x.Degree).HasColumnName("DEGREE").IsOptional().HasMaxLength(50);
            Property(x => x.Education).HasColumnName("EDUCATION").IsOptional().HasMaxLength(50);
            Property(x => x.DegreeGiveDate).HasColumnName("DEGREEGIVEDATE").IsOptional();
            Property(x => x.DegreeGiveCountry).HasColumnName("DEGREEGIVECOUNTRY").IsOptional().HasMaxLength(50);
            Property(x => x.DegreeAttachment).HasColumnName("DEGREEATTACHMENT").IsOptional().HasMaxLength(500);
            Property(x => x.EducationAttachment).HasColumnName("EDUCATIONATTACHMENT").IsOptional().HasMaxLength(500);
        }
    }

    // T_EmployeeAcademicTitle
    internal partial class T_EmployeeAcademicTitleConfiguration : EntityTypeConfiguration<T_EmployeeAcademicTitle>
    {
        public T_EmployeeAcademicTitleConfiguration()
        {
            ToTable("T_EMPLOYEEACADEMICTITLE");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.ModifyDate).HasColumnName("MODIFYDATE").IsOptional();
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUser).HasColumnName("MODIFYUSER").IsOptional().HasMaxLength(50);
            Property(x => x.OrgID).HasColumnName("ORGID").IsOptional().HasMaxLength(50);
            Property(x => x.PrjID).HasColumnName("PRJID").IsOptional().HasMaxLength(50);
            Property(x => x.FlowPhase).HasColumnName("FLOWPHASE").IsOptional().HasMaxLength(50);
            Property(x => x.StepName).HasColumnName("STEPNAME").IsOptional().HasMaxLength(500);
            Property(x => x.Level).HasColumnName("LEVEL").IsOptional().HasMaxLength(50);
            Property(x => x.Title).HasColumnName("TITLE").IsOptional().HasMaxLength(50);
            Property(x => x.Major).HasColumnName("MAJOR").IsOptional().HasMaxLength(50);
            Property(x => x.AuditDept).HasColumnName("AUDITDEPT").IsOptional().HasMaxLength(50);
            Property(x => x.CertificateNumber).HasColumnName("CERTIFICATENUMBER").IsOptional().HasMaxLength(50);
            Property(x => x.IssueDate).HasColumnName("ISSUEDATE").IsOptional();
            Property(x => x.EmployDate).HasColumnName("EMPLOYDATE").IsOptional();
            Property(x => x.Certificate).HasColumnName("CERTIFICATE").IsOptional().HasMaxLength(500);
            Property(x => x.Remark).HasColumnName("REMARK").IsOptional();
            Property(x => x.EmployeeID).HasColumnName("EMPLOYEEID").IsOptional().HasMaxLength(50);
        }
    }

    // T_EmployeeBaseSet
    internal partial class T_EmployeeBaseSetConfiguration : EntityTypeConfiguration<T_EmployeeBaseSet>
    {
        public T_EmployeeBaseSetConfiguration()
        {
            ToTable("T_EMPLOYEEBASESET");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.ModifyDate).HasColumnName("MODIFYDATE").IsOptional();
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUser).HasColumnName("MODIFYUSER").IsOptional().HasMaxLength(50);
            Property(x => x.OrgID).HasColumnName("ORGID").IsOptional().HasMaxLength(50);
            Property(x => x.PrjID).HasColumnName("PRJID").IsOptional().HasMaxLength(50);
            Property(x => x.FlowPhase).HasColumnName("FLOWPHASE").IsOptional().HasMaxLength(50);
            Property(x => x.StepName).HasColumnName("STEPNAME").IsOptional().HasMaxLength(500);
            Property(x => x.Title).HasColumnName("TITLE").IsOptional().HasMaxLength(50);
            Property(x => x.Url).HasColumnName("URL").IsOptional().HasMaxLength(200);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(50);
            Property(x => x.FilterField).HasColumnName("FILTERFIELD").IsOptional().HasMaxLength(50);
            Property(x => x.EmploymentWay).HasColumnName("EMPLOYMENTWAY").IsOptional().HasMaxLength(50);
        }
    }

    // T_EmployeeContract
    internal partial class T_EmployeeContractConfiguration : EntityTypeConfiguration<T_EmployeeContract>
    {
        public T_EmployeeContractConfiguration()
        {
            ToTable("T_EMPLOYEECONTRACT");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.ModifyDate).HasColumnName("MODIFYDATE").IsOptional();
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUser).HasColumnName("MODIFYUSER").IsOptional().HasMaxLength(50);
            Property(x => x.OrgID).HasColumnName("ORGID").IsOptional().HasMaxLength(50);
            Property(x => x.PrjID).HasColumnName("PRJID").IsOptional().HasMaxLength(50);
            Property(x => x.FlowPhase).HasColumnName("FLOWPHASE").IsOptional().HasMaxLength(50);
            Property(x => x.StepName).HasColumnName("STEPNAME").IsOptional().HasMaxLength(500);
            Property(x => x.EmployeeID).HasColumnName("EMPLOYEEID").IsOptional().HasMaxLength(50);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(50);
            Property(x => x.EmployeeName).HasColumnName("EMPLOYEENAME").IsOptional().HasMaxLength(50);
            Property(x => x.ContractCategory).HasColumnName("CONTRACTCATEGORY").IsOptional().HasMaxLength(50);
            Property(x => x.ContractShape).HasColumnName("CONTRACTSHAPE").IsOptional().HasMaxLength(50);
            Property(x => x.ContractBody).HasColumnName("CONTRACTBODY").IsOptional().HasMaxLength(200);
            Property(x => x.ContractStartDate).HasColumnName("CONTRACTSTARTDATE").IsOptional();
            Property(x => x.ContractEndDate).HasColumnName("CONTRACTENDDATE").IsOptional();
            Property(x => x.PeriodStartDate).HasColumnName("PERIODSTARTDATE").IsOptional();
            Property(x => x.PeriodEndDate).HasColumnName("PERIODENDDATE").IsOptional();
            Property(x => x.PracticeEndDate).HasColumnName("PRACTICEENDDATE").IsOptional();
            Property(x => x.ContractPeriod).HasColumnName("CONTRACTPERIOD").IsOptional().HasMaxLength(50);
            Property(x => x.PostDate).HasColumnName("POSTDATE").IsOptional();
            Property(x => x.GongJiJinBasePay).HasColumnName("GONGJIJINBASEPAY").IsOptional().HasMaxLength(50);
            Property(x => x.ContractAttachment).HasColumnName("CONTRACTATTACHMENT").IsOptional().HasMaxLength(500);
            Property(x => x.IsConfidentialityAgreement).HasColumnName("ISCONFIDENTIALITYAGREEMENT").IsOptional().HasMaxLength(50);
            Property(x => x.ConfidentialityAgreementStart).HasColumnName("CONFIDENTIALITYAGREEMENTSTART").IsOptional();
            Property(x => x.ConfidentialityAgreementEnd).HasColumnName("CONFIDENTIALITYAGREEMENTEND").IsOptional();
            Property(x => x.ConfidentialityAttachment).HasColumnName("CONFIDENTIALITYATTACHMENT").IsOptional().HasMaxLength(500);
            Property(x => x.TrainAgreementStartDate).HasColumnName("TRAINAGREEMENTSTARTDATE").IsOptional();
            Property(x => x.TrainAgreementEndDate).HasColumnName("TRAINAGREEMENTENDDATE").IsOptional();
            Property(x => x.TrainAttachment).HasColumnName("TRAINATTACHMENT").IsOptional().HasMaxLength(500);
            Property(x => x.StockAgreementStartDate).HasColumnName("STOCKAGREEMENTSTARTDATE").IsOptional();
            Property(x => x.StockAgreementEndDate).HasColumnName("STOCKAGREEMENTENDDATE").IsOptional();
            Property(x => x.StockAttachment).HasColumnName("STOCKATTACHMENT").IsOptional().HasMaxLength(500);
            Property(x => x.Remark).HasColumnName("REMARK").IsOptional();
        }
    }

    // T_EmployeeHonour
    internal partial class T_EmployeeHonourConfiguration : EntityTypeConfiguration<T_EmployeeHonour>
    {
        public T_EmployeeHonourConfiguration()
        {
            ToTable("T_EMPLOYEEHONOUR");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.ModifyDate).HasColumnName("MODIFYDATE").IsOptional();
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUser).HasColumnName("MODIFYUSER").IsOptional().HasMaxLength(50);
            Property(x => x.OrgID).HasColumnName("ORGID").IsOptional().HasMaxLength(50);
            Property(x => x.PrjID).HasColumnName("PRJID").IsOptional().HasMaxLength(50);
            Property(x => x.FlowPhase).HasColumnName("FLOWPHASE").IsOptional().HasMaxLength(50);
            Property(x => x.StepName).HasColumnName("STEPNAME").IsOptional().HasMaxLength(500);
            Property(x => x.EmployeeID).HasColumnName("EMPLOYEEID").IsOptional().HasMaxLength(50);
            Property(x => x.AwardName).HasColumnName("AWARDNAME").IsOptional().HasMaxLength(200);
            Property(x => x.AwardYear).HasColumnName("AWARDYEAR").IsOptional().HasMaxLength(50);
            Property(x => x.CertificationDate).HasColumnName("CERTIFICATIONDATE").IsOptional();
            Property(x => x.CertificationUnit).HasColumnName("CERTIFICATIONUNIT").IsOptional().HasMaxLength(200);
            Property(x => x.Remark).HasColumnName("REMARK").IsOptional();
            Property(x => x.Attachment).HasColumnName("ATTACHMENT").IsOptional().HasMaxLength(500);
        }
    }

    // T_EmployeeJob
    internal partial class T_EmployeeJobConfiguration : EntityTypeConfiguration<T_EmployeeJob>
    {
        public T_EmployeeJobConfiguration()
        {
            ToTable("T_EMPLOYEEJOB");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.ModifyDate).HasColumnName("MODIFYDATE").IsOptional();
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUser).HasColumnName("MODIFYUSER").IsOptional().HasMaxLength(50);
            Property(x => x.OrgID).HasColumnName("ORGID").IsOptional().HasMaxLength(50);
            Property(x => x.PrjID).HasColumnName("PRJID").IsOptional().HasMaxLength(50);
            Property(x => x.FlowPhase).HasColumnName("FLOWPHASE").IsOptional().HasMaxLength(50);
            Property(x => x.StepName).HasColumnName("STEPNAME").IsOptional().HasMaxLength(500);
            Property(x => x.EmployeeID).HasColumnName("EMPLOYEEID").IsOptional().HasMaxLength(50);
            Property(x => x.IsDeleted).HasColumnName("ISDELETED").IsOptional().HasMaxLength(50);
            Property(x => x.EmployeeCategory).HasColumnName("EMPLOYEECATEGORY").IsOptional().HasMaxLength(50);
            Property(x => x.JobCategory).HasColumnName("JOBCATEGORY").IsOptional().HasMaxLength(50);
            Property(x => x.Clique).HasColumnName("CLIQUE").IsOptional().HasMaxLength(50);
            Property(x => x.SubCompany).HasColumnName("SUBCOMPANY").IsOptional().HasMaxLength(50);
            Property(x => x.DeptID).HasColumnName("DEPTID").IsOptional().HasMaxLength(50);
            Property(x => x.DeptIDName).HasColumnName("DEPTIDNAME").IsOptional().HasMaxLength(50);
            Property(x => x.JobID).HasColumnName("JOBID").IsOptional().HasMaxLength(50);
            Property(x => x.IsMain).HasColumnName("ISMAIN").IsOptional().HasMaxLength(50);
            Property(x => x.Major).HasColumnName("MAJOR").IsOptional().HasMaxLength(50);
            Property(x => x.EmployDate).HasColumnName("EMPLOYDATE").IsOptional();
            Property(x => x.JobAgreeCode).HasColumnName("JOBAGREECODE").IsOptional().HasMaxLength(200);
            Property(x => x.EmployAgreeCode).HasColumnName("EMPLOYAGREECODE").IsOptional().HasMaxLength(200);
            Property(x => x.EmployEndDate).HasColumnName("EMPLOYENDDATE").IsOptional();
            Property(x => x.ClearEmployDate).HasColumnName("CLEAREMPLOYDATE").IsOptional();
            Property(x => x.Attachment).HasColumnName("ATTACHMENT").IsOptional().HasMaxLength(500);
            Property(x => x.Remark).HasColumnName("REMARK").IsOptional();
            Property(x => x.JobName).HasColumnName("JOBNAME").IsOptional().HasMaxLength(50);
        }
    }

    // T_EmployeeJobChange
    internal partial class T_EmployeeJobChangeConfiguration : EntityTypeConfiguration<T_EmployeeJobChange>
    {
        public T_EmployeeJobChangeConfiguration()
        {
            ToTable("T_EMPLOYEEJOBCHANGE");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.EmployeeID).HasColumnName("EMPLOYEEID").IsOptional().HasMaxLength(50);
            Property(x => x.EmployeeCode).HasColumnName("EMPLOYEECODE").IsOptional().HasMaxLength(50);
            Property(x => x.EmployeeName).HasColumnName("EMPLOYEENAME").IsOptional().HasMaxLength(50);
            Property(x => x.ChangeDate).HasColumnName("CHANGEDATE").IsOptional();
            Property(x => x.ChangeReason).HasColumnName("CHANGEREASON").IsOptional().HasMaxLength(2000);
            Property(x => x.Remark).HasColumnName("REMARK").IsOptional().HasMaxLength(2000);
            Property(x => x.CurrentJobList).HasColumnName("CURRENTJOBLIST").IsOptional();
            Property(x => x.ChangeJobList).HasColumnName("CHANGEJOBLIST").IsOptional();
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsOptional().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUser).HasColumnName("MODIFYUSER").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyDate).HasColumnName("MODIFYDATE").IsOptional();
        }
    }

    // T_EmployeeJobChange_ChangeJobList
    internal partial class T_EmployeeJobChange_ChangeJobListConfiguration : EntityTypeConfiguration<T_EmployeeJobChange_ChangeJobList>
    {
        public T_EmployeeJobChange_ChangeJobListConfiguration()
        {
            ToTable("T_EMPLOYEEJOBCHANGE_CHANGEJOBLIST");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.T_EmployeeJobChangeID).HasColumnName("T_EMPLOYEEJOBCHANGEID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.EmployeeCategory).HasColumnName("EMPLOYEECATEGORY").IsOptional().HasMaxLength(200);
            Property(x => x.JobCategory).HasColumnName("JOBCATEGORY").IsOptional().HasMaxLength(200);
            Property(x => x.Clique).HasColumnName("CLIQUE").IsOptional().HasMaxLength(200);
            Property(x => x.SubCompany).HasColumnName("SUBCOMPANY").IsOptional().HasMaxLength(200);
            Property(x => x.DeptID).HasColumnName("DEPTID").IsOptional().HasMaxLength(200);
            Property(x => x.DeptIDName).HasColumnName("DEPTIDNAME").IsOptional().HasMaxLength(200);
            Property(x => x.IsMain).HasColumnName("ISMAIN").IsOptional().HasMaxLength(200);
            Property(x => x.JobName).HasColumnName("JOBNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Major).HasColumnName("MAJOR").IsOptional().HasMaxLength(200);
            Property(x => x.EmployDate).HasColumnName("EMPLOYDATE").IsOptional();
            Property(x => x.JobAgreeCode).HasColumnName("JOBAGREECODE").IsOptional().HasMaxLength(200);
            Property(x => x.EmployAgreeCode).HasColumnName("EMPLOYAGREECODE").IsOptional().HasMaxLength(200);
            Property(x => x.JobID).HasColumnName("JOBID").IsOptional().HasMaxLength(200);
            Property(x => x.IsDeleted).HasColumnName("ISDELETED").IsOptional().HasMaxLength(200);

            // Foreign keys
            HasOptional(a => a.T_EmployeeJobChange).WithMany(b => b.T_EmployeeJobChange_ChangeJobList).HasForeignKey(c => c.T_EmployeeJobChangeID); // FK_T_EmployeeJobChange_ChangeJobList_T_EmployeeJobChange
        }
    }

    // T_EmployeeJobChange_CurrentJobList
    internal partial class T_EmployeeJobChange_CurrentJobListConfiguration : EntityTypeConfiguration<T_EmployeeJobChange_CurrentJobList>
    {
        public T_EmployeeJobChange_CurrentJobListConfiguration()
        {
            ToTable("T_EMPLOYEEJOBCHANGE_CURRENTJOBLIST");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.T_EmployeeJobChangeID).HasColumnName("T_EMPLOYEEJOBCHANGEID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.EmployeeCategory).HasColumnName("EMPLOYEECATEGORY").IsOptional().HasMaxLength(200);
            Property(x => x.JobCategory).HasColumnName("JOBCATEGORY").IsOptional().HasMaxLength(200);
            Property(x => x.Clique).HasColumnName("CLIQUE").IsOptional().HasMaxLength(200);
            Property(x => x.SubCompany).HasColumnName("SUBCOMPANY").IsOptional().HasMaxLength(200);
            Property(x => x.DeptID).HasColumnName("DEPTID").IsOptional().HasMaxLength(200);
            Property(x => x.DeptIDName).HasColumnName("DEPTIDNAME").IsOptional().HasMaxLength(200);
            Property(x => x.IsMain).HasColumnName("ISMAIN").IsOptional().HasMaxLength(200);
            Property(x => x.JobName).HasColumnName("JOBNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Major).HasColumnName("MAJOR").IsOptional().HasMaxLength(200);
            Property(x => x.EmployDate).HasColumnName("EMPLOYDATE").IsOptional();
            Property(x => x.JobAgreeCode).HasColumnName("JOBAGREECODE").IsOptional().HasMaxLength(200);
            Property(x => x.EmployAgreeCode).HasColumnName("EMPLOYAGREECODE").IsOptional().HasMaxLength(200);
            Property(x => x.NeedDelete).HasColumnName("NEEDDELETE").IsOptional().HasMaxLength(200);

            // Foreign keys
            HasOptional(a => a.T_EmployeeJobChange).WithMany(b => b.T_EmployeeJobChange_CurrentJobList).HasForeignKey(c => c.T_EmployeeJobChangeID); // FK_T_EmployeeJobChange_CurrentJobList_T_EmployeeJobChange
        }
    }

    // T_EmployeePersonalRecords
    internal partial class T_EmployeePersonalRecordsConfiguration : EntityTypeConfiguration<T_EmployeePersonalRecords>
    {
        public T_EmployeePersonalRecordsConfiguration()
        {
            ToTable("T_EMPLOYEEPERSONALRECORDS");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.ModifyDate).HasColumnName("MODIFYDATE").IsOptional();
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUser).HasColumnName("MODIFYUSER").IsOptional().HasMaxLength(50);
            Property(x => x.OrgID).HasColumnName("ORGID").IsOptional().HasMaxLength(50);
            Property(x => x.PrjID).HasColumnName("PRJID").IsOptional().HasMaxLength(50);
            Property(x => x.FlowPhase).HasColumnName("FLOWPHASE").IsOptional().HasMaxLength(50);
            Property(x => x.StepName).HasColumnName("STEPNAME").IsOptional().HasMaxLength(500);
            Property(x => x.EmployeeID).HasColumnName("EMPLOYEEID").IsOptional().HasMaxLength(50);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(50);
            Property(x => x.Type).HasColumnName("TYPE").IsOptional().HasMaxLength(50);
            Property(x => x.KeepUnit).HasColumnName("KEEPUNIT").IsOptional().HasMaxLength(200);
            Property(x => x.SourceUnit).HasColumnName("SOURCEUNIT").IsOptional().HasMaxLength(200);
            Property(x => x.ExitUnit).HasColumnName("EXITUNIT").IsOptional().HasMaxLength(200);
            Property(x => x.ReportCardSubDate).HasColumnName("REPORTCARDSUBDATE").IsOptional();
            Property(x => x.EnterDate).HasColumnName("ENTERDATE").IsOptional();
            Property(x => x.ExitDate).HasColumnName("EXITDATE").IsOptional();
            Property(x => x.ResidentAccountsType).HasColumnName("RESIDENTACCOUNTSTYPE").IsOptional().HasMaxLength(50);
            Property(x => x.ResidentAccountsStreet).HasColumnName("RESIDENTACCOUNTSSTREET").IsOptional().HasMaxLength(200);
        }
    }

    // T_EmployeeQualification
    internal partial class T_EmployeeQualificationConfiguration : EntityTypeConfiguration<T_EmployeeQualification>
    {
        public T_EmployeeQualificationConfiguration()
        {
            ToTable("T_EMPLOYEEQUALIFICATION");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.ModifyDate).HasColumnName("MODIFYDATE").IsOptional();
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUser).HasColumnName("MODIFYUSER").IsOptional().HasMaxLength(50);
            Property(x => x.OrgID).HasColumnName("ORGID").IsOptional().HasMaxLength(50);
            Property(x => x.PrjID).HasColumnName("PRJID").IsOptional().HasMaxLength(50);
            Property(x => x.FlowPhase).HasColumnName("FLOWPHASE").IsOptional().HasMaxLength(50);
            Property(x => x.StepName).HasColumnName("STEPNAME").IsOptional().HasMaxLength(500);
            Property(x => x.RegisterID).HasColumnName("REGISTERID").IsOptional().HasMaxLength(50);
            Property(x => x.RegisterIDName).HasColumnName("REGISTERIDNAME").IsOptional().HasMaxLength(50);
            Property(x => x.RegisteDate).HasColumnName("REGISTEDATE").IsOptional();
            Property(x => x.QualificationName).HasColumnName("QUALIFICATIONNAME").IsOptional().HasMaxLength(200);
            Property(x => x.QualificationCode).HasColumnName("QUALIFICATIONCODE").IsOptional().HasMaxLength(200);
            Property(x => x.FirstMajor).HasColumnName("FIRSTMAJOR").IsOptional().HasMaxLength(50);
            Property(x => x.TwoMajor).HasColumnName("TWOMAJOR").IsOptional().HasMaxLength(50);
            Property(x => x.QualificationIssueDate).HasColumnName("QUALIFICATIONISSUEDATE").IsOptional();
            Property(x => x.QualificationKeeperID).HasColumnName("QUALIFICATIONKEEPERID").IsOptional().HasMaxLength(50);
            Property(x => x.QualificationKeeperIDName).HasColumnName("QUALIFICATIONKEEPERIDNAME").IsOptional().HasMaxLength(50);
            Property(x => x.RegisteCode).HasColumnName("REGISTECODE").IsOptional().HasMaxLength(200);
            Property(x => x.RegisteIssueDate).HasColumnName("REGISTEISSUEDATE").IsOptional();
            Property(x => x.RegistelLoseDate).HasColumnName("REGISTELLOSEDATE").IsOptional();
            Property(x => x.RegisteKeeperID).HasColumnName("REGISTEKEEPERID").IsOptional().HasMaxLength(50);
            Property(x => x.RegisteKeeperIDName).HasColumnName("REGISTEKEEPERIDNAME").IsOptional().HasMaxLength(50);
            Property(x => x.SealCode).HasColumnName("SEALCODE").IsOptional().HasMaxLength(50);
            Property(x => x.SealLoseDate).HasColumnName("SEALLOSEDATE").IsOptional();
            Property(x => x.SealKeeperID).HasColumnName("SEALKEEPERID").IsOptional().HasMaxLength(50);
            Property(x => x.SealKeeperIDName).HasColumnName("SEALKEEPERIDNAME").IsOptional().HasMaxLength(50);
            Property(x => x.ContinueTrainDate).HasColumnName("CONTINUETRAINDATE").IsOptional();
            Property(x => x.ContinueTrainLength).HasColumnName("CONTINUETRAINLENGTH").IsOptional();
            Property(x => x.ContinueTrainCompleteLength).HasColumnName("CONTINUETRAINCOMPLETELENGTH").IsOptional();
            Property(x => x.QualificationAttachment).HasColumnName("QUALIFICATIONATTACHMENT").IsOptional().HasMaxLength(500);
            Property(x => x.RegisteAttachment).HasColumnName("REGISTEATTACHMENT").IsOptional().HasMaxLength(500);
            Property(x => x.EmployeeID).HasColumnName("EMPLOYEEID").IsOptional().HasMaxLength(50);
        }
    }

    // T_EmployeeRetired
    internal partial class T_EmployeeRetiredConfiguration : EntityTypeConfiguration<T_EmployeeRetired>
    {
        public T_EmployeeRetiredConfiguration()
        {
            ToTable("T_EMPLOYEERETIRED");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.ModifyDate).HasColumnName("MODIFYDATE").IsOptional();
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUser).HasColumnName("MODIFYUSER").IsOptional().HasMaxLength(50);
            Property(x => x.OrgID).HasColumnName("ORGID").IsOptional().HasMaxLength(50);
            Property(x => x.PrjID).HasColumnName("PRJID").IsOptional().HasMaxLength(50);
            Property(x => x.FlowPhase).HasColumnName("FLOWPHASE").IsOptional().HasMaxLength(50);
            Property(x => x.StepName).HasColumnName("STEPNAME").IsOptional().HasMaxLength(500);
            Property(x => x.EmployeeCode).HasColumnName("EMPLOYEECODE").IsOptional().HasMaxLength(50);
            Property(x => x.EmployeeName).HasColumnName("EMPLOYEENAME").IsOptional().HasMaxLength(50);
            Property(x => x.RetiredDate).HasColumnName("RETIREDDATE").IsOptional();
            Property(x => x.Type).HasColumnName("TYPE").IsOptional().HasMaxLength(50);
            Property(x => x.Direction).HasColumnName("DIRECTION").IsOptional().HasMaxLength(200);
            Property(x => x.Reason).HasColumnName("REASON").IsOptional();
            Property(x => x.Remark).HasColumnName("REMARK").IsOptional();
            Property(x => x.EmployeeID).HasColumnName("EMPLOYEEID").IsOptional().HasMaxLength(50);
        }
    }

    // T_EmployeeSocialSecurity
    internal partial class T_EmployeeSocialSecurityConfiguration : EntityTypeConfiguration<T_EmployeeSocialSecurity>
    {
        public T_EmployeeSocialSecurityConfiguration()
        {
            ToTable("T_EMPLOYEESOCIALSECURITY");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.ModifyDate).HasColumnName("MODIFYDATE").IsOptional();
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUser).HasColumnName("MODIFYUSER").IsOptional().HasMaxLength(50);
            Property(x => x.OrgID).HasColumnName("ORGID").IsOptional().HasMaxLength(50);
            Property(x => x.PrjID).HasColumnName("PRJID").IsOptional().HasMaxLength(50);
            Property(x => x.FlowPhase).HasColumnName("FLOWPHASE").IsOptional().HasMaxLength(50);
            Property(x => x.StepName).HasColumnName("STEPNAME").IsOptional().HasMaxLength(500);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(50);
            Property(x => x.Relation).HasColumnName("RELATION").IsOptional().HasMaxLength(50);
            Property(x => x.BirthDate).HasColumnName("BIRTHDATE").IsOptional();
            Property(x => x.Sex).HasColumnName("SEX").IsOptional().HasMaxLength(50);
            Property(x => x.WorkUnit).HasColumnName("WORKUNIT").IsOptional().HasMaxLength(50);
            Property(x => x.Job).HasColumnName("JOB").IsOptional().HasMaxLength(50);
            Property(x => x.Phone).HasColumnName("PHONE").IsOptional().HasMaxLength(50);
            Property(x => x.Remark).HasColumnName("REMARK").IsOptional();
            Property(x => x.EmployeeID).HasColumnName("EMPLOYEEID").IsOptional().HasMaxLength(50);
        }
    }

    // T_EmployeeWorkHistory
    internal partial class T_EmployeeWorkHistoryConfiguration : EntityTypeConfiguration<T_EmployeeWorkHistory>
    {
        public T_EmployeeWorkHistoryConfiguration()
        {
            ToTable("T_EMPLOYEEWORKHISTORY");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.ModifyDate).HasColumnName("MODIFYDATE").IsOptional();
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUser).HasColumnName("MODIFYUSER").IsOptional().HasMaxLength(50);
            Property(x => x.OrgID).HasColumnName("ORGID").IsOptional().HasMaxLength(50);
            Property(x => x.PrjID).HasColumnName("PRJID").IsOptional().HasMaxLength(50);
            Property(x => x.FlowPhase).HasColumnName("FLOWPHASE").IsOptional().HasMaxLength(50);
            Property(x => x.StepName).HasColumnName("STEPNAME").IsOptional().HasMaxLength(500);
            Property(x => x.EmployeeID).HasColumnName("EMPLOYEEID").IsOptional().HasMaxLength(50);
            Property(x => x.JoinCompanyDate).HasColumnName("JOINCOMPANYDATE").IsOptional();
            Property(x => x.LeaveCompanyDate).HasColumnName("LEAVECOMPANYDATE").IsOptional();
            Property(x => x.CompanyName).HasColumnName("COMPANYNAME").IsOptional().HasMaxLength(200);
            Property(x => x.DeptAndPost).HasColumnName("DEPTANDPOST").IsOptional().HasMaxLength(200);
            Property(x => x.Description).HasColumnName("DESCRIPTION").IsOptional().HasMaxLength(500);
            Property(x => x.Achievement).HasColumnName("ACHIEVEMENT").IsOptional().HasMaxLength(500);
            Property(x => x.Attachment).HasColumnName("ATTACHMENT").IsOptional().HasMaxLength(500);
            Property(x => x.Remark).HasColumnName("REMARK").IsOptional();
        }
    }

    // T_EmployeeWorkPerformance
    internal partial class T_EmployeeWorkPerformanceConfiguration : EntityTypeConfiguration<T_EmployeeWorkPerformance>
    {
        public T_EmployeeWorkPerformanceConfiguration()
        {
            ToTable("T_EMPLOYEEWORKPERFORMANCE");
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
            Property(x => x.EmployeeID).HasColumnName("EMPLOYEEID").IsOptional().HasMaxLength(50);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(50);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectClass).HasColumnName("PROJECTCLASS").IsOptional().HasMaxLength(50);
            Property(x => x.ProjectLevel).HasColumnName("PROJECTLEVEL").IsOptional().HasMaxLength(50);
            Property(x => x.ProjectState).HasColumnName("PROJECTSTATE").IsOptional().HasMaxLength(200);
            Property(x => x.PlanStartDate).HasColumnName("PLANSTARTDATE").IsOptional();
            Property(x => x.FactFinishDate).HasColumnName("FACTFINISHDATE").IsOptional();
            Property(x => x.ProjectDescription).HasColumnName("PROJECTDESCRIPTION").IsOptional();
            Property(x => x.Remark).HasColumnName("REMARK").IsOptional();
            Property(x => x.Attachment).HasColumnName("ATTACHMENT").IsOptional().HasMaxLength(500);
            Property(x => x.UserID).HasColumnName("USERID").IsOptional().HasMaxLength(200);
            Property(x => x.RelateID).HasColumnName("RELATEID").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectRole).HasColumnName("PROJECTROLE").IsOptional().HasMaxLength(200);
        }
    }

    // T_EmployeeWorkPost
    internal partial class T_EmployeeWorkPostConfiguration : EntityTypeConfiguration<T_EmployeeWorkPost>
    {
        public T_EmployeeWorkPostConfiguration()
        {
            ToTable("T_EMPLOYEEWORKPOST");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.ModifyDate).HasColumnName("MODIFYDATE").IsOptional();
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUser).HasColumnName("MODIFYUSER").IsOptional().HasMaxLength(50);
            Property(x => x.OrgID).HasColumnName("ORGID").IsOptional().HasMaxLength(50);
            Property(x => x.PrjID).HasColumnName("PRJID").IsOptional().HasMaxLength(50);
            Property(x => x.FlowPhase).HasColumnName("FLOWPHASE").IsOptional().HasMaxLength(50);
            Property(x => x.StepName).HasColumnName("STEPNAME").IsOptional().HasMaxLength(500);
            Property(x => x.Post).HasColumnName("POST").IsOptional().HasMaxLength(50);
            Property(x => x.PostLevel).HasColumnName("POSTLEVEL").IsOptional().HasMaxLength(50);
            Property(x => x.EffectiveDate).HasColumnName("EFFECTIVEDATE").IsOptional();
            Property(x => x.EmployeeID).HasColumnName("EMPLOYEEID").IsOptional().HasMaxLength(50);
            Property(x => x.IsTheNewest).HasColumnName("ISTHENEWEST").IsOptional().HasMaxLength(50);
            Property(x => x.PostName).HasColumnName("POSTNAME").IsOptional().HasMaxLength(50);
            Property(x => x.test).HasColumnName("TEST").IsOptional().HasMaxLength(200);
        }
    }

    // V_All_Attendance
    internal partial class V_All_AttendanceConfiguration : EntityTypeConfiguration<V_All_Attendance>
    {
        public V_All_AttendanceConfiguration()
        {
            ToTable("V_ALL_ATTENDANCE");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired();
            Property(x => x.UserName).HasColumnName("USERNAME").IsOptional().HasMaxLength(50);
            Property(x => x.WorkNo).HasColumnName("WORKNO").IsOptional().HasMaxLength(50);
            Property(x => x.OrgName).HasColumnName("ORGNAME").IsOptional().HasMaxLength(200);
            Property(x => x.RQ).HasColumnName("RQ").IsOptional().HasMaxLength(10);
            Property(x => x.SingInTime).HasColumnName("SINGINTIME").IsOptional().HasMaxLength(10);
            Property(x => x.ModifySingInTime).HasColumnName("MODIFYSINGINTIME").IsOptional().HasMaxLength(10);
            Property(x => x.SingOutTime).HasColumnName("SINGOUTTIME").IsOptional().HasMaxLength(10);
            Property(x => x.ModifySingOutTime).HasColumnName("MODIFYSINGOUTTIME").IsOptional().HasMaxLength(10);
            Property(x => x.Status).HasColumnName("STATUS").IsOptional().HasMaxLength(8);
        }
    }

    // V_All_AttendanceInfo
    internal partial class V_All_AttendanceInfoConfiguration : EntityTypeConfiguration<V_All_AttendanceInfo>
    {
        public V_All_AttendanceInfoConfiguration()
        {
            ToTable("V_ALL_ATTENDANCEINFO");
            HasKey(x => new { x.ID, x.IsToday, x.AttendanceState });

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(1);
            Property(x => x.UserID).HasColumnName("USERID").IsOptional().HasMaxLength(50);
            Property(x => x.CheckDate).HasColumnName("CHECKDATE").IsOptional();
            Property(x => x.UserName).HasColumnName("USERNAME").IsOptional().HasMaxLength(50);
            Property(x => x.WorkNo).HasColumnName("WORKNO").IsOptional().HasMaxLength(50);
            Property(x => x.OrgID).HasColumnName("ORGID").IsOptional().HasMaxLength(50);
            Property(x => x.OrgName).HasColumnName("ORGNAME").IsOptional().HasMaxLength(200);
            Property(x => x.AMCheckDate).HasColumnName("AMCHECKDATE").IsOptional();
            Property(x => x.AMModifyCheckDate).HasColumnName("AMMODIFYCHECKDATE").IsOptional();
            Property(x => x.PMCheckDate).HasColumnName("PMCHECKDATE").IsOptional();
            Property(x => x.PMModifyCheckDate).HasColumnName("PMMODIFYCHECKDATE").IsOptional();
            Property(x => x.AMDate).HasColumnName("AMDATE").IsOptional();
            Property(x => x.PMDate).HasColumnName("PMDATE").IsOptional();
            Property(x => x.IsToday).HasColumnName("ISTODAY").IsRequired().HasMaxLength(1);
            Property(x => x.AttendanceState).HasColumnName("ATTENDANCESTATE").IsRequired().HasMaxLength(4);
        }
    }

    // V_S_D_UserAptitude
    internal partial class V_S_D_UserAptitudeConfiguration : EntityTypeConfiguration<V_S_D_UserAptitude>
    {
        public V_S_D_UserAptitudeConfiguration()
        {
            ToTable("V_S_D_USERAPTITUDE");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.UserID).HasColumnName("USERID").IsOptional().HasMaxLength(50);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(50);
            Property(x => x.EngageMajor).HasColumnName("ENGAGEMAJOR").IsOptional().HasMaxLength(50);
            Property(x => x.Postion).HasColumnName("POSTION").IsOptional().HasMaxLength(50);
            Property(x => x.AptitudeLevel).HasColumnName("APTITUDELEVEL").IsOptional().HasMaxLength(50);
            Property(x => x.Major).HasColumnName("MAJOR").IsOptional().HasMaxLength(50);
        }
    }

    // V_S_W_AttendanceEditLog
    internal partial class V_S_W_AttendanceEditLogConfiguration : EntityTypeConfiguration<V_S_W_AttendanceEditLog>
    {
        public V_S_W_AttendanceEditLogConfiguration()
        {
            ToTable("V_S_W_ATTENDANCEEDITLOG");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.SourceID).HasColumnName("SOURCEID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyReason).HasColumnName("MODIFYREASON").IsOptional().HasMaxLength(500);
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUserName).HasColumnName("CREATEUSERNAME").IsOptional().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.UserID).HasColumnName("USERID").IsOptional().HasMaxLength(50);
            Property(x => x.UserName).HasColumnName("USERNAME").IsOptional().HasMaxLength(50);
            Property(x => x.WorkNo).HasColumnName("WORKNO").IsOptional().HasMaxLength(50);
            Property(x => x.OrgID).HasColumnName("ORGID").IsOptional().HasMaxLength(50);
            Property(x => x.OrgName).HasColumnName("ORGNAME").IsOptional().HasMaxLength(200);
            Property(x => x.CheckType).HasColumnName("CHECKTYPE").IsOptional().HasMaxLength(10);
            Property(x => x.CheckDate).HasColumnName("CHECKDATE").IsOptional();
            Property(x => x.LastModifyCheckDate).HasColumnName("LASTMODIFYCHECKDATE").IsOptional();
        }
    }

    // V_S_W_AttendanceInfo
    internal partial class V_S_W_AttendanceInfoConfiguration : EntityTypeConfiguration<V_S_W_AttendanceInfo>
    {
        public V_S_W_AttendanceInfoConfiguration()
        {
            ToTable("V_S_W_ATTENDANCEINFO");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.UserID).HasColumnName("USERID").IsOptional().HasMaxLength(50);
            Property(x => x.UserName).HasColumnName("USERNAME").IsOptional().HasMaxLength(50);
            Property(x => x.WorkNo).HasColumnName("WORKNO").IsOptional().HasMaxLength(50);
            Property(x => x.WorkDay).HasColumnName("WORKDAY").IsOptional();
            Property(x => x.CheckDate).HasColumnName("CHECKDATE").IsOptional();
            Property(x => x.CheckType).HasColumnName("CHECKTYPE").IsOptional().HasMaxLength(10);
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUserName).HasColumnName("CREATEUSERNAME").IsOptional().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUserName).HasColumnName("MODIFYUSERNAME").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyDate).HasColumnName("MODIFYDATE").IsOptional();
            Property(x => x.OrgID).HasColumnName("ORGID").IsOptional().HasMaxLength(50);
            Property(x => x.OrgName).HasColumnName("ORGNAME").IsOptional().HasMaxLength(200);
        }
    }

    // V_T_QualificationNoReturn
    internal partial class V_T_QualificationNoReturnConfiguration : EntityTypeConfiguration<V_T_QualificationNoReturn>
    {
        public V_T_QualificationNoReturnConfiguration()
        {
            ToTable("V_T_QUALIFICATIONNORETURN");
            HasKey(x => new { x.ID, x.LendID });

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.SerialNumber).HasColumnName("SERIALNUMBER").IsOptional().HasMaxLength(50);
            Property(x => x.LendID).HasColumnName("LENDID").IsRequired().HasMaxLength(50);
            Property(x => x.ApplyUserID).HasColumnName("APPLYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ApplyUserIDName).HasColumnName("APPLYUSERIDNAME").IsOptional().HasMaxLength(50);
            Property(x => x.ApplyDate).HasColumnName("APPLYDATE").IsOptional();
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(50);
            Property(x => x.CertificateNo).HasColumnName("CERTIFICATENO").IsOptional().HasMaxLength(50);
            Property(x => x.QualificationLevel).HasColumnName("QUALIFICATIONLEVEL").IsOptional().HasMaxLength(50);
            Property(x => x.QualificationID).HasColumnName("QUALIFICATIONID").IsOptional().HasMaxLength(50);
            Property(x => x.OriginalNum).HasColumnName("ORIGINALNUM").IsOptional();
            Property(x => x.CopyNum).HasColumnName("COPYNUM").IsOptional();
        }
    }

    // V_T_QualificationReturn
    internal partial class V_T_QualificationReturnConfiguration : EntityTypeConfiguration<V_T_QualificationReturn>
    {
        public V_T_QualificationReturnConfiguration()
        {
            ToTable("V_T_QUALIFICATIONRETURN");
            HasKey(x => new { x.ID, x.ReturnID });

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.ReturnID).HasColumnName("RETURNID").IsRequired().HasMaxLength(50);
            Property(x => x.SerialNumber).HasColumnName("SERIALNUMBER").IsOptional().HasMaxLength(50);
            Property(x => x.HandleDate).HasColumnName("HANDLEDATE").IsOptional();
            Property(x => x.HandleUserID).HasColumnName("HANDLEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.HandleUserIDName).HasColumnName("HANDLEUSERIDNAME").IsOptional().HasMaxLength(50);
            Property(x => x.ReturnDate).HasColumnName("RETURNDATE").IsOptional();
            Property(x => x.ReturnUserID).HasColumnName("RETURNUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ReturnUserIDName).HasColumnName("RETURNUSERIDNAME").IsOptional().HasMaxLength(50);
            Property(x => x.ApplyDate).HasColumnName("APPLYDATE").IsOptional();
            Property(x => x.ApplyUserID).HasColumnName("APPLYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ApplyUserIDName).HasColumnName("APPLYUSERIDNAME").IsOptional().HasMaxLength(50);
            Property(x => x.CertificateNo).HasColumnName("CERTIFICATENO").IsOptional().HasMaxLength(50);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(200);
            Property(x => x.QualificationID).HasColumnName("QUALIFICATIONID").IsOptional().HasMaxLength(50);
            Property(x => x.DetailID).HasColumnName("DETAILID").IsOptional().HasMaxLength(50);
            Property(x => x.LendID).HasColumnName("LENDID").IsOptional().HasMaxLength(50);
            Property(x => x.LendSerialNumber).HasColumnName("LENDSERIALNUMBER").IsOptional().HasMaxLength(50);
            Property(x => x.OriginalNum).HasColumnName("ORIGINALNUM").IsOptional();
            Property(x => x.CopyNum).HasColumnName("COPYNUM").IsOptional();
        }
    }

    // V_T_WIFixedSalary
    internal partial class V_T_WIFixedSalaryConfiguration : EntityTypeConfiguration<V_T_WIFixedSalary>
    {
        public V_T_WIFixedSalaryConfiguration()
        {
            ToTable("V_T_WIFIXEDSALARY");
            HasKey(x => x.ID);

            Property(x => x.EmployeeCode).HasColumnName("EMPLOYEECODE").IsOptional().HasMaxLength(50);
            Property(x => x.EmployeeName).HasColumnName("EMPLOYEENAME").IsOptional().HasMaxLength(50);
            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.Remark).HasColumnName("REMARK").IsOptional();
            Property(x => x.Year).HasColumnName("YEAR").IsOptional().HasMaxLength(50);
            Property(x => x.Month).HasColumnName("MONTH").IsOptional().HasMaxLength(50);
            Property(x => x.BaseSalaryTotal).HasColumnName("BASESALARYTOTAL").IsOptional().HasPrecision(18,2);
            Property(x => x.PostSalary).HasColumnName("POSTSALARY").IsOptional().HasPrecision(18,2);
            Property(x => x.GradeSalary).HasColumnName("GRADESALARY").IsOptional().HasPrecision(18,2);
            Property(x => x.NormalSalary).HasColumnName("NORMALSALARY").IsOptional().HasPrecision(18,2);
            Property(x => x.AboveReservedSalary).HasColumnName("ABOVERESERVEDSALARY").IsOptional().HasPrecision(18,2);
            Property(x => x.SubsidyTotal).HasColumnName("SUBSIDYTOTAL").IsOptional().HasPrecision(18,2);
            Property(x => x.PostSubsidy).HasColumnName("POSTSUBSIDY").IsOptional().HasPrecision(18,2);
            Property(x => x.ReservedSubsidy).HasColumnName("RESERVEDSUBSIDY").IsOptional().HasPrecision(18,2);
            Property(x => x.LocalSubsidy).HasColumnName("LOCALSUBSIDY").IsOptional().HasPrecision(18,2);
            Property(x => x.YuanPostSubsidy).HasColumnName("YUANPOSTSUBSIDY").IsOptional().HasPrecision(18,2);
            Property(x => x.RentingSubsidy).HasColumnName("RENTINGSUBSIDY").IsOptional().HasPrecision(18,2);
            Property(x => x.RegisterSubsidy).HasColumnName("REGISTERSUBSIDY").IsOptional().HasPrecision(18,2);
            Property(x => x.LifeSubsidy).HasColumnName("LIFESUBSIDY").IsOptional().HasPrecision(18,2);
            Property(x => x.MonthSubsidyTotal).HasColumnName("MONTHSUBSIDYTOTAL").IsOptional().HasPrecision(18,2);
            Property(x => x.Sex).HasColumnName("SEX").IsOptional().HasMaxLength(50);
            Property(x => x.Birthday).HasColumnName("BIRTHDAY").IsOptional();
            Property(x => x.JoinWorkDate).HasColumnName("JOINWORKDATE").IsOptional();
            Property(x => x.Educational).HasColumnName("EDUCATIONAL").IsOptional().HasMaxLength(50);
            Property(x => x.DeptID).HasColumnName("DEPTID").IsOptional().HasMaxLength(50);
            Property(x => x.DeptName).HasColumnName("DEPTNAME").IsOptional().HasMaxLength(50);
            Property(x => x.MedicalInsurancePayDate).HasColumnName("MEDICALINSURANCEPAYDATE").IsOptional();
            Property(x => x.EndowmentInsurancePayDate).HasColumnName("ENDOWMENTINSURANCEPAYDATE").IsOptional();
            Property(x => x.FundPayDate).HasColumnName("FUNDPAYDATE").IsOptional();
        }
    }

    // V_T_WIFundPay
    internal partial class V_T_WIFundPayConfiguration : EntityTypeConfiguration<V_T_WIFundPay>
    {
        public V_T_WIFundPayConfiguration()
        {
            ToTable("V_T_WIFUNDPAY");
            HasKey(x => new { x.ID, x.ProvidentFundCardinality, x.PersonProportion, x.DeptProportion, x.PersonalCardinality, x.Coprcardinality, x.Quarter });

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.EmployeeName).HasColumnName("EMPLOYEENAME").IsOptional().HasMaxLength(50);
            Property(x => x.EmployeeCode).HasColumnName("EMPLOYEECODE").IsOptional().HasMaxLength(50);
            Property(x => x.Sex).HasColumnName("SEX").IsOptional().HasMaxLength(50);
            Property(x => x.Post).HasColumnName("POST").IsOptional().HasMaxLength(50);
            Property(x => x.EmploymentWay).HasColumnName("EMPLOYMENTWAY").IsOptional().HasMaxLength(50);
            Property(x => x.Month).HasColumnName("MONTH").IsOptional().HasMaxLength(50);
            Property(x => x.Year).HasColumnName("YEAR").IsOptional().HasMaxLength(50);
            Property(x => x.ProvidentFundCardinality).HasColumnName("PROVIDENTFUNDCARDINALITY").IsRequired().HasPrecision(18,2);
            Property(x => x.DeptName).HasColumnName("DEPTNAME").IsOptional().HasMaxLength(50);
            Property(x => x.DeptID).HasColumnName("DEPTID").IsOptional().HasMaxLength(50);
            Property(x => x.PersonProportion).HasColumnName("PERSONPROPORTION").IsRequired().HasPrecision(18,2);
            Property(x => x.DeptProportion).HasColumnName("DEPTPROPORTION").IsRequired().HasPrecision(18,2);
            Property(x => x.PersonalCardinality).HasColumnName("PERSONALCARDINALITY").IsRequired().HasPrecision(38,6);
            Property(x => x.Coprcardinality).HasColumnName("COPRCARDINALITY").IsRequired().HasPrecision(38,6);
            Property(x => x.TotalMoney).HasColumnName("TOTALMONEY").IsOptional().HasPrecision(38,6);
            Property(x => x.ProvidentFundAccount).HasColumnName("PROVIDENTFUNDACCOUNT").IsOptional().HasMaxLength(50);
            Property(x => x.JoinDate).HasColumnName("JOINDATE").IsOptional();
            Property(x => x.Quarter).HasColumnName("QUARTER").IsRequired().HasMaxLength(6);
        }
    }

    // V_T_WISocialSecurityPay
    internal partial class V_T_WISocialSecurityPayConfiguration : EntityTypeConfiguration<V_T_WISocialSecurityPay>
    {
        public V_T_WISocialSecurityPayConfiguration()
        {
            ToTable("V_T_WISOCIALSECURITYPAY");
            HasKey(x => new { x.ID, x.Quarter });

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.DeptName).HasColumnName("DEPTNAME").IsOptional().HasMaxLength(50);
            Property(x => x.Year).HasColumnName("YEAR").IsOptional().HasMaxLength(50);
            Property(x => x.Month).HasColumnName("MONTH").IsOptional().HasMaxLength(50);
            Property(x => x.EmployeeID).HasColumnName("EMPLOYEEID").IsOptional().HasMaxLength(50);
            Property(x => x.MedicalInsuranceCardinality).HasColumnName("MEDICALINSURANCECARDINALITY").IsOptional().HasPrecision(18,2);
            Property(x => x.EmployeeName).HasColumnName("EMPLOYEENAME").IsOptional().HasMaxLength(50);
            Property(x => x.EndowmentInsuranceCardinality).HasColumnName("ENDOWMENTINSURANCECARDINALITY").IsOptional().HasPrecision(18,2);
            Property(x => x.PensionDeptProportion).HasColumnName("PENSIONDEPTPROPORTION").IsOptional().HasPrecision(18,2);
            Property(x => x.PensionPersonalBase).HasColumnName("PENSIONPERSONALBASE").IsOptional().HasPrecision(18,2);
            Property(x => x.PensionpersonalProportion).HasColumnName("PENSIONPERSONALPROPORTION").IsOptional().HasPrecision(18,2);
            Property(x => x.MedicalDeptProportion).HasColumnName("MEDICALDEPTPROPORTION").IsOptional().HasPrecision(18,2);
            Property(x => x.MedicalPersonalBase).HasColumnName("MEDICALPERSONALBASE").IsOptional().HasPrecision(18,2);
            Property(x => x.MedicalPersonalProportion).HasColumnName("MEDICALPERSONALPROPORTION").IsOptional().HasPrecision(18,2);
            Property(x => x.Sex).HasColumnName("SEX").IsOptional().HasMaxLength(50);
            Property(x => x.EmployeeCode).HasColumnName("EMPLOYEECODE").IsOptional().HasMaxLength(50);
            Property(x => x.MedicalInsuranceAccount).HasColumnName("MEDICALINSURANCEACCOUNT").IsOptional().HasMaxLength(50);
            Property(x => x.EndowmentInsuranceAccount).HasColumnName("ENDOWMENTINSURANCEACCOUNT").IsOptional().HasMaxLength(50);
            Property(x => x.JoinDate).HasColumnName("JOINDATE").IsOptional();
            Property(x => x.EndowmentJoinDate).HasColumnName("ENDOWMENTJOINDATE").IsOptional();
            Property(x => x.Quarter).HasColumnName("QUARTER").IsRequired().HasMaxLength(6);
            Property(x => x.EicoprCardinality).HasColumnName("EICOPRCARDINALITY").IsOptional().HasPrecision(38,6);
            Property(x => x.EipCardinality).HasColumnName("EIPCARDINALITY").IsOptional().HasPrecision(38,6);
            Property(x => x.MicoprCardinality).HasColumnName("MICOPRCARDINALITY").IsOptional().HasPrecision(38,6);
            Property(x => x.MipCardinality).HasColumnName("MIPCARDINALITY").IsOptional().HasPrecision(38,6);
            Property(x => x.Post).HasColumnName("POST").IsOptional().HasMaxLength(50);
            Property(x => x.TotalMoney).HasColumnName("TOTALMONEY").IsOptional().HasPrecision(38,6);
        }
    }

}

