USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ASPNET_SP_GetRoleAccess]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ASPNET_SP_GetRoleAccess]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[ASPNET_SP_GetRoleAccess](@RoleAccessID INT = 0,@RoleAccessName VARCHAR(200) = '')
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
	LEFT OUTER JOin ROLEACCESS tRParent 
		On tRParent.RoleAccessID = tR.ParentID
	WHERE
		(@RoleAccessID = 0 OR @RoleAccessID = tR.RoleAccessID)
	AND
		(@RoleAccessName = '' OR tR.RoleAccessName = @RoleAccessName)
	AND 
		 tR.IsEnabled = 'Y'

END


GO

