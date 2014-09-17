USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ASPNET_SP_GetUserRoleMapping]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ASPNET_SP_GetUserRoleMapping]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[ASPNET_SP_GetUserRoleMapping](@USERID INT)
AS
BEGIN
	SELECT 
		tRole.SecurityRoleID,
		tRole.RoleName,
		tRole.RoleDescription
	FROM 
		[ROLE] tRole
	INNER JOIN [UserRole_lnk] tUserRole_Lnk 
		On tUserRole_Lnk.SecurityRoleID = tRole.SecurityRoleID
	WHERE 
		tUserRole_Lnk.SecurityUserID = @USERID 
END

GO

