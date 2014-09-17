USE [Enterprise]
GO

/****** Object:  StoredProcedure [dbo].[rsp_GetAllFaultGroup]    Script Date: 07/31/2014 16:34:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetAllFaultGroup]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetAllFaultGroup]
GO

USE [Enterprise]
GO

/****** Object:  StoredProcedure [dbo].[rsp_GetAllFaultGroup]    Script Date: 07/31/2014 16:34:33 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[rsp_GetAllFaultGroup]
AS
BEGIN
SELECT Call_Group_ID,Call_Group_Description FROM Call_Group WHERE Call_Group_End_Date Is Null ORDER BY Call_Group_Reference
END


GO


