using Common.Logic;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace FileSignStamp
{
    public class AttburiteConfigInfo
    {
        public string name;
        public string label;
        public int count;
        public bool isNeedDate;
        public bool IsNeedMajor;
    }

    public class FrameDefaultAttManage
    {
        //属性对照表管理对象
        private static FrameDefaultAttManage _Instance;
        public static FrameDefaultAttManage Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new FrameDefaultAttManage();
                    if (!_Instance.InitInfo())
                    {
                        _Instance = null;
                    }
                }
                return _Instance;
            }
        }

        public List<AttburiteConfigInfo> m_Standard = new List<AttburiteConfigInfo>();
        public List<AttburiteConfigInfo> m_Sign = new List<AttburiteConfigInfo>();
        public List<AttburiteConfigInfo> m_Cosign = new List<AttburiteConfigInfo>();
        public List<AttburiteConfigInfo> m_Barcode = new List<AttburiteConfigInfo>();
        public List<AttburiteConfigInfo> m_QRcode = new List<AttburiteConfigInfo>();
        public List<AttburiteConfigInfo> m_Stamp = new List<AttburiteConfigInfo>();

        public bool InitInfo()
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                string filePath = AppConfig.DllPath + "/Resources/FrameDefaultAtt.xml";
                xmlDoc.Load(filePath);
                XmlNodeList xmlNodes = xmlDoc.GetElementsByTagName("Standard");
                foreach (XmlNode node in xmlNodes)
                {
                    foreach (XmlNode childNode in node.ChildNodes)
                    {
                        AttburiteConfigInfo info = new AttburiteConfigInfo();
                        XmlElement xe = childNode as XmlElement;
                        info.name = xe.GetAttribute("Name");
                        info.label = xe.GetAttribute("Label");
                        info.count = Convert.ToInt16(xe.GetAttribute("Count"));
                        info.isNeedDate = false;
                        info.IsNeedMajor = false;
                        m_Standard.Add(info);
                    }
                }
                xmlNodes = xmlDoc.GetElementsByTagName("Sign");
                foreach (XmlNode node in xmlNodes)
                {
                    foreach (XmlNode childNode in node.ChildNodes)
                    {
                        AttburiteConfigInfo info = new AttburiteConfigInfo();
                        XmlElement xe = childNode as XmlElement;
                        info.name = xe.GetAttribute("Name");
                        info.label = xe.GetAttribute("Label");
                        info.count = Convert.ToInt16(xe.GetAttribute("Count"));
                        info.isNeedDate = xe.GetAttribute("IsNeedDate") == "是";
                        m_Sign.Add(info);
                    }
                }
                xmlNodes = xmlDoc.GetElementsByTagName("CoSign");
                foreach (XmlNode node in xmlNodes)
                {
                    foreach (XmlNode childNode in node.ChildNodes)
                    {
                        AttburiteConfigInfo info = new AttburiteConfigInfo();
                        XmlElement xe = childNode as XmlElement;
                        info.name = xe.GetAttribute("Name");
                        info.label = xe.GetAttribute("Label");
                        info.count = Convert.ToInt16(xe.GetAttribute("Count"));
                        info.isNeedDate = xe.GetAttribute("IsNeedDate") == "是";
                        info.IsNeedMajor = xe.GetAttribute("IsNeedMajor") == "是";
                        m_Cosign.Add(info);
                    }
                }
                xmlNodes = xmlDoc.GetElementsByTagName("Barcode");
                foreach (XmlNode node in xmlNodes)
                {
                    foreach (XmlNode childNode in node.ChildNodes)
                    {
                        AttburiteConfigInfo info = new AttburiteConfigInfo();
                        XmlElement xe = childNode as XmlElement;
                        info.name = xe.GetAttribute("Name");
                        info.label = xe.GetAttribute("Label");
                        info.count = Convert.ToInt16(xe.GetAttribute("Count"));
                        info.isNeedDate = false;
                        info.IsNeedMajor = false;
                        m_Barcode.Add(info);
                    }
                }
                xmlNodes = xmlDoc.GetElementsByTagName("QRcode");
                foreach (XmlNode node in xmlNodes)
                {
                    foreach (XmlNode childNode in node.ChildNodes)
                    {
                        AttburiteConfigInfo info = new AttburiteConfigInfo();
                        XmlElement xe = childNode as XmlElement;
                        info.name = xe.GetAttribute("Name");
                        info.label = xe.GetAttribute("Label");
                        info.count = Convert.ToInt16(xe.GetAttribute("Count"));
                        info.isNeedDate = false;
                        info.IsNeedMajor = false;
                        m_QRcode.Add(info);
                    }
                }
                xmlNodes = xmlDoc.GetElementsByTagName("Stamp");
                foreach (XmlNode node in xmlNodes)
                {
                    foreach (XmlNode childNode in node.ChildNodes)
                    {
                        AttburiteConfigInfo info = new AttburiteConfigInfo();
                        XmlElement xe = childNode as XmlElement;
                        info.name = xe.GetAttribute("Name");
                        info.label = xe.GetAttribute("Label");
                        info.count = Convert.ToInt16(xe.GetAttribute("Count"));
                        info.isNeedDate = false;
                        info.IsNeedMajor = false;
                        m_Stamp.Add(info);
                    }
                }
                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

        public string GetStandardLabel(string name)
        {
            try
            {
                string reVal = string.Empty;
                foreach(AttburiteConfigInfo info in m_Standard)
                {
                    if (string.Compare(name,info.name)==0)
                    {
                        reVal = info.label;
                    }
                }
                return reVal;
            }
            catch (System.Exception ex)
            {
                return string.Empty;
            }
        }

        public int GetStandardLabelCount(string name)
        {
            try
            {
                int reVal = 1;
                foreach (AttburiteConfigInfo info in m_Standard)
                {
                    if (string.Compare(name, info.name) == 0)
                    {
                        reVal = info.count;
                    }
                }
                return reVal;
            }
            catch (System.Exception ex)
            {
                return 1;
            }
        }

        public string GetSignLabel(string name)
        {
            try
            {
                string reVal = string.Empty;
                foreach (AttburiteConfigInfo info in m_Sign)
                {
                    if (string.Compare(name, info.name) == 0)
                    {
                        reVal = info.label;
                    }
                }
                return reVal;
            }
            catch (System.Exception ex)
            {
                return string.Empty;
            }
        }

        public string GetCoSignLabel(string name)
        {
            try
            {
                string reVal = string.Empty;
                foreach (AttburiteConfigInfo info in m_Cosign)
                {
                    if (string.Compare(name, info.name) == 0)
                    {
                        reVal = info.label;
                    }
                }
                return reVal;
            }
            catch (System.Exception ex)
            {
                return string.Empty;
            }
        }

        public string GetBarcodeLabel(string name)
        {
            try
            {
                string reVal = string.Empty;
                foreach (AttburiteConfigInfo info in m_Barcode)
                {
                    if (string.Compare(name, info.name) == 0)
                    {
                        reVal = info.label;
                    }
                }
                return reVal;
            }
            catch (System.Exception ex)
            {
                return string.Empty;
            }
        }

        public string GetQRcodeLabel(string name)
        {
            try
            {
                string reVal = string.Empty;
                foreach (AttburiteConfigInfo info in m_QRcode)
                {
                    if (string.Compare(name, info.name) == 0)
                    {
                        reVal = info.label;
                    }
                }
                return reVal;
            }
            catch (System.Exception ex)
            {
                return string.Empty;
            }
        }

        public string GetStampLabel(string name)
        {
            try
            {
                string reVal = string.Empty;
                foreach (AttburiteConfigInfo info in m_Stamp)
                {
                    if (string.Compare(name, info.name) == 0)
                    {
                        reVal = info.label;
                    }
                }
                return reVal;
            }
            catch (System.Exception ex)
            {
                return string.Empty;
            }
        }

        public bool UpdateInitInfo()
        {
            try
            {
                m_Standard.Clear();
                m_Sign.Clear();
                m_Cosign.Clear();
                m_Barcode.Clear();
                m_QRcode.Clear();
                m_Stamp.Clear();
                InitInfo();
                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }
    }
}
