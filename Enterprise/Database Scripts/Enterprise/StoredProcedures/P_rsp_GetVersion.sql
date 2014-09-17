USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetVersion]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetVersion]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--------------------------------------------------------------------------   
--  
-- Description: Fetches the current version of BMC

-- =======================================================================  
--   
-- Revision History  

-- Kirubakar S 12/05/2010 Created
  
--------------------------------------------------------------------------- 

CREATE Procedure rsp_GetVersion
AS
BEGIN
SELECT TOP 1 versionname as RESULT
FROM VersionHistory 
ORDER BY VersionDate DESC
END
GO

