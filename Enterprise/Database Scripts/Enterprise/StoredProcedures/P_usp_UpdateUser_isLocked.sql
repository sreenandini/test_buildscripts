USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_UpdateUser_isLocked]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_UpdateUser_isLocked]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--------------------------------------------------------------------------- 
--
-- Description: Used to Reset an User with a System Generated Password
--
-- Inputs:      
-- Outputs:     
--
-- =======================================================================
-- 
-- Revision History
-- 
-- Kirubakar S	22-03-2010   Created   
--------------------------------------------------------------------------- 


CREATE PROCEDURE usp_UpdateUser_isLocked    
@UserID varchar(5),    
@Password varchar(200),    
@Staff_Password varchar(50)  
AS    
BEGIN    
IF @UserID IS NOT NULL      
 BEGIN  
  Update [user] set isReset=1,isLocked=0,Password=@Password, PasswordChangeDate=GetDate() where securityUserID=@UserID    
  Update Staff set Staff_Password=@Staff_Password where UserTableID=@UserID   
  
  INSERT INTO dbo.Export_History(EH_Date,EH_Reference1,EH_Type, EH_Site_Code)       
  SELECT GETDATE(),@UserID,'CHANGEPASSWORD',      
  site_code FROM site      
  WHERE site_id in(SELECT siteID FROM usersite_lnk WHERE SecurityUserID=@UserID)     
 END  
END


GO

