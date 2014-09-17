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
using BMC.CoreLib.WcfHelper.Contracts;
using System.ServiceModel.Channels;
using BMC.CoreLib.WcfHelper.Hosting;

namespace BMC.CoreLib.WcfHelper.Proxies
{
    public delegate bool WcfCallbackInitiateClientCheckHandler(IExecutorService executorService);

    public delegate void WcfCallbackSubscribedHandler<T>(DuplexClientBase<T> client)
        where T : class;

    public delegate void WcfCallbackChannelSubscribedHandler<T>(WcfClientChannel<T> client)
        where T : class, IServiceContractBase;

    public abstract class WcfCallbackServiceClientFactory<T> : ExecutorBase
        where T : class
    {
        private DuplexClientBase<T> _client = null;
        private DuplexClientBase<T> _clientListen = null;
        private int _timeoutInMilliseconds = 100;
        private WaitHandle[] _waitHandles = null;

        private bool _skipLogException = false;

        public WcfCallbackServiceClientFactory(IExecutorService executorService, int timeoutInMilliseconds)
            : this(executorService, timeoutInMilliseconds, null) { }

        public WcfCallbackServiceClientFactory(IExecutorService executorService, int timeoutInMilliseconds, WaitHandle canListen)
            : base(executorService)
        {
            _timeoutInMilliseconds = (timeoutInMilliseconds < 100 ? 100 : timeoutInMilliseconds);
            _waitHandles = (executorService as IExecutorService2).CreateWaitHandles(canListen);
            Extensions.CreateThreadAndStart(new ThreadStart(this.OnListen), this.DYN_MODULE_NAME + "_OnListen_");
        }

        protected abstract DuplexClientBase<T> CreateClient();

        protected abstract void Subscribe(DuplexClientBase<T> client);

        protected abstract void Unsubscribe(DuplexClientBase<T> client);

        public event WcfCallbackSubscribedHandler<T> AfterSubcribed = null;

        public void OnAfterSubcribed(DuplexClientBase<T> client)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "OnAfterSubcribed");

            try
            {
                if (this.AfterSubcribed != null)
                {
                    this.AfterSubcribed(client);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        public event WcfCallbackSubscribedHandler<T> AfterUnsubcribed = null;

        public void OnAfterUnsubcribed(DuplexClientBase<T> client)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "OnAfterUnsubcribed");

            try
            {
                if (this.AfterUnsubcribed != null)
                {
                    this.AfterUnsubcribed(client);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        public void Subscribe(ref DuplexClientBase<T> client)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "Subscribe");

            try
            {
                if (client != null)
                {
                    this.Subscribe(client);
                    if (client.State == CommunicationState.Opened)
                    {
                        this.AfterSubcribed(client);
                    }
                }
            }
            catch (Exception ex)
            {
                if (!this.SkipLogException)
                {
                    Log.Exception(PROC, ex);
                }
            }
        }

        public void Unsubscribe(ref DuplexClientBase<T> client)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "Unsubscribe");

