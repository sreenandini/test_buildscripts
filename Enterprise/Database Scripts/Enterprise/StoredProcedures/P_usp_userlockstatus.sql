USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_userlockstatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_userlockstatus]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE usp_userlockstatus @StaffID INT ,@isLocked INT
AS
BEGIN
DECLARE @SecurityUserID INT
SELECT @SecurityUserID = UserTableID FROM dbo.Staff WHERE staff_id =  @StaffID
UPDATE dbo.[user] SET isLocked=@isLocked
WHERE SecurityUserID = ISNULL(@SecurityUserID,0)


INSERT INTO dbo.Export_History(EH_Date,EH_Reference1,EH_Type, EH_Site_Code)           
  SELECT GETDATE(),@SecurityUserID,'CHANGEPASSWORD',site_code FROM site          
  WHERE site_id in(SELECT siteID FROM usersite_lnk WHERE SecurityUserID = @SecurityUserID)   
SELECT 'SUCCESS' AS RESULT
END

GO

