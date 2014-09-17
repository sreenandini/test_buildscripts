USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_InsertUser_Access]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_InsertUser_Access]
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

CREATE PROCEDURE usp_InsertUser_Access
@Parent_Access_Key VARCHAR(100),
@Access_Key VARCHAR(100),
@Access_Name VARCHAR(100)
AS
	DECLARE @Access_Parent_Id INT
	
	SET @Access_Parent_Id = 0
		
	SELECT @Access_Parent_Id = Access_Id FROM User_Access(NOLOCK) WHERE Access_Key = @Parent_Access_Key
	
	IF @Parent_Access_Key = '' OR (@Parent_Access_Key != '' AND @Access_Parent_Id != 0)
	BEGIN
		IF NOT EXISTS ( SELECT 1 FROM User_Access WHERE Access_Key = @Access_Key )
		BEGIN
			INSERT User_Access
			(Access_Key,Access_Name,Access_Parent_Id)
			VALUES
			(@Access_Key,@Access_Name,@Access_Parent_Id)
		END
		ELSE
		BEGIN
			UPDATE User_Access SET 
			Access_Key = @Access_Key,Access_Name = @Access_Name,Access_Parent_Id = @Access_Parent_Id
			WHERE
			Access_Key = @Access_Key
		END
	END
	ELSE
	BEGIN
		SET @Parent_Access_Key = @Parent_Access_Key + ' -InVaild Parent_Access_Key'
		RAISERROR ( @Parent_Access_Key, 16 , 1)
	END

GO

