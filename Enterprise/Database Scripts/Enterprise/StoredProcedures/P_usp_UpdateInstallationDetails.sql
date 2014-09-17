USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_UpdateInstallationDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_UpdateInstallationDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--------------------------------------------------------------------------                     
--                    
-- Description: Update Installation Details from Excahnge                  
--                             
--                    
-- Inputs:      XML File Containing Installation Details
--                    
-- Outputs:     NONE                    
--                    
-- Return:          See Comments                  
--                    
-- =======================================================================                    
--                     
-- Revision History                    
--                     
-- NaveenChander     30/07/2008     Created                    
-- NaveenChander     28/08/2008     Bug Fix- For Duplicate XML Entries     
-- NaveenChander     27/06/2009     Added Zone to Import
---------------------------------------------------------------------------                     

CREATE PROCEDURE [dbo].[usp_UpdateInstallationDetails](@XML nVARCHAR(4000))                            
AS                            
BEGIN    
    
    DECLARE @INT INT    
    Set @INT  = 0    
    EXEC sp_xml_preparedocument @INT OUTPUT, @XML    
    

	DECLARE @Zone_Name VARCHAR(30)   
	DECLARE @ZoneId INT 
	DECLARE @Site_ID INT
    
    SELECT * INTO #temp FROM OPENXML    
    (@INT, '/INSTALLATIONUPDATE',2)    
    WITH    
    (    
        HQ_Installation_No int,    
        Datapak_No int,
		Zone_name Varchar(50),    
        Installation_Reference Varchar(50),    
        Anticipated_Percentage_Payout real,    
        Installation_Counter_Cash_In_Units int,    
        Installation_Counter_Cash_Out_Units int,    
        Installation_Counter_Token_In_Units int,    
        Installation_Counter_Token_Out_Units int,    
        Installation_Counter_Prize_Units int,    
        Installation_Counter_Refill_Units int,    
        Installation_Counter_Tournament_Units int,    
        Installation_Counter_Jukebox_Units int,    
        Installation_Initial_Meters_Coin_Drop int,    
        Installation_Initial_Meters_External_Credit int,    
        Installation_Initial_Meters_Games_Bet int,    
        Installation_Initial_Meters_Games_Won int,    
        Installation_Initial_Meters_Notes int,    
        Installation_Initial_Meters_Handpay int,    
        Installation_Initial_Meters_Coins_In int,    
        Installation_Initial_Meters_Coins_Out int,    
        Installation_Jackpot int,    
        Installation_Price_Of_Play int,    
        Coins_In_Counter int,    
        Coins_Out_Counter int,    
        Tokens_In_Counter int,    
        Tokens_Out_Counter int,    
        Prize_Counter int,    
        Refill_Counter int,    
        Tournament_Counter int,    
        Jukebox_Counter int,    
        Installation_Token_Value int    
    )   


	SELECT
	 @Zone_Name = Zone_Name
	FROM
	 #temp

	SELECT @ZoneId=Zone_Id FROM Zone WHERE Zone_Name=@Zone_Name 
	
	SELECT 
		@Site_ID = Site_ID 
	FROM 
		Bar_Position tBP
	INNER JOIN Installation tI
		ON tI.Bar_Position_ID = tBP.Bar_Position_ID
	INNER JOIN #temp T
		ON T.HQ_Installation_No = HQInstallationID

	                                  
	IF NOT EXISTS(SELECT Zone_id FROM zone WHERE Zone_Name=@Zone_Name AND Site_ID = @Site_ID)                                  
	BEGIN                                  
		INSERT INTO Zone(Site_Id,Zone_Name,Zone_Start_Date) VALUES (@Site_Id,@Zone_Name, Getdate())                                  
		SET @ZONEID = SCOPE_IDENTITY()  
/*****************************************************************************************************  
 * (CR# 191254 : EBS Communication Service) - MODIFIED BY H.VENKATESH (START)  
 *****************************************************************************************************/  
		EXEC [dbo].[usp_EBS_UpdateZoneDetails] @ZoneId = @ZONEID
/*****************************************************************************************************  
 * (CR# 191254 : EBS Communication Service) - MODIFIED BY H.VENKATESH (END)  
 *****************************************************************************************************/                                  
	END 
