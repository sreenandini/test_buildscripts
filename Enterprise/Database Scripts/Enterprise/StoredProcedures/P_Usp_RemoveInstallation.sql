USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Usp_RemoveInstallation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Usp_RemoveInstallation]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--------------------------------------------------------------------------       
--      
-- Description: Remove an Installation    
--          
--    Steps      
--    1. expire the old machine      
--    2. expire the old installation      
--      
-- Inputs:      XML File Containing the following    
--    INSTALLATION  - Root Element    
--    HQInstallation_No - HQ Installation ID     
--    ENDDATE    - End Date for the Installation    
--    ENDTIME    - End Time for the Installation    
--      
-- Outputs:     NONE      
--      
-- Return:          See Comments    
--      
-- =======================================================================      
--       
-- Revision History      
--       
-- NaveenChander     15/05/2008     Created      
-- NaveenChander     22/07/2008     Changed Machine Stock Logic  
-- PoornaChander     26/07/2008     Changed Success code to 100  
-- Sudarsan S		 23/12/2009	    For removing from exchange
-- Renjish 		     29/12/2009	    Added logic for 306 AAMS Message.
-- Yoganandh P		 12/10/2010		Modified - Update Machine_Transit_Site_Code for 'Transit Machine' 
--									and Insert AAMS record only when the asset is not in transit state
-- GBabu			29/11/2010	Removed duplication of the service call closed
---------------------------------------------------------------------------       
CREATE PROCEDURE [dbo].[Usp_RemoveInstallation]( @XML nVARCHAR(4000)) As     
BEGIN    
 DECLARE @INT INT      
 DECLARE @Machine_ID INT    
 DECLARE @Installation_ID INT     
 DECLARE @ddmmmyyyy VARCHAR (20)    
 DECLARE @hhnnss VARCHAR (20)
 DECLARE @Machine_End_Date  VARCHAR(20)  
 DECLARE @Machine_Status_Flag Int  
 DECLARE @Staff_ID_Deleted Int 
 DECLARE @Staff_Name_Deleted VARCHAR(20)
 DECLARE @Machine_Date_Deleted VARCHAR(20)  
 DECLARE @Machine_Type_Of_Sale VARCHAR(20) 
 DECLARE @Machine_Transit_Site_Code VARCHAR(50) 
 DECLARE @HQ_IGI_ID INT  
    
 SET @INT  = 0      
 EXEC sp_xml_preparedocument @INT OUTPUT, @XML      
       
 SELECT * INTO #TEMP FROM OPENXML       
 (@INT, '/OLDINSTALLATION',2)      
 WITH    
  (HQInstallation_No INT,      
--   EndDate VARCHAR(20),    
   EndDate DATETIME,    
--   EndTime VARCHAR(20),  
-- Machine_End_Date  VARCHAR(20),  
 Machine_End_Date  DATETIME,  
  Machine_Status_Flag Int,  
  Staff_ID_Deleted VARCHAR(30),  
  Machine_Date_Deleted DATETIME,  
--  Machine_Date_Deleted VARCHAR(20),  
  Machine_Type_Of_Sale VARCHAR(20),
  MachineTransitSiteCode VARCHAR(50)
  )      
  
-- SELECT @Installation_ID = HQInstallation_No, @ddmmmyyyy = EndDate, @hhnnss = EndTime  FROM #TEMP    
 SELECT @Installation_ID = HQInstallation_No, @ddmmmyyyy = CONVERT(VARCHAR, EndDate, 106), @hhnnss = CONVERT(VARCHAR, EndDate, 108), 
	@Machine_Status_Flag = Machine_Status_Flag, @Machine_Transit_Site_Code = MachineTransitSiteCode
  FROM #TEMP    

 IF(@Machine_Transit_Site_Code = '')
	SELECT @Machine_Transit_Site_Code = NULL
    
 /* Returns -1 if Installation Does not Exists */    
 IF NOT EXISTS ( SELECT * FROM INSTALLATION I WHERE I.Installation_ID = @Installation_ID )    
 BEGIN    
  SELECT -1 AS RESULT    
  RETURN    
 END    
    
 /* Returns -2 if Site Does not Exists */    
 IF NOT EXISTS ( SELECT S.SITE_ID FROM BAR_POSITION B INNER JOIN SITE S ON S.SITE_ID = B.SITE_ID INNER JOIN INSTALLATION I ON I.Bar_Position_ID = B.Bar_Position_ID WHERE I.Installation_ID = @Installation_ID )    
 BEGIN    
  SELECT -2 AS RESULT    
  RETURN    
 END    
    
