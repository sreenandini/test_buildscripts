/************************************************************
 * Code formatted by SoftTree SQL Assistant © v4.8.29
 * Time: 17/05/2013 3:06:34 PM
 ************************************************************/

USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_GetStackerDetails]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_GetStackerDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Durga Devi M
-- Create date: 2nd july 2012
-- Description:	Get stacker details
-- =============================================
CREATE PROCEDURE rsp_GetStackerDetails(@StackerCheck BIT = 0)
AS
BEGIN

IF (@StackerCheck=1)
BEGIN
	SELECT Stacker_Id,
	       StackerName,
	       StackerSize,
	       StackerStatus,
	       StackerDescription
	FROM   Stacker WITH(NOLOCK)
	WHERE  SysDelete = 0
	
	       AND @StackerCheck = 0
	       OR  (@StackerCheck = 1 AND StackerStatus = 1)
	       ORDER BY StackerName
	       END
	       ELSE
	       BEGIN
	       SELECT Stacker_Id,
	       StackerName,
	       StackerSize,
	       StackerStatus,
	       StackerDescription
	FROM   Stacker WITH(NOLOCK)
	WHERE  SysDelete = 0
	
	       AND @StackerCheck = 0
	       OR  (@StackerCheck = 1 AND StackerStatus = 1)
	       END
END
GO

