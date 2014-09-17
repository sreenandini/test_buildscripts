USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_Security_GetData]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_Security_GetData]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE Procedure [dbo].[rsp_Security_GetData] 
(
	@UserName Varchar(200),
	@Password Varchar(200),
	@WindowsUserName Varchar(200) 
)
AS
BEGIN

	DECLARE @USERID INT

	IF (LTRIM(RTRIM(ISNULL(@WindowsUserName,''))) = '')
	BEGIN
		SELECT @USERID = SecurityUserID FROM [USER] WHERE UserName = @UserName AND Password = @Password
	END
	ELSE
	BEGIN
		SELECT @USERID = SecurityUserID FROM [USER] WHERE WindowsUserName = @WindowsUserName
	END

	SELECT * FROM [USER] WHERE SecurityUserID = ISNULL(@USERID, 0)


	SELECT 
		tObject.ObjectName AS ObjectName,
		tObject.ObjectType AS ObjectType,
		tRight.SecurityRightID AS [Right]
	FROM 
		[UserRole_lnk] tUserRole
	INNER JOIN [RoleAccessRole_lnk] tRAR ON tUserRole.SecurityRoleID = tRAR.SecurityRoleID
	INNER JOIN [RoleAccessObjectRight_lnk] tORR ON tORR.RoleAccessID = tRAR.RoleAccessID  
	INNER JOIN [Object] tObject ON tORR.SecurityObjectID = tObject.SecurityObjectID  
	INNER JOIN [Right] tRight ON tORR.SecurityRightID = tRight.SecurityRightID  
	WHERE tUserRole.SecurityUserID = @USERID

END

GO

