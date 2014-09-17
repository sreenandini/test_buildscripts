/************************************************************
 * Code formatted by SoftTree SQL Assistant © v4.8.29
 * Time: 6/25/2013 6:10:04 PM
 ************************************************************/

USE Enterprise
GO

/************************************************************
 * Code formatted by SoftTree SQL Assistant © v4.8.29
 * Time: 6/25/2013 6:04:49 PM
 ************************************************************/

-- =============================================
-- Create basic stored procedure template
-- =============================================

-- Drop stored procedure if it already exists
IF EXISTS (
       SELECT *
       FROM   INFORMATION_SCHEMA.ROUTINES
       WHERE  SPECIFIC_SCHEMA = N'dbo'
              AND SPECIFIC_NAME = N'usp_VSDeleteCollectionBatch'
   )
    DROP PROCEDURE dbo.usp_VSDeleteCollectionBatch
GO

-- exec dbo.usp_VSDeleteCollectionBatch 1
CREATE PROCEDURE dbo.usp_VSDeleteCollectionBatch
	@BatchID INT = 0
AS
BEGIN
	BEGIN TRANSACTION DELETETRAN
	
	BEGIN TRY
		-- Delete from collection details
		DELETE CD
		FROM   Collection_Details CD
		       INNER JOIN [COLLECTION] CC
		            ON  CC.Collection_ID = CD.Collection_ID
		WHERE  CC.Batch_ID = @BatchID
		
		IF (@@ERROR <> 0)
		    GOTO ABORTTRAN
		
		-- Delete from collection
		DELETE CC
		FROM   [COLLECTION] CC
		       INNER JOIN Collection_Details CD
		            ON  CC.Collection_ID = CD.Collection_ID
		WHERE  CC.Batch_ID = @BatchID
		
		IF (@@ERROR <> 0)
		    GOTO ABORTTRAN
		
		-- Delete from batch
		DELETE 
		FROM   BATCH
		WHERE  Batch_ID = @BatchID
		
		IF (@@ERROR <> 0)
		    GOTO ABORTTRAN
	END TRY
	BEGIN CATCH
		GOTO ABORTTRAN
	END CATCH 
	
	COMMITTRAN:
	PRINT 'Delete Transaction Commited'
	COMMIT TRANSACTION DELETETRAN
	RETURN 0
	
	ABORTTRAN:
	PRINT 'Delete Transaction Aborted'
	ROLLBACK TRANSACTION DELETETRAN
	RETURN -1
END
GO
