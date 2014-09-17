USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[esp_InsertReadRecordfromXML]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[esp_InsertReadRecordfromXML]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--------------------------------------------------------------------------------------------------------------   
--  
-- Description: retrieves read records to be exported to enterprise  
--i  
-- Inputs:     None  
-- Outputs:    0 = success , rest indicating failure codes  
--  
-- ============================================================================================================  
--   
-- Revision History  
--   
-- Rakesh		20/02/08	Created  
-- Siva			27/05/08	Remove unwanted logic to get installation ID, as we are receiving it from exchange   
--      changes to process multiple records  
-- Siva			16/07/08	bug fix  
-- Vineetha		21/07/09	Added Column Read_Days to get difference btw last read and current read for an installation  
-- Sudarsan S	07/01/2010	jackpot column  
-- Sudarsan S	11/04/2010	Mystery/Non Cashable Tkt/EFT  
-- Sudarsan S	15/06/2010	Bills 200, 500  
-- Yoganandh P	03/01/2011	Modified For Read Days
---------------------------------------------------------------------------------------------------------------   
  
CREATE PROCEDURE [dbo].[esp_InsertReadRecordfromXML]   
  
@doc varchar(max),  
@IsSuccess int output  
  
As  
set dateformat dmy  
  
Declare @idoc    int  
Declare @Read_Date   VARCHAR(20)  
Declare @InStartDate  varchar(30)  
Declare @InStartTime  varchar(30)  
Declare @InEndDate   varchar(30)  
Declare @InEndTime   varchar(30)  
Declare @InstallationNo  int  
Declare @ReadID    int  
Declare @newID    int  
Declare @error    int  
Declare @Readdate   datetime  
Declare @Installation_Date datetime
Declare @PreviousMHID int
Declare @CurrentMHID int
  
set @IsSuccess=-1  
set @error = 0  
  
