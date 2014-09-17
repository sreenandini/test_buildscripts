USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetPromoTicketForPeriodDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetPromoTicketForPeriodDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****
Version History
----------------------------------------
Kirubakar S		28 May 2010		Created
----------------------------------------
***/

CREATE PROCEDURE dbo.rsp_GetPromoTicketForPeriodDetails        
          
   @StartDate datetime,          
   @EndDate   datetime,  
 @SITE int=0           
          
AS          
          
 SET NOCOUNT ON          
        
        
DECLARE @NONCashable VARCHAR(500)            
DECLARE @Cashable VARCHAR(500)            
DECLARE @TicketDB VARCHAR(100)          
            
EXEC rsp_GetSETting 0, 'PROMO_TICKET_CODE', '8', @Cashable OUTPUT            
EXEC rsp_GetSETting 0, 'PROMO_TICKET_CODE_NONCASH', '7', @NONCashable OUTPUT            
EXEC rsp_GetSETting 0, 'TICKET_DB_NAME', '7', @TicketDB OUTPUT            
            
          
 CREATE TABLE #VoucherInfo          
  (          
   ivoucherid   INT,          
   DeviceID   INT,          
   PrintDevice   VARCHAR(12),          
   PayDevice   VARCHAR(12),        
   GameTitle Varchar(200),          
   strBarCode   CHAR(32),          
   iAmount    INT,          
   strVoucherStatus CHAR(3),          
   dtPaid    DATETIME,          
   dtPrinted   DATETIME,          
   strDeviceType  CHAR(6),          
   ActualBarCode  CHAR(32)          
  )          
        
        
 INSERT INTO #VoucherInfo(        
   ivoucherid   ,          
   DeviceID   ,          
   PrintDevice   ,          
   PayDevice   ,           
   strBarCode   ,          
   iAmount    ,          
   strVoucherStatus ,          
   dtPaid    ,          
   dtPrinted   ,          
   strDeviceType  ,          
   ActualBarCode   )        
 SELECT  V.ivoucherid,          
   V.iDeviceID,          
   PRD.strserial,          
   PAD.strserial,          
   V.strBarCode,          
   V.iAmount,          
   V.strVoucherStatus,          
   V.dtPaid,          
   V.dtPrinted,          
   PAD.strDeviceType,          
   V.strBarCode          
FROM dbo.Voucher V(NOLOCK)        
          
LEFT JOIN dbo.Device AS PRD  (NOLOCK) ON PRD.ideviceid = V.ideviceid           
LEFT JOIN dbo.Device AS PAD (NOLOCK) ON PAD.ideviceid = V.ipaydeviceid          
          
    WHERE   
(@SITE=0 OR V.iSITEID=(Select Site_Code from Site where Site_ID=@SITE))  
AND       
V.strVoucherStatus = 'PD'        
And LEFT(V.strBarCode ,1 ) IN (@NONCashable, @Cashable)        
AND V.dtPaid BETWEEN @StartDate AND @EndDate        
        
ORDER BY ivoucherid desc          
        
        
UPDATE VI          
  SET ActualBarCode = CASE WHEN ISNULL(MC.Validation_Length, 0) > 0           
       THEN RIGHT(LTRIM(RTRIM(strBarCode)), ISNULL(MC.Validation_Length, 0))          
       ELSE ActualBarCode END,        
    VI.GameTitle  = MC.Machine_Name        
  FROM #VoucherInfo VI          
 LEFT JOIN dbo.Device D ON D.ideviceid = VI.DeviceID          
 INNER JOIN Machine M ON D.strSerial = M.Machine_Stock_No  
 INNER JOIN Machine_Class MC ON M.Machine_Class_ID = MC.Machine_Class_ID          
          
Select * from #VoucherINfo  
GO

