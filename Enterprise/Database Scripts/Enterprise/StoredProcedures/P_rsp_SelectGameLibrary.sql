USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_SelectGameLibrary]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_SelectGameLibrary]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE rsp_SelectGameLibrary
AS
BEGIN
SELECT * FROM dbo.Game_Library	
END


GO

