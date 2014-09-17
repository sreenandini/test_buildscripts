USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetExchangeAdminObject]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetExchangeAdminObject]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*  
 * this stored procedure is to export the object to exchange 
 * Vineetha M  29 July 2010  Created 
*/  
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  

CREATE PROCEDURE rsp_GetExchangeAdminObject	
AS    
BEGIN  	
	DECLARE @xml XML
					SET @xml =(SELECT * FROM OBJECT
					FOR XML PATH ('OBJECT'), ELEMENTS, ROOT('OBJECTROOT')) 
					SELECT @xml 
END

GO

