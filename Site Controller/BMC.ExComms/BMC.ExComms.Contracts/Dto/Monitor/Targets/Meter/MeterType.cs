using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Monitor
{
    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
         Name = "MonTgt_G2H_Meter_MeterType")]
    public enum MonTgt_G2H_Meter_MeterType 
    {
        [EnumMember()]
        Unknown = 0,

        #region UNUSED METERS
        // UNUSED METERS
        [EnumMember()]
        Promotional_Credits = 58,
        [EnumMember()]
        Non_Cashable_Credits = 59,
        [EnumMember()]
        Transfers_To_Host = 60,
        [EnumMember()]
        Cashable_Credits = 61,

        [EnumMember()]
        Games_Since_Power_Up_Meter = 74,
        [EnumMember()]
        Dollar_Value_Of_Bills_meter = 76,
        [EnumMember()]
        Bill_Credit_Amount_29 = 77,

        [EnumMember()]
        Last_Accepted_Bill = 79,
        [EnumMember()]
        No_Of_Bills_In_Stacker = 80,
        [EnumMember()]
        Credit_Amount_Of_Bills_In_Stacker = 81,
        [EnumMember()]
        Number_Of_Games_Implemented = 82,
        [EnumMember()]
        Version_Id = 83,
        [EnumMember()]
        Serial_Number = 84,
        [EnumMember()]
        Game_Id = 85,
        #endregion

        #region COINS (In, Out & Drop)
        // COINS (In, Out & Drop)        
        [EnumMember()]
        Coins_In = 50,
        [EnumMember()]
        Coins_Out = 51,
        [EnumMember()]
        Coins_Drop = 52,
        [EnumMember()]
        External_Coin_Amount = 78,
        #endregion

        #region TRUE COINS (In, Out)
        // TRUE COINS (In, Out)
        [EnumMember()]
        True_Coin_In = 86,
        [EnumMember()]
        True_Coin_Out = 87,
        #endregion

        #region GAMES (Bet, Won and Lost)
        // GAMES (Bet, Won and Lost)
        [EnumMember()]
        Jackpot = 53,
        [EnumMember()]
        Games_Bet = 54,
        [EnumMember()]
        Games_Won = 55,
        [EnumMember()]
        Games_Lost = 73,
        #endregion

        #region EVENTS
        // EVENTS
        [EnumMember()]
        Door_Open = 56,
        [EnumMember()]
        Power_Reset = 57,
        #endregion

        #region BILLS
        // BILLS 1, 2, 5
        // BILLS 10, 20, 50
        // BILLS 100, 200, 250, 500
        // BILLS 10000, 20000, 25000, 50000, 100000
        [EnumMember()]
        Bill_1 = 62,
        [EnumMember()]
        Bill_2 = 110,
        [EnumMember()]
        Bill_5 = 63,
        [EnumMember()]
        Bill_10 = 64,
        [EnumMember()]
        Bill_20 = 65,
        [EnumMember()]
        Bill_50 = 66,
        [EnumMember()]
        Bill_100 = 67,
        [EnumMember()]
        Bill_200 = 108,
        [EnumMember()]
        Bill_250 = 68,
        [EnumMember()]
        Bill_500 = 109,
        [EnumMember()]
        Bill_10000 = 69,
        [EnumMember()]
        Bill_20000 = 70,
        [EnumMember()]
        Bill_25000 = 71,
        [EnumMember()]
        Bill_50000 = 72,
        [EnumMember()]
        Bill_100000 = 48,
        #endregion

        #region CREDITS
        // CREDITS
        [EnumMember()]
        Current_Credits = 75,
        [EnumMember()]
        Cancelled_Credits = 49,
        [EnumMember()]
        Handpay_Cancelled_Credits = 88,
        #endregion

        #region TICKETS
        // CASHABLE TICKETS AMOUNT (In & Out)
        [EnumMember()]
        Tickets_Cashable_In_Value = 89,
        [EnumMember()]
        Tickets_Cashable_Out_Value = 90,

        // NON CASHABLE TICKETS AMOUNT (In & Out)
        [EnumMember()]
        Tickets_Noncashable_In_Value = 91,
        [EnumMember()]
        Tickets_Noncashable_Out_Value = 92,

        // CASHABLE TICKETS QTY (In & Out)
        [EnumMember()]
        Tickets_Cashable_In_Qty = 93,
        [EnumMember()]
        Tickets_Cashable_Out_Qty = 94,

        // NON CASHABLE TICKETS QTY (In & Out)
        [EnumMember()]
        Tickets_Noncashable_In_Qty = 95,
        [EnumMember()]
        Tickets_Noncashable_Out_Qty = 96,
        #endregion

        #region PROGRESSIVE WIN
        // PROGRESSIVE WIN
        [EnumMember()]
        Progressive_Win_Value = 97,
        [EnumMember()]
        Progressive_Win_Handpay_Value = 98,
        #endregion

        #region MYSTERY PAID
        // MYSTERY PAID
        [EnumMember()]
        Mystery_Machine_Paid = 99,
        [EnumMember()]
        Mystery_Attendant_Paid = 100,
        #endregion

        #region PROMO CASHABLE EFT (In & Out)
        // PROMO CASHABLE EFT (In & Out)
        [EnumMember()]
        Promo_Cashable_EFT_In = 101,
        [EnumMember()]
        Promo_Cashable_EFT_Out = 102,
        #endregion

        #region NON CASHABLE EFT (In & Out)
        // NON CASHABLE EFT (In & Out)
        [EnumMember()]
        Noncashable_EFT_In = 103,
        [EnumMember()]
        Noncashable_EFT_Out = 104,
        #endregion

        #region CASHABLE EFT (In & Out)
        // CASHABLE EFT (In & Out)
        [EnumMember()]
        Cashable_EFT_In = 105,
        [EnumMember()]
        Cashable_EFT_Out = 106,
        #endregion

        #region Others
        [EnumMember()]
        SASJPMeters_CumulativeProgWin = 107,
        #endregion
    }
}
