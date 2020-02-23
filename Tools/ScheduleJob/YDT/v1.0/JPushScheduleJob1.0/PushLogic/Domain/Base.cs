

// This file was automatically generated.
// Do not make changes directly to this file - edit the template instead.
// 
// The following connection settings were used to generate this file
// 
//     Connection String Name: "Base"
//     Connection String:      "Data Source=10.10.1.5;Initial Catalog=WXStandard_Base;User ID=sa;PWD=123.zxc;"

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

namespace PushLogic.Domain
{
    // ************************************************************************
    // Database context
    public partial class BaseContext : DbContext
    {
        public IDbSet<S_A__OrgRes> S_A__OrgRes { get; set; } // S_A__OrgRes
        public IDbSet<S_A__OrgRole> S_A__OrgRole { get; set; } // S_A__OrgRole
        public IDbSet<S_A__OrgRoleUser> S_A__OrgRoleUser { get; set; } // S_A__OrgRoleUser
        public IDbSet<S_A__OrgUser> S_A__OrgUser { get; set; } // S_A__OrgUser
        public IDbSet<S_A__RoleRes> S_A__RoleRes { get; set; } // S_A__RoleRes
        public IDbSet<S_A__RoleUser> S_A__RoleUser { get; set; } // S_A__RoleUser
        public IDbSet<S_A__UserRes> S_A__UserRes { get; set; } // S_A__UserRes
        public IDbSet<S_A_AuthInfo> S_A_AuthInfo { get; set; } // S_A_AuthInfo
        public IDbSet<S_A_AuthLevel> S_A_AuthLevel { get; set; } // S_A_AuthLevel
        public IDbSet<S_A_AuthLog> S_A_AuthLog { get; set; } // S_A_AuthLog
        public IDbSet<S_A_Org> S_A_Org { get; set; } // S_A_Org
        public IDbSet<S_A_Res> S_A_Res { get; set; } // S_A_Res
        public IDbSet<S_A_Role> S_A_Role { get; set; } // S_A_Role
        public IDbSet<S_A_Security> S_A_Security { get; set; } // S_A_Security
        public IDbSet<S_A_User> S_A_User { get; set; } // S_A_User
        public IDbSet<S_A_UserImg> S_A_UserImg { get; set; } // S_A_UserImg
        public IDbSet<S_A_UserLinkMan> S_A_UserLinkMan { get; set; } // S_A_UserLinkMan
        public IDbSet<S_C_Holiday> S_C_Holiday { get; set; } // S_C_Holiday
        public IDbSet<S_D_FormToPDFRegist> S_D_FormToPDFRegist { get; set; } // S_D_FormToPDFRegist
        public IDbSet<S_D_FormToPDFTask> S_D_FormToPDFTask { get; set; } // S_D_FormToPDFTask
        public IDbSet<S_D_ModifyLog> S_D_ModifyLog { get; set; } // S_D_ModifyLog
        public IDbSet<S_D_PDFTask> S_D_PDFTask { get; set; } // S_D_PDFTask
        public IDbSet<S_D_PushTask> S_D_PushTask { get; set; } // S_D_PushTask
        public IDbSet<S_H_AllFeedback> S_H_AllFeedback { get; set; } // S_H_AllFeedback
        public IDbSet<S_H_Calendar> S_H_Calendar { get; set; } // S_H_Calendar
        public IDbSet<S_H_Feedback> S_H_Feedback { get; set; } // S_H_Feedback
        public IDbSet<S_H_ShortCut> S_H_ShortCut { get; set; } // S_H_ShortCut
        public IDbSet<S_I_FriendLink> S_I_FriendLink { get; set; } // S_I_FriendLink
        public IDbSet<S_I_NewsImage> S_I_NewsImage { get; set; } // S_I_NewsImage
        public IDbSet<S_I_NewsImageGroup> S_I_NewsImageGroup { get; set; } // S_I_NewsImageGroup
        public IDbSet<S_I_PublicInformation> S_I_PublicInformation { get; set; } // S_I_PublicInformation
        public IDbSet<S_I_PublicInformCatalog> S_I_PublicInformCatalog { get; set; } // S_I_PublicInformCatalog
        public IDbSet<S_M_Category> S_M_Category { get; set; } // S_M_Category
        public IDbSet<S_M_ConfigManage> S_M_ConfigManage { get; set; } // S_M_ConfigManage
        public IDbSet<S_M_EnumDef> S_M_EnumDef { get; set; } // S_M_EnumDef
        public IDbSet<S_M_EnumItem> S_M_EnumItem { get; set; } // S_M_EnumItem
        public IDbSet<S_M_Field> S_M_Field { get; set; } // S_M_Field
        public IDbSet<S_M_Table> S_M_Table { get; set; } // S_M_Table
        public IDbSet<S_P_DoorBaseTemplate> S_P_DoorBaseTemplate { get; set; } // S_P_DoorBaseTemplate
        public IDbSet<S_P_DoorBlock> S_P_DoorBlock { get; set; } // S_P_DoorBlock
        public IDbSet<S_P_DoorTemplate> S_P_DoorTemplate { get; set; } // S_P_DoorTemplate
        public IDbSet<S_R_DataSet> S_R_DataSet { get; set; } // S_R_DataSet
        public IDbSet<S_R_Define> S_R_Define { get; set; } // S_R_Define
        public IDbSet<S_R_Field> S_R_Field { get; set; } // S_R_Field
        public IDbSet<S_RC_RuleCode> S_RC_RuleCode { get; set; } // S_RC_RuleCode
        public IDbSet<S_RC_RuleCodeData> S_RC_RuleCodeData { get; set; } // S_RC_RuleCodeData
        public IDbSet<S_S_Alarm> S_S_Alarm { get; set; } // S_S_Alarm
        public IDbSet<S_S_MsgBody> S_S_MsgBody { get; set; } // S_S_MsgBody
        public IDbSet<S_S_MsgReceiver> S_S_MsgReceiver { get; set; } // S_S_MsgReceiver
        public IDbSet<S_UI_Form> S_UI_Form { get; set; } // S_UI_Form
        public IDbSet<S_UI_List> S_UI_List { get; set; } // S_UI_List
        public IDbSet<S_UI_ModifyLog> S_UI_ModifyLog { get; set; } // S_UI_ModifyLog
        public IDbSet<S_UI_Selector> S_UI_Selector { get; set; } // S_UI_Selector
        public IDbSet<S_UI_SerialNumber> S_UI_SerialNumber { get; set; } // S_UI_SerialNumber
        public IDbSet<S_UI_Word> S_UI_Word { get; set; } // S_UI_Word
        public IDbSet<S_UIH5_Form> S_UIH5_Form { get; set; } // S_UIH5_Form

        static BaseContext()
        {
            Database.SetInitializer<BaseContext>(null);
        }

        public BaseContext()
            : base("Name=Base")
        {
        }

