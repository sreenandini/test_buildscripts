USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetALLReportMenu]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetALLReportMenu]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- OUTPUT --Show Report menu           
-- Revision History    
-- Vineetha Mathew 22/03/2010  Created    
-- Vineetha Mathew 30/04/2010  Modified added promoreports setting   
-- Vineetha Mathew 01/06/2010  Modified to check promo reports 
-- ======================================================================= 
CREATE PROCEDURE [dbo].[rsp_GetALLReportMenu]
AS
BEGIN

DECLARE @client as varchar(100)    
SET @client=''    
 EXEC rsp_GetSetting NULL, 'CLIENT', '', @client OUTPUT    
  
DECLARE @powerpromo as varchar(10)    
SET @powerpromo=null    
 EXEC rsp_GetSetting NULL, 'ISPOWERPROMOREPORTSREQUIRED', '', @powerpromo OUTPUT    
  
DECLARE @SGVISetting as varchar(100)    
SET @SGVISetting=''    
 EXEC rsp_GetSetting NULL, 'SGVI_Enabled', '', @SGVISetting OUTPUT    

DECLARE @IsEmployeeTrackingEnabled as varchar(100)
SET @IsEmployeeTrackingEnabled=''
	EXEC rsp_GetSetting NULL, 'IsEmployeeCardTrackingEnabled', '', @IsEmployeeTrackingEnabled OUTPUT

DECLARE @IsGameCappingEnabled as varchar(10)
SET @IsGameCappingEnabled=''
	EXEC rsp_GetSetting NULL, 'IsGameCappingEnabled', '', @IsGameCappingEnabled OUTPUT

DECLARE @IsStackerFeatureEnabled as varchar(100)
SET @IsStackerFeatureEnabled=''
	EXEC rsp_GetSetting NULL, 'StackerFeature', '', @IsStackerFeatureEnabled OUTPUT

DECLARE @IsDropScheduleAlertEnabled as varchar(100)
SET @IsDropScheduleAlertEnabled=''
	EXEC rsp_GetSetting NULL, 'DropScheduleAlert', '', @IsDropScheduleAlertEnabled OUTPUT
	
DECLARE @IsSiteLicensingEnabled as varchar(100)
SET @IsSiteLicensingEnabled=''
	EXEC rsp_GetSetting NULL, 'IsSiteLicensingEnabled', '', @IsSiteLicensingEnabled OUTPUT

DECLARE @IsVaultEnabled as varchar(100)
SET @IsVaultEnabled = ''
	EXEC rsp_GetSetting NULL, 'IsVaultEnabled', '', @IsVaultEnabled OUTPUT

DECLARE @LiquidationProfitShare AS VARCHAR(10)      
	SET @LiquidationProfitShare = ''      
	EXEC rsp_GetSetting NULL, 'LiquidationProfitShare', '', @LiquidationProfitShare OUTPUT
	
DECLARE @IsCrossTicketingEnabled AS VARCHAR(10)      
	SET @IsCrossTicketingEnabled = ''      
	EXEC rsp_GetSetting NULL, 'IsCrossTicketingEnabled', '', @IsCrossTicketingEnabled OUTPUT
--
IF (ISNULL(@client,'')='')    
 SET @client=null    
