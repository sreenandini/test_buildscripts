/************************************************************
 * Created by Bally Technologies © 2011
 * Time: 26/12/12 5:43:30 PM
 ************************************************************/
USE [Enterprise]
GO

IF NOT EXISTS(SELECT 1 FROM [Machine_Termination_Reason_Types]WHERE MTRT_ID = '5')
    INSERT [Machine_Termination_Reason_Types] ( MTRT_ID, MTRT_Description, MTRT_Display_Order )
    SELECT 5, 'Damaged', 1
ELSE
    UPDATE [Machine_Termination_Reason_Types]
    SET    MTRT_Description = 'Damaged', MTRT_Display_Order = 1
    WHERE  MTRT_ID = '5'

IF NOT EXISTS(SELECT 1 FROM [Machine_Termination_Reason_Types]WHERE MTRT_ID = '6')
    INSERT [Machine_Termination_Reason_Types] ( MTRT_ID, MTRT_Description, MTRT_Display_Order )
    SELECT 6, 'Disposal', 2
ELSE
    UPDATE [Machine_Termination_Reason_Types]
    SET    MTRT_Description = 'Disposal', MTRT_Display_Order = 2
    WHERE  MTRT_ID = '6'

IF NOT EXISTS(SELECT 1 FROM [Machine_Termination_Reason_Types]WHERE MTRT_ID = '2')
    INSERT [Machine_Termination_Reason_Types] ( MTRT_ID, MTRT_Description, MTRT_Display_Order )
    SELECT 2, 'Fire', 3
ELSE
    UPDATE [Machine_Termination_Reason_Types]
    SET    MTRT_Description = 'Fire', MTRT_Display_Order = 3
    WHERE  MTRT_ID = '2'

IF NOT EXISTS(SELECT 1 FROM [Machine_Termination_Reason_Types]WHERE MTRT_ID = '4')
    INSERT [Machine_Termination_Reason_Types] ( MTRT_ID, MTRT_Description, MTRT_Display_Order )
    SELECT 4, 'Sequestration', 4
ELSE
    UPDATE [Machine_Termination_Reason_Types]
    SET    MTRT_Description = 'Sequestration', MTRT_Display_Order = 4
    WHERE  MTRT_ID = '4'

IF NOT EXISTS(SELECT 1 FROM [Machine_Termination_Reason_Types]WHERE MTRT_ID = '3')
    INSERT [Machine_Termination_Reason_Types] ( MTRT_ID, MTRT_Description, MTRT_Display_Order )
    SELECT 3, 'Theft', 5
ELSE
    UPDATE [Machine_Termination_Reason_Types]
    SET    MTRT_Description = 'Theft', MTRT_Display_Order = 5
    WHERE  MTRT_ID = '3'

IF NOT EXISTS(SELECT 1 FROM [Machine_Termination_Reason_Types]WHERE MTRT_ID = '1')
    INSERT [Machine_Termination_Reason_Types] ( MTRT_ID, MTRT_Description, MTRT_Display_Order )
    SELECT 1, 'Withdrawal', 6
ELSE
    UPDATE [Machine_Termination_Reason_Types]
    SET    MTRT_Description = 'Withdrawal', MTRT_Display_Order = 6
    WHERE  MTRT_ID = '1'

IF NOT EXISTS(SELECT 1 FROM [Machine_Termination_Reason_Types]WHERE MTRT_ID = '99')
    INSERT [Machine_Termination_Reason_Types] ( MTRT_ID, MTRT_Description, MTRT_Display_Order )
    SELECT 99, 'Other', 99
ELSE
    UPDATE [Machine_Termination_Reason_Types]
    SET    MTRT_Description = 'Other', MTRT_Display_Order = 99
    WHERE  MTRT_ID = '99'

GO