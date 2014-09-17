USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FnGetMergeBatchList]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[FnGetMergeBatchList]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- FnGetMergeBatchList  
-- -----------------------------------------------------------------  
-- 
-- To get the batch id list to calculate negative net
-- 
-- -----------------------------------------------------------------  
-- Revision History  
--   
-- 03/10/2012 Babu Gnanasekar Created  
--   
-- ================================================================= 

CREATE FUNCTION FnGetMergeBatchList    
	(@MinBatchID AS INT,     
	@Siteid AS INT,   
	@Exclude AS BIT) RETURNS VARCHAR(MAX)     
AS    
BEGIN    
	DECLARE @BatchIds VARCHAR(MAX)    

	SELECT     
		@BatchIds = cast(MAX(Batch_id) as VARCHAR(10))    
	FROM Batch Bat     
		INNER JOIN [Site] S ON SUBSTRING(Batch_Ref, 1, CHARINDEX(',', Batch_Ref, 1) -1) = S.Site_Code          
	WHERE     
		S.Site_ID = @Siteid  AND Bat.Batch_ID < @MinBatchID AND Bat.Batch_ID <> CASE WHEN (@Exclude = 1) THEN @MinBatchID  ELSE -1 END  

	SELECT     
		@BatchIds = COALESCE(@BatchIds + ', ', '') + CAST(Batch_id as VARCHAR(10))     
	FROM Batch bat    
		INNER JOIN [Site] S ON SUBSTRING(Batch_Ref, 1, CHARINDEX(',', Batch_Ref, 1) -1) = S.Site_Code          
	WHERE 
		s.Site_ID = @Siteid  AND Bat.Batch_ID >= @MinBatchID  AND Bat.Batch_ID <> CASE WHEN (@Exclude = 1) THEN @MinBatchID  ELSE -1 END     
	ORDER BY Batch_ID asc    

	RETURN ISNULL(@BatchIds,'')    
END  

GO

