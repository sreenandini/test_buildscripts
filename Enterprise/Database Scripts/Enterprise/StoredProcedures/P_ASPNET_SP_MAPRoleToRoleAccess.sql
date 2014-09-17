USE [Enterprise]
GO

IF  EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ASPNET_SP_MAPRoleToRoleAccess]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ASPNET_SP_MAPRoleToRoleAccess]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


Create Procedure [dbo].[ASPNET_SP_MAPRoleToRoleAccess] (@RoleID INT, @RoleAccessID INT)
AS
BEGIN

	IF NOT EXISTS (SELECT 1 FROM RoleAccessRole_lnk WHERE SecurityRoleID = @RoleID AND RoleAccessID = @RoleAccessID)
	BEGIN
		INSERT INTO RoleAccessRole_lnk (SecurityRoleID, RoleAccessID) VALUES (@RoleID, @RoleAccessID)
	END

END

GO

