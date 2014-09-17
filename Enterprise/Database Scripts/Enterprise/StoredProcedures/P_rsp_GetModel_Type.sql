USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetModel_Type]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetModel_Type]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[rsp_GetModel_Type]  
AS  
BEGIN  
  SELECT MT_Model_Name FROM Model_Type MT WITH(NOLOCK) 
  where MT.MT_IsNGA=0    
ORDER BY  MT.MT_Model_Name ASC
END 


