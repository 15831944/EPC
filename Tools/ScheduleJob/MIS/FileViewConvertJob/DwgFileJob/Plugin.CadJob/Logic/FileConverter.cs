using System;
using System.Configuration;
using System.IO;
using System.Linq;
using ICSharpCode.SharpZipLib.Zip;
using System.Data;
using System.Collections.Generic;
using Common.Logic;
using Common.Logic.Enum;
using Common.Logic.DTO;

namespace Plugin.CadJob
{
    public class FileConverter
    {
        public static ResultDTO Exec(ImageDTO file)
        {
            if (file.Versions.Count() > 0)
            {
                //兼容写法，只取第一个（版本功能暂时关闭）
                var version = file.Versions[0];
                //解压字体和引用
                string xrefPath = version.XrefPath;
                var dir = Path.GetDirectoryName(version.FullPath);
                int num = Convert.ToInt32(file.ID) / 1000 + 1;

                string basePath = "Files/Dwg/" + string.Format("{0}", num.ToString("D8"));

                if (!string.IsNullOrEmpty(xrefPath))
                {
                    xrefPath = basePath + xrefPath.Substring(xrefPath.IndexOf("DownLoads", StringComparison.Ordinal));
                    ZipToFolder(xrefPath, dir, 4096);
                }

                string fontPath = version.FontPath;
                if (!string.IsNullOrEmpty(fontPath))
                {
                    fontPath = basePath + fontPath.Substring(fontPath.IndexOf("DownLoads", StringComparison.Ordinal));
                    ZipToFolder(fontPath, dir, 4096);
                }

                //打印
                var result = DwgPlot.plotTest(version.ID, version.FullPath, version.FileID);

                if (result == null || !result.status)
                    LogWriter.Error(string.Format("文件[{0}]转图失败！{1}", file.Name, result.ErrInfo));

                var snapResult = DwgPlot.plotTest(version.ID, version.FullPath, version.FileID, true);
                if (snapResult == null || !snapResult.status)
                    LogWriter.Error(string.Format("文件[{0}]生成缩略图失败！{1}", file.Name, snapResult.ErrInfo));

                return result;
            }
            return null;
        }

        /// <summary>
        /// 解压缩zip目录
        /// </summary>
        /// <param name="zipFilePath">解压的zip文件路径</param>
        /// <param name="extractPath">解压到的文件夹路径</param>
        /// <param name="bufferSize">读取文件的缓冲区大小</param>
        private static void ZipToFolder(string zipFilePath, string extractPath, int bufferSize)
        {
            extractPath += "//";
            byte[] data = new byte[bufferSize];
            int size;
            using (ZipInputStream s = new ZipInputStream(File.OpenRead(zipFilePath)))
            {
                ZipEntry entry;
                while ((entry = s.GetNextEntry()) != null)
                {
                    string directoryName = Path.GetDirectoryName(entry.Name);
                    string fileName = Path.GetFileName(entry.Name);

                    //先创建目录
                    if (directoryName.Length > 0)
                        Directory.CreateDirectory(extractPath + directoryName);

                    if (fileName != String.Empty)
                    {
                        using (FileStream streamWriter = File.Create(extractPath + entry.Name.Replace("/", "//")))
                        {
                            while (true)
                            {
                                size = s.Read(data, 0, data.Length);
                                if (size > 0)
                                    streamWriter.Write(data, 0, size);
                                else
                                    break;
                            }
                        }
                    }
                }
            }
        }


    }
}
