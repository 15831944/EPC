using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interface.Logic
{
    public class S_D_Input
    {
        public string ID { get; set; }
        public string ParentID { get; set; }
        public string FullID { get; set; }
        public string ProjectInfoID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int SortIndex { get; set; }
        public string State { get; set; }
        public string SynID { get; set; }
        public DateTime? SynTime { get; set; }
        public string SynData { get; set; }
    }

    public class DesignInputBatchRequestData
    {
        public string parentId { get; set; }//父目录id，根目录项目id
        public List<DesignInputRequestData> folders { get; set; }//公共区文件夹
    }

    public class DesignInputRequestData
    {
        public string Id { get; set; }
        public string name { get; set; }
        public string Code { get; set; }//四方未用到，接口程序自用
        public string ProjectInfoID { get; set; }//四方未用到，接口程序自用
        public string parentId { get; set; }
        public string FullID { get; set; }//四方未用到，接口程序自用
        public int type { get; set; }//type:31=设计输入根目录，type:8=不能修改的目录
        public int sort { get; set; }
        public List<DesignInputRequestData> children { get; set; }//文件夹
    }

}
