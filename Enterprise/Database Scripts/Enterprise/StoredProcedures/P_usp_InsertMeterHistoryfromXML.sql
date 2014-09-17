USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_InsertMeterHistoryfromXML]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_InsertMeterHistoryfromXML]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- usp_InsertMeterHistoryfromXML    
-- -----------------------------------------------------------------------------------------------------------------------------------    
--    
-- returns inserts into meter_history table from XML    
--     
-- -----------------------------------------------------------------------------------------------------------------------------------    
-- Revision History     
--     
-- 11/05/2007 N.Siva  Created    
-- 07/06/2007   N.Siva  sp_xml_removedocument is not called in all flow paths    
--       If record is already processed, return -1 to the caller    
-- 19/06/07     Poorna  Changes as per standard regional date/time changes    
-- 02/11/07     N.Siva  Added XML encoding     
--       collection date,time should be taken from batch table    
-- 22/10/2007   Poorna K.   Added handling of RAMRESET/ROLLOVER records    
-- 05/12/2007   Siva  bug fix some MH  are not processed  due to time lag    
--       check for installation_start_date only if required    
-- 22/05/2008 Vineetha Get the HQ_Installatio_no as the installation no    
-- 17/06/2008 Poorna K.   Changed MH-VTP link to MH to Hourly_Statistics table by takeing teh LinkReference from HS    
-- 24/07/2008   Siva        Update MH if already exists, insert otherwise    
-- 29/07/2008   Siva        bug fix    
-- 09/03/2010	Sudarsan S	EFT/Non Cahsable Tkt/Mystery
-- 15/06/2010	Sudarsan S	For Bills 200 and 500
-- ===================================================================================================================================    
    
CREATE PROCEDURE [dbo].[usp_InsertMeterHistoryfromXML]       
 @doc varchar(max),      
@IsSuccess int output      
AS      
      
set dateformat dmy    
-- idoc is the document handle of the internal representation of an XML document which is created by calling sp_xml_preparedocument.    
declare @idoc int    
declare @MHID as int    
declare @MHProcess varchar(10)    
declare @MHType varchar(1)    
declare @MHLinkReference int    
declare @VTPDate varchar(20)    
declare @VTPHour varchar(2)    
declare @ReadDate varchar(20)    
declare @CollectionDate varchar(50)    
declare @CollectionTime varchar(50)    
declare @InstallationNo int    
declare @error  int    
    
set @IsSuccess=0      
set @error = 0    
    
--add the encoding version as we need to process special characters like pound symbol    
SET @doc='<?xml version="1.0" encoding="ISO-8859-1"?>' + @doc    
    
--Create an internal representation of the XML document.    
    
 EXEC sp_xml_preparedocument @idoc OUTPUT, @doc    
    
--1.Check to see what type of MH record - Execute a SELECT statement that uses the OPENXML rowset provider.    
    
SELECT  @MHProcess=MH_Process,     
  @MHType=MH_Type,    
  @VTPDate=Date,    
  @ReadDate=CONVERT(VARCHAR, Read_Date, 106),
  @VTPHour=[Hour],    
  @CollectionDate=CONVERT(VARCHAR, Collection_Date, 106),
  @CollectionTime=CONVERT(VARCHAR, Collection_Date, 108),
  @InstallationNo=[HQ_INSTALLATION_NO]         
 FROM    OPENXML (@idoc, '/Installation',2)    
   WITH     
   (    
     MH_Process varchar(10) './Meter_History/MH_Process',    
     [HQ_INSTALLATION_NO] int './Installation/HQ_INSTALLATION_NO',    
     MH_Type varchar(11) './Meter_History/MH_Type',    
     [Date]  varchar(20) './Meter_History/Reference_Link_Details/Date',    
     [Hour] varchar(2) './Meter_History/Reference_Link_Details/Hour',    
     Read_Date DATETIME './Meter_History/Reference_Link_Details/Read_Date',      
     Collection_Date DATETIME './Meter_History/Reference_Link_Details/Collection_Batch_Date'
--     Collection_Time varchar(50) './Meter_History/Reference_Link_Details/Collection_Batch_Time'    
    )   