create table #TempReadInsert  
(  
PAYOUT_FLOAT_TOKEN int,  
PAYOUT_FLOAT_10P int,  
PAYOUT_FLOAT_20P int,  
PAYOUT_FLOAT_50P int,  
PAYOUT_FLOAT_100P int,  
CASH_IN_1P int,
CASH_IN_2P int,  
CASH_IN_5P int,  
CASH_IN_10P int,  
CASH_IN_20P int,  
CASH_IN_50P int,  
CASH_IN_100P int,  
CASH_IN_200P int,  
CASH_IN_500P int,  
CASH_IN_1000P int,  
CASH_IN_2000P int,  
CASH_IN_5000P int,  
CASH_IN_10000P int,  
CASH_IN_20000P int,  
CASH_IN_50000P int,  
CASH_IN_100000P int,  
TOKEN_IN_5P int,  
TOKEN_IN_10P int,  
TOKEN_IN_20P int,  
TOKEN_IN_50P int,  
TOKEN_IN_100P int,  
TOKEN_IN_200P int,  
TOKEN_IN_500P int,  
TOKEN_IN_1000P int,  
CASH_OUT_1P int,
CASH_OUT_2P int,  
CASH_OUT_5P int,  
CASH_OUT_10P int,  
CASH_OUT_20P int,  
CASH_OUT_50P int,  
CASH_OUT_100P int,  
CASH_OUT_200P int,  
CASH_OUT_500P int,  
CASH_OUT_1000P int,  
CASH_OUT_2000P int,  
CASH_OUT_5000P int,  
CASH_OUT_10000P int,  
CASH_OUT_20000P int,  
CASH_OUT_50000P int,  
CASH_OUT_100000P int,  
TOKEN_OUT_5P int,  
TOKEN_OUT_10P int,  
TOKEN_OUT_20P int,  
TOKEN_OUT_50P int,  
TOKEN_OUT_100P int,  
TOKEN_OUT_200P int,  
TOKEN_OUT_500P int,  
TOKEN_OUT_1000P int,  
CASH_REFILL_5P int,  
CASH_REFILL_10P int,  
CASH_REFILL_20P int,  
CASH_REFILL_50P int,  
CASH_REFILL_100P int,  
CASH_REFILL_200P int,  
CASH_REFILL_500P int,  
CASH_REFILL_1000P int,  
CASH_REFILL_2000P int,  
CASH_REFILL_5000P int,  
CASH_REFILL_10000P int,  
CASH_REFILL_20000P int,  
CASH_REFILL_50000P int,  
CASH_REFILL_100000P int,  
TOKEN_REFILL_5P int,  
TOKEN_REFILL_10P int,  
TOKEN_REFILL_20P int,  
TOKEN_REFILL_50P int,  
TOKEN_REFILL_100P int,  
TOKEN_REFILL_200P int,  
TOKEN_REFILL_500P int,  
TOKEN_REFILL_1000P int,  
READ_COINS_IN int,  
READ_COINS_OUT int,  
READ_COIN_DROP int,  
READ_HANDPAY int,  
READ_EXTERNAL_CREDIT int,  
READ_GAMES_BET int,  
READ_GAMES_WON int,  
READ_NOTES int,  
VTP int,  
READ_RDC_CANCELLED_CREDITS int,  
READ_RDC_GAMES_LOST int,  
READ_RDC_GAMES_SINCE_POWER_UP int,  
READ_RDC_TRUE_COIN_IN int,  
READ_RDC_TRUE_COIN_OUT int,  
READ_RDC_CURRENT_CREDITS int,  
READ_RDC_BILL_1 int,  
READ_RDC_BILL_2 int,  
READ_RDC_BILL_5 int,  
READ_RDC_BILL_10 int,  
READ_RDC_BILL_20 int,  
READ_RDC_BILL_50 int,  
READ_RDC_BILL_100 int,  
READ_RDC_BILL_250 int,  
READ_RDC_BILL_10000 int,  
READ_RDC_BILL_20000 int,  
READ_RDC_BILL_50000 int,  
READ_RDC_BILL_100000 int,  
Read_Date datetime,  
READ_TICKET real,  
READ_TICKET_VALUE real,  
READ_TICKET_IN_SUSPENSE real,  
READ_TICKET_IN_SUSPENSE_VALUE real,  
Read_Forced bit,  
Previous_Read_Date datetime,  
Week_ID int,  
Period_ID int,  
Operator_Week_ID int,  
Operator_Period_ID int,  
READ_RDC_Datapak_Type int,  
READ_RDC_Datapak_Version int,  
Read_Occurrence int,  
READ_RDC_JACKPOT INT,  
progressive_win_value int,  
progressive_win_Handpay_value int,  
Mystery_Machine_Paid INT,  
Mystery_Attendant_Paid INT,  
TICKETS_INSERTED_NONCASHABLE_VALUE INT,  
TICKETS_PRINTED_NONCASHABLE_VALUE INT,  
Promo_Cashable_EFT_IN INT,  
Promo_Cashable_EFT_OUT INT,  
NonCashable_EFT_IN INT,  
NonCashable_EFT_OUT INT,  
Cashable_EFT_IN INT,  
Cashable_EFT_OUT INT,  
READ_RDC_BILL_200 int,  
READ_RDC_BILL_500 int  
)  
  
SET @doc='<?xml version="1.0" encoding="ISO-8859-1"?>' + @doc  
  
EXEC sp_xml_preparedocument @idoc OUTPUT, @doc  
  
SELECT  @InstallationNo=Installation_ID,@READ_Date=CONVERT(VARCHAR, Read_Date, 106)  
  
FROM    OPENXML (@idoc, './Read',2)  
  WITH   
  (    
    Installation_ID int './Installation/Installation_Id',  
    Read_Date       DATETIME './Read_Date'  
   )  
  
Set @ReadID=(select Read_ID from [READ] where Installation_ID=@InstallationNo And Read_Date=@Read_Date)  
  
select @InstallationNo,@READ_Date,@ReadID  
  
--select @ReadID 'READ ID'  
  
--Get Last Read_Date for the installation  
SELECT TOP 1 @Readdate=Read_Date  FROM [read] WHERE Installation_ID=@InstallationNo ORDER BY 1 DESC  
select @Installation_Date = Installation_Start_Date from installation where installation_id = @InstallationNo

-- Get Previous & Current Meter History ID for Updating MH_LinkReference with Read_ID
SELECT TOP 1 @PreviousMHID = MH_ID FROM Meter_history MH WHERE MH.MH_Installation_No = @InstallationNo
AND MH.MH_Process = 'READ' AND MH.MH_Type = 'P' AND MH_LinkReference IS NULL ORDER BY MH_ID ASC

SELECT TOP 1 @CurrentMHID = MH_ID FROM Meter_history WHERE MH_Installation_No = @InstallationNo
AND MH_Process = 'READ' AND MH_Type = 'C' AND MH_LinkReference IS NULL ORDER BY MH_ID ASC

