using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace OEMSzsowAPI.Helper
{
    public class FileHelper
    {
        FileService.OuterService outService = new FileService.OuterService();
        private static FileHelper instance;
        private FileHelper() { }
        public static FileHelper Instance
        {
            get
            {
                if (null == instance)
                {
                    instance = new FileHelper();
                    instance.outService.Url = ConfigurationManager.AppSettings["FileStore"];
                }
                return instance;
            }
        }

        public string SaveFile(byte[] fileBuffer,string fileName, string fileId)
        {
            try
            {
                if (string.IsNullOrEmpty(fileId))
                {
                    fileId = outService.SaveFile(fileName, string.Empty, string.Empty, string.Empty, fileBuffer, fileBuffer.Length, string.Empty);
                }
                else
                {
                    outService.OverwriteFile(fileId, string.Empty, string.Empty, string.Empty, fileBuffer, fileBuffer.Length, string.Empty);
                }
                return fileId;
            }
            catch (Exception ex)
            {
                //WebLog.WriteException(ex);
                throw ex;
            }
        }
        
    }
}
