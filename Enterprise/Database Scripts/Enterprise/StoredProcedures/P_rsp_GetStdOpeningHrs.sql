USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetStdOpeningHrs]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetStdOpeningHrs]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE  PROCEDURE [dbo].[rsp_GetStdOpeningHrs] 

AS

BEGIN

SELECT 
	Standard_Opening_Hours_ID, 
	Standard_Opening_Hours_Description 
FROM Standard_Opening_Hours 
ORDER BY Standard_Opening_Hours_Description

END

GO

