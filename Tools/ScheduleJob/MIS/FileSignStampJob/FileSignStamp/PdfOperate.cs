using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;

namespace FileSignStamp
{
    /// <summary>
    /// pdf图片
    /// </summary>
    public class PdfImage
    {
        #region PdfImage属性
        /// <summary>
        /// 图片本地路径
        /// </summary>
        public string ImagePath { get; set; }
        /// <summary>
        /// 图片域宽(mm)
        /// </summary>
        public float FitWidth { get; set; }
        /// <summary>
        /// 图片域高(mm)
        /// </summary>
        public float FitHeight { get; set; }
        /// <summary>
        /// 绝对X坐标(mm)
        /// </summary>
        public float AbsoluteX { get; set; }
        /// <summary>
        /// 绝对Y坐标(mm)
        /// </summary>
        public float AbsoluteY { get; set; }
        /// <summary>
        /// 绝对角度(°)
        /// </summary>
        public float Rotation { get; set; }
        /// <summary>
        /// 页码
        /// </summary>
        public int PageNum { get; set; }
        #endregion

        #region  PdfImage构造
        public PdfImage() { }
        /// <summary>
        /// PdfImage构造
        /// </summary>
        ///  <param name="imagePath">图片路径</param>
        /// <param name="fitWidth">宽度</param>
        /// <param name="fitHeight">长度</param>
        /// <param name="absolutX">X坐标</param>
        /// <param name="absoluteY">Y坐标</param>
        /// <param name="rotation">角度</param>
        public PdfImage(string imagePath, float fitWidth, float fitHeight,
            float absoluteX, float absoluteY, float rotation, int pageNum)
        {
            this.ImagePath = imagePath;
            this.FitWidth = fitWidth;
            this.FitHeight = fitHeight;
            this.AbsoluteX = absoluteX;
            this.AbsoluteY = absoluteY;
            this.Rotation = rotation;
            this.PageNum = pageNum;
        }
        #endregion
    }

    public class PdfAuthImg : PdfImage
    {
        public string CertName { get; set; }

        public PdfAuthImg() { }
        public PdfAuthImg(PdfImage pdfImg)
        {
            this.ImagePath = pdfImg.ImagePath;
            this.FitWidth = pdfImg.FitWidth;
            this.FitHeight = pdfImg.FitHeight;
            this.AbsoluteX = pdfImg.AbsoluteX;
            this.AbsoluteY = pdfImg.AbsoluteY;
            this.Rotation = pdfImg.Rotation;
            this.PageNum = pdfImg.PageNum;
        }
    }

    /// <summary>
    /// pdf文字
    /// </summary>
    public class PdfText
    {
        #region PdfText属性
        /// <summary>
        /// 内容
        /// </summary>
        public string TxtContent { get; set; }
        /// <summary>
        /// 字体名称
        /// </summary>
        public string FontName { get; set; }
        /// <summary>
        /// 字体大小
        /// </summary>
        public float Size { get; set; }
        /// <summary>
        /// 对齐方式
        /// </summary>
        public int Alignment { get; set; }
        /// <summary>
        /// 绝对X坐标
        /// </summary>
        public float AbsoluteX { get; set; }
        /// <summary>
        /// 绝对Y坐标
        /// </summary>
        public float AbsoluteY { get; set; }
        /// <summary>
        /// 绝对角度
        /// </summary>
        public float Rotation { get; set; }

        /// <summary>
        /// 文字域宽
        /// </summary>
        public float FitWidth { get; set; }
        /// <summary>
        /// 文字域高
        /// </summary>
        public float FitHeight { get; set; }
        /// <summary>
        /// 页码
        /// </summary>
        public int PageNum { get; set; }

        #endregion

        #region  PdfText构造
        /// <summary>
        /// PdfText构造
        /// </summary>
        ///  <param name="txtContent">内容</param>
        /// <param name="fontName">字体</param>
        /// <param name="alignment">对齐方式</param>
        /// <param name="height">对齐方式</param>
        /// <param name="absolutX">X坐标</param>
        /// <param name="absoluteY">Y坐标</param>
        /// <param name="rotation">角度</param>
        public PdfText(string txtContent, string fontName, float size, int alignment,
            float absolutX, float absoluteY, float rotation, int pageNum)
        {
            this.TxtContent = txtContent;
            this.FontName = fontName;
            this.Size = size;
            this.Alignment = alignment;
            this.AbsoluteX = absolutX;
            this.AbsoluteY = absoluteY;
            this.Alignment = alignment;
            this.Rotation = rotation;
            this.PageNum = pageNum;
        }
        #endregion
    }

