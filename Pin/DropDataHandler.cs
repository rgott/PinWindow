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
                //client.DownloadFile("data:image/jpeg;base64,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",Path.Combine(project.ProjectPath,"name.png"));
            }
            else if(e.Data.GetFormats().contains(DataFormats.FileDrop))
            {// file drop
                Array data = ((IDataObject)e.Data).GetData(DataFormats.FileDrop) as Array;
                if (data != null)
                {
                    foreach (string item in data)
                    {
                        var savePath = Path.Combine(project.ProjectPath, Path.GetFileName(item));
                        Console.WriteLine(String.Join(", ", item) + "\t=>\t" + savePath);
                        try
                        {
                            if(Directory.Exists(savePath))
                            {
                                switch ((ActionEvent)Properties.Settings.Default.ActionEvent)
                                {
                                    case ActionEvent.Copy:
                                        // TODO: make recursive
                                        string[] files = Directory.GetFiles(savePath);

                                        Directory.CreateDirectory(savePath);
                                        foreach (var file in files)
                                        {
                                            File.Move(file, savePath);
                                        }

                                        break;
                                    case ActionEvent.Move:
                                        Directory.Move(item, savePath);
                                        break;
                                }
                            }
                            else
                            {
                                switch ((ActionEvent)Properties.Settings.Default.ActionEvent)
                                {
                                    case ActionEvent.Copy:
                                        File.Copy(item, savePath);
                                        break;
                                    case ActionEvent.Move:
                                        File.Move(item, savePath);
                                        break;
                                }
                            }
                            
                        }
                        catch(Exception)
                        {
                            MessageBox.Show("Invalid Location:" + savePath);
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
