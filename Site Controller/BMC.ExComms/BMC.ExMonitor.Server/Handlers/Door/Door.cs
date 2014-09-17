using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib;
using BMC.ExComms.Contracts.DTO.Monitor;
using BMC.ExComms.DataLayer.MSSQL;

namespace BMC.ExMonitor.Server.Handlers.Door
{
    [MonitorHandlerMapping((int)FaultSource.DoorEvent, typeof(FaultType_DoorEvent))]
    internal class MonitorHandler_Door 
        : MonitorHandlerBase
    {
        protected override bool OnExecuteInternal(MonitorExecutionContext context, MonitorEntity_MsgTgt target)
        {
            bool status = default(bool);
            MonMsg_G2H request = context.G2HMessage;

            try
            {
                InstallationDetailsForMSMQ dbData = request.Extra as InstallationDetailsForMSMQ;
                int installationNo = Convert.ToInt32(request.InstallationNo);
                System.DateTime dDate = request.FaultDate;

                if (ExCommsDataContext.Current.CreateDoorEvent(installationNo, target.FaultType, true, dDate))
                    status = true;

                // modify the floor status..
                if (ExCommsDataContext.Current.UpdateFloorStatus(installationNo, dDate, target.FaultType))
                    status = true;
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
            }
            return status;
        }
    }
}
