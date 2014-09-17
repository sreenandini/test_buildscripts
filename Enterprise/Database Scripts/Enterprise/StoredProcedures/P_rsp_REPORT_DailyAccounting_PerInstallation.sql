USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_REPORT_DailyAccounting_PerInstallation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_REPORT_DailyAccounting_PerInstallation]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


 /**    History  
 Anuradha      05/09/2011    Created  
 
 Stored Procedure [dbo].rsp_REPORT_DailyAccounting 0,0,0,0,0,'13-Oct-2008','13-Oct-2012'
 EXEC rsp_REPORT_DailyAccounting_PerInstallation @company = 0,@subcompany=0,@region=0,@area=0,@district=0,@site=0,@startdate='2010-12-09',@enddate='2012-12-16'   
**/     
  
Create PROCEDURE [dbo].[rsp_REPORT_DailyAccounting_PerInstallation]          
	@SubCompany INT = 0,            
	@Region     INT = 0,            
	@Area       INT = 0,            
	@District   INT = 0,            
	@Site       INT = 0,            
	@StartDate  DateTime,            
	@EndDate    DateTime,  
	@Company int = 0  ,
	@SiteIDList VARCHAR(MAX)        
            
AS            
            
DECLARE            
 @calcStartDate DateTime,            
 @calcEndDate DateTime            
 SET DATEFORMAT dmy            
            
 SET @calcStartDate=Cast(@startdate as DateTime)            
 SET @calcEndDate=Cast(@enddate as DateTime)            
             
         
  DECLARE @Criteria VARCHAR(1000)                  
                  
              
 declare @denom float           
SET @denom = 0      
  
declare @companyname varchar(50)              
if (ISNULL(@company,0) <> 0)              
 select @companyname = company_name from company where company_id = @company              
else              
 set  @companyname = '--Any--'   
              
declare @subcompanyname varchar(50)                  
if (ISNULL(@subcompany,0) <> 0)                  
 select @subcompanyname = sub_company_name from sub_company where sub_company_id = @subcompany                  
else                  
 set  @subcompanyname = '--Any--'                  
                  
declare @sitename varchar(50)                  
if (ISNULL(@site,0) <> 0)                  
 select @sitename = site_name from site where site_id = @site                  
else                  
 set  @sitename = '--Any--'                  
                  
declare @regionname varchar(50)                
if (ISNULL(@region,0) <> 0)        
 select @regionname = Sub_Company_Region_Name from sub_company_region where Sub_Company_Region_ID = @region                  
else               
 set  @regionname = '--Any--'                
               
          
       
declare @areaname varchar(50)                
if (ISNULL(@area,0) <> 0)      
 select @areaname = Sub_Company_Area_Name from sub_company_area where Sub_Company_Area_ID = @area       
else               
 set  @areaname = '--Any--'                
              
         
     declare @districtname varchar(50)                
if (ISNULL(@district,0) <> 0)       
 select @districtname = Sub_Company_District_Name from sub_company_district where Sub_Company_District_ID = @district     
else                
 set  @districtname = '--Any--'                
               
     
                 
SET @Criteria ='Company: ' + cast(@companyname as varchar(50))+ ' | ' +          
'Sub Company: ' + cast(@subcompanyname as varchar(50))+ ' | ' +         
  'Region: ' + cast(@regionname as varchar(50))+ ' | ' +        
   'Area: ' + cast(@areaname as varchar(50))+ ' | ' +        
   'District: ' + cast(@districtname as varchar(50))+ ' | ' +           
      'Site: ' + cast(@sitename as varchar(50))+ ' | ' +                      
 'Grouping on (Site, MachineType)'                     
              
              
              
          DECLARE @NOTSET varchar(20)            
             
 SET @NOTSET = 'UN-DEFINED'            
            
  IF @subcompany = 0 SET @subcompany = NULL            
  IF @region = 0     SET @region = NULL            
  IF @area = 0       SET @area = NULL            
  IF @district = 0   SET @district = NULL            
  IF @site = 0       SET @site = NULL            
              
        