ELSE IF @client='0'    
 SET @client=null    
   SELECT DISTINCT 
          RM.Client,
          RM.ReportID,
          RMParent.ReportName AS ParentName,
          RM.ReportMenuID AS ParentID,
          RM.ReportName,
          RM.ReportName AS ReportDescription,
          RM.ShowPowerPromoReports
   FROM   ReportsMenu RM
          LEFT  JOIN ReportsMenu RMParent
               ON  ISNULL(RMParent.ReportID, '') = ISNULL(RM.ReportMenuID, '')
   WHERE  ISNULL(RM.Client, '') = '' 
          --AND ISNULL(UPPER(RM.MS_ProcedureUsed),'') <> UPPER('rsp_REPORT_BankingReport')
          AND ISNULL(RMParent.Client, '') = ''
          AND LTRIM(RTRIM(RMParent.ReportName)) <> 'Main Menu'
          AND (
                  UPPER(LTRIM(RTRIM(RM.ShowPowerPromoReports))) = 'FALSE'
                  OR RM.ShowPowerPromoReports IS NULL
              )
          AND UPPER(ISNULL(RMParent.Client, '')) <> UPPER(ISNULL(@client, ''))
          AND LTRIM(RTRIM(RM.ReportName)) 
              NOT IN ('Employee Card List Report', 
                     'Employee Card Sessions Report', 
                     'Stacker Level Details Report', 
                     'Drop Schedule Details Report', 'License History Report', 
                     'Site Licensing Reports', 
                     'License Expiry Report', 'Total Funds In Summary Report', 
                     'Total Funds In Detail Report', 
                     'Cash Dispenser Inventory Status Report', 
                     'Cash Dispenser Cassettes Inventory Status', 
                     'Cash Dispenser Cassette Accounting Detail', 
                     'Cash Dispenser Configuration Details', 
                     'Cash Dispenser Drop Report', 
                     'Cash Dispenser Inventory Status Report', 
                     'Cash Dispenser Level Details', 
                     'Cash Dispenser Transaction Details', 
                     'Cash Dispenser Variance Report', 
                     'Capped Game Summary Report', 'Capped Game List Report', 
                     'Fixed Expense Details Report', 
                     'Period-End Liquidation Revenue Report', 
                     'Cash Dispenser Accounting Report', 
                     'Cross Property Liability Transfer Summary Report', 
                     'Cross Property Liability Transfer Details Report', 
                     'Cross Property Ticket Analysis Report')
   UNION     
   SELECT DISTINCT 
          RM.Client,
          RM.ReportID,
          RMParent.ReportName AS ParentName,
          RM.ReportMenuID AS ParentID,
          RM.ReportName,
          RM.ReportName AS ReportDescription,
          RM.ShowPowerPromoReports
   FROM   ReportsMenu RM
          LEFT  JOIN ReportsMenu RMParent
               ON  ISNULL(RMParent.ReportID, '') = ISNULL(RM.ReportMenuID, '')
   WHERE  ISNULL(UPPER(RM.Client), '') = ISNULL(UPPER(@client), '')
          AND LTRIM(RTRIM(RMParent.ReportName)) <> 'Main Menu'
          AND (
                  ISNULL(RM.ShowPowerPromoReports, '') = ISNULL(UPPER(@powerpromo), '')
                  AND UPPER(@powerpromo) = 'TRUE'
              )    
   UNION     
   SELECT DISTINCT 
          RM.Client,
          RM.ReportID,
          RMParent.ReportName AS ParentName,
          RM.ReportMenuID AS ParentID,
          RM.ReportName,
          RM.ReportName AS ReportDescription,
          RM.ShowPowerPromoReports
   FROM   ReportsMenu RM
          LEFT  JOIN ReportsMenu RMParent
               ON  ISNULL(RMParent.ReportID, '') = ISNULL(RM.ReportMenuID, '')
   WHERE  ISNULL(UPPER(RM.Client), '') = ISNULL(UPPER(@client), '')
          AND LTRIM(RTRIM(RMParent.ReportName)) <> 'Main Menu'
          AND ISNULL(RMParent.Client, '') = @Client
          AND @client = 'SGVI'
          AND @SGVISetting = 'TRUE'  
   UNION       
   SELECT DISTINCT 
          RM.Client,
          RM.ReportID,
          RMParent.ReportName AS ParentName,
          RM.ReportMenuID AS ParentID,
          RM.ReportName,
          RM.ReportName AS ReportDescription,
          RM.ShowPowerPromoReports
   FROM   ReportsMenu RM
          INNER  JOIN ReportsMenu RMParent
               ON  ISNULL(RMParent.ReportID, '') = ISNULL(RM.ReportMenuID, '')
   WHERE  LTRIM(RTRIM(RM.ReportName)) = 'Accounting Machine Win/Loss Report'  
   UNION	
   SELECT DISTINCT 
          RM.Client,
          RM.ReportID,
          RMParent.ReportName AS ParentName,
          RM.ReportMenuID AS ParentID,
          RM.ReportName,
          RM.ReportName AS ReportDescription,
          RM.ShowPowerPromoReports
   FROM   ReportsMenu RM
          INNER  JOIN ReportsMenu RMParent
               ON  ISNULL(RMParent.ReportID, '') = ISNULL(RM.ReportMenuID, '')
   WHERE  LTRIM(RTRIM(RM.ReportName)) IN ('Employee Card Sessions Report', 
                                         'Employee Card List Report')
          AND @IsEmployeeTrackingEnabled = 'True'   
   UNION	
   SELECT DISTINCT 
          RM.Client,
          RM.ReportID,
          RMParent.ReportName AS ParentName,
          RM.ReportMenuID AS ParentID,
          RM.ReportName,
          RM.ReportName AS ReportDescription,
          RM.ShowPowerPromoReports
   FROM   ReportsMenu RM
          INNER  JOIN ReportsMenu RMParent
               ON  ISNULL(RMParent.ReportID, '') = ISNULL(RM.ReportMenuID, '')
   WHERE  LTRIM(RTRIM(RM.ReportName)) IN ('Fixed Expense Details Report', 
                                         'Period-End Liquidation Revenue Report')
          AND @LiquidationProfitShare = 'True'
   UNION	
   SELECT DISTINCT 
          RM.Client,
          RM.ReportID,
          RMParent.ReportName AS ParentName,
          RM.ReportMenuID AS ParentID,
          RM.ReportName,
          RM.ReportName AS ReportDescription,
          RM.ShowPowerPromoReports
   FROM   ReportsMenu RM
          INNER  JOIN ReportsMenu RMParent
               ON  ISNULL(RMParent.ReportID, '') = ISNULL(RM.ReportMenuID, '')
   WHERE  LTRIM(RTRIM(RM.ReportName)) = 'Stacker Level Details Report'
          AND @IsStackerFeatureEnabled = 'True'  
   UNION	
   SELECT DISTINCT 
          RM.Client,
          RM.ReportID,
          RMParent.ReportName AS ParentName,
          RM.ReportMenuID AS ParentID,
          RM.ReportName,
          RM.ReportName AS ReportDescription,
          RM.ShowPowerPromoReports
   FROM   ReportsMenu RM
          INNER  JOIN ReportsMenu RMParent
               ON  ISNULL(RMParent.ReportID, '') = ISNULL(RM.ReportMenuID, '')
   WHERE  LTRIM(RTRIM(RM.ReportName)) = 'Drop Schedule Details Report'
          AND @IsStackerFeatureEnabled = 'True'
          AND @IsDropScheduleAlertEnabled = 'True'  	
   UNION
   SELECT DISTINCT 
          RM.Client,
          RM.ReportID,
          RMParent.ReportName AS ParentName,
          RM.ReportMenuID AS ParentID,
          RM.ReportName,
          RM.ReportName AS ReportDescription,
          RM.ShowPowerPromoReports
   FROM   ReportsMenu RM
          INNER  JOIN ReportsMenu RMParent
               ON  ISNULL(RMParent.ReportID, '') = ISNULL(RM.ReportMenuID, '')
   WHERE  LTRIM(RTRIM(RM.ReportName)) IN ('Site Licensing Reports', 
                                         'License History Report',                                         
                                         'License Expiry Report')
          AND @IsSiteLicensingEnabled = 'True'  
   UNION
   SELECT DISTINCT 
          RM.Client,
          RM.ReportID,
          RMParent.ReportName AS ParentName,
          RM.ReportMenuID AS ParentID,
          RM.ReportName,
          RM.ReportName AS ReportDescription,
          RM.ShowPowerPromoReports
   FROM   ReportsMenu RM
          INNER  JOIN ReportsMenu RMParent
               ON  ISNULL(RMParent.ReportID, '') = ISNULL(RM.ReportMenuID, '')
   WHERE  LTRIM(RTRIM(RM.ReportName)) IN ('Cash Dispenser Configuration Details', 
                                         'Cash Dispenser Cassette Accounting Detail', 
                                         'Cash Dispenser Cassettes Inventory Status', 
                                         'Cash Dispenser Drop Report', 
                                         'Cash Dispenser Inventory Status Report', 
                                         'Cash Dispenser Level Details', 
                                         'Cash Dispenser Transaction Details', 
                                         'Cash Dispenser Variance Report', 
                                         'Total Funds In Detail Report', 
                                         'Total Funds In Summary Report', 
                                         'Cash Dispenser Accounting Report')
          AND @IsVaultEnabled = 'True'
   UNION	
   SELECT DISTINCT 
          RM.Client,
          RM.ReportID,
          RMParent.ReportName AS ParentName,
          RM.ReportMenuID AS ParentID,
          RM.ReportName,
          RM.ReportName AS ReportDescription,
          RM.ShowPowerPromoReports
   FROM   ReportsMenu RM
          INNER  JOIN ReportsMenu RMParent
               ON  ISNULL(RMParent.ReportID, '') = ISNULL(RM.ReportMenuID, '')
   WHERE  LTRIM(RTRIM(RM.ReportName)) IN ('Capped Game Summary Report', 'Capped Game List Report')
          AND @IsGameCappingEnabled = 'True'
   UNION	
   SELECT DISTINCT 
          RM.Client,
          RM.ReportID,
          RMParent.ReportName AS ParentName,
          RM.ReportMenuID AS ParentID,
          RM.ReportName,
          RM.ReportName AS ReportDescription,
          RM.ShowPowerPromoReports
   FROM   ReportsMenu RM
          INNER  JOIN ReportsMenu RMParent
               ON  ISNULL(RMParent.ReportID, '') = ISNULL(RM.ReportMenuID, '')
   WHERE  LTRIM(RTRIM(RM.ReportName)) IN (
                                         'Cross Property Liability Transfer Summary Report', 
                                         'Cross Property Liability Transfer Details Report', 
                                         'Cross Property Ticket Analysis Report')
          AND @IsCrossTicketingEnabled = 'True'
   ORDER BY
          RM.ReportMenuID 
          END    
   GO