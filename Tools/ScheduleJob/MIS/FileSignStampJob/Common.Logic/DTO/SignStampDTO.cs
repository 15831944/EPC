using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Common.Logic.DTO
{
    /// <summary>
    /// 校审状态（待提交 设计 校对 审核 审定 批准 通过 会签 其他）
    /// </summary>
    public enum PackageStateTypes
    {
        [Description("待提交")]
        OnCreate,
        [Description("修订中")]
        OnDesign,
        [Description("校对中")]
        OnCollate,
        [Description("审核中")]
        OnAudit,
        [Description("审定中")]
        OnApprove,
        [Description("批准中")]
        OnAgree,
        [Description("已通过")]
        OnFinish,
        [Description("会签")]
        OnCoSign,
        [Description("项目经理审批")]
        OnProjectManager,
        [Description("专业总工程师审批")]
        OnMajorEngineer,
        [Description("设总审批")]
        OnDesignManager,
        [Description("制图人审批")]
        OnMapper,
        [Description("专业负责人审批")]
        OnMajorPrinciple,
        [Description("部门负责人审批")]
        OnDeptManager,
        [Description("其他")]
        Other
    }

    /// <summary>
    /// 成果设校审状态枚举
    /// </summary>
    [Description("成果设校审状态枚举")]
    public enum AuditState
    {
        /// <summary>
        /// 未提交
        /// </summary>
        [Description("未提交")]
        Create,
        /// <summary>
        /// 批准中
        /// </summary>
        [Description("批准中")]
        Agree,
        /// <summary>
        /// 校审完成
        /// </summary>
        [Description("校审完成")]
        Pass,
        /// <summary>
        /// 设计人提交
        /// </summary>
        [Description("设计人提交")]
        Design,
        /// <summary>
        /// 设计人提交
        /// </summary>
        [Description("设计人提交")]
        Designer,
        /// <summary>
        /// 校核中
        /// </summary>
        [Description("校核中")]
        Collact,
        /// <summary>
        /// 校核中
        /// </summary>
        [Description("校核中")]
        Collactor,
        /// <summary>
        /// 审核中
        /// </summary>
        [Description("审核中")]
        Audit,
        /// <summary>
        /// 审核中
        /// </summary>
        [Description("审核中")]
        Auditor,
        /// <summary>
        /// 审定中
        /// </summary>
        [Description("审定中")]
        Approve,
        /// <summary>
        /// 审定中
        /// </summary>
        [Description("审定中")]
        Approver,
        /// <summary>
        /// 项目负责人
        /// </summary>
        [Description("项目负责人")]
        ProjectManager,
        /// <summary>
        /// 设总
        /// </summary>
        [Description("设总")]
        DesignManager,
        /// <summary>
        /// 制图人
        /// </summary>
        [Description("制图人")]
        Mapper,
        /// <summary>
        /// 专业负责人
        /// </summary>
        [Description("专业负责人")]
        MajorPrinciple,
        /// <summary>
        /// 专业总工程师
        /// </summary>
        [Description("专业总工程师")]
        MajorEngineer,
        /// <summary>
        /// 部门负责人
        /// </summary>
        [Description("部门负责人")]
        DeptManager,
        /// <summary>
        /// 项目助理
        /// </summary>
        [Description("项目助理")]
        ProjectAssistant,
        /// <summary>
        /// 会签
        /// </summary>
        [Description("会签")]
        CoSign
    }

    public class PdfSignInfo
    {
        //用户ID
        public string UserID { get; set; }
        //用户名
        public string UserName { get; set; }
        //校审环节
        public string ActivityKey { get; set; }
        //校审日期
        public string SignDate { get; set; }
        //环节描述
        public string AuditStepDes { get; set; }
    }

    public class PdfCoSignInfo
    {
        //专业代码
        public string MajorValue { get; set; }
        //专业名称
        public string MajorName { get; set; }
        //用户ID
        public string UserID { get; set; }
        //用户名
        public string UserName { get; set; }
    }

    /// <summary>
    /// 图章信息
    /// </summary>
    public class PdfStampInfoDTO
    {
        //主章
        public StampInfoDTO MainSeal { get; set; }
        //从章
        public List<StampInfoDTO> FollowSeals { get; set; }
    }

    public class StampInfoDTO
    {
        //图章ID
        public string ID { get; set; }
        //块名
        public string BlockKey { get; set; }
        //认证信息
        public string AuthInfo { get; set; }
        //长度
        public decimal? Width { get; set; }
        //高度
        public decimal? Height { get; set; }
        //与主章的偏移位置
        public decimal? CorrectPosX { get; set; }
        //与主章的偏移位置
        public decimal? CorrectPosY { get; set; }
        //ca一体机的唯一标识
        public int? Relational { get; set; }
    }

    /// <summary>
    /// PDF签名位置
    /// </summary>
    public class PDFSignPositionDTO
    {
        /// <summary>
        /// 图框分组
        /// </summary>
        public string FrameGroup { get; set; }
        /// <summary>
        /// 图框类型
        /// </summary>
        public string FrameType { get; set; }
        /// <summary>
        /// 图幅
        /// </summary>
        public string FrameSize { get; set; }
        /// <summary>
        /// 纸张宽度
        /// </summary>
        public double PaperWidth { get; set; }
        /// <summary>
        /// 纸张高度
        /// </summary>
        public double PaperHeight { get; set; }
        /// <summary>
        /// 签名角色
        /// </summary>
        public List<string> AuditRoles { get; set; }
        /// <summary>
        /// 签名位置X坐标
        /// </summary>
        public List<double> PositionXs { get; set; }
        /// <summary>
        /// 签名位置Y坐标
        /// </summary>
        public List<double> PositionYs { get; set; }
        /// <summary>
        /// 签名角色类型
        /// </summary>
        public List<string> AuditRolesType { get; set; }
        /// <summary>
        /// 签名域单个配置
        /// </summary>
        public List<AuditRolesConfigInfo> AuditRolesConfig { get; set; }
        /// <summary>
        /// 签名位置页码
        /// </summary>
        public List<int> AuditRolesPageNum { get; set; }
        /// <summary>
        /// PDF每页的长宽
        /// </summary>
        public List<FramePropertyInfo> FrameProperty { get; set; }
        /// <summary>
        /// 签名类型是否签名
        /// </summary>
        public Dictionary<string, bool> PDFSignRoleConfig { get; set; }
        /// <summary>
        /// 图框的配置信息（从CAD端提交会有）
        /// </summary>
        public List<BorderConfigInfo> BorderConfigs { get; set; }
    }

    public class AuditRolesConfigInfo
    {
        public double Width { get; set; }
        public double Height { get; set; }
        public int Angle { get; set; }
        public double CharHeight { get; set; }
        public bool Visiable { get; set; }
    }

    public class FramePropertyInfo
    {
        public double PaperWidth { get; set; }
        public double PaperHeight { get; set; }
        public string FrameType { get; set; }
    }

    public class PlotSealInfo
    {
        public string Groups { get; set; }
        public string Seals { get; set; }
        public string All { get; set; }
    }

    public class BorderConfigInfo
    {
        public string Category { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double Angle { get; set; }
        public double CharHeight { get; set; }
        public double CorrectPosX { get; set; }
        public double CorrectPosY { get; set; }
    }

    public class ProductVersion
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string ProductID { get; set; }
        public decimal? Version { get; set; }
        public string PdfFile { get; set; }
        public string AuditSignUser { get; set; }
        public string CoSignUser { get; set; }
        public string PDFSignPositionInfo { get; set; }
        public string PlotSealGroup { get; set; }
        public string SignPdfFile { get; set; }
    }

    public class S_EP_PlotSealGroup_GroupInfo
    {
        public string ID { get; set; }
        public string S_EP_PlotSealGroupID { get; set; }
        public double? SortIndex { get; set; }
        public string IsMain { get; set; }
        public string PlotSeal { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string BlockKey { get; set; }
        public string Type { get; set; }
        public string BelongUser { get; set; }
        public string BelongUserName { get; set; }
        public string AuthInfo { get; set; }
        public decimal? Width { get; set; }
        public decimal? Height { get; set; }
        public decimal? CorrectPosX { get; set; }
        public decimal? CorrectPosY { get; set; }
    }

    public class RoleDefine
    {
        public string RoleCode { get; set; }
        public string RoleName { get; set; }
        public string OtherName { get; set; }
    }
}
