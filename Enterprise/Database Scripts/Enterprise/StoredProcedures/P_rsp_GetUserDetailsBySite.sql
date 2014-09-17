USE [Enterprise]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetUserDetailsBySite]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetUserDetailsBySite]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- rsp_GetUserDetailsBySite
-- ---------------------------------------------------------------------------------
-- 
-- To get user details who are all have access rights to the site based on the site
-- 
-- ---------------------------------------------------------------------------------
-- Revision History
-- 
-- 15/05/2012 Dinesh Rathinavel Created
-- 
-- 18/03/2014 - Rangesh - Modified UserName to Windows user name
-- =================================================================================


CREATE  PROCEDURE dbo.rsp_GetUserDetailsBySite      
	@Site_No INT
AS      
BEGIN
	SELECT 
		UL.SecurityUserID AS SecurityUserID,
		U.UserName AS UserName
	FROM VW_Enterprise_usersite_lnk UL
		INNER JOIN [User] U ON U.SecurityUserID = UL.SecurityUserID
	WHERE UL.SiteID = @Site_No
	ORDER BY u.UserName

END
GO
