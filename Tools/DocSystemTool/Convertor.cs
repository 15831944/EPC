using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Microsoft.Office.Interop.Word;
using Microsoft.Office.Interop.Excel;
using System.IO;

namespace DocSystemTool
{
    public class Convertor
    {
        public static string WordToPDF(string wordFilePath,string pdfFileFullPath,bool userApose=false)
        {
            if (userApose)
            {
                var doc = new Aspose.Words.Document(wordFilePath);
                doc.Save(pdfFileFullPath, Aspose.Words.SaveFormat.Pdf);                
            }
            else
            {
                var WordApp = new Microsoft.Office.Interop.Word.Application();
                try
                {
                    var doc = WordApp.Documents.Open(wordFilePath);
                    doc.Activate();
                    object pdfFomat = WdSaveFormat.wdFormatPDF;
                    doc.SaveAs(pdfFileFullPath, pdfFomat);
                    foreach (Document item in WordApp.Documents)
                        item.Close();
                }
                catch (Exception exp)
                {
                }
                finally
                {
                    if (WordApp != null)
                    {
                        WordApp.Quit();
                        WordApp = null;
                    }
                }
            }
            return pdfFileFullPath;
        }

        public static string ExcelToPDF(string excelFilePath, string pdfFileFullPath, bool userApose = false)
        {
            if (userApose)
            {
                var xbook = new Aspose.Cells.Workbook(excelFilePath);
                xbook.Save(pdfFileFullPath, Aspose.Cells.SaveFormat.Pdf);
            }
            else
            {
                var ExcelApp = new Microsoft.Office.Interop.Excel.Application();
                try
                {
                    Microsoft.Office.Interop.Excel.Workbook xBook = ExcelApp.Workbooks.Open(excelFilePath);
                    xBook.ExportAsFixedFormat(XlFixedFormatType.xlTypePDF, pdfFileFullPath,
                        XlFixedFormatQuality.xlQualityStandard,
                        true, true, Missing.Value, Missing.Value, false, Missing.Value);

                    foreach (Workbook item in ExcelApp.Workbooks)
                        item.Close(false, Missing.Value, Missing.Value);
                }
                catch (Exception ex)
                {
                }
                finally
                {
                    if (ExcelApp != null)
                    {
                        ExcelApp.Quit();
                        ExcelApp = null;
                    }
                }
            }
            return pdfFileFullPath;
        }

        public static string PDFToBMP(string pdfFilePath,string bmpFilePath)
        {
            var stream = new FileStream(pdfFilePath, FileMode.Open);
            var pdfConvert = new Aspose.Pdf.Facades.PdfConverter();
            var pdfDoc = new Aspose.Pdf.Document(stream);
            pdfConvert.BindPdf(pdfDoc);
            pdfConvert.DoConvert();
            pdfConvert.GetNextImage(bmpFilePath, new Aspose.Pdf.PageSize(200, 150), System.Drawing.Imaging.ImageFormat.Bmp);
            pdfConvert.Close();
            pdfConvert.Dispose();
            stream.Flush();
            stream.Close();
            return bmpFilePath;
        }

        public static string PDFToSWF(string pdfFilePath, string swfFilePath)
        {
            var result = SWFConvert.PDF2SWF(pdfFilePath, swfFilePath);
            return swfFilePath;
        }
    }
}
