USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetSiteStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetSiteStatus]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- rsp_GetSiteStatus  
-- -----------------------------------------------------------------  
--  
-- Get Site Status
--   
-- -----------------------------------------------------------------      
-- Revision History         
--   
-- Lekha 03/29/2012 Modified To Select IsCrossTicketingEnabled Column      
-- Yoganandh.P			09/09/2010			Created
-- =================================================================   
CREATE PROCEDURE rsp_GetSiteStatus
(
	@Site_ID INT
)
AS
BEGIN
	SELECT Site_Enabled, ISNULL(IsTITOEnabled,0) AS IsTITOEnabled, ISNULL(IsCrossTicketingEnabled,0) AS IsCrossTicketingEnabled,ISNULL(IsNonCashVoucherEnabled,0) AS IsNonCashVoucherEnabled FROM Site WHERE Site_ID = @Site_ID
END

GO

