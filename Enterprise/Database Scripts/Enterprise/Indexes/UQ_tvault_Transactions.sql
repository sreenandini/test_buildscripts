USE Enterprise
GO

IF NOT EXISTS (
       SELECT *
       FROM   sys.indexes
       WHERE  NAME                = 'IDX_tvault_Transactions_site_id_Site_Drop_ref'
              AND [object_id]     = OBJECT_ID('tvault_Transactions')
   )
BEGIN
    CREATE NONCLUSTERED INDEX [IDX_tvault_Transactions_site_id_Site_Drop_ref] ON 
    [dbo].[tvault_Transactions] 
    ([site_id], [Site_Drop_ref]) INCLUDE([CreatedDate])
END 
GO