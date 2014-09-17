USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[usp_ImportGameCappingDetailsFromXML]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[usp_ImportGameCappingDetailsFromXML]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_ImportGameCappingDetailsFromXML]
	@XML VARCHAR(MAX),
	@IsSuccess INT OUTPUT
AS
BEGIN
	DECLARE @idoc INT  
	
	SET @IsSuccess = 0  
	
	EXEC sp_xml_preparedocument @idoc OUTPUT,
	     @XML  
	
	DECLARE @GameCappingId INT
	
	SET @GameCappingId = 0
	
	SELECT @GameCappingId = GameCappingID
	FROM   OPENXML(@idoc, './/GameCapping/GameCappingDetails', 2) WITH 
	       (GameCappingId INT './HQ_GameCappingID')
	
	IF @GameCappingId = 0
	BEGIN
	    INSERT INTO dbo.GameCappingDetails
	      (
	        Slot,
	        Stand,
	        InstallationNo,
	        ReservedBy,
	        ReservedCardNo,
	        SessionStartTime,
	        SessionEndTime,
	        TotalTimeReserved,
	        ReservedFor,
	        ReservedForCardNo,	       
	        ReleasedBy,
	        ReleasedCardNo,
	        Site_Code
	      )
	    SELECT *
	    FROM   OPENXML(@idoc, './/GameCapping/GameCappingDetails', 2) 
	           WITH 
	           (
	               Slot VARCHAR(30) './Slot',
	               Stand VARCHAR(30) './Stand',
	               InstallationNo INT './InstallationNo',
	               ReservedBy VARCHAR(30)'./ReservedBy',
	               ReservedCardNo VARCHAR(20)'./ReservedCardNo',
	               SessionStartTime DATETIME './SessionStartTime',
	               SessionEndTime DATETIME './SessionEndTime',
	               TotalTimeReserved VARCHAR(30) './TotalTimeReserved',
	               ReservedFor VARCHAR(36) './ReservedFor',
	               ReservedForCardNo VARCHAR(30) './ReservedForCardNo',	               
	               ReleasedBy VARCHAR(200) './ReleasedBy',
	               ReleasedCardNo VARCHAR(30) './ReleasedCardNo',
	               Site_Code VARCHAR(30) './Site_Code'
	           )  
	    
	    IF @@Error <> 0
	    BEGIN
	        SET @IsSuccess = -1 -- failed while updating the records in the GameCappingDetails table
	        RETURN
	    END
	    ELSE
	    BEGIN
			SET @IsSuccess = SCOPE_IDENTITY()
	    END    
	END
	ELSE
	BEGIN
	    UPDATE GCD
	    SET    GCD.SessionEndTime = A.SessionEndTime,
	           GCD.TotalTimeReserved = A.TotalTimeReserved,	           	          
	           GCD.ReleasedBy = A.ReleasedBy,
	           GCD.ReleasedCardNo = A.ReleasedCardNo	           
	    FROM   GameCappingDetails GCD
	           INNER JOIN OPENXML(@idoc, './/GameCapping/GameCappingDetails', 2) 
	                WITH 
	                (
	                	SessionEndTime DATETIME './SessionEndTime',
	                    TotalTimeReserved VARCHAR(30) './TotalTimeReserved',
	                    ReleasedBy VARCHAR(200) './ReleasedBy',
	                    ReleasedCardNo VARCHAR(30) './ReleasedCardNo'
	                )A
	                ON  GCD.GameCappingID = @GameCappingId
	    
	    IF @@Error <> 0
		BEGIN
			SET @IsSuccess = -1 -- failed while updating the records in the GameCappingDetails table
		END
		ELSE
		BEGIN
			SET @IsSuccess = @GameCappingId
		END
	END
	EXEC sp_xml_removedocument @idoc
END
GO