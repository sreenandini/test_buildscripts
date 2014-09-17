USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_ResolveMACAddress]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_ResolveMACAddress]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =================================================================  
-- rsp_ResolveMACAddress  
-- -----------------------------------------------------------------  
--  
-- Compare the MAC Address of the slot with LGE and update if it is changed
--   
-- -----------------------------------------------------------------      
-- Revision History         
--         
-- 11/03/10 SaravanaKumar P Created        
-- =================================================================  

CREATE PROCEDURE dbo.rsp_ResolveMACAddress
AS
BEGIN

	DECLARE @DBName VARCHAR(100)
	DECLARE @IsLGEEnabled VARCHAR(10)
	DECLARE @SQLQuery NVARCHAR(4000)
	DECLARE @MachineData TABLE(
	Machine_ID int,
	Machine_Manufacturers_Serial_No varchar(50),
	Machine_MAC_Address varchar(17) )
	

	--Verify LGE Configuration
	SELECT @IsLGEEnabled = Setting_Value FROM dbo.Setting WHERE Setting_Name = 'LGEEnabled'
	IF @IsLGEEnabled IS NULL OR @IsLGEEnabled = 'false'
	BEGIN
		RETURN
	END 

	SELECT @DBName = Setting_Value FROM dbo.Setting WHERE Setting_Name = 'LGEDatabase'
	IF @DBName IS NULL
	BEGIN
		RETURN
	END 

	IF EXISTS (SELECT 1 FROM SYS.SERVERS WHERE NAME = 'LGE')
	BEGIN
		IF NOT EXISTS(SELECT 1 FROM SYS.SERVERS WHERE Data_Source = HOST_NAME() AND NAME = 'LGE')
		BEGIN
			SET @SQLQuery = 'INSERT INTO LGE_VLT_DATA EXEC [LGE].['
		END
		ELSE
		BEGIN
            SET @SQLQuery = 'INSERT INTO LGE_VLT_DATA EXEC ['
		END
	END
	ELSE
	BEGIN
            SET @SQLQuery = 'INSERT INTO LGE_VLT_DATA EXEC ['
	END
	
	SET @SQLQuery = @SQLQuery + @DBName + '].[dbo].p_BMCGetVltData '

	TRUNCATE TABLE LGE_VLT_DATA
	--Populate data from LGE
	EXEC SP_EXECUTESQL @SQLQuery

	-- Store Machine data for MAC Address comparison
	INSERT INTO @MachineData (Machine_ID,Machine_Manufacturers_Serial_No,Machine_MAC_Address)
	SELECT Machine_ID,Machine_Manufacturers_Serial_No,Machine_MAC_Address
	FROM Machine
	INNER JOIN LGE_VLT_DATA LGE
	ON CAST(CAST( Machine_Manufacturers_Serial_No  AS BIGINT) AS VARCHAR(20))=  CAST(CAST( LGE.strSerial AS BIGINT) AS VARCHAR(20))
	WHERE ISNULL(Machine_MAC_Address,'') <> LGE.strMacAddress

	--Update MAC Address in BMC
	UPDATE	Machine 
	SET		Machine_MAC_Address_Prev = Machine_MAC_Address,
			Machine_MAC_Address = LGE.strMacAddress 
	FROM Machine 
	INNER JOIN LGE_VLT_DATA LGE
	ON CAST(CAST(Machine_Manufacturers_Serial_No  AS BIGINT) AS VARCHAR(20))=  CAST(CAST( LGE.strSerial AS BIGINT) AS VARCHAR(20))
	WHERE ISNULL(Machine_MAC_Address,'') <> LGE.strMacAddress

	--Populate BMC_BAS_Export_History
	INSERT INTO BMC_BAS_Export_History 
				(BBEH_Reference,BBEH_AAMS_Entity_Type,BBEH_Message_Type
				,BBEH_Status,BBEH_BAS_Message_ID,BBEH_Received_Date,BBEH_Exported_Date
				,BBEH_Comments,BBEH_AAMS_Approval,BBEH_Session_Status,BBEH_Financial_Data)

	SELECT		M.Machine_ID ,3,308,
				0,NULL,getdate(),NULL,
				NULL,NULL,NULL,NULL
	FROM @MachineData M
	WHERE M.Machine_MAC_Address IS NOT NULL

END


GO

