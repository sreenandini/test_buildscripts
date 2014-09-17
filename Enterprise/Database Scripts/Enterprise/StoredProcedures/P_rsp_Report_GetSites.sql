USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_Report_GetSites]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_Report_GetSites]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].rsp_Report_GetSites
(
    @company     INT = 0,
    @subcompany  INT = 0,
    @region      INT = 0,
    @area        INT = 0,
    @Userid      INT = NULL
)
AS
	
BEGIN
	SET NOCOUNT ON
	SELECT DISTINCT B.Site_ID,
	       B.Site_Code,
	       B.Site_Name,
	       B.Region
	FROM   SecurityProfile A
	       INNER JOIN SITE B
	            ON  A.SecurityProfileType_Value = B.Site_ID
	            AND AllowUser = 1
	            AND A.SecurityProfileType_ID = 2
	       INNER JOIN Staff_Customer_Access C
	            ON  A.Customer_Access_ID = C.Customer_Access_ID
	       INNER JOIN STAFF D
	            ON  D.staff_id = C.staff_id                
	                --Added below for UserBased Site Access  	                
	       INNER JOIN [USER] U
	            ON  U.SecurityUserID = D.UserTableID
	       INNER JOIN [UserRole_lnk] URL
	            ON  URL.SecurityUserID = U.SecurityUserID
	       INNER JOIN [ROLE] R
	            ON  R.SecurityRoleID = URL.SecurityRoleID	     
	WHERE  ((R.RoleName = 'SUPER USER' AND URL.SecurityUserID = @Userid)
	       OR  (
	               R.RoleName <> 'SUPER USER'
	               AND U.SecurityUserID = @Userid
	               
	           ))
	       AND (
	               (@subcompany <> 0 AND B.Sub_Company_ID = @subcompany)
	               OR (
	                      @subcompany = 0
	                      AND B.Sub_Company_ID IN (SELECT F.Sub_Company_ID
	                                               FROM   Sub_Company F
	                                               WHERE  F.Company_ID = @company)
	                  )
	           )
	ORDER BY
	       Site_Name
	 SET NOCOUNT OFF
END
GO

--exec rsp_Report_GetSites 2,2,0,0,2

