USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_CancelPlannedMachineConvertion]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_CancelPlannedMachineConvertion]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--------------------------------------------------------------------------             
--            
-- Description: Reverting Planned convertion.            
--                
--    Steps            
--    1. Expire the current machine           
--    3. Revert back the status the old machine            
--            
-- Inputs:      See inputs             
--            
-- Outputs:     NONE            
--            
-- Return:          0 - All Ok          
--              OTHER - SQL Error            
--            
-- =======================================================================            
--             
-- Revision History            
--             
-- Renjish     25/06/2009     Created   
-- Renjish     10/07/2009     Changed the logic to expire the machine to converted state.            
---------------------------------------------------------------------------             
CREATE PROCEDURE [dbo].[usp_CancelPlannedMachineConvertion]         
            
  @Machine_ID         INT,            
  @User_ID            INT  
            
AS            
            
  SET DATEFORMAT DMY            
            
  DECLARE @ddmmmyyyy         varchar(12)          
              
  SET @ddmmmyyyy = convert ( varchar(12), Getdate(), 106 )  -- dd mmm yyyy        
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

  -- Expire the current machine       
  UPDATE Machine            
     SET Machine_End_Date = @ddmmmyyyy,            
         Machine_Status_Flag = 7,            
         Staff_ID_Deleted = @User_ID,            
         Machine_Date_Deleted = getdate(),            
         Machine_Type_Of_Sale = 'Cancelling a previous planned conversion.'   
         OUTPUT INSERTED.Machine_ID,
                DELETED.Machine_Status_Flag, INSERTED.Machine_Status_Flag,
                DELETED.Machine_Class_ID, INSERTED.Machine_Class_ID,
                DELETED.CMPGameType, INSERTED.CMPGameType, 
                DELETED.Machine_Stock_No, INSERTED.Machine_Stock_No
         INTO @_Modified
   WHERE Machine_ID = @Machine_ID  
   
   IF EXISTS(
                SELECT 1
                FROM   @_Modified m
                WHERE  m.FlagChanged = 1 OR
					   m.GameIDChanged = 1 OR
                       m.CMPGameTypeChanged = 1 OR
                       m.StockNoChanged = 1
                )
                BEGIN
                       EXEC [dbo].[usp_EBS_UpdateMachineDetails] @Machine_ID 
                END
                
  DELETE FROM @_Modified
  -- Revert the old machine 
  UPDATE Machine            
     SET Machine_End_Date = NULL,            
         Machine_Status_Flag = 1,            
         Staff_ID_Deleted = NULL,            
         Machine_Date_Deleted = NULL,            
         Machine_Type_Of_Sale = NULL   
         OUTPUT INSERTED.Machine_ID,
                DELETED.Machine_Status_Flag, INSERTED.Machine_Status_Flag,
                DELETED.Machine_Class_ID, INSERTED.Machine_Class_ID,
                DELETED.CMPGameType, INSERTED.CMPGameType, 
                DELETED.Machine_Stock_No, INSERTED.Machine_Stock_No
         INTO @_Modified         
   WHERE Machine_ID = (SELECT Machine_Previous_Machine_ID FROM Machine WHERE Machine_ID = @Machine_ID)
   
   IF EXISTS(
             SELECT 1
             FROM   @_Modified m
             WHERE  m.FlagChanged = 1 OR
					   m.GameIDChanged = 1 OR
                       m.CMPGameTypeChanged = 1 OR
                       m.StockNoChanged = 1
             )
             BEGIN
                       EXEC [dbo].[usp_EBS_UpdateMachineDetails] @Machine_ID 
             END
-- return error            
RETURN @@ERROR   
  

GO

