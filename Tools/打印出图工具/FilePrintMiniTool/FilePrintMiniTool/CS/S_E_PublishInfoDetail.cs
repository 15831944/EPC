using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FilePrintMiniTool
{
    public class S_E_PublishInfoDetail
    {
        /// <summary>
        /// ID
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 图名
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// 图号
        /// </summary>
        public string ProductCode { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectInfoName { get; set; }
        /// <summary>
        /// 项目编号
        /// </summary>
        public string ProjectCode { get; set; }
        /// <summary>
        /// 项目负责人
        /// </summary>
        public string ProjectManager { get; set; }
        /// <summary>
        /// 项目负责人
        /// </summary>
        public string ProjectManagerName { get; set; }
        /// <summary>
        /// 图纸提供人
        /// </summary>
        public string Provider { get; set; }
        /// <summary>
        /// 专业编号
        /// </summary>
        public string MajorCode { get; set; }
        /// <summary>
        /// 专业
        /// </summary>
        public string MajorName { get; set; }
        /// <summary>
        /// 阶段
        /// </summary>
        public string StepName { get; set; }
        /// <summary>
        /// 提交日期
        /// </summary>
        public string SubmitDate { get; set; }
        /// <summary>
        /// 出图单编号
        /// </summary>
        public string CTNum { get; set; }
        public int Count { get; set; }
        /// <summary>
        /// 纸张大小
        /// </summary>
        public string PaperSize { get; set; }
        /// <summary>
        /// 是否纵向打印
        /// </summary>
        public bool IsVertical { get; set; }
        public string IsVerticalDescrib 
        {
            get 
            {
                return IsVertical ? "纵向" : "横向";
            }
        }
        /// <summary>
        /// 是否已打印
        /// </summary>
        public bool Printed { get; set; }
        /// <summary>pdf文件名称</summary>
        public string PdfFile { get; set; } // PdfFile
        /// <summary>plot文件名称</summary>
        public string PlotFile { get; set; } // PlotFile

        /// <summary></summary>	
        public string S_E_PublishInfoID { get; set; } // S_E_PublishInfoID
        /// <summary>图纸ID</summary>	
        public string ProductID { get; set; } // ProductID        
        /// <summary>出图单编号</summary>
        public string SerialNumber { get; set; } // SerialNumber
        /// <summary>图纸提供人名称</summary>
        public string DesingerName { get; set; } // DesingerName
        /// <summary>提交日期</summary>	
        public DateTime? PublishDate { get; set; } // PublishDate
        /// <summary>项目部门</summary>	
        public string ChargeDeptName { get; set; } // ChargeDeptName
    }
}
