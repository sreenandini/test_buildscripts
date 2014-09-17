USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_GetSubCompanyDetailsForAdmin]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_GetSubCompanyDetailsForAdmin]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =======================================================================    
-- OUTPUT --Get Company details -- exec rsp_GetCompanyDetails 2         
-- Revision History    rsp_GetSubCompanyDetails
-- Vineetha Mathew 22/03/2010  Created    
-- ======================================================================= 
CREATE PROCEDURE [dbo].rsp_GetSubCompanyDetailsForAdmin
(@company INT = 0, @SecurityUserID INT = 0)
AS
BEGIN
	SET NOCOUNT ON 	
	SELECT DISTINCT sc.Sub_Company_ID,
	       sc.Sub_Company_Name
	FROM   Sub_Company sc
	       INNER JOIN SITE s
	            ON  s.Sub_Company_ID = sc.Sub_Company_ID
	       INNER JOIN Company c
	            ON  c.Company_ID = sc.Company_ID
	       INNER JOIN UserSite_lnk usl
	            ON  usl.SiteID = s.Site_ID
	WHERE  (
	           (@company = 0)
	           OR (@company IS NOT NULL AND c.Company_ID = @company)
	       )
	       AND usl.SecurityUserID = @SecurityUserID
	ORDER BY
	       Sub_Company_Name
	SET NOCOUNT OFF
END
GO


