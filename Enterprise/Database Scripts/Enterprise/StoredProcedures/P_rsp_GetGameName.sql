USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetGameName]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetGameName]
GO

USE [Enterprise]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO

-----------------------------------------------------------------------------------------------------        
--                
-- Description:		Get Game Name for Installation
-- Inputs:			Installation No         
--                
-- Outputs:			Game Name
--                                  
-- =======================================================================                
--                 
-- Revision History                
--                
-- Yoganandh P 		19/10/2010		Created
------------------------------------------------------------------------------------------------------     
CREATE PROCEDURE rsp_GetGameName
(
	@InstallationId		Int,
	@GameName			Varchar(100) OUTPUT,
	@PreviousInstallationId	Int OUTPUT 
)
AS

DECLARE @MachineId AS INT
DECLARE @CurrentRowCount AS INT

SELECT @MachineId	=	Machine_Id FROM Installation Where Installation_Id = @InstallationId
SELECT @GameName	=	''

BEGIN

	SELECT * INTO #InstallationTempTable FROM
	(
		SELECT Row_Number() OVER(ORDER BY tI.Installation_ID) AS 'InstallationRowNumber',   
		tI.Installation_ID FROM Installation tI	WHERE Machine_Id = @MachineId
		AND tI.Installation_ID <> @InstallationId
	) tIT

	SELECT @CurrentRowCount		=	COUNT(*) from #InstallationTempTable
	
	WHILE @CurrentRowCount <> 0
	BEGIN	
		SELECT @PreviousInstallationId = Installation_ID FROM #InstallationTempTable WHERE InstallationRowNumber = @CurrentRowCount
			IF EXISTS(SELECT Game_Name FROM Installation_Game_Data WHERE Installation_Id = @PreviousInstallationId)
				BEGIN
					SELECT @GameName = UPPER(Game_Name) FROM Installation_Game_Data WHERE Installation_Id = @PreviousInstallationId
						IF @GameName <> ''
							BREAK
				END								
		SET @CurrentRowCount = @CurrentRowCount - 1
	END	
END


GO

