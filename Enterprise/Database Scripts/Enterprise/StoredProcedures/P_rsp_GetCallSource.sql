USE [Enterprise]
GO

/****** Object:  StoredProcedure [dbo].[rsp_GetCallSource]    Script Date: 07/25/2014 15:37:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetCallSource]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetCallSource]
GO

USE [Enterprise]
GO

/****** Object:  StoredProcedure [dbo].[rsp_GetCallSource]    Script Date: 07/25/2014 15:37:32 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[rsp_GetCallSource]
AS
BEGIN
	SELECT * FROM Call_Source ORDER BY Call_Source_Description 
END

GO


