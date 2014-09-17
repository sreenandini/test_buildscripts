USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ASPNET_SP_AddWindowsUserName]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ASPNET_SP_AddWindowsUserName]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[ASPNET_SP_AddWindowsUserName](@username VARCHAR(200), @password VARCHAR(200))
AS
BEGIN
	IF NOT EXISTS (SELECT * FROM [USER] WHERE UserName = @username )	
		SELECT 'ERROR:01' AS RESULT
	ELSE
	BEGIN
		UPDATE [USER] SET Password = @password WHERE UserName = @username AND Password = @Password
		SELECT 'SUCCESS' AS RESULT
	END
END

GO

