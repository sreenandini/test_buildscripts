/************************************************************
 * Created by Bally Technologies © 2011
 * Time: 26/12/12 5:43:30 PM
 ************************************************************/
USE [Enterprise]
GO

IF NOT EXISTS(SELECT 1 FROM [Game_Title]WHERE Game_Title = 'Unassigned GameTitle')
    INSERT [Game_Title] ( Game_Category_ID, Game_Title, Manufacturer_ID )
    SELECT 1, 'Unassigned GameTitle', NULL

GO