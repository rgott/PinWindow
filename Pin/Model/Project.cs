using System;
using System.Collections;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using System.Xml;
using System.Xml.Serialization;

namespace Pin.Model
{
    [Serializable]
    public class Project : CustomXmlSerializer, IProject , INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
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

        private Brush _Color;
        [XmlElement(Type = typeof(XmlColor))]
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

        object ICloneable.Clone()
        {
            return new Project(Name, Path, Color);
        }
    }
}
