USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetDepotName]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetDepotName]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[rsp_GetDepotName]  
AS  
BEGIN  
 SELECT Depot_Name FROM Depot WITH(NOLOCK)    
ORDER BY  Depot_Name ASC
END 
