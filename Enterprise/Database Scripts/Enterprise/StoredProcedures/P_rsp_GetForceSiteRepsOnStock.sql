
USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetForceSiteRepsOnStock]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetForceSiteRepsOnStock]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE rsp_GetForceSiteRepsOnStock
AS
BEGIN
SELECT 
System_Parameter_Force_Site_Reps_On_Stock FROM [System_Parameters]
END
GO 

