using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Pin
{
    public class CustomXmlSerializer
    {
        public string Serialize()
        {
            var settings = new XmlWriterSettings();
            settings.Indent = false;
            settings.NewLineHandling = NewLineHandling.None;

            var xmlOutput = new StringBuilder();
            using (var writer = XmlWriter.Create(xmlOutput, settings))
            {
                var serializer = new XmlSerializer(GetType());

                serializer.Serialize(writer, this);
                return xmlOutput.ToString();
            }
        }

        public static T Deserialize<T> (string objectData)
        {
            var serializer = new XmlSerializer(typeof(T));

            using (var reader = new StringReader(objectData))
            {
                return (T)serializer.Deserialize(reader);
            }
        }
    }
}
