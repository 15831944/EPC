

// This file was automatically generated.
// Do not make changes directly to this file - edit the template instead.
// 
// The following connection settings were used to generate this file
// 
//     Connection String Name: "OfficeAuto"
//     Connection String:      "data source=.;Initial Catalog=EPM_OfficeAuto;User ID=sa;PWD=123.zxc;"

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

namespace OfficeAuto.Logic.Domain
{
    // ************************************************************************
    // Database context
    public partial class OfficeAutoEntities : Formula.FormulaDbContext
    {
        public IDbSet<S_D_Comment> S_D_Comment { get; set; } // S_D_Comment
        public IDbSet<S_D_Incoming> S_D_Incoming { get; set; } // S_D_Incoming
        public IDbSet<S_D_Posting> S_D_Posting { get; set; } // S_D_Posting
        public IDbSet<S_D_RedTitle> S_D_RedTitle { get; set; } // S_D_RedTitle
        public IDbSet<S_F_DocumentFileAuthority> S_F_DocumentFileAuthority { get; set; } // S_F_DocumentFileAuthority
        public IDbSet<S_F_DocumentInfo> S_F_DocumentInfo { get; set; } // S_F_DocumentInfo
        public IDbSet<S_F_FileInfo> S_F_FileInfo { get; set; } // S_F_FileInfo
        public IDbSet<S_FC_CostInfo> S_FC_CostInfo { get; set; } // S_FC_CostInfo
        public IDbSet<S_FC_UserLoanAccount> S_FC_UserLoanAccount { get; set; } // S_FC_UserLoanAccount
        public IDbSet<S_M_ConferenceRoom> S_M_ConferenceRoom { get; set; } // S_M_ConferenceRoom
        public IDbSet<S_Survey_Option> S_Survey_Option { get; set; } // S_Survey_Option
        public IDbSet<S_Survey_Question> S_Survey_Question { get; set; } // S_Survey_Question
        public IDbSet<S_Survey_QuestionComment> S_Survey_QuestionComment { get; set; } // S_Survey_QuestionComment
        public IDbSet<S_Survey_QuestionVote> S_Survey_QuestionVote { get; set; } // S_Survey_QuestionVote
        public IDbSet<S_Survey_Subject> S_Survey_Subject { get; set; } // S_Survey_Subject
        public IDbSet<S_Survey_Vote> S_Survey_Vote { get; set; } // S_Survey_Vote
        public IDbSet<T_B_MemoManagement> T_B_MemoManagement { get; set; } // T_B_MemoManagement
        public IDbSet<T_B_OutgoingFile> T_B_OutgoingFile { get; set; } // T_B_OutgoingFile
        public IDbSet<T_B_Remind> T_B_Remind { get; set; } // T_B_Remind
        public IDbSet<T_B_SendFaxManagement> T_B_SendFaxManagement { get; set; } // T_B_SendFaxManagement
        public IDbSet<T_F_LoanApply> T_F_LoanApply { get; set; } // T_F_LoanApply
        public IDbSet<T_F_ReimbursementApply> T_F_ReimbursementApply { get; set; } // T_F_ReimbursementApply
        public IDbSet<T_F_ReimbursementApply_Details> T_F_ReimbursementApply_Details { get; set; } // T_F_ReimbursementApply_Details
        public IDbSet<T_HC_Test> T_HC_Test { get; set; } // T_HC_Test
        public IDbSet<T_M_ConferenceApply> T_M_ConferenceApply { get; set; } // T_M_ConferenceApply
        public IDbSet<T_M_ConferenceRegist> T_M_ConferenceRegist { get; set; } // T_M_ConferenceRegist
        public IDbSet<T_M_ConferenceRoom> T_M_ConferenceRoom { get; set; } // T_M_ConferenceRoom
        public IDbSet<T_M_ConferenceSummary> T_M_ConferenceSummary { get; set; } // T_M_ConferenceSummary
        public IDbSet<T_M_OuterConference> T_M_OuterConference { get; set; } // T_M_OuterConference
        public IDbSet<T_M_OuterConference_CostGrid> T_M_OuterConference_CostGrid { get; set; } // T_M_OuterConference_CostGrid
        public IDbSet<T_ProductFileSeal> T_ProductFileSeal { get; set; } // T_ProductFileSeal
        public IDbSet<T_ProductFileSeal_CGXX> T_ProductFileSeal_CGXX { get; set; } // T_ProductFileSeal_CGXX
        public IDbSet<T_ProductFileSeal_SealProductInfo> T_ProductFileSeal_SealProductInfo { get; set; } // T_ProductFileSeal_SealProductInfo
        public IDbSet<T_ProductFileSeal_YGJSWJZB> T_ProductFileSeal_YGJSWJZB { get; set; } // T_ProductFileSeal_YGJSWJZB
        public IDbSet<T_Seal_Apply> T_Seal_Apply { get; set; } // T_Seal_Apply
        public IDbSet<T_Seal_Apply_Detail> T_Seal_Apply_Detail { get; set; } // T_Seal_Apply_Detail
        public IDbSet<T_Seal_Borrow> T_Seal_Borrow { get; set; } // T_Seal_Borrow
        public IDbSet<T_Seal_Borrow_Detail> T_Seal_Borrow_Detail { get; set; } // T_Seal_Borrow_Detail
        public IDbSet<T_Seal_Change> T_Seal_Change { get; set; } // T_Seal_Change
        public IDbSet<T_Seal_Repeal> T_Seal_Repeal { get; set; } // T_Seal_Repeal
        public IDbSet<T_Seal_SealInfo> T_Seal_SealInfo { get; set; } // T_Seal_SealInfo
        public IDbSet<T_Seal_Transfer> T_Seal_Transfer { get; set; } // T_Seal_Transfer
        public IDbSet<V_DocumentFileAuthority> V_DocumentFileAuthority { get; set; } // V_DocumentFileAuthority

        static OfficeAutoEntities()
        {
            Database.SetInitializer<OfficeAutoEntities>(null);
        }

        public OfficeAutoEntities()
            : base("Name=OfficeAuto")
        {
        }

