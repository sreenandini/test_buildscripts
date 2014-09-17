USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_ExportPaytable]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_ExportPaytable]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].rsp_ExportPaytable
@PaytableID INT 
AS                    
BEGIN    
	SELECT	MaxBet,
			TheoreticalPayout,
			Paytable_ID
	FROM	Paytable
	WHERE	Paytable_ID = @PaytableID
	FOR XML AUTO, ELEMENTS, ROOT('Paytables')    
END

GO

