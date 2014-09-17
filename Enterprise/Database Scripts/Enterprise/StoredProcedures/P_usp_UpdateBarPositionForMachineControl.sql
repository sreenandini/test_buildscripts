USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_UpdateBarPositionForMachineControl]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_UpdateBarPositionForMachineControl]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

  
  
CREATE PROCEDURE [dbo].[usp_UpdateBarPositionForMachineControl](@EH_Reference VARCHAR(4000),    
 @EH_Type VARCHAR(30),
@BarPosStatus int    
)  
AS    
    
BEGIN    
    
DECLARE @var VARCHAR(200)
declare @temp varchar(200)
declare @sql varchar(500)

SET @var = 'UPDATE Bar_Position SET' 
set @temp= ' WHERE CONVERT(VARCHAR, BAR_POSITION_ID) IN ' + '(' + '''' + REPLACE(@EH_Reference, ',', ''',''') + '''' + ')'

--check the eh type
IF (@EH_Type = 'MACHINEENABLE' or @EH_Type = 'MACHINEDISABLE')  
  BEGIN  
  --enable the machine by setting value 101 for pending enabled and 100 for pending disabled
     set @sql=@var + ' Bar_Position_Machine_Enabled = ' + CONVERT(VARCHAR, @BarPosStatus)  + @temp

	 --Added to update BMC_AAMS_Details table
	 UPDATE tBAD
	 SET tBAD.BMC_Enterprise_Status=(CASE @EH_Type WHEN 'MACHINEENABLE' THEN 1 ELSE 0 END),  
	 tBAD.BAD_Entity_Floor_Controller_Status=0      
	 from BMC_AAMS_Details tBAD    
	 INNER JOIN Machine tM On tM.Machine_Manufacturers_Serial_No = tBAD.BAD_Asset_Serial_No    
	 INNER JOIN installation tI On tM.Machine_ID = tI.Machine_ID    
	 INNER JOIN Bar_Position tBP On tBP.Bar_Position_ID = tI.Bar_Position_ID 
	 WHERE CONVERT(VARCHAR, tBP.Bar_Position_ID)IN ( REPLACE(@EH_Reference, ',', ''',''') ) 
    
  END  
ELSE IF(@EH_Type = 'NOTEACCEPTORENABLE' or @EH_Type = 'NOTEACCEPTORDISABLE')  
  BEGIN  
  --enable the noteacceptor by setting value 101 for pending enabled and 100 for pending disabled
	  set @sql=@var + ' Bar_Position_Note_Acceptor_Enabled = ' + CONVERT(VARCHAR, @BarPosStatus)  + @temp
  END  
END  
EXEC (@sql)


		

GO

