/************************************************************
 * Code formatted by SoftTree SQL Assistant © v4.8.29
 * Time: 22/06/2013 1:45:12 PM
 ************************************************************/

USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_GetBarPositionExtension]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_GetBarPositionExtension]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE rsp_GetBarPositionExtension(@BarPositionID INT)
AS
BEGIN
	SET ANSI_NULLS ON
	SET NOCOUNT ON
	SELECT bpe.Bar_Position_Image FROM Bar_Position_Extension bpe
	WHERE bpe.Bar_Position_ID = @BarPositionID
END
GO
