using Pin.Model;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Pin
{
    public static class ProjectFactory
    {
        public static IProjectViewModel getViewModelFromModel(ProjectViewModelList ProjectVM,IMainWindow Window,IProject Project)
        {
            return new ProjectViewModel(ProjectVM, Window, Project);
        }

        public static readonly IProject DefaultProject = new Model.Project("", "", new SolidColorBrush(Colors.Orange));
    }
}
