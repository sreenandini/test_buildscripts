USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_ecGetSubCompanyRegionDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_ecGetSubCompanyRegionDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE rsp_ecGetSubCompanyRegionDetails
	@SubCompanyID INT = 0
AS
BEGIN
	SET NOCOUNT ON
	SELECT Sub_Company_Region.Sub_Company_Region_ID,
	       Sub_Company_Region.Sub_Company_Region_Name,
	       Sub_Company_Region.Sub_Company_Region_Description,
	       Sub_Company_Region.Staff_ID,
	       Staff.Staff_Last_Name,
	       Staff.Staff_First_Name
	FROM   Sub_Company_Region WITH (NOLOCK)
	       LEFT JOIN Staff  WITH (NOLOCK)
	            ON  Sub_Company_Region.Staff_ID = Staff.Staff_ID
	WHERE  Sub_Company_ID = @SubCompanyID
	
END

GO

