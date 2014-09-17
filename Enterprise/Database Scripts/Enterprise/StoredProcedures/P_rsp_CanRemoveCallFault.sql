USE [Enterprise]
GO

/****** Object:  StoredProcedure [dbo].[rsp_CanRemoveCallFault]    Script Date: 07/31/2014 16:09:34 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_CanRemoveCallFault]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_CanRemoveCallFault]
GO

USE [Enterprise]
GO

/****** Object:  StoredProcedure [dbo].[rsp_CanRemoveCallFault]    Script Date: 07/31/2014 16:09:34 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[rsp_CanRemoveCallFault]
(
@Call_Fault_ID int,
@Call_Group_ID int,
@Result BIT output
)
AS
BEGIN
	
	SET @Result = 1
	IF EXISTS(SELECT 1 FROM [Service] WHERE  Call_Fault_ID = isnull(@Call_Fault_ID,0) AND isnull(@Call_Group_ID,0) = Call_Group_ID )
	BEGIN
		SET @Result = 0
	END


END


GO


