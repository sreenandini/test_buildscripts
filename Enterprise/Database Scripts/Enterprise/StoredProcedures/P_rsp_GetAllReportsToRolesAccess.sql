USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetAllReportsToRolesAccess]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetAllReportsToRolesAccess]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- OUTPUT --Show Reports based on access          
-- Revision History    rsp_GetAllReportsToRolesAccess 1
-- Vineetha Mathew 22/03/2010  Created    
-- Vineetha Mathew 30/04/2010  Modified to show reports based on promoreports setting check(Union added for this) 
-- Vineetha Mathew 18/05/2010  Modified not to show Audit Report on report menu
-- Vineetha Mathew 18/05/2010  Modified reverted the previous change
-- ======================================================================= 

CREATE PROCEDURE [dbo].[rsp_GetAllReportsToRolesAccess]
(@SecurityRoleID int)    
    
AS      
BEGIN      
DECLARE @powerpromo as varchar(10)      
SET @powerpromo=''      
 EXEC rsp_GetSetting NULL, 'ISPOWERPROMOREPORTSREQUIRED', '', @powerpromo OUTPUT      
      
    
    
DECLARE @SGVISetting as varchar(100)      
SET @SGVISetting=''      
 EXEC rsp_GetSetting NULL, 'SGVI_Enabled', '', @SGVISetting OUTPUT      


   -- select @SGVISetting
DECLARE @client as varchar(100)      
SET @client=''      
 EXEC rsp_GetSetting NULL, 'CLIENT', '', @client OUTPUT      
--select @client    

 
DECLARE @IsEmployeeTrackingEnabled AS VARCHAR(100)    
SET @IsEmployeeTrackingEnabled = ''    
EXEC rsp_GetSetting NULL, 'IsEmployeeCardTrackingEnabled', '', @IsEmployeeTrackingEnabled OUTPUT

DECLARE @IsGameCappingEnabled AS VARCHAR(10)      
SET @IsGameCappingEnabled = ''      
EXEC rsp_GetSetting NULL, 'IsGameCappingEnabled', '', @IsGameCappingEnabled OUTPUT

DECLARE @LiquidationProfitShare AS VARCHAR(10)      
SET @LiquidationProfitShare = ''      
EXEC rsp_GetSetting NULL, 'LiquidationProfitShare', '', @LiquidationProfitShare OUTPUT


DECLARE @IsSiteLicensingEnabled AS VARCHAR(100)  
        SET @IsSiteLicensingEnabled = ''  
        EXEC rsp_GetSetting NULL, 
             'IsSiteLicensingEnabled', 
             '', 
             @IsSiteLicensingEnabled OUTPUT 


 SELECT   Distinct       
 RM.ReportID,        
 RMParent.ReportName AS ParentName,        
 RM.ReportMenuID as ParentID,        
 RM.ReportName,        
 RM.ReportName AS ReportDescription,      
 RM.[Level],      
 RM.ApplicationServer,      
 RM.ReportArgName,      
 RM.ReportStatus,      
 RM.ShowException,      
 RM.SecurityRoleID,      
 RM.MS_ProcedureUsed,
 ISNULL(RM.XMLName,'') AS XMLName ,
 ISNULL(RM.ExportExcel,0) AS ExportExcel,
 RM.IsTimeRequired   
      
 FROM         
  ReportsMenu RM           
  LEFT JOIN ReportsMenuAccess RMAccess ON RMAccess.ReportID = RM.ReportID           
  INNER  JOIN ReportsMenu  RMParent on  RMParent.ReportID = RM.ReportMenuID      
 WHERE RMAccess.SecurityRoleID= @SecurityRoleID    
 AND (UPPER(RM.ShowPowerPromoReports) = 'FALSE' OR RM.ShowPowerPromoReports IS NULL)        
--and  ISNULL(UPPER(RMParent.Client),'')= ISNULL(UPPER(@client),'')    
 AND  ISNULL(UPPER(RMParent.Client),'') <> 'SGVI'    
 AND RM.ReportArgName NOT IN ('EmployeeCardListReport','EmployeeCardSessionsReport',
							  'CappedGameSummaryReport','CappedGameListReport',
							  'Fixed Expense Details Report','Period-End Liquidation Revenue Report',
							  'Site Licensing Reports', 'License History Report', 'License Expiry Report'
)
     
