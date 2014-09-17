USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UDF_GetStringTable]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[UDF_GetStringTable]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[UDF_GetStringTable]
(
	@String     VARCHAR(8000),
	@Delimiter  CHAR(1)
)
RETURNS @StringArray TABLE ([value] VARCHAR(8000))
AS
BEGIN
	DECLARE @index  INT        
	DECLARE @slice  VARCHAR(8000)        
	
	SELECT @index = 1        
	IF LEN(@String) < 1
	   OR @String IS NULL
	    RETURN        
	
	WHILE @index != 0
	BEGIN
	    SET @index = CHARINDEX(@Delimiter, @String)        
	    IF @index != 0
	        SET @slice = LEFT(@String, @index - 1)
	    ELSE
	        SET @slice = @String        
	    
	    IF (LEN(@slice) > 0)
	        INSERT INTO @StringArray
	          (
	            [value]
	          )
	        VALUES
	          (
	            @slice
	          )        
	    
	    SET @String = RIGHT(@String, LEN(@String) - @index)        
	    IF LEN(@String) = 0
	        BREAK
	END 
	RETURN
END

GO

