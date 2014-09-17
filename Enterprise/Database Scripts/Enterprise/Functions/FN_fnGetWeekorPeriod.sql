USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fnGetWeekorPeriod]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[fnGetWeekorPeriod]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*		Function: 		dbo.[fnGetWeekorPeriod]()	
 *		Author:			Sudarsan S
 *		Created Date:	20-May-2008
 *		Command Line:	EXEC dbo.[fnGetWeekorPeriod]()
 *		Purpose:		To retrieve Week_ID and Period_ID
 *	
 *	Sudarsan S		09/07/09	pick the PeriodID from Calendar Period table
*/

CREATE FUNCTION [dbo].[fnGetWeekorPeriod](
@piSub_Company_ID INTEGER,
@pvcDate	VARCHAR(30),
@pvcWeekorPeriod	VARCHAR(6)
)
RETURNS INTEGER
AS 

BEGIN

DECLARE @liID	INT

		IF @pvcWeekorPeriod = 'Week'
		BEGIN
			SELECT @liID = CW.Calendar_Week_ID
			FROM dbo.Sub_Company_Calendar SCC 
			INNER JOIN dbo.Calendar C ON C.Calendar_ID = SCC.Calendar_ID
			INNER JOIN dbo.Calendar_Week CW ON C.Calendar_ID = CW.Calendar_ID
			WHERE SCC.Sub_Company_ID = @piSub_Company_ID
			AND CONVERT(DATETIME, @pvcDate, 103) BETWEEN CONVERT(DATETIME, CW.Calendar_Week_Start_Date, 103) AND CONVERT(DATETIME, Calendar_Week_End_Date, 103)
		END
		ELSE
		BEGIN
			SELECT @liID = CP.Calendar_Period_ID
			FROM dbo.Sub_Company_Calendar SCC 
			INNER JOIN dbo.Calendar C ON C.Calendar_ID = SCC.Calendar_ID
			INNER JOIN dbo.Calendar_Period CP ON C.Calendar_ID = CP.Calendar_ID
			WHERE SCC.Sub_Company_ID = @piSub_Company_ID
			AND CONVERT(DATETIME, @pvcDate, 103) BETWEEN CONVERT(DATETIME, CP.Calendar_Period_Start_Date, 103) AND CONVERT(DATETIME, CP.Calendar_Period_End_Date, 103)
		END

RETURN ISNULL(@liID, 0)

END


GO

