USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_GetLatestMeterHistory]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_GetLatestMeterHistory]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROC usp_GetLatestMeterHistory(@doc varchar(max))    
  
AS   
  
BEGIN    

-- idoc is the document handle of the internal representation of an XML document which is created by calling sp_xml_preparedocument. 
DECLARE @idoc int  
-- internal variables 
DECLARE @InstallationNo int 
DECLARE @error  int  
DECLARE @iRowCount  int 

--Table Variable to hold the data temporarily.
CREATE TABLE dbo.#tempInstallations(
	HQ_Installation_No INT,
	Installation_No INT)

--add the encoding version as we need to process special characters like pound symbol 
SET @doc='<?xml version="1.0" encoding="ISO-8859-1"?>' + @doc  

--Create an internal representation of the XML document.
EXEC sp_xml_preparedocument @idoc OUTPUT, @doc  

--Copy the xml data to the table variable.
INSERT INTO dbo.#tempInstallations
SELECT * FROM OPENXML (@idoc, '/Installations/Installation',2) 
WITH dbo.#tempInstallations        
--WITH (HQ_INSTALLATION_NO int './Installation/HQ_Installation_No')--,
--	  INSTALLATION_NO int './Installation/Installation_No')    

--Removes the internal representation of the XML document.
EXEC sp_xml_removedocument @idoc 
SELECT 
	MH_ID,
	MH_Process,
	MH_Type,
	MH_LinkReference,
	MH_Reference,
	MH_Installation_No,
	MH_PAYOUT_FLOAT_TOKEN,
	MH_PAYOUT_FLOAT_10P,
	MH_PAYOUT_FLOAT_20P,
	MH_PAYOUT_FLOAT_50P,
	MH_PAYOUT_FLOAT_100P,
	MH_CASH_IN_1P,
	MH_CASH_IN_2P,
	MH_CASH_IN_5P,
	MH_CASH_IN_10P,
	MH_CASH_IN_20P,
	MH_CASH_IN_50P,
	MH_CASH_IN_100P,
	MH_CASH_IN_200P,
	MH_CASH_IN_500P,
	MH_CASH_IN_1000P,
	MH_CASH_IN_2000P,
	MH_CASH_IN_5000P,
	MH_CASH_IN_10000P,
	MH_CASH_IN_20000P,
	MH_CASH_IN_50000P,
	MH_CASH_IN_100000P,
	MH_TOKEN_IN_5P,
	MH_TOKEN_IN_10P,
	MH_TOKEN_IN_20P,
	MH_TOKEN_IN_50P,
	MH_TOKEN_IN_100P,
	MH_TOKEN_IN_200P,
	MH_TOKEN_IN_500P,
	MH_TOKEN_IN_1000P,
	MH_CASH_OUT_1P,
	MH_CASH_OUT_2P,
	MH_CASH_OUT_5P,
	MH_CASH_OUT_10P,
	MH_CASH_OUT_20P,
	MH_CASH_OUT_50P,
	MH_CASH_OUT_100P,
	MH_CASH_OUT_200P,
	MH_CASH_OUT_500P,
	MH_CASH_OUT_1000P,
	MH_CASH_OUT_2000P,
	MH_CASH_OUT_5000P,
	MH_CASH_OUT_10000P,
	MH_CASH_OUT_20000P,
	MH_CASH_OUT_50000P,
	MH_CASH_OUT_100000P,
	MH_TOKEN_OUT_5P,
	MH_TOKEN_OUT_10P,
	MH_TOKEN_OUT_20P,
	MH_TOKEN_OUT_50P,
	MH_TOKEN_OUT_100P,
	MH_TOKEN_OUT_200P,
	MH_TOKEN_OUT_500P,
	MH_TOKEN_OUT_1000P,
	MH_CASH_REFILL_5P,
	MH_CASH_REFILL_10P,
	MH_CASH_REFILL_20P,
	MH_CASH_REFILL_50P,
	MH_CASH_REFILL_100P,
	MH_CASH_REFILL_200P,
	MH_CASH_REFILL_500P,
	MH_CASH_REFILL_1000P,
	MH_CASH_REFILL_2000P,
	MH_CASH_REFILL_5000P,
	MH_CASH_REFILL_10000P,
	MH_CASH_REFILL_20000P,
	MH_CASH_REFILL_50000P,
	MH_CASH_REFILL_100000P,
	MH_TOKEN_REFILL_5P,
	MH_TOKEN_REFILL_10P,
	MH_TOKEN_REFILL_20P,
	MH_TOKEN_REFILL_50P,
	MH_TOKEN_REFILL_100P,
	MH_TOKEN_REFILL_200P,
	MH_TOKEN_REFILL_500P,
	MH_TOKEN_REFILL_1000P,
	MH_COINS_IN,
	MH_COINS_OUT,
	MH_COIN_DROP,
	MH_HANDPAY,
	MH_EXTERNAL_CREDIT,
	MH_GAMES_BET,
	MH_GAMES_WON,
	MH_NOTES,
	MH_VTP,
	MH_CANCELLED_CREDITS,
	MH_GAMES_LOST,
	MH_GAMES_SINCE_POWER_UP,
	MH_TRUE_COIN_IN,
	MH_TRUE_COIN_OUT,
	MH_CURRENT_CREDITS,
	MH_JACKPOT,
	MH_BILL_1,
	MH_BILL_2,
	MH_BILL_5,
	MH_BILL_10,
	MH_BILL_20,
	MH_BILL_50,
	MH_BILL_100,
	MH_BILL_250,
	MH_BILL_10000,
	MH_BILL_20000,
	MH_BILL_50000,
	MH_BILL_100000,
	MH_TICKET_PRINTED_QTY,
	MH_TICKET_PRINTED_VALUE,
	MH_TICKET_INSERTED_QTY,
	MH_TICKET_INSERTED_VALUE,
	MH_Datetime,
	MH_progressive_win_value,
	MH_progressive_win_Handpay_value,
	MH_Mystery_Machine_Paid,
	MH_Mystery_Attendant_Paid,
	MH_TICKETS_PRINTED_NONCASHABLE_QTY,
	MH_TICKETS_PRINTED_NONCASHABLE_VALUE,
	MH_TICKETS_INSERTED_NONCASHABLE_QTY,
	MH_TICKETS_INSERTED_NONCASHABLE_VALUE,
	MH_Promo_Cashable_EFT_IN,
	MH_Promo_Cashable_EFT_OUT,
	MH_NonCashable_EFT_IN,
	MH_NonCashable_EFT_OUT,
	MH_Cashable_EFT_IN,
	MH_Cashable_EFT_OUT,
	MH_BILL_200,
	MH_BILL_500
FROM Meter_History 
ORDER BY MH_ID ASC
END    
    

GO

