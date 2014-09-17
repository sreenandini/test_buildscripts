USE Enterprise  
GO

IF NOT EXISTS (
       SELECT *
       FROM   sys.indexes
       WHERE  NAME                = 'IDX_tvault_Drops_Site_ID_Device_ID'
              AND [object_id]     = OBJECT_ID('tVault_Drops')
   )
BEGIN
    CREATE UNIQUE NONCLUSTERED INDEX [IDX_tvault_Drops_Site_ID_Device_ID] ON 
    [dbo].[tVault_Drops] 
    ([Site_ID] ASC, [Device_ID] ASC, [Site_Drop_Ref] ASC) INCLUDE(IsDeclared, IsFrozen)
END 
GO