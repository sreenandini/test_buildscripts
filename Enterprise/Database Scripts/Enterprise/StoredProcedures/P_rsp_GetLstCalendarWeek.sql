USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetLstCalendarWeek]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetLstCalendarWeek]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[rsp_GetLstCalendarWeek]
(
    @Calendar_ID                INT,
    @Calendar_Week_Number     INT,
    @Calendar_Week_ID         INT   
)
AS
BEGIN
	SELECT *
	FROM   Calendar_Week
	WHERE  Calendar_ID                    = @Calendar_ID
	       AND Calendar_Week_Number     = @Calendar_Week_Number
	       AND Calendar_Week_ID <> @Calendar_Week_ID
END


GO

