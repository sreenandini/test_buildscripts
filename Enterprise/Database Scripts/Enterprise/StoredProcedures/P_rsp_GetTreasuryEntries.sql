USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetTreasuryEntries]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetTreasuryEntries]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /*  
--------------------------------------------------------------------------     
--    
-- Description: Retrieves all the treasury entries(Handpays,Jackpots  
-- and Progressive Handpays) from Treasury  
--    
-- Inputs:      See inputs    
--    
-- Outputs:         0 - All Ok    
--              OTHER - SQL Error    
--    
-- =======================================================================    
--     
-- Revision History    
--     
-- Anuradha   07/07/2008   Created  -  Retrieves all the treasury entries  
-- Anuradha   25/07/2008   Modified -  Changed the condition for subcompany  
-- Anuradha J 10-03-2009   Modified  Included Treasury_Actual_Date    
-- Anuradha   23-03-2009   Modified  Ordered by Treasury_Actual_Date   

exec [rsp_GetTreasuryEntries] 0,0,0,0,0,0,'11 Apr 2011 06:00:00','15 Apr 2011 06:00:00'
---------------------------------------------------------------------------    
*/          
          
          
CREATE PROCEDURE [dbo].[rsp_GetTreasuryEntries]               
( 
	@Company    INT = 0,          
	@SubCompany INT = 0,          
	@Region     INT = 0,          
	@Area       INT = 0,          
	@District   INT = 0,          
	@Site       INT = 0,
	@StartDate AS datetime,              
	@EndDate AS datetime)          
              
AS              
            
 IF @company = 0    SET @company = NULL          
  IF @subcompany = 0 SET @subcompany = NULL          
  IF @region = 0     SET @region = NULL          
  IF @area = 0       SET @area = NULL          
  IF @district = 0   SET @district = NULL          
  IF @site = 0       SET @site = NULL             
               
  DECLARE @TreasuryLimit as int
SET @TreasuryLimit=''    
 EXEC rsp_GetSetting NULL, 'TreasuryLimitForMajorPrizes', '', @TreasuryLimit OUTPUT    
           
--Get subcompany, Site and treasury details based on Installation.              
SELECT               
[SUB_COMPANY].SUB_COMPANY_NAME AS [SUBCOMPANY],              
[SITE].SITE_NAME AS [SITE],              
MACHINE.MACHINE_STOCK_NO as Machine_ID,MACHINE_CLASS.MACHINE_NAME,              
--CONVERT(VARCHAR,CONVERT(DATETIME,TREASURY_DATE,103),101)          
-- + '   ' + CONVERT(VARCHAR,CONVERT(DATETIME,TREASURY_TIME,113),108) AS [HIGH PRIZE DATE],   
  
CASE   
 WHEN (TREASURY_ACTUAL_DATE IS NULL) THEN   
  CONVERT(VARCHAR,CONVERT(DATETIME,TREASURY_DATE,103),101)          
  + '   ' + CONVERT(VARCHAR,CONVERT(DATETIME,TREASURY_TIME,113),108)  
 ELSE  
  CONVERT(DATETIME,TREASURY_ACTUAL_DATE,103)  
 END         
  AS [HIGH PRIZE DATE],              
TREASURY_AMOUNT ,            
--Get the treasury Type            
CASE TREASURY_TYPE              
WHEN 'ATTENDANTPAY CREDIT' THEN  'SW'              
WHEN 'ATTENDANTPAY Jackpot' THEN 'SW'              
WHEN 'PROGRESSIVE' THEN  'JP'              
END               
AS [STATUS]              
              
FROM              
TREASURY_ENTRY              
INNER JOIN                
INSTALLATION                  
ON TREASURY_ENTRY.INSTALLATION_ID=INSTALLATION.INSTALLATION_ID                  
INNER JOIN BAR_POSITION                  
ON                  
INSTALLATION.BAR_POSITION_ID=BAR_POSITION.BAR_POSITION_ID            
inner join                 
[SITE]                
ON                
[SITE].SITE_ID=BAR_POSITION.SITE_ID               
INNER JOIN                
SUB_COMPANY                
ON                
[SITE].SUB_COMPANY_ID=SUB_COMPANY.SUB_COMPANY_ID                    
INNER JOIN           
Company c ON c.Company_ID = SUB_COMPANY.company_ID           
INNER JOIN                
MACHINE                  
ON                  
INSTALLATION.MACHINE_ID=MACHINE.MACHINE_ID                  
INNER JOIN                  
MACHINE_CLASS                  
ON                  
MACHINE.MACHINE_CLASS_ID=MACHINE_CLASS.MACHINE_CLASS_ID                
--Get treasury entries for the period selected.              
WHERE               
(          
TREASURY_AMOUNT >= @TreasuryLimit AND        
--CONVERT(DATETIME, TREASURY_DATE + ' ' + TREASURY_TIME, 103) BETWEEN           
--CONVERT(DATETIME, @startdate + ' ' + @starttime, 103) AND           
--CONVERT(DATETIME, @enddate + ' ' + @endtime, 103)     
  
  
--Treasury_Actual_Date Between CONVERT(DATETIME, @startdate + ' ' + @starttime, 103) AND   
--CONVERT(DATETIME, @enddate + ' ' + @endtime, 103)   

Treasury_Actual_Date Between  @startdate  AND  @enddate     
          
AND           
          
(TREASURY_TYPE LIKE '%ATTENDANTPAY CREDIT%' OR              
TREASURY_TYPE LIKE '%JACKPOT%' OR              
TREASURY_TYPE LIKE'%PROG%')            
          
   AND ( ( @company IS NULL )             
         OR          
   ( @company IS NOT NULL           
             AND          
             c.company_id = @company           
             
           )          
         )          
          
     AND ( ( @subcompany IS NULL )             
         OR          
           ( @subcompany IS NOT NULL           
             AND          
             SUB_COMPANY.sub_company_id = @subcompany          
           )          
         )          
          
     AND ( ( @region IS NULL )             
         OR          
           ( @region IS NOT NULL           
             AND          
             [SITE].sub_company_region_id = @region          
           )          
         )          
          
     AND ( ( @area IS NULL )             
         OR          
           ( @area IS NOT NULL           
             AND          
             [SITE].sub_company_area_id = @area          
           )          
         )          
          
     AND ( ( @site IS NULL )             
         OR          
           ( @site IS NOT NULL           
             AND          
             [SITE].site_id = @site          
           )          
         ))          
          
          
order by   
  
CASE   
 WHEN (TREASURY_ACTUAL_DATE IS NULL) THEN   
  CONVERT(VARCHAR,CONVERT(DATETIME,TREASURY_DATE,103),101)          
  + '   ' + CONVERT(VARCHAR,CONVERT(DATETIME,TREASURY_TIME,113),108)  
 ELSE  
  CONVERT(DATETIME,TREASURY_ACTUAL_DATE,103)  
 END    
desc   
  
 

GO

