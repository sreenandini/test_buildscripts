USE [Enterprise]
GO

/****** Object:  StoredProcedure [dbo].[rsp_GetAllFaultsByGroup]    Script Date: 07/31/2014 16:34:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetAllFaultsByGroup]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetAllFaultsByGroup]
GO

USE [Enterprise]
GO

/****** Object:  StoredProcedure [dbo].[rsp_GetAllFaultsByGroup]    Script Date: 07/31/2014 16:34:54 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[rsp_GetAllFaultsByGroup]
(     
 @FaultGroupID INT  
)   
AS
BEGIN
SELECT Call_Fault_ID, Call_Fault_Description FROM Call_Fault WITH (NOLOCK) WHERE Call_Fault_End_Date Is Null and Call_Group_ID=@FaultGroupID ORDER BY Call_Fault_Description
END


GO


