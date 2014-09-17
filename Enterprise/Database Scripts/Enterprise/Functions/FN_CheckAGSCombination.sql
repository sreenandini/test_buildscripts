USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FN_CheckAGSCombination]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[FN_CheckAGSCombination]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*  
Exec FN_CheckAGSCombination 

*/  
CREATE FUNCTION FN_CheckAGSCombination
(
	@ActAssetNo   VARCHAR(50)
   ,@NewGMUNo     VARCHAR(50)
   ,@ActSerialNo  VARCHAR(50)
)
RETURNS BIT
AS
BEGIN
    DECLARE @IsExists BIT
    --AGS combination
    --IF EXISTS(
    --       SELECT 1
    --       FROM   [Machine] m
    --       WHERE  m.ActAssetNo = @ActAssetNo
    --              AND m.GMUNo = @NewGMUNo
    --              AND @ActSerialNo = m.ActSerialNo
    --   )
    IF EXISTS(
           SELECT 1
           FROM   [Machine] m
           WHERE m.GMUNo = @NewGMUNo                
       )
    BEGIN
        SET @IsExists = 1
    END
    ELSE
    BEGIN
        SET @IsExists = 0
    END
    
    RETURN @IsExists
END


GO

