using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Channels;
using BMC.CoreLib.Concurrent;
using BMC.CoreLib.Collections;
using System.Threading;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.ServiceModel.Dispatcher;
using BMC.CoreLib.WcfHelper.Hosting;

namespace BMC.CoreLib.Services
{
    public class AppNotifyServiceFactory : ExecutorBase
    {
        private ServiceHost _host = null;
        private object _lock = new object();

        private IThreadSafeQueue<AppNotifyData> _queue = null;
        private Type[] _knownTypes = null;

        private AppNotifyServiceFactory(IExecutorService executorService, string baseAddress, string path)
            : this(executorService, baseAddress, path, null) { }

        private AppNotifyServiceFactory(IExecutorService executorService, string baseAddress, string path, Type[] knownTypes)
            : base(executorService)
        {
            _queue = new BlockingBoundQueueUser<AppNotifyData>(executorService, -1);
            Extensions.CreateThreadAndStart(new ThreadStart(this.OnBroadcastMessages), "AppNotifyServiceFactory_OnBroadcastMessages_");
            _mreShutdown.Set();
            this.Start(baseAddress, path);
        }

        public static AppNotifyServiceFactory GetFactory(IExecutorService executorService, string baseAddress, string path)
        {
            return new AppNotifyServiceFactory(executorService, baseAddress, path);
        }

        public static AppNotifyServiceFactory GetFactory(IExecutorService executorService, string baseAddress, string path, Type[] knownTypes)
        {
            return new AppNotifyServiceFactory(executorService, baseAddress, path, knownTypes);
        }

        private void Start(string baseAddress, string path)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "Start");

            try
            {
                if (_host == null)
                {
                    lock (_lock)
                    {
                        if (_host == null)
                        {
                            // Binding
                            NetTcpBinding binding = AppNotifyServiceHelper.CreateBinding();

                            // Host
                            Uri uriTcp = new Uri(new Uri(Uri.UriSchemeNetTcp + "://" + baseAddress), path);
                            //Uri uriHttp = new Uri(new Uri(Uri.UriSchemeHttp + "://" + baseAddress), path);
                            _host = new WcfServiceHost(typeof(AppNotifyService), _knownTypes, new Uri[] { uriTcp });

                            // ServiceMetadataBehavior
                            ServiceMetadataBehavior bServiceMetadata = _host.Description.Behaviors.Find<ServiceMetadataBehavior>();
                            if (bServiceMetadata == null)
                            {
                                bServiceMetadata = new ServiceMetadataBehavior();
                                _host.Description.Behaviors.Add(bServiceMetadata);
                            }
                            bServiceMetadata.HttpGetEnabled = false;
                            //bServiceMetadata.HttpGetUrl = uriHttp;                            

                            // ServiceDebugBehavior
                            ServiceDebugBehavior bServiceDebug = _host.Description.Behaviors.Find<ServiceDebugBehavior>();
                            if (bServiceDebug == null)
                            {
                                bServiceDebug = new ServiceDebugBehavior();
                                _host.Description.Behaviors.Add(bServiceDebug);
                            }
                            bServiceDebug.IncludeExceptionDetailInFaults = false;

                            // service points
                            _host.AddServiceEndpoint(typeof(IAppNotifyService), binding, string.Empty);
                            _host.AddServiceEndpoint(typeof(IMetadataExchange), MetadataExchangeBindings.CreateMexTcpBinding(), "mex");
                        }
                    }
                }

                if (_host != null &&
                    _host.State == CommunicationState.Created)
                {
                    _host.Open();
                    Log.InfoV(PROC, "Service started at : {0}", _host.BaseAddresses[0]);
                    _mreShutdown.Reset();
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        private void Stop()
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "Stop");

            try
            {
                if (_host != null)
                {
                    lock (_lock)
                    {
                        if (_host != null)
                        {
                            _host.Close();
                            Log.InfoV(PROC, "Service stopped at : {0}", _host.BaseAddresses[0]);
                            _host = null;
                            this.Shutdown();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
                _host = null;
            }
        }

        public void PostMessage(AppNotifyData data)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "PostMessage");

            try
            {
                if (_host != null)
                {
                    _queue.Enqueue(data);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
                _host = null;
            }
        }

        private void OnBroadcastMessages()
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "OnBroadcastMessages");

            try
            {
                while (!this.ExecutorService.WaitForShutdown())
                {
                    try
                    {
                        AppNotifyData data = _queue.Dequeue();
                        AppNotifyService.OnNotifyData(data);
                    }
                    catch (Exception ex)
                    {
                        Log.Exception(PROC, ex);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
                _host = null;
            }
            finally
            {
                this.Stop();
            }
        }
    }
}
