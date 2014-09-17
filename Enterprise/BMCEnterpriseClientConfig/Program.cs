using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BMC.Common.Utilities;
using BMC.Common.LogManagement;
using System.Reflection;
using System.IO;

namespace BMCEnterpriseClientConfig
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
	    	LogManager.WriteLog("Entering Enterprise Configuration EXE", LogManager.enumLogLevel.Debug);
            BMCRegistryHelper.ActiveInstallationType = BMCCategorizedInstallationTypes.Enterprise;
            LogManager.WriteLog("BMCRegistryHelper.InstallationType is :" + BMCRegistryHelper.InstallationType, LogManager.enumLogLevel.Debug);

            /* To Add the Resource file into memory of executing application */
            #region Resource File
            string executingAssemblyFilePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string resourceFilePath = System.IO.Path.Combine(executingAssemblyFilePath, "BMC.Resources.dll");
            Assembly resourceAssembly = Assembly.LoadFile(resourceFilePath);
            BMC.Common.ResourceExtensions.RegisterResource("BMC.Resources.MessageResources", (int)AppResourceTypes.MessageResource, resourceAssembly);
            BMC.Common.ResourceExtensions.RegisterResource("BMC.Resources.TextResources", resourceAssembly);
            #endregion

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmEnterpriseConfig());
        }
    }
}