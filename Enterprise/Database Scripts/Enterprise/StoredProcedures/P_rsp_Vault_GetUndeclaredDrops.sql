SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_Vault_GetUndeclaredDrops]')
              AND TYPE IN (N'P', N'PC')
)
    DROP PROCEDURE [dbo].[rsp_Vault_GetUndeclaredDrops]
GO
-- =============================================
-- Author:		<SriHari Jogaraj>
-- Create date: <9th July 2013>
-- Description:	<Get the Vault Details in Enterprise - Vault Declaration Screen>
-- Example EXEC rsp_Vault_GetUndeclaredDrops 0,1
-- =============================================
CREATE PROCEDURE dbo.rsp_Vault_GetUndeclaredDrops
	@Vault_ID INT,
	@Site_ID INT
AS
	/*****************************************************************************************************      
DESCRIPTION : Get undeclared records for declaration       
CREATED DATE: 10-july-2013      
MODULE  : Enterprise Vault Declaration screen        
CHANGE HISTORY :      
------------------------------------------------------------------------------------------------------      
AUTHOR     DESCRIPTON          MODIFIED DATE      
------------------------------------------------------------------------------------------------------      

*****************************************************************************************************/      
BEGIN
	DECLARE @IsCentralDeclaration VARCHAR(20)
	
	SELECT @IsCentralDeclaration = spi.SettingsProfileItems_SettingsMaster_Values
	FROM   SettingsMaster sm
	       INNER JOIN SettingsProfileItems spi
	            ON  spi.SettingsProfileItems_SettingsMaster_ID = sm.SettingsMaster_ID
	       INNER JOIN SettingsProfile sp
	            ON  sp.SettingsProfile_ID = spi.SettingsProfileItems_SettingsProfile_ID
	       INNER JOIN [Site] s
	            ON  s.Site_Setting_Profile_ID = sp.SettingsProfile_ID
	WHERE  sm.SettingsMaster_Name = 'CentralizedVaultDeclaration'
	       AND s.Site_ID = @Site_ID
	
	SELECT drp.Drop_ID,
	       --drp.Device_ID,
	       drp.FillAmount,
	       drp.OpeningBalance,
	       drp.BleedAmount,
	       drp.AdjustmentAmount,
	       drp.Meter_Balance,
	       drp.Vault_Balance,
	       drp.Declared_Balance,
	       drp.IsDeclared AS Declared,
	       drp.IsFrozen AS Freezed,
	       drp.DropCompleteDate CreatedDate,
	       drp.DropCompleteUser CreateUser,
	       drp.ModifiedDate,
	       drp.ModifiedUser,
	       drp.FrozenDate AS FreezedDate,
	       drp.FrozeUser AS FreezeUser,
	       drp.AuditDate,
	       drp.AuditUser,
	       drp.Site_Drop_Ref,
	       drp.Site_ID,
	       ISNULL(drp.AuditNote, '') AS AuditNote,
	       ISNULL(usr.Staff_Last_Name, '') + ', ' + ISNULL(usr.Staff_First_Name, '') 
	       UserName,
	       td.Vault_ID,
	       td.[Name],
	       td.Type_Prefix,
	       (drp.Declared_Balance - drp.Meter_Balance) AS BMCVariance,
	       (drp.Declared_Balance - drp.Vault_Balance) AS VaultVariance,
	       m.Manufacturer_Name,
	       @IsCentralDeclaration IsCentralDeclaration,
	       drp.IsVaultWebServiceEnabled AS IsWebServiceEnabled,
	       td.Capacity
	FROM   tVault_Drops drp WITH (NOLOCK)
	       INNER JOIN tVault_Devices td WITH (NOLOCK)
	            ON  td.Vault_ID = drp.Device_ID
	       INNER JOIN Manufacturer m WITH (NOLOCK)
	            ON  m.Manufacturer_ID = td.Manufacturer_ID
	       LEFT OUTER JOIN [staff] usr WITH (NOLOCK)
	            ON  drp.DropCompleteUser = usr.UserTableID
	WHERE  drp.Site_ID = @Site_ID
	       AND IsDeclared = 0
	       AND drp.IsDropComplete = 1
	ORDER BY
	       drp.Drop_ID DESC
END
GO