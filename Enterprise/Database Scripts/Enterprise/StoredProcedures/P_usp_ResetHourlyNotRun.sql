USE [Enterprise]
GO

/****** Object:  StoredProcedure [dbo].[usp_ResetHourlyNotRun]    Script Date: 01/30/2013 05:11:14 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_ResetHourlyNotRun]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_ResetHourlyNotRun]
GO


/****** Object:  StoredProcedure [dbo].[usp_ResetHourlyNotRun]    Script Date: 01/30/2013 05:05:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
EXEC [usp_ResetHourlyNotRun]
*/
CREATE PROCEDURE [dbo].[usp_ResetHourlyNotRun]
AS
BEGIN
	 
	 UPDATE SITE SET 
	 HourlyNotRun = NULL
	 
	 RETURN @@ERROR 
END 
GO

