USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetPayPeriods]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetPayPeriods]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[rsp_GetPayPeriods]
	 @Site_ID INT
AS
BEGIN

	SET DATEFORMAT DMY  

	SELECT 
		CP.Calendar_Period_ID,
		CONVERT(DATETIME, CP.Calendar_Period_Start_Date, 103) AS Calendar_Period_Start_Date,
		CONVERT(DATETIME, CP.Calendar_Period_End_Date, 103) AS Calendar_Period_End_Date,
		CONVERT(NVARCHAR(30), CAST(CP.Calendar_Period_Start_Date AS DATETIME), 20)+' - '+ 
		CONVERT(NVARCHAR(30), CAST(CP.Calendar_Period_End_Date AS DATETIME), 20) AS Calendar_Period
	FROM  [Site] S
		INNER JOIN Sub_Company_Calendar SCC ON SCC.Sub_Company_ID = S.Sub_Company_ID
		INNER JOIN Calendar C ON C.Calendar_ID = SCC.Calendar_ID
		INNER JOIN Calendar_Period CP ON C.Calendar_ID = CP.Calendar_ID 
	WHERE S.[Site_ID] = @Site_ID
		AND SCC.Sub_Company_Calendar_Active = 1
END

GO

