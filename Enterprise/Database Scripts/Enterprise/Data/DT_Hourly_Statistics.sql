/************************************************************
 * Code formatted by SoftTree SQL Assistant © v4.8.29
 * Time: 7/15/2013 7:24:28 PM
 ************************************************************/

USE Enterprise
GO

IF EXISTS(
       SELECT 1
       FROM   sysindexes si
       WHERE  si.id = OBJECT_ID('dbo.Hourly_Statistics')
              AND si.[name] = 'NC_HSDate'
   )
BEGIN
     DROP INDEX NC_HSDate ON [dbo].[Hourly_Statistics]
END
GO

IF EXISTS(
       SELECT 1
       FROM   sysindexes si
       WHERE  si.id = OBJECT_ID('dbo.Hourly_Statistics')
              AND si.[name] = 'NC_HSINSTALLATIONNO'
   )
BEGIN
     DROP INDEX NC_HSINSTALLATIONNO ON [dbo].[Hourly_Statistics]
END
GO

IF EXISTS(
       SELECT 1
       FROM   sysindexes si
       WHERE  si.id = OBJECT_ID('dbo.Hourly_Statistics')
              AND si.[name] = 'NC_HSTYPE'
   )
BEGIN
     DROP INDEX NC_HSTYPE ON [dbo].[Hourly_Statistics]
END
GO