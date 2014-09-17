USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_SelectMachineClassDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_SelectMachineClassDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE rsp_SelectMachineClassDetails
AS
BEGIN
	SELECT * FROM dbo.Machine_Class
END


GO

