using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows;

namespace Sort
{
    public partial class MainWindow : Window
    {
        public string? cdir;

        public MainWindow() => InitializeComponent();

        private void SortButton_Click(object sender, RoutedEventArgs e) => InitMethod(SortByTypes);

        private void outputFiles_Click(object sender, RoutedEventArgs e) => InitMethod(OutputFiles);

        public void SortByTypes()
        {
            List<string> extentions = new List<string>();

            if (cdir?.Length > 0)
            {
                var files = Directory.GetFiles(cdir);

                if (files.Length > 0)
                {
                    foreach (var file in files)
                        extentions.Add(new FileInfo(file).Extension);

                    foreach (var exten in extentions)
                        Directory.CreateDirectory(cdir + "/" + exten);

                    foreach (var file in files)
                    {
                        var fileInf = new FileInfo(file);

                        foreach (var exten in extentions)
                        {
                            if (fileInf.Extension == exten)
                                fileInf.MoveTo(cdir + "/" + exten + "/" + fileInf.Name);
                        }
                    }
                }
            }
            else ErrorMessage();
        }

        public void OutputFiles()
        {
            List<string> allFiles = new List<string>();

            if (cdir?.Length > 0)
            {
                foreach (var file in Directory.GetFiles(cdir))
                    allFiles.Add(file);

                var dirs = Directory.GetDirectories(cdir);

                foreach (var dir in dirs)
                {
                    foreach (var file in Directory.GetFiles(dir))
                        allFiles.Add(file);
                }

                foreach (var file in allFiles)
                {
                    var fileInf = new FileInfo(file);

                    fileInf.MoveTo(cdir + "/" + fileInf.Name);
                }
            }
            else ErrorMessage();
        }

        public async void InitMethod(Action action) => await Task.Run(action);

        public void ErrorMessage() => MessageBox.Show("Put the path to sort");

        private void selectDir_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e) => cdir = selectDir.Text;
    }
}
