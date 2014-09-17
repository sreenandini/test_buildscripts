USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_InsertOrUpdateShareBand]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_InsertOrUpdateShareBand]
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
        Kishore S             10-June-2014             Created          This SP is used to insert/update  Share_Band table                                                                       based on Staff_ID.    
*/    
CREATE PROCEDURE usp_InsertOrUpdateShareBand
				@Share_Schedule_Name varchar(50),
				@Share_Band_ID int,
				@Share_Schedule_ID int,
				@Share_Band_Name varchar(50),
				@Share_Band_Start_Date varchar(30),
				@Share_Band_End_Date varchar(30),
				@Share_Band_Description varchar(50),
				@Share_Band_Supplier_Share real,
				@Share_Band_Site_Share real,
				@Share_Band_Company_Share real,
				@Share_Band_Sec_Company_Share real,
				@Share_Band_Future_Supplier_Share real,
				@Share_Band_Future_Site_Share real,
				@Share_Band_Future_Company_Share real,
				@Share_Band_Future_Sec_Company_Share real,
				@Share_Band_Future_Start_Date varchar(30),
				@Share_Band_Past_Supplier_Share real,
				@Share_Band_Past_Site_Share real,
				@Share_Band_Past_Company_Share real,
				@Share_Band_Past_Sec_Company_Share real,
				@Share_Band_Past_End_Date varchar(30),
				@Share_Band_Supplier_Rent real,
				@Share_Band_Future_Supplier_Rent real,
				@Share_Band_Past_Supplier_Rent real,
				@Share_Band_Supplier_Rent_Guaranteed bit,
				@Share_Band_Future_Supplier_Rent_Guaranteed bit,
				@Share_Band_Past_Supplier_Rent_Guaranteed bit,
				@Share_Band_Supplier_Share_Guaranteed bit,
				@Share_Band_Future_Supplier_Share_Guaranteed bit,
				@Share_Band_Past_Supplier_Share_Guaranteed bit,
				@Share_Band_Company_Share_Guaranteed bit,
				@Share_Band_Future_Company_Share_Guaranteed bit,
				@Share_Band_Past_Company_Share_Guaranteed bit,
				@Share_Band_Site_Share_Guaranteed bit,
				@Share_Band_Future_Site_Share_Guaranteed bit,
				@Share_Band_Past_Site_Share_Guaranteed bit,
				@Share_Band_Sec_Company_Share_Guaranteed bit,
				@Share_Band_Future_Sec_Company_Share_Guaranteed bit,
				@Share_Band_Past_Sec_Company_Share_Guaranteed bit
