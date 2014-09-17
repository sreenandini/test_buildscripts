USE [Enterprise]
GO

/****** Object:  StoredProcedure [dbo].[rsp_GetCallRemedy]    Script Date: 07/25/2014 15:37:05 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetCallRemedy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetCallRemedy]
GO

USE [Enterprise]
GO

/****** Object:  StoredProcedure [dbo].[rsp_GetCallRemedy]    Script Date: 07/25/2014 15:37:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[rsp_GetCallRemedy]
AS
BEGIN
	SELECT * FROM Call_Remedy WHERE Call_Remedy_End_Date IS NULL ORDER BY Call_Remedy_Description
END

GO


