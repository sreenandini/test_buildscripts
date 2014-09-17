USE [Enterprise]
GO

IF  EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Usp_RemoveOldAndAddNewInstallation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Usp_RemoveOldAndAddNewInstallation]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[Usp_RemoveOldAndAddNewInstallation]( @OLDXML nVARCHAR(4000), @NEWXML nVARCHAR(4000)) AS                         
BEGIN                        
    DECLARE @OLDINT INT                        
    DECLARE @OldMachine_ID INT                        
    DECLARE @OldInstallation_ID INT                        
    DECLARE @ddmmmyyyy VARCHAR (20)                        
    DECLARE @hhnnss VARCHAR (20)                        
                    
    DECLARE @Zone_Name VARCHAR(30)                            
    DECLARE @BAR_ID INT                            
    DECLARE @MACHINE_ID INT                            
    DECLARE @Site_Id VARCHAR(30)                            
    DECLARE @Installation_Date VARCHAR(30)                            
    DECLARE @Installation_Time VARCHAR(50)                            
    DECLARE @Bar_Position_Name VARCHAR(50)                            
    DECLARE @AssetNo VARCHAR(10)                            
    DECLARE @Price_of_play INT                    
    DECLARE @DATAPAK INT                          
    DECLARE @Machine_Class_Name VARCHAR(50)                            
    DECLARE @Machine_Type_Code VARCHAR(20)                            
    DECLARE @Machine_Stock_No VARCHAR(50)                            
    DECLARE @ZoneId INT        
    DECLARE @AlternateSerialNo VARCHAR(50)          
    DECLARE @SerialNo VARCHAR(50)       
    DECLARE @Machine_End_Date  VARCHAR(20)    
    DECLARE @Machine_Status_Flag Int    
    DECLARE @Staff_ID_Deleted Int    
    DECLARE @Machine_Date_Deleted VARCHAR(20)    
    DECLARE @Machine_Type_Of_Sale VARCHAR(150)       
           --Installation New Columns - Start
DECLARE @Installation_Reference   VARCHAR(50)
--DECLARE @Start_Date DATETIME
--DECLARE @End_Date DATETIME
DECLARE @Coins_In_Counter  INT
DECLARE @Coins_Out_Counter  INT
DECLARE @Tokens_In_Counter  INT
DECLARE @Tokens_Out_Counter  INT
DECLARE @Prize_Counter  INT
DECLARE @Refill_Counter  INT
DECLARE @Tournament_Counter  INT
DECLARE @Jukebox_Counter  INT
--DECLARE @Previous_Installation  INT
DECLARE @Bagged_Cash_Installation_No  REAL
DECLARE @Bagged_Cash_Amount REAL
DECLARE @Bagged_Cash_Float REAL
DECLARE @Installation_Out_Of_Order BIT
--DECLARE @Float_Issued REAL
DECLARE @Float_Issued_By  VARCHAR(50)
DECLARE @Installation_Jackpot_Value  INT
DECLARE @Installation_Meter_1_Initial_Value  INT
DECLARE @Installation_Meter_1_Final_Value  INT
DECLARE @Installation_Meter_2_Initial_Value  INT
DECLARE @Installation_Meter_2_Final_Value  INT
DECLARE @Installation_Meter_3_Initial_Value  INT
DECLARE @Installation_Meter_3_Final_Value  INT
DECLARE @Installation_Meter_4_Initial_Value  INT
DECLARE @Installation_Meter_4_Final_Value  INT
DECLARE @Installation_Meter_5_Initial_Value  INT
DECLARE @Installation_Meter_5_Final_Value  INT
DECLARE @Installation_Meter_6_Initial_Value  INT
DECLARE @Installation_Meter_6_Final_Value  INT
DECLARE @Installation_Meter_7_Initial_Value  INT
DECLARE @Installation_Meter_7_Final_Value  INT
DECLARE @Installation_Meter_8_Initial_Value  INT
DECLARE @Installation_Meter_8_Final_Value  INT
DECLARE @Installation_Meter_9_Initial_Value  INT
DECLARE @Installation_Meter_9_Final_Value  INT
DECLARE @Installation_Meter_10_Initial_Value  INT
DECLARE @Installation_Meter_10_Final_Value  INT
DECLARE @Installation_Meter_11_Initial_Value  INT
DECLARE @Installation_Meter_11_Final_Value  INT
DECLARE @Installation_Meter_12_Initial_Value  INT
DECLARE @Installation_Meter_12_Final_Value  INT
DECLARE @Installation_Meter_13_Initial_Value  INT
DECLARE @Installation_Meter_13_Final_Value  INT
DECLARE @Installation_Meter_14_Initial_Value  INT
DECLARE @Installation_Meter_14_Final_Value  INT
DECLARE @Installation_Meter_15_Initial_Value  INT
DECLARE @Installation_Meter_15_Final_Value  INT
DECLARE @Installation_Meter_16_Initial_Value  INT
DECLARE @Installation_Meter_16_Final_Value  INT
DECLARE @Installation_Meter_17_Initial_Value  INT
DECLARE @Installation_Meter_17_Final_Value  INT
DECLARE @Installation_Meter_18_Initial_Value  INT
DECLARE @Installation_Meter_18_Final_Value  INT
DECLARE @Installation_Meter_19_Initial_Value  INT
DECLARE @Installation_Meter_19_Final_Value  INT
DECLARE @Installation_Meter_20_Initial_Value  INT
DECLARE @Installation_Meter_20_Final_Value  INT
DECLARE @Installation_Meter_21_Initial_Value  INT
DECLARE @Installation_Meter_21_Final_Value  INT
DECLARE @Installation_Meter_22_Initial_Value  INT
DECLARE @Installation_Meter_22_Final_Value  INT
DECLARE @Installation_Meter_23_Initial_Value  INT
DECLARE @Installation_Meter_23_Final_Value  INT
DECLARE @Installation_Meter_24_Initial_Value  INT
DECLARE @Installation_Meter_24_Final_Value  INT
DECLARE @Installation_Meter_25_Initial_Value  INT
DECLARE @Installation_Meter_25_Final_Value  INT
DECLARE @Installation_Meter_26_Initial_Value  INT
DECLARE @Installation_Meter_26_Final_Value  INT
DECLARE @Installation_Meter_27_Initial_Value  INT
DECLARE @Installation_Meter_27_Final_Value  INT
DECLARE @Installation_Meter_28_Initial_Value  INT
DECLARE @Installation_Meter_28_Final_Value  INT
DECLARE @Installation_Meter_29_Initial_Value  INT
DECLARE @Installation_Meter_29_Final_Value  INT
DECLARE @Installation_Meter_30_Initial_Value  INT
DECLARE @Installation_Meter_30_Final_Value  INT
DECLARE @Installation_Meter_31_Initial_Value  INT
DECLARE @Installation_Meter_31_Final_Value  INT
DECLARE @Installation_Meter_32_Initial_Value  INT
DECLARE @Installation_Meter_32_Final_Value  INT
DECLARE @Installation_Float_Status  INT
DECLARE @Installation_Initial_Meters_Coins_In  INT
DECLARE @Installation_Initial_Meters_Coins_Out  INT
DECLARE @Installation_Initial_Meters_Coin_Drop  INT
DECLARE @Installation_Initial_Meters_External_Credit  INT
DECLARE @Installation_Initial_Meters_Games_Bet  INT
DECLARE @Installation_Initial_Meters_Games_Won  INT
DECLARE @Installation_Initial_Meters_Notes  INT
DECLARE @Installation_Initial_Meters_Handpay  INT
DECLARE @Anticipated_Removal_Date VARCHAR(30)
DECLARE @Rental_Step_Down_Date VARCHAR(30)
DECLARE @Rent1 MONEY  
DECLARE @Rent2 MONEY
DECLARE @Licence MONEY
DECLARE @Installation_Out_Order BIT
DECLARE @Installation_Counter_Cash_In_Units  INT
DECLARE @Installation_Counter_Cash_Out_Units  INT
--DECLARE @Installation_Counter_Token_In_Units  INT
DECLARE @Installation_Counter_Token_Out_Units  INT
DECLARE @Installation_Counter_Refill_Units  INT
DECLARE @Installation_Counter_Jackpot_Units  INT
DECLARE @Installation_Counter_Prize_Units  INT
DECLARE @Installation_Counter_Tournament_Units  INT
DECLARE @Installation_Counter_Jukebox_Play_Units  INT
DECLARE @Installation_Counter_Jukebox_Units  INT
DECLARE @Planned_Movement_ID  INT
DECLARE @Installation_RDC_Machine_Code VARCHAR(10)
DECLARE @Installation_RDC_Secondary_Machine_Code VARCHAR(20)
--DECLARE @Installation_Token_Value  INT
DECLARE @Installation_Games_Count  INT
DECLARE @Installation_Status  VARCHAR(50)
DECLARE @Game_Part_Number VARCHAR(20)
DECLARE @Installation_MaxBet  INT
DECLARE @IsAuxSerialPortEnabled BIT
DECLARE @IsGatSerialPortEnabled BIT
DECLARE @IsSlotLinePortEnabled BIT
DECLARE @Port_Disabled_Status BIT
DECLARE @Voucher_Expire_Status CHAR(1)
DECLARE @FinalCollection_Status TINYINT

