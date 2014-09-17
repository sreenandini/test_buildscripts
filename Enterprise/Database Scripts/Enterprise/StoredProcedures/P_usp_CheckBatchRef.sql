USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_CheckBatchRef]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_CheckBatchRef]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

  
-- ===================================================================================================================================  
-- CheckBatchRef  
-- -----------------------------------------------------------------------------------------------------------------------------------  
--  
-- Checks if the provided Batch Ref is available inthe Batch table and inserts/updates the same  
--   
  
-- -----------------------------------------------------------------------------------------------------------------------------------  
-- Revision History   
--   
-- 15/05/2008 Sudarsan S Created  
-- 25/06/2008 Sudarsan S added one more parameter (@pfBatch_Advance) as a new column was added to the Batch table  
-- 09/07/2008 Renjish N Commented RowCount check.  
-- 24/09/08   Anuradha	Added code for Retailer Negative Net
-- ===================================================================================================================================  
  
CREATE PROCEDURE [dbo].[usp_CheckBatchRef]  
  @pvcSite_Code  VARCHAR(50),  
  @piBatch_ID  INT,  
  @pvcBatch_Date VARCHAR(30),  
  @pvcBatch_Time VARCHAR(50),  
  @pvcBatch_Adjustment REAL,  
  @pvcUser_Name VARCHAR(50),  
  @pvcBatch_Date_Performed VARCHAR(30),  
  @pvcBatch_Name VARCHAR(50),  
  @pfBatch_Advance FLOAT,
  @pBatchNegativeNet FLOAT,  
  @oiBatch_ID INT OUTPUT  
AS  
  
BEGIN  
  
  IF NOT EXISTS(SELECT * FROM dbo.Batch WHERE Batch_Ref = @pvcSite_Code + ',' + Convert(VARCHAR, @piBatch_ID))  
  BEGIN  
    INSERT INTO dbo.Batch(Batch_Ref, Batch_Date, Batch_Time, Batch_Company_Error, Batch_User_Name, Batch_Date_Performed, Batch_Name, Batch_Advance,Batch_Negative_Net)   
     VALUES(@pvcSite_Code + ',' + Convert(VARCHAR, @piBatch_ID), @pvcBatch_Date, @pvcBatch_Time, @pvcBatch_Adjustment, @pvcUser_Name, @pvcBatch_Date_Performed, @pvcBatch_Name, @pfBatch_Advance,@pBatchNegativeNet)  
  END  
  
--  IF @@RowCount > 0  
   SELECT @oiBatch_ID = Batch_ID FROM dbo.Batch WHERE Batch_Ref = @pvcSite_Code + ',' + Convert(VARCHAR, @piBatch_ID)  
--  ELSE  
--   SELECT @oiBatch_ID = NULL  
  
END  
  


GO

