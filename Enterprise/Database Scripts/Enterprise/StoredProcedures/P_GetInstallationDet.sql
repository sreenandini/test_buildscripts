USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetInstallationDet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetInstallationDet]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[GetInstallationDet](@machineID as int)
as 
	SELECT Top 1 * From Installation 
	  WHERE Machine_ID=@machineID 
	  ORDER BY Installation_Start_Date ASC
GO