-- /* Returns -3 if Installation already Closed*/    
-- IF (SELECT LTRIM(RTRIM(ISNULL(Installation_End_Date,''))) FROM INSTALLATION WHERE Installation_ID = @Installation_ID   )  <> ''    
-- BEGIN    
--  SELECT -3 AS RESULT    
--  RETURN    
-- END    
  
  -- Expire the old Machine by Setting the Machine_status_Flag to 7 And create a new Machine Detail with Status Flag 0 to put Machine in STOCK  
-- SELECT @Machine_End_Date=Machine_End_Date,@Machine_Status_Flag=Machine_Status_Flag ,@Staff_ID_Deleted= Staff_ID_Deleted,@Machine_Date_Deleted=Machine_Date_Deleted,  @Machine_Type_Of_Sale=Machine_Type_Of_Sale
-- SELECT @Machine_End_Date=CONVERT(VARCHAR, Machine_End_Date, 106),@Machine_Status_Flag=Machine_Status_Flag ,@Staff_Name_Deleted= Staff_ID_Deleted,@Machine_Date_Deleted=CONVERT(VARCHAR, Machine_Date_Deleted, 106),  @Machine_Type_Of_Sale=Machine_Type_Of_Sale
--	FROM #TEMP

--	SELECT @Staff_ID_Deleted = S.Staff_ID FROM dbo.Staff S 
--	INNER JOIN dbo.[User] U ON S.UserTableID = U.SecurityUserID WHERE UserName = @Staff_Name_Deleted
-- 
--   UPDATE M     
--   SET     
--     
--  M.Machine_End_Date	= @Machine_End_Date,  
--  M.Machine_Status_Flag = @Machine_Status_Flag,  
--  M.Staff_ID_Deleted	= @Staff_ID_Deleted,  
--  M.Machine_Date_Deleted = @Machine_End_Date,  
--  M.Machine_Type_Of_Sale =@Machine_Type_Of_Sale       
--  FROM Machine M    
--  INNER JOIN INSTALLATION I ON I.Machine_ID = M.Machine_ID    
--    WHERE I.Installation_ID = @Installation_ID    
  
Select @Machine_ID = M.Machine_ID FROM Machine M    
  INNER JOIN INSTALLATION I ON I.Machine_ID = M.Machine_ID    
    WHERE I.Installation_ID = @Installation_ID   
  
