﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using BMC.Common.Utilities;
using BMC.Common.LogManagement;

namespace BMC.MonitoringService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
	        LogManager.WriteLog("Entering Monitering Service EXE", LogManager.enumLogLevel.Debug);
            BMCRegistryHelper.ActiveInstallationType = BMCCategorizedInstallationTypes.Enterprise;
            LogManager.WriteLog("BMCRegistryHelper.InstallationType is :"+BMCRegistryHelper.InstallationType, LogManager.enumLogLevel.Debug);
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
			{ 
				new BMCMonitoringService() 
			};
            ServiceBase.Run(ServicesToRun);
        }
    }
}
