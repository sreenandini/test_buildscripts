/************************************************************
 * Created by Bally Technologies © 2011
 * Time: 26/12/12 5:43:30 PM
 ************************************************************/
USE [Enterprise]
GO

IF NOT EXISTS(SELECT 1 FROM [SecurityProfileType]WHERE DESCRIPTION = 'Reports')
    INSERT [SecurityProfileType] ( DESCRIPTION )
    SELECT 'Reports'

IF NOT EXISTS(SELECT 1 FROM [SecurityProfileType]WHERE DESCRIPTION = 'Sites')
    INSERT [SecurityProfileType] ( DESCRIPTION )
    SELECT 'Sites'

IF NOT EXISTS(SELECT 1 FROM [SecurityProfileType]WHERE DESCRIPTION = 'Company')
    INSERT [SecurityProfileType] ( DESCRIPTION )
    SELECT 'Company'

IF NOT EXISTS(SELECT 1 FROM [SecurityProfileType]WHERE DESCRIPTION = 'SubCompany')
    INSERT [SecurityProfileType] ( DESCRIPTION )
    SELECT 'SubCompany'

IF NOT EXISTS(SELECT 1 FROM [SecurityProfileType]WHERE DESCRIPTION = 'Depot')
    INSERT [SecurityProfileType] ( DESCRIPTION )
    SELECT 'Depot'

GO