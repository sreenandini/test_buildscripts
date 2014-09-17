USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FN_IssueStatusDescription]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[FN_IssueStatusDescription]
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

-- Kirubakar S 11/03/2010 Created
  
--------------------------------------------------------------------------- 

CREATE FUNCTION FN_IssueStatusDescription      
(@StatusCode nvarchar(15),@IssueDate datetime,@PrintedDate datetime,@PaidDate datetime,@ExpiryDate datetime)      
RETURNS varchar(30)      
AS      
BEGIN      
	declare @Return varchar(30)      
	IF @StatusCode='NA'
		BEGIN
		set @return='CANCELLED'
		return @return
		END
	ELSE IF @StatusCode='VD'
		BEGIN
		set @return='VOID'
		return @return
		END
	ELSE IF @ExpiryDate is not null AND @ExpiryDate<=@IssueDate
		BEGIN
		set @return='EXPIRED'
		return @return
		END
	ELSE IF @PaidDate is not null AND @PaidDate<=@IssueDate
		BEGIN
		set @return ='PAID'
		return @return
		END
	ELSE IF @PrintedDate is not null AND @PrintedDate<=@IssueDate
		BEGIN
		SET @return ='ACTIVE'
		RETURN @return
		END
RETURN @return
END 


GO

