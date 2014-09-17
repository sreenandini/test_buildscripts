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
using BMC.ExComms.DataLayer.MSSQL;

namespace BMC.ExMonitor.Server
{
    internal partial class ExMonitorServerImpl
        : IExMonServer4CommsServer2
    {
        internal StringConcurrentDictionary<IExMonServer4CommsServerCallback> _commsServerCallbacks = null;
        internal StringConcurrentDictionary<string> _commsServersMapByIp = null;
        internal IntConcurrentDictionary<string> _commsServersMapByInsno = null;
        internal StringConcurrentDictionary<int> _mapIpInsNo = null;
        internal WcfCallbackServerHelper<IExMonServer4CommsServerCallback> _commsServerCallbackHelper = null;
        internal bool _isCommsServer = false;

        private void InitCommsServerCallbacks()
        {
            if (ExCommsHostingModuleTypeHelper.Current.HasMonitorServer4CommsServer)
            {
                _isCommsServer = true;
                _commsServersMapByIp = new StringConcurrentDictionary<string>();
                _commsServersMapByInsno = new IntConcurrentDictionary<string>();
                _mapIpInsNo = new StringConcurrentDictionary<int>();
                _commsServerCallbacks = new StringConcurrentDictionary<IExMonServer4CommsServerCallback>();

                _commsServerCallbackHelper = new WcfCallbackServerHelper<IExMonServer4CommsServerCallback>(this.Executor,
                    _configStore.LogClients, true);
                _commsServerCallbackHelper.AfterSubscribed += OnCommServerCallbackHelper_AfterSubscribed;
                _commsServerCallbackHelper.AfterUnsubscribed += OnCommServerCallbackHelper_AfterUnsubscribed;
            }
        }

        void IExMonServer4CommsServer.Subscribe(ExComms.Contracts.DTO.Monitor.ExMonServer4CommsServerCallbackTypes callbackType, ExComms.Contracts.DTO.SubscribeRequestEntity request)
        {
            _commsServerCallbackHelper.Subscribe(new ExMonServer4CommsServerSubscribeEntity()
            {
                CallbackType = callbackType,
                Entity = request,
            });
        }

        void OnCommServerCallbackHelper_AfterSubscribed(IExMonServer4CommsServerCallback callback, object state)
        {
            ExCommsServerSubscribeEntityBase callbackEntity = state as ExCommsServerSubscribeEntityBase;
            if (callbackEntity != null)
            {
                string ipAddress = callbackEntity.Entity.IPAddress;
                if (!_commsServerCallbacks.ContainsKey(ipAddress))
                {
                    _commsServerCallbacks.GetOrAdd(ipAddress, callback);
                }
            }
        }

        void IExMonServer4CommsServer.Unsubscribe(ExComms.Contracts.DTO.Monitor.ExMonServer4CommsServerCallbackTypes callbackType, ExComms.Contracts.DTO.UnsubscribeRequestEntity request)
        {
            _commsServerCallbackHelper.Unsubscribe(new ExMonServer4CommsServerSubscribeEntity()
            {
                CallbackType = callbackType,
                Entity = request,
            });
        }

        void OnCommServerCallbackHelper_AfterUnsubscribed(IExMonServer4CommsServerCallback callback, object state)
        {
            ExCommsServerSubscribeEntityBase callbackEntity = state as ExCommsServerSubscribeEntityBase;
            if (callbackEntity != null)
            {
                string ipAddress = callbackEntity.Entity.IPAddress;
                if (_commsServerCallbacks.ContainsKey(ipAddress))
                {
                    _commsServerCallbacks[ipAddress] = null;
                }
            }
        }

        bool IExMonServer4CommsServer.ProcessG2HMessage(ExComms.Contracts.DTO.Monitor.MonMsg_G2H request)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "ProcessG2HMessage"))
            {
                bool result = default(bool);

                try
                {
                    string ipAddress = request.IpAddress;
                    string hostAddress = request.HostIpAddress;

                    // comms server instance
                    if (!_commsServerCallbacks.ContainsKey(hostAddress))
                    {
                        _commsServerCallbacks.GetOrAdd(hostAddress, ExCommsServerHostFactoryActivatorFactory.Current.CommunicationServer);
                    }

                    // gmus associated with comms servers (by ip address)
                    if (!_commsServersMapByIp.ContainsKey(ipAddress))
                    {
                        _commsServersMapByIp.GetOrAdd(ipAddress, hostAddress);
                    }
                    else
                    {
                        _commsServersMapByIp[ipAddress] = hostAddress;
                    }

                    // get the installation no
                    if (_mapIpInsNo.ContainsKey(ipAddress))
                    {
                        request.InstallationNo = _mapIpInsNo[ipAddress];
                    }

                    // fill the request values by using installation no
                    if (request.InstallationNo > 0)
                    {
                        this.FillRequestValues(request);
                    }

                    // gmus associated with comms servers (by installation no)
                    this.UpdateCommsServerHostAddress(request.InstallationNo, hostAddress);

                    // post the message into transceiver
                    result = _transceiver.ProcessG2HMessage(request);
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }

                return result;
            }
        }

        private void FillRequestValues(MonMsg_G2H request)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "FillRequestValues"))
            {
                try
                {
                    InstallationDetailsForMSMQ data = ModuleHelper.Current.GetInstallationFromCache(request.FaultSourceTypeKey, request.InstallationNo);
                    request.Extra = data;
                    request.SiteCode = data.SiteCode;
                    request.Asset = data.Stock_No;
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
            }
        }

        internal ExMonitorServerImpl UpdateCommsServerHostAddress(int installationNo, string ipAddress)
        {
            if (installationNo > 0)
            {
                if (!_commsServersMapByInsno.ContainsKey(installationNo))
                {
                    _commsServersMapByInsno.GetOrAdd(installationNo, ipAddress);
                }
                else
                {
                    _commsServersMapByInsno[installationNo] = ipAddress;
                }
            }
            return this;
        }

        internal ExMonitorServerImpl UpdateInstallatioIpAddress(int installationNo, string ipAddress)
        {
            if (installationNo > 0)
            {
                if (!_mapIpInsNo.ContainsKey(ipAddress))
                {
                    _mapIpInsNo.GetOrAdd(ipAddress, installationNo);
                }
                else
                {
                    _mapIpInsNo[ipAddress] = installationNo;
                }
            }
            return this;
        }

        bool IExMonServer4CommsServer2.ProcessH2GMessage(ExComms.Contracts.DTO.Monitor.MonMsg_H2G request)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "ProcessG2HMessage"))
            {
                bool result = default(bool);

                try
                {
                    // unable to get the instllation no
                    if (request.InstallationNo <= 0)
                    {
                        Log.Info("Unable to get the installation no.");
                        return false;
                    }

                    // find the host by installation no or ip address
                    string hostIpAddress = string.Empty;
                    if (_commsServersMapByInsno.ContainsKey(request.InstallationNo))
                    {
                        _commsServersMapByInsno.TryGetValue(request.InstallationNo, out hostIpAddress);
                    }
                    else if (!request.IpAddress.IsEmpty() &&
                            _commsServersMapByIp.ContainsKey(request.IpAddress))
                    {
                        _commsServersMapByIp.TryGetValue(request.IpAddress, out hostIpAddress);
                        _commsServersMapByInsno.GetOrAdd(request.InstallationNo, hostIpAddress);
                    }

                    // get the callback by host ip address
                    if (!hostIpAddress.IsEmpty() &&
                        _commsServerCallbacks.ContainsKey(hostIpAddress))
                    {
                        IExMonServer4CommsServerCallback callback = _commsServerCallbacks[hostIpAddress];
                        if (callback != null)
                        {
                            request.HostIpAddress = hostIpAddress;
                            result = callback.ProcessH2GMessage(request);
                        }
                    }
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
