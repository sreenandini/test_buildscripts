/************************************************************
 * Code formatted by SoftTree SQL Assistant © v4.8.29
 * Time: 03/05/2014 11:48:05 PM
 ************************************************************/

USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[usp_UpdateAAMSRequestStatus]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[usp_UpdateAAMSRequestStatus]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =================================================================  
-- usp_UpdateAAMSRequestStatus  
-- -----------------------------------------------------------------  
--  
-- Updates AAMS Request Status.  
--   
-- -----------------------------------------------------------------      
-- Revision History         
--         
-- 02/12/09 Renjish Created        
-- =================================================================   

CREATE PROCEDURE dbo.usp_UpdateAAMSRequestStatus
	@MessageID VARCHAR(20),
	@Status INT
AS
	DECLARE @AAMS_Entity_Type  INT
	DECLARE @Reference         INT
	DECLARE @Machine_Status    INT
	DECLARE @Message_Type      INT
	DECLARE @Process_Type      INT
	
	SELECT @AAMS_Entity_Type = BBEH_AAMS_Entity_Type,
	       @Reference = CAST(BBEH_Reference AS INT),
	       @Message_Type = BBEH_Message_Type,
	       @Process_Type = ISNULL(BBEH_Process_Type, 0)
	FROM   BMC_BAS_Export_History
	WHERE  BBEH_BAS_Message_ID = @MessageID 
	
	UPDATE BMC_BAS_Export_History
	SET    BBEH_AAMS_Approval = @Status,
	       BBEH_Session_Status = 'Completed'
	WHERE  BBEH_BAS_Message_ID = @MessageID
	
	--Update Game Details.
	IF @Process_Type = 2
	BEGIN
	    --Update Game Enable/Disable Status
	    UPDATE Installation_Game_Info
	    SET    Game_Enable_AAMS_Status = @Status,
	           Game_Comments = 'Game Enable/Disable AAMS Reply.' + CAST(@Status AS VARCHAR(5))
	    WHERE  @AAMS_Entity_Type = 4
	           AND HQ_IGI_ID = @Reference
	END
	ELSE
	BEGIN
	    --Update Game Install/Remove Status
	    UPDATE Installation_Game_Info
	    SET    Game_AAMS_Status = @Status,
	           Game_Comments = 'Game Install/Remove AAMS Reply.' + CAST(@Status AS VARCHAR(5))
	    WHERE  @AAMS_Entity_Type = 4
	           AND HQ_IGI_ID = @Reference
	END
	
	/*****************************************************************************************************
	* (CR# 191254 : EBS Communication Service) - MODIFIED BY A.VINOD KUMAR (START)
	*****************************************************************************************************/

	DECLARE @_Modified TABLE (
		MachineId INT,
		OldFlag INT, NewFlag INT,
		OldGameID INT, NewGameID INT,
		OldCMPGameType varchar(50), NewCMPGameType varchar(50),
		OldStockNo varchar(50), NewStockNo varchar(50),
		FlagChanged AS (CASE WHEN OldFlag = NewFlag THEN 0 ELSE 1 END),
		GameIDChanged AS (CASE WHEN OldGameID = NewGameID THEN 0 ELSE 1 END),	
		CMPGameTypeChanged AS (CASE WHEN OldCMPGameType = NewCMPGameType THEN 0 ELSE 1 END),
		StockNoChanged AS (CASE WHEN OldStockNo = NewStockNo THEN 0 ELSE 1 END)
	)
	
	IF @AAMS_Entity_Type = 3
	   AND @Process_Type IN (0, 4, 7, 9)
	BEGIN
	    --Update VLT Details.
	    UPDATE BMC_AAMS_Details
	    SET    BAD_AAMS_Status = @Status--,
	                                    --BAD_Entity_Command = CASE @Status WHEN 1 THEN 'Enabled' ELSE 'Disabled' END
	    WHERE  BAD_AAMS_Entity_Type = @AAMS_Entity_Type
	           AND BAD_Reference_ID = @Reference
	    
	    SELECT @Machine_Status = Machine_Status_Flag
	    FROM   MACHINE
	    WHERE  Machine_ID = @Reference
	    
	    IF @Status = 0
	    BEGIN
	        --Status AAMS Rejected.
	        IF @Machine_Status = 8
	           AND (
	                   @Message_Type = 303
	                   OR @Message_Type = 304
	                   OR @Message_Type = 305
	                   OR @Message_Type = 306
	               )
	        BEGIN
	            UPDATE MACHINE
	            SET    Machine_Status_Flag = 9
				OUTPUT INSERTED.Machine_ID,
					DELETED.Machine_Status_Flag, INSERTED.Machine_Status_Flag,
					DELETED.Machine_Class_ID, INSERTED.Machine_Class_ID,
					DELETED.CMPGameType, INSERTED.CMPGameType, 
					DELETED.Machine_Stock_No, INSERTED.Machine_Stock_No
				INTO @_Modified
	            WHERE  Machine_ID = @Reference
	        END
	        --		IF @Machine_Status = 10 AND (@Message_Type = 304 OR @Message_Type = 306)
	        --		BEGIN
	        --			UPDATE Machine SET Machine_Status_Flag = 11 WHERE Machine_ID = @Reference
	        --		END
	        IF @Machine_Status = 12
	           AND (@Message_Type = 307 OR @Message_Type = 309)
	        BEGIN
	            UPDATE MACHINE
	            SET    Machine_Status_Flag = 14
				OUTPUT INSERTED.Machine_ID,
					DELETED.Machine_Status_Flag, INSERTED.Machine_Status_Flag,
					DELETED.Machine_Class_ID, INSERTED.Machine_Class_ID,
					DELETED.CMPGameType, INSERTED.CMPGameType, 
					DELETED.Machine_Stock_No, INSERTED.Machine_Stock_No
				INTO @_Modified
	            WHERE  Machine_ID = @Reference
	        END
	    END
	    ELSE
	    BEGIN
	        IF (@Machine_Status = 8 OR @Machine_Status = 9)
	           AND (
	                   @Message_Type = 303
	                   OR @Message_Type = 304
	                   OR @Message_Type = 305
	                   OR @Message_Type = 306
	               )
	        BEGIN
	            --Status AAMS Approved i.e. In Use.
	            UPDATE MACHINE
	            SET    Machine_Status_Flag = 1
				OUTPUT INSERTED.Machine_ID,
					DELETED.Machine_Status_Flag, INSERTED.Machine_Status_Flag,
					DELETED.Machine_Class_ID, INSERTED.Machine_Class_ID,
					DELETED.CMPGameType, INSERTED.CMPGameType, 
					DELETED.Machine_Stock_No, INSERTED.Machine_Stock_No
				INTO @_Modified
	            WHERE  Machine_ID = @Reference
	        END
	        
	        IF (@Machine_Status = 10 OR @Machine_Status = 11)
	           AND (@Message_Type = 304 OR @Message_Type = 306)
	        BEGIN
	            --Status AAMS Approved i.e. In Stock.
	            UPDATE MACHINE
	            SET    Machine_Status_Flag = 0
				OUTPUT INSERTED.Machine_ID,
					DELETED.Machine_Status_Flag, INSERTED.Machine_Status_Flag,
					DELETED.Machine_Class_ID, INSERTED.Machine_Class_ID,
					DELETED.CMPGameType, INSERTED.CMPGameType, 
					DELETED.Machine_Stock_No, INSERTED.Machine_Stock_No
				INTO @_Modified
	            WHERE  Machine_ID = @Reference
	        END
	        
	        IF (@Machine_Status = 12 OR @Machine_Status = 14)
	           AND (@Message_Type = 307 OR @Message_Type = 309)
	        BEGIN
	            --Status AAMS Approved i.e. Terminated.
	            UPDATE MACHINE
	            SET    Machine_Status_Flag = 13
				OUTPUT INSERTED.Machine_ID,
					DELETED.Machine_Status_Flag, INSERTED.Machine_Status_Flag,
					DELETED.Machine_Class_ID, INSERTED.Machine_Class_ID,
					DELETED.CMPGameType, INSERTED.CMPGameType, 
					DELETED.Machine_Stock_No, INSERTED.Machine_Stock_No
				INTO @_Modified
	            WHERE  Machine_ID = @Reference
	        END
	    END
	END
   
	IF EXISTS(
		SELECT 1
		FROM   @_Modified m
		WHERE  m.FlagChanged = 1 OR
				m.GameIDChanged = 1 OR
				m.CMPGameTypeChanged = 1 OR
				m.StockNoChanged = 1
	)
	BEGIN
		EXEC [dbo].[usp_EBS_UpdateMachineDetails] @Reference 
	END

	/*****************************************************************************************************
	* (CR# 191254 : EBS Communication Service) - MODIFIED BY A.VINOD KUMAR (END)
	*****************************************************************************************************/
GO

