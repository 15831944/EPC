using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Configuration;

using Common.Logic.DTO;

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
        [DisplayName("成果ID")]
        public string ID { get; set; }

        /// <summary>
        /// 版本
        /// </summary>
        [DisplayName("版本")]
        public decimal? Version { get; set; }

        /// <summary>
        /// 版本ID
        /// </summary>
        [DisplayName("版本ID")]
        public string VersionID { get; set; }

        /// <summary>
        ///文件名称
        /// </summary>
        [DisplayName("文件名称")]
        public string Name { get; set; }

        /// <summary>
        /// 位置信息
        /// </summary>
        [DisplayName("位置信息")]
        public PDFSignPositionDTO PDFPosInfo { get; set; }

        /// <summary>
        /// 签名信息(AuditSignUser)
        /// </summary>
        [DisplayName("签名信息")]
        public List<PdfSignInfo> PDFSignInfos { get; set; }

        /// <summary>
        /// 会签信息(CoSignUser)
        /// </summary>
        [DisplayName("会签信息")]
        public List<PdfCoSignInfo> PDFCoSignInfos { get; set; }

        /// <summary>
        /// 盖章信息
        /// </summary>
        [DisplayName("盖章信息")]
        public List<PdfStampInfoDTO> PDFStampInfos { get; set; }

        /// <summary>
        /// PDF
        /// </summary>
        [DisplayName("PDF")]
        public string PdfFile { get; set; }

        /// <summary>
        /// 章组合ID
        /// </summary>
        [DisplayName("章组合ID")]
        public PlotSealInfo PlotSealGroup { get; set; }
    }

    public enum ResultStatus
    { 
        Process,
        Success,
        Error
    }
}
