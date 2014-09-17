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
    #region ExMonServer4MonClientProxy
    public class ExMonServer4MonClientProxy
        : WcfClientChannelListener<IExMonServer4MonClient, IExMonServer4MonClientCallback>, 
        IExMonServer4MonClient
    {
        #region Constructors
        internal ExMonServer4MonClientProxy(Binding clientBinding, string address)
            : base(clientBinding, address, ExCommsMessageKnownTypeFactory.KnownTypes) { }

        internal ExMonServer4MonClientProxy(IExMonServer4MonClient innerChannel)
            : base(innerChannel) { }

        internal ExMonServer4MonClientProxy(IExMonServer4MonClientCallback callbackInstance, Binding clientBinding, string address, IEnumerable<Type> knownTypes)
            : base(callbackInstance, clientBinding, address, knownTypes) { }

        internal ExMonServer4MonClientProxy(IExMonServer4MonClient innerChannel, IExMonServer4MonClientCallback callbackInstance)
            : base(innerChannel, callbackInstance) { }
        #endregion

        #region Methods
        public void Subscribe(DTO.Monitor.ExMonServer4MonClientCallbackTypes callbackType, SubscribeRequestEntity request)
        {
            this.InvokeMethod((i) => i.Subscribe(callbackType, request));
        }

        public void Unsubscribe(DTO.Monitor.ExMonServer4MonClientCallbackTypes callbackType, UnsubscribeRequestEntity request)
        {
            this.InvokeMethod((i) => i.Unsubscribe(callbackType, request));
        }

        public bool ProcessG2HMessage(DTO.Monitor.MonMsg_G2H request)
        {
            return this.InvokeMethod((i) => i.ProcessG2HMessage(request));
        }

        public bool ProcessH2GMessage(MonMsg_H2G request)
        {
            return this.InvokeMethod((i) => i.ProcessH2GMessage(request));
        }

        public MonMsg_G2H ProcessH2GMessageSync(MonMsg_H2G request)
        {
            return this.InvokeMethod((i) => i.ProcessH2GMessageSync(request));
        }

        public void Subscribe() { }

        public void Unsubscribe() { }
        #endregion
    }
    #endregion

    #region ExMonServer4MonClientCallbackProxy

    public class ExMonServer4MonClientCallbackProxy
        : ExCommsCallbackServiceChannelFactory<IExMonServer4MonClient, IExMonServer4MonClientCallback>
    {
        private readonly ExMonServer4MonClientCallbackTypes _callbackType = ExMonServer4MonClientCallbackTypes.ProcessG2HMessage;
        private readonly SubscribeRequestEntity _subscribeEntity = new SubscribeRequestEntity();
        private readonly UnsubscribeRequestEntity _unsubscribeEntity = new UnsubscribeRequestEntity();

        static ExMonServer4MonClientCallbackProxy() { }

        internal ExMonServer4MonClientCallbackProxy(IExecutorService executorService, ExMonServer4MonClientCallbackTypes callbackType,
                                                IExMonServer4MonClientCallback callbackInstance, int timeoutInMilliseconds, WaitHandle canListen)
            : base(executorService, callbackInstance, timeoutInMilliseconds, canListen)
        {
            _callbackType = callbackType;
        }

        protected override WcfClientChannel<IExMonServer4MonClient> CreateClient()
        {
            return ExMonServer4MonClientProxyFactory.Get(_callbackInstance);
        }

        protected override void SubscribeInternal(WcfClientChannel<IExMonServer4MonClient> client)
        {
            ExMonServer4MonClientProxy proxy = client as ExMonServer4MonClientProxy;
            proxy.Subscribe(_callbackType, FillSubscriptionEntity(_subscribeEntity));
        }

        protected override void UnsubscribeInternal(WcfClientChannel<IExMonServer4MonClient> client)
        {
            ExMonServer4MonClientProxy proxy = client as ExMonServer4MonClientProxy;
            proxy.Unsubscribe(_callbackType, FillSubscriptionEntity(_unsubscribeEntity));
        }
    }
    #endregion

    #region ExMonServer4MonClientProxyFactory
    public static class ExMonServer4MonClientProxyFactory
    {
        private class DummyCallback
            : IExMonServer4MonClientCallback
        {
            public bool ProcessG2HMessage(MonMsg_G2H request)
            {
                return true;
            }
        }

        private static readonly DummyCallback _callback = new DummyCallback();

        static ExMonServer4MonClientProxyFactory() { }

        public static ExMonServer4MonClientProxy Get()
        {
            return Get(_callback);
        }

        internal static ExMonServer4MonClientProxy Get(IExMonServer4MonClientCallback callbackInstance)
        {
            using (ILogMethod method = Log.LogMethod("ExMonServer4MonClientProxyFactory", "Get"))
            {
                ExMonServer4MonClientProxy result = default(ExMonServer4MonClientProxy);

                try
                {
                    result = ExCommsGenericProxy.GetService<ExMonServer4MonClientProxy, IExMonServer4MonClient>(
                            (i) =>
                            {
                                if (callbackInstance != null)
                                    return new ExMonServer4MonClientProxy(i, callbackInstance);
                                else
                                    return new ExMonServer4MonClientProxy(i);
                            },
                            (b, u) =>
                            {
                                if (callbackInstance != null)
                                    return new ExMonServer4MonClientProxy(callbackInstance, b, u, ExCommsMessageKnownTypeFactory.KnownTypes);
                                else
                                    return new ExMonServer4MonClientProxy(b, u);
                            });
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }

                return result;
            }
        }

        public static ExMonServer4MonClientCallbackProxy Get(IExecutorService executorService,
                                                        ExMonServer4MonClientCallbackTypes callbackType,
                                                        IExMonServer4MonClientCallback callbackInstance,
                                                        int timeoutInMilliseconds, WaitHandle canListen)
        {
            using (ILogMethod method = Log.LogMethod("ExMonServer4MonClientProxyFactory", "Get"))
            {
                ExMonServer4MonClientCallbackProxy result = default(ExMonServer4MonClientCallbackProxy);

                try
                {
                    result = new ExMonServer4MonClientCallbackProxy(executorService, callbackType, callbackInstance, timeoutInMilliseconds, canListen);
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
