USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_SL_ExportActiveLicenseInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_SL_ExportActiveLicenseInfo]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[rsp_SL_ExportActiveLicenseInfo]
@LicenseInfoID INT
AS                    
BEGIN 
	SELECT 
		  [LicenseInfo].[LicenseInfoID],
		  [LicenseInfo].[ActivatedDateTime],
		  [LicenseInfo].[ActivatedStaffID]
	  FROM [dbo].[SL_LicenseInfo] LicenseInfo WITH (NOLOCK)
	  INNER JOIN dbo.[SL_Rules] [Rule] WITH (NOLOCK) ON [LicenseInfo].[RuleID] = [Rule].[RuleID] 
	WHERE [LicenseInfo].[LicenseInfoID] = @LicenseInfoID
	 FOR XML AUTO, ELEMENTS, ROOT('LicenseDetails')
END
	 
	 
