USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetTermsInfoForInstallation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetTermsInfoForInstallation]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER OFF
GO

-------------------------------------------------------------------------- 
---
--- Description: 
---
--- Inputs:      see inputs
---
--- Outputs:     (0)   - no error .. 
---              OTHER - SQL error 
--- 
--- =======================================================================
--- 
--- Revision History
--- 
--- C.Taylor   03 Jul 08     Created 
--------------------------------------------------------------------------- 
CREATE PROCEDURE rsp_GetTermsInfoForInstallation

  @installation_id INT,
  @period int

AS

 -- @period 0,1,2 -- past, current, future

/**
  to use

  print 'past'
  EXEC rsp_GetTermsInfoForInstallation 4207, 0
  print 'current'
  EXEC rsp_GetTermsInfoForInstallation 4207, 1
  print 'future'
  EXEC rsp_GetTermsInfoForInstallation 4207, 2

**/

SELECT TOP 1 Installation.Machine_ID, 
	Installation_Start_Date, 
	Machine.Machine_Class_ID, 
	Bar_Position.Bar_Position_ID, 
	Bar_Position.Bar_Position_Last_Collection_Date, 
	Bar_Position.Bar_Position_Override_Rent, 
	Bar_Position.Bar_Position_Override_Shares, 
	Bar_Position.Bar_Position_Override_Licence, 
	Bar_Position.Bar_Position_Rent, 
	Bar_Position.Bar_Position_Rent_Previous, 
	Bar_Position.Bar_Position_Rent_Future, 
	Bar_Position.Bar_Position_Rent_Past_Date, 
	Bar_Position.Bar_Position_Rent_Future_Date, 
	Bar_Position.Bar_Position_Supplier_Share, 
	Bar_Position.Bar_Position_Site_Share, 
	Bar_Position.Bar_Position_Owners_Share, 
	Bar_Position.Bar_Position_Secondary_Owners_Share, 
	Bar_Position.Bar_Position_Supplier_Share_Previous, 
	Bar_Position.Bar_Position_Site_Share_Previous, 
	Bar_Position.Bar_Position_Owners_Share_Previous, 
	Bar_Position.Bar_Position_Secondary_Owners_Share_Previous, 
	Bar_Position.Bar_Position_Supplier_Share_Future, 
	Bar_Position.Bar_Position_Site_Share_Future, 
	Bar_Position.Bar_Position_Owners_Share_Future, 
	Bar_Position.Bar_Position_Secondary_Owners_Share_Future, 
	Bar_Position.Bar_Position_Share_Past_Date, 
	Bar_Position.Bar_Position_Share_Future_Date, 
	Bar_Position.Bar_Position_Licence_Charge, 
	Bar_Position.Bar_Position_Licence_Previous, 
	Bar_Position.Bar_Position_Licence_Future, 
	Bar_Position.Bar_Position_Licence_Past_Date, 
	Bar_Position.Bar_Position_Licence_Future_Date, 
	Bar_Position_Collection_Rent_Paid_Until, 
	Bar_Position_Collection_Period, Bar_Position_Prize_LOS,
	Terms_Profile.Terms_Profile_ID,
	Terms_Profile.Terms_Group_ID,
	Terms_Profile.Machine_Type_ID,
	Terms_Profile.Terms_Profile_Name,
	Terms_Profile.Terms_Profile_Partners_Supplier_Index,
	Terms_Profile.Terms_Profile_Partners_Supplier_Use,
	Terms_Profile.Terms_Profile_Partners_Supplier_Cash_Destination,
	Terms_Profile.Terms_Profile_Partners_Supplier_Deferred_Remittance,
	Terms_Profile.Terms_Profile_Partners_Supplier_Type,
	Terms_Profile.Terms_Profile_Partners_Supplier_Value,
	Terms_Profile.Terms_Profile_Partners_Supplier_Value_Guaranteed,
	Terms_Profile.Terms_Profile_Partners_Supplier_Share,
	Terms_Profile.Terms_Profile_Partners_Supplier_Share_Guaranteed,
	Terms_Profile.Terms_Profile_Partners_Supplier_Share_Schedule,
	Terms_Profile.Terms_Profile_Partners_Supplier_Rent_Schedule,
	Terms_Profile.Terms_Profile_Partners_Supplier_Guarantor,
	Terms_Profile.Terms_Profile_Partners_Supplier_Guarantor_Percentage,
	Terms_Profile.Terms_Profile_Partners_Site_Index,
	Terms_Profile.Terms_Profile_Partners_Site_Use,
	Terms_Profile.Terms_Profile_Partners_Site_Cash_Destination,
	Terms_Profile.Terms_Profile_Partners_Site_Deferred_Remittance,
	Terms_Profile.Terms_Profile_Partners_Site_Type,
	Terms_Profile.Terms_Profile_Partners_Site_Value,
	Terms_Profile.Terms_Profile_Partners_Site_Value_Guaranteed,
	Terms_Profile.Terms_Profile_Partners_Site_Share,
	Terms_Profile.Terms_Profile_Partners_Site_Share_Guaranteed,
	Terms_Profile.Terms_Profile_Partners_Site_Guarantor,
	Terms_Profile.Terms_Profile_Partners_Site_Guarantor_Percentage,
	Terms_Profile.Terms_Profile_Partners_Group_Index,
	Terms_Profile.Terms_Profile_Partners_Group_Use,
	Terms_Profile.Terms_Profile_Partners_Group_Cash_Destination,
	Terms_Profile.Terms_Profile_Partners_Group_Deferred_Remittance,
	Terms_Profile.Terms_Profile_Partners_Group_Type,
	Terms_Profile.Terms_Profile_Partners_Group_Value,
	Terms_Profile.Terms_Profile_Partners_Group_Value_Guaranteed,
	Terms_Profile.Terms_Profile_Partners_Group_Share,
	Terms_Profile.Terms_Profile_Partners_Group_Share_Guaranteed,
	Terms_Profile.Terms_Profile_Partners_Group_Guarantor,
	Terms_Profile.Terms_Profile_Partners_Group_Guarantor_Percentage,
	Terms_Profile.Terms_Profile_Partners_Sec_Group_Index,
	Terms_Profile.Terms_Profile_Partners_Sec_Group_Use,
	Terms_Profile.Terms_Profile_Partners_Sec_Group_Cash_Destination,
	Terms_Profile.Terms_Profile_Partners_Sec_Group_Deferred_Remittance,
	Terms_Profile.Terms_Profile_Partners_Sec_Group_Type,
	Terms_Profile.Terms_Profile_Partners_Sec_Group_Value,
	Terms_Profile.Terms_Profile_Partners_Sec_Group_Value_Guaranteed,
	Terms_Profile.Terms_Profile_Partners_Sec_Group_Share,
	Terms_Profile.Terms_Profile_Partners_Sec_Group_Share_Guaranteed,
	Terms_Profile.Terms_Profile_Partners_Sec_Group_Guarantor,
	Terms_Profile.Terms_Profile_Partners_Sec_Group_Guarantor_Percentage,
	Terms_Profile.Terms_Profile_VAT_Output_Index,
	Terms_Profile.Terms_Profile_VAT_Output_Use,
	Terms_Profile.Terms_Profile_VAT_Output_Cash_Destination,
	Terms_Profile.Terms_Profile_VAT_Output_Deferred_Remittance,
	Terms_Profile.Terms_Profile_VAT_Supplier_Index,
	Terms_Profile.Terms_Profile_VAT_Supplier_Use,
	Terms_Profile.Terms_Profile_VAT_Supplier_Cash_Destination,
	Terms_Profile.Terms_Profile_VAT_Supplier_Deferred_Remittance,
	Terms_Profile.Terms_Profile_VAT_Supplier_Paid_By,
	Terms_Profile.Terms_Profile_VAT_Supplier_Guarantor,
	Terms_Profile.Terms_Profile_VAT_Site_Index,
	Terms_Profile.Terms_Profile_VAT_Site_Use,
	Terms_Profile.Terms_Profile_VAT_Site_Cash_Destination,
	Terms_Profile.Terms_Profile_VAT_Site_Deferred_Remittance,
	Terms_Profile.Terms_Profile_VAT_Site_Paid_By,
	Terms_Profile.Terms_Profile_VAT_Site_Guarantor,
	Terms_Profile.Terms_Profile_VAT_Group_Index,
	Terms_Profile.Terms_Profile_VAT_Group_Use,
	Terms_Profile.Terms_Profile_VAT_Group_Cash_Destination,
	Terms_Profile.Terms_Profile_VAT_Group_Deferred_Remittance,
	Terms_Profile.Terms_Profile_VAT_Group_Paid_By,
	Terms_Profile.Terms_Profile_VAT_Group_Guarantor,
	Terms_Profile.Terms_Profile_VAT_Sec_Group_Index,
	Terms_Profile.Terms_Profile_VAT_Sec_Group_Use,
	Terms_Profile.Terms_Profile_VAT_Sec_Group_Cash_Destination,
	Terms_Profile.Terms_Profile_VAT_Sec_Group_Deferred_Remittance,
	Terms_Profile.Terms_Profile_VAT_Sec_Group_Paid_By,
	Terms_Profile.Terms_Profile_VAT_Sec_Group_Guarantor,
	Terms_Profile.Terms_Profile_Other_Licence_Index,
	Terms_Profile.Terms_Profile_Other_Licence_Use,
	Terms_Profile.Terms_Profile_Other_Licence_Vat,
	Terms_Profile.Terms_Profile_Other_Licence_Cash_Destination,
	Terms_Profile.Terms_Profile_Other_Licence_Deferred_Remittance,
	Terms_Profile.Terms_Profile_Other_Licence_Charge,
	Terms_Profile.Terms_Profile_Other_Licence_Paid_By,
	Terms_Profile.Terms_Profile_Other_Licence_Guarantor,
	Terms_Profile.Terms_Profile_Other_Licence_Frequency,
	Terms_Profile.Terms_Profile_Other_Prize_Index,
	Terms_Profile.Terms_Profile_Other_Prize_Use,
	Terms_Profile.Terms_Profile_Other_Prize_Vat,
	Terms_Profile.Terms_Profile_Other_Prize_Cash_Destination,
	Terms_Profile.Terms_Profile_Other_Prize_Deferred_Remittance,
	Terms_Profile.Terms_Profile_Other_Prize_Charge,
	Terms_Profile.Terms_Profile_Other_Prize_Paid_By,
	Terms_Profile.Terms_Profile_Other_Prize_Guarantor,
	Terms_Profile.Terms_Profile_Other_Prize_Frequency,
	Terms_Profile.Terms_Profile_Other_Consultancy_Index,
	Terms_Profile.Terms_Profile_Other_Consultancy_Use,
	Terms_Profile.Terms_Profile_Other_Consultancy_Vat,
	Terms_Profile.Terms_Profile_Other_Consultancy_Cash_Destination,
	Terms_Profile.Terms_Profile_Other_Consultancy_Deferred_Remittance,
	Terms_Profile.Terms_Profile_Other_Consultancy_Charge,
	Terms_Profile.Terms_Profile_Other_Consultancy_Paid_By,
	Terms_Profile.Terms_Profile_Other_Consultancy_Guarantor,
	Terms_Profile.Terms_Profile_Other_Consultancy_Frequency,
	Terms_Profile.Terms_Profile_Other_Royalty_Index,
	Terms_Profile.Terms_Profile_Other_Royalty_Use,
	Terms_Profile.Terms_Profile_Other_Royalty_Vat,
	Terms_Profile.Terms_Profile_Other_Royalty_Cash_Destination,
	Terms_Profile.Terms_Profile_Other_Royalty_Deferred_Remittance,
	Terms_Profile.Terms_Profile_Other_Royalty_Charge,
	Terms_Profile.Terms_Profile_Other_Royalty_Paid_By,
	Terms_Profile.Terms_Profile_Other_Royalty_Guarantor,
	Terms_Profile.Terms_Profile_Other_Royalty_Frequency,
	Terms_Profile.Terms_Profile_Other_Other1_Index,
	Terms_Profile.Terms_Profile_Other_Other1_Name,
	Terms_Profile.Terms_Profile_Other_Other1_Use,
	Terms_Profile.Terms_Profile_Other_Other1_Vat,
	Terms_Profile.Terms_Profile_Other_Other1_Cash_Destination,
	Terms_Profile.Terms_Profile_Other_Other1_Deferred_Remittance,
	Terms_Profile.Terms_Profile_Other_Other1_Charge,
	Terms_Profile.Terms_Profile_Other_Other1_Paid_By,
	Terms_Profile.Terms_Profile_Other_Other1_Guarantor,
	Terms_Profile.Terms_Profile_Other_Other1_Frequency,
	Terms_Profile.Terms_Profile_Other_Other2_Index,
	Terms_Profile.Terms_Profile_Other_Other2_Name,
	Terms_Profile.Terms_Profile_Other_Other2_Use,
	Terms_Profile.Terms_Profile_Other_Other2_Vat,
	Terms_Profile.Terms_Profile_Other_Other2_Cash_Destination,
	Terms_Profile.Terms_Profile_Other_Other2_Deferred_Remittance,
	Terms_Profile.Terms_Profile_Other_Other2_Charge,
	Terms_Profile.Terms_Profile_Other_Other2_Paid_By,
	Terms_Profile.Terms_Profile_Other_Other2_Guarantor,
	Terms_Profile.Terms_Profile_Other_Other2_Frequency,
	Terms_Profile.Terms_Profile_London_Rent_Use,
	Terms_Profile.Terms_Profile_London_Rent_Charge,
	Terms_Profile.Terms_Profile_GPT_Index,
	Terms_Profile.Terms_Profile_GPT_Use,
	Terms_Profile.Terms_Profile_GPT_Cash_Destination,
	Terms_Profile.Terms_Profile_GPT_Deferred_Remittance, 
	Sub_Company_Use_Split_Rents, 
	Bar_Position.Bar_Position_Use_Terms, 
	Bar_Position_Override_Rent_Schedule, 
	Bar_Position_Override_Share_Schedule, 
	Bar_Position_Rent_Schedule_ID, 
	Bar_Position_Share_Schedule_ID, 
	Terms_Group_Name, 
	Bar_Position_Override_Rent_From_Schedule_To_Rent, 
	Bar_Position_Override_Rent_From_Rent_To_Schedule, 
	Bar_Position_Override_Rent_From_Schedule_To_Rent_Date, 
	Bar_Position_Override_Rent_From_Rent_To_Schedule_Date, 
	Bar_Position_Rent_Schedule_ID_From 
        
