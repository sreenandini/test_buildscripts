USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetRoleToUser]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetRoleToUser]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- -----------------------------------------------------------------------------------------------------------------------------------
-- Revision History  --exec rsp_GetRoleToUser 2
-- 30/03/2010	Vineetha    created
CREATE PROCEDURE [dbo].rsp_GetRoleToUser(@Usertableid AS INT)
AS
BEGIN
	DECLARE @SecurityRoleID INT
	SET @SecurityRoleID=-1
	SELECT @SecurityRoleID=SecurityRoleID FROM UserRole_lnk WHERE SecurityUserID=@Usertableid	
	IF NOT (ISNULL(@SecurityRoleID,'')='')
		SELECT SecurityRoleID,RoleName,RoleDescription FROM [ROLE] WHERE SecurityRoleID=@SecurityRoleID 
END



GO

