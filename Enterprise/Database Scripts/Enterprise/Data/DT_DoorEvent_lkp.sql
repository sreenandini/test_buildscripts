/************************************************************
 * Created by Bally Technologies © 2011
 * Time: 26/12/12 5:43:30 PM
 ************************************************************/
USE [Enterprise]
GO

IF NOT EXISTS(SELECT 1 FROM [DoorEvent_lkp]WHERE EventID = '-1')
    INSERT [DoorEvent_lkp] ( EventID, EventDesc, OpenStatus, AssociatedEvent )
    SELECT -1, 'MaintenanceClose', 1, -1
ELSE
    UPDATE [DoorEvent_lkp]
    SET    EventDesc = 'MaintenanceClose', OpenStatus = 1, AssociatedEvent = -1
    WHERE  EventID = '-1'

IF NOT EXISTS(SELECT 1 FROM [DoorEvent_lkp]WHERE EventID = '1')
    INSERT [DoorEvent_lkp] ( EventID, EventDesc, OpenStatus, AssociatedEvent )
    SELECT 1, 'Front Door', 1, 2
ELSE
    UPDATE [DoorEvent_lkp]
    SET    EventDesc = 'Front Door', OpenStatus = 1, AssociatedEvent = 2
    WHERE  EventID = '1'

IF NOT EXISTS(SELECT 1 FROM [DoorEvent_lkp]WHERE EventID = '2')
    INSERT [DoorEvent_lkp] ( EventID, EventDesc, OpenStatus, AssociatedEvent )
    SELECT 2, 'Front Door', 0, 1
ELSE
    UPDATE [DoorEvent_lkp]
    SET    EventDesc = 'Front Door', OpenStatus = 0, AssociatedEvent = 1
    WHERE  EventID = '2'

IF NOT EXISTS(SELECT 1 FROM [DoorEvent_lkp]WHERE EventID = '3')
    INSERT [DoorEvent_lkp] ( EventID, EventDesc, OpenStatus, AssociatedEvent )
    SELECT 3, 'Cash Door', 1, 4
ELSE
    UPDATE [DoorEvent_lkp]
    SET    EventDesc = 'Cash Door', OpenStatus = 1, AssociatedEvent = 4
    WHERE  EventID = '3'

IF NOT EXISTS(SELECT 1 FROM [DoorEvent_lkp]WHERE EventID = '4')
    INSERT [DoorEvent_lkp] ( EventID, EventDesc, OpenStatus, AssociatedEvent )
    SELECT 4, 'Cash Door', 0, 3
ELSE
    UPDATE [DoorEvent_lkp]
    SET    EventDesc = 'Cash Door', OpenStatus = 0, AssociatedEvent = 3
    WHERE  EventID = '4'

IF NOT EXISTS(SELECT 1 FROM [DoorEvent_lkp]WHERE EventID = '9')
    INSERT [DoorEvent_lkp] ( EventID, EventDesc, OpenStatus, AssociatedEvent )
    SELECT 9, 'Coin Acceptor Disconnect', NULL, 9
ELSE
    UPDATE [DoorEvent_lkp]
    SET    EventDesc = 'Coin Acceptor Disconnect', OpenStatus = NULL, AssociatedEvent = 9
    WHERE  EventID = '9'

IF NOT EXISTS(SELECT 1 FROM [DoorEvent_lkp]WHERE EventID = '10')
    INSERT [DoorEvent_lkp] ( EventID, EventDesc, OpenStatus, AssociatedEvent )
    SELECT 10, 'Slot Door', 1, 11
ELSE
    UPDATE [DoorEvent_lkp]
    SET    EventDesc = 'Slot Door', OpenStatus = 1, AssociatedEvent = 11
    WHERE  EventID = '10'

IF NOT EXISTS(SELECT 1 FROM [DoorEvent_lkp]WHERE EventID = '11')
    INSERT [DoorEvent_lkp] ( EventID, EventDesc, OpenStatus, AssociatedEvent )
    SELECT 11, 'Slot Door', 0, 10
ELSE
    UPDATE [DoorEvent_lkp]
    SET    EventDesc = 'Slot Door', OpenStatus = 0, AssociatedEvent = 10
    WHERE  EventID = '11'

IF NOT EXISTS(SELECT 1 FROM [DoorEvent_lkp]WHERE EventID = '12')
    INSERT [DoorEvent_lkp] ( EventID, EventDesc, OpenStatus, AssociatedEvent )
    SELECT 12, 'Stacker Door', 1, 13
ELSE
    UPDATE [DoorEvent_lkp]
    SET    EventDesc = 'Stacker Door', OpenStatus = 1, AssociatedEvent = 13
    WHERE  EventID = '12'

IF NOT EXISTS(SELECT 1 FROM [DoorEvent_lkp]WHERE EventID = '13')
    INSERT [DoorEvent_lkp] ( EventID, EventDesc, OpenStatus, AssociatedEvent )
    SELECT 13, 'Stacker Door', 0, 12
