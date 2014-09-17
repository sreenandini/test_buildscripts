USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_ExportVerifyGame]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_ExportVerifyGame]
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
CREATE PROCEDURE [dbo].[rsp_ExportVerifyGame]
	@EH_ID	INT,
	@Ref_ID	VARCHAR(50),
	@pAAMSTransactionId	VARCHAR(20),
	@MessageReference VARCHAR(50),
	@Success	INT	OUTPUT
AS
BEGIN
	DECLARE @ExSerial VARCHAR(50)

	SET @Success = 0

	SELECT @ExSerial = BAD_Asset_Serial_No
	FROM BMC_AAMS_Details
	WHERE BAD_AAMS_Code = @MessageReference AND BAD_AAMS_Entity_Type = 3

	EXEC dbo.usp_InsertComponentVerificationRecord @ExSerial, 2, 6

	UPDATE dbo.LGE_Export_History 
		SET LGE_EH_Status = 50,
			LGE_EH_Export_Date = GETDATE()
	WHERE LGE_EH_ID = @EH_ID

END


GO

