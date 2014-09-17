USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_CheckStackerNameExists]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_CheckStackerNameExists]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE rsp_CheckStackerNameExists
	@StackerName VARCHAR(255),
	@NameCount INT OUTPUT,
	@StackerID INT
AS
BEGIN
	SELECT @NameCount = COUNT(*)
	FROM   Stacker
	WHERE  StackerName = @StackerName
	       AND SysDelete = 0
	       AND Stacker_Id <> @StackerID
END
GO