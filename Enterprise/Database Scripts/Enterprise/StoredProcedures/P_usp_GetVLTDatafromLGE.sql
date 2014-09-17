USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_GetVLTDatafromLGE]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_GetVLTDatafromLGE]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-------------------------------------------------------------------------- 
---
--- Description: inserts/updates the table LGE_VLT_Data from LGE server
---
--- Inputs:      see inputs
---
--- Outputs:     
--- 
--- =======================================================================
--- 
--- Revision History
--- 
--- Sudarsan S  24/11/09     Created 
--------------------------------------------------------------------------- 
CREATE PROCEDURE [dbo].[usp_GetVLTDatafromLGE]
AS

BEGIN

	DECLARE @DBName VARCHAR(100)
	DECLARE @SQLQuery NVARCHAR(1000)

	DECLARE @Sno	INT
	SET @Sno = 1
	DECLARE @iID INT
	DECLARE @Count	INT
	DECLARE @iRefID INT
	DECLARE @SiteCode VARCHAR(50)

	CREATE TABLE #BMC_AAMS_Details
	(
		[Sno] [int] IDENTITY(1,1),
		[BAD_ID] [int],
		[BAD_Reference_ID] [int],
		[BAD_Asset_Serial_No] [varchar] (50),
		[BAD_AAMS_Entity_Type] [int],
		[BAD_AAMS_Code] [varchar](12),
		[BAD_AAMS_Status] [int],
		[BAD_Verification_Status] [int],
		[BAD_Entity_Current_Status] [int],
		[BAD_Entity_Floor_Controller_Status] [bit],
		[BAD_Entity_Command] [varchar](12),
		[BAD_Is_Warehouse] [int],
		[BAD_Updated_Date] [datetime],
		[BAD_Comments] [varchar](100),
		[BAD_Game_Name] [varchar](100)
	)

	CREATE TABLE #Installation_Game_Info
	(
		[Sno] [int] IDENTITY(1,1),
		IGI_ID INT,
		Installation_No		INT,
		Game_Position		INT,
		Max_Bet				INT,
		Prog_Group			INT,
		Prog_Level			INT,
		Game_Name			VARCHAR(100),
		Paytables			VARCHAR(1000),
		IsAvailable			BIT,
		[Game_Verification] [int],
		[Game_Current_Status] [int],
		[Game_AAMS_Status] [int],
		[Game_Floor_Controller_Status] [bit],
		[Game_Entity_Command] [varchar](12),
		[Game_Comments] [varchar](100),
		HQ_IGI_ID INT
	)

	SELECT @DBName = Setting_Value FROM dbo.Setting WHERE Setting_Name = 'LGEDatabase'

	IF @DBName IS NULL
	BEGIN
		RETURN
	END 

	--Truncate the LGE data table.
	TRUNCATE TABLE dbo.LGE_VLT_Data

	SET @SQLQuery = 'INSERT INTO dbo.LGE_VLT_Data(strManufacturerID, strSerial, strMacAddress, iPltID, GameTheme, GamePart, 
		GameStatus, GameVerifyDate, OS, OsStatus, OsVerifyDate, OldestTime, bEnrolled, dtUpdated, [State], IsCurrent)  '  
 
	IF EXISTS (SELECT 1 FROM SYS.SERVERS WHERE NAME = 'LGE')
	BEGIN
		IF NOT EXISTS(SELECT 1 FROM SYS.SERVERS WHERE Data_Source = HOST_NAME() AND NAME = 'LGE')
		BEGIN
			SET @SQLQuery = @SQLQuery + 'EXEC [LGE].['
		END
		ELSE
		BEGIN
            SET @SQLQuery = @SQLQuery + 'EXEC ['
		END
	END
	ELSE
	BEGIN
            SET @SQLQuery = @SQLQuery + 'EXEC ['
	END

	SET @SQLQuery = @SQLQuery + @DBName + '].[dbo].p_BMCGetVltData'

	EXEC SP_EXECUTESQL @SQLQuery

	--VLT Changes Start.
	--Update VLT OS Verification - PASS/FAIL/UNKNOWN
