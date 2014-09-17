/************************************************************
 * Created by Bally Technologies © 2014
 * Time: 9/7/14 5:43:30 PM
 * Author: Aishwarrya V S
 ************************************************************/
USE [Enterprise]
GO

IF NOT EXISTS(SELECT TOP 1 1 FROM [GMUEventGroup] WHERE GMUEventGroupName  = 'Door')
    INSERT [GMUEventGroup] ( GMUEventGroupName)
    SELECT 'Door'

IF NOT EXISTS(SELECT TOP 1 1 FROM [GMUEventGroup] WHERE GMUEventGroupName = 'General')
    INSERT [GMUEventGroup] ( GMUEventGroupName )
    SELECT 'General'
IF NOT EXISTS(SELECT TOP 1 1 FROM [GMUEventGroup] WHERE GMUEventGroupName = 'Comms')
    INSERT [GMUEventGroup] ( GMUEventGroupName)
    SELECT 'Comms'
    
IF NOT EXISTS(SELECT TOP 1 1 FROM [GMUEventGroup] WHERE GMUEventGroupName = 'Power')
    INSERT [GMUEventGroup] ( GMUEventGroupName)
    SELECT 'Power'       
     
IF NOT EXISTS(SELECT TOP 1 1 FROM [GMUEventGroup] WHERE GMUEventGroupName = 'Printer')
    INSERT [GMUEventGroup] ( GMUEventGroupName)
    SELECT 'Printer'
    
IF NOT EXISTS(SELECT TOP 1 1 FROM [GMUEventGroup] WHERE GMUEventGroupName = 'Card')
    INSERT [GMUEventGroup] ( GMUEventGroupName)
    SELECT 'Card'
    
IF NOT EXISTS(SELECT TOP 1 1 FROM [GMUEventGroup] WHERE GMUEventGroupName = 'Ecash')
    INSERT [GMUEventGroup] ( GMUEventGroupName)
    SELECT 'Ecash' 
    
IF NOT EXISTS(SELECT TOP 1 1 FROM [GMUEventGroup] WHERE GMUEventGroupName = 'Jackpot')
    INSERT [GMUEventGroup] ( GMUEventGroupName)
    SELECT 'Jackpot'     

IF NOT EXISTS(SELECT TOP 1 1 FROM [GMUEventGroup] WHERE GMUEventGroupName = 'Ram Clear')
    INSERT [GMUEventGroup] ( GMUEventGroupName)
    SELECT 'Ram Clear'  

IF NOT EXISTS(SELECT TOP 1 1 FROM [GMUEventGroup] WHERE GMUEventGroupName = 'New Game Select')
    INSERT [GMUEventGroup] ( GMUEventGroupName)
    SELECT 'New Game Select'  

IF NOT EXISTS(SELECT TOP 1 1 FROM [GMUEventGroup] WHERE GMUEventGroupName = 'Machine Paid Bonus Event' )
    INSERT [GMUEventGroup] ( GMUEventGroupName)
    SELECT 'Machine Paid Bonus Event'  
    
IF NOT EXISTS(SELECT TOP 1 1 FROM [GMUEventGroup] WHERE GMUEventGroupName = 'Attendant paid Bonus Win')
    INSERT [GMUEventGroup] ( GMUEventGroupName)
    SELECT 'Attendant paid Bonus Win'      
    
IF NOT EXISTS(SELECT TOP 1 1 FROM [GMUEventGroup] WHERE GMUEventGroupName = 'Ticket')
    INSERT [GMUEventGroup] ( GMUEventGroupName)
    SELECT 'Ticket'   

IF NOT EXISTS(SELECT TOP 1 1 FROM [GMUEventGroup] WHERE GMUEventGroupName = 'Current Credit')
    INSERT [GMUEventGroup] ( GMUEventGroupName)
    SELECT 'Current Credit'

IF NOT EXISTS(SELECT TOP 1 1 FROM [GMUEventGroup] WHERE GMUEventGroupName = 'Meter Update')
    INSERT [GMUEventGroup] ( GMUEventGroupName)
    SELECT 'Meter Update'    

IF NOT EXISTS(SELECT TOP 1 1 FROM [GMUEventGroup] WHERE GMUEventGroupName = 'Other Events')
    INSERT [GMUEventGroup] ( GMUEventGroupName)
    SELECT 'Other Events'     
GO


