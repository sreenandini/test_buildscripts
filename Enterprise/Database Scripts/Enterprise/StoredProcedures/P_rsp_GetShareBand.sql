USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetShareBand]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetShareBand]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*    
* Revision History    
*     
* <Name of the owner>         DateCreated       Type(Created/Modified)  Description    
        Kishore S             15-May-2014             Created          This SP is used to Read details  from Share_Band table                                                                       based on Staff_ID.    
*/    
CREATE PROCEDURE [rsp_GetShareBand]
@Share_Schedule_ID int

AS
SET NOCOUNT ON

	
	select Share_Band_ID,Share_Schedule_ID,Share_Band_Name,Share_Band_Start_Date,Share_Band_End_Date,Share_Band_Description,
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
	from share_band
	where Share_Schedule_ID=@Share_Schedule_ID
GO
