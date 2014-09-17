USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[USP_UpdatePlannedConversionByAsset]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[USP_UpdatePlannedConversionByAsset]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

      
--------------------------------------------------------------------------                                 
--                                
-- Description: Create an Installation                              
--                                    
--    Steps                                
--    1. Create entry in installation table                          
--    2. Update the Machine ID for the Asset    
--                                
-- Inputs:      XML File Containing the following                              
--    INSTALLATION  - Root Element                              
--    HQInstallation_No - HQ Installation ID                               
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
---------------------------------------------------------------------------                                 
                              
CREATE PROCEDURE [dbo].[USP_UpdatePlannedConversionByAsset](@XML nVARCHAR(4000))                                    
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
 ALTERNATESERIALNO VARCHAR(50),        
 MACHINE_JACKPOT INT,        
 FLOAT_ISSUED REAL,        
 INSTALLATION_TOKEN_VALUE INT,      
 PERCENTAGEPAYOUT INT )                  
                              
Select * INto NEWXMLINSTALL From #temp          
                                    
DECLARE @Zone_Name VARCHAR(30)                                    
DECLARE @BAR_ID INT                                    
DECLARE @MACHINE_ID INT                                    
DECLARE @Site_Id VARCHAR(30)                                    
DECLARE @Installation_Date VARCHAR(30)                                    
DECLARE @Installation_Time VARCHAR(50)                                    
DECLARE @Bar_Position_Name VARCHAR(50)                            
DECLARE @DATAPAK INT                                    
DECLARE @AssetNo VARCHAR(10)                                    
DECLARE @Price_of_play INT                                    
DECLARE @Machine_Class_Name VARCHAR(50)                                    
DECLARE @Machine_Type_Code VARCHAR(20)                                    
DECLARE @Machine_Stock_No VARCHAR(50)                                    
DECLARE @ZoneId INT                  
DECLARE @AlternateSerialNo VARCHAR(50)                  
DECLARE @SerialNo VARCHAR(50)         
DECLARE @MACHINE_JACKPOT INT        
DECLARE @FLOAT_ISSUED REAL        
DECLARE @INSTALLATION_TOKEN_VALUE INT      
DECLARE @PERCENTAGEPAYOUT INT      
                                    
SET @ZoneId=0                                    
                                 
SELECT @Site_Id = Site_ID FROM Site                                     
INNER JOIN #temp ON #temp.Site_Code = Site.Site_Code                                    
           
                                    
SELECT                                     
 @Zone_Name = Zone_Name,                                     
 @Installation_Date = Installation_Date ,                                     
 @Installation_Time = Installation_Time ,                                    
 @DATAPAK = DATAPAK,                            
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
 @PERCENTAGEPAYOUT = PERCENTAGEPAYOUT      
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
		EXEC [dbo].[usp_EBS_UpdateZoneDetails] @ZoneId = @ZoneID
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
                                    
                                    
----Select distinct Machine_Name  from Machine_Class Where  Machine_Name= 'All That Glitters'                                    
----Check for Machine Class                                    
--IF NOT EXISTS(SELECT Machine_Name FROM Machine_Class WHERE Machine_Name=@Machine_Class_Name)                                    
-- BEGIN                                    
--  --return -1                                    
--  SELECT '-1' As Result                                    
--  Return                                    
-- END                                    
--                                    
----Check for Machine Type                                    
--IF NOT EXISTS(SELECT Machine_Type_Code FROM Machine_Type WHERE Machine_Type_Code=@Machine_Type_Code)                                    
-- BEGIN                                    
--  --return -2                                    
--  SELECT '-2' As Result                                    
--  Return                                    
-- END      
                                    
--Check for Asset No                                    
IF NOT EXISTS (SELECT 1 FROM Machine WHERE Machine_Stock_No=@Machine_Stock_No)                                    
 BEGIN                                    
  --return -3                                    
  SELECT '-1' As Result                                    
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
  SELECT '-1' As Result                                    
  Return                                    
 END                              
                                    
