USE [Enterprise]
GO

DECLARE @Setting_Mode_ID INT

SELECT @Setting_Mode_ID = FR_ID
FROM   Factory_Reset_Mode
WHERE  FR_Mode = 'Reset Initial Setting'

IF NOT EXISTS(
       SELECT 1
       FROM   Factory_Reset_List
       WHERE  [FRL_DB] = 'Enterprise'
              AND [FRL_Table] = 'AFTSetting'
              AND [FRL_UniqueColumn] = 'SiteCode'
              AND [FRL_RefTable] = 'Factory_Reset_Details'
              AND [FRL_RefColumn] = 'Site_Code'
   )
    INSERT Factory_Reset_List
      (
        [FRL_Mode_ID],
        [FRL_DB],
        [FRL_Table],
        [FRL_UniqueColumn],
        [FRL_RefTable],
        [FRL_RefColumn]
      )
    VALUES
      (
        @Setting_Mode_ID,
        'Enterprise',
        'AFTSetting',
        'SiteCode',
        'Factory_Reset_Details',
        'Site_Code'
      )

IF NOT EXISTS(
       SELECT 1
       FROM   Factory_Reset_List
       WHERE  [FRL_DB] = 'Enterprise'
              AND [FRL_Table] = 'Device'
              AND [FRL_UniqueColumn] = 'iSiteID'
              AND [FRL_RefTable] = 'Factory_Reset_Details'
              AND [FRL_RefColumn] = 'Site_ID'
   )
    INSERT Factory_Reset_List
      (
        [FRL_Mode_ID],
        [FRL_DB],
        [FRL_Table],
        [FRL_UniqueColumn],
        [FRL_RefTable],
        [FRL_RefColumn]
      )
    VALUES
      (
        @Setting_Mode_ID,
        'Enterprise',
        'Device',
        'iSiteID',
        'Factory_Reset_Details',
        'Site_ID'
      )

IF NOT EXISTS(
       SELECT 1
       FROM   Factory_Reset_List
       WHERE  [FRL_DB] = 'Enterprise'
              AND [FRL_Table] = 'Door_Event'
              AND [FRL_UniqueColumn] = 'Installation_ID'
              AND [FRL_RefTable] = 'Factory_Reset_Details'
              AND [FRL_RefColumn] = 'Installation_ID'
   )
    INSERT Factory_Reset_List
      (
        [FRL_Mode_ID],
        [FRL_DB],
        [FRL_Table],
        [FRL_UniqueColumn],
        [FRL_RefTable],
        [FRL_RefColumn]
      )
    VALUES
      (
        @Setting_Mode_ID,
        'Enterprise',
        'Door_Event',
        'Installation_ID',
        'Factory_Reset_Details',
        'Installation_ID'
      )

IF NOT EXISTS(
       SELECT 1
       FROM   Factory_Reset_List
       WHERE  [FRL_DB] = 'Enterprise'
              AND [FRL_Table] = 'Event'
              AND [FRL_UniqueColumn] = 'Evt_Installation_ID'
              AND [FRL_RefTable] = 'Factory_Reset_Details'
              AND [FRL_RefColumn] = 'Installation_ID'
   )
    INSERT Factory_Reset_List
      (
        [FRL_Mode_ID],
        [FRL_DB],
        [FRL_Table],
        [FRL_UniqueColumn],
        [FRL_RefTable],
        [FRL_RefColumn]
      )
    VALUES
      (
        @Setting_Mode_ID,
        'Enterprise',
        'Event',
        'Evt_Installation_ID',
        'Factory_Reset_Details',
        'Installation_ID'
      )

IF NOT EXISTS(
       SELECT 1
       FROM   Factory_Reset_List
       WHERE  [FRL_DB] = 'Enterprise'
              AND [FRL_Table] = 'Export_History'
              AND [FRL_UniqueColumn] = 'EH_Site_Code'
              AND [FRL_RefTable] = 'Factory_Reset_Details'
              AND [FRL_RefColumn] = 'Site_Code'
   )
    INSERT Factory_Reset_List
      (
        [FRL_Mode_ID],
        [FRL_DB],
        [FRL_Table],
        [FRL_UniqueColumn],
        [FRL_RefTable],
        [FRL_RefColumn]
      )
    VALUES
      (
        @Setting_Mode_ID,
        'Enterprise',
        'Export_History',
        'EH_Site_Code',
        'Factory_Reset_Details',
        'Site_Code'
      )

