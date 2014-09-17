USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fn_GetDeclaredHandpayFromCollection]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[fn_GetDeclaredHandpayFromCollection]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[fn_GetDeclaredHandpayFromCollection] (@BatchNo INT)
RETURNS float
AS
BEGIN
DECLARE @DecHandpay float

SELECT 
	@DecHandpay = DecHandpay
FROM 
	VW_CollectionData 
WHERE 
	Collection_id = @BatchNo 
	
RETURN @DecHandpay

END

GO

