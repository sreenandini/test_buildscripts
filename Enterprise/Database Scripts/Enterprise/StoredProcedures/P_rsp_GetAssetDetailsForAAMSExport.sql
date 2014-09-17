USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetAssetDetailsForAAMSExport]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetAssetDetailsForAAMSExport]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =================================================================  
-- rsp_GetAssetDetailsForAAMSExport  
-- -----------------------------------------------------------------  
--  
-- Get the asset details to be exported for AAMS.  
--   
-- -----------------------------------------------------------------      
-- Revision History         
--         
-- 19/11/09 Renjish Created        
-- =================================================================   
  
CREATE PROCEDURE dbo.rsp_GetAssetDetailsForAAMSExport
@Machine_ID INT 
AS  

DECLARE @Machine_Status INT
DECLARE @AAMS_Code VARCHAR(12)
DECLARE @Site_ID INT
DECLARE @Flag_Status INT
DECLARE @CodeGameSystem VARCHAR(12)
DECLARE @ParlourMACAddr VARCHAR(17)
DECLARE @ParlourConnType INT
DECLARE @NGAMachineID INT
DECLARE @SenderCode VARCHAR(20)
DECLARE @VLTEnabledDate DATETIME
DECLARE @IsWarehouse INT
DECLARE @Depot_AAMS_Code VARCHAR(12)

SET @IsWarehouse = 0

SELECT @SenderCode = Setting_Value FROM Setting WHERE Setting_Name = 'SenderCode'

SELECT @CodeGameSystem = BAD_AAMS_Code FROM dbo.BMC_AAMS_Details WHERE BAD_AAMS_Entity_Type = 1

SELECT @Machine_Status = Machine_Status_Flag FROM Machine WHERE Machine_ID = @Machine_ID

--Machine in stock get the depot's AAMS Code
IF @Machine_Status = 0 OR @Machine_Status = 12 OR @Machine_Status = 13 OR @Machine_Status = 14
	BEGIN
		SELECT @AAMS_Code = BAD.BAD_AAMS_Code FROM dbo.BMC_AAMS_Details BAD
		INNER JOIN dbo.Depot D ON BAD.BAD_Reference_ID = D.Depot_ID
		INNER JOIN dbo.Machine M ON M.Depot_ID = D.Depot_ID	
		WHERE BAD.BAD_AAMS_Entity_Type = 2 AND M.Machine_ID = @Machine_ID
		AND ISNULL(BAD.BAD_Is_Warehouse,0) = 1

		SET @IsWarehouse = 1
	END
ELSE
--Machine in any other status get the site's AAMS Code. 
	BEGIN	
		SET @IsWarehouse = 0
		SELECT @Site_ID = Site_ID FROM dbo.Site WHERE NGA_Machine_ID = @Machine_ID
		IF ISNULL(@Site_ID,0) <> 0
			BEGIN
				SELECT @AAMS_Code = BAD.BAD_AAMS_Code FROM dbo.BMC_AAMS_Details BAD
				INNER JOIN dbo.Site S ON BAD.BAD_Reference_ID = S.Site_ID
				INNER JOIN dbo.Machine M ON S.NGA_Machine_ID = M.Machine_ID	
				WHERE BAD.BAD_AAMS_Entity_Type = 2 AND S.NGA_Machine_ID = @Machine_ID
				AND ISNULL(BAD.BAD_Is_Warehouse,0) <> 1
			END
		ELSE
			BEGIN
				SELECT @AAMS_Code = BD.BAD_AAMS_Code, @Flag_Status = ISNULL(Bar_Position_Machine_Enabled,0)
				, @NGAMachineID = ISNULL(S.NGA_Machine_ID,0), @VLTEnabledDate = ISNULL(Bar_Position_Machine_Enabled_Date,GETDATE()) 
				FROM dbo.BMC_AAMS_Details BAD
				INNER JOIN dbo.Machine M ON BAD.BAD_Reference_ID = M.Machine_ID	
				INNER JOIN dbo.Installation I ON I.Machine_ID = M.Machine_ID	
				INNER JOIN dbo.Bar_Position BP ON I.Bar_Position_ID = BP.Bar_Position_ID	
				INNER JOIN dbo.Site S ON  BP.Site_ID = S.Site_ID
				INNER JOIN dbo.BMC_AAMS_Details BD ON BD.BAD_Reference_ID = S.Site_ID	
				WHERE BAD.BAD_AAMS_Entity_Type = 3 AND M.Machine_ID = @Machine_ID
				AND BD.BAD_AAMS_Entity_Type = 2 AND ISNULL(BD.BAD_Is_Warehouse,0) <> 1
			END
	END

SELECT @ParlourMACAddr = Machine_MAC_Address, @ParlourConnType = Machine_Connection_Type FROM Machine WHERE Machine_ID = @NGAMachineID

--Get the Depot AAMS Code.
SELECT @Depot_AAMS_Code = BAD.BAD_AAMS_Code FROM dbo.BMC_AAMS_Details BAD
INNER JOIN dbo.Depot D ON BAD.BAD_Reference_ID = D.Depot_ID
INNER JOIN dbo.Machine M ON M.Depot_ID = D.Depot_ID	
WHERE BAD.BAD_AAMS_Entity_Type = 2 AND M.Machine_ID = @Machine_ID
AND ISNULL(BAD.BAD_Is_Warehouse,0) = 1

--Retrive the other values.
SELECT ISNULL(BAD.BAD_AAMS_Code,'') AS BAD_AAMS_Code, ISNULL(M.Machine_MAC_Address,'') AS Machine_MAC_Address, 
ISNULL(M.Machine_Manufacturers_Serial_No,'') AS Machine_Manufacturers_Serial_No, ISNULL(M.Machine_Connection_Type,'') AS Machine_Connection_Type, 
ISNULL(@AAMS_Code,'') AS Location_Code, ISNULL(M.Machine_MAC_Address_Prev,'') AS Machine_MAC_Address_Prev, ISNULL(@Flag_Status,'') AS Flag_Status, 
ISNULL(@CodeGameSystem, '') AS Code_Game_System, ISNULL(@ParlourMACAddr,'') AS Parlour_MAC_Addr, 
ISNULL(@ParlourConnType,'') AS Parlour_Conn_Type, ISNULL(@SenderCode, '') AS SenderCode, ISNULL(BD.BAD_AAMS_Code,'') AS BAD_AAMS_Model_Code,
ISNULL(@VLTEnabledDate, GETDATE()) AS VLT_Enabled_Date, @IsWarehouse AS IsWarehouse, ISNULL(M.Machine_CIV, '') AS Machine_CIV,
ISNULL(M.Machine_CIV_Prev, '') AS Machine_CIV_Prev, ISNULL(M.Machine_CIV_Change_Reason, 0) AS Machine_CIV_Change_Reason,
ISNULL(@Depot_AAMS_Code,'') AS Depot_AAMS_Code
FROM dbo.Machine M 
LEFT JOIN dbo.BMC_AAMS_Details BAD ON M.Machine_ID = BAD.BAD_Reference_ID
INNER JOIN dbo.BMC_AAMS_Details BD ON M.Machine_ModelTypeID = BD.BAD_Reference_ID
WHERE M.Machine_ID = @Machine_ID
AND BAD.BAD_AAMS_Entity_Type = 3 AND BD.BAD_AAMS_Entity_Type = 5
--AND ISNULL(M.Machine_MAC_Address,'') <> '' AND ISNULL(BD.BAD_AAMS_Code,'') <> ''


GO

