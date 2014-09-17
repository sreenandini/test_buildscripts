USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[USP_GetUnprocessedRecordsFromImportHistoryForSites]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[USP_GetUnprocessedRecordsFromImportHistoryForSites]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[USP_GetUnprocessedRecordsFromImportHistoryForSites]
(
    @Site_List       AS VARCHAR(8000),
    @Previous_IH_ID  INT,
    @RecordsCount    INT
)
AS
BEGIN
	DECLARE @SiteCode TABLE 
	        ([Site_Code] [varchar](50))        
	
	INSERT @SiteCode
	SELECT [value] AS Site_Code
	FROM   UDF_GetStringTable(@Site_List, ',')      
	
	IF @Previous_IH_ID = 0
	BEGIN
	    SELECT TOP(@RecordsCount) IH.IH_ID,
	           IH_EH_ID,
	           IH_Site_Code,
	           IH_Type,
	           IH_Details,
	           IH_Status,
			   S.WebURL AS '114Site_WebURL'
     FROM   IMPORT_HISTORY IH WITH (NOLOCK)  
            INNER JOIN @SiteCode SC  
                 ON  IH.IH_Site_Code = SC.Site_Code
            INNER JOIN [Site] S ON SC.Site_Code = S.Site_Code
	    WHERE  IH_STATUS < 100
	    ORDER BY
	           IH_ID
	END
	ELSE
	BEGIN
	    SELECT TOP(@RecordsCount) IH.IH_ID,
	           IH_EH_ID,
	           IH_Site_Code,
	           IH_Type,
	           IH_Details,
	           IH_Status,
            S.WebURL AS '114Site_WebURL'
     FROM   IMPORT_HISTORY IH WITH (NOLOCK)  
            INNER JOIN @SiteCode SC  
                 ON  IH.IH_Site_Code = SC.Site_Code
            INNER JOIN [Site] S ON SC.Site_Code = S.Site_Code
	    WHERE  IH_ID >= @Previous_IH_ID
	           AND IH_STATUS < 100
	    ORDER BY
	           IH_ID
	END
END

GO

