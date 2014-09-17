using BMC.Common.ExceptionManagement;
using BMC.CoreLib;
using BMC.ExComms.Contracts.DTO.Monitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExMonitor.Server.Handlers.LongPoll
{
    [MonitorHandlerMapping((int)FaultSource.LongPoll, (int)FaultType_LongPollCode.LPC_Send_InstantPeriodic)]
    internal class MonitorHandler_LPC_103_8
        : MonitorHandlerBase_H2G
    {
        #region Public Methods

        public void SetInstantPeriodicConfig(MonMsg_H2G response)
        {
            try
            {
                this.ProcessH2GMessage(GetInstantPeriodicEntity(response));
            }
            catch(Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        #endregion //Public Methods

        #region Private Methods

        private MonMsg_H2G GetInstantPeriodicEntity(MonMsg_H2G response)
        {
            try
            {
                if (response.Targets.Count <= 0) return null;
                MonTgt_H2G_LP_InstantPeriodic monTgt = response.Targets[0] as MonTgt_H2G_LP_InstantPeriodic;
                if (monTgt == null) return null;

                int gmuTimeout = HandlerHelper.Current.GMUTimeOut / 1000;
                int instantPeriodicValue = (HandlerHelper.Current.InsPerDelay * (response.InstallationNo % HandlerHelper.Current.TotInstGrp));
                Log.Info(" Calculated TimeOut Value :" + instantPeriodicValue.ToString());

                int instantPeriodicInterval = instantPeriodicValue <= gmuTimeout ? gmuTimeout + 5 : instantPeriodicValue;

                monTgt.ConfiguredInterval = Convert.ToByte(instantPeriodicValue);
                monTgt.LowerOrderInterval = Convert.ToByte(instantPeriodicValue & 0xFF00 >> 8);
                monTgt.HigherOrderInterval = Convert.ToByte(instantPeriodicValue & 0xFF);

                response.Targets[0] = monTgt;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return response;
        }

        #endregion //Private Methods
    }
}