Declare @eRegion varchar(2)                 
  Select @eRegion=UPPER(RIGHT(System_Parameter_Region_Culture,2)) from System_Parameters           
          
   declare @ProductVersion  varchar(50)                  
  SELECT TOP 1 @ProductVersion = 'BMC Version : ' + VersionName             
    FROM VersionHistory                   
ORDER BY VersionDate DESC                  
                     
  declare @ProductHeader  varchar(50)                  
  SELECT @ProductHeader = setting_value           
    FROM dbo.Setting            
   WHERE Setting_Name = 'BMC_Reports_Header'                 
                  
  DECLARE @currencyFormat VARCHAR(20)            
 SELECT @currencyFormat = setting_value
 FROM Setting WHERE Setting_Name = 'BMC_Reports_Language'          
          
  DECLARE @isAFTCalculationEnabled BIT          
  SELECT @isAFTCalculationEnabled = Setting_Value FROM Setting WHERE Setting_Name = 'IsAFTIncludedInCalculation'          
            
DECLARE @temptable TABLE             
(            
 Installation_ID int,            
 Bar_Position_ID int ,            
 --Installation_No,            
 Bar_Position_Name varchar(50),               
    Machine_Name varchar(50),            
 Zone_Name varchar(50),              
    Machine_Type_ID int,             
 Machine_Type_Code varchar(50),            
    Site_Name varchar(50),            
 CoinsIn float,           
 CoinsOut float,           
 BillsIn float,            
 TicketsIn float,           
 NonCashableVouchersIn float,           
 EFTIn float,          
 TotalCashIn float,            
 TicketsOut float,            
 NonCashableVouchersOut float,          
 Handpay float,             
 EFTOut float,          
 TotalCashOut float ,            
 Net float,          
 CurrencyFormat varchar(20),          
 Machine_Stock_No varchar(20)  ,      
 ProductVersion VARCHAR(50),      
 ProductHeader VARCHAR(50),     
 Criteria VARCHAR(1000),     
 Installationstatus VARCHAR(15)            
)            
INSERT INTO @temptable            
SELECT            
          
Installation.Installation_ID,            
Installation.Bar_Position_ID,            
Bar_Position.Bar_Position_Name,               
--CASE machine_class.machine_name WHEN 'Auto Detected' THEN 'Multi Game' ELSE machine_class.machine_name END AS machine_name,             
machine_class.machine_name AS machine_name,          
Zone.Zone_Name,              
Machine_Type_Category.Machine_Type_ID,             
Machine_Type_Category.Machine_Type_Code,            
Site.Site_Name,            
 (Sum(CAST(ISNULL(READ_RDC_TRUE_COIN_IN,0) AS FLOAT) )* Installation.Installation_Token_Value) / 100                  
            
 AS CoinsIn,--Coins In            
            
( Sum(CAST(ISNULL(READ_RDC_TRUE_COIN_Out,0) AS FLOAT) ) * Installation.Installation_Token_Value) / 100                   
              
 AS CoinsOut,--Coins Out            
 CASE WHEN Site.Region = 'US' Then            
                       
  Sum( CAST(ISNULL(CASH_IN_100p,0) AS FLOAT) )+                                  
 (Sum(CAST(ISNULL(CASH_IN_500p,0) AS FLOAT)) * 5)+             
 (Sum(CAST(ISNULL(CASH_IN_1000p,0) AS FLOAT)) * 10)+             
 (Sum(CAST(ISNULL(CASH_IN_2000p,0) AS FLOAT)) * 20)+             
 (Sum(CAST(ISNULL(CASH_IN_5000p,0) AS FLOAT)) * 50)+              
 (Sum(CAST(ISNULL(CASH_IN_10000p,0) AS FLOAT)) * 100)+             
 (Sum(CAST(ISNULL(CASH_IN_20000p,0) AS FLOAT) ) * 200)+      
 (Sum(CAST(ISNULL(CASH_IN_50000p,0) AS FLOAT) ) * 500)+            
 (Sum(CAST(ISNULL(CASH_IN_100000p,0) AS FLOAT)) * 1000)             
            
