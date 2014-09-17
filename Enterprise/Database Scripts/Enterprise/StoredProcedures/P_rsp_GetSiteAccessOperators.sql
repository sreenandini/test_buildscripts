USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetSiteAccessOperators]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetSiteAccessOperators]
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
* Retrive all the operators available for this customer access      
* ******************************************************************************************    
*/    
    
CREATE PROCEDURE rsp_GetSiteAccessOperators    
AS    
    
BEGIN    
 SET NOCOUNT ON    
    SELECT   
      Operator.Operator_ID,  
   Operator_Name,  
   Depot_ID,   
   Depot_Name   
 FROM   
  Operator   
 INNER JOIN   
  Depot   
 ON   
  Operator.Operator_ID = Depot.Supplier_ID   
  
 ORDER BY Operator_Name, Operator.Operator_ID, Depot_Name  
END


GO

