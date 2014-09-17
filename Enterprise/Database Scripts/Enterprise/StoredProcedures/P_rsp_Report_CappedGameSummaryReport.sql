/************************************************************
 * Reterives Game Capping Details to display in the Report.
 * Time: 06/09/2013 16:57:20

 select * from GameCappingDetails
 ************************************************************/

USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_Report_CappedGameSummaryReport]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_Report_CappedGameSummaryReport]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE rsp_Report_CappedGameSummaryReport(
    @Company     INT = 0,
    @SubCompany  INT = 0,
    @Site        INT = 0,
    @OrderBy     INT,
    @StartDate   DATETIME,
    @EndDate     DATETIME,
    @SiteIDList  VARCHAR(MAX)
)
AS
	SET DATEFORMAT ymd
	
	IF @company = 0
	    SET @company = NULL
	
	IF @subcompany = 0
	    SET @subcompany = NULL
	
	IF @site = 0
	    SET @site = NULL    	
	
	BEGIN
		SELECT GC.Slot,
		       GC.Stand,
		       GC.TotalTimeReserved,
		       SITE.SITE_NAME
		FROM   GameCappingDetails GC
		       JOIN SITE
		            ON  SITE.Site_Code = GC.Site_Code
		       JOIN sub_company
		            ON  SITE.sub_company_id = sub_company.sub_company_id
		       JOIN company
		            ON  sub_company.company_id = company.company_id
		            AND (
		                    (@company IS NULL)
		                    OR (@company IS NOT NULL AND company.company_id = @company)
		                )
		            AND (
		                    (@subcompany IS NULL)
		                    OR (
		                           @subcompany IS NOT NULL
		                           AND SITE.sub_company_id = @subcompany
		                       )
		                )
		            AND (
		                    (@site IS NULL)
		                    OR (@site IS NOT NULL AND SITE.site_id = @site)
		                )
		            WHERE (
		                    (GC.SessionStartTime BETWEEN @startdate AND @enddate)		                   
						and (GC.SessionEndTime BETWEEN @startdate AND @enddate)
						AND (@SiteIDList IS NOT NULL AND SITE.Site_ID IN(SELECT DATA FROM fnSplit(@SiteIDList,',')))
		                )
		ORDER BY
		       CASE @OrderBy
		            WHEN 1 THEN GC.Slot
		            WHEN 2 THEN GC.Stand
		       END;
	END
	