ELSE
    UPDATE [DoorEvent_lkp]
    SET    EventDesc = 'Stacker Door', OpenStatus = 0, AssociatedEvent = 12
    WHERE  EventID = '13'

IF NOT EXISTS(SELECT 1 FROM [DoorEvent_lkp]WHERE EventID = '14')
    INSERT [DoorEvent_lkp] ( EventID, EventDesc, OpenStatus, AssociatedEvent )
    SELECT 14, 'Drop Door', 1, 15
ELSE
    UPDATE [DoorEvent_lkp]
    SET    EventDesc = 'Drop Door', OpenStatus = 1, AssociatedEvent = 15
    WHERE  EventID = '14'

IF NOT EXISTS(SELECT 1 FROM [DoorEvent_lkp]WHERE EventID = '15')
    INSERT [DoorEvent_lkp] ( EventID, EventDesc, OpenStatus, AssociatedEvent )
    SELECT 15, 'Drop Door', 0, 14
ELSE
    UPDATE [DoorEvent_lkp]
    SET    EventDesc = 'Drop Door', OpenStatus = 0, AssociatedEvent = 14
    WHERE  EventID = '15'

IF NOT EXISTS(SELECT 1 FROM [DoorEvent_lkp]WHERE EventID = '16')
    INSERT [DoorEvent_lkp] ( EventID, EventDesc, OpenStatus, AssociatedEvent )
    SELECT 16, 'Card Cage Door', 1, 17
ELSE
    UPDATE [DoorEvent_lkp]
    SET    EventDesc = 'Card Cage Door', OpenStatus = 1, AssociatedEvent = 17
    WHERE  EventID = '16'

IF NOT EXISTS(SELECT 1 FROM [DoorEvent_lkp]WHERE EventID = '17')
    INSERT [DoorEvent_lkp] ( EventID, EventDesc, OpenStatus, AssociatedEvent )
    SELECT 17, 'Card Cage Door', 0, 16
ELSE
    UPDATE [DoorEvent_lkp]
    SET    EventDesc = 'Card Cage Door', OpenStatus = 0, AssociatedEvent = 16
    WHERE  EventID = '17'

IF NOT EXISTS(SELECT 1 FROM [DoorEvent_lkp]WHERE EventID = '18')
    INSERT [DoorEvent_lkp] ( EventID, EventDesc, OpenStatus, AssociatedEvent )
    SELECT 18, 'Belly Door', 1, 19
ELSE
    UPDATE [DoorEvent_lkp]
    SET    EventDesc = 'Belly Door', OpenStatus = 1, AssociatedEvent = 19
    WHERE  EventID = '18'

IF NOT EXISTS(SELECT 1 FROM [DoorEvent_lkp]WHERE EventID = '19')
    INSERT [DoorEvent_lkp] ( EventID, EventDesc, OpenStatus, AssociatedEvent )
    SELECT 19, 'Belly Door', 0, 18
ELSE
    UPDATE [DoorEvent_lkp]
    SET    EventDesc = 'Belly Door', OpenStatus = 0, AssociatedEvent = 18
    WHERE  EventID = '19'

IF NOT EXISTS(SELECT 1 FROM [DoorEvent_lkp]WHERE EventID = '20')
    INSERT [DoorEvent_lkp] ( EventID, EventDesc, OpenStatus, AssociatedEvent )
    SELECT 20, 'Stacker removed', 1, 21
ELSE
    UPDATE [DoorEvent_lkp]
    SET    EventDesc = 'Stacker removed', OpenStatus = 1, AssociatedEvent = 21
    WHERE  EventID = '20'

IF NOT EXISTS(SELECT 1 FROM [DoorEvent_lkp]WHERE EventID = '21')
    INSERT [DoorEvent_lkp] ( EventID, EventDesc, OpenStatus, AssociatedEvent )
    SELECT 21, 'Stacker replaced', 0, 20
ELSE
    UPDATE [DoorEvent_lkp]
    SET    EventDesc = 'Stacker replaced', OpenStatus = 0, AssociatedEvent = 20
    WHERE  EventID = '21'

IF NOT EXISTS(SELECT 1 FROM [DoorEvent_lkp]WHERE EventID = '22')
    INSERT [DoorEvent_lkp] ( EventID, EventDesc, OpenStatus, AssociatedEvent )
    SELECT 22, 'GMU Compartment', 1, 23
ELSE
    UPDATE [DoorEvent_lkp]
    SET    EventDesc = 'GMU Compartment', OpenStatus = 1, AssociatedEvent = 23
    WHERE  EventID = '22'

IF NOT EXISTS(SELECT 1 FROM [DoorEvent_lkp]WHERE EventID = '23')
    INSERT [DoorEvent_lkp] ( EventID, EventDesc, OpenStatus, AssociatedEvent )
    SELECT 23, 'GMU Compartment', 0, 22
ELSE
    UPDATE [DoorEvent_lkp]
    SET    EventDesc = 'GMU Compartment', OpenStatus = 0, AssociatedEvent = 22
    WHERE  EventID = '23'

