USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_ecGetSubCompanyAreaDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_ecGetSubCompanyAreaDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE rsp_ecGetSubCompanyAreaDetails
	@SubCompanyRegionID INT = 0
AS
BEGIN
	SET NOCOUNT ON
	SELECT Sub_Company_Area.Sub_Company_Area_ID,
	       Sub_Company_Area.Sub_Company_Area_Name,
	       Sub_Company_Area.Sub_Company_Area_Description,
	       Sub_Company_Area.Staff_ID,
	       Staff.Staff_Last_Name,
	       Staff.Staff_First_Name
	FROM   Sub_Company_Area WITH (NOLOCK)
	       LEFT JOIN Staff WITH (NOLOCK)
	            ON  Sub_Company_Area.Staff_ID = Staff.Staff_ID
	WHERE  Sub_Company_Area.Sub_Company_Region_ID = @SubCompanyRegionID
END

GO