/*****************************************************************************************************
* (CR# 191254 : EBS Communication Service) - MODIFIED BY H.VENKATESH (START)
*****************************************************************************************************/

            DECLARE @_Modified TABLE (
										MachineId INT,
										InstallationID INT,
										
										OldInstallation_Price_Per_Play INT, NewInstallation_Price_Per_Play INT,
										Installation_Price_Per_PlayChanged AS (CASE WHEN OldInstallation_Price_Per_Play = NewInstallation_Price_Per_Play THEN 0 ELSE 1 END)
									)
									
			 DECLARE @_Modified1 TABLE (	
										BarPositionID INT,
										OldZone_ID INT, NewZone_ID INT,
										Zone_IDChanged As (CASE WHEN OldZone_ID = NewZone_ID THEN 0 ELSE 1 END)
										)
															
										
	UPDATE
		tBP
	SET
		tBP.Zone_ID = @ZONEID
	OUTPUT inserted.Bar_Position_ID,
	deleted.Zone_ID,inserted.Zone_ID
	INTO @_Modified1
	FROM
		Bar_Position tBP
	INNER JOIN Installation tI
		ON tI.Bar_Position_ID = tBP.Bar_Position_ID
	INNER JOIN #temp T
		ON T.HQ_Installation_No = HQInstallationID
    
    Update     
        I     
    Set     
        I.Datapak_ID = Datapak_No    
        ,I.Installation_Reference = T.Installation_Reference    
        ,I.Installation_Percentage_Payout = T.Anticipated_Percentage_Payout    
        ,I.Installation_Counter_Cash_In_Units = T.Installation_Counter_Cash_In_Units    
        ,I.Installation_Counter_Cash_Out_Units = T.Installation_Counter_Cash_Out_Units    
        ,I.Installation_Counter_Tokens_In_Units = T.Installation_Counter_Token_In_Units    
        ,I.Installation_Counter_Tokens_Out_Units = T.Installation_Counter_Token_Out_Units    
        ,I.Installation_Counter_Prize_Units = T.Installation_Counter_Prize_Units    
        ,I.Installation_Counter_Refill_Units = T.Installation_Counter_Refill_Units    
        ,I.Installation_Counter_Tournament_Play_Units = T.Installation_Counter_Tournament_Units    
        ,I.Installation_Counter_JukeBox_Play_Units = T.Installation_Counter_Jukebox_Units            
        ,I.Installation_Initial_Coins_In = T.Installation_Initial_Meters_Coins_In    
        ,I.Installation_Initial_Coins_Out = T.Installation_Initial_Meters_Coins_Out    
        ,I.Installation_Jackpot_Value = T.Installation_Jackpot    
        ,I.Installation_Price_Per_Play = T.Installation_Price_Of_Play    
        ,I.Installation_Initial_Tokens_In = T.Tokens_In_Counter    
        ,I.Installation_Initial_Tokens_Out = T.Tokens_Out_Counter    
        ,I.Installation_Initial_Prize_Meter = T.Prize_Counter    
        ,I.Installation_Initial_Refill_Meter = T.Refill_Counter    
        ,I.Installation_Initial_Tournament_Play_Meter = T.Tournament_Counter    
        ,I.Installation_Initial_JukeBox_Play_Meter = T.Jukebox_Counter    
        ,I.Installation_Initial_SC_Coins_In = T.Installation_Initial_Meters_Coins_In    
        ,I.Installation_Initial_SC_Coins_Out = T.Installation_Initial_Meters_Coins_Out    
        ,I.Installation_Initial_Coins_Drop = T.Installation_Initial_Meters_Coin_Drop    
        ,I.Installation_Initial_ExternalCredit = T.Installation_Initial_Meters_External_Credit    
        ,I.Installation_Initial_GamesBet = T.Installation_Initial_Meters_Games_Bet    
        ,I.Installation_Initial_GamesWon = T.Installation_Initial_Meters_Games_Won    
        ,I.Installation_Initial_Notes = T.Installation_Initial_Meters_Notes    
        ,I.Installation_Initial_Handpay = T.Installation_Initial_Meters_Handpay    
        ,I.Installation_Token_Value = T.Installation_Token_Value 
     OUTPUT 
		INSERTED.Machine_ID,
		INSERTED.Installation_ID,
		DELETED.Installation_Price_Per_Play, 
		INSERTED.Installation_Price_Per_Play
                               
      INTO @_Modified
    FROM     
        INSTALLATION I    
    INNER JOIN #temp T     
        ON T.HQ_Installation_No = HQInstallationID    
    
    IF @@ROWCOUNT = 0    
    BEGIN    
        SELECT 'NOTUPDATED' As Result    
    END    
    ELSE    
    BEGIN    
            
                IF EXISTS(
                                SELECT 1
                                FROM   @_Modified m
                                WHERE  m.Installation_Price_Per_PlayChanged = 1 
                )
                BEGIN
								DECLARE @Machine_ID INT
								DECLARE @Installation_ID INT
								SELECT @Machine_ID = MachineId,
								@Installation_ID = InstallationID
								 FROM  @_Modified m
                                WHERE  m.Installation_Price_Per_PlayChanged = 1 
                                
                                IF ISNULL(@Installation_ID, 0) > 0
                                BEGIN
									EXEC [dbo].[usp_EBS_UpdateDenominationDetails] @Installation_ID
								END
								IF ISNULL(@Machine_ID, 0) > 0
                                BEGIN
									EXEC [dbo].[usp_EBS_UpdateMachineDetails] @Machine_ID 
                                END
                END
                
                IF EXISTS(
                                SELECT 1
                                FROM   @_Modified1 m
                                WHERE  m.Zone_IDChanged = 1 
                )
                BEGIN
								DECLARE @Machine_ID1 INT
								DECLARE @Zone_ID1 INT
								SELECT @Machine_ID1 = Machine_ID FROM Installation WHERE Installation_ID =
								(Select MAX(Installation_ID) FROM Installation WHERE Bar_Position_ID IN 
								(SELECT BarPositionID FROM @_Modified1 m WHERE  m.Zone_IDChanged = 1))
								
								SELECT @Zone_ID1 = newZone_ID FROM  @_Modified1 m WHERE  m.Zone_IDChanged = 1
								
								
                                IF ISNULL(@Zone_ID1, 0) > 0
                                BEGIN
									EXEC [dbo].[usp_EBS_UpdateZoneDetails] @ZoneId = @Zone_ID1
								END
								IF ISNULL(@Machine_ID1, 0)  > 0
								BEGIN
									EXEC [dbo].[usp_EBS_UpdateMachineDetails] @Machine_ID1 
                                END
                END
		SELECT 'UPDATED' As Result
    END   
    
END


GO

