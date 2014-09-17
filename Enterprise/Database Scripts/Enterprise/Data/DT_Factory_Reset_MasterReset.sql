USE [Enterprise]
GO

DECLARE @Master_Mode_ID INT

SELECT @Master_Mode_ID = FR_ID
FROM   Factory_Reset_Mode
WHERE  FR_Mode = 'Master Reset'

IF NOT EXISTS(
       SELECT 1
       FROM   Factory_Reset_List
       WHERE  [FRL_DB] = 'Enterprise'
              AND [FRL_Table] = 'UserSite_lnk'
              AND [FRL_UniqueColumn] = 'SiteID'
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
        @Master_Mode_ID,
        'Enterprise',
        'UserSite_lnk',
        'SiteID',
        'Factory_Reset_Details',
        'Site_ID'
      )

IF NOT EXISTS(
       SELECT 1
       FROM   Factory_Reset_List
       WHERE  [FRL_DB] = 'Enterprise'
              AND [FRL_Table] = 'TransactionKeys'
              AND [FRL_UniqueColumn] = 'SiteID'
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
        @Master_Mode_ID,
        'Enterprise',
        'TransactionKeys',
        'SiteID',
        'Factory_Reset_Details',
        'Site_ID'
      )
GO