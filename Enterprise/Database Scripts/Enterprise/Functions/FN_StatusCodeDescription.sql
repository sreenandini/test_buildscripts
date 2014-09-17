USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StatusCodeDescription]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[StatusCodeDescription]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION StatusCodeDescription    
(@StatusCode nvarchar(15),@ExpiryDate datetime)    
RETURNS varchar(30)    
AS    
BEGIN    
declare @Return varchar(30)    
select @return = case     
when @StatusCode='PD' then 'PAID'    
when @StatusCode='NA' then 'CANCELLED'    
when @StatusCode='ED' then 'EXPIRED'    
when @StatusCode='VD' then 'VOID'    
when @StatusCode is NULL AND @ExpiryDate < getdate() then 'EXPIRED'    
when @StatusCode is NULL AND @ExpiryDate > getdate() then 'ACTIVE'   
WHEN @statusCode ='LT' THEN 'LIABILITYTRANSFER' 
WHEN @StatusCode='PP' then 'PARTIAL PAID'  
end    
return @return    
end

GO