WHEN Site.Region = 'AR' Then            
                       
(Sum( CAST(ISNULL(CASH_IN_200p,0) AS FLOAT) ) * 2 ) +                                 
( Sum(CAST(ISNULL(CASH_IN_500p,0) AS FLOAT) ) * 5 ) +           
 (Sum(CAST(ISNULL(CASH_IN_1000p,0) AS FLOAT) ) * 10 ) +           
 (Sum(CAST(ISNULL(CASH_IN_2000p,0) AS FLOAT) ) * 20 ) +           
 (Sum(CAST(ISNULL(CASH_IN_5000p,0) AS FLOAT) ) * 50   ) +               
( Sum(CAST(ISNULL(CASH_IN_10000p,0) AS FLOAT) ) * 100)  +              
 (Sum(CAST(ISNULL(CASH_IN_20000p,0) AS FLOAT) ) * 200 ) +           
( Sum(CAST(ISNULL(CASH_IN_50000p,0) AS FLOAT) ) * 500   ) +             
( Sum(CAST(ISNULL(CASH_IN_100000p,0) AS FLOAT) ) * 1000)            
 ELSE                
          
(Sum( CAST(ISNULL(CASH_IN_500p,0) AS FLOAT) ) * 5) +           
(Sum( CAST(ISNULL(CASH_IN_1000p,0) AS FLOAT) ) * 10) +           
(Sum( CAST(ISNULL(CASH_IN_2000p,0) AS FLOAT) ) * 20) +           
(Sum( CAST(ISNULL(CASH_IN_5000p,0) AS FLOAT) ) * 50) +            
(Sum( CAST(ISNULL(CASH_IN_10000p,0) AS FLOAT) ) * 100) +           
(Sum( CAST(ISNULL(CASH_IN_20000p,0) AS FLOAT) ) * 200) +           
(Sum( CAST(ISNULL(CASH_IN_50000p,0) AS FLOAT) ) * 500) +           
(Sum( CAST(ISNULL(CASH_IN_100000p,0) AS FLOAT) ) * 1000)            
            
 END AS BillsIn, --Cash In            
--            
 Sum(ISNULL(dbo.[Read].Read_Ticket_In_Suspense, 0))/100 AS TicketsIn, --Tickets In            
          
 Sum(CAST(ISNULL([Read].TICKETS_INSERTED_NONCASHABLE_VALUE, 0) AS FLOAT) )/100 AS NonCashableVouchersIn, --Non Cashable Vouchers In            
          
(Sum( CAST(ISNULL([Read].Promo_Cashable_EFT_IN,0) AS FLOAT) ) +         
  Sum(CAST(ISNULL([Read].NonCashable_EFT_IN,0) AS FLOAT) ) +         
 Sum( CAST(ISNULL([Read].Cashable_EFT_IN,0) AS FLOAT) ) )/ 100 as EFTIn, -- EFT In          
            
 (Sum(((CAST(ISNULL(dbo.[Read].READ_RDC_TRUE_COIN_IN,0) AS FLOAT) ))) * Installation.Installation_Token_Value) / 100              
+          
 CASE WHEN Site.Region = 'US' Then                
           
 Sum( CAST(ISNULL(CASH_IN_100p,0) AS FLOAT) ) +                                
