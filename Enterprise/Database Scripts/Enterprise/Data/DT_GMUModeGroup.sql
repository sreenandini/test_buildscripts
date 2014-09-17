/************************************************************
 * Created by Bally Technologies © 2011
 * Time: 26/12/12 5:43:30 PM
 * Author: Aishwarrya V S
 ************************************************************/
USE [Enterprise]
GO

IF NOT EXISTS(SELECT TOP 1 1 FROM [GMUModeGroup] WHERE GMUModeGroupName  = 'R')
    INSERT [GMUModeGroup] ( GMUModeGroupName,GMUModeGroupDescription )
    SELECT 'R','Read'

IF NOT EXISTS(SELECT TOP 1 1 FROM [GMUModeGroup] WHERE GMUModeGroupName = 'W')
    INSERT [GMUModeGroup] ( GMUModeGroupName,GMUModeGroupDescription )
    SELECT 'W','Write'

IF NOT EXISTS(SELECT TOP 1 1 FROM [GMUModeGroup] WHERE GMUModeGroupName = 'RW')
    INSERT [GMUModeGroup] ( GMUModeGroupName,GMUModeGroupDescription )
    SELECT 'RW','Read/Write'    
GO



