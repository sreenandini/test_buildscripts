USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetMaintenanceSessionDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetMaintenanceSessionDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[rsp_GetMaintenanceSessionDetails](@SessionID INT, @SiteID INT)
AS 
BEGIN

DECLARE @MAXID INT  
DECLARE @MINID INT  

DECLARE @CoinIn INT  
DECLARE @CoinOut INT  
DECLARE @Bill500 INT  
DECLARE @Bill200 INT  
DECLARE @Bill100 INT  
DECLARE @Bill50 INT  
DECLARE @Bill20 INT  
DECLARE @Bill10 INT  
DECLARE @Bill5 INT  
DECLARE @Bill1 INT  
DECLARE @TrueCoinIn INT  
DECLARE @TrueCoinOut INT  
DECLARE @Drop INT  
DECLARE @Jackpot INT  
DECLARE @CancelledCredits INT  
DECLARE @HandPaidCancelledCredits INT  
DECLARE @CashableTicketIn INT  
DECLARE @CashableTicketOut INT  
DECLARE @CashableTicketInQty INT  
DECLARE @CashableTicketOutQty INT  
DECLARE @ProgressiveHandPay INT  

SELECT @MINID = MIN(ID) FROM MaintenanceHistory WHERE @SessionID = SessionID AND SITE_ID = @SiteID  
SELECT @MAXID = MAX(ID) FROM MaintenanceHistory WHERE @SessionID = SessionID AND SITE_ID = @SiteID  

SELECT   
@CoinIn = CoinIn ,@CoinOut = CoinOut  ,@Bill500 = Bill500 ,@Bill200 = Bill200 ,@Bill100 = Bill100 ,@Bill50 = Bill50 ,@Bill20 = Bill20 ,@Bill10 = Bill10  
,@Bill5 = Bill5 ,@Bill1 = Bill1 ,@TrueCoinIn = TrueCoinIn ,@TrueCoinOut = TrueCoinOut ,@Drop = [Drop] ,@Jackpot = Jackpot  
,@CancelledCredits = CancelledCredits ,@HandPaidCancelledCredits = HandPaidCancelledCredits ,@CashableTicketIn = CashableTicketIn  
,@CashableTicketOut = CashableTicketOut  ,@CashableTicketInQty = CashableTicketInQty ,@CashableTicketOutQty = CashableTicketOutQty  
,@ProgressiveHandPay = ProgressiveHandPay  
 FROM   
MaintenanceHistory  
WHERE ID = @MINID AND SITE_ID = @SiteID  


IF EXISTS (SELECT * FROM SYS.Objects Where Name like 'MeterDetails_Temp' And Type = 'u')  
   BEGIN  
    DROP TABLE MeterDetails_Temp  
   END   

CREATE TABLE MeterDetails_Temp   
( Meter Varchar(50), [Start] INT, [End] INT, [Delta] INT, [Value] Varchar(50) )  

INSERT INTO MeterDetails_Temp   
SELECT 'CoinIn' , @CoinIn, CoinIn, CoinIn - @CoinIn, Convert(Varchar(20), Convert(Decimal(10,2),(Convert( Decimal(10,2),CoinIn) - Convert( Decimal(10,2),@CoinIn)) / 100))
 FROM MaintenanceHistory WHERE ID = @MAXID AND SITE_ID = @SiteID  

INSERT INTO MeterDetails_Temp   
SELECT 'CoinOut' , @CoinOut, CoinOut, CoinOut - @CoinOut, Convert(Varchar(20), Convert(Decimal(10,2),(Convert( Decimal(10,2),CoinOut) - Convert( Decimal(10,2),@CoinOut)) / 100))  
 FROM MaintenanceHistory WHERE ID = @MAXID AND SITE_ID = @SiteID  

IF EXISTS(SELECT 1 FROM Setting WHERE Setting_Name = 'Client' and Setting_Value = 'SISAL')
BEGIN

	INSERT INTO MeterDetails_Temp   
	SELECT 'Bill500' , @Bill500, Bill500, Bill500 - @Bill500, Convert(Varchar(20), Convert(Decimal(10,2),(Convert( Decimal(10,2),Bill500) - Convert( Decimal(10,2),@Bill500)) / 100))  
	 FROM MaintenanceHistory WHERE ID = @MAXID AND SITE_ID = @SiteID


	INSERT INTO MeterDetails_Temp   
	SELECT 'Bill200' , @Bill200, Bill200, Bill200 - @Bill200, Convert(Varchar(20), Convert(Decimal(10,2),(Convert( Decimal(10,2),Bill200) - Convert( Decimal(10,2),@Bill200)) / 100))  
	 FROM MaintenanceHistory WHERE ID = @MAXID AND SITE_ID = @SiteID
