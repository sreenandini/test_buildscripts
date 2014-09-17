USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[USP_GetHQInstallationID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[USP_GetHQInstallationID]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

                              
                                
CREATE PROCEDURE USP_GetHQInstallationID  
(@XML nVARCHAR(MAX))                                        
AS                                        
BEGIN                                        
                                        
DECLARE @INT INT                                        
Set @INT  = 0                                        
EXEC sp_xml_preparedocument @INT OUTPUT, @XML                                        
              
IF Exists (Select * From sysobjects where name like 'NEWXMLINSTALL' And xType = 'u')              
DROP table                       NEWXMLINSTALL                  
                                        
SELECT * INTO #temp FROM OPENXML                                         
(@INT, '/NEWINSTALLATION',2)                                        
WITH                                         
 (Site_Code   VARCHAR(50),                                        
 Installation_Date DATETIME,-- VARCHAR(30),        
 --Installation_Time   VARCHAR(50),                                
 DATAPAK  INT,                                
 Zone_Name  VARCHAR(100),                                        
HQ_Installation_No  INT,                                        
 Bar_Pos_Name   VARCHAR(50),                                        
 Asset_No   VARCHAR(50),                                        
 Machine_Class_Name   VARCHAR(50),                                        
 Machine_Class_Price_Of_play  INT,                                        
 Machine_Type_Code   VARCHAR(50),                      
 SERIALNO   VARCHAR(50),                      
 ALTERNATESERIALNO   VARCHAR(50),            
 MACHINE_JACKPOT  INT,            
 FLOAT_ISSUED  REAL,            
 INSTALLATION_TOKEN_VALUE  INT,          
 PERCENTAGEPAYOUT  INT,       
 Datapak_Variant INT ,  
  
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
                                  
Select * INto NEWXMLINSTALL From #temp              
                                        
DECLARE @Zone_Name  VARCHAR(30)  
DECLARE @BAR_ID INT  
DECLARE @MACHINE_ID INT  
DECLARE @Site_Id  VARCHAR(30)  
DECLARE @Installation_Date  VARCHAR(30)  
DECLARE @Installation_Time  VARCHAR(50)  
DECLARE @Bar_Position_Name  VARCHAR(50)  
DECLARE @DATAPAK INT  
DECLARE @AssetNo  VARCHAR(10)  
DECLARE @Price_of_play INT  
DECLARE @Machine_Class_Name  VARCHAR(50)  
DECLARE @Machine_Type_Code  VARCHAR(20)  
DECLARE @Machine_Stock_No  VARCHAR(50)  
DECLARE @ZoneId INT  
DECLARE @AlternateSerialNo  VARCHAR(50)   
DECLARE @SerialNo  VARCHAR(50)  
DECLARE @MACHINE_JACKPOT INT  
DECLARE @FLOAT_ISSUED REAL  
DECLARE @INSTALLATION_TOKEN_VALUE INT  
DECLARE @PERCENTAGEPAYOUT INT  
DECLARE @IsRegulatoryEnabled  VARCHAR(50)  
DECLARE @RegulatoryType  VARCHAR(50)  
DECLARE @MachineNewInstall INT   
DECLARE @InstallationNo INT   
DECLARE @Datapak_Variant INT   
         
   --Installation Table columns - Start  
  
--DECLARE @HQ_Installation_No  INT  
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
DECLARE @Installation_Initial_Meters_Coins_In INT  
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
--Installation Table Columns -End  
     
                                      
SET @ZoneId=0                                        
                                     
SELECT @Site_Id = Site_ID FROM Site                                         
INNER JOIN #temp ON #temp.Site_Code = Site.Site_Code                                        
               
                                        
SELECT                                         
 @Zone_Name = Zone_Name,                                         
 @Installation_Date = CONVERT( VARCHAR(20), Installation_Date, 106),        
 @Installation_Time = CONVERT( VARCHAR(20), Installation_Date, 108),--Installation_Time ,  
 @DATAPAK = DATAPAK,                        
 @Datapak_Variant = Datapak_Variant,           
 @Bar_Position_Name = Bar_Pos_Name ,                                        
 @AssetNo = Asset_No,                                        
 @Price_of_play = Machine_Class_Price_Of_play,                                        
 @Machine_Class_Name = Machine_Class_Name,                                        
 @Machine_Type_Code = Machine_Type_Code,                         
 @Machine_Stock_No = Asset_No,                      
 @SerialNo = SerialNo,                      
 @AlternateSerialNo = AlternateSerialNo,            
 @MACHINE_JACKPOT = MACHINE_JACKPOT,            
 @FLOAT_ISSUED = FLOAT_ISSUED,            
 @INSTALLATION_TOKEN_VALUE = INSTALLATION_TOKEN_VALUE,          
 @PERCENTAGEPAYOUT = PERCENTAGEPAYOUT  ,    
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
@IsAuxSerialPortEnabled = ISNULL(IsAuxSerialPortEnabled, 0),  
@IsGatSerialPortEnabled = ISNULL(IsGatSerialPortEnabled, 0),  
@IsSlotLinePortEnabled = ISNULL(IsSlotLinePortEnabled, 0),  
@Port_Disabled_Status =ISNULL(Port_Disabled_Status, 0) ,  
@Voucher_Expire_Status =Voucher_Expire_Status,  
@FinalCollection_Status=FinalCollection_Status  
  
  
 --Installation table new columns-End    
FROM                                         
 #temp                           
                                        
SELECT @ZoneId=Zone_Id FROM Zone WHERE Zone_Name=@Zone_Name                                        
                                        
                                        
--Check for Zone                                      
IF NOT EXISTS(SELECT Zone_id FROM zone WHERE Zone_Name=@Zone_Name AND Site_ID = @Site_ID)                                        
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
                                        
                                        
--Select distinct Machine_Name  from Machine_Class Where  Machine_Name= 'All That Glitters'                                        
--Check for Machine Class                 
IF NOT EXISTS(SELECT Machine_Name FROM Machine_Class WHERE Machine_Name=@Machine_Class_Name)                                        
 BEGIN                                        
  --return -1                                        
  SELECT '-1' As Result                                        
  Return                       
 END                                        
                                        
--Check for Machine Type                                        
IF NOT EXISTS(SELECT Machine_Type_Code FROM Machine_Type WHERE Machine_Type_Code=@Machine_Type_Code)                                        
 BEGIN                                        
  --return -2                                        
  SELECT '-2' As Result                                        
  Return                                        
 END                                        
                                        
--Check for Asset No                                        
IF NOT EXISTS (SELECT 1 FROM Machine WHERE Machine_Stock_No=@Machine_Stock_No)                                        
 BEGIN                                        
  --return -3                                        
  SELECT '-3' As Result                                        
  Return                                        
 END               
        
IF ISNULL((SELECT Machine_End_Date FROM Machine WHERE MACHINE_ID = (        
                                                                SELECT MAX(M.Machine_ID) FROM MACHINE M        
                                                                INNER JOIN MACHINE_CLASS MC ON MC.Machine_Class_ID = M.Machine_Class_ID        
                                                                INNER JOIN MACHINE_TYPE MT ON MT.Machine_Type_ID = MC.Machine_Type_ID        
                                                                WHERE MT.Machine_Type_Code=@Machine_Type_Code         
                                                                AND MC.Machine_Name=@Machine_Class_Name        
                                                                AND M.Machine_Stock_No=@Machine_Stock_No )        
            ), '') <> ''         
  BEGIN                                        
  --return -3                                        
  SELECT '-3' As Result                                        
  Return                                        
 END                                  
                                        
--Check asset already allocated                                        
IF EXISTS(SELECT 1 FROM Installation JOIN Machine ON Installation.Machine_Id = Machine.Machine_Id                                        
WHERE Installation_END_date IS  NULL AND Machine.Machine_Stock_No=@AssetNo)                                       
--@Machine_Stock_No)                                        
 BEGIN                                        
  --return -4                                        
  SELECT '-4' As Result                                        
  Return                                        
 END                                        
                                        
--Check the Gametitle AND Asset matches                                        
IF NOT EXISTS(SELECT 1 FROM Machine JOIN Machine_Class ON Machine.Machine_Class_Id = Machine_Class.Machine_Class_Id                                         
WHERE Machine.Machine_Stock_No=@Machine_Stock_No AND Machine_Name=@Machine_Class_Name)                                        
 BEGIN                                        
  SELECT '-5' As Result                                        
  Return                                        
 END                         
                      
                    
IF LTRIM(RTRIM(@SerialNo)) <> ''                    
BEGIN                    
  --Check Serial No                       
IF NOT EXISTS(SELECT 1 FROM Machine WHERE  Machine.Machine_Stock_No=@Machine_Stock_No and Machine.Machine_Manufacturers_Serial_No = @SerialNo)                                                                       
   BEGIN                                        
       UPDATE Machine        
          SET Machine_Manufacturers_Serial_No = @SerialNo        
        WHERE Machine_Stock_No=@Machine_Stock_No        
        
--  SELECT '-6' As Result                                        
--  Return                                        
  END                             
END                    
                      
IF LTRIM(RTRIM(@AlternateSerialNo)) <> ''                    
BEGIN                    
    --Check Alternate Serial No                      
    IF NOT EXISTS(SELECT 1 FROM Machine WHERE  Machine.Machine_Stock_No=@Machine_Stock_No and Machine.Machine_Alternative_Serial_Numbers = @AlternateSerialNo)                                        
     BEGIN                                        
       UPDATE Machine        
          SET Machine_Alternative_Serial_Numbers = @AlternateSerialNo        
        WHERE Machine_Stock_No=@Machine_Stock_No        
                
      --SELECT '-7' As Result                                        
      --Return                                        
     END                             
END                    
                      
                                     
Select @MACHINE_ID = Machine_ID From Machine JOIN Machine_Class ON Machine.Machine_Class_Id = Machine_Class.Machine_Class_Id                                         
WHERE Machine.Machine_Stock_No=@Machine_Stock_No AND Machine_Name=@Machine_Class_Name                                        
                                        
--Everything IS OK. Create a new Installation                                        
INSERT INTO installation                               
                                       
(                                        
Bar_Position_ID,                                        
Machine_ID,                                        
Installation_Start_Date,                                        
Installation_Start_Time,  
Datapak_ID,                                  
Datapak_Variant,              
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
@MACHINE_ID,             
@Installation_Date,                                        
@Installation_Time,  
@DATAPAK,                             
@Datapak_Variant,            
@Price_of_play,                 
0, 0,             
@INSTALLATION_TOKEN_VALUE,             
0, 0 , @MACHINE_JACKPOT, @PERCENTAGEPAYOUT  ,  
  
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
SELECT @Identity    
INSERT INTO Floor_Financials (Installation_No) VALUES (@Identity)  

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
                
   
UPDATE             
    Machine             
SET             
    Machine_status_Flag = 1,             
    Machine_Counter_Jackpot_Units = @MACHINE_JACKPOT,            
    Machine_Float_Maximum_Capacity = @FLOAT_ISSUED 
    OUTPUT INSERTED.Machine_ID,
                                DELETED.Machine_Status_Flag, INSERTED.Machine_Status_Flag,
                                DELETED.Machine_Class_ID, INSERTED.Machine_Class_ID,
                                DELETED.CMPGameType, INSERTED.CMPGameType, 
                                DELETED.Machine_Stock_No, INSERTED.Machine_Stock_No
    INTO @_Modified
           
WHERE             
    Machine_ID = @Machine_Id    
    
IF EXISTS(
                                SELECT 1
                                FROM   @_Modified m
                                WHERE  m.FlagChanged = 1 OR
                                                                m.GameIDChanged = 1 OR
                                                                m.CMPGameTypeChanged = 1 OR
                                                                m.StockNoChanged = 1
                )
                BEGIN
                                EXEC [dbo].[usp_EBS_UpdateMachineDetails] @Machine_Id 
                END
                        
/*****************************************************************************************************
* (CR# 191254 : EBS Communication Service) - MODIFIED BY Venkatesh.H  (END)
*****************************************************************************************************/
          
-- Update Bar_Postion SET  Bar_Position_Jackpot = @MACHINE_JACKPOT WHERE Bar_Position_ID = @BAR_ID          
                                      
UPDATE Installation SET HQInstallationID = @Identity WHERE Installation_ID = @Identity                                      
SELECT HQInstallationID AS Result FROM Installation WHERE Installation_ID = @Identity           
SELECT @InstallationNo = MAX(Installation_ID) FROM Installation        
        
--AAMS Changes.                                   
SELECT @IsRegulatoryEnabled = ISNULL(Setting_Value,'False') FROM Setting WHERE Setting_Name = 'IsRegulatoryEnabled'        
SELECT @RegulatoryType = ISNULL(Setting_Value,'A') FROM Setting WHERE Setting_Name = 'RegulatoryType'        
             
IF @IsRegulatoryEnabled = 'True' AND @RegulatoryType = 'AAMS'                                 
BEGIN        
        
 UPDATE BMC_AAMS_Details        
 SET BAD_Entity_Command = 'Enabled', BAD_Updated_Date = Getdate(),        
 BAD_Comments = 'Installation. Enable the machine.'        
 WHERE BAD_Reference_ID = @Machine_Id AND BAD_AAMS_Entity_Type = 3        
        
 SELECT @MachineNewInstall = ISNULL(Machine_New_Install,0) FROM Machine WHERE Machine_ID = @Machine_Id        
        
 UPDATE             
  Machine             
 SET             
  Machine_status_Flag = 8,--AAMSPENDINGINSTALLATION             
  Machine_New_Install = 0           
 WHERE             
  Machine_ID = @Machine_Id          
         
 IF @MachineNewInstall = 1        
 BEGIN        
  EXEC usp_InsertBMCBASExportRecord @Machine_Id, 3, 305, NULL        
 END        
 ELSE        
 BEGIN        
  EXEC usp_InsertBMCBASExportRecord @Machine_Id, 3, 306, NULL , 7, 'Depot to Venue'       
 END        
        
 INSERT INTO dbo.LGE_Export_History(LGE_EH_Date, LGE_EH_Reference, LGE_EH_Type)          
 VALUES(GETDATE(), @InstallationNo,'AUTOINSTALLATION')          
         
END        
  exec usp_Export_History @InstallationNo,'AFTENABLEDISABLE',@Site_ID     
  DECLARE @IsEmpcardEnabled VARCHAR(10)
  SELECT @IsEmpcardEnabled = setting_value FROM setting WHERE setting_name ='IsEmployeeCardtrackingEnabled'
  

  exec usp_Export_History @Machine_Id,'MACHINEUPDATE',@Site_ID    
  exec usp_Export_History @Site_ID,'SITESETUP',@Site_ID        
End   


GO

