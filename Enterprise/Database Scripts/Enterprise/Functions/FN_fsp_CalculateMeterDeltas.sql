USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fsp_CalculateMeterDeltas]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[fsp_CalculateMeterDeltas]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


  
---------------------------------------------------------------------------           
--          
-- Description: Calculate Meter History Deltas for a given range of           
--          
-- Inputs:                
--          
-- Outputs:               
--          
-- =======================================================================          
--           
-- Revision History          
--           
-- Anuradha	28 May 2010	Created  
---------------------------------------------------------------------------           
         

CREATE Function [dbo].fsp_CalculateMeterDeltas                
(  @Start_ID        int,                  
  @End_ID          int,                  
  @Installation_No int,                  
  @Process         varchar(10) = NULL,                  
  @LinkReference   int = 0,                  
  @Reference       varchar(20) = NULL,                  
  @Asset           varchar(20) = NULL,        
  @MeterType    varchar(50) = NULL                 
        
 )           
RETURNS INT              
        
AS          
      
BEGIN              
--  SET NOCOUNT ON                  
                   
  DECLARE @MAX_ID INT                  
  SET @MAX_ID = 2147483647 -- maximum value for INT                  
                  
  DECLARE @PreUpdateStart_ID INT                  
  declare @oldStart_ID INT                  
                  
  declare @workingid int          
  declare @Result int        
      

        
DECLARE @myWorkingDelta TABLE        
        
