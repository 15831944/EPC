using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace QrCodeProductData.Models
{
    public class ProjectContext : DbContext
    {
        static ProjectContext()
        {
            Database.SetInitializer<ProjectContext>(null);
        }
        public ProjectContext() : base("Project") { }
        public IDbSet<S_I_ProjectInfo> ProjectInfos { get; set; }
        public IDbSet<S_E_Product> Products { get; set; }
        public IDbSet<S_E_ProductVersion> ProductVersions { get; set; }
        public IDbSet<S_W_WBS> WBS { get; set; }
    }

    public partial class S_I_ProjectInfo
    {
        /// <summary>主键</summary>	
        public string ID { get { return _ID; } set { _ID = value ?? ""; } } // ID (Primary key)
        private string _ID = "";
        /// <summary>项目编码</summary>	
        public string Code { get { return _Code; } set { _Code = value ?? ""; } } // Code
        private string _Code = "";
        /// <summary>项目名称</summary>	
        public string Name { get { return _Name; } set { _Name = value ?? ""; } } // Name
        private string _Name = "";
        public string ChargeDeptID { get { return _ChargeDeptID; } set { _ChargeDeptID = value ?? ""; } } // DeptID
        private string _ChargeDeptID = "";
        /// <summary>承担部门</summary>	
        public string ChargeDeptName { get { return _ChargeDeptName; } set { _ChargeDeptName = value ?? ""; } } // DeptName
        private string _ChargeDeptName = "";
        /// <summary>项目经理</summary>	
        public string ChargeUserName { get { return _ChargeUserName; } set { _ChargeUserName = value ?? ""; } } // ManagerName
        private string _ChargeUserName = "";
        /// <summary>项目经理ID</summary>	
        public string ChargeUserID { get { return _ChargeUserID; } set { _ChargeUserID = value ?? ""; } } // ManagerID
        private string _ChargeUserID = "";
        /// <summary>业务要求开始时间</summary>	
        //public DateTime? DelegateDate { get; set; } // MarketPlanStartDate
        /// <summary>计划开始日期</summary>	
        public DateTime? PlanStartDate { get; set; } // PlanStartDate
        /// <summary>计划结束日期</summary>	
        public DateTime? PlanFinishDate { get; set; } // PlanEndDate
        /// <summary>状态，如立项、策划、执行中、完工、中止等</summary>	
        public string State { get { return _State; } set { _State = value ?? ""; } } // Status
        private string _State = "";
        /// <summary>创建人</summary>	
        public string CreateUser { get { return _CreateUser; } set { _CreateUser = value ?? ""; } } // CreateUserName
        private string _CreateUser = "";
        /// <summary>创建人ID</summary>	
        public string CreateUserID { get { return _CreateUserID; } set { _CreateUserID = value ?? ""; } } // CreateUserID
        private string _CreateUserID = "";
        /// <summary>创建时间</summary>	
        public DateTime? CreateDate { get; set; } // CreateTime

        public string CustomerName { get; set; }
    }

    public partial class S_E_Product
    {

        public string ID { get; set; } // ID (Primary key)
        /// <summary>项目信息ID</summary>	
        public string ProjectInfoID { get; set; } // ProjectInfoID
        /// <summary>WBS关联ID</summary>	
        public string WBSID { get; set; } // WBSID

        public string WBSFullID { get; set; } // WBSFullID

        public string MajorValue { get; set; } // MajorValue

        //public string MajorName { get; set; } // MajorName

        public string PhaseValue { get; set; } // PhaseValue

        //public string PhaseName { get; set; } // PhaseName
        /// <summary>成果名称</summary>	
        public string Name { get; set; } // Name
        /// <summary>成果编号</summary>	
        public string Code { get; set; } // Code
        /// <summary>关联校审批次ID</summary>	
        public string AuditID { get; set; } // AuditID
        /// <summary>关联会签批次ID</summary>	
        public string CounterSignAuditID { get; set; } // CounterSignAuditID
        /// <summary>校审状态</summary>	
        public string AuditState { get; set; } // AuditState
        /// <summary>出图状态</summary>	
        public string PrintState { get; set; } // PrintState
        /// <summary>归档状态</summary>	
        public string ArchiveState { get; set; } // ArchiveState
        /// <summary>签字状态</summary>	
        public string SignState { get; set; } // SignState
        /// <summary>成果状态</summary>	
        public string State { get; set; } // State
        /// <summary>折合A1张数</summary>	
        public decimal? ToA1 { get; set; } // ToA1
        /// <summary>文件类型</summary>	
        public string FileType { get; set; } // FileType
        /// <summary>版本号</summary>	
        public decimal Version { get; set; } // Version
        /// <summary>主文件</summary>	
        public string MainFile { get; set; } // MainFile
        /// <summary>打印文件</summary>	
        //public string PrintFile { get; set; } // PrintFile
        /// <summary>浏览文件</summary>	
        //public string ExploreFile { get; set; } // ExploreFile
        /// <summary>缩略图文件</summary>	
        public string ShotSnap { get; set; } // ShotSnap
        /// <summary>其他附件</summary>	
        public string Attachments { get; set; } // Attachments
        /// <summary>提交时间</summary>	
        public DateTime? SubmitDate { get; set; } // SubmitDate
        /// <summary>校审通过日期</summary>	
        public DateTime? AuditPassDate { get; set; } // AuditPassDate
        /// <summary>描述</summary>	
        public string Description { get; set; } // Description
        /// <summary>归档日期</summary>	
        public DateTime? ArchiveDate { get; set; } // ArchiveDate
        /// <summary>创建人</summary>	
        public string CreateUser { get; set; } // CreateUser
        /// <summary>创建人ID</summary>	
        public string CreateUserID { get; set; } // CreateUserID
        /// <summary>创建日期</summary>	
        public DateTime CreateDate { get; set; } // CreateDate
        /// <summary>全文检索列</summary>	
        public string FullContext { get; set; } // FullContext

        public string ModifyUserID { get; set; } // ModifyUserID

        public string ModifyUser { get; set; } // ModifyUser

        public DateTime? ModifyDate { get; set; } // ModifyDate

        public string PDFAuditFiles { get; set; }

        public string FrameAllAttInfo { get; set; }
    }

    public partial class S_E_ProductVersion
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        /// <summary>项目信息ID</summary>	
        public string ProjectInfoID { get; set; }
        public string ProductID { get; set; }
        public string WBSID { get; set; }
        public string WBSFullID { get; set; }
        /// <summary>WBS关联ID</summary>	
        public decimal Version { get; set; }

        public string MainFile { get; set; }

        //public string PrintFile { get; set; }

        //public string ExploreFile { get; set; }

        public string Attachments { get; set; }

        //public string CurrentVersion { get; set; }

        public string DesignerName { get; set; }
        public string CollactorName { get; set; }
        public string AuditorName { get; set; }
        public string ApproverName { get; set; }
        public string FrameAllAttInfo { get; set; }
    }

    public partial class S_W_WBS
    {
        /// <summary>主键</summary>	
        public string ID { get { return _ID; } set { _ID = value ?? ""; } } // ID (Primary key)
        private string _ID = "";
        /// <summary>项目ID</summary>	
        public string ProjectInfoID { get { return _ProjectInfoID; } set { _ProjectInfoID = value ?? ""; } } // ProjectID
        private string _ProjectInfoID = "";
        /// <summary>父节点ID</summary>	
        public string ParentID { get { return _ParentID; } set { _ParentID = value ?? ""; } } // ParentID
        private string _ParentID = "";
        /// <summary>全路径ID</summary>	
        public string FullID { get { return _FullID; } set { _FullID = value ?? ""; } } // FullNodeID
        private string _FullID = "";
        /// <summary>节点类型，如项目、子项、专业等</summary>	
        public string WBSType { get { return _WBSType; } set { _WBSType = value ?? ""; } } // NodeType
        private string _WBSType = "";
        /// <summary>节点编码</summary>	
        public string WBSValue { get { return _WBSValue; } set { _WBSValue = value ?? ""; } } // WBSValue
        private string _WBSValue = "";
        /// <summary>节点名称</summary>	
        public string Name { get { return _Name; } set { _Name = value ?? ""; } } // NodeName
        private string _Name = "";
        /// <summary>计划开始日期</summary>	
        public DateTime? PlanStartDate { get; set; } // PlanStartDate
        /// <summary>计划完成日期</summary>	
        public DateTime? PlanEndDate { get; set; } // PlanEndDate
        /// <summary>实际开始日期</summary>	
        public DateTime? FactStartDate { get; set; } // FactStartDate
        /// <summary>实际完成日期</summary>	
        public DateTime? FactEndDate { get; set; } // FactEndDate
        /// <summary>状态，如创建、发布、完成</summary>	
        public string State { get { return _State; } set { _State = value ?? ""; } } // Status
        private string _State = "";

        public string ChargeUserName { get; set; }
    }
}