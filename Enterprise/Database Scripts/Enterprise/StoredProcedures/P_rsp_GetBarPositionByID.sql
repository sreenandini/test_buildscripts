USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetBarPositionByID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetBarPositionByID]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE rsp_GetBarPositionByID
@BarPositionID INT
AS
BEGIN 
SELECT * FROM Bar_Position WHERE Bar_Position_ID=@BarPositionID
END


GO

