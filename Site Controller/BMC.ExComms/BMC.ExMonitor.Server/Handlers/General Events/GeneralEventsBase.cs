using BMC.CoreLib;
using BMC.ExComms.DataLayer.MSSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.ExComms.Contracts.DTO.Monitor;

namespace BMC.ExMonitor.Server.Handlers
{
    internal class GeneralEventsBase
        : MonitorHandlerBase
    {
        protected override bool OnExecuteInternal(MonitorExecutionContext context, ExComms.Contracts.DTO.Monitor.MonitorEntity_MsgTgt target)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "OnExecuteInternal"))
            {
                bool result = default(bool);

                try
                {
                    MonMsg_G2H request = context.G2HMessage;
                    DateTime dDate = request.FaultDate;
                    return ExCommsDataContext.Current.InsertGeneralEvents(request.InstallationNo, target.FaultType, true, dDate, target.FaultSource);
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
