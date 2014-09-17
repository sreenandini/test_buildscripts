USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ASPNET_SP_CreateRole]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ASPNET_SP_CreateRole]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[ASPNET_SP_CreateRole](@RoleName VARCHAR(100), @RoleDescription VARCHAR(300))
AS
BEGIN
	IF EXISTS (SELECT * FROM [ROLE] WHERE RoleName = @RoleName )	
		SELECT 'ERROR:02' AS RESULT
	ELSE
	BEGIN
		INSERT INTO [ROLE] (RoleName, RoleDescription) Values (@RoleName, @RoleDescription)
		SELECT 'SUCCESS' AS RESULT
	END
END

GO

