USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_GetMeterExceptionData]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_GetMeterExceptionData]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE usp_GetMeterExceptionData   
  @Type varchar(10)  
AS  
  
declare @SelectTable varchar(200)  
declare @WhereCondn  varchar(100)  
declare @JoinCondn   varchar(100)  
declare @SpikeFixed  varchar(10)  
declare @sSQL        varchar(1000)  
  
SET @sSQL=''  
--SELECT @SpikeFixed = '\'''  '%' + 'FIXED' + '%' + '\''' ESCAPE '\'  
  
  
  
if @Type = 'COLL'  
  begin  
 set @SelectTable= ' Select Machine_Name,Site_Name,MH_LinkReference,Bar_Position_Name, Collection.* from Meter_History '  
 set @WhereCondn = ' Where Exception.Exception_Type=202 and Installation.Installation_End_Date is null '  
 set @JoinCondn  = ' join Collection on Meter_History.MH_LinkReference = Collection.Collection_Id '  
  end  
else if @Type = 'VTP'  
  begin  
 set @SelectTable= ' Select Machine_Name,Site_Name,MH_LinkReference,Bar_Position_Name, VTP.*,MH_Reference from Meter_History '  
 set @WhereCondn = ' Where Exception.Exception_Type=200 and Installation.Installation_End_Date is null '  
 set @JoinCondn  = ' join VTP on Meter_History.MH_LinkReference = VTP.VTP_Id '  
  end  
else if @Type = 'READ'  
  begin  
 set @SelectTable= ' Select Machine_Name,Site_Name,MH_LinkReference,Bar_Position_Name, [Read].* from Meter_History '  
 set @WhereCondn = ' Where Exception.Exception_Type=201 and Installation.Installation_End_Date is null '  
 set @JoinCondn  = ' join [Read] on Meter_History.MH_LinkReference = [Read].Read_Id '  
  end  
  
      
  
SET @sSQL = @sSQL + @SelectTable  
SET @sSQL = @sSQL + @JoinCondn  
SET @sSQL = @sSQL + ' INNER JOIN dbo.Exception ON dbo.Meter_history.MH_LinkReference = Exception.Exception_Reference '  
SET @sSQL = @sSQL + ' INNER JOIN dbo.Installation ON dbo.Meter_history.MH_Installation_No = dbo.Installation.Installation_ID '  
SET @sSQL = @sSQL + ' INNER JOIN dbo.Machine ON dbo.Installation.Machine_ID = dbo.Machine.Machine_ID '  
SET @sSQL = @sSQL + ' INNER JOIN dbo.Machine_Class ON dbo.Machine.Machine_Class_ID = dbo.Machine_Class.Machine_Class_ID '  
SET @sSQL = @sSQL + ' INNER JOIN dbo.Bar_Position ON dbo.Installation.Bar_Position_ID = dbo.Bar_Position.Bar_Position_ID '  
SET @sSQL = @sSQL + ' INNER JOIN dbo.Site ON dbo.Bar_Position.Site_ID = dbo.Site.Site_ID '  
SET @sSQL = @sSQL + @WhereCondn + ' and MH_TYPE <> ''P''  ' + ' and COALESCE(exception.Exception_Details,'''') not like ' + '''%' + 'FIXED' + '%'''   
SET @sSQL = @sSQL + ' order by Installation.Installation_ID, Bar_Position_Name'   
Execute (@sSQL)  
  
  
IF @@ERROR > 0  
 begin   
  print @@ERROR  
  RETURN 0  
 end  
  

GO

