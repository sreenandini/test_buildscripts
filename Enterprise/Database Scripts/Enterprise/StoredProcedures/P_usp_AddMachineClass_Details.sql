/************************************************************
 * Created by: Kalaiyarasan.P  Version:12.4
 * Time: 14/05/13 4:41:37 PM
 ************************************************************/

USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[usp_AddMachineClass_Details]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[usp_AddMachineClass_Details]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*  
* Revision History  
*   
* <Name of the owner>         DateCreated       Type(Created/Modified)  Description  
  Kalaiyarasan.P              27-SEP-2012         Created               This SP is used to Add/Modify Machine_Class details   
			                                                             based on Machine_ID. 
--Exec  usp_AddMachineClass_Details  3,'Non Gaming Asset',1,1,
*/  
CREATE PROCEDURE usp_AddMachineClass_Details
	@Machine_ID INT,
	@Machine_Name VARCHAR(50),
	@EDIT BIT,
	@IsNGA BIT,
	@Manufacturer_ID INT,
	@Machine_Type_ID INT OUTPUT,
	@Machine_Class_Category_ID INT,
	@Machine_Class_SP_Features INT,
	@Machine_Class_Model_Code VARCHAR(50),
	@Depreciation_Policy_ID INT,
	@Depreciation_Policy_Use_Default BIT,
	--@Machine_Class_Occupancy_Games_Per_Hour INT,
	@Machine_Class_Counter_Cash_In_Units INT,
	@Machine_Class_Counter_Cash_Out_Units INT,
	@Machine_Class_Counter_Tokens_In_Units INT,
	@Machine_Class_Counter_Tokens_Out_Units INT,
	@Machine_Class_Config_Machine_Version VARCHAR(50),
	@Machine_Class_Config_Attract_Mode_Text VARCHAR(50),
	@Machine_Class_UseCancelledCreditsAsTicketsPrinted BIT,
	@Machine_Class_RecreateTicketsInsertedfromDrop BIT,
	@Meter_Rollover INT,
	@Machine_Class_Test_Machine BIT,
	@Validation_Length INT,
	@MachineClassID INT OUTPUT
AS
BEGIN
	DECLARE	@Machine_Class_ID INT
	,@Currentdt DATETIME   
	
	SELECT @Machine_Class_ID = Machine_Class_ID
	FROM   Machine_Class WITH(NOLOCK)
	WHERE  (
	           (@IsNGA = 1 OR Machine_Name = @Machine_Name)
	           AND Machine_Type_ID = @Machine_Type_ID
	           AND Manufacturer_ID = @Manufacturer_ID
	       )  
	
	
	
	IF @IsNGA = 1
	   AND ISNULL(@MachineClassID, 0) <> 0
	BEGIN
	    UPDATE MC
	    SET    Manufacturer_ID = @Manufacturer_ID,
	           Validation_Length = @Validation_Length
	    FROM   Machine_Class MC
	           INNER JOIN MACHINE M
	                ON  MC.Machine_Class_ID = M.Machine_Class_ID
	    WHERE  M.Machine_ID = @Machine_ID
	END
	ELSE
	BEGIN
	    SET @MachineClassID = @Machine_Class_ID  
	    IF (ISNULL(@Machine_Class_ID, 0) = 0)
	    BEGIN
	        INSERT INTO Machine_Class
	          (
	            Machine_Name,
	            Machine_Type_ID,
	            Machine_Class_Category_ID,
	            Machine_Class_SP_Features,
	            Machine_Class_Model_Code,
	            Depreciation_Policy_ID,
	            Depreciation_Policy_Use_Default,
	          --  Machine_Class_Occupancy_Games_Per_Hour,
	            Manufacturer_ID,
	            Machine_Class_Counter_Cash_In_Units,
	            Machine_Class_Counter_Cash_Out_Units,
	            Machine_Class_Counter_Tokens_In_Units,
	            Machine_Class_Counter_Tokens_Out_Units,
	            Machine_Class_Config_Machine_Version,
	            Machine_Class_Config_Attract_Mode_Text,
	            Machine_Class_UseCancelledCreditsAsTicketsPrinted,
	            Machine_Class_RecreateTicketsInsertedfromDrop,
	            Meter_Rollover,
	            Machine_Class_Test_Machine,
	            Validation_Length
	          )
	        VALUES
	          (
	            @Machine_Name,
	            @Machine_Type_ID,
	            @Machine_Class_Category_ID,
	            @Machine_Class_SP_Features,
	            @Machine_Class_Model_Code,
	            @Depreciation_Policy_ID,
	            @Depreciation_Policy_Use_Default,
	           -- @Machine_Class_Occupancy_Games_Per_Hour,
	            @Manufacturer_ID,
	            @Machine_Class_Counter_Cash_In_Units,
	            @Machine_Class_Counter_Cash_Out_Units,
	            @Machine_Class_Counter_Tokens_In_Units,
	            @Machine_Class_Counter_Tokens_Out_Units,
	            @Machine_Class_Config_Machine_Version,
	            @Machine_Class_Config_Attract_Mode_Text,
	            @Machine_Class_UseCancelledCreditsAsTicketsPrinted,
	            @Machine_Class_RecreateTicketsInsertedfromDrop,
	            @Meter_Rollover,
	            @Machine_Class_Test_Machine,
	            @Validation_Length
	          )   
	        SELECT @Machine_Class_ID = SCOPE_IDENTITY()      
       	    END
	    ELSE
	    BEGIN
	        UPDATE Machine_Class
	        SET    Manufacturer_ID = @Manufacturer_ID,
	               --Machine_Class_Occupancy_Games_Per_Hour = @Machine_Class_Occupancy_Games_Per_Hour,
	               Validation_Length = @Validation_Length
	        WHERE  Machine_Class_ID = @Machine_Class_ID  
	    END      
	    SET @MachineClassID = @Machine_Class_ID  
	    IF (@@ROWCOUNT > 0)
	    BEGIN
	        --Export to Exchange  
	        INSERT INTO dbo.Export_History
	          (
	            EH_Date,
	            EH_Reference1,
	            EH_Type,
	            EH_Site_Code
	          )
	        SELECT GETDATE(),
	               @Machine_Class_ID,
	               'MODEL',
	               Site_Code
	        FROM   SITE WITH(NOLOCK)
	        WHERE  Site_Enabled = 1
	               AND Sitestatus = 'FULLYCONFIGURED'
	    END
	END
END
GO