IF NOT EXISTS(
       SELECT 1
       FROM   Factory_Reset_List
       WHERE  [FRL_DB] = 'Enterprise'
              AND [FRL_Table] = 'Fault_Event'
              AND [FRL_UniqueColumn] = 'Installation_ID'
              AND [FRL_RefTable] = 'Factory_Reset_Details'
              AND [FRL_RefColumn] = 'Installation_ID'
   )
    INSERT Factory_Reset_List
      (
        [FRL_Mode_ID],
        [FRL_DB],
        [FRL_Table],
        [FRL_UniqueColumn],
        [FRL_RefTable],
        [FRL_RefColumn]
      )
    VALUES
      (
        @Setting_Mode_ID,
        'Enterprise',
        'Fault_Event',
        'Installation_ID',
        'Factory_Reset_Details',
        'Installation_ID'
      )

IF NOT EXISTS(
       SELECT 1
       FROM   Factory_Reset_List
       WHERE  [FRL_DB] = 'Enterprise'
              AND [FRL_Table] = 'Import_History'
              AND [FRL_UniqueColumn] = 'IH_Site_Code'
              AND [FRL_RefTable] = 'Factory_Reset_Details'
              AND [FRL_RefColumn] = 'Site_Code'
   )
    INSERT Factory_Reset_List
      (
        [FRL_Mode_ID],
        [FRL_DB],
        [FRL_Table],
        [FRL_UniqueColumn],
        [FRL_RefTable],
        [FRL_RefColumn]
      )
    VALUES
      (
        @Setting_Mode_ID,
        'Enterprise',
        'Import_History',
        'IH_Site_Code',
        'Factory_Reset_Details',
        'Site_Code'
      )

IF NOT EXISTS(
       SELECT 1
       FROM   Factory_Reset_List
       WHERE  [FRL_DB] = 'Enterprise'
              AND [FRL_Table] = 'Installation'
              AND [FRL_UniqueColumn] = 'Installation_ID'
              AND [FRL_RefTable] = 'Factory_Reset_Details'
              AND [FRL_RefColumn] = 'Installation_ID'
   )
    INSERT Factory_Reset_List
      (
        [FRL_Mode_ID],
        [FRL_DB],
        [FRL_Table],
        [FRL_UniqueColumn],
        [FRL_RefTable],
        [FRL_RefColumn]
      )
    VALUES
      (
        @Setting_Mode_ID,
        'Enterprise',
        'Installation',
        'Installation_ID',
        'Factory_Reset_Details',
        'Installation_ID'
      )

IF NOT EXISTS(
       SELECT 1
       FROM   Factory_Reset_List
       WHERE  [FRL_DB] = 'Enterprise'
              AND [FRL_Table] = 'Installation_Datapak'
              AND [FRL_UniqueColumn] = 'Installation_ID'
              AND [FRL_RefTable] = 'Factory_Reset_Details'
              AND [FRL_RefColumn] = 'Installation_ID'
   )
    INSERT Factory_Reset_List
      (
        [FRL_Mode_ID],
        [FRL_DB],
        [FRL_Table],
        [FRL_UniqueColumn],
        [FRL_RefTable],
        [FRL_RefColumn]
      )
    VALUES
      (
        @Setting_Mode_ID,
        'Enterprise',
        'Installation_Datapak',
        'Installation_ID',
        'Factory_Reset_Details',
        'Installation_ID'
      )

IF NOT EXISTS(
       SELECT 1
       FROM   Factory_Reset_List
       WHERE  [FRL_DB] = 'Enterprise'
              AND [FRL_Table] = 'Installation_Game_Data'
              AND [FRL_UniqueColumn] = 'Installation_ID'
              AND [FRL_RefTable] = 'Factory_Reset_Details'
              AND [FRL_RefColumn] = 'Installation_ID'
   )
    INSERT Factory_Reset_List
      (
        [FRL_Mode_ID],
        [FRL_DB],
        [FRL_Table],
        [FRL_UniqueColumn],
        [FRL_RefTable],
        [FRL_RefColumn]
      )
    VALUES
      (
        @Setting_Mode_ID,
        'Enterprise',
        'Installation_Game_Data',
        'Installation_ID',
        'Factory_Reset_Details',
        'Installation_ID'
      )

