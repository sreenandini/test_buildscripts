GO
/************************************************************
 * Code formatted by SoftTree SQL Assistant © v4.8.29
 * Time: 16/07/13 1:19:26 PM
 ************************************************************/

USE Enterprise
GO 
-- Drop stored procedure if it already exists
IF EXISTS (
       SELECT *
       FROM   INFORMATION_SCHEMA.ROUTINES
       WHERE  SPECIFIC_SCHEMA = N'dbo'
              AND SPECIFIC_NAME = N'rsp_Vault_ExportDrop'
   )
    DROP PROCEDURE dbo.rsp_Vault_ExportDrop
GO
--rsp_Vault_ExportDrop 1
 
CREATE PROCEDURE dbo.rsp_Vault_ExportDrop
	@Drop_Id BIGINT
AS
	/*****************************************************************************************************
DESCRIPTION	: PROC Description  
CREATED DATE: PROC CreateDate
MODULE		: Exchange Export Service	
CHANGE HISTORY :
------------------------------------------------------------------------------------------------------
AUTHOR					DESCRIPTON										MODIFIED DATE
------------------------------------------------------------------------------------------------------

*****************************************************************************************************/

BEGIN
	DECLARE @Text VARCHAR(MAX)
	
	DECLARE @CassetteXMl XML 
	SELECT @CassetteXMl = (
	           SELECT TD.Drop_ID,
	                  TD.Cassette_ID,
	                  TD.Denom,
	                  TD.MeterBalance,
	                  TD.VaultBalance,
	                  TD.DeclaredBalance,
	                  TD.AuditBalance,
	                  TD.FillAmount,
	                  TD.BleedAmount,
	                  TD.AdjustmentAmount,
	                  TD.dtCreated,
	                  TD.dtUpdated,
	                  TD.AudtiDate
	           FROM   tVault_CassetteDrops TD WITH(NOLOCK)
	                  INNER JOIN tVault_Cassettes TC WITH(NOLOCK)
	                       ON  td.Cassette_ID = tc.Cassette_ID
	           WHERE  Drop_ID = @Drop_Id
	                  FOR XML RAW('Cassette') , ROOT('Cassettes')
	       ) 
	SELECT @Text=
	(       
	SELECT 
        drp.Device_ID,  
        drp.OpeningBalance,  
        drp.FillAmount,  
        drp.BleedAmount,  
        drp.AdjustmentAmount,  
        drp.Meter_Out,  
        drp.Vault_Out,  
        drp.Meter_Balance,  
        drp.Vault_Balance,  
        drp.Declared_Balance,  
        drp.IsDropComplete,  
        drp.IsDeclared,  
        drp.IsFrozen,  
        drp.CreatedDate,  
        drp.CreateUser,  
        drp.CreateUser,  
        drp.DropCompleteDate,  
        drp.DropCompleteUser,  
        drp.ModifiedDate,  
        drp.ModifiedUser,  
        drp.FrozenDate,  
        drp.FrozeUser,  
        drp.AuditDate,  
        drp.AuditUser,  
        drp.Site_Drop_Ref,
		drp.Site_ID,
		drp.AuditNote,
		drp.Meter_jackpot,
		drp.Meter_Handpay,
		drp.Meter_Voucher,
		ISNULL(drp.IsVaultWebServiceEnabled, 0) IsVaultWebServiceEnabled,
	    ISNULL(@CassetteXMl, '')
 FROM   tVault_Drops drp WITH(NOLOCK)  
 WHERE  drp.Drop_Id = @Drop_Id FOR XML PATH(''),  
        ROOT('DROP')
        )
        
        SELECT @Text  
END
GO

GO