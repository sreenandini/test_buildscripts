USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fnCalcGamingDt]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
	DROP FUNCTION [dbo].[fnCalcGamingDt]
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[fnCalcGamingDt]
(
	@InputDate DATETIME  
	
)
RETURNS VARCHAR(20) AS 

BEGIN

	DECLARE @ReturnDate VARCHAR(20)

	IF DATEPART(HH,@InputDate) > 6 
		SELECT @ReturnDate = DATEADD(DD,0,DATEDIFF(DD,0,@InputDate))
	
	ELSE IF DATEPART(HH,@InputDate) < 6
 		SELECT @ReturnDate = DATEADD(DD,-1,DATEADD(DD,0,DATEDIFF(DD,0,@InputDate)))

	RETURN @ReturnDate

END
GO