using System.Xml.Serialization;
using System.Collections.Generic;
using System.IO;
namespace Helper.Utilities
{
    public static class DeserializeHelper
    {
        public static T DeserializeXML<T>(System.IO.Stream xmlStream)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            try
            {
                return (T)(serializer.Deserialize(xmlStream));
            }
            finally { serializer = null; }
        }

        public static T DeserializeXML<T>(string path)
        {
            Stream fs = File.OpenRead(path);

            XmlSerializer serializer = new XmlSerializer(typeof(T));
            try
            {
                return (T)(serializer.Deserialize(fs));
            }
            finally { serializer = null; fs.Close(); fs.Dispose(); fs = null; }
        }
    }
}

