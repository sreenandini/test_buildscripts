USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetOperatorInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetOperatorInfo]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE  PROCEDURE [dbo].[rsp_GetOperatorInfo] 

AS

BEGIN

SELECT
	Operator_Name, 
	Operator_ID 
FROM Operator  
WHERE ISNULL(operator_end_date, '') = '' 
ORDER BY Operator_Name ASC

END

GO

