USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Read]') AND name = N'Read_Collection_ID')
DROP INDEX [Read_Collection_ID] ON [dbo].[Read] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Read_Collection_ID] ON [dbo].[Read] 
(
	[Collection_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Read]') AND name = N'Read_Diary_Entry_ID')
DROP INDEX [Read_Diary_Entry_ID] ON [dbo].[Read] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Read_Diary_Entry_ID] ON [dbo].[Read] 
(
	[Diary_Entry_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Read]') AND name = N'Read_ID')
DROP INDEX [Read_ID] ON [dbo].[Read] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE UNIQUE NONCLUSTERED INDEX [Read_ID] ON [dbo].[Read] 
(
	[Read_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Read]') AND name = N'Read_Installation_ID')
DROP INDEX [Read_Installation_ID] ON [dbo].[Read] WITH ( ONLINE = OFF )
GO

--USE [Enterprise]
--GO

--CREATE NONCLUSTERED INDEX [Read_Installation_ID] ON [dbo].[Read] 
--(
--	[Installation_ID] ASC
--)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
--GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Read]') AND name = N'Read_Operator_Period_ID')
DROP INDEX [Read_Operator_Period_ID] ON [dbo].[Read] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Read_Operator_Period_ID] ON [dbo].[Read] 
(
	[Operator_Period_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Read]') AND name = N'Read_Operator_Week_ID')
DROP INDEX [Read_Operator_Week_ID] ON [dbo].[Read] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Read_Operator_Week_ID] ON [dbo].[Read] 
(
	[Operator_Week_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO


IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Read]') AND name = N'IDX_Read_ReadDate')
	DROP INDEX [IDX_Read_ReadDate] ON [dbo].[Read] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [IDX_Read_ReadDate] ON [dbo].[Read] 
(
	[ReadDate] ASC
)
INCLUDE ( [Read_ID],
[Installation_ID],
[Collection_ID],
[Read_Date],
[Read_Time],
[Previous_Read_Date],
[Previous_Read_Time],
[Read_Session],
[Read_Interrogator],
[Read_Machine_Code],
[PAYOUT_FLOAT_TOKEN],
[PAYOUT_FLOAT_10P],
[PAYOUT_FLOAT_20P],
[PAYOUT_FLOAT_50P],
[PAYOUT_FLOAT_100P],
[CASH_IN_2P],
[CASH_IN_5P],
[CASH_IN_10P],
[CASH_IN_20P],
[CASH_IN_50P],
[CASH_IN_100P],
[CASH_IN_200P],
[CASH_IN_500P],
[CASH_IN_1000P],
[CASH_IN_2000P],
[CASH_IN_5000P],
[CASH_IN_10000P],
[CASH_IN_20000P],
[CASH_IN_50000P],
[CASH_IN_100000P],
[CASH_IN_200000P],
[CASH_IN_500000P],
[CASH_IN_1000000P],
[TOKEN_IN_5P],
[TOKEN_IN_10P],
[TOKEN_IN_20P],
[TOKEN_IN_50P],
[TOKEN_IN_100P],
[TOKEN_IN_200P],
[TOKEN_IN_500P],
[TOKEN_IN_1000P],
[CASH_OUT_2P],
[CASH_OUT_5P],
[CASH_OUT_10P],
[CASH_OUT_20P],
[CASH_OUT_50P],
[CASH_OUT_100P],
[CASH_OUT_200P],
[CASH_OUT_500P],
[CASH_OUT_1000P],
[CASH_OUT_2000P],
[CASH_OUT_5000P],
[CASH_OUT_10000P],
[CASH_OUT_20000P],
[CASH_OUT_50000P],
[CASH_OUT_100000P],
[CASH_OUT_200000P],
[CASH_OUT_500000P],
[CASH_OUT_1000000P],
[TOKEN_OUT_5P],
[TOKEN_OUT_10P],
[TOKEN_OUT_20P],
[TOKEN_OUT_50P],
[TOKEN_OUT_100P],
[TOKEN_OUT_200P],
[TOKEN_OUT_500P],
[TOKEN_OUT_1000P],
[CASH_REFILL_5P],
[CASH_REFILL_10P],
[CASH_REFILL_20P],
[CASH_REFILL_50P],
[CASH_REFILL_100P],
[CASH_REFILL_200P],
[CASH_REFILL_500P],
[CASH_REFILL_1000P],
[CASH_REFILL_2000P],
[CASH_REFILL_5000P],
[CASH_REFILL_10000P],
[CASH_REFILL_20000P],
[CASH_REFILL_50000P],
[CASH_REFILL_100000P],
[CASH_REFILL_200000P],
[CASH_REFILL_500000P],
[CASH_REFILL_1000000P],
[TOKEN_REFILL_5P],
[TOKEN_REFILL_10P],
[TOKEN_REFILL_20P],
[TOKEN_REFILL_50P],
[TOKEN_REFILL_100P],
[TOKEN_REFILL_200P],
[TOKEN_REFILL_500P],
[TOKEN_REFILL_1000P],
[CREDITS_TAKEN],
[Hours_On],
[Read_Amedis_Uploaded],
[Read_honeyframe_EDI_Uploaded],
[Read_Declaration_AWPSerialNumber],
[Read_Declaration_MachineDays],
[Read_Declaration_TokenFloatPos],
[Read_Declaration_TokenFloatNeg],
[Read_Declaration_Rent],
[Read_Declaration_Cash50p],
[Read_Declaration_Cash100p],
[Read_Declaration_DisposableCash],
[Read_Declaration_TokensRedeemed],
[Read_Declaration_Sundries],
[Read_Declaration_NetBalance],
[Read_Declaration_TypeOfTrade],
[Read_Declaration_Faults],
[Datapak_Serial],
[Diary_Entry_ID],
[Read_RDC_VTP],
[Read_RDC_Secondary_Machine_Code],
[Read_RDC_SiteCode],
[READ_COINS_IN],
[READ_COINS_OUT],
[READ_COIN_DROP],
[READ_HANDPAY],
[READ_EXTERNAL_CREDIT],
[READ_GAMES_BET],
[READ_GAMES_WON],
[READ_NOTES],
[VTP],
[READ_RDC_CANCELLED_CREDITS],
[READ_RDC_GAMES_LOST],
[READ_RDC_GAMES_SINCE_POWER_UP],
[READ_RDC_TRUE_COIN_IN],
[READ_RDC_TRUE_COIN_OUT],
[READ_RDC_CURRENT_CREDITS],
[READ_RDC_BILL_1],
[READ_RDC_BILL_2],
[READ_RDC_BILL_5],
[READ_RDC_BILL_10],
[READ_RDC_BILL_20],
[READ_RDC_BILL_50],
[READ_RDC_BILL_100],
[READ_RDC_BILL_250],
[READ_RDC_BILL_10000],
[READ_RDC_BILL_20000],
[READ_RDC_BILL_50000],
[READ_RDC_BILL_100000],
[READ_TICKET],
[READ_TICKET_VALUE],
[READ_TICKET_IN_SUSPENSE],
[READ_TICKET_IN_SUSPENSE_VALUE],
[Read_Forced],
[READ_RDC_Datapak_Type],
[READ_RDC_Datapak_Version],
[Read_Occurrence],
[Week_ID],
[Period_ID],
[Operator_Week_ID],
[Operator_Period_ID],
[progressive_win_value],
[progressive_win_Handpay_value],
[Read_Days],
[READ_RDC_JACKPOT],
[Mystery_Machine_Paid],
[Mystery_Attendant_Paid],
[TICKETS_INSERTED_NONCASHABLE_VALUE],
[TICKETS_PRINTED_NONCASHABLE_VALUE],
[Promo_Cashable_EFT_IN],
[Promo_Cashable_EFT_OUT],
[NonCashable_EFT_IN],
[NonCashable_EFT_OUT],
[Cashable_EFT_IN],
[Cashable_EFT_OUT],
[READ_RDC_BILL_200],
[READ_RDC_BILL_500]) WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Read]') AND name = N'Read_Installation_ID')
DROP INDEX [Read_Installation_ID] ON [dbo].[Read] WITH ( ONLINE = OFF )
GO

