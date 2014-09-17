USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetDetailFieldsonLoad]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetDetailFieldsonLoad]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[rsp_GetDetailFieldsonLoad] (@SiteID int)
AS

BEGIN

SELECT 
		Site.Site_ID,
		Site.Service_Supplier_ID,
		Site.Service_Area_ID,
		Site.Service_Depot_ID,
		Site.Site_Name, 
		Site.Site_Code, 
		Site.Site_Supplier_Code, 
		Site.Site_Company_Code, 
		Site.Site_Phone_No, 
		Site.Site_Fax_No, 
		Site.Site_Email_Address,
		Site.Site_Address_1, 
		Site.Site_Address_2, 
		Site.Site_Address_3, 
		Site.Site_Address_4, 
		Site.Site_Address_5, 
		Site.Site_Postcode, 
		Site.Site_Manager,
		Site.Site_Price_Per_Play_Default, 
		Site.Site_Price_Per_Play, 
		Site.Site_Jackpot_Default, 
		Site.Site_Jackpot, 
		Site.Site_Percentage_Payout_Default, 
		Site.Site_Percentage_Payout, 
		Site.Sub_Company_ID, 
		Sub_Company.Company_ID, 
		Site.Terms_Group_ID_Default, 
		Site.Terms_Group_ID, 
		Site.Access_Key_ID_Default, 
		Site.Access_Key_ID, 
		Site.Staff_ID_Default, 
		Site.Staff_ID, 
		Site_Invoice_Name, 
		Site.Site_Invoice_Address, 
		Site.Site_Invoice_Postcode, 
		Site.Sub_Company_Region_ID, 
		Site.Sub_Company_Area_ID, 
		Site.Sub_Company_District_ID, 
		Site.Site_Image_Reference, 
		Site.Site_Image_Reference_2,  		
		Site.Site_Closed, 
		Depot.Depot_ID, 
		Depot.Supplier_ID, 
		Site.Site_Classification_ID,
		Site.Site_Grade,  
		Site.Sage_Account_Ref, 
		Site.Site_Is_FreeFloat, 
		Site.Site_Local_Inbox, 
		Site.Site_Local_Outbox, 
		Site.Site_Memo, 
		Site_Reference,
		Site_Trade_Type, 
		Site_Non_Trading_Period_From, 
		Site_Non_Trading_Period_To, 
		Site_Supplier_Service_Area, 
		Site_Supplier_Area, 
		Standard_Opening_Hours_ID, 
		Next_Sub_Company_ID, 
		Next_Sub_Company_Change_Date, 
		Site_Previous_Sub_Company_ID, 
		Previous_Sub_Company_Change_Date, 
		Site.Site_Honeyframe_EDI, 
		Site.Site_Datapak_Protocol, 
		Site.Site_Start_Date, 
		Site.Site_End_Date, 
		Site.Site_Licence_Number, 
		Site.Site_Fiscal_Code, 
		Site.Site_Street_Number, 
		Site.Site_Province, 
		Site.Site_Municipality, 
		Site.Site_Cadastral_Code, 
		Site.Site_Area, 
		Site.Site_Location_Type, 
		Site.Site_Toponym, 
		Site.Site_Licensee_Commenced_Date, 
		Site.Site_Licensee_Agreement_End_Date, 
		Site.Site_Licensee_Agreement_Type, 
		Site_Application, 
		IsNull(Site.Region, 'Default') As Region, 
		IsNull(Site.WebURL, '') AS WebURL, 
		IsNull(Site.Site_Status_ID,0) as Site_Status_ID, 
		Site.Site_Inactive_Date, 
		Site_Connection_IPAddress,
		Site_MaxNumber_VLT,
		Site_ZonaRice, 
		StackerLimitPercentage 
FROM ((Site LEFT JOIN Depot ON Site.Depot_ID = Depot.Depot_ID) 
INNER JOIN Sub_Company ON Site.Sub_Company_ID = Sub_Company.Sub_Company_ID)
WHERE Site_ID = @SiteID

END


GO

