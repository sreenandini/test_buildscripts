USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_ExportLookupMasterALL]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_ExportLookupMasterALL]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[rsp_ExportLookupMasterALL]
AS                      
BEGIN      
 
  SELECT *  
  FROM LookupMaster
  FOR XML AUTO, ELEMENTS, ROOT('LookupMasters')      
 
END 
GO

