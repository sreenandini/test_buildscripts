USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_InsertShareHolderDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_InsertShareHolderDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- Author:		Aishwarrya v s
-- Create date: 8th October 2012
-- Description:	Insert ShareHolder Details
-- =============================================
CREATE PROCEDURE usp_InsertShareHolderDetails
	@ShareHolderName VARCHAR(50),
	@ShareHolderDescription VARCHAR(255)
AS
BEGIN
	DECLARE @ShareHolderId INT	
	
	INSERT INTO ShareHolders
	  (
	    ShareHolderName,
	    ShareHolderDescription
	  )
	VALUES
	  (
	    @ShareHolderName,
	    @ShareHolderDescription
	  )
	
	SET @ShareHolderId = CAST(SCOPE_IDENTITY() AS INT)
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