(Sum( CAST(ISNULL(CASH_IN_500p,0) AS FLOAT) ) * 5) +           
(Sum( CAST(ISNULL(CASH_IN_1000p,0) AS FLOAT) ) * 10) +           
(Sum( CAST(ISNULL(CASH_IN_2000p,0) AS FLOAT) ) * 20) +           
(Sum( CAST(ISNULL(CASH_IN_5000p,0) AS FLOAT) ) * 50) +            
(Sum( CAST(ISNULL(CASH_IN_10000p,0) AS FLOAT) ) * 100) +           
(Sum( CAST(ISNULL(CASH_IN_20000p,0) AS FLOAT) ) * 200) +    
(Sum( CAST(ISNULL(CASH_IN_50000p,0) AS FLOAT) ) * 500) +          
(Sum( CAST(ISNULL(CASH_IN_100000p,0) AS FLOAT) ) * 1000)             
  WHEN Site.Region = 'AR' Then            
                       
(Sum( CAST(ISNULL(CASH_IN_200p,0) AS FLOAT) ) * 2) +                                 
(Sum( CAST(ISNULL(CASH_IN_500p,0) AS FLOAT) ) * 5) +           
(Sum( CAST(ISNULL(CASH_IN_1000p,0) AS FLOAT) ) * 10) +           
(Sum( CAST(ISNULL(CASH_IN_2000p,0) AS FLOAT) ) * 20) +           
(Sum( CAST(ISNULL(CASH_IN_5000p,0) AS FLOAT) ) * 50) +            
(Sum( CAST(ISNULL(CASH_IN_10000p,0) AS FLOAT) ) * 100) +           
(Sum( CAST(ISNULL(CASH_IN_20000p,0) AS FLOAT) ) * 200) +           
(Sum( CAST(ISNULL(CASH_IN_50000p,0) AS FLOAT) ) * 500) +          
(Sum( CAST(ISNULL(CASH_IN_100000p,0) AS FLOAT) ) * 1000)              
 ELSE                     
             
Sum( (CAST(ISNULL(CASH_IN_500p,0) AS FLOAT) ) * 5) +           
(Sum( CAST(ISNULL(CASH_IN_1000p,0) AS FLOAT) ) * 10) +           
(Sum( CAST(ISNULL(CASH_IN_2000p,0) AS FLOAT) ) * 20) +           
(Sum( CAST(ISNULL(CASH_IN_5000p,0) AS FLOAT) ) * 50) +            
(Sum( CAST(ISNULL(CASH_IN_10000p,0) AS FLOAT) ) * 100) +           
(Sum( CAST(ISNULL(CASH_IN_20000p,0) AS FLOAT) ) * 200) +           
(Sum( CAST(ISNULL(CASH_IN_50000p,0) AS FLOAT) ) * 500) +           
(Sum( CAST(ISNULL(CASH_IN_100000p,0) AS FLOAT) ) * 1000)             
            
 END                  
+    
          
  CASE WHEN @isAFTCalculationEnabled = 1 THEN (Sum(CAST(ISNULL([Read].Promo_Cashable_EFT_IN,0) AS FLOAT) ) +         
                                             Sum( CAST(ISNULL([Read].NonCashable_EFT_IN,0) AS FLOAT) ) +         
                                              Sum(CAST(ISNULL([Read].Cashable_EFT_IN,0) AS FLOAT) ))/100          
  ELSE 0          
  END          
           
 +          
 Sum(ISNULL(dbo.[Read].Read_Ticket_In_Suspense, 0))/100          
 +           
 Sum(CAST(ISNULL([Read].TICKETS_INSERTED_NONCASHABLE_VALUE, 0) AS FLOAT) )/100 AS TotalCashIn,            
--            
 Sum(ISNULL(dbo.[Read].READ_TICKET, 0)/100 )AS TicketsOut,             
--          
 Sum(CAST(ISNULL([Read].TICKETS_PRINTED_NONCASHABLE_VALUE, 0) AS FLOAT) )/100 AS NonCashableVouchersOut, --Non Cashable Vouchers Out            
--            
(Sum( CAST(ISNULL(dbo.[Read].READ_HANDPAY,0) AS FLOAT)) * Installation.Installation_Price_Per_Play ) /100 AS Handpay,           
          
