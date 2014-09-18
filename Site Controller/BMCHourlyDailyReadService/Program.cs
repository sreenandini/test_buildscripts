using BMC.Common.LogManagement;
using BMC.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Windows.Forms;

namespace BMC
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            LogManager.WriteLog("EnteringHourly service EXE", LogManager.enumLogLevel.Debug);
            BMCRegistryHelper.ActiveInstallationType = BMCCategorizedInstallationTypes.Exchange;
            LogManager.WriteLog("BMCRegistryHelper.InstallationType is :" + BMCRegistryHelper.InstallationType, LogManager.enumLogLevel.Debug);
            #if (DEBUG)
            {
              Application.Run(new BMC.HourlyDailyReadJobs.TestForm1());
              //HourlyDailyService service = new HourlyDailyService();
              //service.RunService();
            }
#else
            {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
            { 
                new HourlyDailyService() 
            };
            ServiceBase.Run(ServicesToRun);
            }
#endif
        }
    }
}
