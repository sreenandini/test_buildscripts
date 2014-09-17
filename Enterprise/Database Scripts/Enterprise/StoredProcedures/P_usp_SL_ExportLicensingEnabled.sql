USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_SL_ExportLicensingEnabled]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_SL_ExportLicensingEnabled]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


---  
--- Description: Update and Export License Enabled Information to the sites  
---  
--- Inputs:      see inputs  
---  
--- Outputs:       
---   
--- =======================================================================  
---   
--- Revision History  
---   
--- Venkatesan Haridass  17/Apr/2012  Created
--  Venkatesan Haridass	09/Oct/2012  Based on the enable disable functionality we are applying site licensing policy to the sites  
---------------------------------------------------------------------------   
    
CREATE PROCEDURE usp_SL_ExportLicensingEnabled
@IsSiteLicensingEnabled VARCHAR(40) 
AS  
BEGIN  
	SET NOCOUNT ON

	SET @IsSiteLicensingEnabled = LTRIM(RTRIM(@IsSiteLicensingEnabled))
 
	IF NOT EXISTS (SELECT 1 FROM dbo.Setting WHERE Setting_Name = 'IsSiteLicensingEnabled')
	BEGIN
		INSERT INTO dbo.Setting
		(
			Setting_Name,
			Setting_Value
		)
		 VALUES
		(
			'IsSiteLicensingEnabled',
			ISNULL(@IsSiteLicensingEnabled, 'False')
		)
	END
	ELSE
	BEGIN
		UPDATE dbo.Setting
		SET Setting_Value = @IsSiteLicensingEnabled
		WHERE ISNULL(Setting_Value, '') <> ISNULL(@IsSiteLicensingEnabled, '') AND Setting_Name = 'IsSiteLicensingEnabled'
	END

	IF @@ROWCOUNT > 0
	BEGIN
		INSERT INTO Export_History (EH_Date, EH_Reference1, EH_Type, EH_Site_Code)  
			SELECT GETDATE(), Site_ID, 'SITESETUP', Site_Code FROM dbo.[Site] WHERE ISNULL(Site_Code,'') <> ''  
		IF UPPER(ISNULL(@IsSiteLicensingEnabled, '')) = 'TRUE'
		BEGIN
			EXEC usp_SL_UpdateExpiryDate
			IF NOT EXISTS(SELECT 1 FROM [SettingsMaster] WHERE SettingsMaster_Name = 'IsSiteLicensingEnabled')
				INSERT [SettingsMaster](SettingsMaster_Name,SettingsMaster_Type,SettingsMaster_Description,SettingsMaster_IsEnabled)
				SELECT 'IsSiteLicensingEnabled','DB','Enable Site Licensing','Y'
			ELSE
				UPDATE [SettingsMaster]
				SET SettingsMaster_IsEnabled = 'Y'
				WHERE SettingsMaster_Name = 'IsSiteLicensingEnabled'
		END
		ELSE
		BEGIN
			IF NOT EXISTS(SELECT 1 FROM [SettingsMaster] WHERE SettingsMaster_Name = 'IsSiteLicensingEnabled')
				INSERT [SettingsMaster](SettingsMaster_Name,SettingsMaster_Type,SettingsMaster_Description,SettingsMaster_IsEnabled)
				SELECT 'IsSiteLicensingEnabled','DB','Enable Site Licensing','N'
			ELSE
				UPDATE [SettingsMaster]
				SET SettingsMaster_IsEnabled = 'N'
				WHERE SettingsMaster_Name = 'IsSiteLicensingEnabled'
		END
		EXEC usp_SL_UpdateDisableGame
	END
	SET NOCOUNT OFF 
END 

GO

