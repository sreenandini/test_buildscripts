USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetCustomerAccessSubCompany]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetCustomerAccessSubCompany]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE  PROCEDURE [dbo].[rsp_GetCustomerAccessSubCompany]   
(  
 @CompanyID INT,  
 @StaffId INT   
)  
AS  
  
BEGIN  
  
SELECT 
SC.Sub_Company_ID, 
(SC.Sub_Company_Name + ' ' + ISNULL(SC.Sub_Company_AMEDIS_Code,'')) AS Sub_Company_Name 
--SC.Sub_Company_AMEDIS_Code 
FROM 
Sub_Company SC
INNER JOIN Customer_Access_Sub_Company CAS ON CAS.Sub_Company_ID = SC.Sub_Company_ID
INNER JOIN Staff_Customer_Access SCA ON SCA.Customer_Access_ID = CAS.Customer_Access_ID
WHERE
SC.Company_ID = @CompanyID
AND  SCA.Staff_ID = @StaffID
ORDER BY 
SC.Sub_Company_Name, SC.Sub_Company_AMEDIS_Code

END

GO

