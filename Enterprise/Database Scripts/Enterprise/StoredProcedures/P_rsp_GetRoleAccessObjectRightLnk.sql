USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetRoleAccessObjectRightLnk]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetRoleAccessObjectRightLnk]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*  
 * this stored procedure is to export theRoleAccessObjectRight_Lnk to exchange 
 * Vineetha M  29 July 2010  Created 
*/  
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  

CREATE PROCEDURE rsp_GetRoleAccessObjectRightLnk	
AS    
BEGIN  	
	DECLARE @xml XML
					SET @xml =(SELECT * FROM roleaccessobjectright_lnk
					FOR XML PATH ('ROLEACCESSOBJECTRIGHTLNK'), ELEMENTS, ROOT('ROLEACCESSOBJECTRIGHTLNKROOT')) 
					SELECT @xml 
END

GO

