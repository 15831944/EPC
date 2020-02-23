

// This file was automatically generated.
// Do not make changes directly to this file - edit the template instead.
// 
// The following connection settings were used to generate this file
// 
//     Connection String Name: "DocConst"
//     Connection String:      "Data Source=.;Initial Catalog=EPM_DocConst;User ID=sa;PWD=123.zxc;"

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

namespace DocSystem.Logic.Domain
{
    // ************************************************************************
    // Database context
    public partial class DocConstEntities : Formula.FormulaDbContext
    {
        public IDbSet<S_A_AuthoritySetting> S_A_AuthoritySetting { get; set; } // S_A_AuthoritySetting
        public IDbSet<S_A_InventoryLedger> S_A_InventoryLedger { get; set; } // S_A_InventoryLedger
        public IDbSet<S_A_LoseReplenish> S_A_LoseReplenish { get; set; } // S_A_LoseReplenish
        public IDbSet<S_A_StorageRoom> S_A_StorageRoom { get; set; } // S_A_StorageRoom
        public IDbSet<S_A_StorageRoom_CabinetDetail> S_A_StorageRoom_CabinetDetail { get; set; } // S_A_StorageRoom_CabinetDetail
        public IDbSet<S_ArchiveCache> S_ArchiveCache { get; set; } // S_ArchiveCache
        public IDbSet<S_ArchiveCacheCatalog> S_ArchiveCacheCatalog { get; set; } // S_ArchiveCacheCatalog
        public IDbSet<S_BorrowDetail> S_BorrowDetail { get; set; } // S_BorrowDetail
        public IDbSet<S_CarInfo> S_CarInfo { get; set; } // S_CarInfo
        public IDbSet<S_DocumentLog> S_DocumentLog { get; set; } // S_DocumentLog
        public IDbSet<S_DownloadDetail> S_DownloadDetail { get; set; } // S_DownloadDetail
        public IDbSet<S_I_ChangePeriodApply> S_I_ChangePeriodApply { get; set; } // S_I_ChangePeriodApply
        public IDbSet<S_I_ChangePeriodApply_DetailInfo> S_I_ChangePeriodApply_DetailInfo { get; set; } // S_I_ChangePeriodApply_DetailInfo
        public IDbSet<S_I_DestroyApply> S_I_DestroyApply { get; set; } // S_I_DestroyApply
        public IDbSet<S_I_DestroyApply_DetailInfo> S_I_DestroyApply_DetailInfo { get; set; } // S_I_DestroyApply_DetailInfo
        public IDbSet<S_I_IdentifyApply> S_I_IdentifyApply { get; set; } // S_I_IdentifyApply
        public IDbSet<S_I_IdentifyApply_DetailInfo> S_I_IdentifyApply_DetailInfo { get; set; } // S_I_IdentifyApply_DetailInfo
        public IDbSet<S_I_IdentifyInfo> S_I_IdentifyInfo { get; set; } // S_I_IdentifyInfo
        public IDbSet<S_I_IdentifyRule> S_I_IdentifyRule { get; set; } // S_I_IdentifyRule
        public IDbSet<S_R_PhysicalReorganize> S_R_PhysicalReorganize { get; set; } // S_R_PhysicalReorganize
        public IDbSet<S_R_PhysicalReorganize_FileDetail> S_R_PhysicalReorganize_FileDetail { get; set; } // S_R_PhysicalReorganize_FileDetail
        public IDbSet<S_R_PhysicalReorganize_NodeDetail> S_R_PhysicalReorganize_NodeDetail { get; set; } // S_R_PhysicalReorganize_NodeDetail
        public IDbSet<S_R_Reorganize> S_R_Reorganize { get; set; } // S_R_Reorganize
        public IDbSet<S_R_Reorganize_DocumentList> S_R_Reorganize_DocumentList { get; set; } // S_R_Reorganize_DocumentList
        public IDbSet<S_SU_DocumentRelieveSeal> S_SU_DocumentRelieveSeal { get; set; } // S_SU_DocumentRelieveSeal
        public IDbSet<S_SU_DocumentRelieveSeal_DetailList> S_SU_DocumentRelieveSeal_DetailList { get; set; } // S_SU_DocumentRelieveSeal_DetailList
        public IDbSet<S_SU_DocumentSealUp> S_SU_DocumentSealUp { get; set; } // S_SU_DocumentSealUp
        public IDbSet<S_SU_DocumentSealUp_DetailList> S_SU_DocumentSealUp_DetailList { get; set; } // S_SU_DocumentSealUp_DetailList
        public IDbSet<S_UserAdvanceQueryInfo> S_UserAdvanceQueryInfo { get; set; } // S_UserAdvanceQueryInfo
        public IDbSet<T_B_BorrowApply> T_B_BorrowApply { get; set; } // T_B_BorrowApply
        public IDbSet<T_B_BorrowApply_DetailInfo> T_B_BorrowApply_DetailInfo { get; set; } // T_B_BorrowApply_DetailInfo
        public IDbSet<T_Borrow> T_Borrow { get; set; } // T_Borrow
        public IDbSet<T_Borrow_FileInfo> T_Borrow_FileInfo { get; set; } // T_Borrow_FileInfo
        public IDbSet<T_D_DownloadApply> T_D_DownloadApply { get; set; } // T_D_DownloadApply
        public IDbSet<T_D_DownloadApply_DetailInfo> T_D_DownloadApply_DetailInfo { get; set; } // T_D_DownloadApply_DetailInfo
        public IDbSet<T_Download> T_Download { get; set; } // T_Download
        public IDbSet<T_Download_FileInfo> T_Download_FileInfo { get; set; } // T_Download_FileInfo
        public IDbSet<T_Lose> T_Lose { get; set; } // T_Lose
        public IDbSet<T_Lose_LostDetail> T_Lose_LostDetail { get; set; } // T_Lose_LostDetail
        public IDbSet<T_Replenish> T_Replenish { get; set; } // T_Replenish
        public IDbSet<T_Replenish_Detail> T_Replenish_Detail { get; set; } // T_Replenish_Detail

        static DocConstEntities()
        {
            Database.SetInitializer<DocConstEntities>(null);
        }

        public DocConstEntities()
            : base("Name=DocConst")
        {
        }

