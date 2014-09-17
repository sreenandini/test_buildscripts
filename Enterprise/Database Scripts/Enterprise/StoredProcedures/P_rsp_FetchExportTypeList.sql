USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_FetchExportTypeList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_FetchExportTypeList]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-------------------------------------------------------------------------- 
--
-- Description: fetch the types of Export records
-- Inputs:      See inputs 
--
-- Outputs:         
--                  
-- =======================================================================
-- 
-- Revision History
--
-- Sudarsan S		31/03/2010		Created
------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].[rsp_FetchExportTypeList]
AS
BEGIN
	SELECT * FROM dbo.Export_History_Types ORDER BY EH_Type_Desc
END


GO

