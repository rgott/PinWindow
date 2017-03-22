using Pin.Model;
using Pin.ProjectContainer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Pin.Tests
{
    public static class PinFactory
    {
        public static ProjectViewModel ProjectVM(string Name = "testName",string Path = "testPath")
        {
            var Window = new BlankTestWindow();
            var Settings = new BlankTestSettings();
            return new ProjectViewModel(new ProjectViewModelList(Window, Settings), Window, new Pin.Model.Project(Name, Path, new SolidColorBrush(Colors.Brown)));
        }

        internal static IProject Project(string Name = "testName", string Path = "testPath")
        {
            return new Model.Project(Name, Path, new SolidColorBrush(Colors.Azure));
        }

       
    }
}
