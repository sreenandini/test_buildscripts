using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib.Concurrent;
using BMC.ExComms.Contracts.DTO;
using BMC.ExComms.Contracts.DTO.Monitor;
using BMC.ExComms.Contracts.Interfaces;
using BMC.ExComms.Contracts.Proxies;

namespace MonitorServerClientTesting
{
    class Program : IExMonServer4CommsServerCallback
    {
        private IExecutorService _exec = ExecutorServiceFactory.CreateExecutorService();

        static void Main(string[] args)
        {
            Program p = new Program();
            p.Test();

            Console.WriteLine(@"Exit?");
            Console.ReadLine();
        }

        public void Test()
        {
            ExMonServer4CommsServerCallbackProxy proxy = ExMonServer4CommsServerProxyFactory.Get(_exec, ExMonServer4CommsServerCallbackTypes.ProcessH2GMessage,
                this, 10000, null);
            ExMonServer4CommsServerProxy sendProxy = ExMonServer4CommsServerProxyFactory.Get(this);

            while (true)
            {
                Console.WriteLine(@"Press any key to continue...");
                string s = Console.ReadLine();
                if (s == "q") break;

                sendProxy.ProcessG2HMessage(new MonMsg_G2H()
                {
                    FaultSource = 100,
                    FaultType = 1,
                    InstallationNo = 11,
                    IpAddress = "192.168.2.10",
                });
            }
        }

        public bool ProcessH2GMessage(MonMsg_H2G request)
        {
            return false;
        }
    }
}
