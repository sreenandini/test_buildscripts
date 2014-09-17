USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetComponentDetailsForExport]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetComponentDetailsForExport]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =================================================================  
-- rsp_GetComponentDetailsForExport  
-- -----------------------------------------------------------------  
--  
-- Get the Component Details to export to Exchange.    
--   
-- -----------------------------------------------------------------      
-- Revision History         
--         
-- 29/05/10 Renjish Created       
-- =================================================================  

CREATE PROCEDURE dbo.rsp_GetComponentDetailsForExport
@CCD_ID INT
AS

SELECT * FROM dbo.CV_Component_Details CCD 
WHERE CCD_ID = @CCD_ID
FOR XML PATH ('COMPONENT') ,ELEMENTS XSINIL,ROOT('COMPONENT_DETAILS')


GO

