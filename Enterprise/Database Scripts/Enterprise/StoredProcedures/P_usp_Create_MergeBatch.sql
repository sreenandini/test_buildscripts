USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_Create_MergeBatch]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_Create_MergeBatch]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[usp_Create_MergeBatch](@DeletedBatchNo AS INT, @MergedBatchNo AS INT) 
AS
BEGIN

DECLARE @DelectedCollectionNos VARCHAR(MAX)
SELECT @DelectedCollectionNos = COALESCE(@DelectedCollectionNos + ', ', '') + CAST(Collection_id as VARCHAR(10))
FROM collection WITH(NOLOCK) WHERE Batch_id = @DeletedBatchNo

INSERT INTO [Merged_Batch_Details]
SELECT 
@MergedBatchNo,
@DeletedBatchNo,
@DelectedCollectionNos,
[Batch_Ref],
[Batch_Date],
[Batch_Time],
[Schedule_ID],
[Batch_Received_All],
[Staff_ID] [int],
[Batch_Company_Bank],
[Batch_Supplier_Bank],
[Batch_LOS],
[Batch_OverBank],
[Batch_Supplier_Error],
[Batch_Company_Error],
[Batch_Collector_ID],
[Batch_Memo],
[Batch_Route_Reconciliation_Completed],
[Batch_Bank_Reconciliation_Completed],
[Batch_Process_Route_Completed],
[Batch_User_Name],
[Batch_Audit_Day_ID],
[Batch_Audit_Week_ID],
[Batch_Audit_Period_ID],
[Batch_Audit_Year_ID],
[batch_Date_Performed],
[batch_name],
[Collection_Batch_Advance],
[Batch_Advance],
[Batch_Negative_Net],
[Batch_Adjustment],
[Batch_Import_Complete]
FROM BATCH WITH(NOLOCK) WHERE
Batch_id = @DeletedBatchNo

END

GO

