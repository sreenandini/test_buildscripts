/************************************************************
 * Capped Game List Report
 * Time: 9/20/2013 12:51:56 PM
 ************************************************************/

USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_Report_CappedGameListReport]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_Report_CappedGameListReport]
GO 
  
CREATE PROCEDURE rsp_Report_CappedGameListReport(
    @Company     INT = 0,
    @SubCompany  INT = 0,
    @Site        INT = 0,
    @OrderBy     INT,
    @StartDate   DATETIME,
    @EndDate     DATETIME,
    @SiteIDList  VARCHAR(MAX)
)
AS
	SET NOCOUNT ON 
	
	IF @company = 0
	    SET @company = NULL  
	
	IF @subcompany = 0
	    SET @subcompany = NULL  
	
	IF @site = 0
	    SET @site = NULL
	
	SET DATEFORMAT ymd
	
	BEGIN
		SELECT GC.Slot,
		       GC.Stand,
		       GC.ReservedCardNo,
		       CASE GC.ReservedBy
		            WHEN '' THEN '-'
		            ELSE GC.ReservedBy
		       END AS ReservedBy,
		       CASE 
		            WHEN GC.ReservedBy <> GC.ReservedFor THEN GC.ReservedFor
		            ELSE '-'
		       END AS ReservedForCardNo,
		       GC.SessionStartTime,
		       GC.SessionEndTime,
		       GC.TotalTimeReserved,
		       SITE.Site_Name,
		       CASE GC.ReleasedBy
		            WHEN '' THEN '-'
		            ELSE GC.ReleasedBy
		       END AS ReleasedBy,
		       CASE GC.ReleasedCardNo
		            WHEN '' THEN '-'
		            ELSE GC.ReleasedCardNo
		       END AS ReleasedCardNo
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
		            AND (@SiteIDList IS NOT NULL AND SITE.Site_ID IN(SELECT DATA FROM fnSplit(@SiteIDList,',')))
		WHERE  (
		           GC.SessionStartTime >= @StartDate
		           AND ISNULL(GC.SessionEndTime, GETDATE()) < = @EndDate
		)
	
		ORDER BY 
				 CASE WHEN @OrderBy = 1 THEN Slot END ASC,  
				 CASE WHEN @OrderBy = 2 THEN Stand END ASC,  
				 CASE WHEN @OrderBy = 3 THEN GC.SessionStartTime END ASC  
		       
	END
	
