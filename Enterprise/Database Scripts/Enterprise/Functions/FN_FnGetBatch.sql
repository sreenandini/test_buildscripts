USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FnGetBatch]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[FnGetBatch]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- FnGetBatch  
-- -----------------------------------------------------------------  
-- 
-- To get the batch based on the batch id and site id
-- 
-- -----------------------------------------------------------------  
-- Revision History  
--   
-- 03/10/2012 Babu Gnanasekar Created  
--   
-- ================================================================= 

CREATE FUNCTION FnGetBatch(@BatchID AS INT, @Siteid AS INT) RETURNS INT
AS
BEGIN

	DECLARE @NewBatchId INT

	SELECT   
		@NewBatchId = ISNULL(Batch_id, 0)
	FROM Batch Bat
		INNER JOIN [Site] S on SUBSTRING(Batch_Ref,1,CHARINDEX(',',Batch_Ref,1)-1) = S.site_code
	WHERE s.Site_ID = @Siteid  AND Bat.Batch_ID = @BatchID

	RETURN ISNULL(@NewBatchId,0)

END

GO

