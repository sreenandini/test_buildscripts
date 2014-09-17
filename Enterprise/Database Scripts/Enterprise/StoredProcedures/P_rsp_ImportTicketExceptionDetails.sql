USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_ImportTicketExceptionDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_ImportTicketExceptionDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =================================================================
-- rsp_ImportTicketExceptionDetails
-- -----------------------------------------------------------------
--
-- Imports the ticket exception details to Ticket_Exception table.
-- 
-- -----------------------------------------------------------------    
-- Revision History       
--       
-- 09/09/09 Renjish Created    
-- 27/04/10	Sudarsan S	importing new column IsNonCashable  
-- =================================================================  

CREATE PROCEDURE dbo.rsp_ImportTicketExceptionDetails
@doc VARCHAR(MAX),
@iSiteID INT ,
@IsSuccess INT OUTPUT

AS

DECLARE @iRowCount INT
DECLARE @idoc INT
DECLARE @error INT  
DECLARE @iTEID INT  
DECLARE @sBarCode CHAR(32)

--variables for error handling 
SET @IsSuccess = -1 
SET @error = 0

IF ISNULL(@doc,'') = ''
BEGIN
	SET @IsSuccess = 0 
	RETURN @error 
END

--Declare a table variable to hold the data.
DECLARE @Ticket_Exception TABLE(
[TE_ID] INT,
[TE_TicketNumber] VARCHAR(50),
[TE_TicketSequenceNumber] INT,
[TE_Workstation] VARCHAR(50),
[TE_Date] DATETIME,
[TE_Value] INT,
[TE_Installation_No] INT,
[TE_Status] VARCHAR(1),
[TE_Meters_REQ_Handpay] INT,
[TE_Meters_REQ_Jackpot] INT,
[TE_Meters_REQ_TicketOut] INT,
[TE_Meters_RESP_Handpay] INT,
[TE_Meters_RESP_Jackpot] INT,
[TE_Meters_RESP_TicketOut] INT,
[TE_Status_Create_Expected] VARCHAR(10),
[TE_Status_Create_Actual] VARCHAR(10),
[TE_Status_Final_Actual] VARCHAR(10),
[te_hp_type] VARCHAR(50),
[TE_Barcode] VARCHAR(40),
[TE_Hashed] VARBINARY(200),
[TE_Site_ID] INT,
[TE_Ticket_Type] INT
)
--add the encoding version as we need to process special characters like pound symbol 
--SET @doc='<?xml version="1.0" encoding="ISO-8859-1"?>' + @doc  

--Create an internal representation of the XML document.
EXEC sp_xml_preparedocument @idoc OUTPUT, @doc  

--Set row count to 0.
SET @iRowCount = 0

--Insert the XML data to the table varible.
INSERT INTO @Ticket_Exception
SELECT * FROM OPENXML (@idoc, '/Ticket_Exception/Ticket',2)         
WITH Ticket_Exception  

--Update Siteid
UPDATE @Ticket_Exception
SET [TE_Site_ID] = @iSiteID

--Get the row count value.
SELECT @iRowCount = COUNT(TE_ID) FROM @Ticket_Exception

--Assign the Voucher ID, Site ID & Bar Code to the respective variables.  
SELECT  @iTEID = TE_ID,
		@sBarCode = LTRIM(RTRIM(TE_TicketNumber))
FROM OPENXML (@idoc, '/Ticket_Exception',2)    
WITH (TE_ID int './Ticket/TE_ID', 
TE_TicketNumber CHAR(32) './Ticket/TE_TicketNumber') 

--Check for row count value.
IF @iRowCount > 0
BEGIN
	IF EXISTS(SELECT TE_ID FROM Ticket_Exception WHERE TE_ID = @iTEID AND TE_Site_ID = @iSiteID)-- AND LTRIM(RTRIM(TE_TicketNumber)) = @sBarCode)
	BEGIN
		--Update Code Start.
		UPDATE TE
		SET 
		TE_TicketSequenceNumber = tmpTE.TE_TicketSequenceNumber,
		TE_Workstation = tmpTE.TE_Workstation,   
		TE_Date = tmpTE.TE_Date,
		TE_Value = tmpTE.TE_Value,
		TE_Installation_No = tmpTE.TE_Installation_No,
		TE_Status = tmpTE.TE_Status,
		TE_Meters_REQ_Handpay = tmpTE.TE_Meters_REQ_Handpay,
		TE_Meters_REQ_Jackpot = tmpTE.TE_Meters_REQ_Jackpot,
		TE_Meters_REQ_TicketOut = tmpTE.TE_Meters_REQ_TicketOut,
		TE_Meters_RESP_Handpay = tmpTE.TE_Meters_RESP_Handpay,
		TE_Meters_RESP_Jackpot = tmpTE.TE_Meters_RESP_Jackpot,
		TE_Meters_RESP_TicketOut = tmpTE.TE_Meters_RESP_TicketOut,
		TE_Status_Create_Expected = tmpTE.TE_Status_Create_Expected,
		TE_Status_Create_Actual = tmpTE.TE_Status_Create_Actual,
		TE_Status_Final_Actual = tmpTE.TE_Status_Final_Actual,
		te_hp_type = tmpTE.te_hp_type,
		TE_Barcode = tmpTE.TE_Barcode,
		TE_Hashed = tmpTE.TE_Hashed,
		TE_Ticket_Type = tmpTE.TE_Ticket_Type
		FROM @Ticket_Exception tmpTE
		INNER JOIN Ticket_Exception TE 
		ON TE.TE_ID = tmpTE.TE_ID 
		AND TE.TE_Site_ID = @iSiteID 
		--AND	LTRIM(RTRIM(TE.TE_TicketNumber)) = LTRIM(RTRIM(tmpTE.TE_TicketNumber))
		--Update Code End.
	END
	ELSE	
	BEGIN
		--Insert Code Start.
		INSERT INTO Ticket_Exception
		SELECT * FROM @Ticket_Exception   
		--Insert Code End.
	END	
END

--Removes the internal representation of the XML document.
EXEC sp_xml_removedocument @idoc

--Check for any errors during the insert process.
SET @error = @@ERROR
IF @error <> 0 
GOTO Err_Handler 
 
--Return success/failure  
Err_Handler:  
IF @error = 0  
SET @IsSuccess = 0 
--Success 
ELSE
SET @IsSuccess = @error 
--Error  
RETURN @error 

GO

