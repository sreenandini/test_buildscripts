USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_UpdateStaffCustomerAccess]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_UpdateStaffCustomerAccess]
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
  Kalaiyarasan.P              05-Sep-2012         Created               This SP is used to Update Staff_Customer_Access details     
                                                                        based on Staff_ID.    
*/    
CREATE PROCEDURE usp_UpdateStaffCustomerAccess
	@Staff_ID INT,
	@Customer_Access_ID INT
AS
BEGIN
	IF @Customer_Access_ID = 0
	BEGIN
	    DELETE 
	    FROM   Staff_Customer_Access
	    WHERE  Staff_ID = @Staff_ID
	END
	
	IF @Customer_Access_ID <> 0
	    INSERT INTO 
				Staff_Customer_Access ( Staff_ID, Customer_Access_ID ) 
	    VALUES ( @Staff_ID, @Customer_Access_ID  ) 
	    
	    --Exec usp_UpdateStaffCustomerAccess 2,4
END

GO

