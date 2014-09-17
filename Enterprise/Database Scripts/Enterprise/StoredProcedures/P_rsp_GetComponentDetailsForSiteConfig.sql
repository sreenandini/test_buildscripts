USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetComponentDetailsForSiteConfig]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetComponentDetailsForSiteConfig]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =================================================================  
-- rsp_GetComponentDetailsForSiteConfig  
-- -----------------------------------------------------------------  
--  
-- Get the Component Details to export to Exchange.    
--   
-- -----------------------------------------------------------------      
-- Revision History         
--         
-- 06/09/10 Renjish Created       
-- =================================================================  

CREATE PROCEDURE dbo.rsp_GetComponentDetailsForSiteConfig
AS

SELECT * FROM dbo.CV_Component_Details CCD 
FOR XML PATH ('COMPONENT') ,ELEMENTS XSINIL,ROOT('COMPONENT_DETAILS')


GO

