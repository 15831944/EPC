using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Common.Logic
{
    public class FileStoreHelper
    {

        public static Common.Logic.FileService.OuterService GetService()
        {
            var url = AppConfig.GetAppSettings("FileStore");
            var fileService = new Common.Logic.FileService.OuterService(url);

            return fileService;
        }

        public static byte[] GetFile(string id)
        {
            var fileBuffer = GetService().GetFileBytes(id, 0, int.MaxValue);
            return fileBuffer;
        }

        public static string SaveFile(byte[] fileBuffer)
        {
            var id = GetService().SaveFile("ConvertPDF.pdf", Guid.NewGuid().ToString(), "", "", fileBuffer, fileBuffer.Length, "ConvertPDF");
            return id;
        }

        public static string SaveFile(byte[] fileBuffer, string fileName)
        {
            var id = GetService().SaveFile(fileName, string.Empty, string.Empty, string.Empty, fileBuffer, fileBuffer.Length, string.Empty);
            return id;
        }

        public static string SaveFile(byte[] fileBuffer, string fileName, string relateID)
        {
            var id = GetService().SaveFile(fileName, relateID, string.Empty, string.Empty, fileBuffer, fileBuffer.Length, string.Empty);
            return id;
        }

        /// <summary>
        /// 将字节数组保存为文件
        /// </summary>
        /// <param name="buffer">字节数组</param>
        /// <param name="filePath">文件路径</param>
        public static void SaveFileBuffer(byte[] buffer, string filePath)
        {
            Stream s = new FileStream(filePath, FileMode.Create);
            s.Write(buffer, 0, buffer.Length);
            s.Flush();
            s.Close();

            if (!System.IO.File.Exists(filePath))
            {
                throw new System.Exception(string.Format("文件没有写入，请确认目录是否有写权限！目录路径为：{0}", filePath));
            }
        }
    }
}
