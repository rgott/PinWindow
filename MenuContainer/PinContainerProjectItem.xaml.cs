﻿using System;
using System.Windows;
using System.Windows.Controls;

namespace Pin.MenuContainer
{
    /// <summary>
    /// Interaction logic for PinContainerProjectItem.xaml
    /// </summary>
    public partial class PinContainerProjectItem : UserControl
    {
        public delegate void ProjectItemDropEventHandler(object sender, Model.Project project, string[] sourcePaths);
        public event ProjectItemDropEventHandler ProjectItemDropped;


        public ProjectViewModel DataModel { get; set; }
        public PinContainerProjectItem(ProjectViewModel project)
        {
            DataContext = DataModel = project;
            InitializeComponent();
        }

        private void UserControl_DragEnter(object sender, DragEventArgs e)
        {
            
        }

        private void UserControl_Drop(object sender, DragEventArgs e)
        {
            //isPopupOpen = false;
            //if (ProjectItemDropped != null) ProjectItemDropped(sender, DataModel.Project, DropDataHandler.dropData(DataModel.Project, e));
        }

        private void UserControl_DragLeave(object sender, DragEventArgs e)
        {
            //isPopupOpen = false;
            e.Handled = true;
        }

        private void UIProjectProperties_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            e.Handled = false;
        }
    }
}
