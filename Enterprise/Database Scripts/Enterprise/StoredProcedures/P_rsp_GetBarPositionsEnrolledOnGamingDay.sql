USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetBarPositionsEnrolledOnGamingDay]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetBarPositionsEnrolledOnGamingDay]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- exec dbo.rsp_GetBarPositionsEnrolledOnGamingDay '2013-03-19 00:00:00:000', 9
CREATE PROCEDURE dbo.rsp_GetBarPositionsEnrolledOnGamingDay
(
    @date          DATETIME,
    @starthour     INT
)
AS
BEGIN
	SET NOCOUNT ON
	-- BEGIN
	
	DECLARE @GStartDate       DATETIME
	DECLARE @GEndDate         DATETIME      
	
	SET @GStartDate = DATEADD(hh, @starthour, DATEADD(d, 0, DATEDIFF(d, 0, @date)))
	SET @GEndDate = DATEADD(hh, @starthour, DATEADD(d, 0, DATEDIFF(d, 0, @date) + 1))
	
	SELECT BP.Bar_Position_Name
	FROM   dbo.Bar_Position BP
	       INNER JOIN dbo.Installation ins
	            ON  bp.Bar_Position_ID = ins.Bar_Position_ID
	WHERE  INS.Installation_Start_Date >= @GStartDate
	       AND INS.Installation_Start_Date <= @GEndDate
	GROUP BY
	       BP.Bar_Position_Name
	ORDER BY BP.Bar_Position_Name
	
	-- END
	SET NOCOUNT OFF
END

GO

