USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetSASVersionFromGMULogin]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetSASVersionFromGMULogin]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[rsp_GetSASVersionFromGMULogin]
@Installation_No INT,
@GL_SASVersion [varchar](20) OUTPUT
AS  
BEGIN
SET @GL_SASVersion = ''  
	SELECT 
		@GL_SASVersion = ISNULL(GL_SASVersion,'')	
	FROM 
		GMU_Login	
	WHERE 
		GL_Code = @Installation_No

	IF @GL_SASVersion IS NULL OR @GL_SASVersion = ''
		RETURN -1	
END  

GO

