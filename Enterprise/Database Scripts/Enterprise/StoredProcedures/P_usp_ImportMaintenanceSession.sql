USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_ImportMaintenanceSession]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_ImportMaintenanceSession]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[usp_ImportMaintenanceSession]   
@doc xml   
AS  
BEGIN  
	    
	DECLARE @MachineSerialNo VARCHAR(50)    
	DECLARE @MachineStatusFlag INT    
	DECLARE @docHandle INT    
	DECLARE @MachineStatusFlagCurr INT   
	DECLARE @handle INT
	CREATE TABLE #Temp (ID INT,
						Installation_No INT,
						isAuthorized BIT,
						CreatedBy INT,
						CreatedOn DATETIME,
						ClosedBy INT,
						ClosedOn DATETIME,
						IsSessionOpen BIT,
						Code VARCHAR(200))
	
	EXEC sp_xml_preparedocument @handle OUTPUT, @doc  

	SELECT * INTO #MS
	FROM OPENXML (@handle, './MaintenanceSessions/MaintenanceSession',2)  
    WITH #Temp

	EXEC sp_xml_removedocument @handle

	IF NOT EXISTS(SELECT MS.ID FROM MaintenanceSession MS INNER JOIN #MS Temp_MS ON MS.ID = Temp_MS.ID  
       AND MS.Site_ID = (SELECT Site_ID FROM [Site] WHERE Site_Code = Temp_MS.Code))    
	BEGIN
	
		INSERT INTO MaintenanceSession	
		SELECT	ID,Installation_No,isAuthorized,CreatedBy,
				CreatedOn,ClosedBy,ClosedOn,IsSessionOpen,
				(SELECT Site_ID FROM [Site] WHERE Site_Code = Code) AS Site_ID
		FROM #MS

		SET @MachineStatusFlag = 3
	END
	ELSE
	BEGIN
		UPDATE MS 
		SET MS.Installation_No = Temp_MS.Installation_No,
			MS.isAuthorized = Temp_MS.isAuthorized,
			MS.CreatedBy = Temp_MS.CreatedBy ,
			MS.CreatedOn = Temp_MS.CreatedOn,
			MS.ClosedBy = Temp_MS.ClosedBy ,
			MS.ClosedOn = Temp_MS.ClosedOn ,
			MS.IsSessionOpen = Temp_MS.IsSessionOpen 
		FROM MaintenanceSession MS 
		INNER JOIN #MS Temp_MS
		ON MS.ID = Temp_MS.ID
		AND MS.Site_ID = (SELECT Site_ID FROM [Site] WHERE Site_Code = Temp_MS.Code)

		SET @MachineStatusFlag = 1
	END

	SELECT @MachineStatusFlagCurr = Machine_Status_Flag,@MachineSerialNo = Machine_Manufacturers_Serial_No 
	FROM  machine
	INNER JOIN installation ON installation.Machine_ID = machine.Machine_ID
	INNER JOIN #MS ON #MS.Installation_No = installation.Installation_ID 
/*****************************************************************************************************
* (CR# 191254 : EBS Communication Service) - MODIFIED BY Venkatesh.H  (START)
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

	IF @MachineStatusFlag = 3
	BEGIN
		UPDATE dbo.Machine     
		Set Machine_Status_Flag = @MachineStatusFlag,  
		Machine_Status_Flag_Prev = 1 
		OUTPUT INSERTED.Machine_ID,
               DELETED.Machine_Status_Flag, INSERTED.Machine_Status_Flag,
               DELETED.Machine_Class_ID, INSERTED.Machine_Class_ID,
               DELETED.CMPGameType, INSERTED.CMPGameType, 
               DELETED.Machine_Stock_No, INSERTED.Machine_Stock_No
        INTO @_Modified

		WHERE Machine_Manufacturers_Serial_No = @MachineSerialNo  
	END
	ELSE
	BEGIN
		UPDATE dbo.Machine     
		Set Machine_Status_Flag = @MachineStatusFlag,  
		Machine_Status_Flag_Prev = 3 
		OUTPUT INSERTED.Machine_ID,
               DELETED.Machine_Status_Flag, INSERTED.Machine_Status_Flag,
               DELETED.Machine_Class_ID, INSERTED.Machine_Class_ID,
               DELETED.CMPGameType, INSERTED.CMPGameType, 
               DELETED.Machine_Stock_No, INSERTED.Machine_Stock_No
        INTO @_Modified
		WHERE Machine_Manufacturers_Serial_No = @MachineSerialNo  
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
                DECLARE @Machine_ID INT
                SELECT @Machine_ID = MachineId From @_Modified m
                                WHERE  m.FlagChanged = 1 OR
                                                                m.GameIDChanged = 1 OR
                                                                m.CMPGameTypeChanged = 1 OR
                                                                m.StockNoChanged = 1
                                EXEC [dbo].[usp_EBS_UpdateMachineDetails] @Machine_ID 
                END

/*****************************************************************************************************
* (CR# 191254 : EBS Communication Service) - MODIFIED BY Venkatesh.H  (END)
*****************************************************************************************************/

	 IF EXISTS (SELECT 1 FROM #MS WHERE isAuthorized = 1)
	 BEGIN
		 --Insert 306 message.      
		 INSERT INTO BMC_BAS_Export_History(BBEH_Reference, BBEH_AAMS_Entity_Type, BBEH_Message_Type,       
		 BBEH_Status, BBEH_Received_Date, BBEH_BAS_Message_ID, BBEH_Process_Type, BBEH_Session_Status)        
		 SELECT M.Machine_ID, 3, 306 ,0, GETDATE(), null,   
		 CASE ISNULL(M.Machine_Status_Flag,0)  
		 WHEN 3 THEN 3  
		 ELSE 5
		 END, 'Initiated'      
		 FROM dbo.Machine M WHERE M.Machine_Manufacturers_Serial_No = @MachineSerialNo  
	 END

END

GO

