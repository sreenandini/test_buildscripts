USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_CheckProfitShareGroupNameExists]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_CheckProfitShareGroupNameExists]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- Author:		Aishwarrya V S 
-- Create date: 12th October 2012
-- Description:	Check whether ProfitShareGroup name is already exist or not
-- =============================================
CREATE PROCEDURE rsp_CheckProfitShareGroupNameExists
	@ProfitShareGroupName VARCHAR(50),
	@ProfitShareGroupId INT,
	@NameCount INT OUTPUT
AS
BEGIN
	SELECT @NameCount = COUNT(1)
	FROM   ProfitShareGroup WITH(NOLOCK)
	WHERE  ProfitShareGroupId <> @ProfitShareGroupId
	       AND ProfitShareGroupName = @ProfitShareGroupName
	       AND SysDelete = 0
END

GO