        public BaseContext(string connectionString) : base(connectionString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new S_A__OrgResConfiguration());
            modelBuilder.Configurations.Add(new S_A__OrgRoleConfiguration());
            modelBuilder.Configurations.Add(new S_A__OrgRoleUserConfiguration());
            modelBuilder.Configurations.Add(new S_A__OrgUserConfiguration());
            modelBuilder.Configurations.Add(new S_A__RoleResConfiguration());
            modelBuilder.Configurations.Add(new S_A__RoleUserConfiguration());
            modelBuilder.Configurations.Add(new S_A__UserResConfiguration());
            modelBuilder.Configurations.Add(new S_A_AuthInfoConfiguration());
            modelBuilder.Configurations.Add(new S_A_AuthLevelConfiguration());
            modelBuilder.Configurations.Add(new S_A_AuthLogConfiguration());
            modelBuilder.Configurations.Add(new S_A_OrgConfiguration());
            modelBuilder.Configurations.Add(new S_A_ResConfiguration());
            modelBuilder.Configurations.Add(new S_A_RoleConfiguration());
            modelBuilder.Configurations.Add(new S_A_SecurityConfiguration());
            modelBuilder.Configurations.Add(new S_A_UserConfiguration());
            modelBuilder.Configurations.Add(new S_A_UserImgConfiguration());
            modelBuilder.Configurations.Add(new S_A_UserLinkManConfiguration());
            modelBuilder.Configurations.Add(new S_C_HolidayConfiguration());
            modelBuilder.Configurations.Add(new S_D_FormToPDFRegistConfiguration());
            modelBuilder.Configurations.Add(new S_D_FormToPDFTaskConfiguration());
            modelBuilder.Configurations.Add(new S_D_ModifyLogConfiguration());
            modelBuilder.Configurations.Add(new S_D_PDFTaskConfiguration());
            modelBuilder.Configurations.Add(new S_D_PushTaskConfiguration());
            modelBuilder.Configurations.Add(new S_H_AllFeedbackConfiguration());
            modelBuilder.Configurations.Add(new S_H_CalendarConfiguration());
            modelBuilder.Configurations.Add(new S_H_FeedbackConfiguration());
            modelBuilder.Configurations.Add(new S_H_ShortCutConfiguration());
            modelBuilder.Configurations.Add(new S_I_FriendLinkConfiguration());
            modelBuilder.Configurations.Add(new S_I_NewsImageConfiguration());
            modelBuilder.Configurations.Add(new S_I_NewsImageGroupConfiguration());
            modelBuilder.Configurations.Add(new S_I_PublicInformationConfiguration());
            modelBuilder.Configurations.Add(new S_I_PublicInformCatalogConfiguration());
            modelBuilder.Configurations.Add(new S_M_CategoryConfiguration());
            modelBuilder.Configurations.Add(new S_M_ConfigManageConfiguration());
            modelBuilder.Configurations.Add(new S_M_EnumDefConfiguration());
            modelBuilder.Configurations.Add(new S_M_EnumItemConfiguration());
            modelBuilder.Configurations.Add(new S_M_FieldConfiguration());
            modelBuilder.Configurations.Add(new S_M_TableConfiguration());
            modelBuilder.Configurations.Add(new S_P_DoorBaseTemplateConfiguration());
            modelBuilder.Configurations.Add(new S_P_DoorBlockConfiguration());
            modelBuilder.Configurations.Add(new S_P_DoorTemplateConfiguration());
            modelBuilder.Configurations.Add(new S_R_DataSetConfiguration());
            modelBuilder.Configurations.Add(new S_R_DefineConfiguration());
            modelBuilder.Configurations.Add(new S_R_FieldConfiguration());
            modelBuilder.Configurations.Add(new S_RC_RuleCodeConfiguration());
            modelBuilder.Configurations.Add(new S_RC_RuleCodeDataConfiguration());
            modelBuilder.Configurations.Add(new S_S_AlarmConfiguration());
            modelBuilder.Configurations.Add(new S_S_MsgBodyConfiguration());
            modelBuilder.Configurations.Add(new S_S_MsgReceiverConfiguration());
            modelBuilder.Configurations.Add(new S_UI_FormConfiguration());
            modelBuilder.Configurations.Add(new S_UI_ListConfiguration());
            modelBuilder.Configurations.Add(new S_UI_ModifyLogConfiguration());
            modelBuilder.Configurations.Add(new S_UI_SelectorConfiguration());
            modelBuilder.Configurations.Add(new S_UI_SerialNumberConfiguration());
            modelBuilder.Configurations.Add(new S_UI_WordConfiguration());
            modelBuilder.Configurations.Add(new S_UIH5_FormConfiguration());
        }
    }

    // ************************************************************************
    // POCO classes

	/// <summary></summary>	
	[Description("")]
    public partial class S_A__OrgRes
    {
		/// <summary></summary>	
		[Description("")]
        public string ResID { get{return _ResID;} set{_ResID = value??"";} } // ResID (Primary key)
		private string _ResID="";
		/// <summary></summary>	
		[Description("")]
        public string OrgID { get{return _OrgID;} set{_OrgID = value??"";} } // OrgID (Primary key)
		private string _OrgID="";

        // Foreign keys
		[JsonIgnore]
        public virtual S_A_Res S_A_Res { get; set; } //  ResID - FK_S_A__OrgRes_S_A_Res
		[JsonIgnore]
        public virtual S_A_Org S_A_Org { get; set; } //  OrgID - FK_S_A__OrgRes_S_A_Org
    }

	/// <summary>组织和角色关系表</summary>	
	[Description("组织和角色关系表")]
    public partial class S_A__OrgRole
    {
		/// <summary>角色ID</summary>	
		[Description("角色ID")]
        public string RoleID { get{return _RoleID;} set{_RoleID = value??"";} } // RoleID (Primary key)
		private string _RoleID="";
		/// <summary>组织ID</summary>	
		[Description("组织ID")]
        public string OrgID { get{return _OrgID;} set{_OrgID = value??"";} } // OrgID (Primary key)
		private string _OrgID="";

        // Foreign keys
		[JsonIgnore]
        public virtual S_A_Role S_A_Role { get; set; } //  RoleID - FK_A_OrgRole_ARole
		[JsonIgnore]
        public virtual S_A_Org S_A_Org { get; set; } //  OrgID - FK_A_OrgRole_AOrg
    }

	/// <summary></summary>	
	[Description("")]
    public partial class S_A__OrgRoleUser
    {
		/// <summary></summary>	
		[Description("")]
        public string OrgID { get{return _OrgID;} set{_OrgID = value??"";} } // OrgID (Primary key)
		private string _OrgID="";
		/// <summary></summary>	
		[Description("")]
        public string RoleID { get{return _RoleID;} set{_RoleID = value??"";} } // RoleID (Primary key)
		private string _RoleID="";
		/// <summary></summary>	
		[Description("")]
        public string UserID { get{return _UserID;} set{_UserID = value??"";} } // UserID (Primary key)
		private string _UserID="";

        // Foreign keys
		[JsonIgnore]
        public virtual S_A_Role S_A_Role { get; set; } //  RoleID - FK_S_A__OrgRoleUser_S_A_Role
    }

	/// <summary>组织和用户关系表</summary>	
	[Description("组织和用户关系表")]
    public partial class S_A__OrgUser
    {
		/// <summary>组织ID</summary>	
		[Description("组织ID")]
        public string OrgID { get{return _OrgID;} set{_OrgID = value??"";} } // OrgID (Primary key)
		private string _OrgID="";
		/// <summary>用户ID</summary>	
		[Description("用户ID")]
        public string UserID { get{return _UserID;} set{_UserID = value??"";} } // UserID (Primary key)
		private string _UserID="";

        // Foreign keys
		[JsonIgnore]
        public virtual S_A_Org S_A_Org { get; set; } //  OrgID - FK_A_OrgUser_AOrg
		[JsonIgnore]
        public virtual S_A_User S_A_User { get; set; } //  UserID - FK_A_OrgUser_AUser
    }

	/// <summary>角色和权限资源关系表</summary>	
	[Description("角色和权限资源关系表")]
    public partial class S_A__RoleRes
    {
		/// <summary>权限资源ID</summary>	
		[Description("权限资源ID")]
        public string ResID { get{return _ResID;} set{_ResID = value??"";} } // ResID (Primary key)
		private string _ResID="";
		/// <summary>角色ID</summary>	
		[Description("角色ID")]
        public string RoleID { get{return _RoleID;} set{_RoleID = value??"";} } // RoleID (Primary key)
		private string _RoleID="";

        // Foreign keys
		[JsonIgnore]
        public virtual S_A_Res S_A_Res { get; set; } //  ResID - FK_S_A__RoleRes_S_A_Res
		[JsonIgnore]
        public virtual S_A_Role S_A_Role { get; set; } //  RoleID - FK_S_A__RoleRes_S_A_Role
    }

	/// <summary>角色和用户关系表</summary>	
	[Description("角色和用户关系表")]
    public partial class S_A__RoleUser
    {
		/// <summary>角色ID</summary>	
		[Description("角色ID")]
        public string RoleID { get{return _RoleID;} set{_RoleID = value??"";} } // RoleID (Primary key)
		private string _RoleID="";
		/// <summary>用户ID</summary>	
		[Description("用户ID")]
        public string UserID { get{return _UserID;} set{_UserID = value??"";} } // UserID (Primary key)
		private string _UserID="";

        // Foreign keys
		[JsonIgnore]
        public virtual S_A_Role S_A_Role { get; set; } //  RoleID - FK_A_RoleUser_ARole
		[JsonIgnore]
        public virtual S_A_User S_A_User { get; set; } //  UserID - FK_A_RoleUser_AUser
    }

	/// <summary></summary>	
	[Description("")]
    public partial class S_A__UserRes
    {
		/// <summary></summary>	
		[Description("")]
        public string UserID { get{return _UserID;} set{_UserID = value??"";} } // UserID (Primary key)
		private string _UserID="";
		/// <summary></summary>	
		[Description("")]
        public string ResID { get{return _ResID;} set{_ResID = value??"";} } // ResID (Primary key)
		private string _ResID="";
		/// <summary></summary>	
		[Description("")]
        public string DenyAuth { get{return _DenyAuth;} set{_DenyAuth = value??"";} } // DenyAuth
		private string _DenyAuth="";

        // Foreign keys
		[JsonIgnore]
        public virtual S_A_User S_A_User { get; set; } //  UserID - FK_S_A__UserRes_S_A_User
		[JsonIgnore]
        public virtual S_A_Res S_A_Res { get; set; } //  ResID - FK_S_A__UserRes_S_A_Res
    }

	/// <summary>权限模块信息表</summary>	
	[Description("权限模块信息表")]
    public partial class S_A_AuthInfo
    {
		/// <summary>ID</summary>	
		[Description("ID")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID (Primary key)
		private string _ID="";
		/// <summary>名称</summary>	
		[Description("名称")]
        public string Name { get{return _Name;} set{_Name = value??"";} } // Name
		private string _Name="";
		/// <summary>组织机构树的根</summary>	
		[Description("组织机构树的根")]
        public string OrgRootFullID { get{return _OrgRootFullID;} set{_OrgRootFullID = value??"";} } // OrgRootFullID
		private string _OrgRootFullID="";
		/// <summary>组织机构根</summary>	
		[Description("组织机构根")]
        public string ResRootFullID { get{return _ResRootFullID;} set{_ResRootFullID = value??"";} } // ResRootFullID
		private string _ResRootFullID="";
		/// <summary>角色组ID</summary>	
		[Description("角色组ID")]
        public string RoleGroupID { get{return _RoleGroupID;} set{_RoleGroupID = value??"";} } // RoleGroupID
		private string _RoleGroupID="";
		/// <summary>用户组ID</summary>	
		[Description("用户组ID")]
        public string UserGroupID { get{return _UserGroupID;} set{_UserGroupID = value??"";} } // UserGroupID
		private string _UserGroupID="";
		/// <summary>描述</summary>	
		[Description("描述")]
        public string Description { get{return _Description;} set{_Description = value??"";} } // Description
		private string _Description="";
    }

	/// <summary>分级授权</summary>	
	[Description("分级授权")]
    public partial class S_A_AuthLevel
    {
		/// <summary>ID</summary>	
		[Description("ID")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID (Primary key)
		private string _ID="";
		/// <summary>用户ID</summary>	
		[Description("用户ID")]
        public string UserID { get{return _UserID;} set{_UserID = value??"";} } // UserID
		private string _UserID="";
		/// <summary>用户姓名</summary>	
		[Description("用户姓名")]
        public string UserName { get{return _UserName;} set{_UserName = value??"";} } // UserName
		private string _UserName="";
		/// <summary>可以授权的菜单根</summary>	
		[Description("可以授权的菜单根")]
        public string MenuRootFullID { get{return _MenuRootFullID;} set{_MenuRootFullID = value??"";} } // MenuRootFullID
		private string _MenuRootFullID="";
		/// <summary>可以授权的菜单根</summary>	
		[Description("可以授权的菜单根")]
        public string MenuRootName { get{return _MenuRootName;} set{_MenuRootName = value??"";} } // MenuRootName
		private string _MenuRootName="";
		/// <summary>可以授权的规则根</summary>	
		[Description("可以授权的规则根")]
        public string RuleRootFullID { get{return _RuleRootFullID;} set{_RuleRootFullID = value??"";} } // RuleRootFullID
		private string _RuleRootFullID="";
		/// <summary>可以授权的规则根</summary>	
		[Description("可以授权的规则根")]
        public string RuleRootName { get{return _RuleRootName;} set{_RuleRootName = value??"";} } // RuleRootName
		private string _RuleRootName="";
    }

	/// <summary></summary>	
	[Description("")]
    public partial class S_A_AuthLog
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID (Primary key)
		private string _ID="";
		/// <summary></summary>	
		[Description("")]
        public string Operation { get{return _Operation;} set{_Operation = value??"";} } // Operation
		private string _Operation="";
		/// <summary></summary>	
		[Description("")]
        public string OperationTarget { get{return _OperationTarget;} set{_OperationTarget = value??"";} } // OperationTarget
		private string _OperationTarget="";
		/// <summary></summary>	
		[Description("")]
        public string RelateData { get{return _RelateData;} set{_RelateData = value??"";} } // RelateData
		private string _RelateData="";
		/// <summary></summary>	
		[Description("")]
        public string ModifyUserName { get{return _ModifyUserName;} set{_ModifyUserName = value??"";} } // ModifyUserName
		private string _ModifyUserName="";
		/// <summary></summary>	
		[Description("")]
        public DateTime? ModifyTime { get; set; } // ModifyTime
		/// <summary></summary>	
		[Description("")]
        public string ClientIP { get{return _ClientIP;} set{_ClientIP = value??"";} } // ClientIP
		private string _ClientIP="";
    }

	/// <summary>组织机构表</summary>	
	[Description("组织机构表")]
    public partial class S_A_Org
    {
		/// <summary>ID</summary>	
		[Description("ID")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID (Primary key)
		private string _ID="";
		/// <summary>父ID</summary>	
		[Description("父ID")]
        public string ParentID { get{return _ParentID;} set{_ParentID = value??"";} } // ParentID
		private string _ParentID="";
		/// <summary>全路径ID</summary>	
		[Description("全路径ID")]
        public string FullID { get{return _FullID;} set{_FullID = value??"";} } // FullID
		private string _FullID="";
		/// <summary>编号</summary>	
		[Description("编号")]
        public string Code { get{return _Code;} set{_Code = value??"";} } // Code
		private string _Code="";
		/// <summary>名称（简称）</summary>	
		[Description("名称（简称）")]
        public string Name { get{return _Name;} set{_Name = value??"";} } // Name
		private string _Name="";
		/// <summary>类型</summary>	
		[Description("类型")]
        public string Type { get{return _Type;} set{_Type = value??"";} } // Type
		private string _Type="";
		/// <summary>排序索引</summary>	
		[Description("排序索引")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary>描述</summary>	
		[Description("描述")]
        public string Description { get{return _Description;} set{_Description = value??"";} } // Description
		private string _Description="";
		/// <summary>是否已经删除</summary>	
		[Description("是否已经删除")]
        public string IsDeleted { get{return _IsDeleted;} set{_IsDeleted = value??"";} } // IsDeleted
		private string _IsDeleted="";
		/// <summary>删除时间</summary>	
		[Description("删除时间")]
        public DateTime? DeleteTime { get; set; } // DeleteTime
		/// <summary>全称</summary>	
		[Description("全称")]
        public string ShortName { get{return _ShortName;} set{_ShortName = value??"";} } // ShortName
		private string _ShortName="";
		/// <summary>性质</summary>	
		[Description("性质")]
        public string Character { get{return _Character;} set{_Character = value??"";} } // Character
		private string _Character="";
		/// <summary>所在地</summary>	
		[Description("所在地")]
        public string Location { get{return _Location;} set{_Location = value??"";} } // Location
		private string _Location="";

        // Reverse navigation
		[JsonIgnore]
        public virtual ICollection<S_A__OrgRes> S_A__OrgRes { get { onS_A__OrgResGetting(); return _S_A__OrgRes;} }
		private ICollection<S_A__OrgRes> _S_A__OrgRes;
		partial void onS_A__OrgResGetting();

		[JsonIgnore]
        public virtual ICollection<S_A__OrgRole> S_A__OrgRole { get { onS_A__OrgRoleGetting(); return _S_A__OrgRole;} }
		private ICollection<S_A__OrgRole> _S_A__OrgRole;
		partial void onS_A__OrgRoleGetting();

		[JsonIgnore]
        public virtual ICollection<S_A__OrgUser> S_A__OrgUser { get { onS_A__OrgUserGetting(); return _S_A__OrgUser;} }
		private ICollection<S_A__OrgUser> _S_A__OrgUser;
		partial void onS_A__OrgUserGetting();


        public S_A_Org()
        {
			IsDeleted = "0";
            _S_A__OrgRes = new List<S_A__OrgRes>();
            _S_A__OrgRole = new List<S_A__OrgRole>();
            _S_A__OrgUser = new List<S_A__OrgUser>();
        }
    }

	/// <summary>权限资源表</summary>	
	[Description("权限资源表")]
    public partial class S_A_Res
    {
		/// <summary>ID</summary>	
		[Description("ID")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID (Primary key)
		private string _ID="";
		/// <summary>父ID</summary>	
		[Description("父ID")]
        public string ParentID { get{return _ParentID;} set{_ParentID = value??"";} } // ParentID
		private string _ParentID="";
		/// <summary>全ID</summary>	
		[Description("全ID")]
        public string FullID { get{return _FullID;} set{_FullID = value??"";} } // FullID
		private string _FullID="";
		/// <summary></summary>	
		[Description("")]
        public string Code { get{return _Code;} set{_Code = value??"";} } // Code
		private string _Code="";
		/// <summary>名称</summary>	
		[Description("名称")]
        public string Name { get{return _Name;} set{_Name = value??"";} } // Name
		private string _Name="";
		/// <summary>类型</summary>	
		[Description("类型")]
        public string Type { get{return _Type;} set{_Type = value??"";} } // Type
		private string _Type="";
		/// <summary>Url</summary>	
		[Description("Url")]
        public string Url { get{return _Url;} set{_Url = value??"";} } // Url
		private string _Url="";
		/// <summary>图标Url</summary>	
		[Description("图标Url")]
        public string IconUrl { get{return _IconUrl;} set{_IconUrl = value??"";} } // IconUrl
		private string _IconUrl="";
		/// <summary>打开目标</summary>	
		[Description("打开目标")]
        public string Target { get{return _Target;} set{_Target = value??"";} } // Target
		private string _Target="";
		/// <summary>按钮ID</summary>	
		[Description("按钮ID")]
        public string ButtonID { get{return _ButtonID;} set{_ButtonID = value??"";} } // ButtonID
		private string _ButtonID="";
		/// <summary>数据过滤</summary>	
		[Description("数据过滤")]
        public string DataFilter { get{return _DataFilter;} set{_DataFilter = value??"";} } // DataFilter
		private string _DataFilter="";
		/// <summary>描述</summary>	
		[Description("描述")]
        public string Description { get{return _Description;} set{_Description = value??"";} } // Description
		private string _Description="";
		/// <summary>排序索引</summary>	
		[Description("排序索引")]
        public double? SortIndex { get; set; } // SortIndex

        // Reverse navigation
		[JsonIgnore]
        public virtual ICollection<S_A__OrgRes> S_A__OrgRes { get { onS_A__OrgResGetting(); return _S_A__OrgRes;} }
		private ICollection<S_A__OrgRes> _S_A__OrgRes;
		partial void onS_A__OrgResGetting();

		[JsonIgnore]
        public virtual ICollection<S_A__RoleRes> S_A__RoleRes { get { onS_A__RoleResGetting(); return _S_A__RoleRes;} }
		private ICollection<S_A__RoleRes> _S_A__RoleRes;
		partial void onS_A__RoleResGetting();

		[JsonIgnore]
        public virtual ICollection<S_A__UserRes> S_A__UserRes { get { onS_A__UserResGetting(); return _S_A__UserRes;} }
		private ICollection<S_A__UserRes> _S_A__UserRes;
		partial void onS_A__UserResGetting();


        public S_A_Res()
        {
			Type = "Menu";
            _S_A__OrgRes = new List<S_A__OrgRes>();
            _S_A__RoleRes = new List<S_A__RoleRes>();
            _S_A__UserRes = new List<S_A__UserRes>();
        }
    }

	/// <summary>角色表</summary>	
	[Description("角色表")]
    public partial class S_A_Role
    {
		/// <summary>ID</summary>	
		[Description("ID")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID (Primary key)
		private string _ID="";
		/// <summary>角色组ID</summary>	
		[Description("角色组ID")]
        public string GroupID { get{return _GroupID;} set{_GroupID = value??"";} } // GroupID
		private string _GroupID="";
		/// <summary>编号</summary>	
		[Description("编号")]
        public string Code { get{return _Code;} set{_Code = value??"";} } // Code
		private string _Code="";
		/// <summary>名称</summary>	
		[Description("名称")]
        public string Name { get{return _Name;} set{_Name = value??"";} } // Name
		private string _Name="";
		/// <summary>类型</summary>	
		[Description("类型")]
        public string Type { get{return _Type;} set{_Type = value??"";} } // Type
		private string _Type="";
		/// <summary>描述</summary>	
		[Description("描述")]
        public string Description { get{return _Description;} set{_Description = value??"";} } // Description
		private string _Description="";

        // Reverse navigation
		[JsonIgnore]
        public virtual ICollection<S_A__OrgRole> S_A__OrgRole { get { onS_A__OrgRoleGetting(); return _S_A__OrgRole;} }
		private ICollection<S_A__OrgRole> _S_A__OrgRole;
		partial void onS_A__OrgRoleGetting();

		[JsonIgnore]
        public virtual ICollection<S_A__OrgRoleUser> S_A__OrgRoleUser { get { onS_A__OrgRoleUserGetting(); return _S_A__OrgRoleUser;} }
		private ICollection<S_A__OrgRoleUser> _S_A__OrgRoleUser;
		partial void onS_A__OrgRoleUserGetting();

		[JsonIgnore]
        public virtual ICollection<S_A__RoleRes> S_A__RoleRes { get { onS_A__RoleResGetting(); return _S_A__RoleRes;} }
		private ICollection<S_A__RoleRes> _S_A__RoleRes;
		partial void onS_A__RoleResGetting();

		[JsonIgnore]
        public virtual ICollection<S_A__RoleUser> S_A__RoleUser { get { onS_A__RoleUserGetting(); return _S_A__RoleUser;} }
		private ICollection<S_A__RoleUser> _S_A__RoleUser;
		partial void onS_A__RoleUserGetting();


        public S_A_Role()
        {
            _S_A__OrgRole = new List<S_A__OrgRole>();
            _S_A__OrgRoleUser = new List<S_A__OrgRoleUser>();
            _S_A__RoleRes = new List<S_A__RoleRes>();
            _S_A__RoleUser = new List<S_A__RoleUser>();
        }
    }

	/// <summary>三权分离表</summary>	
	[Description("三权分离表")]
    public partial class S_A_Security
    {
		/// <summary>ID</summary>	
		[Description("ID")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID (Primary key)
		private string _ID="";
		/// <summary>超级管理员账号,必须为administrator</summary>	
		[Description("超级管理员账号,必须为administrator")]
        public string SuperAdmin { get{return _SuperAdmin;} set{_SuperAdmin = value??"";} } // SuperAdmin
		private string _SuperAdmin="";
		/// <summary>超级管理员密码</summary>	
		[Description("超级管理员密码")]
        public string SuperAdminPwd { get{return _SuperAdminPwd;} set{_SuperAdminPwd = value??"";} } // SuperAdminPwd
		private string _SuperAdminPwd="";
		/// <summary>超级管理员密码安全</summary>	
		[Description("超级管理员密码安全")]
        public string SuperAdminSecurity { get{return _SuperAdminSecurity;} set{_SuperAdminSecurity = value??"";} } // SuperAdminSecurity
		private string _SuperAdminSecurity="";
		/// <summary>超级管理员密码修改时间</summary>	
		[Description("超级管理员密码修改时间")]
        public DateTime? SuperAdminModifyTime { get; set; } // SuperAdminModifyTime
		/// <summary>管理员IDs</summary>	
		[Description("管理员IDs")]
        public string AdminIDs { get{return _AdminIDs;} set{_AdminIDs = value??"";} } // AdminIDs
		private string _AdminIDs="";
		/// <summary>管理员Names</summary>	
		[Description("管理员Names")]
        public string AdminNames { get{return _AdminNames;} set{_AdminNames = value??"";} } // AdminNames
		private string _AdminNames="";
		/// <summary>管理员修改时间</summary>	
		[Description("管理员修改时间")]
        public DateTime? AdminModifyTime { get; set; } // AdminModifyTime
		/// <summary>管理员安全</summary>	
		[Description("管理员安全")]
        public string AdminSecurity { get{return _AdminSecurity;} set{_AdminSecurity = value??"";} } // AdminSecurity
		private string _AdminSecurity="";
    }

	/// <summary>用户表</summary>	
	[Description("用户表")]
    public partial class S_A_User
    {
		/// <summary>ID</summary>	
		[Description("ID")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID (Primary key)
		private string _ID="";
		/// <summary>用户组ID</summary>	
		[Description("用户组ID")]
        public string GroupID { get{return _GroupID;} set{_GroupID = value??"";} } // GroupID
		private string _GroupID="";
		/// <summary>帐号</summary>	
		[Description("帐号")]
        public string Code { get{return _Code;} set{_Code = value??"";} } // Code
		private string _Code="";
		/// <summary>姓名</summary>	
		[Description("姓名")]
        public string Name { get{return _Name;} set{_Name = value??"";} } // Name
		private string _Name="";
		/// <summary>工号</summary>	
		[Description("工号")]
        public string WorkNo { get{return _WorkNo;} set{_WorkNo = value??"";} } // WorkNo
		private string _WorkNo="";
		/// <summary>密码</summary>	
		[Description("密码")]
        public string Password { get{return _Password;} set{_Password = value??"";} } // Password
		private string _Password="";
		/// <summary>性别</summary>	
		[Description("性别")]
        public string Sex { get{return _Sex;} set{_Sex = value??"";} } // Sex
		private string _Sex="";
		/// <summary>描述</summary>	
		[Description("描述")]
        public string Description { get{return _Description;} set{_Description = value??"";} } // Description
		private string _Description="";
		/// <summary>入职日期</summary>	
		[Description("入职日期")]
        public DateTime? InDate { get; set; } // InDate
		/// <summary>离职日期</summary>	
		[Description("离职日期")]
        public DateTime? OutDate { get; set; } // OutDate
		/// <summary>电话</summary>	
		[Description("电话")]
        public string Phone { get{return _Phone;} set{_Phone = value??"";} } // Phone
		private string _Phone="";
		/// <summary>手机</summary>	
		[Description("手机")]
        public string MobilePhone { get{return _MobilePhone;} set{_MobilePhone = value??"";} } // MobilePhone
		private string _MobilePhone="";
		/// <summary>Email</summary>	
		[Description("Email")]
        public string Email { get{return _Email;} set{_Email = value??"";} } // Email
		private string _Email="";
		/// <summary>地址</summary>	
		[Description("地址")]
        public string Address { get{return _Address;} set{_Address = value??"";} } // Address
		private string _Address="";
		/// <summary>排序索引</summary>	
		[Description("排序索引")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary>最后登录时间</summary>	
		[Description("最后登录时间")]
        public string LastLoginTime { get{return _LastLoginTime;} set{_LastLoginTime = value??"";} } // LastLoginTime
		private string _LastLoginTime="";
		/// <summary>最后登录IP</summary>	
		[Description("最后登录IP")]
        public string LastLoginIP { get{return _LastLoginIP;} set{_LastLoginIP = value??"";} } // LastLoginIP
		private string _LastLoginIP="";
		/// <summary>最后登录SessionID</summary>	
		[Description("最后登录SessionID")]
        public string LastSessionID { get{return _LastSessionID;} set{_LastSessionID = value??"";} } // LastSessionID
		private string _LastSessionID="";
		/// <summary>登录错误次数</summary>	
		[Description("登录错误次数")]
        public int? ErrorCount { get; set; } // ErrorCount
		/// <summary></summary>	
		[Description("")]
        public DateTime? ErrorTime { get; set; } // ErrorTime
		/// <summary>是否已经删除</summary>	
		[Description("是否已经删除")]
        public string IsDeleted { get{return _IsDeleted;} set{_IsDeleted = value??"";} } // IsDeleted
		private string _IsDeleted="";
		/// <summary>删除时间</summary>	
		[Description("删除时间")]
        public DateTime? DeleteTime { get; set; } // DeleteTime
		/// <summary>当前项目ID</summary>	
		[Description("当前项目ID")]
        public string PrjID { get{return _PrjID;} set{_PrjID = value??"";} } // PrjID
		private string _PrjID="";
		/// <summary>当前项目名称</summary>	
		[Description("当前项目名称")]
        public string PrjName { get{return _PrjName;} set{_PrjName = value??"";} } // PrjName
		private string _PrjName="";
		/// <summary>当前部门ID</summary>	
		[Description("当前部门ID")]
        public string DeptID { get{return _DeptID;} set{_DeptID = value??"";} } // DeptID
		private string _DeptID="";
		/// <summary>当前部门全ID</summary>	
		[Description("当前部门全ID")]
        public string DeptFullID { get{return _DeptFullID;} set{_DeptFullID = value??"";} } // DeptFullID
		private string _DeptFullID="";
		/// <summary>当前部门名称</summary>	
		[Description("当前部门名称")]
        public string DeptName { get{return _DeptName;} set{_DeptName = value??"";} } // DeptName
		private string _DeptName="";
		/// <summary></summary>	
		[Description("")]
        public string RTX { get{return _RTX;} set{_RTX = value??"";} } // RTX
		private string _RTX="";
		/// <summary>最后更新时间</summary>	
		[Description("最后更新时间")]
        public DateTime? ModifyTime { get; set; } // ModifyTime
		/// <summary></summary>	
		[Description("")]
        public string Weixin { get{return _Weixin;} set{_Weixin = value??"";} } // Weixin
		private string _Weixin="";

        // Reverse navigation
		[JsonIgnore]
        public virtual ICollection<S_A__OrgUser> S_A__OrgUser { get { onS_A__OrgUserGetting(); return _S_A__OrgUser;} }
		private ICollection<S_A__OrgUser> _S_A__OrgUser;
		partial void onS_A__OrgUserGetting();

		[JsonIgnore]
        public virtual ICollection<S_A__RoleUser> S_A__RoleUser { get { onS_A__RoleUserGetting(); return _S_A__RoleUser;} }
		private ICollection<S_A__RoleUser> _S_A__RoleUser;
		partial void onS_A__RoleUserGetting();

		[JsonIgnore]
        public virtual ICollection<S_A__UserRes> S_A__UserRes { get { onS_A__UserResGetting(); return _S_A__UserRes;} }
		private ICollection<S_A__UserRes> _S_A__UserRes;
		partial void onS_A__UserResGetting();

		[JsonIgnore]
        public virtual ICollection<S_A_UserImg> S_A_UserImg { get { onS_A_UserImgGetting(); return _S_A_UserImg;} }
		private ICollection<S_A_UserImg> _S_A_UserImg;
		partial void onS_A_UserImgGetting();

		[JsonIgnore]
        public virtual ICollection<S_A_UserLinkMan> S_A_UserLinkMan { get { onS_A_UserLinkManGetting(); return _S_A_UserLinkMan;} }
		private ICollection<S_A_UserLinkMan> _S_A_UserLinkMan;
		partial void onS_A_UserLinkManGetting();


        public S_A_User()
        {
			SortIndex = 0;
			ErrorCount = 0;
			IsDeleted = "0";
            _S_A__OrgUser = new List<S_A__OrgUser>();
            _S_A__RoleUser = new List<S_A__RoleUser>();
            _S_A__UserRes = new List<S_A__UserRes>();
            _S_A_UserImg = new List<S_A_UserImg>();
            _S_A_UserLinkMan = new List<S_A_UserLinkMan>();
        }
    }

	/// <summary></summary>	
	[Description("")]
    public partial class S_A_UserImg
    {
		/// <summary>ID</summary>	
		[Description("ID")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID (Primary key)
		private string _ID="";
		/// <summary>用户ID</summary>	
		[Description("用户ID")]
        public string UserID { get{return _UserID;} set{_UserID = value??"";} } // UserID
		private string _UserID="";
		/// <summary>签名图片</summary>	
		[Description("签名图片")]
        public byte[] SignImg { get; set; } // SignImg
		/// <summary>照片</summary>	
		[Description("照片")]
        public byte[] Picture { get; set; } // Picture

        // Foreign keys
		[JsonIgnore]
        public virtual S_A_User S_A_User { get; set; } //  UserID - FK_S_A_UserImg_S_A_User
    }

	/// <summary></summary>	
	[Description("")]
    public partial class S_A_UserLinkMan
    {
		/// <summary>表格标识</summary>	
		[Description("表格标识")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID (Primary key)
		private string _ID="";
		/// <summary>用户ID标识</summary>	
		[Description("用户ID标识")]
        public string UserID { get{return _UserID;} set{_UserID = value??"";} } // UserID
		private string _UserID="";
		/// <summary>联系人ID标识</summary>	
		[Description("联系人ID标识")]
        public string LinkManID { get{return _LinkManID;} set{_LinkManID = value??"";} } // LinkManID
		private string _LinkManID="";
		/// <summary>排序</summary>	
		[Description("排序")]
        public double? SortIndex { get; set; } // SortIndex

        // Foreign keys
		[JsonIgnore]
        public virtual S_A_User S_A_User { get; set; } //  UserID - FK_S_A_UserLinkMan_S_A_User
    }

	/// <summary></summary>	
	[Description("")]
    public partial class S_C_Holiday
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID (Primary key)
		private string _ID="";
		/// <summary></summary>	
		[Description("")]
        public int? Year { get; set; } // Year
		/// <summary></summary>	
		[Description("")]
        public int? Month { get; set; } // Month
		/// <summary></summary>	
		[Description("")]
        public int? Day { get; set; } // Day
		/// <summary></summary>	
		[Description("")]
        public DateTime? Date { get; set; } // Date
		/// <summary></summary>	
		[Description("")]
        public string DayOfWeek { get{return _DayOfWeek;} set{_DayOfWeek = value??"";} } // DayOfWeek
		private string _DayOfWeek="";
		/// <summary></summary>	
		[Description("")]
        public string IsHoliday { get{return _IsHoliday;} set{_IsHoliday = value??"";} } // IsHoliday
		private string _IsHoliday="";
    }

	/// <summary></summary>	
	[Description("")]
    public partial class S_D_FormToPDFRegist
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID (Primary key)
		private string _ID="";
		/// <summary>表单名称</summary>	
		[Description("表单名称")]
        public string FormName { get{return _FormName;} set{_FormName = value??"";} } // FormName
		private string _FormName="";
		/// <summary>表名</summary>	
		[Description("表名")]
        public string TableName { get{return _TableName;} set{_TableName = value??"";} } // TableName
		private string _TableName="";
		/// <summary>Word模板码</summary>	
		[Description("Word模板码")]
        public string TempCode { get{return _TempCode;} set{_TempCode = value??"";} } // TempCode
		private string _TempCode="";
		/// <summary></summary>	
		[Description("")]
        public string ConnName { get{return _ConnName;} set{_ConnName = value??"";} } // ConnName
		private string _ConnName="";
    }

	/// <summary></summary>	
	[Description("")]
    public partial class S_D_FormToPDFTask
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID (Primary key)
		private string _ID="";
		/// <summary></summary>	
		[Description("")]
        public string TempCode { get{return _TempCode;} set{_TempCode = value??"";} } // TempCode
		private string _TempCode="";
		/// <summary></summary>	
		[Description("")]
        public string FormID { get{return _FormID;} set{_FormID = value??"";} } // FormID
		private string _FormID="";
		/// <summary></summary>	
		[Description("")]
        public string PDFFileID { get{return _PDFFileID;} set{_PDFFileID = value??"";} } // PDFFileID
		private string _PDFFileID="";
		/// <summary>表单最后更新时间</summary>	
		[Description("表单最后更新时间")]
        public DateTime? FormLastModifyDate { get; set; } // FormLastModifyDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? BeginTime { get; set; } // BeginTime
		/// <summary>完成时间</summary>	
		[Description("完成时间")]
        public DateTime? EndTime { get; set; } // EndTime
		/// <summary></summary>	
		[Description("")]
        public string DoneLog { get{return _DoneLog;} set{_DoneLog = value??"";} } // DoneLog
		private string _DoneLog="";
		/// <summary></summary>	
		[Description("")]
        public string State { get{return _State;} set{_State = value??"";} } // State
		private string _State="";
    }

	/// <summary></summary>	
	[Description("")]
    public partial class S_D_ModifyLog
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID (Primary key)
		private string _ID="";
		/// <summary></summary>	
		[Description("")]
        public string ConnName { get{return _ConnName;} set{_ConnName = value??"";} } // ConnName
		private string _ConnName="";
		/// <summary></summary>	
		[Description("")]
        public string TableName { get{return _TableName;} set{_TableName = value??"";} } // TableName
		private string _TableName="";
		/// <summary></summary>	
		[Description("")]
        public string ModifyMode { get{return _ModifyMode;} set{_ModifyMode = value??"";} } // ModifyMode
		private string _ModifyMode="";
		/// <summary></summary>	
		[Description("")]
        public string EntityKey { get{return _EntityKey;} set{_EntityKey = value??"";} } // EntityKey
		private string _EntityKey="";
		/// <summary></summary>	
		[Description("")]
        public string CurrentValue { get{return _CurrentValue;} set{_CurrentValue = value??"";} } // CurrentValue
		private string _CurrentValue="";
		/// <summary></summary>	
		[Description("")]
        public string OriginalValue { get{return _OriginalValue;} set{_OriginalValue = value??"";} } // OriginalValue
		private string _OriginalValue="";
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
        public DateTime? ModifyTime { get; set; } // ModifyTime
		/// <summary></summary>	
		[Description("")]
        public string ClientIP { get{return _ClientIP;} set{_ClientIP = value??"";} } // ClientIP
		private string _ClientIP="";
		/// <summary></summary>	
		[Description("")]
        public string UserHostAddress { get{return _UserHostAddress;} set{_UserHostAddress = value??"";} } // UserHostAddress
		private string _UserHostAddress="";
    }

	/// <summary></summary>	
	[Description("")]
    public partial class S_D_PDFTask
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID (Primary key)
		private string _ID="";
		/// <summary></summary>	
		[Description("")]
        public string FileID { get{return _FileID;} set{_FileID = value??"";} } // FileID
		private string _FileID="";
		/// <summary></summary>	
		[Description("")]
        public string FileType { get{return _FileType;} set{_FileType = value??"";} } // FileType
		private string _FileType="";
		/// <summary></summary>	
		[Description("")]
        public string PDFFileID { get{return _PDFFileID;} set{_PDFFileID = value??"";} } // PDFFileID
		private string _PDFFileID="";
		/// <summary></summary>	
		[Description("")]
        public string SWFFileID { get{return _SWFFileID;} set{_SWFFileID = value??"";} } // SWFFileID
		private string _SWFFileID="";
		/// <summary></summary>	
		[Description("")]
        public string SnapFileID { get{return _SnapFileID;} set{_SnapFileID = value??"";} } // SnapFileID
		private string _SnapFileID="";
		/// <summary></summary>	
		[Description("")]
        public int PDFPageCount { get; set; } // PDFPageCount
		/// <summary></summary>	
		[Description("")]
        public string IsSplit { get{return _IsSplit;} set{_IsSplit = value??"";} } // IsSplit
		private string _IsSplit="";
		/// <summary></summary>	
		[Description("")]
        public string Status { get{return _Status;} set{_Status = value??"";} } // Status
		private string _Status="";
		/// <summary></summary>	
		[Description("")]
        public DateTime? StartTime { get; set; } // StartTime
		/// <summary></summary>	
		[Description("")]
        public DateTime? EndTime { get; set; } // EndTime
		/// <summary></summary>	
		[Description("")]
        public string Remark { get{return _Remark;} set{_Remark = value??"";} } // Remark
		private string _Remark="";
    }

	/// <summary>百度云消息推送</summary>	
	[Description("百度云消息推送")]
    public partial class S_D_PushTask
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
        public string Title { get{return _Title;} set{_Title = value??"";} } // Title
		private string _Title="";
		/// <summary></summary>	
		[Description("")]
        public string ShortContent { get{return _ShortContent;} set{_ShortContent = value??"";} } // ShortContent
		private string _ShortContent="";
		/// <summary></summary>	
		[Description("")]
        public string SourceType { get{return _SourceType;} set{_SourceType = value??"";} } // SourceType
		private string _SourceType="";
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
        public string DeptID { get{return _DeptID;} set{_DeptID = value??"";} } // DeptID
		private string _DeptID="";
		/// <summary></summary>	
		[Description("")]
        public string DeptName { get{return _DeptName;} set{_DeptName = value??"";} } // DeptName
		private string _DeptName="";
		/// <summary></summary>	
		[Description("")]
        public string PushType { get{return _PushType;} set{_PushType = value??"";} } // PushType
		private string _PushType="";
		/// <summary></summary>	
		[Description("")]
        public DateTime? BeginTime { get; set; } // BeginTime
		/// <summary></summary>	
		[Description("")]
        public DateTime? EndTime { get; set; } // EndTime
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateTime { get; set; } // CreateTime
		/// <summary></summary>	
		[Description("")]
        public string State { get{return _State;} set{_State = value??"";} } // State
		private string _State="";
		/// <summary></summary>	
		[Description("")]
        public string DoneLog { get{return _DoneLog;} set{_DoneLog = value??"";} } // DoneLog
		private string _DoneLog="";
		/// <summary>移动设备ID（百度云推送生成）</summary>	
		[Description("移动设备ID（百度云推送生成）")]
        public string ClientID { get{return _ClientID;} set{_ClientID = value??"";} } // ClientID
		private string _ClientID="";
		/// <summary>移动AppID（百度生成）</summary>	
		[Description("移动AppID（百度生成）")]
        public string AppID { get{return _AppID;} set{_AppID = value??"";} } // AppID
		private string _AppID="";
		/// <summary>频道（百度云推送生成）</summary>	
		[Description("频道（百度云推送生成）")]
        public string ChannelID { get{return _ChannelID;} set{_ChannelID = value??"";} } // ChannelID
		private string _ChannelID="";
		/// <summary></summary>	
		[Description("")]
        public string DeviceOS { get{return _DeviceOS;} set{_DeviceOS = value??"";} } // DeviceOS
		private string _DeviceOS="";

        public S_D_PushTask()
        {
			PushType = "Boardcast=0,Personal=1,Group=2";
        }
    }

	/// <summary></summary>	
	[Description("")]
    public partial class S_H_AllFeedback
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID (Primary key)
		private string _ID="";
		/// <summary></summary>	
		[Description("")]
        public string Title { get{return _Title;} set{_Title = value??"";} } // Title
		private string _Title="";
		/// <summary></summary>	
		[Description("")]
        public string Content { get{return _Content;} set{_Content = value??"";} } // Content
		private string _Content="";
		/// <summary></summary>	
		[Description("")]
        public string Url { get{return _Url;} set{_Url = value??"";} } // Url
		private string _Url="";
		/// <summary></summary>	
		[Description("")]
        public string Attachment { get{return _Attachment;} set{_Attachment = value??"";} } // Attachment
		private string _Attachment="";
		/// <summary></summary>	
		[Description("")]
        public string IsUse { get{return _IsUse;} set{_IsUse = value??"";} } // IsUse
		private string _IsUse="";
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateTime { get; set; } // CreateTime
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
        public string DealStatus { get{return _DealStatus;} set{_DealStatus = value??"";} } // DealStatus
		private string _DealStatus="";
		/// <summary></summary>	
		[Description("")]
        public string DealResult { get{return _DealResult;} set{_DealResult = value??"";} } // DealResult
		private string _DealResult="";
		/// <summary></summary>	
		[Description("")]
        public string ProjectName { get{return _ProjectName;} set{_ProjectName = value??"";} } // ProjectName
		private string _ProjectName="";
		/// <summary></summary>	
		[Description("")]
        public string DealUserName { get{return _DealUserName;} set{_DealUserName = value??"";} } // DealUserName
		private string _DealUserName="";
		/// <summary></summary>	
		[Description("")]
        public string Type { get{return _Type;} set{_Type = value??"";} } // Type
		private string _Type="";
		/// <summary></summary>	
		[Description("")]
        public string Level { get{return _Level;} set{_Level = value??"";} } // Level
		private string _Level="";
		/// <summary>所属模块</summary>	
		[Description("所属模块")]
        public string Module { get{return _Module;} set{_Module = value??"";} } // Module
		private string _Module="";
		/// <summary>计划完成时间</summary>	
		[Description("计划完成时间")]
        public DateTime? ExpectedCompleteTime { get; set; } // ExpectedCompleteTime
		/// <summary>问题性质</summary>	
		[Description("问题性质")]
        public string ProblemNature { get{return _ProblemNature;} set{_ProblemNature = value??"";} } // ProblemNature
		private string _ProblemNature="";
		/// <summary></summary>	
		[Description("")]
        public string ProjectPrincipal { get{return _ProjectPrincipal;} set{_ProjectPrincipal = value??"";} } // ProjectPrincipal
		private string _ProjectPrincipal="";
		/// <summary></summary>	
		[Description("")]
        public string ProjectDept { get{return _ProjectDept;} set{_ProjectDept = value??"";} } // ProjectDept
		private string _ProjectDept="";
		/// <summary></summary>	
		[Description("")]
        public DateTime? ConfirmProblemsTime { get; set; } // ConfirmProblemsTime
		/// <summary></summary>	
		[Description("")]
        public DateTime? PlanCompleteTime { get; set; } // PlanCompleteTime
		/// <summary></summary>	
		[Description("")]
        public DateTime? ActualCompleteTime { get; set; } // ActualCompleteTime
		/// <summary></summary>	
		[Description("")]
        public DateTime? ConfirmCompleteTime { get; set; } // ConfirmCompleteTime
		/// <summary></summary>	
		[Description("")]
        public string ConfirmProblemsUserID { get{return _ConfirmProblemsUserID;} set{_ConfirmProblemsUserID = value??"";} } // ConfirmProblemsUserID
		private string _ConfirmProblemsUserID="";
		/// <summary></summary>	
		[Description("")]
        public string ConfirmProblemsUserName { get{return _ConfirmProblemsUserName;} set{_ConfirmProblemsUserName = value??"";} } // ConfirmProblemsUserName
		private string _ConfirmProblemsUserName="";
		/// <summary></summary>	
		[Description("")]
        public string ConfirmCompleteUserID { get{return _ConfirmCompleteUserID;} set{_ConfirmCompleteUserID = value??"";} } // ConfirmCompleteUserID
		private string _ConfirmCompleteUserID="";
		/// <summary></summary>	
		[Description("")]
        public string ConfirmCompleteUserName { get{return _ConfirmCompleteUserName;} set{_ConfirmCompleteUserName = value??"";} } // ConfirmCompleteUserName
		private string _ConfirmCompleteUserName="";
    }

	/// <summary></summary>	
	[Description("")]
    public partial class S_H_Calendar
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID (Primary key)
		private string _ID="";
		/// <summary></summary>	
		[Description("")]
        public string Title { get{return _Title;} set{_Title = value??"";} } // Title
		private string _Title="";
		/// <summary></summary>	
		[Description("")]
        public string Description { get{return _Description;} set{_Description = value??"";} } // Description
		private string _Description="";
		/// <summary></summary>	
		[Description("")]
        public DateTime? StartTime { get; set; } // StartTime
		/// <summary></summary>	
		[Description("")]
        public DateTime? EndTime { get; set; } // EndTime
		/// <summary></summary>	
		[Description("")]
        public string Url { get{return _Url;} set{_Url = value??"";} } // Url
		private string _Url="";
		/// <summary></summary>	
		[Description("")]
        public string Grade { get{return _Grade;} set{_Grade = value??"";} } // Grade
		private string _Grade="";
		/// <summary></summary>	
		[Description("")]
        public string Attachments { get{return _Attachments;} set{_Attachments = value??"";} } // Attachments
		private string _Attachments="";
		/// <summary></summary>	
		[Description("")]
        public string Remark { get{return _Remark;} set{_Remark = value??"";} } // Remark
		private string _Remark="";
		/// <summary></summary>	
		[Description("")]
        public string Sponsor { get{return _Sponsor;} set{_Sponsor = value??"";} } // Sponsor
		private string _Sponsor="";
		/// <summary></summary>	
		[Description("")]
        public string SponsorID { get{return _SponsorID;} set{_SponsorID = value??"";} } // SponsorID
		private string _SponsorID="";
		/// <summary></summary>	
		[Description("")]
        public string Participators { get{return _Participators;} set{_Participators = value??"";} } // Participators
		private string _Participators="";
		/// <summary></summary>	
		[Description("")]
        public string ParticipatorsID { get{return _ParticipatorsID;} set{_ParticipatorsID = value??"";} } // ParticipatorsID
		private string _ParticipatorsID="";
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateTime { get; set; } // CreateTime
		/// <summary></summary>	
		[Description("")]
        public string CreateUserName { get{return _CreateUserName;} set{_CreateUserName = value??"";} } // CreateUserName
		private string _CreateUserName="";
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get{return _CreateUserID;} set{_CreateUserID = value??"";} } // CreateUserID
		private string _CreateUserID="";
    }

	/// <summary></summary>	
	[Description("")]
    public partial class S_H_Feedback
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID (Primary key)
		private string _ID="";
		/// <summary></summary>	
		[Description("")]
        public string Title { get{return _Title;} set{_Title = value??"";} } // Title
		private string _Title="";
		/// <summary></summary>	
		[Description("")]
        public string Content { get{return _Content;} set{_Content = value??"";} } // Content
		private string _Content="";
		/// <summary></summary>	
		[Description("")]
        public string Url { get{return _Url;} set{_Url = value??"";} } // Url
		private string _Url="";
		/// <summary></summary>	
		[Description("")]
        public string Attachment { get{return _Attachment;} set{_Attachment = value??"";} } // Attachment
		private string _Attachment="";
		/// <summary></summary>	
		[Description("")]
        public string IsUse { get{return _IsUse;} set{_IsUse = value??"";} } // IsUse
		private string _IsUse="";
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateTime { get; set; } // CreateTime
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
        public string DealStatus { get{return _DealStatus;} set{_DealStatus = value??"";} } // DealStatus
		private string _DealStatus="";
		/// <summary></summary>	
		[Description("")]
        public string DealResult { get{return _DealResult;} set{_DealResult = value??"";} } // DealResult
		private string _DealResult="";
		/// <summary></summary>	
		[Description("")]
        public string ProjectName { get{return _ProjectName;} set{_ProjectName = value??"";} } // ProjectName
		private string _ProjectName="";
		/// <summary></summary>	
		[Description("")]
        public string DealUserName { get{return _DealUserName;} set{_DealUserName = value??"";} } // DealUserName
		private string _DealUserName="";
		/// <summary></summary>	
		[Description("")]
        public string Type { get{return _Type;} set{_Type = value??"";} } // Type
		private string _Type="";
		/// <summary></summary>	
		[Description("")]
        public string Level { get{return _Level;} set{_Level = value??"";} } // Level
		private string _Level="";
		/// <summary>所属模块</summary>	
		[Description("所属模块")]
        public string Module { get{return _Module;} set{_Module = value??"";} } // Module
		private string _Module="";
		/// <summary>计划完成时间</summary>	
		[Description("计划完成时间")]
        public DateTime? ExpectedCompleteTime { get; set; } // ExpectedCompleteTime
		/// <summary>问题性质</summary>	
		[Description("问题性质")]
        public string ProblemNature { get{return _ProblemNature;} set{_ProblemNature = value??"";} } // ProblemNature
		private string _ProblemNature="";
		/// <summary></summary>	
		[Description("")]
        public string ProjectPrincipal { get{return _ProjectPrincipal;} set{_ProjectPrincipal = value??"";} } // ProjectPrincipal
		private string _ProjectPrincipal="";
		/// <summary></summary>	
		[Description("")]
        public string ProjectDept { get{return _ProjectDept;} set{_ProjectDept = value??"";} } // ProjectDept
		private string _ProjectDept="";
		/// <summary></summary>	
		[Description("")]
        public DateTime? ConfirmProblemsTime { get; set; } // ConfirmProblemsTime
		/// <summary></summary>	
		[Description("")]
        public DateTime? PlanCompleteTime { get; set; } // PlanCompleteTime
		/// <summary></summary>	
		[Description("")]
        public DateTime? ActualCompleteTime { get; set; } // ActualCompleteTime
		/// <summary></summary>	
		[Description("")]
        public DateTime? ConfirmCompleteTime { get; set; } // ConfirmCompleteTime
		/// <summary></summary>	
		[Description("")]
        public string ConfirmProblemsUserID { get{return _ConfirmProblemsUserID;} set{_ConfirmProblemsUserID = value??"";} } // ConfirmProblemsUserID
		private string _ConfirmProblemsUserID="";
		/// <summary></summary>	
		[Description("")]
        public string ConfirmProblemsUserName { get{return _ConfirmProblemsUserName;} set{_ConfirmProblemsUserName = value??"";} } // ConfirmProblemsUserName
		private string _ConfirmProblemsUserName="";
		/// <summary></summary>	
		[Description("")]
        public string ConfirmCompleteUserID { get{return _ConfirmCompleteUserID;} set{_ConfirmCompleteUserID = value??"";} } // ConfirmCompleteUserID
		private string _ConfirmCompleteUserID="";
		/// <summary></summary>	
		[Description("")]
        public string ConfirmCompleteUserName { get{return _ConfirmCompleteUserName;} set{_ConfirmCompleteUserName = value??"";} } // ConfirmCompleteUserName
		private string _ConfirmCompleteUserName="";
    }

	/// <summary></summary>	
	[Description("")]
    public partial class S_H_ShortCut
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID (Primary key)
		private string _ID="";
		/// <summary></summary>	
		[Description("")]
        public string Type { get{return _Type;} set{_Type = value??"";} } // Type
		private string _Type="";
		/// <summary></summary>	
		[Description("")]
        public string Name { get{return _Name;} set{_Name = value??"";} } // Name
		private string _Name="";
		/// <summary></summary>	
		[Description("")]
        public string Url { get{return _Url;} set{_Url = value??"";} } // Url
		private string _Url="";
		/// <summary>图标的路径</summary>	
		[Description("图标的路径")]
        public string IconImage { get{return _IconImage;} set{_IconImage = value??"";} } // IconImage
		private string _IconImage="";
		/// <summary></summary>	
		[Description("")]
        public int? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string IsUse { get{return _IsUse;} set{_IsUse = value??"";} } // IsUse
		private string _IsUse="";
		/// <summary></summary>	
		[Description("")]
        public int? PageIndex { get; set; } // PageIndex
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateTime { get; set; } // CreateTime
		/// <summary></summary>	
		[Description("")]
        public string CreateUserName { get{return _CreateUserName;} set{_CreateUserName = value??"";} } // CreateUserName
		private string _CreateUserName="";
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get{return _CreateUserID;} set{_CreateUserID = value??"";} } // CreateUserID
		private string _CreateUserID="";
    }

	/// <summary></summary>	
	[Description("")]
    public partial class S_I_FriendLink
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID (Primary key)
		private string _ID="";
		/// <summary></summary>	
		[Description("")]
        public string Icon { get{return _Icon;} set{_Icon = value??"";} } // Icon
		private string _Icon="";
		/// <summary></summary>	
		[Description("")]
        public string Name { get{return _Name;} set{_Name = value??"";} } // Name
		private string _Name="";
		/// <summary></summary>	
		[Description("")]
        public string Url { get{return _Url;} set{_Url = value??"";} } // Url
		private string _Url="";
		/// <summary></summary>	
		[Description("")]
        public string DeptId { get{return _DeptId;} set{_DeptId = value??"";} } // DeptId
		private string _DeptId="";
		/// <summary></summary>	
		[Description("")]
        public string DeptName { get{return _DeptName;} set{_DeptName = value??"";} } // DeptName
		private string _DeptName="";
		/// <summary></summary>	
		[Description("")]
        public string UserId { get{return _UserId;} set{_UserId = value??"";} } // UserId
		private string _UserId="";
		/// <summary></summary>	
		[Description("")]
        public string UserName { get{return _UserName;} set{_UserName = value??"";} } // UserName
		private string _UserName="";
		/// <summary></summary>	
		[Description("")]
        public string Remark { get{return _Remark;} set{_Remark = value??"";} } // Remark
		private string _Remark="";
		/// <summary></summary>	
		[Description("")]
        public int? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateTime { get; set; } // CreateTime
		/// <summary></summary>	
		[Description("")]
        public string CreateUserName { get{return _CreateUserName;} set{_CreateUserName = value??"";} } // CreateUserName
		private string _CreateUserName="";
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get{return _CreateUserID;} set{_CreateUserID = value??"";} } // CreateUserID
		private string _CreateUserID="";
    }

	/// <summary></summary>	
	[Description("")]
    public partial class S_I_NewsImage
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID (Primary key)
		private string _ID="";
		/// <summary></summary>	
		[Description("")]
        public string GroupID { get{return _GroupID;} set{_GroupID = value??"";} } // GroupID
		private string _GroupID="";
		/// <summary></summary>	
		[Description("")]
        public string PictureName { get{return _PictureName;} set{_PictureName = value??"";} } // PictureName
		private string _PictureName="";
		/// <summary></summary>	
		[Description("")]
        public byte[] PictureEntire { get; set; } // PictureEntire
		/// <summary></summary>	
		[Description("")]
        public byte[] PictureThumb { get; set; } // PictureThumb
		/// <summary>图片</summary>	
		[Description("图片")]
        public string Src { get{return _Src;} set{_Src = value??"";} } // Src
		private string _Src="";
		/// <summary>链接</summary>	
		[Description("链接")]
        public string LinkUrl { get{return _LinkUrl;} set{_LinkUrl = value??"";} } // LinkUrl
		private string _LinkUrl="";
		/// <summary>描述</summary>	
		[Description("描述")]
        public string Description { get{return _Description;} set{_Description = value??"";} } // Description
		private string _Description="";
		/// <summary>排序</summary>	
		[Description("排序")]
        public int? SortIndex { get; set; } // SortIndex
		/// <summary>创建日期</summary>	
		[Description("创建日期")]
        public DateTime? CreateTime { get; set; } // CreateTime
		/// <summary>创建人</summary>	
		[Description("创建人")]
        public string CreateUserName { get{return _CreateUserName;} set{_CreateUserName = value??"";} } // CreateUserName
		private string _CreateUserName="";
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get{return _CreateUserID;} set{_CreateUserID = value??"";} } // CreateUserID
		private string _CreateUserID="";
    }

	/// <summary></summary>	
	[Description("")]
    public partial class S_I_NewsImageGroup
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID (Primary key)
		private string _ID="";
		/// <summary></summary>	
		[Description("")]
        public string Title { get{return _Title;} set{_Title = value??"";} } // Title
		private string _Title="";
		/// <summary></summary>	
		[Description("")]
        public string Remark { get{return _Remark;} set{_Remark = value??"";} } // Remark
		private string _Remark="";
		/// <summary></summary>	
		[Description("")]
        public string DeptDoorId { get{return _DeptDoorId;} set{_DeptDoorId = value??"";} } // DeptDoorId
		private string _DeptDoorId="";
		/// <summary></summary>	
		[Description("")]
        public string DeptDoorName { get{return _DeptDoorName;} set{_DeptDoorName = value??"";} } // DeptDoorName
		private string _DeptDoorName="";
		/// <summary></summary>	
		[Description("")]
        public int? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateTime { get; set; } // CreateTime
		/// <summary></summary>	
		[Description("")]
        public string CreateUserName { get{return _CreateUserName;} set{_CreateUserName = value??"";} } // CreateUserName
		private string _CreateUserName="";
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get{return _CreateUserID;} set{_CreateUserID = value??"";} } // CreateUserID
		private string _CreateUserID="";
    }

	/// <summary></summary>	
	[Description("")]
    public partial class S_I_PublicInformation
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID (Primary key)
		private string _ID="";
		/// <summary></summary>	
		[Description("")]
        public string CatalogId { get{return _CatalogId;} set{_CatalogId = value??"";} } // CatalogId
		private string _CatalogId="";
		/// <summary></summary>	
		[Description("")]
        public string Title { get{return _Title;} set{_Title = value??"";} } // Title
		private string _Title="";
		/// <summary></summary>	
		[Description("")]
        public string Content { get{return _Content;} set{_Content = value??"";} } // Content
		private string _Content="";
		/// <summary></summary>	
		[Description("")]
        public string ContentText { get{return _ContentText;} set{_ContentText = value??"";} } // ContentText
		private string _ContentText="";
		/// <summary></summary>	
		[Description("")]
        public string Attachments { get{return _Attachments;} set{_Attachments = value??"";} } // Attachments
		private string _Attachments="";
		/// <summary></summary>	
		[Description("")]
        public string ReceiveDeptId { get{return _ReceiveDeptId;} set{_ReceiveDeptId = value??"";} } // ReceiveDeptId
		private string _ReceiveDeptId="";
		/// <summary></summary>	
		[Description("")]
        public string ReceiveDeptName { get{return _ReceiveDeptName;} set{_ReceiveDeptName = value??"";} } // ReceiveDeptName
		private string _ReceiveDeptName="";
		/// <summary></summary>	
		[Description("")]
        public string ReceiveUserId { get{return _ReceiveUserId;} set{_ReceiveUserId = value??"";} } // ReceiveUserId
		private string _ReceiveUserId="";
		/// <summary></summary>	
		[Description("")]
        public string ReceiveUserName { get{return _ReceiveUserName;} set{_ReceiveUserName = value??"";} } // ReceiveUserName
		private string _ReceiveUserName="";
		/// <summary></summary>	
		[Description("")]
        public string DeptDoorId { get{return _DeptDoorId;} set{_DeptDoorId = value??"";} } // DeptDoorId
		private string _DeptDoorId="";
		/// <summary></summary>	
		[Description("")]
        public string DeptDoorName { get{return _DeptDoorName;} set{_DeptDoorName = value??"";} } // DeptDoorName
		private string _DeptDoorName="";
		/// <summary></summary>	
		[Description("")]
        public DateTime? ExpiresTime { get; set; } // ExpiresTime
		/// <summary></summary>	
		[Description("")]
        public int? ReadCount { get; set; } // ReadCount
		/// <summary>重要度 1重要，0一般</summary>	
		[Description("重要度 1重要，0一般")]
        public string Important { get{return _Important;} set{_Important = value??"";} } // Important
		private string _Important="";
		/// <summary>紧急度 1重要，0一般</summary>	
		[Description("紧急度 1重要，0一般")]
        public string Urgency { get{return _Urgency;} set{_Urgency = value??"";} } // Urgency
		private string _Urgency="";
		/// <summary>置顶</summary>	
		[Description("置顶")]
        public string IsTop { get{return _IsTop;} set{_IsTop = value??"";} } // IsTop
		private string _IsTop="";
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateTime { get; set; } // CreateTime
		/// <summary></summary>	
		[Description("")]
        public string CreateUserName { get{return _CreateUserName;} set{_CreateUserName = value??"";} } // CreateUserName
		private string _CreateUserName="";
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get{return _CreateUserID;} set{_CreateUserID = value??"";} } // CreateUserID
		private string _CreateUserID="";
    }

	/// <summary></summary>	
	[Description("")]
    public partial class S_I_PublicInformCatalog
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID (Primary key)
		private string _ID="";
		/// <summary></summary>	
		[Description("")]
        public string CatalogName { get{return _CatalogName;} set{_CatalogName = value??"";} } // CatalogName
		private string _CatalogName="";
		/// <summary></summary>	
		[Description("")]
        public string CatalogKey { get{return _CatalogKey;} set{_CatalogKey = value??"";} } // CatalogKey
		private string _CatalogKey="";
		/// <summary></summary>	
		[Description("")]
        public string IsOnHomePage { get{return _IsOnHomePage;} set{_IsOnHomePage = value??"";} } // IsOnHomePage
		private string _IsOnHomePage="";
		/// <summary></summary>	
		[Description("")]
        public int? InHomePageNum { get; set; } // InHomePageNum
		/// <summary></summary>	
		[Description("")]
        public int? SortIndex { get; set; } // SortIndex
		/// <summary>创建日期</summary>	
		[Description("创建日期")]
        public DateTime? CreateTime { get; set; } // CreateTime
		/// <summary>创建人</summary>	
		[Description("创建人")]
        public string CreateUserName { get{return _CreateUserName;} set{_CreateUserName = value??"";} } // CreateUserName
		private string _CreateUserName="";
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get{return _CreateUserID;} set{_CreateUserID = value??"";} } // CreateUserID
		private string _CreateUserID="";
    }

	/// <summary>元数据分类表</summary>	
	[Description("元数据分类表")]
    public partial class S_M_Category
    {
		/// <summary>ID</summary>	
		[Description("ID")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID (Primary key)
		private string _ID="";
		/// <summary>父ID</summary>	
		[Description("父ID")]
        public string ParentID { get{return _ParentID;} set{_ParentID = value??"";} } // ParentID
		private string _ParentID="";
		/// <summary>全ID</summary>	
		[Description("全ID")]
        public string FullID { get{return _FullID;} set{_FullID = value??"";} } // FullID
		private string _FullID="";
		/// <summary>编号</summary>	
		[Description("编号")]
        public string Code { get{return _Code;} set{_Code = value??"";} } // Code
		private string _Code="";
		/// <summary>名称</summary>	
		[Description("名称")]
        public string Name { get{return _Name;} set{_Name = value??"";} } // Name
		private string _Name="";
		/// <summary>排序索引</summary>	
		[Description("排序索引")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary>描述</summary>	
		[Description("描述")]
        public string Description { get{return _Description;} set{_Description = value??"";} } // Description
		private string _Description="";
		/// <summary></summary>	
		[Description("")]
        public string CategoryCode { get{return _CategoryCode;} set{_CategoryCode = value??"";} } // CategoryCode
		private string _CategoryCode="";
		/// <summary></summary>	
		[Description("")]
        public string IconClass { get{return _IconClass;} set{_IconClass = value??"";} } // IconClass
		private string _IconClass="";
    }

	/// <summary></summary>	
	[Description("")]
    public partial class S_M_ConfigManage
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID (Primary key)
		private string _ID="";
		/// <summary></summary>	
		[Description("")]
        public string Code { get{return _Code;} set{_Code = value??"";} } // Code
		private string _Code="";
		/// <summary></summary>	
		[Description("")]
        public string Name { get{return _Name;} set{_Name = value??"";} } // Name
		private string _Name="";
		/// <summary></summary>	
		[Description("")]
        public string Type { get{return _Type;} set{_Type = value??"";} } // Type
		private string _Type="";
		/// <summary></summary>	
		[Description("")]
        public string DbServerAddr { get{return _DbServerAddr;} set{_DbServerAddr = value??"";} } // DbServerAddr
		private string _DbServerAddr="";
		/// <summary></summary>	
		[Description("")]
        public string DbName { get{return _DbName;} set{_DbName = value??"";} } // DbName
		private string _DbName="";
		/// <summary></summary>	
		[Description("")]
        public string DbLoginName { get{return _DbLoginName;} set{_DbLoginName = value??"";} } // DbLoginName
		private string _DbLoginName="";
		/// <summary></summary>	
		[Description("")]
        public string DbPassword { get{return _DbPassword;} set{_DbPassword = value??"";} } // DbPassword
		private string _DbPassword="";
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateTime { get; set; } // CreateTime
		/// <summary></summary>	
		[Description("")]
        public DateTime? SyncTime { get; set; } // SyncTime
		/// <summary></summary>	
		[Description("")]
        public string CategoryID { get{return _CategoryID;} set{_CategoryID = value??"";} } // CategoryID
		private string _CategoryID="";
		/// <summary></summary>	
		[Description("")]
        public string ConnName { get{return _ConnName;} set{_ConnName = value??"";} } // ConnName
		private string _ConnName="";
    }

	/// <summary>枚举定义表</summary>	
	[Description("枚举定义表")]
    public partial class S_M_EnumDef
    {
		/// <summary>ID</summary>	
		[Description("ID")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID (Primary key)
		private string _ID="";
		/// <summary>编号</summary>	
		[Description("编号")]
        public string Code { get{return _Code;} set{_Code = value??"";} } // Code
		private string _Code="";
		/// <summary>名称</summary>	
		[Description("名称")]
        public string Name { get{return _Name;} set{_Name = value??"";} } // Name
		private string _Name="";
		/// <summary>类型</summary>	
		[Description("类型")]
        public string Type { get{return _Type;} set{_Type = value??"";} } // Type
		private string _Type="";
		/// <summary>排序索引</summary>	
		[Description("排序索引")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary>描述</summary>	
		[Description("描述")]
        public string Description { get{return _Description;} set{_Description = value??"";} } // Description
		private string _Description="";
		/// <summary>数据库连接名</summary>	
		[Description("数据库连接名")]
        public string ConnName { get{return _ConnName;} set{_ConnName = value??"";} } // ConnName
		private string _ConnName="";
		/// <summary>查询Sql</summary>	
		[Description("查询Sql")]
        public string Sql { get{return _Sql;} set{_Sql = value??"";} } // Sql
		private string _Sql="";
		/// <summary>排序</summary>	
		[Description("排序")]
        public string Orderby { get{return _Orderby;} set{_Orderby = value??"";} } // Orderby
		private string _Orderby="";
		/// <summary>分类ID</summary>	
		[Description("分类ID")]
        public string CategoryID { get{return _CategoryID;} set{_CategoryID = value??"";} } // CategoryID
		private string _CategoryID="";

        // Reverse navigation
		[JsonIgnore]
        public virtual ICollection<S_M_EnumItem> S_M_EnumItem { get { onS_M_EnumItemGetting(); return _S_M_EnumItem;} }
		private ICollection<S_M_EnumItem> _S_M_EnumItem;
		partial void onS_M_EnumItemGetting();


        public S_M_EnumDef()
        {
			Type = "Normal";
			SortIndex = 0;
            _S_M_EnumItem = new List<S_M_EnumItem>();
        }
    }

	/// <summary>枚举表</summary>	
	[Description("枚举表")]
    public partial class S_M_EnumItem
    {
		/// <summary>ID</summary>	
		[Description("ID")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID (Primary key)
		private string _ID="";
		/// <summary>枚举定义ID</summary>	
		[Description("枚举定义ID")]
        public string EnumDefID { get{return _EnumDefID;} set{_EnumDefID = value??"";} } // EnumDefID
		private string _EnumDefID="";
		/// <summary>编号</summary>	
		[Description("编号")]
        public string Code { get{return _Code;} set{_Code = value??"";} } // Code
		private string _Code="";
		/// <summary>名称</summary>	
		[Description("名称")]
        public string Name { get{return _Name;} set{_Name = value??"";} } // Name
		private string _Name="";
		/// <summary>排序索引</summary>	
		[Description("排序索引")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary>描述</summary>	
		[Description("描述")]
        public string Description { get{return _Description;} set{_Description = value??"";} } // Description
		private string _Description="";
		/// <summary>子枚举编号</summary>	
		[Description("子枚举编号")]
        public string SubEnumDefCode { get{return _SubEnumDefCode;} set{_SubEnumDefCode = value??"";} } // SubEnumDefCode
		private string _SubEnumDefCode="";
		/// <summary>枚举分类</summary>	
		[Description("枚举分类")]
        public string Category { get{return _Category;} set{_Category = value??"";} } // Category
		private string _Category="";
		/// <summary>枚举子分类</summary>	
		[Description("枚举子分类")]
        public string SubCategory { get{return _SubCategory;} set{_SubCategory = value??"";} } // SubCategory
		private string _SubCategory="";

        // Foreign keys
		[JsonIgnore]
        public virtual S_M_EnumDef S_M_EnumDef { get; set; } //  EnumDefID - FK_EnumItem_EnumDef

        public S_M_EnumItem()
        {
			SortIndex = 0.0;
        }
    }

	/// <summary>平台数据库字段表</summary>	
	[Description("平台数据库字段表")]
    public partial class S_M_Field
    {
		/// <summary>ID</summary>	
		[Description("ID")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID (Primary key)
		private string _ID="";
		/// <summary>表ID</summary>	
		[Description("表ID")]
        public string TableID { get{return _TableID;} set{_TableID = value??"";} } // TableID
		private string _TableID="";
		/// <summary>编号</summary>	
		[Description("编号")]
        public string Code { get{return _Code;} set{_Code = value??"";} } // Code
		private string _Code="";
		/// <summary>名称</summary>	
		[Description("名称")]
        public string Name { get{return _Name;} set{_Name = value??"";} } // Name
		private string _Name="";
		/// <summary></summary>	
		[Description("")]
        public string Type { get{return _Type;} set{_Type = value??"";} } // Type
		private string _Type="";
		/// <summary></summary>	
		[Description("")]
        public double SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string EnumKey { get{return _EnumKey;} set{_EnumKey = value??"";} } // EnumKey
		private string _EnumKey="";
		/// <summary>描述</summary>	
		[Description("描述")]
        public string Description { get{return _Description;} set{_Description = value??"";} } // Description
		private string _Description="";

        // Foreign keys
		[JsonIgnore]
        public virtual S_M_Table S_M_Table { get; set; } //  TableID - FK_S_M_Field_S_M_Table

        public S_M_Field()
        {
			SortIndex = 0;
        }
    }

	/// <summary>平台数据库表</summary>	
	[Description("平台数据库表")]
    public partial class S_M_Table
    {
		/// <summary>ID</summary>	
		[Description("ID")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID (Primary key)
		private string _ID="";
		/// <summary>编号</summary>	
		[Description("编号")]
        public string Code { get{return _Code;} set{_Code = value??"";} } // Code
		private string _Code="";
		/// <summary>名称</summary>	
		[Description("名称")]
        public string Name { get{return _Name;} set{_Name = value??"";} } // Name
		private string _Name="";
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary>数据库连接名</summary>	
		[Description("数据库连接名")]
        public string ConnName { get{return _ConnName;} set{_ConnName = value??"";} } // ConnName
		private string _ConnName="";
		/// <summary>描述</summary>	
		[Description("描述")]
        public string Description { get{return _Description;} set{_Description = value??"";} } // Description
		private string _Description="";

        // Reverse navigation
		[JsonIgnore]
        public virtual ICollection<S_M_Field> S_M_Field { get { onS_M_FieldGetting(); return _S_M_Field;} }
		private ICollection<S_M_Field> _S_M_Field;
		partial void onS_M_FieldGetting();


        public S_M_Table()
        {
            _S_M_Field = new List<S_M_Field>();
        }
    }

	/// <summary></summary>	
	[Description("")]
    public partial class S_P_DoorBaseTemplate
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID (Primary key)
		private string _ID="";
		/// <summary></summary>	
		[Description("")]
        public string BaseType { get{return _BaseType;} set{_BaseType = value??"";} } // BaseType
		private string _BaseType="";
		/// <summary></summary>	
		[Description("")]
        public string IsDefault { get{return _IsDefault;} set{_IsDefault = value??"";} } // IsDefault
		private string _IsDefault="";
		/// <summary></summary>	
		[Description("")]
        public string TemplateColWidth { get{return _TemplateColWidth;} set{_TemplateColWidth = value??"";} } // TemplateColWidth
		private string _TemplateColWidth="";
		/// <summary></summary>	
		[Description("")]
        public string TemplateString { get{return _TemplateString;} set{_TemplateString = value??"";} } // TemplateString
		private string _TemplateString="";
		/// <summary></summary>	
		[Description("")]
        public string Title { get{return _Title;} set{_Title = value??"";} } // Title
		private string _Title="";
		/// <summary></summary>	
		[Description("")]
        public string IsEdit { get{return _IsEdit;} set{_IsEdit = value??"";} } // IsEdit
		private string _IsEdit="";
    }

	/// <summary></summary>	
	[Description("")]
    public partial class S_P_DoorBlock
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID (Primary key)
		private string _ID="";
		/// <summary></summary>	
		[Description("")]
        public string BlockName { get{return _BlockName;} set{_BlockName = value??"";} } // BlockName
		private string _BlockName="";
		/// <summary></summary>	
		[Description("")]
        public string BlockKey { get{return _BlockKey;} set{_BlockKey = value??"";} } // BlockKey
		private string _BlockKey="";
		/// <summary></summary>	
		[Description("")]
        public string BlockTitle { get{return _BlockTitle;} set{_BlockTitle = value??"";} } // BlockTitle
		private string _BlockTitle="";
		/// <summary></summary>	
		[Description("")]
        public string BlockType { get{return _BlockType;} set{_BlockType = value??"";} } // BlockType
		private string _BlockType="";
		/// <summary></summary>	
		[Description("")]
        public string BlockImage { get{return _BlockImage;} set{_BlockImage = value??"";} } // BlockImage
		private string _BlockImage="";
		/// <summary></summary>	
		[Description("")]
        public string Remark { get{return _Remark;} set{_Remark = value??"";} } // Remark
		private string _Remark="";
		/// <summary></summary>	
		[Description("")]
        public string HeadHtml { get{return _HeadHtml;} set{_HeadHtml = value??"";} } // HeadHtml
		private string _HeadHtml="";
		/// <summary></summary>	
		[Description("")]
        public string ColorValue { get{return _ColorValue;} set{_ColorValue = value??"";} } // ColorValue
		private string _ColorValue="";
		/// <summary></summary>	
		[Description("")]
        public string Color { get{return _Color;} set{_Color = value??"";} } // Color
		private string _Color="";
		/// <summary></summary>	
		[Description("")]
        public int? RepeatItemCount { get; set; } // RepeatItemCount
		/// <summary></summary>	
		[Description("")]
        public int? RepeatItemLength { get; set; } // RepeatItemLength
		/// <summary></summary>	
		[Description("")]
        public string RepeatDataDataSql { get{return _RepeatDataDataSql;} set{_RepeatDataDataSql = value??"";} } // RepeatDataDataSql
		private string _RepeatDataDataSql="";
		/// <summary></summary>	
		[Description("")]
        public string RepeatItemTemplate { get{return _RepeatItemTemplate;} set{_RepeatItemTemplate = value??"";} } // RepeatItemTemplate
		private string _RepeatItemTemplate="";
		/// <summary></summary>	
		[Description("")]
        public string FootHtml { get{return _FootHtml;} set{_FootHtml = value??"";} } // FootHtml
		private string _FootHtml="";
		/// <summary></summary>	
		[Description("")]
        public int? DelayLoadSecond { get; set; } // DelayLoadSecond
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string RelateScript { get{return _RelateScript;} set{_RelateScript = value??"";} } // RelateScript
		private string _RelateScript="";
		/// <summary></summary>	
		[Description("")]
        public string IsHidden { get{return _IsHidden;} set{_IsHidden = value??"";} } // IsHidden
		private string _IsHidden="";
		/// <summary></summary>	
		[Description("")]
        public string TemplateId { get{return _TemplateId;} set{_TemplateId = value??"";} } // TemplateId
		private string _TemplateId="";
		/// <summary></summary>	
		[Description("")]
        public string AllowUserIDs { get{return _AllowUserIDs;} set{_AllowUserIDs = value??"";} } // AllowUserIDs
		private string _AllowUserIDs="";
		/// <summary></summary>	
		[Description("")]
        public string AllowUserNames { get{return _AllowUserNames;} set{_AllowUserNames = value??"";} } // AllowUserNames
		private string _AllowUserNames="";
		/// <summary></summary>	
		[Description("")]
        public string AllowTypes { get{return _AllowTypes;} set{_AllowTypes = value??"";} } // AllowTypes
		private string _AllowTypes="";
		/// <summary></summary>	
		[Description("")]
        public string IsEdit { get{return _IsEdit;} set{_IsEdit = value??"";} } // IsEdit
		private string _IsEdit="";
    }

	/// <summary></summary>	
	[Description("")]
    public partial class S_P_DoorTemplate
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID (Primary key)
		private string _ID="";
		/// <summary>自定义类别，User或Group</summary>	
		[Description("自定义类别，User或Group")]
        public string Type { get{return _Type;} set{_Type = value??"";} } // Type
		private string _Type="";
		/// <summary></summary>	
		[Description("")]
        public string BaseType { get{return _BaseType;} set{_BaseType = value??"";} } // BaseType
		private string _BaseType="";
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
        public string IsDefault { get{return _IsDefault;} set{_IsDefault = value??"";} } // IsDefault
		private string _IsDefault="";
		/// <summary></summary>	
		[Description("")]
        public string TemplateColWidth { get{return _TemplateColWidth;} set{_TemplateColWidth = value??"";} } // TemplateColWidth
		private string _TemplateColWidth="";
		/// <summary></summary>	
		[Description("")]
        public string TemplateString { get{return _TemplateString;} set{_TemplateString = value??"";} } // TemplateString
		private string _TemplateString="";
		/// <summary></summary>	
		[Description("")]
        public string BaseTemplateId { get{return _BaseTemplateId;} set{_BaseTemplateId = value??"";} } // BaseTemplateId
		private string _BaseTemplateId="";
    }

	/// <summary></summary>	
	[Description("")]
    public partial class S_R_DataSet
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID (Primary key)
		private string _ID="";
		/// <summary></summary>	
		[Description("")]
        public string DefineID { get{return _DefineID;} set{_DefineID = value??"";} } // DefineID
		private string _DefineID="";
		/// <summary></summary>	
		[Description("")]
        public string Name { get{return _Name;} set{_Name = value??"";} } // Name
		private string _Name="";
		/// <summary></summary>	
		[Description("")]
        public string ConnName { get{return _ConnName;} set{_ConnName = value??"";} } // ConnName
		private string _ConnName="";
		/// <summary></summary>	
		[Description("")]
        public string TableNames { get{return _TableNames;} set{_TableNames = value??"";} } // TableNames
		private string _TableNames="";
		/// <summary></summary>	
		[Description("")]
        public string Sql { get{return _Sql;} set{_Sql = value??"";} } // Sql
		private string _Sql="";

        // Reverse navigation
		[JsonIgnore]
        public virtual ICollection<S_R_Field> S_R_Field { get { onS_R_FieldGetting(); return _S_R_Field;} }
		private ICollection<S_R_Field> _S_R_Field;
		partial void onS_R_FieldGetting();


        // Foreign keys
		[JsonIgnore]
        public virtual S_R_Define S_R_Define { get; set; } //  DefineID - FK_S_R_DataSet_S_R_Define

        public S_R_DataSet()
        {
            _S_R_Field = new List<S_R_Field>();
        }
    }

	/// <summary></summary>	
	[Description("")]
    public partial class S_R_Define
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID (Primary key)
		private string _ID="";
		/// <summary></summary>	
		[Description("")]
        public string Code { get{return _Code;} set{_Code = value??"";} } // Code
		private string _Code="";
		/// <summary></summary>	
		[Description("")]
        public string Name { get{return _Name;} set{_Name = value??"";} } // Name
		private string _Name="";
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
        public DateTime? ModifyTime { get; set; } // ModifyTime
		/// <summary></summary>	
		[Description("")]
        public string CategoryID { get{return _CategoryID;} set{_CategoryID = value??"";} } // CategoryID
		private string _CategoryID="";
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
        public DateTime? CreateTime { get; set; } // CreateTime
		/// <summary></summary>	
		[Description("")]
        public string ConnName { get{return _ConnName;} set{_ConnName = value??"";} } // ConnName
		private string _ConnName="";

        // Reverse navigation
		[JsonIgnore]
        public virtual ICollection<S_R_DataSet> S_R_DataSet { get { onS_R_DataSetGetting(); return _S_R_DataSet;} }
		private ICollection<S_R_DataSet> _S_R_DataSet;
		partial void onS_R_DataSetGetting();


        public S_R_Define()
        {
            _S_R_DataSet = new List<S_R_DataSet>();
        }
    }

	/// <summary></summary>	
	[Description("")]
    public partial class S_R_Field
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID (Primary key)
		private string _ID="";
		/// <summary></summary>	
		[Description("")]
        public string DataSetID { get{return _DataSetID;} set{_DataSetID = value??"";} } // DataSetID
		private string _DataSetID="";
		/// <summary></summary>	
		[Description("")]
        public string Code { get{return _Code;} set{_Code = value??"";} } // Code
		private string _Code="";
		/// <summary></summary>	
		[Description("")]
        public string Name { get{return _Name;} set{_Name = value??"";} } // Name
		private string _Name="";
		/// <summary></summary>	
		[Description("")]
        public string Type { get{return _Type;} set{_Type = value??"";} } // Type
		private string _Type="";
		/// <summary></summary>	
		[Description("")]
        public string EnumKey { get{return _EnumKey;} set{_EnumKey = value??"";} } // EnumKey
		private string _EnumKey="";

        // Foreign keys
		[JsonIgnore]
        public virtual S_R_DataSet S_R_DataSet { get; set; } //  DataSetID - FK_S_R_Field_S_R_DataSet
    }

	/// <summary>AHCJ_BASE.S_RC_RULECODE</summary>	
	[Description("AHCJ_BASE.S_RC_RULECODE")]
    public partial class S_RC_RuleCode
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID (Primary key)
		private string _ID="";
		/// <summary></summary>	
		[Description("")]
        public string Code { get{return _Code;} set{_Code = value??"";} } // Code
		private string _Code="";
		/// <summary></summary>	
		[Description("")]
        public string RuleName { get{return _RuleName;} set{_RuleName = value??"";} } // RuleName
		private string _RuleName="";
		/// <summary></summary>	
		[Description("")]
        public string Prefix { get{return _Prefix;} set{_Prefix = value??"";} } // Prefix
		private string _Prefix="";
		/// <summary></summary>	
		[Description("")]
        public string PostFix { get{return _PostFix;} set{_PostFix = value??"";} } // PostFix
		private string _PostFix="";
		/// <summary></summary>	
		[Description("")]
        public string Seperative { get{return _Seperative;} set{_Seperative = value??"";} } // Seperative
		private string _Seperative="";
		/// <summary></summary>	
		[Description("")]
        public int? Digit { get; set; } // Digit
		/// <summary></summary>	
		[Description("")]
        public int? StartNumber { get; set; } // StartNumber
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
    }

	/// <summary>AHCJ_BASE.S_RC_RULECODEDATA</summary>	
	[Description("AHCJ_BASE.S_RC_RULECODEDATA")]
    public partial class S_RC_RuleCodeData
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID (Primary key)
		private string _ID="";
		/// <summary></summary>	
		[Description("")]
        public string Code { get{return _Code;} set{_Code = value??"";} } // Code
		private string _Code="";
		/// <summary></summary>	
		[Description("")]
        public long? Year { get; set; } // Year
		/// <summary></summary>	
		[Description("")]
        public long? AutoNumber { get; set; } // AutoNumber
    }

	/// <summary></summary>	
	[Description("")]
    public partial class S_S_Alarm
    {
		/// <summary>报警ID</summary>	
		[Description("报警ID")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID (Primary key)
		private string _ID="";
		/// <summary>重要度:枚举1,0(重要不重要)</summary>	
		[Description("重要度:枚举1,0(重要不重要)")]
        public string Important { get{return _Important;} set{_Important = value??"";} } // Important
		private string _Important="";
		/// <summary>紧急度:枚举1,0 (紧急不紧急)</summary>	
		[Description("紧急度:枚举1,0 (紧急不紧急)")]
        public string Urgency { get{return _Urgency;} set{_Urgency = value??"";} } // Urgency
		private string _Urgency="";
		/// <summary>报警类型</summary>	
		[Description("报警类型")]
        public string AlarmType { get{return _AlarmType;} set{_AlarmType = value??"";} } // AlarmType
		private string _AlarmType="";
		/// <summary>标题</summary>	
		[Description("标题")]
        public string AlarmTitle { get{return _AlarmTitle;} set{_AlarmTitle = value??"";} } // AlarmTitle
		private string _AlarmTitle="";
		/// <summary>正文内容</summary>	
		[Description("正文内容")]
        public string AlarmContent { get{return _AlarmContent;} set{_AlarmContent = value??"";} } // AlarmContent
		private string _AlarmContent="";
		/// <summary>链接地址</summary>	
		[Description("链接地址")]
        public string AlarmUrl { get{return _AlarmUrl;} set{_AlarmUrl = value??"";} } // AlarmUrl
		private string _AlarmUrl="";
		/// <summary>拥有人</summary>	
		[Description("拥有人")]
        public string OwnerName { get{return _OwnerName;} set{_OwnerName = value??"";} } // OwnerName
		private string _OwnerName="";
		/// <summary>拥有人ID</summary>	
		[Description("拥有人ID")]
        public string OwnerID { get{return _OwnerID;} set{_OwnerID = value??"";} } // OwnerID
		private string _OwnerID="";
		/// <summary>提醒时间</summary>	
		[Description("提醒时间")]
        public DateTime? SendTime { get; set; } // SendTime
		/// <summary>事务截止完成时间</summary>	
		[Description("事务截止完成时间")]
        public DateTime? DeadlineTime { get; set; } // DeadlineTime
		/// <summary></summary>	
		[Description("")]
        public string SenderName { get{return _SenderName;} set{_SenderName = value??"";} } // SenderName
		private string _SenderName="";
		/// <summary></summary>	
		[Description("")]
        public string SenderID { get{return _SenderID;} set{_SenderID = value??"";} } // SenderID
		private string _SenderID="";
		/// <summary></summary>	
		[Description("")]
        public string IsDelete { get{return _IsDelete;} set{_IsDelete = value??"";} } // IsDelete
		private string _IsDelete="";
    }

	/// <summary>短消息表</summary>	
	[Description("短消息表")]
    public partial class S_S_MsgBody
    {
		/// <summary>ID</summary>	
		[Description("ID")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID (Primary key)
		private string _ID="";
		/// <summary>父ID</summary>	
		[Description("父ID")]
        public string ParentID { get{return _ParentID;} set{_ParentID = value??"";} } // ParentID
		private string _ParentID="";
		/// <summary>类型</summary>	
		[Description("类型")]
        public string Type { get{return _Type;} set{_Type = value??"";} } // Type
		private string _Type="";
		/// <summary>标题</summary>	
		[Description("标题")]
        public string Title { get{return _Title;} set{_Title = value??"";} } // Title
		private string _Title="";
		/// <summary>内容</summary>	
		[Description("内容")]
        public string Content { get{return _Content;} set{_Content = value??"";} } // Content
		private string _Content="";
		/// <summary></summary>	
		[Description("")]
        public string ContentText { get{return _ContentText;} set{_ContentText = value??"";} } // ContentText
		private string _ContentText="";
		/// <summary>附件ID</summary>	
		[Description("附件ID")]
        public string AttachFileIDs { get{return _AttachFileIDs;} set{_AttachFileIDs = value??"";} } // AttachFileIDs
		private string _AttachFileIDs="";
		/// <summary>消息连接</summary>	
		[Description("消息连接")]
        public string LinkUrl { get{return _LinkUrl;} set{_LinkUrl = value??"";} } // LinkUrl
		private string _LinkUrl="";
		/// <summary>是否系统消息</summary>	
		[Description("是否系统消息")]
        public string IsSystemMsg { get{return _IsSystemMsg;} set{_IsSystemMsg = value??"";} } // IsSystemMsg
		private string _IsSystemMsg="";
		/// <summary>发送时间</summary>	
		[Description("发送时间")]
        public DateTime? SendTime { get; set; } // SendTime
		/// <summary>发送者ID</summary>	
		[Description("发送者ID")]
        public string SenderID { get{return _SenderID;} set{_SenderID = value??"";} } // SenderID
		private string _SenderID="";
		/// <summary>发送者姓名</summary>	
		[Description("发送者姓名")]
        public string SenderName { get{return _SenderName;} set{_SenderName = value??"";} } // SenderName
		private string _SenderName="";
		/// <summary>接收人ID</summary>	
		[Description("接收人ID")]
        public string ReceiverIDs { get{return _ReceiverIDs;} set{_ReceiverIDs = value??"";} } // ReceiverIDs
		private string _ReceiverIDs="";
		/// <summary>接收人姓名</summary>	
		[Description("接收人姓名")]
        public string ReceiverNames { get{return _ReceiverNames;} set{_ReceiverNames = value??"";} } // ReceiverNames
		private string _ReceiverNames="";
		/// <summary>接受部门ID</summary>	
		[Description("接受部门ID")]
        public string ReceiverDeptIDs { get{return _ReceiverDeptIDs;} set{_ReceiverDeptIDs = value??"";} } // ReceiverDeptIDs
		private string _ReceiverDeptIDs="";
		/// <summary>接受部门</summary>	
		[Description("接受部门")]
        public string ReceiverDeptNames { get{return _ReceiverDeptNames;} set{_ReceiverDeptNames = value??"";} } // ReceiverDeptNames
		private string _ReceiverDeptNames="";
		/// <summary>是否已经删除</summary>	
		[Description("是否已经删除")]
        public string IsDeleted { get{return _IsDeleted;} set{_IsDeleted = value??"";} } // IsDeleted
		private string _IsDeleted="";
		/// <summary>删除时间</summary>	
		[Description("删除时间")]
        public DateTime? DeleteTime { get; set; } // DeleteTime
		/// <summary>是否发送回执</summary>	
		[Description("是否发送回执")]
        public string IsReadReceipt { get{return _IsReadReceipt;} set{_IsReadReceipt = value??"";} } // IsReadReceipt
		private string _IsReadReceipt="";
		/// <summary>重要性</summary>	
		[Description("重要性")]
        public string Importance { get{return _Importance;} set{_Importance = value??"";} } // Importance
		private string _Importance="";

        // Reverse navigation
		[JsonIgnore]
        public virtual ICollection<S_S_MsgReceiver> S_S_MsgReceiver { get { onS_S_MsgReceiverGetting(); return _S_S_MsgReceiver;} }
		private ICollection<S_S_MsgReceiver> _S_S_MsgReceiver;
		partial void onS_S_MsgReceiverGetting();


        public S_S_MsgBody()
        {
			IsSystemMsg = "0";
			IsDeleted = "0";
			IsReadReceipt = "0";
			Importance = "0";
            _S_S_MsgReceiver = new List<S_S_MsgReceiver>();
        }
    }

	/// <summary>短消息接收人表</summary>	
	[Description("短消息接收人表")]
    public partial class S_S_MsgReceiver
    {
		/// <summary>ID</summary>	
		[Description("ID")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID (Primary key)
		private string _ID="";
		/// <summary>消息体ID</summary>	
		[Description("消息体ID")]
        public string MsgBodyID { get{return _MsgBodyID;} set{_MsgBodyID = value??"";} } // MsgBodyID
		private string _MsgBodyID="";
		/// <summary>接收人ID</summary>	
		[Description("接收人ID")]
        public string UserID { get{return _UserID;} set{_UserID = value??"";} } // UserID
		private string _UserID="";
		/// <summary>接收人姓名</summary>	
		[Description("接收人姓名")]
        public string UserName { get{return _UserName;} set{_UserName = value??"";} } // UserName
		private string _UserName="";
		/// <summary>首次查看时间</summary>	
		[Description("首次查看时间")]
        public DateTime? FirstViewTime { get; set; } // FirstViewTime
		/// <summary>回复时间</summary>	
		[Description("回复时间")]
        public DateTime? ReplyTime { get; set; } // ReplyTime
		/// <summary>是否已经删除</summary>	
		[Description("是否已经删除")]
        public string IsDeleted { get{return _IsDeleted;} set{_IsDeleted = value??"";} } // IsDeleted
		private string _IsDeleted="";
		/// <summary>删除时间</summary>	
		[Description("删除时间")]
        public DateTime? DeleteTime { get; set; } // DeleteTime

        // Foreign keys
		[JsonIgnore]
        public virtual S_S_MsgBody S_S_MsgBody { get; set; } //  MsgBodyID - FK_S_S_MsgReceiver_S_S_MsgBody

        public S_S_MsgReceiver()
        {
			IsDeleted = "0";
        }
    }

	/// <summary></summary>	
	[Description("")]
    public partial class S_UI_Form
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID (Primary key)
		private string _ID="";
		/// <summary></summary>	
		[Description("")]
        public string Code { get{return _Code;} set{_Code = value??"";} } // Code
		private string _Code="";
		/// <summary></summary>	
		[Description("")]
        public string Name { get{return _Name;} set{_Name = value??"";} } // Name
		private string _Name="";
		/// <summary></summary>	
		[Description("")]
        public string Category { get{return _Category;} set{_Category = value??"";} } // Category
		private string _Category="";
		/// <summary></summary>	
		[Description("")]
        public string ConnName { get{return _ConnName;} set{_ConnName = value??"";} } // ConnName
		private string _ConnName="";
		/// <summary></summary>	
		[Description("")]
        public string TableName { get{return _TableName;} set{_TableName = value??"";} } // TableName
		private string _TableName="";
		/// <summary></summary>	
		[Description("")]
        public string Description { get{return _Description;} set{_Description = value??"";} } // Description
		private string _Description="";
		/// <summary></summary>	
		[Description("")]
        public string Script { get{return _Script;} set{_Script = value??"";} } // Script
		private string _Script="";
		/// <summary></summary>	
		[Description("")]
        public string ScriptText { get{return _ScriptText;} set{_ScriptText = value??"";} } // ScriptText
		private string _ScriptText="";
		/// <summary></summary>	
		[Description("")]
        public string FlowLogic { get{return _FlowLogic;} set{_FlowLogic = value??"";} } // FlowLogic
		private string _FlowLogic="";
		/// <summary></summary>	
		[Description("")]
        public string HiddenFields { get{return _HiddenFields;} set{_HiddenFields = value??"";} } // HiddenFields
		private string _HiddenFields="";
		/// <summary></summary>	
		[Description("")]
        public string Layout { get{return _Layout;} set{_Layout = value??"";} } // Layout
		private string _Layout="";
		/// <summary></summary>	
		[Description("")]
        public string Items { get{return _Items;} set{_Items = value??"";} } // Items
		private string _Items="";
		/// <summary></summary>	
		[Description("")]
        public string Setttings { get{return _Setttings;} set{_Setttings = value??"";} } // Setttings
		private string _Setttings="";
		/// <summary></summary>	
		[Description("")]
        public string SerialNumberSettings { get{return _SerialNumberSettings;} set{_SerialNumberSettings = value??"";} } // SerialNumberSettings
		private string _SerialNumberSettings="";
		/// <summary></summary>	
		[Description("")]
        public string DefaultValueSettings { get{return _DefaultValueSettings;} set{_DefaultValueSettings = value??"";} } // DefaultValueSettings
		private string _DefaultValueSettings="";
		/// <summary></summary>	
		[Description("")]
        public string CategoryID { get{return _CategoryID;} set{_CategoryID = value??"";} } // CategoryID
		private string _CategoryID="";
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
        public DateTime? ModifyTime { get; set; } // ModifyTime
		/// <summary></summary>	
		[Description("")]
        public DateTime? ReleaseTime { get; set; } // ReleaseTime
		/// <summary></summary>	
		[Description("")]
        public string ReleasedData { get{return _ReleasedData;} set{_ReleasedData = value??"";} } // ReleasedData
		private string _ReleasedData="";
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
        public DateTime? CreateTime { get; set; } // CreateTime
    }

	/// <summary></summary>	
	[Description("")]
    public partial class S_UI_List
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID (Primary key)
		private string _ID="";
		/// <summary></summary>	
		[Description("")]
        public string Code { get{return _Code;} set{_Code = value??"";} } // Code
		private string _Code="";
		/// <summary></summary>	
		[Description("")]
        public string Name { get{return _Name;} set{_Name = value??"";} } // Name
		private string _Name="";
		/// <summary></summary>	
		[Description("")]
        public string ConnName { get{return _ConnName;} set{_ConnName = value??"";} } // ConnName
		private string _ConnName="";
		/// <summary></summary>	
		[Description("")]
        public string SQL { get{return _SQL;} set{_SQL = value??"";} } // SQL
		private string _SQL="";
		/// <summary></summary>	
		[Description("")]
        public string TableNames { get{return _TableNames;} set{_TableNames = value??"";} } // TableNames
		private string _TableNames="";
		/// <summary></summary>	
		[Description("")]
        public string Script { get{return _Script;} set{_Script = value??"";} } // Script
		private string _Script="";
		/// <summary></summary>	
		[Description("")]
        public string ScriptText { get{return _ScriptText;} set{_ScriptText = value??"";} } // ScriptText
		private string _ScriptText="";
		/// <summary></summary>	
		[Description("")]
        public string HasRowNumber { get{return _HasRowNumber;} set{_HasRowNumber = value??"";} } // HasRowNumber
		private string _HasRowNumber="";
		/// <summary></summary>	
		[Description("")]
        public string LayoutGrid { get{return _LayoutGrid;} set{_LayoutGrid = value??"";} } // LayoutGrid
		private string _LayoutGrid="";
		/// <summary></summary>	
		[Description("")]
        public string LayoutField { get{return _LayoutField;} set{_LayoutField = value??"";} } // LayoutField
		private string _LayoutField="";
		/// <summary></summary>	
		[Description("")]
        public string LayoutSearch { get{return _LayoutSearch;} set{_LayoutSearch = value??"";} } // LayoutSearch
		private string _LayoutSearch="";
		/// <summary></summary>	
		[Description("")]
        public string LayoutButton { get{return _LayoutButton;} set{_LayoutButton = value??"";} } // LayoutButton
		private string _LayoutButton="";
		/// <summary></summary>	
		[Description("")]
        public string Settings { get{return _Settings;} set{_Settings = value??"";} } // Settings
		private string _Settings="";
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
        public DateTime? ModifyTime { get; set; } // ModifyTime
		/// <summary></summary>	
		[Description("")]
        public string CategoryID { get{return _CategoryID;} set{_CategoryID = value??"";} } // CategoryID
		private string _CategoryID="";
		/// <summary></summary>	
		[Description("")]
        public string Released { get{return _Released;} set{_Released = value??"";} } // Released
		private string _Released="";
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
        public DateTime? CreateTime { get; set; } // CreateTime
		/// <summary></summary>	
		[Description("")]
        public string DenyDeleteFlow { get{return _DenyDeleteFlow;} set{_DenyDeleteFlow = value??"";} } // DenyDeleteFlow
		private string _DenyDeleteFlow="";
    }

	/// <summary></summary>	
	[Description("")]
    public partial class S_UI_ModifyLog
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID (Primary key)
		private string _ID="";
		/// <summary></summary>	
		[Description("")]
        public string TableName { get{return _TableName;} set{_TableName = value??"";} } // TableName
		private string _TableName="";
		/// <summary></summary>	
		[Description("")]
        public DateTime? ModifyTime { get; set; } // ModifyTime
		/// <summary></summary>	
		[Description("")]
        public string ModifyType { get{return _ModifyType;} set{_ModifyType = value??"";} } // ModifyType
		private string _ModifyType="";
		/// <summary></summary>	
		[Description("")]
        public string Name { get{return _Name;} set{_Name = value??"";} } // Name
		private string _Name="";
    }

	/// <summary></summary>	
	[Description("")]
    public partial class S_UI_Selector
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID (Primary key)
		private string _ID="";
		/// <summary></summary>	
		[Description("")]
        public string Code { get{return _Code;} set{_Code = value??"";} } // Code
		private string _Code="";
		/// <summary></summary>	
		[Description("")]
        public string Name { get{return _Name;} set{_Name = value??"";} } // Name
		private string _Name="";
		/// <summary></summary>	
		[Description("")]
        public string URLSingle { get{return _URLSingle;} set{_URLSingle = value??"";} } // URLSingle
		private string _URLSingle="";
		/// <summary></summary>	
		[Description("")]
        public string URLMulti { get{return _URLMulti;} set{_URLMulti = value??"";} } // URLMulti
		private string _URLMulti="";
		/// <summary></summary>	
		[Description("")]
        public string Width { get{return _Width;} set{_Width = value??"";} } // Width
		private string _Width="";
		/// <summary></summary>	
		[Description("")]
        public string Height { get{return _Height;} set{_Height = value??"";} } // Height
		private string _Height="";
		/// <summary></summary>	
		[Description("")]
        public string Title { get{return _Title;} set{_Title = value??"";} } // Title
		private string _Title="";
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
        public DateTime? ModifyTime { get; set; } // ModifyTime
		/// <summary></summary>	
		[Description("")]
        public string CategoryID { get{return _CategoryID;} set{_CategoryID = value??"";} } // CategoryID
		private string _CategoryID="";
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
        public DateTime? CreateTime { get; set; } // CreateTime
    }

	/// <summary></summary>	
	[Description("")]
    public partial class S_UI_SerialNumber
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID (Primary key)
		private string _ID="";
		/// <summary></summary>	
		[Description("")]
        public string Code { get{return _Code;} set{_Code = value??"";} } // Code
		private string _Code="";
		/// <summary></summary>	
		[Description("")]
        public string YearCode { get{return _YearCode;} set{_YearCode = value??"";} } // YearCode
		private string _YearCode="";
		/// <summary></summary>	
		[Description("")]
        public string MonthCode { get{return _MonthCode;} set{_MonthCode = value??"";} } // MonthCode
		private string _MonthCode="";
		/// <summary></summary>	
		[Description("")]
        public string DayCode { get{return _DayCode;} set{_DayCode = value??"";} } // DayCode
		private string _DayCode="";
		/// <summary></summary>	
		[Description("")]
        public string CategoryCode { get{return _CategoryCode;} set{_CategoryCode = value??"";} } // CategoryCode
		private string _CategoryCode="";
		/// <summary></summary>	
		[Description("")]
        public string SubCategoryCode { get{return _SubCategoryCode;} set{_SubCategoryCode = value??"";} } // SubCategoryCode
		private string _SubCategoryCode="";
		/// <summary></summary>	
		[Description("")]
        public string OrderNumCode { get{return _OrderNumCode;} set{_OrderNumCode = value??"";} } // OrderNumCode
		private string _OrderNumCode="";
		/// <summary></summary>	
		[Description("")]
        public string PrjCode { get{return _PrjCode;} set{_PrjCode = value??"";} } // PrjCode
		private string _PrjCode="";
		/// <summary></summary>	
		[Description("")]
        public string OrgCode { get{return _OrgCode;} set{_OrgCode = value??"";} } // OrgCode
		private string _OrgCode="";
		/// <summary></summary>	
		[Description("")]
        public string UserCode { get{return _UserCode;} set{_UserCode = value??"";} } // UserCode
		private string _UserCode="";
		/// <summary></summary>	
		[Description("")]
        public int? Number { get; set; } // Number
    }

	/// <summary></summary>	
	[Description("")]
    public partial class S_UI_Word
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID (Primary key)
		private string _ID="";
		/// <summary></summary>	
		[Description("")]
        public string Code { get{return _Code;} set{_Code = value??"";} } // Code
		private string _Code="";
		/// <summary></summary>	
		[Description("")]
        public string Name { get{return _Name;} set{_Name = value??"";} } // Name
		private string _Name="";
		/// <summary></summary>	
		[Description("")]
        public string ConnName { get{return _ConnName;} set{_ConnName = value??"";} } // ConnName
		private string _ConnName="";
		/// <summary></summary>	
		[Description("")]
        public string TableNames { get{return _TableNames;} set{_TableNames = value??"";} } // TableNames
		private string _TableNames="";
		/// <summary></summary>	
		[Description("")]
        public string SQL { get{return _SQL;} set{_SQL = value??"";} } // SQL
		private string _SQL="";
		/// <summary></summary>	
		[Description("")]
        public string Description { get{return _Description;} set{_Description = value??"";} } // Description
		private string _Description="";
		/// <summary></summary>	
		[Description("")]
        public string Items { get{return _Items;} set{_Items = value??"";} } // Items
		private string _Items="";
		/// <summary></summary>	
		[Description("")]
        public string CategoryID { get{return _CategoryID;} set{_CategoryID = value??"";} } // CategoryID
		private string _CategoryID="";
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
        public DateTime? ModifyTime { get; set; } // ModifyTime
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
        public DateTime? CreateTime { get; set; } // CreateTime
    }

	/// <summary></summary>	
	[Description("")]
    public partial class S_UIH5_Form
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get{return _ID;} set{_ID = value??"";} } // ID (Primary key)
		private string _ID="";
		/// <summary></summary>	
		[Description("")]
        public string Code { get{return _Code;} set{_Code = value??"";} } // Code
		private string _Code="";
		/// <summary></summary>	
		[Description("")]
        public string Name { get{return _Name;} set{_Name = value??"";} } // Name
		private string _Name="";
		/// <summary></summary>	
		[Description("")]
        public string NameEN { get{return _NameEN;} set{_NameEN = value??"";} } // NameEN
		private string _NameEN="";
		/// <summary></summary>	
		[Description("")]
        public string ListName { get{return _ListName;} set{_ListName = value??"";} } // ListName
		private string _ListName="";
		/// <summary></summary>	
		[Description("")]
        public string ListNameEN { get{return _ListNameEN;} set{_ListNameEN = value??"";} } // ListNameEN
		private string _ListNameEN="";
		/// <summary></summary>	
		[Description("")]
        public string ConnName { get{return _ConnName;} set{_ConnName = value??"";} } // ConnName
		private string _ConnName="";
		/// <summary></summary>	
		[Description("")]
        public string TableName { get{return _TableName;} set{_TableName = value??"";} } // TableName
		private string _TableName="";
		/// <summary></summary>	
		[Description("")]
        public string ListSql { get{return _ListSql;} set{_ListSql = value??"";} } // ListSql
		private string _ListSql="";
		/// <summary></summary>	
		[Description("")]
        public string DefaultValueSettings { get{return _DefaultValueSettings;} set{_DefaultValueSettings = value??"";} } // DefaultValueSettings
		private string _DefaultValueSettings="";
		/// <summary></summary>	
		[Description("")]
        public string FlowLogic { get{return _FlowLogic;} set{_FlowLogic = value??"";} } // FlowLogic
		private string _FlowLogic="";
		/// <summary></summary>	
		[Description("")]
        public string AngularScript { get{return _AngularScript;} set{_AngularScript = value??"";} } // AngularScript
		private string _AngularScript="";
		/// <summary></summary>	
		[Description("")]
        public string ListSqlOrderBy { get{return _ListSqlOrderBy;} set{_ListSqlOrderBy = value??"";} } // ListSqlOrderBy
		private string _ListSqlOrderBy="";
		/// <summary></summary>	
		[Description("")]
        public string Description { get{return _Description;} set{_Description = value??"";} } // Description
		private string _Description="";
		/// <summary></summary>	
		[Description("")]
        public string Items { get{return _Items;} set{_Items = value??"";} } // Items
		private string _Items="";
		/// <summary></summary>	
		[Description("")]
        public string CategoryID { get{return _CategoryID;} set{_CategoryID = value??"";} } // CategoryID
		private string _CategoryID="";
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
        public DateTime? ModifyTime { get; set; } // ModifyTime
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
        public DateTime? CreateTime { get; set; } // CreateTime
    }


    // ************************************************************************
    // POCO Configuration

    // S_A__OrgRes
    internal partial class S_A__OrgResConfiguration : EntityTypeConfiguration<S_A__OrgRes>
    {
        public S_A__OrgResConfiguration()
        {
			ToTable("S_A__ORGRES");
            HasKey(x => new { x.ResID, x.OrgID });

            Property(x => x.ResID).HasColumnName("RESID").IsRequired().HasMaxLength(50);
            Property(x => x.OrgID).HasColumnName("ORGID").IsRequired().HasMaxLength(50);

            // Foreign keys
            HasRequired(a => a.S_A_Res).WithMany(b => b.S_A__OrgRes).HasForeignKey(c => c.ResID); // FK_S_A__OrgRes_S_A_Res
            HasRequired(a => a.S_A_Org).WithMany(b => b.S_A__OrgRes).HasForeignKey(c => c.OrgID); // FK_S_A__OrgRes_S_A_Org
        }
    }

    // S_A__OrgRole
    internal partial class S_A__OrgRoleConfiguration : EntityTypeConfiguration<S_A__OrgRole>
    {
        public S_A__OrgRoleConfiguration()
        {
			ToTable("S_A__ORGROLE");
            HasKey(x => new { x.RoleID, x.OrgID });

            Property(x => x.RoleID).HasColumnName("ROLEID").IsRequired().HasMaxLength(50);
            Property(x => x.OrgID).HasColumnName("ORGID").IsRequired().HasMaxLength(50);

            // Foreign keys
            HasRequired(a => a.S_A_Role).WithMany(b => b.S_A__OrgRole).HasForeignKey(c => c.RoleID); // FK_A_OrgRole_ARole
            HasRequired(a => a.S_A_Org).WithMany(b => b.S_A__OrgRole).HasForeignKey(c => c.OrgID); // FK_A_OrgRole_AOrg
        }
    }

    // S_A__OrgRoleUser
    internal partial class S_A__OrgRoleUserConfiguration : EntityTypeConfiguration<S_A__OrgRoleUser>
    {
        public S_A__OrgRoleUserConfiguration()
        {
			ToTable("S_A__ORGROLEUSER");
            HasKey(x => new { x.OrgID, x.RoleID, x.UserID });

            Property(x => x.OrgID).HasColumnName("ORGID").IsRequired().HasMaxLength(50);
            Property(x => x.RoleID).HasColumnName("ROLEID").IsRequired().HasMaxLength(50);
            Property(x => x.UserID).HasColumnName("USERID").IsRequired().HasMaxLength(50);

            // Foreign keys
            HasRequired(a => a.S_A_Role).WithMany(b => b.S_A__OrgRoleUser).HasForeignKey(c => c.RoleID); // FK_S_A__OrgRoleUser_S_A_Role
        }
    }

    // S_A__OrgUser
    internal partial class S_A__OrgUserConfiguration : EntityTypeConfiguration<S_A__OrgUser>
    {
        public S_A__OrgUserConfiguration()
        {
			ToTable("S_A__ORGUSER");
            HasKey(x => new { x.OrgID, x.UserID });

            Property(x => x.OrgID).HasColumnName("ORGID").IsRequired().HasMaxLength(50);
            Property(x => x.UserID).HasColumnName("USERID").IsRequired().HasMaxLength(50);

            // Foreign keys
            HasRequired(a => a.S_A_Org).WithMany(b => b.S_A__OrgUser).HasForeignKey(c => c.OrgID); // FK_A_OrgUser_AOrg
            HasRequired(a => a.S_A_User).WithMany(b => b.S_A__OrgUser).HasForeignKey(c => c.UserID); // FK_A_OrgUser_AUser
        }
    }

    // S_A__RoleRes
    internal partial class S_A__RoleResConfiguration : EntityTypeConfiguration<S_A__RoleRes>
    {
        public S_A__RoleResConfiguration()
        {
			ToTable("S_A__ROLERES");
            HasKey(x => new { x.ResID, x.RoleID });

            Property(x => x.ResID).HasColumnName("RESID").IsRequired().HasMaxLength(50);
            Property(x => x.RoleID).HasColumnName("ROLEID").IsRequired().HasMaxLength(50);

            // Foreign keys
            HasRequired(a => a.S_A_Res).WithMany(b => b.S_A__RoleRes).HasForeignKey(c => c.ResID); // FK_S_A__RoleRes_S_A_Res
            HasRequired(a => a.S_A_Role).WithMany(b => b.S_A__RoleRes).HasForeignKey(c => c.RoleID); // FK_S_A__RoleRes_S_A_Role
        }
    }

    // S_A__RoleUser
    internal partial class S_A__RoleUserConfiguration : EntityTypeConfiguration<S_A__RoleUser>
    {
        public S_A__RoleUserConfiguration()
        {
			ToTable("S_A__ROLEUSER");
            HasKey(x => new { x.RoleID, x.UserID });

            Property(x => x.RoleID).HasColumnName("ROLEID").IsRequired().HasMaxLength(50);
            Property(x => x.UserID).HasColumnName("USERID").IsRequired().HasMaxLength(50);

            // Foreign keys
            HasRequired(a => a.S_A_Role).WithMany(b => b.S_A__RoleUser).HasForeignKey(c => c.RoleID); // FK_A_RoleUser_ARole
            HasRequired(a => a.S_A_User).WithMany(b => b.S_A__RoleUser).HasForeignKey(c => c.UserID); // FK_A_RoleUser_AUser
        }
    }

    // S_A__UserRes
    internal partial class S_A__UserResConfiguration : EntityTypeConfiguration<S_A__UserRes>
    {
        public S_A__UserResConfiguration()
        {
			ToTable("S_A__USERRES");
            HasKey(x => new { x.UserID, x.ResID });

            Property(x => x.UserID).HasColumnName("USERID").IsRequired().HasMaxLength(50);
            Property(x => x.ResID).HasColumnName("RESID").IsRequired().HasMaxLength(50);
            Property(x => x.DenyAuth).HasColumnName("DENYAUTH").IsOptional().HasMaxLength(50);

            // Foreign keys
            HasRequired(a => a.S_A_User).WithMany(b => b.S_A__UserRes).HasForeignKey(c => c.UserID); // FK_S_A__UserRes_S_A_User
            HasRequired(a => a.S_A_Res).WithMany(b => b.S_A__UserRes).HasForeignKey(c => c.ResID); // FK_S_A__UserRes_S_A_Res
        }
    }

    // S_A_AuthInfo
    internal partial class S_A_AuthInfoConfiguration : EntityTypeConfiguration<S_A_AuthInfo>
    {
        public S_A_AuthInfoConfiguration()
        {
			ToTable("S_A_AUTHINFO");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(50);
            Property(x => x.OrgRootFullID).HasColumnName("ORGROOTFULLID").IsOptional().HasMaxLength(50);
            Property(x => x.ResRootFullID).HasColumnName("RESROOTFULLID").IsOptional().HasMaxLength(50);
            Property(x => x.RoleGroupID).HasColumnName("ROLEGROUPID").IsOptional().HasMaxLength(50);
            Property(x => x.UserGroupID).HasColumnName("USERGROUPID").IsOptional().HasMaxLength(50);
            Property(x => x.Description).HasColumnName("DESCRIPTION").IsOptional().HasMaxLength(500);
        }
    }

    // S_A_AuthLevel
    internal partial class S_A_AuthLevelConfiguration : EntityTypeConfiguration<S_A_AuthLevel>
    {
        public S_A_AuthLevelConfiguration()
        {
			ToTable("S_A_AUTHLEVEL");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.UserID).HasColumnName("USERID").IsOptional().HasMaxLength(50);
            Property(x => x.UserName).HasColumnName("USERNAME").IsOptional().HasMaxLength(50);
            Property(x => x.MenuRootFullID).HasColumnName("MENUROOTFULLID").IsOptional().HasMaxLength(2000);
            Property(x => x.MenuRootName).HasColumnName("MENUROOTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.RuleRootFullID).HasColumnName("RULEROOTFULLID").IsOptional().HasMaxLength(2000);
            Property(x => x.RuleRootName).HasColumnName("RULEROOTNAME").IsOptional().HasMaxLength(200);
        }
    }

    // S_A_AuthLog
    internal partial class S_A_AuthLogConfiguration : EntityTypeConfiguration<S_A_AuthLog>
    {
        public S_A_AuthLogConfiguration()
        {
			ToTable("S_A_AUTHLOG");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.Operation).HasColumnName("OPERATION").IsOptional().HasMaxLength(50);
            Property(x => x.OperationTarget).HasColumnName("OPERATIONTARGET").IsOptional().HasMaxLength(50);
            Property(x => x.RelateData).HasColumnName("RELATEDATA").IsOptional();
            Property(x => x.ModifyUserName).HasColumnName("MODIFYUSERNAME").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyTime).HasColumnName("MODIFYTIME").IsOptional();
            Property(x => x.ClientIP).HasColumnName("CLIENTIP").IsOptional().HasMaxLength(50);
        }
    }

    // S_A_Org
    internal partial class S_A_OrgConfiguration : EntityTypeConfiguration<S_A_Org>
    {
        public S_A_OrgConfiguration()
        {
			ToTable("S_A_ORG");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.ParentID).HasColumnName("PARENTID").IsOptional().HasMaxLength(50);
            Property(x => x.FullID).HasColumnName("FULLID").IsOptional().HasMaxLength(500);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(50);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(200);
            Property(x => x.Type).HasColumnName("TYPE").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.Description).HasColumnName("DESCRIPTION").IsOptional().HasMaxLength(500);
            Property(x => x.IsDeleted).HasColumnName("ISDELETED").IsRequired().HasMaxLength(1);
            Property(x => x.DeleteTime).HasColumnName("DELETETIME").IsOptional();
            Property(x => x.ShortName).HasColumnName("SHORTNAME").IsOptional().HasMaxLength(50);
            Property(x => x.Character).HasColumnName("CHARACTER").IsOptional().HasMaxLength(500);
            Property(x => x.Location).HasColumnName("LOCATION").IsOptional().HasMaxLength(50);
        }
    }

    // S_A_Res
    internal partial class S_A_ResConfiguration : EntityTypeConfiguration<S_A_Res>
    {
        public S_A_ResConfiguration()
        {
			ToTable("S_A_RES");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.ParentID).HasColumnName("PARENTID").IsOptional().HasMaxLength(50);
            Property(x => x.FullID).HasColumnName("FULLID").IsOptional().HasMaxLength(500);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(50);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(50);
            Property(x => x.Type).HasColumnName("TYPE").IsOptional().HasMaxLength(50);
            Property(x => x.Url).HasColumnName("URL").IsOptional().HasMaxLength(200);
            Property(x => x.IconUrl).HasColumnName("ICONURL").IsOptional().HasMaxLength(200);
            Property(x => x.Target).HasColumnName("TARGET").IsOptional().HasMaxLength(50);
            Property(x => x.ButtonID).HasColumnName("BUTTONID").IsOptional().HasMaxLength(500);
            Property(x => x.DataFilter).HasColumnName("DATAFILTER").IsOptional().HasMaxLength(2000);
            Property(x => x.Description).HasColumnName("DESCRIPTION").IsOptional().HasMaxLength(500);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
        }
    }

    // S_A_Role
    internal partial class S_A_RoleConfiguration : EntityTypeConfiguration<S_A_Role>
    {
        public S_A_RoleConfiguration()
        {
			ToTable("S_A_ROLE");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.GroupID).HasColumnName("GROUPID").IsOptional().HasMaxLength(50);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(50);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(50);
            Property(x => x.Type).HasColumnName("TYPE").IsOptional().HasMaxLength(50);
            Property(x => x.Description).HasColumnName("DESCRIPTION").IsOptional().HasMaxLength(500);
        }
    }

    // S_A_Security
    internal partial class S_A_SecurityConfiguration : EntityTypeConfiguration<S_A_Security>
    {
        public S_A_SecurityConfiguration()
        {
			ToTable("S_A_SECURITY");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.SuperAdmin).HasColumnName("SUPERADMIN").IsOptional().HasMaxLength(50);
            Property(x => x.SuperAdminPwd).HasColumnName("SUPERADMINPWD").IsOptional().HasMaxLength(50);
            Property(x => x.SuperAdminSecurity).HasColumnName("SUPERADMINSECURITY").IsOptional().HasMaxLength(500);
            Property(x => x.SuperAdminModifyTime).HasColumnName("SUPERADMINMODIFYTIME").IsOptional();
            Property(x => x.AdminIDs).HasColumnName("ADMINIDS").IsOptional();
            Property(x => x.AdminNames).HasColumnName("ADMINNAMES").IsOptional();
            Property(x => x.AdminModifyTime).HasColumnName("ADMINMODIFYTIME").IsOptional();
            Property(x => x.AdminSecurity).HasColumnName("ADMINSECURITY").IsOptional().HasMaxLength(500);
        }
    }

    // S_A_User
    internal partial class S_A_UserConfiguration : EntityTypeConfiguration<S_A_User>
    {
        public S_A_UserConfiguration()
        {
			ToTable("S_A_USER");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.GroupID).HasColumnName("GROUPID").IsOptional().HasMaxLength(50);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(50);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(50);
            Property(x => x.WorkNo).HasColumnName("WORKNO").IsOptional().HasMaxLength(50);
            Property(x => x.Password).HasColumnName("PASSWORD").IsOptional().HasMaxLength(50);
            Property(x => x.Sex).HasColumnName("SEX").IsOptional().HasMaxLength(50);
            Property(x => x.Description).HasColumnName("DESCRIPTION").IsOptional().HasMaxLength(500);
            Property(x => x.InDate).HasColumnName("INDATE").IsOptional();
            Property(x => x.OutDate).HasColumnName("OUTDATE").IsOptional();
            Property(x => x.Phone).HasColumnName("PHONE").IsOptional().HasMaxLength(50);
            Property(x => x.MobilePhone).HasColumnName("MOBILEPHONE").IsOptional().HasMaxLength(50);
            Property(x => x.Email).HasColumnName("EMAIL").IsOptional().HasMaxLength(50);
            Property(x => x.Address).HasColumnName("ADDRESS").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.LastLoginTime).HasColumnName("LASTLOGINTIME").IsOptional().HasMaxLength(50);
            Property(x => x.LastLoginIP).HasColumnName("LASTLOGINIP").IsOptional().HasMaxLength(50);
            Property(x => x.LastSessionID).HasColumnName("LASTSESSIONID").IsOptional().HasMaxLength(50);
            Property(x => x.ErrorCount).HasColumnName("ERRORCOUNT").IsOptional();
            Property(x => x.ErrorTime).HasColumnName("ERRORTIME").IsOptional();
            Property(x => x.IsDeleted).HasColumnName("ISDELETED").IsRequired().HasMaxLength(1);
            Property(x => x.DeleteTime).HasColumnName("DELETETIME").IsOptional();
            Property(x => x.PrjID).HasColumnName("PRJID").IsOptional().HasMaxLength(50);
            Property(x => x.PrjName).HasColumnName("PRJNAME").IsOptional().HasMaxLength(200);
            Property(x => x.DeptID).HasColumnName("DEPTID").IsOptional().HasMaxLength(50);
            Property(x => x.DeptFullID).HasColumnName("DEPTFULLID").IsOptional().HasMaxLength(500);
            Property(x => x.DeptName).HasColumnName("DEPTNAME").IsOptional().HasMaxLength(50);
            Property(x => x.RTX).HasColumnName("RTX").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyTime).HasColumnName("MODIFYTIME").IsOptional();
            Property(x => x.Weixin).HasColumnName("WEIXIN").IsOptional().HasMaxLength(100);
        }
    }

    // S_A_UserImg
    internal partial class S_A_UserImgConfiguration : EntityTypeConfiguration<S_A_UserImg>
    {
        public S_A_UserImgConfiguration()
        {
			ToTable("S_A_USERIMG");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.UserID).HasColumnName("USERID").IsOptional().HasMaxLength(50);
            Property(x => x.SignImg).HasColumnName("SIGNIMG").IsOptional().HasMaxLength(2147483647);
            Property(x => x.Picture).HasColumnName("PICTURE").IsOptional().HasMaxLength(2147483647);

            // Foreign keys
            HasOptional(a => a.S_A_User).WithMany(b => b.S_A_UserImg).HasForeignKey(c => c.UserID); // FK_S_A_UserImg_S_A_User
        }
    }

    // S_A_UserLinkMan
    internal partial class S_A_UserLinkManConfiguration : EntityTypeConfiguration<S_A_UserLinkMan>
    {
        public S_A_UserLinkManConfiguration()
        {
			ToTable("S_A_USERLINKMAN");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.UserID).HasColumnName("USERID").IsOptional().HasMaxLength(50);
            Property(x => x.LinkManID).HasColumnName("LINKMANID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();

            // Foreign keys
            HasOptional(a => a.S_A_User).WithMany(b => b.S_A_UserLinkMan).HasForeignKey(c => c.UserID); // FK_S_A_UserLinkMan_S_A_User
        }
    }

    // S_C_Holiday
    internal partial class S_C_HolidayConfiguration : EntityTypeConfiguration<S_C_Holiday>
    {
        public S_C_HolidayConfiguration()
        {
			ToTable("S_C_HOLIDAY");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.Year).HasColumnName("YEAR").IsOptional();
            Property(x => x.Month).HasColumnName("MONTH").IsOptional();
            Property(x => x.Day).HasColumnName("DAY").IsOptional();
            Property(x => x.Date).HasColumnName("DATE").IsOptional();
            Property(x => x.DayOfWeek).HasColumnName("DAYOFWEEK").IsOptional().HasMaxLength(50);
            Property(x => x.IsHoliday).HasColumnName("ISHOLIDAY").IsOptional().HasMaxLength(1);
        }
    }

    // S_D_FormToPDFRegist
    internal partial class S_D_FormToPDFRegistConfiguration : EntityTypeConfiguration<S_D_FormToPDFRegist>
    {
        public S_D_FormToPDFRegistConfiguration()
        {
			ToTable("S_D_FORMTOPDFREGIST");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.FormName).HasColumnName("FORMNAME").IsOptional().HasMaxLength(200);
            Property(x => x.TableName).HasColumnName("TABLENAME").IsOptional().HasMaxLength(200);
            Property(x => x.TempCode).HasColumnName("TEMPCODE").IsOptional().HasMaxLength(200);
            Property(x => x.ConnName).HasColumnName("CONNNAME").IsOptional().HasMaxLength(200);
        }
    }

    // S_D_FormToPDFTask
    internal partial class S_D_FormToPDFTaskConfiguration : EntityTypeConfiguration<S_D_FormToPDFTask>
    {
        public S_D_FormToPDFTaskConfiguration()
        {
			ToTable("S_D_FORMTOPDFTASK");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.TempCode).HasColumnName("TEMPCODE").IsOptional().HasMaxLength(100);
            Property(x => x.FormID).HasColumnName("FORMID").IsOptional().HasMaxLength(100);
            Property(x => x.PDFFileID).HasColumnName("PDFFILEID").IsOptional().HasMaxLength(100);
            Property(x => x.FormLastModifyDate).HasColumnName("FORMLASTMODIFYDATE").IsOptional();
            Property(x => x.BeginTime).HasColumnName("BEGINTIME").IsOptional();
            Property(x => x.EndTime).HasColumnName("ENDTIME").IsOptional();
            Property(x => x.DoneLog).HasColumnName("DONELOG").IsOptional();
            Property(x => x.State).HasColumnName("STATE").IsOptional().HasMaxLength(50);
        }
    }

    // S_D_ModifyLog
    internal partial class S_D_ModifyLogConfiguration : EntityTypeConfiguration<S_D_ModifyLog>
    {
        public S_D_ModifyLogConfiguration()
        {
			ToTable("S_D_MODIFYLOG");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.ConnName).HasColumnName("CONNNAME").IsOptional().HasMaxLength(50);
            Property(x => x.TableName).HasColumnName("TABLENAME").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyMode).HasColumnName("MODIFYMODE").IsOptional().HasMaxLength(50);
            Property(x => x.EntityKey).HasColumnName("ENTITYKEY").IsOptional().HasMaxLength(200);
            Property(x => x.CurrentValue).HasColumnName("CURRENTVALUE").IsOptional().HasMaxLength(2000);
            Property(x => x.OriginalValue).HasColumnName("ORIGINALVALUE").IsOptional().HasMaxLength(2000);
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUserName).HasColumnName("MODIFYUSERNAME").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyTime).HasColumnName("MODIFYTIME").IsOptional();
            Property(x => x.ClientIP).HasColumnName("CLIENTIP").IsOptional().HasMaxLength(50);
            Property(x => x.UserHostAddress).HasColumnName("USERHOSTADDRESS").IsOptional().HasMaxLength(50);
        }
    }

    // S_D_PDFTask
    internal partial class S_D_PDFTaskConfiguration : EntityTypeConfiguration<S_D_PDFTask>
    {
        public S_D_PDFTaskConfiguration()
        {
			ToTable("S_D_PDFTASK");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.FileID).HasColumnName("FILEID").IsOptional().HasMaxLength(50);
            Property(x => x.FileType).HasColumnName("FILETYPE").IsOptional().HasMaxLength(50);
            Property(x => x.PDFFileID).HasColumnName("PDFFILEID").IsOptional().HasMaxLength(50);
            Property(x => x.SWFFileID).HasColumnName("SWFFILEID").IsOptional().HasMaxLength(50);
            Property(x => x.SnapFileID).HasColumnName("SNAPFILEID").IsOptional().HasMaxLength(50);
            Property(x => x.PDFPageCount).HasColumnName("PDFPAGECOUNT").IsRequired();
            Property(x => x.IsSplit).HasColumnName("ISSPLIT").IsRequired().HasMaxLength(10);
            Property(x => x.Status).HasColumnName("STATUS").IsOptional().HasMaxLength(50);
            Property(x => x.StartTime).HasColumnName("STARTTIME").IsOptional();
            Property(x => x.EndTime).HasColumnName("ENDTIME").IsOptional();
            Property(x => x.Remark).HasColumnName("REMARK").IsOptional();
        }
    }

    // S_D_PushTask
    internal partial class S_D_PushTaskConfiguration : EntityTypeConfiguration<S_D_PushTask>
    {
        public S_D_PushTaskConfiguration()
        {
			ToTable("S_D_PUSHTASK");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.SourceID).HasColumnName("SOURCEID").IsOptional().HasMaxLength(50);
            Property(x => x.Title).HasColumnName("TITLE").IsOptional().HasMaxLength(100);
            Property(x => x.ShortContent).HasColumnName("SHORTCONTENT").IsOptional().HasMaxLength(500);
            Property(x => x.SourceType).HasColumnName("SOURCETYPE").IsOptional().HasMaxLength(100);
            Property(x => x.UserID).HasColumnName("USERID").IsOptional();
            Property(x => x.UserName).HasColumnName("USERNAME").IsOptional();
            Property(x => x.DeptID).HasColumnName("DEPTID").IsOptional();
            Property(x => x.DeptName).HasColumnName("DEPTNAME").IsOptional();
            Property(x => x.PushType).HasColumnName("PUSHTYPE").IsOptional().HasMaxLength(50);
            Property(x => x.BeginTime).HasColumnName("BEGINTIME").IsOptional();
            Property(x => x.EndTime).HasColumnName("ENDTIME").IsOptional();
            Property(x => x.CreateTime).HasColumnName("CREATETIME").IsOptional();
            Property(x => x.State).HasColumnName("STATE").IsOptional().HasMaxLength(50);
            Property(x => x.DoneLog).HasColumnName("DONELOG").IsOptional();
            Property(x => x.ClientID).HasColumnName("CLIENTID").IsOptional().HasMaxLength(50);
            Property(x => x.AppID).HasColumnName("APPID").IsOptional().HasMaxLength(50);
            Property(x => x.ChannelID).HasColumnName("CHANNELID").IsOptional().HasMaxLength(50);
            Property(x => x.DeviceOS).HasColumnName("DEVICEOS").IsOptional().HasMaxLength(50);
        }
    }

    // S_H_AllFeedback
    internal partial class S_H_AllFeedbackConfiguration : EntityTypeConfiguration<S_H_AllFeedback>
    {
        public S_H_AllFeedbackConfiguration()
        {
			ToTable("S_H_ALLFEEDBACK");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.Title).HasColumnName("TITLE").IsOptional().HasMaxLength(200);
            Property(x => x.Content).HasColumnName("CONTENT").IsOptional();
            Property(x => x.Url).HasColumnName("URL").IsOptional();
            Property(x => x.Attachment).HasColumnName("ATTACHMENT").IsOptional();
            Property(x => x.IsUse).HasColumnName("ISUSE").IsOptional().HasMaxLength(1);
            Property(x => x.CreateTime).HasColumnName("CREATETIME").IsOptional();
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUserName).HasColumnName("CREATEUSERNAME").IsOptional().HasMaxLength(50);
            Property(x => x.DealStatus).HasColumnName("DEALSTATUS").IsOptional().HasMaxLength(50);
            Property(x => x.DealResult).HasColumnName("DEALRESULT").IsOptional().HasMaxLength(500);
            Property(x => x.ProjectName).HasColumnName("PROJECTNAME").IsOptional().HasMaxLength(50);
            Property(x => x.DealUserName).HasColumnName("DEALUSERNAME").IsOptional().HasMaxLength(50);
            Property(x => x.Type).HasColumnName("TYPE").IsOptional().HasMaxLength(50);
            Property(x => x.Level).HasColumnName("LEVEL").IsOptional().HasMaxLength(50);
            Property(x => x.Module).HasColumnName("MODULE").IsOptional().HasMaxLength(50);
            Property(x => x.ExpectedCompleteTime).HasColumnName("EXPECTEDCOMPLETETIME").IsOptional();
            Property(x => x.ProblemNature).HasColumnName("PROBLEMNATURE").IsOptional().HasMaxLength(50);
            Property(x => x.ProjectPrincipal).HasColumnName("PROJECTPRINCIPAL").IsOptional().HasMaxLength(50);
            Property(x => x.ProjectDept).HasColumnName("PROJECTDEPT").IsOptional().HasMaxLength(50);
            Property(x => x.ConfirmProblemsTime).HasColumnName("CONFIRMPROBLEMSTIME").IsOptional();
            Property(x => x.PlanCompleteTime).HasColumnName("PLANCOMPLETETIME").IsOptional();
            Property(x => x.ActualCompleteTime).HasColumnName("ACTUALCOMPLETETIME").IsOptional();
            Property(x => x.ConfirmCompleteTime).HasColumnName("CONFIRMCOMPLETETIME").IsOptional();
            Property(x => x.ConfirmProblemsUserID).HasColumnName("CONFIRMPROBLEMSUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ConfirmProblemsUserName).HasColumnName("CONFIRMPROBLEMSUSERNAME").IsOptional().HasMaxLength(50);
            Property(x => x.ConfirmCompleteUserID).HasColumnName("CONFIRMCOMPLETEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ConfirmCompleteUserName).HasColumnName("CONFIRMCOMPLETEUSERNAME").IsOptional().HasMaxLength(50);
        }
    }

    // S_H_Calendar
    internal partial class S_H_CalendarConfiguration : EntityTypeConfiguration<S_H_Calendar>
    {
        public S_H_CalendarConfiguration()
        {
			ToTable("S_H_CALENDAR");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.Title).HasColumnName("TITLE").IsOptional().HasMaxLength(200);
            Property(x => x.Description).HasColumnName("DESCRIPTION").IsOptional().HasMaxLength(4000);
            Property(x => x.StartTime).HasColumnName("STARTTIME").IsOptional();
            Property(x => x.EndTime).HasColumnName("ENDTIME").IsOptional();
            Property(x => x.Url).HasColumnName("URL").IsOptional().HasMaxLength(4000);
            Property(x => x.Grade).HasColumnName("GRADE").IsOptional().HasMaxLength(20);
            Property(x => x.Attachments).HasColumnName("ATTACHMENTS").IsOptional().HasMaxLength(4000);
            Property(x => x.Remark).HasColumnName("REMARK").IsOptional();
            Property(x => x.Sponsor).HasColumnName("SPONSOR").IsOptional().HasMaxLength(50);
            Property(x => x.SponsorID).HasColumnName("SPONSORID").IsOptional().HasMaxLength(50);
            Property(x => x.Participators).HasColumnName("PARTICIPATORS").IsOptional();
            Property(x => x.ParticipatorsID).HasColumnName("PARTICIPATORSID").IsOptional();
            Property(x => x.CreateTime).HasColumnName("CREATETIME").IsOptional();
            Property(x => x.CreateUserName).HasColumnName("CREATEUSERNAME").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
        }
    }

    // S_H_Feedback
    internal partial class S_H_FeedbackConfiguration : EntityTypeConfiguration<S_H_Feedback>
    {
        public S_H_FeedbackConfiguration()
        {
			ToTable("S_H_FEEDBACK");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.Title).HasColumnName("TITLE").IsOptional().HasMaxLength(200);
            Property(x => x.Content).HasColumnName("CONTENT").IsOptional();
            Property(x => x.Url).HasColumnName("URL").IsOptional();
            Property(x => x.Attachment).HasColumnName("ATTACHMENT").IsOptional();
            Property(x => x.IsUse).HasColumnName("ISUSE").IsOptional().HasMaxLength(1);
            Property(x => x.CreateTime).HasColumnName("CREATETIME").IsOptional();
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUserName).HasColumnName("CREATEUSERNAME").IsOptional().HasMaxLength(50);
            Property(x => x.DealStatus).HasColumnName("DEALSTATUS").IsOptional().HasMaxLength(50);
            Property(x => x.DealResult).HasColumnName("DEALRESULT").IsOptional().HasMaxLength(500);
            Property(x => x.ProjectName).HasColumnName("PROJECTNAME").IsOptional().HasMaxLength(50);
            Property(x => x.DealUserName).HasColumnName("DEALUSERNAME").IsOptional().HasMaxLength(50);
            Property(x => x.Type).HasColumnName("TYPE").IsOptional().HasMaxLength(50);
            Property(x => x.Level).HasColumnName("LEVEL").IsOptional().HasMaxLength(50);
            Property(x => x.Module).HasColumnName("MODULE").IsOptional().HasMaxLength(50);
            Property(x => x.ExpectedCompleteTime).HasColumnName("EXPECTEDCOMPLETETIME").IsOptional();
            Property(x => x.ProblemNature).HasColumnName("PROBLEMNATURE").IsOptional().HasMaxLength(50);
            Property(x => x.ProjectPrincipal).HasColumnName("PROJECTPRINCIPAL").IsOptional().HasMaxLength(50);
            Property(x => x.ProjectDept).HasColumnName("PROJECTDEPT").IsOptional().HasMaxLength(50);
            Property(x => x.ConfirmProblemsTime).HasColumnName("CONFIRMPROBLEMSTIME").IsOptional();
            Property(x => x.PlanCompleteTime).HasColumnName("PLANCOMPLETETIME").IsOptional();
            Property(x => x.ActualCompleteTime).HasColumnName("ACTUALCOMPLETETIME").IsOptional();
            Property(x => x.ConfirmCompleteTime).HasColumnName("CONFIRMCOMPLETETIME").IsOptional();
            Property(x => x.ConfirmProblemsUserID).HasColumnName("CONFIRMPROBLEMSUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ConfirmProblemsUserName).HasColumnName("CONFIRMPROBLEMSUSERNAME").IsOptional().HasMaxLength(50);
            Property(x => x.ConfirmCompleteUserID).HasColumnName("CONFIRMCOMPLETEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ConfirmCompleteUserName).HasColumnName("CONFIRMCOMPLETEUSERNAME").IsOptional().HasMaxLength(50);
        }
    }

    // S_H_ShortCut
    internal partial class S_H_ShortCutConfiguration : EntityTypeConfiguration<S_H_ShortCut>
    {
        public S_H_ShortCutConfiguration()
        {
			ToTable("S_H_SHORTCUT");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.Type).HasColumnName("TYPE").IsOptional().HasMaxLength(50);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(50);
            Property(x => x.Url).HasColumnName("URL").IsOptional();
            Property(x => x.IconImage).HasColumnName("ICONIMAGE").IsOptional().HasMaxLength(250);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsUse).HasColumnName("ISUSE").IsOptional().HasMaxLength(1);
            Property(x => x.PageIndex).HasColumnName("PAGEINDEX").IsOptional();
            Property(x => x.CreateTime).HasColumnName("CREATETIME").IsOptional();
            Property(x => x.CreateUserName).HasColumnName("CREATEUSERNAME").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
        }
    }

    // S_I_FriendLink
    internal partial class S_I_FriendLinkConfiguration : EntityTypeConfiguration<S_I_FriendLink>
    {
        public S_I_FriendLinkConfiguration()
        {
			ToTable("S_I_FRIENDLINK");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.Icon).HasColumnName("ICON").IsOptional().HasMaxLength(100);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(200);
            Property(x => x.Url).HasColumnName("URL").IsOptional().HasMaxLength(200);
            Property(x => x.DeptId).HasColumnName("DEPTID").IsOptional().HasMaxLength(2000);
            Property(x => x.DeptName).HasColumnName("DEPTNAME").IsOptional().HasMaxLength(2000);
            Property(x => x.UserId).HasColumnName("USERID").IsOptional().HasMaxLength(2000);
            Property(x => x.UserName).HasColumnName("USERNAME").IsOptional().HasMaxLength(2000);
            Property(x => x.Remark).HasColumnName("REMARK").IsOptional().HasMaxLength(4000);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.CreateTime).HasColumnName("CREATETIME").IsOptional();
            Property(x => x.CreateUserName).HasColumnName("CREATEUSERNAME").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
        }
    }

    // S_I_NewsImage
    internal partial class S_I_NewsImageConfiguration : EntityTypeConfiguration<S_I_NewsImage>
    {
        public S_I_NewsImageConfiguration()
        {
			ToTable("S_I_NEWSIMAGE");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.GroupID).HasColumnName("GROUPID").IsOptional().HasMaxLength(50);
            Property(x => x.PictureName).HasColumnName("PICTURENAME").IsOptional().HasMaxLength(500);
            Property(x => x.PictureEntire).HasColumnName("PICTUREENTIRE").IsOptional().HasMaxLength(2147483647);
            Property(x => x.PictureThumb).HasColumnName("PICTURETHUMB").IsOptional().HasMaxLength(2147483647);
            Property(x => x.Src).HasColumnName("SRC").IsOptional().HasMaxLength(500);
            Property(x => x.LinkUrl).HasColumnName("LINKURL").IsOptional().HasMaxLength(500);
            Property(x => x.Description).HasColumnName("DESCRIPTION").IsOptional().HasMaxLength(500);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.CreateTime).HasColumnName("CREATETIME").IsOptional();
            Property(x => x.CreateUserName).HasColumnName("CREATEUSERNAME").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
        }
    }

    // S_I_NewsImageGroup
    internal partial class S_I_NewsImageGroupConfiguration : EntityTypeConfiguration<S_I_NewsImageGroup>
    {
        public S_I_NewsImageGroupConfiguration()
        {
			ToTable("S_I_NEWSIMAGEGROUP");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.Title).HasColumnName("TITLE").IsOptional().HasMaxLength(200);
            Property(x => x.Remark).HasColumnName("REMARK").IsOptional().HasMaxLength(2000);
            Property(x => x.DeptDoorId).HasColumnName("DEPTDOORID").IsOptional().HasMaxLength(200);
            Property(x => x.DeptDoorName).HasColumnName("DEPTDOORNAME").IsOptional().HasMaxLength(200);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.CreateTime).HasColumnName("CREATETIME").IsOptional();
            Property(x => x.CreateUserName).HasColumnName("CREATEUSERNAME").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
        }
    }

    // S_I_PublicInformation
    internal partial class S_I_PublicInformationConfiguration : EntityTypeConfiguration<S_I_PublicInformation>
    {
        public S_I_PublicInformationConfiguration()
        {
			ToTable("S_I_PUBLICINFORMATION");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.CatalogId).HasColumnName("CATALOGID").IsOptional().HasMaxLength(50);
            Property(x => x.Title).HasColumnName("TITLE").IsOptional().HasMaxLength(500);
            Property(x => x.Content).HasColumnName("CONTENT").IsOptional();
            Property(x => x.ContentText).HasColumnName("CONTENTTEXT").IsOptional();
            Property(x => x.Attachments).HasColumnName("ATTACHMENTS").IsOptional().HasMaxLength(2000);
            Property(x => x.ReceiveDeptId).HasColumnName("RECEIVEDEPTID").IsOptional().HasMaxLength(2000);
            Property(x => x.ReceiveDeptName).HasColumnName("RECEIVEDEPTNAME").IsOptional().HasMaxLength(2000);
            Property(x => x.ReceiveUserId).HasColumnName("RECEIVEUSERID").IsOptional().HasMaxLength(2000);
            Property(x => x.ReceiveUserName).HasColumnName("RECEIVEUSERNAME").IsOptional().HasMaxLength(2000);
            Property(x => x.DeptDoorId).HasColumnName("DEPTDOORID").IsOptional().HasMaxLength(200);
            Property(x => x.DeptDoorName).HasColumnName("DEPTDOORNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ExpiresTime).HasColumnName("EXPIRESTIME").IsOptional();
            Property(x => x.ReadCount).HasColumnName("READCOUNT").IsOptional();
            Property(x => x.Important).HasColumnName("IMPORTANT").IsOptional().HasMaxLength(50);
            Property(x => x.Urgency).HasColumnName("URGENCY").IsOptional().HasMaxLength(50);
            Property(x => x.IsTop).HasColumnName("ISTOP").IsOptional().HasMaxLength(50);
            Property(x => x.CreateTime).HasColumnName("CREATETIME").IsOptional();
            Property(x => x.CreateUserName).HasColumnName("CREATEUSERNAME").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
        }
    }

    // S_I_PublicInformCatalog
    internal partial class S_I_PublicInformCatalogConfiguration : EntityTypeConfiguration<S_I_PublicInformCatalog>
    {
        public S_I_PublicInformCatalogConfiguration()
        {
			ToTable("S_I_PUBLICINFORMCATALOG");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.CatalogName).HasColumnName("CATALOGNAME").IsOptional().HasMaxLength(50);
            Property(x => x.CatalogKey).HasColumnName("CATALOGKEY").IsOptional().HasMaxLength(50);
            Property(x => x.IsOnHomePage).HasColumnName("ISONHOMEPAGE").IsOptional().HasMaxLength(1);
            Property(x => x.InHomePageNum).HasColumnName("INHOMEPAGENUM").IsOptional();
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.CreateTime).HasColumnName("CREATETIME").IsOptional();
            Property(x => x.CreateUserName).HasColumnName("CREATEUSERNAME").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
        }
    }

    // S_M_Category
    internal partial class S_M_CategoryConfiguration : EntityTypeConfiguration<S_M_Category>
    {
        public S_M_CategoryConfiguration()
        {
			ToTable("S_M_CATEGORY");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.ParentID).HasColumnName("PARENTID").IsOptional().HasMaxLength(50);
            Property(x => x.FullID).HasColumnName("FULLID").IsOptional().HasMaxLength(500);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(50);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.Description).HasColumnName("DESCRIPTION").IsOptional().HasMaxLength(500);
            Property(x => x.CategoryCode).HasColumnName("CATEGORYCODE").IsOptional().HasMaxLength(50);
            Property(x => x.IconClass).HasColumnName("ICONCLASS").IsOptional().HasMaxLength(50);
        }
    }

    // S_M_ConfigManage
    internal partial class S_M_ConfigManageConfiguration : EntityTypeConfiguration<S_M_ConfigManage>
    {
        public S_M_ConfigManageConfiguration()
        {
			ToTable("S_M_CONFIGMANAGE");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(50);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(50);
            Property(x => x.Type).HasColumnName("TYPE").IsOptional().HasMaxLength(50);
            Property(x => x.DbServerAddr).HasColumnName("DBSERVERADDR").IsOptional().HasMaxLength(50);
            Property(x => x.DbName).HasColumnName("DBNAME").IsOptional().HasMaxLength(50);
            Property(x => x.DbLoginName).HasColumnName("DBLOGINNAME").IsOptional().HasMaxLength(50);
            Property(x => x.DbPassword).HasColumnName("DBPASSWORD").IsOptional().HasMaxLength(50);
            Property(x => x.CreateTime).HasColumnName("CREATETIME").IsOptional();
            Property(x => x.SyncTime).HasColumnName("SYNCTIME").IsOptional();
            Property(x => x.CategoryID).HasColumnName("CATEGORYID").IsOptional().HasMaxLength(50);
            Property(x => x.ConnName).HasColumnName("CONNNAME").IsOptional().HasMaxLength(50);
        }
    }

    // S_M_EnumDef
    internal partial class S_M_EnumDefConfiguration : EntityTypeConfiguration<S_M_EnumDef>
    {
        public S_M_EnumDefConfiguration()
        {
			ToTable("S_M_ENUMDEF");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(50);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(50);
            Property(x => x.Type).HasColumnName("TYPE").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.Description).HasColumnName("DESCRIPTION").IsOptional().HasMaxLength(500);
            Property(x => x.ConnName).HasColumnName("CONNNAME").IsOptional().HasMaxLength(50);
            Property(x => x.Sql).HasColumnName("SQL").IsOptional().HasMaxLength(500);
            Property(x => x.Orderby).HasColumnName("ORDERBY").IsOptional().HasMaxLength(200);
            Property(x => x.CategoryID).HasColumnName("CATEGORYID").IsOptional().HasMaxLength(50);
        }
    }

    // S_M_EnumItem
    internal partial class S_M_EnumItemConfiguration : EntityTypeConfiguration<S_M_EnumItem>
    {
        public S_M_EnumItemConfiguration()
        {
			ToTable("S_M_ENUMITEM");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.EnumDefID).HasColumnName("ENUMDEFID").IsOptional().HasMaxLength(50);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(50);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.Description).HasColumnName("DESCRIPTION").IsOptional().HasMaxLength(500);
            Property(x => x.SubEnumDefCode).HasColumnName("SUBENUMDEFCODE").IsOptional().HasMaxLength(50);
            Property(x => x.Category).HasColumnName("CATEGORY").IsOptional().HasMaxLength(50);
            Property(x => x.SubCategory).HasColumnName("SUBCATEGORY").IsOptional().HasMaxLength(50);

            // Foreign keys
            HasOptional(a => a.S_M_EnumDef).WithMany(b => b.S_M_EnumItem).HasForeignKey(c => c.EnumDefID); // FK_EnumItem_EnumDef
        }
    }

    // S_M_Field
    internal partial class S_M_FieldConfiguration : EntityTypeConfiguration<S_M_Field>
    {
        public S_M_FieldConfiguration()
        {
			ToTable("S_M_FIELD");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.TableID).HasColumnName("TABLEID").IsOptional().HasMaxLength(50);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(200);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(200);
            Property(x => x.Type).HasColumnName("TYPE").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsRequired();
            Property(x => x.EnumKey).HasColumnName("ENUMKEY").IsOptional();
            Property(x => x.Description).HasColumnName("DESCRIPTION").IsOptional().HasMaxLength(500);

            // Foreign keys
            HasOptional(a => a.S_M_Table).WithMany(b => b.S_M_Field).HasForeignKey(c => c.TableID); // FK_S_M_Field_S_M_Table
        }
    }

    // S_M_Table
    internal partial class S_M_TableConfiguration : EntityTypeConfiguration<S_M_Table>
    {
        public S_M_TableConfiguration()
        {
			ToTable("S_M_TABLE");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(200);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(200);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.ConnName).HasColumnName("CONNNAME").IsOptional().HasMaxLength(50);
            Property(x => x.Description).HasColumnName("DESCRIPTION").IsOptional().HasMaxLength(500);
        }
    }

    // S_P_DoorBaseTemplate
    internal partial class S_P_DoorBaseTemplateConfiguration : EntityTypeConfiguration<S_P_DoorBaseTemplate>
    {
        public S_P_DoorBaseTemplateConfiguration()
        {
			ToTable("S_P_DOORBASETEMPLATE");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.BaseType).HasColumnName("BASETYPE").IsOptional().HasMaxLength(50);
            Property(x => x.IsDefault).HasColumnName("ISDEFAULT").IsOptional().HasMaxLength(1);
            Property(x => x.TemplateColWidth).HasColumnName("TEMPLATECOLWIDTH").IsOptional().HasMaxLength(100);
            Property(x => x.TemplateString).HasColumnName("TEMPLATESTRING").IsOptional().HasMaxLength(500);
            Property(x => x.Title).HasColumnName("TITLE").IsOptional().HasMaxLength(200);
            Property(x => x.IsEdit).HasColumnName("ISEDIT").IsOptional().HasMaxLength(50);
        }
    }

    // S_P_DoorBlock
    internal partial class S_P_DoorBlockConfiguration : EntityTypeConfiguration<S_P_DoorBlock>
    {
        public S_P_DoorBlockConfiguration()
        {
			ToTable("S_P_DOORBLOCK");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.BlockName).HasColumnName("BLOCKNAME").IsOptional().HasMaxLength(50);
            Property(x => x.BlockKey).HasColumnName("BLOCKKEY").IsOptional().HasMaxLength(50);
            Property(x => x.BlockTitle).HasColumnName("BLOCKTITLE").IsOptional().HasMaxLength(50);
            Property(x => x.BlockType).HasColumnName("BLOCKTYPE").IsOptional().HasMaxLength(50);
            Property(x => x.BlockImage).HasColumnName("BLOCKIMAGE").IsOptional().HasMaxLength(100);
            Property(x => x.Remark).HasColumnName("REMARK").IsOptional().HasMaxLength(400);
            Property(x => x.HeadHtml).HasColumnName("HEADHTML").IsOptional().HasMaxLength(1073741823);
            Property(x => x.ColorValue).HasColumnName("COLORVALUE").IsOptional().HasMaxLength(10);
            Property(x => x.Color).HasColumnName("COLOR").IsOptional().HasMaxLength(50);
            Property(x => x.RepeatItemCount).HasColumnName("REPEATITEMCOUNT").IsOptional();
            Property(x => x.RepeatItemLength).HasColumnName("REPEATITEMLENGTH").IsOptional();
            Property(x => x.RepeatDataDataSql).HasColumnName("REPEATDATADATASQL").IsOptional().HasMaxLength(1073741823);
            Property(x => x.RepeatItemTemplate).HasColumnName("REPEATITEMTEMPLATE").IsOptional().HasMaxLength(1073741823);
            Property(x => x.FootHtml).HasColumnName("FOOTHTML").IsOptional().HasMaxLength(1073741823);
            Property(x => x.DelayLoadSecond).HasColumnName("DELAYLOADSECOND").IsOptional();
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.RelateScript).HasColumnName("RELATESCRIPT").IsOptional().HasMaxLength(2000);
            Property(x => x.IsHidden).HasColumnName("ISHIDDEN").IsOptional().HasMaxLength(50);
            Property(x => x.TemplateId).HasColumnName("TEMPLATEID").IsOptional().HasMaxLength(50);
            Property(x => x.AllowUserIDs).HasColumnName("ALLOWUSERIDS").IsOptional().HasMaxLength(1073741823);
            Property(x => x.AllowUserNames).HasColumnName("ALLOWUSERNAMES").IsOptional().HasMaxLength(1073741823);
            Property(x => x.AllowTypes).HasColumnName("ALLOWTYPES").IsOptional().HasMaxLength(1073741823);
            Property(x => x.IsEdit).HasColumnName("ISEDIT").IsOptional().HasMaxLength(50);
        }
    }

    // S_P_DoorTemplate
    internal partial class S_P_DoorTemplateConfiguration : EntityTypeConfiguration<S_P_DoorTemplate>
    {
        public S_P_DoorTemplateConfiguration()
        {
			ToTable("S_P_DOORTEMPLATE");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.Type).HasColumnName("TYPE").IsOptional().HasMaxLength(50);
            Property(x => x.BaseType).HasColumnName("BASETYPE").IsOptional().HasMaxLength(50);
            Property(x => x.UserID).HasColumnName("USERID").IsOptional().HasMaxLength(50);
            Property(x => x.UserName).HasColumnName("USERNAME").IsOptional().HasMaxLength(100);
            Property(x => x.IsDefault).HasColumnName("ISDEFAULT").IsOptional().HasMaxLength(1);
            Property(x => x.TemplateColWidth).HasColumnName("TEMPLATECOLWIDTH").IsOptional().HasMaxLength(100);
            Property(x => x.TemplateString).HasColumnName("TEMPLATESTRING").IsOptional().HasMaxLength(500);
            Property(x => x.BaseTemplateId).HasColumnName("BASETEMPLATEID").IsOptional().HasMaxLength(50);
        }
    }

    // S_R_DataSet
    internal partial class S_R_DataSetConfiguration : EntityTypeConfiguration<S_R_DataSet>
    {
        public S_R_DataSetConfiguration()
        {
			ToTable("S_R_DATASET");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.DefineID).HasColumnName("DEFINEID").IsOptional().HasMaxLength(50);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(50);
            Property(x => x.ConnName).HasColumnName("CONNNAME").IsOptional().HasMaxLength(50);
            Property(x => x.TableNames).HasColumnName("TABLENAMES").IsOptional().HasMaxLength(200);
            Property(x => x.Sql).HasColumnName("SQL").IsOptional();

            // Foreign keys
            HasOptional(a => a.S_R_Define).WithMany(b => b.S_R_DataSet).HasForeignKey(c => c.DefineID); // FK_S_R_DataSet_S_R_Define
        }
    }

    // S_R_Define
    internal partial class S_R_DefineConfiguration : EntityTypeConfiguration<S_R_Define>
    {
        public S_R_DefineConfiguration()
        {
			ToTable("S_R_DEFINE");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(50);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUserName).HasColumnName("MODIFYUSERNAME").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyTime).HasColumnName("MODIFYTIME").IsOptional();
            Property(x => x.CategoryID).HasColumnName("CATEGORYID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUserName).HasColumnName("CREATEUSERNAME").IsOptional().HasMaxLength(50);
            Property(x => x.CreateTime).HasColumnName("CREATETIME").IsOptional();
            Property(x => x.ConnName).HasColumnName("CONNNAME").IsOptional().HasMaxLength(50);
        }
    }

    // S_R_Field
    internal partial class S_R_FieldConfiguration : EntityTypeConfiguration<S_R_Field>
    {
        public S_R_FieldConfiguration()
        {
			ToTable("S_R_FIELD");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.DataSetID).HasColumnName("DATASETID").IsOptional().HasMaxLength(50);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(50);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(50);
            Property(x => x.Type).HasColumnName("TYPE").IsOptional().HasMaxLength(50);
            Property(x => x.EnumKey).HasColumnName("ENUMKEY").IsOptional();

            // Foreign keys
            HasOptional(a => a.S_R_DataSet).WithMany(b => b.S_R_Field).HasForeignKey(c => c.DataSetID); // FK_S_R_Field_S_R_DataSet
        }
    }

    // S_RC_RuleCode
    internal partial class S_RC_RuleCodeConfiguration : EntityTypeConfiguration<S_RC_RuleCode>
    {
        public S_RC_RuleCodeConfiguration()
        {
			ToTable("S_RC_RULECODE");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(72);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(100);
            Property(x => x.RuleName).HasColumnName("RULENAME").IsOptional().HasMaxLength(400);
            Property(x => x.Prefix).HasColumnName("PREFIX").IsOptional().HasMaxLength(100);
            Property(x => x.PostFix).HasColumnName("POSTFIX").IsOptional().HasMaxLength(100);
            Property(x => x.Seperative).HasColumnName("SEPERATIVE").IsOptional().HasMaxLength(100);
            Property(x => x.Digit).HasColumnName("DIGIT").IsOptional();
            Property(x => x.StartNumber).HasColumnName("STARTNUMBER").IsOptional();
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(100);
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsOptional().HasMaxLength(100);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
        }
    }

    // S_RC_RuleCodeData
    internal partial class S_RC_RuleCodeDataConfiguration : EntityTypeConfiguration<S_RC_RuleCodeData>
    {
        public S_RC_RuleCodeDataConfiguration()
        {
			ToTable("S_RC_RULECODEDATA");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(72);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(100);
            Property(x => x.Year).HasColumnName("YEAR").IsOptional();
            Property(x => x.AutoNumber).HasColumnName("AUTONUMBER").IsOptional();
        }
    }

    // S_S_Alarm
    internal partial class S_S_AlarmConfiguration : EntityTypeConfiguration<S_S_Alarm>
    {
        public S_S_AlarmConfiguration()
        {
			ToTable("S_S_ALARM");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.Important).HasColumnName("IMPORTANT").IsOptional().HasMaxLength(50);
            Property(x => x.Urgency).HasColumnName("URGENCY").IsOptional().HasMaxLength(50);
            Property(x => x.AlarmType).HasColumnName("ALARMTYPE").IsOptional().HasMaxLength(100);
            Property(x => x.AlarmTitle).HasColumnName("ALARMTITLE").IsOptional().HasMaxLength(200);
            Property(x => x.AlarmContent).HasColumnName("ALARMCONTENT").IsOptional();
            Property(x => x.AlarmUrl).HasColumnName("ALARMURL").IsOptional().HasMaxLength(200);
            Property(x => x.OwnerName).HasColumnName("OWNERNAME").IsOptional().HasMaxLength(50);
            Property(x => x.OwnerID).HasColumnName("OWNERID").IsOptional().HasMaxLength(50);
            Property(x => x.SendTime).HasColumnName("SENDTIME").IsOptional();
            Property(x => x.DeadlineTime).HasColumnName("DEADLINETIME").IsOptional();
            Property(x => x.SenderName).HasColumnName("SENDERNAME").IsOptional().HasMaxLength(50);
            Property(x => x.SenderID).HasColumnName("SENDERID").IsOptional().HasMaxLength(50);
            Property(x => x.IsDelete).HasColumnName("ISDELETE").IsOptional().HasMaxLength(50);
        }
    }

    // S_S_MsgBody
    internal partial class S_S_MsgBodyConfiguration : EntityTypeConfiguration<S_S_MsgBody>
    {
        public S_S_MsgBodyConfiguration()
        {
			ToTable("S_S_MSGBODY");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.ParentID).HasColumnName("PARENTID").IsOptional().HasMaxLength(50);
            Property(x => x.Type).HasColumnName("TYPE").IsOptional().HasMaxLength(50);
            Property(x => x.Title).HasColumnName("TITLE").IsOptional().HasMaxLength(500);
            Property(x => x.Content).HasColumnName("CONTENT").IsOptional();
            Property(x => x.ContentText).HasColumnName("CONTENTTEXT").IsOptional();
            Property(x => x.AttachFileIDs).HasColumnName("ATTACHFILEIDS").IsOptional().HasMaxLength(2000);
            Property(x => x.LinkUrl).HasColumnName("LINKURL").IsOptional().HasMaxLength(500);
            Property(x => x.IsSystemMsg).HasColumnName("ISSYSTEMMSG").IsOptional().HasMaxLength(1);
            Property(x => x.SendTime).HasColumnName("SENDTIME").IsOptional();
            Property(x => x.SenderID).HasColumnName("SENDERID").IsOptional().HasMaxLength(50);
            Property(x => x.SenderName).HasColumnName("SENDERNAME").IsOptional().HasMaxLength(50);
            Property(x => x.ReceiverIDs).HasColumnName("RECEIVERIDS").IsOptional().HasMaxLength(2000);
            Property(x => x.ReceiverNames).HasColumnName("RECEIVERNAMES").IsOptional().HasMaxLength(2000);
            Property(x => x.ReceiverDeptIDs).HasColumnName("RECEIVERDEPTIDS").IsOptional().HasMaxLength(4000);
            Property(x => x.ReceiverDeptNames).HasColumnName("RECEIVERDEPTNAMES").IsOptional().HasMaxLength(4000);
            Property(x => x.IsDeleted).HasColumnName("ISDELETED").IsRequired().HasMaxLength(1);
            Property(x => x.DeleteTime).HasColumnName("DELETETIME").IsOptional();
            Property(x => x.IsReadReceipt).HasColumnName("ISREADRECEIPT").IsRequired().HasMaxLength(1);
            Property(x => x.Importance).HasColumnName("IMPORTANCE").IsRequired().HasMaxLength(1);
        }
    }

    // S_S_MsgReceiver
    internal partial class S_S_MsgReceiverConfiguration : EntityTypeConfiguration<S_S_MsgReceiver>
    {
        public S_S_MsgReceiverConfiguration()
        {
			ToTable("S_S_MSGRECEIVER");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.MsgBodyID).HasColumnName("MSGBODYID").IsOptional().HasMaxLength(50);
            Property(x => x.UserID).HasColumnName("USERID").IsOptional().HasMaxLength(50);
            Property(x => x.UserName).HasColumnName("USERNAME").IsOptional().HasMaxLength(50);
            Property(x => x.FirstViewTime).HasColumnName("FIRSTVIEWTIME").IsOptional();
            Property(x => x.ReplyTime).HasColumnName("REPLYTIME").IsOptional();
            Property(x => x.IsDeleted).HasColumnName("ISDELETED").IsOptional().HasMaxLength(1);
            Property(x => x.DeleteTime).HasColumnName("DELETETIME").IsOptional();

            // Foreign keys
            HasOptional(a => a.S_S_MsgBody).WithMany(b => b.S_S_MsgReceiver).HasForeignKey(c => c.MsgBodyID); // FK_S_S_MsgReceiver_S_S_MsgBody
        }
    }

    // S_UI_Form
    internal partial class S_UI_FormConfiguration : EntityTypeConfiguration<S_UI_Form>
    {
        public S_UI_FormConfiguration()
        {
			ToTable("S_UI_FORM");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(50);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(50);
            Property(x => x.Category).HasColumnName("CATEGORY").IsOptional().HasMaxLength(50);
            Property(x => x.ConnName).HasColumnName("CONNNAME").IsOptional().HasMaxLength(50);
            Property(x => x.TableName).HasColumnName("TABLENAME").IsOptional().HasMaxLength(50);
            Property(x => x.Description).HasColumnName("DESCRIPTION").IsOptional().HasMaxLength(500);
            Property(x => x.Script).HasColumnName("SCRIPT").IsOptional().HasMaxLength(1073741823);
            Property(x => x.ScriptText).HasColumnName("SCRIPTTEXT").IsOptional().HasMaxLength(1073741823);
            Property(x => x.FlowLogic).HasColumnName("FLOWLOGIC").IsOptional().HasMaxLength(1073741823);
            Property(x => x.HiddenFields).HasColumnName("HIDDENFIELDS").IsOptional().HasMaxLength(500);
            Property(x => x.Layout).HasColumnName("LAYOUT").IsOptional().HasMaxLength(1073741823);
            Property(x => x.Items).HasColumnName("ITEMS").IsOptional().HasMaxLength(1073741823);
            Property(x => x.Setttings).HasColumnName("SETTTINGS").IsOptional().HasMaxLength(2000);
            Property(x => x.SerialNumberSettings).HasColumnName("SERIALNUMBERSETTINGS").IsOptional().HasMaxLength(2000);
            Property(x => x.DefaultValueSettings).HasColumnName("DEFAULTVALUESETTINGS").IsOptional().HasMaxLength(1073741823);
            Property(x => x.CategoryID).HasColumnName("CATEGORYID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUserName).HasColumnName("MODIFYUSERNAME").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyTime).HasColumnName("MODIFYTIME").IsOptional();
            Property(x => x.ReleaseTime).HasColumnName("RELEASETIME").IsOptional();
            Property(x => x.ReleasedData).HasColumnName("RELEASEDDATA").IsOptional().HasMaxLength(1073741823);
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUserName).HasColumnName("CREATEUSERNAME").IsOptional().HasMaxLength(50);
            Property(x => x.CreateTime).HasColumnName("CREATETIME").IsOptional();
        }
    }

    // S_UI_List
    internal partial class S_UI_ListConfiguration : EntityTypeConfiguration<S_UI_List>
    {
        public S_UI_ListConfiguration()
        {
			ToTable("S_UI_LIST");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(50);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(50);
            Property(x => x.ConnName).HasColumnName("CONNNAME").IsOptional().HasMaxLength(50);
            Property(x => x.SQL).HasColumnName("SQL").IsOptional();
            Property(x => x.TableNames).HasColumnName("TABLENAMES").IsOptional().HasMaxLength(500);
            Property(x => x.Script).HasColumnName("SCRIPT").IsOptional().HasMaxLength(1073741823);
            Property(x => x.ScriptText).HasColumnName("SCRIPTTEXT").IsOptional().HasMaxLength(1073741823);
            Property(x => x.HasRowNumber).HasColumnName("HASROWNUMBER").IsOptional().HasMaxLength(50);
            Property(x => x.LayoutGrid).HasColumnName("LAYOUTGRID").IsOptional().HasMaxLength(1073741823);
            Property(x => x.LayoutField).HasColumnName("LAYOUTFIELD").IsOptional().HasMaxLength(1073741823);
            Property(x => x.LayoutSearch).HasColumnName("LAYOUTSEARCH").IsOptional().HasMaxLength(1073741823);
            Property(x => x.LayoutButton).HasColumnName("LAYOUTBUTTON").IsOptional().HasMaxLength(1073741823);
            Property(x => x.Settings).HasColumnName("SETTINGS").IsOptional().HasMaxLength(2000);
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUserName).HasColumnName("MODIFYUSERNAME").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyTime).HasColumnName("MODIFYTIME").IsOptional();
            Property(x => x.CategoryID).HasColumnName("CATEGORYID").IsOptional().HasMaxLength(50);
            Property(x => x.Released).HasColumnName("RELEASED").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUserName).HasColumnName("CREATEUSERNAME").IsOptional().HasMaxLength(50);
            Property(x => x.CreateTime).HasColumnName("CREATETIME").IsOptional();
            Property(x => x.DenyDeleteFlow).HasColumnName("DENYDELETEFLOW").IsOptional().HasMaxLength(50);
        }
    }

    // S_UI_ModifyLog
    internal partial class S_UI_ModifyLogConfiguration : EntityTypeConfiguration<S_UI_ModifyLog>
    {
        public S_UI_ModifyLogConfiguration()
        {
			ToTable("S_UI_MODIFYLOG");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.TableName).HasColumnName("TABLENAME").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyTime).HasColumnName("MODIFYTIME").IsOptional();
            Property(x => x.ModifyType).HasColumnName("MODIFYTYPE").IsOptional().HasMaxLength(50);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(500);
        }
    }

    // S_UI_Selector
    internal partial class S_UI_SelectorConfiguration : EntityTypeConfiguration<S_UI_Selector>
    {
        public S_UI_SelectorConfiguration()
        {
			ToTable("S_UI_SELECTOR");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(50);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(50);
            Property(x => x.URLSingle).HasColumnName("URLSINGLE").IsOptional().HasMaxLength(200);
            Property(x => x.URLMulti).HasColumnName("URLMULTI").IsOptional().HasMaxLength(200);
            Property(x => x.Width).HasColumnName("WIDTH").IsOptional().HasMaxLength(50);
            Property(x => x.Height).HasColumnName("HEIGHT").IsOptional().HasMaxLength(50);
            Property(x => x.Title).HasColumnName("TITLE").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUserName).HasColumnName("MODIFYUSERNAME").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyTime).HasColumnName("MODIFYTIME").IsOptional();
            Property(x => x.CategoryID).HasColumnName("CATEGORYID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUserName).HasColumnName("CREATEUSERNAME").IsOptional().HasMaxLength(50);
            Property(x => x.CreateTime).HasColumnName("CREATETIME").IsOptional();
        }
    }

    // S_UI_SerialNumber
    internal partial class S_UI_SerialNumberConfiguration : EntityTypeConfiguration<S_UI_SerialNumber>
    {
        public S_UI_SerialNumberConfiguration()
        {
			ToTable("S_UI_SERIALNUMBER");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(50);
            Property(x => x.YearCode).HasColumnName("YEARCODE").IsOptional().HasMaxLength(50);
            Property(x => x.MonthCode).HasColumnName("MONTHCODE").IsOptional().HasMaxLength(50);
            Property(x => x.DayCode).HasColumnName("DAYCODE").IsOptional().HasMaxLength(50);
            Property(x => x.CategoryCode).HasColumnName("CATEGORYCODE").IsOptional().HasMaxLength(50);
            Property(x => x.SubCategoryCode).HasColumnName("SUBCATEGORYCODE").IsOptional().HasMaxLength(50);
            Property(x => x.OrderNumCode).HasColumnName("ORDERNUMCODE").IsOptional().HasMaxLength(50);
            Property(x => x.PrjCode).HasColumnName("PRJCODE").IsOptional().HasMaxLength(50);
            Property(x => x.OrgCode).HasColumnName("ORGCODE").IsOptional().HasMaxLength(50);
            Property(x => x.UserCode).HasColumnName("USERCODE").IsOptional().HasMaxLength(50);
            Property(x => x.Number).HasColumnName("NUMBER").IsOptional();
        }
    }

    // S_UI_Word
    internal partial class S_UI_WordConfiguration : EntityTypeConfiguration<S_UI_Word>
    {
        public S_UI_WordConfiguration()
        {
			ToTable("S_UI_WORD");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(50);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(50);
            Property(x => x.ConnName).HasColumnName("CONNNAME").IsOptional().HasMaxLength(50);
            Property(x => x.TableNames).HasColumnName("TABLENAMES").IsOptional().HasMaxLength(500);
            Property(x => x.SQL).HasColumnName("SQL").IsOptional();
            Property(x => x.Description).HasColumnName("DESCRIPTION").IsOptional().HasMaxLength(500);
            Property(x => x.Items).HasColumnName("ITEMS").IsOptional().HasMaxLength(1073741823);
            Property(x => x.CategoryID).HasColumnName("CATEGORYID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUserName).HasColumnName("MODIFYUSERNAME").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyTime).HasColumnName("MODIFYTIME").IsOptional();
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUserName).HasColumnName("CREATEUSERNAME").IsOptional().HasMaxLength(50);
            Property(x => x.CreateTime).HasColumnName("CREATETIME").IsOptional();
        }
    }

    // S_UIH5_Form
    internal partial class S_UIH5_FormConfiguration : EntityTypeConfiguration<S_UIH5_Form>
    {
        public S_UIH5_FormConfiguration()
        {
			ToTable("S_UIH5_FORM");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(50);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(50);
            Property(x => x.NameEN).HasColumnName("NAMEEN").IsOptional().HasMaxLength(50);
            Property(x => x.ListName).HasColumnName("LISTNAME").IsOptional().HasMaxLength(50);
            Property(x => x.ListNameEN).HasColumnName("LISTNAMEEN").IsOptional().HasMaxLength(50);
            Property(x => x.ConnName).HasColumnName("CONNNAME").IsOptional().HasMaxLength(50);
            Property(x => x.TableName).HasColumnName("TABLENAME").IsOptional().HasMaxLength(50);
            Property(x => x.ListSql).HasColumnName("LISTSQL").IsOptional().HasMaxLength(2000);
            Property(x => x.DefaultValueSettings).HasColumnName("DEFAULTVALUESETTINGS").IsOptional().HasMaxLength(1073741823);
            Property(x => x.FlowLogic).HasColumnName("FLOWLOGIC").IsOptional().HasMaxLength(1073741823);
            Property(x => x.AngularScript).HasColumnName("ANGULARSCRIPT").IsOptional().HasMaxLength(1073741823);
            Property(x => x.ListSqlOrderBy).HasColumnName("LISTSQLORDERBY").IsOptional().HasMaxLength(50);
            Property(x => x.Description).HasColumnName("DESCRIPTION").IsOptional().HasMaxLength(50);
            Property(x => x.Items).HasColumnName("ITEMS").IsOptional().HasMaxLength(1073741823);
            Property(x => x.CategoryID).HasColumnName("CATEGORYID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUserName).HasColumnName("MODIFYUSERNAME").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyTime).HasColumnName("MODIFYTIME").IsOptional();
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUserName).HasColumnName("CREATEUSERNAME").IsOptional().HasMaxLength(50);
            Property(x => x.CreateTime).HasColumnName("CREATETIME").IsOptional();
        }
    }

}

