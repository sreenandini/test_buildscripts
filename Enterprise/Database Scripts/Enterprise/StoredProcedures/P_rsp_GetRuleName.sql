/************************************************************
 * Author: Aishwarrya V S
 * Time: 15/01/2014 5:28:37 PM
 * Desc: Gets the Site License Rule Name for Report filter criteria.
 ************************************************************/


USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_GetRuleName]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_GetRuleName]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[rsp_GetRuleName]
AS
BEGIN
	SELECT DISTINCT RuleName
	FROM   SL_Rules SR
	       INNER JOIN SL_LicenseInfo sli
	            ON  SR.RuleID = SLI.RuleID
END

GO

