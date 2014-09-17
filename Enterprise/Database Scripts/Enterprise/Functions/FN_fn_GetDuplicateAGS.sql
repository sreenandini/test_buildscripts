USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fn_GetDuplicateAGS]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[fn_GetDuplicateAGS]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION fn_GetDuplicateAGS(@AGS VARCHAR(MAX))  
RETURNS VARCHAR(MAX)  
AS  
BEGIN  
 DECLARE @StockNos VARCHAR(MAX)  
 SELECT @StockNos = coalesce(@StockNos + ', ', '') + Machine_Stock_No FROM Machine WITH(NOLOCK) WHERE ActAssetNo + GMUNo + ActSerialNo in (select value from dbo.UDF_GetStringTable(@AGS,','))  
 RETURN @StockNos  
END  

GO

