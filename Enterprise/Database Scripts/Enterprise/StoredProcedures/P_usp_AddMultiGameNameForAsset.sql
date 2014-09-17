USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[usp_AddMultiGameNameForAsset]')
              AND TYPE IN (N'P' ,N'PC')
   )
    DROP PROCEDURE [dbo].[usp_AddMultiGameNameForAsset]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE usp_AddMultiGameNameForAsset
	@Machine_ID INT,
	@MultiGameName VARCHAR(50),
	@AddNew BIT
AS
BEGIN
    IF (@AddNew=1)
    BEGIN
        IF NOT EXISTS(
               SELECT TOP 1 1
               FROM   MultiGameMapping
               WHERE  MACHINEID = @Machine_ID
           )
        BEGIN
            INSERT INTO MultiGameMapping
              (
                MachineID
               ,MultiGameName
              )
            VALUES
              (
                @Machine_ID
               ,@MultiGameName
              )
        END
        ELSE
        BEGIN
            UPDATE MultiGameMapping
            SET    MultiGameName = @MultiGameName
            WHERE  MACHINEID = @Machine_ID
        END
    END
    ELSE
    BEGIN
        DELETE 
        FROM   MultiGameMapping
        WHERE  MACHINEID = @Machine_ID
    END
END
GO

