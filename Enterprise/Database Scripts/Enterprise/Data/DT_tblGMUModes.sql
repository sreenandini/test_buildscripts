/************************************************************
 * Created by Bally Technologies © 2011
 * Time: 25/01/13 7:05:05 PM
 ************************************************************/
USE Enterprise
GO

IF NOT EXISTS(SELECT 1 FROM [tblGMUModes]WHERE GMuMode = '01')
    INSERT [tblGMUModes] ( GMUMode, Permission, GMUModedescription )
    SELECT '01', 'R', 'Used to view some information on the GMU and the EGM.'
ELSE
    UPDATE [tblGMUModes]
    SET    GMUMode = '01', Permission = 'R', GMUModedescription = 'Used to view some information on the GMU and the EGM.'
    WHERE  GMuMode = '01'

IF NOT EXISTS(SELECT 1 FROM [tblGMUModes]WHERE GMuMode = '02')
    INSERT [tblGMUModes] ( GMUMode, Permission, GMUModedescription )
    SELECT '02', 'RW', 'Used to program the gmu address and gmu game number'
ELSE
    UPDATE [tblGMUModes]
    SET    GMUMode = '02', Permission = 'RW', GMUModedescription = 'Used to program the gmu address and gmu game number'
    WHERE  GMuMode = '02'

IF NOT EXISTS(SELECT 1 FROM [tblGMUModes]WHERE GMuMode = '04')
    INSERT [tblGMUModes] ( GMUMode, Permission, GMUModedescription )
    SELECT '04', 'W', 'Used to reserve the gaming machine for a specific player.'
ELSE
    UPDATE [tblGMUModes]
    SET    GMUMode = '04', Permission = 'W', GMUModedescription = 'Used to reserve the gaming machine for a specific player'
    WHERE  GMuMode = '04'

IF NOT EXISTS(SELECT 1 FROM [tblGMUModes]WHERE GMuMode = '05')
    INSERT [tblGMUModes] ( GMUMode, Permission, GMUModedescription )
    SELECT '05', 'W', 'Used to communicate the service done to the gaming machine, in the form a service code. The service code shall range from 1 to 99'
ELSE
    UPDATE [tblGMUModes]
    SET    GMUMode = '05', Permission = 'W', GMUModedescription = 'Used to communicate the service done to the gaming machine, in the form a service code. The service code shall range from 1 to 99'
    WHERE  GMuMode = '05'

IF NOT EXISTS(SELECT 1 FROM [tblGMUModes]WHERE GMuMode = '06')
    INSERT [tblGMUModes] ( GMUMode, Permission, GMUModedescription )
    SELECT '06', 'W', 'Used to allow redirecting the pending jackpot handpay to the credit meter.'
ELSE
    UPDATE [tblGMUModes]
    SET    GMUMode = '06', Permission = 'W', GMUModedescription = 'Used to allow redirecting the pending jackpot handpay to the credit meter.'
    WHERE  GMuMode = '06'

IF NOT EXISTS(SELECT 1 FROM [tblGMUModes]WHERE GMuMode = '07')
    INSERT [tblGMUModes] ( GMUMode, Permission, GMUModedescription )
    SELECT '07', 'W', 'Used to print jackpot information slip'
ELSE
    UPDATE [tblGMUModes]
    SET    GMUMode = '07', Permission = 'W', GMUModedescription = 'Used to print jackpot information slip'
    WHERE  GMuMode = '07'

IF NOT EXISTS(SELECT 1 FROM [tblGMUModes]WHERE GMuMode = '10')
    INSERT [tblGMUModes] ( GMUMode, Permission, GMUModedescription )
    SELECT '10', 'R', 'Used to see the IP Address alloted to the GMU'
ELSE
    UPDATE [tblGMUModes]
    SET    GMUMode = '10', Permission = 'R', GMUModedescription = 'Used to see the IP Address alloted to the GMU'
    WHERE  GMuMode = '10'

IF NOT EXISTS(SELECT 1 FROM [tblGMUModes]WHERE GMuMode = '12')
    INSERT [tblGMUModes] ( GMUMode, Permission, GMUModedescription )
    SELECT '12', 'W', 'Used to send an intimation to the system, requesting to fill the hopper'
ELSE
    UPDATE [tblGMUModes]
    SET    GMUMode = '12', Permission = 'W', GMUModedescription = 'Used to send an intimation to the system, requesting to fill the hopper'
    WHERE  GMuMode = '12'

IF NOT EXISTS(SELECT 1 FROM [tblGMUModes]WHERE GMuMode = '13')
    INSERT [tblGMUModes] ( GMUMode, Permission, GMUModedescription )
    SELECT '13', 'W', 'Used to select a printer, where the hopper fill slip needs to be printed'
ELSE
    UPDATE [tblGMUModes]
    SET    GMUMode = '13', Permission = 'W', GMUModedescription = 'Used to select a printer, where the hopper fill slip needs to be printed'
    WHERE  GMuMode = '13'

IF NOT EXISTS(SELECT 1 FROM [tblGMUModes]WHERE GMuMode = '20')
    INSERT [tblGMUModes] ( GMUMode, Permission, GMUModedescription )
    SELECT '20', 'R', 'Used to view a snapshot of various meters available in the gaming machine'
