USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_GetCompanyDetailsForAdmin]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_GetCompanyDetailsForAdmin]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- OUTPUT --Get Company details -- exec rsp_GetCompanyDetails 2         
-- Revision History    
-- Vineetha Mathew 22/03/2010  Created    
-- ======================================================================= 
CREATE PROCEDURE [dbo].rsp_GetCompanyDetailsForAdmin
(@SecurityUserID INT = 0)
AS
BEGIN
	SELECT DISTINCT c.Company_ID,
	       c.Company_Name
	FROM   SITE s
	       INNER JOIN Sub_Company sc
	            ON  sc.Sub_Company_ID = s.Sub_Company_ID
	       INNER JOIN Company c
	            ON  c.Company_ID = sc.Company_ID
	       INNER JOIN UserSite_lnk usl
	            ON  usl.SiteID = s.Site_ID
	WHERE  usl.SecurityUserID = @SecurityUserID
	ORDER BY
	       c.Company_Name
END
GO
