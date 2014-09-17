using BMC.Common.LogManagement;
using BMC.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Reflection;
using System.IO;

namespace BMC.UI.EnterpriseConfig
{
    static class Program
    {
        public enum AppResourceTypes
        {
            TextResource = 0,
            MessageResource = 1
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            #region Resource File
            string executingAssemblyFilePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string resourceFilePath = System.IO.Path.Combine(executingAssemblyFilePath, "BMC.Resources.dll");
            Assembly resourceAssembly = Assembly.LoadFile(resourceFilePath);
            BMC.Common.ResourceExtensions.RegisterResource("BMC.Resources.MessageResources", (int)AppResourceTypes.MessageResource, resourceAssembly);
            BMC.Common.ResourceExtensions.RegisterResource("BMC.Resources.TextResources", resourceAssembly);
            #endregion

            LogManager.WriteLog("Entering Enterprise Server Configuration EXE", LogManager.enumLogLevel.Debug);
            BMCRegistryHelper.ActiveInstallationType = BMCCategorizedInstallationTypes.Enterprise;
            LogManager.WriteLog("BMCRegistryHelper.InstallationType is :" + BMCRegistryHelper.InstallationType, LogManager.enumLogLevel.Debug);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}