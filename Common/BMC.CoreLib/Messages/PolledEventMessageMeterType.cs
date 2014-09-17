using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.CoreLib.Messages
{
    public enum PolledEventMessageMeterType
    {
        Unknown = 0,

        #region UNUSED METERS
        // UNUSED METERS
        Promotional_Credits = 58,
        Non_Cashable_Credits = 59,
        Transfers_To_Host = 60,
        Cashable_Credits = 61,

        Games_Since_Power_Up_Meter = 74,
        Dollar_Value_Of_Bills_meter = 76,
        Bill_Credit_Amount_29 = 77,

        Last_Accepted_Bill = 79,
        No_Of_Bills_In_Stacker = 80,
        Credit_Amount_Of_Bills_In_Stacker = 81,
        Number_Of_Games_Implemented = 82,
        Version_Id = 83,
        Serial_Number = 84,
        Game_Id = 85,
        #endregion

        #region COINS (In, Out & Drop)
        // COINS (In, Out & Drop)        
        Coins_In = 50,
        Coins_Out = 51,
        Coins_Drop = 52,
        External_Coin_Amount = 78,
        #endregion

        #region TRUE COINS (In, Out)
        // TRUE COINS (In, Out)
        True_Coin_In = 86,
        True_Coin_Out = 87,
        #endregion

        #region GAMES (Bet, Won and Lost)
        // GAMES (Bet, Won and Lost)
        Jackpot = 53,
        Games_Bet = 54,
        Games_Won = 55,
        Games_Lost = 73,
        #endregion

        #region EVENTS
        // EVENTS
        Door_Open = 56,
        Power_Reset = 57,
        #endregion

        #region BILLS
        // BILLS 1, 2, 5
        // BILLS 10, 20, 50
        // BILLS 100, 200, 250, 500
        // BILLS 10000, 20000, 25000, 50000, 100000
        Bill_1 = 62,
        Bill_2 = 110,
        Bill_5 = 63,
        Bill_10 = 64,
        Bill_20 = 65,
        Bill_50 = 66,
        Bill_100 = 67,
        Bill_200 = 108,
        Bill_250 = 68,
        Bill_500 = 109,
        Bill_10000 = 69,
        Bill_20000 = 70,
        Bill_25000 = 71,
        Bill_50000 = 72,
        Bill_100000 = 48,
        #endregion

        #region CREDITS
        // CREDITS
        Current_Credits = 75,
        Cancelled_Credits = 49,
        Handpay_Cancelled_Credits = 88,
        #endregion

        #region TICKETS
        // CASHABLE TICKETS AMOUNT (In & Out)
        Tickets_Cashable_In_Value = 89,
        Tickets_Cashable_Out_Value = 90,

        // NON CASHABLE TICKETS AMOUNT (In & Out)
        Tickets_Noncashable_In_Value = 91,
        Tickets_Noncashable_Out_Value = 92,

        // CASHABLE TICKETS QTY (In & Out)
        Tickets_Cashable_In_Qty = 93,
        Tickets_Cashable_Out_Qty = 94,

        // NON CASHABLE TICKETS QTY (In & Out)
        Tickets_Noncashable_In_Qty = 95,
        Tickets_Noncashable_Out_Qty = 96,
        #endregion

        #region PROGRESSIVE WIN
        // PROGRESSIVE WIN
        Progressive_Win_Value = 97,
        Progressive_Win_Handpay_Value = 98,
        #endregion

        #region MYSTERY PAID
        // MYSTERY PAID
        Mystery_Machine_Paid = 99,
        Mystery_Attendant_Paid = 100,
        #endregion

        #region PROMO CASHABLE EFT (In & Out)
        // PROMO CASHABLE EFT (In & Out)
        Promo_Cashable_EFT_In = 101,
        Promo_Cashable_EFT_Out = 102,
        #endregion

        #region NON CASHABLE EFT (In & Out)
        // NON CASHABLE EFT (In & Out)
        Noncashable_EFT_In = 103,
        Noncashable_EFT_Out = 104,
        #endregion

        #region CASHABLE EFT (In & Out)
        // CASHABLE EFT (In & Out)
        Cashable_EFT_In = 105,
        Cashable_EFT_Out = 106,
        #endregion
    }
}
