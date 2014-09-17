USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Calc_function]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[Calc_function]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
  
---------------------------------------------------------------------------           
--          
-- Description: Calculate max delta         
--          
-- Inputs:                
--          
-- Outputs:               
--          
-- =======================================================================          
--           
-- Revision History          
--           
-- Anuradha	28 May 2010	Created  
--------------------------------------------------------------------------- 
  
CREATE FUNCTION [dbo].[Calc_function] (@Value1 int, @Value2 int, @Rollover int)  
  RETURNS INT  
AS  
  
BEGIN  
  set @Value1 = ISNULL(@Value1,0)  
  set @Value2 = ISNULL(@Value2,0)   
  
  declare @calc int  
  declare @MAXID int  
  set @MAXID = 99999999+1  
  set @calc = 0  
  
  IF @Rollover = 0  
    SET @calc = @value2 - @value1  
  ELSE  
    IF @value1 <> -1  
      SET @calc = @MAXID - @value1 + @value2  
  
  RETURN @calc   
  
END  
  
GO

