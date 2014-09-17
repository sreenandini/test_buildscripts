/************************************************************
 * Created by Bally Technologies © 2011
 * Time: 26/12/12 5:43:30 PM
 ************************************************************/
USE [Enterprise]
GO

IF NOT EXISTS(SELECT 1 FROM [tblEmployeeCardType]WHERE EmpCardType = 'View Only')
    INSERT [tblEmployeeCardType] ( EmpCardType )
    SELECT 'View Only'

IF NOT EXISTS(SELECT 1 FROM [tblEmployeeCardType]WHERE EmpCardType = 'Edit')
    INSERT [tblEmployeeCardType] ( EmpCardType )
    SELECT 'Edit'

GO