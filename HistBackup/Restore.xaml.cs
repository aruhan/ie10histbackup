using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Navigation;
using System.IO;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace HistBackup
{
    /// <summary>
    /// Restore.xaml の相互作用ロジック
    /// </summary>
    public partial class Restore : Page
    {
        public Restore()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var model = ModelLocator.Model;
            if (!System.IO.Directory.Exists(model.RestoreDirectory))
            {
                MessageBox.Show(
                    "バックアップ元ディレクトリが存在しません。存在するディレクトリを指定してください。",
                    "バックアップ元ディレクトリが存在しません",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
                return;
            }
            model.DoRestore();
        }

        private void FolderSelect(object sender, RoutedEventArgs e)
        {
            var model = ModelLocator.Model;
            using (var dlg = new CommonOpenFileDialog())
            {
                dlg.IsFolderPicker = true;
                dlg.EnsurePathExists = true;
                if (Directory.Exists(model.RestoreDirectory))
                {
                    dlg.InitialDirectory = model.RestoreDirectory;
                }
                if (dlg.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    model.RestoreDirectory = dlg.FileName;
                }
            }
        }
    }
}
