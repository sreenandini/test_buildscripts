USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetAFTSetting]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetAFTSetting]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[rsp_GetAFTSetting]
(
@SiteID INT,@Denom INT
)
AS    
BEGIN    
SELECT * from AFTSetting WHERE SiteCode=@SiteID and Denom=@Denom
END


GO