UNION       
 SELECT   Distinct       
 RM.ReportID,        
 RMParent.ReportName AS ParentName,        
 RM.ReportMenuID as ParentID,        
 RM.ReportName,        
 RM.ReportName AS ReportDescription,      
 RM.[Level],      
 RM.ApplicationServer,      
 RM.ReportArgName,      
 RM.ReportStatus,      
 RM.ShowException,      
 RM.SecurityRoleID,      
 RM.MS_ProcedureUsed,
 ISNULL(RM.XMLName,'') AS XMLName ,
 ISNULL(RM.ExportExcel,0) AS ExportExcel,
 RM.IsTimeRequired         
      
 FROM         
  ReportsMenu RM           
  LEFT JOIN ReportsMenuAccess RMAccess ON RMAccess.ReportID = RM.ReportID           
  INNER  JOIN ReportsMenu  RMParent on  RMParent.ReportID = RM.ReportMenuID      
 WHERE RMAccess.SecurityRoleID=@SecurityRoleID     
and (ISNULL(RM.ShowPowerPromoReports,'') = ISNULL(@powerpromo,'')  AND UPPER(@powerpromo)='TRUE')
 and  ISNULL(UPPER(RMParent.Client),'') <> 'SGVI'        
  --AND (RM.ReportArgName<>'AUDITTRAIL')      
AND RM.ReportArgName NOT IN ('EmployeeCardListReport','EmployeeCardSessionsReport',
							  'CappedGameSummaryReport','CappedGameListReport',
							  'Fixed Expense Details Report','Period-End Liquidation Revenue Report',
							  'Site Licensing Reports', 'License History Report', 'License Expiry Report')
    
Union   
 
 SELECT   Distinct       
 RM.ReportID,        
RMParent.ReportName AS ParentName,        
 --RMParent.Client ,

 RM.ReportMenuID as ParentID,        
 RM.ReportName,        
 RM.ReportName AS ReportDescription,      
 RM.[Level],      
 RM.ApplicationServer,      
 RM.ReportArgName,      
 RM.ReportStatus,      
 RM.ShowException,      
 RM.SecurityRoleID,      
 RM.MS_ProcedureUsed,
 ISNULL(RM.XMLName,'') AS XMLName ,
 ISNULL(RM.ExportExcel,0) AS ExportExcel,
 RM.IsTimeRequired  		
      
 FROM         
  ReportsMenu RM           
  LEFT JOIN ReportsMenuAccess RMAccess ON RMAccess.ReportID = RM.ReportID           
  INNER  JOIN ReportsMenu  RMParent on  RMParent.ReportID = RM.ReportMenuID      
 WHERE RMAccess.SecurityRoleID=@SecurityRoleID     
and  ISNULL(UPPER(RMParent.Client),'') = 'SGVI' and @SGVISetting='true' and @Client='SGVI'  
AND RM.ReportArgName NOT IN ('EmployeeCardListReport','EmployeeCardSessionsReport',
							  'CappedGameSummaryReport','CappedGameListReport',
							  'Fixed Expense Details Report','Period-End Liquidation Revenue Report',
							  'Site Licensing Reports', 'License History Report', 'License Expiry Report')

Union        


SELECT   Distinct             
 RM.ReportID,              
 'Management Reports' as ParentName,              
 RM.ReportMenuID as ParentID,              
 RM.ReportName,              
 RM.ReportName AS ReportDescription,            
 RM.[Level],            
 RM.ApplicationServer,            
 RM.ReportArgName,            
 RM.ReportStatus,            
 RM.ShowException,            
 RM.SecurityRoleID,            
 RM.MS_ProcedureUsed,
 ISNULL(RM.XMLName,'') AS XMLName ,
 ISNULL(RM.ExportExcel,0) AS ExportExcel,
 RM.IsTimeRequired                 
            
 FROM               
  ReportsMenu RM     
   LEFT JOIN ReportsMenuAccess RMAccess ON RMAccess.ReportID = RM.ReportID           
  INNER  JOIN ReportsMenu  RMParent on  RMParent.ReportID = RM.ReportMenuID      
 WHERE RMAccess.SecurityRoleID= @SecurityRoleID            
and  Rm.ReportARGname ='ACCWINLOSS_US'        
AND RM.ReportArgName NOT IN ('EmployeeCardListReport','EmployeeCardSessionsReport',							  
							  'Fixed Expense Details Report','Period-End Liquidation Revenue Report',
							  'Site Licensing Reports', 'License History Report', 'License Expiry Report')  


