USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[USP_InsertBarPosition]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[USP_InsertBarPosition]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE USP_InsertBarPosition
@Bar_Position_Name VARCHAR(50),  
@Site_ID INT,  
@Bar_Position_Start_Date VARCHAR(30),  
@Depot_ID INT,  
@Bar_Position_Rent_Past_Date VARCHAR(30),  
@Bar_Position_Share_Past_Date VARCHAR(30),  
@Bar_Position_Licence_Past_Date VARCHAR(30),  
@Bar_Position_Rent_Future_Date VARCHAR(30),  
@Bar_Position_Share_Future_Date VARCHAR(30),  
@Bar_Position_Licence_Future_Date VARCHAR(30),  
@Bar_Position_Use_Terms BIT,  
@Bar_Position_Last_Collection_Date VARCHAR(30),  
@Bar_Position_Collection_Rent_Paid_Until VARCHAR(30),  
@Bar_Position_Price_Per_Play VARCHAR(50),  
@Bar_Position_Price_Per_Play_Default BIT,  
@Bar_Position_Jackpot VARCHAR(50),  
@Bar_Position_Jackpot_Default BIT,  
@Bar_Position_Percentage_Payout VARCHAR(50),  
@Bar_Position_Percentage_Payout_Default BIT,  
@Access_Key_ID INT,  
@Access_Key_ID_Default BIT,  
@Terms_Group_ID INT,  
@Terms_Group_ID_Default BIT,
@BarPositionId Int Output   

AS
BEGIN
INSERT INTO Bar_Position
(
		-- Bar_Position_ID -- this column value is auto-generated,
Bar_Position_Name,
Site_ID,
Bar_Position_Start_Date,
Depot_ID,
Bar_Position_Rent_Past_Date,
Bar_Position_Share_Past_Date,
Bar_Position_Licence_Past_Date,
Bar_Position_Rent_Future_Date,
Bar_Position_Share_Future_Date,
Bar_Position_Licence_Future_Date,
Bar_Position_Use_Terms,
Bar_Position_Last_Collection_Date,
Bar_Position_Collection_Rent_Paid_Until,
Bar_Position_Price_Per_Play,
Bar_Position_Price_Per_Play_Default,
Bar_Position_Jackpot,
Bar_Position_Jackpot_Default,
Bar_Position_Percentage_Payout,
Bar_Position_Percentage_Payout_Default,
Access_Key_ID,
Access_Key_ID_Default,
Terms_Group_ID,
Terms_Group_ID_Default
)
VALUES
(
RIGHT('000'+@Bar_Position_Name, 3),
@Site_ID,
@Bar_Position_Start_Date,
@Depot_ID,
@Bar_Position_Rent_Past_Date,
@Bar_Position_Share_Past_Date,
@Bar_Position_Licence_Past_Date,
@Bar_Position_Rent_Future_Date,
@Bar_Position_Share_Future_Date,
@Bar_Position_Licence_Future_Date,
@Bar_Position_Use_Terms,
@Bar_Position_Last_Collection_Date,
@Bar_Position_Collection_Rent_Paid_Until,
@Bar_Position_Price_Per_Play,
@Bar_Position_Price_Per_Play_Default,
@Bar_Position_Jackpot,
@Bar_Position_Jackpot_Default,
@Bar_Position_Percentage_Payout,
@Bar_Position_Percentage_Payout_Default,
@Access_Key_ID,
@Access_Key_ID_Default,
@Terms_Group_ID,
@Terms_Group_ID_Default
)	
SELECT @BarPositionId = CAST(SCOPE_IDENTITY() AS INT)  
END


GO