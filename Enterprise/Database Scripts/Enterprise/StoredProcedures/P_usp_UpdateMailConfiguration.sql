USE [Enterprise]
GO

/****** Object:  StoredProcedure [dbo].[usp_UpdateMailConfiguration]    Script Date: 07/31/2014 16:36:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_UpdateMailConfiguration]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_UpdateMailConfiguration]
GO

USE [Enterprise]
GO

/****** Object:  StoredProcedure [dbo].[usp_UpdateMailConfiguration]    Script Date: 07/31/2014 16:36:57 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*
 *	this stored procedure is to update the recipients of the events mail
 *
 *	Change History:
 *
 *	Sudarsan S			02-08-2008		created
*/
CREATE PROCEDURE [dbo].[usp_UpdateMailConfiguration]
@ID	INT,
@To	VARCHAR(1000),
@CC VARCHAR(1000)
AS

BEGIN

	--IF RIGHT(@To, 1) = ';'
	--	SET @To = LEFT(@To, LEN(@To) - 1)

	--IF RIGHT(@CC, 1) = ';'
	--	SET @CC = LEFT(@CC, LEN(@CC) - 1)

	UPDATE dbo.Datapak_Fault SET Mail_TO = @To, Mail_CC = @CC WHERE Datapak_Fault_ID = @ID

END



GO