-- INSERT INTO Machine             
--  (            
--    [Machine_Class_ID],            
--    [Operator_ID],            
--    [Terms_Profile_ID],            
--    [Depreciation_Policy_ID],            
--    [Depreciation_Policy_Use_Default],              
--    [Machine_Number_Of_Discs],            
--    [Machine_Stock_No],            
--    [Machine_Counter_Cash_In_Units],            
--    [Machine_Counter_Cash_Out_Units],            
--    [Machine_Counter_Tokens_In_Units],            
--    [Machine_Counter_Tokens_Out_Units],            
--    [Machine_Counter_Refill_Units],            
--    [Machine_Counter_Jackpot_Units],            
--    [Machine_Counter_Prize_Units],            
--    [Machine_Counter_Tournament_Play_Units],            
--    [Machine_Counter_JukeBox_Play_Units],            
--    [Machine_Test],         
--    [Machine_Status_Flag],            
--    [Machine_Status],            
--    [Machine_Start_Date],              
--    [Machine_End_Date],            
--    [Machine_Resale_Value],            
--    [Machine_Sales_Invoice_Number],            
--    [Machine_Sold_To],            
--    [Machine_Type_Of_Sale],            
--    [Machine_PROM_Version],            
--    [Machine_Original_Purchase_Price],            
--    [Machine_Sale_Price],            
--    [Machine_Purchase_Invoice_Number],            
--    [Depot_ID],            
--    [Machine_AMEDIS_Variant_Code],            
--    [Machine_Previous_Machine_ID],            
--    [Machine_Manufacturers_Serial_No],            
--    [Machine_Purchased_From],            
--    [Machine_Depreciation_Start_Date],            
--    [Machine_Last_PAT_Date],            
--    [Machine_PAT_Required],            
--    [Machine_Alternative_Serial_Numbers],            
--    [Staff_ID],            
--    [Machine_Due_In_Stock],            
--    [Machine_Due_In_Stock_Date],            
--    [Machine_Memo],            
--    [Machine_Extra_Details],            
--    [Staff_ID_Entered],            
--    [Staff_ID_Deleted],            
--    [Machine_Date_Entered],            
--    [Machine_Date_Deleted],            
--    [Machine_Float_Maximum_Capacity],            
--    [Machine_Float_200p_Capacity],            
--    [Machine_Float_100p_Capacity],            
--    [Machine_Float_50p_Capacity],            
--    [Machine_Float_20p_Capacity],            
--    [Machine_Float_10p_Capacity],            
--    [Machine_Float_5p_Capacity],            
--    [Machine_Float_2p_Capacity],            
--    [Machine_Site_Planned_Movement_ID],            
--    [Machine_Depot_Planned_Movement_ID],            
--    [Machine_Category_ID]            
--  )            
--            
--  SELECT [Machine_Class_ID],     -- [Machine_Class_ID]            
--         [Operator_ID],            
--         [Terms_Profile_ID],            
--         [Depreciation_Policy_ID],            
--         [Depreciation_Policy_Use_Default],            
--         [Machine_Number_Of_Discs],            
--         [Machine_Stock_No],            
--         [Machine_Counter_Cash_In_Units],            
--         [Machine_Counter_Cash_Out_Units],            
--         [Machine_Counter_Tokens_In_Units],            
--         [Machine_Counter_Tokens_Out_Units],            
--         [Machine_Counter_Refill_Units],            
--         [Machine_Counter_Jackpot_Units],            
--         [Machine_Counter_Prize_Units],            
--         [Machine_Counter_Tournament_Play_Units],            
--         [Machine_Counter_JukeBox_Play_Units],            
--         [Machine_Test],            
--         0,            
--         [Machine_Status],            
--         [Machine_Start_Date],       -- [Machine_Start_Date]            
--         NULL,             -- [Machine_End_Date]            
--         [Machine_Resale_Value],            
--         [Machine_Sales_Invoice_Number],            
--         [Machine_Sold_To],            
--         [Machine_Type_Of_Sale],            
--         [Machine_PROM_Version],            
--         [Machine_Original_Purchase_Price],            
--         [Machine_Sale_Price],            
--         [Machine_Purchase_Invoice_Number],            
--         [Depot_ID],            
--         [Machine_AMEDIS_Variant_Code],            
--         @Machine_ID,        -- [Machine_Previous_Machine_ID]            
--         [Machine_Manufacturers_Serial_No],            
--         [Machine_Purchased_From],            
--         [Machine_Depreciation_Start_Date],            
--         [Machine_Last_PAT_Date],            
--         [Machine_PAT_Required],            
--         [Machine_Alternative_Serial_Numbers],            
--         [Staff_ID],            
--         [Machine_Due_In_Stock],            
--         [Machine_Due_In_Stock_Date],            
--         [Machine_Memo],            
--         [Machine_Extra_Details],            
--         [Staff_ID_Entered],            
--         [Staff_ID_Deleted],            
--         [Machine_Date_Entered],            
--         [Machine_Date_Deleted],            
--        [Machine_Float_Maximum_Capacity],            
--         [Machine_Float_200p_Capacity],            
--         [Machine_Float_100p_Capacity],            
--         [Machine_Float_50p_Capacity],            
--         [Machine_Float_20p_Capacity],            
--         [Machine_Float_10p_Capacity],            
--         [Machine_Float_5p_Capacity],             
--         [Machine_Float_2p_Capacity],            
--         [Machine_Site_Planned_Movement_ID],            
--         [Machine_Depot_Planned_Movement_ID],            
--         [Machine_Category_ID]             
--            
--    FROM Machine  WHERE          
--       Machine_ID = @Machine_ID       
    
 If ISNULL(@Machine_ID,0) > 0    
 Begin    
   UPDATE Installation     
   SET Installation_End_Date = @ddmmmyyyy,     
    Installation_End_Time = @hhnnss      
    WHERE Installation_ID = @Installation_ID    
  IF @@ROWCOUNT > 0    
  BEGIN    
   /* Returns 100 On Successfull removal*/    

	EXEC usp_ResetAAMSDetails @Machine_ID

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
	
	UPDATE MACHINE
	SET Machine_Status_Flag = @Machine_Status_Flag,
		Machine_Status_Flag_Prev = 0,
		Machine_Transit_Site_Code = @Machine_Transit_Site_Code
		OUTPUT INSERTED.Machine_ID,
			DELETED.Machine_Status_Flag, INSERTED.Machine_Status_Flag,
			DELETED.Machine_Class_ID, INSERTED.Machine_Class_ID,
			DELETED.CMPGameType, INSERTED.CMPGameType, 
			DELETED.Machine_Stock_No, INSERTED.Machine_Stock_No
		INTO @_Modified
	WHERE Machine_id = @Machine_ID
	
	IF EXISTS(
		SELECT 1
		FROM   @_Modified m
		WHERE  m.FlagChanged = 1 OR
				m.GameIDChanged = 1 OR
				m.CMPGameTypeChanged = 1 OR
				m.StockNoChanged = 1
	)
	BEGIN
		EXEC [dbo].[usp_EBS_UpdateMachineDetails] @Machine_ID 
	END

	/*****************************************************************************************************
	* (CR# 191254 : EBS Communication Service) - MODIFIED BY A.VINOD KUMAR (END)
	*****************************************************************************************************/
	
	--Reset the Game Availablilty.
	SELECT @HQ_IGI_ID = MAX(HQ_IGI_ID) FROM dbo.Installation_Game_Info
	WHERE Installation_No = @Installation_ID AND IsAvailable = 1

	UPDATE dbo.Installation_Game_Info
	SET IsAvailable = 0,
	Game_Comments = 'Installation Removed.'
	WHERE Installation_No = @Installation_ID AND IsAvailable = 1

	--Auto closure of service call on installation removal
	DECLARE @ServiceId INT
	DECLARE @CloseServiceId CURSOR
	SET @CloseServiceId = CURSOR FOR
	SELECT Service_ID FROM Service WHERE Installation_ID = @Installation_ID AND service_id NOT IN (select service_id from service_closed)
	OPEN @CloseServiceId
	FETCH NEXT
	FROM @CloseServiceId INTO @ServiceId
	WHILE @@FETCH_STATUS = 0
	BEGIN
		IF ISNULL(@ServiceId,0) <> 0
			BEGIN
				PRINT @ServiceId
				EXEC dbo.usp_CloseServiceCalls @ServiceId,NULL,NULL,NULL,'SYSTEM  M/c REMOVED' 
			END
		FETCH NEXT
		FROM @CloseServiceId INTO @ServiceId
	END
	CLOSE @CloseServiceId
	DEALLOCATE @CloseServiceId


    --Add entries for AAMS.
	--Insert a record for AAMS, only when the asset is not in transit state(18)
--	IF(@Machine_Status_Flag <> 18)
--	BEGIN
--		--Insert the Game Removal 310 message.  
--		INSERT INTO BMC_BAS_Export_History(BBEH_Reference, BBEH_AAMS_Entity_Type, BBEH_Message_Type,     
--		BBEH_Status, BBEH_Received_Date, BBEH_BAS_Message_ID, BBEH_AAMS_Approval, BBEH_Session_Status, BBEH_Process_Type)      
--		SELECT @HQ_IGI_ID, 4, 310 ,0, GETDATE(), null, 0, 'Initiated', 1    
--		
--		--Insert the VLT Removal 306 message.
--		EXEC dbo.usp_InsertBMCBASExportRecord @Machine_ID, 3, 306, NULL, 4, 'Installation Removal.'		    
--	END
	SELECT 100 As RESULT
   RETURN    
  END    
  ELSE    
  BEGIN    
   /* Returns -1 if Installation Details could not be updated */    
   SELECT -5 As RESULT    
   RETURN    
  END    
 END    
 ELSE    
 BEGIN    
  /* Returns -4 if Machine Details Could Not be Updated*/    
  SELECT -4 AS RESULT    
 END 
   
END


GO

