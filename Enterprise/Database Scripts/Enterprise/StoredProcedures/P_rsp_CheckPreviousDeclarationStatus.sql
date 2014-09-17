
USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_CheckPreviousDeclarationStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_CheckPreviousDeclarationStatus]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE rsp_CheckPreviousDeclarationStatus
@CollectionNo INT
AS 
BEGIN	
	 IF EXISTS (SELECT 1 FROM COLLECTION c1 INNER JOIN COLLECTION c2
					   ON c1.Installation_ID = c2.Installation_ID
					   WHERE c1.Collection_Date < c2.Collection_Date AND c1.Declaration = 0 
						AND c2.Collection_ID = @CollectionNo)
	  BEGIN
	  	RETURN 1
	  END        
	  RETURN 0
END