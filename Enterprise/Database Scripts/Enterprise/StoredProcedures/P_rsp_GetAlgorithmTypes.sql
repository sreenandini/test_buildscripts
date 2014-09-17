USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetAlgorithmTypes]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetAlgorithmTypes]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =======================================================================    
-- OUTPUT --Select Algorithm types -- exec rsp_GetAlgorithmTypes     
-- Revision History    
-- M Senthil 28/05/2010  Created    
-- ======================================================================= 

CREATE PROCEDURE rsp_GetAlgorithmTypes
@CompID INT,
@New INT
AS

DECLARE @CompType VARCHAR(50)

IF (@New = 1)
BEGIN
	SELECT @CompType = CCT_Name FROM CV_Component_Types WHERE CCT_Code = @CompID
END
ELSE
BEGIN
	SELECT @CompType = CCT.CCT_Name FROM CV_Component_Types CCT INNER JOIN CV_Component_Details CCD
	ON CCT.CCT_Code = CCD.CCD_CCT_Code  WHERE CCD.CCD_ID = @CompID
END

IF(@CompType = 'MC300')
BEGIN
	SELECT CAT_Code, CAT_Name FROM CV_Algorithm_Types WHERE CAT_Name IN ('SHA1', 'SHA512') ORDER BY CAT_Name
	RETURN
END
ELSE
	BEGIN
		IF (@New = 1)
		BEGIN
			SELECT CAT_Code, CAT_Name FROM CV_Algorithm_Types WHERE CAT_Name <> 'SHA512' ORDER BY CAT_Name
			RETURN
		END
		ELSE
		BEGIN
		IF	EXISTS (SELECT CVCSA_CAT_Code	FROM CV_Component_Supported_Algorithm WHERE CVCSA_CCD_ID = @CompID)
			SELECT CAT.CAT_Code, CAT.CAT_Name FROM CV_Algorithm_Types CAT 
			INNER JOIN CV_Component_Supported_Algorithm CCSA ON CAT.CAT_Code = CCSA.CVCSA_CAT_Code
			WHERE CVCSA_CCD_ID = @CompID ORDER BY CAT.CAT_Name
		
		ELSE
			SELECT CAT_Code, CAT_Name FROM CV_Algorithm_Types CAT ORDER BY CAT.CAT_Name			

		END
END


GO

