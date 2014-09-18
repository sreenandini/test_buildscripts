using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceProcess;
using DataXChangeEndPointService.Service;
using BMC.Common.Utilities;
using BMC.Common.LogManagement;

namespace DataXChangeEndPointService
{
    static class Program
    {
        static void Main(string[] args)
        {
            //ServiceHostInvoker invoker = new ServiceHostInvoker();
            //invoker.Start();

            //Console.WriteLine("Exit?");
            //Console.ReadLine();
            //invoker.Stop();
            LogManager.WriteLog("Entering Data Export To Site EXE", LogManager.enumLogLevel.Debug);
            BMCRegistryHelper.ActiveInstallationType = BMCCategorizedInstallationTypes.Exchange;
            LogManager.WriteLog("BMCRegistryHelper.InstallationType is :" + BMCRegistryHelper.InstallationType, LogManager.enumLogLevel.Debug);

            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
            { 
                new ServiceHostInvoker() 
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
