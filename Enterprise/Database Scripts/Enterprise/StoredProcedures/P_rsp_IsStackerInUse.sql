USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_IsStackerInUse]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_IsStackerInUse]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[rsp_IsStackerInUse]
	@StackerId INT,
	@Status INT OUTPUT
AS
BEGIN
	SET NOCOUNT ON
	
	DECLARE @UsedMachines INT	
	SET @Status = 0
	
	SELECT @UsedMachines = COUNT(1)
	FROM   dbo.Machine WITH(NOLOCK)
	WHERE  Stacker_Id = @StackerId
	       AND ISNULL(Machine_End_Date, '') = ''
	
	IF (@UsedMachines>0)
		SET @Status = 1
	
	SET NOCOUNT OFF
END

GO

