using System.Xml.Serialization;
using System.IO;
namespace Helper.Utilities
{
    public class SerializeHelper
    {
        public static void Serialize<T>(string filePath, object pobject)
        {
            StreamWriter objStreamWriter = new StreamWriter(filePath);
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            serializer.Serialize(objStreamWriter, pobject);
            objStreamWriter.Close();
            serializer = null;
            objStreamWriter.Dispose();
            objStreamWriter = null;
        }
    }
}
