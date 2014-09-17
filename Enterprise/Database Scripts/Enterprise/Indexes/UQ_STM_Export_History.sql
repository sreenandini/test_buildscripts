USE [Enterprise]
GO

IF NOT EXISTS (
       SELECT *
       FROM   sys.indexes
       WHERE  NAME = 'IDX_STM_Export_History_Calc_Status'
              AND [object_id] = OBJECT_ID('STM_Export_History')
   )
BEGIN
    CREATE NONCLUSTERED INDEX [IDX_STM_Export_History_Calc_Status] ON [dbo].[STM_Export_History] ([Calc_Status])
END 
GO