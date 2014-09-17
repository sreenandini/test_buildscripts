	 USE [Enterprise]
GO

/****** Object:  StoredProcedure [dbo].[rsp_CanRemoveCallGroup]    Script Date: 07/31/2014 16:09:44 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetSiteDetailsForServiceCall]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetSiteDetailsForServiceCall]
GO

USE [Enterprise]
GO

/****** Object:  StoredProcedure [dbo].[rsp_GetSiteDetailsForServiceCall]    Script Date: 07/31/2014 16:09:44 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


	 CREATE PROCEDURE rsp_GetSiteDetailsForServiceCall(@SiteId INT)
		 AS 
		 BEGIN 
			 SELECT Standard_Opening_Hours.Standard_Opening_Hours_Description, Site.Site_Name, Site.Site_Code, Site.Site_Address_1, Site.Site_Address_2, 
			 Site.Site_Address_3, Site.Site_Address_4, Site.Site_Address_5, Site.Site_Postcode, Site.Site_Manager, Site.Site_Phone_No, Depot.Depot_Name, 
			 Service_Areas.Service_Area_Name, Sub_Company.Sub_Company_Name FROM 		 
			 (((Site LEFT JOIN Depot ON Site.Service_Depot_ID = Depot.Depot_ID)
				LEFT JOIN Service_Areas ON Site.Service_Area_ID = Service_Areas.Service_Area_ID) 
				LEFT JOIN Sub_Company ON Site.Sub_Company_ID = Sub_Company.Sub_Company_ID)
				LEFT JOIN Standard_Opening_Hours ON Site.Standard_Opening_Hours_ID = Standard_Opening_Hours.Standard_Opening_Hours_ID 
			 WHERE Site_ID = @SiteId
		 END
 
