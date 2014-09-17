USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetGameVerificationData]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetGameVerificationData]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =================================================================  
-- rsp_GetGameVerificationData  
-- -----------------------------------------------------------------  
--  
-- Get the asset and game verification details.  
--   
-- -----------------------------------------------------------------      
-- Revision History         
--         
-- 26/01/10 Renjish Created        
-- =================================================================   
  
CREATE PROCEDURE dbo.rsp_GetGameVerificationData 
@MessageID VARCHAR(20) 
AS  

--SELECT VVS.VLT_Status AS VLT_Status, LGE.LGE_EH_Status AS strStatus, BAD.BAD_AAMS_Code AS CodeID,
--BM.BAD_AAMS_Code AS GameID
--FROM dbo.VLT_Verification_Status VVS
--INNER JOIN dbo.BMC_AAMS_Details BAD ON VVS.VLT_Serial = BAD.BAD_Asset_Serial_No
--INNER JOIN dbo.LGE_Export_History LGE ON LGE.LGE_EH_Message_Reference = BAD.BAD_AAMS_Code
--INNER JOIN dbo.BMC_AAMS_Details BM ON LGE.LGE_EH_Reference = BM.BAD_AAMS_Code 
--INNER JOIN Installation_Game_Info IG ON IG.Game_Name = BM.BAD_Game_Name
--WHERE LGE.LGE_EH_AAMS_Message_ID = @MessageID AND BAD.BAD_AAMS_Entity_Type = 3 
--AND BM.BAD_AAMS_Entity_Type = 4 AND IG.IsAvailable = 1

SELECT VVS.VLT_Status AS VLT_Status, LGE.LGE_EH_Status AS strStatus, BAD.BAD_AAMS_Code AS CodeID,  
BM.BAD_AAMS_Code AS GameID  
FROM dbo.VLT_Verification_Status VVS   
INNER JOIN dbo.BMC_AAMS_Details BAD ON VVS.VLT_Serial = BAD.BAD_Asset_Serial_No  
INNER JOIN dbo.Machine M ON M.Machine_Manufacturers_Serial_No = BAD.BAD_Asset_Serial_No
INNER JOIN dbo.LGE_Export_History LGE ON LGE.LGE_EH_Message_Reference = BAD.BAD_AAMS_Code  
INNER JOIN dbo.BMC_AAMS_Details BM ON LGE.LGE_EH_Reference = BM.BAD_AAMS_Code   
INNER JOIN Installation_Game_Info IG ON IG.Game_Name = BM.BAD_Game_Name AND IG.Game_Part_Number = BM.BAD_Game_Part_Number 
INNER JOIN Installation I ON I.Installation_ID = IG.Installation_No AND I.Machine_ID = M.Machine_ID
WHERE LGE.LGE_EH_AAMS_Message_ID = @MessageID AND BAD.BAD_AAMS_Entity_Type = 3   
AND BM.BAD_AAMS_Entity_Type = 4 AND IG.IsAvailable = 1 AND I.Installation_End_Date IS NULL


GO

