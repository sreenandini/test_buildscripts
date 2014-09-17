USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetAAMSDetailsForExport]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetAAMSDetailsForExport]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =================================================================  
-- rsp_GetAAMSDetailsForExport  
-- -----------------------------------------------------------------  
--  
-- Get the AAMS Details to export to Exchange.    
--   
-- -----------------------------------------------------------------      
-- Revision History         
--         
-- 19/11/09 Renjish Created       
-- 09/12/09	Sudarsan	if type = VLT, sending the stock_no 
-- =================================================================  

CREATE PROCEDURE dbo.rsp_GetAAMSDetailsForExport
@AAMS_DETAILS_ID INT
AS

SELECT CASE WHEN BAD.BAD_AAMS_Entity_Type = 3 THEN M.Machine_Stock_No 
			ELSE '' END AS Stock, BAD.* 
 FROM dbo.BMC_AAMS_Details BAD
LEFT JOIN dbo.Machine M ON BAD.BAD_Reference_ID = M.Machine_ID
WHERE BAD.BAD_ID = @AAMS_DETAILS_ID
FOR XML PATH ('AAMSDETAIL') ,ELEMENTS XSINIL,ROOT('BMC_AAMS_DETAILS')


GO

