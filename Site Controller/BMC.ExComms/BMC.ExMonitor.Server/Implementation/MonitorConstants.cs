using BMC.ExComms.Contracts.DTO.Freeform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExMonitor.Server
{
    public static class MonitorConstants
    {
        public static Dictionary<FF_AppId_EFT_AccountTypes, string> AccountType = new Dictionary<FF_AppId_EFT_AccountTypes, string>
        {
            { FF_AppId_EFT_AccountTypes.PromotionalOffer, "PromoBank" },
            { FF_AppId_EFT_AccountTypes.PointsRedemption, "PointsBank" },
            { FF_AppId_EFT_AccountTypes.PlayerCash, "CashBank" }
        };

        public const string CARDIN = "CardIn";
        public const string CARDOUTRESPONSE = "CardOutResponse";
        public const string INTERVALRATINGRESPONSE = "IntervalRatingresponse";

        public const string BALANCERESPONSE = "BalanceResponse";
        public const string WITHDRAWRESPONSE = "WithdrawResponse";
        public const string WITHDRAWCOMPLETERESPONSE = "WithdrawCompleteResponse";
        public const string DEPOSITRESPONSE = "DepositResponse";
        public const string DEPOSITCOMPLETERESPONSE = "DepositCompleteResponse";

        public const string WITHDRAWALREQUEST_TRANSACTIONTYPE = "Withdrawal Request";
        public const string WITHDRAWALRESPONSE_TRANSACTIONTYPE = "Withdrawal Response";
        public const string WITHDRAWALCOMPLETE_TRANSACTIONTYPE = "Withdrawal Complete";
        public const string DEPOSITREQUEST_TRANSACTIONTYPE = "Deposit Request";
        public const string DEPOSITRESPONSE_TRANSACTIONTYPE = "Deposit Response";
        public const string DEPOSITCOMPLETE_TRANSACTIONTYPE = "Deposit Complete";

        public const string GAMECAPPING_AUTHENTICATION = "Game Capping";
        public const string GAMEUNCAPPING_AUTHENTICATION = "Game UnCapping";

        public const int SECTOR_CMD_FLASHING = 62;

    }
}
