USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_newUserGroup]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_newUserGroup]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*
* Revision History
*
* Name : Selva Kumar S
* Date : 31st May 2012
*
* Name					DateCreated       Type(Created/Modified)  Description
*
*/

CREATE PROCEDURE usp_newUserGroup
@NewGroup NVARCHAR(100),@Result INT OUTPUT
AS
	IF NOT EXISTS (SELECT 1 FROM Role WHERE RoleName = @NewGroup)
	BEGIN
		DECLARE @HQ_User_Access_ID INT
		
		INSERT [ROLE]
			([RoleName],[RoleDescription])
		VALUES
			(@NewGroup,@NewGroup)

		INSERT HQ_User_Access
			([HQ_Admin],HQ_Guardian)
		VALUES
			(0,0)
		
		SET @HQ_User_Access_ID = SCOPE_IDENTITY()
		
		INSERT User_Group
			(User_Group_Name,HQ_User_Access_ID)
		VALUES
			(@NewGroup,@HQ_User_Access_ID)
		SET @Result = 1
	END
	ELSE
	BEGIN
		SET @Result = 0
	END

GO

