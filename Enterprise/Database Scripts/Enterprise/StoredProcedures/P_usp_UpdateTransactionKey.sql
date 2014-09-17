USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_UpdateTransactionKey]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_UpdateTransactionKey]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

   
CREATE PROCEDURE [dbo].[usp_UpdateTransactionKey]    
    
 @Site_Code VARCHAR(10),   
 @TransactionKey varchar(100)
    
AS    
    
BEGIN   
  
 DECLARE @ExpiryHours int    
 DECLARE @Date DATETIME
 
 -- get configuration
 EXEC rsp_GetSetting @Setting_Name='AUTHORIZATION_KEY_EXPIRY_HOURS', @Setting_Default='1', @Setting_Value=@ExpiryHours OUTPUT
 
 SET @Date = DATEDIFF(HOUR, @ExpiryHours, GetDate())

--To set expiry date for the transaction key      
 UPDATE [TransactionKeys] SET ExpiryDate = @Date WHERE [TransactionKey] = @TransactionKey 
   
END    


GO

