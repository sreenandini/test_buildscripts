USE Enterprise
GO 
-- Drop stored procedure if it already exists
IF EXISTS (
       SELECT *
       FROM   INFORMATION_SCHEMA.ROUTINES
       WHERE  SPECIFIC_SCHEMA = N'dbo'
              AND SPECIFIC_NAME = N'usp_Vault_UpdateTransactionReason'
   )
    DROP PROCEDURE dbo.usp_Vault_UpdateTransactionReason
GO

	
CREATE PROCEDURE dbo.usp_Vault_UpdateTransactionReason(@Reason_ID INT ,@Reason_Description VARCHAR(50))
AS
	/*****************************************************************************************************
DESCRIPTION	: PROC Description  
CREATED DATE: PROC CreateDate
MODULE		: PROC used in Modules	
CHANGE HISTORY :
------------------------------------------------------------------------------------------------------
AUTHOR					DESCRIPTON										MODIFIED DATE
------------------------------------------------------------------------------------------------------

*****************************************************************************************************/

BEGIN
    IF NOT EXISTS(
           SELECT TOP 1 1
           FROM   tvault_Transaction_Reason
           WHERE  Reason_ID = @Reason_ID
       )
    BEGIN
      
        INSERT INTO tvault_Transaction_Reason
          (
            Reason_Description
          )
        VALUES
          (
            @Reason_Description
          )
        SELECT @Reason_ID = SCOPE_IDENTITY()        
        
    END
    ELSE
    BEGIN
        UPDATE [tVault_Transaction_Reason]
        SET    [Reason_Description] = @Reason_Description
        WHERE  Reason_ID = @Reason_ID
    END
    INSERT INTO dbo.Export_History
          (
            EH_Date
           ,EH_Reference1
           ,EH_Type
           ,EH_Site_Code
          )
        SELECT GETDATE()
              ,@Reason_ID
              ,'VAULTTRANSACTIONREASON'
              ,S.Site_Code
              FROM SITE S
END
GO