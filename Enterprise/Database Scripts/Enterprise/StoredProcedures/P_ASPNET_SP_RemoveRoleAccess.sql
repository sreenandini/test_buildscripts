USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ASPNET_SP_RemoveRoleAccess]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ASPNET_SP_RemoveRoleAccess]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[ASPNET_SP_RemoveRoleAccess](@RoleAccessID INT)
AS
BEGIN

	IF EXISTS (Select * From RoleAccess tRA INNER JOIN  RoleAccessRole_lnk tRAR On tRAR.RoleAccessID = tRA.RoleAccessID Where tRA.RoleAccessID = @RoleAccessID)
	BEGIN
		SELECT 'ERROR:05' AS RESULT
	END
	ELSE
	BEGIN

		Delete From RoleAccess Where RoleAccessID = @RoleAccessID

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

