USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_UpdateGameForInstallationFromXML]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_UpdateGameForInstallationFromXML]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


Create proc usp_UpdateGameForInstallationFromXML	
(
@doc xml	
)
as
DECLARE @docHandle int    
EXEC sp_xml_preparedocument @docHandle OUTPUT, @doc    
declare @MachineClassID int  
declare @MachineName varchar(200)  
declare @machineType varchar(200)
declare @Manufacturer_Name varchar(200)
declare @Manufacturer_Id INT   
declare @InstallationID INT
declare @IsMultiGame BIT
  
 SELECT @InstallationID = x.Installation_ID,  
  @MachineName = x.Machine_Name,  
  @MachineType = x.Machine_Type_Name,
  @Manufacturer_Name = x.Manufacturer_Name 
  FROM OPENXML (@docHandle,  '/Game/InstallationGame',2)    
  with    
  (    
      Installation_ID int  'Installation_ID'  ,  
	Machine_Name varchar(200) 'Machine_Name',  
	Machine_Type_Name varchar(200) 'Machine_Type_Name' ,
	Manufacturer_Name  varchar(200) 'Manufacturer_Name'
  ) x    
  
EXEC sp_xml_removedocument @dochandle    
  
SET @Manufacturer_Id = 0

SELECT @IsMultiGame = CASE WHEN @MachineName = 'MULTI GAME' THEN 1 ELSE 0 END

SELECT @Manufacturer_Id =  Manufacturer_ID FROM Manufacturer WITH(NOLOCK) WHERE Manufacturer_Name = @Manufacturer_Name

select @MachineClassID = mc.machine_class_ID from dbo.machine_class mc inner join dbo.machine_type mt on mc.machine_type_id = mt.machine_type_id  
where mc.machine_name=@MachineName and mt.Machine_Type_Code = @machineType AND mc.Manufacturer_ID = ISNULL(@Manufacturer_Id,0)
  
if @MachineClassID is not null  
begin  
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

 update m set machine_class_ID = @MachineClassID,IsMultiGame = @IsMultiGame  
 OUTPUT INSERTED.Machine_ID,
                                DELETED.Machine_Status_Flag, INSERTED.Machine_Status_Flag,
                                DELETED.Machine_Class_ID, INSERTED.Machine_Class_ID,
                                DELETED.CMPGameType, INSERTED.CMPGameType, 
                                DELETED.Machine_Stock_No, INSERTED.Machine_Stock_No
INTO @_Modified
 from machine m  
 join  installation  i on i.Machine_ID = m.Machine_ID  
  where i.Installation_ID = @InstallationID  
 
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
* (CR# 191254 : EBS Communication Service) - MODIFIED BY Venkatesh.H  (END)
*****************************************************************************************************/

end  
GO