USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_UpdateUserDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_UpdateUserDetails]
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
* <Name of the owner>         DateCreated       Type(Created/Modified)  Description    
  Kalaiyarasan.P              25-May-2012         Created               This SP is used to get Staff details     
                                                                        based on Staff_ID.    
*/    
CREATE PROCEDURE usp_UpdateUserDetails
	@WindowsUserName VARCHAR(200) ,
	@Password VARCHAR(200),
	@UserName VARCHAR(200),
	@LanguageID INT,
	@CurrencyCulture INT,
	@DateCulture INT,
	@SecurityUserID INT OUTPUT
AS
	IF NOT EXISTS ( SELECT 1 FROM [USER] u WHERE ((u.UserName = @UserName OR  u.WindowsUserName = @WindowsUserName) AND ISNULL(@SecurityUserID,0)=0)
	UNION
	 SELECT 1 FROM [USER] u WHERE u.WindowsUserName = @WindowsUserName AND u.SecurityUserID =ISNULL(@SecurityUserID,0) GROUP BY u.WindowsUserName HAVING COUNT(*)>1)
	BEGIN
	    IF NOT EXISTS (  
	       SELECT 1 FROM [USER] u WHERE u.SecurityUserID = @SecurityUserID 
	       )
	    BEGIN
	        INSERT INTO [USER]	( WindowsUserName, UserName, [Password], LanguageID, CurrencyCulture, DateCulture )
						 VALUES ( @WindowsUserName, @UserName, @Password, @LanguageID, @CurrencyCulture, @DateCulture  )
	        SELECT @SecurityUserID = SCOPE_IDENTITY()
	    END
	    ELSE
	    BEGIN
	        UPDATE [USER]
	        SET    WindowsUserName = @WindowsUserName, LanguageID = @LanguageID, CurrencyCulture = @CurrencyCulture, DateCulture = @DateCulture
	        WHERE  SecurityUserID = @SecurityUserID
	    END
	END
	ELSE
	BEGIN
		SET @SecurityUserID=0
	END
	---Exec usp_UpdateUserDetails 'sri','test1',1,1,1,3


GO

