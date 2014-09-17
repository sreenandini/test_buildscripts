USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_exportMeterValues]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_exportMeterValues]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

  
-- =============================================  
-- Author:  <Rakesh Marwaha>  
-- ALTER  date: <07/05/2007>  
-- Description: <For Meter Spikes>  
-- =============================================  
CREATE  PROCEDURE [dbo].[usp_exportMeterValues]   
 -- Add the parameters for the stored procedure here  
 @RecordSet Varchar(20), -- This identifies reason for calling this SP  
 @ID bigInt,   -- For Collection/VTP/READ ID  
 @RType Varchar(10), -- Type of Record, its Collection/VTP/READ  
 @vtpDate DateTime, -- Required to get ReadID from VTP  
 @updateREAD bigInt=0, -- For Checking whether to update READ from VTP or not  
 @HourNo bigInt=-1,  -- If its VTP record, this will contain Hour no  
 @barID bigInt=0  
  
AS  
   
BEGIN  
 -- SET NOCOUNT ON added to prevent extra result sets from  
 -- interfering with SELECT statements.  
 SET NOCOUNT ON;  
 SET DateFormat dmy  
    -- Insert statements for procedure here  
 BEGIN  
  IF (@RecordSet='MeterHistory')  
   BEGIN  
    select top 1 * from Meter_History   
    Where MH_linkreference=@ID   
    AND(  
      (  
       @RType='VTP'  
       and MH_Process = 'VTP' and MH_Reference=@HourNo   
       and MH_Type='P'  
      )  
      OR  
      (  
       @RType='Daily'  
       and MH_Process = 'READ' and MH_Type='P'  
      )  
      OR  
      (  
       @RType='Collection'  
       and MH_Process = 'COLL' and MH_Type='P'  
      )  
     )  
    Order by MH_ID Desc  
   END  
  
  ELSE IF(@RecordSet='Collection')  
   BEGIN  
    select C.batch_id,C.Collection_Date,C.Collection_Time,  
    (select batch_ref from batch where batch_id=C.batch_id) as batch_reference,  
    (select batch_name from batch where batch_id=C.batch_id) as batch_name   
    from collection as C where C.Collection_ID=@ID  
   END  
  
  ELSE IF(@RecordSet='Daily')  
   BEGIN  
    select Read_Date,Read_Time from [Read] where Read_ID=@ID  
   END  
  ELSE IF(@RecordSet='SiteID')  
   BEGIN  
    select Site.Site_ID from Bar_Position Join Site ON   
    Bar_Position.Site_ID = Site.Site_ID Where Bar_Position_ID=@barID  
   END  
 END  
END  
  

GO

