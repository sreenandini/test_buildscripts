USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_Merge_Batch]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_Merge_Batch]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =================================================================  
-- usp_Merge_Batch  
-- -----------------------------------------------------------------  
-- 
-- To update the deleted batch no to Merged batch no
-- 
-- -----------------------------------------------------------------  
-- Revision History  
--   
-- 03/10/2012 Babu Gnanasekar Created  
--   
-- ================================================================= 

CREATE PROCEDURE [dbo].[usp_Merge_Batch]
	(@DeletedBatchNo AS INT,
	 @MergedBatchNo AS INT) 
AS
BEGIN

	UPDATE Collection SET Batch_ID = @MergedBatchNo WHERE Batch_ID = @DeletedBatchNo
	UPDATE Collection_Calcs SET Batch_ID = @MergedBatchNo WHERE Batch_ID = @DeletedBatchNo
	
END


GO

