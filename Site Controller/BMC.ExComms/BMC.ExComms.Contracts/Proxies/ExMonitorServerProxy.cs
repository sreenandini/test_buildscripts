using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.ServiceModel.Channels;
using System.Threading;
using BMC.CoreLib;
using BMC.CoreLib.Concurrent;
using BMC.CoreLib.Diagnostics;
using BMC.CoreLib.WcfHelper.Contracts;
using BMC.CoreLib.WcfHelper.Hosting;
using BMC.CoreLib.WcfHelper.Proxies;
using BMC.CoreLib.WcfHelper.Proxies.ServiceProxy;
using BMC.ExComms.Contracts.DTO;
using BMC.ExComms.Contracts.DTO.Monitor;
using BMC.ExComms.Contracts.Interfaces;

namespace BMC.ExComms.Contracts.Proxies
{
    #region ExMonServer4CommsServerProxy
    public class ExMonServer4CommsServerProxy
        : WcfClientChannelListener<IExMonServer4CommsServer, IExMonServer4CommsServerCallback>, 
        IExMonServer4CommsServer
    {
        #region Constructors
        internal ExMonServer4CommsServerProxy(Binding clientBinding, string address)
            : base(clientBinding, address, ExCommsMessageKnownTypeFactory.KnownTypes) { }

        internal ExMonServer4CommsServerProxy(IExMonServer4CommsServer innerChannel)
            : base(innerChannel) { }

        internal ExMonServer4CommsServerProxy(IExMonServer4CommsServerCallback callbackInstance, Binding clientBinding, string address, IEnumerable<Type> knownTypes)
            : base(callbackInstance, clientBinding, address, knownTypes) { }

        internal ExMonServer4CommsServerProxy(IExMonServer4CommsServer innerChannel, IExMonServer4CommsServerCallback callbackInstance)
            : base(innerChannel, callbackInstance) { }
        #endregion

        #region Methods
        public void Subscribe(DTO.Monitor.ExMonServer4CommsServerCallbackTypes callbackType, SubscribeRequestEntity request)
        {
            this.InvokeMethod((i) => i.Subscribe(callbackType, request));
        }

        public void Unsubscribe(DTO.Monitor.ExMonServer4CommsServerCallbackTypes callbackType, UnsubscribeRequestEntity request)
        {
            this.InvokeMethod((i) => i.Unsubscribe(callbackType, request));
        }

        public bool ProcessG2HMessage(DTO.Monitor.MonMsg_G2H request)
        {
            return this.InvokeMethod((i) =>
            {
                request.HostIpAddress = ProxyHelper.HostIpAddress;
                return i.ProcessG2HMessage(request);
            });
        }

        public void Subscribe() { }

        public void Unsubscribe() { }
        #endregion
    }
    #endregion

    #region ExMonServer4CommsServerCallbackProxy

    public class ExMonServer4CommsServerCallbackProxy
        : ExCommsCallbackServiceChannelFactory<IExMonServer4CommsServer, IExMonServer4CommsServerCallback>
    {
        private readonly ExMonServer4CommsServerCallbackTypes _callbackType = ExMonServer4CommsServerCallbackTypes.ProcessH2GMessage;
        private readonly SubscribeRequestEntity _subscribeEntity = new SubscribeRequestEntity();
        private readonly UnsubscribeRequestEntity _unsubscribeEntity = new UnsubscribeRequestEntity();

        static ExMonServer4CommsServerCallbackProxy() { }

        internal ExMonServer4CommsServerCallbackProxy(IExecutorService executorService, ExMonServer4CommsServerCallbackTypes callbackType,
                                                IExMonServer4CommsServerCallback callbackInstance, int timeoutInMilliseconds, WaitHandle canListen)
            : base(executorService, callbackInstance, timeoutInMilliseconds, canListen)
        {
            _callbackType = callbackType;
        }

        protected override WcfClientChannel<IExMonServer4CommsServer> CreateClient()
        {
            return ExMonServer4CommsServerProxyFactory.Get(_callbackInstance);
        }

        protected override void SubscribeInternal(WcfClientChannel<IExMonServer4CommsServer> client)
        {
            ExMonServer4CommsServerProxy proxy = client as ExMonServer4CommsServerProxy;
            if (proxy != null) proxy.Subscribe(_callbackType, FillSubscriptionEntity(_subscribeEntity));
        }

        protected override void UnsubscribeInternal(WcfClientChannel<IExMonServer4CommsServer> client)
        {
            ExMonServer4CommsServerProxy proxy = client as ExMonServer4CommsServerProxy;
            if (proxy != null) proxy.Unsubscribe(_callbackType, FillSubscriptionEntity(_unsubscribeEntity));
        }
    }
    #endregion

    #region ExMonServer4CommsServerProxyFactory
    public static class ExMonServer4CommsServerProxyFactory
    {
        static ExMonServer4CommsServerProxyFactory() { }

        public static ExMonServer4CommsServerProxy Get()
        {
            return Get(null);
        }

        public static ExMonServer4CommsServerProxy Get(IExMonServer4CommsServerCallback callbackInstance)
        {
            using (ILogMethod method = Log.LogMethod("ExMonServer4CommsServerProxyFactory", "Get"))
            {
                ExMonServer4CommsServerProxy result = default(ExMonServer4CommsServerProxy);
                Binding binding = ExCommServerWcfHelper.CreateTcpBinding();
                string uri = "net.tcp://lt-in224:8880/BMC/Exchange";

                try
                {
                    result = ExCommsGenericProxy.GetService<ExMonServer4CommsServerProxy, IExMonServer4CommsServer>(
                            (i) =>
                            {
                                if (callbackInstance != null)
                                    return new ExMonServer4CommsServerProxy(i, callbackInstance);
                                else
                                    return new ExMonServer4CommsServerProxy(i);
                            },
                            (b, u) =>
                            {
                                b = binding;
                                u = uri;

                                if (callbackInstance != null)
                                    return new ExMonServer4CommsServerProxy(callbackInstance, b, u, null);
                                else
                                    return new ExMonServer4CommsServerProxy(b, u);
                            });
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }

                return result;
            }
        }

        public static ExMonServer4CommsServerCallbackProxy Get(IExecutorService executorService,
                                                        ExMonServer4CommsServerCallbackTypes callbackType,
                                                        IExMonServer4CommsServerCallback callbackInstance,
                                                        int timeoutInMilliseconds, WaitHandle canListen)
        {
            using (ILogMethod method = Log.LogMethod("ExMonServer4CommsServerProxyFactory", "Get"))
            {
                ExMonServer4CommsServerCallbackProxy result = default(ExMonServer4CommsServerCallbackProxy);

                try
                {
                    result = new ExMonServer4CommsServerCallbackProxy(executorService, callbackType, callbackInstance, timeoutInMilliseconds, canListen);
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }

                return result;
            }
        }
    }
    #endregion
}
