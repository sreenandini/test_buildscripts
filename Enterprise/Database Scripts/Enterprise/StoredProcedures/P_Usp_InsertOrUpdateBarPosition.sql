USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Usp_InsertOrUpdateBarPosition]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Usp_InsertOrUpdateBarPosition]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [Usp_InsertOrUpdateBarPosition](
	@Bar_Position_ID	int,
@Site_ID	int,
@Zone_ID	int,
@Access_Key_ID	int,
@Access_Key_ID_Default	bit,
@Terms_Group_ID	int,
@Terms_Group_Changeover_Date	varchar	(30),
@Terms_Group_Future_ID	int,
@Terms_Group_Past_Changeover_Date	varchar	(30),
@Terms_Group_Past_ID	int,
@Terms_Group_ID_Default	bit,
@Duty_ID	int,
@Depot_ID	int,
@Machine_Type_ID	int,
@Bar_Position_Name	VARCHAR	(50),
@Bar_Position_Location	varchar	(50),
@Bar_Position_Start_Date	varchar	(30),
@Bar_Position_End_Date	varchar	(30),
@Bar_Position_Collection_Day	varchar	(30),
@Bar_Position_Price_Per_Play	varchar	(50),
@Bar_Position_Price_Per_Play_Default	bit,
@Bar_Position_Jackpot	varchar	(50),
@Bar_Position_Jackpot_Default	bit,
@Bar_Position_Percentage_Payout	varchar	(50),
@Bar_Position_Percentage_Payout_Default	bit,
@Bar_Position_Last_Collection_Date	varchar	(30),
@Bar_Position_Collection_Rent_Paid_Until	varchar	(30),
@Bar_Position_Collection_Period	int,
@Bar_Position_Supplier_AMEDIS_Code	varchar	(4),
@Bar_Position_Supplier_Depot_AMEDIS_Code	varchar	(4),
@Bar_Position_Supplier_Site_Code	varchar	(8),
@Bar_Position_Supplier_Position_Code	varchar	(6),
@Bar_Position_Supplier_Area	varchar	(50),
@Bar_Position_Supplier_Service_Area	varchar	(50),
@Bar_Position_Company_Position_Code	varchar	(6),
@Bar_Position_Company_Target	real,
@Bar_Position_Collection_Frequency	int,
@Bar_Position_Image_Reference	varchar	(50),
@Bar_Position_Machine_Type_AMEDIS_Code	int,
@Bar_Position_Rent	real,
@Bar_Position_Rent_Previous	real,
@Bar_Position_Rent_Future	real,
@Bar_Position_Rent_Past_Date	varchar	(30),
@Bar_Position_Rent_Future_Date	varchar	(30),
@Bar_Position_Supplier_Share	real,
@Bar_Position_Site_Share	real,
@Bar_Position_Owners_Share	real,
@Bar_Position_Secondary_Owners_Share	real,
@Bar_Position_Supplier_Share_Previous	real,
@Bar_Position_Site_Share_Previous	real,
@Bar_Position_Owners_Share_Previous	real,
@Bar_Position_Secondary_Owners_Share_Previous	real,
@Bar_Position_Supplier_Share_Future	real,
@Bar_Position_Site_Share_Future	real,
@Bar_Position_Owners_Share_Future	real,
@Bar_Position_Secondary_Owners_Share_Future	real,
@Bar_Position_Share_Past_Date	varchar	(30),
@Bar_Position_Share_Future_Date	varchar	(30),
@Bar_Position_Licence_Charge	real,
@Bar_Position_Licence_Previous	real,
@Bar_Position_Licence_Future	real,
@Bar_Position_Licence_Past_Date	varchar	(30),
@Bar_Position_Licence_Future_Date	varchar	(30),
@Bar_Position_Use_Terms	bit,
@Bar_Position_TX_Collection	bit,
@Bar_Position_TX_Collection_Use_Default	bit,
@Bar_Position_TX_Movement	bit,
@Bar_Position_TX_Movement_Use_Default	bit,
@Bar_Position_TX_EDC	bit,
@Bar_Position_TX_EDC_Use_Detault	bit,
@Bar_Position_TX_Format	int,
@Bar_Position_TX_Format_Use_Default	bit,
@Bar_Position_RX_Collection	bit,
@Bar_Position_RX_Collection_Use_Default	bit,
@Bar_Position_RX_Movement	bit,
@Bar_Position_RX_Movement_Use_Default	bit,
@Bar_Position_RX_EDC	bit,
@Bar_Position_RX_EDC_Use_Detault	bit,
@Bar_Position_RX_Format	int,
@Bar_Position_RX_Format_Use_Default	bit,
@Bar_Position_Net_Target	real,
@Bar_Position_Below_Net_Target_Counter	int,
@Bar_Position_Below_Company_Target_Counter	int,
@Bar_Position_Security_Required	bit,
@Bar_Position_Site_Has_Cashbox_Keys	bit,
@Bar_Position_Site_Has_FreePlay_Access	bit,
@Bar_Position_Override_Rent	bit,
@Bar_Position_Override_Shares	bit,
@Bar_Position_Override_Licence	bit,
@Bar_Position_Category	int,
@Bar_Position_PPL_Charge	real,
@Bar_Position_PPL_Previous	real,
@Bar_Position_PPL_Future	real,
@Bar_Position_PPL_Past_Date	varchar(30),
@Bar_Position_PPL_Future_Date	varchar	(30),
@Bar_Position_Float_Issued	int,
@Bar_Position_Float_Recovered	int,
@Bar_Position_Use_Site_Share_For_Secondary_Brewery	bit,
@Bar_Position_Prize_LOS	bit,
@Bar_Position_Rent_Schedule_ID	INT,
@Bar_Position_IsEnable bit
)
AS
BEGIN
	IF EXISTS ( SELECT 1 FROM Bar_Position bp WHERE Bar_Position_ID = @Bar_Position_ID)
	BEGIN
		UPDATE Bar_Position
		SET
		Site_ID = @Site_ID,