(Sum(CAST(ISNULL([Read].Promo_Cashable_EFT_OUT,0) AS FLOAT) )+       
Sum(CAST(ISNULL([Read].NonCashable_EFT_OUT,0) AS FLOAT) )+         
Sum(CAST(ISNULL([Read].Cashable_EFT_OUT,0) AS FLOAT) )) / 100 as EFTOut,  -- EFT Out          
            
((Sum((CAST(ISNULL(dbo.[Read].READ_RDC_TRUE_COIN_OUT,0) AS FLOAT) )) * Installation.Installation_Token_Value)             
           
+Sum((CAST(ISNULL(dbo.[Read].READ_HANDPAY,0) AS FLOAT))  * Installation.Installation_Price_Per_Play)          
           
+             
          
  CASE WHEN @isAFTCalculationEnabled = 1 THEN Sum(CAST(ISNULL([Read].Promo_Cashable_EFT_OUT,0) AS FLOAT) ) +        
                                              Sum(CAST(ISNULL([Read].NonCashable_EFT_OUT,0) AS FLOAT) ) +          
  Sum(CAST(ISNULL([Read].Cashable_EFT_OUT,0) AS FLOAT)  )         
  ELSE 0          
  END          
          
 +            
 Sum(ISNULL(dbo.[Read].READ_TICKET, 0) )       
       +     
 Sum(CAST(ISNULL([Read].TICKETS_PRINTED_NONCASHABLE_VALUE, 0) AS FLOAT)) ) / 100 AS TotalCashOut,  -- Total CashOut          
               
 ((Sum((CAST(ISNULL(READ_RDC_TRUE_COIN_IN,0) AS FLOAT) ) * Installation.Installation_Token_Value) / 100)            
            
 +            
 CASE WHEN  Site.Region = 'US' Then                
               
 Sum( CAST(ISNULL(CASH_IN_100p,0) AS FLOAT) ) +                                 
(Sum( CAST(ISNULL(CASH_IN_500p,0) AS FLOAT) ) * 5) +            
(Sum( CAST(ISNULL(CASH_IN_1000p,0) AS FLOAT) ) * 10) +            
(Sum( CAST(ISNULL(CASH_IN_2000p,0) AS FLOAT) ) * 20) +            
(Sum( CAST(ISNULL(CASH_IN_5000p,0) AS FLOAT) ) * 50) +             
(Sum( CAST(ISNULL(CASH_IN_10000p,0) AS FLOAT) ) * 100) +            
(Sum( CAST(ISNULL(CASH_IN_20000p,0) AS FLOAT) ) * 200) +            
(Sum( CAST(ISNULL(CASH_IN_50000p,0) AS FLOAT) ) * 500) +           
(Sum( CAST(ISNULL(CASH_IN_100000p,0) AS FLOAT) ) * 1000)              
  WHEN Site.Region = 'AR' Then            
                       
(Sum( CAST(ISNULL(CASH_IN_200p,0) AS FLOAT) ) * 2) +                                  
(Sum( CAST(ISNULL(CASH_IN_500p,0) AS FLOAT) ) * 5) +            
(Sum( CAST(ISNULL(CASH_IN_1000p,0) AS FLOAT) ) * 10) +            
(Sum( CAST(ISNULL(CASH_IN_2000p,0) AS FLOAT) ) * 20) +            
(Sum( CAST(ISNULL(CASH_IN_5000p,0) AS FLOAT) ) * 50) +             
(Sum( CAST(ISNULL(CASH_IN_10000p,0) AS FLOAT) ) * 100) +            
(Sum( CAST(ISNULL(CASH_IN_20000p,0) AS FLOAT) ) * 200) +            
(Sum( CAST(ISNULL(CASH_IN_50000p,0) AS FLOAT) ) * 500) +           
(Sum( CAST(ISNULL(CASH_IN_100000p,0) AS FLOAT) ) * 1000)               
 ELSE               
(Sum( CAST(ISNULL(CASH_IN_500p,0) AS FLOAT) ) * 5) +            
(Sum( CAST(ISNULL(CASH_IN_1000p,0) AS FLOAT) ) * 10) +            
(Sum( CAST(ISNULL(CASH_IN_2000p,0) AS FLOAT) ) * 20) +            
(Sum( CAST(ISNULL(CASH_IN_5000p,0) AS FLOAT) ) * 50) +             
(Sum( CAST(ISNULL(CASH_IN_10000p,0) AS FLOAT) ) * 100) +            
(Sum( CAST(ISNULL(CASH_IN_20000p,0) AS FLOAT) ) * 200) +            
(Sum( CAST(ISNULL(CASH_IN_50000p,0) AS FLOAT) ) * 500) +            
(Sum( CAST(ISNULL(CASH_IN_100000p,0) AS FLOAT) ) * 1000)               
            
 END                  
 +            
          
  CASE WHEN @isAFTCalculationEnabled = 1 THEN (Sum(CAST(ISNULL([Read].Promo_Cashable_EFT_IN,0) AS FLOAT) )+          
                                              Sum(CAST(ISNULL([Read].NonCashable_EFT_IN,0) AS FLOAT) ) +          
                                              Sum(CAST(ISNULL([Read].Cashable_EFT_IN,0) AS FLOAT) ))/100          
  ELSE 0          
  END           
          
+              
 Sum(ISNULL(dbo.[Read].Read_Ticket_In_Suspense, 0))/100            
            
 +            
  Sum(CAST(ISNULL([Read].TICKETS_INSERTED_NONCASHABLE_VALUE, 0) AS FLOAT) )/100            
 -              
 ((Sum((CAST(ISNULL(dbo.[Read].READ_RDC_TRUE_COIN_OUT,0) AS FLOAT) )) * Installation.Installation_Token_Value)             
           
+Sum((CAST(ISNULL(dbo.[Read].READ_HANDPAY,0) AS FLOAT))  * Installation.Installation_Price_Per_Play)          
           
+             
          
  CASE WHEN @isAFTCalculationEnabled = 1 THEN Sum(CAST(ISNULL([Read].Promo_Cashable_EFT_OUT,0) AS FLOAT) ) +        
                                              Sum(CAST(ISNULL([Read].NonCashable_EFT_OUT,0) AS FLOAT) ) +          
  Sum(CAST(ISNULL([Read].Cashable_EFT_OUT,0) AS FLOAT)  )         
  ELSE 0          
  END          
          
 +            
 Sum(ISNULL(dbo.[Read].READ_TICKET, 0) )       
       +     
 Sum(CAST(ISNULL([Read].TICKETS_PRINTED_NONCASHABLE_VALUE, 0) AS FLOAT)) ) / 100  )    
 as Net, --(TotalCashin-TotalCashout)        
