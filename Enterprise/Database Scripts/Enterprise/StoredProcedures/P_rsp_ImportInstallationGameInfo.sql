USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_ImportInstallationGameInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_ImportInstallationGameInfo]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =================================================================
-- [rsp_ImportInstallationGameInfo]
-- -----------------------------------------------------------------
--
-- Imports the Installation game info
-- 
-- -----------------------------------------------------------------    
-- Revision History       
--       
-- 03/12/09		Anuradha	Created   
-- 21 Dec 2009  Anuradha    Modified	Changed the node reading hierarchy.   
-- 19 Oct 2010	Yoganandh	Modified	For Italy, Insert 310 AAMS Message only when the Game has changed during installation
-- =================================================================  

CREATE PROCEDURE [dbo].[rsp_ImportInstallationGameInfo]
@doc VARCHAR(MAX),
@IsSuccess INT OUTPUT

AS  
  
DECLARE @iRowCount INT  
DECLARE @idoc INT  
DECLARE @error INT    
DECLARE @InstallationID INT    
DECLARE @sBarCode CHAR(32)  
DECLARE @Game VARCHAR(100)  
DECLARE @Game_ID INT  
DECLARE @Site_ID INT  
DECLARE @Game_Verification INT
DECLARE @Game_Enable_AAMS_Status INT
DECLARE @Game_AAMS_Status INT
  
--variables for error handling   
SET @IsSuccess = -1   
SET @error = 0  
  
--Declare a table variable to hold the data.  
DECLARE @InstallationGameInfo TABLE(  
IGI_ID INT,  
Installation_No int,  
Game_Position int,  
Max_Bet int,  
Prog_Group int,  
Prog_Level int,  
Game_Name varchar(100),  
Paytables varchar(1000),  
IsAvailable bit,  
Game_Verification int,  
Game_Current_Status int,  
Game_AAMS_Status int,  
Game_Floor_Controller_Status bit,  
Game_Entity_Command varchar(12),  
Game_Comments varchar(100),
Installation_cRC	VARCHAR(20),
Game_Part_Number	VARCHAR(20),
Game_Enable_AAMS_Status INT,
IGI_Game_Id INT    
)  
  
CREATE TABLE #TempIG  
( 
 Installation_No int,  
 Game_Position int,  
 Max_Bet int,  
 Prog_Group int,  
 Prog_Level int,  
 Game_Name varchar(100),  
 Paytables varchar(1000),  
 IsAvailable bit,  
 Game_Verification int,  
 Game_Current_Status int,  
 Game_AAMS_Status int,  
 Game_Floor_Controller_Status bit,  
 Game_Entity_Command varchar(12),  
 Game_Comments varchar(100),  
 Site_ID INT,  
 IGI_ID INT,  
 HQ_IGI_ID INT,
 Installation_cRC	VARCHAR(20),
 Game_Part_Number	VARCHAR(20),
 Game_Enable_AAMS_Status INT,
IGI_Game_Id INT       
)  
--add the encoding version as we need to process special characters like pound symbol   
SET @doc='<?xml version="1.0" encoding="ISO-8859-1"?>' + @doc    
  
--Create an internal representation of the XML document.  
EXEC sp_xml_preparedocument @idoc OUTPUT, @doc    
  
--Set row count to 0.  
SET @iRowCount = 0  
  
--Insert the XML data to the table varible.  
INSERT INTO @InstallationGameInfo(Installation_No,
	Game_Position,
	Max_Bet,
	Prog_Group,
	Prog_Level,
	Game_Name,
	Paytables,
	IsAvailable,
	Game_Verification,
	Game_Current_Status,
	Game_AAMS_Status,
	Game_Floor_Controller_Status,
	Game_Entity_Command,
	Game_Comments,
	IGI_ID,
	Installation_cRC,
	Game_Part_Number,
 Game_Enable_AAMS_Status,
 IGI_Game_Id)        
SELECT Installation_No,
	Game_Position,
	Max_Bet,
	Prog_Group,
	Prog_Level,
	Game_Name,
	Paytables,
	IsAvailable,
	Game_Verification,
	Game_Current_Status,
	Game_AAMS_Status,
	Game_Floor_Controller_Status,
	Game_Entity_Command,
	Game_Comments,
	IGI_ID,
	Installation_cRC,
	Game_Part_Number,
	Game_Enable_AAMS_Status
 ,MG_HQ_Game_ID    
  FROM OPENXML (@idoc, './InstallationGameInfo/Installation/InstallationGameInfo',2)             
