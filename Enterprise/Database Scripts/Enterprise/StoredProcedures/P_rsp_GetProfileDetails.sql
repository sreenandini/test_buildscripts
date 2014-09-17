USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetProfileDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetProfileDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE rsp_GetProfileDetails
AS
SELECT SettingsProfile_ID,SettingsProfile_Description FROM SettingsProfile order by SettingsProfile_Description


GO

