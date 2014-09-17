USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fnCollectData]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[fnCollectData]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*		Function: 		dbo.fnCollectData()	
 *		Author:			Sudarsan S
 *		Created Date:	20-Apr-2008
 *		Command Line:	EXEC dbo.fnCollectData(,,,'')
 *		Purpose:		To retrieve data from the Read table (Meter data) to display in the graph screen
*/

CREATE FUNCTION [dbo].[fnCollectData](
@piInstall_ID INTEGER,
@piWeek_ID INTEGER,
@piRead_ID INTEGER,
@pcType	VARCHAR(10)
)
RETURNS INTEGER
AS 

BEGIN

DECLARE @liReturn INTEGER

	IF @pcType = 'VTP'
		SELECT @liReturn = ISNULL(VTP, 0) FROM [dbo].[Read] WHERE Installation_ID = @piInstall_ID AND Week_ID = @piWeek_ID AND Read_ID = @piRead_ID
	ELSE IF @pcType = 'GP'
		SELECT @liReturn = ISNULL(Read_Games_Bet, 0) FROM [dbo].[Read] WHERE Installation_ID = @piInstall_ID AND Week_ID = @piWeek_ID AND Read_ID = @piRead_ID
	ELSE IF @pcType = 'ADW'
		SELECT @liReturn = SUM((ISNULL(R.Read_Coins_In, 0) - ISNULL(R.Read_Coins_Out, 0) / 7) * I.Installation_Price_Per_Play)
							FROM dbo.[Read] R INNER JOIN dbo.Installation I ON R.Installation_ID = I.Installation_ID WHERE R.Installation_ID = @piInstall_ID AND R.Week_ID = @piWeek_ID GROUP BY R.Week_ID
	ELSE IF @pcType = 'AB'
		SELECT @liReturn = SUM((ISNULL(R.Read_Coins_In, 0) / 7) * I.Installation_Price_Per_Play)
							FROM dbo.[Read] R INNER JOIN dbo.Installation I ON R.Installation_ID = I.Installation_ID WHERE R.Installation_ID = @piInstall_ID AND R.Week_ID = @piWeek_ID	GROUP BY R.Week_ID
	ELSE
		SELECT @liReturn = (ISNULL(READ_COIN_DROP, 0) + ISNULL(READ_RDC_TRUE_COIN_IN, 0)) - (ISNULL(READ_COINS_IN, 0) - ISNULL(READ_COINS_OUT, 0))
							FROM [dbo].[Read] WHERE Installation_ID = @piInstall_ID AND Week_ID = @piWeek_ID AND Read_ID = @piRead_ID
	
RETURN ISNULL(@liReturn, 0)

END

GO