--Installation -New column End             
    SET @OLDINT  = 0                        
    EXEC sp_xml_preparedocument @OLDINT OUTPUT, @OLDXML                        
  
  
                  
    SELECT * INTO #OldTemp FROM OPENXML                        
        (@OLDINT, '/OLDINSTALLATION',2)                        
    WITH                        
        (HQInstallation_No INT,                        
          EndDate VARCHAR(20),                        
          EndTime VARCHAR(20),    
          Machine_End_Date  VARCHAR(20),    
          Machine_Status_Flag Int,    
          Staff_ID_Deleted Int,    
          Machine_Date_Deleted VARCHAR(20),    
          Machine_Type_Of_Sale VARCHAR(150)    ,
-- Adding the Installation table columns which are not there in Enterprise Installation Table - Begin
		-- Enterprise Installation table doesn't have some columns and those columns sre added to that table. So in XML including those columns too.
		--HQ_Installation_No  INT,
	 Installation_Reference   VARCHAR(50),
	-- Installation_Date DATETIME,
	 --End_Date DATETIME,
	 Coins_In_Counter  INT,
	 Coins_Out_Counter  INT,
	 Tokens_In_Counter  INT,
	 Tokens_Out_Counter  INT,
	 Prize_Counter  INT,
	 Refill_Counter  INT,
	 Tournament_Counter  INT,
	 Jukebox_Counter  INT,
	-- Previous_Installation  INT,
	 Bagged_Cash_Installation_No  REAL,
	 Bagged_Cash_Amount  REAL,
	 Bagged_Cash_Float  REAL,
	 Installation_Out_Of_Order  BIT,
	-- Float_Issued  REAL,
	 Float_Issued_By   VARCHAR(50),
	 Installation_Jackpot_Value  INT,
	 Installation_Meter_1_Initial_Value  INT,
	 Installation_Meter_1_Final_Value  INT,
	 Installation_Meter_2_Initial_Value  INT,
	 Installation_Meter_2_Final_Value  INT,
	 Installation_Meter_3_Initial_Value  INT,
	 Installation_Meter_3_Final_Value  INT,
	 Installation_Meter_4_Initial_Value  INT,
	 Installation_Meter_4_Final_Value  INT,
	 Installation_Meter_5_Initial_Value  INT,
	 Installation_Meter_5_Final_Value  INT,
	 Installation_Meter_6_Initial_Value  INT,
	 Installation_Meter_6_Final_Value  INT,
	 Installation_Meter_7_Initial_Value  INT,
	 Installation_Meter_7_Final_Value  INT,
	 Installation_Meter_8_Initial_Value  INT,
	 Installation_Meter_8_Final_Value  INT,
	 Installation_Meter_9_Initial_Value  INT,
	 Installation_Meter_9_Final_Value  INT,
	 Installation_Meter_10_Initial_Value  INT,
	 Installation_Meter_10_Final_Value  INT,
	 Installation_Meter_11_Initial_Value  INT,
	 Installation_Meter_11_Final_Value  INT,
	 Installation_Meter_12_Initial_Value  INT,
	 Installation_Meter_12_Final_Value  INT,
	 Installation_Meter_13_Initial_Value  INT,
	 Installation_Meter_13_Final_Value  INT,
	 Installation_Meter_14_Initial_Value  INT,
	 Installation_Meter_14_Final_Value  INT,
	 Installation_Meter_15_Initial_Value  INT,
	 Installation_Meter_15_Final_Value  INT,
	 Installation_Meter_16_Initial_Value  INT,
	 Installation_Meter_16_Final_Value  INT,
	 Installation_Meter_17_Initial_Value  INT,
	 Installation_Meter_17_Final_Value  INT,
	 Installation_Meter_18_Initial_Value  INT,
	 Installation_Meter_18_Final_Value  INT,
	 Installation_Meter_19_Initial_Value  INT,
	 Installation_Meter_19_Final_Value  INT,
	 Installation_Meter_20_Initial_Value  INT,
	 Installation_Meter_20_Final_Value  INT,
	 Installation_Meter_21_Initial_Value  INT,
	 Installation_Meter_21_Final_Value  INT,
	 Installation_Meter_22_Initial_Value  INT,
	 Installation_Meter_22_Final_Value  INT,
	 Installation_Meter_23_Initial_Value  INT,
	 Installation_Meter_23_Final_Value  INT,
	 Installation_Meter_24_Initial_Value  INT,
	 Installation_Meter_24_Final_Value  INT,
	 Installation_Meter_25_Initial_Value  INT,
	 Installation_Meter_25_Final_Value  INT,
	 Installation_Meter_26_Initial_Value  INT,
	 Installation_Meter_26_Final_Value  INT,
	 Installation_Meter_27_Initial_Value  INT,
	 Installation_Meter_27_Final_Value  INT,
	 Installation_Meter_28_Initial_Value  INT,
	 Installation_Meter_28_Final_Value  INT,
	 Installation_Meter_29_Initial_Value  INT,
	 Installation_Meter_29_Final_Value  INT,
	 Installation_Meter_30_Initial_Value  INT,
	 Installation_Meter_30_Final_Value  INT,
	 Installation_Meter_31_Initial_Value  INT,
	 Installation_Meter_31_Final_Value  INT,
	 Installation_Meter_32_Initial_Value  INT,
	 Installation_Meter_32_Final_Value  INT,
	 Installation_Float_Status  INT,
	 Installation_Initial_Meters_Coins_In  INT,
	 Installation_Initial_Meters_Coins_Out  INT,
	 Installation_Initial_Meters_Coin_Drop  INT,
	 Installation_Initial_Meters_External_Credit  INT,
	 Installation_Initial_Meters_Games_Bet  INT,
	 Installation_Initial_Meters_Games_Won  INT,
	 Installation_Initial_Meters_Notes  INT,
	 Installation_Initial_Meters_Handpay  INT,
	 Anticipated_Removal_Date  VARCHAR(30),
	 Rental_Step_Down_Date  VARCHAR(30),
	 Rent1 MONEY,
	 Rent2 MONEY,
	 Licence MONEY,
	 Installation_Out_Order  BIT,
	 Installation_Counter_Cash_In_Units  INT,
	 Installation_Counter_Cash_Out_Units  INT,
	-- Installation_Counter_Token_In_Units  INT,
	 Installation_Counter_Token_Out_Units  INT,
	 Installation_Counter_Refill_Units  INT,
	 Installation_Counter_Jackpot_Units  INT,
	 Installation_Counter_Prize_Units  INT,
	 Installation_Counter_Tournament_Units  INT,
	 Installation_Counter_Jukebox_Play_Units  INT,
	 Installation_Counter_Jukebox_Units  INT,
	 Planned_Movement_ID  INT,
	 Installation_RDC_Machine_Code  VARCHAR(10),
	 Installation_RDC_Secondary_Machine_Code  VARCHAR(20),
	 --Installation_Token_Value  INT,
	 Installation_Games_Count  INT,
	 Installation_Status   VARCHAR(50),
	 Game_Part_Number  VARCHAR(20),
	 Installation_MaxBet  INT,
	 IsAuxSerialPortEnabled  BIT,
	 IsGatSerialPortEnabled  BIT,
	 IsSlotLinePortEnabled  BIT,
	 Port_Disabled_Status  BIT,
	 Voucher_Expire_Status CHAR(1),
	 FinalCollection_Status TINYINT
		-- Adding the Installation table columns which are not there in Enterprise Installation Table - End


                 
        )                        
                    
    SELECT @OldInstallation_ID = HQInstallation_No, @ddmmmyyyy = EndDate, @hhnnss = EndTime  FROM #OldTemp                          
            
    IF EXISTS (SELECT 1 FROM SYSOBJECTS WHERE NAME ='OLDXML' AND xTYPE = 'u')            
       BEGIN             
    DROP TABLE  OLDXML            
    END             
                
     SELECT * INTO OLDXML FRom  #OldTemp    
  
  
    DECLARE @INT INT                            
    Set @INT  = 0                            
    EXEC sp_xml_preparedocument @INT OUTPUT, @NewXML                            
                    
                    
    SELECT * INTO #NewTemp FROM OPENXML                             
        (@INT, '/NEWINSTALLATION',2)                            
    WITH                             
        (Site_Code VARCHAR(50),                            
        Installation_Date VARCHAR(30),                            
        Installation_Time VARCHAR(50),                  
        DATAPAK INT,                  
        Zone_Name VARCHAR(100),                  
        HQ_Installation_no INT,                            
        Bar_Pos_Name VARCHAR(50),                            
        Asset_No VARCHAR(50),                            
        Machine_Class_Name VARCHAR(50),                            
        Machine_Class_Price_Of_play INT,                            
        Machine_Type_Code VARCHAR(50),        
        SERIALNO VARCHAR(50),          
        ALTERNATESERIALNO VARCHAR(50) ,      
        MACHINE_JACKPOT INT,      
        FLOAT_ISSUED REAL,      
        INSTALLATION_TOKEN_VALUE INT,    
        PERCENTAGEPAYOUT INT ,
-- Adding the Installation table columns - Begin
		
-- Adding the Installation table columns which are not there in Enterprise Installation Table - Begin
		-- Enterprise Installation table doesn't have some columns and those columns sre added to that table. So in XML including those columns too.
		--HQ_Installation_No  INT,
	 Installation_Reference   VARCHAR(50),
	-- Installation_Date DATETIME,
	 --End_Date DATETIME,
	 Coins_In_Counter  INT,
	 Coins_Out_Counter  INT,
	 Tokens_In_Counter  INT,
	 Tokens_Out_Counter  INT,
	 Prize_Counter  INT,
	 Refill_Counter  INT,
	 Tournament_Counter  INT,
	 Jukebox_Counter  INT,
	-- Previous_Installation  INT,
	 Bagged_Cash_Installation_No  REAL,
	 Bagged_Cash_Amount  REAL,
	 Bagged_Cash_Float  REAL,
	 Installation_Out_Of_Order  BIT,
	-- Float_Issued  REAL,
	 Float_Issued_By   VARCHAR(50),
	 Installation_Jackpot_Value  INT,
	 Installation_Meter_1_Initial_Value  INT,
	 Installation_Meter_1_Final_Value  INT,
	 Installation_Meter_2_Initial_Value  INT,
	 Installation_Meter_2_Final_Value  INT,
	 Installation_Meter_3_Initial_Value  INT,
	 Installation_Meter_3_Final_Value  INT,
	 Installation_Meter_4_Initial_Value  INT,
	 Installation_Meter_4_Final_Value  INT,
	 Installation_Meter_5_Initial_Value  INT,
	 Installation_Meter_5_Final_Value  INT,
	 Installation_Meter_6_Initial_Value  INT,
	 Installation_Meter_6_Final_Value  INT,
	 Installation_Meter_7_Initial_Value  INT,
	 Installation_Meter_7_Final_Value  INT,
	 Installation_Meter_8_Initial_Value  INT,
	 Installation_Meter_8_Final_Value  INT,
	 Installation_Meter_9_Initial_Value  INT,
	 Installation_Meter_9_Final_Value  INT,
	 Installation_Meter_10_Initial_Value  INT,
	 Installation_Meter_10_Final_Value  INT,
	 Installation_Meter_11_Initial_Value  INT,
	 Installation_Meter_11_Final_Value  INT,
	 Installation_Meter_12_Initial_Value  INT,
	 Installation_Meter_12_Final_Value  INT,
	 Installation_Meter_13_Initial_Value  INT,
	 Installation_Meter_13_Final_Value  INT,
	 Installation_Meter_14_Initial_Value  INT,
	 Installation_Meter_14_Final_Value  INT,
	 Installation_Meter_15_Initial_Value  INT,
	 Installation_Meter_15_Final_Value  INT,
	 Installation_Meter_16_Initial_Value  INT,
	 Installation_Meter_16_Final_Value  INT,
	 Installation_Meter_17_Initial_Value  INT,
	 Installation_Meter_17_Final_Value  INT,
	 Installation_Meter_18_Initial_Value  INT,
	 Installation_Meter_18_Final_Value  INT,
	 Installation_Meter_19_Initial_Value  INT,
	 Installation_Meter_19_Final_Value  INT,
	 Installation_Meter_20_Initial_Value  INT,
	 Installation_Meter_20_Final_Value  INT,
	 Installation_Meter_21_Initial_Value  INT,
	 Installation_Meter_21_Final_Value  INT,
	 Installation_Meter_22_Initial_Value  INT,
	 Installation_Meter_22_Final_Value  INT,
	 Installation_Meter_23_Initial_Value  INT,
	 Installation_Meter_23_Final_Value  INT,
	 Installation_Meter_24_Initial_Value  INT,
	 Installation_Meter_24_Final_Value  INT,
	 Installation_Meter_25_Initial_Value  INT,
	 Installation_Meter_25_Final_Value  INT,
	 Installation_Meter_26_Initial_Value  INT,
	 Installation_Meter_26_Final_Value  INT,
	 Installation_Meter_27_Initial_Value  INT,
	 Installation_Meter_27_Final_Value  INT,
	 Installation_Meter_28_Initial_Value  INT,
	 Installation_Meter_28_Final_Value  INT,
	 Installation_Meter_29_Initial_Value  INT,
	 Installation_Meter_29_Final_Value  INT,
	 Installation_Meter_30_Initial_Value  INT,
	 Installation_Meter_30_Final_Value  INT,
	 Installation_Meter_31_Initial_Value  INT,
	 Installation_Meter_31_Final_Value  INT,
	 Installation_Meter_32_Initial_Value  INT,
	 Installation_Meter_32_Final_Value  INT,
	 Installation_Float_Status  INT,
	 Installation_Initial_Meters_Coins_In  INT,
	 Installation_Initial_Meters_Coins_Out  INT,
	 Installation_Initial_Meters_Coin_Drop  INT,
	 Installation_Initial_Meters_External_Credit  INT,
	 Installation_Initial_Meters_Games_Bet  INT,
	 Installation_Initial_Meters_Games_Won  INT,
	 Installation_Initial_Meters_Notes  INT,
	 Installation_Initial_Meters_Handpay  INT,
	 Anticipated_Removal_Date  VARCHAR(30),
	 Rental_Step_Down_Date  VARCHAR(30),
	 Rent1 MONEY,
	 Rent2 MONEY,
	 Licence MONEY,
	 Installation_Out_Order  BIT,
	 Installation_Counter_Cash_In_Units  INT,
	 Installation_Counter_Cash_Out_Units  INT,
	-- Installation_Counter_Token_In_Units  INT,
	 Installation_Counter_Token_Out_Units  INT,
	 Installation_Counter_Refill_Units  INT,
	 Installation_Counter_Jackpot_Units  INT,
	 Installation_Counter_Prize_Units  INT,
	 Installation_Counter_Tournament_Units  INT,
	 Installation_Counter_Jukebox_Play_Units  INT,
	 Installation_Counter_Jukebox_Units  INT,
	 Planned_Movement_ID  INT,
	 Installation_RDC_Machine_Code  VARCHAR(10),
	 Installation_RDC_Secondary_Machine_Code  VARCHAR(20),
	 --Installation_Token_Value  INT,
	 Installation_Games_Count  INT,
	 Installation_Status   VARCHAR(50),
	 Game_Part_Number  VARCHAR(20),
	 Installation_MaxBet  INT,
	 IsAuxSerialPortEnabled  BIT,
	 IsGatSerialPortEnabled  BIT,
	 IsSlotLinePortEnabled  BIT,
	 Port_Disabled_Status  BIT,
	 Voucher_Expire_Status CHAR(1),
	 FinalCollection_Status TINYINT
		-- Adding the Installation table columns which are not there in Enterprise Installation Table - End	
)       
  
    IF EXISTS (SELECT 1 FROM SYSOBJECTS WHERE NAME = 'NEWXML' AND xTYPE = 'u')         
    BEGIN             
        DROP TABLE  NEWXML            
    END             
                    
    SELECT * INTO NEWXML  FROM #NewTemp     
                    
    BEGIN TRANSACTION                        
                    
    /* Returns -1 if Installation Does not Exists */                          
    IF NOT EXISTS ( SELECT 1 FROM INSTALLATION I WHERE I.HQInstallationID = @OldInstallation_ID )                          
    BEGIN                          
        SELECT -1 AS RESULT                        
        IF @@TRANCOUNT > 0                        
        BEGIN                        
            ROLLBACK TRANSACTION                        
        END                        
        RETURN                          
    END                          
                    
    /* Returns -2 if Site Does not Exists */       
    IF NOT EXISTS ( SELECT S.SITE_ID FROM BAR_POSITION B INNER JOIN SITE S ON S.SITE_ID = B.SITE_ID INNER JOIN INSTALLATION I ON I.Bar_Position_ID = B.Bar_Position_ID WHERE I.HQInstallationID = @OldInstallation_ID )                          
    BEGIN                          
        SELECT -2 AS RESULT                          
        IF @@TRANCOUNT > 0                        
        BEGIN                        
            ROLLBACK TRANSACTION                    
        END                        
    RETURN                          
    END                          
                    
    /* Returns -3 if Installation already Closed*/                          
    IF (SELECT LTRIM(RTRIM(ISNULL(Installation_End_Date,''))) FROM INSTALLATION WHERE HQInstallationID = @OldInstallation_ID   )  <> ''                          
    BEGIN                          
        SELECT -3 AS RESULT                          
        IF @@TRANCOUNT > 0                        
        BEGIN                        
            ROLLBACK TRANSACTION                        
        END                        
    RETURN                  END   
  