WITH (  
  IGI_ID INT './IGI_ID',  
  Installation_No int './Installation_No',  
  Game_Position int './Game_Position',  
  Max_Bet int './Max_Bet',  
  Prog_Group int './Prog_Group',  
  Prog_Level int './Prog_Level',  
  Game_Name varchar(100) './Game_Name',  
  Paytables varchar(1000) './Paytables',  
  IsAvailable bit './IsAvailable',  
  Game_Verification int './Game_Verification',  
  Game_Current_Status int './Game_Current_Status',  
  Game_AAMS_Status int './Game_AAMS_Status',  
  Game_Floor_Controller_Status bit './Game_Floor_Controller_Status',  
  Game_Entity_Command varchar(12) './Game_Entity_Command',  
  Game_Comments varchar(100) './Game_Comments',
  Installation_cRC	varchar(20)	'./Installation_cRC',
  Game_Part_Number varchar(20)	'./Game_Part_Number',
  Game_Enable_AAMS_Status int './Game_Enable_AAMS_Status',
  MG_HQ_Game_ID INT './Machine/GL/MG_HQ_Game_ID'   
)   
  
--Get the row count value.  
SELECT @iRowCount = COUNT(Installation_No) FROM @InstallationGameInfo  
  
--Assign the Voucher ID, Site ID & Bar Code to the respective variables.    
SELECT  @InstallationID = Installation_No  
FROM OPENXML (@idoc, './InstallationGameInfo/Installation',2)      
WITH (Installation_No int './HQ_Installation_No')  
  
SELECT @Site_ID = S.Site_ID FROM dbo.Site S INNER JOIN dbo.Bar_Position B ON S.Site_ID = B.Site_ID  
 INNER JOIN dbo.Installation I ON B.Bar_Position_ID = I.Bar_Position_ID WHERE Installation_ID = @InstallationID  
  
UPDATE @InstallationGameInfo  
SET Installation_No = @InstallationID  

IF EXISTS (SELECT 1 FROM ExchangeVersionHistory WHERE Site_Id = @Site_ID AND PendingUpdate = 1)
BEGIN
	DECLARE @GameName VARCHAR(100)
	DECLARE @Manufacturer INT

	SELECT @Manufacturer = Mn.Manufacturer_id,
		   @GameName = IGI.Game_Name
	FROM   @InstallationGameInfo IGI
	       INNER JOIN installation I
	            ON  I.Installation_ID = IGI.Installation_No
	       INNER JOIN MACHINE M
	            ON  I.Machine_Id = M.Machine_Id
	       INNER JOIN Machine_Class MC
	            ON  M.Machine_Class_Id = MC.Machine_Class_Id
	       INNER JOIN Manufacturer Mn
	            ON  MC.Manufacturer_ID = Mn.Manufacturer_ID
	WHERE  I.Installation_ID = @InstallationID
	
	SET @Game_ID = 0
	
	SELECT @Game_ID = MG_Game_ID FROM Game_Library WHERE MG_Game_Name = @GameName AND MG_Game_Manufacturer_ID = @Manufacturer
	
	UPDATE @InstallationGameInfo SET IGI_Game_Id = @Game_ID
END

