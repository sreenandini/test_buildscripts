USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ASPNET_SP_UpdateRole]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ASPNET_SP_UpdateRole]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[ASPNET_SP_UpdateRole](@RoleID INT , @RoleName VARCHAR(100), @RoleDescription VARCHAR(300))
AS
BEGIN
	IF NOT EXISTS (SELECT * FROM [ROLE] WHERE SecurityRoleID = @RoleID)
		SELECT 'ERROR:02' AS RESULT
	ELSE
	BEGIN
		UPDATE [ROLE] SET RoleName = @RoleName, RoleDescription = @RoleDescription WHERE SecurityRoleID = @RoleID 
		SELECT 'SUCCESS' AS RESULT
	END
END

GO

