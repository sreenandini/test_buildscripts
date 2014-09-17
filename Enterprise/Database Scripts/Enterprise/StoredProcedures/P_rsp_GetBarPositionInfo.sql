USE [Enterprise]
GO

/****** Object:  StoredProcedure [dbo].[rsp_GetBarPositionInfo]    Script Date: 06/14/2013 11:50:08 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetBarPositionInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetBarPositionInfo]
GO

USE [Enterprise]
GO

/****** Object:  StoredProcedure [dbo].[rsp_GetBarPositionInfo]    Script Date: 06/14/2013 11:50:08 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/************************************************************
 * Code formatted by SoftTree SQL Assistant © v4.8.29
 * Time: 13/06/2013 5:15:56 PM
 ************************************************************/

CREATE PROCEDURE [dbo].[rsp_GetBarPositionInfo]
(@BarPositionID INT)

AS
BEGIN


SELECT TOP 1 Bar_Position.Bar_Position_Invoice_Period,
       SITE.Site_Code,
       SITE.Site_Name,
       Bar_Position.Site_ID,
       Bar_Position_Name,
       Bar_Position_Company_Position_Code,
       Bar_Position_End_Date,
       Bar_Position.Machine_Type_ID,
       Bar_Position_Location,
       Bar_Position.Zone_ID,
       Bar_Position.Bar_Position_Supplier_Site_Code,
       Bar_Position.Bar_Position_Supplier_Position_Code,
       Bar_Position_Image_Reference,
       Bar_Position_Price_Per_Play_Default,
       Bar_Position_Price_Per_Play,
       Bar_Position_Jackpot_Default,
       Bar_Position_Jackpot,
       Bar_Position_Percentage_Payout_Default,
       Bar_Position_Percentage_Payout,
       Bar_Position.Terms_Group_ID_Default,
       Bar_Position.Terms_Group_ID,
       Bar_Position.Access_Key_ID_Default,
       Bar_Position.Access_Key_ID,
       Depot.Depot_ID,
       Depot.Depot_Name,
       Operator.Operator_ID,
       Operator.Operator_Name,
       Machine_Class.Machine_Name,
       Machine_Class.Machine_BACTA_Code,
       MACHINE.Machine_Stock_No,
       Installation.Installation_End_Date,
       Bar_Position.Bar_Position_Category,
       Bar_Position.Bar_Position_Override_Licence,
       Bar_Position.Bar_Position_Override_Shares,
       Bar_Position.Bar_Position_Override_Rent,
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
       Bar_Position_Collection_Period,
       Terms_Group_Past_Changeover_Date,
       Terms_Group_Past_ID,
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
       Bar_Position.Bar_Position_Use_Terms,
       Bar_Position_Collection_Day,
       Bar_Position_Use_Site_Share_For_Secondary_Brewery,
       Terms_Group_Changeover_Date,
       Terms_Group_Future_ID,
       Bar_Position_Prize_LOS,
       Bar_Position_Rent_Schedule_ID,
       Bar_Position_Share_Schedule_ID,
       Bar_Position_Override_Rent_Schedule,
       Bar_Position_Override_Share_Schedule,
       Bar_Position_Override_Rent_From_Schedule_To_Rent,
       Bar_Position_Override_Rent_From_Rent_To_Schedule,
       Bar_Position_Override_Rent_From_Schedule_To_Rent_Date,
       Bar_Position_Override_Rent_From_Rent_To_Schedule_Date,
       Bar_Position_Rent_Schedule_ID_From,
       Bar_Position_Disable_EDI_Export,
       Bar_Position_IsEnable
FROM   (
           (
               (
                   (
                       (
                           (
                               Bar_Position LEFT JOIN Depot ON Bar_Position.Depot_ID 
                               = Depot.Depot_ID
                           ) LEFT JOIN Operator ON Depot.Supplier_ID = Operator.Operator_ID
                       ) LEFT JOIN Installation ON Bar_Position.Bar_Position_ID 
                       = Installation.Bar_Position_ID 
                   ) LEFT JOIN MACHINE ON Installation.Machine_ID = MACHINE.Machine_ID
               ) LEFT JOIN Machine_Class ON MACHINE.Machine_Class_ID = 
               Machine_Class.Machine_Class_ID
           ) LEFT JOIN SITE ON Bar_Position.Site_ID = SITE.Site_ID
       )
WHERE  Bar_Position.Bar_Position_ID = @BarPositionID
order by ISNULL(Installation.Installation_ID,0) desc 
END
GO


