USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetMachineTypeCode]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetMachineTypeCode]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[rsp_GetMachineTypeCode]  
AS  
BEGIN  
 SELECT DISTINCT Machine_Type_Code FROM Machine_Type WITH(NOLOCK)    
 where IsNonGamingAssetType=0
ORDER BY  Machine_Type_Code ASC 
END 

