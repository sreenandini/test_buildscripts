using System;
using System.ServiceModel;
using BMC.CoreLib.Concurrent;
using BMC.CoreLib.Services;
using BMC.ExComms.Contracts.Configuration;
using BMC.ExComms.Contracts.DTO.Monitor;
using BMC.ExComms.Contracts.Hosting;
using BMC.ExComms.Contracts.Interfaces;
using BMC.ExComms.Server.ExecutionSteps;
using BMC.ExComms.Server.Handlers;

namespace BMC.ExComms.Server
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    internal partial class ExCommsServerImpl
        : ListenerBase, 
        IExCommsServer,
        IExCommsServerImpl
    {
        internal IExCommsServerConfigStore _storeComm = ExCommsServerConfigStoreFactory.Store;
        private static ExCommsServerImpl _current = null;

        public ExCommsServerImpl(IExecutorService executorService)
            : base(executorService)
        {
            _current = this;
            this.Intialize();
        }

        IExecutorService IExCommsServerImpl.ExecutorService
        {
            get { return this.Executor; }
        }

        public static ExCommsServerImpl Current
        {
            get { return _current; }
        }

        protected override void InitializeInternal()
        {
            base.InitializeInternal();
            ExCommsExecutorFactory.Register(this.Executor);
            FFMsgHandlerFactory.Initialize(this.Executor, FFTgtHandlerDeviceTypes.GMU, () =>
            {
                return new FFMsgTransmitter();
            });
            this.Init_CallbackHelpers();
        }

        protected override bool StartInternal()
        {
            bool result = true;
            result &= this.Initialize_FreeformTransceiver();
            result &= this.Initialize_FreeformExecutor();
            return result;
        }

        /// <summary>
        /// Stops the listener.
        /// </summary>
        /// <returns>
        /// True if succeeded; otherwise false.
        /// </returns>
        protected override bool StopInternal()
        {
            bool result = true;
            this.Executor.Shutdown();
            result &= this.Uninitialize_FreeformTransceiver();
            result &= this.Uninitialize_FreeformExecutor();
            return result;
        }
    }
}