END

INSERT INTO MeterDetails_Temp   
SELECT 'Bill100' , @Bill100, Bill100, Bill100 - @Bill100, Convert(Varchar(20), Convert(Decimal(10,2),(Convert( Decimal(10,2),Bill100) - Convert( Decimal(10,2),@Bill100)) / 100))  
 FROM MaintenanceHistory WHERE ID = @MAXID AND SITE_ID = @SiteID  


INSERT INTO MeterDetails_Temp   
SELECT 'Bill50' , @Bill50, Bill50, Bill50 - @Bill50, Convert(Varchar(20), Convert(Decimal(10,2),(Convert( Decimal(10,2),Bill50) - Convert( Decimal(10,2),@Bill50)) / 100))  
 FROM MaintenanceHistory WHERE ID = @MAXID AND SITE_ID = @SiteID  

INSERT INTO MeterDetails_Temp   
SELECT 'Bill20' , @Bill20, Bill20, Bill20 - @Bill20, Convert(Varchar(20), Convert(Decimal(10,2),(Convert( Decimal(10,2),Bill20) - Convert( Decimal(10,2),@Bill20)) / 100))  
 FROM MaintenanceHistory WHERE ID = @MAXID AND SITE_ID = @SiteID  

INSERT INTO MeterDetails_Temp   
SELECT 'Bill10' , @Bill10, Bill10, Bill10 - @Bill10, Convert(Varchar(20), Convert(Decimal(10,2),(Convert( Decimal(10,2),Bill10) - Convert( Decimal(10,2),@Bill10)) / 100))  
 FROM MaintenanceHistory WHERE ID = @MAXID AND SITE_ID = @SiteID  

INSERT INTO MeterDetails_Temp   
SELECT 'Bill5' , @Bill5, Bill5, Bill5 - @Bill5, Convert(Varchar(20), Convert(Decimal(10,2),(Convert( Decimal(10,2),Bill5) - Convert( Decimal(10,2),@Bill5)) / 100))  
 FROM MaintenanceHistory WHERE ID = @MAXID AND SITE_ID = @SiteID  

IF NOT EXISTS(SELECT 1 FROM Setting WHERE Setting_Name = 'Client' and Setting_Value = 'SISAL')
BEGIN
	INSERT INTO MeterDetails_Temp   
	SELECT 'Bill1' , @Bill1, Bill1, Bill1 - @Bill1, Convert(Varchar(20), Convert(Decimal(10,2),(Convert( Decimal(10,2),Bill1) - Convert( Decimal(10,2),@Bill1)) / 100))  
	FROM MaintenanceHistory WHERE ID = @MAXID AND SITE_ID = @SiteID  
END

INSERT INTO MeterDetails_Temp   
SELECT 'TrueCoinIn' , @TrueCoinIn, TrueCoinIn, TrueCoinIn - @TrueCoinIn, Convert(Varchar(20), Convert(Decimal(10,2),(Convert( Decimal(10,2),TrueCoinIn) - Convert( Decimal(10,2),@TrueCoinIn)) / 100))  
 FROM MaintenanceHistory WHERE ID = @MAXID AND SITE_ID = @SiteID  

INSERT INTO MeterDetails_Temp   
SELECT 'TrueCoinOut' , @TrueCoinOut, TrueCoinOut, TrueCoinOut - @TrueCoinOut, Convert(Varchar(20), Convert(Decimal(10,2),(Convert( Decimal(10,2),TrueCoinOut) - Convert( Decimal(10,2),@TrueCoinOut)) / 100))  
 FROM MaintenanceHistory WHERE ID = @MAXID AND SITE_ID = @SiteID  

