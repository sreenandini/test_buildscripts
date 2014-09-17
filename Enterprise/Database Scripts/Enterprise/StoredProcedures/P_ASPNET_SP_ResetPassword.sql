USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ASPNET_SP_ResetPassword]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ASPNET_SP_ResetPassword]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE ASPNET_SP_ResetPassword     
@UserID varchar(5),      
@Password varchar(200)      
AS      
BEGIN      
IF NOT EXISTS (SELECT 1 FROM [USER] WHERE SecurityUserID = @UserID )       
  SELECT 'ERROR:01' AS RESULT      
 ELSE      
 BEGIN    
  Update [user] set isReset=1,isLocked=0,Password=@Password, PasswordChangeDate=GetDate() where securityUserID=@UserID      
   
  INSERT INTO dbo.Export_History(EH_Date,EH_Reference1,EH_Type, EH_Site_Code)         
  SELECT GETDATE(),@UserID,'CHANGEPASSWORD',        
  site_code FROM site        
  WHERE site_id in(SELECT siteID FROM usersite_lnk WHERE SecurityUserID=@UserID) 
SELECT 'SUCCESS' AS RESULT          
 END    
END  

GO

