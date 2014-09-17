USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_GetZones]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_GetZones]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_GetZones] (@Site_Id int)  
--With Encryption  
As  
Begin  
 Select   
 Zone_ID as Zone_No,  
 Zone_ID as HQ_Zone_ID,  
 Zone_Name as Zone_Name,  
 Zone_Description as Zone_Description,  
 CONVERT(DATETIME, Zone_Start_Date, 101) as Zone_Start_Date,
 CONVERT(DATETIME, Zone_End_Date, 101) as Zone_End_Date  
  from Zone   
 Join Site on Zone.Site_Id=Site.Site_Id  
where Site_Code=@Site_Id  
  FOR XML PATH('Zone'),ROOT('Zones'), TYPE, Elements XSINIL
End  


GO

