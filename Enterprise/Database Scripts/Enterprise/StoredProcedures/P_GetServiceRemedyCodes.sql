USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetServiceRemedyCodes]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetServiceRemedyCodes]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[GetServiceRemedyCodes]
as
SELECT
    Call_Remedy."Call_Remedy_Description", Call_Remedy."Call_Remedy_Reference", Call_Remedy."Call_Remedy_Attract_Downtime", Call_Remedy."Call_Remedy_End_Date"
FROM
    "Call_Remedy" Call_Remedy
WHERE
    Call_Remedy."Call_Remedy_End_Date" IS NULL

GO

