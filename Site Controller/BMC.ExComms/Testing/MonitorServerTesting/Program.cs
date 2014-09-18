using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using BMC.CoreLib;
using BMC.CoreLib.Concurrent;
using BMC.CoreLib.IoC;
using BMC.CoreLib.Services;
using BMC.ExComms.Contracts.Hosting;
using BMC.ExMonitor.Server.Handlers;

namespace MonitorServerTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            Log.GlobalWriteToExternalLog += Log_GlobalWriteToExternalLog;
            IExecutorService exec = ExecutorServiceFactory.CreateExecutorService();
            var activators =
                MEFHelper.GetExportedValues<IExCommsServerHostFactoryActivator>();

            ExCommsServerHostFactory host = null;
            var activator = (from a in activators
                where a.ServerType == ExCommsHostingServerType.MonitorServer
                select a).FirstOrDefault();
            if (activator == null) return;

            ExCommsHostingModuleTypeHelper.Current.ModuleType = ExCommsHostingModuleType.MonitorServer4CommsServer;
            host = activator.Create(exec);
            host.Start();

            while (true)
            {
                Console.WriteLine(@"Press any key to continue...");
                string s = Console.ReadLine();
                if (s == "q") break;
            }
            
            exec.Shutdown();
            host.Stop();
            exec.AwaitTermination(TimeSpan.FromMinutes(2));

            Thread.Sleep(10000);
        }

        static void Log_GlobalWriteToExternalLog(string formattedMessage, BMC.CoreLib.Diagnostics.LogEntryType type, object extra)
        {
            Console.WriteLine(formattedMessage);
        }
    }
}
