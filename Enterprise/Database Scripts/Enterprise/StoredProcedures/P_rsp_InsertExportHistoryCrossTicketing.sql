USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_InsertExportHistoryCrossTicketing]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_InsertExportHistoryCrossTicketing]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--      
-- Description: Inserts Cross Ticketing Datas into Export_History Table
--      
-- Inputs:  --none--     
-- Outputs: --none--         
--      
-- =======================================================================    
    
-- Object:  StoredProcedure [dbo].[rsp_InsertExportHistoryCrossTicketing]      
--  Script Date: 03/20/2012 19:19:28   
--  Created By: Lekha       
---------------------------------------------------------------------------    
CREATE PROCEDURE [dbo].[rsp_InsertExportHistoryCrossTicketing]
@SITECODE VARCHAR(50)
AS  
BEGIN   
SELECT
GETDATE() AS EH_Date,
'ALL' AS EH_Reference1,
'CROSSTICKETING' AS EH_Type,
SIA.ClientSiteCode AS EH_Site_Code
INTO #TMP_ExportHistory
FROM Site SI
INNER JOIN SiteAlliance SIA ON SI.Site_Code=SIA.HostSiteCode
WHERE SI.Site_Code=@SITECODE

INSERT INTO 
    Export_History(EH_Date,EH_Reference1,EH_Type,EH_Site_Code)
	VALUES(GETDATE(),'ALL','CROSSTICKETING',@SITECODE)

INSERT INTO 
    Export_History(EH_Date,EH_Reference1,EH_Type,EH_Site_Code) 
  SELECT 
     EH_Date,EH_Reference1,EH_Type,EH_Site_Code 
  FROM #TMP_ExportHistory

END

GO

