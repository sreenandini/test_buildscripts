using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib;
using BMC.CoreLib.Collections;
using BMC.CoreLib.Concurrent;
using BMC.ExComms.Contracts.DTO.Freeform;
using BMC.ExComms.Server.Handlers;

namespace BMC.ExComms.Server.Executors
{
    public abstract class FFExecutorBase
        : ExecutorServiceBase, IFFExecutor
    {
        private readonly IThreadPoolExecutor<FFMsg_G2H> _g2hExecutor_GIM = null;
        private readonly IThreadPoolExecutor<FFMsg_G2H> _g2hExecutor_Prio = null;
        private readonly IThreadPoolExecutor<FFMsg_G2H> _g2hExecutor_NonPrio = null;

        private readonly IThreadPoolExecutor<FFMsg_H2G> _h2gExecutor_GIM = null;
        private readonly IThreadPoolExecutor<FFMsg_H2G> _h2gExecutor_Prio = null;
        private readonly IThreadPoolExecutor<FFMsg_H2G> _h2gExecutor_NonPrio = null;

        private static readonly IDictionary<int, string> _mapEnrolledMachines = new IntConcurrentDictionary<string>();
        private static readonly IDictionary<int, string> _mapInstallationIP = new IntConcurrentDictionary<string>();
        private static readonly IDictionary<string, int> _mapIPInstallation = new StringConcurrentDictionary<int>();

        internal FFExecutorBase(IExecutorService executorService)
            : base(executorService)
        {
            // GIM (G2H)
            _g2hExecutor_GIM = ThreadPoolExecutorFactory.CreateThreadPool<FFMsg_G2H>(
                new ThreadPoolExecutorArg()
                {
                    ExecutorService = executorService,
                    KernelModeQueue = false,
                    PoolType = ThreadPoolType.AsyncTaskQueue,
                    ThreadCount = 1, // Configurable
                    FlushItemsBeforeClose = true,
                    ThreadQueueCount = -1,
                });
            _g2hExecutor_GIM.ProcessItem += OnProcessG2HMessageFromWorker;

            // GIM (H2G)
            _h2gExecutor_GIM = ThreadPoolExecutorFactory.CreateThreadPool<FFMsg_H2G>(
                new ThreadPoolExecutorArg()
                {
                    ExecutorService = executorService,
                    KernelModeQueue = false,
                    PoolType = ThreadPoolType.AsyncTaskQueue,
                    ThreadCount = 1, // Configurable
                    FlushItemsBeforeClose = true,
                    ThreadQueueCount = -1,
                });
            _h2gExecutor_GIM.ProcessItem += OnProcessH2GMessageFromWorker;

            // Non Priority (G2H)
            _g2hExecutor_NonPrio = ThreadPoolExecutorFactory.CreateThreadPool<FFMsg_G2H>(
                new ThreadPoolExecutorArg()
                {
                    ExecutorService = executorService,
                    KernelModeQueue = false,
                    PoolType = ThreadPoolType.AsyncTaskQueue,
                    ThreadCount = 20, // Configurable
                    FlushItemsBeforeClose = true,
                    ThreadQueueCount = -1,
                });
            _g2hExecutor_NonPrio.ProcessItem += OnProcessG2HMessageFromWorker;

            // Non Priority (H2G)
            _h2gExecutor_NonPrio = ThreadPoolExecutorFactory.CreateThreadPool<FFMsg_H2G>(
                new ThreadPoolExecutorArg()
                {
                    ExecutorService = executorService,
                    KernelModeQueue = false,
                    PoolType = ThreadPoolType.AsyncTaskQueue,
                    ThreadCount = 5, // Configurable
                    FlushItemsBeforeClose = true,
                    ThreadQueueCount = -1,
                });
            _h2gExecutor_NonPrio.ProcessItem += OnProcessH2GMessageFromWorker;

            // Priority (G2H)
            _g2hExecutor_Prio = ThreadPoolExecutorFactory.CreateThreadPool<FFMsg_G2H>(
                new ThreadPoolExecutorArg()
                {
                    ExecutorService = executorService,
                    KernelModeQueue = false,
                    PoolType = ThreadPoolType.AsyncTaskQueue,
                    ThreadCount = 20, // Configurable
                    FlushItemsBeforeClose = true,
                    ThreadQueueCount = -1,
                });
            _g2hExecutor_Prio.ProcessItem += OnProcessG2HMessageFromWorker;

            // Priority (H2G)
            _h2gExecutor_Prio = ThreadPoolExecutorFactory.CreateThreadPool<FFMsg_H2G>(
                new ThreadPoolExecutorArg()
                {
                    ExecutorService = executorService,
                    KernelModeQueue = false,
                    PoolType = ThreadPoolType.AsyncTaskQueue,
                    ThreadCount = 5, // Configurable
                    FlushItemsBeforeClose = true,
                    ThreadQueueCount = -1,
                });
            _h2gExecutor_Prio.ProcessItem += OnProcessH2GMessageFromWorker;
        }

        public virtual bool ProcessMessage(FFMsg_G2H request)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "ProcessG2HMessage"))
            {
                bool result = false;

                try
                {
                    // find the queue executor by session id
                    IThreadPoolExecutor<FFMsg_G2H> executor = this.GetExecutorG2H(request);
                    executor.QueueWorkerItem(request);
                    result = true;
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }

                return result;
            }
        }

        public bool ProcessMessage(FFMsg_H2G request)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "ProcessH2GMessage"))
            {
                bool result = false;

                try
                {
                    // find the queue executor by session id
                    IThreadPoolExecutor<FFMsg_H2G> executor = this.GetExecutorH2G(request);
                    executor.QueueWorkerItem(request);
                    result = true;
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }

                return result;
            }
        }

        protected virtual IThreadPoolExecutor<FFMsg_G2H> GetExecutorG2H(FFMsg_G2H msg)
        {
            switch (msg.SessionID)
            {
                case FF_AppId_SessionIds.A1:
                    return _g2hExecutor_NonPrio;

                case FF_AppId_SessionIds.GIM:
                    return _g2hExecutor_GIM;

                default:
                    return _g2hExecutor_Prio;
            }
        }

        protected virtual IThreadPoolExecutor<FFMsg_H2G> GetExecutorH2G(FFMsg_H2G msg)
        {
            switch (msg.SessionID)
            {
                case FF_AppId_SessionIds.A1:
                    return _h2gExecutor_NonPrio;

                case FF_AppId_SessionIds.GIM:
                    return _h2gExecutor_GIM;

                default:
                    return _h2gExecutor_Prio;
            }
        }

        void OnProcessG2HMessageFromWorker(FFMsg_G2H request)
        {
            bool proceed = true;

            if (request.SessionID != FF_AppId_SessionIds.GIM)
            {
                proceed = PopulateInstallationNo(request);
            }

            if (!proceed)
            {
                Log.Info("Freeform message received before GIM message.");
            }
            else
            {
                FFMsgHandlerFactory.Current.Execute(request);
            }
        }

        void OnProcessH2GMessageFromWorker(FFMsg_H2G request)
        {
            if (request.SessionID == FF_AppId_SessionIds.GIM)
            {
                UpdateInstallationIP(request.IpAddress, request.InstallationNo);
            }
            else
            {
                PopulateIP(request);
            }
            FFMsgHandlerFactory.Current.Execute(request);
        }

        private void UpdateInstallationIP(string ipAddress, int installationNo)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "UpdateInstallationIP"))
            {
                try
                {
                    this.AddInstallation(installationNo);

                    if (_mapIPInstallation.ContainsKey(ipAddress))
                    {
                        _mapIPInstallation[ipAddress] = installationNo;
                    }
                    else
                    {
                        _mapIPInstallation.Add(ipAddress, installationNo);
                    }

                    if (_mapInstallationIP.ContainsKey(installationNo))
                    {
                        _mapInstallationIP[installationNo] = ipAddress;
                    }
                    else
                    {
                        _mapInstallationIP.Add(installationNo, ipAddress);
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
            }
        }

        public bool AddInstallations(IEnumerable<int> installationNos)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "RemoveInstallationIP"))
            {
                try
                {
                    foreach (var installationNo in installationNos)
                    {
                        if (!_mapEnrolledMachines.ContainsKey(installationNo))
                        {
                            _mapEnrolledMachines.Add(installationNo, string.Empty);
                        }
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }

                return true;
            }
        }

        public bool AddInstallation(int installationNo)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "RemoveInstallationIP"))
            {
                try
                {
                    if (!_mapEnrolledMachines.ContainsKey(installationNo))
                    {
                        _mapEnrolledMachines.Add(installationNo, string.Empty);
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }

                return true;
            }
        }

        public bool RemoveInstallation(int installationNo)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "RemoveInstallationIP"))
            {
                try
                {
                    string ipAddress = string.Empty;
                    if (_mapInstallationIP.ContainsKey(installationNo))
                    {
                        ipAddress = _mapInstallationIP[installationNo];
                        _mapInstallationIP.Remove(installationNo);
                    }

                    if (_mapIPInstallation.ContainsKey(ipAddress))
                    {
                        _mapIPInstallation.Remove(ipAddress);
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }

                return true;
            }
        }

        private bool PopulateInstallationNo(FFMsg_G2H message)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "UpdateInstallationNo"))
            {
                bool retval = default(bool);

                try
                {
                    string ipAddress = message.IpAddress;
                    if (_mapIPInstallation != null &&
                        _mapIPInstallation.ContainsKey(ipAddress))
                    {
                        message.InstallationNo = _mapIPInstallation[ipAddress];
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

        private bool PopulateIP(FFMsg_H2G message)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "UpdateInstallationNo"))
            {
                bool retval = default(bool);

                try
                {
                    if (_mapInstallationIP != null &&
                        _mapInstallationIP.ContainsKey(message.InstallationNo))
                    {
                        message.IpAddress = _mapInstallationIP[message.InstallationNo];
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
    }
}
