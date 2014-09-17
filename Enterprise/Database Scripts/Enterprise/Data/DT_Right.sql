/************************************************************
 * Created by Bally Technologies © 2011
 * Time: 26/12/12 4:35:54 PM
 ************************************************************/
USE Enterprise
GO

IF NOT EXISTS(SELECT 1 FROM [Right]WHERE SecurityRightID = '1')
    INSERT [Right] ( SecurityRightID, RightName, RightDescription )
    SELECT 1, 'Enable', 'Enable Control'
ELSE
    UPDATE [Right]
    SET    SecurityRightID = 1, RightName = 'Enable', RightDescription = 'Enable Control'
    WHERE  SecurityRightID = '1'

IF NOT EXISTS(SELECT 1 FROM [Right]WHERE SecurityRightID = '2')
    INSERT [Right] ( SecurityRightID, RightName, RightDescription )
    SELECT 2, 'Disable', 'Disableable Control'
ELSE
    UPDATE [Right]
    SET    SecurityRightID = 2, RightName = 'Disable', RightDescription = 'Disableable Control'
    WHERE  SecurityRightID = '2'

IF NOT EXISTS(SELECT 1 FROM [Right]WHERE SecurityRightID = '3')
    INSERT [Right] ( SecurityRightID, RightName, RightDescription )
    SELECT 3, 'Visible', 'Show Control'
ELSE
    UPDATE [Right]
    SET    SecurityRightID = 3, RightName = 'Visible', RightDescription = 'Show Control'
    WHERE  SecurityRightID = '3'

IF NOT EXISTS(SELECT 1 FROM [Right]WHERE SecurityRightID = '4')
    INSERT [Right] ( SecurityRightID, RightName, RightDescription )
    SELECT 4, 'Hide', 'Hide Control'
ELSE
    UPDATE [Right]
    SET    SecurityRightID = 4, RightName = 'Hide', RightDescription = 'Hide Control'
    WHERE  SecurityRightID = '4'
GO