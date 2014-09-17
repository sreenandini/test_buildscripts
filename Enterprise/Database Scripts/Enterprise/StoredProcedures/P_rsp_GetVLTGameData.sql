USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetVLTGameData]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetVLTGameData]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =================================================================  
-- rsp_GetVLTGameData  
-- -----------------------------------------------------------------  
--  
-- Get the game details for the current VLT.    
--   
-- -----------------------------------------------------------------      
-- Revision History         
--         
-- 19/11/09 Renjish Created        
-- =================================================================     
    
CREATE PROCEDURE dbo.rsp_GetVLTGameData    
@CodeID varchar (50)  
AS    

DECLARE @InstID INT
SELECT @InstID = I.Installation_ID FROM Machine M
INNER JOIN dbo.BMC_AAMS_Details BAD ON M.Machine_ID = BAD.BAD_Reference_ID
INNER JOIN dbo.Installation I ON M.Machine_ID = I.Machine_ID
WHERE BAD.BAD_AAMS_Entity_Type = 3 AND BAD_AAMS_Code = @CodeID AND I.Installation_End_Date IS NULL

SELECT GM.MG_Game_ID, BAD.BAD_AAMS_Code, BAD.BAD_AAMS_Status, GM.MG_Game_Name AS Game_Name,   
--IGI.Game_Current_Status AS CurrentStatus 
ISNULL(BP.Bar_Position_Machine_Enabled,0) AS CurrentStatus  
 FROM dbo.Game_Library GM  
INNER JOIN dbo.BMC_AAMS_Details BAD ON GM.MG_Game_ID = BAD.BAD_Reference_ID  
INNER JOIN dbo.Installation_Game_Info IGI ON GM.MG_Game_Name = IGI.Game_Name 
AND IGI.Game_Part_Number = GM.Game_Part_Number
INNER JOIN dbo.Installation I ON I.Installation_ID = IGI.Installation_No
INNER JOIN dbo.Bar_Position BP ON I.Bar_Position_ID = BP.Bar_Position_ID  
WHERE IGI.Installation_No = @InstID  
AND BAD.BAD_AAMS_Entity_Type = 4 AND IGI.IsAvailable = 1 AND I.Installation_End_Date IS NULL
  

GO

