USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetProfitShareGroupNameList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetProfitShareGroupNameList]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].rsp_GetProfitShareGroupNameList    
AS    
BEGIN    
	SELECT ProfitShareGroupID,ProfitShareGroupName,ProfitSharePercentage
	FROM ProfitShareGroup WHERE SysDelete=0  
END 

GO

