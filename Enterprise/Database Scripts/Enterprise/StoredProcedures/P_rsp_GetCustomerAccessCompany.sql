USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetCustomerAccessCompany]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetCustomerAccessCompany]
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
CREATE PROCEDURE rsp_GetCustomerAccessCompany  
 @CustomerAccessID INT  
AS  
 SELECT sc.Sub_Company_ID,sc.Sub_Company_Name  
 FROM   Customer_Access_Sub_Company casc  
 INNER JOIN Sub_Company sc ON casc.Sub_Company_ID=sc.Sub_Company_ID  
 WHERE  Customer_Access_ID = @CustomerAccessID


GO

