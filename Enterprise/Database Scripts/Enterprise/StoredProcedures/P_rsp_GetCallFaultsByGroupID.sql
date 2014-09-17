USE [Enterprise]
GO

/****** Object:  StoredProcedure [dbo].[rsp_GetCallFaultsByGroupID]    Script Date: 07/25/2014 15:35:21 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetCallFaultsByGroupID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetCallFaultsByGroupID]
GO

USE [Enterprise]
GO

/****** Object:  StoredProcedure [dbo].[rsp_GetCallFaultsByGroupID]    Script Date: 07/25/2014 15:35:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[rsp_GetCallFaultsByGroupID]
      @GroupID INT = 0
AS
BEGIN
     SELECT * FROM Call_Fault WHERE Call_Fault_End_Date IS NULL AND Call_Group_ID = @GroupId ORDER BY Call_Fault_Description
END

GO


