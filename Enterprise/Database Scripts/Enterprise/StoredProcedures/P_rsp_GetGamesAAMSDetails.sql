USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetGamesAAMSDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetGamesAAMSDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =================================================================  
-- rsp_GetGamesAAMSDetails  
-- -----------------------------------------------------------------  
--  
-- Get the game details to export for AAMS.  
--   
-- -----------------------------------------------------------------      
-- Revision History         
--         
-- 19/11/09 Renjish Created        
-- =================================================================   
  
CREATE PROCEDURE dbo.rsp_GetGamesAAMSDetails  
@Game_Name VARCHAR(50)  
AS  
  
SELECT GL.MG_Game_ID, ISNULL(BAD.BAD_AAMS_Code,'') AS BAD_AAMS_Code, BAD.BAD_AAMS_Status
FROM dbo.Game_Library GL  
LEFT JOIN dbo.BMC_AAMS_Details BAD ON GL.MG_Game_ID = BAD.BAD_Reference_ID  
WHERE GL.MG_Game_Name = @Game_Name  
AND BAD.BAD_AAMS_Entity_Type = 4


GO

