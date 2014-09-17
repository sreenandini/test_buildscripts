USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_UpdateGameAAMSDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_UpdateGameAAMSDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =================================================================  
-- usp_UpdateGameAAMSDetails  
-- -----------------------------------------------------------------  
--  
-- Update the game details.  
--   
-- -----------------------------------------------------------------      
-- Revision History         
--         
-- 19/11/09 Renjish Created        
-- =================================================================  

CREATE PROCEDURE dbo.usp_UpdateGameAAMSDetails
@Game_Name VARCHAR(50),
@Game_Code BIGINT
AS

UPDATE BMC_AAMS_Details
SET BAD_AAMS_Code = @Game_Code
WHERE BAD_Reference_ID = (SELECT TOP 1 MG_Game_ID FROM Game_Library WHERE MG_Game_Name = @Game_Name)
AND BAD_AAMS_Entity_Type = 4

GO

