USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ASPNET_SP_GetSecurityInformation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ASPNET_SP_GetSecurityInformation]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[ASPNET_SP_GetSecurityInformation] 
(  
	@UserName VARCHAR(200),  
	@Password VARCHAR(200),  
	@WindowsUserName VARCHAR(200) 
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
	  
	SELECT  SecurityUserID,
			WindowsUserName,
			UserName,
			Password,
			Cast(LanguageID as varchar(10)) as CultureInfo,
			Cast(CurrencyCulture as varchar(10)) as CurrencyCulture,
			Cast(DateCulture as varchar(10)) as DateCulture,
			--ChangePassword,
			CreatedDate,
			PasswordChangeDate,
			isReset,
			isLocked ,
			(SELECT Staff_First_Name FROM STAFF WHERE USERTABLEID=ISNULL(@USERID, 0)) as First_Name,  
			(SELECT Staff_Last_Name FROM STAFF WHERE USERTABLEID=ISNULL(@USERID, 0)) as Last_Name   
		FROM [USER] WHERE SecurityUserID = ISNULL(@USERID, 0)    
	  
	  
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
	WHERE  
	 tUserRole.SecurityUserID = @USERID  
  
END

GO

