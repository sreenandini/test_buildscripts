USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_SelectStackerList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_SelectStackerList]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE rsp_SelectStackerList
AS
BEGIN
SELECT STKR.Stacker_Id,STKR.StackerName FROM Stacker STKR
--INNER JOIN AssetStacker AST_STKR ON STKR.Stacker_ID NOT IN (AST_STKR.StackerID)
WHERE STKR.SysDelete=0
END

GO