SELECT  @MHProcess as MHProcess,@MHType as MHType,@VTPDate as VTPDate,@ReadDate as ReadDate,    
@VTPHour as VTPHour,@CollectionDate as CollectionDate,@CollectionTime as CollectionTime,@InstallationNo as InstallationNo     
   EXEC sp_xml_removedocument @idoc    
    
SET @CollectionDate=REPLACE(REPLACE(REPLACE(@CollectionDate, CHAR(10), ''), CHAR(13), ''), CHAR(9), '')    
   
--2. Get the installation No. Instllation No is different in enterprise than exchange. We need to know to which installation no this MH records belongs to.    
 --Got Installation number from exchange in @InstallationNo from above XML statement    
    
 IF @MHProcess = '__RAMRESET'  
 BEGIN  
  SET @MHID = 1  
  GOTO err_Handler  
 END  
  
--Check the record is RESET or ROLLOVER, if so jump to InsertToMH to insert the record into MH    
 If @MHProcess = 'RAMRESET' or  @MHProcess = 'ROLLOVER'   or @MHProcess = 'RAMCLEAR'
 Begin    
  GoTo InsertToMH    
 End    
--3.Check if the link records have already been processed - We need this check to get the link reference before inserting into MH.    
 If @MHProcess = 'VTP'    
     --select @MHLinkReference= VTP_ID from  VTP where Convert(varchar,cast(date as datetime),106)=@VTPDate and  Installation_Id = @InstallationNo    
   select @MHLinkReference = HS_ID from Hourly_Statistics where HS_Type='GAMES_BET' and HS_Installation_No=@InstallationNo 
and Convert(varchar,cast(HS_Date as datetime) ,106) =Convert(varchar,cast(@VTPDate as datetime),106)  
 If @MHProcess = 'COLL'    
 begin    
  --select @MHLinkReference = Collection_ID from  Collection where Convert(varchar,cast(collection_date as datetime),106) =  @CollectionDate and Collection_Time =  @CollectionTime and  Installation_Id = @InstallationNo    
  select @MHLinkReference = Collection_ID from      
  Collection     
  join batch on Collection.Batch_Id=Batch.batch_id where    
  Convert(varchar,cast(Batch.Batch_Date  as datetime),106)= Convert(varchar,cast(@CollectionDate as datetime),106)     
  and Convert(varchar,cast(Batch.Batch_Time as datetime),108)= Convert(varchar,cast( @CollectionTime as datetime),108) and  Installation_Id = @InstallationNo    
 end    
 If @MHProcess = 'READ'    
  select @MHLinkReference = Read_Id from  [Read] where Convert(varchar,cast(Read_Date as datetime),106) = 
Convert(varchar,cast(@ReadDate as datetime),106) and  Installation_Id = @InstallationNo    
 IF @MHProcess = 'INST'    
   set @MHLinkReference = @InstallationNo    
    
--4.IF the link record had not yet come, exit out of proc.    
    
 If coalesce(@MHLinkReference,0) =0    and  @CollectionDate is not null and @MHProcess <> 'COLL'
 begin    
  Set @IsSuccess = -1 --No Link ref found    
  RETURN     
 end    
    
InsertToMH:    
--5. Parse this XML and insert into Meter_History table    
    
 EXEC sp_xml_preparedocument @idoc OUTPUT, @doc    
    
 SELECT    * INTO #tmpMETERHISTORY     
 FROM       OPENXML (@idoc, 'Installation/Meter_History',2)    
   WITH Meter_history     
    
    set @error = @@ERROR    
    if @error <> 0 goto err_Handler    
    
    EXEC sp_xml_removedocument @idoc    
    