AS

	IF NOT EXISTS ( SELECT 1 FROM [dbo].[Share_Band] sb WHERE (sb.Share_Band_ID=@Share_Band_ID))
	BEGIN
			INSERT INTO [dbo].[Share_Band]
	        (
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
)
	        VALUES (
@Share_Schedule_ID,
@Share_Band_Name,
@Share_Band_Start_Date,
@Share_Band_End_Date,
@Share_Band_Description,
@Share_Band_Supplier_Share,
@Share_Band_Site_Share,
@Share_Band_Company_Share,
@Share_Band_Sec_Company_Share,
@Share_Band_Future_Supplier_Share,
@Share_Band_Future_Site_Share,
@Share_Band_Future_Company_Share,
@Share_Band_Future_Sec_Company_Share,
CONVERT(VARCHAR(14),CAST(@Share_Band_Future_Start_Date AS DATETIME), 106),
@Share_Band_Past_Supplier_Share,
@Share_Band_Past_Site_Share,
@Share_Band_Past_Company_Share,
@Share_Band_Past_Sec_Company_Share,
CONVERT(VARCHAR(14),CAST(@Share_Band_Past_End_Date AS DATETIME) , 106),
@Share_Band_Supplier_Rent,
@Share_Band_Future_Supplier_Rent,
@Share_Band_Past_Supplier_Rent,
@Share_Band_Supplier_Rent_Guaranteed,
@Share_Band_Future_Supplier_Rent_Guaranteed,
@Share_Band_Past_Supplier_Rent_Guaranteed,
@Share_Band_Supplier_Share_Guaranteed,
@Share_Band_Future_Supplier_Share_Guaranteed,
@Share_Band_Past_Supplier_Share_Guaranteed,
@Share_Band_Company_Share_Guaranteed,
@Share_Band_Future_Company_Share_Guaranteed,
@Share_Band_Past_Company_Share_Guaranteed,
@Share_Band_Site_Share_Guaranteed,
@Share_Band_Future_Site_Share_Guaranteed,
@Share_Band_Past_Site_Share_Guaranteed,
@Share_Band_Sec_Company_Share_Guaranteed,
@Share_Band_Future_Sec_Company_Share_Guaranteed,
@Share_Band_Past_Sec_Company_Share_Guaranteed
)
	 END   
	 ELSE
	 BEGIN
	        UPDATE [dbo].[Share_Band]
	        SET  
 Share_Schedule_ID=@Share_Schedule_ID,
 Share_Band_Name=@Share_Band_Name,
 Share_Band_Start_Date=@Share_Band_Start_Date,
 Share_Band_End_Date=@Share_Band_End_Date,
 Share_Band_Description  =@Share_Band_Description,
 Share_Band_Supplier_Share  =@Share_Band_Supplier_Share,
 Share_Band_Site_Share  =@Share_Band_Site_Share,
 Share_Band_Company_Share  =@Share_Band_Company_Share,
 Share_Band_Sec_Company_Share  =@Share_Band_Sec_Company_Share,
 Share_Band_Future_Supplier_Share  =@Share_Band_Future_Supplier_Share,
 Share_Band_Future_Site_Share  =@Share_Band_Future_Site_Share,
 Share_Band_Future_Company_Share  =@Share_Band_Future_Company_Share,
 Share_Band_Future_Sec_Company_Share  =@Share_Band_Future_Sec_Company_Share,
 Share_Band_Future_Start_Date  =CONVERT(VARCHAR(14),CAST(@Share_Band_Future_Start_Date AS DATETIME), 106),
 Share_Band_Past_Supplier_Share  =@Share_Band_Past_Supplier_Share,
 Share_Band_Past_Site_Share  =@Share_Band_Past_Site_Share,
 Share_Band_Past_Company_Share  =@Share_Band_Past_Company_Share,
 Share_Band_Past_Sec_Company_Share  =@Share_Band_Past_Sec_Company_Share,
 Share_Band_Past_End_Date  =CONVERT(VARCHAR(14),CAST(@Share_Band_Past_End_Date AS DATETIME) , 106),
 Share_Band_Supplier_Rent  =@Share_Band_Supplier_Rent,
 Share_Band_Future_Supplier_Rent  =@Share_Band_Future_Supplier_Rent,
 Share_Band_Past_Supplier_Rent  =@Share_Band_Past_Supplier_Rent,
 Share_Band_Supplier_Rent_Guaranteed  =@Share_Band_Supplier_Rent_Guaranteed,
 Share_Band_Future_Supplier_Rent_Guaranteed  =@Share_Band_Future_Supplier_Rent_Guaranteed,
 Share_Band_Past_Supplier_Rent_Guaranteed  =@Share_Band_Past_Supplier_Rent_Guaranteed,
 Share_Band_Supplier_Share_Guaranteed  =@Share_Band_Supplier_Share_Guaranteed,
 Share_Band_Future_Supplier_Share_Guaranteed  =@Share_Band_Future_Supplier_Share_Guaranteed,
 Share_Band_Past_Supplier_Share_Guaranteed  =@Share_Band_Past_Supplier_Share_Guaranteed,
 Share_Band_Company_Share_Guaranteed  =@Share_Band_Company_Share_Guaranteed,
 Share_Band_Future_Company_Share_Guaranteed  =@Share_Band_Future_Company_Share_Guaranteed,
 Share_Band_Past_Company_Share_Guaranteed  =@Share_Band_Past_Company_Share_Guaranteed,
 Share_Band_Site_Share_Guaranteed  =@Share_Band_Site_Share_Guaranteed,
 Share_Band_Future_Site_Share_Guaranteed  =@Share_Band_Future_Site_Share_Guaranteed,
 Share_Band_Past_Site_Share_Guaranteed  =@Share_Band_Past_Site_Share_Guaranteed,
 Share_Band_Sec_Company_Share_Guaranteed  =@Share_Band_Sec_Company_Share_Guaranteed,
 Share_Band_Future_Sec_Company_Share_Guaranteed  =@Share_Band_Future_Sec_Company_Share_Guaranteed,
 Share_Band_Past_Sec_Company_Share_Guaranteed  =@Share_Band_Past_Sec_Company_Share_Guaranteed

	        WHERE  Share_Band_ID=@Share_Band_ID
	 END
GO
