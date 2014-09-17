	/************************************************************
 * Created by Bally Technologies © 2014
 * Time: 9/7/14 5:43:30 PM
 * Author: Aishwarrya V S 
 ************************************************************/
USE Enterprise
GO

TRUNCATE TABLE GMUEvents

GO
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Bill cassette ReInserted')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Bill cassette ReInserted',20,21,1
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Game MPU Removed')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Game MPU Removed',20,32,1 
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Game MPU Reinstalled')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Game MPU Reinstalled',20,33,1    

IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Aux fill door opened')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Aux fill door opened',20,35,1
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Aux fill door closed')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Aux fill door closed',20,36,1
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Acceptor removed')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Acceptor removed',20,66,1
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Slot door opened')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Slot door opened',20,10,1
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Slot door closed')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Slot door closed',20,11,1
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Drop Door opened')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Drop Door opened',20,14,1
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Drop door closed')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Drop door closed',20,15,1
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Acceptor door opened')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Acceptor door opened',20,75,1
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Acceptor door closed')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Acceptor door closed',20,76,1
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Bill cassette removed')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Bill cassette removed',20,20,1
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Bill cassette door opened')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Bill cassette door opened',20,3,1
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Bill cassette door closed')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Bill cassette door closed',20,4,1
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'MPU compartment opened')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'MPU compartment opened',20,167,1
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'MPU compartment closed')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'MPU compartment closed',20,168,1
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'GMU Compartment opened')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'GMU Compartment opened',20,22,1
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'GMU compartment closed')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'GMU compartment closed',20,23,1
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Power Off Card Cage Access')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Power Off Card Cage Access',20,200,1
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Power Off Slot Door Access')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Power Off Slot Door Access',20,201,1
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Power Off Cash Box Door Access')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Power Off Cash Box Door Access',20,202,1 
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Power Off Drop Door Access')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Power Off Drop Door Access',20,203,1 

IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Abandoned Card')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Abandoned Card',22,12,6
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Employee Card in')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Employee Card in',22,37,6
 
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Employee Card out')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Employee Card out',22,38,6
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Player Card in')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Player Card in',22,5,6
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Player Card out')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Player Card out',22,6,6    

     --select * from gmueventgroup
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Game MPU reset')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Game MPU reset',21,40,2
   
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'XCGMUWatchdogXC')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'XCGMUWatchdogXC',27,51,2
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'CompListChangedXC')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'CompListChangedXC',21,100,2
 
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Reel 1 Tilt.Tilt41')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Reel 1 Tilt.Tilt41',24,41,2
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Reel 2 Tilt.Tilt42')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Reel 2 Tilt.Tilt42',24,42,2

IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Reel 3 Tilt.Tilt43')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Reel 3 Tilt.Tilt43',24,43,2
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Reel 4 Tilt.Tilt44')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Reel 4 Tilt.Tilt44',24,44,2
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Reel 5 Tilt.Tilt45')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Reel 5 Tilt.Tilt45',24,45,2
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Slot machine tilt.Tilt64')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Slot machine tilt.Tilt64',24,64,2
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Reel spin after index.Tilt81')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Reel spin after index.Tilt81',24,81,2
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Reel spin after index.Tilt82')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Reel spin after index.Tilt82',24,82,2
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Reel spin after index.Tilt83')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Reel spin after index.Tilt83',24,83,2
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Reel spin after index.Tilt83')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Reel spin after index.Tilt83',24,83,2 

IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Reel spin after index.Tilt84')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Reel spin after index.Tilt84',24,84,2 
 
 IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Reel spin after index.Tilt85')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Reel spin after index.Tilt85',24,85,2 

IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Too Many Bad PINs')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Too Many Bad PINs',25,2,2
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'DivMalfunXC')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'DivMalfunXC',25,9,2
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Illegal Card removal')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Illegal Card removal',25,13,2
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Bad mag card reader')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Bad mag card reader',25,14,2
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Acceptor large buy-in')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Acceptor large buy-in',25,15,2
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Acceptor bad pay')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Acceptor bad pay',25,18,2
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Bonus point rollover')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Bonus point rollover',25,20,2
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Bad Machine Pay amt')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Bad Machine Pay amt',25,31,2 
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Reset during payout')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Reset during payout',25,47,2 
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Extra coins paid out')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Extra coins paid out',25,48,2 
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'No data on mag card')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'No data on mag card',25,50,2
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'GMU malfunction')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'GMU malfunction',25,55,2  
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Win with no handle pull')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Win with no handle pull',25,57,2  
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Win with no coin in')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Win with no coin in',25,58,2     
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Hopper can’t pay')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Hopper can’t pay',25,59,2
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Too many bills rejected')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Too many bills rejected',25,86,2
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Acceptor malfunction')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Acceptor malfunction',25,87,2
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Can’t read mag card')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Can’t read mag card',25,88,2
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Game memory malfunction')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Game memory malfunction',25,95,2
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'GMU meters reset')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'GMU meters reset',25,98,2
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Acceptor Hopper Jam')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Acceptor Hopper Jam',26,3,2
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Bill Acceptor Hardware Fail')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Bill Acceptor Hardware Fail',26,13,2
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'CMOS EEPROM Error')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'CMOS EEPROM Error',26,15,2
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Acceptor can’t vend')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Acceptor can’t vend',26,16,2
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Acceptor runaway hopper')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Acceptor runaway hopper',26,19,2
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Coin out jam')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Coin out jam',26,54,2
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Bill cassette is full')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Bill cassette is full',26,67,2
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Acceptor not responding')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Acceptor not responding',26,69,2
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Acceptor functioning again')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Acceptor functioning again',26,70,2
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Coin in jam')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Coin in jam',26,90,2
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Coin drop switch stuck')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Coin drop switch stuck',26,91,2
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Acceptor jammed')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Acceptor jammed',26,92,2
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Too many coins in')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Too many coins in',26,93,2
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Display fault')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Display fault',26,163,2
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Touch Screen error')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Touch Screen error',26,164,2
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Low battery condition')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Low battery condition',26,165,2
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Game EPROM Signature Fault')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Game EPROM Signature Fault',26,166,2
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Slot Printer Fault')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Slot Printer Fault',26,176,2
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Service Request')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Service Request',27,4,2
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Spintek Info Message')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Spintek Info Message',27,5,2
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'JCMStatusXC')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'JCMStatusXC',27,6,2
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'DMK Preemptive Fill')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'DMK Preemptive Fill',27,7,2
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'HotPlayerXC')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'HotPlayerXC',27,8,2

IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Payout percentage changed')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Payout percentage changed',27,10,2
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Reverse Bill Detected')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Reverse Bill Detected',27,14,2
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'PHY Link Down')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'PHY Link Down',27,11,2
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Game reserved')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Game reserved',27,23,2    

IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Coupon Redeemed')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Coupon Redeemed',27,26,2
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Coupon Request')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Coupon Request',27,28,2
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'DMK Fill Request')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'DMK Fill Request',27,29,2
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Jackpot to Credit Meter')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Jackpot to Credit Meter',27,30,2
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Back In Play')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Back In Play',27,46,2
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Blackout')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Blackout',27,62,2
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Machine paid jackpot')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Machine paid jackpot',27,63,2
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Game Activity report')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Game Activity report',27,65,2
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Bill vend to credit meter')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Bill vend to credit meter',27,89,2
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'EmpServiceEntry')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'EmpServiceEntry',27,99,2
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Patron request for info')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Patron request for info',27,160,2
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Unknown table index')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Unknown table index',27,161,2
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Employee key sequence')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Employee key sequence',27,162,2
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'GMU Intrepidized')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'GMU Intrepidized',27,182,2
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Freeform Response')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Freeform Response',27,183,2
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Freeform transport NAK')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Freeform transport NAK',27,184,2
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'FreeformXC')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'FreeformXC',27,185,2
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Acceptor SW Changed')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Acceptor SW Changed',27,186,2
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Acceptor SW Change Acknowledged')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Acceptor SW Change Acknowledged',27,187,2
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'GMU Initiated Freeform Message.(variable response)')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'GMU Initiated Freeform Message.(variable response)',27,188,2
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Change Request')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Change Request',28,21,2
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'GMU update request')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'GMU update request',28,17,2
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Beverage Request')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Beverage Request',28,22,2
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = '911 Emergency')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT '911 Emergency',28,24,2
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Request to change GMU')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Request to change GMU',28,25,2
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Cashout Request')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Cashout Request',28,177,2
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Clear player request')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Clear player request',28,180,2
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Qualifying play achieved')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Qualifying play achieved',28,181,2
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'AC power applied to game')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'AC power applied to game',31,1,2
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'AC power lost from game')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'AC power lost from game',31,2,2
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'General tilt')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'General tilt',31,3,2
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Counterfeit bill detected')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Counterfeit bill detected',31,4,2
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Reverse coin in detected')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Reverse coin in detected',31,5,2
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Cashbox near full detected')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Cashbox near full detected',31,6,2
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Memory error reset')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Memory error reset',31,7,2
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'A handpay has been validated')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'A handpay has been validated',31,8,2
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Validation ID not configured')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Validation ID not configured',31,9,2
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'No progressive information for 5 seconds')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'No progressive information for 5 seconds',31,10,2    

IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'SAS progressive level hit')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'SAS progressive level hit',31,11,2

IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Exception buffer overflow')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Exception buffer overflow',31,12,2
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Bill validator totals have been reset')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Bill validator totals have been reset',31,13,2    

IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'A legacy bonus pay and/or a multiplied jackpot')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'A legacy bonus pay and/or a multiplied jackpot',31,14,2
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Display meters or attendant menu entered')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Display meters or attendant menu entered',31,15,2
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Display meters or attendant menu exited')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Display meters or attendant menu exited',31,16,2
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Self test or operator menu entered')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Self test or operator menu entered',31,17,2
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Self test or operator menu exited')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Self test or operator menu exited',31,18,2
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Gaming machine is out of service')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Gaming machine is out of service',31,19,2
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'GMU power up')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'GMU power up',21,8,4
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Game power up')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Game power up',21,6,4
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Printer communication error')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Printer communication error',29,26,5 
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Printer paper empty')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Printer paper empty',29,27,5 
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Printer paper low')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Printer paper low',29,28,5 
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Printer power off')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Printer power off',29,29,5 
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Printer power on')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Printer power on',29,30,5 
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Replace printer ribbon')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Replace printer ribbon',29,31,5 
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Printer carriage jammed')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Printer carriage jammed',29,32,5 
  
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Hopper Full')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Hopper Full',30,11,5 
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Hopper level low')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Hopper level low',30,12,5 
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Cashless Balance')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Cashless Balance',23,21,7
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Cashless Withdrawal')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Cashless Withdrawal',23,22,7 

IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Cashless Collect')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Cashless Collect',23,23,7
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'ECash Balance Request')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'ECash Balance Request',23,1,7
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'ECash Withdrawal Request')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'ECash Withdrawal Request',23,2,7
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'ECash Deposit Request')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'ECash Deposit Request',23,3,7
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'ECash Deposit Complete')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'ECash Deposit Complete',23,4,7

IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Hand-Paid Jackpot')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Hand-Paid Jackpot',21,18,8
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Jackpot Reset')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Jackpot Reset',21,19,8
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Ticket Printed')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Ticket Printed',22,9,13
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Ticket Redeem')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Ticket Redeem',22,3,13
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Ticket Redeem CRC Failure')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Ticket Redeem CRC Failure',22,10,13
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Game meters cleared')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Game meters cleared',21,20,9
    
    --select * from gmueventgroup
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'New Game Selected')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'New Game Selected',21,174,10
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Forced periodic')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Forced periodic',21,60,15
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Periodic')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Periodic',21,16,15
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Instant Periodic')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Instant Periodic',21,80,15
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'CC Meter Update')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'CC Meter Update',21,249,15
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'NullXC')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'NullXC',1,1,15
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Machine Paid External Bonus Win')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Machine Paid External Bonus Win',21,204,11
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Start Cardless play')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Start Cardless play',21,178,16
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'End cardless play')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'End cardless play',21,179,16
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Polled Handpay Event')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Polled Handpay Event',21,243,16
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Zero Current Credit')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Zero Current Credit',22,205,14
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Game Comm lost')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Game Comm lost',254,4,3
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Game Comm Restored')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Game Comm Restored',254,5,3
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Denom and Payout')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Denom and Payout',254,250,3
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Total number of Games')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Total number of Games',254,251,3
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Game information')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Game information',254,252,3
    
IF NOT EXISTS(SELECT TOP 1 1 FROM GMUEvents WHERE GMUEventName = 'Attendant Paid External Bonus Win')
    INSERT GMUEvents ( GMUEventName, Event_Fault_Source, Event_Fault_Type,GMUEventGroupID )
    SELECT 'Attendant Paid External Bonus Win',21,206,12
    
    
    
   