If(coalesce(@ReadID,0)=0)  
 Begin  
  INSERT INTO [READ] (  
        PAYOUT_FLOAT_TOKEN,  
        PAYOUT_FLOAT_10P,  
        PAYOUT_FLOAT_20P,  
        PAYOUT_FLOAT_50P,  
        PAYOUT_FLOAT_100P,  
        CASH_IN_1P, 
        CASH_IN_2P,  
        CASH_IN_5P,  
        CASH_IN_10P,  
        CASH_IN_20P,  
        CASH_IN_50P,  
        CASH_IN_100P,  
        CASH_IN_200P,  
        CASH_IN_500P,  
        CASH_IN_1000P,  
        CASH_IN_2000P,  
        CASH_IN_5000P,  
        CASH_IN_10000P,  
        CASH_IN_20000P,  
        CASH_IN_50000P,  
        CASH_IN_100000P,  
        TOKEN_IN_5P,  
        TOKEN_IN_10P,  
        TOKEN_IN_20P,  
        TOKEN_IN_50P,  
        TOKEN_IN_100P,  
        TOKEN_IN_200P,  
        TOKEN_IN_500P,  
        TOKEN_IN_1000P,  
        CASH_OUT_1P,
        CASH_OUT_2P,  
        CASH_OUT_5P,  
        CASH_OUT_10P,  
        CASH_OUT_20P,  
        CASH_OUT_50P,  
        CASH_OUT_100P,  
        CASH_OUT_200P,  
        CASH_OUT_500P,  
        CASH_OUT_1000P,  
        CASH_OUT_2000P,  
        CASH_OUT_5000P,  
        CASH_OUT_10000P,  
        CASH_OUT_20000P,  
        CASH_OUT_50000P,  
        CASH_OUT_100000P,  
        TOKEN_OUT_5P,  
        TOKEN_OUT_10P,  
        TOKEN_OUT_20P,  
        TOKEN_OUT_50P,  
        TOKEN_OUT_100P,  
        TOKEN_OUT_200P,  
        TOKEN_OUT_500P,  
        TOKEN_OUT_1000P,  
        CASH_REFILL_5P,  
        CASH_REFILL_10P,  
        CASH_REFILL_20P,  
        CASH_REFILL_50P,  
        CASH_REFILL_100P,  
        CASH_REFILL_200P,  
        CASH_REFILL_500P,  
        CASH_REFILL_1000P,  
        CASH_REFILL_2000P,  
        CASH_REFILL_5000P,  
        CASH_REFILL_10000P,  
        CASH_REFILL_20000P,  
        CASH_REFILL_50000P,  
        CASH_REFILL_100000P,  
        TOKEN_REFILL_5P,  
        TOKEN_REFILL_10P,  
        TOKEN_REFILL_20P,  
        TOKEN_REFILL_50P,  
        TOKEN_REFILL_100P,  
        TOKEN_REFILL_200P,  
        TOKEN_REFILL_500P,  
        TOKEN_REFILL_1000P,  
        READ_COINS_IN,  
        READ_COINS_OUT,  
        READ_COIN_DROP,  
        READ_HANDPAY,  
        READ_EXTERNAL_CREDIT,  
        READ_GAMES_BET,  
        READ_GAMES_WON,  
        READ_NOTES,  
        VTP,  
        READ_RDC_CANCELLED_CREDITS,  
        READ_RDC_GAMES_LOST,  
        READ_RDC_GAMES_SINCE_POWER_UP,  
        READ_RDC_TRUE_COIN_IN,  
        READ_RDC_TRUE_COIN_OUT,  
        READ_RDC_CURRENT_CREDITS,  
        READ_RDC_BILL_1,  
        READ_RDC_BILL_2,  
        READ_RDC_BILL_5,  
        READ_RDC_BILL_10,  
        READ_RDC_BILL_20,  
        READ_RDC_BILL_50,  
        READ_RDC_BILL_100,  
        READ_RDC_BILL_250,  
        READ_RDC_BILL_10000,  
        READ_RDC_BILL_20000,  
        READ_RDC_BILL_50000,  
        READ_RDC_BILL_100000,  
        Read_Date,  
        Read_Time,  
        READ_TICKET,  
        READ_TICKET_VALUE,  
        READ_TICKET_IN_SUSPENSE,  
        READ_TICKET_IN_SUSPENSE_VALUE,  
        Read_Forced,  
        Previous_Read_Date,  
        Previous_Read_Time,  
        Week_ID,  
        Period_ID,  
        Operator_Week_ID,  
        Operator_Period_ID,  
        READ_RDC_Datapak_Type,  
        READ_RDC_Datapak_Version,  
        Read_Occurrence,  
        READ_RDC_JACKPOT,  
        progressive_win_value,  
        progressive_win_Handpay_value,  
        Mystery_Machine_Paid,  
        Mystery_Attendant_Paid,  
        TICKETS_INSERTED_NONCASHABLE_VALUE,  
        TICKETS_PRINTED_NONCASHABLE_VALUE,  
        Promo_Cashable_EFT_IN,  
        Promo_Cashable_EFT_OUT,  
        NonCashable_EFT_IN,  
        NonCashable_EFT_OUT,  
        Cashable_EFT_IN,  
        Cashable_EFT_OUT,  
        READ_RDC_BILL_200,  
        READ_RDC_BILL_500,
        ReadDate
  )  
  SELECT      
        PAYOUT_FLOAT_TOKEN,  
        PAYOUT_FLOAT_10P,  
        PAYOUT_FLOAT_20P,  
        PAYOUT_FLOAT_50P,  
        PAYOUT_FLOAT_100P,  
        CASH_IN_1P,
        CASH_IN_2P,  
        CASH_IN_5P,  
        CASH_IN_10P,  
        CASH_IN_20P,  
        CASH_IN_50P,  
        CASH_IN_100P,  
        CASH_IN_200P,  
        CASH_IN_500P,  
        CASH_IN_1000P,  
        CASH_IN_2000P,  
        CASH_IN_5000P,  
        CASH_IN_10000P,  
        CASH_IN_20000P,  
        CASH_IN_50000P,  
        CASH_IN_100000P,  
        TOKEN_IN_5P,  
        TOKEN_IN_10P,  
        TOKEN_IN_20P,  
        TOKEN_IN_50P,  
        TOKEN_IN_100P,  
        TOKEN_IN_200P,  
        TOKEN_IN_500P,  
        TOKEN_IN_1000P,  
        CASH_OUT_1P,
        CASH_OUT_2P,  
        CASH_OUT_5P,  
        CASH_OUT_10P,  
        CASH_OUT_20P,  
        CASH_OUT_50P,  
        CASH_OUT_100P,  
        CASH_OUT_200P,  
        CASH_OUT_500P,  
        CASH_OUT_1000P,  
        CASH_OUT_2000P,  
        CASH_OUT_5000P,  
        CASH_OUT_10000P,  
        CASH_OUT_20000P,  
        CASH_OUT_50000P,  
        CASH_OUT_100000P,  
        TOKEN_OUT_5P,  
        TOKEN_OUT_10P,  
        TOKEN_OUT_20P,  
        TOKEN_OUT_50P,  
        TOKEN_OUT_100P,  
        TOKEN_OUT_200P,  
        TOKEN_OUT_500P,  
        TOKEN_OUT_1000P,  
        CASH_REFILL_5P,  
        CASH_REFILL_10P,  
        CASH_REFILL_20P,  
        CASH_REFILL_50P,  
        CASH_REFILL_100P,  
        CASH_REFILL_200P,  
        CASH_REFILL_500P,  
        CASH_REFILL_1000P,  
        CASH_REFILL_2000P,  
        CASH_REFILL_5000P,  
        CASH_REFILL_10000P,  
        CASH_REFILL_20000P,  
        CASH_REFILL_50000P,  
        CASH_REFILL_100000P,  
        TOKEN_REFILL_5P,  
        TOKEN_REFILL_10P,  
        TOKEN_REFILL_20P,  
        TOKEN_REFILL_50P,  
        TOKEN_REFILL_100P,  
        TOKEN_REFILL_200P,  
        TOKEN_REFILL_500P,  
        TOKEN_REFILL_1000P,  
        READ_COINS_IN,  
        READ_COINS_OUT,  
        READ_COIN_DROP,  
        READ_HANDPAY,  
        READ_EXTERNAL_CREDIT,  
        READ_GAMES_BET,  
        READ_GAMES_WON,  
        READ_NOTES,  
        VTP,  
        READ_RDC_CANCELLED_CREDITS,  
        READ_RDC_GAMES_LOST,  
        READ_RDC_GAMES_SINCE_POWER_UP,  
        READ_RDC_TRUE_COIN_IN,  
        READ_RDC_TRUE_COIN_OUT,  
        READ_RDC_CURRENT_CREDITS,  
        READ_RDC_BILL_1,  
        READ_RDC_BILL_2,  
        READ_RDC_BILL_5,  
        READ_RDC_BILL_10,  
        READ_RDC_BILL_20,  
        READ_RDC_BILL_50,  
        READ_RDC_BILL_100,  
        READ_RDC_BILL_250,  
        READ_RDC_BILL_10000,  
        READ_RDC_BILL_20000,  
        READ_RDC_BILL_50000,  
        READ_RDC_BILL_100000,  
        CONVERT(VARCHAR, Read_Date, 106),  
        CONVERT(VARCHAR, Read_Date, 108),  
        READ_TICKET,  
        READ_TICKET_VALUE,  
        READ_TICKET_IN_SUSPENSE,  
        READ_TICKET_IN_SUSPENSE_VALUE,  
        Read_Forced,  
        CONVERT(VARCHAR, Previous_Read_Date, 106),  
        CONVERT(VARCHAR, Previous_Read_Date, 108),  
        Week_ID,  
        Period_ID,  
        Operator_Week_ID,  
        Operator_Period_ID,  
        READ_RDC_Datapak_Type,  
        READ_RDC_Datapak_Version,  
        Read_Occurrence,  
        ISNULL(READ_RDC_JACKPOT, 0),  
        progressive_win_value,  
        progressive_win_Handpay_value,  
        Mystery_Machine_Paid,  
        Mystery_Attendant_Paid,  
        TICKETS_INSERTED_NONCASHABLE_VALUE,  
        TICKETS_PRINTED_NONCASHABLE_VALUE,  
        Promo_Cashable_EFT_IN,  
        Promo_Cashable_EFT_OUT,  
        NonCashable_EFT_IN,  
        NonCashable_EFT_OUT,  
        Cashable_EFT_IN,  
        Cashable_EFT_OUT,  
        READ_RDC_BILL_200,  
        READ_RDC_BILL_500,
        CAST(CONVERT (VARCHAR(30), CAST(Read_Date AS DATETIME), 106) AS DATETIME)
  FROM       OPENXML (@idoc, './Read',2)  
  WITH #TempReadInsert--[Read]   
  
  Set @newID=SCOPE_IDENTITY()  
  SELECT @newID  
  set @error =@@ERROR  
  if @error <> 0 goto err_Handler  
  
  update [READ] Set Installation_ID=@InstallationNo,  
       Read_Date=convert(varchar(50),cast(Read_Date as DateTime),106),  
       --Populate read_days    
       Read_days=  
         Case   
         When @Readdate is Null Then  
          1  
         Else  
          DATEDIFF(DAY, @Readdate, cast(Read_Date as DateTime))         
         End       
  Where Read_ID=@newID  

  If @Read_Date < @Installation_Date
  Begin
		update [read] set Read_days = 0 where Read_ID=@newID  
  End

 End  
