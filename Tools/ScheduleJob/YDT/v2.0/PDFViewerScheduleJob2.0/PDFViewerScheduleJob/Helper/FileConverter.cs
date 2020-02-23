using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using Aspose.Words.Saving;

namespace PDFViewer
{
    /// <summary>
    /// 文件转换器，支持不同文件之间的互转，如：Word转PDF、Excel转PDF、生成缩略图
    /// </summary>
    public class FileConverter
    {
        #region Word2PDF
        public static bool Word2PDF(string wordPath, string pdfPath)
        {
            var pdfBuffer = Word2PDF(GetFileBuffer(wordPath));
            SaveFileBuffer(pdfBuffer, pdfPath);
            return true;
        }

        public static byte[] Word2PDF(byte[] fileBuffer)
        {
            var pdfStream = new MemoryStream();
            var doc = new Aspose.Words.Document(new MemoryStream(fileBuffer));

            // 转PDF
            doc.Save(pdfStream, Aspose.Words.SaveFormat.Pdf);
            var buffer = pdfStream.ToArray();
            pdfStream.Close();

            return buffer;
        }
        #endregion

        #region Word2JPG
        public static List<byte[]> Word2JPG(byte[] fileBuffer)
        {
            var result = new List<byte[]>();

            var doc = new Aspose.Words.Document(new MemoryStream(fileBuffer));
            // 转JPG
            ImageSaveOptions opt = new ImageSaveOptions(Aspose.Words.SaveFormat.Jpeg);
            opt.Resolution = 256;
            for (int i = 1; i <= doc.PageCount; i++)
            {
                var imgStream = new MemoryStream();
                opt.PageIndex = i - 1;
                doc.Save(imgStream, opt);
                result.Add(imgStream.ToArray());
                imgStream.Close();
            }
            return result;
        }
        #endregion

        #region Word2TIFF
        public static byte[] Word2TIFF(byte[] fileBuffer)
        {
            var imgStream = new MemoryStream();
            var doc = new Aspose.Words.Document(new MemoryStream(fileBuffer));
            // 转tiff
            ImageSaveOptions options = new ImageSaveOptions(Aspose.Words.SaveFormat.Tiff);
            options.PageIndex = 0;
            options.PageCount = doc.PageCount;
            options.TiffCompression = TiffCompression.Ccitt4;
            options.Resolution = 256;
            doc.Save(imgStream, options);
            var buffer = imgStream.ToArray();
            imgStream.Close();

            return buffer;
        }
        #endregion


        #region Excel2PDF

        public static bool Excel2PDF(string xlsPath, string pdfPath)
        {
            var pdfBuffer = Excel2PDF(GetFileBuffer(xlsPath));
            SaveFileBuffer(pdfBuffer, pdfPath);
            return true;
        }

        public static byte[] Excel2PDF(byte[] fileBuffer)
        {
            var pdfStream = new MemoryStream();
            var wb = new Aspose.Cells.Workbook(new MemoryStream(fileBuffer));

            // 设置打印在一页，避免宽度超过一页时会出现分页
            foreach (Aspose.Cells.Worksheet sheet in wb.Worksheets)
            {
                sheet.PageSetup.FitToPagesWide = 1;
            }

            // 转PDF
            wb.Save(pdfStream, Aspose.Cells.SaveFormat.Pdf);
            var buffer = pdfStream.ToArray();
            pdfStream.Close();

            return buffer;
        }
        #endregion

        #region PDF2SWF
        /// <summary>
        /// 转换所有的页，图片质量80%
        /// </summary>
        /// <param name="pdfPath">PDF文件地址</param>
        /// <param name="swfPath">生成后的SWF文件地址</param>
        /// <param name="isSplit">是否分页生成</param>
        public static bool PDF2SWF(string pdfPath, string swfPath, bool isSplit = false)
        {
            return PDF2SWF(pdfPath, swfPath, 1, GetPageCount(pdfPath), 80, isSplit);
        }

        /// <summary>
        /// 转换前N页，图片质量80%
        /// </summary>
        /// <param name="pdfPath">PDF文件地址</param>
        /// <param name="swfPath">生成后的SWF文件地址</param>
        /// <param name="page">页数</param>
        /// <param name="isSplit">是否分页生成</param>
        public static bool PDF2SWF(string pdfPath, string swfPath, int page, bool isSplit = false)
        {
            return PDF2SWF(pdfPath, swfPath, 1, page, 80, isSplit);
        }

