/************************************************************
 * Created by Bally Technologies © 2011
 * Time: 26/12/12 5:43:30 PM
 ************************************************************/
USE [Enterprise]
GO

IF NOT EXISTS(SELECT 1 FROM [Hourly_Statistics_Types]WHERE HST_Type = 'CASINO_WIN')
    INSERT [Hourly_Statistics_Types] ( HST_Order_ID, HST_Type, HST_Desc )
    SELECT 1, 'CASINO_WIN', 'NET_WIN'
ELSE
    UPDATE [Hourly_Statistics_Types]
    SET    HST_Order_ID = 1, HST_Type = 'CASINO_WIN', HST_Desc = 'NET_WIN'
    WHERE  HST_Type = 'CASINO_WIN'

IF NOT EXISTS(SELECT 1 FROM [Hourly_Statistics_Types]WHERE HST_Type = 'DROP')
    INSERT [Hourly_Statistics_Types] ( HST_Order_ID, HST_Type, HST_Desc )
    SELECT 2, 'DROP', 'DROP'
ELSE
    UPDATE [Hourly_Statistics_Types]
    SET    HST_Order_ID = 2, HST_Type = 'DROP', HST_Desc = 'DROP'
    WHERE  HST_Type = 'DROP'

IF NOT EXISTS(SELECT 1 FROM [Hourly_Statistics_Types]WHERE HST_Type = 'CREDITS_WAGERED')
    INSERT [Hourly_Statistics_Types] ( HST_Order_ID, HST_Type, HST_Desc )
    SELECT 3, 'CREDITS_WAGERED', 'CREDITS_WAGERED'
ELSE
    UPDATE [Hourly_Statistics_Types]
    SET    HST_Order_ID = 3, HST_Type = 'CREDITS_WAGERED', HST_Desc = 'CREDITS_WAGERED'
    WHERE  HST_Type = 'CREDITS_WAGERED'

IF NOT EXISTS(SELECT 1 FROM [Hourly_Statistics_Types]WHERE HST_Type = 'AVG_BET')
    INSERT [Hourly_Statistics_Types] ( HST_Order_ID, HST_Type, HST_Desc )
    SELECT 4, 'AVG_BET', 'AVG_BET'
ELSE
    UPDATE [Hourly_Statistics_Types]
    SET    HST_Order_ID = 4, HST_Type = 'AVG_BET', HST_Desc = 'AVG_BET'
    WHERE  HST_Type = 'AVG_BET'

IF NOT EXISTS(SELECT 1 FROM [Hourly_Statistics_Types]WHERE HST_Type = 'CANCELLED_CREDITS')
    INSERT [Hourly_Statistics_Types] ( HST_Order_ID, HST_Type, HST_Desc )
    SELECT 5, 'CANCELLED_CREDITS', 'CANCELLED_CREDITS'
ELSE
    UPDATE [Hourly_Statistics_Types]
    SET    HST_Order_ID = 5, HST_Type = 'CANCELLED_CREDITS', HST_Desc = 'CANCELLED_CREDITS'
    WHERE  HST_Type = 'CANCELLED_CREDITS'

IF NOT EXISTS(SELECT 1 FROM [Hourly_Statistics_Types]WHERE HST_Type = 'CREDITS_WON')
    INSERT [Hourly_Statistics_Types] ( HST_Order_ID, HST_Type, HST_Desc )
    SELECT 6, 'CREDITS_WON', 'CREDITS_WON'
ELSE
    UPDATE [Hourly_Statistics_Types]
    SET    HST_Order_ID = 6, HST_Type = 'CREDITS_WON', HST_Desc = 'CREDITS_WON'
    WHERE  HST_Type = 'CREDITS_WON'

IF NOT EXISTS(SELECT 1 FROM [Hourly_Statistics_Types]WHERE HST_Type = 'GAMES_BET')
    INSERT [Hourly_Statistics_Types] ( HST_Order_ID, HST_Type, HST_Desc )
    SELECT 7, 'GAMES_BET', 'GAMES_BET'
ELSE
    UPDATE [Hourly_Statistics_Types]
    SET    HST_Order_ID = 7, HST_Type = 'GAMES_BET', HST_Desc = 'GAMES_BET'
    WHERE  HST_Type = 'GAMES_BET'

