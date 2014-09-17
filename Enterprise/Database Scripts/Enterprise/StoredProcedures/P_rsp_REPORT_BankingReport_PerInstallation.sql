USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_REPORT_BankingReport_PerInstallation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_REPORT_BankingReport_PerInstallation]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/**    History
	Anuradha      05/09/2011    Created   
**/    
    
CREATE  PROCEDURE [dbo].[rsp_REPORT_BankingReport_PerInstallation]      
      
  @Company    INT = 0,      
  @SubCompany INT = 0,      
  @Region     INT = 0,      
  @Area       INT = 0,      
  @District   INT = 0,      
  @Site       INT = 0,      
  @Category   INT = 0,       
  @StartDate  DateTime,      
  @EndDate    DATETIME,
  @SiteIDList VARCHAR(MAX)    
      
AS      
  DECLARE @AddShortpay VARCHAR(500)  
  SELECT @AddShortpay = setting_value FROM setting WHERE setting_name ='AddShortpayInVoucherOut'
  
  DECLARE @Criteria VARCHAR(1000)            
            
declare @companyname varchar(50)            
if (ISNULL(@company,0) <> 0)            
 select @companyname = company_name from company (NOLOCK)  where company_id = @company            
else            
 set  @companyname = '--Any--'            
 declare @denom float     
SET @denom = 0            
declare @subcompanyname varchar(50)            
if (ISNULL(@subcompany,0) <> 0)            
 select @subcompanyname = sub_company_name from sub_company (NOLOCK) where sub_company_id = @subcompany            
else            
 set  @subcompanyname = '--Any--'            
            
declare @sitename varchar(50)            
if (ISNULL(@site,0) <> 0)            
 select @sitename = site_name from site (NOLOCK)  where site_id = @site            
else            
 set  @sitename = '--Any--'            
             
              
declare @regionname varchar(50)            
if (ISNULL(@region,0) <> 0)    
 select @regionname = Sub_Company_Region_Name from sub_company_region (NOLOCK) where Sub_Company_Region_ID = @region              
else           
 set  @regionname = '--Any--'            
   
declare @areaname varchar(50)            
if (ISNULL(@area,0) <> 0)  
 select @areaname = Sub_Company_Area_Name  from sub_company_area (NOLOCK)  where Sub_Company_Area_ID = @area   
else           
 set  @areaname = '--Any--'            
          
declare @districtname varchar(50)            
if (ISNULL(@district,0) <> 0)   
 select @districtname = Sub_Company_District_Name    from sub_company_district (NOLOCK) where Sub_Company_District_ID = @district 
else            
 set  @districtname = '--Any--'           
 
  declare @categoryname varchar(50)            
if (ISNULL(@category,0) <> 0)       
 select @categoryname = Machine_Type_Code from machine_type(NOLOCK)   where Machine_Type_ID = @category 
else  
 set  @categoryname = '--Any--'            
       
SET @criteria = 'Company: ' + cast(@companyname as varchar(50))+ ' | ' +                
      'Sub Company: ' + cast(@subcompanyname as varchar(50))+ ' | ' +   
  'Region: ' + cast(@regionname as varchar(50))+ ' | ' +  
   'Area: ' + cast(@areaname as varchar(50))+ ' | ' +  
   'District: ' + cast(@districtname as varchar(50))+ ' | ' +     
      'Site: ' + cast(@sitename as varchar(50))+ ' | ' +                
      'Category: ' + @categoryname  +  'Grouping on (Site,machinetype)'    
                  
    
  
Declare @eRegion varchar(2)           
  Select @eRegion=UPPER(RIGHT(System_Parameter_Region_Culture,2)) from System_Parameters (NOLOCK)      
    
   declare @ProductVersion  varchar(50)            
  SELECT TOP 1 @ProductVersion = 'BMC Version : ' + VersionName       
    FROM VersionHistory (NOLOCK)             
