USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_Delete_Batch]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_Delete_Batch]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =================================================================  
-- usp_Delete_Batch  
-- -----------------------------------------------------------------  
-- 
-- To delete the batch, once merge it to other batch
-- 
-- -----------------------------------------------------------------  
-- Revision History  
--   
-- 03/10/2012 Babu Gnanasekar Created  
--   
-- ================================================================= 

CREATE PROCEDURE [dbo].[usp_Delete_Batch]
	(@DeletedBatchNo AS INT) 
AS
BEGIN

	DELETE FROM Batch WHERE Batch_ID = @DeletedBatchNo
	
END

GO

