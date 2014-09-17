using BMC.Common.ExceptionManagement;
using BMC.CoreLib;
using BMC.ExComms.Contracts.DTO.Monitor;
using BMC.ExComms.DataLayer.MSSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExMonitor.Server.Handlers.Game
{
    [MonitorHandlerMapping((int)FaultSource.NonPriorityEvent, (int)FaultType_NonPriorityEvent.StartCardlessPlay)]
    [MonitorHandlerMapping((int)FaultSource.NonPriorityEvent, (int)FaultType_NonPriorityEvent.EndCardlessPlay)]
    internal class MonitorHandler_CardLessPlay : MonitorHandlerBase
    {
        protected override bool ProcessG2HMessageInternal(ExComms.Contracts.DTO.Monitor.MonMsg_G2H request)
        {
            using (ILogMethod method = Log.LogMethod("MonitorHandler_CardLessPlay", "ProcessG2HMessageInternal"))
            {
                DateTime dDate = default(System.DateTime);
                dDate = request.FaultDate;

                try
                {
                    ExCommsDataContext.Current.AddFaultEvent(request.InstallationNo, request.FaultSource, request.FaultType, "", true, dDate);
                }
                catch (Exception ex)
                {
                    ExceptionManager.Publish(ex);
                }

                return true;
            }
        }
    }
}
