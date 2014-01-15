
namespace TD1990.Libs.TDLyutil.Rss
{
    using System;
    using System.Xml.Serialization;
    using TDLyutil.Interfaces.Rss;

    [XmlType("item")]
    public partial class RssItem : IRssItem
    {
        [XmlElement("title")]
        public string Title { get; set; }

        [XmlElement("link")]
        public string Link { get; set; }

        [XmlElement("description")]
        public string Description { get; set; }

        [XmlElement("pubDate")]
        public string PubDate { get; set; }

        [XmlIgnore]
        public Uri LinkUri
        {
            get { return new Uri(Link, UriKind.Absolute); }
        }

        [XmlIgnore]
        public DateTime? PublishDate
        {
            get
            {
                DateTime d;
                if (DateTime.TryParse(PubDate, out d))
                    return d;
                else
                    return null;
            }
        }
    }
}
