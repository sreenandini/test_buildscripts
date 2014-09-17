USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FnGetDeletedBatch]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[FnGetDeletedBatch]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- FnGetDeletedBatch  
-- -----------------------------------------------------------------  
-- 
-- To get the deleted batch from the merged batch details
-- 
-- -----------------------------------------------------------------  
-- Revision History  
--   
-- 03/10/2012 Babu Gnanasekar Created  
--   
-- ================================================================= 

CREATE FUNCTION FnGetDeletedBatch(@BatchID AS INT) RETURNS VARCHAR(MAX)
AS
BEGIN
	DECLARE @DeletedBatchNos VARCHAR(MAX)
	
	SELECT 
		@DeletedBatchNos = COALESCE(@DeletedBatchNos + ', ', '') + CAST(Deleted_Batch_ID AS VARCHAR(10))
	FROM [Merged_Batch_Details] WITH(NOLOCK) 
	WHERE Merged_Batch_ID = @BatchID
	
	RETURN ISNULL(@DeletedBatchNos,'') 
END

GO