-- Moving this Script Here.  Because Machine END DATE is updated in the next statement and this check would become void.  
  
  IF ISNULL((SELECT Machine_End_Date FROM DBO.Machine WHERE MACHINE_ID = (  
                                                                    SELECT MAX(M.Machine_ID) FROM DBO.MACHINE M  
    INNER JOIN DBO.MACHINE_CLASS MC ON MC.Machine_Class_ID = M.Machine_Class_ID  
                                                                    INNER JOIN MACHINE_TYPE MT ON MT.Machine_Type_ID = MC.Machine_Type_ID  
                                                                    WHERE M.Machine_Stock_No=@Machine_Stock_No )  
                ), '') <> ''   
      BEGIN                                  
      --return -8                                  
      SELECT '-8' As Result                                  
      Return                                  
     END                                 
                    
          
 SELECT @Machine_End_Date=Machine_End_Date,@Machine_Status_Flag=Machine_Status_Flag ,@Staff_ID_Deleted= Staff_ID_Deleted,@Machine_Date_Deleted=Machine_Date_Deleted,  @Machine_Type_Of_Sale=Machine_Type_Of_Sale    
 FROM #OldTemp    
       
    UPDATE                         
        M                           
    SET                         
            
        M.Machine_End_Date  = @Machine_End_Date,    
  M.Machine_Status_Flag = @Machine_Status_Flag,    
  M.Staff_ID_Deleted = @Staff_ID_Deleted,    
  M.Machine_Date_Deleted = @Machine_End_Date,    
  M.Machine_Type_Of_Sale =@Machine_Type_Of_Sale    
            
    FROM                         
        Machine M                          
    INNER JOIN INSTALLATION I                         
        ON I.Machine_ID = M.Machine_ID                          
    WHERE                         
        I.HQInstallationID = @OldInstallation_ID            
            
            
              
                    
    If @@ROWCOUNT > 0                          
    BEGIN                          
        UPDATE                     
            Installation                           
        SET Installation_End_Date = @ddmmmyyyy,                           
            Installation_End_Time = @hhnnss                            
        WHERE                     
            Installation_ID = @OldInstallation_ID                          
                    
        IF @@ROWCOUNT > 0                          
        BEGIN                          
            /* Successfull removal of OLD Machine*/                        
            Print 'Successfull removal of OLD Machine'                        
        END                          
        ELSE                          
        BEGIN                          
            /* Returns -1 if Installation Details could not be updated */                          
            SELECT -5 AS RESULT                        
            IF @@TRANCOUNT > 0                        
            BEGIN                        
                ROLLBACK TRANSACTION                        
            END                          
            RETURN                          
        END                         
    END                          
    ELSE                          
    BEGIN                          
        /* Returns -4 if Machine Details Could Not be Updated*/                          
        SELECT -4 AS RESULT           
        IF @@TRANCOUNT > 0                        
        BEGIN                        
            ROLLBACK TRANSACTION                        
        END                        
        RETURN                          
    END                        
                    
                    
    /*----------        End Of Old Machine Removal ------------ */                        
    /*        Transaction is Still open.  Transaction Will      */                        
    /*        be open untill New Machine is installed.          */                        
    /*----------     Begining of New Installation  ------------ */                        
                    
                    
            
                
