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
    public class Project : CustomXmlSerializer, IProject
    {
        public static explicit operator Model.Project(string obj)
        {
            return Deserialize<Model.Project>(obj);
        }

        public static explicit operator string(Project obj)
        {
            return obj.Serialize();
        }

        public string Name { get; set; }
        public string Path { get; set; }

        [XmlElement(Type = typeof(XmlColor))]
        public Brush Color { get; set; }

        /// <summary>
        /// Used for Serialization
        /// </summary>
        protected Project() { }

        public Project(string Name, string Path, Brush Color)
        {
            this.Name = Name;
            this.Path = Path;
            this.Color = Color;
        }


        public override bool Equals(object obj)
        {
            if (obj is string)
            {
                return Name.Equals(obj as string);
            }
            else if (obj is Project)
            {
                return Name.Equals((obj as Project).Name);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}
