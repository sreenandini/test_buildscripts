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
    #region ExMonServer4MonProcessorProxy
    public class ExMonServer4MonProcessorProxy
        : WcfClientChannelListener<IExMonServer4MonProcessor, IExMonServer4MonProcessorCallback>, IExMonServer4MonProcessor
    {
        #region Constructors
        internal ExMonServer4MonProcessorProxy(Binding clientBinding, string address)
            : base(clientBinding, address, ExCommsMessageKnownTypeFactory.KnownTypes) { }

        internal ExMonServer4MonProcessorProxy(IExMonServer4MonProcessor innerChannel)
            : base(innerChannel) { }

        internal ExMonServer4MonProcessorProxy(IExMonServer4MonProcessorCallback callbackInstance, Binding clientBinding, string address, IEnumerable<Type> knownTypes)
            : base(callbackInstance, clientBinding, address, knownTypes) { }

        internal ExMonServer4MonProcessorProxy(IExMonServer4MonProcessor innerChannel, IExMonServer4MonProcessorCallback callbackInstance)
            : base(innerChannel, callbackInstance) { }
        #endregion

        #region Methods
        public void Subscribe(DTO.Monitor.ExMonServer4MonProcessorCallbackTypes callbackType, SubscribeRequestEntity request)
        {
            this.InvokeMethod((i) => i.Subscribe(callbackType, request));
        }

        public void Unsubscribe(DTO.Monitor.ExMonServer4MonProcessorCallbackTypes callbackType, UnsubscribeRequestEntity request)
        {
            this.InvokeMethod((i) => i.Unsubscribe(callbackType, request));
        }

        public bool ProcessH2GMessage(MonMsg_H2G request)
        {
            return this.InvokeMethod((i) => i.ProcessH2GMessage(request));
        }

        public void Subscribe() { }

        public void Unsubscribe() { }
        #endregion
    }
    #endregion

    #region ExMonServer4MonProcessorCallbackProxy

    public class ExMonServer4MonProcessorCallbackProxy
        : ExCommsCallbackServiceChannelFactory<IExMonServer4MonProcessor, IExMonServer4MonProcessorCallback>
    {
        private readonly ExMonServer4MonProcessorCallbackTypes _callbackType = ExMonServer4MonProcessorCallbackTypes.ProcessG2HMessage;
        private readonly SubscribeRequestEntity _subscribeEntity = new SubscribeRequestEntity();
        private readonly UnsubscribeRequestEntity _unsubscribeEntity = new UnsubscribeRequestEntity();

        static ExMonServer4MonProcessorCallbackProxy() { }

        internal ExMonServer4MonProcessorCallbackProxy(IExecutorService executorService, ExMonServer4MonProcessorCallbackTypes callbackType,
                                                IExMonServer4MonProcessorCallback callbackInstance, int timeoutInMilliseconds, WaitHandle canListen)
            : base(executorService, callbackInstance, timeoutInMilliseconds, canListen)
        {
            _callbackType = callbackType;
        }

        protected override WcfClientChannel<IExMonServer4MonProcessor> CreateClient()
        {
            return ExMonServer4MonProcessorProxyFactory.Get(_callbackInstance);
        }

        protected override void SubscribeInternal(WcfClientChannel<IExMonServer4MonProcessor> client)
        {
            ExMonServer4MonProcessorProxy proxy = client as ExMonServer4MonProcessorProxy;
            proxy.Subscribe(_callbackType, FillSubscriptionEntity(_subscribeEntity));
        }

        protected override void UnsubscribeInternal(WcfClientChannel<IExMonServer4MonProcessor> client)
        {
            ExMonServer4MonProcessorProxy proxy = client as ExMonServer4MonProcessorProxy;
            proxy.Unsubscribe(_callbackType, FillSubscriptionEntity(_unsubscribeEntity));
        }
    }
    #endregion

    #region ExMonServer4MonProcessorProxyFactory
    public static class ExMonServer4MonProcessorProxyFactory
    {
        static ExMonServer4MonProcessorProxyFactory() { }

        public static ExMonServer4MonProcessorProxy Get()
        {
            return Get(null);
        }

        public static ExMonServer4MonProcessorProxy Get(IExMonServer4MonProcessorCallback callbackInstance)
        {
            using (ILogMethod method = Log.LogMethod("ExMonServer4MonProcessorProxyFactory", "Get"))
            {
                ExMonServer4MonProcessorProxy result = default(ExMonServer4MonProcessorProxy);

                try
                {
                    result = ExCommsGenericProxy.GetService<ExMonServer4MonProcessorProxy, IExMonServer4MonProcessor>(
                            (i) =>
                            {
                                if (callbackInstance != null)
                                    return new ExMonServer4MonProcessorProxy(i, callbackInstance);
                                else
                                    return new ExMonServer4MonProcessorProxy(i);
                            },
                            (b, u) =>
                            {
                                if (callbackInstance != null)
                                    return new ExMonServer4MonProcessorProxy(callbackInstance, b, u, null);
                                else
                                    return new ExMonServer4MonProcessorProxy(b, u);
                            });
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }

                return result;
            }
        }

        public static ExMonServer4MonProcessorCallbackProxy Get(IExecutorService executorService, 
                                                        ExMonServer4MonProcessorCallbackTypes callbackType, 
                                                        IExMonServer4MonProcessorCallback callbackInstance, 
                                                        int timeoutInMilliseconds, WaitHandle canListen)
        {
            using (ILogMethod method = Log.LogMethod("ExMonServer4MonProcessorProxyFactory", "Get"))
            {
                ExMonServer4MonProcessorCallbackProxy result = default(ExMonServer4MonProcessorCallbackProxy);

                try
                {
                    result = new ExMonServer4MonProcessorCallbackProxy(executorService, callbackType, callbackInstance, timeoutInMilliseconds, canListen);
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