INSERT INTO MeterDetails_Temp   
SELECT 'Drop' , @Drop, [Drop], [Drop] - @Drop, Convert(Varchar(20), Convert(Decimal(10,2),(Convert( Decimal(10,2),[Drop]) - Convert( Decimal(10,2),@Drop)) / 100))  
 FROM MaintenanceHistory WHERE ID = @MAXID AND SITE_ID = @SiteID  

INSERT INTO MeterDetails_Temp   
SELECT 'Jackpot' , @Jackpot, Jackpot, Jackpot - @Jackpot, Convert(Varchar(20), Convert(Decimal(10,2),(Convert( Decimal(10,2),Jackpot) - Convert( Decimal(10,2),@Jackpot)) / 100))  
 FROM MaintenanceHistory WHERE ID = @MAXID AND SITE_ID = @SiteID  

INSERT INTO MeterDetails_Temp   
SELECT 'CancelledCredits' , @CancelledCredits, CancelledCredits, CancelledCredits - @CancelledCredits, Convert(Varchar(20), Convert(Decimal(10,2),(Convert( Decimal(10,2),CancelledCredits) - Convert( Decimal(10,2),@CancelledCredits)) / 100))  
 FROM MaintenanceHistory WHERE ID = @MAXID AND SITE_ID = @SiteID  

INSERT INTO MeterDetails_Temp   
SELECT 'Attendant paid Cancelcredits' , @HandPaidCancelledCredits, HandPaidCancelledCredits, HandPaidCancelledCredits - @HandPaidCancelledCredits, Convert(Varchar(20), Convert(Decimal(10,2),(Convert( Decimal(10,2),HandPaidCancelledCredits) - Convert( Decimal(10,2),@HandPaidCancelledCredits)) / 100))  
 FROM MaintenanceHistory WHERE ID = @MAXID AND SITE_ID = @SiteID  

INSERT INTO MeterDetails_Temp   
SELECT 'CashableTicketIn' , @CashableTicketIn, CashableTicketIn, CashableTicketIn - @CashableTicketIn, Convert(Varchar(20), Convert(Decimal(10,2),(Convert( Decimal(10,2),CashableTicketIn) - Convert( Decimal(10,2),@CashableTicketIn)) / 100))  
 FROM MaintenanceHistory WHERE ID = @MAXID AND SITE_ID = @SiteID  

INSERT INTO MeterDetails_Temp   
SELECT 'CashableTicketOut' , @CashableTicketOut, CashableTicketOut, CashableTicketOut - @CashableTicketOut, Convert(Varchar(20), Convert(Decimal(10,2),(Convert( Decimal(10,2),CashableTicketOut) - Convert( Decimal(10,2),@CashableTicketOut)) / 100))  
 FROM MaintenanceHistory WHERE ID = @MAXID AND SITE_ID = @SiteID

INSERT INTO MeterDetails_Temp   
SELECT 'CashableTicketInQty'  , @CashableTicketInQty, CashableTicketInQty, CashableTicketInQty - @CashableTicketInQty, Convert(Varchar(20), CashableTicketInQty - @CashableTicketInQty)  
 FROM MaintenanceHistory WHERE ID = @MAXID AND SITE_ID = @SiteID  

INSERT INTO MeterDetails_Temp   
SELECT 'CashableTicketOutQty' , @CashableTicketOutQty, CashableTicketOutQty, CashableTicketOutQty - @CashableTicketOutQty, Convert(Varchar(20), CashableTicketOutQty - @CashableTicketOutQty)  
 FROM MaintenanceHistory WHERE ID = @MAXID AND SITE_ID = @SiteID  

INSERT INTO MeterDetails_Temp   
SELECT 'Attendant paid Progressive' , @ProgressiveHandPay, ProgressiveHandPay, ProgressiveHandPay - @ProgressiveHandPay, Convert(Varchar(20), Convert(Decimal(10,2),(Convert( Decimal(10,2),ProgressiveHandPay) - Convert( Decimal(10,2),@ProgressiveHandPay)) / 100))  
 FROM MaintenanceHistory WHERE ID = @MAXID AND SITE_ID = @SiteID  

SELECT * FROM MeterDetails_Temp   



IF EXISTS (SELECT * FROM SYS.Objects Where Name like 'MeterDetails_Temp' And Type = 'u')  
BEGIN  
DROP TABLE MeterDetails_Temp  
END   


END  

GO
