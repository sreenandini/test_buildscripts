USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetCompanyAccesstoCustomer]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetCompanyAccesstoCustomer]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE  PROCEDURE [dbo].[rsp_GetCompanyAccesstoCustomer] 
(
	@StaffId INT
)
AS

BEGIN

SELECT C.Company_ID, C.Company_Name 
FROM Company C
INNER JOIN Sub_Company SC ON C.Company_ID = SC.Company_ID
INNER JOIN Customer_Access_Sub_Company CAS ON SC.Sub_Company_ID = CAS.Sub_Company_ID
INNER JOIN Staff_Customer_Access SCA ON CAS.Customer_Access_ID = SCA.Customer_Access_ID
WHERE SCA.Staff_ID = @StaffID
ORDER BY C.Company_Name

END

GO

