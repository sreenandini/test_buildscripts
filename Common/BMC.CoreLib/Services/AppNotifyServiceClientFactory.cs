using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib.Concurrent;
using System.Threading;
using BMC.CoreLib.Diagnostics;
using System.ServiceModel;
using BMC.CoreLib.Collections;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace BMC.CoreLib.Services
{
    public class AppNotifyServiceClientFactory : ExecutorBase
    {
        private AppNotifyServiceClient _client = null;
        private object _callbackInstance = null;
        private string _remoteAddress = string.Empty;
        private int _timeoutInMilliseconds = 100;

        private bool _skipLogException = false;
        private IThreadSafeQueue<AppNotifyData> _queue = null;
        private Type[] _knownTypes = null;

        private AppNotifyServiceClientFactory(IExecutorService executorService, string remoteAddress,
            object callbackInstance, int timeoutInMilliseconds, Type[] knownTypes)
            : base(executorService)
        {
            _callbackInstance = callbackInstance;
            _remoteAddress = remoteAddress;
            _timeoutInMilliseconds = (timeoutInMilliseconds < 100 ? 100 : timeoutInMilliseconds);
            _knownTypes = knownTypes;
            this.InitiateClient();
            Extensions.CreateThreadAndStart(new ThreadStart(this.OnListen), "AppNotifyServiceClientFactory_OnListen_");
        }

        public static AppNotifyServiceClientFactory GetFactory<T>(IExecutorService executorService, string remoteAddress,
            T callbackInstance, int timeoutInMilliseconds)
            where T : IAppNotifyServiceCallback
        {
            return new AppNotifyServiceClientFactory(executorService, remoteAddress,
                new AppNotifyServiceClientCallback(executorService, callbackInstance), timeoutInMilliseconds, null);
        }

        public static AppNotifyServiceClientFactory GetFactory<T>(IExecutorService executorService, string remoteAddress,
            T callbackInstance, int timeoutInMilliseconds, Type[] knownTypes)
            where T : IAppNotifyServiceCallback
        {
            return new AppNotifyServiceClientFactory(executorService, remoteAddress,
                new AppNotifyServiceClientCallback(executorService, callbackInstance), timeoutInMilliseconds, knownTypes);
        }

        public bool SkipLogException
        {
            get { return _skipLogException; }
            set
            {
                if (_skipLogException != value)
                {
                    _skipLogException = value;
                    if (_client != null)
                    {
                        try
                        {
                            _client.SkipLogException = value;
                        }
                        catch { }
                    }
                }
            }
        }

        private void InitiateClient()
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "InitiateClient");

            try
            {
                if (_client == null ||
                    (_client.State != System.ServiceModel.CommunicationState.Opened))
                {
                    if (this.ExecutorService.IsShutdown) return;

                    if (_client != null)
                    {
                        _client.Unsubscribe();
                        try
                        {
                            if (_client.State == System.ServiceModel.CommunicationState.Opened)
                            {
                                _client.Close();
                            }
                        }
                        catch { }
                        _client = null;
                    }
                    _client = new AppNotifyServiceClient(new InstanceContext(_callbackInstance), _remoteAddress);
                    _client.Endpoint.Behaviors.Add(new AppNotifyServiceClientBehavior(_knownTypes));
                    _client.SkipLogException = SkipLogException;
                    _client.Subscribe();
                }
            }
            catch (Exception ex)
            {
                if (!this.SkipLogException)
                {
                    Log.Exception(PROC, ex);
                }
                _client = null;
            }
        }

        private void OnListen()
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "OnBroadcastMessages");

            try
            {
                while (!this.ExecutorService.WaitForShutdown(_timeoutInMilliseconds))
                {
                    this.InitiateClient();
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
            finally
            {
                _client.Unsubscribe();
                this.Shutdown();
            }
        }
    }

    public class AppNotifyServiceClientBehavior : IEndpointBehavior
    {
        private Type[] _knownTypes = null;

        public AppNotifyServiceClientBehavior(Type[] knownTypes)
        {
            _knownTypes = knownTypes;
        }

        #region IEndpointBehavior Members

        public void AddBindingParameters(ServiceEndpoint endpoint, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {

        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.ClientRuntime clientRuntime)
        {
            ModuleProc PROC = new ModuleProc("AppNotifyServiceClientBehavior", "ApplyClientBehavior");

            try
            {
                if (_knownTypes == null) return;
                foreach (OperationDescription od in endpoint.Contract.Operations)
                {
                    foreach (Type knownType in _knownTypes)
                    {
                        if (od.KnownTypes.IndexOf(knownType) == -1)
                        {
                            od.KnownTypes.Add(knownType);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.EndpointDispatcher endpointDispatcher)
        {

        }

        public void Validate(ServiceEndpoint endpoint)
        {

        }

        #endregion
    }
}

