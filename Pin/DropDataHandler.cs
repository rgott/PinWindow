using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using static Pin.MouseOverController;

namespace Pin
{
    public class DropDataHandler
    {
        public static void dropData(Model.Project project, DragEventArgs e)
        {
            if (String.IsNullOrEmpty(project.ProjectPath))
            {
                MessageBox.Show("Path Not set.");
                return;
            }

            if (e.Data.GetFormats().contains(DataFormats.Html))
            {// if html then from web retrieve and save or content

                var match = Regex.Match((string)e.Data.GetData(DataFormats.Html), "src=\"(.*?)\""); // find source of dropped image
                if(match.Groups.Count >= 2)
                {
                    var matchValue = match.Groups[1].Value;
                    if (matchValue.StartsWith("http"))
                    { // must download first
                        using (WebClient client = new WebClient())
                        {
                            var fileNameIndex = matchValue.LastIndexOf('/') + 1;
                            var fileName = matchValue.Substring(fileNameIndex, matchValue.Length - fileNameIndex);
                            var absoluteFilePath = Path.Combine(project.ProjectPath, "_" + fileName);
                            if (File.Exists(absoluteFilePath))
                            {
                                absoluteFilePath = getCheckedRandomFileName(project.ProjectPath, fileName);
                            }
                            client.DownloadFile(matchValue, absoluteFilePath);
                        }
                    }
                    else //if (matchValue.StartsWith("data")
                    { // contains a base 64 image
                        var extention = Regex.Match(matchValue, @"image\/(.*?);").Groups[1]?.Value; // finds the type of image (data:image/jpeg;...)
                        var absoluteFilePath = getCheckedRandomFileName(project.ProjectPath,"." + extention);

                        var base64 = Regex.Match(matchValue, "base64,(.*)").Groups[1]?.Value;
                        var bytes = Convert.FromBase64String(base64);
                        using (var imageFile = new FileStream(absoluteFilePath, FileMode.Create))
                        {
                            imageFile.Write(bytes, 0, bytes.Length);
                            imageFile.Flush();
                        }
                    }
                }
                //WebClient client = new WebClient();
                //client.DownloadFile("data:image/jpeg;base64,/9j/4AAQSk...
            }
            else if(e.Data.GetFormats().contains(DataFormats.FileDrop))
            {// file drop
                Array data = ((IDataObject)e.Data).GetData(DataFormats.FileDrop) as Array;
                if (data != null)
                {
                    foreach (string SourcePath in data)
                    {
                        var DestinationPath = Path.Combine(project.ProjectPath, Path.GetFileName(SourcePath));
                        //Console.WriteLine(String.Join(", ", item) + "\t=>\t" + DestinationPath);
                        try
                        {
                            if(Directory.Exists(DestinationPath))
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
                        catch(Exception)
                        {
                            MessageBox.Show("Invalid Location:" + DestinationPath);
                        }
                    }
                }
            }
        }


        /// <summary>
        /// Finds a random name for a file that does not exist
        /// </summary>
        /// <param name="path">Directory where file will be located</param>
        /// <param name="ext">File extention. must contain the leading period.</param>
        /// <returns></returns>
        private static string getCheckedRandomFileName(string path,string ext)
        {
            string fileName;
            while(File.Exists(fileName = Path.Combine(path,Path.GetRandomFileName().Replace(".","") + ext))) { }
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


        public static void setEffects(DragEventArgs e)
        {
            if (e.Data.GetFormats().contains(DataFormats.Html)
                && e.Data.GetFormats().contains(DataFormats.FileDrop))
            {
                switch ((ActionEvent)Properties.Settings.Default.ActionEvent)
                {
                    case ActionEvent.Move:
                        e.Effects = DragDropEffects.Move;
                        break;
                    case ActionEvent.Copy:
                        e.Effects = DragDropEffects.Copy;
                        break;
                }
            }
        }
    }
}
