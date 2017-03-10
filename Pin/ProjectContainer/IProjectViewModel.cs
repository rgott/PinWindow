﻿using System.Windows.Media;
using Pin.Model;
using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;

namespace Pin
{
    public interface IProjectViewModel
    {
        IProject Project { get; set; }
        IProject OrigionalProject { get; set; }


        Queue<string[]> FileToDrop { get; set; }

        /// <summary>
        /// Used to display editor view
        /// </summary>
        bool ShowEditor { get; set; }
        bool ShowInfo { get; set; }

        /// <summary>
        /// Open Project's path in a file browser
        /// </summary>
        RelayCommand OpenWithExplorer { get; set; }
        RelayCommand ChangeDirectory { get; set; }

        bool Equals(object obj);
    }
}