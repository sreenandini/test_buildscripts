/************************************************************
 * Code formatted by SoftTree SQL Assistant © v4.8.29
 * Time: 03/05/2014 8:01:25 PM
 ************************************************************/

USE [Enterprise]
GO

/****** Object:  StoredProcedure [dbo].[usp_UpdateExportRefPointer]    Script Date: 03/04/2014 20:25:54 ******/
IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[usp_UpdateExportRefPointer]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[usp_UpdateExportRefPointer]
GO

USE [Enterprise]
GO

/****** Object:  StoredProcedure [dbo].[usp_UpdateExportRefPointer]    Script Date: 03/04/2014 20:25:54 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- exec [dbo].[usp_UpdateExportRefPointer] 'EBS', 2

CREATE PROCEDURE [dbo].[usp_UpdateExportRefPointer](@Type VARCHAR(50), @ID INT)
AS
BEGIN
	UPDATE [dbo].[Export_RefPointer]
	SET    [RefPointerLastID] = @ID
	WHERE  [RefPointerType] = @Type
	
	IF (@@ROWCOUNT = 0)
	BEGIN
	    INSERT INTO dbo.[Export_RefPointer]
	      (
	        [RefPointerType],
	        [RefPointerLastID]
	      )
	    VALUES
	      (
	        @Type,
	        @ID
	      )
	END
END
GO


