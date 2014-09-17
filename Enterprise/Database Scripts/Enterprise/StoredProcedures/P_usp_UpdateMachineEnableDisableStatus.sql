USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_UpdateMachineEnableDisableStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_UpdateMachineEnableDisableStatus]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--------------------------------------------------------------------------               
--              
-- Description: Update Machine details for processed records      
--                 
--      
-- Outputs:     NONE      
--              
-- RETURN:      NONE        
--              
-- =======================================================================              
--               
-- Revision History              
--               
-- NaveenChander    25/03/2008     Created 		
-- Renjish		    01/12/2009     Updated for AAMS Changes.               
---------------------------------------------------------------------------        
CREATE  Procedure usp_UpdateMachineEnableDisableStatus(@EH_ID Int) As   
Begin  
Declare @Bar_Pos Varchar(20)  
Declare @Site_Code Varchar(20)  
Declare @Site_COmmand Varchar(20)  
--Declare @iMachineStatus INT
--Declare @Machine_ID Varchar(50)
--Declare @SettingReg_Value Varchar(100)
--Declare @SettingRegType_Value Varchar(100)
 
Select @Site_Code = EH_Site_Code, @Bar_Pos = EH_Reference1, @Site_COmmand = EH_Type From Export_History Where EH_ID = @EH_ID  
--Set @iMachineStatus = -1
  
If @Site_COmmand = 'MACHINEDISABLE'  
Begin  
    Update tBP  Set Bar_Position_Machine_Enabled = 0 From  Bar_Position tBP   
    Inner Join Site tS  On tS.Site_ID = tBP.Site_ID Where Site_Code = @Site_Code And Bar_Position_ID = Convert(int, @Bar_Pos)  
	--Set @iMachineStatus = 0
End  
IF @Site_COmmand = 'MACHINEENABLE'  
Begin  
    Update tBP  Set Bar_Position_Machine_Enabled = 1 From  Bar_Position tBP   
    Inner Join Site tS  On tS.Site_ID = tBP.Site_ID Where Site_Code = @Site_Code And Bar_Position_ID = Convert(int, @Bar_Pos)  
	--Set @iMachineStatus = 1
End  

If @Site_COmmand = 'NOTEACCEPTORDISABLE'  
Begin  
    Update tBP  Set Bar_Position_Note_Acceptor_Enabled = 0 From  Bar_Position tBP   
    Inner Join Site tS  On tS.Site_ID = tBP.Site_ID Where Site_Code = @Site_Code And Bar_Position_ID = Convert(int, @Bar_Pos)  
End  
IF @Site_COmmand = 'NOTEACCEPTORENABLE'  
Begin  
    Update tBP  Set Bar_Position_Note_Acceptor_Enabled = 1 From  Bar_Position tBP   
    Inner Join Site tS  On tS.Site_ID = tBP.Site_ID Where Site_Code = @Site_Code And Bar_Position_ID = Convert(int, @Bar_Pos)  
End  

--EXEC rsp_GetSetting NULL, 'IsRegulatoryEnabled', 'false', @SettingReg_Value OUTPUT
--EXEC rsp_GetSetting NULL, 'RegulatoryType', '', @SettingRegType_Value OUTPUT
--
--IF LOWER(@SettingReg_Value) = 'true' AND LOWER(@SettingRegType_Value) = 'aams'
--Begin
--	IF @iMachineStatus = 0 OR @iMachineStatus = 1
--	Begin
--		SELECT @Machine_ID = Machine_ID FROM Installation WHERE Bar_Position_ID = Convert(int, @Bar_Pos)  
--		IF(ISNULL(@Machine_ID,'') <> '')
--		BEGIN
--			EXEC dbo.usp_InsertBMCBASExportRecord @Machine_ID, 3, 701, NULL
--		END
--	End
--End
  
End
GO

