USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RSP_GetUnprocessedRecordsFromImportHistory]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[RSP_GetUnprocessedRecordsFromImportHistory]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--------------------------------------------------------------------------                 
--                
-- Description: Gets All Unprocessed Data from the import history table.          
--              Rule/Condition: All records that Have IH_Status = 0 would          
--              be listed.  After Listing Updated IH_Status = 1 to Mark    
--              it as Inprogress Records.  So that the same record is not     
--              picked for processing.    
--                
-- Inputs:      NONE          
--                
-- Outputs:     Displays the list of Records from the Import_History Table                
--                
-- RETURN:      NONE          
--                
-- =======================================================================                
--                 
-- Revision History                
--                 
-- NaveenChander     27/05/2008     Created   
-- Siva				 18/09/2008		Added WITH (NOLOCK) for selects    
-- Sudarsan S		 16/07/2009		resetting the status to 0         
---------------------------------------------------------------------------          
          
CREATE PROCEDURE [dbo].[RSP_GetUnprocessedRecordsFromImportHistory]    (@SITE_CODE As Varchar(50))                
AS                    
BEGIN                  
    -- SELECT TOP 100 RECORDS WITH STATUS 0                  
    -- ONLY 100 IS SELECTED FOR BETTER PERFORMANCE

--CREATE TABLE #Temp
--(
--	[IH_ID] [int] ,
--	[IH_EH_ID] [int] ,
--	[IH_Site_Code] [varchar](50) ,
--	[IH_Type] [varchar](50) ,
--	[IH_Details] [xml] ,
--	[IH_Status] [int] ,
--	[IH_Received_Date] [datetime] ,
--	[IH_Processed_Date] [datetime] ,
--	[IH_ExportResult] [varchar](8000) 
--)

DECLARE @Temp TABLE
(
	[IH_ID] [int] ,
	[IH_EH_ID] [int] ,
	[IH_Site_Code] [varchar](50) ,
	[IH_Type] [varchar](50) ,
	[IH_Details] [xml] ,
	[IH_Status] [int] ,
	[IH_Received_Date] [datetime] ,
	[IH_Processed_Date] [datetime] ,
	[IH_ExportResult] [varchar](8000) 
)

SET @SITE_CODE = LTRIM(RTRIM(@SITE_CODE))

	IF EXISTS(SELECT 1 FROM dbo.Import_History WITH (NOLOCK) WHERE IH_STATUS = 1 AND IH_Site_Code = @SITE_CODE)
	BEGIN
		EXEC USP_ResetImportHistoryRecords @SiteCode = @SITE_CODE
	END

    IF NOT EXISTS (SELECT 1 FROM IMPORT_HISTORY WITH (NOLOCK) WHERE IH_Status = -1 And IH_Site_Code = @SITE_CODE)      
    BEGIN
        INSERT INTO @Temp
        SELECT
            TOP 200 *          
        FROM
            IMPORT_HISTORY WITH (NOLOCK)
        WHERE
            ISNULL(IH_STATUS,0) = 0
            AND @SITE_CODE = IH_Site_Code
        ORDER BY
            IH_ID, IH_EH_ID

    END      
    ELSE      
    BEGIN
        --Reset All Inprogress Records to Zarro
        EXEC USP_ResetImportHistoryRecords @SiteCode = @SITE_CODE
        INSERT INTO @Temp SELECT TOP 100 * FROM IMPORT_HISTORY WITH (NOLOCK) WHERE ISNULL(IH_STATUS,0) = -1 AND @SITE_CODE = IH_Site_Code

    END      

       
        SELECT *  FROM @TEMP ORDER BY IH_ID

        UPDATE IH                
            SET IH_STATUS = 1                
        FROM IMPORT_HISTORY IH                 
            INNER JOIN @TEMP tT On tT.IH_ID = IH.IH_ID         

END 

GO

