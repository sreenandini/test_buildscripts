USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fn_InvalidOperator]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[fn_InvalidOperator]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION fn_InvalidOperator(@InputNames VARCHAR(MAX))  
RETURNS VARCHAR(MAX)  
AS  
BEGIN  
 DECLARE @Names VARCHAR(MAX)  
 DECLARE @Table TABLE (Name VARCHAR(100))
 INSERT INTO @Table
 SELECT value from dbo.UDF_GetStringTable(@InputNames,',')
 EXCEPT
 SELECT Operator_Name FROM Operator WITH(NOLOCK)  
 SELECT @Names = coalesce(@Names + ', ', '') + Name FROM @Table 
 RETURN @Names  
END 

GO

