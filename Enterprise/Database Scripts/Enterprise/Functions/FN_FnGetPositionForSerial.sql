USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FnGetPositionForSerial]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[FnGetPositionForSerial]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


 
 CREATE FUNCTION [dbo].FnGetPositionForSerial        
(@strSerial Varchar(50),@SiteCode varchar(50))        
RETURNS varchar(50)        
AS        
BEGIN        
  
DECLARE @PositionName varchar(50)  
  
DECLARE @SITEID int  
select @SITEID=Site_ID FROM SITE  WHERE Site_code=@SiteCode  
  
 declare @Return varchar(50)        
 IF EXISTS( SELECT 1 FROM SiteWorkstations where Site_Workstation=@strSerial and site_ID=@SITEID)   
  RETURN 'CASHDESK'  
   
  SELECT @PositionName=isnull(Bar_Position_Name,'NA')   
  FROM SITE S  
  INNER JOIN Bar_Position BP ON S.Site_ID=BP.Site_ID AND S.Site_ID=@SITEID  
  INNER JOIN INSTALLATION I ON I.Bar_Position_ID=BP.Bar_Position_ID  
  INNER JOIN  MACHINE M ON I.Machine_ID=M.Machine_ID   
  WHERE   
  M.Machine_Stock_No=@strSerial                     
   
   
RETURN @PositionName  
END   

GO

