USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetMachineComponentDetailsForExport]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetMachineComponentDetailsForExport]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =================================================================  
-- rsp_GetMachineComponentDetailsForExport  
-- -----------------------------------------------------------------  
--  
-- Get the Component Details to export to Exchange.    
--   
-- -----------------------------------------------------------------      
-- Revision History         
--         
-- 29/05/10 Renjish Created       
-- =================================================================  

CREATE PROCEDURE dbo.rsp_GetMachineComponentDetailsForExport
@Machine_ID INT
AS

DECLARE @OutXML XML

SET @OutXML = (SELECT CMCD.*, CCD.* 
 FROM dbo.CV_Machine_Component_Details CMCD
INNER JOIN dbo.Machine M ON CMCD.CVMCD_Machine_Serial_No = M.Machine_Manufacturers_Serial_No 
INNER JOIN dbo.CV_Component_Details CCD ON CMCD.CVMCD_CCD_ID = CCD.CCD_ID
WHERE M.Machine_ID = @Machine_ID
FOR XML PATH ('COMPONENT') ,ELEMENTS XSINIL,ROOT('COMPONENT_DETAILS'))

SELECT @OutXML


GO

