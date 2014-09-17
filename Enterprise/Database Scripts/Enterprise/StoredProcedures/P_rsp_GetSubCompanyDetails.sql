USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_GetSubCompanyDetails]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_GetSubCompanyDetails]
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
CREATE PROCEDURE [dbo].rsp_GetSubCompanyDetails
(@company INT = 0, @SecurityUserID INT = 0)
AS
BEGIN
	SET NOCOUNT ON
	DECLARE @ViewAllCompanies  INT
	DECLARE @RoleName          VARCHAR(50)
	
	SELECT @ViewAllCompanies = ISNULL(ca.Customer_Access_View_All_Companies,0),
	       @RoleName = ISNULL(R.RoleName,'')
	FROM   Customer_Access ca
	       INNER JOIN Staff_Customer_Access C
	            ON  CA.Customer_Access_ID = C.Customer_Access_ID
	       INNER JOIN STAFF D
	            ON  D.staff_id = C.staff_id 
	                --Added below for UserBased Site Access  	                
	                
	       INNER JOIN [USER] U
	            ON  U.SecurityUserID = D.UserTableID
	       INNER JOIN [UserRole_lnk] URL
	            ON  URL.SecurityUserID = U.SecurityUserID
	       INNER JOIN [ROLE] R
	            ON  R.SecurityRoleID = URL.SecurityRoleID
	WHERE  URL.SecurityUserID = @SecurityUserID
	
	IF @ViewAllCompanies = 1
	   OR @RoleName = 'SUPER USER'
	    SELECT DISTINCT sc.Sub_Company_ID,
	           sc.Sub_Company_Name
	    FROM   Sub_Company sc
	           INNER JOIN Company c
	                ON  c.Company_ID = sc.Company_ID
	    WHERE  (@company = 0)
	           OR  (@company IS NOT NULL AND c.Company_ID = @company)
	    ORDER BY
	           Sub_Company_Name
	ELSE
	    SELECT DISTINCT sc.Sub_Company_ID,
	           sc.Sub_Company_Name
	    FROM   Sub_Company sc
	           INNER JOIN COMPANY C
	                ON  sc.Company_ID = c.Company_ID
	           INNER JOIN Customer_Access_Sub_Company casc
	                ON  casc.Sub_Company_ID = sc.Sub_Company_ID
	           INNER JOIN Customer_Access ca
	                ON  ca.Customer_Access_ID = casc.Customer_Access_ID
	           INNER JOIN Staff_Customer_Access SCA
	                ON  CA.Customer_Access_ID = SCA.Customer_Access_ID
	           INNER JOIN STAFF D
	                ON  D.staff_id = SCA.staff_id
	           INNER JOIN [USER] U
	                ON  U.SecurityUserID = D.UserTableID
	    WHERE  U.SecurityUserID = @SecurityUserID
	           AND  ((@company = 0)
	           OR  (@company IS NOT NULL AND c.Company_ID = @company))
	    ORDER BY
	           Sub_Company_Name
	    SET NOCOUNT OFF
END 
GO


