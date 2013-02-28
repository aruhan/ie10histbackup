using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.IO;
using System.Diagnostics;

namespace HistBackup
{
    class ModelLocator
    {
        public static Model Model = new Model();
    }

    class Model : BindableBase
    {
        public string SystemDirectory { get; private set; }
        public string Version { get; private set; }

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
            Version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            SystemDirectory = Environment.ExpandEnvironmentVariables(@"%LOCALAPPDATA%\Microsoft\Windows\WebCache");
            var datetime = DateTime.Now.ToString("yyyy-MM-dd hhmmyy");
            _backupDirectory = Path.Combine(BaseDir(), datetime);
        }

        public bool DoBackup()
        {
            var arch = Environment.Is64BitOperatingSystem ? "x64" : "x86";
            var exepath = Path.Combine(BaseDir(), "HoboCopy", String.Format("HoboCopy-{0}.exe", arch));
            var psi = new ProcessStartInfo(
                exepath,
                String.Format("{0} \"{1}\"", SystemDirectory, BackupDirectory));
            var process = Process.Start(psi);
            process.WaitForExit();
            return process.ExitCode == 0;
        }

        public bool DoRestore()
        {
            return true;
        }

        private string _backupDirectory;
        private string _restoreDirectory;

        private static string BaseDir()
        {
            return Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]);
        }
    }
}
