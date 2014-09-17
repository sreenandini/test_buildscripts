USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_ImportPromotionsDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_ImportPromotionsDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =================================================================
-- rsp_ImportPromotionsDetails
-- -----------------------------------------------------------------
--
-- Imports the Promotions details to Promotions table.
-- 
-- -----------------------------------------------------------------    
-- Revision History       
--       
-- 16/09/13 Durga Created     
-- =================================================================  

CREATE PROCEDURE dbo.rsp_ImportPromotionsDetails
@doc VARCHAR(MAX),
@iSiteID INT ,
@IsSuccess INT OUTPUT

AS

DECLARE @iRowCount INT
DECLARE @idoc INT
DECLARE @error INT  
DECLARE @iHQID INT  


--variables for error handling 
SET @IsSuccess = -1 
SET @error = 0

IF ISNULL(@doc,'') = ''
BEGIN
	SET @IsSuccess = 0 
	RETURN @error 
END

--Declare a table variable to hold the data.
DECLARE @Promotions TABLE(
HQ_ID INT,
PromotionalID INT IDENTITY (1,1),
PromotionalTicketType int,
PromotionalName VARCHAR(255),
TotalTickets int,
PromotionalTicketAmount Decimal(18,2),
TotalTicketAmount AS (TotalTickets*PromotionalTicketAmount), 
dtExpire datetime null,
SourceName Varchar(50) null,
SiteID int,
PromoStatus tinyint DEFAULT 0,
dtPromoCreation datetime
)


--Create an internal representation of the XML document.
EXEC sp_xml_preparedocument @idoc OUTPUT, @doc  

--Set row count to 0.
SET @iRowCount = 0

--Insert the XML data to the table varible.
INSERT INTO @Promotions (HQ_ID,PromotionalTicketType,PromotionalName,TotalTickets,PromotionalTicketAmount,dtExpire,SourceName,PromoStatus,dtPromoCreation)
SELECT *  FROM OPENXML (@idoc, '/PromotionsRoot/Promotions',2)         
WITH 
(
PromotionalID int './PromotionalID',
PromotionalTicketType int  './PromotionalTicketType',
PromotionalName VARCHAR(255) './PromotionalName',
TotalTickets int './TotalTickets',
PromotionalTicketAmount Decimal(18,2) './PromotionalTicketAmount',
dtExpire datetime './dtExpire',
SourceName Varchar(50) './SourceName',
PromoStatus tinyint './PromoStatus',
dtPromoCreation datetime './dtPromoCreation'
)


--Update Siteid
UPDATE @Promotions
SET SiteID = @iSiteID

select * from @Promotions

--Get the row count value.
SELECT @iRowCount = COUNT(HQ_ID) FROM @Promotions

print 'RowCount'
print @iRowCount


SELECT  @iHQID = PromotionalID
FROM OPENXML (@idoc, '/PromotionsRoot',2)    
WITH (PromotionalID int './Promotions/PromotionalID') 


print 'iHQID'
print @iHQID

--Check for row count value.
IF @iRowCount > 0
BEGIN
	IF EXISTS(SELECT HQ_ID FROM Promotions WHERE HQ_ID = @iHQID AND SiteID = @iSiteID)
	BEGIN
		--Update Code Start.
		UPDATE Prom
		SET 
		Prom.PromotionalTicketType =tmpProm.PromotionalTicketType,
		Prom.PromotionalName=tmpProm.PromotionalName,
		Prom.TotalTickets=tmpProm.TotalTickets,
		Prom.PromotionalTicketAmount=tmpProm.PromotionalTicketAmount,		
		Prom.dtExpire=tmpProm.dtExpire,
		Prom.SourceName=tmpProm.SourceName,
		Prom.PromoStatus=tmpProm.PromoStatus,
		Prom.dtPromoCreation=tmpProm.dtPromoCreation
		FROM @Promotions tmpProm
		INNER JOIN Promotions Prom 
		ON Prom.HQ_ID = tmpProm.HQ_ID 
		AND Prom.SiteID = @iSiteID 
		
	END
	ELSE	
	BEGIN
		--Insert Code Start.
		INSERT INTO Promotions (HQ_ID,PromotionalTicketType,PromotionalName,TotalTickets,PromotionalTicketAmount,dtExpire,SourceName,SiteID,PromoStatus,dtPromoCreation)
		SELECT
		HQ_ID,
		 PromotionalTicketType,
		 PromotionalName,
		 TotalTickets,
		 PromotionalTicketAmount,
		 dtExpire,
		 SourceName,
		 @iSiteID,
		 PromoStatus,
		 dtPromoCreation
		  FROM @Promotions   
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

