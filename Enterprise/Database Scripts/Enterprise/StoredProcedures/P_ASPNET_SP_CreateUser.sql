USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ASPNET_SP_CreateUser]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ASPNET_SP_CreateUser]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[ASPNET_SP_CreateUser](@username VARCHAR(200), @password VARCHAR(200), @windowsUserName VARCHAR(200))
AS
BEGIN
	IF EXISTS (SELECT * FROM [USER] WHERE UserName = @username )	
		SELECT 'ERROR:01' AS RESULT
	ELSE
	BEGIN
		INSERT INTO [USER] (UserName, WindowsUserName, Password) VALUES (@username, @windowsUserName, @password)
		SELECT 'SUCCESS' AS RESULT
	END
END

GO

