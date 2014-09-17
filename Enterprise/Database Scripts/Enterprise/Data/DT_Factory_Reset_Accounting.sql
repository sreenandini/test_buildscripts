USE Enterprise
GO

DECLARE @Account_Mode_ID  INT

SELECT @Account_Mode_ID = FR_ID
FROM   Factory_Reset_Mode
WHERE  FR_Mode = 'Delete Account Information'


IF NOT EXISTS(
       SELECT *
       FROM   Factory_Reset_List
       WHERE  [FRL_DB] = 'Audit'
              AND [FRL_Table] = 'Site_AFT_AuditHistory'
              AND [FRL_UniqueColumn] = 'AFT_InstallationNo'
              AND [FRL_RefTable] = 'Factory_Reset_Details'
              AND [FRL_RefColumn] = 'Installation_ID'
              AND [FRL_Mode_ID] = @Account_Mode_ID
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
        @Account_Mode_ID,
        'Audit',
        'Site_AFT_AuditHistory',
        'AFT_InstallationNo',
        'Factory_Reset_Details',
        'Installation_ID'
      )

IF NOT EXISTS(
       SELECT 1
       FROM   Factory_Reset_List
       WHERE  [FRL_DB] = 'Enterprise'
              AND [FRL_Table] = 'AFT_Transactions'
              AND [FRL_UniqueColumn] = 'Installation_No'
              AND [FRL_RefTable] = 'Factory_Reset_Details'
              AND [FRL_RefColumn] = 'Installation_ID'
              AND [FRL_Mode_ID] = @Account_Mode_ID
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
        @Account_Mode_ID,
        'Enterprise',
        'AFT_Transactions',
        'Installation_No',
        'Factory_Reset_Details',
        'Installation_ID'
      )

IF NOT EXISTS(
       SELECT 1
       FROM   Factory_Reset_List
       WHERE  [FRL_DB] = 'Enterprise'
              AND [FRL_Table] = 'Batch'
              AND [FRL_UniqueColumn] = 'Batch_ID'
              AND [FRL_RefTable] = 'Factory_Reset_Details'
              AND [FRL_RefColumn] = 'Batch_ID'
              AND [FRL_Mode_ID] = @Account_Mode_ID
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
        @Account_Mode_ID,
        'Enterprise',
        'Batch',
        'Batch_ID',
        'Factory_Reset_Details',
        'Batch_ID'
      )

IF NOT EXISTS(
       SELECT 1
       FROM   Factory_Reset_List
       WHERE  [FRL_DB] = 'Enterprise'
              AND [FRL_Table] = 'Collection'
              AND [FRL_UniqueColumn] = 'Installation_ID'
              AND [FRL_RefTable] = 'Factory_Reset_Details'
              AND [FRL_RefColumn] = 'Installation_ID'
              AND [FRL_Mode_ID] = @Account_Mode_ID
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
        @Account_Mode_ID,
        'Enterprise',
        'Collection',
        'Installation_ID',
        'Factory_Reset_Details',
        'Installation_ID'
      )

IF NOT EXISTS(
       SELECT 1
       FROM   Factory_Reset_List
       WHERE  [FRL_DB] = 'Enterprise'
              AND [FRL_Table] = 'Collection_ExceptionVoucher'
              AND [FRL_UniqueColumn] = 'Installation_No'
              AND [FRL_RefTable] = 'Factory_Reset_Details'
              AND [FRL_RefColumn] = 'Installation_ID'
              AND [FRL_Mode_ID] = @Account_Mode_ID
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
        @Account_Mode_ID,
        'Enterprise',
        'Collection_ExceptionVoucher',
        'Installation_No',
        'Factory_Reset_Details',
        'Installation_ID'
      )

IF NOT EXISTS(
       SELECT 1
       FROM   Factory_Reset_List
       WHERE  [FRL_DB] = 'Enterprise'
              AND [FRL_Table] = 'Collection_Ticket'
              AND [FRL_UniqueColumn] = 'CT_Printed_Installation_ID'
              AND [FRL_RefTable] = 'Factory_Reset_Details'
              AND [FRL_RefColumn] = 'Installation_ID'
              AND [FRL_Mode_ID] = @Account_Mode_ID
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
        @Account_Mode_ID,
        'Enterprise',
        'Collection_Ticket',
        'CT_Printed_Installation_ID',
        'Factory_Reset_Details',
        'Installation_ID'
      )

