using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace Common.Logic.Domain
{
    public class ProjectContext : DbContext
    {
        public ProjectContext() : base("Project") { }
        public IDbSet<S_EP_PlotSealInfo> S_EP_PlotSealInfo { get; set; } // S_EP_PlotSealInfo
    }

    public class S_EP_PlotSealInfo
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
}
