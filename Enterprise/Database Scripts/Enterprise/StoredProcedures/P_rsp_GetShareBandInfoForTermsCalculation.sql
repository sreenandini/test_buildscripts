USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_GetShareBandInfoForTermsCalculation]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_GetShareBandInfoForTermsCalculation]
GO

CREATE PROCEDURE [dbo].[rsp_GetShareBandInfoForTermsCalculation] 
(@Share_Band_ID INT)
AS
BEGIN
	SELECT Share_Band_ID,
	       Share_Schedule_ID,
	       Share_Band_Name,
	       Share_Band_Start_Date,
	       Share_Band_End_Date,
	       Share_Band_Description,
	       Share_Band_Supplier_Share,
	       Share_Band_Site_Share,
	       Share_Band_Company_Share,
	       Share_Band_Sec_Company_Share,
	       Share_Band_Future_Supplier_Share,
	       Share_Band_Future_Site_Share,
	       Share_Band_Future_Company_Share,
	       Share_Band_Future_Sec_Company_Share,
	       Share_Band_Future_Start_Date,
	       Share_Band_Past_Supplier_Share,
	       Share_Band_Past_Site_Share,
	       Share_Band_Past_Company_Share,
	       Share_Band_Past_Sec_Company_Share,
	       Share_Band_Past_End_Date,
	       Share_Band_Supplier_Rent,
	       Share_Band_Future_Supplier_Rent,
	       Share_Band_Past_Supplier_Rent,
	       Share_Band_Supplier_Rent_Guaranteed,
	       Share_Band_Future_Supplier_Rent_Guaranteed,
	       Share_Band_Past_Supplier_Rent_Guaranteed,
	       Share_Band_Supplier_Share_Guaranteed,
	       Share_Band_Future_Supplier_Share_Guaranteed,
	       Share_Band_Past_Supplier_Share_Guaranteed,
	       Share_Band_Company_Share_Guaranteed,
	       Share_Band_Future_Company_Share_Guaranteed,
	       Share_Band_Past_Company_Share_Guaranteed,
	       Share_Band_Site_Share_Guaranteed,
	       Share_Band_Future_Site_Share_Guaranteed,
	       Share_Band_Past_Site_Share_Guaranteed,
	       Share_Band_Sec_Company_Share_Guaranteed,
	       Share_Band_Future_Sec_Company_Share_Guaranteed,
	       Share_Band_Past_Sec_Company_Share_Guaranteed
	FROM   Share_Band
	WHERE  Share_Band_ID = @Share_Band_ID
END
GO
