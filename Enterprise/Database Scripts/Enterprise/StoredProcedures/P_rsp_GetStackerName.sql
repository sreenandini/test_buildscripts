USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetStackerName]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetStackerName]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[rsp_GetStackerName]
AS  
BEGIN  

SELECT DISTINCT StackerName FROM Stacker WITH (NOLOCK)
ORDER BY StackerName ASC
END 
