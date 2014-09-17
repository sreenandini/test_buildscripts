USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Fn_VoucherType]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[Fn_VoucherType]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--------------------------------------------------------------------------   
--  
-- Description: Returns Description for Status Code
--  
-- Inputs:  StatusCode, ExpiryDate
-- Outputs:       
--  
-- =======================================================================  
--   
-- Revision History  

-- Kirubakar S 12/05/2010 Created
  
--------------------------------------------------------------------------- 



CREATE FUNCTION Fn_VoucherType    
(@VouchertypeID int,@VoucherID int)    
RETURNS varchar(50)    
AS    
BEGIN    
declare @Return varchar(50)    


IF EXISTS ( SELECT VoucherID FROM PromotionalTickets WHERE VoucherID=@VoucherID)
BEGIN 
IF (@VouchertypeID=0)
 SET @Return='PROMO CASHABLE'
 ELSE
 SET @Return='PROMO NON-CASHABLE'
 END
ELSE
BEGIN
IF (@VouchertypeID=0)
 SET @Return='CASHABLE'
 ELSE
 SET @Return='NON CASHABLE'
 END

return @return    
end 



GO

