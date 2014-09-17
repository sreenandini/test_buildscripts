USE Enterprise
GO 
-- Drop stored procedure if it already exists
IF EXISTS (
       SELECT *
       FROM   INFORMATION_SCHEMA.ROUTINES
       WHERE  SPECIFIC_SCHEMA = N'dbo'
              AND SPECIFIC_NAME = N'rsp_Vault_GetDeclaredDropsForAudit'
   )
    DROP PROCEDURE dbo.rsp_Vault_GetDeclaredDropsForAudit
GO
  /*-- =============================================  
  -- Author:  <SriHari Jogaraj>  
  -- Create date: <3rd Dec 2013>  
  -- Description: <Get the Declared Drop Details for Auditing in Enterprise - Vault Audit Adjustment Screen>  
  -- Example 
  EXEC rsp_Vault_GetDeclaredDropsForAudit 9
  -- =============================================*/
 
  
CREATE PROCEDURE dbo.rsp_Vault_GetDeclaredDropsForAudit
	@DropId BIGINT
AS
BEGIN
	SELECT tcd.Drop_ID,
	       tcd.Cassette_ID,
	       tcd.Denom,
	       tcd.VaultBalance,
	       tcd.DeclaredBalance,
	       tcd.AuditBalance,
	       tcd.FillAmount,
	       tcd.BleedAmount,
	       tcd.AdjustmentAmount,
	       tcd.dtCreated,
	       tcd.dtUpdated,
	       tcd.AudtiDate,
	       td.IsFrozen,
	       td.Declared_Balance,
	       td.Meter_Balance,
	       td.Vault_Balance,
	       td.AuditNote,
	       td.FrozeUser,
	       case WHEN tct.CassetteType_Name='Rejection' THEN  tc.Cassette_Name + ' (R)'
				ELSE tc.Cassette_Name
			END Cassette_Name,
	       tc.[Type],
	       td.IsVaultWebServiceEnabled,
	       tc.[MaxFillAmount]
	FROM   tVault_Drops td
	       INNER JOIN (
	                tVault_CassetteDrops tcd 
	                INNER JOIN tVault_Cassettes tcin 
	                ON tcin.Cassette_ID = tcd.Cassette_ID
	            )
	            ON  tcd.Drop_ID = td.Drop_ID
	       INNER JOIN tVault_Cassettes TC 
	       ON tcd.Cassette_ID=tc.Cassette_ID
	       INNER JOIN tVault_CassetteTypes     tct
	       ON tc.Type =tct.CassetteType_ID
	WHERE  td.Drop_ID = @DropId
	ORDER BY tc.Type,tc.Denom
	
END
GO
