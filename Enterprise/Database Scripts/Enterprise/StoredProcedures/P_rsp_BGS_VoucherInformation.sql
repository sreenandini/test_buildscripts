USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_BGS_VoucherInformation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_BGS_VoucherInformation]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/***  
Version History  
---------------------------------------  
Kirubakar Created  28 May 2010  
---------------------------------------  
***/  
   
CREATE PROCEDURE dbo.rsp_BGS_VoucherInformation            
            
 @StartDate datetime,            
 @EndDate   datetime,            
 @Type      varchar(1) = '',            
 @Barcode   varchar(20) = '%',            
 @IsLiability BIT = 0,        
 @SITE int=0        
     
AS            
            
DECLARE @Site_Code VARCHAR(50)    
 SET NOCOUNT ON            
       
IF(@site!=0)    
BEGIN    
 SELECT @Site_Code= site_code     
 FROM site     
 WHERE Site_id=@SITE       
END    
         
 CREATE TABLE #VoucherInfo              
  (              
 ivoucherid   INT,              
 DeviceID   INT,              
 PrintDevice   VARCHAR(50),              
 PayDevice   VARCHAR(50),              
 strBarCode   CHAR(32),              
 iAmount    INT,              
 strVoucherStatus CHAR(3),              
 dtPaid    DATETIME,              
 dtPrinted   DATETIME,            
 dtExpire DATETIME,            
 strDeviceType  CHAR(6),              
 ActualBarCode  CHAR(32)              
  )              
              
  IF ( @barcode <> '%' AND @barcode <> '' )              
  BEGIN              
    EXEC rsp_BGS_VoucherInformation_ByNumber @barcode     
    RETURN (0)              
  END                       
              
IF @IsLiability = 0              
    
BEGIN         
 INSERT INTO #VoucherInfo               
 SELECT      
 V.ivoucherid,              
 V.iDeviceID,              
 PRD.strserial,              
 PAD.strserial,              
 V.strBarCode,              
 V.iAmount,              
 V.strVoucherStatus,              
 V.dtPaid,              
 V.dtPrinted,           
 V.dtExpire,             
 PAD.strDeviceType,              
 V.strBarCode              
FROM     
dbo.Voucher V(NOLOCK)              
LEFT JOIN dbo.Device AS PRD  (NOLOCK)     
 ON PRD.ideviceid = V.ideviceid AND V.ISITEID=PRD.Site_Code                     
LEFT JOIN dbo.Device AS PAD (NOLOCK)     
 ON PAD.ideviceid = V.ipaydeviceid AND V.ISITEID=PAD.Site_Code                 
WHERE     
 (@SITE=0 OR (V.ISITEID=@Site_Code AND (PRD.Site_Code=@Site_Code OR PAD.Site_Code=@Site_Code)))        
 AND(    
   -- claimed     
    (@Type = 'C' AND V.strVoucherStatus = 'PD' AND V.dtPaid BETWEEN @StartDate AND @EndDate AND ISNULL(V.Ticket_Type, 0) = 0 )                 
   OR -- printed     
    (@Type = 'P' AND V.dtPrinted BETWEEN @StartDate AND @EndDate and COALESCE(V.strVoucherStatus,'')  NOT IN ('VD', 'NA')     
     AND ISNULL(V.Ticket_Type, 0) = 0 )             
   OR -- unclaimed       
    (@Type = 'U' AND COALESCE(V.strVoucherStatus,'')  = ''  and dtExpire > @EndDate             
     AND V.dtPrinted BETWEEN @StartDate AND @EndDate AND ISNULL(V.Ticket_Type, 0) = 0)                     
   OR -- errors                         
    (@Type = 'E' AND V.strVoucherStatus = 'PP' 
    AND V.dtPrinted BETWEEN @StartDate AND @EndDate AND ISNULL(V.Ticket_Type, 0) = 0 AND V.errcode <> 0)    
   OR -- Void    
    (@Type = 'V' AND (V.strVoucherStatus = 'VD'and V.dtPrinted BETWEEN @StartDate AND @EndDate) AND ISNULL(V.Ticket_Type, 0) = 0)                      
   OR -- Expired              
    (@Type = 'D' AND (V.strVoucherStatus is null OR V.strVoucherStatus = 'EXP') AND v.dtExpire <= getdate() AND            
     V.dtExpire BETWEEN @StartDate AND @EndDate AND ISNULL(V.Ticket_Type, 0) = 0)                
   OR -- all              
    (@Type = 'A'AND V.dtPrinted BETWEEN @StartDate AND @EndDate AND ISNULL(V.Ticket_Type, 0) = 0)                 
   OR -- Auto Cancelled Tickets              
    (@Type = 'N' AND V.strVoucherStatus = 'NA' and V.dtPrinted BETWEEN @StartDate AND @EndDate AND ISNULL(V.Ticket_Type, 0) = 0)                 
   OR -- FOR BOTH VOID AND Auto Cancelled Tickets              
    (@Type = 'B' AND (V.strVoucherStatus = 'VD' OR  V.strVoucherStatus = 'NA') and V.dtPrinted BETWEEN @StartDate AND @EndDate          
     AND ISNULL(V.Ticket_Type, 0) = 0)          
   OR     
    (@Type = 'O' AND V.dtPrinted BETWEEN @StartDate AND @EndDate and COALESCE(V.strVoucherStatus,'') NOT IN ('VD', 'NA')     
     AND ISNULL(V.Ticket_Type, 0) = 1)          
   OR     
    (@Type = 'I' AND V.strVoucherStatus = 'PD' AND V.errcode <> 0 AND V.dtPaid BETWEEN @StartDate AND @EndDate
     AND ISNULL(V.Ticket_Type, 0) = 1 )          
        )     
