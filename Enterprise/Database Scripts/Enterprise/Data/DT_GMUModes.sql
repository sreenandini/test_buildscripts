/************************************************************
 * Created by Bally Technologies © 2011
 * Time: 25/01/13 7:05:05 PM
 ************************************************************/
USE Enterprise
GO

IF NOT EXISTS(SELECT 1 FROM [GMUModes]WHERE GMuMode = '01')
    INSERT [GMUModes] ( GMUMode, GMUModeGroupID, GMUModedescription )
    SELECT '01', 1, 'Used to view some information on the GMU and the EGM.'
ELSE
    UPDATE [GMUModes]
    SET    GMUMode = '01', GMUModeGroupID = 1, GMUModedescription = 'Used to view some information on the GMU and the EGM.'
    WHERE  GMuMode = '01'

IF NOT EXISTS(SELECT 1 FROM [GMUModes]WHERE GMuMode = '02')
    INSERT [GMUModes] ( GMUMode, GMUModeGroupID, GMUModedescription )
    SELECT '02', 3, 'Used to program the gmu address and gmu game number'
ELSE
    UPDATE [GMUModes]
    SET    GMUMode = '02', GMUModeGroupID = 3, GMUModedescription = 'Used to program the gmu address and gmu game number'
    WHERE  GMuMode = '02'

IF NOT EXISTS(SELECT 1 FROM [GMUModes]WHERE GMuMode = '04')
    INSERT [GMUModes] ( GMUMode, GMUModeGroupID, GMUModedescription )
    SELECT '04', 2, 'Used to reserve the gaming machine for a specific player.'
ELSE
    UPDATE [GMUModes]
    SET    GMUMode = '04', GMUModeGroupID = 2, GMUModedescription = 'Used to reserve the gaming machine for a specific player'
    WHERE  GMuMode = '04'

IF NOT EXISTS(SELECT 1 FROM [GMUModes]WHERE GMuMode = '05')
    INSERT [GMUModes] ( GMUMode, GMUModeGroupID, GMUModedescription )
    SELECT '05', 2, 'Used to communicate the service done to the gaming machine, in the form a service code. The service code shall range from 1 to 99'
ELSE
    UPDATE [GMUModes]
    SET    GMUMode = '05', GMUModeGroupID = 2, GMUModedescription = 'Used to communicate the service done to the gaming machine, in the form a service code. The service code shall range from 1 to 99'
    WHERE  GMuMode = '05'

IF NOT EXISTS(SELECT 1 FROM [GMUModes]WHERE GMuMode = '06')
    INSERT [GMUModes] ( GMUMode, GMUModeGroupID, GMUModedescription )
    SELECT '06', 2, 'Used to allow redirecting the pending jackpot handpay to the credit meter.'
ELSE
    UPDATE [GMUModes]
    SET    GMUMode = '06', GMUModeGroupID = 2, GMUModedescription = 'Used to allow redirecting the pending jackpot handpay to the credit meter.'
    WHERE  GMuMode = '06'

IF NOT EXISTS(SELECT 1 FROM [GMUModes]WHERE GMuMode = '07')
    INSERT [GMUModes] ( GMUMode, GMUModeGroupID, GMUModedescription )
    SELECT '07', 2, 'Used to print jackpot information slip'
ELSE
    UPDATE [GMUModes]
    SET    GMUMode = '07', GMUModeGroupID = 2, GMUModedescription = 'Used to print jackpot information slip'
    WHERE  GMuMode = '07'

IF NOT EXISTS(SELECT 1 FROM [GMUModes]WHERE GMuMode = '10')
    INSERT [GMUModes] ( GMUMode, GMUModeGroupID, GMUModedescription )
    SELECT '10', 1, 'Used to see the IP Address alloted to the GMU'
ELSE
    UPDATE [GMUModes]
    SET    GMUMode = '10', GMUModeGroupID = 1, GMUModedescription = 'Used to see the IP Address alloted to the GMU'
    WHERE  GMuMode = '10'

IF NOT EXISTS(SELECT 1 FROM [GMUModes]WHERE GMuMode = '12')
    INSERT [GMUModes] ( GMUMode, GMUModeGroupID, GMUModedescription )
    SELECT '12', 2, 'Used to send an intimation to the system, requesting to fill the hopper'
ELSE
    UPDATE [GMUModes]
    SET    GMUMode = '12', GMUModeGroupID = 2, GMUModedescription = 'Used to send an intimation to the system, requesting to fill the hopper'
    WHERE  GMuMode = '12'

IF NOT EXISTS(SELECT 1 FROM [GMUModes]WHERE GMuMode = '13')
    INSERT [GMUModes] ( GMUMode, GMUModeGroupID, GMUModedescription )
    SELECT '13', 2, 'Used to select a printer, where the hopper fill slip needs to be printed'
ELSE
    UPDATE [GMUModes]
    SET    GMUMode = '13', GMUModeGroupID = 2, GMUModedescription = 'Used to select a printer, where the hopper fill slip needs to be printed'
    WHERE  GMuMode = '13'

