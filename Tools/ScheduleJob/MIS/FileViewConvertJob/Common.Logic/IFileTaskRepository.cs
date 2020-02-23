using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Configuration;

namespace Common.Logic
{
    /// <summary>
    /// 文件转换任务队列的仓储借口
    /// </summary>
    public interface IFileTaskRepository
    {
        /// <summary>
        /// 获取指定ID的PDF文件转换任务, 如果ID为空，则获取最新待执行的任务
        /// </summary>
        /// <param name="fileID">文件ID</param>
        /// <returns></returns>
        FileTask GetTask(string fileID = "");

        /// <summary>
        /// 添加PDF文件转换的任务
        /// </summary>
        /// <param name="task">待添加的任务</param>
        void AddTask(FileTask task);

        /// <summary>
        /// 删除指定ID的PDF文件转换任务
        /// </summary>
        /// <param name="id">任务ID</param>
        void DeleteTask(string id);

        /// <summary>
        /// 开始执行指定ID的任务
        /// </summary>
        /// <param name="id"></param>
        void StartTask(string id);

        /// <summary>
        /// 结束指定ID的任务，并修改任务状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        void EndTask(string id, ResultStatus status);

    }

    /// <summary>
    /// PDF文件转换任务队列，用于在线阅读
    /// </summary>
    public class FileTask
    {
        /// <summary>
        /// 文件ID
        /// </summary>
        [DisplayName("文件ID")]
        public string ID { get; set; }

        /// <summary>
        /// 文件ID，可以是文件名称，也可以是filestore的Id
        /// </summary>
        [DisplayName("文件名称")]
        public string Name { get; set; }

        /// <summary>
        /// 文件类型
        /// </summary>
        [DisplayName("文件类型")]
        public string ExtName { get; set; }

        /// <summary>
        /// PDF文件页数
        /// </summary>
        [DisplayName("PDF文件页数")]
        public int PDFPageCount { get; set; }

        /// <summary>
        /// 转换状态（null、Process、Success、Error）
        /// </summary>
        [DisplayName("转换状态")]
        public ResultStatus ConvertResult { get; set; }

    }

    public enum ResultStatus
    { 
        Process,
        Success,
        Error
    }
}
