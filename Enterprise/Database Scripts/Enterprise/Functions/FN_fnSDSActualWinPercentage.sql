USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fnSDSActualWinPercentage]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[fnSDSActualWinPercentage]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- ===================================================================================================================================  
-- SELECT dbo.fnSDSActualWinPercentage ( 100, 1 )  --
-- -----------------------------------------------------------------------------------------------------------------------------------  
--  
-- returns percentage of a value i.e 1,100 = 1 .. 1 is 1% of 100
--   
-- -----------------------------------------------------------------------------------------------------------------------------------  
-- Revision History   
--   
-- 27/03/2010 C.Taylor ( contractor ) Created  
-- ===================================================================================================================================  
CREATE FUNCTION [dbo].[fnSDSActualWinPercentage]
(
  @ActualWin   float,  -- cash take ( cashin - cashout ) 
  @CoinIn      FLOAT   -- value of games played
  
 )
 RETURNS DECIMAL(18,2)
 
 AS
 
 begin

 IF @coinin = 0
   SET @coinin = 1
   
 DECLARE @value float
 
 SET @value = ( @ActualWin / @CoinIn ) * 100
  
 RETURN @value
 
end
GO

