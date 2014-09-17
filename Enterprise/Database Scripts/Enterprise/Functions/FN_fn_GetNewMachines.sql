USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fn_GetNewMachines]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[fn_GetNewMachines]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION fn_GetNewMachines(@Val VARCHAR(MAX))    
RETURNS VARCHAR(MAX)    
AS    
BEGIN    
 DECLARE @StockNos VARCHAR(MAX)    
 SELECT @StockNos = coalesce(@StockNos + ', ', '') + value FROM dbo.UDF_GetStringTable(@Val,',') WHERE value not in (select Machine_Stock_No from Machine WITH(NOLOCK))    
 RETURN @StockNos    
END   

GO

