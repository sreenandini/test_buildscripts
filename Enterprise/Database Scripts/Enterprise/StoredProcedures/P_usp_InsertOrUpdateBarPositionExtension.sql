/************************************************************
 * Code formatted by SoftTree SQL Assistant © v4.8.29
 * Time: 22/06/2013 1:45:12 PM
 ************************************************************/

USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[usp_InsertOrUpdateBarPositionExtension]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[usp_InsertOrUpdateBarPositionExtension]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE usp_InsertOrUpdateBarPositionExtension(@BarPositionID INT, @Image IMAGE, @isDelete BIT)
AS
BEGIN
	SET ANSI_NULLS ON
	SET NOCOUNT ON
	IF @isDelete = 1
	BEGIN
	    DELETE 
	    FROM   Bar_Position_Extension 
	    WHERE  Bar_Position_ID = @BarPositionID
	END
	ELSE
	BEGIN
	    IF EXISTS (
	           SELECT 1
	           FROM   Bar_Position_Extension bpe
	           WHERE  bpe.Bar_Position_ID = @BarPositionID
	       )
	    BEGIN
	        UPDATE Bar_Position_Extension
	        SET    [Bar_Position_Image] = @Image
	        WHERE  Bar_Position_ID = @BarPositionID
	    END
	    ELSE
	    BEGIN
	        INSERT INTO Bar_Position_Extension
	          (
	            -- Bar_Position_Extension_ID -- this column value is auto-generated,
	            Bar_Position_ID,
	            Bar_Position_Image
	          )
	        VALUES
	          (
	            @BarPositionID,
	            @Image
	          )
	    END
	END
END
GO
