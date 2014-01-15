
namespace TD1990.Libs.TDLyutil.Rss
{
    using System.Xml.Schema;
    using System.Xml.Serialization;
    using TDLyutil.Interfaces.Rss;
    [XmlRoot("rss")]
    public partial class RssRoot : IRssRoot
    {
        [XmlElement("channel", Form = XmlSchemaForm.Unqualified)]
        public RssChannel[] channels { get; set; }

        [XmlAttribute("version")]
        public string Version { get; set; }

        [XmlIgnore]
        public IRssChannel[] Channels
        {
            get
            {
                return channels;
            }
        }
    }
}
