USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetOrganisationInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetOrganisationInfo]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[rsp_GetOrganisationInfo]
@Userid      INT = NULL
AS
	/*****************************************************************************************************
DESCRIPTION		: Procedure to Get the Organisation Information
CREATED DATE	: 2012-10-30 06:03:51.280
MODULE          : Organisation Administrator Screen
CHANGE HISTORY	:
------------------------------------------------------------------------------------------------------
AUTHOR                              DESCRIPTON                                            MODIFIED DATE
------------------------------------------------------------------------------------------------------
Venkatesan Haridass                Enterprise Rewrite                                     2012-10-30
------------------------------------------------------------------------------------------------------

*****************************************************************************************************/
BEGIN

 SELECT DISTINCT
   C.[Company_Name],     
   SC.[Sub_Company_Name],     
   S.[Site_Name],       
   S.[Site_ID],    
   C.[Company_ID],    
   SC.[Sub_Company_ID],    
   S.[Site_Code],    
  (CASE WHEN S.Site_Closed = 1 THEN 'TERMINATED'
   WHEN S.[SiteStatus] LIKE '%Partially%' THEN 'Partially Configured'
   ELSE '' END) AS [SiteStatus]  
   FROM  [dbo].[Company] C   
   LEFT JOIN [dbo].[Sub_Company] SC ON SC.[Company_ID] = C.[Company_ID]  
   LEFT JOIN [dbo].[Site] S  ON S.[Sub_Company_ID] = SC.[Sub_Company_ID]    
 --  INNER JOIN  [dbo].SecurityProfile A
	--            ON  A.SecurityProfileType_Value = S.Site_ID
	--            AND AllowUser = 1
	--            AND A.SecurityProfileType_ID = 2
 --  INNER JOIN [dbo].Staff_Customer_Access CA
 --       ON  A.Customer_Access_ID = CA.Customer_Access_ID
 --  INNER JOIN [dbo].STAFF D
 --       ON  D.staff_id = CA.staff_id
 --  INNER JOIN [dbo].[USER] U
 --       ON  U.SecurityUserID = D.UserTableID
 --  INNER JOIN [dbo].[UserRole_lnk] URL
 --       ON  URL.SecurityUserID = U.SecurityUserID
 --  INNER JOIN [dbo].[ROLE] R
 --       ON  R.SecurityRoleID = URL.SecurityRoleID
 --  LEFT JOIN [dbo].[UserSite_lnk] uSL
 --       ON  uSL.SecurityUserID = U.SecurityUserID
	--WHERE  (R.RoleName = 'SUPER USER' AND URL.SecurityUserID = @Userid)
	--       OR  (
	--               R.RoleName <> 'SUPER USER'
	--               AND uSL.SecurityUserID = @Userid
	--               AND usL.SiteID = S.Site_ID
	--           )
   ORDER BY C.[Company_Name] ASC, C.[Company_ID] ASC, SC.[Sub_Company_Name] ASC,    
 SC.[Sub_Company_ID] ASC, S.[Site_Name] ASC, S.[Site_ID] ASC     
END

GO

