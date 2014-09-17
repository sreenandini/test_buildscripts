USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ASPNET_SP_UpdateRoleAccess]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ASPNET_SP_UpdateRoleAccess]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[ASPNET_SP_UpdateRoleAccess](@RoleAccessID INT, @ParentID INT, @RoleAccessName VARCHAR(200), @Description VARCHAR(200))
AS
BEGIN

	IF EXISTS (SELECT * FROM RoleAccess WHERE RoleAccessID = @RoleAccessID)
	BEGIN
		SELECT 'ERROR:04' AS RESULT
	END
	ELSE
	BEGIN
		Update 
			RoleAccess 
		Set 
			ParentID = @ParentID, 
			RoleAccessName = @RoleAccessName, 
			Description = @Description 
		Where
			RoleAccessID = @RoleAccessID

		IF @@rowcount > 0 
		BEGIN
			SELECT 'SUCCESS' AS RESULT
		END
		ELSE
		BEGIN
			SELECT 'ERROR:05' AS RESULT
		END
	END

END

GO