IF NOT EXISTS(
       SELECT 1
       FROM   Factory_Reset_List
       WHERE  [FRL_DB] = 'Enterprise'
              AND [FRL_Table] = 'Installation_Game_Info'
              AND [FRL_UniqueColumn] = 'Installation_No'
              AND [FRL_RefTable] = 'Factory_Reset_Details'
              AND [FRL_RefColumn] = 'Installation_ID'
   )
    INSERT Factory_Reset_List
      (
        [FRL_Mode_ID],
        [FRL_DB],
        [FRL_Table],
        [FRL_UniqueColumn],
        [FRL_RefTable],
        [FRL_RefColumn]
      )
    VALUES
      (
        @Setting_Mode_ID,
        'Enterprise',
        'Installation_Game_Info',
        'Installation_No',
        'Factory_Reset_Details',
        'Installation_ID'
      )

IF NOT EXISTS(
       SELECT 1
       FROM   Factory_Reset_List
       WHERE  [FRL_DB] = 'Enterprise'
              AND [FRL_Table] = 'Installation_Game_Paytable_Info'
              AND [FRL_UniqueColumn] = 'IGPI_Installation_ID'
              AND [FRL_RefTable] = 'Factory_Reset_Details'
              AND [FRL_RefColumn] = 'Installation_ID'
   )
    INSERT Factory_Reset_List
      (
        [FRL_Mode_ID],
        [FRL_DB],
        [FRL_Table],
        [FRL_UniqueColumn],
        [FRL_RefTable],
        [FRL_RefColumn]
      )
    VALUES
      (
        @Setting_Mode_ID,
        'Enterprise',
        'Installation_Game_Paytable_Info',
        'IGPI_Installation_ID',
        'Factory_Reset_Details',
        'Installation_ID'
      )

IF NOT EXISTS(
       SELECT 1
       FROM   Factory_Reset_List
       WHERE  [FRL_DB] = 'Enterprise'
              AND [FRL_Table] = 'MGMD_Installation'
              AND [FRL_UniqueColumn] = 'MGMD_Installation_ID'
              AND [FRL_RefTable] = 'Factory_Reset_Details'
              AND [FRL_RefColumn] = 'Installation_ID'
   )
    INSERT Factory_Reset_List
      (
        [FRL_Mode_ID],
        [FRL_DB],
        [FRL_Table],
        [FRL_UniqueColumn],
        [FRL_RefTable],
        [FRL_RefColumn]
      )
    VALUES
      (
        @Setting_Mode_ID,
        'Enterprise',
        'MGMD_Installation',
        'MGMD_Installation_ID',
        'Factory_Reset_Details',
        'Installation_ID'
      )

IF NOT EXISTS(
       SELECT 1
       FROM   Factory_Reset_List
       WHERE  [FRL_DB] = 'Enterprise'
              AND [FRL_Table] = 'Power_Event'
              AND [FRL_UniqueColumn] = 'Installation_ID'
              AND [FRL_RefTable] = 'Factory_Reset_Details'
              AND [FRL_RefColumn] = 'Installation_ID'
   )
    INSERT Factory_Reset_List
      (
        [FRL_Mode_ID],
        [FRL_DB],
        [FRL_Table],
        [FRL_UniqueColumn],
        [FRL_RefTable],
        [FRL_RefColumn]
      )
    VALUES
      (
        @Setting_Mode_ID,
        'Enterprise',
        'Power_Event',
        'Installation_ID',
        'Factory_Reset_Details',
        'Installation_ID'
      )

IF NOT EXISTS(
       SELECT 1
       FROM   Factory_Reset_List
       WHERE  [FRL_DB] = 'Enterprise'
              AND [FRL_Table] = 'Route'
              AND [FRL_UniqueColumn] = 'Site_ID'
              AND [FRL_RefTable] = 'Factory_Reset_Details'
              AND [FRL_RefColumn] = 'Site_ID'
   )
    INSERT Factory_Reset_List
      (
        [FRL_Mode_ID],
        [FRL_DB],
        [FRL_Table],
        [FRL_UniqueColumn],
        [FRL_RefTable],
        [FRL_RefColumn]
      )
    VALUES
      (
        @Setting_Mode_ID,
        'Enterprise',
        'Route',
        'Site_ID',
        'Factory_Reset_Details',
        'Site_ID'
      )

