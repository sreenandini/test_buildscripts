USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_ImportPromotionalTicketsDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_ImportPromotionalTicketsDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =================================================================
-- rsp_ImportPromotionalTicketsDetails
-- -----------------------------------------------------------------
--
-- Imports the Promotional Tickets details to PromotionalTickets table.
-- 
-- -----------------------------------------------------------------    
-- Revision History       
--       
-- 16/09/13 Durga Created     
-- =================================================================  

CREATE PROCEDURE dbo.rsp_ImportPromotionalTicketsDetails
@doc VARCHAR(MAX),
@iSiteID INT ,
@IsSuccess INT OUTPUT

AS

DECLARE @iRowCount INT
DECLARE @idoc INT
DECLARE @error INT  
DECLARE @iVoucherID INT  


--variables for error handling 
SET @IsSuccess = -1 
SET @error = 0

IF ISNULL(@doc,'') = ''
BEGIN
	SET @IsSuccess = 0 
	RETURN @error 
END

--Declare a table variable to hold the data.
DECLARE @PromotionalTickets TABLE(
SiteID int,
PromotionalID int,
VoucherID int


)


--Create an internal representation of the XML document.
EXEC sp_xml_preparedocument @idoc OUTPUT, @doc  

--Set row count to 0.
SET @iRowCount = 0

--Insert the XML data to the table varible.
INSERT INTO @PromotionalTickets (PromotionalID,VoucherID,SiteID)
SELECT * FROM OPENXML (@idoc, '/PromotionalTicketsRoot/PromotionalTickets',2)         
WITH (PromotionalID int './PromotionalID',
      VoucherID int './VoucherID',
      SiteID int './Site_Code')  

------Update Siteid
----UPDATE @PromotionalTickets
----SET SiteID = @iSiteID


--Get the row count value.
SELECT @iRowCount = COUNT(PromotionalID) FROM @PromotionalTickets


SELECT  @iVoucherID = VoucherID
FROM OPENXML (@idoc, '/PrommotionalTicketsRoot',2)    
WITH (VoucherID int './PromotionalTickets/VoucherID') 

--Check for row count value.
IF @iRowCount > 0
BEGIN
	IF EXISTS(SELECT VoucherID FROM PromotionalTickets WHERE VoucherID = @iVoucherID AND SiteID = @iSiteID)
	BEGIN
		--Update Code Start.
		UPDATE PT
		SET 
		PT.PromotionalID=tmpPT.PromotionalID,
		PT.VoucherID=tmpPT.VoucherID,
		PT.SiteID=tmpPT.SiteID
		FROM @PromotionalTickets tmpPT
		INNER JOIN PromotionalTickets PT 
		ON PT.VoucherID=tmpPT.VoucherID
		AND PT.SiteID = @iSiteID 
		
	END
	ELSE	
	BEGIN
		--Insert Code Start.
		INSERT INTO PromotionalTickets (PromotionalID,VoucherID,SiteID)
		SELECT
		PromotionalID,
		VoucherID,
		 SiteID
		  FROM @PromotionalTickets   
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