            try
            {
                if (client != null)
                {
                    this.Unsubscribe(client);
                    this.AfterUnsubcribed(client);
                }
            }
            catch (Exception ex)
            {
                if (!this.SkipLogException)
                {
                    Log.Exception(PROC, ex);
                }
            }
        }

        public bool SkipLogException
        {
            get { return _skipLogException; }
            set { _skipLogException = value; }
        }

        private DuplexClientBase<T> InitiateClient(ref DuplexClientBase<T> client, bool skipSubscribe)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "InitiateClient");

            try
            {
                if (client == null ||
                    (client.State != System.ServiceModel.CommunicationState.Opened))
                {
                    if (this.ExecutorService.IsShutdown) return null;

                    if (client != null)
                    {
                        if (!skipSubscribe) this.Unsubscribe(ref client);
                        try
                        {
                            if (client.State == CommunicationState.Opened)
                            {
                                client.Close();
                            }
                        }
                        catch { }
                        client = null;
                    }

                    try
                    {
                        client = this.CreateClient();
                    }
                    catch (Exception ex)
                    {
                        if (!this.SkipLogException)
                        {
                            Log.Exception(PROC, ex);
                        }
                        client = null;
                    }

                    if (client != null)
                    {
                        if (client is IWcfCallbackServiceClient)
                        {
                            ((IWcfCallbackServiceClient)client).SkipLogException = _skipLogException;
                        }
                        if (!skipSubscribe) this.Subscribe(ref client);
                    }
                }
            }
            catch (Exception ex)
            {
                if (!this.SkipLogException)
                {
                    Log.Exception(PROC, ex);
                }
                client = null;
            }

            return client;
        }

        public DuplexClientBase<T> InnerClient
        {
            get
            {
                return this.InitiateClient(ref _client, true);
            }
        }

        private void OnListen()
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "OnBroadcastMessages");

            try
            {
                IExecutorService executor = this.ExecutorService;
                bool canListen = (_waitHandles.Length > 1);

                do
                {
                    if (IsDisposed) break;

                    try
                    {
                        if (canListen)
                        {
                            int index = EventWaitHandle.WaitAny(_waitHandles);
                            if (index == 0) break;
                        }

                        this.InitiateClient(ref _clientListen, false);
                    }
                    catch (Exception ex)
                    {
                        Log.Exception(PROC, ex);
                    }
                } while (!executor.WaitForShutdown(_timeoutInMilliseconds));
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
            finally
            {
                this.Unsubscribe(ref _clientListen);
            }
        }

        protected override void DisposeManaged()
        {
            base.DisposeManaged();
        }
    }

    public abstract class WcfCallbackServiceChannelFactory<T> : ExecutorBase
        where T : class, IServiceContractBase
    {
        private WcfClientChannel<T> _client = null;
        private WcfClientChannel<T> _clientListen = null;
        private int _timeoutInMilliseconds = 100;
        private WaitHandle[] _waitHandles = null;

        private bool _skipLogException = false;

        public WcfCallbackServiceChannelFactory(IExecutorService executorService, int timeoutInMilliseconds)
            : this(executorService, timeoutInMilliseconds, null) { }

        public WcfCallbackServiceChannelFactory(IExecutorService executorService, int timeoutInMilliseconds, WaitHandle canListen)
            : base(executorService)
        {
            _timeoutInMilliseconds = (timeoutInMilliseconds < 100 ? 100 : timeoutInMilliseconds);
            _waitHandles = (executorService as IExecutorService2).CreateWaitHandles(canListen);
            Extensions.CreateThreadAndStart(new ThreadStart(this.OnListen), this.DYN_MODULE_NAME + "_OnListen_");
        }

        protected abstract WcfClientChannel<T> CreateClient();

        protected abstract void SubscribeInternal(WcfClientChannel<T> client);

        protected abstract void UnsubscribeInternal(WcfClientChannel<T> client);

        public event WcfCallbackChannelSubscribedHandler<T> AfterSubcribed = null;

        public void OnAfterSubcribed(WcfClientChannel<T> client)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "OnAfterSubcribed");

            try
            {
                if (this.AfterSubcribed != null)
                {
                    this.AfterSubcribed(client);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        public event WcfCallbackChannelSubscribedHandler<T> AfterUnsubcribed = null;

        public void OnAfterUnsubcribed(WcfClientChannel<T> client)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "OnAfterUnsubcribed");

            try
            {
                if (this.AfterUnsubcribed != null)
                {
                    this.AfterUnsubcribed(client);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        public void Subscribe(ref WcfClientChannel<T> client)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "Subscribe");

            try
            {
                if (client != null)
                {
                    this.SubscribeInternal(client);
                    if (client.IsChannelAvailable)
                    {
                        this.OnAfterSubcribed(client);
                    }
                }
            }
            catch (Exception ex)
            {
                if (!this.SkipLogException)
                {
                    Log.Exception(PROC, ex);
                }
            }
        }

        public void Unsubscribe(ref WcfClientChannel<T> client)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "Unsubscribe");

            try
            {
                if (client != null)
                {
                    this.UnsubscribeInternal(client);
                    this.OnAfterUnsubcribed(client);
                }
            }
            catch (Exception ex)
            {
                if (!this.SkipLogException)
                {
                    Log.Exception(PROC, ex);
                }
            }
        }

        public bool SkipLogException
        {
            get { return _skipLogException; }
            set { _skipLogException = value; }
        }

        private WcfClientChannel<T> InitiateClient(ref WcfClientChannel<T> client, bool skipSubscribe)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "InitiateClient");

            try
            {
                if (client == null ||
                    (!client.IsChannelAvailable))
                {
                    if (this.ExecutorService.IsShutdown) return null;

                    if (client != null)
                    {
                        if (!skipSubscribe) this.Unsubscribe(ref client);
                        try
                        {
                            if (client.IsChannelAvailable)
                            {
                                client.Dispose();
                            }
                        }
                        catch { }
                        client = null;
                    }

                    try
                    {
                        client = this.CreateClient();
                    }
                    catch { client = null; }

                    if (client != null)
                    {
                        if (client is IWcfCallbackServiceClient)
                        {
                            ((IWcfCallbackServiceClient)client).SkipLogException = _skipLogException;
                        }
                        if (!skipSubscribe) this.Subscribe(ref client);
                    }
                }
            }
            catch (Exception ex)
            {
                if (!this.SkipLogException)
                {
                    Log.Exception(PROC, ex);
                }
                client = null;
            }

            return client;
        }

        public WcfClientChannel<T> InnerClient
        {
            get
            {
                return this.InitiateClient(ref _client, true);
            }
        }

        private void OnListen()
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "OnBroadcastMessages");

            try
            {
                IExecutorService executor = this.ExecutorService;
                bool canListen = (_waitHandles.Length > 1);

                do
                {
                    if (IsDisposed) break;

                    try
                    {
                        if (canListen)
                        {
                            int index = EventWaitHandle.WaitAny(_waitHandles);
                            if (index == 0) break;
                        }

                        this.InitiateClient(ref _clientListen, false);
                    }
                    catch (Exception ex)
                    {
                        Log.Exception(PROC, ex);
                    }
                } while (!executor.WaitForShutdown(_timeoutInMilliseconds));
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
            finally
            {
                this.Unsubscribe(ref _clientListen);
            }
        }

        protected override void DisposeManaged()
        {
            base.DisposeManaged();
        }
    }
}

