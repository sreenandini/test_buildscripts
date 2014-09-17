USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Fn_GetIntTableFromStringList]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[Fn_GetIntTableFromStringList]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION dbo.Fn_GetIntTableFromStringList
(
	@StrList VARCHAR(2000)
)
RETURNS @ParsedList TABLE 
        (IntItem INT)
AS
BEGIN
	DECLARE @IntItem  INT,
	        @Pos      INT      
	
	SET @StrList = LTRIM(RTRIM(@StrList)) + ','      
	SET @Pos = CHARINDEX(',', @StrList, 1)      
	
	IF REPLACE(@StrList, ',', '') <> ''
	BEGIN
	    WHILE @Pos > 0
	    BEGIN
	        SET @IntItem = LTRIM(RTRIM(LEFT(@StrList, @Pos - 1)))      
	        IF @IntItem >= 0
	        BEGIN
	            INSERT INTO @ParsedList
	              (
	                IntItem
	              )
	            VALUES
	              (
	                CAST(@IntItem AS INT)
	              ) --Use Appropriate conversion
	        END
	        
	        SET @StrList = RIGHT(@StrList, LEN(@StrList) - @Pos)      
	        SET @Pos = CHARINDEX(',', @StrList, 1)
	    END
	END
	
	RETURN
END


GO

