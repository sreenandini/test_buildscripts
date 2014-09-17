USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ASPNET_SP_RemoveRole]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ASPNET_SP_RemoveRole]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[ASPNET_SP_RemoveRole]	
	(
	@RoleID int	
	)	
AS
BEGIN
	DELETE FROM Role WHERE SecurityRoleID = @RoleID
	If @@Rowcount > 0
		SELECT 'SUCCESS' AS RESULT
	Else
		SELECT 'ERROR:02' AS RESULT
END

GO

