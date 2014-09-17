using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using BMC.CoreLib;
using BMC.CoreLib.Collections;
using BMC.CoreLib.Concurrent;
using BMC.CoreLib.Services;
using BMC.CoreLib.WcfHelper.Hosting;
using BMC.ExComms.Contracts.Configuration;
using BMC.ExComms.Contracts.DTO.Monitor;
using BMC.ExComms.Contracts.Hosting;
using BMC.ExComms.Contracts.Interfaces;
using BMC.ExMonitor.Server.Handlers;
using BMC.ExMonitor.Server.Transceiver;

namespace BMC.ExMonitor.Server
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    internal partial class ExMonitorServerImpl
        : ListenerBase, IExMonitorServerImpl
    {
        private readonly IExMonitorServerConfigStore _configStore = ExMonitorServerConfigStoreFactory.Store;

        public ExMonitorServerImpl(IExecutorService executorService)
            : base(executorService)
        {
            Current = this;
            this.Initialize();
        }

        private void Initialize()
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "Initialize"))
            {
                try
                {
                    MonitorHandlerFactory.Initialize(this.Executor);
                    this.InitCommsServerCallbacks();
                    this.InitMonProcessorCallbacks();
                    this.InitMonitorProcessorProxyHelper();
                    this.InitTransceiver();
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
            }
        }

        protected override bool StartInternal()
        {
            return true;
        }

        protected override bool StopInternal()
        {
            return true;
        }

        public void Subscribe() { }

        public void Unsubscribe() { }

        public static ExMonitorServerImpl Current { get; private set; }

        bool IExMonitorServerImpl.ProcessH2GMessage(MonMsg_H2G request)
        {
            return ((IExMonServer4MonProcessor)this).ProcessH2GMessage(request);
        }

        IExecutorService IExMonitorServerImpl.ExecutorService
        {
            get { return this.Executor; }
        }
    }
}
