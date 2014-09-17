USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetRoleAccessForReport]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetRoleAccessForReport]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =======================================================================    
-- OUTPUT --Show Reports based on access          
-- Revision History    
-- Vineetha Mathew 22/03/2010  Created    
-- ======================================================================= 

CREATE PROCEDURE [dbo].[rsp_GetRoleAccessForReport]
(@SecurityRoleID int)  
AS  
BEGIN  

DECLARE @RoleName VARCHAR(100)  
DECLARE @client as varchar(100)    
SET @client=''    
 EXEC rsp_GetSetting NULL, 'CLIENT', '', @client OUTPUT    
  
  
DECLARE @powerpromo as varchar(10)    
SET @powerpromo=null    
 EXEC rsp_GetSetting NULL, 'ISPOWERPROMOREPORTSREQUIRED', '', @powerpromo OUTPUT    
  
  
DECLARE @SGVISetting as varchar(100)    
SET @SGVISetting=''    
 EXEC rsp_GetSetting NULL, 'SGVi_Enabled', '', @SGVISetting OUTPUT    
    
SELECT @RoleName = RoleName FROM [Role] WHERE SecurityRoleID = @SecurityRoleID
      
 SELECT         
  RM.ReportID, RM.Client,       
  RMParent.ReportName AS ParentName,        
  RM.ReportMenuID,        
  RM.ReportName,        
  RM.ReportName AS ReportDescription,      
  CASE WHEN @RoleName = 'Super User' THEN 'True' ELSE RM.ReportStatus  END ReportStatus       
 FROM  ReportsMenu RM        
 INNER JOIN ReportsMenuAccess RMAccess ON RMAccess.ReportID = RM.ReportID        
 LEFT OUTER JOin ReportsMenu RMParent  ON (RMParent.ReportID = RM.ReportMenuID)        
 WHERE RMAccess.SecurityRoleID=@SecurityRoleID     
 AND UPPER(ISNULL(RM.Client,@client)) = CASE WHEN (UPPER(RM.Client) = 'SGVI' AND @SGVISetting = 'False') THEN ''  
           WHEN (UPPER(RM.Client) = 'WINCHELLS'   
            AND UPPER(LTRIM(RTRIM(ISNULL(RM.ShowPowerPromoReports,'FALSE')))) = 'TRUE'   
            AND UPPER(@powerpromo) = 'FALSE') THEN ''  
           ELSE UPPER(ISNULL(RM.Client,@client)) END  
   
  
  
END    
  

GO

select * FROM ReportsMenu rm