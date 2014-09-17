using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib.Concurrent;
using BMC.ExComms.Contracts.Configuration;
using BMC.ExComms.Contracts.DTO.Freeform;
using BMC.ExComms.Server.Executor;
using BMC.ExComms.Server.Executors;

namespace BMC.ExComms.Server
{
    internal static class ExCommsExecutorFactory
    {
        private static int _executorType = 0;
        private static IFFExecutor _executor = null;

        static ExCommsExecutorFactory()
        {
            _executorType = ExCommsServerConfigStoreFactory.Store.FreeformExecutorType;
        }

        internal static void Register(IExecutorService executorService)
        {
            if (_executorType == 0)
                _executor = FFExecutorFactory.RegisterAndGet(executorService);
        }

        internal static bool ProcessMessage(FFMsg_G2H request)
        {
            if (_executorType == 0)
            {
                return _executor.ProcessMessage(request);
            }
            else
            {
                FreeformExecutorFactory.ProcessMessage(request);
                return true;
            }
        }

        internal static bool ProcessMessage(FFMsg_H2G request)
        {
            if (_executorType == 0)
            {
                return _executor.ProcessMessage(request);
            }
            else
            {
                FreeformExecutorFactory.ProcessMessage(request);
                return true;
            }
        }

        internal static bool AddInstallations(IEnumerable<int> installationNos)
        {
            if (_executorType == 0)
            {
                return _executor.AddInstallations(installationNos);
            }
            else
            {
                return true;
            }
        }

        internal static bool AddInstallation(int installationNo)
        {
            if (_executorType == 0)
            {
                return _executor.AddInstallation(installationNo);
            }
            else
            {
                return true;
            }
        }

        internal static bool RemoveInstallation(int installationNo)
        {
            if (_executorType == 0)
            {
                return _executor.RemoveInstallation(installationNo);
            }
            else
            {                
                return true;
            }
        }

        internal static void StartThreads()
        {
            if (_executorType == 1)
            {
                FreeformExecutorFactory.StartThreads();
            }
        }

        internal static void StopThreads()
        {
            if (_executorType == 1)
            {
                FreeformExecutorFactory.StopThreads();
            }
        }
    }
}
