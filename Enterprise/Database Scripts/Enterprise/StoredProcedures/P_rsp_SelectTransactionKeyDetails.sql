USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_SelectTransactionKeyDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_SelectTransactionKeyDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE rsp_SelectTransactionKeyDetails
@TransactionFlagID INT,
@SiteId INT
AS
BEGIN
SELECT 1 
FROM 
	TransactionKeys TK  
JOIN 
	[TransactionFlag] TF 
ON 
	TF.[TransactionFlagid] = TK.[TransactionFlagid] 
Where 
	TK.SiteID = @SiteId And 
	TF.[TransactionFlagid] = @TransactionFlagID  And 
	ISNULL(TK.[Void],0) =0 And 
	TK.[ExpiryDate]>TK.[CreatedDate]	
END


GO

