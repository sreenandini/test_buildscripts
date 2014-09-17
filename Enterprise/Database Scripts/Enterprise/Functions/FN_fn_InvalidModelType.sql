USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fn_InvalidModelType]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[fn_InvalidModelType]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION fn_InvalidModelType(@InputNames VARCHAR(MAX))  
RETURNS VARCHAR(MAX)  
AS  
BEGIN  
 DECLARE @Names VARCHAR(MAX)  
 DECLARE @Table TABLE (Name VARCHAR(100))
 INSERT INTO @Table
 SELECT value from dbo.UDF_GetStringTable(@InputNames,',')
 EXCEPT
 SELECT MT_Model_Name FROM Model_Type WITH(NOLOCK)  
 SELECT @Names = coalesce(@Names + ', ', '') + Name FROM @Table 
 RETURN @Names  
END 

GO

