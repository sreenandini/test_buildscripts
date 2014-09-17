USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ASPNET_SP_RemoveUser]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ASPNET_SP_RemoveUser]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[ASPNET_SP_RemoveUser](@Username VARCHAR(200), @DeletedUserID INT)
AS
BEGIN
	IF EXISTS (SELECT * FROM [USER] WHERE UserName = @username )	
		SELECT 'ERROR:01' AS RESULT
	ELSE
	BEGIN
		INSERT INTO [INACTIVEUSER] (SecurityUserID, DELETEDBY) SELECT SecurityUserID,  @DeletedUserID FROM [USER] WHERE UserName = @username
		DELETE FROM [USER] WHERE UserName = @username
		SELECT 'SUCCESS' AS RESULT
	END
END

GO

