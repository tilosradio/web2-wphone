
namespace TD1990.Libs.TDLyutil.Rss
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    public class RssReader
    {
        public static RssRoot Load(Stream stream)
        {
            return Xml.Serializer.Deserialize<RssRoot>(stream);
        }

        public static RssRoot Load(string text)
        {
            return Xml.Serializer.Deserialize<RssRoot>(text);
        }

        public static RssItem DeserializeRssItem(string text)
        {
            return Xml.Serializer.Deserialize<RssItem>(text);
        }

    }
}
