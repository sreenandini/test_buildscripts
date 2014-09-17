USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_ImportMaintenanceReasonCategory]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_ImportMaintenanceReasonCategory]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--------------------------------------------------------------------------                           
--                          
-- Description: Get the data from XML and insert into MaintenanceReasonCategory
--                          
-- Inputs:      NONE                    
--                          
-- Outputs:     
--                          

-- =======================================================================                          
--                           
-- Revision History                          
--                           
-- P SaravanaKumar     04/05/2009     Created    
---------------------------------------------------------------------------       


CREATE PROCEDURE [dbo].[usp_ImportMaintenanceReasonCategory]   
@doc xml   
AS  
BEGIN  
	DECLARE @handle INT
	EXEC sp_xml_preparedocument @handle OUTPUT, @doc  
	
	CREATE TABLE #Temp( ID INT,
						SessionID INT,
						CategoryID INT,
						ReasonID INT,
						Comments VARCHAR(100),
						Code VARCHAR(50))

	SELECT	ID,SessionID,CategoryID,ReasonID,
			Comments,Code
	INTO #MRC
	FROM OPENXML (@handle, './MaintenanceReasonCategories/MaintenanceReasonCategory',2)  
	WITH #Temp

    INSERT INTO MaintenanceReasonCategory
	SELECT	ID,SessionID,CategoryID,ReasonID,
			Comments,(SELECT Site_ID FROM [Site] WHERE Site_Code = Code) AS Site_ID
	FROM #MRC
	EXEC sp_xml_removedocument @handle
END
GO

