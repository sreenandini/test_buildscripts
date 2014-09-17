/************************************************************
 * Code formatted by SoftTree SQL Assistant © v4.8.29
 * Time: 21/06/2013 10:07:05 AM
 ************************************************************/

USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_GetBarPositionDetailsById]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_GetBarPositionDetailsById]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE rsp_GetBarPositionDetailsById
	@BarPositionID
AS
	INT
	AS
BEGIN
	SELECT Bar_Position_ID,
	       Site_ID,
	       Zone_ID,
	       Access_Key_ID,
	       Access_Key_ID_Default,
	       Terms_Group_ID,
	       Terms_Group_Changeover_Date,
	       Terms_Group_Future_ID,
	       Terms_Group_Past_Changeover_Date,
	       Terms_Group_Past_ID,
	       Terms_Group_ID_Default,
	       Duty_ID,
	       Depot_ID,
	       Machine_Type_ID,
	       Bar_Position_Name,
	       Bar_Position_Location,
	       Bar_Position_Start_Date,
	       Bar_Position_End_Date,
	       Bar_Position_Collection_Day,
	       Bar_Position_Price_Per_Play,
	       Bar_Position_Price_Per_Play_Default,
	       Bar_Position_Jackpot,
	       Bar_Position_Jackpot_Default,
	       Bar_Position_Percentage_Payout,
	       Bar_Position_Percentage_Payout_Default,
	       Bar_Position_Last_Collection_Date,
	       Bar_Position_Collection_Rent_Paid_Until,
	       Bar_Position_Collection_Period,
	       Bar_Position_Supplier_AMEDIS_Code,
	       Bar_Position_Supplier_Depot_AMEDIS_Code,
	       Bar_Position_Supplier_Site_Code,
	       Bar_Position_Supplier_Position_Code,
	       Bar_Position_Supplier_Area,
	       Bar_Position_Supplier_Service_Area,
	       Bar_Position_Company_Position_Code,
	       Bar_Position_Company_Target,
	       Bar_Position_Collection_Frequency,
	       Bar_Position_Image_Reference,
	       Bar_Position_Machine_Type_AMEDIS_Code,
	       Bar_Position_Rent,
	       Bar_Position_Rent_Previous,
	       Bar_Position_Rent_Future,
	       Bar_Position_Rent_Past_Date,
	       Bar_Position_Rent_Future_Date,
	       Bar_Position_Supplier_Share,
	       Bar_Position_Site_Share,
	       Bar_Position_Owners_Share,
	       Bar_Position_Secondary_Owners_Share,
	       Bar_Position_Supplier_Share_Previous,
	       Bar_Position_Site_Share_Previous,
	       Bar_Position_Owners_Share_Previous,
	       Bar_Position_Secondary_Owners_Share_Previous,
	       Bar_Position_Supplier_Share_Future,
	       Bar_Position_Site_Share_Future,
	       Bar_Position_Owners_Share_Future,
	       Bar_Position_Secondary_Owners_Share_Future,
	       Bar_Position_Share_Past_Date,
	       Bar_Position_Share_Future_Date,
	       Bar_Position_Licence_Charge,
	       Bar_Position_Licence_Previous,
	       Bar_Position_Licence_Future,
	       Bar_Position_Licence_Past_Date,
	       Bar_Position_Licence_Future_Date,
	       Bar_Position_Use_Terms,
	       Bar_Position_TX_Collection,
	       Bar_Position_TX_Collection_Use_Default,
	       Bar_Position_TX_Movement,
	       Bar_Position_TX_Movement_Use_Default,
	       Bar_Position_TX_EDC,
	       Bar_Position_TX_EDC_Use_Detault,
	       Bar_Position_TX_Format,
	       Bar_Position_TX_Format_Use_Default,
	       Bar_Position_RX_Collection,
	       Bar_Position_RX_Collection_Use_Default,
	       Bar_Position_RX_Movement,
	       Bar_Position_RX_Movement_Use_Default,
	       Bar_Position_RX_EDC,
	       Bar_Position_RX_EDC_Use_Detault,
	       Bar_Position_RX_Format,
	       Bar_Position_RX_Format_Use_Default,
	       Bar_Position_Net_Target,
	       Bar_Position_Below_Net_Target_Counter,
	       Bar_Position_Below_Company_Target_Counter,
	       Bar_Position_Security_Required,
	       Bar_Position_Site_Has_Cashbox_Keys,
	       Bar_Position_Site_Has_FreePlay_Access,
	       Bar_Position_Override_Rent,
	       Bar_Position_Override_Shares,
	       Bar_Position_Override_Licence,
	       Bar_Position_Category,
	       Bar_Position_PPL_Charge,
	       Bar_Position_PPL_Previous,
	       Bar_Position_PPL_Future,
	       Bar_Position_PPL_Past_Date,
	       Bar_Position_PPL_Future_Date,
	       Bar_Position_Float_Issued,
	       Bar_Position_Float_Recovered,
	       Bar_Position_Use_Site_Share_For_Secondary_Brewery,
	       Bar_Position_Prize_LOS,
	       Bar_Position_Rent_Schedule_ID,
	       Bar_Position_Share_Schedule_ID,
	       Bar_Position_Override_Rent_Schedule,
	       Bar_Position_Override_Share_Schedule,
	       Bar_Position_Last_Collection_ID,
	       Bar_Position_Override_Rent_From_Schedule_To_Rent,
	       Bar_Position_Override_Rent_From_Rent_To_Schedule,
	       Bar_Position_Override_Rent_From_Schedule_To_Rent_Date,
	       Bar_Position_Override_Rent_From_Rent_To_Schedule_Date,
	       Bar_Position_Rent_Schedule_ID_From,
	       Bar_Position_Disable_EDI_Export,
	       Bar_Position_Invoice_Period,
	       Bar_Position_Machine_Enabled,
	       Bar_Position_Note_Acceptor_Enabled,
	       Bar_Position_Machine_Enabled_Date,
	       Bar_Position_IsEnable
	FROM   Bar_Position
	WHERE  Bar_Position_ID = @BarPositionID
END
GO