        public DocConstEntities(string connectionString) : base(connectionString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new S_A_AuthoritySettingConfiguration());
            modelBuilder.Configurations.Add(new S_A_InventoryLedgerConfiguration());
            modelBuilder.Configurations.Add(new S_A_LoseReplenishConfiguration());
            modelBuilder.Configurations.Add(new S_A_StorageRoomConfiguration());
            modelBuilder.Configurations.Add(new S_A_StorageRoom_CabinetDetailConfiguration());
            modelBuilder.Configurations.Add(new S_ArchiveCacheConfiguration());
            modelBuilder.Configurations.Add(new S_ArchiveCacheCatalogConfiguration());
            modelBuilder.Configurations.Add(new S_BorrowDetailConfiguration());
            modelBuilder.Configurations.Add(new S_CarInfoConfiguration());
            modelBuilder.Configurations.Add(new S_DocumentLogConfiguration());
            modelBuilder.Configurations.Add(new S_DownloadDetailConfiguration());
            modelBuilder.Configurations.Add(new S_I_ChangePeriodApplyConfiguration());
            modelBuilder.Configurations.Add(new S_I_ChangePeriodApply_DetailInfoConfiguration());
            modelBuilder.Configurations.Add(new S_I_DestroyApplyConfiguration());
            modelBuilder.Configurations.Add(new S_I_DestroyApply_DetailInfoConfiguration());
            modelBuilder.Configurations.Add(new S_I_IdentifyApplyConfiguration());
            modelBuilder.Configurations.Add(new S_I_IdentifyApply_DetailInfoConfiguration());
            modelBuilder.Configurations.Add(new S_I_IdentifyInfoConfiguration());
            modelBuilder.Configurations.Add(new S_I_IdentifyRuleConfiguration());
            modelBuilder.Configurations.Add(new S_R_PhysicalReorganizeConfiguration());
            modelBuilder.Configurations.Add(new S_R_PhysicalReorganize_FileDetailConfiguration());
            modelBuilder.Configurations.Add(new S_R_PhysicalReorganize_NodeDetailConfiguration());
            modelBuilder.Configurations.Add(new S_R_ReorganizeConfiguration());
            modelBuilder.Configurations.Add(new S_R_Reorganize_DocumentListConfiguration());
            modelBuilder.Configurations.Add(new S_SU_DocumentRelieveSealConfiguration());
            modelBuilder.Configurations.Add(new S_SU_DocumentRelieveSeal_DetailListConfiguration());
            modelBuilder.Configurations.Add(new S_SU_DocumentSealUpConfiguration());
            modelBuilder.Configurations.Add(new S_SU_DocumentSealUp_DetailListConfiguration());
            modelBuilder.Configurations.Add(new S_UserAdvanceQueryInfoConfiguration());
            modelBuilder.Configurations.Add(new T_B_BorrowApplyConfiguration());
            modelBuilder.Configurations.Add(new T_B_BorrowApply_DetailInfoConfiguration());
            modelBuilder.Configurations.Add(new T_BorrowConfiguration());
            modelBuilder.Configurations.Add(new T_Borrow_FileInfoConfiguration());
            modelBuilder.Configurations.Add(new T_D_DownloadApplyConfiguration());
            modelBuilder.Configurations.Add(new T_D_DownloadApply_DetailInfoConfiguration());
            modelBuilder.Configurations.Add(new T_DownloadConfiguration());
            modelBuilder.Configurations.Add(new T_Download_FileInfoConfiguration());
            modelBuilder.Configurations.Add(new T_LoseConfiguration());
            modelBuilder.Configurations.Add(new T_Lose_LostDetailConfiguration());
            modelBuilder.Configurations.Add(new T_ReplenishConfiguration());
            modelBuilder.Configurations.Add(new T_Replenish_DetailConfiguration());
        }
    }

    // ************************************************************************
    // POCO classes

	/// <summary></summary>	
	[Description("")]
    public partial class S_A_AuthoritySetting : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string SpaceID { get; set; } // SpaceID
		/// <summary></summary>	
		[Description("")]
        public string SecretLevel { get; set; } // SecretLevel
		/// <summary></summary>	
		[Description("")]
        public string AuthType { get; set; } // AuthType
		/// <summary></summary>	
		[Description("")]
        public string RoleType { get; set; } // RoleType
		/// <summary></summary>	
		[Description("")]
        public string RoleCode { get; set; } // RoleCode
    }

	/// <summary></summary>	
	[Description("")]
    public partial class S_A_InventoryLedger : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public int ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string SpaceID { get; set; } // SpaceID
		/// <summary></summary>	
		[Description("")]
        public string RelateID { get; set; } // RelateID
		/// <summary></summary>	
		[Description("")]
        public string RelateName { get; set; } // RelateName
		/// <summary></summary>	
		[Description("")]
        public string RelateType { get; set; } // RelateType
		/// <summary></summary>	
		[Description("")]
        public string CarItemName { get; set; } // CarItemName
		/// <summary>借阅ID</summary>	
		[Description("借阅ID")]
        public string RelateDetailInfoID { get; set; } // RelateDetailInfoID
		/// <summary>出入库类型</summary>	
		[Description("出入库类型")]
        public string Type { get; set; } // Type
		/// <summary>总数加减数量</summary>	
		[Description("总数加减数量")]
        public int TotalAmount { get; set; } // TotalAmount
		/// <summary>库存加减数量</summary>	
		[Description("库存加减数量")]
        public int InventoryAmount { get; set; } // InventoryAmount
		/// <summary></summary>	
		[Description("")]
        public DateTime CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get; set; } // CreateUserID
		/// <summary></summary>	
		[Description("")]
        public string CreateUserName { get; set; } // CreateUserName
		/// <summary></summary>	
		[Description("")]
        public string TargetUserID { get; set; } // TargetUserID
		/// <summary></summary>	
		[Description("")]
        public string TargetUserName { get; set; } // TargetUserName
		/// <summary></summary>	
		[Description("")]
        public string Detail { get; set; } // Detail
		/// <summary></summary>	
		[Description("")]
        public string CreateDept { get; set; } // CreateDept
    }

	/// <summary></summary>	
	[Description("")]
    public partial class S_A_LoseReplenish : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string RelateDocID { get; set; } // RelateDocID
		/// <summary></summary>	
		[Description("")]
        public string State { get; set; } // State
		/// <summary></summary>	
		[Description("")]
        public int? LoseCount { get; set; } // LoseCount
		/// <summary></summary>	
		[Description("")]
        public int? ReplenishCount { get; set; } // ReplenishCount
		/// <summary></summary>	
		[Description("")]
        public string ReplenishState { get; set; } // ReplenishState
		/// <summary></summary>	
		[Description("")]
        public string LosePeople { get; set; } // LosePeople
		/// <summary></summary>	
		[Description("")]
        public string LosePeopleName { get; set; } // LosePeopleName
		/// <summary></summary>	
		[Description("")]
        public string LoseDept { get; set; } // LoseDept
		/// <summary></summary>	
		[Description("")]
        public string LoseDeptName { get; set; } // LoseDeptName
		/// <summary></summary>	
		[Description("")]
        public string Name { get; set; } // Name
		/// <summary></summary>	
		[Description("")]
        public string ConfigID { get; set; } // ConfigID
		/// <summary></summary>	
		[Description("")]
        public string SpaceID { get; set; } // SpaceID
		/// <summary></summary>	
		[Description("")]
        public int? Quantity { get; set; } // Quantity
		/// <summary></summary>	
		[Description("")]
        public string StorageNum { get; set; } // StorageNum
		/// <summary></summary>	
		[Description("")]
        public DateTime? LoseDate { get; set; } // LoseDate
		/// <summary></summary>	
		[Description("")]
        public string LoseExplain { get; set; } // LoseExplain
		/// <summary></summary>	
		[Description("")]
        public string ReplenishDept { get; set; } // ReplenishDept
		/// <summary></summary>	
		[Description("")]
        public string ReplenishDeptName { get; set; } // ReplenishDeptName
		/// <summary></summary>	
		[Description("")]
        public string ReplenishPeople { get; set; } // ReplenishPeople
		/// <summary></summary>	
		[Description("")]
        public string ReplenishPeopleName { get; set; } // ReplenishPeopleName
		/// <summary></summary>	
		[Description("")]
        public DateTime? ReplenishDate { get; set; } // ReplenishDate
		/// <summary></summary>	
		[Description("")]
        public string ReplenishExplain { get; set; } // ReplenishExplain
		/// <summary></summary>	
		[Description("")]
        public string RelateDocType { get; set; } // RelateDocType
		/// <summary></summary>	
		[Description("")]
        public string DocumentCode { get; set; } // DocumentCode
    }

	/// <summary>档案库房管理</summary>	
	[Description("档案库房管理")]
    public partial class S_A_StorageRoom : Formula.BaseModel
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
		/// <summary>库房编号</summary>	
		[Description("库房编号")]
        public string Code { get; set; } // Code
		/// <summary>库房名称</summary>	
		[Description("库房名称")]
        public string Name { get; set; } // Name
		/// <summary>所在位置</summary>	
		[Description("所在位置")]
        public string Location { get; set; } // Location
		/// <summary>库房描述</summary>	
		[Description("库房描述")]
        public string Description { get; set; } // Description
		/// <summary>存储情况</summary>	
		[Description("存储情况")]
        public string Capacitance { get; set; } // Capacitance
		/// <summary>责任人</summary>	
		[Description("责任人")]
        public string ChargeUser { get; set; } // ChargeUser
		/// <summary>责任人名称</summary>	
		[Description("责任人名称")]
        public string ChargeUserName { get; set; } // ChargeUserName
		/// <summary>备注</summary>	
		[Description("备注")]
        public string Remarks { get; set; } // Remarks
		/// <summary>档案柜管理</summary>	
		[Description("档案柜管理")]
        public string CabinetDetail { get; set; } // CabinetDetail

        // Reverse navigation
		[JsonIgnore]
        public virtual ICollection<S_A_StorageRoom_CabinetDetail> S_A_StorageRoom_CabinetDetail { get { onS_A_StorageRoom_CabinetDetailGetting(); return _S_A_StorageRoom_CabinetDetail;} }
		private ICollection<S_A_StorageRoom_CabinetDetail> _S_A_StorageRoom_CabinetDetail;
		partial void onS_A_StorageRoom_CabinetDetailGetting();


        public S_A_StorageRoom()
        {
            _S_A_StorageRoom_CabinetDetail = new List<S_A_StorageRoom_CabinetDetail>();
        }
    }

	/// <summary>档案柜管理</summary>	
	[Description("档案柜管理")]
    public partial class S_A_StorageRoom_CabinetDetail : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string S_A_StorageRoomID { get; set; } // S_A_StorageRoomID
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string IsReleased { get; set; } // IsReleased
		/// <summary>柜架号编号</summary>	
		[Description("柜架号编号")]
        public string Code { get; set; } // Code
		/// <summary>柜架号名称</summary>	
		[Description("柜架号名称")]
        public string Name { get; set; } // Name
		/// <summary>所在区域</summary>	
		[Description("所在区域")]
        public string Area { get; set; } // Area
		/// <summary>柜架号描述</summary>	
		[Description("柜架号描述")]
        public string Description { get; set; } // Description
		/// <summary>存储情况</summary>	
		[Description("存储情况")]
        public string Capacitance { get; set; } // Capacitance
		/// <summary>责任人</summary>	
		[Description("责任人")]
        public string ChargeUser { get; set; } // ChargeUser
		/// <summary>责任人名称</summary>	
		[Description("责任人名称")]
        public string ChargeUserName { get; set; } // ChargeUserName
		/// <summary>备注</summary>	
		[Description("备注")]
        public string Remarks { get; set; } // Remarks

        // Foreign keys
		[JsonIgnore]
        public virtual S_A_StorageRoom S_A_StorageRoom { get; set; } //  S_A_StorageRoomID - FK_S_A_StorageRoom_CabinetDetail_S_A_StorageRoom
    }

	/// <summary></summary>	
	[Description("")]
    public partial class S_ArchiveCache : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string SpaceID { get; set; } // SpaceID
		/// <summary></summary>	
		[Description("")]
        public string ConfigID { get; set; } // ConfigID
		/// <summary></summary>	
		[Description("")]
        public string FileType { get; set; } // FileType
		/// <summary></summary>	
		[Description("")]
        public string Name { get; set; } // Name
		/// <summary></summary>	
		[Description("")]
        public string Code { get; set; } // Code
		/// <summary></summary>	
		[Description("")]
        public string MainFile { get; set; } // MainFile
		/// <summary></summary>	
		[Description("")]
        public string PdfFile { get; set; } // PdfFile
		/// <summary></summary>	
		[Description("")]
        public string PrintFile { get; set; } // PrintFile
		/// <summary></summary>	
		[Description("")]
        public string Attachment { get; set; } // Attachment
		/// <summary></summary>	
		[Description("")]
        public string Data { get; set; } // Data
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
        public DateTime ArchiveDate { get; set; } // ArchiveDate
		/// <summary></summary>	
		[Description("")]
        public string State { get; set; } // State
		/// <summary></summary>	
		[Description("")]
        public string CatalogID { get; set; } // CatalogID
		/// <summary></summary>	
		[Description("")]
        public string FullCatalogID { get; set; } // FullCatalogID
		/// <summary></summary>	
		[Description("")]
        public string SN { get; set; } // SN
		/// <summary></summary>	
		[Description("")]
        public string SNFlag { get; set; } // SNFlag

        // Foreign keys
		[JsonIgnore]
        public virtual S_ArchiveCacheCatalog S_ArchiveCacheCatalog { get; set; } //  CatalogID - FK_S_ArchiveCache_S_ArchiveCacheCatalog
    }

	/// <summary></summary>	
	[Description("")]
    public partial class S_ArchiveCacheCatalog : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string SpaceID { get; set; } // SpaceID
		/// <summary></summary>	
		[Description("")]
        public string ConfigID { get; set; } // ConfigID
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
        public string Type { get; set; } // Type
		/// <summary></summary>	
		[Description("")]
        public string FullID { get; set; } // FullID
		/// <summary></summary>	
		[Description("")]
        public string Data { get; set; } // Data
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
        public string State { get; set; } // State
		/// <summary></summary>	
		[Description("")]
        public string RelateID { get; set; } // RelateID
		/// <summary></summary>	
		[Description("")]
        public string ArchiveFormID { get; set; } // ArchiveFormID

        // Reverse navigation
		[JsonIgnore]
        public virtual ICollection<S_ArchiveCache> S_ArchiveCache { get { onS_ArchiveCacheGetting(); return _S_ArchiveCache;} }
		private ICollection<S_ArchiveCache> _S_ArchiveCache;
		partial void onS_ArchiveCacheGetting();


        public S_ArchiveCacheCatalog()
        {
            _S_ArchiveCache = new List<S_ArchiveCache>();
        }
    }

	/// <summary></summary>	
	[Description("")]
    public partial class S_BorrowDetail : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string BorrowID { get; set; } // BorrowID
		/// <summary></summary>	
		[Description("")]
        public string SpaceID { get; set; } // SpaceID
		/// <summary></summary>	
		[Description("")]
        public string SpaceName { get; set; } // SpaceName
		/// <summary></summary>	
		[Description("")]
        public string DataType { get; set; } // DataType
		/// <summary></summary>	
		[Description("")]
        public string DetailType { get; set; } // DetailType
		/// <summary></summary>	
		[Description("")]
        public string RelateID { get; set; } // RelateID
		/// <summary></summary>	
		[Description("")]
        public string ConfigID { get; set; } // ConfigID
		/// <summary></summary>	
		[Description("")]
        public string Name { get; set; } // Name
		/// <summary></summary>	
		[Description("")]
        public string Code { get; set; } // Code
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get; set; } // CreateUserID
		/// <summary></summary>	
		[Description("")]
        public string CreateUserName { get; set; } // CreateUserName
		/// <summary></summary>	
		[Description("")]
        public string CreateDeptID { get; set; } // CreateDeptID
		/// <summary></summary>	
		[Description("")]
        public string CreateDeptName { get; set; } // CreateDeptName
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public string LendUserID { get; set; } // LendUserID
		/// <summary></summary>	
		[Description("")]
        public string LendUserName { get; set; } // LendUserName
		/// <summary></summary>	
		[Description("")]
        public string LendDeptID { get; set; } // LendDeptID
		/// <summary></summary>	
		[Description("")]
        public string LendDeptName { get; set; } // LendDeptName
		/// <summary></summary>	
		[Description("")]
        public DateTime? LendDate { get; set; } // LendDate
		/// <summary></summary>	
		[Description("")]
        public string ReturnUserID { get; set; } // ReturnUserID
		/// <summary></summary>	
		[Description("")]
        public string ReturnUser { get; set; } // ReturnUser
		/// <summary></summary>	
		[Description("")]
        public string ReturnDeptID { get; set; } // ReturnDeptID
		/// <summary></summary>	
		[Description("")]
        public string ReturnDeptName { get; set; } // ReturnDeptName
		/// <summary></summary>	
		[Description("")]
        public DateTime? ReturnDate { get; set; } // ReturnDate
		/// <summary></summary>	
		[Description("")]
        public string LostUserID { get; set; } // LostUserID
		/// <summary></summary>	
		[Description("")]
        public string LostUserName { get; set; } // LostUserName
		/// <summary></summary>	
		[Description("")]
        public string LostDeptID { get; set; } // LostDeptID
		/// <summary></summary>	
		[Description("")]
        public string LostDeptName { get; set; } // LostDeptName
		/// <summary></summary>	
		[Description("")]
        public DateTime? LostDate { get; set; } // LostDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? BorrowExpireDate { get; set; } // BorrowExpireDate
		/// <summary></summary>	
		[Description("")]
        public int? ApplyNumber { get; set; } // ApplyNumber
		/// <summary></summary>	
		[Description("")]
        public int? LendNumber { get; set; } // LendNumber
		/// <summary></summary>	
		[Description("")]
        public int? ReturnNumber { get; set; } // ReturnNumber
		/// <summary></summary>	
		[Description("")]
        public int? LostNumber { get; set; } // LostNumber
		/// <summary></summary>	
		[Description("")]
        public string LostRemarks { get; set; } // LostRemarks
		/// <summary></summary>	
		[Description("")]
        public string BorrowState { get; set; } // BorrowState
		/// <summary></summary>	
		[Description("")]
        public string ParentID { get; set; } // ParentID
		/// <summary></summary>	
		[Description("")]
        public string HasChild { get; set; } // HasChild
		/// <summary>已续借次数</summary>	
		[Description("已续借次数")]
        public int? ContinuedTimes { get; set; } // ContinuedTimes
    }

	/// <summary></summary>	
	[Description("")]
    public partial class S_CarInfo : Formula.BaseModel
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
        public string Name { get; set; } // Name
		/// <summary></summary>	
		[Description("")]
        public string Code { get; set; } // Code
		/// <summary></summary>	
		[Description("")]
        public string DataType { get; set; } // DataType
		/// <summary></summary>	
		[Description("")]
        public string Type { get; set; } // Type
		/// <summary></summary>	
		[Description("")]
        public string FileID { get; set; } // FileID
		/// <summary></summary>	
		[Description("")]
        public string NodeID { get; set; } // NodeID
		/// <summary></summary>	
		[Description("")]
        public string NodeName { get; set; } // NodeName
		/// <summary></summary>	
		[Description("")]
        public string State { get; set; } // State
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
        public string ConfigID { get; set; } // ConfigID
		/// <summary></summary>	
		[Description("")]
        public string SpaceID { get; set; } // SpaceID
		/// <summary></summary>	
		[Description("")]
        public string Attachments { get; set; } // Attachments
    }

	/// <summary></summary>	
	[Description("")]
    public partial class S_DocumentLog : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string FileID { get; set; } // FileID
		/// <summary></summary>	
		[Description("")]
        public string Name { get; set; } // Name
		/// <summary></summary>	
		[Description("")]
        public string NodeID { get; set; } // NodeID
		/// <summary></summary>	
		[Description("")]
        public string SpaceID { get; set; } // SpaceID
		/// <summary></summary>	
		[Description("")]
        public string ConfigID { get; set; } // ConfigID
		/// <summary></summary>	
		[Description("")]
        public string FullNodeID { get; set; } // FullNodeID
		/// <summary></summary>	
		[Description("")]
        public string LogType { get; set; } // LogType
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get; set; } // CreateUserID
		/// <summary></summary>	
		[Description("")]
        public string CreateUserName { get; set; } // CreateUserName
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateDate { get; set; } // CreateDate
    }

	/// <summary></summary>	
	[Description("")]
    public partial class S_DownloadDetail : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string DownloadID { get; set; } // DownloadID
		/// <summary></summary>	
		[Description("")]
        public string Name { get; set; } // Name
		/// <summary></summary>	
		[Description("")]
        public string Code { get; set; } // Code
		/// <summary></summary>	
		[Description("")]
        public string SpaceID { get; set; } // SpaceID
		/// <summary></summary>	
		[Description("")]
        public string ConfigID { get; set; } // ConfigID
		/// <summary></summary>	
		[Description("")]
        public string FileID { get; set; } // FileID
		/// <summary></summary>	
		[Description("")]
        public string Attachments { get; set; } // Attachments
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get; set; } // CreateUserID
		/// <summary></summary>	
		[Description("")]
        public string CreateUserName { get; set; } // CreateUserName
		/// <summary></summary>	
		[Description("")]
        public DateTime CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public string UserDeptID { get; set; } // UserDeptID
		/// <summary></summary>	
		[Description("")]
        public string UserDeptName { get; set; } // UserDeptName
		/// <summary></summary>	
		[Description("")]
        public DateTime? PassDate { get; set; } // PassDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? DownloadExpireDate { get; set; } // DownloadExpireDate
		/// <summary></summary>	
		[Description("")]
        public string DownloadState { get; set; } // DownloadState
    }

	/// <summary>延长保管期限申请</summary>	
	[Description("延长保管期限申请")]
    public partial class S_I_ChangePeriodApply : Formula.BaseModel
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
		/// <summary>延期清册名称</summary>	
		[Description("延期清册名称")]
        public string Name { get; set; } // Name
		/// <summary>延期清册编号</summary>	
		[Description("延期清册编号")]
        public string SerialNumber { get; set; } // SerialNumber
		/// <summary>保管期限</summary>	
		[Description("保管期限")]
        public string KeepYear { get; set; } // KeepYear
		/// <summary>备注</summary>	
		[Description("备注")]
        public string Remarks { get; set; } // Remarks
		/// <summary>档案负责人审批</summary>	
		[Description("档案负责人审批")]
        public string DocManagerSign { get; set; } // DocManagerSign
		/// <summary>IdentifyApplyID</summary>	
		[Description("IdentifyApplyID")]
        public string IdentifyApplyID { get; set; } // IdentifyApplyID
		/// <summary>同步子节点</summary>	
		[Description("同步子节点")]
        public string SynchChild { get; set; } // SynchChild

        // Reverse navigation
		[JsonIgnore]
        public virtual ICollection<S_I_ChangePeriodApply_DetailInfo> S_I_ChangePeriodApply_DetailInfo { get { onS_I_ChangePeriodApply_DetailInfoGetting(); return _S_I_ChangePeriodApply_DetailInfo;} }
		private ICollection<S_I_ChangePeriodApply_DetailInfo> _S_I_ChangePeriodApply_DetailInfo;
		partial void onS_I_ChangePeriodApply_DetailInfoGetting();


        public S_I_ChangePeriodApply()
        {
            _S_I_ChangePeriodApply_DetailInfo = new List<S_I_ChangePeriodApply_DetailInfo>();
        }
    }

	/// <summary>清单</summary>	
	[Description("清单")]
    public partial class S_I_ChangePeriodApply_DetailInfo : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string S_I_ChangePeriodApplyID { get; set; } // S_I_ChangePeriodApplyID
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string IsReleased { get; set; } // IsReleased
		/// <summary>IdentifyInfoID</summary>	
		[Description("IdentifyInfoID")]
        public string IdentifyInfoID { get; set; } // IdentifyInfoID
		/// <summary>IdentifyApplyDetailInfoID</summary>	
		[Description("IdentifyApplyDetailInfoID")]
        public string IdentifyApplyDetailInfoID { get; set; } // IdentifyApplyDetailInfoID
		/// <summary>档号</summary>	
		[Description("档号")]
        public string Code { get; set; } // Code
		/// <summary>名称</summary>	
		[Description("名称")]
        public string Name { get; set; } // Name
		/// <summary>类型</summary>	
		[Description("类型")]
        public string DataType { get; set; } // DataType
		/// <summary>档案门类</summary>	
		[Description("档案门类")]
        public string SpaceName { get; set; } // SpaceName
		/// <summary>应保管期限</summary>	
		[Description("应保管期限")]
        public string KeepYear { get; set; } // KeepYear
		/// <summary>已保管期限</summary>	
		[Description("已保管期限")]
        public string KeepYearToToday { get; set; } // KeepYearToToday
		/// <summary>密级</summary>	
		[Description("密级")]
        public string SecretLevel { get; set; } // SecretLevel
		/// <summary>归档人</summary>	
		[Description("归档人")]
        public string ArchivePeople { get; set; } // ArchivePeople
		/// <summary>归档人名称</summary>	
		[Description("归档人名称")]
        public string ArchivePeopleName { get; set; } // ArchivePeopleName
		/// <summary>归档日期</summary>	
		[Description("归档日期")]
        public DateTime? ArchiveDate { get; set; } // ArchiveDate
		/// <summary>卷内文件数</summary>	
		[Description("卷内文件数")]
        public int? PhysicalFileCount { get; set; } // PhysicalFileCount
		/// <summary>库房</summary>	
		[Description("库房")]
        public string StorageRoom { get; set; } // StorageRoom
		/// <summary>库房名称</summary>	
		[Description("库房名称")]
        public string StorageRoomName { get; set; } // StorageRoomName
		/// <summary>货架号</summary>	
		[Description("货架号")]
        public string RackNumber { get; set; } // RackNumber
		/// <summary>货架号名称</summary>	
		[Description("货架号名称")]
        public string RackNumberName { get; set; } // RackNumberName

        // Foreign keys
		[JsonIgnore]
        public virtual S_I_ChangePeriodApply S_I_ChangePeriodApply { get; set; } //  S_I_ChangePeriodApplyID - FK_S_I_ChangePeriodApply_DetailInfo_S_I_ChangePeriodApply
    }

	/// <summary>销毁清册</summary>	
	[Description("销毁清册")]
    public partial class S_I_DestroyApply : Formula.BaseModel
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
		/// <summary>销毁清册名称</summary>	
		[Description("销毁清册名称")]
        public string Name { get; set; } // Name
		/// <summary>销毁清册编号</summary>	
		[Description("销毁清册编号")]
        public string SerialNumber { get; set; } // SerialNumber
		/// <summary>申请人</summary>	
		[Description("申请人")]
        public string ApplyUser { get; set; } // ApplyUser
		/// <summary>申请人名称</summary>	
		[Description("申请人名称")]
        public string ApplyUserName { get; set; } // ApplyUserName
		/// <summary>申请时间</summary>	
		[Description("申请时间")]
        public string ApplyDate { get; set; } // ApplyDate
		/// <summary>监销人</summary>	
		[Description("监销人")]
        public string SupervisorUser { get; set; } // SupervisorUser
		/// <summary>监销人名称</summary>	
		[Description("监销人名称")]
        public string SupervisorUserName { get; set; } // SupervisorUserName
		/// <summary>备注</summary>	
		[Description("备注")]
        public string Remarks { get; set; } // Remarks
		/// <summary>档案负责人审批</summary>	
		[Description("档案负责人审批")]
        public string DocManagerSign { get; set; } // DocManagerSign
		/// <summary>监销人审批</summary>	
		[Description("监销人审批")]
        public string SupervisorUserSign { get; set; } // SupervisorUserSign
		/// <summary>IdentifyApplyID</summary>	
		[Description("IdentifyApplyID")]
        public string IdentifyApplyID { get; set; } // IdentifyApplyID

        // Reverse navigation
		[JsonIgnore]
        public virtual ICollection<S_I_DestroyApply_DetailInfo> S_I_DestroyApply_DetailInfo { get { onS_I_DestroyApply_DetailInfoGetting(); return _S_I_DestroyApply_DetailInfo;} }
		private ICollection<S_I_DestroyApply_DetailInfo> _S_I_DestroyApply_DetailInfo;
		partial void onS_I_DestroyApply_DetailInfoGetting();


        public S_I_DestroyApply()
        {
            _S_I_DestroyApply_DetailInfo = new List<S_I_DestroyApply_DetailInfo>();
        }
    }

	/// <summary>销毁清单</summary>	
	[Description("销毁清单")]
    public partial class S_I_DestroyApply_DetailInfo : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string S_I_DestroyApplyID { get; set; } // S_I_DestroyApplyID
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string IsReleased { get; set; } // IsReleased
		/// <summary>IdentifyInfoID</summary>	
		[Description("IdentifyInfoID")]
        public string IdentifyInfoID { get; set; } // IdentifyInfoID
		/// <summary>IdentifyApplyDetailInfoID</summary>	
		[Description("IdentifyApplyDetailInfoID")]
        public string IdentifyApplyDetailInfoID { get; set; } // IdentifyApplyDetailInfoID
		/// <summary>销毁状态</summary>	
		[Description("销毁状态")]
        public string DestroyState { get; set; } // DestroyState
		/// <summary>监销人</summary>	
		[Description("监销人")]
        public string SupervisorUser { get; set; } // SupervisorUser
		/// <summary>监销人名称</summary>	
		[Description("监销人名称")]
        public string SupervisorUserName { get; set; } // SupervisorUserName
		/// <summary>销毁日期</summary>	
		[Description("销毁日期")]
        public DateTime? DestroyDate { get; set; } // DestroyDate
		/// <summary>鉴定结果</summary>	
		[Description("鉴定结果")]
        public string IdentifyResult { get; set; } // IdentifyResult
		/// <summary>鉴定日期</summary>	
		[Description("鉴定日期")]
        public DateTime? IdentifyDate { get; set; } // IdentifyDate
		/// <summary>鉴定人</summary>	
		[Description("鉴定人")]
        public string IdentifyUser { get; set; } // IdentifyUser
		/// <summary>鉴定人名称</summary>	
		[Description("鉴定人名称")]
        public string IdentifyUserName { get; set; } // IdentifyUserName
		/// <summary>档号</summary>	
		[Description("档号")]
        public string Code { get; set; } // Code
		/// <summary>名称</summary>	
		[Description("名称")]
        public string Name { get; set; } // Name
		/// <summary>类型</summary>	
		[Description("类型")]
        public string DataType { get; set; } // DataType
		/// <summary>档案门类</summary>	
		[Description("档案门类")]
        public string SpaceName { get; set; } // SpaceName
		/// <summary>应保管期限</summary>	
		[Description("应保管期限")]
        public string KeepYear { get; set; } // KeepYear
		/// <summary>已保管期限</summary>	
		[Description("已保管期限")]
        public string KeepYearToToday { get; set; } // KeepYearToToday
		/// <summary>密级</summary>	
		[Description("密级")]
        public string SecretLevel { get; set; } // SecretLevel
		/// <summary>归档人</summary>	
		[Description("归档人")]
        public string ArchivePeople { get; set; } // ArchivePeople
		/// <summary>归档人名称</summary>	
		[Description("归档人名称")]
        public string ArchivePeopleName { get; set; } // ArchivePeopleName
		/// <summary>归档日期</summary>	
		[Description("归档日期")]
        public DateTime? ArchiveDate { get; set; } // ArchiveDate
		/// <summary>卷内文件数</summary>	
		[Description("卷内文件数")]
        public int? PhysicalFileCount { get; set; } // PhysicalFileCount
		/// <summary>库房</summary>	
		[Description("库房")]
        public string StorageRoom { get; set; } // StorageRoom
		/// <summary>库房名称</summary>	
		[Description("库房名称")]
        public string StorageRoomName { get; set; } // StorageRoomName
		/// <summary>货架号</summary>	
		[Description("货架号")]
        public string RackNumber { get; set; } // RackNumber
		/// <summary>货架号名称</summary>	
		[Description("货架号名称")]
        public string RackNumberName { get; set; } // RackNumberName

        // Foreign keys
		[JsonIgnore]
        public virtual S_I_DestroyApply S_I_DestroyApply { get; set; } //  S_I_DestroyApplyID - FK_S_I_DestroyApply_DetailInfo_S_I_DestroyApply
    }

	/// <summary>鉴定申请</summary>	
	[Description("鉴定申请")]
    public partial class S_I_IdentifyApply : Formula.BaseModel
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
		/// <summary>鉴定清单名称</summary>	
		[Description("鉴定清单名称")]
        public string Name { get; set; } // Name
		/// <summary>鉴定清单编号</summary>	
		[Description("鉴定清单编号")]
        public string SerialNumber { get; set; } // SerialNumber
		/// <summary>鉴定工作负责人</summary>	
		[Description("鉴定工作负责人")]
        public string ChargeUser { get; set; } // ChargeUser
		/// <summary>鉴定工作负责人名称</summary>	
		[Description("鉴定工作负责人名称")]
        public string ChargeUserName { get; set; } // ChargeUserName
		/// <summary>鉴定小组成员</summary>	
		[Description("鉴定小组成员")]
        public string GroupUser { get; set; } // GroupUser
		/// <summary>鉴定小组成员名称</summary>	
		[Description("鉴定小组成员名称")]
        public string GroupUserName { get; set; } // GroupUserName
		/// <summary>备注</summary>	
		[Description("备注")]
        public string Remarks { get; set; } // Remarks
		/// <summary>鉴定小组审批</summary>	
		[Description("鉴定小组审批")]
        public string GroupUserSign { get; set; } // GroupUserSign
		/// <summary>处理状态</summary>	
		[Description("处理状态")]
        public string HandleState { get; set; } // HandleState

        // Reverse navigation
		[JsonIgnore]
        public virtual ICollection<S_I_IdentifyApply_DetailInfo> S_I_IdentifyApply_DetailInfo { get { onS_I_IdentifyApply_DetailInfoGetting(); return _S_I_IdentifyApply_DetailInfo;} }
		private ICollection<S_I_IdentifyApply_DetailInfo> _S_I_IdentifyApply_DetailInfo;
		partial void onS_I_IdentifyApply_DetailInfoGetting();


        public S_I_IdentifyApply()
        {
            _S_I_IdentifyApply_DetailInfo = new List<S_I_IdentifyApply_DetailInfo>();
        }
    }

	/// <summary>待鉴定内容</summary>	
	[Description("待鉴定内容")]
    public partial class S_I_IdentifyApply_DetailInfo : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string S_I_IdentifyApplyID { get; set; } // S_I_IdentifyApplyID
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string IsReleased { get; set; } // IsReleased
		/// <summary>IdentifyInfoID</summary>	
		[Description("IdentifyInfoID")]
        public string IdentifyInfoID { get; set; } // IdentifyInfoID
		/// <summary>处理结果</summary>	
		[Description("处理结果")]
        public string HandleResult { get; set; } // HandleResult
		/// <summary>鉴定结果</summary>	
		[Description("鉴定结果")]
        public string IdentifyResult { get; set; } // IdentifyResult
		/// <summary>鉴定日期</summary>	
		[Description("鉴定日期")]
        public DateTime? IdentifyDate { get; set; } // IdentifyDate
		/// <summary>鉴定人</summary>	
		[Description("鉴定人")]
        public string IdentifyUser { get; set; } // IdentifyUser
		/// <summary>鉴定人名称</summary>	
		[Description("鉴定人名称")]
        public string IdentifyUserName { get; set; } // IdentifyUserName
		/// <summary>档号</summary>	
		[Description("档号")]
        public string Code { get; set; } // Code
		/// <summary>名称</summary>	
		[Description("名称")]
        public string Name { get; set; } // Name
		/// <summary>类型</summary>	
		[Description("类型")]
        public string DataType { get; set; } // DataType
		/// <summary>档案门类</summary>	
		[Description("档案门类")]
        public string SpaceName { get; set; } // SpaceName
		/// <summary>应保管期限</summary>	
		[Description("应保管期限")]
        public string KeepYear { get; set; } // KeepYear
		/// <summary>已保管期限</summary>	
		[Description("已保管期限")]
        public string KeepYearToToday { get; set; } // KeepYearToToday
		/// <summary>密级</summary>	
		[Description("密级")]
        public string SecretLevel { get; set; } // SecretLevel
		/// <summary>归档人</summary>	
		[Description("归档人")]
        public string ArchivePeople { get; set; } // ArchivePeople
		/// <summary>归档人名称</summary>	
		[Description("归档人名称")]
        public string ArchivePeopleName { get; set; } // ArchivePeopleName
		/// <summary>归档日期</summary>	
		[Description("归档日期")]
        public DateTime? ArchiveDate { get; set; } // ArchiveDate
		/// <summary>卷内文件数</summary>	
		[Description("卷内文件数")]
        public int? PhysicalFileCount { get; set; } // PhysicalFileCount
		/// <summary>库房</summary>	
		[Description("库房")]
        public string StorageRoom { get; set; } // StorageRoom
		/// <summary>库房名称</summary>	
		[Description("库房名称")]
        public string StorageRoomName { get; set; } // StorageRoomName
		/// <summary>货架号</summary>	
		[Description("货架号")]
        public string RackNumber { get; set; } // RackNumber
		/// <summary>货架号名称</summary>	
		[Description("货架号名称")]
        public string RackNumberName { get; set; } // RackNumberName

        // Foreign keys
		[JsonIgnore]
        public virtual S_I_IdentifyApply S_I_IdentifyApply { get; set; } //  S_I_IdentifyApplyID - FK_S_I_IdentifyApply_DetailInfo_S_I_IdentifyApply
    }

	/// <summary>鉴定信息</summary>	
	[Description("鉴定信息")]
    public partial class S_I_IdentifyInfo : Formula.BaseModel
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
		/// <summary>状态</summary>	
		[Description("状态")]
        public string State { get; set; } // State
		/// <summary>SpaceID</summary>	
		[Description("SpaceID")]
        public string SpaceID { get; set; } // SpaceID
		/// <summary>档案门类</summary>	
		[Description("档案门类")]
        public string SpaceName { get; set; } // SpaceName
		/// <summary>NodeID</summary>	
		[Description("NodeID")]
        public string NodeID { get; set; } // NodeID
		/// <summary>ConfigID</summary>	
		[Description("ConfigID")]
        public string ConfigID { get; set; } // ConfigID
		/// <summary>名称</summary>	
		[Description("名称")]
        public string Name { get; set; } // Name
		/// <summary>档号</summary>	
		[Description("档号")]
        public string Code { get; set; } // Code
		/// <summary>类型</summary>	
		[Description("类型")]
        public string DataType { get; set; } // DataType
		/// <summary>应保管期限</summary>	
		[Description("应保管期限")]
        public string KeepYear { get; set; } // KeepYear
		/// <summary>已保管期限</summary>	
		[Description("已保管期限")]
        public string KeepYearToToday { get; set; } // KeepYearToToday
		/// <summary>密级</summary>	
		[Description("密级")]
        public string SecretLevel { get; set; } // SecretLevel
		/// <summary>归档人</summary>	
		[Description("归档人")]
        public string ArchivePeople { get; set; } // ArchivePeople
		/// <summary>归档人名称</summary>	
		[Description("归档人名称")]
        public string ArchivePeopleName { get; set; } // ArchivePeopleName
		/// <summary>归档部门</summary>	
		[Description("归档部门")]
        public string ArchiveDepartment { get; set; } // ArchiveDepartment
		/// <summary>归档部门名称</summary>	
		[Description("归档部门名称")]
        public string ArchiveDepartmentName { get; set; } // ArchiveDepartmentName
		/// <summary>归档日期</summary>	
		[Description("归档日期")]
        public DateTime? ArchiveDate { get; set; } // ArchiveDate
		/// <summary>卷内文件数</summary>	
		[Description("卷内文件数")]
        public int? PhysicalFileCount { get; set; } // PhysicalFileCount
		/// <summary>库房</summary>	
		[Description("库房")]
        public string StorageRoom { get; set; } // StorageRoom
		/// <summary>库房名称</summary>	
		[Description("库房名称")]
        public string StorageRoomName { get; set; } // StorageRoomName
		/// <summary>货架号</summary>	
		[Description("货架号")]
        public string RackNumber { get; set; } // RackNumber
		/// <summary>货架号名称</summary>	
		[Description("货架号名称")]
        public string RackNumberName { get; set; } // RackNumberName
    }

	/// <summary>鉴定规则</summary>	
	[Description("鉴定规则")]
    public partial class S_I_IdentifyRule : Formula.BaseModel
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
		/// <summary>日期规则</summary>	
		[Description("日期规则")]
        public string DayRule { get; set; } // DayRule
		/// <summary>日期规则名称</summary>	
		[Description("日期规则名称")]
        public string DayRuleName { get; set; } // DayRuleName
    }

	/// <summary>设计档案实物签收单</summary>	
	[Description("设计档案实物签收单")]
    public partial class S_R_PhysicalReorganize : Formula.BaseModel
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
		/// <summary>签收单编号</summary>	
		[Description("签收单编号")]
        public string Code { get; set; } // Code
		/// <summary>签收单名称</summary>	
		[Description("签收单名称")]
        public string Name { get; set; } // Name
		/// <summary>项目编号</summary>	
		[Description("项目编号")]
        public string ProjectCode { get; set; } // ProjectCode
		/// <summary>项目经理</summary>	
		[Description("项目经理")]
        public string ProjectManager { get; set; } // ProjectManager
		/// <summary>项目经理名称</summary>	
		[Description("项目经理名称")]
        public string ProjectManagerName { get; set; } // ProjectManagerName
		/// <summary>送归档人</summary>	
		[Description("送归档人")]
        public string SendUser { get; set; } // SendUser
		/// <summary>送归档人名称</summary>	
		[Description("送归档人名称")]
        public string SendUserName { get; set; } // SendUserName
		/// <summary>送归档时间</summary>	
		[Description("送归档时间")]
        public DateTime? SendDate { get; set; } // SendDate
		/// <summary>签收人</summary>	
		[Description("签收人")]
        public string ReceiveUser { get; set; } // ReceiveUser
		/// <summary>签收人名称</summary>	
		[Description("签收人名称")]
        public string ReceiveUserName { get; set; } // ReceiveUserName
		/// <summary>签收时间</summary>	
		[Description("签收时间")]
        public DateTime? ReceiveDate { get; set; } // ReceiveDate
		/// <summary>备注</summary>	
		[Description("备注")]
        public string Remark { get; set; } // Remark
		/// <summary>图档实例库ID</summary>	
		[Description("图档实例库ID")]
        public string SpaceID { get; set; } // SpaceID
		/// <summary>整编状态</summary>	
		[Description("整编状态")]
        public string State { get; set; } // State
		/// <summary>整编人</summary>	
		[Description("整编人")]
        public string ReorganizeUser { get; set; } // ReorganizeUser
		/// <summary>整编时间</summary>	
		[Description("整编时间")]
        public DateTime? ReorganizeDate { get; set; } // ReorganizeDate
		/// <summary>归档目录选择</summary>	
		[Description("归档目录选择")]
        public string DefaultNodes { get; set; } // DefaultNodes
		/// <summary>归档目录选择名称</summary>	
		[Description("归档目录选择名称")]
        public string DefaultNodesName { get; set; } // DefaultNodesName
		/// <summary>整编人名称</summary>	
		[Description("整编人名称")]
        public string ReorganizeUserName { get; set; } // ReorganizeUserName
		/// <summary>TmplCode</summary>	
		[Description("TmplCode")]
        public string TmplCode { get; set; } // TmplCode
		/// <summary>SelectNodeIDs</summary>	
		[Description("SelectNodeIDs")]
        public string SelectNodeIDs { get; set; } // SelectNodeIDs

        // Reverse navigation
		[JsonIgnore]
        public virtual ICollection<S_R_PhysicalReorganize_FileDetail> S_R_PhysicalReorganize_FileDetail { get { onS_R_PhysicalReorganize_FileDetailGetting(); return _S_R_PhysicalReorganize_FileDetail;} }
		private ICollection<S_R_PhysicalReorganize_FileDetail> _S_R_PhysicalReorganize_FileDetail;
		partial void onS_R_PhysicalReorganize_FileDetailGetting();

		[JsonIgnore]
        public virtual ICollection<S_R_PhysicalReorganize_NodeDetail> S_R_PhysicalReorganize_NodeDetail { get { onS_R_PhysicalReorganize_NodeDetailGetting(); return _S_R_PhysicalReorganize_NodeDetail;} }
		private ICollection<S_R_PhysicalReorganize_NodeDetail> _S_R_PhysicalReorganize_NodeDetail;
		partial void onS_R_PhysicalReorganize_NodeDetailGetting();


        public S_R_PhysicalReorganize()
        {
            _S_R_PhysicalReorganize_FileDetail = new List<S_R_PhysicalReorganize_FileDetail>();
            _S_R_PhysicalReorganize_NodeDetail = new List<S_R_PhysicalReorganize_NodeDetail>();
        }
    }

	/// <summary>其他资料文件表单</summary>	
	[Description("其他资料文件表单")]
    public partial class S_R_PhysicalReorganize_FileDetail : Formula.BaseModel
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
		/// <summary>签收单ID</summary>	
		[Description("签收单ID")]
        public string S_R_PhysicalReorganizeID { get; set; } // S_R_PhysicalReorganizeID
		/// <summary>FileConfigID</summary>	
		[Description("FileConfigID")]
        public string FileConfigID { get; set; } // FileConfigID
		/// <summary>其他相关资料名称</summary>	
		[Description("其他相关资料名称")]
        public string Name { get; set; } // Name
		/// <summary>其他相关资料编号</summary>	
		[Description("其他相关资料编号")]
        public string Code { get; set; } // Code
		/// <summary>页数</summary>	
		[Description("页数")]
        public string PageCount { get; set; } // PageCount
		/// <summary>版次</summary>	
		[Description("版次")]
        public string Version { get; set; } // Version
		/// <summary>载体</summary>	
		[Description("载体")]
        public string Media { get; set; } // Media
		/// <summary>整编目录</summary>	
		[Description("整编目录")]
        public string ReorganizePath { get; set; } // ReorganizePath
		/// <summary>整编目录全路径</summary>	
		[Description("整编目录全路径")]
        public string ReorganizeFullID { get; set; } // ReorganizeFullID
		/// <summary>整编文件ID</summary>	
		[Description("整编文件ID")]
        public string ArchiveFileID { get; set; } // ArchiveFileID
		/// <summary>FromCode</summary>	
		[Description("FromCode")]
        public string FromCode { get; set; } // FromCode
		/// <summary>图幅</summary>	
		[Description("图幅")]
        public string Specification { get; set; } // Specification
		/// <summary>是否加长</summary>	
		[Description("是否加长")]
        public string Length { get; set; } // Length
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
		/// <summary>设计完成时间</summary>	
		[Description("设计完成时间")]
        public DateTime? DesignFinishDate { get; set; } // DesignFinishDate
		/// <summary>项目名称</summary>	
		[Description("项目名称")]
        public string ProjectName { get; set; } // ProjectName
		/// <summary>项目编号</summary>	
		[Description("项目编号")]
        public string ProjectCode { get; set; } // ProjectCode
		/// <summary>业务类型</summary>	
		[Description("业务类型")]
        public string ProjectClass { get; set; } // ProjectClass
		/// <summary>项目规模</summary>	
		[Description("项目规模")]
        public string ProjectLevel { get; set; } // ProjectLevel
		/// <summary>专业</summary>	
		[Description("专业")]
        public string Major { get; set; } // Major
		/// <summary>备注</summary>	
		[Description("备注")]
        public string Remark { get; set; } // Remark
		/// <summary>ISO表单分类</summary>	
		[Description("ISO表单分类")]
        public string Type { get; set; } // Type
		/// <summary>文件形成时间</summary>	
		[Description("文件形成时间")]
        public string FileCreateDate { get; set; } // FileCreateDate
		/// <summary>收集人</summary>	
		[Description("收集人")]
        public string CollectionPeople { get; set; } // CollectionPeople
		/// <summary>收集时间</summary>	
		[Description("收集时间")]
        public DateTime? CollectionDate { get; set; } // CollectionDate
		/// <summary>项目负责人</summary>	
		[Description("项目负责人")]
        public string ChargeUserName { get; set; } // ChargeUserName
		/// <summary>项目名称名称</summary>	
		[Description("项目名称名称")]
        public string ProjectNameName { get; set; } // ProjectNameName
		/// <summary>文件类型</summary>	
		[Description("文件类型")]
        public string FileType { get; set; } // FileType
		/// <summary>电子档案是否归档</summary>	
		[Description("电子档案是否归档")]
        public string YesOrNo { get; set; } // YesOrNo
		/// <summary>份数</summary>	
		[Description("份数")]
        public string Quantity { get; set; } // Quantity
		/// <summary>密级</summary>	
		[Description("密级")]
        public string SecretLevel { get; set; } // SecretLevel
		/// <summary>保管期限</summary>	
		[Description("保管期限")]
        public string KeepYear { get; set; } // KeepYear
		/// <summary>设计阶段</summary>	
		[Description("设计阶段")]
        public string PhaseValue { get; set; } // PhaseValue
		/// <summary>存储类型</summary>	
		[Description("存储类型")]
        public string StorageType { get; set; } // StorageType
		/// <summary>版次</summary>	
		[Description("版次")]
        public string Edition { get; set; } // Edition
		/// <summary>项目负责人名称</summary>	
		[Description("项目负责人名称")]
        public string ChargeUserNameName { get; set; } // ChargeUserNameName
		/// <summary>项目负责人</summary>	
		[Description("项目负责人")]
        public string ChargeUser { get; set; } // ChargeUser
		/// <summary>收集人名称</summary>	
		[Description("收集人名称")]
        public string CollectionPeopleName { get; set; } // CollectionPeopleName
		/// <summary>归档人</summary>	
		[Description("归档人")]
        public string ArchivePeople { get; set; } // ArchivePeople
		/// <summary>归档人名称</summary>	
		[Description("归档人名称")]
        public string ArchivePeopleName { get; set; } // ArchivePeopleName
		/// <summary>归档部门</summary>	
		[Description("归档部门")]
        public string ArchiveDepartment { get; set; } // ArchiveDepartment
		/// <summary>归档部门名称</summary>	
		[Description("归档部门名称")]
        public string ArchiveDepartmentName { get; set; } // ArchiveDepartmentName
		/// <summary>归档时间</summary>	
		[Description("归档时间")]
        public DateTime? ArchiveDate { get; set; } // ArchiveDate

        // Foreign keys
		[JsonIgnore]
        public virtual S_R_PhysicalReorganize S_R_PhysicalReorganize { get; set; } //  S_R_PhysicalReorganizeID - FK_S_R_PhysicalReorganize_FileDetail_S_R_PhysicalReorganize
    }

	/// <summary>项目卷册表单</summary>	
	[Description("项目卷册表单")]
    public partial class S_R_PhysicalReorganize_NodeDetail : Formula.BaseModel
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
		/// <summary>卷册名称</summary>	
		[Description("卷册名称")]
        public string Name { get; set; } // Name
		/// <summary>档号</summary>	
		[Description("档号")]
        public string Code { get; set; } // Code
		/// <summary>S_R_PhysicalReorganizeID</summary>	
		[Description("S_R_PhysicalReorganizeID")]
        public string S_R_PhysicalReorganizeID { get; set; } // S_R_PhysicalReorganizeID
		/// <summary>NodeConfID</summary>	
		[Description("NodeConfID")]
        public string NodeConfID { get; set; } // NodeConfID
		/// <summary>NodeType</summary>	
		[Description("NodeType")]
        public string NodeType { get; set; } // NodeType
		/// <summary>StorageType</summary>	
		[Description("StorageType")]
        public string StorageType { get; set; } // StorageType
		/// <summary>整编目录</summary>	
		[Description("整编目录")]
        public string ReorganizePath { get; set; } // ReorganizePath
		/// <summary>整编目录全路径</summary>	
		[Description("整编目录全路径")]
        public string ReorganizeFullID { get; set; } // ReorganizeFullID
		/// <summary>整编卷册ID</summary>	
		[Description("整编卷册ID")]
        public string ArchiveVolumnID { get; set; } // ArchiveVolumnID
		/// <summary>FromCode</summary>	
		[Description("FromCode")]
        public string FromCode { get; set; } // FromCode
		/// <summary>互见号</summary>	
		[Description("互见号")]
        public string Mutual { get; set; } // Mutual
		/// <summary>立卷部门</summary>	
		[Description("立卷部门")]
        public string VolumnDepartment { get; set; } // VolumnDepartment
		/// <summary>立卷部门名称</summary>	
		[Description("立卷部门名称")]
        public string VolumnDepartmentName { get; set; } // VolumnDepartmentName
		/// <summary>立卷人</summary>	
		[Description("立卷人")]
        public string VolumnPeople { get; set; } // VolumnPeople
		/// <summary>立卷人名称</summary>	
		[Description("立卷人名称")]
        public string VolumnPeopleName { get; set; } // VolumnPeopleName
		/// <summary>立卷日期</summary>	
		[Description("立卷日期")]
        public DateTime? VolumnDate { get; set; } // VolumnDate
		/// <summary>起止日期（起）</summary>	
		[Description("起止日期（起）")]
        public DateTime? StartDate { get; set; } // StartDate
		/// <summary>起止日期（止）</summary>	
		[Description("起止日期（止）")]
        public DateTime? EndDate { get; set; } // EndDate
		/// <summary>密级</summary>	
		[Description("密级")]
        public string SecretLevel { get; set; } // SecretLevel
		/// <summary>保管期限</summary>	
		[Description("保管期限")]
        public string KeepYear { get; set; } // KeepYear
		/// <summary>柜架号</summary>	
		[Description("柜架号")]
        public string CabinetCode { get; set; } // CabinetCode
		/// <summary>库房名称</summary>	
		[Description("库房名称")]
        public string StorageName { get; set; } // StorageName
		/// <summary>卷内实物总页数</summary>	
		[Description("卷内实物总页数")]
        public string PhysicalPageCount { get; set; } // PhysicalPageCount
		/// <summary>卷内实物总数</summary>	
		[Description("卷内实物总数")]
        public string PhysicalCount { get; set; } // PhysicalCount
		/// <summary>卷内说明</summary>	
		[Description("卷内说明")]
        public string VolumnExplain { get; set; } // VolumnExplain
		/// <summary>备注</summary>	
		[Description("备注")]
        public string Remark { get; set; } // Remark
		/// <summary>份数</summary>	
		[Description("份数")]
        public int? Quantity { get; set; } // Quantity
		/// <summary>柜架号名称</summary>	
		[Description("柜架号名称")]
        public string CabinetCodeName { get; set; } // CabinetCodeName
		/// <summary>档号</summary>	
		[Description("档号")]
        public string DocumentCode { get; set; } // DocumentCode
		/// <summary>柜架号</summary>	
		[Description("柜架号")]
        public string RackNumber { get; set; } // RackNumber
		/// <summary>柜架号名称</summary>	
		[Description("柜架号名称")]
        public string RackNumberName { get; set; } // RackNumberName
		/// <summary>库房名称</summary>	
		[Description("库房名称")]
        public string StorageRoom { get; set; } // StorageRoom
		/// <summary>归档人</summary>	
		[Description("归档人")]
        public string ArchivePeople { get; set; } // ArchivePeople
		/// <summary>归档人名称</summary>	
		[Description("归档人名称")]
        public string ArchivePeopleName { get; set; } // ArchivePeopleName
		/// <summary>归档部门</summary>	
		[Description("归档部门")]
        public string ArchiveDepartment { get; set; } // ArchiveDepartment
		/// <summary>归档部门名称</summary>	
		[Description("归档部门名称")]
        public string ArchiveDepartmentName { get; set; } // ArchiveDepartmentName
		/// <summary>归档日期</summary>	
		[Description("归档日期")]
        public DateTime? ArchiveDate { get; set; } // ArchiveDate

        // Foreign keys
		[JsonIgnore]
        public virtual S_R_PhysicalReorganize S_R_PhysicalReorganize { get; set; } //  S_R_PhysicalReorganizeID - FK_S_R_PhysicalReorganize_NodeDetail_S_R_PhysicalReorganize
    }

	/// <summary>归档清单</summary>	
	[Description("归档清单")]
    public partial class S_R_Reorganize : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary>来源表数据ID</summary>	
		[Description("来源表数据ID")]
        public string BusinessID { get; set; } // BusinessID
		/// <summary>整编人</summary>	
		[Description("整编人")]
        public string ReorganizeUser { get; set; } // ReorganizeUser
		/// <summary>整编人名称</summary>	
		[Description("整编人名称")]
        public string ReorganizeUserName { get; set; } // ReorganizeUserName
		/// <summary>整编日期</summary>	
		[Description("整编日期")]
        public DateTime? ReorganizeDate { get; set; } // ReorganizeDate
		/// <summary>整编状态</summary>	
		[Description("整编状态")]
        public string ReorganizeState { get; set; } // ReorganizeState
		/// <summary>来源表</summary>	
		[Description("来源表")]
        public string BusinessTable { get; set; } // BusinessTable
		/// <summary>业务类型</summary>	
		[Description("业务类型")]
        public string BusinessType { get; set; } // BusinessType
		/// <summary>归档清单名称</summary>	
		[Description("归档清单名称")]
        public string TaskName { get; set; } // TaskName
		/// <summary>送归档人</summary>	
		[Description("送归档人")]
        public string SendUser { get; set; } // SendUser
		/// <summary>送归档人名称</summary>	
		[Description("送归档人名称")]
        public string SendUserName { get; set; } // SendUserName
		/// <summary>送归档日期</summary>	
		[Description("送归档日期")]
        public DateTime? SendDate { get; set; } // SendDate
		/// <summary>空间ID</summary>	
		[Description("空间ID")]
        public string SpaceID { get; set; } // SpaceID
		/// <summary>文件列表</summary>	
		[Description("文件列表")]
        public string DocumentList { get; set; } // DocumentList
		/// <summary>TmplCode</summary>	
		[Description("TmplCode")]
        public string TmplCode { get; set; } // TmplCode
		/// <summary>归档清单编号</summary>	
		[Description("归档清单编号")]
        public string Code { get; set; } // Code
		/// <summary>备注</summary>	
		[Description("备注")]
        public string Remark { get; set; } // Remark
		/// <summary></summary>	
		[Description("")]
        public string SelectNodeIDs { get; set; } // SelectNodeIDs
		/// <summary>归档目录选择</summary>	
		[Description("归档目录选择")]
        public string DefaultNodes { get; set; } // DefaultNodes
		/// <summary>归档目录选择名称</summary>	
		[Description("归档目录选择名称")]
        public string DefaultNodesName { get; set; } // DefaultNodesName
		/// <summary>送归档清单部门</summary>	
		[Description("送归档清单部门")]
        public string SendDepartment { get; set; } // SendDepartment
		/// <summary>送归档清单部门名称</summary>	
		[Description("送归档清单部门名称")]
        public string SendDepartmentName { get; set; } // SendDepartmentName

        // Reverse navigation
		[JsonIgnore]
        public virtual ICollection<S_R_Reorganize_DocumentList> S_R_Reorganize_DocumentList { get { onS_R_Reorganize_DocumentListGetting(); return _S_R_Reorganize_DocumentList;} }
		private ICollection<S_R_Reorganize_DocumentList> _S_R_Reorganize_DocumentList;
		partial void onS_R_Reorganize_DocumentListGetting();


        public S_R_Reorganize()
        {
            _S_R_Reorganize_DocumentList = new List<S_R_Reorganize_DocumentList>();
        }
    }

	/// <summary>文件列表</summary>	
	[Description("文件列表")]
    public partial class S_R_Reorganize_DocumentList : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string S_R_ReorganizeID { get; set; } // S_R_ReorganizeID
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string IsReleased { get; set; } // IsReleased
		/// <summary>文件名称</summary>	
		[Description("文件名称")]
        public string Name { get; set; } // Name
		/// <summary>文件编号</summary>	
		[Description("文件编号")]
        public string Code { get; set; } // Code
		/// <summary>分类</summary>	
		[Description("分类")]
        public string Catagory { get; set; } // Catagory
		/// <summary>所属专业</summary>	
		[Description("所属专业")]
        public string MajorValue { get; set; } // MajorValue
		/// <summary>版次</summary>	
		[Description("版次")]
        public string Version { get; set; } // Version
		/// <summary>文件</summary>	
		[Description("文件")]
        public string MainFile { get; set; } // MainFile
		/// <summary>文件名称</summary>	
		[Description("文件名称")]
        public string MainFileName { get; set; } // MainFileName
		/// <summary>PDF文件</summary>	
		[Description("PDF文件")]
        public string PDFFile { get; set; } // PDFFile
		/// <summary>归档目录</summary>	
		[Description("归档目录")]
        public string ReorganizePath { get; set; } // ReorganizePath
		/// <summary>整编目录ID</summary>	
		[Description("整编目录ID")]
        public string ReorganizeFullID { get; set; } // ReorganizeFullID
		/// <summary>RelateID</summary>	
		[Description("RelateID")]
        public string RelateID { get; set; } // RelateID
		/// <summary>RelateTable</summary>	
		[Description("RelateTable")]
        public string RelateTable { get; set; } // RelateTable
		/// <summary>Plot文件</summary>	
		[Description("Plot文件")]
        public string PlotFile { get; set; } // PlotFile
		/// <summary>Dwf文件</summary>	
		[Description("Dwf文件")]
        public string DwfFile { get; set; } // DwfFile
		/// <summary>Tiff文件</summary>	
		[Description("Tiff文件")]
        public string TiffFile { get; set; } // TiffFile
		/// <summary>签字PDF文件</summary>	
		[Description("签字PDF文件")]
        public string SignPdfFile { get; set; } // SignPdfFile
		/// <summary>引用文件</summary>	
		[Description("引用文件")]
        public string XrefFile { get; set; } // XrefFile
		/// <summary>字体文件</summary>	
		[Description("字体文件")]
        public string FontFile { get; set; } // FontFile
		/// <summary>属性</summary>	
		[Description("属性")]
        public string Attr { get; set; } // Attr
		/// <summary></summary>	
		[Description("")]
        public string ArchiveFileID { get; set; } // ArchiveFileID
		/// <summary></summary>	
		[Description("")]
        public string ArchiveFileAttrs { get; set; } // ArchiveFileAttrs
		/// <summary>页数</summary>	
		[Description("页数")]
        public string PageCount { get; set; } // PageCount
		/// <summary>所属项目</summary>	
		[Description("所属项目")]
        public string ProjectName { get; set; } // ProjectName
		/// <summary>所属项目名称</summary>	
		[Description("所属项目名称")]
        public string ProjectNameName { get; set; } // ProjectNameName
		/// <summary>PDF文件名称</summary>	
		[Description("PDF文件名称")]
        public string PDFFileName { get; set; } // PDFFileName
		/// <summary>附件</summary>	
		[Description("附件")]
        public string Attachments { get; set; } // Attachments
		/// <summary>附件名称</summary>	
		[Description("附件名称")]
        public string AttachmentsName { get; set; } // AttachmentsName
		/// <summary>ReorganizeConfigID</summary>	
		[Description("ReorganizeConfigID")]
        public string ReorganizeConfigID { get; set; } // ReorganizeConfigID
		/// <summary>版次</summary>	
		[Description("版次")]
        public string Edition { get; set; } // Edition
		/// <summary>所属专业</summary>	
		[Description("所属专业")]
        public string Major { get; set; } // Major

        // Foreign keys
		[JsonIgnore]
        public virtual S_R_Reorganize S_R_Reorganize { get; set; } //  S_R_ReorganizeID - FK_S_R_Reorganize_DocumentList_S_R_Reorganize
    }

	/// <summary>解封信息</summary>	
	[Description("解封信息")]
    public partial class S_SU_DocumentRelieveSeal : Formula.BaseModel
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
		/// <summary>解封清单名称</summary>	
		[Description("解封清单名称")]
        public string Name { get; set; } // Name
		/// <summary>解封清单编号</summary>	
		[Description("解封清单编号")]
        public string Code { get; set; } // Code
		/// <summary>解封人</summary>	
		[Description("解封人")]
        public string RelieveSealUser { get; set; } // RelieveSealUser
		/// <summary>解封人名称</summary>	
		[Description("解封人名称")]
        public string RelieveSealUserName { get; set; } // RelieveSealUserName
		/// <summary>解封日期</summary>	
		[Description("解封日期")]
        public DateTime? RelieveSealDate { get; set; } // RelieveSealDate
		/// <summary>解封柜架号</summary>	
		[Description("解封柜架号")]
        public string RelieveSealRackNumber { get; set; } // RelieveSealRackNumber
		/// <summary>解封柜架号名称</summary>	
		[Description("解封柜架号名称")]
        public string RelieveSealRackNumberName { get; set; } // RelieveSealRackNumberName
		/// <summary>解封库房</summary>	
		[Description("解封库房")]
        public string RelieveSealStorageRoom { get; set; } // RelieveSealStorageRoom
		/// <summary>解封库房名称</summary>	
		[Description("解封库房名称")]
        public string RelieveSealStorageRoomName { get; set; } // RelieveSealStorageRoomName
		/// <summary>申请人</summary>	
		[Description("申请人")]
        public string ApplyUser { get; set; } // ApplyUser
		/// <summary>申请人名称</summary>	
		[Description("申请人名称")]
        public string ApplyUserName { get; set; } // ApplyUserName
		/// <summary>申请日期</summary>	
		[Description("申请日期")]
        public DateTime? ApplyDate { get; set; } // ApplyDate

        // Reverse navigation
		[JsonIgnore]
        public virtual ICollection<S_SU_DocumentRelieveSeal_DetailList> S_SU_DocumentRelieveSeal_DetailList { get { onS_SU_DocumentRelieveSeal_DetailListGetting(); return _S_SU_DocumentRelieveSeal_DetailList;} }
		private ICollection<S_SU_DocumentRelieveSeal_DetailList> _S_SU_DocumentRelieveSeal_DetailList;
		partial void onS_SU_DocumentRelieveSeal_DetailListGetting();


        public S_SU_DocumentRelieveSeal()
        {
            _S_SU_DocumentRelieveSeal_DetailList = new List<S_SU_DocumentRelieveSeal_DetailList>();
        }
    }

	/// <summary>解封内容</summary>	
	[Description("解封内容")]
    public partial class S_SU_DocumentRelieveSeal_DetailList : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string S_SU_DocumentRelieveSealID { get; set; } // S_SU_DocumentRelieveSealID
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string IsReleased { get; set; } // IsReleased
		/// <summary>档号</summary>	
		[Description("档号")]
        public string DocumentCode { get; set; } // DocumentCode
		/// <summary>案卷题名</summary>	
		[Description("案卷题名")]
        public string Name { get; set; } // Name
		/// <summary>密级</summary>	
		[Description("密级")]
        public string SecretLevel { get; set; } // SecretLevel
		/// <summary>归档人</summary>	
		[Description("归档人")]
        public string ArchivePeople { get; set; } // ArchivePeople
		/// <summary>归档人名称</summary>	
		[Description("归档人名称")]
        public string ArchivePeopleName { get; set; } // ArchivePeopleName
		/// <summary>归档日期</summary>	
		[Description("归档日期")]
        public DateTime? ArchiveDate { get; set; } // ArchiveDate
		/// <summary>保管期限</summary>	
		[Description("保管期限")]
        public string KeepYear { get; set; } // KeepYear
		/// <summary>份数</summary>	
		[Description("份数")]
        public int? Quantity { get; set; } // Quantity
		/// <summary>卷内实物文件数</summary>	
		[Description("卷内实物文件数")]
        public int? PhysicalFileCount { get; set; } // PhysicalFileCount
		/// <summary>库房</summary>	
		[Description("库房")]
        public string StorageRoomName { get; set; } // StorageRoomName
		/// <summary>柜架号</summary>	
		[Description("柜架号")]
        public string RackNumberName { get; set; } // RackNumberName
		/// <summary>RelateID</summary>	
		[Description("RelateID")]
        public string RelateID { get; set; } // RelateID
		/// <summary>RelateType</summary>	
		[Description("RelateType")]
        public string RelateType { get; set; } // RelateType
		/// <summary>SpaceID</summary>	
		[Description("SpaceID")]
        public string SpaceID { get; set; } // SpaceID

        // Foreign keys
		[JsonIgnore]
        public virtual S_SU_DocumentRelieveSeal S_SU_DocumentRelieveSeal { get; set; } //  S_SU_DocumentRelieveSealID - FK_S_SU_DocumentRelieveSeal_DetailList_S_SU_DocumentRelieveSeal
    }

	/// <summary>封存信息</summary>	
	[Description("封存信息")]
    public partial class S_SU_DocumentSealUp : Formula.BaseModel
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
		/// <summary>封存清单名称</summary>	
		[Description("封存清单名称")]
        public string Name { get; set; } // Name
		/// <summary>封存清单编号</summary>	
		[Description("封存清单编号")]
        public string Code { get; set; } // Code
		/// <summary>封存人</summary>	
		[Description("封存人")]
        public string SealUpUser { get; set; } // SealUpUser
		/// <summary>封存人名称</summary>	
		[Description("封存人名称")]
        public string SealUpUserName { get; set; } // SealUpUserName
		/// <summary>封存日期</summary>	
		[Description("封存日期")]
        public DateTime? SealUpDate { get; set; } // SealUpDate
		/// <summary>封存柜架号</summary>	
		[Description("封存柜架号")]
        public string SealUpRackNumber { get; set; } // SealUpRackNumber
		/// <summary>封存柜架号名称</summary>	
		[Description("封存柜架号名称")]
        public string SealUpRackNumberName { get; set; } // SealUpRackNumberName
		/// <summary>封存库房</summary>	
		[Description("封存库房")]
        public string SealUpStorageRoom { get; set; } // SealUpStorageRoom
		/// <summary>封存库房名称</summary>	
		[Description("封存库房名称")]
        public string SealUpStorageRoomName { get; set; } // SealUpStorageRoomName
		/// <summary>申请人</summary>	
		[Description("申请人")]
        public string ApplyUser { get; set; } // ApplyUser
		/// <summary>申请人名称</summary>	
		[Description("申请人名称")]
        public string ApplyUserName { get; set; } // ApplyUserName
		/// <summary>申请日期</summary>	
		[Description("申请日期")]
        public DateTime? ApplyDate { get; set; } // ApplyDate

        // Reverse navigation
		[JsonIgnore]
        public virtual ICollection<S_SU_DocumentSealUp_DetailList> S_SU_DocumentSealUp_DetailList { get { onS_SU_DocumentSealUp_DetailListGetting(); return _S_SU_DocumentSealUp_DetailList;} }
		private ICollection<S_SU_DocumentSealUp_DetailList> _S_SU_DocumentSealUp_DetailList;
		partial void onS_SU_DocumentSealUp_DetailListGetting();


        public S_SU_DocumentSealUp()
        {
            _S_SU_DocumentSealUp_DetailList = new List<S_SU_DocumentSealUp_DetailList>();
        }
    }

	/// <summary>封存内容</summary>	
	[Description("封存内容")]
    public partial class S_SU_DocumentSealUp_DetailList : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string S_SU_DocumentSealUpID { get; set; } // S_SU_DocumentSealUpID
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string IsReleased { get; set; } // IsReleased
		/// <summary>档号</summary>	
		[Description("档号")]
        public string DocumentCode { get; set; } // DocumentCode
		/// <summary>案卷题名</summary>	
		[Description("案卷题名")]
        public string Name { get; set; } // Name
		/// <summary>密级</summary>	
		[Description("密级")]
        public string SecretLevel { get; set; } // SecretLevel
		/// <summary>归档人</summary>	
		[Description("归档人")]
        public string ArchivePeople { get; set; } // ArchivePeople
		/// <summary>归档人名称</summary>	
		[Description("归档人名称")]
        public string ArchivePeopleName { get; set; } // ArchivePeopleName
		/// <summary>归档日期</summary>	
		[Description("归档日期")]
        public DateTime? ArchiveDate { get; set; } // ArchiveDate
		/// <summary>保管期限</summary>	
		[Description("保管期限")]
        public string KeepYear { get; set; } // KeepYear
		/// <summary>份数</summary>	
		[Description("份数")]
        public int? Quantity { get; set; } // Quantity
		/// <summary>卷内实物文件数</summary>	
		[Description("卷内实物文件数")]
        public int? PhysicalFileCount { get; set; } // PhysicalFileCount
		/// <summary>库房</summary>	
		[Description("库房")]
        public string StorageRoomName { get; set; } // StorageRoomName
		/// <summary>柜架号</summary>	
		[Description("柜架号")]
        public string RackNumberName { get; set; } // RackNumberName
		/// <summary>RelateID</summary>	
		[Description("RelateID")]
        public string RelateID { get; set; } // RelateID
		/// <summary>RelateType</summary>	
		[Description("RelateType")]
        public string RelateType { get; set; } // RelateType
		/// <summary>SpaceID</summary>	
		[Description("SpaceID")]
        public string SpaceID { get; set; } // SpaceID

        // Foreign keys
		[JsonIgnore]
        public virtual S_SU_DocumentSealUp S_SU_DocumentSealUp { get; set; } //  S_SU_DocumentSealUpID - FK_S_SU_DocumentSealUp_DetailList_S_SU_DocumentSealUp
    }

	/// <summary></summary>	
	[Description("")]
    public partial class S_UserAdvanceQueryInfo : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string UserID { get; set; } // UserID
		/// <summary></summary>	
		[Description("")]
        public string QueryData { get; set; } // QueryData
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? ModifyDate { get; set; } // ModifyDate
    }

	/// <summary>借阅申请单</summary>	
	[Description("借阅申请单")]
    public partial class T_B_BorrowApply : Formula.BaseModel
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
		/// <summary>Name</summary>	
		[Description("Name")]
        public string Name { get; set; } // Name
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
		/// <summary>分机号</summary>	
		[Description("分机号")]
        public string ExtensionNumber { get; set; } // ExtensionNumber
		/// <summary>申请日期</summary>	
		[Description("申请日期")]
        public DateTime? ApplyDate { get; set; } // ApplyDate
		/// <summary>用途</summary>	
		[Description("用途")]
        public string Remarks { get; set; } // Remarks
		/// <summary>图档管理员审批</summary>	
		[Description("图档管理员审批")]
        public string DocManagerSign { get; set; } // DocManagerSign

        // Reverse navigation
		[JsonIgnore]
        public virtual ICollection<T_B_BorrowApply_DetailInfo> T_B_BorrowApply_DetailInfo { get { onT_B_BorrowApply_DetailInfoGetting(); return _T_B_BorrowApply_DetailInfo;} }
		private ICollection<T_B_BorrowApply_DetailInfo> _T_B_BorrowApply_DetailInfo;
		partial void onT_B_BorrowApply_DetailInfoGetting();


        public T_B_BorrowApply()
        {
            _T_B_BorrowApply_DetailInfo = new List<T_B_BorrowApply_DetailInfo>();
        }
    }

	/// <summary>借阅清单</summary>	
	[Description("借阅清单")]
    public partial class T_B_BorrowApply_DetailInfo : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string T_B_BorrowApplyID { get; set; } // T_B_BorrowApplyID
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string IsReleased { get; set; } // IsReleased
		/// <summary>SpaceID</summary>	
		[Description("SpaceID")]
        public string SpaceID { get; set; } // SpaceID
		/// <summary>NodeID</summary>	
		[Description("NodeID")]
        public string NodeID { get; set; } // NodeID
		/// <summary>FileID</summary>	
		[Description("FileID")]
        public string FileID { get; set; } // FileID
		/// <summary>CarInfoID</summary>	
		[Description("CarInfoID")]
        public string CarInfoID { get; set; } // CarInfoID
		/// <summary>名称</summary>	
		[Description("名称")]
        public string Name { get; set; } // Name
		/// <summary>编号</summary>	
		[Description("编号")]
        public string Code { get; set; } // Code
		/// <summary>档案门类</summary>	
		[Description("档案门类")]
        public string SpaceName { get; set; } // SpaceName
		/// <summary>类型</summary>	
		[Description("类型")]
        public string DataType { get; set; } // DataType
		/// <summary>申请数量</summary>	
		[Description("申请数量")]
        public int? ApplyAmount { get; set; } // ApplyAmount

        // Foreign keys
		[JsonIgnore]
        public virtual T_B_BorrowApply T_B_BorrowApply { get; set; } //  T_B_BorrowApplyID - FK_T_B_BorrowApply_DetailInfo_T_B_BorrowApply
    }

	/// <summary>T</summary>	
	[Description("T")]
    public partial class T_Borrow : Formula.BaseModel
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
        public string FlowPhase { get; set; } // FlowPhase
		/// <summary></summary>	
		[Description("")]
        public string StepName { get; set; } // StepName
		/// <summary>部门</summary>	
		[Description("部门")]
        public string UserDept { get; set; } // UserDept
		/// <summary>部门名称</summary>	
		[Description("部门名称")]
        public string UserDeptName { get; set; } // UserDeptName
		/// <summary>借阅日期</summary>	
		[Description("借阅日期")]
        public DateTime? LendDate { get; set; } // LendDate
		/// <summary>归还日期</summary>	
		[Description("归还日期")]
        public DateTime? ReturnDate { get; set; } // ReturnDate
		/// <summary>用途</summary>	
		[Description("用途")]
        public string Purpose { get; set; } // Purpose
		/// <summary>分机号</summary>	
		[Description("分机号")]
        public string Phone { get; set; } // Phone
		/// <summary>项目负责人</summary>	
		[Description("项目负责人")]
        public string PrjPrincipleSign { get; set; } // PrjPrincipleSign
		/// <summary>技术质保部负责人</summary>	
		[Description("技术质保部负责人")]
        public string DirecotorSign { get; set; } // DirecotorSign
		/// <summary>领导审批</summary>	
		[Description("领导审批")]
        public string LeaderSign { get; set; } // LeaderSign
		/// <summary>创建人名称</summary>	
		[Description("创建人名称")]
        public string CreateUserName { get; set; } // CreateUserName
		/// <summary>名称</summary>	
		[Description("名称")]
        public string Name { get; set; } // Name
		/// <summary>类型</summary>	
		[Description("类型")]
        public string Type { get; set; } // Type
		/// <summary>申请日期</summary>	
		[Description("申请日期")]
        public DateTime? ApplyDate { get; set; } // ApplyDate
		/// <summary>申请通过日期</summary>	
		[Description("申请通过日期")]
        public DateTime? PassDate { get; set; } // PassDate
		/// <summary>流程ID</summary>	
		[Description("流程ID")]
        public string FlowID { get; set; } // FlowID
		/// <summary>借阅状态</summary>	
		[Description("借阅状态")]
        public string BorrowState { get; set; } // BorrowState
		/// <summary>用途详细说明(暂未用到可删除)</summary>	
		[Description("用途详细说明(暂未用到可删除)")]
        public string PurposeDetail { get; set; } // PurposeDetail
		/// <summary>项目负责人ID</summary>	
		[Description("项目负责人ID")]
        public string PrjPrincipleID { get; set; } // PrjPrincipleID
		/// <summary>项目负责人Name</summary>	
		[Description("项目负责人Name")]
        public string PrjPrincipleName { get; set; } // PrjPrincipleName
		/// <summary>项目负责人签字日期</summary>	
		[Description("项目负责人签字日期")]
        public string PrjPrincipleDate { get; set; } // PrjPrincipleDate
		/// <summary>技术质保部负责人ID</summary>	
		[Description("技术质保部负责人ID")]
        public string DirecotorID { get; set; } // DirecotorID
		/// <summary>技术质保部负责人Name</summary>	
		[Description("技术质保部负责人Name")]
        public string DirecotorName { get; set; } // DirecotorName
		/// <summary>技术质保部负责人签字日期</summary>	
		[Description("技术质保部负责人签字日期")]
        public string DirecotorDate { get; set; } // DirecotorDate
		/// <summary>领导审批ID</summary>	
		[Description("领导审批ID")]
        public string LeaderID { get; set; } // LeaderID
		/// <summary>领导审批Name</summary>	
		[Description("领导审批Name")]
        public string LeaderName { get; set; } // LeaderName
		/// <summary>领导审批通过日期</summary>	
		[Description("领导审批通过日期")]
        public string LeaderDate { get; set; } // LeaderDate
		/// <summary>SpaceID</summary>	
		[Description("SpaceID")]
        public string SpaceID { get; set; } // SpaceID

        // Reverse navigation
		[JsonIgnore]
        public virtual ICollection<T_Borrow_FileInfo> T_Borrow_FileInfo { get { onT_Borrow_FileInfoGetting(); return _T_Borrow_FileInfo;} }
		private ICollection<T_Borrow_FileInfo> _T_Borrow_FileInfo;
		partial void onT_Borrow_FileInfoGetting();


        public T_Borrow()
        {
            _T_Borrow_FileInfo = new List<T_Borrow_FileInfo>();
        }
    }

	/// <summary>子表</summary>	
	[Description("子表")]
    public partial class T_Borrow_FileInfo : Formula.BaseModel
    {
		/// <summary>ID</summary>	
		[Description("ID")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary>T_BorrowID</summary>	
		[Description("T_BorrowID")]
        public string T_BorrowID { get; set; } // T_BorrowID
		/// <summary>排序号</summary>	
		[Description("排序号")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary>已发布</summary>	
		[Description("已发布")]
        public string IsReleased { get; set; } // IsReleased
		/// <summary>案卷编号</summary>	
		[Description("案卷编号")]
        public string FileCode { get; set; } // FileCode
		/// <summary>案卷名称</summary>	
		[Description("案卷名称")]
        public string FileName { get; set; } // FileName
		/// <summary>FileID</summary>	
		[Description("FileID")]
        public string FileID { get; set; } // FileID
		/// <summary>CarInfoID</summary>	
		[Description("CarInfoID")]
        public string CarInfoID { get; set; } // CarInfoID
		/// <summary>ConfigID</summary>	
		[Description("ConfigID")]
        public string ConfigID { get; set; } // ConfigID
		/// <summary>NodeID</summary>	
		[Description("NodeID")]
        public string NodeID { get; set; } // NodeID
		/// <summary>SpaceID</summary>	
		[Description("SpaceID")]
        public string SpaceID { get; set; } // SpaceID

        // Foreign keys
		[JsonIgnore]
        public virtual T_Borrow T_Borrow { get; set; } //  T_BorrowID - FK_T_Borrow_FileInfo_T_Borrow
    }

	/// <summary>下载申请单</summary>	
	[Description("下载申请单")]
    public partial class T_D_DownloadApply : Formula.BaseModel
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
		/// <summary>Name</summary>	
		[Description("Name")]
        public string Name { get; set; } // Name
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
		/// <summary>分机号</summary>	
		[Description("分机号")]
        public string ExtensionNumber { get; set; } // ExtensionNumber
		/// <summary>申请日期</summary>	
		[Description("申请日期")]
        public DateTime? ApplyDate { get; set; } // ApplyDate
		/// <summary>用途</summary>	
		[Description("用途")]
        public string Remarks { get; set; } // Remarks
		/// <summary>图档管理员审批</summary>	
		[Description("图档管理员审批")]
        public string DocManagerSign { get; set; } // DocManagerSign

        // Reverse navigation
		[JsonIgnore]
        public virtual ICollection<T_D_DownloadApply_DetailInfo> T_D_DownloadApply_DetailInfo { get { onT_D_DownloadApply_DetailInfoGetting(); return _T_D_DownloadApply_DetailInfo;} }
		private ICollection<T_D_DownloadApply_DetailInfo> _T_D_DownloadApply_DetailInfo;
		partial void onT_D_DownloadApply_DetailInfoGetting();


        public T_D_DownloadApply()
        {
            _T_D_DownloadApply_DetailInfo = new List<T_D_DownloadApply_DetailInfo>();
        }
    }

	/// <summary>下载清单</summary>	
	[Description("下载清单")]
    public partial class T_D_DownloadApply_DetailInfo : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string T_D_DownloadApplyID { get; set; } // T_D_DownloadApplyID
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
		/// <summary>档案门类</summary>	
		[Description("档案门类")]
        public string SpaceName { get; set; } // SpaceName
		/// <summary>类型</summary>	
		[Description("类型")]
        public string DataType { get; set; } // DataType
		/// <summary>CarInfoID</summary>	
		[Description("CarInfoID")]
        public string CarInfoID { get; set; } // CarInfoID

        // Foreign keys
		[JsonIgnore]
        public virtual T_D_DownloadApply T_D_DownloadApply { get; set; } //  T_D_DownloadApplyID - FK_T_D_DownloadApply_DetailInfo_T_D_DownloadApply
    }

	/// <summary>T</summary>	
	[Description("T")]
    public partial class T_Download : Formula.BaseModel
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
        public string FlowPhase { get; set; } // FlowPhase
		/// <summary></summary>	
		[Description("")]
        public string StepName { get; set; } // StepName
		/// <summary>部门</summary>	
		[Description("部门")]
        public string UserDept { get; set; } // UserDept
		/// <summary>部门名称</summary>	
		[Description("部门名称")]
        public string UserDeptName { get; set; } // UserDeptName
		/// <summary>用途</summary>	
		[Description("用途")]
        public string Purpose { get; set; } // Purpose
		/// <summary>创建人名称</summary>	
		[Description("创建人名称")]
        public string CreateUserName { get; set; } // CreateUserName
		/// <summary>名称</summary>	
		[Description("名称")]
        public string Name { get; set; } // Name
		/// <summary>类型</summary>	
		[Description("类型")]
        public string Type { get; set; } // Type
		/// <summary>申请日期</summary>	
		[Description("申请日期")]
        public DateTime? ApplyDate { get; set; } // ApplyDate
		/// <summary>申请通过日期</summary>	
		[Description("申请通过日期")]
        public DateTime? PassDate { get; set; } // PassDate
		/// <summary>下载状态</summary>	
		[Description("下载状态")]
        public string DownloadState { get; set; } // DownloadState
		/// <summary>SpaceID</summary>	
		[Description("SpaceID")]
        public string SpaceID { get; set; } // SpaceID
		/// <summary>流程ID</summary>	
		[Description("流程ID")]
        public string FlowID { get; set; } // FlowID

        // Reverse navigation
		[JsonIgnore]
        public virtual ICollection<T_Download_FileInfo> T_Download_FileInfo { get { onT_Download_FileInfoGetting(); return _T_Download_FileInfo;} }
		private ICollection<T_Download_FileInfo> _T_Download_FileInfo;
		partial void onT_Download_FileInfoGetting();


        public T_Download()
        {
            _T_Download_FileInfo = new List<T_Download_FileInfo>();
        }
    }

	/// <summary>子表</summary>	
	[Description("子表")]
    public partial class T_Download_FileInfo : Formula.BaseModel
    {
		/// <summary>ID</summary>	
		[Description("ID")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary>T_DownloadID</summary>	
		[Description("T_DownloadID")]
        public string T_DownloadID { get; set; } // T_DownloadID
		/// <summary>排序号</summary>	
		[Description("排序号")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary>已发布</summary>	
		[Description("已发布")]
        public string IsReleased { get; set; } // IsReleased
		/// <summary>文件编号</summary>	
		[Description("文件编号")]
        public string FileCode { get; set; } // FileCode
		/// <summary>文件名称</summary>	
		[Description("文件名称")]
        public string FileName { get; set; } // FileName
		/// <summary>FileID</summary>	
		[Description("FileID")]
        public string FileID { get; set; } // FileID
		/// <summary>CarInfoID</summary>	
		[Description("CarInfoID")]
        public string CarInfoID { get; set; } // CarInfoID
		/// <summary>ConfigID</summary>	
		[Description("ConfigID")]
        public string ConfigID { get; set; } // ConfigID
		/// <summary>日期</summary>	
		[Description("日期")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary>Attachments</summary>	
		[Description("Attachments")]
        public string Attachments { get; set; } // Attachments
		/// <summary>NodeID</summary>	
		[Description("NodeID")]
        public string NodeID { get; set; } // NodeID
		/// <summary>类型</summary>	
		[Description("类型")]
        public string DataType { get; set; } // DataType
		/// <summary>SpaceID</summary>	
		[Description("SpaceID")]
        public string SpaceID { get; set; } // SpaceID

        // Foreign keys
		[JsonIgnore]
        public virtual T_Download T_Download { get; set; } //  T_DownloadID - FK_T_Download_FileInfo_T_Download
    }

	/// <summary>遗失损毁</summary>	
	[Description("遗失损毁")]
    public partial class T_Lose : Formula.BaseModel
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
		/// <summary>遗失损毁人</summary>	
		[Description("遗失损毁人")]
        public string LosePeople { get; set; } // LosePeople
		/// <summary>遗失损毁人名称</summary>	
		[Description("遗失损毁人名称")]
        public string LosePeopleName { get; set; } // LosePeopleName
		/// <summary>遗失损毁部门</summary>	
		[Description("遗失损毁部门")]
        public string LoseDept { get; set; } // LoseDept
		/// <summary>遗失损毁部门名称</summary>	
		[Description("遗失损毁部门名称")]
        public string LoseDeptName { get; set; } // LoseDeptName
		/// <summary>遗失损毁表</summary>	
		[Description("遗失损毁表")]
        public string LostDetail { get; set; } // LostDetail

        // Reverse navigation
		[JsonIgnore]
        public virtual ICollection<T_Lose_LostDetail> T_Lose_LostDetail { get { onT_Lose_LostDetailGetting(); return _T_Lose_LostDetail;} }
		private ICollection<T_Lose_LostDetail> _T_Lose_LostDetail;
		partial void onT_Lose_LostDetailGetting();


        public T_Lose()
        {
            _T_Lose_LostDetail = new List<T_Lose_LostDetail>();
        }
    }

	/// <summary>遗失损毁表</summary>	
	[Description("遗失损毁表")]
    public partial class T_Lose_LostDetail : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string T_LoseID { get; set; } // T_LoseID
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string IsReleased { get; set; } // IsReleased
		/// <summary>遗失损毁状态</summary>	
		[Description("遗失损毁状态")]
        public string LoseDamageState { get; set; } // LoseDamageState
		/// <summary>名称</summary>	
		[Description("名称")]
        public string Name { get; set; } // Name
		/// <summary>类型</summary>	
		[Description("类型")]
        public string ConfigID { get; set; } // ConfigID
		/// <summary>档案门类</summary>	
		[Description("档案门类")]
        public string SpaceID { get; set; } // SpaceID
		/// <summary>份数</summary>	
		[Description("份数")]
        public int? Quantity { get; set; } // Quantity
		/// <summary>库存</summary>	
		[Description("库存")]
        public string StorageNum { get; set; } // StorageNum
		/// <summary>遗失日期</summary>	
		[Description("遗失日期")]
        public DateTime? LoseDate { get; set; } // LoseDate
		/// <summary>遗失说明</summary>	
		[Description("遗失说明")]
        public string LoseExplain { get; set; } // LoseExplain
		/// <summary>RelateDocID</summary>	
		[Description("RelateDocID")]
        public string RelateDocID { get; set; } // RelateDocID
		/// <summary>RelateDocType</summary>	
		[Description("RelateDocType")]
        public string RelateDocType { get; set; } // RelateDocType
		/// <summary>遗失损毁份数</summary>	
		[Description("遗失损毁份数")]
        public string LoseCount { get; set; } // LoseCount
		/// <summary>档号</summary>	
		[Description("档号")]
        public string DocumentCode { get; set; } // DocumentCode

        // Foreign keys
		[JsonIgnore]
        public virtual T_Lose T_Lose { get; set; } //  T_LoseID - FK_T_Lose_LostDetail_T_Lose
    }

	/// <summary>补录</summary>	
	[Description("补录")]
    public partial class T_Replenish : Formula.BaseModel
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
		/// <summary>补录人</summary>	
		[Description("补录人")]
        public string ReplenishPeople { get; set; } // ReplenishPeople
		/// <summary>补录人名称</summary>	
		[Description("补录人名称")]
        public string ReplenishPeopleName { get; set; } // ReplenishPeopleName
		/// <summary>补录部门</summary>	
		[Description("补录部门")]
        public string ReplenishDept { get; set; } // ReplenishDept
		/// <summary>补录部门名称</summary>	
		[Description("补录部门名称")]
        public string ReplenishDeptName { get; set; } // ReplenishDeptName
		/// <summary>补录表</summary>	
		[Description("补录表")]
        public string Detail { get; set; } // Detail

        // Reverse navigation
		[JsonIgnore]
        public virtual ICollection<T_Replenish_Detail> T_Replenish_Detail { get { onT_Replenish_DetailGetting(); return _T_Replenish_Detail;} }
		private ICollection<T_Replenish_Detail> _T_Replenish_Detail;
		partial void onT_Replenish_DetailGetting();


        public T_Replenish()
        {
            _T_Replenish_Detail = new List<T_Replenish_Detail>();
        }
    }

	/// <summary>补录表</summary>	
	[Description("补录表")]
    public partial class T_Replenish_Detail : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string T_ReplenishID { get; set; } // T_ReplenishID
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string IsReleased { get; set; } // IsReleased
		/// <summary>补录日期</summary>	
		[Description("补录日期")]
        public DateTime? ReplenishDate { get; set; } // ReplenishDate
		/// <summary>补录说明</summary>	
		[Description("补录说明")]
        public string ReplenishExplain { get; set; } // ReplenishExplain
		/// <summary>S_A_LoseReplenishID</summary>	
		[Description("S_A_LoseReplenishID")]
        public string S_A_LoseReplenishID { get; set; } // S_A_LoseReplenishID
		/// <summary>名称</summary>	
		[Description("名称")]
        public string Name { get; set; } // Name
		/// <summary>类型</summary>	
		[Description("类型")]
        public string ConfigID { get; set; } // ConfigID
		/// <summary>档案门类</summary>	
		[Description("档案门类")]
        public string SpaceID { get; set; } // SpaceID
		/// <summary>遗失日期</summary>	
		[Description("遗失日期")]
        public DateTime? LoseDate { get; set; } // LoseDate
		/// <summary>遗失说明</summary>	
		[Description("遗失说明")]
        public string LoseExplain { get; set; } // LoseExplain
		/// <summary>遗失损毁人</summary>	
		[Description("遗失损毁人")]
        public string LosePeople { get; set; } // LosePeople
		/// <summary>遗失损毁人名称</summary>	
		[Description("遗失损毁人名称")]
        public string LosePeopleName { get; set; } // LosePeopleName
		/// <summary>遗失损毁部门</summary>	
		[Description("遗失损毁部门")]
        public string LoseDept { get; set; } // LoseDept
		/// <summary>遗失损毁部门名称</summary>	
		[Description("遗失损毁部门名称")]
        public string LoseDeptName { get; set; } // LoseDeptName
		/// <summary>补录状态</summary>	
		[Description("补录状态")]
        public string ReplenishState { get; set; } // ReplenishState
		/// <summary>RelateDocID</summary>	
		[Description("RelateDocID")]
        public string RelateDocID { get; set; } // RelateDocID
		/// <summary>RelateDocType</summary>	
		[Description("RelateDocType")]
        public string RelateDocType { get; set; } // RelateDocType
		/// <summary>遗失损毁份数</summary>	
		[Description("遗失损毁份数")]
        public string LoseCount { get; set; } // LoseCount
		/// <summary>补录份数</summary>	
		[Description("补录份数")]
        public int? ReplenishCount { get; set; } // ReplenishCount
		/// <summary>状态</summary>	
		[Description("状态")]
        public string State { get; set; } // State
		/// <summary>档号</summary>	
		[Description("档号")]
        public string DocumentCode { get; set; } // DocumentCode

        // Foreign keys
		[JsonIgnore]
        public virtual T_Replenish T_Replenish { get; set; } //  T_ReplenishID - FK_T_Replenish_Detail_T_Replenish
    }


    // ************************************************************************
    // POCO Configuration

    // S_A_AuthoritySetting
    internal partial class S_A_AuthoritySettingConfiguration : EntityTypeConfiguration<S_A_AuthoritySetting>
    {
        public S_A_AuthoritySettingConfiguration()
        {
            ToTable("S_A_AUTHORITYSETTING");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.SpaceID).HasColumnName("SPACEID").IsRequired().HasMaxLength(50);
            Property(x => x.SecretLevel).HasColumnName("SECRETLEVEL").IsRequired().HasMaxLength(50);
            Property(x => x.AuthType).HasColumnName("AUTHTYPE").IsRequired().HasMaxLength(50);
            Property(x => x.RoleType).HasColumnName("ROLETYPE").IsRequired().HasMaxLength(50);
            Property(x => x.RoleCode).HasColumnName("ROLECODE").IsRequired().HasMaxLength(50);
        }
    }

    // S_A_InventoryLedger
    internal partial class S_A_InventoryLedgerConfiguration : EntityTypeConfiguration<S_A_InventoryLedger>
    {
        public S_A_InventoryLedgerConfiguration()
        {
            ToTable("S_A_INVENTORYLEDGER");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.SpaceID).HasColumnName("SPACEID").IsRequired().HasMaxLength(50);
            Property(x => x.RelateID).HasColumnName("RELATEID").IsRequired().HasMaxLength(50);
            Property(x => x.RelateName).HasColumnName("RELATENAME").IsRequired().HasMaxLength(200);
            Property(x => x.RelateType).HasColumnName("RELATETYPE").IsRequired().HasMaxLength(50);
            Property(x => x.CarItemName).HasColumnName("CARITEMNAME").IsOptional().HasMaxLength(500);
            Property(x => x.RelateDetailInfoID).HasColumnName("RELATEDETAILINFOID").IsOptional().HasMaxLength(50);
            Property(x => x.Type).HasColumnName("TYPE").IsRequired().HasMaxLength(50);
            Property(x => x.TotalAmount).HasColumnName("TOTALAMOUNT").IsRequired();
            Property(x => x.InventoryAmount).HasColumnName("INVENTORYAMOUNT").IsRequired();
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsRequired();
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsRequired().HasMaxLength(50);
            Property(x => x.CreateUserName).HasColumnName("CREATEUSERNAME").IsRequired().HasMaxLength(50);
            Property(x => x.TargetUserID).HasColumnName("TARGETUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.TargetUserName).HasColumnName("TARGETUSERNAME").IsOptional().HasMaxLength(50);
            Property(x => x.Detail).HasColumnName("DETAIL").IsOptional().HasMaxLength(500);
            Property(x => x.CreateDept).HasColumnName("CREATEDEPT").IsOptional().HasMaxLength(200);
        }
    }

    // S_A_LoseReplenish
    internal partial class S_A_LoseReplenishConfiguration : EntityTypeConfiguration<S_A_LoseReplenish>
    {
        public S_A_LoseReplenishConfiguration()
        {
            ToTable("S_A_LOSEREPLENISH");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.RelateDocID).HasColumnName("RELATEDOCID").IsOptional().HasMaxLength(50);
            Property(x => x.State).HasColumnName("STATE").IsOptional().HasMaxLength(50);
            Property(x => x.LoseCount).HasColumnName("LOSECOUNT").IsOptional();
            Property(x => x.ReplenishCount).HasColumnName("REPLENISHCOUNT").IsOptional();
            Property(x => x.ReplenishState).HasColumnName("REPLENISHSTATE").IsOptional().HasMaxLength(50);
            Property(x => x.LosePeople).HasColumnName("LOSEPEOPLE").IsOptional().HasMaxLength(200);
            Property(x => x.LosePeopleName).HasColumnName("LOSEPEOPLENAME").IsOptional().HasMaxLength(200);
            Property(x => x.LoseDept).HasColumnName("LOSEDEPT").IsOptional().HasMaxLength(200);
            Property(x => x.LoseDeptName).HasColumnName("LOSEDEPTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(200);
            Property(x => x.ConfigID).HasColumnName("CONFIGID").IsOptional().HasMaxLength(200);
            Property(x => x.SpaceID).HasColumnName("SPACEID").IsOptional().HasMaxLength(200);
            Property(x => x.Quantity).HasColumnName("QUANTITY").IsOptional();
            Property(x => x.StorageNum).HasColumnName("STORAGENUM").IsOptional().HasMaxLength(200);
            Property(x => x.LoseDate).HasColumnName("LOSEDATE").IsOptional();
            Property(x => x.LoseExplain).HasColumnName("LOSEEXPLAIN").IsOptional().HasMaxLength(200);
            Property(x => x.ReplenishDept).HasColumnName("REPLENISHDEPT").IsOptional().HasMaxLength(200);
            Property(x => x.ReplenishDeptName).HasColumnName("REPLENISHDEPTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ReplenishPeople).HasColumnName("REPLENISHPEOPLE").IsOptional().HasMaxLength(200);
            Property(x => x.ReplenishPeopleName).HasColumnName("REPLENISHPEOPLENAME").IsOptional().HasMaxLength(200);
            Property(x => x.ReplenishDate).HasColumnName("REPLENISHDATE").IsOptional();
            Property(x => x.ReplenishExplain).HasColumnName("REPLENISHEXPLAIN").IsOptional().HasMaxLength(200);
            Property(x => x.RelateDocType).HasColumnName("RELATEDOCTYPE").IsOptional().HasMaxLength(50);
            Property(x => x.DocumentCode).HasColumnName("DOCUMENTCODE").IsOptional().HasMaxLength(200);
        }
    }

    // S_A_StorageRoom
    internal partial class S_A_StorageRoomConfiguration : EntityTypeConfiguration<S_A_StorageRoom>
    {
        public S_A_StorageRoomConfiguration()
        {
            ToTable("S_A_STORAGEROOM");
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
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(200);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(200);
            Property(x => x.Location).HasColumnName("LOCATION").IsOptional().HasMaxLength(200);
            Property(x => x.Description).HasColumnName("DESCRIPTION").IsOptional().HasMaxLength(500);
            Property(x => x.Capacitance).HasColumnName("CAPACITANCE").IsOptional().HasMaxLength(200);
            Property(x => x.ChargeUser).HasColumnName("CHARGEUSER").IsOptional().HasMaxLength(200);
            Property(x => x.ChargeUserName).HasColumnName("CHARGEUSERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Remarks).HasColumnName("REMARKS").IsOptional().HasMaxLength(500);
            Property(x => x.CabinetDetail).HasColumnName("CABINETDETAIL").IsOptional().HasMaxLength(1073741823);
        }
    }

    // S_A_StorageRoom_CabinetDetail
    internal partial class S_A_StorageRoom_CabinetDetailConfiguration : EntityTypeConfiguration<S_A_StorageRoom_CabinetDetail>
    {
        public S_A_StorageRoom_CabinetDetailConfiguration()
        {
            ToTable("S_A_STORAGEROOM_CABINETDETAIL");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.S_A_StorageRoomID).HasColumnName("S_A_STORAGEROOMID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(200);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(200);
            Property(x => x.Area).HasColumnName("AREA").IsOptional().HasMaxLength(200);
            Property(x => x.Description).HasColumnName("DESCRIPTION").IsOptional().HasMaxLength(200);
            Property(x => x.Capacitance).HasColumnName("CAPACITANCE").IsOptional().HasMaxLength(200);
            Property(x => x.ChargeUser).HasColumnName("CHARGEUSER").IsOptional().HasMaxLength(200);
            Property(x => x.ChargeUserName).HasColumnName("CHARGEUSERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Remarks).HasColumnName("REMARKS").IsOptional().HasMaxLength(200);

            // Foreign keys
            HasOptional(a => a.S_A_StorageRoom).WithMany(b => b.S_A_StorageRoom_CabinetDetail).HasForeignKey(c => c.S_A_StorageRoomID); // FK_S_A_StorageRoom_CabinetDetail_S_A_StorageRoom
        }
    }

    // S_ArchiveCache
    internal partial class S_ArchiveCacheConfiguration : EntityTypeConfiguration<S_ArchiveCache>
    {
        public S_ArchiveCacheConfiguration()
        {
            ToTable("S_ARCHIVECACHE");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.SpaceID).HasColumnName("SPACEID").IsRequired().HasMaxLength(50);
            Property(x => x.ConfigID).HasColumnName("CONFIGID").IsRequired().HasMaxLength(50);
            Property(x => x.FileType).HasColumnName("FILETYPE").IsOptional().HasMaxLength(50);
            Property(x => x.Name).HasColumnName("NAME").IsRequired().HasMaxLength(500);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(500);
            Property(x => x.MainFile).HasColumnName("MAINFILE").IsOptional().HasMaxLength(500);
            Property(x => x.PdfFile).HasColumnName("PDFFILE").IsOptional().HasMaxLength(500);
            Property(x => x.PrintFile).HasColumnName("PRINTFILE").IsOptional().HasMaxLength(500);
            Property(x => x.Attachment).HasColumnName("ATTACHMENT").IsOptional();
            Property(x => x.Data).HasColumnName("DATA").IsOptional();
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsRequired();
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsRequired().HasMaxLength(50);
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsRequired().HasMaxLength(50);
            Property(x => x.ArchiveDate).HasColumnName("ARCHIVEDATE").IsRequired();
            Property(x => x.State).HasColumnName("STATE").IsRequired().HasMaxLength(50);
            Property(x => x.CatalogID).HasColumnName("CATALOGID").IsRequired().HasMaxLength(50);
            Property(x => x.FullCatalogID).HasColumnName("FULLCATALOGID").IsRequired().HasMaxLength(500);
            Property(x => x.SN).HasColumnName("SN").IsOptional().HasMaxLength(50);
            Property(x => x.SNFlag).HasColumnName("SNFLAG").IsOptional().HasMaxLength(50);

            // Foreign keys
            HasRequired(a => a.S_ArchiveCacheCatalog).WithMany(b => b.S_ArchiveCache).HasForeignKey(c => c.CatalogID); // FK_S_ArchiveCache_S_ArchiveCacheCatalog
        }
    }

    // S_ArchiveCacheCatalog
    internal partial class S_ArchiveCacheCatalogConfiguration : EntityTypeConfiguration<S_ArchiveCacheCatalog>
    {
        public S_ArchiveCacheCatalogConfiguration()
        {
            ToTable("S_ARCHIVECACHECATALOG");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.SpaceID).HasColumnName("SPACEID").IsRequired().HasMaxLength(50);
            Property(x => x.ConfigID).HasColumnName("CONFIGID").IsOptional().HasMaxLength(50);
            Property(x => x.Name).HasColumnName("NAME").IsRequired().HasMaxLength(500);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(500);
            Property(x => x.ParentID).HasColumnName("PARENTID").IsOptional().HasMaxLength(50);
            Property(x => x.Type).HasColumnName("TYPE").IsRequired().HasMaxLength(50);
            Property(x => x.FullID).HasColumnName("FULLID").IsRequired().HasMaxLength(500);
            Property(x => x.Data).HasColumnName("DATA").IsOptional();
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsRequired();
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsRequired().HasMaxLength(50);
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsRequired().HasMaxLength(50);
            Property(x => x.State).HasColumnName("STATE").IsRequired().HasMaxLength(50);
            Property(x => x.RelateID).HasColumnName("RELATEID").IsOptional().HasMaxLength(50);
            Property(x => x.ArchiveFormID).HasColumnName("ARCHIVEFORMID").IsOptional().HasMaxLength(50);
        }
    }

    // S_BorrowDetail
    internal partial class S_BorrowDetailConfiguration : EntityTypeConfiguration<S_BorrowDetail>
    {
        public S_BorrowDetailConfiguration()
        {
            ToTable("S_BORROWDETAIL");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.BorrowID).HasColumnName("BORROWID").IsOptional().HasMaxLength(50);
            Property(x => x.SpaceID).HasColumnName("SPACEID").IsRequired().HasMaxLength(50);
            Property(x => x.SpaceName).HasColumnName("SPACENAME").IsOptional().HasMaxLength(50);
            Property(x => x.DataType).HasColumnName("DATATYPE").IsOptional().HasMaxLength(50);
            Property(x => x.DetailType).HasColumnName("DETAILTYPE").IsRequired().HasMaxLength(50);
            Property(x => x.RelateID).HasColumnName("RELATEID").IsRequired().HasMaxLength(50);
            Property(x => x.ConfigID).HasColumnName("CONFIGID").IsRequired().HasMaxLength(50);
            Property(x => x.Name).HasColumnName("NAME").IsRequired().HasMaxLength(500);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(500);
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUserName).HasColumnName("CREATEUSERNAME").IsOptional().HasMaxLength(50);
            Property(x => x.CreateDeptID).HasColumnName("CREATEDEPTID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateDeptName).HasColumnName("CREATEDEPTNAME").IsOptional().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.LendUserID).HasColumnName("LENDUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.LendUserName).HasColumnName("LENDUSERNAME").IsOptional().HasMaxLength(50);
            Property(x => x.LendDeptID).HasColumnName("LENDDEPTID").IsOptional().HasMaxLength(50);
            Property(x => x.LendDeptName).HasColumnName("LENDDEPTNAME").IsOptional().HasMaxLength(50);
            Property(x => x.LendDate).HasColumnName("LENDDATE").IsOptional();
            Property(x => x.ReturnUserID).HasColumnName("RETURNUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ReturnUser).HasColumnName("RETURNUSER").IsOptional().HasMaxLength(50);
            Property(x => x.ReturnDeptID).HasColumnName("RETURNDEPTID").IsOptional().HasMaxLength(50);
            Property(x => x.ReturnDeptName).HasColumnName("RETURNDEPTNAME").IsOptional().HasMaxLength(50);
            Property(x => x.ReturnDate).HasColumnName("RETURNDATE").IsOptional();
            Property(x => x.LostUserID).HasColumnName("LOSTUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.LostUserName).HasColumnName("LOSTUSERNAME").IsOptional().HasMaxLength(50);
            Property(x => x.LostDeptID).HasColumnName("LOSTDEPTID").IsOptional().HasMaxLength(50);
            Property(x => x.LostDeptName).HasColumnName("LOSTDEPTNAME").IsOptional().HasMaxLength(50);
            Property(x => x.LostDate).HasColumnName("LOSTDATE").IsOptional();
            Property(x => x.BorrowExpireDate).HasColumnName("BORROWEXPIREDATE").IsOptional();
            Property(x => x.ApplyNumber).HasColumnName("APPLYNUMBER").IsOptional();
            Property(x => x.LendNumber).HasColumnName("LENDNUMBER").IsOptional();
            Property(x => x.ReturnNumber).HasColumnName("RETURNNUMBER").IsOptional();
            Property(x => x.LostNumber).HasColumnName("LOSTNUMBER").IsOptional();
            Property(x => x.LostRemarks).HasColumnName("LOSTREMARKS").IsOptional().HasMaxLength(500);
            Property(x => x.BorrowState).HasColumnName("BORROWSTATE").IsRequired().HasMaxLength(50);
            Property(x => x.ParentID).HasColumnName("PARENTID").IsOptional().HasMaxLength(50);
            Property(x => x.HasChild).HasColumnName("HASCHILD").IsOptional().HasMaxLength(50);
            Property(x => x.ContinuedTimes).HasColumnName("CONTINUEDTIMES").IsOptional();
        }
    }

    // S_CarInfo
    internal partial class S_CarInfoConfiguration : EntityTypeConfiguration<S_CarInfo>
    {
        public S_CarInfoConfiguration()
        {
            ToTable("S_CARINFO");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.UserID).HasColumnName("USERID").IsRequired().HasMaxLength(50);
            Property(x => x.UserName).HasColumnName("USERNAME").IsRequired().HasMaxLength(50);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(500);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(500);
            Property(x => x.DataType).HasColumnName("DATATYPE").IsOptional().HasMaxLength(500);
            Property(x => x.Type).HasColumnName("TYPE").IsRequired().HasMaxLength(50);
            Property(x => x.FileID).HasColumnName("FILEID").IsOptional().HasMaxLength(500);
            Property(x => x.NodeID).HasColumnName("NODEID").IsOptional().HasMaxLength(500);
            Property(x => x.NodeName).HasColumnName("NODENAME").IsOptional().HasMaxLength(500);
            Property(x => x.State).HasColumnName("STATE").IsRequired().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsRequired();
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsRequired().HasMaxLength(50);
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsRequired().HasMaxLength(50);
            Property(x => x.ConfigID).HasColumnName("CONFIGID").IsOptional().HasMaxLength(50);
            Property(x => x.SpaceID).HasColumnName("SPACEID").IsOptional().HasMaxLength(50);
            Property(x => x.Attachments).HasColumnName("ATTACHMENTS").IsOptional();
        }
    }

    // S_DocumentLog
    internal partial class S_DocumentLogConfiguration : EntityTypeConfiguration<S_DocumentLog>
    {
        public S_DocumentLogConfiguration()
        {
            ToTable("S_DOCUMENTLOG");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.FileID).HasColumnName("FILEID").IsOptional().HasMaxLength(50);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(500);
            Property(x => x.NodeID).HasColumnName("NODEID").IsOptional().HasMaxLength(50);
            Property(x => x.SpaceID).HasColumnName("SPACEID").IsOptional().HasMaxLength(50);
            Property(x => x.ConfigID).HasColumnName("CONFIGID").IsOptional().HasMaxLength(50);
            Property(x => x.FullNodeID).HasColumnName("FULLNODEID").IsOptional().HasMaxLength(500);
            Property(x => x.LogType).HasColumnName("LOGTYPE").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUserName).HasColumnName("CREATEUSERNAME").IsOptional().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
        }
    }

    // S_DownloadDetail
    internal partial class S_DownloadDetailConfiguration : EntityTypeConfiguration<S_DownloadDetail>
    {
        public S_DownloadDetailConfiguration()
        {
            ToTable("S_DOWNLOADDETAIL");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.DownloadID).HasColumnName("DOWNLOADID").IsRequired().HasMaxLength(50);
            Property(x => x.Name).HasColumnName("NAME").IsRequired().HasMaxLength(500);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(500);
            Property(x => x.SpaceID).HasColumnName("SPACEID").IsRequired().HasMaxLength(50);
            Property(x => x.ConfigID).HasColumnName("CONFIGID").IsRequired().HasMaxLength(50);
            Property(x => x.FileID).HasColumnName("FILEID").IsRequired().HasMaxLength(50);
            Property(x => x.Attachments).HasColumnName("ATTACHMENTS").IsOptional();
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsRequired().HasMaxLength(50);
            Property(x => x.CreateUserName).HasColumnName("CREATEUSERNAME").IsRequired().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsRequired();
            Property(x => x.UserDeptID).HasColumnName("USERDEPTID").IsRequired().HasMaxLength(500);
            Property(x => x.UserDeptName).HasColumnName("USERDEPTNAME").IsRequired().HasMaxLength(500);
            Property(x => x.PassDate).HasColumnName("PASSDATE").IsOptional();
            Property(x => x.DownloadExpireDate).HasColumnName("DOWNLOADEXPIREDATE").IsOptional();
            Property(x => x.DownloadState).HasColumnName("DOWNLOADSTATE").IsRequired().HasMaxLength(50);
        }
    }

    // S_I_ChangePeriodApply
    internal partial class S_I_ChangePeriodApplyConfiguration : EntityTypeConfiguration<S_I_ChangePeriodApply>
    {
        public S_I_ChangePeriodApplyConfiguration()
        {
            ToTable("S_I_CHANGEPERIODAPPLY");
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
            Property(x => x.SerialNumber).HasColumnName("SERIALNUMBER").IsOptional().HasMaxLength(200);
            Property(x => x.KeepYear).HasColumnName("KEEPYEAR").IsOptional().HasMaxLength(200);
            Property(x => x.Remarks).HasColumnName("REMARKS").IsOptional().HasMaxLength(500);
            Property(x => x.DocManagerSign).HasColumnName("DOCMANAGERSIGN").IsOptional();
            Property(x => x.IdentifyApplyID).HasColumnName("IDENTIFYAPPLYID").IsOptional().HasMaxLength(200);
            Property(x => x.SynchChild).HasColumnName("SYNCHCHILD").IsOptional().HasMaxLength(200);
        }
    }

    // S_I_ChangePeriodApply_DetailInfo
    internal partial class S_I_ChangePeriodApply_DetailInfoConfiguration : EntityTypeConfiguration<S_I_ChangePeriodApply_DetailInfo>
    {
        public S_I_ChangePeriodApply_DetailInfoConfiguration()
        {
            ToTable("S_I_CHANGEPERIODAPPLY_DETAILINFO");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.S_I_ChangePeriodApplyID).HasColumnName("S_I_CHANGEPERIODAPPLYID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.IdentifyInfoID).HasColumnName("IDENTIFYINFOID").IsOptional().HasMaxLength(200);
            Property(x => x.IdentifyApplyDetailInfoID).HasColumnName("IDENTIFYAPPLYDETAILINFOID").IsOptional().HasMaxLength(200);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(200);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(200);
            Property(x => x.DataType).HasColumnName("DATATYPE").IsOptional().HasMaxLength(200);
            Property(x => x.SpaceName).HasColumnName("SPACENAME").IsOptional().HasMaxLength(200);
            Property(x => x.KeepYear).HasColumnName("KEEPYEAR").IsOptional().HasMaxLength(200);
            Property(x => x.KeepYearToToday).HasColumnName("KEEPYEARTOTODAY").IsOptional().HasMaxLength(200);
            Property(x => x.SecretLevel).HasColumnName("SECRETLEVEL").IsOptional().HasMaxLength(200);
            Property(x => x.ArchivePeople).HasColumnName("ARCHIVEPEOPLE").IsOptional().HasMaxLength(200);
            Property(x => x.ArchivePeopleName).HasColumnName("ARCHIVEPEOPLENAME").IsOptional().HasMaxLength(200);
            Property(x => x.ArchiveDate).HasColumnName("ARCHIVEDATE").IsOptional();
            Property(x => x.PhysicalFileCount).HasColumnName("PHYSICALFILECOUNT").IsOptional();
            Property(x => x.StorageRoom).HasColumnName("STORAGEROOM").IsOptional().HasMaxLength(200);
            Property(x => x.StorageRoomName).HasColumnName("STORAGEROOMNAME").IsOptional().HasMaxLength(200);
            Property(x => x.RackNumber).HasColumnName("RACKNUMBER").IsOptional().HasMaxLength(200);
            Property(x => x.RackNumberName).HasColumnName("RACKNUMBERNAME").IsOptional().HasMaxLength(200);

            // Foreign keys
            HasOptional(a => a.S_I_ChangePeriodApply).WithMany(b => b.S_I_ChangePeriodApply_DetailInfo).HasForeignKey(c => c.S_I_ChangePeriodApplyID); // FK_S_I_ChangePeriodApply_DetailInfo_S_I_ChangePeriodApply
        }
    }

    // S_I_DestroyApply
    internal partial class S_I_DestroyApplyConfiguration : EntityTypeConfiguration<S_I_DestroyApply>
    {
        public S_I_DestroyApplyConfiguration()
        {
            ToTable("S_I_DESTROYAPPLY");
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
            Property(x => x.SerialNumber).HasColumnName("SERIALNUMBER").IsOptional().HasMaxLength(200);
            Property(x => x.ApplyUser).HasColumnName("APPLYUSER").IsOptional().HasMaxLength(200);
            Property(x => x.ApplyUserName).HasColumnName("APPLYUSERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ApplyDate).HasColumnName("APPLYDATE").IsOptional().HasMaxLength(200);
            Property(x => x.SupervisorUser).HasColumnName("SUPERVISORUSER").IsOptional().HasMaxLength(200);
            Property(x => x.SupervisorUserName).HasColumnName("SUPERVISORUSERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Remarks).HasColumnName("REMARKS").IsOptional().HasMaxLength(500);
            Property(x => x.DocManagerSign).HasColumnName("DOCMANAGERSIGN").IsOptional();
            Property(x => x.SupervisorUserSign).HasColumnName("SUPERVISORUSERSIGN").IsOptional();
            Property(x => x.IdentifyApplyID).HasColumnName("IDENTIFYAPPLYID").IsOptional().HasMaxLength(200);
        }
    }

    // S_I_DestroyApply_DetailInfo
    internal partial class S_I_DestroyApply_DetailInfoConfiguration : EntityTypeConfiguration<S_I_DestroyApply_DetailInfo>
    {
        public S_I_DestroyApply_DetailInfoConfiguration()
        {
            ToTable("S_I_DESTROYAPPLY_DETAILINFO");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.S_I_DestroyApplyID).HasColumnName("S_I_DESTROYAPPLYID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.IdentifyInfoID).HasColumnName("IDENTIFYINFOID").IsOptional().HasMaxLength(200);
            Property(x => x.IdentifyApplyDetailInfoID).HasColumnName("IDENTIFYAPPLYDETAILINFOID").IsOptional().HasMaxLength(200);
            Property(x => x.DestroyState).HasColumnName("DESTROYSTATE").IsOptional().HasMaxLength(200);
            Property(x => x.SupervisorUser).HasColumnName("SUPERVISORUSER").IsOptional().HasMaxLength(200);
            Property(x => x.SupervisorUserName).HasColumnName("SUPERVISORUSERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.DestroyDate).HasColumnName("DESTROYDATE").IsOptional();
            Property(x => x.IdentifyResult).HasColumnName("IDENTIFYRESULT").IsOptional().HasMaxLength(200);
            Property(x => x.IdentifyDate).HasColumnName("IDENTIFYDATE").IsOptional();
            Property(x => x.IdentifyUser).HasColumnName("IDENTIFYUSER").IsOptional().HasMaxLength(200);
            Property(x => x.IdentifyUserName).HasColumnName("IDENTIFYUSERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(200);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(200);
            Property(x => x.DataType).HasColumnName("DATATYPE").IsOptional().HasMaxLength(200);
            Property(x => x.SpaceName).HasColumnName("SPACENAME").IsOptional().HasMaxLength(200);
            Property(x => x.KeepYear).HasColumnName("KEEPYEAR").IsOptional().HasMaxLength(200);
            Property(x => x.KeepYearToToday).HasColumnName("KEEPYEARTOTODAY").IsOptional().HasMaxLength(200);
            Property(x => x.SecretLevel).HasColumnName("SECRETLEVEL").IsOptional().HasMaxLength(200);
            Property(x => x.ArchivePeople).HasColumnName("ARCHIVEPEOPLE").IsOptional().HasMaxLength(200);
            Property(x => x.ArchivePeopleName).HasColumnName("ARCHIVEPEOPLENAME").IsOptional().HasMaxLength(200);
            Property(x => x.ArchiveDate).HasColumnName("ARCHIVEDATE").IsOptional();
            Property(x => x.PhysicalFileCount).HasColumnName("PHYSICALFILECOUNT").IsOptional();
            Property(x => x.StorageRoom).HasColumnName("STORAGEROOM").IsOptional().HasMaxLength(200);
            Property(x => x.StorageRoomName).HasColumnName("STORAGEROOMNAME").IsOptional().HasMaxLength(200);
            Property(x => x.RackNumber).HasColumnName("RACKNUMBER").IsOptional().HasMaxLength(200);
            Property(x => x.RackNumberName).HasColumnName("RACKNUMBERNAME").IsOptional().HasMaxLength(200);

            // Foreign keys
            HasOptional(a => a.S_I_DestroyApply).WithMany(b => b.S_I_DestroyApply_DetailInfo).HasForeignKey(c => c.S_I_DestroyApplyID); // FK_S_I_DestroyApply_DetailInfo_S_I_DestroyApply
        }
    }

    // S_I_IdentifyApply
    internal partial class S_I_IdentifyApplyConfiguration : EntityTypeConfiguration<S_I_IdentifyApply>
    {
        public S_I_IdentifyApplyConfiguration()
        {
            ToTable("S_I_IDENTIFYAPPLY");
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
            Property(x => x.SerialNumber).HasColumnName("SERIALNUMBER").IsOptional().HasMaxLength(200);
            Property(x => x.ChargeUser).HasColumnName("CHARGEUSER").IsOptional().HasMaxLength(200);
            Property(x => x.ChargeUserName).HasColumnName("CHARGEUSERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.GroupUser).HasColumnName("GROUPUSER").IsOptional().HasMaxLength(500);
            Property(x => x.GroupUserName).HasColumnName("GROUPUSERNAME").IsOptional().HasMaxLength(500);
            Property(x => x.Remarks).HasColumnName("REMARKS").IsOptional().HasMaxLength(500);
            Property(x => x.GroupUserSign).HasColumnName("GROUPUSERSIGN").IsOptional();
            Property(x => x.HandleState).HasColumnName("HANDLESTATE").IsOptional().HasMaxLength(200);
        }
    }

    // S_I_IdentifyApply_DetailInfo
    internal partial class S_I_IdentifyApply_DetailInfoConfiguration : EntityTypeConfiguration<S_I_IdentifyApply_DetailInfo>
    {
        public S_I_IdentifyApply_DetailInfoConfiguration()
        {
            ToTable("S_I_IDENTIFYAPPLY_DETAILINFO");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.S_I_IdentifyApplyID).HasColumnName("S_I_IDENTIFYAPPLYID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.IdentifyInfoID).HasColumnName("IDENTIFYINFOID").IsOptional().HasMaxLength(200);
            Property(x => x.HandleResult).HasColumnName("HANDLERESULT").IsOptional().HasMaxLength(200);
            Property(x => x.IdentifyResult).HasColumnName("IDENTIFYRESULT").IsOptional().HasMaxLength(200);
            Property(x => x.IdentifyDate).HasColumnName("IDENTIFYDATE").IsOptional();
            Property(x => x.IdentifyUser).HasColumnName("IDENTIFYUSER").IsOptional().HasMaxLength(200);
            Property(x => x.IdentifyUserName).HasColumnName("IDENTIFYUSERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(200);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(200);
            Property(x => x.DataType).HasColumnName("DATATYPE").IsOptional().HasMaxLength(200);
            Property(x => x.SpaceName).HasColumnName("SPACENAME").IsOptional().HasMaxLength(200);
            Property(x => x.KeepYear).HasColumnName("KEEPYEAR").IsOptional().HasMaxLength(200);
            Property(x => x.KeepYearToToday).HasColumnName("KEEPYEARTOTODAY").IsOptional().HasMaxLength(200);
            Property(x => x.SecretLevel).HasColumnName("SECRETLEVEL").IsOptional().HasMaxLength(200);
            Property(x => x.ArchivePeople).HasColumnName("ARCHIVEPEOPLE").IsOptional().HasMaxLength(200);
            Property(x => x.ArchivePeopleName).HasColumnName("ARCHIVEPEOPLENAME").IsOptional().HasMaxLength(200);
            Property(x => x.ArchiveDate).HasColumnName("ARCHIVEDATE").IsOptional();
            Property(x => x.PhysicalFileCount).HasColumnName("PHYSICALFILECOUNT").IsOptional();
            Property(x => x.StorageRoom).HasColumnName("STORAGEROOM").IsOptional().HasMaxLength(200);
            Property(x => x.StorageRoomName).HasColumnName("STORAGEROOMNAME").IsOptional().HasMaxLength(200);
            Property(x => x.RackNumber).HasColumnName("RACKNUMBER").IsOptional().HasMaxLength(200);
            Property(x => x.RackNumberName).HasColumnName("RACKNUMBERNAME").IsOptional().HasMaxLength(200);

            // Foreign keys
            HasOptional(a => a.S_I_IdentifyApply).WithMany(b => b.S_I_IdentifyApply_DetailInfo).HasForeignKey(c => c.S_I_IdentifyApplyID); // FK_S_I_IdentifyApply_DetailInfo_S_I_IdentifyApply
        }
    }

    // S_I_IdentifyInfo
    internal partial class S_I_IdentifyInfoConfiguration : EntityTypeConfiguration<S_I_IdentifyInfo>
    {
        public S_I_IdentifyInfoConfiguration()
        {
            ToTable("S_I_IDENTIFYINFO");
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
            Property(x => x.State).HasColumnName("STATE").IsOptional().HasMaxLength(200);
            Property(x => x.SpaceID).HasColumnName("SPACEID").IsOptional().HasMaxLength(200);
            Property(x => x.SpaceName).HasColumnName("SPACENAME").IsOptional().HasMaxLength(200);
            Property(x => x.NodeID).HasColumnName("NODEID").IsOptional().HasMaxLength(200);
            Property(x => x.ConfigID).HasColumnName("CONFIGID").IsOptional().HasMaxLength(200);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(200);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(200);
            Property(x => x.DataType).HasColumnName("DATATYPE").IsOptional().HasMaxLength(200);
            Property(x => x.KeepYear).HasColumnName("KEEPYEAR").IsOptional().HasMaxLength(200);
            Property(x => x.KeepYearToToday).HasColumnName("KEEPYEARTOTODAY").IsOptional().HasMaxLength(200);
            Property(x => x.SecretLevel).HasColumnName("SECRETLEVEL").IsOptional().HasMaxLength(200);
            Property(x => x.ArchivePeople).HasColumnName("ARCHIVEPEOPLE").IsOptional().HasMaxLength(200);
            Property(x => x.ArchivePeopleName).HasColumnName("ARCHIVEPEOPLENAME").IsOptional().HasMaxLength(200);
            Property(x => x.ArchiveDepartment).HasColumnName("ARCHIVEDEPARTMENT").IsOptional().HasMaxLength(200);
            Property(x => x.ArchiveDepartmentName).HasColumnName("ARCHIVEDEPARTMENTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ArchiveDate).HasColumnName("ARCHIVEDATE").IsOptional();
            Property(x => x.PhysicalFileCount).HasColumnName("PHYSICALFILECOUNT").IsOptional();
            Property(x => x.StorageRoom).HasColumnName("STORAGEROOM").IsOptional().HasMaxLength(200);
            Property(x => x.StorageRoomName).HasColumnName("STORAGEROOMNAME").IsOptional().HasMaxLength(200);
            Property(x => x.RackNumber).HasColumnName("RACKNUMBER").IsOptional().HasMaxLength(200);
            Property(x => x.RackNumberName).HasColumnName("RACKNUMBERNAME").IsOptional().HasMaxLength(200);
        }
    }

    // S_I_IdentifyRule
    internal partial class S_I_IdentifyRuleConfiguration : EntityTypeConfiguration<S_I_IdentifyRule>
    {
        public S_I_IdentifyRuleConfiguration()
        {
            ToTable("S_I_IDENTIFYRULE");
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
            Property(x => x.DayRule).HasColumnName("DAYRULE").IsOptional().HasMaxLength(200);
            Property(x => x.DayRuleName).HasColumnName("DAYRULENAME").IsOptional().HasMaxLength(200);
        }
    }

    // S_R_PhysicalReorganize
    internal partial class S_R_PhysicalReorganizeConfiguration : EntityTypeConfiguration<S_R_PhysicalReorganize>
    {
        public S_R_PhysicalReorganizeConfiguration()
        {
            ToTable("S_R_PHYSICALREORGANIZE");
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
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(200);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(500);
            Property(x => x.ProjectCode).HasColumnName("PROJECTCODE").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectManager).HasColumnName("PROJECTMANAGER").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectManagerName).HasColumnName("PROJECTMANAGERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.SendUser).HasColumnName("SENDUSER").IsOptional().HasMaxLength(200);
            Property(x => x.SendUserName).HasColumnName("SENDUSERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.SendDate).HasColumnName("SENDDATE").IsOptional();
            Property(x => x.ReceiveUser).HasColumnName("RECEIVEUSER").IsOptional().HasMaxLength(200);
            Property(x => x.ReceiveUserName).HasColumnName("RECEIVEUSERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ReceiveDate).HasColumnName("RECEIVEDATE").IsOptional();
            Property(x => x.Remark).HasColumnName("REMARK").IsOptional().HasMaxLength(500);
            Property(x => x.SpaceID).HasColumnName("SPACEID").IsOptional().HasMaxLength(200);
            Property(x => x.State).HasColumnName("STATE").IsOptional().HasMaxLength(200);
            Property(x => x.ReorganizeUser).HasColumnName("REORGANIZEUSER").IsOptional().HasMaxLength(200);
            Property(x => x.ReorganizeDate).HasColumnName("REORGANIZEDATE").IsOptional();
            Property(x => x.DefaultNodes).HasColumnName("DEFAULTNODES").IsOptional().HasMaxLength(500);
            Property(x => x.DefaultNodesName).HasColumnName("DEFAULTNODESNAME").IsOptional().HasMaxLength(500);
            Property(x => x.ReorganizeUserName).HasColumnName("REORGANIZEUSERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.TmplCode).HasColumnName("TMPLCODE").IsOptional().HasMaxLength(200);
            Property(x => x.SelectNodeIDs).HasColumnName("SELECTNODEIDS").IsOptional().HasMaxLength(1073741823);
        }
    }

    // S_R_PhysicalReorganize_FileDetail
    internal partial class S_R_PhysicalReorganize_FileDetailConfiguration : EntityTypeConfiguration<S_R_PhysicalReorganize_FileDetail>
    {
        public S_R_PhysicalReorganize_FileDetailConfiguration()
        {
            ToTable("S_R_PHYSICALREORGANIZE_FILEDETAIL");
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
            Property(x => x.S_R_PhysicalReorganizeID).HasColumnName("S_R_PHYSICALREORGANIZEID").IsOptional().HasMaxLength(50);
            Property(x => x.FileConfigID).HasColumnName("FILECONFIGID").IsOptional().HasMaxLength(200);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(200);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(200);
            Property(x => x.PageCount).HasColumnName("PAGECOUNT").IsOptional().HasMaxLength(200);
            Property(x => x.Version).HasColumnName("VERSION").IsOptional().HasMaxLength(200);
            Property(x => x.Media).HasColumnName("MEDIA").IsOptional().HasMaxLength(200);
            Property(x => x.ReorganizePath).HasColumnName("REORGANIZEPATH").IsOptional().HasMaxLength(200);
            Property(x => x.ReorganizeFullID).HasColumnName("REORGANIZEFULLID").IsOptional();
            Property(x => x.ArchiveFileID).HasColumnName("ARCHIVEFILEID").IsOptional().HasMaxLength(200);
            Property(x => x.FromCode).HasColumnName("FROMCODE").IsOptional().HasMaxLength(200);
            Property(x => x.Specification).HasColumnName("SPECIFICATION").IsOptional().HasMaxLength(200);
            Property(x => x.Length).HasColumnName("LENGTH").IsOptional().HasMaxLength(200);
            Property(x => x.Designer).HasColumnName("DESIGNER").IsOptional().HasMaxLength(200);
            Property(x => x.DesignerName).HasColumnName("DESIGNERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Collactor).HasColumnName("COLLACTOR").IsOptional().HasMaxLength(200);
            Property(x => x.CollactorName).HasColumnName("COLLACTORNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Auditor).HasColumnName("AUDITOR").IsOptional().HasMaxLength(200);
            Property(x => x.AuditorName).HasColumnName("AUDITORNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Approver).HasColumnName("APPROVER").IsOptional().HasMaxLength(200);
            Property(x => x.ApproverName).HasColumnName("APPROVERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.DesignFinishDate).HasColumnName("DESIGNFINISHDATE").IsOptional();
            Property(x => x.ProjectName).HasColumnName("PROJECTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectCode).HasColumnName("PROJECTCODE").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectClass).HasColumnName("PROJECTCLASS").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectLevel).HasColumnName("PROJECTLEVEL").IsOptional().HasMaxLength(200);
            Property(x => x.Major).HasColumnName("MAJOR").IsOptional().HasMaxLength(200);
            Property(x => x.Remark).HasColumnName("REMARK").IsOptional().HasMaxLength(500);
            Property(x => x.Type).HasColumnName("TYPE").IsOptional().HasMaxLength(200);
            Property(x => x.FileCreateDate).HasColumnName("FILECREATEDATE").IsOptional().HasMaxLength(200);
            Property(x => x.CollectionPeople).HasColumnName("COLLECTIONPEOPLE").IsOptional().HasMaxLength(200);
            Property(x => x.CollectionDate).HasColumnName("COLLECTIONDATE").IsOptional();
            Property(x => x.ChargeUserName).HasColumnName("CHARGEUSERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectNameName).HasColumnName("PROJECTNAMENAME").IsOptional().HasMaxLength(200);
            Property(x => x.FileType).HasColumnName("FILETYPE").IsOptional().HasMaxLength(200);
            Property(x => x.YesOrNo).HasColumnName("YESORNO").IsOptional().HasMaxLength(200);
            Property(x => x.Quantity).HasColumnName("QUANTITY").IsOptional().HasMaxLength(200);
            Property(x => x.SecretLevel).HasColumnName("SECRETLEVEL").IsOptional().HasMaxLength(200);
            Property(x => x.KeepYear).HasColumnName("KEEPYEAR").IsOptional().HasMaxLength(200);
            Property(x => x.PhaseValue).HasColumnName("PHASEVALUE").IsOptional().HasMaxLength(200);
            Property(x => x.StorageType).HasColumnName("STORAGETYPE").IsOptional().HasMaxLength(200);
            Property(x => x.Edition).HasColumnName("EDITION").IsOptional().HasMaxLength(200);
            Property(x => x.ChargeUserNameName).HasColumnName("CHARGEUSERNAMENAME").IsOptional().HasMaxLength(200);
            Property(x => x.ChargeUser).HasColumnName("CHARGEUSER").IsOptional().HasMaxLength(200);
            Property(x => x.CollectionPeopleName).HasColumnName("COLLECTIONPEOPLENAME").IsOptional().HasMaxLength(200);
            Property(x => x.ArchivePeople).HasColumnName("ARCHIVEPEOPLE").IsOptional().HasMaxLength(200);
            Property(x => x.ArchivePeopleName).HasColumnName("ARCHIVEPEOPLENAME").IsOptional().HasMaxLength(200);
            Property(x => x.ArchiveDepartment).HasColumnName("ARCHIVEDEPARTMENT").IsOptional().HasMaxLength(200);
            Property(x => x.ArchiveDepartmentName).HasColumnName("ARCHIVEDEPARTMENTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ArchiveDate).HasColumnName("ARCHIVEDATE").IsOptional();

            // Foreign keys
            HasOptional(a => a.S_R_PhysicalReorganize).WithMany(b => b.S_R_PhysicalReorganize_FileDetail).HasForeignKey(c => c.S_R_PhysicalReorganizeID); // FK_S_R_PhysicalReorganize_FileDetail_S_R_PhysicalReorganize
        }
    }

    // S_R_PhysicalReorganize_NodeDetail
    internal partial class S_R_PhysicalReorganize_NodeDetailConfiguration : EntityTypeConfiguration<S_R_PhysicalReorganize_NodeDetail>
    {
        public S_R_PhysicalReorganize_NodeDetailConfiguration()
        {
            ToTable("S_R_PHYSICALREORGANIZE_NODEDETAIL");
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
            Property(x => x.S_R_PhysicalReorganizeID).HasColumnName("S_R_PHYSICALREORGANIZEID").IsOptional().HasMaxLength(50);
            Property(x => x.NodeConfID).HasColumnName("NODECONFID").IsOptional().HasMaxLength(200);
            Property(x => x.NodeType).HasColumnName("NODETYPE").IsOptional().HasMaxLength(200);
            Property(x => x.StorageType).HasColumnName("STORAGETYPE").IsOptional().HasMaxLength(200);
            Property(x => x.ReorganizePath).HasColumnName("REORGANIZEPATH").IsOptional().HasMaxLength(200);
            Property(x => x.ReorganizeFullID).HasColumnName("REORGANIZEFULLID").IsOptional().HasMaxLength(2000);
            Property(x => x.ArchiveVolumnID).HasColumnName("ARCHIVEVOLUMNID").IsOptional().HasMaxLength(200);
            Property(x => x.FromCode).HasColumnName("FROMCODE").IsOptional().HasMaxLength(200);
            Property(x => x.Mutual).HasColumnName("MUTUAL").IsOptional().HasMaxLength(200);
            Property(x => x.VolumnDepartment).HasColumnName("VOLUMNDEPARTMENT").IsOptional().HasMaxLength(200);
            Property(x => x.VolumnDepartmentName).HasColumnName("VOLUMNDEPARTMENTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.VolumnPeople).HasColumnName("VOLUMNPEOPLE").IsOptional().HasMaxLength(200);
            Property(x => x.VolumnPeopleName).HasColumnName("VOLUMNPEOPLENAME").IsOptional().HasMaxLength(200);
            Property(x => x.VolumnDate).HasColumnName("VOLUMNDATE").IsOptional();
            Property(x => x.StartDate).HasColumnName("STARTDATE").IsOptional();
            Property(x => x.EndDate).HasColumnName("ENDDATE").IsOptional();
            Property(x => x.SecretLevel).HasColumnName("SECRETLEVEL").IsOptional().HasMaxLength(200);
            Property(x => x.KeepYear).HasColumnName("KEEPYEAR").IsOptional().HasMaxLength(200);
            Property(x => x.CabinetCode).HasColumnName("CABINETCODE").IsOptional().HasMaxLength(200);
            Property(x => x.StorageName).HasColumnName("STORAGENAME").IsOptional().HasMaxLength(200);
            Property(x => x.PhysicalPageCount).HasColumnName("PHYSICALPAGECOUNT").IsOptional().HasMaxLength(200);
            Property(x => x.PhysicalCount).HasColumnName("PHYSICALCOUNT").IsOptional().HasMaxLength(200);
            Property(x => x.VolumnExplain).HasColumnName("VOLUMNEXPLAIN").IsOptional().HasMaxLength(500);
            Property(x => x.Remark).HasColumnName("REMARK").IsOptional().HasMaxLength(500);
            Property(x => x.Quantity).HasColumnName("QUANTITY").IsOptional();
            Property(x => x.CabinetCodeName).HasColumnName("CABINETCODENAME").IsOptional().HasMaxLength(200);
            Property(x => x.DocumentCode).HasColumnName("DOCUMENTCODE").IsOptional().HasMaxLength(200);
            Property(x => x.RackNumber).HasColumnName("RACKNUMBER").IsOptional().HasMaxLength(200);
            Property(x => x.RackNumberName).HasColumnName("RACKNUMBERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.StorageRoom).HasColumnName("STORAGEROOM").IsOptional().HasMaxLength(200);
            Property(x => x.ArchivePeople).HasColumnName("ARCHIVEPEOPLE").IsOptional().HasMaxLength(200);
            Property(x => x.ArchivePeopleName).HasColumnName("ARCHIVEPEOPLENAME").IsOptional().HasMaxLength(200);
            Property(x => x.ArchiveDepartment).HasColumnName("ARCHIVEDEPARTMENT").IsOptional().HasMaxLength(200);
            Property(x => x.ArchiveDepartmentName).HasColumnName("ARCHIVEDEPARTMENTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ArchiveDate).HasColumnName("ARCHIVEDATE").IsOptional();

            // Foreign keys
            HasOptional(a => a.S_R_PhysicalReorganize).WithMany(b => b.S_R_PhysicalReorganize_NodeDetail).HasForeignKey(c => c.S_R_PhysicalReorganizeID); // FK_S_R_PhysicalReorganize_NodeDetail_S_R_PhysicalReorganize
        }
    }

    // S_R_Reorganize
    internal partial class S_R_ReorganizeConfiguration : EntityTypeConfiguration<S_R_Reorganize>
    {
        public S_R_ReorganizeConfiguration()
        {
            ToTable("S_R_REORGANIZE");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.BusinessID).HasColumnName("BUSINESSID").IsOptional().HasMaxLength(200);
            Property(x => x.ReorganizeUser).HasColumnName("REORGANIZEUSER").IsOptional().HasMaxLength(200);
            Property(x => x.ReorganizeUserName).HasColumnName("REORGANIZEUSERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ReorganizeDate).HasColumnName("REORGANIZEDATE").IsOptional();
            Property(x => x.ReorganizeState).HasColumnName("REORGANIZESTATE").IsOptional().HasMaxLength(200);
            Property(x => x.BusinessTable).HasColumnName("BUSINESSTABLE").IsOptional().HasMaxLength(200);
            Property(x => x.BusinessType).HasColumnName("BUSINESSTYPE").IsOptional().HasMaxLength(200);
            Property(x => x.TaskName).HasColumnName("TASKNAME").IsOptional().HasMaxLength(500);
            Property(x => x.SendUser).HasColumnName("SENDUSER").IsOptional().HasMaxLength(200);
            Property(x => x.SendUserName).HasColumnName("SENDUSERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.SendDate).HasColumnName("SENDDATE").IsOptional();
            Property(x => x.SpaceID).HasColumnName("SPACEID").IsOptional().HasMaxLength(200);
            Property(x => x.DocumentList).HasColumnName("DOCUMENTLIST").IsOptional();
            Property(x => x.TmplCode).HasColumnName("TMPLCODE").IsOptional().HasMaxLength(200);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(200);
            Property(x => x.Remark).HasColumnName("REMARK").IsOptional().HasMaxLength(500);
            Property(x => x.SelectNodeIDs).HasColumnName("SELECTNODEIDS").IsOptional().HasMaxLength(1073741823);
            Property(x => x.DefaultNodes).HasColumnName("DEFAULTNODES").IsOptional().HasMaxLength(200);
            Property(x => x.DefaultNodesName).HasColumnName("DEFAULTNODESNAME").IsOptional().HasMaxLength(200);
            Property(x => x.SendDepartment).HasColumnName("SENDDEPARTMENT").IsOptional().HasMaxLength(200);
            Property(x => x.SendDepartmentName).HasColumnName("SENDDEPARTMENTNAME").IsOptional().HasMaxLength(200);
        }
    }

    // S_R_Reorganize_DocumentList
    internal partial class S_R_Reorganize_DocumentListConfiguration : EntityTypeConfiguration<S_R_Reorganize_DocumentList>
    {
        public S_R_Reorganize_DocumentListConfiguration()
        {
            ToTable("S_R_REORGANIZE_DOCUMENTLIST");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.S_R_ReorganizeID).HasColumnName("S_R_REORGANIZEID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(500);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(200);
            Property(x => x.Catagory).HasColumnName("CATAGORY").IsOptional().HasMaxLength(200);
            Property(x => x.MajorValue).HasColumnName("MAJORVALUE").IsOptional().HasMaxLength(200);
            Property(x => x.Version).HasColumnName("VERSION").IsOptional().HasMaxLength(50);
            Property(x => x.MainFile).HasColumnName("MAINFILE").IsOptional().HasMaxLength(200);
            Property(x => x.MainFileName).HasColumnName("MAINFILENAME").IsOptional().HasMaxLength(200);
            Property(x => x.PDFFile).HasColumnName("PDFFILE").IsOptional().HasMaxLength(200);
            Property(x => x.ReorganizePath).HasColumnName("REORGANIZEPATH").IsOptional().HasMaxLength(200);
            Property(x => x.ReorganizeFullID).HasColumnName("REORGANIZEFULLID").IsOptional().HasMaxLength(2000);
            Property(x => x.RelateID).HasColumnName("RELATEID").IsOptional().HasMaxLength(200);
            Property(x => x.RelateTable).HasColumnName("RELATETABLE").IsOptional().HasMaxLength(200);
            Property(x => x.PlotFile).HasColumnName("PLOTFILE").IsOptional().HasMaxLength(500);
            Property(x => x.DwfFile).HasColumnName("DWFFILE").IsOptional().HasMaxLength(500);
            Property(x => x.TiffFile).HasColumnName("TIFFFILE").IsOptional().HasMaxLength(500);
            Property(x => x.SignPdfFile).HasColumnName("SIGNPDFFILE").IsOptional().HasMaxLength(500);
            Property(x => x.XrefFile).HasColumnName("XREFFILE").IsOptional().HasMaxLength(500);
            Property(x => x.FontFile).HasColumnName("FONTFILE").IsOptional().HasMaxLength(500);
            Property(x => x.Attr).HasColumnName("ATTR").IsOptional().HasMaxLength(1073741823);
            Property(x => x.ArchiveFileID).HasColumnName("ARCHIVEFILEID").IsOptional().HasMaxLength(50);
            Property(x => x.ArchiveFileAttrs).HasColumnName("ARCHIVEFILEATTRS").IsOptional().HasMaxLength(200);
            Property(x => x.PageCount).HasColumnName("PAGECOUNT").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectName).HasColumnName("PROJECTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectNameName).HasColumnName("PROJECTNAMENAME").IsOptional().HasMaxLength(200);
            Property(x => x.PDFFileName).HasColumnName("PDFFILENAME").IsOptional().HasMaxLength(200);
            Property(x => x.Attachments).HasColumnName("ATTACHMENTS").IsOptional().HasMaxLength(200);
            Property(x => x.AttachmentsName).HasColumnName("ATTACHMENTSNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ReorganizeConfigID).HasColumnName("REORGANIZECONFIGID").IsOptional().HasMaxLength(200);
            Property(x => x.Edition).HasColumnName("EDITION").IsOptional().HasMaxLength(50);
            Property(x => x.Major).HasColumnName("MAJOR").IsOptional().HasMaxLength(200);

            // Foreign keys
            HasOptional(a => a.S_R_Reorganize).WithMany(b => b.S_R_Reorganize_DocumentList).HasForeignKey(c => c.S_R_ReorganizeID); // FK_S_R_Reorganize_DocumentList_S_R_Reorganize
        }
    }

    // S_SU_DocumentRelieveSeal
    internal partial class S_SU_DocumentRelieveSealConfiguration : EntityTypeConfiguration<S_SU_DocumentRelieveSeal>
    {
        public S_SU_DocumentRelieveSealConfiguration()
        {
            ToTable("S_SU_DOCUMENTRELIEVESEAL");
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
            Property(x => x.RelieveSealUser).HasColumnName("RELIEVESEALUSER").IsOptional().HasMaxLength(200);
            Property(x => x.RelieveSealUserName).HasColumnName("RELIEVESEALUSERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.RelieveSealDate).HasColumnName("RELIEVESEALDATE").IsOptional();
            Property(x => x.RelieveSealRackNumber).HasColumnName("RELIEVESEALRACKNUMBER").IsOptional().HasMaxLength(200);
            Property(x => x.RelieveSealRackNumberName).HasColumnName("RELIEVESEALRACKNUMBERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.RelieveSealStorageRoom).HasColumnName("RELIEVESEALSTORAGEROOM").IsOptional().HasMaxLength(200);
            Property(x => x.RelieveSealStorageRoomName).HasColumnName("RELIEVESEALSTORAGEROOMNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ApplyUser).HasColumnName("APPLYUSER").IsOptional().HasMaxLength(200);
            Property(x => x.ApplyUserName).HasColumnName("APPLYUSERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ApplyDate).HasColumnName("APPLYDATE").IsOptional();
        }
    }

    // S_SU_DocumentRelieveSeal_DetailList
    internal partial class S_SU_DocumentRelieveSeal_DetailListConfiguration : EntityTypeConfiguration<S_SU_DocumentRelieveSeal_DetailList>
    {
        public S_SU_DocumentRelieveSeal_DetailListConfiguration()
        {
            ToTable("S_SU_DOCUMENTRELIEVESEAL_DETAILLIST");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.S_SU_DocumentRelieveSealID).HasColumnName("S_SU_DOCUMENTRELIEVESEALID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.DocumentCode).HasColumnName("DOCUMENTCODE").IsOptional().HasMaxLength(200);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(200);
            Property(x => x.SecretLevel).HasColumnName("SECRETLEVEL").IsOptional().HasMaxLength(200);
            Property(x => x.ArchivePeople).HasColumnName("ARCHIVEPEOPLE").IsOptional().HasMaxLength(200);
            Property(x => x.ArchivePeopleName).HasColumnName("ARCHIVEPEOPLENAME").IsOptional().HasMaxLength(200);
            Property(x => x.ArchiveDate).HasColumnName("ARCHIVEDATE").IsOptional();
            Property(x => x.KeepYear).HasColumnName("KEEPYEAR").IsOptional().HasMaxLength(200);
            Property(x => x.Quantity).HasColumnName("QUANTITY").IsOptional();
            Property(x => x.PhysicalFileCount).HasColumnName("PHYSICALFILECOUNT").IsOptional();
            Property(x => x.StorageRoomName).HasColumnName("STORAGEROOMNAME").IsOptional().HasMaxLength(200);
            Property(x => x.RackNumberName).HasColumnName("RACKNUMBERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.RelateID).HasColumnName("RELATEID").IsOptional().HasMaxLength(200);
            Property(x => x.RelateType).HasColumnName("RELATETYPE").IsOptional().HasMaxLength(200);
            Property(x => x.SpaceID).HasColumnName("SPACEID").IsOptional().HasMaxLength(200);

            // Foreign keys
            HasOptional(a => a.S_SU_DocumentRelieveSeal).WithMany(b => b.S_SU_DocumentRelieveSeal_DetailList).HasForeignKey(c => c.S_SU_DocumentRelieveSealID); // FK_S_SU_DocumentRelieveSeal_DetailList_S_SU_DocumentRelieveSeal
        }
    }

    // S_SU_DocumentSealUp
    internal partial class S_SU_DocumentSealUpConfiguration : EntityTypeConfiguration<S_SU_DocumentSealUp>
    {
        public S_SU_DocumentSealUpConfiguration()
        {
            ToTable("S_SU_DOCUMENTSEALUP");
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
            Property(x => x.SealUpUser).HasColumnName("SEALUPUSER").IsOptional().HasMaxLength(200);
            Property(x => x.SealUpUserName).HasColumnName("SEALUPUSERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.SealUpDate).HasColumnName("SEALUPDATE").IsOptional();
            Property(x => x.SealUpRackNumber).HasColumnName("SEALUPRACKNUMBER").IsOptional().HasMaxLength(200);
            Property(x => x.SealUpRackNumberName).HasColumnName("SEALUPRACKNUMBERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.SealUpStorageRoom).HasColumnName("SEALUPSTORAGEROOM").IsOptional().HasMaxLength(200);
            Property(x => x.SealUpStorageRoomName).HasColumnName("SEALUPSTORAGEROOMNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ApplyUser).HasColumnName("APPLYUSER").IsOptional().HasMaxLength(200);
            Property(x => x.ApplyUserName).HasColumnName("APPLYUSERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ApplyDate).HasColumnName("APPLYDATE").IsOptional();
        }
    }

    // S_SU_DocumentSealUp_DetailList
    internal partial class S_SU_DocumentSealUp_DetailListConfiguration : EntityTypeConfiguration<S_SU_DocumentSealUp_DetailList>
    {
        public S_SU_DocumentSealUp_DetailListConfiguration()
        {
            ToTable("S_SU_DOCUMENTSEALUP_DETAILLIST");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.S_SU_DocumentSealUpID).HasColumnName("S_SU_DOCUMENTSEALUPID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.DocumentCode).HasColumnName("DOCUMENTCODE").IsOptional().HasMaxLength(200);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(200);
            Property(x => x.SecretLevel).HasColumnName("SECRETLEVEL").IsOptional().HasMaxLength(200);
            Property(x => x.ArchivePeople).HasColumnName("ARCHIVEPEOPLE").IsOptional().HasMaxLength(200);
            Property(x => x.ArchivePeopleName).HasColumnName("ARCHIVEPEOPLENAME").IsOptional().HasMaxLength(200);
            Property(x => x.ArchiveDate).HasColumnName("ARCHIVEDATE").IsOptional();
            Property(x => x.KeepYear).HasColumnName("KEEPYEAR").IsOptional().HasMaxLength(200);
            Property(x => x.Quantity).HasColumnName("QUANTITY").IsOptional();
            Property(x => x.PhysicalFileCount).HasColumnName("PHYSICALFILECOUNT").IsOptional();
            Property(x => x.StorageRoomName).HasColumnName("STORAGEROOMNAME").IsOptional().HasMaxLength(200);
            Property(x => x.RackNumberName).HasColumnName("RACKNUMBERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.RelateID).HasColumnName("RELATEID").IsOptional().HasMaxLength(200);
            Property(x => x.RelateType).HasColumnName("RELATETYPE").IsOptional().HasMaxLength(200);
            Property(x => x.SpaceID).HasColumnName("SPACEID").IsOptional().HasMaxLength(200);

            // Foreign keys
            HasOptional(a => a.S_SU_DocumentSealUp).WithMany(b => b.S_SU_DocumentSealUp_DetailList).HasForeignKey(c => c.S_SU_DocumentSealUpID); // FK_S_SU_DocumentSealUp_DetailList_S_SU_DocumentSealUp
        }
    }

    // S_UserAdvanceQueryInfo
    internal partial class S_UserAdvanceQueryInfoConfiguration : EntityTypeConfiguration<S_UserAdvanceQueryInfo>
    {
        public S_UserAdvanceQueryInfoConfiguration()
        {
            ToTable("S_USERADVANCEQUERYINFO");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.UserID).HasColumnName("USERID").IsOptional().HasMaxLength(50);
            Property(x => x.QueryData).HasColumnName("QUERYDATA").IsOptional().HasMaxLength(1073741823);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.ModifyDate).HasColumnName("MODIFYDATE").IsOptional();
        }
    }

    // T_B_BorrowApply
    internal partial class T_B_BorrowApplyConfiguration : EntityTypeConfiguration<T_B_BorrowApply>
    {
        public T_B_BorrowApplyConfiguration()
        {
            ToTable("T_B_BORROWAPPLY");
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
            Property(x => x.ApplyUser).HasColumnName("APPLYUSER").IsOptional().HasMaxLength(200);
            Property(x => x.ApplyUserName).HasColumnName("APPLYUSERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ApplyDept).HasColumnName("APPLYDEPT").IsOptional().HasMaxLength(200);
            Property(x => x.ApplyDeptName).HasColumnName("APPLYDEPTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ExtensionNumber).HasColumnName("EXTENSIONNUMBER").IsOptional().HasMaxLength(200);
            Property(x => x.ApplyDate).HasColumnName("APPLYDATE").IsOptional();
            Property(x => x.Remarks).HasColumnName("REMARKS").IsOptional().HasMaxLength(500);
            Property(x => x.DocManagerSign).HasColumnName("DOCMANAGERSIGN").IsOptional();
        }
    }

    // T_B_BorrowApply_DetailInfo
    internal partial class T_B_BorrowApply_DetailInfoConfiguration : EntityTypeConfiguration<T_B_BorrowApply_DetailInfo>
    {
        public T_B_BorrowApply_DetailInfoConfiguration()
        {
            ToTable("T_B_BORROWAPPLY_DETAILINFO");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.T_B_BorrowApplyID).HasColumnName("T_B_BORROWAPPLYID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.SpaceID).HasColumnName("SPACEID").IsOptional().HasMaxLength(200);
            Property(x => x.NodeID).HasColumnName("NODEID").IsOptional().HasMaxLength(200);
            Property(x => x.FileID).HasColumnName("FILEID").IsOptional().HasMaxLength(200);
            Property(x => x.CarInfoID).HasColumnName("CARINFOID").IsOptional().HasMaxLength(50);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(200);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(200);
            Property(x => x.SpaceName).HasColumnName("SPACENAME").IsOptional().HasMaxLength(200);
            Property(x => x.DataType).HasColumnName("DATATYPE").IsOptional().HasMaxLength(200);
            Property(x => x.ApplyAmount).HasColumnName("APPLYAMOUNT").IsOptional();

            // Foreign keys
            HasOptional(a => a.T_B_BorrowApply).WithMany(b => b.T_B_BorrowApply_DetailInfo).HasForeignKey(c => c.T_B_BorrowApplyID); // FK_T_B_BorrowApply_DetailInfo_T_B_BorrowApply
        }
    }

    // T_Borrow
    internal partial class T_BorrowConfiguration : EntityTypeConfiguration<T_Borrow>
    {
        public T_BorrowConfiguration()
        {
            ToTable("T_BORROW");
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
            Property(x => x.UserDept).HasColumnName("USERDEPT").IsOptional().HasMaxLength(200);
            Property(x => x.UserDeptName).HasColumnName("USERDEPTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.LendDate).HasColumnName("LENDDATE").IsOptional();
            Property(x => x.ReturnDate).HasColumnName("RETURNDATE").IsOptional();
            Property(x => x.Purpose).HasColumnName("PURPOSE").IsOptional().HasMaxLength(500);
            Property(x => x.Phone).HasColumnName("PHONE").IsOptional().HasMaxLength(200);
            Property(x => x.PrjPrincipleSign).HasColumnName("PRJPRINCIPLESIGN").IsOptional();
            Property(x => x.DirecotorSign).HasColumnName("DIRECOTORSIGN").IsOptional();
            Property(x => x.LeaderSign).HasColumnName("LEADERSIGN").IsOptional();
            Property(x => x.CreateUserName).HasColumnName("CREATEUSERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(200);
            Property(x => x.Type).HasColumnName("TYPE").IsOptional().HasMaxLength(200);
            Property(x => x.ApplyDate).HasColumnName("APPLYDATE").IsOptional();
            Property(x => x.PassDate).HasColumnName("PASSDATE").IsOptional();
            Property(x => x.FlowID).HasColumnName("FLOWID").IsOptional().HasMaxLength(200);
            Property(x => x.BorrowState).HasColumnName("BORROWSTATE").IsOptional().HasMaxLength(200);
            Property(x => x.PurposeDetail).HasColumnName("PURPOSEDETAIL").IsOptional().HasMaxLength(200);
            Property(x => x.PrjPrincipleID).HasColumnName("PRJPRINCIPLEID").IsOptional().HasMaxLength(200);
            Property(x => x.PrjPrincipleName).HasColumnName("PRJPRINCIPLENAME").IsOptional().HasMaxLength(200);
            Property(x => x.PrjPrincipleDate).HasColumnName("PRJPRINCIPLEDATE").IsOptional().HasMaxLength(200);
            Property(x => x.DirecotorID).HasColumnName("DIRECOTORID").IsOptional().HasMaxLength(200);
            Property(x => x.DirecotorName).HasColumnName("DIRECOTORNAME").IsOptional().HasMaxLength(200);
            Property(x => x.DirecotorDate).HasColumnName("DIRECOTORDATE").IsOptional().HasMaxLength(200);
            Property(x => x.LeaderID).HasColumnName("LEADERID").IsOptional().HasMaxLength(200);
            Property(x => x.LeaderName).HasColumnName("LEADERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.LeaderDate).HasColumnName("LEADERDATE").IsOptional().HasMaxLength(200);
            Property(x => x.SpaceID).HasColumnName("SPACEID").IsOptional().HasMaxLength(200);
        }
    }

    // T_Borrow_FileInfo
    internal partial class T_Borrow_FileInfoConfiguration : EntityTypeConfiguration<T_Borrow_FileInfo>
    {
        public T_Borrow_FileInfoConfiguration()
        {
            ToTable("T_BORROW_FILEINFO");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.T_BorrowID).HasColumnName("T_BORROWID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.FileCode).HasColumnName("FILECODE").IsOptional().HasMaxLength(200);
            Property(x => x.FileName).HasColumnName("FILENAME").IsOptional().HasMaxLength(200);
            Property(x => x.FileID).HasColumnName("FILEID").IsOptional().HasMaxLength(200);
            Property(x => x.CarInfoID).HasColumnName("CARINFOID").IsOptional().HasMaxLength(200);
            Property(x => x.ConfigID).HasColumnName("CONFIGID").IsOptional().HasMaxLength(200);
            Property(x => x.NodeID).HasColumnName("NODEID").IsOptional().HasMaxLength(200);
            Property(x => x.SpaceID).HasColumnName("SPACEID").IsOptional().HasMaxLength(200);

            // Foreign keys
            HasOptional(a => a.T_Borrow).WithMany(b => b.T_Borrow_FileInfo).HasForeignKey(c => c.T_BorrowID); // FK_T_Borrow_FileInfo_T_Borrow
        }
    }

    // T_D_DownloadApply
    internal partial class T_D_DownloadApplyConfiguration : EntityTypeConfiguration<T_D_DownloadApply>
    {
        public T_D_DownloadApplyConfiguration()
        {
            ToTable("T_D_DOWNLOADAPPLY");
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
            Property(x => x.ApplyUser).HasColumnName("APPLYUSER").IsOptional().HasMaxLength(200);
            Property(x => x.ApplyUserName).HasColumnName("APPLYUSERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ApplyDept).HasColumnName("APPLYDEPT").IsOptional().HasMaxLength(200);
            Property(x => x.ApplyDeptName).HasColumnName("APPLYDEPTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ExtensionNumber).HasColumnName("EXTENSIONNUMBER").IsOptional().HasMaxLength(200);
            Property(x => x.ApplyDate).HasColumnName("APPLYDATE").IsOptional();
            Property(x => x.Remarks).HasColumnName("REMARKS").IsOptional().HasMaxLength(500);
            Property(x => x.DocManagerSign).HasColumnName("DOCMANAGERSIGN").IsOptional();
        }
    }

    // T_D_DownloadApply_DetailInfo
    internal partial class T_D_DownloadApply_DetailInfoConfiguration : EntityTypeConfiguration<T_D_DownloadApply_DetailInfo>
    {
        public T_D_DownloadApply_DetailInfoConfiguration()
        {
            ToTable("T_D_DOWNLOADAPPLY_DETAILINFO");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.T_D_DownloadApplyID).HasColumnName("T_D_DOWNLOADAPPLYID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(200);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(200);
            Property(x => x.SpaceName).HasColumnName("SPACENAME").IsOptional().HasMaxLength(200);
            Property(x => x.DataType).HasColumnName("DATATYPE").IsOptional().HasMaxLength(200);
            Property(x => x.CarInfoID).HasColumnName("CARINFOID").IsOptional().HasMaxLength(50);

            // Foreign keys
            HasOptional(a => a.T_D_DownloadApply).WithMany(b => b.T_D_DownloadApply_DetailInfo).HasForeignKey(c => c.T_D_DownloadApplyID); // FK_T_D_DownloadApply_DetailInfo_T_D_DownloadApply
        }
    }

    // T_Download
    internal partial class T_DownloadConfiguration : EntityTypeConfiguration<T_Download>
    {
        public T_DownloadConfiguration()
        {
            ToTable("T_DOWNLOAD");
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
            Property(x => x.UserDept).HasColumnName("USERDEPT").IsOptional().HasMaxLength(200);
            Property(x => x.UserDeptName).HasColumnName("USERDEPTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Purpose).HasColumnName("PURPOSE").IsOptional().HasMaxLength(500);
            Property(x => x.CreateUserName).HasColumnName("CREATEUSERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(200);
            Property(x => x.Type).HasColumnName("TYPE").IsOptional().HasMaxLength(200);
            Property(x => x.ApplyDate).HasColumnName("APPLYDATE").IsOptional();
            Property(x => x.PassDate).HasColumnName("PASSDATE").IsOptional();
            Property(x => x.DownloadState).HasColumnName("DOWNLOADSTATE").IsOptional().HasMaxLength(200);
            Property(x => x.SpaceID).HasColumnName("SPACEID").IsOptional().HasMaxLength(200);
            Property(x => x.FlowID).HasColumnName("FLOWID").IsOptional().HasMaxLength(200);
        }
    }

    // T_Download_FileInfo
    internal partial class T_Download_FileInfoConfiguration : EntityTypeConfiguration<T_Download_FileInfo>
    {
        public T_Download_FileInfoConfiguration()
        {
            ToTable("T_DOWNLOAD_FILEINFO");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.T_DownloadID).HasColumnName("T_DOWNLOADID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.FileCode).HasColumnName("FILECODE").IsOptional().HasMaxLength(200);
            Property(x => x.FileName).HasColumnName("FILENAME").IsOptional().HasMaxLength(200);
            Property(x => x.FileID).HasColumnName("FILEID").IsOptional().HasMaxLength(200);
            Property(x => x.CarInfoID).HasColumnName("CARINFOID").IsOptional().HasMaxLength(200);
            Property(x => x.ConfigID).HasColumnName("CONFIGID").IsOptional().HasMaxLength(200);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.Attachments).HasColumnName("ATTACHMENTS").IsOptional().HasMaxLength(200);
            Property(x => x.NodeID).HasColumnName("NODEID").IsOptional().HasMaxLength(200);
            Property(x => x.DataType).HasColumnName("DATATYPE").IsOptional().HasMaxLength(200);
            Property(x => x.SpaceID).HasColumnName("SPACEID").IsOptional().HasMaxLength(200);

            // Foreign keys
            HasOptional(a => a.T_Download).WithMany(b => b.T_Download_FileInfo).HasForeignKey(c => c.T_DownloadID); // FK_T_Download_FileInfo_T_Download
        }
    }

    // T_Lose
    internal partial class T_LoseConfiguration : EntityTypeConfiguration<T_Lose>
    {
        public T_LoseConfiguration()
        {
            ToTable("T_LOSE");
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
            Property(x => x.LosePeople).HasColumnName("LOSEPEOPLE").IsOptional().HasMaxLength(200);
            Property(x => x.LosePeopleName).HasColumnName("LOSEPEOPLENAME").IsOptional().HasMaxLength(200);
            Property(x => x.LoseDept).HasColumnName("LOSEDEPT").IsOptional().HasMaxLength(200);
            Property(x => x.LoseDeptName).HasColumnName("LOSEDEPTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.LostDetail).HasColumnName("LOSTDETAIL").IsOptional().HasMaxLength(1073741823);
        }
    }

    // T_Lose_LostDetail
    internal partial class T_Lose_LostDetailConfiguration : EntityTypeConfiguration<T_Lose_LostDetail>
    {
        public T_Lose_LostDetailConfiguration()
        {
            ToTable("T_LOSE_LOSTDETAIL");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.T_LoseID).HasColumnName("T_LOSEID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.LoseDamageState).HasColumnName("LOSEDAMAGESTATE").IsOptional().HasMaxLength(200);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(200);
            Property(x => x.ConfigID).HasColumnName("CONFIGID").IsOptional().HasMaxLength(200);
            Property(x => x.SpaceID).HasColumnName("SPACEID").IsOptional().HasMaxLength(200);
            Property(x => x.Quantity).HasColumnName("QUANTITY").IsOptional();
            Property(x => x.StorageNum).HasColumnName("STORAGENUM").IsOptional().HasMaxLength(200);
            Property(x => x.LoseDate).HasColumnName("LOSEDATE").IsOptional();
            Property(x => x.LoseExplain).HasColumnName("LOSEEXPLAIN").IsOptional().HasMaxLength(200);
            Property(x => x.RelateDocID).HasColumnName("RELATEDOCID").IsOptional().HasMaxLength(200);
            Property(x => x.RelateDocType).HasColumnName("RELATEDOCTYPE").IsOptional().HasMaxLength(200);
            Property(x => x.LoseCount).HasColumnName("LOSECOUNT").IsOptional().HasMaxLength(200);
            Property(x => x.DocumentCode).HasColumnName("DOCUMENTCODE").IsOptional().HasMaxLength(200);

            // Foreign keys
            HasOptional(a => a.T_Lose).WithMany(b => b.T_Lose_LostDetail).HasForeignKey(c => c.T_LoseID); // FK_T_Lose_LostDetail_T_Lose
        }
    }

    // T_Replenish
    internal partial class T_ReplenishConfiguration : EntityTypeConfiguration<T_Replenish>
    {
        public T_ReplenishConfiguration()
        {
            ToTable("T_REPLENISH");
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
            Property(x => x.ReplenishPeople).HasColumnName("REPLENISHPEOPLE").IsOptional().HasMaxLength(200);
            Property(x => x.ReplenishPeopleName).HasColumnName("REPLENISHPEOPLENAME").IsOptional().HasMaxLength(200);
            Property(x => x.ReplenishDept).HasColumnName("REPLENISHDEPT").IsOptional().HasMaxLength(200);
            Property(x => x.ReplenishDeptName).HasColumnName("REPLENISHDEPTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Detail).HasColumnName("DETAIL").IsOptional().HasMaxLength(1073741823);
        }
    }

    // T_Replenish_Detail
    internal partial class T_Replenish_DetailConfiguration : EntityTypeConfiguration<T_Replenish_Detail>
    {
        public T_Replenish_DetailConfiguration()
        {
            ToTable("T_REPLENISH_DETAIL");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.T_ReplenishID).HasColumnName("T_REPLENISHID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.ReplenishDate).HasColumnName("REPLENISHDATE").IsOptional();
            Property(x => x.ReplenishExplain).HasColumnName("REPLENISHEXPLAIN").IsOptional().HasMaxLength(200);
            Property(x => x.S_A_LoseReplenishID).HasColumnName("S_A_LOSEREPLENISHID").IsOptional().HasMaxLength(200);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(200);
            Property(x => x.ConfigID).HasColumnName("CONFIGID").IsOptional().HasMaxLength(200);
            Property(x => x.SpaceID).HasColumnName("SPACEID").IsOptional().HasMaxLength(200);
            Property(x => x.LoseDate).HasColumnName("LOSEDATE").IsOptional();
            Property(x => x.LoseExplain).HasColumnName("LOSEEXPLAIN").IsOptional().HasMaxLength(200);
            Property(x => x.LosePeople).HasColumnName("LOSEPEOPLE").IsOptional().HasMaxLength(200);
            Property(x => x.LosePeopleName).HasColumnName("LOSEPEOPLENAME").IsOptional().HasMaxLength(200);
            Property(x => x.LoseDept).HasColumnName("LOSEDEPT").IsOptional().HasMaxLength(200);
            Property(x => x.LoseDeptName).HasColumnName("LOSEDEPTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ReplenishState).HasColumnName("REPLENISHSTATE").IsOptional().HasMaxLength(200);
            Property(x => x.RelateDocID).HasColumnName("RELATEDOCID").IsOptional().HasMaxLength(200);
            Property(x => x.RelateDocType).HasColumnName("RELATEDOCTYPE").IsOptional().HasMaxLength(200);
            Property(x => x.LoseCount).HasColumnName("LOSECOUNT").IsOptional().HasMaxLength(200);
            Property(x => x.ReplenishCount).HasColumnName("REPLENISHCOUNT").IsOptional();
            Property(x => x.State).HasColumnName("STATE").IsOptional().HasMaxLength(200);
            Property(x => x.DocumentCode).HasColumnName("DOCUMENTCODE").IsOptional().HasMaxLength(200);

            // Foreign keys
            HasOptional(a => a.T_Replenish).WithMany(b => b.T_Replenish_Detail).HasForeignKey(c => c.T_ReplenishID); // FK_T_Replenish_Detail_T_Replenish
        }
    }

}

