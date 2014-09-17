USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ASPNET_SP_ChangePassword]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ASPNET_SP_ChangePassword]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ASPNET_SP_ChangePassword](@SecurityUserID int, @password VARCHAR(200))    
AS    
BEGIN    
 IF NOT EXISTS (SELECT 1 FROM [USER] WHERE SecurityUserID = @SecurityUserID )     
  SELECT 'ERROR:01' AS RESULT    
 ELSE    
 BEGIN    
  UPDATE [USER]   
 SET Password = @password,   
  PasswordChangeDate=GetDate(),  
  isReset=0
 WHERE SecurityUserID = @SecurityUserID      
 INSERT INTO dbo.Export_History(EH_Date,EH_Reference1,EH_Type, EH_Site_Code)   
  SELECT GETDATE(),@SecurityuserID,'CHANGEPASSWORD',  
  site_code FROM site  
  WHERE site_id in(SELECT siteID FROM usersite_lnk WHERE SecurityUserID=@SecurityuserID)   
  SELECT 'SUCCESS' AS RESULT    
 END    
END

GO

