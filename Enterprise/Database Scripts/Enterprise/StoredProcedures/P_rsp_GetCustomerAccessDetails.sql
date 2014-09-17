USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetCustomerAccessDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetCustomerAccessDetails]
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
   Kalaiyarasan.P             25-May-2012         Created               This SP is used to get CustomerAccess details   
                                                                         
*/  
  
CREATE PROCEDURE rsp_GetCustomerAccessDetails  
       
AS  
SELECT Customer_Access_ID  
      ,Customer_Access_Name       
  FROM Customer_Access ORDER BY Customer_Access_Name


GO

