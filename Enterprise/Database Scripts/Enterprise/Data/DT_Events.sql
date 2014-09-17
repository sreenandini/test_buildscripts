/************************************************************
 * Created by Bally Technologies © 2011
 * Time: 30/06/14 1:19:05 PM
 * SELECT * FROM GMUEventGroup
 ************************************************************/
USE Enterprise
GO
IF NOT EXISTS(SELECT 1 FROM [GMUEvents] WHERE GMUEventName = 'Bill cassette ReInserted')
    INSERT [GMUEvents] ( GMUEventName, GMUEventGroupID)
    SELECT 'Bill cassette ReInserted',1

IF NOT EXISTS(SELECT 1 FROM [GMUEvents] WHERE GMUEventName = '"Aux fill door opened')
    INSERT [GMUEvents] ( GMUEventName, GMUEventGroupID)
    SELECT 'Aux fill door opened',1

IF NOT EXISTS(SELECT 1 FROM [GMUEvents] WHERE GMUEventName = 'Aux fill door closed')
    INSERT [GMUEvents] ( GMUEventName, GMUEventGroupID)
    SELECT 'Aux fill door closed',1
    
IF NOT EXISTS(SELECT 1 FROM [GMUEvents] WHERE GMUEventName = 'Acceptor removed')
    INSERT [GMUEvents] ( GMUEventName, GMUEventGroupID)
    SELECT 'Acceptor removed',1
    
IF NOT EXISTS(SELECT 1 FROM [GMUEvents] WHERE GMUEventName = 'Slot door opened')
    INSERT [GMUEvents] ( GMUEventName, GMUEventGroupID)
    SELECT 'Slot door opened',1
    
IF NOT EXISTS(SELECT 1 FROM [GMUEvents] WHERE GMUEventName = 'Slot door closed')
    INSERT [GMUEvents] ( GMUEventName, GMUEventGroupID)
    SELECT 'Bill cassette ReInserted',1
    
 IF NOT EXISTS(SELECT 1 FROM [GMUEvents] WHERE GMUEventName = 'Drop Door opened')
    INSERT [GMUEvents] ( GMUEventName, GMUEventGroupID)
    SELECT 'Drop Door opened',1
    
IF NOT EXISTS(SELECT 1 FROM [GMUEvents] WHERE GMUEventName = 'Drop door closed')
    INSERT [GMUEvents] ( GMUEventName, GMUEventGroupID)
    SELECT 'Drop door closed',1