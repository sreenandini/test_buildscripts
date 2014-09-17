USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_ecGetCompanyAdminDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_ecGetCompanyAdminDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE  PROC dbo.rsp_ecGetCompanyAdminDetails	
@CompanyID INT 
AS 
BEGIN
	SELECT Terms_Group_ID,
	       Terms_Group_Name
	FROM   Terms_Group
	ORDER BY
	       Terms_Group_Name ASC
	
	SELECT Access_Key_ID,
	       Access_Key_Name,
	       Access_Key_Ref,
	       Access_Key_Manufacturer,
	       Access_Key_Type
	FROM   Access_Key
	ORDER BY
	       Access_Key_Name ASC
	
	
	SELECT Staff_ID,
	       Staff_Last_Name,
	       Staff_First_Name
	FROM   Staff
	ORDER BY
	       Staff_Last_Name ASC,
	       Staff_First_Name ASC
	
	
	SELECT Company_Name,
	       Company_Switchboard_Phone_No,
	       Company_Address_1,
	       Company_Address_2,
	       Company_Address_3,
	       Company_Address_4,
	       Company_Address_5,
	       Company_Postcode,
	       Company_Contact_Name,
	       Company_Contact_Phone_No,
	       Company_Contact_Email_Address,
	       Company_Price_Per_Play,
	       Company_Jackpot,
	       Company_Percentage_Payout,
	       Terms_Group_ID,
	       Access_Key_ID,
	       Staff_ID,
	       Company_Invoice_Name,
	       Company_Invoice_Address,
	       Company_Invoice_Postcode
	FROM   Company
	WHERE  Company_ID = @CompanyID
END


GO

