USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetOperatorName]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetOperatorName]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[rsp_GetOperatorName]  
AS  
BEGIN  
  SELECT Operator_Name FROM Operator WITH(NOLOCK)    
ORDER BY  Operator_Name ASC
END 

