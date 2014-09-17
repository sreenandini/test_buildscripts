USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_DeMerge_Batch]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_DeMerge_Batch]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =================================================================  
-- usp_DeMerge_Batch  
-- -----------------------------------------------------------------  
-- 
-- To update the deleted batch no from Merged batch no
-- 
-- -----------------------------------------------------------------  
-- Revision History  
--   
-- 03/10/2012 Babu Gnanasekar Created  
--   
-- ================================================================= 

CREATE PROCEDURE [usp_DeMerge_Batch](@DeletedBatchNo AS INT, @MergedBatchNo AS INT)     
AS    
BEGIN    
	DECLARE @Deleted_Collection_Nos VARCHAR(MAX)    

	EXEC [usp_ReCreate_DeletedBatch] @DeletedBatchNo, @MergedBatchNo    
	
	SELECT     
		@Deleted_Collection_Nos = ISNULL(Deleted_Collection_Nos,'')     
	FROM [Merged_Batch_Details]     
	WHERE 
		Deleted_Batch_id = @DeletedBatchNo AND Merged_Batch_ID = @MergedBatchNo    

	UPDATE Collection_Calcs     
		SET Batch_ID = @DeletedBatchNo     
	WHERE Batch_ID = @MergedBatchNo    
		AND Collection_ID IN (SELECT [str] FROM dbo.[iter_charlist_to_tbl](@Deleted_Collection_Nos,',') )    

	UPDATE [Collection]     
		SET Batch_ID = @DeletedBatchNo     
	WHERE Batch_ID = @MergedBatchNo    
		AND Collection_ID IN (SELECT [str] FROM dbo.[iter_charlist_to_tbl](@Deleted_Collection_Nos,',') )    

	DELETE FROM [Merged_Batch_Details]
	WHERE Deleted_Batch_id = @DeletedBatchNo 
		AND Merged_Batch_ID = @MergedBatchNo
END    


GO

