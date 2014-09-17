USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_ExportVerifyOS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_ExportVerifyOS]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

-- =================================================================  
-- rsp_ExportVerificationGamesOS
-- -----------------------------------------------------------------  
--  
-- Exports the details to LGE for verification of OS/Game
--   
-- -----------------------------------------------------------------      
-- Revision History         
--         
-- 24/11/09 Created Created        
-- =================================================================   
CREATE PROCEDURE [dbo].[rsp_ExportVerifyOS]
	@EH_ID	INT,
	@Ref_ID	VARCHAR(50),
	@pAAMSTransactionId	VARCHAR(20),
	@Success	INT	OUTPUT
AS
BEGIN
	DECLARE @ExSerial VARCHAR(50)
	DECLARE @Count INT
	DECLARE @Current INT
	DECLARE @Types TABLE
	(
	ID INT IDENTITY(1,1),
	CompType INT)

	SET @Success = 0
	SET @Current = 1

	SELECT @ExSerial = BAD_Asset_Serial_No
	FROM BMC_AAMS_Details
	WHERE BAD_AAMS_Code = @Ref_ID AND BAD_AAMS_Entity_Type = 3

	INSERT INTO @Types
	SELECT CVMCD_CCT_Code FROM dbo.CV_Machine_Component_Details	
	WHERE CVMCD_Machine_Serial_No = @ExSerial AND CVMCD_IsAvailable = 1

	SELECT @Count = COUNT(*) FROM @Types	

	WHILE(@Current <= @Count)
	BEGIN
		DECLARE @Type INT		
		SELECT @Type = CompType FROM @Types WHERE ID = @Current

		EXEC dbo.usp_InsertComponentVerificationRecord @ExSerial, @Type, 5
	
		SET @Current = @Current + 1
	END

	UPDATE dbo.LGE_Export_History 
		SET LGE_EH_Status = 50,
			LGE_EH_Export_Date = GETDATE()
	WHERE LGE_EH_ID = @EH_ID

END


GO

