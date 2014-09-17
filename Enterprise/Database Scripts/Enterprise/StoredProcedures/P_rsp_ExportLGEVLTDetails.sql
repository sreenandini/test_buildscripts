USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_ExportLGEVLTDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_ExportLGEVLTDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- =================================================================  
-- rsp_ExportLGEVLTDetails  
-- -----------------------------------------------------------------  
--  
-- Exports the Addition/Removal of Installation 
--   
-- -----------------------------------------------------------------      
-- Revision History         
--         
-- 21/11/09 Sudarsan S Created        
-- =================================================================   
CREATE PROCEDURE [dbo].[rsp_ExportLGEVLTDetails]
	@EH_ID	INT,
	@Installation_ID	INT,
	@Success	INT	OUTPUT
AS
BEGIN
	DECLARE @ManufacturerID CHAR(8),
			@Serial VARCHAR(12),
			@InstallationDate DATETIME,
			@Machine_Name	VARCHAR(100),
			@Machine_ID	INT,
			@Location VARCHAR(32),
			@CabinetType VARCHAR(32),
			@GameTheme VARCHAR(32),
			@Return		INT,
			@DBName		VARCHAR(100),
			@SQLQuery	NVARCHAR(1000)

	SET @Success = 0

	SELECT @ManufacturerID = Ma.Manufacturer_Name,
			@Serial = M.Machine_Manufacturers_Serial_No,
			@InstallationDate = I.Installation_Start_Date,
			@Machine_Name = M.Machine_Stock_No,
			@Machine_ID = M.Machine_ID,
			@CabinetType = MT.Machine_Type_Code,
			@GameTheme = MC.Machine_Name,
			@Location = S.Site_Name
	  FROM dbo.Installation I
	INNER JOIN dbo.Machine M ON I.Machine_ID = M.Machine_ID
	INNER JOIN dbo.Bar_Position BP ON I.Bar_Position_ID = BP.Bar_Position_ID
	INNER JOIN dbo.Site S ON S.Site_ID = BP.Site_ID
	INNER JOIN dbo.Machine_Class MC ON M.Machine_Class_ID = MC.Machine_Class_ID
	INNER JOIN dbo.Machine_Type MT ON MT.Machine_Type_ID = MC.Machine_Type_ID
	 LEFT JOIN dbo.Manufacturer Ma ON MC.Manufacturer_ID = Ma.Manufacturer_ID
--	 LEFT JOIN dbo.BMC_BAS_Export_History BB ON I.Installation_ID = BB.BBEH_Reference 
		 WHERE I.Installation_ID = @Installation_ID --AND BBEH_AAMS_Entity_Type = 3 AND BBEH_Status = 100

	SELECT @DBName = Setting_Value FROM dbo.Setting WHERE Setting_Name = 'LGEDatabase'

	IF @DBName IS NULL
	BEGIN
		SET @Success = -1
		RETURN
	END

	IF EXISTS (SELECT 1 FROM SYS.SERVERS WHERE NAME = 'LGE')
	BEGIN
		IF NOT EXISTS(SELECT 1 FROM SYS.SERVERS WHERE Data_Source = HOST_NAME() AND NAME = 'LGE')
		BEGIN
			SET @SQLQuery = 'EXEC [LGE].['
		END
		ELSE
		BEGIN
            SET @SQLQuery = 'EXEC ['
		END
	END
	ELSE
	BEGIN
            SET @SQLQuery = 'EXEC ['
	END

	SET @SQLQuery = @SQLQuery + @DBName + '].[dbo].[p_InsertUpdatePLTPreconfig] ''' + @ManufacturerID + ''',''' + 
					@Serial + ''',''' + CONVERT(VARCHAR, @InstallationDate, 106) + ''',''' + @Machine_Name + ''',' + 
					CAST(@Machine_ID AS VARCHAR(10)) + ',''' + @Location + ''',''' + @CabinetType + ''',''' + CAST(@GameTheme AS VARCHAR(32)) + 
					''',0,0,0,0,'''''

--	EXEC [LGE].[LGE].[dbo].[p_InsertUpdatePLTPreconfig] @ManufacturerID, @Serial, @InstallationDate, @Machine_Name, 
--								@Machine_ID, @Location, @CabinetType, @GameThemeID, 0, 0, 0, 0, ''

	EXEC SP_EXECUTESQL @SQLQuery

	SET @Return = @@ERROR

	UPDATE dbo.LGE_Export_History 
		SET LGE_EH_Status = CASE WHEN ISNULL(@Return, 0) = 0 THEN 100 ELSE -1 END,
			LGE_EH_Export_Date = GETDATE()
	WHERE LGE_EH_ID = @EH_ID

	IF ISNULL(@Return, 0) <> 0
		SET @Success = -1

END


GO

