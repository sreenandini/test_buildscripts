USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_ResetInProgressRecordsinEH]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_ResetInProgressRecordsinEH]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-------------------------------------------------------------------------- 
---
--- Description: update in progress records in EH
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
CREATE PROCEDURE dbo.usp_ResetInProgressRecordsinEH
	@Site_Code	VARCHAR(50)
AS
BEGIN

	UPDATE dbo.Export_History SET EH_Status = NULL WHERE EH_Status = 1 AND EH_Site_Code = @Site_Code

END

GO

