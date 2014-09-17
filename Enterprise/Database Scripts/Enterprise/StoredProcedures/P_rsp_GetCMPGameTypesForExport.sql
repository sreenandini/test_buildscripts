USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetCMPGameTypesForExport]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetCMPGameTypesForExport]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- -----------------------------------------------------------------    
--    
-- Get the CMP Game Type Details to export to Exchange.      
--     
-- rsp_GetCMPGameTypesForExport 430
-- -----------------------------------------------------------------        
-- Revision History           
--           
-- 17/01/2012 Anuradha	 Created         
-- =================================================================    
  
CREATE PROCEDURE dbo.rsp_GetCMPGameTypesForExport  
@MachineID INT  
AS  
  
DECLARE @GameTypeForMachine XML 

SET @GameTypeForMachine = (SELECT machine_id AS MachineID, machine_stock_no AS Asset, CMPGameType FROM [Machine] m where m.machine_Id = @MachineID
 FOR XML PATH ('MachineType'), ELEMENTS XSINIL, ROOT('MachineGameTypes'))

DECLARE @CMPGameTypes XML 

SET @CMPGameTypes =
( 
  
SELECT GameTypeID,
GameTypeCode AS GameType,
GamePrefix AS Prefix

FROM dbo.tblCMPGameTypes tCG
Inner Join Machine M on M.CmpGameType = tcg.GamePrefix 

WHERE m.Machine_ID = @MachineID
FOR XML PATH ('GameType') ,ELEMENTS XSINIL,ROOT('GameTypes') ) 

SELECT @GameTypeForMachine,@CMPGameTypes FOR XML PATH('Details'), ROOT('CMPGameTypeDetails')

GO

