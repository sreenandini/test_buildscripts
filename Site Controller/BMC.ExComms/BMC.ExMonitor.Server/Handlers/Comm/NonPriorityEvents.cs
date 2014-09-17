using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.ExComms.Contracts.DTO.Monitor;
using BMC.ExComms.DataLayer.MSSQL;
using Audit.Transport;
using Audit.BusinessClasses;
using BMC.Common.ExceptionManagement;
using BMC.CoreLib;

namespace BMC.ExMonitor.Server.Handlers
{
    //[MonitorHandlerMapping((int)FaultSource.NonPriorityEvent, (int)FaultType_NonPriorityEvent.ZeroCredit)]
    //internal class MonitorHandler_ZeroCredit : MonitorHandlerBase
    //{
    //    protected override bool OnExecuteInternal(MonitorExecutionContext context, MonitorEntity_MsgTgt target)
    //    {
    //        using (ILogMethod method = Log.LogMethod("MonitorHandler_ZeroCredit", "ProcessG2HMessageInternal"))
    //        {
    //            MonMsg_G2H request = context.G2HMessage;

    //            try
    //            {
    //                ExCommsDataContext.Current.UpdateFloorFinancialSession(request.InstallationNo, "ZERO", "");
    //                ExCommsDataContext.Current.UpdateFloorFinancialSession(request.InstallationNo, "INIT", "");
    //                this.ForceMeterRead(context, target);
    //            }
    //            catch (Exception ex)
    //            {
    //                ExceptionManager.Publish(ex);
    //            }

    //            return true;
    //        }
    //    }
    //}

    [MonitorHandlerMapping((int)FaultSource.NonPriorityEvent, (int)FaultType_NonPriorityEvent.CompListChangedXC)]
    internal class MonitorHandler_CompListChangedXC : MonitorHandlerBase
    {
        protected override bool OnExecuteInternal(MonitorExecutionContext context, MonitorEntity_MsgTgt target)
        {
            using (ILogMethod method = Log.LogMethod("MonitorHandler_CompListChangedXC", "ProcessG2HMessageInternal"))
            {
                try
                {
                    ExCommsDataContext.Current.ResetCompVerificationRequestStatus(context.G2HMessage.InstallationNo, "");
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