DECLARE @MACHINE_JACKPOT INT      
DECLARE @FLOAT_ISSUED REAL      
DECLARE @INSTALLATION_TOKEN_VALUE INT    
DECLARE @PERCENTAGEPAYOUT INT              
            
             
                    
    SET @ZoneId=0         
                    
    SELECT @Site_Id = Site_ID FROM Site                             
        INNER JOIN #NewTemp ON #NewTemp.Site_Code = Site.Site_Code                            
                    
                    
    SELECT                             
        @Zone_Name = Zone_Name,                           @Installation_Date = Installation_Date ,                             
        @Installation_Time = Installation_Time ,                            
        @DATAPAK = DATAPAK,                  
        @Bar_Position_Name = Bar_Pos_Name ,                                    
        @AssetNo = Asset_No,                            
        @Price_of_play = Machine_Class_Price_Of_play,                            
        @Machine_Class_Name = Machine_Class_Name,                            
        @Machine_Type_Code = Machine_Type_Code,                            
        @Machine_Stock_No = Asset_No,        
        @SerialNo = SerialNo,          
        @AlternateSerialNo = AlternateSerialNo ,      
 @MACHINE_JACKPOT = MACHINE_JACKPOT,      
 @FLOAT_ISSUED = FLOAT_ISSUED,      
 @INSTALLATION_TOKEN_VALUE = INSTALLATION_TOKEN_VALUE,    
 @PERCENTAGEPAYOUT = PERCENTAGEPAYOUT    ,
