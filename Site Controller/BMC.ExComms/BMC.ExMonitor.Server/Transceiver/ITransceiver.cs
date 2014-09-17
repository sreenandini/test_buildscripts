using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib;
using BMC.CoreLib.Concurrent;
using BMC.ExComms.Contracts.DTO.Monitor;
using BMC.ExComms.Contracts.Interfaces;

namespace BMC.ExMonitor.Server.Transceiver
{
    public interface IExMonitorTransceiver : IDisposable
    {
        bool ProcessG2HMessage(MonMsg_G2H request);
        bool ProcessH2GMessage(MonMsg_H2G request);
    }

    public static class ExMonitorTransceiverFactory
    {
        private static IExMonitorTransceiver _instance = null;
        private static readonly object _instanceLock = new object();

        internal static IExMonitorTransceiver RegisterAndGet(IExecutorService executorService, ExMonitorServerImpl monitorServer)
        {
            using (ILogMethod method = Log.LogMethod("", "Method"))
            {
                try
                {
                    if (_instance == null)
                    {
                        lock (_instanceLock)
                        {
                            if (_instance == null)
                            {
                                _instance = new MonitorTransceiver_InMemory(executorService, monitorServer);
                            }
                        }
                    }

                    return _instance;
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }

                return _instance;
            }
        }

        public static IExMonitorTransceiver Instance
        {
            get
            {
                return _instance;
            }
        }
    }
}
