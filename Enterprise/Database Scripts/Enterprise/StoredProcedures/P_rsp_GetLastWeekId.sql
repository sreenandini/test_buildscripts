/************************************************************
 * Code formatted by SoftTree SQL Assistant © v4.8.29
 * Time: 7/31/2013 11:32:21 AM
 ************************************************************/

USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_GetLastWeekId]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_GetLastWeekId]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER OFF
GO
/*
declare @p5 int
set @p5=NULL
exec sp_executesql N'EXEC @RETURN_VALUE = [dbo].[rsp_GetLastWeekId] @SiteId = @p0, @Weeks = @p1',N'@p0 int,@p1 int,@RETURN_VALUE int output',@p0=1,@p1=60,@RETURN_VALUE=@p5 output
select @p5 
 Select * from Calendar_Week CW where (GETDATE() BETWEEN CONVERT(DATETIME, CW.Calendar_Week_Start_Date, 103) and CONVERT(DATETIME, CW.Calendar_Week_End_Date, 103)) OR GETDATE() >= CONVERT(DATETIME, CW.Calendar_Week_End_Date, 103)
 Select * from calendar

Select getdate()
*/

CREATE PROCEDURE rsp_GetLastWeekId(@SiteId INT, @Weeks INT)
AS
BEGIN
	SET NOCOUNT ON          
	
	IF (@Weeks < 0)
	BEGIN
	    SELECT DISTINCT Calendar_Week_ID,
	           CONVERT(DATETIME, CW.Calendar_Week_Start_Date, 103) AS 
	           Week_Start_Date
	    FROM   SITE S WITH(NOLOCK)
	           INNER JOIN Sub_Company_Calendar SC WITH(NOLOCK)
	                ON  S.Sub_Company_ID = SC.Sub_Company_ID
	           INNER JOIN Calendar_Week CW WITH(NOLOCK)
	                ON  SC.Calendar_ID = CW.Calendar_ID
	           INNER JOIN Collection_Details CD WITH(NOLOCK)
	                ON  CD.Week_ID = CW.Calendar_Week_ID
	    WHERE  S.Site_ID = @SiteId
	           AND (
	                   GETDATE() >= CONVERT(DATETIME, CW.Calendar_Week_End_Date, 103)
	                   OR (
	                          GETDATE() BETWEEN CONVERT(DATETIME, CW.Calendar_Week_Start_Date, 103) 
	                          AND CONVERT(DATETIME, CW.Calendar_Week_End_Date, 103)
	                      )
	               )
	    ORDER BY
	           CONVERT(DATETIME, CW.Calendar_Week_Start_Date, 103) DESC
	END
	ELSE
	BEGIN
	    SELECT DISTINCT TOP(@Weeks) Calendar_Week_ID,
	           CONVERT(DATETIME, CW.Calendar_Week_Start_Date, 103) AS 
	           Week_Start_Date
	    FROM   SITE S WITH(NOLOCK)
	           INNER JOIN Sub_Company_Calendar SC WITH(NOLOCK)
	                ON  S.Sub_Company_ID = SC.Sub_Company_ID
	           INNER JOIN Calendar_Week CW WITH(NOLOCK)
	                ON  SC.Calendar_ID = CW.Calendar_ID
	           INNER JOIN Collection_Details CD WITH(NOLOCK)
	                ON  CD.Week_ID = CW.Calendar_Week_ID
	    WHERE  S.Site_ID = @SiteId
	           AND (
	                   GETDATE() >= CONVERT(DATETIME, CW.Calendar_Week_End_Date, 103)
	                   OR (
	                          GETDATE() BETWEEN CONVERT(DATETIME, CW.Calendar_Week_Start_Date, 103) 
	                          AND CONVERT(DATETIME, CW.Calendar_Week_End_Date, 103)
	                      )
	               )
	    ORDER BY
	           CONVERT(DATETIME, CW.Calendar_Week_Start_Date, 103) DESC
	END
END
GO