Zone_ID = @Zone_ID,
Access_Key_ID = @Access_Key_ID,
Access_Key_ID_Default = @Access_Key_ID_Default,
Terms_Group_ID = @Terms_Group_ID,
Terms_Group_Changeover_Date = @Terms_Group_Changeover_Date,
Terms_Group_Future_ID = @Terms_Group_Future_ID,
Terms_Group_Past_Changeover_Date = @Terms_Group_Past_Changeover_Date,
Terms_Group_Past_ID = @Terms_Group_Past_ID,
Terms_Group_ID_Default = @Terms_Group_ID_Default,
Duty_ID = @Duty_ID,
Depot_ID = @Depot_ID,
Machine_Type_ID = @Machine_Type_ID,
Bar_Position_Name = @Bar_Position_Name,
Bar_Position_Location = @Bar_Position_Location,
Bar_Position_Start_Date = @Bar_Position_Start_Date,
Bar_Position_End_Date = @Bar_Position_End_Date,
Bar_Position_Collection_Day = @Bar_Position_Collection_Day,
Bar_Position_Price_Per_Play = @Bar_Position_Price_Per_Play,
Bar_Position_Price_Per_Play_Default = @Bar_Position_Price_Per_Play_Default,
Bar_Position_Jackpot = @Bar_Position_Jackpot,
Bar_Position_Jackpot_Default = @Bar_Position_Jackpot_Default,
Bar_Position_Percentage_Payout = @Bar_Position_Percentage_Payout,
Bar_Position_Percentage_Payout_Default = @Bar_Position_Percentage_Payout_Default,
Bar_Position_Last_Collection_Date = @Bar_Position_Last_Collection_Date,
Bar_Position_Collection_Rent_Paid_Until = @Bar_Position_Collection_Rent_Paid_Until,
Bar_Position_Collection_Period = @Bar_Position_Collection_Period,
Bar_Position_Supplier_AMEDIS_Code = @Bar_Position_Supplier_AMEDIS_Code,
Bar_Position_Supplier_Depot_AMEDIS_Code = @Bar_Position_Supplier_Depot_AMEDIS_Code,
Bar_Position_Supplier_Site_Code = @Bar_Position_Supplier_Site_Code,
Bar_Position_Supplier_Position_Code = @Bar_Position_Supplier_Position_Code,
Bar_Position_Supplier_Area = @Bar_Position_Supplier_Area,
Bar_Position_Supplier_Service_Area = @Bar_Position_Supplier_Service_Area,
Bar_Position_Company_Position_Code = @Bar_Position_Company_Position_Code,
Bar_Position_Company_Target = @Bar_Position_Company_Target,
Bar_Position_Collection_Frequency = @Bar_Position_Collection_Frequency,
Bar_Position_Image_Reference = @Bar_Position_Image_Reference,
Bar_Position_Machine_Type_AMEDIS_Code = @Bar_Position_Machine_Type_AMEDIS_Code,
Bar_Position_Rent = @Bar_Position_Rent,
Bar_Position_Rent_Previous = @Bar_Position_Rent_Previous,
Bar_Position_Rent_Future = @Bar_Position_Rent_Future,
Bar_Position_Rent_Past_Date = @Bar_Position_Rent_Past_Date,
Bar_Position_Rent_Future_Date = @Bar_Position_Rent_Future_Date,
Bar_Position_Supplier_Share = @Bar_Position_Supplier_Share,
Bar_Position_Site_Share = @Bar_Position_Site_Share,
Bar_Position_Owners_Share = @Bar_Position_Owners_Share,
Bar_Position_Secondary_Owners_Share = @Bar_Position_Secondary_Owners_Share,
Bar_Position_Supplier_Share_Previous = @Bar_Position_Supplier_Share_Previous,
Bar_Position_Site_Share_Previous = @Bar_Position_Site_Share_Previous,
Bar_Position_Owners_Share_Previous = @Bar_Position_Owners_Share_Previous,
Bar_Position_Secondary_Owners_Share_Previous = @Bar_Position_Secondary_Owners_Share_Previous,
Bar_Position_Supplier_Share_Future = @Bar_Position_Supplier_Share_Future,
Bar_Position_Site_Share_Future = @Bar_Position_Site_Share_Future,
Bar_Position_Owners_Share_Future = @Bar_Position_Owners_Share_Future,
Bar_Position_Secondary_Owners_Share_Future = @Bar_Position_Secondary_Owners_Share_Future,
Bar_Position_Share_Past_Date = @Bar_Position_Share_Past_Date,
Bar_Position_Share_Future_Date = @Bar_Position_Share_Future_Date,
Bar_Position_Licence_Charge = @Bar_Position_Licence_Charge,
Bar_Position_Licence_Previous = @Bar_Position_Licence_Previous,
Bar_Position_Licence_Future = @Bar_Position_Licence_Future,
Bar_Position_Licence_Past_Date = @Bar_Position_Licence_Past_Date,
Bar_Position_Licence_Future_Date = @Bar_Position_Licence_Future_Date,
Bar_Position_Use_Terms = @Bar_Position_Use_Terms,
Bar_Position_TX_Collection = @Bar_Position_TX_Collection,
Bar_Position_TX_Collection_Use_Default = @Bar_Position_TX_Collection_Use_Default,
Bar_Position_TX_Movement = @Bar_Position_TX_Movement,
Bar_Position_TX_Movement_Use_Default = @Bar_Position_TX_Movement_Use_Default,
Bar_Position_TX_EDC = @Bar_Position_TX_EDC,
Bar_Position_TX_EDC_Use_Detault = @Bar_Position_TX_EDC_Use_Detault,
Bar_Position_TX_Format = @Bar_Position_TX_Format,
Bar_Position_TX_Format_Use_Default = @Bar_Position_TX_Format_Use_Default,
Bar_Position_RX_Collection = @Bar_Position_RX_Collection,
Bar_Position_RX_Collection_Use_Default = @Bar_Position_RX_Collection_Use_Default,
Bar_Position_RX_Movement = @Bar_Position_RX_Movement,
Bar_Position_RX_Movement_Use_Default = @Bar_Position_RX_Movement_Use_Default,
Bar_Position_RX_EDC = @Bar_Position_RX_EDC,
Bar_Position_RX_EDC_Use_Detault = @Bar_Position_RX_EDC_Use_Detault,
Bar_Position_RX_Format = @Bar_Position_RX_Format,
Bar_Position_RX_Format_Use_Default = @Bar_Position_RX_Format_Use_Default,
Bar_Position_Net_Target = @Bar_Position_Net_Target,
Bar_Position_Below_Net_Target_Counter = @Bar_Position_Below_Net_Target_Counter,
Bar_Position_Below_Company_Target_Counter = @Bar_Position_Below_Company_Target_Counter,
Bar_Position_Security_Required = @Bar_Position_Security_Required,
Bar_Position_Site_Has_Cashbox_Keys = @Bar_Position_Site_Has_Cashbox_Keys,
Bar_Position_Site_Has_FreePlay_Access = @Bar_Position_Site_Has_FreePlay_Access,
Bar_Position_Override_Rent = @Bar_Position_Override_Rent,
Bar_Position_Override_Shares = @Bar_Position_Override_Shares,
Bar_Position_Override_Licence = @Bar_Position_Override_Licence,
Bar_Position_Category = @Bar_Position_Category,
Bar_Position_PPL_Charge = @Bar_Position_PPL_Charge,
Bar_Position_PPL_Previous = @Bar_Position_PPL_Previous,
Bar_Position_PPL_Future = @Bar_Position_PPL_Future,
Bar_Position_PPL_Past_Date = @Bar_Position_PPL_Past_Date,
Bar_Position_PPL_Future_Date = @Bar_Position_PPL_Future_Date,
Bar_Position_Float_Issued = @Bar_Position_Float_Issued,
Bar_Position_Float_Recovered = @Bar_Position_Float_Recovered,
Bar_Position_Use_Site_Share_For_Secondary_Brewery = @Bar_Position_Use_Site_Share_For_Secondary_Brewery,
Bar_Position_Prize_LOS = @Bar_Position_Prize_LOS,
Bar_Position_Rent_Schedule_ID = @Bar_Position_Rent_Schedule_ID,
Bar_Position_IsEnable = @Bar_Position_IsEnable
WHERE Bar_Position_ID = @Bar_Position_ID	
	END
	ELSE
	BEGIN
			INSERT INTO Bar_Position
			(
				Site_ID,Zone_ID,Access_Key_ID,Access_Key_ID_Default,Terms_Group_ID,Terms_Group_Changeover_Date,Terms_Group_Future_ID,Terms_Group_Past_Changeover_Date,Terms_Group_Past_ID,Terms_Group_ID_Default,Duty_ID,Depot_ID,Machine_Type_ID,Bar_Position_Name,Bar_Position_Location,Bar_Position_Start_Date,Bar_Position_End_Date,Bar_Position_Collection_Day,Bar_Position_Price_Per_Play,Bar_Position_Price_Per_Play_Default,Bar_Position_Jackpot,Bar_Position_Jackpot_Default,Bar_Position_Percentage_Payout,Bar_Position_Percentage_Payout_Default,Bar_Position_Last_Collection_Date,Bar_Position_Collection_Rent_Paid_Until,Bar_Position_Collection_Period,Bar_Position_Supplier_AMEDIS_Code,Bar_Position_Supplier_Depot_AMEDIS_Code,Bar_Position_Supplier_Site_Code,Bar_Position_Supplier_Position_Code,Bar_Position_Supplier_Area,Bar_Position_Supplier_Service_Area,Bar_Position_Company_Position_Code,Bar_Position_Company_Target,Bar_Position_Collection_Frequency,Bar_Position_Image_Reference,Bar_Position_Machine_Type_AMEDIS_Code,Bar_Position_Rent,Bar_Position_Rent_Previous,Bar_Position_Rent_Future,Bar_Position_Rent_Past_Date,Bar_Position_Rent_Future_Date,Bar_Position_Supplier_Share,Bar_Position_Site_Share,Bar_Position_Owners_Share,Bar_Position_Secondary_Owners_Share,Bar_Position_Supplier_Share_Previous,Bar_Position_Site_Share_Previous,Bar_Position_Owners_Share_Previous,Bar_Position_Secondary_Owners_Share_Previous,Bar_Position_Supplier_Share_Future,Bar_Position_Site_Share_Future,Bar_Position_Owners_Share_Future,Bar_Position_Secondary_Owners_Share_Future,Bar_Position_Share_Past_Date,Bar_Position_Share_Future_Date,Bar_Position_Licence_Charge,Bar_Position_Licence_Previous,Bar_Position_Licence_Future,Bar_Position_Licence_Past_Date,Bar_Position_Licence_Future_Date,Bar_Position_Use_Terms,Bar_Position_TX_Collection,Bar_Position_TX_Collection_Use_Default,Bar_Position_TX_Movement,Bar_Position_TX_Movement_Use_Default,Bar_Position_TX_EDC,Bar_Position_TX_EDC_Use_Detault,Bar_Position_TX_Format,Bar_Position_TX_Format_Use_Default,Bar_Position_RX_Collection,Bar_Position_RX_Collection_Use_Default,Bar_Position_RX_Movement,Bar_Position_RX_Movement_Use_Default,Bar_Position_RX_EDC,Bar_Position_RX_EDC_Use_Detault,Bar_Position_RX_Format,Bar_Position_RX_Format_Use_Default,Bar_Position_Net_Target,Bar_Position_Below_Net_Target_Counter,Bar_Position_Below_Company_Target_Counter,Bar_Position_Security_Required,Bar_Position_Site_Has_Cashbox_Keys,Bar_Position_Site_Has_FreePlay_Access,Bar_Position_Override_Rent,Bar_Position_Override_Shares,Bar_Position_Override_Licence,Bar_Position_Category,Bar_Position_PPL_Charge,Bar_Position_PPL_Previous,Bar_Position_PPL_Future,Bar_Position_PPL_Past_Date,Bar_Position_PPL_Future_Date,Bar_Position_Float_Issued,Bar_Position_Float_Recovered,Bar_Position_Use_Site_Share_For_Secondary_Brewery,Bar_Position_Prize_LOS,Bar_Position_Rent_Schedule_ID,Bar_Position_IsEnable
			)
			VALUES
			(
				@Site_ID,@Zone_ID,@Access_Key_ID,@Access_Key_ID_Default,@Terms_Group_ID,@Terms_Group_Changeover_Date,@Terms_Group_Future_ID,@Terms_Group_Past_Changeover_Date,@Terms_Group_Past_ID,@Terms_Group_ID_Default,@Duty_ID,@Depot_ID,@Machine_Type_ID,@Bar_Position_Name,@Bar_Position_Location,@Bar_Position_Start_Date,@Bar_Position_End_Date,@Bar_Position_Collection_Day,@Bar_Position_Price_Per_Play,@Bar_Position_Price_Per_Play_Default,@Bar_Position_Jackpot,@Bar_Position_Jackpot_Default,@Bar_Position_Percentage_Payout,@Bar_Position_Percentage_Payout_Default,@Bar_Position_Last_Collection_Date,@Bar_Position_Collection_Rent_Paid_Until,@Bar_Position_Collection_Period,@Bar_Position_Supplier_AMEDIS_Code,@Bar_Position_Supplier_Depot_AMEDIS_Code,@Bar_Position_Supplier_Site_Code,@Bar_Position_Supplier_Position_Code,@Bar_Position_Supplier_Area,@Bar_Position_Supplier_Service_Area,@Bar_Position_Company_Position_Code,@Bar_Position_Company_Target,@Bar_Position_Collection_Frequency,@Bar_Position_Image_Reference,@Bar_Position_Machine_Type_AMEDIS_Code,@Bar_Position_Rent,@Bar_Position_Rent_Previous,@Bar_Position_Rent_Future,@Bar_Position_Rent_Past_Date,@Bar_Position_Rent_Future_Date,@Bar_Position_Supplier_Share,@Bar_Position_Site_Share,@Bar_Position_Owners_Share,@Bar_Position_Secondary_Owners_Share,@Bar_Position_Supplier_Share_Previous,@Bar_Position_Site_Share_Previous,@Bar_Position_Owners_Share_Previous,@Bar_Position_Secondary_Owners_Share_Previous,@Bar_Position_Supplier_Share_Future,@Bar_Position_Site_Share_Future,@Bar_Position_Owners_Share_Future,@Bar_Position_Secondary_Owners_Share_Future,@Bar_Position_Share_Past_Date,@Bar_Position_Share_Future_Date,@Bar_Position_Licence_Charge,@Bar_Position_Licence_Previous,@Bar_Position_Licence_Future,@Bar_Position_Licence_Past_Date,@Bar_Position_Licence_Future_Date,@Bar_Position_Use_Terms,@Bar_Position_TX_Collection,@Bar_Position_TX_Collection_Use_Default,@Bar_Position_TX_Movement,@Bar_Position_TX_Movement_Use_Default,@Bar_Position_TX_EDC,@Bar_Position_TX_EDC_Use_Detault,@Bar_Position_TX_Format,@Bar_Position_TX_Format_Use_Default,@Bar_Position_RX_Collection,@Bar_Position_RX_Collection_Use_Default,@Bar_Position_RX_Movement,@Bar_Position_RX_Movement_Use_Default,@Bar_Position_RX_EDC,@Bar_Position_RX_EDC_Use_Detault,@Bar_Position_RX_Format,@Bar_Position_RX_Format_Use_Default,@Bar_Position_Net_Target,@Bar_Position_Below_Net_Target_Counter,@Bar_Position_Below_Company_Target_Counter,@Bar_Position_Security_Required,@Bar_Position_Site_Has_Cashbox_Keys,@Bar_Position_Site_Has_FreePlay_Access,@Bar_Position_Override_Rent,@Bar_Position_Override_Shares,@Bar_Position_Override_Licence,@Bar_Position_Category,@Bar_Position_PPL_Charge,@Bar_Position_PPL_Previous,@Bar_Position_PPL_Future,@Bar_Position_PPL_Past_Date,@Bar_Position_PPL_Future_Date,@Bar_Position_Float_Issued,@Bar_Position_Float_Recovered,@Bar_Position_Use_Site_Share_For_Secondary_Brewery,@Bar_Position_Prize_LOS,@Bar_Position_Rent_Schedule_ID,@Bar_Position_IsEnable
			)
	END
END
GO