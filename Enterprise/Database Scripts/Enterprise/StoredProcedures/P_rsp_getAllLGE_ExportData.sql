USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_getAllLGE_ExportData]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_getAllLGE_ExportData]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--------------------------------------------------------------------------                       
--                      
-- Description: Gets All Unprocessed Data from the LGE_Export history table.                
--              Rule/Condition: All records that Have IH_Status = 0 would                
--              be listed.  After Listing Updated EH_Status = 1 to Mark          
--              it as Inprogress Records.  So that the same record is not           
--              picked for processing.          
--                      
-- Inputs:      NONE                
--                      
-- Outputs:     Displays the list of Records from the Import_History Table                      
--                      
-- RETURN:      NONE                
--                      
-- =======================================================================                      
--                       
-- Revision History                      
--                       
-- Sudarsan S     21/11/2009     Created
---------------------------------------------------------------------------                
                
CREATE PROCEDURE [dbo].[rsp_getAllLGE_ExportData]
AS                
BEGIN              

DECLARE @Count	INT
DECLARE @Start	INT
DECLARE @EH_ID	INT
DECLARE @Ref	VARCHAR(50)
DECLARE @Add_Remove	BIT
DECLARE @Type	VARCHAR(100)
DECLARE @Return	INT
DECLARE @AAMSTransactionId	VARCHAR(20)
DECLARE @MessageReference	VARCHAR(50)

SET @Start = 1
            
CREATE TABLE #Temp
	(
		Sno		INT IDENTITY(1,1),
		EH_ID	INT,
		EH_Reference	VARCHAR(50),
		Add_Remove		BIT,
		EH_Type			VARCHAR(50),
		LGE_EH_AAMS_Message_ID	VARCHAR(20),
		LGE_EH_Message_Reference VARCHAR(50)
	)

	INSERT INTO #Temp
	SELECT TOP 200 LGE_EH_ID, LGE_EH_Reference, LGE_Add_Remove, LGE_EH_Type, LGE_EH_AAMS_Message_ID, LGE_EH_Message_Reference FROM dbo.LGE_Export_History
			WHERE ISNULL(LGE_EH_Status, 0) IN (0, -1) ORDER BY LGE_EH_ID            
            
	SELECT @Count = COUNT(*) FROM #Temp

	WHILE @Start <= @Count
	BEGIN
		SELECT @EH_ID = EH_ID,
				@Ref = EH_Reference,
				@Add_Remove = Add_Remove,
				@Type = EH_Type,
				@AAMSTransactionId = LGE_EH_AAMS_Message_ID,
				@MessageReference = LGE_EH_Message_Reference
			FROM #Temp WHERE Sno = @Start

		IF @Type = 'AUTOINSTALLATION'
		BEGIN
			DECLARE @RefID INT
			SET @RefID = CAST(@Ref AS INT)
			EXEC dbo.rsp_ExportLGEVLTDetails @EH_ID, @RefID, @Return OUTPUT
		END
		ELSE IF @Type = 'GAME'
			EXEC dbo.rsp_ExportLGEGameDetails @EH_ID, @Ref, @Return OUTPUT
		ELSE IF @Type = 'VERIFYOS'
			EXEC dbo.rsp_ExportVerifyOS @EH_ID, @Ref, @AAMSTransactionId, @Return OUTPUT
		ELSE IF @Type = 'VERIFYGAME'
			EXEC dbo.rsp_ExportVerifyGame @EH_ID, @Ref, @AAMSTransactionId, @MessageReference, @Return OUTPUT

	-- if Insert fails in the LGE database, we update the record to -1 in LGE_Export_History, but do not
	-- stop with that. make the job to fail and not proceed with the next record.
	IF ISNULL(@Return, 0) < 0
		SELECT CAST('XYZ' AS DATETIME)

	SET @Start = @Start + 1

	END
              
END        


GO

