using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib;
using BMC.CoreLib.Collections;
using BMC.CoreLib.WcfHelper.Hosting;
using BMC.ExComms.Contracts.DTO.Monitor;
using BMC.ExComms.Contracts.Hosting;
using BMC.ExComms.Contracts.Interfaces;

namespace BMC.ExMonitor.Server
{
    internal partial class ExMonitorServerImpl
        : IExMonServer4MonProcessor
    {
        private IDictionary<string, IExMonServer4MonProcessorCallback> _monProcessorCallbacks = null;
        private WcfCallbackServerHelper<IExMonServer4MonProcessorCallback> _monProcessorCallbackHelper = null;
        internal bool _isMonProcessorStandalone = false;

        private void InitMonProcessorCallbacks()
        {
            if (ExCommsHostingModuleTypeHelper.Current.HasMonitorServer4MonProcessor &&
                !ExCommsHostingModuleTypeHelper.Current.HasMonitorServer4CommsServer)
            {
                _isMonProcessorStandalone = true;
                _monProcessorCallbacks = new StringConcurrentDictionary<IExMonServer4MonProcessorCallback>();
                _monProcessorCallbackHelper =
                    new WcfCallbackServerHelper<IExMonServer4MonProcessorCallback>(this.Executor,
                        _configStore.LogClients, true);
                _monProcessorCallbackHelper.AfterSubscribed += OnCommServerCallbackHelper_AfterSubscribed;
                _monProcessorCallbackHelper.AfterUnsubscribed += OnCommServerCallbackHelper_AfterUnsubscribed;
            }
        }

        void IExMonServer4MonProcessor.Subscribe(ExComms.Contracts.DTO.Monitor.ExMonServer4MonProcessorCallbackTypes callbackType, ExComms.Contracts.DTO.SubscribeRequestEntity request)
        {
            _commsServerCallbackHelper.Subscribe(new ExMonServer4MonProcessorSubscribeEntity()
            {
                CallbackType = callbackType,
                Entity = request,
            });
        }

        void OnCommServerCallbackHelper_AfterSubscribed(IExMonServer4MonProcessorCallback callback, object state)
        {
            ExCommsServerSubscribeEntityBase callbackEntity = state as ExCommsServerSubscribeEntityBase;
            if (callbackEntity != null)
            {
                string ipAddress = callbackEntity.Entity.IPAddress;
                if (!_monProcessorCallbacks.ContainsKey(ipAddress))
                {
                    _monProcessorCallbacks.Add(ipAddress, callback);
                }
            }
        }

        void IExMonServer4MonProcessor.Unsubscribe(ExComms.Contracts.DTO.Monitor.ExMonServer4MonProcessorCallbackTypes callbackType, ExComms.Contracts.DTO.UnsubscribeRequestEntity request)
        {
            _commsServerCallbackHelper.Unsubscribe(new ExMonServer4MonProcessorSubscribeEntity()
            {
                CallbackType = callbackType,
                Entity = request,
            });
        }

        void OnCommServerCallbackHelper_AfterUnsubscribed(IExMonServer4MonProcessorCallback callback, object state)
        {
            ExCommsServerSubscribeEntityBase callbackEntity = state as ExCommsServerSubscribeEntityBase;
            if (callbackEntity != null)
            {
                string ipAddress = callbackEntity.Entity.IPAddress;
                if (_monProcessorCallbacks.ContainsKey(ipAddress))
                {
                    _monProcessorCallbacks[ipAddress] = null;
                }
            }
        }

        bool IExMonServer4MonProcessor.ProcessH2GMessage(ExComms.Contracts.DTO.Monitor.MonMsg_H2G request)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "ProcessG2HMessage"))
            {
                bool result = default(bool);

                try
                {
                    result = ((IExMonServer4CommsServer2)this).ProcessH2GMessage(request);
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }

                return result;
            }
        }
    }
}
