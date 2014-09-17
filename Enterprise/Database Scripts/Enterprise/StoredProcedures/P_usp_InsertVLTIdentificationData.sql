USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_InsertVLTIdentificationData]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_InsertVLTIdentificationData]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =================================================================  
-- usp_InsertVLTIdentificationData  
-- -----------------------------------------------------------------  
--  
-- Insert the VLT Identification Data..  
--   
-- -----------------------------------------------------------------      
-- Revision History         
--         
-- 01/03/10 Renjish Created        
-- =================================================================   
  
CREATE PROCEDURE dbo.usp_InsertVLTIdentificationData  
@Message_ID VARCHAR(20), 
@VLT_AAMS_Code	VARCHAR(12),
@VLT_MAC_Address	VARCHAR(17),
@VLT_Connection_Type	INT,
@Site_AAMS_Code	VARCHAR(12),
@PS_MAC_Address	VARCHAR(17),
@PS_Connection_Type	INT,
@Total_Bet	DECIMAL(18,0)
AS  

IF EXISTS(SELECT VID_Message_ID FROM VLT_Identification_Data WHERE VID_Message_ID = @Message_ID)
BEGIN
	UPDATE VLT_Identification_Data
	SET VID_VLT_AAMS_Code = @VLT_AAMS_Code,
	VID_VLT_MAC_Address = @VLT_MAC_Address,
	VID_VLT_Connection_Type = @VLT_Connection_Type,
	VID_Site_AAMS_Code = @Site_AAMS_Code,
	VID_PS_MAC_Address = @PS_MAC_Address,
	VID_PS_Connection_Type = @PS_Connection_Type,
	VID_Total_Bet = @Total_Bet
	WHERE VID_Message_ID = @Message_ID
END
ELSE
BEGIN
	INSERT INTO VLT_Identification_Data(VID_Message_ID, VID_VLT_AAMS_Code, VID_VLT_MAC_Address, VID_VLT_Connection_Type, 
	VID_Site_AAMS_Code, VID_PS_MAC_Address, VID_PS_Connection_Type, VID_Total_Bet) 
	VALUES (@Message_ID, @VLT_AAMS_Code, @VLT_MAC_Address, @VLT_Connection_Type, @Site_AAMS_Code, @PS_MAC_Address,
	@PS_Connection_Type, @Total_Bet)
END

UPDATE BMC_BAS_Export_History
SET BBEH_Session_Status = 'Completed'
WHERE BBEH_Message_Type = 311 AND 
BBEH_BAS_Message_ID = @Message_ID


GO