IF NOT EXISTS(
       SELECT 1
       FROM   Factory_Reset_List
       WHERE  [FRL_DB] = 'Enterprise'
              AND [FRL_Table] = 'Hourly_statistics'
              AND [FRL_UniqueColumn] = 'HS_Installation_No'
              AND [FRL_RefTable] = 'Factory_Reset_Details'
              AND [FRL_RefColumn] = 'Installation_ID'
              AND [FRL_Mode_ID] = @Account_Mode_ID
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
        @Account_Mode_ID,
        'Enterprise',
        'Hourly_statistics',
        'HS_Installation_No',
        'Factory_Reset_Details',
        'Installation_ID'
      )

IF NOT EXISTS(
       SELECT 1
       FROM   Factory_Reset_List
       WHERE  [FRL_DB] = 'Enterprise'
              AND [FRL_Table] = 'LiquidationDetails'
              AND [FRL_UniqueColumn] = 'SiteId'
              AND [FRL_RefTable] = 'Factory_Reset_Details'
              AND [FRL_RefColumn] = 'Site_ID'
              AND [FRL_Mode_ID] = @Account_Mode_ID
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
        @Account_Mode_ID,
        'Enterprise',
        'LiquidationDetails',
        'SiteId',
        'Factory_Reset_Details',
        'Site_ID'
      )

IF NOT EXISTS(
       SELECT 1
       FROM   Factory_Reset_List
       WHERE  [FRL_DB] = 'Enterprise'
              AND [FRL_Table] = 'LiquidationShareDetails'
              AND [FRL_UniqueColumn] = 'LiquidationId'
              AND [FRL_RefTable] = 'Factory_Reset_Details'
              AND [FRL_RefColumn] = 'LiquidationId'
              AND [FRL_Mode_ID] = @Account_Mode_ID
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
        @Account_Mode_ID,
        'Enterprise',
        'LiquidationShareDetails',
        'LiquidationId',
        'Factory_Reset_Details',
        'LiquidationId'
      )

IF NOT EXISTS(
       SELECT 1
       FROM   Factory_Reset_List
       WHERE  [FRL_DB] = 'Enterprise'
              AND [FRL_Table] = 'Meter_history'
              AND [FRL_UniqueColumn] = 'MH_Installation_No'
              AND [FRL_RefTable] = 'Factory_Reset_Details'
              AND [FRL_RefColumn] = 'Installation_ID'
              AND [FRL_Mode_ID] = @Account_Mode_ID
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
        @Account_Mode_ID,
        'Enterprise',
        'Meter_history',
        'MH_Installation_No',
        'Factory_Reset_Details',
        'Installation_ID'
      )

IF NOT EXISTS(
       SELECT 1
       FROM   Factory_Reset_List
       WHERE  [FRL_DB] = 'Enterprise'
              AND [FRL_Table] = 'MGMD_SessionDelta'
              AND [FRL_UniqueColumn] = 'MGMD_Installation_ID'
              AND [FRL_RefTable] = 'Factory_Reset_Details'
              AND [FRL_RefColumn] = 'Installation_ID'
              AND [FRL_Mode_ID] = @Account_Mode_ID
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
        @Account_Mode_ID,
        'Enterprise',
        'MGMD_SessionDelta',
        'MGMD_Installation_ID',
        'Factory_Reset_Details',
        'Installation_ID'
      )

IF NOT EXISTS(
       SELECT 1
       FROM   Factory_Reset_List
       WHERE  [FRL_DB] = 'Enterprise'
              AND [FRL_Table] = 'Part_Collection'
              AND [FRL_UniqueColumn] = 'Installation_ID'
              AND [FRL_RefTable] = 'Factory_Reset_Details'
              AND [FRL_RefColumn] = 'Installation_ID'
              AND [FRL_Mode_ID] = @Account_Mode_ID
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
        @Account_Mode_ID,
        'Enterprise',
        'Part_Collection',
        'Installation_ID',
        'Factory_Reset_Details',
        'Installation_ID'
      )

IF NOT EXISTS(
       SELECT 1
       FROM   Factory_Reset_List
       WHERE  [FRL_DB] = 'Enterprise'
              AND [FRL_Table] = 'Read'
              AND [FRL_UniqueColumn] = 'Installation_ID'
              AND [FRL_RefTable] = 'Factory_Reset_Details'
              AND [FRL_RefColumn] = 'Installation_ID'
              AND [FRL_Mode_ID] = @Account_Mode_ID
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
        @Account_Mode_ID,
        'Enterprise',
        'Read',
        'Installation_ID',
        'Factory_Reset_Details',
        'Installation_ID'
      )

