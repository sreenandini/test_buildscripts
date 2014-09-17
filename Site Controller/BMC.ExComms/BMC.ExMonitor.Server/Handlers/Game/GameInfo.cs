using BMC.CoreLib;
using BMC.ExComms.Contracts.DTO.Monitor;
using BMC.ExComms.DataLayer.MSSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExMonitor.Server.Handlers
{


    [MonitorHandlerMapping((int)FaultSource.NonPriorityEvent, (int)FaultType_NonPriorityEvent.NewGameSelected)]
    internal class MonitorHandler_GameInfo : MonitorHandlerBase
    {
        protected override bool ProcessG2HMessageInternal(ExComms.Contracts.DTO.Monitor.MonMsg_G2H request)
        {
            using (ILogMethod method = Log.LogMethod("MonitorHandler_GameInfo", "ProcessG2HMessageInternal"))
            {
                try
                {
                    MonTgt_G2H_GameInfo monTgtMsg = request.Targets[0] as MonTgt_G2H_GameInfo;
                  
                 //   if (ExCommsDataContext.Current.UpdateSession_MGMD_MeterHistory(monTgtMsg))
                    {

                    }
                    method.Debug("Snap taken, now update the latest meters in floor financials");
                    //++k BGSMeterHandler.Proceess
                    return true;

                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
                return false;
            }
        }
    }

    [MonitorHandlerMapping((int)FaultSource.ExtendedGameInfo, (int) FaultType_ExtendedGameInfoEvent.UpdateExtendedGameInfo)]
    internal class MonitorHandler_ExtendedGameInfo : MonitorHandlerBase
    {
        protected override bool ProcessG2HMessageInternal(ExComms.Contracts.DTO.Monitor.MonMsg_G2H request)
        {
            using (ILogMethod method = Log.LogMethod("MonitorHandler_ExtendedGameInfo", "ProcessG2HMessageInternal"))
            {
                try
                {

                    //MonTgt_G2H_ExtendedGameInfo monTgtMsg = request.Targets[0] as MonTgt_G2H_ExtendedGameInfoInfo;

                //    if (ExCommsDataContext.Current.UpdateInstallationGameInfo(monTgtMsg.installationNo,monTgtMsg.GameNumber,monTgtMsg.MaxBet,monTgtMsg.ProgressGroup,monTgtMsg.ProgressLevel,monTgtMsg.GameName,monTgtMsg.PayTableName,false,monTgtMsg.HasGameNameFramed,""))
                    {

                    }
                    
                    //++k BGSMeterHandler.Proceess
                    return true;

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
