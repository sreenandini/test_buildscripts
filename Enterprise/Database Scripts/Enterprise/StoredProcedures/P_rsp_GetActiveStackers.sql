
USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_GetActiveStackers]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_GetActiveStackers]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Akshaya Sree
-- Create date: 28 April 2012
-- Description:	Get the active stackers
-- =============================================

CREATE PROCEDURE rsp_GetActiveStackers
AS
BEGIN
	
	 SELECT Stacker_Id,
	        StackerName,
			StackerSize,
			StackerStatus,
			StackerDescription
	        
	 FROM   Stacker WITH(NOLOCK)
	 WHERE  SysDelete = 0
	        AND StackerStatus = 1
	          
	 ORDER BY StackerName
	
END
GO