USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_DeleteShareHolder]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_DeleteShareHolder]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE usp_DeleteShareHolder(@ShareHolderId INT, @Status INT OUTPUT)
AS
BEGIN
	UPDATE ShareHolders
	SET    SysDelete = 1
	WHERE  ShareHolderId = @ShareHolderId
	
	SET @Status = 0
	
	IF @@ROWCOUNT > 0
	    SET @Status = 1      
	
	INSERT INTO dbo.Export_History
	  (
	    EH_Date,
	    EH_Reference1,
	    EH_Type,
	    EH_Site_Code
	  )
	SELECT GETDATE(),
	       @ShareHolderId,
	       'SHAREHOLDER',
	       Site_Code
	FROM   SITE WITH(NOLOCK)
	WHERE  Site_Enabled = 1
	       AND sitestatus = 'FULLYCONFIGURED'
END

GO