IF NOT EXISTS(SELECT 1 FROM [Hourly_Statistics_Types]WHERE HST_Type = 'GAMES_LOST')
    INSERT [Hourly_Statistics_Types] ( HST_Order_ID, HST_Type, HST_Desc )
    SELECT 8, 'GAMES_LOST', 'GAMES_LOST'
ELSE
    UPDATE [Hourly_Statistics_Types]
    SET    HST_Order_ID = 8, HST_Type = 'GAMES_LOST', HST_Desc = 'GAMES_LOST'
    WHERE  HST_Type = 'GAMES_LOST'

IF NOT EXISTS(SELECT 1 FROM [Hourly_Statistics_Types]WHERE HST_Type = 'GAMES_WON')
    INSERT [Hourly_Statistics_Types] ( HST_Order_ID, HST_Type, HST_Desc )
    SELECT 9, 'GAMES_WON', 'GAMES_WON'
ELSE
    UPDATE [Hourly_Statistics_Types]
    SET    HST_Order_ID = 9, HST_Type = 'GAMES_WON', HST_Desc = 'GAMES_WON'
    WHERE  HST_Type = 'GAMES_WON'

IF NOT EXISTS(SELECT 1 FROM [Hourly_Statistics_Types]WHERE HST_Type = 'HANDPAY')
    INSERT [Hourly_Statistics_Types] ( HST_Order_ID, HST_Type, HST_Desc )
    SELECT 10, 'HANDPAY', 'ATTENDANTPAY'
ELSE
    UPDATE [Hourly_Statistics_Types]
    SET    HST_Order_ID = 10, HST_Type = 'HANDPAY', HST_Desc = 'ATTENDANTPAY'
    WHERE  HST_Type = 'HANDPAY'

IF NOT EXISTS(SELECT 1 FROM [Hourly_Statistics_Types]WHERE HST_Type = 'JACKPOT')
    INSERT [Hourly_Statistics_Types] ( HST_Order_ID, HST_Type, HST_Desc )
    SELECT 11, 'JACKPOT', 'JACKPOT'
ELSE
    UPDATE [Hourly_Statistics_Types]
    SET    HST_Order_ID = 11, HST_Type = 'JACKPOT', HST_Desc = 'JACKPOT'
    WHERE  HST_Type = 'JACKPOT'

IF NOT EXISTS(SELECT 1 FROM [Hourly_Statistics_Types]WHERE HST_Type = 'PROGRESSIVE_HANDPAY')
    INSERT [Hourly_Statistics_Types] ( HST_Order_ID, HST_Type, HST_Desc )
    SELECT 12, 'PROGRESSIVE_HANDPAY', 'PROGRESSIVE_HANDPAY'
ELSE
    UPDATE [Hourly_Statistics_Types]
    SET    HST_Order_ID = 12, HST_Type = 'PROGRESSIVE_HANDPAY', HST_Desc = 'PROGRESSIVE_HANDPAY'
    WHERE  HST_Type = 'PROGRESSIVE_HANDPAY'

IF NOT EXISTS(SELECT 1 FROM [Hourly_Statistics_Types]WHERE HST_Type = 'PROGRESSIVE_WIN')
    INSERT [Hourly_Statistics_Types] ( HST_Order_ID, HST_Type, HST_Desc )
    SELECT 13, 'PROGRESSIVE_WIN', 'PROGRESSIVE_WIN'
ELSE
    UPDATE [Hourly_Statistics_Types]
    SET    HST_Order_ID = 13, HST_Type = 'PROGRESSIVE_WIN', HST_Desc = 'PROGRESSIVE_WIN'
    WHERE  HST_Type = 'PROGRESSIVE_WIN'

IF NOT EXISTS(SELECT 1 FROM [Hourly_Statistics_Types]WHERE HST_Type = 'TICKETS_INSERTED_QTY')
    INSERT [Hourly_Statistics_Types] ( HST_Order_ID, HST_Type, HST_Desc )
    SELECT 14, 'TICKETS_INSERTED_QTY', 'VOUCHERS_IN_QTY'
ELSE
    UPDATE [Hourly_Statistics_Types]
    SET    HST_Order_ID = 14, HST_Type = 'TICKETS_INSERTED_QTY', HST_Desc = 'VOUCHERS_IN_QTY'
    WHERE  HST_Type = 'TICKETS_INSERTED_QTY'

