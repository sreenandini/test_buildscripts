USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_ImportInstallationGameDataFromXML]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_ImportInstallationGameDataFromXML]
GO

USE [Enterprise]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO

-----------------------------------------------------------------------------------------------------        
--                
-- Description: Import Installation Game Data from XML
-- Inputs:      Game Data XML            
--                
-- Outputs:     
--                                  
-- =======================================================================                
--                 
-- Revision History                
--                
-- Yoganandh P		20/10/2010		Created
------------------------------------------------------------------------------------------------------    
CREATE PROCEDURE rsp_ImportInstallationGameDataFromXML
(
	@doc VARCHAR(MAX)
)
AS
DECLARE @InstallationID INT
DECLARE @idoc INT

BEGIN

	--Create an internal representation of the XML document.
	EXEC sp_xml_preparedocument @idoc OUTPUT, @doc  

    SELECT  @InstallationID = Installation_Id    
	FROM OPENXML (@idoc, './InstallationGameData/GameData',2)        
	WITH (Installation_Id int './Installation_Id')  

	DELETE FROM Installation_Game_Data WHERE Installation_Id = @InstallationID
	
	--Insert the XML data in to Installation_Game_Data table
	INSERT INTO Installation_Game_Data
	SELECT * FROM OPENXML (@idoc, '/InstallationGameData/GameData',2)         
	WITH Installation_Game_Data

	--Removes the internal representation of the XML document.
	EXEC sp_xml_removedocument @idoc

END




GO

