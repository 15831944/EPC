using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Configuration;

namespace PDFViewer
{
    /// <summary>
    /// PDF文件转换任务队列的仓储借口
    /// </summary>
    public interface IPDFTaskRepository
    {
        /// <summary>
        /// 获取PDF文件转换的任务队列
        /// </summary>
        /// <returns>集合</returns>
        IList<PDFTask> GetAllTasks();

        /// <summary>
        /// 获取指定ID的PDF文件转换任务, 如果ID为空，则获取最新待执行的任务
        /// </summary>
        /// <param name="Id">任务ID</param>
        /// <returns></returns>
        PDFTask GetTask(string Id = "");

        /// <summary>
        /// 添加PDF文件转换的任务
        /// </summary>
        /// <param name="task">待添加的任务</param>
        void AddTask(PDFTask task);

        /// <summary>
        /// 删除指定ID的PDF文件转换任务
        /// </summary>
        /// <param name="Id">任务ID</param>
        void DeleteTask(string Id);

        /// <summary>
        /// 开始执行指定ID的任务
        /// </summary>
        /// <param name="ID"></param>
        void StartTask(string ID);

        /// <summary>
        /// 结束指定ID的任务，并修改任务状态
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="status"></param>
        void EndTask(string ID, string status, string remark = "");

        /// <summary>
        /// 更新PDF文件ID
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="pdfFileID"></param>
        /// <param name="pdfPageCount"></param>
        /// <param name="isSplit"></param>
        void UpdatePDFFileID(string ID, string pdfFileID, int pdfPageCount, bool isSplit);

        /// <summary>
        /// 更新SWF文件ID
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="swfFileID"></param>
        void UpdateSWFFileID(string ID, string swfFileID);

        /// <summary>
        /// 更新缩略图的文件ID
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="snapFileID"></param>
        void UpdateSnapFileID(string ID, string snapFileID);
    }

    /// <summary>
    /// PDF文件转换任务队列，用于在线阅读
    /// </summary>
    public class PDFTask
    {
        /// <summary>
        /// 任务ID
        /// </summary>
        [DisplayName("任务ID")]
        public string ID { get; set; }

        /// <summary>
        /// 文件ID，可以是文件名称，也可以是filestore的Id
        /// </summary>
        [DisplayName("文件名称")]
        public string FileID { get; set; }

        /// <summary>
        /// 文件类型
        /// </summary>
        [DisplayName("文件类型")]
        public string FileType { get; set; }

        /// <summary>
        /// PDF文件ID
        /// </summary>
        [DisplayName("PDF文件")]
        public string PDFFileID { get; set; }

        /// <summary>
        /// SWF文件ID
        /// </summary>
        [DisplayName("SWF文件")]
        public string SWFFileID { get; set; }

        /// <summary>
        /// 缩略图文件ID
        /// </summary>
        [DisplayName("缩略图")]
        public string SnapFileID { get; set; }

        /// <summary>
        /// PDF文件页数
        /// </summary>
        [DisplayName("PDF文件页数")]
        public int PDFPageCount { get; set; }

        /// <summary>
        /// 是否每页生成一个SWF文件，用于在线阅读的分页加载，提高阅读质量
        /// </summary>
        [DisplayName("每页生成")]
        public bool IsSplit { get; set; }

        /// <summary>
        /// 转换状态（New、Process、Success、Error）
        /// </summary>
        [DisplayName("转换状态")]
        public string Status { get; set; }

        /// <summary>
        /// 转换开始时间，默认是null
        /// </summary>
        [DisplayName("开始时间")]
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 转换结束时间，默认是null
        /// </summary>
        [DisplayName("结束时间")]
        public DateTime? EndTime { get; set; }
    }

    public class IPDFTaskRepositoryFactory
    {
        private static IPDFTaskRepository repo;
        public static IPDFTaskRepository GetRepository()
        {
            if (repo == null)
            {
                var repoType = ConfigurationManager.AppSettings["PDFTaskRepositoryType"];
                if (!string.IsNullOrEmpty(repoType))
                {
                    Type type = Type.GetType(repoType, true, true);
                    repo = Activator.CreateInstance(type) as IPDFTaskRepository;
                }
                else
                {
                    throw new Exception("请配置PDFTaskRepositoryType节");
                }
            }

            return repo;
        }
    }
}
