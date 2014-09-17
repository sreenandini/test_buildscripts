USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetAFTSettingsDenom]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetAFTSettingsDenom]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[rsp_GetAFTSettingsDenom]
(
@SiteID INT
)
AS    
BEGIN    
SELECT Denom from AFTSetting WHERE SiteCode=@SiteID
END


GO

