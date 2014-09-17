USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_ExportModelDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_ExportModelDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*
 *	this stored procedure is to export all the model details to Exchange
 *
 *	Change History:
 *	
 *	Sudarsan S		20-05-2008		created
 *  Sudarsan S		03-07-2008		handled for one particular model alone
 *  Naveen  21-08-2008  Added New Parameter for Model Detail By AssetNumber for Planned Conversion 
*/
CREATE PROCEDURE [dbo].[rsp_ExportModelDetails]        
 @ID VARCHAR(50), @AssetNumber Varchar(100) = ''        
AS        
        
BEGIN            
          
DECLARE @Model XML              

if @ID = '' OR @ID IS NULL 
	SET @ID = 'ALL'

IF (@AssetNumber = '')            
BEGIN              
-- SET @Model =     
 IF(@ID = 'ALL' )    
 BEGIN     
  --      SELECT Machine_Class.*,               
  --        Machine_Type.*,              
  --        Manufacturer.*              
  --      FROM dbo.Machine_Class Machine_Class              
  --     LEFT JOIN dbo.Manufacturer Manufacturer ON Machine_Class.Manufacturer_ID = Manufacturer.Manufacturer_ID              
  --     LEFT JOIN dbo.Machine_Type Machine_Type ON Machine_Class.Machine_Type_ID = Machine_Type.Machine_Type_ID              
  --      FOR XML AUTO, TYPE, ELEMENTS, ROOT('MODEL')              
     
  /*-------------------------------------------------------------------------------------------------------    
  --START ALL     
  --------------------------------------------------------------------------------------------------------*/    
  DECLARE @Count Int     
  DECLARE @IterationCnt int     
  DECLARE @i int    
  DECLARE  @MaxModelCnt INT    
 --set @MaxModelCnt=200  -- NUMBER OF ASSETS PER ROW   
   
 SELECT @MaxModelCnt=setting_value FROM setting WHERE setting_name='MaxModelExportCount'  
 PRINT @MaxModelCnt  
 --GET MAX EXPORT SIZE FROM CONFIG   
 IF isnull(@MaxModelCnt,0)=0  
 BEGIN  
  SET @MaxModelCnt=100  
  IF NOT EXISTS(SELECT setting_value FROM setting WHERE setting_name='MaxModelExportCount')  
  BEGIN  
   INSERT INTO setting VALUES('MaxModelExportCount',@MaxModelCnt )  
  END  
  ELSE  
  BEGIN  
   UPDATE  setting SET setting_value= @MaxModelCnt Where Setting_Name='MaxModelExportCount'   
  END  
 END  
  
  
  SELECT @Count=COUNT('D') FROM Machine_Class    
    
  SEt @i=1    
    
  SET @IterationCnt = @Count/@MaxModelCnt    
  SET @IterationCnt = @IterationCnt + CASE WHEN (@Count%@MaxModelCnt)>0 THEN 1 ELSE 0 END     
  --SELECT @IterationCnt    
    
  DECLARE @tempExpotTbl TABLE(rowNumber int,Machine_Class_ID int, Batch int)    
    
  insert into @tempExpotTbl    
   SELECT  ROW_NUMBER() OVER(ORDER BY Machine_Class_ID DESC) AS 'RowNumber', Machine_Class_ID, @i    
   from Machine_Class     
    
  DECLARE @Result table(xmlTxt xml)    
    
    WHILE @i <= @IterationCnt    
    BEGIN     
   INSERT INTO @Result    
   SELECT (    
   SELECT     
     -- SRART MACHINE CLASS    
     MC.Machine_Class_ID AS MC_1,    
     MC.Machine_Type_ID AS MC_2,    
     MC.Manufacturer_ID AS MC_3,    
     MC.Depreciation_Policy_ID AS MC_4,    
     MC.Depreciation_Policy_Use_Default AS MC_5,    
     MC.Machine_Class_Category AS MC_6,    
     MC.Machine_Class_Model_Code AS MC_7,    
     MC.Machine_BACTA_Code AS MC_8,    
     MC.Machine_Name AS MC_9,    
     MC.Machine_Hopper_Type AS MC_10,    
     MC.Machine_Max_No_Of_Discs AS MC_11,    
     MC.Machine_Disc_Type AS MC_12,    
     MC.Label_ID AS MC_13,    
     MC.Machine_Picture_Label AS MC_14,    
     MC.Machine_CD_Numbering AS MC_15,    
     MC.Machine_Track_Numbering AS MC_16,    
     MC.Machine_Class_Counter_Cash_In_Units AS MC_17,    
     MC.Machine_Class_Counter_Cash_Out_Units AS MC_18,    
     MC.Machine_Class_Counter_Tokens_In_Units AS MC_19,    
     MC.Machine_Class_Counter_Tokens_Out_Units AS MC_20,    
     MC.Machine_Class_Counter_Refill_Units AS MC_21,    
     MC.Machine_Class_Counter_Jackpot_Units AS MC_22,    
     MC.Machine_Class_Counter_Prize_Units AS MC_23,    
     MC.Machine_Class_Counter_Tournament_Play_Units AS MC_24,    
     MC.Machine_Class_Counter_JukeBox_Play_Units AS MC_25,    
     MC.Machine_Class_Prize_Machine AS MC_26,    
     MC.Machine_Class_Max_Float_Level AS MC_27,    
     MC.Machine_Class_Default_Coin_Mech AS MC_28,    
     MC.Machine_Class_Default_Note_Acceptor AS MC_29,    
     MC.Machine_Class_Test_Machine AS MC_30,    
     MC.Machine_Class_Delisted AS MC_31,    
     MC.Machine_Class_Features AS MC_32,    
     MC.Machine_Class_Tolerance_Cash_In AS MC_33,    
     MC.Machine_Class_Tolerance_Cash_Out AS MC_34,    
     MC.Machine_Class_Tolerance_Tokens_In AS MC_35,    
     MC.Machine_Class_Tolerance_Tokens_Out AS MC_36,    
     MC.Machine_Class_Tolerance_Refills AS MC_37,    
     MC.Machine_Class_Tolerance_Tournament AS MC_38,    
     MC.Machine_Class_Tolerance_Jukebox AS MC_39,    
     MC.Machine_Class_Tolerance_Prize AS MC_40,    
     MC.Machine_Class_Float_200 AS MC_41,    
     MC.Machine_Class_Float_100 AS MC_42,    
     MC.Machine_Class_Float_50 AS MC_43,    
     MC.Machine_Class_Float_20 AS MC_44,    
     MC.Machine_Class_Float_10 AS MC_45,    
     MC.Machine_Class_Float_5 AS MC_46,    
     MC.Machine_Class_Float_2 AS MC_47,    
     MC.Machine_Class_Config_Div_Level_1 AS MC_48,    
     MC.Machine_Class_Config_Div_Level_2 AS MC_49,    
     MC.Machine_Class_Config_Div_Level_3 AS MC_50,    
     MC.Machine_Class_Config_Div_Level_4 AS MC_51,    
     MC.Machine_Class_Config_Single_Comm AS MC_52,    
     MC.Machine_Class_Config_Machine_Mode AS MC_53,    
     MC.Machine_Class_Config_Machine_Version AS MC_54,    
     MC.Machine_Class_Config_Attract_Mode_Text AS MC_55,    
     MC.Machine_Class_Config_Bank_Options AS MC_56,    
     MC.Machine_Class_Config_Max_Credit_Limit AS MC_57,    
     MC.Machine_Class_Config_Max_Bank_Value AS MC_58,    
     MC.Machine_Class_Price_Of_Play AS MC_59,    
     MC.Machine_Class_Jackpot AS MC_60,    
     MC.Machine_Class_Percent_Cash_Payout AS MC_61,    
     MC.Machine_Class_Percent_Token_Payout AS MC_62,    
     MC.Machine_Class_Speaker_Volume AS MC_63,    
     MC.Machine_Class_SP_Features AS MC_64,    
     MC.Machine_Class_Release_Date AS MC_65,    
     MC.Machine_Class_Category_ID AS MC_66,    
     MC.Machine_Class_Is_Ticket AS MC_67,    
     MC.Machine_Class_RecreateCancelledCredits AS MC_68,    
     MC.Machine_Class_JackpotAddedToCancelledCredits AS MC_69,    
     MC.Machine_Class_AddTrueCoinInToDrop AS MC_70,    
     MC.Machine_Class_UseCancelledCreditsAsTicketsPrinted AS MC_71,    
     MC.Machine_Class_RecreateTicketsInsertedfromDrop AS MC_72,    
     MC.Meter_Rollover AS MC_73,    
    -- MC.Machine_Class_Occupancy_Games_Per_Hour AS MC_74,    
     MC.Validation_Length AS MC_75,    
     -- END  MACHINE CLASS    
     --START Machine_Type    
     MT.Machine_Type_ID AS MT_1,    
     MT.Depreciation_Policy_ID AS MT_2,    
     MT.Machine_Type_AMEDIS_ID AS MT_3,    
     MT.Machine_Type_Code AS MT_4,    
     MT.Machine_Type_Description AS MT_5,    
     MT.Machine_Type_Prize_Type AS MT_6,    
     MT.Machine_Type_Needs_PPL AS MT_7,    
     MT.Machine_Type_Icon_Ref AS MT_8,    
     MT.Machine_Type_Income_Ledger_Code AS MT_9,    
     MT.Machine_Type_Royalty_Ledger_Code AS MT_10,    
     MT.Machine_Type_Site_Icon AS MT_11,    
     MT.Machine_Type_Category AS MT_12,    
     MT.IsNonGamingAssetType AS MT_13,    
     --END Machine_Type    
     --START Manufacturer    
     MF.Manufacturer_ID AS MF_1,    
     MF.Manufacturer_Name AS MF_2,    
     MF.Manufacturer_Service_Contact AS MF_3,    
     MF.Manufacturer_Service_EMail AS MF_4,    
     MF.Manufacturer_Service_Tel AS MF_5,    
     MF.Manufacturer_Service_Address AS MF_6,    
     MF.Manufacturer_Service_Postcode AS MF_7,    
     MF.Manufacturer_Sales_Contact AS MF_8,    
     MF.Manufacturer_Sales_EMail AS MF_9,    
     MF.Manufacturer_Sales_Tel AS MF_10,    
     MF.Manufacturer_Sales_Address AS MF_11,    
     MF.Manufacturer_Sales_Postcode AS MF_12,    
     MF.Manufacturer_Code AS MF_13,    
     MF.Manufacturer_Coins_In_Meter_Used AS MF_14,    
     MF.Manufacturer_Coins_Out_Meter_Used AS MF_15,    
     MF.Manufacturer_Coin_Drop_Meter_Used AS MF_16,    
     MF.Manufacturer_Handpay_Meter_Used AS MF_17,    
     MF.Manufacturer_External_Credits_Meter_Used AS MF_18,    
     MF.Manufacturer_Games_Bet_Meter_Used AS MF_19,    
     MF.Manufacturer_Games_Won_Meter_Used AS MF_20,    
     MF.Manufacturer_Notes_Meter_Used AS MF_21,    
     MF.Manufacturer_Single_Coin_Build AS MF_22,    
     MF.Manufacturer_Handpay_Added_To_Coin_Out AS MF_23    
     --END Manufacturer    
   FROM dbo.Machine_Class MC     
   LEFT JOIN dbo.Manufacturer MF ON MC.Manufacturer_ID = MF.Manufacturer_ID              
   LEFT JOIN dbo.Machine_Type MT ON MC.Machine_Type_ID = MT.Machine_Type_ID              
   WHERE  MC.Machine_Class_ID IN    
   (    
    --UPDATE @tempExpotTbl SET rowNumber=@i     
    SELECT Machine_Class_ID FROM @tempExpotTbl    
    WHERE  RowNumber  BETWEEN   ((@MaxModelCnt *@i) -(@MaxModelCnt-1) )  AND   (@MaxModelCnt *@i)     
   )      
   FOR XML AUTO, TYPE, ELEMENTS, ROOT('MODEL') )    
      
   SET @i=@i+1    
    END     
 SELECT * FROM @Result    
  /*-------------------------------------------------------------------------------------------------------    
  --END ALL     
  --------------------------------------------------------------------------------------------------------*/    
    
    
 END           
 ELSE    
 BEGIN 
	  DECLARE @machineStockNo VARCHAR(MAX)
	  SELECT @machineStockNo = COALESCE(@machineStockNo, ',') +
	       CASE 
	            WHEN (
	                     @machineStockNo LIKE '%,' + CAST(Machine_Stock_No AS VARCHAR(50)) 
	                     + ',%'
	                 ) THEN ''
	            ELSE CAST(Machine_Stock_No AS VARCHAR(50)) + ','
	       END
	       FROM dbo.Machine_Class  MC  LEFT JOIN dbo.Machine M ON M.Machine_Class_Id = MC.Machine_Class_ID   
	  WHERE MC.Machine_Class_ID = CAST(@ID AS INT)             
	  
      SELECT   
   -- SRART MACHINE CLASS    
      MC.Machine_Class_ID AS MC_1,    
      MC.Machine_Type_ID AS MC_2,    
      MC.Manufacturer_ID AS MC_3,    
      MC.Depreciation_Policy_ID AS MC_4,    
      MC.Depreciation_Policy_Use_Default AS MC_5,    
      MC.Machine_Class_Category AS MC_6,    
      MC.Machine_Class_Model_Code AS MC_7,    
      MC.Machine_BACTA_Code AS MC_8,    
      MC.Machine_Name AS MC_9,    
      MC.Machine_Hopper_Type AS MC_10,    
      MC.Machine_Max_No_Of_Discs AS MC_11,    
      MC.Machine_Disc_Type AS MC_12,    
      MC.Label_ID AS MC_13,    
      MC.Machine_Picture_Label AS MC_14,    
      MC.Machine_CD_Numbering AS MC_15,    
      MC.Machine_Track_Numbering AS MC_16,    
      MC.Machine_Class_Counter_Cash_In_Units AS MC_17,    
      MC.Machine_Class_Counter_Cash_Out_Units AS MC_18,    
      MC.Machine_Class_Counter_Tokens_In_Units AS MC_19,    
      MC.Machine_Class_Counter_Tokens_Out_Units AS MC_20,    
      MC.Machine_Class_Counter_Refill_Units AS MC_21,    
      MC.Machine_Class_Counter_Jackpot_Units AS MC_22,    
      MC.Machine_Class_Counter_Prize_Units AS MC_23,    
      MC.Machine_Class_Counter_Tournament_Play_Units AS MC_24,    
      MC.Machine_Class_Counter_JukeBox_Play_Units AS MC_25,    
      MC.Machine_Class_Prize_Machine AS MC_26,    
      MC.Machine_Class_Max_Float_Level AS MC_27,    
      MC.Machine_Class_Default_Coin_Mech AS MC_28,    
      MC.Machine_Class_Default_Note_Acceptor AS MC_29,    
      MC.Machine_Class_Test_Machine AS MC_30,    
      MC.Machine_Class_Delisted AS MC_31,    
      MC.Machine_Class_Features AS MC_32,    
      MC.Machine_Class_Tolerance_Cash_In AS MC_33,    
      MC.Machine_Class_Tolerance_Cash_Out AS MC_34,    
      MC.Machine_Class_Tolerance_Tokens_In AS MC_35,    
      MC.Machine_Class_Tolerance_Tokens_Out AS MC_36,    
      MC.Machine_Class_Tolerance_Refills AS MC_37,    
      MC.Machine_Class_Tolerance_Tournament AS MC_38,    
      MC.Machine_Class_Tolerance_Jukebox AS MC_39,    
      MC.Machine_Class_Tolerance_Prize AS MC_40,    
      MC.Machine_Class_Float_200 AS MC_41,    
      MC.Machine_Class_Float_100 AS MC_42,    
      MC.Machine_Class_Float_50 AS MC_43,    
      MC.Machine_Class_Float_20 AS MC_44,    
      MC.Machine_Class_Float_10 AS MC_45,    
      MC.Machine_Class_Float_5 AS MC_46,    
      MC.Machine_Class_Float_2 AS MC_47,    
      MC.Machine_Class_Config_Div_Level_1 AS MC_48,    
      MC.Machine_Class_Config_Div_Level_2 AS MC_49,    
      MC.Machine_Class_Config_Div_Level_3 AS MC_50,    
      MC.Machine_Class_Config_Div_Level_4 AS MC_51,    
      MC.Machine_Class_Config_Single_Comm AS MC_52,    
      MC.Machine_Class_Config_Machine_Mode AS MC_53,    
      MC.Machine_Class_Config_Machine_Version AS MC_54,    
      MC.Machine_Class_Config_Attract_Mode_Text AS MC_55,    
      MC.Machine_Class_Config_Bank_Options AS MC_56,    
      MC.Machine_Class_Config_Max_Credit_Limit AS MC_57,    
      MC.Machine_Class_Config_Max_Bank_Value AS MC_58,    
      MC.Machine_Class_Price_Of_Play AS MC_59,    
      MC.Machine_Class_Jackpot AS MC_60,    
      MC.Machine_Class_Percent_Cash_Payout AS MC_61,    
      MC.Machine_Class_Percent_Token_Payout AS MC_62,    
      MC.Machine_Class_Speaker_Volume AS MC_63,    
      MC.Machine_Class_SP_Features AS MC_64,    
      MC.Machine_Class_Release_Date AS MC_65,    
      MC.Machine_Class_Category_ID AS MC_66,    
      MC.Machine_Class_Is_Ticket AS MC_67,    
      MC.Machine_Class_RecreateCancelledCredits AS MC_68,    
      MC.Machine_Class_JackpotAddedToCancelledCredits AS MC_69,    
      MC.Machine_Class_AddTrueCoinInToDrop AS MC_70,    
      MC.Machine_Class_UseCancelledCreditsAsTicketsPrinted AS MC_71,    
      MC.Machine_Class_RecreateTicketsInsertedfromDrop AS MC_72,    
      MC.Meter_Rollover AS MC_73,    
    --  MC.Machine_Class_Occupancy_Games_Per_Hour AS MC_74,    
      MC.Validation_Length AS MC_75,    
      -- END  MACHINE CLASS    
      --START Machine_Type    
      MT.Machine_Type_ID AS MT_1,    
      MT.Depreciation_Policy_ID AS MT_2,    
      MT.Machine_Type_AMEDIS_ID AS MT_3,    
      MT.Machine_Type_Code AS MT_4,    
      MT.Machine_Type_Description AS MT_5,    
      MT.Machine_Type_Prize_Type AS MT_6,    
      MT.Machine_Type_Needs_PPL AS MT_7,    
      MT.Machine_Type_Icon_Ref AS MT_8,    
      MT.Machine_Type_Income_Ledger_Code AS MT_9,    
      MT.Machine_Type_Royalty_Ledger_Code AS MT_10,    
      MT.Machine_Type_Site_Icon AS MT_11,    
      MT.Machine_Type_Category AS MT_12,    
      MT.IsNonGamingAssetType AS MT_13,    
         --END Machine_Type    
      --START Manufacturer    
      MF.Manufacturer_ID AS MF_1,    
      MF.Manufacturer_Name AS MF_2,    
      MF.Manufacturer_Service_Contact AS MF_3,    
      MF.Manufacturer_Service_EMail AS MF_4,    
      MF.Manufacturer_Service_Tel AS MF_5,    
      MF.Manufacturer_Service_Address AS MF_6,    
      MF.Manufacturer_Service_Postcode AS MF_7,    
      MF.Manufacturer_Sales_Contact AS MF_8,    
      MF.Manufacturer_Sales_EMail AS MF_9,    
      MF.Manufacturer_Sales_Tel AS MF_10,    
      MF.Manufacturer_Sales_Address AS MF_11,    
      MF.Manufacturer_Sales_Postcode AS MF_12,    
      MF.Manufacturer_Code AS MF_13,    
      MF.Manufacturer_Coins_In_Meter_Used AS MF_14,    
      MF.Manufacturer_Coins_Out_Meter_Used AS MF_15,    
      MF.Manufacturer_Coin_Drop_Meter_Used AS MF_16,    
      MF.Manufacturer_Handpay_Meter_Used AS MF_17,    
      MF.Manufacturer_External_Credits_Meter_Used AS MF_18,    
      MF.Manufacturer_Games_Bet_Meter_Used AS MF_19,    
      MF.Manufacturer_Games_Won_Meter_Used AS MF_20,    
      MF.Manufacturer_Notes_Meter_Used AS MF_21,    
      MF.Manufacturer_Single_Coin_Build AS MF_22,    
      MF.Manufacturer_Handpay_Added_To_Coin_Out AS MF_23,
      ISNULL(substring(@machineStockNo, 2,Len(@machineStockNo) -2),'') AS M_1 
      FROM dbo.Machine_Class  MC            
     LEFT JOIN dbo.Manufacturer  MF ON MC.Manufacturer_ID = MF.Manufacturer_ID              
     LEFT JOIN dbo.Machine_Type  MT ON MC.Machine_Type_ID = MT.Machine_Type_ID              
      WHERE MC.Machine_Class_ID = CAST(@ID AS INT)              
      FOR XML AUTO, TYPE, ELEMENTS, ROOT('MODEL')              
    END                
