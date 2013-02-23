﻿using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace HistBackup
{
    /// <summary>
    /// Backup.xaml の相互作用ロジック
    /// </summary>
    public partial class Backup : Page
    {
        public Backup()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Model.Singleton.DoBackup())
            {
                MessageBox.Show("バックアップに成功しました");
            }
        }
    }
}
