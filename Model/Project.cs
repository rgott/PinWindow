using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Text;
using System.Windows.Media;
using System.Xml;
using System.Xml.Serialization;

namespace Pin.Model
{
    [Serializable]
    public class Project
    {
        public static implicit operator Project(string obj)
        {
            return Project.Deserialize(obj);
        }

        public static implicit operator string(Project obj)
        {
            return obj.Serialize();
        }

        public int ID { get; set; }
        public string ProjectName { get; set; }
        public string ProjectPath { get; set; }

        [XmlElement(Type = typeof(XmlColor))]
        public SolidColorBrush Color { get; set; }

        public Project()
        {

        }

        public Project(string ProjectName, string ProjectPath, SolidColorBrush Color)
        {
            this.ID = ID;
            this.ProjectName = ProjectName;
            this.ProjectPath = ProjectPath;
            this.Color = Color;
        }

        public override bool Equals(object obj)
        {
            if(obj is int)
            {
                return ID == obj as int?;
            }
            if (obj is string)
            {
                return ProjectName.Equals(obj as string);
            }
            else if (obj is Project)
            {
                return ProjectName.Equals((obj as Project).ProjectName);
            }
            return false;
        }



        public override int GetHashCode()
        {
            return ProjectName.GetHashCode();
        }

        internal string Serialize()
        {
            StringBuilder xmlOutput = new StringBuilder();
            XmlSerializer xmlSerializer = new XmlSerializer(GetType());

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = false;
            settings.NewLineHandling = NewLineHandling.None;

            using (XmlWriter writer = XmlWriter.Create(xmlOutput, settings))
            {
                xmlSerializer.Serialize(writer, this);
                return xmlOutput.ToString();
            }
        }

        internal static Project Deserialize(string objectData)
        {
            var serializer = new XmlSerializer(typeof(Project));
            object result;

            using (TextReader reader = new StringReader(objectData))
            {
                result = serializer.Deserialize(reader);
            }
            return result as Project;
        }
    }
}
