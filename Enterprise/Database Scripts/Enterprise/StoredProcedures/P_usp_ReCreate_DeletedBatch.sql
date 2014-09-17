USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_ReCreate_DeletedBatch]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_ReCreate_DeletedBatch]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =================================================================  
-- usp_ReCreate_DeletedBatch  
-- -----------------------------------------------------------------  
-- 
-- To recreate the deleted batch on DeMerge
-- 
-- -----------------------------------------------------------------  
-- Revision History  
--   
-- 03/10/2012 Babu Gnanasekar Created  
--   
-- ================================================================= 


CREATE PROCEDURE [usp_ReCreate_DeletedBatch](@DeletedBatchNo AS INT, @MergedBatchNo AS INT) 
AS
BEGIN

	SET IDENTITY_INSERT [Batch] ON
	INSERT INTO [Batch](
		[Batch_ID],
		[Batch_Ref],
		[Batch_Date],
		[Batch_Time],
		[Schedule_ID],
		[Batch_Received_All],
		[Staff_ID],
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
		[Batch_Import_Complete])
	SELECT 
		@DeletedBatchNo,
		[Deleted_Batch_Ref],
		[Deleted_Batch_Date],
		[Deleted_Batch_Time],
		[Deleted_Schedule_ID],
		[Deleted_Batch_Received_All],
		[Deleted_Staff_ID] [Deleted_int],
		[Deleted_Batch_Company_Bank],
		[Deleted_Batch_Supplier_Bank],
		[Deleted_Batch_LOS],
		[Deleted_Batch_OverBank],
		[Deleted_Batch_Supplier_Error],
		[Deleted_Batch_Company_Error],
		[Deleted_Batch_Collector_ID],
		[Deleted_Batch_Memo],
		[Deleted_Batch_Route_Reconciliation_Completed],
		[Deleted_Batch_Bank_Reconciliation_Completed],
		[Deleted_Batch_Process_Route_Completed],
		[Deleted_Batch_User_Name],
		[Deleted_Batch_Audit_Day_ID],
		[Deleted_Batch_Audit_Week_ID],
		[Deleted_Batch_Audit_Period_ID],
		[Deleted_Batch_Audit_Year_ID],
		[Deleted_batch_Date_Performed],
		[Deleted_batch_name],
		[Deleted_Collection_Batch_Advance],
		[Deleted_Batch_Advance],
		[Deleted_Batch_Negative_Net],
		[Deleted_Batch_Adjustment],
		[Deleted_Batch_Import_Complete]
	FROM [Merged_Batch_Details] WITH(NOLOCK) WHERE
	Merged_Batch_ID = @MergedBatchNo AND Deleted_Batch_ID=@DeletedBatchNo
	
	SET IDENTITY_INSERT [Batch] OFF
END


GO

