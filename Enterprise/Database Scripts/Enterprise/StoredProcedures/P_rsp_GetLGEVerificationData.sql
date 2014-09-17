USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetLGEVerificationData]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetLGEVerificationData]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =================================================================  
-- rsp_GetLGEVerificationData  
-- -----------------------------------------------------------------  
--  
-- Get the asset verification details.  
--   
-- -----------------------------------------------------------------      
-- Revision History         
--         
-- 19/11/09 Renjish Created        
-- =================================================================   
  
CREATE PROCEDURE dbo.rsp_GetLGEVerificationData 
@MessageID VARCHAR(20)
AS  

DECLARE @Type VARCHAR(50)

SELECT @Type = LGE_EH_Type FROM dbo.LGE_Export_History
WHERE LGE_EH_AAMS_Message_ID = @MessageID AND LGE_EH_Status = 100

IF (@Type = 'verifyOS')
BEGIN
	SELECT BAD.BAD_Verification_Status AS VLT_Status,
	CASE ISNULL(BAD.BAD_Verification_Status,0) WHEN 1 THEN 'Pass' ELSE 'Fail' END AS strStatus 
	FROM dbo.BMC_AAMS_Details BAD	
	INNER JOIN dbo.LGE_Export_History LGE ON LGE.LGE_EH_Reference = BAD.BAD_AAMS_Code 
	WHERE LGE.LGE_EH_AAMS_Message_ID = @MessageID AND BAD.BAD_AAMS_Entity_Type = 3
END
ELSE IF (@Type = 'verifyGame')
BEGIN
	SELECT TOP 1 CVD.CVD_Verification_Status AS VLT_Status,
	CASE ISNULL(CVD.CVD_Verification_Status,0) WHEN 1 THEN 'Pass' ELSE 'Fail' END AS strStatus 
	FROM dbo.CV_Verification_Details CVD
	INNER JOIN dbo.CV_Component_Details CCD ON CVD.CVD_CCD_ID = CCD.CCD_ID
	INNER JOIN dbo.BMC_AAMS_Details BAD ON CVD.CVD_Machine_Serial_No = BAD.BAD_Asset_Serial_No 
	INNER JOIN dbo.LGE_Export_History LGE ON LGE.LGE_EH_Message_Reference = BAD.BAD_AAMS_Code 
	WHERE LGE.LGE_EH_AAMS_Message_ID = @MessageID AND BAD.BAD_AAMS_Entity_Type = 3 AND
	CVD.CVD_Verification_Type = 5 AND CCD.CCD_CCT_Code = 2
	ORDER BY CVD_ID DESC
END
ELSE
BEGIN
	SELECT 'In Progress' as strStatus, 'In Progress' AS VLT_Status
END


GO