--Installation table new columns-Start
--@HQ_Installation_No =HQ_Installation_No ,
@Installation_Reference =Installation_Reference  ,
--@Start_Date = Start_Date,
--@End_Date=End_Date,
@Coins_In_Counter =Coins_In_Counter ,
@Coins_Out_Counter =Coins_Out_Counter  ,
@Tokens_In_Counter =Tokens_In_Counter  ,
@Tokens_Out_Counter =Tokens_Out_Counter ,
@Prize_Counter =Prize_Counter,
@Refill_Counter =Refill_Counter,
@Tournament_Counter =Tournament_Counter,
@Jukebox_Counter =Jukebox_Counter,
--@Previous_Installation =Previous_Installation,
@Bagged_Cash_Installation_No =Bagged_Cash_Installation_No,
@Bagged_Cash_Amount =Bagged_Cash_Amount ,
@Bagged_Cash_Float =Bagged_Cash_Float,
@Installation_Out_Of_Order =Installation_Out_Of_Order ,
--@Float_Issued =Float_Issued ,
@Float_Issued_By =Float_Issued_By,
@Installation_Jackpot_Value =Installation_Jackpot_Value  ,
@Installation_Meter_1_Initial_Value =Installation_Meter_1_Initial_Value ,
@Installation_Meter_1_Final_Value =Installation_Meter_1_Final_Value ,
@Installation_Meter_2_Initial_Value =Installation_Meter_2_Initial_Value ,
@Installation_Meter_2_Final_Value =Installation_Meter_2_Final_Value ,
@Installation_Meter_3_Initial_Value =Installation_Meter_3_Initial_Value ,
@Installation_Meter_3_Final_Value =Installation_Meter_3_Final_Value ,
@Installation_Meter_4_Initial_Value =Installation_Meter_4_Initial_Value ,
@Installation_Meter_4_Final_Value =Installation_Meter_4_Final_Value ,
@Installation_Meter_5_Initial_Value =Installation_Meter_5_Initial_Value ,
@Installation_Meter_5_Final_Value =Installation_Meter_5_Final_Value ,
@Installation_Meter_6_Initial_Value =Installation_Meter_6_Initial_Value ,
@Installation_Meter_6_Final_Value =Installation_Meter_6_Final_Value ,
@Installation_Meter_7_Initial_Value =Installation_Meter_7_Initial_Value  ,
@Installation_Meter_7_Final_Value =Installation_Meter_7_Final_Value  ,
@Installation_Meter_8_Initial_Value =Installation_Meter_8_Initial_Value  ,
@Installation_Meter_8_Final_Value =Installation_Meter_8_Final_Value  ,
@Installation_Meter_9_Initial_Value =Installation_Meter_9_Initial_Value  ,
@Installation_Meter_9_Final_Value =Installation_Meter_9_Final_Value  ,
@Installation_Meter_10_Initial_Value =Installation_Meter_10_Initial_Value  ,
@Installation_Meter_10_Final_Value =Installation_Meter_10_Final_Value  ,
@Installation_Meter_11_Initial_Value =Installation_Meter_11_Initial_Value  ,
@Installation_Meter_11_Final_Value =Installation_Meter_11_Final_Value  ,
@Installation_Meter_12_Initial_Value =Installation_Meter_12_Initial_Value  ,
@Installation_Meter_12_Final_Value =Installation_Meter_12_Final_Value  ,
@Installation_Meter_13_Initial_Value =Installation_Meter_13_Initial_Value  ,
@Installation_Meter_13_Final_Value =Installation_Meter_13_Final_Value  ,
@Installation_Meter_14_Initial_Value =Installation_Meter_14_Initial_Value  ,
@Installation_Meter_14_Final_Value =Installation_Meter_14_Final_Value  ,
@Installation_Meter_15_Initial_Value =Installation_Meter_15_Initial_Value  ,
@Installation_Meter_15_Final_Value =Installation_Meter_15_Final_Value  ,
@Installation_Meter_16_Initial_Value =Installation_Meter_16_Initial_Value  ,
@Installation_Meter_16_Final_Value =Installation_Meter_16_Final_Value  ,
@Installation_Meter_17_Initial_Value =Installation_Meter_17_Initial_Value  ,
@Installation_Meter_17_Final_Value =Installation_Meter_17_Final_Value  ,
@Installation_Meter_18_Initial_Value =Installation_Meter_18_Initial_Value  ,
@Installation_Meter_18_Final_Value =Installation_Meter_18_Final_Value  ,
@Installation_Meter_19_Initial_Value =Installation_Meter_19_Initial_Value  ,
@Installation_Meter_19_Final_Value =Installation_Meter_19_Final_Value  ,
@Installation_Meter_20_Initial_Value =Installation_Meter_20_Initial_Value  ,
@Installation_Meter_20_Final_Value =Installation_Meter_20_Final_Value  ,
@Installation_Meter_21_Initial_Value =Installation_Meter_21_Initial_Value  ,
@Installation_Meter_21_Final_Value =Installation_Meter_21_Final_Value ,
@Installation_Meter_22_Initial_Value =Installation_Meter_22_Initial_Value ,
@Installation_Meter_22_Final_Value =Installation_Meter_22_Final_Value ,
@Installation_Meter_23_Initial_Value =Installation_Meter_23_Initial_Value ,
@Installation_Meter_23_Final_Value =Installation_Meter_23_Final_Value ,
@Installation_Meter_24_Initial_Value =Installation_Meter_24_Initial_Value ,
@Installation_Meter_24_Final_Value =Installation_Meter_24_Final_Value ,
@Installation_Meter_25_Initial_Value =Installation_Meter_25_Initial_Value ,
@Installation_Meter_25_Final_Value =Installation_Meter_25_Final_Value ,
@Installation_Meter_26_Initial_Value =Installation_Meter_26_Initial_Value ,
@Installation_Meter_26_Final_Value =Installation_Meter_26_Final_Value ,
@Installation_Meter_27_Initial_Value =Installation_Meter_27_Initial_Value ,
@Installation_Meter_27_Final_Value =Installation_Meter_27_Final_Value ,
@Installation_Meter_28_Initial_Value =Installation_Meter_28_Initial_Value ,
@Installation_Meter_28_Final_Value =Installation_Meter_28_Final_Value ,
@Installation_Meter_29_Initial_Value =Installation_Meter_29_Initial_Value ,
@Installation_Meter_29_Final_Value =Installation_Meter_29_Final_Value ,
@Installation_Meter_30_Initial_Value =Installation_Meter_30_Initial_Value ,
@Installation_Meter_30_Final_Value =Installation_Meter_30_Final_Value ,
@Installation_Meter_31_Initial_Value =Installation_Meter_31_Initial_Value ,
@Installation_Meter_31_Final_Value =Installation_Meter_31_Final_Value ,
@Installation_Meter_32_Initial_Value =Installation_Meter_32_Initial_Value ,
@Installation_Meter_32_Final_Value =Installation_Meter_32_Final_Value ,
@Installation_Float_Status =Installation_Float_Status ,
@Installation_Initial_Meters_Coins_In =Installation_Initial_Meters_Coins_In ,
@Installation_Initial_Meters_Coins_Out =Installation_Initial_Meters_Coins_Out ,
@Installation_Initial_Meters_Coin_Drop =Installation_Initial_Meters_Coin_Drop ,
@Installation_Initial_Meters_External_Credit =Installation_Initial_Meters_External_Credit ,
@Installation_Initial_Meters_Games_Bet =Installation_Initial_Meters_Games_Bet ,
@Installation_Initial_Meters_Games_Won =Installation_Initial_Meters_Games_Won ,
@Installation_Initial_Meters_Notes =Installation_Initial_Meters_Notes ,
@Installation_Initial_Meters_Handpay =Installation_Initial_Meters_Handpay ,
@Anticipated_Removal_Date  =Anticipated_Removal_Date,
@Rental_Step_Down_Date  =Rental_Step_Down_Date,
@Rent1 =Rent1 ,
@Rent2 =Rent2 ,
@Licence =Licence,
@Installation_Out_Order =Installation_Out_Order,
@Installation_Counter_Cash_In_Units =Installation_Counter_Cash_In_Units ,
@Installation_Counter_Cash_Out_Units =Installation_Counter_Cash_Out_Units ,
--@Installation_Counter_Token_In_Units =Installation_Counter_Token_In_Units ,
@Installation_Counter_Token_Out_Units =Installation_Counter_Token_Out_Units ,
@Installation_Counter_Refill_Units =Installation_Counter_Refill_Units ,
@Installation_Counter_Jackpot_Units =Installation_Counter_Jackpot_Units ,
@Installation_Counter_Prize_Units =Installation_Counter_Prize_Units ,
@Installation_Counter_Tournament_Units =Installation_Counter_Tournament_Units ,
@Installation_Counter_Jukebox_Play_Units =Installation_Counter_Jukebox_Play_Units ,
@Installation_Counter_Jukebox_Units =Installation_Counter_Jukebox_Units ,
@Planned_Movement_ID =Planned_Movement_ID ,
@Installation_RDC_Machine_Code =Installation_RDC_Machine_Code ,
@Installation_RDC_Secondary_Machine_Code  =Installation_RDC_Secondary_Machine_Code ,
--@Installation_Token_Value =Installation_Token_Value ,
@Installation_Games_Count =Installation_Games_Count ,
@Installation_Status  =Installation_Status  ,
@Game_Part_Number  =Game_Part_Number ,
@Installation_MaxBet =Installation_MaxBet ,
@IsAuxSerialPortEnabled =IsAuxSerialPortEnabled ,
@IsGatSerialPortEnabled =IsGatSerialPortEnabled ,
@IsSlotLinePortEnabled =IsSlotLinePortEnabled ,
@Port_Disabled_Status =Port_Disabled_Status ,
@Voucher_Expire_Status =Voucher_Expire_Status,
@FinalCollection_Status=FinalCollection_Status
      
    FROM                             
        #NewTemp                            
                    
    SELECT @ZoneId=Zone_Id FROM DBO.Zone WHERE Zone_Name=@Zone_Name                            
                    
                    
    --Check for Zone                            
    IF NOT EXISTS(SELECT Zone_id FROM DBO.zone WHERE Zone_Name=@Zone_Name AND Site_ID = @Site_ID)                            
    BEGIN                            
        INSERT INTO Zone(Site_Id,Zone_Name,Zone_Start_Date) VALUES (@Site_Id,@Zone_Name,@Installation_date)                            
        SET @ZONEID = SCOPE_IDENTITY() 
