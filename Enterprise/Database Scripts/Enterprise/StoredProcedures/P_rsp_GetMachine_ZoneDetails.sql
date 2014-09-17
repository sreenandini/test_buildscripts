USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetMachine_ZoneDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetMachine_ZoneDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****
Version History
----------------------------------------
Kirubakar S		28 May 2010		Created
----------------------------------------
***/
CREATE PROCEDURE [dbo].rsp_GetMachine_ZoneDetails                       
@sType varchar(12),
@sInput VARCHAR(12),          
@SITE int=0          
AS          

IF @sType='Position'
BEGIN
	SELECT           
		Bar_Position_Name
	FROM Installation                   
		JOIN Bar_Position          
		ON Installation.Bar_Position_ID = Bar_Position.Bar_Position_ID        
		AND (@SITE=0 OR Bar_Position.Site_ID=@SITE)        
	WHERE Bar_Position.Bar_Position_End_Date Is Null  
		AND Installation.Installation_End_Date Is Null 
END
ELSE IF @sType='Zone'
BEGIN
		SELECT           
		Zone_Name
	FROM Zone                   
	WHERE	
		Zone.Site_ID=@SITE

END          

GO

