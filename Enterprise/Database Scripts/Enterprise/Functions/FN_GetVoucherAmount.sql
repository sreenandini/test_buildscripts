USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetVoucherAmount]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[GetVoucherAmount]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[GetVoucherAmount](@CollectionId INT, @InstallationID INT, @iTicketType INT) 
RETURNS FLOAT
AS
BEGIN
RETURN (SELECT SUM(iAmount) FROM VOUCHER 
		INNER JOIN COLLECTION_TICKET 
		ON strBarCode = CT_Barcode 
		WHERE CT_Inserted_Installation_ID = @InstallationID
		AND CT_Inserted_Collection_ID = @CollectionId
		AND Ticket_Type = @iTicketType
		AND iRedeemCollectionCompleted = 1)
END


GO

