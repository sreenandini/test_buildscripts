USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ASPNET_SP_GetRoleAccessForRole]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ASPNET_SP_GetRoleAccessForRole]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[ASPNET_SP_GetRoleAccessForRole](@RoleID INT)
AS
BEGIN
	SELECT 
		tR.RoleAccessID,
		tRParent.RoleAccessName As ParentName,
		tR.ParentID,
		tR.RoleAccessName,
		tR.Description 
	FROM 
		ROLEACCESS tR
	INNER JOIN RoleAccessRole_lnk tRAR On tRAR.RoleAccessID = tR.RoleAccessID
	LEFT OUTER JOin ROLEACCESS tRParent 
		On tRParent.RoleAccessID = tR.ParentID
	WHERE tRAR.SecurityRoleID = @RoleID
	AND tR.IsEnabled = 'Y'
END

GO

