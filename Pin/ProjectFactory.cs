using Pin.Model;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Pin
{
    public static class ProjectFactory
    {
        public static IProjectViewModel getViewModelFromModel(IProject Project)
        {
            return new ProjectViewModel(Project);
        }

        public static readonly IProject DefaultProject = new Model.Project("", "", new SolidColorBrush(Colors.Orange), false);
    }
}
