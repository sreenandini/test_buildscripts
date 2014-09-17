USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetSiteCustomerAccess]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetSiteCustomerAccess]
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
* Retrive all the customer access        
* rsp_GetSiteCustomerAccess 0  
* ******************************************************************************************      
*/      
      
CREATE PROCEDURE rsp_GetSiteCustomerAccess    
(@CustomerAccessID INT = 0)  
AS      
      
BEGIN      
 SET NOCOUNT ON      
    
  SELECT    
   Customer_Access_ID,  
   Customer_Access_Name,  
   isnull(Customer_Access_View_All_Companies,0) AS Customer_Access_View_All_Companies,  
   isnull(Customer_Access_View_All_Depots,0) AS Customer_Access_View_All_Depots ,  
   isnull(Customer_Access_View_All_Sites,0) AS Customer_Access_View_All_Sites  
  FROM Customer_Access  CA   
  WHERE  
    (@CustomerAccessID =0) OR (@CustomerAccessID <> 0 AND CA.Customer_Access_ID = @CustomerAccessID)  
    
  ORDER BY  Customer_Access_Name 
END


GO