/****** Object:  Index [Read_Installation_ID]    Script Date: 12/10/2013 18:55:37 ******/
CREATE NONCLUSTERED INDEX [Read_Installation_ID] ON [dbo].[Read] 
(
	[Installation_ID] ASC
)
INCLUDE ( [READ_COINS_IN],
[READ_COINS_OUT],
[READ_COIN_DROP],
[READ_HANDPAY],
[READ_GAMES_BET],
[READ_RDC_TRUE_COIN_IN],
[READ_RDC_TRUE_COIN_OUT],
[READ_RDC_BILL_1],
[READ_RDC_BILL_2],
[READ_RDC_BILL_5],
[READ_RDC_BILL_10],
[READ_RDC_BILL_20],
[READ_RDC_BILL_50],
[READ_RDC_BILL_100],
[READ_RDC_BILL_250],
[READ_RDC_BILL_10000],
[READ_RDC_BILL_20000],
[READ_RDC_BILL_50000],
[READ_RDC_BILL_100000],
[READ_TICKET],
[READ_TICKET_VALUE],
[READ_TICKET_IN_SUSPENSE],
[Read_Days],
[READ_RDC_JACKPOT],
[TICKETS_INSERTED_NONCASHABLE_VALUE],
[TICKETS_PRINTED_NONCASHABLE_VALUE],
[Promo_Cashable_EFT_IN],
[Promo_Cashable_EFT_OUT],
[NonCashable_EFT_IN],
[NonCashable_EFT_OUT],
[Cashable_EFT_IN],
[Cashable_EFT_OUT],
[READ_RDC_BILL_200],
[READ_RDC_BILL_500],
[ReadDate],
[Read_Date],
[Previous_Read_Date]) WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO


DBCC DBREINDEX('[dbo].[Read]' ,' ',90) 