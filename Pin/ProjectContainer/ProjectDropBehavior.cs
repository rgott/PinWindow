﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Interactivity;

namespace Pin.ProjectContainer
{
    public class ProjectDropBehavior : Behavior<FrameworkElement>
    {
        public IProjectViewModel ProjectVM
        {
            get { return (IProjectViewModel)GetValue(ProjectVMProperty); }
            set { SetValue(ProjectVMProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ProjectVM.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ProjectVMProperty =
            DependencyProperty.Register("ProjectVM", typeof(IProjectViewModel), typeof(ProjectDropBehavior));


        protected override void OnAttached()
        {
            base.OnAttached();

            this.AssociatedObject.AllowDrop = true;
            this.AssociatedObject.DragEnter += DragEnterCmd;
            this.AssociatedObject.DragLeave += DragLeaveCmd;
            this.AssociatedObject.Drop += DropCmd;
        }

        protected virtual void DragEnterCmd(object sender, DragEventArgs e)
        {
            //Window.WindowChangeState(WindowState.MinimizedDragging);
        }

        protected virtual void DragLeaveCmd(object sender, DragEventArgs e)
        {
            //Window.WindowChangeState(WindowState.Minimized);
        }

        protected virtual void DropCmd(object sender, DragEventArgs e)
        {
            dropData(ProjectVM.Project, e);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="project"></param>
        /// <param name="e"></param>
        /// <returns>Path of newly dropped file</returns>
        public static string[] dropData(Model.IProject project, DragEventArgs e)
        {
            if (String.IsNullOrEmpty(project.Path))
            {
                MessageBox.Show("Path Not set.");
                return null;
            }

            if (e.Data.GetFormats().Contains(DataFormats.Html))
            {// if html then from web retrieve and save or content
                return DropHtml(project, e);
            }
            else if (e.Data.GetFormats().Contains(DataFormats.FileDrop))
            {// file drop
                return DropFileDrop(project, e);
            }
            return null;
        }





        private static string[] DropHtml(Model.IProject project, DragEventArgs e)
        {
            var match = Regex.Match((string)e.Data.GetData(DataFormats.Html), "src=\"(.*?)\""); // find source of dropped image
            if (match.Groups.Count >= 2)
            {
                string DestinationPath;
                var matchValue = match.Groups[1].Value;
                if (matchValue.StartsWith("http"))
                { // must download first
                    using (WebClient client = new WebClient())
                    {
                        var fileNameIndex = matchValue.LastIndexOf('/') + 1;
                        var fileName = matchValue.Substring(fileNameIndex, matchValue.Length - fileNameIndex);
                        DestinationPath = Path.Combine(project.Path, "_" + fileName);
                        if (File.Exists(DestinationPath))
                        {
                            DestinationPath = getCheckedRandomFileName(project.Path, fileName);
                        }
                        client.DownloadFile(matchValue, DestinationPath);
                    }
                }
                else //if (matchValue.StartsWith("data")
                { // contains a base 64 image
                    var extention = Regex.Match(matchValue, @"image\/(.*?);").Groups[1]?.Value; // finds the type of image (data:image/jpeg;...)
                    DestinationPath = getCheckedRandomFileName(project.Path, "." + extention);

                    var base64 = Regex.Match(matchValue, "base64,(.*)").Groups[1]?.Value;
                    var bytes = Convert.FromBase64String(base64);
                    using (var imageFile = new FileStream(DestinationPath, FileMode.Create))
                    {
                        imageFile.Write(bytes, 0, bytes.Length);
                        imageFile.Flush();
                    }
                }
                return new string[] { DestinationPath };
            }
            return null;
            // new WebClient().DownloadFile("data:image/jpeg;base64,/9j/4AAQSk...
        }

        public static string[] DropFileDrop(Model.IProject project, DragEventArgs e)
        {
            Array data = ((IDataObject)e.Data).GetData(DataFormats.FileDrop) as Array;
            if (data != null)
            {
                foreach (string SourcePath in data)
                {
                    var DestinationPath = Path.Combine(project.Path, Path.GetFileName(SourcePath));

                    try
                    {
                        if (Directory.Exists(DestinationPath))
                        {
                            switch ((ActionEvent)Properties.Settings.Default.ActionEvent)
                            {
                                case ActionEvent.Copy:
                                    CopyDirectory(SourcePath, DestinationPath);
                                    break;
                                case ActionEvent.Move:
                                    Directory.Move(SourcePath, DestinationPath);
                                    break;
                            }
                        }
                        else
                        {
                            switch ((ActionEvent)Properties.Settings.Default.ActionEvent)
                            {
                                case ActionEvent.Copy:
                                    File.Copy(SourcePath, DestinationPath);
                                    break;
                                case ActionEvent.Move:
                                    File.Move(SourcePath, DestinationPath);
                                    break;
                            }
                        }

                    }
                    catch (Exception)
                    {
                        Error.ErrorBox.Show("Invalid Location:" + DestinationPath);
                    }
                }
                return (string[])data;
            }
            return null;
        }


        /// <summary>
        /// Finds a random name for a file that does not exist
        /// </summary>
        /// <param name="path">Directory where file will be located</param>
        /// <param name="ext">File extention. must contain the leading period.</param>
        /// <returns></returns>
        private static string getCheckedRandomFileName(string path, string ext)
        {
            string fileName;
            while (File.Exists(fileName = Path.Combine(path, Path.GetRandomFileName().Replace(".", "") + ext))) { }
            return fileName;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <see cref="http://stackoverflow.com/a/3822913"/>
        /// <param name="SourcePath">Source Directory</param>
        /// <param name="DestinationPath">Destination Directory</param>
        public static void CopyDirectory(string SourcePath, string DestinationPath)
        {
            //Now Create all of the directories
            foreach (string dirPath in Directory.GetDirectories(SourcePath, "*",
                SearchOption.AllDirectories))
                Directory.CreateDirectory(dirPath.Replace(SourcePath, DestinationPath));

            //Copy all the files & Replaces any files with the same name
            foreach (string newPath in Directory.GetFiles(SourcePath, "*.*",
                SearchOption.AllDirectories))
                File.Copy(newPath, newPath.Replace(SourcePath, DestinationPath), true);
        }

    }
}
