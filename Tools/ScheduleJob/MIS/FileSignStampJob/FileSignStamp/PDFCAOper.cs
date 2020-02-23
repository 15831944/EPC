using System;
using System.Text;
using System.IO;
using System.Net;
using Common.Logic.DTO;

using Org.BouncyCastle.X509;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.security;
using System.Security.Cryptography.X509Certificates;
using iTextSharp.text;
using Common.Logic;
using System.Collections.Generic;

namespace FileSignStamp
{
    class PDFCAOper
    {
        private bool noTsa = false;
        private ITSAClient _tsaClient;
        private ITSAClient TsaClient
        {
            get
            {
                if (noTsa)
                    return null;
                if (_tsaClient == null)
                {
                    if (string.IsNullOrEmpty(AppConfig.TSA_URL))
                    {
                        noTsa = true;
                        return null;
                    }
                    _tsaClient = new TSAClientBouncyCastle(AppConfig.TSA_URL);
                }
                return _tsaClient;
            }
        }
        //源PDF
        public string BasePdf = string.Empty;
        //盖章后的PDF
        public string StampPdf = string.Empty;

        public int Index;

        private float m_PaperWidth = 0;
        private float m_PaperHeight = 0;
        private float transScalH = 0;
        private float transScalV = 0;

        public PDFCAOper(float paperWidth, float paperHeight)
        {
            this.m_PaperWidth = paperWidth;
            this.m_PaperHeight = paperHeight;
        }
        public void CASignProcess(PdfAuthImg authImg)
        {
            PdfReader pdfReader = null;
            PdfStamper pdfStamper = null;
            FileStream signedPdf = null;
            try
            {
                if (string.IsNullOrEmpty(BasePdf)) throw new Exception("PDF源路径为空");
                if (string.IsNullOrEmpty(StampPdf)) throw new Exception("PDF输出路径为空");

                pdfReader = new PdfReader(BasePdf);
                signedPdf = new FileStream(StampPdf, FileMode.OpenOrCreate);
                pdfStamper = PdfStamper.CreateSignature(pdfReader, signedPdf, '\0', null, true);

                if (transScalH == 0 || transScalV == 0)
                {
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
                    transScalH = cmpOne / cmpThr;
                    transScalV = cmpTwo / cmpFour;
                }


                X509Certificate2 cert = null;
                X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
                store.Open(OpenFlags.ReadOnly);
                foreach (X509Certificate2 myX509Certificate2 in store.Certificates)
                {
                    if (myX509Certificate2.Subject.Contains(authImg.CertName))
                    {
                        cert = myX509Certificate2;
                    }
                }
                store.Close();
                if (cert == null) return;

                float m_llx = authImg.AbsoluteX;
                float m_lly = authImg.AbsoluteY;
                float m_urx = m_llx + authImg.FitWidth;
                float m_ury = m_lly + authImg.FitHeight;

                if (authImg.Rotation == 90 || authImg.Rotation == 270 || authImg.Rotation == -90)
                {
                    m_urx = m_llx + authImg.FitHeight;
                    m_ury = m_lly + authImg.FitWidth;
                }
                var stampArea = new Rectangle(m_llx * transScalH, m_lly * transScalV, m_urx * transScalH, m_ury * transScalV);

                var img = Image.GetInstance(authImg.ImagePath);
                img.Rotation = (float)(authImg.Rotation / 180 * Math.PI);

                X509CertificateParser cp = new X509CertificateParser();
                Org.BouncyCastle.X509.X509Certificate[] chain = new Org.BouncyCastle.X509.X509Certificate[] { cp.ReadCertificate(cert.RawData) };
                IExternalSignature externalSignature = new X509Certificate2Signature(cert, "SHA-1");

                PdfSignatureAppearance signatureAppearance = pdfStamper.SignatureAppearance;
                signatureAppearance.SignatureGraphic = img;
                signatureAppearance.SetVisibleSignature(stampArea, authImg.PageNum, "Signature" + Index.ToString());
                signatureAppearance.SignatureRenderingMode = PdfSignatureAppearance.RenderingMode.GRAPHIC;
                signatureAppearance.SignDate = System.DateTime.Now;
                signatureAppearance.Reason = "12345";
                MakeSignature.SignDetached(signatureAppearance, externalSignature, chain, null, null, TsaClient, 0, CryptoStandard.CMS);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (pdfStamper != null) pdfStamper.Close();
                if (pdfReader != null) pdfReader.Close();
                if (signedPdf != null) signedPdf.Close();

                pdfStamper = null;
                pdfReader = null;
                signedPdf = null;
            }
        }
    }
}
