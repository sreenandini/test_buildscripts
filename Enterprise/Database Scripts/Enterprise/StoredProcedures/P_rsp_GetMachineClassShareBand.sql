USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetMachineClassShareBand]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetMachineClassShareBand]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*    
* Revision History    
*     
* <Name of the owner>         DateCreated       Type(Created/Modified)  Description    
        Kishore S             15-May-2014             Created          This SP is used to Read details Share_Band table                                                                       based on Staff_ID.    
*/    
CREATE PROCEDURE [rsp_GetMachineClassShareBand]
@Share_Schedule_ID INT 

AS
SET NOCOUNT ON
	
	SELECT Machine_Class_Share_Band.Machine_Class_ID, Machine_Class.Machine_Name,
	 Machine_Class.Machine_BACTA_Code, 
	 Machine_Class_Share_Band.Machine_Class_Share_Band,
	  Share_Band_Past.Share_Band_Name AS PastBandName, 
	  Machine_Class_Share_Band.Share_Band_ID_Past ,
	 Share_Band_Current.Share_Band_Name AS BandName,
	 Machine_Class_Share_Band.Share_Band_ID ,
	 Share_Band_Future.Share_Band_Name AS FutureBandName, 
	 Machine_Class_Share_Band.Share_Band_ID_Future ,
	 Machine_Class_Share_Band.Machine_Class_Share_Past_Date,
	  Machine_Class_Share_Band.Machine_Class_Share_Future_Date
            FROM 
            (((
            (Machine_Class RIGHT JOIN 
            Machine_Class_Share_Band ON Machine_Class.Machine_Class_ID = Machine_Class_Share_Band.Machine_Class_ID) 
            LEFT JOIN Share_Band AS Share_Band_Current ON Machine_Class_Share_Band.Share_Band_ID = Share_Band_Current.Share_Band_ID) 
            LEFT JOIN Share_Band AS Share_Band_Future ON Machine_Class_Share_Band.Share_Band_ID_Future = Share_Band_Future.Share_Band_ID) 
            LEFT JOIN Share_Band AS Share_Band_Past ON Machine_Class_Share_Band.Share_Band_ID_Past = Share_Band_Past.Share_Band_ID )
            WHERE Share_Band_Current.Share_Schedule_ID = @Share_Schedule_ID OR Share_Band_Past.Share_Schedule_ID = @Share_Schedule_ID 
            OR Share_Band_Future.Share_Schedule_ID = @Share_Schedule_ID 
      
GO
