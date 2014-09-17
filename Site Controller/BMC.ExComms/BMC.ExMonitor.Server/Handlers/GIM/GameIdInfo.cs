using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.ExComms.Contracts.DTO.Monitor;
using BMC.ExComms.DataLayer.MSSQL;
using BMC.CoreLib;
using System.Net;

namespace BMC.ExMonitor.Server.Handlers.GIM
{
    [MonitorHandlerMapping((int)FaultSource.GIM_Event, (int)FaultType_GIM.Game_Id_Info_G2H)]
    internal class MonitorHandler_GIM
        : MonitorHandlerBase
    {
        protected override bool OnExecuteInternal(MonitorExecutionContext context, MonitorEntity_MsgTgt target)
        {
            using (ILogMethod method = Log.LogMethod("MonitorHandler_GIM", "OnExecuteInternal"))
            {
                try
                {
                    MonMsg_G2H msgSrc = context.G2HMessage;
                    MonTgt_G2H_GIM_GameIDInfo tgtSrc = target as MonTgt_G2H_GIM_GameIDInfo;
                    method.Info("GIM (CALL): IP Address : " + msgSrc.IpAddress +
                            ", Asset No : " + tgtSrc.AssetNumber.ToStringSafe() +
                            ", GMU No : " + tgtSrc.GMUNumber.ToStringSafe() +
                            ", Serial No : " + tgtSrc.SerialNumber.ToStringSafe());

                    int? installationNo = 0;
                    int? assetNo = 0;
                    string pokerGamePrefix = string.Empty;

                    if (ExCommsDataContext.Current.InsertGMULogin(tgtSrc, msgSrc.IpAddress,
                        ref installationNo, ref assetNo, ref pokerGamePrefix))
                    {
                        int assetNoInt = assetNo.SafeValue();
                        IPAddress hostIPAddress = null;

                        // get the ip address 
                        if (_configExchange.Honeyframe_Cashmaster_Exchange_EnableDhcp == 1)
                        {
                            hostIPAddress = _configExchange.Honeyframe_Cashmaster_BMCDHCP_ServerIP.ToIPAddress();
                        }
                        else
                        {
                            hostIPAddress = _configExchange.Honeyframe_Cashmaster_Exchange_interface.ToIPAddress();
                        }
                        method.InfoV("GIM (Success): Installation no ({1:D}), Asset No : {2:D}, Game Prefix : {3} for IP : {0}, from Host : {4}",
                            msgSrc.IpAddress, installationNo, assetNoInt,
                            pokerGamePrefix, hostIPAddress.ToString());
                        int installationNo2 = installationNo.SafeValue();

                        if (installationNo2 > 0)
                        {
                            MonMsg_H2G msgDest = new MonMsg_H2G()
                            {
                                InstallationNo = installationNo2,
                                IpAddress = msgSrc.IpAddress,
                            };
                            MonTgt_H2G_GIM_GameIDInfo tgtDest = new MonTgt_H2G_GIM_GameIDInfo();
                            tgtDest.SourceAddress = hostIPAddress;
                            tgtDest.EnableNetworkMessaging = true;
                            if (_configStore.Iview3AssetNum)
                            {
                                tgtDest.AssetNumberInt = assetNoInt;
                                tgtDest.PokerGamePrefix = pokerGamePrefix.ToString();
                            }

                            // update the installation no
                            ExMonitorServerImpl.Current
                                .UpdateCommsServerHostAddress(installationNo2, msgSrc.HostIpAddress)
                                .UpdateInstallatioIpAddress(installationNo2, msgSrc.IpAddress);

                            // add the target and process
                            msgSrc.InstallationNo = installationNo2;
                            msgDest.Targets.Add(tgtDest);
                            context.H2GMessage = msgDest;
                            return true;
                        }
                    }
                    else
                    {
                        method.InfoV("GIM (Failure): Unable to get the installation no for IP : {0}", msgSrc.IpAddress);
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
                return false;
            }
        }
    }
}
