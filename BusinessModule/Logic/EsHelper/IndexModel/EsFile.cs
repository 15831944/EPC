using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsHelper
{
    public class EsFile
    {
        public string Id { get; set; }//EsDocument主键；如果需更新删除Es记录需要用到
        [Nest.Text(Analyzer = "ik_max_word", SearchAnalyzer = "ik_smart")]
        public string Title { get; set; }
        [Nest.Text(Analyzer = "ik_max_word", SearchAnalyzer = "ik_smart")]
        public string PropertyJson { get; set; }//文件所有上层节点属性
        [Nest.Text(Analyzer = "ik_max_word", SearchAnalyzer = "ik_smart")]
        public string Content { get; set; }

        [Nest.Keyword]//keyword 不分词
        public string SpaceID { get; set; }//图档实例库S_FileInfo主键
        public DateTime FileCreateDate { get; set; }//归档时间(Attr创建时间)
        [Nest.Keyword]//keyword 不分词
        public string FileCreateUser { get; set; }//归档人
        [Nest.Keyword]//keyword 不分词
        public string SecretLevel { get; set; }//密级

        [Nest.Keyword(Index = false)]
        public string FileID { get; set; }//图档实例库S_FileInfo主键
        [Nest.Keyword(Index = false)]
        public string ConfigID { get; set; }
        [Nest.Keyword(Index = false)]
        public string AttrID { get; set; }
        [Nest.Keyword(Index = false)]
        public string NodeID { get; set; }//图档实例库S_NodeInfo主键
        [Nest.Keyword(Index = false)]
        public string FullPathJson { get; set; }//归档文件路径
        [Nest.Keyword(Index = false)]
        public int FsFileID { get; set; }//FileStore文件ID
    }
}