    public class PdfOperate
    {
        //系统字体目录
        private static string _SystemFontsFolder;
        public static string SystemFontsFolder
        {
            get
            {
                if (string.IsNullOrEmpty(_SystemFontsFolder))
                {
                    var shellFolders = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\Shell Folders");
                    _SystemFontsFolder = shellFolders.GetValue("Fonts") as string;
                }
                return _SystemFontsFolder;
            }
        }

        private float m_PaperWidth = 0;
        private float m_PaperHeight = 0;
        public PdfOperate(float paperWidth, float paperHeight)
        {
            m_PaperWidth = paperWidth;
            m_PaperHeight = paperHeight;
        }

        #region 指定pdf模板文件添加图片以及文字
        /// <summary>
        /// 指定pdf模板文件添加图片以及文字
        /// </summary>
        /// <param name="tempFilePath">源文件路径</param>
        /// <param name="createdPdfPath">插入图片后文件路径</param>
        /// <param name="pdfImages">图片对象集合</param>
        public void PdfSign(string tempFilePath, string createdPdfPath, List<PdfImage> pdfImages, List<PdfText> pdfTexts)
        {
            PdfReader pdfReader = null;
            PdfStamper pdfStamper = null;
            try
            {
                pdfReader = new PdfReader(tempFilePath);
                pdfStamper = new PdfStamper(pdfReader, new FileStream(createdPdfPath, FileMode.OpenOrCreate));

                float width = pdfReader.GetPageSizeWithRotation(1).Width;
                float height = pdfReader.GetPageSizeWithRotation(1).Height;
                if (m_PaperWidth == 0 || m_PaperHeight == 0)
                {
                    m_PaperWidth = (float)(width / 72 * 25.4);
                    m_PaperHeight = (float)(height / 72 * 25.4);
                }
                float cmpOne = width, cmpTwo = height, cmpThr = m_PaperWidth, cmpFour = m_PaperHeight;
                if (height > width)
                {
                    cmpOne = height;
                    cmpTwo = width;
                }
                if (m_PaperHeight > m_PaperWidth)
                {
                    cmpThr = m_PaperHeight;
                    cmpFour = m_PaperWidth;
                }
                float transScalH = cmpOne / cmpThr;
                float transScalV = cmpTwo / cmpFour;
                foreach (var pdfImage in pdfImages)
                {
                    Image img = Image.GetInstance(pdfImage.ImagePath);
                    if (img != null)
                    {
                        try
                        {
                            PdfContentByte pdfContentByte = pdfStamper.GetOverContent(pdfImage.PageNum);
                            img.ScaleToFit(pdfImage.FitWidth * transScalH, pdfImage.FitHeight * transScalV);
                            img.SetAbsolutePosition((pdfImage.AbsoluteX) * transScalH, (pdfImage.AbsoluteY) * transScalV);
                            img.Rotation = (float)(pdfImage.Rotation / 180 * Math.PI);
                            pdfContentByte.AddImage(img);
                        }
                        catch (Exception)
                        {
                            continue;
                        }
                    }
                }
                foreach (var pdfText in pdfTexts)
                {
                    string fontPath = SystemFontsFolder + "\\" + pdfText.FontName;
                    BaseFont baseFont = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);//获取系统的字体
                    iTextSharp.text.Font font = new iTextSharp.text.Font(baseFont, pdfText.Size);
                    Phrase phrase = new Phrase(pdfText.TxtContent, font);
                    float recWidth = ColumnText.GetWidth(phrase);

                    float centX = pdfText.AbsoluteX * transScalH;
                    float centY = pdfText.AbsoluteY * transScalV;
                    try
                    {
                        PdfContentByte pdfContentByte = pdfStamper.GetOverContent(pdfText.PageNum);
                        ColumnText.ShowTextAligned(pdfContentByte, pdfText.Alignment, phrase, centX, centY, pdfText.Rotation);
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }
                pdfStamper.FormFlattening = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (pdfStamper != null)
                {
                    pdfStamper.Close();
                }

                if (pdfReader != null)
                {
                    pdfReader.Close();
                }

                pdfStamper = null;
                pdfReader = null;
            }
        }
        #endregion
    }

}