        /// <summary>
        /// PDF格式转为SWF
        /// </summary>
        /// <param name="pdfPath">PDF文件地址</param>
        /// <param name="swfPath">生成后的SWF文件地址</param>
        /// <param name="beginpage">转换开始页</param>
        /// <param name="endpage">转换结束页</param>
        /// <param name="isSplit">是否分页生成</param>
        private static bool PDF2SWF(string pdfPath, string swfPath, int beginpage, int endpage, int photoQuality, bool isSplit = false)
        {
            string exe = AppDomain.CurrentDomain.BaseDirectory.Trim('\\') + "\\pdf2swf.exe";
            if (!System.IO.File.Exists(exe) || !System.IO.File.Exists(pdfPath) || System.IO.File.Exists(swfPath))
                return false;
            StringBuilder sb = new StringBuilder();
            sb.Append(" \"" + pdfPath + "\"");

            //if (isSplit)
            //    sb.Append(" -o \"" + swfPath.Replace(".swf", "%.swf") + "\"");
            //else
            //    sb.Append(" -o \"" + swfPath + "\"");
            //sb.Append(" -s flashversion=9");
            //if (endpage > GetPageCount(pdfPath)) endpage = GetPageCount(pdfPath);
            //sb.Append(" -p " + "\"" + beginpage + "" + "-" + endpage + "\"");

            sb.Append(" -o \"" + swfPath + "\"");
            sb.Append(" -f -T 9 -s poly2bitmap  ");
            if (endpage > GetPageCount(pdfPath)) endpage = GetPageCount(pdfPath);
            sb.Append(" -p " + "\"" + beginpage + "" + "-" + endpage + "\"");
            sb.Append(" -G ");

            sb.Append(" -j " + photoQuality);
            string Command = sb.ToString();
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = exe;
            p.StartInfo.Arguments = Command;
            //p.StartInfo.WorkingDirectory = //HttpContext.Current.Server.MapPath("~/Bin/");
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = false;
            p.Start();
            p.BeginErrorReadLine();
            p.WaitForExit();
            p.Close();
            p.Dispose();
            return true;
        }
        #endregion

        #region 生成缩略图

        /// <summary>
        /// 生成缩略图，只生成第一张页面的缩略图
        /// </summary>
        /// <param name="filePath">文件路径，目前支持：Word、Excel、PDF</param>
        /// <param name="snapPath">生成缩略图的路径</param>
        public static bool ConvertToSnap(string filePath, string snapPath)
        {
            var fileType = System.IO.Path.GetExtension(filePath).Trim('.');
            var pdfBuffer = ConvertToSnap(GetFileBuffer(filePath), fileType);
            SaveFileBuffer(pdfBuffer, snapPath);
            return true;
        }

        /// <summary>
        /// 生成缩略图，只生成第一张页面的缩略图
        /// </summary>
        /// <param name="fileBuffer">文件的字节数组</param>
        /// <param name="fileType">文件类型，目前支持：Word、Excel、PDF</param>
        public static byte[] ConvertToSnap(byte[] fileBuffer, string fileType)
        {
            var snapStream = new MemoryStream();
            if (fileType == "xls" || fileType == "xlsx")
            {
                var book = new Aspose.Cells.Workbook(new MemoryStream(fileBuffer));
                var sheet = book.Worksheets[0];
                var imgOptions = new Aspose.Cells.Rendering.ImageOrPrintOptions
                {
                    OnePagePerSheet = true,
                    VerticalResolution = 400,
                    HorizontalResolution = 300,
                    ImageFormat = System.Drawing.Imaging.ImageFormat.Jpeg
                };
                var sr = new Aspose.Cells.Rendering.SheetRender(sheet, imgOptions);
                sr.ToImage(0, snapStream);
            }
            else if (fileType == "doc" || fileType == "docx")
            {
                var doc = new Aspose.Words.Document(new MemoryStream(fileBuffer));
                var imgOptions = new Aspose.Words.Saving.ImageSaveOptions(Aspose.Words.SaveFormat.Jpeg) { Resolution = 400, };
                doc.Save(snapStream, imgOptions);

            }
            else if (fileType == "pdf")
            {
                var converter = new Aspose.Pdf.Facades.PdfConverter();
                converter.BindPdf(new Aspose.Pdf.Document(new MemoryStream(fileBuffer)));
                converter.DoConvert();
                converter.GetNextImage(snapStream, new Aspose.Pdf.PageSize(200, 150), System.Drawing.Imaging.ImageFormat.Jpeg);
            }

            var result = snapStream.ToArray();
            snapStream.Close();
            return result;
        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 返回文件的页数
        /// </summary>
        /// <param name="pdfPath">PDF文件地址</param>
        public static int GetPageCount(string pdfFilePath)
        {
            byte[] buffer = System.IO.File.ReadAllBytes(pdfFilePath);
            return GetPageCount(buffer);
        }

        /// <summary>
        /// 返回文件的页数
        /// </summary>
        /// <param name="buffer">PDF文件流</param>
        public static int GetPageCount(byte[] buffer)
        {
            //改为由aspose.pdf读取页码
            var pdfStream = new MemoryStream();
            var doc = new Aspose.Pdf.Document(new MemoryStream(buffer));
            return doc.Pages.Count;
           
            //int length = buffer.Length;
            //if (buffer == null)
            //    return -1;
            //if (buffer.Length <= 0)
            //    return -1;
            //string pdfText = Encoding.Default.GetString(buffer);
            //System.Text.RegularExpressions.Regex rx1 = new System.Text.RegularExpressions.Regex(@"/Type\s*/Page[^s]");
            //System.Text.RegularExpressions.MatchCollection matches = rx1.Matches(pdfText);
            //return matches.Count;
        }

        /// <summary>
        /// 获取指定文件路径的字节数组
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns></returns>
        public static byte[] GetFileBuffer(string filePath)
        {
            FileStream fileStream = new FileStream(filePath, FileMode.Open);
            long fileSize = fileStream.Length;
            byte[] fileBuffer = new byte[fileSize];
            fileStream.Read(fileBuffer, 0, (int)fileSize);
            fileStream.Close();

            return fileBuffer;
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
        }
        #endregion
    }
}
