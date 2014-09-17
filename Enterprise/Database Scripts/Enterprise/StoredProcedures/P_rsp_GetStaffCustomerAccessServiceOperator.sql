USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetStaffCustomerAccessServiceOperator]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetStaffCustomerAccessServiceOperator]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

  
CREATE  PROCEDURE [dbo].[rsp_GetStaffCustomerAccessServiceOperator]   
(
	@StaffId INT		
)
  
AS  
  
BEGIN  
  
SELECT  
DISTINCT
	Operator_Name,   
	Operator_ID   
FROM Operator OP
	INNER JOIN Depot D ON D.Supplier_ID = Op.Operator_ID
	INNER JOIN Customer_Access_Depot CAD ON CAD.Depot_ID = D.Depot_ID
	INNER JOIN Staff_Customer_Access SCA ON SCA.Customer_Access_ID = CAD.Customer_Access_ID
WHERE 	
	SCA.Staff_ID =  @StaffId
ORDER BY 
	Operator_Name 
ASC  
  
END  

GO

