/************************************************************
 * Created by Bally Technologies © 2011
 * Time: 26/12/12 5:43:30 PM
 ************************************************************/
USE [Enterprise]
GO

IF NOT EXISTS(SELECT 1 FROM [Region]WHERE RegionName = 'en-GB')
    INSERT [Region] ( RegionName )
    SELECT 'en-GB'

IF NOT EXISTS(SELECT 1 FROM [Region]WHERE RegionName = 'en-US')
    INSERT [Region] ( RegionName )
    SELECT 'en-US'

IF NOT EXISTS(SELECT 1 FROM [Region]WHERE RegionName = 'it-IT')
    INSERT [Region] ( RegionName )
    SELECT 'it-IT'

GO