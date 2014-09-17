/************************************************************
 * Created by Bally Technologies © 2011
 * Time: 26/12/12 5:43:30 PM
 ************************************************************/
USE [Enterprise]
GO

IF NOT EXISTS(SELECT 1 FROM [SecurityObjectType]WHERE ObjectDescription = 'Control')
    INSERT [SecurityObjectType] ( ObjectDescription )
    SELECT 'Control'

GO