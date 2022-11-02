using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace Sort
{
    public partial class MainWindow : Window
    {
        public string cdir = "";

        public bool isGotFocus = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        #region Select&output Directories
        private void selectDir_GotFocus(object sender, RoutedEventArgs e)
        {
            isGotFocus = true;
        }

        private void selectDir_LostFocus(object sender, RoutedEventArgs e)
        {
            isGotFocus = false;
        }

        private void selectDir_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && isGotFocus)
            {
                cdir = selectDir.Text;

                cDir.Text = cdir;
            }
        }
        #endregion

        /// <summary>
        /// Click magic
        /// </summary>
        private void SortButton_Click(object sender, RoutedEventArgs e)
        {
            //Create new List for extentions
            List<string> extentions = new List<string>();

            //if cdir is exist
            if (cdir.Length > 0)
            {
                //get all files
                var files = Directory.GetFiles(cdir);

                //if files count more than zero lol
                if (files.Length > 0)
                {
                    //add extentions
                    foreach (var file in files)
                    {
                        var format = new FileInfo(file);

                        extentions.Add(format.Extension);
                    }

                    //create extention folder
                    foreach (var exten in extentions)
                    {
                        Directory.CreateDirectory(cdir + "/" + exten);
                    }

                    //move current file to current folder
                    foreach (var file in files)
                    {
                        var fileInf = new FileInfo(file);

                        foreach (var exten in extentions)
                        {
                            if (fileInf.Extension == exten)
                            {
                                string thisPath = cdir + "/" + exten + "/";

                                fileInf.MoveTo(thisPath + fileInf.Name);
                            }
                        }
                    }
                }
            }
        }
    }
}
