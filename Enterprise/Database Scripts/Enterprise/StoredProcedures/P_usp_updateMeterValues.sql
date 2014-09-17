USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_updateMeterValues]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_updateMeterValues]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE       PROCEDURE [dbo].[usp_updateMeterValues]   
 -- Add the parameters for the stored procedure here  
 @ID bigInt,   -- For Collection/VTP/READ ID  
 @RType Varchar(10), -- Type of Record, its Collection/VTP/READ  
 @updateREAD bigInt=0, -- For Checking whether to update READ from VTP or not  
 @HourNo bigInt=-1,  -- If its VTP record, this will contain Hour no  
 @vtpDate DateTime, -- Required to get ReadID from VTP  
-- updated P Meter values   
 @Bill100Meter bigInt,  
 @Bill50Meter bigInt,  
 @Bill20Meter bigInt,  
 @Bill10Meter bigInt,  
 @Bill5Meter bigInt,  
 @Bill1Meter bigInt,  
 @TrueCoinIn bigInt,  
 @TrueCoinOut bigInt,  
 @CoinIn bigInt,  
 @CoinOut bigInt,  
 @Drop bigInt,  
 @Jackpot bigInt,  
 @CancelledCredits bigInt,  
 @HandPaidCancelledCredits bigInt,  
 @CashableTicketsInValue bigInt,  
 @CashableTicketsOutValue bigInt,  
 @CashableTicketsInNo bigInt,  
 @CashableTicketsOutNo bigInt,  
-- C Meter values   
 @cBill100Meter bigInt,  
 @cBill50Meter bigInt,  
 @cBill20Meter bigInt,  
 @cBill10Meter bigInt,  
 @cBill5Meter bigInt,  
 @cBill1Meter bigInt,  
 @cTrueCoinIn bigInt,  
 @cTrueCoinOut bigInt,  
 @cCoinIn bigInt,  
 @cCoinOut bigInt,  
 @cDrop bigInt,  
 @cJackpot bigInt,  
 @cCancelledCredits bigInt,  
 @cHandPaidCancelledCredits bigInt,  
 @cCashableTicketsInValue bigInt,  
 @cCashableTicketsOutValue bigInt,  
 @cCashableTicketsInNo bigInt,  
 @cCashableTicketsOutNo bigInt,  
 @readID bigInt output  
AS  
 Declare @TotalHourlyCashIn bigInt,  
   @TotalHourlyCashOut bigInt,  
   @cTotalHourlyCashIn bigInt,  
   @cTotalHourlyCashOut bigInt,  
   @TotalOriginalHourlyCashIn bigInt,  
   @TotalOriginalHourlyCashOut bigInt,  
   @TotalDailyCashIn bigInt,  
   @TotalDailyCashOut bigInt,  
   @cTotalDailyCashIn bigInt,  
   @cTotalDailyCashOut bigInt,  
   @installationID bigInt,  
   @installationPricePerPlay bigInt,  
   @params as nvarchar(3000),  
   @ssql as nvarchar(3000),  
   @ExceptionType as smallint,  
   @ExceptionUser as varchar(50),  
   @ExceptionDetail as Varchar(1000),  
   @FullExceptionDetail as Varchar(1000)  

--Declare for saving record in Exception table  
   Declare @P_MH_BILL_100 VarChar(100),  
   @P_MH_BILL_50 VarChar(100),  
   @P_MH_BILL_20 VarChar(100),  
   @P_MH_BILL_10 VarChar(100),  
   @P_MH_BILL_5 VarChar(100),  
   @P_MH_BILL_1 VarChar(100),  
   @P_TrueCoinIn  VarChar(100),  
   @P_TrueCoinOut VarChar(100),  
   @P_CoinIn VarChar(100),  
   @P_CoinOut VarChar(100),  
   @P_Drop VarChar(100),  
   @P_CancelledCredits VarChar(100),  
   @P_HandPaidCancelledCredits VarChar(100),  
   @P_CashableTicketsInValue VarChar(100),  
   @P_CashableTicketsOutValue VarChar(100),  
   @P_CashableTicketsInNo VarChar(100),  
   @P_CashableTicketsOutNo VarChar(100),  
  
--General Variables Declaration  
   @varInstallationID varchar(50),  
   @varID as varchar(50)  
  
