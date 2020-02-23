using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interface.Logic
{
    public class S_W_TaskWork
    {
        public string ID { get; set; }
        public string ProjectInfoID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string State { get; set; }
        public string SynID { get; set; }
        public DateTime? SynTime { get; set; }
        public string SynData { get; set; }
    }
    public class TaskWorkRequestData
    {
        public string projectId { get; set; }//项目id
        public string id { get; set; }
        public string name { get; set; }
        public string code { get; set; }
        public string userId { get; set; }//任务执行人
        public int type { get; set; }//工作包传1
        public string phaseCode { get; set; }//阶段编号
        public string subProjectName { get; set; }//子项wbsname
        public string specialtyCode { get; set; }//专业编号
        public bool autoPublish { get; set; }
    }
}
