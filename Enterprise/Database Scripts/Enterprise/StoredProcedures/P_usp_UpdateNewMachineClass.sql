USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_UpdateNewMachineClass]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_UpdateNewMachineClass]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--------------------------------------------------------------------------- 
--
-- Description: Inserts a new machine class record
--
-- Inputs:      
-- Outputs:     
--
-- =======================================================================
-- 
-- Revision History
-- 
-- Sudarsan S	08-12-2009   Created   
--------------------------------------------------------------------------- 
CREATE PROCEDURE [dbo].[usp_UpdateNewMachineClass]
	@doc	XML,
	@IsSuccess	INT OUTPUT
AS

BEGIN

DECLARE @idoc	INT
DECLARE @Type_ID	INT
DECLARE @Man_ID	INT
	
CREATE TABLE #Temp
	(
		[Name]	VARCHAR(100),
		Model_Code	VARCHAR(100),
		Machine_Type_Code	VARCHAR(100),
		Manufacturer_Name	VARCHAR(100),
		Machine_Class_Special_Features	INT,
		Meter_RollOver	INT,
		Validation_Length	INT
	)

	EXEC SP_XML_PREPAREDOCUMENT @idoc OUTPUT, @doc

	INSERT INTO #Temp
	SELECT * FROM OPENXML(@idoc, './MACHINECLASS/Machine', 2) WITH #Temp

	EXEC SP_XML_REMOVEDOCUMENT @idoc

	SELECT @Type_ID = Machine_Type_ID FROM dbo.Machine_Type MT
				INNER JOIN #Temp T ON MT.Machine_Type_Code = T.Machine_Type_Code

	SELECT @Man_ID = M.Manufacturer_ID FROM dbo.Manufacturer M
				INNER JOIN #Temp T ON M.Manufacturer_Name = T.Manufacturer_Name

	IF NOT EXISTS(SELECT MC.* FROM dbo.Machine_Class MC INNER JOIN #Temp T ON MC.Machine_Name = T.[Name] AND MC.Machine_Type_ID = @Type_ID AND MC.Manufacturer_ID = @Man_ID)
	BEGIN
		INSERT INTO dbo.Machine_Class(Machine_Name, Machine_Type_ID, Manufacturer_ID, Machine_Class_Model_Code, 
									Machine_Class_SP_Features, Meter_Rollover, Validation_Length)		
		SELECT [Name], @Type_ID, @Man_ID, Model_Code, Machine_Class_Special_Features, Meter_RollOver, Validation_Length FROM #Temp

		INSERT INTO MeterAnalysis.dbo.Machine_Class(Machine_Name, Machine_Type_ID, Manufacturer_ID, Machine_Class_Model_Code, 
									Machine_Class_SP_Features, Meter_Rollover, Validation_Length)		
		SELECT [Name], @Type_ID, @Man_ID, Model_Code, Machine_Class_Special_Features, Meter_RollOver, Validation_Length FROM #Temp
		
		SET @IsSuccess = SCOPE_IDENTITY()
		
		INSERT INTO dbo.Export_History  
           (  
             EH_Date,  
             EH_Reference1,  
             EH_Type,  
             EH_Site_Code  
           )  
         SELECT GETDATE(),  
                @IsSuccess,  
                'MODEL',  
                Site_Code  
         FROM   SITE WITH(NOLOCK)  
         WHERE  Site_Enabled = 1  
                AND Sitestatus = 'FULLYCONFIGURED'

	END
	ELSE
	BEGIN
		SELECT @IsSuccess = MC.Machine_Class_ID FROM dbo.Machine_Class MC INNER JOIN #Temp T ON MC.Machine_Name = T.[Name] AND MC.Machine_Type_ID = @Type_ID AND MC.Manufacturer_ID = @Man_ID
	END

END


GO