@currencyFormat,            
Machine.Machine_Stock_No  ,        
 @ProductVersion AS ProductVersion,          
 @ProductHeader AS ProductHeader,          
 @Criteria AS Criteria,      
 CASE WHEN Installation.Installation_End_Date IS NULL THEN 'Active'            
  WHEN Installation.Installation_End_Date IS NOT NULL THEN 'Closed'           
  Else 'Active' END AS InstallationStatus         
--INTO #temptable1             
            
FROM            
    dbo.Installation Installation   WITH (NOLOCK)          
 INNER JOIN dbo.Bar_Position Bar_Position WITH (NOLOCK) ON Installation.Bar_Position_ID = Bar_Position.Bar_Position_ID            
    INNER JOIN dbo.[Read] WITH (NOLOCK) ON Installation.Installation_ID = [Read].Installation_ID            
    INNER JOIN dbo.Machine MACHINE WITH (NOLOCK) ON Installation.Machine_ID = Machine.Machine_ID            
    INNER JOIN dbo.Site SITE WITH (NOLOCK) ON Bar_Position.Site_ID = Site.Site_ID            
 INNER JOIN dbo.Machine_Class Machine_Class WITH (NOLOCK) ON Machine.Machine_Class_ID = Machine_Class.Machine_Class_ID           
 LEFT OUTER JOIN dbo.Machine_Type Machine_Type_Category  WITH (NOLOCK) ON Machine.Machine_Category_ID = Machine_Type_Category.Machine_Type_ID              
 LEFT OUTER JOIN dbo.Zone Zone WITH (NOLOCK) ON Bar_Position.Zone_ID = Zone.Zone_ID             
    where 
		[Read].ReadDate BETWEEN  DATEADD(d, 0, DATEDIFF(d, 0, @calcStartDate)) and DATEADD(d, 0, DATEDIFF(d, 0, @calcEndDate))
		--cast ( [Read].Read_Date AS DATETIME ) between convert ( varchar(12), @calcStartDate, 106 )  and convert( varchar(12), @calcEndDate, 106 )            
            
     AND ( ( @subcompany IS NULL )               
         OR            
           ( @subcompany IS NOT NULL             
             AND            
             site.sub_company_id = @subcompany            
           )            
         )            
            
     AND ( ( @region IS NULL )               
         OR            
           ( @region IS NOT NULL             
             AND            
             site.sub_company_region_id = @region            
           )            
         )            
            
     AND ( ( @area IS NULL )               
         OR            
           ( @area IS NOT NULL             
             AND            
             site.sub_company_area_id = @area            
           )            
         )            
            
     AND ( ( @district IS NULL )               
         OR            
           ( @district IS NOT NULL             
             AND            
             site.sub_company_district_id = @district            
           )            
         )            
            
     AND ( ( @site IS NULL )               
         OR            
           ( @site IS NOT NULL             
             AND            
             site.site_id = @site            
           )            
         )  
         
         AND (@SiteIDList IS NOT NULL AND SITE.Site_ID IN(SELECT DATA FROM fnSplit(@SiteIDList,',')))     
    
