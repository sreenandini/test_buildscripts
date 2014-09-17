USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_ExportCodeMaster]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_ExportCodeMaster]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

                             
CREATE PROCEDURE [dbo].[rsp_ExportCodeMaster]     
@CodeMasterID INT 
AS                    
BEGIN    
	IF EXISTS (SELECT 1  FROM CodeMaster Where ID = @CodeMasterID )
	BEGIN
	 SELECT *
	 FROM CodeMaster Where ID = @CodeMasterID 
	 FOR XML AUTO, ELEMENTS, ROOT('CodeMasters')    
	END
	ELSE
	BEGIN
	 SELECT TOP 1 @CodeMasterID  AS ID, 'DELETECODE' AS Code, '' AS Description 
	 FROM Setting 
	 CodeMaster FOR XML AUTO, ELEMENTS, ROOT('CodeMasters')
	END
END


GO