IF NOT EXISTS(
       SELECT 1
       FROM   Factory_Reset_List
       WHERE  [FRL_DB] = 'Enterprise'
              AND [FRL_Table] = 'Ticket_Exception'
              AND [FRL_UniqueColumn] = 'TE_Installation_No'
              AND [FRL_RefTable] = 'Factory_Reset_Details'
              AND [FRL_RefColumn] = 'Installation_ID'
              AND [FRL_Mode_ID] = @Account_Mode_ID
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
        @Account_Mode_ID,
        'Enterprise',
        'Ticket_Exception',
        'TE_Installation_No',
        'Factory_Reset_Details',
        'Installation_ID'
      )
      
IF NOT EXISTS(
       SELECT 1
       FROM   Factory_Reset_List
       WHERE  [FRL_DB] = 'Enterprise'
              AND [FRL_Table] = 'CassetteDetails'
              AND [FRL_UniqueColumn] = 'VaultEventid'
              AND [FRL_RefTable] = 'VaultEvents'
              AND [FRL_RefColumn] = 'SiteID'
              AND [FRL_Mode_ID] = @Account_Mode_ID
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
        @Account_Mode_ID,
        'Enterprise',
        'CassetteDetails',
        'VaultEventid',
        'VaultEvents',
        'SiteID'
      )
      
IF NOT EXISTS(
       SELECT 1
       FROM   Factory_Reset_List
       WHERE  [FRL_DB] = 'Enterprise'
              AND [FRL_Table] = 'VaultEvents'
              AND [FRL_UniqueColumn] = 'SiteID'
              AND [FRL_RefTable] = 'Factory_Reset_Details'
              AND [FRL_RefColumn] = 'Site_ID'
              AND [FRL_Mode_ID] = @Account_Mode_ID
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
        @Account_Mode_ID,
        'Enterprise',
        'VaultEvents',
        'SiteID',
        'Factory_Reset_Details',
        'Site_ID'
      )

IF NOT EXISTS(
       SELECT 1
       FROM   Factory_Reset_List
       WHERE  [FRL_DB] = 'Enterprise'
              AND [FRL_Table] = 'tVault_TransactionEvents'
              AND [FRL_UniqueColumn] = 'Site_Id'
              AND [FRL_RefTable] = 'Factory_Reset_Details'
              AND [FRL_RefColumn] = 'Site_ID'
              AND [FRL_Mode_ID] = @Account_Mode_ID
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
        @Account_Mode_ID,
        'Enterprise',
        'tVault_TransactionEvents',
        'Site_Id',
        'Factory_Reset_Details',
        'Site_ID'
      )
            
IF NOT EXISTS(
       SELECT 1
       FROM   Factory_Reset_List
       WHERE  [FRL_DB] = 'Enterprise'
              AND [FRL_Table] = 'TotalVaultBalance'
              AND [FRL_UniqueColumn] = 'Site_ID'
              AND [FRL_RefTable] = 'Factory_Reset_Details'
              AND [FRL_RefColumn] = 'Site_ID'
              AND [FRL_Mode_ID] = @Account_Mode_ID
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
        @Account_Mode_ID,
        'Enterprise',
        'TotalVaultBalance',
        'Site_ID',
        'Factory_Reset_Details',
        'Site_ID'
      )

IF NOT EXISTS(
       SELECT 1
       FROM   Factory_Reset_List
       WHERE  [FRL_DB] = 'Enterprise'
              AND [FRL_Table] = 'tVault_CassetteTransactions'
              AND [FRL_UniqueColumn] = 'Transaction_ID'
              AND [FRL_RefTable] = 'Factory_Reset_Details'
              AND [FRL_RefColumn] = 'Vault_Transaction_ID'
              AND [FRL_Mode_ID] = @Account_Mode_ID
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
        @Account_Mode_ID,
        'Enterprise',
        'tVault_CassetteTransactions',
        'Transaction_ID',
        'Factory_Reset_Details',
        'Vault_Transaction_ID'
      )

IF NOT EXISTS(
       SELECT 1
       FROM   Factory_Reset_List
       WHERE  [FRL_DB] = 'Enterprise'
              AND [FRL_Table] = 'tvault_Transactions'
              AND [FRL_UniqueColumn] = 'site_id'
              AND [FRL_RefTable] = 'Factory_Reset_Details'
              AND [FRL_RefColumn] = 'Site_ID'
              AND [FRL_Mode_ID] = @Account_Mode_ID
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
        @Account_Mode_ID,
        'Enterprise',
        'tvault_Transactions',
        'site_id',
        'Factory_Reset_Details',
        'Site_ID'
      )

