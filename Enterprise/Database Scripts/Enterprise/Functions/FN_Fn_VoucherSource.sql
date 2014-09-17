USE ENTERPRISE
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Fn_VoucherSource]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[Fn_VoucherSource]
GO

USE ENTERPRISE
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE FUNCTION Fn_VoucherSource    
(@VoucherID int)    
RETURNS varchar(50)    
AS    
BEGIN    
declare @Return varchar(50)  

IF EXISTS ( SELECT VoucherID FROM PromotionalTickets WHERE PromotionalID=1 AND VoucherID=@VoucherID) 
 SET @Return='TIS'
ELSE
 SET @Return='BMC'

  
RETURN @return    
end 


GO

