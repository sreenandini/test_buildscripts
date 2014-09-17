USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[compute_decimal]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[compute_decimal]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--------------------------------------------------------------------------   
--  
-- Description: Returns Decimal equivalent of the Amount
--  
-- Inputs:  iAmount
-- Outputs:       
--  
-- =======================================================================  
--   
-- Revision History  

-- Kirubakar S 12/05/2010 Created
  
--------------------------------------------------------------------------- 

CREATE FUNCTION compute_decimal(@iAmount INT)  
 RETURNS DECIMAL(16,2)  
 BEGIN  
     DECLARE @decimalValue dec(16,2)  
     SET @decimalValue=cast(@iAmount as dec(16,2))  
     SET @decimalValue = @decimalValue/100  
 RETURN @decimalValue  
 END  
 
 
 
GO

