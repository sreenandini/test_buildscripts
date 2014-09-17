USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetActiveInstallationsForSite]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetActiveInstallationsForSite]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =================================================================  
-- rsp_GetActiveInstallationsForSite  
-- -----------------------------------------------------------------  
--  
-- Get the active installation details.  
--   
-- -----------------------------------------------------------------      
-- Revision History         
--         
-- 19/11/09 Renjish Created        
-- =================================================================   
CREATE PROCEDURE [dbo].rsp_GetActiveInstallationsForSite
(@Site_Id int)    
   
AS    
    
 SELECT     
   I.Bar_Position_ID    
  FROM Installation I     
 LEFT JOIN Bar_Position B on I.Bar_Position_Id = B.Bar_Position_Id
 JOIN Site S on B.Site_Id = S.Site_Id    
 WHERE S.Site_ID = @Site_Id AND I.Installation_End_Date IS NULL   


GO

