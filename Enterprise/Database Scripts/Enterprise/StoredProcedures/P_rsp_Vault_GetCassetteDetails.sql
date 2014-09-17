USE Enterprise
GO 
-- Drop stored procedure if it already exists
IF EXISTS (
       SELECT *
       FROM   INFORMATION_SCHEMA.ROUTINES
       WHERE  SPECIFIC_SCHEMA = N'dbo'
              AND SPECIFIC_NAME = N'rsp_Vault_GetCassetteDetails'
)

    DROP PROCEDURE dbo.rsp_Vault_GetCassetteDetails
GO 

 
CREATE PROCEDURE dbo.rsp_Vault_GetCassetteDetails
	@Vault_id INT,
	@CassetteType INT --1-Cassette,2-Hopper
AS
	/*****************************************************************************************************
DESCRIPTION	: PROC Description  
CREATED DATE: PROC CreateDate
MODULE		: PROC used in Modulesa	
CHANGE HISTORY :
Example :
 rsp_Vault_GetCassetteDetails 4,2
------------------------------------------------------------------------------------------------------
AUTHOR					DESCRIPTON										MODIFIED DATE
------------------------------------------------------------------------------------------------------

*****************************************************************************************************/

BEGIN
	SELECT Cassette_ID,
	       Cassette_Name,
	       TYPE,
	       Denom,
	       IsActive,
	       AlertLevel,
	       StandardFillAmount,
	       MaxFillAmount,
	       DESCRIPTION
	FROM   tvault_cassettes WITH(NOLOCK)
	WHERE  vault_id = @Vault_id
	       AND [TYPE] = @CassetteType
END
GO