ORDER BY     
 ivoucherid desc              
          
END              
ELSE              
BEGIN              
              
 INSERT INTO #VoucherInfo               
 SELECT      
 V.ivoucherid,              
 V.iDeviceID,              
 PRD.strserial,              
 PAD.strserial,              
 V.strBarCode,              
 V.iAmount,              
 V.strVoucherStatus,              
 V.dtPaid,              
 V.dtPrinted,            
 V.dtExpire,             
 PAD.strDeviceType,              
 V.strBarCode    
FROM     
 dbo.Voucher V(NOLOCK)              
              
LEFT JOIN dbo.Device AS PRD  (NOLOCK)     
 ON PRD.ideviceid = V.ideviceid  AND V.ISITEID=PRD.Site_Code             
LEFT JOIN dbo.Device AS PAD (NOLOCK)     
 ON PAD.ideviceid = V.ipaydeviceid AND V.ISITEID=PAD.Site_Code              
              
WHERE     
  (@SITE=0 OR (V.ISITEID=@Site_Code AND (PRD.Site_Code=@Site_Code OR PAD.Site_Code=@Site_Code)))     
 AND(       
    
   -- claimed              
   (@Type = 'C' AND V.strVoucherStatus = 'PD' AND V.dtPaid BETWEEN @StartDate AND @EndDate     
    AND V.dtPrinted < @StartDate AND ISNULL(V.Ticket_Type, 0) = 0)          
  OR  -- printed    
   (@Type = 'P' AND V.dtPrinted BETWEEN @StartDate AND @EndDate AND COALESCE(V.strVoucherStatus,'')  NOT IN ('VD', 'NA')              
    AND (dtPaid IS NULL OR dtPaid > @EndDate) AND ISNULL(V.Ticket_Type, 0) = 0)          
  OR  -- claimed              
   (@Type = 'I' AND V.strVoucherStatus = 'PD' AND V.dtPaid BETWEEN @StartDate AND @EndDate               
    AND V.dtPrinted < @StartDate AND ISNULL(V.Ticket_Type, 0) = 1)          
  OR     
   (@Type = 'O' AND V.dtPrinted BETWEEN @StartDate AND @EndDate AND COALESCE(V.strVoucherStatus,'')  NOT IN ('VD', 'NA')          
    AND (dtPaid IS NULL OR dtPaid > @EndDate) AND ISNULL(V.Ticket_Type, 0) = 1)    
  )     
 ORDER BY     
 ivoucherid desc                        
END              
              
 UPDATE #VoucherInfo              
  SET ActualBarCode = CASE WHEN ISNULL(MC.Validation_Length, 0) > 0               
       THEN RIGHT(LTRIM(RTRIM(strBarCode)), ISNULL(MC.Validation_Length, 0))              
       ELSE ActualBarCode END              
  FROM #VoucherInfo VI              
 LEFT JOIN dbo.Device D ON D.ideviceid = VI.DeviceID              
 INNER JOIN Machine M ON D.strSerial  = M.Machine_Stock_No              
 INNER JOIN Machine_Class MC ON M.Machine_Class_ID = MC.Machine_Class_ID              
              
              
 SELECT * FROM #VoucherInfo       

GO

