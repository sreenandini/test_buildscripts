USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_UpdateGameForInstallationXML]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_UpdateGameForInstallationXML]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

Create proc usp_UpdateGameForInstallationXML	
(
@doc xml	
)
as
DECLARE @docHandle int  
EXEC sp_xml_preparedocument @docHandle OUTPUT, @doc  
declare @MachineClassID int
declare @MachineName varchar(200)
declare @InstallationID int

 SELECT @InstallationID = x.Installation_ID,
		@MachineName = x.Machine_Name
	 FROM OPENXML (@docHandle,  '/Game/InstallationGame',2)  
  with  
  (  
      Installation_ID int  'Installation_ID'  ,
	 Machine_Name varchar(200) 'Machine_Name'
  ) x  

EXEC sp_xml_removedocument @dochandle  

select @MachineClassID = machine_class_ID from machine_class where machine_name=@MachineName

/*****************************************************************************************************
* (CR# 191254 : EBS Communication Service) - MODIFIED BY Venkatesh.H (START)
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
                
update m set machine_class_ID = @MachineClassID
OUTPUT INSERTED.Machine_ID,
                                DELETED.Machine_Status_Flag, INSERTED.Machine_Status_Flag,
                                DELETED.Machine_Class_ID, INSERTED.Machine_Class_ID,
                                DELETED.CMPGameType, INSERTED.CMPGameType, 
                                DELETED.Machine_Stock_No, INSERTED.Machine_Stock_No
INTO @_Modified
from machine m
join  installation  i on i.Machine_ID = m.Machine_ID

 IF EXISTS(
                                SELECT 1
                                FROM   @_Modified m
                                WHERE  m.FlagChanged = 1 OR
                                                                m.GameIDChanged = 1 OR
                                                                m.CMPGameTypeChanged = 1 OR
                                                                m.StockNoChanged = 1
                )
                BEGIN
                DECLARE @Machine_ID INT
                SELECT @Machine_ID = MachineId FROM @_Modified m
                                WHERE  m.FlagChanged = 1 OR
                                                                m.GameIDChanged = 1 OR
                                                                m.CMPGameTypeChanged = 1 OR
                                                                m.StockNoChanged = 1
                                EXEC [dbo].[usp_EBS_UpdateMachineDetails] @Machine_ID 
                END

/*****************************************************************************************************
* (CR# 191254 : EBS Communication Service) - MODIFIED BY Venkatesh.H (END)
*****************************************************************************************************/

GO

