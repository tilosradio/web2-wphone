
namespace TD1990.Libs.TDLyutil.Rss
{
    using System.Xml.Schema;
    using TD1990.Libs.TDLyutil.Interfaces.Rss;
    using System.Xml.Serialization;
    
    [XmlType("channel")]
    public partial class RssChannel : IRssChannel
    {
        [XmlElement("title")]
        public string Title { get; set; }

        [XmlElement("description")]
        public string Description { get; set; }

        [XmlElement("item", Form = XmlSchemaForm.Unqualified)]
        public RssItem[] items { get; set; }

        [XmlIgnore]
        public IRssItem[] Items
        {
            get
            {
                return items;
            }
        }
    }
}
