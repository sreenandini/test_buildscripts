USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_REPORT_LotteryRevenue]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_REPORT_LotteryRevenue]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[rsp_REPORT_LotteryRevenue]      
      
  @Company    INT = 0,      
  @SubCompany INT = 0,      
  @Region     INT = 0,      
  @Area       INT = 0,      
  @District   INT = 0,      
  @Site       INT = 0,      
  @Category   INT = 0,       
  @StartDate  datetime,      
  @EndDate    datetime
      
AS      
DECLARE      
 @calcStartDate DateTime,      
 @calcEndDate DateTime      
 SET DATEFORMAT dmy      
 SET @calcStartDate=Cast(@startdate as DateTime)      
 SET @calcEndDate=Cast(@enddate as DateTime)      
       
  IF @company = 0    SET @company = NULL      
  IF @subcompany = 0 SET @subcompany = NULL      
  IF @region = 0     SET @region = NULL      
  IF @area = 0       SET @area = NULL      
  IF @district = 0   SET @district = NULL      
  IF @site = 0       SET @site = NULL      
  IF @category = 0   SET @category = NULL      
      
  DECLARE @NOTSET varchar(20)      
  SET @NOTSET = 'UN-DEFINED'      
      
  select company.company_id,      
  site.sub_company_id,      
  site.sub_company_region_id,      
  site.sub_company_area_id,      
  site.sub_company_district_id,      
  site.site_id,      
  site.site_name,      
  CAST( ISNULL (cat.machine_type_id,0) AS INT) machine_type_id,    
  ISNULL (cat.machine_type_code,@NOTSET) machine_type_code,    
  ISNULL (cat.machine_type_description,@NOTSET) machine_type_description,    
  machine.machine_stock_no,       
  machine.Machine_Manufacturers_Serial_No,    
  (CAST( ISNULL (C.Cash_Collected_2p,0) AS FLOAT)    
    + CAST(ISNULL (C.Cash_Collected_5p,0) AS FLOAT) + CAST(ISNULL (C.Cash_Collected_10p,0) AS FLOAT)    
    + CAST(ISNULL (C.Cash_Collected_20p,0) AS FLOAT) + CAST(ISNULL (C.Cash_Collected_50p,0) AS FLOAT)    
    + CAST(ISNULL (C.Cash_Collected_100p,0)AS FLOAT)  + CAST(ISNULL (C.Cash_Collected_200p,0) AS FLOAT)    
    + CAST(ISNULL (C.Cash_Collected_500p,0)AS FLOAT) + CAST(ISNULL (C.Cash_Collected_1000p,0)  AS FLOAT)   
    + CAST(ISNULL (C.Cash_Collected_2000p,0)AS FLOAT)  + CAST(ISNULL (C.Cash_Collected_5000p,0) AS FLOAT)    
    + CAST(ISNULL (C.Cash_Collected_10000p,0)AS FLOAT)  + CAST(ISNULL (C.Cash_Collected_20000p,0)AS FLOAT)     
    + CAST(ISNULL (C.Cash_Collected_50000p,0)AS FLOAT) + CAST(ISNULL (C.Cash_Collected_100000p,0) AS FLOAT)    
    + CAST(ISNULL (C.Cash_Collected_200000p,0)AS FLOAT)  + CAST(ISNULL (C.Cash_Collected_500000p,0)  AS FLOAT)   
    + CAST(ISNULL (C.Cash_Collected_1000000p,0)AS FLOAT))/100 + ISNULL (C.DeclaredTicketValue,0)   
AS CashIn,    
  (CAST( ISNULL (C.DeclaredTicketPrintedValue,0)AS FLOAT) + (CAST (dbo.GetAttendantPay(C.Collection_ID) AS FLOAT))  
 -- + CAST(ISNULL (C.Collection_Treasury_Handpay,0) AS FLOAT)           
  -- + CAST(ISNULL (C.Progressive_Value_Declared,0)AS FLOAT)         
 + COALESCE( (SELECT SUM(Treasury_Amount) FROM Treasury_Entry   
WHERE Treasury_Entry.Collection_ID = C.Collection_ID   
AND Treasury_Type = 'Shortpay' ), 0 ) ) AS CashOut ,    
 --C.Collection_RDC_CoinsIn As CashPaid,    
 --C.Collection_RDC_CoinsOut As CashWon,    
