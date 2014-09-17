
USE [Enterprise]
GO

IF NOT EXISTS(SELECT 1 FROM [User_Group]WHERE User_Group_Name = 'Super User')
BEGIN
    INSERT [User_Group] (User_Group_Name ,HQ_User_Access_ID)
    SELECT  'Super User',(SELECT TOP 1 HQ_User_Access_ID FROM [HQ_User_Access]WHERE HQ_Admin_Users = '1')    	
END

GO
