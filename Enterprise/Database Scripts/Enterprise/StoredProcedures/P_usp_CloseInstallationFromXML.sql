USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_CloseInstallationFromXML]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_CloseInstallationFromXML]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

Create proc usp_CloseInstallationFromXML	
(
@doc xml	
)
as
DECLARE @docHandle int  
EXEC sp_xml_preparedocument @docHandle OUTPUT, @doc  
declare @InstallationID int
declare @EndDate datetime

SELECT @InstallationID = x.Installation_ID,
		@EndDate = x.End_Date
	 FROM OPENXML (@docHandle,  '/CloseInstallation/Installation',2)  
  with  
  (  
      Installation_ID int  'Installation_ID'  ,
	 End_Date datetime 'End_Date'
  ) x  

EXEC sp_xml_removedocument @dochandle  

update Installation 
set Installation_End_Date = convert(varchar, @EndDate, 106),
	Installation_End_Time = convert(varchar, @EndDate, 108)
where Installation_ID = @InstallationID
 

GO

