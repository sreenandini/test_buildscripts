USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_UpdateDepotsCustomerAccess]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_UpdateDepotsCustomerAccess]
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
* Update the Customer access for depots       
* ******************************************************************************************    
*/    
  
CREATE PROCEDURE usp_UpdateDepotsCustomerAccess(
    @CustomerAccessID  INT,
    @DepotID           INT,
    @Status            BIT,
    @IsChecked         BIT
)
AS
BEGIN
	SET NOCOUNT ON    
	
	UPDATE Customer_Access
	SET    Customer_Access_View_All_Depots = @Status
	WHERE  Customer_Access_ID = @CustomerAccessID    
	
	
	IF @Status = 0
	BEGIN
	    IF @IsChecked = 1
	    BEGIN
	        INSERT INTO Customer_Access_Depot
	          (
	            Customer_Access_ID,
	            Depot_ID
	          )
	        VALUES
	          (
	            @CustomerAccessID,
	            @DepotID
	          )
	    END
	    ELSE
	    BEGIN
	        DELETE 
	        FROM   Customer_Access_Depot
	        WHERE  Customer_Access_ID = @CustomerAccessID
	               AND Depot_ID = @DepotID
	    END
	END
	ELSE
	BEGIN
	    DELETE 
	    FROM   Customer_Access_Depot
	    WHERE  Customer_Access_ID = @CustomerAccessID
	END
END
GO