(            
            
Case             
            
 WHEN mc.Machine_Class_SP_Features IN (1, 10, 12, 20) THEN            
            
  ((CAST(C.COLLECTION_RDC_COINSIN AS FLOAT) * Installation.Installation_Price_Per_Play) / 100)             
            
 ELSE            
            
  ((CAST(C.COLLECTION_RDC_VTP AS FLOAT)) / 10)            
            
END            
            
) AS CashPaid,            
            
            
(            
            
Case             
            
 WHEN mc.Machine_Class_SP_Features IN (1, 10, 12, 20) THEN            
             
  ((CAST(C.COLLECTION_RDC_COINSOUT AS FLOAT) * Installation.Installation_Price_Per_Play) / 100)            
            
 ELSE            
            
  ((CAST(ISNULL(C.CASH_OUT_2P,0) AS FLOAT) / 50) +             
  (CAST(ISNULL(C.CASH_OUT_5P,0) AS FLOAT) / 20) +            
  (CAST(ISNULL(C.CASH_OUT_10P,0) AS FLOAT) / 10) +            
  (CAST(ISNULL(C.CASH_OUT_20P,0) AS FLOAT) / 5) +            
  (CAST(ISNULL(C.CASH_OUT_50P,0) AS FLOAT) / 2) +            
  (CAST(ISNULL(C.CASH_OUT_100P,0) AS FLOAT)) +            
  (CAST(ISNULL(C.CASH_OUT_200P,0) AS FLOAT) * 2) +            
  (CAST(ISNULL(C.CASH_OUT_500P,0) AS FLOAT) * 5) +            
  (CAST(ISNULL(C.CASH_OUT_1000P,0) AS FLOAT) * 10) +            
  (CAST(ISNULL(C.CASH_OUT_2000P,0) AS FLOAT) * 20) +            
  (CAST(ISNULL(C.CASH_OUT_5000P,0) AS FLOAT) * 50) +           
(CAST(ISNULL(C.CASH_OUT_10000P,0) AS FLOAT) * 100)    +      
(CAST(ISNULL(C.CASH_OUT_20000P,0) AS FLOAT) * 200)    +      
(CAST(ISNULL(C.CASH_OUT_50000P,0) AS FLOAT) * 500)    +      
(CAST(ISNULL(C.CASH_OUT_100000P,0) AS FLOAT) * 1000)          
 )            
            
  -            
            
  ROUND(((CAST(ISNULL(C.CASH_IN_2P,0) AS FLOAT) / 50) +             
  (CAST(ISNULL(C.CASH_IN_5P,0) AS FLOAT) / 20) +            
  (CAST(ISNULL(C.CASH_IN_10P,0) AS FLOAT) / 10) +            
  (CAST(ISNULL(C.CASH_IN_20P,0) AS FLOAT) / 5) +            
  (CAST(ISNULL(C.CASH_IN_50P,0) AS FLOAT) / 2) +            
  (CAST(ISNULL(C.CASH_IN_100P,0) AS FLOAT)) +            
  (CAST(ISNULL(C.CASH_IN_200P,0) AS FLOAT) * 2) +            
  (CAST(ISNULL(C.CASH_IN_500P,0) AS FLOAT) * 5) +            
  (CAST(ISNULL(C.CASH_IN_1000P,0) AS FLOAT) * 10) +            
  (CAST(ISNULL(C.CASH_IN_2000P,0) AS FLOAT) * 20) +            
  (CAST(ISNULL(C.CASH_IN_5000P,0) AS FLOAT) * 50) +           
(CAST(ISNULL(C.CASH_IN_10000P,0) AS FLOAT) * 100) +      
(CAST(ISNULL(C.CASH_IN_20000P,0) AS FLOAT) * 200) +      
(CAST(ISNULL(C.CASH_IN_50000P,0) AS FLOAT) * 500) +      
(CAST(ISNULL(C.CASH_IN_100000P,0) AS FLOAT) * 1000)      
)            
            
    -            
            
  ((CAST(C.COLLECTION_RDC_VTP AS FLOAT)) / 10),0)            
            
END            
            
) AS CashWon,   
 C.Collection_RDC_GamesBet AS GamesPlayed,     
 C.Collection_RDC_GamesWon As GamesWon     
      
    from Collection C    
      join installation on C.installation_id = installation.installation_id      
      join machine on installation.machine_id = machine.machine_id    
   join machine_class mc on machine.machine_class_id = mc.machine_class_id    
   LEFT join machine_type cat on cat.machine_type_id = machine.machine_category_id       
   inner join bar_position b on installation.bar_position_id = b.bar_position_id    
      join site on site.site_id = b.site_id      
      join sub_company on site.sub_company_id = sub_company.sub_company_id      
      join company  on sub_company.company_id = company.company_id   
   join Batch On Batch.Batch_ID = C.Batch_ID     
     where cast ( collection_date as datetime ) between convert ( varchar(12), @calcStartDate, 106 )  and convert( varchar(12), @calcEndDate, 106 )      
     AND ( ( @company IS NULL ) OR  ( @company IS NOT NULL AND company.company_id = @company))      
     AND ( ( @subcompany IS NULL ) OR ( @subcompany IS NOT NULL AND site.sub_company_id = @subcompany))      
     AND ( ( @region IS NULL ) OR ( @region IS NOT NULL AND site.sub_company_region_id = @region))      
     AND ( ( @area IS NULL ) OR ( @area IS NOT NULL AND site.sub_company_area_id = @area))      
    AND ( ( @district IS NULL ) OR ( @district IS NOT NULL AND site.sub_company_district_id = @district))      
     AND ( ( @site IS NULL ) OR ( @site IS NOT NULL AND site.site_id = @site))      
     AND ( ( @category IS NULL ) OR  ( @category IS NOT NULL AND cat.machine_type_id = @category))      
     order by     
         machine.machine_stock_no,    
         company.company_id,      
         site.sub_company_id,      
         site.sub_company_region_id,      
         site.sub_company_area_id,      
         site.sub_company_district_id,      
         site.site_id,      
         site.site_name,      
         cat.machine_type_id    

GO

