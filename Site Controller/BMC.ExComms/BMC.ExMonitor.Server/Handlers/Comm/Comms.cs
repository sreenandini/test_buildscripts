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
    [MonitorHandlerMapping((int)FaultSource.NonPriorityEvent, (int)FaultType_NonPriorityEvent.CommsStopped)]
    [MonitorHandlerMapping((int)FaultSource.NonPriorityEvent, (int)FaultType_NonPriorityEvent.CommsResumed)]
    internal class MonitorHandler_Comms
        : MonitorHandler_Comms_Base
    {
        protected override bool OnExecuteInternal(MonitorExecutionContext context, ExComms.Contracts.DTO.Monitor.MonitorEntity_MsgTgt target)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "OnExecuteInternal"))
            {
                try
                {
                    MonMsg_G2H request = context.G2HMessage;
                    string faultDesc = request.FaultType == (int)FaultType_NonPriorityEvent.CommsStopped ? "Polling Failed" : "Polling Resumed";

                    base.AddFaultEvent(request.InstallationNo, request.FaultSource, request.FaultType, faultDesc, true, request.FaultDate);
                    return base.OnExecuteInternal(context, target);
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                    return false;
                }
            }
        }
    }

    [MonitorHandlerMapping((int)FaultSource.NonPriorityEvent, (int)FaultType_NonPriorityEvent.ExtraSAS)]
    internal class MonitorHandler_ExtraSAS
        : MonitorHandler_Comms_Base
    {
        protected override bool OnExecuteInternal(MonitorExecutionContext context, ExComms.Contracts.DTO.Monitor.MonitorEntity_MsgTgt target)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "OnExecuteInternal"))
            {
                try
                {
                    MonMsg_G2H request = context.G2HMessage;
                    request.FaultSource = 200;

                    base.AddFaultEvent(request.InstallationNo, request.FaultSource, request.FaultType, string.Empty, true, request.FaultDate);
                    return base.OnExecuteInternal(context, target);
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                    return false;
                }
            }
        }
    }

    [MonitorHandlerMapping((int)FaultSource.NonPriorityEvent, (int)FaultType_NonPriorityEvent.InstantPeriodic)]
    [MonitorHandlerMapping((int)FaultSource.NonPriorityEvent, (int)FaultType_NonPriorityEvent.ClosingSessions)]
    internal class MonitorHandler_InstantPeriodic : MonitorHandlerBase
    {

        protected override bool OnExecuteInternal(MonitorExecutionContext context, ExComms.Contracts.DTO.Monitor.MonitorEntity_MsgTgt target)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "OnExecuteInternal"))
            {
                try
                {
                    this.ForceMeterRead(context, target);
                    return true;
                }
                catch (Exception ex)
                {
                    ExceptionManager.Publish(ex);
                    return false;
                }
            }
        }
    }
}