/*****************************************************************************************************  
 * (CR# 191254 : EBS Communication Service) - MODIFIED BY H.VENKATESH (START)  
 *****************************************************************************************************/  
		EXEC [dbo].[usp_EBS_UpdateZoneDetails] @ZoneId = @ZONEID
/*****************************************************************************************************  
 * (CR# 191254 : EBS Communication Service) - MODIFIED BY H.VENKATESH (END)  
 *****************************************************************************************************/  
                                    
    END                            
                    
                    
    --Check for Bar_Position                            
    IF NOT EXISTS(SELECT Bar_Position_Name FROM bar_position WHERE SITE_ID = @SITE_ID AND Bar_Position_Name=@Bar_Position_Name)                            
    BEGIN                            
        INSERT INTO Bar_Position(Zone_Id,Bar_Position_Name,Site_Id,Bar_Position_Start_date)                            
        VALUES(@ZoneId,@Bar_Position_Name,@Site_Id,@Installation_Date)                            
        SET @BAR_ID = SCOPE_IDENTITY()                             
    END                            
    ELSE                            
    BEGIN                            
        SELECT @BAR_ID = Bar_Position_ID FROM bar_position WHERE SITE_ID = @SITE_ID AND Bar_Position_Name=@Bar_Position_Name                            
    END                            
                    
                    
    --SELECT distinct Machine_Name  from Machine_Class Where  Machine_Name= 'All That Glitters'                            
    --Check for Machine Class                            
    IF NOT EXISTS(SELECT Machine_Name FROM Machine_Class WHERE Machine_Name=@Machine_Class_Name)                            
    BEGIN                            
        --RETURN -6                            
        SELECT '-6' AS Result                  
        IF @@TRANCOUNT > 0                        
        BEGIN                        
            ROLLBACK TRANSACTION                     
        END                        
    RETURN                            
    END                            
                    
    --Check for Machine Type                            
    IF NOT EXISTS(SELECT Machine_Type_Code FROM Machine_Type WHERE Machine_Type_Code=@Machine_Type_Code)                            
    BEGIN                            
        --RETURN -7                            
        SELECT '-7' AS Result                            
        IF @@TRANCOUNT > 0                        
        BEGIN                        
         ROLLBACK TRANSACTION                        
        END                        
    RETURN                     
    END                            
                    
    --Check for Asset No                            
    IF NOT EXISTS (SELECT 1 FROM Machine WHERE Machine_Stock_No=@Machine_Stock_No)                            
    BEGIN                            
        --RETURN -8                            
        SELECT '-8' AS Result                            
        IF @@TRANCOUNT > 0                        
        BEGIN                        
            ROLLBACK TRANSACTION                        
        END                        
    RETURN                       
    END                 
                    
    --Check asset already allocated                            
    IF EXISTS(SELECT 1 FROM Installation JOIN Machine ON Installation.Machine_Id = Machine.Machine_Id                            
    WHERE Installation_END_date IS  NULL AND Machine.Machine_Stock_No=@AssetNo)                            
    --@Machine_Stock_No)                            
    BEGIN                            
        --RETURN -9                            
        SELECT '-9' AS Result                            
        IF @@TRANCOUNT > 0                        
        BEGIN                        
            ROLLBACK TRANSACTION                        
        END                        
    RETURN                            
    END                            
                    
--    --Check the Gametitle AND Asset matches                            
--    IF NOT EXISTS(SELECT * FROM Machine JOIN Machine_Class ON Machine.Machine_Class_Id = Machine_Class.Machine_Class_Id                             
--    WHERE Machine_Name=@Machine_Class_Name)                            
--    BEGIN                            
--        --RETURN -10                        
--        SELECT '-10' AS Result                            
--        IF @@TRANCOUNT > 0                        
--        BEGIN                        
--            ROLLBACK TRANSACTION                        
--        END                        
--        RETURN                            
--    END        
        
    IF LTRIM(RTRIM(@SerialNo)) <> ''        
    BEGIN        
        --Check the Serial No        
        IF NOT EXISTS(SELECT 1 FROM Machine WHERE Machine.Machine_Stock_No=@AssetNo and Machine.Machine_Manufacturers_Serial_No = @SerialNo)                            
      
        BEGIN                                      --RETURN -11                       
            SELECT '-11' AS Result                            
            IF @@TRANCOUNT > 0                        
            BEGIN                        
                ROLLBACK TRANSACTION                        
            END                        
            RETURN                            
        END            
    END        
        
    IF LTRIM(RTRIM(@AlternateSerialNo)) <> ''        
    BEGIN        
        --Check the Alternate Serial No        
        IF NOT EXISTS(SELECT 1 FROM Machine WHERE Machine.Machine_Stock_No=@AssetNo and Machine_Alternative_Serial_Numbers = @AlternateSerialNo)                            
        BEGIN                            
            --RETURN -12                        
            SELECT '-12' AS Result                            
            IF @@TRANCOUNT > 0                        
            BEGIN                        
                ROLLBACK TRANSACTION                        
            END                        
            RETURN                            
        END         
    END        
            
DECLARE @Machine_Class_ID Int            
            
--    SELECT @MACHINE_ID = Machine_ID, @Machine_Class_ID = Machine_Class.Machine_Class_ID  From Machine JOIN Machine_Class ON Machine.Machine_Class_Id = Machine_Class.Machine_Class_Id                             
--    WHERE Machine.Machine_Stock_No=@Machine_Stock_No AND Machine_Name=@Machine_Class_Name                            
--                
            
    --Everything is OK Creata A Machine            
  
            
