USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ASPNET_SP_GetUser]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ASPNET_SP_GetUser]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[ASPNET_SP_GetUser]
	(
	@ID INT = 0,
	@Name VARCHAR(200) = ''
	)	
AS	
BEGIN

SELECT 
	SecurityUserID, 
	WindowsUserName, 
	UserName, 
	Password  
FROM 
	[USER] 
WHERE 
	(@ID = 0 OR SecurityUserID = ISNULL(@ID, 0))
	AND (@Name = '' OR	UserName = @Name)

END

GO

