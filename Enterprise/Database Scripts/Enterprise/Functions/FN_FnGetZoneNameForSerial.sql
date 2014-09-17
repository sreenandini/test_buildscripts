USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FnGetZoneNameForSerial]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[FnGetZoneNameForSerial]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].FnGetZoneNameForSerial        
(@strSerial Varchar(50),@SiteCode varchar(50))        
RETURNS varchar(50)        
AS        
BEGIN        
  
DECLARE @ZoneName varchar(50)  
  
DECLARE @SITEID int  
SELECT @SITEID=Site_ID FROM SITE  WHERE Site_code=@SiteCode  
  
DECLARE @Return varchar(50)        
IF EXISTS( SELECT 1 FROM SiteWorkstations where Site_Workstation=@strSerial and site_ID=@SITEID)   
RETURN 'NA'  
   
IF EXISTS(SELECT 1 FROM ZONE Z  
			INNER JOIN SITE S ON S.Site_ID= Z.Site_ID  
			INNER JOIN Bar_Position BP ON S.Site_ID=BP.Site_ID AND S.Site_ID=@SITEID AND BP.Zone_ID=Z.Zone_ID   
			INNER JOIN INSTALLATION I ON I.Bar_Position_ID=BP.Bar_Position_ID    
			INNER JOIN  MACHINE M ON I.Machine_ID=M.Machine_ID     
			WHERE     
			M.Machine_Stock_No=@strSerial
) 
BEGIN    
SELECT @ZoneName=isnull(Zone_Name,'NA') FROM ZONE Z  
INNER JOIN SITE S ON S.Site_ID= Z.Site_ID  
INNER JOIN Bar_Position BP ON S.Site_ID=BP.Site_ID AND S.Site_ID=@SITEID AND BP.Zone_ID=Z.Zone_ID   
INNER JOIN INSTALLATION I ON I.Bar_Position_ID=BP.Bar_Position_ID    
INNER JOIN  MACHINE M ON I.Machine_ID=M.Machine_ID     
WHERE     
M.Machine_Stock_No=@strSerial
END
ELSE
SET @ZoneName= 'NA'                   

RETURN @ZoneName  
END   

GO

