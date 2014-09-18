using System;
using System.Data;
using System.Collections.Generic;
using BMC.Common.ExceptionManagement;
using BMC.DBInterface.CashDeskOperator;
using BMC.Transport.CashDeskOperatorEntity;
using BMC.Transport;
using System.Data.Linq;

namespace BMC.Business.CashDeskOperator
{
    public partial class HandPay
    {
        //BGSGeneral.cGeneralClass general = new BGSGeneral.cGeneralClass();
        public jackpotProcessInfoDTO getJackpotStatusAmount(string JackpotSlipNumber, int Site)
        {
            return handpayDataAccess.getJackpotStatusAmount(JackpotSlipNumber, Site);
        }

        public jackpotProcessInfoDTO payJackpot(string SequenceNumber, int SiteId, string userId, string firstName, string lastName, string cashDeskLocation)
        {
            return handpayDataAccess.payJackpot(SequenceNumber, SiteId, userId, firstName, lastName, cashDeskLocation);
        }
        public DataSet CreateTickeException_HandpayCage(int Bar_Pos, Double TicketValue, int isHandpayResponse, string HP_Type)
        {
            return handpayDataAccess.CreateTickeException_HandpayCage(Bar_Pos, TicketValue, Environment.MachineName + "_Cage", isHandpayResponse, HP_Type);

        }
        public int GetInstallationNo(string SequenceNumber, int SiteId,out int InstallationNo)
        {
            return handpayDataAccess.GetInstallationNo(SequenceNumber, SiteId,out  InstallationNo);
        }

    }
}