IF NOT EXISTS(SELECT 1 FROM [Hourly_Statistics_Types]WHERE HST_Type = 'TICKETS_INSERTED_VALUE')
    INSERT [Hourly_Statistics_Types] ( HST_Order_ID, HST_Type, HST_Desc )
    SELECT 15, 'TICKETS_INSERTED_VALUE', 'VOUCHERS_IN'
ELSE
    UPDATE [Hourly_Statistics_Types]
    SET    HST_Order_ID = 15, HST_Type = 'TICKETS_INSERTED_VALUE', HST_Desc = 'VOUCHERS_IN'
    WHERE  HST_Type = 'TICKETS_INSERTED_VALUE'

IF NOT EXISTS(SELECT 1 FROM [Hourly_Statistics_Types]WHERE HST_Type = 'TICKETS_PRINTED_QTY')
    INSERT [Hourly_Statistics_Types] ( HST_Order_ID, HST_Type, HST_Desc )
    SELECT 16, 'TICKETS_PRINTED_QTY', 'VOUCHERS_OUT_QTY'
ELSE
    UPDATE [Hourly_Statistics_Types]
    SET    HST_Order_ID = 16, HST_Type = 'TICKETS_PRINTED_QTY', HST_Desc = 'VOUCHERS_OUT_QTY'
    WHERE  HST_Type = 'TICKETS_PRINTED_QTY'

IF NOT EXISTS(SELECT 1 FROM [Hourly_Statistics_Types]WHERE HST_Type = 'TICKETS_PRINTED_VALUE')
    INSERT [Hourly_Statistics_Types] ( HST_Order_ID, HST_Type, HST_Desc )
    SELECT 17, 'TICKETS_PRINTED_VALUE', 'VOUCHERS_OUT'
ELSE
    UPDATE [Hourly_Statistics_Types]
    SET    HST_Order_ID = 17, HST_Type = 'TICKETS_PRINTED_VALUE', HST_Desc = 'VOUCHERS_OUT'
    WHERE  HST_Type = 'TICKETS_PRINTED_VALUE'

--Changed occupancy to occupancy(%)
IF EXISTS(SELECT 1 FROM [Hourly_Statistics_Types]WHERE HST_Type = 'OCCUPANCY')
	DELETE FROM Hourly_Statistics_Types WHERE HST_Type='OCCUPANCY'

IF NOT EXISTS(SELECT 1 FROM [Hourly_Statistics_Types]WHERE HST_Type = 'OCCUPANCY(%)')
    INSERT [Hourly_Statistics_Types] ( HST_Order_ID, HST_Type, HST_Desc )
    SELECT 18, 'OCCUPANCY(%)', 'OCCUPANCY(%)'
ELSE
    UPDATE [Hourly_Statistics_Types]
    SET    HST_Order_ID = 18, HST_Type = 'OCCUPANCY(%)', HST_Desc = 'OCCUPANCY(%)'
    WHERE  HST_Type = 'OCCUPANCY(%)'
--End

IF NOT EXISTS(SELECT 1 FROM [Hourly_Statistics_Types]WHERE HST_Type = 'MYSTERY_MACHINE_PAID')
    INSERT [Hourly_Statistics_Types] ( HST_Order_ID, HST_Type, HST_Desc )
    SELECT 19, 'MYSTERY_MACHINE_PAID', 'MYSTERY_MACHINE_PAID'
ELSE
    UPDATE [Hourly_Statistics_Types]
    SET    HST_Order_ID = 19, HST_Type = 'MYSTERY_MACHINE_PAID', HST_Desc = 'MYSTERY_MACHINE_PAID'
    WHERE  HST_Type = 'MYSTERY_MACHINE_PAID'

IF NOT EXISTS(SELECT 1 FROM [Hourly_Statistics_Types]WHERE HST_Type = 'MYSTERY_ATTENDANT_PAID')
    INSERT [Hourly_Statistics_Types] ( HST_Order_ID, HST_Type, HST_Desc )
    SELECT 20, 'MYSTERY_ATTENDANT_PAID', 'MYSTERY_ATTENDANT_PAID'