--	UPDATE BMC_AAMS_Details
--	SET BAD_Verification_Status = (CASE LVD.OsStatus WHEN 'PASS' THEN 1 ELSE 0 END) 
--	OUTPUT INSERTED.* 
--		INTO #BMC_AAMS_Details
--	FROM BMC_AAMS_Details INNER JOIN LGE_VLT_Data LVD ON BAD_Asset_Serial_No = LVD.strSerial
--	WHERE BAD_AAMS_Entity_Type = 3 AND 
--	BAD_Verification_Status <> (CASE LVD.OsStatus WHEN 'PASS' THEN 1 WHEN 'pass' THEN 1 ELSE 0 END)
--	
--	SELECT @Count = COUNT(Sno) FROM #BMC_AAMS_Details
--
--	WHILE @Sno <= @Count
--	BEGIN
--
--		SELECT @iID = BAD_ID, @iRefID = BAD_Reference_ID 
--		FROM #BMC_AAMS_Details WHERE BAD_AAMS_Entity_Type = 3 AND Sno = @Sno
--
--		IF (@iID IS NOT NULL)
--		BEGIN
--			SELECT @SiteCode = S.Site_Code FROM dbo.Installation I
--			INNER JOIN dbo.Machine M ON I.Machine_ID = M.Machine_ID
--			INNER JOIN dbo.Bar_Position BP ON I.Bar_Position_ID = BP.Bar_Position_ID
--			INNER JOIN dbo.Site S ON S.Site_ID = BP.Site_ID
--			WHERE M.Machine_ID = @iRefID AND I.Installation_End_Date IS NULL
--			
--			IF ISNULL(@SiteCode, '') <> ''
--			BEGIN
--				EXEC dbo.usp_InsertAAMSEHRecord @iID, @SiteCode
--			END
--		END
--
--		SET @Sno = @Sno + 1
--
--	END	

	--VLT Changes End.

	--Game Changes Start.
	--Insert Game Verification - PASS/FAIL/UNKNOWN
