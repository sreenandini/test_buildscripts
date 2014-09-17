USE Enterprise 
GO

IF NOT EXISTS (
       SELECT *
       FROM   sys.indexes
       WHERE  NAME                = 'IDX_tVault_TransactionEvents_site_id_Site_Drop_ref'
              AND [object_id]     = OBJECT_ID('tVault_TransactionEvents')
   )
BEGIN
    CREATE NONCLUSTERED INDEX 
    [IDX_tVault_TransactionEvents_site_id_Site_Drop_ref] ON [dbo].[tVault_TransactionEvents] 
    ([site_id], [Site_Drop_ref]) INCLUDE([CreatedDate], TransactionEvent_Type)
END 
GO

IF NOT EXISTS (
       SELECT *
       FROM   sys.indexes
       WHERE  NAME                = 'IDX_tVault_TransactionEvents_site_id_Site_Event_ID'
              AND [object_id]     = OBJECT_ID('tVault_TransactionEvents')
   )
BEGIN
    CREATE NONCLUSTERED INDEX 
    [IDX_tVault_TransactionEvents_site_id_Site_Event_ID] ON [dbo].[tVault_TransactionEvents] 
    ([site_id], [Site_Event_ID])
END 
GO