ORDER BY VersionDate DESC            
               
  declare @ProductHeader  varchar(50)            
  SELECT @ProductHeader = setting_value     
    FROM dbo.Setting  (NOLOCK)           
   WHERE Setting_Name = 'BMC_Reports_Header'           
            
  declare @currencyformatting varchar(20)            
 SELECT @currencyFormatting = setting_value    
 FROM Setting (NOLOCK)  WHERE Setting_Name = 'BMC_Reports_Language'    
            
         
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
      
  select   
         company.company_id,      
         site.sub_company_id,      
         site.sub_company_region_id,      
         site.sub_company_area_id,      
         site.sub_company_district_id,      
         site.site_id,      
         site.site_name,     
         i.installation_id    ,   
         machine_type_id = CASE WHEN cat.machine_type_id IS NOT NULL THEN cat.machine_type_id ELSE 0 END,       
         machine_type_code = CASE WHEN cat.machine_type_code IS NOT NULL THEN cat.machine_type_code ELSE @NOTSET END,      
         machine_type_description = CASE WHEN cat.machine_type_description IS NOT NULL THEN cat.machine_type_description ELSE @NOTSET END,      
         machine.machine_stock_no,      
      
         vw_collectiondata.machinename,      
         vw_collectiondata.posname,      

		Sum(vw_collectiondata.cashcollected) as CashCollected,      
		Sum( vw_collectiondata.Cash_collected_50000p ) as Cash_collected_50000p,      
		Sum( vw_collectiondata.Cash_collected_20000p) as Cash_collected_20000p,      
		Sum( vw_collectiondata.Cash_collected_10000p) as Cash_collected_10000p,      
		Sum( vw_collectiondata.Cash_collected_5000p) as Cash_collected_5000p,      
		Sum( vw_collectiondata.Cash_collected_2000p) as Cash_collected_2000p,      
		Sum( vw_collectiondata.Cash_collected_1000p) as Cash_collected_1000p,      
		Sum( vw_collectiondata.Cash_collected_500p) as Cash_collected_500p,      
        
		 CASE       
		 WHEN site.Region ='AR' THEN       
		  Sum(vw_collectiondata.Cash_collected_200p )    
		 ELSE      
		  Sum(vw_collectiondata.Cash_collected_100p ) 
		 END      
		as Cash_collected_100p,        
		      
		 Sum(vw_collectiondata.declared_tickets) as declared_tickets,        
		 Sum(vw_collectiondata.shortpay) as shortpay,         
		 --Included Progressive_Value_Declared with declared handpay      
		 Sum(vw_collectiondata.dechandpay )as dechandpay, 
		 Sum(vw_collectiondata.manualrefills) as manualrefills,        
		 Sum(vw_collectiondata.refunds) as refunds, 
		 --test      
		 Sum(vw_collectiondata.tickets_printed+CASE WHEN ISNULL(@AddShortpay,'FALSE')= 'FALSE' THEN 0 ELSE vw_collectiondata.Shortpay end)as tickets_printed,    
		 Sum(vw_collectiondata.handpay_var) as handpay_var,     
		 Sum(vw_collectiondata.note_var) as note_var,   
		 Sum(vw_collectiondata.coin_var) as coin_var,    
		 Sum(vw_collectiondata.rdc_tickets_in) as rdc_tickets_in,         
		 Sum(vw_collectiondata.rdc_tickets_out) as rdc_tickets_out,         
		 Sum(vw_collectiondata.DecWinloss ) as cash_take ,
		 Sum((vw_collectiondata.DecWinloss - vw_collectiondata.MeterWinloss) ) as take_var,         
		 Sum(vw_collectiondata.void) as void,      
		 -- Modified by A.Vinod Kumar for CR92027       
		 Sum((dbo.GetEFTIN(ISNULL(vw_collectiondata.Collection_ID,0)))/100 ) AS EftIn,      
		 Sum(((vw_collectiondata.EFTOut) / 100)) AS EFTOut,      
		 Sum(vw_collectiondata.RDC_TICKETS_INSERTED_NONCASHABLE_VALUE) AS NonCashableVoucherIn,      
		 Sum(vw_collectiondata.RDC_TICKETS_PRINTED_NONCASHABLE_VALUE) AS NonCashableVoucherOut,      
		 Sum(vw_collectiondata.rdc_coins_out) as CoinsOut,  
		 @ProductVersion AS ProductVersion,  
		 @ProductHeader AS ProductHeader,  
		 @Criteria AS Criteria,
		 CASE WHEN i.Installation_End_Date IS NULL THEN 'Active'    
		  WHEN i.Installation_End_Date IS NOT NULL THEN 'Closed'   
		  Else 'Active' END AS InstallationStatus        
      
    from vw_collectiondata (NOLOCK)      
      
    join installation   i  (NOLOCK)  
      on vw_collectiondata.installation_id = i.installation_id      
      
    join machine      (NOLOCK) 
      on i.machine_id = machine.machine_id      
      
	LEFT join machine_type cat  (NOLOCK)     
      on cat.machine_type_id = machine.machine_category_id       
      
    join site      (NOLOCK) 
      on site.site_id = vw_collectiondata.site_id      
      
    join sub_company   (NOLOCK)    
      on site.sub_company_id = sub_company.sub_company_id      
      
    join company      (NOLOCK) 
      on sub_company.company_id = company.company_id      
      
 where cast ( collection_date as datetime ) between convert ( varchar(20), @calcStartDate, 106 )  and convert( varchar(20), @calcEndDate, 106 )      
  --where cast ( collection_date as datetime ) between @calcStartDate   and  @calcEndDate       
     AND ( ( @company IS NULL )         
         OR      
           ( @company IS NOT NULL       
             AND      
             company.company_id = @company       
           )      
         )      
      
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
      AND (@SiteIDList IS NOT NULL AND site.Site_ID IN(SELECT DATA FROM fnSplit(@SiteIDList,',')))
      
     AND ( ( @category IS NULL )         
         OR      
           ( @category IS NOT NULL      
             AND      
             cat.machine_type_id = @category      
           )      
         )      
 
group by 
		i.installation_id , 
		company.company_id   ,
		site.sub_company_id,      
		site.sub_company_region_id,      
		site.sub_company_area_id,      
		site.sub_company_district_id,      
		site.site_id,      
		site.site_name,      
		cat.machine_type_id,    
		cat.machine_type_code,
		machine_type_description,
		machine.Machine_Stock_No,
		vw_collectiondata.MachineName,
		vw_collectiondata.posName ,
		i.Installation_End_Date,
		site.Region
 
  order BY  
		i.installation_id    ,      
		company.company_id,      
		site.sub_company_id,      
		site.sub_company_region_id,      
		site.sub_company_area_id,      
		site.sub_company_district_id,      
		site.site_id,      
		site.site_name,      
		cat.machine_type_id,    
		vw_collectiondata.posName

GO

