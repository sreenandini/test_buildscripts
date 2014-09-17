using BMC.Common.ExceptionManagement;
using BMC.CoreLib;
using BMC.CoreLib.Collections;
using BMC.ExComms.Contracts.DTO.Freeform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.ExComms.Contracts.Hosting;

namespace BMC.ExComms.Server.Executor
{
    public static class FreeformExecutorFactory
    {
        private const string DYN_MODULE_NAME = "FreeformExecutorFactory";

        private static FreeformExecutorBase _executorPriority = null;
        private static FreeformExecutorBase _executorNonPriority = null;
        private static FreeformExecutorBase _executorGIM = null;

        private static int PriorityG2HThreadCount = 0;
        private static int PriorityH2GThreadCount = 0;

        private static int GIMG2HThreadCount = 0;
        private static int GIMH2GThreadCount = 0;

        private static int NonPriorityG2HThreadCount = 0;
        private static int NonPriorityH2GThreadCount = 0;

        private static readonly IDictionary<int, string> _htInstallationIp = new IntConcurrentDictionary<string>();
        private static readonly IDictionary<string, int> _htIpInstallation = new StringConcurrentDictionary<int>();

        static FreeformExecutorFactory()
        {
            Initialize();
        }

        private static void Initialize()
        {
            using (ILogMethod method = Log.LogMethod(DYN_MODULE_NAME, "Initialize"))
            {
                try
                {
                    //get from config file
                    PriorityG2HThreadCount = 10;
                    GIMG2HThreadCount = 1;
                    NonPriorityG2HThreadCount = 10;
                    PriorityH2GThreadCount = 10;
                    GIMH2GThreadCount = 1;
                    NonPriorityH2GThreadCount = 10;
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
            }
        }

        internal static void StartThreads()
        {
            using (ILogMethod method = Log.LogMethod(DYN_MODULE_NAME, "StartThreads"))
            {
                try
                {
                    _executorPriority = new FreeformExecutor_PriorityMessages();
                    _executorPriority.Start(PriorityG2HThreadCount, PriorityH2GThreadCount);

                    _executorGIM = new FreeformExecutor_GIMMessages();
                    _executorGIM.Start(GIMG2HThreadCount, GIMH2GThreadCount);

                    _executorNonPriority = new FreeformExecutor_NonPriorityMessages();
                    _executorNonPriority.Start(NonPriorityG2HThreadCount, NonPriorityH2GThreadCount);
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
            }
        }

        private static FreeformExecutorBase GetMessageType(FF_AppId_SessionIds SessionID)
        {
            switch (SessionID)
            {
                case FF_AppId_SessionIds.A1:
                    return _executorNonPriority;

                case FF_AppId_SessionIds.GIM:
                    return _executorGIM;

                default:
                    return _executorPriority;
            }
        }

        public static void ProcessMessage(FFMsg_G2H message)
        {
            using (ILogMethod method = Log.LogMethod(DYN_MODULE_NAME, "ProcessMessage(G2H)"))
            {
                try
                {
                    FreeformExecutorBase executor = GetMessageType(message.SessionID);
                    bool result = false;

                    if (message.SessionID == FF_AppId_SessionIds.GIM)
                    {
                        result = executor.ProcessMessage(message);
                    }
                    else
                    {
                        if (UpdateInstallationNo(ref message))
                        {
                            result = executor.ProcessMessage(message);
                        }
                    }

                    if (!result)
                    {
                        Log.Info("Freeform message received before GIM message.");
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
            }
        }

        public static void ProcessMessage(FFMsg_H2G message)
        {
            using (ILogMethod method = Log.LogMethod(DYN_MODULE_NAME, "ProcessMessage(H2G)"))
            {
                try
                {
                    FreeformExecutorBase executor = GetMessageType(message.SessionID);
                    if (message.SessionID == FF_AppId_SessionIds.GIM)
                    {
                        UpdateInstallationIP(message.IpAddress, message.InstallationNo);
                    }
                    else
                    {
                        UpdateIP(ref message);
                    }
                    executor.ProcessMessage(message);
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
            }
        }

        private static void UpdateInstallationIP(string ipAddress, int installationNo)
        {
            if (_htIpInstallation.ContainsKey(ipAddress))
            {
                _htIpInstallation[ipAddress] = installationNo;
            }
            else
            {
                _htIpInstallation.Add(new KeyValuePair<string, int>(ipAddress, installationNo));
            }

            if (_htInstallationIp.ContainsKey(installationNo))
            {
                _htInstallationIp[installationNo] = ipAddress;
            }
            else
            {
                _htInstallationIp.Add(new KeyValuePair<int, string>(installationNo, ipAddress));
            }
        }

        private static bool UpdateInstallationNo(ref FFMsg_G2H message)
        {
            using (ILogMethod method = Log.LogMethod(DYN_MODULE_NAME, "UpdateInstallationNo"))
            {
                bool retval = default(bool);

                try
                {
                    string ipAddress = message.IpAddress;
                    if (_htIpInstallation != null &&
                        _htIpInstallation.ContainsKey(ipAddress))
                    {
                        message.InstallationNo = _htIpInstallation[ipAddress];
                        retval = true;
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }

                return retval;
            }
        }

        private static bool UpdateIP(ref FFMsg_H2G message)
        {
            bool retval = false;
            if (_htInstallationIp != null &&
                _htInstallationIp.ContainsKey(message.InstallationNo))
            {
                message.IpAddress = _htInstallationIp[message.InstallationNo];
                retval = true;
            }
            return retval;
        }

        public static void StopThreads()
        {
            using (ILogMethod method = Log.LogMethod(DYN_MODULE_NAME, "StopThreads"))
            {
                try
                {
                    _executorPriority.Stop();
                    _executorGIM.Stop();
                    _executorNonPriority.Stop();
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
            }
        }
    }
}
