USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetOrganizationHierarchy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetOrganizationHierarchy]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE rsp_GetOrganizationHierarchy AS
BEGIN
	SELECT 
		DISTINCT	
		Company.Company_ID,
		Company.Company_Name,
		Sub_Company.Sub_Company_ID,
		Sub_Company.Sub_Company_Name,
		Site.Site_ID,
		Site.Site_Name,
		Site.Site_Code
	FROM 
		Site 
	INNER JOIN Sub_Company 
		ON Site.Sub_Company_ID = Sub_Company.Sub_Company_ID 
	INNER JOIN Company 
		ON Sub_Company.Company_ID = Company.Company_ID 
	LEFT JOIN Bar_Position 
		ON Site.Site_ID = Bar_Position.Site_ID
	LEFT JOIN Depot 
		ON Bar_Position.Depot_ID = Depot.Depot_ID 
	LEFT JOIN Operator 
		ON Depot.Supplier_ID = Operator.Operator_ID 
	LEFT JOIN Installation 
		ON Bar_Position.Bar_Position_ID = Installation.Bar_Position_ID 
	LEFT JOIN Machine 
		ON Installation.Machine_ID = Machine.Machine_ID
	LEFT JOIN Machine_Class 
		ON Machine.Machine_Class_ID = Machine_Class.Machine_Class_ID 
	WHERE 
		Site.Site_End_Date is null
		AND LTRIM(RTRIM(ISNULL(Site.Site_Code,''))) <> ''
	ORDER BY 
		Company.Company_Name,
		Sub_Company.Sub_Company_Name,
		Site.Site_Name
		
END
GO

