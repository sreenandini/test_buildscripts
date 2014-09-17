USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_UpdateTransactionKeyStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_UpdateTransactionKeyStatus]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE dbo.usp_UpdateTransactionKeyStatus
	@TransactionKeyId INT,
	@UserID INT
AS
/*****************************************************************************************************
DESCRIPTION : Mark TransactionKey has VOID  
CREATED DATE: 
MODULE            :   Site Maintenance in Enterprise Client    
CHANGE HISTORY :
------------------------------------------------------------------------------------------------------
AUTHOR							MODIFIED DATE	DESCRIPTON
------------------------------------------------------------------------------------------------------

*****************************************************************************************************/

BEGIN
	SET NOCOUNT ON 
	
	UPDATE TransactionKeys
	SET    Void = 1,
	       ExpiryDate = GETDATE() - 1,
	       ModifiedDate = GETDATE(),
	       [Userid] = @UserID
	WHERE  TransactionKeyId = @TransactionKeyId
END

GO

