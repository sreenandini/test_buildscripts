USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_ImportExternalVoucherDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_ImportExternalVoucherDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =================================================================
-- rsp_ImportExternalVoucherDetails
-- -----------------------------------------------------------------
--
-- Imports the External Voucher details to Promotions table.
-- 
-- -----------------------------------------------------------------    
-- Revision History       
--       
-- Nov 10 2013 Durga Created     
-- =================================================================  

CREATE PROCEDURE dbo.rsp_ImportExternalVoucherDetails  
@doc VARCHAR(MAX),  
@iSiteID INT ,  
@IsSuccess INT OUTPUT  
  
AS  
  
DECLARE @iRowCount INT  
DECLARE @idoc INT  
DECLARE @error INT    
DECLARE @iHQID INT
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
DECLARE @ExtVoucherDetails TABLE(  
RowId INT IDENTITY (1,1),  
PromotionalID INT NULL,  
VoucherID INT NULL,  
ActualValueType tinyint NULL,  
EffectiveDate DateTime NULL,  
IsPlayerIDReq tinyint NULL,  
PlayerID Varchar(20) NULL,  
Location Varchar(20) NULL,   
EmployeeID Varchar(20) NULL,  
HQ_ID int,  
SiteID int  
)  
  
  
--Create an internal representation of the XML document.  
EXEC sp_xml_preparedocument @idoc OUTPUT, @doc    
  
--Set row count to 0.  
SET @iRowCount = 0  
  
--Insert the XML data to the table varible.  
INSERT INTO @ExtVoucherDetails   
(  
PromotionalID,  
VoucherID,  
ActualValueType,  
EffectiveDate,  
IsPlayerIDReq,  
PlayerID,  
Location,   
EmployeeID,  
HQ_ID,  
SiteID)  
SELECT *  FROM OPENXML (@idoc, '/ExtVoucherRoot/ExtVoucher',2)           
WITH   
(  
PromotionalID int './PromotionalID',  
VoucherID int  './VoucherID',  
ActualValueType tinyint './ActualValueType',  
EffectiveDate Datetime './EffectiveDate',  
IsPlayerIDReq tinyint './IsPlayerIDReq',  
PlayerID Varchar(20) './PlayerID',  
Location Varchar(20) './Location',  
EmployeeID Varchar(20) './EmployeeID',  
HQ_ID int './RowId',  
SiteID int './Site_Code'  
)  
  
  
----Update Siteid  
--UPDATE @ExtVoucherDetails  
--SET SiteID = @iSiteID  
  
--select * from @ExtVoucherDetails  
  
--Get the row count value.  
SELECT @iRowCount = COUNT(HQ_ID) FROM @ExtVoucherDetails  
  
--print 'RowCount'  
--print @iRowCount  
  
  
SELECT  @iVoucherID = VoucherID  
FROM OPENXML (@idoc, '/ExtVoucherRoot',2)      
WITH (VoucherID int './ExtVoucher/VoucherID')   
  
  
--print 'iHQID'  
--print @iVoucherID  
  
--Check for row count value.  
IF @iRowCount > 0  
BEGIN  
 IF EXISTS(SELECT 1 FROM ExtVoucherDetails WHERE VoucherID = @iVoucherID AND SiteCode = @iSiteID)  
 BEGIN  
 print 'ID Already Exists'
  --Update Code Start.  
  UPDATE ExtVD  
  SET   
  ExtVD.PromotionalID=tmpExtVD.PromotionalID,  
ExtVD.VoucherID=tmpExtVD.VoucherID,  
ExtVD.ActualValueType=tmpExtVD.ActualValueType,  
ExtVD.EffectiveDate=tmpExtVD.EffectiveDate,  
ExtVD.IsPlayerIDReq=tmpExtVD.IsPlayerIDReq,  
ExtVD.PlayerID=tmpExtVD.PlayerID,  
ExtVD.Location=tmpExtVD.Location,   
ExtVD.EmployeeID=tmpExtVD.EmployeeID,  
ExtVD.HQ_ID=tmpExtVD.HQ_ID  
  FROM @ExtVoucherDetails tmpExtVD  
  INNER JOIN ExtVoucherDetails ExtVD   
  ON ExtVD.VoucherID = @iVoucherID   
  AND ExtVD.SiteCode = tmpExtVD.SiteID   
    
 END  
 ELSE   
 BEGIN  
 print 'New ID'
  --Insert Code Start.  
  INSERT INTO ExtVoucherDetails   
(  
PromotionalID,  
VoucherID,  
ActualValueType,  
EffectiveDate,  
IsPlayerIDReq,  
PlayerID,  
Location,   
EmployeeID,  
HQ_ID,  
SiteCode)  
SELECT      
  
PromotionalID,  
VoucherID,  
ActualValueType,  
EffectiveDate,  
IsPlayerIDReq,  
PlayerID,  
Location,   
EmployeeID,  
HQ_ID,  
SiteID  
  
FROM @ExtVoucherDetails     
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
