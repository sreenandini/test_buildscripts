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
using BMC.ExComms.Contracts.DTO.Freeform;
using BMC.ExComms.Contracts.DTO.Monitor;
using BMC.ExComms.Contracts.Interfaces;

namespace BMC.ExComms.Contracts.Proxies
{
    #region ExCommsServerProxy
    public class ExCommsServerProxy
        : WcfClientChannelListener<IExCommsServer, IExCommsServerCallback>, 
        IExCommsServer
    {
        #region Constructors
        internal ExCommsServerProxy(Binding clientBinding, string address)
            : base(clientBinding, address, ExCommsMessageKnownTypeFactory.KnownTypes) { }

        internal ExCommsServerProxy(IExCommsServer innerChannel)
            : base(innerChannel) { }

        internal ExCommsServerProxy(IExCommsServerCallback callbackInstance, Binding clientBinding, string address, IEnumerable<Type> knownTypes)
            : base(callbackInstance, clientBinding, address, knownTypes) { }

        internal ExCommsServerProxy(IExCommsServer innerChannel, IExCommsServerCallback callbackInstance)
            : base(innerChannel, callbackInstance) { }
        #endregion

        #region Methods
        public void Subscribe(DTO.Freeform.ExCommsServerCallbackTypes callbackType, SubscribeRequestEntity request)
        {
            this.InvokeMethod((i) => i.Subscribe(callbackType, request));
        }

        public void Unsubscribe(DTO.Freeform.ExCommsServerCallbackTypes callbackType, UnsubscribeRequestEntity request)
        {
            this.InvokeMethod((i) => i.Unsubscribe(callbackType, request));
        }

        public void Subscribe() { }

        public void Unsubscribe() { }
        #endregion
    }
    #endregion

    #region ExCommsServerCallbackProxy

    public class ExCommsServerCallbackProxy
        : ExCommsCallbackServiceChannelFactory<IExCommsServer, IExCommsServerCallback>
    {
        private readonly ExCommsServerCallbackTypes _callbackType = ExCommsServerCallbackTypes.RawMessage;
        private readonly SubscribeRequestEntity _subscribeEntity = new SubscribeRequestEntity();
        private readonly UnsubscribeRequestEntity _unsubscribeEntity = new UnsubscribeRequestEntity();

        static ExCommsServerCallbackProxy() { }

        internal ExCommsServerCallbackProxy(IExecutorService executorService, ExCommsServerCallbackTypes callbackType,
                                                IExCommsServerCallback callbackInstance, int timeoutInMilliseconds, WaitHandle canListen)
            : base(executorService, callbackInstance, timeoutInMilliseconds, canListen)
        {
            _callbackType = callbackType;
        }

        protected override WcfClientChannel<IExCommsServer> CreateClient()
        {
            return ExCommsServerProxyFactory.Get(_callbackInstance);
        }

        protected override void SubscribeInternal(WcfClientChannel<IExCommsServer> client)
        {
            ExCommsServerProxy proxy = client as ExCommsServerProxy;
            proxy.Subscribe(_callbackType, FillSubscriptionEntity(_subscribeEntity));
        }

        protected override void UnsubscribeInternal(WcfClientChannel<IExCommsServer> client)
        {
            ExCommsServerProxy proxy = client as ExCommsServerProxy;
            proxy.Unsubscribe(_callbackType, FillSubscriptionEntity(_unsubscribeEntity));
        }
    }
    #endregion

    #region ExCommsServerProxyFactory
    public static class ExCommsServerProxyFactory
    {
        static ExCommsServerProxyFactory() { }

        public static ExCommsServerProxy Get()
        {
            return Get(null);
        }

        internal static ExCommsServerProxy Get(IExCommsServerCallback callbackInstance)
        {
            using (ILogMethod method = Log.LogMethod("ExCommsServerProxyFactory", "Get"))
            {
                ExCommsServerProxy result = default(ExCommsServerProxy);

                try
                {
                    result = ExCommsGenericProxy.GetService<ExCommsServerProxy, IExCommsServer>(
                            (i) =>
                            {
                                if (callbackInstance != null)
                                    return new ExCommsServerProxy(i, callbackInstance);
                                else
                                    return new ExCommsServerProxy(i);
                            },
                            (b, u) =>
                            {
                                if (callbackInstance != null)
                                    return new ExCommsServerProxy(callbackInstance, b, u, null);
                                else
                                    return new ExCommsServerProxy(b, u);
                            });
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }

                return result;
            }
        }

        public static ExCommsServerCallbackProxy Get(IExecutorService executorService,
                                                        ExCommsServerCallbackTypes callbackType,
                                                        IExCommsServerCallback callbackInstance,
                                                        int timeoutInMilliseconds, WaitHandle canListen)
        {
            using (ILogMethod method = Log.LogMethod("ExCommsServerProxyFactory", "Get"))
            {
                ExCommsServerCallbackProxy result = default(ExCommsServerCallbackProxy);

                try
                {
                    result = new ExCommsServerCallbackProxy(executorService, callbackType, callbackInstance, timeoutInMilliseconds, canListen);
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
