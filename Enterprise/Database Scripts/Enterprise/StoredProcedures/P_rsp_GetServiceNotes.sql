USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetServiceNotes]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetServiceNotes]
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
-- Description: Fetches the Notes for the given Service
--
-- Inputs:     Job ID
-- Outputs:     
--
-- =======================================================================
-- 
-- Revision History
-- 
-- Sudarsan S	 24/08/2009   Created
-- Vaishnavi	 17/09/2009   Replaced Username with Firstname,LastName in Staff_Name
--------------------------------------------------------------------------- 
CREATE PROCEDURE [dbo].[rsp_GetServiceNotes] 
	@JobID	VARCHAR(20)
AS

BEGIN

	SELECT @JobID = SUBSTRING(@JobID, 1, CHARINDEX('/', @JobID) - 1)

	SELECT  CONVERT(VARCHAR, CONVERT(DATETIME, SN.Service_Notes_Date, 3), 106) + ' ' + CONVERT(VARCHAR, CONVERT(DATETIME, SN.Service_Notes_Date, 3), 108) AS NoteDate,
			SN.Service_Notes_Notes,
			S.Staff_First_name + ', '+ S.Staff_Last_Name AS Staff_Name  --U.UserName
	  FROM  dbo.Service_Notes SN
	  INNER JOIN  dbo.[User] U ON SN.Staff_ID = U.SecurityUserID 
	  INNER JOIN  dbo.[Staff] S ON S.UserTableID = U.SecurityUserID 
	  WHERE  SN.Service_ID = CAST(@JobID AS INT)
	  ORDER BY CONVERT(DATETIME, SN.Service_Notes_Date, 3) DESC

END


GO

