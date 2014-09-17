USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_CheckGameInstallationApproved]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_CheckGameInstallationApproved]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =================================================================    
-- rsp_CheckGameInstallationApproved    
-- -----------------------------------------------------------------    
--    
-- Check the game installation approval.    
--     
-- -----------------------------------------------------------------        
-- Revision History           
--           
-- 08/10/10 Renjish Created          
-- =================================================================     
    
CREATE PROCEDURE dbo.rsp_CheckGameInstallationApproved  
@Game_ID INT    
AS    
    
SELECT 1 FROM dbo.Installation_Game_Info   
WHERE HQ_IGI_ID = @Game_ID AND Game_AAMS_Status = 1  


GO