Union

 
SELECT   Distinct       
 RM.ReportID,        
 RMParent.ReportName AS ParentName,        
 RM.ReportMenuID as ParentID,        
 RM.ReportName,        
 RM.ReportName AS ReportDescription,      
 RM.[Level],      
 RM.ApplicationServer,      
 RM.ReportArgName,      
 RM.ReportStatus,      
 RM.ShowException,      
 RM.SecurityRoleID,      
 RM.MS_ProcedureUsed,
 ISNULL(RM.XMLName,'') AS XMLName ,
 ISNULL(RM.ExportExcel,0) AS ExportExcel,
 RM.IsTimeRequired     	
      
 FROM         
 ReportsMenu RM          
 LEFT JOIN ReportsMenuAccess RMAccess ON RMAccess.ReportID = RM.ReportID           
 INNER  JOIN ReportsMenu  RMParent on  RMParent.ReportID = RM.ReportMenuID      
 WHERE RMAccess.SecurityRoleID= @SecurityRoleID             
 AND RM.ReportArgName IN ('EmployeeCardListReport','EmployeeCardSessionsReport')
 AND @IsEmployeeTrackingEnabled = 'true'
 
 Union  
  
   
SELECT   Distinct         
 RM.ReportID,          
 RMParent.ReportName AS ParentName,          
 RM.ReportMenuID as ParentID,          
 RM.ReportName,          
 RM.ReportName AS ReportDescription,        
 RM.[Level],        
 RM.ApplicationServer,        
 RM.ReportArgName,        
 RM.ReportStatus,        
 RM.ShowException,        
 RM.SecurityRoleID,        
 RM.MS_ProcedureUsed,
 ISNULL(RM.XMLName,'') AS XMLName ,
 ISNULL(RM.ExportExcel,0) AS ExportExcel,
 RM.IsTimeRequired
     
 FROM           
 ReportsMenu RM            
 LEFT JOIN ReportsMenuAccess RMAccess ON RMAccess.ReportID = RM.ReportID             
 INNER  JOIN ReportsMenu  RMParent on  RMParent.ReportID = RM.ReportMenuID        
 WHERE RMAccess.SecurityRoleID= @SecurityRoleID               
 AND RM.ReportArgName IN ('Fixed Expense Details Report','Period-End Liquidation Revenue Report')  
 AND @LiquidationProfitShare = 'true'
 
 Union  
  
   
SELECT   Distinct         
 RM.ReportID,          
 RMParent.ReportName AS ParentName,          
 RM.ReportMenuID as ParentID,          
 RM.ReportName,          
 RM.ReportName AS ReportDescription,        
 RM.[Level],        
 RM.ApplicationServer,        
 RM.ReportArgName,        
 RM.ReportStatus,        
 RM.ShowException,        
 RM.SecurityRoleID,        
 RM.MS_ProcedureUsed,
 ISNULL(RM.XMLName,'') AS XMLName ,
 ISNULL(RM.ExportExcel,0) AS ExportExcel,
 RM.IsTimeRequired
     
 FROM           
 ReportsMenu RM            
 LEFT JOIN ReportsMenuAccess RMAccess ON RMAccess.ReportID = RM.ReportID             
 INNER  JOIN ReportsMenu  RMParent on  RMParent.ReportID = RM.ReportMenuID        
 WHERE RMAccess.SecurityRoleID= @SecurityRoleID               
 AND RM.ReportArgName IN ('CappedGameSummaryReport','CappedGameListReport')  
 AND @IsGameCappingEnabled = 'true'  
 
 
 Union  
  
   
SELECT   Distinct         
 RM.ReportID,          
 RMParent.ReportName AS ParentName,          
 RM.ReportMenuID as ParentID,          
 RM.ReportName,          
 RM.ReportName AS ReportDescription,        
 RM.[Level],        
 RM.ApplicationServer,        
 RM.ReportArgName,        
 RM.ReportStatus,        
 RM.ShowException,        
 RM.SecurityRoleID,        
 RM.MS_ProcedureUsed,
 ISNULL(RM.XMLName,'') AS XMLName ,
 ISNULL(RM.ExportExcel,0) AS ExportExcel,
 RM.IsTimeRequired
      
 FROM           
 ReportsMenu RM            
 LEFT JOIN ReportsMenuAccess RMAccess ON RMAccess.ReportID = RM.ReportID             
 INNER  JOIN ReportsMenu  RMParent on  RMParent.ReportID = RM.ReportMenuID        
 WHERE RMAccess.SecurityRoleID= @SecurityRoleID               
 AND RM.ReportArgName IN ('Site Licensing Reports', 'License History Report', 'License Expiry Report')  
 AND @IsSiteLicensingEnabled = 'true'  
 ORDER BY level,ParentName,ParentID,RM.ReportName
 
END
GO