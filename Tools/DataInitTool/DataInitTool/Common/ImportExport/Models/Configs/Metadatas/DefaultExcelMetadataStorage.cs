using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aspose.Cells;
using System.IO;
using System.Web;

namespace Common.Logic
{
    public class DefaultExcelMetadataStorage : IExcelMetadataStorage
    {
        public virtual ExcelMetadata GetMetadataByPath(string path)
        {
            var fileBuffer = GetExcelTemplateFile(path);
            var workbook = new Workbook(new MemoryStream(fileBuffer));
            var property = workbook.BuiltInDocumentProperties;
            var metadata = new ExcelMetadata
            {
                Author = property.Author,
                Company = property.Company,
                Keywords = property.Keywords,
                RevisionNumber = property.RevisionNumber,
                FileBuffer = fileBuffer,
            };
            workbook = null;
            return metadata;
        }

        public virtual bool IsValidExcel(byte[] xlsBuffer, ExcelMetadata metadata)
        {
            var workbook = new Workbook(new MemoryStream(xlsBuffer));
            var property = workbook.BuiltInDocumentProperties;

            return (metadata.Keywords == property.Keywords || property.Keywords == "exportExcel");//或从平台导出的
            //&& metadata.RevisionNumber == metadata.RevisionNumber
            //&& metadata.Author == property.Author
            //&& metadata.Company == property.Company;
        }

        /// <summary>
        /// 保存指定key的Excel模板的文件流
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        protected virtual void SaveExcelTemplateFile(byte[] tmplBuffer, string key)
        {
            var path = System.Configuration.ConfigurationManager.AppSettings["ExcelTemplatePath"];
            if (path.StartsWith("/"))
                path = HttpContext.Current.Server.MapPath(path);
            var tmplPath = path.EndsWith("\\") ? string.Format("{0}{1}.xls", path, key) : string.Format("{0}\\{1}.xls", path, key);
            FileHelper.SaveFileBuffer(tmplBuffer, tmplPath);
        }

        /// <summary>
        /// 获取指定key的Excel模板的文件流
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        protected virtual byte[] GetExcelTemplateFile(string path)
        {
            if (!System.IO.File.Exists(path))
            {
                throw new Exception("找不到指点的模板文件");
            }
            var fileBuffer = FileHelper.GetFileBuffer(path);
            return fileBuffer;
        }
    }
}