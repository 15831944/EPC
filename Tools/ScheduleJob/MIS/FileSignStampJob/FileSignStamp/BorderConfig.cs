using Common.Logic;
using System;
using System.Collections.Generic;
using System.Xml;


namespace FileSignStamp
{
    public enum BorderType
    {
        NoneBorderType = -1,
        HORIZONTAL,
        VERTICAL
    };

    public enum BorderSize
    {
        NoneBorderSize = -1,
        A0,
        A1,
        A2,
        A3,
        A4
    };

    public class BarcodeConfigInfo
    {
        public double Height;
        public double Angle;
        public double CorrectPosX;
        public double CorrectPosY;
    }

    public class QRCodeConfigInfo
    {
        public double Width;
        public double Height;
        public double Angle;
        public double CorrectPosX;
        public double CorrectPosY;
    }

    public class SignConfigInfo
    {
        public double Width;
        public double Height;
        public double TextHeight;
        public double Angle;
        public double CorrectPosX;
        public double CorrectPosY;
    }

    public class CoSignConfigInfo
    {
        public double Width;
        public double Height;
        public double TextHeight;
        public double Angle;
        public double CorrectPosX;
        public double CorrectPosY;
    }

    public class StampConfigInfo
    {
        public double Width;
        public double Height;
        public double Angle;
        public double CorrectPosX;
        public double CorrectPosY;
        public List<ChildeStampConfigInfo> ChildStampConfigInfos = new List<ChildeStampConfigInfo>();
    }

    public class ChildeStampConfigInfo
    {
        public double PointX;
        public double PointY;
        public double Width;
        public double Height;
        public double Angle;
    }

    public class BorderConfig
    {
        public BorderType m_BorderType;	                       
        public BorderSize m_BorderSize;
        public BarcodeConfigInfo m_BarcodeConfigInfo;
        public QRCodeConfigInfo m_QRCodeConfigInfo;
        public SignConfigInfo m_SignConfigInfo;
        public CoSignConfigInfo m_CoSignConfigInfo;
        public List<StampConfigInfo> m_StampConfigInfos = new List<StampConfigInfo>();

        public BorderConfig()
        {
            m_BorderType = BorderType.NoneBorderType;
            m_BorderSize = BorderSize.NoneBorderSize;
            m_BarcodeConfigInfo = new BarcodeConfigInfo();
            m_StampConfigInfos = new List<StampConfigInfo>();
        }
    }

    public class BorderConfigManager
    {
        private string m_XmlPath;
        private XmlDocument m_XmlDoc;
        private List<BorderConfig> m_lst_BorderConfig;

        public BorderConfigManager()
        {
            m_XmlDoc = null;
            string strCoTemplateLocal = AppConfig.DllPath.Replace("/", "\\");
            m_XmlPath = strCoTemplateLocal + "\\Resources\\borderconfig.xml";
        }