--6. Check if we already have records in MH table. Need to avoid duplicates. IF found exit out of proc.    

	If coalesce(@MHLinkReference,0) =0    and  @CollectionDate is not null
	begin
	   select @MHID = MH_ID from meter_history where mh_linkreference=@MHLinkReference and MH_Type=@MHType and MH_Process=@MHProcess    
	 and    
	 (    
	  @MHProcess = 'VTP'     
	  and (mh_reference=@VTPHour)    
	      
	 OR    
	  (    
	   @MHProcess <> 'VTP'    
	   and 1=1    
	  )    
	 )
	end 
	else
	begin
	set @MHID = 0
	end
	    
    IF ( COALESCE(@MHID,0) = 0)    
  BEGIN    
     INSERT INTO METER_HISTORY SELECT * from #tmpMETERHISTORY    
     Set @MHID = SCOPE_IDENTITY()    
    
            set @error = @@ERROR    
            if @error <> 0 goto err_Handler    
     END    
 ELSE    
     BEGIN    
   UPDATE METER_HISTORY     
   SET     
   MH_PAYOUT_FLOAT_TOKEN  = #tmpMETERHISTORY.MH_PAYOUT_FLOAT_TOKEN,    
   MH_PAYOUT_FLOAT_10P   = #tmpMETERHISTORY.MH_PAYOUT_FLOAT_10P,    
   MH_PAYOUT_FLOAT_20P   = #tmpMETERHISTORY.MH_PAYOUT_FLOAT_20P,    
   MH_PAYOUT_FLOAT_50P   = #tmpMETERHISTORY.MH_PAYOUT_FLOAT_50P,    
   MH_PAYOUT_FLOAT_100P  = #tmpMETERHISTORY.MH_PAYOUT_FLOAT_100P,  
    MH_CASH_IN_1P    = #tmpMETERHISTORY.MH_CASH_IN_1P,   
   MH_CASH_IN_2P    = #tmpMETERHISTORY.MH_CASH_IN_2P,    
   MH_CASH_IN_5P    = #tmpMETERHISTORY.MH_CASH_IN_5P,    
   MH_CASH_IN_10P    = #tmpMETERHISTORY.MH_CASH_IN_10P,    
   MH_CASH_IN_20P    = #tmpMETERHISTORY.MH_CASH_IN_20P,    
   MH_CASH_IN_50P    = #tmpMETERHISTORY.MH_CASH_IN_50P,    
   MH_CASH_IN_100P    = #tmpMETERHISTORY.MH_CASH_IN_100P,    
   MH_CASH_IN_200P    = #tmpMETERHISTORY.MH_CASH_IN_200P,    
   MH_CASH_IN_500P    = #tmpMETERHISTORY.MH_CASH_IN_500P,    
   MH_CASH_IN_1000P   = #tmpMETERHISTORY.MH_CASH_IN_1000P,    
   MH_CASH_IN_2000P   = #tmpMETERHISTORY.MH_CASH_IN_2000P,    
   MH_CASH_IN_5000P   = #tmpMETERHISTORY.MH_CASH_IN_5000P,    
   MH_CASH_IN_10000P   = #tmpMETERHISTORY.MH_CASH_IN_10000P,    
   MH_CASH_IN_20000P   = #tmpMETERHISTORY.MH_CASH_IN_20000P,    
   MH_CASH_IN_50000P   = #tmpMETERHISTORY.MH_CASH_IN_50000P,    
   MH_CASH_IN_100000P   = #tmpMETERHISTORY.MH_CASH_IN_100000P,    
   MH_TOKEN_IN_5P    = #tmpMETERHISTORY.MH_TOKEN_IN_5P,    
   MH_TOKEN_IN_10P    = #tmpMETERHISTORY.MH_TOKEN_IN_10P,    
   MH_TOKEN_IN_20P    = #tmpMETERHISTORY.MH_TOKEN_IN_20P,    
   MH_TOKEN_IN_50P    = #tmpMETERHISTORY.MH_TOKEN_IN_50P,    
   MH_TOKEN_IN_100P   = #tmpMETERHISTORY.MH_TOKEN_IN_100P,    
   MH_TOKEN_IN_200P   = #tmpMETERHISTORY.MH_TOKEN_IN_200P,    
   MH_TOKEN_IN_500P   = #tmpMETERHISTORY.MH_TOKEN_IN_500P,    
   MH_TOKEN_IN_1000P   = #tmpMETERHISTORY.MH_TOKEN_IN_1000P, 
   MH_CASH_OUT_1P    = #tmpMETERHISTORY.MH_CASH_OUT_1P,     
   MH_CASH_OUT_2P    = #tmpMETERHISTORY.MH_CASH_OUT_2P,    
   MH_CASH_OUT_5P    = #tmpMETERHISTORY.MH_CASH_OUT_5P,    
   MH_CASH_OUT_10P    = #tmpMETERHISTORY.MH_CASH_OUT_10P,    
   MH_CASH_OUT_20P    = #tmpMETERHISTORY.MH_CASH_OUT_20P,    
   MH_CASH_OUT_50P    = #tmpMETERHISTORY.MH_CASH_OUT_50P,    
   MH_CASH_OUT_100P   = #tmpMETERHISTORY.MH_CASH_OUT_100P,    
   MH_CASH_OUT_200P   = #tmpMETERHISTORY.MH_CASH_OUT_200P,    
   MH_CASH_OUT_500P   = #tmpMETERHISTORY.MH_CASH_OUT_500P,    
   MH_CASH_OUT_1000P   = #tmpMETERHISTORY.MH_CASH_OUT_1000P,    
   MH_CASH_OUT_2000P   = #tmpMETERHISTORY.MH_CASH_OUT_2000P,    
   MH_CASH_OUT_5000P   = #tmpMETERHISTORY.MH_CASH_OUT_5000P,    
   MH_CASH_OUT_10000P   = #tmpMETERHISTORY.MH_CASH_OUT_10000P,    
   MH_CASH_OUT_20000P   = #tmpMETERHISTORY.MH_CASH_OUT_20000P,    
   MH_CASH_OUT_50000P   = #tmpMETERHISTORY.MH_CASH_OUT_50000P,    
   MH_CASH_OUT_100000P   = #tmpMETERHISTORY.MH_CASH_OUT_100000P,    
   MH_TOKEN_OUT_5P    = #tmpMETERHISTORY.MH_TOKEN_OUT_5P,    
   MH_TOKEN_OUT_10P   = #tmpMETERHISTORY.MH_TOKEN_OUT_10P,    
   MH_TOKEN_OUT_20P   = #tmpMETERHISTORY.MH_TOKEN_OUT_20P,    
   MH_TOKEN_OUT_50P   = #tmpMETERHISTORY.MH_TOKEN_OUT_50P,    
   MH_TOKEN_OUT_100P   = #tmpMETERHISTORY.MH_TOKEN_OUT_100P,    
   MH_TOKEN_OUT_200P   = #tmpMETERHISTORY.MH_TOKEN_OUT_200P,    
   MH_TOKEN_OUT_500P   = #tmpMETERHISTORY.MH_TOKEN_OUT_500P,    
   MH_TOKEN_OUT_1000P   = #tmpMETERHISTORY.MH_TOKEN_OUT_1000P,    
   MH_CASH_REFILL_5P   = #tmpMETERHISTORY.MH_CASH_REFILL_5P,    
   MH_CASH_REFILL_10P   = #tmpMETERHISTORY.MH_CASH_REFILL_10P,    
   MH_CASH_REFILL_20P   = #tmpMETERHISTORY.MH_CASH_REFILL_20P,    
   MH_CASH_REFILL_50P   = #tmpMETERHISTORY.MH_CASH_REFILL_50P,    
   MH_CASH_REFILL_100P   = #tmpMETERHISTORY.MH_CASH_REFILL_100P,    
   MH_CASH_REFILL_200P   = #tmpMETERHISTORY.MH_CASH_REFILL_200P,    
   MH_CASH_REFILL_500P   = #tmpMETERHISTORY.MH_CASH_REFILL_500P,    
   MH_CASH_REFILL_1000P  = #tmpMETERHISTORY.MH_CASH_REFILL_1000P,    
   MH_CASH_REFILL_2000P  = #tmpMETERHISTORY.MH_CASH_REFILL_2000P,    
   MH_CASH_REFILL_5000P  = #tmpMETERHISTORY.MH_CASH_REFILL_5000P,    
   MH_CASH_REFILL_10000P  = #tmpMETERHISTORY.MH_CASH_REFILL_10000P,    
   MH_CASH_REFILL_20000P  = #tmpMETERHISTORY.MH_CASH_REFILL_20000P,    
   MH_CASH_REFILL_50000P  = #tmpMETERHISTORY.MH_CASH_REFILL_50000P,    
   MH_CASH_REFILL_100000P  = #tmpMETERHISTORY.MH_CASH_REFILL_100000P,    
   MH_TOKEN_REFILL_5P   = #tmpMETERHISTORY.MH_TOKEN_REFILL_5P,    
   MH_TOKEN_REFILL_10P   = #tmpMETERHISTORY.MH_TOKEN_REFILL_10P,    
   MH_TOKEN_REFILL_20P   = #tmpMETERHISTORY.MH_TOKEN_REFILL_20P,    
   MH_TOKEN_REFILL_50P   = #tmpMETERHISTORY.MH_TOKEN_REFILL_50P,    
   MH_TOKEN_REFILL_100P  = #tmpMETERHISTORY.MH_TOKEN_REFILL_100P,    
