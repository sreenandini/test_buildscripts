/************************************************************
 * Code formatted by SoftTree SQL Assistant © v4.8.29
 * Time: 8/13/2014 12:04:18 PM
 ************************************************************/

USE Enterprise
GO

IF EXISTS (
       SELECT 1
       FROM   sys.objects o
       WHERE  o.[name] = 'rsp_GetPurgeTableList'
              AND o.[type] = 'P'
   )
BEGIN
    DROP PROCEDURE rsp_GetPurgeTableList
END

GO

/*
* Revision History
* 
* Anuradha			Created			13 Aug 2014
*/

CREATE PROCEDURE rsp_GetPurgeTableList
AS
	SELECT PT_Id,
	       PT_TableName AS Tablename,
	       PT_TableAliasName AS Alias
	FROM   Purgetables
GO
