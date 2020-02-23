using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Logic
{
    /// <summary>
    /// Excel模板元数据信息的存储接口
    /// </summary>
    public interface IExcelMetadataStorage
    {
        /// <summary>
        /// 获取指定Key的模板元数据
        /// </summary>
        /// <param name="key">模板Key</param>
        /// <returns></returns>
        ExcelMetadata GetMetadataByPath(string key);

        /// <summary>
        /// 判断是否为有效的Excel数据文件
        /// </summary>
        /// <param name="xlsBuffer">上传的Excel数据文件</param>
        /// <param name="metadata">模板的元数据信息</param>
        /// <returns></returns>
        bool IsValidExcel(byte[] xlsBuffer, ExcelMetadata metadata);
    }
}
