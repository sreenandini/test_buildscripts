USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_UpdateNonGamingAsset]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_UpdateNonGamingAsset]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE usp_UpdateNonGamingAsset
@Site_Code VARCHAR(10),
@MAC_ADDDRESS VARCHAR(17),
@IsAuthenticated INT OUTPUT
AS

DECLARE @Machine_ID INT
DECLARE @Site_ID INT
DECLARE @Machine_Site_ID INT
DECLARE @Site_Current_Machine_ID INT
DECLARE @Machine_New_Install INT
DECLARE @IsRegulatoryEnabled VARCHAR(50)
DECLARE @RegulatoryType VARCHAR(50)
DECLARE @TypeComm VARCHAR(100)

SET @IsAuthenticated = 0

--Get the site id & machine id of the current site if any.
SELECT @Site_ID = ISNULL(SITE_ID,0), @Site_Current_Machine_ID = ISNULL(NGA_Machine_ID,0) FROM SITE WHERE SITE_CODE = @SITE_CODE  
--Get the machine id.
SELECT @Machine_ID = ISNULL(Machine_ID,0) FROM dbo.Machine M INNER JOIN dbo.Machine_Class MC
ON M.Machine_Class_ID = MC.Machine_Class_ID
INNER JOIN dbo.Machine_Type MT ON MC.Machine_Type_ID = MT.Machine_Type_ID
WHERE M.Machine_MAC_Address = @MAC_ADDDRESS AND MT.IsNonGamingAssetType = 1

--Get the Site id of the machine if any.
SELECT @Machine_Site_ID = ISNULL(Site_ID,0) FROM Site WHERE NGA_Machine_ID = @Machine_ID

SELECT @IsRegulatoryEnabled = ISNULL(Setting_Value,'False') FROM Setting WHERE Setting_Name = 'IsRegulatoryEnabled'
SELECT @RegulatoryType = ISNULL(Setting_Value,'A') FROM Setting WHERE Setting_Name = 'RegulatoryType'

IF (ISNULL(@Machine_ID,0) <> 0)
BEGIN
	IF (ISNULL(@Machine_Site_ID,0) <> 0)
	BEGIN
		IF(@Machine_Site_ID = ISNULL(@Site_ID,0))
		BEGIN
			SET @IsAuthenticated = 4
		END
		ELSE
		BEGIN
			SET @IsAuthenticated = 5	
		END
	END
	ELSE
	BEGIN
		IF(ISNULL(@Site_Current_Machine_ID,0) <> 0)
		BEGIN		
			--Change the currently assigned NGA to In Stock. 
			UPDATE Machine
			SET Machine_Status_Flag = 0 -- In Stock
			WHERE machine_ID = @Site_Current_Machine_ID

			IF @IsRegulatoryEnabled = 'True' AND @RegulatoryType = 'AAMS'                         
			BEGIN
				--Update new NGA to In Use.
				UPDATE Machine
				SET Machine_Status_Flag = 8 -- AAMS Pending
				WHERE machine_ID = @Machine_ID
			END
			ELSE
			BEGIN
				--Update new NGA to In Use.
				UPDATE Machine
				SET Machine_Status_Flag = 1
				WHERE machine_ID = @Machine_ID
			END

			--Assign the new NGA to site.
			UPDATE SITE
			SET NGA_Machine_ID = @Machine_ID
			WHERE Site_ID = @Site_ID

			IF @IsRegulatoryEnabled = 'True' AND @RegulatoryType = 'AAMS'                         
			BEGIN
				--Add entries for AAMS.
				SET @TypeComm = 'NGA Machine moved from Site to Depot. Swap.'
				EXEC dbo.usp_InsertBMCBASExportRecord @Site_Current_Machine_ID, 3, 304, NULL, 0, @TypeComm

				SET @TypeComm = 'NGA Machine moved from Depot to Site. Swap.'
				SELECT @Machine_New_Install = ISNULL(Machine_New_Install,0) FROM Machine WHERE Machine_ID = @Machine_ID
				IF @Machine_New_Install = 1
				BEGIN					
					EXEC dbo.usp_InsertBMCBASExportRecord @Machine_ID, 3, 303, NULL, 0, @TypeComm
					UPDATE Machine SET Machine_New_Install = 0 WHERE Machine_ID = @Machine_ID
				END
				ELSE
				BEGIN
					EXEC dbo.usp_InsertBMCBASExportRecord @Machine_ID, 3, 304, NULL, 0, @TypeComm
				END
			END

			SET @IsAuthenticated = 3
		END
		ELSE
		BEGIN
			--Assign the NGA to site & change NGA status to 'In Use'.
			UPDATE SITE
			SET NGA_Machine_ID = @Machine_ID
			WHERE Site_ID = @Site_ID

			IF @IsRegulatoryEnabled = 'True' AND @RegulatoryType = 'AAMS'                         
			BEGIN
				UPDATE Machine
				SET Machine_Status_Flag = 8 -- AAMS Pending
				WHERE machine_ID = @Machine_ID
			END
			ELSE
			BEGIN
				UPDATE Machine
				SET Machine_Status_Flag = 1
				WHERE machine_ID = @Machine_ID
			END

			IF @IsRegulatoryEnabled = 'True' AND @RegulatoryType = 'AAMS'                         
			BEGIN
				SET @TypeComm = 'NGA Machine moved from Depot to Site. Install.'
				SELECT @Machine_New_Install = ISNULL(Machine_New_Install,0) FROM Machine WHERE Machine_ID = @Machine_ID
				IF @Machine_New_Install = 1
				BEGIN					
					EXEC dbo.usp_InsertBMCBASExportRecord @Machine_ID, 3, 303, NULL, 0, @TypeComm
					UPDATE Machine SET Machine_New_Install = 0 WHERE Machine_ID = @Machine_ID
				END
				ELSE
				BEGIN
					EXEC dbo.usp_InsertBMCBASExportRecord @Machine_ID, 3, 304, NULL, 0, @TypeComm
				END
			END

			SET @IsAuthenticated = 2
		END
	END
END
ELSE
BEGIN	
	SET @IsAuthenticated = 1
END


GO

