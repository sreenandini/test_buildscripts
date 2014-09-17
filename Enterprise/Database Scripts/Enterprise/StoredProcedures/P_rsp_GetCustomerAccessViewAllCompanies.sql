USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetCustomerAccessViewAllCompanies]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetCustomerAccessViewAllCompanies]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE  PROCEDURE [dbo].[rsp_GetCustomerAccessViewAllCompanies] 
(
	@StaffId INT
)
AS

BEGIN

SELECT
TOP 1
Customer_Access_View_All_Companies  
FROM 
Staff_Customer_Access 
INNER JOIN Customer_Access ON Staff_Customer_Access.Customer_Access_ID = Customer_Access.Customer_Access_ID 
WHERE Staff_ID = @StaffId
ORDER BY Customer_Access_View_All_Companies  
DESC

END

GO

