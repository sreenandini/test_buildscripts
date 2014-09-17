SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_Report_GetUndeclaredVaultCassettes]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_Report_GetUndeclaredVaultCassettes]
GO

---- Example EXEC rsp_Report_GetUndeclaredVaultCassettes 0,2

CREATE PROCEDURE dbo.rsp_Report_GetUndeclaredVaultCassettes
	@Vault_ID INT,
	@Site_ID INT
AS
BEGIN
	SELECT drp.Drop_ID,
	       td.[Name],
	       td.Type_Prefix,	       
	       CASE 
				WHEN  tct.CassetteType_Name ='Rejection' THEN tvc.Cassette_Name + ' (R)'
				ELSE  tvc.Cassette_Name 
		   END AS Cassette_Name,
	       tvc.Denom,
	       tvcd.VaultBalance,
	       td.Vault_ID,
	       drp.DropCompleteDate CreatedDate,
	       m.Manufacturer_Name
	FROM   tVault_Drops drp WITH (NOLOCK)
	       INNER JOIN tVault_Devices td WITH (NOLOCK)
	            ON  td.Vault_ID = drp.Device_ID
	       INNER JOIN Manufacturer m WITH (NOLOCK)
	            ON  m.Manufacturer_ID = td.Manufacturer_ID
	       INNER JOIN [staff] usr WITH (NOLOCK)
	            ON  drp.CreateUser = usr.UserTableID
	       INNER JOIN tVault_CassetteDrops tvcd WITH (NOLOCK)
	            ON  tvcd.Drop_ID = drp.Drop_ID
	       INNER JOIN tVault_Cassettes tvc WITH (NOLOCK)
	            ON  tvc.Cassette_ID = tvcd.Cassette_ID
	       INNER JOIN tVault_CassetteTypes tct WITH (NOLOCK)
				ON tvc.[Type]=tct.CassetteType_ID
	WHERE  drp.Site_ID = @Site_ID
	       AND drp.IsDeclared = 0
	       AND drp.IsDropComplete = 1
	ORDER BY
	       drp.Drop_ID DESC,
	       tvc.[Type] ASC,
	       tvcd.Denom
END
GO
