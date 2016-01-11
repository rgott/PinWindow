using System;
using System.Windows.Controls;
using System.Windows;
using Forms = System.Windows.Forms;
namespace Pin
{
    class PRadioButton : RadioButton
    {
        public PRadioButton(UIElementCollection componentCollection, string projectName, string projectPath)
        {
            //< RadioButton Padding = "10" >
            //  < RadioButton.Content >
            //      < Grid Width = "280" >
            //          < TextBlock Text = "Project 1" />
            //          < Button HorizontalAlignment = "Right" Padding = "0" Content = "+" />
            //          < Button HorizontalAlignment = "Right" Padding = "0" Content = ">" />
            //      </ Grid >
            //  </ RadioButton.Content >
            //</ RadioButton>

            // add xaml
            RadioButton RButton = new RadioButton();
            RButton.Template = (ControlTemplate)FindResource("StyledRadioButton");
            RButton.Padding = new Thickness(10);

            Grid tempG = new Grid();
            tempG.Width = 280;

            TextBlock tempT = new TextBlock();
            tempT.Text = projectName;
            tempG.Children.Add(tempT);

            Button tempB = new Button();
            tempB.HorizontalAlignment = HorizontalAlignment.Right;
            tempB.Padding = new Thickness(0);
            tempB.Margin = new Thickness(0, 0, 15, 0);
            tempB.Content = "+";
            tempB.Click += new RoutedEventHandler(delegate (object o, RoutedEventArgs e)
            {
                System.Diagnostics.Process.Start("explorer.exe", "/root," + projectPath);
            });
            //ContextMenu tempB_cMenu = new ContextMenu();
            //MenuItem tempB_mItem;
            //tempB_mItem = new MenuItem(); // open with explorer
            //tempB_mItem.Header = "Open with Explorer";
            //tempB_mItem.Click += new RoutedEventHandler(delegate (object o, RoutedEventArgs e)
            //{
            //    System.Diagnostics.Process.Start("explorer.exe", "/root," + projectPath);
            //});
            //tempB_cMenu.Items.Add(tempB_mItem);
            //tempB_mItem = new MenuItem(); // open with app
            //tempB_mItem.Header = "Open with App";
            //tempB_mItem.Click += new RoutedEventHandler(delegate (object o, RoutedEventArgs e)
            //{
            //    // TODO: Implement application explorer
            //    System.Diagnostics.Process.Start("explorer.exe", "/root," + projectPath);
            //});
            //tempB_cMenu.Items.Add(tempB_mItem);
            //tempB.ContextMenu = tempB_cMenu;
            tempG.Children.Add(tempB);

            // add second button
            tempB = new Button();
            tempB.HorizontalAlignment = HorizontalAlignment.Right;
            tempB.Padding = new Thickness(0);
            tempB.Content = ">";
            tempB.Click += new RoutedEventHandler(delegate (object o, RoutedEventArgs e)
            {
                System.Diagnostics.Process.Start("explorer.exe", "/root," + projectPath);
            });
            tempG.Children.Add(tempB);

            #region ContextMenu
            ContextMenu tempG_cMenu = new ContextMenu();
            tempG_cMenu.Opened += masterPopupToggleOn;
            tempG_cMenu.Closed += masterPopupToggleOff;
            MenuItem tempG_mItem;
            tempG_mItem = new MenuItem();
            tempG_mItem.Header = "Edit";
            tempG_mItem.Click += new RoutedEventHandler(delegate (object o, RoutedEventArgs e)
            {
                int index = componentCollection.IndexOf(RButton); // replace radio button with editable version
                //< RadioButton >
                //  < Grid Width = "275" >
                //      < TextBox />
                //      < TextBox />
                //      < Button Content = "Save" />
                //      < Button Content = "Cancel" />
                //  </ Grid >
                //</ RadioButton >
                RadioButton editRButton = new RadioButton();
                editRButton.Template = (ControlTemplate)FindResource("StyledRadioButton");
                editRButton.Padding = new Thickness(10);


                // setup grid row - column
                Grid ERB_grid = new Grid();
                RowDefinition row1 = new RowDefinition(); row1.Height = new GridLength(1, GridUnitType.Auto); ERB_grid.RowDefinitions.Add(row1);
                RowDefinition row2 = new RowDefinition(); row2.Height = new GridLength(1, GridUnitType.Auto); ERB_grid.RowDefinitions.Add(row2);
                ColumnDefinition col1 = new ColumnDefinition(); col1.Width = new GridLength(1, GridUnitType.Star); ERB_grid.ColumnDefinitions.Add(col1);
                ColumnDefinition col2 = new ColumnDefinition(); col2.Width = new GridLength(45, GridUnitType.Pixel); ERB_grid.ColumnDefinitions.Add(col2);
                ColumnDefinition col3 = new ColumnDefinition(); col3.Width = new GridLength(45, GridUnitType.Pixel); ERB_grid.ColumnDefinitions.Add(col3);


                ERB_grid.Width = 275;
                TextBox ERB_name = new TextBox();
                ERB_name.Text = projectName;
                Grid.SetColumn(ERB_name, 0);
                Grid.SetRow(ERB_name, 0);
                Grid.SetColumnSpan(ERB_name, 2);

                ERB_grid.Children.Add(ERB_name);
                TextBox ERB_path = new TextBox(); // new textbox
                ERB_path.Text = projectPath;
                Grid.SetColumn(ERB_path, 0);
                Grid.SetRow(ERB_path, 1);
                ERB_grid.Children.Add(ERB_path);

                Button ERB_Btn = new Button();
                ERB_Btn.Content = "Save";
                Grid.SetColumn(ERB_Btn, 2);
                Grid.SetRow(ERB_Btn, 1);
                ERB_Btn.Click += new RoutedEventHandler(delegate (object ob, RoutedEventArgs ea)
                {
                    // change current version
                    tempT.Text = ERB_name.Text;
                    projectName = ERB_name.Text;
                    projectPath = ERB_path.Text;

                    // replace editable version with normal
                    componentCollection.RemoveAt(index);
                    componentCollection.Insert(index, RButton);

                    // change saved version
                    int settingIndex = Properties.Settings.Default.Projects.IndexOf(projectName + "\n" + projectPath);
                    if (settingIndex != -1)
                    {
                        Properties.Settings.Default.Projects.RemoveAt(settingIndex);
                        Properties.Settings.Default.Projects.Insert(settingIndex, ERB_name.Text + "\n" + ERB_path.Text.Replace('/', '\\'));
                        Properties.Settings.Default.Save();
                    }

                });
                ERB_grid.Children.Add(ERB_Btn);

                ERB_Btn = new Button();
                ERB_Btn.Content = "Cancel";
                Grid.SetColumn(ERB_Btn, 2);
                Grid.SetRow(ERB_Btn, 0);
                ERB_Btn.Click += new RoutedEventHandler(delegate (object ob, RoutedEventArgs ea)
                {
                    // replace editable version with normal
                    componentCollection.RemoveAt(index);
                    componentCollection.Insert(index, RButton);
                });
                ERB_grid.Children.Add(ERB_Btn);
                editRButton.Content = ERB_grid;

                ERB_Btn = new Button();
                ERB_Btn.Content = "Browse";
                Grid.SetColumn(ERB_Btn, 1);
                Grid.SetRow(ERB_Btn, 1);
                ERB_Btn.Click += new RoutedEventHandler(delegate (object ob, RoutedEventArgs ea)
                {
                    Forms.FolderBrowserDialog path = new Forms.FolderBrowserDialog();
                    path.ShowDialog();
                    projectPath = path.SelectedPath;

                    // change current version
                    ERB_path.Text = projectPath;



                    int settingIndex1 = Properties.Settings.Default.Projects.IndexOf(projectName + "\n" + projectPath);
                    if (settingIndex1 != -1)
                    {
                        Properties.Settings.Default.Projects.RemoveAt(settingIndex1);
                        Properties.Settings.Default.Projects.Insert(settingIndex1, ERB_name.Text + "\n" + ERB_path.Text.Replace('/', '\\'));
                        Properties.Settings.Default.Save();
                    }
                });
                ERB_grid.Children.Add(ERB_Btn);

                editRButton.Content = ERB_grid;

                componentCollection.RemoveAt(index);
                componentCollection.Insert(index, editRButton);
            });
            tempG_cMenu.Items.Add(tempG_mItem);

            tempG_mItem = new MenuItem(); // new item
            tempG_mItem.Header = "Delete";
            tempG_mItem.Click += new RoutedEventHandler(delegate (object o, RoutedEventArgs e)
            {
                componentCollection.Remove(RButton);// remove from form
                Properties.Settings.Default.Projects.Remove(projectName + "\n" + projectPath); // remove from settings
                Properties.Settings.Default.Save(); // save changes
            });
            tempG_cMenu.Items.Add(tempG_mItem);
            tempG.ContextMenu = tempG_cMenu;
#endregion

            // finalize radio button
            RButton.Content = tempG;
            componentCollection.Add(RButton);
        }
        private void masterPopupToggleOn(object sender, RoutedEventArgs e)
        {
            MouseOverController.isMouseOverMenu = true;
        }
        private void masterPopupToggleOff(object sender, RoutedEventArgs e)
        {
            MouseOverController.isMouseOverMenu = true;
        }
    }
}
