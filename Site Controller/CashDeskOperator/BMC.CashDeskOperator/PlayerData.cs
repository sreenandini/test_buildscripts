using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.Business.CashDeskOperator;
using System.Data;
using BMC.Transport;

namespace BMC.CashDeskOperator
{
    public class PlayerData
    {
        PlayerDataBusinessObject oPlayerBiz = new PlayerDataBusinessObject();
        public List<rsp_GetPTRatingsResult> GetPlayerDataByCard(string CardNumber, DateTime StartDate, DateTime EndDate,string MessageType,bool FailedOnly)
        {
            return oPlayerBiz.GetPlayerDataByCard(CardNumber, StartDate, EndDate, MessageType, FailedOnly);
        }
        public void  UpdateRating_Status(long Sno, bool Status)
        {
            oPlayerBiz.UpdateRating_Status(Sno, Status);
        }

    }

}
