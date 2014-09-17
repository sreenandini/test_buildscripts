USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_InsertServiceNotesDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_InsertServiceNotesDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[usp_InsertServiceNotesDetails]
	@Service_ID	INT,
	@Staff_ID INT,
	@Engineer_ID INT,		
	@Subject VARCHAR(32) = NULL,
	@Notes	VARCHAR(255),
	@Service_Notes_Date DateTime = NULL,
	@Service_Notes_In_Out INT = NULL,
	@Service_Closed_Id INT = 0
AS

BEGIN

	INSERT INTO dbo.Service_Notes 
	(Service_ID, Staff_ID, Engineer_ID, Service_Notes_Subject, Service_Notes_Notes, Service_Notes_Date, Service_Notes_In_Out,Service_Closed_Id )
	VALUES
	( @Service_ID, @Staff_ID, @Engineer_ID, @Subject, @Notes , CONVERT(VARCHAR(20), @Service_Notes_Date, 113), @Service_Notes_In_Out, @Service_Closed_Id)

	IF @@ROWCOUNT > 0
		RETURN SCOPE_IDENTITY()
	ELSE
		RETURN 0

END

GO
