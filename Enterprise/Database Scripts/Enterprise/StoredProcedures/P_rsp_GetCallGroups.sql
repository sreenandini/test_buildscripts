USE [Enterprise]
GO

/****** Object:  StoredProcedure [dbo].[rsp_GetCallGroups]    Script Date: 07/25/2014 15:36:39 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetCallGroups]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetCallGroups]
GO

USE [Enterprise]
GO

/****** Object:  StoredProcedure [dbo].[rsp_GetCallGroups]    Script Date: 07/25/2014 15:36:39 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[rsp_GetCallGroups]
AS
BEGIN
	SELECT * FROM Call_Group WHERE Call_Group_End_Date Is Null ORDER BY Call_Group_Reference
END

GO