Else  
 Begin  
  select * into #tempRead from OPENXML (@idoc, './Read',2)  
  WITH #TempReadInsert   
  
  Update [READ]  
  Set  
  [READ].PAYOUT_FLOAT_TOKEN=#tempRead.PAYOUT_FLOAT_TOKEN,  
  [READ].PAYOUT_FLOAT_10P=#tempRead.PAYOUT_FLOAT_10P,  
  [READ].PAYOUT_FLOAT_20P=#tempRead.PAYOUT_FLOAT_20P,  
  [READ].PAYOUT_FLOAT_50P=#tempRead.PAYOUT_FLOAT_50P,  
  [READ].PAYOUT_FLOAT_100P=#tempRead.PAYOUT_FLOAT_100P, 
  [READ].CASH_IN_1P=#tempRead.CASH_IN_1P, 
  [READ].CASH_IN_2P=#tempRead.CASH_IN_2P,  
  [READ].CASH_IN_5P=#tempRead.CASH_IN_5P,  
  [READ].CASH_IN_10P=#tempRead.CASH_IN_10P,  
  [READ].CASH_IN_20P=#tempRead.CASH_IN_20P,  
  [READ].CASH_IN_50P=#tempRead.CASH_IN_50P,  
  [READ].CASH_IN_100P=#tempRead.CASH_IN_100P,  
  [READ].CASH_IN_200P=#tempRead.CASH_IN_200P,  
  [READ].CASH_IN_500P=#tempRead.CASH_IN_500P,  
  [READ].CASH_IN_1000P=#tempRead.CASH_IN_1000P,  
  [READ].CASH_IN_2000P=#tempRead.CASH_IN_2000P,  
  [READ].CASH_IN_5000P=#tempRead.CASH_IN_5000P,  
  [READ].CASH_IN_10000P=#tempRead.CASH_IN_10000P,  
  [READ].CASH_IN_20000P=#tempRead.CASH_IN_20000P,  
  [READ].CASH_IN_50000P=#tempRead.CASH_IN_50000P,  
  [READ].CASH_IN_100000P=#tempRead.CASH_IN_100000P,  
  [READ].TOKEN_IN_5P=#tempRead.TOKEN_IN_5P,  
  [READ].TOKEN_IN_10P=#tempRead.TOKEN_IN_10P,  
  [READ].TOKEN_IN_20P=#tempRead.TOKEN_IN_20P,  
  [READ].TOKEN_IN_50P=#tempRead.TOKEN_IN_50P,  
  [READ].TOKEN_IN_100P=#tempRead.TOKEN_IN_100P,  
  [READ].TOKEN_IN_200P=#tempRead.TOKEN_IN_200P,  
  [READ].TOKEN_IN_500P=#tempRead.TOKEN_IN_500P,  
  [READ].TOKEN_IN_1000P=#tempRead.TOKEN_IN_1000P, 
  [READ].CASH_OUT_1P=#tempRead.CASH_OUT_1P, 
  [READ].CASH_OUT_2P=#tempRead.CASH_OUT_2P,  
  [READ].CASH_OUT_5P=#tempRead.CASH_OUT_5P,  
  [READ].CASH_OUT_10P=#tempRead.CASH_OUT_10P,  
  [READ].CASH_OUT_20P=#tempRead.CASH_OUT_20P,  
  [READ].CASH_OUT_50P=#tempRead.CASH_OUT_50P,  
  [READ].CASH_OUT_100P=#tempRead.CASH_OUT_100P,  
  [READ].CASH_OUT_200P=#tempRead.CASH_OUT_200P,  
  [READ].CASH_OUT_500P=#tempRead.CASH_OUT_500P,  
  [READ].CASH_OUT_1000P=#tempRead.CASH_OUT_1000P,  
  [READ].CASH_OUT_2000P=#tempRead.CASH_OUT_2000P,  
  [READ].CASH_OUT_5000P=#tempRead.CASH_OUT_5000P,  
  [READ].CASH_OUT_10000P=#tempRead.CASH_OUT_10000P,  
  [READ].CASH_OUT_20000P=#tempRead.CASH_OUT_20000P,  
  [READ].CASH_OUT_50000P=#tempRead.CASH_OUT_50000P,  
  [READ].CASH_OUT_100000P=#tempRead.CASH_OUT_100000P,  
  [READ].TOKEN_OUT_5P=#tempRead.TOKEN_OUT_5P,  
  [READ].TOKEN_OUT_10P=#tempRead.TOKEN_OUT_10P,  
  [READ].TOKEN_OUT_20P=#tempRead.TOKEN_OUT_20P,  
  [READ].TOKEN_OUT_50P=#tempRead.TOKEN_OUT_50P,  
  [READ].TOKEN_OUT_100P=#tempRead.TOKEN_OUT_100P,  
  [READ].TOKEN_OUT_200P=#tempRead.TOKEN_OUT_200P,  
  [READ].TOKEN_OUT_500P=#tempRead.TOKEN_OUT_500P,  
  [READ].TOKEN_OUT_1000P=#tempRead.TOKEN_OUT_1000P,  
  [READ].CASH_REFILL_5P=#tempRead.CASH_REFILL_5P,  
  [READ].CASH_REFILL_10P=#tempRead.CASH_REFILL_10P,  
  [READ].CASH_REFILL_20P=#tempRead.CASH_REFILL_20P,  
  [READ].CASH_REFILL_50P=#tempRead.CASH_REFILL_50P,  
  [READ].CASH_REFILL_100P=#tempRead.CASH_REFILL_100P,  
  [READ].CASH_REFILL_200P=#tempRead.CASH_REFILL_200P,  
  [READ].CASH_REFILL_500P=#tempRead.CASH_REFILL_500P,  
  [READ].CASH_REFILL_1000P=#tempRead.CASH_REFILL_1000P,  
  [READ].CASH_REFILL_2000P=#tempRead.CASH_REFILL_2000P,  
  [READ].CASH_REFILL_5000P=#tempRead.CASH_REFILL_5000P,  
  [READ].CASH_REFILL_10000P=#tempRead.CASH_REFILL_10000P,  
  [READ].CASH_REFILL_20000P=#tempRead.CASH_REFILL_20000P,  
  [READ].CASH_REFILL_50000P=#tempRead.CASH_REFILL_50000P,  
  [READ].CASH_REFILL_100000P=#tempRead.CASH_REFILL_100000P,  
  [READ].TOKEN_REFILL_5P=#tempRead.TOKEN_REFILL_5P,  
  [READ].TOKEN_REFILL_10P=#tempRead.TOKEN_REFILL_10P,  
  [READ].TOKEN_REFILL_20P=#tempRead.TOKEN_REFILL_20P,  
  [READ].TOKEN_REFILL_50P=#tempRead.TOKEN_REFILL_50P,  
  [READ].TOKEN_REFILL_100P=#tempRead.TOKEN_REFILL_100P,  
  [READ].TOKEN_REFILL_200P=#tempRead.TOKEN_REFILL_200P,  
  [READ].TOKEN_REFILL_500P=#tempRead.TOKEN_REFILL_500P,  
  [READ].TOKEN_REFILL_1000P=#tempRead.TOKEN_REFILL_1000P,  
  [READ].READ_COINS_IN=#tempRead.READ_COINS_IN,  
  [READ].READ_COINS_OUT=#tempRead.READ_COINS_OUT,  
  [READ].READ_COIN_DROP=#tempRead.READ_COIN_DROP,  
  [READ].READ_HANDPAY=#tempRead.READ_HANDPAY,  
  [READ].READ_EXTERNAL_CREDIT=#tempRead.READ_EXTERNAL_CREDIT,  
  [READ].READ_GAMES_BET=#tempRead.READ_GAMES_BET,  
  [READ].READ_GAMES_WON=#tempRead.READ_GAMES_WON,  
  [READ].READ_NOTES=#tempRead.READ_NOTES,  
  [READ].VTP=#tempRead.VTP,  
  [READ].READ_RDC_CANCELLED_CREDITS=#tempRead.READ_RDC_CANCELLED_CREDITS,  
  [READ].READ_RDC_GAMES_LOST=#tempRead.READ_RDC_GAMES_LOST,  
  [READ].READ_RDC_GAMES_SINCE_POWER_UP=#tempRead.READ_RDC_GAMES_SINCE_POWER_UP,  
  [READ].READ_RDC_TRUE_COIN_IN=#tempRead.READ_RDC_TRUE_COIN_IN,  
  [READ].READ_RDC_TRUE_COIN_OUT=#tempRead.READ_RDC_TRUE_COIN_OUT,  
  [READ].READ_RDC_CURRENT_CREDITS =#tempRead.READ_RDC_CURRENT_CREDITS,  
  [READ].READ_RDC_BILL_1=#tempRead.READ_RDC_BILL_1,  
  [READ].READ_RDC_BILL_2=#tempRead.READ_RDC_BILL_2,  
  [READ].READ_RDC_BILL_5=#tempRead.READ_RDC_BILL_5,  
  [READ].READ_RDC_BILL_10=#tempRead.READ_RDC_BILL_10,  
  [READ].READ_RDC_BILL_20=#tempRead.READ_RDC_BILL_20,  
  [READ].READ_RDC_BILL_50=#tempRead.READ_RDC_BILL_50,  
  [READ].READ_RDC_BILL_100=#tempRead.READ_RDC_BILL_100,  
  [READ].READ_RDC_BILL_250=#tempRead.READ_RDC_BILL_250,  
  [READ].READ_RDC_BILL_10000=#tempRead.READ_RDC_BILL_10000,  
  [READ].READ_RDC_BILL_20000=#tempRead.READ_RDC_BILL_20000,  
  [READ].READ_RDC_BILL_50000=#tempRead.READ_RDC_BILL_50000,  
  [READ].READ_RDC_BILL_100000=#tempRead.READ_RDC_BILL_100000,  
  
  --[READ].Installation_No Installation_ID,  
  [READ].Read_Date=CONVERT(VARCHAR(50),#tempRead.Read_Date,106),  
  [READ].Read_Time=CONVERT(VARCHAR(50),#tempRead.Read_Date,108),  
  
  [READ].READ_TICKET=#tempRead.READ_TICKET,  
  [READ].READ_TICKET_VALUE=#tempRead.READ_TICKET_VALUE,  
  [READ].READ_TICKET_IN_SUSPENSE=#tempRead.READ_TICKET_IN_SUSPENSE,  
  [READ].READ_TICKET_IN_SUSPENSE_VALUE=#tempRead.READ_TICKET_IN_SUSPENSE_VALUE,  
  
  [READ].Read_Forced=#tempRead.Read_Forced,  
  [READ].Previous_Read_Date=CONVERT(VARCHAR, #tempRead.Previous_Read_Date, 106),  
  [READ].Previous_Read_Time=CONVERT(VARCHAR, #tempRead.Previous_Read_Date, 108),  
  
  [READ].Week_ID=#tempRead.Week_ID,  
  [READ].Period_ID=#tempRead.Period_ID,  
  [READ].Operator_Week_ID=#tempRead.Operator_Week_ID,  
  [READ].Operator_Period_ID=#tempRead.Operator_Period_ID,  
  
  --Extra Columns in Enterprise  
  
  [READ].READ_RDC_Datapak_Type=#tempRead.READ_RDC_Datapak_Type,  
  [READ].READ_RDC_Datapak_Version=#tempRead.READ_RDC_Datapak_Version,  
  [READ].Read_Occurrence=#tempRead.Read_Occurrence,  
  
  --End Extra Columns  
  [READ].READ_RDC_JACKPOT = ISNULL(#tempRead.READ_RDC_JACKPOT, 0),  
  [READ].progressive_win_value=#tempRead.progressive_win_value,  
  [READ].progressive_win_Handpay_value=#tempRead.progressive_win_Handpay_value,  
  [READ].Mystery_Machine_Paid = #tempRead.Mystery_Machine_Paid,  
  [READ].Mystery_Attendant_Paid = #tempRead.Mystery_Attendant_Paid,  
  [READ].TICKETS_INSERTED_NONCASHABLE_VALUE = #tempRead.TICKETS_INSERTED_NONCASHABLE_VALUE,  
  [READ].TICKETS_PRINTED_NONCASHABLE_VALUE = #tempRead.TICKETS_PRINTED_NONCASHABLE_VALUE,  
  [READ].Promo_Cashable_EFT_IN = #tempRead.Promo_Cashable_EFT_IN,  
  [READ].Promo_Cashable_EFT_OUT = #tempRead.Promo_Cashable_EFT_OUT,  
  [READ].NonCashable_EFT_IN = #tempRead.NonCashable_EFT_IN,  
  [READ].NonCashable_EFT_OUT = #tempRead.NonCashable_EFT_OUT,  
  [READ].Cashable_EFT_IN = #tempRead.Cashable_EFT_IN,  
  [READ].Cashable_EFT_OUT = #tempRead.Cashable_EFT_OUT,  
  [READ].READ_RDC_BILL_200=#tempRead.READ_RDC_BILL_200,  
  [READ].READ_RDC_BILL_500=#tempRead.READ_RDC_BILL_500,  
  [READ].ReadDate = CAST(CONVERT (VARCHAR(30), CAST(#tempRead.Read_Date AS DATETIME), 106) AS DATETIME)
  From #tempRead   
  
  Where [READ].Read_ID=@ReadID  
  
  drop table #tempRead  
  
  set @error =@@ERROR  
 End  
EXEC sp_xml_removedocument @idoc  

-- Update MH_LinkReference with Read_ID for Previous & Current Meter History ID
IF(ISNULL(@PreviousMHID,0) <> 0 AND ISNULL(@CurrentMHID,0) <> 0)
BEGIN
    UPDATE Meter_History SET MH_LinkReference = @newID
    WHERE MH_Installation_No = @InstallationNo AND MH_Process = 'READ' 
    AND MH_ID IN (@PreviousMHID, @CurrentMHID) AND MH_LinkReference IS NULL
END

err_Handler:  
  
if @error = 0  
 Set @IsSuccess =0 --success  
else  
  Set @IsSuccess =@error --error  
  


GO

