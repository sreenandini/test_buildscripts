USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fnPercentOf]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[fnPercentOf]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- SELECT * from dbo.fnPercentOf(100,1)  -- returns percentage difference between 100 and 1 = -99
-- -----------------------------------------------------------------------------------------------------------------------------------  
--  
-- returns percentage difference between oldvalue and new, i.e 100,1 = -99
--   
-- -----------------------------------------------------------------------------------------------------------------------------------  
-- Revision History   
--   
-- 27/03/2010 C.Taylor ( contractor ) Created  
-- 22/10/2012 Venkatesan.H Modified  Function Returns invalid percentage(0%) when (@oldvalue+@newvalue)=0
--                                              Example : @oldValue = -300 and @newvalue = 300 
--												SELECT [Enterprise].[dbo].fnPercentOf(-300,300)
-- ===================================================================================================================================  
CREATE FUNCTION fnPercentOf
(
  @oldvalue FLOAT,
  @newvalue FLOAT
  
 )
 RETURNS FLOAT
 
 AS
 
 BEGIN
 
 DECLARE @value float
 
 
 SET @value = @newvalue-@oldvalue --CASE WHEN (@oldvalue+@newvalue)=0 THEN 0 
                --   WHEN @newvalue = 0 THEN 0
                   -- ELSE @newvalue-@oldvalue
                  -- end
  
 IF @oldvalue = 0 
    set @oldvalue = 1
  
  IF @value <> 0
    SET @value = (@value / @oldvalue) * 100
  ELSE
    SET @value = 0
 
  RETURN @value
 
 END

GO

