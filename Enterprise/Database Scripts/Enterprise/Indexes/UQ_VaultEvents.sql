USE Enterprise  
GO

IF NOT EXISTS (
       SELECT *
       FROM   sys.indexes
       WHERE  NAME                = 'IDX_VaultEvents_site_id_Site_Drop_ref'
              AND [object_id]     = OBJECT_ID('VaultEvents')
   )
BEGIN
    CREATE NONCLUSTERED INDEX [IDX_VaultEvents_site_id_Site_Drop_ref] ON [dbo].[VaultEvents] 
    ([SiteID], [Site_Drop_ref]) INCLUDE(CreateDate)
END 
GO

IF NOT EXISTS (
       SELECT *
       FROM   sys.indexes
       WHERE  NAME                = 'IDX_VaultEvents_site_id_Site_Event_ID'
              AND [object_id]     = OBJECT_ID('VaultEvents')
   )
BEGIN
    CREATE NONCLUSTERED INDEX [IDX_VaultEvents_site_id_Site_Event_ID] ON [dbo].[VaultEvents] 
    ([SiteID], [Site_Event_ID])
END 
GO