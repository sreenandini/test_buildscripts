USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_ExportLookupMaster]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_ExportLookupMaster]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

                             
CREATE PROCEDURE [dbo].[rsp_ExportLookupMaster]     
@LookupMasterID INT 
AS                    
BEGIN    
	IF EXISTS (SELECT *	 FROM LookupMaster Where ID = @LookupMasterID )
	BEGIN
		SELECT *
		FROM LookupMaster Where ID = @LookupMasterID 
		FOR XML AUTO, ELEMENTS, ROOT('LookupMasters')    
	END
	ELSE
	BEGIN
		SELECT TOP 1 @LookupMasterID AS ID, 'DELETECODE' AS Code, '' AS Description 
		FROM Setting 
		LookupMaster FOR XML AUTO, ELEMENTS, ROOT('LookupMasters')
	END
END


GO

