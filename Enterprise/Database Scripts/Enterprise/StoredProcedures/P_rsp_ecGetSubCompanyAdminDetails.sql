USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_ecGetSubCompanyAdminDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_ecGetSubCompanyAdminDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE rsp_ecGetSubCompanyAdminDetails
	@SubCompanyID INT = 0
AS
BEGIN
	SET NOCOUNT ON
	SELECT Sub_Company_Name,
	       Company_ID,
	       Sub_Company_Switchboard_Phone_No,
	       Sub_Company_Address_1,
	       Sub_Company_Address_2,
	       Sub_Company_Address_3,
	       Sub_Company_Address_4,
	       Sub_Company_Address_5,
	       Sub_Company_Postcode,
	       Sub_Company_ANA_Number,
	       Sub_Company_Income_Ledger_Code,
	       Sage_Account_Ref,
	       Company_Model_Set_ID,
	       Sub_Company_Trade_Type,
	       Sub_Company_Trade_Type,
	       Sub_Company_Contact_Name,
	       Sub_Company_Contact_Phone_No,
	       Sub_Company_Contact_Email_Address,
	       Sub_Company_Use_Split_Rents,
	       Sub_Company_Price_Per_Play_Default,
	       Sub_Company_Price_Per_Play,
	       Sub_Company_Jackpot_Default,
	       Sub_Company_Jackpot,
	       Sub_Company_Percentage_Payout_Default,
	       Sub_Company_Percentage_Payout,
	       Terms_Group_ID_Default,
	       Terms_Group_ID,
	       Access_Key_ID_Default,
	       Access_Key_ID,
	       Staff_ID_Default,
	       Staff_ID,
	       Sub_Company_Default_Opening_Hours_ID,
	       Sub_Company_Invoice_Name,
	       Sub_Company_Invoice_Address,
	       Sub_Company_Invoice_Postcode,
	       Sub_Company_Account_Name,
	       Sub_Company_Account_No,
	       Sub_Company_Sort_Code
	FROM   Sub_Company sc
	WHERE  sc.Sub_Company_ID = @SubCompanyID
	
	EXEC rsp_ecGetSubCompanyRegionDetails @SubCompanyID
	
	SELECT Company_Percentage_Payout,
	       Company.Company_Price_Per_Play,
	       Company.Company_Jackpot,
	       Company.Terms_Group_ID,
	       Company.Access_Key_ID,
	       Company.Staff_ID
	FROM   Company
	       INNER JOIN Sub_Company
	            ON  Company.Company_ID = Sub_Company.Company_ID
	WHERE  Sub_Company.Sub_Company_ID = @SubCompanyID
	
	
	SELECT Terms_Group_ID,
	       Terms_Group_Name
	FROM   Terms_Group
	ORDER BY
	       Terms_Group_Name ASC
	
	SELECT Company.Access_Key_ID,
	       Access_Key.Access_Key_Name
	FROM   (
	           Company INNER JOIN Sub_Company ON Company.Company_ID =
	           Sub_Company.Company_ID
	       )
	       LEFT JOIN Access_Key
	            ON  Company.Access_Key_ID = Access_Key.Access_Key_ID
	WHERE  Sub_Company.Sub_Company_ID = @SubCompanyID
	
	SELECT Company_ID,
	       Company_Name
	FROM   Company
	WHERE  Company_End_Date IS NULL
	ORDER BY
	       Company_Name
	
	SELECT Company_Model_Set_ID,
	       Company_Model_Set_Description
	FROM   Company_Model_Set
	ORDER BY
	       Company_Model_Set_Description ASC
	
	SELECT Standard_Opening_Hours_ID,
	       Standard_Opening_Hours_Description
	FROM   Standard_Opening_Hours
	ORDER BY
	       Standard_Opening_Hours_Description
	
	SELECT Staff_Last_Name + ', ' + Staff_First_Name AS StaffName,
	       Staff_ID
	FROM   Staff
	ORDER BY
	       Staff_Last_Name ASC,
	       Staff_First_Name ASC
	
	SELECT ISNULL(Company.Company_Jackpot, 0)
	FROM   Company
	       INNER JOIN Sub_Company
	            ON  Company.Company_ID = Sub_Company.Company_ID
	WHERE  Sub_Company.Sub_Company_ID = @SubCompanyID
END

GO

