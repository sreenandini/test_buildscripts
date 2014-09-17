/************************************************************
 * Code formatted by SoftTree SQL Assistant © v4.8.29
 * Time: 03/05/2014 11:44:59 PM
 ************************************************************/

USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[usp_GetMachineID]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[usp_GetMachineID]
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
  Kalaiyarasan.P              17-Sep-2012         Created               This SP is used to get temporary machine id and delete machine details if exists
  --Exec  usp_GetMachineID 2                                                                    
*/  
  
CREATE PROCEDURE usp_GetMachineID
	@MachineID INT,
	@Machine_Class_ID INT,
	@Machine_New_Install INT,
	@Machine_Stock_No VARCHAR(50) OUTPUT
AS
BEGIN
	DECLARE @IsSuccess BIT
	SET @MachineID = ISNULL(@MachineID, 0)
	IF NOT EXISTS (
	       SELECT 1
	       FROM   MACHINE WITH(NOLOCK)
	       WHERE  Machine_ID = @MachineID
	              AND ISNULL(@Machine_New_Install, 0) = 0
	   )
	BEGIN
	    DECLARE @AutoStock_Code  AS BIT,
	            @StockLen        AS INT,
	            @StockPrefix     AS VARCHAR(10),
	            @Machine_ID      AS INT
	    
	    SET @AutoStock_Code = 0
	    SET @Machine_Stock_No = ''
	    INSERT INTO MACHINE
	      (
	        Machine_Class_ID,
	        Machine_New_Install,
	        Machine_Category_ID,
	        Stacker_ID
	      )
	    VALUES
	      (
	        ISNULL(@Machine_Class_ID, 0),
	        ISNULL(@Machine_New_Install, 0),
	        0,
	        0
	      ) 
	    SELECT @Machine_ID = CAST(SCOPE_IDENTITY() AS INT) 
	    IF (@Machine_ID > 0)
	    BEGIN
	    	SET @IsSuccess = 1
	    	
	        SELECT @AutoStock_Code = System_Parameter_Auto_Generate_Stock_Codes,
	               @StockPrefix = System_Parameter_Stock_Code_Prefix,
	               @StockLen = System_Parameter_Stock_Code_Number_Length
	        FROM   [System_Parameters]
	        
	        IF @AutoStock_Code = 1
	        BEGIN	       
	            SET @Machine_Stock_No = @StockPrefix +
	                RIGHT(
	                    REPLICATE('0', @StockLen- LEN(@StockPrefix)) +
	                    CAST(@Machine_ID AS VARCHAR),
	                    @StockLen- LEN(@StockPrefix)
	                )
	                
	            UPDATE MACHINE
	            SET    Machine_Stock_No = @Machine_Stock_No
	            WHERE  Machine_ID = @Machine_ID
	        END
	    END
	    
	    SELECT @Machine_ID AS Machine_ID
	END
	ELSE
	BEGIN
	    DELETE 
	    FROM   MACHINE
	    WHERE  Machine_ID = @MachineID
	    
	    IF (@@ROWCOUNT > 0)
	        SET @IsSuccess = 1
	END
	
	/*****************************************************************************************************
	* (CR# 191254 : EBS Communication Service) - MODIFIED BY A.VINOD KUMAR (START)
	*****************************************************************************************************/
	
	IF(@IsSuccess = 1)
	BEGIN
		EXEC [dbo].[usp_EBS_UpdateMachineDetails] @MachineID
	END
	/*****************************************************************************************************
	* (CR# 191254 : EBS Communication Service) - MODIFIED BY A.VINOD KUMAR (END)
	*****************************************************************************************************/
END
GO

