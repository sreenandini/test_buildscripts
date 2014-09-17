USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_ExportTreasuryXML]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_ExportTreasuryXML]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[rsp_ExportTreasuryXML]  
  (@BatchNo INT , @SiteCode VARCHAR(10) )
AS  
  
BEGIN  

SELECT * FROM Treasury_Entry INNER JOIN collection ON 
COLLECTION.Collection_ID = Treasury_Entry.Collection_ID INNER JOIN batch ON
COLLECTION.Batch_ID = batch.Batch_ID WHERE batch.Batch_Ref = @SiteCode + ',' + CAST(@BatchNo AS VARCHAR(10))
FOR XML PATH('Treasury'),TYPE, ELEMENTS XSINIL, ROOT('TreasuryDetails')  

END

GO

