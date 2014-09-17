USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetBMCLGEData]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetBMCLGEData]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =================================================================  
-- rsp_GetBMCLGEData  
-- -----------------------------------------------------------------  
--  
-- Get the LGE related information.    
--   
-- -----------------------------------------------------------------      
-- Revision History         
--         
-- 19/11/09 Renjish Created        
-- =================================================================   
  
CREATE PROCEDURE dbo.rsp_GetBMCLGEData  
@CodeID varchar (50),  
@Type varchar (30),
@Reference varchar (50)

AS  

DECLARE @Serial VARCHAR(50)

SELECT @Serial = Machine_Manufacturers_Serial_No FROM Machine M INNER JOIN BMC_AAMS_Details BAD 
ON M.Machine_Manufacturers_Serial_No = BAD.BAD_Asset_Serial_No   
WHERE BAD_AAMS_Code = @CodeID AND BAD_AAMS_Entity_Type = 3

IF @Type = 'Game'
BEGIN
	SELECT TOP 1 CVD_Verification_Status as GameStatus, CVD_Verification_Time as Game_Verify_Date 
	FROM dbo.CV_Verification_Details CVD 
	INNER JOIN dbo.CV_Component_Details CCD ON CVD.CVD_CCD_ID = CCD.CCD_ID
	WHERE CVD_Machine_Serial_No = @Serial AND CVD_Request_Status = 0 AND CCD.CCD_CCT_Code = 2
	ORDER BY Game_Verify_Date DESC
END

IF @Type = 'Asset'
BEGIN
	SELECT BAD_Verification_Status as OsStatus, BAD_Updated_Date as OS_Verify_Date 
	FROM dbo.BMC_AAMS_Details 
	WHERE BAD_Asset_Serial_No = @Serial
END


GO