FROM Installation 
JOIN Bar_Position ON Installation.Bar_Position_ID = Bar_Position.Bar_Position_ID
JOIN Site ON Bar_Position.Site_ID = Site.Site_ID
JOIN Sub_Company ON Site.Sub_Company_ID = Sub_Company.Sub_Company_ID
JOIN Terms_Profile ON Bar_Position.Terms_Group_Future_ID = Terms_Profile.Terms_Group_ID
JOIN Terms_Group 
	ON ( ( @period = 2 AND Bar_Position.Terms_Group_Future_ID = Terms_Group.Terms_Group_ID )
	   OR ( @period = 0 AND Bar_Position.Terms_Group_Past_ID = Terms_Group.Terms_Group_ID )
	   OR ( Bar_Position.Terms_Group_ID = Terms_Group.Terms_Group_ID )
           )
JOIN Machine ON Installation.Machine_ID = Machine.Machine_ID
JOIN Machine_Class ON Machine.Machine_Class_ID = Machine_Class.Machine_Class_ID

WHERE Installation.Installation_ID = @installation_id
AND (Terms_Profile.Machine_Type_ID = Machine_Class.Machine_Type_ID OR Terms_Profile.Machine_Type_ID = 0) 

ORDER BY Terms_Profile.Machine_Type_ID ASC

GO

