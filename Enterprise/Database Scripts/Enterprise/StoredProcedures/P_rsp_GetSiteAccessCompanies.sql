USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetSiteAccessCompanies]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetSiteAccessCompanies]
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
* Anuradha  25/05/2012  Created     
*   
* Retrive all the companies available for this customer access    
* ******************************************************************************************  
*/  
  
CREATE PROCEDURE rsp_GetSiteAccessCompanies  
AS  
  
BEGIN  
 SET NOCOUNT ON  
 SELECT   
  Company.Company_ID,   
  Company_Name,   
  Sub_Company_ID,   
  Sub_Company_Name   
 FROM   
  Company   
 INNER JOIN   
  Sub_Company   
 ON   
  Company.Company_ID = Sub_Company.Company_ID   
   
    ORDER BY Company_Name, Company.Company_ID, Sub_Company_Name  
END


GO