        public bool Run()
        {
            try
            {
                m_lst_BorderConfig = new List<BorderConfig>();
                if (m_XmlDoc == null)
                {
                    m_XmlDoc = new XmlDocument();
                }
                XmlDocument xmlDoc = new XmlDocument();
                XmlReaderSettings settings = new XmlReaderSettings();
                settings.IgnoreComments = true;
                XmlReader reader = XmlReader.Create(m_XmlPath, settings);
                xmlDoc.Load(reader);

                XmlNode root = xmlDoc.SelectSingleNode("border");
                if (root == null)
                {
                    reader.Close();
                    return false;
                }

                Read(root, BorderType.HORIZONTAL);
                Read(root, BorderType.VERTICAL);
                reader.Close();
                return true;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        private bool Read(XmlNode pParent, BorderType borderType)
        {
            string borderTypeStr = GetBorderTypeName(borderType);
            XmlNodeList xnl = pParent.ChildNodes;
            foreach (XmlNode xn in xnl)
            {
                if (xn.Name == borderTypeStr)
                {
                    Read(xn, BorderSize.A0, borderType);
                    Read(xn, BorderSize.A1, borderType);
                    Read(xn, BorderSize.A2, borderType);
                    Read(xn, BorderSize.A3, borderType);
                    Read(xn, BorderSize.A4, borderType);
                }
            }
            return true;
        }

        private bool Read(XmlNode pParent, BorderSize borderSize, BorderType borderType)
        {
            BorderConfig borderCofig = new BorderConfig();
            borderCofig.m_BorderType = borderType;
            borderCofig.m_BorderSize = borderSize;
            borderCofig.m_BarcodeConfigInfo = new BarcodeConfigInfo();
            borderCofig.m_QRCodeConfigInfo = new QRCodeConfigInfo();
            borderCofig.m_SignConfigInfo = new SignConfigInfo();
            borderCofig.m_CoSignConfigInfo = new CoSignConfigInfo();
            borderCofig.m_StampConfigInfos = new List<StampConfigInfo>();
            string borderSizeStr = GetBorderSizeName(borderSize);
            XmlNode size_xn = pParent.SelectSingleNode(borderSizeStr);
            XmlNodeList info_xns = size_xn.ChildNodes;
            foreach (XmlNode xn in info_xns)
            {
                XmlElement element = (XmlElement)xn;
                if (element.Name == "Barcode")
                {
                    borderCofig.m_BarcodeConfigInfo.Height = Convert.ToDouble(element.GetAttribute("Height"));
                    borderCofig.m_BarcodeConfigInfo.Angle = Convert.ToDouble(element.GetAttribute("Angle"));
                    borderCofig.m_BarcodeConfigInfo.CorrectPosX = Convert.ToDouble(element.GetAttribute("CorrectPosX"));
                    borderCofig.m_BarcodeConfigInfo.CorrectPosY = Convert.ToDouble(element.GetAttribute("CorrectPosY"));
                }

                if (element.Name == "QRcode")
                {
                    borderCofig.m_QRCodeConfigInfo.Width = Convert.ToDouble(element.GetAttribute("ExtWidth"));
                    borderCofig.m_QRCodeConfigInfo.Height = Convert.ToDouble(element.GetAttribute("ExtHeight"));
                    borderCofig.m_QRCodeConfigInfo.Angle = Convert.ToDouble(element.GetAttribute("Angle"));
                    borderCofig.m_QRCodeConfigInfo.CorrectPosX = Convert.ToDouble(element.GetAttribute("CorrectPosX"));
                    borderCofig.m_QRCodeConfigInfo.CorrectPosY = Convert.ToDouble(element.GetAttribute("CorrectPosY"));
                }

                if (element.Name == "Sign")
                {
                    borderCofig.m_SignConfigInfo.Width = Convert.ToDouble(element.GetAttribute("SignExtWidth"));
                    borderCofig.m_SignConfigInfo.Height = Convert.ToDouble(element.GetAttribute("SignExtHeight"));
                    borderCofig.m_SignConfigInfo.TextHeight = Convert.ToDouble(element.GetAttribute("Height"));
                    borderCofig.m_SignConfigInfo.Angle = Convert.ToDouble(element.GetAttribute("Angle"));
                    borderCofig.m_SignConfigInfo.CorrectPosX = Convert.ToDouble(element.GetAttribute("CorrectPosX"));
                    borderCofig.m_SignConfigInfo.CorrectPosY = Convert.ToDouble(element.GetAttribute("CorrectPosY"));
                }

                if (element.Name == "CoSign")
                {
                    borderCofig.m_CoSignConfigInfo.Width = Convert.ToDouble(element.GetAttribute("CoSignExtWidth"));
                    borderCofig.m_CoSignConfigInfo.Height = Convert.ToDouble(element.GetAttribute("CoSignExtHeight"));
                    borderCofig.m_CoSignConfigInfo.TextHeight = Convert.ToDouble(element.GetAttribute("Height"));
                    borderCofig.m_CoSignConfigInfo.Angle = Convert.ToDouble(element.GetAttribute("Angle"));
                    borderCofig.m_CoSignConfigInfo.CorrectPosX = Convert.ToDouble(element.GetAttribute("CorrectPosX"));
                    borderCofig.m_CoSignConfigInfo.CorrectPosY = Convert.ToDouble(element.GetAttribute("CorrectPosY"));
                }

                if (element.Name == "Stamp")
                {
                    StampConfigInfo stampInfo = new StampConfigInfo();
                    stampInfo.Width = Convert.ToDouble(element.GetAttribute("StampExtWidth"));
                    stampInfo.Height = Convert.ToDouble(element.GetAttribute("StampExtHeight"));
                    stampInfo.Angle = Convert.ToDouble(element.GetAttribute("Angle"));
                    stampInfo.CorrectPosX = Convert.ToDouble(element.GetAttribute("CorrectPosX"));
                    stampInfo.CorrectPosY = Convert.ToDouble(element.GetAttribute("CorrectPosY"));
                    XmlNodeList nodes = xn.ChildNodes;
                    foreach (XmlNode node in nodes)
                    {
                        element = (XmlElement)node;
                        ChildeStampConfigInfo childStampInfo = new ChildeStampConfigInfo();
                        childStampInfo.Width = Convert.ToDouble(element.GetAttribute("StampExtWidth"));
                        childStampInfo.Height = Convert.ToDouble(element.GetAttribute("StampExtHeight"));
                        childStampInfo.Angle = Convert.ToDouble(element.GetAttribute("Angle"));
                        childStampInfo.PointX = Convert.ToDouble(element.GetAttribute("RelativePosX"));
                        childStampInfo.PointY = Convert.ToDouble(element.GetAttribute("RelativePosY"));
                        stampInfo.ChildStampConfigInfos.Add(childStampInfo);
                    }
                    borderCofig.m_StampConfigInfos.Add(stampInfo);
                }
            }
            m_lst_BorderConfig.Add(borderCofig);
            return true;
        }

        private string GetBorderTypeName(BorderType borderType)
        {
            if (borderType == BorderType.HORIZONTAL)
            {
                return "Horizontal";
            }
            if (borderType == BorderType.VERTICAL)
            {
                return "Vertical";
            }
            return "";
        }

        private string GetBorderSizeName(BorderSize borderSize)
        {
            if (borderSize == BorderSize.A0)
            {
                return "A0";
            }
            if (borderSize == BorderSize.A1)
            {
                return "A1";
            }
            if (borderSize == BorderSize.A2)
            {
                return "A2";
            }
            if (borderSize == BorderSize.A3)
            {
                return "A3";
            }
            if (borderSize == BorderSize.A4)
            {
                return "A4";
            }
            return "";
        }

        public List<BorderConfig> GetConfig()
        {
            return m_lst_BorderConfig;
        }

        public BorderConfig GetBorderConfig(BorderType type,BorderSize size)
        {
            BorderConfig reVal = null;
            foreach (BorderConfig item in m_lst_BorderConfig)
            {
                if (item.m_BorderType == type && item.m_BorderSize == size)
                {
                    reVal = item;
                    break;
                }
            }
            return reVal;
        }

        public BorderConfig GetBorderConfig(string strType, string strSize)
        {
            strSize = strSize.Substring(0, 2);
            BorderType type = BorderType.HORIZONTAL;
            BorderSize size = BorderSize.A0;
            if (strType=="竖框")
            {
                type = BorderType.VERTICAL;
            }
            if (strSize.ToUpper() == "A0")
            {
                size = BorderSize.A0;
            }
            if (strSize.ToUpper() == "A1")
            {
                size = BorderSize.A1;
            }
            if (strSize.ToUpper() == "A2")
            {
                size = BorderSize.A2;
            }
            if (strSize.ToUpper() == "A3")
            {
                size = BorderSize.A3;
            }
            if (strSize.ToUpper() == "A4")
            {
                size = BorderSize.A4;
            }
            BorderConfig reVal = null;
            foreach (BorderConfig item in m_lst_BorderConfig)
            {
                if (item.m_BorderType == type && item.m_BorderSize == size)
                {
                    reVal = item;
                    break;
                }
            }
            return reVal;
        }
    }
}
