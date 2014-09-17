USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetGameDetailsForAAMSExport]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetGameDetailsForAAMSExport]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =================================================================  
-- rsp_GetGameDetailsForAAMSExport  
-- -----------------------------------------------------------------  
--  
-- Get the game details to export for AAMS.  
--   
-- -----------------------------------------------------------------      
-- Revision History         
--         
-- 19/11/09 Renjish Created        
-- =================================================================   
  
CREATE PROCEDURE dbo.rsp_GetGameDetailsForAAMSExport
@Game_ID INT  
AS  
  
DECLARE @CodeGameSystem VARCHAR(12)
DECLARE @SenderCode VARCHAR(20)

SELECT @SenderCode = Setting_Value FROM Setting WHERE Setting_Name = 'SenderCode'
SELECT @CodeGameSystem = BAD_AAMS_Code FROM dbo.BMC_AAMS_Details WHERE BAD_AAMS_Entity_Type = 1

SELECT ISNULL(GL.MG_Game_ID,'') AS MG_Game_ID, ISNULL(BAD.BAD_AAMS_Code,'') AS BAD_AAMS_Code, ISNULL(BAD.BAD_AAMS_Status,'') AS BAD_AAMS_Status, ISNULL(M.Machine_MAC_Address,'') AS Machine_MAC_Address, 
ISNULL(IGI.IsAvailable,'') AS IsAvailable, ISNULL(@CodeGameSystem,'') AS Code_Game_System, ISNULL(@SenderCode, '') AS SenderCode, ISNULL(BD.BAD_AAMS_Code,'') AS VLT_AAMS_Code
FROM dbo.Game_Library GL  
INNER JOIN dbo.BMC_AAMS_Details BAD ON GL.MG_Game_ID = BAD.BAD_Reference_ID  
INNER JOIN dbo.Installation_Game_Info IGI ON IGI.Game_Name = GL.MG_Game_Name 
INNER JOIN dbo.Installation I ON I.Installation_ID = IGI.Installation_No
INNER JOIN dbo.Machine M ON M.Machine_ID = I.Machine_ID
INNER JOIN dbo.BMC_AAMS_Details BD ON M.Machine_ID = BD.BAD_Reference_ID  
WHERE IGI.HQ_IGI_ID = @Game_ID AND I.Installation_End_Date IS NULL AND BAD.BAD_AAMS_Entity_Type = 4
AND BD.BAD_AAMS_Entity_Type = 3 AND ISNULL(BAD.BAD_AAMS_Code, '') <> ''


GO

