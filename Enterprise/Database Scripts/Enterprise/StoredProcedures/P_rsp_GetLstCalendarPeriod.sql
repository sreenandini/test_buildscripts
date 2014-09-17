USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetLstCalendarPeriod]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetLstCalendarPeriod]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[rsp_GetLstCalendarPeriod]
(
    @Calendar_ID                INT,
    @Calendar_Period_Number     INT,
    @Calendar_Period_ID         INT   
)
AS
BEGIN
	SELECT *
	FROM   Calendar_Period
	WHERE  Calendar_ID                    = @Calendar_ID
	       AND Calendar_Period_Number     = @Calendar_Period_Number
	       AND Calendar_Period_ID <> @Calendar_Period_ID
END


GO

