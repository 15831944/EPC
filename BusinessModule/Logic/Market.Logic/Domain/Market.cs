

// This file was automatically generated.
// Do not make changes directly to this file - edit the template instead.
// 
// The following connection settings were used to generate this file
// 
//     Connection String Name: "Market"
//     Connection String:      "Data Source=.;Initial Catalog=EPM_Market;User ID=sa;PWD=123.zxc;"

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

namespace Market.Logic.Domain
{
    // ************************************************************************
    // Database context
    public partial class MarketEntities : Formula.FormulaDbContext
    {
        public IDbSet<S_B_Bid> S_B_Bid { get; set; } // S_B_Bid
        public IDbSet<S_B_Bond> S_B_Bond { get; set; } // S_B_Bond
        public IDbSet<S_B_BondReturn> S_B_BondReturn { get; set; } // S_B_BondReturn
        public IDbSet<S_B_PerformanceBond> S_B_PerformanceBond { get; set; } // S_B_PerformanceBond
        public IDbSet<S_C_AcceptanceBill> S_C_AcceptanceBill { get; set; } // S_C_AcceptanceBill
        public IDbSet<S_C_Deposit> S_C_Deposit { get; set; } // S_C_Deposit
        public IDbSet<S_C_Invoice> S_C_Invoice { get; set; } // S_C_Invoice
        public IDbSet<S_C_Invoice_InvoiceDatail> S_C_Invoice_InvoiceDatail { get; set; } // S_C_Invoice_InvoiceDatail
        public IDbSet<S_C_Invoice_ReceiptObject> S_C_Invoice_ReceiptObject { get; set; } // S_C_Invoice_ReceiptObject
        public IDbSet<S_C_InvoiceReceiptRelation> S_C_InvoiceReceiptRelation { get; set; } // S_C_InvoiceReceiptRelation
        public IDbSet<S_C_ManageContract> S_C_ManageContract { get; set; } // S_C_ManageContract
        public IDbSet<S_C_ManageContract_ContractSplit> S_C_ManageContract_ContractSplit { get; set; } // S_C_ManageContract_ContractSplit
        public IDbSet<S_C_ManageContract_DeptRelation> S_C_ManageContract_DeptRelation { get; set; } // S_C_ManageContract_DeptRelation
        public IDbSet<S_C_ManageContract_PaymentDetail> S_C_ManageContract_PaymentDetail { get; set; } // S_C_ManageContract_PaymentDetail
        public IDbSet<S_C_ManageContract_ProjectRelation> S_C_ManageContract_ProjectRelation { get; set; } // S_C_ManageContract_ProjectRelation
        public IDbSet<S_C_ManageContract_ReceiptObj> S_C_ManageContract_ReceiptObj { get; set; } // S_C_ManageContract_ReceiptObj
        public IDbSet<S_C_ManageContract_Supplementary> S_C_ManageContract_Supplementary { get; set; } // S_C_ManageContract_Supplementary
        public IDbSet<S_C_ManageContract_Supplementary_DeptRelation> S_C_ManageContract_Supplementary_DeptRelation { get; set; } // S_C_ManageContract_Supplementary_DeptRelation
        public IDbSet<S_C_ManageContract_Supplementary_ReceiptObj> S_C_ManageContract_Supplementary_ReceiptObj { get; set; } // S_C_ManageContract_Supplementary_ReceiptObj
        public IDbSet<S_C_PlanReceipt> S_C_PlanReceipt { get; set; } // S_C_PlanReceipt
        public IDbSet<S_C_Receipt> S_C_Receipt { get; set; } // S_C_Receipt
        public IDbSet<S_C_Receipt_DeptRelation> S_C_Receipt_DeptRelation { get; set; } // S_C_Receipt_DeptRelation
        public IDbSet<S_C_Receipt_ProjectRelation> S_C_Receipt_ProjectRelation { get; set; } // S_C_Receipt_ProjectRelation
        public IDbSet<S_C_ReceiptPlanRelation> S_C_ReceiptPlanRelation { get; set; } // S_C_ReceiptPlanRelation
        public IDbSet<S_C_ReceiptRegister> S_C_ReceiptRegister { get; set; } // S_C_ReceiptRegister
        public IDbSet<S_F_Customer> S_F_Customer { get; set; } // S_F_Customer
        public IDbSet<S_F_Customer_BankAccounts> S_F_Customer_BankAccounts { get; set; } // S_F_Customer_BankAccounts
        public IDbSet<S_F_Customer_BigEvent> S_F_Customer_BigEvent { get; set; } // S_F_Customer_BigEvent
        public IDbSet<S_F_Customer_Complain> S_F_Customer_Complain { get; set; } // S_F_Customer_Complain
        public IDbSet<S_F_Customer_ContactLog> S_F_Customer_ContactLog { get; set; } // S_F_Customer_ContactLog
        public IDbSet<S_F_Customer_Demand> S_F_Customer_Demand { get; set; } // S_F_Customer_Demand
        public IDbSet<S_F_Customer_LinkMan> S_F_Customer_LinkMan { get; set; } // S_F_Customer_LinkMan
        public IDbSet<S_F_Customer_ReturnVisit> S_F_Customer_ReturnVisit { get; set; } // S_F_Customer_ReturnVisit
        public IDbSet<S_FC_CostInfo> S_FC_CostInfo { get; set; } // S_FC_CostInfo
        public IDbSet<S_I_Engineering> S_I_Engineering { get; set; } // S_I_Engineering
        public IDbSet<S_I_Project> S_I_Project { get; set; } // S_I_Project
        public IDbSet<S_KPI_Category> S_KPI_Category { get; set; } // S_KPI_Category
        public IDbSet<S_KPI_IndicatorCompany> S_KPI_IndicatorCompany { get; set; } // S_KPI_IndicatorCompany
        public IDbSet<S_KPI_IndicatorConfig> S_KPI_IndicatorConfig { get; set; } // S_KPI_IndicatorConfig
        public IDbSet<S_KPI_IndicatorOrg> S_KPI_IndicatorOrg { get; set; } // S_KPI_IndicatorOrg
        public IDbSet<S_KPI_IndicatorPerson> S_KPI_IndicatorPerson { get; set; } // S_KPI_IndicatorPerson
        public IDbSet<S_KPITemp_IndicatorCompany> S_KPITemp_IndicatorCompany { get; set; } // S_KPITemp_IndicatorCompany
        public IDbSet<S_KPITemp_IndicatorOrg> S_KPITemp_IndicatorOrg { get; set; } // S_KPITemp_IndicatorOrg
        public IDbSet<S_KPITemp_IndicatorPerson> S_KPITemp_IndicatorPerson { get; set; } // S_KPITemp_IndicatorPerson
        public IDbSet<S_P_MarketClue> S_P_MarketClue { get; set; } // S_P_MarketClue
        public IDbSet<S_P_MarketClue_TraceInfo> S_P_MarketClue_TraceInfo { get; set; } // S_P_MarketClue_TraceInfo
        public IDbSet<S_SP_Invoice> S_SP_Invoice { get; set; } // S_SP_Invoice
        public IDbSet<S_SP_Payment> S_SP_Payment { get; set; } // S_SP_Payment
        public IDbSet<S_SP_Payment_AcceptanceBill> S_SP_Payment_AcceptanceBill { get; set; } // S_SP_Payment_AcceptanceBill
        public IDbSet<S_SP_Payment_CostInfo> S_SP_Payment_CostInfo { get; set; } // S_SP_Payment_CostInfo
        public IDbSet<S_SP_Payment_InvoiceRelation> S_SP_Payment_InvoiceRelation { get; set; } // S_SP_Payment_InvoiceRelation
        public IDbSet<S_SP_PaymentPlan> S_SP_PaymentPlan { get; set; } // S_SP_PaymentPlan
        public IDbSet<S_SP_Supplier> S_SP_Supplier { get; set; } // S_SP_Supplier
        public IDbSet<S_SP_Supplier_BankInfo> S_SP_Supplier_BankInfo { get; set; } // S_SP_Supplier_BankInfo
        public IDbSet<S_SP_Supplier_ContactUserInfo> S_SP_Supplier_ContactUserInfo { get; set; } // S_SP_Supplier_ContactUserInfo
        public IDbSet<S_SP_Supplier_CoopProjectInfo> S_SP_Supplier_CoopProjectInfo { get; set; } // S_SP_Supplier_CoopProjectInfo
        public IDbSet<S_SP_Supplier_CredentialInfo> S_SP_Supplier_CredentialInfo { get; set; } // S_SP_Supplier_CredentialInfo
        public IDbSet<S_SP_SupplierContract> S_SP_SupplierContract { get; set; } // S_SP_SupplierContract
        public IDbSet<S_SP_SupplierContract_ContractSplit> S_SP_SupplierContract_ContractSplit { get; set; } // S_SP_SupplierContract_ContractSplit
        public IDbSet<S_SP_SupplierContract_Supplementary> S_SP_SupplierContract_Supplementary { get; set; } // S_SP_SupplierContract_Supplementary
        public IDbSet<T_B_BidApply> T_B_BidApply { get; set; } // T_B_BidApply
        public IDbSet<T_B_BidProcessApply> T_B_BidProcessApply { get; set; } // T_B_BidProcessApply
        public IDbSet<T_B_BondApply> T_B_BondApply { get; set; } // T_B_BondApply
        public IDbSet<T_BusinessLeads> T_BusinessLeads { get; set; } // T_BusinessLeads
        public IDbSet<T_BusinessLeads_Linkman> T_BusinessLeads_Linkman { get; set; } // T_BusinessLeads_Linkman
        public IDbSet<T_BusinessLeads_Records> T_BusinessLeads_Records { get; set; } // T_BusinessLeads_Records
        public IDbSet<T_C_ContractChange> T_C_ContractChange { get; set; } // T_C_ContractChange
        public IDbSet<T_C_ContractChange_ContractSplit> T_C_ContractChange_ContractSplit { get; set; } // T_C_ContractChange_ContractSplit
        public IDbSet<T_C_ContractChange_DeptRelation> T_C_ContractChange_DeptRelation { get; set; } // T_C_ContractChange_DeptRelation
        public IDbSet<T_C_ContractChange_PaymentDetail> T_C_ContractChange_PaymentDetail { get; set; } // T_C_ContractChange_PaymentDetail
        public IDbSet<T_C_ContractChange_ProjectRelation> T_C_ContractChange_ProjectRelation { get; set; } // T_C_ContractChange_ProjectRelation
        public IDbSet<T_C_ContractChange_ReceiptObj> T_C_ContractChange_ReceiptObj { get; set; } // T_C_ContractChange_ReceiptObj
        public IDbSet<T_C_ContractReview> T_C_ContractReview { get; set; } // T_C_ContractReview
        public IDbSet<T_C_CreditNoteApply> T_C_CreditNoteApply { get; set; } // T_C_CreditNoteApply
        public IDbSet<T_C_CreditNoteApply_Detail> T_C_CreditNoteApply_Detail { get; set; } // T_C_CreditNoteApply_Detail
        public IDbSet<T_C_GuaranteeLetter> T_C_GuaranteeLetter { get; set; } // T_C_GuaranteeLetter
        public IDbSet<T_C_InvoiceApply> T_C_InvoiceApply { get; set; } // T_C_InvoiceApply
        public IDbSet<T_C_InvoiceApply_Detail> T_C_InvoiceApply_Detail { get; set; } // T_C_InvoiceApply_Detail
        public IDbSet<T_C_InvoiceMakeUpApply> T_C_InvoiceMakeUpApply { get; set; } // T_C_InvoiceMakeUpApply
        public IDbSet<T_C_InvoiceMakeUpApply_Detail> T_C_InvoiceMakeUpApply_Detail { get; set; } // T_C_InvoiceMakeUpApply_Detail
        public IDbSet<T_CP_CustomerRequestReview> T_CP_CustomerRequestReview { get; set; } // T_CP_CustomerRequestReview
        public IDbSet<T_F_GuaranteeLetterApply> T_F_GuaranteeLetterApply { get; set; } // T_F_GuaranteeLetterApply
        public IDbSet<T_F_GuaranteeLetterTerminate> T_F_GuaranteeLetterTerminate { get; set; } // T_F_GuaranteeLetterTerminate
        public IDbSet<T_SP_ContractChange> T_SP_ContractChange { get; set; } // T_SP_ContractChange
        public IDbSet<T_SP_ContractChange_ContractSplit> T_SP_ContractChange_ContractSplit { get; set; } // T_SP_ContractChange_ContractSplit
        public IDbSet<T_SP_ContractReview> T_SP_ContractReview { get; set; } // T_SP_ContractReview
        public IDbSet<T_SP_DesignApproval> T_SP_DesignApproval { get; set; } // T_SP_DesignApproval
        public IDbSet<T_SP_DesignApproval_CoopProjectInfo> T_SP_DesignApproval_CoopProjectInfo { get; set; } // T_SP_DesignApproval_CoopProjectInfo
        public IDbSet<T_SP_DesignApproval_CredentialInfo> T_SP_DesignApproval_CredentialInfo { get; set; } // T_SP_DesignApproval_CredentialInfo
        public IDbSet<T_SP_DesignReview> T_SP_DesignReview { get; set; } // T_SP_DesignReview
        public IDbSet<T_SP_PaymentApply> T_SP_PaymentApply { get; set; } // T_SP_PaymentApply
        public IDbSet<T_SP_PaymentApply_AcceptanceBill> T_SP_PaymentApply_AcceptanceBill { get; set; } // T_SP_PaymentApply_AcceptanceBill
        public IDbSet<T_SP_PaymentApply_CostInfo> T_SP_PaymentApply_CostInfo { get; set; } // T_SP_PaymentApply_CostInfo
        public IDbSet<T_SP_PaymentApply_InvoiceRelation> T_SP_PaymentApply_InvoiceRelation { get; set; } // T_SP_PaymentApply_InvoiceRelation
        public IDbSet<T_SP_PaymentApplyConfirm> T_SP_PaymentApplyConfirm { get; set; } // T_SP_PaymentApplyConfirm
        public IDbSet<view_ContractCustomerList> view_ContractCustomerList { get; set; } // view_ContractCustomerList
        public IDbSet<view_S_B_BidFileInfo> view_S_B_BidFileInfo { get; set; } // view_S_B_BidFileInfo
        public IDbSet<view_S_B_ContractFileInfo> view_S_B_ContractFileInfo { get; set; } // view_S_B_ContractFileInfo
        public IDbSet<view_S_B_MarketClueFileInfo> view_S_B_MarketClueFileInfo { get; set; } // view_S_B_MarketClueFileInfo

        static MarketEntities()
        {
            Database.SetInitializer<MarketEntities>(null);
        }

        public MarketEntities()
            : base("Name=Market")
        {
        }

        public MarketEntities(string connectionString) : base(connectionString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new S_B_BidConfiguration());
            modelBuilder.Configurations.Add(new S_B_BondConfiguration());
            modelBuilder.Configurations.Add(new S_B_BondReturnConfiguration());
            modelBuilder.Configurations.Add(new S_B_PerformanceBondConfiguration());
            modelBuilder.Configurations.Add(new S_C_AcceptanceBillConfiguration());
            modelBuilder.Configurations.Add(new S_C_DepositConfiguration());
            modelBuilder.Configurations.Add(new S_C_InvoiceConfiguration());
            modelBuilder.Configurations.Add(new S_C_Invoice_InvoiceDatailConfiguration());
            modelBuilder.Configurations.Add(new S_C_Invoice_ReceiptObjectConfiguration());
            modelBuilder.Configurations.Add(new S_C_InvoiceReceiptRelationConfiguration());
            modelBuilder.Configurations.Add(new S_C_ManageContractConfiguration());
            modelBuilder.Configurations.Add(new S_C_ManageContract_ContractSplitConfiguration());
            modelBuilder.Configurations.Add(new S_C_ManageContract_DeptRelationConfiguration());
            modelBuilder.Configurations.Add(new S_C_ManageContract_PaymentDetailConfiguration());
            modelBuilder.Configurations.Add(new S_C_ManageContract_ProjectRelationConfiguration());
            modelBuilder.Configurations.Add(new S_C_ManageContract_ReceiptObjConfiguration());
            modelBuilder.Configurations.Add(new S_C_ManageContract_SupplementaryConfiguration());
            modelBuilder.Configurations.Add(new S_C_ManageContract_Supplementary_DeptRelationConfiguration());
            modelBuilder.Configurations.Add(new S_C_ManageContract_Supplementary_ReceiptObjConfiguration());
            modelBuilder.Configurations.Add(new S_C_PlanReceiptConfiguration());
            modelBuilder.Configurations.Add(new S_C_ReceiptConfiguration());
            modelBuilder.Configurations.Add(new S_C_Receipt_DeptRelationConfiguration());
            modelBuilder.Configurations.Add(new S_C_Receipt_ProjectRelationConfiguration());
            modelBuilder.Configurations.Add(new S_C_ReceiptPlanRelationConfiguration());
            modelBuilder.Configurations.Add(new S_C_ReceiptRegisterConfiguration());
            modelBuilder.Configurations.Add(new S_F_CustomerConfiguration());
            modelBuilder.Configurations.Add(new S_F_Customer_BankAccountsConfiguration());
            modelBuilder.Configurations.Add(new S_F_Customer_BigEventConfiguration());
            modelBuilder.Configurations.Add(new S_F_Customer_ComplainConfiguration());
            modelBuilder.Configurations.Add(new S_F_Customer_ContactLogConfiguration());
            modelBuilder.Configurations.Add(new S_F_Customer_DemandConfiguration());
            modelBuilder.Configurations.Add(new S_F_Customer_LinkManConfiguration());
            modelBuilder.Configurations.Add(new S_F_Customer_ReturnVisitConfiguration());
            modelBuilder.Configurations.Add(new S_FC_CostInfoConfiguration());
            modelBuilder.Configurations.Add(new S_I_EngineeringConfiguration());
            modelBuilder.Configurations.Add(new S_I_ProjectConfiguration());
            modelBuilder.Configurations.Add(new S_KPI_CategoryConfiguration());
            modelBuilder.Configurations.Add(new S_KPI_IndicatorCompanyConfiguration());
            modelBuilder.Configurations.Add(new S_KPI_IndicatorConfigConfiguration());
            modelBuilder.Configurations.Add(new S_KPI_IndicatorOrgConfiguration());
            modelBuilder.Configurations.Add(new S_KPI_IndicatorPersonConfiguration());
            modelBuilder.Configurations.Add(new S_KPITemp_IndicatorCompanyConfiguration());
            modelBuilder.Configurations.Add(new S_KPITemp_IndicatorOrgConfiguration());
            modelBuilder.Configurations.Add(new S_KPITemp_IndicatorPersonConfiguration());
            modelBuilder.Configurations.Add(new S_P_MarketClueConfiguration());
            modelBuilder.Configurations.Add(new S_P_MarketClue_TraceInfoConfiguration());
            modelBuilder.Configurations.Add(new S_SP_InvoiceConfiguration());
            modelBuilder.Configurations.Add(new S_SP_PaymentConfiguration());
            modelBuilder.Configurations.Add(new S_SP_Payment_AcceptanceBillConfiguration());
            modelBuilder.Configurations.Add(new S_SP_Payment_CostInfoConfiguration());
            modelBuilder.Configurations.Add(new S_SP_Payment_InvoiceRelationConfiguration());
            modelBuilder.Configurations.Add(new S_SP_PaymentPlanConfiguration());
            modelBuilder.Configurations.Add(new S_SP_SupplierConfiguration());
            modelBuilder.Configurations.Add(new S_SP_Supplier_BankInfoConfiguration());
            modelBuilder.Configurations.Add(new S_SP_Supplier_ContactUserInfoConfiguration());
            modelBuilder.Configurations.Add(new S_SP_Supplier_CoopProjectInfoConfiguration());
            modelBuilder.Configurations.Add(new S_SP_Supplier_CredentialInfoConfiguration());
            modelBuilder.Configurations.Add(new S_SP_SupplierContractConfiguration());
            modelBuilder.Configurations.Add(new S_SP_SupplierContract_ContractSplitConfiguration());
            modelBuilder.Configurations.Add(new S_SP_SupplierContract_SupplementaryConfiguration());
            modelBuilder.Configurations.Add(new T_B_BidApplyConfiguration());
            modelBuilder.Configurations.Add(new T_B_BidProcessApplyConfiguration());
            modelBuilder.Configurations.Add(new T_B_BondApplyConfiguration());
            modelBuilder.Configurations.Add(new T_BusinessLeadsConfiguration());
            modelBuilder.Configurations.Add(new T_BusinessLeads_LinkmanConfiguration());
            modelBuilder.Configurations.Add(new T_BusinessLeads_RecordsConfiguration());
            modelBuilder.Configurations.Add(new T_C_ContractChangeConfiguration());
            modelBuilder.Configurations.Add(new T_C_ContractChange_ContractSplitConfiguration());
            modelBuilder.Configurations.Add(new T_C_ContractChange_DeptRelationConfiguration());
            modelBuilder.Configurations.Add(new T_C_ContractChange_PaymentDetailConfiguration());
            modelBuilder.Configurations.Add(new T_C_ContractChange_ProjectRelationConfiguration());
            modelBuilder.Configurations.Add(new T_C_ContractChange_ReceiptObjConfiguration());
            modelBuilder.Configurations.Add(new T_C_ContractReviewConfiguration());
            modelBuilder.Configurations.Add(new T_C_CreditNoteApplyConfiguration());
            modelBuilder.Configurations.Add(new T_C_CreditNoteApply_DetailConfiguration());
            modelBuilder.Configurations.Add(new T_C_GuaranteeLetterConfiguration());
            modelBuilder.Configurations.Add(new T_C_InvoiceApplyConfiguration());
            modelBuilder.Configurations.Add(new T_C_InvoiceApply_DetailConfiguration());
            modelBuilder.Configurations.Add(new T_C_InvoiceMakeUpApplyConfiguration());
            modelBuilder.Configurations.Add(new T_C_InvoiceMakeUpApply_DetailConfiguration());
            modelBuilder.Configurations.Add(new T_CP_CustomerRequestReviewConfiguration());
            modelBuilder.Configurations.Add(new T_F_GuaranteeLetterApplyConfiguration());
            modelBuilder.Configurations.Add(new T_F_GuaranteeLetterTerminateConfiguration());
            modelBuilder.Configurations.Add(new T_SP_ContractChangeConfiguration());
            modelBuilder.Configurations.Add(new T_SP_ContractChange_ContractSplitConfiguration());
            modelBuilder.Configurations.Add(new T_SP_ContractReviewConfiguration());
            modelBuilder.Configurations.Add(new T_SP_DesignApprovalConfiguration());
            modelBuilder.Configurations.Add(new T_SP_DesignApproval_CoopProjectInfoConfiguration());
            modelBuilder.Configurations.Add(new T_SP_DesignApproval_CredentialInfoConfiguration());
            modelBuilder.Configurations.Add(new T_SP_DesignReviewConfiguration());
            modelBuilder.Configurations.Add(new T_SP_PaymentApplyConfiguration());
            modelBuilder.Configurations.Add(new T_SP_PaymentApply_AcceptanceBillConfiguration());
            modelBuilder.Configurations.Add(new T_SP_PaymentApply_CostInfoConfiguration());
            modelBuilder.Configurations.Add(new T_SP_PaymentApply_InvoiceRelationConfiguration());
            modelBuilder.Configurations.Add(new T_SP_PaymentApplyConfirmConfiguration());
            modelBuilder.Configurations.Add(new view_ContractCustomerListConfiguration());
            modelBuilder.Configurations.Add(new view_S_B_BidFileInfoConfiguration());
            modelBuilder.Configurations.Add(new view_S_B_ContractFileInfoConfiguration());
            modelBuilder.Configurations.Add(new view_S_B_MarketClueFileInfoConfiguration());
        }
    }

    // ************************************************************************
    // POCO classes

	/// <summary>投标记录</summary>	
	[Description("投标记录")]
    public partial class S_B_Bid : Formula.BaseModel
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
		/// <summary>项目名称名称</summary>	
		[Description("项目名称名称")]
        public string ProjectName { get; set; } // ProjectName
		/// <summary>投标编号</summary>	
		[Description("投标编号")]
        public string BidCode { get; set; } // BidCode
		/// <summary>投标阶段</summary>	
		[Description("投标阶段")]
        public string BidPhase { get; set; } // BidPhase
		/// <summary>关联工程</summary>	
		[Description("关联工程")]
        public string EngineeringInfo { get; set; } // EngineeringInfo
		/// <summary>关联工程名称</summary>	
		[Description("关联工程名称")]
        public string EngineeringInfoName { get; set; } // EngineeringInfoName
		/// <summary>投标下达日期</summary>	
		[Description("投标下达日期")]
        public DateTime? BidIssuedDate { get; set; } // BidIssuedDate
		/// <summary>答疑截止时间</summary>	
		[Description("答疑截止时间")]
        public DateTime? CompetencyDate { get; set; } // CompetencyDate
		/// <summary>招标编号</summary>	
		[Description("招标编号")]
        public string TenderCode { get; set; } // TenderCode
		/// <summary>招标单位</summary>	
		[Description("招标单位")]
        public string TenderOrgan { get; set; } // TenderOrgan
		/// <summary>业务类别</summary>	
		[Description("业务类别")]
        public string BusinessClass { get; set; } // BusinessClass
		/// <summary>业主单位</summary>	
		[Description("业主单位")]
        public string BusinessOwner { get; set; } // BusinessOwner
		/// <summary>项目负责人</summary>	
		[Description("项目负责人")]
        public string ProjectManager { get; set; } // ProjectManager
		/// <summary>项目负责人名称</summary>	
		[Description("项目负责人名称")]
        public string ProjectManagerName { get; set; } // ProjectManagerName
		/// <summary>承担部门</summary>	
		[Description("承担部门")]
        public string DeptInfo { get; set; } // DeptInfo
		/// <summary>承担部门名称</summary>	
		[Description("承担部门名称")]
        public string DeptInfoName { get; set; } // DeptInfoName
		/// <summary>交标时间</summary>	
		[Description("交标时间")]
        public DateTime? CompleteBidDate { get; set; } // CompleteBidDate
		/// <summary>邮政编码</summary>	
		[Description("邮政编码")]
        public string PostCode { get; set; } // PostCode
		/// <summary>详细地址</summary>	
		[Description("详细地址")]
        public string Address { get; set; } // Address
		/// <summary>备注</summary>	
		[Description("备注")]
        public string Remark { get; set; } // Remark
		/// <summary>招标代理机构</summary>	
		[Description("招标代理机构")]
        public string BidSurrogateUnit { get; set; } // BidSurrogateUnit
		/// <summary>交标地点</summary>	
		[Description("交标地点")]
        public string BidAddress { get; set; } // BidAddress
		/// <summary>投标保证金方式</summary>	
		[Description("投标保证金方式")]
        public string BidType { get; set; } // BidType
		/// <summary>招标文售价(元)</summary>	
		[Description("招标文售价(元)")]
        public decimal? BidFilePrice { get; set; } // BidFilePrice
		/// <summary>保证金（元）</summary>	
		[Description("保证金（元）")]
        public decimal? EnsureAmount { get; set; } // EnsureAmount
		/// <summary>保证金是否归还</summary>	
		[Description("保证金是否归还")]
        public string EnsureAmountIsReturn { get; set; } // EnsureAmountIsReturn
		/// <summary>保证金归还日期</summary>	
		[Description("保证金归还日期")]
        public DateTime? EnsureAmountReturnDate { get; set; } // EnsureAmountReturnDate
		/// <summary>最后中标单位</summary>	
		[Description("最后中标单位")]
        public string ResultUnit { get; set; } // ResultUnit
		/// <summary>商务标金额(万元)</summary>	
		[Description("商务标金额(万元)")]
        public decimal? TradeAmount { get; set; } // TradeAmount
		/// <summary>商务标单价</summary>	
		[Description("商务标单价")]
        public decimal? TradeUnitAmount { get; set; } // TradeUnitAmount
		/// <summary>参加投标单位情况</summary>	
		[Description("参加投标单位情况")]
        public string BidUntiRemark { get; set; } // BidUntiRemark
		/// <summary>投标结果</summary>	
		[Description("投标结果")]
        public string BidResult { get; set; } // BidResult
		/// <summary>是否中标</summary>	
		[Description("是否中标")]
        public string IsWinBid { get; set; } // IsWinBid
		/// <summary>招标文件</summary>	
		[Description("招标文件")]
        public string InviteBidFile { get; set; } // InviteBidFile
		/// <summary>招标过程澄清文件</summary>	
		[Description("招标过程澄清文件")]
        public string InviteBidClearFile { get; set; } // InviteBidClearFile
		/// <summary>招标其他文件</summary>	
		[Description("招标其他文件")]
        public string InviteBidOtherFile { get; set; } // InviteBidOtherFile
		/// <summary>投标文件</summary>	
		[Description("投标文件")]
        public string BidFile { get; set; } // BidFile
		/// <summary>投标过程澄清文件</summary>	
		[Description("投标过程澄清文件")]
        public string BidClearFile { get; set; } // BidClearFile
		/// <summary>投标其他文件</summary>	
		[Description("投标其他文件")]
        public string BidOtherFile { get; set; } // BidOtherFile
		/// <summary>项目名称</summary>	
		[Description("项目名称")]
        public string Project { get; set; } // Project
		/// <summary>业主单位名称</summary>	
		[Description("业主单位名称")]
        public string BusinessOwnerName { get; set; } // BusinessOwnerName
		/// <summary>保证金</summary>	
		[Description("保证金")]
        public string Bond { get; set; } // Bond
    }

	/// <summary>保证金登记</summary>	
	[Description("保证金登记")]
    public partial class S_B_Bond : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary>项目名称</summary>	
		[Description("项目名称")]
        public string Project { get; set; } // Project
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string IsReleased { get; set; } // IsReleased
		/// <summary>申请编号</summary>	
		[Description("申请编号")]
        public string SerialNumber { get; set; } // SerialNumber
		/// <summary>金额</summary>	
		[Description("金额")]
        public decimal? Amount { get; set; } // Amount
		/// <summary></summary>	
		[Description("")]
        public decimal? ReturnAmountTotal { get; set; } // ReturnAmountTotal
		/// <summary>截止日期</summary>	
		[Description("截止日期")]
        public DateTime? ExpiryDate { get; set; } // ExpiryDate
		/// <summary>申请人名称</summary>	
		[Description("申请人名称")]
        public string ApplierName { get; set; } // ApplierName
		/// <summary>申请部门名称</summary>	
		[Description("申请部门名称")]
        public string DepartInfoName { get; set; } // DepartInfoName
		/// <summary>申请人</summary>	
		[Description("申请人")]
        public string Applier { get; set; } // Applier
		/// <summary>申请部门</summary>	
		[Description("申请部门")]
        public string DepartInfo { get; set; } // DepartInfo
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
		/// <summary>项目名称名称</summary>	
		[Description("项目名称名称")]
        public string ProjectName { get; set; } // ProjectName
		/// <summary>申请日期</summary>	
		[Description("申请日期")]
        public DateTime? ApplyDate { get; set; } // ApplyDate
		/// <summary>项目编号</summary>	
		[Description("项目编号")]
        public string ProjectCode { get; set; } // ProjectCode
		/// <summary>项目负责人名称</summary>	
		[Description("项目负责人名称")]
        public string ProjectMangerName { get; set; } // ProjectMangerName
		/// <summary>转入单位名称</summary>	
		[Description("转入单位名称")]
        public string TransferUnit { get; set; } // TransferUnit
		/// <summary>开户行名称</summary>	
		[Description("开户行名称")]
        public string BankName { get; set; } // BankName
		/// <summary>账号</summary>	
		[Description("账号")]
        public string Account { get; set; } // Account
		/// <summary>转账附言备注</summary>	
		[Description("转账附言备注")]
        public string Remark { get; set; } // Remark
		/// <summary>招标文件</summary>	
		[Description("招标文件")]
        public string Attachment { get; set; } // Attachment
		/// <summary>项目负责人id</summary>	
		[Description("项目负责人id")]
        public string ProjectManager { get; set; } // ProjectManager
		/// <summary>保证金状态</summary>	
		[Description("保证金状态")]
        public string State { get; set; } // State
		/// <summary>项目负责人</summary>	
		[Description("项目负责人")]
        public string ProjectManger { get; set; } // ProjectManger
		/// <summary>保证金类型</summary>	
		[Description("保证金类型")]
        public string BondType { get; set; } // BondType
    }

	/// <summary>保证金归还</summary>	
	[Description("保证金归还")]
    public partial class S_B_BondReturn : Formula.BaseModel
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
		/// <summary>保证金登记ID</summary>	
		[Description("保证金登记ID")]
        public string BondID { get; set; } // BondID
		/// <summary>保证金金额</summary>	
		[Description("保证金金额")]
        public decimal? Amount { get; set; } // Amount
		/// <summary>已归还金额</summary>	
		[Description("已归还金额")]
        public decimal? LastReturnAmountTotal { get; set; } // LastReturnAmountTotal
		/// <summary>本次归还金额</summary>	
		[Description("本次归还金额")]
        public decimal? ReturnAmount { get; set; } // ReturnAmount
		/// <summary>累计归还金额</summary>	
		[Description("累计归还金额")]
        public decimal? ReturnAmountTotal { get; set; } // ReturnAmountTotal
		/// <summary>未归还金额</summary>	
		[Description("未归还金额")]
        public decimal? PerformanceAmount { get; set; } // PerformanceAmount
    }

	/// <summary>履约保证金</summary>	
	[Description("履约保证金")]
    public partial class S_B_PerformanceBond : Formula.BaseModel
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
		/// <summary>申请人ID</summary>	
		[Description("申请人ID")]
        public string ApplyUser { get; set; } // ApplyUser
		/// <summary>申请人</summary>	
		[Description("申请人")]
        public string ApplyUserName { get; set; } // ApplyUserName
		/// <summary>申请部门ID</summary>	
		[Description("申请部门ID")]
        public string ApplyDept { get; set; } // ApplyDept
		/// <summary>申请部门</summary>	
		[Description("申请部门")]
        public string ApplyDeptName { get; set; } // ApplyDeptName
		/// <summary>编号</summary>	
		[Description("编号")]
        public string SerialNumber { get; set; } // SerialNumber
		/// <summary>申请日期</summary>	
		[Description("申请日期")]
        public DateTime? ApplyDate { get; set; } // ApplyDate
		/// <summary>合同</summary>	
		[Description("合同")]
        public string Contract { get; set; } // Contract
		/// <summary>合同名称</summary>	
		[Description("合同名称")]
        public string ContractName { get; set; } // ContractName
		/// <summary>合同编号</summary>	
		[Description("合同编号")]
        public string ContractCode { get; set; } // ContractCode
		/// <summary>项目</summary>	
		[Description("项目")]
        public string Project { get; set; } // Project
		/// <summary>项目名称</summary>	
		[Description("项目名称")]
        public string ProjectName { get; set; } // ProjectName
		/// <summary>项目编号</summary>	
		[Description("项目编号")]
        public string ProjectCode { get; set; } // ProjectCode
		/// <summary>申请金额（元）</summary>	
		[Description("申请金额（元）")]
        public decimal? ApplyAmount { get; set; } // ApplyAmount
		/// <summary>金额大写</summary>	
		[Description("金额大写")]
        public string ApplyAmountDX { get; set; } // ApplyAmountDX
		/// <summary>汇款方式</summary>	
		[Description("汇款方式")]
        public string RemittanceMethod { get; set; } // RemittanceMethod
		/// <summary>收款账号</summary>	
		[Description("收款账号")]
        public string CollectionAccount { get; set; } // CollectionAccount
		/// <summary>开户行名称</summary>	
		[Description("开户行名称")]
        public string BankName { get; set; } // BankName
		/// <summary>收款单位</summary>	
		[Description("收款单位")]
        public string Payee { get; set; } // Payee
		/// <summary>收款单位名称</summary>	
		[Description("收款单位名称")]
        public string PayeeName { get; set; } // PayeeName
		/// <summary>收款单位地址</summary>	
		[Description("收款单位地址")]
        public string PayeeAddress { get; set; } // PayeeAddress
		/// <summary>联系人</summary>	
		[Description("联系人")]
        public string LinkMan { get; set; } // LinkMan
		/// <summary>联系人电话</summary>	
		[Description("联系人电话")]
        public string LinkManPhone { get; set; } // LinkManPhone
		/// <summary>预计退回时间</summary>	
		[Description("预计退回时间")]
        public DateTime? ExpectedReturnDate { get; set; } // ExpectedReturnDate
		/// <summary>要求付款时间</summary>	
		[Description("要求付款时间")]
        public DateTime? RequestPaymentDate { get; set; } // RequestPaymentDate
		/// <summary>是否退回</summary>	
		[Description("是否退回")]
        public string IsSendBack { get; set; } // IsSendBack
		/// <summary>退回时间</summary>	
		[Description("退回时间")]
        public DateTime? SendBackDate { get; set; } // SendBackDate
		/// <summary>退回人</summary>	
		[Description("退回人")]
        public string SendBackUser { get; set; } // SendBackUser
		/// <summary>退回人名称</summary>	
		[Description("退回人名称")]
        public string SendBackUserName { get; set; } // SendBackUserName
		/// <summary>附件</summary>	
		[Description("附件")]
        public string Attachment { get; set; } // Attachment
		/// <summary>备注</summary>	
		[Description("备注")]
        public string Remarks { get; set; } // Remarks
    }

	/// <summary>承兑汇票登记</summary>	
	[Description("承兑汇票登记")]
    public partial class S_C_AcceptanceBill : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string ReceiptIDs { get; set; } // ReceiptIDs
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
		/// <summary>付款单位</summary>	
		[Description("付款单位")]
        public string Customer { get; set; } // Customer
		/// <summary>付款单位名称</summary>	
		[Description("付款单位名称")]
        public string CustomerName { get; set; } // CustomerName
		/// <summary>汇票金额(元)</summary>	
		[Description("汇票金额(元)")]
        public decimal? Amount { get; set; } // Amount
		/// <summary>大写</summary>	
		[Description("大写")]
        public string AmountDX { get; set; } // AmountDX
		/// <summary>汇票号</summary>	
		[Description("汇票号")]
        public string SerialNumber { get; set; } // SerialNumber
		/// <summary>收票日期</summary>	
		[Description("收票日期")]
        public DateTime? BillDate { get; set; } // BillDate
		/// <summary>关联合同</summary>	
		[Description("关联合同")]
        public string Contract { get; set; } // Contract
		/// <summary>关联开票</summary>	
		[Description("关联开票")]
        public string Invoice { get; set; } // Invoice
		/// <summary>关联计划收款</summary>	
		[Description("关联计划收款")]
        public string PlanReceipt { get; set; } // PlanReceipt
		/// <summary>支付状态</summary>	
		[Description("支付状态")]
        public string State { get; set; } // State
		/// <summary>收票人</summary>	
		[Description("收票人")]
        public string Receiver { get; set; } // Receiver
		/// <summary>收票人名称</summary>	
		[Description("收票人名称")]
        public string ReceiverName { get; set; } // ReceiverName
		/// <summary>汇票到期日期</summary>	
		[Description("汇票到期日期")]
        public DateTime? ExpireDate { get; set; } // ExpireDate
		/// <summary>备注</summary>	
		[Description("备注")]
        public string Remark { get; set; } // Remark
    }

	/// <summary>保证金登记</summary>	
	[Description("保证金登记")]
    public partial class S_C_Deposit : Formula.BaseModel
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
		/// <summary>ProjectInfoName</summary>	
		[Description("ProjectInfoName")]
        public string ProjectInfoName { get; set; } // ProjectInfoName
		/// <summary>金额(元)</summary>	
		[Description("金额(元)")]
        public double? Amount { get; set; } // Amount
		/// <summary>是否归还</summary>	
		[Description("是否归还")]
        public string IsReturn { get; set; } // IsReturn
		/// <summary>用途</summary>	
		[Description("用途")]
        public string Purpose { get; set; } // Purpose
		/// <summary>归还时间</summary>	
		[Description("归还时间")]
        public DateTime? ReturnDate { get; set; } // ReturnDate
    }

	/// <summary>开票登记</summary>	
	[Description("开票登记")]
    public partial class S_C_Invoice : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string ContractInfoID { get; set; } // ContractInfoID
		/// <summary>ContractCode</summary>	
		[Description("ContractCode")]
        public string ContractCode { get; set; } // ContractCode
		/// <summary>ContractName</summary>	
		[Description("ContractName")]
        public string ContractName { get; set; } // ContractName
		/// <summary>付款单位</summary>	
		[Description("付款单位")]
        public string PayerUnitID { get; set; } // PayerUnitID
		/// <summary>PayerUnit</summary>	
		[Description("PayerUnit")]
        public string PayerUnit { get; set; } // PayerUnit
		/// <summary>合同甲方FullID</summary>	
		[Description("合同甲方FullID")]
        public string CustomerFullID { get; set; } // CustomerFullID
		/// <summary></summary>	
		[Description("")]
        public string InvoiceCode { get; set; } // InvoiceCode
		/// <summary></summary>	
		[Description("")]
        public string InvoiceNo { get; set; } // InvoiceNo
		/// <summary>发票类型</summary>	
		[Description("发票类型")]
        public string InvoiceType { get; set; } // InvoiceType
		/// <summary></summary>	
		[Description("")]
        public string ReceviedUnit { get; set; } // ReceviedUnit
		/// <summary></summary>	
		[Description("")]
        public string ReceviedUnitID { get; set; } // ReceviedUnitID
		/// <summary></summary>	
		[Description("")]
        public DateTime? IssuedDate { get; set; } // IssuedDate
		/// <summary>税率</summary>	
		[Description("税率")]
        public decimal? TaxRate { get; set; } // TaxRate
		/// <summary>税额</summary>	
		[Description("税额")]
        public decimal? TaxesAmount { get; set; } // TaxesAmount
		/// <summary>去税金额</summary>	
		[Description("去税金额")]
        public decimal? ClearAmount { get; set; } // ClearAmount
		/// <summary>开票金额（元）</summary>	
		[Description("开票金额（元）")]
        public decimal? Amount { get; set; } // Amount
		/// <summary></summary>	
		[Description("")]
        public decimal? AdditionalTaxesAmount { get; set; } // AdditionalTaxesAmount
		/// <summary>InvoiceMaker</summary>	
		[Description("InvoiceMaker")]
        public string InvoiceMaker { get; set; } // InvoiceMaker
		/// <summary>登记人</summary>	
		[Description("登记人")]
        public string InvoiceMakerID { get; set; } // InvoiceMakerID
		/// <summary>开票日期</summary>	
		[Description("开票日期")]
        public DateTime? InvoiceDate { get; set; } // InvoiceDate
		/// <summary>备注</summary>	
		[Description("备注")]
        public string Remark { get; set; } // Remark
		/// <summary></summary>	
		[Description("")]
        public string Attachment { get; set; } // Attachment
		/// <summary>BelongYear</summary>	
		[Description("BelongYear")]
        public int? BelongYear { get; set; } // BelongYear
		/// <summary>BelongQuarter</summary>	
		[Description("BelongQuarter")]
        public int? BelongQuarter { get; set; } // BelongQuarter
		/// <summary>BelongMonth</summary>	
		[Description("BelongMonth")]
        public int? BelongMonth { get; set; } // BelongMonth
		/// <summary>状态</summary>	
		[Description("状态")]
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
        public string OrgID { get; set; } // OrgID
		/// <summary></summary>	
		[Description("")]
        public string OrgName { get; set; } // OrgName
		/// <summary>开户银行</summary>	
		[Description("开户银行")]
        public string BankName { get; set; } // BankName
		/// <summary>银行账号</summary>	
		[Description("银行账号")]
        public string BankAccount { get; set; } // BankAccount
		/// <summary>税号</summary>	
		[Description("税号")]
        public string TaxAccount { get; set; } // TaxAccount
		/// <summary>开票单位名称</summary>	
		[Description("开票单位名称")]
        public string InvoiceName { get; set; } // InvoiceName
		/// <summary>合同名称名称</summary>	
		[Description("合同名称名称")]
        public string ContractInfoIDName { get; set; } // ContractInfoIDName
		/// <summary>付款单位名称</summary>	
		[Description("付款单位名称")]
        public string PayerUnitIDName { get; set; } // PayerUnitIDName
		/// <summary>大写</summary>	
		[Description("大写")]
        public string ChineseAmount { get; set; } // ChineseAmount
		/// <summary>登记人名称</summary>	
		[Description("登记人名称")]
        public string InvoiceMakerIDName { get; set; } // InvoiceMakerIDName
		/// <summary>关联收款项</summary>	
		[Description("关联收款项")]
        public string ReceiptObject { get; set; } // ReceiptObject
		/// <summary>发票明细</summary>	
		[Description("发票明细")]
        public string InvoiceDatail { get; set; } // InvoiceDatail
		/// <summary>税名</summary>	
		[Description("税名")]
        public string TaxName { get; set; } // TaxName
		/// <summary>收入单元</summary>	
		[Description("收入单元")]
        public string IncomeUnit { get; set; } // IncomeUnit
		/// <summary>收入单元名称</summary>	
		[Description("收入单元名称")]
        public string IncomeUnitName { get; set; } // IncomeUnitName
		/// <summary>收入单元编号</summary>	
		[Description("收入单元编号")]
        public string IncomeUnitCode { get; set; } // IncomeUnitCode
		/// <summary>合同甲方</summary>	
		[Description("合同甲方")]
        public string CustomerID { get; set; } // CustomerID
		/// <summary>合同甲方名称</summary>	
		[Description("合同甲方名称")]
        public string CustomerIDName { get; set; } // CustomerIDName

        // Reverse navigation
		[JsonIgnore]
        public virtual ICollection<S_C_Invoice_InvoiceDatail> S_C_Invoice_InvoiceDatail { get { onS_C_Invoice_InvoiceDatailGetting(); return _S_C_Invoice_InvoiceDatail;} }
		private ICollection<S_C_Invoice_InvoiceDatail> _S_C_Invoice_InvoiceDatail;
		partial void onS_C_Invoice_InvoiceDatailGetting();

		[JsonIgnore]
        public virtual ICollection<S_C_Invoice_ReceiptObject> S_C_Invoice_ReceiptObject { get { onS_C_Invoice_ReceiptObjectGetting(); return _S_C_Invoice_ReceiptObject;} }
		private ICollection<S_C_Invoice_ReceiptObject> _S_C_Invoice_ReceiptObject;
		partial void onS_C_Invoice_ReceiptObjectGetting();

		[JsonIgnore]
        public virtual ICollection<S_C_InvoiceReceiptRelation> S_C_InvoiceReceiptRelation { get { onS_C_InvoiceReceiptRelationGetting(); return _S_C_InvoiceReceiptRelation;} }
		private ICollection<S_C_InvoiceReceiptRelation> _S_C_InvoiceReceiptRelation;
		partial void onS_C_InvoiceReceiptRelationGetting();


        // Foreign keys
		[JsonIgnore]
        public virtual S_C_ManageContract S_C_ManageContract { get; set; } //  ContractInfoID - FK_S_C_Invoice_S_C_ManageContract

        public S_C_Invoice()
        {
            _S_C_Invoice_InvoiceDatail = new List<S_C_Invoice_InvoiceDatail>();
            _S_C_Invoice_ReceiptObject = new List<S_C_Invoice_ReceiptObject>();
            _S_C_InvoiceReceiptRelation = new List<S_C_InvoiceReceiptRelation>();
        }
    }

	/// <summary>发票明细</summary>	
	[Description("发票明细")]
    public partial class S_C_Invoice_InvoiceDatail : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string S_C_InvoiceID { get; set; } // S_C_InvoiceID
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string IsReleased { get; set; } // IsReleased
		/// <summary>发票号</summary>	
		[Description("发票号")]
        public string Code { get; set; } // Code
		/// <summary>发票金额（元）</summary>	
		[Description("发票金额（元）")]
        public decimal? InvoiceAmount { get; set; } // InvoiceAmount
		/// <summary>发票状态</summary>	
		[Description("发票状态")]
        public string InvoiceState { get; set; } // InvoiceState

        // Foreign keys
		[JsonIgnore]
        public virtual S_C_Invoice S_C_Invoice { get; set; } //  S_C_InvoiceID - FK_S_C_Invoice_InvoiceDatail_S_C_Invoice
    }

	/// <summary>关联收款项</summary>	
	[Description("关联收款项")]
    public partial class S_C_Invoice_ReceiptObject : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary>发票ID</summary>	
		[Description("发票ID")]
        public string S_C_InvoiceID { get; set; } // S_C_InvoiceID
		/// <summary>ReceiptObjectID</summary>	
		[Description("ReceiptObjectID")]
        public string ReceiptObjectID { get; set; } // ReceiptObjectID
		/// <summary>ContractInfoID</summary>	
		[Description("ContractInfoID")]
        public string ContractInfoID { get; set; } // ContractInfoID
		/// <summary>本次开票金额（元）</summary>	
		[Description("本次开票金额（元）")]
        public decimal? RelationValue { get; set; } // RelationValue
		/// <summary></summary>	
		[Description("")]
        public string ModifyUserID { get; set; } // ModifyUserID
		/// <summary></summary>	
		[Description("")]
        public string ModifyUserName { get; set; } // ModifyUserName
		/// <summary></summary>	
		[Description("")]
        public DateTime? ModifyDate { get; set; } // ModifyDate
		/// <summary>收款项名称</summary>	
		[Description("收款项名称")]
        public string Name { get; set; } // Name
		/// <summary>收款项金额（元）</summary>	
		[Description("收款项金额（元）")]
        public decimal? ReceiptValue { get; set; } // ReceiptValue
		/// <summary>已开票（元）</summary>	
		[Description("已开票（元）")]
        public decimal? SumRelationValue { get; set; } // SumRelationValue
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string IsReleased { get; set; } // IsReleased

        // Foreign keys
		[JsonIgnore]
        public virtual S_C_Invoice S_C_Invoice { get; set; } //  S_C_InvoiceID - FK_S_C_InvoiceReceiptObjectRelation_S_C_Invoice
		[JsonIgnore]
        public virtual S_C_ManageContract_ReceiptObj S_C_ManageContract_ReceiptObj { get; set; } //  ReceiptObjectID - FK_S_C_InvoiceReceiptObjectRelation_S_C_ManageContract_ReceiptObj
    }

	/// <summary></summary>	
	[Description("")]
    public partial class S_C_InvoiceReceiptRelation : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string InvoiceID { get; set; } // InvoiceID
		/// <summary></summary>	
		[Description("")]
        public string ReceiptID { get; set; } // ReceiptID
		/// <summary></summary>	
		[Description("")]
        public string ContractInfoID { get; set; } // ContractInfoID
		/// <summary></summary>	
		[Description("")]
        public decimal RelationValue { get; set; } // RelationValue
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
        public virtual S_C_Invoice S_C_Invoice { get; set; } //  InvoiceID - FK_S_C_InvoiceReciptRelation_S_C_Invoice
		[JsonIgnore]
        public virtual S_C_Receipt S_C_Receipt { get; set; } //  ReceiptID - FK_S_C_InvoiceReciptRelation_S_C_Receipt
    }

	/// <summary>合同管理</summary>	
	[Description("合同管理")]
    public partial class S_C_ManageContract : Formula.BaseModel
    {
		/// <summary>GUID</summary>	
		[Description("GUID")]
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
		/// <summary>合同名称</summary>	
		[Description("合同名称")]
        public string Name { get; set; } // Name
		/// <summary>甲方</summary>	
		[Description("甲方")]
        public string PartyA { get; set; } // PartyA
		/// <summary>甲方名称</summary>	
		[Description("甲方名称")]
        public string PartyAName { get; set; } // PartyAName
		/// <summary>甲方FullID</summary>	
		[Description("甲方FullID")]
        public string CustomerFullID { get; set; } // CustomerFullID
		/// <summary>甲方FullIDName</summary>	
		[Description("甲方FullIDName")]
        public string CustomerFullIDName { get; set; } // CustomerFullIDName
		/// <summary>乙方</summary>	
		[Description("乙方")]
        public string PartyB { get; set; } // PartyB
		/// <summary>乙方名称</summary>	
		[Description("乙方名称")]
        public string PartyBName { get; set; } // PartyBName
		/// <summary>丙方</summary>	
		[Description("丙方")]
        public string PartyC { get; set; } // PartyC
		/// <summary>合同编号</summary>	
		[Description("合同编号")]
        public string SerialNumber { get; set; } // SerialNumber
		/// <summary>合同类型</summary>	
		[Description("合同类型")]
        public string ContractType { get; set; } // ContractType
		/// <summary>合同性质</summary>	
		[Description("合同性质")]
        public string ContractClass { get; set; } // ContractClass
		/// <summary>签约日期</summary>	
		[Description("签约日期")]
        public DateTime? SignDate { get; set; } // SignDate
		/// <summary>签约地点</summary>	
		[Description("签约地点")]
        public string SignAddress { get; set; } // SignAddress
		/// <summary>合同状态</summary>	
		[Description("合同状态")]
        public string ContractState { get; set; } // ContractState
		/// <summary>合同起始日期</summary>	
		[Description("合同起始日期")]
        public DateTime? StartDate { get; set; } // StartDate
		/// <summary>合同截止日期</summary>	
		[Description("合同截止日期")]
        public DateTime? EndDate { get; set; } // EndDate
		/// <summary>合同来源</summary>	
		[Description("合同来源")]
        public string ContractSource { get; set; } // ContractSource
		/// <summary>合同金额</summary>	
		[Description("合同金额")]
        public decimal? ContractValue { get; set; } // ContractValue
		/// <summary>关联主合同</summary>	
		[Description("关联主合同")]
        public string MainContract { get; set; } // MainContract
		/// <summary>关联主合同名称</summary>	
		[Description("关联主合同名称")]
        public string MainContractName { get; set; } // MainContractName
		/// <summary>币种</summary>	
		[Description("币种")]
        public string ContractCurrency { get; set; } // ContractCurrency
		/// <summary>结算人民币金额</summary>	
		[Description("结算人民币金额")]
        public decimal? ContractRMBAmount { get; set; } // ContractRMBAmount
		/// <summary>汇率</summary>	
		[Description("汇率")]
        public decimal? ExchangeRate { get; set; } // ExchangeRate
		/// <summary>合同金额大写</summary>	
		[Description("合同金额大写")]
        public string ContractValueDX { get; set; } // ContractValueDX
		/// <summary>结算人民币金额大写</summary>	
		[Description("结算人民币金额大写")]
        public string ContractRMBValueDX { get; set; } // ContractRMBValueDX
		/// <summary>甲方法人</summary>	
		[Description("甲方法人")]
        public string PartyALegalPerson { get; set; } // PartyALegalPerson
		/// <summary>乙方法人</summary>	
		[Description("乙方法人")]
        public string PartyBLegalPerson { get; set; } // PartyBLegalPerson
		/// <summary>丙方法人</summary>	
		[Description("丙方法人")]
        public string PartyCLegalPerson { get; set; } // PartyCLegalPerson
		/// <summary>甲方委托代理人</summary>	
		[Description("甲方委托代理人")]
        public string PartyAHusbandingAgentName { get; set; } // PartyAHusbandingAgentName
		/// <summary>乙方委托代理人</summary>	
		[Description("乙方委托代理人")]
        public string PartyBHusbandingAgentName { get; set; } // PartyBHusbandingAgentName
		/// <summary>丙方委托代理人</summary>	
		[Description("丙方委托代理人")]
        public string PartyCHusbandingAgentName { get; set; } // PartyCHusbandingAgentName
		/// <summary>商务部门</summary>	
		[Description("商务部门")]
        public string BusinessDept { get; set; } // BusinessDept
		/// <summary>商务部门名称</summary>	
		[Description("商务部门名称")]
        public string BusinessDeptName { get; set; } // BusinessDeptName
		/// <summary>商务经理</summary>	
		[Description("商务经理")]
        public string BusinessManager { get; set; } // BusinessManager
		/// <summary>商务经理名称</summary>	
		[Description("商务经理名称")]
        public string BusinessManagerName { get; set; } // BusinessManagerName
		/// <summary>生产部门</summary>	
		[Description("生产部门")]
        public string ProductionDept { get; set; } // ProductionDept
		/// <summary>生产部门名称</summary>	
		[Description("生产部门名称")]
        public string ProductionDeptName { get; set; } // ProductionDeptName
		/// <summary>生产责任人</summary>	
		[Description("生产责任人")]
        public string ProductionManager { get; set; } // ProductionManager
		/// <summary>生产责任人名称</summary>	
		[Description("生产责任人名称")]
        public string ProductionManagerName { get; set; } // ProductionManagerName
		/// <summary>所属工程</summary>	
		[Description("所属工程")]
        public string EngineeringInfo { get; set; } // EngineeringInfo
		/// <summary>所属工程名称</summary>	
		[Description("所属工程名称")]
        public string EngineeringInfoName { get; set; } // EngineeringInfoName
		/// <summary>合同文本</summary>	
		[Description("合同文本")]
        public string ContractAttachment { get; set; } // ContractAttachment
		/// <summary>相关资料</summary>	
		[Description("相关资料")]
        public string Attachment { get; set; } // Attachment
		/// <summary>备注</summary>	
		[Description("备注")]
        public string Remark { get; set; } // Remark
		/// <summary>收款合计</summary>	
		[Description("收款合计")]
        public decimal? SumReceiptValue { get; set; } // SumReceiptValue
		/// <summary>开票合计</summary>	
		[Description("开票合计")]
        public decimal? SumInvoiceValue { get; set; } // SumInvoiceValue
		/// <summary>坏账合计</summary>	
		[Description("坏账合计")]
        public decimal? SumBadDebtValue { get; set; } // SumBadDebtValue
		/// <summary>是否签约</summary>	
		[Description("是否签约")]
        public string IsSigned { get; set; } // IsSigned
		/// <summary>归属年份</summary>	
		[Description("归属年份")]
        public int? BelongYear { get; set; } // BelongYear
		/// <summary>归属月份</summary>	
		[Description("归属月份")]
        public int? BelongMonth { get; set; } // BelongMonth
		/// <summary>归属季度</summary>	
		[Description("归属季度")]
        public int? BelongQuarter { get; set; } // BelongQuarter
		/// <summary>合同文本状态</summary>	
		[Description("合同文本状态")]
        public string ContractTextState { get; set; } // ContractTextState
		/// <summary>合同文本寄出人</summary>	
		[Description("合同文本寄出人")]
        public string ContractTextSendUser { get; set; } // ContractTextSendUser
		/// <summary>合同文本寄出人名称</summary>	
		[Description("合同文本寄出人名称")]
        public string ContractTextSendUserName { get; set; } // ContractTextSendUserName
		/// <summary>合同分解</summary>	
		[Description("合同分解")]
        public string ContractSplit { get; set; } // ContractSplit
		/// <summary>本合同人民币金额</summary>	
		[Description("本合同人民币金额")]
        public decimal? ThisContractRMBAmount { get; set; } // ThisContractRMBAmount
		/// <summary>人民币金额大写</summary>	
		[Description("人民币金额大写")]
        public string ThisContractRMBAmountDX { get; set; } // ThisContractRMBAmountDX
		/// <summary>税率</summary>	
		[Description("税率")]
        public decimal? TaxRate { get; set; } // TaxRate
		/// <summary>税名</summary>	
		[Description("税名")]
        public string TaxName { get; set; } // TaxName
		/// <summary>不含税金额</summary>	
		[Description("不含税金额")]
        public decimal? ClearContractValue { get; set; } // ClearContractValue
		/// <summary>税金</summary>	
		[Description("税金")]
        public decimal? TaxValue { get; set; } // TaxValue
		/// <summary>PayerUnit</summary>	
		[Description("PayerUnit")]
        public string PayerUnit { get; set; } // PayerUnit
		/// <summary>PayerUnit名称</summary>	
		[Description("PayerUnit名称")]
        public string PayerUnitName { get; set; } // PayerUnitName

        // Reverse navigation
		[JsonIgnore]
        public virtual ICollection<S_C_Invoice> S_C_Invoice { get { onS_C_InvoiceGetting(); return _S_C_Invoice;} }
		private ICollection<S_C_Invoice> _S_C_Invoice;
		partial void onS_C_InvoiceGetting();

		[JsonIgnore]
        public virtual ICollection<S_C_ManageContract_ContractSplit> S_C_ManageContract_ContractSplit { get { onS_C_ManageContract_ContractSplitGetting(); return _S_C_ManageContract_ContractSplit;} }
		private ICollection<S_C_ManageContract_ContractSplit> _S_C_ManageContract_ContractSplit;
		partial void onS_C_ManageContract_ContractSplitGetting();

		[JsonIgnore]
        public virtual ICollection<S_C_ManageContract_DeptRelation> S_C_ManageContract_DeptRelation { get { onS_C_ManageContract_DeptRelationGetting(); return _S_C_ManageContract_DeptRelation;} }
		private ICollection<S_C_ManageContract_DeptRelation> _S_C_ManageContract_DeptRelation;
		partial void onS_C_ManageContract_DeptRelationGetting();

		[JsonIgnore]
        public virtual ICollection<S_C_ManageContract_PaymentDetail> S_C_ManageContract_PaymentDetail { get { onS_C_ManageContract_PaymentDetailGetting(); return _S_C_ManageContract_PaymentDetail;} }
		private ICollection<S_C_ManageContract_PaymentDetail> _S_C_ManageContract_PaymentDetail;
		partial void onS_C_ManageContract_PaymentDetailGetting();

		[JsonIgnore]
        public virtual ICollection<S_C_ManageContract_ProjectRelation> S_C_ManageContract_ProjectRelation { get { onS_C_ManageContract_ProjectRelationGetting(); return _S_C_ManageContract_ProjectRelation;} }
		private ICollection<S_C_ManageContract_ProjectRelation> _S_C_ManageContract_ProjectRelation;
		partial void onS_C_ManageContract_ProjectRelationGetting();

		[JsonIgnore]
        public virtual ICollection<S_C_ManageContract_ReceiptObj> S_C_ManageContract_ReceiptObj { get { onS_C_ManageContract_ReceiptObjGetting(); return _S_C_ManageContract_ReceiptObj;} }
		private ICollection<S_C_ManageContract_ReceiptObj> _S_C_ManageContract_ReceiptObj;
		partial void onS_C_ManageContract_ReceiptObjGetting();

		[JsonIgnore]
        public virtual ICollection<S_C_ManageContract_Supplementary> S_C_ManageContract_Supplementary { get { onS_C_ManageContract_SupplementaryGetting(); return _S_C_ManageContract_Supplementary;} }
		private ICollection<S_C_ManageContract_Supplementary> _S_C_ManageContract_Supplementary;
		partial void onS_C_ManageContract_SupplementaryGetting();

		[JsonIgnore]
        public virtual ICollection<S_C_PlanReceipt> S_C_PlanReceipt { get { onS_C_PlanReceiptGetting(); return _S_C_PlanReceipt;} }
		private ICollection<S_C_PlanReceipt> _S_C_PlanReceipt;
		partial void onS_C_PlanReceiptGetting();

		[JsonIgnore]
        public virtual ICollection<S_C_Receipt> S_C_Receipt { get { onS_C_ReceiptGetting(); return _S_C_Receipt;} }
		private ICollection<S_C_Receipt> _S_C_Receipt;
		partial void onS_C_ReceiptGetting();


        // Foreign keys
		[JsonIgnore]
        public virtual S_F_Customer S_F_Customer { get; set; } //  PartyA - FK_S_C_ManageContract_S_F_Customer

        public S_C_ManageContract()
        {
            _S_C_Invoice = new List<S_C_Invoice>();
            _S_C_ManageContract_ContractSplit = new List<S_C_ManageContract_ContractSplit>();
            _S_C_ManageContract_DeptRelation = new List<S_C_ManageContract_DeptRelation>();
            _S_C_ManageContract_PaymentDetail = new List<S_C_ManageContract_PaymentDetail>();
            _S_C_ManageContract_ProjectRelation = new List<S_C_ManageContract_ProjectRelation>();
            _S_C_ManageContract_ReceiptObj = new List<S_C_ManageContract_ReceiptObj>();
            _S_C_ManageContract_Supplementary = new List<S_C_ManageContract_Supplementary>();
            _S_C_PlanReceipt = new List<S_C_PlanReceipt>();
            _S_C_Receipt = new List<S_C_Receipt>();
        }
    }

	/// <summary>合同分解</summary>	
	[Description("合同分解")]
    public partial class S_C_ManageContract_ContractSplit : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string S_C_ManageContractID { get; set; } // S_C_ManageContractID
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string IsReleased { get; set; } // IsReleased
		/// <summary>分类</summary>	
		[Description("分类")]
        public string Name { get; set; } // Name
		/// <summary>占比（%）</summary>	
		[Description("占比（%）")]
        public decimal? Scale { get; set; } // Scale
		/// <summary>分解合同额（元）</summary>	
		[Description("分解合同额（元）")]
        public decimal? SplitValue { get; set; } // SplitValue
		/// <summary>分解类型</summary>	
		[Description("分解类型")]
        public string SplitType { get; set; } // SplitType
		/// <summary>业务类型</summary>	
		[Description("业务类型")]
        public string BusinessType { get; set; } // BusinessType

        // Foreign keys
		[JsonIgnore]
        public virtual S_C_ManageContract S_C_ManageContract { get; set; } //  S_C_ManageContractID - FK_S_C_ManageContract_ContractSplit_S_C_ManageContract
    }

	/// <summary>部门分解</summary>	
	[Description("部门分解")]
    public partial class S_C_ManageContract_DeptRelation : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string S_C_ManageContractID { get; set; } // S_C_ManageContractID
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string IsReleased { get; set; } // IsReleased
		/// <summary>部门</summary>	
		[Description("部门")]
        public string Dept { get; set; } // Dept
		/// <summary>部门名称</summary>	
		[Description("部门名称")]
        public string DeptName { get; set; } // DeptName
		/// <summary>占比（%）</summary>	
		[Description("占比（%）")]
        public decimal? Scale { get; set; } // Scale
		/// <summary>部门合同额（元）</summary>	
		[Description("部门合同额（元）")]
        public decimal? DeptValue { get; set; } // DeptValue
		/// <summary>补充协议合同额（元）</summary>	
		[Description("补充协议合同额（元）")]
        public decimal? SumSupplementaryValue { get; set; } // SumSupplementaryValue
		/// <summary>累计收款（元）</summary>	
		[Description("累计收款（元）")]
        public decimal? SumDeptReceiptValue { get; set; } // SumDeptReceiptValue

        // Foreign keys
		[JsonIgnore]
        public virtual S_C_ManageContract S_C_ManageContract { get; set; } //  S_C_ManageContractID - FK_S_C_ManageContract_DeptRelation_S_C_ManageContract
    }

	/// <summary>付款单位列表</summary>	
	[Description("付款单位列表")]
    public partial class S_C_ManageContract_PaymentDetail : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string S_C_ManageContractID { get; set; } // S_C_ManageContractID
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string IsReleased { get; set; } // IsReleased
		/// <summary>付款方</summary>	
		[Description("付款方")]
        public string CustomerID { get; set; } // CustomerID
		/// <summary>付款方名称</summary>	
		[Description("付款方名称")]
        public string CustomerIDName { get; set; } // CustomerIDName
		/// <summary>付款方联系人</summary>	
		[Description("付款方联系人")]
        public string PaymentLinkMan { get; set; } // PaymentLinkMan
		/// <summary>付款方联系人电话</summary>	
		[Description("付款方联系人电话")]
        public string PaymentLinkTel { get; set; } // PaymentLinkTel
		/// <summary>证明材料</summary>	
		[Description("证明材料")]
        public string EvidenceAttachment { get; set; } // EvidenceAttachment
		/// <summary>证明材料名称</summary>	
		[Description("证明材料名称")]
        public string EvidenceAttachmentName { get; set; } // EvidenceAttachmentName

        // Foreign keys
		[JsonIgnore]
        public virtual S_C_ManageContract S_C_ManageContract { get; set; } //  S_C_ManageContractID - FK_S_C_ManageContract_PaymentDetail_S_C_ManageContract
    }

	/// <summary>关联项目</summary>	
	[Description("关联项目")]
    public partial class S_C_ManageContract_ProjectRelation : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string S_C_ManageContractID { get; set; } // S_C_ManageContractID
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string IsReleased { get; set; } // IsReleased
		/// <summary>项目编号</summary>	
		[Description("项目编号")]
        public string ProjectCode { get; set; } // ProjectCode
		/// <summary>项目名称</summary>	
		[Description("项目名称")]
        public string ProjectName { get; set; } // ProjectName
		/// <summary>项目ID</summary>	
		[Description("项目ID")]
        public string ProjectID { get; set; } // ProjectID
		/// <summary>占比%</summary>	
		[Description("占比%")]
        public decimal? Scale { get; set; } // Scale
		/// <summary>合同金额（元）</summary>	
		[Description("合同金额（元）")]
        public decimal? ProjectValue { get; set; } // ProjectValue
		/// <summary>税金（元）</summary>	
		[Description("税金（元）")]
        public decimal? TaxValue { get; set; } // TaxValue
		/// <summary>不含税金额（元）</summary>	
		[Description("不含税金额（元）")]
        public decimal? ClearValue { get; set; } // ClearValue
		/// <summary>税率</summary>	
		[Description("税率")]
        public decimal? TaxRate { get; set; } // TaxRate
		/// <summary>项目部门</summary>	
		[Description("项目部门")]
        public string ChargerDept { get; set; } // ChargerDept
		/// <summary>项目部门名称</summary>	
		[Description("项目部门名称")]
        public string ChargerDeptName { get; set; } // ChargerDeptName

        // Foreign keys
		[JsonIgnore]
        public virtual S_C_ManageContract S_C_ManageContract { get; set; } //  S_C_ManageContractID - FK_S_C_ManageContract_ProjectRelation_S_C_ManageContract
    }

	/// <summary>合同收款项</summary>	
	[Description("合同收款项")]
    public partial class S_C_ManageContract_ReceiptObj : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string S_C_ManageContractID { get; set; } // S_C_ManageContractID
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string IsReleased { get; set; } // IsReleased
		/// <summary>收款项名称</summary>	
		[Description("收款项名称")]
        public string Name { get; set; } // Name
		/// <summary>占比（%）</summary>	
		[Description("占比（%）")]
        public decimal? ReceiptPercent { get; set; } // ReceiptPercent
		/// <summary>金额（元）</summary>	
		[Description("金额（元）")]
        public decimal? ReceiptValue { get; set; } // ReceiptValue
		/// <summary>已开票（元）</summary>	
		[Description("已开票（元）")]
        public decimal? FactInvoiceValue { get; set; } // FactInvoiceValue
		/// <summary>已收款（元）</summary>	
		[Description("已收款（元）")]
        public decimal? FactReceiptValue { get; set; } // FactReceiptValue
		/// <summary>原计划日期</summary>	
		[Description("原计划日期")]
        public DateTime? OriginalPlanFinishDate { get; set; } // OriginalPlanFinishDate
		/// <summary>计划日期</summary>	
		[Description("计划日期")]
        public DateTime? PlanFinishDate { get; set; } // PlanFinishDate
		/// <summary>关联项目</summary>	
		[Description("关联项目")]
        public string ProjectInfo { get; set; } // ProjectInfo
		/// <summary>关联项目名称</summary>	
		[Description("关联项目名称")]
        public string ProjectInfoName { get; set; } // ProjectInfoName
		/// <summary>关联里程碑.名称</summary>	
		[Description("关联里程碑.名称")]
        public string MileStoneName { get; set; } // MileStoneName
		/// <summary></summary>	
		[Description("")]
        public string MileStoneID { get; set; } // MileStoneID
		/// <summary>坏账（元）</summary>	
		[Description("坏账（元）")]
        public decimal? FactBadValue { get; set; } // FactBadValue
		/// <summary>MileStoneState</summary>	
		[Description("MileStoneState")]
        public string MileStoneState { get; set; } // MileStoneState
		/// <summary>关联里程碑.计划完成时间</summary>	
		[Description("关联里程碑.计划完成时间")]
        public DateTime? MileStonePlanEndDate { get; set; } // MileStonePlanEndDate
		/// <summary>关联里程碑.实际完成时间</summary>	
		[Description("关联里程碑.实际完成时间")]
        public DateTime? MileStoneFactEndDate { get; set; } // MileStoneFactEndDate
		/// <summary>补充协议</summary>	
		[Description("补充协议")]
        public string SupplementaryID { get; set; } // SupplementaryID

        // Reverse navigation
		[JsonIgnore]
        public virtual ICollection<S_C_Invoice_ReceiptObject> S_C_Invoice_ReceiptObject { get { onS_C_Invoice_ReceiptObjectGetting(); return _S_C_Invoice_ReceiptObject;} }
		private ICollection<S_C_Invoice_ReceiptObject> _S_C_Invoice_ReceiptObject;
		partial void onS_C_Invoice_ReceiptObjectGetting();

		[JsonIgnore]
        public virtual ICollection<S_C_PlanReceipt> S_C_PlanReceipt { get { onS_C_PlanReceiptGetting(); return _S_C_PlanReceipt;} }
		private ICollection<S_C_PlanReceipt> _S_C_PlanReceipt;
		partial void onS_C_PlanReceiptGetting();


        // Foreign keys
		[JsonIgnore]
        public virtual S_C_ManageContract S_C_ManageContract { get; set; } //  S_C_ManageContractID - FK_S_C_ManageContract_ReceiptObj_S_C_ManageContract

        public S_C_ManageContract_ReceiptObj()
        {
            _S_C_Invoice_ReceiptObject = new List<S_C_Invoice_ReceiptObject>();
            _S_C_PlanReceipt = new List<S_C_PlanReceipt>();
        }
    }

	/// <summary>合同补充协议</summary>	
	[Description("合同补充协议")]
    public partial class S_C_ManageContract_Supplementary : Formula.BaseModel
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
		/// <summary>合同ID</summary>	
		[Description("合同ID")]
        public string ContractInfoID { get; set; } // ContractInfoID
		/// <summary>协议名称</summary>	
		[Description("协议名称")]
        public string Name { get; set; } // Name
		/// <summary>协议编号</summary>	
		[Description("协议编号")]
        public string SerialNumber { get; set; } // SerialNumber
		/// <summary>责任人</summary>	
		[Description("责任人")]
        public string ChargeUser { get; set; } // ChargeUser
		/// <summary>责任人名称</summary>	
		[Description("责任人名称")]
        public string ChargeUserName { get; set; } // ChargeUserName
		/// <summary>签订日期</summary>	
		[Description("签订日期")]
        public DateTime? SignDate { get; set; } // SignDate
		/// <summary>协议金额</summary>	
		[Description("协议金额")]
        public decimal? SupplementaryValue { get; set; } // SupplementaryValue
		/// <summary>协议币种</summary>	
		[Description("协议币种")]
        public string SupplementaryCurrency { get; set; } // SupplementaryCurrency
		/// <summary>协议人民币金额</summary>	
		[Description("协议人民币金额")]
        public decimal? SupplementaryRMBAmount { get; set; } // SupplementaryRMBAmount
		/// <summary>协议汇率</summary>	
		[Description("协议汇率")]
        public decimal? SupplementaryExchangeRate { get; set; } // SupplementaryExchangeRate
		/// <summary>协议金额大写</summary>	
		[Description("协议金额大写")]
        public string SupplementaryValueDX { get; set; } // SupplementaryValueDX
		/// <summary>协议人民币金额大写</summary>	
		[Description("协议人民币金额大写")]
        public string SupplementaryRMBAmountDX { get; set; } // SupplementaryRMBAmountDX
		/// <summary>相关资料</summary>	
		[Description("相关资料")]
        public string Attachment { get; set; } // Attachment
		/// <summary>备注</summary>	
		[Description("备注")]
        public string Remark { get; set; } // Remark
		/// <summary>BelongYear</summary>	
		[Description("BelongYear")]
        public int? BelongYear { get; set; } // BelongYear
		/// <summary>BelongMonth</summary>	
		[Description("BelongMonth")]
        public int? BelongMonth { get; set; } // BelongMonth
		/// <summary>BelongQuarter</summary>	
		[Description("BelongQuarter")]
        public int? BelongQuarter { get; set; } // BelongQuarter
		/// <summary>ReceiptObjID</summary>	
		[Description("ReceiptObjID")]
        public string ReceiptObjID { get; set; } // ReceiptObjID

        // Reverse navigation
		[JsonIgnore]
        public virtual ICollection<S_C_ManageContract_Supplementary_DeptRelation> S_C_ManageContract_Supplementary_DeptRelation { get { onS_C_ManageContract_Supplementary_DeptRelationGetting(); return _S_C_ManageContract_Supplementary_DeptRelation;} }
		private ICollection<S_C_ManageContract_Supplementary_DeptRelation> _S_C_ManageContract_Supplementary_DeptRelation;
		partial void onS_C_ManageContract_Supplementary_DeptRelationGetting();

		[JsonIgnore]
        public virtual ICollection<S_C_ManageContract_Supplementary_ReceiptObj> S_C_ManageContract_Supplementary_ReceiptObj { get { onS_C_ManageContract_Supplementary_ReceiptObjGetting(); return _S_C_ManageContract_Supplementary_ReceiptObj;} }
		private ICollection<S_C_ManageContract_Supplementary_ReceiptObj> _S_C_ManageContract_Supplementary_ReceiptObj;
		partial void onS_C_ManageContract_Supplementary_ReceiptObjGetting();


        // Foreign keys
		[JsonIgnore]
        public virtual S_C_ManageContract S_C_ManageContract { get; set; } //  ContractInfoID - FK_ContractInfoID

        public S_C_ManageContract_Supplementary()
        {
            _S_C_ManageContract_Supplementary_DeptRelation = new List<S_C_ManageContract_Supplementary_DeptRelation>();
            _S_C_ManageContract_Supplementary_ReceiptObj = new List<S_C_ManageContract_Supplementary_ReceiptObj>();
        }
    }

	/// <summary>部门分解</summary>	
	[Description("部门分解")]
    public partial class S_C_ManageContract_Supplementary_DeptRelation : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string S_C_ManageContract_SupplementaryID { get; set; } // S_C_ManageContract_SupplementaryID
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string IsReleased { get; set; } // IsReleased
		/// <summary>部门</summary>	
		[Description("部门")]
        public string Dept { get; set; } // Dept
		/// <summary>部门名称</summary>	
		[Description("部门名称")]
        public string DeptName { get; set; } // DeptName
		/// <summary>占比（%）</summary>	
		[Description("占比（%）")]
        public decimal? Scale { get; set; } // Scale
		/// <summary>本协议金额（元）</summary>	
		[Description("本协议金额（元）")]
        public decimal? DeptValue { get; set; } // DeptValue

        // Foreign keys
		[JsonIgnore]
        public virtual S_C_ManageContract_Supplementary S_C_ManageContract_Supplementary { get; set; } //  S_C_ManageContract_SupplementaryID - FK_S_C_ManageContract_Supplementary_DeptRelation_S_C_ManageContract_Supplementary
    }

	/// <summary>收款项</summary>	
	[Description("收款项")]
    public partial class S_C_ManageContract_Supplementary_ReceiptObj : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string S_C_ManageContract_SupplementaryID { get; set; } // S_C_ManageContract_SupplementaryID
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string IsReleased { get; set; } // IsReleased
		/// <summary>收款项名称</summary>	
		[Description("收款项名称")]
        public string Name { get; set; } // Name
		/// <summary>占比（%）</summary>	
		[Description("占比（%）")]
        public decimal? ReceiptPercent { get; set; } // ReceiptPercent
		/// <summary>金额（元）</summary>	
		[Description("金额（元）")]
        public decimal? ReceiptValue { get; set; } // ReceiptValue
		/// <summary>已开票（元）</summary>	
		[Description("已开票（元）")]
        public decimal? FactInvoiceValue { get; set; } // FactInvoiceValue
		/// <summary>已收款（元）</summary>	
		[Description("已收款（元）")]
        public decimal? FactReceiptValue { get; set; } // FactReceiptValue
		/// <summary>原计划日期</summary>	
		[Description("原计划日期")]
        public DateTime? OriginalPlanFinishDate { get; set; } // OriginalPlanFinishDate
		/// <summary>计划日期</summary>	
		[Description("计划日期")]
        public DateTime? PlanFinishDate { get; set; } // PlanFinishDate
		/// <summary>关联项目</summary>	
		[Description("关联项目")]
        public string ProjectInfo { get; set; } // ProjectInfo
		/// <summary>ProjectInfoName</summary>	
		[Description("ProjectInfoName")]
        public string ProjectInfoName { get; set; } // ProjectInfoName
		/// <summary>关联里程碑.名称</summary>	
		[Description("关联里程碑.名称")]
        public string MileStoneName { get; set; } // MileStoneName
		/// <summary>关联里程碑.计划完成时间</summary>	
		[Description("关联里程碑.计划完成时间")]
        public DateTime? MileStonePlanEndDate { get; set; } // MileStonePlanEndDate
		/// <summary>关联里程碑.实际完成时间</summary>	
		[Description("关联里程碑.实际完成时间")]
        public DateTime? MileStoneFactEndDate { get; set; } // MileStoneFactEndDate
		/// <summary></summary>	
		[Description("")]
        public string MileStoneID { get; set; } // MileStoneID
		/// <summary>坏账（元）</summary>	
		[Description("坏账（元）")]
        public decimal? FactBadValue { get; set; } // FactBadValue
		/// <summary>MileStoneState</summary>	
		[Description("MileStoneState")]
        public string MileStoneState { get; set; } // MileStoneState

        // Foreign keys
		[JsonIgnore]
        public virtual S_C_ManageContract_Supplementary S_C_ManageContract_Supplementary { get; set; } //  S_C_ManageContract_SupplementaryID - FK_S_C_ManageContract_Supplementary_ReceiptObj_S_C_ManageContract_Supplementary
    }

	/// <summary></summary>	
	[Description("")]
    public partial class S_C_PlanReceipt : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string ContractInfoID { get; set; } // ContractInfoID
		/// <summary></summary>	
		[Description("")]
        public string ReceiptObjectID { get; set; } // ReceiptObjectID
		/// <summary></summary>	
		[Description("")]
        public string RelateParentID { get; set; } // RelateParentID
		/// <summary></summary>	
		[Description("")]
        public string Name { get; set; } // Name
		/// <summary></summary>	
		[Description("")]
        public string CustomerID { get; set; } // CustomerID
		/// <summary></summary>	
		[Description("")]
        public string CustomerFullID { get; set; } // CustomerFullID
		/// <summary></summary>	
		[Description("")]
        public string CustomerCode { get; set; } // CustomerCode
		/// <summary></summary>	
		[Description("")]
        public string CustomerName { get; set; } // CustomerName
		/// <summary></summary>	
		[Description("")]
        public decimal PlanReceiptValue { get; set; } // PlanReceiptValue
		/// <summary></summary>	
		[Description("")]
        public decimal? FactReceiptValue { get; set; } // FactReceiptValue
		/// <summary></summary>	
		[Description("")]
        public DateTime? PlanReceiptDate { get; set; } // PlanReceiptDate
		/// <summary></summary>	
		[Description("")]
        public DateTime? ReceiptDate { get; set; } // ReceiptDate
		/// <summary></summary>	
		[Description("")]
        public string ProductionUnitID { get; set; } // ProductionUnitID
		/// <summary></summary>	
		[Description("")]
        public string ProductionUnitName { get; set; } // ProductionUnitName
		/// <summary></summary>	
		[Description("")]
        public string ProduceMasterID { get; set; } // ProduceMasterID
		/// <summary></summary>	
		[Description("")]
        public string ProduceMasterName { get; set; } // ProduceMasterName
		/// <summary></summary>	
		[Description("")]
        public string FirstDutyManName { get; set; } // FirstDutyManName
		/// <summary></summary>	
		[Description("")]
        public string FirstDutyManID { get; set; } // FirstDutyManID
		/// <summary></summary>	
		[Description("")]
        public string PlanReceiptYearMonth { get; set; } // PlanReceiptYearMonth
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
        public string ContractName { get; set; } // ContractName
		/// <summary></summary>	
		[Description("")]
        public string ContractCode { get; set; } // ContractCode
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
        public string Remark { get; set; } // Remark
		/// <summary></summary>	
		[Description("")]
        public string State { get; set; } // State
		/// <summary></summary>	
		[Description("")]
        public string IsBadDebt { get; set; } // IsBadDebt
		/// <summary></summary>	
		[Description("")]
        public decimal? BadDebtValue { get; set; } // BadDebtValue
		/// <summary></summary>	
		[Description("")]
        public string Importance { get; set; } // Importance
		/// <summary></summary>	
		[Description("")]
        public string RiskLevel { get; set; } // RiskLevel
		/// <summary></summary>	
		[Description("")]
        public string DutyType { get; set; } // DutyType
		/// <summary></summary>	
		[Description("")]
        public string MasterUnitID { get; set; } // MasterUnitID
		/// <summary></summary>	
		[Description("")]
        public string MasterUnit { get; set; } // MasterUnit
		/// <summary></summary>	
		[Description("")]
        public string MasterID { get; set; } // MasterID
		/// <summary></summary>	
		[Description("")]
        public string MasterName { get; set; } // MasterName
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
        public virtual ICollection<S_C_ReceiptPlanRelation> S_C_ReceiptPlanRelation { get { onS_C_ReceiptPlanRelationGetting(); return _S_C_ReceiptPlanRelation;} }
		private ICollection<S_C_ReceiptPlanRelation> _S_C_ReceiptPlanRelation;
		partial void onS_C_ReceiptPlanRelationGetting();


        // Foreign keys
		[JsonIgnore]
        public virtual S_C_ManageContract S_C_ManageContract { get; set; } //  ContractInfoID - FK_S_C_PlanReceipt_S_C_ManageContract
		[JsonIgnore]
        public virtual S_C_ManageContract_ReceiptObj S_C_ManageContract_ReceiptObj { get; set; } //  ReceiptObjectID - FK_S_C_PlanReceipt_S_C_ManageContract_ReceiptObj

        public S_C_PlanReceipt()
        {
            _S_C_ReceiptPlanRelation = new List<S_C_ReceiptPlanRelation>();
        }
    }

	/// <summary></summary>	
	[Description("")]
    public partial class S_C_Receipt : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string ContractInfoID { get; set; } // ContractInfoID
		/// <summary></summary>	
		[Description("")]
        public string ContractName { get; set; } // ContractName
		/// <summary></summary>	
		[Description("")]
        public string ContractCode { get; set; } // ContractCode
		/// <summary></summary>	
		[Description("")]
        public string CustomerID { get; set; } // CustomerID
		/// <summary></summary>	
		[Description("")]
        public string CustomerName { get; set; } // CustomerName
		/// <summary></summary>	
		[Description("")]
        public string CustomerFullID { get; set; } // CustomerFullID
		/// <summary></summary>	
		[Description("")]
        public string PayerUnit { get; set; } // PayerUnit
		/// <summary></summary>	
		[Description("")]
        public string PayerUnitName { get; set; } // PayerUnitName
		/// <summary></summary>	
		[Description("")]
        public string RegisterID { get; set; } // RegisterID
		/// <summary></summary>	
		[Description("")]
        public string Name { get; set; } // Name
		/// <summary></summary>	
		[Description("")]
        public string Code { get; set; } // Code
		/// <summary></summary>	
		[Description("")]
        public decimal Amount { get; set; } // Amount
		/// <summary></summary>	
		[Description("")]
        public DateTime ArrivedDate { get; set; } // ArrivedDate
		/// <summary></summary>	
		[Description("")]
        public string ReceiptType { get; set; } // ReceiptType
		/// <summary></summary>	
		[Description("")]
        public string ReceiptMasterUnit { get; set; } // ReceiptMasterUnit
		/// <summary></summary>	
		[Description("")]
        public string ReceiptMasterUnitID { get; set; } // ReceiptMasterUnitID
		/// <summary></summary>	
		[Description("")]
        public string ProductUnit { get; set; } // ProductUnit
		/// <summary></summary>	
		[Description("")]
        public string ProductUnitName { get; set; } // ProductUnitName
		/// <summary></summary>	
		[Description("")]
        public string Operator { get; set; } // Operator
		/// <summary></summary>	
		[Description("")]
        public string OperatorID { get; set; } // OperatorID
		/// <summary></summary>	
		[Description("")]
        public string Remark { get; set; } // Remark
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
        public string OrgID { get; set; } // OrgID
		/// <summary></summary>	
		[Description("")]
        public string OrgName { get; set; } // OrgName
		/// <summary></summary>	
		[Description("")]
        public string State { get; set; } // State

        // Reverse navigation
		[JsonIgnore]
        public virtual ICollection<S_C_InvoiceReceiptRelation> S_C_InvoiceReceiptRelation { get { onS_C_InvoiceReceiptRelationGetting(); return _S_C_InvoiceReceiptRelation;} }
		private ICollection<S_C_InvoiceReceiptRelation> _S_C_InvoiceReceiptRelation;
		partial void onS_C_InvoiceReceiptRelationGetting();

		[JsonIgnore]
        public virtual ICollection<S_C_Receipt_DeptRelation> S_C_Receipt_DeptRelation { get { onS_C_Receipt_DeptRelationGetting(); return _S_C_Receipt_DeptRelation;} }
		private ICollection<S_C_Receipt_DeptRelation> _S_C_Receipt_DeptRelation;
		partial void onS_C_Receipt_DeptRelationGetting();

		[JsonIgnore]
        public virtual ICollection<S_C_Receipt_ProjectRelation> S_C_Receipt_ProjectRelation { get { onS_C_Receipt_ProjectRelationGetting(); return _S_C_Receipt_ProjectRelation;} }
		private ICollection<S_C_Receipt_ProjectRelation> _S_C_Receipt_ProjectRelation;
		partial void onS_C_Receipt_ProjectRelationGetting();

		[JsonIgnore]
        public virtual ICollection<S_C_ReceiptPlanRelation> S_C_ReceiptPlanRelation { get { onS_C_ReceiptPlanRelationGetting(); return _S_C_ReceiptPlanRelation;} }
		private ICollection<S_C_ReceiptPlanRelation> _S_C_ReceiptPlanRelation;
		partial void onS_C_ReceiptPlanRelationGetting();


        // Foreign keys
		[JsonIgnore]
        public virtual S_C_ManageContract S_C_ManageContract { get; set; } //  ContractInfoID - FK_S_C_Receipt_S_C_ManageContract

        public S_C_Receipt()
        {
            _S_C_InvoiceReceiptRelation = new List<S_C_InvoiceReceiptRelation>();
            _S_C_Receipt_DeptRelation = new List<S_C_Receipt_DeptRelation>();
            _S_C_Receipt_ProjectRelation = new List<S_C_Receipt_ProjectRelation>();
            _S_C_ReceiptPlanRelation = new List<S_C_ReceiptPlanRelation>();
        }
    }

	/// <summary></summary>	
	[Description("")]
    public partial class S_C_Receipt_DeptRelation : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string S_C_Receipt_ID { get; set; } // S_C_Receipt_ID
		/// <summary></summary>	
		[Description("")]
        public string ContractInfoID { get; set; } // ContractInfoID
		/// <summary></summary>	
		[Description("")]
        public string Dept { get; set; } // Dept
		/// <summary></summary>	
		[Description("")]
        public string DeptName { get; set; } // DeptName
		/// <summary></summary>	
		[Description("")]
        public decimal? RelationValue { get; set; } // RelationValue
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
        public virtual S_C_Receipt S_C_Receipt { get; set; } //  S_C_Receipt_ID - FK_S_C_Receipt_DeptRelation_S_C_Receipt
    }

	/// <summary></summary>	
	[Description("")]
    public partial class S_C_Receipt_ProjectRelation : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string S_C_Receipt_ID { get; set; } // S_C_Receipt_ID
		/// <summary></summary>	
		[Description("")]
        public string ContractInfoID { get; set; } // ContractInfoID
		/// <summary></summary>	
		[Description("")]
        public string ProjectID { get; set; } // ProjectID
		/// <summary></summary>	
		[Description("")]
        public string ProjectName { get; set; } // ProjectName
		/// <summary></summary>	
		[Description("")]
        public decimal? RelationValue { get; set; } // RelationValue
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
        public virtual S_C_Receipt S_C_Receipt { get; set; } //  S_C_Receipt_ID - FK_S_C_Receipt_ProjectRelation_S_C_Receipt
    }

	/// <summary></summary>	
	[Description("")]
    public partial class S_C_ReceiptPlanRelation : Formula.BaseModel
    {
		/// <summary>主键ID</summary>	
		[Description("主键ID")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary>到款ID</summary>	
		[Description("到款ID")]
        public string ReceiptID { get; set; } // ReceiptID
		/// <summary>计划ID</summary>	
		[Description("计划ID")]
        public string PlanID { get; set; } // PlanID
		/// <summary></summary>	
		[Description("")]
        public string ReceiptObjectID { get; set; } // ReceiptObjectID
		/// <summary></summary>	
		[Description("")]
        public string ReceiptObjectCode { get; set; } // ReceiptObjectCode
		/// <summary>关联金额</summary>	
		[Description("关联金额")]
        public decimal RelationValue { get; set; } // RelationValue
		/// <summary></summary>	
		[Description("")]
        public string RelateParentID { get; set; } // RelateParentID
		/// <summary></summary>	
		[Description("")]
        public string ModifyUser { get; set; } // ModifyUser
		/// <summary></summary>	
		[Description("")]
        public string ModifyUserID { get; set; } // ModifyUserID
		/// <summary></summary>	
		[Description("")]
        public DateTime? ModifyDate { get; set; } // ModifyDate

        // Foreign keys
		[JsonIgnore]
        public virtual S_C_Receipt S_C_Receipt { get; set; } //  ReceiptID - FK_S_C_ReceiptPlanRelation_S_C_Receipt
		[JsonIgnore]
        public virtual S_C_PlanReceipt S_C_PlanReceipt { get; set; } //  PlanID - FK_S_C_ReceiptPlanRelation_S_C_PlanReceipt
    }

	/// <summary>收款登记</summary>	
	[Description("收款登记")]
    public partial class S_C_ReceiptRegister : Formula.BaseModel
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
		/// <summary>付款单位母公司</summary>	
		[Description("付款单位母公司")]
        public string CustomerInfo { get; set; } // CustomerInfo
		/// <summary>付款单位母公司名称</summary>	
		[Description("付款单位母公司名称")]
        public string CustomerInfoName { get; set; } // CustomerInfoName
		/// <summary>收款金额（元）</summary>	
		[Description("收款金额（元）")]
        public decimal? ReceiptValue { get; set; } // ReceiptValue
		/// <summary>收款凭证号</summary>	
		[Description("收款凭证号")]
        public string ReceiptNo { get; set; } // ReceiptNo
		/// <summary>收款日期</summary>	
		[Description("收款日期")]
        public DateTime? ReceiptDate { get; set; } // ReceiptDate
		/// <summary>登记人</summary>	
		[Description("登记人")]
        public string Register { get; set; } // Register
		/// <summary>登记人名称</summary>	
		[Description("登记人名称")]
        public string RegisterName { get; set; } // RegisterName
		/// <summary>登记日期</summary>	
		[Description("登记日期")]
        public DateTime? RegisterDate { get; set; } // RegisterDate
		/// <summary>备注</summary>	
		[Description("备注")]
        public string Remark { get; set; } // Remark
		/// <summary>已确认金额（元）</summary>	
		[Description("已确认金额（元）")]
        public decimal? ConfirmValue { get; set; } // ConfirmValue
		/// <summary>状态</summary>	
		[Description("状态")]
        public string RegisterState { get; set; } // RegisterState
    }

	/// <summary>客户信息维护</summary>	
	[Description("客户信息维护")]
    public partial class S_F_Customer : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public string CreateUser { get; set; } // CreateUser
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get; set; } // CreateUserID
		/// <summary></summary>	
		[Description("")]
        public DateTime? ModifyDate { get; set; } // ModifyDate
		/// <summary></summary>	
		[Description("")]
        public string ModifyUser { get; set; } // ModifyUser
		/// <summary></summary>	
		[Description("")]
        public string ModifyUserID { get; set; } // ModifyUserID
		/// <summary>客户名称</summary>	
		[Description("客户名称")]
        public string Name { get; set; } // Name
		/// <summary>集团公司</summary>	
		[Description("集团公司")]
        public string Parent { get; set; } // Parent
		/// <summary>集团公司名称</summary>	
		[Description("集团公司名称")]
        public string ParentName { get; set; } // ParentName
		/// <summary></summary>	
		[Description("")]
        public string FullID { get; set; } // FullID
		/// <summary>客户简称</summary>	
		[Description("客户简称")]
        public string ShortName { get; set; } // ShortName
		/// <summary></summary>	
		[Description("")]
        public string Code { get; set; } // Code
		/// <summary>主要联系人</summary>	
		[Description("主要联系人")]
        public string LinkUser { get; set; } // LinkUser
		/// <summary>主要联系人电话</summary>	
		[Description("主要联系人电话")]
        public string LinkTelphone { get; set; } // LinkTelphone
		/// <summary>所属行业</summary>	
		[Description("所属行业")]
        public string Industry { get; set; } // Industry
		/// <summary>是否重点客户</summary>	
		[Description("是否重点客户")]
        public string IsImportantCustomer { get; set; } // IsImportantCustomer
		/// <summary>法人代表</summary>	
		[Description("法人代表")]
        public string JuridicalPerson { get; set; } // JuridicalPerson
		/// <summary>所在国家</summary>	
		[Description("所在国家")]
        public string Country { get; set; } // Country
		/// <summary>所在省份</summary>	
		[Description("所在省份")]
        public string Province { get; set; } // Province
		/// <summary>所在城市</summary>	
		[Description("所在城市")]
        public string City { get; set; } // City
		/// <summary>所在区域</summary>	
		[Description("所在区域")]
        public string Area { get; set; } // Area
		/// <summary>邮政编码</summary>	
		[Description("邮政编码")]
        public string ZipCode { get; set; } // ZipCode
		/// <summary>联系地址</summary>	
		[Description("联系地址")]
        public string Address { get; set; } // Address
		/// <summary>开票单位名称</summary>	
		[Description("开票单位名称")]
        public string InvoiceName { get; set; } // InvoiceName
		/// <summary>纳税人识别号</summary>	
		[Description("纳税人识别号")]
        public string TaxAccounts { get; set; } // TaxAccounts
		/// <summary>相关资料</summary>	
		[Description("相关资料")]
        public string Attachment { get; set; } // Attachment
		/// <summary>备注</summary>	
		[Description("备注")]
        public string Remark { get; set; } // Remark
		/// <summary>同步协作云时间</summary>	
		[Description("同步协作云时间")]
        public DateTime? SyncCloudTime { get; set; } // SyncCloudTime
		/// <summary>是否合作过</summary>	
		[Description("是否合作过")]
        public string IsCooperated { get; set; } // IsCooperated
		/// <summary>客户等级</summary>	
		[Description("客户等级")]
        public string CustomerLevel { get; set; } // CustomerLevel
		/// <summary>客户编码</summary>	
		[Description("客户编码")]
        public string SerialNumber { get; set; } // SerialNumber
		/// <summary>首次合作日期</summary>	
		[Description("首次合作日期")]
        public DateTime? BusinessDate { get; set; } // BusinessDate
		/// <summary>工商注册号</summary>	
		[Description("工商注册号")]
        public string BusinessCode { get; set; } // BusinessCode

        // Reverse navigation
		[JsonIgnore]
        public virtual ICollection<S_C_ManageContract> S_C_ManageContract { get { onS_C_ManageContractGetting(); return _S_C_ManageContract;} }
		private ICollection<S_C_ManageContract> _S_C_ManageContract;
		partial void onS_C_ManageContractGetting();

		[JsonIgnore]
        public virtual ICollection<S_F_Customer_BankAccounts> S_F_Customer_BankAccounts { get { onS_F_Customer_BankAccountsGetting(); return _S_F_Customer_BankAccounts;} }
		private ICollection<S_F_Customer_BankAccounts> _S_F_Customer_BankAccounts;
		partial void onS_F_Customer_BankAccountsGetting();

		[JsonIgnore]
        public virtual ICollection<S_F_Customer_Complain> S_F_Customer_Complain { get { onS_F_Customer_ComplainGetting(); return _S_F_Customer_Complain;} }
		private ICollection<S_F_Customer_Complain> _S_F_Customer_Complain;
		partial void onS_F_Customer_ComplainGetting();

		[JsonIgnore]
        public virtual ICollection<S_F_Customer_ContactLog> S_F_Customer_ContactLog { get { onS_F_Customer_ContactLogGetting(); return _S_F_Customer_ContactLog;} }
		private ICollection<S_F_Customer_ContactLog> _S_F_Customer_ContactLog;
		partial void onS_F_Customer_ContactLogGetting();

		[JsonIgnore]
        public virtual ICollection<S_F_Customer_Demand> S_F_Customer_Demand { get { onS_F_Customer_DemandGetting(); return _S_F_Customer_Demand;} }
		private ICollection<S_F_Customer_Demand> _S_F_Customer_Demand;
		partial void onS_F_Customer_DemandGetting();

		[JsonIgnore]
        public virtual ICollection<S_F_Customer_LinkMan> S_F_Customer_LinkMan { get { onS_F_Customer_LinkManGetting(); return _S_F_Customer_LinkMan;} }
		private ICollection<S_F_Customer_LinkMan> _S_F_Customer_LinkMan;
		partial void onS_F_Customer_LinkManGetting();

		[JsonIgnore]
        public virtual ICollection<S_F_Customer_ReturnVisit> S_F_Customer_ReturnVisit { get { onS_F_Customer_ReturnVisitGetting(); return _S_F_Customer_ReturnVisit;} }
		private ICollection<S_F_Customer_ReturnVisit> _S_F_Customer_ReturnVisit;
		partial void onS_F_Customer_ReturnVisitGetting();


        public S_F_Customer()
        {
            _S_C_ManageContract = new List<S_C_ManageContract>();
            _S_F_Customer_BankAccounts = new List<S_F_Customer_BankAccounts>();
            _S_F_Customer_Complain = new List<S_F_Customer_Complain>();
            _S_F_Customer_ContactLog = new List<S_F_Customer_ContactLog>();
            _S_F_Customer_Demand = new List<S_F_Customer_Demand>();
            _S_F_Customer_LinkMan = new List<S_F_Customer_LinkMan>();
            _S_F_Customer_ReturnVisit = new List<S_F_Customer_ReturnVisit>();
        }
    }

	/// <summary>银行账号</summary>	
	[Description("银行账号")]
    public partial class S_F_Customer_BankAccounts : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string S_F_CustomerID { get; set; } // S_F_CustomerID
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string IsReleased { get; set; } // IsReleased
		/// <summary>开户银行</summary>	
		[Description("开户银行")]
        public string Bank { get; set; } // Bank
		/// <summary>开户银行名称</summary>	
		[Description("开户银行名称")]
        public string BankName { get; set; } // BankName
		/// <summary>银行账号</summary>	
		[Description("银行账号")]
        public string BankAccount { get; set; } // BankAccount
		/// <summary>账户名称</summary>	
		[Description("账户名称")]
        public string BankAccountName { get; set; } // BankAccountName
		/// <summary>开户行地址</summary>	
		[Description("开户行地址")]
        public string BankInfo { get; set; } // BankInfo

        // Foreign keys
		[JsonIgnore]
        public virtual S_F_Customer S_F_Customer { get; set; } //  S_F_CustomerID - FK_S_F_Customer_BankAccounts_S_F_Customer
    }

	/// <summary>客户大事记</summary>	
	[Description("客户大事记")]
    public partial class S_F_Customer_BigEvent : Formula.BaseModel
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
		/// <summary>客户名称</summary>	
		[Description("客户名称")]
        public string CustomerInfo { get; set; } // CustomerInfo
		/// <summary>客户名称名称</summary>	
		[Description("客户名称名称")]
        public string CustomerInfoName { get; set; } // CustomerInfoName
		/// <summary>事项标题</summary>	
		[Description("事项标题")]
        public string EventTitle { get; set; } // EventTitle
		/// <summary>登记人员</summary>	
		[Description("登记人员")]
        public string Register { get; set; } // Register
		/// <summary>登记人员名称</summary>	
		[Description("登记人员名称")]
        public string RegisterName { get; set; } // RegisterName
		/// <summary>事项内容</summary>	
		[Description("事项内容")]
        public string EventContent { get; set; } // EventContent
		/// <summary>登记日期</summary>	
		[Description("登记日期")]
        public DateTime? RegisteDate { get; set; } // RegisteDate
    }

	/// <summary>客户投诉管理</summary>	
	[Description("客户投诉管理")]
    public partial class S_F_Customer_Complain : Formula.BaseModel
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
		/// <summary>投诉单位</summary>	
		[Description("投诉单位")]
        public string CustomerInfo { get; set; } // CustomerInfo
		/// <summary>投诉单位名称</summary>	
		[Description("投诉单位名称")]
        public string CustomerInfoName { get; set; } // CustomerInfoName
		/// <summary>投诉人</summary>	
		[Description("投诉人")]
        public string ComplainManName { get; set; } // ComplainManName
		/// <summary>联系方式</summary>	
		[Description("联系方式")]
        public string ContactWay { get; set; } // ContactWay
		/// <summary>被投诉项目</summary>	
		[Description("被投诉项目")]
        public string ComplainProjectInfo { get; set; } // ComplainProjectInfo
		/// <summary>被投诉项目名称</summary>	
		[Description("被投诉项目名称")]
        public string ComplainProjectInfoName { get; set; } // ComplainProjectInfoName
		/// <summary>被投诉部门</summary>	
		[Description("被投诉部门")]
        public string ComplainDept { get; set; } // ComplainDept
		/// <summary>被投诉部门名称</summary>	
		[Description("被投诉部门名称")]
        public string ComplainDeptName { get; set; } // ComplainDeptName
		/// <summary>投诉问题</summary>	
		[Description("投诉问题")]
        public string ComplainQuestion { get; set; } // ComplainQuestion
		/// <summary>处理单位</summary>	
		[Description("处理单位")]
        public string ManageDept { get; set; } // ManageDept
		/// <summary>处理单位名称</summary>	
		[Description("处理单位名称")]
        public string ManageDeptName { get; set; } // ManageDeptName
		/// <summary>处理人</summary>	
		[Description("处理人")]
        public string Manager { get; set; } // Manager
		/// <summary>处理人名称</summary>	
		[Description("处理人名称")]
        public string ManagerName { get; set; } // ManagerName
		/// <summary>处理措施</summary>	
		[Description("处理措施")]
        public string ManageMethod { get; set; } // ManageMethod
		/// <summary>处理结果</summary>	
		[Description("处理结果")]
        public string ManageResult { get; set; } // ManageResult
		/// <summary>附件</summary>	
		[Description("附件")]
        public string Attachment { get; set; } // Attachment

        // Foreign keys
		[JsonIgnore]
        public virtual S_F_Customer S_F_Customer { get; set; } //  CustomerInfo - FK_S_F_Customer_Complain_S_F_Customer
    }

	/// <summary>客户联系记录</summary>	
	[Description("客户联系记录")]
    public partial class S_F_Customer_ContactLog : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string S_F_CustomerID { get; set; } // S_F_CustomerID
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string IsReleased { get; set; } // IsReleased
		/// <summary>联系时间</summary>	
		[Description("联系时间")]
        public DateTime? ContactDate { get; set; } // ContactDate
		/// <summary>客户联系人</summary>	
		[Description("客户联系人")]
        public string CustomerPerson { get; set; } // CustomerPerson
		/// <summary>本公司联系人</summary>	
		[Description("本公司联系人")]
        public string CompanyPerson { get; set; } // CompanyPerson
		/// <summary>本公司联系人名称</summary>	
		[Description("本公司联系人名称")]
        public string CompanyPersonName { get; set; } // CompanyPersonName
		/// <summary>联系方式</summary>	
		[Description("联系方式")]
        public string ContactType { get; set; } // ContactType
		/// <summary>联系内容</summary>	
		[Description("联系内容")]
        public string ContactContent { get; set; } // ContactContent
		/// <summary>备注</summary>	
		[Description("备注")]
        public string Remark { get; set; } // Remark

        // Foreign keys
		[JsonIgnore]
        public virtual S_F_Customer S_F_Customer { get; set; } //  S_F_CustomerID - FK_S_F_Customer_ContactLog_S_F_Customer
    }

	/// <summary>客户需求管理</summary>	
	[Description("客户需求管理")]
    public partial class S_F_Customer_Demand : Formula.BaseModel
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
		/// <summary>客户名称</summary>	
		[Description("客户名称")]
        public string CustomerInfo { get; set; } // CustomerInfo
		/// <summary>客户名称名称</summary>	
		[Description("客户名称名称")]
        public string CustomerInfoName { get; set; } // CustomerInfoName
		/// <summary>联系地址</summary>	
		[Description("联系地址")]
        public string Address { get; set; } // Address
		/// <summary>所属行业</summary>	
		[Description("所属行业")]
        public string Industry { get; set; } // Industry
		/// <summary>登记人</summary>	
		[Description("登记人")]
        public string RegisterUser { get; set; } // RegisterUser
		/// <summary>登记人名称</summary>	
		[Description("登记人名称")]
        public string RegisterUserName { get; set; } // RegisterUserName
		/// <summary>登记日期</summary>	
		[Description("登记日期")]
        public DateTime? RegisterDate { get; set; } // RegisterDate
		/// <summary>需求说明</summary>	
		[Description("需求说明")]
        public string DemandDescription { get; set; } // DemandDescription
		/// <summary>附件</summary>	
		[Description("附件")]
        public string Attachment { get; set; } // Attachment
		/// <summary>项目名称</summary>	
		[Description("项目名称")]
        public string ProjectName { get; set; } // ProjectName

        // Foreign keys
		[JsonIgnore]
        public virtual S_F_Customer S_F_Customer { get; set; } //  CustomerInfo - FK_S_F_Customer_Demand_S_F_Customer_Demand
    }

	/// <summary>客户联系人</summary>	
	[Description("客户联系人")]
    public partial class S_F_Customer_LinkMan : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string S_F_CustomerID { get; set; } // S_F_CustomerID
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string IsReleased { get; set; } // IsReleased
		/// <summary>联系人姓名</summary>	
		[Description("联系人姓名")]
        public string LinkmanName { get; set; } // LinkmanName
		/// <summary>部门</summary>	
		[Description("部门")]
        public string DeptName { get; set; } // DeptName
		/// <summary>职务</summary>	
		[Description("职务")]
        public string Position { get; set; } // Position
		/// <summary>负责事务</summary>	
		[Description("负责事务")]
        public string ResponsiblePerson { get; set; } // ResponsiblePerson
		/// <summary>移动电话</summary>	
		[Description("移动电话")]
        public string MobilePhone { get; set; } // MobilePhone
		/// <summary>办公电话</summary>	
		[Description("办公电话")]
        public string OfficeTelephone { get; set; } // OfficeTelephone
		/// <summary>传真</summary>	
		[Description("传真")]
        public string Fax { get; set; } // Fax
		/// <summary>邮箱</summary>	
		[Description("邮箱")]
        public string Email { get; set; } // Email
		/// <summary>联系地址</summary>	
		[Description("联系地址")]
        public string OfficeRoom { get; set; } // OfficeRoom
		/// <summary>传真名称</summary>	
		[Description("传真名称")]
        public string FaxName { get; set; } // FaxName

        // Foreign keys
		[JsonIgnore]
        public virtual S_F_Customer S_F_Customer { get; set; } //  S_F_CustomerID - FK_S_F_Customer_LinkMan_S_F_Customer
    }

	/// <summary>客户回访记录</summary>	
	[Description("客户回访记录")]
    public partial class S_F_Customer_ReturnVisit : Formula.BaseModel
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
		/// <summary>回访主题</summary>	
		[Description("回访主题")]
        public string Name { get; set; } // Name
		/// <summary>登记人</summary>	
		[Description("登记人")]
        public string Register { get; set; } // Register
		/// <summary>登记人名称</summary>	
		[Description("登记人名称")]
        public string RegisterName { get; set; } // RegisterName
		/// <summary>登记日期</summary>	
		[Description("登记日期")]
        public DateTime? RegisterDate { get; set; } // RegisterDate
		/// <summary>客户名称</summary>	
		[Description("客户名称")]
        public string CustomerInfo { get; set; } // CustomerInfo
		/// <summary>客户名称名称</summary>	
		[Description("客户名称名称")]
        public string CustomerInfoName { get; set; } // CustomerInfoName
		/// <summary>拜访人员</summary>	
		[Description("拜访人员")]
        public string VisitManName { get; set; } // VisitManName
		/// <summary>部门</summary>	
		[Description("部门")]
        public string DeptName { get; set; } // DeptName
		/// <summary>职务</summary>	
		[Description("职务")]
        public string Duties { get; set; } // Duties
		/// <summary>回访人</summary>	
		[Description("回访人")]
        public string ReturnVisitMan { get; set; } // ReturnVisitMan
		/// <summary>回访人名称</summary>	
		[Description("回访人名称")]
        public string ReturnVisitManName { get; set; } // ReturnVisitManName
		/// <summary>回访日期</summary>	
		[Description("回访日期")]
        public DateTime? VisitDate { get; set; } // VisitDate
		/// <summary>回访事务</summary>	
		[Description("回访事务")]
        public string Content { get; set; } // Content
		/// <summary>回访纪要</summary>	
		[Description("回访纪要")]
        public string Record { get; set; } // Record
		/// <summary>相关资料</summary>	
		[Description("相关资料")]
        public string Attachment { get; set; } // Attachment
		/// <summary>备注</summary>	
		[Description("备注")]
        public string Remark { get; set; } // Remark

        // Foreign keys
		[JsonIgnore]
        public virtual S_F_Customer S_F_Customer { get; set; } //  CustomerInfo - FK_S_F_Customer_ReturnVisit_S_F_Customer
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

	/// <summary>工程信息管理</summary>	
	[Description("工程信息管理")]
    public partial class S_I_Engineering : Formula.BaseModel
    {
		/// <summary>主键ID</summary>	
		[Description("主键ID")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary>工程名称</summary>	
		[Description("工程名称")]
        public string Name { get; set; } // Name
		/// <summary>工程编号</summary>	
		[Description("工程编号")]
        public string Code { get; set; } // Code
		/// <summary>工程类型</summary>	
		[Description("工程类型")]
        public string Class { get; set; } // Class
		/// <summary>工程规模</summary>	
		[Description("工程规模")]
        public string Scale { get; set; } // Scale
		/// <summary>主要承接部门</summary>	
		[Description("主要承接部门")]
        public string MainDept { get; set; } // MainDept
		/// <summary>主要承接部门名称</summary>	
		[Description("主要承接部门名称")]
        public string MainDeptName { get; set; } // MainDeptName
		/// <summary>备注</summary>	
		[Description("备注")]
        public string Remark { get; set; } // Remark
		/// <summary>相关附件</summary>	
		[Description("相关附件")]
        public string Attachment { get; set; } // Attachment
		/// <summary>投资额</summary>	
		[Description("投资额")]
        public decimal? Investment { get; set; } // Investment
		/// <summary>建设地点</summary>	
		[Description("建设地点")]
        public string Address { get; set; } // Address
		/// <summary>国家</summary>	
		[Description("国家")]
        public string Country { get; set; } // Country
		/// <summary>省份</summary>	
		[Description("省份")]
        public string Proinvce { get; set; } // Proinvce
		/// <summary>城市</summary>	
		[Description("城市")]
        public string City { get; set; } // City
		/// <summary>建筑面积</summary>	
		[Description("建筑面积")]
        public string Proportion { get; set; } // Proportion
		/// <summary>商务负责人</summary>	
		[Description("商务负责人")]
        public string BusinessManager { get; set; } // BusinessManager
		/// <summary>商务负责人名称</summary>	
		[Description("商务负责人名称")]
        public string BusinessManagerName { get; set; } // BusinessManagerName
		/// <summary>阶段</summary>	
		[Description("阶段")]
        public string PhaseValue { get; set; } // PhaseValue
		/// <summary>阶段名称</summary>	
		[Description("阶段名称")]
        public string PhaseContent { get; set; } // PhaseContent
		/// <summary>省份</summary>	
		[Description("省份")]
        public string Province { get; set; } // Province
		/// <summary>客户母公司</summary>	
		[Description("客户母公司")]
        public string CustomerInfo { get; set; } // CustomerInfo
		/// <summary>客户母公司名称</summary>	
		[Description("客户母公司名称")]
        public string CustomerInfoName { get; set; } // CustomerInfoName
		/// <summary>协作部门</summary>	
		[Description("协作部门")]
        public string OtherDept { get; set; } // OtherDept
		/// <summary>协作部门名称</summary>	
		[Description("协作部门名称")]
        public string OtherDeptName { get; set; } // OtherDeptName
		/// <summary>总用地面积</summary>	
		[Description("总用地面积")]
        public string LandingArea { get; set; } // LandingArea
		/// <summary>工程负责人</summary>	
		[Description("工程负责人")]
        public string ChargerUser { get; set; } // ChargerUser
		/// <summary>工程负责人名称</summary>	
		[Description("工程负责人名称")]
        public string ChargerUserName { get; set; } // ChargerUserName
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public string CreateUser { get; set; } // CreateUser
		/// <summary></summary>	
		[Description("")]
        public string CreateUserID { get; set; } // CreateUserID
		/// <summary>顾客要求评审</summary>	
		[Description("顾客要求评审")]
        public string CustomerRequestReview { get; set; } // CustomerRequestReview
		/// <summary>顾客要求评审名称</summary>	
		[Description("顾客要求评审名称")]
        public string CustomerRequestReviewName { get; set; } // CustomerRequestReviewName
		/// <summary></summary>	
		[Description("")]
        public string Area { get; set; } // Area
    }

	/// <summary></summary>	
	[Description("")]
    public partial class S_I_Project : Formula.BaseModel
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
        public string ProjectScale { get; set; } // ProjectScale
		/// <summary></summary>	
		[Description("")]
        public string ProjectClass { get; set; } // ProjectClass
		/// <summary></summary>	
		[Description("")]
        public string ProjectType { get; set; } // ProjectType
		/// <summary></summary>	
		[Description("")]
        public string Industry { get; set; } // Industry
		/// <summary></summary>	
		[Description("")]
        public string Customer { get; set; } // Customer
		/// <summary></summary>	
		[Description("")]
        public string CustomerName { get; set; } // CustomerName
		/// <summary></summary>	
		[Description("")]
        public string CustomerFullID { get; set; } // CustomerFullID
		/// <summary></summary>	
		[Description("")]
        public string ProjectLevel { get; set; } // ProjectLevel
		/// <summary></summary>	
		[Description("")]
        public string CustomerLevel { get; set; } // CustomerLevel
		/// <summary></summary>	
		[Description("")]
        public string Phase { get; set; } // Phase
		/// <summary></summary>	
		[Description("")]
        public string Major { get; set; } // Major
		/// <summary></summary>	
		[Description("")]
        public string ChargerDeptName { get; set; } // ChargerDeptName
		/// <summary></summary>	
		[Description("")]
        public string ChargerDept { get; set; } // ChargerDept
		/// <summary></summary>	
		[Description("")]
        public string ChargerUserName { get; set; } // ChargerUserName
		/// <summary></summary>	
		[Description("")]
        public string ChargerUser { get; set; } // ChargerUser
		/// <summary></summary>	
		[Description("")]
        public string MarketDeptName { get; set; } // MarketDeptName
		/// <summary></summary>	
		[Description("")]
        public string MarketDept { get; set; } // MarketDept
		/// <summary></summary>	
		[Description("")]
        public string BusinessChargerUserName { get; set; } // BusinessChargerUserName
		/// <summary></summary>	
		[Description("")]
        public string BusinessChargerUser { get; set; } // BusinessChargerUser
		/// <summary></summary>	
		[Description("")]
        public string OtherDeptName { get; set; } // OtherDeptName
		/// <summary></summary>	
		[Description("")]
        public string OtherDept { get; set; } // OtherDept
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
        public string BuildAddress { get; set; } // BuildAddress
		/// <summary></summary>	
		[Description("")]
        public string State { get; set; } // State
		/// <summary></summary>	
		[Description("")]
        public string Remark { get; set; } // Remark
		/// <summary></summary>	
		[Description("")]
        public string Attachments { get; set; } // Attachments
		/// <summary></summary>	
		[Description("")]
        public string MakertClueID { get; set; } // MakertClueID
		/// <summary></summary>	
		[Description("")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public string TasKNoticeID { get; set; } // TasKNoticeID
		/// <summary></summary>	
		[Description("")]
        public string EngineeringInfo { get; set; } // EngineeringInfo
		/// <summary></summary>	
		[Description("")]
        public string OrgID { get; set; } // OrgID
		/// <summary></summary>	
		[Description("")]
        public string OrgName { get; set; } // OrgName
		/// <summary></summary>	
		[Description("")]
        public string TasKNoticeTmplCode { get; set; } // TasKNoticeTmplCode
		/// <summary></summary>	
		[Description("")]
        public string Area { get; set; } // Area
    }

	/// <summary></summary>	
	[Description("")]
    public partial class S_KPI_Category : Formula.BaseModel
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
        public string OrgType { get; set; } // OrgType
		/// <summary></summary>	
		[Description("")]
        public int SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string State { get; set; } // State
    }

	/// <summary></summary>	
	[Description("")]
    public partial class S_KPI_IndicatorCompany : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public int BelongYear { get; set; } // BelongYear
		/// <summary></summary>	
		[Description("")]
        public int? BelongMonth { get; set; } // BelongMonth
		/// <summary></summary>	
		[Description("")]
        public int? BelongQuarter { get; set; } // BelongQuarter
		/// <summary></summary>	
		[Description("")]
        public string IndicatorType { get; set; } // IndicatorType
		/// <summary></summary>	
		[Description("")]
        public string BusiniessCategory { get; set; } // BusiniessCategory
		/// <summary></summary>	
		[Description("")]
        public decimal? ProjectValue { get; set; } // ProjectValue
		/// <summary></summary>	
		[Description("")]
        public decimal? ProjectValueCost { get; set; } // ProjectValueCost
		/// <summary></summary>	
		[Description("")]
        public decimal? ContractValue { get; set; } // ContractValue
		/// <summary></summary>	
		[Description("")]
        public decimal? ContractValueCost { get; set; } // ContractValueCost
		/// <summary></summary>	
		[Description("")]
        public decimal? ReceiptValue { get; set; } // ReceiptValue
		/// <summary></summary>	
		[Description("")]
        public decimal? ReceiptValueCost { get; set; } // ReceiptValueCost
		/// <summary></summary>	
		[Description("")]
        public decimal? ContractValue2 { get; set; } // ContractValue2
		/// <summary></summary>	
		[Description("")]
        public decimal? ContractValue2Cost { get; set; } // ContractValue2Cost
		/// <summary></summary>	
		[Description("")]
        public decimal? PlanReceiptValue { get; set; } // PlanReceiptValue
		/// <summary></summary>	
		[Description("")]
        public decimal? PlanReceiptValueCost { get; set; } // PlanReceiptValueCost
		/// <summary></summary>	
		[Description("")]
        public decimal? ProductionValue { get; set; } // ProductionValue
		/// <summary></summary>	
		[Description("")]
        public decimal? ProductionValueCost { get; set; } // ProductionValueCost
    }

	/// <summary></summary>	
	[Description("")]
    public partial class S_KPI_IndicatorConfig : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string IndicatorName { get; set; } // IndicatorName
		/// <summary></summary>	
		[Description("")]
        public string IndicatorCode { get; set; } // IndicatorCode
		/// <summary></summary>	
		[Description("")]
        public string DataSource { get; set; } // DataSource
		/// <summary></summary>	
		[Description("")]
        public string RelateCaculateTable { get; set; } // RelateCaculateTable
		/// <summary></summary>	
		[Description("")]
        public string RelateCaculateField { get; set; } // RelateCaculateField
		/// <summary></summary>	
		[Description("")]
        public string RelateUserField { get; set; } // RelateUserField
		/// <summary></summary>	
		[Description("")]
        public string RelateUserNameField { get; set; } // RelateUserNameField
		/// <summary></summary>	
		[Description("")]
        public string RelateOrgField { get; set; } // RelateOrgField
		/// <summary></summary>	
		[Description("")]
        public string RelateOrgNameField { get; set; } // RelateOrgNameField
		/// <summary></summary>	
		[Description("")]
        public string YearField { get; set; } // YearField
		/// <summary></summary>	
		[Description("")]
        public string MonthField { get; set; } // MonthField
		/// <summary></summary>	
		[Description("")]
        public string QuarterField { get; set; } // QuarterField
		/// <summary></summary>	
		[Description("")]
        public int? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string State { get; set; } // State
		/// <summary></summary>	
		[Description("")]
        public string OrgType { get; set; } // OrgType
    }

	/// <summary></summary>	
	[Description("")]
    public partial class S_KPI_IndicatorOrg : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public int BelongYear { get; set; } // BelongYear
		/// <summary></summary>	
		[Description("")]
        public int? BelongMonth { get; set; } // BelongMonth
		/// <summary></summary>	
		[Description("")]
        public int? BelongQuarter { get; set; } // BelongQuarter
		/// <summary></summary>	
		[Description("")]
        public string OrgID { get; set; } // OrgID
		/// <summary></summary>	
		[Description("")]
        public string OrgName { get; set; } // OrgName
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string IndicatorType { get; set; } // IndicatorType
		/// <summary></summary>	
		[Description("")]
        public string BusiniessCategory { get; set; } // BusiniessCategory
		/// <summary></summary>	
		[Description("")]
        public string ParentIndicatorID { get; set; } // ParentIndicatorID
		/// <summary></summary>	
		[Description("")]
        public decimal? ProjectValue { get; set; } // ProjectValue
		/// <summary></summary>	
		[Description("")]
        public decimal? ProjectValueCost { get; set; } // ProjectValueCost
		/// <summary></summary>	
		[Description("")]
        public decimal? ContractValue { get; set; } // ContractValue
		/// <summary></summary>	
		[Description("")]
        public decimal? ContractValueCost { get; set; } // ContractValueCost
		/// <summary></summary>	
		[Description("")]
        public decimal? ReceiptValue { get; set; } // ReceiptValue
		/// <summary></summary>	
		[Description("")]
        public decimal? ReceiptValueCost { get; set; } // ReceiptValueCost
		/// <summary></summary>	
		[Description("")]
        public decimal? ContractValue2 { get; set; } // ContractValue2
		/// <summary></summary>	
		[Description("")]
        public decimal? ContractValue2Cost { get; set; } // ContractValue2Cost
		/// <summary></summary>	
		[Description("")]
        public decimal? PlanReceiptValue { get; set; } // PlanReceiptValue
		/// <summary></summary>	
		[Description("")]
        public decimal? PlanReceiptValueCost { get; set; } // PlanReceiptValueCost
		/// <summary></summary>	
		[Description("")]
        public decimal? ProductionValue { get; set; } // ProductionValue
		/// <summary></summary>	
		[Description("")]
        public decimal? ProductionValueCost { get; set; } // ProductionValueCost
    }

	/// <summary></summary>	
	[Description("")]
    public partial class S_KPI_IndicatorPerson : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public int BelongYear { get; set; } // BelongYear
		/// <summary></summary>	
		[Description("")]
        public int? BelongMonth { get; set; } // BelongMonth
		/// <summary></summary>	
		[Description("")]
        public int? BelongQuarter { get; set; } // BelongQuarter
		/// <summary></summary>	
		[Description("")]
        public string Name { get; set; } // Name
		/// <summary></summary>	
		[Description("")]
        public string Code { get; set; } // Code
		/// <summary></summary>	
		[Description("")]
        public string UserID { get; set; } // UserID
		/// <summary></summary>	
		[Description("")]
        public string UserName { get; set; } // UserName
		/// <summary></summary>	
		[Description("")]
        public string IndicatorType { get; set; } // IndicatorType
		/// <summary></summary>	
		[Description("")]
        public string BusiniessCategory { get; set; } // BusiniessCategory
		/// <summary></summary>	
		[Description("")]
        public string ParentIndicatorID { get; set; } // ParentIndicatorID
		/// <summary></summary>	
		[Description("")]
        public decimal? ProjectValue { get; set; } // ProjectValue
		/// <summary></summary>	
		[Description("")]
        public decimal? ProjectValueCost { get; set; } // ProjectValueCost
		/// <summary></summary>	
		[Description("")]
        public decimal? ContractValue { get; set; } // ContractValue
		/// <summary></summary>	
		[Description("")]
        public decimal? ContractValueCost { get; set; } // ContractValueCost
		/// <summary></summary>	
		[Description("")]
        public decimal? ReceiptValue { get; set; } // ReceiptValue
		/// <summary></summary>	
		[Description("")]
        public decimal? ReceiptValueCost { get; set; } // ReceiptValueCost
		/// <summary></summary>	
		[Description("")]
        public decimal? ContractValue2 { get; set; } // ContractValue2
		/// <summary></summary>	
		[Description("")]
        public decimal? ContractValue2Cost { get; set; } // ContractValue2Cost
		/// <summary></summary>	
		[Description("")]
        public decimal? PlanReceiptValue { get; set; } // PlanReceiptValue
		/// <summary></summary>	
		[Description("")]
        public decimal? PlanReceiptValueCost { get; set; } // PlanReceiptValueCost
		/// <summary></summary>	
		[Description("")]
        public decimal? ProductionValue { get; set; } // ProductionValue
		/// <summary></summary>	
		[Description("")]
        public decimal? ProductionValueCost { get; set; } // ProductionValueCost
    }

	/// <summary></summary>	
	[Description("")]
    public partial class S_KPITemp_IndicatorCompany : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public int BelongYear { get; set; } // BelongYear
		/// <summary></summary>	
		[Description("")]
        public int? BelongMonth { get; set; } // BelongMonth
		/// <summary></summary>	
		[Description("")]
        public int? BelongQuarter { get; set; } // BelongQuarter
		/// <summary></summary>	
		[Description("")]
        public string IndicatorType { get; set; } // IndicatorType
		/// <summary></summary>	
		[Description("")]
        public string BusiniessCategory { get; set; } // BusiniessCategory
		/// <summary></summary>	
		[Description("")]
        public DateTime CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public int Version { get; set; } // Version
		/// <summary></summary>	
		[Description("")]
        public string CurrentVersion { get; set; } // CurrentVersion
		/// <summary></summary>	
		[Description("")]
        public decimal? ProjectValue { get; set; } // ProjectValue
		/// <summary></summary>	
		[Description("")]
        public decimal? ProjectValueCost { get; set; } // ProjectValueCost
		/// <summary></summary>	
		[Description("")]
        public decimal? ContractValue { get; set; } // ContractValue
		/// <summary></summary>	
		[Description("")]
        public decimal? ContractValueCost { get; set; } // ContractValueCost
		/// <summary></summary>	
		[Description("")]
        public decimal? ReceiptValue { get; set; } // ReceiptValue
		/// <summary></summary>	
		[Description("")]
        public decimal? ReceiptValueCost { get; set; } // ReceiptValueCost
		/// <summary></summary>	
		[Description("")]
        public decimal? ContractValue2 { get; set; } // ContractValue2
		/// <summary></summary>	
		[Description("")]
        public decimal? ContractValue2Cost { get; set; } // ContractValue2Cost
		/// <summary></summary>	
		[Description("")]
        public decimal? PlanReceiptValue { get; set; } // PlanReceiptValue
		/// <summary></summary>	
		[Description("")]
        public decimal? PlanReceiptValueCost { get; set; } // PlanReceiptValueCost
		/// <summary></summary>	
		[Description("")]
        public decimal? ProductionValue { get; set; } // ProductionValue
		/// <summary></summary>	
		[Description("")]
        public decimal? ProductionValueCost { get; set; } // ProductionValueCost
    }

	/// <summary></summary>	
	[Description("")]
    public partial class S_KPITemp_IndicatorOrg : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public int BelongYear { get; set; } // BelongYear
		/// <summary></summary>	
		[Description("")]
        public int? BelongMonth { get; set; } // BelongMonth
		/// <summary></summary>	
		[Description("")]
        public int? BelongQuarter { get; set; } // BelongQuarter
		/// <summary></summary>	
		[Description("")]
        public string OrgID { get; set; } // OrgID
		/// <summary></summary>	
		[Description("")]
        public string OrgName { get; set; } // OrgName
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string IndicatorType { get; set; } // IndicatorType
		/// <summary></summary>	
		[Description("")]
        public string BusiniessCategory { get; set; } // BusiniessCategory
		/// <summary></summary>	
		[Description("")]
        public DateTime CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public int Version { get; set; } // Version
		/// <summary></summary>	
		[Description("")]
        public string CurrentVersion { get; set; } // CurrentVersion
		/// <summary></summary>	
		[Description("")]
        public decimal? ProjectValue { get; set; } // ProjectValue
		/// <summary></summary>	
		[Description("")]
        public decimal? ProjectValueCost { get; set; } // ProjectValueCost
		/// <summary></summary>	
		[Description("")]
        public decimal? ContractValue { get; set; } // ContractValue
		/// <summary></summary>	
		[Description("")]
        public decimal? ContractValueCost { get; set; } // ContractValueCost
		/// <summary></summary>	
		[Description("")]
        public decimal? ReceiptValue { get; set; } // ReceiptValue
		/// <summary></summary>	
		[Description("")]
        public decimal? ReceiptValueCost { get; set; } // ReceiptValueCost
		/// <summary></summary>	
		[Description("")]
        public decimal? ContractValue2 { get; set; } // ContractValue2
		/// <summary></summary>	
		[Description("")]
        public decimal? ContractValue2Cost { get; set; } // ContractValue2Cost
		/// <summary></summary>	
		[Description("")]
        public decimal? PlanReceiptValue { get; set; } // PlanReceiptValue
		/// <summary></summary>	
		[Description("")]
        public decimal? PlanReceiptValueCost { get; set; } // PlanReceiptValueCost
		/// <summary></summary>	
		[Description("")]
        public decimal? ProductionValue { get; set; } // ProductionValue
		/// <summary></summary>	
		[Description("")]
        public decimal? ProductionValueCost { get; set; } // ProductionValueCost
    }

	/// <summary></summary>	
	[Description("")]
    public partial class S_KPITemp_IndicatorPerson : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public int BelongYear { get; set; } // BelongYear
		/// <summary></summary>	
		[Description("")]
        public int? BelongMonth { get; set; } // BelongMonth
		/// <summary></summary>	
		[Description("")]
        public int? BelongQuarter { get; set; } // BelongQuarter
		/// <summary></summary>	
		[Description("")]
        public string UserID { get; set; } // UserID
		/// <summary></summary>	
		[Description("")]
        public string UserName { get; set; } // UserName
		/// <summary></summary>	
		[Description("")]
        public string IndicatorType { get; set; } // IndicatorType
		/// <summary></summary>	
		[Description("")]
        public string BusiniessCategory { get; set; } // BusiniessCategory
		/// <summary></summary>	
		[Description("")]
        public DateTime CreateDate { get; set; } // CreateDate
		/// <summary></summary>	
		[Description("")]
        public int Version { get; set; } // Version
		/// <summary></summary>	
		[Description("")]
        public string CurrentVersion { get; set; } // CurrentVersion
		/// <summary></summary>	
		[Description("")]
        public decimal? ProjectValue { get; set; } // ProjectValue
		/// <summary></summary>	
		[Description("")]
        public decimal? ProjectValueCost { get; set; } // ProjectValueCost
		/// <summary></summary>	
		[Description("")]
        public decimal? ContractValue { get; set; } // ContractValue
		/// <summary></summary>	
		[Description("")]
        public decimal? ContractValueCost { get; set; } // ContractValueCost
		/// <summary></summary>	
		[Description("")]
        public decimal? ReceiptValue { get; set; } // ReceiptValue
		/// <summary></summary>	
		[Description("")]
        public decimal? ReceiptValueCost { get; set; } // ReceiptValueCost
		/// <summary></summary>	
		[Description("")]
        public decimal? ContractValue2 { get; set; } // ContractValue2
		/// <summary></summary>	
		[Description("")]
        public decimal? ContractValue2Cost { get; set; } // ContractValue2Cost
		/// <summary></summary>	
		[Description("")]
        public decimal? PlanReceiptValue { get; set; } // PlanReceiptValue
		/// <summary></summary>	
		[Description("")]
        public decimal? PlanReceiptValueCost { get; set; } // PlanReceiptValueCost
		/// <summary></summary>	
		[Description("")]
        public decimal? ProductionValue { get; set; } // ProductionValue
		/// <summary></summary>	
		[Description("")]
        public decimal? ProductionValueCost { get; set; } // ProductionValueCost
    }

	/// <summary>经营项目管理</summary>	
	[Description("经营项目管理")]
    public partial class S_P_MarketClue : Formula.BaseModel
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
		/// <summary>集团公司</summary>	
		[Description("集团公司")]
        public string CustomerInfo { get; set; } // CustomerInfo
		/// <summary>集团公司名称</summary>	
		[Description("集团公司名称")]
        public string CustomerInfoName { get; set; } // CustomerInfoName
		/// <summary>客户名称</summary>	
		[Description("客户名称")]
        public string CustomerInfoSub { get; set; } // CustomerInfoSub
		/// <summary>客户名称名称</summary>	
		[Description("客户名称名称")]
        public string CustomerInfoSubName { get; set; } // CustomerInfoSubName
		/// <summary>项目名称</summary>	
		[Description("项目名称")]
        public string Name { get; set; } // Name
		/// <summary>项目编号</summary>	
		[Description("项目编号")]
        public string SerialNumber { get; set; } // SerialNumber
		/// <summary>建设地点</summary>	
		[Description("建设地点")]
        public string Address { get; set; } // Address
		/// <summary>所在国家</summary>	
		[Description("所在国家")]
        public string Country { get; set; } // Country
		/// <summary>省份</summary>	
		[Description("省份")]
        public string Province { get; set; } // Province
		/// <summary>城市</summary>	
		[Description("城市")]
        public string City { get; set; } // City
		/// <summary>项目内容</summary>	
		[Description("项目内容")]
        public string Content { get; set; } // Content
		/// <summary>投资金额</summary>	
		[Description("投资金额")]
        public string Investment { get; set; } // Investment
		/// <summary>预计合同金额</summary>	
		[Description("预计合同金额")]
        public string ContractValue { get; set; } // ContractValue
		/// <summary>商务负责人</summary>	
		[Description("商务负责人")]
        public string BusinessManager { get; set; } // BusinessManager
		/// <summary>商务负责人名称</summary>	
		[Description("商务负责人名称")]
        public string BusinessManagerName { get; set; } // BusinessManagerName
		/// <summary>技术支持</summary>	
		[Description("技术支持")]
        public string TechCharger { get; set; } // TechCharger
		/// <summary>技术支持名称</summary>	
		[Description("技术支持名称")]
        public string TechChargerName { get; set; } // TechChargerName
		/// <summary>其他支持</summary>	
		[Description("其他支持")]
        public string OtherUser { get; set; } // OtherUser
		/// <summary>其他支持名称</summary>	
		[Description("其他支持名称")]
        public string OtherUserName { get; set; } // OtherUserName
		/// <summary>要求开始日期</summary>	
		[Description("要求开始日期")]
        public DateTime? RequiredStartDate { get; set; } // RequiredStartDate
		/// <summary>要求完工日期</summary>	
		[Description("要求完工日期")]
        public DateTime? RequiredFinishDate { get; set; } // RequiredFinishDate
		/// <summary>竞争对手说明</summary>	
		[Description("竞争对手说明")]
        public string Competitor { get; set; } // Competitor
		/// <summary>备注</summary>	
		[Description("备注")]
        public string Remark { get; set; } // Remark
		/// <summary>资料附件</summary>	
		[Description("资料附件")]
        public string Attachment { get; set; } // Attachment
		/// <summary>状态</summary>	
		[Description("状态")]
        public string State { get; set; } // State
		/// <summary>所属工程</summary>	
		[Description("所属工程")]
        public string EngineeringInfo { get; set; } // EngineeringInfo
		/// <summary>所属工程名称</summary>	
		[Description("所属工程名称")]
        public string EngineeringInfoName { get; set; } // EngineeringInfoName
		/// <summary>所属部门</summary>	
		[Description("所属部门")]
        public string DeptInfo { get; set; } // DeptInfo
		/// <summary>所属部门名称</summary>	
		[Description("所属部门名称")]
        public string DeptInfoName { get; set; } // DeptInfoName
		/// <summary>业务类型</summary>	
		[Description("业务类型")]
        public string ProjectClass { get; set; } // ProjectClass
		/// <summary>来源</summary>	
		[Description("来源")]
        public string Source { get; set; } // Source

        // Reverse navigation
		[JsonIgnore]
        public virtual ICollection<S_P_MarketClue_TraceInfo> S_P_MarketClue_TraceInfo { get { onS_P_MarketClue_TraceInfoGetting(); return _S_P_MarketClue_TraceInfo;} }
		private ICollection<S_P_MarketClue_TraceInfo> _S_P_MarketClue_TraceInfo;
		partial void onS_P_MarketClue_TraceInfoGetting();


        public S_P_MarketClue()
        {
            _S_P_MarketClue_TraceInfo = new List<S_P_MarketClue_TraceInfo>();
        }
    }

	/// <summary>跟踪记录</summary>	
	[Description("跟踪记录")]
    public partial class S_P_MarketClue_TraceInfo : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string S_P_MarketClueID { get; set; } // S_P_MarketClueID
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string IsReleased { get; set; } // IsReleased
		/// <summary>跟踪内容</summary>	
		[Description("跟踪内容")]
        public string TraceContent { get; set; } // TraceContent
		/// <summary>下一步计划</summary>	
		[Description("下一步计划")]
        public string NextStep { get; set; } // NextStep
		/// <summary>跟踪方式</summary>	
		[Description("跟踪方式")]
        public string TraceType { get; set; } // TraceType
		/// <summary>联系人</summary>	
		[Description("联系人")]
        public string Contact { get; set; } // Contact
		/// <summary>记录人</summary>	
		[Description("记录人")]
        public string CreateUser { get; set; } // CreateUser
		/// <summary>记录人名称</summary>	
		[Description("记录人名称")]
        public string CreateUserName { get; set; } // CreateUserName
		/// <summary>记录日期</summary>	
		[Description("记录日期")]
        public DateTime? CreateDate { get; set; } // CreateDate

        // Foreign keys
		[JsonIgnore]
        public virtual S_P_MarketClue S_P_MarketClue { get; set; } //  S_P_MarketClueID - FK_S_P_MarketClue_TraceInfo_S_P_MarketClue
    }

	/// <summary>分包发票管理</summary>	
	[Description("分包发票管理")]
    public partial class S_SP_Invoice : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary>发票编号</summary>	
		[Description("发票编号")]
        public string InvoiceCode { get; set; } // InvoiceCode
		/// <summary>发票类型</summary>	
		[Description("发票类型")]
        public string InvoiceType { get; set; } // InvoiceType
		/// <summary>经办人</summary>	
		[Description("经办人")]
        public string InvoiceReciever { get; set; } // InvoiceReciever
		/// <summary>经办人名称</summary>	
		[Description("经办人名称")]
        public string InvoiceRecieverName { get; set; } // InvoiceRecieverName
		/// <summary>开票日期</summary>	
		[Description("开票日期")]
        public DateTime? InvoiceDate { get; set; } // InvoiceDate
		/// <summary>经营合同ID</summary>	
		[Description("经营合同ID")]
        public string ManagerContract { get; set; } // ManagerContract
		/// <summary>经营合同</summary>	
		[Description("经营合同")]
        public string ManagerContractName { get; set; } // ManagerContractName
		/// <summary>分包合同ID</summary>	
		[Description("分包合同ID")]
        public string SupplierContract { get; set; } // SupplierContract
		/// <summary>分包合同名称</summary>	
		[Description("分包合同名称")]
        public string SupplierContractName { get; set; } // SupplierContractName
		/// <summary>开票单位</summary>	
		[Description("开票单位")]
        public string Supplier { get; set; } // Supplier
		/// <summary>开票单位名称</summary>	
		[Description("开票单位名称")]
        public string SupplierName { get; set; } // SupplierName
		/// <summary>成本单元</summary>	
		[Description("成本单元")]
        public string CostUnit { get; set; } // CostUnit
		/// <summary>成本单元名称</summary>	
		[Description("成本单元名称")]
        public string CostUnitName { get; set; } // CostUnitName
		/// <summary>发票金额（元）</summary>	
		[Description("发票金额（元）")]
        public decimal? Amount { get; set; } // Amount
		/// <summary></summary>	
		[Description("")]
        public int? BelongYear { get; set; } // BelongYear
		/// <summary></summary>	
		[Description("")]
        public int? BelongQuarter { get; set; } // BelongQuarter
		/// <summary></summary>	
		[Description("")]
        public int? BelongMonth { get; set; } // BelongMonth
		/// <summary>发票状态</summary>	
		[Description("发票状态")]
        public string State { get; set; } // State
		/// <summary>备注</summary>	
		[Description("备注")]
        public string Remark { get; set; } // Remark
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
		/// <summary>税率</summary>	
		[Description("税率")]
        public decimal? TaxRate { get; set; } // TaxRate
		/// <summary>税名</summary>	
		[Description("税名")]
        public string TaxName { get; set; } // TaxName
		/// <summary>税金</summary>	
		[Description("税金")]
        public decimal? TaxValue { get; set; } // TaxValue
		/// <summary>去税金额</summary>	
		[Description("去税金额")]
        public decimal? ClearValue { get; set; } // ClearValue

        // Foreign keys
		[JsonIgnore]
        public virtual S_SP_SupplierContract S_SP_SupplierContract { get; set; } //  SupplierContract - FK_S_SP_Invoice_S_SP_SupplierContract
    }

	/// <summary>付款登记</summary>	
	[Description("付款登记")]
    public partial class S_SP_Payment : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary>分包合同</summary>	
		[Description("分包合同")]
        public string Contract { get; set; } // Contract
		/// <summary>委外合同名称</summary>	
		[Description("委外合同名称")]
        public string ContractName { get; set; } // ContractName
		/// <summary>合作单位</summary>	
		[Description("合作单位")]
        public string Supplier { get; set; } // Supplier
		/// <summary>合作单位名称</summary>	
		[Description("合作单位名称")]
        public string SupplierName { get; set; } // SupplierName
		/// <summary>付款金额</summary>	
		[Description("付款金额")]
        public decimal? PaymentValue { get; set; } // PaymentValue
		/// <summary>付款日期</summary>	
		[Description("付款日期")]
        public DateTime? PaymentDate { get; set; } // PaymentDate
		/// <summary></summary>	
		[Description("")]
        public int? BelongYear { get; set; } // BelongYear
		/// <summary></summary>	
		[Description("")]
        public int? BelongMonth { get; set; } // BelongMonth
		/// <summary></summary>	
		[Description("")]
        public int? BelongQuarter { get; set; } // BelongQuarter
		/// <summary>经办人</summary>	
		[Description("经办人")]
        public string PaymentUser { get; set; } // PaymentUser
		/// <summary>经办人名称</summary>	
		[Description("经办人名称")]
        public string PaymentUserName { get; set; } // PaymentUserName
		/// <summary>备注</summary>	
		[Description("备注")]
        public string Remark { get; set; } // Remark
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
        public string ModifyUser { get; set; } // ModifyUser
		/// <summary></summary>	
		[Description("")]
        public string ModifyUserID { get; set; } // ModifyUserID
		/// <summary></summary>	
		[Description("")]
        public DateTime? ModifyDate { get; set; } // ModifyDate
		/// <summary>付款编号</summary>	
		[Description("付款编号")]
        public string PaymentCode { get; set; } // PaymentCode
		/// <summary></summary>	
		[Description("")]
        public string PaymentApplyID { get; set; } // PaymentApplyID
		/// <summary>主合同</summary>	
		[Description("主合同")]
        public string ManagerContract { get; set; } // ManagerContract
		/// <summary>主合同名称</summary>	
		[Description("主合同名称")]
        public string ManagerContractName { get; set; } // ManagerContractName
		/// <summary>关联发票</summary>	
		[Description("关联发票")]
        public string InvoiceRelation { get; set; } // InvoiceRelation
		/// <summary>承兑汇票支付</summary>	
		[Description("承兑汇票支付")]
        public string AcceptanceBill { get; set; } // AcceptanceBill
		/// <summary>承兑汇票金额</summary>	
		[Description("承兑汇票金额")]
        public decimal? BillValue { get; set; } // BillValue
		/// <summary>资金支付</summary>	
		[Description("资金支付")]
        public decimal? PaymentWithoutBill { get; set; } // PaymentWithoutBill
		/// <summary>成本明细</summary>	
		[Description("成本明细")]
        public string CostInfo { get; set; } // CostInfo
		/// <summary>供方银行账号</summary>	
		[Description("供方银行账号")]
        public string BankAccount { get; set; } // BankAccount
		/// <summary>供方银行账号名称</summary>	
		[Description("供方银行账号名称")]
        public string BankAccountName { get; set; } // BankAccountName
		/// <summary>供方开户银行</summary>	
		[Description("供方开户银行")]
        public string BankName { get; set; } // BankName

        // Reverse navigation
		[JsonIgnore]
        public virtual ICollection<S_SP_Payment_AcceptanceBill> S_SP_Payment_AcceptanceBill { get { onS_SP_Payment_AcceptanceBillGetting(); return _S_SP_Payment_AcceptanceBill;} }
		private ICollection<S_SP_Payment_AcceptanceBill> _S_SP_Payment_AcceptanceBill;
		partial void onS_SP_Payment_AcceptanceBillGetting();

		[JsonIgnore]
        public virtual ICollection<S_SP_Payment_CostInfo> S_SP_Payment_CostInfo { get { onS_SP_Payment_CostInfoGetting(); return _S_SP_Payment_CostInfo;} }
		private ICollection<S_SP_Payment_CostInfo> _S_SP_Payment_CostInfo;
		partial void onS_SP_Payment_CostInfoGetting();

		[JsonIgnore]
        public virtual ICollection<S_SP_Payment_InvoiceRelation> S_SP_Payment_InvoiceRelation { get { onS_SP_Payment_InvoiceRelationGetting(); return _S_SP_Payment_InvoiceRelation;} }
		private ICollection<S_SP_Payment_InvoiceRelation> _S_SP_Payment_InvoiceRelation;
		partial void onS_SP_Payment_InvoiceRelationGetting();


        // Foreign keys
		[JsonIgnore]
        public virtual S_SP_SupplierContract S_SP_SupplierContract { get; set; } //  Contract - FK_S_SP_Payment_S_SP_SupplierContract

        public S_SP_Payment()
        {
            _S_SP_Payment_AcceptanceBill = new List<S_SP_Payment_AcceptanceBill>();
            _S_SP_Payment_CostInfo = new List<S_SP_Payment_CostInfo>();
            _S_SP_Payment_InvoiceRelation = new List<S_SP_Payment_InvoiceRelation>();
        }
    }

	/// <summary>承兑汇票支付</summary>	
	[Description("承兑汇票支付")]
    public partial class S_SP_Payment_AcceptanceBill : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string S_SP_PaymentID { get; set; } // S_SP_PaymentID
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string IsReleased { get; set; } // IsReleased
		/// <summary>汇票号</summary>	
		[Description("汇票号")]
        public string SerialNumber { get; set; } // SerialNumber
		/// <summary>付款单位</summary>	
		[Description("付款单位")]
        public string CustomerName { get; set; } // CustomerName
		/// <summary>开票日期</summary>	
		[Description("开票日期")]
        public DateTime? BillDate { get; set; } // BillDate
		/// <summary>汇票金额(元)</summary>	
		[Description("汇票金额(元)")]
        public decimal? Amount { get; set; } // Amount
		/// <summary>AcceptanceBillID</summary>	
		[Description("AcceptanceBillID")]
        public string AcceptanceBillID { get; set; } // AcceptanceBillID

        // Foreign keys
		[JsonIgnore]
        public virtual S_SP_Payment S_SP_Payment { get; set; } //  S_SP_PaymentID - FK_S_SP_Payment_AcceptanceBill_S_SP_Payment
    }

	/// <summary>成本明细</summary>	
	[Description("成本明细")]
    public partial class S_SP_Payment_CostInfo : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string S_SP_PaymentID { get; set; } // S_SP_PaymentID
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
		/// <summary>合同金额（元）</summary>	
		[Description("合同金额（元）")]
        public decimal? ContractValue { get; set; } // ContractValue
		/// <summary>付款金额（元）</summary>	
		[Description("付款金额（元）")]
        public decimal? CostValue { get; set; } // CostValue
		/// <summary>CostUnitID</summary>	
		[Description("CostUnitID")]
        public string CostUnitID { get; set; } // CostUnitID

        // Foreign keys
		[JsonIgnore]
        public virtual S_SP_Payment S_SP_Payment { get; set; } //  S_SP_PaymentID - FK_S_SP_Payment_CostInfo_S_SP_Payment
    }

	/// <summary>关联发票</summary>	
	[Description("关联发票")]
    public partial class S_SP_Payment_InvoiceRelation : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string S_SP_PaymentID { get; set; } // S_SP_PaymentID
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string IsReleased { get; set; } // IsReleased
		/// <summary>发票编号</summary>	
		[Description("发票编号")]
        public string InvoiceNo { get; set; } // InvoiceNo
		/// <summary>收票日期</summary>	
		[Description("收票日期")]
        public string InvoiceDate { get; set; } // InvoiceDate
		/// <summary>开票单位</summary>	
		[Description("开票单位")]
        public string InvoiceUnit { get; set; } // InvoiceUnit
		/// <summary>本次对应金额（元）</summary>	
		[Description("本次对应金额（元）")]
        public decimal? RelationValue { get; set; } // RelationValue
		/// <summary>可对应金额（元）</summary>	
		[Description("可对应金额（元）")]
        public decimal? RemainInvoiceValue { get; set; } // RemainInvoiceValue
		/// <summary>InvoiceID</summary>	
		[Description("InvoiceID")]
        public string InvoiceID { get; set; } // InvoiceID
		/// <summary>已对应金额（元）</summary>	
		[Description("已对应金额（元）")]
        public decimal? InvoiceRelationValue { get; set; } // InvoiceRelationValue

        // Foreign keys
		[JsonIgnore]
        public virtual S_SP_Payment S_SP_Payment { get; set; } //  S_SP_PaymentID - FK_S_SP_Payment_InvoiceRelation_S_SP_Payment
    }

	/// <summary></summary>	
	[Description("")]
    public partial class S_SP_PaymentPlan : Formula.BaseModel
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
        public string SupplierContract { get; set; } // SupplierContract
		/// <summary></summary>	
		[Description("")]
        public string SupplierContractName { get; set; } // SupplierContractName
		/// <summary></summary>	
		[Description("")]
        public string Supplier { get; set; } // Supplier
		/// <summary></summary>	
		[Description("")]
        public string SupplierName { get; set; } // SupplierName
		/// <summary></summary>	
		[Description("")]
        public DateTime? PlanDate { get; set; } // PlanDate
		/// <summary></summary>	
		[Description("")]
        public int? BelongYear { get; set; } // BelongYear
		/// <summary></summary>	
		[Description("")]
        public int? BelongMonth { get; set; } // BelongMonth
		/// <summary></summary>	
		[Description("")]
        public int? BelongQuarter { get; set; } // BelongQuarter
		/// <summary></summary>	
		[Description("")]
        public decimal? PlanPaymentValue { get; set; } // PlanPaymentValue
		/// <summary></summary>	
		[Description("")]
        public decimal? SummaryPaymentValue { get; set; } // SummaryPaymentValue
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
        public string ModifyUser { get; set; } // ModifyUser
		/// <summary></summary>	
		[Description("")]
        public string ModifyUserID { get; set; } // ModifyUserID
		/// <summary></summary>	
		[Description("")]
        public DateTime? ModifyDate { get; set; } // ModifyDate
    }

	/// <summary>合作单位管理</summary>	
	[Description("合作单位管理")]
    public partial class S_SP_Supplier : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary>合作单位名称</summary>	
		[Description("合作单位名称")]
        public string Name { get; set; } // Name
		/// <summary>合作单位编号</summary>	
		[Description("合作单位编号")]
        public string Code { get; set; } // Code
		/// <summary>状态</summary>	
		[Description("状态")]
        public string State { get; set; } // State
		/// <summary>合作单位简称</summary>	
		[Description("合作单位简称")]
        public string SimplyName { get; set; } // SimplyName
		/// <summary>邮编</summary>	
		[Description("邮编")]
        public string ZipCode { get; set; } // ZipCode
		/// <summary>电话</summary>	
		[Description("电话")]
        public string Telephone { get; set; } // Telephone
		/// <summary>传真</summary>	
		[Description("传真")]
        public string Fax { get; set; } // Fax
		/// <summary>电子邮件</summary>	
		[Description("电子邮件")]
        public string Email { get; set; } // Email
		/// <summary>主页</summary>	
		[Description("主页")]
        public string HomePage { get; set; } // HomePage
		/// <summary>所在国家</summary>	
		[Description("所在国家")]
        public string Country { get; set; } // Country
		/// <summary>省份</summary>	
		[Description("省份")]
        public string Province { get; set; } // Province
		/// <summary>城市</summary>	
		[Description("城市")]
        public string City { get; set; } // City
		/// <summary>地区</summary>	
		[Description("地区")]
        public string Area { get; set; } // Area
		/// <summary>合作单位级别</summary>	
		[Description("合作单位级别")]
        public string SupplierLevel { get; set; } // SupplierLevel
		/// <summary>关系建立日期</summary>	
		[Description("关系建立日期")]
        public DateTime? FirstContactDate { get; set; } // FirstContactDate
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
		/// <summary>省份枚举</summary>	
		[Description("省份枚举")]
        public string ProvinceEnum { get; set; } // ProvinceEnum
		/// <summary></summary>	
		[Description("")]
        public DateTime? SyncCloudTime { get; set; } // SyncCloudTime
		/// <summary>主要联系人</summary>	
		[Description("主要联系人")]
        public string LinkUser { get; set; } // LinkUser
		/// <summary>联系电话</summary>	
		[Description("联系电话")]
        public string LinkTelphone { get; set; } // LinkTelphone
		/// <summary>银行信息</summary>	
		[Description("银行信息")]
        public string BankInfo { get; set; } // BankInfo
		/// <summary>联系地址</summary>	
		[Description("联系地址")]
        public string ContactAddress { get; set; } // ContactAddress

        // Reverse navigation
		[JsonIgnore]
        public virtual ICollection<S_SP_Supplier_BankInfo> S_SP_Supplier_BankInfo { get { onS_SP_Supplier_BankInfoGetting(); return _S_SP_Supplier_BankInfo;} }
		private ICollection<S_SP_Supplier_BankInfo> _S_SP_Supplier_BankInfo;
		partial void onS_SP_Supplier_BankInfoGetting();

		[JsonIgnore]
        public virtual ICollection<S_SP_Supplier_ContactUserInfo> S_SP_Supplier_ContactUserInfo { get { onS_SP_Supplier_ContactUserInfoGetting(); return _S_SP_Supplier_ContactUserInfo;} }
		private ICollection<S_SP_Supplier_ContactUserInfo> _S_SP_Supplier_ContactUserInfo;
		partial void onS_SP_Supplier_ContactUserInfoGetting();

		[JsonIgnore]
        public virtual ICollection<S_SP_Supplier_CoopProjectInfo> S_SP_Supplier_CoopProjectInfo { get { onS_SP_Supplier_CoopProjectInfoGetting(); return _S_SP_Supplier_CoopProjectInfo;} }
		private ICollection<S_SP_Supplier_CoopProjectInfo> _S_SP_Supplier_CoopProjectInfo;
		partial void onS_SP_Supplier_CoopProjectInfoGetting();

		[JsonIgnore]
        public virtual ICollection<S_SP_Supplier_CredentialInfo> S_SP_Supplier_CredentialInfo { get { onS_SP_Supplier_CredentialInfoGetting(); return _S_SP_Supplier_CredentialInfo;} }
		private ICollection<S_SP_Supplier_CredentialInfo> _S_SP_Supplier_CredentialInfo;
		partial void onS_SP_Supplier_CredentialInfoGetting();


        public S_SP_Supplier()
        {
            _S_SP_Supplier_BankInfo = new List<S_SP_Supplier_BankInfo>();
            _S_SP_Supplier_ContactUserInfo = new List<S_SP_Supplier_ContactUserInfo>();
            _S_SP_Supplier_CoopProjectInfo = new List<S_SP_Supplier_CoopProjectInfo>();
            _S_SP_Supplier_CredentialInfo = new List<S_SP_Supplier_CredentialInfo>();
        }
    }

	/// <summary>银行信息</summary>	
	[Description("银行信息")]
    public partial class S_SP_Supplier_BankInfo : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string S_SP_SupplierID { get; set; } // S_SP_SupplierID
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string IsReleased { get; set; } // IsReleased
		/// <summary>开户银行</summary>	
		[Description("开户银行")]
        public string Bank { get; set; } // Bank
		/// <summary>银行账号</summary>	
		[Description("银行账号")]
        public string BankAccount { get; set; } // BankAccount
		/// <summary>账号名称</summary>	
		[Description("账号名称")]
        public string BankAccountName { get; set; } // BankAccountName
		/// <summary>开户行地址</summary>	
		[Description("开户行地址")]
        public string BankAddress { get; set; } // BankAddress
		/// <summary>开户银行名称</summary>	
		[Description("开户银行名称")]
        public string BankName { get; set; } // BankName

        // Foreign keys
		[JsonIgnore]
        public virtual S_SP_Supplier S_SP_Supplier { get; set; } //  S_SP_SupplierID - FK_S_SP_Supplier_BankInfo_S_SP_Supplier
    }

	/// <summary>联系人信息</summary>	
	[Description("联系人信息")]
    public partial class S_SP_Supplier_ContactUserInfo : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string S_SP_SupplierID { get; set; } // S_SP_SupplierID
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string IsReleased { get; set; } // IsReleased
		/// <summary>联系人</summary>	
		[Description("联系人")]
        public string ContactUser { get; set; } // ContactUser
		/// <summary>电话</summary>	
		[Description("电话")]
        public string Phone { get; set; } // Phone

        // Foreign keys
		[JsonIgnore]
        public virtual S_SP_Supplier S_SP_Supplier { get; set; } //  S_SP_SupplierID - FK_S_SP_Supplier_ContactUserInfo_S_SP_Supplier
    }

	/// <summary>合作项目信息</summary>	
	[Description("合作项目信息")]
    public partial class S_SP_Supplier_CoopProjectInfo : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string S_SP_SupplierID { get; set; } // S_SP_SupplierID
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string IsReleased { get; set; } // IsReleased
		/// <summary>项目名称</summary>	
		[Description("项目名称")]
        public string Project { get; set; } // Project
		/// <summary>项目名称名称</summary>	
		[Description("项目名称名称")]
        public string ProjectName { get; set; } // ProjectName
		/// <summary>项目负责人</summary>	
		[Description("项目负责人")]
        public string ChargerUser { get; set; } // ChargerUser
		/// <summary>项目负责人名称</summary>	
		[Description("项目负责人名称")]
        public string ChargerUserName { get; set; } // ChargerUserName
		/// <summary>设计部门</summary>	
		[Description("设计部门")]
        public string DesignDept { get; set; } // DesignDept
		/// <summary>设计部门名称</summary>	
		[Description("设计部门名称")]
        public string DesignDeptName { get; set; } // DesignDeptName
		/// <summary>分包设计产品名称</summary>	
		[Description("分包设计产品名称")]
        public string ProductName { get; set; } // ProductName
		/// <summary>评定</summary>	
		[Description("评定")]
        public decimal? Evaluate { get; set; } // Evaluate

        // Foreign keys
		[JsonIgnore]
        public virtual S_SP_Supplier S_SP_Supplier { get; set; } //  S_SP_SupplierID - FK_S_SP_Supplier_CoopProjectInfo_S_SP_Supplier
    }

	/// <summary>资质信息</summary>	
	[Description("资质信息")]
    public partial class S_SP_Supplier_CredentialInfo : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string S_SP_SupplierID { get; set; } // S_SP_SupplierID
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string IsReleased { get; set; } // IsReleased
		/// <summary>证书编号</summary>	
		[Description("证书编号")]
        public string CertificateCode { get; set; } // CertificateCode
		/// <summary>证书名称</summary>	
		[Description("证书名称")]
        public string CertificateName { get; set; } // CertificateName
		/// <summary>有效期限</summary>	
		[Description("有效期限")]
        public DateTime? ExpiryDate { get; set; } // ExpiryDate
		/// <summary>业务范围</summary>	
		[Description("业务范围")]
        public string Scope { get; set; } // Scope
		/// <summary>主要业绩</summary>	
		[Description("主要业绩")]
        public string Achievement { get; set; } // Achievement
		/// <summary>附件</summary>	
		[Description("附件")]
        public string Attachment { get; set; } // Attachment
		/// <summary>附件名称</summary>	
		[Description("附件名称")]
        public string AttachmentName { get; set; } // AttachmentName

        // Foreign keys
		[JsonIgnore]
        public virtual S_SP_Supplier S_SP_Supplier { get; set; } //  S_SP_SupplierID - FK_S_SP_Supplier_CredentialInfo_S_SP_Supplier
    }

	/// <summary>委外合同管理</summary>	
	[Description("委外合同管理")]
    public partial class S_SP_SupplierContract : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary>合同名称</summary>	
		[Description("合同名称")]
        public string Name { get; set; } // Name
		/// <summary>编号</summary>	
		[Description("编号")]
        public string Code { get; set; } // Code
		/// <summary></summary>	
		[Description("")]
        public int? BelongYear { get; set; } // BelongYear
		/// <summary></summary>	
		[Description("")]
        public int? BelongMonth { get; set; } // BelongMonth
		/// <summary></summary>	
		[Description("")]
        public int? BelongQuarter { get; set; } // BelongQuarter
		/// <summary>供应商</summary>	
		[Description("供应商")]
        public string Supplier { get; set; } // Supplier
		/// <summary>供应商名称</summary>	
		[Description("供应商名称")]
        public string SupplierName { get; set; } // SupplierName
		/// <summary>人民币结算金额（元）</summary>	
		[Description("人民币结算金额（元）")]
        public decimal? ContractValue { get; set; } // ContractValue
		/// <summary>签约日期</summary>	
		[Description("签约日期")]
        public DateTime? SignDate { get; set; } // SignDate
		/// <summary>合同状态</summary>	
		[Description("合同状态")]
        public string State { get; set; } // State
		/// <summary>关联经营合同</summary>	
		[Description("关联经营合同")]
        public string ManagerContract { get; set; } // ManagerContract
		/// <summary>关联经营合同名称</summary>	
		[Description("关联经营合同名称")]
        public string ManagerContractName { get; set; } // ManagerContractName
		/// <summary>经营合同编号</summary>	
		[Description("经营合同编号")]
        public string ManagerContractCode { get; set; } // ManagerContractCode
		/// <summary>分包部门</summary>	
		[Description("分包部门")]
        public string SupplierDept { get; set; } // SupplierDept
		/// <summary>分包部门名称</summary>	
		[Description("分包部门名称")]
        public string SupplierDeptName { get; set; } // SupplierDeptName
		/// <summary></summary>	
		[Description("")]
        public decimal? SumPaymentValue { get; set; } // SumPaymentValue
		/// <summary></summary>	
		[Description("")]
        public decimal? SumSPInvoiceValue { get; set; } // SumSPInvoiceValue
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
		/// <summary>项目名称</summary>	
		[Description("项目名称")]
        public string ProjectInfo { get; set; } // ProjectInfo
		/// <summary>项目名称名称</summary>	
		[Description("项目名称名称")]
        public string ProjectInfoName { get; set; } // ProjectInfoName
		/// <summary>项目编号</summary>	
		[Description("项目编号")]
        public string ProjectInfoCode { get; set; } // ProjectInfoCode
		/// <summary>合同文本</summary>	
		[Description("合同文本")]
        public string ContractAttachment { get; set; } // ContractAttachment
		/// <summary>相关附件</summary>	
		[Description("相关附件")]
        public string Attachments { get; set; } // Attachments
		/// <summary>备注说明</summary>	
		[Description("备注说明")]
        public string Remark { get; set; } // Remark
		/// <summary>合同性质</summary>	
		[Description("合同性质")]
        public string ContractClass { get; set; } // ContractClass
		/// <summary>关联主合同</summary>	
		[Description("关联主合同")]
        public string MainContract { get; set; } // MainContract
		/// <summary>关联主合同名称</summary>	
		[Description("关联主合同名称")]
        public string MainContractName { get; set; } // MainContractName
		/// <summary>主合同编号</summary>	
		[Description("主合同编号")]
        public string MainContractCode { get; set; } // MainContractCode
		/// <summary>折合人民币（元）</summary>	
		[Description("折合人民币（元）")]
        public decimal? ThisContractValue { get; set; } // ThisContractValue
		/// <summary>成本单元</summary>	
		[Description("成本单元")]
        public string CostUnit { get; set; } // CostUnit
		/// <summary>关联成本单元</summary>	
		[Description("关联成本单元")]
        public string ContractSplit { get; set; } // ContractSplit
		/// <summary>税名</summary>	
		[Description("税名")]
        public string TaxName { get; set; } // TaxName
		/// <summary>税率</summary>	
		[Description("税率")]
        public decimal? TaxRate { get; set; } // TaxRate
		/// <summary>编号</summary>	
		[Description("编号")]
        public string SerialNumber { get; set; } // SerialNumber
		/// <summary>合同金额</summary>	
		[Description("合同金额")]
        public decimal? ContractAmount { get; set; } // ContractAmount
		/// <summary>汇率</summary>	
		[Description("汇率")]
        public decimal? ExchangeRate { get; set; } // ExchangeRate
		/// <summary>币种</summary>	
		[Description("币种")]
        public string Currency { get; set; } // Currency
		/// <summary>折合人民币大写</summary>	
		[Description("折合人民币大写")]
        public string ContractValueDX { get; set; } // ContractValueDX
		/// <summary>税金</summary>	
		[Description("税金")]
        public decimal? TaxValue { get; set; } // TaxValue
		/// <summary>不含税金额</summary>	
		[Description("不含税金额")]
        public decimal? ClearContractValue { get; set; } // ClearContractValue

        // Reverse navigation
		[JsonIgnore]
        public virtual ICollection<S_SP_Invoice> S_SP_Invoice { get { onS_SP_InvoiceGetting(); return _S_SP_Invoice;} }
		private ICollection<S_SP_Invoice> _S_SP_Invoice;
		partial void onS_SP_InvoiceGetting();

		[JsonIgnore]
        public virtual ICollection<S_SP_Payment> S_SP_Payment { get { onS_SP_PaymentGetting(); return _S_SP_Payment;} }
		private ICollection<S_SP_Payment> _S_SP_Payment;
		partial void onS_SP_PaymentGetting();

		[JsonIgnore]
        public virtual ICollection<S_SP_SupplierContract_ContractSplit> S_SP_SupplierContract_ContractSplit { get { onS_SP_SupplierContract_ContractSplitGetting(); return _S_SP_SupplierContract_ContractSplit;} }
		private ICollection<S_SP_SupplierContract_ContractSplit> _S_SP_SupplierContract_ContractSplit;
		partial void onS_SP_SupplierContract_ContractSplitGetting();


        public S_SP_SupplierContract()
        {
            _S_SP_Invoice = new List<S_SP_Invoice>();
            _S_SP_Payment = new List<S_SP_Payment>();
            _S_SP_SupplierContract_ContractSplit = new List<S_SP_SupplierContract_ContractSplit>();
        }
    }

	/// <summary>关联成本单元</summary>	
	[Description("关联成本单元")]
    public partial class S_SP_SupplierContract_ContractSplit : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string S_SP_SupplierContractID { get; set; } // S_SP_SupplierContractID
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string IsReleased { get; set; } // IsReleased
		/// <summary>CostUnitID</summary>	
		[Description("CostUnitID")]
        public string CostUnitID { get; set; } // CostUnitID
		/// <summary>名称</summary>	
		[Description("名称")]
        public string Name { get; set; } // Name
		/// <summary>编号</summary>	
		[Description("编号")]
        public string Code { get; set; } // Code
		/// <summary>负责人</summary>	
		[Description("负责人")]
        public string ChargerUser { get; set; } // ChargerUser
		/// <summary>负责人名称</summary>	
		[Description("负责人名称")]
        public string ChargerUserName { get; set; } // ChargerUserName
		/// <summary>责任部门</summary>	
		[Description("责任部门")]
        public string ChargerDept { get; set; } // ChargerDept
		/// <summary>责任部门名称</summary>	
		[Description("责任部门名称")]
        public string ChargerDeptName { get; set; } // ChargerDeptName
		/// <summary>委外金额（元）</summary>	
		[Description("委外金额（元）")]
        public decimal? SplitValue { get; set; } // SplitValue
		/// <summary>最低委外金额（元）</summary>	
		[Description("最低委外金额（元）")]
        public decimal? MinContractValue { get; set; } // MinContractValue

        // Foreign keys
		[JsonIgnore]
        public virtual S_SP_SupplierContract S_SP_SupplierContract { get; set; } //  S_SP_SupplierContractID - FK_S_SP_SupplierContract_CostUnit_S_SP_SupplierContract
    }

	/// <summary>分包合同补充协议</summary>	
	[Description("分包合同补充协议")]
    public partial class S_SP_SupplierContract_Supplementary : Formula.BaseModel
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
		/// <summary>MainContract</summary>	
		[Description("MainContract")]
        public string MainContract { get; set; } // MainContract
		/// <summary>名称</summary>	
		[Description("名称")]
        public string Name { get; set; } // Name
		/// <summary>编号</summary>	
		[Description("编号")]
        public string Code { get; set; } // Code
		/// <summary>金额（元）</summary>	
		[Description("金额（元）")]
        public decimal? ContractValue { get; set; } // ContractValue
		/// <summary>生效日期</summary>	
		[Description("生效日期")]
        public DateTime? SignDate { get; set; } // SignDate
		/// <summary>经办人</summary>	
		[Description("经办人")]
        public string OperateUser { get; set; } // OperateUser
		/// <summary>经办人名称</summary>	
		[Description("经办人名称")]
        public string OperateUserName { get; set; } // OperateUserName
		/// <summary>补充协议文本</summary>	
		[Description("补充协议文本")]
        public string ContractAttachment { get; set; } // ContractAttachment
		/// <summary>相关附件</summary>	
		[Description("相关附件")]
        public string Attachments { get; set; } // Attachments
		/// <summary>备注说明</summary>	
		[Description("备注说明")]
        public string Remark { get; set; } // Remark
		/// <summary>BelongYear</summary>	
		[Description("BelongYear")]
        public int? BelongYear { get; set; } // BelongYear
		/// <summary>BelongMonth</summary>	
		[Description("BelongMonth")]
        public int? BelongMonth { get; set; } // BelongMonth
		/// <summary>BelongQuarter</summary>	
		[Description("BelongQuarter")]
        public int? BelongQuarter { get; set; } // BelongQuarter
    }

	/// <summary>投标申请</summary>	
	[Description("投标申请")]
    public partial class T_B_BidApply : Formula.BaseModel
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
        public string Project { get; set; } // Project
		/// <summary>项目名称</summary>	
		[Description("项目名称")]
        public string ProjectName { get; set; } // ProjectName
		/// <summary>投标编号</summary>	
		[Description("投标编号")]
        public string BidCode { get; set; } // BidCode
		/// <summary>投标阶段</summary>	
		[Description("投标阶段")]
        public string BidPhase { get; set; } // BidPhase
		/// <summary>所属工程</summary>	
		[Description("所属工程")]
        public string EngineeringInfo { get; set; } // EngineeringInfo
		/// <summary>所属工程名称</summary>	
		[Description("所属工程名称")]
        public string EngineeringInfoName { get; set; } // EngineeringInfoName
		/// <summary>投标下达日期</summary>	
		[Description("投标下达日期")]
        public DateTime? BidIssuedDate { get; set; } // BidIssuedDate
		/// <summary>答疑截止时间</summary>	
		[Description("答疑截止时间")]
        public DateTime? CompetencyDate { get; set; } // CompetencyDate
		/// <summary>招标编号</summary>	
		[Description("招标编号")]
        public string TenderCode { get; set; } // TenderCode
		/// <summary>招标单位</summary>	
		[Description("招标单位")]
        public string TenderOrgan { get; set; } // TenderOrgan
		/// <summary>业务类别</summary>	
		[Description("业务类别")]
        public string BusinessClass { get; set; } // BusinessClass
		/// <summary>业主单位</summary>	
		[Description("业主单位")]
        public string BusinessOwner { get; set; } // BusinessOwner
		/// <summary>项目负责人</summary>	
		[Description("项目负责人")]
        public string ProjectManager { get; set; } // ProjectManager
		/// <summary>项目负责人名称</summary>	
		[Description("项目负责人名称")]
        public string ProjectManagerName { get; set; } // ProjectManagerName
		/// <summary>承担部门</summary>	
		[Description("承担部门")]
        public string DeptInfo { get; set; } // DeptInfo
		/// <summary>承担部门名称</summary>	
		[Description("承担部门名称")]
        public string DeptInfoName { get; set; } // DeptInfoName
		/// <summary>交标时间</summary>	
		[Description("交标时间")]
        public DateTime? CompleteBidDate { get; set; } // CompleteBidDate
		/// <summary>邮政编码</summary>	
		[Description("邮政编码")]
        public string PostCode { get; set; } // PostCode
		/// <summary>详细地址</summary>	
		[Description("详细地址")]
        public string Address { get; set; } // Address
		/// <summary>备注</summary>	
		[Description("备注")]
        public string Remark { get; set; } // Remark
		/// <summary>招标文件</summary>	
		[Description("招标文件")]
        public string InviteBidFile { get; set; } // InviteBidFile
		/// <summary>招标过程澄清文件</summary>	
		[Description("招标过程澄清文件")]
        public string InviteBidClearFile { get; set; } // InviteBidClearFile
		/// <summary>招标其他文件</summary>	
		[Description("招标其他文件")]
        public string InviteBidOtherFile { get; set; } // InviteBidOtherFile
		/// <summary>业主单位名称</summary>	
		[Description("业主单位名称")]
        public string BusinessOwnerName { get; set; } // BusinessOwnerName
		/// <summary>是否同意投标</summary>	
		[Description("是否同意投标")]
        public string Agree { get; set; } // Agree
		/// <summary>管理者意见</summary>	
		[Description("管理者意见")]
        public string FinalOpinion { get; set; } // FinalOpinion
    }

	/// <summary>投标过程评审</summary>	
	[Description("投标过程评审")]
    public partial class T_B_BidProcessApply : Formula.BaseModel
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
		/// <summary>投标编号</summary>	
		[Description("投标编号")]
        public string BidCode { get; set; } // BidCode
		/// <summary>投标阶段</summary>	
		[Description("投标阶段")]
        public string BidPhase { get; set; } // BidPhase
		/// <summary>投标下达日期</summary>	
		[Description("投标下达日期")]
        public DateTime? BidIssuedDate { get; set; } // BidIssuedDate
		/// <summary>备注</summary>	
		[Description("备注")]
        public string Remark { get; set; } // Remark
		/// <summary>投标文件</summary>	
		[Description("投标文件")]
        public string BidFile { get; set; } // BidFile
		/// <summary>投标过程澄清文件</summary>	
		[Description("投标过程澄清文件")]
        public string BidClearFile { get; set; } // BidClearFile
		/// <summary>投标其他文件</summary>	
		[Description("投标其他文件")]
        public string BidOtherFile { get; set; } // BidOtherFile
		/// <summary>BidID</summary>	
		[Description("BidID")]
        public string BidID { get; set; } // BidID
		/// <summary>Project</summary>	
		[Description("Project")]
        public string Project { get; set; } // Project
    }

	/// <summary>保证金申请</summary>	
	[Description("保证金申请")]
    public partial class T_B_BondApply : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary>项目id</summary>	
		[Description("项目id")]
        public string Project { get; set; } // Project
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
		/// <summary>申请编号</summary>	
		[Description("申请编号")]
        public string SerialNumber { get; set; } // SerialNumber
		/// <summary>项目名称</summary>	
		[Description("项目名称")]
        public string ProjectName { get; set; } // ProjectName
		/// <summary>申请日期</summary>	
		[Description("申请日期")]
        public DateTime? ApplyDate { get; set; } // ApplyDate
		/// <summary>项目编号</summary>	
		[Description("项目编号")]
        public string ProjectCode { get; set; } // ProjectCode
		/// <summary>项目负责人</summary>	
		[Description("项目负责人")]
        public string ProjectMangerName { get; set; } // ProjectMangerName
		/// <summary>金额</summary>	
		[Description("金额")]
        public decimal? Amount { get; set; } // Amount
		/// <summary>截止日期</summary>	
		[Description("截止日期")]
        public DateTime? ExpiryDate { get; set; } // ExpiryDate
		/// <summary>转入单位名称</summary>	
		[Description("转入单位名称")]
        public string TransferUnit { get; set; } // TransferUnit
		/// <summary>开户行名称</summary>	
		[Description("开户行名称")]
        public string BankName { get; set; } // BankName
		/// <summary>账号</summary>	
		[Description("账号")]
        public string Account { get; set; } // Account
		/// <summary>转账附言备注</summary>	
		[Description("转账附言备注")]
        public string Remark { get; set; } // Remark
		/// <summary>申请人</summary>	
		[Description("申请人")]
        public string Applier { get; set; } // Applier
		/// <summary>申请人名称</summary>	
		[Description("申请人名称")]
        public string ApplierName { get; set; } // ApplierName
		/// <summary>申请部门</summary>	
		[Description("申请部门")]
        public string DepartInfo { get; set; } // DepartInfo
		/// <summary>申请部门名称</summary>	
		[Description("申请部门名称")]
        public string DepartInfoName { get; set; } // DepartInfoName
		/// <summary>招标文件</summary>	
		[Description("招标文件")]
        public string Attachment { get; set; } // Attachment
		/// <summary>经营计划部意见</summary>	
		[Description("经营计划部意见")]
        public string BusinessPlanningDepartOpinion { get; set; } // BusinessPlanningDepartOpinion
		/// <summary>财务助理意见</summary>	
		[Description("财务助理意见")]
        public string FinanceAssistantOpinion { get; set; } // FinanceAssistantOpinion
		/// <summary>财务部主任意见</summary>	
		[Description("财务部主任意见")]
        public string FinanceDepartDirectorOpinion { get; set; } // FinanceDepartDirectorOpinion
		/// <summary>院长意见</summary>	
		[Description("院长意见")]
        public string PresidentOpinion { get; set; } // PresidentOpinion
		/// <summary>出纳意见</summary>	
		[Description("出纳意见")]
        public string TellerOpinion { get; set; } // TellerOpinion
		/// <summary>项目负责人id</summary>	
		[Description("项目负责人id")]
        public string ProjectManager { get; set; } // ProjectManager
		/// <summary>项目负责人</summary>	
		[Description("项目负责人")]
        public string ProjectManagerName { get; set; } // ProjectManagerName
		/// <summary>保证金类型</summary>	
		[Description("保证金类型")]
        public string BondType { get; set; } // BondType
    }

	/// <summary>商机线索</summary>	
	[Description("商机线索")]
    public partial class T_BusinessLeads : Formula.BaseModel
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
		/// <summary>线索名称</summary>	
		[Description("线索名称")]
        public string LeadsName { get; set; } // LeadsName
		/// <summary>线索所属单位</summary>	
		[Description("线索所属单位")]
        public string LeadsOwner { get; set; } // LeadsOwner
		/// <summary>线索来源</summary>	
		[Description("线索来源")]
        public string LeadsResource { get; set; } // LeadsResource
		/// <summary>线索类型</summary>	
		[Description("线索类型")]
        public string LeadsType { get; set; } // LeadsType
		/// <summary>所属行业</summary>	
		[Description("所属行业")]
        public string Industry { get; set; } // Industry
		/// <summary>业务类型</summary>	
		[Description("业务类型")]
        public string ProjectClass { get; set; } // ProjectClass
		/// <summary>设计阶段</summary>	
		[Description("设计阶段")]
        public string Phase { get; set; } // Phase
		/// <summary>项目等级</summary>	
		[Description("项目等级")]
        public string ProjectLevel { get; set; } // ProjectLevel
		/// <summary>所在国家</summary>	
		[Description("所在国家")]
        public string Country { get; set; } // Country
		/// <summary>所在省份</summary>	
		[Description("所在省份")]
        public string Province { get; set; } // Province
		/// <summary>所在城市</summary>	
		[Description("所在城市")]
        public string City { get; set; } // City

        // Reverse navigation
		[JsonIgnore]
        public virtual ICollection<T_BusinessLeads_Linkman> T_BusinessLeads_Linkman { get { onT_BusinessLeads_LinkmanGetting(); return _T_BusinessLeads_Linkman;} }
		private ICollection<T_BusinessLeads_Linkman> _T_BusinessLeads_Linkman;
		partial void onT_BusinessLeads_LinkmanGetting();

		[JsonIgnore]
        public virtual ICollection<T_BusinessLeads_Records> T_BusinessLeads_Records { get { onT_BusinessLeads_RecordsGetting(); return _T_BusinessLeads_Records;} }
		private ICollection<T_BusinessLeads_Records> _T_BusinessLeads_Records;
		partial void onT_BusinessLeads_RecordsGetting();


        public T_BusinessLeads()
        {
            _T_BusinessLeads_Linkman = new List<T_BusinessLeads_Linkman>();
            _T_BusinessLeads_Records = new List<T_BusinessLeads_Records>();
        }
    }

	/// <summary>线索联系人</summary>	
	[Description("线索联系人")]
    public partial class T_BusinessLeads_Linkman : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string T_BusinessLeadsID { get; set; } // T_BusinessLeadsID
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string IsReleased { get; set; } // IsReleased
		/// <summary>联系人姓名</summary>	
		[Description("联系人姓名")]
        public string LinkmanName { get; set; } // LinkmanName
		/// <summary>职务</summary>	
		[Description("职务")]
        public string Position { get; set; } // Position
		/// <summary>办公电话</summary>	
		[Description("办公电话")]
        public string OfficeTelephone { get; set; } // OfficeTelephone
		/// <summary>负责事务</summary>	
		[Description("负责事务")]
        public string ResponsiblePerson { get; set; } // ResponsiblePerson
		/// <summary>移动电话</summary>	
		[Description("移动电话")]
        public string MobilePhone { get; set; } // MobilePhone
		/// <summary>邮箱</summary>	
		[Description("邮箱")]
        public string Email { get; set; } // Email
		/// <summary>其它备注</summary>	
		[Description("其它备注")]
        public string Remark { get; set; } // Remark

        // Foreign keys
		[JsonIgnore]
        public virtual T_BusinessLeads T_BusinessLeads { get; set; } //  T_BusinessLeadsID - FK_T_BusinessLeads_Linkman_T_BusinessLeads
    }

	/// <summary>线索跟踪记录</summary>	
	[Description("线索跟踪记录")]
    public partial class T_BusinessLeads_Records : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string T_BusinessLeadsID { get; set; } // T_BusinessLeadsID
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string IsReleased { get; set; } // IsReleased
		/// <summary>追踪内容</summary>	
		[Description("追踪内容")]
        public string TrackDescription { get; set; } // TrackDescription
		/// <summary>下一步计划</summary>	
		[Description("下一步计划")]
        public string NextSetpPlan { get; set; } // NextSetpPlan
		/// <summary>追踪类型</summary>	
		[Description("追踪类型")]
        public string LinkType { get; set; } // LinkType
		/// <summary>联系人</summary>	
		[Description("联系人")]
        public string LinkerName { get; set; } // LinkerName
		/// <summary>记录人</summary>	
		[Description("记录人")]
        public string Register { get; set; } // Register
		/// <summary>记录时间</summary>	
		[Description("记录时间")]
        public DateTime? RegisterDate { get; set; } // RegisterDate

        // Foreign keys
		[JsonIgnore]
        public virtual T_BusinessLeads T_BusinessLeads { get; set; } //  T_BusinessLeadsID - FK_T_BusinessLeads_Records_T_BusinessLeads
    }

	/// <summary>合同变更</summary>	
	[Description("合同变更")]
    public partial class T_C_ContractChange : Formula.BaseModel
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
		/// <summary>合同名称</summary>	
		[Description("合同名称")]
        public string Name { get; set; } // Name
		/// <summary>甲方</summary>	
		[Description("甲方")]
        public string PartyA { get; set; } // PartyA
		/// <summary>甲方名称</summary>	
		[Description("甲方名称")]
        public string PartyAName { get; set; } // PartyAName
		/// <summary>乙方</summary>	
		[Description("乙方")]
        public string PartyB { get; set; } // PartyB
		/// <summary>乙方名称</summary>	
		[Description("乙方名称")]
        public string PartyBName { get; set; } // PartyBName
		/// <summary>丙方</summary>	
		[Description("丙方")]
        public string PartyC { get; set; } // PartyC
		/// <summary>合同编号</summary>	
		[Description("合同编号")]
        public string SerialNumber { get; set; } // SerialNumber
		/// <summary>合同类型</summary>	
		[Description("合同类型")]
        public string ContractType { get; set; } // ContractType
		/// <summary>合同性质</summary>	
		[Description("合同性质")]
        public string ContractClass { get; set; } // ContractClass
		/// <summary>签约日期</summary>	
		[Description("签约日期")]
        public DateTime? SignDate { get; set; } // SignDate
		/// <summary>签约地点</summary>	
		[Description("签约地点")]
        public string SignAddress { get; set; } // SignAddress
		/// <summary>合同状态</summary>	
		[Description("合同状态")]
        public string ContractState { get; set; } // ContractState
		/// <summary>合同起始日期</summary>	
		[Description("合同起始日期")]
        public DateTime? StartDate { get; set; } // StartDate
		/// <summary>合同截止日期</summary>	
		[Description("合同截止日期")]
        public DateTime? EndDate { get; set; } // EndDate
		/// <summary>合同来源</summary>	
		[Description("合同来源")]
        public string ContractSource { get; set; } // ContractSource
		/// <summary>合同金额</summary>	
		[Description("合同金额")]
        public decimal? ContractValue { get; set; } // ContractValue
		/// <summary>关联主合同</summary>	
		[Description("关联主合同")]
        public string MainContract { get; set; } // MainContract
		/// <summary>关联主合同名称</summary>	
		[Description("关联主合同名称")]
        public string MainContractName { get; set; } // MainContractName
		/// <summary>币种</summary>	
		[Description("币种")]
        public string ContractCurrency { get; set; } // ContractCurrency
		/// <summary>结算人民币金额</summary>	
		[Description("结算人民币金额")]
        public decimal? ContractRMBAmount { get; set; } // ContractRMBAmount
		/// <summary>汇率</summary>	
		[Description("汇率")]
        public decimal? ExchangeRate { get; set; } // ExchangeRate
		/// <summary>合同金额大写</summary>	
		[Description("合同金额大写")]
        public string ContractValueDX { get; set; } // ContractValueDX
		/// <summary>结算人民币金额大写</summary>	
		[Description("结算人民币金额大写")]
        public string ContractRMBValueDX { get; set; } // ContractRMBValueDX
		/// <summary>甲方法人</summary>	
		[Description("甲方法人")]
        public string PartyALegalPerson { get; set; } // PartyALegalPerson
		/// <summary>乙方法人</summary>	
		[Description("乙方法人")]
        public string PartyBLegalPerson { get; set; } // PartyBLegalPerson
		/// <summary>丙方法人</summary>	
		[Description("丙方法人")]
        public string PartyCLegalPerson { get; set; } // PartyCLegalPerson
		/// <summary>甲方委托代理人</summary>	
		[Description("甲方委托代理人")]
        public string PartyAHusbandingAgentName { get; set; } // PartyAHusbandingAgentName
		/// <summary>乙方委托代理人</summary>	
		[Description("乙方委托代理人")]
        public string PartyBHusbandingAgentName { get; set; } // PartyBHusbandingAgentName
		/// <summary>丙方委托代理人</summary>	
		[Description("丙方委托代理人")]
        public string PartyCHusbandingAgentName { get; set; } // PartyCHusbandingAgentName
		/// <summary>商务部门</summary>	
		[Description("商务部门")]
        public string BusinessDept { get; set; } // BusinessDept
		/// <summary>商务部门名称</summary>	
		[Description("商务部门名称")]
        public string BusinessDeptName { get; set; } // BusinessDeptName
		/// <summary>商务经理</summary>	
		[Description("商务经理")]
        public string BusinessManager { get; set; } // BusinessManager
		/// <summary>商务经理名称</summary>	
		[Description("商务经理名称")]
        public string BusinessManagerName { get; set; } // BusinessManagerName
		/// <summary>生产部门</summary>	
		[Description("生产部门")]
        public string ProductionDept { get; set; } // ProductionDept
		/// <summary>生产部门名称</summary>	
		[Description("生产部门名称")]
        public string ProductionDeptName { get; set; } // ProductionDeptName
		/// <summary>生产责任人</summary>	
		[Description("生产责任人")]
        public string ProductionManager { get; set; } // ProductionManager
		/// <summary>生产责任人名称</summary>	
		[Description("生产责任人名称")]
        public string ProductionManagerName { get; set; } // ProductionManagerName
		/// <summary>所属工程</summary>	
		[Description("所属工程")]
        public string EngineeringInfo { get; set; } // EngineeringInfo
		/// <summary>所属工程名称</summary>	
		[Description("所属工程名称")]
        public string EngineeringInfoName { get; set; } // EngineeringInfoName
		/// <summary>合同分解</summary>	
		[Description("合同分解")]
        public string ContractSplit { get; set; } // ContractSplit
		/// <summary>合同文本</summary>	
		[Description("合同文本")]
        public string ContractAttachment { get; set; } // ContractAttachment
		/// <summary>相关资料</summary>	
		[Description("相关资料")]
        public string Attachment { get; set; } // Attachment
		/// <summary>备注</summary>	
		[Description("备注")]
        public string Remark { get; set; } // Remark
		/// <summary>收款合计</summary>	
		[Description("收款合计")]
        public decimal? SumReceiptValue { get; set; } // SumReceiptValue
		/// <summary>开票合计</summary>	
		[Description("开票合计")]
        public decimal? SumInvoiceValue { get; set; } // SumInvoiceValue
		/// <summary>坏账合计</summary>	
		[Description("坏账合计")]
        public decimal? SumBadDebtValue { get; set; } // SumBadDebtValue
		/// <summary>是否签约</summary>	
		[Description("是否签约")]
        public string IsSigned { get; set; } // IsSigned
		/// <summary>归属年份</summary>	
		[Description("归属年份")]
        public int? BelongYear { get; set; } // BelongYear
		/// <summary>归属月份</summary>	
		[Description("归属月份")]
        public int? BelongMonth { get; set; } // BelongMonth
		/// <summary>归属季度</summary>	
		[Description("归属季度")]
        public int? BelongQuarter { get; set; } // BelongQuarter
		/// <summary>合同文本状态</summary>	
		[Description("合同文本状态")]
        public string ContractTextState { get; set; } // ContractTextState
		/// <summary>合同文本寄出人</summary>	
		[Description("合同文本寄出人")]
        public string ContractTextSendUser { get; set; } // ContractTextSendUser
		/// <summary>合同文本寄出人名称</summary>	
		[Description("合同文本寄出人名称")]
        public string ContractTextSendUserName { get; set; } // ContractTextSendUserName
		/// <summary>甲方FullID</summary>	
		[Description("甲方FullID")]
        public string CustomerFullID { get; set; } // CustomerFullID
		/// <summary>上一版合同数据</summary>	
		[Description("上一版合同数据")]
        public string LastVersionData { get; set; } // LastVersionData
		/// <summary>申请变更人</summary>	
		[Description("申请变更人")]
        public string ApplyUser { get; set; } // ApplyUser
		/// <summary>申请变更人名称</summary>	
		[Description("申请变更人名称")]
        public string ApplyUserName { get; set; } // ApplyUserName
		/// <summary>申请日期</summary>	
		[Description("申请日期")]
        public DateTime? ApplyDate { get; set; } // ApplyDate
		/// <summary>参与会签人</summary>	
		[Description("参与会签人")]
        public string MeetingSignUsers { get; set; } // MeetingSignUsers
		/// <summary>参与会签人名称</summary>	
		[Description("参与会签人名称")]
        public string MeetingSignUsersName { get; set; } // MeetingSignUsersName
		/// <summary>经营领导审批</summary>	
		[Description("经营领导审批")]
        public string MarketLeaderSign { get; set; } // MarketLeaderSign
		/// <summary>技术部门审核</summary>	
		[Description("技术部门审核")]
        public string TechLeaderSign { get; set; } // TechLeaderSign
		/// <summary>公司领导审批</summary>	
		[Description("公司领导审批")]
        public string CompanyLeaderSign { get; set; } // CompanyLeaderSign
		/// <summary>ContractID</summary>	
		[Description("ContractID")]
        public string ContractID { get; set; } // ContractID
		/// <summary>变更说明</summary>	
		[Description("变更说明")]
        public string ChangeDescription { get; set; } // ChangeDescription
		/// <summary>甲方FullID名称</summary>	
		[Description("甲方FullID名称")]
        public string CustomerFullIDName { get; set; } // CustomerFullIDName
		/// <summary>本合同人民币金额</summary>	
		[Description("本合同人民币金额")]
        public decimal? ThisContractRMBAmount { get; set; } // ThisContractRMBAmount
		/// <summary>人民币金额大写</summary>	
		[Description("人民币金额大写")]
        public string ThisContractRMBAmountDX { get; set; } // ThisContractRMBAmountDX
		/// <summary>税率</summary>	
		[Description("税率")]
        public decimal? TaxRate { get; set; } // TaxRate
		/// <summary>税名</summary>	
		[Description("税名")]
        public string TaxName { get; set; } // TaxName
		/// <summary>税金</summary>	
		[Description("税金")]
        public decimal? TaxValue { get; set; } // TaxValue
		/// <summary>不含税金额</summary>	
		[Description("不含税金额")]
        public decimal? ClearContractValue { get; set; } // ClearContractValue
		/// <summary>PayerUnit</summary>	
		[Description("PayerUnit")]
        public string PayerUnit { get; set; } // PayerUnit
		/// <summary>PayerUnit名称</summary>	
		[Description("PayerUnit名称")]
        public string PayerUnitName { get; set; } // PayerUnitName

        // Reverse navigation
		[JsonIgnore]
        public virtual ICollection<T_C_ContractChange_ContractSplit> T_C_ContractChange_ContractSplit { get { onT_C_ContractChange_ContractSplitGetting(); return _T_C_ContractChange_ContractSplit;} }
		private ICollection<T_C_ContractChange_ContractSplit> _T_C_ContractChange_ContractSplit;
		partial void onT_C_ContractChange_ContractSplitGetting();

		[JsonIgnore]
        public virtual ICollection<T_C_ContractChange_DeptRelation> T_C_ContractChange_DeptRelation { get { onT_C_ContractChange_DeptRelationGetting(); return _T_C_ContractChange_DeptRelation;} }
		private ICollection<T_C_ContractChange_DeptRelation> _T_C_ContractChange_DeptRelation;
		partial void onT_C_ContractChange_DeptRelationGetting();

		[JsonIgnore]
        public virtual ICollection<T_C_ContractChange_PaymentDetail> T_C_ContractChange_PaymentDetail { get { onT_C_ContractChange_PaymentDetailGetting(); return _T_C_ContractChange_PaymentDetail;} }
		private ICollection<T_C_ContractChange_PaymentDetail> _T_C_ContractChange_PaymentDetail;
		partial void onT_C_ContractChange_PaymentDetailGetting();

		[JsonIgnore]
        public virtual ICollection<T_C_ContractChange_ProjectRelation> T_C_ContractChange_ProjectRelation { get { onT_C_ContractChange_ProjectRelationGetting(); return _T_C_ContractChange_ProjectRelation;} }
		private ICollection<T_C_ContractChange_ProjectRelation> _T_C_ContractChange_ProjectRelation;
		partial void onT_C_ContractChange_ProjectRelationGetting();

		[JsonIgnore]
        public virtual ICollection<T_C_ContractChange_ReceiptObj> T_C_ContractChange_ReceiptObj { get { onT_C_ContractChange_ReceiptObjGetting(); return _T_C_ContractChange_ReceiptObj;} }
		private ICollection<T_C_ContractChange_ReceiptObj> _T_C_ContractChange_ReceiptObj;
		partial void onT_C_ContractChange_ReceiptObjGetting();


        public T_C_ContractChange()
        {
            _T_C_ContractChange_ContractSplit = new List<T_C_ContractChange_ContractSplit>();
            _T_C_ContractChange_DeptRelation = new List<T_C_ContractChange_DeptRelation>();
            _T_C_ContractChange_PaymentDetail = new List<T_C_ContractChange_PaymentDetail>();
            _T_C_ContractChange_ProjectRelation = new List<T_C_ContractChange_ProjectRelation>();
            _T_C_ContractChange_ReceiptObj = new List<T_C_ContractChange_ReceiptObj>();
        }
    }

	/// <summary>合同分解</summary>	
	[Description("合同分解")]
    public partial class T_C_ContractChange_ContractSplit : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string T_C_ContractChangeID { get; set; } // T_C_ContractChangeID
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string IsReleased { get; set; } // IsReleased
		/// <summary>分类</summary>	
		[Description("分类")]
        public string Name { get; set; } // Name
		/// <summary>业务类型</summary>	
		[Description("业务类型")]
        public string BusinessType { get; set; } // BusinessType
		/// <summary>百分比</summary>	
		[Description("百分比")]
        public decimal? Scale { get; set; } // Scale
		/// <summary>分解合同额（元）</summary>	
		[Description("分解合同额（元）")]
        public decimal? SplitValue { get; set; } // SplitValue
		/// <summary>分解类型</summary>	
		[Description("分解类型")]
        public string SplitType { get; set; } // SplitType
		/// <summary>OrlID</summary>	
		[Description("OrlID")]
        public string OrlID { get; set; } // OrlID

        // Foreign keys
		[JsonIgnore]
        public virtual T_C_ContractChange T_C_ContractChange { get; set; } //  T_C_ContractChangeID - FK_T_C_ContractChange_ContractSplit_T_C_ContractChange
    }

	/// <summary>部门分解</summary>	
	[Description("部门分解")]
    public partial class T_C_ContractChange_DeptRelation : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string T_C_ContractChangeID { get; set; } // T_C_ContractChangeID
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string IsReleased { get; set; } // IsReleased
		/// <summary>部门</summary>	
		[Description("部门")]
        public string Dept { get; set; } // Dept
		/// <summary>部门名称</summary>	
		[Description("部门名称")]
        public string DeptName { get; set; } // DeptName
		/// <summary>占比（%）</summary>	
		[Description("占比（%）")]
        public decimal? Scale { get; set; } // Scale
		/// <summary>部门合同额（元）</summary>	
		[Description("部门合同额（元）")]
        public decimal? DeptValue { get; set; } // DeptValue
		/// <summary>补充协议合同额（元）</summary>	
		[Description("补充协议合同额（元）")]
        public decimal? SumSupplementaryValue { get; set; } // SumSupplementaryValue
		/// <summary>累计收款（元）</summary>	
		[Description("累计收款（元）")]
        public decimal? SumDeptReceiptValue { get; set; } // SumDeptReceiptValue
		/// <summary>OrlID</summary>	
		[Description("OrlID")]
        public string OrlID { get; set; } // OrlID

        // Foreign keys
		[JsonIgnore]
        public virtual T_C_ContractChange T_C_ContractChange { get; set; } //  T_C_ContractChangeID - FK_T_C_ContractChange_DeptRelation_T_C_ContractChange
    }

	/// <summary>付款单位列表</summary>	
	[Description("付款单位列表")]
    public partial class T_C_ContractChange_PaymentDetail : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string T_C_ContractChangeID { get; set; } // T_C_ContractChangeID
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string IsReleased { get; set; } // IsReleased
		/// <summary>付款方</summary>	
		[Description("付款方")]
        public string CustomerID { get; set; } // CustomerID
		/// <summary>付款方名称</summary>	
		[Description("付款方名称")]
        public string CustomerIDName { get; set; } // CustomerIDName
		/// <summary>付款方联系人</summary>	
		[Description("付款方联系人")]
        public string PaymentLinkMan { get; set; } // PaymentLinkMan
		/// <summary>付款方联系人电话</summary>	
		[Description("付款方联系人电话")]
        public string PaymentLinkTel { get; set; } // PaymentLinkTel
		/// <summary>证明材料</summary>	
		[Description("证明材料")]
        public string EvidenceAttachment { get; set; } // EvidenceAttachment

        // Foreign keys
		[JsonIgnore]
        public virtual T_C_ContractChange T_C_ContractChange { get; set; } //  T_C_ContractChangeID - FK_T_C_ContractChange_PaymentDetail_T_C_ContractChange
    }

	/// <summary>关联项目</summary>	
	[Description("关联项目")]
    public partial class T_C_ContractChange_ProjectRelation : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string T_C_ContractChangeID { get; set; } // T_C_ContractChangeID
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string IsReleased { get; set; } // IsReleased
		/// <summary>项目编号</summary>	
		[Description("项目编号")]
        public string ProjectCode { get; set; } // ProjectCode
		/// <summary>项目名称</summary>	
		[Description("项目名称")]
        public string ProjectName { get; set; } // ProjectName
		/// <summary>项目ID</summary>	
		[Description("项目ID")]
        public string ProjectID { get; set; } // ProjectID
		/// <summary>占比%</summary>	
		[Description("占比%")]
        public decimal? Scale { get; set; } // Scale
		/// <summary>合同金额（元）</summary>	
		[Description("合同金额（元）")]
        public decimal? ProjectValue { get; set; } // ProjectValue
		/// <summary>OrlID</summary>	
		[Description("OrlID")]
        public string OrlID { get; set; } // OrlID
		/// <summary>税率</summary>	
		[Description("税率")]
        public decimal? TaxRate { get; set; } // TaxRate
		/// <summary>税金（元）</summary>	
		[Description("税金（元）")]
        public decimal? TaxValue { get; set; } // TaxValue
		/// <summary>不含税金额（元）</summary>	
		[Description("不含税金额（元）")]
        public decimal? ClearValue { get; set; } // ClearValue
		/// <summary>项目部门</summary>	
		[Description("项目部门")]
        public string ChargerDept { get; set; } // ChargerDept
		/// <summary>项目部门名称</summary>	
		[Description("项目部门名称")]
        public string ChargerDeptName { get; set; } // ChargerDeptName

        // Foreign keys
		[JsonIgnore]
        public virtual T_C_ContractChange T_C_ContractChange { get; set; } //  T_C_ContractChangeID - FK_T_C_ContractChange_ProjectRelation_T_C_ContractChange
    }

	/// <summary>合同收款项</summary>	
	[Description("合同收款项")]
    public partial class T_C_ContractChange_ReceiptObj : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string T_C_ContractChangeID { get; set; } // T_C_ContractChangeID
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string IsReleased { get; set; } // IsReleased
		/// <summary>收款项名称</summary>	
		[Description("收款项名称")]
        public string Name { get; set; } // Name
		/// <summary>占比（%）</summary>	
		[Description("占比（%）")]
        public decimal? ReceiptPercent { get; set; } // ReceiptPercent
		/// <summary>金额（元）</summary>	
		[Description("金额（元）")]
        public decimal? ReceiptValue { get; set; } // ReceiptValue
		/// <summary>已开票（元）</summary>	
		[Description("已开票（元）")]
        public decimal? FactInvoiceValue { get; set; } // FactInvoiceValue
		/// <summary>已收款（元）</summary>	
		[Description("已收款（元）")]
        public decimal? FactReceiptValue { get; set; } // FactReceiptValue
		/// <summary>原计划日期</summary>	
		[Description("原计划日期")]
        public DateTime? OriginalPlanFinishDate { get; set; } // OriginalPlanFinishDate
		/// <summary>计划日期</summary>	
		[Description("计划日期")]
        public DateTime? PlanFinishDate { get; set; } // PlanFinishDate
		/// <summary>关联项目</summary>	
		[Description("关联项目")]
        public string ProjectInfo { get; set; } // ProjectInfo
		/// <summary>关联项目名称</summary>	
		[Description("关联项目名称")]
        public string ProjectInfoName { get; set; } // ProjectInfoName
		/// <summary>关联里程碑.名称</summary>	
		[Description("关联里程碑.名称")]
        public string MileStoneName { get; set; } // MileStoneName
		/// <summary>关联里程碑.计划完成时间</summary>	
		[Description("关联里程碑.计划完成时间")]
        public DateTime? MileStonePlanEndDate { get; set; } // MileStonePlanEndDate
		/// <summary>关联里程碑.实际完成时间</summary>	
		[Description("关联里程碑.实际完成时间")]
        public DateTime? MileStoneFactEndDate { get; set; } // MileStoneFactEndDate
		/// <summary></summary>	
		[Description("")]
        public string MileStoneID { get; set; } // MileStoneID
		/// <summary>坏账（元）</summary>	
		[Description("坏账（元）")]
        public decimal? FactBadValue { get; set; } // FactBadValue
		/// <summary>MileStoneState</summary>	
		[Description("MileStoneState")]
        public string MileStoneState { get; set; } // MileStoneState
		/// <summary>OrlID</summary>	
		[Description("OrlID")]
        public string OrlID { get; set; } // OrlID
		/// <summary>补充协议</summary>	
		[Description("补充协议")]
        public string SupplementaryID { get; set; } // SupplementaryID

        // Foreign keys
		[JsonIgnore]
        public virtual T_C_ContractChange T_C_ContractChange { get; set; } //  T_C_ContractChangeID - FK_T_C_ContractChange_ReceiptObj_T_C_ContractChange
    }

	/// <summary>合同评审单</summary>	
	[Description("合同评审单")]
    public partial class T_C_ContractReview : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary>发起日期</summary>	
		[Description("发起日期")]
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
		/// <summary>合同号</summary>	
		[Description("合同号")]
        public string ContractCode { get; set; } // ContractCode
		/// <summary>合同性质</summary>	
		[Description("合同性质")]
        public string NewOrAdded { get; set; } // NewOrAdded
		/// <summary>合同名称</summary>	
		[Description("合同名称")]
        public string ContractName { get; set; } // ContractName
		/// <summary>甲方ID</summary>	
		[Description("甲方ID")]
        public string PartyAID { get; set; } // PartyAID
		/// <summary>甲方</summary>	
		[Description("甲方")]
        public string PartyA { get; set; } // PartyA
		/// <summary>生产负责部门ID</summary>	
		[Description("生产负责部门ID")]
        public string ProductionUnitID { get; set; } // ProductionUnitID
		/// <summary>生产负责部门</summary>	
		[Description("生产负责部门")]
        public string ProductionUnitName { get; set; } // ProductionUnitName
		/// <summary>评审方式</summary>	
		[Description("评审方式")]
        public string ReviewType { get; set; } // ReviewType
		/// <summary>生产负责人</summary>	
		[Description("生产负责人")]
        public string ProduceMasterName { get; set; } // ProduceMasterName
		/// <summary>生产负责人ID</summary>	
		[Description("生产负责人ID")]
        public string ProduceMasterID { get; set; } // ProduceMasterID
		/// <summary>顾客规定的要求</summary>	
		[Description("顾客规定的要求")]
        public string CustomerRequire { get; set; } // CustomerRequire
		/// <summary>法律、法规</summary>	
		[Description("法律、法规")]
        public string LawRequire { get; set; } // LawRequire
		/// <summary>附加要求</summary>	
		[Description("附加要求")]
        public string AdditionalRequire { get; set; } // AdditionalRequire
		/// <summary>合同文本</summary>	
		[Description("合同文本")]
        public string ContractAttachment { get; set; } // ContractAttachment
		/// <summary>相关资料</summary>	
		[Description("相关资料")]
        public string Attachment { get; set; } // Attachment
		/// <summary>评审结论</summary>	
		[Description("评审结论")]
        public string Remark { get; set; } // Remark
		/// <summary>生产负责人意见</summary>	
		[Description("生产负责人意见")]
        public string ProduceMasterApproval { get; set; } // ProduceMasterApproval
		/// <summary>会签意见</summary>	
		[Description("会签意见")]
        public string ReviewApproval { get; set; } // ReviewApproval
		/// <summary>市场部领导意见</summary>	
		[Description("市场部领导意见")]
        public string OrgLeaderApproval { get; set; } // OrgLeaderApproval
		/// <summary>公司领导意见</summary>	
		[Description("公司领导意见")]
        public string CompLeaderApproval { get; set; } // CompLeaderApproval
		/// <summary>合同ID</summary>	
		[Description("合同ID")]
        public string ContractID { get; set; } // ContractID
		/// <summary>会签人</summary>	
		[Description("会签人")]
        public string CosignUser { get; set; } // CosignUser
		/// <summary>会签人名称</summary>	
		[Description("会签人名称")]
        public string CosignUserName { get; set; } // CosignUserName
		/// <summary>项目的要求是否得到确定</summary>	
		[Description("项目的要求是否得到确定")]
        public string RequriedClear { get; set; } // RequriedClear
		/// <summary>与以前不一致的合同要求和口头要求是否解决</summary>	
		[Description("与以前不一致的合同要求和口头要求是否解决")]
        public string ConflictResloved { get; set; } // ConflictResloved
		/// <summary>是否有能力满足规定的要求</summary>	
		[Description("是否有能力满足规定的要求")]
        public string CanDo { get; set; } // CanDo
		/// <summary>弃用1</summary>	
		[Description("弃用1")]
        public string PartyASub { get; set; } // PartyASub
		/// <summary>弃用2</summary>	
		[Description("弃用2")]
        public string PartyASubName { get; set; } // PartyASubName
    }

	/// <summary>红冲申请</summary>	
	[Description("红冲申请")]
    public partial class T_C_CreditNoteApply : Formula.BaseModel
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
		/// <summary>合同</summary>	
		[Description("合同")]
        public string Contract { get; set; } // Contract
		/// <summary>合同名称</summary>	
		[Description("合同名称")]
        public string ContractName { get; set; } // ContractName
		/// <summary>付款单位</summary>	
		[Description("付款单位")]
        public string PayerUnit { get; set; } // PayerUnit
		/// <summary>付款单位(弃用)</summary>	
		[Description("付款单位(弃用)")]
        public string PayerUnitSub { get; set; } // PayerUnitSub
		/// <summary>付款单位(弃用)名称</summary>	
		[Description("付款单位(弃用)名称")]
        public string PayerUnitSubName { get; set; } // PayerUnitSubName
		/// <summary>红冲金额</summary>	
		[Description("红冲金额")]
        public decimal? Amount { get; set; } // Amount
		/// <summary>申请人</summary>	
		[Description("申请人")]
        public string Applyer { get; set; } // Applyer
		/// <summary>申请人名称</summary>	
		[Description("申请人名称")]
        public string ApplyerName { get; set; } // ApplyerName
		/// <summary>申请日期</summary>	
		[Description("申请日期")]
        public DateTime? ApplyDate { get; set; } // ApplyDate
		/// <summary>备注</summary>	
		[Description("备注")]
        public string Remark { get; set; } // Remark
		/// <summary>状态</summary>	
		[Description("状态")]
        public string State { get; set; } // State
		/// <summary>大写</summary>	
		[Description("大写")]
        public string Capital { get; set; } // Capital
		/// <summary>税号</summary>	
		[Description("税号")]
        public string TaxAccount { get; set; } // TaxAccount
		/// <summary>开票单位名称</summary>	
		[Description("开票单位名称")]
        public string InvoiceName { get; set; } // InvoiceName
		/// <summary>单位地址</summary>	
		[Description("单位地址")]
        public string Address { get; set; } // Address
		/// <summary>电话号码</summary>	
		[Description("电话号码")]
        public string TelPhone { get; set; } // TelPhone
		/// <summary>开户银行</summary>	
		[Description("开户银行")]
        public string BankName { get; set; } // BankName
		/// <summary>银行账号</summary>	
		[Description("银行账号")]
        public string BankAccount { get; set; } // BankAccount
		/// <summary>开票编号</summary>	
		[Description("开票编号")]
        public string InvoiceCode { get; set; } // InvoiceCode
		/// <summary>税率</summary>	
		[Description("税率")]
        public decimal? TaxRate { get; set; } // TaxRate
		/// <summary>所长意见</summary>	
		[Description("所长意见")]
        public string Director { get; set; } // Director
		/// <summary>财务意见</summary>	
		[Description("财务意见")]
        public string Finance { get; set; } // Finance
		/// <summary>付款单位名称</summary>	
		[Description("付款单位名称")]
        public string PayerUnitName { get; set; } // PayerUnitName
		/// <summary>税金</summary>	
		[Description("税金")]
        public decimal? TaxValue { get; set; } // TaxValue
		/// <summary>不含税金额</summary>	
		[Description("不含税金额")]
        public decimal? ClearValue { get; set; } // ClearValue
		/// <summary>税名</summary>	
		[Description("税名")]
        public string TaxName { get; set; } // TaxName
		/// <summary>合同甲方</summary>	
		[Description("合同甲方")]
        public string CustomerID { get; set; } // CustomerID
		/// <summary>合同甲方名称</summary>	
		[Description("合同甲方名称")]
        public string CustomerIDName { get; set; } // CustomerIDName
		/// <summary>合同甲方FullID</summary>	
		[Description("合同甲方FullID")]
        public string CustomerFullID { get; set; } // CustomerFullID

        // Reverse navigation
		[JsonIgnore]
        public virtual ICollection<T_C_CreditNoteApply_Detail> T_C_CreditNoteApply_Detail { get { onT_C_CreditNoteApply_DetailGetting(); return _T_C_CreditNoteApply_Detail;} }
		private ICollection<T_C_CreditNoteApply_Detail> _T_C_CreditNoteApply_Detail;
		partial void onT_C_CreditNoteApply_DetailGetting();


        public T_C_CreditNoteApply()
        {
            _T_C_CreditNoteApply_Detail = new List<T_C_CreditNoteApply_Detail>();
        }
    }

	/// <summary>红冲明细</summary>	
	[Description("红冲明细")]
    public partial class T_C_CreditNoteApply_Detail : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string T_C_CreditNoteApplyID { get; set; } // T_C_CreditNoteApplyID
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string IsReleased { get; set; } // IsReleased
		/// <summary>收款项名称</summary>	
		[Description("收款项名称")]
        public string PlanReceiptName { get; set; } // PlanReceiptName
		/// <summary>已开票金额（元）</summary>	
		[Description("已开票金额（元）")]
        public decimal? SumInvoiceAmount { get; set; } // SumInvoiceAmount
		/// <summary>可开票金额（元）</summary>	
		[Description("可开票金额（元）")]
        public decimal? RemainInvoiceAmount { get; set; } // RemainInvoiceAmount
		/// <summary>本次申请开票（元）</summary>	
		[Description("本次申请开票（元）")]
        public decimal? ApplyInvoiceAmount { get; set; } // ApplyInvoiceAmount
		/// <summary>收款项ID</summary>	
		[Description("收款项ID")]
        public string PlanReceiptID { get; set; } // PlanReceiptID
		/// <summary>已红冲金额（元）</summary>	
		[Description("已红冲金额（元）")]
        public decimal? MinusRelationValue { get; set; } // MinusRelationValue
		/// <summary>可红冲金额（元）</summary>	
		[Description("可红冲金额（元）")]
        public decimal? RemainValue { get; set; } // RemainValue
		/// <summary>本次红冲金额（元）</summary>	
		[Description("本次红冲金额（元）")]
        public decimal? CreditValue { get; set; } // CreditValue

        // Foreign keys
		[JsonIgnore]
        public virtual T_C_CreditNoteApply T_C_CreditNoteApply { get; set; } //  T_C_CreditNoteApplyID - FK_T_C_CreditNote_Detail_T_C_CreditNote
    }

	/// <summary>保函申请</summary>	
	[Description("保函申请")]
    public partial class T_C_GuaranteeLetter : Formula.BaseModel
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
		/// <summary>合同名称</summary>	
		[Description("合同名称")]
        public string Contract { get; set; } // Contract
		/// <summary>合同名称名称</summary>	
		[Description("合同名称名称")]
        public string ContractName { get; set; } // ContractName
		/// <summary>合同号</summary>	
		[Description("合同号")]
        public string ContractCode { get; set; } // ContractCode
		/// <summary>项目名称</summary>	
		[Description("项目名称")]
        public string Project { get; set; } // Project
		/// <summary>项目编号</summary>	
		[Description("项目编号")]
        public string ProjectCode { get; set; } // ProjectCode
		/// <summary>合同金额</summary>	
		[Description("合同金额")]
        public string ContractAmount { get; set; } // ContractAmount
		/// <summary>保函类别</summary>	
		[Description("保函类别")]
        public string GuaranteeLetterType { get; set; } // GuaranteeLetterType
		/// <summary>保函金额(元)</summary>	
		[Description("保函金额(元)")]
        public decimal? GuaranteeLetterAmount { get; set; } // GuaranteeLetterAmount
		/// <summary>保函到期日</summary>	
		[Description("保函到期日")]
        public DateTime? GuaranteeLetterDeadline { get; set; } // GuaranteeLetterDeadline
		/// <summary>受益人名称</summary>	
		[Description("受益人名称")]
        public string BeneficiaryName { get; set; } // BeneficiaryName
		/// <summary>说明</summary>	
		[Description("说明")]
        public string Explain { get; set; } // Explain
		/// <summary>项目经理签字</summary>	
		[Description("项目经理签字")]
        public string ManagerSign { get; set; } // ManagerSign
		/// <summary>主管项目副总审批</summary>	
		[Description("主管项目副总审批")]
        public string DirectorSign { get; set; } // DirectorSign
		/// <summary>财务经理接收</summary>	
		[Description("财务经理接收")]
        public string FinancialSign { get; set; } // FinancialSign
    }

	/// <summary>开票申请</summary>	
	[Description("开票申请")]
    public partial class T_C_InvoiceApply : Formula.BaseModel
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
		/// <summary>开票合同</summary>	
		[Description("开票合同")]
        public string Contract { get; set; } // Contract
		/// <summary>开票合同</summary>	
		[Description("开票合同")]
        public string ContractName { get; set; } // ContractName
		/// <summary>付款单位</summary>	
		[Description("付款单位")]
        public string PayerUnit { get; set; } // PayerUnit
		/// <summary>付款单位</summary>	
		[Description("付款单位")]
        public string PayerUnitName { get; set; } // PayerUnitName
		/// <summary>付款单位(弃用)</summary>	
		[Description("付款单位(弃用)")]
        public string PayerUnitSub { get; set; } // PayerUnitSub
		/// <summary>付款单位(弃用)名称</summary>	
		[Description("付款单位(弃用)名称")]
        public string PayerUnitSubName { get; set; } // PayerUnitSubName
		/// <summary>开票金额</summary>	
		[Description("开票金额")]
        public decimal? Amount { get; set; } // Amount
		/// <summary>申请人</summary>	
		[Description("申请人")]
        public string Applyer { get; set; } // Applyer
		/// <summary>申请人名称</summary>	
		[Description("申请人名称")]
        public string ApplyerName { get; set; } // ApplyerName
		/// <summary>申请日期</summary>	
		[Description("申请日期")]
        public DateTime? ApplyDate { get; set; } // ApplyDate
		/// <summary>备注</summary>	
		[Description("备注")]
        public string Remark { get; set; } // Remark
		/// <summary>状态</summary>	
		[Description("状态")]
        public string State { get; set; } // State
		/// <summary>发票类型</summary>	
		[Description("发票类型")]
        public string InvoiceType { get; set; } // InvoiceType
		/// <summary>大写</summary>	
		[Description("大写")]
        public string Capital { get; set; } // Capital
		/// <summary>开票编号</summary>	
		[Description("开票编号")]
        public string InvoiceCode { get; set; } // InvoiceCode
		/// <summary>税率</summary>	
		[Description("税率")]
        public decimal? TaxRate { get; set; } // TaxRate
		/// <summary>所长意见</summary>	
		[Description("所长意见")]
        public string Director { get; set; } // Director
		/// <summary>财务意见</summary>	
		[Description("财务意见")]
        public string Finance { get; set; } // Finance
		/// <summary>税号</summary>	
		[Description("税号")]
        public string TaxAccount { get; set; } // TaxAccount
		/// <summary>单位地址</summary>	
		[Description("单位地址")]
        public string Address { get; set; } // Address
		/// <summary>电话号码</summary>	
		[Description("电话号码")]
        public string TelPhone { get; set; } // TelPhone
		/// <summary>开户银行</summary>	
		[Description("开户银行")]
        public string BankName { get; set; } // BankName
		/// <summary>银行账号</summary>	
		[Description("银行账号")]
        public string BankAccount { get; set; } // BankAccount
		/// <summary>开票单位名称</summary>	
		[Description("开票单位名称")]
        public string InvoiceName { get; set; } // InvoiceName
		/// <summary>开票明细</summary>	
		[Description("开票明细")]
        public string Detail { get; set; } // Detail
		/// <summary>税名</summary>	
		[Description("税名")]
        public string TaxName { get; set; } // TaxName
		/// <summary>税金</summary>	
		[Description("税金")]
        public decimal? TaxValue { get; set; } // TaxValue
		/// <summary>不含税金额</summary>	
		[Description("不含税金额")]
        public decimal? ClearValue { get; set; } // ClearValue
		/// <summary>收入单元</summary>	
		[Description("收入单元")]
        public string IncomeUnit { get; set; } // IncomeUnit
		/// <summary>收入单元名称</summary>	
		[Description("收入单元名称")]
        public string IncomeUnitName { get; set; } // IncomeUnitName
		/// <summary>收入单元编号</summary>	
		[Description("收入单元编号")]
        public string IncomeUnitCode { get; set; } // IncomeUnitCode
		/// <summary>合同甲方FullID</summary>	
		[Description("合同甲方FullID")]
        public string CustomerFullID { get; set; } // CustomerFullID
		/// <summary>合同甲方</summary>	
		[Description("合同甲方")]
        public string CustomerID { get; set; } // CustomerID
		/// <summary>合同甲方名称</summary>	
		[Description("合同甲方名称")]
        public string CustomerIDName { get; set; } // CustomerIDName

        // Reverse navigation
		[JsonIgnore]
        public virtual ICollection<T_C_InvoiceApply_Detail> T_C_InvoiceApply_Detail { get { onT_C_InvoiceApply_DetailGetting(); return _T_C_InvoiceApply_Detail;} }
		private ICollection<T_C_InvoiceApply_Detail> _T_C_InvoiceApply_Detail;
		partial void onT_C_InvoiceApply_DetailGetting();


        public T_C_InvoiceApply()
        {
            _T_C_InvoiceApply_Detail = new List<T_C_InvoiceApply_Detail>();
        }
    }

	/// <summary>开票明细</summary>	
	[Description("开票明细")]
    public partial class T_C_InvoiceApply_Detail : Formula.BaseModel
    {
		/// <summary>ID</summary>	
		[Description("ID")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary>T_C_InvoiceApplyID</summary>	
		[Description("T_C_InvoiceApplyID")]
        public string T_C_InvoiceApplyID { get; set; } // T_C_InvoiceApplyID
		/// <summary>排序号</summary>	
		[Description("排序号")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary>已发布</summary>	
		[Description("已发布")]
        public string IsReleased { get; set; } // IsReleased
		/// <summary>收款项名称</summary>	
		[Description("收款项名称")]
        public string PlanReceiptName { get; set; } // PlanReceiptName
		/// <summary>可开票金额（元）</summary>	
		[Description("可开票金额（元）")]
        public decimal? RemainInvoiceAmount { get; set; } // RemainInvoiceAmount
		/// <summary>本次申请开票（元）</summary>	
		[Description("本次申请开票（元）")]
        public decimal? ApplyInvoiceAmount { get; set; } // ApplyInvoiceAmount
		/// <summary>收款项ID</summary>	
		[Description("收款项ID")]
        public string PlanReceiptID { get; set; } // PlanReceiptID
		/// <summary>已开票金额（元）</summary>	
		[Description("已开票金额（元）")]
        public decimal? SumInvoiceAmount { get; set; } // SumInvoiceAmount

        // Foreign keys
		[JsonIgnore]
        public virtual T_C_InvoiceApply T_C_InvoiceApply { get; set; } //  T_C_InvoiceApplyID - FK_T_C_InvoiceApply_Detail_T_C_InvoiceApply
    }

	/// <summary>发票补开申请</summary>	
	[Description("发票补开申请")]
    public partial class T_C_InvoiceMakeUpApply : Formula.BaseModel
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
		/// <summary>开票合同</summary>	
		[Description("开票合同")]
        public string Contract { get; set; } // Contract
		/// <summary>ContractName</summary>	
		[Description("ContractName")]
        public string ContractName { get; set; } // ContractName
		/// <summary>付款单位ID</summary>	
		[Description("付款单位ID")]
        public string PayerUnit { get; set; } // PayerUnit
		/// <summary>付款单位(弃用)</summary>	
		[Description("付款单位(弃用)")]
        public string PayerUnitSub { get; set; } // PayerUnitSub
		/// <summary>付款单位(弃用)名称</summary>	
		[Description("付款单位(弃用)名称")]
        public string PayerUnitSubName { get; set; } // PayerUnitSubName
		/// <summary>补开金额</summary>	
		[Description("补开金额")]
        public decimal? Amount { get; set; } // Amount
		/// <summary>申请人</summary>	
		[Description("申请人")]
        public string Applyer { get; set; } // Applyer
		/// <summary>申请人名称</summary>	
		[Description("申请人名称")]
        public string ApplyerName { get; set; } // ApplyerName
		/// <summary>申请日期</summary>	
		[Description("申请日期")]
        public DateTime? ApplyDate { get; set; } // ApplyDate
		/// <summary>备注</summary>	
		[Description("备注")]
        public string Remark { get; set; } // Remark
		/// <summary>状态</summary>	
		[Description("状态")]
        public string State { get; set; } // State
		/// <summary>发票类型</summary>	
		[Description("发票类型")]
        public string InvoiceType { get; set; } // InvoiceType
		/// <summary>大写</summary>	
		[Description("大写")]
        public string Capital { get; set; } // Capital
		/// <summary>税号</summary>	
		[Description("税号")]
        public string TaxAccount { get; set; } // TaxAccount
		/// <summary>开票单位名称</summary>	
		[Description("开票单位名称")]
        public string InvoiceName { get; set; } // InvoiceName
		/// <summary>单位地址</summary>	
		[Description("单位地址")]
        public string Address { get; set; } // Address
		/// <summary>电话号码</summary>	
		[Description("电话号码")]
        public string TelPhone { get; set; } // TelPhone
		/// <summary>开户银行</summary>	
		[Description("开户银行")]
        public string BankName { get; set; } // BankName
		/// <summary>银行账号</summary>	
		[Description("银行账号")]
        public string BankAccount { get; set; } // BankAccount
		/// <summary>开票编号</summary>	
		[Description("开票编号")]
        public string InvoiceCode { get; set; } // InvoiceCode
		/// <summary>税率</summary>	
		[Description("税率")]
        public decimal? TaxRate { get; set; } // TaxRate
		/// <summary>所长意见</summary>	
		[Description("所长意见")]
        public string Director { get; set; } // Director
		/// <summary>财务意见</summary>	
		[Description("财务意见")]
        public string Finance { get; set; } // Finance
		/// <summary>付款单位</summary>	
		[Description("付款单位")]
        public string PayerUnitName { get; set; } // PayerUnitName
		/// <summary>需补开金额</summary>	
		[Description("需补开金额")]
        public decimal? NeedAmount { get; set; } // NeedAmount
		/// <summary>剩余补开金额</summary>	
		[Description("剩余补开金额")]
        public decimal? RestNeedAmount { get; set; } // RestNeedAmount
		/// <summary>到款ID</summary>	
		[Description("到款ID")]
        public string ReceiptID { get; set; } // ReceiptID
		/// <summary>税名</summary>	
		[Description("税名")]
        public string TaxName { get; set; } // TaxName
		/// <summary>税金</summary>	
		[Description("税金")]
        public decimal? TaxValue { get; set; } // TaxValue
		/// <summary>不含税金额</summary>	
		[Description("不含税金额")]
        public decimal? ClearValue { get; set; } // ClearValue
		/// <summary>发票明细</summary>	
		[Description("发票明细")]
        public string Detail { get; set; } // Detail

        // Reverse navigation
		[JsonIgnore]
        public virtual ICollection<T_C_InvoiceMakeUpApply_Detail> T_C_InvoiceMakeUpApply_Detail { get { onT_C_InvoiceMakeUpApply_DetailGetting(); return _T_C_InvoiceMakeUpApply_Detail;} }
		private ICollection<T_C_InvoiceMakeUpApply_Detail> _T_C_InvoiceMakeUpApply_Detail;
		partial void onT_C_InvoiceMakeUpApply_DetailGetting();


        public T_C_InvoiceMakeUpApply()
        {
            _T_C_InvoiceMakeUpApply_Detail = new List<T_C_InvoiceMakeUpApply_Detail>();
        }
    }

	/// <summary>发票明细</summary>	
	[Description("发票明细")]
    public partial class T_C_InvoiceMakeUpApply_Detail : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string T_C_InvoiceMakeUpApplyID { get; set; } // T_C_InvoiceMakeUpApplyID
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string IsReleased { get; set; } // IsReleased
		/// <summary>收款项名称</summary>	
		[Description("收款项名称")]
        public string PlanReceiptName { get; set; } // PlanReceiptName
		/// <summary>已开票金额（元）</summary>	
		[Description("已开票金额（元）")]
        public decimal? SumInvoiceAmount { get; set; } // SumInvoiceAmount
		/// <summary>可开票金额（元）</summary>	
		[Description("可开票金额（元）")]
        public decimal? RemainInvoiceAmount { get; set; } // RemainInvoiceAmount
		/// <summary>本次申请开票（元）</summary>	
		[Description("本次申请开票（元）")]
        public decimal? ApplyInvoiceAmount { get; set; } // ApplyInvoiceAmount
		/// <summary>收款项ID</summary>	
		[Description("收款项ID")]
        public string PlanReceiptID { get; set; } // PlanReceiptID

        // Foreign keys
		[JsonIgnore]
        public virtual T_C_InvoiceMakeUpApply T_C_InvoiceMakeUpApply { get; set; } //  T_C_InvoiceMakeUpApplyID - FK_T_C_InvoiceMakeUpApply_Detail_T_C_InvoiceMakeUpApply
    }

	/// <summary>顾客要求评审</summary>	
	[Description("顾客要求评审")]
    public partial class T_CP_CustomerRequestReview : Formula.BaseModel
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
		/// <summary>项目名称</summary>	
		[Description("项目名称")]
        public string ProjectName { get; set; } // ProjectName
		/// <summary>委托单位</summary>	
		[Description("委托单位")]
        public string CustomerInfo { get; set; } // CustomerInfo
		/// <summary>委托单位名称</summary>	
		[Description("委托单位名称")]
        public string CustomerInfoName { get; set; } // CustomerInfoName
		/// <summary>项目规模</summary>	
		[Description("项目规模")]
        public string ProjectLevel { get; set; } // ProjectLevel
		/// <summary>委托内容</summary>	
		[Description("委托内容")]
        public string Phase { get; set; } // Phase
		/// <summary>设计部门</summary>	
		[Description("设计部门")]
        public string DeptInfo { get; set; } // DeptInfo
		/// <summary>设计部门名称</summary>	
		[Description("设计部门名称")]
        public string DeptInfoName { get; set; } // DeptInfoName
		/// <summary>评审方式</summary>	
		[Description("评审方式")]
        public string ReviewType { get; set; } // ReviewType
		/// <summary>顾客要求</summary>	
		[Description("顾客要求")]
        public string CustomerRequire { get; set; } // CustomerRequire
		/// <summary>相关法律法规要求</summary>	
		[Description("相关法律法规要求")]
        public string LawRequire { get; set; } // LawRequire
		/// <summary>附加要求</summary>	
		[Description("附加要求")]
        public string AddtionalRequire { get; set; } // AddtionalRequire
		/// <summary>主要风险</summary>	
		[Description("主要风险")]
        public string Risk { get; set; } // Risk
		/// <summary>相关附件</summary>	
		[Description("相关附件")]
        public string Attach { get; set; } // Attach
		/// <summary>备注</summary>	
		[Description("备注")]
        public string Remark { get; set; } // Remark
		/// <summary>要求是否清晰</summary>	
		[Description("要求是否清晰")]
        public string RequireClear { get; set; } // RequireClear
		/// <summary>解决不一致</summary>	
		[Description("解决不一致")]
        public string ConfictReslove { get; set; } // ConfictReslove
		/// <summary>满足客户要求</summary>	
		[Description("满足客户要求")]
        public string CanRequried { get; set; } // CanRequried
		/// <summary>能否做</summary>	
		[Description("能否做")]
        public string CanDo { get; set; } // CanDo
		/// <summary>设计部门负责人意见</summary>	
		[Description("设计部门负责人意见")]
        public string MainDesignOrgApproval { get; set; } // MainDesignOrgApproval
		/// <summary>市场经营部主任意见</summary>	
		[Description("市场经营部主任意见")]
        public string ChiefEngineerApproval { get; set; } // ChiefEngineerApproval
		/// <summary>分管院长意见</summary>	
		[Description("分管院长意见")]
        public string MajorOrgApproval { get; set; } // MajorOrgApproval
		/// <summary>总师意见</summary>	
		[Description("总师意见")]
        public string ProduceOrgApproval { get; set; } // ProduceOrgApproval
		/// <summary>院长意见</summary>	
		[Description("院长意见")]
        public string CompLeaderApproval { get; set; } // CompLeaderApproval
		/// <summary>顾客要求ID</summary>	
		[Description("顾客要求ID")]
        public string DemandID { get; set; } // DemandID
    }

	/// <summary>保函申请</summary>	
	[Description("保函申请")]
    public partial class T_F_GuaranteeLetterApply : Formula.BaseModel
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
		/// <summary>合同名称</summary>	
		[Description("合同名称")]
        public string Contract { get; set; } // Contract
		/// <summary>合同名称名称</summary>	
		[Description("合同名称名称")]
        public string ContractName { get; set; } // ContractName
		/// <summary>合同号</summary>	
		[Description("合同号")]
        public string ContractCode { get; set; } // ContractCode
		/// <summary>项目名称</summary>	
		[Description("项目名称")]
        public string Clue { get; set; } // Clue
		/// <summary>项目编号</summary>	
		[Description("项目编号")]
        public string ClueCode { get; set; } // ClueCode
		/// <summary>合同金额</summary>	
		[Description("合同金额")]
        public string ContractAmount { get; set; } // ContractAmount
		/// <summary>保函类别</summary>	
		[Description("保函类别")]
        public string GuaranteeLetterType { get; set; } // GuaranteeLetterType
		/// <summary>保函金额(元)</summary>	
		[Description("保函金额(元)")]
        public decimal? GuaranteeLetterAmount { get; set; } // GuaranteeLetterAmount
		/// <summary>保函到期日</summary>	
		[Description("保函到期日")]
        public DateTime? GuaranteeLetterDeadline { get; set; } // GuaranteeLetterDeadline
		/// <summary>受益人名称</summary>	
		[Description("受益人名称")]
        public string BeneficiaryName { get; set; } // BeneficiaryName
		/// <summary>说明</summary>	
		[Description("说明")]
        public string Explain { get; set; } // Explain
		/// <summary>项目经理签字</summary>	
		[Description("项目经理签字")]
        public string ManagerSign { get; set; } // ManagerSign
		/// <summary>主管项目副总审批</summary>	
		[Description("主管项目副总审批")]
        public string DirectorSign { get; set; } // DirectorSign
		/// <summary>财务经理接收</summary>	
		[Description("财务经理接收")]
        public string FinancialSign { get; set; } // FinancialSign
		/// <summary></summary>	
		[Description("")]
        public string Terminator { get; set; } // Terminator
		/// <summary></summary>	
		[Description("")]
        public string TerminatorName { get; set; } // TerminatorName
		/// <summary></summary>	
		[Description("")]
        public DateTime? TerminateDate { get; set; } // TerminateDate
		/// <summary></summary>	
		[Description("")]
        public string Status { get; set; } // Status
    }

	/// <summary>保函解除</summary>	
	[Description("保函解除")]
    public partial class T_F_GuaranteeLetterTerminate : Formula.BaseModel
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
		/// <summary>GuaranteeLetterID</summary>	
		[Description("GuaranteeLetterID")]
        public string GuaranteeLetterID { get; set; } // GuaranteeLetterID
		/// <summary>合同ID</summary>	
		[Description("合同ID")]
        public string Contract { get; set; } // Contract
		/// <summary>合同名称</summary>	
		[Description("合同名称")]
        public string ContractName { get; set; } // ContractName
		/// <summary>合同号</summary>	
		[Description("合同号")]
        public string ContractCode { get; set; } // ContractCode
		/// <summary>保函金额（元）</summary>	
		[Description("保函金额（元）")]
        public decimal? GuaranteeLetterAmount { get; set; } // GuaranteeLetterAmount
		/// <summary>保函到期日</summary>	
		[Description("保函到期日")]
        public DateTime? GuaranteeLetterDeadline { get; set; } // GuaranteeLetterDeadline
		/// <summary>解除人</summary>	
		[Description("解除人")]
        public string Terminator { get; set; } // Terminator
		/// <summary>解除人名称</summary>	
		[Description("解除人名称")]
        public string TerminatorName { get; set; } // TerminatorName
		/// <summary>解除日期</summary>	
		[Description("解除日期")]
        public DateTime? TerminateDate { get; set; } // TerminateDate
    }

	/// <summary>委外合同变更</summary>	
	[Description("委外合同变更")]
    public partial class T_SP_ContractChange : Formula.BaseModel
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
		/// <summary>合同名称</summary>	
		[Description("合同名称")]
        public string Name { get; set; } // Name
		/// <summary>编号</summary>	
		[Description("编号")]
        public string SerialNumber { get; set; } // SerialNumber
		/// <summary>供应商</summary>	
		[Description("供应商")]
        public string Supplier { get; set; } // Supplier
		/// <summary>供应商名称</summary>	
		[Description("供应商名称")]
        public string SupplierName { get; set; } // SupplierName
		/// <summary>关联经营合同</summary>	
		[Description("关联经营合同")]
        public string ManagerContract { get; set; } // ManagerContract
		/// <summary>关联经营合同名称</summary>	
		[Description("关联经营合同名称")]
        public string ManagerContractName { get; set; } // ManagerContractName
		/// <summary>经营合同编号</summary>	
		[Description("经营合同编号")]
        public string ManagerContractCode { get; set; } // ManagerContractCode
		/// <summary>项目名称</summary>	
		[Description("项目名称")]
        public string ProjectInfo { get; set; } // ProjectInfo
		/// <summary>项目名称名称</summary>	
		[Description("项目名称名称")]
        public string ProjectInfoName { get; set; } // ProjectInfoName
		/// <summary>项目编号</summary>	
		[Description("项目编号")]
        public string ProjectInfoCode { get; set; } // ProjectInfoCode
		/// <summary>合同性质</summary>	
		[Description("合同性质")]
        public string ContractClass { get; set; } // ContractClass
		/// <summary>关联主合同</summary>	
		[Description("关联主合同")]
        public string MainContract { get; set; } // MainContract
		/// <summary>关联主合同名称</summary>	
		[Description("关联主合同名称")]
        public string MainContractName { get; set; } // MainContractName
		/// <summary>主合同编号</summary>	
		[Description("主合同编号")]
        public string MainContractCode { get; set; } // MainContractCode
		/// <summary>分包部门</summary>	
		[Description("分包部门")]
        public string SupplierDept { get; set; } // SupplierDept
		/// <summary>分包部门名称</summary>	
		[Description("分包部门名称")]
        public string SupplierDeptName { get; set; } // SupplierDeptName
		/// <summary>合同金额</summary>	
		[Description("合同金额")]
        public decimal? ContractAmount { get; set; } // ContractAmount
		/// <summary>合同状态</summary>	
		[Description("合同状态")]
        public string State { get; set; } // State
		/// <summary>签约日期</summary>	
		[Description("签约日期")]
        public DateTime? SignDate { get; set; } // SignDate
		/// <summary>折合人民币（元）</summary>	
		[Description("折合人民币（元）")]
        public decimal? ThisContractValue { get; set; } // ThisContractValue
		/// <summary>税名</summary>	
		[Description("税名")]
        public string TaxName { get; set; } // TaxName
		/// <summary>税率</summary>	
		[Description("税率")]
        public decimal? TaxRate { get; set; } // TaxRate
		/// <summary>汇率</summary>	
		[Description("汇率")]
        public decimal? ExchangeRate { get; set; } // ExchangeRate
		/// <summary>币种</summary>	
		[Description("币种")]
        public string Currency { get; set; } // Currency
		/// <summary>人民币结算金额（元）</summary>	
		[Description("人民币结算金额（元）")]
        public decimal? ContractValue { get; set; } // ContractValue
		/// <summary>合同文本</summary>	
		[Description("合同文本")]
        public string ContractAttachment { get; set; } // ContractAttachment
		/// <summary>相关附件</summary>	
		[Description("相关附件")]
        public string Attachments { get; set; } // Attachments
		/// <summary>备注说明</summary>	
		[Description("备注说明")]
        public string Remark { get; set; } // Remark
		/// <summary>关联成本单元</summary>	
		[Description("关联成本单元")]
        public string ContractSplit { get; set; } // ContractSplit
		/// <summary>折合人民币大写</summary>	
		[Description("折合人民币大写")]
        public string ContractValueDX { get; set; } // ContractValueDX
		/// <summary>变更人</summary>	
		[Description("变更人")]
        public string ApplyUser { get; set; } // ApplyUser
		/// <summary>变更人名称</summary>	
		[Description("变更人名称")]
        public string ApplyUserName { get; set; } // ApplyUserName
		/// <summary>变更日期</summary>	
		[Description("变更日期")]
        public DateTime? ApplyDate { get; set; } // ApplyDate
		/// <summary>变更原因</summary>	
		[Description("变更原因")]
        public string Reason { get; set; } // Reason
		/// <summary>上一版数据</summary>	
		[Description("上一版数据")]
        public string LastVersionData { get; set; } // LastVersionData
		/// <summary>委外合同ID</summary>	
		[Description("委外合同ID")]
        public string ContractID { get; set; } // ContractID
		/// <summary>编号</summary>	
		[Description("编号")]
        public string ContractCode { get; set; } // ContractCode
		/// <summary>税金</summary>	
		[Description("税金")]
        public decimal? TaxValue { get; set; } // TaxValue
		/// <summary>不含税金额</summary>	
		[Description("不含税金额")]
        public decimal? ClearContractValue { get; set; } // ClearContractValue

        // Reverse navigation
		[JsonIgnore]
        public virtual ICollection<T_SP_ContractChange_ContractSplit> T_SP_ContractChange_ContractSplit { get { onT_SP_ContractChange_ContractSplitGetting(); return _T_SP_ContractChange_ContractSplit;} }
		private ICollection<T_SP_ContractChange_ContractSplit> _T_SP_ContractChange_ContractSplit;
		partial void onT_SP_ContractChange_ContractSplitGetting();


        public T_SP_ContractChange()
        {
            _T_SP_ContractChange_ContractSplit = new List<T_SP_ContractChange_ContractSplit>();
        }
    }

	/// <summary>关联成本单元</summary>	
	[Description("关联成本单元")]
    public partial class T_SP_ContractChange_ContractSplit : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string T_SP_ContractChangeID { get; set; } // T_SP_ContractChangeID
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
		/// <summary>负责人名称</summary>	
		[Description("负责人名称")]
        public string ChargerUserName { get; set; } // ChargerUserName
		/// <summary>责任部门名称</summary>	
		[Description("责任部门名称")]
        public string ChargerDeptName { get; set; } // ChargerDeptName
		/// <summary>委外金额（元）</summary>	
		[Description("委外金额（元）")]
        public decimal? SplitValue { get; set; } // SplitValue
		/// <summary>最低合同额（元）</summary>	
		[Description("最低合同额（元）")]
        public decimal? MinContractValue { get; set; } // MinContractValue
		/// <summary>CostUnitID</summary>	
		[Description("CostUnitID")]
        public string CostUnitID { get; set; } // CostUnitID
		/// <summary>OrlID</summary>	
		[Description("OrlID")]
        public string OrlID { get; set; } // OrlID
		/// <summary>负责人</summary>	
		[Description("负责人")]
        public string ChargerUser { get; set; } // ChargerUser
		/// <summary>责任部门</summary>	
		[Description("责任部门")]
        public string ChargerDept { get; set; } // ChargerDept

        // Foreign keys
		[JsonIgnore]
        public virtual T_SP_ContractChange T_SP_ContractChange { get; set; } //  T_SP_ContractChangeID - FK_T_SP_ContractChange_ContractSplit_T_SP_ContractChange
    }

	/// <summary>委外合同评审</summary>	
	[Description("委外合同评审")]
    public partial class T_SP_ContractReview : Formula.BaseModel
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
		/// <summary>合同名称</summary>	
		[Description("合同名称")]
        public string SPContractInfo { get; set; } // SPContractInfo
		/// <summary>合同名称名称</summary>	
		[Description("合同名称名称")]
        public string SPContractInfoName { get; set; } // SPContractInfoName
		/// <summary>合同编号</summary>	
		[Description("合同编号")]
        public string SPContractCode { get; set; } // SPContractCode
		/// <summary>收入合同</summary>	
		[Description("收入合同")]
        public string ContractInfo { get; set; } // ContractInfo
		/// <summary>收入合同名称</summary>	
		[Description("收入合同名称")]
        public string ContractInfoName { get; set; } // ContractInfoName
		/// <summary>收入合同编号</summary>	
		[Description("收入合同编号")]
        public string ContractCode { get; set; } // ContractCode
		/// <summary>选定供应商</summary>	
		[Description("选定供应商")]
        public string Supplier { get; set; } // Supplier
		/// <summary>选定供应商名称</summary>	
		[Description("选定供应商名称")]
        public string SupplierName { get; set; } // SupplierName
		/// <summary>关联项目</summary>	
		[Description("关联项目")]
        public string ProjectInfo { get; set; } // ProjectInfo
		/// <summary>关联项目名称</summary>	
		[Description("关联项目名称")]
        public string ProjectInfoName { get; set; } // ProjectInfoName
		/// <summary>责任部门</summary>	
		[Description("责任部门")]
        public string ChargerDept { get; set; } // ChargerDept
		/// <summary>责任部门名称</summary>	
		[Description("责任部门名称")]
        public string ChargerDeptName { get; set; } // ChargerDeptName
		/// <summary>发票到达</summary>	
		[Description("发票到达")]
        public string InvoiceCondition { get; set; } // InvoiceCondition
		/// <summary>申请人</summary>	
		[Description("申请人")]
        public string ApplyUser { get; set; } // ApplyUser
		/// <summary>申请人名称</summary>	
		[Description("申请人名称")]
        public string ApplyUserName { get; set; } // ApplyUserName
		/// <summary>申请日期</summary>	
		[Description("申请日期")]
        public DateTime? ApplyDate { get; set; } // ApplyDate
		/// <summary>委外金额</summary>	
		[Description("委外金额")]
        public decimal? SPContractValue { get; set; } // SPContractValue
		/// <summary>委外金额大写</summary>	
		[Description("委外金额大写")]
        public string SPContractValueDX { get; set; } // SPContractValueDX
		/// <summary>税名</summary>	
		[Description("税名")]
        public string TaxName { get; set; } // TaxName
		/// <summary>税率</summary>	
		[Description("税率")]
        public decimal? TaxRate { get; set; } // TaxRate
		/// <summary>备注</summary>	
		[Description("备注")]
        public string Remark { get; set; } // Remark
		/// <summary>合同文本</summary>	
		[Description("合同文本")]
        public string ContractText { get; set; } // ContractText
		/// <summary>其他附件</summary>	
		[Description("其他附件")]
        public string Attachments { get; set; } // Attachments
    }

	/// <summary>项目分包设计审批表</summary>	
	[Description("项目分包设计审批表")]
    public partial class T_SP_DesignApproval : Formula.BaseModel
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
		/// <summary>项目名称</summary>	
		[Description("项目名称")]
        public string Project { get; set; } // Project
		/// <summary>项目名称名称</summary>	
		[Description("项目名称名称")]
        public string ProjectName { get; set; } // ProjectName
		/// <summary>关联合同</summary>	
		[Description("关联合同")]
        public string Contract { get; set; } // Contract
		/// <summary>关联合同名称</summary>	
		[Description("关联合同名称")]
        public string ContractName { get; set; } // ContractName
		/// <summary>分包内容</summary>	
		[Description("分包内容")]
        public string Content { get; set; } // Content
		/// <summary>单位名称</summary>	
		[Description("单位名称")]
        public string Suppier { get; set; } // Suppier
		/// <summary>单位名称名称</summary>	
		[Description("单位名称名称")]
        public string SuppierName { get; set; } // SuppierName
		/// <summary>部门领导签字</summary>	
		[Description("部门领导签字")]
        public string DeptLeaderSign { get; set; } // DeptLeaderSign
		/// <summary>经营计划室签字</summary>	
		[Description("经营计划室签字")]
        public string MarketPlanSign { get; set; } // MarketPlanSign
		/// <summary>项目编号</summary>	
		[Description("项目编号")]
        public string ProjectCode { get; set; } // ProjectCode
		/// <summary>分包原因</summary>	
		[Description("分包原因")]
        public string Reason { get; set; } // Reason

        // Reverse navigation
		[JsonIgnore]
        public virtual ICollection<T_SP_DesignApproval_CoopProjectInfo> T_SP_DesignApproval_CoopProjectInfo { get { onT_SP_DesignApproval_CoopProjectInfoGetting(); return _T_SP_DesignApproval_CoopProjectInfo;} }
		private ICollection<T_SP_DesignApproval_CoopProjectInfo> _T_SP_DesignApproval_CoopProjectInfo;
		partial void onT_SP_DesignApproval_CoopProjectInfoGetting();

		[JsonIgnore]
        public virtual ICollection<T_SP_DesignApproval_CredentialInfo> T_SP_DesignApproval_CredentialInfo { get { onT_SP_DesignApproval_CredentialInfoGetting(); return _T_SP_DesignApproval_CredentialInfo;} }
		private ICollection<T_SP_DesignApproval_CredentialInfo> _T_SP_DesignApproval_CredentialInfo;
		partial void onT_SP_DesignApproval_CredentialInfoGetting();


        public T_SP_DesignApproval()
        {
            _T_SP_DesignApproval_CoopProjectInfo = new List<T_SP_DesignApproval_CoopProjectInfo>();
            _T_SP_DesignApproval_CredentialInfo = new List<T_SP_DesignApproval_CredentialInfo>();
        }
    }

	/// <summary>合作项目信息</summary>	
	[Description("合作项目信息")]
    public partial class T_SP_DesignApproval_CoopProjectInfo : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string T_SP_DesignApprovalID { get; set; } // T_SP_DesignApprovalID
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string IsReleased { get; set; } // IsReleased
		/// <summary>项目名称</summary>	
		[Description("项目名称")]
        public string Project { get; set; } // Project
		/// <summary>项目名称名称</summary>	
		[Description("项目名称名称")]
        public string ProjectName { get; set; } // ProjectName
		/// <summary>分包设计产品名称</summary>	
		[Description("分包设计产品名称")]
        public string ProductName { get; set; } // ProductName
		/// <summary>评定</summary>	
		[Description("评定")]
        public decimal? Evaluate { get; set; } // Evaluate

        // Foreign keys
		[JsonIgnore]
        public virtual T_SP_DesignApproval T_SP_DesignApproval { get; set; } //  T_SP_DesignApprovalID - FK_T_SP_DesignApproval_CoopProjectInfo_T_SP_DesignApproval
    }

	/// <summary>资质信息</summary>	
	[Description("资质信息")]
    public partial class T_SP_DesignApproval_CredentialInfo : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string T_SP_DesignApprovalID { get; set; } // T_SP_DesignApprovalID
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string IsReleased { get; set; } // IsReleased
		/// <summary>资质</summary>	
		[Description("资质")]
        public string CertificateName { get; set; } // CertificateName
		/// <summary>证书编号</summary>	
		[Description("证书编号")]
        public string CertificateCode { get; set; } // CertificateCode
		/// <summary>主要业绩</summary>	
		[Description("主要业绩")]
        public string Achievement { get; set; } // Achievement

        // Foreign keys
		[JsonIgnore]
        public virtual T_SP_DesignApproval T_SP_DesignApproval { get; set; } //  T_SP_DesignApprovalID - FK_T_SP_DesignApproval_CredentialInfo_T_SP_DesignApproval
    }

	/// <summary>项目分包设计评审</summary>	
	[Description("项目分包设计评审")]
    public partial class T_SP_DesignReview : Formula.BaseModel
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
		/// <summary>项目名称</summary>	
		[Description("项目名称")]
        public string Project { get; set; } // Project
		/// <summary>项目名称名称</summary>	
		[Description("项目名称名称")]
        public string ProjectName { get; set; } // ProjectName
		/// <summary>设计部门</summary>	
		[Description("设计部门")]
        public string DesignDept { get; set; } // DesignDept
		/// <summary>设计部门名称</summary>	
		[Description("设计部门名称")]
        public string DesignDeptName { get; set; } // DesignDeptName
		/// <summary>主合同号</summary>	
		[Description("主合同号")]
        public string ContractCode { get; set; } // ContractCode
		/// <summary>主合同号名称</summary>	
		[Description("主合同号名称")]
        public string ContractCodeName { get; set; } // ContractCodeName
		/// <summary>分包合同号</summary>	
		[Description("分包合同号")]
        public string SuppierContractCode { get; set; } // SuppierContractCode
		/// <summary>分包合同号名称</summary>	
		[Description("分包合同号名称")]
        public string SuppierContractCodeName { get; set; } // SuppierContractCodeName
		/// <summary>项目负责人</summary>	
		[Description("项目负责人")]
        public string ChargerUser { get; set; } // ChargerUser
		/// <summary>项目负责人名称</summary>	
		[Description("项目负责人名称")]
        public string ChargerUserName { get; set; } // ChargerUserName
		/// <summary>分包方</summary>	
		[Description("分包方")]
        public string Suppier { get; set; } // Suppier
		/// <summary>分包方名称</summary>	
		[Description("分包方名称")]
        public string SuppierName { get; set; } // SuppierName
		/// <summary>涉及专业</summary>	
		[Description("涉及专业")]
        public string Majors { get; set; } // Majors
		/// <summary>分包设计产品名称</summary>	
		[Description("分包设计产品名称")]
        public string ProductName { get; set; } // ProductName
		/// <summary>签收人</summary>	
		[Description("签收人")]
        public string Receiver { get; set; } // Receiver
		/// <summary>签收人名称</summary>	
		[Description("签收人名称")]
        public string ReceiverName { get; set; } // ReceiverName
		/// <summary>签收日期</summary>	
		[Description("签收日期")]
        public DateTime? ReceiveDate { get; set; } // ReceiveDate
		/// <summary>验证意见</summary>	
		[Description("验证意见")]
        public string ValidationSelection { get; set; } // ValidationSelection
		/// <summary>评分</summary>	
		[Description("评分")]
        public decimal? Evaluate { get; set; } // Evaluate
		/// <summary>问题及建议</summary>	
		[Description("问题及建议")]
        public string Suggestions { get; set; } // Suggestions
		/// <summary>相关专业负责人签字</summary>	
		[Description("相关专业负责人签字")]
        public string MajorChargersSign { get; set; } // MajorChargersSign
		/// <summary>项目负责人签字</summary>	
		[Description("项目负责人签字")]
        public string ProjectChargerSign { get; set; } // ProjectChargerSign
		/// <summary>设计总负责人签字</summary>	
		[Description("设计总负责人签字")]
        public string DesignChargerSign { get; set; } // DesignChargerSign
    }

	/// <summary>付款申请</summary>	
	[Description("付款申请")]
    public partial class T_SP_PaymentApply : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary>付款申请日期</summary>	
		[Description("付款申请日期")]
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
		/// <summary>付款编号</summary>	
		[Description("付款编号")]
        public string SerialNumber { get; set; } // SerialNumber
		/// <summary>合作单位</summary>	
		[Description("合作单位")]
        public string Supplier { get; set; } // Supplier
		/// <summary>合作单位名称</summary>	
		[Description("合作单位名称")]
        public string SupplierName { get; set; } // SupplierName
		/// <summary>分包合同</summary>	
		[Description("分包合同")]
        public string Contract { get; set; } // Contract
		/// <summary>分包合同名称</summary>	
		[Description("分包合同名称")]
        public string ContractName { get; set; } // ContractName
		/// <summary></summary>	
		[Description("")]
        public string CostUnit { get; set; } // CostUnit
		/// <summary></summary>	
		[Description("")]
        public string CostUnitName { get; set; } // CostUnitName
		/// <summary>经办人</summary>	
		[Description("经办人")]
        public string PaymentUser { get; set; } // PaymentUser
		/// <summary>经办人名称</summary>	
		[Description("经办人名称")]
        public string PaymentUserName { get; set; } // PaymentUserName
		/// <summary>付款日期</summary>	
		[Description("付款日期")]
        public DateTime? PaymentDate { get; set; } // PaymentDate
		/// <summary>实付总金额</summary>	
		[Description("实付总金额")]
        public decimal? PaymentValue { get; set; } // PaymentValue
		/// <summary>备注</summary>	
		[Description("备注")]
        public string Remark { get; set; } // Remark
		/// <summary>部门领导审批</summary>	
		[Description("部门领导审批")]
        public string DeptSign { get; set; } // DeptSign
		/// <summary>院领导审批</summary>	
		[Description("院领导审批")]
        public string CompanySign { get; set; } // CompanySign
		/// <summary>财务付款</summary>	
		[Description("财务付款")]
        public string FinanceSign { get; set; } // FinanceSign
		/// <summary>已付款金额（元）</summary>	
		[Description("已付款金额（元）")]
        public decimal? SumPaymentValue { get; set; } // SumPaymentValue
		/// <summary>合同总金额（元）</summary>	
		[Description("合同总金额（元）")]
        public decimal? ContractValue { get; set; } // ContractValue
		/// <summary>申请付款金额（元）</summary>	
		[Description("申请付款金额（元）")]
        public decimal? ApplyValue { get; set; } // ApplyValue
		/// <summary>主合同</summary>	
		[Description("主合同")]
        public string ManagerContract { get; set; } // ManagerContract
		/// <summary>主合同名称</summary>	
		[Description("主合同名称")]
        public string ManagerContractName { get; set; } // ManagerContractName
		/// <summary>关联发票</summary>	
		[Description("关联发票")]
        public string InvoiceRelation { get; set; } // InvoiceRelation
		/// <summary>承兑汇票支付</summary>	
		[Description("承兑汇票支付")]
        public string AcceptanceBill { get; set; } // AcceptanceBill
		/// <summary>承兑汇票金额</summary>	
		[Description("承兑汇票金额")]
        public decimal? BillValue { get; set; } // BillValue
		/// <summary>付款金额</summary>	
		[Description("付款金额")]
        public decimal? PaymentWithoutBill { get; set; } // PaymentWithoutBill
		/// <summary>费用类型</summary>	
		[Description("费用类型")]
        public string ExpenseType { get; set; } // ExpenseType
		/// <summary>成本明细</summary>	
		[Description("成本明细")]
        public string CostInfo { get; set; } // CostInfo
		/// <summary>供方银行账号</summary>	
		[Description("供方银行账号")]
        public string BankAccount { get; set; } // BankAccount
		/// <summary>供方银行账号名称</summary>	
		[Description("供方银行账号名称")]
        public string BankAccountName { get; set; } // BankAccountName
		/// <summary>供方开户银行</summary>	
		[Description("供方开户银行")]
        public string BankName { get; set; } // BankName

        // Reverse navigation
		[JsonIgnore]
        public virtual ICollection<T_SP_PaymentApply_AcceptanceBill> T_SP_PaymentApply_AcceptanceBill { get { onT_SP_PaymentApply_AcceptanceBillGetting(); return _T_SP_PaymentApply_AcceptanceBill;} }
		private ICollection<T_SP_PaymentApply_AcceptanceBill> _T_SP_PaymentApply_AcceptanceBill;
		partial void onT_SP_PaymentApply_AcceptanceBillGetting();

		[JsonIgnore]
        public virtual ICollection<T_SP_PaymentApply_CostInfo> T_SP_PaymentApply_CostInfo { get { onT_SP_PaymentApply_CostInfoGetting(); return _T_SP_PaymentApply_CostInfo;} }
		private ICollection<T_SP_PaymentApply_CostInfo> _T_SP_PaymentApply_CostInfo;
		partial void onT_SP_PaymentApply_CostInfoGetting();

		[JsonIgnore]
        public virtual ICollection<T_SP_PaymentApply_InvoiceRelation> T_SP_PaymentApply_InvoiceRelation { get { onT_SP_PaymentApply_InvoiceRelationGetting(); return _T_SP_PaymentApply_InvoiceRelation;} }
		private ICollection<T_SP_PaymentApply_InvoiceRelation> _T_SP_PaymentApply_InvoiceRelation;
		partial void onT_SP_PaymentApply_InvoiceRelationGetting();


        public T_SP_PaymentApply()
        {
            _T_SP_PaymentApply_AcceptanceBill = new List<T_SP_PaymentApply_AcceptanceBill>();
            _T_SP_PaymentApply_CostInfo = new List<T_SP_PaymentApply_CostInfo>();
            _T_SP_PaymentApply_InvoiceRelation = new List<T_SP_PaymentApply_InvoiceRelation>();
        }
    }

	/// <summary>承兑汇票支付</summary>	
	[Description("承兑汇票支付")]
    public partial class T_SP_PaymentApply_AcceptanceBill : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string T_SP_PaymentApplyID { get; set; } // T_SP_PaymentApplyID
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string IsReleased { get; set; } // IsReleased
		/// <summary>汇票号</summary>	
		[Description("汇票号")]
        public string SerialNumber { get; set; } // SerialNumber
		/// <summary>付款单位</summary>	
		[Description("付款单位")]
        public string CustomerName { get; set; } // CustomerName
		/// <summary>开票日期</summary>	
		[Description("开票日期")]
        public DateTime? BillDate { get; set; } // BillDate
		/// <summary>汇票金额(元)</summary>	
		[Description("汇票金额(元)")]
        public decimal? Amount { get; set; } // Amount
		/// <summary>AcceptanceBillID</summary>	
		[Description("AcceptanceBillID")]
        public string AcceptanceBillID { get; set; } // AcceptanceBillID

        // Foreign keys
		[JsonIgnore]
        public virtual T_SP_PaymentApply T_SP_PaymentApply { get; set; } //  T_SP_PaymentApplyID - FK_T_SP_PaymentApply_AcceptanceBill_T_SP_PaymentApply
    }

	/// <summary>成本明细</summary>	
	[Description("成本明细")]
    public partial class T_SP_PaymentApply_CostInfo : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string T_SP_PaymentApplyID { get; set; } // T_SP_PaymentApplyID
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
		/// <summary>合同金额（元）</summary>	
		[Description("合同金额（元）")]
        public string ContractValue { get; set; } // ContractValue
		/// <summary>付款金额（元）</summary>	
		[Description("付款金额（元）")]
        public decimal? CostValue { get; set; } // CostValue
		/// <summary>ContractCostUnitID</summary>	
		[Description("ContractCostUnitID")]
        public string ContractCostUnitID { get; set; } // ContractCostUnitID
		/// <summary>CostUnitID</summary>	
		[Description("CostUnitID")]
        public string CostUnitID { get; set; } // CostUnitID
		/// <summary>SubjectCode</summary>	
		[Description("SubjectCode")]
        public string SubjectCode { get; set; } // SubjectCode
		/// <summary>已分摊金额（元）</summary>	
		[Description("已分摊金额（元）")]
        public decimal? CostValueDistribute { get; set; } // CostValueDistribute

        // Foreign keys
		[JsonIgnore]
        public virtual T_SP_PaymentApply T_SP_PaymentApply { get; set; } //  T_SP_PaymentApplyID - FK_T_SP_PaymentApply_CostInfo_T_SP_PaymentApply
    }

	/// <summary>关联发票</summary>	
	[Description("关联发票")]
    public partial class T_SP_PaymentApply_InvoiceRelation : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary></summary>	
		[Description("")]
        public string T_SP_PaymentApplyID { get; set; } // T_SP_PaymentApplyID
		/// <summary></summary>	
		[Description("")]
        public double? SortIndex { get; set; } // SortIndex
		/// <summary></summary>	
		[Description("")]
        public string IsReleased { get; set; } // IsReleased
		/// <summary>发票编号</summary>	
		[Description("发票编号")]
        public string InvoiceNo { get; set; } // InvoiceNo
		/// <summary>开票单位</summary>	
		[Description("开票单位")]
        public string InvoiceUnit { get; set; } // InvoiceUnit
		/// <summary>已对应金额（元）</summary>	
		[Description("已对应金额（元）")]
        public decimal? InvoiceRelationValue { get; set; } // InvoiceRelationValue
		/// <summary>可对应金额（元）</summary>	
		[Description("可对应金额（元）")]
        public decimal? RemainInvoiceValue { get; set; } // RemainInvoiceValue
		/// <summary>本次对应金额（元）</summary>	
		[Description("本次对应金额（元）")]
        public decimal? RelationValue { get; set; } // RelationValue
		/// <summary>InvoiceID</summary>	
		[Description("InvoiceID")]
        public string InvoiceID { get; set; } // InvoiceID

        // Foreign keys
		[JsonIgnore]
        public virtual T_SP_PaymentApply T_SP_PaymentApply { get; set; } //  T_SP_PaymentApplyID - FK_T_SP_PaymentApply_InvoiceRelation_T_SP_PaymentApply
    }

	/// <summary>委外付款确认</summary>	
	[Description("委外付款确认")]
    public partial class T_SP_PaymentApplyConfirm : Formula.BaseModel
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
		/// <summary>委外合同名称</summary>	
		[Description("委外合同名称")]
        public string SubContractName { get; set; } // SubContractName
		/// <summary>委外合同编号</summary>	
		[Description("委外合同编号")]
        public string SubContractCode { get; set; } // SubContractCode
		/// <summary>项目名称</summary>	
		[Description("项目名称")]
        public string CBSInfoName { get; set; } // CBSInfoName
		/// <summary>项目编号</summary>	
		[Description("项目编号")]
        public string CBSInfoCode { get; set; } // CBSInfoCode
		/// <summary>单元名称</summary>	
		[Description("单元名称")]
        public string CostUnitName { get; set; } // CostUnitName
		/// <summary>单元编号</summary>	
		[Description("单元编号")]
        public string CostUnitCode { get; set; } // CostUnitCode
		/// <summary>负责人</summary>	
		[Description("负责人")]
        public string ChargerUser { get; set; } // ChargerUser
		/// <summary>负责人名称</summary>	
		[Description("负责人名称")]
        public string ChargerUserName { get; set; } // ChargerUserName
		/// <summary>负责部门</summary>	
		[Description("负责部门")]
        public string ChargerDept { get; set; } // ChargerDept
		/// <summary>负责部门名称</summary>	
		[Description("负责部门名称")]
        public string ChargerDeptName { get; set; } // ChargerDeptName
		/// <summary>付款金额</summary>	
		[Description("付款金额")]
        public decimal? PaymentApplyValue { get; set; } // PaymentApplyValue
		/// <summary>上期末累计进度</summary>	
		[Description("上期末累计进度")]
        public decimal? LastProgress { get; set; } // LastProgress
		/// <summary>上期末累计金额</summary>	
		[Description("上期末累计金额")]
        public decimal? LastValue { get; set; } // LastValue
		/// <summary>本期末累计进度</summary>	
		[Description("本期末累计进度")]
        public decimal? TotalProgress { get; set; } // TotalProgress
		/// <summary>本期末累计金额</summary>	
		[Description("本期末累计金额")]
        public decimal? TotalValue { get; set; } // TotalValue
		/// <summary>本期确认金额</summary>	
		[Description("本期确认金额")]
        public decimal? CurrentValue { get; set; } // CurrentValue
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
		/// <summary>附件</summary>	
		[Description("附件")]
        public string Attachment { get; set; } // Attachment
		/// <summary>CBSInfoID</summary>	
		[Description("CBSInfoID")]
        public string CBSInfoID { get; set; } // CBSInfoID
		/// <summary>CostUnitID</summary>	
		[Description("CostUnitID")]
        public string CostUnitID { get; set; } // CostUnitID
		/// <summary>CBSNodeID</summary>	
		[Description("CBSNodeID")]
        public string CBSNodeID { get; set; } // CBSNodeID
		/// <summary>付款登记ID</summary>	
		[Description("付款登记ID")]
        public string PaymentApplyID { get; set; } // PaymentApplyID
		/// <summary>关联科目编码</summary>	
		[Description("关联科目编码")]
        public string SubjectCode { get; set; } // SubjectCode
		/// <summary>结算状态</summary>	
		[Description("结算状态")]
        public string CostState { get; set; } // CostState
		/// <summary>税率</summary>	
		[Description("税率")]
        public decimal? TaxRate { get; set; } // TaxRate
		/// <summary>税金</summary>	
		[Description("税金")]
        public decimal? TaxValue { get; set; } // TaxValue
		/// <summary>去税成本</summary>	
		[Description("去税成本")]
        public decimal? ClearValue { get; set; } // ClearValue
		/// <summary>确认日期</summary>	
		[Description("确认日期")]
        public DateTime? ConfirmDate { get; set; } // ConfirmDate
    }

	/// <summary></summary>	
	[Description("")]
    public partial class view_ContractCustomerList : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ContractType { get; set; } // ContractType
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID
		/// <summary></summary>	
		[Description("")]
        public string SerialNumber { get; set; } // SerialNumber
		/// <summary></summary>	
		[Description("")]
        public string Name { get; set; } // Name
		/// <summary></summary>	
		[Description("")]
        public decimal? ContractRMBAmount { get; set; } // ContractRMBAmount
		/// <summary></summary>	
		[Description("")]
        public decimal? SumInvoiceValue { get; set; } // SumInvoiceValue
		/// <summary></summary>	
		[Description("")]
        public decimal? NoInvoiceAmount { get; set; } // NoInvoiceAmount
		/// <summary></summary>	
		[Description("")]
        public decimal? SumReceiptValue { get; set; } // SumReceiptValue
		/// <summary></summary>	
		[Description("")]
        public string PartyA { get; set; } // PartyA
		/// <summary></summary>	
		[Description("")]
        public string PartyAName { get; set; } // PartyAName
		/// <summary></summary>	
		[Description("")]
        public string ProductionDeptName { get; set; } // ProductionDeptName
		/// <summary></summary>	
		[Description("")]
        public string BusinessDeptName { get; set; } // BusinessDeptName
		/// <summary></summary>	
		[Description("")]
        public decimal? SumNoReceiptValue { get; set; } // SumNoReceiptValue
		/// <summary></summary>	
		[Description("")]
        public string InvoiceName { get; set; } // InvoiceName
		/// <summary></summary>	
		[Description("")]
        public string BankAccounts { get; set; } // BankAccounts
		/// <summary></summary>	
		[Description("")]
        public string BankName { get; set; } // BankName
		/// <summary></summary>	
		[Description("")]
        public string TaxAccounts { get; set; } // TaxAccounts
		/// <summary></summary>	
		[Description("")]
        public string Address { get; set; } // Address
		/// <summary></summary>	
		[Description("")]
        public string LinkTelphone { get; set; } // LinkTelphone
		/// <summary></summary>	
		[Description("")]
        public string CustomerFullID { get; set; } // CustomerFullID
		/// <summary></summary>	
		[Description("")]
        public string CustomerFullIDName { get; set; } // CustomerFullIDName
    }

	/// <summary></summary>	
	[Description("")]
    public partial class view_S_B_BidFileInfo : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ProjectName { get; set; } // ProjectName
		/// <summary></summary>	
		[Description("")]
        public string Files { get; set; } // Files
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID
		/// <summary></summary>	
		[Description("")]
        public string EngineeringInfo { get; set; } // EngineeringInfo
		/// <summary></summary>	
		[Description("")]
        public string EngineeringInfoName { get; set; } // EngineeringInfoName
		/// <summary></summary>	
		[Description("")]
        public string ProjectManager { get; set; } // ProjectManager
		/// <summary></summary>	
		[Description("")]
        public string ProjectManagerName { get; set; } // ProjectManagerName
    }

	/// <summary></summary>	
	[Description("")]
    public partial class view_S_B_ContractFileInfo : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID
		/// <summary></summary>	
		[Description("")]
        public string Name { get; set; } // Name
		/// <summary></summary>	
		[Description("")]
        public string EngineeringInfo { get; set; } // EngineeringInfo
		/// <summary></summary>	
		[Description("")]
        public string EngineeringInfoName { get; set; } // EngineeringInfoName
		/// <summary></summary>	
		[Description("")]
        public string Files { get; set; } // Files
		/// <summary></summary>	
		[Description("")]
        public string SerialNumber { get; set; } // SerialNumber
    }

	/// <summary></summary>	
	[Description("")]
    public partial class view_S_B_MarketClueFileInfo : Formula.BaseModel
    {
		/// <summary></summary>	
		[Description("")]
        public string ID { get; set; } // ID
		/// <summary></summary>	
		[Description("")]
        public string ProjectName { get; set; } // ProjectName
		/// <summary></summary>	
		[Description("")]
        public string ProjectCode { get; set; } // ProjectCode
		/// <summary></summary>	
		[Description("")]
        public string EngineeringInfo { get; set; } // EngineeringInfo
		/// <summary></summary>	
		[Description("")]
        public string EngineeringInfoName { get; set; } // EngineeringInfoName
		/// <summary></summary>	
		[Description("")]
        public string Files { get; set; } // Files
    }


    // ************************************************************************
    // POCO Configuration

    // S_B_Bid
    internal partial class S_B_BidConfiguration : EntityTypeConfiguration<S_B_Bid>
    {
        public S_B_BidConfiguration()
        {
            ToTable("S_B_BID");
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
            Property(x => x.BidCode).HasColumnName("BIDCODE").IsOptional().HasMaxLength(200);
            Property(x => x.BidPhase).HasColumnName("BIDPHASE").IsOptional().HasMaxLength(200);
            Property(x => x.EngineeringInfo).HasColumnName("ENGINEERINGINFO").IsOptional().HasMaxLength(200);
            Property(x => x.EngineeringInfoName).HasColumnName("ENGINEERINGINFONAME").IsOptional().HasMaxLength(200);
            Property(x => x.BidIssuedDate).HasColumnName("BIDISSUEDDATE").IsOptional();
            Property(x => x.CompetencyDate).HasColumnName("COMPETENCYDATE").IsOptional();
            Property(x => x.TenderCode).HasColumnName("TENDERCODE").IsOptional().HasMaxLength(200);
            Property(x => x.TenderOrgan).HasColumnName("TENDERORGAN").IsOptional().HasMaxLength(200);
            Property(x => x.BusinessClass).HasColumnName("BUSINESSCLASS").IsOptional().HasMaxLength(200);
            Property(x => x.BusinessOwner).HasColumnName("BUSINESSOWNER").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectManager).HasColumnName("PROJECTMANAGER").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectManagerName).HasColumnName("PROJECTMANAGERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.DeptInfo).HasColumnName("DEPTINFO").IsOptional().HasMaxLength(200);
            Property(x => x.DeptInfoName).HasColumnName("DEPTINFONAME").IsOptional().HasMaxLength(200);
            Property(x => x.CompleteBidDate).HasColumnName("COMPLETEBIDDATE").IsOptional();
            Property(x => x.PostCode).HasColumnName("POSTCODE").IsOptional().HasMaxLength(200);
            Property(x => x.Address).HasColumnName("ADDRESS").IsOptional().HasMaxLength(200);
            Property(x => x.Remark).HasColumnName("REMARK").IsOptional().HasMaxLength(500);
            Property(x => x.BidSurrogateUnit).HasColumnName("BIDSURROGATEUNIT").IsOptional().HasMaxLength(200);
            Property(x => x.BidAddress).HasColumnName("BIDADDRESS").IsOptional().HasMaxLength(200);
            Property(x => x.BidType).HasColumnName("BIDTYPE").IsOptional().HasMaxLength(200);
            Property(x => x.BidFilePrice).HasColumnName("BIDFILEPRICE").IsOptional().HasPrecision(18,4);
            Property(x => x.EnsureAmount).HasColumnName("ENSUREAMOUNT").IsOptional().HasPrecision(18,4);
            Property(x => x.EnsureAmountIsReturn).HasColumnName("ENSUREAMOUNTISRETURN").IsOptional().HasMaxLength(200);
            Property(x => x.EnsureAmountReturnDate).HasColumnName("ENSUREAMOUNTRETURNDATE").IsOptional();
            Property(x => x.ResultUnit).HasColumnName("RESULTUNIT").IsOptional().HasMaxLength(200);
            Property(x => x.TradeAmount).HasColumnName("TRADEAMOUNT").IsOptional().HasPrecision(18,4);
            Property(x => x.TradeUnitAmount).HasColumnName("TRADEUNITAMOUNT").IsOptional().HasPrecision(18,4);
            Property(x => x.BidUntiRemark).HasColumnName("BIDUNTIREMARK").IsOptional();
            Property(x => x.BidResult).HasColumnName("BIDRESULT").IsOptional().HasMaxLength(500);
            Property(x => x.IsWinBid).HasColumnName("ISWINBID").IsOptional().HasMaxLength(200);
            Property(x => x.InviteBidFile).HasColumnName("INVITEBIDFILE").IsOptional().HasMaxLength(500);
            Property(x => x.InviteBidClearFile).HasColumnName("INVITEBIDCLEARFILE").IsOptional().HasMaxLength(500);
            Property(x => x.InviteBidOtherFile).HasColumnName("INVITEBIDOTHERFILE").IsOptional().HasMaxLength(500);
            Property(x => x.BidFile).HasColumnName("BIDFILE").IsOptional().HasMaxLength(500);
            Property(x => x.BidClearFile).HasColumnName("BIDCLEARFILE").IsOptional().HasMaxLength(500);
            Property(x => x.BidOtherFile).HasColumnName("BIDOTHERFILE").IsOptional().HasMaxLength(500);
            Property(x => x.Project).HasColumnName("PROJECT").IsOptional().HasMaxLength(200);
            Property(x => x.BusinessOwnerName).HasColumnName("BUSINESSOWNERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Bond).HasColumnName("BOND").IsOptional();
        }
    }

    // S_B_Bond
    internal partial class S_B_BondConfiguration : EntityTypeConfiguration<S_B_Bond>
    {
        public S_B_BondConfiguration()
        {
            ToTable("S_B_BOND");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.Project).HasColumnName("PROJECT").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.SerialNumber).HasColumnName("SERIALNUMBER").IsOptional().HasMaxLength(200);
            Property(x => x.Amount).HasColumnName("AMOUNT").IsOptional().HasPrecision(18,4);
            Property(x => x.ReturnAmountTotal).HasColumnName("RETURNAMOUNTTOTAL").IsOptional().HasPrecision(18,4);
            Property(x => x.ExpiryDate).HasColumnName("EXPIRYDATE").IsOptional();
            Property(x => x.ApplierName).HasColumnName("APPLIERNAME").IsOptional().HasMaxLength(50);
            Property(x => x.DepartInfoName).HasColumnName("DEPARTINFONAME").IsOptional().HasMaxLength(50);
            Property(x => x.Applier).HasColumnName("APPLIER").IsOptional().HasMaxLength(50);
            Property(x => x.DepartInfo).HasColumnName("DEPARTINFO").IsOptional().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.ModifyDate).HasColumnName("MODIFYDATE").IsOptional();
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUser).HasColumnName("MODIFYUSER").IsOptional().HasMaxLength(50);
            Property(x => x.OrgID).HasColumnName("ORGID").IsOptional().HasMaxLength(50);
            Property(x => x.CompanyID).HasColumnName("COMPANYID").IsOptional().HasMaxLength(50);
            Property(x => x.ProjectName).HasColumnName("PROJECTNAME").IsOptional().HasMaxLength(50);
            Property(x => x.ApplyDate).HasColumnName("APPLYDATE").IsOptional();
            Property(x => x.ProjectCode).HasColumnName("PROJECTCODE").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectMangerName).HasColumnName("PROJECTMANGERNAME").IsOptional().HasMaxLength(50);
            Property(x => x.TransferUnit).HasColumnName("TRANSFERUNIT").IsOptional().HasMaxLength(200);
            Property(x => x.BankName).HasColumnName("BANKNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Account).HasColumnName("ACCOUNT").IsOptional().HasMaxLength(200);
            Property(x => x.Remark).HasColumnName("REMARK").IsOptional().HasMaxLength(500);
            Property(x => x.Attachment).HasColumnName("ATTACHMENT").IsOptional().HasMaxLength(500);
            Property(x => x.ProjectManager).HasColumnName("PROJECTMANAGER").IsOptional().HasMaxLength(200);
            Property(x => x.State).HasColumnName("STATE").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectManger).HasColumnName("PROJECTMANGER").IsOptional().HasMaxLength(50);
            Property(x => x.BondType).HasColumnName("BONDTYPE").IsOptional().HasMaxLength(50);
        }
    }

    // S_B_BondReturn
    internal partial class S_B_BondReturnConfiguration : EntityTypeConfiguration<S_B_BondReturn>
    {
        public S_B_BondReturnConfiguration()
        {
            ToTable("S_B_BONDRETURN");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.ModifyDate).HasColumnName("MODIFYDATE").IsOptional();
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUser).HasColumnName("MODIFYUSER").IsOptional().HasMaxLength(50);
            Property(x => x.BondID).HasColumnName("BONDID").IsOptional().HasMaxLength(200);
            Property(x => x.Amount).HasColumnName("AMOUNT").IsOptional().HasPrecision(18,4);
            Property(x => x.LastReturnAmountTotal).HasColumnName("LASTRETURNAMOUNTTOTAL").IsOptional().HasPrecision(18,4);
            Property(x => x.ReturnAmount).HasColumnName("RETURNAMOUNT").IsOptional().HasPrecision(18,4);
            Property(x => x.ReturnAmountTotal).HasColumnName("RETURNAMOUNTTOTAL").IsOptional().HasPrecision(18,4);
            Property(x => x.PerformanceAmount).HasColumnName("PERFORMANCEAMOUNT").IsOptional().HasPrecision(18,4);
        }
    }

    // S_B_PerformanceBond
    internal partial class S_B_PerformanceBondConfiguration : EntityTypeConfiguration<S_B_PerformanceBond>
    {
        public S_B_PerformanceBondConfiguration()
        {
            ToTable("S_B_PERFORMANCEBOND");
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
            Property(x => x.SerialNumber).HasColumnName("SERIALNUMBER").IsOptional().HasMaxLength(200);
            Property(x => x.ApplyDate).HasColumnName("APPLYDATE").IsOptional();
            Property(x => x.Contract).HasColumnName("CONTRACT").IsOptional().HasMaxLength(200);
            Property(x => x.ContractName).HasColumnName("CONTRACTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ContractCode).HasColumnName("CONTRACTCODE").IsOptional().HasMaxLength(200);
            Property(x => x.Project).HasColumnName("PROJECT").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectName).HasColumnName("PROJECTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectCode).HasColumnName("PROJECTCODE").IsOptional().HasMaxLength(200);
            Property(x => x.ApplyAmount).HasColumnName("APPLYAMOUNT").IsOptional().HasPrecision(18,4);
            Property(x => x.ApplyAmountDX).HasColumnName("APPLYAMOUNTDX").IsOptional().HasMaxLength(200);
            Property(x => x.RemittanceMethod).HasColumnName("REMITTANCEMETHOD").IsOptional().HasMaxLength(200);
            Property(x => x.CollectionAccount).HasColumnName("COLLECTIONACCOUNT").IsOptional().HasMaxLength(200);
            Property(x => x.BankName).HasColumnName("BANKNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Payee).HasColumnName("PAYEE").IsOptional().HasMaxLength(200);
            Property(x => x.PayeeName).HasColumnName("PAYEENAME").IsOptional().HasMaxLength(200);
            Property(x => x.PayeeAddress).HasColumnName("PAYEEADDRESS").IsOptional().HasMaxLength(200);
            Property(x => x.LinkMan).HasColumnName("LINKMAN").IsOptional().HasMaxLength(200);
            Property(x => x.LinkManPhone).HasColumnName("LINKMANPHONE").IsOptional().HasMaxLength(200);
            Property(x => x.ExpectedReturnDate).HasColumnName("EXPECTEDRETURNDATE").IsOptional();
            Property(x => x.RequestPaymentDate).HasColumnName("REQUESTPAYMENTDATE").IsOptional();
            Property(x => x.IsSendBack).HasColumnName("ISSENDBACK").IsOptional().HasMaxLength(200);
            Property(x => x.SendBackDate).HasColumnName("SENDBACKDATE").IsOptional();
            Property(x => x.SendBackUser).HasColumnName("SENDBACKUSER").IsOptional().HasMaxLength(200);
            Property(x => x.SendBackUserName).HasColumnName("SENDBACKUSERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Attachment).HasColumnName("ATTACHMENT").IsOptional().HasMaxLength(500);
            Property(x => x.Remarks).HasColumnName("REMARKS").IsOptional().HasMaxLength(500);
        }
    }

    // S_C_AcceptanceBill
    internal partial class S_C_AcceptanceBillConfiguration : EntityTypeConfiguration<S_C_AcceptanceBill>
    {
        public S_C_AcceptanceBillConfiguration()
        {
            ToTable("S_C_ACCEPTANCEBILL");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.ReceiptIDs).HasColumnName("RECEIPTIDS").IsOptional().HasMaxLength(500);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.ModifyDate).HasColumnName("MODIFYDATE").IsOptional();
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUser).HasColumnName("MODIFYUSER").IsOptional().HasMaxLength(50);
            Property(x => x.OrgID).HasColumnName("ORGID").IsOptional().HasMaxLength(50);
            Property(x => x.CompanyID).HasColumnName("COMPANYID").IsOptional().HasMaxLength(50);
            Property(x => x.Customer).HasColumnName("CUSTOMER").IsOptional().HasMaxLength(50);
            Property(x => x.CustomerName).HasColumnName("CUSTOMERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Amount).HasColumnName("AMOUNT").IsOptional().HasPrecision(18,2);
            Property(x => x.AmountDX).HasColumnName("AMOUNTDX").IsOptional().HasMaxLength(200);
            Property(x => x.SerialNumber).HasColumnName("SERIALNUMBER").IsOptional().HasMaxLength(200);
            Property(x => x.BillDate).HasColumnName("BILLDATE").IsOptional();
            Property(x => x.Contract).HasColumnName("CONTRACT").IsOptional();
            Property(x => x.Invoice).HasColumnName("INVOICE").IsOptional();
            Property(x => x.PlanReceipt).HasColumnName("PLANRECEIPT").IsOptional();
            Property(x => x.State).HasColumnName("STATE").IsOptional().HasMaxLength(200);
            Property(x => x.Receiver).HasColumnName("RECEIVER").IsOptional().HasMaxLength(50);
            Property(x => x.ReceiverName).HasColumnName("RECEIVERNAME").IsOptional().HasMaxLength(50);
            Property(x => x.ExpireDate).HasColumnName("EXPIREDATE").IsOptional();
            Property(x => x.Remark).HasColumnName("REMARK").IsOptional().HasMaxLength(500);
        }
    }

    // S_C_Deposit
    internal partial class S_C_DepositConfiguration : EntityTypeConfiguration<S_C_Deposit>
    {
        public S_C_DepositConfiguration()
        {
            ToTable("S_C_DEPOSIT");
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
            Property(x => x.ProjectInfo).HasColumnName("PROJECTINFO").IsOptional().HasMaxLength(50);
            Property(x => x.ProjectInfoName).HasColumnName("PROJECTINFONAME").IsOptional().HasMaxLength(500);
            Property(x => x.Amount).HasColumnName("AMOUNT").IsOptional();
            Property(x => x.IsReturn).HasColumnName("ISRETURN").IsOptional().HasMaxLength(50);
            Property(x => x.Purpose).HasColumnName("PURPOSE").IsOptional().HasMaxLength(200);
            Property(x => x.ReturnDate).HasColumnName("RETURNDATE").IsOptional();
        }
    }

    // S_C_Invoice
    internal partial class S_C_InvoiceConfiguration : EntityTypeConfiguration<S_C_Invoice>
    {
        public S_C_InvoiceConfiguration()
        {
            ToTable("S_C_INVOICE");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.ContractInfoID).HasColumnName("CONTRACTINFOID").IsOptional().HasMaxLength(50);
            Property(x => x.ContractCode).HasColumnName("CONTRACTCODE").IsOptional().HasMaxLength(500);
            Property(x => x.ContractName).HasColumnName("CONTRACTNAME").IsOptional().HasMaxLength(500);
            Property(x => x.PayerUnitID).HasColumnName("PAYERUNITID").IsOptional().HasMaxLength(200);
            Property(x => x.PayerUnit).HasColumnName("PAYERUNIT").IsOptional().HasMaxLength(200);
            Property(x => x.CustomerFullID).HasColumnName("CUSTOMERFULLID").IsOptional().HasMaxLength(200);
            Property(x => x.InvoiceCode).HasColumnName("INVOICECODE").IsOptional().HasMaxLength(50);
            Property(x => x.InvoiceNo).HasColumnName("INVOICENO").IsOptional().HasMaxLength(50);
            Property(x => x.InvoiceType).HasColumnName("INVOICETYPE").IsOptional().HasMaxLength(200);
            Property(x => x.ReceviedUnit).HasColumnName("RECEVIEDUNIT").IsOptional().HasMaxLength(200);
            Property(x => x.ReceviedUnitID).HasColumnName("RECEVIEDUNITID").IsOptional().HasMaxLength(200);
            Property(x => x.IssuedDate).HasColumnName("ISSUEDDATE").IsOptional();
            Property(x => x.TaxRate).HasColumnName("TAXRATE").IsOptional().HasPrecision(18,4);
            Property(x => x.TaxesAmount).HasColumnName("TAXESAMOUNT").IsOptional().HasPrecision(18,4);
            Property(x => x.ClearAmount).HasColumnName("CLEARAMOUNT").IsOptional().HasPrecision(18,4);
            Property(x => x.Amount).HasColumnName("AMOUNT").IsOptional().HasPrecision(18,4);
            Property(x => x.AdditionalTaxesAmount).HasColumnName("ADDITIONALTAXESAMOUNT").IsOptional().HasPrecision(18,4);
            Property(x => x.InvoiceMaker).HasColumnName("INVOICEMAKER").IsOptional().HasMaxLength(200);
            Property(x => x.InvoiceMakerID).HasColumnName("INVOICEMAKERID").IsOptional().HasMaxLength(200);
            Property(x => x.InvoiceDate).HasColumnName("INVOICEDATE").IsOptional();
            Property(x => x.Remark).HasColumnName("REMARK").IsOptional().HasMaxLength(500);
            Property(x => x.Attachment).HasColumnName("ATTACHMENT").IsOptional();
            Property(x => x.BelongYear).HasColumnName("BELONGYEAR").IsOptional();
            Property(x => x.BelongQuarter).HasColumnName("BELONGQUARTER").IsOptional();
            Property(x => x.BelongMonth).HasColumnName("BELONGMONTH").IsOptional();
            Property(x => x.State).HasColumnName("STATE").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsOptional().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUser).HasColumnName("MODIFYUSER").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyDate).HasColumnName("MODIFYDATE").IsOptional();
            Property(x => x.OrgID).HasColumnName("ORGID").IsOptional().HasMaxLength(500);
            Property(x => x.OrgName).HasColumnName("ORGNAME").IsOptional().HasMaxLength(500);
            Property(x => x.BankName).HasColumnName("BANKNAME").IsOptional().HasMaxLength(200);
            Property(x => x.BankAccount).HasColumnName("BANKACCOUNT").IsOptional().HasMaxLength(200);
            Property(x => x.TaxAccount).HasColumnName("TAXACCOUNT").IsOptional().HasMaxLength(200);
            Property(x => x.InvoiceName).HasColumnName("INVOICENAME").IsOptional().HasMaxLength(200);
            Property(x => x.ContractInfoIDName).HasColumnName("CONTRACTINFOIDNAME").IsOptional().HasMaxLength(50);
            Property(x => x.PayerUnitIDName).HasColumnName("PAYERUNITIDNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ChineseAmount).HasColumnName("CHINESEAMOUNT").IsOptional().HasMaxLength(200);
            Property(x => x.InvoiceMakerIDName).HasColumnName("INVOICEMAKERIDNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ReceiptObject).HasColumnName("RECEIPTOBJECT").IsOptional();
            Property(x => x.InvoiceDatail).HasColumnName("INVOICEDATAIL").IsOptional();
            Property(x => x.TaxName).HasColumnName("TAXNAME").IsOptional().HasMaxLength(50);
            Property(x => x.IncomeUnit).HasColumnName("INCOMEUNIT").IsOptional().HasMaxLength(50);
            Property(x => x.IncomeUnitName).HasColumnName("INCOMEUNITNAME").IsOptional().HasMaxLength(50);
            Property(x => x.IncomeUnitCode).HasColumnName("INCOMEUNITCODE").IsOptional().HasMaxLength(200);
            Property(x => x.CustomerID).HasColumnName("CUSTOMERID").IsOptional().HasMaxLength(200);
            Property(x => x.CustomerIDName).HasColumnName("CUSTOMERIDNAME").IsOptional().HasMaxLength(200);

            // Foreign keys
            HasOptional(a => a.S_C_ManageContract).WithMany(b => b.S_C_Invoice).HasForeignKey(c => c.ContractInfoID); // FK_S_C_Invoice_S_C_ManageContract
        }
    }

    // S_C_Invoice_InvoiceDatail
    internal partial class S_C_Invoice_InvoiceDatailConfiguration : EntityTypeConfiguration<S_C_Invoice_InvoiceDatail>
    {
        public S_C_Invoice_InvoiceDatailConfiguration()
        {
            ToTable("S_C_INVOICE_INVOICEDATAIL");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.S_C_InvoiceID).HasColumnName("S_C_INVOICEID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(200);
            Property(x => x.InvoiceAmount).HasColumnName("INVOICEAMOUNT").IsOptional().HasPrecision(18,2);
            Property(x => x.InvoiceState).HasColumnName("INVOICESTATE").IsOptional().HasMaxLength(200);

            // Foreign keys
            HasOptional(a => a.S_C_Invoice).WithMany(b => b.S_C_Invoice_InvoiceDatail).HasForeignKey(c => c.S_C_InvoiceID); // FK_S_C_Invoice_InvoiceDatail_S_C_Invoice
        }
    }

    // S_C_Invoice_ReceiptObject
    internal partial class S_C_Invoice_ReceiptObjectConfiguration : EntityTypeConfiguration<S_C_Invoice_ReceiptObject>
    {
        public S_C_Invoice_ReceiptObjectConfiguration()
        {
            ToTable("S_C_INVOICE_RECEIPTOBJECT");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.S_C_InvoiceID).HasColumnName("S_C_INVOICEID").IsRequired().HasMaxLength(50);
            Property(x => x.ReceiptObjectID).HasColumnName("RECEIPTOBJECTID").IsOptional().HasMaxLength(50);
            Property(x => x.ContractInfoID).HasColumnName("CONTRACTINFOID").IsOptional().HasMaxLength(50);
            Property(x => x.RelationValue).HasColumnName("RELATIONVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUserName).HasColumnName("MODIFYUSERNAME").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyDate).HasColumnName("MODIFYDATE").IsOptional();
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(200);
            Property(x => x.ReceiptValue).HasColumnName("RECEIPTVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.SumRelationValue).HasColumnName("SUMRELATIONVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);

            // Foreign keys
            HasRequired(a => a.S_C_Invoice).WithMany(b => b.S_C_Invoice_ReceiptObject).HasForeignKey(c => c.S_C_InvoiceID); // FK_S_C_InvoiceReceiptObjectRelation_S_C_Invoice
            HasOptional(a => a.S_C_ManageContract_ReceiptObj).WithMany(b => b.S_C_Invoice_ReceiptObject).HasForeignKey(c => c.ReceiptObjectID); // FK_S_C_InvoiceReceiptObjectRelation_S_C_ManageContract_ReceiptObj
        }
    }

    // S_C_InvoiceReceiptRelation
    internal partial class S_C_InvoiceReceiptRelationConfiguration : EntityTypeConfiguration<S_C_InvoiceReceiptRelation>
    {
        public S_C_InvoiceReceiptRelationConfiguration()
        {
            ToTable("S_C_INVOICERECEIPTRELATION");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.InvoiceID).HasColumnName("INVOICEID").IsRequired().HasMaxLength(50);
            Property(x => x.ReceiptID).HasColumnName("RECEIPTID").IsRequired().HasMaxLength(50);
            Property(x => x.ContractInfoID).HasColumnName("CONTRACTINFOID").IsRequired().HasMaxLength(50);
            Property(x => x.RelationValue).HasColumnName("RELATIONVALUE").IsRequired().HasPrecision(18,4);
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUser).HasColumnName("MODIFYUSER").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyDate).HasColumnName("MODIFYDATE").IsOptional();

            // Foreign keys
            HasRequired(a => a.S_C_Invoice).WithMany(b => b.S_C_InvoiceReceiptRelation).HasForeignKey(c => c.InvoiceID); // FK_S_C_InvoiceReciptRelation_S_C_Invoice
            HasRequired(a => a.S_C_Receipt).WithMany(b => b.S_C_InvoiceReceiptRelation).HasForeignKey(c => c.ReceiptID); // FK_S_C_InvoiceReciptRelation_S_C_Receipt
        }
    }

    // S_C_ManageContract
    internal partial class S_C_ManageContractConfiguration : EntityTypeConfiguration<S_C_ManageContract>
    {
        public S_C_ManageContractConfiguration()
        {
            ToTable("S_C_MANAGECONTRACT");
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
            Property(x => x.PartyA).HasColumnName("PARTYA").IsOptional().HasMaxLength(50);
            Property(x => x.PartyAName).HasColumnName("PARTYANAME").IsOptional().HasMaxLength(200);
            Property(x => x.CustomerFullID).HasColumnName("CUSTOMERFULLID").IsOptional().HasMaxLength(200);
            Property(x => x.CustomerFullIDName).HasColumnName("CUSTOMERFULLIDNAME").IsOptional().HasMaxLength(500);
            Property(x => x.PartyB).HasColumnName("PARTYB").IsOptional().HasMaxLength(200);
            Property(x => x.PartyBName).HasColumnName("PARTYBNAME").IsOptional().HasMaxLength(200);
            Property(x => x.PartyC).HasColumnName("PARTYC").IsOptional().HasMaxLength(200);
            Property(x => x.SerialNumber).HasColumnName("SERIALNUMBER").IsOptional().HasMaxLength(200);
            Property(x => x.ContractType).HasColumnName("CONTRACTTYPE").IsOptional().HasMaxLength(200);
            Property(x => x.ContractClass).HasColumnName("CONTRACTCLASS").IsOptional().HasMaxLength(200);
            Property(x => x.SignDate).HasColumnName("SIGNDATE").IsOptional();
            Property(x => x.SignAddress).HasColumnName("SIGNADDRESS").IsOptional().HasMaxLength(200);
            Property(x => x.ContractState).HasColumnName("CONTRACTSTATE").IsOptional().HasMaxLength(200);
            Property(x => x.StartDate).HasColumnName("STARTDATE").IsOptional();
            Property(x => x.EndDate).HasColumnName("ENDDATE").IsOptional();
            Property(x => x.ContractSource).HasColumnName("CONTRACTSOURCE").IsOptional().HasMaxLength(200);
            Property(x => x.ContractValue).HasColumnName("CONTRACTVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.MainContract).HasColumnName("MAINCONTRACT").IsOptional().HasMaxLength(200);
            Property(x => x.MainContractName).HasColumnName("MAINCONTRACTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ContractCurrency).HasColumnName("CONTRACTCURRENCY").IsOptional().HasMaxLength(200);
            Property(x => x.ContractRMBAmount).HasColumnName("CONTRACTRMBAMOUNT").IsOptional().HasPrecision(18,4);
            Property(x => x.ExchangeRate).HasColumnName("EXCHANGERATE").IsOptional().HasPrecision(18,4);
            Property(x => x.ContractValueDX).HasColumnName("CONTRACTVALUEDX").IsOptional().HasMaxLength(200);
            Property(x => x.ContractRMBValueDX).HasColumnName("CONTRACTRMBVALUEDX").IsOptional().HasMaxLength(200);
            Property(x => x.PartyALegalPerson).HasColumnName("PARTYALEGALPERSON").IsOptional().HasMaxLength(200);
            Property(x => x.PartyBLegalPerson).HasColumnName("PARTYBLEGALPERSON").IsOptional().HasMaxLength(200);
            Property(x => x.PartyCLegalPerson).HasColumnName("PARTYCLEGALPERSON").IsOptional().HasMaxLength(200);
            Property(x => x.PartyAHusbandingAgentName).HasColumnName("PARTYAHUSBANDINGAGENTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.PartyBHusbandingAgentName).HasColumnName("PARTYBHUSBANDINGAGENTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.PartyCHusbandingAgentName).HasColumnName("PARTYCHUSBANDINGAGENTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.BusinessDept).HasColumnName("BUSINESSDEPT").IsOptional().HasMaxLength(200);
            Property(x => x.BusinessDeptName).HasColumnName("BUSINESSDEPTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.BusinessManager).HasColumnName("BUSINESSMANAGER").IsOptional().HasMaxLength(200);
            Property(x => x.BusinessManagerName).HasColumnName("BUSINESSMANAGERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ProductionDept).HasColumnName("PRODUCTIONDEPT").IsOptional().HasMaxLength(500);
            Property(x => x.ProductionDeptName).HasColumnName("PRODUCTIONDEPTNAME").IsOptional().HasMaxLength(500);
            Property(x => x.ProductionManager).HasColumnName("PRODUCTIONMANAGER").IsOptional().HasMaxLength(200);
            Property(x => x.ProductionManagerName).HasColumnName("PRODUCTIONMANAGERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.EngineeringInfo).HasColumnName("ENGINEERINGINFO").IsOptional().HasMaxLength(200);
            Property(x => x.EngineeringInfoName).HasColumnName("ENGINEERINGINFONAME").IsOptional().HasMaxLength(200);
            Property(x => x.ContractAttachment).HasColumnName("CONTRACTATTACHMENT").IsOptional().HasMaxLength(500);
            Property(x => x.Attachment).HasColumnName("ATTACHMENT").IsOptional().HasMaxLength(500);
            Property(x => x.Remark).HasColumnName("REMARK").IsOptional().HasMaxLength(500);
            Property(x => x.SumReceiptValue).HasColumnName("SUMRECEIPTVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.SumInvoiceValue).HasColumnName("SUMINVOICEVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.SumBadDebtValue).HasColumnName("SUMBADDEBTVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.IsSigned).HasColumnName("ISSIGNED").IsOptional().HasMaxLength(200);
            Property(x => x.BelongYear).HasColumnName("BELONGYEAR").IsOptional();
            Property(x => x.BelongMonth).HasColumnName("BELONGMONTH").IsOptional();
            Property(x => x.BelongQuarter).HasColumnName("BELONGQUARTER").IsOptional();
            Property(x => x.ContractTextState).HasColumnName("CONTRACTTEXTSTATE").IsOptional().HasMaxLength(200);
            Property(x => x.ContractTextSendUser).HasColumnName("CONTRACTTEXTSENDUSER").IsOptional().HasMaxLength(200);
            Property(x => x.ContractTextSendUserName).HasColumnName("CONTRACTTEXTSENDUSERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ContractSplit).HasColumnName("CONTRACTSPLIT").IsOptional();
            Property(x => x.ThisContractRMBAmount).HasColumnName("THISCONTRACTRMBAMOUNT").IsOptional().HasPrecision(18,4);
            Property(x => x.ThisContractRMBAmountDX).HasColumnName("THISCONTRACTRMBAMOUNTDX").IsOptional().HasMaxLength(200);
            Property(x => x.TaxRate).HasColumnName("TAXRATE").IsOptional().HasPrecision(18,4);
            Property(x => x.TaxName).HasColumnName("TAXNAME").IsOptional().HasMaxLength(50);
            Property(x => x.ClearContractValue).HasColumnName("CLEARCONTRACTVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.TaxValue).HasColumnName("TAXVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.PayerUnit).HasColumnName("PAYERUNIT").IsOptional().HasMaxLength(200);
            Property(x => x.PayerUnitName).HasColumnName("PAYERUNITNAME").IsOptional().HasMaxLength(200);

            // Foreign keys
            HasOptional(a => a.S_F_Customer).WithMany(b => b.S_C_ManageContract).HasForeignKey(c => c.PartyA); // FK_S_C_ManageContract_S_F_Customer
        }
    }

    // S_C_ManageContract_ContractSplit
    internal partial class S_C_ManageContract_ContractSplitConfiguration : EntityTypeConfiguration<S_C_ManageContract_ContractSplit>
    {
        public S_C_ManageContract_ContractSplitConfiguration()
        {
            ToTable("S_C_MANAGECONTRACT_CONTRACTSPLIT");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.S_C_ManageContractID).HasColumnName("S_C_MANAGECONTRACTID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(200);
            Property(x => x.Scale).HasColumnName("SCALE").IsOptional().HasPrecision(18,4);
            Property(x => x.SplitValue).HasColumnName("SPLITVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.SplitType).HasColumnName("SPLITTYPE").IsOptional().HasMaxLength(200);
            Property(x => x.BusinessType).HasColumnName("BUSINESSTYPE").IsOptional().HasMaxLength(200);

            // Foreign keys
            HasOptional(a => a.S_C_ManageContract).WithMany(b => b.S_C_ManageContract_ContractSplit).HasForeignKey(c => c.S_C_ManageContractID); // FK_S_C_ManageContract_ContractSplit_S_C_ManageContract
        }
    }

    // S_C_ManageContract_DeptRelation
    internal partial class S_C_ManageContract_DeptRelationConfiguration : EntityTypeConfiguration<S_C_ManageContract_DeptRelation>
    {
        public S_C_ManageContract_DeptRelationConfiguration()
        {
            ToTable("S_C_MANAGECONTRACT_DEPTRELATION");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.S_C_ManageContractID).HasColumnName("S_C_MANAGECONTRACTID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.Dept).HasColumnName("DEPT").IsOptional().HasMaxLength(200);
            Property(x => x.DeptName).HasColumnName("DEPTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Scale).HasColumnName("SCALE").IsOptional().HasPrecision(18,4);
            Property(x => x.DeptValue).HasColumnName("DEPTVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.SumSupplementaryValue).HasColumnName("SUMSUPPLEMENTARYVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.SumDeptReceiptValue).HasColumnName("SUMDEPTRECEIPTVALUE").IsOptional().HasPrecision(18,4);

            // Foreign keys
            HasOptional(a => a.S_C_ManageContract).WithMany(b => b.S_C_ManageContract_DeptRelation).HasForeignKey(c => c.S_C_ManageContractID); // FK_S_C_ManageContract_DeptRelation_S_C_ManageContract
        }
    }

    // S_C_ManageContract_PaymentDetail
    internal partial class S_C_ManageContract_PaymentDetailConfiguration : EntityTypeConfiguration<S_C_ManageContract_PaymentDetail>
    {
        public S_C_ManageContract_PaymentDetailConfiguration()
        {
            ToTable("S_C_MANAGECONTRACT_PAYMENTDETAIL");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.S_C_ManageContractID).HasColumnName("S_C_MANAGECONTRACTID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.CustomerID).HasColumnName("CUSTOMERID").IsOptional().HasMaxLength(200);
            Property(x => x.CustomerIDName).HasColumnName("CUSTOMERIDNAME").IsOptional().HasMaxLength(200);
            Property(x => x.PaymentLinkMan).HasColumnName("PAYMENTLINKMAN").IsOptional().HasMaxLength(200);
            Property(x => x.PaymentLinkTel).HasColumnName("PAYMENTLINKTEL").IsOptional().HasMaxLength(200);
            Property(x => x.EvidenceAttachment).HasColumnName("EVIDENCEATTACHMENT").IsOptional().HasMaxLength(200);
            Property(x => x.EvidenceAttachmentName).HasColumnName("EVIDENCEATTACHMENTNAME").IsOptional().HasMaxLength(200);

            // Foreign keys
            HasOptional(a => a.S_C_ManageContract).WithMany(b => b.S_C_ManageContract_PaymentDetail).HasForeignKey(c => c.S_C_ManageContractID); // FK_S_C_ManageContract_PaymentDetail_S_C_ManageContract
        }
    }

    // S_C_ManageContract_ProjectRelation
    internal partial class S_C_ManageContract_ProjectRelationConfiguration : EntityTypeConfiguration<S_C_ManageContract_ProjectRelation>
    {
        public S_C_ManageContract_ProjectRelationConfiguration()
        {
            ToTable("S_C_MANAGECONTRACT_PROJECTRELATION");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.S_C_ManageContractID).HasColumnName("S_C_MANAGECONTRACTID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.ProjectCode).HasColumnName("PROJECTCODE").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectName).HasColumnName("PROJECTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectID).HasColumnName("PROJECTID").IsOptional().HasMaxLength(200);
            Property(x => x.Scale).HasColumnName("SCALE").IsOptional().HasPrecision(18,4);
            Property(x => x.ProjectValue).HasColumnName("PROJECTVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.TaxValue).HasColumnName("TAXVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.ClearValue).HasColumnName("CLEARVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.TaxRate).HasColumnName("TAXRATE").IsOptional().HasPrecision(18,4);
            Property(x => x.ChargerDept).HasColumnName("CHARGERDEPT").IsOptional().HasMaxLength(200);
            Property(x => x.ChargerDeptName).HasColumnName("CHARGERDEPTNAME").IsOptional().HasMaxLength(200);

            // Foreign keys
            HasOptional(a => a.S_C_ManageContract).WithMany(b => b.S_C_ManageContract_ProjectRelation).HasForeignKey(c => c.S_C_ManageContractID); // FK_S_C_ManageContract_ProjectRelation_S_C_ManageContract
        }
    }

    // S_C_ManageContract_ReceiptObj
    internal partial class S_C_ManageContract_ReceiptObjConfiguration : EntityTypeConfiguration<S_C_ManageContract_ReceiptObj>
    {
        public S_C_ManageContract_ReceiptObjConfiguration()
        {
            ToTable("S_C_MANAGECONTRACT_RECEIPTOBJ");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.S_C_ManageContractID).HasColumnName("S_C_MANAGECONTRACTID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(200);
            Property(x => x.ReceiptPercent).HasColumnName("RECEIPTPERCENT").IsOptional().HasPrecision(18,2);
            Property(x => x.ReceiptValue).HasColumnName("RECEIPTVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.FactInvoiceValue).HasColumnName("FACTINVOICEVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.FactReceiptValue).HasColumnName("FACTRECEIPTVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.OriginalPlanFinishDate).HasColumnName("ORIGINALPLANFINISHDATE").IsOptional();
            Property(x => x.PlanFinishDate).HasColumnName("PLANFINISHDATE").IsOptional();
            Property(x => x.ProjectInfo).HasColumnName("PROJECTINFO").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectInfoName).HasColumnName("PROJECTINFONAME").IsOptional().HasMaxLength(200);
            Property(x => x.MileStoneName).HasColumnName("MILESTONENAME").IsOptional().HasMaxLength(200);
            Property(x => x.MileStoneID).HasColumnName("MILESTONEID").IsOptional().HasMaxLength(200);
            Property(x => x.FactBadValue).HasColumnName("FACTBADVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.MileStoneState).HasColumnName("MILESTONESTATE").IsOptional().HasMaxLength(200);
            Property(x => x.MileStonePlanEndDate).HasColumnName("MILESTONEPLANENDDATE").IsOptional();
            Property(x => x.MileStoneFactEndDate).HasColumnName("MILESTONEFACTENDDATE").IsOptional();
            Property(x => x.SupplementaryID).HasColumnName("SUPPLEMENTARYID").IsOptional().HasMaxLength(200);

            // Foreign keys
            HasOptional(a => a.S_C_ManageContract).WithMany(b => b.S_C_ManageContract_ReceiptObj).HasForeignKey(c => c.S_C_ManageContractID); // FK_S_C_ManageContract_ReceiptObj_S_C_ManageContract
        }
    }

    // S_C_ManageContract_Supplementary
    internal partial class S_C_ManageContract_SupplementaryConfiguration : EntityTypeConfiguration<S_C_ManageContract_Supplementary>
    {
        public S_C_ManageContract_SupplementaryConfiguration()
        {
            ToTable("S_C_MANAGECONTRACT_SUPPLEMENTARY");
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
            Property(x => x.ContractInfoID).HasColumnName("CONTRACTINFOID").IsOptional().HasMaxLength(50);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(200);
            Property(x => x.SerialNumber).HasColumnName("SERIALNUMBER").IsOptional().HasMaxLength(200);
            Property(x => x.ChargeUser).HasColumnName("CHARGEUSER").IsOptional().HasMaxLength(200);
            Property(x => x.ChargeUserName).HasColumnName("CHARGEUSERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.SignDate).HasColumnName("SIGNDATE").IsOptional();
            Property(x => x.SupplementaryValue).HasColumnName("SUPPLEMENTARYVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.SupplementaryCurrency).HasColumnName("SUPPLEMENTARYCURRENCY").IsOptional().HasMaxLength(200);
            Property(x => x.SupplementaryRMBAmount).HasColumnName("SUPPLEMENTARYRMBAMOUNT").IsOptional().HasPrecision(18,4);
            Property(x => x.SupplementaryExchangeRate).HasColumnName("SUPPLEMENTARYEXCHANGERATE").IsOptional().HasPrecision(18,4);
            Property(x => x.SupplementaryValueDX).HasColumnName("SUPPLEMENTARYVALUEDX").IsOptional().HasMaxLength(200);
            Property(x => x.SupplementaryRMBAmountDX).HasColumnName("SUPPLEMENTARYRMBAMOUNTDX").IsOptional().HasMaxLength(200);
            Property(x => x.Attachment).HasColumnName("ATTACHMENT").IsOptional().HasMaxLength(500);
            Property(x => x.Remark).HasColumnName("REMARK").IsOptional().HasMaxLength(500);
            Property(x => x.BelongYear).HasColumnName("BELONGYEAR").IsOptional();
            Property(x => x.BelongMonth).HasColumnName("BELONGMONTH").IsOptional();
            Property(x => x.BelongQuarter).HasColumnName("BELONGQUARTER").IsOptional();
            Property(x => x.ReceiptObjID).HasColumnName("RECEIPTOBJID").IsOptional().HasMaxLength(200);

            // Foreign keys
            HasOptional(a => a.S_C_ManageContract).WithMany(b => b.S_C_ManageContract_Supplementary).HasForeignKey(c => c.ContractInfoID); // FK_ContractInfoID
        }
    }

    // S_C_ManageContract_Supplementary_DeptRelation
    internal partial class S_C_ManageContract_Supplementary_DeptRelationConfiguration : EntityTypeConfiguration<S_C_ManageContract_Supplementary_DeptRelation>
    {
        public S_C_ManageContract_Supplementary_DeptRelationConfiguration()
        {
            ToTable("S_C_MANAGECONTRACT_SUPPLEMENTARY_DEPTRELATION");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.S_C_ManageContract_SupplementaryID).HasColumnName("S_C_MANAGECONTRACT_SUPPLEMENTARYID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.Dept).HasColumnName("DEPT").IsOptional().HasMaxLength(200);
            Property(x => x.DeptName).HasColumnName("DEPTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Scale).HasColumnName("SCALE").IsOptional().HasPrecision(18,4);
            Property(x => x.DeptValue).HasColumnName("DEPTVALUE").IsOptional().HasPrecision(18,4);

            // Foreign keys
            HasOptional(a => a.S_C_ManageContract_Supplementary).WithMany(b => b.S_C_ManageContract_Supplementary_DeptRelation).HasForeignKey(c => c.S_C_ManageContract_SupplementaryID); // FK_S_C_ManageContract_Supplementary_DeptRelation_S_C_ManageContract_Supplementary
        }
    }

    // S_C_ManageContract_Supplementary_ReceiptObj
    internal partial class S_C_ManageContract_Supplementary_ReceiptObjConfiguration : EntityTypeConfiguration<S_C_ManageContract_Supplementary_ReceiptObj>
    {
        public S_C_ManageContract_Supplementary_ReceiptObjConfiguration()
        {
            ToTable("S_C_MANAGECONTRACT_SUPPLEMENTARY_RECEIPTOBJ");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.S_C_ManageContract_SupplementaryID).HasColumnName("S_C_MANAGECONTRACT_SUPPLEMENTARYID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(200);
            Property(x => x.ReceiptPercent).HasColumnName("RECEIPTPERCENT").IsOptional().HasPrecision(18,2);
            Property(x => x.ReceiptValue).HasColumnName("RECEIPTVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.FactInvoiceValue).HasColumnName("FACTINVOICEVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.FactReceiptValue).HasColumnName("FACTRECEIPTVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.OriginalPlanFinishDate).HasColumnName("ORIGINALPLANFINISHDATE").IsOptional();
            Property(x => x.PlanFinishDate).HasColumnName("PLANFINISHDATE").IsOptional();
            Property(x => x.ProjectInfo).HasColumnName("PROJECTINFO").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectInfoName).HasColumnName("PROJECTINFONAME").IsOptional().HasMaxLength(200);
            Property(x => x.MileStoneName).HasColumnName("MILESTONENAME").IsOptional().HasMaxLength(200);
            Property(x => x.MileStonePlanEndDate).HasColumnName("MILESTONEPLANENDDATE").IsOptional();
            Property(x => x.MileStoneFactEndDate).HasColumnName("MILESTONEFACTENDDATE").IsOptional();
            Property(x => x.MileStoneID).HasColumnName("MILESTONEID").IsOptional().HasMaxLength(200);
            Property(x => x.FactBadValue).HasColumnName("FACTBADVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.MileStoneState).HasColumnName("MILESTONESTATE").IsOptional().HasMaxLength(200);

            // Foreign keys
            HasOptional(a => a.S_C_ManageContract_Supplementary).WithMany(b => b.S_C_ManageContract_Supplementary_ReceiptObj).HasForeignKey(c => c.S_C_ManageContract_SupplementaryID); // FK_S_C_ManageContract_Supplementary_ReceiptObj_S_C_ManageContract_Supplementary
        }
    }

    // S_C_PlanReceipt
    internal partial class S_C_PlanReceiptConfiguration : EntityTypeConfiguration<S_C_PlanReceipt>
    {
        public S_C_PlanReceiptConfiguration()
        {
            ToTable("S_C_PLANRECEIPT");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.ContractInfoID).HasColumnName("CONTRACTINFOID").IsRequired().HasMaxLength(50);
            Property(x => x.ReceiptObjectID).HasColumnName("RECEIPTOBJECTID").IsRequired().HasMaxLength(50);
            Property(x => x.RelateParentID).HasColumnName("RELATEPARENTID").IsOptional().HasMaxLength(50);
            Property(x => x.Name).HasColumnName("NAME").IsRequired().HasMaxLength(500);
            Property(x => x.CustomerID).HasColumnName("CUSTOMERID").IsOptional().HasMaxLength(50);
            Property(x => x.CustomerFullID).HasColumnName("CUSTOMERFULLID").IsOptional().HasMaxLength(200);
            Property(x => x.CustomerCode).HasColumnName("CUSTOMERCODE").IsOptional().HasMaxLength(500);
            Property(x => x.CustomerName).HasColumnName("CUSTOMERNAME").IsOptional().HasMaxLength(500);
            Property(x => x.PlanReceiptValue).HasColumnName("PLANRECEIPTVALUE").IsRequired().HasPrecision(18,4);
            Property(x => x.FactReceiptValue).HasColumnName("FACTRECEIPTVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.PlanReceiptDate).HasColumnName("PLANRECEIPTDATE").IsOptional();
            Property(x => x.ReceiptDate).HasColumnName("RECEIPTDATE").IsOptional();
            Property(x => x.ProductionUnitID).HasColumnName("PRODUCTIONUNITID").IsOptional().HasMaxLength(50);
            Property(x => x.ProductionUnitName).HasColumnName("PRODUCTIONUNITNAME").IsOptional().HasMaxLength(50);
            Property(x => x.ProduceMasterID).HasColumnName("PRODUCEMASTERID").IsOptional().HasMaxLength(50);
            Property(x => x.ProduceMasterName).HasColumnName("PRODUCEMASTERNAME").IsOptional().HasMaxLength(50);
            Property(x => x.FirstDutyManName).HasColumnName("FIRSTDUTYMANNAME").IsOptional().HasMaxLength(50);
            Property(x => x.FirstDutyManID).HasColumnName("FIRSTDUTYMANID").IsOptional().HasMaxLength(50);
            Property(x => x.PlanReceiptYearMonth).HasColumnName("PLANRECEIPTYEARMONTH").IsOptional().HasMaxLength(50);
            Property(x => x.BelongYear).HasColumnName("BELONGYEAR").IsOptional();
            Property(x => x.BelongQuarter).HasColumnName("BELONGQUARTER").IsOptional();
            Property(x => x.BelongMonth).HasColumnName("BELONGMONTH").IsOptional();
            Property(x => x.ContractName).HasColumnName("CONTRACTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ContractCode).HasColumnName("CONTRACTCODE").IsOptional().HasMaxLength(50);
            Property(x => x.ProjectID).HasColumnName("PROJECTID").IsOptional().HasMaxLength(50);
            Property(x => x.ProjectName).HasColumnName("PROJECTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectCode).HasColumnName("PROJECTCODE").IsOptional().HasMaxLength(50);
            Property(x => x.Remark).HasColumnName("REMARK").IsOptional().HasMaxLength(500);
            Property(x => x.State).HasColumnName("STATE").IsOptional().HasMaxLength(50);
            Property(x => x.IsBadDebt).HasColumnName("ISBADDEBT").IsOptional().HasMaxLength(50);
            Property(x => x.BadDebtValue).HasColumnName("BADDEBTVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.Importance).HasColumnName("IMPORTANCE").IsOptional().HasMaxLength(50);
            Property(x => x.RiskLevel).HasColumnName("RISKLEVEL").IsOptional().HasMaxLength(50);
            Property(x => x.DutyType).HasColumnName("DUTYTYPE").IsOptional().HasMaxLength(50);
            Property(x => x.MasterUnitID).HasColumnName("MASTERUNITID").IsOptional().HasMaxLength(50);
            Property(x => x.MasterUnit).HasColumnName("MASTERUNIT").IsOptional().HasMaxLength(50);
            Property(x => x.MasterID).HasColumnName("MASTERID").IsOptional().HasMaxLength(50);
            Property(x => x.MasterName).HasColumnName("MASTERNAME").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsOptional().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUser).HasColumnName("MODIFYUSER").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyDate).HasColumnName("MODIFYDATE").IsOptional();

            // Foreign keys
            HasRequired(a => a.S_C_ManageContract).WithMany(b => b.S_C_PlanReceipt).HasForeignKey(c => c.ContractInfoID); // FK_S_C_PlanReceipt_S_C_ManageContract
            HasRequired(a => a.S_C_ManageContract_ReceiptObj).WithMany(b => b.S_C_PlanReceipt).HasForeignKey(c => c.ReceiptObjectID); // FK_S_C_PlanReceipt_S_C_ManageContract_ReceiptObj
        }
    }

    // S_C_Receipt
    internal partial class S_C_ReceiptConfiguration : EntityTypeConfiguration<S_C_Receipt>
    {
        public S_C_ReceiptConfiguration()
        {
            ToTable("S_C_RECEIPT");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.ContractInfoID).HasColumnName("CONTRACTINFOID").IsRequired().HasMaxLength(50);
            Property(x => x.ContractName).HasColumnName("CONTRACTNAME").IsRequired().HasMaxLength(500);
            Property(x => x.ContractCode).HasColumnName("CONTRACTCODE").IsOptional().HasMaxLength(500);
            Property(x => x.CustomerID).HasColumnName("CUSTOMERID").IsOptional().HasMaxLength(50);
            Property(x => x.CustomerName).HasColumnName("CUSTOMERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.CustomerFullID).HasColumnName("CUSTOMERFULLID").IsOptional().HasMaxLength(200);
            Property(x => x.PayerUnit).HasColumnName("PAYERUNIT").IsOptional().HasMaxLength(50);
            Property(x => x.PayerUnitName).HasColumnName("PAYERUNITNAME").IsOptional().HasMaxLength(50);
            Property(x => x.RegisterID).HasColumnName("REGISTERID").IsOptional().HasMaxLength(50);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(200);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(50);
            Property(x => x.Amount).HasColumnName("AMOUNT").IsRequired().HasPrecision(18,4);
            Property(x => x.ArrivedDate).HasColumnName("ARRIVEDDATE").IsRequired();
            Property(x => x.ReceiptType).HasColumnName("RECEIPTTYPE").IsOptional().HasMaxLength(50);
            Property(x => x.ReceiptMasterUnit).HasColumnName("RECEIPTMASTERUNIT").IsOptional().HasMaxLength(200);
            Property(x => x.ReceiptMasterUnitID).HasColumnName("RECEIPTMASTERUNITID").IsOptional().HasMaxLength(200);
            Property(x => x.ProductUnit).HasColumnName("PRODUCTUNIT").IsOptional().HasMaxLength(500);
            Property(x => x.ProductUnitName).HasColumnName("PRODUCTUNITNAME").IsOptional().HasMaxLength(500);
            Property(x => x.Operator).HasColumnName("OPERATOR").IsOptional().HasMaxLength(50);
            Property(x => x.OperatorID).HasColumnName("OPERATORID").IsOptional().HasMaxLength(50);
            Property(x => x.Remark).HasColumnName("REMARK").IsOptional();
            Property(x => x.BelongYear).HasColumnName("BELONGYEAR").IsRequired();
            Property(x => x.BelongQuarter).HasColumnName("BELONGQUARTER").IsRequired();
            Property(x => x.BelongMonth).HasColumnName("BELONGMONTH").IsRequired();
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsRequired().HasMaxLength(50);
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsRequired().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsRequired();
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUser).HasColumnName("MODIFYUSER").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyDate).HasColumnName("MODIFYDATE").IsOptional();
            Property(x => x.OrgID).HasColumnName("ORGID").IsOptional().HasMaxLength(500);
            Property(x => x.OrgName).HasColumnName("ORGNAME").IsOptional().HasMaxLength(500);
            Property(x => x.State).HasColumnName("STATE").IsOptional().HasMaxLength(200);

            // Foreign keys
            HasRequired(a => a.S_C_ManageContract).WithMany(b => b.S_C_Receipt).HasForeignKey(c => c.ContractInfoID); // FK_S_C_Receipt_S_C_ManageContract
        }
    }

    // S_C_Receipt_DeptRelation
    internal partial class S_C_Receipt_DeptRelationConfiguration : EntityTypeConfiguration<S_C_Receipt_DeptRelation>
    {
        public S_C_Receipt_DeptRelationConfiguration()
        {
            ToTable("S_C_RECEIPT_DEPTRELATION");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.S_C_Receipt_ID).HasColumnName("S_C_RECEIPT_ID").IsOptional().HasMaxLength(50);
            Property(x => x.ContractInfoID).HasColumnName("CONTRACTINFOID").IsOptional().HasMaxLength(50);
            Property(x => x.Dept).HasColumnName("DEPT").IsOptional().HasMaxLength(200);
            Property(x => x.DeptName).HasColumnName("DEPTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.RelationValue).HasColumnName("RELATIONVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUser).HasColumnName("MODIFYUSER").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyDate).HasColumnName("MODIFYDATE").IsOptional();

            // Foreign keys
            HasOptional(a => a.S_C_Receipt).WithMany(b => b.S_C_Receipt_DeptRelation).HasForeignKey(c => c.S_C_Receipt_ID); // FK_S_C_Receipt_DeptRelation_S_C_Receipt
        }
    }

    // S_C_Receipt_ProjectRelation
    internal partial class S_C_Receipt_ProjectRelationConfiguration : EntityTypeConfiguration<S_C_Receipt_ProjectRelation>
    {
        public S_C_Receipt_ProjectRelationConfiguration()
        {
            ToTable("S_C_RECEIPT_PROJECTRELATION");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.S_C_Receipt_ID).HasColumnName("S_C_RECEIPT_ID").IsOptional().HasMaxLength(50);
            Property(x => x.ContractInfoID).HasColumnName("CONTRACTINFOID").IsOptional().HasMaxLength(50);
            Property(x => x.ProjectID).HasColumnName("PROJECTID").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectName).HasColumnName("PROJECTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.RelationValue).HasColumnName("RELATIONVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUser).HasColumnName("MODIFYUSER").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyDate).HasColumnName("MODIFYDATE").IsOptional();

            // Foreign keys
            HasOptional(a => a.S_C_Receipt).WithMany(b => b.S_C_Receipt_ProjectRelation).HasForeignKey(c => c.S_C_Receipt_ID); // FK_S_C_Receipt_ProjectRelation_S_C_Receipt
        }
    }

    // S_C_ReceiptPlanRelation
    internal partial class S_C_ReceiptPlanRelationConfiguration : EntityTypeConfiguration<S_C_ReceiptPlanRelation>
    {
        public S_C_ReceiptPlanRelationConfiguration()
        {
            ToTable("S_C_RECEIPTPLANRELATION");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.ReceiptID).HasColumnName("RECEIPTID").IsRequired().HasMaxLength(50);
            Property(x => x.PlanID).HasColumnName("PLANID").IsRequired().HasMaxLength(50);
            Property(x => x.ReceiptObjectID).HasColumnName("RECEIPTOBJECTID").IsOptional().HasMaxLength(500);
            Property(x => x.ReceiptObjectCode).HasColumnName("RECEIPTOBJECTCODE").IsOptional().HasMaxLength(500);
            Property(x => x.RelationValue).HasColumnName("RELATIONVALUE").IsRequired().HasPrecision(18,4);
            Property(x => x.RelateParentID).HasColumnName("RELATEPARENTID").IsRequired().HasMaxLength(50);
            Property(x => x.ModifyUser).HasColumnName("MODIFYUSER").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyDate).HasColumnName("MODIFYDATE").IsOptional();

            // Foreign keys
            HasRequired(a => a.S_C_Receipt).WithMany(b => b.S_C_ReceiptPlanRelation).HasForeignKey(c => c.ReceiptID); // FK_S_C_ReceiptPlanRelation_S_C_Receipt
            HasRequired(a => a.S_C_PlanReceipt).WithMany(b => b.S_C_ReceiptPlanRelation).HasForeignKey(c => c.PlanID); // FK_S_C_ReceiptPlanRelation_S_C_PlanReceipt
        }
    }

    // S_C_ReceiptRegister
    internal partial class S_C_ReceiptRegisterConfiguration : EntityTypeConfiguration<S_C_ReceiptRegister>
    {
        public S_C_ReceiptRegisterConfiguration()
        {
            ToTable("S_C_RECEIPTREGISTER");
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
            Property(x => x.CustomerInfo).HasColumnName("CUSTOMERINFO").IsOptional().HasMaxLength(50);
            Property(x => x.CustomerInfoName).HasColumnName("CUSTOMERINFONAME").IsOptional().HasMaxLength(50);
            Property(x => x.ReceiptValue).HasColumnName("RECEIPTVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.ReceiptNo).HasColumnName("RECEIPTNO").IsOptional().HasMaxLength(200);
            Property(x => x.ReceiptDate).HasColumnName("RECEIPTDATE").IsOptional();
            Property(x => x.Register).HasColumnName("REGISTER").IsOptional().HasMaxLength(200);
            Property(x => x.RegisterName).HasColumnName("REGISTERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.RegisterDate).HasColumnName("REGISTERDATE").IsOptional();
            Property(x => x.Remark).HasColumnName("REMARK").IsOptional().HasMaxLength(500);
            Property(x => x.ConfirmValue).HasColumnName("CONFIRMVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.RegisterState).HasColumnName("REGISTERSTATE").IsOptional().HasMaxLength(200);
        }
    }

    // S_F_Customer
    internal partial class S_F_CustomerConfiguration : EntityTypeConfiguration<S_F_Customer>
    {
        public S_F_CustomerConfiguration()
        {
            ToTable("S_F_CUSTOMER");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyDate).HasColumnName("MODIFYDATE").IsOptional();
            Property(x => x.ModifyUser).HasColumnName("MODIFYUSER").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(500);
            Property(x => x.Parent).HasColumnName("PARENT").IsOptional().HasMaxLength(50);
            Property(x => x.ParentName).HasColumnName("PARENTNAME").IsOptional().HasMaxLength(50);
            Property(x => x.FullID).HasColumnName("FULLID").IsOptional().HasMaxLength(500);
            Property(x => x.ShortName).HasColumnName("SHORTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(200);
            Property(x => x.LinkUser).HasColumnName("LINKUSER").IsOptional().HasMaxLength(200);
            Property(x => x.LinkTelphone).HasColumnName("LINKTELPHONE").IsOptional().HasMaxLength(200);
            Property(x => x.Industry).HasColumnName("INDUSTRY").IsOptional().HasMaxLength(50);
            Property(x => x.IsImportantCustomer).HasColumnName("ISIMPORTANTCUSTOMER").IsOptional().HasMaxLength(50);
            Property(x => x.JuridicalPerson).HasColumnName("JURIDICALPERSON").IsOptional().HasMaxLength(200);
            Property(x => x.Country).HasColumnName("COUNTRY").IsOptional().HasMaxLength(50);
            Property(x => x.Province).HasColumnName("PROVINCE").IsOptional().HasMaxLength(50);
            Property(x => x.City).HasColumnName("CITY").IsOptional().HasMaxLength(50);
            Property(x => x.Area).HasColumnName("AREA").IsOptional().HasMaxLength(50);
            Property(x => x.ZipCode).HasColumnName("ZIPCODE").IsOptional().HasMaxLength(200);
            Property(x => x.Address).HasColumnName("ADDRESS").IsOptional().HasMaxLength(200);
            Property(x => x.InvoiceName).HasColumnName("INVOICENAME").IsOptional().HasMaxLength(200);
            Property(x => x.TaxAccounts).HasColumnName("TAXACCOUNTS").IsOptional().HasMaxLength(200);
            Property(x => x.Attachment).HasColumnName("ATTACHMENT").IsOptional().HasMaxLength(500);
            Property(x => x.Remark).HasColumnName("REMARK").IsOptional().HasMaxLength(500);
            Property(x => x.SyncCloudTime).HasColumnName("SYNCCLOUDTIME").IsOptional();
            Property(x => x.IsCooperated).HasColumnName("ISCOOPERATED").IsOptional().HasMaxLength(200);
            Property(x => x.CustomerLevel).HasColumnName("CUSTOMERLEVEL").IsOptional().HasMaxLength(200);
            Property(x => x.SerialNumber).HasColumnName("SERIALNUMBER").IsOptional().HasMaxLength(200);
            Property(x => x.BusinessDate).HasColumnName("BUSINESSDATE").IsOptional();
            Property(x => x.BusinessCode).HasColumnName("BUSINESSCODE").IsOptional().HasMaxLength(200);
        }
    }

    // S_F_Customer_BankAccounts
    internal partial class S_F_Customer_BankAccountsConfiguration : EntityTypeConfiguration<S_F_Customer_BankAccounts>
    {
        public S_F_Customer_BankAccountsConfiguration()
        {
            ToTable("S_F_CUSTOMER_BANKACCOUNTS");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.S_F_CustomerID).HasColumnName("S_F_CUSTOMERID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.Bank).HasColumnName("BANK").IsOptional().HasMaxLength(200);
            Property(x => x.BankName).HasColumnName("BANKNAME").IsOptional().HasMaxLength(200);
            Property(x => x.BankAccount).HasColumnName("BANKACCOUNT").IsOptional().HasMaxLength(200);
            Property(x => x.BankAccountName).HasColumnName("BANKACCOUNTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.BankInfo).HasColumnName("BANKINFO").IsOptional().HasMaxLength(200);

            // Foreign keys
            HasOptional(a => a.S_F_Customer).WithMany(b => b.S_F_Customer_BankAccounts).HasForeignKey(c => c.S_F_CustomerID); // FK_S_F_Customer_BankAccounts_S_F_Customer
        }
    }

    // S_F_Customer_BigEvent
    internal partial class S_F_Customer_BigEventConfiguration : EntityTypeConfiguration<S_F_Customer_BigEvent>
    {
        public S_F_Customer_BigEventConfiguration()
        {
            ToTable("S_F_CUSTOMER_BIGEVENT");
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
            Property(x => x.CustomerInfo).HasColumnName("CUSTOMERINFO").IsOptional().HasMaxLength(50);
            Property(x => x.CustomerInfoName).HasColumnName("CUSTOMERINFONAME").IsOptional().HasMaxLength(50);
            Property(x => x.EventTitle).HasColumnName("EVENTTITLE").IsOptional().HasMaxLength(200);
            Property(x => x.Register).HasColumnName("REGISTER").IsOptional().HasMaxLength(50);
            Property(x => x.RegisterName).HasColumnName("REGISTERNAME").IsOptional().HasMaxLength(50);
            Property(x => x.EventContent).HasColumnName("EVENTCONTENT").IsOptional().HasMaxLength(500);
            Property(x => x.RegisteDate).HasColumnName("REGISTEDATE").IsOptional();
        }
    }

    // S_F_Customer_Complain
    internal partial class S_F_Customer_ComplainConfiguration : EntityTypeConfiguration<S_F_Customer_Complain>
    {
        public S_F_Customer_ComplainConfiguration()
        {
            ToTable("S_F_CUSTOMER_COMPLAIN");
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
            Property(x => x.CustomerInfo).HasColumnName("CUSTOMERINFO").IsOptional().HasMaxLength(50);
            Property(x => x.CustomerInfoName).HasColumnName("CUSTOMERINFONAME").IsOptional().HasMaxLength(50);
            Property(x => x.ComplainManName).HasColumnName("COMPLAINMANNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ContactWay).HasColumnName("CONTACTWAY").IsOptional().HasMaxLength(200);
            Property(x => x.ComplainProjectInfo).HasColumnName("COMPLAINPROJECTINFO").IsOptional().HasMaxLength(200);
            Property(x => x.ComplainProjectInfoName).HasColumnName("COMPLAINPROJECTINFONAME").IsOptional().HasMaxLength(200);
            Property(x => x.ComplainDept).HasColumnName("COMPLAINDEPT").IsOptional().HasMaxLength(200);
            Property(x => x.ComplainDeptName).HasColumnName("COMPLAINDEPTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ComplainQuestion).HasColumnName("COMPLAINQUESTION").IsOptional().HasMaxLength(500);
            Property(x => x.ManageDept).HasColumnName("MANAGEDEPT").IsOptional().HasMaxLength(200);
            Property(x => x.ManageDeptName).HasColumnName("MANAGEDEPTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Manager).HasColumnName("MANAGER").IsOptional().HasMaxLength(200);
            Property(x => x.ManagerName).HasColumnName("MANAGERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ManageMethod).HasColumnName("MANAGEMETHOD").IsOptional().HasMaxLength(500);
            Property(x => x.ManageResult).HasColumnName("MANAGERESULT").IsOptional().HasMaxLength(500);
            Property(x => x.Attachment).HasColumnName("ATTACHMENT").IsOptional().HasMaxLength(500);

            // Foreign keys
            HasOptional(a => a.S_F_Customer).WithMany(b => b.S_F_Customer_Complain).HasForeignKey(c => c.CustomerInfo); // FK_S_F_Customer_Complain_S_F_Customer
        }
    }

    // S_F_Customer_ContactLog
    internal partial class S_F_Customer_ContactLogConfiguration : EntityTypeConfiguration<S_F_Customer_ContactLog>
    {
        public S_F_Customer_ContactLogConfiguration()
        {
            ToTable("S_F_CUSTOMER_CONTACTLOG");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.S_F_CustomerID).HasColumnName("S_F_CUSTOMERID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.ContactDate).HasColumnName("CONTACTDATE").IsOptional();
            Property(x => x.CustomerPerson).HasColumnName("CUSTOMERPERSON").IsOptional().HasMaxLength(200);
            Property(x => x.CompanyPerson).HasColumnName("COMPANYPERSON").IsOptional().HasMaxLength(200);
            Property(x => x.CompanyPersonName).HasColumnName("COMPANYPERSONNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ContactType).HasColumnName("CONTACTTYPE").IsOptional().HasMaxLength(200);
            Property(x => x.ContactContent).HasColumnName("CONTACTCONTENT").IsOptional().HasMaxLength(200);
            Property(x => x.Remark).HasColumnName("REMARK").IsOptional().HasMaxLength(200);

            // Foreign keys
            HasOptional(a => a.S_F_Customer).WithMany(b => b.S_F_Customer_ContactLog).HasForeignKey(c => c.S_F_CustomerID); // FK_S_F_Customer_ContactLog_S_F_Customer
        }
    }

    // S_F_Customer_Demand
    internal partial class S_F_Customer_DemandConfiguration : EntityTypeConfiguration<S_F_Customer_Demand>
    {
        public S_F_Customer_DemandConfiguration()
        {
            ToTable("S_F_CUSTOMER_DEMAND");
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
            Property(x => x.StepName).HasColumnName("STEPNAME").IsOptional().HasMaxLength(200);
            Property(x => x.CustomerInfo).HasColumnName("CUSTOMERINFO").IsOptional().HasMaxLength(50);
            Property(x => x.CustomerInfoName).HasColumnName("CUSTOMERINFONAME").IsOptional().HasMaxLength(50);
            Property(x => x.Address).HasColumnName("ADDRESS").IsOptional().HasMaxLength(200);
            Property(x => x.Industry).HasColumnName("INDUSTRY").IsOptional().HasMaxLength(200);
            Property(x => x.RegisterUser).HasColumnName("REGISTERUSER").IsOptional().HasMaxLength(200);
            Property(x => x.RegisterUserName).HasColumnName("REGISTERUSERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.RegisterDate).HasColumnName("REGISTERDATE").IsOptional();
            Property(x => x.DemandDescription).HasColumnName("DEMANDDESCRIPTION").IsOptional().HasMaxLength(500);
            Property(x => x.Attachment).HasColumnName("ATTACHMENT").IsOptional().HasMaxLength(500);
            Property(x => x.ProjectName).HasColumnName("PROJECTNAME").IsOptional().HasMaxLength(200);

            // Foreign keys
            HasOptional(a => a.S_F_Customer).WithMany(b => b.S_F_Customer_Demand).HasForeignKey(c => c.CustomerInfo); // FK_S_F_Customer_Demand_S_F_Customer_Demand
        }
    }

    // S_F_Customer_LinkMan
    internal partial class S_F_Customer_LinkManConfiguration : EntityTypeConfiguration<S_F_Customer_LinkMan>
    {
        public S_F_Customer_LinkManConfiguration()
        {
            ToTable("S_F_CUSTOMER_LINKMAN");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.S_F_CustomerID).HasColumnName("S_F_CUSTOMERID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.LinkmanName).HasColumnName("LINKMANNAME").IsOptional().HasMaxLength(200);
            Property(x => x.DeptName).HasColumnName("DEPTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Position).HasColumnName("POSITION").IsOptional().HasMaxLength(200);
            Property(x => x.ResponsiblePerson).HasColumnName("RESPONSIBLEPERSON").IsOptional().HasMaxLength(200);
            Property(x => x.MobilePhone).HasColumnName("MOBILEPHONE").IsOptional().HasMaxLength(200);
            Property(x => x.OfficeTelephone).HasColumnName("OFFICETELEPHONE").IsOptional().HasMaxLength(200);
            Property(x => x.Fax).HasColumnName("FAX").IsOptional().HasMaxLength(200);
            Property(x => x.Email).HasColumnName("EMAIL").IsOptional().HasMaxLength(200);
            Property(x => x.OfficeRoom).HasColumnName("OFFICEROOM").IsOptional().HasMaxLength(200);
            Property(x => x.FaxName).HasColumnName("FAXNAME").IsOptional().HasMaxLength(200);

            // Foreign keys
            HasOptional(a => a.S_F_Customer).WithMany(b => b.S_F_Customer_LinkMan).HasForeignKey(c => c.S_F_CustomerID); // FK_S_F_Customer_LinkMan_S_F_Customer
        }
    }

    // S_F_Customer_ReturnVisit
    internal partial class S_F_Customer_ReturnVisitConfiguration : EntityTypeConfiguration<S_F_Customer_ReturnVisit>
    {
        public S_F_Customer_ReturnVisitConfiguration()
        {
            ToTable("S_F_CUSTOMER_RETURNVISIT");
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
            Property(x => x.Register).HasColumnName("REGISTER").IsOptional().HasMaxLength(200);
            Property(x => x.RegisterName).HasColumnName("REGISTERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.RegisterDate).HasColumnName("REGISTERDATE").IsOptional();
            Property(x => x.CustomerInfo).HasColumnName("CUSTOMERINFO").IsOptional().HasMaxLength(50);
            Property(x => x.CustomerInfoName).HasColumnName("CUSTOMERINFONAME").IsOptional().HasMaxLength(50);
            Property(x => x.VisitManName).HasColumnName("VISITMANNAME").IsOptional().HasMaxLength(200);
            Property(x => x.DeptName).HasColumnName("DEPTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Duties).HasColumnName("DUTIES").IsOptional().HasMaxLength(200);
            Property(x => x.ReturnVisitMan).HasColumnName("RETURNVISITMAN").IsOptional().HasMaxLength(200);
            Property(x => x.ReturnVisitManName).HasColumnName("RETURNVISITMANNAME").IsOptional().HasMaxLength(200);
            Property(x => x.VisitDate).HasColumnName("VISITDATE").IsOptional();
            Property(x => x.Content).HasColumnName("CONTENT").IsOptional().HasMaxLength(500);
            Property(x => x.Record).HasColumnName("RECORD").IsOptional().HasMaxLength(500);
            Property(x => x.Attachment).HasColumnName("ATTACHMENT").IsOptional().HasMaxLength(500);
            Property(x => x.Remark).HasColumnName("REMARK").IsOptional().HasMaxLength(500);

            // Foreign keys
            HasOptional(a => a.S_F_Customer).WithMany(b => b.S_F_Customer_ReturnVisit).HasForeignKey(c => c.CustomerInfo); // FK_S_F_Customer_ReturnVisit_S_F_Customer
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
            Property(x => x.SubjectCode).HasColumnName("SUBJECTCODE").IsOptional().HasMaxLength(50);
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

    // S_I_Engineering
    internal partial class S_I_EngineeringConfiguration : EntityTypeConfiguration<S_I_Engineering>
    {
        public S_I_EngineeringConfiguration()
        {
            ToTable("S_I_ENGINEERING");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(200);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(200);
            Property(x => x.Class).HasColumnName("CLASS").IsOptional().HasMaxLength(200);
            Property(x => x.Scale).HasColumnName("SCALE").IsOptional().HasMaxLength(200);
            Property(x => x.MainDept).HasColumnName("MAINDEPT").IsOptional().HasMaxLength(200);
            Property(x => x.MainDeptName).HasColumnName("MAINDEPTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Remark).HasColumnName("REMARK").IsOptional().HasMaxLength(500);
            Property(x => x.Attachment).HasColumnName("ATTACHMENT").IsOptional().HasMaxLength(500);
            Property(x => x.Investment).HasColumnName("INVESTMENT").IsOptional().HasPrecision(18,4);
            Property(x => x.Address).HasColumnName("ADDRESS").IsOptional().HasMaxLength(200);
            Property(x => x.Country).HasColumnName("COUNTRY").IsOptional().HasMaxLength(200);
            Property(x => x.Proinvce).HasColumnName("PROINVCE").IsOptional().HasMaxLength(200);
            Property(x => x.City).HasColumnName("CITY").IsOptional().HasMaxLength(200);
            Property(x => x.Proportion).HasColumnName("PROPORTION").IsOptional().HasMaxLength(200);
            Property(x => x.BusinessManager).HasColumnName("BUSINESSMANAGER").IsOptional().HasMaxLength(200);
            Property(x => x.BusinessManagerName).HasColumnName("BUSINESSMANAGERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.PhaseValue).HasColumnName("PHASEVALUE").IsOptional().HasMaxLength(200);
            Property(x => x.PhaseContent).HasColumnName("PHASECONTENT").IsOptional().HasMaxLength(200);
            Property(x => x.Province).HasColumnName("PROVINCE").IsOptional().HasMaxLength(200);
            Property(x => x.CustomerInfo).HasColumnName("CUSTOMERINFO").IsOptional().HasMaxLength(200);
            Property(x => x.CustomerInfoName).HasColumnName("CUSTOMERINFONAME").IsOptional().HasMaxLength(200);
            Property(x => x.OtherDept).HasColumnName("OTHERDEPT").IsOptional().HasMaxLength(200);
            Property(x => x.OtherDeptName).HasColumnName("OTHERDEPTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.LandingArea).HasColumnName("LANDINGAREA").IsOptional().HasMaxLength(200);
            Property(x => x.ChargerUser).HasColumnName("CHARGERUSER").IsOptional().HasMaxLength(500);
            Property(x => x.ChargerUserName).HasColumnName("CHARGERUSERNAME").IsOptional().HasMaxLength(500);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CustomerRequestReview).HasColumnName("CUSTOMERREQUESTREVIEW").IsOptional().HasMaxLength(200);
            Property(x => x.CustomerRequestReviewName).HasColumnName("CUSTOMERREQUESTREVIEWNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Area).HasColumnName("AREA").IsOptional().HasMaxLength(200);
        }
    }

    // S_I_Project
    internal partial class S_I_ProjectConfiguration : EntityTypeConfiguration<S_I_Project>
    {
        public S_I_ProjectConfiguration()
        {
            ToTable("S_I_PROJECT");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.Name).HasColumnName("NAME").IsRequired().HasMaxLength(500);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(500);
            Property(x => x.ProjectScale).HasColumnName("PROJECTSCALE").IsOptional().HasMaxLength(50);
            Property(x => x.ProjectClass).HasColumnName("PROJECTCLASS").IsOptional().HasMaxLength(50);
            Property(x => x.ProjectType).HasColumnName("PROJECTTYPE").IsOptional().HasMaxLength(50);
            Property(x => x.Industry).HasColumnName("INDUSTRY").IsOptional().HasMaxLength(50);
            Property(x => x.Customer).HasColumnName("CUSTOMER").IsOptional().HasMaxLength(500);
            Property(x => x.CustomerName).HasColumnName("CUSTOMERNAME").IsOptional().HasMaxLength(500);
            Property(x => x.CustomerFullID).HasColumnName("CUSTOMERFULLID").IsOptional().HasMaxLength(500);
            Property(x => x.ProjectLevel).HasColumnName("PROJECTLEVEL").IsOptional().HasMaxLength(500);
            Property(x => x.CustomerLevel).HasColumnName("CUSTOMERLEVEL").IsOptional().HasMaxLength(500);
            Property(x => x.Phase).HasColumnName("PHASE").IsOptional().HasMaxLength(500);
            Property(x => x.Major).HasColumnName("MAJOR").IsOptional().HasMaxLength(500);
            Property(x => x.ChargerDeptName).HasColumnName("CHARGERDEPTNAME").IsOptional().HasMaxLength(500);
            Property(x => x.ChargerDept).HasColumnName("CHARGERDEPT").IsOptional().HasMaxLength(500);
            Property(x => x.ChargerUserName).HasColumnName("CHARGERUSERNAME").IsOptional().HasMaxLength(500);
            Property(x => x.ChargerUser).HasColumnName("CHARGERUSER").IsOptional().HasMaxLength(500);
            Property(x => x.MarketDeptName).HasColumnName("MARKETDEPTNAME").IsOptional().HasMaxLength(500);
            Property(x => x.MarketDept).HasColumnName("MARKETDEPT").IsOptional().HasMaxLength(500);
            Property(x => x.BusinessChargerUserName).HasColumnName("BUSINESSCHARGERUSERNAME").IsOptional().HasMaxLength(50);
            Property(x => x.BusinessChargerUser).HasColumnName("BUSINESSCHARGERUSER").IsOptional().HasMaxLength(50);
            Property(x => x.OtherDeptName).HasColumnName("OTHERDEPTNAME").IsOptional().HasMaxLength(500);
            Property(x => x.OtherDept).HasColumnName("OTHERDEPT").IsOptional().HasMaxLength(500);
            Property(x => x.Country).HasColumnName("COUNTRY").IsOptional().HasMaxLength(50);
            Property(x => x.Province).HasColumnName("PROVINCE").IsOptional().HasMaxLength(50);
            Property(x => x.City).HasColumnName("CITY").IsOptional().HasMaxLength(50);
            Property(x => x.BuildAddress).HasColumnName("BUILDADDRESS").IsOptional().HasMaxLength(500);
            Property(x => x.State).HasColumnName("STATE").IsOptional().HasMaxLength(50);
            Property(x => x.Remark).HasColumnName("REMARK").IsOptional().HasMaxLength(2000);
            Property(x => x.Attachments).HasColumnName("ATTACHMENTS").IsOptional().HasMaxLength(2000);
            Property(x => x.MakertClueID).HasColumnName("MAKERTCLUEID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.TasKNoticeID).HasColumnName("TASKNOTICEID").IsOptional().HasMaxLength(50);
            Property(x => x.EngineeringInfo).HasColumnName("ENGINEERINGINFO").IsOptional().HasMaxLength(50);
            Property(x => x.OrgID).HasColumnName("ORGID").IsOptional().HasMaxLength(500);
            Property(x => x.OrgName).HasColumnName("ORGNAME").IsOptional().HasMaxLength(500);
            Property(x => x.TasKNoticeTmplCode).HasColumnName("TASKNOTICETMPLCODE").IsOptional().HasMaxLength(200);
            Property(x => x.Area).HasColumnName("AREA").IsOptional().HasMaxLength(50);
        }
    }

    // S_KPI_Category
    internal partial class S_KPI_CategoryConfiguration : EntityTypeConfiguration<S_KPI_Category>
    {
        public S_KPI_CategoryConfiguration()
        {
            ToTable("S_KPI_CATEGORY");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.Name).HasColumnName("NAME").IsRequired().HasMaxLength(500);
            Property(x => x.Code).HasColumnName("CODE").IsRequired().HasMaxLength(500);
            Property(x => x.OrgType).HasColumnName("ORGTYPE").IsOptional().HasMaxLength(500);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsRequired();
            Property(x => x.State).HasColumnName("STATE").IsOptional().HasMaxLength(50);
        }
    }

    // S_KPI_IndicatorCompany
    internal partial class S_KPI_IndicatorCompanyConfiguration : EntityTypeConfiguration<S_KPI_IndicatorCompany>
    {
        public S_KPI_IndicatorCompanyConfiguration()
        {
            ToTable("S_KPI_INDICATORCOMPANY");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.BelongYear).HasColumnName("BELONGYEAR").IsRequired();
            Property(x => x.BelongMonth).HasColumnName("BELONGMONTH").IsOptional();
            Property(x => x.BelongQuarter).HasColumnName("BELONGQUARTER").IsOptional();
            Property(x => x.IndicatorType).HasColumnName("INDICATORTYPE").IsRequired().HasMaxLength(200);
            Property(x => x.BusiniessCategory).HasColumnName("BUSINIESSCATEGORY").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectValue).HasColumnName("PROJECTVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.ProjectValueCost).HasColumnName("PROJECTVALUECOST").IsOptional().HasPrecision(18,4);
            Property(x => x.ContractValue).HasColumnName("CONTRACTVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.ContractValueCost).HasColumnName("CONTRACTVALUECOST").IsOptional().HasPrecision(18,4);
            Property(x => x.ReceiptValue).HasColumnName("RECEIPTVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.ReceiptValueCost).HasColumnName("RECEIPTVALUECOST").IsOptional().HasPrecision(18,4);
            Property(x => x.ContractValue2).HasColumnName("CONTRACTVALUE2").IsOptional().HasPrecision(18,4);
            Property(x => x.ContractValue2Cost).HasColumnName("CONTRACTVALUE2COST").IsOptional().HasPrecision(18,4);
            Property(x => x.PlanReceiptValue).HasColumnName("PLANRECEIPTVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.PlanReceiptValueCost).HasColumnName("PLANRECEIPTVALUECOST").IsOptional().HasPrecision(18,4);
            Property(x => x.ProductionValue).HasColumnName("PRODUCTIONVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.ProductionValueCost).HasColumnName("PRODUCTIONVALUECOST").IsOptional().HasPrecision(18,4);
        }
    }

    // S_KPI_IndicatorConfig
    internal partial class S_KPI_IndicatorConfigConfiguration : EntityTypeConfiguration<S_KPI_IndicatorConfig>
    {
        public S_KPI_IndicatorConfigConfiguration()
        {
            ToTable("S_KPI_INDICATORCONFIG");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.IndicatorName).HasColumnName("INDICATORNAME").IsRequired().HasMaxLength(200);
            Property(x => x.IndicatorCode).HasColumnName("INDICATORCODE").IsRequired().HasMaxLength(200);
            Property(x => x.DataSource).HasColumnName("DATASOURCE").IsOptional().HasMaxLength(200);
            Property(x => x.RelateCaculateTable).HasColumnName("RELATECACULATETABLE").IsRequired().HasMaxLength(200);
            Property(x => x.RelateCaculateField).HasColumnName("RELATECACULATEFIELD").IsRequired().HasMaxLength(200);
            Property(x => x.RelateUserField).HasColumnName("RELATEUSERFIELD").IsOptional().HasMaxLength(200);
            Property(x => x.RelateUserNameField).HasColumnName("RELATEUSERNAMEFIELD").IsOptional().HasMaxLength(200);
            Property(x => x.RelateOrgField).HasColumnName("RELATEORGFIELD").IsOptional().HasMaxLength(200);
            Property(x => x.RelateOrgNameField).HasColumnName("RELATEORGNAMEFIELD").IsOptional().HasMaxLength(200);
            Property(x => x.YearField).HasColumnName("YEARFIELD").IsOptional().HasMaxLength(200);
            Property(x => x.MonthField).HasColumnName("MONTHFIELD").IsOptional().HasMaxLength(200);
            Property(x => x.QuarterField).HasColumnName("QUARTERFIELD").IsOptional().HasMaxLength(200);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.State).HasColumnName("STATE").IsOptional().HasMaxLength(200);
            Property(x => x.OrgType).HasColumnName("ORGTYPE").IsOptional().HasMaxLength(200);
        }
    }

    // S_KPI_IndicatorOrg
    internal partial class S_KPI_IndicatorOrgConfiguration : EntityTypeConfiguration<S_KPI_IndicatorOrg>
    {
        public S_KPI_IndicatorOrgConfiguration()
        {
            ToTable("S_KPI_INDICATORORG");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.BelongYear).HasColumnName("BELONGYEAR").IsRequired();
            Property(x => x.BelongMonth).HasColumnName("BELONGMONTH").IsOptional();
            Property(x => x.BelongQuarter).HasColumnName("BELONGQUARTER").IsOptional();
            Property(x => x.OrgID).HasColumnName("ORGID").IsRequired().HasMaxLength(200);
            Property(x => x.OrgName).HasColumnName("ORGNAME").IsRequired().HasMaxLength(500);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IndicatorType).HasColumnName("INDICATORTYPE").IsRequired().HasMaxLength(200);
            Property(x => x.BusiniessCategory).HasColumnName("BUSINIESSCATEGORY").IsOptional().HasMaxLength(200);
            Property(x => x.ParentIndicatorID).HasColumnName("PARENTINDICATORID").IsOptional().HasMaxLength(50);
            Property(x => x.ProjectValue).HasColumnName("PROJECTVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.ProjectValueCost).HasColumnName("PROJECTVALUECOST").IsOptional().HasPrecision(18,4);
            Property(x => x.ContractValue).HasColumnName("CONTRACTVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.ContractValueCost).HasColumnName("CONTRACTVALUECOST").IsOptional().HasPrecision(18,4);
            Property(x => x.ReceiptValue).HasColumnName("RECEIPTVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.ReceiptValueCost).HasColumnName("RECEIPTVALUECOST").IsOptional().HasPrecision(18,4);
            Property(x => x.ContractValue2).HasColumnName("CONTRACTVALUE2").IsOptional().HasPrecision(18,4);
            Property(x => x.ContractValue2Cost).HasColumnName("CONTRACTVALUE2COST").IsOptional().HasPrecision(18,4);
            Property(x => x.PlanReceiptValue).HasColumnName("PLANRECEIPTVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.PlanReceiptValueCost).HasColumnName("PLANRECEIPTVALUECOST").IsOptional().HasPrecision(18,4);
            Property(x => x.ProductionValue).HasColumnName("PRODUCTIONVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.ProductionValueCost).HasColumnName("PRODUCTIONVALUECOST").IsOptional().HasPrecision(18,4);
        }
    }

    // S_KPI_IndicatorPerson
    internal partial class S_KPI_IndicatorPersonConfiguration : EntityTypeConfiguration<S_KPI_IndicatorPerson>
    {
        public S_KPI_IndicatorPersonConfiguration()
        {
            ToTable("S_KPI_INDICATORPERSON");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.BelongYear).HasColumnName("BELONGYEAR").IsRequired();
            Property(x => x.BelongMonth).HasColumnName("BELONGMONTH").IsOptional();
            Property(x => x.BelongQuarter).HasColumnName("BELONGQUARTER").IsOptional();
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(500);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(50);
            Property(x => x.UserID).HasColumnName("USERID").IsRequired().HasMaxLength(50);
            Property(x => x.UserName).HasColumnName("USERNAME").IsRequired().HasMaxLength(50);
            Property(x => x.IndicatorType).HasColumnName("INDICATORTYPE").IsRequired().HasMaxLength(200);
            Property(x => x.BusiniessCategory).HasColumnName("BUSINIESSCATEGORY").IsOptional().HasMaxLength(200);
            Property(x => x.ParentIndicatorID).HasColumnName("PARENTINDICATORID").IsOptional().HasMaxLength(50);
            Property(x => x.ProjectValue).HasColumnName("PROJECTVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.ProjectValueCost).HasColumnName("PROJECTVALUECOST").IsOptional().HasPrecision(18,4);
            Property(x => x.ContractValue).HasColumnName("CONTRACTVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.ContractValueCost).HasColumnName("CONTRACTVALUECOST").IsOptional().HasPrecision(18,4);
            Property(x => x.ReceiptValue).HasColumnName("RECEIPTVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.ReceiptValueCost).HasColumnName("RECEIPTVALUECOST").IsOptional().HasPrecision(18,4);
            Property(x => x.ContractValue2).HasColumnName("CONTRACTVALUE2").IsOptional().HasPrecision(18,4);
            Property(x => x.ContractValue2Cost).HasColumnName("CONTRACTVALUE2COST").IsOptional().HasPrecision(18,4);
            Property(x => x.PlanReceiptValue).HasColumnName("PLANRECEIPTVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.PlanReceiptValueCost).HasColumnName("PLANRECEIPTVALUECOST").IsOptional().HasPrecision(18,4);
            Property(x => x.ProductionValue).HasColumnName("PRODUCTIONVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.ProductionValueCost).HasColumnName("PRODUCTIONVALUECOST").IsOptional().HasPrecision(18,4);
        }
    }

    // S_KPITemp_IndicatorCompany
    internal partial class S_KPITemp_IndicatorCompanyConfiguration : EntityTypeConfiguration<S_KPITemp_IndicatorCompany>
    {
        public S_KPITemp_IndicatorCompanyConfiguration()
        {
            ToTable("S_KPITEMP_INDICATORCOMPANY");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.BelongYear).HasColumnName("BELONGYEAR").IsRequired();
            Property(x => x.BelongMonth).HasColumnName("BELONGMONTH").IsOptional();
            Property(x => x.BelongQuarter).HasColumnName("BELONGQUARTER").IsOptional();
            Property(x => x.IndicatorType).HasColumnName("INDICATORTYPE").IsRequired().HasMaxLength(500);
            Property(x => x.BusiniessCategory).HasColumnName("BUSINIESSCATEGORY").IsOptional().HasMaxLength(500);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsRequired();
            Property(x => x.Version).HasColumnName("VERSION").IsRequired();
            Property(x => x.CurrentVersion).HasColumnName("CURRENTVERSION").IsRequired().HasMaxLength(50);
            Property(x => x.ProjectValue).HasColumnName("PROJECTVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.ProjectValueCost).HasColumnName("PROJECTVALUECOST").IsOptional().HasPrecision(18,4);
            Property(x => x.ContractValue).HasColumnName("CONTRACTVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.ContractValueCost).HasColumnName("CONTRACTVALUECOST").IsOptional().HasPrecision(18,4);
            Property(x => x.ReceiptValue).HasColumnName("RECEIPTVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.ReceiptValueCost).HasColumnName("RECEIPTVALUECOST").IsOptional().HasPrecision(18,4);
            Property(x => x.ContractValue2).HasColumnName("CONTRACTVALUE2").IsOptional().HasPrecision(18,4);
            Property(x => x.ContractValue2Cost).HasColumnName("CONTRACTVALUE2COST").IsOptional().HasPrecision(18,4);
            Property(x => x.PlanReceiptValue).HasColumnName("PLANRECEIPTVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.PlanReceiptValueCost).HasColumnName("PLANRECEIPTVALUECOST").IsOptional().HasPrecision(18,4);
            Property(x => x.ProductionValue).HasColumnName("PRODUCTIONVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.ProductionValueCost).HasColumnName("PRODUCTIONVALUECOST").IsOptional().HasPrecision(18,4);
        }
    }

    // S_KPITemp_IndicatorOrg
    internal partial class S_KPITemp_IndicatorOrgConfiguration : EntityTypeConfiguration<S_KPITemp_IndicatorOrg>
    {
        public S_KPITemp_IndicatorOrgConfiguration()
        {
            ToTable("S_KPITEMP_INDICATORORG");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.BelongYear).HasColumnName("BELONGYEAR").IsRequired();
            Property(x => x.BelongMonth).HasColumnName("BELONGMONTH").IsOptional();
            Property(x => x.BelongQuarter).HasColumnName("BELONGQUARTER").IsOptional();
            Property(x => x.OrgID).HasColumnName("ORGID").IsRequired().HasMaxLength(500);
            Property(x => x.OrgName).HasColumnName("ORGNAME").IsRequired().HasMaxLength(500);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IndicatorType).HasColumnName("INDICATORTYPE").IsOptional().HasMaxLength(200);
            Property(x => x.BusiniessCategory).HasColumnName("BUSINIESSCATEGORY").IsOptional().HasMaxLength(500);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsRequired();
            Property(x => x.Version).HasColumnName("VERSION").IsRequired();
            Property(x => x.CurrentVersion).HasColumnName("CURRENTVERSION").IsRequired().HasMaxLength(50);
            Property(x => x.ProjectValue).HasColumnName("PROJECTVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.ProjectValueCost).HasColumnName("PROJECTVALUECOST").IsOptional().HasPrecision(18,4);
            Property(x => x.ContractValue).HasColumnName("CONTRACTVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.ContractValueCost).HasColumnName("CONTRACTVALUECOST").IsOptional().HasPrecision(18,4);
            Property(x => x.ReceiptValue).HasColumnName("RECEIPTVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.ReceiptValueCost).HasColumnName("RECEIPTVALUECOST").IsOptional().HasPrecision(18,4);
            Property(x => x.ContractValue2).HasColumnName("CONTRACTVALUE2").IsOptional().HasPrecision(18,4);
            Property(x => x.ContractValue2Cost).HasColumnName("CONTRACTVALUE2COST").IsOptional().HasPrecision(18,4);
            Property(x => x.PlanReceiptValue).HasColumnName("PLANRECEIPTVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.PlanReceiptValueCost).HasColumnName("PLANRECEIPTVALUECOST").IsOptional().HasPrecision(18,4);
            Property(x => x.ProductionValue).HasColumnName("PRODUCTIONVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.ProductionValueCost).HasColumnName("PRODUCTIONVALUECOST").IsOptional().HasPrecision(18,4);
        }
    }

    // S_KPITemp_IndicatorPerson
    internal partial class S_KPITemp_IndicatorPersonConfiguration : EntityTypeConfiguration<S_KPITemp_IndicatorPerson>
    {
        public S_KPITemp_IndicatorPersonConfiguration()
        {
            ToTable("S_KPITEMP_INDICATORPERSON");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.BelongYear).HasColumnName("BELONGYEAR").IsRequired();
            Property(x => x.BelongMonth).HasColumnName("BELONGMONTH").IsOptional();
            Property(x => x.BelongQuarter).HasColumnName("BELONGQUARTER").IsOptional();
            Property(x => x.UserID).HasColumnName("USERID").IsRequired().HasMaxLength(500);
            Property(x => x.UserName).HasColumnName("USERNAME").IsRequired().HasMaxLength(500);
            Property(x => x.IndicatorType).HasColumnName("INDICATORTYPE").IsRequired().HasMaxLength(500);
            Property(x => x.BusiniessCategory).HasColumnName("BUSINIESSCATEGORY").IsOptional().HasMaxLength(500);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsRequired();
            Property(x => x.Version).HasColumnName("VERSION").IsRequired();
            Property(x => x.CurrentVersion).HasColumnName("CURRENTVERSION").IsRequired().HasMaxLength(50);
            Property(x => x.ProjectValue).HasColumnName("PROJECTVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.ProjectValueCost).HasColumnName("PROJECTVALUECOST").IsOptional().HasPrecision(18,4);
            Property(x => x.ContractValue).HasColumnName("CONTRACTVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.ContractValueCost).HasColumnName("CONTRACTVALUECOST").IsOptional().HasPrecision(18,4);
            Property(x => x.ReceiptValue).HasColumnName("RECEIPTVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.ReceiptValueCost).HasColumnName("RECEIPTVALUECOST").IsOptional().HasPrecision(18,4);
            Property(x => x.ContractValue2).HasColumnName("CONTRACTVALUE2").IsOptional().HasPrecision(18,4);
            Property(x => x.ContractValue2Cost).HasColumnName("CONTRACTVALUE2COST").IsOptional().HasPrecision(18,4);
            Property(x => x.PlanReceiptValue).HasColumnName("PLANRECEIPTVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.PlanReceiptValueCost).HasColumnName("PLANRECEIPTVALUECOST").IsOptional().HasPrecision(18,4);
            Property(x => x.ProductionValue).HasColumnName("PRODUCTIONVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.ProductionValueCost).HasColumnName("PRODUCTIONVALUECOST").IsOptional().HasPrecision(18,4);
        }
    }

    // S_P_MarketClue
    internal partial class S_P_MarketClueConfiguration : EntityTypeConfiguration<S_P_MarketClue>
    {
        public S_P_MarketClueConfiguration()
        {
            ToTable("S_P_MARKETCLUE");
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
            Property(x => x.CustomerInfo).HasColumnName("CUSTOMERINFO").IsOptional().HasMaxLength(200);
            Property(x => x.CustomerInfoName).HasColumnName("CUSTOMERINFONAME").IsOptional().HasMaxLength(200);
            Property(x => x.CustomerInfoSub).HasColumnName("CUSTOMERINFOSUB").IsOptional().HasMaxLength(200);
            Property(x => x.CustomerInfoSubName).HasColumnName("CUSTOMERINFOSUBNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(200);
            Property(x => x.SerialNumber).HasColumnName("SERIALNUMBER").IsOptional().HasMaxLength(200);
            Property(x => x.Address).HasColumnName("ADDRESS").IsOptional().HasMaxLength(200);
            Property(x => x.Country).HasColumnName("COUNTRY").IsOptional().HasMaxLength(200);
            Property(x => x.Province).HasColumnName("PROVINCE").IsOptional().HasMaxLength(200);
            Property(x => x.City).HasColumnName("CITY").IsOptional().HasMaxLength(200);
            Property(x => x.Content).HasColumnName("CONTENT").IsOptional().HasMaxLength(500);
            Property(x => x.Investment).HasColumnName("INVESTMENT").IsOptional().HasMaxLength(200);
            Property(x => x.ContractValue).HasColumnName("CONTRACTVALUE").IsOptional().HasMaxLength(200);
            Property(x => x.BusinessManager).HasColumnName("BUSINESSMANAGER").IsOptional().HasMaxLength(200);
            Property(x => x.BusinessManagerName).HasColumnName("BUSINESSMANAGERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.TechCharger).HasColumnName("TECHCHARGER").IsOptional().HasMaxLength(200);
            Property(x => x.TechChargerName).HasColumnName("TECHCHARGERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.OtherUser).HasColumnName("OTHERUSER").IsOptional().HasMaxLength(200);
            Property(x => x.OtherUserName).HasColumnName("OTHERUSERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.RequiredStartDate).HasColumnName("REQUIREDSTARTDATE").IsOptional();
            Property(x => x.RequiredFinishDate).HasColumnName("REQUIREDFINISHDATE").IsOptional();
            Property(x => x.Competitor).HasColumnName("COMPETITOR").IsOptional().HasMaxLength(500);
            Property(x => x.Remark).HasColumnName("REMARK").IsOptional().HasMaxLength(500);
            Property(x => x.Attachment).HasColumnName("ATTACHMENT").IsOptional().HasMaxLength(500);
            Property(x => x.State).HasColumnName("STATE").IsOptional().HasMaxLength(200);
            Property(x => x.EngineeringInfo).HasColumnName("ENGINEERINGINFO").IsOptional().HasMaxLength(200);
            Property(x => x.EngineeringInfoName).HasColumnName("ENGINEERINGINFONAME").IsOptional().HasMaxLength(200);
            Property(x => x.DeptInfo).HasColumnName("DEPTINFO").IsOptional().HasMaxLength(200);
            Property(x => x.DeptInfoName).HasColumnName("DEPTINFONAME").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectClass).HasColumnName("PROJECTCLASS").IsOptional().HasMaxLength(200);
            Property(x => x.Source).HasColumnName("SOURCE").IsOptional().HasMaxLength(200);
        }
    }

    // S_P_MarketClue_TraceInfo
    internal partial class S_P_MarketClue_TraceInfoConfiguration : EntityTypeConfiguration<S_P_MarketClue_TraceInfo>
    {
        public S_P_MarketClue_TraceInfoConfiguration()
        {
            ToTable("S_P_MARKETCLUE_TRACEINFO");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.S_P_MarketClueID).HasColumnName("S_P_MARKETCLUEID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.TraceContent).HasColumnName("TRACECONTENT").IsOptional().HasMaxLength(200);
            Property(x => x.NextStep).HasColumnName("NEXTSTEP").IsOptional().HasMaxLength(200);
            Property(x => x.TraceType).HasColumnName("TRACETYPE").IsOptional().HasMaxLength(200);
            Property(x => x.Contact).HasColumnName("CONTACT").IsOptional().HasMaxLength(200);
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsOptional().HasMaxLength(200);
            Property(x => x.CreateUserName).HasColumnName("CREATEUSERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();

            // Foreign keys
            HasOptional(a => a.S_P_MarketClue).WithMany(b => b.S_P_MarketClue_TraceInfo).HasForeignKey(c => c.S_P_MarketClueID); // FK_S_P_MarketClue_TraceInfo_S_P_MarketClue
        }
    }

    // S_SP_Invoice
    internal partial class S_SP_InvoiceConfiguration : EntityTypeConfiguration<S_SP_Invoice>
    {
        public S_SP_InvoiceConfiguration()
        {
            ToTable("S_SP_INVOICE");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.InvoiceCode).HasColumnName("INVOICECODE").IsOptional().HasMaxLength(50);
            Property(x => x.InvoiceType).HasColumnName("INVOICETYPE").IsOptional().HasMaxLength(50);
            Property(x => x.InvoiceReciever).HasColumnName("INVOICERECIEVER").IsOptional().HasMaxLength(50);
            Property(x => x.InvoiceRecieverName).HasColumnName("INVOICERECIEVERNAME").IsOptional().HasMaxLength(50);
            Property(x => x.InvoiceDate).HasColumnName("INVOICEDATE").IsOptional();
            Property(x => x.ManagerContract).HasColumnName("MANAGERCONTRACT").IsOptional().HasMaxLength(50);
            Property(x => x.ManagerContractName).HasColumnName("MANAGERCONTRACTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.SupplierContract).HasColumnName("SUPPLIERCONTRACT").IsOptional().HasMaxLength(50);
            Property(x => x.SupplierContractName).HasColumnName("SUPPLIERCONTRACTNAME").IsOptional().HasMaxLength(50);
            Property(x => x.Supplier).HasColumnName("SUPPLIER").IsOptional().HasMaxLength(50);
            Property(x => x.SupplierName).HasColumnName("SUPPLIERNAME").IsOptional().HasMaxLength(50);
            Property(x => x.CostUnit).HasColumnName("COSTUNIT").IsOptional().HasMaxLength(50);
            Property(x => x.CostUnitName).HasColumnName("COSTUNITNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Amount).HasColumnName("AMOUNT").IsOptional().HasPrecision(18,4);
            Property(x => x.BelongYear).HasColumnName("BELONGYEAR").IsOptional();
            Property(x => x.BelongQuarter).HasColumnName("BELONGQUARTER").IsOptional();
            Property(x => x.BelongMonth).HasColumnName("BELONGMONTH").IsOptional();
            Property(x => x.State).HasColumnName("STATE").IsOptional().HasMaxLength(50);
            Property(x => x.Remark).HasColumnName("REMARK").IsOptional().HasMaxLength(500);
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsOptional().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUser).HasColumnName("MODIFYUSER").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyDate).HasColumnName("MODIFYDATE").IsOptional();
            Property(x => x.TaxRate).HasColumnName("TAXRATE").IsOptional().HasPrecision(18,4);
            Property(x => x.TaxName).HasColumnName("TAXNAME").IsOptional().HasMaxLength(200);
            Property(x => x.TaxValue).HasColumnName("TAXVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.ClearValue).HasColumnName("CLEARVALUE").IsOptional().HasPrecision(18,4);

            // Foreign keys
            HasOptional(a => a.S_SP_SupplierContract).WithMany(b => b.S_SP_Invoice).HasForeignKey(c => c.SupplierContract); // FK_S_SP_Invoice_S_SP_SupplierContract
        }
    }

    // S_SP_Payment
    internal partial class S_SP_PaymentConfiguration : EntityTypeConfiguration<S_SP_Payment>
    {
        public S_SP_PaymentConfiguration()
        {
            ToTable("S_SP_PAYMENT");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.Contract).HasColumnName("CONTRACT").IsOptional().HasMaxLength(50);
            Property(x => x.ContractName).HasColumnName("CONTRACTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Supplier).HasColumnName("SUPPLIER").IsOptional().HasMaxLength(50);
            Property(x => x.SupplierName).HasColumnName("SUPPLIERNAME").IsOptional().HasMaxLength(50);
            Property(x => x.PaymentValue).HasColumnName("PAYMENTVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.PaymentDate).HasColumnName("PAYMENTDATE").IsOptional();
            Property(x => x.BelongYear).HasColumnName("BELONGYEAR").IsOptional();
            Property(x => x.BelongMonth).HasColumnName("BELONGMONTH").IsOptional();
            Property(x => x.BelongQuarter).HasColumnName("BELONGQUARTER").IsOptional();
            Property(x => x.PaymentUser).HasColumnName("PAYMENTUSER").IsOptional().HasMaxLength(50);
            Property(x => x.PaymentUserName).HasColumnName("PAYMENTUSERNAME").IsOptional().HasMaxLength(50);
            Property(x => x.Remark).HasColumnName("REMARK").IsOptional().HasMaxLength(500);
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsOptional().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.ModifyUser).HasColumnName("MODIFYUSER").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyDate).HasColumnName("MODIFYDATE").IsOptional();
            Property(x => x.PaymentCode).HasColumnName("PAYMENTCODE").IsOptional().HasMaxLength(50);
            Property(x => x.PaymentApplyID).HasColumnName("PAYMENTAPPLYID").IsOptional().HasMaxLength(50);
            Property(x => x.ManagerContract).HasColumnName("MANAGERCONTRACT").IsOptional().HasMaxLength(200);
            Property(x => x.ManagerContractName).HasColumnName("MANAGERCONTRACTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.InvoiceRelation).HasColumnName("INVOICERELATION").IsOptional();
            Property(x => x.AcceptanceBill).HasColumnName("ACCEPTANCEBILL").IsOptional();
            Property(x => x.BillValue).HasColumnName("BILLVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.PaymentWithoutBill).HasColumnName("PAYMENTWITHOUTBILL").IsOptional().HasPrecision(18,4);
            Property(x => x.CostInfo).HasColumnName("COSTINFO").IsOptional().HasMaxLength(1073741823);
            Property(x => x.BankAccount).HasColumnName("BANKACCOUNT").IsOptional().HasMaxLength(200);
            Property(x => x.BankAccountName).HasColumnName("BANKACCOUNTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.BankName).HasColumnName("BANKNAME").IsOptional().HasMaxLength(200);

            // Foreign keys
            HasOptional(a => a.S_SP_SupplierContract).WithMany(b => b.S_SP_Payment).HasForeignKey(c => c.Contract); // FK_S_SP_Payment_S_SP_SupplierContract
        }
    }

    // S_SP_Payment_AcceptanceBill
    internal partial class S_SP_Payment_AcceptanceBillConfiguration : EntityTypeConfiguration<S_SP_Payment_AcceptanceBill>
    {
        public S_SP_Payment_AcceptanceBillConfiguration()
        {
            ToTable("S_SP_PAYMENT_ACCEPTANCEBILL");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.S_SP_PaymentID).HasColumnName("S_SP_PAYMENTID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.SerialNumber).HasColumnName("SERIALNUMBER").IsOptional().HasMaxLength(200);
            Property(x => x.CustomerName).HasColumnName("CUSTOMERNAME").IsOptional().HasMaxLength(500);
            Property(x => x.BillDate).HasColumnName("BILLDATE").IsOptional();
            Property(x => x.Amount).HasColumnName("AMOUNT").IsOptional().HasPrecision(18,4);
            Property(x => x.AcceptanceBillID).HasColumnName("ACCEPTANCEBILLID").IsOptional().HasMaxLength(200);

            // Foreign keys
            HasOptional(a => a.S_SP_Payment).WithMany(b => b.S_SP_Payment_AcceptanceBill).HasForeignKey(c => c.S_SP_PaymentID); // FK_S_SP_Payment_AcceptanceBill_S_SP_Payment
        }
    }

    // S_SP_Payment_CostInfo
    internal partial class S_SP_Payment_CostInfoConfiguration : EntityTypeConfiguration<S_SP_Payment_CostInfo>
    {
        public S_SP_Payment_CostInfoConfiguration()
        {
            ToTable("S_SP_PAYMENT_COSTINFO");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.S_SP_PaymentID).HasColumnName("S_SP_PAYMENTID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(200);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(200);
            Property(x => x.ContractValue).HasColumnName("CONTRACTVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.CostValue).HasColumnName("COSTVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.CostUnitID).HasColumnName("COSTUNITID").IsOptional().HasMaxLength(200);

            // Foreign keys
            HasOptional(a => a.S_SP_Payment).WithMany(b => b.S_SP_Payment_CostInfo).HasForeignKey(c => c.S_SP_PaymentID); // FK_S_SP_Payment_CostInfo_S_SP_Payment
        }
    }

    // S_SP_Payment_InvoiceRelation
    internal partial class S_SP_Payment_InvoiceRelationConfiguration : EntityTypeConfiguration<S_SP_Payment_InvoiceRelation>
    {
        public S_SP_Payment_InvoiceRelationConfiguration()
        {
            ToTable("S_SP_PAYMENT_INVOICERELATION");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.S_SP_PaymentID).HasColumnName("S_SP_PAYMENTID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.InvoiceNo).HasColumnName("INVOICENO").IsOptional().HasMaxLength(200);
            Property(x => x.InvoiceDate).HasColumnName("INVOICEDATE").IsOptional().HasMaxLength(200);
            Property(x => x.InvoiceUnit).HasColumnName("INVOICEUNIT").IsOptional().HasMaxLength(200);
            Property(x => x.RelationValue).HasColumnName("RELATIONVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.RemainInvoiceValue).HasColumnName("REMAININVOICEVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.InvoiceID).HasColumnName("INVOICEID").IsOptional().HasMaxLength(200);
            Property(x => x.InvoiceRelationValue).HasColumnName("INVOICERELATIONVALUE").IsOptional().HasPrecision(18,4);

            // Foreign keys
            HasOptional(a => a.S_SP_Payment).WithMany(b => b.S_SP_Payment_InvoiceRelation).HasForeignKey(c => c.S_SP_PaymentID); // FK_S_SP_Payment_InvoiceRelation_S_SP_Payment
        }
    }

    // S_SP_PaymentPlan
    internal partial class S_SP_PaymentPlanConfiguration : EntityTypeConfiguration<S_SP_PaymentPlan>
    {
        public S_SP_PaymentPlanConfiguration()
        {
            ToTable("S_SP_PAYMENTPLAN");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.Name).HasColumnName("NAME").IsRequired().HasMaxLength(500);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(50);
            Property(x => x.SupplierContract).HasColumnName("SUPPLIERCONTRACT").IsRequired().HasMaxLength(50);
            Property(x => x.SupplierContractName).HasColumnName("SUPPLIERCONTRACTNAME").IsRequired().HasMaxLength(500);
            Property(x => x.Supplier).HasColumnName("SUPPLIER").IsOptional().HasMaxLength(50);
            Property(x => x.SupplierName).HasColumnName("SUPPLIERNAME").IsOptional().HasMaxLength(500);
            Property(x => x.PlanDate).HasColumnName("PLANDATE").IsOptional();
            Property(x => x.BelongYear).HasColumnName("BELONGYEAR").IsOptional();
            Property(x => x.BelongMonth).HasColumnName("BELONGMONTH").IsOptional();
            Property(x => x.BelongQuarter).HasColumnName("BELONGQUARTER").IsOptional();
            Property(x => x.PlanPaymentValue).HasColumnName("PLANPAYMENTVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.SummaryPaymentValue).HasColumnName("SUMMARYPAYMENTVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.State).HasColumnName("STATE").IsRequired().HasMaxLength(50);
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsOptional().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.ModifyUser).HasColumnName("MODIFYUSER").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyDate).HasColumnName("MODIFYDATE").IsOptional();
        }
    }

    // S_SP_Supplier
    internal partial class S_SP_SupplierConfiguration : EntityTypeConfiguration<S_SP_Supplier>
    {
        public S_SP_SupplierConfiguration()
        {
            ToTable("S_SP_SUPPLIER");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(200);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(200);
            Property(x => x.State).HasColumnName("STATE").IsOptional().HasMaxLength(50);
            Property(x => x.SimplyName).HasColumnName("SIMPLYNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ZipCode).HasColumnName("ZIPCODE").IsOptional().HasMaxLength(50);
            Property(x => x.Telephone).HasColumnName("TELEPHONE").IsOptional().HasMaxLength(50);
            Property(x => x.Fax).HasColumnName("FAX").IsOptional().HasMaxLength(50);
            Property(x => x.Email).HasColumnName("EMAIL").IsOptional().HasMaxLength(200);
            Property(x => x.HomePage).HasColumnName("HOMEPAGE").IsOptional().HasMaxLength(200);
            Property(x => x.Country).HasColumnName("COUNTRY").IsOptional().HasMaxLength(50);
            Property(x => x.Province).HasColumnName("PROVINCE").IsOptional().HasMaxLength(50);
            Property(x => x.City).HasColumnName("CITY").IsOptional().HasMaxLength(50);
            Property(x => x.Area).HasColumnName("AREA").IsOptional().HasMaxLength(50);
            Property(x => x.SupplierLevel).HasColumnName("SUPPLIERLEVEL").IsOptional().HasMaxLength(200);
            Property(x => x.FirstContactDate).HasColumnName("FIRSTCONTACTDATE").IsOptional();
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsOptional().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUser).HasColumnName("MODIFYUSER").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyDate).HasColumnName("MODIFYDATE").IsOptional();
            Property(x => x.ProvinceEnum).HasColumnName("PROVINCEENUM").IsOptional().HasMaxLength(50);
            Property(x => x.SyncCloudTime).HasColumnName("SYNCCLOUDTIME").IsOptional();
            Property(x => x.LinkUser).HasColumnName("LINKUSER").IsOptional().HasMaxLength(200);
            Property(x => x.LinkTelphone).HasColumnName("LINKTELPHONE").IsOptional().HasMaxLength(200);
            Property(x => x.BankInfo).HasColumnName("BANKINFO").IsOptional().HasMaxLength(1073741823);
            Property(x => x.ContactAddress).HasColumnName("CONTACTADDRESS").IsOptional().HasMaxLength(200);
        }
    }

    // S_SP_Supplier_BankInfo
    internal partial class S_SP_Supplier_BankInfoConfiguration : EntityTypeConfiguration<S_SP_Supplier_BankInfo>
    {
        public S_SP_Supplier_BankInfoConfiguration()
        {
            ToTable("S_SP_SUPPLIER_BANKINFO");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.S_SP_SupplierID).HasColumnName("S_SP_SUPPLIERID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.Bank).HasColumnName("BANK").IsOptional().HasMaxLength(200);
            Property(x => x.BankAccount).HasColumnName("BANKACCOUNT").IsOptional().HasMaxLength(200);
            Property(x => x.BankAccountName).HasColumnName("BANKACCOUNTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.BankAddress).HasColumnName("BANKADDRESS").IsOptional().HasMaxLength(200);
            Property(x => x.BankName).HasColumnName("BANKNAME").IsOptional().HasMaxLength(200);

            // Foreign keys
            HasOptional(a => a.S_SP_Supplier).WithMany(b => b.S_SP_Supplier_BankInfo).HasForeignKey(c => c.S_SP_SupplierID); // FK_S_SP_Supplier_BankInfo_S_SP_Supplier
        }
    }

    // S_SP_Supplier_ContactUserInfo
    internal partial class S_SP_Supplier_ContactUserInfoConfiguration : EntityTypeConfiguration<S_SP_Supplier_ContactUserInfo>
    {
        public S_SP_Supplier_ContactUserInfoConfiguration()
        {
            ToTable("S_SP_SUPPLIER_CONTACTUSERINFO");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.S_SP_SupplierID).HasColumnName("S_SP_SUPPLIERID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.ContactUser).HasColumnName("CONTACTUSER").IsOptional().HasMaxLength(200);
            Property(x => x.Phone).HasColumnName("PHONE").IsOptional().HasMaxLength(200);

            // Foreign keys
            HasOptional(a => a.S_SP_Supplier).WithMany(b => b.S_SP_Supplier_ContactUserInfo).HasForeignKey(c => c.S_SP_SupplierID); // FK_S_SP_Supplier_ContactUserInfo_S_SP_Supplier
        }
    }

    // S_SP_Supplier_CoopProjectInfo
    internal partial class S_SP_Supplier_CoopProjectInfoConfiguration : EntityTypeConfiguration<S_SP_Supplier_CoopProjectInfo>
    {
        public S_SP_Supplier_CoopProjectInfoConfiguration()
        {
            ToTable("S_SP_SUPPLIER_COOPPROJECTINFO");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.S_SP_SupplierID).HasColumnName("S_SP_SUPPLIERID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.Project).HasColumnName("PROJECT").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectName).HasColumnName("PROJECTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ChargerUser).HasColumnName("CHARGERUSER").IsOptional().HasMaxLength(200);
            Property(x => x.ChargerUserName).HasColumnName("CHARGERUSERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.DesignDept).HasColumnName("DESIGNDEPT").IsOptional().HasMaxLength(200);
            Property(x => x.DesignDeptName).HasColumnName("DESIGNDEPTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ProductName).HasColumnName("PRODUCTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Evaluate).HasColumnName("EVALUATE").IsOptional().HasPrecision(18,2);

            // Foreign keys
            HasOptional(a => a.S_SP_Supplier).WithMany(b => b.S_SP_Supplier_CoopProjectInfo).HasForeignKey(c => c.S_SP_SupplierID); // FK_S_SP_Supplier_CoopProjectInfo_S_SP_Supplier
        }
    }

    // S_SP_Supplier_CredentialInfo
    internal partial class S_SP_Supplier_CredentialInfoConfiguration : EntityTypeConfiguration<S_SP_Supplier_CredentialInfo>
    {
        public S_SP_Supplier_CredentialInfoConfiguration()
        {
            ToTable("S_SP_SUPPLIER_CREDENTIALINFO");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.S_SP_SupplierID).HasColumnName("S_SP_SUPPLIERID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.CertificateCode).HasColumnName("CERTIFICATECODE").IsOptional().HasMaxLength(200);
            Property(x => x.CertificateName).HasColumnName("CERTIFICATENAME").IsOptional().HasMaxLength(200);
            Property(x => x.ExpiryDate).HasColumnName("EXPIRYDATE").IsOptional();
            Property(x => x.Scope).HasColumnName("SCOPE").IsOptional().HasMaxLength(200);
            Property(x => x.Achievement).HasColumnName("ACHIEVEMENT").IsOptional().HasMaxLength(200);
            Property(x => x.Attachment).HasColumnName("ATTACHMENT").IsOptional().HasMaxLength(200);
            Property(x => x.AttachmentName).HasColumnName("ATTACHMENTNAME").IsOptional().HasMaxLength(200);

            // Foreign keys
            HasOptional(a => a.S_SP_Supplier).WithMany(b => b.S_SP_Supplier_CredentialInfo).HasForeignKey(c => c.S_SP_SupplierID); // FK_S_SP_Supplier_CredentialInfo_S_SP_Supplier
        }
    }

    // S_SP_SupplierContract
    internal partial class S_SP_SupplierContractConfiguration : EntityTypeConfiguration<S_SP_SupplierContract>
    {
        public S_SP_SupplierContractConfiguration()
        {
            ToTable("S_SP_SUPPLIERCONTRACT");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(200);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(200);
            Property(x => x.BelongYear).HasColumnName("BELONGYEAR").IsOptional();
            Property(x => x.BelongMonth).HasColumnName("BELONGMONTH").IsOptional();
            Property(x => x.BelongQuarter).HasColumnName("BELONGQUARTER").IsOptional();
            Property(x => x.Supplier).HasColumnName("SUPPLIER").IsOptional().HasMaxLength(200);
            Property(x => x.SupplierName).HasColumnName("SUPPLIERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ContractValue).HasColumnName("CONTRACTVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.SignDate).HasColumnName("SIGNDATE").IsOptional();
            Property(x => x.State).HasColumnName("STATE").IsOptional().HasMaxLength(50);
            Property(x => x.ManagerContract).HasColumnName("MANAGERCONTRACT").IsOptional().HasMaxLength(50);
            Property(x => x.ManagerContractName).HasColumnName("MANAGERCONTRACTNAME").IsOptional().HasMaxLength(50);
            Property(x => x.ManagerContractCode).HasColumnName("MANAGERCONTRACTCODE").IsOptional().HasMaxLength(200);
            Property(x => x.SupplierDept).HasColumnName("SUPPLIERDEPT").IsOptional().HasMaxLength(50);
            Property(x => x.SupplierDeptName).HasColumnName("SUPPLIERDEPTNAME").IsOptional().HasMaxLength(50);
            Property(x => x.SumPaymentValue).HasColumnName("SUMPAYMENTVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.SumSPInvoiceValue).HasColumnName("SUMSPINVOICEVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsOptional().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUser).HasColumnName("MODIFYUSER").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyDate).HasColumnName("MODIFYDATE").IsOptional();
            Property(x => x.ProjectInfo).HasColumnName("PROJECTINFO").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectInfoName).HasColumnName("PROJECTINFONAME").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectInfoCode).HasColumnName("PROJECTINFOCODE").IsOptional().HasMaxLength(200);
            Property(x => x.ContractAttachment).HasColumnName("CONTRACTATTACHMENT").IsOptional().HasMaxLength(500);
            Property(x => x.Attachments).HasColumnName("ATTACHMENTS").IsOptional().HasMaxLength(500);
            Property(x => x.Remark).HasColumnName("REMARK").IsOptional().HasMaxLength(500);
            Property(x => x.ContractClass).HasColumnName("CONTRACTCLASS").IsOptional().HasMaxLength(200);
            Property(x => x.MainContract).HasColumnName("MAINCONTRACT").IsOptional().HasMaxLength(200);
            Property(x => x.MainContractName).HasColumnName("MAINCONTRACTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.MainContractCode).HasColumnName("MAINCONTRACTCODE").IsOptional().HasMaxLength(200);
            Property(x => x.ThisContractValue).HasColumnName("THISCONTRACTVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.CostUnit).HasColumnName("COSTUNIT").IsOptional().HasMaxLength(1073741823);
            Property(x => x.ContractSplit).HasColumnName("CONTRACTSPLIT").IsOptional().HasMaxLength(1073741823);
            Property(x => x.TaxName).HasColumnName("TAXNAME").IsOptional().HasMaxLength(50);
            Property(x => x.TaxRate).HasColumnName("TAXRATE").IsOptional().HasPrecision(18,4);
            Property(x => x.SerialNumber).HasColumnName("SERIALNUMBER").IsOptional().HasMaxLength(200);
            Property(x => x.ContractAmount).HasColumnName("CONTRACTAMOUNT").IsOptional().HasPrecision(18,4);
            Property(x => x.ExchangeRate).HasColumnName("EXCHANGERATE").IsOptional().HasPrecision(18,4);
            Property(x => x.Currency).HasColumnName("CURRENCY").IsOptional().HasMaxLength(200);
            Property(x => x.ContractValueDX).HasColumnName("CONTRACTVALUEDX").IsOptional().HasMaxLength(200);
            Property(x => x.TaxValue).HasColumnName("TAXVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.ClearContractValue).HasColumnName("CLEARCONTRACTVALUE").IsOptional().HasPrecision(18,4);
        }
    }

    // S_SP_SupplierContract_ContractSplit
    internal partial class S_SP_SupplierContract_ContractSplitConfiguration : EntityTypeConfiguration<S_SP_SupplierContract_ContractSplit>
    {
        public S_SP_SupplierContract_ContractSplitConfiguration()
        {
            ToTable("S_SP_SUPPLIERCONTRACT_CONTRACTSPLIT");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.S_SP_SupplierContractID).HasColumnName("S_SP_SUPPLIERCONTRACTID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.CostUnitID).HasColumnName("COSTUNITID").IsOptional().HasMaxLength(50);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(200);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(200);
            Property(x => x.ChargerUser).HasColumnName("CHARGERUSER").IsOptional().HasMaxLength(200);
            Property(x => x.ChargerUserName).HasColumnName("CHARGERUSERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ChargerDept).HasColumnName("CHARGERDEPT").IsOptional().HasMaxLength(200);
            Property(x => x.ChargerDeptName).HasColumnName("CHARGERDEPTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.SplitValue).HasColumnName("SPLITVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.MinContractValue).HasColumnName("MINCONTRACTVALUE").IsOptional().HasPrecision(18,4);

            // Foreign keys
            HasOptional(a => a.S_SP_SupplierContract).WithMany(b => b.S_SP_SupplierContract_ContractSplit).HasForeignKey(c => c.S_SP_SupplierContractID); // FK_S_SP_SupplierContract_CostUnit_S_SP_SupplierContract
        }
    }

    // S_SP_SupplierContract_Supplementary
    internal partial class S_SP_SupplierContract_SupplementaryConfiguration : EntityTypeConfiguration<S_SP_SupplierContract_Supplementary>
    {
        public S_SP_SupplierContract_SupplementaryConfiguration()
        {
            ToTable("S_SP_SUPPLIERCONTRACT_SUPPLEMENTARY");
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
            Property(x => x.MainContract).HasColumnName("MAINCONTRACT").IsOptional().HasMaxLength(200);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(200);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(200);
            Property(x => x.ContractValue).HasColumnName("CONTRACTVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.SignDate).HasColumnName("SIGNDATE").IsOptional();
            Property(x => x.OperateUser).HasColumnName("OPERATEUSER").IsOptional().HasMaxLength(200);
            Property(x => x.OperateUserName).HasColumnName("OPERATEUSERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ContractAttachment).HasColumnName("CONTRACTATTACHMENT").IsOptional().HasMaxLength(500);
            Property(x => x.Attachments).HasColumnName("ATTACHMENTS").IsOptional().HasMaxLength(500);
            Property(x => x.Remark).HasColumnName("REMARK").IsOptional().HasMaxLength(500);
            Property(x => x.BelongYear).HasColumnName("BELONGYEAR").IsOptional();
            Property(x => x.BelongMonth).HasColumnName("BELONGMONTH").IsOptional();
            Property(x => x.BelongQuarter).HasColumnName("BELONGQUARTER").IsOptional();
        }
    }

    // T_B_BidApply
    internal partial class T_B_BidApplyConfiguration : EntityTypeConfiguration<T_B_BidApply>
    {
        public T_B_BidApplyConfiguration()
        {
            ToTable("T_B_BIDAPPLY");
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
            Property(x => x.Project).HasColumnName("PROJECT").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectName).HasColumnName("PROJECTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.BidCode).HasColumnName("BIDCODE").IsOptional().HasMaxLength(200);
            Property(x => x.BidPhase).HasColumnName("BIDPHASE").IsOptional().HasMaxLength(200);
            Property(x => x.EngineeringInfo).HasColumnName("ENGINEERINGINFO").IsOptional().HasMaxLength(50);
            Property(x => x.EngineeringInfoName).HasColumnName("ENGINEERINGINFONAME").IsOptional().HasMaxLength(50);
            Property(x => x.BidIssuedDate).HasColumnName("BIDISSUEDDATE").IsOptional();
            Property(x => x.CompetencyDate).HasColumnName("COMPETENCYDATE").IsOptional();
            Property(x => x.TenderCode).HasColumnName("TENDERCODE").IsOptional().HasMaxLength(200);
            Property(x => x.TenderOrgan).HasColumnName("TENDERORGAN").IsOptional().HasMaxLength(200);
            Property(x => x.BusinessClass).HasColumnName("BUSINESSCLASS").IsOptional().HasMaxLength(200);
            Property(x => x.BusinessOwner).HasColumnName("BUSINESSOWNER").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectManager).HasColumnName("PROJECTMANAGER").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectManagerName).HasColumnName("PROJECTMANAGERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.DeptInfo).HasColumnName("DEPTINFO").IsOptional().HasMaxLength(200);
            Property(x => x.DeptInfoName).HasColumnName("DEPTINFONAME").IsOptional().HasMaxLength(200);
            Property(x => x.CompleteBidDate).HasColumnName("COMPLETEBIDDATE").IsOptional();
            Property(x => x.PostCode).HasColumnName("POSTCODE").IsOptional().HasMaxLength(200);
            Property(x => x.Address).HasColumnName("ADDRESS").IsOptional().HasMaxLength(200);
            Property(x => x.Remark).HasColumnName("REMARK").IsOptional().HasMaxLength(500);
            Property(x => x.InviteBidFile).HasColumnName("INVITEBIDFILE").IsOptional().HasMaxLength(500);
            Property(x => x.InviteBidClearFile).HasColumnName("INVITEBIDCLEARFILE").IsOptional().HasMaxLength(500);
            Property(x => x.InviteBidOtherFile).HasColumnName("INVITEBIDOTHERFILE").IsOptional().HasMaxLength(500);
            Property(x => x.BusinessOwnerName).HasColumnName("BUSINESSOWNERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Agree).HasColumnName("AGREE").IsOptional().HasMaxLength(50);
            Property(x => x.FinalOpinion).HasColumnName("FINALOPINION").IsOptional();
        }
    }

    // T_B_BidProcessApply
    internal partial class T_B_BidProcessApplyConfiguration : EntityTypeConfiguration<T_B_BidProcessApply>
    {
        public T_B_BidProcessApplyConfiguration()
        {
            ToTable("T_B_BIDPROCESSAPPLY");
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
            Property(x => x.BidCode).HasColumnName("BIDCODE").IsOptional().HasMaxLength(200);
            Property(x => x.BidPhase).HasColumnName("BIDPHASE").IsOptional().HasMaxLength(200);
            Property(x => x.BidIssuedDate).HasColumnName("BIDISSUEDDATE").IsOptional();
            Property(x => x.Remark).HasColumnName("REMARK").IsOptional().HasMaxLength(500);
            Property(x => x.BidFile).HasColumnName("BIDFILE").IsOptional().HasMaxLength(500);
            Property(x => x.BidClearFile).HasColumnName("BIDCLEARFILE").IsOptional().HasMaxLength(500);
            Property(x => x.BidOtherFile).HasColumnName("BIDOTHERFILE").IsOptional().HasMaxLength(500);
            Property(x => x.BidID).HasColumnName("BIDID").IsOptional().HasMaxLength(50);
            Property(x => x.Project).HasColumnName("PROJECT").IsOptional().HasMaxLength(50);
        }
    }

    // T_B_BondApply
    internal partial class T_B_BondApplyConfiguration : EntityTypeConfiguration<T_B_BondApply>
    {
        public T_B_BondApplyConfiguration()
        {
            ToTable("T_B_BONDAPPLY");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.Project).HasColumnName("PROJECT").IsOptional().HasMaxLength(200);
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
            Property(x => x.ApplyDate).HasColumnName("APPLYDATE").IsOptional();
            Property(x => x.ProjectCode).HasColumnName("PROJECTCODE").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectMangerName).HasColumnName("PROJECTMANGERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Amount).HasColumnName("AMOUNT").IsOptional().HasPrecision(18,4);
            Property(x => x.ExpiryDate).HasColumnName("EXPIRYDATE").IsOptional();
            Property(x => x.TransferUnit).HasColumnName("TRANSFERUNIT").IsOptional().HasMaxLength(200);
            Property(x => x.BankName).HasColumnName("BANKNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Account).HasColumnName("ACCOUNT").IsOptional().HasMaxLength(200);
            Property(x => x.Remark).HasColumnName("REMARK").IsOptional().HasMaxLength(500);
            Property(x => x.Applier).HasColumnName("APPLIER").IsOptional().HasMaxLength(50);
            Property(x => x.ApplierName).HasColumnName("APPLIERNAME").IsOptional().HasMaxLength(50);
            Property(x => x.DepartInfo).HasColumnName("DEPARTINFO").IsOptional().HasMaxLength(50);
            Property(x => x.DepartInfoName).HasColumnName("DEPARTINFONAME").IsOptional().HasMaxLength(50);
            Property(x => x.Attachment).HasColumnName("ATTACHMENT").IsOptional().HasMaxLength(500);
            Property(x => x.BusinessPlanningDepartOpinion).HasColumnName("BUSINESSPLANNINGDEPARTOPINION").IsOptional();
            Property(x => x.FinanceAssistantOpinion).HasColumnName("FINANCEASSISTANTOPINION").IsOptional();
            Property(x => x.FinanceDepartDirectorOpinion).HasColumnName("FINANCEDEPARTDIRECTOROPINION").IsOptional();
            Property(x => x.PresidentOpinion).HasColumnName("PRESIDENTOPINION").IsOptional();
            Property(x => x.TellerOpinion).HasColumnName("TELLEROPINION").IsOptional();
            Property(x => x.ProjectManager).HasColumnName("PROJECTMANAGER").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectManagerName).HasColumnName("PROJECTMANAGERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.BondType).HasColumnName("BONDTYPE").IsOptional().HasMaxLength(50);
        }
    }

    // T_BusinessLeads
    internal partial class T_BusinessLeadsConfiguration : EntityTypeConfiguration<T_BusinessLeads>
    {
        public T_BusinessLeadsConfiguration()
        {
            ToTable("T_BUSINESSLEADS");
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
            Property(x => x.LeadsName).HasColumnName("LEADSNAME").IsOptional().HasMaxLength(200);
            Property(x => x.LeadsOwner).HasColumnName("LEADSOWNER").IsOptional().HasMaxLength(200);
            Property(x => x.LeadsResource).HasColumnName("LEADSRESOURCE").IsOptional().HasMaxLength(200);
            Property(x => x.LeadsType).HasColumnName("LEADSTYPE").IsOptional().HasMaxLength(200);
            Property(x => x.Industry).HasColumnName("INDUSTRY").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectClass).HasColumnName("PROJECTCLASS").IsOptional().HasMaxLength(200);
            Property(x => x.Phase).HasColumnName("PHASE").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectLevel).HasColumnName("PROJECTLEVEL").IsOptional().HasMaxLength(200);
            Property(x => x.Country).HasColumnName("COUNTRY").IsOptional().HasMaxLength(200);
            Property(x => x.Province).HasColumnName("PROVINCE").IsOptional().HasMaxLength(200);
            Property(x => x.City).HasColumnName("CITY").IsOptional().HasMaxLength(200);
        }
    }

    // T_BusinessLeads_Linkman
    internal partial class T_BusinessLeads_LinkmanConfiguration : EntityTypeConfiguration<T_BusinessLeads_Linkman>
    {
        public T_BusinessLeads_LinkmanConfiguration()
        {
            ToTable("T_BUSINESSLEADS_LINKMAN");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.T_BusinessLeadsID).HasColumnName("T_BUSINESSLEADSID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.LinkmanName).HasColumnName("LINKMANNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Position).HasColumnName("POSITION").IsOptional().HasMaxLength(200);
            Property(x => x.OfficeTelephone).HasColumnName("OFFICETELEPHONE").IsOptional().HasMaxLength(200);
            Property(x => x.ResponsiblePerson).HasColumnName("RESPONSIBLEPERSON").IsOptional().HasMaxLength(200);
            Property(x => x.MobilePhone).HasColumnName("MOBILEPHONE").IsOptional().HasMaxLength(200);
            Property(x => x.Email).HasColumnName("EMAIL").IsOptional().HasMaxLength(200);
            Property(x => x.Remark).HasColumnName("REMARK").IsOptional().HasMaxLength(200);

            // Foreign keys
            HasOptional(a => a.T_BusinessLeads).WithMany(b => b.T_BusinessLeads_Linkman).HasForeignKey(c => c.T_BusinessLeadsID); // FK_T_BusinessLeads_Linkman_T_BusinessLeads
        }
    }

    // T_BusinessLeads_Records
    internal partial class T_BusinessLeads_RecordsConfiguration : EntityTypeConfiguration<T_BusinessLeads_Records>
    {
        public T_BusinessLeads_RecordsConfiguration()
        {
            ToTable("T_BUSINESSLEADS_RECORDS");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.T_BusinessLeadsID).HasColumnName("T_BUSINESSLEADSID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.TrackDescription).HasColumnName("TRACKDESCRIPTION").IsOptional().HasMaxLength(200);
            Property(x => x.NextSetpPlan).HasColumnName("NEXTSETPPLAN").IsOptional().HasMaxLength(200);
            Property(x => x.LinkType).HasColumnName("LINKTYPE").IsOptional().HasMaxLength(200);
            Property(x => x.LinkerName).HasColumnName("LINKERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Register).HasColumnName("REGISTER").IsOptional().HasMaxLength(200);
            Property(x => x.RegisterDate).HasColumnName("REGISTERDATE").IsOptional();

            // Foreign keys
            HasOptional(a => a.T_BusinessLeads).WithMany(b => b.T_BusinessLeads_Records).HasForeignKey(c => c.T_BusinessLeadsID); // FK_T_BusinessLeads_Records_T_BusinessLeads
        }
    }

    // T_C_ContractChange
    internal partial class T_C_ContractChangeConfiguration : EntityTypeConfiguration<T_C_ContractChange>
    {
        public T_C_ContractChangeConfiguration()
        {
            ToTable("T_C_CONTRACTCHANGE");
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
            Property(x => x.PartyA).HasColumnName("PARTYA").IsOptional().HasMaxLength(50);
            Property(x => x.PartyAName).HasColumnName("PARTYANAME").IsOptional().HasMaxLength(50);
            Property(x => x.PartyB).HasColumnName("PARTYB").IsOptional().HasMaxLength(200);
            Property(x => x.PartyBName).HasColumnName("PARTYBNAME").IsOptional().HasMaxLength(200);
            Property(x => x.PartyC).HasColumnName("PARTYC").IsOptional().HasMaxLength(200);
            Property(x => x.SerialNumber).HasColumnName("SERIALNUMBER").IsOptional().HasMaxLength(200);
            Property(x => x.ContractType).HasColumnName("CONTRACTTYPE").IsOptional().HasMaxLength(200);
            Property(x => x.ContractClass).HasColumnName("CONTRACTCLASS").IsOptional().HasMaxLength(200);
            Property(x => x.SignDate).HasColumnName("SIGNDATE").IsOptional();
            Property(x => x.SignAddress).HasColumnName("SIGNADDRESS").IsOptional().HasMaxLength(200);
            Property(x => x.ContractState).HasColumnName("CONTRACTSTATE").IsOptional().HasMaxLength(200);
            Property(x => x.StartDate).HasColumnName("STARTDATE").IsOptional();
            Property(x => x.EndDate).HasColumnName("ENDDATE").IsOptional();
            Property(x => x.ContractSource).HasColumnName("CONTRACTSOURCE").IsOptional().HasMaxLength(200);
            Property(x => x.ContractValue).HasColumnName("CONTRACTVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.MainContract).HasColumnName("MAINCONTRACT").IsOptional().HasMaxLength(200);
            Property(x => x.MainContractName).HasColumnName("MAINCONTRACTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ContractCurrency).HasColumnName("CONTRACTCURRENCY").IsOptional().HasMaxLength(200);
            Property(x => x.ContractRMBAmount).HasColumnName("CONTRACTRMBAMOUNT").IsOptional().HasPrecision(18,4);
            Property(x => x.ExchangeRate).HasColumnName("EXCHANGERATE").IsOptional().HasPrecision(18,4);
            Property(x => x.ContractValueDX).HasColumnName("CONTRACTVALUEDX").IsOptional().HasMaxLength(200);
            Property(x => x.ContractRMBValueDX).HasColumnName("CONTRACTRMBVALUEDX").IsOptional().HasMaxLength(200);
            Property(x => x.PartyALegalPerson).HasColumnName("PARTYALEGALPERSON").IsOptional().HasMaxLength(200);
            Property(x => x.PartyBLegalPerson).HasColumnName("PARTYBLEGALPERSON").IsOptional().HasMaxLength(200);
            Property(x => x.PartyCLegalPerson).HasColumnName("PARTYCLEGALPERSON").IsOptional().HasMaxLength(200);
            Property(x => x.PartyAHusbandingAgentName).HasColumnName("PARTYAHUSBANDINGAGENTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.PartyBHusbandingAgentName).HasColumnName("PARTYBHUSBANDINGAGENTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.PartyCHusbandingAgentName).HasColumnName("PARTYCHUSBANDINGAGENTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.BusinessDept).HasColumnName("BUSINESSDEPT").IsOptional().HasMaxLength(200);
            Property(x => x.BusinessDeptName).HasColumnName("BUSINESSDEPTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.BusinessManager).HasColumnName("BUSINESSMANAGER").IsOptional().HasMaxLength(200);
            Property(x => x.BusinessManagerName).HasColumnName("BUSINESSMANAGERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ProductionDept).HasColumnName("PRODUCTIONDEPT").IsOptional().HasMaxLength(500);
            Property(x => x.ProductionDeptName).HasColumnName("PRODUCTIONDEPTNAME").IsOptional().HasMaxLength(500);
            Property(x => x.ProductionManager).HasColumnName("PRODUCTIONMANAGER").IsOptional().HasMaxLength(200);
            Property(x => x.ProductionManagerName).HasColumnName("PRODUCTIONMANAGERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.EngineeringInfo).HasColumnName("ENGINEERINGINFO").IsOptional().HasMaxLength(200);
            Property(x => x.EngineeringInfoName).HasColumnName("ENGINEERINGINFONAME").IsOptional().HasMaxLength(200);
            Property(x => x.ContractSplit).HasColumnName("CONTRACTSPLIT").IsOptional();
            Property(x => x.ContractAttachment).HasColumnName("CONTRACTATTACHMENT").IsOptional().HasMaxLength(500);
            Property(x => x.Attachment).HasColumnName("ATTACHMENT").IsOptional().HasMaxLength(500);
            Property(x => x.Remark).HasColumnName("REMARK").IsOptional().HasMaxLength(500);
            Property(x => x.SumReceiptValue).HasColumnName("SUMRECEIPTVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.SumInvoiceValue).HasColumnName("SUMINVOICEVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.SumBadDebtValue).HasColumnName("SUMBADDEBTVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.IsSigned).HasColumnName("ISSIGNED").IsOptional().HasMaxLength(200);
            Property(x => x.BelongYear).HasColumnName("BELONGYEAR").IsOptional();
            Property(x => x.BelongMonth).HasColumnName("BELONGMONTH").IsOptional();
            Property(x => x.BelongQuarter).HasColumnName("BELONGQUARTER").IsOptional();
            Property(x => x.ContractTextState).HasColumnName("CONTRACTTEXTSTATE").IsOptional().HasMaxLength(200);
            Property(x => x.ContractTextSendUser).HasColumnName("CONTRACTTEXTSENDUSER").IsOptional().HasMaxLength(200);
            Property(x => x.ContractTextSendUserName).HasColumnName("CONTRACTTEXTSENDUSERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.CustomerFullID).HasColumnName("CUSTOMERFULLID").IsOptional().HasMaxLength(500);
            Property(x => x.LastVersionData).HasColumnName("LASTVERSIONDATA").IsOptional().HasMaxLength(1073741823);
            Property(x => x.ApplyUser).HasColumnName("APPLYUSER").IsOptional().HasMaxLength(200);
            Property(x => x.ApplyUserName).HasColumnName("APPLYUSERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ApplyDate).HasColumnName("APPLYDATE").IsOptional();
            Property(x => x.MeetingSignUsers).HasColumnName("MEETINGSIGNUSERS").IsOptional().HasMaxLength(200);
            Property(x => x.MeetingSignUsersName).HasColumnName("MEETINGSIGNUSERSNAME").IsOptional().HasMaxLength(200);
            Property(x => x.MarketLeaderSign).HasColumnName("MARKETLEADERSIGN").IsOptional();
            Property(x => x.TechLeaderSign).HasColumnName("TECHLEADERSIGN").IsOptional();
            Property(x => x.CompanyLeaderSign).HasColumnName("COMPANYLEADERSIGN").IsOptional();
            Property(x => x.ContractID).HasColumnName("CONTRACTID").IsOptional().HasMaxLength(50);
            Property(x => x.ChangeDescription).HasColumnName("CHANGEDESCRIPTION").IsOptional();
            Property(x => x.CustomerFullIDName).HasColumnName("CUSTOMERFULLIDNAME").IsOptional().HasMaxLength(500);
            Property(x => x.ThisContractRMBAmount).HasColumnName("THISCONTRACTRMBAMOUNT").IsOptional().HasPrecision(18,4);
            Property(x => x.ThisContractRMBAmountDX).HasColumnName("THISCONTRACTRMBAMOUNTDX").IsOptional().HasMaxLength(200);
            Property(x => x.TaxRate).HasColumnName("TAXRATE").IsOptional().HasPrecision(18,4);
            Property(x => x.TaxName).HasColumnName("TAXNAME").IsOptional().HasMaxLength(200);
            Property(x => x.TaxValue).HasColumnName("TAXVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.ClearContractValue).HasColumnName("CLEARCONTRACTVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.PayerUnit).HasColumnName("PAYERUNIT").IsOptional().HasMaxLength(200);
            Property(x => x.PayerUnitName).HasColumnName("PAYERUNITNAME").IsOptional().HasMaxLength(200);
        }
    }

    // T_C_ContractChange_ContractSplit
    internal partial class T_C_ContractChange_ContractSplitConfiguration : EntityTypeConfiguration<T_C_ContractChange_ContractSplit>
    {
        public T_C_ContractChange_ContractSplitConfiguration()
        {
            ToTable("T_C_CONTRACTCHANGE_CONTRACTSPLIT");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.T_C_ContractChangeID).HasColumnName("T_C_CONTRACTCHANGEID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(200);
            Property(x => x.BusinessType).HasColumnName("BUSINESSTYPE").IsOptional().HasMaxLength(200);
            Property(x => x.Scale).HasColumnName("SCALE").IsOptional().HasPrecision(18,4);
            Property(x => x.SplitValue).HasColumnName("SPLITVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.SplitType).HasColumnName("SPLITTYPE").IsOptional().HasMaxLength(200);
            Property(x => x.OrlID).HasColumnName("ORLID").IsOptional().HasMaxLength(200);

            // Foreign keys
            HasOptional(a => a.T_C_ContractChange).WithMany(b => b.T_C_ContractChange_ContractSplit).HasForeignKey(c => c.T_C_ContractChangeID); // FK_T_C_ContractChange_ContractSplit_T_C_ContractChange
        }
    }

    // T_C_ContractChange_DeptRelation
    internal partial class T_C_ContractChange_DeptRelationConfiguration : EntityTypeConfiguration<T_C_ContractChange_DeptRelation>
    {
        public T_C_ContractChange_DeptRelationConfiguration()
        {
            ToTable("T_C_CONTRACTCHANGE_DEPTRELATION");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.T_C_ContractChangeID).HasColumnName("T_C_CONTRACTCHANGEID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.Dept).HasColumnName("DEPT").IsOptional().HasMaxLength(200);
            Property(x => x.DeptName).HasColumnName("DEPTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Scale).HasColumnName("SCALE").IsOptional().HasPrecision(18,4);
            Property(x => x.DeptValue).HasColumnName("DEPTVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.SumSupplementaryValue).HasColumnName("SUMSUPPLEMENTARYVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.SumDeptReceiptValue).HasColumnName("SUMDEPTRECEIPTVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.OrlID).HasColumnName("ORLID").IsOptional().HasMaxLength(200);

            // Foreign keys
            HasOptional(a => a.T_C_ContractChange).WithMany(b => b.T_C_ContractChange_DeptRelation).HasForeignKey(c => c.T_C_ContractChangeID); // FK_T_C_ContractChange_DeptRelation_T_C_ContractChange
        }
    }

    // T_C_ContractChange_PaymentDetail
    internal partial class T_C_ContractChange_PaymentDetailConfiguration : EntityTypeConfiguration<T_C_ContractChange_PaymentDetail>
    {
        public T_C_ContractChange_PaymentDetailConfiguration()
        {
            ToTable("T_C_CONTRACTCHANGE_PAYMENTDETAIL");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.T_C_ContractChangeID).HasColumnName("T_C_CONTRACTCHANGEID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.CustomerID).HasColumnName("CUSTOMERID").IsOptional().HasMaxLength(200);
            Property(x => x.CustomerIDName).HasColumnName("CUSTOMERIDNAME").IsOptional().HasMaxLength(200);
            Property(x => x.PaymentLinkMan).HasColumnName("PAYMENTLINKMAN").IsOptional().HasMaxLength(200);
            Property(x => x.PaymentLinkTel).HasColumnName("PAYMENTLINKTEL").IsOptional().HasMaxLength(200);
            Property(x => x.EvidenceAttachment).HasColumnName("EVIDENCEATTACHMENT").IsOptional().HasMaxLength(200);

            // Foreign keys
            HasOptional(a => a.T_C_ContractChange).WithMany(b => b.T_C_ContractChange_PaymentDetail).HasForeignKey(c => c.T_C_ContractChangeID); // FK_T_C_ContractChange_PaymentDetail_T_C_ContractChange
        }
    }

    // T_C_ContractChange_ProjectRelation
    internal partial class T_C_ContractChange_ProjectRelationConfiguration : EntityTypeConfiguration<T_C_ContractChange_ProjectRelation>
    {
        public T_C_ContractChange_ProjectRelationConfiguration()
        {
            ToTable("T_C_CONTRACTCHANGE_PROJECTRELATION");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.T_C_ContractChangeID).HasColumnName("T_C_CONTRACTCHANGEID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.ProjectCode).HasColumnName("PROJECTCODE").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectName).HasColumnName("PROJECTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectID).HasColumnName("PROJECTID").IsOptional().HasMaxLength(200);
            Property(x => x.Scale).HasColumnName("SCALE").IsOptional().HasPrecision(18,4);
            Property(x => x.ProjectValue).HasColumnName("PROJECTVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.OrlID).HasColumnName("ORLID").IsOptional().HasMaxLength(200);
            Property(x => x.TaxRate).HasColumnName("TAXRATE").IsOptional().HasPrecision(18,4);
            Property(x => x.TaxValue).HasColumnName("TAXVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.ClearValue).HasColumnName("CLEARVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.ChargerDept).HasColumnName("CHARGERDEPT").IsOptional().HasMaxLength(200);
            Property(x => x.ChargerDeptName).HasColumnName("CHARGERDEPTNAME").IsOptional().HasMaxLength(200);

            // Foreign keys
            HasOptional(a => a.T_C_ContractChange).WithMany(b => b.T_C_ContractChange_ProjectRelation).HasForeignKey(c => c.T_C_ContractChangeID); // FK_T_C_ContractChange_ProjectRelation_T_C_ContractChange
        }
    }

    // T_C_ContractChange_ReceiptObj
    internal partial class T_C_ContractChange_ReceiptObjConfiguration : EntityTypeConfiguration<T_C_ContractChange_ReceiptObj>
    {
        public T_C_ContractChange_ReceiptObjConfiguration()
        {
            ToTable("T_C_CONTRACTCHANGE_RECEIPTOBJ");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.T_C_ContractChangeID).HasColumnName("T_C_CONTRACTCHANGEID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(200);
            Property(x => x.ReceiptPercent).HasColumnName("RECEIPTPERCENT").IsOptional().HasPrecision(18,2);
            Property(x => x.ReceiptValue).HasColumnName("RECEIPTVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.FactInvoiceValue).HasColumnName("FACTINVOICEVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.FactReceiptValue).HasColumnName("FACTRECEIPTVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.OriginalPlanFinishDate).HasColumnName("ORIGINALPLANFINISHDATE").IsOptional();
            Property(x => x.PlanFinishDate).HasColumnName("PLANFINISHDATE").IsOptional();
            Property(x => x.ProjectInfo).HasColumnName("PROJECTINFO").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectInfoName).HasColumnName("PROJECTINFONAME").IsOptional().HasMaxLength(200);
            Property(x => x.MileStoneName).HasColumnName("MILESTONENAME").IsOptional().HasMaxLength(200);
            Property(x => x.MileStonePlanEndDate).HasColumnName("MILESTONEPLANENDDATE").IsOptional();
            Property(x => x.MileStoneFactEndDate).HasColumnName("MILESTONEFACTENDDATE").IsOptional();
            Property(x => x.MileStoneID).HasColumnName("MILESTONEID").IsOptional().HasMaxLength(200);
            Property(x => x.FactBadValue).HasColumnName("FACTBADVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.MileStoneState).HasColumnName("MILESTONESTATE").IsOptional().HasMaxLength(200);
            Property(x => x.OrlID).HasColumnName("ORLID").IsOptional().HasMaxLength(200);
            Property(x => x.SupplementaryID).HasColumnName("SUPPLEMENTARYID").IsOptional().HasMaxLength(200);

            // Foreign keys
            HasOptional(a => a.T_C_ContractChange).WithMany(b => b.T_C_ContractChange_ReceiptObj).HasForeignKey(c => c.T_C_ContractChangeID); // FK_T_C_ContractChange_ReceiptObj_T_C_ContractChange
        }
    }

    // T_C_ContractReview
    internal partial class T_C_ContractReviewConfiguration : EntityTypeConfiguration<T_C_ContractReview>
    {
        public T_C_ContractReviewConfiguration()
        {
            ToTable("T_C_CONTRACTREVIEW");
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
            Property(x => x.ContractCode).HasColumnName("CONTRACTCODE").IsOptional().HasMaxLength(200);
            Property(x => x.NewOrAdded).HasColumnName("NEWORADDED").IsOptional().HasMaxLength(200);
            Property(x => x.ContractName).HasColumnName("CONTRACTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.PartyAID).HasColumnName("PARTYAID").IsOptional().HasMaxLength(500);
            Property(x => x.PartyA).HasColumnName("PARTYA").IsOptional().HasMaxLength(500);
            Property(x => x.ProductionUnitID).HasColumnName("PRODUCTIONUNITID").IsOptional().HasMaxLength(200);
            Property(x => x.ProductionUnitName).HasColumnName("PRODUCTIONUNITNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ReviewType).HasColumnName("REVIEWTYPE").IsOptional().HasMaxLength(200);
            Property(x => x.ProduceMasterName).HasColumnName("PRODUCEMASTERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ProduceMasterID).HasColumnName("PRODUCEMASTERID").IsOptional().HasMaxLength(200);
            Property(x => x.CustomerRequire).HasColumnName("CUSTOMERREQUIRE").IsOptional().HasMaxLength(500);
            Property(x => x.LawRequire).HasColumnName("LAWREQUIRE").IsOptional().HasMaxLength(500);
            Property(x => x.AdditionalRequire).HasColumnName("ADDITIONALREQUIRE").IsOptional().HasMaxLength(500);
            Property(x => x.ContractAttachment).HasColumnName("CONTRACTATTACHMENT").IsOptional().HasMaxLength(500);
            Property(x => x.Attachment).HasColumnName("ATTACHMENT").IsOptional().HasMaxLength(500);
            Property(x => x.Remark).HasColumnName("REMARK").IsOptional().HasMaxLength(500);
            Property(x => x.ProduceMasterApproval).HasColumnName("PRODUCEMASTERAPPROVAL").IsOptional();
            Property(x => x.ReviewApproval).HasColumnName("REVIEWAPPROVAL").IsOptional();
            Property(x => x.OrgLeaderApproval).HasColumnName("ORGLEADERAPPROVAL").IsOptional();
            Property(x => x.CompLeaderApproval).HasColumnName("COMPLEADERAPPROVAL").IsOptional();
            Property(x => x.ContractID).HasColumnName("CONTRACTID").IsOptional().HasMaxLength(200);
            Property(x => x.CosignUser).HasColumnName("COSIGNUSER").IsOptional();
            Property(x => x.CosignUserName).HasColumnName("COSIGNUSERNAME").IsOptional();
            Property(x => x.RequriedClear).HasColumnName("REQURIEDCLEAR").IsOptional().HasMaxLength(200);
            Property(x => x.ConflictResloved).HasColumnName("CONFLICTRESLOVED").IsOptional().HasMaxLength(200);
            Property(x => x.CanDo).HasColumnName("CANDO").IsOptional().HasMaxLength(200);
            Property(x => x.PartyASub).HasColumnName("PARTYASUB").IsOptional().HasMaxLength(200);
            Property(x => x.PartyASubName).HasColumnName("PARTYASUBNAME").IsOptional().HasMaxLength(200);
        }
    }

    // T_C_CreditNoteApply
    internal partial class T_C_CreditNoteApplyConfiguration : EntityTypeConfiguration<T_C_CreditNoteApply>
    {
        public T_C_CreditNoteApplyConfiguration()
        {
            ToTable("T_C_CREDITNOTEAPPLY");
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
            Property(x => x.Contract).HasColumnName("CONTRACT").IsOptional().HasMaxLength(200);
            Property(x => x.ContractName).HasColumnName("CONTRACTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.PayerUnit).HasColumnName("PAYERUNIT").IsOptional().HasMaxLength(200);
            Property(x => x.PayerUnitSub).HasColumnName("PAYERUNITSUB").IsOptional().HasMaxLength(50);
            Property(x => x.PayerUnitSubName).HasColumnName("PAYERUNITSUBNAME").IsOptional().HasMaxLength(50);
            Property(x => x.Amount).HasColumnName("AMOUNT").IsOptional().HasPrecision(18,4);
            Property(x => x.Applyer).HasColumnName("APPLYER").IsOptional().HasMaxLength(50);
            Property(x => x.ApplyerName).HasColumnName("APPLYERNAME").IsOptional().HasMaxLength(50);
            Property(x => x.ApplyDate).HasColumnName("APPLYDATE").IsOptional();
            Property(x => x.Remark).HasColumnName("REMARK").IsOptional().HasMaxLength(500);
            Property(x => x.State).HasColumnName("STATE").IsOptional().HasMaxLength(200);
            Property(x => x.Capital).HasColumnName("CAPITAL").IsOptional().HasMaxLength(200);
            Property(x => x.TaxAccount).HasColumnName("TAXACCOUNT").IsOptional().HasMaxLength(200);
            Property(x => x.InvoiceName).HasColumnName("INVOICENAME").IsOptional().HasMaxLength(200);
            Property(x => x.Address).HasColumnName("ADDRESS").IsOptional().HasMaxLength(200);
            Property(x => x.TelPhone).HasColumnName("TELPHONE").IsOptional().HasMaxLength(200);
            Property(x => x.BankName).HasColumnName("BANKNAME").IsOptional().HasMaxLength(200);
            Property(x => x.BankAccount).HasColumnName("BANKACCOUNT").IsOptional().HasMaxLength(200);
            Property(x => x.InvoiceCode).HasColumnName("INVOICECODE").IsOptional().HasMaxLength(200);
            Property(x => x.TaxRate).HasColumnName("TAXRATE").IsOptional().HasPrecision(18,4);
            Property(x => x.Director).HasColumnName("DIRECTOR").IsOptional();
            Property(x => x.Finance).HasColumnName("FINANCE").IsOptional();
            Property(x => x.PayerUnitName).HasColumnName("PAYERUNITNAME").IsOptional().HasMaxLength(200);
            Property(x => x.TaxValue).HasColumnName("TAXVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.ClearValue).HasColumnName("CLEARVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.TaxName).HasColumnName("TAXNAME").IsOptional().HasMaxLength(200);
            Property(x => x.CustomerID).HasColumnName("CUSTOMERID").IsOptional().HasMaxLength(200);
            Property(x => x.CustomerIDName).HasColumnName("CUSTOMERIDNAME").IsOptional().HasMaxLength(200);
            Property(x => x.CustomerFullID).HasColumnName("CUSTOMERFULLID").IsOptional().HasMaxLength(200);
        }
    }

    // T_C_CreditNoteApply_Detail
    internal partial class T_C_CreditNoteApply_DetailConfiguration : EntityTypeConfiguration<T_C_CreditNoteApply_Detail>
    {
        public T_C_CreditNoteApply_DetailConfiguration()
        {
            ToTable("T_C_CREDITNOTEAPPLY_DETAIL");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.T_C_CreditNoteApplyID).HasColumnName("T_C_CREDITNOTEAPPLYID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.PlanReceiptName).HasColumnName("PLANRECEIPTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.SumInvoiceAmount).HasColumnName("SUMINVOICEAMOUNT").IsOptional().HasPrecision(18,4);
            Property(x => x.RemainInvoiceAmount).HasColumnName("REMAININVOICEAMOUNT").IsOptional().HasPrecision(18,4);
            Property(x => x.ApplyInvoiceAmount).HasColumnName("APPLYINVOICEAMOUNT").IsOptional().HasPrecision(18,4);
            Property(x => x.PlanReceiptID).HasColumnName("PLANRECEIPTID").IsOptional().HasMaxLength(200);
            Property(x => x.MinusRelationValue).HasColumnName("MINUSRELATIONVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.RemainValue).HasColumnName("REMAINVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.CreditValue).HasColumnName("CREDITVALUE").IsOptional().HasPrecision(18,4);

            // Foreign keys
            HasOptional(a => a.T_C_CreditNoteApply).WithMany(b => b.T_C_CreditNoteApply_Detail).HasForeignKey(c => c.T_C_CreditNoteApplyID); // FK_T_C_CreditNote_Detail_T_C_CreditNote
        }
    }

    // T_C_GuaranteeLetter
    internal partial class T_C_GuaranteeLetterConfiguration : EntityTypeConfiguration<T_C_GuaranteeLetter>
    {
        public T_C_GuaranteeLetterConfiguration()
        {
            ToTable("T_C_GUARANTEELETTER");
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
            Property(x => x.Contract).HasColumnName("CONTRACT").IsOptional().HasMaxLength(200);
            Property(x => x.ContractName).HasColumnName("CONTRACTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ContractCode).HasColumnName("CONTRACTCODE").IsOptional().HasMaxLength(200);
            Property(x => x.Project).HasColumnName("PROJECT").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectCode).HasColumnName("PROJECTCODE").IsOptional().HasMaxLength(200);
            Property(x => x.ContractAmount).HasColumnName("CONTRACTAMOUNT").IsOptional().HasMaxLength(200);
            Property(x => x.GuaranteeLetterType).HasColumnName("GUARANTEELETTERTYPE").IsOptional().HasMaxLength(200);
            Property(x => x.GuaranteeLetterAmount).HasColumnName("GUARANTEELETTERAMOUNT").IsOptional().HasPrecision(18,2);
            Property(x => x.GuaranteeLetterDeadline).HasColumnName("GUARANTEELETTERDEADLINE").IsOptional();
            Property(x => x.BeneficiaryName).HasColumnName("BENEFICIARYNAME").IsOptional().HasMaxLength(500);
            Property(x => x.Explain).HasColumnName("EXPLAIN").IsOptional().HasMaxLength(500);
            Property(x => x.ManagerSign).HasColumnName("MANAGERSIGN").IsOptional().HasMaxLength(200);
            Property(x => x.DirectorSign).HasColumnName("DIRECTORSIGN").IsOptional().HasMaxLength(200);
            Property(x => x.FinancialSign).HasColumnName("FINANCIALSIGN").IsOptional().HasMaxLength(200);
        }
    }

    // T_C_InvoiceApply
    internal partial class T_C_InvoiceApplyConfiguration : EntityTypeConfiguration<T_C_InvoiceApply>
    {
        public T_C_InvoiceApplyConfiguration()
        {
            ToTable("T_C_INVOICEAPPLY");
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
            Property(x => x.Contract).HasColumnName("CONTRACT").IsOptional().HasMaxLength(200);
            Property(x => x.ContractName).HasColumnName("CONTRACTNAME").IsOptional().HasMaxLength(2000);
            Property(x => x.PayerUnit).HasColumnName("PAYERUNIT").IsOptional().HasMaxLength(200);
            Property(x => x.PayerUnitName).HasColumnName("PAYERUNITNAME").IsOptional().HasMaxLength(2000);
            Property(x => x.PayerUnitSub).HasColumnName("PAYERUNITSUB").IsOptional().HasMaxLength(50);
            Property(x => x.PayerUnitSubName).HasColumnName("PAYERUNITSUBNAME").IsOptional().HasMaxLength(50);
            Property(x => x.Amount).HasColumnName("AMOUNT").IsOptional().HasPrecision(18,4);
            Property(x => x.Applyer).HasColumnName("APPLYER").IsOptional().HasMaxLength(50);
            Property(x => x.ApplyerName).HasColumnName("APPLYERNAME").IsOptional().HasMaxLength(50);
            Property(x => x.ApplyDate).HasColumnName("APPLYDATE").IsOptional();
            Property(x => x.Remark).HasColumnName("REMARK").IsOptional().HasMaxLength(500);
            Property(x => x.State).HasColumnName("STATE").IsOptional().HasMaxLength(200);
            Property(x => x.InvoiceType).HasColumnName("INVOICETYPE").IsOptional().HasMaxLength(50);
            Property(x => x.Capital).HasColumnName("CAPITAL").IsOptional().HasMaxLength(200);
            Property(x => x.InvoiceCode).HasColumnName("INVOICECODE").IsOptional().HasMaxLength(200);
            Property(x => x.TaxRate).HasColumnName("TAXRATE").IsOptional().HasPrecision(18,4);
            Property(x => x.Director).HasColumnName("DIRECTOR").IsOptional();
            Property(x => x.Finance).HasColumnName("FINANCE").IsOptional();
            Property(x => x.TaxAccount).HasColumnName("TAXACCOUNT").IsOptional().HasMaxLength(200);
            Property(x => x.Address).HasColumnName("ADDRESS").IsOptional().HasMaxLength(200);
            Property(x => x.TelPhone).HasColumnName("TELPHONE").IsOptional().HasMaxLength(200);
            Property(x => x.BankName).HasColumnName("BANKNAME").IsOptional().HasMaxLength(200);
            Property(x => x.BankAccount).HasColumnName("BANKACCOUNT").IsOptional().HasMaxLength(200);
            Property(x => x.InvoiceName).HasColumnName("INVOICENAME").IsOptional().HasMaxLength(200);
            Property(x => x.Detail).HasColumnName("DETAIL").IsOptional().HasMaxLength(1073741823);
            Property(x => x.TaxName).HasColumnName("TAXNAME").IsOptional().HasMaxLength(50);
            Property(x => x.TaxValue).HasColumnName("TAXVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.ClearValue).HasColumnName("CLEARVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.IncomeUnit).HasColumnName("INCOMEUNIT").IsOptional().HasMaxLength(50);
            Property(x => x.IncomeUnitName).HasColumnName("INCOMEUNITNAME").IsOptional().HasMaxLength(50);
            Property(x => x.IncomeUnitCode).HasColumnName("INCOMEUNITCODE").IsOptional().HasMaxLength(200);
            Property(x => x.CustomerFullID).HasColumnName("CUSTOMERFULLID").IsOptional().HasMaxLength(200);
            Property(x => x.CustomerID).HasColumnName("CUSTOMERID").IsOptional().HasMaxLength(200);
            Property(x => x.CustomerIDName).HasColumnName("CUSTOMERIDNAME").IsOptional().HasMaxLength(200);
        }
    }

    // T_C_InvoiceApply_Detail
    internal partial class T_C_InvoiceApply_DetailConfiguration : EntityTypeConfiguration<T_C_InvoiceApply_Detail>
    {
        public T_C_InvoiceApply_DetailConfiguration()
        {
            ToTable("T_C_INVOICEAPPLY_DETAIL");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.T_C_InvoiceApplyID).HasColumnName("T_C_INVOICEAPPLYID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.PlanReceiptName).HasColumnName("PLANRECEIPTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.RemainInvoiceAmount).HasColumnName("REMAININVOICEAMOUNT").IsOptional().HasPrecision(18,4);
            Property(x => x.ApplyInvoiceAmount).HasColumnName("APPLYINVOICEAMOUNT").IsOptional().HasPrecision(18,4);
            Property(x => x.PlanReceiptID).HasColumnName("PLANRECEIPTID").IsOptional().HasMaxLength(200);
            Property(x => x.SumInvoiceAmount).HasColumnName("SUMINVOICEAMOUNT").IsOptional().HasPrecision(18,4);

            // Foreign keys
            HasOptional(a => a.T_C_InvoiceApply).WithMany(b => b.T_C_InvoiceApply_Detail).HasForeignKey(c => c.T_C_InvoiceApplyID); // FK_T_C_InvoiceApply_Detail_T_C_InvoiceApply
        }
    }

    // T_C_InvoiceMakeUpApply
    internal partial class T_C_InvoiceMakeUpApplyConfiguration : EntityTypeConfiguration<T_C_InvoiceMakeUpApply>
    {
        public T_C_InvoiceMakeUpApplyConfiguration()
        {
            ToTable("T_C_INVOICEMAKEUPAPPLY");
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
            Property(x => x.Contract).HasColumnName("CONTRACT").IsOptional().HasMaxLength(200);
            Property(x => x.ContractName).HasColumnName("CONTRACTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.PayerUnit).HasColumnName("PAYERUNIT").IsOptional().HasMaxLength(50);
            Property(x => x.PayerUnitSub).HasColumnName("PAYERUNITSUB").IsOptional().HasMaxLength(50);
            Property(x => x.PayerUnitSubName).HasColumnName("PAYERUNITSUBNAME").IsOptional().HasMaxLength(50);
            Property(x => x.Amount).HasColumnName("AMOUNT").IsOptional().HasPrecision(18,4);
            Property(x => x.Applyer).HasColumnName("APPLYER").IsOptional().HasMaxLength(50);
            Property(x => x.ApplyerName).HasColumnName("APPLYERNAME").IsOptional().HasMaxLength(50);
            Property(x => x.ApplyDate).HasColumnName("APPLYDATE").IsOptional();
            Property(x => x.Remark).HasColumnName("REMARK").IsOptional().HasMaxLength(500);
            Property(x => x.State).HasColumnName("STATE").IsOptional().HasMaxLength(200);
            Property(x => x.InvoiceType).HasColumnName("INVOICETYPE").IsOptional().HasMaxLength(50);
            Property(x => x.Capital).HasColumnName("CAPITAL").IsOptional().HasMaxLength(200);
            Property(x => x.TaxAccount).HasColumnName("TAXACCOUNT").IsOptional().HasMaxLength(200);
            Property(x => x.InvoiceName).HasColumnName("INVOICENAME").IsOptional().HasMaxLength(200);
            Property(x => x.Address).HasColumnName("ADDRESS").IsOptional().HasMaxLength(200);
            Property(x => x.TelPhone).HasColumnName("TELPHONE").IsOptional().HasMaxLength(200);
            Property(x => x.BankName).HasColumnName("BANKNAME").IsOptional().HasMaxLength(200);
            Property(x => x.BankAccount).HasColumnName("BANKACCOUNT").IsOptional().HasMaxLength(200);
            Property(x => x.InvoiceCode).HasColumnName("INVOICECODE").IsOptional().HasMaxLength(200);
            Property(x => x.TaxRate).HasColumnName("TAXRATE").IsOptional().HasPrecision(18,4);
            Property(x => x.Director).HasColumnName("DIRECTOR").IsOptional();
            Property(x => x.Finance).HasColumnName("FINANCE").IsOptional();
            Property(x => x.PayerUnitName).HasColumnName("PAYERUNITNAME").IsOptional().HasMaxLength(200);
            Property(x => x.NeedAmount).HasColumnName("NEEDAMOUNT").IsOptional().HasPrecision(18,4);
            Property(x => x.RestNeedAmount).HasColumnName("RESTNEEDAMOUNT").IsOptional().HasPrecision(18,4);
            Property(x => x.ReceiptID).HasColumnName("RECEIPTID").IsOptional().HasMaxLength(50);
            Property(x => x.TaxName).HasColumnName("TAXNAME").IsOptional().HasMaxLength(50);
            Property(x => x.TaxValue).HasColumnName("TAXVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.ClearValue).HasColumnName("CLEARVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.Detail).HasColumnName("DETAIL").IsOptional().HasMaxLength(1073741823);
        }
    }

    // T_C_InvoiceMakeUpApply_Detail
    internal partial class T_C_InvoiceMakeUpApply_DetailConfiguration : EntityTypeConfiguration<T_C_InvoiceMakeUpApply_Detail>
    {
        public T_C_InvoiceMakeUpApply_DetailConfiguration()
        {
            ToTable("T_C_INVOICEMAKEUPAPPLY_DETAIL");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.T_C_InvoiceMakeUpApplyID).HasColumnName("T_C_INVOICEMAKEUPAPPLYID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.PlanReceiptName).HasColumnName("PLANRECEIPTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.SumInvoiceAmount).HasColumnName("SUMINVOICEAMOUNT").IsOptional().HasPrecision(18,4);
            Property(x => x.RemainInvoiceAmount).HasColumnName("REMAININVOICEAMOUNT").IsOptional().HasPrecision(18,4);
            Property(x => x.ApplyInvoiceAmount).HasColumnName("APPLYINVOICEAMOUNT").IsOptional().HasPrecision(18,4);
            Property(x => x.PlanReceiptID).HasColumnName("PLANRECEIPTID").IsOptional().HasMaxLength(50);

            // Foreign keys
            HasOptional(a => a.T_C_InvoiceMakeUpApply).WithMany(b => b.T_C_InvoiceMakeUpApply_Detail).HasForeignKey(c => c.T_C_InvoiceMakeUpApplyID); // FK_T_C_InvoiceMakeUpApply_Detail_T_C_InvoiceMakeUpApply
        }
    }

    // T_CP_CustomerRequestReview
    internal partial class T_CP_CustomerRequestReviewConfiguration : EntityTypeConfiguration<T_CP_CustomerRequestReview>
    {
        public T_CP_CustomerRequestReviewConfiguration()
        {
            ToTable("T_CP_CUSTOMERREQUESTREVIEW");
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
            Property(x => x.CustomerInfo).HasColumnName("CUSTOMERINFO").IsOptional().HasMaxLength(200);
            Property(x => x.CustomerInfoName).HasColumnName("CUSTOMERINFONAME").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectLevel).HasColumnName("PROJECTLEVEL").IsOptional().HasMaxLength(200);
            Property(x => x.Phase).HasColumnName("PHASE").IsOptional().HasMaxLength(200);
            Property(x => x.DeptInfo).HasColumnName("DEPTINFO").IsOptional().HasMaxLength(200);
            Property(x => x.DeptInfoName).HasColumnName("DEPTINFONAME").IsOptional().HasMaxLength(200);
            Property(x => x.ReviewType).HasColumnName("REVIEWTYPE").IsOptional().HasMaxLength(200);
            Property(x => x.CustomerRequire).HasColumnName("CUSTOMERREQUIRE").IsOptional().HasMaxLength(500);
            Property(x => x.LawRequire).HasColumnName("LAWREQUIRE").IsOptional().HasMaxLength(500);
            Property(x => x.AddtionalRequire).HasColumnName("ADDTIONALREQUIRE").IsOptional().HasMaxLength(500);
            Property(x => x.Risk).HasColumnName("RISK").IsOptional().HasMaxLength(500);
            Property(x => x.Attach).HasColumnName("ATTACH").IsOptional().HasMaxLength(500);
            Property(x => x.Remark).HasColumnName("REMARK").IsOptional().HasMaxLength(500);
            Property(x => x.RequireClear).HasColumnName("REQUIRECLEAR").IsOptional().HasMaxLength(200);
            Property(x => x.ConfictReslove).HasColumnName("CONFICTRESLOVE").IsOptional().HasMaxLength(200);
            Property(x => x.CanRequried).HasColumnName("CANREQURIED").IsOptional().HasMaxLength(200);
            Property(x => x.CanDo).HasColumnName("CANDO").IsOptional().HasMaxLength(200);
            Property(x => x.MainDesignOrgApproval).HasColumnName("MAINDESIGNORGAPPROVAL").IsOptional();
            Property(x => x.ChiefEngineerApproval).HasColumnName("CHIEFENGINEERAPPROVAL").IsOptional();
            Property(x => x.MajorOrgApproval).HasColumnName("MAJORORGAPPROVAL").IsOptional();
            Property(x => x.ProduceOrgApproval).HasColumnName("PRODUCEORGAPPROVAL").IsOptional();
            Property(x => x.CompLeaderApproval).HasColumnName("COMPLEADERAPPROVAL").IsOptional();
            Property(x => x.DemandID).HasColumnName("DEMANDID").IsOptional().HasMaxLength(50);
        }
    }

    // T_F_GuaranteeLetterApply
    internal partial class T_F_GuaranteeLetterApplyConfiguration : EntityTypeConfiguration<T_F_GuaranteeLetterApply>
    {
        public T_F_GuaranteeLetterApplyConfiguration()
        {
            ToTable("T_F_GUARANTEELETTERAPPLY");
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
            Property(x => x.Contract).HasColumnName("CONTRACT").IsOptional().HasMaxLength(200);
            Property(x => x.ContractName).HasColumnName("CONTRACTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ContractCode).HasColumnName("CONTRACTCODE").IsOptional().HasMaxLength(200);
            Property(x => x.Clue).HasColumnName("CLUE").IsOptional().HasMaxLength(200);
            Property(x => x.ClueCode).HasColumnName("CLUECODE").IsOptional().HasMaxLength(200);
            Property(x => x.ContractAmount).HasColumnName("CONTRACTAMOUNT").IsOptional().HasMaxLength(200);
            Property(x => x.GuaranteeLetterType).HasColumnName("GUARANTEELETTERTYPE").IsOptional().HasMaxLength(200);
            Property(x => x.GuaranteeLetterAmount).HasColumnName("GUARANTEELETTERAMOUNT").IsOptional().HasPrecision(18,2);
            Property(x => x.GuaranteeLetterDeadline).HasColumnName("GUARANTEELETTERDEADLINE").IsOptional();
            Property(x => x.BeneficiaryName).HasColumnName("BENEFICIARYNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Explain).HasColumnName("EXPLAIN").IsOptional().HasMaxLength(500);
            Property(x => x.ManagerSign).HasColumnName("MANAGERSIGN").IsOptional();
            Property(x => x.DirectorSign).HasColumnName("DIRECTORSIGN").IsOptional();
            Property(x => x.FinancialSign).HasColumnName("FINANCIALSIGN").IsOptional();
            Property(x => x.Terminator).HasColumnName("TERMINATOR").IsOptional().HasMaxLength(50);
            Property(x => x.TerminatorName).HasColumnName("TERMINATORNAME").IsOptional().HasMaxLength(200);
            Property(x => x.TerminateDate).HasColumnName("TERMINATEDATE").IsOptional();
            Property(x => x.Status).HasColumnName("STATUS").IsOptional().HasMaxLength(50);
        }
    }

    // T_F_GuaranteeLetterTerminate
    internal partial class T_F_GuaranteeLetterTerminateConfiguration : EntityTypeConfiguration<T_F_GuaranteeLetterTerminate>
    {
        public T_F_GuaranteeLetterTerminateConfiguration()
        {
            ToTable("T_F_GUARANTEELETTERTERMINATE");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.ModifyDate).HasColumnName("MODIFYDATE").IsOptional();
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUser).HasColumnName("MODIFYUSER").IsOptional().HasMaxLength(50);
            Property(x => x.OrgID).HasColumnName("ORGID").IsOptional().HasMaxLength(50);
            Property(x => x.GuaranteeLetterID).HasColumnName("GUARANTEELETTERID").IsOptional().HasMaxLength(50);
            Property(x => x.Contract).HasColumnName("CONTRACT").IsOptional().HasMaxLength(200);
            Property(x => x.ContractName).HasColumnName("CONTRACTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ContractCode).HasColumnName("CONTRACTCODE").IsOptional().HasMaxLength(200);
            Property(x => x.GuaranteeLetterAmount).HasColumnName("GUARANTEELETTERAMOUNT").IsOptional().HasPrecision(18,4);
            Property(x => x.GuaranteeLetterDeadline).HasColumnName("GUARANTEELETTERDEADLINE").IsOptional();
            Property(x => x.Terminator).HasColumnName("TERMINATOR").IsOptional().HasMaxLength(200);
            Property(x => x.TerminatorName).HasColumnName("TERMINATORNAME").IsOptional().HasMaxLength(200);
            Property(x => x.TerminateDate).HasColumnName("TERMINATEDATE").IsOptional();
        }
    }

    // T_SP_ContractChange
    internal partial class T_SP_ContractChangeConfiguration : EntityTypeConfiguration<T_SP_ContractChange>
    {
        public T_SP_ContractChangeConfiguration()
        {
            ToTable("T_SP_CONTRACTCHANGE");
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
            Property(x => x.Supplier).HasColumnName("SUPPLIER").IsOptional().HasMaxLength(200);
            Property(x => x.SupplierName).HasColumnName("SUPPLIERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ManagerContract).HasColumnName("MANAGERCONTRACT").IsOptional().HasMaxLength(50);
            Property(x => x.ManagerContractName).HasColumnName("MANAGERCONTRACTNAME").IsOptional().HasMaxLength(50);
            Property(x => x.ManagerContractCode).HasColumnName("MANAGERCONTRACTCODE").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectInfo).HasColumnName("PROJECTINFO").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectInfoName).HasColumnName("PROJECTINFONAME").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectInfoCode).HasColumnName("PROJECTINFOCODE").IsOptional().HasMaxLength(200);
            Property(x => x.ContractClass).HasColumnName("CONTRACTCLASS").IsOptional().HasMaxLength(200);
            Property(x => x.MainContract).HasColumnName("MAINCONTRACT").IsOptional().HasMaxLength(200);
            Property(x => x.MainContractName).HasColumnName("MAINCONTRACTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.MainContractCode).HasColumnName("MAINCONTRACTCODE").IsOptional().HasMaxLength(200);
            Property(x => x.SupplierDept).HasColumnName("SUPPLIERDEPT").IsOptional().HasMaxLength(50);
            Property(x => x.SupplierDeptName).HasColumnName("SUPPLIERDEPTNAME").IsOptional().HasMaxLength(50);
            Property(x => x.ContractAmount).HasColumnName("CONTRACTAMOUNT").IsOptional().HasPrecision(18,4);
            Property(x => x.State).HasColumnName("STATE").IsOptional().HasMaxLength(50);
            Property(x => x.SignDate).HasColumnName("SIGNDATE").IsOptional();
            Property(x => x.ThisContractValue).HasColumnName("THISCONTRACTVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.TaxName).HasColumnName("TAXNAME").IsOptional().HasMaxLength(50);
            Property(x => x.TaxRate).HasColumnName("TAXRATE").IsOptional().HasPrecision(18,4);
            Property(x => x.ExchangeRate).HasColumnName("EXCHANGERATE").IsOptional().HasPrecision(18,4);
            Property(x => x.Currency).HasColumnName("CURRENCY").IsOptional().HasMaxLength(200);
            Property(x => x.ContractValue).HasColumnName("CONTRACTVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.ContractAttachment).HasColumnName("CONTRACTATTACHMENT").IsOptional().HasMaxLength(500);
            Property(x => x.Attachments).HasColumnName("ATTACHMENTS").IsOptional().HasMaxLength(500);
            Property(x => x.Remark).HasColumnName("REMARK").IsOptional().HasMaxLength(500);
            Property(x => x.ContractSplit).HasColumnName("CONTRACTSPLIT").IsOptional().HasMaxLength(1073741823);
            Property(x => x.ContractValueDX).HasColumnName("CONTRACTVALUEDX").IsOptional().HasMaxLength(200);
            Property(x => x.ApplyUser).HasColumnName("APPLYUSER").IsOptional().HasMaxLength(200);
            Property(x => x.ApplyUserName).HasColumnName("APPLYUSERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ApplyDate).HasColumnName("APPLYDATE").IsOptional();
            Property(x => x.Reason).HasColumnName("REASON").IsOptional().HasMaxLength(500);
            Property(x => x.LastVersionData).HasColumnName("LASTVERSIONDATA").IsOptional().HasMaxLength(1073741823);
            Property(x => x.ContractID).HasColumnName("CONTRACTID").IsOptional().HasMaxLength(200);
            Property(x => x.ContractCode).HasColumnName("CONTRACTCODE").IsOptional().HasMaxLength(200);
            Property(x => x.TaxValue).HasColumnName("TAXVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.ClearContractValue).HasColumnName("CLEARCONTRACTVALUE").IsOptional().HasPrecision(18,4);
        }
    }

    // T_SP_ContractChange_ContractSplit
    internal partial class T_SP_ContractChange_ContractSplitConfiguration : EntityTypeConfiguration<T_SP_ContractChange_ContractSplit>
    {
        public T_SP_ContractChange_ContractSplitConfiguration()
        {
            ToTable("T_SP_CONTRACTCHANGE_CONTRACTSPLIT");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.T_SP_ContractChangeID).HasColumnName("T_SP_CONTRACTCHANGEID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(200);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(200);
            Property(x => x.ChargerUserName).HasColumnName("CHARGERUSERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ChargerDeptName).HasColumnName("CHARGERDEPTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.SplitValue).HasColumnName("SPLITVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.MinContractValue).HasColumnName("MINCONTRACTVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.CostUnitID).HasColumnName("COSTUNITID").IsOptional().HasMaxLength(50);
            Property(x => x.OrlID).HasColumnName("ORLID").IsOptional().HasMaxLength(50);
            Property(x => x.ChargerUser).HasColumnName("CHARGERUSER").IsOptional().HasMaxLength(200);
            Property(x => x.ChargerDept).HasColumnName("CHARGERDEPT").IsOptional().HasMaxLength(200);

            // Foreign keys
            HasOptional(a => a.T_SP_ContractChange).WithMany(b => b.T_SP_ContractChange_ContractSplit).HasForeignKey(c => c.T_SP_ContractChangeID); // FK_T_SP_ContractChange_ContractSplit_T_SP_ContractChange
        }
    }

    // T_SP_ContractReview
    internal partial class T_SP_ContractReviewConfiguration : EntityTypeConfiguration<T_SP_ContractReview>
    {
        public T_SP_ContractReviewConfiguration()
        {
            ToTable("T_SP_CONTRACTREVIEW");
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
            Property(x => x.SPContractInfo).HasColumnName("SPCONTRACTINFO").IsOptional().HasMaxLength(200);
            Property(x => x.SPContractInfoName).HasColumnName("SPCONTRACTINFONAME").IsOptional().HasMaxLength(200);
            Property(x => x.SPContractCode).HasColumnName("SPCONTRACTCODE").IsOptional().HasMaxLength(200);
            Property(x => x.ContractInfo).HasColumnName("CONTRACTINFO").IsOptional().HasMaxLength(200);
            Property(x => x.ContractInfoName).HasColumnName("CONTRACTINFONAME").IsOptional().HasMaxLength(200);
            Property(x => x.ContractCode).HasColumnName("CONTRACTCODE").IsOptional().HasMaxLength(200);
            Property(x => x.Supplier).HasColumnName("SUPPLIER").IsOptional().HasMaxLength(200);
            Property(x => x.SupplierName).HasColumnName("SUPPLIERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectInfo).HasColumnName("PROJECTINFO").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectInfoName).HasColumnName("PROJECTINFONAME").IsOptional().HasMaxLength(200);
            Property(x => x.ChargerDept).HasColumnName("CHARGERDEPT").IsOptional().HasMaxLength(200);
            Property(x => x.ChargerDeptName).HasColumnName("CHARGERDEPTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.InvoiceCondition).HasColumnName("INVOICECONDITION").IsOptional().HasMaxLength(200);
            Property(x => x.ApplyUser).HasColumnName("APPLYUSER").IsOptional().HasMaxLength(200);
            Property(x => x.ApplyUserName).HasColumnName("APPLYUSERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ApplyDate).HasColumnName("APPLYDATE").IsOptional();
            Property(x => x.SPContractValue).HasColumnName("SPCONTRACTVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.SPContractValueDX).HasColumnName("SPCONTRACTVALUEDX").IsOptional().HasMaxLength(200);
            Property(x => x.TaxName).HasColumnName("TAXNAME").IsOptional().HasMaxLength(200);
            Property(x => x.TaxRate).HasColumnName("TAXRATE").IsOptional().HasPrecision(18,4);
            Property(x => x.Remark).HasColumnName("REMARK").IsOptional().HasMaxLength(500);
            Property(x => x.ContractText).HasColumnName("CONTRACTTEXT").IsOptional().HasMaxLength(500);
            Property(x => x.Attachments).HasColumnName("ATTACHMENTS").IsOptional().HasMaxLength(500);
        }
    }

    // T_SP_DesignApproval
    internal partial class T_SP_DesignApprovalConfiguration : EntityTypeConfiguration<T_SP_DesignApproval>
    {
        public T_SP_DesignApprovalConfiguration()
        {
            ToTable("T_SP_DESIGNAPPROVAL");
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
            Property(x => x.Project).HasColumnName("PROJECT").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectName).HasColumnName("PROJECTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Contract).HasColumnName("CONTRACT").IsOptional().HasMaxLength(200);
            Property(x => x.ContractName).HasColumnName("CONTRACTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Content).HasColumnName("CONTENT").IsOptional().HasMaxLength(500);
            Property(x => x.Suppier).HasColumnName("SUPPIER").IsOptional().HasMaxLength(200);
            Property(x => x.SuppierName).HasColumnName("SUPPIERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.DeptLeaderSign).HasColumnName("DEPTLEADERSIGN").IsOptional();
            Property(x => x.MarketPlanSign).HasColumnName("MARKETPLANSIGN").IsOptional();
            Property(x => x.ProjectCode).HasColumnName("PROJECTCODE").IsOptional().HasMaxLength(200);
            Property(x => x.Reason).HasColumnName("REASON").IsOptional().HasMaxLength(500);
        }
    }

    // T_SP_DesignApproval_CoopProjectInfo
    internal partial class T_SP_DesignApproval_CoopProjectInfoConfiguration : EntityTypeConfiguration<T_SP_DesignApproval_CoopProjectInfo>
    {
        public T_SP_DesignApproval_CoopProjectInfoConfiguration()
        {
            ToTable("T_SP_DESIGNAPPROVAL_COOPPROJECTINFO");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.T_SP_DesignApprovalID).HasColumnName("T_SP_DESIGNAPPROVALID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.Project).HasColumnName("PROJECT").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectName).HasColumnName("PROJECTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ProductName).HasColumnName("PRODUCTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Evaluate).HasColumnName("EVALUATE").IsOptional().HasPrecision(18,2);

            // Foreign keys
            HasOptional(a => a.T_SP_DesignApproval).WithMany(b => b.T_SP_DesignApproval_CoopProjectInfo).HasForeignKey(c => c.T_SP_DesignApprovalID); // FK_T_SP_DesignApproval_CoopProjectInfo_T_SP_DesignApproval
        }
    }

    // T_SP_DesignApproval_CredentialInfo
    internal partial class T_SP_DesignApproval_CredentialInfoConfiguration : EntityTypeConfiguration<T_SP_DesignApproval_CredentialInfo>
    {
        public T_SP_DesignApproval_CredentialInfoConfiguration()
        {
            ToTable("T_SP_DESIGNAPPROVAL_CREDENTIALINFO");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.T_SP_DesignApprovalID).HasColumnName("T_SP_DESIGNAPPROVALID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.CertificateName).HasColumnName("CERTIFICATENAME").IsOptional().HasMaxLength(200);
            Property(x => x.CertificateCode).HasColumnName("CERTIFICATECODE").IsOptional().HasMaxLength(200);
            Property(x => x.Achievement).HasColumnName("ACHIEVEMENT").IsOptional().HasMaxLength(200);

            // Foreign keys
            HasOptional(a => a.T_SP_DesignApproval).WithMany(b => b.T_SP_DesignApproval_CredentialInfo).HasForeignKey(c => c.T_SP_DesignApprovalID); // FK_T_SP_DesignApproval_CredentialInfo_T_SP_DesignApproval
        }
    }

    // T_SP_DesignReview
    internal partial class T_SP_DesignReviewConfiguration : EntityTypeConfiguration<T_SP_DesignReview>
    {
        public T_SP_DesignReviewConfiguration()
        {
            ToTable("T_SP_DESIGNREVIEW");
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
            Property(x => x.Project).HasColumnName("PROJECT").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectName).HasColumnName("PROJECTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.DesignDept).HasColumnName("DESIGNDEPT").IsOptional().HasMaxLength(200);
            Property(x => x.DesignDeptName).HasColumnName("DESIGNDEPTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ContractCode).HasColumnName("CONTRACTCODE").IsOptional().HasMaxLength(200);
            Property(x => x.ContractCodeName).HasColumnName("CONTRACTCODENAME").IsOptional().HasMaxLength(200);
            Property(x => x.SuppierContractCode).HasColumnName("SUPPIERCONTRACTCODE").IsOptional().HasMaxLength(200);
            Property(x => x.SuppierContractCodeName).HasColumnName("SUPPIERCONTRACTCODENAME").IsOptional().HasMaxLength(200);
            Property(x => x.ChargerUser).HasColumnName("CHARGERUSER").IsOptional().HasMaxLength(200);
            Property(x => x.ChargerUserName).HasColumnName("CHARGERUSERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Suppier).HasColumnName("SUPPIER").IsOptional().HasMaxLength(200);
            Property(x => x.SuppierName).HasColumnName("SUPPIERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Majors).HasColumnName("MAJORS").IsOptional().HasMaxLength(200);
            Property(x => x.ProductName).HasColumnName("PRODUCTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Receiver).HasColumnName("RECEIVER").IsOptional().HasMaxLength(200);
            Property(x => x.ReceiverName).HasColumnName("RECEIVERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ReceiveDate).HasColumnName("RECEIVEDATE").IsOptional();
            Property(x => x.ValidationSelection).HasColumnName("VALIDATIONSELECTION").IsOptional().HasMaxLength(200);
            Property(x => x.Evaluate).HasColumnName("EVALUATE").IsOptional().HasPrecision(18,2);
            Property(x => x.Suggestions).HasColumnName("SUGGESTIONS").IsOptional().HasMaxLength(500);
            Property(x => x.MajorChargersSign).HasColumnName("MAJORCHARGERSSIGN").IsOptional();
            Property(x => x.ProjectChargerSign).HasColumnName("PROJECTCHARGERSIGN").IsOptional();
            Property(x => x.DesignChargerSign).HasColumnName("DESIGNCHARGERSIGN").IsOptional();
        }
    }

    // T_SP_PaymentApply
    internal partial class T_SP_PaymentApplyConfiguration : EntityTypeConfiguration<T_SP_PaymentApply>
    {
        public T_SP_PaymentApplyConfiguration()
        {
            ToTable("T_SP_PAYMENTAPPLY");
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
            Property(x => x.SerialNumber).HasColumnName("SERIALNUMBER").IsOptional().HasMaxLength(50);
            Property(x => x.Supplier).HasColumnName("SUPPLIER").IsOptional().HasMaxLength(50);
            Property(x => x.SupplierName).HasColumnName("SUPPLIERNAME").IsOptional().HasMaxLength(50);
            Property(x => x.Contract).HasColumnName("CONTRACT").IsOptional().HasMaxLength(50);
            Property(x => x.ContractName).HasColumnName("CONTRACTNAME").IsOptional().HasMaxLength(50);
            Property(x => x.CostUnit).HasColumnName("COSTUNIT").IsOptional().HasMaxLength(50);
            Property(x => x.CostUnitName).HasColumnName("COSTUNITNAME").IsOptional().HasMaxLength(200);
            Property(x => x.PaymentUser).HasColumnName("PAYMENTUSER").IsOptional().HasMaxLength(50);
            Property(x => x.PaymentUserName).HasColumnName("PAYMENTUSERNAME").IsOptional().HasMaxLength(50);
            Property(x => x.PaymentDate).HasColumnName("PAYMENTDATE").IsOptional();
            Property(x => x.PaymentValue).HasColumnName("PAYMENTVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.Remark).HasColumnName("REMARK").IsOptional().HasMaxLength(500);
            Property(x => x.DeptSign).HasColumnName("DEPTSIGN").IsOptional();
            Property(x => x.CompanySign).HasColumnName("COMPANYSIGN").IsOptional();
            Property(x => x.FinanceSign).HasColumnName("FINANCESIGN").IsOptional();
            Property(x => x.SumPaymentValue).HasColumnName("SUMPAYMENTVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.ContractValue).HasColumnName("CONTRACTVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.ApplyValue).HasColumnName("APPLYVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.ManagerContract).HasColumnName("MANAGERCONTRACT").IsOptional().HasMaxLength(200);
            Property(x => x.ManagerContractName).HasColumnName("MANAGERCONTRACTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.InvoiceRelation).HasColumnName("INVOICERELATION").IsOptional();
            Property(x => x.AcceptanceBill).HasColumnName("ACCEPTANCEBILL").IsOptional();
            Property(x => x.BillValue).HasColumnName("BILLVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.PaymentWithoutBill).HasColumnName("PAYMENTWITHOUTBILL").IsOptional().HasPrecision(18,4);
            Property(x => x.ExpenseType).HasColumnName("EXPENSETYPE").IsOptional().HasMaxLength(50);
            Property(x => x.CostInfo).HasColumnName("COSTINFO").IsOptional();
            Property(x => x.BankAccount).HasColumnName("BANKACCOUNT").IsOptional().HasMaxLength(200);
            Property(x => x.BankAccountName).HasColumnName("BANKACCOUNTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.BankName).HasColumnName("BANKNAME").IsOptional().HasMaxLength(200);
        }
    }

    // T_SP_PaymentApply_AcceptanceBill
    internal partial class T_SP_PaymentApply_AcceptanceBillConfiguration : EntityTypeConfiguration<T_SP_PaymentApply_AcceptanceBill>
    {
        public T_SP_PaymentApply_AcceptanceBillConfiguration()
        {
            ToTable("T_SP_PAYMENTAPPLY_ACCEPTANCEBILL");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.T_SP_PaymentApplyID).HasColumnName("T_SP_PAYMENTAPPLYID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.SerialNumber).HasColumnName("SERIALNUMBER").IsOptional().HasMaxLength(200);
            Property(x => x.CustomerName).HasColumnName("CUSTOMERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.BillDate).HasColumnName("BILLDATE").IsOptional();
            Property(x => x.Amount).HasColumnName("AMOUNT").IsOptional().HasPrecision(18,4);
            Property(x => x.AcceptanceBillID).HasColumnName("ACCEPTANCEBILLID").IsOptional().HasMaxLength(200);

            // Foreign keys
            HasOptional(a => a.T_SP_PaymentApply).WithMany(b => b.T_SP_PaymentApply_AcceptanceBill).HasForeignKey(c => c.T_SP_PaymentApplyID); // FK_T_SP_PaymentApply_AcceptanceBill_T_SP_PaymentApply
        }
    }

    // T_SP_PaymentApply_CostInfo
    internal partial class T_SP_PaymentApply_CostInfoConfiguration : EntityTypeConfiguration<T_SP_PaymentApply_CostInfo>
    {
        public T_SP_PaymentApply_CostInfoConfiguration()
        {
            ToTable("T_SP_PAYMENTAPPLY_COSTINFO");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.T_SP_PaymentApplyID).HasColumnName("T_SP_PAYMENTAPPLYID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(200);
            Property(x => x.Code).HasColumnName("CODE").IsOptional().HasMaxLength(200);
            Property(x => x.ContractValue).HasColumnName("CONTRACTVALUE").IsOptional().HasMaxLength(200);
            Property(x => x.CostValue).HasColumnName("COSTVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.ContractCostUnitID).HasColumnName("CONTRACTCOSTUNITID").IsOptional().HasMaxLength(50);
            Property(x => x.CostUnitID).HasColumnName("COSTUNITID").IsOptional().HasMaxLength(200);
            Property(x => x.SubjectCode).HasColumnName("SUBJECTCODE").IsOptional().HasMaxLength(200);
            Property(x => x.CostValueDistribute).HasColumnName("COSTVALUEDISTRIBUTE").IsOptional().HasPrecision(18,4);

            // Foreign keys
            HasOptional(a => a.T_SP_PaymentApply).WithMany(b => b.T_SP_PaymentApply_CostInfo).HasForeignKey(c => c.T_SP_PaymentApplyID); // FK_T_SP_PaymentApply_CostInfo_T_SP_PaymentApply
        }
    }

    // T_SP_PaymentApply_InvoiceRelation
    internal partial class T_SP_PaymentApply_InvoiceRelationConfiguration : EntityTypeConfiguration<T_SP_PaymentApply_InvoiceRelation>
    {
        public T_SP_PaymentApply_InvoiceRelationConfiguration()
        {
            ToTable("T_SP_PAYMENTAPPLY_INVOICERELATION");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.T_SP_PaymentApplyID).HasColumnName("T_SP_PAYMENTAPPLYID").IsOptional().HasMaxLength(50);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
            Property(x => x.IsReleased).HasColumnName("ISRELEASED").IsOptional().HasMaxLength(1);
            Property(x => x.InvoiceNo).HasColumnName("INVOICENO").IsOptional().HasMaxLength(200);
            Property(x => x.InvoiceUnit).HasColumnName("INVOICEUNIT").IsOptional().HasMaxLength(200);
            Property(x => x.InvoiceRelationValue).HasColumnName("INVOICERELATIONVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.RemainInvoiceValue).HasColumnName("REMAININVOICEVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.RelationValue).HasColumnName("RELATIONVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.InvoiceID).HasColumnName("INVOICEID").IsOptional().HasMaxLength(200);

            // Foreign keys
            HasOptional(a => a.T_SP_PaymentApply).WithMany(b => b.T_SP_PaymentApply_InvoiceRelation).HasForeignKey(c => c.T_SP_PaymentApplyID); // FK_T_SP_PaymentApply_InvoiceRelation_T_SP_PaymentApply
        }
    }

    // T_SP_PaymentApplyConfirm
    internal partial class T_SP_PaymentApplyConfirmConfiguration : EntityTypeConfiguration<T_SP_PaymentApplyConfirm>
    {
        public T_SP_PaymentApplyConfirmConfiguration()
        {
            ToTable("T_SP_PAYMENTAPPLYCONFIRM");
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
            Property(x => x.SubContractName).HasColumnName("SUBCONTRACTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.SubContractCode).HasColumnName("SUBCONTRACTCODE").IsOptional().HasMaxLength(200);
            Property(x => x.CBSInfoName).HasColumnName("CBSINFONAME").IsOptional().HasMaxLength(500);
            Property(x => x.CBSInfoCode).HasColumnName("CBSINFOCODE").IsOptional().HasMaxLength(200);
            Property(x => x.CostUnitName).HasColumnName("COSTUNITNAME").IsOptional().HasMaxLength(500);
            Property(x => x.CostUnitCode).HasColumnName("COSTUNITCODE").IsOptional().HasMaxLength(200);
            Property(x => x.ChargerUser).HasColumnName("CHARGERUSER").IsOptional().HasMaxLength(200);
            Property(x => x.ChargerUserName).HasColumnName("CHARGERUSERNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ChargerDept).HasColumnName("CHARGERDEPT").IsOptional().HasMaxLength(200);
            Property(x => x.ChargerDeptName).HasColumnName("CHARGERDEPTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.PaymentApplyValue).HasColumnName("PAYMENTAPPLYVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.LastProgress).HasColumnName("LASTPROGRESS").IsOptional().HasPrecision(18,4);
            Property(x => x.LastValue).HasColumnName("LASTVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.TotalProgress).HasColumnName("TOTALPROGRESS").IsOptional().HasPrecision(18,4);
            Property(x => x.TotalValue).HasColumnName("TOTALVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.CurrentValue).HasColumnName("CURRENTVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.ApplyUser).HasColumnName("APPLYUSER").IsOptional().HasMaxLength(50);
            Property(x => x.ApplyUserName).HasColumnName("APPLYUSERNAME").IsOptional().HasMaxLength(50);
            Property(x => x.ApplyDate).HasColumnName("APPLYDATE").IsOptional();
            Property(x => x.Remark).HasColumnName("REMARK").IsOptional().HasMaxLength(500);
            Property(x => x.Attachment).HasColumnName("ATTACHMENT").IsOptional().HasMaxLength(500);
            Property(x => x.CBSInfoID).HasColumnName("CBSINFOID").IsOptional().HasMaxLength(50);
            Property(x => x.CostUnitID).HasColumnName("COSTUNITID").IsOptional().HasMaxLength(50);
            Property(x => x.CBSNodeID).HasColumnName("CBSNODEID").IsOptional().HasMaxLength(50);
            Property(x => x.PaymentApplyID).HasColumnName("PAYMENTAPPLYID").IsOptional().HasMaxLength(50);
            Property(x => x.SubjectCode).HasColumnName("SUBJECTCODE").IsOptional().HasMaxLength(200);
            Property(x => x.CostState).HasColumnName("COSTSTATE").IsOptional().HasMaxLength(200);
            Property(x => x.TaxRate).HasColumnName("TAXRATE").IsOptional().HasPrecision(18,4);
            Property(x => x.TaxValue).HasColumnName("TAXVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.ClearValue).HasColumnName("CLEARVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.ConfirmDate).HasColumnName("CONFIRMDATE").IsOptional();
        }
    }

    // view_ContractCustomerList
    internal partial class view_ContractCustomerListConfiguration : EntityTypeConfiguration<view_ContractCustomerList>
    {
        public view_ContractCustomerListConfiguration()
        {
            ToTable("VIEW_CONTRACTCUSTOMERLIST");
            HasKey(x => x.ID);

            Property(x => x.ContractType).HasColumnName("CONTRACTTYPE").IsOptional().HasMaxLength(200);
            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.SerialNumber).HasColumnName("SERIALNUMBER").IsOptional().HasMaxLength(200);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(200);
            Property(x => x.ContractRMBAmount).HasColumnName("CONTRACTRMBAMOUNT").IsOptional().HasPrecision(18,4);
            Property(x => x.SumInvoiceValue).HasColumnName("SUMINVOICEVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.NoInvoiceAmount).HasColumnName("NOINVOICEAMOUNT").IsOptional().HasPrecision(19,4);
            Property(x => x.SumReceiptValue).HasColumnName("SUMRECEIPTVALUE").IsOptional().HasPrecision(18,4);
            Property(x => x.PartyA).HasColumnName("PARTYA").IsOptional().HasMaxLength(50);
            Property(x => x.PartyAName).HasColumnName("PARTYANAME").IsOptional().HasMaxLength(50);
            Property(x => x.ProductionDeptName).HasColumnName("PRODUCTIONDEPTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.BusinessDeptName).HasColumnName("BUSINESSDEPTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.SumNoReceiptValue).HasColumnName("SUMNORECEIPTVALUE").IsOptional().HasPrecision(19,4);
            Property(x => x.InvoiceName).HasColumnName("INVOICENAME").IsOptional().HasMaxLength(200);
            Property(x => x.BankAccounts).HasColumnName("BANKACCOUNTS").IsOptional().HasMaxLength(200);
            Property(x => x.BankName).HasColumnName("BANKNAME").IsOptional().HasMaxLength(200);
            Property(x => x.TaxAccounts).HasColumnName("TAXACCOUNTS").IsOptional().HasMaxLength(200);
            Property(x => x.Address).HasColumnName("ADDRESS").IsOptional().HasMaxLength(200);
            Property(x => x.LinkTelphone).HasColumnName("LINKTELPHONE").IsOptional().HasMaxLength(200);
            Property(x => x.CustomerFullID).HasColumnName("CUSTOMERFULLID").IsOptional().HasMaxLength(500);
            Property(x => x.CustomerFullIDName).HasColumnName("CUSTOMERFULLIDNAME").IsOptional().HasMaxLength(500);
        }
    }

    // view_S_B_BidFileInfo
    internal partial class view_S_B_BidFileInfoConfiguration : EntityTypeConfiguration<view_S_B_BidFileInfo>
    {
        public view_S_B_BidFileInfoConfiguration()
        {
            ToTable("VIEW_S_B_BIDFILEINFO");
            HasKey(x => new { x.Files, x.ID });

            Property(x => x.ProjectName).HasColumnName("PROJECTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.Files).HasColumnName("FILES").IsRequired().HasMaxLength(3005);
            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.EngineeringInfo).HasColumnName("ENGINEERINGINFO").IsOptional().HasMaxLength(200);
            Property(x => x.EngineeringInfoName).HasColumnName("ENGINEERINGINFONAME").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectManager).HasColumnName("PROJECTMANAGER").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectManagerName).HasColumnName("PROJECTMANAGERNAME").IsOptional().HasMaxLength(200);
        }
    }

    // view_S_B_ContractFileInfo
    internal partial class view_S_B_ContractFileInfoConfiguration : EntityTypeConfiguration<view_S_B_ContractFileInfo>
    {
        public view_S_B_ContractFileInfoConfiguration()
        {
            ToTable("VIEW_S_B_CONTRACTFILEINFO");
            HasKey(x => new { x.Files, x.ID });

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(200);
            Property(x => x.EngineeringInfo).HasColumnName("ENGINEERINGINFO").IsOptional().HasMaxLength(200);
            Property(x => x.EngineeringInfoName).HasColumnName("ENGINEERINGINFONAME").IsOptional().HasMaxLength(200);
            Property(x => x.Files).HasColumnName("FILES").IsRequired().HasMaxLength(1001);
            Property(x => x.SerialNumber).HasColumnName("SERIALNUMBER").IsOptional().HasMaxLength(200);
        }
    }

    // view_S_B_MarketClueFileInfo
    internal partial class view_S_B_MarketClueFileInfoConfiguration : EntityTypeConfiguration<view_S_B_MarketClueFileInfo>
    {
        public view_S_B_MarketClueFileInfoConfiguration()
        {
            ToTable("VIEW_S_B_MARKETCLUEFILEINFO");
            HasKey(x => new { x.Files, x.ID });

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.ProjectName).HasColumnName("PROJECTNAME").IsOptional().HasMaxLength(200);
            Property(x => x.ProjectCode).HasColumnName("PROJECTCODE").IsOptional().HasMaxLength(200);
            Property(x => x.EngineeringInfo).HasColumnName("ENGINEERINGINFO").IsOptional().HasMaxLength(200);
            Property(x => x.EngineeringInfoName).HasColumnName("ENGINEERINGINFONAME").IsOptional().HasMaxLength(200);
            Property(x => x.Files).HasColumnName("FILES").IsRequired().HasMaxLength(500);
        }
    }

}

