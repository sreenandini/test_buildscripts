USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetCustomerAccessDepotonOperator]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetCustomerAccessDepotonOperator]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE  PROCEDURE [dbo].[rsp_GetCustomerAccessDepotonOperator] 
(
	@SupplierID INT,
	@StaffId INT	
)
AS

BEGIN

SELECT 
DISTINCT 
	D.Depot_Name, 
	D.Depot_ID 
--Depot_ID 
FROM Depot D 
INNER JOIN Customer_Access_Depot CAD ON CAD.DEPOT_ID = D.DEPOT_ID
INNER JOIN Staff_Customer_Access SCA ON SCA.CUSTOMER_ACCESS_ID = CAD.CUSTOMER_ACCESS_ID

WHERE 
	D.Supplier_ID = @SupplierID
AND	SCA.Staff_ID = @StaffId

ORDER BY Depot_Name 
ASC

END

GO

