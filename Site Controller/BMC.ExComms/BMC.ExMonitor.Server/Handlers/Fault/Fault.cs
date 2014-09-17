using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.ExComms.Contracts.DTO.Monitor;
using BMC.ExComms.DataLayer.MSSQL;

namespace BMC.ExMonitor.Server.Handlers.Fault
{
    [MonitorHandlerMapping(-1, -1)]
    internal class MonitorHandler_Fault 
        : MonitorHandlerBase
    {
        protected override bool OnExecuteInternal(MonitorExecutionContext context, MonitorEntity_MsgTgt target)
        {
            InstallationDetailsForMSMQ dbData = null;
            string sText = null;
            string CardID = null;
            DateTime dDate;
            int installationNo = default(int);
            bool status = default(bool);
            MonMsg_G2H request = context.G2HMessage;

            try
            {

                long lFaultSource = 0;
                long lFaultType = 0;

                dbData = request.Extra as InstallationDetailsForMSMQ;
                installationNo = request.InstallationNo;
                dDate = default(System.DateTime);

                dDate = request.FaultDate;
                sText = "";

                if (request.FaultSource == 21 & request.FaultType == 21)
                {
                    lFaultSource = 200;
                    lFaultType = request.FaultType;

                }
                else
                {
                    lFaultSource = request.FaultSource;
                    lFaultType = request.FaultType;
                }

                try
                {
                    CardID = dbData.EmployeeCardNumber;
                }
                catch (Exception ex)
                {
                    CardID = string.Empty;
                }

                if (ExCommsDataContext.Current.CreateFaultEvent(installationNo, (int)lFaultSource, (int)lFaultType, sText, true, dDate, CardID))
                    status = true;
            }
            catch (Exception ex)
            {
                status = false;
            }
            return status;
        }
    }
}