IF NOT EXISTS(
       SELECT 1
       FROM   Factory_Reset_List
       WHERE  [FRL_DB] = 'Enterprise'
              AND [FRL_Table] = 'tVault_CassetteDrops'
              AND [FRL_UniqueColumn] = 'Drop_ID'
              AND [FRL_RefTable] = 'Factory_Reset_Details'
              AND [FRL_RefColumn] = 'Vault_Drop_ID'
              AND [FRL_Mode_ID] = @Account_Mode_ID
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
        @Account_Mode_ID,
        'Enterprise',
        'tVault_CassetteDrops',
        'Drop_ID',
        'Factory_Reset_Details',
        'Vault_Drop_ID'
      )

IF NOT EXISTS(
       SELECT 1
       FROM   Factory_Reset_List
       WHERE  [FRL_DB] = 'Enterprise'
              AND [FRL_Table] = 'tVault_Drops'
              AND [FRL_UniqueColumn] = 'Site_ID'
              AND [FRL_RefTable] = 'Factory_Reset_Details'
              AND [FRL_RefColumn] = 'Site_ID'
              AND [FRL_Mode_ID] = @Account_Mode_ID
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
        @Account_Mode_ID,
        'Enterprise',
        'tVault_Drops',
        'Site_ID',
        'Factory_Reset_Details',
        'Site_ID'
      )

IF NOT EXISTS(
       SELECT 1
       FROM   Factory_Reset_List
       WHERE  [FRL_DB] = 'Enterprise'
              AND [FRL_Table] = 'Treasury_entry'
              AND [FRL_UniqueColumn] = 'Installation_ID'
              AND [FRL_RefTable] = 'Factory_Reset_Details'
              AND [FRL_RefColumn] = 'Installation_ID'
              AND [FRL_Mode_ID] = @Account_Mode_ID
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
        @Account_Mode_ID,
        'Enterprise',
        'Treasury_entry',
        'Installation_ID',
        'Factory_Reset_Details',
        'Installation_ID'
      )

IF NOT EXISTS(
       SELECT 1
       FROM   Factory_Reset_List
       WHERE  [FRL_DB] = 'Enterprise'
              AND [FRL_Table] = 'Voucher'
              AND [FRL_UniqueColumn] = 'iSiteID'
              AND [FRL_RefTable] = 'Factory_Reset_Details'
              AND [FRL_RefColumn] = 'Site_ID'
              AND [FRL_Mode_ID] = @Account_Mode_ID
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
        @Account_Mode_ID,
        'Enterprise',
        'Voucher',
        'iSiteID',
        'Factory_Reset_Details',
        'Site_ID'
      )

IF NOT EXISTS(
       SELECT 1
       FROM   Factory_Reset_List
       WHERE  [FRL_DB] = 'Enterprise'
              AND [FRL_Table] = 'Import_History'
              AND [FRL_UniqueColumn] = 'IH_Site_Code'
              AND [FRL_RefTable] = 'Factory_Reset_Details'
              AND [FRL_RefColumn] = 'Site_Code'
              AND [FRL_Mode_ID] = @Account_Mode_ID
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
        @Account_Mode_ID,
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
              AND [FRL_Table] = 'Collection_Calcs'
              AND [FRL_UniqueColumn] = 'Installation_ID'
              AND [FRL_RefTable] = 'Factory_Reset_Details'
              AND [FRL_RefColumn] = 'Installation_ID'
              AND [FRL_Mode_ID] = @Account_Mode_ID
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
        @Account_Mode_ID,
        'Enterprise',
        'Collection_Calcs',
        'Installation_ID',
        'Factory_Reset_Details',
        'Installation_ID'
      )
      
IF NOT EXISTS(
       SELECT 1
       FROM   Factory_Reset_List
       WHERE  [FRL_DB] = 'MeterAnalysis'
              AND [FRL_Table] = 'Meter_Analysis_Data'
              AND [FRL_UniqueColumn] = 'Installation_ID'
              AND [FRL_RefTable] = 'Factory_Reset_Details'
              AND [FRL_RefColumn] = 'Installation_ID'
              AND [FRL_Mode_ID] = @Account_Mode_ID
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
        @Account_Mode_ID,
        'MeterAnalysis',
        'Meter_Analysis_Data',
        'Installation_ID',
        'Factory_Reset_Details',
        'Installation_ID'
      )      
GO