--Check for row count value.  
IF @iRowCount > 0  
BEGIN  

 DECLARE @RowCount INT
 DECLARE @PreviousInstallationGameName Varchar(100)
 DECLARE @PreviousInstallationID INT
 DECLARE @CurrentInstallationGameName Varchar(100)
 DECLARE @IsGameInfoMatches BIT
 
 SELECT @IsGameInfoMatches = 0
 SELECT @RowCount = Count(1) FROM @InstallationGameInfo    
 
 -- Check condition only for Single Game Asset
 IF @RowCount = 1
 BEGIN
	EXEC rsp_GetGameName @InstallationID, @PreviousInstallationGameName OUTPUT,@PreviousInstallationID OUTPUT

    IF ISNULL(@PreviousInstallationGameName,'') <> ''
	BEGIN
		SELECT @CurrentInstallationGameName = UPPER(Game_Name) FROM @InstallationGameInfo
		IF ISNULL(@PreviousInstallationGameName,'') = ISNULL(@CurrentInstallationGameName,'')			
			SET @IsGameInfoMatches = 1
	END
 END
 
 --Update Code Start.  
 UPDATE TE  
 SET   
 Installation_No = tmpTE.Installation_No,  
 Game_Position = tmpTE.Game_Position,  
 Max_Bet = tmpTE.Max_Bet,     
 Prog_Group = tmpTE.Prog_Group,  
 Prog_Level = tmpTE.Prog_Level,  
 Game_Name = tmpTE.Game_Name,  
 Paytables = tmpTE.Paytables,  
 IsAvailable = tmpTE.IsAvailable,  
 Game_Current_Status = tmpTE.Game_Current_Status,  
 Game_Floor_Controller_Status = tmpTE.Game_Floor_Controller_Status,  
 Game_Entity_Command = tmpTE.Game_Entity_Command,  
 Game_Comments = tmpTE.Game_Comments,
 Installation_cRC = tmpTE.Installation_cRC,
 Game_AAMS_Status = tmpTE.Game_AAMS_Status,
 Game_Part_Number = tmpTE.Game_Part_Number,
    Game_Enable_AAMS_Status = tmpTE.Game_Enable_AAMS_Status,    
    IGI_Game_ID = tmpTE.IGI_Game_ID
 FROM @InstallationGameInfo tmpTE  
 INNER JOIN Installation_Game_Info TE   
 ON TE.IGI_ID = tmpTE.IGI_ID  
 WHERE TE.Site_ID = @Site_ID  
 --Update Code End.  
  
 --Insert Code Start.  
 INSERT INTO Installation_Game_Info(Installation_No,
	Game_Position,
	Max_Bet,
	Prog_Group,
	Prog_Level,
	Game_Name,
	Paytables,
	IsAvailable,
	Game_Verification,
	Game_Current_Status,
	Game_AAMS_Status,
	Game_Floor_Controller_Status,
	Game_Entity_Command,
	Game_Comments,
	Site_ID,
	IGI_ID,
	Installation_cRC,
	Game_Part_Number,
    Game_Enable_AAMS_Status,
    IGI_Game_ID)      
 OUTPUT Inserted.Installation_No,
	Inserted.Game_Position,
	Inserted.Max_Bet,
	Inserted.Prog_Group,
	Inserted.Prog_Level,
	Inserted.Game_Name,
	Inserted.Paytables,
	Inserted.IsAvailable,
	Inserted.Game_Verification,
	Inserted.Game_Current_Status,
	Inserted.Game_AAMS_Status,
	Inserted.Game_Floor_Controller_Status,
	Inserted.Game_Entity_Command,
	Inserted.Game_Comments,
	Inserted.Site_ID,
	Inserted.IGI_ID,
	Inserted.HQ_IGI_ID,
	Inserted.Installation_cRC,
	Inserted.Game_Part_Number,
    Inserted.Game_Enable_AAMS_Status, 
    Inserted.IGI_Game_ID INTO #TempIG      
 SELECT T.Installation_No,
	T.Game_Position,
	T.Max_Bet,
	T.Prog_Group,
	T.Prog_Level,
	T.Game_Name,
	T.Paytables,
	T.IsAvailable,
	T.Game_Verification,
	T.Game_Current_Status,
	T.Game_AAMS_Status,
	T.Game_Floor_Controller_Status,
	T.Game_Entity_Command,
	T.Game_Comments,
	@Site_ID,
	T.IGI_ID,
	T.Installation_cRC,
	T.Game_Part_Number,
    T.Game_Enable_AAMS_Status,
    T.IGI_Game_ID    
  FROM @InstallationGameInfo T LEFT JOIN dbo.Installation_Game_Info IG   
  ON T.IGI_ID = IG.IGI_ID AND IG.Site_ID = @Site_ID WHERE IG.IGI_ID IS NULL
 --Insert Code End.   
 
 INSERT INTO dbo.BMC_AAMS_Details([BAD_Reference_ID], [BAD_AAMS_Entity_Type], [BAD_AAMS_Status],   
 [BAD_Verification_Status], [BAD_Entity_Floor_Controller_Status], [BAD_Game_Name], [BAD_Updated_Date], [BAD_Game_Part_Number])  
 SELECT GL.MG_Game_ID, 4, 0, 0, 0, GL.MG_Game_Name, getdate(), GL.Game_Part_Number FROM dbo.Game_Library GL  
 LEFT JOIN dbo.BMC_AAMS_Details B ON GL.MG_Game_Name = B.BAD_Game_Name
 WHERE B.BAD_Game_Name IS NULL AND B.BAD_AAMS_Entity_Type = 4

END  
  
--Removes the internal representation of the XML document.  
EXEC sp_xml_removedocument @idoc  
  
--Check for any errors during the insert process.  
SET @error = @@ERROR  
IF @error <> 0   
GOTO Err_Handler   
   
--Return success/failure    
Err_Handler:    
IF @error = 0    
SET @IsSuccess = 0   
--Success   
ELSE  
SET @IsSuccess = @error   
--Error    
RETURN @error   
  

GO