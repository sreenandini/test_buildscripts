USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_MeterValues]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_MeterValues]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================  
-- Author:  <Rakesh Marwaha>  
-- ALTER  date: <17/04/2007>  
-- Description: <Insertion of data into Export_History table>  
-- =============================================  
CREATE  PROCEDURE [dbo].[usp_MeterValues]  
 -- Add the parameters for the stored procedure here  
 @RecordType Varchar(10),  
 @instID int=0,   
 @id int,  
 @HourNo int=-1  
    
AS  
BEGIN  
 -- SET NOCOUNT ON added to prevent extra result sets from  
 -- interfering with SELECT statements.  
 SET NOCOUNT ON;  
  
    -- Insert statements for procedure here  
 Select (SELECT Bar_Position_Name   
 FROM (((Installation INNER JOIN Bar_Position   
 ON Installation.Bar_Position_ID = Bar_Position.Bar_Position_ID)   
 INNER JOIN Machine ON Installation.Machine_ID = Machine.Machine_ID)   
 INNER JOIN Machine_Class ON Machine.Machine_Class_ID = Machine_Class.Machine_Class_ID)   
 WHERE Installation_ID = MH_Installation_No) As Bar_Position,  
 (SELECT Machine_Name FROM (((Installation INNER JOIN Bar_Position   
 ON Installation.Bar_Position_ID = Bar_Position.Bar_Position_ID)   
 INNER JOIN Machine ON Installation.Machine_ID = Machine.Machine_ID)   
 INNER JOIN Machine_Class ON Machine.Machine_Class_ID = Machine_Class.Machine_Class_ID)   
 WHERE Installation_ID = MH_Installation_No) As Name,  
 (SELECT Machine_Stock_No FROM (((Installation INNER JOIN Bar_Position   
 ON Installation.Bar_Position_ID = Bar_Position.Bar_Position_ID)   
 INNER JOIN Machine ON Installation.Machine_ID = Machine.Machine_Id)   
 INNER JOIN Machine_Class ON Machine.Machine_Class_ID = Machine_Class.Machine_Class_ID)   
 WHERE Installation_ID = MH_Installation_No) As Stock_No,*   
 from Meter_History  
 Where MH_linkreference=@id   
 AND(  
   (  
    @RecordType='VTP'  
    and MH_Process = 'VTP' and MH_Reference=@HourNo   
   )  
   OR  
   (  
    @RecordType='Daily'  
    and MH_Process = 'READ'  
   )  
   OR  
   (  
    @RecordType='Collection'  
    and MH_Process = 'COLL'  
   )  
  )  
 Order by MH_Type,MH_ID desc  
   
END  

GO

