using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib;
using BMC.CoreLib.Concurrent;
using BMC.ExComms.Contracts.DTO.Monitor;
using BMC.ExComms.Contracts.Hosting;
using BMC.ExComms.Contracts.Interfaces;

namespace BMC.ExMonitor.Server.Transceiver
{
    public abstract class ExMonitorTransceiverBase
        : ExecutorServiceBase, IExMonitorTransceiver
    {
        internal readonly ExMonitorServerImpl _monitorServer = null;
        private readonly bool _isMonProcessorStandalone = false;

        private readonly IThreadPoolExecutor<MonMsg_G2H> _g2hExecutor_GIM = null;
        private readonly IThreadPoolExecutor<MonMsg_G2H> _g2hExecutor_Prio = null;
        private readonly IThreadPoolExecutor<MonMsg_G2H> _g2hExecutor_NonPrio = null;

        private readonly IThreadPoolExecutor<MonMsg_H2G> _h2gExecutor_GIM = null;
        private readonly IThreadPoolExecutor<MonMsg_H2G> _h2gExecutor_Prio = null;
        private readonly IThreadPoolExecutor<MonMsg_H2G> _h2gExecutor_NonPrio = null;


        internal ExMonitorTransceiverBase(IExecutorService executorService, ExMonitorServerImpl monitorServer)
            : base(executorService)
        {
            _monitorServer = monitorServer;
            _isMonProcessorStandalone = _monitorServer._isMonProcessorStandalone;

            // GIM (G2H)
            _g2hExecutor_GIM = ThreadPoolExecutorFactory.CreateThreadPool<MonMsg_G2H>(
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
            _h2gExecutor_GIM = ThreadPoolExecutorFactory.CreateThreadPool<MonMsg_H2G>(
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
            _g2hExecutor_NonPrio = ThreadPoolExecutorFactory.CreateThreadPool<MonMsg_G2H>(
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
            _h2gExecutor_NonPrio = ThreadPoolExecutorFactory.CreateThreadPool<MonMsg_H2G>(
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
            _g2hExecutor_Prio = ThreadPoolExecutorFactory.CreateThreadPool<MonMsg_G2H>(
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
            _h2gExecutor_Prio = ThreadPoolExecutorFactory.CreateThreadPool<MonMsg_H2G>(
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

        public virtual bool ProcessG2HMessage(MonMsg_G2H request)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "ProcessG2HMessageInWorker"))
            {
                bool result = false;

                try
                {
                    // find the queue executor by fault source and type
                    IThreadPoolExecutor<MonMsg_G2H> executor = this.GetExecutorG2H(request);
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

        public bool ProcessH2GMessage(MonMsg_H2G request)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "ProcessH2GMessage"))
            {
                bool result = false;

                try
                {
                    // find the queue executor by fault source and type
                    IThreadPoolExecutor<MonMsg_H2G> executor = this.GetExecutorH2G(request);
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

        protected virtual IThreadPoolExecutor<MonMsg_G2H> GetExecutorG2H(IMonitorEntity_Msg msg)
        {
            return _g2hExecutor_NonPrio;
        }

        protected virtual IThreadPoolExecutor<MonMsg_H2G> GetExecutorH2G(IMonitorEntity_Msg msg)
        {
            return _h2gExecutor_NonPrio;
        }

        void OnProcessG2HMessageFromWorker(MonMsg_G2H request)
        {
            if (!_isMonProcessorStandalone)
            {
                ((IExMonServer4MonProcessorCallback) _monitorServer).ProcessG2HMessage(request);
            }
            else
            {
                // monitor server processor mappings should be coming here
            }
        }

        void OnProcessH2GMessageFromWorker(MonMsg_H2G request)
        {
            if (!_isMonProcessorStandalone)
            {
                ((IExMonServer4MonProcessor) _monitorServer).ProcessH2GMessage(request);
            }
            else
            {
                _monitorServer.MonitorProcessorProxy.ProcessH2GMessage(request);
            }
        }
    }
}
