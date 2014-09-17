USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_Import_BatchExportCompleteStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_Import_BatchExportCompleteStatus]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[usp_Import_BatchExportCompleteStatus]
	@Doc VARCHAR(MAX)

AS
BEGIN

	DECLARE @docHandle INT    
	DECLARE @Ret       INT    
    
	SET @Doc = '<?xml version="1.0" encoding="ISO-8859-1"?>' + @Doc    
	EXEC SP_XML_PREPAREDOCUMENT @DocHandle OUTPUT, @Doc
	
	 SELECT * INTO #TempBatch     
	 FROM OPENXML (@docHandle, './BATCHEXPCOMPSTATUS/BATCHEXPCOMPLETE', 2)
	 WITH    
	 (    
		[Batch_ID] INT './Batch_ID',    
		[BatchExportCompletedStatus] BIT './BatchExportCompletedStatus',
		[Batch_Declared] BIT './Batch_Declared'
	 )
	 
	 UPDATE [Batch]
		SET [Batch].[Batch_Import_Complete] = TB.[BatchExportCompletedStatus],
			[Batch].[Batch_Declared] = TB.[Batch_Declared]
	 FROM [Batch] B
		INNER JOIN #TempBatch TB ON 1 = 1
	 WHERE TB.Batch_ID = CAST(Convert(VARCHAR(10),RIGHT(B.Batch_Ref, LEN(B.Batch_Ref) - LEN(LEFT(B.Batch_Ref, 5)))) AS INT)
		
END

GO

