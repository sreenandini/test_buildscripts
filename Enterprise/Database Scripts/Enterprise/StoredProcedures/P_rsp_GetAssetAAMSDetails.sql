USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetAssetAAMSDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetAssetAAMSDetails]
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

CREATE PROCEDURE dbo.rsp_GetAssetAAMSDetails
@Machine_ID VARCHAR(50)
AS

SELECT M.Machine_ID, M.Machine_MAC_Address, M.Machine_Manufacturers_Serial_No, BAD.BAD_AAMS_Code, BAD.BAD_AAMS_Status 
 FROM dbo.Machine M
INNER JOIN dbo.BMC_AAMS_Details BAD ON M.Machine_ID = BAD.BAD_Reference_ID
INNER JOIN dbo.AAMS_Entities AE ON  BAD.BAD_AAMS_Entity_Type = AE.AE_Type
WHERE M.Machine_ID = @Machine_ID
AND AE.AE_Type = 'Asset'


GO

