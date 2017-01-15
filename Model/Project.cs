using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Windows.Media;

namespace Pin.Model
{
    public class Project
    {
        public string ProjectName { get; set; }
        public string ProjectPath { get; set; }
        public Brush Color { get; set; }

        public Project(string ProjectName,string ProjectPath,Brush Color)
        {
            this.ProjectName = ProjectName;
            this.ProjectPath = ProjectPath;
            this.Color = Color;
        }



        public override bool Equals(object obj)
        {
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

    }
}
