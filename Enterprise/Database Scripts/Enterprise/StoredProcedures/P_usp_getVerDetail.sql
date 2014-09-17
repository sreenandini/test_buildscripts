USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_getVerDetail]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_getVerDetail]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE usp_getVerDetail(@Ver VARCHAR(50), @SP INT = 0 OUTPUT, @EP INT = 0 OUTPUT)
AS
BEGIN
	
	IF ( @Ver = '12.1' ) 
		SET @Ver='12.1.1'
		
	SELECT @Ver = LTRIM(
	           RTRIM(
	               REPLACE (
	                   (
	                       LTRIM(RTRIM(REPLACE(REPLACE (@Ver, '12.1.1', ''), 'SP', '')))
	                   ),
	                   'EP',
	                   ''
	               )
	           )
	       )
	
	SELECT @SP = CASE 
	                  WHEN CHARINDEX(' ', @Ver, 0) > 0 THEN SUBSTRING(@Ver, 0, CHARINDEX(' ', @Ver, 0))
	                  ELSE @Ver
	             END,
	       @EP = CASE 
	                  WHEN CHARINDEX(' ', @Ver, 0) > 0 THEN SUBSTRING(@Ver, CHARINDEX(' ', @Ver, 0), LEN(@Ver))
	                  ELSE 0
	             END
END             

GO

