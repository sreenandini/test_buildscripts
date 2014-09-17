USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_FetchImportTypeList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_FetchImportTypeList]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-------------------------------------------------------------------------- 
--
-- Description: fetch the types of Import records
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

CREATE PROCEDURE [dbo].[rsp_FetchImportTypeList]
AS
BEGIN
	SELECT * FROM dbo.Import_History_Types ORDER BY IH_Type_Desc
END

GO

