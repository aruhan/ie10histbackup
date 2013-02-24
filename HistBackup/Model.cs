using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.IO;
using System.Diagnostics;

namespace HistBackup
{
    class Model : BindableBase
    {
        public static readonly Model Singleton = new Model();

        public string SystemDirectory { get; private set; }

        public string BackupDirectory
        {
            get { return _backupDirectory; }
            set { SetProperty(ref _backupDirectory, value, "BackupDirectory"); }
        }

        public string RestoreDirectory
        {
            get { return _restoreDirectory; }
            set { SetProperty(ref _restoreDirectory, value, "RestoreDirectory"); }
        }

        public Model()
        {
            SystemDirectory = Environment.ExpandEnvironmentVariables(@"%LOCALAPPDATA%\Microsoft\Windows\WebCache");
            var datetime = DateTime.Now.ToString("yyyy-MM-dd hhmmyy");
            _backupDirectory = Path.Combine(BaseDir(), datetime);
        }

        public bool DoBackup()
        {
            var exepath = Path.Combine(BaseDir(), "HoboCopy.exe");
            var psi = new ProcessStartInfo(
                exepath,
                String.Format("{0} {1}", SystemDirectory, BackupDirectory));
            var process = Process.Start(psi);
            process.WaitForExit();
            return process.ExitCode == 0;
        }

        private string _backupDirectory;
        private string _restoreDirectory;

        private static string BaseDir()
        {
            return Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]);
        }
    }
}
