using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BMC.Common.Utilities;
using BMC.CoreLib;
using BMC.ExComms.Simulator.DataLayer;

namespace BMC.ExComms.Simulator
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        internal static readonly FFDbManager DB = new FFDbManager();
        internal static readonly StringBuilder SbLog = new StringBuilder();
        internal static bool ScreenInitialized = false;

        public App()
        {
            BMCRegistryHelper.ActiveInstallationType = BMCCategorizedInstallationTypes.Exchange;
            Log.AddAppFileLoggingSystem();
            Log.GlobalWriteToExternalLog += Log_GlobalWriteToExternalLog;
        }

        void Log_GlobalWriteToExternalLog(string formattedMessage, CoreLib.Diagnostics.LogEntryType type, object extra)
        {
            if (ScreenInitialized)
            {
                Log.GlobalWriteToExternalLog -= Log_GlobalWriteToExternalLog;
                return;
            }
            SbLog.AppendLine(DateTime.Now.ToFullString() + "\t" + formattedMessage);
        }

        public static Window MainWindow { get; set; }
    }
}
