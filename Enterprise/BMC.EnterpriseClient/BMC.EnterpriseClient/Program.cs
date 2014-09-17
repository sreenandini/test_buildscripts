using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BMC.CoreLib;
using BMC.EnterpriseClient;
using BMC.CoreLib.Win32;
using BMC.Common.Utilities;
using BMC.Common.LogManagement;
using System.Reflection;
using System.IO;
using System.Threading;

namespace BMC.EnterpriseClient
{
    public enum AppResourceTypes
    {
        TextResource = 0,
        MessageResource = 1
    }

    static class Program
    {

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            LogManager.WriteLog("Entering Enterprise Client EXE", LogManager.enumLogLevel.Debug);
            BMCRegistryHelper.ActiveInstallationType = BMCCategorizedInstallationTypes.Enterprise;
            LogManager.WriteLog("BMCRegistryHelper.InstallationType is :" + BMCRegistryHelper.InstallationType, LogManager.enumLogLevel.Debug);
            //Extensions.AppTitle = TextResources.APP_TITLE;
            //Win32Extensions.AppTitle = Extensions.AppTitle;

            /* To Add the Resource file into memory of executing application */
            #region Resource File
            string executingAssemblyFilePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string resourceFilePath = System.IO.Path.Combine(executingAssemblyFilePath, "BMC.Resources.dll");
            Assembly resourceAssembly = Assembly.LoadFile(resourceFilePath);
            BMC.Common.ResourceExtensions.RegisterResource("BMC.Resources.MessageResources", (int)AppResourceTypes.MessageResource, resourceAssembly);
            BMC.Common.ResourceExtensions.RegisterResource("BMC.Resources.TextResources", resourceAssembly);
            #endregion

            Extensions.AppTitle = BMC.Common.ResourceExtensions.GetResourceTextByKey(null,1,"MSG_APP_TITLE");
            Win32Extensions.AppTitle = Extensions.AppTitle;

            using (EntryPoint ep = new AppEntryPoint(args))
            {
                ep.Run();
            }
            AppSettings.Current.Save();
        }
    }
}
