using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FilePrintMiniTool.CS
{
    //[Serializable]
    public class SearchPara
    {
        /// <summary>
        /// 图名
        /// </summary>
        public string ProductName = "";
        /// <summary>
        /// 图号
        /// </summary>
        public string ProductCode = "";
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectInfoName = "";
        /// <summary>
        /// 项目编号
        /// </summary>
        public string ProjectCode = "";
        /// <summary>
        /// 项目负责人
        /// </summary>
        public string ProjectManager = "";
        /// <summary>
        /// 图纸提供人
        /// </summary>
        public string Provider = "";
        /// <summary>
        /// 专业
        /// </summary>
        public string MajorCode = "";
        /// <summary>
        /// 提交起始日期
        /// </summary>
        public DateTime? SubmitDateFrm = null;
        /// <summary>
        /// 提交终止日期
        /// </summary>
        public DateTime? SubmitDateTo = null;
        /// <summary>
        /// 作业部门
        /// </summary>
        public string ChargeDeptName = "";
        /// <summary>
        ///  
        /// </summary>
        public string DesignerName = "";
        /// <summary>
        /// 出图单编号
        /// </summary>
        public string CTNum = "";
        /// <summary>
        /// 是否已打印
        /// </summary>
        public bool Printed = false;
        public int CurPage = 1;
        public int PageSize = 10;
    }
}