MH_TOKEN_REFILL_200P  = #tmpMETERHISTORY.MH_TOKEN_REFILL_200P,    
   MH_TOKEN_REFILL_500P  = #tmpMETERHISTORY.MH_TOKEN_REFILL_500P,    
   MH_TOKEN_REFILL_1000P  = #tmpMETERHISTORY.MH_TOKEN_REFILL_1000P,    
   MH_COINS_IN     = #tmpMETERHISTORY.MH_COINS_IN,    
MH_COINS_OUT    = #tmpMETERHISTORY.MH_COINS_OUT,    
   MH_COIN_DROP    = #tmpMETERHISTORY.MH_COIN_DROP,    
   MH_HANDPAY     = #tmpMETERHISTORY.MH_HANDPAY,    
   MH_EXTERNAL_CREDIT   = #tmpMETERHISTORY.MH_EXTERNAL_CREDIT,    
   MH_GAMES_BET    = #tmpMETERHISTORY.MH_GAMES_BET,    
   MH_GAMES_WON    = #tmpMETERHISTORY.MH_GAMES_WON,    
   MH_NOTES     = #tmpMETERHISTORY.MH_NOTES,    
   MH_VTP      = #tmpMETERHISTORY.MH_VTP,    
   MH_CANCELLED_CREDITS  = #tmpMETERHISTORY.MH_CANCELLED_CREDITS,    
   MH_GAMES_LOST    = #tmpMETERHISTORY.MH_GAMES_LOST,    
   MH_GAMES_SINCE_POWER_UP  = #tmpMETERHISTORY.MH_GAMES_SINCE_POWER_UP,    
   MH_TRUE_COIN_IN    = #tmpMETERHISTORY.MH_TRUE_COIN_IN,    
   MH_TRUE_COIN_OUT   = #tmpMETERHISTORY.MH_TRUE_COIN_OUT,    
   MH_CURRENT_CREDITS   = #tmpMETERHISTORY.MH_CURRENT_CREDITS,    
   MH_JACKPOT     = #tmpMETERHISTORY.MH_JACKPOT,    
   MH_BILL_1     = #tmpMETERHISTORY.MH_BILL_1,    
   MH_BILL_2     = #tmpMETERHISTORY.MH_BILL_2,    
   MH_BILL_5     = #tmpMETERHISTORY.MH_BILL_5,    
   MH_BILL_10     = #tmpMETERHISTORY.MH_BILL_10,    
   MH_BILL_20     = #tmpMETERHISTORY.MH_BILL_20,    
   MH_BILL_50     = #tmpMETERHISTORY.MH_BILL_50,    
   MH_BILL_100     = #tmpMETERHISTORY.MH_BILL_100,    
   MH_BILL_250     = #tmpMETERHISTORY.MH_BILL_250,    
   MH_BILL_10000    = #tmpMETERHISTORY.MH_BILL_10000,    
   MH_BILL_20000    = #tmpMETERHISTORY.MH_BILL_20000,    
   MH_BILL_50000    = #tmpMETERHISTORY.MH_BILL_50000,    
   MH_BILL_100000    = #tmpMETERHISTORY.MH_BILL_100000,    
   MH_TICKET_PRINTED_QTY  = #tmpMETERHISTORY.MH_TICKET_PRINTED_QTY,    
   MH_TICKET_PRINTED_VALUE  = #tmpMETERHISTORY.MH_TICKET_PRINTED_VALUE,    
   MH_TICKET_INSERTED_QTY  = #tmpMETERHISTORY.MH_TICKET_INSERTED_QTY,    
   MH_TICKET_INSERTED_VALUE = #tmpMETERHISTORY.MH_TICKET_INSERTED_VALUE,    
   MH_Datetime     = #tmpMETERHISTORY.MH_Datetime,    
   MH_progressive_win_value = #tmpMETERHISTORY.MH_progressive_win_value,    
   MH_progressive_win_Handpay_value= #tmpMETERHISTORY.MH_progressive_win_Handpay_value,
	MH_Mystery_Machine_Paid = #tmpMETERHISTORY.MH_Mystery_Machine_Paid,
	MH_Mystery_Attendant_Paid = #tmpMETERHISTORY.MH_Mystery_Attendant_Paid,
	MH_TICKETS_PRINTED_NONCASHABLE_QTY = #tmpMETERHISTORY.MH_TICKETS_PRINTED_NONCASHABLE_QTY,
	MH_TICKETS_PRINTED_NONCASHABLE_VALUE = #tmpMETERHISTORY.MH_TICKETS_PRINTED_NONCASHABLE_VALUE,
	MH_TICKETS_INSERTED_NONCASHABLE_QTY = #tmpMETERHISTORY.MH_TICKETS_INSERTED_NONCASHABLE_QTY,
	MH_TICKETS_INSERTED_NONCASHABLE_VALUE = #tmpMETERHISTORY.MH_TICKETS_INSERTED_NONCASHABLE_VALUE,
	MH_Promo_Cashable_EFT_IN = #tmpMETERHISTORY.MH_Promo_Cashable_EFT_IN,
	MH_Promo_Cashable_EFT_OUT = #tmpMETERHISTORY.MH_Promo_Cashable_EFT_OUT,
	MH_NonCashable_EFT_IN = #tmpMETERHISTORY.MH_NonCashable_EFT_IN,
	MH_NonCashable_EFT_OUT = #tmpMETERHISTORY.MH_NonCashable_EFT_OUT,
	MH_Cashable_EFT_IN = #tmpMETERHISTORY.MH_Cashable_EFT_IN,
	MH_Cashable_EFT_OUT = #tmpMETERHISTORY.MH_Cashable_EFT_OUT,
   MH_BILL_200     = #tmpMETERHISTORY.MH_BILL_200,    
   MH_BILL_500     = #tmpMETERHISTORY.MH_BILL_500

   FROM #tmpMETERHISTORY     
            WHERE MH_ID=@MHID       
      
            set @error = @@ERROR    
            if @error <> 0 goto err_Handler         
     END    
    
    drop table #tmpMETERHISTORY    
    
    set @error =@@ERROR    
    if @error <> 0 goto err_Handler    
        
     
--7. Now we have to update this MH record to point to corresponding link record.    
    
 If @MHProcess = 'RAMRESET' or  @MHProcess = 'ROLLOVER'  or  @MHProcess = 'RAMCLEAR' 
 update METER_HISTORY set MH_LinkReference = @MHID,MH_Installation_No= @InstallationNo where MH_ID=@MHID    
 else    
 update METER_HISTORY set MH_LinkReference = @MHLinkReference,MH_Installation_No= @InstallationNo where MH_ID=@MHID    
    
        set @error = @@ERROR    
        if @error <> 0 goto err_Handler    
    
--8. Finally check for spikes and update exception table - only when MH record type is 'C' (we need both records to check exception    
    
    /* old code  for spike check - commeting as it is obselete now.    
 If @MHType='C'     
    BEGIN    
   exec usp_CheckNUpdateSpikes @MHProcess,@MHLinkReference,@VTPHour    
        set @error = @@ERROR      
    END*/    
    
--9. Return success/failure    
    
err_Handler:    
    
if @error = 0    
 Set @IsSuccess =@MHID     
else    
  Set @IsSuccess =0    
    
return @error -- log that as well @RETURN_VALUE    

GO

