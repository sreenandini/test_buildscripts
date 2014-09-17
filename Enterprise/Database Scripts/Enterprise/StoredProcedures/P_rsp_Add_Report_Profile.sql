USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_Add_Report_Profile]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_Add_Report_Profile]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER OFF
GO


-------------------------------------------------------------------------- 
--
-- Description: SP is being used to Add Report Profile of existing users of Staff table
--
-- Revision History
-- 
-- Rakesh Marwaha     18/Sep/2008     Created
CREATE PROCEDURE [dbo].[rsp_Add_Report_Profile]

@userID Varchar(50),	--UserName (Column->staff_username) from Staff table
@profileID INT			--Its value will be 1 to access all reports and 2 to access lotery reports

As

Declare @securityUserID INT

Set @securityUserID=(select max(Staff_ID)From Staff Where staff_username=@userID)
If Exists (Select 1 from user_security_profile Where usp_user_id=@securityUserID And usp_type=1)
	update user_security_profile Set usp_Reference=@profileID where usp_user_id=@securityUserID And usp_type=1
Else
	Insert into user_security_profile(usp_user_id,usp_type,usp_Reference) Values (@securityUserID,1,@profileID)


GO