Group by     
    
Installation.Installation_ID,            
Installation.Bar_Position_ID,            
Bar_Position.Bar_Position_Name,               
--CASE machine_class.machine_name WHEN 'Auto Detected' THEN 'Multi Game' ELSE machine_class.machine_name END AS machine_name,             
machine_class.machine_name,      
Zone.Zone_Name,              
Machine_Type_Category.Machine_Type_ID,             
Machine_Type_Category.Machine_Type_Code,            
Site.Site_Name,      
Site.Region,    
Installation.Installation_Token_Value,    
Installation.Installation_Price_Per_Play,      
Machine.Machine_Stock_No,    
Installation.Installation_End_Date              
            
ORDER BY    
site.site_name,            
Machine_Type_Category.Machine_Type_Code,            
Installation.Bar_Position_ID,            
Machine_Type_Category.Machine_Type_ID            
--Installation.installation_start_date     
          
DESC            
            
SELECT             
            
 Installation_ID,            
 Bar_Position_ID,            
 --Installation_No,            
 Bar_Position_Name,               
    Machine_Name,            
 Zone_Name,              
    Machine_Type_ID,             
 Machine_Type_Code,            
    Site_Name,            
 CoinsIn,            
 CoinsOut,            
 BillsIn,            
 TicketsIn,           
 NonCashableVouchersIn,          
 EFTIn,           
 TotalCashIn,            
 TicketsOut,          
 NonCashableVouchersOut,          
 EFTOut,            
 Handpay,            
 TotalCashOut,            
 Net,          
 CurrencyFormat,          
 Machine_Stock_No,      
 ProductVersion,      
 ProductHeader,      
 Criteria,    
 InstallationStatus            
            
--FROM #temptable1            
FROM @temptable  ORDER BY installation_id ASC          
            
     
--DROP TABLE #temptable1            

GO

