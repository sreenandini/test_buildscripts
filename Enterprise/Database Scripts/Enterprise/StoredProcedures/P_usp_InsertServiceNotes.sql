USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_InsertServiceNotes]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_InsertServiceNotes]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[usp_InsertServiceNotes]
	@Service_ID	VARCHAR(20),
	@Notes	VARCHAR(1000),
	@User	VARCHAR(100)
AS

BEGIN

	INSERT INTO dbo.Service_Notes
	SELECT  SUBSTRING(@Service_ID, 1, CHARINDEX('/', @Service_ID) - 1),
			SecurityUserID,
			@Notes,
			CONVERT(VARCHAR, GETDATE(), 106) + ' ' + CONVERT(VARCHAR, GETDATE(), 108),
			0, 0, 0,
			NULL
	  FROM  dbo.[User]
	 WHERE  UserName = @User

	IF @@ROWCOUNT > 0
		RETURN SCOPE_IDENTITY()
	ELSE
		RETURN 0
END
GO		
