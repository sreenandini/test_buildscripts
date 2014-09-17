USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[USP_GetUnprocessedRecordsFromImportHistory]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[USP_GetUnprocessedRecordsFromImportHistory]
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
---------------------------------------------------------------------------          
          
CREATE  PROCEDURE [dbo].[USP_GetUnprocessedRecordsFromImportHistory]    (@SITE_CODE As Varchar(50))      
AS          
BEGIN        

-- DIFFERENT Error codes 
-- 1 RECORDS BEING PROCESSED
-- -1 PREVIOUS RECORD NOT IMPORTED
-- -2 PREVIOUS RECORD NOT PROCESSED
-- -3 UNKNOWN ERROR
IF NOT EXISTS (SELECT 1 FROM IMPORT_HISTORY WHERE IH_STATUS IN (1 , -1, -2, -3) AND LTRIM(RTRIM(UPPER(@SITE_CODE))) = LTRIM(RTRIM(UPPER(IH_Site_Code))) )
BEGIN
	-- SELECT TOP 100 RECORDS WITH STATUS 0        
	-- ONLY 100 IS SELECTED FOR BETTER PERFORMANCE   
	SELECT        
		TOP 100        
		 IH_ID          
		,IH_EH_ID          
		,IH_Site_Code          
		,IH_Type          
		,IH_Details          
		,IH_Status          
		,IH_Received_Date          
		,IH_Processed_Date        
		,IH_ExportResult      
		INTO #TEMP    
	FROM           
		IMPORT_HISTORY           
	WHERE           
		ISNULL(IH_STATUS,0) = 0      
		AND LTRIM(RTRIM(UPPER(@SITE_CODE))) = LTRIM(RTRIM(UPPER(IH_Site_Code)))      
	ORDER BY        
	   IH_EH_ID ,IH_TYPE    
	      
	SELECT * FROM #TEMP ORDER BY IH_ID      
	      
	UPDATE IH      
	SET IH_STATUS = 1      
	FROM IMPORT_HISTORY IH       
	INNER JOIN #TEMP tT On tT.IH_ID = IH.IH_ID      
END
ELSE
BEGIN
	SELECT * FROM IMPORT_HISTORY WHERE 1 <> 1   
END
END  
GO