(        
 [MH_ID] [int]  NOT NULL,        
 [MH_Process] [varchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,        
 [MH_Type] [varchar](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,        
 [MH_LinkReference] [int] NULL,        
 [MH_Reference] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,        
 [MH_Installation_No] [int] NULL,        
 [MH_PAYOUT_FLOAT_TOKEN] [int] NULL,        
 [MH_PAYOUT_FLOAT_10P] [int] NULL,        
 [MH_PAYOUT_FLOAT_20P] [int] NULL,        
 [MH_PAYOUT_FLOAT_50P] [int] NULL,        
 [MH_PAYOUT_FLOAT_100P] [int] NULL,       
 [MH_CASH_IN_2P] [int] NULL,        
 [MH_CASH_IN_5P] [int] NULL,        
 [MH_CASH_IN_10P] [int] NULL,        
 [MH_CASH_IN_20P] [int] NULL,        
 [MH_CASH_IN_50P] [int] NULL,        
 [MH_CASH_IN_100P] [int] NULL,        
 [MH_CASH_IN_200P] [int] NULL,        
 [MH_CASH_IN_500P] [int] NULL,        
 [MH_CASH_IN_1000P] [int] NULL,        
 [MH_CASH_IN_2000P] [int] NULL,        
 [MH_CASH_IN_5000P] [int] NULL,        
 [MH_CASH_IN_10000P] [int] NULL,        
 [MH_CASH_IN_20000P] [int] NULL,        
 [MH_CASH_IN_50000P] [int] NULL,        
 [MH_CASH_IN_100000P] [int] NULL,        
 [MH_TOKEN_IN_5P] [int] NULL,        
 [MH_TOKEN_IN_10P] [int] NULL,        
 [MH_TOKEN_IN_20P] [int] NULL,        
 [MH_TOKEN_IN_50P] [int] NULL,        
 [MH_TOKEN_IN_100P] [int] NULL,        
 [MH_TOKEN_IN_200P] [int] NULL,        
 [MH_TOKEN_IN_500P] [int] NULL,        
 [MH_TOKEN_IN_1000P] [int] NULL,    
 [MH_CASH_OUT_2P] [int] NULL,        
 [MH_CASH_OUT_5P] [int] NULL,        
 [MH_CASH_OUT_10P] [int] NULL,        
 [MH_CASH_OUT_20P] [int] NULL,        
 [MH_CASH_OUT_50P] [int] NULL,        
 [MH_CASH_OUT_100P] [int] NULL,        
 [MH_CASH_OUT_200P] [int] NULL,        
 [MH_CASH_OUT_500P] [int] NULL,        
 [MH_CASH_OUT_1000P] [int] NULL,        
 [MH_CASH_OUT_2000P] [int] NULL,        
 [MH_CASH_OUT_5000P] [int] NULL,        
 [MH_CASH_OUT_10000P] [int] NULL,        
 [MH_CASH_OUT_20000P] [int] NULL,        
 [MH_CASH_OUT_50000P] [int] NULL,        
 [MH_CASH_OUT_100000P] [int] NULL,        
 [MH_TOKEN_OUT_5P] [int] NULL,        
 [MH_TOKEN_OUT_10P] [int] NULL,        
 [MH_TOKEN_OUT_20P] [int] NULL,        
 [MH_TOKEN_OUT_50P] [int] NULL,        
 [MH_TOKEN_OUT_100P] [int] NULL,        
 [MH_TOKEN_OUT_200P] [int] NULL,        
 [MH_TOKEN_OUT_500P] [int] NULL,        
 [MH_TOKEN_OUT_1000P] [int] NULL,  
 [MH_CASH_REFILL_5P] [int] NULL,  
 [MH_CASH_REFILL_10P] [int] NULL,        
 [MH_CASH_REFILL_20P] [int] NULL,        
 [MH_CASH_REFILL_50P] [int] NULL,        
 [MH_CASH_REFILL_100P] [int] NULL,        
 [MH_CASH_REFILL_200P] [int] NULL,        
 [MH_CASH_REFILL_500P] [int] NULL,        
 [MH_CASH_REFILL_1000P] [int] NULL,        
 [MH_CASH_REFILL_2000P] [int] NULL,        
 [MH_CASH_REFILL_5000P] [int] NULL,        
 [MH_CASH_REFILL_10000P] [int] NULL,        
 [MH_CASH_REFILL_20000P] [int] NULL,        
 [MH_CASH_REFILL_50000P] [int] NULL,        
 [MH_CASH_REFILL_100000P] [int] NULL,        
 [MH_TOKEN_REFILL_5P] [int] NULL,        
 [MH_TOKEN_REFILL_10P] [int] NULL,        
 [MH_TOKEN_REFILL_20P] [int] NULL,        
 [MH_TOKEN_REFILL_50P] [int] NULL,        
 [MH_TOKEN_REFILL_100P] [int] NULL,        
 [MH_TOKEN_REFILL_200P] [int] NULL,        
 [MH_TOKEN_REFILL_500P] [int] NULL,        
 [MH_TOKEN_REFILL_1000P] [int] NULL,        
 [MH_COINS_IN] [int] NULL,        
 [MH_COINS_OUT] [int] NULL,        
 [MH_COIN_DROP] [int] NULL,        
 [MH_HANDPAY] [int] NULL,        
 [MH_EXTERNAL_CREDIT] [int] NULL,        
 [MH_GAMES_BET] [int] NULL,        
 [MH_GAMES_WON] [int] NULL,        
 [MH_NOTES] [int] NULL,        
 [MH_VTP] [int] NULL,        
 [MH_CANCELLED_CREDITS] [int] NULL,        
 [MH_GAMES_LOST] [int] NULL,        
 [MH_GAMES_SINCE_POWER_UP] [int] NULL,        
 [MH_TRUE_COIN_IN] [int] NULL,        
 [MH_TRUE_COIN_OUT] [int] NULL,        
 [MH_CURRENT_CREDITS] [int] NULL,        
 [MH_JACKPOT] [int] NULL,        
 [MH_BILL_1] [int] NULL,        
 [MH_BILL_2] [int] NULL,        
 [MH_BILL_5] [int] NULL,        
 [MH_BILL_10] [int] NULL,        
 [MH_BILL_20] [int] NULL,        
 [MH_BILL_50] [int] NULL,        
 [MH_BILL_100] [int] NULL,        
 [MH_BILL_250] [int] NULL,        
 [MH_BILL_10000] [int] NULL,        
 [MH_BILL_20000] [int] NULL,        
 [MH_BILL_50000] [int] NULL,        
 [MH_BILL_100000] [int] NULL,        
 [MH_TICKET_PRINTED_QTY] [int] NULL,        
 [MH_TICKET_PRINTED_VALUE] [int] NULL,        
 [MH_TICKET_INSERTED_QTY] [int] NULL,        
 [MH_TICKET_INSERTED_VALUE] [int] NULL,        
 [MH_Datetime] [varchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,        
 [MH_progressive_win_value] [int] NULL,        
 [MH_progressive_win_Handpay_value] [int] NULL,        
 [MH_Mystery_Machine_Paid] [int] NULL,        
 [MH_Mystery_Attendant_Paid] [int] NULL,        
 [MH_TICKETS_PRINTED_NONCASHABLE_QTY] [int] NULL,        
 [MH_TICKETS_PRINTED_NONCASHABLE_VALUE] [int] NULL,        
 [MH_TICKETS_INSERTED_NONCASHABLE_QTY] [int] NULL,        
 [MH_TICKETS_INSERTED_NONCASHABLE_VALUE] [int] NULL,        
 [MH_Promo_Cashable_EFT_IN] [int] NULL,        
 [MH_Promo_Cashable_EFT_OUT] [int] NULL,        
 [MH_NonCashable_EFT_IN] [int] NULL,        
 [MH_NonCashable_EFT_OUT] [int] NULL,        
 [MH_Cashable_EFT_IN] [int] NULL,        
 [MH_Cashable_EFT_OUT] [int] NULL,        
 [MH_BILL_200] [int] NULL,        
 [MH_BILL_500] [int] NULL ,
 [MH_CASH_IN_1P] [int] NULL,
 [MH_CASH_OUT_1P] [int] NULL           
)        
        
insert into @myWorkingDelta  select  top 1 * from meter_history        



 --select top 1 * into @myWorkingDelta from meter_history        
  select @workingid = MAX(mh_id) from @myWorkingDelta                  
                  
  IF COALESCE(@Asset,'') = ''                  
    SET @Asset = NULL                  
                  
  SET @oldStart_ID = @Start_ID                  
       
-- create our working area                  
DECLARE @Calc TABLE                   
(                  
 [cMH_ID] [int] NOT NULL PRIMARY KEY,                  
 [cMH_PAYOUT_FLOAT_TOKEN] [int] NULL DEFAULT(0) ,                  
 [cMH_PAYOUT_FLOAT_10P] [int] NULL DEFAULT(0),                  
 [cMH_PAYOUT_FLOAT_20P] [int] NULL DEFAULT(0),                  
 [cMH_PAYOUT_FLOAT_50P] [int] NULL DEFAULT(0),                  
 [cMH_PAYOUT_FLOAT_100P] [int] NULL DEFAULT(0),  
  [cMH_CASH_IN_1P] [int] NULL DEFAULT(0),                     
 [cMH_CASH_IN_2P] [int] NULL DEFAULT(0),                  
 [cMH_CASH_IN_5P] [int] NULL DEFAULT(0),                  
 [cMH_CASH_IN_10P] [int] NULL DEFAULT(0),                  
 [cMH_CASH_IN_20P] [int] NULL DEFAULT(0),                  
 [cMH_CASH_IN_50P] [int] NULL DEFAULT(0),                  
 [cMH_CASH_IN_100P] [int] NULL DEFAULT(0),       
 [cMH_CASH_IN_200P] [int] NULL DEFAULT(0),                  
 [cMH_CASH_IN_500P] [int] NULL DEFAULT(0),                  
 [cMH_CASH_IN_1000P] [int] NULL DEFAULT(0),                  
 [cMH_CASH_IN_2000P] [int] NULL DEFAULT(0),                  
 [cMH_CASH_IN_5000P] [int] NULL DEFAULT(0),                  
 [cMH_CASH_IN_10000P] [int] NULL DEFAULT(0),                  
 [cMH_CASH_IN_20000P] [int] NULL DEFAULT(0),                  
 [cMH_CASH_IN_50000P] [int] NULL DEFAULT(0),                  
 [cMH_CASH_IN_100000P] [int] NULL DEFAULT(0),                  
 [cMH_TOKEN_IN_5P] [int] NULL DEFAULT(0),                  
 [cMH_TOKEN_IN_10P] [int] NULL DEFAULT(0),                  
 [cMH_TOKEN_IN_20P] [int] NULL DEFAULT(0),                  
 [cMH_TOKEN_IN_50P] [int] NULL DEFAULT(0),                  
 [cMH_TOKEN_IN_100P] [int] NULL DEFAULT(0),                  
 [cMH_TOKEN_IN_200P] [int] NULL DEFAULT(0),                  
 [cMH_TOKEN_IN_500P] [int] NULL DEFAULT(0),           
 [cMH_TOKEN_IN_1000P] [int] NULL DEFAULT(0),  
 [cMH_CASH_OUT_1P] [int] NULL DEFAULT(0),                  
 [cMH_CASH_OUT_2P] [int] NULL DEFAULT(0),                  
 [cMH_CASH_OUT_5P] [int] NULL DEFAULT(0),                  
 [cMH_CASH_OUT_10P] [int] NULL DEFAULT(0),                  
 [cMH_CASH_OUT_20P] [int] NULL DEFAULT(0),                  
 [cMH_CASH_OUT_50P] [int] NULL DEFAULT(0),                  
 [cMH_CASH_OUT_100P] [int] NULL DEFAULT(0),                  
 [cMH_CASH_OUT_200P] [int] NULL DEFAULT(0),                  
 [cMH_CASH_OUT_500P] [int] NULL DEFAULT(0),                  
 [cMH_CASH_OUT_1000P] [int] NULL DEFAULT(0),                  
 [cMH_CASH_OUT_2000P] [int] NULL DEFAULT(0),                  
[cMH_CASH_OUT_5000P] [int] NULL DEFAULT(0),                  
 [cMH_CASH_OUT_10000P] [int] NULL DEFAULT(0),                  
 [cMH_CASH_OUT_20000P] [int] NULL DEFAULT(0),                  
 [cMH_CASH_OUT_50000P] [int] NULL DEFAULT(0),                  
 [cMH_CASH_OUT_100000P] [int] NULL DEFAULT(0),                  
 [cMH_TOKEN_OUT_5P] [int] NULL DEFAULT(0),                  
 [cMH_TOKEN_OUT_10P] [int] NULL DEFAULT(0),                  
 [cMH_TOKEN_OUT_20P] [int] NULL DEFAULT(0),                  
 [cMH_TOKEN_OUT_50P] [int] NULL DEFAULT(0),                  
 [cMH_TOKEN_OUT_100P] [int] NULL DEFAULT(0),                  
 [cMH_TOKEN_OUT_200P] [int] NULL DEFAULT(0),                  
 [cMH_TOKEN_OUT_500P] [int] NULL DEFAULT(0),                  
 [cMH_TOKEN_OUT_1000P] [int] NULL DEFAULT(0),                  
 [cMH_CASH_REFILL_5P] [int] NULL DEFAULT(0),                  
 [cMH_CASH_REFILL_10P] [int] NULL DEFAULT(0),                  
 [cMH_CASH_REFILL_20P] [int] NULL DEFAULT(0),                  
 [cMH_CASH_REFILL_50P] [int] NULL DEFAULT(0),                  
 [cMH_CASH_REFILL_100P] [int] NULL DEFAULT(0),                  
 [cMH_CASH_REFILL_200P] [int] NULL DEFAULT(0),                  
 [cMH_CASH_REFILL_500P] [int] NULL DEFAULT(0),                  
 [cMH_CASH_REFILL_1000P] [int] NULL DEFAULT(0),                  
 [cMH_CASH_REFILL_2000P] [int] NULL DEFAULT(0),                  
 [cMH_CASH_REFILL_5000P] [int] NULL DEFAULT(0),                  
 [cMH_CASH_REFILL_10000P] [int] NULL DEFAULT(0),                  
 [cMH_CASH_REFILL_20000P] [int] NULL DEFAULT(0),                  
 [cMH_CASH_REFILL_50000P] [int] NULL DEFAULT(0),                  
 [cMH_CASH_REFILL_100000P] [int] NULL DEFAULT(0),                  
 [cMH_TOKEN_REFILL_5P] [int] NULL DEFAULT(0),                  
 [cMH_TOKEN_REFILL_10P] [int] NULL DEFAULT(0),                  
 [cMH_TOKEN_REFILL_20P] [int] NULL DEFAULT(0),                  
 [cMH_TOKEN_REFILL_50P] [int] NULL DEFAULT(0),      
 [cMH_TOKEN_REFILL_100P] [int] NULL DEFAULT(0),                  
 [cMH_TOKEN_REFILL_200P] [int] NULL DEFAULT(0),                  
 [cMH_TOKEN_REFILL_500P] [int] NULL DEFAULT(0),                  
 [cMH_TOKEN_REFILL_1000P] [int] NULL DEFAULT(0),                  
 [cMH_COINS_IN] [int] NULL DEFAULT(0),                  
 [cMH_COINS_OUT] [int] NULL DEFAULT(0),                  
 [cMH_COIN_DROP] [int] NULL DEFAULT(0),                  
 [cMH_HANDPAY] [int] NULL DEFAULT(0),                  
 [cMH_EXTERNAL_CREDIT] [int] NULL DEFAULT(0),                  
 [cMH_GAMES_BET] [int] NULL DEFAULT(0),                  
 [cMH_GAMES_WON] [int] NULL DEFAULT(0),                  
 [cMH_NOTES] [int] NULL DEFAULT(0),                  
 [cMH_VTP] [int] NULL DEFAULT(0),                  
 [cMH_CANCELLED_CREDITS] [int] NULL DEFAULT(0),                  
 [cMH_GAMES_LOST] [int] NULL DEFAULT(0),                  
 [cMH_GAMES_SINCE_POWER_UP] [int] NULL DEFAULT(0),                  
 [cMH_TRUE_COIN_IN] [int] NULL DEFAULT(0),                  
 [cMH_TRUE_COIN_OUT] [int] NULL DEFAULT(0),                  
 [cMH_CURRENT_CREDITS] [int] NULL DEFAULT(0),                
 [cMH_JACKPOT] [int] NULL DEFAULT(0),                  
 [cMH_BILL_1] [int] NULL DEFAULT(0),                  
 [cMH_BILL_2] [int] NULL DEFAULT(0),                  
 [cMH_BILL_5] [int] NULL DEFAULT(0),                  
 [cMH_BILL_10] [int] NULL DEFAULT(0),                  
 [cMH_BILL_20] [int] NULL DEFAULT(0),                  
 [cMH_BILL_50] [int] NULL DEFAULT(0),                  
 [cMH_BILL_100] [int] NULL DEFAULT(0),                  
 [cMH_BILL_250] [int] NULL DEFAULT(0),                  
 [cMH_BILL_10000] [int] NULL DEFAULT(0),                  
 [cMH_BILL_20000] [int] NULL DEFAULT(0),                  
 [cMH_BILL_50000] [int] NULL DEFAULT(0),                  
 [cMH_BILL_100000] [int] NULL DEFAULT(0),                  
 [cMH_TICKET_PRINTED_QTY] [int] NULL DEFAULT(0),                  
 [cMH_TICKET_PRINTED_VALUE] [int] NULL DEFAULT(0),                  
 [cMH_TICKET_INSERTED_QTY] [int] NULL DEFAULT(0),                  
 [cMH_TICKET_INSERTED_VALUE] [bigint] NULL DEFAULT(0),                  
 [cMH_progressive_win_value] [int] NULL DEFAULT(0),                  
 [cMH_progressive_win_Handpay_value] [int] NULL DEFAULT(0),                  
 [cMH_Coins_Out_Actual] [int] NULL DEFAULT(0),          
 [cMH_Mystery_Machine_Paid] [int] NULL DEFAULT(0),          
 [cMH_Mystery_Attendant_Paid] [int] NULL DEFAULT(0),          
 [cMH_TICKETS_PRINTED_NONCASHABLE_QTY] [int] NULL DEFAULT(0),          
 [cMH_TICKETS_PRINTED_NONCASHABLE_VALUE] [int] NULL DEFAULT(0),          
 [cMH_TICKETS_INSERTED_NONCASHABLE_QTY] [int] NULL DEFAULT(0),          
 [cMH_TICKETS_INSERTED_NONCASHABLE_VALUE] [int] NULL DEFAULT(0),          
 [cMH_Promo_Cashable_EFT_IN] [int] NULL DEFAULT(0),          
 [cMH_Promo_Cashable_EFT_OUT] [int] NULL DEFAULT(0),          
 [cMH_NonCashable_EFT_IN] [int] NULL DEFAULT(0),          
 [cMH_NonCashable_EFT_OUT] [int] NULL DEFAULT(0),          
 [cMH_Cashable_EFT_IN] [int] NULL DEFAULT(0),          
 [cMH_Cashable_EFT_OUT] [int] NULL DEFAULT(0),        
 [cMH_BILL_200] [int] NULL DEFAULT(0),        
 [cMH_BILL_500] [int] NULL DEFAULT(0)        
)                  
          
DECLARE @TempMH TABLE                   
(                  
 [MH_ID] [int] NOT NULL PRIMARY KEY,                  
 [MH_Process] [varchar] (10) NOT NULL ,                  
 [MH_Type] [varchar] (1) NOT NULL ,                  
 [MH_LinkReference] [int] NULL ,                  
 [MH_Reference] [varchar] (255) NULL ,                  
 [MH_Installation_No] [int] NOT NULL ,                  
 [MH_PAYOUT_FLOAT_TOKEN] [int] NULL DEFAULT(0) ,                  
 [MH_PAYOUT_FLOAT_10P] [int] NULL DEFAULT(0),                  
 [MH_PAYOUT_FLOAT_20P] [int] NULL DEFAULT(0),                  
 [MH_PAYOUT_FLOAT_50P] [int] NULL DEFAULT(0),                  
 [MH_PAYOUT_FLOAT_100P] [int] NULL DEFAULT(0),  
 
 [MH_CASH_IN_2P] [int] NULL DEFAULT(0),                  
 [MH_CASH_IN_5P] [int] NULL DEFAULT(0),                  
 [MH_CASH_IN_10P] [int] NULL DEFAULT(0),                  
 [MH_CASH_IN_20P] [int] NULL DEFAULT(0),                  
 [MH_CASH_IN_50P] [int] NULL DEFAULT(0),                  
 [MH_CASH_IN_100P] [int] NULL DEFAULT(0),                  
 [MH_CASH_IN_200P] [int] NULL DEFAULT(0),                  
 [MH_CASH_IN_500P] [int] NULL DEFAULT(0),                  
 [MH_CASH_IN_1000P] [int] NULL DEFAULT(0),                  
 [MH_CASH_IN_2000P] [int] NULL DEFAULT(0),                  
 [MH_CASH_IN_5000P] [int] NULL DEFAULT(0),                  
 [MH_CASH_IN_10000P] [int] NULL DEFAULT(0),                  
 [MH_CASH_IN_20000P] [int] NULL DEFAULT(0),                  
 [MH_CASH_IN_50000P] [int] NULL DEFAULT(0),                  
 [MH_CASH_IN_100000P] [int] NULL DEFAULT(0),                  
 [MH_TOKEN_IN_5P] [int] NULL DEFAULT(0),                  
 [MH_TOKEN_IN_10P] [int] NULL DEFAULT(0),                  
 [MH_TOKEN_IN_20P] [int] NULL DEFAULT(0),                  
 [MH_TOKEN_IN_50P] [int] NULL DEFAULT(0),                  
 [MH_TOKEN_IN_100P] [int] NULL DEFAULT(0),                  
 [MH_TOKEN_IN_200P] [int] NULL DEFAULT(0),                  
 [MH_TOKEN_IN_500P] [int] NULL DEFAULT(0),                  
                  
 [MH_TOKEN_IN_1000P] [int] NULL DEFAULT(0),    
 
 [MH_CASH_OUT_2P] [int] NULL DEFAULT(0),                  
 [MH_CASH_OUT_5P] [int] NULL DEFAULT(0),            
 [MH_CASH_OUT_10P] [int] NULL DEFAULT(0),                  
 [MH_CASH_OUT_20P] [int] NULL DEFAULT(0),                  
 [MH_CASH_OUT_50P] [int] NULL DEFAULT(0),                  
 [MH_CASH_OUT_100P] [int] NULL DEFAULT(0),                  
 [MH_CASH_OUT_200P] [int] NULL DEFAULT(0),                  
 [MH_CASH_OUT_500P] [int] NULL DEFAULT(0),                  
 [MH_CASH_OUT_1000P] [int] NULL DEFAULT(0),                  
 [MH_CASH_OUT_2000P] [int] NULL DEFAULT(0),                  
 [MH_CASH_OUT_5000P] [int] NULL DEFAULT(0),                  
 [MH_CASH_OUT_10000P] [int] NULL DEFAULT(0),                  
 [MH_CASH_OUT_20000P] [int] NULL DEFAULT(0),                  
 [MH_CASH_OUT_50000P] [int] NULL DEFAULT(0),                  
 [MH_CASH_OUT_100000P] [int] NULL DEFAULT(0),                  
 [MH_TOKEN_OUT_5P] [int] NULL DEFAULT(0),                  
 [MH_TOKEN_OUT_10P] [int] NULL DEFAULT(0),            
 [MH_TOKEN_OUT_20P] [int] NULL DEFAULT(0),                  
 [MH_TOKEN_OUT_50P] [int] NULL DEFAULT(0),                  
 [MH_TOKEN_OUT_100P] [int] NULL DEFAULT(0),                  
 [MH_TOKEN_OUT_200P] [int] NULL DEFAULT(0),                  
 [MH_TOKEN_OUT_500P] [int] NULL DEFAULT(0),                  
 [MH_TOKEN_OUT_1000P] [int] NULL DEFAULT(0),                  
 [MH_CASH_REFILL_5P] [int] NULL DEFAULT(0),                  
 [MH_CASH_REFILL_10P] [int] NULL DEFAULT(0),                  
 [MH_CASH_REFILL_20P] [int] NULL DEFAULT(0),                  
 [MH_CASH_REFILL_50P] [int] NULL DEFAULT(0),                  
 [MH_CASH_REFILL_100P] [int] NULL DEFAULT(0),                  
 [MH_CASH_REFILL_200P] [int] NULL DEFAULT(0),                  
 [MH_CASH_REFILL_500P] [int] NULL DEFAULT(0),                  
 [MH_CASH_REFILL_1000P] [int] NULL DEFAULT(0),                  
 [MH_CASH_REFILL_2000P] [int] NULL DEFAULT(0),                  
 [MH_CASH_REFILL_5000P] [int] NULL DEFAULT(0),                  
 [MH_CASH_REFILL_10000P] [int] NULL DEFAULT(0),                  
 [MH_CASH_REFILL_20000P] [int] NULL DEFAULT(0),                  
 [MH_CASH_REFILL_50000P] [int] NULL DEFAULT(0),                  
 [MH_CASH_REFILL_100000P] [int] NULL DEFAULT(0),                  
 [MH_TOKEN_REFILL_5P] [int] NULL DEFAULT(0),                  
 [MH_TOKEN_REFILL_10P] [int] NULL DEFAULT(0),                  
 [MH_TOKEN_REFILL_20P] [int] NULL DEFAULT(0),                  
 [MH_TOKEN_REFILL_50P] [int] NULL DEFAULT(0),                  
 [MH_TOKEN_REFILL_100P] [int] NULL DEFAULT(0),                  
 [MH_TOKEN_REFILL_200P] [int] NULL DEFAULT(0),                  
 [MH_TOKEN_REFILL_500P] [int] NULL DEFAULT(0),                  
 [MH_TOKEN_REFILL_1000P] [int] NULL DEFAULT(0),                  
 [MH_COINS_IN] [int] NULL DEFAULT(0),                  
 [MH_COINS_OUT] [int] NULL DEFAULT(0),                  
 [MH_COIN_DROP] [int] NULL DEFAULT(0),                  
 [MH_HANDPAY] [int] NULL DEFAULT(0),                  
 [MH_EXTERNAL_CREDIT] [int] NULL DEFAULT(0),                  
 [MH_GAMES_BET] [int] NULL DEFAULT(0),                  
 [MH_GAMES_WON] [int] NULL DEFAULT(0),                  
 [MH_NOTES] [int] NULL DEFAULT(0),                  
 [MH_VTP] [int] NULL DEFAULT(0),                  
 [MH_CANCELLED_CREDITS] [int] NULL DEFAULT(0),                  
 [MH_GAMES_LOST] [int] NULL DEFAULT(0),                  
 [MH_GAMES_SINCE_POWER_UP] [int] NULL DEFAULT(0),                  
 [MH_TRUE_COIN_IN] [int] NULL DEFAULT(0),                  
 [MH_TRUE_COIN_OUT] [int] NULL DEFAULT(0),                  
 [MH_CURRENT_CREDITS] [int] NULL DEFAULT(0),                  
 [MH_JACKPOT] [int] NULL DEFAULT(0),                  
 [MH_BILL_1] [int] NULL DEFAULT(0),                  
 [MH_BILL_2] [int] NULL DEFAULT(0),                  
 [MH_BILL_5] [int] NULL DEFAULT(0),                  
 [MH_BILL_10] [int] NULL DEFAULT(0),                  
 [MH_BILL_20] [int] NULL DEFAULT(0),                  
 [MH_BILL_50] [int] NULL DEFAULT(0),                  
 [MH_BILL_100] [int] NULL DEFAULT(0),        
 [MH_BILL_250] [int] NULL DEFAULT(0),                  
 [MH_BILL_10000] [int] NULL DEFAULT(0),                  
 [MH_BILL_20000] [int] NULL DEFAULT(0),                  
 [MH_BILL_50000] [int] NULL DEFAULT(0),                  
 [MH_BILL_100000] [int] NULL DEFAULT(0),                  
 [MH_TICKET_PRINTED_QTY] [int] NULL DEFAULT(0),              
 [MH_TICKET_PRINTED_VALUE] [int] NULL DEFAULT(0),                  
 [MH_TICKET_INSERTED_QTY] [int] NULL DEFAULT(0),                  
 [MH_TICKET_INSERTED_VALUE] [bigint] NULL DEFAULT(0),                  
 [MH_Datetime] [datetime] NULL ,                  
 [MH_progressive_win_value] [int] NULL DEFAULT(0),                  
 [MH_progressive_win_Handpay_value] [int] NULL DEFAULT(0),                  
 [MH_Mystery_Machine_Paid] [int] NULL DEFAULT(0),          
 [MH_Mystery_Attendant_Paid] [int] NULL DEFAULT(0),          
 [MH_TICKETS_PRINTED_NONCASHABLE_QTY] [int] NULL DEFAULT(0),          
 [MH_TICKETS_PRINTED_NONCASHABLE_VALUE] [int] NULL DEFAULT(0),          
 [MH_TICKETS_INSERTED_NONCASHABLE_QTY] [int] NULL DEFAULT(0),          
 [MH_TICKETS_INSERTED_NONCASHABLE_VALUE] [int] NULL DEFAULT(0),          
 [MH_Promo_Cashable_EFT_IN] [int] NULL DEFAULT(0),          
 [MH_Promo_Cashable_EFT_OUT] [int] NULL DEFAULT(0),          
 [MH_NonCashable_EFT_IN] [int] NULL DEFAULT(0),          
 [MH_NonCashable_EFT_OUT] [int] NULL DEFAULT(0),          
 [MH_Cashable_EFT_IN] [int] NULL DEFAULT(0),          
 [MH_Cashable_EFT_OUT] [int] NULL DEFAULT(0),        
 [MH_BILL_200] [int] NULL DEFAULT(0),        
 [MH_BILL_500] [int] NULL DEFAULT(0) ,
 [MH_CASH_IN_1P] [int] NULL DEFAULT(0), 
 [MH_CASH_OUT_1P] [int] NULL DEFAULT(0)
)                  
                  
INSERT INTO @Calc ( cMH_ID, cMH_PAYOUT_FLOAT_TOKEN ) VALUES ( 1, 0 ) -- create the working record                  
                  
set @Process = COALESCE(@Process,'')                  
set @PreUpdateStart_ID = 0                  
     
-- find snap record                  
if @Process = 'SNAP' AND @Asset IS NULL                  
  select @End_ID = MH_ID                   
    from Meter_History WITH (NOLOCK)                   
   WHERE MH_Process = 'SNAP' AND MH_Installation_No = @Installation_No                  
                  
                  
-- get the last SNAP record for the asset for the latest installation no                  
if @Process = 'SNAP' AND @Asset IS NOT NULL                  
  select @End_ID = MAX(MH_ID)                  
    from Meter_History WITH (NOLOCK)                  
    JOIN Installation i WITH (NOLOCK)                  
      ON MH_Installation_No = i.Installation_ID                
    JOIN Machine m WITH (NOLOCK)                  
      ON m.Machine_ID   = i.Machine_ID                  
   WHERE MH_Process = 'SNAP'                   
     AND Machine_Stock_No = @Asset                  
     AND i.Installation_ID   = @installation_no                   
                  
-- an update has been done to a P record, find the original P record                  
if @Process <> 'SNAP' AND @Start_ID > @End_ID                  
BEGIN                  
  SELECT @PreUpdateStart_ID = MIN(beforeUpdate.MH_ID)                  
    FROM Meter_History WITH (NOLOCK)                  
    JOIN Meter_History beforeUpdate WITH (NOLOCK)                  
      ON ( beforeUpdate.MH_Process = Meter_History.MH_Process                  
           AND                  
        beforeUpdate.MH_Reference = Meter_History.MH_Reference                  
           AND                  
        beforeUpdate.MH_LinkReference = Meter_History.MH_LinkReference                  
           AND                  
        beforeUpdate.MH_Installation_No = Meter_History.MH_Installation_No                  
           AND                  
           beforeUpdate.MH_Type = Meter_History.MH_Type                  
           AND                  
           beforeUpdate.MH_ID < Meter_History.MH_ID                  
         )                  
   WHERE Meter_History.MH_ID = @Start_ID                  
END                  
                  
-- select @PreUpdateStart_ID                  
                  
-- create a list table                  
declare @MHIds table                   
  ( myID int identity(1,1) PRIMARY KEY,                   
    MH_ID int)                  
                  
--1. get the start record                  
IF @Asset IS NULL                   
  INSERT INTO @MHIds VALUES ( @Start_ID )                  
                  
-- using the MIN and MAX ids, find all RAMRESET / ROLLOVERS between the values                  
if @PreUpdateStart_ID > 0                   
  SET @Start_ID = @PreUpdateStart_ID                  
                  
declare @myendid int                  
set @myendid = CASE WHEN @Process <> 'SNAP' THEN @End_ID ELSE @MAX_ID END                  
                  
IF @Asset IS NULL                   
  INSERT INTO @MHIds (MH_ID)                  
       SELECT MH_ID                   
         FROM Meter_History WITH (NOLOCK)                  
        WHERE MH_ID BETWEEN @Start_ID AND @myendid                  
       AND MH_Process IN ( 'RAMRESET', 'ROLLOVER' )                  
          AND MH_Installation_No = @Installation_no                  
     ORDER BY MH_ID                  
                  
ELSE                  
  INSERT INTO @MHIds (MH_ID)                  
       SELECT MH_ID                   
         FROM Meter_History WITH (NOLOCK)                  
        WHERE MH_ID BETWEEN @Start_ID AND @myendid                  
          AND MH_Process IN ( 'RAMRESET','ROLLOVER', 'INST' )                  
          AND MH_Installation_No IN ( SELECT i.Installation_ID                   
                                        FROM Machine m WITH (NOLOCK)                  
       JOIN Installation i WITH (NOLOCK)                  
                                          ON ( i.Machine_ID  = m.Machine_ID AND Machine_stock_no = @asset )                   
                                    )                            
     ORDER BY MH_ID                  
                  
--1. dump the 1st record in temp table ( SNAP or anything )                   
INSERT INTO @MHIds VALUES ( @End_ID )                  
                  
-- select * from @MHIds                  
INSERT INTO @TempMH                  
  select meter_history.*                   
    from @MHIds m                   
    join meter_history WITH (NOLOCK)                  
      on m.MH_ID  = Meter_History.MH_ID                  
order by myid                  
                  
-- select * from @TempMH                  
                  
  -- create a list of linked MH_IDs .. 1,2 2,3 3,4 etc ..                  
declare @DoubleSorted table                   
  ( id1 int, id2 int )                  
                  
INSERT INTO @DoubleSorted                  
  select distinct                   
         id1 = mynumber1.myid,                   
         id2 = MIN(mynumber2.myid)                  
    from @MHIds mynumber1, @MHIds mynumber2                  
   where mynumber1.myid < mynumber2.myid                   
group by mynumber1.myid                  
                  
-- select * from @DoubleSorted                  
                  
-- now starting at the beginning of the list apply any changes to meters to a single delta table                  
---                  
DECLARE @id1        INT,                  
        @mh1process varchar(10),                  
   @mh1type    varchar(1),                  
        @id2        INT,                  
        @mh2process varchar(10),                  
        @mh2type    varchar(1)                  
                  
declare @MHHISTORY TABLE                  
(                   
  rownum int identity(1,1) NOT NULL PRIMARY KEY,                  
  id1 int, mh1type varchar(1), mh1process varchar(20),                   
  id2 int, mh2type varchar(1), mh2process varchar(20)                   
)                  
                  
INSERT INTO @MHHISTORY ( id1, mh1type, mh1process, id2, mh2type, mh2process )                   
  SELECT myfirst.mh_id, myfirst.mh_type, myfirst.mh_process,                  
         mysecond.mh_id, mysecond.mh_type, mysecond.mh_process                  
                  
    FROM @DoubleSorted                  
                  
    JOIN @MHIds id1                  
      ON id1 = id1.myid                  
                
    JOIN @MHIds id2                  
      ON id2 = id2.myid                  
                  
    JOIN @TempMH myfirst                  
      ON myfirst.mh_id = id1.mh_id                  
                  
    JOIN @TempMH  mysecond                  
      ON mysecond.mh_id = id2.mh_id                  
                  
ORDER BY id1                  
                  
-- select * from @MHHISTORY                  
                  
  declare @MaxRows int                  
  declare @rowcnt int                  
                    
  select @MaxRows=count(*) from @MHHISTORY                   
                    
  set @RowCnt = 1                  
  while @RowCnt <= @MaxRows                   
  begin                   
                  
    SELECT @id1        = id1,                  
           @mh1process = mh1process,                  
           @mh1type    = mh1type,                  
           @id2        = id2,                  
           @mh2process = mh2process,                  
           @mh2type    = mh2type                  
      FROM @MHHISTORY                  
     WHERE rownum = @RowCnt                  
                  
      -- select @id1, @mh1Type, @mh1process, @id2, @mh2Type, @mh2process                  
                  
      declare @bUpdate         int,                  
              @bHandleRollover int                  
                  
      SET @bUpdate = 0                  
      SET @bHandleRollover = 0                  
                  
      -- based on the type of record do the calculations                  
                  
    -- start of process to either end of                  
      IF ( @mh1process IN ( 'INST' ) AND @mh1type = 'P' )                   
      AND                  
         ( @mh2process IN ( 'INST' ) AND @mh2type = 'C' )                  
         SET @bUpdate = 1                  
                  
      IF ( @mh1process IN ( 'COLL', 'VTP', 'READ' ) AND @mh1type = 'P' )                   
      AND                  
         ( @mh2process IN ( 'COLL', 'VTP', 'READ' ) AND @mh2type = 'C' )                  
         SET @bUpdate = 1                  
                  
      IF ( @mh1process IN ( 'COLL', 'VTP', 'READ', 'INST' ) AND @mh1type = 'P' )                   
      AND                  
         ( @mh2process = 'RAMRESET' AND @mh2type = 'P' )                 
         SET @bUpdate = 1                  
                  
      IF ( @mh1process IN ( 'COLL', 'VTP', 'READ', 'INST' ) AND @mh1type = 'P' )                   
      AND                  
         ( @mh2process = 'ROLLOVER' AND @mh2type = 'P' )                  
         SET @bUpdate = 1                  
                  
    -- end of ramreset --> start of next ramreset                  
      IF ( @mh1process = 'RAMRESET' AND @mh1type = 'C' )                  
      AND                  
         ( @mh2process = 'RAMRESET' AND @mh2type = 'P' )                   
      SET @bUpdate = 1                  
                  
    -- rollover --> rollover                  
      IF ( @mh1process = 'ROLLOVER' AND @mh1type = 'C' )                  
      AND                  
         ( @mh2process = 'ROLLOVER' AND @mh2type = 'P' )                  
         SET @bUpdate = 1                  
                  
    -- end of ramreset --> start of rollover                  
      IF ( @mh1process = 'RAMRESET' AND @mh1type = 'C' )                  
      AND                  
         ( @mh2process = 'ROLLOVER' AND @mh2type = 'P' )                  
         SET @bUpdate = 1                  
                  
    -- rollover process                  
      IF ( @mh1process = 'ROLLOVER' AND @mh1type = 'P' )                  
      AND                  
      ( @mh2process = 'ROLLOVER' AND @mh2type = 'C' )                  
         BEGIN                  
           SET @bUpdate = 1                  
           SET @bHandleRollover = 1                  
         END                  
                   
    -- end of rollover --> start of ramreset                  
      IF ( @mh1process = 'ROLLOVER' AND @mh1type = 'C' )                  
      AND                  
         ( @mh2process = 'RAMRESET' AND @mh2type = 'P' )                  
         SET @bUpdate = 1                  
                  
    -- > SNAPs                  
      IF ( @mh1process IN ( 'COLL', 'VTP', 'READ', 'INST' ) AND @mh1type = 'P' )                   
      AND                  
         ( @mh2process IN ( 'SNAP' )  )                  
         SET @bUpdate = 1                  
                  
      IF ( @mh1process = 'RAMRESET' AND @mh1type = 'C' )                  
      AND                  
         ( @mh2process = 'SNAP' )                  
         SET @bUpdate = 1                  
                  
      IF ( @mh1process = 'ROLLOVER' AND @mh1type = 'C' )                  
      AND                  
         ( @mh2process = 'SNAP' )                  
         SET @bUpdate = 1                  
                  
    -- > END                  
      IF ( @mh1process = 'RAMRESET' AND @mh1type = 'C' )                   
      AND                  
         ( @mh2process IN ( 'COLL', 'VTP', 'READ' ) AND @mh2type = 'C' )                  
         SET @bUpdate = 1                  
                  
      IF ( @mh1process = 'ROLLOVER' AND @mh1type = 'C' )                   
      AND                  
         ( @mh2process IN ( 'COLL', 'VTP', 'READ' ) AND @mh2type = 'C' )                  
         SET @bUpdate = 1                  
                  
                   
                  
      IF ( @bUpdate = 1 )                   
      BEGIN                  
                  
        -- do the update                  
        Update  @calc                  
           set  cMH_PAYOUT_FLOAT_TOKEN         = cMH_PAYOUT_FLOAT_TOKEN + dbo.Calc_function ( MH.MH_PAYOUT_FLOAT_TOKEN, MH2.MH_PAYOUT_FLOAT_TOKEN, @bHandleRollover ),                  
                cMH_PAYOUT_FLOAT_10P           = cMH_PAYOUT_FLOAT_10P + dbo.Calc_function ( MH.MH_PAYOUT_FLOAT_10P, MH2.MH_PAYOUT_FLOAT_10P, @bHandleRollover ),                  
                cMH_PAYOUT_FLOAT_20P           = cMH_PAYOUT_FLOAT_20P + dbo.Calc_function ( MH.MH_PAYOUT_FLOAT_20P, MH2.MH_PAYOUT_FLOAT_20P, @bHandleRollover ),                  
               cMH_PAYOUT_FLOAT_50P           = cMH_PAYOUT_FLOAT_50P + dbo.Calc_function ( MH.MH_PAYOUT_FLOAT_50P, MH2.MH_PAYOUT_FLOAT_50P, @bHandleRollover ),                  
               cMH_PAYOUT_FLOAT_100P          = cMH_PAYOUT_FLOAT_100P + dbo.Calc_function ( MH.MH_PAYOUT_FLOAT_100P, MH2.MH_PAYOUT_FLOAT_100P, @bHandleRollover ),                  
               cMH_CASH_IN_1P                 = cMH_CASH_IN_1P + dbo.Calc_function ( MH.MH_CASH_IN_1P, MH2.MH_CASH_IN_1P, @bHandleRollover ),     
               cMH_CASH_IN_2P                 = cMH_CASH_IN_2P + dbo.Calc_function ( MH.MH_CASH_IN_2P, MH2.MH_CASH_IN_2P, @bHandleRollover ),                  
               cMH_CASH_IN_5P                 = cMH_CASH_IN_5P + dbo.Calc_function ( MH.MH_CASH_IN_5P, MH2.MH_CASH_IN_5P, @bHandleRollover ),                  
               cMH_CASH_IN_10P                = cMH_CASH_IN_10P + dbo.Calc_function ( MH.MH_CASH_IN_10P, MH2.MH_CASH_IN_10P, @bHandleRollover ),                  
               cMH_CASH_IN_20P                = cMH_CASH_IN_20P + dbo.Calc_function ( MH.MH_CASH_IN_20P, MH2.MH_CASH_IN_20P, @bHandleRollover ),                  
               cMH_CASH_IN_50P                = cMH_CASH_IN_50P + dbo.Calc_function ( MH.MH_CASH_IN_50P, MH2.MH_CASH_IN_50P, @bHandleRollover ),                  
               cMH_CASH_IN_100P               = cMH_CASH_IN_100P + dbo.Calc_function ( MH.MH_CASH_IN_100P, MH2.MH_CASH_IN_100P, @bHandleRollover ),                  
               cMH_CASH_IN_200P               = cMH_CASH_IN_200P + dbo.Calc_function ( MH.MH_CASH_IN_200P, MH2.MH_CASH_IN_200P, @bHandleRollover ),                  
               cMH_CASH_IN_500P               = cMH_CASH_IN_500P + dbo.Calc_function ( MH.MH_CASH_IN_500P, MH2.MH_CASH_IN_500P, @bHandleRollover ),                  
               cMH_CASH_IN_1000P              = cMH_CASH_IN_1000P + dbo.Calc_function ( MH.MH_CASH_IN_1000P, MH2.MH_CASH_IN_1000P, @bHandleRollover ),                  
               cMH_CASH_IN_2000P              = cMH_CASH_IN_2000P + dbo.Calc_function ( MH.MH_CASH_IN_2000P, MH2.MH_CASH_IN_2000P, @bHandleRollover ),                  
               cMH_CASH_IN_5000P              = cMH_CASH_IN_5000P + dbo.Calc_function ( MH.MH_CASH_IN_5000P, MH2.MH_CASH_IN_5000P, @bHandleRollover ),                  
               cMH_CASH_IN_10000P             = cMH_CASH_IN_10000P + dbo.Calc_function ( MH.MH_CASH_IN_10000P, MH2.MH_CASH_IN_10000P, @bHandleRollover ),                  
               cMH_CASH_IN_20000P             = cMH_CASH_IN_20000P + dbo.Calc_function ( MH.MH_CASH_IN_20000P, MH2.MH_CASH_IN_20000P, @bHandleRollover ),                  
               cMH_CASH_IN_50000P             = cMH_CASH_IN_50000P + dbo.Calc_function ( MH.MH_CASH_IN_50000P, MH2.MH_CASH_IN_50000P, @bHandleRollover ),                  
               cMH_CASH_IN_100000P            = cMH_CASH_IN_100000P + dbo.Calc_function ( MH.MH_CASH_IN_100000P, MH2.MH_CASH_IN_100000P, @bHandleRollover ),                  
--                  
               cMH_TOKEN_IN_5P                 = cMH_TOKEN_IN_5P + dbo.Calc_function ( MH.MH_TOKEN_IN_5P, MH2.MH_TOKEN_IN_5P, @bHandleRollover ),                  
               cMH_TOKEN_IN_10P  = cMH_TOKEN_IN_10P + dbo.Calc_function ( MH.MH_TOKEN_IN_10P, MH2.MH_TOKEN_IN_10P, @bHandleRollover ),                  
               cMH_TOKEN_IN_20P                = cMH_TOKEN_IN_20P + dbo.Calc_function ( MH.MH_TOKEN_IN_20P, MH2.MH_TOKEN_IN_20P, @bHandleRollover ),                  
               cMH_TOKEN_IN_50P                = cMH_TOKEN_IN_50P + dbo.Calc_function ( MH.MH_TOKEN_IN_50P, MH2.MH_TOKEN_IN_50P, @bHandleRollover ),                  
               cMH_TOKEN_IN_100P               = cMH_TOKEN_IN_100P + dbo.Calc_function ( MH.MH_TOKEN_IN_100P, MH2.MH_TOKEN_IN_100P, @bHandleRollover ),                  
               cMH_TOKEN_IN_200P               = cMH_TOKEN_IN_200P + dbo.Calc_function ( MH.MH_TOKEN_IN_200P, MH2.MH_TOKEN_IN_200P, @bHandleRollover ),                  
               cMH_TOKEN_IN_500P               = cMH_TOKEN_IN_500P + dbo.Calc_function ( MH.MH_TOKEN_IN_500P, MH2.MH_TOKEN_IN_500P, @bHandleRollover ),                  
               cMH_TOKEN_IN_1000P              = cMH_TOKEN_IN_1000P + dbo.Calc_function ( MH.MH_TOKEN_IN_1000P, MH2.MH_TOKEN_IN_1000P, @bHandleRollover ),                  
--             
               cMH_CASH_OUT_1P                 = cMH_CASH_OUT_1P + dbo.Calc_function ( MH.MH_CASH_OUT_1P, MH2.MH_CASH_OUT_1P, @bHandleRollover ),     
               cMH_CASH_OUT_2P                 = cMH_CASH_OUT_2P + dbo.Calc_function ( MH.MH_CASH_OUT_2P, MH2.MH_CASH_OUT_2P, @bHandleRollover ),                  
               cMH_CASH_OUT_5P                 = cMH_CASH_OUT_5P + dbo.Calc_function ( MH.MH_CASH_OUT_5P, MH2.MH_CASH_OUT_5P, @bHandleRollover ),                  
               cMH_CASH_OUT_10P                = cMH_CASH_OUT_10P + dbo.Calc_function ( MH.MH_CASH_OUT_10P, MH2.MH_CASH_OUT_10P, @bHandleRollover ),                  
               cMH_CASH_OUT_20P                = cMH_CASH_OUT_20P + dbo.Calc_function ( MH.MH_CASH_OUT_20P, MH2.MH_CASH_OUT_20P, @bHandleRollover ),                  
               cMH_CASH_OUT_50P                = cMH_CASH_OUT_50P + dbo.Calc_function ( MH.MH_CASH_OUT_50P, MH2.MH_CASH_OUT_50P, @bHandleRollover ),                  
               cMH_CASH_OUT_100P               = cMH_CASH_OUT_100P + dbo.Calc_function ( MH.MH_CASH_OUT_100P, MH2.MH_CASH_OUT_100P, @bHandleRollover ),                  
               cMH_CASH_OUT_200P               = cMH_CASH_OUT_200P + dbo.Calc_function ( MH.MH_CASH_OUT_200P, MH2.MH_CASH_OUT_200P, @bHandleRollover ),                  
               cMH_CASH_OUT_500P               = cMH_CASH_OUT_500P + dbo.Calc_function ( MH.MH_CASH_OUT_500P, MH2.MH_CASH_OUT_500P, @bHandleRollover ),                  
               cMH_CASH_OUT_1000P              = cMH_CASH_OUT_1000P + dbo.Calc_function ( MH.MH_CASH_OUT_1000P, MH2.MH_CASH_OUT_1000P, @bHandleRollover ),                  
               cMH_CASH_OUT_2000P              = cMH_CASH_OUT_2000P + dbo.Calc_function ( MH.MH_CASH_OUT_2000P, MH2.MH_CASH_OUT_2000P, @bHandleRollover ),                  
               cMH_CASH_OUT_5000P              = cMH_CASH_OUT_5000P + dbo.Calc_function ( MH.MH_CASH_OUT_5000P, MH2.MH_CASH_OUT_5000P, @bHandleRollover ),                  
               cMH_CASH_OUT_10000P             = cMH_CASH_OUT_10000P + dbo.Calc_function ( MH.MH_CASH_OUT_10000P, MH2.MH_CASH_OUT_10000P, @bHandleRollover ),                  
               cMH_CASH_OUT_20000P             = cMH_CASH_OUT_20000P + dbo.Calc_function ( MH.MH_CASH_OUT_20000P, MH2.MH_CASH_OUT_20000P, @bHandleRollover ),                  
               cMH_CASH_OUT_50000P             = cMH_CASH_OUT_50000P + dbo.Calc_function ( MH.MH_CASH_OUT_50000P, MH2.MH_CASH_OUT_50000P, @bHandleRollover ),                  
               cMH_CASH_OUT_100000P            = cMH_CASH_OUT_100000P + dbo.Calc_function ( MH.MH_CASH_OUT_100000P, MH2.MH_CASH_OUT_100000P, @bHandleRollover ),                  
--                  
               cMH_TOKEN_OUT_5P                 = cMH_TOKEN_OUT_5P + dbo.Calc_function ( MH.MH_TOKEN_OUT_5P, MH2.MH_TOKEN_OUT_5P, @bHandleRollover ),                  
               cMH_TOKEN_OUT_10P                = cMH_TOKEN_OUT_10P + dbo.Calc_function ( MH.MH_TOKEN_OUT_10P, MH2.MH_TOKEN_OUT_10P, @bHandleRollover ),                  
               cMH_TOKEN_OUT_20P        = cMH_TOKEN_OUT_20P + dbo.Calc_function ( MH.MH_TOKEN_OUT_20P, MH2.MH_TOKEN_OUT_20P, @bHandleRollover ),                  
               cMH_TOKEN_OUT_50P                = cMH_TOKEN_OUT_50P + dbo.Calc_function ( MH.MH_TOKEN_OUT_50P, MH2.MH_TOKEN_OUT_50P, @bHandleRollover ),                  
               cMH_TOKEN_OUT_100P               = cMH_TOKEN_OUT_100P + dbo.Calc_function ( MH.MH_TOKEN_OUT_100P, MH2.MH_TOKEN_OUT_100P, @bHandleRollover ),                  
               cMH_TOKEN_OUT_200P               = cMH_TOKEN_OUT_200P + dbo.Calc_function ( MH.MH_TOKEN_OUT_200P, MH2.MH_TOKEN_OUT_200P, @bHandleRollover ),                  
               cMH_TOKEN_OUT_500P               = cMH_TOKEN_OUT_500P + dbo.Calc_function ( MH.MH_TOKEN_OUT_500P, MH2.MH_TOKEN_OUT_500P, @bHandleRollover ),                  
               cMH_TOKEN_OUT_1000P              = cMH_TOKEN_OUT_1000P + dbo.Calc_function ( MH.MH_TOKEN_OUT_1000P, MH2.MH_TOKEN_OUT_1000P, @bHandleRollover ),                  
--                  
             cMH_CASH_REFILL_5P             = cMH_CASH_REFILL_5P + dbo.Calc_function ( MH.MH_CASH_REFILL_5P, MH2.MH_CASH_REFILL_5P, @bHandleRollover ),                  
             cMH_CASH_REFILL_10P            = cMH_CASH_REFILL_10P + dbo.Calc_function ( MH.MH_CASH_REFILL_10P, MH2.MH_CASH_REFILL_10P, @bHandleRollover ),                  
             cMH_CASH_REFILL_20P            = cMH_CASH_REFILL_20P + dbo.Calc_function ( MH.MH_CASH_REFILL_20P, MH2.MH_CASH_REFILL_20P, @bHandleRollover ),                  
             cMH_CASH_REFILL_50P            = cMH_CASH_REFILL_50P + dbo.Calc_function ( MH.MH_CASH_REFILL_50P, MH2.MH_CASH_REFILL_50P, @bHandleRollover ),                  
             cMH_CASH_REFILL_100P           = cMH_CASH_REFILL_100P + dbo.Calc_function ( MH.MH_CASH_REFILL_100P, MH2.MH_CASH_REFILL_100P, @bHandleRollover ),                  
             cMH_CASH_REFILL_200P           = cMH_CASH_REFILL_200P + dbo.Calc_function ( MH.MH_CASH_REFILL_200P, MH2.MH_CASH_REFILL_200P, @bHandleRollover ),                  
             cMH_CASH_REFILL_500P           = cMH_CASH_REFILL_500P + dbo.Calc_function ( MH.MH_CASH_REFILL_500P, MH2.MH_CASH_REFILL_500P, @bHandleRollover ),                  
             cMH_CASH_REFILL_1000P          = cMH_CASH_REFILL_1000P+ dbo.Calc_function ( MH.MH_CASH_REFILL_1000P, MH2.MH_CASH_REFILL_1000P, @bHandleRollover ),                  
             cMH_CASH_REFILL_2000P          = cMH_CASH_REFILL_2000P + dbo.Calc_function ( MH.MH_CASH_REFILL_2000P, MH2.MH_CASH_REFILL_2000P, @bHandleRollover ),                  
             cMH_CASH_REFILL_5000P          = cMH_CASH_REFILL_5000P + dbo.Calc_function ( MH.MH_CASH_REFILL_5000P, MH2.MH_CASH_REFILL_5000P, @bHandleRollover ),                  
             cMH_CASH_REFILL_10000P         = cMH_CASH_REFILL_10000P + dbo.Calc_function ( MH.MH_CASH_REFILL_10000P, MH2.MH_CASH_REFILL_10000P, @bHandleRollover ),                  
             cMH_CASH_REFILL_20000P         = cMH_CASH_REFILL_20000P + dbo.Calc_function ( MH.MH_CASH_REFILL_20000P, MH2.MH_CASH_REFILL_20000P, @bHandleRollover ),                  
             cMH_CASH_REFILL_50000P         = cMH_CASH_REFILL_50000P + dbo.Calc_function ( MH.MH_CASH_REFILL_50000P, MH2.MH_CASH_REFILL_50000P, @bHandleRollover ),                  
             cMH_CASH_REFILL_100000P        = cMH_CASH_REFILL_100000P + dbo.Calc_function ( MH.MH_CASH_REFILL_100000P, MH2.MH_CASH_REFILL_100000P, @bHandleRollover ),                  
--                  
             cMH_TOKEN_REFILL_5P            = cMH_TOKEN_REFILL_5P + dbo.Calc_function ( MH.MH_TOKEN_REFILL_5P, MH2.MH_TOKEN_REFILL_5P, @bHandleRollover ),                  
             cMH_TOKEN_REFILL_10P           = cMH_TOKEN_REFILL_10P + dbo.Calc_function ( MH.MH_TOKEN_REFILL_10P, MH2.MH_TOKEN_REFILL_10P, @bHandleRollover ),                  
             cMH_TOKEN_REFILL_20P           = cMH_TOKEN_REFILL_20P + dbo.Calc_function ( MH.MH_TOKEN_REFILL_20P, MH2.MH_TOKEN_REFILL_20P, @bHandleRollover ),                  
             cMH_TOKEN_REFILL_50P           = cMH_TOKEN_REFILL_50P + dbo.Calc_function ( MH.MH_TOKEN_REFILL_50P, MH2.MH_TOKEN_REFILL_50P, @bHandleRollover ),                  
      cMH_TOKEN_REFILL_100P          = cMH_TOKEN_REFILL_100P + dbo.Calc_function ( MH.MH_TOKEN_REFILL_100P, MH2.MH_TOKEN_REFILL_100P, @bHandleRollover ),                  
             cMH_TOKEN_REFILL_200P          = cMH_TOKEN_REFILL_200P + dbo.Calc_function ( MH.MH_TOKEN_REFILL_200P, MH2.MH_TOKEN_REFILL_200P, @bHandleRollover ),                  
             cMH_TOKEN_REFILL_500P          = cMH_TOKEN_REFILL_500P + dbo.Calc_function ( MH.MH_TOKEN_REFILL_500P, MH2.MH_TOKEN_REFILL_500P, @bHandleRollover ),                  
             cMH_TOKEN_REFILL_1000P         = cMH_TOKEN_REFILL_1000P + dbo.Calc_function ( MH.MH_TOKEN_REFILL_1000P, MH2.MH_TOKEN_REFILL_1000P, @bHandleRollover ),                  
--                  
             cMH_COINS_IN  = cMH_COINS_IN + dbo.Calc_function ( MH.MH_COINS_IN, MH2.MH_COINS_IN, @bHandleRollover ),                  
             cMH_COINS_OUT                  = cMH_COINS_OUT + dbo.Calc_function ( MH.MH_COINS_OUT, MH2.MH_COINS_OUT, @bHandleRollover ),                  
             cMH_COIN_DROP                  = cMH_COIN_DROP + dbo.Calc_function ( MH.MH_COIN_DROP, MH2.MH_COIN_DROP, @bHandleRollover ),                  
             cMH_HANDPAY                    = cMH_HANDPAY + dbo.Calc_function ( MH.MH_HANDPAY, MH2.MH_HANDPAY, @bHandleRollover ),                  
             cMH_EXTERNAL_CREDIT            = cMH_EXTERNAL_CREDIT + dbo.Calc_function ( MH.MH_EXTERNAL_CREDIT, MH2.MH_EXTERNAL_CREDIT, @bHandleRollover ),                  
             cMH_GAMES_BET                  = cMH_GAMES_BET + dbo.Calc_function ( MH.MH_GAMES_BET, MH2.MH_GAMES_BET, @bHandleRollover ),                  
             cMH_GAMES_WON                  = cMH_GAMES_WON + dbo.Calc_function ( MH.MH_GAMES_WON, MH2.MH_GAMES_WON, @bHandleRollover ),                  
             cMH_NOTES                      = cMH_NOTES + dbo.Calc_function ( MH.MH_NOTES, MH2.MH_NOTES, @bHandleRollover ),                  
             cMH_VTP                        = cMH_VTP + dbo.Calc_function ( MH.MH_VTP, MH2.MH_VTP, @bHandleRollover ),                  
             cMH_CANCELLED_CREDITS          = cMH_CANCELLED_CREDITS + dbo.Calc_function ( MH.MH_CANCELLED_CREDITS, MH2.MH_CANCELLED_CREDITS, @bHandleRollover ),                  
             cMH_GAMES_LOST                 = cMH_GAMES_LOST + dbo.Calc_function ( MH.MH_GAMES_LOST, MH2.MH_GAMES_LOST, @bHandleRollover ),                  
             cMH_GAMES_SINCE_POWER_UP       = cMH_GAMES_SINCE_POWER_UP + dbo.Calc_function ( MH.MH_GAMES_SINCE_POWER_UP, MH2.MH_GAMES_SINCE_POWER_UP, @bHandleRollover ),                  
             cMH_TRUE_COIN_IN               = cMH_TRUE_COIN_IN + dbo.Calc_function ( MH.MH_TRUE_COIN_IN, MH2.MH_TRUE_COIN_IN, @bHandleRollover ),                  
             cMH_TRUE_COIN_OUT              = cMH_TRUE_COIN_OUT + dbo.Calc_function ( MH.MH_TRUE_COIN_OUT, MH2.MH_TRUE_COIN_OUT, @bHandleRollover ),                  
                  
                -- current credits not affected by rollover...                  
             cMH_CURRENT_CREDITS            = cMH_CURRENT_CREDITS + ( ISNULL(MH2.MH_CURRENT_CREDITS,0) - ISNULL(MH.MH_CURRENT_CREDITS,0) ),                   
                  
             cMH_JACKPOT                    = cMH_JACKPOT + dbo.Calc_function ( MH.MH_JACKPOT, MH2.MH_JACKPOT, @bHandleRollover ),                  
             cMH_BILL_1                     = cMH_BILL_1 + dbo.Calc_function ( MH.MH_BILL_1, MH2.MH_BILL_1, @bHandleRollover ),                  
             cMH_BILL_2                     = cMH_BILL_2 + dbo.Calc_function ( MH.MH_BILL_2, MH2.MH_BILL_2, @bHandleRollover ),                  
             cMH_BILL_5                     = cMH_BILL_5 + dbo.Calc_function ( MH.MH_BILL_5, MH2.MH_BILL_5, @bHandleRollover ),                  
             cMH_BILL_10                    = cMH_BILL_10 + dbo.Calc_function ( MH.MH_BILL_10, MH2.MH_BILL_10, @bHandleRollover ),                  
             cMH_BILL_20                    = cMH_BILL_20 + dbo.Calc_function ( MH.MH_BILL_20, MH2.MH_BILL_20, @bHandleRollover ),                     cMH_BILL_50                    = cMH_BILL_50 + dbo.Calc_function ( MH.MH_BILL_50, MH2.MH_BILL_50,      
  
    @bHandleRollover ),                  
             cMH_BILL_100                   = cMH_BILL_100 + dbo.Calc_function ( MH.MH_BILL_100, MH2.MH_BILL_100, @bHandleRollover ),                  
             cMH_BILL_250                   = cMH_BILL_250 + dbo.Calc_function ( MH.MH_BILL_250, MH2.MH_BILL_250, @bHandleRollover ),                  
             cMH_BILL_10000                 = cMH_BILL_10000 + dbo.Calc_function ( MH.MH_BILL_10000, MH2.MH_BILL_10000, @bHandleRollover ),                  
             cMH_BILL_20000                 = cMH_BILL_20000 + dbo.Calc_function ( MH.MH_BILL_20000, MH2.MH_BILL_20000, @bHandleRollover ),                  
             cMH_BILL_50000                 = cMH_BILL_50000 + dbo.Calc_function ( MH.MH_BILL_50000, MH2.MH_BILL_50000, @bHandleRollover ),                  
             cMH_BILL_100000                = cMH_BILL_100000 + dbo.Calc_function ( MH.MH_BILL_100000, MH2.MH_BILL_100000, @bHandleRollover ),                  
                  
             cMH_TICKET_PRINTED_QTY         = cMH_TICKET_PRINTED_QTY + dbo.Calc_function ( MH.MH_TICKET_PRINTED_QTY, MH2.MH_TICKET_PRINTED_QTY, @bHandleRollover ),                  
             cMH_TICKET_PRINTED_VALUE       = cMH_TICKET_PRINTED_VALUE + dbo.Calc_function ( MH.MH_TICKET_PRINTED_VALUE, MH2.MH_TICKET_PRINTED_VALUE, @bHandleRollover ),                  
             cMH_TICKET_INSERTED_QTY        = cMH_TICKET_INSERTED_QTY + dbo.Calc_function ( MH.MH_TICKET_INSERTED_QTY, MH2.MH_TICKET_INSERTED_QTY, @bHandleRollover ),                  
                cMH_TICKET_INSERTED_VALUE      = cMH_TICKET_INSERTED_VALUE + dbo.Calc_function ( MH.MH_TICKET_INSERTED_VALUE, MH2.MH_TICKET_INSERTED_VALUE, @bHandleRollover ),                  
                cMH_Progressive_Win_Value      = cMH_Progressive_Win_Value +dbo.Calc_function ( MH.MH_Progressive_Win_Value, MH2.MH_Progressive_Win_Value, @bHandleRollover ),                  
                cMH_Progressive_Win_Handpay_Value  = cMH_Progressive_Win_Handpay_Value + dbo.Calc_function ( MH.MH_Progressive_Win_Handpay_Value, MH2.MH_Progressive_Win_Handpay_Value, @bHandleRollover ),                  
          
      cMH_Mystery_Machine_Paid  = cMH_Mystery_Machine_Paid + dbo.Calc_function ( MH.MH_Mystery_Machine_Paid, MH2.MH_Mystery_Machine_Paid, @bHandleRollover ),          
      cMH_Mystery_Attendant_Paid  = cMH_Mystery_Attendant_Paid + dbo.Calc_function ( MH.MH_Mystery_Attendant_Paid, MH2.MH_Mystery_Attendant_Paid, @bHandleRollover ),          
      cMH_TICKETS_PRINTED_NONCASHABLE_QTY  = cMH_TICKETS_PRINTED_NONCASHABLE_QTY + dbo.Calc_function ( MH.MH_TICKETS_PRINTED_NONCASHABLE_QTY, MH2.MH_TICKETS_PRINTED_NONCASHABLE_QTY, @bHandleRollover ),          
      cMH_TICKETS_PRINTED_NONCASHABLE_VALUE  = cMH_TICKETS_PRINTED_NONCASHABLE_VALUE + dbo.Calc_function ( MH.MH_TICKETS_PRINTED_NONCASHABLE_VALUE, MH2.MH_TICKETS_PRINTED_NONCASHABLE_VALUE, @bHandleRollover ),          
      cMH_TICKETS_INSERTED_NONCASHABLE_QTY  = cMH_TICKETS_INSERTED_NONCASHABLE_QTY + dbo.Calc_function ( MH.MH_TICKETS_INSERTED_NONCASHABLE_QTY, MH2.MH_TICKETS_INSERTED_NONCASHABLE_QTY, @bHandleRollover ),          
      cMH_TICKETS_INSERTED_NONCASHABLE_VALUE  = cMH_TICKETS_INSERTED_NONCASHABLE_VALUE + dbo.Calc_function ( MH.MH_TICKETS_INSERTED_NONCASHABLE_VALUE, MH2.MH_TICKETS_INSERTED_NONCASHABLE_VALUE, @bHandleRollover ),          
      cMH_Promo_Cashable_EFT_IN  = cMH_Promo_Cashable_EFT_IN + dbo.Calc_function ( MH.MH_Promo_Cashable_EFT_IN, MH2.MH_Promo_Cashable_EFT_IN, @bHandleRollover ),          
      cMH_Promo_Cashable_EFT_OUT  = cMH_Promo_Cashable_EFT_OUT + dbo.Calc_function ( MH.MH_Promo_Cashable_EFT_OUT, MH2.MH_Promo_Cashable_EFT_OUT, @bHandleRollover ),          
      cMH_NonCashable_EFT_IN  = cMH_NonCashable_EFT_IN + dbo.Calc_function ( MH.MH_NonCashable_EFT_IN, MH2.MH_NonCashable_EFT_IN, @bHandleRollover ),          
      cMH_NonCashable_EFT_OUT  = cMH_NonCashable_EFT_OUT + dbo.Calc_function ( MH.MH_NonCashable_EFT_OUT, MH2.MH_NonCashable_EFT_OUT, @bHandleRollover ),          
      cMH_Cashable_EFT_IN  = cMH_Cashable_EFT_IN + dbo.Calc_function ( MH.MH_Cashable_EFT_IN, MH2.MH_Cashable_EFT_IN, @bHandleRollover ),          
      cMH_Cashable_EFT_OUT  = cMH_Cashable_EFT_OUT + dbo.Calc_function ( MH.MH_Cashable_EFT_OUT, MH2.MH_Cashable_EFT_OUT, @bHandleRollover ),        
             cMH_BILL_200                   = cMH_BILL_200 + dbo.Calc_function ( MH.MH_BILL_200, MH2.MH_BILL_200, @bHandleRollover ),        
             cMH_BILL_500                   = cMH_BILL_500 + dbo.Calc_function ( MH.MH_BILL_500, MH2.MH_BILL_500, @bHandleRollover )        
                  
                  
         FROM   @TempMH  MH, @TempMH  MH2                  
        WHERE   MH.MH_ID = @id1                  
          AND   MH2.MH_ID = @id2                  
          AND   cMH_ID = 1                  
                  
    END                  
                  
    Select @RowCnt = @RowCnt + 1 -- get next row                  
                  
  END                  
                  
  -- return changed #myWorkingDelta                  
  -- return filled                   
Update @myWorkingDelta                   
   set MH_Process                       = 'CALC',                  
       MH_Type                          = '',                  
       MH_Installation_No               = 0,                  
       MH_Reference                     = '',                  
       MH_LinkReference                 = '',                  
       MH_PAYOUT_FLOAT_TOKEN    = cMH_PAYOUT_FLOAT_TOKEN,                  
       MH_PAYOUT_FLOAT_10P      = cMH_PAYOUT_FLOAT_10P,                  
  MH_PAYOUT_FLOAT_20P      = cMH_PAYOUT_FLOAT_20P,                  
       MH_PAYOUT_FLOAT_50P      = cMH_PAYOUT_FLOAT_50P,                  
       MH_PAYOUT_FLOAT_100P     = cMH_PAYOUT_FLOAT_100P,    
       MH_CASH_IN_1P            = cMH_CASH_IN_1P,                 
       MH_CASH_IN_2P            = cMH_CASH_IN_2P,                  
       MH_CASH_IN_5P            = cMH_CASH_IN_5P,                   
       MH_CASH_IN_10P           = cMH_CASH_IN_10P,                   
       MH_CASH_IN_20P           = cMH_CASH_IN_20P,                  
       MH_CASH_IN_50P           = cMH_CASH_IN_50P,                  
       MH_CASH_IN_100P   = cMH_CASH_IN_100P,                  
       MH_CASH_IN_200P   = cMH_CASH_IN_200P,                  
      MH_CASH_IN_500P   = cMH_CASH_IN_500P,                  
       MH_CASH_IN_1000P   = cMH_CASH_IN_1000P,                  
       MH_CASH_IN_2000P   = cMH_CASH_IN_2000P,                  
       MH_CASH_IN_5000P   = cMH_CASH_IN_5000P,                  
       MH_CASH_IN_10000P  = cMH_CASH_IN_10000P,                  
       MH_CASH_IN_20000P  = cMH_CASH_IN_20000P,                  
       MH_CASH_IN_50000P  = cMH_CASH_IN_50000P,                  
       MH_CASH_IN_100000P  = cMH_CASH_IN_100000P,                  
       MH_TOKEN_IN_5P   = cMH_TOKEN_IN_5P,                  
       MH_TOKEN_IN_10P   = cMH_TOKEN_IN_10P,                  
       MH_TOKEN_IN_20P   = cMH_TOKEN_IN_20P,                  
       MH_TOKEN_IN_50P   = cMH_TOKEN_IN_50P,                  
       MH_TOKEN_IN_100P   = cMH_TOKEN_IN_100P,                  
       MH_TOKEN_IN_200P   = cMH_TOKEN_IN_200P,                  
       MH_TOKEN_IN_500P   = cMH_TOKEN_IN_500P,                  
       MH_TOKEN_IN_1000P  = cMH_TOKEN_IN_1000P,  
       MH_CASH_OUT_1P   = cMH_CASH_OUT_1P,                
       MH_CASH_OUT_2P   = cMH_CASH_OUT_2P,                  
       MH_CASH_OUT_5P   = cMH_CASH_OUT_5P,                  
       MH_CASH_OUT_10P   = cMH_CASH_OUT_10P,                  
       MH_CASH_OUT_20P   = cMH_CASH_OUT_20P,                  
       MH_CASH_OUT_50P   = cMH_CASH_OUT_50P,                  
       MH_CASH_OUT_100P   = cMH_CASH_OUT_100P,                  
       MH_CASH_OUT_200P   = cMH_CASH_OUT_200P,                  
       MH_CASH_OUT_500P   = cMH_CASH_OUT_500P,                  
       MH_CASH_OUT_1000P  = cMH_CASH_OUT_1000P,                  
       MH_CASH_OUT_2000P  = cMH_CASH_OUT_2000P,                  
       MH_CASH_OUT_5000P  = cMH_CASH_OUT_5000P,                  
       MH_CASH_OUT_10000P  = cMH_CASH_OUT_10000P,                  
       MH_CASH_OUT_20000P  = cMH_CASH_OUT_20000P,                  
       MH_CASH_OUT_50000P  = cMH_CASH_OUT_50000P,                  
       MH_CASH_OUT_100000P  = cMH_CASH_OUT_100000P,                  
       MH_TOKEN_OUT_5P   = cMH_TOKEN_OUT_5P,                  
       MH_TOKEN_OUT_10P   = cMH_TOKEN_OUT_10P,                  
       MH_TOKEN_OUT_20P   = cMH_TOKEN_OUT_20P,                  
       MH_TOKEN_OUT_50P   = cMH_TOKEN_OUT_50P,                  
      MH_TOKEN_OUT_100P  = cMH_TOKEN_OUT_100P,                  
       MH_TOKEN_OUT_200P  = cMH_TOKEN_OUT_200P,                  
       MH_TOKEN_OUT_500P  = cMH_TOKEN_OUT_500P,                  
  MH_TOKEN_OUT_1000P  = cMH_TOKEN_OUT_1000P,                  
       MH_CASH_REFILL_5P  = cMH_CASH_REFILL_5P,                  
       MH_CASH_REFILL_10P  = cMH_CASH_REFILL_10P,                  
       MH_CASH_REFILL_20P  = cMH_CASH_REFILL_20P,                  
       MH_CASH_REFILL_50P  = cMH_CASH_REFILL_50P,                  
       MH_CASH_REFILL_100P  = cMH_CASH_REFILL_100P,                  
       MH_CASH_REFILL_200P  = cMH_CASH_REFILL_200P,                  
       MH_CASH_REFILL_500P  = cMH_CASH_REFILL_500P,                  
       MH_CASH_REFILL_1000P  = cMH_CASH_REFILL_1000P,                  
       MH_CASH_REFILL_2000P  = cMH_CASH_REFILL_2000P,                  
       MH_CASH_REFILL_5000P  = cMH_CASH_REFILL_5000P,                  
       MH_CASH_REFILL_10000P  = cMH_CASH_REFILL_10000P,                  
       MH_CASH_REFILL_20000P  = cMH_CASH_REFILL_20000P,                  
       MH_CASH_REFILL_50000P  = cMH_CASH_REFILL_50000P,                  
       MH_CASH_REFILL_100000P  = cMH_CASH_REFILL_100000P,                  
       MH_TOKEN_REFILL_5P  = cMH_TOKEN_REFILL_5P,                  
       MH_TOKEN_REFILL_10P  = cMH_TOKEN_REFILL_10P,                  
       MH_TOKEN_REFILL_20P  = cMH_TOKEN_REFILL_20P,                  
       MH_TOKEN_REFILL_50P  = cMH_TOKEN_REFILL_50P,                  
       MH_TOKEN_REFILL_100P  = cMH_TOKEN_REFILL_100P,                  
       MH_TOKEN_REFILL_200P  = cMH_TOKEN_REFILL_200P,                  
       MH_TOKEN_REFILL_500P  = cMH_TOKEN_REFILL_500P,                  
       MH_TOKEN_REFILL_1000P  = cMH_TOKEN_REFILL_1000P,                  
       MH_COINS_IN   = cMH_COINS_IN,                  
       MH_COINS_OUT   = cMH_COINS_OUT,                  
       MH_COIN_DROP   = cMH_COIN_DROP,                  
       MH_HANDPAY   = cMH_HANDPAY,                  
       MH_EXTERNAL_CREDIT  = cMH_EXTERNAL_CREDIT,                  
       MH_GAMES_BET   = cMH_GAMES_BET,                  
       MH_GAMES_WON   = cMH_GAMES_WON,                  
       MH_NOTES    = cMH_NOTES,                  
       MH_VTP    = cMH_VTP,                  
       MH_CANCELLED_CREDITS  = cMH_CANCELLED_CREDITS,                  
       MH_GAMES_LOST   = cMH_GAMES_LOST,                  
       MH_GAMES_SINCE_POWER_UP  = cMH_GAMES_SINCE_POWER_UP,                  
       MH_TRUE_COIN_IN   = cMH_TRUE_COIN_IN,                  
       MH_TRUE_COIN_OUT   = cMH_TRUE_COIN_OUT,                  
       MH_CURRENT_CREDITS  = cMH_CURRENT_CREDITS,                  
       MH_JACKPOT   = cMH_JACKPOT,                  
       MH_BILL_1   = cMH_BILL_1,                  
       MH_BILL_2   = cMH_BILL_2,                  
       MH_BILL_5   = cMH_BILL_5,                  
       MH_BILL_10  = cMH_BILL_10,                  
       MH_BILL_20                       = cMH_BILL_20,                  
       MH_BILL_50                       = cMH_BILL_50,                  
       MH_BILL_100                      = cMH_BILL_100,                  
       MH_BILL_250                = cMH_BILL_250,                  
       MH_BILL_10000                    = cMH_BILL_10000,                  
       MH_BILL_20000                    = cMH_BILL_20000,                  
       MH_BILL_50000                    = cMH_BILL_50000,                  
       MH_BILL_100000                   = cMH_BILL_100000,                  
    MH_TICKET_PRINTED_QTY            = cMH_TICKET_PRINTED_QTY,                  
       MH_TICKET_PRINTED_VALUE          = cMH_TICKET_PRINTED_VALUE,                  
       MH_TICKET_INSERTED_QTY           = cMH_TICKET_INSERTED_QTY,                  
       MH_TICKET_INSERTED_VALUE         = cMH_TICKET_INSERTED_VALUE,                  
       MH_Progressive_Win_Value   = cMH_Progressive_Win_Value,                  
       MH_Progressive_Win_Handpay_Value = cMH_Progressive_Win_Handpay_Value,                  
          
  MH_Mystery_Machine_Paid = cMH_Mystery_Machine_Paid,          
  MH_Mystery_Attendant_Paid = cMH_Mystery_Attendant_Paid,          
  MH_TICKETS_PRINTED_NONCASHABLE_QTY = cMH_TICKETS_PRINTED_NONCASHABLE_QTY,          
  MH_TICKETS_PRINTED_NONCASHABLE_VALUE = cMH_TICKETS_PRINTED_NONCASHABLE_VALUE,          
  MH_TICKETS_INSERTED_NONCASHABLE_QTY = cMH_TICKETS_INSERTED_NONCASHABLE_QTY,          
  MH_TICKETS_INSERTED_NONCASHABLE_VALUE = cMH_TICKETS_INSERTED_NONCASHABLE_VALUE,          
  MH_Promo_Cashable_EFT_IN = cMH_Promo_Cashable_EFT_IN,          
  MH_Promo_Cashable_EFT_OUT = cMH_Promo_Cashable_EFT_OUT,          
  MH_NonCashable_EFT_IN = cMH_NonCashable_EFT_IN,          
  MH_NonCashable_EFT_OUT = cMH_NonCashable_EFT_OUT,          
  MH_Cashable_EFT_IN = cMH_Cashable_EFT_IN,          
  MH_Cashable_EFT_OUT = cMH_Cashable_EFT_OUT,        
       MH_BILL_200                      = cMH_BILL_200,        
       MH_BILL_500                      = cMH_BILL_500        
  FROM @Calc c                  
 WHERE cMH_ID = 1                  
   AND MH_ID = @workingid                    
          
      
      
SELECT @Result=         
CASE @MeterType        
 WHEN 'Mh_Games_Bet' THEN        
   MH_GAMES_BET         
 WHEN 'mh_games_won' THEN        
   mh_games_won         
   WHEN 'mh_coin_drop' THEN        
   mh_coin_drop         
 WHEN 'mh_coins_in' THEN        
   mh_coins_in         
 WHEN 'mh_coins_out' THEN        
   mh_coins_out        
 WHEN 'mh_jackpot' THEN        
   mh_jackpot         
 WHEN 'mh_cancelled_credits' THEN        
   mh_cancelled_credits         
 WHEN 'mh_progressive_win_handpay_value' THEN        
   mh_progressive_win_handpay_value         
 WHEN 'mh_progressive_win_value' THEN        
   mh_progressive_win_value        
  WHEN 'mh_ticket_printed_value' THEN      
   MH_TICKET_PRINTED_VALUE      
  WHEN 'mh_ticket_inserted_value' THEN      
   MH_TICKET_INSERTED_VALUE      
  WHEN 'mh_tickets_inserted_noncashable_value' THEN      
 MH_TICKETS_INSERTED_NONCASHABLE_VALUE      
  WHEN 'mh_tickets_printed_noncashable_value' THEN      
 MH_TICKETS_PRINTED_NONCASHABLE_VALUE      
  WHEN 'mh_HandPay' THEN      
   MH_HandPay      
  WHEN 'mh_true_coin_in' THEN      
   MH_TRUE_COIN_IN      
  WHEN 'mh_true_coin_out' THEN      
   MH_TRUE_COIN_OUT      
  WHEN 'Mh_Bill_1' THEN      
 MH_BILL_1      
WHEN 'Mh_Bill_2' THEN      
 MH_BILL_2    
  WHEN 'Mh_Bill_5' THEN      
 MH_BILL_5      
  WHEN 'Mh_Bill_10' THEN      
 MH_BILL_10      
  WHEN 'Mh_Bill_20' THEN      
 MH_BILL_20      
  WHEN 'Mh_Bill_50' THEN      
 MH_BILL_50      
  WHEN 'Mh_Bill_100' THEN      
 MH_BILL_100      
 ELSE             
  0        
  END        
FROM @myWorkingDelta        
        
return @Result
  END 

GO

