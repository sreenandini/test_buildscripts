USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_ExportEventDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_ExportEventDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[rsp_ExportEventDetails]
(@BatchNo INT , @SiteCode VARCHAR(10) )
AS

BEGIN
DECLARE	@DoorXML XML
DECLARE	@PowerXML XML
DECLARE	@FaultXML XML
DECLARE @Data	XML

SET @DoorXML=(
SELECT Door_Event.*, [UserName]
FROM Collection JOIN Door_Event ON Collection.Collection_id = Door_Event.Collection_id
JOIN Batch on Collection.batch_ID = batch.Batch_ID
LEFT JOIN [User] ON Door_Event.Door_Cleared_By = [User].SecurityUserID 
WHERE batch.Batch_Ref = @SiteCode + ',' + CAST(@BatchNo AS VARCHAR(10))
FOR XML PATH('Door_Event'),TYPE, ELEMENTS, ROOT('DoorEvents') )

Set @PowerXML=(
SELECT Power_Event.*, [UserName]
FROM Collection JOIN Power_Event ON Collection.Collection_id = Power_Event.Collection_id
JOIN Batch on Collection.batch_ID = batch.Batch_ID
LEFT JOIN [User] ON Power_Event.Power_Cleared_By = [User].SecurityUserID 
WHERE batch.Batch_Ref = @SiteCode + ',' + CAST(@BatchNo AS VARCHAR(10))
FOR XML PATH('Power_Event'),TYPE, ELEMENTS, ROOT('PowerEvents') )

set @FaultXML=(
SELECT Fault_Event.*, [UserName]
FROM Collection JOIN Fault_Event ON Collection.Collection_id = Fault_Event.Collection_id
JOIN Batch on Collection.batch_ID = batch.Batch_ID
LEFT JOIN [User] ON Fault_Event.Fault_Cleared_By = [User].SecurityUserID 
WHERE batch.Batch_Ref = @SiteCode + ',' + CAST(@BatchNo AS VARCHAR(10))
FOR XML PATH('Fault_Event'),TYPE, ELEMENTS, ROOT('FaultEvents') )

SET @Data = (SELECT TOP 1 @DoorXML,@PowerXML,@FaultXML  
FROM COLLECTION as CollectionDetails FOR XML AUTO,TYPE) 

SELECT @Data

END


GO

