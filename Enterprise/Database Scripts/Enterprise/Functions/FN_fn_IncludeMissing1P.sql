USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fn_IncludeMissing1P]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[fn_IncludeMissing1P]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--------------------------------------------------------------------------   
--  
-- Description: Retrieves the total power events
--  
--  
-- ====================================================================  
-- Revision History  
--   
-- Gnanasekar Babu   27/09/10    Created   
---------------------------------------------------------------------------   
  
CREATE FUNCTION fn_IncludeMissing1P(@Process varchar(10), @Installation_No INT, @Collection_No INT )    
RETURNS FLOAT    
AS    
BEGIN    
DECLARE @PREVIOUS_CASH_IN_1P INT    
DECLARE @CURRENT_CASH_IN_1P INT    
SET @PREVIOUS_CASH_IN_1P = 0
SET @CURRENT_CASH_IN_1P = 0
SELECT @CURRENT_CASH_IN_1P = CASH_IN_1P FROM CASH_IN_1P WHERE Installation_No = @Installation_No     
and Collection_No = @Collection_No and Process = @Process 

SELECT @PREVIOUS_CASH_IN_1P = CASH_IN_1P FROM CASH_IN_1P WHERE Installation_No = @Installation_No     
and Collection_No < @Collection_No and Process = @Process ORDER BY CASH_IN_1P_ID ASC

IF (ISNULL(@PREVIOUS_CASH_IN_1P,0) = 0)
BEGIN
	SELECT @PREVIOUS_CASH_IN_1P = CASH_IN_1P FROM CASH_IN_1P WHERE Installation_No = @Installation_No and Process = 'INITIAL'
END

RETURN (CASE WHEN ((@CURRENT_CASH_IN_1P - @PREVIOUS_CASH_IN_1P)%2) = 0 THEN 0.00 
		WHEN ((@CURRENT_CASH_IN_1P%2) = 0 AND (@PREVIOUS_CASH_IN_1P%2) != 0) THEN (- 0.01)
		WHEN (@CURRENT_CASH_IN_1P%2 != 0) AND (@PREVIOUS_CASH_IN_1P%2 = 0) THEN 0.01 
		END)
   
END


GO

