USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ASPNET_SP_RemoveRoleToRoleAccessMapping]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ASPNET_SP_RemoveRoleToRoleAccessMapping]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


Create Procedure [dbo].[ASPNET_SP_RemoveRoleToRoleAccessMapping] (@RoleID INT, @RoleAccessID INT)
AS
BEGIN

	IF EXISTS (SELECT 1 FROM RoleAccessRole_lnk WHERE SecurityRoleID = @RoleID AND RoleAccessID = @RoleAccessID)
	BEGIN
		DELETE FROM RoleAccessRole_lnk WHERE SecurityRoleID = @RoleID AND RoleAccessID = @RoleAccessID
		SELECT 'SUCCESS' AS RESULT	
	END
	ELSE
	BEGIN
		SELECT 'ERROR:06' AS RESULT
	END

END

GO

