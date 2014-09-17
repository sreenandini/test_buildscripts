USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_ExportLGEGameDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_ExportLGEGameDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- =================================================================  
-- rsp_ExportLGEGameDetails  
-- -----------------------------------------------------------------  
--  
-- Exports the Addition/Removal of Game 
--   
-- -----------------------------------------------------------------      
-- Revision History         
--         
-- 21/11/09 Created Created        
-- =================================================================   
CREATE PROCEDURE [dbo].[rsp_ExportLGEGameDetails]
	@EH_ID	INT,
	@Add_Remove	BIT,
	@Game_ID	INT,
	@Success	INT	OUTPUT
AS
BEGIN
	DECLARE @ManufacturerID CHAR(8),
			@Serial VARCHAR(12),
			@InstallationDate DATETIME,
			@Game_Name	VARCHAR(100),
			@MG_Game_ID	INT,
			@Return		INT,
			@DBName		VARCHAR(100),
			@SQLQuery	NVARCHAR(1000)

	SET @Success = 0

	SELECT @ManufacturerID = Ma.Manufacturer_Name,
			@Serial = G.MG_Game_SerialNo,
			@InstallationDate = G.MG_Game_StartDate,
			@Game_Name = G.MG_Game_Name,
			@MG_Game_ID = G.MG_Game_ID
	  FROM dbo.Game_Library G
	 INNER JOIN Game_Title GT ON GT.Game_Title_ID = G.MG_Group_ID  
	 LEFT JOIN dbo.Manufacturer Ma ON GT.Manufacturer_ID = Ma.Manufacturer_ID
		 WHERE G.MG_Game_ID = @Game_ID-- AND BBEH_AAMS_Entity_Type = 4 AND BBEH_Status = 100


	SELECT @DBName = Setting_Value FROM dbo.Setting WHERE Setting_Name = 'LGEDatabase'

	IF @DBName IS NULL
	BEGIN
		SET @Success = -1
		RETURN
	END

--	SET @SQLQuery = 'EXEC [LGE].[' + @DBName + '].[dbo].[p_InsertUpdatePLTPreconfig] ''' + @ManufacturerID + ''',''' + 
--					@Serial + ''',''' + CONVERT(VARCHAR, @InstallationDate, 106) + ''',''' + @Game_Name + ''',' + 
--					CAST(@Game_Name AS VARCHAR(32)) + ',''' + ''',' + '''' +  ''',0,0,0,0,0,'''''
--
--	-- [LinkedServer].[Database].[Owner].[Procedure]
----	EXEC [LGE].[LGE].[dbo].[p_AAMSInsertRequest] @Command, @ManufacturerID, @Serial, @AAMSTransactionId, NULL, @Return OUTPUT
--
--	EXEC SP_EXECUTESQL @SQLQuery

	SET @Return = @@ERROR

	UPDATE dbo.LGE_Export_History 
		SET LGE_EH_Status = CASE WHEN ISNULL(@Return, 0) >= 0 THEN 100 ELSE -1 END,
			LGE_EH_Export_Date = GETDATE()
	WHERE LGE_EH_ID = @EH_ID

	IF ISNULL(@Return, 0) < 0
		SET @Success = -1

END


GO