BEGIN  
 -- SET NOCOUNT ON added to prevent extra result sets from  
 -- interfering with SELECT statements.  
 SET NOCOUNT ON;  
 SET @ExceptionUser = 'IMPORT SYSTEM'  
 set @readID=-1  
 --SET DateFormat dmy  
    -- Insert statements for procedure here  
 BEGIN  
  select top 1 * into #tmptablename from Meter_History   
  Where MH_linkreference=@ID   
  AND(  
    (  
     @RType='VTP'  
     and MH_Process = 'VTP' and MH_Reference=@HourNo and MH_Type='P'  
    )  
    OR  
    (  
     @RType='Daily'  
     and MH_Process = 'READ' and MH_Type='P'  
    )  
    OR  
    (  
     @RType='Collection'  
     and MH_Process = 'COLL' and MH_Type='P'  
    )  
   )  
  Order by MH_ID Desc  
  --Create 1 temp table for putting previous P values into that , so that we can save it in Exception Table  
  select * into #tmptablePvalues from #tmptablename   
  SET @P_MH_BILL_100=(select MH_BILL_100 from #tmptablePvalues)  
  SET @P_MH_BILL_50=(select MH_BILL_50 from #tmptablePvalues)  
  SET @P_MH_BILL_20=(select MH_BILL_20 from #tmptablePvalues)  
  SET @P_MH_BILL_10=(select MH_BILL_10 from #tmptablePvalues)  
  SET @P_MH_BILL_5=(select MH_BILL_5 from #tmptablePvalues)  
  SET @P_MH_BILL_1=(select MH_BILL_1 from #tmptablePvalues)  
  SET @P_TrueCoinIn=(select MH_True_Coin_In from #tmptablePvalues)  
  SET @P_TrueCoinOut=(select MH_True_Coin_Out from #tmptablePvalues)  
  SET @P_CoinIn=(select MH_Coins_In from #tmptablePvalues)  
  SET @P_CoinOut=(select MH_Coins_Out from #tmptablePvalues)  
  SET @P_Drop=(select MH_Coin_Drop from #tmptablePvalues)  
  SET @P_CancelledCredits=(select MH_Cancelled_Credits from #tmptablePvalues)  
  SET @P_HandPaidCancelledCredits=(select MH_HandPay from #tmptablePvalues)  
  SET @P_CashableTicketsInValue=(select MH_Ticket_Inserted_Value from #tmptablePvalues)  
  SET @P_CashableTicketsOutValue=(select MH_Ticket_Printed_Value from #tmptablePvalues)  
  SET @P_CashableTicketsInNo=(select MH_Ticket_Inserted_Qty from #tmptablePvalues)  
  SET @P_CashableTicketsOutNo=(select MH_Ticket_Printed_Qty from #tmptablePvalues)  
  Drop table  #tmptablePvalues  
    
  Update #tmptablename SET MH_BILL_100=@Bill100Meter,  
  MH_BILL_50=@Bill50Meter,MH_BILL_20=@Bill20Meter,MH_BILL_10=@Bill10Meter,  
  MH_BILL_5=@Bill5Meter,MH_BILL_1=@Bill1Meter,MH_True_Coin_In=@TrueCoinIn,  
  MH_True_Coin_Out=@TrueCoinOut,MH_Coins_In=@CoinIn,MH_Coins_Out=@CoinOut,  
  MH_Coin_Drop=@Drop,MH_Cancelled_Credits=@CancelledCredits,  
  MH_HandPay=@HandPaidCancelledCredits,  
  MH_Ticket_Inserted_Value=@CashableTicketsInValue,  
  MH_Ticket_Printed_Value=@CashableTicketsOutValue,  
  MH_Ticket_Inserted_Qty=@CashableTicketsInNo,  
  MH_Ticket_Printed_Qty=@CashableTicketsOutNo  
    
--  Insert into Meter_History select * from #tmptablename  
  Insert into Meter_History  
  (  
  MH_Process,  
  MH_Type,  
  MH_LinkReference,  
  MH_Reference,  
  MH_Installation_No,  
  MH_Datetime,  
  MH_PAYOUT_FLOAT_TOKEN,  
  MH_PAYOUT_FLOAT_10P,  
  MH_PAYOUT_FLOAT_20P,  
  MH_PAYOUT_FLOAT_50P,  
  MH_PAYOUT_FLOAT_100P,  
  MH_CASH_IN_1P, 
  MH_CASH_IN_2P,  
  MH_CASH_IN_5P,  
  MH_CASH_IN_10P,  
  MH_CASH_IN_20P,  
  MH_CASH_IN_50P,  
  MH_CASH_IN_100P,  
  MH_CASH_IN_200P,  
  MH_CASH_IN_500P,  
  MH_CASH_IN_1000P,  
  MH_CASH_IN_2000P,  
  MH_CASH_IN_5000P,  
  MH_CASH_IN_10000P,  
  MH_CASH_IN_20000P,  
  MH_CASH_IN_50000P,  
  MH_CASH_IN_100000P,  
  MH_TOKEN_IN_5P,  
  MH_TOKEN_IN_10P,  
  MH_TOKEN_IN_20P,  
  MH_TOKEN_IN_50P,  
  MH_TOKEN_IN_100P,  
  MH_TOKEN_IN_200P,  
  MH_TOKEN_IN_500P,  
  MH_TOKEN_IN_1000P,  
  MH_CASH_OUT_1P, 
  MH_CASH_OUT_2P,  
  MH_CASH_OUT_5P,  
  MH_CASH_OUT_10P,  
  MH_CASH_OUT_20P,  
  MH_CASH_OUT_50P,  
  MH_CASH_OUT_100P,  
  MH_CASH_OUT_200P,  
  MH_CASH_OUT_500P,  
  MH_CASH_OUT_1000P,  
  MH_CASH_OUT_2000P,  
  MH_CASH_OUT_5000P,  
  MH_CASH_OUT_10000P,  
  MH_CASH_OUT_20000P,  
  MH_CASH_OUT_50000P,  
  MH_CASH_OUT_100000P,  
  MH_TOKEN_OUT_5P,  
  MH_TOKEN_OUT_10P,  
  MH_TOKEN_OUT_20P,  
  MH_TOKEN_OUT_50P,  
  MH_TOKEN_OUT_100P,  
  MH_TOKEN_OUT_200P,  
  MH_TOKEN_OUT_500P,  
  MH_TOKEN_OUT_1000P,  
  MH_CASH_REFILL_5P,  
  MH_CASH_REFILL_10P,  
  MH_CASH_REFILL_20P,  
  MH_CASH_REFILL_50P,  
  MH_CASH_REFILL_100P,  
  MH_CASH_REFILL_200P,  
  MH_CASH_REFILL_500P,  
  MH_CASH_REFILL_1000P,  
  MH_CASH_REFILL_2000P,  
  MH_CASH_REFILL_5000P,  
  MH_CASH_REFILL_10000P,  
  MH_CASH_REFILL_20000P,  
  MH_CASH_REFILL_50000P,  
  MH_CASH_REFILL_100000P,  
  MH_TOKEN_REFILL_5P,  
  MH_TOKEN_REFILL_10P,  
  MH_TOKEN_REFILL_20P,  
  MH_TOKEN_REFILL_50P,  
  MH_TOKEN_REFILL_100P,  
  MH_TOKEN_REFILL_200P,  
  MH_TOKEN_REFILL_500P,  
  MH_TOKEN_REFILL_1000P,  
  MH_COINS_IN,  
  MH_COINS_OUT,  
  MH_COIN_DROP,  
  MH_HANDPAY,  
  MH_EXTERNAL_CREDIT,  
  MH_GAMES_BET,  
  MH_GAMES_WON,  
  MH_NOTES,  
  MH_VTP,  
  MH_CANCELLED_CREDITS,  
  MH_GAMES_LOST,  
  MH_GAMES_SINCE_POWER_UP,  
  MH_TRUE_COIN_IN,  
  MH_TRUE_COIN_OUT,  
  MH_CURRENT_CREDITS,  
  MH_JACKPOT,  
  MH_BILL_1,  
  MH_BILL_2,  
  MH_BILL_5,  
  MH_BILL_10,  
  MH_BILL_20,  
  MH_BILL_50,  
  MH_BILL_100,  
  MH_BILL_250,  
  MH_BILL_10000,  
  MH_BILL_20000,  
  MH_BILL_50000,  
  MH_BILL_100000,  
  MH_TICKET_PRINTED_QTY,  
  MH_TICKET_PRINTED_VALUE,  
  MH_TICKET_INSERTED_QTY,  
  MH_TICKET_INSERTED_VALUE  
  )   
  
  Select    
    
  MH_Process,  
  MH_Type,  
  MH_LinkReference,  
  MH_Reference,  
  MH_Installation_No,  
--  MH_Datetime,  
  convert(varchar(12),getdate(),106) + ' ' +  convert(varchar(8),getdate(),114),  
  MH_PAYOUT_FLOAT_TOKEN,  
  MH_PAYOUT_FLOAT_10P,  
  MH_PAYOUT_FLOAT_20P,  
  MH_PAYOUT_FLOAT_50P,  
  MH_PAYOUT_FLOAT_100P,
  MH_CASH_IN_1P,  
  MH_CASH_IN_2P,  
  MH_CASH_IN_5P,  
  MH_CASH_IN_10P,  
  MH_CASH_IN_20P,  
  MH_CASH_IN_50P,  
  MH_CASH_IN_100P,  
  MH_CASH_IN_200P,  
  MH_CASH_IN_500P,  
  MH_CASH_IN_1000P,  
  MH_CASH_IN_2000P,  
  MH_CASH_IN_5000P,  
  MH_CASH_IN_10000P,  
  MH_CASH_IN_20000P,  
  MH_CASH_IN_50000P,  
  MH_CASH_IN_100000P,  
  MH_TOKEN_IN_5P,  
  MH_TOKEN_IN_10P,  
  MH_TOKEN_IN_20P,  
  MH_TOKEN_IN_50P,  
  MH_TOKEN_IN_100P,  
  MH_TOKEN_IN_200P,  
  MH_TOKEN_IN_500P,  
  MH_TOKEN_IN_1000P,
  MH_CASH_OUT_1P,  
  MH_CASH_OUT_2P,  
  MH_CASH_OUT_5P,  
  MH_CASH_OUT_10P,  
  MH_CASH_OUT_20P,  
  MH_CASH_OUT_50P,  
  MH_CASH_OUT_100P,  
  MH_CASH_OUT_200P,  
  MH_CASH_OUT_500P,  
  MH_CASH_OUT_1000P,  
  MH_CASH_OUT_2000P,  
  MH_CASH_OUT_5000P,  
  MH_CASH_OUT_10000P,  
  MH_CASH_OUT_20000P,  
  MH_CASH_OUT_50000P,  
  MH_CASH_OUT_100000P,  
  MH_TOKEN_OUT_5P,  
  MH_TOKEN_OUT_10P,  
  MH_TOKEN_OUT_20P,  
  MH_TOKEN_OUT_50P,  
  MH_TOKEN_OUT_100P,  
  MH_TOKEN_OUT_200P,  
  MH_TOKEN_OUT_500P,  
  MH_TOKEN_OUT_1000P,  
  MH_CASH_REFILL_5P,  
  MH_CASH_REFILL_10P,  
  MH_CASH_REFILL_20P,  
  MH_CASH_REFILL_50P,  
  MH_CASH_REFILL_100P,  
  MH_CASH_REFILL_200P,  
  MH_CASH_REFILL_500P,  
  MH_CASH_REFILL_1000P,  
  MH_CASH_REFILL_2000P,  
  MH_CASH_REFILL_5000P,  
  MH_CASH_REFILL_10000P,  
  MH_CASH_REFILL_20000P,  
  MH_CASH_REFILL_50000P,  
  MH_CASH_REFILL_100000P,  
  MH_TOKEN_REFILL_5P,  
  MH_TOKEN_REFILL_10P,  
  MH_TOKEN_REFILL_20P,  
  MH_TOKEN_REFILL_50P,  
  MH_TOKEN_REFILL_100P,  
  MH_TOKEN_REFILL_200P,  
  MH_TOKEN_REFILL_500P,  
  MH_TOKEN_REFILL_1000P,  
  MH_COINS_IN,  
  MH_COINS_OUT,  
  MH_COIN_DROP,  
  MH_HANDPAY,  
  MH_EXTERNAL_CREDIT,  
  MH_GAMES_BET,  
  MH_GAMES_WON,  
  MH_NOTES,  
  MH_VTP,  
  MH_CANCELLED_CREDITS,  
  MH_GAMES_LOST,  
  MH_GAMES_SINCE_POWER_UP,  
  MH_TRUE_COIN_IN,  
  MH_TRUE_COIN_OUT,  
  MH_CURRENT_CREDITS,  
  MH_JACKPOT,  
  MH_BILL_1,  
  MH_BILL_2,  
  MH_BILL_5,  
  MH_BILL_10,  
  MH_BILL_20,  
  MH_BILL_50,  
  MH_BILL_100,  
  MH_BILL_250,  
  MH_BILL_10000,  
  MH_BILL_20000,  
  MH_BILL_50000,  
  MH_BILL_100000,  
  MH_TICKET_PRINTED_QTY,  
  MH_TICKET_PRINTED_VALUE,  
  MH_TICKET_INSERTED_QTY,  
  MH_TICKET_INSERTED_VALUE  
     
  from #tmptablename  
      
  Drop table #tmptablename  
  
    
 END  
 if(@Rtype='VTP')  
  Begin  
   SET @installationID=(Select Installation_ID from VTP where VTP_ID=@ID)  
   EXEC usp_UpdateLastMeterDeltas @installationID,'VTP',@ID  
  
   if(@updateREAD=1)  
    Begin  
       
     --To get latest P record of VTP  
  
     select top 1 * into #tmptablename1 from Meter_History   
     Where MH_linkreference=@ID   
     and MH_Process = 'VTP' and MH_Reference=@HourNo and MH_Type='P'  
     Order by MH_ID Desc  
    
--     Alter table #tmptablename1 drop column MH_ID  
  
     --To Get 2nd Latest P record of VTP  
  
     select top 1 *  into #tmptablename2 from meter_history   
     where MH_ID!=(select max(mh_id) from meter_history where    
     MH_Process = 'VTP' and MH_Reference=@HourNo and MH_Type='P')   
     AND MH_Process = 'VTP' and MH_Reference=@HourNo and MH_Type='P'  
     Order by MH_ID Desc  
  
--     Alter table #tmptablename2 drop column MH_ID  
  
     Update #tmptablename2 SET #tmptablename2.MH_BILL_100=#tmptablename2.MH_BILL_100-#tmptablename1.MH_BILL_100,  
     #tmptablename2.MH_BILL_50=#tmptablename2.MH_BILL_50-#tmptablename1.MH_BILL_50,  
     #tmptablename2.MH_BILL_20=#tmptablename2.MH_BILL_20-#tmptablename1.MH_BILL_20,  
     #tmptablename2.MH_BILL_10=#tmptablename2.MH_BILL_10-#tmptablename1.MH_BILL_10,  
     #tmptablename2.MH_BILL_5=#tmptablename2.MH_BILL_5-#tmptablename1.MH_BILL_5,  
     #tmptablename2.MH_BILL_1=#tmptablename2.MH_BILL_1-#tmptablename1.MH_BILL_1,  
     #tmptablename2.MH_True_Coin_In=#tmptablename2.MH_True_Coin_In-#tmptablename1.MH_True_Coin_In,  
     #tmptablename2.MH_True_Coin_Out=#tmptablename2.MH_True_Coin_Out-#tmptablename1.MH_True_Coin_Out,  
     #tmptablename2.MH_Coins_In=#tmptablename2.MH_Coins_In-#tmptablename1.MH_Coins_In,  
     #tmptablename2.MH_Coins_Out=#tmptablename2.MH_Coins_Out-#tmptablename1.MH_Coins_Out,  
     #tmptablename2.MH_Coin_Drop=#tmptablename2.MH_Coin_Drop-#tmptablename1.MH_Coin_Drop,  
     #tmptablename2.MH_Cancelled_Credits=#tmptablename2.MH_Cancelled_Credits-#tmptablename1.MH_Cancelled_Credits,  
     #tmptablename2.MH_HandPay=#tmptablename2.MH_HandPay-#tmptablename1.MH_HandPay,  
     #tmptablename2.MH_Ticket_Inserted_Value=#tmptablename2.MH_Ticket_Inserted_Value-#tmptablename1.MH_Ticket_Inserted_Value,  
     #tmptablename2.MH_Ticket_Printed_Value=#tmptablename2.MH_Ticket_Printed_Value-#tmptablename1.MH_Ticket_Printed_Value,  
     #tmptablename2.MH_Ticket_Inserted_Qty=#tmptablename2.MH_Ticket_Inserted_Qty-#tmptablename1.MH_Ticket_Inserted_Qty,  
     #tmptablename2.MH_Ticket_Printed_Qty=#tmptablename2.MH_Ticket_Printed_Qty-#tmptablename1.MH_Ticket_Printed_Qty  
--     #tmptablename2.MH_DateTime=getDate()  
     FROM #tmptablename1  
   
     SET @installationID=(Select Installation_ID from VTP where VTP_ID=@ID)  
     SET @installationPricePerPlay=(Select Installation_Price_Per_Play FROM Installation WHERE Installation_ID = @installationID)  
     SET @readID=(Select Read_ID FROM [Read] WHERE Installation_ID = @installationID And Read_Date=@vtpDate)  
  
     --Getting Latest Read Record       
     select top 1 * into #tmptablenameREAD from Meter_History   
     Where MH_linkreference=@readID   
     and MH_Process = 'READ' and MH_Type='P'  
     Order by MH_ID Desc  
    
--     Alter table #tmptablenameREAD drop column MH_ID  
  
  
     Update #tmptablenameREAD SET #tmptablenameREAD.MH_BILL_100=#tmptablenameREAD.MH_BILL_100-#tmptablename2.MH_BILL_100,  
     #tmptablenameREAD.MH_BILL_50=#tmptablenameREAD.MH_BILL_50-#tmptablename2.MH_BILL_50,  
     #tmptablenameREAD.MH_BILL_20=#tmptablenameREAD.MH_BILL_20-#tmptablename2.MH_BILL_20,  
     #tmptablenameREAD.MH_BILL_10=#tmptablenameREAD.MH_BILL_10-#tmptablename2.MH_BILL_10,  
     #tmptablenameREAD.MH_BILL_5=#tmptablenameREAD.MH_BILL_5-#tmptablename2.MH_BILL_5,  
     #tmptablenameREAD.MH_BILL_1=#tmptablenameREAD.MH_BILL_1-#tmptablename2.MH_BILL_1,  
     #tmptablenameREAD.MH_True_Coin_In=#tmptablenameREAD.MH_True_Coin_In-#tmptablename2.MH_True_Coin_In,  
     #tmptablenameREAD.MH_True_Coin_Out=#tmptablenameREAD.MH_True_Coin_Out-#tmptablename2.MH_True_Coin_Out,  
     #tmptablenameREAD.MH_Coins_In=#tmptablenameREAD.MH_Coins_In-#tmptablename2.MH_Coins_In,  
     #tmptablenameREAD.MH_Coins_Out=#tmptablenameREAD.MH_Coins_Out-#tmptablename2.MH_Coins_Out,  
     #tmptablenameREAD.MH_Coin_Drop=#tmptablenameREAD.MH_Coin_Drop-#tmptablename2.MH_Coin_Drop,  
     #tmptablenameREAD.MH_Cancelled_Credits=#tmptablenameREAD.MH_Cancelled_Credits-#tmptablename2.MH_Cancelled_Credits,  
     #tmptablenameREAD.MH_HandPay=#tmptablenameREAD.MH_HandPay-#tmptablename2.MH_HandPay,  
     #tmptablenameREAD.MH_Ticket_Inserted_Value=#tmptablenameREAD.MH_Ticket_Inserted_Value-#tmptablename2.MH_Ticket_Inserted_Value,  
     #tmptablenameREAD.MH_Ticket_Printed_Value=#tmptablenameREAD.MH_Ticket_Printed_Value-#tmptablename2.MH_Ticket_Printed_Value,  
     #tmptablenameREAD.MH_Ticket_Inserted_Qty=#tmptablenameREAD.MH_Ticket_Inserted_Qty-#tmptablename2.MH_Ticket_Inserted_Qty,  
     #tmptablenameREAD.MH_Ticket_Printed_Qty=#tmptablenameREAD.MH_Ticket_Printed_Qty-#tmptablename2.MH_Ticket_Printed_Qty  
--     #tmptablenameREAD.MH_DateTime=getDate()  
     FROM #tmptablename2  
  
--     Insert into Meter_History select * from #tmptablenameREAD  
  
     Insert into Meter_History  
     (  
     MH_Process,  
     MH_Type,  
     MH_LinkReference,  
     MH_Reference,  
     MH_Installation_No,  
     MH_Datetime,  
     MH_PAYOUT_FLOAT_TOKEN,  
     MH_PAYOUT_FLOAT_10P,  
     MH_PAYOUT_FLOAT_20P,  
     MH_PAYOUT_FLOAT_50P,  
     MH_PAYOUT_FLOAT_100P,
     MH_CASH_IN_1P,  
     MH_CASH_IN_2P,  
     MH_CASH_IN_5P,  
     MH_CASH_IN_10P,  
     MH_CASH_IN_20P,  
     MH_CASH_IN_50P,  
     MH_CASH_IN_100P,  
     MH_CASH_IN_200P,  
     MH_CASH_IN_500P,  
     MH_CASH_IN_1000P,  
     MH_CASH_IN_2000P,  
     MH_CASH_IN_5000P,  
     MH_CASH_IN_10000P,  
     MH_CASH_IN_20000P,  
     MH_CASH_IN_50000P,  
     MH_CASH_IN_100000P,  
     MH_TOKEN_IN_5P,  
     MH_TOKEN_IN_10P,  
     MH_TOKEN_IN_20P,  
     MH_TOKEN_IN_50P,  
     MH_TOKEN_IN_100P,  
     MH_TOKEN_IN_200P,  
     MH_TOKEN_IN_500P,  
     MH_TOKEN_IN_1000P,  
     MH_CASH_OUT_1P,
     MH_CASH_OUT_2P,  
     MH_CASH_OUT_5P,  
     MH_CASH_OUT_10P,  
     MH_CASH_OUT_20P,  
     MH_CASH_OUT_50P,  
     MH_CASH_OUT_100P,  
     MH_CASH_OUT_200P,  
     MH_CASH_OUT_500P,  
     MH_CASH_OUT_1000P,  
     MH_CASH_OUT_2000P,  
     MH_CASH_OUT_5000P,  
     MH_CASH_OUT_10000P,  
     MH_CASH_OUT_20000P,  
     MH_CASH_OUT_50000P,  
     MH_CASH_OUT_100000P,  
     MH_TOKEN_OUT_5P,  
     MH_TOKEN_OUT_10P,  
     MH_TOKEN_OUT_20P,  
     MH_TOKEN_OUT_50P,  
     MH_TOKEN_OUT_100P,  
     MH_TOKEN_OUT_200P,  
     MH_TOKEN_OUT_500P,  
     MH_TOKEN_OUT_1000P,  
     MH_CASH_REFILL_5P,  
     MH_CASH_REFILL_10P,  
     MH_CASH_REFILL_20P,  
     MH_CASH_REFILL_50P,  
     MH_CASH_REFILL_100P,  
     MH_CASH_REFILL_200P,  
     MH_CASH_REFILL_500P,  
     MH_CASH_REFILL_1000P,  
     MH_CASH_REFILL_2000P,  
     MH_CASH_REFILL_5000P,  
     MH_CASH_REFILL_10000P,  
     MH_CASH_REFILL_20000P,  
     MH_CASH_REFILL_50000P,  
     MH_CASH_REFILL_100000P,  
     MH_TOKEN_REFILL_5P,  
     MH_TOKEN_REFILL_10P,  
     MH_TOKEN_REFILL_20P,  
     MH_TOKEN_REFILL_50P,  
     MH_TOKEN_REFILL_100P,  
     MH_TOKEN_REFILL_200P,  
     MH_TOKEN_REFILL_500P,  
     MH_TOKEN_REFILL_1000P,  
     MH_COINS_IN,  
     MH_COINS_OUT,  
     MH_COIN_DROP,  
     MH_HANDPAY,  
     MH_EXTERNAL_CREDIT,  
     MH_GAMES_BET,  
     MH_GAMES_WON,  
     MH_NOTES,  
     MH_VTP,  
     MH_CANCELLED_CREDITS,  
     MH_GAMES_LOST,  
     MH_GAMES_SINCE_POWER_UP,  
     MH_TRUE_COIN_IN,  
     MH_TRUE_COIN_OUT,  
     MH_CURRENT_CREDITS,  
     MH_JACKPOT,  
     MH_BILL_1,  
     MH_BILL_2,  
     MH_BILL_5,  
     MH_BILL_10,  
     MH_BILL_20,  
     MH_BILL_50,  
     MH_BILL_100,  
     MH_BILL_250,  
     MH_BILL_10000,  
     MH_BILL_20000,  
     MH_BILL_50000,  
     MH_BILL_100000,  
     MH_TICKET_PRINTED_QTY,  
     MH_TICKET_PRINTED_VALUE,  
     MH_TICKET_INSERTED_QTY,  
     MH_TICKET_INSERTED_VALUE  
     )   
  
     Select    
       
     MH_Process,  
     MH_Type,  
     MH_LinkReference,  
     MH_Reference,  
     MH_Installation_No,  
--     MH_Datetime,  
     convert(varchar(12),getdate(),106) + ' ' +  convert(varchar(8),getdate(),114),  
     MH_PAYOUT_FLOAT_TOKEN,  
     MH_PAYOUT_FLOAT_10P,  
     MH_PAYOUT_FLOAT_20P,  
     MH_PAYOUT_FLOAT_50P,  
     MH_PAYOUT_FLOAT_100P,
     MH_CASH_IN_1P,   
     MH_CASH_IN_2P,  
     MH_CASH_IN_5P,  
     MH_CASH_IN_10P,  
     MH_CASH_IN_20P,  
     MH_CASH_IN_50P,  
     MH_CASH_IN_100P,  
     MH_CASH_IN_200P,  
     MH_CASH_IN_500P,  
     MH_CASH_IN_1000P,  
     MH_CASH_IN_2000P,  
     MH_CASH_IN_5000P,  
     MH_CASH_IN_10000P,  
     MH_CASH_IN_20000P,  
     MH_CASH_IN_50000P,  
     MH_CASH_IN_100000P,  
     MH_TOKEN_IN_5P,  
     MH_TOKEN_IN_10P,  
     MH_TOKEN_IN_20P,  
     MH_TOKEN_IN_50P,  
     MH_TOKEN_IN_100P,  
     MH_TOKEN_IN_200P,  
     MH_TOKEN_IN_500P,  
     MH_TOKEN_IN_1000P,  
     MH_CASH_OUT_1P, 
     MH_CASH_OUT_2P,  
     MH_CASH_OUT_5P,  
     MH_CASH_OUT_10P,  
     MH_CASH_OUT_20P,  
     MH_CASH_OUT_50P,  
     MH_CASH_OUT_100P,  
     MH_CASH_OUT_200P,  
     MH_CASH_OUT_500P,  
     MH_CASH_OUT_1000P,  
     MH_CASH_OUT_2000P,  
     MH_CASH_OUT_5000P,  
     MH_CASH_OUT_10000P,  
     MH_CASH_OUT_20000P,  
     MH_CASH_OUT_50000P,  
     MH_CASH_OUT_100000P,  
     MH_TOKEN_OUT_5P,  
     MH_TOKEN_OUT_10P,  
     MH_TOKEN_OUT_20P,  
     MH_TOKEN_OUT_50P,  
     MH_TOKEN_OUT_100P,  
     MH_TOKEN_OUT_200P,  
     MH_TOKEN_OUT_500P,  
     MH_TOKEN_OUT_1000P,  
     MH_CASH_REFILL_5P,  
     MH_CASH_REFILL_10P,  
     MH_CASH_REFILL_20P,  
     MH_CASH_REFILL_50P,  
     MH_CASH_REFILL_100P,  
     MH_CASH_REFILL_200P,  
     MH_CASH_REFILL_500P,  
     MH_CASH_REFILL_1000P,  
     MH_CASH_REFILL_2000P,  
     MH_CASH_REFILL_5000P,  
     MH_CASH_REFILL_10000P,  
     MH_CASH_REFILL_20000P,  
     MH_CASH_REFILL_50000P,  
     MH_CASH_REFILL_100000P,  
     MH_TOKEN_REFILL_5P,  
     MH_TOKEN_REFILL_10P,  
     MH_TOKEN_REFILL_20P,  
     MH_TOKEN_REFILL_50P,  
     MH_TOKEN_REFILL_100P,  
     MH_TOKEN_REFILL_200P,  
     MH_TOKEN_REFILL_500P,  
     MH_TOKEN_REFILL_1000P,  
     MH_COINS_IN,  
     MH_COINS_OUT,  
     MH_COIN_DROP,  
     MH_HANDPAY,  
     MH_EXTERNAL_CREDIT,  
     MH_GAMES_BET,  
     MH_GAMES_WON,  
     MH_NOTES,  
     MH_VTP,  
     MH_CANCELLED_CREDITS,  
     MH_GAMES_LOST,  
     MH_GAMES_SINCE_POWER_UP,  
     MH_TRUE_COIN_IN,  
     MH_TRUE_COIN_OUT,  
     MH_CURRENT_CREDITS,  
     MH_JACKPOT,  
     MH_BILL_1,  
     MH_BILL_2,  
     MH_BILL_5,  
     MH_BILL_10,  
     MH_BILL_20,  
     MH_BILL_50,  
     MH_BILL_100,  
     MH_BILL_250,  
     MH_BILL_10000,  
     MH_BILL_20000,  
     MH_BILL_50000,  
     MH_BILL_100000,  
     MH_TICKET_PRINTED_QTY,  
     MH_TICKET_PRINTED_VALUE,  
     MH_TICKET_INSERTED_QTY,  
     MH_TICKET_INSERTED_VALUE  
        
     from #tmptablenameREAD  
  
  
     Drop table #tmptablename1  
     Drop table #tmptablename2  
     Drop table #tmptablenameREAD  
  
     EXEC usp_UpdateLastMeterDeltas @installationID,'READ',@readID  
  
    End   
  End  
 Else If(@Rtype='Daily')  
  Begin  
   SET @installationID=(Select Installation_ID from [READ] where READ_ID=@ID)  
  
   EXEC usp_UpdateLastMeterDeltas @installationID,'READ',@ID  
  
  
  End  
 Else If(@Rtype='Collection')  
  Begin  
   SET @installationID=(Select Installation_ID from Collection where Collection_ID=@ID)  
  
  
   EXEC usp_UpdateLastMeterDeltas @installationID,'COLL',@ID  
  
   exec RecalculateCollectionCalcs @ID  
  
  End  
END  
Begin  
  
  --update corresponding row in Exception for the spike fix  
  
 SET @ExceptionDetail=' M100 from '+@P_MH_BILL_100+' To '+Cast(@Bill100Meter as Varchar(100))+',M50 from '+@P_MH_BILL_50+  
 ' To '+Cast(@Bill50Meter as Varchar(100))+',M20 from '+@P_MH_BILL_20+' To '+Cast(@Bill20Meter as Varchar(100))+',M10 from '+@P_MH_BILL_10+  
' To '+Cast(@Bill10Meter as Varchar(100))+',M5 from '+@P_MH_BILL_5+' To '+Cast(@Bill5Meter as Varchar(100))+',M1 from '+Cast(@P_MH_BILL_1 as Varchar(100))+  
' To '+Cast(@Bill1Meter as Varchar(100))+',TCoinIn from '+@P_TrueCoinIn+' To '+Cast(@TrueCoinIn as Varchar(100))+',TCoinOut from '+@P_TrueCoinOut+  
' To '+Cast(@TrueCoinOut as Varchar(100))+',CoinIn from '+@P_CoinIn+' To '+Cast(@CoinIn as Varchar(100))+',CoinOut from '+@P_CoinOut+  
' To '+Cast(@CoinOut as Varchar(100))+',CoinDrop from '+@P_Drop+' To '+Cast(@Drop as Varchar(100))+',CancelledCredits from '+@P_CancelledCredits+  
' To '+Cast(@CancelledCredits as Varchar(100))+',HandPay from '+@P_HandPaidCancelledCredits+  
' To '+Cast(@HandPaidCancelledCredits as Varchar(100))+',TicketInsertedValue from '+@P_CashableTicketsInValue+  
' To '+Cast(@CashableTicketsInValue as Varchar(100))+',TicketPrintedValue from '+@P_CashableTicketsOutValue+  
' To '+Cast(@CashableTicketsOutValue as Varchar(100))+',TicketInsertedQty from '+@P_CashableTicketsInNo+  
' To '+Cast(@CashableTicketsInNo as Varchar(100))+',TicketPrintedQty from '+@P_CashableTicketsOutNo+  
' To '+Cast(@CashableTicketsOutNo as Varchar(100))  
   
 SET @FullExceptionDetail='Fixed '+@ExceptionDetail  
 SET @varID=cast(@ID as Varchar(50))  
 SET @varInstallationID=cast(@installationId as VarChar(50))  
 update Exception Set Exception_Details = @FullExceptionDetail where Exception_Reference = @VarID  
 --Now, if we have fixed a record without Spikes, then it will not be available in Exception  
 -- So, we have to insert a new record with status fixed  
 if(@@rowcount=0)  
  BEGIN  
--check the process and assign exceptionType accordingly  
  
   if @Rtype='VTP'  
    set @ExceptionType = 200  
   else if @Rtype='Daily'  
    set @ExceptionType = 201  
   else if @Rtype='Collection'  
    set @ExceptionType = 202  
     
   exec usp_InsertException @varInstallationID,@ExceptionType,@FullExceptionDetail,@varID,@ExceptionUser  
  END  
 --At this point there might still be spike so check again  
   
   
 if(@RType='Daily')   exec usp_CheckNUpdateSpikes 'READ',@varID,@HourNo  
 Else if(@RType='Collection')   exec usp_CheckNUpdateSpikes 'COLL',@varID,@HourNo  
 Else  exec usp_CheckNUpdateSpikes 'VTP',@varID,@HourNo  
End  

GO

