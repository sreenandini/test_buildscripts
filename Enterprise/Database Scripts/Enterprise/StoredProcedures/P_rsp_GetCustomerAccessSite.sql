USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetCustomerAccessSite]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetCustomerAccessSite]
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
CREATE PROCEDURE rsp_GetCustomerAccessSite  
 @CustomerAccessID INT  
AS  
 SELECT SecurityProfileType_Value,  
        AllowUser  
 FROM   SecurityProfile  
 WHERE  SecurityProfileType_ID = 2  
        AND Customer_Access_ID = @CustomerAccessID


GO

