USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_UpdateUnverifiedComponentsData]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_UpdateUnverifiedComponentsData]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--------------------------------------------------------------------------
-------------------------------------------------------------------------- 
---
--- Description: Get  the unverified Comp Data.
---
--- Inputs:      see inputs
---
--- Outputs:     
--- 
--- =======================================================================
--- 
--- Revision History
--- 
--- Senthil  04/06/10     Created 
--------------------------------------------------------------------------- 
CREATE PROCEDURE dbo.usp_UpdateUnverifiedComponentsData
@Serial_No VARCHAR(30),
@ComponentID INT,
@CVD_ID INT
AS
BEGIN

	DECLARE @EnteredHashValue VARCHAR(150)
	DECLARE @RecevdHashValue VARCHAR(150)
	DECLARE @SeedValue VARCHAR(50)
	DECLARE @CATCode INT
	DECLARE @VerificationStatus INT
	DECLARE @CompType VARCHAR(50)

	SELECT  @CATCode = CD.CCD_CAT_Code,
			@SeedValue = CD.CCD_Seed_Value,		
			@EnteredHashValue = CD.CCD_Hash_Value,
			@CompType = CCT.CCT_Name
	FROM dbo.CV_Component_Details CD
	INNER JOIN dbo.CV_Component_Types CCT ON CD.CCD_CCT_Code = CCT.CCT_Code
	WHERE CD.CCD_ID = @ComponentID

	SELECT  @RecevdHashValue = MD.CVMCD_Hash_Value_Actual
	FROM CV_Machine_Component_Details MD 
	WHERE MD.CVMCD_Machine_Serial_No = @Serial_No AND MD.CVMCD_CCD_ID = @ComponentID

	IF ISNULL(@EnteredHashValue, '') = '' OR ISNULL(@RecevdHashValue, '') = ''
	RETURN

	IF @EnteredHashValue = @RecevdHashValue
		BEGIN
			UPDATE CV_Verification_Details
			SET CVD_Verification_Status = 1,
				CVD_Request_Status = 0,
				CVD_CAT_Code = @CATCode,
				CVD_Seed_Value = @SeedValue,
				CVD_Hash_Value = @EnteredHashValue,
				CVD_Hash_Value_Recieved = @RecevdHashValue,
				CVD_Verification_Time = GETDATE()
			WHERE CVD_ID =  @CVD_ID

			UPDATE CV_Machine_Component_Details
			SET CVMCD_Status = 1
			WHERE CVMCD_Machine_Serial_No = @Serial_No AND CVMCD_CCD_ID = @ComponentID 
		END
	ELSE
		BEGIN
			UPDATE CV_Verification_Details
			SET CVD_Verification_Status = 0,
				CVD_Request_Status = 0,
				CVD_CAT_Code = @CATCode,
				CVD_Seed_Value = @SeedValue,
				CVD_Hash_Value = @EnteredHashValue,
				CVD_Hash_Value_Recieved = @RecevdHashValue,
				CVD_Verification_Time = GETDATE()
			WHERE CVD_ID =  @CVD_ID
	
			UPDATE CV_Machine_Component_Details
			SET CVMCD_Status = 0
			WHERE CVMCD_Machine_Serial_No = @Serial_No AND CVMCD_CCD_ID = @ComponentID 
		END

	IF @CompType = 'GAME'
		BEGIN
			--Game Verification.
			SELECT @VerificationStatus = ISNULL(CMCD.CVMCD_Status,0) 
			FROM dbo.CV_Machine_Component_Details CMCD INNER JOIN dbo.CV_Component_Types CCT ON 
			CMCD.CVMCD_CCT_Code = CCT.CCT_Code WHERE CMCD.CVMCD_Machine_Serial_No = @Serial_No 
			AND CCT.CCT_Name = 'GAME' AND CMCD.CVMCD_IsAvailable = 1

			UPDATE IGI
			SET Game_Verification = ISNULL(@VerificationStatus,0),
			Game_Floor_Controller_Status = 0
			FROM dbo.Installation_Game_Info IGI 
			INNER JOIN dbo.Installation I ON IGI.Installation_No = I.Installation_ID
			INNER JOIN dbo.Machine M ON I.Machine_ID = M.Machine_ID
			WHERE M.Machine_Manufacturers_Serial_No = @Serial_No AND I.Installation_End_Date IS NULL 
			AND IGI.IsAvailable = 1 AND IGI.Game_Verification <> ISNULL(@VerificationStatus,0)
		END

	-- VLT Verification.
	-- First check for the Component Count.
	DECLARE @CompCountActual INT
	DECLARE @CompCountRec INT

	SELECT @CompCountActual = CCRS_Component_Count FROM dbo.CV_Component_Request_Status
		WHERE CCRS_Machine_Serial_No = @Serial_No
	SELECT @CompCountRec = COUNT(*) FROM CV_Machine_Component_Details 
		WHERE CVMCD_Machine_Serial_No = @Serial_No AND CVMCD_IsAvailable = 1

	IF(@CompCountRec > @CompCountActual)
	BEGIN
		IF EXISTS(SELECT CVMCD_CCT_Code FROM CV_Machine_Component_Details 
		WHERE CVMCD_Machine_Serial_No = @Serial_No AND CVMCD_Status = 0 AND CVMCD_IsAvailable = 1)
		BEGIN
			UPDATE BMC_AAMS_Details
			SET BAD_Verification_Status = 0,
			BAD_Entity_Floor_Controller_Status = 0,
			BAD_Updated_Date = GETDATE(),
			BAD_Comments = 'Component Verification Failed. Component ID - ' + CAST(@ComponentID As VARCHAR) 
			WHERE BAD_Asset_Serial_No = @Serial_No 
			AND BAD_AAMS_Entity_Type = 3 --AND BAD_Verification_Status <> 0
		END
		ELSE
		BEGIN
			UPDATE BMC_AAMS_Details
			SET BAD_Verification_Status = 1,
			BAD_Entity_Floor_Controller_Status = 0,
			BAD_Updated_Date = GETDATE(),
			BAD_Comments = 'Component Verification Passed.'
			WHERE BAD_Asset_Serial_No = @Serial_No 
			AND BAD_AAMS_Entity_Type = 3 ---AND BAD_Verification_Status <> 1
		END

		--AAMS On Demand Request Update.
		DECLARE @VerType INT
		DECLARE @ID INT

		SELECT @VerType = CVD_Verification_Type FROM dbo.CV_Verification_Details WHERE CVD_ID = @CVD_ID

		--Check for type AAMS On Demand Type - VLT.
		IF(ISNULL(@VerType,0) = 5)
		BEGIN			
			SELECT TOP 1 @ID = LEH.LGE_EH_ID FROM dbo.LGE_Export_History LEH
			INNER JOIN dbo.BMC_AAMS_Details BD ON LEH.LGE_EH_Reference = BD.BAD_AAMS_Code 
			WHERE LEH.LGE_EH_Status = 50 AND BD.BAD_Asset_Serial_No = @Serial_No
			ORDER BY LGE_EH_ID ASC

			UPDATE dbo.LGE_Export_History 
				SET LGE_EH_Status = 100,
					LGE_EH_Export_Date = GETDATE()
			WHERE LGE_EH_ID = @ID
		END
		
		--Check for type AAMS On Demand Type - Game.
		IF(ISNULL(@VerType,0) = 6)
		BEGIN			
			SELECT TOP 1 @ID = LEH.LGE_EH_ID FROM dbo.LGE_Export_History LEH
			INNER JOIN dbo.BMC_AAMS_Details BD ON LEH.LGE_EH_Message_Reference = BD.BAD_AAMS_Code 
			WHERE LEH.LGE_EH_Status = 50 AND BD.BAD_Asset_Serial_No = @Serial_No
			ORDER BY LGE_EH_ID ASC

			UPDATE dbo.LGE_Export_History 
				SET LGE_EH_Status = 100,
					LGE_EH_Export_Date = GETDATE()
			WHERE LGE_EH_ID = @ID

			UPDATE dbo.CV_Verification_Details 
			SET CVD_Verification_Type = 5 -- This is bcoz as per the UI just AAMS On Demand option is available.
			WHERE CVD_ID = @CVD_ID
		END

	END
	ELSE
	BEGIN
		UPDATE BMC_AAMS_Details
		SET BAD_Verification_Status = 0,
		BAD_Entity_Floor_Controller_Status = 0,
		BAD_Updated_Date = GETDATE(),
		BAD_Comments = 'Component Verification Failed. Comp Count Mistmatch - ' + CAST(@CompCountActual As VARCHAR) + ',' + CAST(@CompCountActual As VARCHAR) 
		WHERE BAD_Asset_Serial_No = @Serial_No 
		 AND BAD_AAMS_Entity_Type = 3 AND BAD_Verification_Status <> 0
	END
END	


GO

