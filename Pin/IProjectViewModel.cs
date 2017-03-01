using System.Windows.Media;
using Pin.Model;
using GalaSoft.MvvmLight.Command;

namespace Pin
{
    public interface IProjectViewModel
    {
        Model.IProject Project { get; set; }

        /// <summary>
        /// Used to display editor view
        /// </summary>
        bool ShowEditor { get; set; }

        /// <summary>
        /// Open Project's path in a file browser
        /// </summary>
        RelayCommand OpenWithExplorer { get; set; }


        bool Equals(object obj);
    }
}