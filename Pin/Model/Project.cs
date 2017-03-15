using System;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Media;
using System.Xml;
using System.Xml.Serialization;

namespace Pin.Model
{
    [Serializable]
    [XmlInclude(typeof(XmlBrushConverter))]
    public class Project : IProject , INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public static explicit operator Model.Project(string obj)
        {
            return Deserialize<Model.Project>(obj);
        }

        public static explicit operator string(Project obj)
        {
            return obj.Serialize();
        }

        private string _Name;
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
                NotifyPropertyChanged();
            }
        }

        private string _Path;
        public string Path
        {
            get
            {
                return _Path;
            }
            set
            {
                _Path = value;
                NotifyPropertyChanged();
            }
        }
        [XmlElement(Type = typeof(XmlBrushConverter))]
        private Brush _Color;
        [XmlElement(Type = typeof(XmlBrushConverter))]
        public Brush Color
        {
            get
            {
                return _Color;
            }
            set
            {
                _Color = value;
                NotifyPropertyChanged();
            }
        }

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
            if (obj is Project)
            {
                return Name.Equals((obj as Project).Name);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        object ICloneable.Clone()
        {
            return new Project(Name, Path, Color);
        }

        public string Serialize()
        {
            var settings = new XmlWriterSettings()
            {
                Indent = false,
                NewLineHandling = NewLineHandling.None
            };

            var xmlOutput = new StringBuilder();
            using (var writer = XmlWriter.Create(xmlOutput, settings))
            {
                var serializer = new XmlSerializer(GetType());

                serializer.Serialize(writer, this);
                return xmlOutput.ToString();
            }
        }

        public static T Deserialize<T>(string objectData)
        {
            var serializer = new XmlSerializer(typeof(T));

            using (var reader = new StringReader(objectData))
            {
                return (T)serializer.Deserialize(reader);
            }
        }

    }
}