END               
ELSE            
BEGIN            
 SELECT  -- SRART MACHINE CLASS    
      MC.Machine_Class_ID AS MC_1,    
      MC.Machine_Type_ID AS MC_2,    
      MC.Manufacturer_ID AS MC_3,    
      MC.Depreciation_Policy_ID AS MC_4,    
      MC.Depreciation_Policy_Use_Default AS MC_5,    
      MC.Machine_Class_Category AS MC_6,    
      MC.Machine_Class_Model_Code AS MC_7,    
      MC.Machine_BACTA_Code AS MC_8,    
      MC.Machine_Name AS MC_9,    
      MC.Machine_Hopper_Type AS MC_10,    
      MC.Machine_Max_No_Of_Discs AS MC_11,    
      MC.Machine_Disc_Type AS MC_12,    
      MC.Label_ID AS MC_13,    
      MC.Machine_Picture_Label AS MC_14,    
      MC.Machine_CD_Numbering AS MC_15,    
      MC.Machine_Track_Numbering AS MC_16,    
      MC.Machine_Class_Counter_Cash_In_Units AS MC_17,    
      MC.Machine_Class_Counter_Cash_Out_Units AS MC_18,    
      MC.Machine_Class_Counter_Tokens_In_Units AS MC_19,    
      MC.Machine_Class_Counter_Tokens_Out_Units AS MC_20,    
      MC.Machine_Class_Counter_Refill_Units AS MC_21,    
      MC.Machine_Class_Counter_Jackpot_Units AS MC_22,    
      MC.Machine_Class_Counter_Prize_Units AS MC_23,    
      MC.Machine_Class_Counter_Tournament_Play_Units AS MC_24,    
      MC.Machine_Class_Counter_JukeBox_Play_Units AS MC_25,    
      MC.Machine_Class_Prize_Machine AS MC_26,    
      MC.Machine_Class_Max_Float_Level AS MC_27,    
      MC.Machine_Class_Default_Coin_Mech AS MC_28,    
      MC.Machine_Class_Default_Note_Acceptor AS MC_29,    
      MC.Machine_Class_Test_Machine AS MC_30,    
      MC.Machine_Class_Delisted AS MC_31,    
      MC.Machine_Class_Features AS MC_32,    
      MC.Machine_Class_Tolerance_Cash_In AS MC_33,    
      MC.Machine_Class_Tolerance_Cash_Out AS MC_34,    
      MC.Machine_Class_Tolerance_Tokens_In AS MC_35,    
      MC.Machine_Class_Tolerance_Tokens_Out AS MC_36,    
      MC.Machine_Class_Tolerance_Refills AS MC_37,    
      MC.Machine_Class_Tolerance_Tournament AS MC_38,    
      MC.Machine_Class_Tolerance_Jukebox AS MC_39,    
      MC.Machine_Class_Tolerance_Prize AS MC_40,    
      MC.Machine_Class_Float_200 AS MC_41,    
      MC.Machine_Class_Float_100 AS MC_42,    
      MC.Machine_Class_Float_50 AS MC_43,    
      MC.Machine_Class_Float_20 AS MC_44,    
      MC.Machine_Class_Float_10 AS MC_45,    
      MC.Machine_Class_Float_5 AS MC_46,    
      MC.Machine_Class_Float_2 AS MC_47,    
      MC.Machine_Class_Config_Div_Level_1 AS MC_48,    
      MC.Machine_Class_Config_Div_Level_2 AS MC_49,    
      MC.Machine_Class_Config_Div_Level_3 AS MC_50,    
      MC.Machine_Class_Config_Div_Level_4 AS MC_51,    
      MC.Machine_Class_Config_Single_Comm AS MC_52,    
      MC.Machine_Class_Config_Machine_Mode AS MC_53,    
      MC.Machine_Class_Config_Machine_Version AS MC_54,    
      MC.Machine_Class_Config_Attract_Mode_Text AS MC_55,    
      MC.Machine_Class_Config_Bank_Options AS MC_56,    
      MC.Machine_Class_Config_Max_Credit_Limit AS MC_57,    
      MC.Machine_Class_Config_Max_Bank_Value AS MC_58,    
      MC.Machine_Class_Price_Of_Play AS MC_59,    
      MC.Machine_Class_Jackpot AS MC_60,    
      MC.Machine_Class_Percent_Cash_Payout AS MC_61,    
      MC.Machine_Class_Percent_Token_Payout AS MC_62,    
      MC.Machine_Class_Speaker_Volume AS MC_63,    
      MC.Machine_Class_SP_Features AS MC_64,    
      MC.Machine_Class_Release_Date AS MC_65,    
      MC.Machine_Class_Category_ID AS MC_66,    
      MC.Machine_Class_Is_Ticket AS MC_67,    
      MC.Machine_Class_RecreateCancelledCredits AS MC_68,    
      MC.Machine_Class_JackpotAddedToCancelledCredits AS MC_69,    
      MC.Machine_Class_AddTrueCoinInToDrop AS MC_70,    
      MC.Machine_Class_UseCancelledCreditsAsTicketsPrinted AS MC_71,    
      MC.Machine_Class_RecreateTicketsInsertedfromDrop AS MC_72,    
      MC.Meter_Rollover AS MC_73,    
     -- MC.Machine_Class_Occupancy_Games_Per_Hour AS MC_74,    
      MC.Validation_Length AS MC_75,    
      -- END  MACHINE CLASS    
      --START Machine_Type    
      MT.Machine_Type_ID AS MT_1,    
      MT.Depreciation_Policy_ID AS MT_2,    
      MT.Machine_Type_AMEDIS_ID AS MT_3,    
      MT.Machine_Type_Code AS MT_4,    
      MT.Machine_Type_Description AS MT_5,    
      MT.Machine_Type_Prize_Type AS MT_6,    
      MT.Machine_Type_Needs_PPL AS MT_7,    
      MT.Machine_Type_Icon_Ref AS MT_8,    
      MT.Machine_Type_Income_Ledger_Code AS MT_9,    
      MT.Machine_Type_Royalty_Ledger_Code AS MT_10,    
      MT.Machine_Type_Site_Icon AS MT_11,    
      MT.Machine_Type_Category AS MT_12,    
      MT.IsNonGamingAssetType AS MT_13,    
         --END Machine_Type    
      --START Manufacturer    
      MF.Manufacturer_ID AS MF_1,    
      MF.Manufacturer_Name AS MF_2,    
      MF.Manufacturer_Service_Contact AS MF_3,    
      MF.Manufacturer_Service_EMail AS MF_4,    
      MF.Manufacturer_Service_Tel AS MF_5,    
      MF.Manufacturer_Service_Address AS MF_6,    
      MF.Manufacturer_Service_Postcode AS MF_7,   
      MF.Manufacturer_Sales_Contact AS MF_8,    
      MF.Manufacturer_Sales_EMail AS MF_9,    
      MF.Manufacturer_Sales_Tel AS MF_10,    
      MF.Manufacturer_Sales_Address AS MF_11,    
      MF.Manufacturer_Sales_Postcode AS MF_12,    
      MF.Manufacturer_Code AS MF_13,    
      MF.Manufacturer_Coins_In_Meter_Used AS MF_14,    
      MF.Manufacturer_Coins_Out_Meter_Used AS MF_15,    
      MF.Manufacturer_Coin_Drop_Meter_Used AS MF_16,    
      MF.Manufacturer_Handpay_Meter_Used AS MF_17,    
      MF.Manufacturer_External_Credits_Meter_Used AS MF_18,    
      MF.Manufacturer_Games_Bet_Meter_Used AS MF_19,    
      MF.Manufacturer_Games_Won_Meter_Used AS MF_20,    
      MF.Manufacturer_Notes_Meter_Used AS MF_21,    
      MF.Manufacturer_Single_Coin_Build AS MF_22,    
      MF.Manufacturer_Handpay_Added_To_Coin_Out AS MF_23        
      --END Manufacturer           
      FROM dbo.Machine tM          
     INNER JOIN dbo.Machine_Class MC  ON tM.Machine_Class_ID = MC.Machine_Class_ID          
     LEFT JOIN dbo.Manufacturer MF ON MC.Manufacturer_ID = MF.Manufacturer_ID            
     LEFT JOIN dbo.Machine_Type MT ON MC.Machine_Type_ID = MT.Machine_Type_ID            
      WHERE tM.Machine_id  in ( Select Max(Machine_id) From Machine WHere Machine_Stock_No =  @AssetNumber )And Machine_Status_Flag = 107          
      FOR XML AUTO, TYPE, ELEMENTS, ROOT('MODEL')           
END            
        
          
END

GO