Select @Machine_Class_ID = Machine_Class_ID From Machine_Class Where Machine_Name In (Select Machine_Class_Name From #NewTemp)            
          
Select @Machine_ID = M.Machine_ID            
 FROM Machine  M            
        INNER JOIN INSTALLATION I                         
            ON I.Machine_ID = M.Machine_ID                          
        WHERE                         
            I.HQInstallationID = @OldInstallation_ID            
            
            
  INSERT INTO Machine               
  (              
    [Machine_Class_ID],              
    [Operator_ID],              
    [Terms_Profile_ID],              
    [Depreciation_Policy_ID],              
    [Depreciation_Policy_Use_Default],                
    [Machine_Number_Of_Discs],              
    [Machine_Stock_No],              
    [Machine_Counter_Cash_In_Units],              
    [Machine_Counter_Cash_Out_Units],              
    [Machine_Counter_Tokens_In_Units],              
    [Machine_Counter_Tokens_Out_Units],              
    [Machine_Counter_Refill_Units],              
    [Machine_Counter_Jackpot_Units],              
    [Machine_Counter_Prize_Units],              
    [Machine_Counter_Tournament_Play_Units],              
    [Machine_Counter_JukeBox_Play_Units],              
    [Machine_Test],              
    [Machine_Status_Flag],              
    [Machine_Status],              
    [Machine_Start_Date],                
    [Machine_End_Date],              
    [Machine_Resale_Value],              
    [Machine_Sales_Invoice_Number],              
    [Machine_Sold_To],              
    [Machine_Type_Of_Sale],              
    [Machine_PROM_Version],              
    [Machine_Original_Purchase_Price],              
    [Machine_Sale_Price],              
    [Machine_Purchase_Invoice_Number],              
    [Depot_ID],              
    [Machine_AMEDIS_Variant_Code],              
    [Machine_Previous_Machine_ID],              
    [Machine_Manufacturers_Serial_No],              
    [Machine_Purchased_From],              
    [Machine_Depreciation_Start_Date],              
    [Machine_Last_PAT_Date],              
    [Machine_PAT_Required],              
    [Machine_Alternative_Serial_Numbers],              
    [Staff_ID],              
    [Machine_Due_In_Stock],              
    [Machine_Due_In_Stock_Date],              
    [Machine_Memo],              
    [Machine_Extra_Details],              
    [Staff_ID_Entered],              
    [Staff_ID_Deleted],              
    [Machine_Date_Entered],              
    [Machine_Date_Deleted],              
[Machine_Float_Maximum_Capacity],              
    [Machine_Float_200p_Capacity],              
    [Machine_Float_100p_Capacity],              
    [Machine_Float_50p_Capacity],              
    [Machine_Float_20p_Capacity],              
    [Machine_Float_10p_Capacity],              
    [Machine_Float_5p_Capacity],              
    [Machine_Float_2p_Capacity],              
    [Machine_Site_Planned_Movement_ID],              
    [Machine_Depot_Planned_Movement_ID],              
    [Machine_Category_ID]              
  )              
              
  SELECT @Machine_Class_ID,     -- [Machine_Class_ID]              
         [Operator_ID],              
         [Terms_Profile_ID],              
         [Depreciation_Policy_ID],              
         [Depreciation_Policy_Use_Default],              
         [Machine_Number_Of_Discs],              
         [Machine_Stock_No],              
         [Machine_Counter_Cash_In_Units],              
         [Machine_Counter_Cash_Out_Units],              
         [Machine_Counter_Tokens_In_Units],              
         [Machine_Counter_Tokens_Out_Units],              
         [Machine_Counter_Refill_Units],              
         @MACHINE_JACKPOT,              
         [Machine_Counter_Prize_Units],              
         [Machine_Counter_Tournament_Play_Units],              
         [Machine_Counter_JukeBox_Play_Units],              
         [Machine_Test],              
         1,      --[Machine_Status_Flag],              
         [Machine_Status],              
         @Installation_Date,       -- [Machine_Start_Date]              
         NULL,             -- [Machine_End_Date]              
         [Machine_Resale_Value],              
         [Machine_Sales_Invoice_Number],              
         [Machine_Sold_To],              
         NULL,    --[Machine_Type_Of_Sale],              
         [Machine_PROM_Version],              
         [Machine_Original_Purchase_Price],              
         [Machine_Sale_Price],              
         [Machine_Purchase_Invoice_Number],              
         [Depot_ID],              
         [Machine_AMEDIS_Variant_Code],      
   @Machine_ID, -- [Machine_Previous_Machine_ID]          
--         CASE WHEN ( COALESCE( [Machine_Previous_Machine_ID], 0 ) > 0 ) THEN [Machine_Previous_Machine_ID]              
--              ELSE @Machine_ID              
--              END,        -- [Machine_Previous_Machine_ID]              
         [Machine_Manufacturers_Serial_No],              
         [Machine_Purchased_From],              
         [Machine_Depreciation_Start_Date],              
         [Machine_Last_PAT_Date],              
         [Machine_PAT_Required],              
         [Machine_Alternative_Serial_Numbers],              
         [Staff_ID],              
         [Machine_Due_In_Stock],              
         [Machine_Due_In_Stock_Date],              
         [Machine_Memo],              
         [Machine_Extra_Details],              
         [Staff_ID_Entered],              
         NULL,  --[Staff_ID_Deleted],              
         [Machine_Date_Entered],              
         NULL,  --[Machine_Date_Deleted],              
         @FLOAT_ISSUED,              
         [Machine_Float_200p_Capacity],              
         [Machine_Float_100p_Capacity],              
         [Machine_Float_50p_Capacity],              
         [Machine_Float_20p_Capacity],              
         [Machine_Float_10p_Capacity],              
         [Machine_Float_5p_Capacity],               
         [Machine_Float_2p_Capacity],              
         [Machine_Site_Planned_Movement_ID],              
         [Machine_Depot_Planned_Movement_ID],              
         [Machine_Category_ID]               
              
    FROM Machine  WHERE            
       Machine_ID = @Machine_ID            
               
            
            
            
            
            
Declare @NewMachineID INT            
            
            
  SET @NewMachineID =SCOPE_IDENTITY()-- IDENT_CURRENT('Machine')            
            
            
            
            
                
    --Everything IS OK. Create a new Installation                            
    INSERT INTO installation       
  (                            
        Bar_Position_ID,                            
        Machine_ID,                            
        Installation_Start_Date,                            
        Installation_Start_Time,                  
        Datapak_ID,                            
        Installation_Price_Per_Play,                            
        Installation_RDC_Datapak_Type,                            
        Installation_RDC_Datapak_Version,                            
        Installation_Token_Value,                            
        Installation_Initial_Change,                            
        Installation_Initial_VTP,  
        Installation_Jackpot_Value,    
        Installation_Percentage_Payout ,

-- Adding the Installation table columns - Begin
		
	Installation_Reference,
--Start_Date,
--End_Date,
Coins_In_Counter,
Coins_Out_Counter,
Tokens_In_Counter,
Tokens_Out_Counter,
Prize_Counter,
Refill_Counter,
Tournament_Counter,
Jukebox_Counter,
--Previous_Installation,
Bagged_Cash_Installation_No,
Bagged_Cash_Amount,
Bagged_Cash_Float,
Installation_Out_Of_Order,
--Float_Issued,
Float_Issued_By,
Installation_Meter_1_Initial_Value,
Installation_Meter_1_Final_Value,
Installation_Meter_2_Initial_Value,
Installation_Meter_2_Final_Value,
Installation_Meter_3_Initial_Value,
Installation_Meter_3_Final_Value,
Installation_Meter_4_Initial_Value,
Installation_Meter_4_Final_Value,
Installation_Meter_5_Initial_Value,
Installation_Meter_5_Final_Value,
Installation_Meter_6_Initial_Value,
Installation_Meter_6_Final_Value,
Installation_Meter_7_Initial_Value,
Installation_Meter_7_Final_Value,
Installation_Meter_8_Initial_Value,
Installation_Meter_8_Final_Value,
Installation_Meter_9_Initial_Value,
Installation_Meter_9_Final_Value,
Installation_Meter_10_Initial_Value,
Installation_Meter_10_Final_Value,
Installation_Meter_11_Initial_Value,
Installation_Meter_11_Final_Value,
Installation_Meter_12_Initial_Value,
Installation_Meter_12_Final_Value,
Installation_Meter_13_Initial_Value,
Installation_Meter_13_Final_Value,
Installation_Meter_14_Initial_Value,
Installation_Meter_14_Final_Value,
Installation_Meter_15_Initial_Value,
Installation_Meter_15_Final_Value,
Installation_Meter_16_Initial_Value,
Installation_Meter_16_Final_Value,
Installation_Meter_17_Initial_Value,
Installation_Meter_17_Final_Value,
Installation_Meter_18_Initial_Value,
Installation_Meter_18_Final_Value,
Installation_Meter_19_Initial_Value,
Installation_Meter_19_Final_Value,
Installation_Meter_20_Initial_Value,
Installation_Meter_20_Final_Value,
Installation_Meter_21_Initial_Value,
Installation_Meter_21_Final_Value,
Installation_Meter_22_Initial_Value,
Installation_Meter_22_Final_Value,
Installation_Meter_23_Initial_Value,
Installation_Meter_23_Final_Value,
Installation_Meter_24_Initial_Value,
Installation_Meter_24_Final_Value,
Installation_Meter_25_Initial_Value,
Installation_Meter_25_Final_Value,
Installation_Meter_26_Initial_Value,
Installation_Meter_26_Final_Value,
Installation_Meter_27_Initial_Value,
Installation_Meter_27_Final_Value,
Installation_Meter_28_Initial_Value,
Installation_Meter_28_Final_Value,
Installation_Meter_29_Initial_Value,
Installation_Meter_29_Final_Value,
Installation_Meter_30_Initial_Value,
Installation_Meter_30_Final_Value,
Installation_Meter_31_Initial_Value,
Installation_Meter_31_Final_Value,
Installation_Meter_32_Initial_Value,
Installation_Meter_32_Final_Value,
Installation_Float_Status,
Installation_Initial_Meters_Coins_In,
Installation_Initial_Meters_Coins_Out,
Installation_Initial_Meters_Coin_Drop,
Installation_Initial_Meters_External_Credit,
Installation_Initial_Meters_Games_Bet,
Installation_Initial_Meters_Games_Won,
Installation_Initial_Meters_Notes,
Installation_Initial_Meters_Handpay,
Anticipated_Removal_Date,
Rental_Step_Down_Date,
Rent1,
Rent2,
Licence,
Installation_Out_Order,
Installation_Counter_Cash_In_Units,
--Installation_Counter_Cash_Out_Units,
--Installation_Counter_Token_In_Units,
--Installation_Counter_Token_Out_Units,
Installation_Counter_Refill_Units,
Installation_Counter_Jackpot_Units,
Installation_Counter_Prize_Units,
--Installation_Counter_Tournament_Units,
Installation_Counter_Jukebox_Play_Units,
Installation_Counter_Jukebox_Units,
Planned_Movement_ID,
Installation_RDC_Machine_Code,
Installation_RDC_Secondary_Machine_Code,
--Installation_Token_Value,
Installation_Games_Count,
Installation_Status,
Game_Part_Number,
Installation_MaxBet,
IsAuxSerialPortEnabled,
IsGatSerialPortEnabled,
IsSlotLinePortEnabled,
Port_Disabled_Status,
Voucher_Expire_Status,
FinalCollection_Status



		-- Adding the Installation table columns  - End     
 )                            
    VALUES (                            
        @BAR_ID,                            
        @NewMachineID,                            
        @Installation_Date,                            
        @Installation_Time,                  
        @DATAPAK,                  
        @Price_of_play,                            
        0, 0, @INSTALLATION_TOKEN_VALUE, 0, 0   , @MACHINE_JACKPOT, @PERCENTAGEPAYOUT     ,
-- Adding the Installation table columns  - Begin
		
		--@HQ_Installation_No,
@Installation_Reference,
--@Start_Date,
--@End_Date,
@Coins_In_Counter,
@Coins_Out_Counter,
@Tokens_In_Counter,
@Tokens_Out_Counter,
@Prize_Counter,
@Refill_Counter,
@Tournament_Counter,
@Jukebox_Counter,
--@Previous_Installation,
@Bagged_Cash_Installation_No,
@Bagged_Cash_Amount,
@Bagged_Cash_Float,
@Installation_Out_Of_Order,
--@Float_Issued,
@Float_Issued_By,
@Installation_Meter_1_Initial_Value,
@Installation_Meter_1_Final_Value,
@Installation_Meter_2_Initial_Value,
@Installation_Meter_2_Final_Value,
@Installation_Meter_3_Initial_Value,
@Installation_Meter_3_Final_Value,
@Installation_Meter_4_Initial_Value,
@Installation_Meter_4_Final_Value,
@Installation_Meter_5_Initial_Value,
@Installation_Meter_5_Final_Value,
@Installation_Meter_6_Initial_Value,
@Installation_Meter_6_Final_Value,
@Installation_Meter_7_Initial_Value,
@Installation_Meter_7_Final_Value,
@Installation_Meter_8_Initial_Value,
@Installation_Meter_8_Final_Value,
@Installation_Meter_9_Initial_Value,
@Installation_Meter_9_Final_Value,
@Installation_Meter_10_Initial_Value,
@Installation_Meter_10_Final_Value,
@Installation_Meter_11_Initial_Value,
@Installation_Meter_11_Final_Value,
@Installation_Meter_12_Initial_Value,
@Installation_Meter_12_Final_Value,
@Installation_Meter_13_Initial_Value,
@Installation_Meter_13_Final_Value,
@Installation_Meter_14_Initial_Value,
@Installation_Meter_14_Final_Value,
@Installation_Meter_15_Initial_Value,
@Installation_Meter_15_Final_Value,
@Installation_Meter_16_Initial_Value,
@Installation_Meter_16_Final_Value,
@Installation_Meter_17_Initial_Value,
@Installation_Meter_17_Final_Value,
@Installation_Meter_18_Initial_Value,
@Installation_Meter_18_Final_Value,
@Installation_Meter_19_Initial_Value,
@Installation_Meter_19_Final_Value,
@Installation_Meter_20_Initial_Value,
@Installation_Meter_20_Final_Value,
@Installation_Meter_21_Initial_Value,
@Installation_Meter_21_Final_Value,
@Installation_Meter_22_Initial_Value,
@Installation_Meter_22_Final_Value,
@Installation_Meter_23_Initial_Value,
@Installation_Meter_23_Final_Value,
@Installation_Meter_24_Initial_Value,
@Installation_Meter_24_Final_Value,
@Installation_Meter_25_Initial_Value,
@Installation_Meter_25_Final_Value,
@Installation_Meter_26_Initial_Value,
@Installation_Meter_26_Final_Value,
@Installation_Meter_27_Initial_Value,
@Installation_Meter_27_Final_Value,
@Installation_Meter_28_Initial_Value,
@Installation_Meter_28_Final_Value,
@Installation_Meter_29_Initial_Value,
@Installation_Meter_29_Final_Value,
@Installation_Meter_30_Initial_Value,
@Installation_Meter_30_Final_Value,
@Installation_Meter_31_Initial_Value,
@Installation_Meter_31_Final_Value,
@Installation_Meter_32_Initial_Value,
@Installation_Meter_32_Final_Value,
@Installation_Float_Status,
@Installation_Initial_Meters_Coins_In,
@Installation_Initial_Meters_Coins_Out,
@Installation_Initial_Meters_Coin_Drop,
@Installation_Initial_Meters_External_Credit,
@Installation_Initial_Meters_Games_Bet,
@Installation_Initial_Meters_Games_Won,
@Installation_Initial_Meters_Notes,
@Installation_Initial_Meters_Handpay,
@Anticipated_Removal_Date,
@Rental_Step_Down_Date,
@Rent1,
@Rent2,
@Licence,
@Installation_Out_Order,
@Installation_Counter_Cash_In_Units,
--@Installation_Counter_Cash_Out_Units,
--@Installation_Counter_Token_In_Units,
--@Installation_Counter_Token_Out_Units,
@Installation_Counter_Refill_Units,
@Installation_Counter_Jackpot_Units,
@Installation_Counter_Prize_Units,
--@Installation_Counter_Tournament_Units,
@Installation_Counter_Jukebox_Play_Units,
@Installation_Counter_Jukebox_Units,
@Planned_Movement_ID,
@Installation_RDC_Machine_Code,
@Installation_RDC_Secondary_Machine_Code,
--@Installation_Token_Value,
@Installation_Games_Count,
@Installation_Status,
@Game_Part_Number,
@Installation_MaxBet,
@IsAuxSerialPortEnabled,
@IsGatSerialPortEnabled,
@IsSlotLinePortEnabled,
@Port_Disabled_Status,
@Voucher_Expire_Status,
@FinalCollection_Status


		-- Adding the Installation table columns  - End                      
        )                            
    DECLARE @Identity INT
    SET @Identity =   SCOPE_IDENTITY()            
    UPDATE Installation SET HQInstallationID = @Identity WHERE Installation_ID = @Identity                          
    SELECT @Identity AS Result                         
    IF @@TRANCOUNT > 0                        
    BEGIN                        
        COMMIT TRANSACTION                        
    END                        
                    
    /*----------     End of New Installation  ------------ */                        
    /*----------     End of Usp_RemoveOldAndAddNewInstallation  ------------ */                        
                    
END

GO

