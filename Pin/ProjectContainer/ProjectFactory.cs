using Pin.Model;
using Pin.ProjectContainer;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Pin
{
    public static class ProjectFactory
    {
        public static IProjectViewModel getViewModelFromModel(ProjectViewModelList ProjectVM,IApplicationWindow Window,IProject Project)
        {
            return new ProjectViewModel(ProjectVM, Window, Project);
        }

        public static readonly IProject DefaultProject = Model.Project.DefaultOrange;
    }
}
