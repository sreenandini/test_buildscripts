USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_UpdateUserInfoFromXML]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_UpdateUserInfoFromXML]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE usp_UpdateUserInfoFromXML
@doc xml 
AS
BEGIN
--set @doc='<Users><User_Info><SecurityuserID>1</SecurityuserID><Password>ISMvKXpXpadDiUoOSoAfww==</Password><PasswordChangeDate>2010-03-04T20:04:51.283</PasswordChangeDate><isReset>1</isReset><isLocked>0</isLocked></User_Info></Users>' 
DECLARE @docHandle int  
EXEC sp_xml_preparedocument @docHandle OUTPUT, @doc  

DECLARE @SecurityuserID int
DECLARE @Password varchar(200)
DECLARE @PasswordChangeDate datetime
DECLARE @isReset bit    
DECLARE @isLocked bit  
DECLARE @UserID int

SELECT	@SecurityuserID=x.SecurityuserID ,
		@Password=x.Password,
		@PasswordChangeDate=x.PasswordChangeDate,       
		@isReset=x.isReset,    
		@isLocked=x.isLocked          
  FROM OPENXML (@docHandle,  '/Users/User_Info',1)    
  WITH    
  (    
		SecurityuserID int 'SecurityuserID',
		Password varchar(200) 'Password',
		PasswordChangeDate datetime 'PasswordChangeDate',       
		isReset bit 'isReset',    
		isLocked bit 'isLocked'       

  )x    
EXEC sp_xml_removedocument @dochandle    

SELECT @UserID=SecurityUserID FROM [user] WHERE SecurityUserID=@SecurityuserID

IF @UserID IS NOT NULL
	BEGIN
		UPDATE [User]
		SET Password=@Password,
			PasswordChangeDate=@PasswordChangeDate,      
			isReset=@isReset,    
			isLocked=@isLocked       
		WHERE
			SecurityUserID=@SecurityuserID

		INSERT INTO dbo.Export_History(EH_Date,EH_Reference1,EH_Type, EH_Site_Code) 
		SELECT GETDATE(),@SecurityuserID,'CHANGEPASSWORD',
		site_code FROM site
		WHERE site_id in(SELECT siteID FROM usersite_lnk WHERE SecurityUserID=@SecurityuserID) 
	END
ELSE
	BEGIN  
		 RAISERROR ('Unable to find the user id from given xml', 16 , 1)  
		 RETURN(0)  
	END  
END

GO