IF NOT EXISTS(
       SELECT 1
       FROM   Factory_Reset_List
       WHERE  [FRL_DB] = 'Enterprise'
              AND [FRL_Table] = 'Route_Member'
              AND [FRL_UniqueColumn] = 'Bar_Position_ID'
              AND [FRL_RefTable] = 'Factory_Reset_Details'
              AND [FRL_RefColumn] = 'Position_ID'
   )
    INSERT Factory_Reset_List
      (
        [FRL_Mode_ID],
        [FRL_DB],
        [FRL_Table],
        [FRL_UniqueColumn],
        [FRL_RefTable],
        [FRL_RefColumn]
      )
    VALUES
      (
        @Setting_Mode_ID,
        'Enterprise',
        'Route_Member',
        'Bar_Position_ID',
        'Factory_Reset_Details',
        'Position_ID'
      )

IF NOT EXISTS(
       SELECT 1
       FROM   Factory_Reset_List
       WHERE  [FRL_DB] = 'Enterprise'
              AND [FRL_Table] = 'VTP'
              AND [FRL_UniqueColumn] = 'Installation_ID'
              AND [FRL_RefTable] = 'Factory_Reset_Details'
              AND [FRL_RefColumn] = 'Installation_ID'
   )
    INSERT Factory_Reset_List
      (
        [FRL_Mode_ID],
        [FRL_DB],
        [FRL_Table],
        [FRL_UniqueColumn],
        [FRL_RefTable],
        [FRL_RefColumn]
      )
    VALUES
      (
        @Setting_Mode_ID,
        'Enterprise',
        'VTP',
        'Installation_ID',
        'Factory_Reset_Details',
        'Installation_ID'
      )
      
IF NOT EXISTS(
       SELECT 1
       FROM   Factory_Reset_List
       WHERE  [FRL_DB] = 'Enterprise'
              AND [FRL_Table] = 'tngainstallations'
              AND [FRL_UniqueColumn] = 'Site_ID'
              AND [FRL_RefTable] = 'Factory_Reset_Details'
              AND [FRL_RefColumn] = 'Site_ID'
   )
    INSERT Factory_Reset_List
      (
        [FRL_Mode_ID],
        [FRL_DB],
        [FRL_Table],
        [FRL_UniqueColumn],
        [FRL_RefTable],
        [FRL_RefColumn]
      )
    VALUES
      (
        @Setting_Mode_ID,
        'Enterprise',
        'tngainstallations',
        'Site_ID',
        'Factory_Reset_Details',
        'Site_ID'
      )
      
IF NOT EXISTS(
       SELECT 1
       FROM   Factory_Reset_List
       WHERE  [FRL_DB] = 'Enterprise'
              AND [FRL_Table] = 'tVault_Devices'
              AND [FRL_UniqueColumn] = 'Site_ID'
              AND [FRL_RefTable] = 'Factory_Reset_Details'
              AND [FRL_RefColumn] = 'Site_ID'
   )
    INSERT Factory_Reset_List
      (
        [FRL_Mode_ID],
        [FRL_DB],
        [FRL_Table],
        [FRL_UniqueColumn],
        [FRL_RefTable],
        [FRL_RefColumn]
      )
    VALUES
      (
        @Setting_Mode_ID,
        'Enterprise',
        'tVault_Devices',
        'Site_ID',
        'Factory_Reset_Details',
        'Site_ID'
      )

IF NOT EXISTS(
       SELECT 1
       FROM   Factory_Reset_List
       WHERE  [FRL_DB] = 'Enterprise'
              AND [FRL_Table] = 'SL_LicenseInfo'
              AND [FRL_UniqueColumn] = 'Site_ID'
              AND [FRL_RefTable] = 'Factory_Reset_Details'
              AND [FRL_RefColumn] = 'Site_ID'
   )
    INSERT Factory_Reset_List
      (
        [FRL_Mode_ID],
        [FRL_DB],
        [FRL_Table],
        [FRL_UniqueColumn],
        [FRL_RefTable],
        [FRL_RefColumn]
      )
    VALUES
      (
        @Setting_Mode_ID,
        'Enterprise',
        'SL_LicenseInfo',
        'Site_ID',
        'Factory_Reset_Details',
        'Site_ID'
      )
GO