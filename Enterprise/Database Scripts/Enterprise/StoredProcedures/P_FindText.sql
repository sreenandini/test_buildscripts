USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FindText]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[FindText]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE dbo.FindText
	@SearchString	VARCHAR(50)
AS

/*******************************************************************************
 *
 * Author		  :		Sudarsan S
 * Procedure Name :		FindText
 * Date Created	  :		18-Sep-2008
 * Input		  :		@SearchString
 * Description	  :		Lists all the code where the search string provided is available - for impact analysis 
 *
 *******************************************************************************/

BEGIN

	SET @SearchString = '%' + @SearchString + '%'

	DECLARE @Object_Name VARCHAR(50)
	DECLARE @Definition	VARCHAR(MAX)
	DECLARE @lineNo INT
	DECLARE @counter INT
	DECLARE @line	VARCHAR(500)
	DECLARE @char CHAR(1)

	DECLARE FindText CURSOR
	FOR

	SELECT OBJECT_NAME(OBJECT_ID), Definition from SYS.SQL_MODULES WHERE Definition LIKE @SearchString
	
	OPEN FindText
	FETCH NEXT FROM FindText INTO @Object_Name, @Definition

	IF @@FETCH_STATUS <> 0
	BEGIN
		PRINT 'No code was found with the provided Search String'
		CLOSE FindText
		DEALLOCATE FindText
		RETURN
	END

	PRINT 'PROCEDURE' + CHAR(9) + CHAR(9) + 'Line No' + CHAR(9) + CHAR(9) + 'Code' + CHAR(13)

	WHILE @@FETCH_STATUS = 0
	BEGIN
		
		SET @lineno = 0
		SET @counter = 1

		WHILE @counter <> LEN(@Definition)
		BEGIN
			SET @char = SUBSTRING(@definition, @counter, 1)

			IF @char = CHAR(13) 
			BEGIN
				SET @lineno = @lineno + 1
				IF (PATINDEX(@SearchString, @line) <> 0)
						PRINT @Object_Name + CHAR(9) + CHAR(9) + STR(@lineno) + CHAR(9) + CHAR(9) + LTRIM(RTRIM(@line))

				SET @line = ''
			END
			ELSE
				IF @char <> CHAR(10)
					SET @line = @line + @char

			SET @counter = @counter + 1

		END

		FETCH NEXT FROM FindText INTO @Object_Name, @Definition

	END

	CLOSE FindText
	DEALLOCATE FindText

END





GO

