using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PDFViewer
{
    public class FileStoreHelper
    {
        private static string fileStoreUrl = System.Configuration.ConfigurationManager.AppSettings["FileStore"];
        public static PDFViewer.FileService.OuterService GetService()
        {
            var fileService = new PDFViewer.FileService.OuterService();
            fileService.Url = fileStoreUrl;
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
    }
}
