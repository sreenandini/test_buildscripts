USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ASPNET_SP_RemoveUserRoleMapping]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ASPNET_SP_RemoveUserRoleMapping]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[ASPNET_SP_RemoveUserRoleMapping](@UserID INT, @RoleID INT)
AS
BEGIN
	DELETE FROM [UserRole_lnk] WHERE SecurityUserID = @USERID AND SecurityRoleID = @RoleID
END

GO