ELSE
    UPDATE [Hourly_Statistics_Types]
    SET    HST_Order_ID = 20, HST_Type = 'MYSTERY_ATTENDANT_PAID', HST_Desc = 'MYSTERY_ATTENDANT_PAID'
    WHERE  HST_Type = 'MYSTERY_ATTENDANT_PAID'

IF NOT EXISTS(SELECT 1 FROM [Hourly_Statistics_Types]WHERE HST_Type = 'PROMO_CASHABLE_EFT_IN')
    INSERT [Hourly_Statistics_Types] ( HST_Order_ID, HST_Type, HST_Desc )
    SELECT 21, 'PROMO_CASHABLE_EFT_IN', 'PROMO_CASHABLE_EFT_IN'
ELSE
    UPDATE [Hourly_Statistics_Types]
    SET    HST_Order_ID = 21, HST_Type = 'PROMO_CASHABLE_EFT_IN', HST_Desc = 'PROMO_CASHABLE_EFT_IN'
    WHERE  HST_Type = 'PROMO_CASHABLE_EFT_IN'

IF NOT EXISTS(SELECT 1 FROM [Hourly_Statistics_Types]WHERE HST_Type = 'NON_CASHABLE_EFT_IN')
    INSERT [Hourly_Statistics_Types] ( HST_Order_ID, HST_Type, HST_Desc )
    SELECT 22, 'NON_CASHABLE_EFT_IN', 'NON_CASHABLE_EFT_IN'
ELSE
    UPDATE [Hourly_Statistics_Types]
    SET    HST_Order_ID = 22, HST_Type = 'NON_CASHABLE_EFT_IN', HST_Desc = 'NON_CASHABLE_EFT_IN'
    WHERE  HST_Type = 'NON_CASHABLE_EFT_IN'

IF NOT EXISTS(SELECT 1 FROM [Hourly_Statistics_Types]WHERE HST_Type = 'CASHABLE_EFT_IN')
    INSERT [Hourly_Statistics_Types] ( HST_Order_ID, HST_Type, HST_Desc )
    SELECT 23, 'CASHABLE_EFT_IN', 'CASHABLE_EFT_IN'
ELSE
    UPDATE [Hourly_Statistics_Types]
    SET    HST_Order_ID = 23, HST_Type = 'CASHABLE_EFT_IN', HST_Desc = 'CASHABLE_EFT_IN'
    WHERE  HST_Type = 'CASHABLE_EFT_IN'

IF NOT EXISTS(SELECT 1 FROM [Hourly_Statistics_Types]WHERE HST_Type = 'NON_CASHABLE_VOUCHERS_IN_QTY')
    INSERT [Hourly_Statistics_Types] ( HST_Order_ID, HST_Type, HST_Desc )
    SELECT 24, 'NON_CASHABLE_VOUCHERS_IN_QTY', 'NON_CASHABLE_VOUCHERS_IN_QTY'
ELSE
    UPDATE [Hourly_Statistics_Types]
    SET    HST_Order_ID = 24, HST_Type = 'NON_CASHABLE_VOUCHERS_IN_QTY', HST_Desc = 'NON_CASHABLE_VOUCHERS_IN_QTY'
    WHERE  HST_Type = 'NON_CASHABLE_VOUCHERS_IN_QTY'

IF NOT EXISTS(SELECT 1 FROM [Hourly_Statistics_Types]WHERE HST_Type = 'NON_CASHABLE_VOUCHERS_IN_VALUE')
    INSERT [Hourly_Statistics_Types] ( HST_Order_ID, HST_Type, HST_Desc )
    SELECT 25, 'NON_CASHABLE_VOUCHERS_IN_VALUE', 'NON_CASHABLE_VOUCHERS_IN_VALUE'
ELSE
    UPDATE [Hourly_Statistics_Types]
    SET    HST_Order_ID = 25, HST_Type = 'NON_CASHABLE_VOUCHERS_IN_VALUE', HST_Desc = 'NON_CASHABLE_VOUCHERS_IN_VALUE'
    WHERE  HST_Type = 'NON_CASHABLE_VOUCHERS_IN_VALUE'

