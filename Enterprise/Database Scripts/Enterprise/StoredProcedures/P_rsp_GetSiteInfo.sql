USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetSiteInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetSiteInfo]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- rsp_GetSiteInfo
-- -----------------------------------------------------------------
-- 
-- To get sitecode and name based on the subcompany     
-- 
-- -----------------------------------------------------------------
-- Revision History
-- 
-- 29/03/2012 Dinesh Rathinavel Created
-- 
-- =================================================================

CREATE PROCEDURE [dbo].[rsp_GetSiteInfo]
	@SubCompanyId INT = 0,
	@UserId       INT = 0
AS  
BEGIN
	SET NOCOUNT ON;
	
	SELECT DISTINCT
			S.[Site_ID],
			S.[Site_Name]
	FROM [dbo].[Site] S
			INNER JOIN [dbo].[Sub_Company] SC ON SC.[Sub_Company_ID] = S.[Sub_Company_ID]
			INNER JOIN [dbo].[Company] C ON SC.[Company_ID] = C.[Company_ID]
			INNER JOIN SecurityProfile SP
	            ON  SP.SecurityProfileType_Value = S.Site_ID
	            AND AllowUser = 1
	            AND SecurityProfileType_ID = 2
	       INNER JOIN STAFF_CUSTOMER_ACCESS SCA
	            ON  SP.Customer_Access_ID = SCA. Customer_Access_ID
	            AND (@UserId = 0 OR SCA.Staff_ID = @UserId)
	WHERE (@SubCompanyId = 0 OR SC.[Sub_Company_ID] = @SubCompanyId)
	ORDER BY S.Site_Name
END

GO

