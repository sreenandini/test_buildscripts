USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ASPNET_SP_GetRole]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ASPNET_SP_GetRole]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[ASPNET_SP_GetRole]
	(
	@ID INT = 0,
	@Name VARCHAR(200) = ''
	)	
AS	
BEGIN

	SELECT 
		SecurityRoleID,
		RoleName,
		RoleDescription 
	FROM 
		[Role] 
	WHERE 
		(@ID = 0 OR SecurityRoleID = ISNULL(@ID, 0))
		AND (@Name = '' OR	RoleName = @Name)

END

GO

