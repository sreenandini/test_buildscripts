/************************************************************
 * Code formatted by SoftTree SQL Assistant © v4.8.29
 * Time: 03/05/2014 7:53:44 PM
 ************************************************************/

USE [Enterprise]
GO

/****** Object:  Index [IDX_Export_RefPointerType]    Script Date: 03/05/2014 19:53:17 ******/
IF NOT EXISTS (
       SELECT 1
       FROM   sys.indexes
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[Export_RefPointer]')
              AND NAME = N'IDX_Export_RefPointerType'
   )
BEGIN
    CREATE NONCLUSTERED INDEX [IDX_Export_RefPointerType] ON [dbo].[Export_RefPointer] 
    ([RefPointerType] ASC)
    INCLUDE([RefPointerLastID]) WITH (
        PAD_INDEX = OFF,
        STATISTICS_NORECOMPUTE = OFF,
        SORT_IN_TEMPDB = OFF,
        IGNORE_DUP_KEY = OFF,
        DROP_EXISTING = OFF,
        ONLINE = OFF,
        ALLOW_ROW_LOCKS = ON,
        ALLOW_PAGE_LOCKS = ON
    ) ON [PRIMARY]
END
GO


