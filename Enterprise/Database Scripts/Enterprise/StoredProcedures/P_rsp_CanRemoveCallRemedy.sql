USE [Enterprise]
GO

/****** Object:  StoredProcedure [dbo].[rsp_CanRemoveCallRemedy]    Script Date: 07/31/2014 16:09:48 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_CanRemoveCallRemedy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_CanRemoveCallRemedy]
GO

USE [Enterprise]
GO

/****** Object:  StoredProcedure [dbo].[rsp_CanRemoveCallRemedy]    Script Date: 07/31/2014 16:09:48 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[rsp_CanRemoveCallRemedy]
(
@Call_Remedy_ID int,
@Result BIT output
)
AS
BEGIN
	
	SET @Result = 1
	IF EXISTS(select 1 from [Service] WHERE  Call_Remedy_ID = isnull(@Call_Remedy_ID,0))
	BEGIN
		SET @Result = 0
	END


END

GO


