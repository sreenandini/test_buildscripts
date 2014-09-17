USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetBatchDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetBatchDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[rsp_GetBatchDetails]    
(@Site VARCHAR (20))    
AS    
    
Select Batch_ID,(SUBSTRING(Batch_ref,CHARINDEX(',',Batch_ref,2)+1,LEN(Batch_ref)) + ', ' + Batch_Date) AS Batch_ref ,Batch_Date from batch where LEFT(Batch_ref,4)=@Site ORDER BY batch_ID DESC  

GO

