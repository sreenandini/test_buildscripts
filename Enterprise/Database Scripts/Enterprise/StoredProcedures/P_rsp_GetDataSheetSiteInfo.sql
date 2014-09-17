GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetDataSheetSiteInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetDataSheetSiteInfo]
GO
-- =======================================================================
-- Revision History      
-- Rajkumar R 22/08/2013  Created      
-- =======================================================================   
GO
CREATE PROCEDURE rsp_GetDataSheetSiteInfo
(
@SubCompanyId INT = 0 ,
@CompanyId INT=0,
@SecurityUserID INT	
)
 AS
 BEGIN

IF @SubCompanyId = 0 
	SET @SubCompanyId = NULL
	
IF @CompanyId = 0 
	SET @CompanyId = NULL
	
 SELECT
			S.[Site_ID],
			S.[Site_Name]
	FROM [dbo].[Site] S
	INNER JOIN [dbo].[Sub_Company] SC ON SC.[Sub_Company_ID] = S.[Sub_Company_ID]
	INNER JOIN [dbo].[Company] C ON SC.[Company_ID] = C.[Company_ID]	
	INNER JOIN  SecurityProfile SP
	            ON  SP.SecurityProfileType_Value = s.Site_ID
	            AND SP.AllowUser = 1
	            AND SP.SecurityProfileType_ID = 2
	INNER JOIN Staff_Customer_Access SCA
	            ON  SP.Customer_Access_ID = SCA.Customer_Access_ID
	       INNER JOIN STAFF ST
	            ON  ST.staff_id = SCA.staff_id                
	                --Added below for UserBased Site Access  	                
	       INNER JOIN [USER] U
	            ON  U.SecurityUserID = ST.UserTableID
	       INNER JOIN [UserRole_lnk] URL
	            ON  URL.SecurityUserID = U.SecurityUserID
	       INNER JOIN [ROLE] R
	            ON  R.SecurityRoleID = URL.SecurityRoleID	     
	WHERE  ((R.RoleName = 'SUPER USER' AND URL.SecurityUserID = @SecurityUserID)
	       OR  (
	               R.RoleName <> 'SUPER USER'
	               AND U.SecurityUserID = @SecurityUserID
	               
	           ))
		AND  C.Company_ID =ISNULL(@CompanyId, C.Company_ID)
		AND SC.Sub_Company_ID = ISNULL(@SubCompanyId, SC.Sub_Company_ID)
 
 ORDER BY S.Site_Name
 END