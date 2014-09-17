USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_UpdateManufacturerDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_UpdateManufacturerDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[usp_UpdateManufacturerDetails]
		@Manufacturer_ID int = 0,
		@Manufacturer_Name varchar(50),
		@Manufacturer_Code varchar(10),
		@Manufacturer_Sales_Address ntext,
		@Manufacturer_Sales_Contact varchar(50),
		@Manufacturer_Sales_EMail varchar(50),
		@Manufacturer_Sales_Postcode varchar(10),
		@Manufacturer_Sales_Tel varchar(50),
		@Manufacturer_Service_Address ntext,
		@Manufacturer_Service_Contact varchar(50),
		@Manufacturer_Service_EMail varchar(50),
		@Manufacturer_Service_Postcode varchar(10),
		@Manufacturer_Service_Tel varchar(50),
		@Manufacturer_Coins_In_Meter_Used bit,
		@Manufacturer_Coins_Out_Meter_Used bit,
		@Manufacturer_Coin_Drop_Meter_Used bit,
		@Manufacturer_Handpay_Meter_Used bit,
		@Manufacturer_External_Credits_Meter_Used bit,
		@Manufacturer_Games_Bet_Meter_Used bit,
		@Manufacturer_Games_Won_Meter_Used bit,
		@Manufacturer_Notes_Meter_Used bit,
		@Manufacturer_Single_Coin_Build bit,
		@Manufacturer_Handpay_Added_To_Coin_Out bit		 
	  
AS
/*****************************************************************************************************
DESCRIPTION : Updates Manufacturer Details  
CREATED DATE: PROC CreateDate
CREATED BY: Lekha
MODULE            : Manufacturer      
CHANGE HISTORY :
------------------------------------------------------------------------------------------------------
AUTHOR                              DESCRIPTON                                                        MODIFIED DATE
------------------------------------------------------------------------------------------------------
                                                              
*****************************************************************************************************/

BEGIN
      SET NOCOUNT ON 
      DECLARE @_Modified TABLE (
                                Manufacturer_ID INT,
                                OldManufacturer_Name varchar(50), NewManufacturer_Name varchar(50),
                                Manufacturer_NameChanged AS (CASE WHEN OldManufacturer_Name = NewManufacturer_Name THEN 0 ELSE 1 END)
)

      UPDATE Manufacturer SET
				Manufacturer_Name=@Manufacturer_Name,
				Manufacturer_Code=@Manufacturer_Code,
				Manufacturer_Sales_Address=@Manufacturer_Sales_Address,
				Manufacturer_Sales_Contact=@Manufacturer_Sales_Contact,
				Manufacturer_Sales_EMail=@Manufacturer_Sales_EMail,
				Manufacturer_Sales_Postcode=@Manufacturer_Sales_Postcode,
				Manufacturer_Sales_Tel=@Manufacturer_Sales_Tel,
				Manufacturer_Service_Address=@Manufacturer_Service_Address,
				Manufacturer_Service_Contact=@Manufacturer_Service_Contact,
				Manufacturer_Service_EMail=@Manufacturer_Service_EMail,
				Manufacturer_Service_Postcode=@Manufacturer_Service_Postcode,
				Manufacturer_Service_Tel=@Manufacturer_Service_Tel,
				Manufacturer_Coins_In_Meter_Used=@Manufacturer_Coins_In_Meter_Used,
				Manufacturer_Coins_Out_Meter_Used=@Manufacturer_Coins_Out_Meter_Used,
				Manufacturer_Coin_Drop_Meter_Used=@Manufacturer_Coin_Drop_Meter_Used,
				Manufacturer_Handpay_Meter_Used=@Manufacturer_Handpay_Meter_Used,
				Manufacturer_External_Credits_Meter_Used=@Manufacturer_External_Credits_Meter_Used,
				Manufacturer_Games_Bet_Meter_Used=@Manufacturer_Games_Bet_Meter_Used,
				Manufacturer_Games_Won_Meter_Used=@Manufacturer_Games_Won_Meter_Used,
				Manufacturer_Notes_Meter_Used=@Manufacturer_Notes_Meter_Used,
				Manufacturer_Single_Coin_Build=@Manufacturer_Single_Coin_Build,
				Manufacturer_Handpay_Added_To_Coin_Out=@Manufacturer_Handpay_Added_To_Coin_Out
	OUTPUT INSERTED.Manufacturer_ID,
           DELETED.Manufacturer_Name, INSERTED.Manufacturer_Name
    INTO @_Modified
	WHERE Manufacturer_ID=@Manufacturer_ID
	
	UPDATE MeterAnalysis.dbo.Manufacturer SET
				Manufacturer_Name=@Manufacturer_Name,
				Manufacturer_Code=@Manufacturer_Code,
				Manufacturer_Sales_Address=@Manufacturer_Sales_Address,
				Manufacturer_Sales_Contact=@Manufacturer_Sales_Contact,
				Manufacturer_Sales_EMail=@Manufacturer_Sales_EMail,
				Manufacturer_Sales_Postcode=@Manufacturer_Sales_Postcode,
				Manufacturer_Sales_Tel=@Manufacturer_Sales_Tel,
				Manufacturer_Service_Address=@Manufacturer_Service_Address,
				Manufacturer_Service_Contact=@Manufacturer_Service_Contact,
				Manufacturer_Service_EMail=@Manufacturer_Service_EMail,
				Manufacturer_Service_Postcode=@Manufacturer_Service_Postcode,
				Manufacturer_Service_Tel=@Manufacturer_Service_Tel,
				Manufacturer_Coins_In_Meter_Used=@Manufacturer_Coins_In_Meter_Used,
				Manufacturer_Coins_Out_Meter_Used=@Manufacturer_Coins_Out_Meter_Used,
				Manufacturer_Coin_Drop_Meter_Used=@Manufacturer_Coin_Drop_Meter_Used,
				Manufacturer_Handpay_Meter_Used=@Manufacturer_Handpay_Meter_Used,
				Manufacturer_External_Credits_Meter_Used=@Manufacturer_External_Credits_Meter_Used,
				Manufacturer_Games_Bet_Meter_Used=@Manufacturer_Games_Bet_Meter_Used,
				Manufacturer_Games_Won_Meter_Used=@Manufacturer_Games_Won_Meter_Used,
				Manufacturer_Notes_Meter_Used=@Manufacturer_Notes_Meter_Used,
				Manufacturer_Single_Coin_Build=@Manufacturer_Single_Coin_Build,
				Manufacturer_Handpay_Added_To_Coin_Out=@Manufacturer_Handpay_Added_To_Coin_Out
	OUTPUT INSERTED.Manufacturer_ID,
           DELETED.Manufacturer_Name, INSERTED.Manufacturer_Name
    INTO @_Modified
	WHERE Manufacturer_ID=@Manufacturer_ID
	
	
	IF EXISTS(
               SELECT 1
               FROM   @_Modified m
               WHERE  m.Manufacturer_NameChanged = 1 
                )
                BEGIN
					EXEC [dbo].usp_EBS_UpdateManufacturerDetails @Manufacturer_ID 
                END

END

GO

