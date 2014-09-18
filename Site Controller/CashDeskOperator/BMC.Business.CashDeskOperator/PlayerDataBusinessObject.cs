using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.DBInterface.CashDeskOperator;
using System.Data;
using BMC.Transport; 
namespace BMC.Business.CashDeskOperator
{
    public class PlayerDataBusinessObject
    {
        PlayerDataDataAccess oPlayerDataDataAccess = new PlayerDataDataAccess();
        List<rsp_GetPTRatingsResult> oRresult;
        public List<rsp_GetPTRatingsResult> GetPlayerDataByCard(string CardNumber, DateTime StartDate, DateTime EndDate, string MessageType,bool FailedOnly)
        {
            PlayerRatingDataContext oDc = new PlayerRatingDataContext();
            return oDc.rsp_GetPTRatings(CardNumber, StartDate, EndDate, MessageType, FailedOnly).ToList<rsp_GetPTRatingsResult>();
        }
        public void UpdateRating_Status(long Sno, bool Status)
        {
            oPlayerDataDataAccess.UpdateRating_Status(Sno, Status);
        }
    }
}
