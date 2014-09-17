USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_CheckMissingReadData]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_CheckMissingReadData]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--------------------------------------------------------------------------         
--        
-- Description: Checks for any missing read records for the given set of conditions.        
--        
-- Inputs:     Company, SubCompany, Region, Area, District, Site, Category, StartDate, EndDate    
-- Outputs:    The following status -    
--       0 - Issue with execution of the sp.     
--       1 - Missing Read for selected conditions.       
--       2 - No Missing Read for selected conditions.          
--        
-- =======================================================================        
--         
-- Revision History        
--         
-- Renjish   28/07/09   Created       
-- Renjish   08/07/09   Modified  To check active installation and machines within the period  
---------------------------------------------------------------------------         
    
CREATE PROCEDURE [dbo].[rsp_CheckMissingReadData]    
@Company    INT = 0,    
@SubCompany INT = 0,    
@Region     INT = 0,    
@Area       INT = 0,      
@District   INT = 0,      
@Site       INT = 0,      
@Category   INT = 0,       
@StartDate  DATETIME,      
@EndDate    DATETIME,    
@Status INT OUT    
    
AS    
    
SET @Status = 0    
SET DATEFORMAT dmy 
    
IF @Company = 0    SET @Company = NULL    
IF @SubCompany = 0 SET @SubCompany = NULL    
IF @Region = 0     SET @Region = NULL    
IF @Area = 0       SET @Area = NULL    
IF @District = 0   SET @District = NULL    
IF @Site = 0       SET @Site = NULL    
IF @Category = 0   SET @Category = NULL    
  
 -- if the date is greater than today, we should only have reads for today      
  if @EndDate > getdate()      
  set @EndDate =getdate() 

  if @StartDate > getdate()
	set @StartDate =getdate();
    
--Step 1 - Get list of active assets for the period.    
--Step 2 - Calculate the number of days each asset was active for the period.    
WITH ActiveInstallations(Installation_ID,    
Install_Start_Date,    
Install_End_Date,    
Asset_Active_Days,    
Bar_Position_Name,  
Bar_Position_ID,    
Site_Name,  
Machine_ID,  
Machine_Stock_No,  
Machine_Start_Date,  
Machine_END_Date  
)    
    
AS    
    
(    
 SELECT     
 I.Installation_ID,    
 CONVERT(DATETIME,I.Installation_Start_Date,103) AS Install_Start_Date,    
 CONVERT(DATETIME,I.Installation_End_Date,103) AS Install_End_Date,     
 DATEDIFF(dd, CASE WHEN @StartDate < CONVERT(DATETIME,I.Installation_Start_Date,103)     
 THEN CONVERT(DATETIME,I.Installation_Start_Date,103) ELSE @StartDate END,     
 CASE WHEN @EndDate < ISNULL(CONVERT(DATETIME,I.Installation_End_Date,103),@EndDate)     
 THEN @EndDate ELSE ISNULL(CONVERT(DATETIME,I.Installation_End_Date,103),@EndDate) END) + 1 AS Asset_Active_Days,    
 BP.Bar_Position_Name,    
 BP.Bar_Position_ID,  
 S.Site_Name,  
 M.Machine_ID,  
 M.Machine_Stock_No,  
 M.Machine_Start_Date,  
 M.Machine_END_Date  
 FROM     
 dbo.Installation I     
 INNER JOIN dbo.Bar_Position BP on I.Bar_Position_ID = BP.Bar_Position_ID        
 INNER JOIN dbo.Machine M on I.Machine_ID = M.Machine_ID        
 INNER JOIN dbo.Machine_Class MC on M.Machine_Class_ID = MC.Machine_Class_ID        
 LEFT JOIN dbo.Machine_Type MT on M.Machine_Category_ID = MT.Machine_Type_ID    
 INNER JOIN dbo.Site S on BP.Site_ID = S.Site_ID    
 LEFT JOIN dbo.Zone Z on BP.Zone_ID = Z.Zone_ID     
 WHERE   
  
 (  
  @EndDate  >= CONVERT(DATETIME,I.Installation_Start_Date,103)        
  AND   
  (    
  @StartDate <= CONVERT(DATETIME,I.Installation_End_Date,103)   
  OR   
  ISNULL(I.Installation_End_Date,'') = ''  
  )  
 )   
   
 AND  
 (  
  @EndDate  >= CONVERT(DATETIME,M.Machine_Start_Date,103)        
  AND   
  (    
  @StartDate <= CONVERT(DATETIME,M.Machine_End_Date,103)   
  OR   
  ISNULL(M.Machine_End_Date,'') = ''  
  )  
 )   
  
 AND ( ( @SubCompany IS NULL ) OR  ( @SubCompany IS NOT NULL AND S.Sub_Company_ID = @SubCompany ) )    
 AND ( ( @Region IS NULL ) OR ( @Region IS NOT NULL AND S.Sub_Company_Region_ID = @Region  ))        
 AND ( ( @Area IS NULL ) OR ( @Area IS NOT NULL AND S.Sub_Company_Area_ID = @Area ))        
 AND ( ( @District IS NULL )OR ( @District IS NOT NULL AND S.Sub_Company_District_ID = @District ))        
 AND ( ( @Site IS NULL )OR ( @Site IS NOT NULL AND S.Site_ID = @Site ))    
  
)  

--Step 3 - Get the Read Days for each active asset    
SELECT Distinct  AI.*,    
SUM(R.Read_Days) OVER (PARTITION BY R.Installation_No)AS Asset_Read_Days  
INTO #Temp    
FROM ActiveInstallations AI    
LEFT JOIN dbo.VW_Read R ON ( AI.Installation_ID = R.Installation_No     
 AND CONVERT(DATETIME,R.Read_Date,103) BETWEEN CONVERT(DATETIME,@StartDate,103) AND CONVERT(DATETIME,@EndDate,103) )    
  
ORDER BY AI.Installation_ID DESC  
--  
--Step 4 - Check if there are any records with Read_Day and Active_Days missing.    
IF EXISTS(SELECT 1 FROM #TEMP WHERE isnull(Asset_Read_Days,0) <> isnull(Asset_Active_Days,0) AND isnull(Asset_Read_Days,0) < isnull(Asset_Active_Days,0) )    
 SET @Status=1  
	
ELSE  
 SET @Status=2  

DROP TABLE #TEMP    

GO

