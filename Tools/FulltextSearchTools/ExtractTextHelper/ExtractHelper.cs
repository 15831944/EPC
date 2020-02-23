using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtractTextHelper
{
    public class ExtractHelper
    {
        #region 文字提取
        public static string GetWordText(string filepath)
        {
            Aspose.Words.Document doc = new Aspose.Words.Document(filepath);
            return GetWordText(doc);
        }
        public static string GetWordText(byte[] buffer)
        {
            var ms = new System.IO.MemoryStream(buffer);
            Aspose.Words.Document doc = new Aspose.Words.Document(ms);
            return GetWordText(doc);
        }
        private static string GetWordText(Aspose.Words.Document doc)
        {
            return doc.GetText();
        }

        public static string GetExcelText(string filepath)
        {
            Aspose.Cells.Workbook workbook = new Aspose.Cells.Workbook(filepath);
            return GetExcelText(workbook);
        }
        public static string GetExcelText(byte[] buffer)
        {
            var ms = new System.IO.MemoryStream(buffer);
            Aspose.Cells.Workbook workbook = new Aspose.Cells.Workbook(ms);
            return GetExcelText(workbook);
        }
        private static string GetExcelText(Aspose.Cells.Workbook workbook)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < workbook.Worksheets.Count; i++)
            {
                var sheet = workbook.Worksheets[i];
                var cells = sheet.Cells;
                if (cells.MaxDataRow < 0 || cells.MaxColumn < 0)
                    continue;
                var dt = cells.ExportDataTableAsString(0, 0, cells.MaxDataRow + 1, cells.MaxColumn + 1, true);
                for (int k = 0; k < dt.Rows.Count; k++)
                {
                    var _rowsb = new StringBuilder();
                    for (int h = 0; h < dt.Columns.Count; h++)
                    {
                        var str = dt.Rows[k][h].ToString();
                        if (!string.IsNullOrEmpty(str))
                            _rowsb.Append(str + "\t");
                    }
                    if (_rowsb.Length > 0)
                        sb.AppendLine(_rowsb.ToString());
                }
            }
            return sb.ToString();
        }

        public static string GetPdfText_Aspose(string filepath)
        {
            Aspose.Pdf.Document pdf = new Aspose.Pdf.Document(filepath);
            return GetPdfText_Aspose(pdf);
        }
        public static string GetPdfText_Aspose(byte[] buffer)
        {
            var ms = new System.IO.MemoryStream(buffer);
            Aspose.Pdf.Document pdf = new Aspose.Pdf.Document(ms);
            return GetPdfText_Aspose(pdf);
        }
        private static string GetPdfText_Aspose(Aspose.Pdf.Document pdf)
        {
            System.Text.StringBuilder builder = new System.Text.StringBuilder();
            // String to hold extracted text
            string extractedText = "";

            foreach (Aspose.Pdf.Page pdfPage in pdf.Pages)
            {
                using (System.IO.MemoryStream textStream = new System.IO.MemoryStream())
                {
                    // Create text device
                    Aspose.Pdf.Devices.TextDevice textDevice = new Aspose.Pdf.Devices.TextDevice();

                    // Set text extraction options - set text extraction mode (Raw or Pure)
                    Aspose.Pdf.Text.TextOptions.TextExtractionOptions textExtOptions = new
                    Aspose.Pdf.Text.TextOptions.TextExtractionOptions(Aspose.Pdf.Text.TextOptions.TextExtractionOptions.TextFormattingMode.Pure);
                    textDevice.ExtractionOptions = textExtOptions;

                    // Convert a particular page and save text to the stream
                    textDevice.Process(pdfPage, textStream);
                    // Convert a particular page and save text to the stream
                    //textDevice.Process(pdf.Pages[1], textStream);

                    // Close memory stream
                    textStream.Close();

                    // Get text from memory stream
                    extractedText = Encoding.Unicode.GetString(textStream.ToArray());
                    //extractedText = encoding.GetString(textStream.ToArray());
                }
                builder.Append(extractedText);
            }
            return builder.ToString();
        }
        public static string GetPdfText_Itextsharp(string filepath)
        {
            var reader = new iTextSharp.text.pdf.PdfReader(filepath);
            return GetPdfText_Itextsharp(reader);
        }
        public static string GetPdfText_Itextsharp(byte[] buffer)
        {
            var reader = new iTextSharp.text.pdf.PdfReader(buffer);
            return GetPdfText_Itextsharp(reader);
        }
        private static string GetPdfText_Itextsharp(iTextSharp.text.pdf.PdfReader reader)
        {
            System.Text.StringBuilder builder = new System.Text.StringBuilder();
            int n = reader.NumberOfPages;
            for (int i = 1; i <= n; i++)
            {
                string text = iTextSharp.text.pdf.parser.PdfTextExtractor.GetTextFromPage(reader, i);
                //text = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(text)));
                builder.Append(text);
            }
            try { reader.Close(); }
            catch { }
            return builder.ToString();
        }

        public static string GetTxtText(string filepath)
        {
            return System.IO.File.ReadAllText(filepath, GetFileEncodeType(filepath));
        }
        public static string GetTxtText(byte[] buffer)
        {
            var ms = new System.IO.MemoryStream(buffer);
            System.IO.BinaryReader br = new System.IO.BinaryReader(ms);
            var text = GetFileEncodeType(br).GetString(buffer);
            return text;
        }

        #endregion

        #region 获得文件编码
        private static System.Text.Encoding GetFileEncodeType(string filename)
        {
            System.IO.FileStream fs = new System.IO.FileStream(filename, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            System.IO.BinaryReader br = new System.IO.BinaryReader(fs);
            return GetFileEncodeType(br);
        }
        private static System.Text.Encoding GetFileEncodeType(System.IO.BinaryReader br)
        {
            Byte[] buffer = br.ReadBytes(2);
            if (buffer[0] >= 0xEF)
            {
                if (buffer[0] == 0xEF && buffer[1] == 0xBB)
                {
                    return System.Text.Encoding.UTF8;
                }
                else if (buffer[0] == 0xFE && buffer[1] == 0xFF)
                {
                    return System.Text.Encoding.BigEndianUnicode;
                }
                else if (buffer[0] == 0xFF && buffer[1] == 0xFE)
                {
                    return System.Text.Encoding.Unicode;
                }
                else
                {
                    return System.Text.Encoding.Default;
                }
            }
            else
            {
                return System.Text.Encoding.Default;
            }

        }
        #endregion
    }
}
