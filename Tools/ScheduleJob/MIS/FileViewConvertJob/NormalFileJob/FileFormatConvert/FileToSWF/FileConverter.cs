using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using Aspose.Words.Saving;
using System.Drawing.Imaging;
using Aspose.Cells.Rendering;
using Aspose.Words;
using O2S.Components.PDFRender4NET;
using Common.Logic;
using Common.Logic.DTO;
using Common.Logic.Enum;
using Word = Microsoft.Office.Interop.Word;

namespace FileFormatConvert
{
    /// <summary>
    /// 文件转换器，支持不同文件之间的互转，如：Word转PDF、Excel转PDF、生成缩略图
    /// </summary>
    public class FileConverter
    {
        /// <summary>
        /// 主入口
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public ResultDTO Exec(ImageDTO file)
        {
            if (file.Versions.Count() > 0)
            {
                //兼容写法，只取第一个（版本功能暂时关闭）
                var version = file.Versions[0];

                var result = this.Converter(version.Suffix, version.FullPath, version.ID, file.ID);
                if (result == null || !result.status)
                    LogWriter.Error(string.Format("文件[{0}]转图失败！{1}", file.Name, result.info));

                return result;
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="suffix"></param>
        /// <param name="fileFullPath"></param>
        /// <param name="fileID"></param>
        /// <param name="taskID"></param>
        /// <returns></returns>
        private ResultDTO Converter(string suffix, string fileFullPath, string fileID, string taskID)
        {
            var result = new ResultDTO();
            switch (suffix.ToUpper())
            {
                case ".DOC":
                case ".DOCX":
                    result = Office2JPG(fileFullPath, suffix, fileID, taskID);
                    break;
                case ".XLS":
                case ".XLSX":
                    result = Office2JPG(fileFullPath, suffix, fileID, taskID);
                    break;
                case ".PDF":
                    result = Office2JPG(fileFullPath, suffix, fileID, taskID);
                    break;
                case ".PNG":
                case ".JPG":
                case ".JPEG":
                case ".GIF":
                    result = Office2JPG(fileFullPath, suffix, fileID, taskID);
                    break;
                default:
                    result = null;
                    break;
            }
            return result;
        }
        
        
        #region Word2PDF

        /// <summary>
        /// 将word文档转换成PDF格式
        /// </summary>
        /// <param name="sourcePath">源文件路径</param>
        /// <param name="targetPath">目标文件路径</param>
        /// <returns></returns>
        private static bool WordConvertPDF(string sourcePath, string targetPath)
        {
            bool result;
            Word.WdExportFormat exportFormat = Word.WdExportFormat.wdExportFormatPDF;   //PDF格式
            object paramMissing = Type.Missing;
            Word.Application wordApplication = new Word.Application();
            Word.Document wordDocument = null;
            try
            {
                object paramSourceDocPath = sourcePath;
                string paramExportFilePath = targetPath;

                Word.WdExportFormat paramExportFormat = exportFormat;
                bool paramOpenAfterExport = false;
                Word.WdExportOptimizeFor paramExportOptimizeFor =
                        Word.WdExportOptimizeFor.wdExportOptimizeForPrint;
                Word.WdExportRange paramExportRange = Word.WdExportRange.wdExportAllDocument;
                int paramStartPage = 0;
                int paramEndPage = 0;
                Word.WdExportItem paramExportItem = Word.WdExportItem.wdExportDocumentContent;
                bool paramIncludeDocProps = true;
                bool paramKeepIRM = true;
                Word.WdExportCreateBookmarks paramCreateBookmarks =
                        Word.WdExportCreateBookmarks.wdExportCreateWordBookmarks;
                bool paramDocStructureTags = true;
                bool paramBitmapMissingFonts = true;
                bool paramUseISO19005_1 = false;

                wordDocument = wordApplication.Documents.Open(
                        ref paramSourceDocPath, ref paramMissing, ref paramMissing,
                        ref paramMissing, ref paramMissing, ref paramMissing,
                        ref paramMissing, ref paramMissing, ref paramMissing,
                        ref paramMissing, ref paramMissing, ref paramMissing,
                        ref paramMissing, ref paramMissing, ref paramMissing,
                        ref paramMissing);

                if (wordDocument != null)
                {
                    wordDocument.ExportAsFixedFormat(paramExportFilePath,
                            paramExportFormat, paramOpenAfterExport,
                            paramExportOptimizeFor, paramExportRange, paramStartPage,
                            paramEndPage, paramExportItem, paramIncludeDocProps,
                            paramKeepIRM, paramCreateBookmarks, paramDocStructureTags,
                            paramBitmapMissingFonts, paramUseISO19005_1,
                            ref paramMissing);
                    result = true;
                }
                else
                    result = false;
            }
            catch (Exception ex)
            {
                result = false;
            }
            finally
            {
                if (wordDocument != null)
                {
                    wordDocument.Close(ref paramMissing, ref paramMissing, ref paramMissing);
                    wordDocument = null;
                }
                if (wordApplication != null)
                {
                    wordApplication.Quit(ref paramMissing, ref paramMissing, ref paramMissing);
                    wordApplication = null;
                }
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            return result;
        }

        public static byte[] Word2PDF(byte[] fileBuffer, string pdfPath, string extName)
        {
            byte[] buffer = null;

            string docPath = pdfPath.Replace(Path.GetExtension(pdfPath), "." + extName);
            SaveFileBuffer(fileBuffer, docPath);
            WordConvertPDF(docPath, pdfPath);
            buffer = File.ReadAllBytes(pdfPath);
            if (File.Exists(docPath))
                File.Delete(docPath);

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
            options.TiffCompression = Aspose.Words.Saving.TiffCompression.Ccitt4;
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

        #region Office2JPG
        public static ResultDTO Office2JPG(string path, string suffix, string fileID, string taskID)
        {
            int highHeightUnit = 0;
            try
            {
                switch (suffix.ToUpper())
                {
                    case ".DOC":
                    case ".DOCX":
                        highHeightUnit = LeafletWord2JPG(path, fileID, taskID);
                        break;
                    case ".XLS":
                    case ".XLSX":
                        highHeightUnit = LeafletExcel2JPG(path, fileID, taskID);
                        break;
                    case ".PDF":
                        highHeightUnit = LeafletPdf2JPG(path, fileID, taskID);
                        break;
                    case ".PNG":
                    case ".JPG":
                    case ".JPEG":
                    case ".GIF":
                        highHeightUnit = LeafletImage2JPG(path, fileID, taskID);
                        break;
                    default:
                        break;
                }

                return new ResultDTO { status = true, HighHeightUnit = highHeightUnit };
            }
            catch (Exception ex)
            {
                return new ResultDTO { status = false, HighHeightUnit = highHeightUnit };
            }
        }
        #endregion

        #region LeafletWord2JPG
        public static int LeafletWord2JPG(string wordPath, string fileID, string taskID)
        {
            curIndex = 1;
            int highHeightUnit = 0;
            Document doc = new Document(wordPath);
            string root = Path.Combine(OfficeHelper.GetFolderPath(taskID, "Office"), fileID);
            imgHeight = 0; listBitmaps = new Dictionary<string, Bitmap>();
            if (!Directory.Exists(@root))
            {
                Directory.CreateDirectory(@root);
            }

            // 转JPG
            ImageSaveOptions opt = new ImageSaveOptions(Aspose.Words.SaveFormat.Jpeg);
            opt.Resolution = 100;
            opt.PrettyFormat = true;
            opt.UseAntiAliasing = true;
            List<Bitmap> listImage = new List<Bitmap>();
            for (int i = 1; i <= doc.PageCount; i++)
            {
                var imgStream = new MemoryStream();
                opt.PageIndex = i - 1;
                doc.Save(imgStream, opt);

                Bitmap image = (Bitmap)Image.FromStream(imgStream);
                if (image != null)
                    listImage.Add(image);
                imgStream.Close();
            }
            highHeightUnit = MergerImageAndCut(listImage, root, true);

            foreach (KeyValuePair<string, Bitmap> bitmaps in listBitmaps)
            {
                Bitmap bitmap = bitmaps.Value;
                if (bitmap != null)
                {
                    ImageFormat imageFormat = ImageFormat.Jpeg;
                    bitmap.Save(bitmaps.Key, imageFormat);
                }
            }
            return highHeightUnit;
        }
        #endregion

        #region LeafletExcel2JPG
        public static int LeafletExcel2JPG(string excelPath, string fileID, string taskID)
        {
            curIndex = 1;
            int highHeightUnit = 0;
            var wb = new Aspose.Cells.Workbook(new MemoryStream(GetFileBuffer(excelPath)));
            imgHeight = 0; listBitmaps = new Dictionary<string, Bitmap>();
            string root = Path.Combine(OfficeHelper.GetFolderPath(taskID, "Office"), fileID);
            if (!Directory.Exists(@root))
            {
                Directory.CreateDirectory(@root);
            }
            List<Bitmap> listImage = new List<Bitmap>();
            foreach (Aspose.Cells.Worksheet sheet in wb.Worksheets)
            {
                Aspose.Cells.Rendering.ImageOrPrintOptions imgOptions = new Aspose.Cells.Rendering.ImageOrPrintOptions();

                ImageFormat imageFormat = ImageFormat.Jpeg;
                imgOptions.ImageFormat = imageFormat;
                imgOptions.OnePagePerSheet = true;
                imgOptions.PrintingPage = Aspose.Cells.PrintingPageType.IgnoreBlank;

                int index = sheet.Index + 1;
                SheetRender sr = new SheetRender(sheet, imgOptions);

                Bitmap bitmap = sr.ToImage(0);
                if (bitmap != null)
                {
                    listImage.Add(bitmap);
                }
            }
            highHeightUnit = MergerImageAndCut(listImage, root, true);

            foreach (KeyValuePair<string, Bitmap> bitmaps in listBitmaps)
            {
                Bitmap bitmap = bitmaps.Value;
                if (bitmap != null)
                {
                    ImageFormat imageFormat = ImageFormat.Jpeg;
                    bitmap.Save(bitmaps.Key, imageFormat);
                }
            }
            return highHeightUnit;
        }
        #endregion

        #region LeafletPdf2JPG
        public enum Definition
        {
            One = 1, Two = 2, Three = 3, Four = 4, Five = 5, Six = 6, Seven = 7, Eight = 8, Nine = 9, Ten = 10
        }
        public static int LeafletPdf2JPG(string pdfPath, string fileID, string taskID)
        {
            //Aspose.Pdf.Document pdf = new Aspose.Pdf.Document(pdfPath);
            curIndex = 1;
            int highHeightUnit = 0;
            string root = Path.Combine(OfficeHelper.GetFolderPath(taskID, "Office"), fileID);
            imgHeight = 0; listBitmaps = new Dictionary<string, Bitmap>();
            if (!Directory.Exists(@root))
            {
                Directory.CreateDirectory(@root);
            }

            /*O2S.Components.PDFRender4NET.dll方式*/
            PDFFile pdfFile = PDFFile.Open(pdfPath);
            int pageCount = pdfFile.PageCount;
            List<Bitmap> listImage = new List<Bitmap>();
            for (int i = 1; i <= pageCount; i++)
            {
                Bitmap bitmap = pdfFile.GetPageImage(i - 1, 56 * (int)Definition.Two);
                if (bitmap != null)
                {
                    listImage.Add(bitmap);
                }
            }
            highHeightUnit = MergerImageAndCut(listImage, root, true);

            /*Aspose.Pdf方式,比较慢*/
            //for (int pageCount = 1; pageCount <= pdf.Pages.Count; pageCount++)
            //{
            //    string fileName = root + "\\" + pageCount + ".jpg";
            //    using (FileStream imageStream = new FileStream(fileName, FileMode.Create))
            //    {
            //        Aspose.Pdf.Devices.Resolution resolution = new Aspose.Pdf.Devices.Resolution(300);
            //        Aspose.Pdf.Devices.PngDevice pngDevice = new Aspose.Pdf.Devices.PngDevice(resolution);
            //        pngDevice.Process(pdf.Pages[pageCount], imageStream);


            //        Image image = Image.FromStream(imageStream);
            //        imageStream.Close();
            //        if (image != null)
            //            ImgCutSave(image, root, pageCount); //代码截图
            //    }
            //    if (File.Exists(fileName))
            //        File.Delete(fileName);
            //}

            foreach (KeyValuePair<string, Bitmap> bitmaps in listBitmaps)
            {
                Bitmap bitmap = bitmaps.Value;
                if (bitmap != null)
                {
                    ImageFormat imageFormat = ImageFormat.Jpeg;
                    bitmap.Save(bitmaps.Key, imageFormat);
                }
            }
            return highHeightUnit;
        }
        #endregion

        #region LeafletImage2JPG
        public static int LeafletImage2JPG(string imagePath, string fileID, string taskID)
        {
            int highHeightUnit = 0;
            Bitmap fromImage = (Bitmap)Image.FromFile(imagePath);
            imgHeight = 0; listBitmaps = new Dictionary<string, Bitmap>();
            string root = Path.Combine(OfficeHelper.GetFolderPath(taskID, "Office"), fileID);
            if (!Directory.Exists(@root))
            {
                Directory.CreateDirectory(@root);
            }
            List<Bitmap> listImage = new List<Bitmap>();
            if (fromImage != null)
            {
                listImage.Add(fromImage);
                highHeightUnit = MergerImageAndCut(listImage, root, true);

                foreach (KeyValuePair<string, Bitmap> bitmaps in listBitmaps)
                {
                    Bitmap bitmap = bitmaps.Value;
                    if (bitmap != null)
                    {
                        ImageFormat imageFormat = ImageFormat.Jpeg;
                        bitmap.Save(bitmaps.Key, imageFormat);
                    }
                }
            }
            return highHeightUnit;
        }
        #endregion

        #region Txt2PDF

        public static bool Txt2PDF(string txtPath, string pdfPath)
        {
            var pdfBuffer = Txt2PDF(GetFileBuffer(txtPath));
            SaveFileBuffer(pdfBuffer, pdfPath);
            return true;
        }

        public static byte[] Txt2PDF(byte[] fileBuffer)
        {

            MemoryStream mystream = new MemoryStream(fileBuffer);

            var encoding = System.Text.Encoding.Default;
            if (fileBuffer.Length >= 2 && fileBuffer[0] >= 0xEF)
            {
                if (fileBuffer[0] == 0xEF && fileBuffer[1] == 0xBB)
                {
                    encoding = System.Text.Encoding.UTF8;
                }
                else if (fileBuffer[0] == 0xFE && fileBuffer[1] == 0xFF)
                {
                    encoding = System.Text.Encoding.BigEndianUnicode;
                }
                else if (fileBuffer[0] == 0xFF && fileBuffer[1] == 0xFE)
                {
                    encoding = System.Text.Encoding.Unicode;
                }
            }

            System.IO.TextReader tr = new StreamReader(mystream, encoding);
            var text = tr.ReadToEnd();

            var pdfStream = new MemoryStream();
            var doc = new Aspose.Words.Document();
            Aspose.Words.DocumentBuilder builder = new Aspose.Words.DocumentBuilder(doc);
            Aspose.Words.Font font = builder.Font;
            font.Name = "宋体";
            builder.Write(text);
            // 转PDF
            doc.Save(pdfStream, Aspose.Words.SaveFormat.Pdf);
            var buffer = pdfStream.ToArray();
            pdfStream.Close();

            return buffer;
        }

        #endregion

        #region Img2PDF

        public static bool Img2PDF(string imgPath, string pdfPath)
        {
            var pdfBuffer = Img2PDF(GetFileBuffer(imgPath));
            SaveFileBuffer(pdfBuffer, pdfPath);
            return true;
        }

        public static byte[] Img2PDF(byte[] fileBuffer)
        {

            Aspose.Pdf.Document doc = new Aspose.Pdf.Document();
            // Add a page to pages collection of document
            Aspose.Pdf.Page page = doc.Pages.Add();
            MemoryStream mystream = new MemoryStream(fileBuffer);
            // Instantiate BitMap object with loaded image stream
            Bitmap b = new Bitmap(mystream);

            // Set margins so image will fit, etc.
            page.PageInfo.Margin.Bottom = 0;
            page.PageInfo.Margin.Top = 0;
            page.PageInfo.Margin.Left = 0;
            page.PageInfo.Margin.Right = 0;
            page.SetPageSize(b.Width, b.Height);

            //page.CropBox = new Aspose.Pdf.Rectangle(0, 0, b.Width, b.Height);

            page.Resources.Images.Add(mystream);

            // Using GSave operator: this operator saves current graphics state
            page.Contents.Add(new Aspose.Pdf.Operator.GSave());
            // Create Rectangle and Matrix objects
            Aspose.Pdf.Rectangle rectangle = new Aspose.Pdf.Rectangle(0, 0, b.Width, b.Height);
            Aspose.Pdf.DOM.Matrix matrix = new Aspose.Pdf.DOM.Matrix(new double[] { rectangle.URX - rectangle.LLX, 0, 0, rectangle.URY - rectangle.LLY, rectangle.LLX, rectangle.LLY });

            // Using ConcatenateMatrix (concatenate matrix) operator: defines how image must be placed
            page.Contents.Add(new Aspose.Pdf.Operator.ConcatenateMatrix(matrix));
            Aspose.Pdf.XImage ximage = page.Resources.Images[page.Resources.Images.Count];

            // Using Do operator: this operator draws image
            page.Contents.Add(new Aspose.Pdf.Operator.Do(ximage.Name));

            // Using GRestore operator: this operator restores graphics state
            page.Contents.Add(new Aspose.Pdf.Operator.GRestore());

            var pdfStream = new MemoryStream();
            doc.Save(pdfStream);
            // Close memoryStream object
            var buffer = pdfStream.ToArray();
            pdfStream.Close();
            mystream.Close();

            return buffer;
        }

        #endregion

        #region Tif2PDF

        public static bool Tif2PDF(string imgPath, string pdfPath)
        {
            var pdfBuffer = Tif2PDF(GetFileBuffer(imgPath));
            SaveFileBuffer(pdfBuffer, pdfPath);
            return true;
        }

        public static byte[] Tif2PDF(byte[] fileBuffer)
        {

            Aspose.Pdf.Document doc = new Aspose.Pdf.Document();
            // Add a page to pages collection of document

            MemoryStream mystream = new MemoryStream(fileBuffer);

            Image tiffImage = Image.FromStream(mystream);
            int frameCount = tiffImage.GetFrameCount(FrameDimension.Page);
            MemoryStream[] images = new MemoryStream[frameCount];
            Guid objGuid = tiffImage.FrameDimensionsList[0];
            FrameDimension objDimension = new FrameDimension(objGuid);
            for (int i = 0; i < frameCount; i++)
            {
                tiffImage.SelectActiveFrame(objDimension, i);
                using (MemoryStream ms = new MemoryStream())
                {
                    tiffImage.Save(ms, ImageFormat.Bmp);
                    Aspose.Pdf.Page page = doc.Pages.Add();
                    Bitmap b = new Bitmap(ms);
                    page.Resources.Images.Add(ms);
                    page.PageInfo.Margin.Bottom = 0;
                    page.PageInfo.Margin.Top = 0;
                    page.PageInfo.Margin.Left = 0;
                    page.PageInfo.Margin.Right = 0;
                    page.SetPageSize(b.Width, b.Height);
                    
                    // Using GSave operator: this operator saves current graphics state
                    page.Contents.Add(new Aspose.Pdf.Operator.GSave());
                    // Create Rectangle and Matrix objects
                    Aspose.Pdf.Rectangle rectangle = new Aspose.Pdf.Rectangle(0, 0, b.Width, b.Height);
                    Aspose.Pdf.DOM.Matrix matrix = new Aspose.Pdf.DOM.Matrix(new double[] { rectangle.URX - rectangle.LLX, 0, 0, rectangle.URY - rectangle.LLY, rectangle.LLX, rectangle.LLY });

                    // Using ConcatenateMatrix (concatenate matrix) operator: defines how image must be placed
                    page.Contents.Add(new Aspose.Pdf.Operator.ConcatenateMatrix(matrix));
                    Aspose.Pdf.XImage ximage = page.Resources.Images[page.Resources.Images.Count];

                    // Using Do operator: this operator draws image
                    page.Contents.Add(new Aspose.Pdf.Operator.Do(ximage.Name));

                    // Using GRestore operator: this operator restores graphics state
                    page.Contents.Add(new Aspose.Pdf.Operator.GRestore());

                }
            }
            
           

            var pdfStream = new MemoryStream();
            doc.Save(pdfStream);
            // Close memoryStream object
            var buffer = pdfStream.ToArray();
            pdfStream.Close();
            mystream.Close();

            return buffer;
        }
        
        #endregion

        #region PDF2SWF
        /// <summary>
        /// 转换所有的页，图片质量80%
        /// </summary>
        /// <param name="pdfPath">PDF文件地址</param>
        /// <param name="swfPath">生成后的SWF文件地址</param>
        public static bool PDF2SWF(string pdfPath, string swfPath)
        {
            return PDF2SWF(pdfPath, swfPath, 1, GetPageCount(pdfPath), 80);
        }

        /// <summary>
        /// 转换前N页，图片质量80%
        /// </summary>
        /// <param name="pdfPath">PDF文件地址</param>
        /// <param name="swfPath">生成后的SWF文件地址</param>
        /// <param name="page">页数</param>
        public static bool PDF2SWF(string pdfPath, string swfPath, int page)
        {
            return PDF2SWF(pdfPath, swfPath, 1, page, 80);
        }

        /// <summary>
        /// PDF格式转为SWF
        /// </summary>
        /// <param name="pdfPath">PDF文件地址</param>
        /// <param name="swfPath">生成后的SWF文件地址</param>
        /// <param name="beginpage">转换开始页</param>
        /// <param name="endpage">转换结束页</param>
        /// <param name="isSplit">是否分页生成</param>
        private static bool PDF2SWF(string pdfPath, string swfPath, int beginpage, int endpage, int photoQuality)
        {
            string exe = AppDomain.CurrentDomain.BaseDirectory.Trim('\\') + "\\pdf2swf.exe";
            if (!System.IO.File.Exists(exe) || !System.IO.File.Exists(pdfPath) || System.IO.File.Exists(swfPath))
                return false;
            StringBuilder sb = new StringBuilder();
            sb.Append(" \"" + pdfPath + "\"");

            sb.Append(" -o \"" + swfPath + "\"");
            sb.Append(" -f -T 9 -s poly2bitmap  ");
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
                //aspose有性能问题
                //var converter = new Aspose.Pdf.Facades.PdfConverter();
                //converter.BindPdf(new Aspose.Pdf.Document(new MemoryStream(fileBuffer)));
                //converter.DoConvert();
                //converter.GetNextImage(snapStream, new Aspose.Pdf.PageSize(200, 150), System.Drawing.Imaging.ImageFormat.Jpeg,30);
                
                //换用ghostscript
                //GhostscriptRasterizer rasterizer = new GhostscriptRasterizer();
                //rasterizer.Open(new MemoryStream(fileBuffer));
                //Image page = rasterizer.GetPage(96, 96, 1);
                //page.Save(snapStream, ImageFormat.Jpeg);

                PDFFile pdfFile = PDFFile.Open(new MemoryStream(fileBuffer));
                Bitmap bitmap = pdfFile.GetPageImage(0, 56 * (int)Definition.Two);
                bitmap.Save(snapStream, ImageFormat.Jpeg);

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

        #region leaflet私有方法

        /// <summary> 
        /// 剪裁 -- 用GDI+ 
        /// </summary> 
        /// <param name="b">原始Bitmap</param> 
        /// <param name="StartX">开始坐标X</param> 
        /// <param name="StartY">开始坐标Y</param> 
        /// <param name="iWidth">宽度</param> 
        /// <param name="iHeight">高度</param> 
        /// <returns>剪裁后的Bitmap</returns> 
        private static Bitmap CutImage(Image b, int StartX, int StartY, int iWidth, int iHeight)
        {
            if (b == null)
            {
                return null;
            }

            try
            {
                Bitmap bmpOut = new Bitmap(iWidth, iHeight);
                Graphics g = Graphics.FromImage(bmpOut);
                g.DrawImage(b, new Rectangle(0, 0, iWidth, iHeight), new Rectangle(StartX, StartY, iWidth, iHeight), GraphicsUnit.Pixel);
                g.Dispose();
                return bmpOut;
            }
            catch
            {
                return null;
            }
        }

        private static int imgHeight = 0;
        private static Dictionary<string, Bitmap> listBitmaps = new Dictionary<string, Bitmap>();
        private static int imgMaxHeight = 28000; //拼接图片的高度
        private static int curIndex = 1; //当前Y坐标

        //
        //代码截图
        //srcImage 图片
        //fileRoot 根目录
        //index 当前图片序号
        //isWidthHeightEqual 是否宽度与高度一样的截图
        //
        private static int ImgCutToDictionary(Image srcImage, string fileRoot, int index, bool isWidthHeightEqual = true)
        {
            ImageFormat imageFormat = ImageFormat.Jpeg; //图片格式

            int max = 300; //图片大小写定义
            int height = srcImage.Height; //原图片高度
            int width = srcImage.Width; //原图片宽度
            int highHeightUnit = 0; //切的高度
            if (isWidthHeightEqual)
            {
                if (width > max)
                {
                    for (int i = 1; i <= (width / max) + 1; i++)
                    {
                        int cutWidth = i * max < width ? max : width - ((i - 1) * max);//截取的图片宽度
                        if (height > max) //原图片高度大于自定的图片大小截图
                        {
                            int startX = (i - 1) * max;//截图的开始坐标
                            for (int j = 1; j <= (height / max) + 1; j++) //根据图片高度除上定义的大小+1得到高度个数
                            {
                                int cutHeight = j * max < height ? max : height - ((j - 1) * max);//截取的图片高度
                                int startY = (j - 1) * max;
                                if (cutHeight > 0)
                                {
                                    Bitmap cutedImage = CutImage(srcImage, startX, startY, cutWidth, cutHeight);//截取图片
                                    int x = j - 1;
                                    int y = i - 1;
                                    if (imgHeight != 0)
                                    {

                                        x = (imgHeight % max == 0 ? imgHeight / max : (imgHeight / max + 1)) + x;//得到截取后的Y坐标序号
                                    }
                                    string key = fileRoot + "\\" + string.Format("18-{0}-{1}.jpg", y, x);
                                    highHeightUnit = highHeightUnit < x ? x : highHeightUnit;
                                    if (!listBitmaps.ContainsKey(key))
                                        listBitmaps.Add(key, cutedImage); //集合中不存在则添加用于保存图片
                                }
                            }
                        }
                        else
                        {
                            int startX = (i - 1) * max;
                            if (height > 0)
                            {
                                Bitmap cutedImage = CutImage(srcImage, startX, 0, cutWidth, height);
                                int x = i - 1;
                                if (imgHeight != 0)
                                {

                                    x = (imgHeight % max == 0 ? imgHeight / max : (imgHeight / max + 1)) + x;
                                }
                                string key = fileRoot + "\\" + string.Format("18-{0}-{1}.jpg", i - 1, x);
                                highHeightUnit = highHeightUnit < x ? x : highHeightUnit;
                                if (!listBitmaps.ContainsKey(key))
                                    listBitmaps.Add(key, cutedImage);
                            }
                        }
                    }
                }
                else
                {
                    for (int j = 1; j <= (height / max) + 1; j++)
                    {
                        int cutHeight = j * max < height ? max : height - ((j - 1) * max);
                        int startY = (j - 1) * max;
                        if (cutHeight > 0)
                        {
                            Bitmap cutedImage = CutImage(srcImage, 0, startY, width, cutHeight);
                            int x = j - 1;
                            if (imgHeight != 0)
                            {

                                x = (imgHeight % max == 0 ? imgHeight / max : (imgHeight / max + 1)) + x;
                            }
                            string key = fileRoot + "\\" + string.Format("18-0-{0}.jpg", x);
                            highHeightUnit = highHeightUnit < x ? x : highHeightUnit;
                            if (!listBitmaps.ContainsKey(key))
                                listBitmaps.Add(key, cutedImage);
                        }
                    }
                }
                if (index == 1)
                    imgHeight = height < max ? max : height;
                else
                    imgHeight = imgHeight + (height < max ? max : height);
            }
            else
            {
                int count = 0;
                if (height % max == 0)
                    count = height / max;
                else
                    count = height / max + 1;

                for (int j = 1; j <= count; j++)
                {
                    int cutHeight = j * max < height ? max : height - ((j - 1) * max);
                    int startY = (j - 1) * max;
                    Bitmap cutedImage = CutImage(srcImage, 0, startY, width, cutHeight);
                    string key = fileRoot + "\\" + string.Format("18-0-{0}.jpg", curIndex - 1);
                    highHeightUnit = highHeightUnit < (curIndex - 1) ? (curIndex - 1) : highHeightUnit;
                    if (!listBitmaps.ContainsKey(key))
                        listBitmaps.Add(key, cutedImage);
                    curIndex = curIndex + 1;
                }
            }
            return highHeightUnit;
        }

        private static int MergerImageAndCut(List<Bitmap> bitmaps, string root, bool isWidthHeightEqual)
        {
            int curHeight = 0, curIndex = 0, maxWidth = 0, count = 1;
            bool isConvert = false; int highHeightUnit = 0; //切的高度
            List<Bitmap> images = new List<Bitmap>();
            if (bitmaps.Count > 0)
            {
                foreach (var bitmap in bitmaps)
                {
                    if (bitmap.Width > maxWidth)
                        maxWidth = bitmap.Width;
                }
                foreach (var bitmap in bitmaps)
                {
                    List<Bitmap> bps = new List<Bitmap>();
                    bps.Add(bitmap);
                    if (bitmaps.Count != count)
                    {
                        Image fromImage = ImgRes.bottom;
                        bps.Add(CutImage(fromImage, 0, 0, maxWidth, fromImage.Height));
                    }
                    Bitmap image = MergerImg(bps);
                    if ((curHeight += image.Height) < imgMaxHeight)
                    {
                        images.Add(image);
                    }
                    else
                    {
                        Bitmap bp = MergerImg(images);
                        if (bp != null)
                        {
                            images = new List<Bitmap>();
                            highHeightUnit = ImgCutToDictionary((Image)bp, root, bitmaps.Count, isWidthHeightEqual); //代码截图
                        }
                        images.Add(image);
                        curHeight = 0;
                    }
                    curIndex = curIndex + 1;
                    if (!isConvert && bitmaps.Last() == bitmap)
                    {
                        Bitmap bp = MergerImg(images);
                        if (bp != null)
                        {
                            images = new List<Bitmap>();
                            highHeightUnit = ImgCutToDictionary((Image)bp, root, bitmaps.Count, isWidthHeightEqual); //代码截图
                        }
                    }
                    count = count + 1;
                }
            }
            return highHeightUnit;
        }

        private static Bitmap MergerImg(List<Bitmap> arrMap)
        {
            //获取图片数组长度
            int len = arrMap.Count;

            if (len == 0)
            {
                return null;
            }

            //求数组中最宽图片的宽度
            int maxWidth = 0;
            //计算图片的总数
            int sumHeight = 0;

            for (int i = 0; i < len; i++)
            {
                maxWidth = Math.Max(maxWidth, arrMap[i].Width);
                sumHeight += arrMap[i].Height;
            }
            //创建一个位图
            Bitmap bgImg = new Bitmap(maxWidth, sumHeight);
            Graphics g = Graphics.FromImage(bgImg);
            //清除画布，背景设置为白色
            g.Clear(Color.White);

            int gHeight = 0;
            for (int i = 0; i < len; i++)
            {
                gHeight = i == 0 ? 0 : gHeight = arrMap[i - 1].Height + gHeight;
                g.DrawImage(arrMap[i], 0, gHeight, arrMap[i].Width, arrMap[i].Height);
            }
            g.Dispose();
            return bgImg;
        }

        #endregion
    }
}
