using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.IO;
using System.Diagnostics;

namespace HistBackup
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public string SourceDirectory { get; private set; }
        public string DestinationDirectory { get; private set; }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SourceDirectory = Environment.ExpandEnvironmentVariables(@"%LOCALAPPDATA%\Microsoft\Windows\WebCache");
            var basedir = Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]);
            var dir = DateTime.Now.ToString("yyyyMMdd");
            DestinationDirectory = Path.Combine(basedir, dir);

            SourceDirectoryText.Text = SourceDirectory;
            DestinationDirectoryText.Text = DestinationDirectory;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var basedir = Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]);
            var arg = String.Format("{0} {1}", SourceDirectory, DestinationDirectory);
            var psi = new ProcessStartInfo(Path.Combine(basedir, "hobocopy.exe"), arg);
            var process = Process.Start(psi);
            process.WaitForExit();
            if (process.ExitCode == 0)
            {
                MessageBox.Show("バックアップに成功しました");
            }
        }

        private void DestinationDirectoryText_TextChanged(object sender, TextChangedEventArgs e)
        {
            DestinationDirectory = DestinationDirectoryText.Text;
        }

    }

}
