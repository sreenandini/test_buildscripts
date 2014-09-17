USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_UpdateBarPositionMachineEnabledStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_UpdateBarPositionMachineEnabledStatus]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- Created by Yoganandh 18/02/2010
CREATE PROCEDURE usp_UpdateBarPositionMachineEnabledStatus
(
	@doc XML
)
AS

BEGIN	

DECLARE @BADAssetSerialNo VARCHAR(50)
DECLARE @BADEntityCurrentStatus INT
DECLARE @BarPositionId INT
DECLARE @BarPositionDate DATETIME

DECLARE @docHandle INT

EXEC sp_xml_preparedocument @docHandle OUTPUT, @doc 

Select	@BADAssetSerialNo = BADAssetSerialNo, 
		@BADEntityCurrentStatus = BADEntityCurrentStatus,
		@BarPositionDate = BADUpdatedDate
FROM OPENXML(@docHandle , './AAMSDetails/BADAAMSStatus', 2 )
with      
(      
	BADAssetSerialNo varchar(50) './BADAssetSerialNo',	
	BADEntityCurrentStatus varchar(50) './BADEntityCurrentStatus',
	BADUpdatedDate DATETIME './BADUpdatedDate'
)  
EXEC sp_xml_removedocument @dochandle

		IF EXISTS(SELECT 1 FROM Machine WHERE ActAssetNo+'|'+Machine_Manufacturers_Serial_No+'|'+GMUNo =@BADAssetSerialNo)
		BEGIN
		SELECT @BarPositionId = Bar_Position_ID FROM INSTALLATION WHERE Machine_ID IN (SELECT Machine_ID FROM Machine 
		WHERE ActAssetNo+'|'+Machine_Manufacturers_Serial_No+'|'+GMUNo =@BADAssetSerialNo) AND Installation_End_Date IS NULL
		--Machine_Manufacturers_Serial_No = @SerialNo AND ActAssetNo=@AssetNo AND GMUNo=@GMUNo
		END	
		IF EXISTS(SELECT 1 FROM BAR_Position WHERE Bar_Position_ID = @BarPositionId AND ISNULL(Bar_Position_Machine_Enabled,0) <> 100 AND ISNULL(Bar_Position_Machine_Enabled,0) <> 101)
		BEGIN			
			UPDATE BAR_Position SET Bar_Position_Machine_Enabled = @BADEntityCurrentStatus, Bar_Position_Machine_Enabled_Date = @BarPositionDate WHERE Bar_Position_ID = @BarPositionId	
		END

END

GO