        public OfficeAutoEntities(string connectionString) : base(connectionString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new S_D_CommentConfiguration());
            modelBuilder.Configurations.Add(new S_D_IncomingConfiguration());
            modelBuilder.Configurations.Add(new S_D_PostingConfiguration());
            modelBuilder.Configurations.Add(new S_D_RedTitleConfiguration());
            modelBuilder.Configurations.Add(new S_F_DocumentFileAuthorityConfiguration());
            modelBuilder.Configurations.Add(new S_F_DocumentInfoConfiguration());
            modelBuilder.Configurations.Add(new S_F_FileInfoConfiguration());
            modelBuilder.Configurations.Add(new S_FC_CostInfoConfiguration());
            modelBuilder.Configurations.Add(new S_FC_UserLoanAccountConfiguration());
            modelBuilder.Configurations.Add(new S_M_ConferenceRoomConfiguration());
            modelBuilder.Configurations.Add(new S_Survey_OptionConfiguration());
            modelBuilder.Configurations.Add(new S_Survey_QuestionConfiguration());
            modelBuilder.Configurations.Add(new S_Survey_QuestionCommentConfiguration());
            modelBuilder.Configurations.Add(new S_Survey_QuestionVoteConfiguration());
            modelBuilder.Configurations.Add(new S_Survey_SubjectConfiguration());
            modelBuilder.Configurations.Add(new S_Survey_VoteConfiguration());
            modelBuilder.Configurations.Add(new T_B_MemoManagementConfiguration());
            modelBuilder.Configurations.Add(new T_B_OutgoingFileConfiguration());
            modelBuilder.Configurations.Add(new T_B_RemindConfiguration());
            modelBuilder.Configurations.Add(new T_B_SendFaxManagementConfiguration());
            modelBuilder.Configurations.Add(new T_F_LoanApplyConfiguration());
            modelBuilder.Configurations.Add(new T_F_ReimbursementApplyConfiguration());
            modelBuilder.Configurations.Add(new T_F_ReimbursementApply_DetailsConfiguration());
            modelBuilder.Configurations.Add(new T_HC_TestConfiguration());
            modelBuilder.Configurations.Add(new T_M_ConferenceApplyConfiguration());
            modelBuilder.Configurations.Add(new T_M_ConferenceRegistConfiguration());
            modelBuilder.Configurations.Add(new T_M_ConferenceRoomConfiguration());
            modelBuilder.Configurations.Add(new T_M_ConferenceSummaryConfiguration());
            modelBuilder.Configurations.Add(new T_M_OuterConferenceConfiguration());
            modelBuilder.Configurations.Add(new T_M_OuterConference_CostGridConfiguration());
            modelBuilder.Configurations.Add(new T_ProductFileSealConfiguration());
            modelBuilder.Configurations.Add(new T_ProductFileSeal_CGXXConfiguration());
            modelBuilder.Configurations.Add(new T_ProductFileSeal_SealProductInfoConfiguration());
            modelBuilder.Configurations.Add(new T_ProductFileSeal_YGJSWJZBConfiguration());
            modelBuilder.Configurations.Add(new T_Seal_ApplyConfiguration());
            modelBuilder.Configurations.Add(new T_Seal_Apply_DetailConfiguration());
            modelBuilder.Configurations.Add(new T_Seal_BorrowConfiguration());
            modelBuilder.Configurations.Add(new T_Seal_Borrow_DetailConfiguration());
            modelBuilder.Configurations.Add(new T_Seal_ChangeConfiguration());
            modelBuilder.Configurations.Add(new T_Seal_RepealConfiguration());
            modelBuilder.Configurations.Add(new T_Seal_SealInfoConfiguration());
            modelBuilder.Configurations.Add(new T_Seal_TransferConfiguration());
            modelBuilder.Configurations.Add(new V_DocumentFileAuthorityConfiguration());
        }
    }

    // ************************************************************************
    // POCO classes

	/// <summary>批语</summary>	
	[Description("批语")]
    public partial class S_D_Comment : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary>批语类型</summary>	
		[Description("批语类型")]
        public string Type { get; set; } // Type
		/// <summary>用户名称</summary>	
		[Description("用户名称")]
        public string UserName { get; set; } // UserName
		/// <summary>用户ID</summary>	
		[Description("用户ID")]
        public string UserID { get; set; } // UserID
		/// <summary>批语</summary>	
		[Description("批语")]
        public string Comment { get; set; } // Comment
		/// <summary>是否是模板</summary>	
		[Description("是否是模板")]
        public string IsTemplate { get; set; } // IsTemplate
		/// <summary>启用</summary>	
		[Description("启用")]
        public string IsUse { get; set; } // IsUse
		/// <summary></summary>	
		[Description("")]
        public string CreateUserName { get; set; } // CreateUserName
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get; set; } // CreateUserID
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateDate { get; set; } // CreateDate
    }

	/// <summary>收文登记</summary>	
	[Description("收文登记")]
    public partial class S_D_Incoming : Formula.BaseModel
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
		/// <summary>文件名称</summary>	
		[Description("文件名称")]
        public string FileTitle { get; set; } // FileTitle
		/// <summary>文件</summary>	
		[Description("文件")]
        public string Files { get; set; } // Files
		/// <summary>来文机关</summary>	
		[Description("来文机关")]
        public string OfficeFrom { get; set; } // OfficeFrom
		/// <summary>来文字号</summary>	
		[Description("来文字号")]
        public string FontSize { get; set; } // FontSize
		/// <summary>收文类别</summary>	
		[Description("收文类别")]
        public string Type { get; set; } // Type
		/// <summary>收文编号</summary>	
		[Description("收文编号")]
        public string Code { get; set; } // Code
		/// <summary>序号</summary>	
		[Description("序号")]
        public string Number { get; set; } // Number
		/// <summary>收文日期</summary>	
		[Description("收文日期")]
        public DateTime? IncomingDate { get; set; } // IncomingDate
		/// <summary>密级</summary>	
		[Description("密级")]
        public string Security { get; set; } // Security
		/// <summary>紧急程度</summary>	
		[Description("紧急程度")]
        public string EmergencyLevel { get; set; } // EmergencyLevel
		/// <summary>附件（个）</summary>	
		[Description("附件（个）")]
        public int? FileCount { get; set; } // FileCount
		/// <summary>需报材料</summary>	
		[Description("需报材料")]
        public string Materials { get; set; } // Materials
		/// <summary>办文截止时间</summary>	
		[Description("办文截止时间")]
        public DateTime? UndertakeDeadline { get; set; } // UndertakeDeadline
		/// <summary>主题词</summary>	
		[Description("主题词")]
        public string Subject { get; set; } // Subject
		/// <summary>收文登记</summary>	
		[Description("收文登记")]
        public string Register { get; set; } // Register
		/// <summary>院办党办主任批签</summary>	
		[Description("院办党办主任批签")]
        public string OfficialSign { get; set; } // OfficialSign
		/// <summary>相关领导批阅</summary>	
		[Description("相关领导批阅")]
        public string LeaderApprove { get; set; } // LeaderApprove
		/// <summary>主办部门领导办理</summary>	
		[Description("主办部门领导办理")]
        public string DeptLeaderHandle { get; set; } // DeptLeaderHandle
		/// <summary>会办部门领导办理</summary>	
		[Description("会办部门领导办理")]
        public string PresidentHandle { get; set; } // PresidentHandle
		/// <summary>相关人员办理</summary>	
		[Description("相关人员办理")]
        public string PeopleHandle { get; set; } // PeopleHandle
    }

	/// <summary>文件签发单</summary>	
	[Description("文件签发单")]
    public partial class S_D_Posting : Formula.BaseModel
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
		/// <summary></summary>	
		[Description("")]
        public byte[] MainFile { get; set; } // MainFile
		/// <summary></summary>	
		[Description("")]
        public byte[] RedFile { get; set; } // RedFile
		/// <summary>文稿标题</summary>	
		[Description("文稿标题")]
        public string Title { get; set; } // Title
		/// <summary>文号</summary>	
		[Description("文号")]
        public string FileNo { get; set; } // FileNo
		/// <summary>拟稿人</summary>	
		[Description("拟稿人")]
        public string Drafter { get; set; } // Drafter
		/// <summary>拟稿人名称</summary>	
		[Description("拟稿人名称")]
        public string DrafterName { get; set; } // DrafterName
		/// <summary>拟稿日期</summary>	
		[Description("拟稿日期")]
        public DateTime? DraftDate { get; set; } // DraftDate
		/// <summary>拟稿部门</summary>	
		[Description("拟稿部门")]
        public string DraftDept { get; set; } // DraftDept
		/// <summary>拟稿部门名称</summary>	
		[Description("拟稿部门名称")]
        public string DraftDeptName { get; set; } // DraftDeptName
		/// <summary>部门负责人</summary>	
		[Description("部门负责人")]
        public string DeptLeader { get; set; } // DeptLeader
		/// <summary>部门负责人名称</summary>	
		[Description("部门负责人名称")]
        public string DeptLeaderName { get; set; } // DeptLeaderName
		/// <summary>密级</summary>	
		[Description("密级")]
        public string Security { get; set; } // Security
		/// <summary>紧急程度</summary>	
		[Description("紧急程度")]
        public string EmergencyLevel { get; set; } // EmergencyLevel
		/// <summary>正文份数</summary>	
		[Description("正文份数")]
        public int? FormalNumber { get; set; } // FormalNumber
		/// <summary>附件份数</summary>	
		[Description("附件份数")]
        public int? AttachNumber { get; set; } // AttachNumber
		/// <summary>总份数</summary>	
		[Description("总份数")]
        public int? TotalNumber { get; set; } // TotalNumber
		/// <summary>主送单位</summary>	
		[Description("主送单位")]
        public string SendTo { get; set; } // SendTo
		/// <summary>主送单位名称</summary>	
		[Description("主送单位名称")]
        public string SendToName { get; set; } // SendToName
		/// <summary>抄送单位</summary>	
		[Description("抄送单位")]
        public string CopyTo { get; set; } // CopyTo
		/// <summary>抄送单位名称</summary>	
		[Description("抄送单位名称")]
        public string CopyToName { get; set; } // CopyToName
		/// <summary>主送人员</summary>	
		[Description("主送人员")]
        public string SpecificStaff { get; set; } // SpecificStaff
		/// <summary>主送人员名称</summary>	
		[Description("主送人员名称")]
        public string SpecificStaffName { get; set; } // SpecificStaffName
		/// <summary>拟稿意见</summary>	
		[Description("拟稿意见")]
        public string DraftSign { get; set; } // DraftSign
		/// <summary>科长审批</summary>	
		[Description("科长审批")]
        public string SectionChiefAudit { get; set; } // SectionChiefAudit
		/// <summary>处长审批</summary>	
		[Description("处长审批")]
        public string DirectorApprove { get; set; } // DirectorApprove
		/// <summary>院办收发</summary>	
		[Description("院办收发")]
        public string OfficialTransfer { get; set; } // OfficialTransfer
		/// <summary>主管院领导</summary>	
		[Description("主管院领导")]
        public string LeaderApprove { get; set; } // LeaderApprove
		/// <summary>院办文书</summary>	
		[Description("院办文书")]
        public string OfficialDocument { get; set; } // OfficialDocument
    }

	/// <summary>红头文件管理</summary>	
	[Description("红头文件管理")]
    public partial class S_D_RedTitle : Formula.BaseModel
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
		/// <summary>标题</summary>	
		[Description("标题")]
        public string Title { get; set; } // Title
		/// <summary>所属部门</summary>	
		[Description("所属部门")]
        public string DeptInfo { get; set; } // DeptInfo
		/// <summary>所属部门名称</summary>	
		[Description("所属部门名称")]
        public string DeptInfoName { get; set; } // DeptInfoName
		/// <summary>登记人</summary>	
		[Description("登记人")]
        public string Register { get; set; } // Register
		/// <summary>登记人名称</summary>	
		[Description("登记人名称")]
        public string RegisterName { get; set; } // RegisterName
		/// <summary>文件</summary>	
		[Description("文件")]
        public string RedFile { get; set; } // RedFile
		/// <summary>备注</summary>	
		[Description("备注")]
        public string Remark { get; set; } // Remark
    }

	/// <summary>文件资料库节点权限表</summary>	
	[Description("文件资料库节点权限表")]
    public partial class S_F_DocumentFileAuthority : Formula.BaseModel
    {
		/// <summary>主键ID</summary>	
		[Description("主键ID")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string DocumentInfoID { get; set; } // DocumentInfoID
		/// <summary>权限类型（ReadOnly,CanWrite,FullControl）</summary>	
		[Description("权限类型（ReadOnly,CanWrite,FullControl）")]
        public string AuthType { get; set; } // AuthType
		/// <summary>角色类型（User：用户；SysRole：系统角色；Org：组织；OrgRole：组织角色）</summary>	
		[Description("角色类型（User：用户；SysRole：系统角色；Org：组织；OrgRole：组织角色）")]
        public string RoleType { get; set; } // RoleType
		/// <summary>角色ID（对应用户ID，角色ID，部门ID）</summary>	
		[Description("角色ID（对应用户ID，角色ID，部门ID）")]
        public string RoleCode { get; set; } // RoleCode
		/// <summary></summary>	
		[Description("")]
        public int? IsParentAuth { get; set; } // IsParentAuth

        // Foreign keys
		[JsonIgnore]
        public virtual S_F_DocumentInfo S_F_DocumentInfo { get; set; } //  DocumentInfoID - FK_S_F_DocumentFileAuthority_S_F_DocumentInfo
    }

	/// <summary>文件资料柜节点表</summary>	
	[Description("文件资料柜节点表")]
    public partial class S_F_DocumentInfo : Formula.BaseModel
    {
		/// <summary>主键ID</summary>	
		[Description("主键ID")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary>父节点ID</summary>	
		[Description("父节点ID")]
        public string ParentID { get; set; } // ParentID
		/// <summary>全路径ID</summary>	
		[Description("全路径ID")]
        public string FullPathID { get; set; } // FullPathID
		/// <summary>节点名称</summary>	
		[Description("节点名称")]
        public string Name { get; set; } // Name
		/// <summary>节点编号</summary>	
		[Description("节点编号")]
        public string Code { get; set; } // Code
		/// <summary>排序号</summary>	
		[Description("排序号")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary>节点层级</summary>	
		[Description("节点层级")]
        public int? Level { get; set; } // Level
		/// <summary>创建人ID</summary>	
		[Description("创建人ID")]
        public string CreateUserID { get; set; } // CreateUserID
		/// <summary>创建人</summary>	
		[Description("创建人")]
        public string CreateUserName { get; set; } // CreateUserName
		/// <summary>创建时间</summary>	
		[Description("创建时间")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary>修改人ID</summary>	
		[Description("修改人ID")]
        public string ModifyUserID { get; set; } // ModifyUserID
		/// <summary>修改人</summary>	
		[Description("修改人")]
        public string ModifyUserName { get; set; } // ModifyUserName
		/// <summary>修改日期</summary>	
		[Description("修改日期")]
        public DateTime? ModifyDate { get; set; } // ModifyDate
		/// <summary>是否继承上级节点（0：不是；1：是）</summary>	
		[Description("是否继承上级节点（0：不是；1：是）")]
        public int? IsInherit { get; set; } // IsInherit
		/// <summary>作废标识（T：正常；F：作废）</summary>	
		[Description("作废标识（T：正常；F：作废）")]
        public string IsValid { get; set; } // IsValid
		/// <summary>删除标识（0：正常；1：删除）</summary>	
		[Description("删除标识（0：正常；1：删除）")]
        public int? IsDeleted { get; set; } // IsDeleted

        // Reverse navigation
		[JsonIgnore]
        public virtual ICollection<S_F_DocumentFileAuthority> S_F_DocumentFileAuthority { get { onS_F_DocumentFileAuthorityGetting(); return _S_F_DocumentFileAuthority;} }
		private ICollection<S_F_DocumentFileAuthority> _S_F_DocumentFileAuthority;
		partial void onS_F_DocumentFileAuthorityGetting();

		[JsonIgnore]
        public virtual ICollection<S_F_FileInfo> S_F_FileInfo { get { onS_F_FileInfoGetting(); return _S_F_FileInfo;} }
		private ICollection<S_F_FileInfo> _S_F_FileInfo;
		partial void onS_F_FileInfoGetting();


        public S_F_DocumentInfo()
        {
            _S_F_DocumentFileAuthority = new List<S_F_DocumentFileAuthority>();
            _S_F_FileInfo = new List<S_F_FileInfo>();
        }
    }

	/// <summary>资料柜文件信息</summary>	
	[Description("资料柜文件信息")]
    public partial class S_F_FileInfo : Formula.BaseModel
    {
		/// <summary>主键ID</summary>	
		[Description("主键ID")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary>文件资料柜对应ID</summary>	
		[Description("文件资料柜对应ID")]
        public string DocumentInfoID { get; set; } // DocumentInfoID
		/// <summary>文件名称</summary>	
		[Description("文件名称")]
        public string FileName { get; set; } // FileName
		/// <summary>文件编号</summary>	
		[Description("文件编号")]
        public string FileCode { get; set; } // FileCode
		/// <summary>文件扩展名称</summary>	
		[Description("文件扩展名称")]
        public string ExtName { get; set; } // ExtName
		/// <summary>文件大小</summary>	
		[Description("文件大小")]
        public int? FileSize { get; set; } // FileSize
		/// <summary>其它</summary>	
		[Description("其它")]
        public string Attachment { get; set; } // Attachment
		/// <summary>上传文件</summary>	
		[Description("上传文件")]
        public string SourceFileID { get; set; } // SourceFileID
		/// <summary>创建人ID</summary>	
		[Description("创建人ID")]
        public string CreateUserID { get; set; } // CreateUserID
		/// <summary>创建人</summary>	
		[Description("创建人")]
        public string CreateUserName { get; set; } // CreateUserName
		/// <summary>创建时间</summary>	
		[Description("创建时间")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary>修改人ID</summary>	
		[Description("修改人ID")]
        public string ModifyUserID { get; set; } // ModifyUserID
		/// <summary>修改人</summary>	
		[Description("修改人")]
        public string ModifyUserName { get; set; } // ModifyUserName
		/// <summary>修改日期</summary>	
		[Description("修改日期")]
        public DateTime? ModifyDate { get; set; } // ModifyDate
		/// <summary>部门ID</summary>	
		[Description("部门ID")]
        public string OrgID { get; set; } // OrgID
		/// <summary>部门名称</summary>	
		[Description("部门名称")]
        public string OrgName { get; set; } // OrgName
		/// <summary>备注</summary>	
		[Description("备注")]
        public string Remark { get; set; } // Remark
		/// <summary>是否作废</summary>	
		[Description("是否作废")]
        public string IsValid { get; set; } // IsValid
		/// <summary>是否删除</summary>	
		[Description("是否删除")]
        public int? IsDeleted { get; set; } // IsDeleted
		/// <summary>上传人</summary>	
		[Description("上传人")]
        public string SubmitUser { get; set; } // SubmitUser
		/// <summary>上传人名称</summary>	
		[Description("上传人名称")]
        public string SubmitUserName { get; set; } // SubmitUserName
		/// <summary>所属部门</summary>	
		[Description("所属部门")]
        public string SubmitDept { get; set; } // SubmitDept
		/// <summary>所属部门名称</summary>	
		[Description("所属部门名称")]
        public string SubmitDeptName { get; set; } // SubmitDeptName
		/// <summary>上传时间</summary>	
		[Description("上传时间")]
        public DateTime? SubmitDate { get; set; } // SubmitDate

        // Foreign keys
		[JsonIgnore]
        public virtual S_F_DocumentInfo S_F_DocumentInfo { get; set; } //  DocumentInfoID - FK_S_F_FileInfo_S_F_DocumentInfo
    }

	/// <summary></summary>	
	[Description("")]
    public partial class S_FC_CostInfo : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string SubjectName { get; set; } // SubjectName
		/// <summary></summary>	
		[Description("")]
        public string SubjectCode { get; set; } // SubjectCode
		/// <summary></summary>	
		[Description("")]
        public string ProjectID { get; set; } // ProjectID
		/// <summary></summary>	
		[Description("")]
        public string ProjectName { get; set; } // ProjectName
		/// <summary></summary>	
		[Description("")]
        public string ProjectCode { get; set; } // ProjectCode
		/// <summary></summary>	
		[Description("")]
        public string CostType { get; set; } // CostType
		/// <summary></summary>	
		[Description("")]
        public string ProjectType { get; set; } // ProjectType
		/// <summary></summary>	
		[Description("")]
        public decimal? UnitPrice { get; set; } // UnitPrice
		/// <summary></summary>	
		[Description("")]
        public decimal? Quantity { get; set; } // Quantity
		/// <summary></summary>	
		[Description("")]
        public decimal CostValue { get; set; } // CostValue
		/// <summary></summary>	
		[Description("")]
        public string CostUserID { get; set; } // CostUserID
		/// <summary></summary>	
		[Description("")]
        public string CostUserName { get; set; } // CostUserName
		/// <summary></summary>	
		[Description("")]
        public string CostUserDeptID { get; set; } // CostUserDeptID
		/// <summary></summary>	
		[Description("")]
        public string CostUserDeptName { get; set; } // CostUserDeptName
		/// <summary></summary>	
		[Description("")]
        public string ProjectDeptID { get; set; } // ProjectDeptID
		/// <summary></summary>	
		[Description("")]
        public string ProjectDeptName { get; set; } // ProjectDeptName
		/// <summary></summary>	
		[Description("")]
        public string ProjectChargerUserID { get; set; } // ProjectChargerUserID
		/// <summary></summary>	
		[Description("")]
        public string ProjectChargerUserName { get; set; } // ProjectChargerUserName
		/// <summary></summary>	
		[Description("")]
        public string ProjectClass { get; set; } // ProjectClass
		/// <summary></summary>	
		[Description("")]
        public DateTime CostDate { get; set; } // CostDate
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
        public string RelateID { get; set; } // RelateID
		/// <summary></summary>	
		[Description("")]
        public string FormID { get; set; } // FormID
		/// <summary></summary>	
		[Description("")]
        public string Extend1 { get; set; } // Extend1
		/// <summary></summary>	
		[Description("")]
        public string Extend2 { get; set; } // Extend2
		/// <summary></summary>	
		[Description("")]
        public string Extend3 { get; set; } // Extend3
		/// <summary></summary>	
		[Description("")]
        public string Extend4 { get; set; } // Extend4
		/// <summary></summary>	
		[Description("")]
        public string Extend5 { get; set; } // Extend5
		/// <summary></summary>	
		[Description("")]
        public string Extend6 { get; set; } // Extend6
		/// <summary></summary>	
		[Description("")]
        public string Extend7 { get; set; } // Extend7
		/// <summary></summary>	
		[Description("")]
        public string Extend8 { get; set; } // Extend8
    }

	/// <summary></summary>	
	[Description("")]
    public partial class S_FC_UserLoanAccount : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string UserID { get; set; } // UserID
		/// <summary></summary>	
		[Description("")]
        public string UserName { get; set; } // UserName
		/// <summary></summary>	
		[Description("")]
        public string UserDeptID { get; set; } // UserDeptID
		/// <summary></summary>	
		[Description("")]
        public string UserDeptName { get; set; } // UserDeptName
		/// <summary></summary>	
		[Description("")]
        public string AccountType { get; set; } // AccountType
		/// <summary></summary>	
		[Description("")]
        public decimal AccountValue { get; set; } // AccountValue
		/// <summary></summary>	
		[Description("")]
        public string RelateFormID { get; set; } // RelateFormID
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public string Operator { get; set; } // Operator
		/// <summary></summary>	
		[Description("")]
        public string OperatorName { get; set; } // OperatorName
		/// <summary></summary>	
		[Description("")]
        public DateTime? ReturnDate { get; set; } // ReturnDate
    }

	/// <summary>会议室基本信息</summary>	
	[Description("会议室基本信息")]
    public partial class S_M_ConferenceRoom : Formula.BaseModel
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
		/// <summary>会议室编号</summary>	
		[Description("会议室编号")]
        public string RoomName { get; set; } // RoomName
		/// <summary>联系人</summary>	
		[Description("联系人")]
        public string LinkName { get; set; } // LinkName
		/// <summary>最大使用人数</summary>	
		[Description("最大使用人数")]
        public int? Capacity { get; set; } // Capacity
		/// <summary>管理部门</summary>	
		[Description("管理部门")]
        public string ManageDeptID { get; set; } // ManageDeptID
		/// <summary>管理部门名称</summary>	
		[Description("管理部门名称")]
        public string ManageDeptIDName { get; set; } // ManageDeptIDName
		/// <summary>会议室地址</summary>	
		[Description("会议室地址")]
        public string RoomAddress { get; set; } // RoomAddress
		/// <summary>设施情况</summary>	
		[Description("设施情况")]
        public string Configuredevice { get; set; } // Configuredevice
    }

	/// <summary>问卷调查问题选项维护</summary>	
	[Description("问卷调查问题选项维护")]
    public partial class S_Survey_Option : Formula.BaseModel
    {
		/// <summary>主键ID</summary>	
		[Description("主键ID")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary>问题ID</summary>	
		[Description("问题ID")]
        public string QuestionID { get; set; } // QuestionID
		/// <summary>选项名称</summary>	
		[Description("选项名称")]
        public string OptionName { get; set; } // OptionName
		/// <summary>投票总数</summary>	
		[Description("投票总数")]
        public int? VoteNum { get; set; } // VoteNum
		/// <summary>创建人ID</summary>	
		[Description("创建人ID")]
        public string CreateUserID { get; set; } // CreateUserID
		/// <summary>创建人</summary>	
		[Description("创建人")]
        public string CreateUserName { get; set; } // CreateUserName
		/// <summary>创建时间</summary>	
		[Description("创建时间")]
        public DateTime? CreateDate { get; set; } // CreateDate

        // Reverse navigation
		[JsonIgnore]
        public virtual ICollection<S_Survey_Vote> S_Survey_Vote { get { onS_Survey_VoteGetting(); return _S_Survey_Vote;} }
		private ICollection<S_Survey_Vote> _S_Survey_Vote;
		partial void onS_Survey_VoteGetting();


        // Foreign keys
		[JsonIgnore]
        public virtual S_Survey_Question S_Survey_Question { get; set; } //  QuestionID - FK_S_Survey_Option_S_Survey_Question

        public S_Survey_Option()
        {
            _S_Survey_Vote = new List<S_Survey_Vote>();
        }
    }

	/// <summary>问卷调查问题维护</summary>	
	[Description("问卷调查问题维护")]
    public partial class S_Survey_Question : Formula.BaseModel
    {
		/// <summary>主键ID</summary>	
		[Description("主键ID")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary>调查主题ID</summary>	
		[Description("调查主题ID")]
        public string SurveyID { get; set; } // SurveyID
		/// <summary>调查问题标题</summary>	
		[Description("调查问题标题")]
        public string QuestionName { get; set; } // QuestionName
		/// <summary>问题类型(单选 ，多选 ，文本)</summary>	
		[Description("问题类型(单选 ，多选 ，文本)")]
        public string QuestionType { get; set; } // QuestionType
		/// <summary>是否必须回复</summary>	
		[Description("是否必须回复")]
        public string IsMustAnswer { get; set; } // IsMustAnswer
		/// <summary>是否需要评论</summary>	
		[Description("是否需要评论")]
        public string IsComment { get; set; } // IsComment
		/// <summary>创建人ID</summary>	
		[Description("创建人ID")]
        public string CreateUserID { get; set; } // CreateUserID
		/// <summary>创建人</summary>	
		[Description("创建人")]
        public string CreateUserName { get; set; } // CreateUserName
		/// <summary>创建日期</summary>	
		[Description("创建日期")]
        public DateTime? CreateDate { get; set; } // CreateDate

        // Reverse navigation
		[JsonIgnore]
        public virtual ICollection<S_Survey_Option> S_Survey_Option { get { onS_Survey_OptionGetting(); return _S_Survey_Option;} }
		private ICollection<S_Survey_Option> _S_Survey_Option;
		partial void onS_Survey_OptionGetting();

		[JsonIgnore]
        public virtual ICollection<S_Survey_QuestionComment> S_Survey_QuestionComment { get { onS_Survey_QuestionCommentGetting(); return _S_Survey_QuestionComment;} }
		private ICollection<S_Survey_QuestionComment> _S_Survey_QuestionComment;
		partial void onS_Survey_QuestionCommentGetting();

		[JsonIgnore]
        public virtual ICollection<S_Survey_QuestionVote> S_Survey_QuestionVote { get { onS_Survey_QuestionVoteGetting(); return _S_Survey_QuestionVote;} }
		private ICollection<S_Survey_QuestionVote> _S_Survey_QuestionVote;
		partial void onS_Survey_QuestionVoteGetting();


        // Foreign keys
		[JsonIgnore]
        public virtual S_Survey_Subject S_Survey_Subject { get; set; } //  SurveyID - FK_S_Survey_Question_S_Survey_Subject

        public S_Survey_Question()
        {
            _S_Survey_Option = new List<S_Survey_Option>();
            _S_Survey_QuestionComment = new List<S_Survey_QuestionComment>();
            _S_Survey_QuestionVote = new List<S_Survey_QuestionVote>();
        }
    }

	/// <summary>用户问卷调查问题评论表</summary>	
	[Description("用户问卷调查问题评论表")]
    public partial class S_Survey_QuestionComment : Formula.BaseModel
    {
		/// <summary>主键ID</summary>	
		[Description("主键ID")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary>问题ID</summary>	
		[Description("问题ID")]
        public string QuestionID { get; set; } // QuestionID
		/// <summary>投票人ID</summary>	
		[Description("投票人ID")]
        public string UserID { get; set; } // UserID
		/// <summary>投票人</summary>	
		[Description("投票人")]
        public string UserName { get; set; } // UserName
		/// <summary>评论</summary>	
		[Description("评论")]
        public string Comment { get; set; } // Comment
		/// <summary>投票时间</summary>	
		[Description("投票时间")]
        public DateTime? VoteDate { get; set; } // VoteDate

        // Foreign keys
		[JsonIgnore]
        public virtual S_Survey_Question S_Survey_Question { get; set; } //  QuestionID - FK_S_Survey_QuestionComment_S_Survey_Question
    }

	/// <summary>问卷调查问题文本描述表</summary>	
	[Description("问卷调查问题文本描述表")]
    public partial class S_Survey_QuestionVote : Formula.BaseModel
    {
		/// <summary>主键ID</summary>	
		[Description("主键ID")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary>问题ID</summary>	
		[Description("问题ID")]
        public string QuestionID { get; set; } // QuestionID
		/// <summary>投票人ID</summary>	
		[Description("投票人ID")]
        public string UserID { get; set; } // UserID
		/// <summary>投票人</summary>	
		[Description("投票人")]
        public string UserName { get; set; } // UserName
		/// <summary>问题信息</summary>	
		[Description("问题信息")]
        public string QuestionMsg { get; set; } // QuestionMsg
		/// <summary>投票时间</summary>	
		[Description("投票时间")]
        public DateTime? VoteDate { get; set; } // VoteDate

        // Foreign keys
		[JsonIgnore]
        public virtual S_Survey_Question S_Survey_Question { get; set; } //  QuestionID - FK_S_Survey_QuestionVote_S_Survey_Question
    }

	/// <summary>问卷调查配置</summary>	
	[Description("问卷调查配置")]
    public partial class S_Survey_Subject : Formula.BaseModel
    {
		/// <summary>主键ID</summary>	
		[Description("主键ID")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary>调查主题</summary>	
		[Description("调查主题")]
        public string SurveyName { get; set; } // SurveyName
		/// <summary>问题数量</summary>	
		[Description("问题数量")]
        public int? QuestionNum { get; set; } // QuestionNum
		/// <summary>是否当前调查主题</summary>	
		[Description("是否当前调查主题")]
        public string IsPeriod { get; set; } // IsPeriod
		/// <summary>调查开始时间</summary>	
		[Description("调查开始时间")]
        public DateTime? SurveyStartDate { get; set; } // SurveyStartDate
		/// <summary>调查结束时间</summary>	
		[Description("调查结束时间")]
        public DateTime? SurveyEndDate { get; set; } // SurveyEndDate
		/// <summary>是否记名</summary>	
		[Description("是否记名")]
        public string IsMemoryUser { get; set; } // IsMemoryUser
		/// <summary>查询人员ID</summary>	
		[Description("查询人员ID")]
        public string QueryUserIDs { get; set; } // QueryUserIDs
		/// <summary>查询人员</summary>	
		[Description("查询人员")]
        public string QueryUserNames { get; set; } // QueryUserNames
		/// <summary>统计人员ID</summary>	
		[Description("统计人员ID")]
        public string StatisticsUserIDs { get; set; } // StatisticsUserIDs
		/// <summary>统计 人员 </summary>	
		[Description("统计 人员 ")]
        public string StatisticsUserNames { get; set; } // StatisticsUserNames
		/// <summary>相关附件</summary>	
		[Description("相关附件")]
        public string SurveyAttchment { get; set; } // SurveyAttchment
		/// <summary>备注 </summary>	
		[Description("备注 ")]
        public string Remark { get; set; } // Remark
		/// <summary>发布人ID</summary>	
		[Description("发布人ID")]
        public string IssueID { get; set; } // IssueID
		/// <summary>发布人姓名</summary>	
		[Description("发布人姓名")]
        public string IssueName { get; set; } // IssueName
		/// <summary>发布时间</summary>	
		[Description("发布时间")]
        public DateTime? IssueDate { get; set; } // IssueDate
		/// <summary>创建人ID</summary>	
		[Description("创建人ID")]
        public string CreateUserID { get; set; } // CreateUserID
		/// <summary>创建人</summary>	
		[Description("创建人")]
        public string CreateUserName { get; set; } // CreateUserName
		/// <summary>创建日期</summary>	
		[Description("创建日期")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary>是否开启投票</summary>	
		[Description("是否开启投票")]
        public string State { get; set; } // State
		/// <summary></summary>	
		[Description("")]
        public string QueryDeptIDs { get; set; } // QueryDeptIDs
		/// <summary></summary>	
		[Description("")]
        public string QueryDeptNames { get; set; } // QueryDeptNames

        // Reverse navigation
		[JsonIgnore]
        public virtual ICollection<S_Survey_Question> S_Survey_Question { get { onS_Survey_QuestionGetting(); return _S_Survey_Question;} }
		private ICollection<S_Survey_Question> _S_Survey_Question;
		partial void onS_Survey_QuestionGetting();


        public S_Survey_Subject()
        {
            _S_Survey_Question = new List<S_Survey_Question>();
        }
    }

	/// <summary>投票表</summary>	
	[Description("投票表")]
    public partial class S_Survey_Vote : Formula.BaseModel
    {
		/// <summary>主键ID</summary>	
		[Description("主键ID")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary>问题选项ID</summary>	
		[Description("问题选项ID")]
        public string OptionID { get; set; } // OptionID
		/// <summary>投票人ID</summary>	
		[Description("投票人ID")]
        public string UserID { get; set; } // UserID
		/// <summary>投票人</summary>	
		[Description("投票人")]
        public string UserName { get; set; } // UserName
		/// <summary>投票时间</summary>	
		[Description("投票时间")]
        public DateTime? VoteDate { get; set; } // VoteDate

        // Foreign keys
		[JsonIgnore]
        public virtual S_Survey_Option S_Survey_Option { get; set; } //  OptionID - FK_S_Survey_Vote_S_Survey_Option
    }

	/// <summary>便函管理</summary>	
	[Description("便函管理")]
    public partial class T_B_MemoManagement : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary>创建人ID</summary>	
		[Description("创建人ID")]
        public string CreateUserID { get; set; } // CreateUserID
		/// <summary>创建人</summary>	
		[Description("创建人")]
        public string CreateUser { get; set; } // CreateUser
		/// <summary>创建时间</summary>	
		[Description("创建时间")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary>操作人ID</summary>	
		[Description("操作人ID")]
        public string ModifyUserID { get; set; } // ModifyUserID
		/// <summary>操作人</summary>	
		[Description("操作人")]
        public string ModifyUser { get; set; } // ModifyUser
		/// <summary>操作时间</summary>	
		[Description("操作时间")]
        public DateTime? ModifyDate { get; set; } // ModifyDate
		/// <summary></summary>	
		[Description("")]
        public string OrgID { get; set; } // OrgID
		/// <summary></summary>	
		[Description("")]
        public string OrgName { get; set; } // OrgName
		/// <summary>流程阶段</summary>	
		[Description("流程阶段")]
        public string FlowPhase { get; set; } // FlowPhase
		/// <summary>流程环节</summary>	
		[Description("流程环节")]
        public string StepName { get; set; } // StepName
		/// <summary>密级</summary>	
		[Description("密级")]
        public string Dense { get; set; } // Dense
		/// <summary>急缓</summary>	
		[Description("急缓")]
        public string UrgentSlow { get; set; } // UrgentSlow
		/// <summary>签发人</summary>	
		[Description("签发人")]
        public string LeaderIssueName { get; set; } // LeaderIssueName
		/// <summary>签发人ID</summary>	
		[Description("签发人ID")]
        public string LeaderIssueID { get; set; } // LeaderIssueID
		/// <summary>签发意见</summary>	
		[Description("签发意见")]
        public string LeaderIssueComment { get; set; } // LeaderIssueComment
		/// <summary>部门负责人</summary>	
		[Description("部门负责人")]
        public string WorkDeptLeaderName { get; set; } // WorkDeptLeaderName
		/// <summary>部门负责人ID</summary>	
		[Description("部门负责人ID")]
        public string WorkDeptLeaderID { get; set; } // WorkDeptLeaderID
		/// <summary>签发部门领导意见</summary>	
		[Description("签发部门领导意见")]
        public string WorkDeptLeaderComment { get; set; } // WorkDeptLeaderComment
		/// <summary>公共事业部核稿人</summary>	
		[Description("公共事业部核稿人")]
        public string PublicCareerDeptName { get; set; } // PublicCareerDeptName
		/// <summary>公共事业部核稿人ID</summary>	
		[Description("公共事业部核稿人ID")]
        public string PublicCareerDeptID { get; set; } // PublicCareerDeptID
		/// <summary>公共事业部核稿</summary>	
		[Description("公共事业部核稿")]
        public string PublicCareerDeptComment { get; set; } // PublicCareerDeptComment
		/// <summary>会签人</summary>	
		[Description("会签人")]
        public string LeaderCountersignNames { get; set; } // LeaderCountersignNames
		/// <summary>会签人ID</summary>	
		[Description("会签人ID")]
        public string LeaderCountersignIDs { get; set; } // LeaderCountersignIDs
		/// <summary>会签意见</summary>	
		[Description("会签意见")]
        public string LeaderCountersignComment { get; set; } // LeaderCountersignComment
		/// <summary>部门负责人</summary>	
		[Description("部门负责人")]
        public string DeptLeaderName { get; set; } // DeptLeaderName
		/// <summary>部门负责人ID</summary>	
		[Description("部门负责人ID")]
        public string DeptLeaderID { get; set; } // DeptLeaderID
		/// <summary>会签部门负责人意见</summary>	
		[Description("会签部门负责人意见")]
        public string DeptLeaderComment { get; set; } // DeptLeaderComment
		/// <summary>拟稿人意见</summary>	
		[Description("拟稿人意见")]
        public string DraftManComment { get; set; } // DraftManComment
		/// <summary>文号</summary>	
		[Description("文号")]
        public string PaperNumber { get; set; } // PaperNumber
		/// <summary>标题</summary>	
		[Description("标题")]
        public string Title { get; set; } // Title
		/// <summary>附件</summary>	
		[Description("附件")]
        public string DocID { get; set; } // DocID
		/// <summary>套红签章Word</summary>	
		[Description("套红签章Word")]
        public string MergeDocID { get; set; } // MergeDocID
		/// <summary>主送人</summary>	
		[Description("主送人")]
        public string MainSendUserName { get; set; } // MainSendUserName
		/// <summary>主送人ID</summary>	
		[Description("主送人ID")]
        public string MainSendUserID { get; set; } // MainSendUserID
		/// <summary>抄送人</summary>	
		[Description("抄送人")]
        public string OtherSendUserNames { get; set; } // OtherSendUserNames
		/// <summary>抄送人ID</summary>	
		[Description("抄送人ID")]
        public string OtherSendUserIDs { get; set; } // OtherSendUserIDs
		/// <summary>总份数</summary>	
		[Description("总份数")]
        public string TotalNumber { get; set; } // TotalNumber
		/// <summary>正文总份数</summary>	
		[Description("正文总份数")]
        public string BodyTotalNumber { get; set; } // BodyTotalNumber
		/// <summary>附件总份数</summary>	
		[Description("附件总份数")]
        public string AttachmentTotalNumber { get; set; } // AttachmentTotalNumber
		/// <summary>状态</summary>	
		[Description("状态")]
        public string Status { get; set; } // Status
		/// <summary>流程已执行步骤</summary>	
		[Description("流程已执行步骤")]
        public string ExecutedSteps { get; set; } // ExecutedSteps
    }

	/// <summary>发文</summary>	
	[Description("发文")]
    public partial class T_B_OutgoingFile : Formula.BaseModel
    {
		/// <summary>红头编辑ID</summary>	
		[Description("红头编辑ID")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary>创建人ID</summary>	
		[Description("创建人ID")]
        public string CreateUserID { get; set; } // CreateUserID
		/// <summary>创建人</summary>	
		[Description("创建人")]
        public string CreateUser { get; set; } // CreateUser
		/// <summary>创建时间</summary>	
		[Description("创建时间")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary>流程状态</summary>	
		[Description("流程状态")]
        public string FlowState { get; set; } // FlowState
		/// <summary>流程阶段</summary>	
		[Description("流程阶段")]
        public string FlowPhase { get; set; } // FlowPhase
		/// <summary>流程阶段</summary>	
		[Description("流程阶段")]
        public string StepName { get; set; } // StepName
		/// <summary>登记人ID</summary>	
		[Description("登记人ID")]
        public string WriteID { get; set; } // WriteID
		/// <summary>登记人姓名</summary>	
		[Description("登记人姓名")]
        public string WriteName { get; set; } // WriteName
		/// <summary>登记日期</summary>	
		[Description("登记日期")]
        public DateTime? WriteDate { get; set; } // WriteDate
		/// <summary>拟稿人部门ID</summary>	
		[Description("拟稿人部门ID")]
        public string WriteDeptID { get; set; } // WriteDeptID
		/// <summary>拟稿人部门名称</summary>	
		[Description("拟稿人部门名称")]
        public string WriteDeptName { get; set; } // WriteDeptName
		/// <summary>拟办日期</summary>	
		[Description("拟办日期")]
        public DateTime? HandleDate { get; set; } // HandleDate
		/// <summary>发文类型</summary>	
		[Description("发文类型")]
        public string OutType { get; set; } // OutType
		/// <summary>年份</summary>	
		[Description("年份")]
        public string Year { get; set; } // Year
		/// <summary>文号</summary>	
		[Description("文号")]
        public string Code { get; set; } // Code
		/// <summary>密级</summary>	
		[Description("密级")]
        public string Security { get; set; } // Security
		/// <summary>缓急</summary>	
		[Description("缓急")]
        public string Urgency { get; set; } // Urgency
		/// <summary>份数</summary>	
		[Description("份数")]
        public int? CopyCount { get; set; } // CopyCount
		/// <summary>标题</summary>	
		[Description("标题")]
        public string Title { get; set; } // Title
		/// <summary>主送</summary>	
		[Description("主送")]
        public string MainSend { get; set; } // MainSend
		/// <summary>抄报</summary>	
		[Description("抄报")]
        public string TellDept { get; set; } // TellDept
		/// <summary>抄送</summary>	
		[Description("抄送")]
        public string CopySend { get; set; } // CopySend
		/// <summary>正文（单附件）</summary>	
		[Description("正文（单附件）")]
        public string MainBody { get; set; } // MainBody
		/// <summary>正文（单附件）</summary>	
		[Description("正文（单附件）")]
        public string NewMainBody { get; set; } // NewMainBody
		/// <summary>附件</summary>	
		[Description("附件")]
        public string Attachment { get; set; } // Attachment
		/// <summary>主办单位核稿</summary>	
		[Description("主办单位核稿")]
        public string MainDeptCheckSign { get; set; } // MainDeptCheckSign
		/// <summary>拟稿</summary>	
		[Description("拟稿")]
        public string DrafterSign { get; set; } // DrafterSign
		/// <summary>多人会签</summary>	
		[Description("多人会签")]
        public string HuiQinSign { get; set; } // HuiQinSign
		/// <summary>签发</summary>	
		[Description("签发")]
        public string LeaderSign { get; set; } // LeaderSign
		/// <summary>套红管理员(要根据发文类型选择党办套红管理人员或者院办办理套红)</summary>	
		[Description("套红管理员(要根据发文类型选择党办套红管理人员或者院办办理套红)")]
        public string TaoHongAdmin { get; set; } // TaoHongAdmin
		/// <summary>印章ID</summary>	
		[Description("印章ID")]
        public string SealID { get; set; } // SealID
    }

	/// <summary></summary>	
	[Description("")]
    public partial class T_B_Remind : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary>创建人ID</summary>	
		[Description("创建人ID")]
        public string CreateUserID { get; set; } // CreateUserID
		/// <summary>创建人</summary>	
		[Description("创建人")]
        public string CreateUser { get; set; } // CreateUser
		/// <summary>创建时间</summary>	
		[Description("创建时间")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary>发文ID</summary>	
		[Description("发文ID")]
        public string OutgoingFileID { get; set; } // OutgoingFileID
		/// <summary>被提醒的人员ID</summary>	
		[Description("被提醒的人员ID")]
        public string UserID { get; set; } // UserID
		/// <summary>被提醒的人员姓名</summary>	
		[Description("被提醒的人员姓名")]
        public string UserName { get; set; } // UserName
		/// <summary>提醒内容</summary>	
		[Description("提醒内容")]
        public string Msg { get; set; } // Msg
    }

	/// <summary>发传真管理</summary>	
	[Description("发传真管理")]
    public partial class T_B_SendFaxManagement : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary>创建人ID</summary>	
		[Description("创建人ID")]
        public string CreateUserID { get; set; } // CreateUserID
		/// <summary>创建人</summary>	
		[Description("创建人")]
        public string CreateUser { get; set; } // CreateUser
		/// <summary>创建时间</summary>	
		[Description("创建时间")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary>操作人ID</summary>	
		[Description("操作人ID")]
        public string ModifyUserID { get; set; } // ModifyUserID
		/// <summary>操作人</summary>	
		[Description("操作人")]
        public string ModifyUser { get; set; } // ModifyUser
		/// <summary>操作时间</summary>	
		[Description("操作时间")]
        public DateTime? ModifyDate { get; set; } // ModifyDate
		/// <summary></summary>	
		[Description("")]
        public string OrgID { get; set; } // OrgID
		/// <summary></summary>	
		[Description("")]
        public string OrgName { get; set; } // OrgName
		/// <summary>收件单位</summary>	
		[Description("收件单位")]
        public string ReceiveDept { get; set; } // ReceiveDept
		/// <summary>发送单位</summary>	
		[Description("发送单位")]
        public string SendDept { get; set; } // SendDept
		/// <summary></summary>	
		[Description("")]
        public string SendDeptID { get; set; } // SendDeptID
		/// <summary>收件人</summary>	
		[Description("收件人")]
        public string ReceiveUserName { get; set; } // ReceiveUserName
		/// <summary>发件人</summary>	
		[Description("发件人")]
        public string SendUserName { get; set; } // SendUserName
		/// <summary></summary>	
		[Description("")]
        public string SendUserID { get; set; } // SendUserID
		/// <summary>抄送人</summary>	
		[Description("抄送人")]
        public string OtherSendUserNames { get; set; } // OtherSendUserNames
		/// <summary></summary>	
		[Description("")]
        public string OtherSendUserIDs { get; set; } // OtherSendUserIDs
		/// <summary></summary>	
		[Description("")]
        public string SignedUserIDs { get; set; } // SignedUserIDs
		/// <summary>签发</summary>	
		[Description("签发")]
        public string SignedUserNames { get; set; } // SignedUserNames
		/// <summary>页数</summary>	
		[Description("页数")]
        public string PageNo { get; set; } // PageNo
		/// <summary>电话</summary>	
		[Description("电话")]
        public string Telephone { get; set; } // Telephone
		/// <summary></summary>	
		[Description("")]
        public string Telephone2 { get; set; } // Telephone2
		/// <summary>传真</summary>	
		[Description("传真")]
        public string FaxNumber { get; set; } // FaxNumber
		/// <summary></summary>	
		[Description("")]
        public string FaxNumber2 { get; set; } // FaxNumber2
		/// <summary>传真主题</summary>	
		[Description("传真主题")]
        public string FaxTheme { get; set; } // FaxTheme
		/// <summary>附件</summary>	
		[Description("附件")]
        public string Attachment { get; set; } // Attachment
		/// <summary>核稿人意见</summary>	
		[Description("核稿人意见")]
        public string CheckorComment { get; set; } // CheckorComment
		/// <summary>部门领导意见</summary>	
		[Description("部门领导意见")]
        public string DeptLeaderComment { get; set; } // DeptLeaderComment
		/// <summary>分管领导意见</summary>	
		[Description("分管领导意见")]
        public string DepartLeaderComment { get; set; } // DepartLeaderComment
		/// <summary>流程阶段</summary>	
		[Description("流程阶段")]
        public string FlowPhase { get; set; } // FlowPhase
    }

	/// <summary>借款申请单</summary>	
	[Description("借款申请单")]
    public partial class T_F_LoanApply : Formula.BaseModel
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
		/// <summary>借款人</summary>	
		[Description("借款人")]
        public string ApplyUser { get; set; } // ApplyUser
		/// <summary>借款人名称</summary>	
		[Description("借款人名称")]
        public string ApplyUserName { get; set; } // ApplyUserName
		/// <summary>借款人部门</summary>	
		[Description("借款人部门")]
        public string ApplyDept { get; set; } // ApplyDept
		/// <summary>借款人部门名称</summary>	
		[Description("借款人部门名称")]
        public string ApplyDeptName { get; set; } // ApplyDeptName
		/// <summary>申请时间</summary>	
		[Description("申请时间")]
        public DateTime? ApplyDate { get; set; } // ApplyDate
		/// <summary>借款金额（元）</summary>	
		[Description("借款金额（元）")]
        public decimal? LoanValue { get; set; } // LoanValue
		/// <summary>款项用途</summary>	
		[Description("款项用途")]
        public string Reason { get; set; } // Reason
		/// <summary>相关项目</summary>	
		[Description("相关项目")]
        public string ProjectInfo { get; set; } // ProjectInfo
		/// <summary>相关项目名称</summary>	
		[Description("相关项目名称")]
        public string ProjectInfoName { get; set; } // ProjectInfoName
		/// <summary>说明</summary>	
		[Description("说明")]
        public string Remark { get; set; } // Remark
		/// <summary>附件</summary>	
		[Description("附件")]
        public string Attachment { get; set; } // Attachment
		/// <summary>实际借出金额（元）</summary>	
		[Description("实际借出金额（元）")]
        public decimal? FactValue { get; set; } // FactValue
		/// <summary>申请金额大写</summary>	
		[Description("申请金额大写")]
        public string LoanValueDX { get; set; } // LoanValueDX
		/// <summary>实际借出大写</summary>	
		[Description("实际借出大写")]
        public string FactValueDX { get; set; } // FactValueDX
		/// <summary>关联出差申请单</summary>	
		[Description("关联出差申请单")]
        public string TravelApply { get; set; } // TravelApply
		/// <summary>关联出差申请单名称</summary>	
		[Description("关联出差申请单名称")]
        public string TravelApplyName { get; set; } // TravelApplyName
    }

	/// <summary>报销单</summary>	
	[Description("报销单")]
    public partial class T_F_ReimbursementApply : Formula.BaseModel
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
		/// <summary>申请人部门</summary>	
		[Description("申请人部门")]
        public string ApplyDept { get; set; } // ApplyDept
		/// <summary>申请人部门名称</summary>	
		[Description("申请人部门名称")]
        public string ApplyDeptName { get; set; } // ApplyDeptName
		/// <summary>类型</summary>	
		[Description("类型")]
        public string ReimbursementType { get; set; } // ReimbursementType
		/// <summary>报销形式</summary>	
		[Description("报销形式")]
        public string ReimbursementClass { get; set; } // ReimbursementClass
		/// <summary>相关责任部门</summary>	
		[Description("相关责任部门")]
        public string DeptInfo { get; set; } // DeptInfo
		/// <summary>相关责任部门名称</summary>	
		[Description("相关责任部门名称")]
        public string DeptInfoName { get; set; } // DeptInfoName
		/// <summary>关联出差申请单</summary>	
		[Description("关联出差申请单")]
        public string TravelApplyInfo { get; set; } // TravelApplyInfo
		/// <summary>关联出差申请单</summary>	
		[Description("关联出差申请单")]
        public string TravelApplyInfoName { get; set; } // TravelApplyInfoName
		/// <summary>实际支付金额</summary>	
		[Description("实际支付金额")]
        public decimal? FactPaymentValue { get; set; } // FactPaymentValue
		/// <summary>报销金额总计</summary>	
		[Description("报销金额总计")]
        public decimal? ReimbursementValue { get; set; } // ReimbursementValue
		/// <summary>申请支付金额</summary>	
		[Description("申请支付金额")]
        public decimal? ApplyValue { get; set; } // ApplyValue
		/// <summary>欠款</summary>	
		[Description("欠款")]
        public decimal? LoanValue { get; set; } // LoanValue
		/// <summary>附件</summary>	
		[Description("附件")]
        public string Attachments { get; set; } // Attachments
		/// <summary>备注</summary>	
		[Description("备注")]
        public string Remark { get; set; } // Remark
		/// <summary>报销单编号</summary>	
		[Description("报销单编号")]
        public string SerialNumber { get; set; } // SerialNumber
		/// <summary>关联项目</summary>	
		[Description("关联项目")]
        public string ProjectInfo { get; set; } // ProjectInfo
		/// <summary>项目</summary>	
		[Description("项目")]
        public string ProjectInfoName { get; set; } // ProjectInfoName
		/// <summary>明细</summary>	
		[Description("明细")]
        public string Details { get; set; } // Details

        // Reverse navigation
		[JsonIgnore]
        public virtual ICollection<T_F_ReimbursementApply_Details> T_F_ReimbursementApply_Details { get { onT_F_ReimbursementApply_DetailsGetting(); return _T_F_ReimbursementApply_Details;} }
		private ICollection<T_F_ReimbursementApply_Details> _T_F_ReimbursementApply_Details;
		partial void onT_F_ReimbursementApply_DetailsGetting();


        public T_F_ReimbursementApply()
        {
            _T_F_ReimbursementApply_Details = new List<T_F_ReimbursementApply_Details>();
        }
    }

	/// <summary>明细</summary>	
	[Description("明细")]
    public partial class T_F_ReimbursementApply_Details : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string T_F_ReimbursementApplyID { get; set; } // T_F_ReimbursementApplyID
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string IsReleased { get; set; } // IsReleased
		/// <summary>项目名称</summary>	
		[Description("项目名称")]
        public string ProjectInfo { get; set; } // ProjectInfo
		/// <summary>项目名称名称</summary>	
		[Description("项目名称名称")]
        public string ProjectInfoName { get; set; } // ProjectInfoName
		/// <summary>费用科目</summary>	
		[Description("费用科目")]
        public string SubjectCode { get; set; } // SubjectCode
		/// <summary>申报金额（元）</summary>	
		[Description("申报金额（元）")]
        public decimal? ApplyValue { get; set; } // ApplyValue
		/// <summary>备注</summary>	
		[Description("备注")]
        public string Remark { get; set; } // Remark
		/// <summary>ProjectType</summary>	
		[Description("ProjectType")]
        public string ProjectType { get; set; } // ProjectType
		/// <summary>部门</summary>	
		[Description("部门")]
        public string Dept { get; set; } // Dept
		/// <summary>部门名称</summary>	
		[Description("部门名称")]
        public string DeptName { get; set; } // DeptName

        // Foreign keys
		[JsonIgnore]
        public virtual T_F_ReimbursementApply T_F_ReimbursementApply { get; set; } //  T_F_ReimbursementApplyID - FK_T_F_ReimbursementApply_Details_T_F_ReimbursementApply
    }

	/// <summary>测试列表定义</summary>	
	[Description("测试列表定义")]
    public partial class T_HC_Test : Formula.BaseModel
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
        public string 名称 { get; set; } // 名称
		/// <summary>SortIndex</summary>	
		[Description("SortIndex")]
        public double? 排序 { get; set; } // 排序
		/// <summary>名称</summary>	
		[Description("名称")]
        public string Name { get; set; } // Name
		/// <summary>排序</summary>	
		[Description("排序")]
        public double? SortIndex { get; set; } // SortIndex
    }

	/// <summary>会议申请</summary>	
	[Description("会议申请")]
    public partial class T_M_ConferenceApply : Formula.BaseModel
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
		/// <summary>会议主题</summary>	
		[Description("会议主题")]
        public string Title { get; set; } // Title
		/// <summary>主办部门</summary>	
		[Description("主办部门")]
        public string HostDept { get; set; } // HostDept
		/// <summary>主办部门名称</summary>	
		[Description("主办部门名称")]
        public string HostDeptName { get; set; } // HostDeptName
		/// <summary>申请人</summary>	
		[Description("申请人")]
        public string ApplyUser { get; set; } // ApplyUser
		/// <summary>申请人名称</summary>	
		[Description("申请人名称")]
        public string ApplyUserName { get; set; } // ApplyUserName
		/// <summary>申请时间</summary>	
		[Description("申请时间")]
        public DateTime? ApplyDate { get; set; } // ApplyDate
		/// <summary>会议开始日期</summary>	
		[Description("会议开始日期")]
        public DateTime? MeetingStart { get; set; } // MeetingStart
		/// <summary>会议结束日期</summary>	
		[Description("会议结束日期")]
        public DateTime? MeetingEnd { get; set; } // MeetingEnd
		/// <summary>会议开始时间</summary>	
		[Description("会议开始时间")]
        public string MeetingStartHour { get; set; } // MeetingStartHour
		/// <summary>会议结束时间</summary>	
		[Description("会议结束时间")]
        public string MeetingEndHour { get; set; } // MeetingEndHour
		/// <summary>会议室</summary>	
		[Description("会议室")]
        public string MeetingRoom { get; set; } // MeetingRoom
		/// <summary>会议室名称</summary>	
		[Description("会议室名称")]
        public string MeetingRoomName { get; set; } // MeetingRoomName
		/// <summary>会议地点</summary>	
		[Description("会议地点")]
        public string RoomAddress { get; set; } // RoomAddress
		/// <summary>主持人</summary>	
		[Description("主持人")]
        public string Host { get; set; } // Host
		/// <summary>主要内容</summary>	
		[Description("主要内容")]
        public string MainContent { get; set; } // MainContent
		/// <summary>参加人员</summary>	
		[Description("参加人员")]
        public string JoinUser { get; set; } // JoinUser
		/// <summary>参加人员名称</summary>	
		[Description("参加人员名称")]
        public string JoinUserName { get; set; } // JoinUserName
		/// <summary>是否有会场需求</summary>	
		[Description("是否有会场需求")]
        public string IsNeedBanner { get; set; } // IsNeedBanner
		/// <summary>我方人数</summary>	
		[Description("我方人数")]
        public int? SelfTotal { get; set; } // SelfTotal
		/// <summary>他方人数</summary>	
		[Description("他方人数")]
        public int? GuestTotal { get; set; } // GuestTotal
		/// <summary>我方领导数</summary>	
		[Description("我方领导数")]
        public int? SelfLeader { get; set; } // SelfLeader
		/// <summary>他方领导数</summary>	
		[Description("他方领导数")]
        public int? GuestLeader { get; set; } // GuestLeader
		/// <summary>备注</summary>	
		[Description("备注")]
        public string Remark { get; set; } // Remark
		/// <summary>相关资料附件</summary>	
		[Description("相关资料附件")]
        public string Attachments { get; set; } // Attachments
		/// <summary>审批意见</summary>	
		[Description("审批意见")]
        public string AuditorInfo { get; set; } // AuditorInfo
		/// <summary>会议室管理员办理结果</summary>	
		[Description("会议室管理员办理结果")]
        public string MeetingAdmin { get; set; } // MeetingAdmin
		/// <summary>结束分钟</summary>	
		[Description("结束分钟")]
        public int? MeetingEndMin { get; set; } // MeetingEndMin
		/// <summary>开始分钟</summary>	
		[Description("开始分钟")]
        public int? MeetingStartMin { get; set; } // MeetingStartMin
		/// <summary>主持人名称</summary>	
		[Description("主持人名称")]
        public string HostName { get; set; } // HostName
		/// <summary>状态</summary>	
		[Description("状态")]
        public string State { get; set; } // State
		/// <summary>会议室最大使用人数</summary>	
		[Description("会议室最大使用人数")]
        public string RoomCapacity { get; set; } // RoomCapacity
    }

	/// <summary>T</summary>	
	[Description("T")]
    public partial class T_M_ConferenceRegist : Formula.BaseModel
    {
		/// <summary>ID</summary>	
		[Description("ID")]
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
        public string PrjID { get; set; } // PrjID
		/// <summary></summary>	
		[Description("")]
        public string FlowPhase { get; set; } // FlowPhase
		/// <summary></summary>	
		[Description("")]
        public string StepName { get; set; } // StepName
		/// <summary>会议主题</summary>	
		[Description("会议主题")]
        public string MeetApplyID { get; set; } // MeetApplyID
		/// <summary>会议主题名称</summary>	
		[Description("会议主题名称")]
        public string MeetApplyIDName { get; set; } // MeetApplyIDName
		/// <summary>主办部门</summary>	
		[Description("主办部门")]
        public string HostDeptID { get; set; } // HostDeptID
		/// <summary>主办部门名称</summary>	
		[Description("主办部门名称")]
        public string HostDeptIDName { get; set; } // HostDeptIDName
		/// <summary>申请人</summary>	
		[Description("申请人")]
        public string ApplyUserID { get; set; } // ApplyUserID
		/// <summary>申请人名称</summary>	
		[Description("申请人名称")]
        public string ApplyUserIDName { get; set; } // ApplyUserIDName
		/// <summary>计划举办时间</summary>	
		[Description("计划举办时间")]
        public DateTime? PlanMeetingDate { get; set; } // PlanMeetingDate
		/// <summary>计划会议地点</summary>	
		[Description("计划会议地点")]
        public string PlanMeetingPlace { get; set; } // PlanMeetingPlace
		/// <summary>编制日期</summary>	
		[Description("编制日期")]
        public DateTime? RegistDate { get; set; } // RegistDate
		/// <summary>编制人</summary>	
		[Description("编制人")]
        public string RegistUserID { get; set; } // RegistUserID
		/// <summary>编制人名称</summary>	
		[Description("编制人名称")]
        public string RegistUserIDName { get; set; } // RegistUserIDName
		/// <summary>计划参会人员</summary>	
		[Description("计划参会人员")]
        public string PlanJoinUserID { get; set; } // PlanJoinUserID
		/// <summary>计划参会人员名称</summary>	
		[Description("计划参会人员名称")]
        public string PlanJoinUserIDName { get; set; } // PlanJoinUserIDName
		/// <summary>实际举办时间起</summary>	
		[Description("实际举办时间起")]
        public DateTime? MeetingStartDate { get; set; } // MeetingStartDate
		/// <summary>实际举办时间止</summary>	
		[Description("实际举办时间止")]
        public DateTime? MeetingEndDate { get; set; } // MeetingEndDate
		/// <summary>实际举办地址</summary>	
		[Description("实际举办地址")]
        public string MettingPlace { get; set; } // MettingPlace
		/// <summary>实际参加人员</summary>	
		[Description("实际参加人员")]
        public string JoinUserID { get; set; } // JoinUserID
		/// <summary>实际参加人员名称</summary>	
		[Description("实际参加人员名称")]
        public string JoinUserIDName { get; set; } // JoinUserIDName
		/// <summary>大字段</summary>	
		[Description("大字段")]
        public string MeetCost { get; set; } // MeetCost
		/// <summary>附件</summary>	
		[Description("附件")]
        public string Attachment { get; set; } // Attachment
    }

	/// <summary>T</summary>	
	[Description("T")]
    public partial class T_M_ConferenceRoom : Formula.BaseModel
    {
		/// <summary>ID</summary>	
		[Description("ID")]
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
        public string PrjID { get; set; } // PrjID
		/// <summary></summary>	
		[Description("")]
        public string FlowPhase { get; set; } // FlowPhase
		/// <summary></summary>	
		[Description("")]
        public string StepName { get; set; } // StepName
		/// <summary>会议室编号</summary>	
		[Description("会议室编号")]
        public string RoomName { get; set; } // RoomName
		/// <summary>联系人</summary>	
		[Description("联系人")]
        public string LinkName { get; set; } // LinkName
		/// <summary>最大使用人数</summary>	
		[Description("最大使用人数")]
        public int? Capacity { get; set; } // Capacity
		/// <summary>管理部门</summary>	
		[Description("管理部门")]
        public string ManageDeptID { get; set; } // ManageDeptID
		/// <summary>管理部门名称</summary>	
		[Description("管理部门名称")]
        public string ManageDeptIDName { get; set; } // ManageDeptIDName
		/// <summary>会议室地址</summary>	
		[Description("会议室地址")]
        public string RoomAddress { get; set; } // RoomAddress
		/// <summary>设施情况</summary>	
		[Description("设施情况")]
        public string Configuredevice { get; set; } // Configuredevice
    }

	/// <summary>T</summary>	
	[Description("T")]
    public partial class T_M_ConferenceSummary : Formula.BaseModel
    {
		/// <summary>ID</summary>	
		[Description("ID")]
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
        public string PrjID { get; set; } // PrjID
		/// <summary></summary>	
		[Description("")]
        public string FlowPhase { get; set; } // FlowPhase
		/// <summary></summary>	
		[Description("")]
        public string StepName { get; set; } // StepName
		/// <summary>会议主题</summary>	
		[Description("会议主题")]
        public string Title { get; set; } // Title
		/// <summary>会议主题名称</summary>	
		[Description("会议主题名称")]
        public string TitleName { get; set; } // TitleName
		/// <summary>参加人员</summary>	
		[Description("参加人员")]
        public string JoinUserID { get; set; } // JoinUserID
		/// <summary>参加人员名称</summary>	
		[Description("参加人员名称")]
        public string JoinUserIDName { get; set; } // JoinUserIDName
		/// <summary>主办部门</summary>	
		[Description("主办部门")]
        public string HostDepID { get; set; } // HostDepID
		/// <summary>主办部门名称</summary>	
		[Description("主办部门名称")]
        public string HostDepIDName { get; set; } // HostDepIDName
		/// <summary>主持人</summary>	
		[Description("主持人")]
        public string HostUserID { get; set; } // HostUserID
		/// <summary>主持人名称</summary>	
		[Description("主持人名称")]
        public string HostUserIDName { get; set; } // HostUserIDName
		/// <summary>会议日期</summary>	
		[Description("会议日期")]
        public DateTime? MeetingStart { get; set; } // MeetingStart
		/// <summary>记录人</summary>	
		[Description("记录人")]
        public string RecordUserID { get; set; } // RecordUserID
		/// <summary>记录人名称</summary>	
		[Description("记录人名称")]
        public string RecordUserIDName { get; set; } // RecordUserIDName
		/// <summary>会议纪要</summary>	
		[Description("会议纪要")]
        public string MeetingSummary { get; set; } // MeetingSummary
		/// <summary>相关附件</summary>	
		[Description("相关附件")]
        public string AboutInfomation { get; set; } // AboutInfomation
		/// <summary>是否需要审签</summary>	
		[Description("是否需要审签")]
        public string IsNeedSigned { get; set; } // IsNeedSigned
		/// <summary>审签人</summary>	
		[Description("审签人")]
        public string ApproveUserIDs { get; set; } // ApproveUserIDs
		/// <summary>审签人名称</summary>	
		[Description("审签人名称")]
        public string ApproveUserIDsName { get; set; } // ApproveUserIDsName
		/// <summary>批准人</summary>	
		[Description("批准人")]
        public string RatifyUserIDs { get; set; } // RatifyUserIDs
		/// <summary>批准人名称</summary>	
		[Description("批准人名称")]
        public string RatifyUserIDsName { get; set; } // RatifyUserIDsName
		/// <summary>主要内容</summary>	
		[Description("主要内容")]
        public string MainContent { get; set; } // MainContent
		/// <summary>详细内容</summary>	
		[Description("详细内容")]
        public string DetailContent { get; set; } // DetailContent
		/// <summary>审签人意见</summary>	
		[Description("审签人意见")]
        public string CountersignederComment { get; set; } // CountersignederComment
		/// <summary>批准人意见</summary>	
		[Description("批准人意见")]
        public string ApprovalerComment { get; set; } // ApprovalerComment
    }

	/// <summary>T</summary>	
	[Description("T")]
    public partial class T_M_OuterConference : Formula.BaseModel
    {
		/// <summary>ID</summary>	
		[Description("ID")]
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
        public string PrjID { get; set; } // PrjID
		/// <summary></summary>	
		[Description("")]
        public string FlowPhase { get; set; } // FlowPhase
		/// <summary></summary>	
		[Description("")]
        public string StepName { get; set; } // StepName
		/// <summary>会议名称</summary>	
		[Description("会议名称")]
        public string MeetingName { get; set; } // MeetingName
		/// <summary>会议举办时间</summary>	
		[Description("会议举办时间")]
        public DateTime? MeetingDate { get; set; } // MeetingDate
		/// <summary>会议举办地址</summary>	
		[Description("会议举办地址")]
        public string MeetingPlace { get; set; } // MeetingPlace
		/// <summary>会议主办单位</summary>	
		[Description("会议主办单位")]
        public string MeetingOrganizers { get; set; } // MeetingOrganizers
		/// <summary>会议承办部门</summary>	
		[Description("会议承办部门")]
        public string MeetingDeptName { get; set; } // MeetingDeptName
		/// <summary>会议承办部门名称</summary>	
		[Description("会议承办部门名称")]
        public string MeetingDeptNameName { get; set; } // MeetingDeptNameName
		/// <summary>参加会议人员构成和人数</summary>	
		[Description("参加会议人员构成和人数")]
        public string JoinUserNames { get; set; } // JoinUserNames
		/// <summary>会议预算大字段</summary>	
		[Description("会议预算大字段")]
        public string CostGrid { get; set; } // CostGrid
		/// <summary>合计</summary>	
		[Description("合计")]
        public decimal? TotalMoney { get; set; } // TotalMoney
		/// <summary>预算编制人签名</summary>	
		[Description("预算编制人签名")]
        public string ApplyUserComment { get; set; } // ApplyUserComment
		/// <summary>部门主任意见</summary>	
		[Description("部门主任意见")]
        public string DeptLeaderComment { get; set; } // DeptLeaderComment
		/// <summary>财务会计意见</summary>	
		[Description("财务会计意见")]
        public string FinancialAccountingComment { get; set; } // FinancialAccountingComment
		/// <summary>财务主任意见</summary>	
		[Description("财务主任意见")]
        public string FinancialLeaderComment { get; set; } // FinancialLeaderComment
		/// <summary>分管总经理意见</summary>	
		[Description("分管总经理意见")]
        public string DepartLeaderComment { get; set; } // DepartLeaderComment
		/// <summary>总经理意见</summary>	
		[Description("总经理意见")]
        public string GeneralManagerComment { get; set; } // GeneralManagerComment
		/// <summary>借款金额(元)</summary>	
		[Description("借款金额(元)")]
        public decimal? BorrowMoney { get; set; } // BorrowMoney
		/// <summary>借款日期</summary>	
		[Description("借款日期")]
        public DateTime? BorrowMoneyDate { get; set; } // BorrowMoneyDate
		/// <summary>报销金额(元)</summary>	
		[Description("报销金额(元)")]
        public decimal? ReimbursedMoney { get; set; } // ReimbursedMoney
		/// <summary>报销日期</summary>	
		[Description("报销日期")]
        public DateTime? ReimbursedMoneyDate { get; set; } // ReimbursedMoneyDate

        // Reverse navigation
		[JsonIgnore]
        public virtual ICollection<T_M_OuterConference_CostGrid> T_M_OuterConference_CostGrid { get { onT_M_OuterConference_CostGridGetting(); return _T_M_OuterConference_CostGrid;} }
		private ICollection<T_M_OuterConference_CostGrid> _T_M_OuterConference_CostGrid;
		partial void onT_M_OuterConference_CostGridGetting();


        public T_M_OuterConference()
        {
            _T_M_OuterConference_CostGrid = new List<T_M_OuterConference_CostGrid>();
        }
    }

	/// <summary>会议预算大字段</summary>	
	[Description("会议预算大字段")]
    public partial class T_M_OuterConference_CostGrid : Formula.BaseModel
    {
		/// <summary>ID</summary>	
		[Description("ID")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary>T_MOuterConferenceID</summary>	
		[Description("T_MOuterConferenceID")]
        public string T_M_OuterConferenceID { get; set; } // T_M_OuterConferenceID
		/// <summary>排序号</summary>	
		[Description("排序号")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary>已发布</summary>	
		[Description("已发布")]
        public string IsReleased { get; set; } // IsReleased
		/// <summary>项目</summary>	
		[Description("项目")]
        public string Project { get; set; } // Project
		/// <summary>标准(单价:元)</summary>	
		[Description("标准(单价:元)")]
        public double? Price { get; set; } // Price
		/// <summary>数量</summary>	
		[Description("数量")]
        public double? Amount { get; set; } // Amount
		/// <summary>费用小计(元)</summary>	
		[Description("费用小计(元)")]
        public double? Subtotal { get; set; } // Subtotal
		/// <summary>备注</summary>	
		[Description("备注")]
        public string Remarks { get; set; } // Remarks

        // Foreign keys
		[JsonIgnore]
        public virtual T_M_OuterConference T_M_OuterConference { get; set; } //  T_M_OuterConferenceID - FK_T_MOuterConference_MeetingCostList_T_MOuterConference
    }

	/// <summary>成果类文件用印申请</summary>	
	[Description("成果类文件用印申请")]
    public partial class T_ProductFileSeal : Formula.BaseModel
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
        public string XMMC { get; set; } // XMMC
		/// <summary>项目编号</summary>	
		[Description("项目编号")]
        public string XMBH { get; set; } // XMBH
		/// <summary>是否有合同</summary>	
		[Description("是否有合同")]
        public string SFYHT { get; set; } // SFYHT
		/// <summary>申请用印日期</summary>	
		[Description("申请用印日期")]
        public DateTime? SQYYRQ { get; set; } // SQYYRQ
		/// <summary>打回次数</summary>	
		[Description("打回次数")]
        public int? DHCS { get; set; } // DHCS
		/// <summary>经办人</summary>	
		[Description("经办人")]
        public string JBR { get; set; } // JBR
		/// <summary>经办人名称</summary>	
		[Description("经办人名称")]
        public string JBRName { get; set; } // JBRName
		/// <summary>经办人联系电话</summary>	
		[Description("经办人联系电话")]
        public string JBRLXDH { get; set; } // JBRLXDH
		/// <summary>设计阶段</summary>	
		[Description("设计阶段")]
        public string SJJD { get; set; } // SJJD
		/// <summary>申请册数</summary>	
		[Description("申请册数")]
        public int? SQCS { get; set; } // SQCS
		/// <summary>申请份数</summary>	
		[Description("申请份数")]
        public int? SQFS { get; set; } // SQFS
		/// <summary>文件名称</summary>	
		[Description("文件名称")]
        public string WJMC { get; set; } // WJMC
		/// <summary>实际册数</summary>	
		[Description("实际册数")]
        public int? SJCS { get; set; } // SJCS
		/// <summary>实际份数</summary>	
		[Description("实际份数")]
        public int? SJFS { get; set; } // SJFS
		/// <summary>文件用途</summary>	
		[Description("文件用途")]
        public string WJYT { get; set; } // WJYT
		/// <summary>用印单位</summary>	
		[Description("用印单位")]
        public string YYDW { get; set; } // YYDW
		/// <summary>用印单位名称</summary>	
		[Description("用印单位名称")]
        public string YYDWName { get; set; } // YYDWName
		/// <summary>用印人</summary>	
		[Description("用印人")]
        public string YYR { get; set; } // YYR
		/// <summary>用印人</summary>	
		[Description("用印人")]
        public string YYRName { get; set; } // YYRName
		/// <summary>部门</summary>	
		[Description("部门")]
        public string BM { get; set; } // BM
		/// <summary>部门名称</summary>	
		[Description("部门名称")]
        public string BMName { get; set; } // BMName
		/// <summary>用印合规性</summary>	
		[Description("用印合规性")]
        public string YYHGX { get; set; } // YYHGX
		/// <summary>二级部门</summary>	
		[Description("二级部门")]
        public string EJBM { get; set; } // EJBM
		/// <summary>二级部门名称</summary>	
		[Description("二级部门名称")]
        public string EJBMName { get; set; } // EJBMName
		/// <summary>审定人专业</summary>	
		[Description("审定人专业")]
        public string SDRZY { get; set; } // SDRZY
		/// <summary>印章名称</summary>	
		[Description("印章名称")]
        public string YZMC { get; set; } // YZMC
		/// <summary>有关技术文件</summary>	
		[Description("有关技术文件")]
        public string YGJSWJ { get; set; } // YGJSWJ
		/// <summary>有关技术文件详情</summary>	
		[Description("有关技术文件详情")]
        public string YGJSWJZB { get; set; } // YGJSWJZB
		/// <summary>备注</summary>	
		[Description("备注")]
        public string BZ { get; set; } // BZ
		/// <summary>打回原因</summary>	
		[Description("打回原因")]
        public string DHYY { get; set; } // DHYY
		/// <summary>驳回次数</summary>	
		[Description("驳回次数")]
        public int? BHCS { get; set; } // BHCS
		/// <summary>科技质量部意见</summary>	
		[Description("科技质量部意见")]
        public string KJZLBYJ { get; set; } // KJZLBYJ
		/// <summary>科技质量部领导批准日期</summary>	
		[Description("科技质量部领导批准日期")]
        public DateTime? KJZLBLDPZRQ { get; set; } // KJZLBLDPZRQ
		/// <summary>是否总院章</summary>	
		[Description("是否总院章")]
        public string IsParentSeal { get; set; } // IsParentSeal
		/// <summary>所属公司</summary>	
		[Description("所属公司")]
        public string BelongDept { get; set; } // BelongDept
		/// <summary>项目性质</summary>	
		[Description("项目性质")]
        public string ProjectSpecialty { get; set; } // ProjectSpecialty
		/// <summary>成果ID</summary>	
		[Description("成果ID")]
        public string S_E_ProductID { get; set; } // S_E_ProductID
		/// <summary>是否项目负责人</summary>	
		[Description("是否项目负责人")]
        public string IsManager { get; set; } // IsManager
		/// <summary>部门领导</summary>	
		[Description("部门领导")]
        public string leader { get; set; } // leader
		/// <summary>页面号</summary>	
		[Description("页面号")]
        public string FormCode { get; set; } // FormCode
		/// <summary>申请印章份数</summary>	
		[Description("申请印章份数")]
        public double? SealNum { get; set; } // SealNum
		/// <summary>用章总页数</summary>	
		[Description("用章总页数")]
        public double? SealNumAll { get; set; } // SealNumAll
		/// <summary>实际印章份数</summary>	
		[Description("实际印章份数")]
        public double? RealSealNum { get; set; } // RealSealNum
		/// <summary>实际用章总页数</summary>	
		[Description("实际用章总页数")]
        public double? RealSealNumAll { get; set; } // RealSealNumAll
		/// <summary>用章详情（用章材料及用途说明）</summary>	
		[Description("用章详情（用章材料及用途说明）")]
        public string SealDetail { get; set; } // SealDetail
		/// <summary>合同编号</summary>	
		[Description("合同编号")]
        public string ContractNo { get; set; } // ContractNo
		/// <summary>是否设计变更</summary>	
		[Description("是否设计变更")]
        public string IsDesignChange { get; set; } // IsDesignChange
		/// <summary>一级选项</summary>	
		[Description("一级选项")]
        public string FirstChoose { get; set; } // FirstChoose
		/// <summary>二级选项</summary>	
		[Description("二级选项")]
        public string SecondChoose { get; set; } // SecondChoose
		/// <summary>附件</summary>	
		[Description("附件")]
        public string Attachments { get; set; } // Attachments
		/// <summary>其他原因</summary>	
		[Description("其他原因")]
        public string OtherReason { get; set; } // OtherReason
		/// <summary>其他</summary>	
		[Description("其他")]
        public string OtherForSecond { get; set; } // OtherForSecond
		/// <summary>用印成果信息</summary>	
		[Description("用印成果信息")]
        public string SealProductInfo { get; set; } // SealProductInfo

        // Reverse navigation
		[JsonIgnore]
        public virtual ICollection<T_ProductFileSeal_CGXX> T_ProductFileSeal_CGXX { get { onT_ProductFileSeal_CGXXGetting(); return _T_ProductFileSeal_CGXX;} }
		private ICollection<T_ProductFileSeal_CGXX> _T_ProductFileSeal_CGXX;
		partial void onT_ProductFileSeal_CGXXGetting();

		[JsonIgnore]
        public virtual ICollection<T_ProductFileSeal_SealProductInfo> T_ProductFileSeal_SealProductInfo { get { onT_ProductFileSeal_SealProductInfoGetting(); return _T_ProductFileSeal_SealProductInfo;} }
		private ICollection<T_ProductFileSeal_SealProductInfo> _T_ProductFileSeal_SealProductInfo;
		partial void onT_ProductFileSeal_SealProductInfoGetting();

		[JsonIgnore]
        public virtual ICollection<T_ProductFileSeal_YGJSWJZB> T_ProductFileSeal_YGJSWJZB { get { onT_ProductFileSeal_YGJSWJZBGetting(); return _T_ProductFileSeal_YGJSWJZB;} }
		private ICollection<T_ProductFileSeal_YGJSWJZB> _T_ProductFileSeal_YGJSWJZB;
		partial void onT_ProductFileSeal_YGJSWJZBGetting();


        public T_ProductFileSeal()
        {
            _T_ProductFileSeal_CGXX = new List<T_ProductFileSeal_CGXX>();
            _T_ProductFileSeal_SealProductInfo = new List<T_ProductFileSeal_SealProductInfo>();
            _T_ProductFileSeal_YGJSWJZB = new List<T_ProductFileSeal_YGJSWJZB>();
        }
    }

	/// <summary>成果信息</summary>	
	[Description("成果信息")]
    public partial class T_ProductFileSeal_CGXX : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string T_ProductFileSealID { get; set; } // T_ProductFileSealID
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string IsReleased { get; set; } // IsReleased
		/// <summary>编号</summary>	
		[Description("编号")]
        public string CGBH { get; set; } // CGBH
		/// <summary>文件名称</summary>	
		[Description("文件名称")]
        public string ProductFile { get; set; } // ProductFile
		/// <summary>文件名称名称</summary>	
		[Description("文件名称名称")]
        public string ProductFileName { get; set; } // ProductFileName
		/// <summary>文件类型</summary>	
		[Description("文件类型")]
        public string FileType { get; set; } // FileType
		/// <summary>阶段</summary>	
		[Description("阶段")]
        public string JD { get; set; } // JD
		/// <summary>成果名称</summary>	
		[Description("成果名称")]
        public string CGMC { get; set; } // CGMC
		/// <summary>单体</summary>	
		[Description("单体")]
        public string DT { get; set; } // DT
		/// <summary>类型</summary>	
		[Description("类型")]
        public string LX { get; set; } // LX
		/// <summary>版本号</summary>	
		[Description("版本号")]
        public string BBH { get; set; } // BBH
		/// <summary>校审状态</summary>	
		[Description("校审状态")]
        public string XSZT { get; set; } // XSZT
		/// <summary>会签状态</summary>	
		[Description("会签状态")]
        public string HQZT { get; set; } // HQZT
		/// <summary>提交人</summary>	
		[Description("提交人")]
        public string TJR { get; set; } // TJR
		/// <summary>提交日期</summary>	
		[Description("提交日期")]
        public string TJRQ { get; set; } // TJRQ

        // Foreign keys
		[JsonIgnore]
        public virtual T_ProductFileSeal T_ProductFileSeal { get; set; } //  T_ProductFileSealID - FK_T_ProductFileSeal_CGXX_T_ProductFileSeal
    }

	/// <summary>用印成果信息</summary>	
	[Description("用印成果信息")]
    public partial class T_ProductFileSeal_SealProductInfo : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string T_ProductFileSealID { get; set; } // T_ProductFileSealID
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string IsReleased { get; set; } // IsReleased
		/// <summary>成果编号</summary>	
		[Description("成果编号")]
        public string CGBH { get; set; } // CGBH
		/// <summary>成果名称</summary>	
		[Description("成果名称")]
        public string CGMC { get; set; } // CGMC
		/// <summary>阶段</summary>	
		[Description("阶段")]
        public string JD { get; set; } // JD
		/// <summary>单体</summary>	
		[Description("单体")]
        public string DT { get; set; } // DT
		/// <summary>类型</summary>	
		[Description("类型")]
        public string LX { get; set; } // LX
		/// <summary>版本号</summary>	
		[Description("版本号")]
        public string BBH { get; set; } // BBH
		/// <summary>成果附件</summary>	
		[Description("成果附件")]
        public string MainFile { get; set; } // MainFile
		/// <summary>成果附件名称</summary>	
		[Description("成果附件名称")]
        public string MainFileName { get; set; } // MainFileName
		/// <summary>校审状态</summary>	
		[Description("校审状态")]
        public string XSZT { get; set; } // XSZT
		/// <summary>会签状态</summary>	
		[Description("会签状态")]
        public string HQZT { get; set; } // HQZT
		/// <summary>提交人</summary>	
		[Description("提交人")]
        public string TJR { get; set; } // TJR
		/// <summary>提交日期</summary>	
		[Description("提交日期")]
        public DateTime? TJRQ { get; set; } // TJRQ

        // Foreign keys
		[JsonIgnore]
        public virtual T_ProductFileSeal T_ProductFileSeal { get; set; } //  T_ProductFileSealID - FK_T_ProductFileSeal_SealProductInfo_T_ProductFileSeal
    }

	/// <summary>有关技术文件详情</summary>	
	[Description("有关技术文件详情")]
    public partial class T_ProductFileSeal_YGJSWJZB : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string T_ProductFileSealID { get; set; } // T_ProductFileSealID
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string IsReleased { get; set; } // IsReleased
		/// <summary>名称</summary>	
		[Description("名称")]
        public string YGJSWJMC { get; set; } // YGJSWJMC
		/// <summary>附件</summary>	
		[Description("附件")]
        public string YGJSWJFJ { get; set; } // YGJSWJFJ
		/// <summary>附件名称</summary>	
		[Description("附件名称")]
        public string YGJSWJFJName { get; set; } // YGJSWJFJName

        // Foreign keys
		[JsonIgnore]
        public virtual T_ProductFileSeal T_ProductFileSeal { get; set; } //  T_ProductFileSealID - FK_T_ProductFileSeal_YGJSWJZB_T_ProductFileSeal
    }

	/// <summary>用印申请单</summary>	
	[Description("用印申请单")]
    public partial class T_Seal_Apply : Formula.BaseModel
    {
		/// <summary>ID</summary>	
		[Description("ID")]
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
        public string PrjID { get; set; } // PrjID
		/// <summary></summary>	
		[Description("")]
        public string FlowPhase { get; set; } // FlowPhase
		/// <summary></summary>	
		[Description("")]
        public string StepName { get; set; } // StepName
		/// <summary>部门1</summary>	
		[Description("部门1")]
        public string DeptID { get; set; } // DeptID
		/// <summary>部门1名称</summary>	
		[Description("部门1名称")]
        public string DeptIDName { get; set; } // DeptIDName
		/// <summary>申请人</summary>	
		[Description("申请人")]
        public string ApplyUserID { get; set; } // ApplyUserID
		/// <summary>申请人名称</summary>	
		[Description("申请人名称")]
        public string ApplyUserIDName { get; set; } // ApplyUserIDName
		/// <summary>申请事由</summary>	
		[Description("申请事由")]
        public string ApplyReason { get; set; } // ApplyReason
		/// <summary>用印种类</summary>	
		[Description("用印种类")]
        public string SealType { get; set; } // SealType
		/// <summary>印章全称</summary>	
		[Description("印章全称")]
        public string SealInfoID { get; set; } // SealInfoID
		/// <summary>印章全称</summary>	
		[Description("印章全称")]
        public string SealInfoIDName { get; set; } // SealInfoIDName
		/// <summary>用印份数</summary>	
		[Description("用印份数")]
        public int? SealNumber { get; set; } // SealNumber
		/// <summary>用印日期</summary>	
		[Description("用印日期")]
        public DateTime? ApplyDate { get; set; } // ApplyDate
		/// <summary>部门领导意见</summary>	
		[Description("部门领导意见")]
        public string DeptLeaderComment { get; set; } // DeptLeaderComment
		/// <summary>分管领导意见</summary>	
		[Description("分管领导意见")]
        public string ApplyDeptDepartLeaderComment { get; set; } // ApplyDeptDepartLeaderComment
		/// <summary>附件测试</summary>	
		[Description("附件测试")]
        public string FJCS { get; set; } // FJCS
		/// <summary>印章明细</summary>	
		[Description("印章明细")]
        public string Detail { get; set; } // Detail
		/// <summary>单附件</summary>	
		[Description("单附件")]
        public string DFJ { get; set; } // DFJ
		/// <summary>表单编号</summary>	
		[Description("表单编号")]
        public string SerialNumber { get; set; } // SerialNumber
		/// <summary>空白凭证</summary>	
		[Description("空白凭证")]
        public string KBPZ { get; set; } // KBPZ
		/// <summary>字表</summary>	
		[Description("字表")]
        public string XXNR { get; set; } // XXNR
		/// <summary>智能部门负责人</summary>	
		[Description("智能部门负责人")]
        public string ZNBMFZR { get; set; } // ZNBMFZR
		/// <summary>相关人员意见</summary>	
		[Description("相关人员意见")]
        public string XGRYYJ { get; set; } // XGRYYJ
		/// <summary>提出时间</summary>	
		[Description("提出时间")]
        public DateTime? TCSJ { get; set; } // TCSJ
		/// <summary>资料</summary>	
		[Description("资料")]
        public string ZL { get; set; } // ZL
		/// <summary>部门负责人</summary>	
		[Description("部门负责人")]
        public string BMFZR { get; set; } // BMFZR
		/// <summary>部门负责人名称</summary>	
		[Description("部门负责人名称")]
        public string BMFZRName { get; set; } // BMFZRName
		/// <summary>用印份数</summary>	
		[Description("用印份数")]
        public decimal? YYFS { get; set; } // YYFS
		/// <summary>单选枚举</summary>	
		[Description("单选枚举")]
        public string DXMJ { get; set; } // DXMJ
		/// <summary>测试附件</summary>	
		[Description("测试附件")]
        public string Attachment2 { get; set; } // Attachment2
		/// <summary>测试</summary>	
		[Description("测试")]
        public string Test { get; set; } // Test
		/// <summary>测试部门</summary>	
		[Description("测试部门")]
        public string CSBM { get; set; } // CSBM
		/// <summary>测试部门名称</summary>	
		[Description("测试部门名称")]
        public string CSBMName { get; set; } // CSBMName
		/// <summary>groupinfo</summary>	
		[Description("groupinfo")]
        public string groupinfo { get; set; } // groupinfo
		/// <summary>groupuserids</summary>	
		[Description("groupuserids")]
        public string groupuserids { get; set; } // groupuserids

        // Reverse navigation
		[JsonIgnore]
        public virtual ICollection<T_Seal_Apply_Detail> T_Seal_Apply_Detail { get { onT_Seal_Apply_DetailGetting(); return _T_Seal_Apply_Detail;} }
		private ICollection<T_Seal_Apply_Detail> _T_Seal_Apply_Detail;
		partial void onT_Seal_Apply_DetailGetting();


        public T_Seal_Apply()
        {
            _T_Seal_Apply_Detail = new List<T_Seal_Apply_Detail>();
        }
    }

	/// <summary>印章明细</summary>	
	[Description("印章明细")]
    public partial class T_Seal_Apply_Detail : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string T_Seal_ApplyID { get; set; } // T_Seal_ApplyID
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string IsReleased { get; set; } // IsReleased
		/// <summary>印章编号</summary>	
		[Description("印章编号")]
        public string SealCode { get; set; } // SealCode
		/// <summary>印章名称</summary>	
		[Description("印章名称")]
        public string SealName { get; set; } // SealName
		/// <summary>印章类别</summary>	
		[Description("印章类别")]
        public string SealType { get; set; } // SealType
		/// <summary>印章保管部门</summary>	
		[Description("印章保管部门")]
        public string DeptIDName { get; set; } // DeptIDName
		/// <summary>印章保管人</summary>	
		[Description("印章保管人")]
        public string KeeperIDName { get; set; } // KeeperIDName
		/// <summary>印章ID</summary>	
		[Description("印章ID")]
        public string SealID { get; set; } // SealID
		/// <summary>用印份数</summary>	
		[Description("用印份数")]
        public int? SealNumber { get; set; } // SealNumber
		/// <summary>印章保管人</summary>	
		[Description("印章保管人")]
        public string KeeperID { get; set; } // KeeperID
		/// <summary>用印日期</summary>	
		[Description("用印日期")]
        public DateTime? SealDate { get; set; } // SealDate
		/// <summary>附件</summary>	
		[Description("附件")]
        public string SealFJ { get; set; } // SealFJ
		/// <summary>附件名称</summary>	
		[Description("附件名称")]
        public string SealFJName { get; set; } // SealFJName

        // Foreign keys
		[JsonIgnore]
        public virtual T_Seal_Apply T_Seal_Apply { get; set; } //  T_Seal_ApplyID - FK_T_Seal_Apply_Detail_T_Seal_Apply
    }

	/// <summary>印章借用申请单</summary>	
	[Description("印章借用申请单")]
    public partial class T_Seal_Borrow : Formula.BaseModel
    {
		/// <summary>ID</summary>	
		[Description("ID")]
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
        public string PrjID { get; set; } // PrjID
		/// <summary></summary>	
		[Description("")]
        public string FlowPhase { get; set; } // FlowPhase
		/// <summary></summary>	
		[Description("")]
        public string StepName { get; set; } // StepName
		/// <summary>部门</summary>	
		[Description("部门")]
        public string DeptID { get; set; } // DeptID
		/// <summary>部门名称</summary>	
		[Description("部门名称")]
        public string DeptIDName { get; set; } // DeptIDName
		/// <summary>申请人</summary>	
		[Description("申请人")]
        public string ApplyUserID { get; set; } // ApplyUserID
		/// <summary>申请人名称</summary>	
		[Description("申请人名称")]
        public string ApplyUserIDName { get; set; } // ApplyUserIDName
		/// <summary>借用范畴</summary>	
		[Description("借用范畴")]
        public string BorrowRange { get; set; } // BorrowRange
		/// <summary>申请事由</summary>	
		[Description("申请事由")]
        public string ApplyReason { get; set; } // ApplyReason
		/// <summary>借用时间(起)</summary>	
		[Description("借用时间(起)")]
        public DateTime? BorrowStartDate { get; set; } // BorrowStartDate
		/// <summary>借用时间(止)</summary>	
		[Description("借用时间(止)")]
        public DateTime? BorrowEndDate { get; set; } // BorrowEndDate
		/// <summary>文件名称</summary>	
		[Description("文件名称")]
        public string FileName { get; set; } // FileName
		/// <summary>用印份数</summary>	
		[Description("用印份数")]
        public int? UseSealPaperNumber { get; set; } // UseSealPaperNumber
		/// <summary>申请日期</summary>	
		[Description("申请日期")]
        public DateTime? ApplyDate { get; set; } // ApplyDate
		/// <summary>部门负责人意见</summary>	
		[Description("部门负责人意见")]
        public string DeptLeaderComment { get; set; } // DeptLeaderComment
		/// <summary>总经理工作部意见</summary>	
		[Description("总经理工作部意见")]
        public string GeneralManagerWorkDeptComment { get; set; } // GeneralManagerWorkDeptComment
		/// <summary>分管领导意见</summary>	
		[Description("分管领导意见")]
        public string DepartLeaderComment { get; set; } // DepartLeaderComment
		/// <summary>总经理意见</summary>	
		[Description("总经理意见")]
        public string GeneralManagerComment { get; set; } // GeneralManagerComment
		/// <summary>借用印章</summary>	
		[Description("借用印章")]
        public string Seal { get; set; } // Seal
		/// <summary>借用印章名称</summary>	
		[Description("借用印章名称")]
        public string SealName { get; set; } // SealName
		/// <summary>印章类别</summary>	
		[Description("印章类别")]
        public string SealType { get; set; } // SealType
		/// <summary>借用状态</summary>	
		[Description("借用状态")]
        public string BorrowState { get; set; } // BorrowState
		/// <summary>归还时间</summary>	
		[Description("归还时间")]
        public DateTime? ReturnDate { get; set; } // ReturnDate
		/// <summary>印章明细</summary>	
		[Description("印章明细")]
        public string Detail { get; set; } // Detail

        // Reverse navigation
		[JsonIgnore]
        public virtual ICollection<T_Seal_Borrow_Detail> T_Seal_Borrow_Detail { get { onT_Seal_Borrow_DetailGetting(); return _T_Seal_Borrow_Detail;} }
		private ICollection<T_Seal_Borrow_Detail> _T_Seal_Borrow_Detail;
		partial void onT_Seal_Borrow_DetailGetting();


        public T_Seal_Borrow()
        {
            _T_Seal_Borrow_Detail = new List<T_Seal_Borrow_Detail>();
        }
    }

	/// <summary>印章明细</summary>	
	[Description("印章明细")]
    public partial class T_Seal_Borrow_Detail : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string T_Seal_BorrowID { get; set; } // T_Seal_BorrowID
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string IsReleased { get; set; } // IsReleased
		/// <summary>印章编号</summary>	
		[Description("印章编号")]
        public string SealCode { get; set; } // SealCode
		/// <summary>印章名称</summary>	
		[Description("印章名称")]
        public string SealName { get; set; } // SealName
		/// <summary>印章类别</summary>	
		[Description("印章类别")]
        public string SealType { get; set; } // SealType
		/// <summary>用印份数</summary>	
		[Description("用印份数")]
        public int? SealNumber { get; set; } // SealNumber
		/// <summary>印章保管部门</summary>	
		[Description("印章保管部门")]
        public string DeptIDName { get; set; } // DeptIDName
		/// <summary>印章保管人</summary>	
		[Description("印章保管人")]
        public string KeeperIDName { get; set; } // KeeperIDName
		/// <summary>印章ID</summary>	
		[Description("印章ID")]
        public string SealID { get; set; } // SealID

        // Foreign keys
		[JsonIgnore]
        public virtual T_Seal_Borrow T_Seal_Borrow { get; set; } //  T_Seal_BorrowID - FK_T_Seal_Borrow_Detail_T_Seal_Borrow
    }

	/// <summary>T</summary>	
	[Description("T")]
    public partial class T_Seal_Change : Formula.BaseModel
    {
		/// <summary>ID</summary>	
		[Description("ID")]
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
        public string PrjID { get; set; } // PrjID
		/// <summary></summary>	
		[Description("")]
        public string FlowPhase { get; set; } // FlowPhase
		/// <summary></summary>	
		[Description("")]
        public string StepName { get; set; } // StepName
		/// <summary>部门</summary>	
		[Description("部门")]
        public string DeptID { get; set; } // DeptID
		/// <summary>部门名称</summary>	
		[Description("部门名称")]
        public string DeptIDName { get; set; } // DeptIDName
		/// <summary>申请人</summary>	
		[Description("申请人")]
        public string ApplyUserID { get; set; } // ApplyUserID
		/// <summary>申请人名称</summary>	
		[Description("申请人名称")]
        public string ApplyUserIDName { get; set; } // ApplyUserIDName
		/// <summary>申请事由</summary>	
		[Description("申请事由")]
        public string ApplyReason { get; set; } // ApplyReason
		/// <summary>印章全称</summary>	
		[Description("印章全称")]
        public string SealID { get; set; } // SealID
		/// <summary>印章全称名称</summary>	
		[Description("印章全称名称")]
        public string SealIDName { get; set; } // SealIDName
		/// <summary>印章样式</summary>	
		[Description("印章样式")]
        public string SealStyle { get; set; } // SealStyle
		/// <summary>刻印数量(枚)</summary>	
		[Description("刻印数量(枚)")]
        public int? SealNumber { get; set; } // SealNumber
		/// <summary>申请日期</summary>	
		[Description("申请日期")]
        public DateTime? ApplyDate { get; set; } // ApplyDate
		/// <summary>单价</summary>	
		[Description("单价")]
        public decimal? SealCost { get; set; } // SealCost
		/// <summary>总费用</summary>	
		[Description("总费用")]
        public decimal? TotalCost { get; set; } // TotalCost
		/// <summary>完成时间</summary>	
		[Description("完成时间")]
        public DateTime? CompleteDate { get; set; } // CompleteDate
		/// <summary>完成人</summary>	
		[Description("完成人")]
        public string CompletePersonID { get; set; } // CompletePersonID
		/// <summary>完成人名称</summary>	
		[Description("完成人名称")]
        public string CompletePersonIDName { get; set; } // CompletePersonIDName
		/// <summary>部门负责人意见</summary>	
		[Description("部门负责人意见")]
        public string DeptLeaderComment { get; set; } // DeptLeaderComment
		/// <summary>批准人审批意见</summary>	
		[Description("批准人审批意见")]
        public string ApprovalPersonComment { get; set; } // ApprovalPersonComment
    }

	/// <summary>印章废止申请单</summary>	
	[Description("印章废止申请单")]
    public partial class T_Seal_Repeal : Formula.BaseModel
    {
		/// <summary>ID</summary>	
		[Description("ID")]
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
        public string PrjID { get; set; } // PrjID
		/// <summary></summary>	
		[Description("")]
        public string FlowPhase { get; set; } // FlowPhase
		/// <summary></summary>	
		[Description("")]
        public string StepName { get; set; } // StepName
		/// <summary>部门</summary>	
		[Description("部门")]
        public string DeptID { get; set; } // DeptID
		/// <summary>部门名称</summary>	
		[Description("部门名称")]
        public string DeptIDName { get; set; } // DeptIDName
		/// <summary>申请人</summary>	
		[Description("申请人")]
        public string ApplyUserID { get; set; } // ApplyUserID
		/// <summary>申请人名称</summary>	
		[Description("申请人名称")]
        public string ApplyUserIDName { get; set; } // ApplyUserIDName
		/// <summary>废止印章全称</summary>	
		[Description("废止印章全称")]
        public string SealInfoID { get; set; } // SealInfoID
		/// <summary>废止印章全称名称</summary>	
		[Description("废止印章全称名称")]
        public string SealInfoIDName { get; set; } // SealInfoIDName
		/// <summary>印章编号</summary>	
		[Description("印章编号")]
        public string SealCode { get; set; } // SealCode
		/// <summary>废止原因</summary>	
		[Description("废止原因")]
        public string RepealReason { get; set; } // RepealReason
		/// <summary>废止日期</summary>	
		[Description("废止日期")]
        public DateTime? RepealDate { get; set; } // RepealDate
		/// <summary>部门负责人意见</summary>	
		[Description("部门负责人意见")]
        public string DeptLeaderComment { get; set; } // DeptLeaderComment
		/// <summary>总经理工作部意见</summary>	
		[Description("总经理工作部意见")]
        public string GeneralManagerWorkDeptComment { get; set; } // GeneralManagerWorkDeptComment
		/// <summary>申请部门分管领导意见</summary>	
		[Description("申请部门分管领导意见")]
        public string ApplyDeptDepartLeaderComment { get; set; } // ApplyDeptDepartLeaderComment
		/// <summary>总经理意见</summary>	
		[Description("总经理意见")]
        public string GeneralManagerComment { get; set; } // GeneralManagerComment
		/// <summary>董事长意见</summary>	
		[Description("董事长意见")]
        public string ChairmanComment { get; set; } // ChairmanComment
    }

	/// <summary>印章基本信息</summary>	
	[Description("印章基本信息")]
    public partial class T_Seal_SealInfo : Formula.BaseModel
    {
		/// <summary>ID</summary>	
		[Description("ID")]
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
        public string PrjID { get; set; } // PrjID
		/// <summary></summary>	
		[Description("")]
        public string FlowPhase { get; set; } // FlowPhase
		/// <summary></summary>	
		[Description("")]
        public string StepName { get; set; } // StepName
		/// <summary>印章编号</summary>	
		[Description("印章编号")]
        public string SealCode { get; set; } // SealCode
		/// <summary>印章名称</summary>	
		[Description("印章名称")]
        public string SealName { get; set; } // SealName
		/// <summary>印章类别</summary>	
		[Description("印章类别")]
        public string SealType { get; set; } // SealType
		/// <summary>开始使用时间</summary>	
		[Description("开始使用时间")]
        public DateTime? StartTime { get; set; } // StartTime
		/// <summary>印章保管部门</summary>	
		[Description("印章保管部门")]
        public string DeptID { get; set; } // DeptID
		/// <summary>印章保管部门名称</summary>	
		[Description("印章保管部门名称")]
        public string DeptIDName { get; set; } // DeptIDName
		/// <summary>印章保管人</summary>	
		[Description("印章保管人")]
        public string KeeperID { get; set; } // KeeperID
		/// <summary>印章保管人名称</summary>	
		[Description("印章保管人名称")]
        public string KeeperIDName { get; set; } // KeeperIDName
		/// <summary>印章用途</summary>	
		[Description("印章用途")]
        public string Application { get; set; } // Application
		/// <summary>备注</summary>	
		[Description("备注")]
        public string Remark { get; set; } // Remark
		/// <summary>图片</summary>	
		[Description("图片")]
        public string Attachment { get; set; } // Attachment
		/// <summary>印章状态</summary>	
		[Description("印章状态")]
        public string State { get; set; } // State
		/// <summary>借用人</summary>	
		[Description("借用人")]
        public string Borrower { get; set; } // Borrower
		/// <summary>借用人名称</summary>	
		[Description("借用人名称")]
        public string BorrowerName { get; set; } // BorrowerName
		/// <summary>借用时间</summary>	
		[Description("借用时间")]
        public DateTime? BorrowDate { get; set; } // BorrowDate
		/// <summary>测试附件</summary>	
		[Description("测试附件")]
        public string Attachment2 { get; set; } // Attachment2
    }

	/// <summary>T</summary>	
	[Description("T")]
    public partial class T_Seal_Transfer : Formula.BaseModel
    {
		/// <summary>ID</summary>	
		[Description("ID")]
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
        public string PrjID { get; set; } // PrjID
		/// <summary></summary>	
		[Description("")]
        public string FlowPhase { get; set; } // FlowPhase
		/// <summary></summary>	
		[Description("")]
        public string StepName { get; set; } // StepName
		/// <summary>印章名称</summary>	
		[Description("印章名称")]
        public string SealInfoID { get; set; } // SealInfoID
		/// <summary>印章名称名称</summary>	
		[Description("印章名称名称")]
        public string SealInfoIDName { get; set; } // SealInfoIDName
		/// <summary>编号</summary>	
		[Description("编号")]
        public string SealCode { get; set; } // SealCode
		/// <summary>移交原因</summary>	
		[Description("移交原因")]
        public string TransferReason { get; set; } // TransferReason
		/// <summary>印模</summary>	
		[Description("印模")]
        public string SealModel { get; set; } // SealModel
		/// <summary>接收人</summary>	
		[Description("接收人")]
        public string ReceivePersonID { get; set; } // ReceivePersonID
		/// <summary>接收人名称</summary>	
		[Description("接收人名称")]
        public string ReceivePersonIDName { get; set; } // ReceivePersonIDName
		/// <summary>移交人</summary>	
		[Description("移交人")]
        public string TransferPersonID { get; set; } // TransferPersonID
		/// <summary>移交人名称</summary>	
		[Description("移交人名称")]
        public string TransferPersonIDName { get; set; } // TransferPersonIDName
    }

	/// <summary></summary>	
	[Description("")]
    public partial class V_DocumentFileAuthority : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID
		/// <summary></summary>	
		[Description("")]
        public string DocumentInfoID { get; set; } // DocumentInfoID
		/// <summary></summary>	
		[Description("")]
        public string AuthType { get; set; } // AuthType
		/// <summary></summary>	
		[Description("")]
        public string RoleType { get; set; } // RoleType
		/// <summary></summary>	
		[Description("")]
        public string RoleCode { get; set; } // RoleCode
		/// <summary></summary>	
		[Description("")]
        public string Name { get; set; } // Name
		/// <summary></summary>	
		[Description("")]
        public int ReadOnly { get; set; } // ReadOnly
		/// <summary></summary>	
		[Description("")]
        public int CanWrite { get; set; } // CanWrite
		/// <summary></summary>	
		[Description("")]
        public int FullControl { get; set; } // FullControl
    }


    // ************************************************************************
    // POCO Configuration

    // S_D_Comment
    internal partial class S_D_CommentConfiguration : EntityTypeConfiguration<S_D_Comment>
    {
        public S_D_CommentConfiguration()
        {
			ToTable("S_D_COMMENT");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.Type).HasColumnName("TYPE").IsOptional().HasMaxLength(50);
            Property(x => x.UserName).HasColumnName("USERNAME").IsOptional().HasMaxLength(50);
            Property(x => x.UserID).HasColumnName("USERID").IsOptional().HasMaxLength(50);
            Property(x => x.Comment).HasColumnName("COMMENT").IsOptional().HasMaxLength(4000);
            Property(x => x.IsTemplate).HasColumnName("ISTEMPLATE").IsOptional().HasMaxLength(10);
            Property(x => x.IsUse).HasColumnName("ISUSE").IsOptional().HasMaxLength(10);
            Property(x => x.CreateUserName).HasColumnName("CREATEUSERNAME").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
        }
    }

    // S_D_Incoming
    internal partial class S_D_IncomingConfiguration : EntityTypeConfiguration<S_D_Incoming>
    {
        public S_D_IncomingConfiguration()
        {
			ToTable("S_D_INCOMING");
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
            Property(x => x.FileTitle).HasColumnName("FILETITLE").IsOptional().HasMaxLength(200);
            Property(x => x.Files).HasColumnName("FILES").IsOptional().HasMaxLength(500);
            Property(x => x.OfficeFrom).HasColumnName("OFFICEFROM").IsOptional().HasMaxLength(200);
            Property(x => x.FontSize).HasColumnName("FONTSIZE").IsOptional().HasMaxLength(200);
            Property(x => x.Type).HasColumnName("TYPE").IsOptional().HasMaxLength(200);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(200);
            Property(x => x.Number).HasColumnName("NUMBER").IsOptional().HasMaxLength(200);
            Property(x => x.IncomingDate).HasColumnName("INCOMINGDATE").IsOptional();
            Property(x => x.Security).HasColumnName("SECURITY").IsOptional().HasMaxLength(200);
            Property(x => x.EmergencyLevel).HasColumnName("EMERGENCYLEVEL").IsOptional().HasMaxLength(200);
            Property(x => x.FileCount).HasColumnName("FILECOUNT").IsOptional();
            Property(x => x.Materials).HasColumnName("MATERIALS").IsOptional().HasMaxLength(200);
            Property(x => x.UndertakeDeadline).HasColumnName("UNDERTAKEDEADLINE").IsOptional();
            Property(x => x.Subject).HasColumnName("SUBJECT").IsOptional().HasMaxLength(500);
            Property(x => x.Register).HasColumnName("REGISTER").IsOptional();
            Property(x => x.OfficialSign).HasColumnName("OFFICIALSIGN").IsOptional();
            Property(x => x.LeaderApprove).HasColumnName("LEADERAPPROVE").IsOptional();
            Property(x => x.DeptLeaderHandle).HasColumnName("DEPTLEADERHANDLE").IsOptional();
            Property(x => x.PresidentHandle).HasColumnName("PRESIDENTHANDLE").IsOptional();
            Property(x => x.PeopleHandle).HasColumnName("PEOPLEHANDLE").IsOptional();
        }
    }

    // S_D_Posting
    internal partial class S_D_PostingConfiguration : EntityTypeConfiguration<S_D_Posting>
    {
        public S_D_PostingConfiguration()
        {
			ToTable("S_D_POSTING");
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
            Property(x => x.MainFile).HasColumnName("MAINFILE").IsOptional().HasMaxLength(2147483647);
            Property(x => x.RedFile).HasColumnName("REDFILE").IsOptional().HasMaxLength(2147483647);
            Property(x => x.Title).HasColumnName("TITLE").IsOptional().HasMaxLength(200);
            Property(x => x.FileNo).HasColumnName("FILENO").IsOptional().HasMaxLength(200);
            Property(x => x.Drafter).HasColumnName("DRAFTER").IsOptional().HasMaxLength(200);
            Property(x => x.DrafterName).HasColumnName("DRAFTERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.DraftDate).HasColumnName("DRAFTDATE").IsOptional();
            Property(x => x.DraftDept).HasColumnName("DRAFTDEPT").IsOptional().HasMaxLength(200);
            Property(x => x.DraftDeptName).HasColumnName("DRAFTDEPTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.DeptLeader).HasColumnName("DEPTLEADER").IsOptional().HasMaxLength(200);
            Property(x => x.DeptLeaderName).HasColumnName("DEPTLEADERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Security).HasColumnName("SECURITY").IsOptional().HasMaxLength(200);
            Property(x => x.EmergencyLevel).HasColumnName("EMERGENCYLEVEL").IsOptional().HasMaxLength(200);
            Property(x => x.FormalNumber).HasColumnName("FORMALNUMBER").IsOptional();
            Property(x => x.AttachNumber).HasColumnName("ATTACHNUMBER").IsOptional();
            Property(x => x.TotalNumber).HasColumnName("TOTALNUMBER").IsOptional();
            Property(x => x.SendTo).HasColumnName("SENDTO").IsOptional().HasMaxLength(2000);
            Property(x => x.SendToName).HasColumnName("SENDTONAME").IsOptional().HasMaxLength(2000);
            Property(x => x.CopyTo).HasColumnName("COPYTO").IsOptional().HasMaxLength(2000);
            Property(x => x.CopyToName).HasColumnName("COPYTONAME").IsOptional().HasMaxLength(2000);
            Property(x => x.SpecificStaff).HasColumnName("SPECIFICSTAFF").IsOptional().HasMaxLength(200);
            Property(x => x.SpecificStaffName).HasColumnName("SPECIFICSTAFFNAME").IsOptional().HasMaxLength(200);
            Property(x => x.DraftSign).HasColumnName("DRAFTSIGN").IsOptional();
            Property(x => x.SectionChiefAudit).HasColumnName("SECTIONCHIEFAUDIT").IsOptional();
            Property(x => x.DirectorApprove).HasColumnName("DIRECTORAPPROVE").IsOptional();
            Property(x => x.OfficialTransfer).HasColumnName("OFFICIALTRANSFER").IsOptional();
            Property(x => x.LeaderApprove).HasColumnName("LEADERAPPROVE").IsOptional();
            Property(x => x.OfficialDocument).HasColumnName("OFFICIALDOCUMENT").IsOptional();
        }
    }

    // S_D_RedTitle
    internal partial class S_D_RedTitleConfiguration : EntityTypeConfiguration<S_D_RedTitle>
    {
        public S_D_RedTitleConfiguration()
        {
			ToTable("S_D_REDTITLE");
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
            Property(x => x.Title).HasColumnName("TITLE").IsOptional().HasMaxLength(200);
            Property(x => x.DeptInfo).HasColumnName("DEPTINFO").IsOptional().HasMaxLength(200);
            Property(x => x.DeptInfoName).HasColumnName("DEPTINFONAME").IsOptional().HasMaxLength(200);
            Property(x => x.Register).HasColumnName("REGISTER").IsOptional().HasMaxLength(200);
            Property(x => x.RegisterName).HasColumnName("REGISTERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.RedFile).HasColumnName("REDFILE").IsOptional().HasMaxLength(500);
            Property(x => x.Remark).HasColumnName("REMARK").IsOptional().HasMaxLength(500);
        }
    }

    // S_F_DocumentFileAuthority
    internal partial class S_F_DocumentFileAuthorityConfiguration : EntityTypeConfiguration<S_F_DocumentFileAuthority>
    {
        public S_F_DocumentFileAuthorityConfiguration()
        {
			ToTable("S_F_DOCUMENTFILEAUTHORITY");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.DocumentInfoID).HasColumnName("DOCUMENTINFOID").IsOptional().HasMaxLength(50);
            Property(x => x.AuthType).HasColumnName("AUTHTYPE").IsOptional().HasMaxLength(50);
            Property(x => x.RoleType).HasColumnName("ROLETYPE").IsOptional().HasMaxLength(50);
            Property(x => x.RoleCode).HasColumnName("ROLECODE").IsOptional().HasMaxLength(50);
            Property(x => x.IsParentAuth).HasColumnName("ISPARENTAUTH").IsOptional();

            // Foreign keys
            HasOptional(a => a.S_F_DocumentInfo).WithMany(b => b.S_F_DocumentFileAuthority).HasForeignKey(c => c.DocumentInfoID); // FK_S_F_DocumentFileAuthority_S_F_DocumentInfo
        }
    }

    // S_F_DocumentInfo
    internal partial class S_F_DocumentInfoConfiguration : EntityTypeConfiguration<S_F_DocumentInfo>
    {
        public S_F_DocumentInfoConfiguration()
        {
			ToTable("S_F_DOCUMENTINFO");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.ParentID).HasColumnName("PARENTID").IsOptional().HasMaxLength(50);
            Property(x => x.FullPathID).HasColumnName("FULLPATHID").IsOptional().HasMaxLength(500);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(50);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.Level).HasColumnName("LEVEL").IsOptional();
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUserName).HasColumnName("CREATEUSERNAME").IsOptional().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUserName).HasColumnName("MODIFYUSERNAME").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyDate).HasColumnName("MODIFYDATE").IsOptional();
            Property(x => x.IsInherit).HasColumnName("ISINHERIT").IsOptional();
            Property(x => x.IsValid).HasColumnName("ISVALID").IsOptional().HasMaxLength(10);
            Property(x => x.IsDeleted).HasColumnName("ISDELETED").IsOptional();
        }
    }

    // S_F_FileInfo
    internal partial class S_F_FileInfoConfiguration : EntityTypeConfiguration<S_F_FileInfo>
    {
        public S_F_FileInfoConfiguration()
        {
			ToTable("S_F_FILEINFO");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.DocumentInfoID).HasColumnName("DOCUMENTINFOID").IsOptional().HasMaxLength(50);
            Property(x => x.FileName).HasColumnName("FILENAME").IsOptional().HasMaxLength(200);
            Property(x => x.FileCode).HasColumnName("FILECODE").IsOptional().HasMaxLength(200);
            Property(x => x.ExtName).HasColumnName("EXTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.FileSize).HasColumnName("FILESIZE").IsOptional();
            Property(x => x.Attachment).HasColumnName("ATTACHMENT").IsOptional().HasMaxLength(2000);
            Property(x => x.SourceFileID).HasColumnName("SOURCEFILEID").IsOptional().HasMaxLength(200);
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUserName).HasColumnName("CREATEUSERNAME").IsOptional().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUserName).HasColumnName("MODIFYUSERNAME").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyDate).HasColumnName("MODIFYDATE").IsOptional();
            Property(x => x.OrgID).HasColumnName("ORGID").IsOptional().HasMaxLength(50);
            Property(x => x.OrgName).HasColumnName("ORGNAME").IsOptional().HasMaxLength(50);
            Property(x => x.Remark).HasColumnName("REMARK").IsOptional().HasMaxLength(500);
            Property(x => x.IsValid).HasColumnName("ISVALID").IsOptional().HasMaxLength(50);
            Property(x => x.IsDeleted).HasColumnName("ISDELETED").IsOptional();
            Property(x => x.SubmitUser).HasColumnName("SUBMITUSER").IsOptional().HasMaxLength(200);
            Property(x => x.SubmitUserName).HasColumnName("SUBMITUSERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.SubmitDept).HasColumnName("SUBMITDEPT").IsOptional().HasMaxLength(200);
            Property(x => x.SubmitDeptName).HasColumnName("SUBMITDEPTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.SubmitDate).HasColumnName("SUBMITDATE").IsOptional();

            // Foreign keys
            HasOptional(a => a.S_F_DocumentInfo).WithMany(b => b.S_F_FileInfo).HasForeignKey(c => c.DocumentInfoID); // FK_S_F_FileInfo_S_F_DocumentInfo
        }
    }

    // S_FC_CostInfo
    internal partial class S_FC_CostInfoConfiguration : EntityTypeConfiguration<S_FC_CostInfo>
    {
        public S_FC_CostInfoConfiguration()
        {
			ToTable("S_FC_COSTINFO");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.SubjectName).HasColumnName("SUBJECTNAME").IsOptional().HasMaxLength(500);
            Property(x => x.SubjectCode).HasColumnName("SUBJECTCODE").IsOptional().HasMaxLength(500);
            Property(x => x.ProjectID).HasColumnName("PROJECTID").IsOptional().HasMaxLength(50);
            Property(x => x.ProjectName).HasColumnName("PROJECTNAME").IsOptional().HasMaxLength(500);
            Property(x => x.ProjectCode).HasColumnName("PROJECTCODE").IsOptional().HasMaxLength(500);
            Property(x => x.CostType).HasColumnName("COSTTYPE").IsOptional().HasMaxLength(50);
            Property(x => x.ProjectType).HasColumnName("PROJECTTYPE").IsOptional().HasMaxLength(50);
            Property(x => x.UnitPrice).HasColumnName("UNITPRICE").IsOptional().HasPrecision(18,4);
            Property(x => x.Quantity).HasColumnName("QUANTITY").IsOptional().HasPrecision(18,4);
            Property(x => x.CostValue).HasColumnName("COSTVALUE").IsRequired().HasPrecision(18,4);
            Property(x => x.CostUserID).HasColumnName("COSTUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CostUserName).HasColumnName("COSTUSERNAME").IsOptional().HasMaxLength(50);
            Property(x => x.CostUserDeptID).HasColumnName("COSTUSERDEPTID").IsOptional().HasMaxLength(500);
            Property(x => x.CostUserDeptName).HasColumnName("COSTUSERDEPTNAME").IsOptional().HasMaxLength(500);
            Property(x => x.ProjectDeptID).HasColumnName("PROJECTDEPTID").IsOptional().HasMaxLength(500);
            Property(x => x.ProjectDeptName).HasColumnName("PROJECTDEPTNAME").IsOptional().HasMaxLength(500);
            Property(x => x.ProjectChargerUserID).HasColumnName("PROJECTCHARGERUSERID").IsOptional().HasMaxLength(500);
            Property(x => x.ProjectChargerUserName).HasColumnName("PROJECTCHARGERUSERNAME").IsOptional().HasMaxLength(500);
            Property(x => x.ProjectClass).HasColumnName("PROJECTCLASS").IsOptional().HasMaxLength(50);
            Property(x => x.CostDate).HasColumnName("COSTDATE").IsRequired();
            Property(x => x.BelongYear).HasColumnName("BELONGYEAR").IsRequired();
            Property(x => x.BelongMonth).HasColumnName("BELONGMONTH").IsRequired();
            Property(x => x.BelongQuarter).HasColumnName("BELONGQUARTER").IsRequired();
            Property(x => x.RelateID).HasColumnName("RELATEID").IsOptional().HasMaxLength(50);
            Property(x => x.FormID).HasColumnName("FORMID").IsOptional().HasMaxLength(50);
            Property(x => x.Extend1).HasColumnName("EXTEND1").IsOptional().HasMaxLength(200);
            Property(x => x.Extend2).HasColumnName("EXTEND2").IsOptional().HasMaxLength(200);
            Property(x => x.Extend3).HasColumnName("EXTEND3").IsOptional().HasMaxLength(200);
            Property(x => x.Extend4).HasColumnName("EXTEND4").IsOptional().HasMaxLength(200);
            Property(x => x.Extend5).HasColumnName("EXTEND5").IsOptional().HasMaxLength(200);
            Property(x => x.Extend6).HasColumnName("EXTEND6").IsOptional().HasMaxLength(200);
            Property(x => x.Extend7).HasColumnName("EXTEND7").IsOptional().HasMaxLength(200);
            Property(x => x.Extend8).HasColumnName("EXTEND8").IsOptional().HasMaxLength(200);
        }
    }

    // S_FC_UserLoanAccount
    internal partial class S_FC_UserLoanAccountConfiguration : EntityTypeConfiguration<S_FC_UserLoanAccount>
    {
        public S_FC_UserLoanAccountConfiguration()
        {
			ToTable("S_FC_USERLOANACCOUNT");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.UserID).HasColumnName("USERID").IsRequired().HasMaxLength(50);
            Property(x => x.UserName).HasColumnName("USERNAME").IsRequired().HasMaxLength(50);
            Property(x => x.UserDeptID).HasColumnName("USERDEPTID").IsOptional().HasMaxLength(50);
            Property(x => x.UserDeptName).HasColumnName("USERDEPTNAME").IsOptional().HasMaxLength(50);
            Property(x => x.AccountType).HasColumnName("ACCOUNTTYPE").IsRequired().HasMaxLength(50);
            Property(x => x.AccountValue).HasColumnName("ACCOUNTVALUE").IsRequired().HasPrecision(18,4);
            Property(x => x.RelateFormID).HasColumnName("RELATEFORMID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.Operator).HasColumnName("OPERATOR").IsOptional().HasMaxLength(50);
            Property(x => x.OperatorName).HasColumnName("OPERATORNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ReturnDate).HasColumnName("RETURNDATE").IsOptional();
        }
    }

    // S_M_ConferenceRoom
    internal partial class S_M_ConferenceRoomConfiguration : EntityTypeConfiguration<S_M_ConferenceRoom>
    {
        public S_M_ConferenceRoomConfiguration()
        {
			ToTable("S_M_CONFERENCEROOM");
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
            Property(x => x.RoomName).HasColumnName("ROOMNAME").IsOptional().HasMaxLength(50);
            Property(x => x.LinkName).HasColumnName("LINKNAME").IsOptional().HasMaxLength(50);
            Property(x => x.Capacity).HasColumnName("CAPACITY").IsOptional();
            Property(x => x.ManageDeptID).HasColumnName("MANAGEDEPTID").IsOptional().HasMaxLength(50);
            Property(x => x.ManageDeptIDName).HasColumnName("MANAGEDEPTIDNAME").IsOptional().HasMaxLength(50);
            Property(x => x.RoomAddress).HasColumnName("ROOMADDRESS").IsOptional().HasMaxLength(200);
            Property(x => x.Configuredevice).HasColumnName("CONFIGUREDEVICE").IsOptional();
        }
    }

    // S_Survey_Option
    internal partial class S_Survey_OptionConfiguration : EntityTypeConfiguration<S_Survey_Option>
    {
        public S_Survey_OptionConfiguration()
        {
			ToTable("S_SURVEY_OPTION");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.QuestionID).HasColumnName("QUESTIONID").IsOptional().HasMaxLength(50);
            Property(x => x.OptionName).HasColumnName("OPTIONNAME").IsOptional().HasMaxLength(200);
            Property(x => x.VoteNum).HasColumnName("VOTENUM").IsOptional();
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUserName).HasColumnName("CREATEUSERNAME").IsOptional().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();

            // Foreign keys
            HasOptional(a => a.S_Survey_Question).WithMany(b => b.S_Survey_Option).HasForeignKey(c => c.QuestionID); // FK_S_Survey_Option_S_Survey_Question
        }
    }

    // S_Survey_Question
    internal partial class S_Survey_QuestionConfiguration : EntityTypeConfiguration<S_Survey_Question>
    {
        public S_Survey_QuestionConfiguration()
        {
			ToTable("S_SURVEY_QUESTION");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.SurveyID).HasColumnName("SURVEYID").IsOptional().HasMaxLength(50);
            Property(x => x.QuestionName).HasColumnName("QUESTIONNAME").IsOptional().HasMaxLength(100);
            Property(x => x.QuestionType).HasColumnName("QUESTIONTYPE").IsOptional().HasMaxLength(50);
            Property(x => x.IsMustAnswer).HasColumnName("ISMUSTANSWER").IsOptional().HasMaxLength(1);
            Property(x => x.IsComment).HasColumnName("ISCOMMENT").IsOptional().HasMaxLength(1);
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUserName).HasColumnName("CREATEUSERNAME").IsOptional().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();

            // Foreign keys
            HasOptional(a => a.S_Survey_Subject).WithMany(b => b.S_Survey_Question).HasForeignKey(c => c.SurveyID); // FK_S_Survey_Question_S_Survey_Subject
        }
    }

    // S_Survey_QuestionComment
    internal partial class S_Survey_QuestionCommentConfiguration : EntityTypeConfiguration<S_Survey_QuestionComment>
    {
        public S_Survey_QuestionCommentConfiguration()
        {
			ToTable("S_SURVEY_QUESTIONCOMMENT");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.QuestionID).HasColumnName("QUESTIONID").IsOptional().HasMaxLength(50);
            Property(x => x.UserID).HasColumnName("USERID").IsOptional().HasMaxLength(50);
            Property(x => x.UserName).HasColumnName("USERNAME").IsOptional().HasMaxLength(50);
            Property(x => x.Comment).HasColumnName("COMMENT").IsOptional().HasMaxLength(4000);
            Property(x => x.VoteDate).HasColumnName("VOTEDATE").IsOptional();

            // Foreign keys
            HasOptional(a => a.S_Survey_Question).WithMany(b => b.S_Survey_QuestionComment).HasForeignKey(c => c.QuestionID); // FK_S_Survey_QuestionComment_S_Survey_Question
        }
    }

    // S_Survey_QuestionVote
    internal partial class S_Survey_QuestionVoteConfiguration : EntityTypeConfiguration<S_Survey_QuestionVote>
    {
        public S_Survey_QuestionVoteConfiguration()
        {
			ToTable("S_SURVEY_QUESTIONVOTE");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.QuestionID).HasColumnName("QUESTIONID").IsOptional().HasMaxLength(50);
            Property(x => x.UserID).HasColumnName("USERID").IsOptional().HasMaxLength(50);
            Property(x => x.UserName).HasColumnName("USERNAME").IsOptional().HasMaxLength(50);
            Property(x => x.QuestionMsg).HasColumnName("QUESTIONMSG").IsOptional().HasMaxLength(4000);
            Property(x => x.VoteDate).HasColumnName("VOTEDATE").IsOptional();

            // Foreign keys
            HasOptional(a => a.S_Survey_Question).WithMany(b => b.S_Survey_QuestionVote).HasForeignKey(c => c.QuestionID); // FK_S_Survey_QuestionVote_S_Survey_Question
        }
    }

    // S_Survey_Subject
    internal partial class S_Survey_SubjectConfiguration : EntityTypeConfiguration<S_Survey_Subject>
    {
        public S_Survey_SubjectConfiguration()
        {
			ToTable("S_SURVEY_SUBJECT");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.SurveyName).HasColumnName("SURVEYNAME").IsOptional().HasMaxLength(200);
            Property(x => x.QuestionNum).HasColumnName("QUESTIONNUM").IsOptional();
            Property(x => x.IsPeriod).HasColumnName("ISPERIOD").IsOptional().HasMaxLength(50);
            Property(x => x.SurveyStartDate).HasColumnName("SURVEYSTARTDATE").IsOptional();
            Property(x => x.SurveyEndDate).HasColumnName("SURVEYENDDATE").IsOptional();
            Property(x => x.IsMemoryUser).HasColumnName("ISMEMORYUSER").IsOptional().HasMaxLength(50);
            Property(x => x.QueryUserIDs).HasColumnName("QUERYUSERIDS").IsOptional().HasMaxLength(4000);
            Property(x => x.QueryUserNames).HasColumnName("QUERYUSERNAMES").IsOptional().HasMaxLength(4000);
            Property(x => x.StatisticsUserIDs).HasColumnName("STATISTICSUSERIDS").IsOptional().HasMaxLength(4000);
            Property(x => x.StatisticsUserNames).HasColumnName("STATISTICSUSERNAMES").IsOptional().HasMaxLength(4000);
            Property(x => x.SurveyAttchment).HasColumnName("SURVEYATTCHMENT").IsOptional().HasMaxLength(4000);
            Property(x => x.Remark).HasColumnName("REMARK").IsOptional().HasMaxLength(4000);
            Property(x => x.IssueID).HasColumnName("ISSUEID").IsOptional().HasMaxLength(50);
            Property(x => x.IssueName).HasColumnName("ISSUENAME").IsOptional().HasMaxLength(50);
            Property(x => x.IssueDate).HasColumnName("ISSUEDATE").IsOptional();
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUserName).HasColumnName("CREATEUSERNAME").IsOptional().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.State).HasColumnName("STATE").IsOptional().HasMaxLength(50);
            Property(x => x.QueryDeptIDs).HasColumnName("QUERYDEPTIDS").IsOptional().HasMaxLength(4000);
            Property(x => x.QueryDeptNames).HasColumnName("QUERYDEPTNAMES").IsOptional().HasMaxLength(4000);
        }
    }

    // S_Survey_Vote
    internal partial class S_Survey_VoteConfiguration : EntityTypeConfiguration<S_Survey_Vote>
    {
        public S_Survey_VoteConfiguration()
        {
			ToTable("S_SURVEY_VOTE");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.OptionID).HasColumnName("OPTIONID").IsOptional().HasMaxLength(50);
            Property(x => x.UserID).HasColumnName("USERID").IsOptional().HasMaxLength(50);
            Property(x => x.UserName).HasColumnName("USERNAME").IsOptional().HasMaxLength(50);
            Property(x => x.VoteDate).HasColumnName("VOTEDATE").IsOptional();

            // Foreign keys
            HasOptional(a => a.S_Survey_Option).WithMany(b => b.S_Survey_Vote).HasForeignKey(c => c.OptionID); // FK_S_Survey_Vote_S_Survey_Option
        }
    }

    // T_B_MemoManagement
    internal partial class T_B_MemoManagementConfiguration : EntityTypeConfiguration<T_B_MemoManagement>
    {
        public T_B_MemoManagementConfiguration()
        {
			ToTable("T_B_MEMOMANAGEMENT");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsOptional().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUser).HasColumnName("MODIFYUSER").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyDate).HasColumnName("MODIFYDATE").IsOptional();
            Property(x => x.OrgID).HasColumnName("ORGID").IsOptional().HasMaxLength(50);
            Property(x => x.OrgName).HasColumnName("ORGNAME").IsOptional().HasMaxLength(500);
            Property(x => x.FlowPhase).HasColumnName("FLOWPHASE").IsOptional().HasMaxLength(50);
            Property(x => x.StepName).HasColumnName("STEPNAME").IsOptional().HasMaxLength(50);
            Property(x => x.Dense).HasColumnName("DENSE").IsOptional().HasMaxLength(50);
            Property(x => x.UrgentSlow).HasColumnName("URGENTSLOW").IsOptional().HasMaxLength(50);
            Property(x => x.LeaderIssueName).HasColumnName("LEADERISSUENAME").IsOptional().HasMaxLength(50);
            Property(x => x.LeaderIssueID).HasColumnName("LEADERISSUEID").IsOptional().HasMaxLength(50);
            Property(x => x.LeaderIssueComment).HasColumnName("LEADERISSUECOMMENT").IsOptional().HasMaxLength(2000);
            Property(x => x.WorkDeptLeaderName).HasColumnName("WORKDEPTLEADERNAME").IsOptional().HasMaxLength(50);
            Property(x => x.WorkDeptLeaderID).HasColumnName("WORKDEPTLEADERID").IsOptional().HasMaxLength(50);
            Property(x => x.WorkDeptLeaderComment).HasColumnName("WORKDEPTLEADERCOMMENT").IsOptional().HasMaxLength(2000);
            Property(x => x.PublicCareerDeptName).HasColumnName("PUBLICCAREERDEPTNAME").IsOptional().HasMaxLength(50);
            Property(x => x.PublicCareerDeptID).HasColumnName("PUBLICCAREERDEPTID").IsOptional().HasMaxLength(50);
            Property(x => x.PublicCareerDeptComment).HasColumnName("PUBLICCAREERDEPTCOMMENT").IsOptional().HasMaxLength(2000);
            Property(x => x.LeaderCountersignNames).HasColumnName("LEADERCOUNTERSIGNNAMES").IsOptional().HasMaxLength(2000);
            Property(x => x.LeaderCountersignIDs).HasColumnName("LEADERCOUNTERSIGNIDS").IsOptional().HasMaxLength(2000);
            Property(x => x.LeaderCountersignComment).HasColumnName("LEADERCOUNTERSIGNCOMMENT").IsOptional();
            Property(x => x.DeptLeaderName).HasColumnName("DEPTLEADERNAME").IsOptional().HasMaxLength(50);
            Property(x => x.DeptLeaderID).HasColumnName("DEPTLEADERID").IsOptional().HasMaxLength(50);
            Property(x => x.DeptLeaderComment).HasColumnName("DEPTLEADERCOMMENT").IsOptional().HasMaxLength(2000);
            Property(x => x.DraftManComment).HasColumnName("DRAFTMANCOMMENT").IsOptional().HasMaxLength(2000);
            Property(x => x.PaperNumber).HasColumnName("PAPERNUMBER").IsOptional().HasMaxLength(50);
            Property(x => x.Title).HasColumnName("TITLE").IsOptional().HasMaxLength(500);
            Property(x => x.DocID).HasColumnName("DOCID").IsOptional().HasMaxLength(500);
            Property(x => x.MergeDocID).HasColumnName("MERGEDOCID").IsOptional().HasMaxLength(500);
            Property(x => x.MainSendUserName).HasColumnName("MAINSENDUSERNAME").IsOptional().HasMaxLength(500);
            Property(x => x.MainSendUserID).HasColumnName("MAINSENDUSERID").IsOptional().HasMaxLength(500);
            Property(x => x.OtherSendUserNames).HasColumnName("OTHERSENDUSERNAMES").IsOptional().HasMaxLength(500);
            Property(x => x.OtherSendUserIDs).HasColumnName("OTHERSENDUSERIDS").IsOptional().HasMaxLength(500);
            Property(x => x.TotalNumber).HasColumnName("TOTALNUMBER").IsOptional().HasMaxLength(50);
            Property(x => x.BodyTotalNumber).HasColumnName("BODYTOTALNUMBER").IsOptional().HasMaxLength(50);
            Property(x => x.AttachmentTotalNumber).HasColumnName("ATTACHMENTTOTALNUMBER").IsOptional().HasMaxLength(50);
            Property(x => x.Status).HasColumnName("STATUS").IsOptional().HasMaxLength(50);
            Property(x => x.ExecutedSteps).HasColumnName("EXECUTEDSTEPS").IsOptional().HasMaxLength(4000);
        }
    }

    // T_B_OutgoingFile
    internal partial class T_B_OutgoingFileConfiguration : EntityTypeConfiguration<T_B_OutgoingFile>
    {
        public T_B_OutgoingFileConfiguration()
        {
			ToTable("T_B_OUTGOINGFILE");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsOptional().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.FlowState).HasColumnName("FLOWSTATE").IsOptional().HasMaxLength(50);
            Property(x => x.FlowPhase).HasColumnName("FLOWPHASE").IsOptional().HasMaxLength(50);
            Property(x => x.StepName).HasColumnName("STEPNAME").IsOptional().HasMaxLength(50);
            Property(x => x.WriteID).HasColumnName("WRITEID").IsOptional().HasMaxLength(50);
            Property(x => x.WriteName).HasColumnName("WRITENAME").IsOptional().HasMaxLength(50);
            Property(x => x.WriteDate).HasColumnName("WRITEDATE").IsOptional();
            Property(x => x.WriteDeptID).HasColumnName("WRITEDEPTID").IsOptional().HasMaxLength(50);
            Property(x => x.WriteDeptName).HasColumnName("WRITEDEPTNAME").IsOptional().HasMaxLength(50);
            Property(x => x.HandleDate).HasColumnName("HANDLEDATE").IsOptional();
            Property(x => x.OutType).HasColumnName("OUTTYPE").IsOptional().HasMaxLength(50);
            Property(x => x.Year).HasColumnName("YEAR").IsOptional().HasMaxLength(50);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(50);
            Property(x => x.Security).HasColumnName("SECURITY").IsOptional().HasMaxLength(50);
            Property(x => x.Urgency).HasColumnName("URGENCY").IsOptional().HasMaxLength(50);
            Property(x => x.CopyCount).HasColumnName("COPYCOUNT").IsOptional();
            Property(x => x.Title).HasColumnName("TITLE").IsOptional().HasMaxLength(100);
            Property(x => x.MainSend).HasColumnName("MAINSEND").IsOptional().HasMaxLength(100);
            Property(x => x.TellDept).HasColumnName("TELLDEPT").IsOptional().HasMaxLength(100);
            Property(x => x.CopySend).HasColumnName("COPYSEND").IsOptional().HasMaxLength(100);
            Property(x => x.MainBody).HasColumnName("MAINBODY").IsOptional().HasMaxLength(100);
            Property(x => x.NewMainBody).HasColumnName("NEWMAINBODY").IsOptional().HasMaxLength(100);
            Property(x => x.Attachment).HasColumnName("ATTACHMENT").IsOptional();
            Property(x => x.MainDeptCheckSign).HasColumnName("MAINDEPTCHECKSIGN").IsOptional();
            Property(x => x.DrafterSign).HasColumnName("DRAFTERSIGN").IsOptional();
            Property(x => x.HuiQinSign).HasColumnName("HUIQINSIGN").IsOptional();
            Property(x => x.LeaderSign).HasColumnName("LEADERSIGN").IsOptional();
            Property(x => x.TaoHongAdmin).HasColumnName("TAOHONGADMIN").IsOptional().HasMaxLength(50);
            Property(x => x.SealID).HasColumnName("SEALID").IsOptional().HasMaxLength(50);
        }
    }

    // T_B_Remind
    internal partial class T_B_RemindConfiguration : EntityTypeConfiguration<T_B_Remind>
    {
        public T_B_RemindConfiguration()
        {
			ToTable("T_B_REMIND");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsOptional().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.OutgoingFileID).HasColumnName("OUTGOINGFILEID").IsOptional().HasMaxLength(50);
            Property(x => x.UserID).HasColumnName("USERID").IsOptional();
            Property(x => x.UserName).HasColumnName("USERNAME").IsOptional();
            Property(x => x.Msg).HasColumnName("MSG").IsOptional();
        }
    }

    // T_B_SendFaxManagement
    internal partial class T_B_SendFaxManagementConfiguration : EntityTypeConfiguration<T_B_SendFaxManagement>
    {
        public T_B_SendFaxManagementConfiguration()
        {
			ToTable("T_B_SENDFAXMANAGEMENT");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsOptional().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUser).HasColumnName("MODIFYUSER").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyDate).HasColumnName("MODIFYDATE").IsOptional();
            Property(x => x.OrgID).HasColumnName("ORGID").IsOptional().HasMaxLength(50);
            Property(x => x.OrgName).HasColumnName("ORGNAME").IsOptional().HasMaxLength(500);
            Property(x => x.ReceiveDept).HasColumnName("RECEIVEDEPT").IsOptional();
            Property(x => x.SendDept).HasColumnName("SENDDEPT").IsOptional().HasMaxLength(500);
            Property(x => x.SendDeptID).HasColumnName("SENDDEPTID").IsOptional().HasMaxLength(500);
            Property(x => x.ReceiveUserName).HasColumnName("RECEIVEUSERNAME").IsOptional().HasMaxLength(500);
            Property(x => x.SendUserName).HasColumnName("SENDUSERNAME").IsOptional().HasMaxLength(500);
            Property(x => x.SendUserID).HasColumnName("SENDUSERID").IsOptional().HasMaxLength(500);
            Property(x => x.OtherSendUserNames).HasColumnName("OTHERSENDUSERNAMES").IsOptional().HasMaxLength(500);
            Property(x => x.OtherSendUserIDs).HasColumnName("OTHERSENDUSERIDS").IsOptional().HasMaxLength(500);
            Property(x => x.SignedUserIDs).HasColumnName("SIGNEDUSERIDS").IsOptional().HasMaxLength(500);
            Property(x => x.SignedUserNames).HasColumnName("SIGNEDUSERNAMES").IsOptional().HasMaxLength(500);
            Property(x => x.PageNo).HasColumnName("PAGENO").IsOptional().HasMaxLength(50);
            Property(x => x.Telephone).HasColumnName("TELEPHONE").IsOptional().HasMaxLength(50);
            Property(x => x.Telephone2).HasColumnName("TELEPHONE2").IsOptional().HasMaxLength(50);
            Property(x => x.FaxNumber).HasColumnName("FAXNUMBER").IsOptional().HasMaxLength(50);
            Property(x => x.FaxNumber2).HasColumnName("FAXNUMBER2").IsOptional().HasMaxLength(50);
            Property(x => x.FaxTheme).HasColumnName("FAXTHEME").IsOptional().HasMaxLength(200);
            Property(x => x.Attachment).HasColumnName("ATTACHMENT").IsOptional().HasMaxLength(2000);
            Property(x => x.CheckorComment).HasColumnName("CHECKORCOMMENT").IsOptional().HasMaxLength(4000);
            Property(x => x.DeptLeaderComment).HasColumnName("DEPTLEADERCOMMENT").IsOptional().HasMaxLength(4000);
            Property(x => x.DepartLeaderComment).HasColumnName("DEPARTLEADERCOMMENT").IsOptional().HasMaxLength(4000);
            Property(x => x.FlowPhase).HasColumnName("FLOWPHASE").IsOptional().HasMaxLength(50);
        }
    }

    // T_F_LoanApply
    internal partial class T_F_LoanApplyConfiguration : EntityTypeConfiguration<T_F_LoanApply>
    {
        public T_F_LoanApplyConfiguration()
        {
			ToTable("T_F_LOANAPPLY");
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
            Property(x => x.ApplyUser).HasColumnName("APPLYUSER").IsOptional().HasMaxLength(200);
            Property(x => x.ApplyUserName).HasColumnName("APPLYUSERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ApplyDept).HasColumnName("APPLYDEPT").IsOptional().HasMaxLength(200);
            Property(x => x.ApplyDeptName).HasColumnName("APPLYDEPTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ApplyDate).HasColumnName("APPLYDATE").IsOptional();
            Property(x => x.LoanValue).HasColumnName("LOANVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.Reason).HasColumnName("REASON").IsOptional().HasMaxLength(500);
            Property(x => x.ProjectInfo).HasColumnName("PROJECTINFO").IsOptional().HasMaxLength(500);
            Property(x => x.ProjectInfoName).HasColumnName("PROJECTINFONAME").IsOptional().HasMaxLength(500);
            Property(x => x.Remark).HasColumnName("REMARK").IsOptional().HasMaxLength(500);
            Property(x => x.Attachment).HasColumnName("ATTACHMENT").IsOptional().HasMaxLength(500);
            Property(x => x.FactValue).HasColumnName("FACTVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.LoanValueDX).HasColumnName("LOANVALUEDX").IsOptional().HasMaxLength(200);
            Property(x => x.FactValueDX).HasColumnName("FACTVALUEDX").IsOptional().HasMaxLength(200);
            Property(x => x.TravelApply).HasColumnName("TRAVELAPPLY").IsOptional().HasMaxLength(200);
            Property(x => x.TravelApplyName).HasColumnName("TRAVELAPPLYNAME").IsOptional().HasMaxLength(200);
        }
    }

    // T_F_ReimbursementApply
    internal partial class T_F_ReimbursementApplyConfiguration : EntityTypeConfiguration<T_F_ReimbursementApply>
    {
        public T_F_ReimbursementApplyConfiguration()
        {
			ToTable("T_F_REIMBURSEMENTAPPLY");
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
            Property(x => x.ApplyDept).HasColumnName("APPLYDEPT").IsOptional().HasMaxLength(200);
            Property(x => x.ApplyDeptName).HasColumnName("APPLYDEPTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ReimbursementType).HasColumnName("REIMBURSEMENTTYPE").IsOptional().HasMaxLength(200);
            Property(x => x.ReimbursementClass).HasColumnName("REIMBURSEMENTCLASS").IsOptional().HasMaxLength(200);
            Property(x => x.DeptInfo).HasColumnName("DEPTINFO").IsOptional().HasMaxLength(200);
            Property(x => x.DeptInfoName).HasColumnName("DEPTINFONAME").IsOptional().HasMaxLength(200);
            Property(x => x.TravelApplyInfo).HasColumnName("TRAVELAPPLYINFO").IsOptional().HasMaxLength(200);
            Property(x => x.TravelApplyInfoName).HasColumnName("TRAVELAPPLYINFONAME").IsOptional().HasMaxLength(2000);
            Property(x => x.FactPaymentValue).HasColumnName("FACTPAYMENTVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.ReimbursementValue).HasColumnName("REIMBURSEMENTVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.ApplyValue).HasColumnName("APPLYVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.LoanValue).HasColumnName("LOANVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.Attachments).HasColumnName("ATTACHMENTS").IsOptional().HasMaxLength(500);
            Property(x => x.Remark).HasColumnName("REMARK").IsOptional().HasMaxLength(500);
            Property(x => x.SerialNumber).HasColumnName("SERIALNUMBER").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectInfo).HasColumnName("PROJECTINFO").IsOptional().HasMaxLength(500);
            Property(x => x.ProjectInfoName).HasColumnName("PROJECTINFONAME").IsOptional().HasMaxLength(2000);
            Property(x => x.Details).HasColumnName("DETAILS").IsOptional().HasMaxLength(1073741823);
        }
    }

    // T_F_ReimbursementApply_Details
    internal partial class T_F_ReimbursementApply_DetailsConfiguration : EntityTypeConfiguration<T_F_ReimbursementApply_Details>
    {
        public T_F_ReimbursementApply_DetailsConfiguration()
        {
			ToTable("T_F_REIMBURSEMENTAPPLY_DETAILS");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.T_F_ReimbursementApplyID).HasColumnName("T_F_REIMBURSEMENTAPPLYID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.ProjectInfo).HasColumnName("PROJECTINFO").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectInfoName).HasColumnName("PROJECTINFONAME").IsOptional().HasMaxLength(200);
            Property(x => x.SubjectCode).HasColumnName("SUBJECTCODE").IsOptional().HasMaxLength(200);
            Property(x => x.ApplyValue).HasColumnName("APPLYVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.Remark).HasColumnName("REMARK").IsOptional().HasMaxLength(500);
            Property(x => x.ProjectType).HasColumnName("PROJECTTYPE").IsOptional().HasMaxLength(200);
            Property(x => x.Dept).HasColumnName("DEPT").IsOptional().HasMaxLength(200);
            Property(x => x.DeptName).HasColumnName("DEPTNAME").IsOptional().HasMaxLength(200);

            // Foreign keys
            HasOptional(a => a.T_F_ReimbursementApply).WithMany(b => b.T_F_ReimbursementApply_Details).HasForeignKey(c => c.T_F_ReimbursementApplyID); // FK_T_F_ReimbursementApply_Details_T_F_ReimbursementApply
        }
    }

    // T_HC_Test
    internal partial class T_HC_TestConfiguration : EntityTypeConfiguration<T_HC_Test>
    {
        public T_HC_TestConfiguration()
        {
			ToTable("T_HC_TEST");
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
            Property(x => x.名称).HasColumnName("名称").IsOptional().HasMaxLength(200);
            Property(x => x.排序).HasColumnName("排序").IsOptional();
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(200);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
        }
    }

    // T_M_ConferenceApply
    internal partial class T_M_ConferenceApplyConfiguration : EntityTypeConfiguration<T_M_ConferenceApply>
    {
        public T_M_ConferenceApplyConfiguration()
        {
			ToTable("T_M_CONFERENCEAPPLY");
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
            Property(x => x.Title).HasColumnName("TITLE").IsOptional().HasMaxLength(200);
            Property(x => x.HostDept).HasColumnName("HOSTDEPT").IsOptional().HasMaxLength(200);
            Property(x => x.HostDeptName).HasColumnName("HOSTDEPTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ApplyUser).HasColumnName("APPLYUSER").IsOptional().HasMaxLength(200);
            Property(x => x.ApplyUserName).HasColumnName("APPLYUSERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ApplyDate).HasColumnName("APPLYDATE").IsOptional();
            Property(x => x.MeetingStart).HasColumnName("MEETINGSTART").IsOptional();
            Property(x => x.MeetingEnd).HasColumnName("MEETINGEND").IsOptional();
            Property(x => x.MeetingStartHour).HasColumnName("MEETINGSTARTHOUR").IsOptional().HasMaxLength(200);
            Property(x => x.MeetingEndHour).HasColumnName("MEETINGENDHOUR").IsOptional().HasMaxLength(200);
            Property(x => x.MeetingRoom).HasColumnName("MEETINGROOM").IsOptional().HasMaxLength(200);
            Property(x => x.MeetingRoomName).HasColumnName("MEETINGROOMNAME").IsOptional().HasMaxLength(200);
            Property(x => x.RoomAddress).HasColumnName("ROOMADDRESS").IsOptional().HasMaxLength(200);
            Property(x => x.Host).HasColumnName("HOST").IsOptional().HasMaxLength(200);
            Property(x => x.MainContent).HasColumnName("MAINCONTENT").IsOptional().HasMaxLength(500);
            Property(x => x.JoinUser).HasColumnName("JOINUSER").IsOptional().HasMaxLength(500);
            Property(x => x.JoinUserName).HasColumnName("JOINUSERNAME").IsOptional().HasMaxLength(500);
            Property(x => x.IsNeedBanner).HasColumnName("ISNEEDBANNER").IsOptional().HasMaxLength(200);
            Property(x => x.SelfTotal).HasColumnName("SELFTOTAL").IsOptional();
            Property(x => x.GuestTotal).HasColumnName("GUESTTOTAL").IsOptional();
            Property(x => x.SelfLeader).HasColumnName("SELFLEADER").IsOptional();
            Property(x => x.GuestLeader).HasColumnName("GUESTLEADER").IsOptional();
            Property(x => x.Remark).HasColumnName("REMARK").IsOptional().HasMaxLength(500);
            Property(x => x.Attachments).HasColumnName("ATTACHMENTS").IsOptional().HasMaxLength(500);
            Property(x => x.AuditorInfo).HasColumnName("AUDITORINFO").IsOptional();
            Property(x => x.MeetingAdmin).HasColumnName("MEETINGADMIN").IsOptional();
            Property(x => x.MeetingEndMin).HasColumnName("MEETINGENDMIN").IsOptional();
            Property(x => x.MeetingStartMin).HasColumnName("MEETINGSTARTMIN").IsOptional();
            Property(x => x.HostName).HasColumnName("HOSTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.State).HasColumnName("STATE").IsOptional().HasMaxLength(200);
            Property(x => x.RoomCapacity).HasColumnName("ROOMCAPACITY").IsOptional().HasMaxLength(200);
        }
    }

    // T_M_ConferenceRegist
    internal partial class T_M_ConferenceRegistConfiguration : EntityTypeConfiguration<T_M_ConferenceRegist>
    {
        public T_M_ConferenceRegistConfiguration()
        {
			ToTable("T_M_CONFERENCEREGIST");
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
            Property(x => x.MeetApplyID).HasColumnName("MEETAPPLYID").IsOptional().HasMaxLength(50);
            Property(x => x.MeetApplyIDName).HasColumnName("MEETAPPLYIDNAME").IsOptional().HasMaxLength(50);
            Property(x => x.HostDeptID).HasColumnName("HOSTDEPTID").IsOptional().HasMaxLength(50);
            Property(x => x.HostDeptIDName).HasColumnName("HOSTDEPTIDNAME").IsOptional().HasMaxLength(50);
            Property(x => x.ApplyUserID).HasColumnName("APPLYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ApplyUserIDName).HasColumnName("APPLYUSERIDNAME").IsOptional().HasMaxLength(50);
            Property(x => x.PlanMeetingDate).HasColumnName("PLANMEETINGDATE").IsOptional();
            Property(x => x.PlanMeetingPlace).HasColumnName("PLANMEETINGPLACE").IsOptional().HasMaxLength(50);
            Property(x => x.RegistDate).HasColumnName("REGISTDATE").IsOptional();
            Property(x => x.RegistUserID).HasColumnName("REGISTUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.RegistUserIDName).HasColumnName("REGISTUSERIDNAME").IsOptional().HasMaxLength(50);
            Property(x => x.PlanJoinUserID).HasColumnName("PLANJOINUSERID").IsOptional();
            Property(x => x.PlanJoinUserIDName).HasColumnName("PLANJOINUSERIDNAME").IsOptional();
            Property(x => x.MeetingStartDate).HasColumnName("MEETINGSTARTDATE").IsOptional();
            Property(x => x.MeetingEndDate).HasColumnName("MEETINGENDDATE").IsOptional();
            Property(x => x.MettingPlace).HasColumnName("METTINGPLACE").IsOptional().HasMaxLength(200);
            Property(x => x.JoinUserID).HasColumnName("JOINUSERID").IsOptional();
            Property(x => x.JoinUserIDName).HasColumnName("JOINUSERIDNAME").IsOptional();
            Property(x => x.MeetCost).HasColumnName("MEETCOST").IsOptional();
            Property(x => x.Attachment).HasColumnName("ATTACHMENT").IsOptional();
        }
    }

    // T_M_ConferenceRoom
    internal partial class T_M_ConferenceRoomConfiguration : EntityTypeConfiguration<T_M_ConferenceRoom>
    {
        public T_M_ConferenceRoomConfiguration()
        {
			ToTable("T_M_CONFERENCEROOM");
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
            Property(x => x.RoomName).HasColumnName("ROOMNAME").IsOptional().HasMaxLength(50);
            Property(x => x.LinkName).HasColumnName("LINKNAME").IsOptional().HasMaxLength(50);
            Property(x => x.Capacity).HasColumnName("CAPACITY").IsOptional();
            Property(x => x.ManageDeptID).HasColumnName("MANAGEDEPTID").IsOptional().HasMaxLength(50);
            Property(x => x.ManageDeptIDName).HasColumnName("MANAGEDEPTIDNAME").IsOptional().HasMaxLength(50);
            Property(x => x.RoomAddress).HasColumnName("ROOMADDRESS").IsOptional().HasMaxLength(200);
            Property(x => x.Configuredevice).HasColumnName("CONFIGUREDEVICE").IsOptional();
        }
    }

    // T_M_ConferenceSummary
    internal partial class T_M_ConferenceSummaryConfiguration : EntityTypeConfiguration<T_M_ConferenceSummary>
    {
        public T_M_ConferenceSummaryConfiguration()
        {
			ToTable("T_M_CONFERENCESUMMARY");
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
            Property(x => x.Title).HasColumnName("TITLE").IsOptional().HasMaxLength(200);
            Property(x => x.TitleName).HasColumnName("TITLENAME").IsOptional().HasMaxLength(200);
            Property(x => x.JoinUserID).HasColumnName("JOINUSERID").IsOptional();
            Property(x => x.JoinUserIDName).HasColumnName("JOINUSERIDNAME").IsOptional();
            Property(x => x.HostDepID).HasColumnName("HOSTDEPID").IsOptional().HasMaxLength(50);
            Property(x => x.HostDepIDName).HasColumnName("HOSTDEPIDNAME").IsOptional().HasMaxLength(50);
            Property(x => x.HostUserID).HasColumnName("HOSTUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.HostUserIDName).HasColumnName("HOSTUSERIDNAME").IsOptional().HasMaxLength(50);
            Property(x => x.MeetingStart).HasColumnName("MEETINGSTART").IsOptional();
            Property(x => x.RecordUserID).HasColumnName("RECORDUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.RecordUserIDName).HasColumnName("RECORDUSERIDNAME").IsOptional().HasMaxLength(50);
            Property(x => x.MeetingSummary).HasColumnName("MEETINGSUMMARY").IsOptional().HasMaxLength(200);
            Property(x => x.AboutInfomation).HasColumnName("ABOUTINFOMATION").IsOptional().HasMaxLength(500);
            Property(x => x.IsNeedSigned).HasColumnName("ISNEEDSIGNED").IsOptional().HasMaxLength(50);
            Property(x => x.ApproveUserIDs).HasColumnName("APPROVEUSERIDS").IsOptional().HasMaxLength(50);
            Property(x => x.ApproveUserIDsName).HasColumnName("APPROVEUSERIDSNAME").IsOptional().HasMaxLength(50);
            Property(x => x.RatifyUserIDs).HasColumnName("RATIFYUSERIDS").IsOptional().HasMaxLength(50);
            Property(x => x.RatifyUserIDsName).HasColumnName("RATIFYUSERIDSNAME").IsOptional().HasMaxLength(50);
            Property(x => x.MainContent).HasColumnName("MAINCONTENT").IsOptional();
            Property(x => x.DetailContent).HasColumnName("DETAILCONTENT").IsOptional();
            Property(x => x.CountersignederComment).HasColumnName("COUNTERSIGNEDERCOMMENT").IsOptional();
            Property(x => x.ApprovalerComment).HasColumnName("APPROVALERCOMMENT").IsOptional();
        }
    }

    // T_M_OuterConference
    internal partial class T_M_OuterConferenceConfiguration : EntityTypeConfiguration<T_M_OuterConference>
    {
        public T_M_OuterConferenceConfiguration()
        {
			ToTable("T_M_OUTERCONFERENCE");
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
            Property(x => x.MeetingName).HasColumnName("MEETINGNAME").IsOptional().HasMaxLength(200);
            Property(x => x.MeetingDate).HasColumnName("MEETINGDATE").IsOptional();
            Property(x => x.MeetingPlace).HasColumnName("MEETINGPLACE").IsOptional().HasMaxLength(200);
            Property(x => x.MeetingOrganizers).HasColumnName("MEETINGORGANIZERS").IsOptional().HasMaxLength(200);
            Property(x => x.MeetingDeptName).HasColumnName("MEETINGDEPTNAME").IsOptional().HasMaxLength(50);
            Property(x => x.MeetingDeptNameName).HasColumnName("MEETINGDEPTNAMENAME").IsOptional().HasMaxLength(50);
            Property(x => x.JoinUserNames).HasColumnName("JOINUSERNAMES").IsOptional();
            Property(x => x.CostGrid).HasColumnName("COSTGRID").IsOptional();
            Property(x => x.TotalMoney).HasColumnName("TOTALMONEY").IsOptional().HasPrecision(18,2);
            Property(x => x.ApplyUserComment).HasColumnName("APPLYUSERCOMMENT").IsOptional();
            Property(x => x.DeptLeaderComment).HasColumnName("DEPTLEADERCOMMENT").IsOptional();
            Property(x => x.FinancialAccountingComment).HasColumnName("FINANCIALACCOUNTINGCOMMENT").IsOptional();
            Property(x => x.FinancialLeaderComment).HasColumnName("FINANCIALLEADERCOMMENT").IsOptional();
            Property(x => x.DepartLeaderComment).HasColumnName("DEPARTLEADERCOMMENT").IsOptional();
            Property(x => x.GeneralManagerComment).HasColumnName("GENERALMANAGERCOMMENT").IsOptional();
            Property(x => x.BorrowMoney).HasColumnName("BORROWMONEY").IsOptional().HasPrecision(18,2);
            Property(x => x.BorrowMoneyDate).HasColumnName("BORROWMONEYDATE").IsOptional();
            Property(x => x.ReimbursedMoney).HasColumnName("REIMBURSEDMONEY").IsOptional().HasPrecision(18,2);
            Property(x => x.ReimbursedMoneyDate).HasColumnName("REIMBURSEDMONEYDATE").IsOptional();
        }
    }

    // T_M_OuterConference_CostGrid
    internal partial class T_M_OuterConference_CostGridConfiguration : EntityTypeConfiguration<T_M_OuterConference_CostGrid>
    {
        public T_M_OuterConference_CostGridConfiguration()
        {
			ToTable("T_M_OUTERCONFERENCE_COSTGRID");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.T_M_OuterConferenceID).HasColumnName("T_M_OUTERCONFERENCEID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.Project).HasColumnName("PROJECT").IsOptional().HasMaxLength(50);
            Property(x => x.Price).HasColumnName("PRICE").IsOptional();
            Property(x => x.Amount).HasColumnName("AMOUNT").IsOptional();
            Property(x => x.Subtotal).HasColumnName("SUBTOTAL").IsOptional();
            Property(x => x.Remarks).HasColumnName("REMARKS").IsOptional().HasMaxLength(200);

            // Foreign keys
            HasOptional(a => a.T_M_OuterConference).WithMany(b => b.T_M_OuterConference_CostGrid).HasForeignKey(c => c.T_M_OuterConferenceID); // FK_T_MOuterConference_MeetingCostList_T_MOuterConference
        }
    }

    // T_ProductFileSeal
    internal partial class T_ProductFileSealConfiguration : EntityTypeConfiguration<T_ProductFileSeal>
    {
        public T_ProductFileSealConfiguration()
        {
			ToTable("T_PRODUCTFILESEAL");
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
            Property(x => x.XMMC).HasColumnName("XMMC").IsOptional().HasMaxLength(200);
            Property(x => x.XMBH).HasColumnName("XMBH").IsOptional().HasMaxLength(200);
            Property(x => x.SFYHT).HasColumnName("SFYHT").IsOptional().HasMaxLength(200);
            Property(x => x.SQYYRQ).HasColumnName("SQYYRQ").IsOptional();
            Property(x => x.DHCS).HasColumnName("DHCS").IsOptional();
            Property(x => x.JBR).HasColumnName("JBR").IsOptional().HasMaxLength(200);
            Property(x => x.JBRName).HasColumnName("JBRNAME").IsOptional().HasMaxLength(200);
            Property(x => x.JBRLXDH).HasColumnName("JBRLXDH").IsOptional().HasMaxLength(200);
            Property(x => x.SJJD).HasColumnName("SJJD").IsOptional().HasMaxLength(200);
            Property(x => x.SQCS).HasColumnName("SQCS").IsOptional();
            Property(x => x.SQFS).HasColumnName("SQFS").IsOptional();
            Property(x => x.WJMC).HasColumnName("WJMC").IsOptional().HasMaxLength(200);
            Property(x => x.SJCS).HasColumnName("SJCS").IsOptional();
            Property(x => x.SJFS).HasColumnName("SJFS").IsOptional();
            Property(x => x.WJYT).HasColumnName("WJYT").IsOptional().HasMaxLength(200);
            Property(x => x.YYDW).HasColumnName("YYDW").IsOptional().HasMaxLength(200);
            Property(x => x.YYDWName).HasColumnName("YYDWNAME").IsOptional().HasMaxLength(200);
            Property(x => x.YYR).HasColumnName("YYR").IsOptional().HasMaxLength(200);
            Property(x => x.YYRName).HasColumnName("YYRNAME").IsOptional().HasMaxLength(2000);
            Property(x => x.BM).HasColumnName("BM").IsOptional().HasMaxLength(200);
            Property(x => x.BMName).HasColumnName("BMNAME").IsOptional().HasMaxLength(200);
            Property(x => x.YYHGX).HasColumnName("YYHGX").IsOptional().HasMaxLength(200);
            Property(x => x.EJBM).HasColumnName("EJBM").IsOptional().HasMaxLength(200);
            Property(x => x.EJBMName).HasColumnName("EJBMNAME").IsOptional().HasMaxLength(200);
            Property(x => x.SDRZY).HasColumnName("SDRZY").IsOptional().HasMaxLength(200);
            Property(x => x.YZMC).HasColumnName("YZMC").IsOptional().HasMaxLength(200);
            Property(x => x.YGJSWJ).HasColumnName("YGJSWJ").IsOptional().HasMaxLength(200);
            Property(x => x.YGJSWJZB).HasColumnName("YGJSWJZB").IsOptional();
            Property(x => x.BZ).HasColumnName("BZ").IsOptional().HasMaxLength(500);
            Property(x => x.DHYY).HasColumnName("DHYY").IsOptional().HasMaxLength(200);
            Property(x => x.BHCS).HasColumnName("BHCS").IsOptional();
            Property(x => x.KJZLBYJ).HasColumnName("KJZLBYJ").IsOptional().HasMaxLength(200);
            Property(x => x.KJZLBLDPZRQ).HasColumnName("KJZLBLDPZRQ").IsOptional();
            Property(x => x.IsParentSeal).HasColumnName("ISPARENTSEAL").IsOptional().HasMaxLength(200);
            Property(x => x.BelongDept).HasColumnName("BELONGDEPT").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectSpecialty).HasColumnName("PROJECTSPECIALTY").IsOptional().HasMaxLength(200);
            Property(x => x.S_E_ProductID).HasColumnName("S_E_PRODUCTID").IsOptional().HasMaxLength(200);
            Property(x => x.IsManager).HasColumnName("ISMANAGER").IsOptional().HasMaxLength(200);
            Property(x => x.leader).HasColumnName("LEADER").IsOptional().HasMaxLength(200);
            Property(x => x.FormCode).HasColumnName("FORMCODE").IsOptional().HasMaxLength(200);
            Property(x => x.SealNum).HasColumnName("SEALNUM").IsOptional();
            Property(x => x.SealNumAll).HasColumnName("SEALNUMALL").IsOptional();
            Property(x => x.RealSealNum).HasColumnName("REALSEALNUM").IsOptional();
            Property(x => x.RealSealNumAll).HasColumnName("REALSEALNUMALL").IsOptional();
            Property(x => x.SealDetail).HasColumnName("SEALDETAIL").IsOptional().HasMaxLength(500);
            Property(x => x.ContractNo).HasColumnName("CONTRACTNO").IsOptional().HasMaxLength(200);
            Property(x => x.IsDesignChange).HasColumnName("ISDESIGNCHANGE").IsOptional().HasMaxLength(200);
            Property(x => x.FirstChoose).HasColumnName("FIRSTCHOOSE").IsOptional().HasMaxLength(200);
            Property(x => x.SecondChoose).HasColumnName("SECONDCHOOSE").IsOptional().HasMaxLength(200);
            Property(x => x.Attachments).HasColumnName("ATTACHMENTS").IsOptional().HasMaxLength(500);
            Property(x => x.OtherReason).HasColumnName("OTHERREASON").IsOptional().HasMaxLength(200);
            Property(x => x.OtherForSecond).HasColumnName("OTHERFORSECOND").IsOptional().HasMaxLength(200);
            Property(x => x.SealProductInfo).HasColumnName("SEALPRODUCTINFO").IsOptional();
        }
    }

    // T_ProductFileSeal_CGXX
    internal partial class T_ProductFileSeal_CGXXConfiguration : EntityTypeConfiguration<T_ProductFileSeal_CGXX>
    {
        public T_ProductFileSeal_CGXXConfiguration()
        {
			ToTable("T_PRODUCTFILESEAL_CGXX");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.T_ProductFileSealID).HasColumnName("T_PRODUCTFILESEALID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.CGBH).HasColumnName("CGBH").IsOptional().HasMaxLength(200);
            Property(x => x.ProductFile).HasColumnName("PRODUCTFILE").IsOptional().HasMaxLength(200);
            Property(x => x.ProductFileName).HasColumnName("PRODUCTFILENAME").IsOptional().HasMaxLength(200);
            Property(x => x.FileType).HasColumnName("FILETYPE").IsOptional().HasMaxLength(200);
            Property(x => x.JD).HasColumnName("JD").IsOptional().HasMaxLength(200);
            Property(x => x.CGMC).HasColumnName("CGMC").IsOptional().HasMaxLength(200);
            Property(x => x.DT).HasColumnName("DT").IsOptional().HasMaxLength(200);
            Property(x => x.LX).HasColumnName("LX").IsOptional().HasMaxLength(200);
            Property(x => x.BBH).HasColumnName("BBH").IsOptional().HasMaxLength(200);
            Property(x => x.XSZT).HasColumnName("XSZT").IsOptional().HasMaxLength(200);
            Property(x => x.HQZT).HasColumnName("HQZT").IsOptional().HasMaxLength(200);
            Property(x => x.TJR).HasColumnName("TJR").IsOptional().HasMaxLength(200);
            Property(x => x.TJRQ).HasColumnName("TJRQ").IsOptional().HasMaxLength(200);

            // Foreign keys
            HasOptional(a => a.T_ProductFileSeal).WithMany(b => b.T_ProductFileSeal_CGXX).HasForeignKey(c => c.T_ProductFileSealID); // FK_T_ProductFileSeal_CGXX_T_ProductFileSeal
        }
    }

    // T_ProductFileSeal_SealProductInfo
    internal partial class T_ProductFileSeal_SealProductInfoConfiguration : EntityTypeConfiguration<T_ProductFileSeal_SealProductInfo>
    {
        public T_ProductFileSeal_SealProductInfoConfiguration()
        {
			ToTable("T_PRODUCTFILESEAL_SEALPRODUCTINFO");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.T_ProductFileSealID).HasColumnName("T_PRODUCTFILESEALID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.CGBH).HasColumnName("CGBH").IsOptional().HasMaxLength(200);
            Property(x => x.CGMC).HasColumnName("CGMC").IsOptional().HasMaxLength(200);
            Property(x => x.JD).HasColumnName("JD").IsOptional().HasMaxLength(200);
            Property(x => x.DT).HasColumnName("DT").IsOptional().HasMaxLength(200);
            Property(x => x.LX).HasColumnName("LX").IsOptional().HasMaxLength(200);
            Property(x => x.BBH).HasColumnName("BBH").IsOptional().HasMaxLength(200);
            Property(x => x.MainFile).HasColumnName("MAINFILE").IsOptional().HasMaxLength(200);
            Property(x => x.MainFileName).HasColumnName("MAINFILENAME").IsOptional().HasMaxLength(200);
            Property(x => x.XSZT).HasColumnName("XSZT").IsOptional().HasMaxLength(200);
            Property(x => x.HQZT).HasColumnName("HQZT").IsOptional().HasMaxLength(200);
            Property(x => x.TJR).HasColumnName("TJR").IsOptional().HasMaxLength(200);
            Property(x => x.TJRQ).HasColumnName("TJRQ").IsOptional();

            // Foreign keys
            HasOptional(a => a.T_ProductFileSeal).WithMany(b => b.T_ProductFileSeal_SealProductInfo).HasForeignKey(c => c.T_ProductFileSealID); // FK_T_ProductFileSeal_SealProductInfo_T_ProductFileSeal
        }
    }

    // T_ProductFileSeal_YGJSWJZB
    internal partial class T_ProductFileSeal_YGJSWJZBConfiguration : EntityTypeConfiguration<T_ProductFileSeal_YGJSWJZB>
    {
        public T_ProductFileSeal_YGJSWJZBConfiguration()
        {
			ToTable("T_PRODUCTFILESEAL_YGJSWJZB");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.T_ProductFileSealID).HasColumnName("T_PRODUCTFILESEALID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.YGJSWJMC).HasColumnName("YGJSWJMC").IsOptional().HasMaxLength(200);
            Property(x => x.YGJSWJFJ).HasColumnName("YGJSWJFJ").IsOptional().HasMaxLength(200);
            Property(x => x.YGJSWJFJName).HasColumnName("YGJSWJFJNAME").IsOptional().HasMaxLength(200);

            // Foreign keys
            HasOptional(a => a.T_ProductFileSeal).WithMany(b => b.T_ProductFileSeal_YGJSWJZB).HasForeignKey(c => c.T_ProductFileSealID); // FK_T_ProductFileSeal_YGJSWJZB_T_ProductFileSeal
        }
    }

    // T_Seal_Apply
    internal partial class T_Seal_ApplyConfiguration : EntityTypeConfiguration<T_Seal_Apply>
    {
        public T_Seal_ApplyConfiguration()
        {
			ToTable("T_SEAL_APPLY");
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
            Property(x => x.DeptID).HasColumnName("DEPTID").IsOptional().HasMaxLength(50);
            Property(x => x.DeptIDName).HasColumnName("DEPTIDNAME").IsOptional().HasMaxLength(50);
            Property(x => x.ApplyUserID).HasColumnName("APPLYUSERID").IsOptional().HasMaxLength(200);
            Property(x => x.ApplyUserIDName).HasColumnName("APPLYUSERIDNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ApplyReason).HasColumnName("APPLYREASON").IsOptional().HasMaxLength(500);
            Property(x => x.SealType).HasColumnName("SEALTYPE").IsOptional().HasMaxLength(50);
            Property(x => x.SealInfoID).HasColumnName("SEALINFOID").IsOptional().HasMaxLength(500);
            Property(x => x.SealInfoIDName).HasColumnName("SEALINFOIDNAME").IsOptional().HasMaxLength(2000);
            Property(x => x.SealNumber).HasColumnName("SEALNUMBER").IsOptional();
            Property(x => x.ApplyDate).HasColumnName("APPLYDATE").IsOptional();
            Property(x => x.DeptLeaderComment).HasColumnName("DEPTLEADERCOMMENT").IsOptional();
            Property(x => x.ApplyDeptDepartLeaderComment).HasColumnName("APPLYDEPTDEPARTLEADERCOMMENT").IsOptional();
            Property(x => x.FJCS).HasColumnName("FJCS").IsOptional().HasMaxLength(200);
            Property(x => x.Detail).HasColumnName("DETAIL").IsOptional();
            Property(x => x.DFJ).HasColumnName("DFJ").IsOptional().HasMaxLength(200);
            Property(x => x.SerialNumber).HasColumnName("SERIALNUMBER").IsOptional().HasMaxLength(200);
            Property(x => x.KBPZ).HasColumnName("KBPZ").IsOptional().HasMaxLength(500);
            Property(x => x.XXNR).HasColumnName("XXNR").IsOptional();
            Property(x => x.ZNBMFZR).HasColumnName("ZNBMFZR").IsOptional();
            Property(x => x.XGRYYJ).HasColumnName("XGRYYJ").IsOptional();
            Property(x => x.TCSJ).HasColumnName("TCSJ").IsOptional();
            Property(x => x.ZL).HasColumnName("ZL").IsOptional().HasMaxLength(200);
            Property(x => x.BMFZR).HasColumnName("BMFZR").IsOptional().HasMaxLength(200);
            Property(x => x.BMFZRName).HasColumnName("BMFZRNAME").IsOptional().HasMaxLength(200);
            Property(x => x.YYFS).HasColumnName("YYFS").IsOptional().HasPrecision(18,2);
            Property(x => x.DXMJ).HasColumnName("DXMJ").IsOptional().HasMaxLength(200);
            Property(x => x.Attachment2).HasColumnName("ATTACHMENT2").IsOptional().HasMaxLength(500);
            Property(x => x.Test).HasColumnName("TEST").IsOptional().HasMaxLength(1073741823);
            Property(x => x.CSBM).HasColumnName("CSBM").IsOptional().HasMaxLength(2000);
            Property(x => x.CSBMName).HasColumnName("CSBMNAME").IsOptional().HasMaxLength(2000);
            Property(x => x.groupinfo).HasColumnName("GROUPINFO").IsOptional().HasMaxLength(1073741823);
            Property(x => x.groupuserids).HasColumnName("GROUPUSERIDS").IsOptional().HasMaxLength(1073741823);
        }
    }

    // T_Seal_Apply_Detail
    internal partial class T_Seal_Apply_DetailConfiguration : EntityTypeConfiguration<T_Seal_Apply_Detail>
    {
        public T_Seal_Apply_DetailConfiguration()
        {
			ToTable("T_SEAL_APPLY_DETAIL");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.T_Seal_ApplyID).HasColumnName("T_SEAL_APPLYID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.SealCode).HasColumnName("SEALCODE").IsOptional().HasMaxLength(200);
            Property(x => x.SealName).HasColumnName("SEALNAME").IsOptional().HasMaxLength(200);
            Property(x => x.SealType).HasColumnName("SEALTYPE").IsOptional().HasMaxLength(200);
            Property(x => x.DeptIDName).HasColumnName("DEPTIDNAME").IsOptional().HasMaxLength(200);
            Property(x => x.KeeperIDName).HasColumnName("KEEPERIDNAME").IsOptional().HasMaxLength(200);
            Property(x => x.SealID).HasColumnName("SEALID").IsOptional().HasMaxLength(200);
            Property(x => x.SealNumber).HasColumnName("SEALNUMBER").IsOptional();
            Property(x => x.KeeperID).HasColumnName("KEEPERID").IsOptional().HasMaxLength(200);
            Property(x => x.SealDate).HasColumnName("SEALDATE").IsOptional();
            Property(x => x.SealFJ).HasColumnName("SEALFJ").IsOptional().HasMaxLength(200);
            Property(x => x.SealFJName).HasColumnName("SEALFJNAME").IsOptional().HasMaxLength(200);

            // Foreign keys
            HasOptional(a => a.T_Seal_Apply).WithMany(b => b.T_Seal_Apply_Detail).HasForeignKey(c => c.T_Seal_ApplyID); // FK_T_Seal_Apply_Detail_T_Seal_Apply
        }
    }

    // T_Seal_Borrow
    internal partial class T_Seal_BorrowConfiguration : EntityTypeConfiguration<T_Seal_Borrow>
    {
        public T_Seal_BorrowConfiguration()
        {
			ToTable("T_SEAL_BORROW");
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
            Property(x => x.DeptID).HasColumnName("DEPTID").IsOptional().HasMaxLength(50);
            Property(x => x.DeptIDName).HasColumnName("DEPTIDNAME").IsOptional().HasMaxLength(50);
            Property(x => x.ApplyUserID).HasColumnName("APPLYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ApplyUserIDName).HasColumnName("APPLYUSERIDNAME").IsOptional().HasMaxLength(50);
            Property(x => x.BorrowRange).HasColumnName("BORROWRANGE").IsOptional().HasMaxLength(200);
            Property(x => x.ApplyReason).HasColumnName("APPLYREASON").IsOptional().HasMaxLength(2000);
            Property(x => x.BorrowStartDate).HasColumnName("BORROWSTARTDATE").IsOptional();
            Property(x => x.BorrowEndDate).HasColumnName("BORROWENDDATE").IsOptional();
            Property(x => x.FileName).HasColumnName("FILENAME").IsOptional().HasMaxLength(50);
            Property(x => x.UseSealPaperNumber).HasColumnName("USESEALPAPERNUMBER").IsOptional();
            Property(x => x.ApplyDate).HasColumnName("APPLYDATE").IsOptional();
            Property(x => x.DeptLeaderComment).HasColumnName("DEPTLEADERCOMMENT").IsOptional();
            Property(x => x.GeneralManagerWorkDeptComment).HasColumnName("GENERALMANAGERWORKDEPTCOMMENT").IsOptional();
            Property(x => x.DepartLeaderComment).HasColumnName("DEPARTLEADERCOMMENT").IsOptional();
            Property(x => x.GeneralManagerComment).HasColumnName("GENERALMANAGERCOMMENT").IsOptional();
            Property(x => x.Seal).HasColumnName("SEAL").IsOptional().HasMaxLength(200);
            Property(x => x.SealName).HasColumnName("SEALNAME").IsOptional().HasMaxLength(200);
            Property(x => x.SealType).HasColumnName("SEALTYPE").IsOptional().HasMaxLength(200);
            Property(x => x.BorrowState).HasColumnName("BORROWSTATE").IsOptional().HasMaxLength(200);
            Property(x => x.ReturnDate).HasColumnName("RETURNDATE").IsOptional();
            Property(x => x.Detail).HasColumnName("DETAIL").IsOptional().HasMaxLength(1073741823);
        }
    }

    // T_Seal_Borrow_Detail
    internal partial class T_Seal_Borrow_DetailConfiguration : EntityTypeConfiguration<T_Seal_Borrow_Detail>
    {
        public T_Seal_Borrow_DetailConfiguration()
        {
			ToTable("T_SEAL_BORROW_DETAIL");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.T_Seal_BorrowID).HasColumnName("T_SEAL_BORROWID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.SealCode).HasColumnName("SEALCODE").IsOptional().HasMaxLength(200);
            Property(x => x.SealName).HasColumnName("SEALNAME").IsOptional().HasMaxLength(200);
            Property(x => x.SealType).HasColumnName("SEALTYPE").IsOptional().HasMaxLength(200);
            Property(x => x.SealNumber).HasColumnName("SEALNUMBER").IsOptional();
            Property(x => x.DeptIDName).HasColumnName("DEPTIDNAME").IsOptional().HasMaxLength(200);
            Property(x => x.KeeperIDName).HasColumnName("KEEPERIDNAME").IsOptional().HasMaxLength(200);
            Property(x => x.SealID).HasColumnName("SEALID").IsOptional().HasMaxLength(200);

            // Foreign keys
            HasOptional(a => a.T_Seal_Borrow).WithMany(b => b.T_Seal_Borrow_Detail).HasForeignKey(c => c.T_Seal_BorrowID); // FK_T_Seal_Borrow_Detail_T_Seal_Borrow
        }
    }

    // T_Seal_Change
    internal partial class T_Seal_ChangeConfiguration : EntityTypeConfiguration<T_Seal_Change>
    {
        public T_Seal_ChangeConfiguration()
        {
			ToTable("T_SEAL_CHANGE");
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
            Property(x => x.DeptID).HasColumnName("DEPTID").IsOptional().HasMaxLength(50);
            Property(x => x.DeptIDName).HasColumnName("DEPTIDNAME").IsOptional().HasMaxLength(50);
            Property(x => x.ApplyUserID).HasColumnName("APPLYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ApplyUserIDName).HasColumnName("APPLYUSERIDNAME").IsOptional().HasMaxLength(50);
            Property(x => x.ApplyReason).HasColumnName("APPLYREASON").IsOptional().HasMaxLength(500);
            Property(x => x.SealID).HasColumnName("SEALID").IsOptional().HasMaxLength(500);
            Property(x => x.SealIDName).HasColumnName("SEALIDNAME").IsOptional().HasMaxLength(500);
            Property(x => x.SealStyle).HasColumnName("SEALSTYLE").IsOptional().HasMaxLength(500);
            Property(x => x.SealNumber).HasColumnName("SEALNUMBER").IsOptional();
            Property(x => x.ApplyDate).HasColumnName("APPLYDATE").IsOptional();
            Property(x => x.SealCost).HasColumnName("SEALCOST").IsOptional().HasPrecision(18,2);
            Property(x => x.TotalCost).HasColumnName("TOTALCOST").IsOptional().HasPrecision(18,2);
            Property(x => x.CompleteDate).HasColumnName("COMPLETEDATE").IsOptional();
            Property(x => x.CompletePersonID).HasColumnName("COMPLETEPERSONID").IsOptional().HasMaxLength(50);
            Property(x => x.CompletePersonIDName).HasColumnName("COMPLETEPERSONIDNAME").IsOptional().HasMaxLength(50);
            Property(x => x.DeptLeaderComment).HasColumnName("DEPTLEADERCOMMENT").IsOptional();
            Property(x => x.ApprovalPersonComment).HasColumnName("APPROVALPERSONCOMMENT").IsOptional();
        }
    }

    // T_Seal_Repeal
    internal partial class T_Seal_RepealConfiguration : EntityTypeConfiguration<T_Seal_Repeal>
    {
        public T_Seal_RepealConfiguration()
        {
			ToTable("T_SEAL_REPEAL");
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
            Property(x => x.DeptID).HasColumnName("DEPTID").IsOptional().HasMaxLength(50);
            Property(x => x.DeptIDName).HasColumnName("DEPTIDNAME").IsOptional().HasMaxLength(50);
            Property(x => x.ApplyUserID).HasColumnName("APPLYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ApplyUserIDName).HasColumnName("APPLYUSERIDNAME").IsOptional().HasMaxLength(50);
            Property(x => x.SealInfoID).HasColumnName("SEALINFOID").IsOptional().HasMaxLength(200);
            Property(x => x.SealInfoIDName).HasColumnName("SEALINFOIDNAME").IsOptional().HasMaxLength(200);
            Property(x => x.SealCode).HasColumnName("SEALCODE").IsOptional().HasMaxLength(200);
            Property(x => x.RepealReason).HasColumnName("REPEALREASON").IsOptional().HasMaxLength(500);
            Property(x => x.RepealDate).HasColumnName("REPEALDATE").IsOptional();
            Property(x => x.DeptLeaderComment).HasColumnName("DEPTLEADERCOMMENT").IsOptional();
            Property(x => x.GeneralManagerWorkDeptComment).HasColumnName("GENERALMANAGERWORKDEPTCOMMENT").IsOptional();
            Property(x => x.ApplyDeptDepartLeaderComment).HasColumnName("APPLYDEPTDEPARTLEADERCOMMENT").IsOptional();
            Property(x => x.GeneralManagerComment).HasColumnName("GENERALMANAGERCOMMENT").IsOptional();
            Property(x => x.ChairmanComment).HasColumnName("CHAIRMANCOMMENT").IsOptional();
        }
    }

    // T_Seal_SealInfo
    internal partial class T_Seal_SealInfoConfiguration : EntityTypeConfiguration<T_Seal_SealInfo>
    {
        public T_Seal_SealInfoConfiguration()
        {
			ToTable("T_SEAL_SEALINFO");
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
            Property(x => x.SealCode).HasColumnName("SEALCODE").IsOptional().HasMaxLength(50);
            Property(x => x.SealName).HasColumnName("SEALNAME").IsOptional().HasMaxLength(50);
            Property(x => x.SealType).HasColumnName("SEALTYPE").IsOptional().HasMaxLength(50);
            Property(x => x.StartTime).HasColumnName("STARTTIME").IsOptional();
            Property(x => x.DeptID).HasColumnName("DEPTID").IsOptional().HasMaxLength(50);
            Property(x => x.DeptIDName).HasColumnName("DEPTIDNAME").IsOptional().HasMaxLength(50);
            Property(x => x.KeeperID).HasColumnName("KEEPERID").IsOptional().HasMaxLength(50);
            Property(x => x.KeeperIDName).HasColumnName("KEEPERIDNAME").IsOptional().HasMaxLength(50);
            Property(x => x.Application).HasColumnName("APPLICATION").IsOptional().HasMaxLength(500);
            Property(x => x.Remark).HasColumnName("REMARK").IsOptional().HasMaxLength(500);
            Property(x => x.Attachment).HasColumnName("ATTACHMENT").IsOptional().HasMaxLength(500);
            Property(x => x.State).HasColumnName("STATE").IsOptional().HasMaxLength(50);
            Property(x => x.Borrower).HasColumnName("BORROWER").IsOptional().HasMaxLength(200);
            Property(x => x.BorrowerName).HasColumnName("BORROWERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.BorrowDate).HasColumnName("BORROWDATE").IsOptional();
            Property(x => x.Attachment2).HasColumnName("ATTACHMENT2").IsOptional().HasMaxLength(500);
        }
    }

    // T_Seal_Transfer
    internal partial class T_Seal_TransferConfiguration : EntityTypeConfiguration<T_Seal_Transfer>
    {
        public T_Seal_TransferConfiguration()
        {
			ToTable("T_SEAL_TRANSFER");
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
            Property(x => x.SealInfoID).HasColumnName("SEALINFOID").IsOptional().HasMaxLength(200);
            Property(x => x.SealInfoIDName).HasColumnName("SEALINFOIDNAME").IsOptional().HasMaxLength(200);
            Property(x => x.SealCode).HasColumnName("SEALCODE").IsOptional().HasMaxLength(200);
            Property(x => x.TransferReason).HasColumnName("TRANSFERREASON").IsOptional().HasMaxLength(500);
            Property(x => x.SealModel).HasColumnName("SEALMODEL").IsOptional().HasMaxLength(500);
            Property(x => x.ReceivePersonID).HasColumnName("RECEIVEPERSONID").IsOptional().HasMaxLength(50);
            Property(x => x.ReceivePersonIDName).HasColumnName("RECEIVEPERSONIDNAME").IsOptional().HasMaxLength(50);
            Property(x => x.TransferPersonID).HasColumnName("TRANSFERPERSONID").IsOptional().HasMaxLength(50);
            Property(x => x.TransferPersonIDName).HasColumnName("TRANSFERPERSONIDNAME").IsOptional().HasMaxLength(50);
        }
    }

    // V_DocumentFileAuthority
    internal partial class V_DocumentFileAuthorityConfiguration : EntityTypeConfiguration<V_DocumentFileAuthority>
    {
        public V_DocumentFileAuthorityConfiguration()
        {
			ToTable("V_DOCUMENTFILEAUTHORITY");
            HasKey(x => new { x.ID, x.ReadOnly, x.CanWrite, x.FullControl });

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.DocumentInfoID).HasColumnName("DOCUMENTINFOID").IsOptional().HasMaxLength(50);
            Property(x => x.AuthType).HasColumnName("AUTHTYPE").IsOptional().HasMaxLength(50);
            Property(x => x.RoleType).HasColumnName("ROLETYPE").IsOptional().HasMaxLength(50);
            Property(x => x.RoleCode).HasColumnName("ROLECODE").IsOptional().HasMaxLength(50);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(200);
            Property(x => x.ReadOnly).HasColumnName("READONLY").IsRequired();
            Property(x => x.CanWrite).HasColumnName("CANWRITE").IsRequired();
            Property(x => x.FullControl).HasColumnName("FULLCONTROL").IsRequired();
        }
    }

}

