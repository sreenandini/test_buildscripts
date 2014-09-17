USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetCompanyName]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetCompanyName]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[rsp_GetCompanyName]
AS
BEGIN
  SELECT c.Company_Name, c.Company_ID
  FROM Company c WITH (NOLOCK)ORDER BY c.Company_Name ASC
END

GO

