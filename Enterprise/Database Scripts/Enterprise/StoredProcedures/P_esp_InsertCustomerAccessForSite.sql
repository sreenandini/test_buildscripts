USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[esp_InsertCustomerAccessForSite]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[esp_InsertCustomerAccessForSite]
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
* Anuradha  29/05/2012  Created       
*     
* Update all the customer access      
* ******************************************************************************************    
*/    
    
CREATE PROCEDURE esp_InsertCustomerAccessForSite  
(  
 @CustomerAccessName VARCHAR(50)  
)  
AS    
    
BEGIN    
 SET NOCOUNT ON    
  
IF NOT EXISTS (SELECT * FROM Customer_Access ca WHERE ca.Customer_Access_Name =@CustomerAccessName)  
BEGIN  
 INSERT INTO Customer_Access  
 (  
  -- Customer_Access_ID -- this column value is auto-generated,  
  Customer_Access_Name,  
  Customer_Access_View_All_Companies,  
  Customer_Access_View_All_Depots,  
  Customer_Access_View_All_Sites  
 )  
 VALUES  
 (  
  @CustomerAccessName,  
  0,  
 0,  
  0  
 )  
 RETURN (0)  
END  
ELSE  
 RETURN (1)  
   
END   

GO

