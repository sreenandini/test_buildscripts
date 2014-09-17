USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_CheckShareHolderNameExists]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_CheckShareHolderNameExists]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- Author:		Aishwarrya V S 
-- Create date: 12th October 2012
-- Description:	Check whether ShareHolder name is already exist or not
-- =============================================
CREATE PROCEDURE rsp_CheckShareHolderNameExists
	@ShareHolderName VARCHAR(50),
	@ShareHolderId INT,
	@NameCount INT OUTPUT
AS
BEGIN
	SELECT @NameCount = COUNT(1)
	FROM   ShareHolders WITH(NOLOCK)
	WHERE  ShareHolderId <> @ShareHolderId
	       AND ShareHolderName = @ShareHolderName
	       AND SysDelete = 0
END

GO