--	UPDATE IGI
--	SET Game_Verification = (CASE LVD.GameStatus WHEN 'PASS' THEN 1 WHEN 'pass' THEN 1 ELSE 0 END)
--	OUTPUT INSERTED.IGI_ID, INSERTED.Installation_No, INSERTED.Game_Position, INSERTED.Max_Bet, INSERTED.Prog_Group, INSERTED.Prog_Level, INSERTED.Game_Name, 
--		INSERTED.Paytables, INSERTED.IsAvailable, INSERTED.Game_Verification, INSERTED.Game_Current_Status, INSERTED.Game_AAMS_Status, 
--		INSERTED.Game_Floor_Controller_Status, INSERTED.Game_Entity_Command, INSERTED.Game_Comments, INSERTED.HQ_IGI_ID
--	INTO #Installation_Game_Info	
--	FROM Installation_Game_Info IGI INNER JOIN Installation I ON IGI.Installation_No = I.Installation_ID
--	INNER JOIN Machine M ON I.Machine_ID = M.Machine_ID INNER JOIN LGE_VLT_Data LVD 
--	ON M.Machine_Manufacturers_Serial_No = LVD.strSerial --AND IGI.Game_Part_Number = LVD.GamePart	
--	WHERE Game_Verification <> (CASE LVD.GameStatus WHEN 'PASS' THEN 1 WHEN 'pass' THEN 1 ELSE 0 END)
--	AND I.Installation_End_Date IS NULL AND IGI.IsAvailable = 1
--
--	SELECT @Count = COUNT(Sno) FROM #Installation_Game_Info
--	SET @Sno = 1
--	DECLARE @HQ_IGI_ID INT
--	WHILE @Sno <= @Count
--	BEGIN
--
--		SELECT @iID = Installation_No, @HQ_IGI_ID = HQ_IGI_ID FROM dbo.#Installation_Game_Info WHERE Sno = @Sno
--
--		IF (@iID IS NOT NULL)
--		BEGIN
--				SELECT @SiteCode = S.Site_Code FROM dbo.Installation I
--				INNER JOIN dbo.Bar_Position BP ON I.Bar_Position_ID = BP.Bar_Position_ID
--				INNER JOIN dbo.Site S ON S.Site_ID = BP.Site_ID
--				WHERE I.Installation_ID = @iID --AND I.Installation_End_Date IS NULL
--				
--				IF ISNULL(@SiteCode, '') <> ''
--				BEGIN
--				    INSERT INTO Export_History (EH_Date, EH_Reference1, EH_Type, EH_Site_Code)  
--					VALUES(GETDATE(), @HQ_IGI_ID, 'GAMEINFO', @SiteCode)  
--				END
--		END
--
--		SET @Sno = @Sno + 1
--
--	END	

	--Game Changes End.
	 DECLARE @MachineData TABLE(  
	 Machine_ID int,  
	 Machine_Manufacturers_Serial_No varchar(50),  
	 Machine_MAC_Address varchar(17) )  

	-- MAC Addr Changes Start.
	-- Store Machine data for MAC Address comparison
	INSERT INTO @MachineData (Machine_ID,Machine_Manufacturers_Serial_No,Machine_MAC_Address)
	SELECT Machine_ID,Machine_Manufacturers_Serial_No,Machine_MAC_Address
	FROM Machine
	INNER JOIN LGE_VLT_DATA LGE ON Machine_Manufacturers_Serial_No = LGE.strSerial
	WHERE ISNULL(Machine_MAC_Address,'') <> ISNULL(LGE.strMacAddress, '')

	--Update MAC Address in BMC
	UPDATE	Machine 
	SET		Machine_MAC_Address_Prev = Machine_MAC_Address,
			Machine_MAC_Address = LGE.strMacAddress 
	FROM Machine M
	INNER JOIN LGE_VLT_DATA LGE ON M.Machine_Manufacturers_Serial_No = LGE.strSerial
	INNER JOIN MACHINE_CLASS MC ON M.Machine_Class_ID = MC.Machine_Class_ID
	INNER JOIN Machine_Type MT ON MC.Machine_Type_ID = MT.Machine_Type_ID
	WHERE ISNULL(M.Machine_MAC_Address,'') <> ISNULL(LGE.strMacAddress, '') 
	AND ISNULL(MT.IsNonGamingAssetType,0) <> 1

	--Populate BMC_BAS_Export_History
	INSERT INTO BMC_BAS_Export_History 
				(BBEH_Reference,BBEH_AAMS_Entity_Type,BBEH_Message_Type
				,BBEH_Status,BBEH_BAS_Message_ID,BBEH_Received_Date,BBEH_Exported_Date
				,BBEH_Comments,BBEH_AAMS_Approval,BBEH_Session_Status,BBEH_Financial_Data, 
				BBEH_Process_Type, BBEH_Process_Type_Comments)

	SELECT		M.Machine_ID ,3,308,
				0,NULL,getdate(),NULL,
				NULL,NULL,NULL,NULL,0, 'Mac Adress Change.'
	FROM @MachineData tempM
	INNER JOIN Machine M ON tempM.Machine_ID = M.Machine_ID
	INNER JOIN MACHINE_CLASS MC ON M.Machine_Class_ID = MC.Machine_Class_ID
	INNER JOIN Machine_Type MT ON MC.Machine_Type_ID = MT.Machine_Type_ID
	WHERE tempM.Machine_MAC_Address IS NOT NULL AND ISNULL(MT.IsNonGamingAssetType,0) <> 1
	-- MAC Addr Changes end.

END


GO

