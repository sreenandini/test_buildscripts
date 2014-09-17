using System.Collections.Generic;
using System.ServiceProcess;
using System.Text;
using BMC.Common.Utilities;
using BMC.Common.LogManagement;

namespace SiteStatusService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;

            // More than one user Service may run within the same process. To add
            // another service to this process, change the following line to
            // create a second service object. For example,
            //
            //   ServicesToRun = new ServiceBase[] {new Service1(), new MySecondUserService()};
            //
            LogManager.WriteLog("Entering Guardian Service", LogManager.enumLogLevel.Debug);
            BMCRegistryHelper.ActiveInstallationType = BMCCategorizedInstallationTypes.Exchange;
            LogManager.WriteLog("BMCRegistryHelper.InstallationType is :" + BMCRegistryHelper.InstallationType, LogManager.enumLogLevel.Debug);
                        
            ServicesToRun = new ServiceBase[] { new SiteStats() };

            ServiceBase.Run(ServicesToRun);
        }
    }
}