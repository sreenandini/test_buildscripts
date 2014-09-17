USE [Enterprise]
GO

/****** Object:  StoredProcedure [dbo].[rsp_SL_GetSites]    Script Date: 05/21/2013 22:36:14 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_SL_GetSites]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_SL_GetSites]
GO

USE [Enterprise]
GO

/****** Object:  StoredProcedure [dbo].[rsp_SL_GetSites]    Script Date: 05/21/2013 22:36:14 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/************************************************************
 * Code formatted by SoftTree SQL Assistant © v4.8.29
 * Time: 21/05/2013 10:35:42 PM
 ************************************************************/

CREATE PROCEDURE [dbo].[rsp_SL_GetSites] 
(@company_ID INT = 0, @subcompany_ID INT = 0, @UserId       INT = 0)
AS
	-- =======================================================================
	-- OUTPUT --Get Company details -- exec rsp_SL_GetSites 0,0
	-- Revision History
	-- Venkatesan Haridass 21/05/2013  Created
	--EXEC rsp_SL_GetSites 0,0
	-- =======================================================================   
BEGIN
	SELECT DISTINCT S.Site_ID,
	       S.[Site_Name]
	FROM   SITE S
	       INNER JOIN Sub_Company SC
	            ON  S.Sub_Company_ID = SC.Sub_Company_ID
	       INNER JOIN Company C
	            ON  C.Company_ID = SC.Company_ID
	       INNER JOIN SecurityProfile SP
	            ON  SP.SecurityProfileType_Value = S.Site_ID
	            AND AllowUser = 1
	            AND SecurityProfileType_ID = 2
	       INNER JOIN STAFF_CUSTOMER_ACCESS SCA
	            ON  SP.Customer_Access_ID = SCA. Customer_Access_ID
	            AND (@UserId = 0 OR SCA.Staff_ID = @UserId)
	WHERE  (
	           @subcompany_ID = 0
	           OR (@subcompany_ID <> 0 AND S.Sub_Company_ID = @subcompany_ID)
	       )
	       AND (
	               @company_ID = 0
	               OR (@company_ID <> 0 AND C.Company_ID = @company_ID)
	           )
	ORDER BY
	       S.Site_Name
END    
GO


