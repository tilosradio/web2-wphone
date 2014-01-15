namespace TD1990.Libs.TDLyutil.Xml
{
    using System.IO;
    using System.Xml.Serialization;

    public static class Serializer
    {
        public static T Deserialize<T>(string text)
        {
            using (StringReader reader = new StringReader(text))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                var root = (T)serializer.Deserialize(reader);
                return root;
            }
        }

        public static T Deserialize<T>(Stream stream)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            var root = (T)serializer.Deserialize(stream);
            return root;
        }

    }
}