IF NOT EXISTS(SELECT 1 FROM [Hourly_Statistics_Types]WHERE HST_Type = 'NON_CASHABLE_VOUCHERS_OUT_QTY')
    INSERT [Hourly_Statistics_Types] ( HST_Order_ID, HST_Type, HST_Desc )
    SELECT 26, 'NON_CASHABLE_VOUCHERS_OUT_QTY', 'NON_CASHABLE_VOUCHERS_OUT_QTY'
ELSE
    UPDATE [Hourly_Statistics_Types]
    SET    HST_Order_ID = 26, HST_Type = 'NON_CASHABLE_VOUCHERS_OUT_QTY', HST_Desc = 'NON_CASHABLE_VOUCHERS_OUT_QTY'
    WHERE  HST_Type = 'NON_CASHABLE_VOUCHERS_OUT_QTY'

IF NOT EXISTS(SELECT 1 FROM [Hourly_Statistics_Types]WHERE HST_Type = 'NON_CASHABLE_VOUCHERS_OUT_VALUE')
    INSERT [Hourly_Statistics_Types] ( HST_Order_ID, HST_Type, HST_Desc )
    SELECT 27, 'NON_CASHABLE_VOUCHERS_OUT_VALUE', 'NON_CASHABLE_VOUCHERS_OUT_VALUE'
ELSE
    UPDATE [Hourly_Statistics_Types]
    SET    HST_Order_ID = 27, HST_Type = 'NON_CASHABLE_VOUCHERS_OUT_VALUE', HST_Desc = 'NON_CASHABLE_VOUCHERS_OUT_VALUE'
    WHERE  HST_Type = 'NON_CASHABLE_VOUCHERS_OUT_VALUE'

IF NOT EXISTS(SELECT 1 FROM [Hourly_Statistics_Types]WHERE HST_Type = 'PROMO_CASHABLE_EFT_OUT')
    INSERT [Hourly_Statistics_Types] ( HST_Order_ID, HST_Type, HST_Desc )
    SELECT 24, 'PROMO_CASHABLE_EFT_OUT', 'PROMO_CASHABLE_EFT_OUT'
ELSE
    UPDATE [Hourly_Statistics_Types]
    SET    HST_Order_ID = 24, HST_Type = 'PROMO_CASHABLE_EFT_OUT', HST_Desc = 'PROMO_CASHABLE_EFT_OUT'
    WHERE  HST_Type = 'PROMO_CASHABLE_EFT_OUT'

IF NOT EXISTS(SELECT 1 FROM [Hourly_Statistics_Types]WHERE HST_Type = 'NON_CASHABLE_EFT_OUT')
    INSERT [Hourly_Statistics_Types] ( HST_Order_ID, HST_Type, HST_Desc )
    SELECT 25, 'NON_CASHABLE_EFT_OUT', 'NON_CASHABLE_EFT_OUT'
ELSE
    UPDATE [Hourly_Statistics_Types]
    SET    HST_Order_ID = 25, HST_Type = 'NON_CASHABLE_EFT_OUT', HST_Desc = 'NON_CASHABLE_EFT_OUT'
    WHERE  HST_Type = 'NON_CASHABLE_EFT_OUT'

IF NOT EXISTS(SELECT 1 FROM [Hourly_Statistics_Types]WHERE HST_Type = 'CASHABLE_EFT_OUT')
    INSERT [Hourly_Statistics_Types] ( HST_Order_ID, HST_Type, HST_Desc )
    SELECT 26, 'CASHABLE_EFT_OUT', 'CASHABLE_EFT_OUT'
ELSE
    UPDATE [Hourly_Statistics_Types]
    SET    HST_Order_ID = 26, HST_Type = 'CASHABLE_EFT_OUT', HST_Desc = 'CASHABLE_EFT_OUT'
    WHERE  HST_Type = 'CASHABLE_EFT_OUT'

IF NOT EXISTS(SELECT 1 FROM [Hourly_Statistics_Types]WHERE HST_Type = 'BILLS_IN')
    INSERT [Hourly_Statistics_Types] ( HST_Order_ID, HST_Type, HST_Desc )
    SELECT 31, 'BILLS_IN', 'BILLS IN'
ELSE
    UPDATE [Hourly_Statistics_Types]
    SET    HST_Order_ID = 31, HST_Type = 'BILLS_IN', HST_Desc = 'BILLS IN'
    WHERE  HST_Type = 'BILLS_IN'

GO