USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[usp_UpdateTerminationMCDetails]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[usp_UpdateTerminationMCDetails]
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
  Kalaiyarasan.P              21-NOV-2012         Created               This SP is used to update Termination Machine details   
  Aishwarrya V S			  25-Oct-2013								Updated for Audit.	      
                                                              
/*
DECLARE @p10 INT
SET @p10 = NULL
EXEC sp_executesql 
     N'EXEC @RETURN_VALUE = [dbo].[usp_UpdateTerminationMCDetails] @Machine_Stock_No = @p0, 
@Machine_Termination_Comments = @p1, @Machine_Termination_Username = @p2, @Machine_Termination_Reason = @p3, @Machine_Status_Flag = @p4, @Machine_End_Date = @p5, @isNGA = @p6',
     N'@p0 varchar(8000),@p1 varchar(8000),@p2 varchar(8000),@p3 int,@p4 int,@p5 varchar(8000),@p6 bit,@RETURN_VALUE int output',
     @p0 = 'LC0066',
     @p1 = '',
     @p2 = 'admin',
     @p3 = 5,
     @p4 = 13,
     @p5 = '18/12/13',
     @p6 = 1,
     @RETURN_VALUE = @p10 OUTPUT

SELECT @p10
*/
*/
CREATE PROCEDURE usp_UpdateTerminationMCDetails
	@Machine_Stock_No VARCHAR(50),
	@Machine_Termination_Comments VARCHAR(250),
	@Machine_Termination_Username VARCHAR(100),
	@Machine_Termination_Reason INT,
	@Machine_Status_Flag INT,
	@Machine_End_Date VARCHAR(30),
	@isNGA BIT

AS
BEGIN
	DECLARE @Audit_DESC VARCHAR(MAX)
	DECLARE @Machine_Terminate VARCHAR(20)	
	DECLARE @Staff_ID INT
	DECLARE @NGA VARCHAR(10)
	PRINT @isNGA
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

	UPDATE MACHINE
	SET    Machine_Termination_Comments = @Machine_Termination_Comments,
	       Machine_Termination_Username = @Machine_Termination_Username,
	       Machine_Termination_Reason = @Machine_Termination_Reason,
	       Machine_End_Date = @Machine_End_Date,
	       Machine_Status_Flag = @Machine_Status_Flag
	OUTPUT INSERTED.Machine_ID,
                                DELETED.Machine_Status_Flag, INSERTED.Machine_Status_Flag,
                                DELETED.Machine_Class_ID, INSERTED.Machine_Class_ID,
                                DELETED.CMPGameType, INSERTED.CMPGameType, 
                                DELETED.Machine_Stock_No, INSERTED.Machine_Stock_No
	INTO @_Modified

	WHERE  Machine_Stock_No = LTRIM(RTRIM(@Machine_Stock_No))
	
	IF EXISTS(
		SELECT 1
        FROM   @_Modified m
        WHERE  m.FlagChanged = 1 OR
			   m.GameIDChanged = 1 OR
               m.CMPGameTypeChanged = 1 OR
               m.StockNoChanged = 1
              ) AND @isNGA = 0
        BEGIN
			  DECLARE @MachineId INT
			  SELECT @MachineId = MachineId FROM @_Modified
			  EXEC [dbo].[usp_EBS_UpdateMachineDetails] @MachineId 
		END

/*****************************************************************************************************
* (CR# 191254 : EBS Communication Service) - MODIFIED BY Venkatesh.H  (END)
*****************************************************************************************************/

	SELECT @Machine_Terminate = MTRT_Description
	FROM   Machine_Termination_Reason_Types
	WHERE  MTRT_ID = @Machine_Termination_Reason
	
	SET @NGA= case WHEN @isNGA = 1 THEN '[NGA]'
	ELSE '' END;
		
	
	SELECT @Staff_ID=SecurityUserID FROM [USER] WHERE UserName=@Machine_Termination_Username
	SET @Audit_DESC = ' Machine_Stock_No =' + @Machine_Stock_No +@NGA+
	    + ' Terminated. '
	    + 'Terminate Reason = ' + @Machine_Terminate
	    + ' ,Terminate Date = ' + @Machine_End_Date
	    + ' ,User Name =' + @Machine_Termination_Username + ' ,Comments =' + 
	    case when len(@Machine_Termination_Comments) > 0 then @Machine_Termination_Comments else  'No Comments' End  
	
	
	EXEC [usp_InsertAuditData] 
	     @Staff_ID,
	     @Machine_Termination_Username,
	     562,
	     'AUDIT_TERMINATEMACHINE',
	     'Terminate Machine',
	     '',
	     '',
	     '',
	     'TRUE',
	     @Audit_DESC,
	     'TERMINATE'
END
GO


