USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetJobProfilesList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetJobProfilesList]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
/*
 *	this stored procedure is to fetch the available profiles for Scheduled Jobs
 *
 *	Change History:
 *	
 *	Sudarsan S		16-02-2009		created
*/
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[rsp_GetJobProfilesList]
AS
SELECT [ID], [Name] AS Profile_Description, [Description] FROM dbo.ScheduleProfile



GO

