/************************************************************
 * Created by Bally Technologies © 2011
 * Time: 26/12/12 5:43:30 PM
 ************************************************************/
USE [Enterprise]
GO

IF NOT EXISTS(SELECT 1 FROM [Game_Category]WHERE Game_Category_Name = 'Unassigned Category')
    INSERT [Game_Category] ( Game_Category_Name )
    SELECT 'Unassigned Category'

GO