ELSE
    UPDATE [tblGMUModes]
    SET    GMUMode = '20', Permission = 'R', GMUModedescription = 'Used to view a snapshot of various meters available in the gaming machine'
    WHERE  GMuMode = '20'

IF NOT EXISTS(SELECT 1 FROM [tblGMUModes]WHERE GMuMode = '55')
    INSERT [tblGMUModes] ( GMUMode, Permission, GMUModedescription )
    SELECT '55', 'I', 'Used by the gmu developer for debugging purpose. This mode shall not be exposed to the property.'
ELSE
    UPDATE [tblGMUModes]
    SET    GMUMode = '55', Permission = 'I', GMUModedescription = 'Used by the gmu developer for debugging purpose. This mode shall not be exposed to the property.'
    WHERE  GMuMode = '55'

IF NOT EXISTS(SELECT 1 FROM [tblGMUModes]WHERE GMuMode = '56')
    INSERT [tblGMUModes] ( GMUMode, Permission, GMUModedescription )
    SELECT '56', 'W', 'Used to clear all the meters and ticket records that are stored in the GMU.'
ELSE
    UPDATE [tblGMUModes]
    SET    GMUMode = '56', Permission = 'W', GMUModedescription = 'Used to clear all the meters and ticket records that are stored in the GMU.'
    WHERE  GMuMode = '56'

IF NOT EXISTS(SELECT 1 FROM [tblGMUModes]WHERE GMuMode = '57')
    INSERT [tblGMUModes] ( GMUMode, Permission, GMUModedescription )
    SELECT '57', 'W', 'Used to reboot the GMU.'
ELSE
    UPDATE [tblGMUModes]
    SET    GMUMode = '57', Permission = 'W', GMUModedescription = 'Used to reboot the GMU.'
    WHERE  GMuMode = '57'

IF NOT EXISTS(SELECT 1 FROM [tblGMUModes]WHERE GMuMode = '60')
    INSERT [tblGMUModes] ( GMUMode, Permission, GMUModedescription )
    SELECT '60', 'R', 'Used to display all epi devices that are currently online'
ELSE
    UPDATE [tblGMUModes]
    SET    GMUMode = '60', Permission = 'R', GMUModedescription = 'Used to display all epi devices that are currently online'
    WHERE  GMuMode = '60'

IF NOT EXISTS(SELECT 1 FROM [tblGMUModes]WHERE GMuMode = '61')
    INSERT [tblGMUModes] ( GMUMode, Permission, GMUModedescription )
    SELECT '61', 'RW', 'Used to view the SAS Version on the gaming machine. This mode shall also be used to enable or disable the gaming machine'
ELSE
    UPDATE [tblGMUModes]
    SET    GMUMode = '61', Permission = 'RW', GMUModedescription = 'Used to view the SAS Version on the gaming machine. This mode shall also be used to enable or disable the gaming machine'
    WHERE  GMuMode = '61'

IF NOT EXISTS(SELECT 1 FROM [tblGMUModes]WHERE GMuMode = '63')
    INSERT [tblGMUModes] ( GMUMode, Permission, GMUModedescription )
    SELECT '63', 'RW', 'Used to view the ticket records. This mode shall also be used to initiate ticket parameters and ticket key exchanges'
ELSE
    UPDATE [tblGMUModes]
    SET    GMUMode = '63', Permission = 'RW', GMUModedescription = 'Used to view the ticket records. This mode shall also be used to initiate ticket parameters and ticket key exchanges'
    WHERE  GMuMode = '63'

IF NOT EXISTS(SELECT 1 FROM [tblGMUModes]WHERE GMuMode = '70')
    INSERT [tblGMUModes] ( GMUMode, Permission, GMUModedescription )
    SELECT '70', 'RW', 'Used to view the eCash records. This mode shall also be used to clear the error that occurs during an eCash transaction.'
ELSE
    UPDATE [tblGMUModes]
    SET    GMUMode = '70', Permission = 'RW', GMUModedescription = 'Used to view the eCash records. This mode shall also be used to clear the error that occurs during an eCash transaction.'
    WHERE  GMuMode = '70'

IF NOT EXISTS(SELECT 1 FROM [tblGMUModes]WHERE GMuMode = '91')
    INSERT [tblGMUModes] ( GMUMode, Permission, GMUModedescription )
    SELECT '91', 'W', 'Used to indicate an emergency situation - at the spot where the gaming machine is located - to the system.'
ELSE
    UPDATE [tblGMUModes]
    SET    GMUMode = '91', Permission = 'W', GMUModedescription = 'Used to indicate an emergency situation - at the spot where the gaming machine is located - to the system.'
    WHERE  GMuMode = '91'

IF NOT EXISTS(SELECT 1 FROM [tblGMUModes]WHERE GMuMode = '98')
    INSERT [tblGMUModes] ( GMUMode, Permission, GMUModedescription )
    SELECT '98', 'R', 'Used to get the gmu version string.'
ELSE
    UPDATE [tblGMUModes]
    SET    GMUMode = '98', Permission = 'R', GMUModedescription = 'Used to get the gmu version string.'
    WHERE  GMuMode = '98'
    
GO