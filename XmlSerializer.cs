using Pin.Model;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Pin
{
    public class XmlSerializer
    {
        internal string Serialize()
        {
            StringBuilder xmlOutput = new StringBuilder();
            System.Xml.Serialization.XmlSerializer xmlSerializer = new System.Xml.Serialization.XmlSerializer(GetType());

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = false;
            settings.NewLineHandling = NewLineHandling.None;

            using (XmlWriter writer = XmlWriter.Create(xmlOutput, settings))
            {
                xmlSerializer.Serialize(writer, this);
                return xmlOutput.ToString();
            }
        }

        internal static T Deserialize<T> (string objectData)
        {
            var serializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
            object result;

            using (TextReader reader = new StringReader(objectData))
            {
                result = serializer.Deserialize(reader);
            }
            return (T)result;
        }
    }
}