IF NOT EXISTS(SELECT 1 FROM [GMUModes]WHERE GMuMode = '20')
    INSERT [GMUModes] ( GMUMode, GMUModeGroupID, GMUModedescription )
    SELECT '20', 1, 'Used to view a snapshot of various meters available in the gaming machine'
ELSE
    UPDATE [GMUModes]
    SET    GMUMode = '20', GMUModeGroupID = 1, GMUModedescription = 'Used to view a snapshot of various meters available in the gaming machine'
    WHERE  GMuMode = '20'

IF NOT EXISTS(SELECT 1 FROM [GMUModes]WHERE GMuMode = '56')
    INSERT [GMUModes] ( GMUMode, GMUModeGroupID, GMUModedescription )
    SELECT '56', 2, 'Used to clear all the meters and ticket records that are stored in the GMU.'
ELSE
    UPDATE [GMUModes]
    SET    GMUMode = '56', GMUModeGroupID = 2, GMUModedescription = 'Used to clear all the meters and ticket records that are stored in the GMU.'
    WHERE  GMuMode = '56'

IF NOT EXISTS(SELECT 1 FROM [GMUModes]WHERE GMuMode = '57')
    INSERT [GMUModes] ( GMUMode, GMUModeGroupID, GMUModedescription )
    SELECT '57', 2, 'Used to reboot the GMU.'
ELSE
    UPDATE [GMUModes]
    SET    GMUMode = '57', GMUModeGroupID = 2, GMUModedescription = 'Used to reboot the GMU.'
    WHERE  GMuMode = '57'

IF NOT EXISTS(SELECT 1 FROM [GMUModes]WHERE GMuMode = '60')
    INSERT [GMUModes] ( GMUMode, GMUModeGroupID, GMUModedescription )
    SELECT '60', 1, 'Used to display all epi devices that are currently online'
ELSE
    UPDATE [GMUModes]
    SET    GMUMode = '60', GMUModeGroupID = 1, GMUModedescription = 'Used to display all epi devices that are currently online'
    WHERE  GMuMode = '60'

IF NOT EXISTS(SELECT 1 FROM [GMUModes]WHERE GMuMode = '61')
    INSERT [GMUModes] ( GMUMode, GMUModeGroupID, GMUModedescription )
    SELECT '61', 3, 'Used to view the SAS Version on the gaming machine. This mode shall also be used to enable or disable the gaming machine'
ELSE
    UPDATE [GMUModes]
    SET    GMUMode = '61', GMUModeGroupID = 3, GMUModedescription = 'Used to view the SAS Version on the gaming machine. This mode shall also be used to enable or disable the gaming machine'
    WHERE  GMuMode = '61'

IF NOT EXISTS(SELECT 1 FROM [GMUModes]WHERE GMuMode = '63')
    INSERT [GMUModes] ( GMUMode, GMUModeGroupID, GMUModedescription )
    SELECT '63', 3, 'Used to view the ticket records. This mode shall also be used to initiate ticket parameters and ticket key exchanges'
ELSE
    UPDATE [GMUModes]
    SET    GMUMode = '63', GMUModeGroupID = 3, GMUModedescription = 'Used to view the ticket records. This mode shall also be used to initiate ticket parameters and ticket key exchanges'
    WHERE  GMuMode = '63'

IF NOT EXISTS(SELECT 1 FROM [GMUModes]WHERE GMuMode = '70')
    INSERT [GMUModes] ( GMUMode, GMUModeGroupID, GMUModedescription )
    SELECT '70', 3, 'Used to view the eCash records. This mode shall also be used to clear the error that occurs during an eCash transaction.'
ELSE
    UPDATE [GMUModes]
    SET    GMUMode = '70', GMUModeGroupID = 3, GMUModedescription = 'Used to view the eCash records. This mode shall also be used to clear the error that occurs during an eCash transaction.'
    WHERE  GMuMode = '70'

IF NOT EXISTS(SELECT 1 FROM [GMUModes]WHERE GMuMode = '91')
    INSERT [GMUModes] ( GMUMode, GMUModeGroupID, GMUModedescription )
    SELECT '91', 2, 'Used to indicate an emergency situation - at the spot where the gaming machine is located - to the system.'
ELSE
    UPDATE [GMUModes]
    SET    GMUMode = '91', GMUModeGroupID = 2, GMUModedescription = 'Used to indicate an emergency situation - at the spot where the gaming machine is located - to the system.'
    WHERE  GMuMode = '91'

IF NOT EXISTS(SELECT 1 FROM [GMUModes]WHERE GMuMode = '98')
    INSERT [GMUModes] ( GMUMode, GMUModeGroupID, GMUModedescription )
    SELECT '98', 1, 'Used to get the gmu version string.'
ELSE
    UPDATE [GMUModes]
    SET    GMUMode = '98', GMUModeGroupID = 1, GMUModedescription = 'Used to get the gmu version string.'
    WHERE  GMuMode = '98'


IF EXISTS(SELECT 1 FROM [GMUModes]WHERE GMuMode = '55')
    DELETE FROM GMUMODES WHERE GMUMODE='55'
GO