IF NOT EXISTS(SELECT 1 FROM [DoorEvent_lkp]WHERE EventID = '35')
    INSERT [DoorEvent_lkp] ( EventID, EventDesc, OpenStatus, AssociatedEvent )
    SELECT 35, 'Aux fill Door', 1, 36
ELSE
    UPDATE [DoorEvent_lkp]
    SET    EventDesc = 'Aux fill Door', OpenStatus = 1, AssociatedEvent = 36
    WHERE  EventID = '35'

IF NOT EXISTS(SELECT 1 FROM [DoorEvent_lkp]WHERE EventID = '36')
    INSERT [DoorEvent_lkp] ( EventID, EventDesc, OpenStatus, AssociatedEvent )
    SELECT 36, 'Aux fill Door', 0, 35
ELSE
    UPDATE [DoorEvent_lkp]
    SET    EventDesc = 'Aux fill Door', OpenStatus = 0, AssociatedEvent = 35
    WHERE  EventID = '36'

IF NOT EXISTS(SELECT 1 FROM [DoorEvent_lkp]WHERE EventID = '75')
    INSERT [DoorEvent_lkp] ( EventID, EventDesc, OpenStatus, AssociatedEvent )
    SELECT 75, 'Acceptor Door', 1, 76
ELSE
    UPDATE [DoorEvent_lkp]
    SET    EventDesc = 'Acceptor Door', OpenStatus = 1, AssociatedEvent = 76
    WHERE  EventID = '75'

IF NOT EXISTS(SELECT 1 FROM [DoorEvent_lkp]WHERE EventID = '76')
    INSERT [DoorEvent_lkp] ( EventID, EventDesc, OpenStatus, AssociatedEvent )
    SELECT 76, 'Acceptor Door', 0, 75
ELSE
    UPDATE [DoorEvent_lkp]
    SET    EventDesc = 'Acceptor Door', OpenStatus = 0, AssociatedEvent = 75
    WHERE  EventID = '76'

IF NOT EXISTS(SELECT 1 FROM [DoorEvent_lkp]WHERE EventID = '167')
    INSERT [DoorEvent_lkp] ( EventID, EventDesc, OpenStatus, AssociatedEvent )
    SELECT 167, 'MPU Compartment', 1, 168
ELSE
    UPDATE [DoorEvent_lkp]
    SET    EventDesc = 'MPU Compartment', OpenStatus = 1, AssociatedEvent = 168
    WHERE  EventID = '167'

IF NOT EXISTS(SELECT 1 FROM [DoorEvent_lkp]WHERE EventID = '168')
    INSERT [DoorEvent_lkp] ( EventID, EventDesc, OpenStatus, AssociatedEvent )
    SELECT 168, 'MPU Compartment', 0, 167
ELSE
    UPDATE [DoorEvent_lkp]
    SET    EventDesc = 'MPU Compartment', OpenStatus = 0, AssociatedEvent = 167
    WHERE  EventID = '168'

IF NOT EXISTS(SELECT 1 FROM [DoorEvent_lkp]WHERE EventID = '200')
    INSERT [DoorEvent_lkp] ( EventID, EventDesc, OpenStatus, AssociatedEvent )
    SELECT 200, 'Power Off Card Cage Access', NULL, 200
ELSE
    UPDATE [DoorEvent_lkp]
    SET    EventDesc = 'Power Off Card Cage Access', OpenStatus = NULL, AssociatedEvent = 200
    WHERE  EventID = '200'

IF NOT EXISTS(SELECT 1 FROM [DoorEvent_lkp]WHERE EventID = '201')
    INSERT [DoorEvent_lkp] ( EventID, EventDesc, OpenStatus, AssociatedEvent )
    SELECT 201, 'Power Off Slot Door Access', NULL, 201
ELSE
    UPDATE [DoorEvent_lkp]
    SET    EventDesc = 'Power Off Slot Door Access', OpenStatus = NULL, AssociatedEvent = 201
    WHERE  EventID = '201'

IF NOT EXISTS(SELECT 1 FROM [DoorEvent_lkp]WHERE EventID = '202')
    INSERT [DoorEvent_lkp] ( EventID, EventDesc, OpenStatus, AssociatedEvent )
    SELECT 202, 'Power Off Cash Box Door Access', NULL, 202
ELSE
    UPDATE [DoorEvent_lkp]
    SET    EventDesc = 'Power Off Cash Box Door Access', OpenStatus = NULL, AssociatedEvent = 202
    WHERE  EventID = '202'

IF NOT EXISTS(SELECT 1 FROM [DoorEvent_lkp]WHERE EventID = '203')
    INSERT [DoorEvent_lkp] ( EventID, EventDesc, OpenStatus, AssociatedEvent )
    SELECT 203, 'Power Off Drop Door Access', NULL, 203
ELSE
    UPDATE [DoorEvent_lkp]
    SET    EventDesc = 'Power Off Drop Door Access', OpenStatus = NULL, AssociatedEvent = 203
    WHERE  EventID = '203'

GO