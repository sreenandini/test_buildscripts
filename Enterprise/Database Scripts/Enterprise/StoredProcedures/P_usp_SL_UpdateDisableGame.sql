USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_SL_UpdateDisableGame]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_SL_UpdateDisableGame]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- usp_SL_UpdateDisableGame    
-- -----------------------------------------------------------------    
--    
-- To update disable games for expired/cancelled licenses.
-- Iff Disable Games set to true for those licenses.
--     
-- -----------------------------------------------------------------        
-- Revision History           
--           
-- 19/04/2012 Dinesh Rathinavel Created
--    
-- =================================================================  

CREATE PROCEDURE [dbo].[usp_SL_UpdateDisableGame]
	@LicenseInfoId INT = 0
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @TodayDate DATETIME
	SET @TodayDate = GETDATE()


	INSERT INTO Export_History (EH_Date, EH_Reference1, EH_Type, EH_Site_Code)
		SELECT GETDATE(),MAX(L.[LicenseInfoID]),'SITELICENSING',LTRIM(RTRIM(S.[Site_Code])) AS Site_Code FROM [dbo].[SL_LicenseInfo] L  

		INNER JOIN [dbo].[Site] S ON S.[Site_ID] = L.[Site_ID]  
		WHERE 
		L.ActivatedDateTime IS NOT NULL AND L.[StartDate] < @TodayDate
		GROUP BY S.[Site_Code]
END
GO

