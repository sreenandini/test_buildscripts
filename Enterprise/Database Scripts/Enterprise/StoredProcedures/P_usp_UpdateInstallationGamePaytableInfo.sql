USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_UpdateInstallationGamePaytableInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_UpdateInstallationGamePaytableInfo]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-------------------------------------------------------------------------- 
--
-- Description: Update Installation Game Paytable Information
-- Inputs:      See inputs 
--
-- Outputs:         
--                  
-- =======================================================================
-- 
-- Revision History
--
-- Yoganandh P		06/01/2011		Created
------------------------------------------------------------------------------------------------------

CREATE PROCEDURE dbo.usp_UpdateInstallationGamePaytableInfo
	@doc  XML 
AS  
  
BEGIN  
   
DECLARE @idoc INT  
DECLARE @InstallationID INT  
DECLARE @GameID INT  
DECLARE @PaytableID INT  
  
 EXEC sp_xml_preparedocument @idoc OUTPUT, @doc  
  
 SELECT @InstallationID = Installation_ID, @GameID = Game_ID, @PaytableID = Paytable_ID FROM OPENXML(@idoc, './InstallationGamePaytableInfo/Installation_Game_Paytable_Info', 2) WITH  
  (
   Installation_ID INT './Installation/HQ_Installation_No',
   Game_ID VARCHAR(100) './IGPI_Game_ID',  
   Paytable_ID VARCHAR(20) './IGPI_Paytable_ID'   
  )  
  
 EXEC sp_xml_removedocument @idoc  
 
 INSERT INTO dbo.Installation_Game_Paytable_Info(IGPI_Installation_ID, IGPI_Game_ID, IGPI_Paytable_ID) 
 VALUES (@InstallationID, @GameID, @PaytableID)  
   
END  


GO

