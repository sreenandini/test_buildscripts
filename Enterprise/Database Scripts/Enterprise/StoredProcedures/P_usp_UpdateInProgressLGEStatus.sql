USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_UpdateInProgressLGEStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_UpdateInProgressLGEStatus]
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
CREATE PROCEDURE dbo.usp_UpdateInProgressLGEStatus
AS
BEGIN

	UPDATE dbo.LGE_Export_History SET LGE_EH_Status = NULL WHERE LGE_EH_Status = 1

END

GO

