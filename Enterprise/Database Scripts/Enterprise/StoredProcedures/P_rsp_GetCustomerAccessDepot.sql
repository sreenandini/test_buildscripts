USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetCustomerAccessDepot]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetCustomerAccessDepot]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*  
* *************************************************************************************  
* Revision History  
* *************************************************************************************   
* Anuradha  29/05/2012  Created    
*/  
CREATE PROCEDURE rsp_GetCustomerAccessDepot  
 @CustomerAccessID INT  
AS  
 SELECT d.Depot_ID,  
        d.Depot_Name  
 FROM   Customer_Access_Depot cad  
        INNER JOIN Depot d  
             ON  cad.Depot_ID = d.Depot_ID  
 WHERE  Customer_Access_ID = @CustomerAccessID


GO

