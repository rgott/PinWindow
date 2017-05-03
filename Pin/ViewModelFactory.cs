using System;
using Pin.MenuContainer;
using Pin.ProjectContainer;

namespace Pin
{
    internal static class ViewModelFactory
    {
        internal static MainViewModel MainVM(ISettings Settings, IApplicationWindow Window)
        {
            var Projects = new ProjectViewModelList(Window, Settings);

            var Menu = new MenuItemViewModel(Settings, Window, Projects);

            return new MainViewModel(Menu, Settings, Window);
        }
    }
}