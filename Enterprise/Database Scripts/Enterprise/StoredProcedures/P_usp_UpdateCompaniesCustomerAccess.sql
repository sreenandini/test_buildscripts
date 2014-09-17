USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_UpdateCompaniesCustomerAccess]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_UpdateCompaniesCustomerAccess]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*    
* Revision History    
* ******************************************************************************************    
* Anuradha  05/06/2012  Created       
*     
* Update the Customer access for companies       
* ******************************************************************************************    
*/    
  
CREATE PROCEDURE usp_UpdateCompaniesCustomerAccess(
    @CustomerAccessID  INT,
    @SubCompanyID      INT,
    @Status            BIT,
    @IsChecked         BIT
)
AS
BEGIN
	SET NOCOUNT ON	    
	
	UPDATE Customer_Access
	SET    Customer_Access_View_All_Companies = @Status
	WHERE  Customer_Access_ID = @CustomerAccessID    	
	
	IF @Status = 0
	BEGIN
	    IF @IsChecked = 1
	    BEGIN
	        INSERT INTO Customer_Access_Sub_Company
	          (
	            Customer_Access_ID,
	            Sub_Company_ID
	          )
	        VALUES
	          (
	            @CustomerAccessID,
	            @SubCompanyID
	          )
	    END
	    ELSE
	    BEGIN
	        DELETE 
	        FROM   Customer_Access_Sub_Company
	        WHERE  Customer_Access_ID = @CustomerAccessID
	               AND Sub_Company_ID = @SubCompanyID
	    END
	END
	ELSE
	BEGIN
	    DELETE 
	    FROM   Customer_Access_Sub_Company
	    WHERE  Customer_Access_ID = @CustomerAccessID
	END
END
GO