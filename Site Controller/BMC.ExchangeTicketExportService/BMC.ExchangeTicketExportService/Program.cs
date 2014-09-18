using BMC.Common.LogManagement;
using BMC.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;


namespace BMC.ExchangeTicketExportService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
	    LogManager.WriteLog("Entering Ticketing Service EXE", LogManager.enumLogLevel.Debug);
                BMCRegistryHelper.ActiveInstallationType = BMCCategorizedInstallationTypes.Exchange;
                LogManager.WriteLog("BMCRegistryHelper.InstallationType is :" + BMCRegistryHelper.InstallationType, LogManager.enumLogLevel.Debug);
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
			{ 
				new ExchangeTicketExportService() 
			};
            ServiceBase.Run(ServicesToRun);
        }
    }
}
