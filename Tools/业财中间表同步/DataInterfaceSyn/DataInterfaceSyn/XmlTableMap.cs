using DataInterfaceSyn.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace DataInterfaceSyn
{
    public class XmlTableMap
    {
        [XmlAttribute("sourceConn")]
        public string SourceConnStr { get; set; }

        [XmlAttribute("source")]
        public string SourceTable { get; set; }
        [XmlAttribute("target")]
        public string TargetTable { get; set; }
        [XmlAttribute("waitSecond")]
        public string WaitSec { get; set; }
        [XmlArray("fieldMaps")]
        public List<SpecialFieldMap> SpecialFieldMaps { get; set; }
        

        public XmlTableMap()
        {
            SpecialFieldMaps = new List<SpecialFieldMap>();
        }

        public class SpecialFieldMap
        {
            [XmlAttribute("source")]
            public string SourceField { get; set; }
            [XmlAttribute("target")]
            public string TargetField { get; set; }
        }

        public static List<XmlTableMap> Create(string strXML)
        {
            try
            {
                using (XmlTextReader sr = new XmlTextReader(strXML))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(List<XmlTableMap>), new XmlRootAttribute("Root"));
                    return serializer.Deserialize(sr) as List<XmlTableMap>;
                }
            }
            catch (Exception ex)
            {
                LogWriter.Error(ex);
                return null;
            }
        }
    }


}
