USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_CheckEnrolmentType]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_CheckEnrolmentType]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*
declare @Result int
 exec [dbo].[rsp_CheckEnrolmentType] '122', NULL, '556', @Result  output
select @Result 
*/
CREATE PROCEDURE [dbo].[rsp_CheckEnrolmentType]
	@Serial VARCHAR(50) = NULL,
	@Asset VARCHAR(50) = NULL,
	@GMU VARCHAR(50) = NULL,
	@Result INT OUTPUT,
	@Machine_ID INT = 0
AS
BEGIN
	SET NOCOUNT ON  
	
	
	IF @Machine_ID = 0
	    SELECT @Result = COUNT(1)
	    FROM   [Machine] m
	    WHERE  m.ActSerialNo = COALESCE(@Serial, m.ActSerialNo)
	           AND m.ActAssetNo = COALESCE(@Asset, m.ActAssetNo)
	           AND m.GMUNo = COALESCE(@GMU, m.GMUNo)
	           AND m.Machine_Status_Flag<>6
	ELSE
	    SELECT @Result = COUNT(1)
	    FROM   [Machine] m
	    WHERE  m.ActSerialNo = COALESCE(@Serial, m.ActSerialNo)
	           AND m.ActAssetNo = COALESCE(@Asset, m.ActAssetNo)
	           AND m.GMUNo = COALESCE(@GMU, m.GMUNo)
	           AND m.Machine_ID <> @Machine_ID
	           AND m.Machine_Status_Flag<>6
END  


GO

