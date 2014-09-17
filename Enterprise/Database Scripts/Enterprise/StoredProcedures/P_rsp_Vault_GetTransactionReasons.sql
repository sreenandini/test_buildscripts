USE Enterprise
GO 
-- Drop stored procedure if it already exists
IF EXISTS (
       SELECT *
       FROM   INFORMATION_SCHEMA.ROUTINES
       WHERE  SPECIFIC_SCHEMA = N'dbo'
              AND SPECIFIC_NAME = N'rsp_Vault_GetTransactionReasons'
   )
    DROP PROCEDURE dbo.rsp_Vault_GetTransactionReasons
GO

	
CREATE PROCEDURE dbo.rsp_Vault_GetTransactionReasons
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
	SELECT Reason_ID,
			CASE WHEN Reason_ID=1 THEN 'FILL'
			 WHEN Reason_ID=2 THEN 'BLEED'
			 WHEN Reason_ID=3 THEN 'ADJUSTMENT'
			 WHEN Reason_ID=4 THEN 'DROP'
			 WHEN Reason_ID=5 THEN 'STANDARD FILL'
			 WHEN Reason_ID=6 THEN 'INITIAL FILL'
			 WHEN Reason_ID=7 THEN 'EMERGENCY FILL'
			 WHEN Reason_ID=8 THEN 'FINAL DROP'
			 --WHEN Reason_ID=9 THEN 'AUTO ADJUST [System Type]'
			ELSE 'USER DEFINED' END ReasonType, 
			--CASE WHEN Reason_ID=9 THEN 0
			--ELSE 1 END IsEditable, 
	       Reason_Description
	FROM   tvault_Transaction_Reason WITH(NOLOCK) WHERE Reason_ID<>9
	
	ORDER BY Reason_ID
END
GO