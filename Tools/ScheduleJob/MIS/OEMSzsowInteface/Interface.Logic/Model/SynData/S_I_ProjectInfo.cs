using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interface.Logic
{
    public class S_I_ProjectInfo
    {
        public string ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string State { get; set; }
        public string SynID { get; set; }
        public DateTime? SynTime { get; set; }
        public string SynData { get; set; }
    }
    public class ProjectRequestData
    {
        public string id { get; set; }//项目id
        public string code { get; set; }//项目编号
        public string name { get; set; }//项目名称
        public string owerUserId { get; set; }//项目经理
        public string constructionUnit { get; set; }//建设单位
        public List<phase> phases { get; set; }//阶段
        public bool enable { get; set; }//是否停用
        public bool standard { get; set; }//是否标准产品；（中山、华景城以外设置为true，主要是专业、阶段传Code了）
        public bool isdeleted { get; set; }//是否删除
        public List<drawInfo> drawInfo { get; set; }//图框扩展属性
    }
    public class phase
    {
        public string PhaseCode { get; set; }//阶段名称
        public List<ProjectUser> phaseUsers { get; set; }//专业、用户
        public List<subProject> subProjects { get; set; }//子项
    }
    public class ProjectUser
    {
        public string UserID { get; set; }//用户id
        public string SpecialtyCode { get; set; }//专业名称
        public string RoleCode { get; set; }//四方角色编号
        //public bool PM { get; set; }//项目经理
        //public bool Master { get; set; }//设计
        //public bool Proof { get; set; }//校核
        //public bool Audit { get; set; }//审核
        //public bool Approval { get; set; }//审定
        //public bool Drafting { get; set; }//制图
    }
    public class subProject
    {
        public string id { get; set; }//子项WBSID
        public string Name { get; set; }//子项名称
        public string Code { get; set; }//子项编号
        public List<ProjectUser> users { get; set; }//专业、用户
    }
    public class drawInfo
    {
        public string ID { get; set; }
        public string Code { get; set; }//属性名称
        public string Name { get; set; }//属性值
        public string ProjectId { get; set; }//项目id，必须
        public string Description { get; set; } //ParentID字段
    }
}