--Check asset already allocated                                    
IF EXISTS(SELECT 1 FROM Installation JOIN Machine ON Installation.Machine_Id = Machine.Machine_Id                                    
WHERE Installation_END_date IS  NULL AND Machine.Machine_Stock_No=@AssetNo)                                    
--@Machine_Stock_No)                                    
 BEGIN                                    
  --return -4                                    
  SELECT '-2' As Result                                    
  Return                                    
 END                                    
                                    
----Check the Gametitle AND Asset matches                                    
--IF NOT EXISTS(SELECT * FROM Machine JOIN Machine_Class ON Machine.Machine_Class_Id = Machine_Class.Machine_Class_Id                                     
--WHERE Machine.Machine_Stock_No=@Machine_Stock_No AND Machine_Name=@Machine_Class_Name)                                    
-- BEGIN                                    
--  SELECT '-5' As Result                                    
--  Return                                    
-- END                     
                  
                
IF LTRIM(RTRIM(@SerialNo)) <> ''                
BEGIN                
--Check Serial No                   
--IF NOT EXISTS(SELECT * FROM Machine JOIN Machine_Class ON Machine.Machine_Class_Id = Machine_Class.Machine_Class_Id                                     
--WHERE Machine.Machine_Stock_No=@Machine_Stock_No AND Machine_Name=@Machine_Class_Name AND Machine.Machine_Manufacturers_Serial_No = @SerialNo)                                    
IF NOT EXISTS(SELECT 1 FROM Machine WHERE  Machine.Machine_Stock_No=@Machine_Stock_No and Machine.Machine_Manufacturers_Serial_No = @SerialNo)                                    
--@Machine_Stock_No)                                    
 BEGIN                                    
  --return -6                                    
  SELECT '-3' As Result                                    
  Return                                    
 END                         
END                
                  
IF LTRIM(RTRIM(@AlternateSerialNo)) <> ''                
BEGIN                
    --Check Alternate Serial No                  
--    IF NOT EXISTS(SELECT * FROM Machine JOIN Machine_Class ON Machine.Machine_Class_Id = Machine_Class.Machine_Class_Id                                     
--    WHERE Machine.Machine_Stock_No=@Machine_Stock_No AND Machine_Name=@Machine_Class_Name AND Machine_Alternative_Serial_Numbers = @AlternateSerialNo)                  
    IF NOT EXISTS(SELECT 1 FROM Machine WHERE  Machine.Machine_Stock_No=@Machine_Stock_No and Machine.Machine_Alternative_Serial_Numbers = @AlternateSerialNo)                                    
    --@Machine_Stock_No)                                    
     BEGIN                                    
      --return -7                                    
      SELECT '-4' As Result                                    
      Return                                    
     END                         
END                
                  
                                 
Select @MACHINE_ID = Max(Machine_ID) From Machine WHERE Machine.Machine_Stock_No=@Machine_Stock_No    
                                    
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
Installation_Percentage_Payout      
)                                    
VALUES (                                    
@BAR_ID,                                    
@MACHINE_ID,                                    
@Installation_Date,                                    
@Installation_Time,                            
@DATAPAK,                                    
@Price_of_play,             
0, 0,         
@INSTALLATION_TOKEN_VALUE,         
0, 0 , @MACHINE_JACKPOT, @PERCENTAGEPAYOUT      
)                                    
DECLARE @Identity INT
SET @Identity =   SCOPE_IDENTITY() 
                        
UPDATE         
    Machine         
SET         
    Machine_status_Flag = 1,         
    Machine_Counter_Jackpot_Units = @MACHINE_JACKPOT,        
    Machine_Float_Maximum_Capacity = @FLOAT_ISSUED        
WHERE         
    Machine_ID = @Machine_Id                        
      
-- Update Bar_Postion SET  Bar_Position_Jackpot = @MACHINE_JACKPOT WHERE Bar_Position_ID = @BAR_ID      
                                  
UPDATE Installation SET HQInstallationID = @Identity WHERE Installation_ID = @IDENTITY                                 
SELECT HQInstallationID AS Result FROM Installation WHERE Installation_ID = @IDENTITY                                 
                                     
End 


GO

