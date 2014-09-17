USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetSubCompanyAccessKey]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetSubCompanyAccessKey]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE dbo.rsp_GetSubCompanyAccessKey(@SiteID INT = 0)
AS
/*****************************************************************************************************
DESCRIPTION		: Procedure to get the Sub Company Access Key ID  
CREATED DATE	: 2012-10-30 06:03:51.280
MODULE          : FrmAdminSite      
CHANGE HISTORY	:
------------------------------------------------------------------------------------------------------
AUTHOR                              DESCRIPTON                                            MODIFIED DATE
------------------------------------------------------------------------------------------------------
Venkatesan Haridass                Enterprise Rewrite                                     2012-10-30
------------------------------------------------------------------------------------------------------

*****************************************************************************************************/

BEGIN
	SET NOCOUNT ON 
	SELECT Sub_Company.Access_Key_ID
	FROM   Sub_Company WITH (NOLOCK)
	       INNER JOIN [SITE] WITH (NOLOCK)
	            ON  Sub_Company.Sub_Company_ID = SITE.Sub_Company_ID
	WHERE  SITE.Site_ID = @SiteID
END

GO

