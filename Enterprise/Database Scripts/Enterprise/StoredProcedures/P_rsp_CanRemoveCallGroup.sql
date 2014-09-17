USE [Enterprise]
GO

/****** Object:  StoredProcedure [dbo].[rsp_CanRemoveCallGroup]    Script Date: 07/31/2014 16:09:44 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_CanRemoveCallGroup]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_CanRemoveCallGroup]
GO

USE [Enterprise]
GO

/****** Object:  StoredProcedure [dbo].[rsp_CanRemoveCallGroup]    Script Date: 07/31/2014 16:09:44 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[rsp_CanRemoveCallGroup]
(
@Call_Group_ID int = 0,
@Result BIT output
)
AS
BEGIN
	
	SET @Result = 1
	IF EXISTS(SELECT 1 FROM [Service] WHERE  Call_Group_ID = isnull(@Call_Group_ID,0))
	BEGIN
		SET @Result = 0
	END


END

GO


