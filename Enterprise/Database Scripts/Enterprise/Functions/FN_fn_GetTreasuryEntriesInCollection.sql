USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fn_GetTreasuryEntriesInCollection]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[fn_GetTreasuryEntriesInCollection]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[fn_GetTreasuryEntriesInCollection] (@BatchNo INT, @InstallationNo INT, @Treasury_Type Varchar(200) = 'REFILL')
RETURNS float
AS
BEGIN
DECLARE @TreasuryAmout float

SELECT 
	@TreasuryAmout = Sum(Treasury_Amount) 
FROM 
	Treasury_Entry 
WHERE 
	Collection_Id = @BatchNo 
	AND Installation_Id = @InstallationNo 
	And Treasury_Type = @Treasury_Type

RETURN @TreasuryAmout

END

GO

