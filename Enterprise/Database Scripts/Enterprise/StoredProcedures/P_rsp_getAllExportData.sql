USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_getAllExportData]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_getAllExportData]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[rsp_getAllExportData]           
(@SiteCode Varchar(50))                
AS                  
BEGIN                
              
SELECT                    
    *            
    INTO #TEMP            
FROM Export_History  
INNER JOIN Site ON Export_History.EH_Site_Code = Site.Site_Code  
WHERE ISNULL(EH_Status, '') IN ('', '-1') AND Site.SiteStatus = 'FULLYCONFIGURED'  AND EH_Site_Code = @SiteCode
ORDER BY EH_ID              
              
SELECT * FROM #TEMP ORDER BY EH_ID              
              
UPDATE IH              
SET EH_Status = 1              
FROM Export_History IH               
INNER JOIN #TEMP tT On tT.EH_ID = IH.EH_ID              
                
END

GO

