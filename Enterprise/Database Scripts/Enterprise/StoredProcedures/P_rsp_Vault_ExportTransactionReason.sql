USE Enterprise
GO 
-- Drop stored procedure if it already exists
IF EXISTS (
       SELECT *
       FROM   INFORMATION_SCHEMA.ROUTINES
       WHERE  SPECIFIC_SCHEMA = N'dbo'
              AND SPECIFIC_NAME = N'rsp_Vault_ExportTransactionReason'
   )
    DROP PROCEDURE dbo.rsp_Vault_ExportTransactionReason
GO
    
CREATE PROCEDURE dbo.rsp_Vault_ExportTransactionReason
	@Reason_Id BIGINT
AS
	/*****************************************************************************************************  
DESCRIPTION : PROC Description    
CREATED DATE: PROC CreateDate  
MODULE  : Exchange Export Service   
CHANGE HISTORY :  
------------------------------------------------------------------------------------------------------  
AUTHOR     DESCRIPTON          MODIFIED DATE  
------------------------------------------------------------------------------------------------------  
Exec rsp_Vault_ExportTransactionReason 1
*****************************************************************************************************/  

BEGIN
    DECLARE @Text VARCHAR(MAX)  
    
    SELECT @Text = (
               SELECT trs.Reason_ID
                     ,trs.Reason_Description
               FROM   tVault_Transaction_Reason trs WITH(NOLOCK)
               WHERE  trs.Reason_ID = @Reason_Id FOR XML PATH(''), 
                      ROOT('Reason')
           )  
    
    SELECT @Text
END  