USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_UpdateShareHolderDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_UpdateShareHolderDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- Author:		Aishwarrya v s
-- Create date: 8th October 2012
-- Description:	Update ShareHolder Details
-- =============================================
CREATE PROCEDURE usp_UpdateShareHolderDetails
	@ShareHolderId INT,
	@ShareHolderName VARCHAR(50),
	@ShareHolderDescription VARCHAR(255)
AS
BEGIN
	UPDATE ShareHolders
	SET    ShareHolderName = @ShareHolderName,
	       ShareHolderDescription = @ShareHolderDescription,
	       DateModified = GETDATE()
	WHERE  ShareHolderId = @ShareHolderId
	
	
	IF @@ROWCOUNT > 0
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

