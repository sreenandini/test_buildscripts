USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetVerificationExportData]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetVerificationExportData]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-------------------------------------------------------------------------- 
---
--- Description: Get the data to export to exchange
---
--- Inputs:      see inputs
---
--- Outputs:     
--- 
--- =======================================================================
--- 
--- Revision History
--- 
--- Sudarsan S  24/01/10     Created 
--------------------------------------------------------------------------- 
CREATE PROCEDURE dbo.rsp_GetVerificationExportData
AS
BEGIN

	SELECT S.Site_Code, L.LGE_EH_ID, M.Machine_Manufacturers_Serial_No, L.LGE_EH_AAMS_Message_ID
	  INTO #TempVerification
	  FROM dbo.LGE_Export_History L
INNER JOIN dbo.BMC_AAMS_Details BAD ON (BAD.BAD_AAMS_Code = L.LGE_EH_Reference AND L.LGE_EH_Type = 'VERIFYOS')-- AND BAD.BAD_AAMS_Entity_Type = 3)
				OR (L.LGE_EH_Message_Reference = BAD.BAD_AAMS_Code AND L.LGE_EH_Type = 'VERIFYGAME')-- AND BAD.BAD_AAMS_Entity_Type = 4)
--INNER JOIN dbo.Machine M ON L.LGE_EH_Reference = M.Machine_ID AND L.LGE_EH_Type = 'VERIFYOS' 
--			OR L.LGE_EH_Message_Reference = M.Machine_ID AND L.LGE_EH_Type = 'VERIFYGAME'
INNER JOIN dbo.Machine M ON BAD.BAD_Asset_Serial_No = M.Machine_Manufacturers_Serial_No
INNER JOIN dbo.Installation I ON M.Machine_ID = I.Machine_ID
INNER JOIN dbo.Bar_Position B ON I.Bar_Position_ID = B.Bar_Position_ID
INNER JOIN dbo.Site S ON S.Site_ID = B.Site_ID
	WHERE ISNULL(L.LGE_EH_Status, 0) = 0 AND I.Installation_End_Date IS NULL

	UPDATE L
	   SET L.LGE_EH_Status = 1
	  FROM dbo.LGE_Export_History L
INNER JOIN #TempVerification T ON L.LGE_EH_ID = T.LGE_EH_ID

	SELECT * FROM #TempVerification

END

GO

