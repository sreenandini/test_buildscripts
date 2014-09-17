USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_getAllExportDataForSites]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_getAllExportDataForSites]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[rsp_getAllExportDataForSites]       
(@Site_List VARCHAR(8000))      
AS      
BEGIN      
 DECLARE @SiteCode TABLE      
         ([TP_Site_Code] [varchar](50) PRIMARY KEY)      
       
 INSERT @SiteCode      
 SELECT [value] AS TP_Site_Code      
 FROM   UDF_GetStringTable(@Site_List, ',')  
 
 DECLARE @ProcessActiveSitesOnly VARCHAR(10) , @ExportProcessRecordCount int ,@LastUpdatedTimeIntervalInSec int      
 
 EXEC rsp_GetSetting 0,'ProcessActiveSitesOnly','TRUE',@ProcessActiveSitesOnly output
 
 EXEC rsp_GetSetting 0,'ExportProcessRecordCount','10',@ExportProcessRecordCount output
 
 EXEC rsp_GetSetting 0,'LastUpdatedTimeIntervalInSec','900',@LastUpdatedTimeIntervalInSec output
 
 DECLARE @TEMP_EXPORT_HISTORY_SITES  TABLE (IDRank int,
											[EH_ID] int,      
        [EH_Date] DATETIME,      
        [EH_Reference1] varchar(50),      
        [EH_Type] varchar(30),      
        [EH_Status] varchar(100),      
        [EH_Export_Date] datetime,      
        [EH_Site_Code] varchar(50),
        [EH_Skip] int)
 
 INSERT INTO @TEMP_EXPORT_HISTORY_SITES             
 SELECT TOP(@ExportProcessRecordCount) ROW_NUMBER() OVER(PARTITION BY EH_Site_Code ORDER BY EH_ID) AS IDRank,      
        [EH_ID],      
        [EH_Date],      
        [EH_Reference1],      
        [EH_Type],      
        [EH_Status],      
        [EH_Export_Date],      
        [EH_Site_Code],
        ISNULL(ExVerHis.PendingUpdate,0) AS [EH_Skip]
 FROM   Export_History      
        INNER JOIN SITE (NOLOCK)      
             ON  Export_History.EH_Site_Code = SITE.Site_Code      
        INNER JOIN @SiteCode SC      
             ON  SC.TP_Site_Code = SITE.Site_Code
        LEFT OUTER JOIN ExchangeVersionHistory ExVerHis
			ON ExVerHis.Site_Id = SITE.Site_ID
 WHERE  ISNULL(EH_Status, '') IN ('', '-1')      
        AND SITE.SiteStatus ='FULLYCONFIGURED'    
		AND ((@ProcessActiveSitesOnly = 'FALSE') OR  (@ProcessActiveSitesOnly = 'TRUE' AND DateDIFF(ss,SITE.Last_Updated_Time,GETDATE()) <= @LastUpdatedTimeIntervalInSec)) 
 ORDER BY      
        EH_ID                          
       
 SELECT [EH_ID],      
        [EH_Date],      
        [EH_Reference1],      
        [EH_Type],      
        [EH_Status],      
        [EH_Export_Date],      
        [EH_Site_Code],
        [EH_Skip]
 FROM   @TEMP_EXPORT_HISTORY_SITES       
 ORDER BY IDRank ASC, EH_ID ASC                          
       
 UPDATE IH      
 SET    EH_Status = 1      
 FROM   Export_History IH      
        INNER JOIN @TEMP_EXPORT_HISTORY_SITES tT      
             ON  tT.EH_ID = IH.EH_ID      
       
END
GO