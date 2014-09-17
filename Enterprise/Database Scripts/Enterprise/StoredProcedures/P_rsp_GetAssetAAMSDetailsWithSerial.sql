USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetAssetAAMSDetailsWithSerial]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetAssetAAMSDetailsWithSerial]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =================================================================  
-- rsp_GetAssetAAMSDetails  
-- -----------------------------------------------------------------  
--  
-- Gets the Asset AAMS Details.    
--   
-- -----------------------------------------------------------------      
-- Revision History         
--         
-- 19/11/09 Renjish Created        
-- =================================================================  

CREATE PROCEDURE dbo.rsp_GetAssetAAMSDetailsWithSerial
@Serial_No VARCHAR(12)
AS

SELECT M.Machine_ID, M.Machine_MAC_Address, M.Machine_Manufacturers_Serial_No, BAD.BAD_AAMS_Code, BAD.BAD_AAMS_Status 
 FROM dbo.Machine M
INNER JOIN dbo.BMC_AAMS_Details BAD ON M.Machine_ID = BAD.BAD_Reference_ID
WHERE M.Machine_Manufacturers_Serial_No = @Serial_No
AND BAD.BAD_AAMS_Entity_Type = 3


GO

