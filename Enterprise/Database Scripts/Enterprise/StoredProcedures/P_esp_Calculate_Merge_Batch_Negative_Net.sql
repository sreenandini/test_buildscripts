USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[esp_Calculate_Merge_Batch_Negative_Net]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[esp_Calculate_Merge_Batch_Negative_Net]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =================================================================  
-- esp_Calculate_Merge_Batch_Negative_Net  
-- -----------------------------------------------------------------  
-- 
-- To calculate Batch negative net at the time of merge batch
-- 
-- -----------------------------------------------------------------  
-- Revision History  
--   
-- 03/10/2012 Babu Gnanasekar Created  
--   
-- =================================================================  


CREATE PROCEDURE dbo.[esp_Calculate_Merge_Batch_Negative_Net]    
 (@PreviousBatchNo INT,    
  @CurrentBatchNo INT,    
  @RETAILER_SHARE FLOAT)    
AS              
BEGIN      
	SET DATEFORMAT dmy           
	SET NOCOUNT ON          

	DECLARE @PrevNegShare       float    
	DECLARE @cashtake           float    
	DECLARE @newnegshare        FLOAT    
	
	SELECT @PrevNegShare = ISNULL(BATCH_NEGATIVE_NET, 0)           
	FROM dbo.Batch WHERE Batch_ID =  @PreviousBatchNo        

	-- Get the cash take for this batch          
	SELECT @cashtake = sum((COALESCE(cs.Collection_Declared_Tickets , 0) + ISNULL(cs.Collection_Declared_Notes, 0)
		+ ISNULL(cs.Collection_Declared_Coins, 0)) -(ISNULL(dbo.GetAttendantPay(c.Collection_id), 0) + (CAST(ISNULL(c.COLLECTION_RDC_TICKETS_PRINTED_VALUE,0) + ISNULL(c.RDC_TICKETS_PRINTED_NONCASHABLE_VALUE,0) AS FLOAT)/100)))           
	FROM dbo.[Collection] c                
		INNER JOIN dbo.Collection_Calcs cs ON cs.Collection_ID = c.Collection_ID           
	WHERE c.batch_id = @CurrentBatchNo AND cs.batch_id = @CurrentBatchNo      

	SET @newnegshare = (@cashtake * @RETAILER_SHARE ) + @PrevNegShare  

	UPDATE BATCH           
		SET Batch_Negative_Net = CASE WHEN (@newnegshare < 0) THEN @newnegshare ELSE 0 END  
	WHERE BATCH_id = @CurrentBatchNo     
END  
GO

