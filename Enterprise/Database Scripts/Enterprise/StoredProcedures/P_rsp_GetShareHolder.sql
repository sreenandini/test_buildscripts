USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetShareHolder]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetShareHolder]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[rsp_GetShareHolder]  
AS  
BEGIN  
SELECT * FROM ShareHolders where SysDelete = 0  
  
END  

GO

