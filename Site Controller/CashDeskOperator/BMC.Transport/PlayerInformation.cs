using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.Transport.CashDeskOperatorEntity
{
    public class PlayerInformation
    {
        public string AccountNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName { get; set; }
        public int PlayerID { get; set; }
        public double PointsBalance { get; set; }
        public string ClubStatus { get; set; }
        public string ClubState { get; set; }
    }

    public class PrizeInfoDTO
    {
        public string AuthAward { get; set; }
        public string AwardUsed { get; set; }
        public string BasePoints { get; set; }
        public string BonusPoints { get; set; }
        public string PrizeId { get; set; }
        public string PrizeName { get; set; }
        public string RedeemPoints { get; set; }
    }

    public class LoginInfoDTO
    {
        public string ComputerName { get; set; }
        public DateTime GamingDate { get; set; }
        public string LocationCode { get; set; }
        public string Password { get; set; }
        public int Shift { get; set; }
        public string UserName { get; set; }
    }

    public class PlayerInfoDTO
    {
        public string AccountNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName { get; set; }
        public int PlayerID { get; set; }
        public double PointsBalance { get; set; }
        public string ClubStatus { get; set; }
        public string ClubState { get; set; }
    }



}
