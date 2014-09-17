USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_UpdateSiteEnabledStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_UpdateSiteEnabledStatus]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/* ********************************************************************************************************
-- StoredProcedure usp_UpdateSiteEnabledStatus
-- --------------------------------------------------------------------------------------------------------
--
-- To update the Site Enabled Status for the Site
--
-- --------------------------------------------------------------------------------------------------------
-- Revision History 
-- 
-- 09/09/2010		Yoganandh P		Created
***********************************************************************************************************/

CREATE PROCEDURE usp_UpdateSiteEnabledStatus
(	
	@Site_ID INT,  
	@Site_Enabled BIT
)
AS
BEGIN
		UPDATE Site SET Site_Enabled = @Site_Enabled WHERE Site_ID = @Site_ID  
END


GO

