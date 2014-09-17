USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetAssetTerminationDetailsForAAMSExport]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetAssetTerminationDetailsForAAMSExport]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =================================================================  
-- rsp_GetAssetTerminationDetailsForAAMSExport  
-- -----------------------------------------------------------------  
--  
-- Get the asset termination details to be exported for AAMS.  
--   
-- -----------------------------------------------------------------      
-- Revision History         
--         
-- 01/03/10 Renjish Created        
-- =================================================================   
  
CREATE PROCEDURE dbo.rsp_GetAssetTerminationDetailsForAAMSExport  
@Machine_ID INT 
AS  

DECLARE @SenderCode VARCHAR(20)

SELECT @SenderCode = Setting_Value FROM Setting WHERE Setting_Name = 'SenderCode'

--Retrive the other values.
SELECT ISNULL(BAD.BAD_AAMS_Code,'') AS BAD_AAMS_Code, ISNULL(M.Machine_MAC_Address,'') AS Machine_MAC_Address, 
ISNULL(M.Machine_Manufacturers_Serial_No,'') AS Machine_Manufacturers_Serial_No, ISNULL(@SenderCode, '') AS SenderCode,
ISNULL(CONVERT(DATETIME,M.Machine_End_Date,101), '') AS Machine_End_Date, ISNULL(Machine_Termination_Reason, 0) AS Machine_Termination_Reason
FROM dbo.Machine M 
INNER JOIN dbo.BMC_AAMS_Details BAD ON M.Machine_ID = BAD.BAD_Reference_ID
INNER JOIN dbo.AAMS_Entities AE ON  BAD.BAD_AAMS_Entity_Type = AE.AE_ID 
WHERE M.Machine_ID = @Machine_ID
AND AE.AE_Type = 'Asset' 


GO

