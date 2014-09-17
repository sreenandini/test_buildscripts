USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetEftIn]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[GetEftIn]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[GetEftIn] (@collection_ID INT)
RETURNS Float
AS
BEGIN
DECLARE @Amount float

if (@collection_ID >0)
BEGIN
Select @Amount = ISNULL(SUM(ISNULL(Promo_Cashable_EFT_OUT,0) +ISNULL(NonCashable_EFT_OUT,0) + ISNULL(WAT_Out,0)),0) from AFT_Transactions 
WHERE Transaction_Type = 'WithDrawal Complete' AND Collection_No = @collection_ID 
END
ELSE
	set @Amount = 0
RETURN @Amount
END


GO

