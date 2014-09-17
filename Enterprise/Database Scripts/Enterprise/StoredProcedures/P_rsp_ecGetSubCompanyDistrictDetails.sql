USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_ecGetSubCompanyDistrictDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_ecGetSubCompanyDistrictDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE rsp_ecGetSubCompanyDistrictDetails
	@SubCompanyAreaID INT = 0
AS
BEGIN
	SET NOCOUNT ON
	SELECT Sub_Company_District.Sub_Company_District_ID,
	       Sub_Company_District.Sub_Company_District_Name,
	       Sub_Company_District.Sub_Company_District_Description,
	       Sub_Company_District.Staff_ID,
	       Staff.Staff_Last_Name,
	       Staff.Staff_First_Name
	FROM   Sub_Company_District
	       LEFT JOIN Staff
	            ON  Sub_Company_District.Staff_ID = Staff.Staff_ID
	WHERE  Sub_Company_District.Sub_Company_Area_ID = @SubCompanyAreaID
END

GO

